# Marshaling & ABI Audit

Facepunch.Steamworks — C# ↔ Steamworks SDK native boundary.
Audit performed against the C++ headers in `Generator/steam_sdk/*.h` and `Generator/steam_sdk/steam_api.json`, which are treated as authoritative.

Steam was **not** installed and no native Steam call was made. Everything below is static analysis, hand-computed C++ layout from header text, and pure-managed measurement via `Marshal.SizeOf` / `Marshal.OffsetOf`.

---

## Summary

| | |
|---|---|
| Structs compared field-by-field (Windows x64) | **240** |
| Structs compared field-by-field (POSIX x64) | **240** |
| P/Invoke declarations inspected | **1018** |
| Callback IDs cross-checked (C# ↔ headers ↔ json) | **219** |
| **Structs with proven wrong field offsets** | **5** (Windows x64), **1** (POSIX x64) — all **FIXED** |
| **Structs with proven wrong total size** | **17** (Windows x64), **5** (POSIX x64) — **16 FIXED**, 1 still open (F6) |
| **String-encoding defects** | **3 sites** (22 fields) — open |

The size counts are 15 found by the automated diff plus 2 computed by hand — `RequestPlayersForGameResultCallback_t` and `SteamInputActionEvent_t` contain nested types the diff tool could not resolve, so they were derived manually from the header text (F1a, F6).

**Overall state.** The *mechanical* parts of the binding are in very good shape: calling convention is `Cdecl` everywhere (1018/1018), every `bool`-returning P/Invoke has `[return: MarshalAs(UnmanagedType.I1)]` (0 violations), every `bool` parameter has `I1` (0 violations), no P/Invoke takes a raw `System.String`, all 219 callback IDs match the headers exactly, and the UTF-8 string helper layer (`Utf8StringToNative` / `Utf8StringPointer` / `Helpers.MemoryToString`) is correct and consistent across `net46` / `netstandard2.1` / `net6.0`.

The defects were concentrated in **one place**: the generator's `IsPack4OnWindows` heuristic (`Generator/SteamApiDefinition.cs`), which decided `[StructLayout(Pack = …)]` for every generated struct. It was a two-line guess, wrong in two opposite directions, and it mis-laid-out 17 structs. **F1, F2 and F3 are now fixed** — they share one root cause, and the *Resolution* note at the end of F1 covers all three. A secondary cluster, still open, is `char`-typed struct members being marshaled with the platform ANSI code page instead of UTF-8 (F4/F5).

None of the 5 offset defects sat on a code path Facepunch itself wires up, but 3 of the size defects did (`FriendRichPresenceUpdate_t`, `GameConnectedFriendChatMsg_t`, `P2PSessionConnectFail_t` are all `Dispatch.Install`ed), and `UserStatsReceived_t` — the backing type for `RequestUserStats` — had a wrong `DataSize` that was passed straight to Steam.

> **A correction to an earlier revision of this report.** F1's prose claimed "20 mis-laid-out structs". That figure did not come from the tables below; it was the number of size *changes* produced by a fix that was attempted and then reverted, several of which were regressions rather than corrections. The count the evidence in this report actually supports is **17**: 5 offset defects (F1) + 9 over-reads (F2) + 2 under-reads (F3) + `SteamInputActionEvent_t` (F6). Sixteen are fixed; F6 is not a packing defect (the generator drops the struct's `union`) and remains open.

---

## Method

### 1. Ground truth: C++ layout computed from header text

There is no C++ compiler on this machine (`cl.exe`, `g++`, `clang++` all absent), so C++ layouts were computed by a model, not measured. The model is in `scratchpad/cpplayout.js` and does the following:

1. **Recovers the `#pragma pack` context for every struct** by scanning all 45 headers line-by-line, maintaining a pack stack, and resolving the Valve conditional
   ```c
   #if defined( VALVE_CALLBACK_PACK_SMALL )
   #pragma pack( push, 4 )
   #elif defined( VALVE_CALLBACK_PACK_LARGE )
   #pragma pack( push, 8 )
   ```
   by taking the `LARGE` branch for Windows and the `SMALL` branch for POSIX, per `steamclientpublic.h:1161-1170`. It recognises both `struct X` / `class X` declarations and the `STEAM_CALLBACK_BEGIN( X, … )` macro form used throughout `isteamhtmlsurface.h`.

2. **Applies MSVC x64 layout rules**: effective member alignment = `min(natural_align, pack)`; struct alignment = max of effective member alignments; size rounded up to struct alignment; empty struct = 1 byte. `bool` = 1 byte, enum = 4 bytes, pointer = 8 bytes.

3. **Uses two hard-coded type facts taken directly from the headers** — these are the linchpin of the whole analysis:

   `steamclientpublic.h:475` opens a packing block that is not closed until `steamclientpublic.h:1108`:
   ```c
   #pragma pack( push, 1 )

   #define CSTEAMID_DEFINED

   // Steam ID structure (64 bits total)
   class CSteamID          // line 480
   ```
   `CGameID` is declared at line 922, inside the same block. Both wrap a single 8-byte union (`uint64 m_unAll64Bits` / `uint64 m_ulGameID`). Under `#pragma pack(1)` therefore:

   > **`sizeof(CSteamID) == sizeof(CGameID) == 8`, `alignof(CSteamID) == alignof(CGameID) == 1`.**

   Every finding below follows from that. Note that the generator *already assumes this* — it is the entire reason `Platform.StructPackSize = 4` exists — so this is not a novel claim, it is the codebase's own premise applied consistently.

4. Field types are read from `steam_api.json` (`fields[].fieldtype`), the same input the generator consumes, so the two sides are compared on identical field lists.

### 2. Measured managed layout

`scratchpad/LayoutProbe/` is a throwaway `net6.0` x64 console app **outside the repo** that references the built `Facepunch.Steamworks.Win64.dll` (and a sibling for `Facepunch.Steamworks.Posix.dll`), reflects over every value type, and emits `Marshal.SizeOf` plus `Marshal.OffsetOf` for every field, along with the `StructLayoutAttribute` `Pack`/`CharSet`/`Value`. 337 types measured per assembly; the 98 failures are all async state machines and managed-only helper structs.

Build used: `dotnet build Facepunch.Steamworks.Win64.csproj -c Release` — all three TFMs (`net46`, `netstandard2.1`, `net6.0`) build clean.

