using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Facepunch.Steamworks.Test
{
    [TestClass]
    [DeploymentItem( "FacepunchSteamworksApi.dll" )]
    [DeploymentItem( "steam_appid.txt" )]
    public class Client
    {
        [TestMethod]
        public void ClientInit()
        {
            using ( var client = new Facepunch.Steamworks.Client( 252490 ) )
            {
                Assert.IsTrue( client.Valid );
            }
        }

        [TestMethod]
        public void ClientName()
        {
            using ( var client = new Facepunch.Steamworks.Client( 252490 ) )
            {
                var username = client.Username;
                Console.WriteLine( username );
                Assert.IsTrue( client.Valid );
                Assert.IsNotNull( username );
            }
        }

        [TestMethod]
        public void ClientSteamId()
        {
            using ( var client = new Facepunch.Steamworks.Client( 252490 ) )
            {
                var steamid = client.SteamId;
                Console.WriteLine( steamid );
                Assert.IsTrue( client.Valid );
                Assert.AreNotEqual( 0, steamid );
            }
        }

        [TestMethod]
        public void ClientAuthSessionTicket()
        {
            using ( var client = new Facepunch.Steamworks.Client( 252490 ) )
            {
                var ticket = client.Auth.GetAuthSessionTicket();

                Assert.IsTrue( ticket != null );
                Assert.IsTrue( ticket.Handle != 0 );
                Assert.IsTrue( ticket.Data.Length > 0 );

                client.Auth.CancelAuthTicket( ticket );

                Assert.IsTrue( ticket.Handle == 0 );
            }
        }

        [TestMethod]
        public void ClientVoiceOptimalSampleRate()
        {
            using ( var client = new Facepunch.Steamworks.Client( 252490 ) )
            {
                var rate = client.Voice.OptimalSampleRate;
                Assert.AreNotEqual( rate, 0 );
            }
        }

        [TestMethod]
        public void ClientUpdate()
        {
            using ( var client = new Facepunch.Steamworks.Client( 252490 ) )
            {
                for( int i=0; i<32; i++ )
                {
                    client.Update();
                    System.Threading.Thread.Sleep( 10 );
                }
            }
        }

        [TestMethod]
        public void ClientGetVoice()
        {
            using ( var client = new Facepunch.Steamworks.Client( 252490 ) )
            {
                int dataRead = 0;

                client.Voice.OnCompressedData = ( data ) =>
                {
                    dataRead += data.Length;
                };

                client.Voice.OnUncompressedData = ( data ) =>
                {
                    dataRead += data.Length;
                };

                client.Voice.WantsRecording = true;

                for ( int i = 0; i < 32; i++ )
                {
                    client.Update();
                    System.Threading.Thread.Sleep( 10 );
                }

                // Should really be > 0 if the mic was getting audio
                Console.Write( dataRead );
            }
        }
    }
}
