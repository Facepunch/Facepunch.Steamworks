# Public API Coverage & Missing Features

**Date:** 2026-07-28
**Repo state:** `master` @ `6424be1` (plus the working-tree deletion of `ISteamAppList.cs` made by the
native-conformance pass — see [`00-native-conformance.md`](00-native-conformance.md))
**Method:** static analysis only. Every number below is produced by parsing
`Facepunch.Steamworks/Generated/Interfaces/ISteam*.cs`, the rest of the C# library, the PE export
table of the committed `steam_api64.dll`, and the SDK headers in `Generator/steam_sdk/`.
No Steam client, Steam account, or network access was used.
The solution builds clean at this state (`dotnet build …Win64.csproj` → 0 errors, 6 pre-existing warnings).

---

## Why this audit exists

`Facepunch.Steamworks` has two layers:

* **`Generated/Interfaces/ISteam*.cs`** — `internal` P/Invoke bindings, machine-generated from
  `Generator/steam_sdk/steam_api_flat.h` + `steam_api.json`. Near-total coverage of the SDK.
* **`Facepunch.Steamworks/Steam*.cs` + `Structs/` + `Networking/`** — the `public` API a game
  actually consumes. Hand-written, and *much* smaller.

The previous audit proved the *bindings* are sound. This one measures the second layer: **how much of
the Steamworks SDK a game using this library can actually reach**, and what it is missing.

The short answer is that roughly **half of the bound surface is unreachable from public code**, and
several of the gaps are features Valve shipped in the last few years that games actively want.

---

## Summary

| Metric | Value |
|---|---|
| Generated interface bindings | **35** (was 36; `ISteamAppList.cs` deleted this session) |
| ISteam member functions declared in `steam_api_flat.h` | **946** |
| …bound via `DllImport` | **906** (95.8%) |
| …never bound | **40** (4.2%) |
| Distinct `internal` C# methods across the 35 interfaces | **893** |
| …reachable from public API surface | **472** |
| **Public API coverage of the bound surface** | **52.9 %** |
| Coverage excluding structurally-unusable bindings¹ | **55.7 %** (472 / 847) |
| Interfaces with a real public wrapper | **24** |
| Interfaces with **zero** public reachability | **11** |
| Interfaces at 100 % coverage | **5** (`ISteamMusic`, `ISteamNetworkingMessages`, `ISteamParentalSettings`, `ISteamTimeline`, and `ISteamGameServerStats` at 88 %) |

¹ Excludes `ISteamClient` (35 methods, deliberately superseded by the flat `SteamAPI_*` entry points
the library actually uses) and the four `ISteamMatchmaking*Response` classes (11 methods, which are
C++ vtables *you implement*, not interfaces you call — see below).

**Reconciliation with `00-native-conformance.md`.** That document reports "911 of 946 bound, 40
unbound", which does not close (946 − 911 = 35). The 5-function discrepancy is exactly
`ISteamAppList`, whose entry points were bound but were **not** flat-API functions at all. With that
binding removed the arithmetic is now exact: 946 declared − 40 unbound = **906 bound**, verified by
counting `SteamAPI_ISteam*` `EntryPoint` strings in the tree (906, all present in the header and all
exported by the DLL).

**How "reachable" is defined.** A bound method counts as reachable if some file outside
`Generated/Interfaces/` calls it — via `Internal.X(…)`, `SteamFoo.Internal.X(…)`, or through a local
variable typed as an `ISteam*` interface (the only instance of the last form is
`Callbacks/CallResult.cs:17`, which uses `utils.IsAPICallCompleted` / `utils.GetAPICallResult`).
Commented-out call sites do **not** count. There were zero call sites the attribution pass could not
resolve, so the reachable set is exact rather than estimated.

---

## Per-interface coverage table

`flat` = flat-API functions bound for this interface. `unbnd` = flat-API functions Valve exports that
this library does not bind. `meth` = distinct `internal` C# method names (C++ overloads such as
`GetStat(int)` / `GetStat(float)` collapse to one name). `reach` = of those, how many are called from
public code.

| Interface | flat | unbnd | meth | reach | % | Notes |
|---|--:|--:|--:|--:|--:|---|
| `ISteamApps` | 33 | 0 | 33 | 27 | **82 %** | Missing the entire 2023 beta-branch API + `SetDlcContext`. |
| `ISteamClient` | 35 | 0 | 35 | 0 | **0 %** | Zero references anywhere outside its own file. `SteamClient.cs` uses `SteamAPI_Init`/`SteamInternal_*` instead. Dead but harmless. |
| `ISteamController` | 34 | 0 | 34 | 0 | **0 %** | Deprecated by Valve in favour of `ISteamInput`. Correctly unwrapped; candidate for deletion. |
| `ISteamFriends` | 80 | 0 | 80 | 51 | **64 %** | Missing clan-chat windows, friend groups, rich-presence enumeration, equipped profile items. |
| `ISteamGameSearch` | 14 | 0 | 14 | 0 | **0 %** | No wrapper. Gated by Valve (`k_EGameSearchErrorCode_Failed_NotAuthorized`). |
| `ISteamGameServer` | 41 | 0 | 41 | 26 | **63 %** | 3 of the 15 gaps are Valve-deprecated. `SetGameData`/`SetRegion`/`SetSpectatorPort` are real gaps. |
| `ISteamGameServerStats` | 10 | 0 | 8 | 7 | **88 %** | Only `UpdateUserAvgRateStat` missing. |
| `ISteamHTMLSurface` | 37 | 0 | 37 | 0 | **0 %** | No wrapper. Largest single unwrapped interface. |
| `ISteamHTTP` | 25 | 0 | 25 | 0 | **0 %** | No wrapper. |
| `ISteamInput` | 46 | **2** | 46 | 17 | **37 %** | Worst-covered high-value interface. No rumble, no LED, no gyro, no origin strings, no binding panel, no action-manifest path. |
| `ISteamInventory` | 38 | 0 | 35 | 23 | **66 %** | Missing dynamic item properties (`StartUpdateProperties` family) and promo-item eligibility. 2 of the gaps are Valve-deprecated. |
| `ISteamMatchmaking` | 38 | 0 | 38 | 36 | **95 %** | Best-covered large interface. Only `SetLinkedLobby` + `AddRequestLobbyListCompatibleMembersFilter`. |
| `ISteamMatchmakingPingResponse` | 2 | 0 | 2 | 0 | **0 %** | Structurally unusable — see below. |
| `ISteamMatchmakingPlayersResponse` | 3 | 0 | 3 | 0 | **0 %** | Structurally unusable. |
| `ISteamMatchmakingRulesResponse` | 3 | 0 | 3 | 0 | **0 %** | Structurally unusable. |
| `ISteamMatchmakingServerListResponse` | 3 | 0 | 3 | 0 | **0 %** | Structurally unusable. |
| `ISteamMatchmakingServers` | 17 | 0 | 17 | 10 | **59 %** | Per-server ping / rules / player queries unavailable; the library does raw A2S over UDP instead. |
| `ISteamMusic` | 9 | 0 | 9 | 9 | **100 %** | Complete. |
| `ISteamMusicRemote` | 32 | 0 | 32 | 0 | **0 %** | No wrapper. Not for games — see below. |
| `ISteamNetworking` | 9 | **13** | 9 | 6 | **67 %** | Whole interface deprecated by Valve, yet still wrapped by `SteamNetworking.cs`. |
| `ISteamNetworkingFakeUDPPort` | 4 | 0 | 4 | 0 | **0 %** | Wrapper file `SteamNetworkingFakeUDPPort.cs` is an **empty class**. Live dead-end (below). |
| `ISteamNetworkingMessages` | 6 | 0 | 6 | 6 | **100 %** | Complete. |
| `ISteamNetworkingSockets` | 47 | 0 | 47 | 26 | **55 %** | No SDR/hosted-server support, no certificates, no custom signalling, no poll-based `GetConnectionInfo`. |
| `ISteamNetworkingUtils` | 41 | 0 | 41 | 15 | **37 %** | Only ~18 hand-picked config values exposed as properties; the generic setters are `internal`. |
| `ISteamParentalSettings` | 6 | 0 | 6 | 6 | **100 %** | Complete. |
| `ISteamParties` | 12 | 0 | 12 | 7 | **58 %** | You can *join* a party beacon but not *create* one. |
| `ISteamRemotePlay` | 8 | 0 | 8 | 6 | **75 %** | Missing `BStartRemotePlayTogether` (the actual invite entry point). |
| `ISteamRemoteStorage` | 35 | **24** | 35 | 14 | **40 %** | The 24 unbound are legacy workshop (correct). The 21 unreached include async I/O, write streams, sync platforms, local-file-change. |
| `ISteamScreenshots` | 9 | 0 | 9 | 8 | **89 %** | Only `AddVRScreenshotToLibrary` missing. |
| `ISteamTimeline` | 18 | 0 | 18 | 18 | **100 %** | Complete — including game phases and recordings. |
| `ISteamUGC` | 93 | **1** | 91 | 66 | **73 %** | Missing content descriptors, per-tag query accessors, app dependencies, video previews, date-range filters, game-version filters. |
| `ISteamUser` | 33 | 0 | 33 | 24 | **73 %** | 2 of the 9 gaps are Valve-deprecated. `GetUserDataFolder`, `UserHasLicenseForApp`, `BSetDurationControlOnlineState` are real gaps. |
| `ISteamUserStats` | 44 | 0 | 38 | 34 | **89 %** | `RequestGlobalAchievementPercentages` gap causes a **live bug** (below). |
| `ISteamUtils` | 37 | 0 | 37 | 29 | **78 %** | Floating gamepad text input is written but **commented out**. |
| `ISteamVideo` | 4 | 0 | 4 | 1 | **25 %** | Only `IsBroadcasting` reachable. |
| ~~`ISteamAppList`~~ | — | — | ~~5~~ | 0 | — | Deleted during this session. Interface no longer exists in the SDK, the flat API, `steam_api.json`, or `steam_api64.dll`'s export table. |
| **TOTAL** | **906** | **40** | **893** | **472** | **52.9 %** | |

