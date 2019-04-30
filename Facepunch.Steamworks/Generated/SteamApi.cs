using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Steamworks.Data;


namespace Steamworks
{
	internal static class SteamAPI
	{
		internal static class Win64
		{
			[DllImport( "steam_api64", EntryPoint = "SteamAPI_Init", CallingConvention = CallingConvention.Cdecl )]
			[return: MarshalAs( UnmanagedType.I1 )]
			public static extern bool SteamAPI_Init();
			
			[DllImport( "steam_api64", EntryPoint = "SteamAPI_RunCallbacks", CallingConvention = CallingConvention.Cdecl )]
			public static extern void SteamAPI_RunCallbacks();
			
			[DllImport( "steam_api64", EntryPoint = "SteamAPI_RegisterCallback", CallingConvention = CallingConvention.Cdecl )]
			public static extern void SteamAPI_RegisterCallback( IntPtr pCallback, int callback );
			
			[DllImport( "steam_api64", EntryPoint = "SteamAPI_UnregisterCallback", CallingConvention = CallingConvention.Cdecl )]
			public static extern void SteamAPI_UnregisterCallback( IntPtr pCallback );
			
			[DllImport( "steam_api64", EntryPoint = "SteamAPI_RegisterCallResult", CallingConvention = CallingConvention.Cdecl )]
			public static extern void SteamAPI_RegisterCallResult( IntPtr pCallback, SteamAPICall_t callback );
			
			[DllImport( "steam_api64", EntryPoint = "SteamAPI_UnregisterCallResult", CallingConvention = CallingConvention.Cdecl )]
			public static extern void SteamAPI_UnregisterCallResult( IntPtr pCallback, SteamAPICall_t callback );
			
			[DllImport( "steam_api64", EntryPoint = "SteamAPI_Shutdown", CallingConvention = CallingConvention.Cdecl )]
			public static extern void SteamAPI_Shutdown();
			
			[DllImport( "steam_api64", EntryPoint = "SteamAPI_GetHSteamUser", CallingConvention = CallingConvention.Cdecl )]
			public static extern HSteamUser SteamAPI_GetHSteamUser();
			
			[DllImport( "steam_api64", EntryPoint = "SteamAPI_GetHSteamPipe", CallingConvention = CallingConvention.Cdecl )]
			public static extern HSteamPipe SteamAPI_GetHSteamPipe();
			
			[DllImport( "steam_api64", EntryPoint = "SteamAPI_RestartAppIfNecessary", CallingConvention = CallingConvention.Cdecl )]
			[return: MarshalAs( UnmanagedType.I1 )]
			public static extern bool SteamAPI_RestartAppIfNecessary( uint unOwnAppID );
			
		}
		internal static class Posix
		{
			[DllImport( "libsteam_api", EntryPoint = "SteamAPI_Init", CallingConvention = CallingConvention.Cdecl )]
			[return: MarshalAs( UnmanagedType.I1 )]
			public static extern bool SteamAPI_Init();
			
			[DllImport( "libsteam_api", EntryPoint = "SteamAPI_RunCallbacks", CallingConvention = CallingConvention.Cdecl )]
			public static extern void SteamAPI_RunCallbacks();
			
			[DllImport( "libsteam_api", EntryPoint = "SteamAPI_RegisterCallback", CallingConvention = CallingConvention.Cdecl )]
			public static extern void SteamAPI_RegisterCallback( IntPtr pCallback, int callback );
			
			[DllImport( "libsteam_api", EntryPoint = "SteamAPI_UnregisterCallback", CallingConvention = CallingConvention.Cdecl )]
			public static extern void SteamAPI_UnregisterCallback( IntPtr pCallback );
			
			[DllImport( "libsteam_api", EntryPoint = "SteamAPI_RegisterCallResult", CallingConvention = CallingConvention.Cdecl )]
			public static extern void SteamAPI_RegisterCallResult( IntPtr pCallback, SteamAPICall_t callback );
			
			[DllImport( "libsteam_api", EntryPoint = "SteamAPI_UnregisterCallResult", CallingConvention = CallingConvention.Cdecl )]
			public static extern void SteamAPI_UnregisterCallResult( IntPtr pCallback, SteamAPICall_t callback );
			
			[DllImport( "libsteam_api", EntryPoint = "SteamAPI_Shutdown", CallingConvention = CallingConvention.Cdecl )]
			public static extern void SteamAPI_Shutdown();
			
