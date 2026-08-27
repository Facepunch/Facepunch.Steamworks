# Dedicated Servers — end to end

This guide assumes you have never shipped a Steam game. A dedicated server is a headless
process that hosts a match. It is not a player: it does not log in as a user, it has no
friends list, and it uses a different set of Steam interfaces from your game client.

Dedicated servers are the reason this fork exists, so this is the most detailed guide here —
including the parts that are currently broken, because you will hit them.

If you only read one thing: **`SteamServer.Init` does not log you on.** It sets up the
interfaces and returns. Without an explicit `LogOnAnonymous()` your server runs, listens,
authenticates nobody and appears nowhere, with no error printed anywhere.

---

## 1. Three shapes of "server", and which you have

| Shape | What it is | Steam interfaces |
|---|---|---|
| **Peer-to-peer** | One player's client is authoritative. | `SteamClient` only |
| **Listen server** | A client that also runs a game server, in one process. | `SteamClient` **and** `SteamServer` |
| **Dedicated server** | A headless process. No player is signed in. | `SteamServer` only |

This guide is about the third. The second is a genuinely awkward case in this library — a
process with both initialised resolves several shared interfaces to the *client* one, which
is wrong for server operations. §10 covers it.

What `SteamServer` gets you that a raw socket does not:

- **Identity.** Players prove who they are with a Steam auth ticket; you get a verified
  `SteamId` rather than a name they typed.
- **VAC.** Valve Anti-Cheat, enabled with one flag.
- **The server browser.** Your server appears in Steam's list, with your map name, player
  count and tags.
- **Ownership checks.** You can ask whether a connecting player actually owns your game.

---

## 2. Initialising

```csharp
using Steamworks;

var init = new SteamServerInit( "mygame", "My Game" )
{
    GamePort      = 28015,
    QueryPort     = 28016,
    Secure        = true,          // VAC
    VersionString = "1.0.0.0",
    IpAddress     = null,          // null = bind any interface
    DedicatedServer = true
};

try
{
    SteamServer.Init( 480, init );          // your AppID
}
catch ( Exception e )
{
    Console.WriteLine( $"Server init failed: {e.Message}" );
    return 1;
}
```

`SteamServerInit`'s constructor sets sensible defaults — `GamePort = 27015`,
`QueryPort = 27016`, `Secure = true`, `VersionString = "1.0.0.0"`, `DedicatedServer = true` —
so you only override what differs. Note that `default(SteamServerInit)` gets none of them;
always use the constructor.

### The two ports

| Port | Talks to | If it is closed |
|---|---|---|
| `GamePort` | Your players' game clients. | Nobody can play. |
| `QueryPort` | Steam's server browser and master servers. | The server works perfectly and **appears nowhere**. |

Both must be reachable from the internet. A server that runs fine but never shows up in the
browser is almost always a closed `QueryPort`, and nothing in the API tells you.

If you would rather not open a second port, share one:

```csharp
init = init.WithQueryShareGamePort();
```

That sets `QueryPort` to `0xFFFF`, which tells Steam to multiplex queries over `GamePort`.
Your game protocol then has to hand Steam the query packets itself — see
`SteamServer.HandleIncomingPacket` and `SteamServer.GetOutgoingPacket`. It is more work; open
the second port unless you cannot.

Note `WithQueryShareGamePort()` mutates the receiver *and* returns a copy, because
`SteamServerInit` is a struct. Assign the result, as above.

### `ModDir` must be stable

The first constructor argument is `ModDir` — a short, folder-safe identifier. The server
browser keys off it. Changing it between versions makes your existing servers look like a
different game. Pick it once.

### What `Init` actually does

Worth knowing precisely, because the gaps matter:

1. Validates you have not already initialised, and throws if you have.
2. Sets the `SteamAppId` and `SteamGameId` environment variables for the process.
3. Calls Steam's game-server init with your ports, security level and version string.
   Throws a `System.Exception` if the result is not `OK`.
