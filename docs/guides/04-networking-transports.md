# Networking — choosing a transport, and using it without allocating

This guide assumes you have never shipped a Steam game. Steam offers three networking APIs
that all look like they do the same thing, Valve's documentation does not clearly say which
to pick, and one of them is superseded but not marked deprecated anywhere. This guide picks
for you and explains why.

If you only read one thing: **use `SteamNetworkingSockets`.** The other two exist for
specific shapes of game, and the third is legacy.

---

## 1. The three APIs, and the one that is not an API

| Class | Shape | Use it when |
|---|---|---|
| **`SteamNetworkingSockets`** | Connection-oriented. Listen sockets, connections, connection state callbacks. | **Default.** Anything with a host and clients, or a dedicated server. |
| **`SteamNetworkingMessages`** | Connectionless. Send to a `SteamId` on a channel; Steam manages the session underneath. | Small mesh games, or a UDP-shaped codebase you are porting. |
| **`SteamNetworking`** | The original P2P API. | Nothing new. It works, but Valve superseded it. |

**Steam Datagram Relay is not a fourth API.** SDR is a *routing mode* of
`SteamNetworkingSockets`: the same sockets, connections and messages, with traffic routed
through Valve's relay network instead of directly. You choose it at socket-creation time:

```csharp
SteamNetworkingSockets.CreateNormalSocket( address );        // direct UDP
SteamNetworkingSockets.CreateRelaySocket<T>( virtualport );  // through SDR
```

Everything after that call is identical. This is why "should I use SDR or NetworkingSockets"
is not a real question — see §6.

### A note on `SteamNetworking`

The legacy P2P API is **not marked `[Obsolete]`** anywhere in this library, and its
class comment says nothing about being superseded. That is worth knowing so you do not
conclude from the compiler's silence that it is the current recommendation. Valve's own SDK
supersedes it with the two APIs above; 13 of its native functions are not even bound here
because they belong to a socket layer Valve replaced. Use it only to keep an existing
integration alive.

If you are on it today, one shape to be aware of: `SendP2PPacket`'s two overloads have
**different channel defaults** — the `byte[]` overload defaults to channel `0`, the `byte*`
overload to channel `1`. That is almost certainly an oversight, but it is what compiles, so
pass the channel explicitly.

---

## 2. Which one do I want?

Answer these in order.

**Do you have a dedicated server?**
→ `SteamNetworkingSockets`, and read [the dedicated servers guide](06-dedicated-servers.md).
If it runs in a Valve data centre, use the hosted-server SDR path (§6).

**Do you have a host player that others connect to (listen server, co-op, party game)?**
→ `SteamNetworkingSockets` with a relay socket. You get NAT traversal and IP privacy free.

**Is every player equal, talking to every other player, with no host?**
→ `SteamNetworkingMessages`. It is connectionless, so an N-player mesh does not require you
to manage N² connection lifecycles. Steam creates and tears down the underlying sessions.

**Are you porting a codebase built on `sendto`/`recvfrom` with your own reliability layer?**
→ `SteamNetworkingMessages` maps onto that shape most cleanly, and `SendType.Unreliable`
behaves like the UDP you already have.

**Anything else?**
→ `SteamNetworkingSockets`.

The honest summary: connection-oriented code is easier to reason about, because "this player
disconnected" is an event rather than something you infer from silence. Take the
connectionless API only when the mesh shape genuinely earns it.

---

## 3. `SteamNetworkingSockets` — the standard shape

You implement two roles. The **socket** listens and accepts; the **connection** is one end of
one link.

### Server side