			[DllImport( "libsteam_api", EntryPoint = "SteamAPI_GetHSteamUser", CallingConvention = CallingConvention.Cdecl )]
			public static extern HSteamUser SteamAPI_GetHSteamUser();
			
			[DllImport( "libsteam_api", EntryPoint = "SteamAPI_GetHSteamPipe", CallingConvention = CallingConvention.Cdecl )]
			public static extern HSteamPipe SteamAPI_GetHSteamPipe();
			
			[DllImport( "libsteam_api", EntryPoint = "SteamAPI_RestartAppIfNecessary", CallingConvention = CallingConvention.Cdecl )]
			[return: MarshalAs( UnmanagedType.I1 )]
			public static extern bool SteamAPI_RestartAppIfNecessary( uint unOwnAppID );
			
		}
		static internal bool Init()
		{
			if ( Config.Os == OsType.Windows )
			{
				return Win64.SteamAPI_Init();
			}
			else if ( Config.Os == OsType.Posix )
			{
				return Posix.SteamAPI_Init();
			}
			else
			{
				throw new System.Exception( "this platform isn't supported" );
			}
		}
		
		static internal void RunCallbacks()
		{
			if ( Config.Os == OsType.Windows )
			{
				Win64.SteamAPI_RunCallbacks();
			}
			else if ( Config.Os == OsType.Posix )
			{
				Posix.SteamAPI_RunCallbacks();
			}
			else
			{
				throw new System.Exception( "this platform isn't supported" );
			}
		}
		
		static internal void RegisterCallback( IntPtr pCallback, int callback )
		{
			if ( Config.Os == OsType.Windows )
			{
				Win64.SteamAPI_RegisterCallback( pCallback, callback );
			}
			else if ( Config.Os == OsType.Posix )
			{
				Posix.SteamAPI_RegisterCallback( pCallback, callback );
			}
			else
			{
				throw new System.Exception( "this platform isn't supported" );
			}
		}
		
		static internal void UnregisterCallback( IntPtr pCallback )
		{
			if ( Config.Os == OsType.Windows )
			{
				Win64.SteamAPI_UnregisterCallback( pCallback );
			}
			else if ( Config.Os == OsType.Posix )
			{
				Posix.SteamAPI_UnregisterCallback( pCallback );
			}
			else
			{
				throw new System.Exception( "this platform isn't supported" );
			}
		}
		
		static internal void RegisterCallResult( IntPtr pCallback, SteamAPICall_t callback )
		{
			if ( Config.Os == OsType.Windows )
			{
				Win64.SteamAPI_RegisterCallResult( pCallback, callback );
			}
			else if ( Config.Os == OsType.Posix )
			{
				Posix.SteamAPI_RegisterCallResult( pCallback, callback );
			}
			else
			{
				throw new System.Exception( "this platform isn't supported" );
			}
		}
		
		static internal void UnregisterCallResult( IntPtr pCallback, SteamAPICall_t callback )
		{
			if ( Config.Os == OsType.Windows )
			{
				Win64.SteamAPI_UnregisterCallResult( pCallback, callback );
			}
			else if ( Config.Os == OsType.Posix )
			{
				Posix.SteamAPI_UnregisterCallResult( pCallback, callback );
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
				Win64.SteamAPI_Shutdown();
			}
			else if ( Config.Os == OsType.Posix )
			{
				Posix.SteamAPI_Shutdown();
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
				return Win64.SteamAPI_GetHSteamUser();
			}
			else if ( Config.Os == OsType.Posix )
			{
				return Posix.SteamAPI_GetHSteamUser();
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
				return Win64.SteamAPI_GetHSteamPipe();
			}
			else if ( Config.Os == OsType.Posix )
			{
				return Posix.SteamAPI_GetHSteamPipe();
			}
			else
			{
				throw new System.Exception( "this platform isn't supported" );
			}
		}
		
		static internal bool RestartAppIfNecessary( uint unOwnAppID )
		{
			if ( Config.Os == OsType.Windows )
			{
				return Win64.SteamAPI_RestartAppIfNecessary( unOwnAppID );
			}
			else if ( Config.Os == OsType.Posix )
			{
				return Posix.SteamAPI_RestartAppIfNecessary( unOwnAppID );
			}
			else
			{
				throw new System.Exception( "this platform isn't supported" );
			}
		}
		
	}
}
