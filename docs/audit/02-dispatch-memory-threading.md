# Dispatch, Memory Safety & Threading Audit

Scope: callback dispatch, call-result lifetime, unmanaged memory, pinning, threading, init/shutdown
lifecycle, and per-frame allocation in `Facepunch.Steamworks`.

Method: full read of `Classes/Dispatch.cs`, `Callbacks/*`, `Utility/*`, `ServerList/*`, `Networking/*`,
`SteamClient.cs`, `SteamServer.cs`, all `Steam*.cs` wrappers and the relevant generated interfaces,
cross-checked against `Generator/steam_sdk/steam_api.h`, `steam_api_internal.h`, `steam_api_common.h`
and `isteamnetworkingsockets.h`. Claims marked **PROVEN** were reproduced offline against the built
`Facepunch.Steamworks.Win64.dll` (net6.0) with a reflection/behaviour harness; no live Steam client was
used or required. Claims marked **INFERRED** are reasoned from source + SDK headers and are labelled as such.

---

## Summary

The dispatch core is structurally sound in the one place people usually get it wrong:
`SteamAPI_ManualDispatch_FreeLastCallback` **is** called on every path including exception paths, the
`CallbackMsg_t` marshalling layout **is** byte-exact against the native x64 struct, and the custom
`CallResult<T>` awaiter **does** resume inline on the dispatch thread, so `GetAPICallResult` runs before the
native message is freed. There are also **zero `GCHandle`s in the entire assembly** — the classic
`ISteamMatchmakingServers` "delegate collected → native calls into freed memory" crash is structurally
impossible here because the binding passes `IntPtr.Zero` for the response vtable and polls instead.

The serious problems are all at the **edges of the pump**, not inside it:

1. **Shutdown races the async pump.** `SteamClient.Shutdown()` zeroes the pipe and immediately calls
   `SteamAPI_Shutdown()` without joining the pump task. The pump provably runs on ThreadPool threads
   when there is no `SynchronizationContext` (dedicated servers, console hosts), so it can be mid-`Frame`
   on a destroyed pipe. This is a crash-on-exit.
2. **Every pending `await` is silently abandoned on Shutdown.** Proven: 5000 registered continuations,
   0 invoked, dictionary emptied. Awaiters hang forever.
3. **`RunCallbacks` is not thread-safe and the reentrancy guard is a non-atomic `bool`.** Proven to admit
   two concurrent entrants ~872,000 times in a 2M-iteration two-thread race.
4. **Server init installs client-flagged callbacks that server shutdown never removes.** Proven: handler
   count grows 2 → 4 → 6 across three dedicated-server restart cycles.
5. **User callback exceptions are swallowed by default** and permanently drop the remaining handlers for
   that callback.

The library has **no documented threading contract**; the real one is derived below.

Counts: 23 findings — 6 HIGH, 10 MEDIUM, 7 LOW. No CRITICAL (nothing corrupts memory on a
correctly-sequenced single-threaded main-thread usage), but findings F1/F3/F6 are memory-unsafe as soon as
the caller does anything the library never told them not to.

---

## The actual threading & lifetime contract (as implemented, not as documented)

Neither `README.md` nor any XML doc states a threading contract. Here is what the code actually does.

### Who runs the pump

`SteamClient.Init(appid, asyncCallbacks: true)` (the **default**) starts:

```csharp
// Classes/Dispatch.cs:236-243
internal static async void LoopClientAsync()
{
    while ( ClientPipe != 0 )
    {
        Frame( ClientPipe );
        await Task.Delay( 16 );
    }
}
```

`async void`, no `ConfigureAwait`. Therefore **the thread that runs `Frame()` — and so every user callback
and every `public static event` in the library — is decided entirely by the `SynchronizationContext`
that was current on the thread that called `Init`.**

PROVEN (harness TEST B, reproducing the exact `Frame(); await Task.Delay(16)` shape):

```
main thread id = 1, SynchronizationContext.Current = null
no SyncContext  -> Frame() ran on thread ids: 1,5,5,5,5,5,5,5  (distinct=2)
with SyncContext-> Frame() ran on thread ids: 1,1,1,1,1,1,1,1  (distinct=1)
```

So:

| Host | `SynchronizationContext` at `Init` | Where callbacks fire |
|---|---|---|
| Unity (Init from a MonoBehaviour) | `UnitySynchronizationContext` | Unity main thread — safe, and this is why it "works" |
| WPF / WinForms | Dispatcher/WinForms context | UI thread |
| Console / dedicated server / xUnit | `null` | **arbitrary ThreadPool threads, changing between frames** |
| `asyncCallbacks: false` + manual `RunCallbacks()` | n/a | whatever thread you call it on |

This is a silent, host-dependent contract. A dedicated server built on this library gets all Steam events on
random pool threads, which is almost certainly not what the game code assumes.

### What the pump does per frame

```csharp
// Classes/Dispatch.cs:84-118
internal static void Frame( HSteamPipe pipe )
{
    if ( runningFrame ) return;
    try {
        runningFrame = true;
        SteamAPI_ManualDispatch_RunFrame( pipe );
        SteamNetworkingUtils.OutputDebugMessages();
        CallbackMsg_t msg = default;
        while ( SteamAPI_ManualDispatch_GetNextCallback( pipe, ref msg ) )
        {
            try     { ProcessCallback( msg, pipe == ServerPipe ); }
            finally { SteamAPI_ManualDispatch_FreeLastCallback( pipe ); }
        }
    }
    catch ( System.Exception e ) { OnException?.Invoke( e ); }
    finally { runningFrame = false; }
}
```

This matches Valve's reference loop in `steam_sdk/steam_api.h:178-206` in shape, with two deviations:
call results are fetched through `ISteamUtils::GetAPICallResult` rather than
`SteamAPI_ManualDispatch_GetAPICallResult`, and `msg.DataSize` is never checked.

### Object lifetime rules the code actually enforces

* **Callback registrations** (`Dispatch.Callbacks`) live from `InitializeInterface` to
  `Dispatch.ShutdownClient/ShutdownServer`, and are partitioned only by a `bool server`.
* **Call results** (`Dispatch.ResultCallbacks`) live from the first `await` until the matching
  `SteamAPICallCompleted_t` arrives — or **forever**, if it never does.
* **Public static events** live for the lifetime of the AppDomain. Nothing ever unsubscribes them.
* **`SteamInterface.Self` pointers** are dropped by `DestroyInterface` setting the holder to `null`, but
  `SteamInterface.ShutdownInterface()` (`Utility/SteamInterface.cs:50-53`) — the method that actually
  clears `Self` — is dead code, never called from anywhere.
* **Native scratch memory** is pooled in process-lifetime statics (`Helpers.BufferBag`,
  `Helpers.BufferPool`, `BufferManager.BufferPools`) that are never drained on shutdown.

---

## Findings

### F1 — HIGH — `Shutdown()` does not stop or join the async pump; it can call into `steam_api` after `SteamAPI_Shutdown()`

**Location:** `Facepunch.Steamworks/SteamClient.cs:136-151`, `Facepunch.Steamworks/SteamServer.cs:164-170`,
`Facepunch.Steamworks/Classes/Dispatch.cs:236-257`

**What's wrong:** the pump is an `async void` fire-and-forget loop with no cancellation token, no completion
`Task`, and no handshake. `Shutdown()` zeroes the pipe and then calls the native shutdown on the caller's
thread. If a pump thread has already passed the `while ( ClientPipe != 0 )` check and entered `Frame(pipe)`,
it keeps issuing `SteamAPI_ManualDispatch_RunFrame` / `GetNextCallback` / `FreeLastCallback` against a pipe
that native code has torn down.

**Evidence:**

```csharp
// SteamClient.cs:136-151
public static void Shutdown()
{
    if ( !IsValid ) return;
    Cleanup();
    SteamAPI.Shutdown();          // <-- native teardown, immediately
}
internal static void Cleanup()
{
    Dispatch.ShutdownClient();    // <-- only sets ClientPipe = 0
    initialized = false;
    ShutdownInterfaces();
}
```

