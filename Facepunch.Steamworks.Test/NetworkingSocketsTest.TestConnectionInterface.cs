using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Steamworks.Data;

namespace Steamworks
{
	public partial class NetworkingSocketsTest
	{
		private class TestConnectionInterface : ConnectionManager
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

				var sw = System.Diagnostics.Stopwatch.StartNew();

				Console.WriteLine( "[Connection] Connecting" );
				while ( Connecting )
				{
					await Task.Delay( 10 );

					if ( sw.Elapsed.TotalSeconds > 10 )
						break;
				}

				if ( !Connected )
				{
					Console.WriteLine( "[Connection] Couldn't connect!" );
					Console.WriteLine( Connection.DetailedStatus() );
					return;
				}

				Console.WriteLine( "[Connection] Hey We're Connected!" );


				sw = System.Diagnostics.Stopwatch.StartNew();
				while ( Connected )
				{
					Receive();
					await Task.Delay( 100 );

					if ( sw.Elapsed.TotalSeconds > 30 )
					{
						Assert.Fail( "Client Took Too Long" );
						break;
					}
				}
			}

			public override unsafe void OnMessage( IntPtr data, int size, long messageNum, long recvTime, int channel )
			{
				// We're only sending strings, so it's fine to read this like this
				var str = UTF8Encoding.UTF8.GetString( (byte*) data, size );

				Console.WriteLine( $"[Connection][{messageNum}][{recvTime}][{channel}] \"{str}\"" );

				if ( str.Contains( "Hello" ) )
				{
					Console.WriteLine( $"[Connection][{messageNum}][{recvTime}][{channel}] Sending: Hello, How are you!?" );
					Connection.SendMessage( "Hello, How are you!?" );

					Console.WriteLine( $"[Connection][{messageNum}][{recvTime}][{channel}] Sending: How do you like 20 messages in a row?" );
					Connection.SendMessage( "How do you like 20 messages in a row?" );

					var connections = new[] { Connection };
                    var results = new Result[1];
					for ( int i=0; i<20; i++ )
					{
						Console.WriteLine( $"[Connection][{messageNum}][{recvTime}][{channel}] Sending: BLAMMO {i}!" );
						SendMessages( connections, connections.Length, $"BLAMMO {i}!", results: results );
						Assert.AreEqual( Result.OK, results[0] );
					}

					Connection.Flush();
				}

				if ( str.Contains( "status" ))
				{
					Console.WriteLine( Connection.DetailedStatus() );
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
	}

}