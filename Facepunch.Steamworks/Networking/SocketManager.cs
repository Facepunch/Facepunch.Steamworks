using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Steamworks.Data;

namespace Steamworks
{
	/// <summary>
	/// Used as a base to create your networking server. This creates a socket
	/// and listens/communicates with multiple queries.
	/// 
	/// You can override all the virtual functions to turn it into what you
	/// want it to do.
	/// </summary>
	public partial class SocketManager
	{
		public ISocketManager Interface { get; set; }

		public HashSet<Connection> Connecting = new HashSet<Connection>();
		public HashSet<Connection> Connected = new HashSet<Connection>();

		public Socket Socket { get; internal set; }

		public override string ToString() => Socket.ToString();

		internal HSteamNetPollGroup pollGroup;

		internal void Initialize()
		{
			pollGroup = SteamNetworkingSockets.Internal.CreatePollGroup();
		}

		public bool Close()
		{
			if ( SteamNetworkingSockets.Internal.IsValid )
			{
				SteamNetworkingSockets.Internal.DestroyPollGroup( pollGroup );
				Socket.Close();
			}
			
			pollGroup = 0;
			Socket = 0;
			return true;
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

						OnConnecting( connection, info );
					}
					break;
				case ConnectionState.Connected:
					if ( Connecting.Contains( connection ) && !Connected.Contains( connection ) )
					{
						Connecting.Remove( connection );
						Connected.Add( connection );

						OnConnected( connection, info );
					}
					break;
				case ConnectionState.ClosedByPeer:
				case ConnectionState.ProblemDetectedLocally:
				case ConnectionState.None:
					if ( Connecting.Contains( connection ) || Connected.Contains( connection ) )
					{
						Connecting.Remove( connection );
						Connected.Remove( connection );

						OnDisconnected( connection, info );
					}
					break;
			}
		}

		/// <summary>
		/// Default behaviour is to accept every connection
		/// </summary>
		public virtual void OnConnecting( Connection connection, ConnectionInfo info )
		{
			if ( Interface != null )
			{
				Interface.OnConnecting( connection, info );
			}
			else
			{
				connection.Accept();
			}			
		}

		/// <summary>
		/// Client is connected. They move from connecting to Connections
		/// </summary>
		public virtual void OnConnected( Connection connection, ConnectionInfo info )
		{
			SteamNetworkingSockets.Internal.SetConnectionPollGroup( connection, pollGroup );

			Interface?.OnConnected( connection, info );
		}

		/// <summary>
		/// The connection has been closed remotely or disconnected locally. Check data.State for details.
		/// </summary>
		public virtual void OnDisconnected( Connection connection, ConnectionInfo info )
		{
			if ( Interface != null )
			{
				Interface.OnDisconnected( connection, info );
			}
			else
			{
				connection.Close();
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
				OnMessage( msg.Connection, msg.Identity, msg.DataPtr, msg.DataSize, msg.RecvTime, msg.MessageNumber, msg.Channel );
			}
			finally
			{
				//
				// Releases the message
				//
				NetMsg.InternalRelease( (NetMsg*) msgPtr );
			}
		}

		public virtual void OnMessage( Connection connection, NetIdentity identity, IntPtr data, int size, long messageNum, long recvTime, int channel )
		{
			Interface?.OnMessage( connection, identity, data, size, messageNum, recvTime, channel );
		}
	}
}
