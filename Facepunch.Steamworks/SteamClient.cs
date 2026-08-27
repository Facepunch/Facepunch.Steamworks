using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Steamworks.Data;

namespace Steamworks
{
	/// <summary>
	/// Starts and stops the Steam API for a game client, and is the gateway everything else in this
	/// library depends on. Nothing in <c>SteamFriends</c>, <c>SteamUserStats</c>, <c>SteamUGC</c> or
	/// anywhere else works until <see cref="Init"/> has succeeded.
	/// </summary>
	/// <remarks>
	/// <para>
	/// For a <b>dedicated game server</b> use <see cref="SteamServer"/> instead. The two are separate
	/// APIs with separate lifetimes; a server does not init the client.
	/// </para>
	/// <para>
	/// <b>Steam must be running and the user signed in.</b> There is no offline mode &#8212;
	/// <see cref="Init"/> throws rather than degrading, so treat "Steam is not available" as a real
	/// startup path in your game and not an edge case.
	/// </para>
	/// <para>
	/// During development, a build launched outside Steam needs a <c>steam_appid.txt</c> file
	/// containing just your app id next to the executable, or Steam cannot tell which app you are.
	/// Ship without that file.
	/// </para>
	/// </remarks>
	/// <example>
	/// The whole lifecycle. This is the shape almost every game wants:
	/// <code>
	/// try
	/// {
	///     // asyncCallbacks: true means a background thread pumps callbacks for you
	///     SteamClient.Init( 480 );
	/// }
	/// catch ( System.Exception e )
	/// {
	///     // Steam is not running, the user is not signed in, or the app id is wrong.
	///     Console.WriteLine( $"Steam is unavailable: {e.Message}" );
	///     return;
	/// }
	///
	/// Console.WriteLine( $"Hello {SteamClient.Name} ({SteamClient.SteamId})" );
	///
	/// // ... game runs ...
	///
	/// SteamClient.Shutdown();
	/// </code>
	/// If you would rather pump callbacks yourself &#8212; usually to guarantee they arrive on your
	/// main thread &#8212; pass <c>asyncCallbacks: false</c> and call
	/// <see cref="RunCallbacks"/> once a frame:
	/// <code>
	/// SteamClient.Init( 480, asyncCallbacks: false );
	///
	/// while ( running )
	/// {
	///     SteamClient.RunCallbacks();   // omit this and no event or await ever completes
	///     Update();
	/// }
	///
	/// SteamClient.Shutdown();
	/// </code>
	/// </example>
	public static class SteamClient
	{
		static bool initialized;

		/// <summary>
		/// Initialize the steam client. Call this once, before touching any other part of this
		/// library, and pair it with <see cref="Shutdown"/>.
		/// If <paramref name="asyncCallbacks"/> is false you need to call <see cref="RunCallbacks"/> manually every frame.
		/// </summary>
		/// <param name="appid">
		/// Your Steam application id. During development this must match the <c>steam_appid.txt</c>
		/// beside your executable if the game is launched outside Steam. Passing an id the signed-in
		/// user does not own causes this to throw.
		/// </param>
		/// <param name="asyncCallbacks">
		/// When <see langword="true"/> (the default) a background thread pumps Steam callbacks, so
		/// events fire and awaited tasks complete without you doing anything &#8212; but they arrive
		/// on <b>that</b> thread, which matters for engines that require main-thread access.
		/// When <see langword="false"/> nothing is delivered until you call
		/// <see cref="RunCallbacks"/> yourself; forgetting to do so makes every event silently never
		/// fire and every awaited Steam call hang for ever.
		/// </param>
		/// <exception cref="System.Exception">
		/// Thrown if Steam is not running, no user is signed in, the app id is wrong or not owned, or
		/// <see cref="Init"/> has already been called without an intervening <see cref="Shutdown"/>.
		/// The message carries Steam's own failure reason.
		/// </exception>
		/// <remarks>
		/// This sets the <c>SteamAppId</c> and <c>SteamGameId</c> environment variables for the
		/// process as a side effect.
		/// </remarks>
		public static void Init( uint appid, bool asyncCallbacks = true )
		{
			if ( initialized )
				throw new System.Exception( "Calling SteamClient.Init but is already initialized" );

			System.Environment.SetEnvironmentVariable( "SteamAppId", appid.ToString() );
			System.Environment.SetEnvironmentVariable( "SteamGameId", appid.ToString() );

			var interfaceVersions = Helpers.BuildVersionString(
				ISteamApps.Version,
				ISteamFriends.Version,
				ISteamInput.Version,
				ISteamInventory.Version,
				ISteamMatchmaking.Version,
				ISteamMatchmakingServers.Version,
				ISteamMusic.Version,
				ISteamNetworking.Version,
				ISteamNetworkingSockets.Version,
				ISteamNetworkingUtils.Version,
				ISteamParentalSettings.Version,
				ISteamParties.Version,
				ISteamRemoteStorage.Version,
				ISteamScreenshots.Version,
				ISteamUGC.Version,
				ISteamUser.Version,
				ISteamUserStats.Version,
				ISteamUtils.Version,
				ISteamVideo.Version,
				ISteamRemotePlay.Version,
				ISteamTimeline.Version,
				ISteamNetworkingMessages.Version );
			var result = SteamAPI.Init( interfaceVersions, out var error );
			if ( result != SteamAPIInitResult.OK )
			{
				throw new System.Exception( $"SteamApi_Init failed with {result} - error: {error}" );
			}

			AppId = appid;

			initialized = true;

			//
			// Dispatch is responsible for pumping the event loop.
			//
			Dispatch.Init();
			Dispatch.ClientPipe = SteamAPI.GetHSteamPipe();

			// Note: don't forget to add the interface version to SteamAPI.Init above!!!
			AddInterface<SteamApps>();
			AddInterface<SteamFriends>();
			AddInterface<SteamInput>();
			AddInterface<SteamInventory>();
			AddInterface<SteamMatchmaking>();
			AddInterface<SteamMatchmakingServers>();
			AddInterface<SteamMusic>();
			AddInterface<SteamNetworking>();
			AddInterface<SteamNetworkingSockets>();
			AddInterface<SteamNetworkingUtils>();
			AddInterface<SteamParental>();
			AddInterface<SteamParties>();
			AddInterface<SteamRemoteStorage>();
			AddInterface<SteamScreenshots>();
			AddInterface<SteamUGC>();
			AddInterface<SteamUser>();
			AddInterface<SteamUserStats>();
			AddInterface<SteamUtils>();
			AddInterface<SteamVideo>();
			AddInterface<SteamRemotePlay>();
			AddInterface<SteamNetworkingMessages>();
			AddInterface<SteamTimeline>();
			// Note: don't forget to add the interface version to SteamAPI.Init above!!!

			initialized = openInterfaces.Count > 0;

			if ( asyncCallbacks )
			{
				//
				// This will keep looping in the background every 16 ms
				// until we shut down.
				//
				Dispatch.LoopClientAsync();
			}
		}

