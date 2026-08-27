using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Steamworks.Data;

using QueryType = Steamworks.Ugc.Query;

namespace Steamworks.Ugc
{
	/// <summary>
	/// A single piece of Steam Workshop content &#8212; a mod, map, collection, guide, screenshot or
	/// controller config &#8212; together with the operations you can perform on it: subscribe,
	/// download, favourite, vote and edit.
	/// </summary>
	/// <remarks>
	/// <para>
	/// <b>There are two very different ways to get one of these, and they are not interchangeable.</b>
	/// </para>
	/// <list type="number">
	/// <item><description>
	/// From a <see cref="Query"/> (or <see cref="GetAsync"/>, or
	/// <see cref="Steamworks.SteamUGC.QueryFileAsync"/>). This costs a network round trip and gives
	/// you a fully populated item: <see cref="Title"/>, <see cref="Description"/>,
	/// <see cref="Owner"/>, <see cref="Tags"/>, vote counts and so on.
	/// </description></item>
	/// <item><description>
	/// From the <see cref="Item(Steamworks.Data.PublishedFileId)"/> constructor. This is free and
	/// synchronous, but it only carries the id. Every property that comes from Steam's item details
	/// &#8212; title, description, owner, score, timestamps, visibility, vote counts &#8212; reads
	/// back as <see langword="null"/>, <c>0</c> or <c>default</c>. Nothing warns you.
	/// </description></item>
	/// </list>
	/// <para>
	/// The properties that keep working on a bare constructed item are the ones that ask the local
	/// Steam client rather than the backend: <see cref="IsInstalled"/>, <see cref="IsSubscribed"/>,
	/// <see cref="IsDownloading"/>, <see cref="NeedsUpdate"/>, <see cref="Directory"/>,
	/// <see cref="SizeBytes"/> and the download-progress properties. That makes the cheap constructor
	/// exactly right for "is this installed and where are its files", and wrong for anything you want
	/// to display.
	/// </para>
	/// </remarks>
	/// <example>
	/// Showing an item to a player, then subscribing on their behalf:
	/// <code>
	/// var item = await Ugc.Item.GetAsync( fileId );
	/// if ( item.HasValue )
	/// {
	///     Console.WriteLine( $"{item.Value.Title} by {item.Value.Owner.Name}" );
	///     Console.WriteLine( $"{item.Value.VotesUp} up / {item.Value.VotesDown} down" );
	///
	///     if ( !item.Value.IsSubscribed )
	///         await item.Value.Subscribe();
	///
	///     // subscribing queues a download; wait for the content before using it
	///     await item.Value.DownloadAsync( SteamClient.AppId );
	///     Console.WriteLine( $"installed to {new Ugc.Item( fileId ).Directory}" );
	/// }
	/// </code>
	/// </example>
	public struct Item
	{
		internal SteamUGCDetails_t details;
		internal PublishedFileId _id;

		/// <summary>
		/// Wraps a published file id so you can ask about its local install state. Cheap and
		/// synchronous &#8212; it performs no network call and fetches no details.
		/// </summary>
		/// <param name="id">The Workshop item's published file id.</param>
		/// <remarks>
		/// An item built this way has <b>no details</b>. <see cref="Title"/>,
		/// <see cref="Description"/>, <see cref="Owner"/>, <see cref="Tags"/>, <see cref="Score"/>,
		/// <see cref="Created"/> and the vote counts are all empty or zero until the item comes back
		/// from a query. Use <see cref="GetAsync"/> if you need any of those. The id is not validated,
		/// so a nonexistent id constructs successfully and simply reports as not installed.
		/// </remarks>
		public Item( PublishedFileId id ) : this()
		{
			_id = id;
		}

		/// <summary>
		/// The actual ID of this file
		/// </summary>
		public PublishedFileId Id => _id;

		/// <summary>
		/// The given title of this item
		/// </summary>
		public string Title { get; internal set; }

		/// <summary>
		/// The description of this item, in your local language if available
		/// </summary>
		public string Description { get; internal set; }

		/// <summary>
		/// A list of tags for this item, all lowercase
		/// </summary>
		public string[] Tags { get; internal set; }

		/// <summary>
		/// A dictionary of key value tags for this item, only available from queries WithKeyValueTags(true)
		/// </summary>
		public Dictionary<string,string> KeyValueTags { get; internal set; }

