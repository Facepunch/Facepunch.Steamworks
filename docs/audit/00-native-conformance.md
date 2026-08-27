# Native Conformance Audit — P/Invoke surface vs. the shipped Steamworks binaries

**Date:** 2026-07-28
**Auditor:** automated + manual verification
**Repo state:** `master` @ `6424be1`
**Method:** every claim below is derived from parsing the PE export tables of the native
binaries committed to this repository and from the SDK headers in `Generator/steam_sdk/`.
No Steam client, Steam account, or network access was used or required.

---

## Why this audit exists

`Facepunch.Steamworks` is a P/Invoke binding. Every managed call crosses into
`steam_api64.dll` / `steam_api.dll` by **entry-point name string**. Those strings are not
checked by the C# compiler. If a name is wrong, stale, or refers to a function Valve has
since removed, the code compiles perfectly and then throws
`EntryPointNotFoundException` the first time a game calls it — often in a shipped build,
on a customer's machine.

That entire class of defect is detectable offline by comparing the binding names against
the DLL's export directory. That is what this audit does.

---

## Summary of results

| Check | Before | After this pass |
|---|---|---|
| Distinct `DllImport` entry points in managed code | 1024 | **1018** |
| Entry points that **do not exist** in the native library | **6** (all `ISteamAppList`) | **0** |
| Committed native binaries failing conformance | **8 of 17** | **0 of 17** |
| Exports in current `steam_api64.dll` / `steam_api.dll` | 1089 | 1089 |
| Flat-API functions declared in `steam_api_flat.h` | 946 | 946 |
| …of those, bound via `DllImport` | 911 (96.3%) | 911 (96.3%) |
| Exported functions never bound (unused native surface) | 40 | 40 |
| Interface accessors bound at a **stale** version | 0 | 0 |

**Headline.** Binding coverage and interface versioning were already in very good shape.
Two real defects were found and fixed: six bindings to an interface Valve deleted
(`ISteamAppList`), and eight committed native binaries that were ~11 SDK releases
stale — including every Linux and macOS library, which would have prevented a Linux
dedicated server from starting at all. One genuine feature gap remains open (DualSense
adaptive triggers).

---

## Findings

### F-01 — `ISteamAppList` is entirely dead code that throws at runtime

**Severity: HIGH** (it is unreachable from the public API today, which is the only reason
this is not CRITICAL — see Impact.)

**Location:** `Facepunch.Steamworks/Generated/Interfaces/ISteamAppList.cs` (whole file)

**What's wrong.** All six of this interface's native entry points are absent from both
shipped binaries:

```
SteamAPI_SteamAppList_v001                  <- the interface accessor itself
SteamAPI_ISteamAppList_GetNumInstalledApps
SteamAPI_ISteamAppList_GetInstalledApps
SteamAPI_ISteamAppList_GetAppName
SteamAPI_ISteamAppList_GetAppInstallDir
SteamAPI_ISteamAppList_GetAppBuildId
```

These are the *only* 6 of 1024 entry points in the codebase with no matching export.

**Evidence — this is Valve removing the interface, not us mistyping it.** The repo happens
to contain an *older* `steam_api64.dll` at `Facepunch.Steamworks/win64/` (see F-02). That
older binary **does** export all six, plus `SteamAPI_ISteamClient_GetISteamAppList`. The
current binary exports none of them. So `ISteamAppList` was generated against an older SDK
and was never removed when the SDK was updated.

Corroborating: the interface has no source of truth left in the repo either —
`Generator/steam_sdk/` contains no `isteamapplist.h`, `steam_api.json` contains **0**
occurrences of `ISteamAppList`, and `steam_api_flat.h` contains **0** occurrences of
`AppList`. The Generator therefore cannot regenerate this file; it survives only because
it was committed and never deleted.

**Impact.** `SteamAppList` has no public wrapper, so no consumer can reach it through the
normal API and nothing is broken today. But `ISteamAppList.SetupInterface()` would fail to
resolve its accessor, and any future work that wires this up — or any consumer using
reflection over the internal interfaces — hits `EntryPointNotFoundException`. It is a
loaded footgun and it makes the "911 of 946 bound" coverage number misleading.

**Recommended fix.** Delete `ISteamAppList.cs` and remove `ISteamAppList` from any
interface registry. If the app-list capability is wanted later, note that Valve restricted
it to whitelisted partners; it is not available through the public flat API at all.

---

### F-02 — Eight committed native binaries were ~11 SDK releases stale, including every Linux and macOS library

