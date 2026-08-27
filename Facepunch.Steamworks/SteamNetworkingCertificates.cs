using System;
using Steamworks.Data;

namespace Steamworks
{
	/// <summary>
	/// Application-provided certificates for Steam Datagram Relay, and resetting the networking
	/// identity. This is how you run SDR on a machine that has no Steam login.
	/// </summary>
	/// <remarks>
	/// <para>
	/// <b>You almost certainly do not need this.</b> When your process is signed in to Steam — an
	/// ordinary game client, or a game server that called <c>SteamGameServer_Init</c> and logged in
	/// — Steam fetches and renews an SDR certificate for you automatically, and nothing in this
	/// class needs to be touched. Valve files these under "advanced functions" for exactly that
	/// reason.
	/// </para>
	/// <para>
	/// <b>When you do need it.</b> SDR connections are authenticated. Each end proves who it is
	/// with a certificate signed by Valve for your app. If a process cannot get one from Steam —
	/// an anonymous dedicated server in a data center with no Steam account, a build on a platform
	/// where the player signs in to something other than Steam — then you have to obtain the
	/// certificate out of band and hand it to the networking library yourself. That is what these
	/// three functions are for.
	/// </para>
	///
	/// <para><b>The end-to-end workflow</b></para>
	/// <list type="number">
	/// <item><description>
	/// <b>On the game instance:</b> call <see cref="TryGetCertificateRequest(out byte[], out string)"/>.
	/// You get back a small opaque blob (Valve's conservative estimate is 512 bytes) that contains
	/// a freshly generated public key plus the identity being requested. The matching private key
	/// stays inside the networking library and never leaves the process.
	/// </description></item>
	/// <item><description>
	/// <b>Send that blob to your own backend</b> — Valve calls it the "game coordinator" — over
	/// whatever channel you already have (HTTPS, your login service, anything). The blob is not
	/// secret, but the channel should be authenticated, because whoever you sign a certificate for
	/// gets to claim that identity on your network.
	/// </description></item>
	/// <item><description>
	/// <b>On the backend:</b> call <c>SteamDatagram_CreateCert</c>. This is <b>not</b> part of
	/// <c>steam_api</c> and therefore not part of this binding — it lives in Valve's separate game
	/// coordinator library (<c>steamdatagram_gamecoordinator.h</c>), which you get with the Steam
	/// Datagram SDK. It needs your app's SDR signing private key, which you generate and register
	/// on the Steamworks partner site. Treat that key like any other production signing key: it
	/// belongs in a secret store on a server you control, never in the game build.
	/// </description></item>
	/// <item><description>
	/// <b>Send the signed certificate back</b> to the game instance.
	/// </description></item>
	/// <item><description>
	/// <b>On the game instance:</b> call <see cref="TrySetCertificate(byte[], out string)"/> with
	/// that blob, <i>before</i> you create listen sockets or connections. From here on SDR behaves
	/// exactly as it does for a Steam-authenticated process.
	/// </description></item>
	/// </list>
	///
	/// <para><b>Gotchas</b></para>
	/// <list type="bullet">
	/// <item><description>
	/// Do this during startup, before you open sockets. A connection attempted without a usable
	/// certificate fails authentication rather than waiting for one.
	/// </description></item>
	/// <item><description>
	/// Both calls report failure through an out <c>error</c> string sourced from Steam's own
	/// <c>SteamNetworkingErrMsg</c>. It is the only diagnostic you get — log it. It is
	/// <see langword="null"/> on success, so the success path allocates nothing.
	/// </description></item>
	/// <item><description>
	/// Certificates are not permanent. Valve does not document a lifetime in the public header, so
	/// build the renewal path in from the start: your process must be able to request and install a
	/// new certificate while running, not only at launch. <i>(That certificates expire at all is
	/// inferred from their being signed credentials — the header does not say so.)</i>
	/// </description></item>
	/// <item><description>
	/// <see cref="ResetIdentity(ref NetIdentity)"/> closes every open connection and discards the
	/// current certificate. Valve's header states plainly that it "is not actually supported on
	/// Steam" — it exists for platforms where one user can sign out and another sign in.
	/// </description></item>
	/// </list>
	/// </remarks>
	/// <example>
	/// <code>
	/// // Dedicated server with no Steam login, at startup.
	/// if ( !SteamNetworkingCertificates.TryGetCertificateRequest( out var request, out var error ) )
	///     throw new Exception( $"Could not build an SDR certificate request: {error}" );
	///
	/// // Your own backend, which holds the SDR signing key and calls SteamDatagram_CreateCert.
	/// byte[] signed = await gameCoordinator.SignSdrCertificateAsync( request );
	///
	/// if ( !SteamNetworkingCertificates.TrySetCertificate( signed, out error ) )
	///     throw new Exception( $"Steam rejected the SDR certificate: {error}" );
	///
	/// // Only now open the listen socket.
	/// var socket = SteamNetworkingSockets.CreateRelaySocket&lt;SocketManager&gt;();
	/// </code>
	/// </example>
	public static class SteamNetworkingCertificates
	{
		/// <summary>
		/// Certificates and identity live on the sockets interface, not the utils interface, so
		/// this forwards to <see cref="SteamNetworkingSockets"/>.
		/// </summary>
		/// <remarks>
		/// Resolves to the client interface if one exists, otherwise the game server interface —
		/// the same rule the rest of the binding uses. A dedicated server that only called
		/// <c>SteamServer.Init</c> therefore gets the server interface, which is what you want.
		/// </remarks>
		internal static ISteamNetworkingSockets Internal
		{
			get
			{
				var iface = SteamNetworkingSockets.Internal;
				if ( iface == null )
					throw new InvalidOperationException( "SteamNetworkingSockets is not initialised. Call SteamClient.Init or SteamServer.Init before using certificates." );

				return iface;
			}
		}