---

## Interfaces with no public wrapper

The brief's hypothesis was `ISteamHTTP`, `ISteamHTMLSurface`, `ISteamGameSearch`, `ISteamMusicRemote`,
`ISteamAppList`, `ISteamController`. **That list is correct but incomplete.** Add `ISteamClient`,
`ISteamNetworkingFakeUDPPort`, and the four `ISteamMatchmaking*Response` classes — 11 interfaces with
zero public reachability after the `ISteamAppList` removal.

Verification: `grep -rn "ISteamClient\|ISteamNetworkingFakeUDPPort\|ISteamMatchmaking*Response\|ISteamController\|ISteamHTTP\|ISteamHTMLSurface\|ISteamGameSearch\|ISteamMusicRemote"`
across the library, excluding `Generated/Interfaces/`, returns exactly one hit — a commented-out line
at `Facepunch.Steamworks/SteamServer.cs:114` (`//AddInterface<ISteamHTTP>();`).

### 1. `ISteamHTTP` — 25 functions

**What it does.** A general-purpose HTTP client that routes through the Steam client process rather
than the game's own socket stack. `Generator/steam_sdk/isteamhttp.h:3`: `"// Purpose: interface to
http client"`. Create a request handle from a verb + absolute URL, set headers / GET-POST params /
raw body / timeouts, send, then pull the response headers and body out of the handle. Available to
both clients (`SteamHTTP()`, `isteamhttp.h:146`) and dedicated servers (`SteamGameServerHTTP()`,
`isteamhttp.h:150`). No API key, no partner gating, no `STEAM_PRIVATE_API` anywhere in the header.

**Who needs it.** Two concrete audiences. (a) **Dedicated servers** that need to call a backend Web
API but must not link a second TLS stack — this is the one HTTP client guaranteed present on a Steam
game server. (b) Games shipping to consoles / restricted platforms via Steam where the platform's own
HTTP stack is awkward. For a normal PC game, `System.Net.Http` is simpler and better, so this is a
**niche-but-real** want rather than a headline feature.