```csharp
// SteamServer.cs:164-170
public static void Shutdown()
{
    Dispatch.ShutdownServer();    // ServerPipe = 0
    ShutdownInterfaces();
    SteamGameServer.Shutdown();   // <-- native teardown, immediately
}
```

There is no `Task` field for the loop, no `CancellationTokenSource`, and no wait on `runningFrame`.
`Dispatch.Frame` takes `pipe` **by value**, so zeroing the static does not stop an in-flight frame.

PROVEN (harness TEST B) that `Frame()` runs on a ThreadPool thread (id 5) while the caller is on thread 1
whenever `SynchronizationContext.Current == null`, which is the normal case for a console/dedicated-server
host. Under Unity's main-thread context the two are the same thread and the race does not occur — which is
exactly why this has survived.

**Failure scenario:** A headless Rust-style dedicated server calls `SteamServer.Init(appid, init)` (default
`asyncCallbacks: true`) and later `SteamServer.Shutdown()` from its main loop on exit. ~1 frame in N the pump
thread is inside `SteamAPI_ManualDispatch_GetNextCallback(deadPipe)` when `SteamGameServer_Shutdown()`
returns → access violation inside `steam_api64.dll` at process exit. Intermittent, unreproducible-looking,
and it will be blamed on Steam.

**Recommended fix:** hold the loop `Task` and a `CancellationTokenSource` in `Dispatch`; make
`LoopClientAsync`/`LoopServerAsync` `async Task` instead of `async void`; have `Shutdown()` cancel and then
block on the task (with a bounded timeout) *before* calling `SteamAPI.Shutdown()` /
`SteamGameServer.Shutdown()`. At minimum, spin-wait on `Volatile.Read(ref runningFrame)` after zeroing the
pipe.

---

### F2 — HIGH — Every pending call result is silently discarded on Shutdown; awaiters hang forever

**Location:** `Facepunch.Steamworks/Classes/Dispatch.cs:308-332` (`ShutdownServer`, `ShutdownClient`),
`Dispatch.cs:265-277` (`ResultCallbacks`, `OnCallComplete`)

**What's wrong:** `ShutdownClient`/`ShutdownServer` rebuild `ResultCallbacks` filtered by the `server` flag.
The dropped entries' `continuation` delegates are **never invoked**, so the awaiting async state machines are
never resumed. The `Task` returned to the caller never completes, never faults, and never cancels. The
dictionary is also never bounded or evicted while running — anything Steam never completes stays forever.

**Evidence:**

```csharp
// Dispatch.cs:321-332
internal static void ShutdownClient()
{
    ClientPipe = 0;
    foreach ( var callback in Callbacks )
        Callbacks[callback.Key].RemoveAll( x => !x.server );

    ResultCallbacks = ResultCallbacks.Where( x => x.Value.server )
                                     .ToDictionary( x => x.Key, x => x.Value );
}
```

```csharp
// Dispatch.cs:270-277
internal static void OnCallComplete<T>( SteamAPICall_t call, Action continuation, bool server ) where T : struct, ICallbackData
{
    ResultCallbacks[call.Value] = new ResultCallback { continuation = continuation, server = server };
}
```

Nothing else in the file touches `ResultCallbacks`, and there is no timeout sweep.

**PROVEN** (harness TEST 4 — registers 5000 client-side continuations via reflection, then calls
`Dispatch.ShutdownClient()`):

```
ResultCallbacks.Count after 5000 pending client calls = 5000
continuations invoked so far                          = 0
ResultCallbacks.Count after ShutdownClient()          = 0
continuations invoked after ShutdownClient()          = 0   <-- 0 means every awaiter hangs forever
```

**Failure scenario:** Player clicks "Quit" while a leaderboard upload is in flight:

```csharp
await board.SubmitScoreAsync( score );   // never returns
SteamClient.Shutdown();                  // from the quit handler on another frame
```

The `await` never resumes. If the game had `await`-chained its save-and-quit sequence behind that call, the
process hangs on exit with no exception and no log line. The same applies to a Unity domain reload:
`Shutdown()` on `OnApplicationQuit`, the pending state machines and all their captured objects stay rooted
in the (now abandoned) dictionary until the domain dies.

**Recommended fix:** on shutdown, drain the dropped entries and invoke each continuation exactly once so
`CallResult<T>.GetResult()` runs its failure path and returns `null` (it already handles this — see
Verified-correct). Better: convert to `TaskCompletionSource<T?>` and `TrySetResult(null)` /
`TrySetCanceled()` on shutdown. Additionally add an age-based sweep so calls Steam never completes cannot
accumulate.

---

### F3 — HIGH — `runningFrame` is a non-atomic, non-volatile check-then-set; `RunCallbacks` is not thread-safe

**Location:** `Facepunch.Steamworks/Classes/Dispatch.cs:79` (`static bool runningFrame = false;`),
`Dispatch.cs:86-91`, `Dispatch.cs:114-117`

