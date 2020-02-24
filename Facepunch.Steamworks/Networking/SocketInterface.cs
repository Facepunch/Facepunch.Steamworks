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
	public class SocketInterface
	{
		public List<Connection> Connecting = new List<Connection>();
		public List<Connection> Connected = new List<Connection>();
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

				Console.WriteLine( "Closing Socket!" );
				Console.WriteLine( Socket.ToString() );

				Socket.Close();
			}

			pollGroup = 0;
			Socket = 0;
			return true;
		}

		public virtual void OnConnectionChanged( Connection connection, ConnectionInfo data )
		{
			switch ( data.State )
			{
				case ConnectionState.Connecting:
					OnConnecting( connection, data );
					break;
				case ConnectionState.Connected:
					OnConnected( connection, data );
					break;
				case ConnectionState.ClosedByPeer:
				case ConnectionState.ProblemDetectedLocally:
				case ConnectionState.None:
					OnDisconnected( connection, data );
					break;
			}
		}

		/// <summary>
		/// Default behaviour is to accept every connection
		/// </summary>
		public virtual void OnConnecting( Connection connection, ConnectionInfo data )
		{
			connection.Accept();
			Connecting.Add( connection );

			SteamNetworkingSockets.Internal.SetConnectionPollGroup( connection, pollGroup );
		}

		/// <summary>
		/// Client is connected. They move from connecting to Connections
		/// </summary>
		public virtual void OnConnected( Connection connection, ConnectionInfo data )
		{
			Connecting.Remove( connection );
			Connected.Add( connection );
		}

		/// <summary>
		/// The connection has been closed remotely or disconnected locally. Check data.State for details.
		/// </summary>
		public virtual void OnDisconnected( Connection connection, ConnectionInfo data )
		{
			connection.Close();

			Connecting.Remove( connection );
			Connected.Remove( connection );
		}

		public void Receive( int bufferSize = 32 )
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
			if ( processed == bufferSize )
				Receive( bufferSize );
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

		}
	}
}