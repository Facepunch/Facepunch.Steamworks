using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Steamworks.Data;

namespace Steamworks
{
	/// <summary>
	/// The Steam Workshop: querying, downloading, publishing and tracking user generated content.
	/// <para>
	/// This class holds the app-wide operations. The pieces you actually build a Workshop feature out
	/// of live next to it: <see cref="Ugc.Query"/> to search, <see cref="Ugc.Item"/> to inspect and
	/// subscribe, and <see cref="Ugc.Editor"/> to publish.
	/// </para>
	/// </summary>
	/// <remarks>
	/// <para>
	/// Workshop must be enabled for your app on the Steamworks partner site before any of this returns
	/// anything. On an app with no Workshop configured, queries succeed and return zero results rather
	/// than reporting an error &#8212; which is easy to mistake for a bug in your code. Valve does not
	/// document this anywhere in <c>isteamugc.h</c>.
	/// </para>
	/// <para>
	/// Everything asynchronous here needs callbacks to be running. That is automatic when you
	/// initialise with <c>SteamClient.Init( appid, asyncCallbacks: true )</c>; otherwise pump
	/// <c>SteamClient.RunCallbacks()</c> yourself or the returned tasks never complete.
	/// </para>
	/// </remarks>
	/// <example>
	/// A minimal "download everything the player is subscribed to" pass, which is what most games
	/// need at startup:
	/// <code>
	/// var subscribed = new List&lt;PublishedFileId&gt;();
	/// SteamUGC.GetSubscribedItems( subscribed );
	///
	/// foreach ( var id in subscribed )
	/// {
	///     var item = new Ugc.Item( id );
	///     if ( item.IsInstalled &amp;&amp; !item.NeedsUpdate )
	///     {
	///         Load( item.Directory );
	///         continue;
	///     }
	///
	///     if ( await SteamUGC.DownloadAsync( SteamClient.AppId, id ) )
	///         Load( new Ugc.Item( id ).Directory );
	/// }
	/// </code>
	/// </example>
	public class SteamUGC : SteamSharedClass<SteamUGC>
	{
		internal static ISteamUGC Internal => Interface as ISteamUGC;

		internal override bool InitializeInterface( bool server )
		{
			SetInterface( server, new ISteamUGC( server ) );
			if ( Interface.Self == IntPtr.Zero ) return false;

			InstallEvents( server );

			return true;
		}

		internal static void InstallEvents( bool server )
		{
			Dispatch.Install<DownloadItemResult_t>( x => OnDownloadItemResult?.Invoke( x.AppID.Value, x.PublishedFileId, x.Result ), server );
			Dispatch.Install<RemoteStoragePublishedFileSubscribed_t>( x => OnItemSubscribed?.Invoke( x.AppID.Value, x.PublishedFileId ), server );
			Dispatch.Install<RemoteStoragePublishedFileUnsubscribed_t>( x => OnItemUnsubscribed?.Invoke( x.AppID.Value, x.PublishedFileId ), server );
			Dispatch.Install<ItemInstalled_t>( x => OnItemInstalled?.Invoke( x.AppID.Value, x.PublishedFileId ), server );
		}

		/// <summary>
		/// Invoked when a Workshop download finishes, successfully or not. The arguments are the app
		/// the item belongs to, the item's id, and the outcome.
		/// </summary>
		/// <remarks>
		/// <para>
		/// This fires for every completed download, including ones Steam started on its own (an
		/// auto-update to a subscribed item), not only ones you asked for with <see cref="Download"/>.
		/// Filter on the id you care about.
		/// </para>
		/// <para>
		/// Valve's header warns that until this fires, "files on disk should not be used" for an item
		/// that was already installed &#8212; the update is written in place. Do not read an item's
		/// files between starting a download and seeing this event.
		/// </para>
		/// <para>
		/// A <see cref="Result"/> other than <see cref="Result.OK"/> means the content is not on disk.
		/// The previous version may or may not still be usable; Valve does not say.
		/// </para>
		/// </remarks>
		public static event Action<AppId, PublishedFileId, Result> OnDownloadItemResult;

