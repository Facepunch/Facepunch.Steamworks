# Facepunch.Steamworks — AnkleBreaker Studio fork

A C# binding for the Steamworks SDK, with an emphasis on making things easy: real C# types,
`await` instead of callback registration, and a single managed DLL with no third-party
native dependency.

This is [AnkleBreaker Studio](https://github.com/AnkleBreaker-Studio)'s fork of
[Facepunch/Facepunch.Steamworks](https://github.com/Facepunch/Facepunch.Steamworks).
It started life as a set of dedicated-server fixes and has since grown a good deal further.

**New here? Read [Getting Started](docs/guides/01-getting-started.md) rather than this
file.** It explains the IPC model, why `Init` throwing is normal, and the `steam_appid.txt`
traps that catch everyone once. This README is the overview and the honest inventory.

---

## Why this fork exists, and how it differs

Upstream is a good library and this fork does not diverge from its design. What it does is
fix things that only show up when you actually ship a dedicated server, and then keep going.

| | Upstream | This fork |
|---|---|---|
| **Dedicated servers** | Supported, lightly exercised | The reason the fork exists. Auth-ticket path on the server, `QueryEndReason` from server-browser queries, a full audit of interface routing ([04](docs/audit/04-gameserver.md)) |
| **Steam Datagram Relay** | Relay sockets only | The complete public surface: poll groups, hosted dedicated servers, ping locations, POPs, certificates, FakeIP |
| **PS5 DualSense adaptive triggers** | Not bound — the generator cannot emit Sony's C `union` | Hand-written binding with a typed effect API |
| **Steam Deck floating keyboard** | Bound but unreachable (the wrapper was commented out) | Exposed, along with both text-input dismiss calls |
| **Native binaries** | Per-platform folders were ~11 SDK releases stale, including *every* Linux and macOS library | All 17 refreshed and checked on every build |
| **Struct layout** | 16 generated structs mis-laid-out; `Marshal.PtrToStructure` read past the end of Steam's callback buffer on one live path | Fixed at the generator, and pinned by a CI-enforced baseline |
| **Allocation** | Per-callback and per-message garbage on the hot paths | Measured and largely eliminated — see [Performance](#performance) |
| **Offline verification** | A build | `verify.ps1`: build + native export conformance + struct-layout baseline, none of which needs Steam |
| **Documentation** | A README and a community wiki | [Task-oriented guides](docs/README.md) and six evidence-backed [audit reports](docs/audit/README.md) |

Everything upstream does, this still does. The public API is source-compatible except where
[a defect fix changed behaviour](#behaviour-changes-vs-upstream) — those are listed, not
hidden.

---

## Features

| | State |
|---|---|
| Windows x64 / x86 | Supported |
| Linux x64 / x86 | Supported — but see [known limitations](#known-limitations--open-issues) about packaging the `.so` |
| macOS | Supported |
| Unity | Supported (`net46` build, `UnityPlugin/`) |
| Unity IL2CPP | Supported. **Note:** the performance numbers below are CoreCLR; IL2CPP is not re-measured |
| Target frameworks | `net46`, `netstandard2.1`, `net6.0` |
| Async call results (`await`) | Yes |
| Events (Steam callbacks) | Yes |
| Single managed DLL, no third-party native library | Yes — you ship Valve's `steam_api` and nothing else |
| Client (`ISteamUser`, friends, apps, utils, screenshots, music, parental, remote play, video) | Yes |
| Dedicated server (`ISteamGameServer`, `ISteamGameServerStats`) | Yes |
| Server browser (`ISteamMatchmakingServers`) | Yes — client-side only, which is correct |
| Lobbies & matchmaking | Yes |
| Achievements, stats, leaderboards | Yes |
| Workshop / UGC (query, subscribe, publish) | Yes |
| Steam Cloud | Yes |
| Steam Inventory | Yes |
| Voice | Yes |
| Steam Input, incl. **PS5 DualSense adaptive triggers** | Yes |
| **Steam Deck floating keyboard** | Yes |
| Steam Timeline | Yes |
| Parties, Remote Play | Yes |
| Networking: `ISteamNetworkingSockets` | Yes |
| Networking: `ISteamNetworkingMessages` | Yes |
| Networking: legacy `ISteamNetworking` P2P | Yes — Valve has superseded it; prefer the two above |
| **Steam Datagram Relay** — relay sockets, poll groups, hosted dedicated servers, ping locations, POPs, certificates, FakeIP | Yes |
| `ISteamHTTP` | **Not exposed.** The P/Invoke layer is generated, but there is no managed facade |
| `ISteamAppList` | **Removed.** Valve deleted the interface; the bindings threw `EntryPointNotFoundException` |
| MIT licensed | Yes |

**Coverage, measured rather than claimed:** 1,019 P/Invoke entry points, 100% of which
resolve against all 17 committed native binaries. 906 of 946 flat-API functions are bound
(95.8%); of the 40 unbound, 38 are legacy surface Valve itself superseded. 52.9% of bound
internal methods are reachable from the public API — the gap is catalogued per interface in
[audit 03](docs/audit/03-api-coverage-gaps.md).

---

## Quick start

### Client

```csharp
using Steamworks;

const uint AppId = 480; // 480 = Spacewar, Valve's public test app. Replace with yours.

try
{
    SteamClient.Init( AppId, asyncCallbacks: false );
}
catch ( Exception e )
{
    // Steam isn't running, the player doesn't own the app, steam_appid.txt is wrong,
    // or the native library couldn't be loaded. Your game must still be able to start.
    Console.WriteLine( $"Steam unavailable: {e.Message}" );
    RunWithoutSteam();
    return;
}

Console.WriteLine( $"Logged in as {SteamClient.Name} ({SteamClient.SteamId})" );

// ... every frame, from your main loop ...
SteamClient.RunCallbacks();

// ... when you close ...
SteamClient.Shutdown();
```

**Three things this snippet is deliberately doing:**

1. **`Init` is inside a `try`.** It throws a plain `System.Exception` when Steam is not
   running, and Steam not running is not exceptional — it is Tuesday. Treating `Init` as
   infallible is the single most common shipping bug in a Steam integration. Decide up
   front whether your game continues without Steam, exits cleanly, or relaunches through
   Steam (`SteamClient.RestartAppIfNecessary`), and never carry on as if `Init` succeeded.

2. **`asyncCallbacks: false`, plus `RunCallbacks()` in your loop.** The default (`true`)
   starts a background pump, and your callbacks and `await` continuations then resume on a
   *background thread*. In a game engine, touching engine objects off the main thread is
   undefined behaviour that usually crashes somewhere else, later. Pass `false` in a game
   and pump it yourself; leave the default for servers, tools and console apps.

   If you pass `false` and forget `RunCallbacks()`, nothing breaks loudly — your `await`s
   just never complete. A hang with no exception is the signature of this mistake.

3. **`steam_appid.txt`.** During development, put a file called `steam_appid.txt` next to
   your *executable* containing only the AppID — no newline, no BOM, no quotes. Without it
   Steam cannot tell which app your process is. In a shipped build you normally **delete**
   it and let Steam tell your process what it is at launch; if you ship it, any player can
   edit it and claim to be a different app.

   The traps: the file must sit beside the built executable, not the project file; some
   editors silently add a BOM or trailing newline; and the account you are signed in as
   must own that AppID. Use 480 while prototyping — every account can use it.

### Dedicated server

A dedicated server is a different entry point. It does not log in as a user.

```csharp
var init = new SteamServerInit( "mygame", "My Game" )
{
    GamePort = 28015,
    QueryPort = 28016,
    Secure = true,          // enable VAC
    VersionString = "1.0.0"
};

try
{
    SteamServer.Init( AppId, init );
}
catch ( Exception e )
{
    Console.WriteLine( $"Server init failed: {e.Message}" );
    return;
}

SteamServer.ServerName = "My Server";
SteamServer.MapName    = "de_dust";
SteamServer.LogOnAnonymous();      // Init does NOT do this for you.

// ... every tick ...
SteamServer.RunCallbacks();
```

Two traps in that snippet:

- **`Init` does not log you on.** It sets up the interfaces and nothing else. Without an
  explicit `LogOnAnonymous()` (or `LogOn(token)` with a Game Server Login Token) the server
  runs, accepts nothing, and never appears anywhere. `Init` also leaves `ServerName`,
  `MapName` and `GameTags` unset.
- **`GamePort` *and* `QueryPort` must both be reachable from the internet.** `QueryPort` is
  what the Steam server browser talks to — with only `GamePort` open your server runs
  correctly and never appears in the browser, which is a miserable thing to debug.

Full walkthrough: [Getting Started](docs/guides/01-getting-started.md) and
[Dedicated Servers](docs/guides/06-dedicated-servers.md).

---

## What this fork adds

### Steam Datagram Relay — the complete surface

SDR is Valve's private overlay for game traffic. Players connect to a nearby Valve relay
instead of to each other, so neither end learns the other's IP address (nothing to DDoS),
and traffic crosses Valve's backbone, which frequently beats public internet routing.
Upstream exposed relay sockets. This fork exposes the rest.

**Poll groups** — drain many connections in one native call instead of one call per
connection. At 100 players and 60 Hz, per-connection polling is 6,000 managed→native
transitions per second, most of which return nothing. A poll group is 60, regardless of
player count.

```csharp
var group = SteamNetworkingSockets.CreatePollGroup();
group.Add( connection );

// Hoist the delegate into a field — a lambda written at the call site
// allocates a closure every tick and throws away the point of the API.
int processed = group.Receive( OnMessage );

group.Destroy();
```

**Hosted dedicated servers**, ticketed and ticketless. A ticketed server accepts only
clients holding a ticket your backend issued, and a cached ticket lets a client reconnect
*even while disconnected from Steam*, because the ticket is already on disk.

```csharp
ushort port  = SteamNetworkingSockets.HostedDedicatedServerPort;
NetPOPID pop = SteamNetworkingSockets.HostedDedicatedServerPopId;

var address = SteamNetworkingSockets.GetHostedDedicatedServerAddress();
var login   = SteamNetworkingSockets.GetGameCoordinatorServerLogin();

var socket = SteamNetworkingSockets.CreateHostedDedicatedServerSocket<SocketManager>();
```

These route through the **game server** interface, not the shared one — Valve's header is
explicit that they must, and on a listen server the shared accessor would silently pick the
user interface and answer the wrong question. They throw with a message naming the actual
cause (`SDR_LISTEN_PORT` unset; no cached auth ticket) rather than handing back an invalid
handle, and they surface Valve's own diagnostic string, which is otherwise unobtainable.

**Ping locations and POPs** — estimate latency between two hosts without sending a packet,
so matchmaking does not need `O(n²)` round trips. Failure is a *negative return*, not an
exception (`-1` failed, `-2` unknown); sort without filtering those and every unmeasured
candidate looks like the best match.

**Certificates** — for a process that cannot get one from Steam, such as an anonymous
dedicated server with no Steam account.

Both SDR documents are worth reading in full before using any of this:
[poll groups & hosted servers](docs/sdr/poll-groups-and-hosted-servers.md),
[ping locations & certificates](docs/sdr/ping-locations-and-certificates.md). They record
which behaviours were inferred from the headers rather than documented by Valve.

### PS5 DualSense adaptive triggers

Motorised trigger resistance — the thing that makes a trigger feel like a gun, a bowstring,
or a stiff brake pedal. Valve exports `SetDualSenseTriggerEffect` and the SDK declares it,
but its parameter is a C `union`, which the code generator cannot emit, so no C# Steamworks
binding exposed it. This one hand-writes the binding and wraps it in a typed API.

```csharp
foreach ( var controller in SteamInput.Controllers )
{
    SteamInput.SetDualSenseTriggerEffect(
        controller,
        DualSenseTriggerEffect.Weapon( startPosition: 2, endPosition: 7, strength: 8 ),
        DualSenseTrigger.Right );
}
```

Effects are built with factory methods — `Off`, `Feedback`, `Weapon`, `Vibration`,
`MultiplePositionFeedback`, `SlopeFeedback`, `MultiplePositionVibration` — which validate
their arguments instead of packing an out-of-range byte into the native struct.
`SetDualSenseTriggerEffects` sets the two triggers differently in one call.

**Two things to know.** An effect is a *state*, not a one-shot: it stays applied until you
replace it or send `Off`, so clear it when the player holsters the weapon or the trigger
stays stiff in the menu. And it **fails silently by design** — on anything that is not a
DualSense the call does nothing, and the native API returns no status, so there is no way
to detect that it was ignored.

### Steam Deck floating keyboard

The on-screen keyboard that floats over your game instead of replacing the screen with the
Steam overlay. The wrapper existed in upstream, commented out, with the wrong return type.

```csharp
SteamUtils.ShowFloatingGamepadTextInput(
    TextInputMode.SingleLine, fieldLeft, fieldTop, fieldWidth, fieldHeight );

// ... when your field loses focus ...
SteamUtils.DismissFloatingGamepadTextInput();
```

This works differently from `ShowGamepadTextInput`, and the difference catches people out:
the full-screen version takes the text from the player and hands it back through a callback,
whereas the floating keyboard types into *your* field as if it were a hardware keyboard, and
you position it yourself so it does not cover the field. `DismissGamepadTextInput` is the
counterpart to the full-screen one; `DismissFloatingGamepadTextInput` to this one.

---

## Performance

These are measured numbers from harnesses on real hardware, not estimates. The full method,
the harness bug that was found and corrected mid-audit, and every number is in
[audit 06](docs/audit/06-performance.md). Runtime: .NET 6.0.36, x64, workstation GC,
Release.

| Path | Before | After |
|---|---|---|
| `SteamUGCDetails_t` marshal (one workshop item) | **19,616 B/op** | **0 B/op** |
| Dedicated-server receive loop, 100 players @ 20 msg/player/s | **~460 KB/s of garbage** | **zero** |
| Seven list enumerators (friends, lobby members, achievements, DLC, cloud files, …) | `2N+1` interop transitions | `N+1` |

Why those numbers were what they were:

- **`SteamUGCDetails_t`** carries five `[MarshalAs(ByValArray)] byte[]` fields totalling
  9,670 bytes — `Description` alone is 8,000. Five fresh arrays were allocated on *every*
  marshal regardless of content, and because `GetQueryUGCResult` takes the struct by `ref`,
  a non-blittable `ref` marshals `[In,Out]`, so it happened in both directions. A 50-item
  workshop page cost roughly half a megabyte in those arrays alone. The fix converts them to
  `fixed byte` buffers decoded on demand, which is the pattern the generator already used
  elsewhere. 51 `ByValArray byte[]` fields across the assembly got the same treatment.

- **The dedicated-server receive loop.** `SocketManager.Receive` was the un-optimised twin
  of `ConnectionManager.Receive`: a `Marshal.AllocHGlobal`/`FreeHGlobal` pair per call
  (58 ns), a `Marshal.PtrToStructure<NetMsg>` per message (232 B + 84 ns), and recursion
  rather than a loop to drain. The client path had already been made zero-allocation; the
  server path — this fork's flagship area — had not. It now mirrors it exactly:
  `stackalloc`, raw `NetMsg*`, a `while` loop, and release-on-throw so a throwing handler no
  longer leaks the rest of the batch back to Steam.

- **`Marshal.PtrToStructure<T>` does not avoid boxing.** This overturned a fix made earlier
  in the same session. Both the generic and the `Type` overload allocate exactly
  `sizeof(T) + 16` bytes on CoreCLR — the generic one creates an `object`, marshals into it
  and unboxes on return — so swapping between them changes nothing, and the generic form is
  *slower* for small structs. Callback delivery now uses a plain pointer dereference behind
  an `unmanaged` constraint, which the compiler checks rather than trusts: it refuses any
  type carrying a managed reference, which is exactly the set that genuinely needs
  marshalling. Zero allocation per delivered callback, for the 222 of 440 value types that
  qualify.

Also verified and genuinely fine, so nobody re-litigates it: the dispatch pump's own
bookkeeping allocates **0 bytes** per frame; string marshalling is **at the theoretical
floor** (a returned string costs exactly the string and nothing more); and the `Ugc.Query`
builder chain allocates **0 bytes** despite being a 192-byte struct returned by value at
each step.

**One honest caveat.** Every number above is CoreCLR. Under IL2CPP/Mono some of these are
expected to be *worse* and have not been re-measured on device — in particular
`Enum.HasFlag` (Mono has no JIT expansion) and enum-keyed `Dictionary` lookups, which on
older Mono fall back to a boxing comparer and would put an allocation on the callback hot
path. That is listed as the single most important thing to verify on device.

---

## Verifying the library without Steam

Steam does not need to be installed to check a great deal of this library's correctness.
**Run this before every commit:**

```
powershell -ExecutionPolicy Bypass -File verify.ps1
```

It builds every target framework of every platform project, then runs two gates. Exit code 0
means everything passed.

**Local is the primary gate; CI is the backstop.** CI runs the same scripts, but if
`verify.ps1` passes on your machine the change is good, and if it fails, CI will not save
you. Nothing here needs a Steam client, a Steam account, a login, or a network connection —
which is the only reason it actually gets run.

### Gate 1 — native export conformance

```
powershell -ExecutionPolicy Bypass -File verify-native-conformance.ps1
```

**What it proves:** every `DllImport` entry point in the managed code exists in all 17
committed native binaries, across Windows x86/x64, Linux x86/x64 and macOS.

**Why that matters:** entry points are plain strings the C# compiler never checks. If a name
is stale or refers to a function Valve has since removed, the code compiles perfectly and
throws `EntryPointNotFoundException` the first time a player's machine calls it. This is
exactly how the six dead `ISteamAppList` bindings were found — and how the eight stale native
binaries were found, one of which would have stopped a Linux dedicated server from
initialising at all.

### Gate 2 — struct layout baseline

```
powershell -ExecutionPolicy Bypass -File verify-struct-layout.ps1
```

**What it proves:** the marshalled size, pack and every field offset of all 343 ABI structs
still match `Tools/baselines/layout-win64.txt`.

**Why that matters:** Steam writes callback structs directly into memory that managed code
reads back with `Marshal.PtrToStructure`. If a managed layout disagrees with the C++ one,
*nothing throws* — you silently read the wrong fields, or read past the end of Steam's
buffer. It is the worst failure mode in this codebase: invisible, non-deterministic, and
nearly impossible to attribute afterwards. The layouts are produced by the code generator,
where one small change can move dozens of structs at once, so the baseline turns that into
an explicit, reviewable diff.

Pass `-Record` to deliberately accept an intended layout change, and commit the new baseline
alongside the change that caused it.

**What the baseline does *not* prove:** it pins what the layout *is*, not what it *should
be*. It catches drift, but would happily accept a wrong layout that was recorded
deliberately. The proof that the current layouts are correct comes from a header-derived
MSVC-x64 layout model diffed against `Marshal.SizeOf`/`OffsetOf`, which still lives outside
this repository — so that argument is reproducible by hand but not yet by CI.

Runtime behaviour against a live Steam client is a separate thing: see
`run-live-steam-tests.ps1`, which needs Steam running and signed in.

---

## Which native binary do I ship?

This library is pure C#, but it P/Invokes into Valve's native `steam_api` library, which you
must ship alongside your game.

| Target | File | Comes from |
|---|---|---|
| Windows x64 | `steam_api64.dll` | `Facepunch.Steamworks/steam_api64.dll` |
| Windows x86 | `steam_api.dll` | `Facepunch.Steamworks/steam_api.dll` |
| Linux x64 | `libsteam_api.so` | `Facepunch.Steamworks/linux64/libsteam_api.so` |
| Linux x86 | `libsteam_api.so` | `Facepunch.Steamworks/linux32/libsteam_api.so` |
| macOS | `libsteam_api.dylib` | `Facepunch.Steamworks/osx/libsteam_api.dylib` |

The binary must sit **beside the executable**, and must match the version this library was
built against. A mismatch does not fail gracefully — you get `EntryPointNotFoundException`
at the first call using a function the older binary lacks, potentially deep into a play
session.

Every binary in this repository is checked against the library's P/Invoke declarations by
`verify-native-conformance.ps1` on every build, so the copies above are known good. If you
source a `steam_api` binary from anywhere else, run that script against it before shipping.

**Linux and macOS need manual staging today.** The two Linux `.so` files above are genuine,
current and verified — but the build does not copy them to the output directory. See the
next section.

---

## Known limitations & open issues

A fork that documents its own defects is easier to trust than one that does not. These are
the findings the [audit](docs/audit/README.md) has verified and *not yet fixed*, ranked by
severity. Each links to the report that reproduces it.

**Will bite you in production**

1. **`MatchMakingKeyValuePair` and 20 `const char*` callback fields decode as ANSI, not
   UTF-8.** Server-browser filters marshal as ANSI, so `"café"` goes out as `63 61 66 E9`
   and `"日本語"` becomes three literal `?` — unrecoverably. Among the 20 callback fields
   typed as raw `string` is `HTML_NeedsPaint_t.PBGRA`, which is a BGRA framebuffer pointer
   being scanned for a NUL terminator. ([01](docs/audit/01-marshaling-abi.md))
2. **Any `SteamApps.*` call on a *pure* dedicated server is an access violation, not an
   exception.** `SteamApps` is registered server-side, but `ISteamApps` has no game-server
   accessor, so `Self` stays `IntPtr.Zero` and `SteamServer.AddInterface<T>` ignores the
   failure (unlike `SteamClient`'s). Masked on a listen server, fatal on a dedicated one.
   ([04](docs/audit/04-gameserver.md))
3. **Shutdown races the async callback pump.** `Dispatch.LoopClientAsync` is `async void`
   with no cancellation or join, so an in-flight frame can call into a destroyed pipe —
   crash-on-exit for headless servers. ([02](docs/audit/02-dispatch-memory-threading.md))
4. **Pending call results are silently abandoned on shutdown.** Proven: 5,000 registered
   continuations, 0 invoked. Every awaiting `Task` hangs forever.
   ([02](docs/audit/02-dispatch-memory-threading.md))
5. **`Dispatch.runningFrame` is a non-volatile check-then-set.** 872,129 simultaneous
   entries measured in a 2M-iteration race. Concurrent `FreeLastCallback` is something Valve
   explicitly forbids. ([02](docs/audit/02-dispatch-memory-threading.md))

**Server-side gaps**

6. **Server auth tickets cannot be cancelled.** `AuthTicket.Cancel()` hard-codes
   `SteamUser.Internal`, which is never registered server-side, so `Dispose()` throws.
   ([04](docs/audit/04-gameserver.md))
7. **`SteamServer.Shutdown()` does not reset cached statics**, so a second `Init` in the same
   process silently skips `ModDir` / `GameDescription` / `MaxPlayers` — properties the SDK
   requires before `LogOn`. The server then lists incorrectly, with no error anywhere.
   ([04](docs/audit/04-gameserver.md))
8. **The Posix build produces no deployable Linux artifact.** The project has no item group
   copying `libsteam_api.so`, and shares `bin/` with the Windows projects. Copy the `.so`
   yourself when staging a Linux server. ([04](docs/audit/04-gameserver.md))
9. **Shared interfaces resolve client-first on a *listen* server.**
   `SteamSharedClass<T>.Interface` is `InterfaceClient ?? InterfaceServer`, so with both a
   client and a server initialised in one process, `SteamNetworkingSockets`, `SteamUtils`,
   `SteamUGC`, `SteamInventory` and `SteamNetworkingMessages` operate on the *user's*
   interface. No error is raised; the behaviour is just wrong. The SDR hosted-server calls
   added by this fork deliberately bypass this. ([04](docs/audit/04-gameserver.md))

**Smaller, but real**

10. **Callback exceptions are swallowed** and permanently drop the remaining handlers for
    that callback. ([02](docs/audit/02-dispatch-memory-threading.md))
11. **`SteamInputActionEvent_t` is 16 bytes against a native 33** — the generator drops its
    `union`. Unreachable today (`EnableActionEventCallbacks` is filtered out as deprecated),
    but it is a compiled-in landmine. ([01](docs/audit/01-marshaling-abi.md))
12. **`Achievement.GlobalUnlocked` always returns `-1`** — its prerequisite call is never
    made. ([03](docs/audit/03-api-coverage-gaps.md))
13. **`SteamNetworkingSockets.CreateFakeUDPPort` returns an unusable handle** — the backing
    class is empty and its `Self` is permanently zero.
    ([03](docs/audit/03-api-coverage-gaps.md))
14. **`GSStatsUnloaded_t` is unreachable** — a genuine upstream callback-ID collision at 1108
    that the generator resolves inconsistently with the identical one at 1112.
    ([05](docs/audit/05-tests-and-docs.md))

**Test and documentation coverage, stated plainly**

- **118 of 118 tests fail offline**, all on one `[AssemblyInitialize]`, in 227 ms.
  Separately, **53 of the 118 contain no assertions at all.** The offline gates in
  `verify.ps1` exist because the test suite cannot currently carry that weight.
- **36.2% of the public API has any XML documentation**, `<param>` sits at 11.8%, and there
  is **not one `<example>` or `<remarks>` in the library**. For a P/Invoke binding, the
  arguments are exactly where the danger is. Documentation is one of this fork's two stated
  priorities and this number is the reason.

### Behaviour changes vs upstream

If you are migrating from upstream, these change what your existing code observes:

- **`OnMessage` received `messageNum` and `recvTime` transposed** on both `SocketManager`
  and `ConnectionManager` — every consumer got a microsecond timestamp as the message number
  and a sequence counter as the receive time. **This is now fixed, so anyone who compensated
  for the swap in their own handler must remove that compensation.**
- **`ServerList.Base.RunQueryAsync` returns `Task<QueryEndReason>`, not `Task<bool>`** — you
  can now distinguish a timeout from a cancellation from an invalid client. Source-breaking.
- **`SteamUserStats.RequestCurrentStats()` is an obsolete no-op.** Valve deleted the native
  function. Stats now arrive automatically at startup; see
  [the achievements guide](docs/guides/02-achievements-and-stats.md).

---

## Documentation

Start at [docs/README.md](docs/README.md). The short version:

- **[Guides](docs/README.md#guides)** — task-oriented, written for someone who has not
  shipped a Steam game before. Getting started, achievements & stats, lobbies &
  matchmaking, choosing a networking transport, workshop & cloud, dedicated servers.
- **[SDR notes](docs/README.md#steam-datagram-relay-sdr)** — what was added and why each
  design decision went the way it did.
- **[Audit reports](docs/audit/README.md)** — six evidence-backed workstreams. Every finding
  cites a header quote, a measured number, or a reproduction; suspicions that did not
  survive verification are recorded as such rather than quietly dropped.

Where the docs assume rather than know, they say so. Valve leaves 30.9% of SDK methods with
no comment at all, and 91.7% of functions returning `const char*` document no pointer
lifetime, so silence in Valve's headers is treated as a reason to state our assumption, not
to stay quiet.

---

## Help & contributing

Pull requests, bug reports — yes, please. Run `verify.ps1` before you send one; it is the
gate, and it needs nothing but a checkout and the .NET SDK.

For upstream's community resources, the [Steamworks
wiki](https://wiki.facepunch.com/steamworks/) and the [Steamworks
thread](http://steamcommunity.com/groups/steamworks/discussions/0/1319961618833314524/) are
still the best places for general Steamworks discussion.

---

## License

MIT — do whatever you want.

This is a fork of [Facepunch.Steamworks](https://github.com/Facepunch/Facepunch.Steamworks),
copyright © 2016 Facepunch Studios LTD, created by Garry Newman and contributors, and used
here under the MIT licence. The original licence text is preserved verbatim in
[LICENSE](LICENSE). The overwhelming majority of this library is still their work; the fork
adds to it rather than replacing it.

Steamworks itself is © Valve Corporation. The `steam_api` binaries in this repository are
Valve's redistributables and are subject to the [Steamworks
SDK](https://partner.steamgames.com/doc/sdk) terms, not the MIT licence above.
