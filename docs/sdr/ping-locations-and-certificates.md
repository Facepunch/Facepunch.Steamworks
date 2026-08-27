# SDR: ping locations, points of presence, and certificates

This note covers the Steam Datagram Relay features added to the public API in this change:
latency estimation between arbitrary hosts, enumeration of Valve's relay clusters, and
application-provided certificates. It is written for someone who has never used SDR.

## What SDR actually is

Steam Datagram Relay is Valve's private network overlay for game traffic. Instead of two
players sending UDP straight to each other, each player sends to a nearby Valve **relay**,
and the traffic crosses Valve's backbone between relays. You get three things from that:

- **No IP disclosure.** Neither peer learns the other's address, so there is nothing to
  DDoS. This is the reason most studios adopt it.
- **Often lower latency.** Public internet routing between two ISPs is frequently
  suboptimal. Valve's backbone is not.
- **NAT traversal that works**, because both ends are making outbound connections.

The cost is that everything is authenticated, and routing decisions have to be made by
somebody. The APIs below are how you participate in those decisions.

## Part A — ping locations

### The problem

You want to put players on the server, or with the peer, that gives the lowest latency.
The obvious approach — have everyone ping everyone — is `O(n²)` round trips and takes
seconds. You cannot do it inside a matchmaking query.

### The primitive

Steam continuously measures each client's latency to Valve's relay clusters and keeps a
routing model in memory. A **ping location** (`NetPingLocation`) is a snapshot of where
one host sits in that model. Give Steam two ping locations and it will *estimate* the
round-trip time between them instantly, locally, without sending a packet.

That estimate is conservative and assumes the connection is relayed. If the two peers end
up with a direct route via NAT traversal, the real ping may be better — or slightly worse,
since plain IP routing is often bad. Treat it as a reasonable upper bound.

### The catch, and why the string round-trip exists

`NetPingLocation` is a 512-byte opaque blob, and Valve is explicit: **do not serialise it,
send it over the wire, or persist it.** It is only meaningful inside the process that
produced it.

So there is a supported text form. `ConvertPingLocationToString` produces a compact,
human-readable token; `ParsePingLocationString` turns it back into a location on another
machine. That string is what you put in lobby metadata or POST to your own matchmaker.

Three rules:

1. **Never parse it yourself.** Valve states the format is subject to change.
2. **Size buffers with `MaxPingLocationStringLength` (1024).** That is
   `k_cchMaxSteamNetworkingPingLocationString`, which Valve calls "an extremely
   conservative worst case value which leaves room for future syntax enhancements". Real
   strings are a few dozen bytes. Size a *buffer* with 1024; do not size a *database
   column* or a packet field with it.
3. **Treat a stored string as a cache measured in hours, not months.** *This is inferred,
   not documented.* The header states no expiry, but the underlying data is refreshed
   periodically and a player's real network position changes. Refresh at session start
   rather than persisting one in a player profile forever.

### The shape of the API

```csharp
// Host: publish where we are.
await SteamNetworkingUtils.WaitForPingDataAsync();
var here = SteamNetworkingUtils.LocalPingLocation.Value;
lobby.SetData( "loc", SteamNetworkingUtils.ConvertPingLocationToString( ref here ) );

// Joiner: estimate against every candidate without connecting to any of them.
var me = SteamNetworkingUtils.LocalPingLocation.Value;

foreach ( var candidate in lobbies )
{
    if ( !SteamNetworkingUtils.TryParsePingLocation( candidate.GetData( "loc" ), out var host ) )
        continue;

    var ping = SteamNetworkingUtils.EstimatePingBetween( ref me, ref host );
    if ( ping < 0 ) continue;   // PingFailed (-1) or PingUnknown (-2)
    ...
}
```

`EstimatePingTo` is the same thing when one end is the local host — it is faster and
slightly more accurate, because Steam knows more about its own routing than a location
string can carry.

**The bug everyone writes once:** failure is signalled by a *negative return*, not an
exception. `PingFailed` is `-1` and `PingUnknown` is `-2`. If you sort ascending without
filtering, every unmeasured candidate sorts to the front and looks like the best possible
match. Always check for negatives first.

Doing this estimation on a *backend* rather than in a game client is a different problem —
Valve ships a separate "ticketgen" / game-coordinator library for it. This binding only
wraps the in-process API.

