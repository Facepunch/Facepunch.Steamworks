using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Steamworks.Data;

namespace Steamworks
{
	public partial class NetworkingSocketsTest
	{
		private class TestSocketInterface : SocketInterface
		{
			public bool HasFinished = false;

			public override void OnConnectionChanged( Connection connection, ConnectionInfo data )
			{
				Console.WriteLine( $"[Socket{Socket}][{connection}] [{data.State}]" );

				base.OnConnectionChanged( connection, data );
			}

			public override void OnConnecting( Connection connection, ConnectionInfo data )
			{
				Console.WriteLine( $" - OnConnecting" );
				base.OnConnecting( connection, data );
			}

			/// <summary>
			/// Client is connected. They move from connecting to Connections
			/// </summary>
			public override void OnConnected( Connection connection, ConnectionInfo data )
			{
				Console.WriteLine( $" - OnConnected" );
				base.OnConnected( connection, data );
			}

			/// <summary>
			/// The connection has been closed remotely or disconnected locally. Check data.State for details.
			/// </summary>
			public override void OnDisconnected( Connection connection, ConnectionInfo data )
			{
				Console.WriteLine( $" - OnDisconnected" );
				base.OnDisconnected( connection, data );
			}

			internal async Task RunAsync()
			{
				var sw = System.Diagnostics.Stopwatch.StartNew();

				while ( Connected.Count == 0 )
				{
					await Task.Delay( 10 );

					if ( sw.Elapsed.TotalSeconds > 5 )
					{
						Assert.Fail( "Client Took Too Long To Connect"  );
						break;
					}
				}

				await Task.Delay( 1000 );

				var singleClient = Connected.First();

				singleClient.SendMessage( "Hey?" );
				await Task.Delay( 100 );
				singleClient.SendMessage( "Anyone?" );
				await Task.Delay( 100 );
				singleClient.SendMessage( "What's this?" );
				await Task.Delay( 100 );
				singleClient.SendMessage( "What's your status?" );
				await Task.Delay( 10 );
				singleClient.SendMessage( "Greetings!!??" );
				await Task.Delay( 100 );
				singleClient.SendMessage( "Hello Client!?" );

				sw = System.Diagnostics.Stopwatch.StartNew();

				Console.WriteLine( $"Socket: Listening" );

				while ( Connected.Contains( singleClient ) )
				{
					Receive();
					await Task.Delay( 100 );

					if ( sw.Elapsed.TotalSeconds > 5 )
					{
						Console.WriteLine( "Socket: This all took too long - throwing an exception" );
						Assert.Fail( "Socket Took Too Long" );
						break;
					}
				}

				Console.WriteLine( $"Socket: Closing connection because {Connected.Count()} Connected" );

				await Task.Delay( 1000 );

				Close();
			}

			public override unsafe void OnMessage( Connection connection, NetIdentity identity, IntPtr data, int size, long messageNum, long recvTime, int channel )
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
					connection.Flush();
					connection.Close( true, 10, "Said Bye" );
				}
			}
		}
	}

}