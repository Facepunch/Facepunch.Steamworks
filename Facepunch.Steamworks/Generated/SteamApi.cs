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
		static internal bool Init()
		{
			return Win64.SteamAPI_Init();
		}
		
		static internal void RunCallbacks()
		{
			Win64.SteamAPI_RunCallbacks();
		}
		
		static internal void RegisterCallback( IntPtr pCallback, int callback )
		{
			Win64.SteamAPI_RegisterCallback( pCallback, callback );
		}
		
		static internal void UnregisterCallback( IntPtr pCallback )
		{
			Win64.SteamAPI_UnregisterCallback( pCallback );
		}
		
		static internal void RegisterCallResult( IntPtr pCallback, SteamAPICall_t callback )
		{
			Win64.SteamAPI_RegisterCallResult( pCallback, callback );
		}
		
		static internal void UnregisterCallResult( IntPtr pCallback, SteamAPICall_t callback )
		{
			Win64.SteamAPI_UnregisterCallResult( pCallback, callback );
		}
		
		static internal void Shutdown()
		{
			Win64.SteamAPI_Shutdown();
		}
		
		static internal HSteamUser GetHSteamUser()
		{
			return Win64.SteamAPI_GetHSteamUser();
		}
		
		static internal HSteamPipe GetHSteamPipe()
		{
			return Win64.SteamAPI_GetHSteamPipe();
		}
		
		static internal bool RestartAppIfNecessary( uint unOwnAppID )
		{
			return Win64.SteamAPI_RestartAppIfNecessary( unOwnAppID );
		}
		
	}
}