**What's wrong:** the comment on line 76-78 says the guard exists so "we don't call Frame in a callback".
It does that. It does **not** make concurrent `RunCallbacks()` safe, but the shape of the code invites that
reading. Two threads can both observe `runningFrame == false` and both proceed, because the read and the
write are separate non-interlocked operations on a non-`volatile` field. Two threads inside `Frame` on the
same pipe means concurrent `GetNextCallback`/`FreeLastCallback` — which Valve explicitly forbids
(`steam_api.h:217-218`: "you MUST call `SteamAPI_ManualDispatch_FreeLastCallback` … *before* calling
`SteamAPI_ManualDispatch_GetNextCallback` again") — plus concurrent mutation of the **static**
`actionsToCall` list at `Dispatch.cs:126`.

**Evidence:**

```csharp
// Dispatch.cs:79
static bool runningFrame = false;
// Dispatch.cs:86-91
if ( runningFrame )
    return;
try
{
    runningFrame = true;
```

```csharp
// Dispatch.cs:126 — shared across all threads and both pipes
static List<Action<IntPtr>> actionsToCall = new List<Action<IntPtr>>();
```

**PROVEN** (harness TEST D, replicating the exact guard shape with two threads and a barrier):

```
times both threads were inside Frame() simultaneously = 872129 (max concurrent = 2)
=> the guard does NOT make RunCallbacks thread-safe.
```

**Failure scenario:** A listen-server game pumps `SteamClient.RunCallbacks()` on the Unity main thread and
also runs `SteamServer.RunCallbacks()` on a dedicated network thread (a very natural design, and nothing in
the docs forbids it). Symptoms: sporadic `InvalidOperationException: Collection was modified` out of
`ProcessCallback`'s `foreach ( var action in actionsToCall )` at `Dispatch.cs:154`, and — worse —
`FreeLastCallback` called for a message the other thread is still marshalling out of
`msg.Data`, i.e. a use-after-free read of the native callback payload.

**Recommended fix:** make the guard `[ThreadStatic]` for its stated purpose (reentrancy), and separately
serialise the whole `Frame` body per pipe with a `lock`/`SemaphoreSlim`, or document loudly that
`RunCallbacks` is single-threaded-only and add `Interlocked.CompareExchange` so a second thread returns
instead of racing. Make `actionsToCall` a local (it is cleared and refilled every call anyway; the static
saves one allocation and buys a data race).

---

### F4 — HIGH — Server init registers client-flagged callbacks that server shutdown never removes

**Location:** `Facepunch.Steamworks/SteamApps.cs:18-32`, `Facepunch.Steamworks/SteamServer.cs:117`,
`Facepunch.Steamworks/Classes/Dispatch.cs:308-319`

**What's wrong:** `SteamApps` is a `SteamSharedClass<T>` added by **both** `SteamClient.Init` and
`SteamServer.Init`. Its `InstallEvents()` takes no `server` parameter and so always registers with
`Dispatch.Install<T>(p, server: false)`. `Dispatch.ShutdownServer()` removes only entries with
`x.server == true`. The two `SteamApps` handlers therefore survive every `SteamServer.Shutdown()` and are
re-added on the next `SteamServer.Init()`.

**Evidence:**

```csharp
// SteamApps.cs:18-32
internal override bool InitializeInterface( bool server )
{
    SetInterface( server, new ISteamApps( server ) );
    if ( Interface.Self == IntPtr.Zero ) return false;
    InstallEvents();                                   // <-- `server` is dropped on the floor
    return true;
}

internal static void InstallEvents()
{
    Dispatch.Install<DlcInstalled_t>( x => OnDlcInstalled?.Invoke( x.AppID ) );          // server defaults to false
    Dispatch.Install<NewUrlLaunchParameters_t>( x => OnNewLaunchParameters?.Invoke() );  // server defaults to false
}
```

```csharp
// SteamServer.cs:117
AddInterface<SteamApps>();
```

```csharp
// Dispatch.cs:312-315
foreach ( var callback in Callbacks )
{
    Callbacks[callback.Key].RemoveAll( x => x.server );   // only true-flagged entries
}
```

**PROVEN** (harness TEST E — calls the real `SteamApps.InstallEvents()` then the real
`Dispatch.ShutdownServer()`, three times):

```
SteamApps.InstallEvents signature = Void InstallEvents()   (no `bool server` parameter)
 after server Init+Shutdown cycle 1: leftover handlers = 2 (expected 0)
 after server Init+Shutdown cycle 2: leftover handlers = 4 (expected 0)
 after server Init+Shutdown cycle 3: leftover handlers = 6 (expected 0)
```

For contrast, the correctly-parameterised classes are clean — harness TEST 5 shows matched
`Install(server:x)` / `Shutdown` pairs returning to 0 handlers.

**Failure scenario:** A dedicated server that hot-restarts its Steam session between map rotations (or a
Unity editor entering/exiting play mode without a domain reload) accumulates two extra `DlcInstalled_t`
handlers per cycle. Because `OnDlcInstalled` is a static event that is *also* never cleared (F15), each
`DlcInstalled_t` fires the game's handler N times after N restarts. Any handler that is not idempotent
(spawn an entity, increment a counter, send a Discord webhook) misbehaves in a way that scales with uptime.

**Recommended fix:** give `SteamApps.InstallEvents` a `bool server` parameter and pass it through, matching
every other shared class. Separately, make `Dispatch.Install` idempotent (dedupe by
`(CallbackType, target-method, server)`) so a mis-parameterised caller cannot double-register.

---

### F5 — HIGH — User callback exceptions are swallowed by default and permanently drop the remaining handlers for that callback

**Location:** `Facepunch.Steamworks/Classes/Dispatch.cs:110-113`, `Dispatch.cs:142-160`, `Dispatch.cs:36`

**What's wrong:** `Frame`'s `catch` writes to `OnException`, which is `null` unless the game explicitly sets
it. So by default an exception thrown from a game's `SteamMatchmaking.OnLobbyEntered` handler produces
**no output at all**. Worse, the throw unwinds past the `foreach ( var action in actionsToCall )` loop, so
handlers 2..N for that same callback never run, and the native message is freed in the `finally` — those
handlers do not get a second chance.

**Evidence:**

```csharp
// Dispatch.cs:36
public static Action<Exception> OnException;      // defaults to null

// Dispatch.cs:154-157
foreach ( var action in actionsToCall )
{
    action( msg.Data );                           // no per-handler try/catch
}

// Dispatch.cs:110-113
catch ( System.Exception e )
{
    OnException?.Invoke( e );                     // no-op when unset
}
```

Note the failure asymmetry: callbacks *already dequeued* in this frame's `while` loop are lost for the other
handlers; callbacks still queued in native are merely deferred to the next `Frame` (they are not lost,
because they were never dequeued).

`SteamNetworkingUtils.OutputDebugMessages()` at `Dispatch.cs:94` is inside the same `try` and *before* the
loop, so a throwing `OnDebugOutput` subscriber skips the entire frame's callbacks.

**Failure scenario:** Two systems subscribe `SteamFriends.OnPersonaStateChange` — a UI nameplate updater and
a friend-cache invalidator. The UI updater NREs because a panel was destroyed. The cache invalidator never
runs, silently, for the rest of the session's persona updates. Nothing is logged. Debugging this from a
player bug report is essentially impossible.

**Recommended fix:** wrap each `action( msg.Data )` in its own `try/catch` that reports to `OnException` and
continues with the next handler. Default `OnException` to something that at least writes to
`Console.Error`/`Debug.LogException` rather than `null`.

---

### F6 — HIGH — Calling `Shutdown()` from inside a callback leaves the pump using a destroyed pipe

**Location:** `Facepunch.Steamworks/Classes/Dispatch.cs:84-108`, `SteamServer.cs:164-170`,
`SteamUtils.cs:36-41`

**What's wrong:** `Frame` captures `pipe` as a by-value parameter. A handler invoked from
`ProcessCallback` that calls `SteamServer.Shutdown()` runs `SteamGameServer_Shutdown()` natively and then
returns into `Frame`'s `finally { SteamAPI_ManualDispatch_FreeLastCallback( pipe ); }` followed by another
`SteamAPI_ManualDispatch_GetNextCallback( pipe, ref msg )` — both against a torn-down pipe. Additionally,
once `ServerPipe` is zeroed mid-loop, `pipe == ServerPipe` evaluates to `false` for every remaining message
in that frame, so genuinely server-side callbacks get dispatched as if they were client-side.

**Evidence:**

```csharp
// Dispatch.cs:98-108 — `pipe` is a local copy; zeroing the static does not affect it
while ( SteamAPI_ManualDispatch_GetNextCallback( pipe, ref msg ) )
{
    try     { ProcessCallback( msg, pipe == ServerPipe ); }   // <-- re-evaluated every iteration
    finally { SteamAPI_ManualDispatch_FreeLastCallback( pipe ); }
}
```

The library itself does exactly this pattern internally, though on the survivable side:

```csharp
// SteamUtils.cs:31, 36-41
Dispatch.Install<SteamShutdown_t>( x => SteamClosed(), server );
...
private static void SteamClosed()
{
    SteamClient.Cleanup();      // Dispatch.ShutdownClient() -> ClientPipe = 0, from inside the pump
    OnSteamShutdown?.Invoke();
}
```

`SteamClosed` is survivable because `Cleanup()` deliberately does **not** call `SteamAPI.Shutdown()`, so the
pipe stays valid natively. But it does null every `SteamXxx.Interface`, so any *subsequent* handler in the
same `actionsToCall` batch that touches `SteamXxx.Internal` NREs (which then triggers F5).

**Failure scenario:** A dedicated server calls `SteamServer.Shutdown()` from its
`SteamServer.OnSteamServersDisconnected` handler to trigger a clean restart. The pump immediately calls
`FreeLastCallback` on a freed pipe → access violation, in the exact code path a server operator hits when
Steam has a bad day.

**Recommended fix:** re-read the pipe from the static at the top of each loop iteration and break out if it
changed or became 0; hoist `bool isServer = pipe == ServerPipe;` above the loop; and defer native teardown
in `Shutdown()` when `runningFrame` is set on the current thread.

---

### F7 — MEDIUM (INFERRED) — `ResultCallbacks` is keyed only on the call handle; the `server` flag is stored but never checked

**Location:** `Facepunch.Steamworks/Classes/Dispatch.cs:259-277`, `Dispatch.cs:201-229`

**What's wrong:** `ResultCallback` carries a `server` bool, `OnCallComplete` records it, `ShutdownClient`/
`ShutdownServer` filter on it — but `ProcessResult`, the only consumer, ignores it entirely and keys purely
on `SteamAPICall_t`. If the client and gameserver pipes ever hand out overlapping `SteamAPICall_t` values in
one process, a server completion will fire the continuation registered for a client call of the same handle
(and vice versa), with a `CallResult<T>` bound to the *other* pipe's `ISteamUtils`.

Note also `OnCallComplete` uses **indexer assignment**, not `Add`, so a collision silently overwrites and
discards the earlier continuation — producing the F2 hang for that awaiter.

**Evidence:**

```csharp
// Dispatch.cs:259-265
struct ResultCallback
{
    public Action continuation;
    public bool server;                       // written, filtered on at shutdown...
}
static Dictionary<ulong, ResultCallback> ResultCallbacks = new Dictionary<ulong, ResultCallback>();

// Dispatch.cs:272 — overwrite, not Add
ResultCallbacks[call.Value] = new ResultCallback { continuation = continuation, server = server };

// Dispatch.cs:208-228 — ...but never read here
if ( !ResultCallbacks.TryGetValue( result.AsyncCall, out var callbackInfo ) ) { ...; return; }
ResultCallbacks.Remove( result.AsyncCall );
callbackInfo.continuation();
```

**Status:** INFERRED. Whether Steam's `SteamAPICall_t` handle space is per-pipe or process-global cannot be
determined from the headers in `Generator/steam_sdk` and requires a live client to test (see Open questions).
The defect is cheap to make impossible regardless.

**Failure scenario (if handles overlap):** a listen server (both `SteamClient.Init` and `SteamServer.Init` in
one process, as Rust and Garry's Mod do) sees awaited Steam calls intermittently return `null` — because the
wrong continuation ran and `GetResult()` found its own call not yet complete — while an unrelated `await`
elsewhere hangs forever.

**Recommended fix:** key on `(bool server, ulong call)`, or check `callbackInfo.server == isServer` in
`ProcessResult` and skip if mismatched. Use `Add` (or assert on overwrite) so a collision is loud.

---

### F8 — MEDIUM — The pump never validates `msg.DataSize` against the size of the struct it marshals

**Location:** `Facepunch.Steamworks/Classes/Dispatch.cs:131-161`, `Dispatch.cs:166-196`,
`Facepunch.Steamworks/Utility/Utility.cs:15-21`

**What's wrong:** `ProcessCallback` reads `sizeof(T)` bytes out of `msg.Data` with no reference to
`msg.DataSize`. If a shipped Steam client posts a callback smaller than the struct this binding was
generated against (Valve shrinks/reshapes structs across SDK revisions and reuses callback ids), the
marshaller reads past the end of the native allocation. `CallbackToString` even takes the size and then
never uses it:

**Evidence:**

```csharp
// Dispatch.cs:166 — `expectedsize` is never referenced in the body
internal static string CallbackToString( CallbackType type, IntPtr data, int expectedsize )
{
    if ( !CallbackTypeFactory.All.TryGetValue( type, out var t ) ) return $"[{type} not in sdk]";
    var strct = data.ToType( t );          // reads sizeof(t), not expectedsize
```

```csharp
// Dispatch.cs:303 — the dispatch closure, also size-blind
action = x => p( x.ToType<T>() ),
```

```csharp
// Utility.cs:15-21
static internal T ToType<T>( this IntPtr ptr )
{
    if ( ptr == IntPtr.Zero ) return default;
    return (T)Marshal.PtrToStructure( ptr, typeof( T ) );
}
```

The over-read window is not small. PROVEN (harness TEST F, over all 216 registered callback structs):

```
SteamUGCRequestUGCDetailsResult_t                 9792 bytes
RemoteStorageGetPublishedFileDetailsResult_t      9760 bytes
GetTicketForWebApiResponse_t                      2572 bytes
OverlayBrowserProtocolNavigation_t                1024 bytes
SteamNetConnectionStatusChangedCallback_t          712 bytes
total callback structs = 216
```

**Failure scenario:** A player runs an older/newer Steam client than the SDK the binding was generated
against; a `SteamUGCRequestUGCDetailsResult_t` arrives with `m_cubParam` of, say, 8 KB. The binding reads
9792 bytes → heap over-read → garbage strings, or an access violation if the allocation sat at the end of a
page. Reported as a random crash in workshop browsing.

**Recommended fix:** in `ProcessCallback`, compare `msg.DataSize` with `default(T).DataSize` (already
available on every `ICallbackData` via the generated `_datasize`) and skip + report via `OnException` on
mismatch. Use `expectedsize` in `CallbackToString` or delete the parameter.

---

### F9 — MEDIUM — Every callback delivery boxes; measured 320 bytes per `SteamNetConnectionStatusChangedCallback_t`, per handler

**Location:** `Facepunch.Steamworks/Classes/Dispatch.cs:290-306`, `Facepunch.Steamworks/Utility/Utility.cs:15-21`

**What's wrong:** `Dispatch.Install<T>` builds a closure over the non-generic
`Marshal.PtrToStructure(IntPtr, Type)`, which returns `object` — so every single callback delivery boxes the
struct and then unboxes it. The generic `Marshal.PtrToStructure<T>(IntPtr)` overload avoids the box entirely
and has been available since .NET 4.5.1 / netstandard2.0. On top of the box,
`ConnectionInfo` declares two `[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)] string` fields, so each
delivery also allocates two strings.

**Evidence:**

```csharp
// Dispatch.cs:301-305
list.Add( new Callback
{
    action = x => p( x.ToType<T>() ),
    server = server
} );
```

```csharp
// Utility.cs:20
return (T)Marshal.PtrToStructure( ptr, typeof( T ) );   // boxes
```

```csharp
// Networking/ConnectionInfo.cs:20-23
[MarshalAs( UnmanagedType.ByValTStr, SizeConst = 128 )]
internal string endDebug;
[MarshalAs( UnmanagedType.ByValTStr, SizeConst = 128 )]
internal string connectionDescription;
```

**PROVEN** (harness TEST A — 100,000 iterations of the exact `Marshal.PtrToStructure(buf, t)` call the
closure makes, measured with `GC.GetAllocatedBytesForCurrentThread()` against a private `AllocHGlobal`
buffer, no Steam involved):

```
SteamNetConnectionStatusChangedCallback_t      Marshal.SizeOf=  712  bytes alloc/callback = 320.0
LobbyChatMsg_t                                 Marshal.SizeOf=   24  bytes alloc/callback =  40.0
PersonaStateChange_t                           Marshal.SizeOf=   16  bytes alloc/callback =  32.0
SteamAPICallCompleted_t                        Marshal.SizeOf=   16  bytes alloc/callback =  32.0
```

This is **per registered handler** — `ProcessCallback` invokes the closure once per entry in `actionsToCall`.

**Failure scenario:** A 100-player server where a map change disconnects and reconnects everyone gets
100 × (1 + N handlers) × 320 B of gen-0 garbage in a burst, plus 200 short-lived strings. Every
`SteamAPICallCompleted_t` also costs 32 B via `ProcessResult`'s `msg.Data.ToType<SteamAPICallCompleted_t>()`
(`Dispatch.cs:203`). On IL2CPP with a conservative GC this is the kind of thing that shows up as a
frame-time spike on lobby joins.

**Recommended fix:** change `Utility.ToType<T>` to `Marshal.PtrToStructure<T>( ptr )` (allocation-free for
blittable structs; still allocates the strings for `ConnectionInfo`). Consider exposing the string fields
lazily rather than marshalling both on every status change.

---

### F10 — MEDIUM — `Helpers.TakeBuffer` hands the same array to overlapping callers

**Location:** `Facepunch.Steamworks/Utility/Helpers.cs:59-85`; 8 call sites

**What's wrong:** a round-robin over 4 arrays with no ownership tracking. The `lock` protects the index bump
and nothing else — the array is returned to the caller and used outside the lock. The 5th overlapping borrow
aliases the 1st. The source comment already admits "We shouldn't really be using this anymore."

**Evidence:**

```csharp
// Helpers.cs:66-85
public static byte[] TakeBuffer( int minSize )
{
    lock ( BufferPool  )
    {
        BufferPoolIndex++;
        if ( BufferPoolIndex >= BufferPool.Length ) BufferPoolIndex = 0;
        if ( BufferPool[BufferPoolIndex] == null )  BufferPool[BufferPoolIndex] = new byte[1024 * 256];
        if ( BufferPool[BufferPoolIndex].Length < minSize ) BufferPool[BufferPoolIndex] = new byte[minSize + 1024];
        return BufferPool[BufferPoolIndex];
    }
}
```

**PROVEN** (harness TEST 6, calling the real `Helpers.TakeBuffer` via reflection):

```
5 sequential TakeBuffer(1024) calls, distinct arrays = 4 of 5
buffer[0] and buffer[4] same object?                 = True   <-- pool size is 4
my buffer[0] after 4 unrelated TakeBuffer calls      = 99   (42 == safe, 99 == silently overwritten)
```

Most call sites copy out immediately and are safe (`SteamNetworking.cs:95`, `SteamUtils.cs:114`,
`SteamUser.cs:317`, `SteamServer.cs:487`). One deliberately does not:

```csharp
// SteamServer.cs:434 (doc), 438, 451
/// <param name="packet">Packet to send. The Data passed is pooled - so use it immediately.</param>
var buffer = Helpers.TakeBuffer( 1024 * 32 );
...
packet.Data = buffer;      // pooled array escapes to the caller
```

**Failure scenario:** A dedicated server's query thread loops
`while (SteamServer.GetOutgoingPacket(out var p)) socket.SendTo(p.Data, p.Size, ...)` while the main thread
services a `SteamFriends` avatar request (`SteamUtils.GetImage` → `TakeBuffer`) and a voice decompress
(`SteamUser.cs:240-241`, two borrows). Four intervening borrows and the outgoing packet's backing array is
overwritten between `GetOutgoingPacket` and `SendTo` — the server sends corrupted A2S responses. Silent,
intermittent, and it looks like a network problem.

**Recommended fix:** replace with `ArrayPool<byte>.Shared.Rent/Return`, or at minimum have
`GetOutgoingPacket` copy into a caller-owned array. Grow the pool and/or make ownership explicit
(`IDisposable` lease) if the allocation-free behaviour must be kept.

---

### F11 — MEDIUM — 128 KB static voice buffer shared by two public APIs with no lock

**Location:** `Facepunch.Steamworks/SteamUser.cs:158`, used at `SteamUser.cs:175` and `SteamUser.cs:202`

**Evidence:**

```csharp
// SteamUser.cs:158
static byte[] readBuffer = new byte[1024*128];
```

```csharp
// SteamUser.cs:175-181 (ReadVoiceData)   and   SteamUser.cs:202-208 (ReadVoiceDataBytes)
fixed ( byte* b = readBuffer )
{
    if ( Internal.GetVoice( true, (IntPtr)b, (uint)readBuffer.Length, ref szWritten, ... ) != VoiceResult.OK )
        return 0;
}
...
stream.Write( readBuffer, 0, (int) szWritten );   // read outside the `fixed`, no lock
```

**What's wrong:** no synchronisation at all. Two threads (or one thread interleaving `ReadVoiceData` with a
callback-driven `ReadVoiceDataBytes`) corrupt each other's audio frames. Note the `fixed` block ends before
`stream.Write`, so even the pin does not span the read.

**Failure scenario:** Voice capture runs on an audio thread while a `SteamNetworking` callback on the pump
thread (F-B: a *different* thread on dedicated servers) calls `ReadVoiceDataBytes()`. Result: garbled/mixed
voice packets, non-deterministic.

**Recommended fix:** `[ThreadStatic]` buffer, or lock, or `ArrayPool`.

---

### F12 — MEDIUM — `SocketInterfaces` / `ConnectionInterfaces` are append-only; entries are never removed

**Location:** `Facepunch.Steamworks/SteamNetworkingSockets.cs:45` and `:66`

**What's wrong:** both dictionaries are `static readonly` with a `Set*` but **no `Remove` and no `Clear`
anywhere in the assembly** (verified by full-repo grep — the only mutation sites are the ten
`SetSocketManager`/`SetConnectionManager` calls at lines 144, 170, 182, 200, 216, 241, 254, 273, 323, 348,
plus `Socket.cs:26`). `SocketManager.Close()` (`Networking/SocketManager.cs:33-44`) destroys the poll group
and the socket but leaves the registry entry, holding the `SocketManager` — and its `Connecting`/`Connected`
`HashSet<Connection>` — alive forever.

**Evidence:**

```csharp
// SteamNetworkingSockets.cs:45, 58-62
static readonly Dictionary<uint, SocketManager> SocketInterfaces = new Dictionary<uint, SocketManager>();
internal static void SetSocketManager( uint id, SocketManager manager )
{
    if ( id == 0 ) throw new System.ArgumentException( "Invalid Socket" );
    SocketInterfaces[id] = manager;
}
```

```csharp
// Networking/SocketManager.cs:33-44 — no deregistration
public bool Close()
{
    if ( SteamNetworkingSockets.Internal.IsValid )
    {
        SteamNetworkingSockets.Internal.DestroyPollGroup( pollGroup );
        Socket.Close();
    }
    pollGroup = 0;
    Socket = 0;
    return true;
}
```

**Failure scenario:** Two problems, one leak and one correctness. (a) A matchmaking client that connects to
30 servers over a session retains 30 dead `ConnectionManager`s plus their `ConnectionInfo` (712 B marshalled,
including two 128-char strings each). (b) Steam recycles `HSteamNetConnection` ids. When a new connection
reuses id N, `SetConnectionManager` overwrites — fine — but a `SteamNetConnectionStatusChangedCallback_t`
that was queued for the *old* connection with id N and processed after the overwrite is routed to the **new**
manager, which sees a spurious `ClosedByPeer` and tears down a healthy connection.

**Recommended fix:** remove the entry in `SocketManager.Close()` / when a connection reaches
`ConnectionState.None` in `SteamNetworkingSockets.ConnectionStatusChanged`, and clear both dictionaries in
`DestroyInterface`.

---

### F13 — MEDIUM — `GetAuthSessionTicketAsync` subscribes a handler that dereferences a field before it is assigned

**Location:** `Facepunch.Steamworks/SteamUser.cs:341-357` (and the identical shape at `SteamUser.cs:406-423`)

**Evidence:**

```csharp
// SteamUser.cs:343-357
var result = Result.Pending;
AuthTicket ticket = null;                       // still null
...
void f( GetAuthSessionTicketResponse_t t )
{
    if ( t.AuthTicket != ticket.Handle ) return;   // <-- NRE if this runs before line 357
    result = t.Result;
}

OnGetAuthSessionTicketResponse += f;               // subscribed here

try
{
    ticket = GetAuthSessionTicket( identity );     // assigned here
```

**What's wrong:** the handler is live on the dispatch pump before `ticket` is non-null. On a single-threaded
pump the two statements cannot interleave, so this looks safe. But PROVEN (TEST B) the pump runs on a
*different* thread whenever `SynchronizationContext.Current` is null at `Init` — so a
`GetAuthSessionTicketResponse_t` arriving in that window (e.g. from a previous ticket, or a concurrent call
to this same method) dereferences `null` **inside the pump**, which then trips F5 and silently kills the rest
of that callback's handlers.

There is a second, thread-independent bug in the same handler: it is a plain closure over locals with no
memory barrier, and the polling loop reads `while ( result == Result.Pending )` (`SteamUser.cs:361`) from
another thread without `volatile`/`Interlocked`. That read can be hoisted.

**Recommended fix:** capture the handle into a local before subscribing (or subscribe after
`ticket = GetAuthSessionTicket(...)` and re-check), and use `Volatile.Read`/`Interlocked` for `result`.

---

### F14 — MEDIUM — Two `AllocHGlobal(1024)` calls freed on the straight-line path only

**Location:** `Facepunch.Steamworks/SteamUser.cs:541` (freed at `:551`) and `SteamUser.cs:572` (freed at `:582`)

**Evidence:**

```csharp
// SteamUser.cs:541-553 (inside RequestEncryptedAppTicketAsync(byte[]))
var ticketData = Marshal.AllocHGlobal( 1024 );
uint outSize = 0;
byte[] data = null;

if ( Internal.GetEncryptedAppTicket( ticketData, 1024, ref outSize ) )
{
    data = new byte[outSize];
    Marshal.Copy( ticketData, data, 0, (int) outSize );   // throws if outSize > 1024
}

Marshal.FreeHGlobal( ticketData );                        // skipped on throw
return data;
```

The enclosing `finally` only covers `dataPtr` (`SteamUser.cs:555-558`). The parameterless overload at
`SteamUser.cs:567-584` has the same shape with **no** `try/finally` at all.

**What's wrong:** if `GetEncryptedAppTicket` throws, or `outSize` exceeds 1024 and `Marshal.Copy` throws
`ArgumentOutOfRangeException`, 1024 bytes of unmanaged memory leak per call. Note also there is no bounds
check on `outSize` before `Marshal.Copy` — the code trusts native to respect the 1024 limit.

**Failure scenario:** A game that calls `RequestEncryptedAppTicketAsync()` on a retry loop when the backend
is misconfigured leaks 1 KB per failed attempt. Small, but unbounded and invisible to managed profilers.

**Recommended fix:** `try/finally` around both, and validate `outSize <= 1024` before copying.

---

### F15 — MEDIUM — 62 `public static event`s are never cleared; subscriptions survive Shutdown and Unity domain reloads

**Location:** all `Steam*.cs` (full inventory: `SteamApps.cs:37,45`; `SteamFriends.cs:48,53,58,65,71,77,83,88,94`;
`SteamInventory.cs:47,48`; `SteamMatchmaking.cs:91-146`; `SteamMusic.cs:38,43`;
`SteamNetworkingSockets.cs:114,128`; `SteamNetworkingUtils.cs:44`; `SteamParental.cs:35`;
`SteamParties.cs:39,44`; `SteamRemotePlay.cs:36,41`; `SteamScreenshots.cs:44,49,54`;
`SteamServer.cs:42,48,53,58,63`; `SteamUgc.cs:41,46,47,48`; `SteamUser.cs:56-121`; `SteamUserStats.cs:53-71`;
`SteamUtils.cs:45,51,56,61`), plus `Utility/SteamInterface.cs:86-97,117-120,140-143`

**What's wrong:** the three `DestroyInterface` overrides — the **only** three in the assembly — null the
interface pointer holder and nothing else:

```csharp
// Utility/SteamInterface.cs:117-120
internal override void DestroyInterface( bool server )
{
    Interface = null;
}
```

No event is ever set to `null`, so a subscriber that has been destroyed (a Unity `MonoBehaviour` from the
previous play session, a disposed UI panel) stays on the invocation list into the next `Init`. Combined with
F4 (duplicate registrations) and F5 (swallowed exceptions), the resulting `MissingReferenceException`s are
invisible.

**Recommended fix:** either null the events in `DestroyInterface`, or (better, for Unity) provide an explicit
`Dispatch.Reset()`/`SteamClient.ResetEvents()` and document that callers must unsubscribe. Note that nulling
static events is a behaviour change and should be a deliberate, documented decision.

---

### F16 — MEDIUM — `ServerList.Base.Dispose()` throws after `SteamClient.Shutdown()`, and the native request handle leaks

**Location:** `Facepunch.Steamworks/ServerList/Base.cs:110`, `:141-149`, `:151-154`

**Evidence:**

```csharp
// ServerList/Base.cs:22
internal static ISteamMatchmakingServers Internal => SteamMatchmakingServers.Internal;   // null after DestroyInterface

// ServerList/Base.cs:110
public virtual void Cancel() => Internal.CancelQuery( request );

// ServerList/Base.cs:141-154
void ReleaseQuery()
{
    if ( request.Value != IntPtr.Zero )
    {
        Cancel();                            // NRE if Internal is null
        Internal.ReleaseRequest( request );
        request = IntPtr.Zero;
    }
}
public virtual void Dispose() { ReleaseQuery(); }
```

**What's wrong:** `SteamMatchmakingServers.Internal` is `Interface as ISteamMatchmakingServers`, and
`SteamClientClass<T>.DestroyInterface` sets `Interface = null`. Any `ServerList` object still alive at
shutdown throws `NullReferenceException` out of `Dispose()`. If `Dispose` is in a `using`, the NRE replaces
whatever exception was in flight; if it is in a finalizer-adjacent teardown path it takes the shutdown with
it. Either way the native `HServerListRequest` is never released.

Related: `RunQueryAsync` returns early at `Base.cs:82` (`CancelledOrChangedRequest`) and `Base.cs:85`
(`InvalidClient`) without releasing the request; it is only released by the next `Reset()` or by `Dispose()`.
The README example (`README.md:79-89`) does use `using`, so this is a latent rather than routine leak.

**Recommended fix:** null-guard `Internal` in `ReleaseQuery`/`Cancel`, and have `SteamClient.Cleanup()`
either release outstanding requests or leave `SteamMatchmakingServers.Interface` valid until after user
disposal.

---

### F17 — LOW — `SteamAPI_ManualDispatch_FreeLastCallback` is declared as returning `bool`; the SDK declares it `void`

**Location:** `Facepunch.Steamworks/Classes/Dispatch.cs:49-51` vs `Generator/steam_sdk/steam_api.h:222`

**Evidence:**

```csharp
// Dispatch.cs:49-51
[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ManualDispatch_FreeLastCallback", CallingConvention = CallingConvention.Cdecl )]
[return: MarshalAs( UnmanagedType.I1 )]
internal static extern bool SteamAPI_ManualDispatch_FreeLastCallback( HSteamPipe pipe );
```

```c
/* steam_api.h:222 */
S_API void S_CALLTYPE SteamAPI_ManualDispatch_FreeLastCallback( HSteamPipe hSteamPipe );
```

**What's wrong:** the managed declaration reads a return register the native function never wrote. Harmless
on Windows x64 and SysV AMD64 cdecl (the value is discarded and the caller cleans the stack), but it is a
signature mismatch against ground truth and would be a real problem on any ABI where return-value handling
differs, or if a future runtime validates signatures.

**Recommended fix:** declare it `void` and drop the `[return: MarshalAs]`.

---

### F18 — LOW — `stackalloc long[0]` is handed to native `SendMessages`; currently NULL on CoreCLR but not guaranteed

**Location:** `Facepunch.Steamworks/Networking/ConnectionManager.cs:189`, used at `:201`

**Evidence:**

```csharp
// ConnectionManager.cs:188-201
var messages = stackalloc NetMsg*[connectionCount];
var messageNumberOrResults = stackalloc long[results != null ? connectionCount : 0];
...
SteamNetworkingSockets.Internal.SendMessages( connectionCount, messages, messageNumberOrResults );
```

The SDK contract (`isteamnetworkingsockets.h:295-302`) makes `pOutMessageNumberOrResult` optional — native
tests it for NULL and, if non-NULL, writes `nMessages` × `int64`. A non-NULL zero-length stack buffer would
therefore be a stack smash.

**PROVEN SAFE on this runtime** (harness TEST 1, using a JIT-opaque `null` so the size cannot be
const-folded):

```
results == null                    = True
stackalloc long[0] pointer         = 0x0
is NULL (native will skip writing) = True
stackalloc long[4] pointer         = 0xA9FE57EA20 (non-null, as expected)
```

**What's wrong:** RyuJIT lowers `localloc 0` to a null pointer, so this happens to be correct today on
CoreCLR x64. ECMA-335 leaves the address returned by `localloc` with size 0 unspecified, and Mono/IL2CPP —
the runtimes this library actually ships on for Unity — are not covered by this test. This is a correctness
guarantee resting on an unstated JIT implementation detail.

**Recommended fix:** make the intent explicit — `long* p = results != null ? stackalloc long[connectionCount] : null;`
(or an `if`/`else` around the two call shapes).

---

### F19 — LOW — `[MonoPInvokeCallback]` is the library's own no-op attribute, not Unity's AOT attribute

**Location:** `Facepunch.Steamworks/Utility/Helpers.cs:116-119`; applied at
`Facepunch.Steamworks/SteamNetworkingUtils.cs:370` and `Facepunch.Steamworks/Networking/BroadcastBufferManager.cs:91`

**Evidence:**

```csharp
// Helpers.cs:116-119
internal class MonoPInvokeCallbackAttribute : Attribute
{
    public MonoPInvokeCallbackAttribute() { }
}
```

**What's wrong:** IL2CPP/Mono-AOT recognise `UnityEngine.AOT.MonoPInvokeCallbackAttribute` **with the
delegate type as a constructor argument** (`[MonoPInvokeCallback(typeof(NetDebugFunc))]`). A same-named
attribute in the `Steamworks` namespace with a parameterless constructor satisfies neither condition, so the
AOT compiler has no signal to emit a reverse-P/Invoke wrapper for these two methods.

Both are genuinely invoked from native: `OnDebugMessage` from Steam's logging thread, and
`BufferManager.Free` from `SendMessages`' free callback — which the SDK documents
(`isteamnetworkingsockets.h:282-283`) "can be invoked at any time from any thread (perhaps even before
SendMessages returns!)". A missing AOT wrapper here is a hard crash on IL2CPP builds, not a degradation.

