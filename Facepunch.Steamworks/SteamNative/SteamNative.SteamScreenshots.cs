using System;
using System.Runtime.InteropServices;

namespace SteamNative
{
	public unsafe class SteamScreenshots
	{
		internal Platform.Interface _pi;
		
		public SteamScreenshots( IntPtr pointer )
		{
			if ( Platform.IsWindows64 ) _pi = new Platform.Win64( pointer );
			else if ( Platform.IsWindows32 ) _pi = new Platform.Win32( pointer );
			else if ( Platform.IsLinux32 ) _pi = new Platform.Linux32( pointer );
			else if ( Platform.IsLinux64 ) _pi = new Platform.Linux64( pointer );
			else if ( Platform.IsOsx ) _pi = new Platform.Mac( pointer );
		}
		
		public bool IsValid{ get{ return _pi != null && _pi.IsValid; } }
		
		// ScreenshotHandle
		public ScreenshotHandle AddScreenshotToLibrary( string pchFilename /*const char **/, string pchThumbnailFilename /*const char **/, int nWidth /*int*/, int nHeight /*int*/ )
		{
			return _pi.ISteamScreenshots_AddScreenshotToLibrary( pchFilename, pchThumbnailFilename, nWidth, nHeight );
		}
		
		// void
		public void HookScreenshots( bool bHook /*bool*/ )
		{
			_pi.ISteamScreenshots_HookScreenshots( bHook );
		}
		
		// bool
		public bool SetLocation( ScreenshotHandle hScreenshot /*ScreenshotHandle*/, string pchLocation /*const char **/ )
		{
			return _pi.ISteamScreenshots_SetLocation( hScreenshot, pchLocation );
		}
		
		// bool
		public bool TagPublishedFile( ScreenshotHandle hScreenshot /*ScreenshotHandle*/, PublishedFileId_t unPublishedFileID /*PublishedFileId_t*/ )
		{
			return _pi.ISteamScreenshots_TagPublishedFile( hScreenshot, unPublishedFileID );
		}
		
		// bool
		public bool TagUser( ScreenshotHandle hScreenshot /*ScreenshotHandle*/, CSteamID steamID /*class CSteamID*/ )
		{
			return _pi.ISteamScreenshots_TagUser( hScreenshot, steamID );
		}
		
		// void
		public void TriggerScreenshot()
		{
			_pi.ISteamScreenshots_TriggerScreenshot();
		}
		
		// ScreenshotHandle
		public ScreenshotHandle WriteScreenshot( IntPtr pubRGB /*void **/, uint cubRGB /*uint32*/, int nWidth /*int*/, int nHeight /*int*/ )
		{
			return _pi.ISteamScreenshots_WriteScreenshot( (IntPtr) pubRGB, cubRGB, nWidth, nHeight );
		}
		
	}
}
