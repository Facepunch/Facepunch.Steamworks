using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Steamworks.Data;

namespace Steamworks
{
	/// <summary>
	/// Steam's original peer-to-peer transport: send a blob of bytes to a <see cref="SteamId"/> and Steam
	/// works out how to get it there, punching through NAT or falling back to its own relay servers.
	/// <para>
	/// <b>Valve has deprecated this entire interface.</b> The Steamworks SDK header carries the deprecation
	/// notice three separate times — once on the interface itself and once on each half of it — and states
	/// that it may be removed in a future SDK release. New code should not use this class. See the
	/// migration notes below.
	/// </para>
	/// </summary>
	/// <remarks>
	/// <para>
	/// <b>What to use instead.</b> Valve names two replacements and does not explain when to pick which, so:
	/// </para>
	/// <list type="bullet">
	/// <item><description><see cref="SteamNetworkingMessages"/> is the direct equivalent of the API in this
	/// class — send a message to a peer identity on a channel, with no connection object to manage. It is
	/// the smaller migration, and it keeps the same shape of code.</description></item>
	/// <item><description><see cref="SteamNetworkingSockets"/> is the connection-oriented API. You get an
	/// explicit connection handle with a real lifecycle, connection status callbacks, and poll groups for
	/// draining many peers with one call. It is the better destination for anything with a genuine
	/// session concept, such as a game server.</description></item>
	/// </list>
	/// <para>
	/// Both replacements supersede this class outright: they carry richer error reporting, real connection
	/// state, and — unlike the API here — do not force a per-packet allocation to read a message.
	/// </para>
	/// <para>
	/// <b>Session lifecycle.</b> There is no connect call. The first packet you send to a peer creates the
	/// session implicitly, and the first packet a stranger sends you raises
	/// <see cref="OnP2PSessionRequest"/>. You will receive nothing from them until you call
	/// <see cref="AcceptP2PSessionWithUser"/> — or until you send them something, which accepts implicitly.
	/// Ignoring the request is a valid way to refuse; Valve re-posts it periodically for as long as they
	/// keep trying. Call <see cref="CloseP2PSessionWithUser"/> when you are finished to release the
	/// resources; if they later send again, a fresh <see cref="OnP2PSessionRequest"/> is raised.
	/// </para>
	/// <para>
	/// <b>Receiving is polled, never pushed.</b> No callback delivers packet data. Steam queues incoming
	/// messages and you must drain them yourself with <see cref="IsP2PPacketAvailable(int)"/> and
	/// <see cref="ReadP2PPacket(int)"/>, once per channel you use, every tick. Each read returns exactly one
	/// message, so a single read per frame silently accumulates a backlog. Valve documents neither a queue
	/// depth limit nor what happens if you overrun it.
	/// </para>
	/// <para>
	/// <b>Callbacks still need pumping.</b> <see cref="OnP2PSessionRequest"/> and
	/// <see cref="OnP2PConnectionFailed"/> only fire while <see cref="SteamClient.RunCallbacks"/> (or
	/// <see cref="SteamServer.RunCallbacks"/> on a game server) is being called. Packet polling is
	/// independent of that, so a game that forgets to pump callbacks can find itself never accepting a
	/// session while wondering why no packets arrive.
	/// </para>
	/// </remarks>
	/// <example>
	/// A complete legacy P2P tick:
	/// <code>
	/// // Once, at startup
	/// SteamNetworking.OnP2PSessionRequest = id =&gt;
	/// {
	///     if ( Lobby.Contains( id ) )                     // never blanket-accept strangers
	///         SteamNetworking.AcceptP2PSessionWithUser( id );
	/// };
	///
	/// SteamNetworking.OnP2PConnectionFailed = ( id, error ) =&gt;
	/// {
	///     // Anything queued for this peer has already been dropped.
	///     Log( $"P2P to {id} failed: {error}" );
	/// };
	///
	/// // Every tick
	/// SteamClient.RunCallbacks();
	///
	/// while ( SteamNetworking.ReadP2PPacket( channel: 0 ) is P2Packet packet )
	///     HandleGameplay( packet.SteamId, packet.Data );
	///
	/// while ( SteamNetworking.ReadP2PPacket( channel: 1 ) is P2Packet voice )
	///     HandleVoice( voice.SteamId, voice.Data );
	///
	/// SteamNetworking.SendP2PPacket( peer, state, sendType: P2PSend.Unreliable, nChannel: 0 );
	///
	/// // When done with a peer
	/// SteamNetworking.CloseP2PSessionWithUser( peer );
	/// </code>
	/// </example>
	public class SteamNetworking : SteamSharedClass<SteamNetworking>
	{
		internal static ISteamNetworking Internal => Interface as ISteamNetworking;