		/// <summary>
		/// App Id of the app that created this item
		/// </summary>
		public AppId CreatorApp => details.CreatorAppID;

		/// <summary>
		/// App Id of the app that will consume this item.
		/// </summary>
		public AppId ConsumerApp => details.ConsumerAppID;

		/// <summary>
		/// User who created this content
		/// </summary>
		public Friend Owner => new Friend( details.SteamIDOwner );

		/// <summary>
		/// The bayesian average for up votes / total votes, between [0,1]
		/// </summary>
		public float Score => details.Score;

		/// <summary>
		/// Time when the published item was created
		/// </summary>
		public DateTime Created => Epoch.ToDateTime( details.TimeCreated );

		/// <summary>
		/// Time when the published item was last updated
		/// </summary>
		public DateTime Updated => Epoch.ToDateTime( details.TimeUpdated );

		/// <summary>
		/// True if this is publically visible
		/// </summary>
		public bool IsPublic => details.Visibility == RemoteStoragePublishedFileVisibility.Public;

		/// <summary>
		/// True if this item is only visible by friends of the creator
		/// </summary>
		public bool IsFriendsOnly => details.Visibility == RemoteStoragePublishedFileVisibility.FriendsOnly;

		/// <summary>
		/// True if this is only visible to the creator
		/// </summary>
		public bool IsPrivate => details.Visibility == RemoteStoragePublishedFileVisibility.Private;
		
		/// <summary>
		/// True if this item has been banned
		/// </summary>
		public bool IsBanned => details.Banned;

		/// <summary>
		/// Whether the developer of this app has specifically flagged this item as accepted in the Workshop
		/// </summary>
		public bool IsAcceptedForUse => details.AcceptedForUse;

        /// <summary>
        /// The number of upvotes of this item
        /// </summary>
        public uint VotesUp => details.VotesUp;

        /// <summary>
        /// The number of downvotes of this item
        /// </summary>
        public uint VotesDown => details.VotesDown;
		/// <summary>
		/// Dependencies/children of this item or collection, available only from WithDependencies(true) queries
		/// </summary>
		public PublishedFileId[] Children;

		/// <summary>
		/// Additional previews of this item or collection, available only from WithAdditionalPreviews(true) queries
		/// </summary>
		public UgcAdditionalPreview[] AdditionalPreviews { get; internal set; }

        /// <summary>
        /// Whether the item's content is on disk and usable. Works on a bare constructed item &#8212;
        /// this asks the local Steam client, not the Workshop backend.
        /// </summary>
        /// <remarks>
        /// Installed does not mean current. Valve's header notes an item can be "installed and usable
        /// (but maybe out of date)", so check <see cref="NeedsUpdate"/> as well before loading it.
        /// </remarks>
        public bool IsInstalled => (State & ItemState.Installed) == ItemState.Installed;

		/// <summary>
		/// Whether Steam is transferring this item's content right now. Use it to drive a progress
		/// bar alongside <see cref="DownloadAmount"/>.
		/// </summary>
		public bool IsDownloading => (State & ItemState.Downloading) == ItemState.Downloading;

		/// <summary>
		/// Whether a download has been requested but has not started transferring yet &#8212; queued
		/// behind other downloads, or waiting on Steam. Valve's header ties this to
		/// "DownloadItem() was called for this item, content isn't available until
		/// DownloadItemResult_t is fired".
		/// </summary>
		public bool IsDownloadPending => (State & ItemState.DownloadPending) == ItemState.DownloadPending;

		/// <summary>
		/// Whether the local user is subscribed to this item. Subscribing is what makes Steam download
		/// it and keep it updated; it is the durable "the player wants this" flag, as opposed to
		/// <see cref="IsInstalled"/> which is just "the bytes are here".
		/// </summary>
		public bool IsSubscribed => (State & ItemState.Subscribed) == ItemState.Subscribed;

		/// <summary>
		/// Whether the installed content is stale and should be re-downloaded. Valve's header gives two
		/// causes: the item is not installed yet, or the creator has updated it since.
		/// </summary>
		/// <remarks>
		/// This is <see langword="true"/> for an item that has never been installed, so it is not by
		/// itself a signal that an existing install went stale &#8212; pair it with
		/// <see cref="IsInstalled"/>.
		/// </remarks>
		public bool NeedsUpdate => (State & ItemState.NeedsUpdate) == ItemState.NeedsUpdate;

