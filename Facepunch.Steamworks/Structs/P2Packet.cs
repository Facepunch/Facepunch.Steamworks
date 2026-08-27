namespace Steamworks.Data
{
	/// <summary>
	/// One complete datagram received from a peer over the legacy Steam P2P transport, together with the
	/// identity of the peer that sent it. Produced only by <see cref="SteamNetworking.ReadP2PPacket(int)"/>.
	/// <para>
	/// The transport is message-oriented rather than stream-oriented: one <c>SendP2PPacket</c> on the sending
	/// machine surfaces as one <see cref="P2Packet"/> here, carrying the whole payload. You do not have to
	/// reassemble or length-prefix anything yourself.
	/// </para>
	/// </summary>
	/// <remarks>
	/// <para>
	/// <b>Ownership — you own <see cref="P2Packet.Data"/> outright.</b> It is a fresh array allocated for this one
	/// packet. Nothing is pooled or rented, there is nothing to dispose or hand back, and the contents stay
	/// valid for as long as you keep the reference. Queueing it, passing it to another thread, or holding it
	/// across frames are all safe. Internally the binding does read into a shared scratch buffer first and
	/// copies out of it before returning, but that buffer is never exposed to you.
	/// </para>
	/// <para>
	/// <b>Cost.</b> Because every packet allocates, a high receive rate becomes GC pressure. If that matters,
	/// bypass this type and use the <c>SteamNetworking.ReadP2PPacket( byte[], ref uint, ref SteamId, int )</c>
	/// overload, which reads directly into a buffer you allocate once and reuse.
	/// </para>
	/// <para>
	/// <b>Deprecated transport.</b> Valve has deprecated the entire legacy P2P API this type belongs to. See
	/// <see cref="SteamNetworking"/> for the replacement APIs.
	/// </para>
	/// </remarks>
	/// <example>
	/// Draining one channel per tick:
	/// <code>
	/// while ( SteamNetworking.ReadP2PPacket() is P2Packet packet )
	/// {
	///     // No copy needed — packet.Data is already ours to keep.
	///     Inbox.Enqueue( packet );
	/// }
	/// </code>
	/// </example>
	public struct P2Packet
	{
		/// <summary>
		/// The peer that sent this packet, as reported by Steam. Pass this back to <c>SendP2PPacket</c> to
		/// reply, or to <c>CloseP2PSessionWithUser</c> when you are finished talking to them.
		/// </summary>
		public SteamId SteamId;
		/// <summary>
		/// The payload, byte for byte as it was handed to <c>SendP2PPacket</c> on the sending machine.
		/// <c>Length</c> is the real message length — this is never an oversized pooled buffer — so there is
		/// no separate byte count to carry alongside it.
		/// <para>
		/// Size depends on how the sender sent it: <see cref="P2PSend.Unreliable"/> and
		/// <see cref="P2PSend.UnreliableNoDelay"/> are capped by Valve at 1200 bytes (typical MTU), while the
		/// reliable send types fragment and reassemble transparently and allow up to 1&#160;MB per message.
		/// </para>
		/// </summary>
		public byte[] Data;
	}
}