**Status:** INFERRED — I cannot build an IL2CPP player offline to confirm whether Unity's linker rescues
this. The fix is free.

**Recommended fix:** `#if UNITY_5_3_OR_NEWER` alias to `AOT.MonoPInvokeCallbackAttribute` and pass the
delegate type; keep the local shim only for non-Unity builds. Also note the delegate roots themselves are
correct (see Verified-correct).

---

### F20 — LOW — Handle/state caches survive Shutdown and poison the next Init

**Location:** `SteamServer.cs:372` (`KeyValue`), `SteamInput.cs:105,116,127` (`DigitalHandles`,
`AnalogHandles`, `ActionSets`), `SteamInventory.cs:162` (`_defMap`), `SteamUserStats.cs:25` (`StatsRecieved`),
`SteamNetworkingUtils.cs:49` (`Status`), `SteamClient.cs:195` (`AppId`)

**Evidence (worst case):**

```csharp
// SteamServer.cs:372, 380-395
static Dictionary<string, string> KeyValue = new Dictionary<string, string>();
public static void SetKey( string Key, string Value )
{
    if ( KeyValue.ContainsKey( Key ) )
    {
        if ( KeyValue[Key] == Value )
            return;                       // <-- early-out based on the PREVIOUS session's state
        KeyValue[Key] = Value;
    }
    ...
    Internal.SetKeyValue( Key, Value );
}
```

