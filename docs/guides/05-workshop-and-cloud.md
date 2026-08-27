# Workshop & Steam Cloud — user content and save games

This guide assumes you have never shipped a Steam game. It covers two separate systems that
happen to share a chapter because both are "Steam stores files for you":

- **Workshop (UGC)** — content players make, publish, browse and subscribe to.
- **Steam Cloud** — the player's own saves and settings, synced between their machines.

If you only read one thing about Workshop: **queries are always scoped to your own AppID and
you cannot change that**, and **`ResultPage.Entries` is a lazy iterator that returns nothing
after you dispose the page**. Those two account for most of the confusion.

If you only read one thing about Cloud: **`FileRead` returns `null` on failure and there is
no way to tell why.**

---

## 0. A namespace trap, before any code

There are two different things called `Ugc`:

| | What it is |
|---|---|
| `Steamworks.Ugc` | The **namespace** holding `Query`, `Item`, `Editor`, `ResultPage`. |
| `Steamworks.Data.Ugc` | A **struct** wrapping a UGC file handle. Unrelated. |

So this does not compile:

```csharp
using Steamworks;
using Steamworks.Data;      // brings the Ugc STRUCT into scope

var q = Ugc.Query.All;      // error: 'Ugc' does not contain 'Query'
```

Either drop the `Steamworks.Data` using, or qualify:

```csharp
var q = Steamworks.Ugc.Query.All;
```

Code inside `namespace Steamworks { … }` resolves `Ugc.Query` through the enclosing
namespace and works either way, which is why the library's own tests do not hit this. Yours
will.

---

# Part one — Workshop

## 1. The model

```
   Player A                Steam                    Player B
  ┌────────┐            ┌──────────┐              ┌────────┐
  │ Editor │──publish──►│  Item    │──subscribe──►│ auto-  │
  │        │            │ 12345678 │              │ download│
  └────────┘            │ title    │              └────────┘
                        │ tags     │                   │
                        │ content  │            Directory ──► your game loads it
                        └──────────┘
```

Four concepts:

- **Item** — one published thing, identified by a `PublishedFileId` (a `ulong`).
- **Query** — a search over items.
- **Subscription** — the player opting in. Steam then downloads and updates the item
  automatically, forever, without your game asking.
- **Editor** — the publish/update path.

The part people underestimate: **once a player is subscribed, Steam keeps the files current
on its own.** Your game's job at startup is to ask what is installed and where, not to manage
downloads.

---

## 2. Loading what the player is subscribed to

This is the code every Workshop-supporting game needs, and it is short:

```csharp
var subscribed = new List<PublishedFileId>();
SteamUGC.GetSubscribedItems( subscribed );

foreach ( var id in subscribed )
{
    var item = new Steamworks.Ugc.Item( id );

    if ( !item.IsInstalled )
        continue;                       // still downloading, or failed

    if ( item.Directory == null )
        continue;                       // installed but the path is unavailable

    LoadModFrom( item.Directory );
}
```

`GetSubscribedItems` **appends** to the list you pass — it does not clear it first. Pass a
fresh list, or clear it yourself, or you will accumulate duplicates each time you refresh.

`Directory` returns `null` rather than throwing when Steam cannot supply the install path.
Check it; a mod loader that assumes a non-null path crashes on the first partially-installed
item.

To react to changes while running:

```csharp
SteamUGC.OnItemInstalled     += ( appId, fileId ) => ReloadMod( fileId );
SteamUGC.OnItemSubscribed    += ( appId, fileId ) => Log( $"subscribed {fileId}" );
SteamUGC.OnItemUnsubscribed  += ( appId, fileId ) => UnloadMod( fileId );
SteamUGC.OnDownloadItemResult+= ( appId, fileId, result ) => Log( $"{fileId}: {result}" );
```

---

## 3. Browsing — queries