		/// <summary>
		/// The absolute path of the folder Steam installed this item's content into, or
		/// <see langword="null"/> if it is not installed. This is the path your game loads the mod
		/// from.
		/// </summary>
		/// <remarks>
		/// <para>
		/// Do not cache this across sessions or across an update. Steam may move content between
		/// library folders, and the path changes when the item is reinstalled. Re-read it each time.
		/// </para>
		/// <para>
		/// <see langword="null"/> means "no install info", which covers not subscribed, not yet
		/// downloaded, and a genuinely failed install &#8212; the underlying call gives no way to tell
		/// them apart.
		/// </para>
		/// </remarks>
		public string Directory 
		{
			get
			{
				ulong size = 0;
				uint ts = 0;

				if ( !SteamUGC.Internal.GetItemInstallInfo( Id, ref size, out var strVal, ref ts ) )
					return null;

				return strVal;
			}
		}

		/// <summary>
		/// Start downloading this item and return immediately.
		/// If this returns false the item isn't getting downloaded.
		/// Completion is signalled through <see cref="Steamworks.SteamUGC.OnDownloadItemResult"/>.
		/// </summary>
		/// <param name="highPriority">
		/// If <see langword="true"/>, Steam suspends every other Workshop download until this one
		/// completes. Leave it off for background or bulk downloads.
		/// </param>
		/// <returns>
		/// <see langword="true"/> if the download was queued. Valve does not document what makes this
		/// return <see langword="false"/>; an invalid id and an app with no Workshop configured are
		/// the observed causes.
		/// </returns>
		/// <remarks>
		/// Do not read the item's files between this returning and the completion event: if the item
		/// was already installed, Steam overwrites the content in place.
		/// </remarks>
		public bool Download( bool highPriority = false )
		{
			return SteamUGC.Download( Id, highPriority );
		}

		/// <summary>
		/// If we're downloading, how big the total download is, in bytes.
		/// When the item is already up to date this reports <see cref="SizeBytes"/> instead, so a
		/// progress bar reads as complete rather than as unknown.
		/// </summary>
		/// <returns>
		/// Total bytes, or <c>-1</c> if Steam has no download info for this item &#8212; which is the
		/// case before a download has actually begun.
		/// </returns>
		/// <remarks>
		/// This and <see cref="DownloadBytesDownloaded"/> and <see cref="SizeBytes"/> each re-read the
		/// item's live state from Steam and delegate to one another depending on
		/// <see cref="NeedsUpdate"/>. If that flag changes between two of those reads the delegation
		/// can bounce back and forth, so treat these three as a cheap snapshot for UI, not as values
		/// to poll in a tight loop.
		/// </remarks>
		public long DownloadBytesTotal
		{
			get
			{
				if ( !NeedsUpdate )
					return SizeBytes;

				ulong downloaded = 0;
				ulong total = 0;
				if ( SteamUGC.Internal.GetItemDownloadInfo( Id, ref downloaded, ref total ) )
					return (long) total;

				return -1;
			}
		}

		/// <summary>
		/// If we're downloading, how much we've downloaded, in bytes. Reports
		/// <see cref="SizeBytes"/> when the item is already up to date.
		/// </summary>
		/// <returns>Bytes transferred so far, or <c>-1</c> if Steam has no download info yet.</returns>
		public long DownloadBytesDownloaded
		{
			get
			{
				if ( !NeedsUpdate )
					return SizeBytes;

				ulong downloaded = 0;
				ulong total = 0;
				if ( SteamUGC.Internal.GetItemDownloadInfo( Id, ref downloaded, ref total ) )
					return (long)downloaded;

				return -1;
			}
		}

		/// <summary>
		/// If we're installed, how big is the install, in bytes on disk.
		/// </summary>
		/// <returns>
		/// The installed size, or <c>0</c> if the item is not installed. Note the inconsistency with
		/// <see cref="DownloadBytesTotal"/>, which uses <c>-1</c> for its "no data" case.
		/// </returns>
		public long SizeBytes
		{
			get
			{
				if ( NeedsUpdate )
					return DownloadBytesDownloaded;

				ulong size = 0;
				uint ts = 0;
				if ( !SteamUGC.Internal.GetItemInstallInfo( Id, ref size, out _, ref ts ) )
					return 0;

				return (long) size;
			}
		}