**Failure scenario:** A dedicated server restarts its Steam session between maps and re-applies the same
`SetKey("gamemode","survival")`. The dictionary still holds it from the previous session, so `SetKey` returns
without calling `Internal.SetKeyValue` — the new Steam session never learns the tag and the server appears
in the browser with no gamemode filter. Same shape for `SteamInput`'s handle caches, which will hand out
handles from a dead Steam session.

**Recommended fix:** clear these in the corresponding `DestroyInterface`/`Shutdown`.

---

### F21 — LOW — Native buffer pools are never drained at shutdown

**Location:** `Utility/Helpers.cs:15` (`BufferBag`, up to 4 × 32 KB `AllocHGlobal`),
`Networking/BroadcastBufferManager.cs:57` (`BufferPools` — up to 1024×512 B + 512×1 KB + 128×4 KB +
32×16 KB + 16×64 KB + 8×256 KB ≈ 6 MB of pooled `AllocHGlobal`), `:60` (`ReferenceCounters`)

**What's wrong:** these are process-lifetime caches of unmanaged memory with no shutdown hook. Not a leak in
the growing sense (both are capped), but ~6 MB of unmanaged memory stays resident after `SteamClient.Shutdown()`,
and any `ReferenceCounters` entry whose native free callback never fires is permanently retained.

