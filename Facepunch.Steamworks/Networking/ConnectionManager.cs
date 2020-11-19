using Steamworks.Data;
using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	public class ConnectionManager
	{
		/// <summary>
		/// An optional interface to use instead of deriving
		/// </summary>
		public IConnectionManager Interface { get; set; }

		/// <summary>
		/// The actual connection we're managing
		/// </summary>
		public Connection Connection;

		/// <summary>
		/// The last received ConnectionInfo
		/// </summary>
		public ConnectionInfo ConnectionInfo { get; internal set; }

		public bool Connected = false;
		public bool Connecting = true;

		public string ConnectionName
		{
			get => Connection.ConnectionName;
			set => Connection.ConnectionName = value;
		}

		public long UserData 
		{
			get => Connection.UserData;
			set => Connection.UserData = value;
		}

		public void Close( bool linger = false, int reasonCode = 0, string debugString = "Closing Connection" )
		{
			Connection.Close( linger, reasonCode, debugString );
		}

		public override string ToString() => Connection.ToString();

		public virtual void OnConnectionChanged( ConnectionInfo info )
		{
			ConnectionInfo = info;

			//
			// Some notes:
			// - Update state before the callbacks, in case an exception is thrown
			// - ConnectionState.None happens when a connection is destroyed, even if it was already disconnected (ClosedByPeer / ProblemDetectedLocally)
			//
			switch ( info.State )
			{
				case ConnectionState.Connecting:
					if ( !Connecting && !Connected )
					{
						Connecting = true;

						OnConnecting( info );
					}
					break;
				case ConnectionState.Connected:
					if ( Connecting && !Connected )
					{
						Connecting = false;
						Connected = true;

						OnConnected( info );
					}
					break;
				case ConnectionState.ClosedByPeer:
				case ConnectionState.ProblemDetectedLocally:
				case ConnectionState.None:
					if ( Connecting || Connected )
					{
						Connecting = false;
						Connected = false;

						OnDisconnected( info );
					}
					break;
			}
		}

		/// <summary>
		/// We're trying to connect!
		/// </summary>
		public virtual void OnConnecting( ConnectionInfo info )
		{
			Interface?.OnConnecting( info );
		}

		/// <summary>
		/// Client is connected. They move from connecting to Connections
		/// </summary>
		public virtual void OnConnected( ConnectionInfo info )
		{
			Interface?.OnConnected( info );
		}

		/// <summary>
		/// The connection has been closed remotely or disconnected locally. Check data.State for details.
		/// </summary>
		public virtual void OnDisconnected( ConnectionInfo info )
		{
			Interface?.OnDisconnected( info );
		}

		public int Receive( int bufferSize = 32, bool receiveToEnd = true )
		{
			int processed = 0;
			IntPtr messageBuffer = Marshal.AllocHGlobal( IntPtr.Size * bufferSize );

			try
			{
				processed = SteamNetworkingSockets.Internal.ReceiveMessagesOnConnection( Connection, messageBuffer, bufferSize );

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
				OnMessage( msg.DataPtr, msg.DataSize, msg.RecvTime, msg.MessageNumber, msg.Channel );
			}
			finally
			{
				//
				// Releases the message
				//
				NetMsg.InternalRelease( (NetMsg*) msgPtr );
			}
		}

		public virtual void OnMessage( IntPtr data, int size, long messageNum, long recvTime, int channel )
		{
			Interface?.OnMessage( data, size, messageNum, recvTime, channel );
		}
	}
}