4. Sets up the callback dispatch on the **server pipe**.
5. Registers the server-side interfaces.
6. Applies initial properties: `AdvertiseServer = true`, `MaxPlayers = 32`, `BotCount = 0`,
   `Product`, `ModDir`, `GameDescription`, `Passworded = false`, `DedicatedServer`.
7. Starts the async callback pump, if `asyncCallbacks` is `true` (the default).

**What it does not do:** log on, set `ServerName`, set `MapName`, or set `GameTags`. Those are
yours.

---

## 3. Logging on

```csharp
SteamServer.LogOnAnonymous();
```

That is the normal path for a dedicated server and it needs no credentials.

The alternative is a **Game Server Login Token** (GSLT), which you generate on the Steamworks
partner site:

```csharp
SteamServer.LogOn( "YOUR_GSLT_TOKEN" );
```

A GSLT ties the server to your account, which lets Valve contact you about it and lets you
ban a misbehaving host. Some games require one for a server to be publicly listed at all.
Check your app's rules; anonymous is fine for development either way.

Watch the connection events, because logon is asynchronous and failure is silent otherwise:

```csharp
SteamServer.OnSteamServersConnected += () =>
    Console.WriteLine( $"Logged on as {SteamServer.SteamId}, public IP {SteamServer.PublicIp}" );

SteamServer.OnSteamServerConnectFailure += ( result, stillRetrying ) =>
    Console.WriteLine( $"Logon failed: {result}, retrying: {stillRetrying}" );

SteamServer.OnSteamServersDisconnected += result =>
    Console.WriteLine( $"Lost Steam: {result}" );
```

Note the naming: `OnSteamServerConnectFailure` (singular "Server") takes
`(Result, bool stillRetrying)`; `OnSteamServersDisconnected` (plural) takes `(Result)`. There
is also `OnSteamNetAuthenticationStatus`, which reports whether the server's networking
identity is usable — worth logging if you are using SDR.

`SteamServer.LoggedOn` is the polled equivalent. `LogOff()` disconnects without shutting down.

**Do not treat a disconnect as fatal.** Steam connectivity drops and comes back. Keep the
match running; players already authenticated stay valid.

---

## 4. Describing yourself to the server browser

```csharp
SteamServer.ServerName      = "AnkleBreaker EU #1";
SteamServer.MapName         = "de_dust";
SteamServer.MaxPlayers      = 64;
SteamServer.BotCount        = 0;
SteamServer.Passworded      = false;
SteamServer.GameTags        = "ranked,eu,hardcore";
SteamServer.AdvertiseServer = true;
```

Update `MapName` and player counts as the match changes — the browser reflects them live.

`GameTags` is a comma-separated string and is what the client-side server browser filters on —
the repo's own tests use `list.AddFilter( "gametype", "v2405" )` against it. Design your tag
vocabulary before you ship; changing it later invalidates everyone's saved filters.

`SetKey( key, value )` adds arbitrary key/value data for rule queries; `ClearKeys()` removes
all of it.

Two shapes to be aware of:

- **`AdvertiseServer` is write-only.** There is no getter. Track it yourself if you need to
  know.
- **`ForceHeartbeat()` is an `[Obsolete]` no-op** with an empty body. It does nothing at all;
  Steam manages heartbeats. Do not call it, and do not conclude from a call to it that
  anything was sent.

### The re-init trap

Every one of those properties is a "set only if changed" cache, and **`Shutdown()` does not
reset the caches.** So a second `Init` in the same process compares against the *previous*
run's values and skips the native calls entirely — `SetModDir`, `SetProduct`,
`SetGameDescription`, `SetMaxPlayerCount` and `SetPasswordProtected` are never made on the
fresh interface. The SDK requires those before logon, so the server logs on with SDK defaults
and lists incorrectly, silently.

The `SetKey` dictionary survives too, so identical keys are skipped on the second run.

