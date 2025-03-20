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
			[DllImport( Platform.LibraryName, EntryPoint = "SteamInternal_GameServer_Init_V2", CallingConvention = CallingConvention.Cdecl )]
			public static extern SteamAPIInitResult SteamInternal_GameServer_Init_V2( uint unIP, ushort usGamePort, ushort usQueryPort, int eServerMode, IntPtr pchVersionString, IntPtr pszInternalCheckInterfaceVersions, IntPtr pOutErrMsg );	
		}

		static internal SteamAPIInitResult GameServer_Init( uint unIP, ushort usGamePort, ushort usQueryPort, int eServerMode, string pchVersionString, string pszInternalCheckInterfaceVersions, out string pOutErrMsg )
		{
			using var versionStr = new Utf8StringToNative( pchVersionString );
			using var interfaceVersionsStr = new Utf8StringToNative( pszInternalCheckInterfaceVersions );
			using var buffer = Helpers.Memory.Take();
			var result = Native.SteamInternal_GameServer_Init_V2( unIP, usGamePort, usQueryPort, eServerMode, versionStr.Pointer, interfaceVersionsStr.Pointer, buffer.Ptr );
			pOutErrMsg = Helpers.MemoryToString( buffer.Ptr );
			return result;
		}		
	}
}
