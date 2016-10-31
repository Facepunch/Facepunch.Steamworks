using System;
using System.Runtime.InteropServices;

namespace SteamNative
{
	internal unsafe class SteamVideo : IDisposable
	{
		//
		// Holds a platform specific implentation
		//
		internal Platform.Interface platform;
		internal Facepunch.Steamworks.BaseSteamworks steamworks;
		
		//
		// Constructor decides which implementation to use based on current platform
		//
		public SteamVideo( Facepunch.Steamworks.BaseSteamworks steamworks, IntPtr pointer )
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
		
		// void
		public void GetVideoURL( AppId_t unVideoAppID /*AppId_t*/ )
		{
			platform.ISteamVideo_GetVideoURL( unVideoAppID.Value );
		}
		
		// bool
		public bool IsBroadcasting( IntPtr pnNumViewers /*int **/ )
		{
			return platform.ISteamVideo_IsBroadcasting( (IntPtr) pnNumViewers );
		}
		
	}
}
