using System;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Facepunch.Steamworks.Test
{
    [DeploymentItem( "steam_api.dll" )]
    [DeploymentItem( "steam_api64.dll" )]
    [DeploymentItem( "steam_appid.txt" )]
    [TestClass]
    public class Inventory
    {
        [TestMethod]
        public void InventoryDefinitions()
        {
            using ( var client = new Facepunch.Steamworks.Client( 252490 ) )
            {
                Assert.IsNotNull( client.Inventory.Definitions );
                Assert.AreNotEqual( 0, client.Inventory.Definitions.Length );

                foreach ( var i in client.Inventory.Definitions.Where( x => x.PriceRaw != "" ) )
                {
                    Console.WriteLine( "{0}: {1} ({2})", i.Id, i.Name, i.Type );
                    Console.WriteLine( "  itemshortname: {0}", i.GetStringProperty( "itemshortname" ) );
                    Console.WriteLine( "  workshopdownload: {0}", i.GetStringProperty( "workshopdownload" ) );
                    Console.WriteLine( "           IconUrl: {0}", i.IconUrl );
                    Console.WriteLine( "      IconLargeUrl: {0}", i.IconLargeUrl );
                    Console.WriteLine( "          PriceRaw: {0}", i.PriceRaw );
                    Console.WriteLine( "      PriceDollars: {0}", i.PriceDollars );
                }
            }
        }

        [TestMethod]
        public void InventoryDefinitionExchange()
        {
            using ( var client = new Facepunch.Steamworks.Client( 252490 ) )
            {
                Assert.IsNotNull( client.Inventory.Definitions );
                Assert.AreNotEqual( 0, client.Inventory.Definitions.Length );

                foreach ( var i in client.Inventory.Definitions )
                {
                    if ( i.Recipes == null ) continue;

                    Console.WriteLine( "Ways To Create " + i.Name );

                    foreach ( var r in i.Recipes )
                    {
                        Console.WriteLine( "  " + string.Join( ", ", r.Ingredients.Select( x => x.Count + " x " + x.Definition.Name ) ) );
                    }
                }
            }
        }

        [TestMethod]
        public void InventoryDefinitionIngredients()
        {
            using ( var client = new Facepunch.Steamworks.Client( 252490 ) )
            {
                Assert.IsNotNull( client.Inventory.Definitions );
                Assert.AreNotEqual( 0, client.Inventory.Definitions.Length );

                foreach ( var i in client.Inventory.Definitions )
                {
                    if ( i.IngredientFor == null ) continue;

                    Console.WriteLine( i.Name + " Can Be Used to Make" );

                    foreach ( var r in i.IngredientFor )
                    {
                        Console.WriteLine( "  " + r.Result.Name );
                    }
                }
            }
        }

        [TestMethod]
        public void InventoryItemList()
        {
            using ( var client = new Facepunch.Steamworks.Client( 252490 ) )
            {
                bool CallbackCalled = false;

                // OnUpdate hsould be called when we receive a list of our items
                client.Inventory.OnUpdate = () => { CallbackCalled = true; };

                // tell steam to download the items
                client.Inventory.Refresh();

                // Wait for the items
                var timeout = Stopwatch.StartNew();
                while ( client.Inventory.Items == null )
                {
                    client.Update();
                    System.Threading.Thread.Sleep( 1000 );

                    if ( timeout.Elapsed.TotalSeconds > 10 )
                        break;
                }

                // make sure callback was called
                Assert.IsTrue( CallbackCalled );

                // Make sure items are valid
                foreach ( var item in client.Inventory.Items )
                {
                    Assert.IsNotNull( item );
                    Assert.IsNotNull( item.Definition );

                    Console.WriteLine( item.Definition.Name + " - " + item.Id );
                }
            }
        }

        [TestMethod]
        public void Deserialize()
        {
            using ( var client = new Facepunch.Steamworks.Client( 252490 ) )
            {
                Assert.IsTrue( client.IsValid );

                client.Inventory.Refresh();

                //
                // Block until we have the items
                //
                while ( client.Inventory.SerializedItems == null )
                {
                    client.Update();
                }

                Assert.IsNotNull( client.Inventory.SerializedItems );
                Assert.IsTrue( client.Inventory.SerializedItems.Length > 4 );

                using ( var server = new Facepunch.Steamworks.Server( 252490, new ServerInit( "rust", "Rust" ) ) )
                {
                    server.LogOnAnonymous();
                    Assert.IsTrue( server.IsValid );

                    var result = server.Inventory.Deserialize( client.Inventory.SerializedItems );

                    server.UpdateWhile( () => result.IsPending );

                    Assert.IsFalse( result.IsPending );
                    Assert.IsNotNull( result.Items );

                    foreach ( var item in result.Items )
                    {
                        Console.WriteLine( "Item: {0} ({1})", item.Id, item.DefinitionId );
                        Console.WriteLine( "Item: {0} ({1})", item.Id, item.DefinitionId );
                    }
                }
            }
        }
    }
}
