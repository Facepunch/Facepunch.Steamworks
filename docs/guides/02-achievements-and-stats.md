# Achievements & Stats — and why your first write disappears

This guide assumes you have never shipped a Steam game. It covers achievements, stats and
leaderboards, and it is blunt about the places where this library's own XML documentation is
wrong, because two of them will cost you an afternoon each.

If you only read one thing: **stats arrive on their own a few frames after `Init`, and
anything you write before they arrive is silently discarded.** Gate your stats code on
`SteamUserStats.StatsReceived`.

> **Obsolete advice you will find elsewhere.** Every tutorial written before ~2023 tells you
> to call `RequestCurrentStats()` and wait for its callback. Valve **deleted** that function
> from `steam_api`. It survives here as an `[Obsolete]` stub that returns `true` and calls
> nothing (`SteamUserStats.cs:184-192`), so code following that advice waits for a callback
> that can never arrive. See §2 for what replaced it.

---

## 1. The mental model

Stats and achievements live on Steam's servers, not in your game. Your process holds a
**local cache** of them.

```
Your game                        Steam client                    Steam servers
┌──────────────────┐            ┌────────────────┐             ┌──────────────┐
│ SetStat("kills") │──writes──► │  local cache   │             │              │
│                  │            │                │─StoreStats─►│  the truth   │
│ GetStatInt(...)  │◄──reads─── │                │◄─on login───│              │
└──────────────────┘            └────────────────┘             └──────────────┘
```

Four consequences:

1. **Reads and writes are local and instant.** They do not block and they do not fail
   because the network is slow.
2. **Nothing is persisted until `StoreStats()`.** Until then a crash loses the changes.
3. **The cache is empty for the first few frames** after `Init`, while Steam fills it. That
   is §2, and it is the thing that bites everyone.
4. **You cannot invent stats at runtime.** Every stat and achievement must exist in the
   Steamworks partner site for your AppID *and be published*. A call naming a stat that does
   not exist returns `false` and does nothing else.

---

## 2. Waiting for stats — the trap

`SteamUserStats.StatsReceived` is `false` until Steam delivers `UserStatsReceived_t` for the
local user. Steam sends it automatically shortly after `SteamClient.Init`; you do not ask
for it. In practice that is a handful of frames, but it is *not* synchronous with `Init`.

Before it flips:

- every achievement reads as **locked**,
- every stat reads as **zero**,
- every write is **silently discarded** — no exception, no `false` you can distinguish.

Two ways to gate. Polling, if your architecture prefers it:

```csharp
if ( !SteamUserStats.StatsReceived )
    return;         // try again next frame
```

Or event-driven, which is usually cleaner:

```csharp
SteamUserStats.OnUserStatsReceived += ( steamId, result ) =>
{
    if ( steamId != SteamClient.SteamId ) return;   // fires for friends' stats too
    if ( result != Result.OK ) { Log( $"stats failed: {result}" ); return; }

    LoadPlayerProgress();
};
```

That `steamId` check matters. `OnUserStatsReceived` fires for *any* user whose stats have
arrived, including friends fetched via `Friend.RequestUserStatsAsync()`. Only the local
user's arrival sets `StatsReceived` (`SteamUserStats.cs:61-67`).

