# Test Suite & Documentation Coverage Audit

**Date:** 2026-07-28
**Auditor:** automated measurement + manual verification
**Repo state:** `Facepunch.Steamworks.sln`, builds clean (`dotnet build -c Release` → **0 errors, 16 warnings**, 2.30 s)
**Method:** every number below is produced by running a tool, parsing a file, or reading a
declaration. Nothing is estimated. The measurement harness lives in the scratchpad
(`docaudit/`) and is reproducible; its outputs (`members.csv`, `types-ranked.csv`,
`dllimports.csv`, `struct-sizes.txt`, `native-exports.txt`) back every table here.

**Environment constraint:** Steam is **not installed** on this machine and there is **no Steam
account**. That constraint is the point of Part A — it forces the question of what this
library can prove about itself with no Valve infrastructure at all.

---

## Part A: Test suite

### Current state

The suite is `Facepunch.Steamworks.Test`, built by two csproj files that live in the **same
directory** (`Facepunch.Steamworks.TestWin32.csproj`, `Facepunch.Steamworks.TestWin64.csproj`)
and therefore share `obj/` and `bin/Release/net6.0/`. Both exclude exactly one file
(`<Compile Remove="ClanTest.cs" />`) and nothing else.

#### Headline counts

| Metric | Count |
|---|---:|
| `.cs` files in the test project | 25 |
| `[TestClass]` | 21 |
| `[TestMethod]` attribute literals | 124 |
| …minus 1 inside the fully block-commented `Client/Server/StatsTest.cs` | 123 |
| …minus 5 in `ClanTest.cs` (excluded from **both** csproj) | **118 compiled and runnable** |
| Tests observed by the runner | **118** (exact match — see failure below) |
| Test methods containing **zero** `Assert.` calls | **53 / 118 (44.9%)** |
| `Assert.Inconclusive` | 0 |
| `[Ignore]` | 0 |
| `[TestCategory]` | 0 |
| `async void` | 0 |
| `Thread.Sleep` | 1 (`UserTest.cs:30`) |
| `[AssemblyInitialize]` / `[AssemblyCleanup]` | 1 / **0** |

Nearly **half the suite asserts nothing**. `UtilsTest.cs` is the extreme case: 13 of its 14
tests are `Console.WriteLine` with no assertion, so they cannot fail for any reason other
than an exception. `FriendsTest.cs` has **zero live assertions across all 8 tests** — its only
four `Assert.` occurrences are commented out and reference an API shape
(`Steamworks.Friends.AvatarSize`) that no longer exists.

#### Per-file inventory

Dependency classes: **LIVE** = needs a running, logged-in Steam client · **ACCT** = needs an
account in a specific state · **NET** = needs Steam backend round-trips · **WRITE** = mutates
remote state · **PURE** = would pass with no Steam at all.

| File | Tests | No-assert | Covers | LIVE | ACCT | NET | WRITE | PURE |
|---|---:|---:|---|:-:|:-:|:-:|:-:|:-:|
| `AppTest.cs` | 6 | 2 | SteamApps surface **+ the assembly-wide client/server init** | ● | ● | ● | | |
| `ClanTest.cs` | 5 | 1 | Group name/tag/owner — *excluded from both builds* | ● | | ● | | |
| `FriendsTest.cs` | 8 | **8** | Friend/blocked/played-with lists, avatars, overlay | ● | ● | ● | | |
| `GameServerStatsTest.cs` | 1 | 0 | Server-side read of another user's achievement | ● | ● | ● | | |
| `GameServerTest.cs` | 3 | 1 | Dedicated flag, public IP, auth-ticket round trip | ● | | ● | ● | |
| `InputTest.cs` | 1 | **1** | SteamInput controllers, digital + analog action | ● | ● | | | |
| `InventoryTest.cs` | 10 | 1 | Item defs, prices, owned items, exchange, serialise | ● | ● | ● | | |
| `NetworkingMessagesTest.cs` | 2 | 0 | Zero-alloc `MessageIntercept` drain, `IntPtr`/`Span` sends | ● | | | | |
| `NetworkingSockets.cs` | 7 | 6 | SDR relay / normal / FakeIP sockets, loopback exchange | ● | | ● | | **1** |
| `NetworkingSocketsTest.TestConnectionInterface.cs` | 0 | – | Helper `ConnectionManager` subclass | | | | | |
| `NetworkingSocketsTest.TestSocketInterface.cs` | 0 | – | Helper `SocketManager` subclass | | | | | |
| `NetworkingUtils.cs` | 3 | 0 | SDR ping location, parse, estimate | ● | | ● | | |
| `RemotePlayTest.cs` | 1 | 0 | Session count + invalid handle | ● | | | | |
| `RemoteStorageTest.cs` | 5 | 3 | Quota flags, **100 MB** cloud write→read, listing | ● | ● | ● | ● | |
| `ServerlistTest.cs` | 11 | **8** | Internet/LAN/favourites/friends/history/IP queries | ● | ● | ● | | **1** |
| `SteamMatchmakingTest.cs` | 4 | 2 | Create public lobby, set data, list, chat | ● | | ● | ● | |
| `SteamNetworkingTest.cs` | 1 | 0 | Legacy `ISteamNetworking` P2P to self | ● | | ● | | |
| `UgcEditor.cs` | 4 | 0 | Publish workshop items (ASCII, CJK, 32 MB), edit, delete | ● | | ● | ● | |
| `UgcQuery.cs` | 6 | 0 | Workshop queries: all / tag / friends / user / file id | ● | ● | ● | | |
| `UgcTest.cs` | 2 | 1 | Download hardcoded item, `Item.GetAsync` | ● | | ● | ● | |
| `UserStatsTest.cs` | 13 | 2 | Achievements, leaderboards (find/create/submit/read), stats | ● | ● | ● | ● | |
| `UserTest.cs` | 16 | 5 | Voice, login, SteamID, auth tickets, level, store URL | ● | ● | ● | ● | |
| `UtilsTest.cs` | 14 | **13** | `SteamUtils` scalar properties + file-signature check | ● | | | | |
| `Properties/AssemblyInfo.cs` | 0 | – | Legacy attributes (`Copyright © 2016`, COM GUID) | | | | | |
| `Client/Server/StatsTest.cs` | 1* | – | **100% dead** — whole file is one `/* */` block, pre-2.0 API | | | | | |

**Only two tests are pure-managed**, and one of them asserts nothing:

1. **`ServerListTest.IpAddressConversions`** (`ServerlistTest.cs:18`) — exercises
   `Utility.IpToInt32` / `Int32ToIp`, which are 100% managed bit-shuffling over
   `System.Net.IPAddress` (`Facepunch.Steamworks/Utility/Utility.cs:39-47`). **It contains zero
   assertions** — it prints four lines and can never fail.
2. **`NetworkingSocketsTest.NetAddressTest`** (`NetworkingSockets.cs:149`) — the **only**
   assertion-bearing test in the entire suite that does not need a logged-in Steam client. It
   P/Invokes the stateless `SteamAPI_SteamNetworkingIPAddr_*` struct helpers
   (`Generated/SteamStructFunctions.cs:199-227`): no interface pointer, no `SteamAPI_Init`, no
   account, no network. It needs only that the native DLL loads.

So the honest figure is: **1 of 118 tests (0.8%) provides offline signal today.**

#### The exact observed failure running offline

```
dotnet test Facepunch.Steamworks.Test/Facepunch.Steamworks.TestWin64.csproj \
            -c Release --no-build --blame-hang-timeout 60s
```

Every one of the 118 tests fails identically, in 227 ms:

```
Assembly Initialization method Steamworks.AppTest.AssemblyInit threw exception.
System.Exception: System.Exception: SteamApi_Init failed with FailedGeneric - error:
Could not determine Steam client install directory.. Aborting test execution.

Stack Trace:
   at Steamworks.SteamClient.Init(UInt32 appid, Boolean asyncCallbacks)
      in Facepunch.Steamworks\SteamClient.cs:line 52
   at Steamworks.AppTest.AssemblyInit(TestContext context)
      in Facepunch.Steamworks.Test\AppTest.cs:line 49
```

```
Failed!  - Failed: 118, Passed: 0, Skipped: 0, Total: 118, Duration: 227 ms
           - Facepunch.Steamworks.TestWin64.dll (net6.0)
```

