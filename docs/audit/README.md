# Audit set — AnkleBreaker fork of Facepunch.Steamworks

A full-codebase audit run on 2026-07-28 against `master`. Six workstreams, each producing
an evidence-backed report. **Every finding is verified** — against the SDK headers in
`Generator/steam_sdk/`, against the export tables of the shipped native binaries, or by a
harness that reproduces the behaviour offline. Suspicions that did not survive
verification are recorded as such rather than quietly dropped.

**Steam was not installed and no Steam account was used.** That constraint shaped the
method: it forced everything onto static analysis and pure-managed harnesses, which turned
out to be a strength — those techniques are reproducible in CI, and they found real bugs.

---

## The reports

| # | Report | Scope |
|---|---|---|
| 00 | [Native conformance](00-native-conformance.md) | Every P/Invoke entry point vs. the shipped `steam_api` binaries, on all three platforms |
| 01 | [Marshaling & ABI](01-marshaling-abi.md) | Struct layout, packing, string and array marshaling vs. the C++ headers |
| 02 | [Dispatch, memory & threading](02-dispatch-memory-threading.md) | The callback pump, call-result lifetimes, allocation, and the real threading contract |
| 03 | [API coverage & missing features](03-api-coverage-gaps.md) | What the SDK offers vs. what the public API exposes, per interface |
| 04 | [GameServer / dedicated server](04-gameserver.md) | The fork's flagship area: server interface routing, auth, server browser |
| 05 | [Tests & documentation coverage](05-tests-and-docs.md) | Test suite state, offline test strategy, measured doc coverage |

---

## What was found, in one page

### Fixed during this pass

| Defect | Severity | Report |
|---|---|---|
| **16 generated structs had the wrong memory layout** — one bad heuristic in the generator. `Marshal.PtrToStructure` read up to 7 bytes past Steam's callback buffer, or read every field after the first from the wrong offset. See the note below the table | **Critical** | 01 |
| Solution did not build on a clean machine (`net46` refs + three projects sharing one `obj/`) | Blocker | — |
| `ISteamAppList` — 6 bindings to functions Valve deleted; threw `EntryPointNotFoundException` | High | 00 |
| 8 committed native binaries ~11 SDK releases stale, incl. **every Linux and macOS library** — missing `SteamInternal_GameServer_Init_V2`, so a Linux dedicated server could not initialise | **Critical** | 00 |
| `SteamUtils.CurrentBatteryPower` returned `0` for every charge level below 100% (integer division) | High | 05 |
| `Utility.ToType<T>` boxed every callback struct — 32–320 bytes per handler per delivery, forever | Medium | 02 |
| `Newtonsoft.Json 9.0.2-beta1` (GHSA-5crp-9r3c-p9vr) referenced but unused by both test projects | Medium | 05 |
| PS5 DualSense adaptive triggers unsupported — exported and declared, but the generator cannot emit its C `union` | Medium | 00 |
| **`OnMessage` received `messageNum` and `recvTime` transposed** on both `SocketManager` and `ConnectionManager` — every consumer got a microsecond timestamp as the message number and a sequence counter as the receive time. **Behaviour change:** anyone who compensated for the swap in their own handler must remove that compensation | High | 06 |
| `SocketManager.Receive` leaked the rest of the batch when a handler threw — the unprocessed `NetMsg*` from that native call were never released back to Steam | High | 06 |
| `NetErrorMessage` declared `fixed char[1024]` for a native `char[1024]`, i.e. 2048 bytes for 1024, and its contents were unreadable as declared (Steam writes UTF-8) | Medium | 01 |
| Generated UTF-8 accessors threw `ArgumentOutOfRangeException` when Steam filled a buffer with no terminator — `Array.IndexOf` returns `-1` | Medium | 06 |
| 51 `ByValArray byte[]` fields heap-allocated on every marshal — `SteamUGCDetails_t` cost 19,616 B per workshop item, and several were callback structs allocating per delivery | High | 06 |
| The dedicated-server receive loop allocated per tick and per message — ~460 KB/s of garbage on a 100-player server | High | 06 |
| Seven enumerators re-queried their count through the native boundary every iteration, doubling interop transitions | Medium | 06 |
| Steam Deck floating keyboard and both text-input dismiss calls were bound but unreachable (one was commented out with the wrong return type) | Medium | 03 |
| Complete SDR surface unexposed — poll groups, hosted dedicated servers, ping locations, POPs, certificates | High | 03 |