		/// <summary>
		/// If we're downloading our current progress as a delta betwen 0-1. Drive a progress bar with
		/// this while <see cref="IsDownloading"/> is true.
		/// </summary>
		/// <returns>
		/// A fraction between 0 and 1. Returns <c>1</c> when no download is in progress &#8212;
		/// including for an item that is not installed and was never requested, so this alone is not
		/// evidence that content is present. Check <see cref="IsInstalled"/> for that.
		/// </returns>
		public float DownloadAmount
		{
			get
			{
				//changed from NeedsUpdate as it's false when validating and redownloading ugc
				//possibly similar properties should also be changed
				if ( !IsDownloading ) return 1;

				ulong downloaded = 0;
				ulong total = 0;
				if ( SteamUGC.Internal.GetItemDownloadInfo( Id, ref downloaded, ref total ) && total > 0 )
					return (float)((double)downloaded / (double)total);

				if ( NeedsUpdate || !IsInstalled || IsDownloading )
					return 0;

				return 1;
			}
		}

		private ItemState State => (ItemState) SteamUGC.Internal.GetItemState( Id );

		/// <summary>
		/// Fetch one Workshop item's full details from Steam. This is the async counterpart to the
		/// <see cref="Item(Steamworks.Data.PublishedFileId)"/> constructor: it costs a round trip, and
		/// in exchange you get a populated <see cref="Title"/>, <see cref="Description"/>,
		/// <see cref="Owner"/> and the rest.
		/// </summary>
		/// <param name="id">The Workshop item to fetch.</param>
		/// <param name="maxageseconds">
		/// How stale a cached copy may be before Steam re-queries the backend, in seconds. Defaults to
		/// 30 minutes. Pass 0 to insist on fresh data.
		/// </param>
		/// <returns>
		/// The item, or <see langword="null"/> if the query failed or matched nothing &#8212; which is
		/// what an id that does not exist, or belongs to another app, produces. The two cases are not
		/// distinguishable here.
		/// </returns>
		/// <remarks>
		/// Requests the long description, so <see cref="Description"/> is not truncated. It does not
		/// request metadata, children, key/value tags or additional previews &#8212; if you need any of
		/// those, build a <see cref="Query"/> yourself with the matching <c>With*</c> switch.
		/// </remarks>
		public static async Task<Item?> GetAsync( PublishedFileId id, int maxageseconds = 60 * 30 )
		{
			var file = await Steamworks.Ugc.Query.All
											.WithFileId( id )
											.WithLongDescription( true )
											.AllowCachedResponse( maxageseconds )
											.GetPageAsync( 1 );

			if ( !file.HasValue ) return null;
			using ( file.Value )
			{
				if ( file.Value.ResultCount == 0 ) return null;

				return file.Value.Entries.First();
			}
		}

		internal static Item From( SteamUGCDetails_t details )
		{
			var d = new Item
			{
				_id = details.PublishedFileId,
				details = details,
				Title = details.TitleUTF8(),
				Description = details.DescriptionUTF8(),
				Tags = details.TagsUTF8().ToLower().Split( new[] { ',' }, StringSplitOptions.RemoveEmptyEntries )
			};

			return d;
		}

		/// <summary>
		/// A case insensitive check for tag. Tags are how Workshop content is categorised, so this is
		/// the usual way to decide whether an item belongs in a particular part of your UI.
		/// </summary>
		/// <param name="find">The tag to look for. Compared with <c>OrdinalIgnoreCase</c>.</param>
		/// <returns>
		/// <see langword="true"/> if the item carries the tag. <see langword="false"/> if it does not,
		/// and also if the item has no tag data at all &#8212; which is the case for an item built
		/// from the <see cref="Item(Steamworks.Data.PublishedFileId)"/> constructor rather than
		/// returned by a query.
		/// </returns>
		public bool HasTag( string find )
		{
			if ( Tags == null || Tags.Length == 0 ) return false;

			return Tags.Contains( find, StringComparer.OrdinalIgnoreCase );
		}

        /// <summary>
        /// Subscribe the local user to this item. Subscribing is the durable "I want this" flag:
        /// Steam downloads the content and keeps it updated across sessions and machines.
        /// </summary>
        /// <returns>
        /// <see langword="true"/> if Steam accepted the subscription; <see langword="false"/> if the
        /// call failed or returned a non-OK result.
        /// </returns>
        /// <remarks>
        /// The content is <b>not</b> on disk when this returns &#8212; subscribing only queues the
        /// download. Await <see cref="DownloadAsync"/> or wait for
        /// <see cref="Steamworks.SteamUGC.OnItemInstalled"/> before reading files.
        /// </remarks>
        public async Task<bool> Subscribe ()
        {
            var result = await SteamUGC.Internal.SubscribeItem( _id );
            return result?.Result == Result.OK;
        }