The mechanism is `[AssemblyInitialize]` at `AppTest.cs:21`, which calls
`SteamClient.Init(1442910)` at `AppTest.cs:49`. MSTest treats an `[AssemblyInitialize]`
throw as fatal to the whole assembly, so **the single init call gates 100% of the suite**.
There is no `[TestCategory]` and no `[Ignore]` anywhere, so there is no filter that can
select a Steam-free subset — even `NetAddressTest`, which needs nothing but the DLL, is
taken down with the rest.

Note the double-wrapped message (`System.Exception: System.Exception:`) — `SteamClient.cs:52`
wraps an already-`Exception`-typed failure. Worth tidying when that file is next touched.

#### Structural problems found while inventorying

Ordered by severity. These matter because they shape what a rewrite should preserve.

**Blocking**

- **App ID mismatch.** `AppTest.cs:49`/`:61` initialise client and server as **`1442910`**, but
  virtually every account-state assertion targets **Rust (`252490`)**: `COLLECT_100_WOOD`, stat
  `"deaths"`, tags `"Version3"`/`"Hunting Bow"`, `game_actions_252490.vdf`, itemstore
  `252490`, `"RustClient.exe"`. Every ACCT assertion is checked against the wrong app. Even
  with a live Steam client and a Rust-owning account, large parts of this suite cannot pass.
- **`InputTest.cs:15` deploys a file that does not exist.**
  `[DeploymentItem("controller_config/game_actions_252490.vdf")]` — there is no
  `controller_config` directory and **zero `.vdf` files anywhere in the repository**.
- **`Facepunch.Steamworks.TestWin32.csproj` is mis-targeted.** It sets
  `<PlatformTarget>x64</PlatformTarget>` on `Release|AnyCPU` (line 9), `Debug|x86` (line 24)
  and `Release|x86` (line 30) while referencing `Facepunch.Steamworks.Win32.csproj`, which
  P/Invokes 32-bit `steam_api.dll`. Only `Debug|AnyCPU` is actually x86 — every other
  configuration is a guaranteed `BadImageFormatException`.
- **Both csproj share one output directory**, so building both in one configuration races on
  intermediate artifacts.
- **`GameServerTest.PublicIp` (`:23`) is a `while (true)` with no timeout** — the only one in
  the suite. If `SteamServer.PublicIp` never resolves, the run hangs indefinitely.

**High**

- **No `[AssemblyCleanup]`.** `SteamClient.Shutdown()` / `SteamServer.Shutdown()` are never
  called. `AppTest.cs:31-38` documents that a callback exception on the pump thread used to
  abort the whole run; the fix latches it into `LastDispatchException`, but **only
  `NetworkingMessagesTest` asserts on that latch**. Every other class silently swallows
  dispatch exceptions.
- **`UgcEditor.UploadBigishFile` leaks 32 MB per run** — writes `testfile1.bin` (`:108`),
  `finally` deletes `testfile.bin` (`:124`).
- **`RemoteStorageTest` writes a 100 MB blob to Steam Cloud** under key `"testfile"` and never
  deletes it. `FileRead` calls `FileWrite()` directly (`:53`) — test-calls-test coupling, and
  200 MB allocated plus two cloud writes if both run.
- **`SteamMatchmakingTest` creates three real public lobbies per run** and
  `LobbyList`/`LobbyListWithAtLeastOne` never call `Leave()`.
- **`UserTest.GetVoice` (`:24`) sets `SteamUser.VoiceRecord = true` and never resets it** — the
  microphone stays capturing for the life of the test host.
- **`UserStatsTest.StoreStats` (`:42`) is missing `[TestMethod]`** — a complete `async Task`
  body with an `Assert.AreEqual` that is dead code.
- **Bit-rotted network fixtures.** `ServerlistTest.RustServerListTest` (`:74`) filters
  `gametype="v2405"`, a Rust protocol version no live server advertises; its own comment dates
  it to "around august 2023". `ServerListIps` (`:242`) asserts against **36 hardcoded IPv4
  literals** from ~2019. `FilterByMap` filters `"de_dust"` — a CS 1.6-era map — against appid
  `1442910`, returns nothing, and its assertion lives inside a `foreach` that never
  iterates (vacuous pass).
- **Non-deterministic SteamIDs.** `FriendsTest.cs:55,70,87` use
  `(ulong)(76561197960279927 + (new Random().Next() % 10000))` — a different random account
  every run.
- **Third-party dependency.** `UserStatsTest.cs:174` asserts a *specific other person's*
  account state (`new Friend(76561197965732579); // Hezzy` — deaths > 0, `COLLECT_100_WOOD`
  unlocked).

**Medium / low**

- `Client/Server/StatsTest.cs` is entirely commented out but still compiled (empty translation
  unit) — should be deleted.
- Dead `break` after `Assert.Fail` (which throws) at
  `NetworkingSocketsTest.TestConnectionInterface.cs:79`,
  `NetworkingSocketsTest.TestSocketInterface.cs:71` and `:104`.
- `UgcTest.Download` (`:17`) fires `SteamUGC.Download(1717844711)` and returns immediately —
  no await, no assertion.
- `SteamMatchmakingTest.cs:55` sets lobby data `"dicks" = "unlicked"` and `:89` sends
  `"Hello Friends, It's me - your big fat daddy"` to a **real public lobby**. Regardless of
  taste, this is user-visible output from an automated test.
- Shipped typos: `AppTest.GameLangauge` (`:73`), `ServerlistTest.ServerListInternetInterupted`
  (`:35`).
- `Properties/AssemblyInfo.cs` still declares `AssemblyCopyright("Copyright ©  2016")` and a
  COM GUID for a `net6.0` assembly.

---

### Dependency/CVE issues

Measured from the two csproj files, `packages.config`, and the restore warnings emitted by
`dotnet build -c Release`.

