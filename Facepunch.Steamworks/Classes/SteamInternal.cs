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
		}

		static internal bool GameServer_Init( uint unIP, ushort usPort, ushort usGamePort, ushort usQueryPort, int eServerMode, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchVersionString )
		{
			return Native.SteamInternal_GameServer_Init( unIP, usPort, usGamePort, usQueryPort, eServerMode, pchVersionString );
		}		
	}
}
