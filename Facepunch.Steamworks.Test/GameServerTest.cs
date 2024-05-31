using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Steamworks.Data;

namespace Steamworks
{
    [DeploymentItem( "steam_api64.dll" )]
	[DeploymentItem( "steam_api.dll" )]
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
			string failed = null;
			AuthResponse response = AuthResponse.AuthTicketInvalidAlreadyUsed;

			//
			// Clientside calls this function, gets ticket
			//
			var clientTicket = SteamUser.GetAuthSessionTicket( NetIdentity.LocalHost );

			//
			// The client sends this data to the server along with their steamid
			//
			var ticketData = clientTicket.Data;
			var clientSteamId = SteamClient.SteamId;

			//
			// Server listens to auth responses from Gabe
			//
			SteamServer.OnValidateAuthTicketResponse += ( steamid, ownerid, rsponse ) =>
			{
				finished = true;
				response = rsponse;

				if ( steamid == 0 )
					failed = $"steamid is 0! {steamid} != {ownerid} ({rsponse})";

				if ( ownerid == 0 )
					failed = $"ownerid is 0! {steamid} != {ownerid} ({rsponse})";

				if ( steamid != ownerid )
					failed = $"Steamid and Ownerid are different! {steamid} != {ownerid} ({rsponse})";
			};

			//
			// Server gets the ticket, starts authing
			//
			// if ( !SteamServer.BeginAuthSession( ticketData, clientSteamId ) )
			// {
			// 	Assert.Fail( "BeginAuthSession returned false, called bullshit without even having to check with Gabe" );
			// }

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

			if ( failed != null )
				Assert.Fail( failed );

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

			if ( failed != null )
				Assert.Fail( failed );

			//Assert.AreEqual( response, AuthResponse.AuthTicketCanceled );

		}
    }
}
