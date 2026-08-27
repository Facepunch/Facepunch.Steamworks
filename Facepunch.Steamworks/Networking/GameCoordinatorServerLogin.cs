using System;

namespace Steamworks.Data
{
	/// <summary>
	/// The result of asking Steam to produce a <b>signed login blob</b> that a hosted
	/// dedicated server sends to your own backend ("game coordinator") to prove who it is,
	/// where it is, and how the relays should reach it. This wraps Valve's
	/// <c>SteamDatagramGameCoordinatorServerLogin</c> plus the serialized, signed bytes.
	///
	/// <para>
	/// <b>The problem this solves.</b> Your backend is about to tell players "go join server
	/// X". Two things have to be true before it can safely do that: it must know the server
	/// is genuinely one of yours and not somebody's laptop pretending, and it must know the
	/// SDR routing information to bake into the tickets it hands out. Steam signs this blob
	/// with the certificate issued to that server, so your backend can verify both facts in
	/// one step, using <c>SteamDatagram_ParseHostedServerLogin</c> from Valve's game
	/// coordinator library. This is why Valve recommends it over
	/// <see cref="SteamNetworkingSockets.GetHostedDedicatedServerAddress"/>: the routing data
	/// is included anyway, and you authenticate at the same time.
	/// </para>
	///
	/// <para>
	/// <b>Where it fits in the boot sequence.</b> Server starts → <c>SteamServer.Init</c> →
	/// logs on anonymously → calls this once → POSTs <see cref="SignedBlob"/> to your backend
	/// → backend verifies, records the routing info, and starts issuing tickets → server
	/// calls <see cref="SteamNetworkingSockets.CreateHostedDedicatedServerSocket{T}"/> and
	/// waits for players. It is a once-at-startup call, so it deliberately allocates for
	/// clarity rather than contorting itself to avoid a 4 KB array.
	/// </para>
	///
	/// <para>
	/// <b>Do not give this to players.</b> Valve: "The routing blob returned here is not
	/// encrypted. Send it to your backend and don't share it directly with clients."
	/// </para>
	///
	/// <para>
	/// <b>Dev machines.</b> Signing needs a certificate. In development it is acceptable not
	/// to have one, but your backend then has to opt in to insecure dev logins when it parses
	/// the blob. Do not ship that.
	/// </para>
	/// </summary>
	/// <example>
	/// <code>
	/// // Your own data - anything your backend wants to know about this instance.
	/// var appData = Encoding.UTF8.GetBytes( "{\"region\":\"eu\",\"build\":4711}" );
	///
	/// var login = SteamNetworkingSockets.GetGameCoordinatorServerLogin( appData );
	///
	/// if ( login.Result != Result.OK )
	/// {
	///     // NotLoggedOn  -> SteamServer.Init has not finished logging on yet
	///     // InvalidState -> SDR_LISTEN_PORT is not set; this is not an SDR-hosted server
	///     // Pending      -> credentials not fetched yet, retry in a moment
	///     Log.Error( $"GC login failed: {login.Result} - {login.DiagnosticMessage}" );
	///     return;
	/// }
	///
	/// Log.Info( $"Identity {login.Identity}, app {login.AppId}, pop {login.Routing.PopId}" );
	/// await Backend.LoginServerAsync( login.SignedBlob );
	/// </code>
	/// </example>
	public sealed class GameCoordinatorServerLogin
	{
		/// <summary>
		/// Maximum number of bytes of your own data you may attach, matching Valve's
		/// <c>k_cbMaxSteamDatagramGameCoordinatorServerLoginAppData</c>.
		/// </summary>
		public const int MaxAppDataSize = 2048;

		/// <summary>
		/// Size of the buffer Valve requires for the serialized blob, matching
		/// <c>k_cbMaxSteamDatagramGameCoordinatorServerLoginSerialized</c>. The blob Steam
		/// actually produces is normally much smaller — see <see cref="SignedBlob"/>.
		/// </summary>
		public const int MaxSerializedSize = 4096;

		/// <summary>
		/// What Steam said.
		///
		/// <list type="bullet">
		/// <item><description><see cref="Steamworks.Result.OK"/> — everything below is populated.</description></item>
		/// <item><description><see cref="Steamworks.Result.NotLoggedOn"/> — the game server has not logged on yet. Wait for the logon callback and retry.</description></item>
		/// <item><description><see cref="Steamworks.Result.InvalidState"/> — not configured to listen for SDR. In practice: <c>SDR_LISTEN_PORT</c> is not set.</description></item>
		/// <item><description><see cref="Steamworks.Result.Pending"/> — Steam does not have the authentication information yet.</description></item>
		/// </list>
		///
		/// Anything other than <see cref="Steamworks.Result.OK"/> leaves
		/// <see cref="SignedBlob"/> <see langword="null"/> and puts an explanation in
		/// <see cref="DiagnosticMessage"/>.
		/// </summary>
		public Result Result { get; }

		/// <summary>
		/// The identity Steam assigned this game server. Only meaningful when
		/// <see cref="Result"/> is <see cref="Steamworks.Result.OK"/>.
		/// </summary>
		public NetIdentity Identity { get; }

		/// <summary>
		/// The SDR routing information for this server — the same thing
		/// <see cref="SteamNetworkingSockets.GetHostedDedicatedServerAddress"/> returns, but
		/// obtained as part of an authenticated login. Read
		/// <see cref="HostedServerAddress.PopId"/> from it to find out which data center you
		/// are in.
		/// </summary>
		public HostedServerAddress Routing { get; }

		/// <summary>
		/// The AppID this server is running as. Worth asserting against in your backend —
		/// a signed login for the wrong app is a signed login you should reject.
		/// </summary>
		public AppId AppId { get; }

		/// <summary>
		/// Steam's timestamp for this login, in UTC. Your backend can use it to reject
		/// stale blobs that have been captured and replayed.
		/// </summary>
		public DateTime Time { get; }

		/// <summary>
		/// The serialized, signed blob to hand to your backend, trimmed to its actual length
		/// (not padded to <see cref="MaxSerializedSize"/>). <see langword="null"/> unless
		/// <see cref="Result"/> is <see cref="Steamworks.Result.OK"/>.
		///
		/// <para>
		/// Treat it as opaque. Your backend passes it to Valve's
		/// <c>SteamDatagram_ParseHostedServerLogin</c>, which verifies the signature and
		/// unpacks the same fields you can see on this object.
		/// </para>
		/// </summary>
		public byte[] SignedBlob { get; }

		/// <summary>
		/// Steam's non-localized English diagnostic text, written into the blob buffer instead
		/// of a blob when the call fails. <see langword="null"/> on success.
		///
		/// <para>
		/// This is usually far more specific than <see cref="Result"/> alone and there is
		/// nowhere else to obtain it — log it.
		/// </para>
		/// </summary>
		public string DiagnosticMessage { get; }

		internal GameCoordinatorServerLogin( Result result, NetIdentity identity, HostedServerAddress routing, AppId appId, uint unixTime, byte[] signedBlob, string diagnosticMessage )
		{
			Result = result;
			Identity = identity;
			Routing = routing;
			AppId = appId;
			Time = Epoch.ToDateTime( unixTime );
			SignedBlob = signedBlob;
			DiagnosticMessage = diagnosticMessage;
		}

		public override string ToString()
		{
			if ( Result != Steamworks.Result.OK )
				return $"GameCoordinatorServerLogin( {Result}: {DiagnosticMessage} )";

			return $"GameCoordinatorServerLogin( {Identity}, app={AppId}, pop={Routing.PopId}, {SignedBlob.Length} signed bytes )";
		}
	}
}