**Severity: CRITICAL** for the Linux/macOS dedicated-server path. **FIXED** in this pass.

**What was wrong.** The repository contained **two different SDK drops** of the native
Steamworks runtime. The root-level Windows DLLs and the whole
`UnityPlugin/redistributable_bin/` set were current. Every per-platform subfolder was not:

| Location | Was | Verdict |
|---|---|---|
| `Facepunch.Steamworks/steam_api64.dll`, `steam_api.dll` | 1089 exports | current — PASS |
| `UnityPlugin/redistributable_bin/**` (all 5 binaries) | 1089 / 1154 exports | current — PASS |
| `Facepunch.Steamworks/win64/steam_api64.dll` | 1069 exports | **stale — FAIL (36 missing)** |
| `Facepunch.Steamworks/linux32/libsteam_api.so` | — | **stale — FAIL (36 missing)** |
| `Facepunch.Steamworks/linux64/libsteam_api.so` | — | **stale — FAIL (36 missing)** |
| `Facepunch.Steamworks/osx/libsteam_api.dylib` | 1069 exports | **stale — FAIL (36 missing)** |
| `Facepunch.Steamworks.Test/{win64,linux32,linux64,osx}/**` | — | **stale — FAIL (36 missing)** |

Every stale copy was missing the *same* 36 entry points, so they all came from one old SDK
drop (roughly the 1.51–1.53 era, judging by `SteamUGC_v017` / `SteamUserStats_v012` /
`SteamRemotePlay_v001` / `SteamVideo_v002` versus the current `v020` / `v013` / `v002` /
`v007` plus `SteamTimeline_v004`).

The 36 missing symbols include the *entire* `ISteamTimeline` interface (22 functions), the
app beta-branch APIs (`GetNumBetas`, `GetBetaInfo`, `SetActiveBeta`), five newer
`ISteamUGC` functions (supported game versions, content descriptors, admin query),
`DismissGamepadTextInput`, `BStartRemotePlayTogether`, and — decisively — both modern init
entry points:

```
SteamInternal_SteamAPI_Init          <- client init
SteamInternal_GameServer_Init_V2     <- dedicated server init
```

**Why this was CRITICAL rather than cosmetic.** `SteamInternal_GameServer_Init_V2` is the
*first* call a dedicated server makes (`Classes/SteamInternal.cs:14`). A Linux dedicated
server shipped with `Facepunch.Steamworks/linux64/libsteam_api.so` next to it could not
initialise at all — it would throw `EntryPointNotFoundException` on startup, before doing
anything. Linux dedicated servers are precisely this fork's reason for existing, and that
was the one platform a Windows dev box could not otherwise verify.

Nothing in the repo referenced these subfolders (no `.csproj`, `.targets`, `.props`,
`.sln`, workflow or script), so the build never consumed them — which is exactly why the
staleness went unnoticed. A human packaging a Linux server would very reasonably copy
`linux64/libsteam_api.so`, and that is the failure path.

**Fix applied.** All eight stale binaries (plus the matching import `.lib` files) were
refreshed from `UnityPlugin/redistributable_bin/`, which conformance-testing proves is the
current drop. Every native binary in the repository — 17 of them, across Windows x86/x64,
Linux x86/x64 and macOS — now passes.

**Regression guard.** `verify-native-conformance.ps1` now checks all 17 on every CI run.

---

### F-03 — PS5 DualSense adaptive triggers are not bound

**Severity: MEDIUM** (feature gap, not a defect)

**Location:** `Facepunch.Steamworks/Generated/Interfaces/ISteamInput.cs` — absent

**What's wrong.** `SteamAPI_ISteamInput_SetDualSenseTriggerEffect` is declared by the SDK
and **is exported by the shipped DLL**, but has no binding.

**Evidence.** `Generator/steam_sdk/steam_api_flat.h:713`:

```c
S_API void SteamAPI_ISteamInput_SetDualSenseTriggerEffect( ISteamInput* self, InputHandle_t inputHandle, const ScePadTriggerEffectParam * pParam );
```

and the symbol is present in the current `steam_api64.dll` export table.

**Why it was skipped.** The parameter type is Sony's `ScePadTriggerEffectParam`, defined in
`Generator/steam_sdk/isteamdualsense.h`, whose payload is a **C `union`**
(`ScePadTriggerEffectCommandData`, line 128) of seven different parameter structs. The
Generator has no union support, so the function and its whole type graph were dropped.
`isteamdualsense.h` has no corresponding generated file at all.