**Recommended fix:** a `BufferManager.Shutdown()` / `Helpers.DrainBuffers()` called from
`SteamClient.Cleanup()`.

---

### F22 — LOW — `Dispatch.OnDebugCallback` and `Dispatch.OnException` are fields, not events

**Location:** `Facepunch.Steamworks/Classes/Dispatch.cs:28`, `:36`

```csharp
public static Action<CallbackType, string, bool> OnDebugCallback;
public static Action<Exception> OnException;
```

A second consumer writing `Dispatch.OnException = ...` silently replaces the first. The same pattern appears
at `SteamNetworking.cs:38,45` and `SteamNetworkingMessages.cs:40,42`. Declare them `event` so `+=` is the
only option.

---

### F23 — LOW — `BufferManager.Get` reference is leaked if anything throws before `SendMessages`

**Location:** `Facepunch.Steamworks/Networking/Connection.cs:79-91`,
`Facepunch.Steamworks/Networking/ConnectionManager.cs:185-201`

**Evidence:**

```csharp
// ConnectionManager.cs:185-201
var copyPtr = BufferManager.Get( size, connectionCount );   // refcount = connectionCount
Buffer.MemoryCopy( (void*)ptr, (void*)copyPtr, size, size );

var messages = stackalloc NetMsg*[connectionCount];
...
for ( var i = 0; i < connectionCount; i++ )
{
    messages[i] = SteamNetworkingUtils.AllocateMessage();    // can return null if the interface is gone
    messages[i]->Connection = connections[i];                // -> AccessViolation, refcount never decremented
    ...
}
SteamNetworkingSockets.Internal.SendMessages( connectionCount, messages, messageNumberOrResults );
```

