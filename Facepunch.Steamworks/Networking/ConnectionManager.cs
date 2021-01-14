using Steamworks.Data;
using System;

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

		public unsafe int Receive( int bufferSize = 32, bool receiveToEnd = true )
        {
            if ( bufferSize < 1 || bufferSize > 256 ) throw new ArgumentOutOfRangeException( nameof( bufferSize ) );

			int totalProcessed = 0;
            NetMsg** messageBuffer = stackalloc NetMsg*[bufferSize];
			
			while ( true )
            {
				int processed = SteamNetworkingSockets.Internal.ReceiveMessagesOnConnection( Connection, new IntPtr( &messageBuffer[0] ), bufferSize );
                totalProcessed += processed;

			    for ( int i = 0; i < processed; i++ )
			    {
				    // TODO: if this throws we will leak the remaining NetMsgs (probably not going to happen much though)
				    ReceiveMessage( messageBuffer[i] );
			    }

			    //
			    // Keep going if receiveToEnd and we filled the buffer
			    //
			    if ( !receiveToEnd || processed < bufferSize )
				    break;
			}

			return totalProcessed;
		}

		internal unsafe void ReceiveMessage( NetMsg* msg )
		{
			try
			{
				OnMessage( msg->DataPtr, msg->DataSize, msg->RecvTime, msg->MessageNumber, msg->Channel );
			}
			finally
			{
				//
				// Releases the message
				//
				NetMsg.InternalRelease( msg );
			}
		}

		public virtual void OnMessage( IntPtr data, int size, long messageNum, long recvTime, int channel )
		{
			Interface?.OnMessage( data, size, messageNum, recvTime, channel );
		}
	}
}