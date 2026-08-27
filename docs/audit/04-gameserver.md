# GameServer / Dedicated Server Audit

**Repo:** `C:\Users\admin\Desktop\Claude Cowork Global\Facepunch.Steamworks` (AnkleBreaker Studio fork of Facepunch.Steamworks)
**Ground truth:** `Generator/steam_sdk/*.h` (Steamworks SDK as vendored in this repo — `SteamGameServer015` / `SteamGameServerStats001`)
**Method:** static analysis + full solution build. No Steam client, no Steam account, no live game server. Nothing below was runtime-verified.
**Build:** `dotnet build Facepunch.Steamworks.sln` → **Build succeeded, 0 errors, 16 warnings** (SDK 8.0.423). All three platform projects (Win32/Win64/Posix) and both test projects compile.

---

## Summary

The generated P/Invoke layer (`Generated/Interfaces/ISteamGameServer.cs`, `ISteamGameServerStats.cs`) is a faithful, complete 1:1 binding of the headers — every method in `isteamgameserver.h` and `isteamgameserverstats.h` is present with the right signature and the right accessor. **Interface *routing* is correct in the generated layer for every interface** (§ matrix below). The bugs are all one level up, in the hand-written `SteamServer` / `SteamServerStats` / `ServerList` facades and in the shared-interface resolution policy.

The most serious problems, in order:

1. **`SteamServer.Init` registers `SteamApps`, which has no game-server accessor in the SDK.** The result is a live managed interface object whose native `Self` is `IntPtr.Zero`, published into `SteamApps.InterfaceServer`. Any `SteamApps.*` call on a pure dedicated server passes a NULL `this` to the flat API → access violation, not a managed exception. `SteamServer.AddInterface<T>` (unlike `SteamClient.AddInterface<T>`) ignores the `false` return from `InitializeInterface`, so nothing catches it.
2. **The fork's own `SteamServer.GetAuthSessionTicket` has no working cancel path.** `AuthTicket.Cancel()` unconditionally calls `SteamUser.Internal.CancelAuthTicket` — the *client* interface. On a dedicated server `SteamUser.Internal` is `null` → `NullReferenceException` in `Cancel()`/`Dispose()`. Server-issued tickets can never be cancelled; `ISteamGameServer::CancelAuthTicket` is bound but unreachable from public API.
3. **`SteamServer.Shutdown()` does not reset the cached property statics**, so a second `Init` in the same process silently skips `ModDir`, `GameDescription`, `Product` and `MaxPlayers` — properties the SDK says "must be set before calling LogOn."
4. **Auth sessions are never guaranteed to be ended.** `BeginAuthSession`/`EndSession` are unpaired manual calls with no registry, no auto-end on a deny response, and no cleanup on shutdown. `SteamServer.BeginAuthSession` also throws away the `BeginAuthResult` enum and returns `bool`, so the caller cannot distinguish `InvalidTicket` from `DuplicateRequest` from `GameMismatch`.
5. **Listen-server processes get the wrong pointer for every shared interface.** `SteamSharedClass<T>.Interface => InterfaceClient ?? InterfaceServer` prefers the client pointer whenever a client is also initialized. The fork made this strictly worse by adding `SteamNetworkingMessages` to both `SteamClient.Init` and `SteamServer.Init`.
6. **The Posix build ships no `libsteam_api.so`.** `Facepunch.Steamworks.Posix.csproj` has no `ItemGroup` at all, produces no NuGet package, and shares `bin/` with the Windows projects. `bin/Debug/net6.0/` contains `steam_api.dll` and `steam_api64.dll` and no `.so`. A headless Linux dedicated server cannot be deployed from this repo's build output without manual staging.

Good news worth stating plainly: the server-browser code **deliberately avoids** the classic native-holds-a-managed-callback crash by passing `IntPtr.Zero` for every `ISteamMatchmakingServerListResponse*` and polling instead. There is no GCHandle to get wrong. The remaining server-browser risk is a use-after-free on the `HServerListRequest` handle between `Dispose()` and the async pump, not a callback-object lifetime bug.

---

## Interface routing matrix

Server-side validity determined by `STEAM_DEFINE_GAMESERVER_INTERFACE_ACCESSOR` in the vendored headers, cross-checked against the exported symbols in `Generator/steam_sdk/steam_api_flat.h`. The complete set of game-server accessors in this SDK is exactly nine:

```
isteamgameserver.h:273        SteamGameServer            → SteamAPI_SteamGameServer_v015
isteamgameserverstats.h:67    SteamGameServerStats       → SteamAPI_SteamGameServerStats_v001
isteamhttp.h:151              SteamGameServerHTTP        → SteamAPI_SteamGameServerHTTP_v003
isteaminventory.h:368         SteamGameServerInventory   → SteamAPI_SteamGameServerInventory_v003
isteamnetworking.h:299        SteamGameServerNetworking  → SteamAPI_SteamGameServerNetworking_v006
isteamnetworkingmessages.h:190 SteamGameServerNetworkingMessages_SteamAPI → ..._v002
isteamnetworkingsockets.h:942  SteamGameServerNetworkingSockets_SteamAPI  → ..._v012
isteamugc.h:407               SteamGameServerUGC         → SteamAPI_SteamGameServerUGC_v020
(+ ISteamUtils via SteamAPI_SteamGameServerUtils_v010, steam_api_flat.h:192)
```

`ISteamNetworkingUtils` is a **global** accessor (`SteamAPI_SteamNetworkingUtils_SteamAPI_v004`), valid from either side. Everything else is `STEAM_DEFINE_USER_INTERFACE_ACCESSOR` — client only.

| Interface | Valid client-side | Valid server-side | Routed correctly? | Evidence |
|---|---|---|---|---|
| `ISteamGameServer` | ✗ | ✓ | **Yes** | `ISteamGameServer.cs:21` `GetServerInterfacePointer() => SteamAPI_SteamGameServer_v015()`; no user accessor. Facade `SteamServer : SteamServerClass<>` (`SteamServer.cs:14`) throws `NotSupportedException` if `SetInterface(false, …)` (`SteamInterface.cs:132-137`). |
| `ISteamGameServerStats` | ✗ | ✓ | **Yes** | `ISteamGameServerStats.cs:21` server accessor only. Facade `SteamServerStats : SteamServerClass<>` (`SteamServerStats.cs:10`). `CallResult` correctly carries `IsServer` (`ISteamGameServerStats.cs:32`). |
| `ISteamUtils` | ✓ | ✓ | **Yes (dedicated) / No (listen)** | `ISteamUtils.cs:21,24` — both accessors present. But `SteamUtils : SteamSharedClass<>` (`SteamUtils.cs:14`) and `SteamSharedClass<T>.Interface => InterfaceClient ?? InterfaceServer` (`SteamInterface.cs:64`) prefers client. See F5. |
| `ISteamNetworking` | ✓ | ✓ | **Yes (dedicated) / No (listen)** | `ISteamNetworking.cs:21,24`. Same `SteamSharedClass` preference. |
| `ISteamNetworkingSockets` | ✓ | ✓ | **Yes (dedicated) / No (listen)** | `ISteamNetworkingSockets.cs:21,24`. `SteamNetworkingSockets : SteamSharedClass<>` (`SteamNetworkingSockets.cs:11`). Same preference — and this one matters most, since `SteamNetworkingSockets.Identity` is documented in-repo as "for the gameserver interface, the SteamID assigned to the gameserver" (`SteamNetworkingSockets.cs:17-19`). |
| `ISteamNetworkingMessages` | ✓ | ✓ | **Yes (dedicated) / No (listen)** | `ISteamNetworkingMessages.cs:21,24`. Fork-added to both `SteamClient.Init` (`SteamClient.cs:86`) and `SteamServer.Init` (`SteamServer.cs:121`). Same preference. |
| `ISteamNetworkingUtils` | ✓ (global) | ✓ (global) | **Yes** | `ISteamNetworkingUtils.cs:21` uses `GetGlobalInterfacePointer`; `SetupInterface` returns it before the client/server branch (`SteamInterface.cs:32-36`). The shared-class preference is a no-op here because both sides resolve to the same pointer. |
| `ISteamInventory` | ✓ | ✓ | **Yes (dedicated) / No (listen)** | `ISteamInventory.cs:21,24`. Registered server-side at `SteamServer.cs:115`. |
| `ISteamUGC` | ✓ | ✓ | **Yes (dedicated) / No (listen)** | `ISteamUGC.cs:21,24`. Registered server-side at `SteamServer.cs:116`. |
| `ISteamHTTP` | ✓ | ✓ | **N/A — not exposed** | `ISteamHTTP.cs:21,24` has both accessors, but there is **no `SteamHttp` managed facade class anywhere in the library**, and `SteamServer.cs:114` has it commented out: `//AddInterface<ISteamHTTP>();`. See G3. |
| **`ISteamApps`** | ✓ | **✗** | **NO — CRITICAL** | `isteamapps.h:129` is `STEAM_DEFINE_USER_INTERFACE_ACCESSOR` only; there is no `SteamAPI_SteamGameServerApps_*` in `steam_api_flat.h`. `ISteamApps.cs:21` correctly declares only `GetUserInterfacePointer`. But `SteamServer.cs:117` calls `AddInterface<SteamApps>()` and `SteamServer.cs:94` puts `ISteamApps.Version` in the server version-check string. See **F1**. |
| `ISteamMatchmakingServers` | ✓ | ✗ | **Yes** | `ISteamMatchmakingServers.cs:21` user accessor only; facade is `SteamClientClass<>` (`SteamMatchmakingServers.cs:13`) which throws on `SetInterface(true, …)` (`SteamInterface.cs:132-135`). Never registered by `SteamServer.Init`. Correct — the server browser is client-only. |
| `ISteamUser` | ✓ | ✗ | **Yes** | `ISteamUser.cs:21` user accessor only; `SteamUser : SteamClientClass<>` (`SteamUser.cs:16`). Never registered server-side. (This is *why* F2 NREs.) |
| `ISteamFriends`, `ISteamMatchmaking`, `ISteamUserStats`, `ISteamRemoteStorage`, `ISteamInput`, `ISteamScreenshots`, `ISteamMusic`, `ISteamVideo`, `ISteamParties`, `ISteamParentalSettings`, `ISteamRemotePlay`, `ISteamTimeline`, `ISteamAppList`, `ISteamController`, `ISteamGameSearch`, `ISteamHTMLSurface`, `ISteamMusicRemote` | ✓ | ✗ | **Yes** | All declare `GetUserInterfacePointer` only; none is registered by `SteamServer.Init` (`SteamServer.cs:110-121`). Verified by sweeping every file in `Generated/Interfaces/`. |

