using System;
using System.Runtime.InteropServices;
using Steamworks.Data;

namespace Steamworks
{
	/// <summary>
	/// Steam Datagram Relay (SDR) support for dedicated servers that run <i>inside</i> Valve's
	/// relay network.
	///
	/// <para>
	/// <b>What this is.</b> Normally your dedicated server has a public IP, players connect
	/// straight to it, and anyone who joins learns that IP and can attack it. SDR hosting
	/// removes the public IP from the equation entirely: your server sits in a Valve data
	/// center, players connect to Valve's relays, and the relays forward traffic to you over
	/// a private path. Players never learn where you are. As a side effect, traffic rides
	/// Valve's backbone between relays, which is frequently faster and more stable than the
	/// public internet route.
	/// </para>
	///
	/// <para>
	/// <b>Two flavours, and you must pick one.</b>
	/// </para>
	/// <list type="number">
	/// <item><description>
	/// <b>Ticketless</b> — the server listens with
	/// <see cref="CreateRelaySocket{T}(int)"/> (<c>CreateListenSocketP2P</c>) and clients
	/// connect with <see cref="ConnectRelay{T}(SteamId, int)"/>. Any user who owns the app
	/// and is signed in can attempt to connect. Much simpler; no backend required. If that
	/// is acceptable, stop reading — you do not need this file.
	/// </description></item>
	/// <item><description>
	/// <b>Ticketed</b> — the server listens with
	/// <see cref="CreateHostedDedicatedServerSocket{T}(int)"/> and clients connect with
	/// <see cref="ConnectToHostedDedicatedServer{T}(NetIdentity, int)"/>. Now <i>your</i>
	/// backend decides who may connect, by issuing signed tickets. It is more moving parts,
	/// but it also means a client can reconnect to your server even if it has been
	/// disconnected from Steam, because the ticket is already cached locally.
	/// </description></item>
	/// </list>
	///
	/// <para>
	/// <b>SDR_LISTEN_PORT is the thing that trips everyone up.</b> Every server-side call here
	/// depends on the <c>SDR_LISTEN_PORT</c> environment variable, which names the UDP port
	/// the relays will send your traffic to. In a real Valve data center it is set for you.
	/// On your own machine nothing sets it, so <see cref="HostedDedicatedServerPort"/> returns
	/// 0, <see cref="HostedDedicatedServerPopId"/> returns the zero ID,
	/// <see cref="GetHostedDedicatedServerAddress"/> returns
	/// <see cref="Result.InvalidState"/>, and creating the listen socket fails. That is not a
	/// bug in your code — it is the environment. Set the variable yourself for local
	/// development and the POP ID becomes <see cref="NetPOPID.Dev"/>.
	/// </para>
	///
	/// <para>
	/// (Valve's own header calls this variable <c>SDR_LISTEN_SOCKET</c> in exactly one place —
	/// the failure list for <c>GetHostedDedicatedServerAddress</c> — and <c>SDR_LISTEN_PORT</c>
	/// in the other three. We believe that single mention is a typo in the header; every
	/// other reference, and Valve's partner documentation, says <c>SDR_LISTEN_PORT</c>.)
	/// </para>
	///
	/// <para>
	/// <b>Which interface these run on.</b> The server-side calls are documented by Valve as
	/// gameserver-only, so they are routed through the interface created by
	/// <c>SteamServer.Init</c> and throw <see cref="InvalidOperationException"/> if it does
	/// not exist. This matters in a listen-server build, where the client interface would
	/// otherwise be picked and quietly answer the wrong questions.
	/// </para>
	///
	/// <para>
	/// <b>Also call <c>SteamNetworkingUtils.InitRelayNetworkAccess()</c></b> when your app
	/// starts, on both ends. It warms up the relay network so the first connection is not
	/// paying for the discovery round trips.
	/// </para>
	/// </summary>
	/// <example>
	/// A complete ticketed hosted-server startup:
	/// <code>
	/// // ---- On the dedicated server ----
	/// SteamServer.Init( AppId, serverInit );
	///
	/// if ( SteamNetworkingSockets.HostedDedicatedServerPort == 0 )
	///     throw new Exception( "SDR_LISTEN_PORT is not set - this build is not SDR-hosted." );
	///
	/// // Authenticate to your own backend and hand it the routing info in one step.
	/// var login = SteamNetworkingSockets.GetGameCoordinatorServerLogin( myAppDataBytes );
	/// if ( login.Result != Result.OK )
	///     throw new Exception( $"{login.Result}: {login.DiagnosticMessage}" );
	///
	/// await Backend.RegisterAsync( login.SignedBlob );   // backend now issues tickets
	///
	/// var socket = SteamNetworkingSockets.CreateHostedDedicatedServerSocket&lt;MySocketManager&gt;( 0 );
	///
	/// // ---- On the client ----
	/// // 1. Ask your backend for a ticket, then hand the bytes to Steam's ticket cache.
	/// // 2. Connect. The virtual port must match the one the server listened on.
	/// var conn = SteamNetworkingSockets.ConnectToHostedDedicatedServer&lt;MyConnectionManager&gt;( serverIdentity, 0 );
	/// </code>
	/// </example>
	public partial class SteamNetworkingSockets
	{
		//
		// SteamDatagramHostedAddress::m_data is char[128]. The generated struct marshals it as
		// a ByValArray of that size, and the marshaller needs a correctly sized managed array
		// on the way IN as well as OUT, so we always allocate one before calling.
		//
		private const int HostedServerAddressDataSize = 128;