		internal override bool InitializeInterface( bool server )
		{
			SetInterface( server, new ISteamNetworking( server ) );
			if ( Interface.Self == IntPtr.Zero ) return false;

			InstallEvents( server );

			return true;
		}

		internal static void InstallEvents( bool server )
		{
			Dispatch.Install<P2PSessionRequest_t>( x => OnP2PSessionRequest?.Invoke( x.SteamIDRemote ), server );
			Dispatch.Install<P2PSessionConnectFail_t>( x => OnP2PConnectionFailed?.Invoke( x.SteamIDRemote, (P2PSessionError) x.P2PSessionError ), server );
		}

		/// <summary>
		/// Invoked when a <see cref="SteamId"/> wants to send the current user a message. You should respond by calling <see cref="AcceptP2PSessionWithUser(SteamId)"/>
		/// if you want to recieve their messages.
		/// </summary>
		/// <remarks>
		/// <para>
		/// Raised for any Steam user who sends you a packet and whom you have not already talked to — it is
		/// not restricted to your lobby, your friends, or players you invited. Treat the
		/// <see cref="SteamId"/> as untrusted input and check it against your own list of expected peers
		/// before accepting; accepting unconditionally lets an arbitrary user open a session with your
		/// process.
		/// </para>
		/// <para>
		/// Ignoring the request is the documented way to decline. Valve re-posts it periodically while the
		/// sender keeps trying, so a single ignore is not permanent and your handler must tolerate seeing the
		/// same user repeatedly.
		/// </para>
		/// <para>
		/// This is a plain delegate field, not a C# event: assigning to it replaces any previous handler
		/// rather than adding to it. Only fires while callbacks are being pumped.
		/// </para>
		/// </remarks>
		public static Action<SteamId> OnP2PSessionRequest;

		/// <summary>
		/// Invoked when packets can't get through to the specified user.
		/// All queued packets unsent at this point will be dropped, further attempts
		/// to send will retry making the connection (but will be dropped if we fail again).
		/// </summary>
		/// <remarks>
		/// <para>
		/// The <see cref="P2PSessionError"/> you actually receive is narrower than the enum suggests. Only
		/// <see cref="P2PSessionError.NoRightsToApp"/> (the local user does not own the running app) and
		/// <see cref="P2PSessionError.Timeout"/> are still sent.
		/// <see cref="P2PSessionError.NotRunningApp_DELETED"/> and
		/// <see cref="P2PSessionError.DestinationNotLoggedIn_DELETED"/> were withdrawn by Valve and will
		/// never arrive: for privacy reasons Steam does not reply at all when the target is offline or
		/// playing something else, so those cases surface as an ordinary timeout.
		/// </para>
		/// <para>
		/// <see cref="P2PSessionError.Timeout"/> therefore conflates several very different situations: the
		/// peer is offline, the peer is in another game, the peer never called
		/// <see cref="AcceptP2PSessionWithUser"/>, or the network is blocking you. Valve notes that
		/// corporate firewalls cause this too, and that UDP ports 3478, 4379 and 4380 must be open
		/// outbound — NAT traversal is not firewall traversal.
		/// </para>
		/// <para>
		/// This is a plain delegate field, not a C# event: assigning replaces the previous handler. Only
		/// fires while callbacks are being pumped.
		/// </para>
		/// </remarks>
		public static Action<SteamId, P2PSessionError> OnP2PConnectionFailed;

		/// <summary>
		/// This should be called in response to a <see cref="OnP2PSessionRequest"/>. Until you do, packets
		/// from that user are not delivered to you.
		/// </summary>
		/// <param name="user">
		/// The peer to start accepting packets from — normally the <see cref="SteamId"/> handed to your
		/// <see cref="OnP2PSessionRequest"/> handler. Validate it against your own list of expected players
		/// first; nothing in Steam restricts who may open a session with you.
		/// </param>
		/// <returns>
		/// Valve does not document what the return value means for this call, and gives no list of failure
		/// conditions. Do not treat <see langword="true"/> as proof that a working route to the peer exists —
		/// a session that cannot actually be established surfaces later as
		/// <see cref="OnP2PConnectionFailed"/>, not here.
		/// </returns>
		/// <remarks>
		/// Safe to call more than once for the same user; Valve explicitly allows it. Sending a packet to a
		/// user with <c>SendP2PPacket</c> accepts their session implicitly, so a peer you are already
		/// talking to needs no explicit accept.
		/// </remarks>
		public static bool AcceptP2PSessionWithUser( SteamId user ) => Internal.AcceptP2PSessionWithUser( user );

