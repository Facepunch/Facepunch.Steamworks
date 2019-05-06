using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Steamworks.Data;

using QueryType = Steamworks.Ugc.Query;

namespace Steamworks.Ugc
{
	public struct Item
	{
		internal enum ItemState : int
		{
			None = 0,
			Subscribed = 1,
			LegacyItem = 2,
			Installed = 4,
			NeedsUpdate = 8,
			Downloading = 16,
			DownloadPending = 32,
		}

		public PublishedFileId Id { get; internal set; }

		public string Title { get; internal set; }
		public string Description { get; internal set; }
		public string[] Tags { get; internal set; }

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
				var sb = Helpers.TakeStringBuilder();
				if ( !SteamUGC.Internal.GetItemInstallInfo( Id, ref size, sb, (uint)sb.Capacity, ref ts ) )
					return null;

				return sb.ToString();
			}
		}

		public bool Download( bool highPriority = false )
		{
			return SteamUGC.Internal.DownloadItem( Id, highPriority );
		}

		private ItemState State => (ItemState) SteamUGC.Internal.GetItemState( Id );

		public static async Task<Item?> Get( PublishedFileId id, int maxageseconds = 60 * 30 )
		{
			var result = await SteamUGC.Internal.RequestUGCDetails( id, (uint) maxageseconds );
			if ( !result.HasValue ) return null;

			return From( result.Value.Details );
		}

		internal static Item From( SteamUGCDetails_t details )
		{
			var d = new Item
			{
				Id = details.PublishedFileId,
			//	FileType = details.FileType,

				Title = details.Title,
				Description = details.Description,
				Tags = details.Tags.Split( new[] { ',' }, StringSplitOptions.RemoveEmptyEntries )

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

	}
}