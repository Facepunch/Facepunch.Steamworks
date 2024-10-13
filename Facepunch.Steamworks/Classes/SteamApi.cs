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

		public static SteamAPIInitResult InitEx( out string outSteamErrMsg )
		{
			var steamErrorMsgPtr = Marshal.AllocHGlobal( Defines.k_cchMaxSteamErrMsg );
			
			var pszInternalCheckInterfaceVersions = new System.Text.StringBuilder();
			pszInternalCheckInterfaceVersions.Append( Defines.STEAMUTILS_INTERFACE_VERSION ).Append( "\0" );
			pszInternalCheckInterfaceVersions.Append( Defines.STEAMNETWORKINGUTILS_INTERFACE_VERSION ).Append( "\0" );
			pszInternalCheckInterfaceVersions.Append( Defines.STEAMAPPS_INTERFACE_VERSION ).Append( "\0" );
			// pszInternalCheckInterfaceVersions.Append( "SteamController008" ).Append( "\0" ); // ISteamController is deprecated in favor of ISteamInput.
			pszInternalCheckInterfaceVersions.Append( Defines.STEAMFRIENDS_INTERFACE_VERSION ).Append( "\0" );
			pszInternalCheckInterfaceVersions.Append( Defines.STEAMGAMESEARCH_INTERFACE_VERSION ).Append( "\0" );
			pszInternalCheckInterfaceVersions.Append( Defines.STEAMHTMLSURFACE_INTERFACE_VERSION ).Append( "\0" );
			pszInternalCheckInterfaceVersions.Append( Defines.STEAMHTTP_INTERFACE_VERSION ).Append( "\0" );
			pszInternalCheckInterfaceVersions.Append( Defines.STEAMINPUT_INTERFACE_VERSION ).Append( "\0" );
			pszInternalCheckInterfaceVersions.Append( Defines.STEAMINVENTORY_INTERFACE_VERSION ).Append( "\0" );
			pszInternalCheckInterfaceVersions.Append( Defines.STEAMMATCHMAKINGSERVERS_INTERFACE_VERSION ).Append( "\0" );
			pszInternalCheckInterfaceVersions.Append( Defines.STEAMMATCHMAKING_INTERFACE_VERSION ).Append( "\0" );
			pszInternalCheckInterfaceVersions.Append( Defines.STEAMMUSICREMOTE_INTERFACE_VERSION ).Append( "\0" );
			pszInternalCheckInterfaceVersions.Append( Defines.STEAMMUSIC_INTERFACE_VERSION ).Append( "\0" );
			pszInternalCheckInterfaceVersions.Append( Defines.STEAMNETWORKINGMESSAGES_INTERFACE_VERSION ).Append( "\0" );
			pszInternalCheckInterfaceVersions.Append( Defines.STEAMNETWORKINGSOCKETS_INTERFACE_VERSION ).Append( "\0" );
			pszInternalCheckInterfaceVersions.Append( Defines.STEAMNETWORKING_INTERFACE_VERSION ).Append( "\0" );
			pszInternalCheckInterfaceVersions.Append( Defines.STEAMPARENTALSETTINGS_INTERFACE_VERSION ).Append( "\0" );
			pszInternalCheckInterfaceVersions.Append( Defines.STEAMPARTIES_INTERFACE_VERSION ).Append( "\0" );
			pszInternalCheckInterfaceVersions.Append( Defines.STEAMREMOTEPLAY_INTERFACE_VERSION ).Append( "\0" );
			pszInternalCheckInterfaceVersions.Append( Defines.STEAMREMOTESTORAGE_INTERFACE_VERSION ).Append( "\0" );
			pszInternalCheckInterfaceVersions.Append( Defines.STEAMSCREENSHOTS_INTERFACE_VERSION ).Append( "\0" );
			pszInternalCheckInterfaceVersions.Append( Defines.STEAMUGC_INTERFACE_VERSION ).Append( "\0" );
			pszInternalCheckInterfaceVersions.Append( Defines.STEAMUSERSTATS_INTERFACE_VERSION ).Append( "\0" );
			pszInternalCheckInterfaceVersions.Append( Defines.STEAMUSER_INTERFACE_VERSION ).Append( "\0" );
			pszInternalCheckInterfaceVersions.Append( Defines.STEAMVIDEO_INTERFACE_VERSION ).Append( "\0" );

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
