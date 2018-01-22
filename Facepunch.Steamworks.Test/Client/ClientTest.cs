using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Facepunch.Steamworks.Test
{
    [TestClass]
    [DeploymentItem( "steam_api.dll" )]
    [DeploymentItem( "steam_api64.dll" )]
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
        public void Update()
        {
            var sw = new Stopwatch();
            using ( var client = new Facepunch.Steamworks.Client( 252490 ) )
            {
                Assert.IsTrue(client.IsValid);

                for( int i=0; i<1024; i++ )
                {
                    sw.Restart();
                    client.Update();
                    Console.WriteLine( $"{sw.Elapsed.TotalMilliseconds}ms" );

                }
            }
        }

        [TestMethod]
        public void Subscribed()
        {
            var sw = new Stopwatch();
            using (var client = new Facepunch.Steamworks.Client(252490))
            {
                Assert.IsTrue(client.IsValid);
                Assert.IsTrue(client.IsSubscribed);
            }
        }

        [TestMethod]
        public void Owner()
        {
            var sw = new Stopwatch();
            using (var client = new Facepunch.Steamworks.Client(252490))
            {
                Assert.IsTrue(client.IsValid);
                Assert.AreEqual(client.OwnerSteamId, client.SteamId);
            }
        }

        [TestMethod]
        public void InstallFolder()
        {
            var sw = new Stopwatch();
            using (var client = new Facepunch.Steamworks.Client(252490))
            {
                Assert.IsTrue(client.IsValid);
                Assert.IsTrue(client.InstallFolder.Exists);

                Console.Write($"Install Folder: {client.InstallFolder}");
            }
        }

        [TestMethod]
        public void CurrentLanguage()
        {
            var sw = new Stopwatch();
            using ( var client = new Facepunch.Steamworks.Client( 252490 ) )
            {
                Assert.IsTrue( client.IsValid );
                Assert.IsTrue( client.CurrentLanguage != null );
                Assert.IsTrue( client.CurrentLanguage.Length > 0 );

                Console.Write( $"CurrentLanguage: {client.CurrentLanguage}" );
            }
        }

        [TestMethod]
        public void AvailableLanguages()
        {
            var sw = new Stopwatch();
            using ( var client = new Facepunch.Steamworks.Client( 252490 ) )
            {
                Assert.IsTrue( client.IsValid );
                Assert.IsTrue( client.AvailableLanguages != null );
                Assert.IsTrue( client.AvailableLanguages.Length > 0 );

                foreach ( var lang in client.AvailableLanguages )
                {
                    Console.Write( $"AvailableLanguages: {lang}" );
                }
                
            }
        }

        [TestMethod]
        public void Cybercafe()
        {
            var sw = new Stopwatch();
            using (var client = new Facepunch.Steamworks.Client(252490))
            {
                Assert.IsTrue(client.IsValid);
                Assert.IsFalse(client.IsCybercafe);
            }
        }

        [TestMethod]
        public void LowViolence()
        {
            var sw = new Stopwatch();
            using (var client = new Facepunch.Steamworks.Client(252490))
            {
                Assert.IsTrue(client.IsValid);
                Assert.IsFalse(client.IsLowViolence);
            }
        }
    }
}