**Workarounds, in order of preference:**

1. **Do not re-init in-process.** Restart the process between matches. This is what most
   dedicated servers do anyway.
2. If you must, set every property to a throwaway value and back after the second `Init`, so
   the change-detection fires:
   ```csharp
   SteamServer.MaxPlayers = 1;
   SteamServer.MaxPlayers = 64;      // now the native call actually happens
   ```
   Note this does not work for `ModDir`, `Product` and `GameDescription` — their setters are
   `internal`, so you cannot reach them from outside the assembly at all.

This is [audit 04](../audit/04-gameserver.md) F3, and it is open.

---

## 5. The tick loop

```csharp
while ( running )
{
    SteamServer.RunCallbacks();
    socket.Receive();               // your networking, see §7
    Tick();
    Thread.Sleep( 16 );
}
```

`SteamServer.RunCallbacks()` pumps the **server** pipe. It is not the same call as
`SteamClient.RunCallbacks()`, and on a listen server you need both.

If you left `asyncCallbacks` at its default of `true`, a background loop pumps the server
pipe every **32 ms** and you do not have to call it — but calling it anyway is harmless and
gives you deterministic timing. For a headless server there is little reason to want the
background pump; pass `asyncCallbacks: false` and drive it yourself.

**Install an exception hook during development:**

```csharp
Dispatch.OnException += e => Console.WriteLine( $"[steam callback] {e}" );
```

Exceptions thrown inside callback handlers are otherwise swallowed, *and* they permanently
drop the remaining handlers for that callback. Without this hook, the symptom is a server
that silently stops receiving auth responses.

---

## 6. Authenticating players

This is the part that makes a Steam server a Steam server. The shape is:

```
  Client                          Your transport             Server                Steam
    │                                                          │                     │
    ├─ GetAuthSessionTicketAsync ─────────────────────────────────────────────────►  │
    │◄─ ticket bytes ──────────────────────────────────────────────────────────────  │
    ├─ send ticket ──────────────►│                            │                     │
    │                             ├─ BeginAuthSession(bytes) ─►├────────────────────►│
    │                             │                            │◄─ OnValidateAuth ───┤
    │◄──────── admitted or kicked ┤◄───────────────────────────┤                     │
```

### Client side

```csharp
AuthTicket ticket = await SteamUser.GetAuthSessionTicketAsync( serverIdentity );

if ( ticket == null )
{
    ShowError( "Could not get a Steam auth ticket." );
    return;
}

SendToServer( ticket.Data );

// Later, when leaving:
ticket.Cancel();
```

Prefer the **async** form. The synchronous `GetAuthSessionTicket` hands you a ticket before
Steam has confirmed it is usable; the async one waits for the confirmation callback, with a
default 10-second timeout. An unconfirmed ticket fails validation intermittently and
unreproducibly, which is a miserable bug to chase.

Cancelling on leave is not optional housekeeping — it is how the server learns the player is
gone. The server receives another `OnValidateAuthTicketResponse` with
`AuthResponse.AuthTicketCanceled`.

### Server side

```csharp
SteamServer.OnValidateAuthTicketResponse += ( steamId, ownerId, response ) =>
{
    if ( response != AuthResponse.OK )
    {
        Kick( steamId, response.ToString() );
        return;
    }

    if ( ownerId != steamId )
        Log( $"{steamId} is playing on {ownerId}'s licence (Family Sharing)" );

    Admit( steamId );
};

// When a player sends their ticket:
if ( !SteamServer.BeginAuthSession( ticketBytes, claimedSteamId ) )
{
    Kick( claimedSteamId, "bad ticket" );
    return;
}
// Now WAIT for OnValidateAuthTicketResponse. Do not admit them yet.

// When they leave, for any reason:
SteamServer.EndSession( steamId );
```

Four things to get right:

1. **`BeginAuthSession` returning `true` does not mean the player is authenticated.** It means
   the ticket was well-formed enough to start validation. The answer arrives later on
   `OnValidateAuthTicketResponse`. Admit players there, not here.

2. **`ownerId` versus `steamId`.** With Steam Family Sharing they differ: `steamId` is who is
   playing, `ownerId` is whose licence they are using. Ban lists should usually key on
   `ownerId` so a shared account cannot evade a ban, but player identity is `steamId`.

3. **Always call `EndSession`.** It is the counterpart to `BeginAuthSession` and nothing calls
   it for you — not a denial response, not `Shutdown()`. Any path that returns early between
   begin and "player fully connected" leaks a session on Steam's side. The visible symptom is
   a player who cannot rejoin after a network blip, because Steam answers the second
   `BeginAuthSession` with `DuplicateRequest`. Put `EndSession` in your disconnect handler and
   drain any remaining sessions on shutdown.

4. **`false` is unattributable.** `SteamServer.BeginAuthSession` collapses Steam's
   `BeginAuthResult` enum to a `bool`, so you cannot distinguish `InvalidTicket` from
   `DuplicateRequest` from `GameMismatch` from `ExpiredTicket` — which are four very
   different operator problems. (The client-side `SteamUser.BeginAuthSession` does return the
   enum.) This is [audit 04](../audit/04-gameserver.md) F4, and it is open.

### Ownership and DLC

```csharp
var licence = SteamServer.UserHasLicenseForApp( steamId, dlcAppId );

if ( licence != UserHasLicenseForAppResult.HasLicense )
    DenyDlcContent( steamId );
```

### Server-issued tickets — currently broken

The fork added `SteamServer.GetAuthSessionTicket( NetIdentity )` so a server can authenticate
*itself* to your own backend. The ticket works; **cancelling it does not.**

```csharp
// DO NOT do this on a dedicated server:
using ( var t = SteamServer.GetAuthSessionTicket( backendIdentity ) )   // throws on scope exit
{
    PostToBackend( t.Data );
}
```

`AuthTicket.Cancel()` hard-codes the *client* interface (`SteamUser.Internal`), which is never
registered on a dedicated server, so it is `null` and `Cancel()` / `Dispose()` throw a
`NullReferenceException`. On a listen server it is worse than a crash: it cancels on the
user's interface, so the server-side handle leaks in Steam and an unrelated client ticket may
be invalidated.

Until it is fixed: **do not wrap a server ticket in `using`, and do not call `Cancel()` on
it.** Let the handle leak for the process lifetime, which is finite for a server. There is no
public `SteamServer.CancelAuthTicket` to call instead.

[Audit 04](../audit/04-gameserver.md) F2.

---

## 7. Networking

Use `SteamNetworkingSockets` — see [the networking guide](04-networking-transports.md) for the
full picture. The server-specific parts:

**Poll groups.** Drain every connection in one native call rather than one per connection. At
100 players and 60 Hz that is 60 calls per second instead of 6,000, and receive cost scales
with message count rather than player count. `SocketManager` gives you one for free
(`socket.PollGroup`); create your own to partition traffic per match or per room.

**Zero-allocation receive.** Use the `IntPtr` handler shape, hoisted into a cached field. The
`byte[]` convenience overloads allocate one array per message and are documented in-source as
prototype-only. This fork's `SocketManager.Receive` was rewritten to be allocation-free — a
100-player server was producing roughly 460 KB/s of pure garbage before that.

**Hosted SDR servers.** If your server runs in a Valve data centre, it can sit inside the
relay network: players never learn its address, so nobody can DDoS the server they just
joined.

```csharp
ushort   port = SteamNetworkingSockets.HostedDedicatedServerPort;
NetPOPID pop  = SteamNetworkingSockets.HostedDedicatedServerPopId;

var socket = SteamNetworkingSockets.CreateHostedDedicatedServerSocket<GameServer>();
```

