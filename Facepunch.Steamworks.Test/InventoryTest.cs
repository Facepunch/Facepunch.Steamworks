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
		public async Task LoadItemDefinitionsAsync()
		{
			var result = await SteamInventory.WaitForDefinitions( 5 );
			Assert.IsTrue( result );

			result = await SteamInventory.WaitForDefinitions( 5 );
			Assert.IsTrue( result );
		}

		[TestMethod]
        public async Task GetDefinitions()
        {
			await SteamInventory.WaitForDefinitions();

			Assert.IsNotNull( SteamInventory.Definitions );

			foreach ( var def in SteamInventory.Definitions )
			{
				Console.WriteLine( $"[{def.Id:0000000000}] {def.Name} [{def.Type}]" );
			}
		}

		[TestMethod]
		public async Task GetDefinitionsWithPrices()
		{
			var defs = await SteamInventory.GetDefinitionsWithPricesAsync();

			foreach ( var def in defs )
			{
				Console.WriteLine( $"[{def.Id:0000000000}] {def.Name} [{def.LocalPriceFormatted}]" );
			}
		}

		[TestMethod]
		public async Task GetAllItems()
		{
			await SteamInventory.WaitForDefinitions();

			var result = await SteamInventory.GetAllItemsAsync();

			Assert.IsTrue( result.HasValue );

			using ( result )
			{
				var items = result?.GetItems( true );

				foreach ( var item in items )
				{
					Console.WriteLine( $"{item.Id} / {item.DefId} / {item.Quantity} / {item.Def?.Name} " );

					foreach ( var prop in item.Properties )
					{
						Console.WriteLine( $"	{prop.Key} : {prop.Value}" );
					}
				}
			}
		}

		[TestMethod]
		public async Task Items()
		{
			SteamInventory.GetAllItems();
			await SteamInventory.WaitForDefinitions();
			
			while ( SteamInventory.Items == null )
			{
				await Task.Delay( 10 );
			}

			Assert.IsNotNull( SteamInventory.Items );

			foreach ( var item in SteamInventory.Items )
			{
				Console.WriteLine( $"{item.Id} / {item.DefId} / {item.Quantity} / {item.Def.Name}" );
			}
		}

		[TestMethod]
		public async Task GetExchanges()
		{
			var result = await SteamInventory.WaitForDefinitions( 5 );
			Assert.IsTrue( result );

			foreach ( var def in SteamInventory.Definitions )
			{
				var exchangelist = def.GetRecipes();
				if ( exchangelist == null ) continue;

				foreach ( var exchange in exchangelist )
				{
					Assert.AreEqual( exchange.Result, def );

					Console.WriteLine( $"{def.Name}:" );

					foreach ( var item in exchange.Ingredients )
					{
						Console.WriteLine( $"	{item.Count} x {item.Definition.Name}" );
					}

					Console.WriteLine( $"" );
				}
			}
		}
	}

}