**It only ever flips while callbacks are being pumped.** With `asyncCallbacks: false` and no
`SteamClient.RunCallbacks()` in your loop, `StatsReceived` stays `false` forever and your
stats code never runs — with no error. See
[Getting Started §5](01-getting-started.md#5-callbacks-the-part-everyone-gets-wrong).

### The spelling

The property was originally misspelled `StatsRecieved`. Both spellings exist:

```csharp
public static bool StatsReceived { get; internal set; }              // use this

[Obsolete( "Misspelled - use StatsReceived instead. This forwards to it.", false )]
public static bool StatsRecieved { get; internal set; }              // still compiles
```

The old one forwards to the new one, so they read the same state and existing code keeps
working with a warning. It was not renamed outright because that is a binary-breaking change.

### A rough edge: `StatsReceived` is never reset

Nothing sets it back to `false` — not `SteamClient.Shutdown()`, not `DestroyInterface`. In a
long-lived process that initialises Steam more than once (the Unity editor across play
sessions is the usual case) it stays `true` from the previous run while the new run's cache
is still empty. If you re-`Init` in-process, do not trust it; wait for a fresh
`OnUserStatsReceived` instead.

---

## 3. Stats

### Reading

```csharp
int   kills    = SteamUserStats.GetStatInt( "kills" );
float distance = SteamUserStats.GetStatFloat( "distance_travelled" );
```

There is no method called `GetStat` — the two are named apart because they would otherwise
be indistinguishable overloads.

**Neither can fail visibly.** Both discard the native call's success flag and return `0` /
`0.0f` when the stat does not exist, is unpublished, or has not arrived yet
(`SteamUserStats.cs:283-298`). You cannot tell "the player has 0 kills" from "there is no
such stat". If you need that distinction, check `StatsReceived` first and verify the stat
name against the partner site — the API will not tell you.

### Writing

```csharp
SteamUserStats.SetStat( "kills", 42 );
SteamUserStats.SetStat( "distance_travelled", 1523.5f );

SteamUserStats.AddStat( "kills", 1 );          // read-modify-write, not atomic
SteamUserStats.AddStat( "distance", 12.5f );
```

> **The XML doc on `SetStat` is wrong.** It says *"This will automatically call `StoreStats`
> after a successful call."* It does not — the body is a single pass-through to the native
> setter, with no store (`SteamUserStats.cs:264-278`). The same false claim is repeated on
> the `float` overload, and because `AddStat` delegates to `SetStat`, it does not store
> either. **You must call `StoreStats()` yourself.** This is the single most likely reason
> your progress is not saving.

Two more things about these:

- **`AddStat` is not atomic.** It reads, adds, and writes back — the library's own comment
  says so, because Steam offers no atomic increment. Two code paths incrementing the same
  stat in the same frame can lose one.
- **`AddStat( "kills" )` with one argument does not compile.** Both overloads declare their
  second parameter as optional (`int amount = 1` and `float amount = 1.0f`), so a
  one-argument call is ambiguous — `CS0121`. Always pass the amount explicitly, and let the
  literal pick the overload: `1` for `int`, `1f` for `float`. Note there is no `double`
  overload, so `SetStat( "x", 5.0 )` also fails to compile.

### Storing

```csharp
if ( !SteamUserStats.StoreStats() )
{
    // Nothing was sent. Keep the pending values and try again later.
}
```

`StoreStats()` returns `false` and sends **nothing** on failure — it is all-or-nothing, so a
failed call leaves your local changes intact to retry.

**It is rate limited.** The library's own doc is explicit that call frequency should be *"on
the order of minutes, rather than seconds"* — end of round, map change, player leaving. Do
not call it every time a stat changes, and never per frame.

Two useful behaviours:

- If your process exits with unstored changes, Steam calls `StoreStats` for you. Do not rely
  on it (a crash is not a clean exit), but it means a missed store is usually not data loss.
- `StoreStats` is **required to display the achievement unlock toast.** If you unlock an
  achievement with `Trigger( apply: false )` and never store, the player never sees the
  popup. See §4.

Debug output goes to `%steam_install%\logs\stats_log.txt`, which is the only place that will
tell you *why* a store was rejected.

### The `Stat` struct — for other players and global aggregates

`Steamworks.Data.Stat` is a richer wrapper around the same data:

```csharp
var stat = new Stat( "kills" );
int mine  = stat.GetInt();
bool ok   = stat.Set( 50 );
bool sent = stat.Store();

// Global aggregates across every player (needs RequestGlobalStatsAsync first)
await SteamUserStats.RequestGlobalStatsAsync( days: 7 );
long  totalKills = new Stat( "kills" ).GetGlobalInt();
long[] perDay    = await new Stat( "kills" ).GetGlobalIntDaysAsync( 7 );
```

`RequestGlobalStatsAsync` returns `Result.InvalidState` if the local user's stats have not
arrived yet, and `days` is capped at 60.

For another player, construct it with their id — but you must fetch their stats first:

```csharp
var friend = new Friend( someSteamId );
if ( await friend.RequestUserStatsAsync() )
{
    int theirKills = new Stat( "kills", someSteamId ).GetInt();
}
```

**`Set`, `Add`, `UpdateAverageRate` and `Store` throw when `UserId` is not the local user** —
a bare `System.Exception` reading `"Stat.<method> can only be called for the local user"`.
Clients cannot write other players' stats; only a game server can, and only from an official
server IP range.

Two naming inconsistencies to be aware of: `Stat.GetGlobalIntDaysAsync` has the `Async`
suffix but `Stat.GetGlobalFloatDays` does not, despite both being `async`.

---

## 4. Achievements

An `Achievement` is a lightweight struct wrapping the achievement's **API Name** — the
identifier you typed into the Steamworks partner site, not the text the player sees.

```csharp
var ach = new Achievement( "ACH_KILL_100_ZOMBIES" );
```

Constructing one never fails and never validates: a typo produces a struct that reads as
locked, has an empty display name, and cannot be unlocked. That is the same observable state
as a real achievement that the player has not earned, so **check your identifiers against
the partner site** — nothing in the API will.

### Reading

```csharp
foreach ( var a in SteamUserStats.Achievements )
{
    Console.WriteLine( $"{a.Identifier} — {a.Name}: {a.Description}" );
    Console.WriteLine( a.State ? $"unlocked {a.UnlockTime}" : "locked" );
}
```

| Member | Type | What it is |
|---|---|---|
| `Identifier` | `string` | The API Name. What you pass to the constructor. |
| `Name` | `string` | The **localized display name**. Changes with the player's language. |
| `Description` | `string` | The localized description. |
| `State` | `bool` | `true` if unlocked. |
| `UnlockTime` | `DateTime?` | `null` when locked *or* when the call failed. |
| `GetIcon()` | `Image?` | See below. |
| `GetIconAsync( int timeout = 5000 )` | `Task<Image?>` | See below. |

`Identifier` and `Name` are easy to swap and the compiler cannot help you — both are
`string`. If your achievement list renders identifiers to the player, you used the wrong one.

`GetIcon()` returns `null` when the icon exists but Steam has not downloaded it yet, which is
common on the first frames. Use `GetIconAsync()` unless you are re-checking every frame
anyway.

### Unlocking

```csharp
var ach = new Achievement( "ACH_KILL_100_ZOMBIES" );

if ( !ach.State )
    ach.Trigger();      // unlocks AND stores
```

`Trigger( bool apply = true )` calls `StoreStats` for you when `apply` is `true`, which is
the default. That is the opposite of `SetStat`, and it is the reason achievements usually
"just work" while stats mysteriously do not.

Pass `apply: false` when unlocking several at once, then store once:

```csharp
foreach ( var name in newlyEarned )
    new Achievement( name ).Trigger( apply: false );

SteamUserStats.StoreStats();      // one store, one batch of toasts
```

`Clear()` re-locks an achievement and does **not** store — you must call `StoreStats()`
after it. It is for development; shipping games should not be re-locking achievements.

### Progress toasts

```csharp
SteamUserStats.IndicateAchievementProgress( "ACH_KILL_100_ZOMBIES", 17, 100 );
```

**This only shows the "17 of 100" popup. It records nothing.** Progress is stored by setting
a stat and configuring Steam's achievement progress rules on the partner site to watch it.
Calling this without also storing a stat produces a notification the player sees once and
that is forgotten immediately.

It returns `false` — without telling you which — when stats have not arrived, the achievement
name does not exist or has unpublished changes, or the achievement is already unlocked. It
*throws* `ArgumentNullException` for a null/empty name and `ArgumentException` when
`curProg >= maxProg`, so do not call it with the completing value; call `Trigger()` instead.

### `GlobalUnlocked` does not work

```csharp
float pct = ach.GlobalUnlocked;    // always -1 in this build
```

It is documented as "a decimal (0-1) representing the global amount of users who have
unlocked this achievement, or -1 if no data available", and it unconditionally returns `-1`.
The native call it wraps requires `RequestGlobalAchievementPercentages` to have completed
first, and **nothing in the library ever calls that** — the binding exists but is `internal`,
so you cannot make the call yourself from outside the assembly either.

This is a known open defect, not a documentation nuance. See
[audit 03](../audit/03-api-coverage-gaps.md) and the
[known limitations](../../README.md#known-limitations--open-issues) list.

### Other players' achievements

```csharp
var friend = new Friend( someSteamId );
if ( await friend.RequestUserStatsAsync() )
{
    bool got = friend.GetAchievement( "ACH_KILL_100_ZOMBIES" );
    DateTime when = friend.GetAchievementUnlockTime( "ACH_KILL_100_ZOMBIES" );
}
```

Note the inconsistency: `Friend.GetAchievementUnlockTime` returns a non-nullable `DateTime`
using `DateTime.MinValue` as its failure sentinel, while `Achievement.UnlockTime` returns
`DateTime?`. Check for `DateTime.MinValue` on the `Friend` path.

---

## 5. Leaderboards

You cannot construct a `Leaderboard`. You get one from Steam:

```csharp
Leaderboard? board = await SteamUserStats.FindLeaderboardAsync( "Best Times" );
if ( !board.HasValue ) return;                // not found, or the call failed
```

Or create it on demand:

```csharp
var board = await SteamUserStats.FindOrCreateLeaderboardAsync(
    "Best Times",
    LeaderboardSort.Ascending,               // lowest score wins — a time
    LeaderboardDisplay.TimeMilliSeconds );
```

**Prefer creating leaderboards on the partner site.** One created through
`FindOrCreateLeaderboardAsync` does not appear in the Steam Community until you set its
Community Name in the App Admin panel by hand. Use the API form only when you genuinely need
leaderboards created dynamically.

`LeaderboardSort` is `Ascending` (lowest is best) or `Descending` (highest is best).
`LeaderboardDisplay` is `Numeric`, `TimeSeconds` or `TimeMilliSeconds`.

### Submitting

```csharp
// Keeps the player's existing score if it is better.
LeaderboardUpdate? r = await board.Value.SubmitScoreAsync( 12345 );

// Overwrites unconditionally — for "latest run" boards, or to correct a bad entry.
LeaderboardUpdate? f = await board.Value.ReplaceScore( 12345 );

if ( r.HasValue && r.Value.Changed )
    Console.WriteLine( $"rank {r.Value.OldGlobalRank} -> {r.Value.NewGlobalRank}" );
```

`ReplaceScore` is the only async member on `Leaderboard` **without** an `Async` suffix. That
is a naming wart, not a different mechanism.

`LeaderboardUpdate` carries `Score`, `Changed`, `NewGlobalRank`, `OldGlobalRank` and a
computed `RankChange`. `Changed` is `false` when your submitted score did not beat the
existing one — that is a success, not a failure.

Both take an optional `int[] details`, which is arbitrary per-entry data you get back when
reading entries. **Keep it to 64 ints**; the read path uses a fixed 64-element buffer and
silently truncates beyond that.

### Reading

```csharp
LeaderboardEntry[] top    = await board.Value.GetScoresAsync( 10 );          // ranks 1-10
LeaderboardEntry[] around = await board.Value.GetScoresAroundUserAsync( -5, 5 );
LeaderboardEntry[] mates  = await board.Value.GetScoresFromFriendsAsync();
LeaderboardEntry[] some   = await board.Value.GetScoresForUsersAsync( ids );

if ( top == null ) return;      // yes, really — see below

foreach ( var e in top )
    Console.WriteLine( $"#{e.GlobalRank} {e.User.Name} {e.Score}" );
```

**Every one of these returns `null`, not an empty array, when there are no entries.** An
empty leaderboard and a failed call are indistinguishable, and a `foreach` over the result
without a null check is a `NullReferenceException` on an empty board — which is exactly the
state a new leaderboard is in when you first test it.

Leaderboards are **1-indexed**. `GetScoresAsync( count, offset = 1 )` throws
`ArgumentException` for `offset <= 0`; there is no rank 0.

`GetScoresAroundUserAsync` clamps: request `-2..2` for the #1 player and Steam returns the
first five entries rather than three. If the player has no entry at all, you get nothing back.

`LeaderboardEntry` exposes `User` (a `Friend`), `GlobalRank`, `Score` and `Details`
(`null` unless the entry had any).

### Two sharp edges in the read path

- **`GetScores*` can hang indefinitely.** Before returning, it resolves every entry's display
  name by polling `RequestUserInformation` in a loop with no timeout. If callbacks stop being
  pumped mid-await, the task never completes. Pump callbacks for as long as any leaderboard
  read is outstanding.
- **The details buffer is a shared static.** Two leaderboard reads in flight at once race on
  it. Await one before starting the next.

`Leaderboard.AttachUgc( Ugc file )` exists but is effectively uncallable — the `Ugc` handle
type has no public constructor and nothing in the public API returns one, so the only value
you can pass is `default`. Treat it as unimplemented.

---

## 6. Testing without shipping

`SteamUserStats.ResetAll( includeAchievements: true )` wipes every stat and, optionally, every
achievement for the current user. It is the only sane way to test a progression system more
than once.

```csharp
#if DEBUG
if ( DebugKeyPressed( "F9" ) )
{
    SteamUserStats.ResetAll( includeAchievements: true );
    SteamUserStats.StoreStats();
}
#endif
```

Guard it behind a debug build. It is not reversible and it operates on the real account.

Things that are worth knowing before you conclude your integration is broken:

- **A stat or achievement must be *published*** on the partner site, not merely created.
  Unpublished ones behave exactly like ones that do not exist.
- **AppID 480 (Spacewar) has its own fixed set** of stats and achievements. Your own names
  will not work against it; test progression against your real AppID.
- **The toast only appears once per unlock, ever**, until you reset. If you are testing the
  popup, `ResetAll` between runs.
- **Nothing here works before `SteamClient.Init`.** `SteamUserStats.Internal` is `null` until
  then, and every member on `Achievement`, `Stat` and `Leaderboard` dereferences it with no
  guard — you get a `NullReferenceException`, not a helpful message.

---

## What this library does not expose

For completeness, so you do not go looking:

- **`RequestGlobalAchievementPercentages`** — the prerequisite for `GlobalUnlocked` (§4). The
  binding exists but is `internal`.
- **Server-side stats are a different class.** `SteamServerStats` has its own
  `RequestUserStatsAsync` / `SetInt` / `SetFloat` / `SetAchievement` / `StoreUserStats`, and
  they take a `SteamId` because a server acts on behalf of players. Note that its getters
  cannot distinguish "not loaded" from zero, and that Steam unloading a user's stats is not
  surfaced — writing back the defaults you just read would overwrite the player's real
  progress. See [audit 04](../audit/04-gameserver.md) F10.

---

## Where to go next

- [Getting Started](01-getting-started.md) — initialisation, callback pumping, the
  `asyncCallbacks` decision that governs whether any of the above ever runs.
- [Dedicated Servers](06-dedicated-servers.md) — `SteamServerStats`, and why writing stats
  from a server is more restricted than you expect.
- [API coverage and missing features](../audit/03-api-coverage-gaps.md) — what is and is not
  exposed, per interface, with evidence.