These require `SteamServer.Init` to have run and throw `InvalidOperationException` naming it
if not. See [poll groups & hosted servers](../sdr/poll-groups-and-hosted-servers.md).

---

## 8. Server-side stats

A server can write stats and achievements on behalf of players, through `SteamServerStats`
(not `SteamUserStats`):

```csharp
var result = await SteamServerStats.RequestUserStatsAsync( steamId );
if ( result != Result.OK ) return;

int kills = SteamServerStats.GetInt( steamId, "kills" );
SteamServerStats.SetInt( steamId, "kills", kills + 1 );
SteamServerStats.SetAchievement( steamId, "ACH_FIRST_BLOOD" );

await SteamServerStats.StoreUserStats( steamId );
```

Note the naming: the download is `RequestUserStatsAsync` but the commit is `StoreUserStats`
with no `Async` suffix, despite both being awaitable. The setters are `SetInt` / `SetFloat`,
not `SetUserStat`.

**Two hard constraints, and a real hazard.**

- **Writing stats requires an official server.** Steam only accepts stat writes from servers
  on the publisher's registered IP range. On a community-hosted server the writes are
  rejected. Design for the server *reporting* results to your backend, and let the backend or
  the client write stats, unless you run the servers yourself.

- **The getters cannot fail visibly.** `GetInt`, `GetFloat` and `GetAchievement` return the
  default when the call fails, when the stats were never requested, and when the value
  genuinely is zero/false. All three look identical.

- **That combination is dangerous.** Steam unloads a user's cached stats once they are no
  longer on the server, and the callback that announces this (`GSStatsUnloaded_t`) is
  **not surfaced by this library**. So a read-modify-write after an unload reads zeros, adds
  to zero, and stores — **overwriting the player's real progress**. Re-request stats after any
  Steam reconnect, and treat a zero read for an established player as suspicious rather than
  authoritative.

[Audit 04](../audit/04-gameserver.md) F10.

---

## 9. Finding your server from a client

The server browser is **client-side only** — `ServerList` uses an interface with no
game-server accessor, which is correct.

```csharp
using ( var list = new ServerList.Internet() )
{
    list.AddFilter( "gamedir", "mygame" );
    list.AddFilter( "map", "de_dust" );

    var reason = await list.RunQueryAsync( timeoutSeconds: 10 );

    if ( reason != QueryEndReason.EndOfRefresh )
        Log( $"query ended early: {reason}" );

    foreach ( var server in list.Responsive )
        Console.WriteLine( $"{server.Address}:{server.ConnectionPort} " +
                           $"{server.Name} {server.Players}/{server.MaxPlayers} {server.Ping}ms" );
}
```

`RunQueryAsync` returns a `QueryEndReason` — `EndOfRefresh`, `TimeOut`,
`CancelledOrChangedRequest` or `InvalidClient`. **This is a fork change**: upstream returned
`Task<bool>`, which could not distinguish a timeout from a cancellation. It is a
source-breaking change if you are migrating.

The five list types are `Internet`, `LocalNetwork`, `Favourites` (British spelling),
`History` and `Friends`, plus `IpList` for querying specific addresses. Note that
`LocalNetwork` **ignores filters entirely** — a LAN query returns everything on the subnet.

Results land in `Responsive`, `Unresponsive` and `Unqueried`. `ServerInfo` carries `Name`,
`Map`, `Players`, `MaxPlayers`, `BotPlayers`, `Ping`, `Passworded`, `Secure`, `Tags`,
`Address`, `ConnectionPort` and `QueryPort`. `Tags` returns **`null`**, not an empty array,
when the server set none.

`server.QueryRulesAsync()` fetches the key/value data you published with `SetKey`.

### Two open defects in the browser

- **Disposing a list while a query is in flight is a use-after-free.** The polling loop runs
  on thread-pool continuations and can call into a request that `Dispose()` has already
  released — a crash inside `steamclient` with no managed stack. **Await `RunQueryAsync`
  before leaving the `using` scope**, exactly as the snippet above does. Do not write
  `_ = list.RunQueryAsync();` inside a `using`.
