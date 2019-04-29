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
		static internal void RunCallbacks()
		{
			Win64.SteamGameServer_RunCallbacks();
		}
		
		static internal void Shutdown()
		{
			Win64.SteamGameServer_Shutdown();
		}
		
		static internal HSteamUser GetHSteamUser()
		{
			return Win64.SteamGameServer_GetHSteamUser();
		}
		
		static internal HSteamPipe GetHSteamPipe()
		{
			return Win64.SteamGameServer_GetHSteamPipe();
		}
		
	}
}