		/// <summary>
		/// The UDP port the relays will forward traffic to, taken from the
		/// <c>SDR_LISTEN_PORT</c> environment variable.
		///
		/// <para>
		/// Set automatically in a Valve data center. <b>0 means you are not SDR-hosted</b> —
		/// either you are on a developer machine and have not set the variable, or the build
		/// simply is not deployed that way. Checking this at startup is the cheapest way to
		/// find out which world you are in before anything else fails confusingly.
		/// </para>
		/// </summary>
		/// <exception cref="InvalidOperationException">Neither <c>SteamServer.Init</c> nor <c>SteamClient.Init</c> has run.</exception>
		public static ushort HostedDedicatedServerPort => HostedServerQueryInterface().GetHostedDedicatedServerPort();

		/// <summary>
		/// Which Valve data center this server is running in.
		///
		/// <para>
		/// The zero ID (<see cref="NetPOPID.IsValid"/> is <see langword="false"/>) when
		/// <c>SDR_LISTEN_PORT</c> is not set. <see cref="NetPOPID.Dev"/> in any non-production
		/// environment — so if you set <c>SDR_LISTEN_PORT</c> by hand on your laptop you will
		/// see <c>dev</c>, not a real data center code.
		/// </para>
		/// </summary>
		/// <exception cref="InvalidOperationException">Neither <c>SteamServer.Init</c> nor <c>SteamClient.Init</c> has run.</exception>
		public static NetPOPID HostedDedicatedServerPopId => new NetPOPID( HostedServerQueryInterface().GetHostedDedicatedServerPOPID().Value );

