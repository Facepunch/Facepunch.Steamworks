# Subsystem Audit — Inventory, UGC, Cloud, Input, Friends, Matchmaking

## Summary

Reports 00–06 covered the binding *machinery*: exports, struct layout, dispatch, coverage
counts, the server path, tests, and allocation. They did not look at what the six big
gameplay subsystems actually *do* with the values Steam hands back. This pass does, and the
picture is consistent: **the P/Invoke declarations are almost all correct, and the C# wrapped
around them is where the defects are.** Of 22 findings, exactly two are marshaling bugs; the
other twenty are lifetime, validation, and error-handling failures in hand-written wrapper
code.

Three results are worth pulling out of the list.

**A remote lobby member can read this process's recycled string buffers.** `Helpers.Memory`
is a process-wide pool of four 32 KB buffers. `Take()` zeroes byte `[0]` and nothing else;
`Dispose()` returns the buffer to the pool unscrubbed. Every chat receive path asks Steam how
many bytes it wrote, ignores the answer, and then calls `Helpers.MemoryToString`, which scans
for a NUL up to the full 32768. A chat payload containing no NUL — which nothing on the wire
requires, and which this library's own `Lobby.SendChatBytes` produces — makes that scan run
off the end of the message into the previous borrower's data. **Reproduced offline against the
shipped assembly:** a 9-byte message `"hello all"` was delivered to `OnChatMessage` as
`"hello ally card is 4111 1111 1111 1111"`, with 29 characters of an unrelated caller's buffer
appended and attributed to the sender. The same pool backs friend chat, clan chat, lobby
metadata and 33 other call sites. This is a remotely triggerable information disclosure, and
it is finding **S2**.

**`GetDigitalActionOrigins` and `GetAnalogActionOrigins` bind an 8-element output array as
`ref` to a single 4-byte enum.** `isteaminput.h:822-824` says *"originsOut should point to a
`STEAM_INPUT_MAX_ORIGINS` sized array"* and annotates the parameter
`STEAM_OUT_ARRAY_COUNT( STEAM_INPUT_MAX_ORIGINS, ... )`, with `STEAM_INPUT_MAX_ORIGINS` = 8 at
`:24`. The C# supplies one stack local. Three *public* methods reach it. Native is entitled to
write 32 bytes into 4. Finding **S1**.

**No inventory result handle is ever destroyed.** `DestroyResult` is called from exactly one
place in the library — `InventoryResult.Dispose()` — and the library never calls that on any
result it creates. Eleven public entry points hand the caller an owned handle with no
documentation saying so; the `SteamInventoryFullUpdate_t` handler leaks one per inventory
update forever; and `SteamInventory.GetAllItems()` leaks one unconditionally on every call.
Finding **S4**.

Counts: **1 critical, 5 high, 9 medium, 7 low.** Nine are proven by a running harness or by an
unambiguous code path; the rest are inferences whose boundary is stated explicitly. Twenty-six
checks that came back clean are recorded in *Verified-correct*, including several plausible
hypotheses that did **not** survive — `SteamRemoteStorage.FileRead` handles short reads
correctly, `GetQuota` has no 32-bit truncation, all 36 pooled-buffer call sites declare exactly
the size they allocate, and the lobby-list filter set does not accumulate across queries.

---

## Method

Static analysis plus pure-managed measurement. **Steam was not installed and no Steam account
was used**, consistent with reports 00–06.

- Every claim about native behaviour is anchored to `Generator/steam_sdk/*.h` and the header
  is quoted inline. Where the header is silent, the report says so rather than guessing.
- Offline reproductions run against
  `Facepunch.Steamworks/bin/Release/net6.0/Facepunch.Steamworks.Win64.dll` via reflection —
  the shipped assembly, not a re-implementation. The harness lives in the session scratchpad
  and is reproduced inline where it matters.
- **PROVEN** marks a finding reproduced by that harness, or one that follows from a code path
  with no branch that could avoid it. **INFERRED** marks everything else, with the specific
  unknown named.

**A note on line numbers.** Three sessions were editing `.cs` files concurrently while this
pass ran. Line numbers are accurate as of commit `e1635e9`, but
`Facepunch.Steamworks/Structs/UgcQuery.cs` in particular had uncommitted modifications. Every
finding therefore quotes the code verbatim and names the enclosing method; if a line number has
drifted, search for the quoted text.

### Overlap with concurrent work

A documentation pass was rewriting `SteamUgc.cs`, `UgcQuery.cs` and `SteamInventory.cs` while
this audit ran, and fixed live bugs as it went (`1b3504a`). Checked against it, so the two do
not contradict each other:

- **`Query.InLanguage()` was dead code — already fixed, claim dropped.** A first-pass sweep
  flagged it; re-checking the working tree showed `SetLanguage` is now wired up. It appears in
  *Verified-correct* rather than as a finding, to record that it was examined.
- **Three fixes in `1b3504a` do not overlap this report**: `InLanguage`, the
  `result.Value.Result`-on-a-null-`CallResult` crash in the playtime-tracking methods, and the
  unterminatable `DownloadAsync` polling loop. All three are real; none is re-reported here.
- **S4 partly overlaps in-flight documentation.** That pass is adding `<remarks>` to
  `SteamInventory` stating that results own a native handle and must be disposed — which
  addresses the *documentation* half of S4. It explicitly does **not** fix the leaks: its own
  text says the `SteamInventoryFullUpdate_t` handler "does not dispose it", which is the
  behaviour S4 reports. The three leaks and the missing per-method `<remarks>` on the other ten
  entry points stand. Note also that the example it adds uses
  `using ( var inventory = result.Value )` — correct as written for a single use, but it binds a
  **copy** of the struct, so it is only safe while S13 (non-idempotent `Dispose`) is unfixed in
  the direction that matters. Fixing S13 first, as the backlog says, keeps that example valid.

Nothing in this report was written against code a concurrent session had already changed
without that being re-verified against the working tree.

---

## Findings

### S1 — `GetDigitalActionOrigins` / `GetAnalogActionOrigins` bind an 8-element out-array as a single scalar

**Severity: CRITICAL** · **INFERRED** (the size mismatch is proven; whether Steam writes more
than one origin in a given configuration is not observable offline)

**Location**
- `Facepunch.Steamworks/Generated/Interfaces/ISteamInput.cs:215` and `:218` (digital)
- `Facepunch.Steamworks/Generated/Interfaces/ISteamInput.cs:260` and `:263` (analog)
- Reached from `Facepunch.Steamworks/SteamInput.cs:66`, `:86`, `:100`
- Same defect, no managed caller: `Generated/Interfaces/ISteamController.cs:168`, `:202`

**What's wrong**

The header is explicit that the parameter is an array of eight:

```c
// isteaminput.h:24
#define STEAM_INPUT_MAX_ORIGINS 8
```
```c
// isteaminput.h:822-825
// Get the origin(s) for a digital action within an action set. Returns the number of origins supplied in originsOut. ...
// originsOut should point to a STEAM_INPUT_MAX_ORIGINS sized array of EInputActionOrigin handles. ...
virtual int GetDigitalActionOrigins( InputHandle_t inputHandle, InputActionSetHandle_t actionSetHandle, InputDigitalActionHandle_t digitalActionHandle, STEAM_OUT_ARRAY_COUNT( STEAM_INPUT_MAX_ORIGINS, Receives list of action origins ) EInputActionOrigin *originsOut ) = 0;
```

The C# supplies one:

```csharp
// ISteamInput.cs:215
private static extern int _GetDigitalActionOrigins( IntPtr self, InputHandle_t inputHandle, InputActionSetHandle_t actionSetHandle, InputDigitalActionHandle_t digitalActionHandle, ref InputActionOrigin originsOut );
```

`InputActionOrigin` is `: int` (`Generated/SteamEnums.cs:1068`), confirmed 4 bytes by the
harness. Native may write **8 × 4 = 32 bytes**; the managed side provides **4**.

All three call sites pass a bare stack local *and discard the return count* — the count is the
one value that would tell the caller how much was written:

```csharp
// SteamInput.cs:62-74
public static string GetDigitalActionGlyph( Controller controller, string action )
{
    InputActionOrigin origin = InputActionOrigin.None;

    Internal.GetDigitalActionOrigins(
        controller.Handle,
        Internal.GetCurrentActionSet(controller.Handle),
        GetDigitalActionHandle(action),
        ref origin
    );

    return Internal.GetGlyphForActionOrigin_Legacy(origin);
}
```

`GetPngActionGlyph` (`:82`) and `GetSvgActionGlyph` (`:96`) are identical. All three are
`public`.

The generator got the array-ness *right* for the two neighbouring functions with the same
shape — `GetConnectedControllers` (`ISteamInput.cs:97`) and `GetActiveActionSetLayers`
(`:181`) both use `[In,Out] T[]`. Only the two origin functions are wrong.

**Failure scenario**