- **A timed-out query is never cancelled.** The method returns but the native query keeps
  pinging every server until something disposes it. Always dispose, and prefer one long-lived
  list over one per refresh.

[Audit 04](../audit/04-gameserver.md) F6 and F7.

---

## 10. Things that will crash your server

Read this section before you ship. All four are verified, and all four are silent.

### Never call `SteamApps` on a dedicated server

```csharp
var lang = SteamApps.GameLanguage;      // ACCESS VIOLATION on a dedicated server
```

`SteamApps` is registered by `SteamServer.Init`, but `ISteamApps` has **no game-server
accessor** in the SDK — so its native pointer stays null, and the registration failure is
discarded rather than checked (unlike on the client, which tears the interface down). Every
`SteamApps` member then passes a NULL `this` into native code. That is a segfault, not a
catchable .NET exception, and there is no `IsValid` you can test.

On a listen server it is masked, because the client interface wins — which means it works
during development and kills the dedicated build.

**Do not touch `SteamApps` from server code at all.** [Audit 04](../audit/04-gameserver.md) F1.

### Shutdown races the callback pump

```csharp
SteamServer.Shutdown();
```

The async pump is an `async void` loop with no cancellation and nothing to join, so an
in-flight frame can call into a pipe that has just been destroyed. This is a crash-on-exit,
which for a headless server means a non-zero exit code that a supervisor will interpret as a
failure.

**Mitigation:** initialise with `asyncCallbacks: false` and pump manually, then stop pumping
before you call `Shutdown()`. That removes the race entirely, since there is no background
loop to be mid-frame. [Audit 02](../audit/02-dispatch-memory-threading.md).

### Awaiting anything across shutdown hangs forever

Pending call results are abandoned on shutdown rather than completed or cancelled — measured:
5,000 registered continuations, 0 invoked. Any `Task` still awaiting a Steam call when you
shut down never completes. Do not `await` a Steam operation in your shutdown path.

### Listen servers use the wrong interface

If you initialise both a client and a server in one process, `SteamNetworkingSockets`,
`SteamUtils`, `SteamUGC`, `SteamInventory` and `SteamNetworkingMessages` all resolve to the
**client** interface, because the shared accessor prefers it. So a listen server creates
listen sockets under the *user's* identity rather than the game server's. Nothing errors;
the behaviour is just wrong.

The SDR hosted-server calls added by this fork deliberately bypass this and go through the
server interface explicitly. Nothing else does.
[Audit 04](../audit/04-gameserver.md) F5.

---

## 11. Shipping a Linux server

Almost every dedicated server runs on Linux. Two things to get right.

**Ship the right native binary.** `libsteam_api.so` must sit beside your server executable:

| Target | File | Comes from |
|---|---|---|
| Linux x64 | `libsteam_api.so` | `Facepunch.Steamworks/linux64/libsteam_api.so` |
| Linux x86 | `libsteam_api.so` | `Facepunch.Steamworks/linux32/libsteam_api.so` |

**You must copy it yourself.** The Posix project does not copy the `.so` to its output
directory, and the build output directory is shared with the Windows projects — so
"copy `bin/Release/net6.0` to the Linux box and run" produces `DllNotFoundException` on the
first P/Invoke. Add the copy to your packaging step.

Both files are the current SDK drop and are verified against every P/Invoke declaration on
each build. This matters more than it sounds: before this fork, the committed Linux libraries
were about eleven SDK releases stale and **missing `SteamInternal_GameServer_Init_V2`
entirely** — a Linux dedicated server shipped with them could not start at all, throwing
`EntryPointNotFoundException` before doing anything. That class of defect is now caught by
`verify-native-conformance.ps1` on every build.

The two files have the same name, and there is no `DllImportResolver` in this library to pick
between them, so stage exactly one.