		/// <summary>
		/// Invoked when the user subscribes to an item &#8212; usually because they clicked Subscribe
		/// in the Steam overlay while your game was running. Subscribing is what makes Steam download
		/// and keep an item up to date.
		/// </summary>
		/// <remarks>
		/// The content is <b>not</b> on disk yet when this fires. Steam queues a download; wait for
		/// <see cref="OnItemInstalled"/> (or <see cref="OnDownloadItemResult"/>) before reading files.
		/// </remarks>
		public static event Action<AppId, PublishedFileId> OnItemSubscribed;

		/// <summary>
		/// Invoked when the user unsubscribes from an item.
		/// </summary>
		/// <remarks>
		/// The files are still on disk when this fires. Valve's header states an unsubscribed item
		/// "will be uninstalled after game quits", so a game that keeps using the content until it
		/// shuts down will not fail &#8212; but you should stop offering the item to the player.
		/// </remarks>
		public static event Action<AppId, PublishedFileId> OnItemUnsubscribed;

		/// <summary>
		/// Invoked when an item's content has finished installing and its files are safe to read.
		/// This is the event to react to if you want to hot-load newly subscribed content.
		/// </summary>
		public static event Action<AppId, PublishedFileId> OnItemInstalled;

		/// <summary>
		/// Permanently delete a Workshop item you published. There is no confirmation prompt and no
		/// undo &#8212; Valve's header describes it as deleting "without prompting the user".
		/// </summary>
		/// <param name="fileId">The item to delete.</param>
		/// <returns>
		/// <see langword="true"/> only if Steam confirmed the deletion. <see langword="false"/> covers
		/// every failure without distinguishing them: the call failed outright, or it completed with a
		/// non-OK result because you do not own the item, it does not exist, or you are not logged in.
		/// </returns>
		/// <remarks>
		/// Only the item's owner (or an app with the right partner permissions) can delete it.
		/// Deleting removes the item for everyone subscribed to it.
		/// </remarks>
		public static async Task<bool> DeleteFileAsync( PublishedFileId fileId )
		{
			var r = await Internal.DeleteItem( fileId );
			return r?.Result == Result.OK;
		}

		/// <summary>
		/// Start downloading this item and return immediately. You'll get notified of completion via
		/// <see cref="OnDownloadItemResult"/>. Use this when you want to drive the download from your
		/// own event handling; use <see cref="DownloadAsync"/> if you would rather await it.
		/// </summary>
		/// <param name="fileId">The ID of the file to download.</param>
		/// <param name="highPriority">
		/// If <see langword="true"/> this should go straight to the top of the download list. Valve's
		/// header is stronger than that: high priority "will suspend any other item download" until
		/// this one finishes. Do not set it for bulk background downloads.
		/// </param>
		/// <returns>
		/// <see langword="true"/> if nothing went wrong and the download is started. Valve does not
		/// document the conditions that produce <see langword="false"/>; observed causes are an
		/// invalid id and an app with no Workshop configured.
		/// </returns>
		/// <remarks>
		/// <para>
		/// The download has only been <i>queued</i> when this returns. If the item was already
		/// installed, its files are being overwritten in place, and Valve's header says they "should
		/// not be used until callback received".
		/// </para>
		/// <para>
		/// You do not have to be subscribed to an item to download it. Valve notes an unsubscribed
		/// item "will be cached for some time" &#8212; how long is not specified, so do not rely on
		/// unsubscribed content staying on disk.
		/// </para>
		/// </remarks>
		public static bool Download( PublishedFileId fileId, bool highPriority = false )
		{
			return Internal.DownloadItem( fileId, highPriority );
		}

