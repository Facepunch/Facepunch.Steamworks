# Poll groups and SDR hosted dedicated servers

This note covers what was added to expose two Steam Datagram Relay capabilities the binding
could not reach before, and — more usefully — *why* each design decision went the way it did.
Valve's documentation for both features is thin, so a fair amount of this is reasoning from
the headers rather than restating them.

---

## 1. Poll groups

### What the feature is

A poll group is a set of connections whose inbound messages are merged into one queue. You
drain the whole set with a single `ReceiveMessagesOnPollGroup` call instead of one
`ReceiveMessagesOnConnection` call per connection.

The header is unusually terse about it:

> Poll groups. A poll group is a set of connections that can be polled efficiently.
> (In our API, to "poll" a connection means to retrieve all pending messages. We
> actually don't have an API to "poll" the connection *state*, like BSD sockets.)

That undersells it. This is the difference between a server whose receive cost scales with
**player count** and one whose receive cost scales with **message count**. At 100 players and
60 Hz, per-connection polling is 6,000 managed→native transitions per second, and the large
majority of them return zero messages. With a poll group it is 60 calls per second no matter
how many players are connected.

### What was already there

`SocketManager` has always created a poll group internally (`SocketManager.Initialize`) and
added every accepted connection to it (`SocketManager.OnConnected`). So the single most
common path already benefited. What was missing was any way to:

- create a poll group of your own,
- partition connections across several groups (per match, per room, lobby traffic vs.
  gameplay traffic, trusted vs. untrusted),
- poll connections that were never created by a `SocketManager` — e.g. a server that also
  holds outbound `ConnectRelay` connections to a backend, or a custom transport built
  directly on `Connection`.

### API shape

```
SteamNetworkingSockets.CreatePollGroup()  ->  PollGroup

PollGroup.Add( Connection )
PollGroup.Remove( Connection )
PollGroup.Receive( PollGroupMessage onMessage, int bufferSize = 32, bool receiveToEnd = true )
PollGroup.Receive( Action<Connection, NetIdentity, byte[]> onMessage, ... )   // convenience
PollGroup.Destroy()

Connection.SetPollGroup( PollGroup )      // same operation, spelled from the other side
SocketManager.PollGroup                   // read-only access to the manager's own group
```

#### Why `PollGroup` is a struct handle and not `IDisposable`

`HSteamNetPollGroup` is a `uint`. `Socket` and `Connection` in this binding are already
`uint`-sized structs with methods hanging off them (`Socket.Close()`, `Connection.Accept()`),
and matching that was more valuable than inventing a second idiom.

`IDisposable` was considered and rejected. A struct handle gets copied freely — into a list,
into a lambda, out of a property getter — and every copy would carry a `Dispose` that destroys
the *shared* underlying group. `using var group = ...` would look correct and be a
double-destroy waiting to happen. Explicit `Destroy()` makes ownership something you have to
state rather than something the compiler infers wrongly. `Socket.Close()` sets the same
precedent.

#### Why the receive path looks the way it does

`SteamNetworkingMessages.ReceiveMessagesOnChannel` had already established a zero-allocation
receive idiom in this fork, and `PollGroup.Receive` deliberately mirrors it line for line:

```csharp
NetMsg** messageBuffer = stackalloc NetMsg*[bufferSize];

while ( true )
{
    int processed = Internal.ReceiveMessagesOnPollGroup( Id, new IntPtr( &messageBuffer[0] ), bufferSize );
    ...
}
```

The properties that matter:

| Concern | How it is handled |
|---|---|
| Message-pointer array | `stackalloc`, hoisted out of the loop. No heap traffic, no `Marshal.AllocHGlobal`. |
| Payload | Handed over as `IntPtr` + `int`. No `byte[]` is created. |
| Message struct | Read through `NetMsg*` directly. No `Marshal.PtrToStructure`, which copies ~72 bytes per message. |
| Draining | `while` loop, not recursion. |
| Leaks on throw | If the handler throws, every message from that native call is `Release()`d before the exception propagates. |

`bufferSize` is capped at 256 (`PollGroup.MaxReceiveBufferSize`) so the `stackalloc` cannot
exceed 2 KB on a 64-bit runtime. The same cap and the same default of 32 are used by
`SteamNetworkingMessages`.

**Contrast with `SocketManager.Receive`**, which was left alone deliberately (see §4): it
`Marshal.AllocHGlobal`s a pointer array *every tick*, `Marshal.PtrToStructure<NetMsg>`s *every
message*, and recurses to drain. New code should prefer `PollGroup.Receive`.

#### The one thing callers still have to get right

`PollGroupMessage` is a delegate. Writing the handler as a lambda **at the call site** that
captures locals allocates a closure every tick and throws away the point of the method. The
XML docs say this explicitly: hoist the delegate into a cached field. There is no way to
enforce it in the type system without giving up the ergonomics entirely.

#### Allocating convenience overload

`Receive( Action<Connection, NetIdentity, byte[]> )` exists and is documented as
"CONVENIENCE ONLY — allocates one array per message". Prototypes and tests want it; server
ticks must not use it. This follows the precedent already set by
`SteamNetworkingMessages.ReceiveMessagesOnChannel`, which ships both shapes side by side.

#### Negative return values

The header documents that `ReceiveMessagesOnConnection` returns `-1` for an invalid handle,
but says **nothing** about `ReceiveMessagesOnPollGroup`'s return value. We assume the same
convention and convert any negative return into an `InvalidOperationException`. Without that,
a destroyed-then-reused handle would silently show up as an empty tick, and `totalProcessed`
would go backwards. This assumption is called out in the XML docs rather than presented as
fact.

---

## 2. SDR hosted dedicated servers

### What the feature is

Your dedicated server runs inside a Valve data center. Players connect to Valve's relays and
the relays forward traffic to you over a private path. Players never learn the server's
address, so a player cannot DDoS the server they just joined. Traffic also rides Valve's
backbone between relays, which is frequently faster than the public route.

There are two flavours and the binding now exposes both:

| | Listen | Connect | Who may join |
|---|---|---|---|
| **Ticketless** | `CreateRelaySocket` (`CreateListenSocketP2P`) | `ConnectRelay` (`ConnectP2P`) | anyone who owns the app and is signed into Steam |
| **Ticketed** | `CreateHostedDedicatedServerSocket` | `ConnectToHostedDedicatedServer` | only holders of a ticket your backend issued |

Ticketless already worked. Ticketed did not exist at all — that is what was added.

Valve's stated reason to accept the extra complexity of tickets: a cached ticket lets a client
reconnect to your server **even if the client has been disconnected from Steam**, because the
ticket is already on disk. Tickets are stored in a cache rather than passed as an argument
specifically so that reconnection survives a crash or restart.

### API shape

```
SteamNetworkingSockets.HostedDedicatedServerPort         -> ushort
SteamNetworkingSockets.HostedDedicatedServerPopId        -> NetPOPID
SteamNetworkingSockets.GetHostedDedicatedServerAddress() -> HostedServerAddress
SteamNetworkingSockets.GetGameCoordinatorServerLogin( byte[] appData = null )
                                                         -> GameCoordinatorServerLogin
SteamNetworkingSockets.CreateHostedDedicatedServerSocket<T>( int virtualPort = 0 )
SteamNetworkingSockets.CreateHostedDedicatedServerSocket( int virtualPort, ISocketManager )
SteamNetworkingSockets.ConnectToHostedDedicatedServer<T>( NetIdentity, int virtualPort = 0 )
SteamNetworkingSockets.ConnectToHostedDedicatedServer( NetIdentity, int virtualPort, IConnectionManager )
```

### Design decisions

#### Routing these calls through the **game server** interface

`SteamSharedClass.Interface` resolves to `InterfaceClient ?? InterfaceServer`. That is the
right default almost everywhere, and it is wrong here. The header is explicit:

> This call MUST be made through the SteamGameServerNetworkingSockets() interface.
> — `CreateHostedDedicatedServerListenSocket`

In a pure dedicated-server process there is no client interface, so the existing accessor
would have worked by accident. In a **listen-server** build (`SteamClient.Init` *and*
`SteamServer.Init` in one process) it would silently pick the user interface and answer the
wrong questions.

So `SteamNetworkingSockets.InternalServer` was added, and the server-side calls
(`CreateHostedDedicatedServerListenSocket`, `GetHostedDedicatedServerAddress`,
`GetGameCoordinatorServerLogin`) go through `RequireServerInterface()`, which throws a
`InvalidOperationException` naming `SteamServer.Init` if the game server interface does not
exist.

`GetHostedDedicatedServerPort` / `…POPID` only read an environment variable, so they *prefer*
the server interface and fall back to whatever is initialised. Being strict there would add
friction with no safety benefit.

#### Failing loudly when Steam returns an invalid handle

`CreateRelaySocket` and `ConnectRelay` currently return a manager wrapping an invalid handle
if Steam refuses. The new hosted-server creators throw instead, because the two dominant
failure modes are extremely specific and extremely easy to diagnose *if* you are told:

- listen socket fails → `SDR_LISTEN_PORT` is not set,
- connect fails → no relay auth ticket is cached for that server and virtual port.

The exception messages say exactly that. A silently invalid socket here costs a day; without
a Steam environment to test in, that is a day nobody in this project can afford. (`Connection`
handles are additionally checked before `SetConnectionManager`, which would otherwise throw
a bare `ArgumentException( "Invalid Connection" )` with no context.)

#### Surfacing Valve's diagnostic strings

Both failing calls hide a plain-text English explanation in an output buffer, which is easy
to miss and impossible to obtain any other way:

> A non-localized diagnostic debug message will be placed in `m_data` that describes the
> cause of the failure. — `GetHostedDedicatedServerAddress`

> A non-localized diagnostic debug message will be placed in `pBlob` that describes the cause
> of the failure. — `GetGameCoordinatorServerLogin`

`HostedServerAddress.DiagnosticMessage` and `GameCoordinatorServerLogin.DiagnosticMessage`
decode that buffer as NUL-terminated UTF-8 whenever `Result != OK`, and return `null` on
success (where the same bytes are binary routing data, not text). The decode is length-bounded
by the actual buffer size rather than scanning to the first NUL in unbounded memory.

#### The POP ID type is shared, not duplicated

`HostedDedicatedServerPopId` and `HostedServerAddress.PopId` are typed as
`Steamworks.Data.NetPOPID`, the POP-ID wrapper introduced alongside the
`SteamNetworkingUtils` ping/POP-list work. A second, near-identical POP-ID struct was written
here first and then deleted: two public types for the same 32-bit packed data-center code is
an API defect, and `NetPOPID` is the more complete implementation (it has non-throwing
`TryParse`, allocation-free `WriteCode` overloads, and span forms). Sharing it also means
`SteamNetworkingUtils.GetPOPList()` values compare directly against
`SteamNetworkingSockets.HostedDedicatedServerPopId`, which is exactly the question a hosted
server wants to ask.

#### `HostedServerAddress` does not wrap the generated struct

`SteamDatagramHostedAddress` is a generated struct with a `[MarshalAs(ByValArray)] byte[]`
field. Embedding it in a public type would mean every copy of the public value silently
aliases the same mutable array. `HostedServerAddress` is a `readonly struct` that keeps the
byte array plus `m_cbSize`, exposes `Length` / `CopyTo` / `ToArray`, and computes the POP ID
once at construction (by calling native `GetPopID`, and only when `Result == OK` — on failure
the buffer holds an error string, so parsing it as routing data would be meaningless).

#### `GameCoordinatorServerLogin` is a class, and allocates on purpose

This is the one place the "no allocation" rule is deliberately not applied. The call is a
once-per-process startup handshake: server boots, logs on, asks Steam for a signed blob, POSTs
it to your backend, backend starts issuing tickets. Valve requires a 4 KB buffer for the blob
and the struct carries a 2 KB app-data array. Contorting that into a caller-supplied-buffer
API would trade real readability for an allocation that happens once in a server's lifetime.

The result object carries `Result`, `Identity`, `Routing`, `AppId`, `Time`, `SignedBlob`
(trimmed to the actual length Steam reported, not padded to 4096) and `DiagnosticMessage`, so
one object answers both "did it work" and "why not". The doc comment says explicitly that the
allocation is intentional and why.

The blob length Steam reports back is range-checked against the buffer we handed it before
being used as a copy length. Trusting it blindly would be a buffer overrun driven by a value
from outside the managed heap.

#### `appData` is validated at the boundary

`GetGameCoordinatorServerLogin` throws `ArgumentException` if `appData` exceeds
`k_cbMaxSteamDatagramGameCoordinatorServerLoginAppData` (2048) rather than silently truncating
into a signed blob whose contents do not match what the caller passed.

#### ByValArray fields are pre-allocated before marshalling

`SteamDatagramHostedAddress.Data` (128 bytes) and
`SteamDatagramGameCoordinatorServerLogin.AppData` (2048 bytes) are marshalled as
`UnmanagedType.ByValArray`. A `ref` parameter marshals in *and* out, so both arrays are
allocated with their exact declared sizes before the call. Leaving them `null` is
inconsistently handled across runtimes and is not worth finding out about in production.

---

## 3. `GetRemoteFakeIPForConnection`

The last unexposed piece of the FakeIP surface. A FakeIP is a real-looking IPv4 address Valve
issues purely as an *identifier* — no packet is ever sent to it — so that existing code which
identifies peers by `IPAddress` (server browsers, ban lists, admin tools, logs) keeps working
on top of SDR.

```
SteamNetworkingSockets.GetRemoteFakeIPForConnection( Connection, out NetAddress ) -> Result
Connection.GetRemoteFakeIP( out NetAddress )                                      -> Result
```

The generated binding takes a `NetAddress[]`, so a naive wrapper would allocate a
single-element array on every lookup. A `[ThreadStatic]` one-element scratch array is used
instead: no allocation after the first call per thread, and safe if a game resolves FakeIPs
off more than one thread.

Two behaviours worth repeating from the header, because they are easy to get wrong:

- If the peer had a **global** FakeIP when the connection was established you get that one.
  Otherwise Steam mints a **local** one that only means anything inside this process. Valve
  says repeat connections to the same host will "probably" reuse the same local FakeIP but
  that the namespace is limited, so do not persist one and expect it to still identify the
  same peer later.
- The current range is `169.254.0.0/16` with a port above 1024, and Valve explicitly warns
  that this **will** change. Use `NetAddress.IsFakeIPv4`, never a range check.

---

## 4. Deliberate non-changes

**`SocketManager.Receive` was not rewritten**, even though it is measurably worse than
`PollGroup.Receive` (a native heap alloc/free per tick, a struct marshal per message,
unbounded recursion to drain, and no release-on-throw protection for messages already fetched
in the current batch). The reason is a pre-existing argument-order defect present in **both**
`SocketManager.ReceiveMessage` and `ConnectionManager.ReceiveMessage`:

```csharp
// call site
OnMessage( msg.Connection, msg.Identity, msg.DataPtr, msg.DataSize, msg.RecvTime, msg.MessageNumber, msg.Channel );
// signature
OnMessage( Connection connection, NetIdentity identity, IntPtr data, int size, long messageNum, long recvTime, int channel )
```

`RecvTime` and `MessageNumber` are passed to each other's parameters. Routing `SocketManager`
through the new code would silently change the values existing games receive in those two
arguments. That is a behaviour change that belongs in its own commit with its own release
note, not smuggled in behind a performance refactor.

The new `PollGroupMessage` delegate uses the **correct** order and does not replicate the
defect. That does mean a handler shaped like `ISocketManager.OnMessage` receives *correct*
values when driven by `PollGroup.Receive` and *swapped* ones when driven by `SocketManager` —
until the defect is fixed. The XML docs on `PollGroupMessage` describe the parameters by their
true meaning, so they are accurate for the new API; the divergence is recorded here so it is
not discovered by surprise.

**`SteamDatagramHostedAddress::SetDevAddress` was not exposed.** It fabricates a routing
address for development, and its only consumer would be ticket generation — and
`SteamDatagramRelayAuthTicket` in this binding is still a stub marked "Not implemented, not
used". Exposing a setter with nothing to feed would be API surface without a use case.

**`ReceivedRelayAuthTicket` / `FindRelayAuthTicketForServer` were not exposed.** They were
outside the requested scope and both take a `SteamDatagramRelayAuthTicket`, which is the same
unimplemented stub. Note the consequence: a client currently has no binding-level way to put a
ticket into Steam's cache, so the ticketed flow is only completable by an application that
reaches the ticket cache another way. This is called out here rather than left to be
discovered.

---

## 5. Header ambiguities resolved by assumption

Everything below was inferred, not read. Each is also flagged in the XML docs at the point of
use, rather than being presented as certainty.

1. **`ReceiveMessagesOnPollGroup` return on an invalid handle.** Undocumented. Assumed to
   follow `ReceiveMessagesOnConnection` (`-1`); any negative value throws.
2. **Fate of messages queued on a poll group when it is destroyed.** Undocumented. The
   `Destroy()` docs recommend draining first rather than claiming a behaviour.
3. **`SDR_LISTEN_PORT` vs `SDR_LISTEN_SOCKET`.** `isteamnetworkingsockets.h` names the
   variable `SDR_LISTEN_SOCKET` in exactly one place — the failure list for
   `GetHostedDedicatedServerAddress` — and `SDR_LISTEN_PORT` in the other three. Treated as a
   typo in Valve's header; the docs say `SDR_LISTEN_PORT` and note the discrepancy.
4. **`SteamDatagramHostedAddress` / `SteamDatagramGameCoordinatorServerLogin` field
   semantics.** Neither struct is *defined* in `Generator/steam_sdk/` — the headers only
   forward-declare them, and the real definitions live in `steamdatagram_tickets.h` /
   `steamdatagram_gamecoordinator.h`, which are not vendored here. Field layout was taken from
   `steam_api.json`, which carries no comments. Meanings of `m_cbSize`, `m_data`, `m_rtime`
   were inferred from how `GetHostedDedicatedServerAddress` and
   `GetGameCoordinatorServerLogin` describe using them. `m_rtime` is treated as `RTime32`
   (Unix seconds, UTC) consistently with every other `RTime32` in this binding.
5. **Whether the game server interface is strictly required for
   `GetHostedDedicatedServerAddress` and `GetGameCoordinatorServerLogin`.** Valve states it
   only for `CreateHostedDedicatedServerListenSocket`. Applied to all three on the grounds
   that a certificate issued to *this server* is what signs the login, and there is no
   coherent meaning for either call on a user interface.

---

## 6. Verification

No Steam client, account or network is involved in any of this, so the checks are static:

- `dotnet build Facepunch.Steamworks.sln -c Release` — 0 errors across `net46`,
  `netstandard2.1` and `net6.0`, for the Win64 / Win32 / Posix projects.
- `verify-native-conformance.ps1` — every `DllImport` entry point used still exists in all 17
  committed native binaries. Nothing new was P/Invoked; the low-level bindings for all of this
  already existed in `Generated/Interfaces/ISteamNetworkingSockets.cs`.
- `verify-struct-layout.ps1` — the new public value types (`PollGroup`,
  `HostedServerAddress`) change the recorded layout set, so the baseline in
  `Tools/baselines/layout-win64.txt` was re-recorded. No *existing* struct's layout moved;
  the diff is additions only.

`net46` has no `Span<T>` in this project (there is no `System.Memory` package reference), so
every span-based API in the codebase is guarded by
`#if NETSTANDARD2_1_OR_GREATER || NET`. Nothing added here needs `Span<T>` on a hot path —
the zero-allocation receive path uses `stackalloc` and raw pointers, which work identically on
all three target frameworks.
