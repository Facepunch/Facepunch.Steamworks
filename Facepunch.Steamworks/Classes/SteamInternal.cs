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
			[DllImport( Platform.LibraryName, EntryPoint = "SteamInternal_GameServer_Init_V2",
				CallingConvention = CallingConvention.Cdecl )]
			public static extern SteamAPIInitResult SteamInternal_GameServer_Init_V2( uint unIP, ushort usGamePort, ushort usQueryPort, ServerMode eServerMode,
				[MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchVersionString, 
				[MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pszInternalCheckInterfaceVersions,
				IntPtr pOutErrMsg );
		}

		static internal bool GameServer_Init( uint unIP, ushort usGamePort,
			ushort usQueryPort, ServerMode eServerMode,
			[MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )]
			string pchVersionString )
		{
			var pszInternalCheckInterfaceVersions = new StringBuilder();
			pszInternalCheckInterfaceVersions.Append( SteamApiVersions.STEAMUTILS_INTERFACE_VERSION ).Append( '\0' );
			pszInternalCheckInterfaceVersions.Append( SteamApiVersions.STEAMNETWORKINGUTILS_INTERFACE_VERSION ).Append( '\0' );
			pszInternalCheckInterfaceVersions.Append( SteamApiVersions.STEAMGAMESERVER_INTERFACE_VERSION ).Append( '\0' );
			pszInternalCheckInterfaceVersions.Append( SteamApiVersions.STEAMGAMESERVERSTATS_INTERFACE_VERSION ).Append( '\0' );
			pszInternalCheckInterfaceVersions.Append( SteamApiVersions.STEAMHTTP_INTERFACE_VERSION ).Append( '\0' );
			pszInternalCheckInterfaceVersions.Append( SteamApiVersions.STEAMINVENTORY_INTERFACE_VERSION ).Append( '\0' );
			pszInternalCheckInterfaceVersions.Append( SteamApiVersions.STEAMNETWORKING_INTERFACE_VERSION ).Append( '\0' );
			pszInternalCheckInterfaceVersions.Append( SteamApiVersions.STEAMNETWORKINGMESSAGES_INTERFACE_VERSION ).Append( '\0' );
			pszInternalCheckInterfaceVersions.Append( SteamApiVersions.STEAMNETWORKINGSOCKETS_INTERFACE_VERSION ).Append( '\0' );
			pszInternalCheckInterfaceVersions.Append( SteamApiVersions.STEAMUGC_INTERFACE_VERSION ).Append( '\0' );
			
			var steamErrorMsgPtr = Marshal.AllocHGlobal( Defines.k_cchMaxSteamErrMsg );
			
			var result = Native.SteamInternal_GameServer_Init_V2( unIP, usGamePort, usQueryPort, eServerMode, pchVersionString, pszInternalCheckInterfaceVersions.ToString(), steamErrorMsgPtr );

			var steamErrorMsg = Helpers.PtrToStringUTF8( steamErrorMsgPtr );

			Marshal.FreeHGlobal( steamErrorMsgPtr );

			if ( result != SteamAPIInitResult.OK )
			{
				throw new Exception( $"Failed to initialize error: {steamErrorMsg}, result: {result}, ServerMode: {eServerMode}" );
			}

			return true;
		}
	}
}