```csharp
class GameServer : SocketManager
{
    public override void OnConnecting( Connection connection, ConnectionInfo info )
    {
        // Your chance to refuse. Call Accept() to allow.
        connection.Accept();
    }

    public override void OnConnected( Connection connection, ConnectionInfo info )
    {
        Log( $"{info.Identity} joined" );
    }

    public override void OnDisconnected( Connection connection, ConnectionInfo info )
    {
        Log( $"{info.Identity} left: {info.EndReason}" );
    }

    public override void OnMessage( Connection connection, NetIdentity identity,
                                    IntPtr data, int size,
                                    long messageNum, long recvTime, int channel )
    {
        HandlePacket( connection, data, size );
    }
}

var socket = SteamNetworkingSockets.CreateRelaySocket<GameServer>();

// every tick:
socket.Receive();
```

`OnConnecting` fires **before** the connection is established and is your admission control
point. If you do not call `connection.Accept()`, the connection never completes. The base
implementation accepts everything, so overriding it is only necessary if you want to refuse
someone — but that is where a ban check or a server-full check belongs.

If you prefer composition to inheritance, every creator has an overload taking an interface
instead:

```csharp
var socket = SteamNetworkingSockets.CreateRelaySocket( virtualport: 0, myISocketManager );
```

### Client side

```csharp
class GameClient : ConnectionManager
{
    public override void OnConnected( ConnectionInfo info ) => Log( "connected" );
    public override void OnDisconnected( ConnectionInfo info ) => Log( $"lost: {info.EndReason}" );

    public override void OnMessage( IntPtr data, int size,
                                    long messageNum, long recvTime, int channel )
    {
        HandlePacket( data, size );
    }
}

var conn = SteamNetworkingSockets.ConnectRelay<GameClient>( hostSteamId );

// every tick:
conn.Receive();
```

> **`ConnectionManager.OnConnecting` never fires.** The `Connecting` field is initialised to
> `true`, and the dispatch that would raise `OnConnecting` is guarded by
> `if ( !Connecting && !Connected )`. So on a normal client connect the guard is never
> satisfied. Do not put logic in that override; `OnConnected` and `OnDisconnected` work
> correctly. This is a real defect, recorded here rather than left to be discovered.

### Sending

```csharp
connection.SendMessage( bytes );                                  // reliable (default)
connection.SendMessage( bytes, SendType.Unreliable );
connection.SendMessage( ptr, size, SendType.Unreliable | SendType.NoNagle );
```

`SendType` has exactly four members:

| Member | Meaning |
|---|---|
| `Unreliable` | `0` — fire and forget. This is the *absence* of `Reliable`, not a flag. |
| `Reliable` | Retransmitted and delivered in order. |
| `NoNagle` | Send immediately; do not coalesce with the next message. |
| `NoDelay` | Do not buffer at all — fail rather than queue if the link is not ready. |

`Unreliable` being `0` matters: `SendType.Unreliable | SendType.NoNagle` is just `NoNagle`,
which is correct but reads oddly, and there is no way to express "unreliable" as a positive
flag. Reliable is the default on every overload.

**Nagle is on by default and you usually want it.** It coalesces small sends into one packet.
Reach for `NoNagle` on the packet that ends a batch — the input frame, the state snapshot —
not on everything, or you lose the batching benefit entirely.

The `string` overloads exist and are marked in-source as *"This creates a ton of garbage — so
don't do anything with this beyond testing!"* Believe it.

---

## 4. Receiving without allocating

This is where a Steam integration usually starts leaking garbage, and where this fork has
done the most work. There are two shapes of receive API and the difference is not cosmetic.

### The allocating shape

```csharp
// Allocates one byte[] per message.
pollGroup.Receive( ( conn, identity, data ) => Handle( data ) );

SteamNetworkingMessages.ReceiveMessagesOnChannel( 0, ( from, channel, data ) => Handle( data ) );
```

Both are documented in-source as **"CONVENIENCE ONLY"**. They copy every message into a
freshly allocated array before handing it to you. They are for prototypes and tests. On a
server tick they are the wrong call: one array per message, per tick, forever.

### The zero-allocation shape