		/// <summary>
		/// Valve's stated conservative estimate for the size of a certificate request blob, in
		/// bytes. Useful as a starting buffer size; prefer asking with
		/// <see cref="GetCertificateRequestSize(out string)"/> when you want to be exact.
		/// </summary>
		public const int CertificateRequestSizeEstimate = 512;

		/// <summary>
		/// Maximum length of the diagnostic strings Steam returns from these calls, in bytes.
		/// Mirrors <c>k_cchMaxSteamNetworkingErrMsg</c>.
		/// </summary>
		public const int MaxErrorMessageLength = 1024;

		/// <summary>
		/// Asks Steam how many bytes a certificate request blob needs, without producing one.
		/// </summary>
		/// <param name="error">Steam's diagnostic message on failure; <see langword="null"/> on success.</param>
		/// <returns>The required size in bytes, or <c>-1</c> on failure.</returns>
		/// <remarks>
		/// This is the size-query form of <c>GetCertificateRequest</c> (native: pass a null buffer).
		/// You usually do not need it — <see cref="TryGetCertificateRequest(out byte[], out string)"/>
		/// does the two-step dance for you — but it is here if you are filling a pooled buffer and
		/// want to check it is big enough first.
		/// </remarks>
		public static int GetCertificateRequestSize( out string error )
		{
			var size = 0;

			if ( !TryGetCertificateRequest( IntPtr.Zero, ref size, out error ) )
				return -1;

			return size;
		}

