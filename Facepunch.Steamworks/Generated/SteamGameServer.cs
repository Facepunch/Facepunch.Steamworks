using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Steamworks.Data;


namespace Steamworks
{
	internal static class SteamGameServer
	{
		internal static class Native
		{
			[DllImport( Platform.LibraryName, EntryPoint = "SteamGameServer_RunCallbacks", CallingConvention = CallingConvention.Cdecl )]
			public static extern void SteamGameServer_RunCallbacks();
			
			[DllImport( Platform.LibraryName, EntryPoint = "SteamGameServer_Shutdown", CallingConvention = CallingConvention.Cdecl )]
			public static extern void SteamGameServer_Shutdown();
			
			[DllImport( Platform.LibraryName, EntryPoint = "SteamGameServer_GetHSteamUser", CallingConvention = CallingConvention.Cdecl )]
			public static extern HSteamUser SteamGameServer_GetHSteamUser();
			
			[DllImport( Platform.LibraryName, EntryPoint = "SteamGameServer_GetHSteamPipe", CallingConvention = CallingConvention.Cdecl )]
			public static extern HSteamPipe SteamGameServer_GetHSteamPipe();
			
		}
		static internal void RunCallbacks()
		{
			Native.SteamGameServer_RunCallbacks();
		}
		
		static internal void Shutdown()
		{
			Native.SteamGameServer_Shutdown();
		}
		
		static internal HSteamUser GetHSteamUser()
		{
			return Native.SteamGameServer_GetHSteamUser();
		}
		
		static internal HSteamPipe GetHSteamPipe()
		{
			return Native.SteamGameServer_GetHSteamPipe();
		}
		
	}
}