		/// <summary>
		/// Ask Steam for the opaque routing information the relays need in order to forward
		/// player traffic to this server.
		///
		/// <para>
		/// You send this to <i>your own backend</i>, which bakes it into the connection
		/// tickets it issues. Valve is blunt about the alternative: "The returned blob is not
		/// encrypted. Send it to your backend, but don't directly share it with clients."
		/// Handing it to players defeats the purpose of SDR hosting.
		/// </para>
		///
		/// <para>
		/// Valve recommends using <see cref="GetGameCoordinatorServerLogin"/> instead where
		/// possible, since it contains this same routing information <i>and</i> authenticates
		/// the server to your backend at the same time. Reach for this call when you only
		/// want the routing data, or when you are logging/diagnosing.
		/// </para>
		///
		/// <para>
		/// This never throws on a Steam-side failure. Check
		/// <see cref="HostedServerAddress.Result"/> and log
		/// <see cref="HostedServerAddress.DiagnosticMessage"/>, which carries Valve's own
		/// explanation of what went wrong.
		/// </para>
		/// </summary>
		/// <returns>
		/// The routing blob on success; otherwise a value carrying the failure
		/// <see cref="Result"/> and Steam's diagnostic text.
		/// </returns>
		/// <exception cref="InvalidOperationException"><c>SteamServer.Init</c> has not run — this is a dedicated-server API.</exception>
		public static HostedServerAddress GetHostedDedicatedServerAddress()
		{
			var iface = RequireServerInterface( nameof( GetHostedDedicatedServerAddress ) );

			var routing = default( SteamDatagramHostedAddress );
			routing.Data = new byte[HostedServerAddressDataSize];

			var result = iface.GetHostedDedicatedServerAddress( ref routing );

			return new HostedServerAddress( ref routing, result );
		}

		/// <summary>
		/// Produce the signed blob this server sends to your backend to log in, prove its
		/// identity, and deliver its SDR routing information in one step.
		///
		/// <para>
		/// See <see cref="GameCoordinatorServerLogin"/> for what the blob contains, where it
		/// fits in the boot sequence, and how your backend verifies it. In short: call this
		/// once after the game server has logged on, POST
		/// <see cref="GameCoordinatorServerLogin.SignedBlob"/> to your backend, and let the
		/// backend start issuing tickets for this server.
		/// </para>
		///
		/// <para>
		/// This never throws on a Steam-side failure — check
		/// <see cref="GameCoordinatorServerLogin.Result"/>. The most common outcome on a first
		/// attempt is <see cref="Result.NotLoggedOn"/>, because the game server logs on
		/// asynchronously; retry once <c>SteamServer.OnSteamServersConnected</c> has fired.
		/// </para>
		/// </summary>
		/// <param name="appData">
		/// Optional bytes of your own that get signed along with everything else — region,
		/// build number, instance ID, whatever your backend wants to trust. Maximum
		/// <see cref="GameCoordinatorServerLogin.MaxAppDataSize"/> bytes. Pass
		/// <see langword="null"/> for none.
		/// </param>
		/// <exception cref="InvalidOperationException"><c>SteamServer.Init</c> has not run, or Steam reported a blob length outside its own documented maximum.</exception>
		/// <exception cref="ArgumentException"><paramref name="appData"/> is longer than <see cref="GameCoordinatorServerLogin.MaxAppDataSize"/>.</exception>
		public static unsafe GameCoordinatorServerLogin GetGameCoordinatorServerLogin( byte[] appData = null )
		{
			if ( appData != null && appData.Length > GameCoordinatorServerLogin.MaxAppDataSize )
				throw new ArgumentException( $"appData is {appData.Length} bytes - the maximum is {GameCoordinatorServerLogin.MaxAppDataSize}", nameof( appData ) );

			var iface = RequireServerInterface( nameof( GetGameCoordinatorServerLogin ) );

			//
			// Valve: "Populate the app data in pLoginInfo (m_cbAppData and m_appData). You can
			// leave all other fields uninitialized." m_appData is a fixed size buffer stored
			// inline, so it needs no allocation - but m_routing.m_data is still a marshalled
			// array, and the marshaller needs a real array to copy into in both directions.
			//
			var info = default( SteamDatagramGameCoordinatorServerLogin );
			info.Routing.Data = new byte[HostedServerAddressDataSize];
			info.CbAppData = appData?.Length ?? 0;

			if ( info.CbAppData > 0 )
				Marshal.Copy( appData, 0, (IntPtr)info.AppData, info.CbAppData );

			//
			// pcbSignedBlob is in/out: in = capacity, out = bytes actually written. Valve
			// requires the buffer to be at least k_cbMaxSteamDatagramGameCoordinatorServerLoginSerialized.
			//
			var blobLength = GameCoordinatorServerLogin.MaxSerializedSize;
			var blob = Marshal.AllocHGlobal( GameCoordinatorServerLogin.MaxSerializedSize );

			try
			{
				var result = iface.GetGameCoordinatorServerLogin( ref info, ref blobLength, blob );

				if ( result != Result.OK )
				{
					//
					// On failure Steam writes a plain-text explanation into the blob buffer
					// instead of a blob. It is the only place that text exists.
					//
					return new GameCoordinatorServerLogin( result, default, default, default, 0, null,
						ReadNullTerminated( blob, GameCoordinatorServerLogin.MaxSerializedSize ) );
				}

				if ( blobLength < 0 || blobLength > GameCoordinatorServerLogin.MaxSerializedSize )
					throw new InvalidOperationException( $"Steam reported a signed blob of {blobLength} bytes, which is outside the buffer we gave it ({GameCoordinatorServerLogin.MaxSerializedSize}). Refusing to read it." );

				var signedBlob = new byte[blobLength];
				Marshal.Copy( blob, signedBlob, 0, blobLength );

				var routing = new HostedServerAddress( ref info.Routing, Result.OK );

				return new GameCoordinatorServerLogin( result, info.Identity, routing, info.AppID, info.Time, signedBlob, null );
			}
			finally
			{
				Marshal.FreeHGlobal( blob );
			}
		}

