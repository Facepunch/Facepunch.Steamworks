using System;
using System.Collections.Generic;

namespace Steamworks.Data
{

	/// <summary>
	/// Used as a base to create your client connection. This creates a socket
	/// to a single connection.
	/// 
	/// You can override all the virtual functions to turn it into what you
	/// want it to do.
	/// </summary>
	public struct Connection : IEquatable<Connection>
	{
		public uint Id { get; set; }

		public bool Equals( Connection other ) => Id == other.Id;
		public override bool Equals( object obj ) => obj is Connection other && Id == other.Id;
		public override int GetHashCode() => Id.GetHashCode();
		public override string ToString() => Id.ToString();
		public static implicit operator Connection( uint value ) => new Connection() { Id = value };
		public static implicit operator uint( Connection value ) => value.Id;
		public static bool operator ==( Connection value1, Connection value2 ) => value1.Equals( value2 );
		public static bool operator !=( Connection value1, Connection value2 ) => !value1.Equals( value2 );

		/// <summary>
		/// Accept an incoming connection that has been received on a listen socket.
		/// </summary>
		public Result Accept()
		{
			return SteamNetworkingSockets.Internal.AcceptConnection( this );
		}

		/// <summary>
		/// Disconnects from the remote host and invalidates the connection handle. Any unread data on the connection is discarded..
		/// reasonCode is defined and used by you.
		/// </summary>
		public bool Close( bool linger = false, int reasonCode = 0, string debugString = "Closing Connection" )
		{
			return SteamNetworkingSockets.Internal.CloseConnection( this, reasonCode, debugString, linger );
		}

		/// <summary>
		/// Get/Set connection user data
		/// </summary>
		public long UserData
		{
			get => SteamNetworkingSockets.Internal.GetConnectionUserData( this );
			set => SteamNetworkingSockets.Internal.SetConnectionUserData( this, value );
		}

		/// <summary>
		/// A name for the connection, used mostly for debugging
		/// </summary>
		public string ConnectionName
		{
			get
			{
				if ( !SteamNetworkingSockets.Internal.GetConnectionName( this, out var strVal ) )
					return "ERROR";

				return strVal;
			}

			set => SteamNetworkingSockets.Internal.SetConnectionName( this, value );
		}

		/// <summary>
		/// This is the best version to use.
		/// </summary>
		public unsafe Result SendMessage( IntPtr ptr, int size, SendType sendType = SendType.Reliable, ushort laneIndex = 0 )
		{
			if ( ptr == IntPtr.Zero )
				throw new ArgumentNullException( nameof( ptr ) );
			if ( size == 0 )
				throw new ArgumentException( "`size` cannot be zero", nameof( size ) );

			var copyPtr = BufferManager.Get( size, 1 );
			Buffer.MemoryCopy( (void*)ptr, (void*)copyPtr, size, size );

			var message = SteamNetworkingUtils.AllocateMessage();
			message->Connection = this;
			message->Flags = sendType;
			message->DataPtr = copyPtr;
			message->DataSize = size;
			message->FreeDataPtr = BufferManager.FreeFunctionPointer;
			message->IdxLane = laneIndex;

			long messageNumber = 0;
			SteamNetworkingSockets.Internal.SendMessages( 1, &message, &messageNumber );

			return messageNumber >= 0
				? Result.OK
				: (Result)(-messageNumber);
		}

		/// <summary>
		/// Ideally should be using an IntPtr version unless you're being really careful with the byte[] array and 
		/// you're not creating a new one every frame (like using .ToArray())
		/// </summary>
		public unsafe Result SendMessage( byte[] data, SendType sendType = SendType.Reliable, ushort laneIndex = 0 )
		{
			fixed ( byte* ptr = data )
			{
				return SendMessage( (IntPtr)ptr, data.Length, sendType, laneIndex );
			}
		}

		/// <summary>
		/// Ideally should be using an IntPtr version unless you're being really careful with the byte[] array and 
		/// you're not creating a new one every frame (like using .ToArray())
		/// </summary>
		public unsafe Result SendMessage( byte[] data, int offset, int length, SendType sendType = SendType.Reliable, ushort laneIndex = 0 )
		{
			fixed ( byte* ptr = data )
			{
				return SendMessage( (IntPtr)ptr + offset, length, sendType, laneIndex );
			}
		}