**Version strings match the headers exactly.** `ISteamGameServer.Version == "SteamGameServer015"` vs `isteamgameserver.h:269`; `ISteamGameServerStats.Version == "SteamGameServerStats001"` vs `isteamgameserverstats.h:63`. ✓

---

## What this fork changed vs upstream (and a review of each change)

Merge base with upstream `Facepunch/Facepunch.Steamworks` is `4463739`; upstream tip merged in at `a3382a1` is `90d0a6b` (v2.5.1). `git diff 90d0a6b HEAD` is the fork delta: **49 files, +640/−232**, of which the server-relevant source changes are small and enumerable.

### Studio commits (pre-merge)

| Commit | Change | Review |
|---|---|---|
| `1995358` "Added GetAuthTicket … Auth Workflow on a server from an home made API" | Adds `SteamServer.GetAuthSessionTicket(NetIdentity)` (`SteamServer.cs:485-503`); adds `NetIdentity` string operator + `GenericString` (`NetIdentity.cs:96-102, 122-132`) | **Two real defects.** (a) The returned `AuthTicket`'s `Cancel()` routes to `ISteamUser`, which is null on a dedicated server → **F2 (HIGH)**. (b) No async / "ticket ready" variant, unlike the client's `GetAuthSessionTicketAsync` (`SteamUser.cs:341`) → **F9 (MEDIUM)**. The `NetIdentity` additions are fine and were later fixed for the post-`ICustomMarshaler` world in `0d78622`. |
| `15a9f33` "Addition of SteamNetworkingMessages (WIP) / Update of SteamClient / Update of SteamServer" | `AddInterface<SteamNetworkingMessages>()` on **both** client and server (`SteamClient.cs:86`, `SteamServer.cs:121`); adds `SteamServer.LogOn(string token)` (`SteamServer.cs:306-310`) | Routing itself is correct — `ISteamNetworkingMessages.cs:24` has the game-server accessor and `SteamNetworkingMessages.InstallEvents(server)` passes the flag through. **But** because `SteamNetworkingMessages : SteamSharedClass<>` (`SteamNetworkingMessages.cs:20`), a listen server resolves to the client pointer (**F5**), and `OnSessionRequest`/`OnSessionFailed` are a single pair of `public static Action<>` **fields** (not `event`s) shared by both pipes (`SteamNetworkingMessages.cs:40`, `:42`) — a handler cannot tell whether a session request arrived on the client or the server side, and the fields are never cleared on shutdown. `LogOn(token)` is a correct binding of `isteamgameserver.h:57` but is undocumented, unvalidated (empty/null token accepted), and calls the `[Obsolete]` no-op `ForceHeartbeat()` (`SteamServer.cs:357-360`). |
| `9488b5c` "Added QueryEndReason enum for ServerList queries" | `ServerList.Base.RunQueryAsync` return type `Task<bool>` → `Task<QueryEndReason>` (`Base.cs:10-16, 63-108`); `IpList` likewise (`IpList.cs:22-64`) | **A genuine improvement** and the right call — callers can now distinguish timeout from cancellation from an invalid client. It is a **binary/source-breaking API change vs upstream**, which is acceptable in a fork but should be in release notes. Note that no test in the repo actually checks the returned value (`ServerlistTest.cs:65` prints it into a variable literally named `success`). |
| `2f85f1c`, `a3124e5`, `9ae6342`, `5028bcb`, `df075f3`, `3c705d6`, `616afa5` | NetworkingSockets / NetworkingMessages / UGC changes | Out of scope for the server path except that `ISteamNetworkingMessages._SendMessageToUser` was changed from `[In,Out] IntPtr[]` to a plain `IntPtr` (`ISteamNetworkingMessages.cs:29-34`) — **correct** (the SDK parameter is `const void*`, not an array of pointers), but the change is applied by **editing generated code with the old line left commented out**, so the next generator run will silently revert it. See F12. |
| `a3419df` | `GameServerTest.BeginAuthSession` now passes `NetIdentity.LocalHost` instead of `SteamClient.SteamId` | Correct for the current signature. |

### Post-merge studio commits

| Commit | Change | Review |
|---|---|---|
| `0d78622` | Fix `NetIdentity` string operator for post-`ICustomMarshaler` marshaling | Correct — uses `Utf8StringToNative` consistently with the rest of the tree. |
| `8bc22ae`, `c3b9f5a` | Generator fix for the greedy Hungarian-prefix strip: `m_identityRemote`→`IdentityRemote` (was `DentityRemote`), `m_info`→`Info` (was `Nfo`), `m_identity`→`Identity` (was `Dentity`), `m_routing`→`Routing` (was `Outing`) — `Generator/CodeWriter/Utility.cs:18-27` plus the regenerated `SteamCallbacks.cs`/`SteamStructs.cs` | **Correct and well-commented.** All four affected members are `internal`, so no public API break. The fix is a hard-coded allow-list rather than a fix to the greedy `.Replace()` chain, so the same class of bug will recur for any future `m_i*`/`m_n*`/`m_o*` member whose word starts with a Hungarian-looking letter. Worth converting to a regex that only strips a prefix when the following character is uppercase. |
| `d5bf68c`, `e5db9f2`, `07a53ff` | `run-live-steam-tests.ps1` + NetworkingMessages live tests | The runner's **default filter excludes every server test**: `run-live-steam-tests.ps1:40-42` matches only `AppTest`, `FriendsTest`, `NetworkingMessagesTest`. `GameServerTest`, `GameServerStatsTest`, `ServerListTest` run only under `-Full`, and `-Full` sets `--blame-hang-timeout 90s` (`:45`) which is ≤ `RustServerListTest`'s own `RunQueryAsync( 90 )` budget (`ServerlistTest.cs:85`). The flagship area is the least-exercised part of the harness. Also `run-live-steam-tests.ps1:45` assigns to `$args`, shadowing a PowerShell automatic variable. |
| `6424be1`, `ef2073a` | Build fixes (net46 + shared `obj/`), cruft purge | `Directory.Build.props` now gives each project its own `BaseIntermediateOutputPath` but **deliberately keeps `bin/` shared** (see its own comment at `Directory.Build.props:25-28`). That is what puts the Posix managed DLL next to two Windows natives with no `.so` — see F8. |