The only decrement path is Steam invoking `BufferManager.FreeFunctionPointer`. If the loop throws, the
pooled native buffer is orphaned in `ReferenceCounters` forever. Wrap in `try/catch` and decrement the
outstanding references on the failure path.

---

## Verified-correct areas

These were specifically suspected and checked; they hold.

**1. `SteamAPI_ManualDispatch_FreeLastCallback` is called on every path, including exceptions.**
`Dispatch.cs:100-107` places the call in a `finally` around `ProcessCallback`. An exception from any user
handler still frees the native message before unwinding. This matches Valve's requirement at
`steam_api.h:217-222`. (The consequences of the unwind are F5, but the native message is not leaked.)

**2. `CallbackMsg_t` marshalling is byte-exact against native x64.** PROVEN (TEST 2):

```
Marshal.SizeOf = 24
  m_hSteamUser offset=0  type=HSteamUser
  Type         offset=4  type=CallbackType   (underlying Int32)
  Data         offset=8  type=IntPtr
  DataSize     offset=16 type=Int32
```

matches `steam_api_internal.h:192-198` (`HSteamUser`/`int`/`uint8*`/`int`) under `#pragma pack(push, 8)`
and `Platform.StructPlatformPackSize == 8` for WIN64.

**3. The `CallResult<T>` continuation resumes inline on the dispatch thread**, so `GetAPICallResult` runs
*before* `FreeLastCallback`. This was the highest-risk assumption in the whole design and it holds: because
`CallResult<T>` implements only `INotifyCompletion` (not a `TaskAwaiter`), the async builder passes a raw
completion action that restores the `ExecutionContext` but does **not** post to a `SynchronizationContext`.
PROVEN (TEST C, with a single-threaded `SynchronizationContext` installed):

```
'ProcessResult' thread = 8 ; continuation body ran on thread = 8
inline on the dispatch thread? True
```

**4. `Install` / `ShutdownClient` / `ShutdownServer` are symmetric when the `server` flag matches.**
PROVEN (TEST 5): Install → Shutdown → Install leaves exactly 1 handler on both the client and server paths.
The only asymmetry is F4's mis-parameterised caller.

