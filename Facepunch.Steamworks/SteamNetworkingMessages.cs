using Steamworks.Data;
using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	/// <summary>
	/// Delivery flags controlling how a message is sent: reliability, whether Nagle batching applies,
	/// and whether an undeliverable message is dropped rather than queued. These mirror Valve's
	/// <c>k_nSteamNetworkingSend_*</c> constants exactly.
	/// </summary>
	/// <remarks>
	/// The names are combinations rather than independent bits, so prefer one of the composite values
	/// (<see cref="UnreliableNoNagle"/>, <see cref="UnreliableNoDelay"/>,
	/// <see cref="ReliableNoNagle"/>) over OR-ing flags yourself.
	/// </remarks>
	[Flags]
	public enum SteamNetworkingOptions
	{
		/// <summary>
		/// Fire and forget. The message may be dropped or arrive out of order, and Nagle batching
		/// still applies so it may sit briefly waiting for company. This is the default (value 0), so
		/// passing no flags means unreliable.
		/// </summary>
		Unreliable = 0,

		/// <summary>
		/// Bypass Nagle's algorithm and push what is buffered onto the wire now, instead of waiting to
		/// group this message with others.
		/// </summary>
		/// <remarks>
		/// Valve's header is unusually emphatic here: do not set this on everything just to make
		/// packets arrive sooner. For small messages sent many at a time, leaving Nagle on is more
		/// efficient. The intended use is the <i>last</i> message of a batch &#8212; for example the
		/// final message of a server tick to a given client &#8212; to flush the rest.
		/// </remarks>
		NoNagle = 1,

		/// <summary>
		/// Unreliable, and flushed immediately along with anything already waiting on the Nagle timer.
		/// Equivalent to sending unreliably and then flushing, but in one call.
		/// </summary>
		UnreliableNoNagle = Unreliable | NoNagle,

		/// <summary>
		/// Drop the message rather than queue it if it cannot go out very soon &#8212; because the
		/// connection is still handshaking or negotiating a route, or because too much is already
		/// queued.
		/// </summary>
		/// <remarks>
		/// <b>Only valid on unreliable sends.</b> Valve states that using this on a reliable message
		/// is invalid; the header does not say what happens if you do. A message dropped for this
		/// reason reports <see cref="Result.Ignored"/> rather than an error.
		/// </remarks>
		NoDelay = 4,

		/// <summary>
		/// Unreliable, dropped if it cannot be sent quickly, and not subject to Nagle. This is the
		/// right choice for data that is worthless once late &#8212; voice, or a position update that
		/// a newer one has already superseded.
		/// </summary>
		/// <remarks>
		/// Valve documents the drop conditions concretely: the connection is not fully connected, or
		/// enough messages are queued that this one would not reach the wire within roughly 200ms.
		/// Dropped messages return <see cref="Result.Ignored"/>.
		/// </remarks>
		UnreliableNoDelay = Unreliable | NoDelay | NoNagle,

		/// <summary>
		/// Guaranteed delivery, in order. Steam handles fragmentation and reassembly, so a reliable
		/// message can be far larger than a packet, and uses a sliding window for bulk transfers.
		/// Nagle batching still applies.
		/// </summary>
		/// <remarks>
		/// If you are porting from the old P2P API, note Valve's migration warning: this is
		/// <b>not</b> the equivalent of <c>k_EP2PSendReliable</c>, it behaves like
		/// <c>k_EP2PSendReliableWithBuffering</c>. <see cref="ReliableNoNagle"/> is the direct
		/// equivalent.
		/// </remarks>
		Reliable = 8,

		/// <summary>
		/// Reliable, but flushed immediately instead of waiting on the Nagle timer. Valve notes this
		/// is the exact equivalent of the legacy <c>k_EP2PSendReliable</c>.
		/// </summary>
		ReliableNoNagle = Reliable | NoNagle,

		/// <summary>
		/// If the session with this peer has broken, silently tear it down and start a new one instead
		/// of failing the send.
		/// </summary>
		/// <remarks>
		/// Convenient, but it hides connection failures from you: without it a broken session surfaces
		/// as <see cref="Result.NoConnection"/> from the send. Note also that Valve's session failure
		/// details time out after a while, so if you do want to diagnose a failure you must read
		/// <see cref="SteamNetworkingMessages.GetSessionConnectionInfo"/> promptly rather than later.
		/// </remarks>
		AutoRestartBrokenSession = 32
	}

	/// <summary>
	/// Connectionless peer-to-peer messaging, addressed by Steam identity rather than by socket.
	/// This is the modern replacement for the deprecated legacy <c>ISteamNetworking</c> P2P API, and
	/// the easier of the two current options &#8212; you send to a user, Steam works out the route
	/// (direct, NAT-punched, or through Valve's relay network), and there is no connection object to
	/// manage.
	/// </summary>
	/// <remarks>
	/// <para>
	/// Use this when messaging is naturally addressed to <i>people</i>. Use
	/// <see cref="SteamNetworkingSockets"/> instead when you want an explicit connection with a
	/// lifecycle, a listen socket, or a dedicated server.
	/// </para>
	/// <para>
	/// <b>Sessions are implicit but not automatic.</b> The first message from a peer you have not
	/// talked to raises <see cref="OnSessionRequest"/>, and messages from them are <b>not</b>
	/// delivered until you call <see cref="AcceptSessionWithUser"/>. Forgetting to handle that event
	/// is the usual reason "nothing arrives" &#8212; sends in the other direction still appear to work.
	/// Sending to a peer implicitly accepts a session with them.
	/// </para>
	/// <para>
	/// Sessions that go unused time out after a few minutes, per Valve's header, and a timed-out peer
	/// simply raises <see cref="OnSessionRequest"/> again next time they talk to you.
	/// </para>
	/// <para>
	/// Nothing is delivered unless you drain it. Call one of the
	/// <see cref="ReceiveMessagesOnChannel(int, MessageIntercept, int, bool)"/> overloads every frame
	/// for each channel you use; there is no callback that pushes messages to you.
	/// </para>
	/// </remarks>
	/// <example>
	/// A minimal peer-to-peer pump:
	/// <code>
	/// // Accept anyone who talks to us. In a real game, check they belong in your lobby first.
	/// SteamNetworkingMessages.OnSessionRequest += identity =&gt;
	/// {
	///     var id = identity;                                     // need an lvalue for ref
	///     SteamNetworkingMessages.AcceptSessionWithUser( ref id );
	/// };
	///
	/// // Send - SteamId converts to NetIdentity implicitly
	/// NetIdentity peer = friendSteamId;
	/// SteamNetworkingMessages.SendMessageToUser( ref peer, payload,
	///                                            SteamNetworkingOptions.Reliable, channel: 0 );
	///
	/// // Receive - call this every frame, per channel
	/// SteamNetworkingMessages.ReceiveMessagesOnChannel( 0, ( from, channel, data ) =&gt;
	/// {
	///     Handle( from, data );
	/// } );
	/// </code>
	/// </example>
	public class SteamNetworkingMessages : SteamSharedClass<SteamNetworkingMessages>
	{
		internal static ISteamNetworkingMessages Internal => Interface as ISteamNetworkingMessages;

		internal override bool InitializeInterface( bool server )
		{
			SetInterface( server, new ISteamNetworkingMessages( server ) );
			if ( Interface.Self == IntPtr.Zero ) return false;

			InstallEvents( server );

			return true;
		}

		internal static void InstallEvents( bool server )
		{
			Dispatch.Install<SteamNetworkingMessagesSessionRequest_t>( x => OnSessionRequest?.Invoke( x.IdentityRemote), server );
			Dispatch.Install<SteamNetworkingMessagesSessionFailed_t>( x => OnSessionFailed?.Invoke( x.Info), server );
		}

		/// <summary>
		/// Raised when a peer you have no session with sends you something. <b>Their messages are not
		/// delivered until you call <see cref="AcceptSessionWithUser"/></b>, so a handler here is
		/// mandatory for incoming traffic to work at all.
		/// </summary>
		/// <remarks>
		/// <para>
		/// This is your authorisation point, and the only one you get. Anyone on Steam can trigger it,
		/// so check the identity belongs in your lobby or match before accepting; accepting everyone
		/// leaves you open to unsolicited traffic.
		/// </para>
		/// <para>
		/// Ignoring the event is a valid way to refuse &#8212; there is no explicit "reject". The
		/// pending session simply times out. Note this is a plain <see cref="Action{T}"/> field rather
		/// than a C# event, so assigning with <c>=</c> replaces any existing handler instead of
		/// adding to it.
		/// </para>
		/// </remarks>
		public static Action<NetIdentity> OnSessionRequest;

		/// <summary>
		/// Raised when a session with a peer fails &#8212; they went offline, no route could be
		/// negotiated, or an established session broke. The argument carries the connection details,
		/// including the end reason.
		/// </summary>
		/// <remarks>
		/// Not raised for sessions torn down and rebuilt by
		/// <see cref="SteamNetworkingOptions.AutoRestartBrokenSession"/>, which exists precisely to
		/// hide that. As with <see cref="OnSessionRequest"/>, this is a plain
		/// <see cref="Action{T}"/> field, so <c>=</c> replaces rather than adds.
		/// </remarks>
		public static Action<ConnectionInfo> OnSessionFailed;

		/// <summary>
		/// Allow a peer's messages to be delivered to you. Call this in response to
		/// <see cref="OnSessionRequest"/>, after deciding you actually want to hear from them.
		/// </summary>
		/// <param name="identity">
		/// The peer to accept. Passed by reference only because the underlying native call takes a
		/// pointer &#8212; it is not modified, but it does mean you need a local variable rather than
		/// an expression.
		/// </param>
		/// <returns>
		/// <see langword="true"/> if the session was accepted. Valve does not document the failure
		/// conditions.
		/// </returns>
		/// <remarks>
		/// You do not need to call this before <i>sending</i> to someone &#8212; sending implicitly
		/// opens a session from your side. It is only needed to receive.
		/// </remarks>
		public static bool AcceptSessionWithUser( ref NetIdentity identity ) => Internal.AcceptSessionWithUser( ref identity );

		/// <summary>
		/// Finish talking to a peer and free the resources backing the session immediately, rather
		/// than waiting for the few-minute idle timeout.
		/// </summary>
		/// <param name="identity">The peer to disconnect from.</param>
		/// <returns><see langword="true"/> if a session existed and was closed.</returns>
		/// <remarks>
		/// This is not a ban. Valve's header is explicit that if the peer sends to you again, a fresh
		/// <see cref="OnSessionRequest"/> is raised. Any messages still queued for them are lost.
		/// </remarks>
		public static bool CloseSessionWithUser( ref NetIdentity identity ) => Internal.CloseSessionWithUser( ref identity );

		/// <summary>
		/// Stop talking to a peer on one channel while leaving your other channels with them open.
		/// Useful when channels map to subsystems &#8212; closing the voice channel without dropping
		/// gameplay traffic.
		/// </summary>
		/// <param name="identity">The peer.</param>
		/// <param name="channel">The local channel number to close.</param>
		/// <returns><see langword="true"/> if the channel was closed.</returns>
		/// <remarks>
		/// Closing the last open channel to a peer closes the whole session, after which new data from
		/// them raises <see cref="OnSessionRequest"/> again.
		/// </remarks>
		public static bool CloseChannelWithUser( ref NetIdentity identity, int channel ) => Internal.CloseChannelWithUser( ref identity, channel );

		/// <summary>
		/// Inspect the state of the connection underlying a session &#8212; ping, route, quality, and
		/// why it failed if it did. Valve describes this as primarily a debugging aid, but it is also
		/// the only way to get a detailed failure reason.
		/// </summary>
		/// <param name="identity">The peer to ask about.</param>
		/// <param name="info">Receives the connection details, including the end reason on a failure.</param>
		/// <param name="status">Receives live quality figures &#8212; ping, queued bytes, estimated bandwidth.</param>
		/// <returns>
		/// The connection state, or <see cref="ConnectionState.None"/> if there is no session with
		/// this peer at all.
		/// </returns>
		/// <remarks>
		/// <b>Read it promptly.</b> Valve warns that sessions time out, so after a send returns
		/// <see cref="Result.NoConnection"/> you cannot wait indefinitely before asking why &#8212;
		/// the reason is discarded with the session. Unlike the native call, which allows null for
		/// either output, both parameters here are required.
		/// </remarks>
		public static ConnectionState GetSessionConnectionInfo( ref NetIdentity identity, ref ConnectionInfo info, ref ConnectionStatus status ) => Internal.GetSessionConnectionInfo( ref identity, ref info, ref status );

		/// <summary>
		/// Send a message to a peer, addressed by Steam identity. Opens a session with them
		/// implicitly if one is not already up.
		/// </summary>
		/// <param name="identity">Who to send to.</param>
		/// <param name="data">
		/// The payload. Pinned and passed to Steam without an intermediate copy, but Steam copies it
		/// before returning, so the array is yours again immediately afterwards.
		/// </param>
		/// <param name="flags">
		/// Delivery semantics &#8212; reliability, Nagle batching, and drop-if-slow. See
		/// <see cref="SteamNetworkingOptions"/>.
		/// </param>
		/// <param name="channel">
		/// The channel to send on. Channels are independent streams; the receiver must be draining
		/// this same number for the message to be seen. Valve documents no valid range.
		/// </param>
		/// <returns>
		/// <see cref="Result.OK"/> if the message was accepted for sending &#8212; which is not a
		/// delivery confirmation. <see cref="Result.Ignored"/> means it was deliberately dropped
		/// because of <see cref="SteamNetworkingOptions.NoDelay"/>, and
		/// <see cref="Result.NoConnection"/> means the session is broken.
		/// </returns>
		/// <remarks>
		/// A null <paramref name="data"/> throws <see cref="NullReferenceException"/> rather than
		/// being treated as an empty message.
		/// </remarks>
		public static unsafe Result SendMessageToUser( ref NetIdentity identity, byte[] data, SteamNetworkingOptions flags, int channel)
		{
			uint length = (uint)data.Length;
			fixed ( byte* p = data )
			{
				return Internal.SendMessageToUser( ref identity, (IntPtr)p, length, (int)flags, channel );
			}
		}

		/// <summary>
		/// Raw send — the caller owns the buffer. Zero managed allocation;
		/// pairs with pooled/pinned buffers or native memory.
		/// </summary>
		/// <param name="identity">Who to send to.</param>
		/// <param name="data">
		/// Pointer to the payload. <b>Must remain valid and pinned for the duration of this call.</b>
		/// A pointer into a managed array that has not been pinned may be moved by the GC mid-call.
		/// Steam copies the bytes before returning, so you may reuse the buffer immediately afterwards.
		/// </param>
		/// <param name="length">Payload length in bytes. Not validated against the buffer &#8212; an oversized value reads past the end.</param>
		/// <param name="flags">Delivery semantics. See <see cref="SteamNetworkingOptions"/>.</param>
		/// <param name="channel">The channel to send on. The receiver must be draining the same number.</param>
		/// <returns>
		/// <see cref="Result.OK"/> if accepted for sending, <see cref="Result.Ignored"/> if
		/// deliberately dropped under <see cref="SteamNetworkingOptions.NoDelay"/>,
		/// <see cref="Result.NoConnection"/> if the session is broken.
		/// </returns>
		public static Result SendMessageToUser( ref NetIdentity identity, IntPtr data, uint length, SteamNetworkingOptions flags, int channel )
		{
			return Internal.SendMessageToUser( ref identity, data, length, (int)flags, channel );
		}

#if NETSTANDARD2_1_OR_GREATER || NET
		/// <summary>
		/// Span send — zero managed allocation (slices of pooled arrays,
		/// stackalloc, etc.). Available on netstandard2.1+/.NET builds.
		/// This is the overload to reach for in a per-frame send loop.
		/// </summary>
		/// <param name="identity">Who to send to.</param>
		/// <param name="data">
		/// The payload. Pinned for the duration of the call, so slices of pooled arrays and
		/// <c>stackalloc</c> buffers are both fine. An empty span sends a zero-length message rather
		/// than nothing.
		/// </param>
		/// <param name="flags">Delivery semantics. See <see cref="SteamNetworkingOptions"/>.</param>
		/// <param name="channel">The channel to send on. The receiver must be draining the same number.</param>
		/// <returns>
		/// <see cref="Result.OK"/> if accepted for sending, <see cref="Result.Ignored"/> if
		/// deliberately dropped under <see cref="SteamNetworkingOptions.NoDelay"/>,
		/// <see cref="Result.NoConnection"/> if the session is broken.
		/// </returns>
		/// <remarks>
		/// Not compiled on the <c>net46</c> target, which lacks <see cref="ReadOnlySpan{T}"/>. Code
		/// that must build for Unity's older profile should use the <c>byte[]</c> or
		/// <see cref="IntPtr"/> overload.
		/// </remarks>
		public static unsafe Result SendMessageToUser( ref NetIdentity identity, ReadOnlySpan<byte> data, SteamNetworkingOptions flags, int channel )
		{
			fixed ( byte* p = data )
			{
				return Internal.SendMessageToUser( ref identity, (IntPtr)p, (uint)data.Length, (int)flags, channel );
			}
		}
#endif

		/// <summary>
		/// Zero-copy message delivery: the buffer belongs to Steam and is only
		/// valid for the duration of the callback — copy it if you keep it.
		/// Mirrors the sockets-layer <c>OnMessage( IntPtr, int, … )</c> contract.
		/// </summary>
		/// <param name="identity">Who sent the message. The full identity, not just a <see cref="SteamId"/>.</param>
		/// <param name="channel">The channel it arrived on.</param>
		/// <param name="data">
		/// Pointer to Steam's own buffer. <b>Only valid until this callback returns</b> &#8212; Steam
		/// releases it immediately afterwards. Copy anything you need to keep.
		/// </param>
		/// <param name="size">Payload length in bytes.</param>
		/// <remarks>
		/// Throwing from this callback is safe with respect to memory: the caller releases every
		/// message still held before rethrowing. The exception still propagates out of
		/// <see cref="ReceiveMessagesOnChannel(int, MessageIntercept, int, bool)"/>, abandoning any
		/// messages not yet fetched from Steam &#8212; they remain queued for the next call.
		/// </remarks>
		public delegate void MessageIntercept( NetIdentity identity, int channel, IntPtr data, int size );

		/// <summary>
		/// Drain pending messages on a channel, handing each to your callback as a fresh
		/// <c>byte[]</c>. Convenient, but it allocates one array per message &#8212; for a per-frame
		/// pump prefer the
		/// <see cref="ReceiveMessagesOnChannel(int, MessageIntercept, int, bool)"/> overload, which
		/// allocates nothing.
		/// </summary>
		/// <param name="channel">
		/// Which channel to drain. Channels are independent queues and this only touches one, so you
		/// must call this once per channel you use.
		/// </param>
		/// <param name="callback">
		/// Invoked once per message with (sender, channel, payload). The array is yours to keep. Note
		/// the sender arrives as a <see cref="SteamId"/> here, losing the rest of the identity.
		/// </param>
		/// <param name="bufferSize">
		/// How many messages to fetch per native call, between 1 and 256. This is a stack-allocated
		/// batch size, not a queue limit; larger values mean fewer native calls when busy.
		/// </param>
		/// <param name="receiveToEnd">
		/// When <see langword="true"/> (the default), keep fetching until the queue is empty rather
		/// than stopping after one batch. Leaving this on is what guarantees you do not fall behind.
		/// </param>
		/// <returns>The total number of messages processed across all batches.</returns>
		/// <exception cref="ArgumentOutOfRangeException">Thrown if <paramref name="bufferSize"/> is outside 1-256.</exception>
		/// <remarks>
		/// <para>
		/// If <paramref name="callback"/> throws, every message in the current batch is released
		/// before the exception propagates, so no native memory leaks &#8212; but the messages in that
		/// batch are lost.
		/// </para>
		/// <para>
		/// Messages only arrive from peers whose session you accepted via
		/// <see cref="OnSessionRequest"/> and <see cref="AcceptSessionWithUser"/>. A return of 0
		/// forever usually means that handler is missing rather than that the peer is not sending.
		/// </para>
		/// </remarks>
		public unsafe static int ReceiveMessagesOnChannel( int channel, Action<SteamId, int, byte[]> callback, int bufferSize = 32, bool receiveToEnd = true )
		{
			if ( bufferSize < 1 || bufferSize > 256 ) throw new ArgumentOutOfRangeException( nameof( bufferSize ) );

			int totalProcessed = 0;
			NetMsg** messageBuffer = stackalloc NetMsg*[bufferSize];

			while ( true )
			{
				int processed = Internal.ReceiveMessagesOnChannel(channel, new IntPtr( &messageBuffer[0] ), bufferSize );
				totalProcessed += processed;

				try
				{
					for ( int i = 0; i < processed; i++ )
					{
						ReceiveMessage( ref messageBuffer[i], callback );
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

		internal unsafe static void ReceiveMessage( ref NetMsg* msg, Action<SteamId, int, byte[]> callback )
		{
			try
			{
				byte[] data = new byte[msg->DataSize];
				Marshal.Copy( msg->DataPtr, data, 0, msg->DataSize );
				callback(msg->Identity.SteamId, msg->Channel, data);
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
		/// ZERO-ALLOCATION receive: drains up to <paramref name="bufferSize"/>
		/// messages per native call and hands each to
		/// <paramref name="onMessage"/> as a raw pointer + size. The buffer is
		/// Steam-owned and freed when the callback returns — copy it (into a
		/// pooled buffer) if it must outlive the call. Unlike the
		/// <c>byte[]</c> overload, this allocates NOTHING per message, which
		/// is what a per-frame network pump wants.
		/// </summary>
		/// <param name="channel">Which channel to drain. Call once per channel you use.</param>
		/// <param name="onMessage">
		/// Invoked once per message with a pointer into Steam's buffer, valid only for the duration of
		/// the call. Copy anything you need to keep.
		/// </param>
		/// <param name="bufferSize">
		/// How many messages to fetch per native call, between 1 and 256. A stack-allocated batch
		/// size, not a queue limit.
		/// </param>
		/// <param name="receiveToEnd">
		/// When <see langword="true"/> (the default), keep fetching until the queue is empty rather
		/// than stopping after one batch.
		/// </param>
		/// <returns>The total number of messages processed across all batches.</returns>
		/// <exception cref="ArgumentOutOfRangeException">Thrown if <paramref name="bufferSize"/> is outside 1-256.</exception>
		/// <remarks>
		/// If <paramref name="onMessage"/> throws, every message in the current batch is released
		/// before the exception propagates &#8212; no native memory is leaked, but that batch is lost.
		/// </remarks>
		public unsafe static int ReceiveMessagesOnChannel( int channel, MessageIntercept onMessage, int bufferSize = 32, bool receiveToEnd = true )
		{
			if ( bufferSize < 1 || bufferSize > 256 ) throw new ArgumentOutOfRangeException( nameof( bufferSize ) );

			int totalProcessed = 0;
			NetMsg** messageBuffer = stackalloc NetMsg*[bufferSize];

			while ( true )
			{
				int processed = Internal.ReceiveMessagesOnChannel( channel, new IntPtr( &messageBuffer[0] ), bufferSize );
				totalProcessed += processed;

				try
				{
					for ( int i = 0; i < processed; i++ )
					{
						ReceiveMessageIntercept( ref messageBuffer[i], onMessage );
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

				if ( !receiveToEnd || processed < bufferSize )
					break;
			}

			return totalProcessed;
		}

		internal unsafe static void ReceiveMessageIntercept( ref NetMsg* msg, MessageIntercept onMessage )
		{
			try
			{
				onMessage( msg->Identity, msg->Channel, msg->DataPtr, msg->DataSize );
			}
			finally
			{
				NetMsg.InternalRelease( msg );
				msg = null;
			}
		}
	}
}