```csharp
// Hoisted into a field — see below for why this matters.
static readonly PollGroupMessage handler = OnPollGroupMessage;

static void OnPollGroupMessage( Connection connection, NetIdentity identity,
                                IntPtr data, int size,
                                long messageNum, long recvTime, int channel )
{
    // `data` points into Steam's own buffer. Valid until this method returns.
    ReadDirectlyFrom( data, size );
}

// every tick:
pollGroup.Receive( handler );
```

Nothing is allocated per message or per call. The message pointer array is `stackalloc`'d,
the message struct is read through a raw pointer, and the payload is handed over as
`IntPtr` + `int` with no copy.

**Two rules come with that.**

1. **The buffer belongs to Steam, and is released as soon as your handler returns.** If you
   need the bytes after that — queuing them for another thread, deferring to end of frame —
   you must copy them yourself. Storing the `IntPtr` is a use-after-free.

2. **Hoist the delegate into a cached field.** Writing the handler as a lambda at the call
   site that captures a local allocates a closure *every tick* and throws away the entire
   point of the API. The type system cannot enforce this; the XML docs say it and so does
   this guide.

`SteamNetworkingMessages` has the same split — `ReceiveMessagesOnChannel( channel,
MessageIntercept, ... )` is the zero-allocation form:

```csharp
public delegate void MessageIntercept( NetIdentity identity, int channel, IntPtr data, int size );
```

Note the parameter order: **channel is second** here, but **last** in `OnMessage` and
`PollGroupMessage`. They are all `int`s, so a transposition compiles silently.

### The `messageNum` / `recvTime` fix

`OnMessage` takes `(..., long messageNum, long recvTime, int channel)`. Both are `long`, and
in upstream they were **passed the other way round** — every consumer received a microsecond
timestamp as the message number and a sequence counter as the receive time, silently, on
both `SocketManager` and `ConnectionManager`.

That is fixed here. The cause is worth knowing: the native `NetMsg` struct declares
`RecvTime` *before* `MessageNumber`, so passing the fields in struct order produces the swap
and compiles cleanly.

**If you are migrating from upstream and compensated for the swap in your own handler, remove
that compensation.**

---

## 5. Poll groups — receiving from many connections at once

The naive server drains each connection separately:

```csharp
foreach ( var conn in connections )
    conn.Receive();          // one native call per connection, per tick
```

At 100 players and 60 Hz that is 6,000 managed→native transitions per second, and the large
majority return zero messages. A **poll group** merges many connections into one queue:

```csharp
var group = SteamNetworkingSockets.CreatePollGroup();

foreach ( var conn in connections )
    group.Add( conn );       // or conn.SetPollGroup( group )

// every tick — 1 call regardless of player count:
group.Receive( handler );

// when you are done with it:
group.Destroy();
```

60 calls per second instead of 6,000, and receive cost now scales with **message** count
rather than **player** count.

`SocketManager` already creates a poll group internally and adds every accepted connection to
it, exposed as `socket.PollGroup` — so if you use `SocketManager.Receive()` you are already
getting this. Create your own when you want to partition traffic: per match, per room, lobby
chatter vs. gameplay, trusted vs. untrusted. You can also poll connections a `SocketManager`
never created, such as outbound links to a backend.

`PollGroup` is a struct handle, like `Socket` and `Connection`. It is deliberately **not**
`IDisposable`: a struct gets copied freely into lists, lambdas and property getters, and every
copy would carry a `Dispose` that destroys the *shared* underlying group. `using var group =
…` would look correct and be a double-destroy waiting to happen. Call `Destroy()` explicitly,
and drain the group before you do — what happens to messages still queued on a destroyed poll
group is not documented by Valve, so the safe order is drain, then destroy.

`bufferSize` defaults to 32 and is capped at `PollGroup.MaxReceiveBufferSize` (256), which
bounds the `stackalloc` at 2 KB on a 64-bit runtime.

---

## 6. SDR — when and how

Steam Datagram Relay routes your traffic through Valve's relay network. Two things you get:

- **Neither peer learns the other's IP address.** There is nothing to DDoS. This is why most
  studios adopt it.