		/// <summary>
		/// Create the listen socket for a ticketed SDR-hosted dedicated server. The physical
		/// UDP port comes from <c>SDR_LISTEN_PORT</c>; you only choose the <i>virtual</i> port.
		///
		/// <para>
		/// Clients reaching this socket must present a ticket issued by your backend and must
		/// connect with <see cref="ConnectToHostedDedicatedServer{T}(NetIdentity, int)"/>. If
		/// you do not want to run a ticket authority, use
		/// <see cref="CreateRelaySocket{T}(int)"/> instead — that is the ticketless path, and
		/// any owner of the app can connect.
		/// </para>
		///
		/// <para>
		/// To use this, derive a class from <see cref="SocketManager"/> and override as much as
		/// you want.
		/// </para>
		/// </summary>
		/// <typeparam name="T">Your <see cref="SocketManager"/> subclass.</typeparam>
		/// <param name="virtualPort">
		/// A small integer that identifies this listen socket. Clients must pass the same
		/// number. Use 0 unless you deliberately run several listen sockets on one server.
		/// </param>
		/// <exception cref="InvalidOperationException">
		/// <c>SteamServer.Init</c> has not run, or Steam refused to create the socket — which
		/// almost always means <c>SDR_LISTEN_PORT</c> is not set.
		/// </exception>
		/// <exception cref="ArgumentOutOfRangeException"><paramref name="virtualPort"/> is negative.</exception>
		public static T CreateHostedDedicatedServerSocket<T>( int virtualPort = 0 ) where T : SocketManager, new()
		{
			var socket = CreateHostedDedicatedServerListenSocket( virtualPort );

			var t = new T();
			t.Socket = socket;
			t.Initialize();

			SetSocketManager( t.Socket.Id, t );
			return t;
		}