		/// <summary>
		/// Produces a certificate request blob to send to your backend. This is step one of the
		/// workflow described on <see cref="SteamNetworkingCertificates"/>.
		/// </summary>
		/// <param name="blob">
		/// Receives an exactly-sized array on success; <see langword="null"/> on failure.
		/// </param>
		/// <param name="error">Steam's diagnostic message on failure; <see langword="null"/> on success.</param>
		/// <returns><see langword="false"/> if Steam could not produce a request.</returns>
		/// <remarks>
		/// Convenience overload: it queries the size, allocates, and fills. It allocates twice in
		/// the worst case (once for the buffer, once to trim it). That is fine — this runs once at
		/// startup, or occasionally on renewal. Use
		/// <see cref="TryGetCertificateRequest(byte[], int, out int, out string)"/> if you want to
		/// control the buffer yourself.
		/// </remarks>
		public static bool TryGetCertificateRequest( out byte[] blob, out string error )
		{
			blob = null;

			var size = GetCertificateRequestSize( out error );
			if ( size < 0 )
				return false;

			if ( size == 0 )
			{
				blob = Array.Empty<byte>();
				return true;
			}

			var buffer = new byte[size];

			if ( !TryGetCertificateRequest( buffer, 0, out var written, out error ) )
				return false;

			// Steam may populate fewer bytes than the size query reported. Trim rather than hand
			// back trailing zeros, which would corrupt the blob for the signing service.
			if ( written == buffer.Length )
			{
				blob = buffer;
				return true;
			}

			blob = new byte[written];
			Array.Copy( buffer, blob, written );
			return true;
		}

		/// <summary>
		/// Writes a certificate request blob into a caller-owned array.
		/// </summary>
		/// <param name="destination">
		/// Buffer to fill. Size it with <see cref="GetCertificateRequestSize(out string)"/>, or
		/// start from <see cref="CertificateRequestSizeEstimate"/>.
		/// </param>
		/// <param name="offset">Index in <paramref name="destination"/> to start writing at.</param>
		/// <param name="bytesWritten">Receives the number of bytes populated.</param>
		/// <param name="error">Steam's diagnostic message on failure; <see langword="null"/> on success.</param>
		/// <returns><see langword="false"/> if Steam could not produce a request — typically because the buffer was too small.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="destination"/> is <see langword="null"/>.</exception>
		/// <exception cref="ArgumentOutOfRangeException">
		/// <paramref name="offset"/> is negative or past the end of <paramref name="destination"/>.
		/// </exception>
		public static unsafe bool TryGetCertificateRequest( byte[] destination, int offset, out int bytesWritten, out string error )
		{
			if ( destination == null )
				throw new ArgumentNullException( nameof( destination ) );

			if ( offset < 0 || offset > destination.Length )
				throw new ArgumentOutOfRangeException( nameof( offset ) );

			var capacity = destination.Length - offset;

			// A zero-length destination pins to a null pointer, which native reads as "query the
			// required size" — it would then report success having written nothing. Reject it.
			if ( capacity == 0 )
				throw new ArgumentException( "Destination has no room. Use GetCertificateRequestSize to query the size instead.", nameof( destination ) );

			bytesWritten = capacity;

			fixed ( byte* ptr = destination )
			{
				if ( TryGetCertificateRequest( (IntPtr)( ptr + offset ), ref bytesWritten, out error ) )
					return true;
			}

			bytesWritten = 0;
			return false;
		}

#if NETSTANDARD2_1_OR_GREATER || NET
		/// <summary>
		/// Span form of <see cref="TryGetCertificateRequest(byte[], int, out int, out string)"/>.
		/// Available on netstandard2.1+/.NET builds.
		/// </summary>
		/// <param name="destination">Buffer to fill.</param>
		/// <param name="bytesWritten">Receives the number of bytes populated.</param>
		/// <param name="error">Steam's diagnostic message on failure; <see langword="null"/> on success.</param>
		/// <returns><see langword="false"/> if Steam could not produce a request.</returns>
		/// <exception cref="ArgumentException"><paramref name="destination"/> is empty.</exception>
		public static unsafe bool TryGetCertificateRequest( Span<byte> destination, out int bytesWritten, out string error )
		{
			// A zero-length destination pins to a null pointer, which native reads as "query the
			// required size" — it would then report success having written nothing. Reject it.
			if ( destination.Length == 0 )
				throw new ArgumentException( "Destination has no room. Use GetCertificateRequestSize to query the size instead.", nameof( destination ) );

			bytesWritten = destination.Length;

			fixed ( byte* ptr = destination )
			{
				if ( TryGetCertificateRequest( (IntPtr)ptr, ref bytesWritten, out error ) )
					return true;
			}

			bytesWritten = 0;
			return false;
		}
#endif

