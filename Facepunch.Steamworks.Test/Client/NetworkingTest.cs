using System;
using System.Diagnostics;
using System.Text;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Facepunch.Steamworks.Test
{
    [TestClass]
    [DeploymentItem( "steam_api.dll" )]
    [DeploymentItem( "steam_api64.dll" )]
    public partial class Networking
    {
        [TestMethod]
        public void PeerToPeerSend()
        {
            using ( var client = new Facepunch.Steamworks.Client( 252490 ) )
            {
                var TestString = "This string will be transformed to bytes, sent over the Steam P2P network, then converted back to a string.";
                var OutputReceived = false;
                var data = Encoding.UTF8.GetBytes( TestString );

                //
                // Enable listening on this channel
                //
                client.Networking.SetListenChannel( 0, true );

                client.Networking.OnP2PData = ( steamid, bytes, length, channel ) =>
                {
                    var str = Encoding.UTF8.GetString( bytes, 0, length );
                    Assert.AreEqual( str, TestString );
                    Assert.AreEqual( steamid, client.SteamId );
                    OutputReceived = true;

                    Console.WriteLine( "Got: " + str );
                };

                client.Networking.OnIncomingConnection = ( steamid ) =>
                {
                    Console.WriteLine( "Incoming P2P Connection: " + steamid );
                    return true;
                };

                client.Networking.OnConnectionFailed = ( steamid, error ) =>
                {
                    Console.WriteLine( "Connection Error: " + steamid + " - " + error );
                };

                client.Networking.SendP2PPacket( client.SteamId, data, data.Length );

                while( true )
                {
                    Thread.Sleep( 10 );
                    client.Update();

                    if ( OutputReceived )
                        break;
                }
            }
        }

        [TestMethod]
        public void PeerToPeerFailure()
        {
            using ( var client = new Facepunch.Steamworks.Client( 252490 ) )
            {
                var TestString = "This string will be transformed to bytes, sent over the Steam P2P network, then converted back to a string.";
                var TimeoutReceived = false;
                var data = Encoding.UTF8.GetBytes( TestString );

                client.Networking.OnIncomingConnection = ( steamid ) =>
                {
                    Console.WriteLine( "Incoming P2P Connection: " + steamid );

                    return true;
                };

                client.Networking.OnConnectionFailed = ( steamid, error ) =>
                {
                    Console.WriteLine( "Connection Error: " + steamid + " - " + error );
                    TimeoutReceived = true;
                };

                ulong rand = (ulong) new Random().Next( 1024 * 16 );

                // Send to an invalid, not listening steamid
                if  ( !client.Networking.SendP2PPacket( client.SteamId + rand, data, data.Length ) )
                {
                    Console.WriteLine( "Couldn't send packet" );
                    return;
                }

                var sw = Stopwatch.StartNew();

                while ( true )
                {
                    Thread.Sleep( 10 );
                    client.Update();

                    //
                    // Timout is usually around 15 seconds
                    //
                    if ( TimeoutReceived )
                        break;

                    if ( sw.Elapsed.TotalSeconds > 30 )
                    {
                        Assert.Fail( "Didn't time out" );
                    }
                }
            }
        }

    }
}
