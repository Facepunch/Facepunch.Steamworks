using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Steamworks.Data;

namespace Steamworks
{
	[TestClass]
    [DeploymentItem( "steam_api64.dll" )]
    [DeploymentItem( "steam_api.dll" )]
    public partial class NetworkingSocketsTest
	{
		void DebugOutput( NetDebugOutput type, string text )
		{
			Console.WriteLine( $"[NET:{type}]\t\t{text}" );
		}

		[TestMethod]
        public async Task CreateRelayServer()
        {
			SteamNetworkingUtils.DebugLevel = NetDebugOutput.Everything;
			SteamNetworkingUtils.OnDebugOutput += DebugOutput;

			var si = SteamNetworkingSockets.CreateRelaySocket<TestSocketInterface>();

			Console.WriteLine( $"Created Socket: {si}" );

			// Give it a second for something to happen
			await Task.Delay( 1000 );

			si.Close();
		}

		[TestMethod]
		public async Task CreateNormalServer()
		{
			SteamNetworkingUtils.DebugLevel = NetDebugOutput.Everything;
			SteamNetworkingUtils.OnDebugOutput += DebugOutput;

			var si = SteamNetworkingSockets.CreateNormalSocket<TestSocketInterface>( Data.NetAddress.AnyIp( 21893 ) );

			Console.WriteLine( $"Created Socket: {si}" );

			// Give it a second for something to happen
			await Task.Delay( 1000 );

			si.Close();
		}

		[TestMethod]
		public async Task CreateRelayServerFakeIP()
		{
			SteamNetworkingUtils.DebugLevel = NetDebugOutput.Everything;
			SteamNetworkingUtils.OnDebugOutput += DebugOutput;

			var si = SteamNetworkingSockets.CreateRelaySocketFakeIP<TestSocketInterface>();

			Console.WriteLine( $"Created Socket: {si}" );

			// Give it a second for something to happen
			await Task.Delay( 1000 );

			si.Close();
		}

		[TestMethod]
		public async Task RelayEndtoEnd()
		{
			SteamNetworkingUtils.InitRelayNetworkAccess();
			SteamNetworkingUtils.DebugLevel = NetDebugOutput.Warning;
			SteamNetworkingUtils.OnDebugOutput += DebugOutput;

			// For some reason giving steam a couple of seconds here 
			// seems to prevent it returning null connections from ConnectNormal
			await Task.Delay( 2000 );

			Console.WriteLine( $"----- Creating Socket Relay Socket.." );
			var socket = SteamNetworkingSockets.CreateRelaySocket<TestSocketInterface>( 6 );
			var server = socket.RunAsync();

			await Task.Delay( 1000 );

			Console.WriteLine( $"----- Connecting To Socket via SteamId ({SteamClient.SteamId})" );
			var connection = SteamNetworkingSockets.ConnectRelay<TestConnectionInterface>( SteamClient.SteamId, 6 );
			var client = connection.RunAsync();

			await Task.WhenAll( server, client );
		}

		[TestMethod]
		public async Task NormalEndtoEnd()
		{
			SteamNetworkingUtils.DebugLevel = NetDebugOutput.Everything;
			SteamNetworkingUtils.OnDebugOutput += DebugOutput;

			// For some reason giving steam a couple of seconds here 
			// seems to prevent it returning null connections from ConnectNormal
			await Task.Delay( 2000 );

			//
			// Start the server
			//
			Console.WriteLine( "CreateNormalSocket" );
			var socket = SteamNetworkingSockets.CreateNormalSocket<TestSocketInterface>( NetAddress.AnyIp( 12445 ) );
			var server = socket.RunAsync();

			//
			// Start the client
			//
			Console.WriteLine( "ConnectNormal" );
			var connection = SteamNetworkingSockets.ConnectNormal<TestConnectionInterface>( NetAddress.From( "127.0.0.1", 12445 ) );
			var client = connection.RunAsync();

			await Task.WhenAll( server, client );
		}

		[TestMethod]
		public async Task RelayEndtoEndFakeIP()
		{
			SteamNetworkingUtils.InitRelayNetworkAccess();
			SteamNetworkingUtils.DebugLevel = NetDebugOutput.Warning;
			SteamNetworkingUtils.OnDebugOutput += DebugOutput;

			// For some reason giving steam a couple of seconds here 
			// seems to prevent it returning null connections from ConnectNormal
			await Task.Delay( 2000 );

			Console.WriteLine( $"----- Creating Socket Relay Socket.." );
			var socket = SteamNetworkingSockets.CreateRelaySocketFakeIP<TestSocketInterface>();
			var server = socket.RunAsync();

			await Task.Delay( 1000 );

			Console.WriteLine( $"----- Retrieving Fake IP.." );
			SteamNetworkingSockets.GetFakeIP( 0, out NetAddress address );

			Console.WriteLine( $"----- Connecting To Socket via Fake IP ({address})" );
			var connection = SteamNetworkingSockets.ConnectNormal<TestConnectionInterface>( address );
			var client = connection.RunAsync();

			await Task.WhenAll( server, client );
		}

		[TestMethod]
		public void NetAddressTest()
		{
			{
				var n = NetAddress.From( "127.0.0.1", 12445 );
				Assert.AreEqual( n.ToString(), "127.0.0.1:12445" );
			}

			{
				var n = NetAddress.AnyIp( 5543 );
				Assert.AreEqual( n.ToString(), "[::]:5543" );
			}
		}
	}

}