A game calls `SteamInput.GetPngActionGlyph( pad, "jump", GlyphSize.Medium )` to draw a button
prompt. The player's Steam Input config binds `jump` to both the A button and a radial-menu
entry — an ordinary configuration, and the norm on Steam Deck. Steam writes two
`EInputActionOrigin` values, 8 bytes, into the 4-byte slot the marshaller pinned for `origin`.
The extra 4 bytes land on whatever the JIT placed next in that frame. There is no exception and
no diagnostic; the corruption surfaces later as a wrong value, a wrong branch, or a crash with
an unrelated stack.

**Inference boundary.** Proven: the contract permits 32 bytes and the C# supplies 4. Not
observable offline: how many bytes Steam's implementation actually writes for a given binding.
If it zero-fills the full 8-slot array before populating — a common implementation choice — the
overflow is *unconditional* rather than conditional on multi-origin bindings.

**Recommended fix**

Change both bindings to `[In,Out] InputActionOrigin[] originsOut`, matching
`GetConnectedControllers`. Add `internal const int STEAM_INPUT_MAX_ORIGINS = 8;`, have the
three call sites allocate `new InputActionOrigin[STEAM_INPUT_MAX_ORIGINS]`, and use the
returned count to select an origin instead of assuming index 0. Fix
`ISteamController.cs:168`/`:202` at the same time — they are unreachable today but are the same
landmine. This is a generator fix, not a hand-edit of the generated file.

---

### S2 — Chat receive paths disclose recycled buffer contents to a remote player

**Severity: HIGH** · **PROVEN** (reproduced against the shipped assembly)

**Location**
- `Facepunch.Steamworks/SteamMatchmaking.cs:74-86` — `OnLobbyChatMessageRecievedAPI`
- `Facepunch.Steamworks/SteamFriends.cs:97-115` — `OnFriendChatMessage`
- `Facepunch.Steamworks/SteamFriends.cs:117-136` — `OnGameConnectedClanChatMessage`
- Root cause: `Facepunch.Steamworks/Utility/Helpers.cs:28`, `:35-50`, `:87-101`

**What's wrong**

Three independent problems compose into one exploitable defect.

*One — the pool is never scrubbed.* `Helpers.Memory.Take()` clears exactly one byte:

```csharp
// Helpers.cs:21-33
internal static unsafe Memory Take()
{
    IntPtr ptr;
    lock (BufferBag)
    {
        ptr = BufferBag.Count > 0 ? BufferBag.Dequeue() : Marshal.AllocHGlobal(MemoryBufferSize);
    }
    ((byte*)ptr)[0] = 0;
    return new Memory { Ptr = ptr };
}
```

`Dispose()` (`Helpers.cs:35-50`) enqueues the pointer back with no clear. The remaining 32767
bytes still hold the previous borrower's data.

*Two — the decode ignores the length and trusts a terminator.*

```csharp
// Helpers.cs:87-101
internal unsafe static string MemoryToString( IntPtr ptr )
{
    var len = 0;
    for( len = 0; len < MemoryBufferSize; len++ )
    {
        if ( ((byte*)ptr)[len] == 0 )
            break;
    }
    ...
    return Utility.Utf8NoBom.GetString( (byte*)ptr, len );
}
```

*Three — the caller has the true length and throws it away.*

```csharp
// SteamMatchmaking.cs:74-86
var readData = Internal.GetLobbyChatEntry( callback.SteamIDLobby, (int)callback.ChatID, ref steamid, buffer, Helpers.MemoryBufferSize, ref chatEntryType );

if ( readData > 0 )
{
    OnChatMessage?.Invoke( new Lobby( callback.SteamIDLobby ), new Friend( steamid ), Helpers.MemoryToString( buffer ) );
}
```

`readData` is the header's documented byte count —
`isteammatchmaking.h:205`: *"return value is the number of bytes written into the buffer"* — and
it is compared against zero and then never used again.