		/// <summary>
		/// Raw form of <c>GetCertificateRequest</c> — the caller owns the memory. Use this when the
		/// buffer is native, pinned, or comes from a pool.
		/// </summary>
		/// <param name="destination">
		/// Where to write the blob. Pass <see cref="IntPtr.Zero"/> to query the required size
		/// instead of producing a blob; <paramref name="size"/> then receives that size.
		/// </param>
		/// <param name="size">
		/// On entry, the capacity of <paramref name="destination"/> in bytes. On success, the
		/// number of bytes actually populated.
		/// </param>
		/// <param name="error">Steam's diagnostic message on failure; <see langword="null"/> on success.</param>
		/// <returns><see langword="false"/> if Steam could not produce a request.</returns>
		/// <exception cref="ArgumentOutOfRangeException"><paramref name="size"/> is negative.</exception>
		public static bool TryGetCertificateRequest( IntPtr destination, ref int size, out string error )
		{
			if ( size < 0 )
				throw new ArgumentOutOfRangeException( nameof( size ) );

			NetErrorMessage message = default;

			if ( !Internal.GetCertificateRequest( ref size, destination, ref message ) )
			{
				error = ReadErrorMessage( ref message );
				return false;
			}

			error = null;
			return true;
		}

		/// <summary>
		/// Installs a certificate produced by your backend's call to <c>SteamDatagram_CreateCert</c>.
		/// This is the final step of the workflow described on
		/// <see cref="SteamNetworkingCertificates"/>.
		/// </summary>
		/// <param name="certificate">The signed certificate blob, exactly as your backend returned it.</param>
		/// <param name="error">Steam's diagnostic message on failure; <see langword="null"/> on success.</param>
		/// <returns><see langword="false"/> if Steam rejected the certificate.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="certificate"/> is <see langword="null"/>.</exception>
		/// <exception cref="ArgumentException"><paramref name="certificate"/> is empty.</exception>
		/// <remarks>
		/// Call this before creating listen sockets or connections. A rejection here is almost
		/// always one of: the blob was signed with a key that does not match the app, the identity
		/// in the certificate does not match this process, the certificate has expired, or the blob
		/// was truncated in transit. <paramref name="error"/> will say which.
		/// </remarks>
		public static unsafe bool TrySetCertificate( byte[] certificate, out string error )
		{
			if ( certificate == null )
				throw new ArgumentNullException( nameof( certificate ) );

			if ( certificate.Length == 0 )
				throw new ArgumentException( "Certificate blob is empty.", nameof( certificate ) );

			fixed ( byte* ptr = certificate )
			{
				return TrySetCertificate( (IntPtr)ptr, certificate.Length, out error );
			}
		}

#if NETSTANDARD2_1_OR_GREATER || NET
		/// <summary>
		/// Span form of <see cref="TrySetCertificate(byte[], out string)"/>, so a certificate that
		/// arrived inside a larger pooled buffer can be installed without copying it out.
		/// Available on netstandard2.1+/.NET builds.
		/// </summary>
		/// <exception cref="ArgumentException"><paramref name="certificate"/> is empty.</exception>
		public static unsafe bool TrySetCertificate( ReadOnlySpan<byte> certificate, out string error )
		{
			if ( certificate.Length == 0 )
				throw new ArgumentException( "Certificate blob is empty.", nameof( certificate ) );

			fixed ( byte* ptr = certificate )
			{
				return TrySetCertificate( (IntPtr)ptr, certificate.Length, out error );
			}
		}
#endif