		/// <summary>
		/// Download an item and await its installation, reporting progress along the way. This wraps
		/// the callback-driven <see cref="Download"/> into a single awaitable call, which is usually
		/// what you want when loading subscribed content at startup.
		/// </summary>
		/// <param name="appId">
		/// The app the item belongs to, used to filter the completion callback. For ordinary Workshop
		/// content this is your own <see cref="SteamClient.AppId"/>. Passing the wrong id means the
		/// completion callback is never matched and the call runs until it is cancelled.
		/// </param>
		/// <param name="fileId">The ID of the file you download.</param>
		/// <param name="progress">
		/// Optional callback invoked repeatedly with (fraction complete 0-1, bytes downloaded, bytes
		/// total). The fraction is shaped for UI rather than exact: it reports 0 immediately, then
		/// 0.1 while queued, then 0.1 to 0.95 while transferring, then exactly 1 on success. Byte
		/// counts are 0 until Steam has started the transfer.
		/// </param>
		/// <param name="milisecondsUpdateDelay">
		/// How often to poll and call the progress function, in milliseconds. This is a polling loop,
		/// not an event subscription, so very small values busy-wait.
		/// </param>
		/// <param name="ct">
		/// Allows to send a message to cancel the download anywhere during the process. Cancelling
		/// stops this method waiting; it does <b>not</b> stop Steam downloading the item.
		/// </param>
		/// <returns>
		/// <see langword="true"/> if downloaded and installed properly, otherwise
		/// <see langword="false"/> &#8212; including when cancelled, when Steam refused to start the
		/// download, and when the download completed with an error.
		/// </returns>
		/// <remarks>
		/// This method always downloads at high priority, which suspends other Workshop downloads
		/// while it runs. Awaiting it for many items in sequence is fine; running many in parallel is
		/// not, because each one preempts the others.
		/// </remarks>
		public static async Task<bool> DownloadAsync( AppId appId, PublishedFileId fileId, Action<float, long, long> progress = null, int milisecondsUpdateDelay = 60, CancellationToken ct = default )
		{
			var item = new Steamworks.Ugc.Item( fileId );

			progress?.Invoke( 0.0f , 0, 0);

			Result result = Result.None;

			Action<AppId, PublishedFileId, Result> onDownloadStarted = ( appIdCallback, fileIdInCallback, resultInCallback ) =>
			{
				if ( appIdCallback == appId && fileIdInCallback == fileId )
					result = resultInCallback;
			};

			SteamUGC.OnDownloadItemResult += onDownloadStarted;
			if ( SteamUGC.Download( fileId, true ) == false )
			{
				SteamUGC.OnDownloadItemResult -= onDownloadStarted;
				return item.IsInstalled;
			}

			try
			{
				while ( true )
				{
					if ( ct != default && ct.IsCancellationRequested )
						break;

					if ( !item.IsDownloading )
						progress?.Invoke( 0.1f , 0, 0);
					else
						progress?.Invoke( 0.1f + item.DownloadAmount * 0.85f , item.DownloadBytesDownloaded, item.DownloadBytesTotal);

					if ( !item.IsDownloading && item.IsInstalled && result != Result.None )
						break;

					//
					// A failed DownloadItemResult_t never installs the item, so the success
					// condition above can never become true. Without this the loop spins for
					// ever on any download failure and the check below is unreachable.
					//
					if ( result != Result.None && result != Result.OK )
						break;

					await Task.Delay( milisecondsUpdateDelay );
				}

				if ( result != Result.OK && result != Result.None )
					return false;

				if ( ct.IsCancellationRequested )
					return false;
			}
			finally
			{
				SteamUGC.OnDownloadItemResult -= onDownloadStarted;
			}

			progress?.Invoke( 1.0f, item.DownloadBytesTotal, item.DownloadBytesTotal);
			return item.IsInstalled;
		}

