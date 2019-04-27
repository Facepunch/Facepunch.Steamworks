using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Steamworks.Data;

namespace Steamworks
{
    [TestClass]
    [DeploymentItem( "steam_api64.dll" )]
    public class InventoryTest
	{
		[TestMethod]
        public async Task GetItemsWithPricesAsync()
        {
			var items = await SteamInventory.GetItemsWithPricesAsync();

			foreach ( var item in items )
			{
				Console.WriteLine( $"[{item.LocalPrice}] {item.Name}" );
			}
		}

		[TestMethod]
		public async Task IutemDefs()
		{
			var items = await SteamInventory.GetItemsWithPricesAsync();

			foreach ( var item in items )
			{
				Console.WriteLine( $"{item.Id}" );
				foreach ( var prop in item.Properties )
				{
					Console.WriteLine( $"	{prop.Key}: {prop.Value}" );
				}
				Console.WriteLine( $"" );
			}
		}
	}

}
