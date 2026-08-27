# Lobbies & Matchmaking — getting players into the same game

This guide assumes you have never shipped a Steam game. A **lobby** is Steam's answer to
"how do a group of players agree to play together before anyone connects to anything". It is
a small piece of shared, replicated state hosted by Steam: a member list, a bag of key/value
data, and a chat channel.

If you only read one thing: **a lobby you create is invisible, and stays invisible until you
call `SetPublic()`.** Every "my lobby doesn't show up in searches" question has this answer.

---

## 1. What a lobby is, and what it is not

```
        Steam's servers
   ┌─────────────────────────┐
   │  Lobby 109775241234567  │
   │  ─────────────────────  │
   │  owner:  Alice          │◄──── replicated to every member
   │  members: Alice, Bob    │      (Steam pushes updates)
   │  data:  map=de_dust     │
   │         mode=ranked     │
   └─────────────────────────┘
        ▲              ▲
        │              │
     Alice           Bob            ← no direct connection between them yet
```

A lobby **is**:

- a rendezvous point, discoverable by search or invite,
- a replicated key/value store (map, mode, password-protected, whatever you need),
- a member list with an owner,
- a chat channel.

A lobby is **not** a network transport. Nobody's game traffic goes through it. When everyone
has agreed to play, you connect them — peer to peer or to a game server — using one of the
transports in [the networking guide](04-networking-transports.md). The lobby is how you
distribute the address to connect *to*.

Lobbies are cheap and Steam-hosted, so use them even for a two-player co-op game. You get
invites, the friends-list "Join Game" button and Steam overlay integration for free, and
those are genuinely tedious to build yourself.

---

## 2. Creating a lobby

```csharp
Lobby? lobby = await SteamMatchmaking.CreateLobbyAsync( maxMembers: 8 );

if ( !lobby.HasValue )
{
    // Steam refused, or the call result never arrived.
    ShowError( "Could not create a lobby." );
    return;
}

var l = lobby.Value;

l.SetPublic();                       // <-- without this, nobody can find it
l.SetJoinable( true );
l.SetData( "map", "de_dust" );
l.SetData( "mode", "ranked" );
```

`maxMembers` defaults to 100 and **cannot exceed 250**.

### Why `SetPublic()` is not the default

`CreateLobbyAsync` creates the lobby as **invisible**, deliberately. The library's own
comment says so: *"Creates a new invisible lobby. Call `Lobby.SetPublic` to take it online."*

That ordering exists so you can populate the lobby's data *before* anyone can find it. If the
lobby went public the instant it existed, a search could return it in the window before you
set `map` and `mode`, and the searcher would see a lobby with no data — or worse, filter it
out because the key it was filtering on had not been written yet.

So the shape is always: create, configure, publish.

The four visibility settings:

| Call | Who can find and join |
|---|---|
| `SetPublic()` | Anyone, via `LobbyList` search. |
| `SetFriendsOnly()` | The owner's friends, via the friends list. Not returned by search. |
| `SetPrivate()` | Invite only. |
| `SetInvisible()` | Nobody — the state a new lobby starts in. |

`SetJoinable( bool )` is orthogonal to all four. Use it to slam the door once a match starts
without changing the lobby's visibility, so it still appears in your own UI.

### Failure is `null`, never an exception

Every async call in this API signals failure by returning `null`. Nothing throws for a Steam
error. `CreateLobbyAsync` returns `null` if the call result never came back *or* if
`Result != Result.OK` — you cannot tell those apart, which in practice does not matter
because your recovery is the same.

---

## 3. Finding lobbies

```csharp
Lobby[] lobbies = await SteamMatchmaking.LobbyList
                            .WithKeyValue( "mode", "ranked" )
                            .WithSlotsAvailable( 1 )
                            .FilterDistanceClose()
                            .WithMaxResults( 20 )
                            .RequestAsync();

if ( lobbies == null )
{
    ShowMessage( "No games found." );
    return;
}

foreach ( var lobby in lobbies )
    AddToBrowser( lobby.GetData( "map" ), lobby.MemberCount, lobby.MaxMembers );
```

> **`RequestAsync()` returns `null`, not an empty array, when nothing matches.** This is the
> single most common crash in lobby browser code, because "no lobbies" is the normal state
> during development when you are the only person running the game. Null-check before you
> enumerate.

`SteamMatchmaking.LobbyList` returns a fresh `LobbyQuery` each time you read it. `LobbyQuery`
is a **struct**, and every filter returns a modified copy — so you must chain or reassign:

```csharp
var q = SteamMatchmaking.LobbyList;
q.WithKeyValue( "mode", "ranked" );      // WRONG — result discarded, filter lost
q = q.WithKeyValue( "mode", "ranked" );  // right
```

### The filters