#### The generator's `Pack` heuristic — the diagnosis, the failed attempt, and what worked

This was the top open finding for two passes and is worth recording in full, because the
obvious fix is wrong in a way that is easy to miss.

**The defect.** `Generator/SteamApiDefinition.cs` decided a struct's
`[StructLayout(Pack=…)]` with a guess: *"if any field **after the first** mentions
`CSteamID`/`CGameID`, use `Pack=4`, else `Pack=8`."* The intent was right —
`steamclientpublic.h:475` opens `#pragma pack( push, 1 )`, `class CSteamID` follows at
:480 and `class CGameID` at :922, and the block is not popped until :1108, so both are 8
bytes with **alignment 1**, while a C# `ulong` wants alignment 8. But `Pack` is a
whole-struct switch and cannot express "this 8-byte field aligns to 1, that one aligns to
8", so it was wrong in both directions:

- `P2PSessionConnectFail_t` — its `CSteamID` is the *first* field, so `Skip(1)` missed it,
  the struct got `Pack=8`, and C# reported **16 bytes against a native 9**.
  `Marshal.PtrToStructure` therefore read **7 bytes past the end** of Steam's callback
  buffer. This one was live (`SteamNetworking.cs:31`).
- `RequestPlayersForGameResultCallback_t` — `Skip(1)` *did* fire, so it got `Pack=4`,
  giving **56 bytes against a native 64**, with 9 of its 10 fields at the wrong offset.

Valve documents the surrounding rule at `steamclientpublic.h:1161-1178`: callback structs
are `#pragma pack(8)` on Windows and `#pragma pack(4)` on Linux/macOS. Combined with the
`pack(1)` on `CSteamID`/`CGameID`, no single `Pack` value can be correct in general.

**The attempt that was reverted, and why it failed.** The obvious move is: model the
pragma by putting `Pack = 1` on the C# `SteamId`/`GameId` structs, then drop the heuristic
and use the platform pack everywhere. That reasoning is sound and was verified in
isolation. It does not work in this codebase, because **the generated structs did not use
`SteamId`** — they emitted a raw `ulong` for every `CSteamID` field, so `Pack = 1` on
`SteamId` never applied to them and flipping their pack from 4 to 8 merely let the `ulong`
align to 8. Measured consequence: `FriendsGetFollowerCount_t` went from 16 bytes to 24,
and **16 was already correct** — native is `int@0`, `CSteamID@4` (align 1), `int@12`,
struct align 4, size 16.

That exposed something the original audit did not state: **the `Pack = 4` hack was
accidentally right much of the time.** Whenever a 4-byte field precedes the `CSteamID`, the
native offset is 4 and a pack-4 `ulong` also lands at 4, so the layouts coincide. The
broken structs were exactly the ones where that coincidence failed — which is why a
wholesale pack flip traded one wrong set for a larger one.

**What worked: change the field's *type*, not the struct's pack.**

1. `Facepunch.Steamworks/Structs/PackedId.cs` — a new `internal`
   `[StructLayout(LayoutKind.Sequential, Pack = 1)] struct PackedId { ulong Value; }`, with
   implicit conversions to and from `ulong`, `SteamId` and `GameId`. `Pack = 1` is
   load-bearing and documented as such in the file. It is deliberately *not* `SteamId`:
   `SteamId` is public and is a by-value P/Invoke argument in over a hundred places, so it
   was left untouched.
