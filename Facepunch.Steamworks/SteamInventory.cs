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
	/// Undocumented Parental Settings
	/// </summary>
	public static class SteamInventory
	{
		static ISteamInventory _internal;
		internal static ISteamInventory Internal
		{
			get
			{
				if ( _internal == null )
					_internal = new ISteamInventory();

				return _internal;
			}
		}

		internal static void InstallEvents()
		{
			new Event<SteamInventoryFullUpdate_t>( x => OnInventoryUpdated?.Invoke() );
			new Event<SteamInventoryDefinitionUpdate_t>( x => OnDefinitionsUpdated?.Invoke() );

			Internal.LoadItemDefinitions();
		}

		public static event Action OnInventoryUpdated;
		public static event Action OnDefinitionsUpdated;

		public static async Task<SteamItemDef[]> GetItemsWithPricesAsync()
		{
			var priceRequest = await Internal.RequestPrices();
			if ( !priceRequest.HasValue || priceRequest.Value.Result != Result.OK )
				return null;

			Console.WriteLine( $"Currency: {priceRequest?.Currency}" );

			var num = Internal.GetNumItemsWithPrices();

			Console.WriteLine( $"num: {num}" );
			if ( num <= 0 )
				return null;

			var defs = new SteamItemDef_t[num];
			var currentPrices = new ulong[num];
			var baseprices = new ulong[num];

			var gotPrices = Internal.GetItemsWithPrices( defs, currentPrices, baseprices, num );
			if ( !gotPrices )
				return null;

			return defs.Select( x => new SteamItemDef { _id = x } ).ToArray();
		}

	}
}