		/// <summary>
		/// Raw form of <c>SetCertificate</c> — the caller owns the memory. Steam copies what it
		/// needs during the call, so the buffer does not have to outlive it.
		/// </summary>
		/// <param name="certificate">Pointer to the signed certificate blob.</param>
		/// <param name="length">Length of the blob in bytes.</param>
		/// <param name="error">Steam's diagnostic message on failure; <see langword="null"/> on success.</param>
		/// <returns><see langword="false"/> if Steam rejected the certificate.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="certificate"/> is <see cref="IntPtr.Zero"/>.</exception>
		/// <exception cref="ArgumentOutOfRangeException"><paramref name="length"/> is not positive.</exception>
		/// <remarks>
		/// That Steam copies rather than retains the blob is <b>inferred</b> from the parameter
		/// being <c>const void *</c> with no documented lifetime requirement, which is the
		/// convention throughout this API. If you are holding the buffer in a pool, the cautious
		/// move is to keep it alive until the next frame.
		/// </remarks>
		public static bool TrySetCertificate( IntPtr certificate, int length, out string error )
		{
			if ( certificate == IntPtr.Zero )
				throw new ArgumentNullException( nameof( certificate ) );

			if ( length <= 0 )
				throw new ArgumentOutOfRangeException( nameof( length ) );

			NetErrorMessage message = default;

			if ( !Internal.SetCertificate( certificate, length, ref message ) )
			{
				error = ReadErrorMessage( ref message );
				return false;
			}

			error = null;
			return true;
		}

		/// <summary>
		/// Discards the current identity and certificate and adopts a new identity.
		/// <b>All open connections are closed.</b>
		/// </summary>
		/// <param name="identity">The identity to adopt.</param>
		/// <remarks>
		/// <para>
		/// Wraps <c>ResetIdentity</c>. Valve's header is blunt about the scope of this:
		/// "This function is not actually supported on Steam! It is included for use on other
		/// platforms where the active user can sign out and a new user can sign in." On a Steam
		/// build, expect it to do nothing useful.
		/// </para>
		/// <para>
		/// After resetting, the process has no certificate. Run the request/sign/install workflow
		/// again — see <see cref="SteamNetworkingCertificates"/> — before opening connections.
		/// </para>
		/// <para>
		/// <b>Not exposed:</b> natively you may pass a null identity, which leaves the identity
		/// invalid until <c>SetCertificate</c> supplies one. The generated binding takes the
		/// identity by <see langword="ref"/> and so cannot express a null pointer, and forging one
		/// is not worth the risk in a shipped library. If you need that behaviour, install a
		/// certificate whose embedded identity is the one you want and let it define the identity.
		/// </para>
		/// <para>
		/// Takes the identity by <see langword="ref"/> to avoid copying 136 bytes; it is not
		/// modified.
		/// </para>
		/// </remarks>
		public static void ResetIdentity( ref NetIdentity identity )
		{
			Internal.ResetIdentity( ref identity );
		}

		/// <summary>
		/// Convenience by-value form of <see cref="ResetIdentity(ref NetIdentity)"/>.
		/// </summary>
		public static void ResetIdentity( NetIdentity identity )
		{
			Internal.ResetIdentity( ref identity );
		}

		/// <summary>
		/// Reads Steam's <c>SteamNetworkingErrMsg</c> out of the interop struct.
		/// </summary>
		/// <remarks>
		/// The native type is <c>char[1024]</c> — 1024 <i>bytes</i> of NUL-terminated UTF-8, so
		/// <see cref="NetErrorMessage"/> stores <c>byte</c>s. The scan is bounded at the buffer
		/// length rather than trusting Steam to terminate.
		/// </remarks>
		private static unsafe string ReadErrorMessage( ref NetErrorMessage message )
		{
			fixed ( NetErrorMessage* ptr = &message )
			{
				var bytes = ptr->Value;

				var length = 0;
				while ( length < MaxErrorMessageLength && bytes[length] != 0 )
					length++;

				if ( length == 0 )
					return "Steam reported a failure but did not provide a message.";

				return Utility.Utf8NoBom.GetString( bytes, length );
			}
		}
	}
}