**Impact.** Games shipping on PC with PS5 controller support cannot drive adaptive
triggers through this library. This is a visible, marketable feature.

**Recommended fix.** Hand-write the binding rather than teach the Generator about unions.
In C#, model the union with `[StructLayout(LayoutKind.Explicit)]` and `[FieldOffset(0)]`
on each of the seven parameter variants, then expose a small typed API
(`SteamInput.SetDualSenseTriggerEffect(...)` with per-mode helpers). Add it to the
generator's manual-override list so a regeneration does not delete it. Effort: M.

---

### F-04 — `EnableActionEventCallbacks` is not bound

**Severity: LOW**

`SteamAPI_ISteamInput_EnableActionEventCallbacks` is exported and declared but unbound. It
takes a native function pointer (`SteamInputActionEventCallbackPointer`), which the
Generator does not emit. The same result is achievable by polling the action state each
frame, which the library does support, so this is a convenience gap rather than a
capability gap. Binding it safely requires a rooted, `[MonoPInvokeCallback]`-decorated
static delegate to survive IL2CPP — non-trivial. Recommend deferring.

---

## Correctly-omitted native surface (verified, no action needed)

40 exported functions are never bound. All but the two `ISteamInput` ones above are
deliberate and correct:

| Interface | Count | What it is |
|---|---|---|
| `ISteamRemoteStorage` | 24 | The pre-`ISteamUGC` Workshop API (`PublishWorkshopFile`, `EnumerateUserPublishedFiles`, `UpdatePublishedFile*`, …). Superseded by `ISteamUGC`, which **is** bound. |
| `ISteamNetworking` | 13 | The legacy socket-oriented P2P API (`CreateListenSocket`, `SendDataOnSocket`, `RetrieveData`, …). Superseded by `ISteamNetworkingSockets` / `ISteamNetworkingMessages`, both bound. |
| `ISteamUGC` | 1 | `RequestUGCDetails` — deprecated by Valve in favour of the query API. |
| `ISteamInput` | 2 | See F-03 / F-04. |

---

## Verified-correct areas

These were checked and are **fine**. Recording them so future audits do not re-litigate:

1. **No interface-version drift.** For all 31 versioned accessors the DLL exports, the
   managed code binds the newest available version. There is no case of binding e.g.
   `SteamUGC_v017` while the DLL offers `v020`. (The 32nd bound accessor is the dead
   `SteamAppList_v001` from F-01.)

2. **Modern init entry points are used.** The code calls
   `SteamInternal_SteamAPI_Init` (`Classes/SteamApi.cs:14`) and
   `SteamInternal_GameServer_Init_V2` (`Classes/SteamInternal.cs:14`), not the removed
   `SteamAPI_Init` / `SteamInternal_GameServer_Init`. This matters: those older symbols
   are gone from the current DLL.

3. **`RequestCurrentStats` removal is handled correctly.** Valve deleted
   `SteamAPI_ISteamUserStats_RequestCurrentStats` from the DLL. The public
   `SteamUserStats.RequestCurrentStats()` (`SteamUserStats.cs:137`) is a no-op stub
   returning `true`, marked `[Obsolete]`, and P/Invokes nothing. Correct.

4. **Removed Stadia identity helpers are not referenced** anywhere in managed code.

5. **x86 and x64 binaries expose the same 1089 exports**, so a single set of bindings is
   valid for both — the `Win32`/`Win64` split is about which native file is loaded, not
   about differing surfaces.

---

## Known documentation defect found while verifying F-'s

`SteamUserStats.cs` still tells users to wait for `RequestCurrentStats` to complete:

- line 89: *"Will return false if RequestCurrentStats has not completed and successfully returned"*
- line 124: *"RequestCurrentStats has completed and successfully returned its callback AND…"*
- line 145: *"You must have called `RequestCurrentStats` and it needs to return successfully via its callback prior to calling this."*

`RequestCurrentStats` is now a no-op that never produces a callback. The real precondition
is that Steam has delivered `UserStatsReceived_t`, which it now does automatically at
startup — that is what sets `StatsRecieved`. The guidance as written is unfollowable.
Folded into the documentation workstream.

(Also noted: the public property is spelled `StatsRecieved`. Fixing the typo is a binary-
breaking change; recommend adding a correctly-spelled `StatsReceived` and marking the old
one `[Obsolete]` rather than renaming outright.)

---

## Reproducing this audit

The export-table comparison is fully deterministic and offline. It should become a
permanent CI check so F-01-class defects cannot reappear — see the conformance tooling
added under `Tools/`.