		/// <summary>
		/// Download this item's content and await its installation, reporting progress as it goes.
		/// Convenience wrapper over <see cref="Steamworks.SteamUGC.DownloadAsync"/>.
		/// </summary>
		/// <param name="appId">
		/// The app the item belongs to, used to match the completion callback. For ordinary Workshop
		/// content pass <see cref="SteamClient.AppId"/>. The wrong value here means the completion is
		/// never matched and the call waits until cancelled.
		/// </param>
		/// <param name="progress">
		/// Optional callback receiving (fraction complete 0-1, bytes downloaded, bytes total).
		/// </param>
		/// <param name="milisecondsUpdateDelay">Polling interval for progress, in milliseconds.</param>
		/// <param name="ct">
		/// Cancels waiting. Cancelling does not stop Steam downloading the item, only this method's
		/// wait for it.
		/// </param>
		/// <returns>
		/// <see langword="true"/> if the item ended up installed, otherwise <see langword="false"/>
		/// &#8212; including when cancelled or when the download failed.
		/// </returns>
		/// <remarks>
		/// Contrary to the wording this method previously carried, there is <b>no</b> built-in
		/// 60-second timeout. The <c>60</c> default is the progress polling interval in milliseconds.
		/// Pass a <see cref="CancellationToken"/> if you need this to give up.
		/// </remarks>
		public async Task<bool> DownloadAsync( AppId appId, Action<float, long, long> progress = null, int milisecondsUpdateDelay = 60, CancellationToken ct = default )
		{
			return await SteamUGC.DownloadAsync( appId, Id, progress, milisecondsUpdateDelay, ct );
		}

		/// <summary>
		/// Unsubscribe the local user from this item, so Steam stops keeping it updated and
		/// eventually removes it.
		/// </summary>
		/// <returns>
		/// <see langword="true"/> if Steam accepted the change; <see langword="false"/> on failure or
		/// a non-OK result.
		/// </returns>
		/// <remarks>
		/// The files stay on disk for now. Valve's header says an unsubscribed item "will be
		/// uninstalled after game quits", so content already loaded keeps working for this session.
		/// </remarks>
		public async Task<bool> Unsubscribe ()
        {
            var result = await SteamUGC.Internal.UnsubscribeItem( _id );
            return result?.Result == Result.OK;
        }

        /// <summary>
        /// Add this item to the user's Workshop favourites. Favouriting is a bookmark shown on the
        /// user's profile; unlike subscribing it does not cause Steam to download anything.
        /// </summary>
        /// <returns>
        /// <see langword="true"/> if Steam accepted the change; <see langword="false"/> on failure or
        /// a non-OK result.
        /// </returns>
        /// <remarks>
        /// This uses the item's consumer app id, which is only populated on an item that came from a
        /// query. Called on an item built from the
        /// <see cref="Item(Steamworks.Data.PublishedFileId)"/> constructor it sends app id 0 and
        /// fails.
        /// </remarks>
	    public async Task<bool> AddFavorite()
	    {
	        var result = await SteamUGC.Internal.AddItemToFavorites(details.ConsumerAppID, _id);
	        return result?.Result == Result.OK;
	    }

	    /// <summary>
	    /// Remove this item from the user's Workshop favourites.
	    /// </summary>
	    /// <returns>
	    /// <see langword="true"/> if Steam accepted the change; <see langword="false"/> on failure or
	    /// a non-OK result.
	    /// </returns>
	    /// <remarks>
	    /// Carries the same consumer-app-id caveat as <see cref="AddFavorite"/>.
	    /// </remarks>
        public async Task<bool> RemoveFavorite()
	    {
	        var result = await SteamUGC.Internal.RemoveItemFromFavorites(details.ConsumerAppID, _id);
	        return result?.Result == Result.OK;
	    }