| Package | Pinned version | Status | Assessment |
|---|---|---|---|
| `Newtonsoft.Json` | **9.0.2-beta1** | **HIGH severity CVE**, [GHSA-5crp-9r3c-p9vr](https://github.com/advisories/GHSA-5crp-9r3c-p9vr) | **Remove outright.** See below. |
| `Newtonsoft.Json` (Generator) | **9.0.1** | Same CVE | Bump to `13.0.3`. |
| `MSTest.TestAdapter` | **2.0.0-beta4** | Prerelease from 2019 | Replace. |
| `MSTest.TestFramework` | **2.0.0-beta4** | Prerelease from 2019 | Replace. |
| `Microsoft.NET.Test.Sdk` | `16.*` | Floating major-16; VS2019 era | Pin `17.11.1`+. |
| `packages.config` | targets `net46`/`net452` | Leftover from pre-SDK projects | Delete. |

The build emits **four** `NU1903` warnings for the CVE (two projects × two restore passes):

```
Facepunch.Steamworks.Test\Facepunch.Steamworks.TestWin64.csproj : warning NU1903:
  Package 'Newtonsoft.Json' 9.0.2-beta1 has a known high severity vulnerability
Facepunch.Steamworks.Test\Facepunch.Steamworks.TestWin32.csproj : warning NU1903: (same)
Generator\Generator.csproj : warning NU1903:
  Package 'Newtonsoft.Json' 9.0.1 has a known high severity vulnerability
```

**Assessment and recommendation:**

1. **`Newtonsoft.Json` in the test projects is entirely unused.** There are **zero**
   `Newtonsoft` / `JsonConvert` references in any test source file. This is a HIGH-severity
   advisory carried for no benefit whatsoever. **Delete the `PackageReference` from both test
   csproj.** This is a zero-risk, zero-effort fix and should not wait for the test rewrite.
   GHSA-5crp-9r3c-p9vr is a denial-of-service via deeply-nested JSON during deserialisation —
   it needs attacker-controlled JSON to be exploitable, which a test project that never
   deserialises anything is not. So the *practical* risk here is nil; the reason to act is
   that it is free to fix and it is noise in every build log and every SCA scan.
2. **`Generator`'s `Newtonsoft.Json` 9.0.1 is real usage** — the generator parses
   `steam_api.json`. Bump it to `13.0.3`. The input is a vendored Valve file, not attacker
   controlled, so this is hygiene rather than an incident.
3. **MSTest `2.0.0-beta4` is a prerelease from 2019.** Beyond being unsupported, it predates
   `[DataRow]` improvements, parallelisation attributes, and the `Assert.ThrowsException`
   overloads a modern suite wants. Nothing in the current tests depends on MSTest specifics
   beyond `[TestClass]`/`[TestMethod]`/`Assert.*`, so migration cost is low.
4. **`Microsoft.NET.Test.Sdk 16.*`** floats within a major version that shipped with VS2019.
   Pin explicitly — floating versions make CI non-reproducible.
5. `net6.0` itself went out of support in **November 2024**. The library correctly
   multi-targets `net46;netstandard2.1;net6.0` for consumer reach, but the *test host* should
   run on a supported runtime (`net8.0`).

---

### Offline test strategy

This is the substantive deliverable. Each row below was **prototyped and run** during this
audit, not reasoned about — the "Proven by" column names what was actually executed.

| # | Technique | Feasible? | What it catches | Real bugs found *during this audit* | Effort |
|---|---|:-:|---|---|---|
| 1 | **Native export conformance** — parse the PE export directory of the shipped `steam_api64.dll`/`steam_api.dll`, assert every `DllImport` `EntryPoint` exists | **Yes, proven** | `EntryPointNotFoundException` at customer runtime; stale bindings after an SDK bump | **6 dead entry points**, all `ISteamAppList` | **S** (~150 LOC PE reader, done) |
| 2 | **Struct layout / size assertions** — `Marshal.SizeOf` + `Marshal.OffsetOf` against a golden file derived from the SDK headers | **Yes, proven** | Silent memory corruption at the marshalling boundary; `Pack` regressions across TFMs | 0 today (**337 sizes captured as baseline**) | **M** |
| 3 | **Callback ID uniqueness & correctness** vs. the headers | **Yes, proven** | Callbacks dispatched to the wrong struct; unreachable callbacks | **1 live collision at id 1108**, 1 generator inconsistency | **S** |
| 4 | **Enum value conformance** vs. the headers | **Yes, proven** | Enum drift after an SDK bump — the classic silent binding bug | 0 mismatches; **2 header callbacks absent from the binding** | **M** |
| 5 | **Pure-managed logic** — `Ugc.Query` builder, `ServerList` filters, `SteamId`/`GameId`/`NetAddress`/`NetIdentity`, `Dispatch` bookkeeping | **Yes** | Ordinary logic bugs in the ~57 structs and 42 classes that are not thin P/Invoke shims | **`GameId` does not override `ToString()`** | **M–L** |
| 6 | **Reflection-based API invariants** — docs present, accessors wired, no internal-type leaks | **Yes, proven** | API-surface regressions; accidental internal exposure; doc rot | **0 leaks (baseline)**; **11 unwired wrappers**; doc gaps → Part B | **S** |

#### 1. Native export conformance — **proven, highest value per line of code**

Reads the PE export directory directly (no `dumpbin`, no external tooling) and cross-checks
against every `DllImport` in the assembly, discovered by reflection.

```
Exported symbols in steam_api64.dll : 1089
DllImport declarations in assembly  : 1024
ENTRY POINTS NOT IN EXPORT TABLE    : 6
    SteamAPI_ISteamAppList_GetAppBuildId         <- ISteamAppList._GetAppBuildId
    SteamAPI_ISteamAppList_GetAppInstallDir      <- ISteamAppList._GetAppInstallDir
    SteamAPI_ISteamAppList_GetAppName            <- ISteamAppList._GetAppName
    SteamAPI_ISteamAppList_GetInstalledApps      <- ISteamAppList._GetInstalledApps
    SteamAPI_ISteamAppList_GetNumInstalledApps   <- ISteamAppList._GetNumInstalledApps
    SteamAPI_SteamAppList_v001                   <- ISteamAppList.SteamAPI_SteamAppList_v001

Exports never referenced by any DllImport : 71
```

This confirms the finding already recorded in `00-native-conformance.md`. The "71 unused"
figure is *exports minus successfully-bound imports* (1089 − 1018); it is a superset of the
40 reported there, which counted only unbound functions declared in `steam_api_flat.h`. Both
are correct under their own definitions — the test should assert on the **6**, and merely
report the unused set.

The reverse direction is also worth asserting once `ISteamAppList` is fixed: a *new* export
appearing in a vendored SDK bump is how you notice Valve added an API.

#### 2. Struct layout / size assertions — **proven, needs a golden file**

`Marshal.SizeOf` succeeds on **337 of 435** value types in the assembly. The 98 failures are
**not bugs**:

- compiler-generated async state machines (`<GetFileDetailsAsync>d__44`, `<LoopClientAsync>d__22`, …)
- managed-facing structs that legitimately hold reference types (`Ugc.Query`, `Ugc.Item`,
  `Ugc.Editor`, `InventoryItem`, `Data.LobbyQuery`, `Dispatch+Callback`)

The test must therefore filter to `[StructLayout]`-attributed, blittable interop structs —
the assembly carries **244 `StructLayout` attributes**, of which **235** use
`Pack = Platform.StructPlatformPackSize` and **5** use `Pack = 1`. That `Pack` split is
exactly the sort of thing that breaks silently on a platform change, and exactly what this
test locks down.

All 337 sizes were written to `struct-sizes.txt` during this audit. **That file is the golden
baseline** — commit it and diff against it. Deriving expected sizes from the headers as well
is a second, stronger step (it catches the case where *both* the binding and the golden file
are wrong), but the self-consistency check alone catches every regression introduced by our
own edits, which is the common case.

`Marshal.OffsetOf` per field is the stronger form and is mechanical to add once `SizeOf` is
in place. Recommend `SizeOf` first (one line per struct), `OffsetOf` for the ~20 structs
where a field ordering mistake would be catastrophic (the networking and callback structs).

#### 3. Callback ID uniqueness and correctness — **proven, found a live issue**

Running against the real assembly:

```
CallbackType members                 : 217
Distinct numeric values              : 216
DUPLICATE callback ids               : 1
    1108 <- UserStatsUnloaded, GSStatsUnloaded

Structs implementing ICallbackData   : 217
  DataSize == Marshal.SizeOf         : 217 ok / 0 MISMATCH
  CallbackType id defined            : 217 ok / 0 undefined
  two structs sharing one id         : 1  (UserStatsUnloaded_t and GSStatsUnloaded_t)

CallbackTypeFactory.All entries      : 216
CallbackType values NOT mapped       : 0
```

**The collision is genuinely upstream in Valve's SDK**, verified in the headers:

- `Generator/steam_sdk/isteamuserstats.h:419` — `UserStatsUnloaded_t` uses `k_iSteamUserStatsCallbacks + 8`
- `Generator/steam_sdk/isteamgameserverstats.h:107` — `GSStatsUnloaded_t` uses `k_iSteamUserStatsCallbacks + 8`
- `Generator/steam_sdk/steam_api_internal.h:256` — `k_iSteamUserStatsCallbacks = 1100`

Both resolve to **1108**, and both structs have identical layout (a single `CSteamID`).

The binding's handling is **inconsistent between the two collisions it has**:

| Collision | Header structs | Binding's workaround | Location |
|---|---|---|---|
| **1108** | `UserStatsUnloaded_t`, `GSStatsUnloaded_t` | Both enum members kept; the **factory entry** for `GSStatsUnloaded` is commented out | `Generated/CustomEnums.cs:91-92`, `:315-316` |
| **1112** | `GlobalStatsReceived_t`, `PS3TrophiesInstalled_t` | The **enum member** `PS3TrophiesInstalled` is commented out | `Generated/CustomEnums.cs:96-97` |

Consequence of the 1108 case: `Dispatch` resolves the type via
`CallbackTypeFactory.All.TryGetValue` (`Classes/Dispatch.cs:168`), so **`GSStatsUnloaded_t` is
unreachable** — id 1108 always resolves to `UserStatsUnloaded_t`. Because the layouts are
identical this is *functionally* benign today, but it is undocumented, it is asymmetric with
how 1112 is handled, and it will silently break if Valve ever changes either struct.

A test here should assert: every `CallbackType` value maps to exactly one struct **except an
explicit, commented allow-list of known upstream collisions**. That turns an invisible quirk
into a documented, enforced decision. The `DataSize == Marshal.SizeOf` check (217/217 passing)
is a free regression guard worth locking in immediately.

#### 4. Enum value conformance vs. the headers — **proven**

Extracting `k_iCallback` from the headers and resolving the base constants:

```
callback base constants found         : 37
structs with k_iCallback in headers   : 172
CallbackType enum members (live)      : 217

matched exactly                       : 170
MISMATCHED                            : 0
no header struct found                : 49   (Facepunch-added / renamed)
header callbacks NOT in binding enum  : 2    (GCMessageAvailable_t, GCMessageFailed_t)
duplicate k_iCallback IN VALVE HEADERS: 2    (1108, 1112)
```

**Zero mismatches** — the generator is doing its job. The two absent callbacks belong to
`ISteamGameCoordinator`, a Valve-internal facility; their absence is correct but should be
recorded as an intentional exclusion rather than an accident.

The same technique extends to the ordinary enums (`Result`, `NetConnectionEnd`, …), which is
where drift is most likely and most damaging, because a shifted enum value produces *plausible
wrong answers* rather than a crash. Effort is **M**: the header regex work is done for
callbacks; the general case needs to handle `enum EResult { k_EResultOK = 1, ... }` forms and
the `k_E`-prefix-stripping convention the generator applies.

#### 5. Pure-managed logic — **feasible, largest surface**

The parts of this library that are *not* thin P/Invoke shims are exactly the parts most
likely to contain ordinary bugs, and they need nothing from Steam:

- **`Ugc.Query`** — 72 public members, a pure fluent builder. It accumulates filter state and
  only touches native code at `RunAsync`. Every `WithTag` / `WhereUserPublished` /
  `MatchAnyTag` permutation is assertable offline. It is also the **#3 documentation gap**
  (4.2% covered), so tests and docs should be written together.
- **`ServerList.*`** — 36 public members across `Base`/`Internet`/`Local`/`Favourites`/
  `Friends`/`History`/`IpList`. Filter construction (`gametype`, `secure`, `appid`,
  `gamedir`) is string/array assembly.
- **Value types** — verified during this audit:

  | Type | `Marshal.SizeOf` | Round trip | `ToString()` |
  |---|---:|---|---|
  | `SteamId` | 8 | OK | `"76561197960287930"` ✔ |
  | `Data.GameId` | 8 | OK | **`"Steamworks.Data.GameId"`** ✘ — not overridden |
  | `AppId` | 4 | OK | `"1442910"` ✔ |
  | `Data.NetAddress` | 18 | – | 4 static factories (`AnyIp`, `LocalHost`, `From`×2) |
  | `Data.NetIdentity` | 136 | – | – |

  **`GameId` not overriding `ToString()` is a real, if minor, API inconsistency** found by the
  probe — its siblings both do. Exactly the class of defect an offline suite exists to catch.
- **`Dispatch`** managed bookkeeping — the callback/CallResult dictionaries, disposal, and the
  `OnException` latch can be driven by feeding synthetic callback data through the managed
  path without a live pump.

Effort **M–L** because it is genuine test-writing rather than a mechanical sweep, but this is
where the *durable* value is — techniques 1-4 are one-off harnesses; this is ongoing coverage.

#### 6. Reflection-based API invariants — **proven**

```
Public member exposes an internal type : 0 violations
ISteam* wrapper classes                : 35
  ...with no static accessor anywhere  : 11
[Obsolete] members on public surface   : 4
XML doc entries — net46 / ns2.1 / net6.0 : 727 / 728 / 728
```

- **The no-internal-leak invariant already holds (0 violations).** Assert it now so it keeps
  holding — this is the cheapest possible regression test.
- **11 of 35 `ISteam*` wrappers have no static accessor**: `ISteamClient`, `ISteamController`,
  `ISteamGameSearch`, `ISteamHTMLSurface`, `ISteamHTTP`, `ISteamMatchmakingPingResponse`,
  `ISteamMatchmakingPlayersResponse`, `ISteamMatchmakingRulesResponse`,
  `ISteamMatchmakingServerListResponse`, `ISteamMusicRemote`, `ISteamNetworkingFakeUDPPort`.
  Several are legitimately callback-response interfaces rather than accessible singletons, so
  this needs an allow-list rather than a blanket assertion — but the ones that are *not*
  (`ISteamHTTP`, `ISteamHTMLSurface`, `ISteamGameSearch`, `ISteamMusicRemote`) are unexposed
  functionality worth a roadmap entry.
- Note `ISteamAppList` is **not** in that list — it *has* an accessor. That is precisely why
  the 6 dead entry points are dangerous: the wiring looks complete right up until the call
  throws.
- **The `net46` build is missing one XML doc entry** relative to the other two TFMs. Small,
  but it means conditional compilation is silently changing the public surface across
  targets — worth an assertion that the three TFMs expose the same API.
- "Every public API has XML docs" is feasible but **cannot be turned on as a gate today** —
  Part B measures 36.2% coverage. Adopt it as a **ratchet**: record the current per-namespace
  numbers as a floor and fail the build if coverage drops.

#### Recommendation

Adopt **all six**, in two waves.

**Wave 1 — conformance harness (techniques 1, 2, 3, 6).** Mechanical, already prototyped,
runs in milliseconds, needs nothing but the built assembly and the committed DLLs. This is
the wave that turns "we cannot test anything here" into a real CI gate, and it is the wave
that already found 6 real bugs plus a callback collision plus a `ToString()` gap. Estimated
**2-3 days** including CI wiring and the golden files.

**Wave 2 — behavioural tests (techniques 4, 5).** Real test-writing against `Ugc.Query`,
`ServerList`, the value types, and `Dispatch`, plus generalising header-conformance beyond
callbacks. Estimated **1-2 weeks**, and it should be sequenced alongside the documentation
work in Part B because both require actually understanding each API's contract.

**Keep the live-Steam suite, but quarantine it.** `run-live-steam-tests.ps1` exists and is
sensibly written (it checks Steam is running, writes `steam_appid.txt`, defaults to a
read-only subset, uses `--blame-hang-timeout 90s`). Its header comment is correct that live
runs are the *only* proof of the native callback boundary. The problem is not that these
tests exist, it is that they are the *only* tests and that they are unconditionally coupled to
`[AssemblyInitialize]`. Split them into their own project so the offline suite can run
everywhere and the live suite runs on a tagged machine. (Note: that script says "NUnit" while
the projects use MSTest — fix the comment during the migration.)

---

### Recommended test project layout

The library multi-targets `net46;netstandard2.1;net6.0`. Test projects do **not** need to
mirror that. Test on **`net8.0`** (supported LTS) plus **`net48`** where you specifically want
to prove the `net46` surface still marshals correctly — that second target is worth it here
because Unity consumers are the main reason `net46` exists.

```
tests/
├── Directory.Build.props                 # shared TFMs, LangVersion, analyzers, pinned SDK
│
├── Facepunch.Steamworks.Conformance/     # net8.0  — NO Steam, NO network. CI gate.
│   ├── NativeExportTests.cs              #   technique 1  (PE export table)
│   ├── StructLayoutTests.cs              #   technique 2  (SizeOf/OffsetOf vs golden)
│   ├── CallbackIdTests.cs                #   technique 3  (uniqueness, DataSize, factory)
│   ├── EnumConformanceTests.cs           #   technique 4  (vs Generator/steam_sdk/*.h)
│   ├── ApiInvariantTests.cs              #   technique 6  (leaks, accessors, doc ratchet)
│   ├── Baselines/
│   │   ├── struct-sizes.txt              #   337 entries, generated this audit
│   │   ├── native-exports.txt            #   1089 entries, generated this audit
│   │   └── doc-coverage.json             #   per-namespace floors (Part B)
│   └── Infrastructure/
│       ├── PeExportReader.cs             #   ~150 LOC, written & working
│       └── SdkHeaderParser.cs            #   k_iCallback + enum extraction
│
├── Facepunch.Steamworks.Unit/            # net8.0;net48 — pure managed, NO Steam.
│   ├── UgcQueryBuilderTests.cs
│   ├── ServerListFilterTests.cs
│   ├── SteamIdTests.cs  GameIdTests.cs  AppIdTests.cs
│   ├── NetAddressTests.cs  NetIdentityTests.cs
│   ├── UtilityTests.cs                   #   IpToInt32/Int32ToIp — real assertions this time
│   └── DispatchBookkeepingTests.cs
│
└── Facepunch.Steamworks.Live/            # net8.0 — REQUIRES Steam. Not in default CI.
    ├── LiveTestBase.cs                   #   opt-in gate, per-class init, guaranteed cleanup
    ├── AppTests.cs  FriendsTests.cs  UserTests.cs  ...   (ported from today's suite)
    └── README.md                         #   account prerequisites, appid, what it mutates
```

**Framework: adopt xUnit** for the new projects.

| | xUnit | MSTest 3.x | NUnit |
|---|---|---|---|
| Per-test isolation | new class instance per test — kills the shared-static-state bugs this suite has | `[TestInitialize]` | `[SetUp]` |
| Opt-in skipping | `Skip` on `[Fact]`, `SkipUnless` in v3 — exactly what the live suite needs | `[Ignore]` (static only) | `Assert.Ignore` |
| Ecosystem default for new .NET | yes | – | – |
| Migration cost from current tests | low — the suite uses only `[TestClass]`/`[TestMethod]`/`Assert.*` | lowest | low |

MSTest 3.x is a perfectly defensible alternative if minimising churn matters more than
tooling; the decisive point is **abandoning `2.0.0-beta4`**, not which of the three replaces
it. xUnit's per-test class instantiation is the tiebreaker given how much shared mutable
state (`LastDispatchException`, `VoiceRecord`, leaked lobbies) the current suite carries.

**Supporting changes:**

- **Delete `packages.config`** and the unused `Newtonsoft.Json` references from both test csproj.
- **Split the two same-directory csproj** into separate directories to stop the `obj/` race,
  or collapse to one project with an x86/x64 dimension handled properly.
- **Add `Directory.Build.props`** under `tests/` pinning `Microsoft.NET.Test.Sdk` to an exact
  version and enabling `TreatWarningsAsErrors` for the test projects.
- **CI:** run `Conformance` + `Unit` on every push (they need no Steam, no network, and
  complete in seconds). Run `Live` manually or on a self-hosted runner with Steam installed,
  via the existing `run-live-steam-tests.ps1`.
- **Move the golden files into the repo** so a diff is reviewable in a PR.

---

## Part B: Documentation coverage

### Measurement method and its validation

The library builds with `<GenerateDocumentationFile>true</GenerateDocumentationFile>`,
producing `Facepunch.Steamworks/bin/Release/{net46,netstandard2.1,net6.0}/Facepunch.Steamworks.Win64.xml`
(220,329 bytes for `net6.0`).

Coverage was measured by a purpose-built tool that (a) loads the assembly through
`MetadataLoadContext` so no native dependency is touched, (b) enumerates the public surface by
reflection, (c) generates the ECMA-334 XML documentation ID for each member, and (d) binds
those IDs against the compiler-emitted XML.

**Validation of the measurement itself:**

```
XML <member> entries              : 728
Bound to a reflected member       : 728
UNBOUND (DocId generation gap)    : 0
```

**Every one of the 728 XML entries binds to a real member.** There is no doc-ID generation
gap, so the coverage numbers below are exact rather than approximate. (The gap between 728
XML entries and 678 documented *public* items is simply 50 entries documenting
internal/private members.)

One correction applied throughout: the compiler emits a synthetic `value__` backing field for
each of the 40 enums. Those 40 pseudo-members are **excluded** from every figure below.

### Measured coverage

#### Totals

| | Count | With `<summary>` | Coverage |
|---|---:|---:|---:|
| Public types | 141 | 41 | **29.1%** |
| Public members (non-type) | 1,731 | 637 | **36.8%** |
| **All public API items** | **1,872** | **678** | **36.2%** |
| All items **excluding enums entirely** | 1,183 | 670 | **56.6%** |

That last row matters: the headline 36.2% is dragged down almost entirely by enum members.
The *hand-written* surface is at 56.6%, and the primary facade classes are at 74.3%.

#### By member kind

| Kind | Total | Documented | Coverage |
|---|---:|---:|---:|
| Event | 66 | 60 | **90.9%** |
| Property | 347 | 231 | 66.6% |
| Method | 527 | 316 | 60.0% |
| Class | 42 | 21 | 50.0% |
| Struct | 57 | 20 | 35.1% |
| Field | 91 | 22 | 24.2% |
| **EnumField** | **649** | **8** | **1.2%** |
| **Enum (the types)** | **40** | **0** | **0.0%** |
| **Ctor** | **51** | **0** | **0.0%** |
| Interface | 2 | 0 | 0.0% |

Three categories are at or near **absolute zero**: all 40 enum types, all 51 public
constructors, and 641 of 649 enum members.

#### Parameters, returns, examples

| | Count | Documented | Coverage |
|---|---:|---:|---:|
| Members taking ≥1 parameter | 373 | 46 have **any** `<param>` | **12.3%** |
| Members with `<param>` for **every** parameter | 373 | 46 | 12.3% |
| Individual parameters needing a tag | 637 | 75 tagged | **11.8%** |
| Methods with a non-`void` return | 425 | 28 have `<returns>` | **6.6%** |
| Any item with `<example>` | 1,872 | **0** | **0.0%** |
| Any item with `<remarks>` | 1,872 | **0** | **0.0%** |

**There is not a single `<example>` or `<remarks>` tag in the entire library.** For a binding
whose selling point is "an emphasis on making things easy", and whose underlying API is
notoriously callback-driven and order-sensitive, zero worked examples is the single largest
gap in the documentation.

The `<param>`/`<returns>` numbers are worse than the summary numbers by a factor of ~5.
Where an author wrote a summary they almost never documented the arguments — and for a P/Invoke
binding the arguments are where the danger lives (buffer sizes, ownership, nullability).

#### Per namespace

| Namespace | Items | Documented | Coverage |
|---|---:|---:|---:|
| `Steamworks` | 1,391 | 467 | 33.6% |
| `Steamworks.Data` | 307 | 154 | 50.2% |
| `Steamworks.Ugc` | 178 | 51 | 28.7% |
| `Steamworks.ServerList` | 36 | 6 | **16.7%** |

#### Per facade class — the primary user-facing API

These are the static `SteamXxx` entry points most consumers touch first.

| Class | Members | Documented | Coverage |
|---|---:|---:|---:|
| `SteamApps` | 29 | 28 | **96.6%** |
| `SteamRemoteStorage` | 17 | 16 | 94.1% |
| `SteamTimeline` | 19 | 18 | 94.7% |
| `SteamUtils` | 31 | 29 | 93.5% |
| `SteamNetworkingUtils` | 27 | 25 | 92.6% |
| `SteamNetworking` | 13 | 12 | 92.3% |
| `SteamMusic` | 11 | 10 | 90.9% |
| `SteamClient` | 10 | 9 | 90.0% |
| `SteamServerStats` | 10 | 9 | 90.0% |
| `SteamMatchmaking` | 18 | 16 | 88.9% |
| `SteamScreenshots` | 8 | 7 | 87.5% |
| `SteamServer` | 39 | 34 | 87.2% |
| `SteamUser` | 35 | 30 | 85.7% |
| `SteamUserStats` | 21 | 18 | 85.7% |
| `SteamInput` | 6 | 5 | 83.3% |
| `SteamRemotePlay` | 6 | 5 | 83.3% |
| `SteamParties` | 5 | 4 | 80.0% |
| `SteamNetworkingSockets` | 17 | 13 | 76.5% |
| `SteamFriends` | 40 | 28 | 70.0% |
| `SteamInventory` | 20 | 14 | 70.0% |
| `SteamVideo` | 3 | 2 | 66.7% |
| `SteamUGC` | 17 | 9 | **52.9%** |
| `SteamServerInit` | 10 | 5 | 50.0% |
| `SteamNetworkingMessages` | 12 | 3 | **25.0%** |
| `SteamParental` | 8 | 1 | **12.5%** |
| **Total** | **471** | **350** | **74.3%** |

The facade layer is in decent shape. The problem is everything *behind* it — the enums,
structs, and builders those methods accept and return.

#### Coverage distribution across types

| | Count |
|---|---:|
| Types with **100%** member coverage | 15 |
| Types with **0%** member coverage | **72** (accounting for **770** undocumented members) |

72 types — over half the public type count — have not a single documented member. Those 72
types account for 770 of the 1,194 undocumented members, i.e. **64.5% of the total gap is
concentrated in types with zero coverage.** That is good news for planning: the work is
clustered, not smeared.

### Highest-impact documentation targets

Ranked by **undocumented member count** = `members × (1 − coverage)`.

| # | Type | Kind | Members | Documented | Coverage | Impact |
|---:|---|---|---:|---:|---:|---:|
| 1 | `Steamworks.CallbackType` | Enum | 217 | 0 | 0.0% | **217** |
| 2 | `Steamworks.Result` | Enum | 130 | 0 | 0.0% | **130** |
| 3 | `Steamworks.Ugc.Query` | Struct | 72 | 3 | 4.2% | **69** |
| 4 | `Steamworks.NetConnectionEnd` | Enum | 32 | 0 | 0.0% | 32 |
| 5 | `Steamworks.Ugc.Item` | Struct | 65 | 41 | 63.1% | 24 |
| 6 | `Steamworks.BroadcastUploadResult` | Enum | 24 | 0 | 0.0% | 24 |
| 7 | `Steamworks.Data.ServerInfo` | Struct | 28 | 6 | 21.4% | 22 |
| 8 | `Steamworks.Ugc.Editor` | Struct | 23 | 6 | 26.1% | 17 |
| 9 | `Steamworks.ParentalFeature` | Enum | 17 | 0 | 0.0% | 17 |
| 10 | `Steamworks.InputType` | Enum | 17 | 0 | 0.0% | 17 |
| 11 | `Steamworks.InputSourceMode` | Enum | 17 | 0 | 0.0% | 17 |
| 12 | `Steamworks.Data.Stat` | Struct | 16 | 0 | 0.0% | 16 |
| 13 | `Steamworks.UgcType` | Enum | 14 | 0 | 0.0% | 14 |
| 14 | `Steamworks.SteamFriends` | Class | 40 | 28 | 70.0% | 12 |
| 15 | `Steamworks.RoomEnter` | Enum | 12 | 0 | 0.0% | 12 |
| 16 | `Steamworks.Friend` | Struct | 31 | 19 | 61.3% | 12 |
| 17 | `Steamworks.AuthResponse` | Enum | 11 | 0 | 0.0% | 11 |
| 18 | `Steamworks.SteamNetworkingAvailability` | Enum | 10 | 0 | 0.0% | 10 |
| 19 | `Steamworks.SocketManager` | Class | 13 | 3 | 23.1% | 10 |
| 20 | `Steamworks.ConnectionManager` | Class | 20 | 10 | 50.0% | 10 |
| 21 | `Steamworks.SteamNetworkingMessages` | Class | 12 | 3 | 25.0% | 9 |
| 22 | `Steamworks.Relationship` | Enum | 9 | 0 | 0.0% | 9 |
| 23 | `Steamworks.NetDebugOutput` | Enum | 9 | 0 | 0.0% | 9 |
| 24 | `Steamworks.FriendState` | Enum | 9 | 0 | 0.0% | 9 |
| 25 | `Steamworks.Controller` | Struct | 12 | 3 | 25.0% | 9 |

**Reading this list — three distinct workstreams, not one:**

**(a) `Result` (#2) is the single highest-value target in the library, ahead of its rank.**
It is the return type threaded through nearly every async operation. A developer who gets
`Result.Fail` or `Result.LimitExceeded` back has no in-IDE explanation of what to do. 130
members, all undocumented, and unlike `CallbackType` they are *constantly* surfaced to users.
Valve's `EResult` list is largely self-describing by name, so this is high-value/low-difficulty
— do it first.

**(b) `CallbackType` (#1) is the biggest number but the lowest priority.** It is an internal
dispatch mechanism; consumers subscribe to typed events (`SteamFriends.OnPersonaStateChange`)
rather than reasoning about callback IDs. Documenting all 217 would move the headline
percentage more than any other single action while helping almost nobody. **Explicitly
deprioritise it** — and be aware that whoever reports "documentation coverage" will be tempted
by exactly this. A better treatment: one good `<summary>` on the *enum type* explaining what
callback IDs are and that the values mirror Valve's `k_iCallback`, plus the 1108/1112
collision note from Part A.

**(c) `Ugc.Query` (#3) is the best effort-to-value ratio in the library.** 72 members at
4.2% coverage, it is a fluent builder (so each method is one sentence), it is the entry point
for all Workshop functionality, and it is pure-managed — meaning **the same work session can
write its docs and its offline unit tests** (technique 5). `Ugc.Item` (#5), `Ugc.Editor` (#8)
and `UgcType` (#13) sit in the same subsystem: together **124 undocumented members in one
coherent area**. Doing UGC as a single project is far more efficient than picking off types by
rank.

**(d) The networking cluster** — `NetConnectionEnd` (#4), `SocketManager` (#19),
`ConnectionManager` (#20), `SteamNetworkingMessages` (#21), `SteamNetworkingAvailability`
(#18), `NetDebugOutput` (#23) — is **92 undocumented members**. `NetConnectionEnd` deserves
priority within it: it is what a developer reads when a connection drops, and it is the one
area where Valve's own headers are *good* (see below), so it can be documented with citations
rather than assumptions.

### Quality assessment

Presence is not quality. Of the 678 documented items:

| Summary length | Count |
|---|---:|
| 1–3 words | 16 |
| 4–7 words | 175 |
| 8–15 words | 237 |
| 16–40 words | 175 |
| 41+ words | 75 |
| **Median** | **11 words** |
| **Mean** | **19.5 words** |

A tautology heuristic (summary ≤8 words and ≥60% of its content words appear in the member
name) flags **27 of 637 documented members (4.2%)**. That is a *floor* — the detector only
catches short summaries, and manual reading finds more.

**The good news: this library's documentation is genuinely better than the raw percentage
suggests.** Where someone wrote a summary, it is usually a real explanation. The 41+ word
tier (75 items) contains material that is more useful than Valve's own headers.

#### Genuinely good — worth using as the house style

`SteamClient.IsLoggedOn` (110 words) explains the *consequence*, not the mechanism:

> "Checks if the current user's Steam client is connected to the Steam servers. If it's not,
> no real-time services provided by the Steamworks API will be enabled. The Steam client will
> automatically be trying to recreate the connection…"

`SteamNetworkingUtils.InitRelayNetworkAccess` (181 words, the longest in the library) tells
you *when to call it and why*, which is exactly the question Valve's header leaves open:

> "If you know that you are going to be using the relay network (for example, because you
> anticipate making P2P connections), call this to initialize the relay network. If you do not
> call this…"

`InventoryItem.ConsumeAsync` (87 words) leads with the irreversibility:

> "Consumes items from a user's inventory. If the quantity of the given item goes to zero, it
> is permanently removed. Once an item is removed it cannot be recovered."

`SteamUserStats.StoreStats` (178 words) documents the retry contract, and
`InventoryResult.Serialize` (117 words) explains the anti-forgery/replay design. These are
the standard the overhaul should hold everything to.

#### Bad — restatements that add nothing

- `SteamServer.LogOff` → **"Log off of Steam."** (Does it block? Can you log back on? What
  happens to connected players?)
- `SteamServer.Product` → **"Gets the current product."** (What *is* a product here? What
  format?)
- `SteamServer.MapName` → **"Gets or sets the current Map Name."**
- `SteamNetworkingSockets.ConnectRelay` → **"Connect to a relay server."** (On two overloads.)
- `Connection.UserData` → **"Get/Set connection user data."** (This is a 64-bit opaque slot
  with specific lifetime semantics — none of which is stated.)
- `SteamUtils.CurrentBatteryPower` → **"Returns battery power [0-1]."** — the summary
  describes intended behaviour that the code does not implement. **See the verified bug
  below.**
- `SteamParental.OnSettingsChanged` → **"Parental Settings Changed"** — not even a sentence.
- `SteamUtils.GetEnteredGamepadText` → **"Returns previously entered text."** (Valid when?
  After which callback? What if the user cancelled?)
- `SteamInventory.GetAllItemsAsync` → **"Get all items and return the InventoryResult"**
  (No mention that the result must be disposed, or that it can be `null`.)
- `SteamMatchmaking.OnLobbyMemberLeave` and `OnLobbyMemberDisconnected` → **both** carry the
  identical string **"Invoked when a lobby member leaves the lobby."** Two distinct events
  with one copy-pasted summary; a reader cannot tell them apart. This is the clearest single
  example of docs that are present but worse than useless.

#### A documented summary that exposed a live bug

Checking the `CurrentBatteryPower` summary against Valve's header turned up a genuine defect
in the implementation, not just the prose.

`Generator/steam_sdk/isteamutils.h:91` states the native contract: percentage `[0..100]`, with
`255` meaning on AC power. `Generated/Interfaces/ISteamUtils.cs:111` correctly returns `byte`.
But `Facepunch.Steamworks/SteamUtils.cs:132` is:

```csharp
/// <summary>
/// Returns battery power [0-1].
/// </summary>
public static float CurrentBatteryPower => Math.Min( Internal.GetCurrentBatteryPower() / 100, 1.0f );
```

`byte / 100` is **integer division**. Both operands promote to `int`, so the division truncates
*before* the `float` conversion. Measured behaviour:

| Native return | 0 | 1 | 50 | 75 | 99 | 100 | 255 (AC) |
|---|---|---|---|---|---|---|---|
| `CurrentBatteryPower` | 0 | 0 | 0 | 0 | **0** | 1 | 1 |

**The property can only ever return `0` or `1`.** Every battery level from 0% to 99% reports
`0`. The documented `[0-1]` range is right about the *intent* and the code never delivers it.
Fix: `... / 100.0f`.

This is worth calling out for two reasons. First, it is a real user-visible bug in shipped
code. Second, it is precisely the class of defect that **technique 5 (pure-managed tests)
would have caught on day one** — no Steam client, no account, no network, just three
assertions over a pure arithmetic expression. It is the strongest single argument in this
audit for the offline unit-test project.

#### The structural quality problem

Beyond wording: **0 `<example>`, 0 `<remarks>`, 6.6% `<returns>`, 11.8% `<param>`.** Even the
excellent long-form summaries above are prose blobs in a `<summary>` tag. `InitRelayNetworkAccess`
would be far more useful split into a one-line `<summary>` plus a `<remarks>` block plus an
`<example>` showing the call ordering. The overhaul should treat **tag structure** as a
first-class deliverable, not just word count.

### Areas where Valve's docs are inadequate and we must document assumptions

Measured across the 44 headers in `Generator/steam_sdk/` (17,596 lines). These are the places
where our documentation cannot cite Valve and must instead state an assumption explicitly.

**Overall: 295 of 955 interface methods (30.9%) carry no comment at all.**

| Header | Methods | Bare | Coverage |
|---|---:|---:|---:|
| `isteamnetworkingsockets.h` | 47 | 0 | **100.0%** |
| `isteamnetworkingmessages.h` | 6 | 0 | **100.0%** |
| `isteaminput.h` | 48 | 4 | 91.7% |
| `isteamuser.h` | 33 | 4 | 87.9% |
| `isteamutils.h` | 37 | 5 | 86.5% |
| `isteammatchmaking.h` | 93 | 16 | 82.8% |
| `isteaminventory.h` | 38 | 9 | 76.3% |
| `isteamapps.h` | 33 | 8 | 75.8% |
| `isteamfriends.h` | 80 | 34 | 57.5% |
| `isteamugc.h` | 94 | **51** | **45.7%** |
| `isteamuserstats.h` | 44 | **26** | **40.9%** |
| `isteamremotestorage.h` | 59 | **48** | **18.6%** |
| `isteamgameserverstats.h` | 10 | 10 | **0.0%** |
| `isteamparentalsettings.h` | 6 | 6 | **0.0%** |

**1. String pointer lifetime — the single most binding-relevant gap.**
Of **36** `const char *`-returning interface methods, only **3** say anything about lifetime or
ownership. **33 (91.7%) do not.** This covers `GetFriendPersonaName`, `GetClanName`,
`GetClanTag`, `GetFriendRichPresence`, `GetAchievementName`,
`GetAchievementDisplayAttribute`, `GetIPCountry`, `GetSteamUILanguage`,
`GetCurrentGameLanguage`, `GetLaunchQueryParam` and more. Nothing states whether the pointer
is a static buffer, how long it survives, or whether the next call invalidates it.
**Our docs must state: "the returned string is copied immediately; the native pointer is
assumed to be invalidated by the next call on the same interface."** That is an assumption,
and it should be labelled as one.

**2. Buffer sizes.** **29 of 37 (78.4%) buffer-output methods** never mention size, buffer,
bytes, length, or capacity in prose. `isteamugc.h` and `isteamuserstats.h` are at **100%
omission**. Separately, **40 of 43** `k_cch*`/`k_cb*` size constants carry no inline
explanation, and **14 of 43 are never referenced anywhere in any header** — declared and
abandoned. Only 14 comment lines in the whole SDK link a constant to a method. So for
`GetMostAchievedAchievementInfo(char *pchName, uint32 unNameBufLen, …)`, that
`k_cchStatNameMax = 128` applies is *our inference*. Document the inferred capacity for every
wrapped buffer call and say it is inferred.

**3. Threading.** **11 of the 12 major interfaces contain zero thread-safety statements.**
Only `isteamnetworkingsockets.h` addresses threading (4 statements). The only global claim
lives in `isteamclient.h:31-48` and concerns pipe management, not the per-interface getters.
Our docs must state the assumed model — API calls on the thread that pumps callbacks — and
flag it as assumption.

**4. Callback and CallResult behaviour.** The *type* is 100% machine-readable: all **82**
`SteamAPICall_t`-returning methods carry `STEAM_CALL_RESULT(...)`. The *behaviour* is not.
Re-firing, ordering, and failure paths are documented in roughly **six places SDK-wide**
(e.g. `isteamugc.h:380`, `isteamnetworking.h:172`, `isteammatchmaking.h:404`,
`isteamnetworkingmessages.h:150`). Concretely: `StoreStats` (`isteamuserstats.h:130`) does not
state the ordering between `UserStatsStored_t` and the N `UserAchievementStored_t` callbacks,
nor what fires when it returns `false`. `DownloadItem` (`isteamugc.h:352`) says nothing about
the `false` path or whether a duplicate call yields one result or two. **13 callback structs
have no comment at all**, including all three in `isteamhttp.h` and `NumberOfCurrentPlayers_t`.
This is where our async wrappers most need to state what they assume, because our
`Task`-based API has to pick *one* answer.

**5. `bool` failure conditions.** ~55 comment lines SDK-wide explain a `false` return, but
distribution is extreme: `isteamhttp.h` explains 10 of 25; `isteamugc.h` explains **one across
94 methods**; `isteamremotestorage.h` explains **one across 59**, and that one is about a
different function's out-parameter. Every `bool Set*`/`AddRequired*`/`SetReturn*` in
`isteamugc.h` (~25 methods) returns `bool` with no stated failure condition — and these are
exactly the `Ugc.Query`/`Ugc.Editor` builders that are documentation target #3 and #8.

**6. Errors in Valve's own machine-readable annotations.** Two found:
- `isteamugc.h:244` — the `STEAM_OUT_STRING_COUNT` annotation names `cchURLSize` while the
  actual size parameter is `cchOriginalFileNameSize`. **A generator that trusts these
  annotations emits the wrong capacity.** Ours does trust them. Worth a targeted check.
- `isteamapps.h:48` uses a closed index range `[0, GetDLCCount()]` where seven other
  index-range comments in the SDK use half-open (`isteamfriends.h:224,358`,
  `isteamuserstats.h:152`, `isteammatchmaking.h:67,127,167,185`). We must assume it is a typo.

**7. Deprecated and absent interfaces.** 90 deprecation markers across the headers vs. **0**
TODO/FIXME — absence of TODOs is not evidence of completeness.
- **`ISteamAppList` has no header in this SDK drop at all** (0 references anywhere). This is
  the root cause of the 6 dead entry points in Part A: **we cannot cite Valve for
  `ISteamAppList` in any form.**
- `ISteamNetworking` legacy P2P is deprecated wholesale (`isteamnetworking.h:128,145,216`).
- `isteamremotestorage.h`'s publish/enumerate block (~30 methods) is superseded by `ISteamUGC`
  with **no cross-reference in the header** — the only hint is
  `isteamugc.h:119`'s `k_EItemStateLegacyItem` comment. Our docs must supply the migration
  pointer Valve omits.
- `isteamugc.h` **contradicts itself** on deprecation: line 227 says the older call is replaced
  by "this", line 288 points at `CreateQueryUGCDetailsRequest` as the replacement — but that
  function is at line 228, above 288. The two comments point at each other.
- Reserved-parameter contracts are absent: `isteaminput.h:739`
  `RunFrame(bool bReservedValue = true)` is explained nowhere.

**8. Partner-site gating.** Only **10 lines in the entire SDK** mention that a function
depends on Steamworks partner-site configuration, and most name no page. Only **three URLs
appear in the whole SDK**. `isteamapps.h:42` defers to "your Valve technical contact";
`isteaminventory.h:248` and `isteamremoteplay.h:56,61` cite configuration with no destination.
For every wrapped API that silently returns empty because the appid is not configured
(`SteamInventory`, `SteamRemotePlay`, `SteamInput`, `SteamTimeline` icons,
`SteamUtils.CheckFileSignature`), **our documentation has to say so — Valve's does not.**
This is arguably the highest-value documentation we can add, because it converts a baffling
silent failure into a known prerequisite.

**Where Valve is good and we should cite rather than assume:**
`isteamnetworkingsockets.h` and `isteamnetworkingmessages.h` (100% coverage, real threading /
lifetime / callback-ordering prose, `///` Doxygen style — evidently a different author),
`isteamhttp.h`'s failure-case discipline, `isteamgameserver.h`'s `@see k_cb*` convention, and
the handful of exemplary buffer contracts at `isteamutils.h:85`, `isteamuser.h:169-172`,
`isteaminventory.h:105-108`.

---

## Prioritised recommendations

### P0 — do now, hours of work, no dependencies

1. **Delete the `Newtonsoft.Json` `PackageReference` from both test csproj.** It is completely
   unused (0 references in test source) and carries a HIGH-severity advisory
   (GHSA-5crp-9r3c-p9vr). Removes 2 of 4 `NU1903` build warnings for free.
2. **Bump `Generator`'s `Newtonsoft.Json` 9.0.1 → 13.0.3.** Real usage, same advisory. Removes
   the remaining 2 warnings.
3. **Delete `Facepunch.Steamworks.Test/packages.config`** — a pre-SDK leftover pinning
   `net46`/`net452` for a `net6.0` project.
4. **Fix `ISteamAppList`** — 6 entry points that do not exist in the shipped DLL, with a
   working accessor in front of them. Either bind the correct exports or remove the interface.
5. **Fix `SteamUtils.CurrentBatteryPower` (`SteamUtils.cs:132`).** Integer division
   (`byte / 100`) means the property returns `0` for every battery level from 0% to 99%.
   One-character fix: `/ 100` → `/ 100.0f`. Verified during this audit; see Part B.

### P1 — the conformance harness (2-3 days)

6. **Stand up `tests/Facepunch.Steamworks.Conformance/`** on xUnit/net8.0 with techniques
   1, 2, 3 and 6 — all four prototyped and working during this audit. Commit
   `struct-sizes.txt` (337 entries) and `native-exports.txt` (1,089 entries) as golden
   baselines. **Gate CI on it.** This converts "no test can run without Steam" into a real
   build gate that already catches 6 live bugs.
7. **Add the callback-collision allow-list test.** Assert every `CallbackType` value maps to
   exactly one struct except an explicit, commented allow-list containing 1108. Harmonise the
   generator's inconsistent handling of 1108 vs 1112 while you are there.
8. **Assert the three TFMs expose an identical public surface** — `net46` is currently one XML
   doc entry short of the other two, meaning conditional compilation is silently changing the
   API.

### P2 — documentation, in this order (the ordering is the recommendation)

9. **`Result` enum — 130 members.** Highest value in the library: it is the return type of
   nearly every async operation and users see it constantly. Largely self-describing from
   Valve's `EResult`, so low difficulty.
10. **The UGC cluster as one project — `Ugc.Query` (69) + `Ugc.Item` (24) + `Ugc.Editor` (17)
   + `UgcType` (14) = 124 undocumented members.** Do it *together with* the pure-managed unit
   tests (technique 5), because both require understanding the same contracts and `Ugc.Query`
   is a pure builder. Note that `isteamugc.h` is 45.7% bare and self-contradictory on
   deprecation, so budget time for stating assumptions.
11. **The networking cluster — 92 members.** `NetConnectionEnd` first: it is what developers
    read when a connection drops, and it is the one area where Valve's headers are excellent,
    so it can be documented with citations rather than assumptions.
12. **Add `<param>` and `<returns>` to the facade layer.** At 11.8% and 6.6% these are five
    times worse than summary coverage, and for a P/Invoke binding the arguments are where the
    danger is. Prioritise the 373 members that take parameters.
13. **Write the first `<example>` blocks.** There are currently **zero** in the library.
    Target the top ~15 entry-point flows (init/shutdown, lobby create-join, UGC publish, auth
    ticket, inventory grant). This is the highest-leverage thing available for a library whose
    stated purpose is ease of use.
14. **Sweep the summaries that are wrong rather than merely thin.** The clearest case is the
    identical copy-pasted summary on `SteamMatchmaking.OnLobbyMemberLeave` and
    `OnLobbyMemberDisconnected` — two distinct events a reader cannot tell apart. Once
    recommendation 5 lands, also rewrite `SteamUtils.CurrentBatteryPower`'s summary to state
    the AC-power (`255` native) case, which the current `[0-1]` phrasing hides. Audit the
    27 heuristically-flagged tautological summaries in the same pass.
15. **Document the partner-site prerequisites** for `SteamInventory`, `SteamRemotePlay`,
    `SteamInput`, `SteamTimeline` icons and `SteamUtils.CheckFileSignature`. Valve mentions
    this in only 10 lines SDK-wide and names almost no pages; ours would be the only place a
    developer can learn why the API silently returns nothing.
16. **Explicitly deprioritise `CallbackType` (217 members).** It is the largest number on the
    ranking and the lowest user value — an internal dispatch mechanism consumers never touch.
    Document the *type* well (including the 1108/1112 collisions), skip the members, and make
    sure nobody games the coverage metric with it.

### P3 — the test rewrite (1-2 weeks)

17. **Split the suite three ways** (`Conformance` / `Unit` / `Live`) per the layout above.
    Migrate to xUnit. Move the 118 existing tests into `Live` largely as-is.
18. **Fix the blocking defects in the live suite** before trusting a green run: the
    `1442910` vs `252490` app ID mismatch, the missing `game_actions_252490.vdf`, the
    `TestWin32` x64 mis-targeting, the shared output directory, and the un-timeboxed
    `while (true)` in `GameServerTest.PublicIp`.
19. **Add `[AssemblyCleanup]`/`IDisposable` teardown** and stop leaking: 100 MB cloud files,
    32 MB workshop uploads, three public lobbies per run, and a permanently-open microphone.
20. **Give the 53 assertion-free tests real assertions, or delete them.** 44.9% of the suite
    currently cannot fail. `UtilsTest` (13/14) and `FriendsTest` (8/8) are the worst.
21. **Adopt a documentation-coverage ratchet** in `ApiInvariantTests` using the per-namespace
    floors measured here (`Steamworks` 33.6%, `Steamworks.Data` 50.2%, `Steamworks.Ugc` 28.7%,
    `Steamworks.ServerList` 16.7%). Fail the build if coverage regresses; raise the floor as
    P2 lands.

---

### Appendix: measurement artefacts

Produced by the audit harness; all reproducible from a clean `dotnet build -c Release`.

| Artefact | Contents |
|---|---|
| `members.csv` | 1,912 rows — every public API item with kind, namespace, type, doc status, param/returns tags, summary word count, doc ID |
| `types-ranked.csv` | every public type ranked by `members × (1 − coverage)` |
| `dllimports.csv` | all 1,024 `DllImport` declarations with library and entry point |
| `struct-sizes.txt` | 337 `Marshal.SizeOf` values — **the proposed golden baseline** |
| `native-exports.txt` | 1,089 exported symbols parsed from `steam_api64.dll`'s PE export directory |
