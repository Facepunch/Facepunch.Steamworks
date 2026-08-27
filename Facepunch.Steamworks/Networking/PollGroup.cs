using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	/// <summary>
	/// A handle to a Steam <b>poll group</b> — a set of connections whose inbound messages
	/// are merged into one queue so that the whole set can be drained with a
	/// <b>single</b> native call.
	///
	/// <para>
	/// <b>Why this exists.</b> Without a poll group a server has to call
	/// <c>ReceiveMessagesOnConnection</c> once for every connected client, every tick. At
	/// 100 players and 60 ticks/second that is 6,000 managed→native transitions per second
	/// that produce nothing but "no messages" for most of the calls. A poll group collapses
	/// that to 60 calls per second regardless of player count: the cost of polling stops
	/// scaling with the number of players and starts scaling with the number of
	/// <i>messages</i>, which is what you actually wanted.
	/// </para>
	///
	/// <para>
	/// <b>You may already be using one.</b> <see cref="SocketManager"/> creates a poll group
	/// internally and adds every accepted connection to it, so if you use
	/// <see cref="SocketManager"/> you get this for free (see
	/// <see cref="SocketManager.PollGroup"/>). This type exists for the cases the built-in
	/// manager cannot cover: partitioning clients across several groups (per match, per
	/// room, lobby traffic vs. gameplay traffic), polling connections that were created
	/// outside a <see cref="SocketManager"/>, or building your own transport layer on top
	/// of the raw <see cref="Connection"/> API.
	/// </para>
	///
	/// <para>
	/// <b>Lifetime.</b> This is a raw handle, exactly like <see cref="Socket"/> and
	/// <see cref="Connection"/>. Copying it copies the handle, not the group. You must call
	/// <see cref="Destroy"/> exactly once when you are finished, otherwise the group leaks
	/// inside the Steam networking library for the lifetime of the process. It is
	/// deliberately <i>not</i> <see cref="IDisposable"/>: a struct handle that gets copied
	/// around freely would make double-dispose far too easy to write by accident.
	/// </para>
	/// </summary>
	/// <example>
	/// A complete server tick, allocating nothing per message:
	/// <code>
	/// // Startup
	/// var group = SteamNetworkingSockets.CreatePollGroup();
	///
	/// // Whenever a client finishes connecting
	/// void OnConnected( Connection c, ConnectionInfo info )
	/// {
	///     c.Accept();
	///     group.Add( c );
	/// }
	///
	/// // Once per tick — ONE native call drains every client
	/// group.Receive( ( connection, identity, data, size, messageNum, recvTime, channel ) =&gt;
	/// {
	///     // 'data' is Steam-owned and is freed the moment this callback returns.
	///     // Copy it if you need to keep it.
	///     Dispatch( connection, data, size );
	/// } );
	///
	/// // Shutdown
	/// group.Destroy();
	/// </code>
	/// </example>
	[StructLayout( LayoutKind.Sequential )]
	public struct PollGroup : IEquatable<PollGroup>
	{
		internal uint Id;

		/// <summary>
		/// The largest number of messages a single <c>Receive</c> call will ask Steam for at
		/// once. The receive buffer is <c>stackalloc</c>'d, so this bounds stack usage to
		/// <c>MaxReceiveBufferSize * sizeof(void*)</c> bytes (2 KB on 64-bit).
		/// </summary>
		public const int MaxReceiveBufferSize = 256;

		/// <summary>
		/// An invalid poll group handle. Equivalent to Valve's
		/// <c>k_HSteamNetPollGroup_Invalid</c>.
		/// </summary>
		public static PollGroup Invalid => default;

		/// <summary>
		/// <see langword="true"/> if this handle is not <see cref="Invalid"/>.
		///
		/// <para>
		/// This only tells you the handle is non-zero. It cannot tell you the group is still
		/// alive — Steam gives us no way to interrogate that, and a handle you already
		/// destroyed will still report <see langword="true"/>.
		/// </para>
		/// </summary>
		public bool IsValid => Id != 0;

		public override string ToString() => Id.ToString();
		public bool Equals( PollGroup other ) => Id == other.Id;
		public override bool Equals( object obj ) => obj is PollGroup other && Id == other.Id;
		public override int GetHashCode() => Id.GetHashCode();
		public static bool operator ==( PollGroup value1, PollGroup value2 ) => value1.Equals( value2 );
		public static bool operator !=( PollGroup value1, PollGroup value2 ) => !value1.Equals( value2 );

		/// <summary>
		/// Destroy this poll group.
		///
		/// <para>
		/// Valve documents exactly one consequence: "If there are any connections in the poll
		/// group, they are removed from the group, and left in a state where they are not part
		/// of any poll group." They are <b>not</b> closed; their messages simply go back to
		/// being retrievable per-connection.
		/// </para>
		///
		/// <para>
		/// <b>Inferred, not documented:</b> the header says nothing about messages already
		/// queued <i>on the group</i> that you never received. Drain the group one last time
		/// before destroying it if losing those would matter.
		/// </para>
		///
		/// <para>
		/// Call this exactly once. The handle is not cleared for you (this is a value type,
		/// so it could not be cleared reliably anyway) — overwrite your copy with
		/// <see cref="Invalid"/> if you want to make reuse obvious.
		/// </para>
		/// </summary>
		/// <returns>
		/// <see langword="false"/> if the handle was not a valid poll group — which for a
		/// handle you created means it was already destroyed.
		/// </returns>
		/// <exception cref="InvalidOperationException">The handle is <see cref="Invalid"/>.</exception>
		public bool Destroy()
		{
			ThrowIfInvalid();

			return SteamNetworkingSockets.Internal.DestroyPollGroup( Id );
		}

		/// <summary>
		/// Put <paramref name="connection"/> into this poll group. Its inbound messages will
		/// from now on be returned by <see cref="Receive(PollGroupMessage, int, bool)"/>
		/// instead of by <c>ReceiveMessagesOnConnection</c>.
		///
		/// <para>
		/// A connection belongs to at most one poll group. Adding it here silently removes it
		/// from whatever group it was in before — including the group
		/// <see cref="SocketManager"/> manages for you, which is a trap worth knowing about
		/// if you mix the two.
		/// </para>
		///
		/// <para>
		/// Messages that arrived before the connection joined are moved into the group's
		/// queue, roughly in the order they would have had if the connection had been a
		/// member all along. Valve documents that ordering as approximate, so do not build
		/// anything that depends on the exact interleaving at the moment of joining.
		/// </para>
		/// </summary>
		/// <returns>
		/// <see langword="false"/> if <paramref name="connection"/> is not a valid connection
		/// handle, or if this group no longer exists.
		/// </returns>
		/// <exception cref="InvalidOperationException">The handle is <see cref="Invalid"/>.</exception>
		/// <exception cref="ArgumentException"><paramref name="connection"/> is the invalid connection handle.</exception>
		public bool Add( Connection connection )
		{
			ThrowIfInvalid();

			if ( connection.Id == 0 )
				throw new ArgumentException( "Invalid Connection", nameof( connection ) );

			return SteamNetworkingSockets.Internal.SetConnectionPollGroup( connection, Id );
		}

		/// <summary>
		/// Take <paramref name="connection"/> out of this poll group without closing it. Its
		/// messages become retrievable per-connection again.
		///
		/// <para>
		/// Steam has no "remove from group" call; this is
		/// <c>SetConnectionPollGroup( conn, k_HSteamNetPollGroup_Invalid )</c>, so it removes
		/// the connection from <i>whatever</i> group it is currently in, which is not
		/// necessarily this one. You normally do not need to call this at all — closing a
		/// connection removes it from its group as a matter of course.
		/// </para>
		/// </summary>
		/// <returns><see langword="false"/> if <paramref name="connection"/> is not a valid connection handle.</returns>
		/// <exception cref="ArgumentException"><paramref name="connection"/> is the invalid connection handle.</exception>
		public bool Remove( Connection connection )
		{
			if ( connection.Id == 0 )
				throw new ArgumentException( "Invalid Connection", nameof( connection ) );

			return SteamNetworkingSockets.Internal.SetConnectionPollGroup( connection, 0 );
		}

		/// <summary>
		/// ZERO-ALLOCATION receive. Drains up to <paramref name="bufferSize"/> messages per
		/// native call — from <b>all</b> connections in the group at once — and hands each one
		/// to <paramref name="onMessage"/> as a raw pointer plus a size.
		///
		/// <para>
		/// <b>The buffer belongs to Steam.</b> It is released as soon as
		/// <paramref name="onMessage"/> returns. Copy it (ideally into a pooled buffer) if it
		/// has to outlive the callback. This is the same contract as
		/// <see cref="SocketManager.OnMessage"/> and
		/// <c>SteamNetworkingMessages.ReceiveMessagesOnChannel</c>.
		/// </para>
		///
		/// <para>
		/// Nothing is allocated per message or per call: the array of message pointers is
		/// <c>stackalloc</c>'d, and no managed <c>byte[]</c> is ever produced. Hoist
		/// <paramref name="onMessage"/> into a cached field rather than writing a lambda at
		/// the call site — a lambda that captures locals allocates a closure every tick and
		/// throws away the point of this method.
		/// </para>
		///
		/// <para>
		/// <b>Ordering.</b> Messages from one connection arrive in order. Messages from
		/// different connections are interleaved and are only loosely ordered relative to each
		/// other, so use the <c>connection</c> argument to demultiplex — do not assume
		/// messages arrive grouped by sender.
		/// </para>
		///
		/// <para>
		/// If <paramref name="onMessage"/> throws, every message still held from that native
		/// call is released before the exception propagates, so a throwing handler leaks
		/// nothing. The messages it never saw are lost.
		/// </para>
		/// </summary>
		/// <param name="onMessage">Invoked once per received message. Must not be <see langword="null"/>.</param>
		/// <param name="bufferSize">
		/// How many messages to fetch per native call, 1..<see cref="MaxReceiveBufferSize"/>.
		/// Bigger means fewer native calls when traffic is heavy; the default of 32 is a
		/// reasonable middle for most servers.
		/// </param>
		/// <param name="receiveToEnd">
		/// Keep calling until the queue is empty. Leave this on unless you deliberately want
		/// to cap how much work a single tick does.
		/// </param>
		/// <returns>The total number of messages processed.</returns>
		/// <exception cref="InvalidOperationException">
		/// The handle is <see cref="Invalid"/>, or Steam rejected it — see the remark on
		/// negative returns below.
		/// </exception>
		/// <exception cref="ArgumentNullException"><paramref name="onMessage"/> is <see langword="null"/>.</exception>
		/// <exception cref="ArgumentOutOfRangeException"><paramref name="bufferSize"/> is outside 1..<see cref="MaxReceiveBufferSize"/>.</exception>
		/// <remarks>
		/// <b>Inferred, not documented:</b> Valve documents that the per-connection
		/// <c>ReceiveMessagesOnConnection</c> returns -1 for an invalid handle, but says
		/// nothing about the return value of <c>ReceiveMessagesOnPollGroup</c>. We assume the
		/// same convention and turn any negative return into an exception rather than
		/// letting it corrupt the running total — a destroyed-then-reused handle would
		/// otherwise show up as a silently-empty tick.
		/// </remarks>
		public unsafe int Receive( PollGroupMessage onMessage, int bufferSize = 32, bool receiveToEnd = true )
		{
			ThrowIfInvalid();

			if ( onMessage == null ) throw new ArgumentNullException( nameof( onMessage ) );
			if ( bufferSize < 1 || bufferSize > MaxReceiveBufferSize ) throw new ArgumentOutOfRangeException( nameof( bufferSize ) );

			int totalProcessed = 0;
			NetMsg** messageBuffer = stackalloc NetMsg*[bufferSize];

			while ( true )
			{
				int processed = SteamNetworkingSockets.Internal.ReceiveMessagesOnPollGroup( Id, new IntPtr( &messageBuffer[0] ), bufferSize );

				if ( processed < 0 )
					throw new InvalidOperationException( $"ReceiveMessagesOnPollGroup( {Id} ) failed - the poll group handle was rejected by Steam. Has it already been destroyed?" );

				totalProcessed += processed;

				try
				{
					for ( int i = 0; i < processed; i++ )
					{
						ReceiveMessage( ref messageBuffer[i], onMessage );
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

		internal static unsafe void ReceiveMessage( ref NetMsg* msg, PollGroupMessage onMessage )
		{
			try
			{
				onMessage( msg->Connection, msg->Identity, msg->DataPtr, msg->DataSize, msg->MessageNumber, msg->RecvTime, msg->Channel );
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

		/// <summary>
		/// CONVENIENCE ONLY — copies every message into a freshly allocated
		/// <c>byte[]</c> before handing it over.
		///
		/// <para>
		/// This allocates one array <b>per message</b> and hands it to the GC afterwards. It
		/// is here so that prototypes, tools and tests stay readable; it is the wrong thing
		/// to call from a server tick. Use
		/// <see cref="Receive(PollGroupMessage, int, bool)"/> there instead.
		/// </para>
		/// </summary>
		/// <param name="onMessage">Invoked once per received message with an owned copy of the payload.</param>
		/// <param name="bufferSize">How many messages to fetch per native call, 1..<see cref="MaxReceiveBufferSize"/>.</param>
		/// <param name="receiveToEnd">Keep calling until the queue is empty.</param>
		/// <returns>The total number of messages processed.</returns>
		/// <exception cref="InvalidOperationException">The handle is <see cref="Invalid"/>.</exception>
		/// <exception cref="ArgumentNullException"><paramref name="onMessage"/> is <see langword="null"/>.</exception>
		/// <exception cref="ArgumentOutOfRangeException"><paramref name="bufferSize"/> is outside 1..<see cref="MaxReceiveBufferSize"/>.</exception>
		public int Receive( Action<Connection, NetIdentity, byte[]> onMessage, int bufferSize = 32, bool receiveToEnd = true )
		{
			if ( onMessage == null ) throw new ArgumentNullException( nameof( onMessage ) );

			return Receive( ( connection, identity, data, size, messageNum, recvTime, channel ) =>
			{
				var copy = new byte[size];
				Marshal.Copy( data, copy, 0, size );
				onMessage( connection, identity, copy );
			}, bufferSize, receiveToEnd );
		}

		private void ThrowIfInvalid()
		{
			if ( Id == 0 )
				throw new InvalidOperationException( "This PollGroup handle is invalid. Create one with SteamNetworkingSockets.CreatePollGroup()." );
		}
	}

	/// <summary>
	/// Receives one message drained from a <see cref="PollGroup"/>.
	///
	/// <para>
	/// <paramref name="data"/> points into memory owned by Steam and is freed the instant
	/// this delegate returns — copy it if you need to keep it. The parameter list matches
	/// <see cref="ISocketManager.OnMessage"/> so an existing handler can be forwarded to
	/// directly.
	/// </para>
	/// </summary>
	/// <param name="connection">Which connection in the group sent this. Use it to demultiplex.</param>
	/// <param name="identity">Who sent it.</param>
	/// <param name="data">Pointer to the payload. Steam-owned, valid only for this call.</param>
	/// <param name="size">Payload length in bytes.</param>
	/// <param name="messageNum">Per-connection, per-lane message number assigned by Steam.</param>
	/// <param name="recvTime">Local receive timestamp, in microseconds, on Steam's networking clock.</param>
	/// <param name="channel">Lane / channel the message arrived on.</param>
	public delegate void PollGroupMessage( Connection connection, NetIdentity identity, IntPtr data, int size, long messageNum, long recvTime, int channel );
}