- **Often lower latency**, because Valve's backbone routinely beats public inter-ISP routing.

Plus NAT traversal that works, because both ends make outbound connections.

### Peer-to-peer / listen server

```csharp
// Host:
var socket = SteamNetworkingSockets.CreateRelaySocket<GameServer>();

// Client:
var conn = SteamNetworkingSockets.ConnectRelay<GameClient>( hostSteamId );
```

That is the whole difference from the direct path — the host is identified by `SteamId`, not
by address, and no address is ever exchanged. Anyone who owns your app and is signed into
Steam can connect.

Call `SteamNetworkingUtils.InitRelayNetworkAccess()` at startup, well before you need a
connection. It begins measuring latency to Valve's relays in the background; without it, your
first connection pays for that measurement. `SteamNetworkingUtils.Status` reports how far
along it is.

### Hosted dedicated servers

If your server runs in a Valve data centre, it can live *inside* the relay network:

```csharp
// On the server:
ushort   port = SteamNetworkingSockets.HostedDedicatedServerPort;
NetPOPID pop  = SteamNetworkingSockets.HostedDedicatedServerPopId;

var socket = SteamNetworkingSockets.CreateHostedDedicatedServerSocket<GameServer>();
```

There are two flavours:

| | Who may join | How the client connects |
|---|---|---|
| **Ticketless** | Anyone who owns the app and is signed into Steam | `ConnectRelay` |
| **Ticketed** | Only holders of a ticket your backend issued | `ConnectToHostedDedicatedServer` |

Tickets buy you one specific thing worth the complexity: a cached ticket lets a client
reconnect **even while disconnected from Steam**, because the ticket is already on disk.

These calls route through the *game server* interface, which Valve's header requires. They
throw `InvalidOperationException` naming `SteamServer.Init` if you have not initialised a
game server, and they throw — rather than returning an invalid handle — when Steam refuses,
with a message naming the actual cause: `SDR_LISTEN_PORT` not set for a listen failure, no
cached auth ticket for a connect failure.

**One current limitation to plan around:** this binding has no way to *put* a ticket into
Steam's cache — `ReceivedRelayAuthTicket` / `FindRelayAuthTicketForServer` both take a type
that is still an unimplemented stub. So the ticketed flow is only completable by an
application that reaches the ticket cache another way. Use the ticketless flavour unless you
have that covered.

Full detail, including every behaviour that was inferred rather than documented by Valve:
[poll groups & hosted servers](../sdr/poll-groups-and-hosted-servers.md).

### Latency-aware matchmaking

Ping locations let you estimate round-trip time between two hosts **without sending a
packet**, which is what makes latency-aware matchmaking possible at all — the obvious
approach of everyone pinging everyone is `O(n²)` round trips and cannot run inside a
matchmaking query.

```csharp
await SteamNetworkingUtils.WaitForPingDataAsync();

var here = SteamNetworkingUtils.LocalPingLocation.Value;
lobby.SetData( "loc", SteamNetworkingUtils.ConvertPingLocationToString( ref here ) );
```

and on the other side:

```csharp
var me = SteamNetworkingUtils.LocalPingLocation.Value;

foreach ( var candidate in lobbies )
{
    if ( !SteamNetworkingUtils.TryParsePingLocation( candidate.GetData( "loc" ), out var host ) )
        continue;

    int ping = SteamNetworkingUtils.EstimatePingBetween( ref me, ref host );
    if ( ping < 0 ) continue;      // PingFailed (-1) or PingUnknown (-2)

    Consider( candidate, ping );
}
```

**Failure is a negative return, not an exception.** Sort ascending without filtering and every
unmeasured candidate sorts to the front looking like a perfect match. Check for negatives
first, every time.

Never serialise a `NetPingLocation` itself — it is a 512-byte blob meaningful only inside the
process that produced it. The string form is the supported wire representation, and you must
not parse it yourself; Valve states the format may change.

