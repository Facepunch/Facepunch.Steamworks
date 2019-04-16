using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
			SteamServer.DedicatedServer = true;
			SteamServer.DedicatedServer = false;
		}

        [TestMethod]
        public async Task PublicIp()
        {
            while ( true )
            {
                var ip = SteamServer.PublicIp;

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
			var stopwatch = System.Diagnostics.Stopwatch.StartNew();
			bool finished = false;
			AuthResponse response = AuthResponse.AuthTicketInvalidAlreadyUsed;

			//
			// Clientside calls this function, gets ticket
			//
			var clientTicket = SteamUser.GetAuthSessionTicket();

			//
			// The client sends this data to the server along with their steamid
			//
			var ticketData = clientTicket.Data;
			var clientSteamId = SteamUser.SteamId;

			//
			// Server listens to auth responses from Gabe
			//
			SteamServer.OnValidateAuthTicketResponse += ( steamid, ownerid, rsponse ) =>
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
			if ( !SteamServer.BeginAuthSession( ticketData, clientSteamId ) )
			{
				Assert.Fail( "BeginAuthSession returned false, called bullshit without even having to check with Gabe" );
			}

			//
			// Wait for that to go through steam
			//
			while ( !finished )
			{
				if ( stopwatch.Elapsed.TotalSeconds > 5 )
					throw new System.Exception( "Took too long waiting for AuthSessionResponse.OK" );

				await Task.Delay( 10 );
			}

			Assert.AreEqual( response, AuthResponse.OK );

			finished = false;
			stopwatch = System.Diagnostics.Stopwatch.StartNew();

			//
			// The client is leaving, and now wants to cancel the ticket
			//

			Assert.AreNotEqual( 0, clientTicket.Handle );
			clientTicket.Cancel();

			//
			// We should get another callback 
			//
			while ( !finished )
			{
				if ( stopwatch.Elapsed.TotalSeconds > 5 )
					throw new System.Exception( "Took too long waiting for AuthSessionResponse.AuthTicketCanceled" );

				await Task.Delay( 10 );
			}

			Assert.AreEqual( response, AuthResponse.AuthTicketCanceled );

		}
    }
}