```csharp
var query = Steamworks.Ugc.Query.Items
                .RankedByTrend()
                .WithTag( "Weapons" )
                .MatchAllTags()
                .WithLongDescription( true );

var page = await query.GetPageAsync( 1 );        // pages are 1-based

if ( !page.HasValue )
    return;

using ( var result = page.Value )
{
    Console.WriteLine( $"{result.ResultCount} of {result.TotalCount}" );

    foreach ( var item in result.Entries )       // MUST be inside the using
        Show( item.Title, item.Owner.Name, item.VotesUp );
}
```

### Three things that will catch you

**Pages are 1-based and a fixed 50 items.** `GetPageAsync( 0 )` throws, and the page size is
Valve's `kNumUGCResultsPerPage = 50` — you cannot change it. Paginate; do not try to ask for
200 items at once.

**`Entries` is a lazy iterator over a live native handle.** It re-queries on every
enumeration, and returns nothing once the page is disposed. Enumerate inside the `using`, or
materialise with `.ToList()` before leaving it. A `ResultPage` captured and read later yields
an empty sequence — not an exception, which makes it hard to spot.

**`Query` is a struct, but its tag lists are not.** Every builder method returns a modified
copy, so an unassigned call is lost:

```csharp
var q = Steamworks.Ugc.Query.Items;
q.WithTag( "Weapons" );          // WRONG — the copy is discarded
q = q.WithTag( "Weapons" );      // right
```

That much is the ordinary struct rule. The subtler half is that the tag lists inside are
reference types **shared by every copy**, so branching one stored query contaminates all the
branches:

```csharp
var q = Steamworks.Ugc.Query.Items.WithTag( "Fantasy" );
var swords = q.WithTag( "Sword" );
var bows   = q.WithTag( "Bow" );
// swords, bows AND q now all require Fantasy + Sword + Bow.
```

**Build each query from a fresh factory call.** Do not branch or reuse one. This is verified
against the implementation, not documented by Valve.

### What comes back, and what does not

Most optional fields are **off by default** and each costs a round trip's worth of payload,
so you opt in:

| Call | Populates |
|---|---|
| `WithLongDescription( true )` | `Item.Description` |
| `WithMetadata( true )` | `Item.Metadata` |
| `WithKeyValueTags( true )` | `Item.KeyValueTags` |
| `WithChildren( true )` | `Item.Children` |
| `WithAdditionalPreviews( true )` | `Item.AdditionalPreviews` |
| `WithDefaultStats( bool )` | The `Num*` statistics — **on by default** |
| `WithTotalOnly( true )` | Only `TotalCount`; skips fetching entries |

All of these take a **required `bool`** — there is no parameterless form.

Filters and ranking worth knowing:

```csharp
Steamworks.Ugc.Query.Items
    .WhereSearchText( "sword" )       // NOT WithSearchText
    .WithTag( "Fantasy" ).WithTag( "Melee" ).MatchAllTags()
    .WithoutTag( "NSFW" )
    .RankedByVote()
    .AllowCachedResponse( 300 )       // seconds; 0 forces a fresh query
```

`MatchAnyTag()` is the alternative to `MatchAllTags()`. `AllowCachedResponse` takes seconds
and is cast to unsigned internally, so a negative value becomes an enormous cache age — pass
`0` if you mean "no cache".

Ranking methods are `RankedByVote`, `RankedByPublicationDate`, `RankedByTrend`,
`RankedByTextSearch`, `RankedByTotalUniqueSubscriptions`, `RankedByPlaytimeTrend`,
`RankedByTotalPlaytime`, and a dozen more.

### Querying a specific player's items

```csharp
// Everything the current user published:
var mine = Steamworks.Ugc.Query.Items.WhereUserPublished();

// Everything a specific user published:
var theirs = Steamworks.Ugc.Query.Items.WhereUserPublished( someSteamId );

// Also available: WhereUserSubscribed, WhereUserFavorited, WhereUserVotedUp,
// WhereUserVotedDown, WhereUserFollowed, WhereUserUsedOrPlayed, WhereUserWillVoteLater
```