| Filter | Effect |
|---|---|
| `WithKeyValue( key, value )` | Exact string match on a lobby data key. |
| `WithEqual( key, int )` / `WithNotEqual` | Numeric comparison. |
| `WithHigher( key, int )` / `WithLower` | Numeric comparison. |
| `WithSlotsAvailable( int )` | At least this many free member slots. |
| `OrderByNear( key, int )` | Sort by closeness to a target value, rather than filter. |
| `FilterDistanceClose()` / `FilterDistanceFar()` / `FilterDistanceWorldwide()` | Geographic scope. |
| `WithMaxResults( int )` | Cap the result count. |

Things to know:

- **Keys are limited to 255 characters** and every filter validates this.
- **String filters are exact-match only.** There is no substring, prefix or not-equal
  variant for strings — `WithNotEqual` is numeric. If you need "any map except X", encode it
  numerically or filter client-side after the query.
- **Do not pass the same key twice.** `WithKeyValue` and `OrderByNear` store into a
  dictionary keyed by the filter key, so a duplicate throws `ArgumentException` when you
  build the query, not when you run it.
- **`OrderByNear` priority is not guaranteed.** Its documentation says each successive filter
  is lower priority than the last, but the filters are replayed by iterating a `Dictionary`,
  whose order is not contractually defined. Use one `OrderByNear` and rely on it; do not
  build a multi-level sort out of them.
- **Distance filters mean nothing to Steam without a reason to differ.** If your player base
  is small, `FilterDistanceWorldwide()` is usually the right default; a close filter on an
  empty region returns nothing at all.