        /// <summary>
        /// Cast the local user's vote on this item. Votes feed the item's <see cref="Score"/> and the
        /// <c>RankedByVote</c> orderings, and they are public on the user's profile.
        /// </summary>
        /// <param name="up"><see langword="true"/> for a thumbs up, <see langword="false"/> for a thumbs down.</param>
        /// <returns>
        /// The result code Steam returned, or <see langword="null"/> if the call itself failed. Unlike
        /// most methods on this type you get the actual <see cref="Steamworks.Result"/>, so you can
        /// tell a rejection apart from a transport failure.
        /// </returns>
        /// <remarks>
        /// Voting again replaces the previous vote rather than adding to it. Valve does not document
        /// whether there is a rate limit on voting.
        /// </remarks>
        public async Task<Result?> Vote( bool up )
		{
			var r = await SteamUGC.Internal.SetUserItemVote( Id, up );
			return r?.Result;
		}

        /// <summary>
        /// Read back how the local user has already voted on this item, so your UI can show the
        /// current state rather than offering a fresh vote.
        /// </summary>
        /// <returns>
        /// The user's vote, or <see langword="null"/> if the call failed. A user who has never voted
        /// is reported through the returned value's fields, not by returning null.
        /// </returns>
	    public async Task<UserItemVote?> GetUserVote()
	    {
	        var result = await SteamUGC.Internal.GetUserItemVote(_id);
	        if (!result.HasValue)
	            return null;
	        return UserItemVote.From(result.Value);
	    }

        /// <summary>
        /// Return a URL to view this item online
        /// </summary>
        public string Url => $"http://steamcommunity.com/sharedfiles/filedetails/?source=Facepunch.Steamworks&id={Id}";

		/// <summary>
		/// The URl to view this item's changelog
		/// </summary>
		public string ChangelogUrl => $"http://steamcommunity.com/sharedfiles/filedetails/changelog/{Id}";

		/// <summary>
		/// The URL to view the comments on this item
		/// </summary>
		public string CommentsUrl => $"http://steamcommunity.com/sharedfiles/filedetails/comments/{Id}";

		/// <summary>
		/// The URL to discuss this item
		/// </summary>
		public string DiscussUrl => $"http://steamcommunity.com/sharedfiles/filedetails/discussions/{Id}";

		/// <summary>
		/// The URL to view this items stats online
		/// </summary>
		public string StatsUrl => $"http://steamcommunity.com/sharedfiles/filedetails/stats/{Id}";

		/// <summary>
		/// Total number of subscriptions this item has ever received, counting repeat subscriptions by
		/// the same user. See <see cref="NumUniqueSubscriptions"/> for the deduplicated figure, which
		/// is the better popularity signal.
		/// </summary>
		/// <remarks>
		/// Every statistic on this item is 0 unless the query that produced it had
		/// <see cref="Query.WithDefaultStats"/> left on (it is on by default) &#8212; and 0 is
		/// indistinguishable from a genuinely unused item. On an item built from the
		/// <see cref="Item(Steamworks.Data.PublishedFileId)"/> constructor they are always 0.
		/// </remarks>
		public ulong NumSubscriptions { get; internal set; }

		/// <summary>
		/// Total number of times this item has been favourited, counting repeats.
		/// </summary>
		public ulong NumFavorites { get; internal set; }

		/// <summary>
		/// Total number of times users have followed this item to be notified of its updates.
		/// </summary>
		public ulong NumFollowers { get; internal set; }

		/// <summary>
		/// Number of distinct users who have subscribed to this item. This is the figure to show as
		/// "subscribers"; it does not double-count a user who unsubscribed and subscribed again.
		/// </summary>
		public ulong NumUniqueSubscriptions { get; internal set; }

		/// <summary>
		/// Number of distinct users who have favourited this item.
		/// </summary>
		public ulong NumUniqueFavorites { get; internal set; }

		/// <summary>
		/// Number of distinct users who follow this item.
		/// </summary>
		public ulong NumUniqueFollowers { get; internal set; }

		/// <summary>
		/// Number of distinct users who have opened this item's page on the Steam community website.
		/// Counts web views only, not views inside your game.
		/// </summary>
		public ulong NumUniqueWebsiteViews { get; internal set; }

		/// <summary>
		/// How much this item has been reported by users, as an opaque score. Useful for building
		/// moderation tooling; Valve does not document the scale or how it is computed.
		/// </summary>
		public ulong ReportScore { get; internal set; }

		/// <summary>
		/// Total seconds this item has been played across all users, as reported by playtime tracking.
		/// </summary>
		/// <remarks>
		/// Zero unless your game calls
		/// <see cref="Steamworks.SteamUGC.StartPlaytimeTracking"/> and
		/// <see cref="Steamworks.SteamUGC.StopPlaytimeTracking"/>. A game that never tracks playtime
		/// sees zero here forever, which reads as "never played" rather than "never measured".
		/// </remarks>
		public ulong NumSecondsPlayed { get; internal set; }

