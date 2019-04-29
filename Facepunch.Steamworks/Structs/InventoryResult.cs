using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Steamworks.Data;

namespace Steamworks
{
	public struct InventoryResult : IDisposable
	{
		internal SteamInventoryResult_t _id;
		internal Result _result;

		internal InventoryResult( SteamInventoryResult_t id )
		{
			_id = id;
			_result = Result.Pending;
		}

		internal async Task<bool> WaitUntilReadyAsync()
		{
			while ( _result == Result.Pending )
			{
				_result = SteamInventory.Internal.GetResultStatus( _id );
				Task.Delay( 10 );
			}

			return _result == Result.OK || _result == Result.Expired;
		}

		public int ItemCount
		{
			get
			{
				uint cnt = 0;

				if ( !SteamInventory.Internal.GetResultItems( _id, null, ref cnt ) )
					return 0;

				return (int) cnt;
			}
		}

		public InventoryItem[] GetItems( bool includeProperties = false )
		{
			uint cnt = (uint) ItemCount;
			if ( cnt <= 0 ) return null;

			var pOutItemsArray = new SteamItemDetails_t[cnt];

			if ( !SteamInventory.Internal.GetResultItems( _id, pOutItemsArray, ref cnt ) )
				return null;

			var items = new InventoryItem[cnt];

			for( int i=0; i< cnt; i++ )
			{
				var item = InventoryItem.From( pOutItemsArray[i] );

				if ( includeProperties )
					item._properties = InventoryItem.GetProperties( _id, i );

				items[i] = item;
			}


			return items;			
		}

		public void Dispose()
		{
			SteamInventory.Internal.DestroyResult( _id );
		}
	}
}