		/// <summary>
		/// Allow or disallow P2P connects to fall back on Steam server relay if direct 
		/// connection or NAT traversal can't be established. Applies to connections 
		/// created after setting or old connections that need to reconnect.
		/// </summary>
		/// <param name="allow">
		/// <see langword="true"/> to permit relaying, <see langword="false"/> to request direct connections
		/// only. Relay is <b>already allowed by default</b>, so calling this with <see langword="true"/>
		/// changes nothing.
		/// <para>
		/// Passing <see langword="false"/> is a request, not a guarantee: Valve states that Steam may still
		/// relay traffic to certain peers regardless, to avoid revealing a client's IP address to another
		/// peer. Do not build anything on the assumption that a false here means the peer sees your real
		/// address, or that it does not.
		/// </para>
		/// </param>
		/// <returns>
		/// Valve documents no meaning for this return value and no failure conditions.
		/// </returns>
		/// <remarks>
		/// Deprecated even within this already-deprecated interface, and it applies only to connections
		/// created after the call, plus existing connections that happen to reconnect afterwards. Set it once
		/// during startup, before any session exists — flipping it mid-session leaves you with a mix of
		/// connections under different policies and no way to inspect which is which, since this binding does
		/// not expose the SDK's <c>GetP2PSessionState</c>.
		/// </remarks>
		public static bool AllowP2PPacketRelay( bool allow ) => Internal.AllowP2PPacketRelay( allow );

		/// <summary>
		/// This should be called when you're done communicating with a user, as this will
		/// free up all of the resources allocated for the connection under-the-hood.
		/// If the remote user tries to send data to you again, a new <see cref="OnP2PSessionRequest"/> 
		/// callback will be posted
		/// </summary>
		/// <param name="user">The peer to tear the session down with.</param>
		/// <returns>
		/// Valve documents no meaning for this return value.
		/// </returns>
		/// <remarks>
		/// <para>
		/// This closes the session across <b>every channel</b> at once. The SDK also has a per-channel
		/// <c>CloseP2PChannelWithUser</c>, which closes the whole session once its last open channel goes
		/// away — but this binding does not expose it, so a multi-channel peer can only be closed wholesale.
		/// </para>
		/// <para>
		/// Valve does not document what becomes of packets from this peer that are already queued and unread
		/// at the moment you close. If you care about draining them, read the queue empty on every channel
		/// before calling this.
		/// </para>
		/// </remarks>
		public static bool CloseP2PSessionWithUser( SteamId user ) => Internal.CloseP2PSessionWithUser( user );

		/// <summary>
		/// Checks if a P2P packet is available to read. Use this when you only want to know <i>whether</i> to
		/// do work; if you are about to read anyway, <see cref="ReadP2PPacket(int)"/> already performs this
		/// check internally, so calling both is a wasted native call.
		/// </summary>
		/// <param name="channel">
		/// Which channel to check. Channels are independent queues: a packet waiting on channel 1 is
		/// invisible to a check on channel 0. Must match the channel the sender used.
		/// </param>
		/// <returns>
		/// <see langword="true"/> if at least one message is queued on that channel. Never blocks.
		/// </returns>
		public static bool IsP2PPacketAvailable( int channel = 0 )
		{
			uint _ = 0;
			return Internal.IsP2PPacketAvailable( ref _, channel );
		}
		
