using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Steamworks.Data;


namespace Steamworks
{
	internal static class SteamInternal
	{
		internal static class Win64
		{
			[DllImport( "steam_api64", EntryPoint = "SteamInternal_GameServer_Init", CallingConvention = CallingConvention.Cdecl )]
			[return: MarshalAs( UnmanagedType.I1 )]
			public static extern bool SteamInternal_GameServer_Init( uint unIP, ushort usPort, ushort usGamePort, ushort usQueryPort, int eServerMode, string pchVersionString );
			
			[DllImport( "steam_api64", EntryPoint = "SteamInternal_FindOrCreateUserInterface", CallingConvention = CallingConvention.Cdecl )]
			public static extern IntPtr SteamInternal_FindOrCreateUserInterface( int steamuser, string versionname );
			
			[DllImport( "steam_api64", EntryPoint = "SteamInternal_FindOrCreateGameServerInterface", CallingConvention = CallingConvention.Cdecl )]
			public static extern IntPtr SteamInternal_FindOrCreateGameServerInterface( int steamuser, string versionname );
			
			[DllImport( "steam_api64", EntryPoint = "SteamInternal_CreateInterface", CallingConvention = CallingConvention.Cdecl )]
			public static extern IntPtr SteamInternal_CreateInterface( string version );
			
		}
		static internal bool GameServer_Init( uint unIP, ushort usPort, ushort usGamePort, ushort usQueryPort, int eServerMode, string pchVersionString )
		{
			return Win64.SteamInternal_GameServer_Init( unIP, usPort, usGamePort, usQueryPort, eServerMode, pchVersionString );
		}
		
		static internal IntPtr FindOrCreateUserInterface( int steamuser, string versionname )
		{
			return Win64.SteamInternal_FindOrCreateUserInterface( steamuser, versionname );
		}
		
		static internal IntPtr FindOrCreateGameServerInterface( int steamuser, string versionname )
		{
			return Win64.SteamInternal_FindOrCreateGameServerInterface( steamuser, versionname );
		}
		
		static internal IntPtr CreateInterface( string version )
		{
			return Win64.SteamInternal_CreateInterface( version );
		}
		
	}
}
