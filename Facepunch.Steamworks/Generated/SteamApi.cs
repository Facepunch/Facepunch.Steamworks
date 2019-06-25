using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Steamworks.Data;


namespace Steamworks
{
	internal static class SteamAPI
	{
		internal static class Native
		{
			[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_Init", CallingConvention = CallingConvention.Cdecl )]
			[return: MarshalAs( UnmanagedType.I1 )]
			public static extern bool SteamAPI_Init();
			
			[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_RunCallbacks", CallingConvention = CallingConvention.Cdecl )]
			public static extern void SteamAPI_RunCallbacks();
			
			[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_RegisterCallback", CallingConvention = CallingConvention.Cdecl )]
			public static extern void SteamAPI_RegisterCallback( IntPtr pCallback, int callback );
			
			[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_UnregisterCallback", CallingConvention = CallingConvention.Cdecl )]
			public static extern void SteamAPI_UnregisterCallback( IntPtr pCallback );
			
			[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_RegisterCallResult", CallingConvention = CallingConvention.Cdecl )]
			public static extern void SteamAPI_RegisterCallResult( IntPtr pCallback, SteamAPICall_t callback );
			
			[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_UnregisterCallResult", CallingConvention = CallingConvention.Cdecl )]
			public static extern void SteamAPI_UnregisterCallResult( IntPtr pCallback, SteamAPICall_t callback );
			
			[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_Shutdown", CallingConvention = CallingConvention.Cdecl )]
			public static extern void SteamAPI_Shutdown();
			
			[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_GetHSteamUser", CallingConvention = CallingConvention.Cdecl )]
			public static extern HSteamUser SteamAPI_GetHSteamUser();
			
			[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_GetHSteamPipe", CallingConvention = CallingConvention.Cdecl )]
			public static extern HSteamPipe SteamAPI_GetHSteamPipe();
			
			[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_RestartAppIfNecessary", CallingConvention = CallingConvention.Cdecl )]
			[return: MarshalAs( UnmanagedType.I1 )]
			public static extern bool SteamAPI_RestartAppIfNecessary( uint unOwnAppID );
			
		}
		static internal bool Init()
		{
			return Native.SteamAPI_Init();
		}
		
		static internal void RunCallbacks()
		{
			Native.SteamAPI_RunCallbacks();
		}
		
		static internal void RegisterCallback( IntPtr pCallback, int callback )
		{
			Native.SteamAPI_RegisterCallback( pCallback, callback );
		}
		
		static internal void UnregisterCallback( IntPtr pCallback )
		{
			Native.SteamAPI_UnregisterCallback( pCallback );
		}
		
		static internal void RegisterCallResult( IntPtr pCallback, SteamAPICall_t callback )
		{
			Native.SteamAPI_RegisterCallResult( pCallback, callback );
		}
		
		static internal void UnregisterCallResult( IntPtr pCallback, SteamAPICall_t callback )
		{
			Native.SteamAPI_UnregisterCallResult( pCallback, callback );
		}
		
		static internal void Shutdown()
		{
			Native.SteamAPI_Shutdown();
		}
		
		static internal HSteamUser GetHSteamUser()
		{
			return Native.SteamAPI_GetHSteamUser();
		}
		
		static internal HSteamPipe GetHSteamPipe()
		{
			return Native.SteamAPI_GetHSteamPipe();
		}
		
		static internal bool RestartAppIfNecessary( uint unOwnAppID )
		{
			return Native.SteamAPI_RestartAppIfNecessary( unOwnAppID );
		}
		
	}
}
