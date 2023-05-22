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

				try
				{
					for ( int i = 0; i < processed; i++ )
					{
						ReceiveMessage( ref messageBuffer[i] );
					}
				}
				catch
				{
					for ( int i = 0; i < processed; i++ )
					{
						if ( messageBuffer[i] != null )
						{
							NetMsg.InternalRelease( messageBuffer[i] );
						}
					}

					throw;
				}
				

				//
				// Keep going if receiveToEnd and we filled the buffer
				//
				if ( !receiveToEnd || processed < bufferSize )
					break;
			}

			return totalProcessed;
		}

		/// <summary>
		/// Sends a message to multiple connections.
		/// </summary>
		/// <param name="connections">The connections to send the message to.</param>
		/// <param name="connectionCount">The number of connections to send the message to, to allow reusing the connections array.</param>
		/// <param name="ptr">Pointer to the message data.</param>
		/// <param name="size">Size of the message data.</param>
		/// <param name="sendType">Flags to control delivery of the message.</param>
		/// <param name="results">An optional array to hold the results of sending the messages for each connection.</param>
		public unsafe void SendMessages( Connection[] connections, int connectionCount, IntPtr ptr, int size, SendType sendType = SendType.Reliable, Result[] results = null )
		{
			if ( connections == null )
				throw new ArgumentNullException( nameof( connections ) );
			if ( connectionCount < 0 || connectionCount > connections.Length )
				throw new ArgumentException( "`connectionCount` must be between 0 and `connections.Length`", nameof( connectionCount ) );
			if ( results != null && connectionCount > results.Length )
				throw new ArgumentException( "`results` must have at least `connectionCount` entries", nameof( results ) );
			if ( connectionCount > 1024 ) // restricting this because we stack allocate based on this value
				throw new ArgumentOutOfRangeException( nameof( connectionCount ) );
			if ( ptr == IntPtr.Zero )
				throw new ArgumentNullException( nameof( ptr ) );
			if ( size == 0 )
				throw new ArgumentException( "`size` cannot be zero", nameof( size ) );

			if ( connectionCount == 0 )
				return;

			// SendMessages does not make a copy of the data. We will need to copy because we don't want to force the caller to keep the pointer valid.
			//   1. We don't want a copy per message. They all refer to the same data. This is the benefit of using Broadcast vs. many sends.
			//   2. We need to use unmanaged memory. Managed memory may move around and invalidate pointers so it's not an option.
			//   3. We'll use a reference counter and custom free() function to release this unmanaged memory.
			var copyPtr = BufferManager.Get( size, connectionCount );
			Buffer.MemoryCopy( (void*)ptr, (void*)copyPtr, size, size );

			var messages = stackalloc NetMsg*[connectionCount];
			var messageNumberOrResults = stackalloc long[results != null ? connectionCount : 0];

			for ( var i = 0; i < connectionCount; i++ )
			{
				messages[i] = SteamNetworkingUtils.AllocateMessage();
				messages[i]->Connection = connections[i];
				messages[i]->Flags = sendType;
				messages[i]->DataPtr = copyPtr;
				messages[i]->DataSize = size;
				messages[i]->FreeDataPtr = BufferManager.FreeFunctionPointer;
			}

			SteamNetworkingSockets.Internal.SendMessages( connectionCount, messages, messageNumberOrResults );

			if (results == null)
				return;

			for ( var i = 0; i < connectionCount; i++ )
			{
				if ( messageNumberOrResults[i] < 0 )
				{
					results[i] = (Result)( -messageNumberOrResults[i] );
				}
				else
				{
					results[i] = Result.OK;
				}
			}
		}

		/// <summary>
		/// Ideally should be using an IntPtr version unless you're being really careful with the byte[] array and 
		/// you're not creating a new one every frame (like using .ToArray())
		/// </summary>
		public unsafe void SendMessages( Connection[] connections, int connectionCount, byte[] data, SendType sendType = SendType.Reliable, Result[] results = null )
		{
			fixed ( byte* ptr = data )
			{
				SendMessages( connections, connectionCount, (IntPtr)ptr, data.Length, sendType, results );
			}
		}

		/// <summary>
		/// Ideally should be using an IntPtr version unless you're being really careful with the byte[] array and 
		/// you're not creating a new one every frame (like using .ToArray())
		/// </summary>
		public unsafe void SendMessages( Connection[] connections, int connectionCount, byte[] data, int offset, int length, SendType sendType = SendType.Reliable, Result[] results = null )
		{
			fixed ( byte* ptr = data )
			{
				SendMessages( connections, connectionCount, (IntPtr)ptr + offset, length, sendType, results );
			}
		}

		/// <summary>
		/// This creates a ton of garbage - so don't do anything with this beyond testing!
		/// </summary>
		public void SendMessages( Connection[] connections, int connectionCount, string str, SendType sendType = SendType.Reliable, Result[] results = null )
		{
			var bytes = System.Text.Encoding.UTF8.GetBytes( str );
			SendMessages( connections, connectionCount, bytes, sendType, results );
		}

		internal unsafe void ReceiveMessage( ref NetMsg* msg )
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
				msg = null;
			}
		}

		public virtual void OnMessage( IntPtr data, int size, long messageNum, long recvTime, int channel )
		{
			Interface?.OnMessage( data, size, messageNum, recvTime, channel );
		}
	}
}