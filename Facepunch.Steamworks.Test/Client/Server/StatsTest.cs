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
            using ( var server = new Facepunch.Steamworks.Server( 252490, new ServerInit( "rust", "Rust" ) ) )
            {
                Assert.IsTrue( server.IsValid );
                server.LogOnAnonymous();

                ulong MySteamId = 76561197960279927;

                bool GotStats = false;

                server.Stats.Refresh( MySteamId, (steamid, success) =>
                {
                    GotStats = true;
                    Assert.IsTrue( success );

                    var deathsInCallback = server.Stats.GetInt( MySteamId, "deaths", -1 );
                    Console.WriteLine( "deathsInCallback: {0}", deathsInCallback );
                    Assert.IsTrue( deathsInCallback > 0 );
                } );


                server.UpdateWhile( () => !GotStats );

                var deaths = server.Stats.GetInt( MySteamId, "deaths", -1 );
                Console.WriteLine( "deathsInCallback: {0}", deaths );
                Assert.IsTrue( deaths > 0 );
            }
        }
        
    }
}
