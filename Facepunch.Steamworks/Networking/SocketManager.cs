using System;
using System.Buffers;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Steamworks.Data;
using static Steamworks.SpanActions;

namespace Steamworks
{
	/// <summary>
	/// Used as a base to create your networking server. This creates a socket
	/// and listens/communicates with multiple queries.
	/// 
	/// You can override all the virtual functions to turn it into what you
	/// want it to do.
	/// </summary>
	public partial class SocketManager : IDisposable
	{
		// public ISocketManager Interface { get; set; }

		public HashSet<Connection> Connecting = new();
		public HashSet<Connection> Connected = new();
		
		public Socket Socket { get; internal set; }

		internal HSteamNetPollGroup pollGroup;


		public Action<Connection, ConnectionInfo> onConnecting;
		public Action<Connection, ConnectionInfo> onConnected;
		public Action<Connection, ConnectionInfo> onDisconnected;

		public MessageAction onMessage;

    	public delegate void MessageAction(ReadOnlySpan<byte> data, Connection connection, NetIdentity identity, long messageNum, long recvTime, int channel);


		public override string ToString() => Socket.ToString();


		internal void Initialize()
		{
			pollGroup = SteamNetworkingSockets.Internal.CreatePollGroup();
		}

		public void Dispose()
		{
			if ( SteamNetworkingSockets.Internal.IsValid )
			{
				SteamNetworkingSockets.Internal.DestroyPollGroup( pollGroup );
				Socket.Close();
			}

			pollGroup = 0;
			Socket = 0;
		}

		public virtual void OnConnectionChanged( Connection connection, ConnectionInfo info )
		{
			//
			// Some notes:
			// - Update state before the callbacks, in case an exception is thrown
			// - ConnectionState.None happens when a connection is destroyed, even if it was already disconnected (ClosedByPeer / ProblemDetectedLocally)
			//
			switch ( info.State )
			{
				case ConnectionState.Connecting:
					if ( !Connecting.Contains( connection ) && !Connected.Contains( connection ) )
					{
						Connecting.Add( connection );

						onConnecting( connection, info );
						
						//TODO:: test what happens when accepting a closed connection, 
						//       might need a check to ensure a closed connection isn't accepted here 
						connection.Accept(); 

					}
					break;
				case ConnectionState.Connected:
					if ( Connecting.Contains( connection ) && !Connected.Contains( connection ) )
					{
						Connecting.Remove( connection );
						Connected.Add( connection );

						onConnected( connection, info );
					}
					break;
				case ConnectionState.ClosedByPeer:
				case ConnectionState.ProblemDetectedLocally:
				case ConnectionState.None:
					if ( Connecting.Contains( connection ) || Connected.Contains( connection ) )
					{
						Connecting.Remove( connection );
						Connected.Remove( connection );

						onDisconnected( connection, info );

						connection.Close();
					}
					break;
			}
		}

		public int Receive( int bufferSize = 32, bool receiveToEnd = true )
		{
			int processed = 0;
			IntPtr messageBuffer = Marshal.AllocHGlobal( IntPtr.Size * bufferSize );

			try
			{
				processed = SteamNetworkingSockets.Internal.ReceiveMessagesOnPollGroup( pollGroup, messageBuffer, bufferSize );

				for ( int i = 0; i < processed; i++ )
				{
					ReceiveMessage( Marshal.ReadIntPtr( messageBuffer, i * IntPtr.Size ) );
				}
			}
			finally
			{
				Marshal.FreeHGlobal( messageBuffer );
			}
			

			//
			// Overwhelmed our buffer, keep going
			//
			if ( receiveToEnd && processed == bufferSize )
				processed += Receive( bufferSize );

			return processed;
		}

		internal unsafe void ReceiveMessage( IntPtr msgPtr )
		{
			var msg = Marshal.PtrToStructure<NetMsg>( msgPtr );
			try
			{
				onMessage(new Span<byte>(msg.DataPtr.ToPointer(), msg.DataSize), msg.Connection, msg.Identity, msg.MessageNumber, msg.RecvTime, msg.Channel);
			}
			finally
			{
				//
				// Releases the message
				//
				NetMsg.InternalRelease( (NetMsg*) msgPtr );
			}
		}
	}
}
