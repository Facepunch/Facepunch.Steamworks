using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Facepunch.Steamworks.Test
{
    [TestClass]
    [DeploymentItem( "FacepunchSteamworksApi.dll" )]
    [DeploymentItem( "steam_appid.txt" )]
    public partial class Client
    {
        [TestMethod]
        public void Init()
        {
            using ( var client = new Facepunch.Steamworks.Client( 252490 ) )
            {
                Assert.IsTrue( client.Valid );
            }
        }

        [TestMethod]
        public void Name()
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
        public void SteamId()
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
        public void AuthSessionTicket()
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
        public void VoiceOptimalSampleRate()
        {
            using ( var client = new Facepunch.Steamworks.Client( 252490 ) )
            {
                var rate = client.Voice.OptimalSampleRate;
                Assert.AreNotEqual( rate, 0 );
            }
        }

        [TestMethod]
        public void Update()
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
        public void GetVoice()
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

        [TestMethod]
        public void GetServers()
        {
            using ( var client = new Facepunch.Steamworks.Client( 252490 ) )
            {
                var query = client.ServerList.Test();

                for ( int i = 0; i < 100 ; i++ )
                {
                    client.Update();
                    System.Threading.Thread.Sleep( 5 );

                    if ( query.Finished )
                        break;
                }

                Console.WriteLine( "Responded: " + query.Responded.Count.ToString() );
                Console.WriteLine( "Unresponsive: " + query.Unresponsive.Count.ToString() );
            }
        }

        [TestMethod]
        public void InventoryDefinitions()
        {
            using ( var client = new Facepunch.Steamworks.Client( 252490 ) )
            {
                Assert.IsNotNull( client.Inventory.Definitions );
                Assert.AreNotEqual( 0, client.Inventory.Definitions.Length );
            }
        }

        [TestMethod]
        public void InventoryItemList()
        {
            using ( var client = new Facepunch.Steamworks.Client( 252490 ) )
            {
                bool CallbackCalled = false;

                // OnUpdate hsould be called when we receive a list of our items
                client.Inventory.OnUpdate = () => { CallbackCalled = true; };

                // tell steam to download the items
                client.Inventory.Refresh();

                // Wait for the items
                while ( client.Inventory.Items == null )
                {
                    client.Update();
                    System.Threading.Thread.Sleep( 10 );
                }

                // make sure callback was called
                Assert.IsTrue( CallbackCalled );

                // Make sure items are valid
                foreach ( var item in client.Inventory.Items )
                {
                    Assert.IsNotNull( item );
                    Assert.IsNotNull( item.Definition );

                    Console.WriteLine( item.Definition.Name + " - " + item.Id );
                }
            }
        }
    }
}
