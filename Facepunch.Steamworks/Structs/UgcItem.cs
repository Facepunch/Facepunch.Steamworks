﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Steamworks.Data;

using QueryType = Steamworks.Ugc.Query;

namespace Steamworks.Ugc
{
	public struct Item
	{
		internal SteamUGCDetails_t details;
		internal PublishedFileId _id;

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

        public bool IsInstalled => (State & ItemState.Installed) == ItemState.Installed;
		public bool IsDownloading => (State & ItemState.Downloading) == ItemState.Downloading;
		public bool IsDownloadPending => (State & ItemState.DownloadPending) == ItemState.DownloadPending;
		public bool IsSubscribed => (State & ItemState.Subscribed) == ItemState.Subscribed;
		public bool NeedsUpdate => (State & ItemState.NeedsUpdate) == ItemState.NeedsUpdate;

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
		/// Start downloading this item.
		/// If this returns false the item isn#t getting downloaded.
		/// </summary>
		public bool Download( bool highPriority = false )
		{
			return SteamUGC.Internal.DownloadItem( Id, highPriority );
		}

		/// <summary>
		/// If we're downloading, how big the total download is 
		/// </summary>
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
		/// If we're downloading, how much we've downloaded
		/// </summary>
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
		/// If we're installed, how big is the install
		/// </summary>
		public long SizeBytes
		{
			get
			{
				if ( NeedsUpdate )
					return DownloadBytesDownloaded;

				ulong size = 0;
				uint ts = 0;

				if ( !SteamUGC.Internal.GetItemInstallInfo( Id, ref size, out var strVal, ref ts ) )
					return 0;

				return (long) size;
			}
		}

		/// <summary>
		/// If we're downloading our current progress as a delta betwen 0-1
		/// </summary>
		public float DownloadAmount
		{
			get
			{
				if ( !NeedsUpdate ) return 1;

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

		public static async Task<Item?> GetAsync( PublishedFileId id, int maxageseconds = 60 * 30 )
		{
			var result = await SteamUGC.Internal.RequestUGCDetails( id, (uint) maxageseconds );
			if ( !result.HasValue ) return null;

			return From( result.Value.Details );
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
		/// A case insensitive check for tag
		/// </summary>
		public bool HasTag( string find )
		{
			if ( Tags.Length == 0 ) return false;

			return Tags.Contains( find, StringComparer.OrdinalIgnoreCase );
		}

        /// <summary>
        /// Allows the user to subscribe to this item
        /// </summary>
        public async Task<bool> Subscribe ()
        {
            var result = await SteamUGC.Internal.SubscribeItem( _id );
            return result?.Result == Result.OK;
        }

        /// <summary>
        /// Allows the user to unsubscribe from this item
        /// </summary>
        public async Task<bool> Unsubscribe ()
        {
            var result = await SteamUGC.Internal.UnsubscribeItem( _id );
            return result?.Result == Result.OK;
        }

        /// <summary>
        /// Allows the user to rate a workshop item up or down.
        /// </summary>
        public async Task<bool> Vote( bool up )
		{
			var r = await SteamUGC.Internal.SetUserItemVote( Id, up );
			return r?.Result == Result.OK;
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

		public ulong NumSubscriptions { get; internal set; }
		public ulong NumFavorites { get; internal set; }
		public ulong NumFollowers { get; internal set; }
		public ulong NumUniqueSubscriptions { get; internal set; }
		public ulong NumUniqueFavorites { get; internal set; }
		public ulong NumUniqueFollowers { get; internal set; }
		public ulong NumUniqueWebsiteViews { get; internal set; }
		public ulong ReportScore { get; internal set; }
		public ulong NumSecondsPlayed { get; internal set; }
		public ulong NumPlaytimeSessions { get; internal set; }
		public ulong NumComments { get; internal set; }
		public ulong NumSecondsPlayedDuringTimePeriod { get; internal set; }
		public ulong NumPlaytimeSessionsDuringTimePeriod { get; internal set; }

        /// <summary>
        /// The URL to the preview image for this item
        /// </summary>
        public string PreviewImageUrl { get; internal set; }

		/// <summary>
		/// Edit this item
		/// </summary>
		public Ugc.Editor Edit()
		{
			return new Ugc.Editor( Id );
		}
	}

}