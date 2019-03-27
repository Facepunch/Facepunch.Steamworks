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
        public void AuthenticateAClient()
        {
            using ( var client = new Facepunch.Steamworks.Client( 252490 ) )
            using ( var server = new Facepunch.Steamworks.Server( 252490, new ServerInit( "rust", "Rust" ) ) )
            {
                Assert.IsTrue( client.IsValid );
                Assert.IsTrue( server.IsValid );

                server.LogOnAnonymous();

                for ( int i = 0; i < 100; i++ )
                {
                    System.Threading.Thread.Sleep( 16 );
                    server.Update();
                    client.Update();

                    if ( server.SteamId != null )
                        break;
                }

                Assert.IsNotNull( server.SteamId );

                var ticket = client.Auth.GetAuthSessionTicket();
                var status = server.Auth.StartSession( ticket.Data, client.SteamId );

                Assert.IsTrue( status == Auth.StartAuthResult.OK );

                bool isAuthed = false;

                server.Auth.OnAuthChange += ( steamId, ownerId, authStatus ) => {
                    isAuthed = authStatus == Auth.AuthStatus.OK;
                };

                for ( int i = 0; i < 100; i++ )
                {
                    System.Threading.Thread.Sleep( 16 );
                    server.Update();
                    client.Update();

                    if ( isAuthed )
                        break;
                }

                Assert.IsTrue( isAuthed );

                ticket.Cancel();

                for ( int i = 0; i < 100; i++ )
                {
                    System.Threading.Thread.Sleep( 16 );
                    server.Update();
                    client.Update();

                    if ( !isAuthed )
                        break;
                }

                Assert.IsFalse( isAuthed );
            }
        }

        [TestMethod]
        public void AuthenticateAServer()
        {
            using ( var client = new Facepunch.Steamworks.Client( 252490 ) )
            using ( var server = new Facepunch.Steamworks.Server( 4000, new ServerInit( "rust", "Rust" ) ) )
            {
                Assert.IsTrue( client.IsValid );
                Assert.IsTrue( server.IsValid );

                server.LogOnAnonymous();

                for ( int i = 0; i < 100; i++ )
                {
                    System.Threading.Thread.Sleep( 16 );
                    server.Update();
                    client.Update();

                    if ( server.SteamId != null )
                        break;
                }

                Assert.IsNotNull( server.SteamId );

                var ticket = server.Auth.GetAuthSessionTicket();
                var status = client.Auth.StartSession( ticket.Data, server.SteamId.Value );

                Assert.IsTrue( status == Auth.StartAuthResult.OK );

                bool isAuthed = false;

                client.Auth.OnAuthChange += ( steamId, ownerId, authStatus ) => {
                    isAuthed = authStatus == Auth.AuthStatus.OK;
                };

                for ( int i = 0; i < 100; i++ )
                {
                    System.Threading.Thread.Sleep( 16 );
                    server.Update();
                    client.Update();

                    if ( isAuthed )
                        break;
                }

                Assert.IsTrue( isAuthed );

                ticket.Cancel();

                for ( int i = 0; i < 100; i++ )
                {
                    System.Threading.Thread.Sleep( 16 );
                    server.Update();
                    client.Update();

                    if ( !isAuthed )
                        break;
                }

                Assert.IsFalse( isAuthed );
            }
        }
    }
}