		/// <summary>
		/// Create the listen socket for a ticketed SDR-hosted dedicated server, driving an
		/// <see cref="ISocketManager"/> you supply instead of a subclass.
		///
		/// <para>
		/// See <see cref="CreateHostedDedicatedServerSocket{T}(int)"/> for what "ticketed"
		/// means and when you should be using the ticketless
		/// <see cref="CreateRelaySocket(int, ISocketManager)"/> instead.
		/// </para>
		/// </summary>
		/// <param name="virtualPort">A small integer identifying this listen socket. Clients must match it.</param>
		/// <param name="intrface">Receives the connection and message callbacks.</param>
		/// <exception cref="InvalidOperationException"><c>SteamServer.Init</c> has not run, or Steam refused to create the socket (usually <c>SDR_LISTEN_PORT</c> is not set).</exception>
		/// <exception cref="ArgumentOutOfRangeException"><paramref name="virtualPort"/> is negative.</exception>
		/// <exception cref="ArgumentNullException"><paramref name="intrface"/> is <see langword="null"/>.</exception>
		public static SocketManager CreateHostedDedicatedServerSocket( int virtualPort, ISocketManager intrface )
		{
			if ( intrface == null )
				throw new ArgumentNullException( nameof( intrface ) );

			var socket = CreateHostedDedicatedServerListenSocket( virtualPort );

			var t = new SocketManager
			{
				Socket = socket,
				Interface = intrface
			};

			t.Initialize();

			SetSocketManager( t.Socket.Id, t );
			return t;
		}

		/// <summary>
		/// Connect to a dedicated server hosted in a Valve data center, using a ticket your
		/// backend already issued.
		///
		/// <para>
		/// <b>You must already have a ticket, and you do not pass it here.</b> Tickets live in
		/// a persistent local cache — you put one there when your backend sends it to you, and
		/// this call looks it up. Valve chose a cache rather than an argument specifically so
		/// that reconnecting survives the client losing its Steam connection, the game
		/// restarting, or a crash. Without a cached ticket for this server and virtual port,
		/// the connect attempt fails.
		/// </para>
		///
		/// <para>
		/// If you are <i>not</i> issuing your own tickets, do not use this at all — use
		/// <see cref="ConnectRelay{T}(SteamId, int)"/>, which connects to an
		/// SDR-hosted server in auto-ticket mode (the server must be listening via
		/// <see cref="CreateRelaySocket{T}(int)"/>).
		/// </para>
		///
		/// <para>
		/// Call <c>SteamNetworkingUtils.InitRelayNetworkAccess()</c> when your app initialises
		/// so the relay network is already warm by the time you get here.
		/// </para>
		/// </summary>
		/// <typeparam name="T">Your <see cref="ConnectionManager"/> subclass.</typeparam>
		/// <param name="identity">
		/// The server's identity. A <see cref="SteamId"/> converts implicitly, so you can pass
		/// one directly.
		/// </param>
		/// <param name="virtualPort">Must match the virtual port the server listened on.</param>
		/// <exception cref="InvalidOperationException">
		/// Steam refused the connection outright. The usual cause is no cached ticket for this
		/// server and virtual port.
		/// </exception>
		/// <exception cref="ArgumentOutOfRangeException"><paramref name="virtualPort"/> is negative.</exception>
		public static T ConnectToHostedDedicatedServer<T>( NetIdentity identity, int virtualPort = 0 ) where T : ConnectionManager, new()
		{
			var connection = ConnectToHostedDedicatedServerInternal( ref identity, virtualPort );

			var t = new T();
			t.Connection = connection;

			SetConnectionManager( t.Connection.Id, t );
			return t;
		}

		/// <summary>
		/// Connect to a dedicated server hosted in a Valve data center, driving an
		/// <see cref="IConnectionManager"/> you supply instead of a subclass.
		///
		/// <para>
		/// See <see cref="ConnectToHostedDedicatedServer{T}(NetIdentity, int)"/> for the ticket
		/// requirement — it is the part that catches people out.
		/// </para>
		/// </summary>
		/// <param name="identity">The server's identity. A <see cref="SteamId"/> converts implicitly.</param>
		/// <param name="virtualPort">Must match the virtual port the server listened on.</param>
		/// <param name="iface">Receives the connection and message callbacks.</param>
		/// <exception cref="InvalidOperationException">Steam refused the connection outright, usually because no ticket is cached.</exception>
		/// <exception cref="ArgumentOutOfRangeException"><paramref name="virtualPort"/> is negative.</exception>
		/// <exception cref="ArgumentNullException"><paramref name="iface"/> is <see langword="null"/>.</exception>
		public static ConnectionManager ConnectToHostedDedicatedServer( NetIdentity identity, int virtualPort, IConnectionManager iface )
		{
			if ( iface == null )
				throw new ArgumentNullException( nameof( iface ) );

			var connection = ConnectToHostedDedicatedServerInternal( ref identity, virtualPort );

			var t = new ConnectionManager
			{
				Connection = connection,
				Interface = iface
			};

			SetConnectionManager( t.Connection.Id, t );
			return t;
		}