For latency-aware matchmaking that actually measures rather than guessing by region, put an
SDR ping location in the lobby data and estimate against it — see
[ping locations](../sdr/ping-locations-and-certificates.md#part-a--ping-locations). That is
strictly better than distance filters, and it costs no round trips.

---

## 4. Joining

There are two ways in, and they are not equivalent.

```csharp
// Shape A — you get a Lobby back, or null.
Lobby? joined = await SteamMatchmaking.JoinLobbyAsync( lobbyId );

// Shape B — you get the actual reason.
RoomEnter result = await new Lobby( lobbyId ).Join();
```

**Prefer shape B.** `JoinLobbyAsync` never inspects Steam's `EChatRoomEnterResponse`, so a
non-`null` return does **not** prove you got in — it only proves the call completed. `Join()`
surfaces the real answer:

```csharp
var lobby = new Lobby( lobbyId );
var result = await lobby.Join();

if ( result != RoomEnter.Success )
{
    ShowError( result switch
    {
        RoomEnter.Full            => "That game is full.",
        RoomEnter.DoesntExist     => "That game no longer exists.",
        RoomEnter.NotAllowed      => "You are not allowed to join.",
        RoomEnter.Banned          => "You are banned from that game.",
        RoomEnter.MemberBlockedYou=> "A player there has blocked you.",
        RoomEnter.YouBlockedMember=> "You have blocked a player there.",
        _                         => $"Could not join ({result})."
    } );
    return;
}
```

`RoomEnter` also has `Limited` (the account is limited), `ClanDisabled`, `CommunityBan`,
`RatelimitExceeded` and `Error`. Handle `Full` and `DoesntExist` explicitly at minimum —
between a search returning a lobby and the player clicking it, both are likely.

`new Lobby( id )` is just a handle around a `SteamId`; constructing one joins nothing and
validates nothing.

### Leaving

```csharp
lobby.Leave();
```

Always leave explicitly when the player backs out. Steam removes members that disconnect, but
a client that quits to the main menu without leaving stays in the lobby until Steam notices,
occupying a slot.

---

## 5. Lobby data — the shared state

```csharp
// Owner writes:
lobby.SetData( "map", "de_dust" );
lobby.SetData( "server", "192.168.1.5:28015" );

// Everyone reads:
string map = lobby.GetData( "map" );

// Enumerate everything:
foreach ( var kv in lobby.Data )
    Console.WriteLine( $"{kv.Key} = {kv.Value}" );

lobby.DeleteData( "map" );
```

Steam replicates this to every member and raises `OnLobbyDataChanged`. It is the right place
for anything the whole group needs to agree on.

**Limits, enforced by throwing:**

| | Limit |
|---|---|
| Key length | 255 characters |
| Value length | 8192 characters |

Both throw `ArgumentException` at the call site. (A minor wart: the value-too-long exception
reports `"key"` as its `ParamName`, because the check passes the wrong `nameof`. If you get
an `ArgumentException` naming `key` on a short key, it is your *value* that is too long.)

Only members can read a lobby's data, and only after joining. The SDK states lobby metadata
is usable immediately once `Join` completes, so there is no second wait.

### Per-member data

```csharp
lobby.SetMemberData( "ready", "1" );                    // always about YOU
string ready = lobby.GetMemberData( someFriend, "ready" );
```

`SetMemberData` sets **your own** data — there is no way to write another member's, which is
what you want, since it makes each member the authority on their own state. It returns
`void`, unlike `SetData` which returns `bool`; there is no success signal.

This is the natural home for ready-checks, chosen character, chosen team, loadout — anything
each player decides for themselves.

### Refresh

`lobby.Refresh()` pulls down metadata for a lobby you are **not** a member of, which is how a
browser shows details without joining. Do not call it for a lobby you are in; you already
receive updates. It returns `false` if you are not connected to Steam, and the data arrives
asynchronously via `OnLobbyDataChanged`.

---

## 6. Members and ownership

```csharp
Console.WriteLine( $"{lobby.MemberCount}/{lobby.MaxMembers}" );

foreach ( Friend member in lobby.Members )
    Console.WriteLine( $"{member.Id} {member.Name}" );

if ( lobby.IsOwnedBy( SteamClient.SteamId ) )
    OnlyTheOwnerCanDoThis();

lobby.Owner = new Friend( someMemberId );      // hand over ownership
```

**`Members` is re-evaluated live.** Each iteration re-reads the member count through the
native boundary, so the collection can change shape while you are enumerating it. Snapshot it
before doing anything expensive:

```csharp
var members = lobby.Members.ToList();
```

Steam picks a new owner automatically if the current one leaves, so your game must cope with
ownership moving at any moment — do not cache "am I the host" across frames.

`MaxMembers` is settable, but only by the owner.

---

## 7. Chat

```csharp
lobby.SendChatString( "ready when you are" );

SteamMatchmaking.OnChatMessage += ( lobby, sender, message ) =>
{
    AppendToChatLog( $"{sender.Name}: {message}" );
};
```

That is the whole feature, and it is genuinely useful — pre-game chat with no server of your
own.

**`SendChatBytes` is not binary-safe on the receive side.** The send path appends a NUL
terminator, and `OnChatMessage` decodes with a routine that stops at the first NUL. So
arbitrary bytes containing a zero are truncated when they come back out. If you want to
push structured data through the lobby, use lobby data or member data, or base64 it — do not
use the chat channel as a byte pipe.

Received messages are capped at 32 KiB by the shared decode buffer.

---

## 8. Events

Subscribe with `+=`. All of these are on `SteamMatchmaking` and are real C# `event`s.

| Event | Signature | Fires when |
|---|---|---|
| `OnLobbyCreated` | `Action<Result, Lobby>` | Your `CreateLobbyAsync` completed. |
| `OnLobbyEntered` | `Action<Lobby>` | You entered a lobby. |
| `OnLobbyMemberJoined` | `Action<Lobby, Friend>` | Someone joined. |
| `OnLobbyMemberLeave` | `Action<Lobby, Friend>` | Someone left. |
| `OnLobbyMemberDisconnected` | `Action<Lobby, Friend>` | Someone dropped. |
| `OnLobbyMemberKicked` | `Action<Lobby, Friend, Friend>` | `(lobby, whoWasKicked, whoDidIt)` |
| `OnLobbyMemberBanned` | `Action<Lobby, Friend, Friend>` | `(lobby, whoWasBanned, whoDidIt)` |
| `OnLobbyDataChanged` | `Action<Lobby>` | Lobby data changed. |
| `OnLobbyMemberDataChanged` | `Action<Lobby, Friend>` | A member's data changed. |
| `OnChatMessage` | `Action<Lobby, Friend, string>` | A chat message arrived. |
| `OnLobbyInvite` | `Action<Friend, Lobby>` | **Inviter first, lobby second.** |
| `OnLobbyGameCreated` | `Action<Lobby, uint, ushort, SteamId>` | `(lobby, ip, port, serverId)` |

Three things that catch people:

- **`OnLobbyInvite` is `(Friend, Lobby)`** while `SteamFriends.OnGameLobbyJoinRequested` is
  `(Lobby, SteamId)`. The order is reversed between the two, and both compile either way if
  you name your lambda parameters carelessly.
- **`OnLobbyMemberLeave` and `OnLobbyMemberDisconnected` can both fire for one departure.**
  Steam's underlying state-change value is a bit field, and the library raises an event per
  bit. Make your leave handling idempotent.
- **A failed data refresh raises nothing at all.** If Steam reports the update as
  unsuccessful the library returns early, so `Refresh()` on a lobby that no longer exists
  produces silence, not an error.

Note also that an exception thrown inside one of your handlers is swallowed and **prevents
the remaining handlers for that same callback from running** — see
[audit 02](../audit/02-dispatch-memory-threading.md). Set `Dispatch.OnException` during
development so you at least see them.

---

## 9. Invites and joining from the friends list

This is the part that makes lobbies worth using, and the part with a genuine gap you must
fill yourself.

### Inviting

```csharp
lobby.InviteFriend( friendSteamId );          // direct invite

SteamFriends.OpenGameInviteOverlay( lobby.Id );   // let Steam show the picker
```

### Being invited — the game is already running

```csharp
SteamFriends.OnGameLobbyJoinRequested += async ( lobby, invitedBy ) =>
{
    var result = await lobby.Join();
    if ( result == RoomEnter.Success )
        SwitchToLobbyScreen( lobby );
};
```

This fires when the player clicks an invite, or "Join Game" on the friends list, while your
game is running. Handle it or those buttons do nothing.

`SteamMatchmaking.OnLobbyInvite` is the lower-level notification that an invite *arrived*, for
if you want to show your own in-game toast rather than relying on Steam's.

### Being invited — the game is not running yet

Steam launches your executable with **`+connect_lobby <64-bit lobby id>`** on the command
line. **This library does not handle that for you** — there is no parsing of `+connect_lobby`
anywhere in it. You must do it at startup:

```csharp
// After SteamClient.Init succeeds.
static ulong? PendingLobbyFromCommandLine()
{
    // Prefer Steam's own copy: it is used when Steam launches you via a URL, and it
    // keeps the connect string off the OS command line, which is a security improvement.
    var line = SteamApps.CommandLine;
    var args = string.IsNullOrEmpty( line )
        ? Environment.GetCommandLineArgs()
        : line.Split( ' ' );

    for ( int i = 0; i < args.Length - 1; i++ )
    {
        if ( args[i] == "+connect_lobby" && ulong.TryParse( args[i + 1], out var id ) )
            return id;
    }

    return null;
}
```

then join it once you are initialised:

```csharp
if ( PendingLobbyFromCommandLine() is ulong id )
    await new Lobby( id ).Join();
```

**Why `SteamApps.CommandLine` and not just `Environment.GetCommandLineArgs()`?** Because the
OS command line is visible to every process on the machine and can be spoofed by anything
that can launch your executable. Steam supports delivering the connect string through the API
instead, which is the safer path — but it is **opt-in per app** and has to be enabled in your
app's Steam configuration by Valve. Until it is, `SteamApps.CommandLine` is empty and the
argument only appears on the OS command line. The snippet above handles both, which is why
it checks Steam's copy first and falls back.

`SteamApps.OnNewLaunchParameters` fires if the player activates a URL launch while your game
is already running.

### Rich presence — the "Join Game" button

For the friends-list "Join Game" button to appear at all, you must publish a `connect` string:

```csharp
SteamFriends.SetRichPresence( "connect", $"+connect_lobby {lobby.Id}" );

// and clear it when you leave
SteamFriends.ClearRichPresence();
```

One gotcha: `SteamFriends.GetRichPresence( key )` for **yourself** reads a process-local
dictionary populated only by your own successful `SetRichPresence` calls — it never queries
Steam, and returns `null` for anything you did not set this session. To read *another*
player's rich presence, use the instance method on `Friend`, which does go to Steam.

---

## 10. Handing off to the game

The lobby's job ends when everyone is ready. Two shapes:

**Peer-to-peer / listen server** — the owner's `SteamId` is the address:

```csharp
// Owner:
lobby.SetData( "host", SteamClient.SteamId.ToString() );

// Everyone else, on OnLobbyDataChanged:
if ( ulong.TryParse( lobby.GetData( "host" ), out var host ) )
    ConnectTo( host );          // see the networking guide
```

**Dedicated server** — use the purpose-built call, which Steam understands:

```csharp
lobby.SetGameServer( "203.0.113.10", 28015 );   // or SetGameServer( serverSteamId )
```

Every member then receives `OnLobbyGameCreated( lobby, ip, port, serverId )` and connects.
Using `SetGameServer` rather than a hand-rolled data key means the Steam overlay and friends
list also understand where the player is.

`SetGameServer` throws `ArgumentException` for an unparseable address or an invalid
`SteamId`, so validate before calling it.

For choosing *which* transport to connect with, and the tradeoffs between them, see
[Networking transports](04-networking-transports.md).

---

## What this library does not expose

- **`SetLinkedLobby`** — linking two lobbies, for e.g. a clan lobby plus a match lobby.
- **`AddRequestLobbyListCompatibleMembersFilter`** — filtering by members compatible with a
  given user.
- **A string not-equal or substring lobby filter.** The source carries a TODO for
  `WithoutKeyValue`; it does not exist yet.

All three are catalogued in [audit 03](../audit/03-api-coverage-gaps.md).

---

## Where to go next

- [Networking transports](04-networking-transports.md) — what to actually connect with once
  the lobby has done its job.
- [Ping locations](../sdr/ping-locations-and-certificates.md) — latency-aware matchmaking
  without pinging anything.
- [Getting Started](01-getting-started.md) — if invites are not arriving at all, the cause is
  usually that callbacks are not being pumped.
