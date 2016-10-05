using System;
using System.Text;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Facepunch.Steamworks.Test
{
    public partial class Server
    {
        [TestMethod]
        public void StatsGet()
        {
            using ( var server = new Facepunch.Steamworks.Server( 252490, 0, 30002, true, "VersionString" ) )
            {
                Assert.IsTrue( server.IsValid );
                server.LogOnAnonymous();

                ulong MySteamId = 76561197960279927;

                server.Stats.Refresh( MySteamId );

                // TODO - Callback on complete

                Thread.Sleep( 2000 );

                var deaths = server.Stats.GetInt( MySteamId, "deaths", -1 );

                Console.WriteLine( "Deaths: {0}", deaths );
                Assert.IsTrue( deaths > 0 );
            }
        }
        
    }
}
