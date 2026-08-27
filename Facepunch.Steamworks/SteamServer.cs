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
	/// Provides the core of the Steam Game Servers API
	/// </summary>
	public partial class SteamServer : SteamServerClass<SteamServer>
	{
		internal static ISteamGameServer Internal => Interface as ISteamGameServer;

		internal override bool InitializeInterface( bool server )
		{
			SetInterface( server, new ISteamGameServer( server ) );
			if ( Interface.Self == IntPtr.Zero ) return false;

			InstallEvents();

			return true;
		}

		/// <summary>
		/// Whether the game server interface has been created and is usable. This is the guard
		/// <see cref="Init"/> checks to refuse a double initialization.
		/// </summary>
		/// <value>
		/// <see langword="true"/> once <see cref="Init"/> has succeeded and until <see cref="Shutdown"/> is
		/// called. It says nothing about whether the server has logged on - use <see cref="LoggedOn"/> for
		/// that. Touching most other members of this class while this is <see langword="false"/> throws a
		/// <c>NullReferenceException</c> rather than failing gracefully.
		/// </value>
		public static bool IsValid => Internal != null && Internal.IsValid;

		internal static void InstallEvents()
		{
            Dispatch.Install<ValidateAuthTicketResponse_t>( x => OnValidateAuthTicketResponse?.Invoke( x.SteamID, x.OwnerSteamID, x.AuthSessionResponse ), true );
			Dispatch.Install<SteamServersConnected_t>( x => OnSteamServersConnected?.Invoke(), true );
			Dispatch.Install<SteamServerConnectFailure_t>( x => OnSteamServerConnectFailure?.Invoke( x.Result, x.StillRetrying ), true );
			Dispatch.Install<SteamServersDisconnected_t>( x => OnSteamServersDisconnected?.Invoke( x.Result ), true );
			Dispatch.Install<SteamNetAuthenticationStatus_t>(x => OnSteamNetAuthenticationStatus?.Invoke(x.Avail), true);
		}

		/// <summary>
		/// Steam's verdict on a ticket passed to <see cref="BeginAuthSession"/>. This is where a player is
		/// actually authenticated or rejected - <see cref="BeginAuthSession"/> returning
		/// <see langword="true"/> only means the check started.
		/// </summary>
		/// <remarks>
		/// The first <c>SteamId</c> is the player. The second is the account that owns the game
		/// licence, which differs from the first when the game is borrowed through Family Sharing - check
		/// DLC entitlements with <see cref="UserHasLicenseForApp"/> against the owner, but identify and ban
		/// the player by the first. The <see cref="AuthResponse"/> is the verdict:
		/// <see cref="AuthResponse.OK"/> admits them, anything else is a rejection reason.
		/// <para>
		/// This does not fire only once. Steam re-raises it during the session to revoke a player who is
		/// subsequently VAC banned (<see cref="AuthResponse.VACBanned"/>), whose ticket was cancelled
		/// (<see cref="AuthResponse.AuthTicketCanceled"/>), or who logged in elsewhere
		/// (<see cref="AuthResponse.LoggedInElseWhere"/>), so stay subscribed for the whole session and be
		/// prepared to kick someone who is already playing.
		/// </para>
		/// </remarks>
		public static event Action<SteamId, SteamId, AuthResponse> OnValidateAuthTicketResponse;

		/// <summary>
		/// Invoked when a connection to the Steam back-end has been established.
		/// This means the server now is logged on and has a working connection to the Steam master server.
		/// </summary>
		public static event Action OnSteamServersConnected;

		/// <summary>
		/// This will occur periodically if the Steam client is not connected, and has failed when retrying to establish a connection (result, stilltrying).
		/// </summary>
		public static event Action<Result, bool> OnSteamServerConnectFailure;

		/// <summary>
		/// Invoked when the server is disconnected from Steam
		/// </summary>
		public static event Action<Result> OnSteamServersDisconnected;

		/// <summary>
		/// Invoked when authentication status changes, useful for grabbing <see cref="SteamId"/> once availability is current.
		/// </summary>
		public static event Action<SteamNetworkingAvailability> OnSteamNetAuthenticationStatus;


		/// <summary>
		/// Initialize the steam server.
		/// If <paramref name="asyncCallbacks"/> is <see langword="false"/> you need to call <see cref="RunCallbacks"/> manually every frame.
		/// </summary>
		/// <param name="appid">
		/// The app this server is hosting. Before the native init runs, this is written into the
		/// process-wide <c>SteamAppId</c> and <c>SteamGameId</c> environment variables - so if you host a
		/// client and a server in one process, the later initializer wins and both end up pointed at the
		/// same app. Its decimal form also becomes the initial <see cref="Product"/> string.
		/// </param>
		/// <param name="init">
		/// Ports, bind address, security mode, version string, mod directory and game description. Every
		/// field on it is baked in by this call and cannot be changed afterwards - see
		/// <see cref="SteamServerInit"/>.
		/// </param>
		/// <param name="asyncCallbacks">
		/// When <see langword="true"/> (the default) a background loop pumps the server callback queue
		/// roughly every 32 ms, so <c>await</c> on this API works without you doing anything. Pass
		/// <see langword="false"/> on a server with its own tick loop and call <see cref="RunCallbacks"/>
		/// yourself once per frame - if you pass <see langword="false"/> and then forget, no callback ever
		/// fires and every awaited call hangs forever rather than failing.
		/// </param>
		/// <exception cref="System.Exception">
		/// Thrown if the server is already initialized, or if the native init failed - typically a port
		/// already in use, a bad bind address, or a Steam library mismatch. The message carries the
		/// arguments used and Steam's own error text.
		/// </exception>
		/// <remarks>
		/// <para>
		/// This does not log the server on. Steam splits server setup into two phases and this is only the
		/// first: after it returns you set the properties you care about and then call
		/// <see cref="LogOnAnonymous"/> or <see cref="LogOn"/>. Anything that must be established before
		/// login - <see cref="Product"/>, <see cref="GameDescription"/>, <see cref="ModDir"/>,
		/// <see cref="DedicatedServer"/> - is set for you here from <paramref name="init"/>, which is why
		/// those four have no public setter.
		/// </para>
		/// <para>
		/// <see cref="SteamServerInit.Secure"/> selects Valve's server mode: <see langword="true"/> means
		/// authenticate users, list on the master server and VAC-protect connecting clients;
		/// <see langword="false"/> means authenticate and list but run no VAC. There is no mode here that
		/// skips authentication entirely.
		/// </para>
		/// <para>
		/// Defaults applied on your behalf: <see cref="MaxPlayers"/> 32, <see cref="BotCount"/> 0,
		/// <see cref="Passworded"/> <see langword="false"/>, and advertising already switched on. Override
		/// them straight after this call. Note that Valve's guidance is to set all relevant server
		/// parameters before enabling advertisement; this binding enables it first, so treat the window
		/// between <c>Init</c> and <see cref="LogOnAnonymous"/> as the place to finish configuring.
		/// </para>
		/// </remarks>
		/// <example>
		/// A dedicated-server startup in the order Steam expects:
		/// <code>
		/// var init = new SteamServerInit( "rust", "Rusty Mode" )
		/// {
		///     GamePort = 28015,
		///     QueryPort = 28016,
		///     Secure = true,
		///     VersionString = "1.0.0.0"
		/// };
		///
		/// SteamServer.Init( 252490, init );
		///
		/// // Server-state properties - safe here and any time later.
		/// SteamServer.ServerName = "Rusty Mode | EU";
		/// SteamServer.MapName = "Procedural Map";
		/// SteamServer.MaxPlayers = 200;
		/// SteamServer.GameTags = "pve,monthly,eu";
		///
		/// SteamServer.OnSteamServersConnected += () =&gt; Log( $"logged on as {SteamServer.SteamId}" );
		/// SteamServer.OnSteamServerConnectFailure += ( result, stillRetrying ) =&gt;
		///     Log( $"logon failed: {result} (retrying: {stillRetrying})" );
		///
		/// SteamServer.LogOnAnonymous();
		///
		/// // ... run the game ...
		///
		/// SteamServer.LogOff();
		/// SteamServer.Shutdown();
		/// </code>
		/// </example>
		public static void Init( AppId appid, SteamServerInit init, bool asyncCallbacks = true )
		{
			if ( IsValid )
				throw new System.Exception( "Calling SteamServer.Init but is already initialized" );

			uint ipaddress = 0; // Any Port

			if ( init.IpAddress != null )
				ipaddress = Utility.IpToInt32( init.IpAddress );

			System.Environment.SetEnvironmentVariable( "SteamAppId", appid.ToString() );
			System.Environment.SetEnvironmentVariable( "SteamGameId", appid.ToString() );
			var secure = (int)(init.Secure ? 3 : 2);

			//
			// Get other interfaces
			//
			var interfaceVersions = Helpers.BuildVersionString(
				ISteamGameServer.Version,
				ISteamUtils.Version,
				ISteamNetworking.Version,
				ISteamGameServerStats.Version,
				ISteamInventory.Version,
				ISteamUGC.Version,
				ISteamApps.Version,
				ISteamNetworkingUtils.Version,
				ISteamNetworkingSockets.Version,
				ISteamNetworkingMessages.Version );
			var result = SteamInternal.GameServer_Init( ipaddress, init.GamePort, init.QueryPort, secure, init.VersionString, interfaceVersions, out var error );
			if ( result != SteamAPIInitResult.OK )
			{
				throw new System.Exception( $"InitGameServer({ipaddress},{init.GamePort},{init.QueryPort},{secure},\"{init.VersionString}\") returned false - error: {error}" );
			}

			//
			// Dispatch is responsible for pumping the event loop.
			//
			Dispatch.Init();
			Dispatch.ServerPipe = SteamGameServer.GetHSteamPipe();

			AddInterface<SteamServer>();
			AddInterface<SteamUtils>();
			AddInterface<SteamNetworking>();
			AddInterface<SteamServerStats>();
			//AddInterface<ISteamHTTP>();
			AddInterface<SteamInventory>();
			AddInterface<SteamUGC>();
			AddInterface<SteamApps>();

			AddInterface<SteamNetworkingUtils>();
			AddInterface<SteamNetworkingSockets>();
			AddInterface<SteamNetworkingMessages>();

			//
			// Initial settings
			//
			AdvertiseServer = true;
			MaxPlayers = 32;
			BotCount = 0;
			Product = $"{appid.Value}";
			ModDir = init.ModDir;
			GameDescription = init.GameDescription;
			Passworded = false;
			DedicatedServer = init.DedicatedServer;

			if ( asyncCallbacks )
			{
				//
				// This will keep looping in the background every 16 ms
				// until we shut down.
				//
				Dispatch.LoopServerAsync();
			}
		}

		internal static void AddInterface<T>() where T : SteamClass, new()
		{
			var t = new T();
			t.InitializeInterface( true );
			openInterfaces.Add( t );
		}

		static readonly List<SteamClass> openInterfaces = new List<SteamClass>();

		internal static void ShutdownInterfaces()
		{
			foreach ( var e in openInterfaces )
			{
				e.DestroyInterface( true );
			}

			openInterfaces.Clear();
		}

		/// <summary>
		/// Tears the server down: stops the callback pump, destroys every interface opened by
		/// <see cref="Init"/>, and shuts the native game server down. Call this before the process exits so
		/// the master server sees a clean departure instead of a timeout.
		/// </summary>
		/// <remarks>
		/// <para>
		/// This does not log off first. Call <see cref="LogOff"/> beforehand if you want the logout to be
		/// orderly; otherwise the connection simply disappears from Steam's point of view.
		/// </para>
		/// <para>
		/// Shutting down the dispatcher also unregisters every server-side callback handler, so events such
		/// as <see cref="OnValidateAuthTicketResponse"/> stop firing immediately - any auth session still
		/// open at this point never gets a verdict. Note that, unlike <c>SteamClient.Shutdown</c>, this does
		/// not return early when <see cref="IsValid"/> is <see langword="false"/> - it always calls through
		/// to the native shutdown, so guard the call yourself rather than relying on it to be idempotent.
		/// </para>
		/// </remarks>
		public static void Shutdown()
		{
			Dispatch.ShutdownServer();

			ShutdownInterfaces();
			SteamGameServer.Shutdown();
		}

		/// <summary>
		/// Pumps the server callback queue once. Every event on this class, and the completion of every
		/// awaitable call, happens inside this - nothing is delivered until it runs.
		/// </summary>
		/// <remarks>
		/// You only need to call this if you initialized with <c>asyncCallbacks: false</c>; otherwise the
		/// background loop started by <see cref="Init"/> already calls it about every 32 ms, and calling it
		/// as well is harmless because a re-entrant call returns immediately. It is a no-op before
		/// <see cref="Init"/> and after <see cref="Shutdown"/>, since both leave the server pipe at zero -
		/// so forgetting it produces silence rather than an error.
		/// </remarks>
		public static void RunCallbacks()
		{
			if ( Dispatch.ServerPipe != 0 )
			{
				Dispatch.Frame( Dispatch.ServerPipe );
			}
		}

		/// <summary>
		/// Whether Steam should treat this as a dedicated server rather than a listen server hosted inside
		/// a player's game client. It is one of the four properties Valve requires to be established before
		/// login, which is why the setter is internal and the value comes from
		/// <see cref="SteamServerInit.DedicatedServer"/>.
		/// </summary>
		/// <value>
		/// <see langword="true"/> for a dedicated server, <see langword="false"/> for a listen server.
		/// Defaults to <see langword="false"/> in Valve's API; <see cref="SteamServerInit"/> defaults it to
		/// <see langword="true"/> instead.
		/// </value>
		/// <remarks>
		/// Valve groups this with the basic server data that must be set before <see cref="LogOn"/> and may
		/// not be changed once logged in. The setter here short-circuits when the value is unchanged, so a
		/// redundant assignment after login is silently harmless - but a genuine change after login is not
		/// supported by Steam, and this binding does nothing to stop you attempting it.
		/// </remarks>
		public static bool DedicatedServer
		{
			get => _dedicatedServer;
			set { if ( _dedicatedServer == value ) return; Internal.SetDedicatedServer( value ); _dedicatedServer = value; }
		}
		private static bool _dedicatedServer;

		/// <summary>
		/// The player capacity advertised to the server browser and to client queries. Purely cosmetic
		/// reporting - Steam will not turn anyone away, so your own connection handler still has to enforce
		/// the limit.
		/// </summary>
		/// <value>
		/// Slot count as it should appear in the browser. <see cref="Init"/> sets it to 32. Valve puts this
		/// in the "server state" group, so it may be changed at any time, before or after login.
		/// </value>
		/// <remarks>
		/// The getter returns this binding's cached value rather than querying Steam, and the setter skips
		/// the native call when the value is unchanged. The cache is <see langword="static"/> and is not
		/// reset by <see cref="Shutdown"/>, so if you shut down and initialize again in the same process,
		/// assigning the value it already holds will not reach the new interface. Assign a different value
		/// first if you need to force it through. The same pattern applies to every cached property on this
		/// class.
		/// </remarks>
		public static int MaxPlayers
		{
			get => _maxplayers;
			set { if ( _maxplayers == value ) return; Internal.SetMaxPlayerCount( value ); _maxplayers = value; }
		}
		private static int _maxplayers = 0;

		/// <summary>
		/// How many of the players on this server are bots. The server browser subtracts these from the
		/// player count so humans looking for a populated server are not misled by an AI-filled lobby.
		/// </summary>
		/// <value>
		/// Number of AI players. Valve's default is zero and this binding leaves it there. Server state, so
		/// it may be changed at any time - keep it in step with your actual bot roster rather than setting
		/// it once at startup.
		/// </value>
		/// <remarks>
		/// Reporting only: nothing is enforced, and nothing validates this against
		/// <see cref="MaxPlayers"/>. Cached and change-suppressed exactly like <see cref="MaxPlayers"/> -
		/// note that <see cref="Init"/>'s assignment of <c>0</c> matches the initial cached value, so it
		/// never actually reaches Steam. That is harmless only because zero is already Valve's default.
		/// </remarks>
		public static int BotCount
		{
			get => _botcount;
			set { if ( _botcount == value ) return; Internal.SetBotPlayerCount( value ); _botcount = value; }
		}
		private static int _botcount = 0;

		/// <summary>
		/// The level currently being played, as shown in the "Map" column of the Steam server browser and
		/// used by browser-side map filters. Update it every time you change level, or the browser keeps
		/// advertising the previous one and your server stops matching players' filters.
		/// </summary>
		/// <value>
		/// The map identifier your game uses - typically the level file name without a path or extension.
		/// Valve's only documented bound is a reference to <c>k_cbMaxGameServerMapName</c>, which is 32
		/// bytes; the browser record this feeds stores it in a fixed 32-byte array, so assume anything
		/// longer is truncated rather than rejected. Starts <see langword="null"/> until you set it.
		/// </value>
		/// <remarks>
		/// Server state, so it may be changed at any time, before or after login. Cached and
		/// change-suppressed like the other reporting properties.
		/// </remarks>
		public static string MapName
		{
			get => _mapname;
			set { if ( _mapname == value ) return; Internal.SetMapName( value ); _mapname = value; }
		}
		private static string _mapname;

		/// <summary>
		/// Identifies which mod of the app this server is running. Valve's default is the empty string,
		/// meaning "this is the original game, not a mod"; the server browser groups and filters servers by
		/// this value, so clients running a different mod string will not see you in their list.
		/// </summary>
		/// <value>
		/// The mod's folder name only - not a path - matching the directory the game installs into, for
		/// example <c>"rust"</c> or <c>"garrysmod"</c>. Valve references <c>k_cbMaxGameServerGameDir</c>,
		/// which is 32 bytes. Taken from <see cref="SteamServerInit.ModDir"/>; the setter is internal
		/// because Valve requires this to be established before login and forbids changing it afterwards.
		/// </value>
		/// <remarks>
		/// Whatever you put here has to match what clients expect, and clients derive their expectation from
		/// their own install. A mismatch is not an error - your server simply never appears in their browser,
		/// which is the most common cause of "my dedicated server is invisible".
		/// </remarks>
		public static string ModDir
		{
			get => _modDir; 
			internal set { if ( _modDir == value ) return; Internal.SetModDir( value ); _modDir = value; }
		}
		private static string _modDir = "";

		/// <summary>
		/// The game product identifier the master server uses for version checking. Valve describes it as a
		/// required field that will eventually be replaced by the AppID - and this binding has already
		/// pre-empted that: <see cref="Init"/> sets it to the decimal AppID string, so on a normal setup you
		/// never need to think about it.
		/// </summary>
		/// <value>
		/// A short product string. Here it is always the AppID rendered in decimal, for example
		/// <c>"252490"</c>, because <see cref="Init"/> assigns <c>appid.Value.ToString()</c>. Empty until
		/// <see cref="Init"/> runs.
		/// </value>
		/// <remarks>
		/// One of the four properties Valve requires to be set before <see cref="LogOn"/> and which may not
		/// change afterwards, hence the internal setter. Distinct from <see cref="GameDescription"/>, which
		/// is human-readable text for the browser - this one is an identifier the master server matches on,
		/// so it is not a place for a display name.
		/// </remarks>
		public static string Product
		{
			get => _product;
			internal set { if ( _product == value ) return; Internal.SetProduct( value ); _product = value; }
		}
		private static string _product = "";

		/// <summary>
		/// Human-readable description of the game, displayed in the Steam server browser. Valve recommends
		/// the full name of your game. Despite sitting next to <see cref="Product"/>, this is display text,
		/// not an identifier.
		/// </summary>
		/// <value>
		/// The description as players should read it. The browser record stores it in a 64-byte field
		/// (<c>k_cbMaxGameServerGameDescription</c>), so keep it short. Taken from
		/// <see cref="SteamServerInit.GameDescription"/>; empty until <see cref="Init"/> runs.
		/// </value>
		/// <remarks>
		/// Required by Valve and one of the properties that must be established before <see cref="LogOn"/>
		/// and not changed after, which is why the setter is internal. If you want per-session text that
		/// changes - a game mode, a rotation name - use <see cref="ServerName"/>, <see cref="GameTags"/>, or
		/// <see cref="SetKey"/> instead; those are server state and may change at any time.
		/// </remarks>
		public static string GameDescription
		{
			get => _gameDescription;
			internal set { if ( _gameDescription == value ) return; Internal.SetGameDescription( value ); _gameDescription = value; }
		}
		private static string _gameDescription = "";

		/// <summary>
		/// The server's display name - the headline row in the Steam server browser and what players search
		/// on. This is the one string most operators actually want to configure.
		/// </summary>
		/// <value>
		/// Free text. Valve references <c>k_cbMaxGameServerName</c>, which is 64 bytes, and the browser
		/// record uses a fixed field of that size, so assume longer names are truncated. Empty until you
		/// set it - <see cref="Init"/> does not assign a default, so a server that never sets this shows up
		/// nameless.
		/// </value>
		/// <remarks>
		/// Server state: safe to change at any time, before or after login, and a good place for live
		/// information such as the current game mode. Cached and change-suppressed like the other reporting
		/// properties.
		/// </remarks>
		public static string ServerName
		{
			get => _serverName;
			set { if ( _serverName == value ) return; Internal.SetServerName( value ); _serverName = value; }
		}
		private static string _serverName = "";

		/// <summary>
		/// Advertises whether joining requires a password, so the browser can show the padlock and honour
		/// the "hide passworded servers" filter. This is advertisement only - Steam never sees or checks
		/// your password, and enforcing it remains entirely your job.
		/// </summary>
		/// <value>
		/// <see langword="true"/> to report the server as password protected. <see cref="Init"/> sets it to
		/// <see langword="false"/>. Server state, so it may be changed at any time - flip it the moment your
		/// password does, or the browser advertises a lock that is not there (or, worse, no lock on a server
		/// nobody can get into).
		/// </value>
		public static bool Passworded
		{
			get => _passworded;
			set { if ( _passworded == value ) return; Internal.SetPasswordProtected( value ); _passworded = value; }
		}
		private static bool _passworded;

		/// <summary>
		/// Comma-separated tags describing this server, used by matchmaking and by the server browser's tag
		/// filters. Optional, but it is the mechanism by which players find the kind of server they want -
		/// region, ruleset, wipe schedule, modded or vanilla.
		/// </summary>
		/// <value>
		/// A comma-separated list such as <c>"pve,monthly,eu"</c>. Valve references
		/// <c>k_cbMaxGameServerTags</c>, which is 128 bytes for the whole list including separators, so
		/// budget accordingly - the browser record uses a fixed field of that size and there is no error if
		/// you exceed it. Empty by default.
		/// </value>
		/// <remarks>
		/// Server state: changeable at any time. There is no add or remove - you always assign the complete
		/// list, so read-modify-write if you are toggling one tag. Meanings are entirely your own
		/// convention; Steam only string-matches. For richer structured data that shows up in rules queries
		/// rather than the tag filter, use <see cref="SetKey"/>.
		/// </remarks>
		public static string GameTags
		{
			get => _gametags;
			set
			{
				if ( _gametags == value ) return;
				Internal.SetGameTags( value );
				_gametags = value;
			}
		}
		private static string _gametags = "";

		/// <summary>
		/// The Steam account identity this server is logged on as. Clients need it to bind an auth ticket
		/// to this specific server, so it is the value you publish alongside your connection info.
		/// </summary>
		/// <value>
		/// The server's <c>SteamId</c>, queried from Steam on every read. It is not assigned until
		/// login completes, so reading it immediately after <see cref="LogOnAnonymous"/> gives you a
		/// placeholder - wait for <see cref="OnSteamServersConnected"/>, or for
		/// <see cref="OnSteamNetAuthenticationStatus"/> to report availability, before publishing it.
		/// </value>
		public static SteamId SteamId => Internal.GetSteamID();

		/// <summary>
		/// Begins logging the server on to Steam with a throwaway anonymous account. This is what most
		/// dedicated servers use: it gets you listed and able to authenticate players, without a persistent
		/// game-server account, at the cost of no persistent identity or reputation between restarts.
		/// </summary>
		/// <remarks>
		/// <para>
		/// Asynchronous and non-blocking. It returns immediately having started the process; success arrives
		/// on <see cref="OnSteamServersConnected"/> and failure on
		/// <see cref="OnSteamServerConnectFailure"/>, which carries a <see cref="Result"/> and a
		/// still-retrying flag. There is no return value and no exception, so a server that never subscribes
		/// to those events has no way to know whether it is on Steam at all - poll <see cref="LoggedOn"/> if
		/// you prefer.
		/// </para>
		/// <para>
		/// Valve notes this used to happen automatically inside the native init and no longer does, so
		/// forgetting to call it is a live failure mode: the server runs, accepts connections, and is simply
		/// never listed. Everything Valve classes as basic server data - <see cref="Product"/>,
		/// <see cref="GameDescription"/>, <see cref="ModDir"/>, <see cref="DedicatedServer"/> - must already
		/// be set, which <see cref="Init"/> handles.
		/// </para>
		/// </remarks>
		public static void LogOnAnonymous()
		{
			Internal.LogOnAnonymous();
			ForceHeartbeat();
		}

		/// <summary>
		/// Begins logging the server on to a persistent game server account, giving it a stable identity
		/// across restarts. Use this instead of <see cref="LogOnAnonymous"/> when you need the server to be
		/// consistently recognisable - favourites that survive a restart, reputation, or accounts tied to
		/// your app in the partner site.
		/// </summary>
		/// <param name="token">
		/// The game server login token issued for your app on the Steamworks partner site. It is a
		/// credential: it identifies your server account, it is per-server, and it should be treated like a
		/// secret rather than committed alongside your config.
		/// </param>
		/// <remarks>
		/// Asynchronous and non-blocking, with exactly the same result path as
		/// <see cref="LogOnAnonymous"/>: <see cref="OnSteamServersConnected"/> on success,
		/// <see cref="OnSteamServerConnectFailure"/> on failure. Nothing here validates the token, so an
		/// expired or revoked one is reported only through that failure event.
		/// </remarks>
		public static void LogOn(string token)
		{
			Internal.LogOn( token );
			ForceHeartbeat();
		}

		/// <summary>
		/// Begins logging the server out of Steam, removing it from the master server list so it stops
		/// appearing in the browser. Call this before <see cref="Shutdown"/> when you want a clean
		/// departure rather than letting the listing time out.
		/// </summary>
		/// <remarks>
		/// <para>
		/// Like the login calls this only starts the process - Valve describes it as "begin process of
		/// logging game server out of steam". It does not block, it returns nothing, and there is no
		/// completion callback documented for it, so there is no supported way to wait for it to finish;
		/// <see cref="LoggedOn"/> going <see langword="false"/> is the only observable signal.
		/// </para>
		/// <para>
		/// It does not disconnect anyone. Players already connected stay connected on your own transport,
		/// and it is your responsibility to end their auth sessions with <see cref="EndSession"/> - Steam
		/// stops being able to vouch for them once the server is logged out.
		/// </para>
		/// <para>
		/// The interface stays alive afterwards, so logging back on with <see cref="LogOnAnonymous"/> or
		/// <see cref="LogOn"/> without re-running <see cref="Init"/> is possible. Valve does not document
		/// the log-off then log-on cycle, and this binding does nothing special for it - if you need a
		/// guaranteed-clean state, prefer <see cref="Shutdown"/> and a fresh <see cref="Init"/>, keeping in
		/// mind the property-cache caveat noted on <see cref="MaxPlayers"/>.
		/// </para>
		/// </remarks>
		public static void LogOff()
		{
			Internal.LogOff();
		}

		/// <summary>
		/// Returns true if the server is connected and registered with the Steam master server
		/// You should have called <see cref="LogOnAnonymous"/> etc on startup.
		/// </summary>
		/// <value>
		/// <see langword="true"/> while the server has a live connection to Steam. Queried from Steam on
		/// every read rather than cached, so it does reflect a connection dropped after login. It is the
		/// only polling alternative to the <see cref="OnSteamServersConnected"/> /
		/// <see cref="OnSteamServersDisconnected"/> events, but it cannot tell you why you are not logged on
		/// - "never called <see cref="LogOnAnonymous"/>", "still connecting", and "was connected and lost
		/// it" all read <see langword="false"/>.
		/// </value>
		public static bool LoggedOn => Internal.BLoggedOn();

		/// <summary>
		/// To the best of its ability this tries to get the server's
		/// current public IP address. Be aware that this is likely to return
		/// <see langword="null"/> for the first few seconds after initialization.
		/// </summary>
		/// <value>
		/// The address Steam believes this server is reachable at from the outside - useful for advertising
		/// a NAT-ed server in a lobby - or <see langword="null"/> while Steam does not yet know. A
		/// <see langword="null"/> here is a "not yet", not a permanent failure: it typically resolves once
		/// the server has logged on, so read it after <see cref="OnSteamServersConnected"/> rather than
		/// immediately after <see cref="Init"/>.
		/// </value>
		public static System.Net.IPAddress PublicIp => Internal.GetPublicIP();

		/// <summary>
		/// Enable or disable heartbeats, which are sent regularly to the master server.
		/// Enabled by default.
		/// </summary>
		/// <value>
		/// Identical in effect to <see cref="AdvertiseServer"/> - the same underlying call, kept under the
		/// old name. Valve renamed it because "heartbeats" described the mechanism rather than the effect,
		/// which is being listed and answering discovery packets. Prefer <see cref="AdvertiseServer"/>.
		/// </value>
		[Obsolete( "Renamed to AdvertiseServer in 1.52" )]
		public static bool AutomaticHeartbeats
		{
			set { Internal.SetAdvertiseServerActive( value ); }
		}		
		
		
		/// <summary>
		/// Controls whether this server is listed on the master server list and answers server-browser and
		/// LAN discovery packets. Set it to <see langword="false"/> to make the server invisible - a private
		/// match, a warm-up before the server is ready for players, or a maintenance window - without
		/// logging off or dropping anyone.
		/// </summary>
		/// <value>
		/// <see langword="true"/> to be listed and discoverable. Valve's native default is
		/// <see langword="false"/>; <see cref="Init"/> turns it on for you, which is why the docs elsewhere
		/// describe it as enabled by default.
		/// </value>
		/// <remarks>
		/// Write-only - there is no getter here and Valve exposes no query, so track the state yourself if
		/// you need it. Valve's guidance is to set all relevant server parameters before enabling
		/// advertisement; <see cref="Init"/> enables it before applying the rest of its defaults, so if that
		/// ordering matters to you, set this <see langword="false"/> immediately after <see cref="Init"/>,
		/// configure everything, then set it back. This is the same underlying call as the obsolete
		/// <c>AutomaticHeartbeats</c>, which was only ever renamed.
		/// </remarks>
		public static bool AdvertiseServer
		{
			set { Internal.SetAdvertiseServerActive( value ); }
		}

		/// <summary>
		/// Force send a heartbeat to the master server instead of waiting
		/// for the next automatic update (if you've left them enabled)
		/// </summary>
		/// <remarks>
		/// This body is empty - it does nothing at all. Valve moved the corresponding native call into the
		/// deprecated, private section of the interface, so there is no way to force a heartbeat any more
		/// and no replacement to point you at. It is still called internally by
		/// <see cref="LogOnAnonymous"/> and <see cref="LogOn"/>, which is harmless but means those calls do
		/// not do what the name suggests either. If your server is not appearing, look at
		/// <see cref="AdvertiseServer"/>, <see cref="ModDir"/>, and
		/// <see cref="SteamServerInit.VersionString"/> instead.
		/// </remarks>
		[Obsolete( "No longer used" )]
		public static void ForceHeartbeat()
		{
		}

		/// <summary>
		/// Update this connected player's information. You should really call this
		/// any time a player's name or score changes. This keeps the information shown
		/// to server queries up to date.
		/// </summary>
		/// <param name="steamid">
		/// The connected player to update. Valve requires this to be a player already validated on this
		/// server - for a real user, only after their auth session has succeeded. Passing an ID that is not
		/// an active player makes the call fail.
		/// </param>
		/// <param name="name">
		/// The display name to show in server queries. This is your game's notion of the player's name, not
		/// necessarily their Steam persona name, so it is where a nickname or clan tag belongs.
		/// </param>
		/// <param name="score">
		/// The player's score as it should appear in the browser's player list. Signed here for
		/// convenience but cast to an unsigned value before it reaches Steam, so a negative score wraps to a
		/// very large positive number rather than being rejected. Clamp at zero if your game allows negative
		/// scores.
		/// </param>
		/// <remarks>
		/// Valve's underlying call returns whether the update succeeded - false when the ID was not an
		/// active player - but this binding discards it, so a mis-timed call (before authentication
		/// completed, or after the player left) fails silently and the browser just keeps showing stale
		/// data.
		/// </remarks>
		public static void UpdatePlayer( SteamId steamid, string name, int score )
		{
			Internal.BUpdateUserData( steamid, name, (uint)score );
		}

		static Dictionary<string, string> KeyValue = new Dictionary<string, string>();

		/// <summary>
		/// Sets a Key Value. These can be anything you like, and are accessible
		/// when querying servers from the server list.
		/// 
		/// Information describing gamemodes are common here.
		/// </summary>
		/// <param name="Key">
		/// The rule name. Setting the same key again replaces its value rather than adding a duplicate.
		/// There is no remove - <see cref="ClearKeys"/> wipes everything, so a rule you no longer want has
		/// to be set to an empty value or the whole set rebuilt.
		/// </param>
		/// <param name="Value">
		/// The rule value, as a string. Everything is a string here; there is no typing, so numbers and
		/// booleans are whatever convention you and your query client agree on.
		/// </param>
		/// <remarks>
		/// These appear in rules queries against the server, which is a different channel from
		/// <see cref="GameTags"/>: tags drive the browser's built-in filtering, rules carry richer detail
		/// for clients that ask. This binding keeps its own dictionary of what it has sent and skips the
		/// native call when a key is set to the value it already holds. That cache is
		/// <see langword="static"/> and is not cleared by <see cref="Shutdown"/>, so after a re-
		/// <see cref="Init"/> the values it still remembers will not be re-sent to the new interface - call
		/// <see cref="ClearKeys"/> first to resynchronise.
		/// </remarks>
		public static void SetKey( string Key, string Value )
		{
			if ( KeyValue.ContainsKey( Key ) )
			{
				if ( KeyValue[Key] == Value )
					return;

				KeyValue[Key] = Value;
			}
			else
			{
				KeyValue.Add( Key, Value );
			}

			Internal.SetKeyValue( Key, Value );
		}

		/// <summary>
		/// Drops every key/value rule sent with <see cref="SetKey"/>, both from Steam and from this
		/// binding's cache. Use it when the rule set changes wholesale - a new round or game mode - rather
		/// than trying to overwrite stale keys individually, since there is no per-key remove.
		/// </summary>
		/// <remarks>
		/// Clearing the local cache as well is what makes this the right way to resynchronise after a
		/// <see cref="Shutdown"/> and re-<see cref="Init"/>: without it, <see cref="SetKey"/> would suppress
		/// re-sending values it thinks are already set.
		/// </remarks>
		public static void ClearKeys()
		{
			KeyValue.Clear();
			Internal.ClearAllKeyValues();
		}

		/// <summary>
		/// Starts verifying an auth ticket a joining client sent you. Steam checks that the ticket is
		/// genuine, that it belongs to the claimed account, that the account owns the app, and that it is not
		/// VAC banned - then reports the verdict asynchronously on
		/// <see cref="OnValidateAuthTicketResponse"/>. This is the server half of the ticket round trip
		/// begun on the client by <see cref="SteamUser.GetAuthSessionTicket"/>.
		/// </summary>
		/// <param name="data">
		/// The raw ticket bytes exactly as the client produced them - <c>AuthTicket.Data</c> transported
		/// verbatim, not truncated, re-encoded, or padded. Must not be <see langword="null"/>. Ticket sizes
		/// vary, so size your receive buffer from the message length rather than assuming a constant.
		/// </param>
		/// <param name="steamid">
		/// The account the client claims to be. Steam validates the ticket against it, so an impersonation
		/// attempt is caught here rather than being yours to detect.
		/// </param>
		/// <returns>
		/// <see langword="true"/> only if Steam accepted the ticket for tracking. It is not an
		/// authorization result, and treating it as one is the classic mistake: the real verdict arrives
		/// later on <see cref="OnValidateAuthTicketResponse"/>. <see langword="false"/> means no session was
		/// started and no callback will ever arrive, so the client must be rejected immediately. All the
		/// distinct reasons - invalid, expired, wrong app, or a duplicate request because you never called
		/// <see cref="EndSession"/> for a previous connection - are collapsed into that one
		/// <see langword="false"/> here; use <see cref="SteamUser.BeginAuthSession"/>, which returns the
		/// <see cref="BeginAuthResult"/> itself, if you need to log which.
		/// </returns>
		/// <remarks>
		/// Every <see langword="true"/> must be matched by <see cref="EndSession"/> when the player leaves,
		/// on every exit path. Stay subscribed to <see cref="OnValidateAuthTicketResponse"/> for the whole
		/// session too: it fires again to revoke a player mid-game with values such as
		/// <see cref="AuthResponse.VACBanned"/> or <see cref="AuthResponse.AuthTicketCanceled"/>, which is
		/// what happens when a client cancels its ticket or is banned while connected.
		/// </remarks>
		/// <example>
		/// <code>
		/// SteamServer.OnValidateAuthTicketResponse += ( steamid, ownerid, response ) =&gt;
		/// {
		///     if ( response != AuthResponse.OK )
		///     {
		///         Kick( steamid, response.ToString() );  // also fires mid-session on a ban
		///         return;
		///     }
		///
		///     // ownerid differs from steamid when the game is borrowed via Family Sharing -
		///     // check DLC entitlements against ownerid, not steamid.
		///     if ( SteamServer.UserHasLicenseForApp( ownerid, dlcAppId ) == UserHasLicenseForAppResult.HasLicense )
		///         GrantDlcContent( steamid );
		///
		///     Admit( steamid );
		/// };
		///
		/// void OnClientJoined( SteamId steamid, byte[] ticketBytes )
		/// {
		///     if ( !SteamServer.BeginAuthSession( ticketBytes, steamid ) )
		///         Kick( steamid, "bad ticket" );  // no callback is coming
		/// }
		///
		/// void OnClientLeft( SteamId steamid )
		/// {
		///     SteamServer.EndSession( steamid );  // required on every exit path
		/// }
		/// </code>
		/// </example>
		public static unsafe bool BeginAuthSession( byte[] data, SteamId steamid )
		{
			fixed ( byte* p = data )
			{
				var result = Internal.BeginAuthSession( (IntPtr)p, data.Length, steamid );

				if ( result == BeginAuthResult.OK )
					return true;

				return false;
			}
		}

		/// <summary>
		/// Stops tracking the auth session started by <see cref="BeginAuthSession"/>. Call it whenever the
		/// player leaves for any reason - clean disconnect, timeout, kick, or a failed authentication - not
		/// only on an orderly quit.
		/// </summary>
		/// <param name="steamid">
		/// The account whose session should be closed. Must be the ID you passed to
		/// <see cref="BeginAuthSession"/>.
		/// </param>
		/// <remarks>
		/// No return value and no error path: calling it for an ID with no open session does nothing.
		/// Omitting it is the problem - Steam keeps counting the player as being on this server, and their
		/// next join is refused as a duplicate request, which this binding surfaces only as
		/// <see cref="BeginAuthSession"/> returning <see langword="false"/>. Because the reason is not
		/// distinguishable there, a missing <see cref="EndSession"/> typically shows up as "some players
		/// cannot rejoin after a crash" rather than as anything obviously auth-related. This does not cancel
		/// the client's ticket; that is the client's own responsibility.
		/// </remarks>
		public static void EndSession( SteamId steamid )
		{
			Internal.EndAuthSession( steamid );
		}

		/// <summary>
		/// Collects one server-browser packet Steam wants sent, for use in GameSocketShare mode - the mode
		/// you are in when <see cref="SteamServerInit.QueryPort"/> was set to the shared sentinel via
		/// <see cref="SteamServerInit.WithQueryShareGamePort"/>. In that mode Steam opens no socket of its
		/// own, so you must relay its query traffic over your game socket. If you use a separate query port,
		/// you never call this.
		/// </summary>
		/// <param name="packet">
		/// Receives the packet to send. Send it connectionlessly to <c>packet.Address</c> (a packed IPv4
		/// address in host order) and <c>packet.Port</c>. Only the first <c>packet.Size</c> bytes of
		/// <c>packet.Data</c> are meaningful - the array is a large pooled buffer, so send the prefix, and
		/// copy it if you will not send immediately, because the next pooled call may reuse it. Set to
		/// <c>default</c> when there is nothing to send.
		/// </param>
		/// <returns>
		/// <see langword="true"/> if a packet was produced. Call it repeatedly until it returns
		/// <see langword="false"/>; a single call per frame drains only one packet and lets the rest queue
		/// up, which shows as your server responding sluggishly to browser pings.
		/// </returns>
		/// <remarks>
		/// Ordering matters: Valve requires that you feed in every inbound query for the frame with
		/// <c>HandleIncomingPacket</c> first, and only then drain this until it is empty. A zero-length
		/// packet is indistinguishable from "no more packets" in Valve's own API, which is why the
		/// drain-until-false loop is the contract rather than a suggestion.
		/// </remarks>
		public static unsafe bool GetOutgoingPacket( out OutgoingPacket packet )
		{
			var buffer = Helpers.TakeBuffer( 1024 * 32 );
			packet = new OutgoingPacket();

			fixed ( byte* ptr = buffer )
			{
				uint addr = 0;
				ushort port = 0;

				var size = Internal.GetNextOutgoingPacket( (IntPtr)ptr, buffer.Length, ref addr, ref port );
				if ( size == 0 )
					return false;

				packet.Size = size;
				packet.Data = buffer;
				packet.Address = addr;
				packet.Port = port;
				return true;
			}
		}

		/// <summary>
		/// Hands Steam an inbound server-browser query that arrived on your game socket, for GameSocketShare
		/// mode. Valve's rule for spotting one: a UDP datagram whose first four bytes are <c>0xFFFFFFFF</c>
		/// is Steam's, not yours.
		/// </summary>
		/// <param name="data">
		/// The datagram as received, starting at the <c>0xFFFFFFFF</c> prefix - do not strip the header.
		/// </param>
		/// <param name="size">
		/// Bytes of <paramref name="data"/> that were actually received. Pass the receive length, not
		/// <c>data.Length</c>, when you are reusing a fixed receive buffer.
		/// </param>
		/// <param name="address">
		/// Source IPv4 address, packed into 32 bits in host order - so 127.0.0.1 is <c>0x7F000001</c>. Steam
		/// replies to this address, so byte-swapped input means the reply goes to the wrong host and the
		/// query simply times out.
		/// </param>
		/// <param name="port">Source port, in host order. Steam addresses its reply to it.</param>
		/// <remarks>
		/// Valve's underlying call reports whether the packet was accepted; this binding discards it, so a
		/// malformed or misrouted packet is dropped silently. After handling every inbound query for the
		/// frame, drain <see cref="GetOutgoingPacket"/> until it returns <see langword="false"/>.
		/// </remarks>
		public static unsafe void HandleIncomingPacket( byte[] data, int size, uint address, ushort port )
		{
			fixed ( byte* ptr = data )
			{
				HandleIncomingPacket( (IntPtr)ptr, size, address, port );
			}
		}
		
		/// <summary>
		/// Pointer form of the inbound server-query handler, for when the datagram is already in unmanaged
		/// or pinned memory and you want to avoid a copy.
		/// </summary>
		/// <param name="ptr">
		/// Pointer to the datagram, starting at its <c>0xFFFFFFFF</c> prefix. Must stay valid for the
		/// duration of the call. Not null-checked.
		/// </param>
		/// <param name="size">Bytes available at <paramref name="ptr"/>.</param>
		/// <param name="address">Source IPv4 address packed into 32 bits, host order.</param>
		/// <param name="port">Source port, host order.</param>
		/// <remarks>
		/// Same silent-failure caveat as the array overload: the accepted/rejected result from Steam is
		/// discarded.
		/// </remarks>
		public static unsafe void HandleIncomingPacket( IntPtr ptr, int size, uint address, ushort port )
		{
			Internal.HandleIncomingPacket( ptr, size, address, port );
		}		
		
		/// <summary>
		/// Asks Steam whether a player owns a given app - in practice, whether they own a piece of DLC, so
		/// the server can gate content instead of trusting the client to tell the truth about what it
		/// bought.
		/// </summary>
		/// <param name="steamid">
		/// The account to check. For a Family Shared game this must be the licence owner - the second
		/// <c>SteamId</c> from <see cref="OnValidateAuthTicketResponse"/> - not the player, or you
		/// will wrongly deny entitlements the borrower legitimately has access to.
		/// </param>
		/// <param name="appid">
		/// The app or DLC to test. Only apps related to the one this server is running are meaningful here.
		/// </param>
		/// <returns>
		/// <see cref="UserHasLicenseForAppResult.HasLicense"/> or
		/// <see cref="UserHasLicenseForAppResult.DoesNotHaveLicense"/> when Steam could answer, and
		/// <see cref="UserHasLicenseForAppResult.NoAuth"/> when it could not - which is a distinct third
		/// state, not a "no". Valve's precondition is that the user's ticket has already been passed to
		/// <see cref="BeginAuthSession"/>, so <see cref="UserHasLicenseForAppResult.NoAuth"/> is what you get
		/// for calling before authentication has completed. Do not collapse it into a denial; retry after
		/// the auth callback instead.
		/// </returns>
		/// <remarks>
		/// Synchronous and cheap - Steam answers from data it already holds for the authenticated session,
		/// so there is no callback and no call result here.
		/// </remarks>
		public static UserHasLicenseForAppResult UserHasLicenseForApp( SteamId steamid, AppId appid )
		{
			return Internal.UserHasLicenseForApp( steamid, appid );
		}
		
		/// <summary>
		/// Mints an auth ticket proving this server's identity to something that wants to authenticate it -
		/// a peer server in a mesh, a relay, or a backend service. This is the server-side mirror of
		/// <see cref="SteamUser.GetAuthSessionTicket"/>, not the thing you validate clients with; for that
		/// see <see cref="BeginAuthSession"/>.
		/// </summary>
		/// <param name="identity">
		/// Who the ticket is being issued to. Binding it to a <see cref="SteamId"/> or a
		/// <see cref="NetAddress"/> restricts redemption to that account or that IP, which is what stops a
		/// ticket being relayed somewhere it was not meant for. Passing <c>default</c> produces an unbound
		/// ticket.
		/// </param>
		/// <returns>
		/// The ticket, or <see langword="null"/> if Steam refused to issue one - most often because the
		/// server is not logged on yet. It is <see cref="System.IDisposable"/> and owns a live Steam handle,
		/// so it must be cancelled or disposed when you are done; dropping it leaks the session.
		/// </returns>
		/// <remarks>
		/// <para>
		/// The staging buffer here is 1024 bytes, where the client-side equivalent uses 2560. Valve does not
		/// document a maximum ticket size for this call, so if a server ticket ever exceeds 1024 bytes the
		/// underlying call would fail and this would return <see langword="null"/> with no further
		/// explanation.
		/// </para>
		/// <para>
		/// As on the client, the ticket is returned before Steam has confirmed it with the backend. There is
		/// no async variant on this class, so if you need certainty before sending, wait for the ticket
		/// response callback yourself.
		/// </para>
		/// </remarks>
		public static unsafe AuthTicket GetAuthSessionTicket( NetIdentity identity )
		{
			var data = Helpers.TakeBuffer( 1024 );

			fixed ( byte* b = data )
			{
				uint ticketLength = 0;
				uint ticket = Internal.GetAuthSessionTicket( (IntPtr)b, data.Length, ref ticketLength, ref identity );

				if ( ticket == 0 )
					return null;

				return new AuthTicket()
				{
					Data = data.Take( (int)ticketLength ).ToArray(),
					Handle = ticket
				};
			}
		}
	}
}
