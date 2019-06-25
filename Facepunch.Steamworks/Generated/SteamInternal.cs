using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Steamworks.Data;


namespace Steamworks
{
	internal static class SteamInternal
	{
		internal static class Native
		{
			[DllImport( Platform.LibraryName, EntryPoint = "SteamInternal_GameServer_Init", CallingConvention = CallingConvention.Cdecl )]
			[return: MarshalAs( UnmanagedType.I1 )]
			public static extern bool SteamInternal_GameServer_Init( uint unIP, ushort usPort, ushort usGamePort, ushort usQueryPort, int eServerMode, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchVersionString );
			
			[DllImport( Platform.LibraryName, EntryPoint = "SteamInternal_FindOrCreateUserInterface", CallingConvention = CallingConvention.Cdecl )]
			public static extern IntPtr SteamInternal_FindOrCreateUserInterface( int steamuser, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string versionname );
			
			[DllImport( Platform.LibraryName, EntryPoint = "SteamInternal_FindOrCreateGameServerInterface", CallingConvention = CallingConvention.Cdecl )]
			public static extern IntPtr SteamInternal_FindOrCreateGameServerInterface( int steamuser, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string versionname );
			
			[DllImport( Platform.LibraryName, EntryPoint = "SteamInternal_CreateInterface", CallingConvention = CallingConvention.Cdecl )]
			public static extern IntPtr SteamInternal_CreateInterface( [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string version );
			
		}
		static internal bool GameServer_Init( uint unIP, ushort usPort, ushort usGamePort, ushort usQueryPort, int eServerMode, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchVersionString )
		{
			return Native.SteamInternal_GameServer_Init( unIP, usPort, usGamePort, usQueryPort, eServerMode, pchVersionString );
		}
		
		static internal IntPtr FindOrCreateUserInterface( int steamuser, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string versionname )
		{
			return Native.SteamInternal_FindOrCreateUserInterface( steamuser, versionname );
		}
		
		static internal IntPtr FindOrCreateGameServerInterface( int steamuser, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string versionname )
		{
			return Native.SteamInternal_FindOrCreateGameServerInterface( steamuser, versionname );
		}
		
		static internal IntPtr CreateInterface( [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string version )
		{
			return Native.SteamInternal_CreateInterface( version );
		}
		
	}
}
