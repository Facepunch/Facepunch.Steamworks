using System;
using System.Runtime.InteropServices;

namespace SteamNative
{
	public unsafe class SteamScreenshots : IDisposable
	{
		//
		// Holds a platform specific implentation
		//
		internal Platform.Interface _pi;
		
		//
		// Constructor decides which implementation to use based on current platform
		//
		public SteamScreenshots( IntPtr pointer )
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
		
		// ScreenshotHandle
		public ScreenshotHandle AddScreenshotToLibrary( string pchFilename /*const char **/, string pchThumbnailFilename /*const char **/, int nWidth /*int*/, int nHeight /*int*/ )
		{
			return _pi.ISteamScreenshots_AddScreenshotToLibrary( pchFilename, pchThumbnailFilename, nWidth, nHeight );
		}
		
		// ScreenshotHandle
		public ScreenshotHandle AddVRScreenshotToLibrary( VRScreenshotType eType /*EVRScreenshotType*/, string pchFilename /*const char **/, string pchVRFilename /*const char **/ )
		{
			return _pi.ISteamScreenshots_AddVRScreenshotToLibrary( eType, pchFilename, pchVRFilename );
		}
		
		// void
		public void HookScreenshots( bool bHook /*bool*/ )
		{
			_pi.ISteamScreenshots_HookScreenshots( bHook );
		}
		
		// bool
		public bool IsScreenshotsHooked()
		{
			return _pi.ISteamScreenshots_IsScreenshotsHooked();
		}
		
		// bool
		public bool SetLocation( ScreenshotHandle hScreenshot /*ScreenshotHandle*/, string pchLocation /*const char **/ )
		{
			return _pi.ISteamScreenshots_SetLocation( hScreenshot.Value, pchLocation );
		}
		
		// bool
		public bool TagPublishedFile( ScreenshotHandle hScreenshot /*ScreenshotHandle*/, PublishedFileId_t unPublishedFileID /*PublishedFileId_t*/ )
		{
			return _pi.ISteamScreenshots_TagPublishedFile( hScreenshot.Value, unPublishedFileID.Value );
		}
		
		// bool
		public bool TagUser( ScreenshotHandle hScreenshot /*ScreenshotHandle*/, CSteamID steamID /*class CSteamID*/ )
		{
			return _pi.ISteamScreenshots_TagUser( hScreenshot.Value, steamID.Value );
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
