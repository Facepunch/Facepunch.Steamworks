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

			[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_Shutdown", CallingConvention = CallingConvention.Cdecl )]
			public static extern void SteamAPI_Shutdown();
						
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
		
		static internal void Shutdown()
		{
			Native.SteamAPI_Shutdown();
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
