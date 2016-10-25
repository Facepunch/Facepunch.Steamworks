using System;
using System.Runtime.InteropServices;

namespace SteamNative
{
	public unsafe class SteamUnifiedMessages
	{
		internal IntPtr _ptr;
		
		public SteamUnifiedMessages( IntPtr pointer )
		{
			_ptr = pointer;
		}
		
		
		// bool
		public bool GetMethodResponseData( ClientUnifiedMessageHandle hHandle /*ClientUnifiedMessageHandle*/, IntPtr pResponseBuffer /*void **/, uint unResponseBufferSize /*uint32*/, bool bAutoRelease /*bool*/ )
		{
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamUnifiedMessages.GetMethodResponseData( _ptr, hHandle, (IntPtr) pResponseBuffer, unResponseBufferSize, bAutoRelease );
			else return Platform.Win64.ISteamUnifiedMessages.GetMethodResponseData( _ptr, hHandle, (IntPtr) pResponseBuffer, unResponseBufferSize, bAutoRelease );
		}
		
		// bool
		public bool GetMethodResponseInfo( ClientUnifiedMessageHandle hHandle /*ClientUnifiedMessageHandle*/, out uint punResponseSize /*uint32 **/, out Result peResult /*EResult **/ )
		{
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamUnifiedMessages.GetMethodResponseInfo( _ptr, hHandle, out punResponseSize, out peResult );
			else return Platform.Win64.ISteamUnifiedMessages.GetMethodResponseInfo( _ptr, hHandle, out punResponseSize, out peResult );
		}
		
		// bool
		public bool ReleaseMethod( ClientUnifiedMessageHandle hHandle /*ClientUnifiedMessageHandle*/ )
		{
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamUnifiedMessages.ReleaseMethod( _ptr, hHandle );
			else return Platform.Win64.ISteamUnifiedMessages.ReleaseMethod( _ptr, hHandle );
		}
		
		// ClientUnifiedMessageHandle
		public ClientUnifiedMessageHandle SendMethod( string pchServiceMethod /*const char **/, IntPtr pRequestBuffer /*const void **/, uint unRequestBufferSize /*uint32*/, ulong unContext /*uint64*/ )
		{
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamUnifiedMessages.SendMethod( _ptr, pchServiceMethod, (IntPtr) pRequestBuffer, unRequestBufferSize, unContext );
			else return Platform.Win64.ISteamUnifiedMessages.SendMethod( _ptr, pchServiceMethod, (IntPtr) pRequestBuffer, unRequestBufferSize, unContext );
		}
		
		// bool
		public bool SendNotification( string pchServiceNotification /*const char **/, IntPtr pNotificationBuffer /*const void **/, uint unNotificationBufferSize /*uint32*/ )
		{
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamUnifiedMessages.SendNotification( _ptr, pchServiceNotification, (IntPtr) pNotificationBuffer, unNotificationBufferSize );
			else return Platform.Win64.ISteamUnifiedMessages.SendNotification( _ptr, pchServiceNotification, (IntPtr) pNotificationBuffer, unNotificationBufferSize );
		}
		
	}
}