Nothing requires the sender to include a NUL. The header makes it the sender's choice
(`isteammatchmaking.h:200`: *"if pvMsgBody is text, cubMsgBody should be strlen( text ) + 1, to
include the null terminator"*). This library's own public API produces unterminated payloads:

```csharp
// Lobby.cs:137-142
public bool SendChatString( string message )
{
    //adding null terminator as it's used in Helpers.MemoryToString
    var data = Utility.Utf8NoBom.GetBytes( message + '\0' );
    return SendChatBytes( data );
}
```

That comment is the tell — the author knew the decode depends on a terminator and patched the
*send* side, which only works when every peer runs this library. `SendChatBytes` and
`SendChatBytesUnsafe` (`Lobby.cs:147-161`) are public and add nothing.

**Evidence — reproduced offline**

Driving the shipped `Helpers` through reflection: borrower #1 writes a string and disposes;
borrower #2 (the chat path) receives the same pooled buffer, writes 9 unterminated bytes, and
calls `MemoryToString`.

```
=== T0  pooled-buffer residue disclosure via Helpers.MemoryToString ===
  Helpers.MemoryBufferSize = 32768
  borrower #1 wrote  : "gg wp - my card is 4111 1111 1111 1111"  at 1563951820928
  borrower #1 disposed (buffer goes back to the bag, NOT scrubbed)
  borrower #2 got    : 1563951820928   <-- SAME BUFFER
  GetLobbyChatEntry wrote 9 bytes, returned 9, no NUL

    what the sender sent      : "hello all"
    what OnChatMessage gets   : "hello ally card is 4111 1111 1111 1111"

    ==> CONFIRMED. 29 extra chars of another caller's buffer were appended
        and attributed to the sender.

  Take() zeroes byte [0] only - proof:
    byte[0] = 0    byte[1] = 101    byte[2] = 108    byte[3] = 108
```

**Failure scenario**

A player joins a public lobby with a client that does not NUL-terminate — a modified client, a
different Steamworks wrapper, or any game using this library's own `SendChatBytes`. He sends a
short message. The pool has four buffers shared process-wide across 36 `TakeMemory()` call
sites, including friend chat, clan chat, lobby metadata (`ISteamMatchmaking.cs:277-278`), and
inventory item properties. His message is delivered to every other player in the lobby with the
tail of somebody's private friend DM, or a previous lobby's metadata, concatenated onto it —
displayed in the chat UI, attributed to him. Repeating this walks the pool.

**Scope, stated precisely.** The scan bound `len < MemoryBufferSize` exactly equals the
`AllocHGlobal(MemoryBufferSize)` size, so this is **not** a heap over-read. It is disclosure of
residue within the library's own buffer pool, plus message-integrity loss. Nothing outside the
pool is reachable.

**Recommended fix**

Bound the decode by the length already in hand. Add
`Helpers.MemoryToString( IntPtr ptr, int maxLength )` that clamps the scan to
`Math.Min( maxLength, MemoryBufferSize )`, and pass `readData` / `len` at all three call sites.
Zeroing the whole buffer in `Take()` would also close it but costs a 32 KB `memset` per call on
a path report 06 worked to keep allocation-free — the clamp is both cheaper and more correct.
Separately, `SendChatBytes` should document that it does not terminate. Note the two
`SteamFriends` sites also have an `&&` bail-out (`if ( len == 0 && type == ChatEntryType.Invalid )`)
that lets `len == 0` through with a valid type.

---

### S3 — UGC query handles leak on every failure path, and on every caller that forgets to dispose

**Severity: HIGH** · **PROVEN** (code path)

**Location**
- `Facepunch.Steamworks/Structs/UgcQuery.cs` — `Query.GetPageAsync` (≈:569-619 at `e1635e9`)
- `Facepunch.Steamworks/Structs/UgcResultPage.cs:125-132` — the only release site
- `Facepunch.Steamworks/SteamUgc.cs:139-141` — `QueryFileAsync`

**What's wrong**

`ReleaseQueryUGCRequest` is called from exactly one place in the library:

```csharp
// UgcResultPage.cs:125-132
public void Dispose()
{
    if ( Handle > 0 )
    {
        SteamUGC.Internal.ReleaseQueryUGCRequest( Handle );
        Handle = 0;
    }
}
```

`GetPageAsync` creates the handle and has two early returns that skip it, with no `try/finally`
anywhere between creation and the `ResultPage` construction:

```csharp
handle = SteamUGC.Internal.CreateQueryAllUGCRequest( queryType, matchingType, creatorApp.Value, consumerApp.Value, (uint)page );
...
var result = await SteamUGC.Internal.SendQueryUGCRequest( handle );
if ( !result.HasValue )
    return null;

if ( result.Value.Result != Steamworks.Result.OK )
    return null;
```

The second is the damaging one: Steam accepted the request and allocated the query, then
returned a non-OK `EResult`. The handle is dropped with no reference left anywhere. Any
exception thrown by `ApplyReturns`, `SetAllowCachedResponse`, `ApplyConstraints`, or the `await`
leaks it the same way.

`QueryFileAsync` leaks on its own success-ish path:

```csharp
// SteamUgc.cs:139-145
if ( !result.HasValue || result.Value.ResultCount != 1 )
    return null;

var item = result.Value.Entries.First();

result.Value.Dispose();
```

When the query succeeds but returns any count other than 1 — zero results for a deleted item,
or more than one — the handle is leaked. And `result` is a `ResultPage?`, so `result.Value`
returns a **copy**: `ReleaseQueryUGCRequest` does fire, but the `Handle = 0` write lands on a
compiler temporary and is discarded, leaving the original's `Handle` non-zero and armed for a
double release. `UgcItem.cs:240` (`using ( file.Value )`) has the same shape.

`ResultPage` is a `struct`, so there is no finalizer to fall back on. Disposal is entirely
opt-in and undocumented — **the repository's own tests never dispose**:
`Facepunch.Steamworks.Test/UgcQuery.cs` lines 21, 36, 57, 75, 93 all do
`var result = await q.GetPageAsync( 1 );` and stop there. Five leaked handles in the test suite.

Related: `Dispose()`'s guard is `Handle > 0`, but the SDK's invalid sentinel is
`k_UGCQueryHandleInvalid = 0xffffffffffffffff` (`isteamugc.h:30`,
`Generated/SteamConstants.cs:57`), which *is* `> 0`. `GetPageAsync` never checks the handle
against that sentinel after creation either.

**Failure scenario**

A mod browser pages through the Workshop. Steam rate-limits or transiently fails one request in
twenty and returns a non-OK result. Each failure strands a query allocation inside the Steam
client for the lifetime of the process. A long browsing session, or a background poller
refreshing subscribed items, accumulates them steadily; the game's own heap looks fine, so the
growth is invisible from the managed side.

**Recommended fix**

Wrap the body of `GetPageAsync` from handle creation onward in `try/finally`, releasing the
handle on every path that does not transfer ownership into the returned `ResultPage`. Fix
`QueryFileAsync` to dispose before its `ResultCount != 1` return. Make `ResultPage` a `class`,
or have `Dispose` be genuinely idempotent, so the `Nullable<T>.Value` copy problem cannot cause
a double release. Compare `Handle` against `Defines.k_UGCQueryHandleInvalid` rather than `> 0`.
Add disposal to the five test call sites.

---

### S4 — Inventory result handles are never destroyed anywhere in the library

**Severity: HIGH** · **PROVEN** (code path; entry-point count measured)

**Location**
- `Facepunch.Steamworks/Structs/InventoryResult.cs:97-102` — the only `DestroyResult` call
- `Facepunch.Steamworks/SteamInventory.cs:39-45` — `InventoryUpdated`
- `Facepunch.Steamworks/SteamInventory.cs:181-185` — `GetAllItems`
- `Facepunch.Steamworks/Structs/InventoryResult.cs:104-117` — `GetAsync`

**What's wrong**

The header is unambiguous about ownership:

```c
// isteaminventory.h:130-132
// Captures the entire state of the current user's Steam inventory.
// You must call DestroyResult on this handle when you are done with it.
```
```c
// isteaminventory.h:123-124
// Destroys a result handle and frees all associated memory.
virtual void DestroyResult( SteamInventoryResult_t resultHandle ) = 0;
```

A repository-wide search finds `DestroyResult` in exactly one place — `InventoryResult.Dispose()` —
and the library never calls `Dispose()` on any result it creates. Three distinct leaks follow.

*The callback handler leaks one per update, forever:*

```csharp
// SteamInventory.cs:39-45
private static void InventoryUpdated( SteamInventoryFullUpdate_t x )
{
    var r = new InventoryResult( x.Handle, false );
    Items = r.GetItems( false );

    OnInventoryUpdated?.Invoke( r );
}
```

*The public synchronous API leaks one on every call:*

```csharp
// SteamInventory.cs:181-185
public static bool GetAllItems()
{
    var sresult = Defines.k_SteamInventoryResultInvalid;
    return Internal.GetAllItems( ref sresult );
}
```

`sresult` is an owned handle, written by Steam and then discarded.

*The async helper leaks one per failed operation:*

```csharp
// InventoryResult.cs:104-117
internal static async Task<InventoryResult?> GetAsync( SteamInventoryResult_t sresult )
{
    var _result = Result.Pending;
    while ( _result == Result.Pending )
    {
        _result = SteamInventory.Internal.GetResultStatus( sresult );
        await Task.Delay( 10 );
    }

    if ( _result != Result.OK && _result != Result.Expired )
        return null;
    ...
}
```

`ServiceUnavailable`, `LimitExceeded`, `InvalidParam` and `Fail` are all documented outcomes
(`isteaminventory.h:85-88`) and all leak.

Beyond the leaks, **ownership is undocumented**. The harness enumerated the public surface:

```
public entry points handing an owned result handle to the caller: 11
    InventoryItem.AddAsync          SteamInventory.CraftItemAsync (x2)
    InventoryItem.ConsumeAsync      SteamInventory.DeserializeAsync
    InventoryItem.SplitStackAsync   SteamInventory.GenerateItemAsync
    SteamInventory.AddPromoItemAsync SteamInventory.GetAllItemsAsync
    SteamInventory.GrantPromoItemsAsync SteamInventory.TriggerItemDropAsync
```

At the time of the audit none of their XML docs mentioned disposal; a concurrent documentation
pass is now adding that to the `SteamInventory` class `<remarks>` (see *Overlap with concurrent
work*), but not to the individual methods, and it does not change the leaks above.
`InventoryResult` is a `struct`, so no finalizer can rescue a caller who does not know. Report
05 measured `<param>` documentation at 11.8%; this is what that costs in practice.

There is also an aliasing hazard: `GetAllItemsAsync` returns a result wrapping handle *H*, and
the `SteamInventoryFullUpdate_t` callback then constructs a second `InventoryResult` around the
same *H*. Whichever is disposed first invalidates the other.

**Failure scenario**

A game calls `GetAllItemsAsync()` on every inventory screen open and reads `.GetItems()`.
Steam allocates a result set per call — item list, per-item property blobs — and frees none of
it. Over a play session the Steam client's memory for that game grows without bound, and
nothing in the game's own profiler shows it, because the allocation is on the far side of the
API boundary.

**Recommended fix**

Dispose in `InventoryUpdated` after `OnInventoryUpdated` returns (and document that the handler
must not retain the result), or clone the items and dispose immediately. Destroy the handle in
`GetAsync` before every `return null`. Either destroy the handle in the synchronous
`GetAllItems()` or remove the method in favour of `GetAllItemsAsync`. Add `<remarks>` to all 11
entry points stating that the caller owns the result and must dispose it, and add a
`using`-based example.

---

### S5 — `Editor.SubmitAsync` reports success when the update never started

**Severity: HIGH** · **PROVEN** (code path)

**Location** `Facepunch.Steamworks/Structs/UgcEditor.cs:170-199`, reset at `:245`

**What's wrong**

On the create-new path `result.Result` is set to `OK` after `CreateItem` succeeds, and the
pessimistic reset to `Fail` sits *after* the early return that fires when `StartItemUpdate`
fails:

```csharp
// UgcEditor.cs:170-199
if ( creatingNew )
{
    result.Result = Steamworks.Result.Fail;

    var created = await SteamUGC.Internal.CreateItem( consumerAppId, creatingType );
    if ( !created.HasValue ) return result;

    result.Result = created.Value.Result;          // <-- now OK

    if ( result.Result != Steamworks.Result.OK )
        return result;
    ...
}

result.FileId = fileId;

{
    var handle = SteamUGC.Internal.StartItemUpdate( consumerAppId, fileId );
    if ( handle == 0xffffffffffffffff )
        return result;                              // <-- returns with Result.OK
```

`result.Result = Steamworks.Result.Fail;` at `:245` — the line that would have covered this —
is unreachable from that return. `PublishResult.Success` therefore reads `true`.

The non-create path escapes only by accident: `result` is `default(PublishResult)`, so
`result.Result == Result.None == 0` and `Success` is false.

**Failure scenario**

A player publishes a map. `CreateItem` succeeds and Steam allocates a published-file id.
`StartItemUpdate` then fails — wrong app id, or the user is not permitted to update that item.
`SubmitAsync` returns `Success == true` with a valid `FileId`. The game shows "Published!" and
links the player to a Workshop page that exists but has no title, no description, no content,
no tags and no preview image. The player's actual map was never uploaded, and the game has no
way to tell.

**Recommended fix**

Move the `result.Result = Steamworks.Result.Fail;` reset to immediately before
`StartItemUpdate`, or set `result.Result = Result.Fail` explicitly in the invalid-handle branch.
Compare against `Defines.k_UGCUpdateHandleInvalid` rather than the literal `0xffffffffffffffff`.
Consider wrapping everything after `StartItemUpdate` in `try/finally` — every exception between
there and `SubmitItemUpdate` currently abandons a started-but-uncommitted update.

---

### S6 — SteamInput caches invalid handles permanently, is not thread-safe, and is never invalidated

**Severity: HIGH** · **PROVEN** (cache shape measured; code path for the `0` caching)

**Location** `Facepunch.Steamworks/SteamInput.cs:105-136`

**What's wrong**

Three static dictionaries memoise action handles by name:

```csharp
// SteamInput.cs:105-114
internal static Dictionary<string, InputDigitalActionHandle_t> DigitalHandles = new Dictionary<string, InputDigitalActionHandle_t>();
internal static InputDigitalActionHandle_t GetDigitalActionHandle( string name )
{
    if ( DigitalHandles.TryGetValue( name, out var val ) )
        return val;

    val = Internal.GetDigitalActionHandle( name );
    DigitalHandles.Add( name, val );
    return val;
}
```

`AnalogHandles` (`:116`) and `ActionSets` (`:127`) are identical.

*Invalid handles are cached.* There is no check on `val` between the native call and the
`.Add`. Steam returns `0` when the action manifest has not loaded yet — routine at startup,
before the config-loaded callback fires — or when the name is misspelled. That `0` is memoised
and returned forever; the action is dead for the process lifetime and Steam is never re-asked.

*Never invalidated.* The harness confirms the only three collections and that none is
concurrent:

```
=== T7  SteamInput static handle caches ===
  DigitalHandles             Dictionary<String,InputDigitalActionHandle_t>
  AnalogHandles              Dictionary<String,InputAnalogActionHandle_t>
  ActionSets                 Dictionary<String,InputActionSetHandle_t>
  any System.Collections.Concurrent field: False
```

No `Clear()` exists anywhere. `SteamInputConfigurationLoaded_t` is defined
(`Generated/SteamCallbacks.cs:1944`) and the header documents it firing on config load
(`isteaminput.h:761-763`), but **nothing subscribes**. `SteamInputDeviceConnected_t` /
`Disconnected_t` likewise. `SteamInput` does not override `DestroyInterface`, and the base
implementation (`Utility/SteamInterface.cs:117-120`) only nulls the interface pointer — so after
`SteamClient.Shutdown()` the dictionaries survive, and a subsequent `Init` hands out handles
from the dead session.

*Not thread-safe.* Plain `Dictionary`, static, mutated by `.Add` with no lock. The public
callers are `Controller.GetDigitalState` / `GetAnalogState` (`Structs/Controller.cs:39`, `:47`) —
ordinary methods a game may call from a job thread. Concurrent `TryGetValue` during a resize can
return a wrong value or spin; concurrent `Add` of the same key throws `ArgumentException`,
because the code uses `.Add` rather than the idempotent indexer.

**Failure scenario**

A game queries `"jump"` during its first frame, before Steam has parsed the action manifest.
Steam returns `0`; the cache stores it. The manifest loads a moment later and
`SteamInputConfigurationLoaded_t` fires into a void. `"jump"` returns `0` for the rest of the
session — no exception, no log, the button simply never responds. Restarting the game usually
"fixes" it, which makes the bug read as flaky hardware.

**Recommended fix**

Guard the insert: only cache when the handle is non-zero, so lookups self-heal. Switch the
indexer for `.Add` to remove the duplicate-key throw. Subscribe to
`SteamInputConfigurationLoaded_t` and clear all three caches from it. Override
`DestroyInterface` to clear them on shutdown. Use `ConcurrentDictionary`, or document that the
input API is main-thread-only — report 02 established the threading contract this should align
with.

---

### S7 — `JoinLobbyAsync` ignores the join response; failed joins look successful

**Severity: MEDIUM** · **PROVEN** (code path)

**Location** `Facepunch.Steamworks/SteamMatchmaking.cs:164-170` and `:37`

**What's wrong**

```csharp
public static async Task<Lobby?> JoinLobbyAsync( SteamId lobbyId )
{
    var lobby = await Internal.JoinLobby( lobbyId );
    if ( !lobby.HasValue ) return null;

    return new Lobby { Id = lobby.Value.SteamIDLobby };
}
```

`LobbyEnter_t` carries `EChatRoomEnterResponse` (`Generated/SteamCallbacks.cs:682-688`), which
is the actual outcome. It is never read, so a lobby that is full, locked, banned or rate-limited
returns a non-null `Lobby` — indistinguishable from success.

The same gap is in the event: `Dispatch.Install<LobbyEnter_t>( x => OnLobbyEntered?.Invoke( new Lobby( x.SteamIDLobby ) ) );`
(`:37`) fires on failed joins and drops the response code, `Locked`, and `GfChatPermissions`.

The struct-level API gets this right, which makes the divergence sharper —
`Structs/Lobby.cs:25-31` returns `(RoomEnter) result.Value.EChatRoomEnterResponse`. Neighbouring
handlers get it right too: `LobbyCreated_t` forwards its result (`:39`) and `LobbyDataUpdate_t`
gates on `if ( x.Success == 0 ) return;` (`:45`).

**Failure scenario** A player accepts an invite to a lobby that filled up in transit.
`JoinLobbyAsync` returns a `Lobby`; the game transitions to the lobby screen and starts polling
members, getting zero. The player sits in an empty room with no error.

**Recommended fix** Return `null` (or expose the `RoomEnter` code) when
`EChatRoomEnterResponse != Success`. Change `OnLobbyEntered` to carry the response code, as
`OnLobbyCreated` already carries its `Result`.

---

### S8 — `GetUGCDetails` binds a `char **` out-parameter as `[In,Out] ref char[]`

**Severity: MEDIUM** · **PROVEN** (signature mismatch); unreachable today

**Location** `Facepunch.Steamworks/Generated/Interfaces/ISteamRemoteStorage.cs:340`, `:343`

**What's wrong**

```c
// isteamremotestorage.h:250
virtual bool	GetUGCDetails( UGCHandle_t hContent, AppId_t *pnAppID, STEAM_OUT_STRING() char **ppchName, int32 *pnFileSizeInBytes, STEAM_OUT_STRUCT() CSteamID *pSteamIDOwner ) = 0;
```
```csharp
// ISteamRemoteStorage.cs:340
private static extern bool _GetUGCDetails( IntPtr self, UGCHandle_t hContent, ref AppId pnAppID, [In,Out] ref char[]  ppchName, ref int pnFileSizeInBytes, ref SteamId pSteamIDOwner );
```

Native wants a slot to write a `char*` into. The C# declares a *reference to a managed UTF-16
`char[]`*; the marshaller will pin the array and pass a pointer to its data. `[In,Out]` combined
with `ref` on an array is itself a red flag. Every other string-out in this file uses
`Utf8StringPointer` — e.g. `:259`, `:407`.

The function has no managed caller, which is presumably why it has gone unnoticed. It is
`internal`, so it is not reachable from the public API today.

**Failure scenario** A future wrapper calls it and Steam writes a pointer into the first 8
bytes of a managed `char[]`, or into pinned memory the runtime believes holds characters. Silent
garbage at best.

**Recommended fix** Emit `out Utf8StringPointer ppchName`, matching the sibling bindings. This
is a generator fix. Report 03 already recommends leaving the legacy UGC surface unwrapped; if
that stands, deleting the binding is equally acceptable — but it should not stay in its current
form.

---

### S9 — `InventoryDef.Properties` throws once any property has been read

**Severity: MEDIUM** · **PROVEN** (reproduced)

**Location** `Facepunch.Steamworks/Structs/InventoryDef.cs:96-113` and `:151-163`

**What's wrong**

`Properties` enumerates by asking for the property-name list, which the header specifies as a
`NULL` name:

```c
// isteaminventory.h:102-103
// Pass a NULL pointer for pchPropertyName to get a comma - separated list of available
// property names.
```
```csharp
// InventoryDef.cs:151-163
public IEnumerable<KeyValuePair<string, string>> Properties
{
    get
    {
        var list = GetProperty( null );
        var keys = list.Split( ',' );
        ...
```

But `GetProperty`'s first statement feeds that `null` straight into a `Dictionary` lookup:

```csharp
// InventoryDef.cs:96-101
public string GetProperty( string name )
{
    if ( _properties!= null && _properties.TryGetValue( name, out string val ) )
        return val;
    ...
```

`Dictionary<string,string>.TryGetValue(null)` throws `ArgumentNullException`. The guard
`_properties != null` means the cold path survives, so the bug appears only *after* any single
property has been cached — which every one of `Name`, `Description`, `IconUrl`, `Type`,
`Marketable` and `Tradable` does.

**Evidence — reproduced**

```
=== T1  InventoryDef.GetProperty(null) once any property has been cached ===
  resolved String GetProperty(string)
  _properties seeded with 1 entry (what one earlier GetProperty("name") leaves behind)
  GetProperty(null) -> THREW System.ArgumentNullException
                       Value cannot be null. (Parameter 'key')
    ==> CONFIRMED: InventoryDef.Properties is unreachable once anything is cached.
  control, _properties == null -> NullReferenceException   (only 'Steam is not running')
```

A second bug sits on the same path: `var keys = list.Split( ',' )` will `NullReferenceException`
if `GetProperty` returns `null`, which it does whenever the native call fails
(`InventoryDef.cs:101-102`).

**Failure scenario** An item-inspection UI shows the item's name, then iterates `Properties` to
render the rest. Reading the name populates `_properties`; the iteration then throws
`ArgumentNullException` from inside a property getter. Reordering the two makes it "work",
which sends the developer hunting for a race that is not there.

**Recommended fix** Skip the cache lookup when `name == null`, and null-guard `list` before
`Split`. Also consider caching the name list itself under a sentinel key.

---

### S10 — Two more `InventoryDef` defects: null-deref on failed reads, and a copy-paste in the price formatter

**Severity: MEDIUM** · **PROVEN** (code path)

**Location** `Facepunch.Steamworks/Structs/InventoryDef.cs:118-126` and `:202`

**What's wrong**

```csharp
// InventoryDef.cs:118-126
public bool GetBoolProperty( string name )
{
    string val = GetProperty( name );

    if ( val.Length == 0 ) return false;
```

`GetProperty` returns `null` on a failed native call (`:101-102`). `val.Length` then throws
`NullReferenceException`. Both `Marketable` (`:76`) and `Tradable` (`:81`) route through here,
and both are the kind of property a store or trading UI reads for every item in a list.

Separately:

```csharp
// InventoryDef.cs:202
public string LocalBasePriceFormatted => Utility.FormatPrice( SteamInventory.Currency, LocalPrice / 100.0 );
```

It formats `LocalPrice`, not `LocalBasePrice` (`:188`). The "was £9.99, now £4.99" strikethrough
renders identical to the current price, so a discount silently displays as no discount.
`LocalPriceFormatted` at `:182` is correct.

**Recommended fix** `if ( string.IsNullOrEmpty( val ) ) return false;`, and change `:202` to use
`LocalBasePrice`. Both are one-liners. Note that `LocalPrice` and `LocalBasePrice` each make a
separate `GetItemPrice` call and discard the other half of the pair — worth folding together
while in there.

---

### S11 — Workshop tags marshal as ANSI, not UTF-8

**Severity: MEDIUM** · **PROVEN** (reproduced)

**Location** `Facepunch.Steamworks/Structs/SteamParamStringArray.cs:19-22`, used at
`Facepunch.Steamworks/Structs/UgcEditor.cs:210`

**What's wrong**

```csharp
for ( int i = 0; i < a.NativeStrings.Length; i++ )
{
    a.NativeStrings[i] = Marshal.StringToHGlobalAnsi( array[i] );
}
```

Every other string in the binding goes through `Utf8StringToNative`
(`Utility/Utf8String.cs`). This is the tag path for `Editor.WithTag`.

This is the **same class of defect as report 01's F4/F5**, but a *distinct site* — F4 covers
`MatchMakingKeyValuePair` (server-browser filters) and F5 covers 20 `const char*` callback
fields. `SteamParamStringArray` appears in neither; a search of `docs/audit/*.md` returns no
mention of it. It is the second of the only two `StringToHGlobalAnsi` / `CharSet.Ansi` sites in
the assembly, and the other one is F4.

**Evidence — reproduced**

```
=== T8  workshop tag marshaling: ANSI vs UTF-8 ===
  "café"         ANSI[4] 63 61 66 E9                UTF8[5] 63 61 66 C3 A9             DIFFERENT
  "日本語"        ANSI[3] 3F 3F 3F                   UTF8[9] E6 97 A5 E6 9C AC E8 AA 9E DIFFERENT
  "Ürünler"      ANSI[7] DC 72 FC 6E 6C 65 72       UTF8[9] C3 9C 72 C3 BC 6E 6C 65 72 DIFFERENT
  "plain-ascii"  ANSI[11] 70 6C 61 69 6E ...        UTF8[11] 70 6C 61 69 6E ...        identical
```

`"日本語"` becomes three literal `?` — unrecoverable. And because ANSI means the process ACP, the
bytes also change with the machine's locale, so the same tag uploads differently from different
developers' machines.

**Failure scenario** A Japanese-language game publishes Workshop items with localised category
tags. Every tag uploads as `???`, they all collide, and Workshop search for them returns
nothing. The item is already published by the time anyone notices.

**Recommended fix** Use `Utf8StringToNative` in `SteamParamStringArray.From`. Fix alongside
report 01 F4 — one commit, two sites, same root cause. Also null-guard `Dispose()`
(`SteamParamStringArray.cs:37-43` throws `NullReferenceException` on a default-constructed
instance) and `From(null)`.

---

### S12 — Avatar and persona loading poll forever with no timeout

**Severity: MEDIUM** · **PROVEN** (code path)

**Location** `Facepunch.Steamworks/SteamFriends.cs:277-294` and `:323-337`

**What's wrong**

```csharp
// SteamFriends.cs:323-337
public static async Task<Data.Image?> GetLargeAvatarAsync( SteamId steamid )
{
    await CacheUserInformationAsync( steamid, false );

    var imageid = Internal.GetLargeFriendAvatar( steamid );

    // Wait for the image to download
    while ( imageid == -1 )
    {
        await Task.Delay( 50 );
        imageid = Internal.GetLargeFriendAvatar( steamid );
    }

    return SteamUtils.GetImage( imageid );
}
```

No timeout, no `CancellationToken`, no iteration cap. `CacheUserInformationAsync` (`:277-294`)
is the same shape, plus a hard-coded `await Task.Delay( 500 )` with the comment *"And extra wait
here seems to solve avatars loading as [?]"* — a symptom being papered over.

The header names the mechanism the wrapper is not using:

```c
// isteamfriends.h:333
// returns -1 if this image has yet to be loaded, in this case wait for a AvatarImageLoaded_t callback and then call this again
```

`AvatarImageLoaded_t` is defined (`Generated/SteamCallbacks.cs:280-291`) and registered in the
type map (`Generated/CustomEnums.cs:264`) but **nothing installs a handler**, so the wrapper
polls instead. Note this means there is no callback subscription to leak — the scope question
resolves the other way.

The correctly-shaped version of this pattern is in the same codebase:
`Structs/Achievement.cs:83` takes `int timeout = 5000` and unsubscribes in a `finally` at
`:116-118`.

**Failure scenario** A friends list renders 200 avatars. A few Steam IDs never resolve — a
deleted account, a network hiccup, a user with no avatar. Those `Task`s never complete. If the
UI awaits them all before drawing, the list never appears; if it awaits individually, a handful
of tasks and their captured state stay alive for the session.

**Recommended fix** Add `int timeout = 5000` and a `CancellationToken` to
`GetSmall/Medium/LargeAvatarAsync` and `CacheUserInformationAsync`, returning `null` on
expiry — matching `Achievement`. Better: install `AvatarImageLoaded_t` and complete a
`TaskCompletionSource` from it, which is what the header describes and removes the 500 ms sleep.

---

### S13 — `InventoryResult.Dispose()` is not idempotent

**Severity: MEDIUM** · **PROVEN** (reproduced)

**Location** `Facepunch.Steamworks/Structs/InventoryResult.cs:97-102`

**What's wrong**

```csharp
public void Dispose()
{
    if ( _id.Value == -1 ) return;

    SteamInventory.Internal.DestroyResult( _id );
}
```

The guard checks for the invalid sentinel but the method never *sets* it, so the guard is never
armed. `InventoryResult` is a `struct`, so every copy carries the same live `_id` and any copy
can free it.

**Evidence — reproduced**

```
=== T2  InventoryResult.Dispose() idempotency ===
  _id.Value = 7 (a live-looking handle)
  Dispose() #1 -> threw NullReferenceException
  Dispose() #2 -> threw NullReferenceException
  control, _id.Value = -1 -> returned normally   (so the -1 guard IS the only gate, and it is never armed)
    ==> CONFIRMED: call #2 reached the DestroyResult line again.
```

Both calls reach the `DestroyResult` line; the `-1` control shows the guard *would* work if it
were ever set. (The `NullReferenceException` is just `SteamInventory.Internal` being null with
no Steam client — it is the marker proving the line was reached.)

A second issue on the same two lines: `SteamInventory.Internal` is `Interface as ISteamInventory`,
so disposing after `SteamClient.Shutdown()` throws `NullReferenceException` out of a `Dispose`.

**Failure scenario** Once S4 is fixed and callers start disposing, `using` on a result that was
also handed to `OnInventoryUpdated` double-frees the handle inside the Steam client.

**Recommended fix** Set `_id = Defines.k_SteamInventoryResultInvalid;` after the destroy, and
null-check `SteamInventory.Internal`. Fix this *before* S4, so the disposal S4 introduces is
safe.

---

### S14 — Lobby data limits are checked in the wrong unit, against the wrong parameter, and not at all for member data

**Severity: MEDIUM** · **PROVEN** (code path)

**Location** `Facepunch.Steamworks/Structs/Lobby.cs:83-89` and `:129-132`

**What's wrong**

```csharp
public bool SetData( string key, string value )
{
    if ( key.Length > 255 ) throw new System.ArgumentException( "Key should be < 255 chars", nameof( key ) );
    if ( value.Length > 8192 ) throw new System.ArgumentException( "Value should be < 8192 chars", nameof( key ) );

    return SteamMatchmaking.Internal.SetLobbyData( Id, key, value );
}
```

Three problems in two lines:

1. **Wrong unit.** `k_nMaxLobbyKeyLength 255` (`isteammatchmaking.h:51`) counts native `char`
   bytes, and the 8192 comes from `k_cubChatMetadataMax` (`isteamfriends.h:135`) where `cub` is
   Valve's prefix for *count of unsigned bytes*. `string.Length` counts UTF-16 code units, and
   `Utf8StringToNative` encodes to UTF-8. A 200-character CJK key is 600 bytes and passes; a
   5000-character emoji value is 20000 bytes and passes.
2. **Wrong `nameof`.** The value check reports `nameof( key )`.
3. **Message contradicts the guard** — `> 255` rejected, message says `< 255`.

`SetMemberData` has no validation at all:

```csharp
public void SetMemberData( string key, string value )
{
    SteamMatchmaking.Internal.SetLobbyMemberData( Id, key, value );
}
```

The literal `255` is also not tied to the named constant that already exists —
`SteamMatchmaking.cs:28-30` declares `internal static int MaxLobbyKeyLength => 255;`.

**Failure scenario** A game keyed by localised map names sets lobby data with a 100-character
Japanese value. The C# check passes at 100; Steam sees 300 bytes. If Steam truncates, the lobby
browser silently shows a corrupt name; if it rejects, `SetData` returns `false` and the
`return` value is one the caller has no reason to expect can fail.

**Recommended fix** Validate `Utility.Utf8NoBom.GetByteCount( key )` / `( value )` against the
constants, use `nameof( value )` on the second check, fix the message text, route both through
`SteamMatchmaking.MaxLobbyKeyLength`, and apply the same validation to `SetMemberData`.

---

### S15 — Allocation sizes taken directly from Steam-returned counts, unvalidated

**Severity: MEDIUM** · **INFERRED** (requires the Steam client to return a pathological count)

**Location**
- `Facepunch.Steamworks/Structs/UgcResultPage.cs:78-86` — `numChildren`
- `Facepunch.Steamworks/Structs/UgcResultPage.cs:58-60` — key-value tag count
- `Facepunch.Steamworks/Structs/UgcResultPage.cs:90-93` — additional-preview count
- `Facepunch.Steamworks/SteamUgc.cs:167-176` — `GetSubscribedItems`
- `Facepunch.Steamworks/SteamInventory.cs:140-148` — `GetDefinitionsWithPricesAsync`
- `Facepunch.Steamworks/Structs/InventoryResult.cs:44-47` — `GetItems`

**What's wrong**

The representative case:

```csharp
// UgcResultPage.cs:78-86
uint numChildren = item.details.NumChildren;
if ( ReturnsChildren && numChildren > 0 )
{
    var children = new PublishedFileId[numChildren];
```

`NumChildren` is `m_unNumChildren` from `SteamUGCDetails_t`, a `uint32` Steam fills in. The only
check is `> 0`. At the top of the range that is a 34 GB allocation request →
`OutOfMemoryException` thrown out of a lazy `Entries` iterator, which per S3 also strands the
query handle.

`UgcResultPage.cs:58-60` casts a Steam `uint` to `int` for a `Dictionary` capacity — a value
above `int.MaxValue` gives a negative capacity and `ArgumentOutOfRangeException`.
`SteamUgc.cs:172` does `new PublishedFileId[numItems]` straight from `GetNumSubscribedItems()`.

Importantly, in every case **the array length and the count passed to Steam are the same
variable**, so there is no buffer overrun — the two always agree. This is a denial-of-service
and robustness gap, not a memory-safety one.

**Inference boundary.** Proven: no upper bound is applied. Not established: whether the Steam
client can be made to return such a value. For the UGC paths the underlying data is
attacker-authored Workshop metadata, which makes a corrupt-value path more plausible than for
purely local counts; I could not confirm it offline.

**Recommended fix** Clamp each count to a sane ceiling before allocating and log when clamping
fires. `numChildren` and the preview count have natural Workshop limits; subscribed items and
inventory definitions can use a large but finite cap. Cast to `int` with an explicit range check
rather than an unchecked narrowing.

---

### S16 — `InventoryItem` property parsing throws on ordinary Steam data

**Severity: LOW** · **PROVEN** (reproduced)

**Location** `Facepunch.Steamworks/Structs/InventoryItem.cs:101-117` and `:140-161`

**What's wrong**

`GetProperties` builds a dictionary from Steam's comma-separated name list with `.Add`:

```csharp
foreach ( var propertyName in propNames.Split( ',' ) )
{
    if ( SteamInventory.Internal.GetResultItemProperty( result, (uint)index, propertyName, out var strVal ) )
    {
        props.Add( propertyName, strVal );
    }
}
```

A repeated name throws. So does an empty list, in a subtler way — `"".Split(',')` yields one
*empty* key, not zero keys.

`Acquired` (`:140-161`) substrings a Steam-supplied string at six fixed offsets with no length
check and no `TryParse`.

**Evidence — reproduced**

```
=== T4  Dictionary.Add over Steam's comma-separated property-name list ===
  "colour,wear,colour" -> THREW ArgumentException: An item with the same key has already been added. Key: colour
  "".Split(',') -> Length=1, [0]=""  (one empty key, not zero)

=== T6  InventoryItem.Acquired parsing the Steam-supplied 'acquired' property ===
  "20240102T030405Z" -> 2024-01-02T03:04:05.0000000Z
  ""               -> THREW ArgumentOutOfRangeException
  "2024"           -> THREW ArgumentOutOfRangeException
  "notadate______" -> THREW FormatException
```

`Acquired` matters more than it looks, because for a **deserialized** result the properties
originate from a remote player's serialized blob (S-2 in the security review).

**Recommended fix** Use `props[propertyName] = strVal` instead of `.Add`, and skip empty names.
In `Acquired`, length-check before substringing and use
`DateTime.TryParseExact` with the actual format, returning `DateTime.UtcNow` (the existing
fallback) on failure rather than throwing out of a property getter.

---

### S17 — `LobbyQuery` filter state is process-global and applied across an `await`

**Severity: LOW** · **INFERRED** (concurrent queries are possible but not forced by the API)

**Location** `Facepunch.Steamworks/Structs/LobbyQuery.cs:175-215`, `:222-224`, `:60`, `:141`

**What's wrong**

The header settles the accumulation question — native clears filters per request:

```c
// isteammatchmaking.h:109-110
// this needs to be called before RequestLobbyList() to take effect
// these are cleared on each call to RequestLobbyList()
```

So sequential queries are fine. But `ApplyFilters()` writes into that global native state and
then the method `await`s:

```csharp
public async Task<Lobby[]> RequestAsync()
{
    ApplyFilters();

    LobbyMatchList_t? list = await SteamMatchmaking.Internal.RequestLobbyList();
```

Two `RequestAsync()` calls in flight interleave: B's filters are applied on top of A's before
A's request fires. Nothing prevents this — `SteamMatchmaking.LobbyList` hands out a fresh query
on every access.

Two smaller issues alongside: `stringFilters.Add( key, value )` (`:60`) and
`nearValFilters.Add( key, value )` (`:141`) throw `ArgumentException` on a repeated key, while
`numericalFilters` is a `List` that silently accumulates duplicates — inconsistent. And
`WithKeyValue` validates the key length but never the value; `WithSlotsAvailable` and
`WithMaxResults` validate nothing.

**Recommended fix** Document that lobby-list queries cannot overlap, and either serialise them
behind a lock or fail fast when one is in flight. Use indexer assignment for the two
dictionaries. Validate value length and reject negative slot/result counts.

---

### S18 — Smaller subsystem defects

**Severity: LOW** · **PROVEN** (code paths) unless noted

| # | Location | Issue |
|---|---|---|
| a | `Structs/UgcItem.cs:267` | `if ( Tags.Length == 0 )` in `HasTag` throws `NullReferenceException` for an `Item` built by `new Item( fileId )` (`:17`), where `Tags` is never assigned — e.g. the item created in `SteamUgc.DownloadAsync` (`SteamUgc.cs:78`). |
| b | `Generated/SteamStructs.cs:134` | `TagsTruncated` (`m_bTagsTruncated`, `isteamremotestorage.h:486`) is parsed but never surfaced. Tags come from the fixed `char[1025]` blob (`k_cchTagListMax`, `:56`), so silent truncation is invisible to callers. |
| c | `SteamRemoteStorage.cs:33-39` | `FileWrite` passes `data.Length` through with no check against `k_unMaxCloudFileChunkSize = 100 * 1024 * 1024` (`isteamremotestorage.h:20`). That constant has **no C# counterpart anywhere** in the repo. Also no null check — `data == null` gives `NullReferenceException`, not a clean error. Soft failure: Steam returns `false`. |
| d | `SteamRemoteStorage.cs:110`, `:123`, `:136` | `GetQuota`'s `bool` return is discarded at all three call sites; on failure the out-params keep their initial `0` and `QuotaBytes`/`QuotaUsedBytes`/`QuotaRemainingBytes` all silently report zero. `QuotaUsedBytes` also computes `t - a` on `ulong`, which wraps rather than clamps. |
| e | `SteamInput.cs:35`, `:44-48` | `queryArray` is a `static readonly` 16-slot buffer consumed by a lazy `yield return` iterator, so two interleaved enumerations of `SteamInput.Controllers` overwrite each other's results. Separately, the returned `num` is used as the loop bound with no clamp to `queryArray.Length` — managed array, so `IndexOutOfRangeException` rather than corruption. |
| f | `Generated/SteamEnums.cs:865-870` | `UgcReadAction` member names are mangled by the generator — `ontinueReadingUntilFinished`, `ontinueReading`, `lose`. Values 0/1/2 are correct against `isteamremotestorage.h:149/153/157`; the identifiers lost their leading `C`. |
| g | `Structs/InventoryResult.cs:129-145` | `Serialize()` does `fixed ( byte* ptr = data )` on a `byte[]` sized from Steam. **Reproduced:** `fixed` over a zero-length array yields a null pointer, which `SerializeResult` reads as "tell me the size" rather than "write here" — a silent no-op returning `byte[0]`. |
| h | `SteamInventory.cs:132-154` | `GetDefinitionsWithPricesAsync` fetches `currentPrices` and `baseprices` into local arrays and then **discards both**, returning only the ids. Callers must re-query per item via `InventoryDef.LocalPrice`, which is a fresh `GetItemPrice` call each — the batch call's entire benefit is thrown away. |
| i | `SteamInventory.cs:275-302` | `DeserializeAsync`'s `dataLength` parameter is unvalidated. **Reproduced:** `Marshal.AllocHGlobal` with a negative value throws `OutOfMemoryException` (misleading), and `Marshal.Copy` with `dataLength > data.Length` throws `ArgumentOutOfRangeException`. Both are safe but present as the wrong error. |
| j | `SteamInventory.cs:50-65` | `LoadDefinitions` assigns `Definitions` **before** rebuilding `_defMap`, then fills the new dictionary entry by entry. A concurrent `FindDefinition` can observe a partially built map. `WaitForDefinitions` gates on `Definitions != null`, i.e. on the earlier of the two writes. *INFERRED* — depends on the threading contract report 02 established. |
| k | `Structs/Lobby.cs:55-69` vs `SteamFriends.cs:138-149` | `Lobby.Members` re-reads `MemberCount` every iteration (one extra native call per member); `GetFriendsWithFlag` deliberately hoists its count. Both are lazy iterators, so the hoisted one can index past a shrunk list and the re-read one can skip or repeat a member. The two files disagree on the pattern. |

---

## Security review

Scoped to what actually applies to a binding library: buffer sizing on every call where a
buffer is supplied or received, integer overflow in size arithmetic, unvalidated indices into
native arrays, unbounded allocation driven by a Steam-returned value, and any path where
remote-player-controlled data reaches a fixed buffer or an allocation size.

### Where remote data reaches a buffer

| Source | Path | Verdict |
|---|---|---|
| **Lobby chat message** | `GetLobbyChatEntry` → 32 KB pooled buffer → `MemoryToString` | **S2 — exploitable.** Length discarded; NUL scan runs into unscrubbed pool residue. Proven. |
| **Friend / clan chat** | `GetFriendMessage`, `GetClanChatMessage` → same | **S2 — same defect, same root cause.** |
| **Lobby metadata** | `GetLobbyDataByIndex` → two pooled buffers | Sized correctly (32768 declared == allocated). Feeds the same pool, so it is a *source* of residue for S2. |
| **Workshop metadata** (title, description, tags, kv-tags, metadata, preview URLs) | `GetQueryUGC*` → pooled buffers | Sizes correct. `numChildren` / preview count drive unbounded allocations — S15. Tags truncate silently — S18b. |
| **Deserialized inventory result** | `DeserializeResult` → `GetResultItems` / `GetResultItemProperty` | Buffer sizing correct. Item count drives `new SteamItemDetails_t[cnt]` unvalidated (S15); property strings reach the unguarded `Acquired` parser (S16). Steam signs the blob, which bounds the trust problem. |
| **Rich presence** | `GetFriendRichPresence` | Pooled buffer, correct size. No length validation on the *set* side (below). |
| **Server-browser strings** | — | Covered by report 04; not re-examined. |

### Buffer sizing — swept, and clean

All 36 `Helpers.TakeMemory()` call sites across the generated interfaces were checked
mechanically for a declared size that differs from the allocation:

```
grep -rn "TakeMemory()" Generated/Interfaces/ | wc -l      -> 36
grep -rn "uint sz|int sz" Generated/Interfaces/ | grep -v "= (1024 \* 32);"   -> (empty)
```

Every one declares exactly `1024 * 32`, matching `Helpers.MemoryBufferSize`. `MemoryToString`
scans to `MemoryBufferSize`, which equals the `AllocHGlobal` size — so there is **no
off-by-one and no over-read** on any pooled-string path. The buffers are over-provisioned
relative to the header maxima (32768 against `k_cchDeveloperMetadataMax = 5000`,
`k_nMaxLobbyKeyLength = 255`, `k_cchPublishedFileURLMax = 256`), which is wasteful but safe.

**The one genuine buffer-size defect is S1** — and it is in a `ref`-vs-array declaration, not in
a size constant.

### Integer overflow in size arithmetic

`SteamUtils.GetImage` (`SteamUtils.cs:112`) computes `i.Width * i.Height * 4` in **unchecked
`uint`**, with no validation of either dimension, then narrows to `int` twice. Above 2^30 pixels
this wraps. It is reachable from `SteamFriends.GetLargeAvatarAsync`. The copy itself stays in
bounds because `Helpers.TakeBuffer` honours `minSize` (`Helpers.cs:78-81`), so the practical
consequence is a wrong-sized image rather than corruption — but the guard is missing and the
dimensions come from outside the process. **INFERRED**; requires the Steam client to report
bogus dimensions.

`UgcResultPage.cs:58` and `UgcQuery`'s `(int) result.Value.NumResultsReturned` are unchecked
`uint`→`int` narrowings. Negative results yield zero-iteration loops or
`ArgumentOutOfRangeException`, not corruption.

### Unvalidated indices into native arrays

- `GetFriendByIndex` with a hoisted count (`SteamFriends.cs:143-148`) can be called outside the
  documented `[0, GetFriendCount())` range (`isteamfriends.h:224`) if the list shrinks
  mid-enumeration. Native behaviour out of range is not documented and not in this repo —
  **cannot confirm**.
- `GetLobbyMemberByIndex` re-reads its bound, so the range holds; identity can still shift.
- `GetFriendRichPresenceKeyByIndex` takes an unvalidated index but has **no public caller**, so
  it is unreachable today.
- `GetFileNameAndSize` (`SteamRemoteStorage.cs:179-181`) re-reads `FileCount` per iteration —
  one native call per file, and a stale-index window between the bound check and the call.

### Shared-buffer aliasing

`Helpers.TakeBuffer` (`Helpers.cs:66-85`) round-robins four `byte[]` buffers and hands them out
**beyond the lock**. Concurrent `GetImage` calls — trivially reachable, since all three avatar
getters are `async` — can receive the same buffer and corrupt each other's pixel data. The
`Helpers.Memory` pool is better (it hands out exclusive pointers) but is unscrubbed, which is
S2. `SteamInput.queryArray` is a third instance of the same shape (S18e).

### Not vulnerabilities — checked and cleared

- **No heap over-read on any string path.** The `MemoryToString` scan bound equals the
  allocation size exactly.
- **No buffer overrun in `GetQueryUGCChildren`.** The array length and `cMaxEntries` are the
  same variable.
- **`FileRead` handles short reads correctly** — it compares the return against the requested
  size and returns `null` on mismatch. The hypothesised full-size-array-on-partial-read defect
  does not exist.
- **`SerializeResult` / `DeserializeResult` sizing is correct** — the two-pass
  query-size-then-fill pattern is used properly.
- **`GetQuota` has no truncation** — `uint64` ↔ `ulong` throughout.
- **Steam signs serialized inventory results**, so `DeserializeAsync` is not an arbitrary
  deserialization sink; `BelongsTo` (`CheckResultSteamID`) is exposed for the impersonation
  check the header calls for.

---

## Verified-correct

Recorded so the next pass does not re-litigate them.

**Inventory**
1. `GetResultItems` two-pass sizing (count first, then fill) is used correctly in both
   `GetItems` overloads.
2. `Serialize()` uses the header's documented `NULL`-to-query-size pattern correctly.
3. `DeserializeResult` passes `bRESERVED_MUST_BE_FALSE = false`, as the header requires
   (`isteaminventory.h:171-172`).
4. `BelongsTo` correctly wraps `CheckResultSteamID`, the impersonation check `isteaminventory.h:118-120`
   asks for.
5. `k_SteamInventoryResultInvalid` is `-1` in both header (`:62`) and C#
   (`SteamConstants.cs:63`); `SteamInventoryResult_t` is `int` in both.
6. `SteamItemDetails_t.Quantity` is `uint16` in both.
7. `GetItemDefinitionIDs` correctly passes `null` for the count-query pass.
8. The 32 KB property buffers exceed any plausible definition property.

**UGC**
9. Pagination is 1-based and matches `isteamugc.h:216` (*"unPage should start at 1"*), with an
   explicit `page <= 0` guard. There is no page arithmetic at all, so no divide-by-zero.
10. `GetQueryUGCChildren`'s array length and `cMaxEntries` always agree — no overrun.
11. `SteamParamStringArray`'s native memory is kept alive across the P/Invoke by a `using` block
    (`UgcEditor.cs:208-215`) — no dangling pointer. `NumStrings` is `int` matching `int32`.
12. Key-value tag ordering is correct: all removals are issued before all additions, matching the
    documented replace idiom.
13. `GetItemUpdateProgress`'s percentage divide is guarded (`total > 0 ? ... : 0.0f`).
14. `Query.InLanguage()` **does** call `SetLanguage` in the current working tree. A first-pass
    sweep flagged it as dead; re-checking showed a concurrent session had already fixed it.
15. `GetItemUpdateInfo` does not exist in this SDK version — only `GetItemUpdateProgress`. Not a
    gap.

**Cloud**
16. `FileRead` checks the return value against the requested size and returns `null` on a short
    read (`SteamRemoteStorage.cs:53-57`). Deliberate — commit `aab6d0d`.
17. `FileRead` guards `size <= 0` before `new byte[size]`.
18. `GetQuota` is `uint64` ↔ `ulong` with no truncation.
19. `UGCRead`'s parameter types and order match `isteamremotestorage.h:258` exactly.
20. `RemoteStoragePlatform` bit values 0–64 map correctly; `All = -1` is the signed
    reinterpretation of `0xffffffff` and is wire-correct (though it should be `[Flags]` and
    `uint`).

**Input**
21. `STEAM_CONTROLLER_MAX_COUNT = 16` matches `STEAM_INPUT_MAX_COUNT` (`isteaminput.h:18`).
    (The C# name is the old `ISteamController` one and the value is a hand-maintained literal —
    worth linking to the header, but currently correct.)
22. `GetActiveActionSetLayers` is marshalled correctly as `[In,Out] T[]` — unlike the two origin
    functions.
23. `GetGlyphForActionOrigin` / `GetStringForActionOrigin` take a single origin by value and
    return `Utf8StringPointer`. No buffer sizing involved; correct.

**Friends / Matchmaking**
24. Lobby-list filters do **not** accumulate across queries — native clears them per
    `RequestLobbyList` (`isteammatchmaking.h:109-110`), and `ApplyFilters` runs immediately
    before the request.
25. `GetLobbyByIndex` is correctly bounded by `LobbyMatchList_t::m_nLobbiesMatching`, matching
    `isteammatchmaking.h:127`.
26. `CreateLobbyAsync` checks `LobbyCreated_t.Result` correctly; `LobbyDataUpdate_t` gates on
    `Success`. Only `LobbyEnter_t` is unchecked (S7).
27. `Dispatch.Install` handlers are removed by scope on shutdown (`Dispatch.cs:314-336`) — no
    callback leak in either subsystem. `AvatarImageLoaded_t` has no handler to leak because none
    is ever installed (S12).
28. `Utf8StringPointer` null-guards a `nullptr` return (`Utf8String.cs:50-51`), so
    `GetFileNameAndSize` on an out-of-range index yields `null` rather than a crash. (Left open
    by the first-pass sweep; confirmed here.)
29. `Lobby.Members` re-reads its count every iteration, so the index stays within
    `[0, GetNumLobbyMembers())`.

---

## Open questions requiring a live Steam client

1. **S1 — how many bytes does Steam actually write into `originsOut`?** If the implementation
   zero-fills all 8 slots before populating, the overflow is unconditional rather than
   conditional on multi-origin bindings. A single call with a two-origin binding, watching a
   canary local, settles it. **This is the highest-value live test on the list.**
2. **S2 — does a real third-party client send unterminated chat payloads?** The defect is proven
   against the library's own `SendChatBytes`; confirming an off-the-shelf client does it too
   would raise the practical severity.
3. **S6 — does `GetDigitalActionHandle` return `0` before the manifest loads?** The permanent
   caching of `0` is proven; the frequency of the trigger is not.
4. **S15 — can `m_unNumChildren` or `GetNumSubscribedItems` be driven to a pathological value?**
   Requires a crafted collection or a large subscription set.
5. **Out-of-range index behaviour** for `GetFriendByIndex` / `GetLobbyMemberByIndex` /
   `GetFriendRichPresenceKeyByIndex`. The headers document the valid range but not what happens
   outside it, and the implementation is not in this repo.
6. **S18d — can Steam report `available > total` from `GetQuota`?** Determines whether the
   `ulong` subtraction can underflow in practice.
7. **S4 — confirm the leak with the Steam client's own memory counters** across N
   `GetAllItemsAsync` calls, to put a number on it.
8. **S8 — `GetUGCDetails`'s actual behaviour** under the `ref char[]` binding. Only worth doing
   if the function is going to be wrapped rather than deleted.

---

## Prioritised backlog

| P | Finding | Why this order | Effort |
|---|---|---|---|
| **P0** | **S1** — origins bound as `ref` scalar | Only memory-corruption defect in scope; three public methods reach it; generator fix, mechanical | S |
| **P0** | **S2** — chat residue disclosure | Remotely triggerable, proven, and the fix is to *use a value already in hand* | S |
| **P1** | **S13** then **S4** — inventory handle lifetime | Must land in this order: make `Dispose` idempotent before introducing disposal, or S4's fix double-frees | S then M |
| **P1** | **S3** — UGC query handle leak | Unbounded native leak on an ordinary failure path; `try/finally` plus one early-return fix | M |
| **P1** | **S5** — false publish success | Data-loss-adjacent: player believes a Workshop item published when nothing uploaded. One-line move | XS |
| **P1** | **S6** — input handle caches | Silently dead actions with no diagnostic; four independent problems, all small | M |
| **P2** | **S7** — `JoinLobbyAsync` response | Straightforward correctness gap; `Lobby.Join()` already shows the right shape | XS |
| **P2** | **S11** — workshop tags as ANSI | Ship alongside report 01 F4 — same root cause, one commit | XS |
| **P2** | **S9**, **S10** — `InventoryDef` throws + wrong price field | Three one-liners, each a user-visible bug | XS |
| **P2** | **S12** — unbounded avatar polling | Copy the timeout/`finally` shape from `Achievement.cs` | S |
| **P2** | **S14** — lobby data validation | Byte-vs-char is a real correctness bug for non-Latin games; `nameof` fix is free | XS |
| **P3** | **S8** — `GetUGCDetails` binding | Unreachable today. Fix in the generator or delete the binding | S |
| **P3** | **S15** — unbounded allocations | Defence in depth; clamp and log | S |
| **P3** | **S16**, **S17**, **S18** | Robustness and consistency cleanup; several are one-liners worth folding into neighbouring work | S |

**Cross-cutting, and worth its own task:** the three shared-buffer mechanisms —
`Helpers.Memory` (unscrubbed pool, S2), `Helpers.TakeBuffer` (aliasing beyond the lock), and
`SteamInput.queryArray` (static array behind a lazy iterator) — are three instances of one
design mistake. `TakeBuffer` even carries the comment *"We shouldn't really be using this
anymore."* Report 06 tightened these paths for allocation; a follow-up should give them
ownership semantics.

**Testability.** Six of these findings are reachable by offline tests of exactly the kind
report 05 recommends, with no Steam client: S2 (pooled-buffer residue — the harness in this
report is already a test), S9, S13, S16 (all pure-managed), S1 (a reflection assertion that no
`STEAM_OUT_ARRAY_COUNT` parameter is bound as `ref`), and S11 (an assertion that
`StringToHGlobalAnsi` appears nowhere in the assembly). The last two are *invariant* tests over
the generated code and would have caught S1 and S11 at generation time. They belong in the
`Tools/` project alongside `verify-native-conformance.ps1`.
