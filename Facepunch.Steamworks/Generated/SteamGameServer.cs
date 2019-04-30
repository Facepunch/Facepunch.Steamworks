using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Steamworks.Data;


namespace Steamworks
{
	internal static class SteamGameServer
	{
		internal static class Win64
		{
			[DllImport( "steam_api64", EntryPoint = "SteamGameServer_RunCallbacks", CallingConvention = CallingConvention.Cdecl )]
			public static extern void SteamGameServer_RunCallbacks();
			
			[DllImport( "steam_api64", EntryPoint = "SteamGameServer_Shutdown", CallingConvention = CallingConvention.Cdecl )]
			public static extern void SteamGameServer_Shutdown();
			
			[DllImport( "steam_api64", EntryPoint = "SteamGameServer_GetHSteamUser", CallingConvention = CallingConvention.Cdecl )]
			public static extern HSteamUser SteamGameServer_GetHSteamUser();
			
			[DllImport( "steam_api64", EntryPoint = "SteamGameServer_GetHSteamPipe", CallingConvention = CallingConvention.Cdecl )]
			public static extern HSteamPipe SteamGameServer_GetHSteamPipe();
			
		}
		internal static class Posix
		{
			[DllImport( "libsteam_api", EntryPoint = "SteamGameServer_RunCallbacks", CallingConvention = CallingConvention.Cdecl )]
			public static extern void SteamGameServer_RunCallbacks();
			
			[DllImport( "libsteam_api", EntryPoint = "SteamGameServer_Shutdown", CallingConvention = CallingConvention.Cdecl )]
			public static extern void SteamGameServer_Shutdown();
			
			[DllImport( "libsteam_api", EntryPoint = "SteamGameServer_GetHSteamUser", CallingConvention = CallingConvention.Cdecl )]
			public static extern HSteamUser SteamGameServer_GetHSteamUser();
			
			[DllImport( "libsteam_api", EntryPoint = "SteamGameServer_GetHSteamPipe", CallingConvention = CallingConvention.Cdecl )]
			public static extern HSteamPipe SteamGameServer_GetHSteamPipe();
			
		}
		static internal void RunCallbacks()
		{
			if ( Config.Os == OsType.Windows )
			{
				Win64.SteamGameServer_RunCallbacks();
			}
			else if ( Config.Os == OsType.Posix )
			{
				Posix.SteamGameServer_RunCallbacks();
			}
			else
			{
				throw new System.Exception( "this platform isn't supported" );
			}
		}
		
		static internal void Shutdown()
		{
			if ( Config.Os == OsType.Windows )
			{
				Win64.SteamGameServer_Shutdown();
			}
			else if ( Config.Os == OsType.Posix )
			{
				Posix.SteamGameServer_Shutdown();
			}
			else
			{
				throw new System.Exception( "this platform isn't supported" );
			}
		}
		
		static internal HSteamUser GetHSteamUser()
		{
			if ( Config.Os == OsType.Windows )
			{
				return Win64.SteamGameServer_GetHSteamUser();
			}
			else if ( Config.Os == OsType.Posix )
			{
				return Posix.SteamGameServer_GetHSteamUser();
			}
			else
			{
				throw new System.Exception( "this platform isn't supported" );
			}
		}
		
		static internal HSteamPipe GetHSteamPipe()
		{
			if ( Config.Os == OsType.Windows )
			{
				return Win64.SteamGameServer_GetHSteamPipe();
			}
			else if ( Config.Os == OsType.Posix )
			{
				return Posix.SteamGameServer_GetHSteamPipe();
			}
			else
			{
				throw new System.Exception( "this platform isn't supported" );
			}
		}
		
	}
}