### Restored-from-binary addition

`SteamUser.AdvertiseGame( SteamId, uint ip, ushort port )` (`SteamUser.cs:479-482`) carries the comment *"Studio addition — previously shipped only in the compiled binaries; source restored here so the DLL is reproducible from this repo."* The binding is correct (`ISteamUser::AdvertiseGame`), but the parameter order and host-vs-network byte order of `ip` are not verifiable statically. It is client-side, so it only matters for the listen-server / "join my server" flow. Flagged as an open question.

---

## Findings

### F1 — CRITICAL — `SteamApps` is registered on the game server but has no game-server accessor; calls dereference a NULL interface

**Location:** `Facepunch.Steamworks/SteamServer.cs:117` (and `:94`), `Facepunch.Steamworks/SteamServer.cs:145-150`, `Facepunch.Steamworks/SteamApps.cs:18-21`, `Facepunch.Steamworks/Utility/SteamInterface.cs:26-48`

**What's wrong:** `SteamServer.Init` registers `SteamApps` as a server interface. `ISteamApps` has no game-server accessor in the SDK, so `SetupInterface(true)` leaves `Self == IntPtr.Zero`. `SteamApps.InitializeInterface` publishes the object into `InterfaceServer` **before** it checks for the null pointer, and `SteamServer.AddInterface<T>` discards the `false` return.

**Evidence:**

`Generator/steam_sdk/isteamapps.h:129` — user accessor only, and there is no `SteamAPI_SteamGameServerApps_*` symbol anywhere in `steam_api_flat.h`:
```c
STEAM_DEFINE_USER_INTERFACE_ACCESSOR( ISteamApps *, SteamApps, STEAMAPPS_INTERFACE_VERSION );
```

`Facepunch.Steamworks/Utility/SteamInterface.cs:38-47` — when `gameServer` is true and there is no override, `Self` stays zero:
```csharp
			if ( gameServer )
			{
				SelfServer = GetServerInterfacePointer();
				Self = SelfServer;
			}
```
`SteamInterface.GetServerInterfacePointer()` base implementation (`SteamInterface.cs:15`) is `=> IntPtr.Zero`, and `ISteamApps.cs` overrides only `GetUserInterfacePointer` (`ISteamApps.cs:21`).

`Facepunch.Steamworks/SteamApps.cs:18-21` — publish-then-check:
```csharp
		internal override bool InitializeInterface( bool server )
		{
			SetInterface( server, new ISteamApps( server ) );
			if ( Interface.Self == IntPtr.Zero ) return false;
```

`Facepunch.Steamworks/SteamServer.cs:145-150` — return value ignored:
```csharp
		internal static void AddInterface<T>() where T : SteamClass, new()
		{
			var t = new T();
			t.InitializeInterface( true );
			openInterfaces.Add( t );
		}
```
Compare `Facepunch.Steamworks/SteamClient.cs:102-113`, which does the right thing:
```csharp
			bool valid = t.InitializeInterface( false );
			if ( valid ) { openInterfaces.Add( t ); }
			else { t.DestroyInterface( false ); }
```

**Failure scenario:** Pure dedicated server (`SteamServer.Init` only, no `SteamClient.Init`). `SteamApps.InterfaceClient` is null, so `SteamApps.Interface` resolves to the zeroed `InterfaceServer`. Any `SteamApps` property — e.g. `SteamApps.GameLanguage`, `SteamApps.IsDlcInstalled`, `SteamApps.BuildId` — invokes `SteamAPI_ISteamApps_*( IntPtr.Zero, … )`, which does `((ISteamApps*)self)->Method()` in native code. Null vtable dereference → SIGSEGV / access violation. This is a hard process kill, not a catchable .NET exception. In a listen server it is masked because `InterfaceClient` wins.