2. `Generator/CodeWriter/Struct.cs` emits `PackedId` for every `CSteamID`/`CGameID` struct
   member. The one native id *array* in the SDK
   (`FriendsEnumerateFollowingList_t.m_rgSteamID`, `CSteamID[50]`) becomes
   `fixed byte[400]` — a C# `fixed` buffer takes its alignment from its element type and
   only accepts primitives, so `fixed ulong[50]` would have forced alignment 8 where native
   has 1. `byte` gives the same 400 bytes at the same alignment.
3. Only then was `IsPack4OnWindows` deleted and `Platform.StructPlatformPackSize` used
   uniformly. No heuristic remains; the C# types now model the native ABI directly.

**Verified.** 16 structs moved to their header-derived layouts, on both Windows (pack 8)
and POSIX (pack 4). A further 17 changed only their recorded `Pack` attribute, with size
and every field offset byte-identical — those are precisely the "accidentally right" set.
Nothing else in the assembly moved, and no public type changed. The independent check is a
header-derived MSVC-x64 layout model diffed against `Marshal.SizeOf`/`OffsetOf`: **19
mismatches → 0** on Windows and **4 → 0** on POSIX, the only remaining entries being four
hand-written structs that deliberately expose fewer fields than the C++ definition while
pinning the same total size. `FriendsGetFollowerCount_t` — the canary from the failed
attempt — stays at **16**.

The `20` figure used in earlier revisions of this file was a miscount, taken from the
number of size *changes* the reverted pack-flip produced rather than from the defect
tables. The verifiable number from [01](01-marshaling-abi.md)'s own tables is **17**:
5 offset defects (F1) + 9 over-reads (F2) + 2 under-reads (F3) + `SteamInputActionEvent_t`
(F6). 16 of those are fixed here. `SteamInputActionEvent_t` is not, and cannot be by
packing alone — the generator drops its `union` entirely, so its size is wrong for an
unrelated reason. It remains open as F6.

### Open, ranked by severity

1. **`MatchMakingKeyValuePair` and 20 `const char *` callback fields decode as ANSI, not
   UTF-8** — independent of packing, from [01](01-marshaling-abi.md) F4/F5.
   `MatchMakingKeyValuePair` (server-browser filters) marshals as ANSI, so `"café"` goes
   out as `63 61 66 E9` instead of UTF-8 and `"日本語"` becomes three literal `?` —
   unrecoverable. Twenty `const char*` callback fields are typed as raw `string`, including
   `HTML_NeedsPaint_t.PBGRA`, which is a **BGRA framebuffer pointer** being scanned for a
   NUL terminator.
