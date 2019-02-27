using Facepunch.Steamworks;
using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace SteamNative
{
	internal unsafe class SteamScreenshots : IDisposable
	{
		//
		// Holds a platform specific implentation
		//
		internal Platform.Interface platform;
		internal Facepunch.Steamworks.BaseSteamworks steamworks;
		
		//
		// Constructor decides which implementation to use based on current platform
		//
		internal SteamScreenshots( Facepunch.Steamworks.BaseSteamworks steamworks, IntPtr pointer )
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
		
		// ScreenshotHandle
		public ScreenshotHandle AddScreenshotToLibrary( string pchFilename /*const char **/, string pchThumbnailFilename /*const char **/, int nWidth /*int*/, int nHeight /*int*/ )
		{
			return platform.ISteamScreenshots_AddScreenshotToLibrary( Utility.GetUtf8Bytes(pchFilename), Utility.GetUtf8Bytes(pchThumbnailFilename), nWidth, nHeight );
		}
		
		// ScreenshotHandle
		public ScreenshotHandle AddVRScreenshotToLibrary( VRScreenshotType eType /*EVRScreenshotType*/, string pchFilename /*const char **/, string pchVRFilename /*const char **/ )
		{
			return platform.ISteamScreenshots_AddVRScreenshotToLibrary( eType, Utility.GetUtf8Bytes(pchFilename), Utility.GetUtf8Bytes(pchVRFilename) );
		}
		
		// void
		public void HookScreenshots( bool bHook /*bool*/ )
		{
			platform.ISteamScreenshots_HookScreenshots( bHook );
		}
		
		// bool
		public bool IsScreenshotsHooked()
		{
			return platform.ISteamScreenshots_IsScreenshotsHooked();
		}
		
		// bool
		public bool SetLocation( ScreenshotHandle hScreenshot /*ScreenshotHandle*/, string pchLocation /*const char **/ )
		{
			return platform.ISteamScreenshots_SetLocation( hScreenshot.Value, Utility.GetUtf8Bytes(pchLocation) );
		}
		
		// bool
		public bool TagPublishedFile( ScreenshotHandle hScreenshot /*ScreenshotHandle*/, PublishedFileId_t unPublishedFileID /*PublishedFileId_t*/ )
		{
			return platform.ISteamScreenshots_TagPublishedFile( hScreenshot.Value, unPublishedFileID.Value );
		}
		
		// bool
		public bool TagUser( ScreenshotHandle hScreenshot /*ScreenshotHandle*/, CSteamID steamID /*class CSteamID*/ )
		{
			return platform.ISteamScreenshots_TagUser( hScreenshot.Value, steamID.Value );
		}
		
		// void
		public void TriggerScreenshot()
		{
			platform.ISteamScreenshots_TriggerScreenshot();
		}
		
		// ScreenshotHandle
		public ScreenshotHandle WriteScreenshot( IntPtr pubRGB /*void **/, uint cubRGB /*uint32*/, int nWidth /*int*/, int nHeight /*int*/ )
		{
			return platform.ISteamScreenshots_WriteScreenshot( (IntPtr) pubRGB, cubRGB, nWidth, nHeight );
		}
		
	}
}