		/// <summary>
		/// Checks if a P2P packet is available to read, and gets the size of the message if there is one. This
		/// is how you size a buffer before calling the <c>ReadP2PPacket</c> overloads that take one, which
		/// truncate silently if you guess too small.
		/// </summary>
		/// <param name="msgSize">
		/// Receives the exact byte length of the <b>next</b> message on that channel — not the total queued.
		/// Set to 0 when the method returns <see langword="false"/>.
		/// </param>
		/// <param name="channel">
		/// Which channel's queue to inspect. Must match the channel the sender used.
		/// </param>
		/// <returns>
		/// <see langword="true"/> if a message is waiting, in which case <paramref name="msgSize"/> is
		/// meaningful. Never blocks.
		/// </returns>
		/// <remarks>
		/// The size reported here is only guaranteed to describe the message at the head of the queue at the
		/// moment of the call. Read on the same thread immediately afterwards; another thread reading the
		/// same channel in between will leave you sizing a buffer for a message you no longer get.
		/// </remarks>
		public static bool IsP2PPacketAvailable( out uint msgSize, int channel = 0 )
		{
			msgSize = 0;
			return Internal.IsP2PPacketAvailable( ref msgSize, channel );
		}

		/// <summary>
		/// Reads in a packet that has been sent from another user via <c>SendP2PPacket</c>, as a managed
		/// <see cref="P2Packet"/> you own outright. This is the convenient overload; the ones taking a buffer
		/// are the ones that do not allocate.
		/// </summary>
		/// <param name="channel">
		/// Which channel to read from. Channels are separate queues and must match the channel the sender
		/// used — reading channel 0 will never return a packet sent on channel 1.
		/// </param>
		/// <returns>
		/// A packet whose <c>Data</c> is a freshly allocated array sized exactly to the message, or
		/// <see langword="null"/> if there was nothing to read.
		/// <para>
		/// <b>Ownership:</b> the array belongs to you. Nothing is pooled or rented from your point of view,
		/// there is nothing to dispose or return, and the contents remain valid indefinitely — keep it, queue
		/// it, or hand it to another thread freely. Internally this reads into a shared scratch buffer and
		/// copies out of it before returning, so the array you get back is never aliased by a later read.
		/// </para>
		/// <para>
		/// <b>A <see langword="null"/> is not conclusively "queue empty".</b> This binding also returns
		/// <see langword="null"/> when the underlying read fails and when the message length is zero, and
		/// those cases are not distinguishable from an empty queue. In the zero-length case the message has
		/// already been consumed from Steam's queue, so a <c>while ( ... is P2Packet p )</c> drain loop would
		/// stop early with packets still waiting behind it.
		/// </para>
		/// </returns>
		/// <remarks>
		/// <para>
		/// Allocates one array per packet. At high message rates that is real GC pressure — use the
		/// <c>ReadP2PPacket( byte[], ref uint, ref SteamId, int )</c> overload with a buffer you reuse
		/// instead.
		/// </para>
		/// <para>
		/// The internal scratch buffer is shared process-wide and is not per-call, so this overload is not
		/// safe to call concurrently from several threads. Poll each channel from one thread.
		/// </para>
		/// </remarks>
		public unsafe static P2Packet? ReadP2PPacket( int channel = 0 )
		{
			uint size = 0;

			if ( !Internal.IsP2PPacketAvailable( ref size, channel ) )
				return null;

			var buffer = Helpers.TakeBuffer( (int) size );

			fixed ( byte* p = buffer )
			{
				SteamId steamid = 1;
				if ( !Internal.ReadP2PPacket( (IntPtr)p, (uint) buffer.Length, ref size, ref steamid, channel ) || size == 0 )
				    return null;

				var data = new byte[size];
				Array.Copy( buffer, 0, data, 0, size );

				return new P2Packet
				{
					SteamId = steamid,
					Data = data
				};
			}
		}