		internal static void AddInterface<T>() where T : SteamClass, new()
		{
			var t = new T();
			bool valid = t.InitializeInterface( false );
			if ( valid )
			{
				openInterfaces.Add( t );
			}
			else
			{
				t.DestroyInterface( false );
			}
		}

		static readonly List<SteamClass> openInterfaces = new List<SteamClass>();

		internal static void ShutdownInterfaces()
		{
			foreach ( var e in openInterfaces )
			{
				e.DestroyInterface( false );
			}

			openInterfaces.Clear();
		}

		/// <summary>
		/// Check if Steam is loaded and accessible.
		/// </summary>		
		public static bool IsValid => initialized;

		/// <summary>
		/// Shuts down the steam client.
		/// </summary>
		public static void Shutdown()
		{
			if ( !IsValid ) return;

			Cleanup();

			SteamAPI.Shutdown();
		}

		internal static void Cleanup()
		{
			Dispatch.ShutdownClient();

			initialized = false;
			ShutdownInterfaces();
		}

		public static void RunCallbacks()
		{
			if ( Dispatch.ClientPipe != 0 )
				Dispatch.Frame( Dispatch.ClientPipe );
		}

		/// <summary>
		/// Checks if the current user's Steam client is connected to the Steam servers.
		/// <para>
		/// If it's not, no real-time services provided by the Steamworks API will be enabled. The Steam 
		/// client will automatically be trying to recreate the connection as often as possible. When the 
		/// connection is restored a SteamServersConnected_t callback will be posted.
		/// You usually don't need to check for this yourself. All of the API calls that rely on this will 
		/// check internally. Forcefully disabling stuff when the player loses access is usually not a 
		/// very good experience for the player and you could be preventing them from accessing APIs that do not 
		/// need a live connection to Steam.
		/// </para>
		/// </summary>
		public static bool IsLoggedOn => SteamUser.Internal.BLoggedOn();

		/// <summary>
		/// Gets the Steam ID of the account currently logged into the Steam client. This is 
		/// commonly called the 'current user', or 'local user'.
		/// A Steam ID is a unique identifier for a Steam accounts, Steam groups, Lobbies and Chat 
		/// rooms, and used to differentiate users in all parts of the Steamworks API.
		/// </summary>
		public static SteamId SteamId => SteamUser.Internal.GetSteamID();

		/// <summary>
		/// returns the local players name - guaranteed to not be <see langword="null"/>.
		/// This is the same name as on the user's community profile page.
		/// </summary>
		public static string Name => SteamFriends.Internal.GetPersonaName();

		/// <summary>
		/// Gets the status of the current user.
		/// </summary>
		public static FriendState State => SteamFriends.Internal.GetPersonaState();

		/// <summary>
		/// Returns the App ID of the current process.
		/// </summary>
		public static AppId AppId { get; internal set; }

		/// <summary>
		/// Checks if your executable was launched through Steam and relaunches it through Steam if it wasn't.
		/// <para>
		///  This returns true then it starts the Steam client if required and launches your game again through it, 
		///  and you should quit your process as soon as possible. This effectively runs steam://run/AppId so it 
		///  may not relaunch the exact executable that called it, as it will always relaunch from the version 
		///  installed in your Steam library folder/
		///  Note that during development, when not launching via Steam, this might always return true.
		///  </para>
		/// </summary>
		public static bool RestartAppIfNecessary( uint appid )
		{
			// Having these here would probably mean it always returns false?

			//System.Environment.SetEnvironmentVariable( "SteamAppId", appid.ToString() );
			//System.Environment.SetEnvironmentVariable( "SteamGameId", appid.ToString() );

			return SteamAPI.RestartAppIfNecessary( appid );
		}

		/// <summary>
		/// Called in interfaces that rely on this being initialized
		/// </summary>
		internal static void ValidCheck()
		{
			if ( !IsValid )
				throw new System.Exception( "SteamClient isn't initialized" );
		}

	}
}