`WhereUserPublished()` with no argument means **the current user**. There is no `FromSelf()`
and no separate `UserQuery` type — this is the whole mechanism.

**Do not combine `CreatedByFriends()` with a `WhereUserX(…)` filter.** `CreatedByFriends` is
a *ranking* on the all-items query; adding a user filter switches to a different underlying
query shape and the ranking is silently dropped.

### Fetching one known item

```csharp
Steamworks.Ugc.Item? item = await Steamworks.Ugc.Item.GetAsync( 1720164672 );

Console.WriteLine( item?.Title );
Console.WriteLine( item?.IsSubscribed );
```

`Item.GetAsync` takes a `maxageseconds` parameter that is **declared and never used** — the
implementation does not apply it. Do not rely on it to control caching.

### One limitation of the query API

**You cannot query another app's Workshop.** The consumer and creator AppIDs are private
fields defaulted to `SteamClient.AppId`, with no public setter and no `ForAppId` on `Query`.
(`Editor` does have `ForAppId`.)

`InLanguage( "german" )` asks Steam to return titles and descriptions in a specific language
where the creator supplied a translation, falling back to the language the item was written
in. It was a no-op until recently — the value was stored and never read — so if you tried it
before and concluded it did not work, try again.

---

## 4. Subscribing and downloading

```csharp
var item = new Steamworks.Ugc.Item( fileId );

bool ok = await item.Subscribe();      // Steam now keeps it updated forever
await item.Unsubscribe();

await item.AddFavorite();
await item.Vote( up: true );
```

Subscribing is normally all you need — Steam downloads on its own. Force it when you want the
content *now*:

```csharp
if ( !item.Download( highPriority: true ) )
{
    // The download did not start.
}
```

`Download` returns `bool`, and `false` means it did not begin.

To wait and show progress:

```csharp
await SteamUGC.DownloadAsync(
    SteamClient.AppId, fileId,
    progress: ( fraction, downloaded, total ) => SetProgressBar( fraction ),
    ct: cancellationToken );
```

The progress callback is `Action<float, long, long>` — a `0..1` fraction, then bytes
downloaded and bytes total. It is not `IProgress<T>`.

> **Two things about `DownloadAsync` its own documentation gets wrong.**
>
> **It has no timeout.** The XML doc claims *"If CancellationToken is default then there is
> 60 seconds timeout"*. There is no such timeout in the code — the `60` in the signature is
> `milisecondsUpdateDelay`, the polling interval (and yes, that parameter name is misspelled
> in the public API). A download that *fails* now exits the loop and returns `false`; a
> download that simply never progresses — no result callback ever arrives — still spins
> forever. **Always pass a `CancellationToken`.** It is the only exit for that case.
>
> **A `true` return does not mean it downloaded.** On the early-out path it returns
> `item.IsInstalled` — which is `true` if the item was already installed before you asked.

Progress polling, without awaiting:

```csharp
if ( item.IsDownloading )
    SetProgressBar( item.DownloadAmount );      // 0..1
```

`SteamUGC.SuspendDownloads()` / `ResumeDownloads()` pause Steam's background downloading —
useful during a loading screen or a benchmark, if you remember to resume.

---

## 5. Publishing

```csharp
var result = await Steamworks.Ugc.Editor.NewCommunityFile
    .WithTitle( "My Weapon Pack" )
    .WithDescription( "Five new swords." )
    .WithContent( "C:/mods/weaponpack" )       // a folder, not a file
    .WithPreviewFile( "C:/mods/weaponpack/preview.png" )
    .WithTag( "Weapons" )
    .WithTag( "Fantasy" )
    .WithPublicVisibility()
    .WithChangeLog( "Initial release" )
    .SubmitAsync( progress );

if ( !result.Success )
{
    Log( $"Publish failed: {result.Result}" );

    if ( result.NeedsWorkshopAgreement )
        ShowMessage( "You must accept the Workshop legal agreement first." );

    return;
}

Log( $"Published as {result.FileId}" );
```

