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
			public static extern bool SteamInternal_GameServer_Init( uint unIP, ushort usPort, ushort usGamePort, ushort usQueryPort, int eServerMode, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8String ) )] string pchVersionString );
			
			[DllImport( "steam_api64", EntryPoint = "SteamInternal_FindOrCreateUserInterface", CallingConvention = CallingConvention.Cdecl )]
			public static extern IntPtr SteamInternal_FindOrCreateUserInterface( int steamuser, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8String ) )] string versionname );
			
			[DllImport( "steam_api64", EntryPoint = "SteamInternal_FindOrCreateGameServerInterface", CallingConvention = CallingConvention.Cdecl )]
			public static extern IntPtr SteamInternal_FindOrCreateGameServerInterface( int steamuser, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8String ) )] string versionname );
			
			[DllImport( "steam_api64", EntryPoint = "SteamInternal_CreateInterface", CallingConvention = CallingConvention.Cdecl )]
			public static extern IntPtr SteamInternal_CreateInterface( [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8String ) )] string version );
			
		}
		internal static class Posix
		{
			[DllImport( "libsteam_api", EntryPoint = "SteamInternal_GameServer_Init", CallingConvention = CallingConvention.Cdecl )]
			[return: MarshalAs( UnmanagedType.I1 )]
			public static extern bool SteamInternal_GameServer_Init( uint unIP, ushort usPort, ushort usGamePort, ushort usQueryPort, int eServerMode, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8String ) )] string pchVersionString );
			
			[DllImport( "libsteam_api", EntryPoint = "SteamInternal_FindOrCreateUserInterface", CallingConvention = CallingConvention.Cdecl )]
			public static extern IntPtr SteamInternal_FindOrCreateUserInterface( int steamuser, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8String ) )] string versionname );
			
			[DllImport( "libsteam_api", EntryPoint = "SteamInternal_FindOrCreateGameServerInterface", CallingConvention = CallingConvention.Cdecl )]
			public static extern IntPtr SteamInternal_FindOrCreateGameServerInterface( int steamuser, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8String ) )] string versionname );
			
			[DllImport( "libsteam_api", EntryPoint = "SteamInternal_CreateInterface", CallingConvention = CallingConvention.Cdecl )]
			public static extern IntPtr SteamInternal_CreateInterface( [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8String ) )] string version );
			
		}
		static internal bool GameServer_Init( uint unIP, ushort usPort, ushort usGamePort, ushort usQueryPort, int eServerMode, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8String ) )] string pchVersionString )
		{
			if ( Config.Os == OsType.Windows )
			{
				return Win64.SteamInternal_GameServer_Init( unIP, usPort, usGamePort, usQueryPort, eServerMode, pchVersionString );
			}
			else if ( Config.Os == OsType.Posix )
			{
				return Posix.SteamInternal_GameServer_Init( unIP, usPort, usGamePort, usQueryPort, eServerMode, pchVersionString );
			}
			else
			{
				throw new System.Exception( "this platform isn't supported" );
			}
		}
		
		static internal IntPtr FindOrCreateUserInterface( int steamuser, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8String ) )] string versionname )
		{
			if ( Config.Os == OsType.Windows )
			{
				return Win64.SteamInternal_FindOrCreateUserInterface( steamuser, versionname );
			}
			else if ( Config.Os == OsType.Posix )
			{
				return Posix.SteamInternal_FindOrCreateUserInterface( steamuser, versionname );
			}
			else
			{
				throw new System.Exception( "this platform isn't supported" );
			}
		}
		
		static internal IntPtr FindOrCreateGameServerInterface( int steamuser, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8String ) )] string versionname )
		{
			if ( Config.Os == OsType.Windows )
			{
				return Win64.SteamInternal_FindOrCreateGameServerInterface( steamuser, versionname );
			}
			else if ( Config.Os == OsType.Posix )
			{
				return Posix.SteamInternal_FindOrCreateGameServerInterface( steamuser, versionname );
			}
			else
			{
				throw new System.Exception( "this platform isn't supported" );
			}
		}
		
		static internal IntPtr CreateInterface( [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8String ) )] string version )
		{
			if ( Config.Os == OsType.Windows )
			{
				return Win64.SteamInternal_CreateInterface( version );
			}
			else if ( Config.Os == OsType.Posix )
			{
				return Posix.SteamInternal_CreateInterface( version );
			}
			else
			{
				throw new System.Exception( "this platform isn't supported" );
			}
		}
		
	}
}