**Effort: M.** All 25 bindings already exist. The work is a `Task`-returning request builder plus two
callback structs (`HTTPRequestCompleted_t`, and for streaming `HTTPRequestHeadersReceived_t` +
`HTTPRequestDataReceived_t`). Sharp edges the header calls out: cookie containers are
**process-lifetime only** (`isteamhttp.h:112-116`), streaming and non-streaming reads are mutually
exclusive (`isteamhttp.h:88-90`), streamed chunk offsets must be echoed back verbatim
(`isteamhttp.h:93-96`), and `GetHTTPRequestWasTimedOut` takes a `bool*` (1 byte native vs 4-byte
default C# interop `bool`) — the binding already handles this, but a wrapper must not re-break it.

**Recommendation: implement, P2.** Ship it as `SteamHTTP` with an async `Task<HttpResult>` façade.
Do the non-streaming path first; streaming can follow.

### 2. `ISteamHTMLSurface` — 37 functions

**What it does.** An off-screen Chromium surface that renders web pages into a raw BGRA buffer the
game blits itself. `Generator/steam_sdk/isteamhtmlsurface.h:19`: `"// Purpose: Functions for
displaying HTML pages and interacting with them"`. It renders nothing on its own — you forward mouse,
keyboard and scroll events in, and repaint from `HTML_NeedsPaint_t`.

**Who needs it.** In-game browsers, news panels, server MOTD pages, EULA displays. Historically used
by Garry's Mod and by server-browser MOTD screens in Source games. In 2026 most teams reach for a
platform webview or CEF directly, so demand is **low and shrinking**.

**Effort: L.** This is by far the most expensive unwrapped interface. It needs **21 new
`HTML_*_t` callback structs** (`isteamhtmlsurface.h:226`–`:470`), all declared with
`STEAM_CALLBACK_BEGIN`/`STEAM_CALLBACK_MEMBER` macros that a generator must expand by hand. Five of
them are *mandatory* or the browser hangs — `isteamhtmlsurface.h:37-41`: `"YOU MUST HAVE IMPLEMENTED
HANDLERS FOR HTML_BrowserReady_t, HTML_StartRequest_t, HTML_JSAlert_t, HTML_JSConfirm_t, and
HTML_FileOpenDialog_t"`. `FileLoadDialogResponse` (`isteamhtmlsurface.h:204`) takes a
null-terminated array of C strings that C# must build and pin. `HTML_NeedsPaint_t`'s BGRA pointer is
valid only until the next `SteamAPI_RunCallbacks` (`isteamhtmlsurface.h:236`), so the texture must be
copied out synchronously. Popups cannot be rendered at all (`isteamhtmlsurface.h:406-410`).

**Recommendation: defer, P2.** Only worth doing on concrete user demand. If it is done, budget it as
a standalone project, not a sprint item.

### 3. `ISteamGameSearch` — 14 functions

**What it does.** Steam-brokered matchmaking-as-a-service: players queue solo or as a lobby, hosts
advertise and request players to fill a match, Steam pairs them, and hosts report per-player results
back for reputation. Interface version string `SteamMatchGameSearch001`
(`Generated/Interfaces/ISteamGameSearch.cs:12`).

**Who needs it.** Almost nobody today. **There is no `isteamgamesearch.h` in
`Generator/steam_sdk/`** — the interface survives only in `steam_api_flat.h`, `steam_api.json`, and
the generated C#. The error enum contains `k_EGameSearchErrorCode_Failed_NotAuthorized`, which means
Valve authorises apps individually rather than exposing this freely. Valve's public documentation has
described it as work-in-progress for a specific partner, but that is **not verifiable from anything
in this repo** — treat it as unconfirmed.

**Effort: M** (14 thin methods + a handful of callbacks), but the payoff is zero for an unauthorised
app and it cannot be tested without Valve's approval.

**Recommendation: do not implement.** Revisit only if a user reports being authorised for it.

### 4. `ISteamMusicRemote` — 32 functions

**What it does.** Lets a **third-party music application register itself as Steam's music player**, so
Steam's Big Picture / overlay music controls drive that app instead of Steam Music. You publish a
display name and 64×64 PNG icon, declare which transport controls you support, then push now-playing
text, cover art, elapsed time, queue and playlist state up; Steam pushes user intent back down as 14
`MusicPlayer*_t` callbacks (`isteammusicremote.h:81-127`). The header has **no purpose comment at
all** — the class opens cold at `isteammusicremote.h:16`.

**Who needs it.** **No game, ever.** Every callback is an inbound *command to a music player*
(`MusicPlayerWantsPlayNext_t`, `MusicPlayerSelectsPlaylistEntry_t`, …), which is meaningless to a
game. The correct interface for a game is `ISteamMusic` — already at 100 % coverage — which lets you
read and duck the user's Steam Music. The absence of a wrapper here reflects a correct judgment call,
not an oversight.

**Effort: S/M** (mechanically simple), **value: zero.**

**Recommendation: do not implement.** Worth a one-line note in the README so it stops being
re-reported as a gap. Note also two latent bugs in Valve's header if anyone ever regenerates from it:
`MusicPlayerWantsVolume_t` (`:113`), `MusicPlayerSelectsQueueEntry_t` (`:117`) and
`MusicPlayerSelectsPlaylistEntry_t` (`:121`) are declared against `k_iSteamMusicCallbacks` rather
than `k_iSteamMusicRemoteCallbacks`, so their callback IDs collide with `ISteamMusic`'s range.

### 5. `ISteamController` — 34 functions

**What it does.** The predecessor of `ISteamInput`: the same action/action-set abstraction over 300+
controllers.

**Who needs it.** Nobody new. **Valve deprecated the entire interface**
(`Generator/steam_sdk/isteamcontroller.h:2-4`):

> `//    Note: The older ISteamController interface has been deprecated in favor of ISteamInput - this interface`
> `//			was updated in this SDK but will be removed from future SDK's.`

Reinforced at `isteamclient.h:118`: `"// Exposes the ISteamController interface - deprecated in favor
of Steam Input"`.

**Effort: N/A.**

**Recommendation: do not implement; delete the binding.** `ISteamController.cs` is 34 bindings to an
interface Valve says will be removed. Keeping it invites someone to wrap the wrong API. The
work that should happen instead is closing the `ISteamInput` gaps (§ below), which are the *same
features* on the supported interface.

### 6. `ISteamClient` — 35 functions

**What it does.** The root factory: creates the IPC pipe to the Steam client, creates/connects users
on it, and hands back versioned pointers to every other `ISteam*` interface.
`Generator/steam_sdk/isteamclient.h:18-25` explains you only need it *"if you have a more complex
versioning scheme, or if you want to implement a multiplexed gameserver where a single process is
handling multiple games at once"*.

**Who needs it.** Effectively nobody using this library. `SteamClient.cs:47` calls
`SteamAPI.Init(interfaceVersions, out var error)` (the flat `SteamInternal_SteamAPI_Init` path) and
`SteamClient.cs:63` uses `SteamAPI.GetHSteamPipe()`, which is the modern, supported way. Valve's own
header opens with a warning (`isteamclient.h:3-6`): `"// Internal low-level access to Steamworks
interfaces. // Most users of the Steamworks SDK do not need to include this file."` Five of its
members are wrapped in `STEAM_PRIVATE_API`, which `steam_api_common.h:50` expands to `protected:` —
i.e. Valve does not intend SDK consumers to call them at all. Five more carry
`"// NOT THREADSAFE - ensure that no other threads are accessing Steamworks API when calling"`
(`isteamclient.h:31, 35, 40, 44, 48`).

**Effort / recommendation: do not wrap.** Two members are individually worth a thought later:
`GetISteamGenericInterface` (an escape hatch for interfaces with no dedicated getter) and
`SetWarningMessageHook` (`isteamclient.h:110`) — the latter is the one function-pointer parameter in
this whole review and needs a delegate pinned for process lifetime. `ISteamUtils` has its own
`SetWarningMessageHook` (`ISteamUtils.cs:189`), which is the better home for that feature.

### 7. `ISteamNetworkingFakeUDPPort` — 4 functions

**What it does.** A drop-in UDP-socket replacement addressed by Valve-assigned FakeIP addresses, so
existing `sendto`/`recvfrom` code can run over Steam Datagram Relay with minimal changes. Defined at
`Generator/steam_sdk/steamnetworkingfakeip.h:29-78`; only forward-declared in
`isteamnetworkingsockets.h:14`. You obtain one from `ISteamNetworkingSockets::CreateFakeUDPPort`
(`isteamnetworkingsockets.h:914`).

**This one is a live dead-end, not merely a gap.** `SteamNetworkingSockets.cs:290` exposes:

```csharp
public static IntPtr CreateFakeUDPPort( int index )
```

…which hands the caller a raw native pointer, and `Facepunch.Steamworks/SteamNetworkingFakeUDPPort.cs`
is an **empty `internal class` with no members at all**. There is no way for a user to turn that
`IntPtr` into anything callable: `ISteamNetworkingFakeUDPPort` derives from `SteamInterface`, whose
`SetupInterface` (`Utility/SteamInterface.cs:26-48`) only ever populates `Self` from the three
`Get*InterfacePointer()` overrides — and `ISteamNetworkingFakeUDPPort.cs` overrides none of them, so
`Self` is permanently `IntPtr.Zero`. The public API therefore advertises a feature that cannot work.

**Who needs it.** Teams porting an existing UDP game to SDR without rewriting their transport. A real
but small audience — and the same teams are usually better served by `SteamNetworkingSockets`
proper.

**Effort: S.** Add an `internal void SetSelf(IntPtr)` (or a constructor overload) to `SteamInterface`,
build a small `public class FakeUdpPort : IDisposable` around the four methods, and change
`CreateFakeUDPPort` to return it. Watch the header's rules: the port object for a nonnegative index is
a **shared, non-reference-counted singleton** (`isteamnetworkingsockets.h:907-908`), so `Dispose`
must not blindly destroy it; only `DestroyFakeUDPPort` is thread-unsafe
(`steamnetworkingfakeip.h:33-35`); reliable send is unsupported (`steamnetworkingfakeip.h:44-46`);
max message size is 4096 with a recommended 1200-byte MTU (`steamnetworkingfakeip.h:15,19`).

**Recommendation: either finish it or remove the public `CreateFakeUDPPort`.** Shipping a public
method that returns an unusable pointer is worse than shipping nothing. **P1** on that basis alone.

### 8–11. `ISteamMatchmakingServerListResponse` / `PingResponse` / `PlayersResponse` / `RulesResponse` — 11 functions

**What they are.** Not interfaces you call — **pure abstract C++ classes your game implements**, whose
vtable pointer you hand to `ISteamMatchmakingServers`. `isteammatchmaking.h:266-274`: *"your game code
implements objects that implement these interfaces to receive callback notifications … This is
different than normal Steam callback handling due to the potentially large size of server lists."*
(There is no `isteammatchmakingservers.h` in this SDK drop; the four classes live in
`isteammatchmaking.h:266-378`.)

**Why the generated bindings can never work.** Same root cause as `ISteamNetworkingFakeUDPPort`: none
of the four generated classes has an interface accessor or a `GetUserInterfacePointer` override, so
`Self` is always `IntPtr.Zero` and every bound method would pass a null `this` into native code. They
are inert. The library sidesteps them entirely — `ServerList/Internet.cs:9` passes `IntPtr.Zero` as
the response object and then **polls** instead (`Internal.IsRefreshing`, `Internal.GetServerCount`,
`Internal.GetServerDetails` at `ServerList/Base.cs:129-130`), and gets server *rules* by speaking raw
A2S over UDP in `Utility/SourceServerQuery.cs`.

**Effort: L** to do properly — it means synthesising a native vtable, keeping the managed object and
its delegates alive, and honouring the lifetime contract Valve repeats on all four classes
(`isteammatchmaking.h:286-290`, `:311-314`, `:333-336`, `:360-363`): *"Failure to cancel in progress
queries when destructing a callback handler may result in a crash when a callback later occurs."*

**Recommendation: do not implement; delete the four generated files.** The polling design already
works and is far safer from managed code. Deleting these removes 11 methods of permanently-dead
binding and lifts headline coverage from 52.9 % to 53.6 % with zero functional change.

---

## Notable missing capabilities in partially-covered interfaces

Priorities: **P0** = ships-blocking for a modern title or fixes a live defect; **P1** = commonly
requested, clear value; **P2** = nice to have.

### `ISteamInput` — 17 / 46 (37 %) — the worst high-value gap in the library

The entire public surface is five members (`SteamInput.cs`: `RunFrame`, `Controllers`,
`GetDigitalActionGlyph`, `GetPngActionGlyph`, `GetSvgActionGlyph`) plus `Structs/Controller.cs`
(`InputType`, `ActionSet`, `ActivateLayer`, `DeactivateLayer`, `ClearLayers`, `GetDigitalState`,
`GetAnalogState`). That is action *input* only. Everything else is missing.

| Missing | Flat function | What it enables | Pri |
|---|---|---|:--:|
| Rumble | `SteamAPI_ISteamInput_TriggerVibration` (`ISteamInput.cs:350`), `TriggerVibrationExtended` (`:360`) | **Controller rumble. At all.** A game using this library today cannot vibrate a gamepad. | **P0** |
| Action-manifest path | `SetInputActionManifestFilePath` (`:54`) | Ship your own `.vdf` action manifest instead of relying on a Steam-hosted config. Required by most non-trivial Steam Input integrations. | **P0** |
| Origin display names | `GetStringForActionOrigin` (`:307`), `GetStringForDigitalActionName` (`:229`), `GetStringForAnalogActionName` (`:318`) | Text prompts ("Press A to jump") when you don't want glyph art. Glyphs are wrapped; strings are not. | **P0** |
| Analog origins | `GetAnalogActionOrigins` (`:263`) | Glyphs/prompts for analog actions. `GetDigitalActionOrigins` **is** wrapped — this is a straight asymmetry. | **P0** |
| Binding panel | `ShowBindingPanel` (`:411`) | Open Steam's rebinding UI from an options menu. Expected by Steam Deck reviewers. | **P1** |
| Gyro / accelerometer | `GetMotionData` (`:339`) | Motion aim. `MotionState` already exists in `Structs/Controller.cs:72` but is `internal` — the struct is there, the accessor is not. | **P1** |
| LED colour | `SetLEDColor` (`:380`) | DualShock/DualSense light bar. | **P1** |
| Simple haptics | `TriggerSimpleHapticEvent` (`:370`) | Steam Deck / Steam Controller haptic events. | **P1** |
| Explicit lifecycle | `Init` (`:30`), `Shutdown` (`:42`), `BNewDataAvailable` (`:89`), `BWaitForData` (`:77`), `EnableDeviceCallbacks` (`:111`) | Correct init ordering and low-latency polling. | **P1** |
| Active layer query | `GetActiveActionSetLayers` (`:184`) | Read back which layers are active; today layers are write-only. | **P2** |
| Xbox-origin bridging | `GetStringForXboxOrigin` (`:455`), `GetGlyphForXboxOrigin` (`:466`), `GetActionOriginFromXboxOrigin` (`:477`), `TranslateActionOrigin` (`:488`), `GetControllerForGamepadIndex` (`:433`), `GetGamepadIndexForController` (`:444`) | Migrating an existing XInput game incrementally. | **P2** |
| Remote Play / config | `GetRemotePlaySessionID` (`:511`), `GetSessionInputConfigurationSettings` (`:522`), `GetDeviceBindingRevision` (`:500`) | Per-session input attribution. | **P2** |

Also note the library currently uses the **legacy** glyph path: `GetDigitalActionGlyph`
(`SteamInput.cs:62`) returns `Internal.GetGlyphForActionOrigin_Legacy(origin)` at `SteamInput.cs:73`,
described by Valve at `isteaminput.h:847` as *"an older, Big Picture Mode-style PNG file"*. The modern
`GetGlyphPNGForActionOrigin` (`SteamInput.cs:88`) and `GetGlyphSVGForActionOrigin` are also wrapped,
so this is a default to revisit rather than a gap.

**DualSense adaptive triggers — `SetDualSenseTriggerEffect`.** Unbound (one of the 40).
Declaration at `isteaminput.h:932-933`. The parameter is
`const ScePadTriggerEffectParam*`, defined in Sony's `isteamdualsense.h:152-167`: a **120-byte**
struct (`static_assert( sizeof( ScePadTriggerEffectParam ) == 120 )`) containing an array of 2
`ScePadTriggerEffectCommand`, each holding a 7-member union of 48-byte parameter structs
(`isteamdualsense.h:128-136`). **Genuinely valuable** — adaptive triggers are the single most
requested DualSense-on-PC feature and no other C# Steamworks binding exposes them. **Effort: M.** No
function pointers involved; it is a fixed-size blob. The clean approach is an explicit-layout struct
with `fixed byte` buffers, size-asserted to 120, plus C# mirrors of the seven effect-parameter structs
and the mode enum (`isteamdualsense.h:23-31`). Caveat: the header carries a Sony copyright notice
(`isteamdualsense.h:1-5`, *"SIE CONFIDENTIAL"*) — the struct layout must be re-derived, and licensing
should be checked before vendoring the types verbatim. **P1.**

**Steam Input action events — `EnableActionEventCallbacks`.** Unbound. Declaration at
`isteaminput.h:787`, with Valve's rationale at `:784-786`: *"Enable SteamInputActionEvent_t callbacks.
Directly calls your callback function for lower latency than standard Steam callbacks. Supports one
callback at a time."* The parameter is a raw function pointer
(`typedef void (*SteamInputActionEventCallbackPointer)( SteamInputActionEvent_t * )`,
`isteaminput.h:714`), and the payload (`isteaminput.h:689-705`) is a `#pragma pack(1)` struct with an
**anonymous union** of two nested structs. **Effort: M**, harder than a normal binding: needs
`[StructLayout(Pack=1)]` + `[FieldOffset]` for the union, a delegate rooted for the lifetime of the
registration, and awareness that Steam invokes it from inside `RunFrame`/`SteamAPI_RunCallbacks`.
**Value: moderate** — it is a latency optimisation over polling, not a new capability, and polling is
already exposed. **P2.**

> Incidental finding for the Generator team: six comment lines in `isteaminput.h` (`:651, :686, :717,
> :775, :786, :929`) use a stray `\` instead of `//`. Line `:786` is exactly the explanatory note for
> `EnableActionEventCallbacks`. If the Generator's doc-comment scraper keys on `//`, it silently drops
> them.

### `ISteamUGC` — 66 / 91 (73 %)

Playtime tracking, favourites and child-item dependencies are **all present** (`StartPlaytimeTracking`,
`StopPlaytimeTracking`, `StopPlaytimeTrackingForAllItems`, `AddItemToFavorites`,
`RemoveItemFromFavorites`, `AddDependency`, `RemoveDependency`, `GetQueryUGCChildren`,
`SetReturnChildren`, `SetReturnPlaytimeStats` are all reachable). The real gaps:

| Missing | Flat function(s) | What it enables | Pri |
|---|---|---|:--:|
| Content descriptors (mature-content tagging) | `AddContentDescriptor` (`ISteamUGC.cs:809`), `RemoveContentDescriptor` (`:821`), `GetQueryUGCContentDescriptors` (`:287`), `GetUserContentDescriptorPreferences` (`:1151`) | Valve's mandated adult/violence tagging for workshop items (`EUGCContentDescriptorID`, `isteamugc.h:162-168`) and honouring the user's filter preference. Missing this means a workshop-heavy game **cannot comply** with Valve's content-descriptor policy from C#. | **P0** |
| Per-tag query accessors | `GetQueryUGCNumTags` (`:100`), `GetQueryUGCTag` (`:112`), `GetQueryUGCTagDisplayName` (`:126`) | Read an item's tags properly. Today tags come only from the truncated `m_rgchTags` string in `SteamUGCDetails_t`, and there is no way to get **localised** tag display names. | **P0** |
| App dependencies | `AddAppDependency` (`:1084`), `RemoveAppDependency` (`:1095`), `GetAppDependencies` (`:1106`) | Mark an item as requiring a particular DLC/app. Distinct from `AddDependency` (child items), which *is* wrapped. | **P1** |
| Query date filters | `SetTimeCreatedDateRange` (`:532`), `SetTimeUpdatedDateRange` (`:544`) | "New this week" / "recently updated" browsing. | **P1** |
| Query language | `SetLanguage` (`:445`) | Return item titles/descriptions in the user's language. | **P1** |
| Video previews | `AddItemPreviewVideo` (`:758`), `UpdateItemPreviewVideo` (`:784`), `UpdateItemPreviewFile` (`:771`) | Image previews can be added and removed but never **updated in place**, and YouTube video previews are unavailable. `Structs/UgcAdditionalPreview.cs` already models the read side. | **P1** |
| Tag groups | `AddRequiredTagGroup` (`:324`) | OR-within-group / AND-across-group tag filtering. `AddRequiredTag`/`AddExcludedTag`/`SetMatchAnyTag` are wrapped; groups are not. | **P2** |
| Required game versions | `SetRequiredGameVersions` (`:833`), `GetNumSupportedGameVersions` (`:260`), `GetSupportedGameVersionData` (`:272`) | Pin a workshop item to a game branch range. | **P2** |
| Bulk kv-tag clear | `RemoveAllItemKeyValueTags` (`:706`) | `RemoveItemKeyValueTags(key)` is wrapped; the clear-all variant is not. | **P2** |
| Cloud-filename filter | `SetCloudFileNameFilter` (`:482`) | Query by legacy cloud filename. | **P2** |
| Dedicated-server workshop | `BInitWorkshopForGameServer` (`:1007`) | Let a dedicated server download workshop content. Requested regularly by community-server games. | **P1** |
| Admin queries | `SetAdminQuery` (`:470`) | Return hidden items (moderation tooling). | **P2** |

`RequestUGCDetails` is the one unbound `ISteamUGC` function — correctly omitted, see the legacy
section.

### `ISteamApps` — 27 / 33 (82 %)

| Missing | Flat function(s) | What it enables | Pri |
|---|---|---|:--:|
| Beta-branch API | `GetNumBetas` (`ISteamApps.cs:380`), `GetBetaInfo` (`:392`), `SetActiveBeta` (`:408`) | Enumerate the beta branches the user can access (with flags, build ID, name, description) and switch branch from inside the game. `GetCurrentBetaName` is wrapped, so games can read the branch but neither list nor change it. This is the 2023 API set that a game with public betas wants. | **P1** |
| DLC context | `SetDlcContext` (`:369`) | Tell Steam which DLC the running code belongs to — required for correctly attributing playtime/telemetry in DLC-heavy titles. | **P1** |
| Legacy CD keys | `RequestAppProofOfPurchaseKey` (`:191`), `RequestAllProofOfPurchaseKeys` (`:309`) | Valve labels these legacy at `isteamapps.h:55` (*"Request legacy cd-key…"*). Correctly omitted. | — |

`BIsTimedTrial` **is** wrapped and reachable — the timed-trial gap in the brief does not exist.

### `ISteamUtils` — 29 / 37 (78 %)

Text filtering (`InitFilterText`/`FilterText`, `SteamUtils.cs:275,284`), Steam Deck detection
(`IsRunningOnSteamDeck`, `SteamUtils.cs:291`) and the classic gamepad text input are all present.

| Missing | Flat function(s) | What it enables | Pri |
|---|---|---|:--:|
| Floating gamepad text input | `ShowFloatingGamepadTextInput` (`ISteamUtils.cs:413`), `DismissFloatingGamepadTextInput` (`:435`) | The **Steam Deck** on-screen keyboard that floats over the game rather than replacing the screen. The wrapper is **already written and commented out** at `SteamUtils.cs:299-302`. This is the single cheapest high-value fix in the whole library. | **P0** |
| Dismiss classic input | `DismissGamepadTextInput` (`:447`) | Programmatically close the full-screen keyboard. | **P1** |
| IPv6 connectivity | `GetIPv6ConnectivityState` (`:389`) | Decide whether to advertise IPv6 endpoints. | **P2** |
| Warning hook | `SetWarningMessageHook` (`:189`) | Route Steam's diagnostics into the game's log. Needs a pinned delegate. | **P2** |
| Misc | `GetAppID` (`:122`), `GetIPCCallCount` (`:178`), `GetAPICallFailureReason` (`:155`) | `GetAppID` duplicates `SteamClient.AppId`; the other two are diagnostics. | **P2** |

### `ISteamUser` — 24 / 33 (73 %)

Encrypted app tickets (`RequestEncryptedAppTicket`, `GetEncryptedAppTicket`),
`GetAuthTicketForWebApi` (`SteamUser.cs:387`), duration control (`GetDurationControl`) and the full
voice API are **all present**. The brief's suspicions here are unfounded — this is one of the
better-covered interfaces. Note Valve draws a hard line between the two ticket types
(`isteamuser.h:130`: `"// not to be used for \"ISteamUserAuth\\AuthenticateUserTicket\" - it will
fail"`), and both are exposed, which is correct.

| Missing | Flat function(s) | What it enables | Pri |
|---|---|---|:--:|
| Duration-control online state | `BSetDurationControlOnlineState` (`ISteamUser.cs:388`) | Required for Chinese anti-addiction compliance — you must tell Steam whether the player is in online play. `GetDurationControl` is wrapped, the setter is not, so the feature is half-implemented. | **P1** |
| User data folder | `GetUserDataFolder` (`:96`) | Per-user writable directory. Common request; trivial. | **P1** |
| License check | `UserHasLicenseForApp` (`:227`) | Server-side DLC ownership check. | **P1** |
| Market eligibility | `GetMarketEligibility` (`:365`) | Gate in-game trading UI. | **P2** |
| Game badge level | `GetGameBadgeLevel` (`:283`) | Cosmetic. | **P2** |
| `GetHSteamUser` (`:29`), `TrackAppUsageEvent` (`:84`) | | Internal plumbing. | — |
| `InitiateGameConnection_DEPRECATED` (`:63`), `TerminateGameConnection_DEPRECATED` (`:74`) | | Valve-deprecated — see below. | — |

### `ISteamFriends` — 51 / 80 (64 %)

Rich presence *writing* (`SetRichPresence`, `ClearRichPresence`), *reading* one key
(`GetFriendRichPresence`), follower counts (`GetFollowerCount`, `IsFollowing`,
`EnumerateFollowingList`), and all four overlay activations
(`ActivateGameOverlay`, `…ToUser`, `…ToWebPage`, `…ToStore`) are present. The brief's suspicions are
mostly unfounded. Real gaps:

| Missing | Flat function(s) | What it enables | Pri |
|---|---|---|:--:|
| Rich-presence enumeration | `GetFriendRichPresenceKeyCount` (`ISteamFriends.cs:539`), `GetFriendRichPresenceKeyByIndex` (`:550`), `RequestFriendRichPresence` (`:561`) | Read a friend's *whole* rich-presence dictionary rather than guessing key names, and request it for a friend not currently cached. Today `GetFriendRichPresence` only works if you already know the key. | **P1** |
| Remote Play Together invite | `ActivateGameOverlayRemotePlayTogetherInviteDialog` (`:849`) | The invite dialog for Remote Play Together. Pairs with the missing `ISteamRemotePlay.BStartRemotePlayTogether`. | **P1** |
| Connect-string invite | `ActivateGameOverlayInviteDialogConnectString` (`:872`) | Invite friends to an arbitrary connect string (no lobby required). | **P1** |
| Friends groups | `GetFriendsGroupCount` (`:163`), `GetFriendsGroupIDByIndex` (`:174`), `GetFriendsGroupName` (`:185`), `GetFriendsGroupMembersCount` (`:196`), `GetFriendsGroupMembersList` (`:207`) | The user's own friend categories, for invite UIs. | **P2** |
| Clan chat windows | `OpenClanChatWindowInSteam` (`:722`), `CloseClanChatWindowInSteam` (`:734`), `IsClanChatWindowOpenInSteam` (`:710`), `IsClanChatAdmin` (`:698`), `LeaveClanChatRoom` (`:640`), `GetChatMemberByIndex` (`:662`) | `JoinClanChatRoom`, `SendClanChatMessage` and `GetClanChatMessage` **are** wrapped, so group chat is joinable and readable but you can never leave a room or enumerate its members. Asymmetric and worth closing. | **P1** |
| Clan activity | `DownloadClanActivityCounts` (`:285`), `GetClanActivityCounts` (`:274`) | Online/in-game/chatting counts for a group. | **P2** |
| Equipped profile items | `RequestEquippedProfileItems` (`:883`), `BHasEquippedProfileItem` (`:895`), `GetProfileItemPropertyString` (`:906`), `GetProfileItemPropertyUint` (`:917`) | Animated avatars, avatar frames, profile backgrounds — increasingly expected in social UIs. | **P2** |
| Misc | `HasFriend` (`:218`), `SetPersonaName` (`:40`), `SetInGameVoiceSpeaking` (`:330`), `GetUserRestrictions` (`:492`), `GetFriendCoplayGame` (`:617`), `GetFriendCoplayTime` (`:606`), `GetNumChatsWithUnreadPriorityMessages` (`:838`) | `HasFriend` in particular is a one-liner people re-implement by iterating the friends list. | **P2** |

The library currently uses `GetPlayerNickname`, which Valve marks deprecated at
`isteamfriends.h:250`: *"DEPRECATED: GetPersonaName follows the Steam nickname preferences, so apps
shouldn't need to care about nicknames explicitly."* Worth revisiting.

### `ISteamNetworkingUtils` — 15 / 41 (37 %)

Ping locations and estimated ping **are** covered (`GetLocalPingLocation`,
`EstimatePingTimeBetweenTwoLocations`, `EstimatePingTimeFromLocalHost`, `ParsePingLocationString`,
`ConvertPingLocationToString`, `CheckPingDataUpToDate`, `InitRelayNetworkAccess`) — the brief's
concern there is unfounded. The config-value concern is **well founded**.

| Missing | Flat function(s) | What it enables | Pri |
|---|---|---|:--:|
| Generic config setters | `SetGlobalConfigValueInt32` (`ISteamNetworkingUtils.cs:231`), `…Float` (`:243`), `…String` (`:255`), `…Ptr` (`:268`), `SetConnectionConfigValueInt32` (`:280`), `…Float` (`:292`), `…String` (`:304`), `SetConfigValueStruct` (`:401`) | `SteamNetworkingUtils.cs` exposes ~18 *hand-picked* config values as properties (`FakeSendPacketLoss`, `NagleTime`, `SendRateMax`, the debug levels, …) but the generic `SetConfigValue` is `internal` (`SteamNetworkingUtils.cs:406`). Anything Valve adds later — ICE/STUN transport selection, symmetric connect, SDR ticket overrides, per-connection tuning — is **unreachable without a library change**. This is the single most limiting design decision in the networking layer. | **P0** |
| POP / data-centre info | `GetPOPCount` (`:153`), `GetPOPList` (`:164`), `GetPingToDataCenter` (`:131`), `GetDirectPingToPOP` (`:142`) | Pick a relay region, show server-region latency in a UI. | **P1** |
| Config introspection | `GetConfigValueInfo` (`:423`), `IterateGenericEditableConfigValues` (`:434`) | Build a debug/tuning UI. | **P2** |
| Global callbacks | `SetGlobalCallback_SteamNetConnectionStatusChanged` (`:317`), `…AuthenticationStatusChanged` (`:329`), `…RelayNetworkStatusChanged` (`:341`), `…FakeIPResult` (`:353`), `…MessagesSessionRequest` (`:365`), `…MessagesSessionFailed` (`:377`) | Lower-latency callback delivery than the dispatch loop. The library uses the callback-struct path instead, which is the right default. | **P2** |
| FakeIP helpers | `GetIPv4FakeIPType` (`:208`), `GetRealIdentityForFakeIP` (`:219`) | Resolve a FakeIP back to a SteamID. | **P2** |
| Address parsing | `SteamNetworkingIPAddr_ToString` (`:445`), `…ParseString` (`:458`), `…GetFakeIPType` (`:470`), `SteamNetworkingIdentity_ParseString` (`:494`) | `Structs/NetAddress.cs` implements these in managed code, so mostly redundant. | — |

### `ISteamNetworkingSockets` — 26 / 47 (55 %)

Poll groups **are** wrapped (`CreatePollGroup` at `Networking/SocketManager.cs:30`,
`SetConnectionPollGroup`, `ReceiveMessagesOnPollGroup`, `DestroyPollGroup`) and FakeIP is partly
wrapped (`BeginAsyncRequestFakeIP`, `GetFakeIP`, `CreateListenSocketP2PFakeIP`). Gaps:

| Missing | Flat function(s) | What it enables | Pri |
|---|---|---|:--:|
| Poll-based connection info | `GetConnectionInfo` (`ISteamNetworkingSockets.cs:204`) | Query a connection's identity, remote address, state and end-reason **on demand**. Today `Networking/ConnectionInfo.cs` is only ever populated from the status-changed callback, so a game that missed or discarded the callback cannot ask again. `GetConnectionRealTimeStatus` and `GetDetailedConnectionStatus` are wrapped; this one is not. | **P0** |
| Authentication status | `InitAuthentication` (`:286`), `GetAuthenticationStatus` (`:297`) | Detect "cannot obtain a cert / not logged in" *before* connections start failing. Valve's recommended startup check. | **P1** |
| SDR hosted servers | `ConnectToHostedDedicatedServer` (`:377`), `CreateHostedDedicatedServerListenSocket` (`:421`), `GetHostedDedicatedServerAddress` (`:410`), `GetHostedDedicatedServerPort` (`:388`), `GetHostedDedicatedServerPOPID` (`:399`), `GetGameCoordinatorServerLogin` (`:432`), `FindRelayAuthTicketForServer` (`:366`), `ReceivedRelayAuthTicket` (`:355`) | The entire Steam Datagram Relay hosted-server model — DDoS-protected dedicated servers with hidden IPs. Big feature, but only usable by studios running SDR-hosted fleets. | **P2** |
| Certificates | `GetCertificateRequest` (`:467`), `SetCertificate` (`:479`), `ResetIdentity` (`:490`) | Offline/self-signed cert flows for LAN or non-Steam identity. | **P2** |
| Custom signalling | `ConnectP2PCustomSignaling` (`:443`), `ReceivedP2PCustomSignal` (`:455`) | Bring-your-own rendezvous (e.g. your own matchmaking backend brokering the P2P handshake). Needs a native vtable shim, like the matchmaking response classes — hence expensive. | **P2** |
| Loopback | `CreateSocketPair` (`:252`) | In-process client/server for tests and single-player-hosted sessions. Cheap and genuinely useful for CI. | **P1** |
| Misc | `GetListenSocketAddress` (`:240`), `GetRemoteFakeIPForConnection` (`:543`), `SendMessageToConnection` (`:160`, superseded by the wrapped `SendMessages`), `RunCallbacks` (`:500`, handled by `Dispatch`) | | **P2** |

### `ISteamRemoteStorage` — 14 / 35 (40 %)

Cloud quota **is** exposed (`SteamRemoteStorage.cs:105-141`: `QuotaBytes`, `QuotaUsedBytes`,
`QuotaRemainingBytes`) — the brief's concern there is unfounded. Real gaps:

| Missing | Flat function(s) | What it enables | Pri |
|---|---|---|:--:|
| Async file I/O | `FileWriteAsync` (`ISteamRemoteStorage.cs:54`), `FileReadAsync` (`:66`), `FileReadAsyncComplete` (`:79`) | Non-blocking cloud saves. Today `FileWrite`/`FileRead` are synchronous and will hitch the frame on a large save. For a game with multi-megabyte saves this is the difference between a smooth autosave and a visible stall. | **P0** |
| Write streams | `FileWriteStreamOpen` (`:141`), `FileWriteStreamWriteChunk` (`:154`), `FileWriteStreamClose` (`:166`), `FileWriteStreamCancel` (`:178`) | Write files larger than the 100 MB single-call limit, and stream saves without buffering the whole thing in memory. | **P1** |
| Write batches | `BeginFileWriteBatch` (`:422`), `EndFileWriteBatch` (`:434`) | Group a multi-file save so Steam syncs it atomically instead of racing mid-write. Valve recommends this for any game writing more than one file per save. | **P1** |
| Sync platforms | `SetSyncPlatforms` (`:129`), `GetSyncPlatforms` (`:239`) | Restrict a file to specific OSes (e.g. don't sync a Windows-only config to a Deck). | **P1** |
| Local file change | `GetLocalFileChangeCount` (`:399`), `GetLocalFileChange` (`:410`) | The "dynamic sync" flow — react to Steam changing files under you while the game runs, which is how Steam Deck's suspend/resume cloud handoff works. | **P1** |
| Legacy UGC download | `UGCDownload` (`:319`), `UGCDownloadToLocation` (`:387`), `UGCRead` (`:354`), `GetUGCDetails` (`:343`), `GetUGCDownloadProgress` (`:331`), `GetCachedUGCCount` (`:365`), `GetCachedUGCHandle` (`:376`), `FileShare` (`:116`) | Bound but unreached. Superseded by `ISteamUGC`. Leave unwrapped. | — |

### `ISteamUserStats` — 34 / 38 (89 %) — contains a live bug

`Structs/Achievement.cs:125-136` exposes `GlobalUnlocked`, which calls
`GetAchievementAchievedPercent`. **Nothing in the library ever calls
`RequestGlobalAchievementPercentages`** (`ISteamUserStats.cs:428`). Valve is explicit at
`isteamuserstats.h:260-261`: *"Will return -1 if there is no data on achievement percentages (ie, you
haven't called RequestGlobalAchievementPercentages and waited on the callback)."* So
`Achievement.GlobalUnlocked` returns `-1.0f` unconditionally. **P0 — this is a defect, not a gap.**

| Missing | Flat function(s) | What it enables | Pri |
|---|---|---|:--:|
| Global % prerequisite | `RequestGlobalAchievementPercentages` (`:428`) | Makes `Achievement.GlobalUnlocked` actually work. | **P0** |
| Rarity iteration | `GetMostAchievedAchievementInfo` (`:439`), `GetNextMostAchievedAchievementInfo` (`:452`) | "Rarest achievement" UIs. Same prerequisite. | **P1** |
| Progress limits | `GetAchievementProgressLimits` (`:540`) | Read the min/max of a progress-stat achievement so you can render a bar without hard-coding. | **P1** |

### `ISteamGameServer` — 26 / 41 (63 %)

| Missing | Flat function(s) | What it enables | Pri |
|---|---|---|:--:|
| Server metadata | `SetGameData` (`ISteamGameServer.cs:256`), `SetRegion` (`:267`) | `SetGameData` is the standard channel for the server-browser "gametype" filter string that most community-server games rely on. | **P1** |
| Spectator | `SetSpectatorPort` (`:202`), `SetSpectatorServerName` (`:212`) | SourceTV-style spectator advertising. | **P2** |
| Auth extras | `CancelAuthTicket` (`:320`), `RequestUserGroupStatus` (`:342`), `AssociateWithClan` (`:408`), `ComputeNewPlayerCompatibility` (`:419`) | `BeginAuthSession`/`EndAuthSession` are wrapped but the server cannot cancel its own tickets. Clan association drives "official server" badging. | **P1** |
| Security / restart | `BSecure` (`:116`), `WasRestartRequested` (`:139`) | VAC-secure status and "an update is available, restart" — both routinely needed by server operators. | **P1** |
| Deprecated | `GetGameplayStats` (`:353`), `GetServerReputation` (`:363`), `SendUserConnectAndAuthenticate_DEPRECATED` (`:431`), `SendUserDisconnect_DEPRECATED` (`:453`), `CreateUnauthenticatedUserConnection` (`:442`) | Correctly omitted — see below. | — |

### `ISteamInventory` — 23 / 35 (66 %)

Consumption (`ConsumeItem`), exchange (`ExchangeItems`), pricing (`GetItemPrice`,
`GetItemsWithPrices`, `RequestPrices`) and definitions (`LoadItemDefinitions`,
`GetItemDefinitionIDs`, `GetItemDefinitionProperty`) are **all present**. The brief's concerns are
unfounded. Real gaps:

| Missing | Flat function(s) | What it enables | Pri |
|---|---|---|:--:|
| Dynamic properties | `StartUpdateProperties` (`ISteamInventory.cs:390`), `SetProperty` (`:415`), `RemoveProperty` (`:402`), `SubmitUpdateProperties` (`:468`) | Mutable per-item state — kill counters on a weapon, name tags, stickers. This is the whole "item has a story" feature set and it is entirely unreachable. | **P1** |
| Promo eligibility | `RequestEligiblePromoItemDefinitionsIDs` (`:310`), `GetEligiblePromoItemDefinitionIDs` (`:322`), `AddPromoItems` (`:189`) | `AddPromoItem` (singular) and `GrantPromoItems` are wrapped; the eligibility query and the bulk variant are not. | **P2** |
| Lookup / inspect | `GetItemsByID` (`:117`), `InspectItem` (`:480`), `GetResultTimestamp` (`:71`) | Fetch specific items without a full inventory pull; open an inspect link. | **P2** |
| Deprecated | `SendItemDropHeartbeat` (`:236`), `TradeItems` (`:259`) | Correctly omitted — see below. | — |

### Smaller interfaces

| Interface | Missing | What it enables | Pri |
|---|---|---|:--:|
| `ISteamParties` 7/12 | `GetNumAvailableBeaconLocations` (`ISteamParties.cs:77`), `GetAvailableBeaconLocations` (`:89`), `CreateBeacon` (`:100`), `ChangeNumOpenSlots` (`:133`), `GetBeaconLocationData` (`:157`) | You can **join** a party beacon but not **create** one. The feature is half a feature — `Structs/PartyBeacon.cs` models joining only. | **P1** |
| `ISteamRemotePlay` 6/8 | `BStartRemotePlayTogether` (`ISteamRemotePlay.cs:97`), `BGetSessionClientResolution` (`:85`) | Starting a Remote Play Together session — the *actual* entry point. Sessions can be enumerated but never started. | **P1** |
| `ISteamVideo` 1/4 | `GetVideoURL` (`ISteamVideo.cs:29`), `GetOPFSettings` (`:51`), `GetOPFStringForApp` (`:62`) | 360°/OPF video playback. Niche. | **P2** |
| `ISteamMatchmakingServers` 10/17 | `PingServer` (`:169`), `PlayerDetails` (`:180`), `ServerRules` (`:191`), `CancelServerQuery` (`:202`), `RefreshServer` (`:159`), `RefreshQuery` (`:126`), `RequestSpectatorServerList` (`:84`) | Per-server queries. All four require a callback-object vtable (see §8-11), which is why they are absent; the library uses raw A2S over UDP in `Utility/SourceServerQuery.cs` instead. `RefreshServer`/`RefreshQuery` do **not** need a vtable and are cheap wins for a server browser. | **P2** |
| `ISteamGameServerStats` 7/8 | `UpdateUserAvgRateStat` (`:106`) | Server-side average-rate stats. Trivial. | **P2** |
| `ISteamScreenshots` 8/9 | `AddVRScreenshotToLibrary` (`:122`) | VR stereo screenshots. | **P2** |
| `ISteamMatchmaking` 36/38 | `SetLinkedLobby` (`:455`), `AddRequestLobbyListCompatibleMembersFilter` (`:150`) | Party↔game lobby linking; filter lobbies by compatible members. Everything else — all lobby data, member limits, member data, chat, filters, favourites — is covered. | **P2** |

**Steam Timeline is fully covered.** `ISteamTimeline` is 18/18 including
`AddInstantaneousTimelineEvent`, `AddRangeTimelineEvent`, `StartGamePhase`/`EndGamePhase`,
`SetGamePhaseAttribute`, `OpenOverlayToGamePhase` and `DoesGamePhaseRecordingExist`, wrapped by
`SteamTimeline.cs` with supporting structs `Structs/GamePhaseRecordingInfo.cs`. No work needed.

---

## Correctly-omitted legacy surface

40 exported functions are never bound. Every one of them is correctly omitted, and 37 of the 40 have
direct evidence in Valve's own headers.

### `ISteamNetworking` — 13 unbound socket functions (unambiguously correct)

Valve deprecates the **entire interface** at
`Generator/steam_sdk/isteamnetworking.h:128-130`:

> `// NOTE: This interface is deprecated and may be removed in a future release of`
> `///      the Steamworks SDK.  Please see ISteamNetworkingSockets and`
> `///      ISteamNetworkingMessages`

and the socket block specifically at `isteamnetworking.h:216-217`:

> `// These APIs are deprecated, and may be removed in a future version of the Steamworks`
> `// SDK.  See ISteamNetworkingSockets.`

All 13 unbound functions (`CreateListenSocket` `:230`, `CreateP2PConnectionSocket` `:236`,
`CreateConnectionSocket` `:237`, `DestroySocket` `:242`, `DestroyListenSocket` `:244`,
`SendDataOnSocket` `:251`, `IsDataAvailableOnSocket` `:256`, `RetrieveDataFromSocket` `:262`,
`IsDataAvailable` `:268`, `RetrieveData` `:276`, `GetSocketInfo` `:279`, `GetListenSocketInfo` `:283`,
`GetSocketConnectionType` `:286`, `GetMaxPacketSize` `:289`) sit under that comment. **Correctly
omitted.**

**However** — the *P2P* half of the same deprecated interface **is** bound and **is** wrapped by
`SteamNetworking.cs` (6/9 reachable). Valve deprecates it too, at `isteamnetworking.h:145-146`:
*"These APIs are deprecated, and may be removed in a future version of the Steamworks SDK. See
ISteamNetworkingMessages."* Since `ISteamNetworkingMessages` is already at **100 %** coverage, the
right move is to mark `SteamNetworking` `[Obsolete]` with a pointer to `SteamNetworkingMessages`,
rather than extend it. (`AllowP2PPacketRelay` carries an extra warning at `isteamnetworking.h:196-198`
that Valve may relay traffic regardless of the flag.)

### `ISteamRemoteStorage` — 24 unbound legacy workshop functions (correct, but on weaker header evidence)

These are the pre-`ISteamUGC` publishing API: `PublishWorkshopFile`,
`CreatePublishedFileUpdateRequest`, the seven `UpdatePublishedFile*` calls,
`CommitPublishedFileUpdate`, `GetPublishedFileDetails`, `DeletePublishedFile`, the five
`Enumerate*` calls, `Subscribe`/`UnsubscribePublishedFile`, the three vote-detail calls,
`PublishVideo`, `SetUserPublishedFileAction`, `EnumeratePublishedFilesByUserAction`.

**Be precise about the evidence: `isteamremotestorage.h` does not carry a deprecation comment on
them.** The block is headed only `// publishing UGC` (`isteamremotestorage.h:264`). The "legacy"
status is asserted from the `ISteamUGC` side:

* `isteamugc.h:119` — `k_EItemStateLegacyItem = 2,	// item was created with ISteamRemoteStorage`
* `isteamugc.h:197` — `// Size of the primary file (for legacy items which only support one file).`
* `isteamugc.h:343` — `// if k_EItemStateLegacyItem is set, pchFolder contains the path to the legacy file itself (not a folder)`
* `isteamremotestorage.h:354` / `:368` — `// k_iSteamRemoteStorageCallbacks + 8 is deprecated! Do not reuse` and the same for `+ 10`

**Verdict: correctly omitted.** `ISteamUGC` supersedes all of it and is 73 % covered; binding the old
publishing API would be actively harmful. But the justification is "superseded", not "Valve marked it
deprecated in this header" — do not overclaim in changelogs.

### `ISteamUGC::RequestUGCDetails` — 1 unbound (unambiguously correct)

`Generator/steam_sdk/isteamugc.h:288`:

> `// DEPRECATED - Use CreateQueryUGCDetailsRequest call above instead!`

and at the replacement, `isteamugc.h:227`: *"Query for the details of the given published file ids
(the RequestUGCDetails call is deprecated and replaced with this)"*. `CreateQueryUGCDetailsRequest` is
already wrapped and reachable. **Correctly omitted; do not add it.**

### `ISteamInput` — 2 unbound (**incorrectly** omitted)

`EnableActionEventCallbacks` and `SetDualSenseTriggerEffect` are the only two unbound functions Valve
does **not** deprecate. Both are current, both are useful, and both were almost certainly skipped
because the Generator cannot handle a function pointer and a Sony-defined 120-byte struct. See the
`ISteamInput` section above for the full assessment. **These are the only two of the 40 that should be
added.**

### Bound-but-deliberately-unwrapped deprecated surface

These are bound (so they count against coverage) but correctly have no public wrapper:

| Function | Header evidence |
|---|---|
| `ISteamUser::InitiateGameConnection_DEPRECATED`, `TerminateGameConnection_DEPRECATED` | `isteamuser.h:51`, `:58` — *"DEPRECATED! This function will be removed from the SDK in an upcoming version."* |
| `ISteamGameServer::SendUserConnectAndAuthenticate_DEPRECATED`, `SendUserDisconnect_DEPRECATED` | `isteamgameserver.h:234-235`, `:248-249` — *"DEPRECATED! … Please migrate to BeginAuthSession and related functions."* |
| `ISteamGameServer::GetGameplayStats`, `GetServerReputation` | `isteamgameserver.h:178-179` — *"these two functions s are deprecated, and will not return results"* |
| `ISteamInventory::SendItemDropHeartbeat` | `isteaminventory.h:238` — *"Deprecated. Calling this method is not required for proper playtime accounting."* |
| `ISteamInventory::TradeItems` | `isteaminventory.h:253` — *"Deprecated. This method is not supported."* |
| `ISteamApps::RequestAppProofOfPurchaseKey`, `RequestAllProofOfPurchaseKeys` | `isteamapps.h:55` — *"Request legacy cd-key for yourself or owned DLC."* |
| `ISteamInput::Legacy_TriggerHapticPulse`, `Legacy_TriggerRepeatedHapticPulse` | `isteaminput.h:879`, `:883` — *"if you are approximating rumble you may want to use TriggerVibration instead."* |
| `ISteamClient::RunFrame`, `ISteamUtils::RunFrame` | `isteamclient.h:97`, `isteamutils.h:107` — *"Deprecated. Applications should use SteamAPI_RunCallbacks() … instead."* |

Two places where the library **uses** something Valve marks legacy, worth revisiting:

* `ISteamFriends::GetPlayerNickname` — reachable; `isteamfriends.h:250` says *"DEPRECATED:
  GetPersonaName follows the Steam nickname preferences…"*
* `ISteamInput::GetGlyphForActionOrigin_Legacy` — called at `SteamInput.cs:73`; `isteaminput.h:847`
  describes it as *"an older, Big Picture Mode-style PNG file"*. The modern PNG/SVG variants are also
  wrapped, so this is just a default to flip.

### `ISteamAppList` — resolved during this session

The interface no longer exists in `isteamclient.h`, `steam_api_flat.h`, `steam_api.json`, or the
export table of the committed `steam_api64.dll`. Its 6 bindings were the only entry points in the tree
that did not exist natively; they have been removed (working tree shows
`D  Facepunch.Steamworks/Generated/Interfaces/ISteamAppList.cs`). Nothing further to do.

---

## Prioritised backlog

Ordered by (value to a real shipping game) ÷ (effort), with defects first. Effort is **S** ≈ under a
day, **M** ≈ a few days, **L** ≈ a sprint or more. Almost everything is S/M because the low-level
bindings already exist — the work is wrapper code, not interop plumbing.

| # | Item | Interface | Effort | Why first |
|--:|---|---|:--:|---|
| 1 | Call `RequestGlobalAchievementPercentages` before serving `Achievement.GlobalUnlocked` | `ISteamUserStats` | **S** | **Live defect.** `Structs/Achievement.cs:125` currently always returns `-1`. Valve states the prerequisite at `isteamuserstats.h:260-261`. |
| 2 | Uncomment + finish `ShowFloatingGamepadTextInput` / add `DismissFloatingGamepadTextInput` | `ISteamUtils` | **S** | Steam Deck on-screen keyboard. The code already exists, commented out, at `SteamUtils.cs:299-302`. Cheapest real feature in the repo. |
| 3 | `TriggerVibration` + `TriggerVibrationExtended` + `SetLEDColor` + `TriggerSimpleHapticEvent` | `ISteamInput` | **S** | A game on this library **cannot rumble a controller**. Four one-line wrappers. |
| 4 | `SetInputActionManifestFilePath`, `GetStringForActionOrigin`, `GetStringForDigitalActionName`, `GetStringForAnalogActionName`, `GetAnalogActionOrigins`, `ShowBindingPanel` | `ISteamInput` | **S** | Completes a *usable* Steam Input integration. `GetAnalogActionOrigins` closes a straight asymmetry with the already-wrapped `GetDigitalActionOrigins`. |
| 5 | Public generic config API (`SetGlobalConfigValue*`, `SetConnectionConfigValue*`) | `ISteamNetworkingUtils` | **S** | Unblocks every future Valve networking knob without a library release. Today `SetConfigValue` is `internal` (`SteamNetworkingUtils.cs:406`). |
| 6 | Either implement `FakeUdpPort` properly or remove the public `CreateFakeUDPPort` | `ISteamNetworkingFakeUDPPort` | **S** | `SteamNetworkingSockets.cs:290` returns an `IntPtr` that cannot be used, because `SteamNetworkingFakeUDPPort.cs` is an empty class and `Self` can never be set. Shipping a broken public method is worse than shipping none. |
| 7 | Delete `ISteamController.cs` and the four `ISteamMatchmaking*Response` files | — | **S** | 45 methods of permanently-dead binding: one interface Valve says will be removed (`isteamcontroller.h:2-4`), four that can never have a valid `Self`. Raises coverage to ~55 % with zero behaviour change. |
| 8 | UGC content descriptors + per-tag query accessors | `ISteamUGC` | **M** | `AddContentDescriptor`, `RemoveContentDescriptor`, `GetQueryUGCContentDescriptors`, `GetUserContentDescriptorPreferences`, `GetQueryUGCNumTags`, `GetQueryUGCTag`, `GetQueryUGCTagDisplayName`. Required to comply with Valve's mature-content policy and to read tags without parsing a truncated string. |
| 9 | `GetConnectionInfo` + `InitAuthentication`/`GetAuthenticationStatus` | `ISteamNetworkingSockets` | **S** | Poll-based connection state (today only available via a callback you may have missed) and a proper pre-flight auth check. |
| 10 | Async cloud I/O: `FileWriteAsync`, `FileReadAsync`, `FileReadAsyncComplete` | `ISteamRemoteStorage` | **M** | Removes the frame hitch on cloud saves. Needs `Task` plumbing over `RemoteStorageFileWriteAsyncComplete_t` / `…ReadAsyncComplete_t`. |
| 11 | Beta-branch API: `GetNumBetas`, `GetBetaInfo`, `SetActiveBeta`, plus `SetDlcContext` | `ISteamApps` | **S** | Modern, frequently requested, four thin wrappers (`ISteamApps.cs:369-408`). |
| 12 | Party beacon creation: `CreateBeacon`, `GetAvailableBeaconLocations`, `GetNumAvailableBeaconLocations`, `ChangeNumOpenSlots`, `GetBeaconLocationData` | `ISteamParties` | **S** | Turns a half-feature into a feature — today you can join a beacon but never create one. |
| 13 | `BStartRemotePlayTogether` + `ActivateGameOverlayRemotePlayTogetherInviteDialog` | `ISteamRemotePlay` / `ISteamFriends` | **S** | Remote Play Together cannot currently be *started* from this library. |
| 14 | `SetDualSenseTriggerEffect` (bind + wrap) | `ISteamInput` | **M** | One of only two non-deprecated unbound functions. 120-byte fixed-layout struct (`isteamdualsense.h:152-167`), no function pointers. Differentiating feature — no other C# binding exposes it. Check the Sony copyright header before vendoring types. |
| 15 | Rich-presence enumeration + clan-chat symmetry | `ISteamFriends` | **S** | `GetFriendRichPresenceKeyCount`/`ByIndex`/`RequestFriendRichPresence`; `LeaveClanChatRoom`, `GetChatMemberByIndex`, `IsClanChatAdmin`, `OpenClanChatWindowInSteam`. Closes asymmetries in already-wrapped features. |
| 16 | Cloud write streams + write batches + sync platforms + local file change | `ISteamRemoteStorage` | **M** | Large saves, atomic multi-file saves, per-OS filtering, Deck suspend/resume handoff. |
| 17 | `GetUserDataFolder`, `UserHasLicenseForApp`, `BSetDurationControlOnlineState` | `ISteamUser` | **S** | Three thin wrappers; `BSetDurationControlOnlineState` completes the half-implemented Chinese-compliance duration-control feature. |
| 18 | Server essentials: `SetGameData`, `BSecure`, `WasRestartRequested`, `CancelAuthTicket`, `AssociateWithClan` | `ISteamGameServer` | **S** | Standard requests from community-server games. |
| 19 | Inventory dynamic properties: `StartUpdateProperties`, `SetProperty`, `RemoveProperty`, `SubmitUpdateProperties` | `ISteamInventory` | **M** | The entire mutable-item-state feature is unreachable. |
| 20 | UGC quality-of-life: app dependencies, date-range filters, `SetLanguage`, `UpdateItemPreviewFile`, `AddItemPreviewVideo`, `BInitWorkshopForGameServer` | `ISteamUGC` | **M** | Rounds out the workshop story; `BInitWorkshopForGameServer` is a recurring request. |
| 21 | `CreateSocketPair`, `GetPOPList`/`GetPingToDataCenter` | `ISteamNetworkingSockets` / `Utils` | **S** | In-process loopback (great for CI/tests) and region-latency UIs. |
| 22 | `EnableActionEventCallbacks` (bind + wrap) | `ISteamInput` | **M** | The other non-deprecated unbound function. Latency optimisation over already-available polling — real, but not a new capability. Needs a rooted delegate and a `[FieldOffset]` union. |
| 23 | Mark `SteamNetworking` `[Obsolete]`, point at `SteamNetworkingMessages` | `ISteamNetworking` | **S** | Valve deprecated the interface (`isteamnetworking.h:128-130`) and the replacement is already 100 % covered. |
| 24 | `SteamHTTP` wrapper (non-streaming first) | `ISteamHTTP` | **M** | Mostly for dedicated servers that need an HTTP client without a second TLS stack. |
| 25 | SDR hosted-server support | `ISteamNetworkingSockets` | **L** | Big feature, small audience — only studios running SDR-hosted fleets. |
| 26 | `SteamHTMLSurface` wrapper | `ISteamHTMLSurface` | **L** | 21 new callback structs, five of them mandatory (`isteamhtmlsurface.h:37-41`). Do only on concrete demand. |
| — | `ISteamMusicRemote`, `ISteamGameSearch`, `ISteamClient`, `ISteamController` | — | — | **Will not implement.** Not for games / Valve-gated / explicitly internal / deprecated. Document the decision so they stop being re-reported. |

**Items 1–7 alone** are roughly one engineer-week, fix one live defect and one broken public method,
and deliver controller rumble, the Steam Deck keyboard, a usable Steam Input integration and an
open-ended networking config API. That is the highest-leverage slice by a wide margin.
