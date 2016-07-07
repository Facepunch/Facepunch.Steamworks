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
                var ticket = client.GetAuthSessionTicket();

                Assert.IsTrue( ticket != null );
                Assert.IsTrue( ticket.Handle != 0 );
                Assert.IsTrue( ticket.Data.Length > 0 );

                client.CancelAuthTicket( ticket );

                Assert.IsTrue( ticket.Handle == 0 );
            }
        }
    }
}