		/// <summary>
		/// Total number of play sessions recorded for this item across all users. Requires playtime
		/// tracking; see <see cref="NumSecondsPlayed"/>.
		/// </summary>
		public ulong NumPlaytimeSessions { get; internal set; }

		/// <summary>
		/// Number of comments left on this item's Workshop page.
		/// </summary>
		public ulong NumComments { get; internal set; }

		/// <summary>
		/// Seconds played within the window requested by <see cref="Query.WithPlaytimeStats"/>.
		/// Zero unless that was called <i>and</i> your game reports playtime.
		/// </summary>
		public ulong NumSecondsPlayedDuringTimePeriod { get; internal set; }

		/// <summary>
		/// Play sessions within the window requested by <see cref="Query.WithPlaytimeStats"/>.
		/// Zero unless that was called <i>and</i> your game reports playtime.
		/// </summary>
		public ulong NumPlaytimeSessionsDuringTimePeriod { get; internal set; }

		/// <summary>
		/// The URL to the preview image for this item
		/// </summary>
		public string PreviewImageUrl { get; internal set; }

		/// <summary>
		/// The metadata string for this item, only available from queries WithMetadata(true)
		/// </summary>
		public string Metadata { get; internal set; }

		/// <summary>
		/// Begin editing this item, returning a builder you configure and then submit. This is how you
		/// update content the local user already published &#8212; new files, a new title, new tags.
		/// </summary>
		/// <returns>
		/// An <see cref="Editor"/> targeting this item's id. Creating it is free and local; nothing is
		/// sent to Steam until you submit the editor.
		/// </returns>
		/// <remarks>
		/// Only the item's owner can commit an edit. The editor starts empty rather than pre-filled
		/// with this item's current values, so fields you do not set are left unchanged on Steam
		/// rather than being cleared.
		/// </remarks>
		public Ugc.Editor Edit()
		{
			return new Ugc.Editor( Id );
		}

		/// <summary>
		/// Record that this item depends on, or contains, another item. This is how a collection gets
		/// its members, and how a mod declares that it needs another mod present.
		/// </summary>
		/// <param name="child">The item to add as a child or dependency of this one.</param>
		/// <returns>
		/// <see langword="true"/> if Steam accepted the change; <see langword="false"/> if the call
		/// failed or returned a non-OK result &#8212; most often because the caller does not own this
		/// item.
		/// </returns>
		/// <remarks>
		/// Only the item's owner can change its dependencies. Adding a dependency does not cause Steam
		/// to download the child automatically; subscribing to a collection does, but a plain
		/// dependency is informational and your game is responsible for acting on it. To read
		/// dependencies back, query with <see cref="Query.WithChildren"/> and look at
		/// <see cref="Children"/>.
		/// </remarks>
		public async Task<bool> AddDependency( PublishedFileId child )
		{
			var r = await SteamUGC.Internal.AddDependency( Id, child );
			return r?.Result == Result.OK;
		}

		/// <summary>
		/// Remove a dependency or collection membership previously created with
		/// <see cref="AddDependency"/>.
		/// </summary>
		/// <param name="child">The child item to detach from this one.</param>
		/// <returns>
		/// <see langword="true"/> if Steam accepted the change; <see langword="false"/> on failure or
		/// a non-OK result. Removing a dependency that was never there is not distinguished from
		/// success.
		/// </returns>
		public async Task<bool> RemoveDependency( PublishedFileId child )
		{
			var r = await SteamUGC.Internal.RemoveDependency( Id, child );
			return r?.Result == Result.OK;
		}

		/// <summary>
		/// The per-item result code Steam returned when this item's details were fetched. An item in a
		/// result page can individually fail even though the query as a whole succeeded &#8212; for
		/// example if it has been deleted since it was indexed.
		/// </summary>
		/// <remarks>
		/// Only meaningful on an item that came from a query. On an item built from the
		/// <see cref="Item(Steamworks.Data.PublishedFileId)"/> constructor this is
		/// <see cref="Steamworks.Result.None"/> because no details were ever fetched &#8212; not
		/// because anything went wrong.
		/// </remarks>
		public Result Result => details.Result;
	}
}