## Part B — points of presence

A **POP** is one of Valve's relay clusters, named with a short airport-style code: `iad`,
`sto`, `fra`, `lhr`. `GetPOPList()` enumerates them, `GetDirectPingToPOP` gives the raw
first-hop latency, and `GetPingToDataCenter` gives the latency of the best *relayed*
route, plus the intermediate relay it goes through.

`GetPingToDataCenter` can return a **lower** number than `GetDirectPingToPOP` for the same
POP. That is not a bug — entering Valve's backbone nearby and travelling privately often
beats going straight there over the public internet.

### `NetPOPID`

POP IDs are a 3- or 4-character ASCII code packed into a `uint`, and the packing is
genuinely strange. Valve shipped a 3-character format, then had to add a fourth character
without breaking IDs already in their databases. The result, from the header: the code
`"abcd"` encodes as `0xddaabbcc` — the *fourth* character lands in the *most significant*
byte, out of order. Valve's own comment on this is "deep regret and sadness".

`NetPOPID` is a value type wrapping that `uint`, modelled on `SteamId` and `DepotId` in
`Structs/`. It handles the packing so nothing in game code has to.

**Design decisions:**

- **A value type rather than raw `uint`.** POP IDs are passed around alongside plain
  latency `int`s and other IDs; making it a distinct type means the compiler catches
  argument transposition, and `ToString()` prints `"fra"` instead of `1718903296`.
- **`FromCode` validates; Valve's helper does not.** The native packer casts each
  character to `uint8`, so a non-ASCII code silently truncates into a *different,
  valid-looking* POP. `FromCode` throws instead, and `TryParse` is there for input that
  came off the network or out of a config file. The printable-ASCII bound is inferred from
  the cast, not stated by Valve.
- **`CompareTo` orders by raw value, not alphabetically.** Because of the encoding those
  are different orders. It is documented on the member; sort on `ToString()` for display.
- **`WriteCode` overloads exist so rendering is allocation-free.** `ToString()` allocates;
  a latency HUD refreshing every frame should not.

### Allocation

The enumerate-and-ping path allocates nothing:

```csharp
var pops = new NetPOPID[SteamNetworkingUtils.POPCount];   // once
...
var count = SteamNetworkingUtils.GetPOPList( pops );      // every frame, zero alloc
for ( int i = 0; i < count; i++ )
    ping[i] = SteamNetworkingUtils.GetDirectPingToPOP( pops[i] );
```

`GetPOPList()` with no arguments is the convenience form and does allocate — it is for
startup and settings screens.

## Part C — certificates and identity

### When you need this

Almost never. A process signed in to Steam gets an SDR certificate automatically and
renews it automatically. Valve files these functions under "advanced".

You need them when a process **cannot** get a certificate from Steam: an anonymous
dedicated server in a data center with no Steam account, or a build on a platform where
the player signs in to something that is not Steam.

### The workflow, end to end

Valve documents this in about four lines, so here it is in full:

1. **Game instance:** `SteamNetworkingCertificates.TryGetCertificateRequest( out var blob, out var error )`.
   You get a small opaque blob — Valve's conservative estimate is 512 bytes — containing a
   freshly generated public key and the identity being requested. The matching private key
   never leaves the process.
2. **Send the blob to your own backend** (Valve calls it the "game coordinator") over
   whatever channel you already have. The blob is not secret, but the channel must be
   authenticated: whoever you sign a certificate for gets to claim that identity on your
   network.
3. **Backend:** call `SteamDatagram_CreateCert`. **This is not part of `steam_api` and
   therefore not part of this binding.** It lives in Valve's separate game-coordinator
   library (`steamdatagram_gamecoordinator.h`), shipped with the Steam Datagram SDK. It
   needs your app's SDR signing private key, which you generate and register on the
   Steamworks partner site. That key belongs in a secret store on a server you control,
   never in the game build.
4. **Send the signed certificate back** to the game instance.
5. **Game instance:** `TrySetCertificate( signed, out var error )` — **before** you create
   listen sockets or connections.

### Gotchas

- **Order matters.** A connection attempted without a usable certificate fails
  authentication; it does not wait for one to arrive.
- **The error string is your only diagnostic.** Both calls report failure through an out
  `error` sourced from Steam's `SteamNetworkingErrMsg`. Log it. It is `null` on success, so
  the happy path allocates nothing.
