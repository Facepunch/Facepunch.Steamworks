using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Facepunch.Steamworks.Test
{
    [DeploymentItem( "FacepunchSteamworksApi.dll" )]
    [DeploymentItem( "steam_appid.txt" )]
    [DeploymentItem( "tier0_s.dll" )]
    [DeploymentItem( "vstdlib_s.dll" )]
    [DeploymentItem( "steamclient.dll" )]
    [TestClass]
    public class Server
    {
        [TestMethod]
        public void Init()
        {
            using ( var server = new Facepunch.Steamworks.Server( 252490, 30000, 30001, 30002, 30003, false, "VersionString" ) )
            {
                Assert.IsTrue( server.Valid );
            }
        }

        [TestMethod]
        public void AuthCallback()
        {
            using ( var client = new Facepunch.Steamworks.Client( 252490 ) )
            {
                Assert.IsTrue( client.Valid );

                var ticket = client.Auth.GetAuthSessionTicket();
                var ticketBinary = ticket.Data;

                using ( var server = new Facepunch.Steamworks.Server( 252490, 30000, 30001, 30002, 30003, false, "VersionString" ) )
                {
                    Assert.IsTrue( server.Valid );

                    for ( int i = 0; i < 16; i++ )
                    {
                        System.Threading.Thread.Sleep( 10 );
                        server.Update();
                        client.Update();
                    }

                    if ( !server.Auth.StartSession( ticketBinary, client.SteamId ) )
                    {
                        Assert.Fail( "Start Session returned false" );
                    }

                    for( int i = 0; i<16; i++ )
                    {
                        System.Threading.Thread.Sleep( 10 );
                        server.Update();
                        client.Update();
                    }

                    //
                    // Client cancels ticket
                    //
                    ticket.Cancel();

                }
            }
        }
    }
}