		/// <summary>
		/// Fetch the full details of one Workshop item by id &#8212; title, description, owner, tags,
		/// vote counts. Use this to turn an id you have stored into something you can show a player.
		/// </summary>
		/// <param name="fileId">The item to look up.</param>
		/// <returns>
		/// The item, or <see langword="null"/> if the query failed or did not return exactly one
		/// result &#8212; which is what you get for an id that does not exist or belongs to another
		/// app. There is no way to tell those cases apart here.
		/// </returns>
		/// <remarks>
		/// <para>
		/// This is a convenience wrapper over <see cref="Ugc.Query"/> with
		/// <see cref="Ugc.Query.WithFileId"/>. It runs a full query round trip per call, so fetching
		/// many items one at a time is markedly slower than passing all the ids to
		/// <see cref="Ugc.Query.WithFileId"/> in one go.
		/// </para>
		/// <para>
		/// Only the default fields are returned. If you need metadata, children, key/value tags or
		/// additional previews, build the query yourself and turn the matching <c>With*</c> switch on.
		/// </para>
		/// </remarks>
		public static async Task<Ugc.Item?> QueryFileAsync( PublishedFileId fileId )
		{
			var result = await Ugc.Query.All
									.WithFileId( fileId )
									.GetPageAsync( 1 );

			if ( !result.HasValue || result.Value.ResultCount != 1 )
				return null;

			var item = result.Value.Entries.First();

			result.Value.Dispose();

			return item;
		}

		/// <summary>
		/// Tell Steam the player has started using a Workshop item, so it accrues playtime. Those
		/// numbers are what the <c>RankedByPlaytime*</c> orderings and the playtime fields on
		/// <see cref="Ugc.Item"/> report; without this call they stay at zero forever and your
		/// Workshop looks unplayed.
		/// </summary>
		/// <param name="fileId">The item the player has begun using.</param>
		/// <returns>
		/// <see langword="true"/> if Steam accepted the call. <see langword="false"/> if it failed or
		/// returned a non-OK result, which this API does not distinguish.
		/// </returns>
		/// <remarks>
		/// <para>
		/// Pair every call with <see cref="StopPlaytimeTracking"/> or
		/// <see cref="StopPlaytimeTrackingForAllItems"/>. Valve does not document what happens to a
		/// session that is never stopped &#8212; for example if the game crashes &#8212; so treat
		/// stopping on shutdown as a requirement rather than a courtesy.
		/// </para>
		/// <para>
		/// Valve's native call accepts a batch of ids; this wrapper tracks exactly one per call.
		/// </para>
		/// </remarks>
		public static async Task<bool> StartPlaytimeTracking(PublishedFileId fileId)
		{
			var result = await Internal.StartPlaytimeTracking(new[] {fileId}, 1);
			return result?.Result == Result.OK;
		}

		/// <summary>
		/// Tell Steam the player has stopped using a Workshop item, closing the playtime session
		/// opened by <see cref="StartPlaytimeTracking"/>.
		/// </summary>
		/// <param name="fileId">The item the player has stopped using.</param>
		/// <returns>
		/// <see langword="true"/> if Steam accepted the call; <see langword="false"/> on failure or a
		/// non-OK result. Stopping an item that was never started is not distinguished from success.
		/// </returns>
		public static async Task<bool> StopPlaytimeTracking(PublishedFileId fileId)
		{
			var result = await Internal.StopPlaytimeTracking(new[] {fileId}, 1);
			return result?.Result == Result.OK;
		}

		/// <summary>
		/// Close every open playtime session at once. Call this when the player leaves a level or
		/// quits, rather than tracking which items you opened.
		/// </summary>
		/// <returns>
		/// <see langword="true"/> if Steam accepted the call; <see langword="false"/> on failure or a
		/// non-OK result.
		/// </returns>
		public static async Task<bool> StopPlaytimeTrackingForAllItems()
		{
			var result = await Internal.StopPlaytimeTrackingForAllItems();
			return result?.Result == Result.OK;
		}

