# Performance Audit

Measured audit of allocation, interop-transition count and per-frame work.
Everything below is either a **number produced by a harness on this machine**, a
**quoted line of source**, or an **API-shape argument**. Where a claim could not
be settled offline it is in [Needs runtime proof](#needs-runtime-proof).

- Repo state: `d73b90c`, sources snapshotted 2026-07-28 13:58 (this audit is read-only;
  `SteamNetworkingSockets.cs`, `SteamNetworkingUtils.cs`, `Networking/` and `Structs/`
  were being edited concurrently, so line numbers in those files may drift by a few lines).
- Runtime: .NET 6.0.36, x64, workstation GC, non-concurrent, Release/optimised.

---

## Summary

**The headline result overturns a fix made earlier this session.**

`Utility.ToType<T>` was changed from `Marshal.PtrToStructure(ptr, Type)` to
`Marshal.PtrToStructure<T>(ptr)` on the belief that the generic overload avoids
boxing. **It does not.** On .NET 6 / CoreCLR both overloads allocate exactly
`Unsafe.SizeOf<T>() + 16` bytes — the generic one boxes internally. Measured
side by side in a straight-line loop with no delegate boundary:

| | bytes/op | ns/op |
|---|---|---|
| `Marshal.PtrToStructure<NetMsg>(ptr)` **(generic — "the fix")** | **232.001** | 83.87 |
| `Marshal.PtrToStructure(ptr, typeof(NetMsg))` **(the old code)** | **232.001** | 27.91 |
| `Unsafe.Read<NetMsg>(void*)` **(proposed)** | **0** | 2.59 |

`Unsafe.SizeOf<NetMsg>()` is 216; 216 + 16 (object header) = 232. The same
identity holds for every type tested. The change was allocation-neutral and made
the small-struct case *slower*.

End to end through the library's own helper:

| | bytes/op | ns/op |
|---|---|---|
| `Utility.ToType<PersonaStateChange_t>(ptr)` (current) | **32.001** | 42.90 |
| `Unsafe.Read<PersonaStateChange_t>(ptr)` | **0** | 0.46 |

That is **93x faster and zero allocation** for the blittable case, which is
**222 of the 440** value types in the assembly.

Other headline costs, all measured:

| What | Cost | Where |
|---|---|---|
| One `GetQueryUGCResult` (workshop item) | **9,976 B + 5.44 µs** | 5 `ByValArray byte[]` fields = 9,670 B |
| One `SteamNetConnectionStatusChanged` delivery | **256 B + 2.87 µs** | non-blittable `ConnectionInfo` |
| One message through `SocketManager.Receive` | **232 B + 84 ns** | `PtrToStructure<NetMsg>` |
| One `SocketManager.Receive()` call | **58 ns** unmanaged alloc/free | `Marshal.AllocHGlobal` per call |
| `ConnectionInfo` through 3 call layers by value | 25.68 ns → **7.29 ns** with `in` | 696-byte struct |
| `Dispatch` per-frame bookkeeping | **0 B** | already fine |
| String marshalling | **at the theoretical floor** | already fine |

A 100-player dedicated server at 60 Hz receiving ~20 messages/player/second
(2,000 msg/s) pays **~464 KB/s of pure garbage** in `SocketManager.Receive`
alone, on a path whose client-side twin (`ConnectionManager.Receive`) is already
zero-allocation.

---

## Method

Steam is not installed and there is no Steam account, so **no native Steam
function was called**. To reach `internal` types without reflection overhead in
the measured region, the library sources were snapshotted to a scratch directory
and compiled *into* the harness assembly, so `Dispatch`, `Helpers`,
`Utf8StringPointer` etc. are directly callable. The snapshot compiles clean
(0 errors) against the same TFM (`net6.0`) and defines as the shipping project.

Harnesses (all in the session scratchpad, `.../scratchpad/harness/`):

| File | Measures |
|---|---|
| `Bench.cs` | `GC.GetAllocatedBytesForCurrentThread()` delta / iteration + `Stopwatch` ns/op. Warm-up 2,000, measure 20,000. |
| `Program.cs` | Struct-size census over every value type in the assembly (managed size via `Unsafe.SizeOf<T>`, native via `Marshal.SizeOf`, blittability via `GCHandle.Alloc(..., Pinned)`); callback delivery; dispatch pump shape; string marshalling; boxing checks. |
| `Precise.cs` | Re-run of the above with **typed static sinks**. |
| `Verify.cs` | Independent confirmation of the two headline claims in straight-line loops with **no delegate or closure boundary**, 200,000 iterations. |
| `Strings2.cs` | String marshalling with correct sinks. |
| `Ugc2.cs` | `SteamUGCDetails_t` marshalling cost. |

**Callback delivery** is measured by calling `Dispatch.Install<T>(handler)`, then
pulling the generated `Action<IntPtr>` out of `Dispatch`'s private `Callbacks`
dictionary by reflection **once**, and invoking that delegate in the timed loop.
The reflection is outside the measured region, so what is measured is exactly
what `Dispatch.ProcessCallback` invokes per delivered callback.

### Harness bug found and corrected — read this before trusting any "0 vs 24 B" claim

The first pass used `GC.KeepAlive(x)` as the sink. `GC.KeepAlive` takes `object`,
so **every value-type result was boxed**, adding `sizeof(T)+16` to each row. This
inflated e.g. `SteamId.AccountId` to "24 B/op" (really 0) and
`SteamNetConnectionStatusChangedCallback_t` delivery to "512 B/op" (really 256).
All numbers in this document come from the corrected runs (`Precise.cs`,
`Verify.cs`, `Strings2.cs`) which use typed static sinks. The correction is
called out here because the same mistake would make a re-measurement disagree.

---

## Findings

### F1 — `Utility.ToType<T>` still boxes; the generic overload is not the fix

- **Severity: HIGH** (highest cost×cheapness product in the audit)
- **Location:** `Facepunch.Steamworks/Utility/Utility.cs:30-36`, plus the doc
  comment at `:18-29` which is factually wrong on CoreCLR.
- **Called from:** `Classes/Dispatch.cs:303` (`action = x => p( x.ToType<T>() )`) —
  once per delivered callback **per registered handler** — and
  `Classes/Dispatch.cs:203` (`ProcessResult`), and
  `Networking/NetIdentity.cs:115`.

**Measured cost**

| Type | managed | blittable | `PtrToStructure<T>` | `PtrToStructure(ptr,Type)` | `Unsafe.Read<T>` |
|---|---|---|---|---|---|
| `PersonaStateChange_t` | 16 B | yes | **32.001 B** / 24.0 ns | 32.001 B | **0 B** / 0.23 ns |
| `NetMsg` | 216 B | yes | **232.001 B** / 83.9 ns | 232.001 B | **0 B** / 2.59 ns |
| `NetIdentity` | 136 B | yes | **152.01 B** / 48.0 ns | 152.01 B | **0 B** / 4.20 ns |
| `SteamNetConnStatusChanged` | 240 B | **NO** | **256.005 B** / 2,168 ns | 256.01 B | n/a |
| `ConnectionInfo` | 224 B | **NO** | **240.01 B** / 1,687 ns | 240.01 B | n/a |

**Why it costs that.** The .NET Core implementation of the generic overload
creates `object box = default(T)`, marshals into the box, then unboxes on
return. The `Type` overload does the same thing and hands the box back. The
allocation is identical to the byte; only the JIT's handling of the unbox
differs, which is why the generic form is measurably *slower* for small structs
(24.0 ns vs the Type overload's path) while allocating the same.

**Concrete fix.** Split the blittable and non-blittable cases. `Unsafe.Read<T>`
is only valid for blittable `T`; for the rest `PtrToStructure` must stay.

```csharp
// Utility.cs
static internal unsafe T ToType<T>( this IntPtr ptr ) where T : unmanaged
{
    if ( ptr == IntPtr.Zero ) return default;
    return System.Runtime.CompilerServices.Unsafe.Read<T>( (void*)ptr );
}

// keep a separate, differently-named path for the ~31 callback structs that
// carry [MarshalAs(ByValArray/ByValTStr)] fields and genuinely need marshalling:
static internal T ToTypeMarshalled<T>( this IntPtr ptr ) where T : struct
{
    if ( ptr == IntPtr.Zero ) return default;
    return Marshal.PtrToStructure<T>( ptr );
}
```

`where T : unmanaged` makes the compiler enforce the precondition, so a
non-blittable callback struct cannot accidentally take the fast path. The
generator already knows which structs have `ByValArray`/`ByValTStr` fields
(31 of 217 callback structs — list in F2), so `Dispatch.Install<T>` can be
generated to pick the right helper, or `Install` can be split into
`Install<T> where T : unmanaged` and `InstallMarshalled<T>`.

Cheaper interim variant if changing the constraint is too invasive: for
blittable `T`, `Unsafe.AsRef<T>(ptr)` avoids even the struct copy when the
handler only reads a few fields.

- **Breaking?** No. `ToType` is `internal`. Adding `where T : unmanaged` is a
  source-compatible tightening for all current call sites except the
  non-blittable callbacks, which move to the second helper.
- **Effort:** Small — one file plus a generator tweak for the split.
- **Also:** delete/replace the doc comment at `Utility.cs:18-29`; it asserts
  "The generic overload writes straight into the returned value with no boxing",
  which this audit disproves. Leaving it will cause the next person to skip the
  real fix.

---

### F2 — 31 callback structs are non-blittable; `ConnectionInfo` is the hot one

- **Severity: HIGH**
- **Location:** `Facepunch.Steamworks/Networking/ConnectionInfo.cs:8-23`,
  `Generated/SteamCallbacks.cs` (31 structs), `Generated/SteamStructs.cs:118+`.

**Measured.** 218 of 440 value types in the assembly are non-blittable.
Per delivered callback, corrected sinks:

| Callback | bytes/op | ns/op |
|---|---|---|
| `P2PSessionRequest_t` | 24 | 96.9 |
| `PersonaStateChange_t` | 32 | 99.1 |
| `LobbyChatUpdate_t` | 48 | 58.6 |
| `SteamNetworkingMessagesSessionRequest_t` | 152 | 90.2 |
| **`SteamNetConnectionStatusChangedCallback_t`** | **256** | **2,871** |
| `SteamNetAuthenticationStatus_t` (`ByValArray` 256) | 312 | 6,547 |
| `SteamRelayNetworkStatus_t` (`ByValArray` 256) | 320 | 5,508 |
| `SteamUGCQueryCompleted_t` (`ByValArray` 256) | 328 | 4,436 |

The 30x time gap between blittable (≈90 ns) and non-blittable (2.9–6.5 µs)
deliveries is the marshalling stub walking the field list.

**Why.** `ConnectionInfo` (696 bytes native) carries:

```csharp
[MarshalAs( UnmanagedType.ByValTStr, SizeConst = 128 )]
internal string endDebug;
[MarshalAs( UnmanagedType.ByValTStr, SizeConst = 128 )]
internal string connectionDescription;
```

Two managed `string` fields make the whole struct non-blittable, which makes
`SteamNetConnectionStatusChangedCallback_t` (which embeds it) non-blittable too.
Every delivery allocates the box plus up to two strings.

**Concrete fix.** Replace the marshalled `string` fields with fixed buffers and
decode on demand — the pattern the generator *already uses* elsewhere in
`SteamStructs.cs` (`TitleUTF8()`, `DescriptionUTF8()`):

```csharp
[StructLayout( LayoutKind.Sequential, Size = 696 )]
public unsafe struct ConnectionInfo
{
    ...
    private fixed byte endDebugRaw[128];
    private fixed byte connectionDescriptionRaw[128];

    public string EndDebug            { get { fixed ( byte* p = endDebugRaw ) return Helpers.MemoryToString( (IntPtr)p ); } }
    public string ConnectionDescription { get { fixed ( byte* p = connectionDescriptionRaw ) return Helpers.MemoryToString( (IntPtr)p ); } }
}
```

This makes `ConnectionInfo` blittable, which makes
`SteamNetConnectionStatusChangedCallback_t` blittable, which lets both take the
F1 fast path: **256 B + 2,871 ns → 0 B + ~5 ns**, and the strings are only built
if someone reads them (most frames, nobody does).

- **Breaking?** `endDebug`/`connectionDescription` are `internal`, so the raw
  fields are free to change. If any public surface exposes them as `string`,
  keep the property name and change only the backing store — then it is not
  breaking. Adding `unsafe` to the struct is invisible to callers.
- **Effort:** Medium — a generator change plus `ConnectionInfo`. Do
  `ConnectionInfo` first; it is the only one on a per-connection-event path.

The other 30 non-blittable callbacks (`SteamRelayNetworkStatus_t`,
`SteamNetAuthenticationStatus_t`, `SteamUGCQueryCompleted_t`,
`RemoteStorage*`, …) fire rarely; converting them is the same mechanical change
but far lower value.

---

### F3 — `SocketManager.Receive` is the un-optimised twin of `ConnectionManager.Receive`

- **Severity: HIGH** (this is the *dedicated-server* receive path)
- **Location:** `Facepunch.Steamworks/Networking/SocketManager.cs:141-184`
  (re-verified against the working tree after the concurrent edits to this file —
  `Receive` is unchanged and this finding stands)

A previous pass made the client path (`ConnectionManager.Receive`,
`Networking/ConnectionManager.cs:111-152`) zero-allocation with `stackalloc` and
raw `NetMsg*`. The server path was left behind and still has three problems:

```csharp
public int Receive( int bufferSize = 32, bool receiveToEnd = true )
{
    int processed = 0;
    IntPtr messageBuffer = Marshal.AllocHGlobal( IntPtr.Size * bufferSize );   // (1) per call
    ...
            ReceiveMessage( Marshal.ReadIntPtr( messageBuffer, i * IntPtr.Size ) );
    ...
    if ( receiveToEnd && processed == bufferSize )
        processed += Receive( bufferSize );                                     // (3) recursion
}

internal unsafe void ReceiveMessage( IntPtr msgPtr )
{
    var msg = Marshal.PtrToStructure<NetMsg>( msgPtr );                          // (2) per message
```

**Measured**

| | cost |
|---|---|
| (1) `Marshal.AllocHGlobal(256)` + `FreeHGlobal`, per `Receive()` | **58.25 ns** |
| `stackalloc IntPtr[32]` (what `ConnectionManager` does) | **11.79 ns** |
| (2) `Marshal.PtrToStructure<NetMsg>`, **per message** | **232 B + 83.9 ns** |
| `Unsafe.Read<NetMsg>` | 0 B + 2.59 ns |
| `((NetMsg*)p)->field` (no copy at all) | 0 B + 0.23 ns |

(3) The recursion re-enters `Receive`, so a busy socket pays the `AllocHGlobal`
again per 32 messages and grows the stack instead of looping.

**Scale.** 100-player server, 20 msg/player/s = 2,000 msg/s →
2,000 × 232 B = **464 KB/s of garbage**, plus 2,000 × 84 ns = 168 µs/s of
marshalling, plus ~63 `AllocHGlobal`/`FreeHGlobal` pairs per second.

**Concrete fix** — mirror `ConnectionManager.Receive` exactly:

```csharp
public unsafe int Receive( int bufferSize = 32, bool receiveToEnd = true )
{
    if ( bufferSize < 1 || bufferSize > 256 ) throw new ArgumentOutOfRangeException( nameof( bufferSize ) );

    int totalProcessed = 0;
    NetMsg** messageBuffer = stackalloc NetMsg*[bufferSize];

    while ( true )
    {
        int processed = SteamNetworkingSockets.Internal.ReceiveMessagesOnPollGroup(
            pollGroup, new IntPtr( &messageBuffer[0] ), bufferSize );
        totalProcessed += processed;

        try
        {
            for ( int i = 0; i < processed; i++ )
                ReceiveMessage( ref messageBuffer[i] );
        }
        catch
        {
            for ( int i = 0; i < processed; i++ )
                if ( messageBuffer[i] != null ) NetMsg.InternalRelease( messageBuffer[i] );
            throw;
        }

        if ( !receiveToEnd || processed < bufferSize ) break;
    }
    return totalProcessed;
}

internal unsafe void ReceiveMessage( ref NetMsg* msg )
{
    try
    {
        OnMessage( msg->Connection, msg->Identity, msg->DataPtr, msg->DataSize,
                   msg->MessageNumber, msg->RecvTime, msg->Channel );
    }
    finally { NetMsg.InternalRelease( msg ); msg = null; }
}
```

- **Breaking?** `Receive`'s signature is unchanged. `ReceiveMessage` is
  `internal`. Not breaking.
- **Effort:** Small — the reference implementation is 40 lines away in the same
  folder.

**Correctness note spotted while measuring** (not a perf issue, but it is in the
lines being replaced): `SocketManager.cs:175` passes
`msg.RecvTime, msg.MessageNumber` into a method declared
`OnMessage( ..., long messageNum, long recvTime, int channel )` at
`SocketManager.cs:186` — the two arguments are swapped relative to the parameter
names. `ConnectionManager.cs:256` has the same ordering. Worth confirming which
is intended before rewriting the call.

---

### F4 — 136-byte and 696-byte structs passed by value through every callback layer

- **Severity: MEDIUM** (cheap fix, real but bounded win)
- **Location:** `Networking/SocketManager.cs:46, 89, 104, 114, 171`,
  `Networking/ConnectionManager.cs:45, 90, 98, 106, 268`,
  `Networking/ISocketManager.cs`, `Networking/IConnectionManager.cs`

`ConnectionInfo` is **696 bytes** and `NetIdentity` is **136 bytes**. Both travel
by value through 3–4 frames per event:

`OnConnectionChanged(conn, info)` → `OnConnected(conn, info)` →
`Interface.OnConnected(conn, info)`, and on the message path
`ReceiveMessage` → `OnMessage(conn, identity, …)` → `Interface.OnMessage(…)`.

**Measured** (`ConnectionInfo`, 3 non-inlined layers, 0 allocation either way):

| | ns/op |
|---|---|
| by value | **25.68** |
| by `in` | **7.29** |

**3.5x**, ~18 ns saved per event. Connection-state changes are not per-frame, so
this is worth doing mainly because it is nearly free — but `NetIdentity` on
`SocketManager.OnMessage` **is** per-message: 136 bytes copied 2–3 times per
received packet, i.e. ~816 KB/s of stack traffic at 2,000 msg/s.

**Concrete fix**, and the trap that comes with it:

```csharp
public virtual void OnConnectionChanged( Connection connection, in ConnectionInfo info )
public virtual void OnMessage( Connection connection, in NetIdentity identity, IntPtr data, ... )
```

**`in` alone will make this slower unless the properties are also marked
`readonly`.** `ConnectionInfo` is a mutable struct, so `info.State` on an `in`
parameter forces the compiler to emit a **defensive copy of all 696 bytes**
before every property access. Mark the members `readonly`:

```csharp
public readonly ConnectionState State => state;
public readonly NetAddress      Address => address;
public readonly NetIdentity     Identity => identity;
public readonly NetConnectionEnd EndReason => (NetConnectionEnd)endReason;
```

Same for `NetAddress.Port` and `NetIdentity.IsSteamId` / `IsIpAddress`.

- **Breaking?** **Yes** — changing a `virtual` method's signature breaks
  subclasses, and changing an interface method breaks implementors. Since
  `SocketManager`/`ConnectionManager` are designed to be derived from, this
  is a real break. Adding `readonly` to the properties is **not** breaking and
  should be done unconditionally — it is a pure win even without `in`.
  If the `in` change is wanted without a break, add overloads and have the
  by-value ones forward, marking them `[Obsolete]` for a release.
- **Effort:** Small for `readonly`; medium for `in` because of the break.

---

### F5 — `SteamUGCDetails_t`: 9,976 bytes allocated per workshop item

- **Severity: MEDIUM-HIGH** (not per-frame, but enormous per operation)
- **Location:** `Generated/SteamStructs.cs:118-158`,
  `Generated/Interfaces/ISteamUGC.cs:86-89`, `Structs/UgcResultPage.cs:26-31`

**Measured:** `Marshal.PtrToStructure<SteamUGCDetails_t>` =
**9,976 B/op, 5,443 ns/op**.

**Why.** The struct has five `[MarshalAs(UnmanagedType.ByValArray)] byte[]`
fields:

| field | `SizeConst` |
|---|---|
| `Title` | 129 |
| `Description` | **8000** |
| `Tags` | 1025 |
| `PchFileName` | 260 |
| `URL` | 256 |
| | **9,670 B** |

Five fresh `byte[]` are allocated on **every** marshal, regardless of content —
9,670 B of arrays + 5 array headers + the box ≈ the measured 9,976 B.
`GetQueryUGCResult` takes `ref SteamUGCDetails_t`, and `ref` on a non-blittable
struct is `[In,Out]`, so this marshalling happens **in both directions** per call.

A 50-item workshop page therefore allocates **~500 KB** in these arrays alone,
before the other ~19 native calls per item catalogued in F7.

Good news: the *managed* struct is only 152 bytes, so `Item.From( details )`
passing it by value costs **10.6 ns** and copies 152 B, not 9,784 B. The
brief's concern about a 9,784-byte by-value copy does not apply — the cost is
marshalling, not copying.

**Concrete fix.** Convert the five `byte[]` fields to `fixed byte` buffers, as
the generator already does for other structs, making `SteamUGCDetails_t`
blittable:

```csharp
internal unsafe struct SteamUGCDetails_t
{
    ...
    internal fixed byte Title[129];
    internal fixed byte Description[8000];
    internal fixed byte Tags[1025];
    internal fixed byte PchFileName[260];
    internal fixed byte URL[256];

    internal string TitleUTF8() { fixed ( byte* p = Title ) return Helpers.MemoryToString( (IntPtr)p ); }
    ...
}
```

`GetQueryUGCResult(handle, i, ref details)` then becomes a straight pin-and-pass
with no marshalling: **9,976 B → 0 B**, 5.44 µs → ~0.

- **Breaking?** The struct is `internal`; the `*UTF8()` accessor names are
  already the public-facing shape and are preserved. `Ugc.Item` (328 B, returned
  by value from `Item.From`) is unaffected.
- **Effort:** Medium — generator change; must be validated against
  `verify-struct-layout.ps1` since `fixed byte[N]` and
  `[MarshalAs(ByValArray, SizeConst=N)]` have identical native layout but the
  managed layout changes.

---

### F6 — `2N+1` native calls in ten enumerators: the count is re-fetched every iteration

- **Severity: MEDIUM** (trivial fix, doubles the transition count on user-facing lists)
- **Locations** (all the same shape — the loop *condition* is a native call):

| File:line | Loop condition (native, per iteration) | Body (native) |
|---|---|---|
| `SteamFriends.cs:140` | `Internal.GetFriendCount( (int)flag )` | `GetFriendByIndex` |
| `SteamFriends.cs:190` | `Internal.GetCoplayFriendCount()` | `GetCoplayFriend` |
| `SteamFriends.cs:198` | `Internal.GetFriendCountFromSource( steamid )` | `GetFriendFromSourceByIndex` |
| `SteamFriends.cs:206` | `Internal.GetClanCount()` | `GetClanByIndex` |
| `Structs/Lobby.cs:64` | `MemberCount` → `GetNumLobbyMembers` | `GetLobbyMemberByIndex` |
| `Structs/Clan.cs:44` | `GetClanOfficerCount( Id )` | `GetClanOfficerByIndex` |
| `SteamUserStats.cs:112` | `Internal.GetNumAchievements()` | `GetAchievementName` |
| `SteamApps.cs:124` | `Internal.GetDLCCount()` | `BGetDLCDataByIndex` |
| `SteamParties.cs:58` | `ActiveBeaconCount` → `GetNumActiveBeacons` | `GetBeaconByIndex` |
| `SteamRemoteStorage.cs:179` | `FileCount` → `Internal.GetFileCount()` | `GetFileNameAndSize` |

`SteamFriends.cs:138-144` backs **six** public APIs (`GetFriends`, `GetBlocked`,
`GetFriendsRequested`, `GetFriendsClanMembers`, `GetFriendsOnGameServer`,
`GetFriendsRequestingFriendship`):

```csharp
for ( int i=0; i<Internal.GetFriendCount( (int)flag); i++ )
{
    yield return new Friend( Internal.GetFriendByIndex( i, (int)flag ) );
}
```

**Cost:** `2N+1` transitions instead of `N+1`. 300 friends = **601** instead of
301. A 32-player lobby's `Members` = **65** instead of 33. Exactly 2x on every
one of these.

Because they are lazy `IEnumerable`s, **every re-enumeration re-pays in full** —
`.Count()`, `.Any()`, `.ToList()` or a second `foreach` doubles it again.

**Concrete fix** — hoist:

```csharp
int count = Internal.GetFriendCount( (int)flag );
for ( int i = 0; i < count; i++ )
    yield return new Friend( Internal.GetFriendByIndex( i, (int)flag ) );
```

- **Breaking?** No. Behaviour changes only if the list mutates mid-enumeration,
  which the current code handles by accident, not by design (and inconsistently
  — `Structs/Lobby.cs:106`, `SteamMatchmaking.cs:177`, `SteamInput.cs:44` and
  `SteamApps.cs:184` already hoist).
- **Effort:** Trivial — ten one-line changes.

**The model to copy** is already in the codebase: `SteamUgc.cs:168-178`,
`SteamApps.cs:178-190` and `SteamInput.cs:40-51` do **one bulk native call**
then loop a managed array — 1 transition total.

---

### F7 — property-per-native-call structs: `Friend`, `Ugc.Item`, `NetAddress`

- **Severity: MEDIUM**
- **Locations:** `Structs/Friend.cs`, `Structs/Item.cs`, `Networking/NetAddress.cs`,
  `Structs/Clan.cs`, `Structs/Achievement.cs`

`Friend` has **15** native-calling properties with no caching. Reading five
fields for one friends-list row (`Name`, `State`, `Relationship`, `SteamLevel`,
`IsPlayingThisGame`) = **6 transitions per friend**. Combined with F6, populating
a 300-friend list = **601 + 1,800 ≈ 2,400 transitions**.

Worst individual offenders:

- `Structs/Friend.cs:43` — `IsPlayingThisGame` evaluates the `GameInfo` getter
  **twice** (`GameInfo?.GameID` then `GameInfo.Value.GameID`), so **2**
  `GetFriendGamePlayed` calls where a local would make it 1.
- `Structs/Friend.cs:48,62,67,72` — `IsOnline`/`IsAway`/`IsBusy`/`IsSnoozing`
  each re-fetch `GetFriendPersonaState`. A UI reading all four = 4 transitions
  for one value.
- `Structs/Item.cs:121-125` — five `ItemState` flag properties each re-fetch the
  **same** bitmask; `DownloadAmount` (`:210`) costs **up to 5**. A mod-manager
  row = **10-14 transitions** where `GetItemState` + `GetItemDownloadInfo` (2)
  covers everything.
- `Networking/NetAddress.cs:92,104,116,128,140` — five properties make native
  calls to inspect bytes that are **already in the struct** at offset 0
  (`IPV4 ip`). `Port` (`:29`) correctly reads the field directly; the rest could too.
- `Structs/Friend.cs:19-22` — `ToString()` calls `GetFriendPersonaName`. Any
  `Debug.Log(friend)`, string interpolation, or debugger inspection of a
  `List<Friend>` triggers a native call per element.

**Concrete fix** — the codebase already contains the right pattern.
`Connection.QuickStatus()` (`Networking/Connection.cs:153-160`) makes **one**
`GetConnectionRealTimeStatus` call and returns a `ConnectionStatus` struct whose
ten properties are all free field reads. Give `Friend` and `Ugc.Item` the same:

```csharp
// additive - does not touch any existing member
public struct FriendSnapshot
{
    public string Name; public FriendState State; public Relationship Relationship;
    public int SteamLevel; public FriendGameInfo? GameInfo;
}
public FriendSnapshot Snapshot()      // 4 native calls, once
{
    ...
}
```

and for `Ugc.Item`:

```csharp
public struct ItemStateSnapshot { public ItemState State; public ulong BytesDownloaded, BytesTotal; }
public ItemStateSnapshot QueryState()   // GetItemState + GetItemDownloadInfo = 2 calls
```

- **Breaking?** No — purely additive. Leave the existing properties alone.
- **Effort:** Medium. Highest value on `Friend` and `Ugc.Item`.

Two incidental defects surfaced while tracing these (not performance):
`Networking/NetIdentity.cs:40-46` — `IsLocalHost` tests a `default` identity
instead of `this` (compare the correct `NetAddress.cs:130`); and
`Structs/InventoryDef.cs:204` — `LocalBasePriceFormatted` formats `LocalPrice`,
not `LocalBasePrice`.

---

### F8 — per-message allocation on the two legacy receive overloads

- **Severity: MEDIUM**
- **Locations:** `SteamNetworkingMessages.cs:131-147`, `SteamNetworking.cs:90-112`

```csharp
// SteamNetworkingMessages.cs:135
byte[] data = new byte[msg->DataSize];
Marshal.Copy( msg->DataPtr, data, 0, msg->DataSize );
callback( msg->Identity.SteamId, msg->Channel, data );
//         ^^^^^^^^^^^^^^^^^^^^ this property is a NATIVE CALL (NetIdentity.cs:82)
```

One heap array **per received message**, plus a native transition per message
just to extract the `SteamId` from a `NetIdentity` the code already holds.

`SteamNetworking.ReadP2PPacket` (`SteamNetworking.cs:103-105`) does a
**double copy**: native → `Helpers.TakeBuffer` (a shared 256 KB array) →
`new byte[size]`, one heap array per packet.

**Mitigation already present:** `SteamNetworkingMessages.ReceiveMessagesOnChannel(
int, MessageIntercept, …)` (`:158+`) is documented zero-allocation and uses
`stackalloc` + raw `NetMsg*`. That is the right shape.

**Concrete fix.** Add caller-buffer overloads next to the allocating ones:

```csharp
// SteamNetworking
public static bool ReadP2PPacket( Span<byte> destination, out int written, out SteamId from, int channel = 0 )
```

and for `SteamNetworkingMessages`, promote the `MessageIntercept` overload in
the docs/README so the `byte[]` one is understood as the convenience path. Also
hoist the identity out of the loop rather than going native per message.

- **Breaking?** No — additive overloads.
- **Effort:** Small.

---

### F9 — `Dispatch.CallbackToString`: 544 B + 2 µs per callback when the debug hook is set

- **Severity: LOW-MEDIUM** (opt-in path, but the cost is much worse than the docs imply)
- **Location:** `Classes/Dispatch.cs:166-196`, invoked from `:133`

**Measured:** 544 B/op, 1,960 ns/op for the *smallest* callback
(`PersonaStateChange_t`), including ~100 B of `MethodInfo.Invoke` harness
overhead. The method does, per delivered callback:

```csharp
var strct = data.ToType( t );                    // boxing PtrToStructure
var fields = t.GetFields( ... );                 // FieldInfo[] alloc, no cache
var columnSize = fields.Max( x => x.Name.Length ) + 1;   // LINQ + delegate
foreach ( var field in fields )
    str += $"{new String( ' ', spaces )}{field.Name}: {field.GetValue( strct )}\n";
    //  ^^ string += in a loop   ^^ new string per field   ^^ boxes every field value
```

`Dispatch.cs:192` is the single densest allocation line in the library:
quadratic string concatenation, an interpolation, a `new String(char,int)`, and
a `FieldInfo.GetValue` box — all per field, per callback.

The `?.Invoke` at `:133` **does** correctly short-circuit — measured **0 B,
1.6 ns** when `OnDebugCallback` is null, so this costs nothing when unused. The
existing doc comment ("This is SLOW!!") is accurate; it just understates by how
much.

**Concrete fix** (only worth it if anyone runs with this on): cache
`FieldInfo[]` and `columnSize` per type in a `static Dictionary<Type, …>`, and
build into a reusable `StringBuilder` instead of `str +=`. Would take it to
roughly one string allocation per callback.

- **Breaking?** No. **Effort:** Small.
- Minor: `Dispatch.cs:219` uses `$"[no callback waiting/required]"` — an
  interpolated string with no holes. Drop the `$`.

---

### F10 — `Enum.HasFlag` on a field receiver allocates 48 B, even on .NET 6

- **Severity: LOW** (tiny fix, but a real per-call cost on Unity)
- **Location:** `Structs/InventoryItem.cs:34, 40, 46`

```csharp
public bool IsNoTrade  => _flags.HasFlag( SteamItemFlags.NoTrade );
public bool IsRemoved  => _flags.HasFlag( SteamItemFlags.Removed );
public bool IsConsumed => _flags.HasFlag( SteamItemFlags.Consumed );
```

**Measured, and the nuance matters:**

| receiver | bytes/op |
|---|---|
| local variable, direct loop | **0** (JIT expands to a bit test) |
| **field** (closure field), 20k iterations | **48** |
| **field**, after 400,000 warm-up iterations | **48** |

The 400k warm-up rules out JIT tiering as the explanation — with a **field**
receiver the CoreCLR expansion does not fire and both operands box (2 × 24 B).
`InventoryItem._flags` is an instance field, i.e. exactly the shape that boxes.

**Concrete fix:**

```csharp
public bool IsNoTrade  => ( _flags & SteamItemFlags.NoTrade ) != 0;
```

Measured at **0 B, 0.53 ns** vs 48 B. Under IL2CPP/Mono the expansion does not
exist at all, so this is unconditionally correct there.

- **Breaking?** No. **Effort:** Trivial — three lines.

---

### F11 — `Dispatch.LoopClientAsync` allocates a `Task` per pumped frame

- **Severity: LOW**
- **Location:** `Classes/Dispatch.cs:236-243, 250-257`

```csharp
internal static async void LoopClientAsync()
{
    while ( ClientPipe != 0 ) { Frame( ClientPipe ); await Task.Delay( 16 ); }
}
```

**Measured:** `Task.Delay(16)` object graph = **168 B**. At ~62 iterations/s that
is **~10 KB/s** for the client loop plus ~5 KB/s for the server loop (`:255`,
32 ms). Small in absolute terms, but it is continuous, unavoidable-by-the-user
background garbage in a library that advertises being allocation-conscious — and
Unity users typically call `SteamClient.RunCallbacks()` from `Update()` instead,
making this pure waste when it is running.

Also `async void`: an exception escaping `Frame` cannot be observed by the
caller. `Frame` catches broadly (`:110`) so this is mostly theoretical.

**Concrete fix:** use a `PeriodicTimer` (net6+) or a plain `Thread` with
`Thread.Sleep`, and gate the loop behind an opt-in flag so hosts that pump
manually never start it.

- **Breaking?** No — both methods are `internal`.
- **Effort:** Small.

---

### F12 — `ServerList` and `Leaderboard` poll loops

- **Severity: LOW-MEDIUM** (bounded to specific operations, but very high transition counts)
- **Locations:** `ServerList/Base.cs:74-102, 174-204`, `Structs/Leaderboard.cs:140-157`

`Leaderboard.WaitForUserNames` polls **every entry** at **1 ms** until all
resolve, and never removes entries that already resolved:

```csharp
while ( !gotAll )
{
    gotAll = true;
    foreach ( var entry in entries )
    {
        if ( entry.User.Id == 0 ) continue;
        if ( !SteamFriends.Internal.RequestUserInformation( entry.User.Id, true ) ) continue;
        gotAll = false;
    }
    await Task.Delay( 1 );
}
```

A 100-entry board resolving in 2 s ≈ **200,000** `RequestUserInformation`
transitions plus 2,000 `Task.Delay` allocations (≈336 KB), for work that needs
~100 calls. **Fix:** remove resolved entries from the poll set and raise the
delay to ~50 ms.

`ServerList/Base.cs:176,197` allocates a capturing closure + `Predicate<int>`
per 33 ms tick and re-polls the whole watchlist: `2 + W + R` transitions per
tick, ≈150,000/s for a 5,000-server internet query. **Fix:** hoist the
predicate to a cached field (it captures only `this`), and consider a lower poll
rate.

- **Breaking?** No. **Effort:** Small.

---

### F13 — `ServerList.Base.GetFilters()` allocates per query, and `Helpers` locks

- **Severity: LOW**
- **Locations:** `ServerList/Base.cs:120`, `Utility/Helpers.cs:24, 68`, `Utility/Utility.cs:121`

`internal virtual MatchMakingKeyValuePair[] GetFilters() => filters.ToArray();`
— a fresh array per query launch, called from all five subclasses.
`MatchMakingKeyValuePair` measures **512 bytes native / 16 managed** and is
non-blittable, so this array is also a marshalling cost. Low frequency; fix by
caching the array and invalidating on `AddFilter`.

`Helpers.Memory.Take()` (`Helpers.cs:24`) takes a `lock` per call: measured
**0 B but 59.94 ns**. Every string-returning generated method pays it once
(twice for two-out-param methods like `GetLobbyDataByIndex`). Replacing the
`Queue<IntPtr>` + `lock` with a `[ThreadStatic]` buffer removes the contention
and most of the 60 ns. See also the thread-safety notes in the
[verified-acceptable](#verified-acceptable) section.

---

## Verified-acceptable

Measured, and genuinely fine. Do not spend time here.

**The dispatch pump's own bookkeeping is allocation-free.**

| operation | bytes/op | ns/op |
|---|---|---|
| `Dictionary<CallbackType, List<Callback>>.TryGetValue` (enum key) | **0** | 13.4 |
| full `ProcessCallback` shape, 3 handlers | **0** | 41.7 |
| `ProcessResult` shape (lookup + remove + add) | **0** | 30.6 |
| `OnDebugCallback?.Invoke(..)` with null hook | **0** | 1.6 |

- The `actionsToCall` pattern (`Dispatch.cs:126, 144-159`) is correct: the list is
  a reused static, `Clear()` does not free the backing array, and `foreach` over
  `List<T>` uses the struct enumerator. **0 B.** The extra copy costs ~26 ns vs
  invoking directly (41.7 vs 15.0) — real, but it buys re-entrancy safety, which
  is a fair trade.
- **Enum-keyed `Dictionary` does not need a custom comparer** on CoreCLR:
  `EqualityComparer<CallbackType>.Default` resolves to a devirtualised
  `EnumEqualityComparer`; lookup measured **0 B**. (IL2CPP: see below.)
- `OnDebugCallback?.Invoke( msg.Type, CallbackToString(...), isServer )` at
  `Dispatch.cs:133` correctly short-circuits — the expensive argument is **not**
  evaluated when the hook is null. Verified at 0 B / 1.6 ns with a deliberately
  expensive argument expression.
- `ICallbackData` is an interface implemented by structs, but the constrained
  generic call in `Dispatch.Install<T>` (`:292-293`,
  `var t = default(T); var type = t.CallbackType;`) does **not** box on CoreCLR —
  `constrained. callvirt` dispatches directly. Measured 0 B. **No boxing at any
  `ICallbackData` call site in the library**; there are no `ICallbackData`-typed
  locals, fields or parameters anywhere (only generic constraints and
  `struct X : ICallbackData` declarations).

**String marshalling is at the theoretical floor.** This was the biggest
surprise — the buffer scheme is good.

| operation | bytes/op | ns/op |
|---|---|---|
| `Helpers.TakeMemory()` + `Dispose()` | **0** | 59.9 |
| `new Utf8StringToNative(18 chars)` + `Dispose()` — every string **argument** | **0** | 135.0 |
| `Helpers.MemoryToString` → 8-char string | 40 | 58.7 |
| `Helpers.MemoryToString` → 32-char string | 88 | 77.3 |
| `Helpers.MemoryToString` → 128-char string | 280 | 257.9 |
| generated 1-string method (24-char result) | 72 | 115.2 |
| generated 2-string method (`GetLobbyDataByIndex`) | 144 | 254.2 |
| `Utf8StringPointer` → `string` (14 chars) | 56 | 36.7 |

- **No double copy.** `MemoryToString` costs exactly the resulting `string` and
  nothing more (40 B for 8 chars = 22 B header + 16 B chars, rounded). The
  32 KB pooled buffer is reused, not reallocated.
- `Utf8StringToNative` (string → native) allocates **zero managed bytes**; it
  uses `AllocHGlobal` and writes UTF-8 directly with `fixed`. Correct.
- **`Helpers.Memory` is thread-safe.** 4 threads × 20,000 concurrent
  `Take()`/`Dispose()` produced **0 double-handouts** — the `lock` correctly
  gives each caller exclusive ownership.

Remaining string caveats, both minor:
- The `lock` costs ~60 ns/call (F13); `[ThreadStatic]` would remove it.
- `Memory.Take()` zeroes only byte 0 (`Helpers.cs:28`). That is sufficient —
  a failed native call leaves byte 0 as NUL and `MemoryToString` returns `""`.
  But the scan is bounded only by `MemoryBufferSize`: a buffer with no NUL in
  32 KB produces a 32 KB string in **27 µs**. Native callees always terminate,
  so this is defensive-only.
- **`Helpers.TakeBuffer` (`Helpers.cs:66-85`) and
  `Utility.ReadNullTerminatedUTF8String` (`Utility.cs:119-133`) are *not*
  equivalent to `Memory`.** `TakeBuffer` returns a **shared** `byte[]` from a
  4-slot ring; the `lock` guards only slot selection, not the caller's
  subsequent use. `ReadNullTerminatedUTF8String` locks a single static 8 KB
  buffer, serialising all callers. Neither is on a per-frame path today, but
  neither is safe to move onto one.

**Value-type API surface is clean.**

| operation | bytes/op | ns/op |
|---|---|---|
| `SteamId.AccountId` / `.IsValid` | **0** | 2.4 / 2.9 |
| `Connection.GetHashCode()` / `.Equals(Connection)` | **0** | 4.1 / 6.5 |
| `HashSet<Connection>.Contains` (3x per state change) | **0** | 5.2 |
| `ConnectionInfo.State` (by value) | **0** | 3.4 |
| `ConnectionInfo.Identity` (returns 136 B) | **0** | 7.2 |
| `Utility.IpToInt32(IPAddress)` | **0** | 4.0 |
| `Ugc.Query` 4-step builder chain | **0** | 61.5 |

- **The `Ugc.Query` builder is fine.** It is a 192-byte struct returned by value
  from each step, but the whole chain allocates **0 bytes** — the copies stay on
  the stack. Only `WithFileId(params PublishedFileId[])`
  (`Structs/UgcQuery.cs:101`) allocates, and only the `params` array (48 B for
  3 ids), which is inherent to `params`. Add a `ReadOnlySpan<PublishedFileId>`
  overload if that ever matters; it does not today.
- `Connection` implements `IEquatable<Connection>` and a typed `GetHashCode`, so
  `HashSet<Connection>` does not box. Correct.
- `ConnectionManager.Receive` (`Networking/ConnectionManager.cs:111-152`) and
  `SteamNetworkingMessages.ReceiveMessagesOnChannel(int, MessageIntercept, …)`
  are already zero-allocation and use the right shape. They are the templates
  for F3.
- `SteamNetworkingUtils.OutputDebugMessages()` (`:385-394`), called from
  `Dispatch.Frame` every frame, early-outs on `debugMessages.IsEmpty`. Correct —
  no per-frame cost when no debug output is configured.
- `SteamNetworkingSockets`' `BroadcastBufferManager` pooling and
  `ConnectionManager.SendMessages`' `stackalloc` + refcounted unmanaged buffer
  are well built.
- **No `foreach` over an interface-typed variable anywhere in the library** —
  every internal `foreach` iterates a concrete `T[]`, `List<T>`, `HashSet<T>` or
  `Dictionary<,>`, all struct enumerators. (Callers of the public
  `IEnumerable<T>` APIs do pay interface dispatch — that is F6/F14.)
- **No `string.Format` anywhere.** Interpolation is used, but outside
  `Dispatch.CallbackToString` (F9) and `BroadcastBufferManager`'s debug logging
  it is confined to throw paths and URL properties.

---

## Needs runtime proof

1. **Everything about IL2CPP / Mono.** All numbers here are CoreCLR. Under
   IL2CPP the following are expected to be *worse* and must be re-measured on
   device:
   - `Marshal.PtrToStructure<T>` — the boxing is in the BCL, so F1 holds, but
     the relative cost of `Unsafe.Read<T>` differs.
   - `Enum.HasFlag` — Mono has no JIT expansion; F10's 48 B is the *floor*, and
     it will apply to local receivers too.
   - Enum-keyed `Dictionary<CallbackType, …>` — older Mono lacks the
     devirtualised `EnumEqualityComparer` and falls back to
     `ObjectEqualityComparer`, boxing both key and stored key on every lookup.
     That would put a boxed allocation in `ProcessCallback` on the hot path.
     **This is the single most important thing to verify on device.** If it
     boxes, the fix is a `struct CallbackTypeComparer : IEqualityComparer<CallbackType>`
     passed to the dictionary constructor at `Dispatch.cs:285`.
   - Reverse P/Invoke delegate marshalling for `NetDebugFunc`
     (`SteamNetworkingUtils.cs:343`) and the `ISteamMatchmaking*Response`
     callback classes — IL2CPP requires `[MonoPInvokeCallback]` (present) and
     the delegates must be rooted (they are, `_debugFunc` at `:357`).
2. **Actual callback frequency in a live session.** The per-callback costs are
   exact, but the multiplier — how many `SteamNetConnectionStatusChanged` and
   friends actually arrive per second on a 100-player server — is a guess. The
   throughput numbers in F3 assume 20 msg/player/s.
3. **Native transition latency.** Only the managed side of each interop call was
   measured. The `2N+1` findings (F6) count transitions, not wall time; the
   actual saving depends on the cost of a `steam_api64.dll` call, which needs a
   live measurement.
4. **`ref` marshalling direction for `SteamUGCDetails_t`.** F5 measures
   `PtrToStructure`, which is the in-bound half. Whether the CLR also performs
   the out-bound marshal for `ref` on this specific signature should be
   confirmed with a live call before claiming 2x.
5. **Whether the `ConnectionInfo` `fixed byte` conversion preserves native
   layout** — `verify-struct-layout.ps1` exists in the repo and should gate that
   change.

---

## Prioritised optimisation backlog

Ranked by (cost per second in a realistic workload) × (cheapness of fix).

| # | Finding | Fix effort | Payoff |
|---|---|---|---|
| 1 | **F1** `ToType<T>` → `Unsafe.Read<T>` for blittable `T` | Small | 32–232 B **and** 24–84 ns per callback per handler → 0 B / 0.5 ns. Fixes a change that was believed done. |
| 2 | **F3** `SocketManager.Receive` → mirror `ConnectionManager.Receive` | Small | ~464 KB/s on a 100-player server → 0. Reference implementation already in the repo. |
| 3 | **F10** `HasFlag` → bitwise, 3 lines | Trivial | 48 B → 0 per call; unconditionally correct on IL2CPP. |
| 4 | **F6** hoist the count in 10 enumerators | Trivial | Halves interop transitions on every user-facing list. |
| 5 | **F4a** add `readonly` to `ConnectionInfo` / `NetAddress` / `NetIdentity` properties | Trivial | Prevents 696-byte defensive copies; prerequisite for F4b. Not breaking. |
| 6 | **F2** make `ConnectionInfo` blittable (`fixed byte` for the two `ByValTStr`) | Medium | 256 B + 2,871 ns → ~0 per connection-status callback. Unlocks F1's fast path for the hottest networking callback. |
| 7 | **F5** make `SteamUGCDetails_t` blittable | Medium | 9,976 B + 5.4 µs → 0 per workshop item; ~500 KB per 50-item page. |
| 8 | **F12** `Leaderboard.WaitForUserNames` — drop resolved entries, raise delay | Small | ~200,000 transitions → ~100. |
| 9 | **F8** `Span<byte>` receive overloads for `ReadP2PPacket` / messages | Small | One heap array per packet → 0, additive. |
| 10 | **F7** `Friend.Snapshot()` / `Item.QueryState()` batch accessors | Medium | 6–14 transitions per row → 2–4. Additive. |
| 11 | **F11** replace `Task.Delay` pump loop; make it opt-in | Small | ~15 KB/s of background garbage → 0. |
| 12 | **F13** `[ThreadStatic]` buffers in `Helpers`; cache `GetFilters()` | Small | ~60 ns per string-returning call. |
| 13 | **F9** cache `FieldInfo[]` + `StringBuilder` in `CallbackToString` | Small | Only matters when `OnDebugCallback` is set; correctly free otherwise. |
| 14 | **F4b** `in ConnectionInfo` / `in NetIdentity` on the virtual/interface surface | Medium | 3.5x on the pass, but **breaking** — needs additive overloads or a major version. |

**Do 1–5 first.** They are all small or trivial, none is breaking, and together
they remove the great majority of the measured per-frame allocation.
