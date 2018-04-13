using System;
using System.Text;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Facepunch.Steamworks.Test
{
    [TestClass]
    [DeploymentItem( "steam_api.dll" )]
    [DeploymentItem( "steam_api64.dll" )]
    public class App
    {
        [TestMethod]
        public void IsSubscribed()
        {
            using ( var client = new Facepunch.Steamworks.Client( 252490 ) )
            {
                Console.WriteLine("This test assumes you own Garry's Mod and not Charles III of Spain and the antiquity");

                Assert.IsTrue( client.App.IsSubscribed( 4000 ) );
                Assert.IsFalse( client.App.IsSubscribed( 590440 ));
            }
        }

        [TestMethod]
        public void IsInstalled()
        {
            using (var client = new Facepunch.Steamworks.Client(252490))
            {
                Console.WriteLine("This test assumes you have Garry's Mod installed but not Charles III of Spain and the antiquity");

                Assert.IsTrue(client.App.IsInstalled(4000));
                Assert.IsFalse(client.App.IsInstalled(590440));
            }
        }

        [TestMethod]
        public void PurchaseTime()
        {
            using (var client = new Facepunch.Steamworks.Client(252490))
            {
                Console.WriteLine("This test assumes you own Garry's Mod but not Charles III of Spain and the antiquity");

                var gmodBuyTime = client.App.PurchaseTime( 4000 );
                Assert.AreNotEqual( gmodBuyTime, DateTime.MinValue );

                Console.WriteLine($"You bought Garry's Mod {gmodBuyTime}");

                var otherBuyTime = client.App.PurchaseTime(590440);
                Assert.AreEqual(otherBuyTime, DateTime.MinValue);
            }
        }

        [TestMethod]
        public void AppName()
        {
            using ( var client = new Facepunch.Steamworks.Client( 252490 ) )
            {
                var name = client.App.GetName( 4000 );
                Console.WriteLine( name );
            }
        }

    }
}