		/// <summary>
		/// Read the ids of every Workshop item the local user is subscribed to for this app. This is
		/// the canonical "what content should I load" list, and it is answered from the Steam client's
		/// local state &#8212; no network round trip, no paging, no async.
		/// </summary>
		/// <param name="subscribedItems">
		/// The list to write ids into. Ids are <b>appended</b>; the list is not cleared first, so
		/// calling this twice with the same list produces duplicates. Passing <see langword="null"/>
		/// is safe and returns 0.
		/// </param>
		/// <returns>The number of ids appended.</returns>
		/// <remarks>
		/// <para>
		/// Being subscribed does not mean the content is on disk. Check <see cref="Ugc.Item.IsInstalled"/>
		/// and <see cref="Ugc.Item.NeedsUpdate"/> on each id, and download the ones that need it.
		/// </para>
		/// <para>
		/// This reflects subscriptions Steam knows about locally. Immediately after a fresh install,
		/// or before the client has synced, the list can be short or empty.
		/// </para>
		/// </remarks>
		public static uint GetSubscribedItems(List<PublishedFileId> subscribedItems)
		{
			if (subscribedItems == null) return 0;

			uint numItems = Internal.GetNumSubscribedItems();
			PublishedFileId[] items = new PublishedFileId[numItems];
			numItems = Internal.GetSubscribedItems( items, numItems );
			for ( int i = 0; i < numItems; i++ )
				subscribedItems.Add( items[i] );
			return numItems;
		}

		/// <summary>
		/// Pause all Workshop downloads. The usual reason is to stop background downloading from
		/// competing for disk and bandwidth during loading or gameplay.
		/// Downloads will be suspended until you resume them by calling <see cref="ResumeDownloads"/> or when the game ends.
		/// </summary>
		/// <remarks>
		/// This suspends downloads Steam started on its own as well as ones you requested. An awaited
		/// <see cref="DownloadAsync"/> will simply not progress until you resume &#8212; it does not
		/// fail, so a forgotten suspend looks like a hang.
		/// </remarks>
		public static void SuspendDownloads() => Internal.SuspendDownloads(true);

		/// <summary>
		/// Resume Workshop downloads previously paused with <see cref="SuspendDownloads"/>.
		/// Harmless to call when nothing is suspended.
		/// </summary>
		public static void ResumeDownloads() => Internal.SuspendDownloads(false);

		/// <summary>
		/// Show the app's latest Workshop EULA to the user in an overlay window, where they can accept it or not.
		/// Required if your app has a Workshop EULA configured and the user has not yet agreed to it.
		/// </summary>
		/// <returns>
		/// <see langword="true"/> if the overlay was shown. Valve does not document the failure
		/// conditions; the overlay being disabled or unavailable is the obvious one.
		/// </returns>
		/// <remarks>
		/// The return value only says the window opened &#8212; it tells you nothing about whether the
		/// user accepted. Poll <see cref="GetWorkshopEulaStatus"/> for that.
		/// </remarks>
		public static bool ShowWorkshopEula()
		{
			return Internal.ShowWorkshopEULA();
		}

		/// <summary>
		/// Check whether the user has accepted this app's Workshop EULA. Where an app has a EULA
		/// configured, publishing is refused until the user agrees, so check this before showing any
		/// upload UI.
		/// </summary>
		/// <returns>
		/// <see langword="true"/> or <see langword="false"/> for accepted / not accepted, or
		/// <see langword="null"/> if the call itself failed. Note that null is not the same as "not
		/// accepted" &#8212; do not treat it as a refusal.
		/// </returns>
		/// <remarks>
		/// Apps with no Workshop EULA configured on the partner site have nothing to accept. Valve
		/// does not document what this returns in that case.
		/// </remarks>
		public static async Task<bool?> GetWorkshopEulaStatus()
		{
			var status = await Internal.GetWorkshopEULAStatus();
			return status?.Accepted;
		}

	}
}