`progress` is a plain BCL `IProgress<float>`:

```csharp
class Bar : IProgress<float>
{
    public void Report( float value ) => SetProgressBar( value );
}
```

Reported values step through roughly: 0.1 preparing config, 0.2 preparing content, 0.2→0.8
uploading content, 0.8 uploading preview, 1.0 committing.

`SubmitAsync` also takes an optional `Action<PublishResult> onItemCreated`, which fires
**only when creating a new item**, after Steam allocates the `PublishedFileId` but before the
content upload. That is how you capture the ID early, so a failed upload does not orphan an
item you cannot find again.

Updating an existing item is the same builder, from the item:

```csharp
await new Steamworks.Ugc.Item( fileId ).Edit()
    .WithTitle( "My Weapon Pack v2" )
    .WithContent( "C:/mods/weaponpack" )
    .WithChangeLog( "Fixed the sword" )
    .SubmitAsync( progress );
```

### Editor sharp edges

- **`SubmitAsync` throws, it does not return a failure**, for two content-folder problems: a
  folder that does not exist, and a folder that is empty. Both are bare `System.Exception`.
  `PublishResult.Success` will never tell you about them, so wrap the call in a `try`.
- **`WithTag` replaces, it does not merge.** Tags are submitted as a complete set. There is
  also no way to clear all tags through this API — the tag call is skipped entirely when the
  set is empty.
- **`AddKeyValueTag` has constraints Valve documents and the compiler does not:** keys are
  alphanumeric plus underscore, keys and values are capped at 255 characters, they are
  searchable by exact match only, and a single item update may not remove more than 100 keys.
  To replace all values for a key, call `RemoveKeyValueTags( key )` then `AddKeyValueTag`.
- The starting points are `NewCommunityFile`, `NewCollection`, `NewMicrotransactionFile` and
  `NewGameManagedFile`. Community files are what players publish; the others are for content
  your app manages.

### Dependencies and collections

```csharp
await collection.AddDependency( childFileId );
await collection.RemoveDependency( childFileId );
```

To read them back, the query must ask for them — `WithChildren( true )`. (An older doc
comment mentions `WithDependencies(true)`; no such method exists.)

---

# Part two — Steam Cloud

## 6. Reading and writing

```csharp
byte[] data = Encoding.UTF8.GetBytes( json );
bool ok = SteamRemoteStorage.FileWrite( "save1.json", data );

byte[] loaded = SteamRemoteStorage.FileRead( "save1.json" );
if ( loaded == null )
{
    // The file does not exist, is empty, or the read was short. You cannot tell which.
    return NewGame();
}
```

The API is **bytes only**. There is no string overload in either direction, and `FileRead`
applies no encoding — you do the conversion.

**`FileRead` returns `null` for three different reasons** — file missing, file empty, or a
short read — with no way to distinguish them. Use `FileExists` first if the distinction
matters to your UI:

```csharp
if ( SteamRemoteStorage.FileExists( "save1.json" ) )
{
    var bytes = SteamRemoteStorage.FileRead( "save1.json" );
    if ( bytes == null ) ShowError( "Save file is corrupt or unreadable." );
}
```

**`FileWrite` does not tolerate `null` or an empty array.** It pins the array without
checking, so passing either is a crash rather than a `false` return. Guard at your call site.

Enumerating:

```csharp
foreach ( var name in SteamRemoteStorage.Files )
{
    Console.WriteLine( $"{name}  {SteamRemoteStorage.FileSize( name )} bytes  " +
                       $"{SteamRemoteStorage.FileTime( name )}" );
}
```

`FileSize` returns `int`, which caps a single file at about 2 GB regardless of what Steam
would allow. `FileTime` returns a `DateTime`, already converted from Steam's Unix timestamp.

## 7. Delete versus forget

These sound similar and do opposite things:

| Call | Effect |
|---|---|
| `FileDelete( name )` | Deletes locally **and** propagates the delete to the cloud. |
| `FileForget( name )` | Removes it from the cloud, **leaves the local copy** in place and readable. |

`FileForget` is for "stop syncing this" — a large local cache, a machine-specific config.
`FileDelete` is for "this save is gone". Getting them backwards means either a save the
player thought they deleted reappearing on their other machine, or a file vanishing that was
only supposed to stop syncing.

## 8. Quota

```csharp
ulong total     = SteamRemoteStorage.QuotaBytes;
ulong used      = SteamRemoteStorage.QuotaUsedBytes;
ulong remaining = SteamRemoteStorage.QuotaRemainingBytes;

if ( remaining < (ulong)data.Length )
    ShowError( "Not enough Steam Cloud space." );
```

Each of those three is a separate native call, and `QuotaUsedBytes` is computed as
total-minus-remaining rather than read directly — so reading all three costs three calls and
can tear if a sync completes between them. Read `QuotaRemainingBytes` once and use it.

Check the quota before writing something large. A write that exceeds it fails, and the
failure looks the same as any other.

## 9. When Cloud is off

```csharp
if ( !SteamRemoteStorage.IsCloudEnabled )
{
    // Writes still "succeed" — they just go to local disk and never sync.
}
```

Three separate switches exist:

| Property | Who controls it |
|---|---|
| `IsCloudEnabledForAccount` | The player, in Steam's global settings. |
| `IsCloudEnabledForApp` | The player, per game — **and settable by you**. |
| `IsCloudEnabled` | Both of the above together. |

**This library does not guard on any of them.** `FileWrite` and `FileRead` pass straight
through to Steam whether Cloud is on or off; Steam writes locally and skips the sync. So a
disabled Cloud is invisible to your code unless you check.

`IsCloudEnabledForApp` is the only settable member. Use it for an in-game "sync my saves"
toggle — but do not silently turn Cloud off on the player's behalf.

## 10. Designing saves for Cloud

Steam Cloud syncs files. It does not merge them, and it does not understand your format.
Practical consequences:

- **Conflicts are resolved by the player, badly.** If they play on two machines while offline,
  Steam shows a dialog asking which version to keep, and one is discarded wholesale. Small,
  granular files lose less than one big monolithic save.
- **Write whole files atomically.** A partial write that syncs is a corrupt save on every
  machine. Build the full byte array, then write once.
- **Keep a local fallback.** Cloud may be off, the quota may be full, or Steam may not be
  running at all. Your save system must work without any of it — see
  [Getting Started §2](01-getting-started.md#2-the-minimum-viable-integration).
- **Do not put anything security-sensitive there.** The files are on the player's disk and
  under their control. Anything that must be authoritative belongs on your own backend.

---

## What this library does not expose

- **File sharing / UGC URLs.** `FileShare`, `UGCDownload`, `UGCDownloadToLocation` and
  `GetUGCDetails` are bound but `internal` — there is no public wrapper, so you cannot share
  a cloud file and get a handle for it.
- **The pre-`ISteamUGC` Workshop API** — 24 functions on `ISteamRemoteStorage`
  (`PublishWorkshopFile`, `EnumerateUserPublishedFiles`, `UpdatePublishedFile*`, …). These are
  deliberately unbound; `ISteamUGC` supersedes them and is fully bound.
- **`SetTimeCreatedDateRange` / `SetTimeUpdatedDateRange`** — date-range query filters exist
  natively but have no builder methods.

---

## Where to go next

- [Getting Started](01-getting-started.md) — if none of the above is working, check that
  callbacks are being pumped; every `await` here depends on it.
- [Performance audit](../audit/06-performance.md) — why a workshop item used to cost 19,616
  bytes to marshal, and what it costs now.
- [API coverage and missing features](../audit/03-api-coverage-gaps.md) — the full per-
  interface inventory.
