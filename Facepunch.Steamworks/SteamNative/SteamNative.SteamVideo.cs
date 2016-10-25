using System;
using System.Runtime.InteropServices;

namespace SteamNative
{
	public unsafe class SteamVideo
	{
		internal IntPtr _ptr;
		
		public SteamVideo( IntPtr pointer )
		{
			_ptr = pointer;
		}
		
		
		// void
		public void GetVideoURL( AppId_t unVideoAppID /*AppId_t*/ )
		{
			if ( Platform.IsWindows32 ) Platform.Win32.ISteamVideo.GetVideoURL( _ptr, unVideoAppID );
			else Platform.Win64.ISteamVideo.GetVideoURL( _ptr, unVideoAppID );
		}
		
		// bool
		public bool IsBroadcasting( IntPtr pnNumViewers /*int **/ )
		{
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamVideo.IsBroadcasting( _ptr, (IntPtr) pnNumViewers );
			else return Platform.Win64.ISteamVideo.IsBroadcasting( _ptr, (IntPtr) pnNumViewers );
		}
		
	}
}