More: [ping locations & certificates](../sdr/ping-locations-and-certificates.md).

---

## 7. `SteamNetworkingMessages` — the connectionless shape

```csharp
// Accept incoming sessions, or nothing arrives.
SteamNetworkingMessages.OnSessionRequest += identity =>
{
    var id = identity;                                    // need a local for `ref`
    if ( IsExpected( id.SteamId ) )
        SteamNetworkingMessages.AcceptSessionWithUser( ref id );
};

SteamNetworkingMessages.OnSessionFailed += info => Log( $"session failed: {info.EndReason}" );

// Send.
NetIdentity peer = someSteamId;
SteamNetworkingMessages.SendMessageToUser( ref peer, bytes,
                                           SteamNetworkingOptions.Reliable, channel: 0 );

// Receive, zero-allocation.
SteamNetworkingMessages.ReceiveMessagesOnChannel( 0, intercept );
```

Four things this API does differently, all of which cause compile errors or silent bugs:

1. **It uses a different flags enum.** Not `SendType` — `SteamNetworkingOptions`, declared
   alongside the class: `Unreliable`, `NoNagle`, `UnreliableNoNagle`, `NoDelay`,
   `UnreliableNoDelay`, `Reliable`, `ReliableNoNagle`, `AutoRestartBrokenSession`. There are
   no defaults on `flags` or `channel`; both are required arguments.

2. **Identities are passed by `ref`.** You cannot write
   `SendMessageToUser( ref someSteamId, … )` — you need a `NetIdentity` local. There is an
   implicit conversion from `SteamId`, so `NetIdentity peer = someSteamId;` is the idiom.

3. **`OnSessionRequest` and `OnSessionFailed` are plain fields, not `event`s.** So `=` silently
   replaces every other subscriber instead of failing to compile. Always use `+=`. The same is
   true of `SteamNetworking.OnP2PSessionRequest` and `OnP2PConnectionFailed`.

4. **The `byte[]` receive callback gives you a `SteamId`, the intercept form gives you a full
   `NetIdentity`.** Different types for the same concept, on two overloads of one method.

`AutoRestartBrokenSession` is worth knowing about: it tells Steam to transparently re-establish
a session that broke, which is close to the "it just works" behaviour people expect from a
connectionless API.

---

## 8. Tuning and diagnostics

Configuration is global, set through properties on `SteamNetworkingUtils`:

```csharp
SteamNetworkingUtils.ConnectionTimeout = 10000;   // ms to establish  (TimeoutInitial)
SteamNetworkingUtils.Timeout           = 20000;   // ms before an idle link is dead
SteamNetworkingUtils.SendBufferSize    = 524288;
SteamNetworkingUtils.NagleTime         = 5000;    // microseconds, default 5000
```

Note `ConnectionTimeout` maps to Steam's *initial* timeout and `Timeout` to the connected
one — the names do not make that obvious. `NagleTime` is in **microseconds**; setting it to
`5` gets you 5 µs, not 5 ms.

There is no per-connection configuration surface in this binding; every setter is global.

### Simulating a bad network

```csharp
SteamNetworkingUtils.FakeSendPacketLoss = 5.0f;    // percent
SteamNetworkingUtils.FakeRecvPacketLag  = 100.0f;  // ms
```

Do this. A netcode that has only ever run on localhost has not been tested.

### Debug output

```csharp
SteamNetworkingUtils.DebugLevel = NetDebugOutput.Msg;
SteamNetworkingUtils.OnDebugOutput += ( level, message ) => Log( $"[{level}] {message}" );
```

Levels run `None`, `Bug`, `Error`, `Important`, `Warning`, `Msg`, `Verbose`, `Debug`,
`Everything`. `Msg` is a reasonable development setting; `Verbose` and above will drown you.

Usefully, the output is **queued and flushed on the dispatch loop**, not raised on Steam's
own thread — so your handler runs wherever you pump callbacks, and can touch engine objects
safely if you pump on the main thread.

