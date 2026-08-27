# Getting Started — initialising Steam, and why it fails

This guide assumes you have never shipped a Steam game. It explains not just *what* to
call but *why*, and it covers the failure modes that Valve's documentation glosses over.

If you only read one thing: **`SteamClient.Init` throwing is normal and you must handle
it.** Steam not running is not an exceptional situation — it is Tuesday.

---

## 1. The mental model

Steamworks is not a web API. It is a local IPC channel to the Steam client process
already running on the player's machine.

```
Your game process                    Steam client process
┌────────────────────┐               ┌────────────────────┐
│ Facepunch.Steamwks │               │                    │
│        ↓           │               │   steam.exe        │
│   steam_api64.dll  │ ◄── IPC ────► │   (must already    │
│                    │               │    be running)     │
└────────────────────┘               └────────────────────┘
```

Three consequences that catch everyone out:

1. **If Steam is not running, nothing works.** There is no offline mode, no fallback, no
   retry that helps. `Init` fails and that is the correct outcome.
2. **Steam must know which game you are.** It identifies you by AppID. Getting that
   wrong is the single most common setup failure — see §3.
3. **Results arrive later, on a pump you control.** Steam does not call you back
   spontaneously. You get answers only when callbacks are pumped. See §5.

---

## 2. The minimum viable integration

```csharp
using Steamworks;

const uint AppId = 480; // 480 = Spacewar, Valve's public test app. Replace with yours.

try
{
    SteamClient.Init( AppId );
}
catch ( Exception e )
{
    // Steam isn't running, the player doesn't own the game, steam_appid.txt is wrong,
    // or the DLL couldn't be loaded. Your game must still be able to start.
    Console.WriteLine( $"Steam unavailable: {e.Message}" );
    RunWithoutSteam();
    return;
}

Console.WriteLine( $"Logged in as {SteamClient.Name} ({SteamClient.SteamId})" );

// ... your game runs ...

SteamClient.Shutdown();
```

That is genuinely all that is required. Everything else is optional.

### Do not let a Steam failure kill your game

The single most common shipping bug is treating `Init` as infallible. Players run games
from a desktop shortcut, from a debugger, with Steam restarting in the background, or
while Steam is in offline mode. Decide up front which of these you support:

| Policy | When it's right |
|---|---|
| Continue without Steam features | Singleplayer games, tools, anything with a non-Steam build |
| Show an error and quit cleanly | Multiplayer games where Steam identity is mandatory |
| Relaunch through Steam | You want the overlay and DRM; see `SteamClient.RestartAppIfNecessary` |

Never: catch the exception and carry on as if Steam initialised. Every later Steam call
will then fail in a harder-to-diagnose way.

---

## 3. AppID: the number one source of setup pain

Steam has to know which app your process is. There are two ways it can find out, and
mixing them up produces confusing failures.

**During development**, put a file called `steam_appid.txt` next to your executable
containing *only* the AppID, no newline, no BOM, no quotes:

```
480
```

With that file present, you can launch your game directly from your IDE and Steam will
accept it.

**In a shipped build**, you normally *delete* `steam_appid.txt` and let Steam tell your
process which app it is when it launches you. If you ship `steam_appid.txt`, any player
can edit it and impersonate a different app.

Things that go wrong here:

- **The file is in the wrong directory.** It must sit beside the *executable*, which for
  .NET means the build output folder, not the project folder. In Unity's editor it is the
  project root; in a Unity player build it is beside the player executable.
- **The file has a UTF-8 BOM or a trailing newline.** Some editors add these silently.
  Write it with an editor you trust, or from code.
- **The AppID does not match the one you are logged in as owning.** You must own the app
  on the Steam account currently logged in. Use 480 (Spacewar) while prototyping — every
  account can use it.

> **Undocumented behaviour worth knowing:** this library also sets the `SteamAppId` and
> `SteamGameId` environment variables for the current process during `Init`. That is why
> `Init` sometimes succeeds in situations where the raw C++ API would not. Do not rely on
> it as a substitute for correct `steam_appid.txt` handling.

---

## 4. Reading the failure

`Init` throws a plain `System.Exception` whose message contains the underlying
`SteamAPIInitResult` and Steam's own error text. The results mean:

| Result | What it actually means | What to do |
|---|---|---|
| `OK` | Success. | — |
| `FailedGeneric` | Usually: Steam is not running, or is not logged in. | Ask the player to start Steam. |
| `NoSteamClient` | The Steam client could not be reached at all. | Same as above. Check Steam is installed. |
| `VersionMismatch` | The `steam_api` binary does not match the running Steam client. | Ship the `steam_api` binaries from the SDK version this library targets. |

