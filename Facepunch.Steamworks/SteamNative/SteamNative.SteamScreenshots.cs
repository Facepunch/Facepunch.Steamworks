using System;
using System.Runtime.InteropServices;

namespace SteamNative
{
	public unsafe class SteamScreenshots
	{
		internal IntPtr _ptr;
		
		public SteamScreenshots( IntPtr pointer )
		{
			_ptr = pointer;
		}
		
		
		// ScreenshotHandle
		public ScreenshotHandle AddScreenshotToLibrary( string pchFilename /*const char **/, string pchThumbnailFilename /*const char **/, int nWidth /*int*/, int nHeight /*int*/ )
		{
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamScreenshots.AddScreenshotToLibrary( _ptr, pchFilename, pchThumbnailFilename, nWidth, nHeight );
			else return Platform.Win64.ISteamScreenshots.AddScreenshotToLibrary( _ptr, pchFilename, pchThumbnailFilename, nWidth, nHeight );
		}
		
		// void
		public void HookScreenshots( bool bHook /*bool*/ )
		{
			if ( Platform.IsWindows32 ) Platform.Win32.ISteamScreenshots.HookScreenshots( _ptr, bHook );
			else Platform.Win64.ISteamScreenshots.HookScreenshots( _ptr, bHook );
		}
		
		// bool
		public bool SetLocation( ScreenshotHandle hScreenshot /*ScreenshotHandle*/, string pchLocation /*const char **/ )
		{
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamScreenshots.SetLocation( _ptr, hScreenshot, pchLocation );
			else return Platform.Win64.ISteamScreenshots.SetLocation( _ptr, hScreenshot, pchLocation );
		}
		
		// bool
		public bool TagPublishedFile( ScreenshotHandle hScreenshot /*ScreenshotHandle*/, PublishedFileId_t unPublishedFileID /*PublishedFileId_t*/ )
		{
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamScreenshots.TagPublishedFile( _ptr, hScreenshot, unPublishedFileID );
			else return Platform.Win64.ISteamScreenshots.TagPublishedFile( _ptr, hScreenshot, unPublishedFileID );
		}
		
		// bool
		public bool TagUser( ScreenshotHandle hScreenshot /*ScreenshotHandle*/, CSteamID steamID /*class CSteamID*/ )
		{
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamScreenshots.TagUser( _ptr, hScreenshot, steamID );
			else return Platform.Win64.ISteamScreenshots.TagUser( _ptr, hScreenshot, steamID );
		}
		
		// void
		public void TriggerScreenshot()
		{
			if ( Platform.IsWindows32 ) Platform.Win32.ISteamScreenshots.TriggerScreenshot( _ptr );
			else Platform.Win64.ISteamScreenshots.TriggerScreenshot( _ptr );
		}
		
		// ScreenshotHandle
		public ScreenshotHandle WriteScreenshot( IntPtr pubRGB /*void **/, uint cubRGB /*uint32*/, int nWidth /*int*/, int nHeight /*int*/ )
		{
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamScreenshots.WriteScreenshot( _ptr, (IntPtr) pubRGB, cubRGB, nWidth, nHeight );
			else return Platform.Win64.ISteamScreenshots.WriteScreenshot( _ptr, (IntPtr) pubRGB, cubRGB, nWidth, nHeight );
		}
		
	}
}