		/// <summary>
		/// Reads in a packet that has been sent from another user via <c>SendP2PPacket</c> straight into a
		/// buffer you own, allocating nothing. This is the overload to use in a hot receive loop.
		/// </summary>
		/// <param name="buffer">
		/// Destination for the payload. You allocate it, you own it, and it is yours to reuse across calls —
		/// this binding neither pools nor retains it. Only the first <paramref name="size"/> bytes are
		/// written; anything beyond that is left holding whatever was there before, so never read past the
		/// reported size.
		/// <para>
		/// <b>Too small means silent truncation.</b> Valve truncates the message to fit and still reports
		/// success — there is no error and no way to detect the loss afterwards. Size the buffer from
		/// <see cref="IsP2PPacketAvailable(out uint, int)"/>, or make it large enough for your largest
		/// possible message (1200 bytes for the unreliable send types, up to 1&#160;MB for the reliable ones).
		/// </para>
		/// </param>
		/// <param name="size">
		/// Receives the number of bytes actually written. Declared <see langword="ref"/> only because of how
		/// the native call is marshalled — it behaves as an output, and whatever you pass in is discarded.
		/// </param>
		/// <param name="steamid">
		/// Receives the peer that sent the message. Also an output despite the <see langword="ref"/>.
		/// </param>
		/// <param name="channel">
		/// Which channel to read from. Must match the channel the sender used.
		/// </param>
		/// <returns>
		/// <see langword="true"/> if a message was read, in which case <paramref name="size"/> and
		/// <paramref name="steamid"/> are meaningful. <see langword="false"/> means no data was available;
		/// the call never blocks. Note that <see langword="true"/> does not rule out truncation.
		/// </returns>
		/// <remarks>
		/// Unlike <see cref="ReadP2PPacket(int)"/>, this does not check availability first — it goes straight
		/// to the native read and lets it report emptiness. Loop until it returns <see langword="false"/> to
		/// drain the channel.
		/// </remarks>
		public unsafe static bool ReadP2PPacket( byte[] buffer, ref uint size, ref SteamId steamid, int channel = 0 )
		{
			fixed (byte* p = buffer) {
				return Internal.ReadP2PPacket( (IntPtr)p, (uint)buffer.Length, ref size, ref steamid, channel );
			}
		}

		/// <summary>
		/// Reads in a packet that has been sent from another user via <c>SendP2PPacket</c> into raw
		/// unmanaged memory. Use this when the destination is native memory, a pinned region you already
		/// hold, or a slice of a larger arena.
		/// </summary>
		/// <param name="buffer">
		/// Pointer to at least <paramref name="cbuf"/> writable bytes. Lifetime and pinning are entirely
		/// your responsibility — if this points into a managed array, it must stay pinned for the duration
		/// of the call. Nothing here is pooled or retained by the binding.
		/// </param>
		/// <param name="cbuf">
		/// Capacity of <paramref name="buffer"/> in bytes. <b>Nothing validates this against the real
		/// allocation:</b> overstating it lets native code write past the end of your memory. Understating it
		/// silently truncates the message, exactly as with the <see langword="byte"/> array overload.
		/// </param>
		/// <param name="size">
		/// Receives the number of bytes written. An output in practice, despite the <see langword="ref"/>.
		/// </param>
		/// <param name="steamid">
		/// Receives the peer that sent the message. Also an output.
		/// </param>
		/// <param name="channel">
		/// Which channel to read from. Must match the channel the sender used.
		/// </param>
		/// <returns>
		/// <see langword="true"/> if a message was read; <see langword="false"/> if the channel had nothing
		/// queued. Never blocks, and never reports truncation.
		/// </returns>
		public unsafe static bool ReadP2PPacket( byte* buffer, uint cbuf, ref uint size, ref SteamId steamid, int channel = 0 )
		{
			return Internal.ReadP2PPacket( (IntPtr)buffer, cbuf, ref size, ref steamid, channel );
		}

