using System.Collections.Generic;
using Steamworks.Data;

namespace Steamworks.Ugc
{
	public struct ResultPage : System.IDisposable
	{
		internal UGCQueryHandle_t Handle;

		public int ResultCount;
		public int TotalCount;

		public bool CachedData;

		public IEnumerable<Item> Entries
		{
			get
			{
				var details = default( SteamUGCDetails_t );
				for ( uint i=0; i< ResultCount; i++ )
				{
					if ( SteamUGC.Internal.GetQueryUGCResult( Handle, i, ref details ) )
					{
						yield return Item.From( details );
					}
				}
			}
		}

		public void Dispose()
		{
			if ( Handle > 0 )
			{
				SteamUGC.Internal.ReleaseQueryUGCRequest( Handle );
				Handle = 0;
			}
		}
	}
}