using System;
using System.Diagnostics;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Facepunch.Steamworks.Test
{
    [TestClass]
    [DeploymentItem( "steam_api.dll" )]
    [DeploymentItem( "steam_api64.dll" )]
    [DeploymentItem( "steam_appid.txt" )]
    public partial class Client
    {
        [TestMethod]
        public void Init()
        {
            using ( var client = new Facepunch.Steamworks.Client( 252490 ) )
            {
                Assert.IsTrue( client.IsValid );
            }
        }

        [TestMethod]
        public void Init_10()
        {
            for ( int i = 0; i < 10; i++ )
            {
                using ( var client = new Facepunch.Steamworks.Client( 252490 ) )
                {
                    Assert.IsTrue( client.IsValid );
                }

                GC.Collect();
            }
        }

        [TestMethod]
        public void Name()
        {
            using ( var client = new Facepunch.Steamworks.Client( 252490 ) )
            {
                Assert.IsTrue( client.IsValid );

                var username = client.Username;
                Console.WriteLine( username );
                Assert.IsNotNull( username );
            }
        }

        [TestMethod]
        public void SteamId()
        {
            using ( var client = new Facepunch.Steamworks.Client( 252490 ) )
            {
                Assert.IsTrue( client.IsValid );

                var steamid = client.SteamId;
                Console.WriteLine( steamid );
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

                ticket.Cancel();

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
                for( int i=0; i<1024; i++ )
                {
                    client.Update();
                }
            }
        }

        static MemoryStream decompressStream = new MemoryStream();

        [TestMethod]
        public void GetVoice()
        {
            using ( var client = new Facepunch.Steamworks.Client( 252490 ) )
            {
                int unCompressed = 0;
                int compressed = 0;

                client.Voice.OnCompressedData = ( ptr, length ) =>
                {
                    compressed += length;

                    if ( !client.Voice.Decompress( ptr, 0, length, decompressStream ) )
                    {
                        Assert.Fail( "Decompress returned false" );
                    }
                };

                client.Voice.OnUncompressedData = ( ptr, length ) =>
                {
                    unCompressed += length;
                };

                client.Voice.WantsRecording = true;

                var sw = Stopwatch.StartNew();

                while ( sw.Elapsed.TotalSeconds < 3 )
                {
                    client.Update();
                    System.Threading.Thread.Sleep( 10 );
                }

                Assert.AreNotEqual( unCompressed, 0 );
                Assert.AreNotEqual( compressed, 0 );

                // Should really be > 0 if the mic was getting audio
                Console.WriteLine( "unCompressed: {0}", unCompressed );
                Console.WriteLine( "compressed: {0}", compressed );
            }
        }

        [TestMethod]
        public void GetVoice_Compressed_Only()
        {
            using ( var client = new Facepunch.Steamworks.Client( 252490 ) )
            {
                int compressed = 0;

                client.Voice.OnCompressedData = ( ptr, length ) =>
                {
                    compressed += length;
                };

                client.Voice.WantsRecording = true;

                var sw = Stopwatch.StartNew();

                while ( sw.Elapsed.TotalSeconds < 3 )
                {
                    client.Update();
                    System.Threading.Thread.Sleep( 10 );
                }

                Assert.AreNotEqual( compressed, 0 );
                Console.WriteLine( "compressed: {0}", compressed );
            }
        }

        [TestMethod]
        public void GetVoice_UnCompressed_Only()
        {
            using ( var client = new Facepunch.Steamworks.Client( 252490 ) )
            {
                int unCompressed = 0;

                client.Voice.OnUncompressedData = ( ptr, length ) =>
                {
                    unCompressed += length;
                };

                client.Voice.WantsRecording = true;

                var sw = Stopwatch.StartNew();

                while ( sw.Elapsed.TotalSeconds < 3 )
                {
                    client.Update();
                    System.Threading.Thread.Sleep( 10 );
                }

                Assert.AreNotEqual( unCompressed, 0 );

                // Should really be > 0 if the mic was getting audio
                Console.WriteLine( "unCompressed: {0}", unCompressed );

            }
        }

        [TestMethod]
        public void InventoryDefinitions()
        {
            using ( var client = new Facepunch.Steamworks.Client( 252490 ) )
            {
                Assert.IsNotNull( client.Inventory.Definitions );
                Assert.AreNotEqual( 0, client.Inventory.Definitions.Length );

                foreach ( var i in client.Inventory.Definitions )
                {
                    Console.WriteLine( "{0}: {1}", i.Id, i.Name );
                    Console.WriteLine( "  itemshortname: {0}", i.GetStringProperty( "itemshortname" ) );
                    Console.WriteLine( "  workshopdownload: {0}", i.GetStringProperty( "workshopdownload" ) );
                }
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
                var timeout = Stopwatch.StartNew();
                while ( client.Inventory.Items == null )
                {
                    client.Update();
                    System.Threading.Thread.Sleep( 1000 );

                    if ( timeout.Elapsed.TotalSeconds > 5 )
                        break;
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
