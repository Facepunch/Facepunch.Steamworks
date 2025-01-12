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
			[DllImport( Platform.LibraryName, EntryPoint = "SteamInternal_SteamAPI_Init", CallingConvention = CallingConvention.Cdecl )]
			public static extern SteamAPIInitResult SteamAPI_Init(
				[MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )]
				string pszInternalCheckInterfaceVersions,
				IntPtr pOutErrMsg );

			[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_Shutdown", CallingConvention = CallingConvention.Cdecl )]
			public static extern void SteamAPI_Shutdown();
						
			[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_GetHSteamPipe", CallingConvention = CallingConvention.Cdecl )]
			public static extern HSteamPipe SteamAPI_GetHSteamPipe();
			
			[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_RestartAppIfNecessary", CallingConvention = CallingConvention.Cdecl )]
			[return: MarshalAs( UnmanagedType.I1 )]
			public static extern bool SteamAPI_RestartAppIfNecessary( uint unOwnAppID );
		}

		static internal SteamAPIInitResult InitEx( out string outSteamErrMsg )
		{
			var steamErrorMsgPtr = Marshal.AllocHGlobal( Defines.k_cchMaxSteamErrMsg );
			
			var pszInternalCheckInterfaceVersions = new StringBuilder();
			pszInternalCheckInterfaceVersions.Append( SteamApiVersions.STEAMUTILS_INTERFACE_VERSION ).Append( "\0" );
			pszInternalCheckInterfaceVersions.Append( SteamApiVersions.STEAMNETWORKINGUTILS_INTERFACE_VERSION ).Append( "\0" );
			pszInternalCheckInterfaceVersions.Append( SteamApiVersions.STEAMAPPS_INTERFACE_VERSION ).Append( "\0" );
			// pszInternalCheckInterfaceVersions.Append( "SteamController008" ).Append( "\0" ); // ISteamController is deprecated in favor of ISteamInput.
			pszInternalCheckInterfaceVersions.Append( SteamApiVersions.STEAMFRIENDS_INTERFACE_VERSION ).Append( "\0" );
			pszInternalCheckInterfaceVersions.Append( SteamApiVersions.STEAMGAMESEARCH_INTERFACE_VERSION ).Append( "\0" );
			pszInternalCheckInterfaceVersions.Append( SteamApiVersions.STEAMHTMLSURFACE_INTERFACE_VERSION ).Append( "\0" );
			pszInternalCheckInterfaceVersions.Append( SteamApiVersions.STEAMHTTP_INTERFACE_VERSION ).Append( "\0" );
			pszInternalCheckInterfaceVersions.Append( SteamApiVersions.STEAMINPUT_INTERFACE_VERSION ).Append( "\0" );
			pszInternalCheckInterfaceVersions.Append( SteamApiVersions.STEAMINVENTORY_INTERFACE_VERSION ).Append( "\0" );
			pszInternalCheckInterfaceVersions.Append( SteamApiVersions.STEAMMATCHMAKINGSERVERS_INTERFACE_VERSION ).Append( "\0" );
			pszInternalCheckInterfaceVersions.Append( SteamApiVersions.STEAMMATCHMAKING_INTERFACE_VERSION ).Append( "\0" );
			pszInternalCheckInterfaceVersions.Append( SteamApiVersions.STEAMMUSICREMOTE_INTERFACE_VERSION ).Append( "\0" );
			pszInternalCheckInterfaceVersions.Append( SteamApiVersions.STEAMMUSIC_INTERFACE_VERSION ).Append( "\0" );
			pszInternalCheckInterfaceVersions.Append( SteamApiVersions.STEAMNETWORKINGMESSAGES_INTERFACE_VERSION ).Append( "\0" );
			pszInternalCheckInterfaceVersions.Append( SteamApiVersions.STEAMNETWORKINGSOCKETS_INTERFACE_VERSION ).Append( "\0" );
			pszInternalCheckInterfaceVersions.Append( SteamApiVersions.STEAMNETWORKING_INTERFACE_VERSION ).Append( "\0" );
			pszInternalCheckInterfaceVersions.Append( SteamApiVersions.STEAMPARENTALSETTINGS_INTERFACE_VERSION ).Append( "\0" );
			pszInternalCheckInterfaceVersions.Append( SteamApiVersions.STEAMPARTIES_INTERFACE_VERSION ).Append( "\0" );
			pszInternalCheckInterfaceVersions.Append( SteamApiVersions.STEAMREMOTEPLAY_INTERFACE_VERSION ).Append( "\0" );
			pszInternalCheckInterfaceVersions.Append( SteamApiVersions.STEAMREMOTESTORAGE_INTERFACE_VERSION ).Append( "\0" );
			pszInternalCheckInterfaceVersions.Append( SteamApiVersions.STEAMSCREENSHOTS_INTERFACE_VERSION ).Append( "\0" );
			pszInternalCheckInterfaceVersions.Append( SteamApiVersions.STEAMUGC_INTERFACE_VERSION ).Append( "\0" );
			pszInternalCheckInterfaceVersions.Append( SteamApiVersions.STEAMUSERSTATS_INTERFACE_VERSION ).Append( "\0" );
			pszInternalCheckInterfaceVersions.Append( SteamApiVersions.STEAMUSER_INTERFACE_VERSION ).Append( "\0" );
			pszInternalCheckInterfaceVersions.Append( SteamApiVersions.STEAMVIDEO_INTERFACE_VERSION ).Append( "\0" );

			var initResult = Native.SteamAPI_Init( pszInternalCheckInterfaceVersions.ToString(), steamErrorMsgPtr );

			outSteamErrMsg = new Utf8StringPointer { ptr = steamErrorMsgPtr };

			return initResult;
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
