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

		/// <summary>
		/// The poll group this manager drains in <see cref="Receive"/>. Every connection it
		/// accepts is added to this group, which is why one <see cref="Receive"/> call services
		/// every client instead of one call per client.
		///
		/// <para>
		/// Exposed so you can add connections this manager did not create — a connection you
		/// opened yourself with <see cref="SteamNetworkingSockets.ConnectRelay{T}(SteamId, int)"/>,
		/// for instance — and have their messages arrive through the same
		/// <see cref="OnMessage"/> pump. Do not <see cref="Data.PollGroup.Destroy"/> it; this
		/// manager owns it and destroys it in <see cref="Close"/>.
		/// </para>
		/// </summary>
		public PollGroup PollGroup => new PollGroup { Id = pollGroup.Value };

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

		/// <summary>
		/// Drains queued messages from every connection on this socket and dispatches them to
		/// <see cref="OnMessage"/>. Call this once per tick.
		/// </summary>
		/// <param name="bufferSize">
		/// How many messages to fetch per native call, 1..<see cref="PollGroup.MaxReceiveBufferSize"/>.
		/// The buffer is <c>stackalloc</c>'d, so this bounds stack usage to
		/// <c>bufferSize * sizeof(void*)</c> bytes.
		/// </param>
		/// <param name="receiveToEnd">Keep fetching until Steam's queue is empty.</param>
		/// <returns>How many messages were dispatched.</returns>
		/// <remarks>
		/// <para>
		/// Allocation-free. This is the dedicated-server receive path, so it runs at tick rate
		/// with every connected player's traffic flowing through it, and anything it allocates
		/// becomes continuous GC pressure. It previously cost an <c>AllocHGlobal</c>/
		/// <c>FreeHGlobal</c> pair per call plus a <c>Marshal.PtrToStructure&lt;NetMsg&gt;</c> per
		/// message - the latter boxes, at roughly 232 bytes each. A 100-player server at
		/// 2,000 msg/s was producing on the order of 460 KB/s of garbage.
		/// </para>
		/// <para>
		/// Message fields are now read straight through <c>NetMsg*</c>, and the drain loop is a
		/// <c>while</c> rather than recursion so a sustained flood cannot grow the stack.
		/// </para>
		/// </remarks>
		public unsafe int Receive( int bufferSize = 32, bool receiveToEnd = true )
		{
			if ( bufferSize < 1 || bufferSize > PollGroup.MaxReceiveBufferSize ) throw new ArgumentOutOfRangeException( nameof( bufferSize ) );

			int totalProcessed = 0;
			NetMsg** messageBuffer = stackalloc NetMsg*[bufferSize];

			while ( true )
			{
				int processed = SteamNetworkingSockets.Internal.ReceiveMessagesOnPollGroup( pollGroup, new IntPtr( &messageBuffer[0] ), bufferSize );
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
					// A throwing handler must not leak the messages Steam handed us. Release
					// whatever is still outstanding, then let the exception continue.
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
				// Overwhelmed our buffer, keep going
				//
				if ( !receiveToEnd || processed < bufferSize )
					break;
			}

			return totalProcessed;
		}

		internal unsafe void ReceiveMessage( ref NetMsg* msg )
		{
			try
			{
				// Argument order matters here: OnMessage takes ( ..., messageNum, recvTime, ... ).
				// These two were previously passed the other way round, so every consumer
				// received the microsecond timestamp as the message number and vice versa.
				OnMessage( msg->Connection, msg->Identity, msg->DataPtr, msg->DataSize, msg->MessageNumber, msg->RecvTime, msg->Channel );
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

		public virtual void OnMessage( Connection connection, NetIdentity identity, IntPtr data, int size, long messageNum, long recvTime, int channel )
		{
			Interface?.OnMessage( connection, identity, data, size, messageNum, recvTime, channel );
		}
	}
}