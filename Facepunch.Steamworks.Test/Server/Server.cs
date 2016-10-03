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
            using ( var server = new Facepunch.Steamworks.Server( 252490, 30001, 30002, 30003, false, "VersionString" ) )
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

                using ( var server = new Facepunch.Steamworks.Server( 252490, 30001, 30002, 30003, true, "VersionString" ) )
                {
                    

                    server.LogOnAnonymous();



                    Assert.IsTrue( server.Valid );

                    var auth = server.Auth;

                    var Authed = false;

                    server.Auth.OnAuthChange = ( steamid, ownerid, status ) =>
                    {
                        Authed = status == ServerAuth.Status.OK;

                        Assert.AreEqual( steamid, client.SteamId );

                        Console.WriteLine( "steamid: {0}", steamid );
                        Console.WriteLine( "ownerid: {0}", ownerid );
                        Console.WriteLine( "status: {0}", status );
                    };

                    for ( int i = 0; i < 16; i++ )
                    {
                        System.Threading.Thread.Sleep( 10 );
                        GC.Collect();
                        server.Update();
                        GC.Collect();
                        client.Update();
                        GC.Collect();
                    }

                    GC.Collect();
                    if ( !server.Auth.StartSession( ticketBinary, client.SteamId ) )
                    {
                        Assert.Fail( "Start Session returned false" );
                    }
                    GC.Collect();

                    //
                    // Server should receive a ServerAuth.Status.OK 
                    // message via the OnAuthChange callback
                    //

                    for ( int i = 0; i< 100; i++ )
                    {
                        GC.Collect();
                        System.Threading.Thread.Sleep( 100 );
                        GC.Collect();
                        server.Update();
                        client.Update();

                        if ( Authed )
                            break;
                    }

                    Assert.IsTrue( Authed );

                    //
                    // Client cancels ticket
                    //
                    ticket.Cancel();

                    //
                    // Server should receive a ticket cancelled message
                    //

                    for ( int i = 0; i < 100; i++ )
                    {
                        System.Threading.Thread.Sleep( 100 );
                        server.Update();
                        client.Update();

                        if ( !Authed )
                            break;
                    }

                    Assert.IsTrue( !Authed );

                }
            }
        }
    }
}
