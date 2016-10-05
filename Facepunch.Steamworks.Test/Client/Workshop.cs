using System;
using System.Text;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Facepunch.Steamworks.Test
{
    [TestClass]
    [DeploymentItem( Config.LibraryName + ".dll" )]
    [DeploymentItem( "steam_appid.txt" )]
    public class WorkshopTest
    {
        [TestMethod]
        public void Query()
        {
            using ( var client = new Facepunch.Steamworks.Client( 252490 ) )
            {
                Assert.IsTrue( client.IsValid );

                var Query = client.Workshop.CreateQuery();

                Query.Run();

                // Block, wait for result
                // (don't do this in realtime)
                Query.Block();

                Assert.IsFalse( Query.IsRunning );

                // results

                Console.WriteLine( "Searching" );

                Query.SearchText = "shit";
                Query.Run();

                // Block, wait for result
                // (don't do this in realtime)
                Query.Block();

                for ( int i=0; i<100; i++ )
                {
                    client.Update();
                    Thread.Sleep( 10 );
                }
            }
        }

    }
}