**Recommended fix:**
1. Make `SteamServer.AddInterface<T>` mirror `SteamClient.AddInterface<T>` — check the return, call `DestroyInterface(true)` on failure so the bogus object is never published.
2. Remove `AddInterface<SteamApps>()` from `SteamServer.Init:117` and drop `ISteamApps.Version` from the server version-check string at `SteamServer.cs:94` (Valve's canonical server list in `steam_gameserver.h:94-106` does not include `STEAMAPPS_INTERFACE_VERSION`).
3. Consider moving the `Self == IntPtr.Zero` check *before* `SetInterface` in every `InitializeInterface` override so a failed interface is never observable.

---

### F2 — HIGH — Server-issued auth tickets can never be cancelled; `AuthTicket.Dispose()` NREs on a dedicated server

**Location:** `Facepunch.Steamworks/Classes/AuthTicket.cs:14-28`, `Facepunch.Steamworks/SteamServer.cs:485-503`

**What's wrong:** The fork added `SteamServer.GetAuthSessionTicket` (studio commit `1995358`) which returns the shared `AuthTicket` type, but `AuthTicket.Cancel()` hard-codes the **client** interface.

**Evidence:**
```csharp
// Classes/AuthTicket.cs:14-23
		public void Cancel()
		{
			if ( Handle != 0 )
			{
				SteamUser.Internal.CancelAuthTicket( Handle );
			}
			Handle = 0;
			Data = null;
		}
```
`SteamUser.Internal` is `Interface as ISteamUser` where `Interface` is the `SteamClientClass<SteamUser>` static (`SteamUser.cs:16-18`). `SteamServer.Init` never registers `SteamUser` (`SteamServer.cs:110-121`), so on a dedicated server it is `null`.

The correct call exists and is bound — `Generated/Interfaces/ISteamGameServer.cs:316-323`:
```csharp
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamGameServer_CancelAuthTicket", CallingConvention = Platform.CC)]
		private static extern void _CancelAuthTicket( IntPtr self, HAuthTicket hAuthTicket );
		internal void CancelAuthTicket( HAuthTicket hAuthTicket ) { _CancelAuthTicket( Self, hAuthTicket ); }
```
— matching `isteamgameserver.h:167`:
```c
	// Cancel auth ticket from GetAuthSessionTicket, called when no longer playing game with the entity you gave the ticket to
	virtual void CancelAuthTicket( HAuthTicket hAuthTicket ) = 0;
```
There is **no** `SteamServer.CancelAuthTicket` wrapper.

**Failure scenario:** Dedicated server obtains a ticket to authenticate itself against a studio backend (the stated purpose in the commit message). On backend re-auth, shutdown, or any `using (var t = SteamServer.GetAuthSessionTicket(id))` block, `Dispose()` → `Cancel()` → `NullReferenceException` on a background/async path. In a listen server it is worse than a crash: it silently cancels on the *client's* `ISteamUser`, so the server-side `HAuthTicket` leaks in Steam and an unrelated client ticket may be invalidated (handles are per-interface).

**Recommended fix:** Give `AuthTicket` an owner. Either add an `internal bool IsServerTicket` set by the factory and branch in `Cancel()`, or subclass. Add `public static void CancelAuthTicket( uint handle )` to `SteamServer`. Guard both paths with a null check on `Internal`.

---

### F3 — HIGH — `SteamServer.Shutdown()` leaves the property cache dirty; re-`Init` silently skips required server properties

**Location:** `Facepunch.Steamworks/SteamServer.cs:126-133`, `:164-170`, `:187-290`, `:372-404`

**What's wrong:** Every server property is a "set only if changed" static cache. `Shutdown()` tears down the interfaces but never resets those statics or the `KeyValue` dictionary, so a second `Init` in the same process compares against the *previous* run's values and skips the native calls.

**Evidence:**
```csharp
// SteamServer.cs:126-133 — Init's "initial settings"
			AdvertiseServer = true;
			MaxPlayers = 32;
			BotCount = 0;
			Product = $"{appid.Value}";
			ModDir = init.ModDir;
			GameDescription = init.GameDescription;
			Passworded = false;
			DedicatedServer = init.DedicatedServer;
```
```csharp
// SteamServer.cs:198-203 — representative setter
		public static int MaxPlayers
		{
			get => _maxplayers;
			set { if ( _maxplayers == value ) return; Internal.SetMaxPlayerCount( value ); _maxplayers = value; }
		}
		private static int _maxplayers = 0;
```
```csharp
// SteamServer.cs:164-170
		public static void Shutdown()
		{
			Dispatch.ShutdownServer();
			ShutdownInterfaces();
			SteamGameServer.Shutdown();
		}
```
The SDK is explicit that these are not optional — `isteamgameserver.h:23-25`:
```c
// Basic server data.  These properties, if set, must be set before before calling LogOn.  They
// may not be changed after logged in.
```

Two distinct symptoms:
- **On re-init:** `_maxplayers == 32`, `_product == "{appid}"`, `_modDir == init.ModDir`, `_gameDescription == init.GameDescription` all still hold from the previous run → `SetMaxPlayerCount`, `SetProduct`, `SetModDir`, `SetGameDescription` are **never called** on the new interface. The server logs on with SDK defaults and will not list correctly. `SetKey` is affected identically because `KeyValue` (`SteamServer.cs:372`) is never cleared — `ClearKeys()` exists but `Shutdown()` doesn't call it.
- **On first init:** `BotCount = 0` and `Passworded = false` are no-ops because the C# static defaults already match. Benign today (they match the SDK defaults) but it is accidental, not designed.

**Failure scenario:** A server host that restarts the Steam server in-process between matches/maps (or any test run that calls `Init` twice) ends up advertised with no mod dir and no game description — invisible or mis-categorised in the server browser, with no error anywhere.

**Recommended fix:** Add a `ResetPropertyCache()` invoked from `Shutdown()` (and defensively at the top of `Init`) that restores every backing field to a sentinel that can never equal a real value (e.g. `_maxplayers = -1`, strings to `null`), and calls `KeyValue.Clear()`. Alternatively drop the caches and always call through — these are cheap setters.

---

### F4 — HIGH — Auth sessions are not tracked and `EndAuthSession` is never guaranteed; `BeginAuthSession` discards the failure reason

**Location:** `Facepunch.Steamworks/SteamServer.cs:409-420`, `:425-428`, `:32`, `:164-170`

**What's wrong:** There is no registry of open auth sessions, nothing ends them on disconnect or shutdown, and the result enum is collapsed to `bool`.

**Evidence:**
```csharp
// SteamServer.cs:409-420
		public static unsafe bool BeginAuthSession( byte[] data, SteamId steamid )
		{
			fixed ( byte* p = data )
			{
				var result = Internal.BeginAuthSession( (IntPtr)p, data.Length, steamid );
				if ( result == BeginAuthResult.OK )
					return true;
				return false;
			}
		}
```
The information is available and discarded — `ISteamGameServer.cs:299` returns `BeginAuthResult`. Contrast the client facade, which surfaces it (`SteamUser.cs:450-456` returns `BeginAuthResult`).

`isteamgameserver.h:163-164` on the pairing requirement:
```c
	// Stop tracking started by BeginAuthSession - called when no longer playing game with this entity
	virtual void EndAuthSession( CSteamID steamID ) = 0;
```

`SteamServer.InstallEvents` (`SteamServer.cs:30-37`) wires `ValidateAuthTicketResponse_t` straight to a public event and does nothing else — it does not auto-`EndAuthSession` when `AuthResponse` is a denial, and `Shutdown()` (`SteamServer.cs:164-170`) ends nothing.

**Failure scenario:** Any code path that returns early between `BeginAuthSession` and the game-side "player fully connected" state — a socket error, an exception in the connect handler, a server crash-restart — leaves the session open on Steam's side. Steam will keep the user marked as playing on that server, and re-connecting the same user can return `BeginAuthResult.DuplicateRequest`, which the current `bool` API reports simply as "false / not authorised". This is a classic "player cannot rejoin after a network blip" bug and it is invisible from the API.

**Recommended fix:**
1. Change `SteamServer.BeginAuthSession` to return `BeginAuthResult` (keep a `bool`-returning overload for source compatibility if needed).
2. Track begun sessions in a `HashSet<SteamId>`; `EndSession` removes; `Shutdown()` drains the set calling `Internal.EndAuthSession` for each before `SteamGameServer.Shutdown()`.
3. In the `ValidateAuthTicketResponse_t` handler, auto-end the session for terminal denial responses so the SDK-side state cannot drift from the game-side state.

---

### F5 — HIGH — Shared interfaces resolve client-first, so a listen server calls the wrong native interface

**Location:** `Facepunch.Steamworks/Utility/SteamInterface.cs:62-66`

**What's wrong:**
```csharp
	public class SteamSharedClass<T> : SteamClass
	{
		internal static SteamInterface Interface => InterfaceClient ?? InterfaceServer;
		internal static SteamInterface InterfaceClient;
		internal static SteamInterface InterfaceServer;
```
Both pointers are stored, but every `Internal` accessor that resolves through `Interface` gets the client one whenever a client is initialized in the same process. Affected facades: `SteamUtils` (`SteamUtils.cs:14`), `SteamNetworking`, `SteamInventory`, `SteamUGC`, `SteamApps`, `SteamNetworkingSockets` (`SteamNetworkingSockets.cs:11`), `SteamNetworkingMessages` (`SteamNetworkingMessages.cs:20`). `SteamNetworkingUtils` is unaffected because its accessor is global.

The SDK deliberately hands out distinct objects — `steam_api_flat.h:192, 526, 764, 914, 1028, 1046`:
```c
S_API ISteamUtils *SteamAPI_SteamGameServerUtils_v010();
S_API ISteamNetworking *SteamAPI_SteamGameServerNetworking_v006();
S_API ISteamUGC *SteamAPI_SteamGameServerUGC_v020();
S_API ISteamInventory *SteamAPI_SteamGameServerInventory_v003();
S_API ISteamNetworkingMessages *SteamAPI_SteamGameServerNetworkingMessages_SteamAPI_v002();
S_API ISteamNetworkingSockets *SteamAPI_SteamGameServerNetworkingSockets_SteamAPI_v012();
```

`CallResult<T>` gets this right (`CallResult.cs:26-29` picks `SteamUtils.InterfaceServer` when `server` is true), which shows the split is understood elsewhere in the codebase — the facade layer just doesn't use it.

**Failure scenario:** Listen server (`SteamClient.Init` then `SteamServer.Init`, exactly what `AppTest.AssemblyInit` does). `SteamNetworkingSockets.CreateNormalSocket`/`Identity`/`RequestFakeIP` all operate on the client's networking interface, so listen sockets are created under the user's identity instead of the game server's, and `SteamNetworkingMessages` sessions are attributed to the user. No error is raised; behaviour is just wrong.

**Recommended fix:** Give the shared facades an explicit server-context accessor, e.g. `internal static ISteamNetworkingSockets ServerInternal => InterfaceServer as ISteamNetworkingSockets;`, and route server-side call sites through it. At minimum, document loudly that shared facades are client-biased and that a listen server must not use them for server operations. Longer term this wants the same treatment as `CallResult` — an explicit `server` flag threaded through the public API.

---

### F6 — HIGH — Server-browser query handle can be released underneath the running async pump (use-after-free)

**Location:** `Facepunch.Steamworks/ServerList/Base.cs:63-108`, `:129-130`, `:141-154`, `:174-205`

**What's wrong:** `RunQueryAsync` is a fire-and-forget polling loop whose `await Task.Delay(33)` continuations run on the thread pool. `Dispose()`/`Reset()` can call `Internal.ReleaseRequest(request)` from another thread at any point in the loop body. The `request.Value == IntPtr.Zero` guard at `:81` is checked once per iteration and is not atomic with the four native calls that follow.

**Evidence:**
```csharp
// ServerList/Base.cs:74-102
			while ( IsRefreshing )
			{
				await Task.Delay( 33 );

				if ( request.Value == IntPtr.Zero || thisRequest.Value != request.Value )
					return QueryEndReason.CancelledOrChangedRequest;
				...
				UpdatePending();
				UpdateResponsive();
```
```csharp
// ServerList/Base.cs:141-154
		void ReleaseQuery()
		{
			if ( request.Value != IntPtr.Zero )
			{
				Cancel();
				Internal.ReleaseRequest( request );
				request = IntPtr.Zero;
			}
		}
		public virtual void Dispose() { ReleaseQuery(); }
```
The native calls that can run against a freed handle: `Internal.GetServerCount( request )` (`:129`), `Internal.IsRefreshing( request )` (`:130`), `Internal.HasServerResponded( request, x )` (`:179`), `Internal.GetServerDetails( request, x )` (`:183`, `:199`).

The SDK warns about exactly this class of teardown race — `isteammatchmaking.h`, `CancelQuery` doc comment:
```c
	// Cancel an request which is operation on the given list type.  You should call this to cancel
	// any in-progress requests before destructing a callback object that may have been passed 
	// to one of the above list request calls.  Not doing so may result in a crash when a callback
	// occurs on the destructed object.
```

**Failure scenario:** `using ( var list = new ServerList.Internet() ) { _ = list.RunQueryAsync(); }` — or any UI that disposes a browser page while a refresh is in flight. The `using` scope exits, `ReleaseRequest` frees the native request, and the still-scheduled continuation calls `GetServerCount` on it. Steam's `HServerListRequest` is a pointer to a heap object; this is a use-after-free that usually manifests as a crash inside `steamclient.dll` with no managed stack.

**Recommended fix:** Add a `CancellationTokenSource`/`disposed` flag set under a lock in `Dispose()`, check it immediately before each native call, and make `RunQueryAsync` respect it. Better: hold the handle in a `SafeHandle`-like wrapper with a reference count so release is deferred until the pump exits, and have `RunQueryAsync` accept a `CancellationToken`.

**Note (verified-correct, don't "fix" this):** every `LaunchQuery` passes `IntPtr.Zero` for the `ISteamMatchmakingServerListResponse*` parameter (`Internet.cs:10`, `Favourites.cs:10`, `Friends.cs:10`, `History.cs:10`, `LocalNetwork.cs:9`). There is no managed callback object handed to native code, therefore **no GCHandle rooting problem exists** — the design polls instead. That is the right call for a P/Invoke binding. It is however undocumented behaviour: the header does not state that a NULL response object is legal.

---

### F7 — MEDIUM — Timed-out server-browser queries are never cancelled and have no finalizer

**Location:** `Facepunch.Steamworks/ServerList/Base.cs:97-108`, `:151-154`

**What's wrong:**
```csharp
				if ( stopwatch.Elapsed.TotalSeconds > timeoutSeconds )
				{
					ret = QueryEndReason.TimeOut;
					break;
				}
			}
			MovePendingToUnresponsive();
			InvokeChanges();
			return ret;
```
On timeout the method returns but never calls `Cancel()`. The native query keeps pinging every server in the list until someone calls `Dispose()`. `Base` has no finalizer and holds no `SafeHandle`, so a dropped instance leaks the `HServerListRequest` for the lifetime of the process.

**Failure scenario:** A client that starts a fresh `ServerList.Internet` per browser refresh and relies on GC (a very common usage of an `IDisposable` that looks like a plain object) accumulates live master-server queries. Bandwidth and Steam-side resource growth with no diagnostic.

**Recommended fix:** Call `Cancel()` on the timeout path before returning. Add `~Base()` that releases the request, or wrap the handle in a `CriticalFinalizerObject`/`SafeHandle`.

---

### F8 — MEDIUM — The Posix build produces no deployable Linux artifact

**Location:** `Facepunch.Steamworks/Facepunch.Steamworks.Posix.csproj` (whole file), `Facepunch.Steamworks/Directory.Build.props:25-28`, `.github/workflows/dotnetcore.yml`

**What's wrong:** The Posix project has no `ItemGroup` at all — no `<None Include="linux64/libsteam_api.so">`, no `PackageId`, no `GeneratePackageOnBuild`, no `RuntimeIdentifiers`. Compare `Facepunch.Steamworks.Win64.csproj:36-40` which does copy `steam_api64.dll`.

**Evidence:** the entire Posix project:
```xml
<Project Sdk="Microsoft.NET.Sdk">
	<PropertyGroup>
		<AssemblyName>Facepunch.Steamworks.Posix</AssemblyName>
		<DefineConstants>$(DefineConstants);PLATFORM_POSIX</DefineConstants>
		<TargetFrameworks>netstandard2.1;net6.0;net46</TargetFrameworks>
		...
	</PropertyGroup>
	<Import Project="Facepunch.Steamworks.targets" />
</Project>
```
Actual build output of this repo, `Facepunch.Steamworks/bin/Debug/net6.0/`:
```
Facepunch.Steamworks.Posix.dll   Facepunch.Steamworks.Win32.dll   Facepunch.Steamworks.Win64.dll
steam_api.dll                    steam_api64.dll
```
No `libsteam_api.so`, and the Posix managed DLL sits next to two Windows natives because `Directory.Build.props` deliberately shares `bin/`. The genuine ELF binaries **are** committed (`Facepunch.Steamworks/linux32/libsteam_api.so`, `linux64/libsteam_api.so`) — they're just never copied.

Related: there is no `NativeLibrary.SetDllImportResolver` anywhere in the tree, so there is no runtime hook to select linux32 vs linux64 (both files have the same name) and no better diagnostic than `DllNotFoundException`. And `.github/workflows/dotnetcore.yml` builds the Posix project only on `windows-latest`, with no Linux job and no test project referencing it.

**Verified correct and not to be re-litigated:** `Utility/Platform.cs:11-26` maps pack sizes exactly as `steamclientpublic.h:1161-1178` requires (POSIX→4, Win32→8, Win64→8 — Valve uses `VALVE_CALLBACK_PACK_SMALL` on Linux/macOS/FreeBSD *regardless of bitness*), `CallingConvention.Cdecl` is correct on Linux, and there are zero Windows-only API usages in the library source (no `Microsoft.Win32`, `Registry`, `kernel32`, `user32`, `Environment.OSVersion`, or hardcoded `\` paths). `Utility/SourceServerQuery.cs` is fully portable.

**Failure scenario:** "Copy `bin/Release/net6.0` to the Linux box and run" produces `DllNotFoundException: libsteam_api` on the first P/Invoke. Consuming via NuGet is impossible — only `Facepunch.Steamworks.2.5.2.nupkg` (Win64) and `Facepunch.Steamworks.win32.2.5.2.nupkg` are produced.

**Recommended fix:** Add the `.so` copy items to `Posix.csproj` (or better, a `runtimes/linux-x64/native/` + `runtimes/linux-x86/native/` RID layout so the CLR picks the right one), give it `PackageId`/`GeneratePackageOnBuild`, and give each platform project its own `BaseOutputPath`. Add a Linux CI job that at minimum builds and loads the assembly.

---

### F9 — MEDIUM — `SteamServer.GetAuthSessionTicket` has no "ticket is ready" wait

**Location:** `Facepunch.Steamworks/SteamServer.cs:485-503`

**What's wrong:** The client facade has both a synchronous `GetAuthSessionTicket` (`SteamUser.cs:315-332`) and a `GetAuthSessionTicketAsync` that waits for `GetAuthSessionTicketResponse_t` before handing the ticket back (`SteamUser.cs:341-382`) — precisely because a raw ticket is not guaranteed usable until Steam confirms it. The fork's server-side copy has only the synchronous form.

**Evidence:** `SteamUser.cs:341` (`GetAuthSessionTicketAsync`) vs `SteamServer.cs:485-503` (no async counterpart, no `OnGetAuthSessionTicketResponse` equivalent on `SteamServer`).

Also worth recording so it is not "fixed" incorrectly: the server copy passes `Helpers.TakeBuffer( 1024 )` while upstream bumped the client to `2560` in `dcfaa86` ("Fixed truncated session ticket array"). **This is not actually a truncation bug in either case** — `Helpers.TakeBuffer` (`Utility/Helpers.cs:59-84`) floors every pool slot at `new byte[1024 * 256]` and only ever grows it, so the `cbMaxTicket` argument (`data.Length`) is 262144 on both paths regardless of the `minSize` argument. Upstream's fix was cosmetic. The server copy should still be aligned to `2560` for consistency, but do not report it as a live truncation defect.

**Failure scenario:** The server sends a not-yet-validated ticket to the studio's backend; the backend's `AuthenticateUserTicket` call fails intermittently and unreproducibly.

**Recommended fix:** Confirm on a live server whether `GetAuthSessionTicketResponse_t` (a `k_iSteamUserCallbacks`-range callback, `isteamuser.h:354-356`) is delivered on the game-server pipe. If yes, add `SteamServer.OnGetAuthSessionTicketResponse` + `GetAuthSessionTicketAsync` mirroring the client. If not, document the limitation prominently. See Open Questions.

---

### F10 — MEDIUM — Server-side stats API cannot distinguish "not loaded" from "zero", and `GSStatsUnloaded_t` is not surfaced

**Location:** `Facepunch.Steamworks/SteamServerStats.cs:30-35`, `:69-95`, `:122-130`; `Facepunch.Steamworks/SteamServer.cs:30-37`

**What's wrong:** The request-then-read pattern is implemented, but the "read" half silently swallows every failure mode, and the SDK's explicit "your cached stats are gone, request them again" callback is generated but never installed.

**Evidence:**
```csharp
// SteamServerStats.cs:122-130
		public static bool GetAchievement( SteamId steamid, string name )
		{
			bool achieved = false;
			if ( !Internal.GetUserAchievement( steamid, name, ref achieved ) )
				return false;
			return achieved;
		}
```
`false` means all three of: not requested yet, API call failed, achievement locked. `GetInt`/`GetFloat` (`:69-95`) collapse the same way onto `defaultValue`.

`isteamgameserverstats.h:101-109` defines the callback that exists precisely for this:
```c
//-----------------------------------------------------------------------------
// Purpose: Callback indicating that a user's stats have been unloaded.
//  Call RequestUserStats again to access stats for this user
//-----------------------------------------------------------------------------
struct GSStatsUnloaded_t
{
	enum { k_iCallback = k_iSteamUserStatsCallbacks + 8 };
	CSteamID	m_steamIDUser;	// User whose stats have been unloaded
};
```
It is generated as `GSStatsUnloaded_t` (`Generated/SteamCallbacks.cs:3097`) but `SteamServer.InstallEvents` (`SteamServer.cs:30-37`) installs only five callbacks and `SteamServerStats.InitializeInterface` (`SteamServerStats.cs:14-20`) installs none.

`SteamServerStats` also omits `UpdateUserAvgRateStat`, which *is* bound in the generated layer (`ISteamGameServerStats.cs:106`) per `isteamgameserverstats.h:49`.

**Failure scenario:** Steam unloads a user's stats (documented behaviour once the user is no longer on the server, or on a Steam reconnect). Every subsequent `GetInt`/`GetAchievement` returns the default. The server writes those defaults back via `SetInt`/`StoreUserStats` and **overwrites the player's real stats with zeros**. Nothing in the API tells the caller this happened.

**Recommended fix:** Install `GSStatsUnloaded_t` on the server pipe and expose `SteamServerStats.OnStatsUnloaded`. Add `TryGetInt`/`TryGetFloat`/`TryGetAchievement` returning `bool` + `out`, or make the getters nullable. Expose `UpdateUserAvgRateStat`. Document that `SetUserStat`/`SetUserAchievement` only work for servers on the official-server IP range (`isteamgameserverstats.h:39-42`).

---

### F11 — MEDIUM — Server-side callbacks the SDK defines are generated but never installed

**Location:** `Facepunch.Steamworks/SteamServer.cs:30-37`

**What's wrong:** `InstallEvents` wires five callbacks:
```csharp
            Dispatch.Install<ValidateAuthTicketResponse_t>( ..., true );
			Dispatch.Install<SteamServersConnected_t>( ..., true );
			Dispatch.Install<SteamServerConnectFailure_t>( ..., true );
			Dispatch.Install<SteamServersDisconnected_t>( ..., true );
			Dispatch.Install<SteamNetAuthenticationStatus_t>( ..., true );
```
The SDK defines nine more game-server callbacks in `isteamgameserver.h:285-389`, all of which are generated as structs (`Generated/SteamCallbacks.cs:2921-3033`) and none of which is reachable: `GSClientApprove_t`, `GSClientDeny_t`, **`GSClientKick_t`**, `GSClientAchievementStatus_t`, `GSPolicyResponse_t`, `GSClientGroupStatus_t`, `GSGameplayStats_t`, `GSReputation_t`, `AssociateWithClanResult_t`, `ComputeNewPlayerCompatibilityResult_t`.

**Failure scenario:** `GSClientKick_t` (`isteamgameserver.h:304-310`) is Steam telling the server *"kick this user"* — the mechanism by which Steam propagates a VAC ban or account action to a live session. A server built on this binding **cannot receive that instruction**, so a player banned mid-session stays on the server until they disconnect voluntarily. For a fork whose flagship claim is dedicated-server support, this is the single most impactful missing feature. `GSPolicyResponse_t` (`isteamgameserver.h:327-331`) is likewise how the server learns whether it may display as VAC-secure.

**Recommended fix:** Install at least `GSClientKick_t`, `GSClientDeny_t`, `GSClientApprove_t` and `GSPolicyResponse_t` on the server pipe and expose them as `SteamServer.OnClientKick` / `OnClientDeny` / `OnClientApprove` / `OnPolicyResponse`.

---

### F12 — MEDIUM — Generated code was hand-edited; the next generator run will silently revert it

**Location:** `Facepunch.Steamworks/Generated/Interfaces/ISteamNetworkingMessages.cs:28-34`

**What's wrong:** The fork fixed a genuinely wrong signature by editing the **generated** file and commenting out the old line, rather than fixing the generator:
```csharp
		//private static extern Result _SendMessageToUser( IntPtr self, ref NetIdentity identityRemote, [In,Out] IntPtr[]  pubData, uint cubData, int nSendFlags, int nRemoteChannel );
		private static extern Result _SendMessageToUser( IntPtr self, ref NetIdentity identityRemote, IntPtr pubData, uint cubData, int nSendFlags, int nRemoteChannel );
```
The fix is correct — `isteamnetworkingmessages.h` declares `const void *pubData`, not an array of pointers. But `Generator/` is checked in and part of the solution; regenerating restores the broken `IntPtr[]` marshalling. Note the contrast with the *right* way, which this same fork also demonstrates: the greedy-strip fix in `Generator/CodeWriter/Utility.cs:18-27` was done in the generator and the outputs regenerated.

**Failure scenario:** Someone regenerates after an SDK bump; `SendMessageToUser` starts marshalling a managed `IntPtr[]` where native expects a raw buffer pointer. Silent data corruption or crash on every P2P message send, on the server as well as the client.

**Recommended fix:** Move the `pubData` type override into the generator's parameter-type mapping and regenerate. Delete the commented-out lines.

---

### F13 — MEDIUM — `Dispatch.runningFrame` is one non-volatile static shared by both pipes

**Location:** `Facepunch.Steamworks/Classes/Dispatch.cs:79`, `:84-118`, `:236-257`

**What's wrong:**
```csharp
		static bool runningFrame = false;

		internal static void Frame( HSteamPipe pipe )
		{
			if ( runningFrame )
				return;
			try { runningFrame = true; ... }
			finally { runningFrame = false; }
		}
```
`LoopClientAsync` (16 ms, `:236-243`) and `LoopServerAsync` (32 ms, `:250-257`) are `async void` methods; after the first `await Task.Delay`, their continuations run on thread-pool threads with no synchronization context. In a listen server both loops are live simultaneously on different threads.

**Failure scenario:** Two effects, both silent. (a) Whenever the two loops overlap, one pipe's `Frame` returns immediately and **that pipe's callbacks are simply not pumped for that tick** — server callbacks (auth responses, connection status) get dropped under load. (b) `runningFrame` is a non-volatile `bool` read and written from multiple threads with no barrier — the guard itself is not reliable, so genuine concurrent `SteamAPI_ManualDispatch_RunFrame` on two pipes is possible.

`SteamServer.RunCallbacks()` (`SteamServer.cs:175-181`) shares the same `Frame`, so the manual-pump path has the identical problem.

**Recommended fix:** Make the guard per-pipe (`HashSet<HSteamPipe>` under a lock, or `Interlocked.CompareExchange` on a per-pipe flag). The guard's stated purpose — "don't call Frame inside a callback" — is per-pipe reentrancy, not global mutual exclusion.

---

### F14 — MEDIUM — `ServerInfo` equality is hash-based; the IP-list dedupe can silently drop servers

**Location:** `Facepunch.Steamworks/Structs/Server.cs:135-143`, `Facepunch.Steamworks/ServerList/IpList.cs:52-55`

**What's wrong:**
```csharp
		public bool Equals( ServerInfo other )
		{
			return this.GetHashCode() == other.GetHashCode();
		}
		public override int GetHashCode()
		{
			return Address.GetHashCode() + SteamId.GetHashCode() + ConnectionPort.GetHashCode() + QueryPort.GetHashCode();
		}
```
Equality *is* hash equality, and the hash is an unweighted sum, so `(cport=27015, qport=27016)` and `(cport=27016, qport=27015)` on the same address+steamid hash identically. `IpList` relies on it:
```csharp
					Responsive.AddRange( list.Responsive );
					Responsive = Responsive.Distinct().ToList();
```
`Address.GetHashCode()` also NREs if `Address` is null.

**Failure scenario:** Querying a machine that hosts several server instances via `ServerList.IpList` can silently drop instances from the results. Low incidence, zero diagnosability.

**Recommended fix:** Implement `Equals` by comparing the identifying fields directly (`AddressRaw`, `ConnectionPort`, `QueryPort`, `SteamId`) and use `HashCode.Combine` (or an XOR/multiply mix) for the hash. Add `operator ==`/`!=` and `Equals(object)`.

---

### F15 — LOW — `SteamServer.Init` version-check string deviates from Valve's canonical server list

**Location:** `Facepunch.Steamworks/SteamServer.cs:87-97`

**What's wrong:** The list passed to `SteamInternal_GameServer_Init_V2` includes `ISteamApps.Version`, which is not in the SDK's own list.

**Evidence:** `steam_gameserver.h:94-106`:
```c
	const char *pszInternalCheckInterfaceVersions = 
		STEAMUTILS_INTERFACE_VERSION "\0"
		STEAMNETWORKINGUTILS_INTERFACE_VERSION "\0"
		STEAMGAMESERVER_INTERFACE_VERSION "\0"
		STEAMGAMESERVERSTATS_INTERFACE_VERSION "\0"
		STEAMHTTP_INTERFACE_VERSION "\0"
		STEAMINVENTORY_INTERFACE_VERSION "\0"
		STEAMNETWORKING_INTERFACE_VERSION "\0"
		STEAMNETWORKINGMESSAGES_INTERFACE_VERSION "\0"
		STEAMNETWORKINGSOCKETS_INTERFACE_VERSION "\0"
		STEAMUGC_INTERFACE_VERSION "\0"
		"\0";
```
The repo sends `ISteamGameServer, ISteamUtils, ISteamNetworking, ISteamGameServerStats, ISteamInventory, ISteamUGC, ISteamApps, ISteamNetworkingUtils, ISteamNetworkingSockets, ISteamNetworkingMessages` — an extra `STEAMAPPS_INTERFACE_VERSION008`, and no `STEAMHTTP_INTERFACE_VERSION`. It appears to be tolerated in practice, but it is an undocumented deviation and it is the same root cause as F1.

**Fix:** Drop `ISteamApps.Version`. Add `ISteamHTTP.Version` if/when a `SteamHttp` facade is added.

**Verified correct (do not "fix"):** the double-NUL list encoding is right. `Helpers.BuildVersionString` (`Utility/Helpers.cs:103-113`) appends `'\0'` after each entry plus a final `'\0'`, and `Utf8StringToNative` (`Utility/Utf8String.cs:11-30`) copies the full byte count including embedded NULs and adds its own terminator — so the native side sees a correctly double-terminated list. Also verified: `SteamServerInit` has **no `SteamPort` field, and correctly so** — `SteamInternal_GameServer_Init_V2` (`steam_gameserver.h:91`) does not take one; it was removed from the SDK. Do not re-add it.

---

### F16 — LOW — `SteamServer.Init` failure path leaves the process in an undefined state

**Location:** `Facepunch.Steamworks/SteamServer.cs:98-102`

```csharp
			var result = SteamInternal.GameServer_Init( ipaddress, init.GamePort, init.QueryPort, secure, init.VersionString, interfaceVersions, out var error );
			if ( result != SteamAPIInitResult.OK )
			{
				throw new System.Exception( $"InitGameServer({ipaddress},{init.GamePort},{init.QueryPort},{secure},\"{init.VersionString}\") returned false - error: {error}" );
			}
```
The error surfacing itself is actually **good** — the `SteamErrMsg` from `SteamInternal_GameServer_Init_V2` is captured and included, which is the main thing the V2 API added. Three smaller problems: (a) it throws bare `System.Exception`, so a caller cannot catch it selectively; (b) the message says "returned false" when the API returns an enum; (c) `SteamAppId`/`SteamGameId` environment variables are set at `:80-81` *before* the init attempt and are never unset on failure, so a subsequent `SteamClient.Init` for a different appid inherits stale values.

**Fix:** Introduce a `SteamServerInitException` carrying `SteamAPIInitResult` and the message. Reset the env vars on the failure path.

---

### F17 — LOW — Miscellaneous server-browser correctness nits

**Location:** `Facepunch.Steamworks/ServerList/Base.cs:110`, `:195-205`; `Facepunch.Steamworks/ServerList/IpList.cs:52-55`, `:67-70`

- `public virtual void Cancel() => Internal.CancelQuery( request );` (`Base.cs:110`) is unguarded. Called before `LaunchQuery` it passes a zero handle; called when `SteamClient` is not initialized, `SteamMatchmakingServers.Internal` is `null` → NRE.
- `MovePendingToUnresponsive()` (`Base.cs:195-205`) adds to **`Unqueried`**, not `Unresponsive`, despite the name. `Unresponsive` is in practice never populated at all: `OnServer` is only ever reached with `responded == true` (`Base.cs:186`), so the `Unresponsive.Add` branch at `:216` is dead.
- Consequently `IpList` merges `list.Responsive` and `list.Unresponsive` (`IpList.cs:52-55`) but never `list.Unqueried`, so timed-out servers from an IP-list query are lost entirely.
- `IpList.Cancel()` (`:67-70`) only sets `wantsCancel`; it does not cancel the inner `ServerList.Internet` query, so cancelling waits out the current block's full timeout (up to `timeoutSeconds`).

---

## Verified-correct areas

These were checked against the headers and found correct. Do not "fix" them.

- **Generated `ISteamGameServer` binding is complete and exact.** All 40 methods of `isteamgameserver.h`'s `SteamGameServer015` are present with matching signatures, including `GetAuthSessionTicket(void*, int, uint32*, const SteamNetworkingIdentity*)` (`ISteamGameServer.cs:288`), the shared-socket pair `HandleIncomingPacket`/`GetNextOutgoingPacket` (`:386`, `:397`), and `CancelAuthTicket` (`:320`). `GetServerReputation`, `AssociateWithClan` and `ComputeNewPlayerCompatibility` correctly return `CallResult<T>` seeded with `IsServer` (`:366`, `:411`, `:422`).
- **Generated `ISteamGameServerStats` binding is complete and exact** — all 10 methods of `isteamgameserverstats.h`, with the `STEAM_FLAT_NAME(GetUserStatInt32/GetUserStatFloat/SetUserStatInt32/SetUserStatFloat)` overloads mapped to the right entry points (`ISteamGameServerStats.cs:38-93`).
- **Every interface's client/server accessor choice in `Generated/Interfaces/` matches the SDK.** Swept all 36 files; no interface declares a game-server accessor the SDK doesn't have, and every interface the SDK gives a game-server accessor to declares one. `ISteamNetworkingUtils` correctly uses `GetGlobalInterfacePointer`.
- **`SteamServerClass<T>` / `SteamClientClass<T>` enforce their side.** `SteamInterface.cs:132-135` and `:109-112` throw `NotSupportedException` on the wrong-side `SetInterface`, so `SteamServer`, `SteamServerStats`, `SteamUser`, `SteamMatchmakingServers` etc. cannot be mis-registered.
- **`STEAMGAMESERVER_QUERY_PORT_SHARED` is handled correctly.** `SteamServerInit.WithQueryShareGamePort()` (`Structs/ServerInit.cs:63-67`) sets `QueryPort = 0xFFFF`, matching `steam_gameserver.h:29` (`const uint16 STEAMGAMESERVER_QUERY_PORT_SHARED = 0xffff;`). Because it is a struct instance method it mutates the receiver *and* returns a copy, so both `init.WithQueryShareGamePort();` and `init = init.WithQueryShareGamePort();` work. `SteamServer.GetOutgoingPacket` / `HandleIncomingPacket` (`SteamServer.cs:436-475`) are the correct partner APIs per `isteamgameserver.h:189-207`.
- **`eServerMode` mapping is correct.** `SteamServer.cs:82` `var secure = (int)(init.Secure ? 3 : 2);` matches `steam_gameserver.h:17-23` (`eServerModeAuthenticationAndSecure = 3`, `eServerModeAuthentication = 2`).
- **No `SteamPort` parameter — correct.** See F15.
- **There is a real, separate server-side callback pump.** `Dispatch.ServerPipe = SteamGameServer.GetHSteamPipe()` (`SteamServer.cs:108`) via `SteamGameServer_GetHSteamPipe` (`Classes/SteamGameServer.cs:20-21`), a dedicated `LoopServerAsync` (`Dispatch.cs:250-257`), a manual `SteamServer.RunCallbacks()` for `asyncCallbacks:false` (`SteamServer.cs:175-181`), and `ProcessCallback` filters strictly on `item.server != isServer` (`Dispatch.cs:148-149`). `ShutdownServer` removes only server callbacks and server call-results (`Dispatch.cs:308-319`). The separation is real and correct — F13 is about the shared reentrancy guard, not about the pipe separation.
- **`ServerFilterMarshaler` is correct.** `MatchMakingKeyValuePair` uses `ByValTStr` fixed 256-byte fields (`Structs/MatchMakingKeyValuePair.cs:9-13`) matching `matchmakingtypes.h`, so `Marshal.StructureToPtr(filter, itemDst, false)` allocates no owned native memory and `FreeHGlobal` of the two blocks is a complete cleanup (`ServerFilterMarshaler.cs:44-57`). The array-of-pointers layout matches `MatchMakingKeyValuePair_t **ppchFilters`.
- **No managed callback object is ever handed to native code in the server browser** — see the note under F6.
- **Platform/marshalling fundamentals are right for Linux**: pack sizes match `steamclientpublic.h:1161-1178`, `Cdecl` on all 1024 P/Invokes, no Windows-only APIs in the library. F8 is a packaging problem, not a portability problem in the code.
- **The solution builds clean**: 0 errors across `netstandard2.1`, `net6.0`, `net46` for Win32/Win64/Posix plus both test projects.

---

## Gaps vs the SDK (server-side features not exposed at all)

**`ISteamGameServer` methods bound in `Generated/` but with no `SteamServer` facade wrapper:**

| SDK method | Header | Generated at | Why it matters for a dedicated server |
|---|---|---|---|
| `WasRestartRequested()` | `isteamgameserver.h:75` | `ISteamGameServer.cs:139` | The documented mechanism for "the master server wants you to restart for an update." A dedicated server that never polls this runs a stale build and gets delisted. **Highest-value missing wrapper.** |
| `BSecure()` | `:70` | `:116` | Whether the server is actually VAC-secure. |
| `SetGameData(const char*)` | `:134` | `:256` | Required to use the `gamedataand`/`gamedataor`/`gamedatanor` server-browser filters (documented in `isteammatchmaking.h`). Without it those filters can never match. |
| `SetRegion(const char*)` | `:137` | `:267` | Region field in the browser. |
| `SetSpectatorPort` / `SetSpectatorServerName` | `:111`, `:116` | `:202`, `:212` | Spectator advertisement, including the FakeIP second-port behaviour. |
| `RequestUserGroupStatus` (+ `GSClientGroupStatus_t`) | `:175` | `:342` | Steam-group-based admin/whitelist gating. |
| `AssociateWithClan` | `:215` | `:408` | |
| `ComputeNewPlayerCompatibility` | `:219` | `:419` | Player-compatibility / avoid-list checks before admitting a player. |
| `CreateUnauthenticatedUserConnection` | `:242` | `:442` | Bots that appear in the player list. |
| `CancelAuthTicket` | `:167` | `:320` | See F2. |
| `GetGameplayStats` / `GetServerReputation` | `:180`, `:182` | `:353`, `:363` | Deprecated in the SDK; fine to skip, but note `GetGameplayStats` is marked "will not return results". |

**`ISteamGameServerStats`:** `UpdateUserAvgRateStat` (`isteamgameserverstats.h:49`, generated at `ISteamGameServerStats.cs:106`) has no facade wrapper.

**Callbacks:** ten server-side callbacks generated but never installed — see F11.

**`ISteamHTTP` server-side is entirely absent.** The SDK exposes `SteamGameServerHTTP` (`isteamhttp.h:151`, `SteamAPI_SteamGameServerHTTP_v003`), the binding exists (`ISteamHTTP.cs:24`), and `SteamServer.cs:114` has `//AddInterface<ISteamHTTP>();` commented out — but there is **no `SteamHttp` facade class anywhere in the library**, client or server. A dedicated server that wants to call a web backend must use `System.Net.Http` directly (which is fine, but the gap should be a documented decision rather than a commented-out line).

**Server browser (`ISteamMatchmakingServers`) methods bound but not surfaced by `ServerList`:** `RefreshQuery`, `RefreshServer`, `PingServer`, `PlayerDetails`, `ServerRules`, `CancelServerQuery`, `RequestSpectatorServerList` (all in `Generated/Interfaces/ISteamMatchmakingServers.cs`). `ServerInfo.QueryRulesAsync()` goes out over a raw UDP socket via `Utility/SourceServerQuery.cs` instead of using `ISteamMatchmakingServers::ServerRules` — a deliberate choice (it avoids the response-object lifetime problem) worth documenting.

**Test coverage gaps on the flagship path.** `SteamServer.Init` is called from exactly one place in the whole repo (`Facepunch.Steamworks.Test/AppTest.cs:61`, inside `[AssemblyInitialize]`), always with the same parameters. Never exercised anywhere: `SteamServer.Shutdown()`, `SteamServer.RunCallbacks()` / the `asyncCallbacks:false` manual-pump path, the double-init guard, `WithQueryShareGamePort()`, a non-null `IpAddress`, `Secure = false`, `GetOutgoingPacket`/`HandleIncomingPacket`, `SteamServer.GetAuthSessionTicket` (the fork's own addition — `GameServerTest.cs:50` uses the *client* `SteamUser.GetAuthSessionTicket`), `EndSession`, `UserHasLicenseForApp`, `SetKey`/`ClearKeys`, `UpdatePlayer`, `LogOn(token)`, `LogOff`, `LoggedOn`, `SteamId`, four of the five server events, and seven of nine `SteamServerStats` methods. `GameServerTest.Init()` (`GameServerTest.cs:13-18`) has zero assertions and leaves `SteamServer.DedicatedServer = false` set globally for the rest of the run; `GameServerTest.BeginAuthSession` (`:61`) leaks a static event handler that is never unsubscribed; `PublicIp()` has an unbounded `while(true)` and an assertion at `:33` that is unreachable when false. `Facepunch.Steamworks.Test/Client/Server/StatsTest.cs` is 100% dead — the whole body is inside a `/* … */` block and targets a removed API.

---

## Open questions requiring a live Steam game server

1. **F1 severity confirmation.** Does `SteamAPI_ISteamApps_GetCurrentGameLanguage(IntPtr.Zero)` actually access-violate, or does the flat wrapper null-check? Run a dedicated-server-only process (no `SteamClient.Init`) and touch any `SteamApps` member. The fix is warranted either way, but this settles CRITICAL vs HIGH.
2. **Is `GetAuthSessionTicketResponse_t` delivered on the game-server pipe?** It is defined in `isteamuser.h:354-356` in the `k_iSteamUserCallbacks` range. If it is delivered, F9 is fixable with a server-side `GetAuthSessionTicketAsync`; if not, the server ticket is usable immediately and F9 downgrades to a documentation note.
3. **Does `SteamInternal_GameServer_Init_V2` accept an interface-version list containing `STEAMAPPS_INTERFACE_VERSION008`?** (F15) It presumably does today, since the library ships this way, but confirm it does not produce a warning in `SteamErrMsg` on a current Steam build.
4. **Does `ISteamMatchmakingServers::Request*ServerList` officially tolerate a NULL `ISteamMatchmakingServerListResponse*`?** The polling design depends on it and the header does not say so. Worth confirming with Valve or by watching for `RefreshComplete` dispatch attempts. If NULL ever stops being tolerated, every `ServerList` class breaks at once.
5. **`SteamUser.AdvertiseGame` byte order.** Is the `ip` parameter host order or network order, and does the friends "join game" flow actually resolve? The studio restored this from a binary, so it has no upstream reference.
6. **F3 reproduction.** Init → LogOnAnonymous → Shutdown → Init → LogOnAnonymous in one process, then check the server browser entry for mod dir / description / max players. This should show the stale-cache bug directly.
7. **F13 under load.** With both `SteamClient.Init` and `SteamServer.Init` live, instrument `Dispatch.Frame` to count early-returns per pipe and confirm how often server callbacks are being skipped.
8. **F6 reproduction.** Start `RunQueryAsync` without awaiting it, `Dispose()` the list ~100 ms later, repeat in a loop under a native debugger; expect a fault inside `steamclient` on `GetServerCount`/`GetServerDetails`.
9. **Linux end-to-end.** After fixing F8, verify `libsteam_api.so` loads on a headless Linux host with no Steam client installed, that `SteamInternal_GameServer_Init_V2` succeeds, and that the `net6.0` leg (EOL) is still an acceptable runtime target for the studio's deployment.
