using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SteamNative;

namespace Steamworks
{
    [DeploymentItem( "steam_api64.dll" )]
    [DeploymentItem( "tier0_s64.dll" )]
    [DeploymentItem( "vstdlib_s64.dll" )]
    [DeploymentItem( "steamclient64.dll" )]
    [TestClass]
    public partial class GameServerTest
    {
        [TestMethod]
        public void Init()
        {
			GameServer.DedicatedServer = true;
			GameServer.DedicatedServer = false;
		}

        [TestMethod]
        public async Task PublicIp()
        {
            while ( true )
            {
                var ip = GameServer.PublicIp;

                if ( ip == null )
                {
					await Task.Delay( 10 );
                    continue;
                }

                Assert.IsNotNull( ip );
                Console.WriteLine( ip.ToString() );
                break;
            }
        }

        [TestMethod]
        public async Task BeginAuthSession()
        {
			bool finished = false;
			AuthSessionResponse response = AuthSessionResponse.AuthTicketInvalidAlreadyUsed;

			//
			// Clientside calls this function, gets ticket
			//
			var clientTicket = User.GetAuthSessionTicket();

			//
			// The client sends this data to the server along with their steamid
			//
			var ticketData = clientTicket.Data;
			var clientSteamId = User.SteamId;

			//
			// Server listens to auth responses from Gabe
			//
			GameServer.OnValidateAuthTicketResponse += ( steamid, ownerid, rsponse ) =>
			{
				finished = true;
				response = rsponse;

				Assert.AreEqual( steamid, clientSteamId );
				Assert.AreEqual( steamid, ownerid );

				Console.WriteLine( "steamid: {0}", steamid );
				Console.WriteLine( "ownerid: {0}", ownerid );
				Console.WriteLine( "status: {0}", response );
			};

			//
			// Server gets the ticket, starts authing
			//
			if ( !GameServer.BeginAuthSession( ticketData, clientSteamId ) )
			{
				Assert.Fail( "BeginAuthSession returned false, called bullshit without even having to check with Gabe" );
			}

			//
			// Wait for that to go through steam
			//
			while ( !finished )
				await Task.Delay( 10 );

			Assert.AreEqual( response, AuthSessionResponse.OK );

			finished = false;

			//
			// The client is leaving, and now wants to cancel the ticket
			//

			clientTicket.Cancel();

			//
			// We should get another callback 
			//
			while ( !finished )
				await Task.Delay( 10 );

			Assert.AreEqual( response, AuthSessionResponse.AuthTicketCanceled );

		}
    }
}