The two sides are then diffed by name (applying a faithful JS port of `Generator/Cleanup.cs::ConvertType` to map C++ names to C# names).

### 3. P/Invoke signature sweep

`scratchpad/PinvokeProbe/` reflects over all 1018 `PinvokeImpl` methods and checks calling convention, `bool` return/parameter marshaling, raw `string` parameters, `CharSet`, `SetLastError`, by-value struct parameters > 8 bytes, and struct return types.

### 4. Encoding reproduction

`scratchpad/CharSetRepro/` reconstructs the exact `[StructLayout]` shapes used for fixed `char[]` struct members and round-trips real strings through `Marshal.StructureToPtr` / `PtrToStructure`, printing raw bytes. Fully offline, no Steam types involved.

### 5. Delegated cross-checks

Two independent sweeps were run in parallel: a callback-ID reconciliation (headers vs `steam_api.json` vs `CustomEnums.cs`, 3-way) and a string-marshaling audit of the interface layer. Their results are folded in below.

---

## Findings

### F1 — CRITICAL — `Pack = 4` misaligns genuine 8-byte fields, shifting every subsequent field — **FIXED**

**Severity:** CRITICAL (silent memory corruption — reads structurally garbage values)

**Status:** FIXED. See *Resolution* at the end of this finding; it also resolves F2 and F3.

**Location (root cause):** `Generator/SteamApiDefinition.cs:105-119`
**Location (emitters):** `Generator/CodeWriter/Callbacks.cs:38`, `Generator/CodeWriter/Struct.cs:38`
**Location (constant):** `Facepunch.Steamworks/Utility/Platform.cs:25` — `public const int StructPackSize = 4;`

**What was wrong.** The generator decided packing with this heuristic:

```csharp
public bool IsPack4OnWindows
{
    get
    {
        // 4/8 packing is irrevant to these classes
        if ( Name.Contains( "MatchMakingKeyValuePair_t" ) ) return true;

        if ( Fields.Skip( 1 ).Any( x => x.Type.Contains( "CSteamID" ) ) )
            return true;

        if ( Fields.Skip( 1 ).Any( x => x.Type.Contains( "CGameID" ) ) )
            return true;

        return false;
    }
}
```

The *intent* is right: `CSteamID` maps to a C# `ulong`, which the CLR aligns to 8, whereas the C++ `CSteamID` is 1-aligned, so the struct must be packed down to reproduce the native offsets. But `Pack = 4` is a whole-struct switch. It does not just relax `CSteamID` — it relaxes **every** member. Any struct that contains a `CSteamID`/`CGameID` **and** a real `uint64`/`int64` gets that real 64-bit field pulled from its natural 8-byte boundary down to 4, and everything after it shifts.

**Evidence.** Five structs, computed from the headers and measured in the assembly. `cs=` is `Marshal.OffsetOf` on `Facepunch.Steamworks.Win64` (net6.0, x64); `cpp=` is the header-derived offset under `#pragma pack(8)`.

**F1a — `RequestPlayersForGameResultCallback_t`** (`Facepunch.Steamworks/Generated/SteamCallbacks.cs:878`)

`Generator/steam_sdk/isteammatchmaking.h:972-992`:
```c
struct RequestPlayersForGameResultCallback_t
{
	enum { k_iCallback = k_iSteamGameSearchCallbacks + 12 };

	EResult m_eResult;		// m_ullSearchID will be non-zero if this is k_EResultOK
	uint64  m_ullSearchID;

	CSteamID m_SteamIDPlayerFound; // player steamID
	CSteamID m_SteamIDLobby;	// if the player is in a lobby, the lobby ID
	enum PlayerAcceptState_t { … };
	PlayerAcceptState_t m_ePlayerAcceptState;
	int32 m_nPlayerIndex;
	int32 m_nTotalPlayersFound;		// expect this many callbacks at minimum
	int32 m_nTotalPlayersAcceptedGame;
	int32 m_nSuggestedTeamIndex;
	uint64 m_ullUniqueGameID;
};
```

| field | C# offset (measured) | C++ offset (header) |
|---|---|---|
| `m_eResult` | 0 | 0 |
| `m_ullSearchID` | **4** | **8** |
| `m_SteamIDPlayerFound` | **12** | **16** |
| `m_SteamIDLobby` | **20** | **24** |
| `m_ePlayerAcceptState` | **28** | **32** |
| `m_nPlayerIndex` | **32** | **36** |
| `m_nTotalPlayersFound` | **36** | **40** |
| `m_nTotalPlayersAcceptedGame` | **40** | **44** |
| `m_nSuggestedTeamIndex` | **44** | **48** |
| `m_ullUniqueGameID` | **48** | **56** |
| **total size** | **56** | **64** |

9 of 10 fields wrong. Every value read from this callback is garbage.

**F1b — `SteamInputConfigurationLoaded_t`** (`SteamCallbacks.cs:1972`)

`Generator/steam_sdk/isteaminput.h:974-987`:
```c
struct SteamInputConfigurationLoaded_t
{
	enum { k_iCallback = k_iSteamControllerCallbacks + 3 };
	AppId_t			m_unAppID;
	InputHandle_t	m_ulDeviceHandle;		// Handle for device
	CSteamID		m_ulMappingCreator;		// May differ from local user when using
	uint32			m_unMajorRevision;
	uint32			m_unMinorRevision;
	bool			m_bUsesSteamInputAPI;
	bool			m_bUsesGamepadAPI;
};
```
C# offsets `0, 4, 12, 20, 24, 28, 29` (size 32) vs C++ `0, 8, 16, 24, 28, 32, 33` (size 40). 6 of 7 fields wrong.

**F1c — `JoinPartyCallback_t`** (`SteamCallbacks.cs:947`)

`isteammatchmaking.h:1032-1040`:
```c
struct JoinPartyCallback_t
{
	enum { k_iCallback = k_iSteamPartiesCallbacks + 1 };

	EResult m_eResult;
	PartyBeaconID_t m_ulBeaconID;
	CSteamID m_SteamIDBeaconOwner;
	char m_rgchConnectString[256];
};
```
C# `0, 4, 12, 20` (size 276) vs C++ `0, 8, 16, 24` (size 280). 3 of 4 wrong, including the connect string.

**F1d — `SubmitPlayerResultResultCallback_t`** (`SteamCallbacks.cs:920`)

`isteammatchmaking.h:1008-1015`:
```c
struct SubmitPlayerResultResultCallback_t
{
	enum { k_iCallback = k_iSteamGameSearchCallbacks + 14 };

	EResult m_eResult;
	uint64 ullUniqueGameID;
	CSteamID steamIDPlayer;
};
```
C# `0, 4, 12` (size 20) vs C++ `0, 8, 16` (size 24).

**F1e — `PSNGameBootInviteResult_t`** (`SteamCallbacks.cs:804`) — *the only offset defect that also affects POSIX*

`isteammatchmaking.h:897-903`:
```c
struct PSNGameBootInviteResult_t
{
	enum { k_iCallback = k_iSteamMatchmakingCallbacks + 15 };

	bool m_bGameBootInviteExists;
	CSteamID m_steamIDLobby;		// Should be valid if m_bGameBootInviteExists == true
};
```
Here the failure is the reverse: `Pack = 4` is *too large*. C++ places the 1-aligned `CSteamID` immediately after the `bool`, at offset **1**; C# places it at **4**. Size 9 vs 12. Wrong on Windows *and* POSIX (POSIX uses `Pack = 4` for everything).

**Impact.** `Dispatch.ProcessCallback` (`Facepunch.Steamworks/Classes/Dispatch.cs:156`) hands the raw native pointer to the handler, which does `Marshal.PtrToStructure`. With shifted offsets, every field after the first is read from the wrong bytes — `SteamId`s become nonsense, string buffers start mid-word, enum fields land on padding. For `CallResult<T>` (`Callbacks/CallResult.cs:67`) the same struct is used to interpret the buffer Steam filled.

None of these five are currently `Dispatch.Install`ed by Facepunch's own wrappers (verified: 0 references outside `Generated/`), so today the corruption is only reachable by a consumer using `Dispatch.Install<T>` directly, or by hooking the public `Dispatch.OnDebugCallback`, which marshals *every* arriving callback through `CallbackTypeFactory.All` (`Dispatch.cs:168-171`). That makes it latent rather than live — but it is unambiguously wrong, and F1e/F1c are ordinary matchmaking/parties callbacks a game could reasonably subscribe to.

---

#### Resolution — F1, F2 and F3

The fix is *model the type, not the struct*, and it lands in three parts.

**1. An alignment-1 id type.** `Facepunch.Steamworks/Structs/PackedId.cs`:

```csharp
[StructLayout( LayoutKind.Sequential, Pack = 1 )]
internal struct PackedId { internal ulong Value; /* implicit ⇄ ulong, SteamId, GameId */ }
```

`Pack = 1` is load-bearing and the file says so at length. It is deliberately a *new* type rather than `Pack = 1` on `SteamId`: `SteamId` is public and is a by-value P/Invoke argument in over a hundred generated methods, so it stays untouched. The implicit conversions mean every consumer compiled unchanged except one (see part 2).

**2. The generator emits it.** `Generator/CodeWriter/Struct.cs::FieldType` maps every `CSteamID`/`CGameID` member to `PackedId` instead of a raw `ulong`. Emitting `SteamId` would not have worked: the generated structs never used it — `ToManagedType` collapsed `CSteamID` to `ulong` — which is why the first attempt at this fix (putting `Pack = 1` on `SteamId` and flipping the pack) was reverted. It changed 20 struct sizes, several of them regressions: `FriendsGetFollowerCount_t` went 16 → 24 when 16 was already correct.

The SDK's one native id *array*, `FriendsEnumerateFollowingList_t.m_rgSteamID` (`CSteamID[50]`), becomes `fixed byte[400]`. A C# `fixed` buffer takes its alignment from its element type and only accepts primitives, so `fixed ulong[50]` would sit on an 8-byte boundary where native sits on 1 — under `Pack = 8` the array would have moved from offset 4 to 8 and the struct grown 412 → 424. `byte` gives the same 400 bytes at alignment 1. Its one consumer, `SteamFriends.AddFollowedIds`, now reassembles each id byte-wise so no alignment is assumed.

**3. Only then, delete the heuristic.** `IsPack4OnWindows` is gone and every generated struct carries `Platform.StructPlatformPackSize` — the header's own value, 8 on Windows and 4 on POSIX. The `MatchMakingKeyValuePair_t` special case inside it was dead code: `Cleanup.ShouldCreate` excludes that struct from generation entirely (it is hand-written in `Structs/MatchMakingKeyValuePair.cs`), and its measured layout is unchanged at 512 bytes. `Platform.StructPackSize` survives only as that hand-written struct's pack.

**Measured outcome.** Windows x64, `Marshal.SizeOf`/`OffsetOf` against the header-derived model:

| Struct | was | now | header-derived | finding |
|---|---|---|---|---|
| `RequestPlayersForGameResultCallback_t` | 56 | **64** | 64 | F1a |
| `SteamInputConfigurationLoaded_t` | 32 | **40** | 40 | F1b |
| `JoinPartyCallback_t` | 276 | **280** | 280 | F1c |
| `SubmitPlayerResultResultCallback_t` | 20 | **24** | 24 | F1d |
| `PSNGameBootInviteResult_t` | 12 | **9** | 9 | F1e |
| `P2PSessionConnectFail_t` | 16 | **9** | 9 | F2 |
| `AvatarImageLoaded_t` | 24 | **20** | 20 | F2 |
| `FriendRichPresenceUpdate_t` | 16 | **12** | 12 | F2 |
| `JoinClanChatRoomCompletionResult_t` | 16 | **12** | 12 | F2 |
| `GameConnectedFriendChatMsg_t` | 16 | **12** | 12 | F2 |
| `GSClientDeny_t` | 144 | **140** | 140 | F2 |
| `GSClientKick_t` | 16 | **12** | 12 | F2 |
| `GameConnectedChatLeave_t` | 20 | **18** | 18 | F2 |
| `GSClientGroupStatus_t` | 20 | **18** | 18 | F2 |
| `UserStatsReceived_t` | 20 | **24** | 24 | F3 |
| `SearchForGameProgressCallback_t` | 36 | **40** | 40 | F3 |

Every field offset in F1a–F1e now matches the C++ column of the tables above exactly. The full header-derived diff goes from **19 mismatches to 0** on Windows and **4 to 0** on POSIX; the only entries left are the four hand-written structs whose field *count* deliberately differs from the C++ definition (`NetIdentity`, `ConnectionInfo`, `NetPingLocation`, `NetKeyValue`) while their total size matches — a known and legitimate pattern, see *Verified-correct areas*.

A further **17** structs changed only their recorded `Pack` attribute, with size and every field offset byte-identical: `gameserveritem_t`, `FriendGameInfo_t`, `ValidateAuthTicketResponse_t`, `GameLobbyJoinRequested_t`, `GameConnectedClanChatMsg_t`, `GameConnectedChatJoin_t`, `FriendsGetFollowerCount_t`, `FriendsIsFollowing_t`, `FriendsEnumerateFollowingList_t`, `EquippedProfileItems_t`, `SearchForGameResultCallback_t`, `ReservationNotificationCallback_t`, `SteamInventoryEligiblePromoItemDefIDs_t`, `GSClientApprove_t`, `ComputeNewPlayerCompatibilityResult_t`, `GSStatsReceived_t`, `GSStatsStored_t`. These are exactly the "accidentally right" set: a 4-byte field preceded the id, so a pack-4 `ulong` happened to land on the native offset. Nothing else in the assembly moved, and **no public type changed** — `PackedId` is `internal`, and the compiler would have rejected any public member exposing it.

Pinned by `verify-struct-layout.ps1` against `Tools/baselines/layout-win64.txt`.

**Not fixed by this:** `SteamInputActionEvent_t` (F6). Its size is wrong because the generator drops its `union`, not because of packing, so no `Pack` value can correct it. It is unchanged at 16 bytes against a native 33.

---

### F2 — HIGH — 9 callback structs are larger in C# than in native memory (out-of-bounds read) — **FIXED**

**Severity:** HIGH (reads past the end of Steam's callback buffer)

**Status:** FIXED — same root cause as F1, see the *Resolution* note there. All nine now measure their header-derived size.

**Root cause:** the `Fields.Skip(1)` in `Generator/SteamApiDefinition.cs:110,113`. When `CSteamID`/`CGameID` is the **first** field it is skipped, so `IsPack4OnWindows` returns `false` and the struct gets `Pack = 8`. Offsets still line up (the SteamID is at 0 either way), but the C# struct's *alignment* becomes 8 because of the `ulong`, while the C++ struct's alignment is only 4 or 1 — so the CLR adds tail padding that native does not have.

**Evidence.** All measured (`Marshal.SizeOf` on `Facepunch.Steamworks.Win64`, net6.0 x64) vs header-derived.

| Struct | C# size | C++ size | over-read | C# `Pack` | Header |
|---|---|---|---|---|---|
| `P2PSessionConnectFail_t` | **16** | **9** | +7 | 8 | `isteamnetworking.h:322` |
| `AvatarImageLoaded_t` | **24** | **20** | +4 | 8 | `isteamfriends.h:562` |
| `FriendRichPresenceUpdate_t` | **16** | **12** | +4 | 8 | `isteamfriends.h:587` |
| `JoinClanChatRoomCompletionResult_t` | **16** | **12** | +4 | 8 | `isteamfriends.h:656` |
| `GameConnectedFriendChatMsg_t` | **16** | **12** | +4 | 8 | `isteamfriends.h:666` |
| `GSClientDeny_t` | **144** | **140** | +4 | 8 | `isteamgameserver.h:295` |
| `GSClientKick_t` | **16** | **12** | +4 | 8 | `isteamgameserver.h:305` |
| `GameConnectedChatLeave_t` † | **20** | **18** | +2 | 4 | `isteamfriends.h:633` |
| `GSClientGroupStatus_t` † | **20** | **18** | +2 | 4 | `isteamgameserver.h:344` |

† also wrong on POSIX (struct ends in two `bool`s, so C++ alignment is 1 and there is no tail padding at all; C# pads to the `Pack` multiple on both platforms).

Representative header quotes:

`isteamnetworking.h:322-327` — worst case, 7 bytes:
```c
struct P2PSessionConnectFail_t
{ 
	enum { k_iCallback = k_iSteamNetworkingCallbacks + 3 };
	CSteamID m_steamIDRemote;			// user we were sending packets to
	uint8 m_eP2PSessionError;			// EP2PSessionError indicating why we're having trouble
};
```
C++: `CSteamID`@0 (8 bytes, align 1), `uint8`@8 → size **9**, alignment 1. C# with `Pack = 8`: `ulong`@0, `byte`@8 → padded to **16**.

`isteamfriends.h:562-568`:
```c
struct AvatarImageLoaded_t
{
	enum { k_iCallback = k_iSteamFriendsCallbacks + 34 };
	CSteamID m_steamID; // steamid the avatar has been loaded for
	int m_iImage; // the image index of the now loaded image
	int m_iWide; // width of the loaded image
	int m_iTall; // height of the loaded image
};
```
C++: 8 + 4 + 4 + 4 = **20**, alignment 4. C#: **24**.

`isteamfriends.h:633-640`:
```c
struct GameConnectedChatLeave_t
{
	enum { k_iCallback = k_iSteamFriendsCallbacks + 40 };
	CSteamID m_steamIDClanChat;
	CSteamID m_steamIDUser;
	bool m_bKicked;		// true if admin kicked
	bool m_bDropped;	// true if Steam connection dropped
};
```
C++: 8 + 8 + 1 + 1 = **18**, alignment 1. C# (`Pack = 4`): **20**.

**Impact.** All field offsets are correct, so the *values* read are correct. The defect is the read length. `Dispatch.ProcessCallback` passes `msg.Data` — a pointer into Steam's own callback buffer, whose true length is `msg.DataSize` — to `Marshal.PtrToStructure`, which reads `Marshal.SizeOf(T)` bytes. That is a read of up to 7 bytes past the object.

Three of these are live today (`Dispatch.Install` sites): `FriendRichPresenceUpdate_t` (`SteamFriends.cs:40`), `GameConnectedFriendChatMsg_t` (`SteamFriends.cs:35`), `P2PSessionConnectFail_t` (`SteamNetworking.cs:31`).

Whether the over-read ever faults depends on Steam's internal allocator — if the callback buffer is a pooled block the extra bytes are almost always mapped, which is why this has gone unnoticed. I cannot test that here. It is nevertheless an out-of-bounds read and is exactly the class of thing that turns into a rare, unreproducible crash.

**Fix applied.** See the *Resolution* note under F1: the id members are now `PackedId`, a `Pack = 1` wrapper, so the C# struct's natural alignment matches C++ and the tail padding disappears. All nine measure their header-derived size — the +7 over-read on `P2PSessionConnectFail_t` is 0.

**Still worth doing, as defence in depth:** `Dispatch.ProcessCallback` could refuse to marshal when `msg.DataSize < T._datasize` and surface it through `OnException` instead of reading anyway. That would turn any *future* layout mistake into a diagnosable error rather than an out-of-bounds read.

---

### F3 — HIGH — `UserStatsReceived_t` is *smaller* than native; its `DataSize` is passed to `GetAPICallResult` — **FIXED**

**Severity:** HIGH (silent permanent failure of an async API)

**Status:** FIXED — same root cause as F1, see the *Resolution* note there. `Marshal.SizeOf(UserStatsReceived_t)` is now **24**, so `CallResult` passes `cubCallback = 24` for a 24-byte result. `SearchForGameProgressCallback_t` is now 40. The open question below — whether Steam *rejects* a short `cubCallback` — is moot for these two, since the value is now correct either way.

**Location:** `Facepunch.Steamworks/Generated/SteamCallbacks.cs:1425-1435`; consumed at `Facepunch.Steamworks/Callbacks/CallResult.cs:54,59` and `Generated/Interfaces/ISteamUserStats.cs:218-221`

**What's wrong.** `Generator/steam_sdk/isteamuserstats.h:327-333`:
```c
struct UserStatsReceived_t
{
	enum { k_iCallback = k_iSteamUserStatsCallbacks + 1 };
	uint64		m_nGameID;		// Game these stats are for
	EResult		m_eResult;		// Success / error fetching the stats
	CSteamID	m_steamIDUser;	// The user for whom the stats are retrieved for
};
```
C++ under `#pragma pack(8)`: `uint64`@0, `EResult`@8, `CSteamID`@12 (1-aligned) → 20 bytes of data, struct alignment **8** (because of the `uint64`) → **size 24**.
C# with `Pack = 4`: same offsets `0, 8, 12` → **size 20**.

Offsets match, so field reads are fine. The problem is the size, and this struct is used as a **CallResult**:

`Generated/Interfaces/ISteamUserStats.cs:218`
```csharp
internal CallResult<UserStatsReceived_t> RequestUserStats( SteamId steamIDUser )
```

`Callbacks/CallResult.cs:53-59`
```csharp
var t = default( T );
var size = t.DataSize;                       // == Marshal.SizeOf(UserStatsReceived_t) == 20
var ptr = Marshal.AllocHGlobal( size );
…
if ( !utils.GetAPICallResult( call, ptr, size, (int)t.CallbackType, ref failed ) || failed )
```

`isteamutils.h:105` — `cubCallback` is the buffer size Steam validates against the real callback size:
```c
virtual bool GetAPICallResult( SteamAPICall_t hSteamAPICall, void *pCallback, int cubCallback, int iCallbackExpected, bool *pbFailed ) = 0;
```

**Impact.** Steam is handed `cubCallback = 20` for a 24-byte result. If Steam enforces an exact match (which its `k_ESteamAPICallFailureMismatchedCallback` machinery and standard behaviour suggest), `GetAPICallResult` returns `false`, `GetResult()` returns `null` (`CallResult.cs:62`), and `RequestUserStats` resolves to `null` **forever, with no exception** — the failure is swallowed at `CallResult.cs:61` into an optional debug hook.

`SearchForGameProgressCallback_t` (`SteamCallbacks.cs:830`, `isteammatchmaking.h:924-936`) has the identical shape: C# **36** vs C++ **40**, offsets correct.

**Honest caveat.** I have *proven* the size mismatch (measured 20, header-derived 24). I have **not** proven that Steam rejects it — that requires a live client. If Steam only checks `cubCallback >= actual`, this degrades to a truncated-copy rather than a hard failure. Either way the value passed is wrong. This is the one finding in this report where the *consequence*, not the defect, needs runtime confirmation.

**Fix applied.** Same root fix as F1/F2 — with the id member typed `PackedId` and the struct carrying `Pack = 8` on Windows, `Marshal.SizeOf` yields 24 and the call is correct. Note the fix is *not* "make `SteamId` 1-aligned", which is what an earlier revision of this report recommended and what the reverted attempt tried: these structs never used `SteamId`. See the *Resolution* note under F1.

---

### F4 — HIGH — Fixed `char[]` struct members are marshaled with the OS ANSI code page, not UTF-8

**Severity:** HIGH (proven data corruption; locale-dependent, so it passes CI on en-US and fails in the field)

**Locations:**
- `Facepunch.Steamworks/Structs/MatchMakingKeyValuePair.cs:6-13` — **write** path, explicit `CharSet = CharSet.Ansi`
- `Facepunch.Steamworks/Networking/ConnectionInfo.cs:8,21,23` — **read** path, `CharSet` omitted (defaults to `Ansi`)

**What's wrong.**
```csharp
// Structs/MatchMakingKeyValuePair.cs:6
[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPackSize, CharSet = CharSet.Ansi )]
internal partial struct MatchMakingKeyValuePair
{
	[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
	internal string Key;

	[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
	internal string Value;
}
```
`ByValTStr` + `CharSet.Ansi` transcodes through `WideCharToMultiByte(CP_ACP, …)` — the machine's legacy ANSI code page. The native fields are plain `char[256]` (`matchmakingtypes.h:40-41`) which Steam treats as UTF-8.

`Networking/ConnectionInfo.cs:8` declares `[StructLayout( LayoutKind.Sequential, Size = 696 )]` with no `CharSet`; the reflected value is `Ansi` (measured), and `endDebug` / `connectionDescription` are `ByValTStr[128]` at offsets 184 and 312 — mapping to `m_szEndDebug` / `m_szConnectionDescription`, which Steam fills with UTF-8.

**Evidence (measured, `scratchpad/CharSetRepro`, Windows CP_ACP = 1252):**

```
default StructLayout CharSet when unspecified = Ansi
sizeof(KVP_Ansi) = 512  (C++ MatchMakingKeyValuePair_t = 512)

WRITE path (managed -> native):
  Value="Zürich"
    marshalled bytes : 5A FC 72 69 63 68
    correct UTF-8    : 5A C3 BC 72 69 63 68     <-- corrupted
  Value="日本語"
    marshalled bytes : 3F 3F 3F                  ("???" — total data loss)
    correct UTF-8    : E6 97 A5 E6 9C AC E8 AA 9E
  Value="café"
    marshalled bytes : 63 61 66 E9
    correct UTF-8    : 63 61 66 C3 A9            <-- corrupted

READ path (native UTF-8 -> managed):
  native UTF-8 "Zürich" -> managed "ZÃ¼rich"     <-- mojibake
  native UTF-8 "日本語" -> managed "æ—¥æœ¬èªž"     <-- mojibake
```

Note `日本語` marshals to `3F 3F 3F` — three `?` characters. The data is not merely mis-encoded, it is destroyed and unrecoverable.

**Impact.** `MatchMakingKeyValuePair` is the server-browser filter type, reachable from public API: `ServerList/Base.cs:122-125`
```csharp
public void AddFilter( string key, string value )
{
    filters.Add( new MatchMakingKeyValuePair { Key = key, Value = value } );
}
```
and marshaled at `ServerList/ServerFilterMarshaler.cs:39` (`Marshal.StructureToPtr`). Any non-ASCII filter value — a map name, a gametag, a server-name substring — is corrupted before it reaches Steam, so the filter silently matches nothing. Behaviour depends on the *player's* Windows locale, and is accidentally correct on .NET Core/Unix (where `CharSet.Ansi` maps to UTF-8), which is exactly the profile of a bug that survives testing.

Also measured: a value longer than 255 chars is silently truncated, no exception.

**Recommended fix.** Do not use `ByValTStr`. Declare these as `[MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)] byte[]` — which is what the generator already does for every other `char[N]` member (`Generator/CodeWriter/Struct.cs:79-86`) — and convert with the existing `Steamworks.Utility.Utf8NoBom`. That makes the encoding explicit and platform-independent.

---

### F5 — HIGH — `const char *` members of callback structs are typed `string` (ANSI decode); one of them is a binary pixel buffer

**Severity:** HIGH for `HTML_NeedsPaint_t`; MEDIUM for the rest

**Location:** `Generator/CodeWriter/Struct.cs:61-155` (`StructFields` has no case for `const char *`, so it falls through to the raw type name), producing 20 fields across `Facepunch.Steamworks/Generated/SteamCallbacks.cs`.

**What's wrong.** The generator emits pointer-to-char struct members as bare `System.String`:

```csharp
// Generated/SteamCallbacks.cs:2303-2317  (HTML_NeedsPaint_t)
internal struct HTML_NeedsPaint_t : ICallbackData
{
    internal uint UnBrowserHandle; // unBrowserHandle HHTMLBrowser
    internal string PBGRA;         // pBGRA const char *
    internal uint UnWide;          // unWide uint32
    …
```

The declaring `[StructLayout]` has no `CharSet`, so the field marshals as `LPStr` — ANSI, not UTF-8 — the same defect as F4 but on the read path, for 20 fields:

`SteamCallbacks.cs:2306, 2329, 2330, 2331, 2358, 2359, 2362, 2377, 2378, 2391, 2404, 2485, 2502, 2515, 2528, 2529, 2542, 2573, 2586, 2599` — URLs, page titles, chat messages, file names.

`PBGRA` is worse than mis-encoded. `isteamhtmlsurface.h:236`:
```c
STEAM_CALLBACK_MEMBER(1, const char *, pBGRA ) // a pointer to the B8G8R8A8 data for this surface, valid until SteamAPI_RunCallbacks is next called
```
It is a raw BGRA framebuffer, not a string. `Marshal.PtrToStructure` will scan it for a NUL byte and allocate a managed string from pixel data. For a fully-opaque surface (alpha = 0xFF everywhere) there may be no zero byte for megabytes, producing an enormous allocation and a long unbounded read. Sizes/offsets themselves are correct here (measured 56 = header-derived 56), so this is purely a field-*type* defect.

**Impact, honestly scoped.** `grep` confirms no file outside `Generated/` references `HTMLSurface` or any `HTML_*` type — Facepunch never initialises the HTML surface, so these callbacks never fire in the library's own usage, and the structs are `internal`. The exposure is (a) a consumer driving `ISteamHTMLSurface` directly, and (b) `Dispatch.OnDebugCallback`, which marshals every arriving callback via `CallbackTypeFactory.All` (`Dispatch.cs:168-171`) — `HTML_NeedsPaint` is registered there at `Generated/CustomEnums.cs:401`.

**Recommended fix.** Add a `const char *` case to `Generator/CodeWriter/Struct.cs` that emits `IntPtr` plus a `…UTF8()` accessor using `Utf8StringPointer`'s existing implicit conversion (`Utility/Utf8String.cs:48-65`), mirroring how return values are already handled correctly. That fixes the encoding for all 20 fields and makes `PBGRA` an honest pointer.

---

### F6 — MEDIUM — `SteamInputActionEvent_t` has the wrong pack and is missing its union

**Severity:** MEDIUM (unreachable today — the only API that produces it is filtered out as deprecated)

**Location:** `Facepunch.Steamworks/Generated/SteamStructs.cs:108-115`

**What's wrong.** `isteaminput.h:626` opens `#pragma pack( push, 1 )`, not closed until line 712, and `SteamInputActionEvent_t` is declared at line 689 inside it:

```c
#pragma pack( push, 1 )
…
struct SteamInputActionEvent_t
{
	InputHandle_t controllerHandle;
	ESteamInputActionEventType eEventType;
	struct AnalogAction_t {
		InputAnalogActionHandle_t actionHandle;
		InputAnalogActionData_t analogActionData;
	};
	struct DigitalAction_t { … };
	union {
		AnalogAction_t analogAction;
		DigitalAction_t digitalAction;
	};
};
```

C++ under `pack(1)`: `controllerHandle`@0 (8), `eEventType`@8 (4), union@12 (21 bytes: `uint64` + 13-byte `InputAnalogActionData_t`) → **size 33**.

The C# version uses `Pack = Platform.StructPlatformPackSize` (**8**, should be 1) and drops the union entirely — the generator explicitly comments it out at `Generator/CodeWriter/Struct.cs:149-152`:
```csharp
if ( t == "SteamInputActionEvent_t.AnalogAction_t" )
{
    Write( "// " );
}
```
Measured C# size: **16**. C++: **33**.

**Impact.** Currently none: `Cleanup.IsDeprecated` (`Generator/Cleanup.cs:172-176`) filters out `ISteamInput.EnableActionEventCallbacks`, the only entry point that delivers this struct, so nothing in the assembly consumes it. It is a compiled-in landmine rather than a live bug.

**Recommended fix.** Either give it `Pack = 1` and model the union with `LayoutKind.Explicit` (`AnalogState` and `DigitalState` already exist and are correct), or drop the type from generation entirely so it cannot be misused.

---

### F7 — LOW — `SteamIPAddress` carries `Pack = 8` where the header says `pack(1)`

**Severity:** LOW (currently harmless — verified size-equivalent)

**Location:** `Facepunch.Steamworks/Structs/SteamIpAddress.cs:9`

`steamtypes.h:113` opens `#pragma pack( push, 1 )` immediately before `struct SteamIPAddress_t` at line 115. The C# declaration is:
```csharp
[StructLayout( LayoutKind.Explicit, Pack = Platform.StructPlatformPackSize )]   // = 8 on Windows
```
Because the layout is `Explicit` and every field offset is hard-coded (`Ip4Address` @0, `Type` @16), the measured size is **20**, which equals the header-derived size of 20. So it is correct *by accident*: the explicit offsets mask the wrong `Pack`. Worth changing to `Pack = 1` so it stays correct if a field is ever added.

---

## Verified-correct areas

These were checked and are **fine**. Listing them explicitly so the surface that does *not* need attention is clear.

### Calling convention — clean
All **1018** P/Invoke declarations use `CallingConvention.Cdecl` (0 exceptions), via `Platform.CC` (`Utility/Platform.cs:24`). This matches `S_API` in `steam_api_flat.h`, which resolves to `extern "C"` + default cdecl on all three targets. Measured by reflection over `DllImportAttribute.CallingConvention`.

### `bool` marshaling — clean
- **0** of the `bool`-returning P/Invokes lack `[return: MarshalAs(UnmanagedType.I1)]`.
- **0** `bool` parameters (by value or by ref) lack `I1`.
- **0** P/Invokes set `SetLastError`.
- Inside structs, the generator emits `[MarshalAs(UnmanagedType.I1)]` for every `bool` member (`Generator/CodeWriter/Struct.cs:74-77`), confirmed by measurement (e.g. `gameserveritem_t.HadSuccessfulResponse` @12 size 1, `.DoNotRefresh` @13 size 1).

C++ `bool` is 1 byte and the default .NET marshaling of `bool` is a 4-byte `BOOL`, so this is the single most common source of ABI drift in bindings of this kind. It is handled correctly and uniformly here.

### Callback IDs — clean (219/219)
Three-way reconciliation of the header-computed `k_iSteam*Callbacks + N` values, `steam_api.json`'s `callback_id`, and the C# `enum CallbackType` (`Generated/CustomEnums.cs`): **zero mismatches**. Two header structs (`GCMessageAvailable_t` = 1701, `GCMessageFailed_t` = 1702, `isteamgamecoordinator.h:62,69`) are absent from both the JSON and the C# enum — benign, `ISteamGameCoordinator` is not a bound interface and has no `SteamAPI_ISteamGameCoordinator_*` exports in `steam_api_flat.h`.

Two genuine ID collisions exist *in the SDK itself* and are reproduced faithfully: `UserStatsUnloaded_t` and `GSStatsUnloaded_t` are both 1108 (`isteamuserstats.h:419`, `isteamgameserverstats.h:107`); the generator comments out the duplicate dictionary entry (`CustomEnums.cs:316`) and disambiguates at runtime with the `server` flag in `Dispatch.Callback`. That is the correct handling.

This matters because a wrong ID means either a callback that never fires or, worse, the wrong struct type used to read a payload. Neither is reachable.

### String marshaling in the interface layer — clean
- **No** `[DllImport]` anywhere takes a raw `System.String` or `StringBuilder` parameter, and none sets `CharSet`. Verified two ways (reflection over 1018 methods; multiline regex over all `static extern` declarations). The default ANSI marshaler is therefore never used on the interface layer — the classic UTF-8-vs-ANSI trap is avoided by construction.
- Input strings go through `Utf8StringToNative` (`Utility/Utf8String.cs:11-30`), which does an explicit UTF-8 encode into `AllocHGlobal` memory with a manual NUL. The `GetByteCount`/`GetBytes` pair share a fallback so the `len + 1` allocation can never overflow.
- Returned `const char *` is typed `Utf8StringPointer` (41 sites) with a manual scan + `Utf8NoBom.GetString` (`Utf8String.cs:48-65`). `Marshal.PtrToStringAnsi` / `PtrToStringUni` / `PtrToStringUTF8` appear **zero** times in the entire repository.
- Caller-allocated out-buffers: 38 generated sites, all allocate 32 KiB (`Helpers.MemoryBufferSize`, `Helpers.cs:10`) and all pass exactly `(1024 * 32)` as the count — 0 mismatches, including the awkward dual-buffer/shared-size and `ref`-size cases. `Helpers.MemoryToString` caps its NUL scan at the buffer length, so an unterminated native fill cannot over-read.
- `Utility.Utf8NoBom = new UTF8Encoding( false, false )` (`Utility/Utility.cs:13`) — no BOM (required; a BOM on a `const char*` would corrupt every string Steam receives) and replacement fallback rather than throwing (correct here: a throwing decoder would let malformed native data escape as an exception out of `Dispatch.Frame`).
- The `Helpers.Memory` buffer pool is a genuine pool with `lock`-protected take/return, not a shared scratch buffer — no cross-thread aliasing on the string paths.

### Framework targets — no divergence
`net46`, `netstandard2.1`, `net6.0` all build clean. There is **no** conditional compilation around string or struct marshaling anywhere in the library; the only `#if`s are `PLATFORM_WIN64/WIN32/POSIX` (`Utility/Platform.cs:13-22`), `#if DEBUG` diagnostics, `GameId` bitfield comments, and one `ReadOnlySpan<byte>` send overload. This is achieved deliberately by avoiding `Marshal.PtrToStringUTF8` (unavailable on net46) — it appears zero times. Behaviour is identical on all three TFMs.

### Struct return / by-value ABI — correct
The x64 rule is that a struct is returned in `RAX` only at size 1/2/4/8, otherwise via a hidden pointer. Five P/Invokes return a struct larger than 8 bytes:

| Method | C# size | C++ size | header |
|---|---|---|---|
| `SteamAPI_ISteamInput_GetAnalogActionData` → `AnalogState` | 13 | 13 | `isteaminput.h:628` (pack 1) |
| `SteamAPI_ISteamController_GetAnalogActionData` → `AnalogState` | 13 | 13 | same |
| `SteamAPI_ISteamInput_GetMotionData` → `MotionState` | 40 | 40 | `isteaminput.h:649` (pack 1) |
| `SteamAPI_ISteamController_GetMotionData` → `MotionState` | 40 | 40 | same |
| `SteamAPI_ISteamGameServer_GetPublicIP` → `SteamIPAddress` | 20 | 20 | `steamtypes.h:115` (pack 1) |

All sizes match, and the flat API genuinely returns them by value (`steam_api_flat.h:687,695,1183`), so declaring them as return types is right — the CLR marshaller implements the hidden-pointer convention. `AnalogState`/`DigitalState`/`MotionState` correctly carry `Pack = 1` (`Structs/Controller.cs:61,71,86`) matching `#pragma pack( push, 1 )` at `isteaminput.h:626`.

One by-value struct parameter > 8 bytes: `SteamAPI_ISteamParties_GetBeaconLocationData( …, SteamPartyBeaconLocation_t BeaconLocation, … )`, C# 16 = C++ 16. Correct.

The struct-accessor pattern in `Generated/SteamStructFunctions.cs` uses `ref self` throughout, matching the flat API's `Type* self` — correct, no by-value/by-ref confusion.

### Structs verified byte-exact
The following were computed from the headers and measured, and **agree exactly** on both size and every field offset (a representative subset of the 220 that passed):

`SteamUGCDetails_t` (9784 bytes, 27 fields — including the awkward `char[8000]` at offset 153 forcing `m_ulSteamIDOwner` to 8160), `gameserveritem_t` (372, 18 fields), `NetMsg`/`SteamNetworkingMessage_t` (216, 14 fields), `NetIdentity`/`SteamNetworkingIdentity` (136), `NetAddress`/`SteamNetworkingIPAddr` (18), `ConnectionInfo`/`SteamNetConnectionInfo_t` (696, offsets 0/136/144/148/166/168/172/176/180/184/312), `ConnectionStatus` (120), `ConnectionLaneStatus` (64), `NetKeyValue` (16), `NetPingLocation` (512), `CallbackMsg_t` (24), `LeaderboardEntry_t` (32), `P2PSessionState_t` (20), `SteamItemDetails_t` (16), `FriendGameInfo_t` (24), `SteamPartyBeaconLocation_t` (16), `servernetadr_t` (8), `SteamIPAddress_t` (20), all `HTML_*` callbacks (sizes/offsets only — see F5 for the field types), and every empty marker callback (1 byte in both).

The hand-written `Networking/*` structs deliberately expose fewer fields than the C++ definition (e.g. `ConnectionInfo` stops before `m_nFlags` and the 252-byte reserved tail) but pin the total with `Size = 696` and keep every exposed offset correct. That is a legitimate pattern, not a defect.

### Fixed-size buffer constants — correct
Every `SizeConst` matches its `#define`/array bound in the headers: `gameserveritem_t` 32/32/64/64/128, `SteamUGCDetails_t` 129/8000/1025/260/256, `JoinPartyCallback_t` 256, `GSClientDeny_t` 128, `SteamDatagramHostedAddress` 128, `SteamDatagramGameCoordinatorServerLogin` 2048, `NetIdentity` 128 (`k_cchMaxString`, `steamnetworkingtypes.h:319`), `ConnectionInfo` 128/128, `MatchMakingKeyValuePair` 256/256.

### Enum underlying types — correct
113 enums in the assembly; **112** have underlying type `System.Int32`, matching the C++ default enum type (4 bytes). The single exception is `Steamworks.Data.GameIdType : byte` (`Structs/GameId.cs:5`), which is *not* an ABI type — it is a helper for the `CGameID` bitfield accessor and is never a struct member or P/Invoke parameter. The `GameId` bitfield decomposition (`Structs/GameId.cs:33-48`) correctly reproduces MSVC/gcc little-endian bitfield allocation for `m_nAppID:24 / m_nType:8 / m_nModID:32`.

### POSIX target
Rebuilt and re-measured against `VALVE_CALLBACK_PACK_SMALL` (pack 4). Only **4** real mismatches on POSIX vs 15 on Windows — `GameConnectedChatLeave_t` (20 vs 18), `GSClientGroupStatus_t` (20 vs 18), `P2PSessionConnectFail_t` (12 vs 9), `PSNGameBootInviteResult_t` (12 vs 9, offset wrong). The Windows-only F1a–F1d defects vanished on POSIX because `Pack = 4` is the *correct* pack there. That asymmetry was itself diagnostic: it confirmed the root cause was `Pack` selection, not field ordering.

**After the fix, POSIX is clean too** — all four measure their header-derived size (18, 18, 9, 9), and `PSNGameBootInviteResult_t` puts its `CSteamID` at offset 1 as the header requires. `RequestPlayersForGameResultCallback_t`, which the automated diff cannot resolve, measures **56** on POSIX and **64** on Windows, both matching the F1a table. The header-derived diff reports 0 real mismatches on both targets.

---

## Open questions / things that need a live Steam client to settle

1. ~~**Does `ISteamUtils::GetAPICallResult` reject a short `cubCallback`?**~~ (F3) **Moot.** `UserStatsReceived_t` now measures 24, matching the header, so the value passed is correct whichever way Steam checks it. The question is still interesting for the general case but no longer gates anything here.

2. ~~**Do the F2 over-reads ever fault?**~~ **Moot.** All nine over-reads are gone; `Marshal.SizeOf` now equals the native size for every one of them, so nothing reads past the buffer to begin with.

3. **`CSteamID` alignment.** *This is the one premise the whole fix rests on, so it deserves restating.* `alignof(CSteamID) == alignof(CGameID) == 1` follows directly from `#pragma pack( push, 1 )` at `steamclientpublic.h:475`, which encloses `class CSteamID` (:480) and `class CGameID` (:922) and is not popped until :1108 — all four line numbers verified against the committed header text. It has not been *measured*, because there is no C++ compiler on this machine. A definitive check would be a 5-line C++ TU compiled with MSVC printing `sizeof`/`alignof`/`offsetof` for `AvatarImageLoaded_t` and `SubmitPlayerResultResultCallback_t`, and it is worth adding to CI. Failing that, a managed test that reads a live `AvatarImageLoaded_t` and checks `msg.DataSize == 20` would do it.

   Note the corroborating evidence: the pre-fix layouts were correct for 17 of the 40 id-bearing structs *precisely* on the arithmetic this premise predicts — the ones where a 4-byte field preceded the id, so a pack-4 `ulong` happened to land on the align-1 native offset. If `CSteamID` were 8-aligned, that pattern would be inexplicable.

4. **Win32 (x86) target.** `Facepunch.Steamworks.Win32` ships as a NuGet package and uses the same `StructPlatformPackSize = 8` / `StructPackSize = 4` constants, so the same class of defect applies. I could not measure it: no x86 .NET runtime is installed (`C:\Program Files (x86)\dotnet` absent), and pointer size (4 vs 8) changes offsets for any struct containing a `const char *`. The x64 findings should be re-run under an x86 host before assuming they transfer.

5. **`HTML_NeedsPaint_t.PBGRA` behaviour in practice.** (F5) Marshalling a BGRA framebuffer as `LPStr` is clearly wrong, but the actual allocation size depends on where the first zero byte falls in real pixel data. Needs a live HTML surface to characterise.

---

## Reproduction

All harnesses are outside the repository, in
`C:\Users\admin\AppData\Local\Temp\claude\C--Users-admin-Desktop-Claude-Cowork-Global-Facepunch-Steamworks\a40aa6e1-0fed-4b76-b07a-e6559244c52d\scratchpad`:

| Path | Purpose |
|---|---|
| `LayoutProbe/` | Reflects `Facepunch.Steamworks.Win64.dll`, emits `Marshal.SizeOf`/`OffsetOf` for 337 types → `layout.json` |
| `LayoutProbePosix/` | Same for `Facepunch.Steamworks.Posix.dll` → `layout-posix.json` |
| `cpplayout.js` | Recovers `#pragma pack` context from the headers, computes MSVC x64 layouts from `steam_api.json`, diffs against the measured JSON. `VERBOSE=1` for per-field dumps |
| `cpplayout2.js` | **Use this one for the POSIX pass.** Identical to `cpplayout.js` except that `TARGET=posix` also switches the *managed* input to `layout-posix.json`; `cpplayout.js` switches only the C++ side, so `TARGET=posix node cpplayout.js` compares POSIX C++ against the Windows assembly and reports ~88 spurious mismatches |
| `idsurvey.js` | Lists every struct containing a `CSteamID`/`CGameID` member (transitively), header-derived layout beside measured, for reviewing the F1/F2/F3 fix |
| `PinvokeProbe/` | Audits all 1018 P/Invoke signatures (calling convention, bool marshaling, string params, struct returns) |
| `CharSetProbe/` | Lists every struct with `ByValTStr` fields and its effective `CharSet` |
| `CharSetRepro/` | Round-trips real strings through the two `[StructLayout]` shapes and prints raw bytes (evidence for F4) |
| `EnumProbe/` | Checks the underlying type of all 113 enums |

```
export PATH="/c/dotnet:$PATH"; export DOTNET_ROOT="C:\\dotnet"

# Windows x64
dotnet build Facepunch.Steamworks/Facepunch.Steamworks.Win64.csproj -f net6.0 -c Release
dotnet run --project <scratchpad>/LayoutProbe/LayoutProbe.csproj -c Release -- <scratchpad>/layout.json
node <scratchpad>/cpplayout.js

# POSIX x64 - note both halves have to be switched over
dotnet build Facepunch.Steamworks/Facepunch.Steamworks.Posix.csproj -f net6.0 -c Release
dotnet run --project <scratchpad>/LayoutProbePosix/LayoutProbePosix.csproj -c Release -- <scratchpad>/layout-posix.json
TARGET=posix node <scratchpad>/cpplayout2.js
```

Expected output after the F1/F2/F3 fix: `4 MISMATCH` on both targets, all four
`FIELDCOUNT`-only on the hand-written structs listed in *Verified-correct areas*, with
`cppSize == csSize` on every one. Anything else is a regression.

None of these touch Steam.