**5. No `GCHandle` anywhere in the assembly.** Verified by exhaustive sweep — the only `GCHandle`
substring hits are the unrelated generated type `UGCHandle_t`. Pinning is done exclusively with `fixed`,
whose scope is statically checked. Consequently the classic *"ISteamMatchmakingServers response object gets
collected and Steam calls into freed memory"* crash cannot occur here: `ServerList/Internet.cs:10` and its
siblings pass `IntPtr.Zero` for `pRequestServersResponse` and poll instead
(`ServerList/Base.cs:174-193` `UpdateResponsive`, backed by the allocation-free
`Interfaces/ISteamMatchmakingServers.cs:20-40` `HasServerResponded`). See Open questions for the tradeoff.

**6. Delegates handed to native are correctly rooted.** `SteamNetworkingUtils._debugFunc`
(`SteamNetworkingUtils.cs:357`) is assigned to a static *before* `SetDebugOutputFunction`
(`:343-345`), and `BufferManager.FreeFunctionPin` (`BroadcastBufferManager.cs:63`) is a
`static readonly` field holding the delegate whose function pointer is taken at `:65`. Neither can be
collected while native holds the pointer.

**7. All string and scratch-memory marshalling is scope-bound.** Verified exhaustively: all **240**
`Utf8StringToNative` constructions are `using var`, and all **44** `Helpers.Memory.Take()` /
`Helpers.TakeMemory()` call sites are `using var`. `Utf8StringToNative` (`Utility/Utf8String.cs:22/36`),
`ServerFilterMarshaler` (`ServerList/ServerFilterMarshaler.cs:29-30/48-55`),
`CallResult<T>.GetResult` (`Callbacks/CallResult.cs:55/71`),
`SocketManager.Receive` (`Networking/SocketManager.cs:129/142`) and
`SteamInventory.Deserialize` (`SteamInventory.cs:283/300`) all pair alloc/free in `try/finally` or `using`.

**8. Call-result *failure* is surfaced, not hung.** `CallResult<T>.GetResult` (`Callbacks/CallResult.cs:47-73`)
returns `null` when `IsAPICallCompleted` reports failure, and `IsCompleted` (`:78-88`) returns `true` on
failure so the awaiter resolves. Callers correctly test `result.HasValue`. The only hang is F2 (the
completion event never arriving at all).

**9. The single `CallbackType` id collision is benign.** PROVEN (TEST 7): of 216 registered callback
structs, exactly one id is shared — `1108 => UserStatsUnloaded, GSStatsUnloaded`. This mirrors Valve's own
reuse, and because `ProcessCallback` partitions handlers by `item.server != isServer`
(`Dispatch.cs:148-149`), a client-pipe `UserStatsUnloaded_t` never reaches a gameserver `GSStatsUnloaded_t`
handler and vice versa.

**10. `SteamGameServer_RunCallbacks` is never called.** Valve forbids mixing it with manual dispatch
(`steam_api.h:172-175`). The P/Invoke exists at `Classes/SteamGameServer.cs:14-15` and the wrapper at `:24-27`,
but repo-wide grep shows no caller — the wrapper is dead code. Consider deleting it so nobody wires it up.

**11. `OnDebugCallback` costs nothing when unset.** `Dispatch.cs:133` uses `?.Invoke( ..., CallbackToString(...), ... )`.
C# null-conditional invocation short-circuits argument evaluation, so the expensive reflection-based
`CallbackToString` (`Dispatch.cs:166-196`, which does `GetFields` + LINQ `Max` + per-field string
concatenation on every callback) is not executed when the hook is null. PROVEN with the identical
`Action<int,string,bool>` shape:

```
Hook null -> Expensive() invocations = 0  (0 == short-circuits)
Hook set  -> Expensive() invocations = 1
```

This matters because the doc comment at `Dispatch.cs:20-27` warns the hook "is SLOW!!" — the warning is
accurate when enabled and correctly free when not.

**12. `ConnectionManager.Receive` releases messages on the exception path.**
`Networking/ConnectionManager.cs:130-141` releases every non-null `NetMsg*` in the batch before rethrowing;
`ReceiveMessage` (`:252-266`) uses `finally`. `SteamNetworkingMessages.cs:113,144,183,205` follow the same
pattern. `SocketManager.ReceiveMessage` (`Networking/SocketManager.cs:155-169`) likewise.

**13. `BufferManager` is genuinely thread-safe for the native free callback.** The SDK warns
(`isteamnetworkingsockets.h:282-283`) that the free callback "can be invoked at any time from any thread".
`Free` (`BroadcastBufferManager.cs:92-132`) takes `lock (ReferenceCounters)` and uses
`Interlocked.Decrement`, with an explicit double-free guard at `:41-45`.

---

## Open questions requiring a live Steam client

1. **Is passing `IntPtr.Zero` as `ISteamMatchmakingServerListResponse*` safe?**
   `ServerList/Internet.cs:10`, `Friends.cs`, `Favourites.cs`, `History.cs` all pass `IntPtr.Zero`. The SDK
   header (`isteammatchmaking.h:396`) does not document NULL as permitted, and Valve's implementation calls
   `pRequestServersResponse->ServerResponded(...)` / `RefreshComplete(...)`. This design elegantly
   sidesteps the classic delegate-lifetime crash, but if Steam does not null-check, it is an immediate
   null-vtable dereference the moment a server responds. Needs one live `ServerList.Internet().RunQueryAsync()`
   to settle. (The presence of `Facepunch.Steamworks.Test/ServerlistTest.cs` suggests it has been exercised,
   but that cannot be confirmed offline.)

2. **Do client and gameserver pipes share the `SteamAPICall_t` handle space?** This decides whether F7 is a
   latent design smell or an active listen-server defect. Test: init both, issue an awaited call on each,
   and log the two handle values.

3. **Does `ISteamUtils::IsAPICallCompleted` / `GetAPICallResult` behave correctly under manual dispatch?**
   `Callbacks/CallResult.cs:50,59,83` uses the classic API, while Valve's manual-dispatch documentation
   (`steam_api.h:224-226`) directs you to `SteamAPI_ManualDispatch_GetAPICallResult` and warns "You really
   should only call this in a handler for `SteamAPICallCompleted_t`". The library does call it from within
   that handler (verified item 3 above), so this is probably fine, but only a live run confirms that the
   classic path is not silently degraded in manual-dispatch mode.

4. **Is `SteamAPI_ManualDispatch_Init()` idempotent?** It is called from both `SteamClient.Init`
   (`SteamClient.cs:62`) and `SteamServer.Init` (`SteamServer.cs:107`), so a listen server calls it twice.
   The header (`steam_api.h:209-211`) says only "must be called after `SteamAPI_Init`".

5. **Does `SteamAPI_ManualDispatch_GetNextCallback` ever return a `m_cubParam` smaller than this binding's
   `sizeof(T)`?** This is what turns F8 from a hardening item into an active over-read. Instrument
   `ProcessCallback` to log `msg.Type`, `msg.DataSize` and `default(T).DataSize` for every callback across a
   full session and diff.

6. **Does IL2CPP successfully emit reverse-P/Invoke wrappers for `OnDebugMessage` and `BufferManager.Free`
   without the real `AOT.MonoPInvokeCallbackAttribute`?** (F19.) Requires an IL2CPP player build plus a
   `SendMessages` broadcast to trigger the free callback.

---

## Harness

The offline proofs above were produced by a throwaway console project referencing
`Facepunch.Steamworks/bin/Release/net6.0/Facepunch.Steamworks.Win64.dll` and driving the internals by
reflection. It touches no native Steam entry point. Location (session scratchpad, not part of the repo):

```
%LOCALAPPDATA%\Temp\claude\C--Users-admin-Desktop-Claude-Cowork-Global-Facepunch-Steamworks\
  a40aa6e1-0fed-4b76-b07a-e6559244c52d\scratchpad\probe\
    probe.csproj
    Program.cs   (TEST 1-7)
    Probe2.cs    (TEST A-F)
```

Reproduce with `dotnet run -c Release` after building
`Facepunch.Steamworks/Facepunch.Steamworks.Win64.csproj -c Release`.
