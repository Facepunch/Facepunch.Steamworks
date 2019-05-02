using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Steamworks
{
    [TestClass]
    [DeploymentItem( "steam_api64.dll" )]
    public class NetworkingSocketsTest
	{

		[TestMethod]
        public async Task CreateRelayServer()
        {
			var socket = SteamNetworkingSockets.CreateRelaySocket();

			Console.WriteLine( $"{socket}" );

			// Give it a second for something to happen
			await Task.Delay( 5000 );

			Console.WriteLine( $"{socket}" );

			socket.Close();
		}

		[TestMethod]
		public async Task CreateNormalServer()
		{
			var socket = SteamNetworkingSockets.CreateNormalSocket( Data.NetworkAddress.AnyIp( 21893 ) );

			Console.WriteLine( $"{socket}" );

			// Give it a second for something to happen
			await Task.Delay( 5000 );

			Console.WriteLine( $"{socket}" );

			socket.Close();
		}

		[TestMethod]
		public async Task ConnectToRelayServer()
		{
			var socket = SteamNetworkingSockets.CreateRelaySocket( 7788 );
			Console.WriteLine( $"Created {socket}" );
			//await Task.Delay( 1000 );

			var connection = SteamNetworkingSockets.ConnectRelay( SteamClient.SteamId, 7788 );
			connection.ConnectionName = "Connected To Self";
			connection.UserData = 69;

			// Give it a second for something to happen
			await Task.Delay( 1000 );

			connection.Close();

			Console.WriteLine( $"{socket}" );

			socket.Close();
		}

	}

}