2. **`SteamApps` is registered on dedicated servers but has no game-server accessor** —
   `Self` stays `IntPtr.Zero` and `SteamServer.AddInterface<T>` ignores the failure return
   (unlike `SteamClient`'s). Any `SteamApps.*` call on a pure dedicated server passes a
   NULL `this` to the flat API: an access violation, not a catchable exception. (04)
3. **Shutdown races the async callback pump** — `Dispatch.LoopClientAsync` is `async void`
   with no cancellation or join, so an in-flight frame can call into a destroyed pipe.
   Crash-on-exit for headless servers. (02)
4. **Pending call results are silently abandoned on shutdown** — proven: 5000 registered
   continuations, 0 invoked. Every awaiting `Task` hangs forever. (02)
5. **`Dispatch.runningFrame` is a non-volatile check-then-set** — 872,129 simultaneous
   entries measured in a 2M-iteration race. Concurrent `FreeLastCallback` is something
   Valve explicitly forbids. (02)
6. **Server auth tickets cannot be cancelled** — `AuthTicket.Cancel()` hard-codes
   `SteamUser.Internal`, which is never registered server-side, so `Dispose()` throws. (04)
7. **`SteamServer.Shutdown()` does not reset cached statics**, so a second `Init` silently
   skips `ModDir`/`GameDescription`/`MaxPlayers`. (04)
8. **Callback exceptions are swallowed** and permanently drop the remaining handlers for
   that callback. (02)
9. **`SteamInputActionEvent_t` is 16 bytes against a native 33** — the generator cannot emit
   its C `union`, so it drops it. Unreachable today (`EnableActionEventCallbacks` is filtered
   out as deprecated), but it is a compiled-in landmine. (01, F6)
10. **`Achievement.GlobalUnlocked` always returns `-1`** — its prerequisite call is never
    made. (03)
11. **`SteamNetworkingSockets.CreateFakeUDPPort` returns an unusable handle** — the backing
    class is empty and its `Self` is permanently zero. (03)
12. **`GSStatsUnloaded_t` is unreachable** — a genuine upstream callback-ID collision at
    1108 that the generator resolves inconsistently with the identical one at 1112. (05)

### The headline numbers

- **1019** P/Invoke entry points, **100%** resolving against **all 17** committed native
  binaries across Windows x86/x64, Linux x86/x64 and macOS. Was 8 of 17 failing.
- **906 of 946** flat-API functions bound (95.8%). Of the 40 unbound, **38 are correctly
  omitted** (Valve-superseded legacy) and 2 were genuine gaps — one of which (DualSense)
  is now closed.
- **52.9%** of bound internal methods are reachable from the public API. **11 interfaces
  have zero public reachability.**
- **36.2%** of the public API has any XML documentation. **Zero `<example>` and zero
  `<remarks>` in the entire library.** `<param>` sits at 11.8% — and for a P/Invoke
  binding, the arguments are exactly where the danger is.
- **118 of 118 tests fail offline** in 227 ms, all on one `[AssemblyInitialize]`. Separately,
  **53 of 118 contain no assertions at all**.
- Valve's own headers leave **30.9%** of methods with no comment, and **91.7%** of
  `const char*` returns document no pointer lifetime — so our docs must state assumptions
  rather than cite Valve.

---

## Method notes

**Verification over assertion.** Findings cite a header quote, a measured number, or a
reproduction. Several plausible-sounding hypotheses were investigated and *disproved*;
those are recorded in each report's "verified-correct" section so the next audit does not
re-litigate them. Notable examples: there is not a single `GCHandle` in the assembly, so
the classic `ISteamMatchmakingServers` delegate-collection crash is structurally
impossible; `FreeLastCallback` genuinely is called on every path including exceptions; and
`CallbackMsg_t` is byte-exact against native.

**Offline testing is viable and valuable.** Six techniques were prototyped and run, not
merely proposed — export conformance, struct layout, callback IDs, enum conformance,
pure-managed logic, and reflection-based API invariants. Between them they found the dead
`ISteamAppList` bindings, the callback-ID collision, a missing `ToString()` override, and
contributed to the battery bug. None require Steam. Report 05 details the recommended
test-project layout.

**Already in CI.** `verify-native-conformance.ps1` runs the full 17-binary export sweep on
every push, and `verify-struct-layout.ps1` pins the marshalled size, pack and every field
offset of all 240 ABI structs against `Tools/baselines/layout-win64.txt`. Between them the
three most severe defects found here cannot silently reappear: a symbol Valve deleted, and
a generator change that quietly re-lays-out dozens of structs at once.

**Still worth adding.** The layout baseline pins what the layout *is*, not what it *should
be* — it catches drift but would happily accept a wrong layout that was recorded
deliberately. The proof that the current layouts are correct comes from a header-derived
MSVC-x64 layout model (`#pragma pack` context recovered from the 45 SDK headers, field
lists read from `steam_api.json`) diffed against `Marshal.SizeOf`/`OffsetOf`. That model
still lives outside the repository, so the correctness argument is reproducible by hand but
not by CI. Committing it as a third `Tools/` project would close the last gap.
