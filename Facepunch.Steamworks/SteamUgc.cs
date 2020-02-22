using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Steamworks.Data;

namespace Steamworks
{
	/// <summary>
	/// Functions for accessing and manipulating Steam user information.
	/// This is also where the APIs for Steam Voice are exposed.
	/// </summary>
	public class SteamUGC : SteamClass
	{
		internal static ISteamUGC Internal;
		internal override SteamInterface Interface => Internal;

		internal override void InitializeInterface( bool server )
		{
			Internal = new ISteamUGC( server );
			InstallEvents();
		}

		internal static void InstallEvents()
		{
			Dispatch.Install<DownloadItemResult_t>( x => OnDownloadItemResult?.Invoke( x.Result ) );
		}

		/// <summary>
		/// Posted after Download call
		/// </summary>
		public static event Action<Result> OnDownloadItemResult;

		public static async Task<bool> DeleteFileAsync( PublishedFileId fileId )
		{
			var r = await Internal.DeleteItem( fileId );
			return r?.Result == Result.OK;
		}

		public static bool Download( PublishedFileId fileId, bool highPriority = false )
		{
			return Internal.DownloadItem( fileId, highPriority );
		}

		/// <summary>
		/// Utility function to fetch a single item. Internally this uses Ugc.FileQuery -
		/// which you can use to query multiple items if you need to.
		/// </summary>
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

		public static async Task<bool> StartPlaytimeTracking(PublishedFileId fileId)
		{
			var result = await Internal.StartPlaytimeTracking(new[] {fileId}, 1);
			return result.Value.Result == Result.OK;
		}
		
		public static async Task<bool> StopPlaytimeTracking(PublishedFileId fileId)
		{
			var result = await Internal.StopPlaytimeTracking(new[] {fileId}, 1);
			return result.Value.Result == Result.OK;
		}
		
		public static async Task<bool> StopPlaytimeTrackingForAllItems()
		{
			var result = await Internal.StopPlaytimeTrackingForAllItems();
			return result.Value.Result == Result.OK;
		}
	}
}