		private static Socket CreateHostedDedicatedServerListenSocket( int virtualPort )
		{
			if ( virtualPort < 0 )
				throw new ArgumentOutOfRangeException( nameof( virtualPort ), "Virtual port cannot be negative" );

			var iface = RequireServerInterface( nameof( CreateHostedDedicatedServerSocket ) );

			var options = Array.Empty<NetKeyValue>();
			var socket = iface.CreateHostedDedicatedServerListenSocket( virtualPort, options.Length, options );

			if ( socket.Id == 0 )
			{
				throw new InvalidOperationException(
					$"Steam refused to create a hosted dedicated server listen socket on virtual port {virtualPort}. " +
					"The usual cause is that the SDR_LISTEN_PORT environment variable is not set, so there is no UDP port to bind. " +
					$"HostedDedicatedServerPort currently reports {HostedDedicatedServerPort}." );
			}

			return socket;
		}

		private static Connection ConnectToHostedDedicatedServerInternal( ref NetIdentity identity, int virtualPort )
		{
			if ( virtualPort < 0 )
				throw new ArgumentOutOfRangeException( nameof( virtualPort ), "Virtual port cannot be negative" );

			var options = Array.Empty<NetKeyValue>();
			var connection = Internal.ConnectToHostedDedicatedServer( ref identity, virtualPort, options.Length, options );

			if ( connection.Id == 0 )
			{
				throw new InvalidOperationException(
					$"Steam refused to start a connection to hosted dedicated server '{identity}' on virtual port {virtualPort}. " +
					"The usual cause is that no relay auth ticket for this server is in the local cache - your backend has to issue one first. " +
					"It can also mean the identity is unset or not one Steam recognises. " +
					"If you are not issuing your own tickets, use ConnectRelay instead." );
			}

			return connection;
		}

		//
		// The hosted-server calls that Valve documents as gameserver-only. Failing loudly here
		// is much kinder than silently answering from the user interface in a listen-server
		// build and returning plausible nonsense.
		//
		private static ISteamNetworkingSockets RequireServerInterface( string caller )
		{
			var iface = InternalServer;

			if ( iface == null || !iface.IsValid )
			{
				throw new InvalidOperationException(
					$"{caller} is a dedicated-server API and has to go through the game server networking interface. " +
					"Call SteamServer.Init(...) first - SteamClient.Init is not enough." );
			}

			return iface;
		}

		//
		// GetHostedDedicatedServerPort/POPID only read an environment variable, so they are
		// harmless on either interface. Prefer the server one when it exists so a listen
		// server reports what its server half sees.
		//
		private static ISteamNetworkingSockets HostedServerQueryInterface()
		{
			var iface = InternalServer;

			if ( iface == null || !iface.IsValid )
				iface = Internal;

			if ( iface == null || !iface.IsValid )
				throw new InvalidOperationException( "Steam networking is not initialised. Call SteamServer.Init(...) or SteamClient.Init(...) first." );

			return iface;
		}

		private static unsafe string ReadNullTerminated( IntPtr buffer, int maxLength )
		{
			var p = (byte*)buffer;

			var length = 0;
			while ( length < maxLength && p[length] != 0 )
				length++;

			if ( length == 0 )
				return string.Empty;

			return Utility.Utf8NoBom.GetString( p, length );
		}
	}
}
