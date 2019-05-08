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
	public static class SteamUGC
	{
		static ISteamUGC _internal;
		internal static ISteamUGC Internal
		{
			get
			{
				if ( _internal == null )
				{
					_internal = new ISteamUGC();
					_internal.Init();
				}

				return _internal;
			}
		}

		internal static void Shutdown()
		{
			_internal = null;
		}

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
		public static async Task<Ugc.Item?> FetchItem( PublishedFileId fileId )
		{
			var result = await new Ugc.FileQuery( fileId )
									.GetPageAsync( 1 );

			if ( !result.HasValue || result.Value.ResultCount != 1 )
				return null;

			var item = result.Value.Entries.First();

			result.Value.Dispose();

			return item;
		}
	}
}