using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Steamworks.Data;

namespace Steamworks
{
	public static class SteamClient
	{
		static bool initialized;

		public static void Init( uint appid )
		{
			System.Environment.SetEnvironmentVariable( "SteamAppId", appid.ToString() );
			System.Environment.SetEnvironmentVariable( "SteamGameId", appid.ToString() );

			if ( !SteamAPI.Init() )
			{
				throw new System.Exception( "SteamApi_Init returned false. Steam isn't running, couldn't find Steam, AppId is ureleased, Don't own AppId." );
			}

			AppId = appid;

			initialized = true;

			SteamApps.InstallEvents();
			SteamUtils.InstallEvents();
			SteamParental.InstallEvents();
			SteamMusic.InstallEvents();
			SteamVideo.InstallEvents();
			SteamUser.InstallEvents();
			SteamFriends.InstallEvents();
			SteamScreenshots.InstallEvents();
			SteamUserStats.InstallEvents();
			SteamInventory.InstallEvents();
			SteamNetworking.InstallEvents();
			SteamMatchmaking.InstallEvents();
			SteamParties.InstallEvents();
			SteamNetworkingSockets.InstallEvents();
			SteamInput.InstallEvents();

			if ( asyncCallbacks )
			{
				RunCallbacksAsync();
			}
		}

		static List<SteamInterface> openIterfaces = new List<SteamInterface>();

		internal static void WatchInterface( SteamInterface steamInterface )
		{
			if ( openIterfaces.Contains( steamInterface ) )
				throw new System.Exception( "openIterfaces already contains interface!" );

			openIterfaces.Add( steamInterface );
		}

		internal static void ShutdownInterfaces()
		{
			foreach ( var e in openIterfaces )
			{
				e.Shutdown();
			}

			openIterfaces.Clear();
		}

		public static Action<Exception> OnCallbackException;

		public static bool IsValid => initialized;

		internal static async void RunCallbacksAsync()
		{
			while ( IsValid )
			{
				await Task.Delay( 16 );

				try
				{
					RunCallbacks();
				}
				catch ( System.Exception e )
				{
					OnCallbackException?.Invoke( e );
				}
			}
		}

		public static void Shutdown()
		{
			if ( !IsValid ) return;

			SteamInput.Shutdown();

			Cleanup();

			SteamAPI.Shutdown();
		}

		internal static void Cleanup()
		{
			initialized = false;

			Event.DisposeAllClient();
			ShutdownInterfaces();

			SteamInput.Shutdown();
			SteamApps.Shutdown();
			SteamUtils.Shutdown();
			SteamParental.Shutdown();
			SteamMusic.Shutdown();
			SteamVideo.Shutdown();
			SteamUser.Shutdown();
			SteamFriends.Shutdown();
			SteamScreenshots.Shutdown();
			SteamUserStats.Shutdown();
			SteamInventory.Shutdown();
			SteamNetworking.Shutdown();
			SteamMatchmaking.Shutdown();
			SteamParties.Shutdown();
			SteamNetworkingUtils.Shutdown();
			SteamNetworkingSockets.Shutdown();
			ServerList.Base.Shutdown();
		}

		internal static void RegisterCallback( IntPtr intPtr, int callbackId )
		{
			SteamAPI.RegisterCallback( intPtr, callbackId );
		}

		public static void RunCallbacks()
		{
			if ( !IsValid ) return;

			SteamAPI.RunCallbacks();
		}

		internal static void UnregisterCallback( IntPtr intPtr )
		{
			SteamAPI.UnregisterCallback( intPtr );
		}

		/// <summary>
		/// Checks if the current user's Steam client is connected to the Steam servers.
		/// If it's not then no real-time services provided by the Steamworks API will be enabled. The Steam 
		/// client will automatically be trying to recreate the connection as often as possible. When the 
		/// connection is restored a SteamServersConnected_t callback will be posted.
		/// You usually don't need to check for this yourself. All of the API calls that rely on this will 
		/// check internally. Forcefully disabling stuff when the player loses access is usually not a 
		/// very good experience for the player and you could be preventing them from accessing APIs that do not 
		/// need a live connection to Steam.
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
		/// returns the local players name - guaranteed to not be NULL.
		/// this is the same name as on the users community profile page
		/// </summary>
		public static string Name => SteamFriends.Internal.GetPersonaName();

		/// <summary>
		/// gets the status of the current user
		/// </summary>
		public static FriendState State => SteamFriends.Internal.GetPersonaState();

		/// <summary>
		/// returns the appID of the current process
		/// </summary>
		public static AppId AppId { get; internal set; }

		/// <summary>
		/// Checks if your executable was launched through Steam and relaunches it through Steam if it wasn't
		///  this returns true then it starts the Steam client if required and launches your game again through it, 
		///  and you should quit your process as soon as possible. This effectively runs steam://run/AppId so it 
		///  may not relaunch the exact executable that called it, as it will always relaunch from the version 
		///  installed in your Steam library folder/
		///  Note that during development, when not launching via Steam, this might always return true.
		/// </summary>
		public static bool RestartAppIfNecessary( uint appid )
		{
			// Having these here would probably mean it always returns false?

			//System.Environment.SetEnvironmentVariable( "SteamAppId", appid.ToString() );
			//System.Environment.SetEnvironmentVariable( "SteamGameId", appid.ToString() );

			return SteamAPI.RestartAppIfNecessary( appid );
		}

	}
}