Per-connection state, allocation-free:

```csharp
ConnectionStatus s = connection.QuickStatus();
Console.WriteLine( $"ping {s.Ping}ms  out {s.OutPacketsPerSec}/s  quality {s.ConnectionQualityLocal}" );
```

`connection.DetailedStatus()` returns Valve's own multi-line diagnostic string, or `null` on
failure. It is the right thing to dump into a bug report.

---

## 9. Sharp edges

Verified against the source, so you do not lose a day to any of them.

- **`NetIdentity.IsLocalHost` does not work.** It tests a `default` identity rather than
  `this`, so its answer is unrelated to the identity you asked about. `NetAddress.IsLocalHost`
  is correct — use that.
- **`NetAddress` is IPv4-only.** `NetAddress.From( IPAddress, port )` and the `Address`
  getter throw `NotImplementedException( "Oops - no IPV6 support yet?" )` for an IPv6
  address. Plan for it, do not discover it.
- **`ConnectionManager.OnConnecting` never fires** (§3).
- **`SteamNetworkingSockets.CreateFakeUDPPort` returns an unusable handle** — the backing
  class is empty and its native pointer is permanently zero. It is on the open list.
- **On a listen server (client *and* server initialised in one process),
  `SteamNetworkingSockets` resolves to the *client* interface**, so listen sockets are created
  under the user's identity rather than the game server's. No error is raised. The SDR
  hosted-server calls deliberately bypass this; nothing else does. See
  [audit 04](../audit/04-gameserver.md) F5.
- **Exceptions thrown from your message handler are swallowed** by the dispatch layer, and
  permanently drop the remaining handlers for that callback. Set `Dispatch.OnException`
  during development.
- **`Connection.ConnectionName`'s getter returns the string `"ERROR"`** on failure, not
  `null`. Do not display it unfiltered.

---

## 10. A minimal working shape

Putting it together — a host and a client over SDR, allocation-free on the receive path.

```csharp
// ---- shared ----
SteamNetworkingUtils.InitRelayNetworkAccess();

// ---- host ----
class Host : SocketManager
{
    public override void OnConnecting( Connection c, ConnectionInfo info )
    {
        if ( Connected.Count >= MaxPlayers ) { c.Close(); return; }
        c.Accept();
    }

    public override void OnMessage( Connection c, NetIdentity id, IntPtr data, int size,
                                    long messageNum, long recvTime, int channel )
        => Server.Handle( c, data, size );
}

var socket = SteamNetworkingSockets.CreateRelaySocket<Host>();
// tick: socket.Receive();
// tell everyone where you are, e.g. lobby.SetData( "host", SteamClient.SteamId.ToString() );

// ---- client ----
class Client : ConnectionManager
{
    public override void OnConnected( ConnectionInfo info ) => Game.Start();
    public override void OnDisconnected( ConnectionInfo info ) => Game.Stop( info.EndReason );

    public override void OnMessage( IntPtr data, int size,
                                    long messageNum, long recvTime, int channel )
        => Game.Handle( data, size );
}

var conn = SteamNetworkingSockets.ConnectRelay<Client>( hostSteamId );
// tick: conn.Receive();
```

Both `Receive()` calls must run every tick, and callbacks must be pumped — see
[Getting Started §5](01-getting-started.md#5-callbacks-the-part-everyone-gets-wrong). A
connection that never reports `OnConnected` is almost always a missing `RunCallbacks()`.

---

## Where to go next

- [Dedicated servers](06-dedicated-servers.md) — the full server lifecycle, auth, and the
  server browser.
- [Lobbies & matchmaking](03-lobbies-and-matchmaking.md) — how players find each other before
  any of this runs.
- [Poll groups & hosted servers](../sdr/poll-groups-and-hosted-servers.md) — the design
  reasoning behind the SDR surface, and the header ambiguities resolved by assumption.
- [Performance audit](../audit/06-performance.md) — the measured cost of every receive shape
  described above.
