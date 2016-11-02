using System;
using System.Runtime.InteropServices;

namespace SteamNative
{
	internal unsafe class SteamUnifiedMessages : IDisposable
	{
		//
		// Holds a platform specific implentation
		//
		internal Platform.Interface platform;
		internal Facepunch.Steamworks.BaseSteamworks steamworks;
		
		//
		// Constructor decides which implementation to use based on current platform
		//
		internal SteamUnifiedMessages( Facepunch.Steamworks.BaseSteamworks steamworks, IntPtr pointer )
		{
			this.steamworks = steamworks;
			
			if ( Platform.IsWindows64 ) platform = new Platform.Win64( pointer );
			else if ( Platform.IsWindows32 ) platform = new Platform.Win32( pointer );
			else if ( Platform.IsLinux32 ) platform = new Platform.Linux32( pointer );
			else if ( Platform.IsLinux64 ) platform = new Platform.Linux64( pointer );
			else if ( Platform.IsOsx ) platform = new Platform.Mac( pointer );
		}
		
		//
		// Class is invalid if we don't have a valid implementation
		//
		public bool IsValid{ get{ return platform != null && platform.IsValid; } }
		
		//
		// When shutting down clear all the internals to avoid accidental use
		//
		public virtual void Dispose()
		{
			 if ( platform != null )
			{
				platform.Dispose();
				platform = null;
			}
		}
		
		// bool
		public bool GetMethodResponseData( ClientUnifiedMessageHandle hHandle /*ClientUnifiedMessageHandle*/, IntPtr pResponseBuffer /*void **/, uint unResponseBufferSize /*uint32*/, bool bAutoRelease /*bool*/ )
		{
			return platform.ISteamUnifiedMessages_GetMethodResponseData( hHandle.Value, (IntPtr) pResponseBuffer, unResponseBufferSize, bAutoRelease );
		}
		
		// bool
		public bool GetMethodResponseInfo( ClientUnifiedMessageHandle hHandle /*ClientUnifiedMessageHandle*/, out uint punResponseSize /*uint32 **/, out Result peResult /*EResult **/ )
		{
			return platform.ISteamUnifiedMessages_GetMethodResponseInfo( hHandle.Value, out punResponseSize, out peResult );
		}
		
		// bool
		public bool ReleaseMethod( ClientUnifiedMessageHandle hHandle /*ClientUnifiedMessageHandle*/ )
		{
			return platform.ISteamUnifiedMessages_ReleaseMethod( hHandle.Value );
		}
		
		// ClientUnifiedMessageHandle
		public ClientUnifiedMessageHandle SendMethod( string pchServiceMethod /*const char **/, IntPtr pRequestBuffer /*const void **/, uint unRequestBufferSize /*uint32*/, ulong unContext /*uint64*/ )
		{
			return platform.ISteamUnifiedMessages_SendMethod( pchServiceMethod, (IntPtr) pRequestBuffer, unRequestBufferSize, unContext );
		}
		
		// bool
		public bool SendNotification( string pchServiceNotification /*const char **/, IntPtr pNotificationBuffer /*const void **/, uint unNotificationBufferSize /*uint32*/ )
		{
			return platform.ISteamUnifiedMessages_SendNotification( pchServiceNotification, (IntPtr) pNotificationBuffer, unNotificationBufferSize );
		}
		
	}
}