		/// <summary>
		/// Sends a P2P packet to the specified user.
		/// This is a session-less API which automatically establishes NAT-traversing or Steam relay server connections.
		/// NOTE: The first packet send may be delayed as the NAT-traversal code runs.
		/// </summary>
		/// <param name="steamid">
		/// The peer to send to. Sending to someone who has an inbound session request pending accepts that
		/// request implicitly, so you do not need <see cref="AcceptP2PSessionWithUser"/> as well.
		/// </param>
		/// <param name="data">The payload. Not retained — it is copied into Steam before this returns.</param>
		/// <param name="length">
		/// How many bytes of <paramref name="data"/> to send. Any value of zero or below is treated as "the
		/// whole array", which is why the default is <c>-1</c>. Two consequences worth knowing: you cannot
		/// send a zero-length message through this overload, and nothing checks the value against
		/// <c>data.Length</c>, so a length larger than the array reads past the end of it.
		/// </param>
		/// <param name="nChannel">
		/// A routing number of your choosing — Steam does not interpret it. The receiver must call
		/// <see cref="ReadP2PPacket(int)"/> with the same number or the message is never seen. Using several
		/// channels with one peer is cheap: they share a single underlying P2P connection.
		/// </param>
		/// <param name="sendType">
		/// How to deliver it, and the choice that sets your size limit:
		/// <list type="bullet">
		/// <item><description><see cref="P2PSend.Unreliable"/> — UDP-like. Max 1200 bytes. May be lost or
		/// (rarely) reordered. Steam still batches it if NAT traversal is mid-flight.</description></item>
		/// <item><description><see cref="P2PSend.UnreliableNoDelay"/> — as above, but discarded outright if
		/// the connection is not up yet. Valve notes that using this for your first packet to a peer almost
		/// guarantees it is dropped. Intended for data that must never buffer, such as voice.</description></item>
		/// <item><description><see cref="P2PSend.Reliable"/> — up to 1&#160;MB per message, fragmented and
		/// reassembled for you. The default.</description></item>
		/// <item><description><see cref="P2PSend.ReliableWithBuffering"/> — reliable, but Nagle-buffered:
		/// held until roughly an MTU has accumulated or about 200&#160;ms has passed. Batch small messages
		/// with this, then force the flush with a plain <see cref="P2PSend.Reliable"/> send.</description></item>
		/// </list>
		/// </param>
		/// <returns>
		/// <see langword="true"/> means Steam accepted the message for sending — <b>not</b> that it arrived,
		/// or that a route to the peer even exists. Delivery failure is reported asynchronously and much
		/// later through <see cref="OnP2PConnectionFailed"/>, at which point everything still queued for that
		/// peer has already been dropped.
		/// </returns>
		public static unsafe bool SendP2PPacket( SteamId steamid, byte[] data, int length = -1, int nChannel = 0, P2PSend sendType = P2PSend.Reliable )
		{
			if ( length <= 0 )
				length = data.Length;

			fixed ( byte* p = data )
			{
				return Internal.SendP2PPacket( steamid, (IntPtr)p, (uint)length, (P2PSend)sendType, nChannel );
			}
		}

		/// <summary>
		/// Sends a P2P packet to the specified user.
		/// This is a session-less API which automatically establishes NAT-traversing or Steam relay server connections.
		/// NOTE: The first packet send may be delayed as the NAT-traversal code runs.
		/// <para>
		/// The unmanaged-pointer form, for sending out of native memory or a pinned region without a managed
		/// array.
		/// </para>
		/// </summary>
		/// <param name="steamid">
		/// The peer to send to. Sending accepts a pending inbound session request implicitly.
		/// </param>
		/// <param name="data">
		/// Pointer to at least <paramref name="length"/> readable bytes. It only has to stay valid for the
		/// duration of the call — Steam copies the payload before returning — but if it points into a managed
		/// array, that array must be pinned across the call.
		/// </param>
		/// <param name="length">
		/// Bytes to send, starting at <paramref name="data"/>. Unlike the managed-array overload there is no
		/// "whole buffer" sentinel here; the value is passed through verbatim and nothing validates it, so an
		/// overstated length reads past the end of your memory.
		/// </param>
		/// <param name="nChannel">
		/// The routing channel the receiver must read from. The receiver has to poll this same
		/// channel — <see cref="ReadP2PPacket(int)"/> and <see cref="IsP2PPacketAvailable(int)"/>
		/// both take one — or the message is queued and never seen.
		/// <para>
		/// This defaulted to <c>1</c> until recently, while the managed-array overload and every
		/// receive path defaulted to <c>0</c>. Omitting it therefore sent on a channel a
		/// default-argument receive loop never polls, and the packet silently vanished. It now
		/// defaults to <c>0</c> like everything else. If you were compensating by passing
		/// <c>1</c> explicitly on the send side only, remove that.
		/// </para>
		/// </param>
		/// <param name="sendType">
		/// Delivery mode and, with it, the size ceiling: 1200 bytes for
		/// <see cref="P2PSend.Unreliable"/> and <see cref="P2PSend.UnreliableNoDelay"/>, up to 1&#160;MB for
		/// <see cref="P2PSend.Reliable"/> and <see cref="P2PSend.ReliableWithBuffering"/>. See the
		/// managed-array overload for the full description of each.
		/// </param>
		/// <returns>
		/// <see langword="true"/> means queued for sending, not delivered. Failures surface later via
		/// <see cref="OnP2PConnectionFailed"/>.
		/// </returns>
		public static unsafe bool SendP2PPacket( SteamId steamid, byte* data, uint length, int nChannel = 0, P2PSend sendType = P2PSend.Reliable )
		{ 
			return Internal.SendP2PPacket( steamid, (IntPtr)data, (uint)length, (P2PSend)sendType, nChannel );
		}

	}
}
