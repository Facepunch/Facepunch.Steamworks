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
    public class NetworkingSocketsTest
	{

		[TestMethod]
        public async Task CreateRelayServer()
        {
			var si = SteamNetworkingSockets.CreateRelaySocket<TestSocketInterface>();

			Console.WriteLine( $"Created Socket: {si}" );

			// Give it a second for something to happen
			await Task.Delay( 1000 );

			si.Close();
		}

		[TestMethod]
		public async Task CreateNormalServer()
		{
			var si = SteamNetworkingSockets.CreateNormalSocket<TestSocketInterface>( Data.NetworkAddress.AnyIp( 21893 ) );

			Console.WriteLine( $"Created Socket: {si}" );

			// Give it a second for something to happen
			await Task.Delay( 1000 );

			si.Close();
		}

		[TestMethod]
		public async Task RelayEndtoEnd()
		{
			var socket = SteamNetworkingSockets.CreateRelaySocket<TestSocketInterface>( 7788 );
			var server = socket.RunAsync();

			await Task.Delay( 1000 );

			var connection = SteamNetworkingSockets.ConnectRelay<TestConnectionInterface>( SteamClient.SteamId, 7788 );
			var client = connection.RunAsync();

			await Task.WhenAll( server, client );
		}

		private class TestConnectionInterface : ConnectionInterface
		{
			public override void OnConnectionChanged( ConnectionInfo data )
			{
				Console.WriteLine( $"[Connection][{Connection}] [{data.State}]" );

				base.OnConnectionChanged( data );
			}

			public override void OnConnecting( ConnectionInfo data )
			{
				Console.WriteLine( $" - OnConnecting" );
				base.OnConnecting( data );
			}

			/// <summary>
			/// Client is connected. They move from connecting to Connections
			/// </summary>
			public override void OnConnected( ConnectionInfo data )
			{
				Console.WriteLine( $" - OnConnected" );
				base.OnConnected( data );
			}

			/// <summary>
			/// The connection has been closed remotely or disconnected locally. Check data.State for details.
			/// </summary>
			public override void OnDisconnected( ConnectionInfo data )
			{
				Console.WriteLine( $" - OnDisconnected" );
				base.OnDisconnected( data );
			}

			internal async Task RunAsync()
			{
				Console.WriteLine( "[Connection] RunAsync" );

				while ( !Connected )
					await Task.Delay( 10 );

				Console.WriteLine( "[Connection] Hey We're Connected!" );

				var sw = System.Diagnostics.Stopwatch.StartNew();


				while ( Connected )
				{
					Receive();
					await Task.Delay( 100 );

					if ( sw.Elapsed.TotalSeconds > 5 )
					{
						Console.WriteLine( "CLIENT ERROR!!!!! TIMED OUT" );
						break;
					}
				}

				Console.WriteLine( Connection.DetailedStatus() );
			}

			public override unsafe void OnMessage( IntPtr data, int size, long messageNum, SteamNetworkingMicroseconds recvTime, int channel )
			{
				// We're only sending strings, so it's fine to read this like this
				var str = UTF8Encoding.UTF8.GetString( (byte*) data, size );

				Console.WriteLine( $"[Connection][{messageNum}][{recvTime}][{channel}] \"{str}\"" );

				if ( str.Contains( "Hello" ) )
				{
					Connection.SendMessage( "Hello, How are you!?" );

					Connection.SendMessage( "How do you like 20 messages in a row?" );

					for ( int i=0; i<20; i++ )
					{
						Connection.SendMessage( $"BLAMMO!" );
					}
				}

				if ( str.Contains( "how about yourself" ) )
				{
					Connection.SendMessage( "I'm great, but I have to go now, bye." );
				}

				if ( str.Contains( "hater" ) )
				{
					Close();
				}

			}
		}
	

		private class TestSocketInterface : SocketInterface
		{
			public bool HasFinished = false;

			public override void OnConnectionChanged( NetConnection connection, ConnectionInfo data )
			{
				Console.WriteLine( $"[Socket{Socket}][{connection}] [{data.State}]" );

				base.OnConnectionChanged( connection, data );
			}

			public override void OnConnecting( NetConnection connection, ConnectionInfo data )
			{
				Console.WriteLine( $" - OnConnecting" );
				base.OnConnecting( connection, data );
			}

			/// <summary>
			/// Client is connected. They move from connecting to Connections
			/// </summary>
			public override void OnConnected( NetConnection connection, ConnectionInfo data )
			{
				Console.WriteLine( $" - OnConnected" );
				base.OnConnected( connection, data );
			}

			/// <summary>
			/// The connection has been closed remotely or disconnected locally. Check data.State for details.
			/// </summary>
			public override void OnDisconnected( NetConnection connection, ConnectionInfo data )
			{
				Console.WriteLine( $" - OnDisconnected" );
				base.OnDisconnected( connection, data );
			}

			internal async Task RunAsync()
			{
				while ( Connected.Count == 0 )
					await Task.Delay( 10 );

				await Task.Delay( 1000 );

				var singleClient = Connected.First();

				singleClient.SendMessage( "Hey?" );
				await Task.Delay( 1000 );
				singleClient.SendMessage( "Anyone?" );
				await Task.Delay( 100 );
				singleClient.SendMessage( "What's this?" );
				await Task.Delay( 10 );
				singleClient.SendMessage( "Greetings!!??" );
				await Task.Delay( 300 );
				singleClient.SendMessage( "Hello Client!?" );

				var sw = System.Diagnostics.Stopwatch.StartNew();

				while ( Connected.Contains( singleClient ) )
				{
					Receive();
					await Task.Delay( 100 );

					if ( sw.Elapsed.TotalSeconds > 5 )
					{
						Assert.Fail( "Took too long" );
						break;
					}
				}

				await Task.Delay( 1000 );

				Close();
			}

			public override unsafe void OnMessage( NetConnection connection, NetworkIdentity identity, IntPtr data, int size, long messageNum, SteamNetworkingMicroseconds recvTime, int channel )
			{
				// We're only sending strings, so it's fine to read this like this
				var str = UTF8Encoding.UTF8.GetString( (byte*)data, size );

				Console.WriteLine( $"[SOCKET][{connection}[{identity}][{messageNum}][{recvTime}][{channel}] \"{str}\"" );

				if ( str.Contains( "Hello, How are you" ) )
				{
					connection.SendMessage( "I'm great thanks, how about yourself?" );
				}

				if ( str.Contains( "bye" ) )
				{
					connection.SendMessage( "See you later, hater." );
					connection.Close( true, 10, "Said Bye" );
				}
			}
		}
	}

}
