using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Facepunch.Steamworks.Test
{
    [DeploymentItem( "steam_api.dll" )]
    [DeploymentItem( "steam_api64.dll" )]
    [DeploymentItem( "tier0_s.dll" )]
    [DeploymentItem( "vstdlib_s.dll" )]
    [DeploymentItem( "steamclient.dll" )]
    [DeploymentItem( "tier0_s64.dll" )]
    [DeploymentItem( "vstdlib_s64.dll" )]
    [DeploymentItem( "steamclient64.dll" )]
    [TestClass]
    public partial class Server
    {
        [TestMethod]
        public void Init()
        {
            var serverInit = new ServerInit( "rust", "Rust" );
            serverInit.GamePort = 28015;
            serverInit.Secure = true;
            serverInit.QueryPort = 28016;

            using ( var server = new Facepunch.Steamworks.Server( 252490, serverInit ) )
            {
                server.ServerName = "My Test Server";
                server.LogOnAnonymous();

                Assert.IsTrue( server.IsValid );
            }
        }

        [TestMethod]
        public void PublicIp()
        {
            using ( var server = new Facepunch.Steamworks.Server( 252490, new ServerInit( "rust", "Rust" ) ) )
            {
                server.LogOnAnonymous();

                Assert.IsTrue( server.IsValid );

                while ( true )
                {
                    var ip = server.PublicIp;

                    if ( ip == null )
                    {
                        System.Threading.Thread.Sleep( 100 );
                        server.Update();
                        continue;
                    }

                    Assert.IsNotNull( ip );
                    Console.WriteLine( ip.ToString() );
                    break;
                }

            }
        }

        [TestMethod]
        public void AuthCallback()
        {
            using ( var client = new Facepunch.Steamworks.Client( 252490 ) )
            {
                Assert.IsTrue( client.IsValid );
                var ticket = client.Auth.GetAuthSessionTicket();
                var ticketBinary = ticket.Data;

                using ( var server = new Facepunch.Steamworks.Server( 252490, new ServerInit( "rust", "Rust" ) ) )
                {
                    server.LogOnAnonymous();

                    Assert.IsTrue( server.IsValid );

                    var auth = server.Auth;

                    var Authed = false;

                    server.Auth.OnAuthChange = ( steamid, ownerid, status ) =>
                    {
                        Authed = status == ServerAuth.Status.OK;

                        Assert.AreEqual( steamid, client.SteamId );
                        Assert.AreEqual( steamid, ownerid );

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
