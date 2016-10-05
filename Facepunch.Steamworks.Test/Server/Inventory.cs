using System;
using System.Text;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Facepunch.Steamworks.Test
{
    public partial class Server
    {
        [TestMethod]
        public void InventoryDeserialize()
        {
            using ( var client = new Facepunch.Steamworks.Client( 252490 ) )
            {
                Assert.IsTrue( client.IsValid );

                Assert.IsNull( client.Inventory.SerializedItems );

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

                using ( var server = new Facepunch.Steamworks.Server( 252490, 0, 30002, true, "VersionString" ) )
                {
                    server.LogOnAnonymous();
                    Assert.IsTrue( server.IsValid );

                    var result = server.Inventory.Deserialize( client.Inventory.SerializedItems );

                    Assert.IsTrue( result.Block() );
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
