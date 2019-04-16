using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Steamworks
{
	public static class SteamClient
	{
		static bool initialized;

		public static void Init( uint appid )
		{
			if ( IntPtr.Size != 8 )
			{
				throw new System.Exception( "Only 64bit processes are currently supported" );
			}

			System.Environment.SetEnvironmentVariable( "SteamAppId", appid.ToString() );
			System.Environment.SetEnvironmentVariable( "SteamGameId", appid.ToString() );

			if ( !SteamApi.SteamAPI_Init() )
			{
				throw new System.Exception( "SteamApi_Init returned false. Steam isn't running, couldn't find Steam, AppId is ureleased, Don't own AppId." );
			}

			initialized = true;

			SteamApps.InstallEvents();
			SteamUtils.InstallEvents();
			SteamParental.InstallEvents();
			SteamMusic.InstallEvents();
			SteamVideo.InstallEvents();
			SteamUser.InstallEvents();
			SteamFriends.InstallEvents();

			RunCallbacks();
		}

		public static bool IsValid => initialized;

		internal static async void RunCallbacks()
		{
			while ( true )
			{
				await Task.Delay( 16 );
				try
				{
					SteamApi.SteamAPI_RunCallbacks();
				}
				catch ( System.Exception )
				{
					// TODO - error outputs
				}
			}
		}

		public static void Shutdown()
		{
			// TODO.
		}

		internal static void RegisterCallback( IntPtr intPtr, int callbackId )
		{
			SteamApi.RegisterCallback( intPtr, callbackId );
		}

		public static void Update()
		{
			SteamApi.SteamAPI_RunCallbacks();
		}

		internal static void UnregisterCallback( IntPtr intPtr )
		{
			SteamApi.UnregisterCallback( intPtr );
		}
	}
}