- **Build the renewal path from day one.** Valve does not document a certificate lifetime
  in the public header, so assume expiry: your process must be able to request and install
  a new certificate *while running*, not only at launch. *(That certificates expire at all
  is inferred from their being signed credentials.)*
- **`ResetIdentity` does nothing useful on Steam.** The header says so outright: "This
  function is not actually supported on Steam! It is included for use on other platforms
  where the active user can sign out and a new user can sign in." It closes every open
  connection and discards the current certificate.

### Two things deliberately not exposed

**The null-identity form of `ResetIdentity`.** Natively you may pass a null identity,
leaving the identity invalid until `SetCertificate` supplies one. The generated binding
takes the identity by `ref` and cannot express a null pointer. Forging one (`ref *(T*)0`)
would work in practice on current runtimes but is not worth shipping untested in a
library, particularly one that cannot be exercised against live Steam here. If you need
that behaviour, install a certificate whose embedded identity is the one you want.

**Anything from the game-coordinator library.** `SteamDatagram_CreateCert` and friends are
a different binary with a different distribution; wrapping them is out of scope for a
`steam_api` binding.

## Implementation notes

**Where the code lives.** Ping-location and POP APIs are on `SteamNetworkingUtils`, since
that is the interface they belong to natively. Certificates and `ResetIdentity` live on
`ISteamNetworkingSockets`, not utils — they are exposed as a separate
`SteamNetworkingCertificates` class rather than folded into `SteamNetworkingSockets`,
which keeps a rarely used, easily misused workflow visibly separate from ordinary socket
code.

**No new P/Invoke.** Every one of these calls an `extern` the generator already emitted.
`Interfaces/ISteamNetworkingUtils.PingLocation.cs` is a hand-written `partial` that adds
caller-buffer overloads on top of the existing `private extern`s — a partial class shares
member accessibility across files, so this needs no generator change and re-running the
generator will not delete it. The generated wrappers were unsuitable on their own: the
convenience `ConvertPingLocationToString` leases a 32 KB buffer from a lock-protected pool
to produce a string that is never longer than 1 KB, and the convenience
`ParsePingLocationString` heap-allocates a NUL-terminated copy of its input. The public
layer uses a 1 KB stack buffer instead.

**Multi-targeting.** The library targets `net46`, `netstandard2.1` and `net6.0`. **net46
has no `Span<T>`** — `System.Memory` is not referenced. Every span overload is therefore
behind `#if NETSTANDARD2_1_OR_GREATER || NET`, matching the existing split in
`SteamNetworkingMessages.cs`, and each has an array-plus-offset or `IntPtr` counterpart
available on all three targets. `stackalloc` itself is fine everywhere, so the internals
use raw pointers and only the public surface varies.

**Buffer size enforcement.** `ConvertPingLocationToString` requires a full
`MaxPingLocationStringLength` destination even though real strings are far shorter. Valve
documents the minimum but says nothing about what happens if you pass less, so a smaller
buffer is rejected rather than risking a silently truncated — and therefore unparseable —
location.

**A latent marshalling wart, worked around here.** `SteamNetworkingErrMsg` is natively
`char[1024]`, i.e. 1024 *bytes* of NUL-terminated UTF-8. The generated
`NetErrorMessage` declares `fixed char Value[1024]`, which in C# is 2048 bytes of UTF-16.
The buffer handed to Steam is therefore twice the size Steam expects, which is harmless —
Steam writes at most 1024 bytes into it — but the contents must be read as *bytes*.
Reading them as `char`s yields mojibake. `SteamNetworkingCertificates.ReadErrorMessage`
casts to `byte*` for this reason; the comment on that method records why. The declaration
itself is in `Networking/NetErrorMessage.cs` and was left alone.

**`NetPOPID` and the struct-layout baseline.** `NetPOPID` is a new value type, so it adds
lines to `Tools/baselines/layout-win64.txt` and `verify-struct-layout.ps1` fails until the
baseline is re-recorded. That is expected and is the tool working as designed. The struct
is a single `uint` with default sequential layout, which is what the native
`SteamNetworkingPOPID` typedef is, so `GetPOPList` can fill a caller's `NetPOPID[]`
directly through a pointer cast with no intermediate array.
