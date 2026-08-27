using System;

namespace Steamworks.Data
{
	/// <summary>
	/// The opaque <b>routing blob</b> that tells Valve's relays how to reach a dedicated
	/// server running inside one of their data centers, together with the
	/// <see cref="PopId"/> of that data center. This is Valve's
	/// <c>SteamDatagramHostedAddress</c>.
	///
	/// <para>
	/// <b>What it is for.</b> An SDR-hosted server never tells clients its real IP. Instead
	/// the server asks Steam for this blob, ships it to <i>your own backend</i>, and your
	/// backend bakes it into the connection tickets it issues to players. When a player
	/// connects, the relays read the blob to work out where to forward the traffic. The
	/// player never learns the server's address, which is the entire point: the server
	/// cannot be DDoSed by someone who joined the game.
	/// </para>
	///
	/// <para>
	/// <b>Treat the bytes as secret-ish.</b> Valve is explicit: "The returned blob is not
	/// encrypted. Send it to your backend, but don't directly share it with clients." If you
	/// leak it to players you have thrown away the protection you turned SDR on for.
	/// </para>
	///
	/// <para>
	/// <b>Prefer the login flow.</b> Valve recommends
	/// <see cref="SteamNetworkingSockets.GetGameCoordinatorServerLogin"/> over fetching this
	/// on its own, because the routing information is included in the login blob anyway and
	/// that call additionally authenticates the server to your backend. Use this type when
	/// you only want the routing data, or when you are inspecting/logging it.
	/// </para>
	///
	/// <para>
	/// <b>Failures carry a message.</b> When <see cref="Result"/> is not
	/// <see cref="Steamworks.Result.OK"/>, Steam writes a plain-text English diagnostic into
	/// the same buffer, which is surfaced here as <see cref="DiagnosticMessage"/>. That
	/// string is frequently the fastest way to work out why a hosted server will not come up,
	/// and there is nowhere else to read it from.
	/// </para>
	/// </summary>
	/// <example>
	/// <code>
	/// var routing = SteamNetworkingSockets.GetHostedDedicatedServerAddress();
	///
	/// if ( routing.Result != Result.OK )
	/// {
	///     // e.g. Result.InvalidState  -> SDR_LISTEN_PORT is not set, we are not SDR-hosted
	///     //      Result.Pending       -> Steam has not finished fetching our credentials yet
	///     Log.Error( $"No SDR routing: {routing.Result} - {routing.DiagnosticMessage}" );
	///     return;
	/// }
	///
	/// Log.Info( $"Hosted in {routing.PopId}, routing blob is {routing.Length} bytes" );
	/// await Backend.RegisterServerAsync( routing.ToArray() );   // NEVER send this to players
	/// </code>
	/// </example>
	public readonly struct HostedServerAddress
	{
		//
		// The raw 128-byte m_data buffer exactly as Steam filled it in, plus m_cbSize.
		// We keep the whole buffer rather than a trimmed copy because on failure Steam
		// reuses it for the diagnostic string, and m_cbSize is then meaningless.
		//
		internal readonly byte[] data;
		internal readonly int size;

		/// <summary>
		/// What Steam said when this address was fetched.
		///
		/// <list type="bullet">
		/// <item><description><see cref="Steamworks.Result.OK"/> — the routing blob is valid.</description></item>
		/// <item><description><see cref="Steamworks.Result.InvalidState"/> — this process is not configured to listen for SDR. In practice: the <c>SDR_LISTEN_PORT</c> environment variable is not set.</description></item>
		/// <item><description><see cref="Steamworks.Result.Pending"/> — Steam does not have the authentication information yet. Retry shortly, or pre-fetch the network configuration via environment variables so it is always available immediately.</description></item>
		/// </list>
		/// </summary>
		public readonly Result Result;

		/// <summary>
		/// Which Valve data center this server is running in. <see cref="NetPOPID.Dev"/> in any
		/// non-production environment. Only meaningful when <see cref="IsValid"/>.
		/// </summary>
		public readonly NetPOPID PopId;

		internal HostedServerAddress( ref SteamDatagramHostedAddress routing, Result result )
		{
			Result = result;
			data = routing.Data;
			size = routing.CbSize;

			//
			// GetPopID parses the routing blob, so it is only meaningful once we know Steam
			// actually produced one. On failure the buffer holds a diagnostic string instead.
			//
			PopId = result == Steamworks.Result.OK
				? new NetPOPID( SteamDatagramHostedAddress.InternalGetPopID( ref routing ).Value )
				: default;
		}

		/// <summary>
		/// <see langword="true"/> when this holds a usable routing blob.
		/// </summary>
		public bool IsValid => Result == Steamworks.Result.OK && data != null && size > 0 && size <= data.Length;

		/// <summary>
		/// Length in bytes of the routing blob, or 0 if this is not <see cref="IsValid"/>.
		/// Steam's buffer is 128 bytes; the blob itself is normally much shorter.
		/// </summary>
		public int Length => IsValid ? size : 0;

		/// <summary>
		/// Steam's non-localized English diagnostic text, populated when <see cref="Result"/>
		/// is not <see cref="Steamworks.Result.OK"/>.
		///
		/// <para>
		/// It is meant for your logs, not for players. On success this returns
		/// <see langword="null"/> — the buffer then holds binary routing data, not text.
		/// </para>
		/// </summary>
		public string DiagnosticMessage
		{
			get
			{
				if ( Result == Steamworks.Result.OK || data == null )
					return null;

				var end = Array.IndexOf<byte>( data, 0 );
				if ( end < 0 ) end = data.Length;
				if ( end == 0 ) return string.Empty;

				return Utility.Utf8NoBom.GetString( data, 0, end );
			}
		}

		/// <summary>
		/// Copy the routing blob into a buffer you own.
		///
		/// <para>
		/// Use this instead of <see cref="ToArray"/> if you are re-registering with your
		/// backend periodically and want to reuse one buffer. Blobs are small (well under
		/// 128 bytes), so this is rarely worth the trouble.
		/// </para>
		/// </summary>
		/// <param name="destination">The buffer to copy into.</param>
		/// <param name="offset">Where in <paramref name="destination"/> to start writing.</param>
		/// <returns>The number of bytes written, which is <see cref="Length"/>.</returns>
		/// <exception cref="InvalidOperationException">This address is not <see cref="IsValid"/>.</exception>
		/// <exception cref="ArgumentNullException"><paramref name="destination"/> is <see langword="null"/>.</exception>
		/// <exception cref="ArgumentOutOfRangeException"><paramref name="offset"/> is negative.</exception>
		/// <exception cref="ArgumentException"><paramref name="destination"/> is too small.</exception>
		public int CopyTo( byte[] destination, int offset = 0 )
		{
			if ( !IsValid )
				throw new InvalidOperationException( InvalidMessage() );
			if ( destination == null )
				throw new ArgumentNullException( nameof( destination ) );
			if ( offset < 0 )
				throw new ArgumentOutOfRangeException( nameof( offset ) );
			if ( destination.Length - offset < size )
				throw new ArgumentException( $"Destination is too small - need {size} bytes at offset {offset}, have {destination.Length - offset}", nameof( destination ) );

			Buffer.BlockCopy( data, 0, destination, offset, size );
			return size;
		}

		/// <summary>
		/// The routing blob as a freshly allocated array, ready to be posted to your backend.
		///
		/// <para>
		/// Allocates. That is fine here: a server fetches its routing address a handful of
		/// times over its whole lifetime, not per frame.
		/// </para>
		/// </summary>
		/// <exception cref="InvalidOperationException">This address is not <see cref="IsValid"/>.</exception>
		public byte[] ToArray()
		{
			if ( !IsValid )
				throw new InvalidOperationException( InvalidMessage() );

			var copy = new byte[size];
			Buffer.BlockCopy( data, 0, copy, 0, size );
			return copy;
		}

		public override string ToString()
		{
			if ( !IsValid )
				return $"HostedServerAddress( {InvalidMessage()} )";

			return $"HostedServerAddress( pop={PopId}, {size} bytes )";
		}

		private string InvalidMessage()
		{
			if ( data == null )
				return "no hosted server address has been fetched";

			var diagnostic = DiagnosticMessage;

			return string.IsNullOrEmpty( diagnostic )
				? $"{Result}"
				: $"{Result}: {diagnostic}";
		}
	}
}
