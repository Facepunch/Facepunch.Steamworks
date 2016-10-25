using System;
using System.Runtime.InteropServices;

namespace SteamNative
{
	public unsafe class SteamVideo
	{
		internal Platform.Interface _pi;
		
		public SteamVideo( IntPtr pointer )
		{
			if ( Platform.IsWindows64 ) _pi = new Platform.Win64( pointer );
			else if ( Platform.IsWindows32 ) _pi = new Platform.Win32( pointer );
			else if ( Platform.IsLinux32 ) _pi = new Platform.Linux32( pointer );
			else if ( Platform.IsLinux64 ) _pi = new Platform.Linux64( pointer );
			else if ( Platform.IsOsx ) _pi = new Platform.Mac( pointer );
		}
		
		public bool IsValid{ get{ return _pi != null && _pi.IsValid; } }
		
		// void
		public void GetVideoURL( AppId_t unVideoAppID /*AppId_t*/ )
		{
			_pi.ISteamVideo_GetVideoURL( unVideoAppID );
		}
		
		// bool
		public bool IsBroadcasting( IntPtr pnNumViewers /*int **/ )
		{
			return _pi.ISteamVideo_IsBroadcasting( (IntPtr) pnNumViewers );
		}
		
	}
}