The exception message is the most useful diagnostic you will get — log it verbatim. Steam
writes further detail to its own logs, and to `%steam_install%\logs\`.

> **A rough edge to be aware of.** `Init` throws a bare `System.Exception`, so you cannot
> `catch` a Steam-specific type without catching everything. Until that is improved, catch
> `Exception` at the call site and do not try to parse the message programmatically —
> match on behaviour (did `Init` return normally?) rather than on text.

---

## 5. Callbacks: the part everyone gets wrong

Almost every interesting Steam operation is asynchronous. You ask a question, and the
answer arrives later — but only while callbacks are being *pumped*.

`Init` takes a second parameter that decides who does the pumping:

```csharp
SteamClient.Init( AppId );                          // asyncCallbacks: true  (the default)
SteamClient.Init( AppId, asyncCallbacks: false );   // you pump it yourself
```

### `asyncCallbacks: true` (default)

The library starts a background loop that pumps callbacks roughly every 16 ms. You do not
have to do anything.

**The catch:** your event handlers and `async` continuations may then run on that
background thread, not your main thread. In a game engine, touching engine objects from a
non-main thread is usually undefined behaviour and will crash or corrupt state — often
not immediately, which makes it miserable to debug.

Use this mode for servers, tools, and console applications.

### `asyncCallbacks: false` — the right choice for a game engine

You pump it yourself, once per frame, from your main loop:

```csharp
void Update()   // Unity: MonoBehaviour.Update, or your engine's per-frame tick
{
    SteamClient.RunCallbacks();
}
```

Now every callback and every `await` continuation resumes on the thread that called
`RunCallbacks`. That is your main thread, so it is safe to touch game objects directly.

**If you forget to call `RunCallbacks` in this mode, nothing breaks loudly — your
`await`s simply never complete.** A hang with no exception and no error is the signature
of this mistake.

### Why this matters for `await`

```csharp
var image = await SteamFriends.GetLargeAvatarAsync( steamId );
```

That `await` completes only when the corresponding callback is pumped. With
`asyncCallbacks: false` and no `RunCallbacks` call, it waits forever.

---

## 6. Shutting down

```csharp
SteamClient.Shutdown();
```

Call it once, when your game is closing. It stops the background pump (if any), releases
the interfaces, and tells Steam you are done.

- Calling `Init` twice without a `Shutdown` in between throws.
- **Unity users:** the editor does not unload your process between play sessions, so a
  domain reload can leave the library initialised while your static state is reset. Guard
  your `Init` call and shut down on `OnApplicationQuit`/`OnDisable` to avoid the "already
  initialized" exception on the second play.

---

## 7. Dedicated servers are a different entry point

A dedicated server is not a client. It does not log in as a user and it uses
`SteamServer`, not `SteamClient`:

```csharp
var init = new SteamServerInit( "mygame", "My Game" )
{
    GamePort = 28015,
    QueryPort = 28016,
    Secure = true,          // enable VAC
    VersionString = "1.0.0"
};

try
{
    SteamServer.Init( AppId, init );
}
catch ( Exception e )
{
    Console.WriteLine( $"Server init failed: {e.Message}" );
    return;
}
```

Notes that are easy to get wrong:

- `ModDir` (the first constructor argument) must be a short folder-safe identifier, and
  must stay stable — the server browser keys off it.
- `GamePort` and `QueryPort` must both be reachable from the internet. `QueryPort` is what
  the Steam server browser talks to; if only `GamePort` is open, your server runs but
  never appears in the browser.
- A dedicated server pumps callbacks with `SteamServer.RunCallbacks()`, not
  `SteamClient.RunCallbacks()`.
- You need the native `steam_api` binary beside the server executable, matching the
  server's platform — `libsteam_api.so` for a Linux server, not the Windows DLL.

See [the GameServer audit](../audit/04-gameserver.md) for the current state of this path.

---

## 8. Which native binary do I ship?

This library is pure C# but it P/Invokes into Valve's native `steam_api` library, which
you must ship alongside your game.

| Target | File | Comes from |
|---|---|---|
| Windows x64 | `steam_api64.dll` | `Facepunch.Steamworks/steam_api64.dll` |
| Windows x86 | `steam_api.dll` | `Facepunch.Steamworks/steam_api.dll` |
| Linux x64 | `libsteam_api.so` | `Facepunch.Steamworks/linux64/libsteam_api.so` |
| Linux x86 | `libsteam_api.so` | `Facepunch.Steamworks/linux32/libsteam_api.so` |
| macOS | `libsteam_api.dylib` | `Facepunch.Steamworks/osx/libsteam_api.dylib` |

**These must match the version this library was built against.** A mismatched binary does
not fail gracefully — you get `EntryPointNotFoundException` at the first call that uses a
function the older binary lacks, potentially deep into a play session.

Every binary in this repository is checked against the library's P/Invoke declarations on
every CI run by `verify-native-conformance.ps1`, so the copies listed above are known
good. If you source a `steam_api` binary from elsewhere, run that script against it before
shipping.

---

## Where to go next

- [Native conformance audit](../audit/00-native-conformance.md) — what is bound, what is
  not, and why.
- [API coverage and missing features](../audit/03-api-coverage-gaps.md) — what this
  library does and does not expose.