**Use the right build.** The Posix managed assembly is `Facepunch.Steamworks.Posix.dll` —
not the Win64 one. The pack size of Steam's callback structs differs between Windows and
POSIX, which is a compile-time constant per platform build. Running the Windows assembly on
Linux would misread every callback.

---

## 12. A minimal working server

```csharp
using System;
using System.Threading;
using Steamworks;

class Program
{
    static bool running = true;

    static int Main()
    {
        var init = new SteamServerInit( "mygame", "My Game" )
        {
            GamePort = 28015, QueryPort = 28016,
            Secure = true, VersionString = "1.0.0.0"
        };

        try
        {
            SteamServer.Init( 480, init, asyncCallbacks: false );
        }
        catch ( Exception e )
        {
            Console.Error.WriteLine( $"Steam init failed: {e.Message}" );
            return 1;
        }

        Dispatch.OnException += e => Console.Error.WriteLine( $"[callback] {e}" );

        SteamServer.OnSteamServersConnected += () =>
            Console.WriteLine( $"Online as {SteamServer.SteamId}" );

        SteamServer.OnSteamServerConnectFailure += ( r, retrying ) =>
            Console.Error.WriteLine( $"Logon failed: {r} (retrying: {retrying})" );

        SteamServer.OnValidateAuthTicketResponse += ( id, owner, response ) =>
        {
            if ( response == AuthResponse.OK ) Admit( id );
            else                               Kick( id, response.ToString() );
        };

        SteamServer.ServerName = "My Server";
        SteamServer.MapName    = "de_dust";
        SteamServer.MaxPlayers = 64;
        SteamServer.LogOnAnonymous();

        Console.CancelKeyPress += ( s, e ) => { e.Cancel = true; running = false; };

        while ( running )
        {
            SteamServer.RunCallbacks();
            // socket.Receive();  -- your transport
            Thread.Sleep( 16 );
        }

        // Stop pumping BEFORE shutting down, so nothing is mid-frame.
        SteamServer.Shutdown();
        return 0;
    }

    static void Admit( SteamId id ) => Console.WriteLine( $"admit {id}" );
    static void Kick( SteamId id, string why ) => Console.WriteLine( $"kick {id}: {why}" );
}
```

That is a complete, correct Steam dedicated server: initialised without the async pump,
exception hook installed, logged on, describing itself to the browser, authenticating players
properly, and shutting down without racing the pump.

---

## Open gaps you should know about

Beyond the defects above, these are missing capabilities rather than bugs:

- **`GSClientKick_t` is not surfaced.** This is Steam telling your server *"kick this
  player"* — the mechanism by which a VAC ban or account action reaches a live session. A
  server built on this binding **cannot receive that instruction**, so a player banned
  mid-match stays until they leave voluntarily. Nine other game-server callbacks are
  generated but never installed, including `GSPolicyResponse_t` (how the server learns whether
  it may display as VAC-secure), `GSClientApprove_t` and `GSClientDeny_t`.
- **`SteamServer.GetAuthSessionTicket` has no async "ticket is ready" form**, unlike the
  client's. The synchronous ticket may not be usable yet.
- **`SteamServerStats` omits `UpdateUserAvgRateStat`**, which is bound internally.
- **`ISteamHTTP` has no managed facade at all**, on server or client.

All are catalogued with evidence in [audit 04](../audit/04-gameserver.md) and
[audit 03](../audit/03-api-coverage-gaps.md).

---

## Where to go next

- [Networking transports](04-networking-transports.md) — poll groups, zero-allocation
  receive, and the SDR modes in full.
- [Poll groups & hosted servers](../sdr/poll-groups-and-hosted-servers.md) — the hosted
  dedicated server path, and every behaviour inferred rather than documented.
- [GameServer audit](../audit/04-gameserver.md) — the evidence behind every defect named
  here, with the exact source lines.
- [Getting Started](01-getting-started.md) — the callback model, if anything above never
  fires.