		/// <summary>
		/// This creates a ton of garbage - so don't do anything with this beyond testing!
		/// </summary>
		public unsafe Result SendMessage( string str, SendType sendType = SendType.Reliable, ushort laneIndex = 0 )
		{
			var bytes = Utility.Utf8NoBom.GetBytes( str );
			return SendMessage( bytes, sendType, laneIndex );
		}

		/// <summary>
		/// Flush any messages waiting on the Nagle timer and send them at the next transmission 
		/// opportunity (often that means right now).
		/// </summary>
		public Result Flush() => SteamNetworkingSockets.Internal.FlushMessagesOnConnection( this );

		/// <summary>
		/// Returns detailed connection stats in text format.  Useful
		/// for dumping to a log, etc.
		/// </summary>
		/// <returns>Plain text connection info</returns>
		public string DetailedStatus()
		{
			if ( SteamNetworkingSockets.Internal.GetDetailedConnectionStatus( this, out var strVal ) != 0 )
				return null;

			return strVal;
		}

		/// <summary>
		/// Returns a small set of information about the real-time state of the connection.
		/// </summary>
		public ConnectionStatus QuickStatus()
		{
			ConnectionStatus connectionStatus = default( ConnectionStatus );

			SteamNetworkingSockets.Internal.GetConnectionRealTimeStatus( this, ref connectionStatus, 0, null );

			return connectionStatus;
		}

		/// <summary>
		/// Configure multiple outbound messages streams ("lanes") on a connection, and
		/// control head-of-line blocking between them.
		/// </summary>
		public Result ConfigureConnectionLanes( int[] lanePriorities, ushort[] laneWeights )
		{
			return SteamNetworkingSockets.Internal.ConfigureConnectionLanes( this, lanePriorities.Length, lanePriorities, laneWeights );
		}

		/// <summary>
		/// Put this connection into <paramref name="group"/> so that its messages are drained
		/// by <see cref="PollGroup.Receive(PollGroupMessage, int, bool)"/> along with every
		/// other connection in that group, instead of needing a receive call of its own.
		///
		/// <para>
		/// A connection belongs to at most one group; this silently removes it from any group
		/// it was already in. Pass <see cref="PollGroup.Invalid"/> to remove it from its group
		/// without joining another. This is the same operation as
		/// <see cref="PollGroup.Add(Connection)"/>, spelled from the connection's side.
		/// </para>
		/// </summary>
		/// <returns><see langword="false"/> if this connection handle is invalid, or if <paramref name="group"/> is a non-zero handle that Steam does not recognise.</returns>
		/// <exception cref="ArgumentException">This connection is the invalid connection handle.</exception>
		public bool SetPollGroup( PollGroup group )
		{
			//
			// Routed through PollGroup so that all three spellings of "SetConnectionPollGroup"
			// - PollGroup.Add, PollGroup.Remove and this - validate identically. Add requires a
			// live group; Remove deliberately does not, because clearing a connection's group is
			// meaningful whichever group it happens to be in.
			//
			return group.IsValid ? group.Add( this ) : group.Remove( this );
		}

		/// <summary>
		/// The FakeIP address of the peer at the other end of this connection — a real-looking
		/// IPv4 address that Valve issues purely as an identifier, so that code which expects
		/// to identify peers by IP keeps working over SDR.
		///
		/// <para>
		/// See <see cref="SteamNetworkingSockets.GetRemoteFakeIPForConnection"/> for the
		/// caveats, in particular that a locally allocated FakeIP is only meaningful inside
		/// this process and is not guaranteed to be stable across connections.
		/// </para>
		/// </summary>
		/// <returns>
		/// <see cref="Result.OK"/> on success, <see cref="Result.IPNotFound"/> if this
		/// connection was not made using the FakeIP system.
		/// </returns>
		public Result GetRemoteFakeIP( out NetAddress address )
		{
			return SteamNetworkingSockets.GetRemoteFakeIPForConnection( this, out address );
		}
	}
}
