using System;
using System.Runtime.InteropServices;

namespace SteamNative
{
	public unsafe class SteamVideo : IDisposable
	{
		//
		// Holds a platform specific implentation
		//
		internal Platform.Interface _pi;
		
		//
		// Constructor decides which implementation to use based on current platform
		//
		public SteamVideo( IntPtr pointer )
		{
			if ( Platform.IsWindows64 ) _pi = new Platform.Win64( pointer );
			else if ( Platform.IsWindows32 ) _pi = new Platform.Win32( pointer );
			else if ( Platform.IsLinux32 ) _pi = new Platform.Linux32( pointer );
			else if ( Platform.IsLinux64 ) _pi = new Platform.Linux64( pointer );
			else if ( Platform.IsOsx ) _pi = new Platform.Mac( pointer );
		}
		
		//
		// Class is invalid if we don't have a valid implementation
		//
		public bool IsValid{ get{ return _pi != null && _pi.IsValid; } }
		
		//
		// When shutting down clear all the internals to avoid accidental use
		//
		public virtual void Dispose()
		{
			 if ( _pi != null )
			{
				_pi.Dispose();
				_pi = null;
			}
		}
		
		// void
		public void GetVideoURL( AppId_t unVideoAppID /*AppId_t*/ )
		{
			_pi.ISteamVideo_GetVideoURL( unVideoAppID.Value );
		}
		
		// bool
		public bool IsBroadcasting( IntPtr pnNumViewers /*int **/ )
		{
			return _pi.ISteamVideo_IsBroadcasting( (IntPtr) pnNumViewers );
		}
		
	}
}
