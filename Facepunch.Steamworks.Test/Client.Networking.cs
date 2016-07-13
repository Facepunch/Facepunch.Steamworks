using System;
using System.Text;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Facepunch.Steamworks.Test
{
    public partial class Client
    {
        [TestMethod]
        public void PeerToPeerSend()
        {
            using ( var client = new Facepunch.Steamworks.Client( 252490 ) )
            {
                var TestString = "This string will be transformed to bytes, sent over the Steam P2P network, then converted back to a string.";
                var OutputReceived = false;
                var data = Encoding.UTF8.GetBytes( TestString );

                client.Networking.OnP2PData = ( steamid, ms, channel ) =>
                {
                    var str = Encoding.UTF8.GetString( ms.GetBuffer() );
                    Assert.AreEqual( str, TestString );
                    Assert.AreEqual( steamid, client.SteamId );
                    OutputReceived = true;
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
    }
}
