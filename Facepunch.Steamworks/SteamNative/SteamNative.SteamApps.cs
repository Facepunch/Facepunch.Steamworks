using System;
using System.Runtime.InteropServices;

namespace SteamNative
{
	internal unsafe class SteamApps : IDisposable
	{
		//
		// Holds a platform specific implentation
		//
		internal Platform.Interface platform;
		internal Facepunch.Steamworks.BaseSteamworks steamworks;
		
		//
		// Constructor decides which implementation to use based on current platform
		//
		internal SteamApps( Facepunch.Steamworks.BaseSteamworks steamworks, IntPtr pointer )
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
		// with: Detect_StringFetch False
		public bool BGetDLCDataByIndex( int iDLC /*int*/, ref AppId_t pAppID /*AppId_t **/, ref bool pbAvailable /*bool **/, out string pchName /*char **/ )
		{
			bool bSuccess = default( bool );
			pchName = string.Empty;
			System.Text.StringBuilder pchName_sb = new System.Text.StringBuilder( 4096 );
			int cchNameBufferSize = 4096;
			bSuccess = platform.ISteamApps_BGetDLCDataByIndex( iDLC, ref pAppID.Value, ref pbAvailable, pchName_sb, cchNameBufferSize );
			if ( !bSuccess ) return bSuccess;
			pchName = pchName_sb.ToString();
			return bSuccess;
		}
		
		// bool
		public bool BIsAppInstalled( AppId_t appID /*AppId_t*/ )
		{
			return platform.ISteamApps_BIsAppInstalled( appID.Value );
		}
		
		// bool
		public bool BIsCybercafe()
		{
			return platform.ISteamApps_BIsCybercafe();
		}
		
		// bool
		public bool BIsDlcInstalled( AppId_t appID /*AppId_t*/ )
		{
			return platform.ISteamApps_BIsDlcInstalled( appID.Value );
		}
		
		// bool
		public bool BIsLowViolence()
		{
			return platform.ISteamApps_BIsLowViolence();
		}
		
		// bool
		public bool BIsSubscribed()
		{
			return platform.ISteamApps_BIsSubscribed();
		}
		
		// bool
		public bool BIsSubscribedApp( AppId_t appID /*AppId_t*/ )
		{
			return platform.ISteamApps_BIsSubscribedApp( appID.Value );
		}
		
		// bool
		public bool BIsSubscribedFromFreeWeekend()
		{
			return platform.ISteamApps_BIsSubscribedFromFreeWeekend();
		}
		
		// bool
		public bool BIsVACBanned()
		{
			return platform.ISteamApps_BIsVACBanned();
		}
		
		// int
		public int GetAppBuildId()
		{
			return platform.ISteamApps_GetAppBuildId();
		}
		
		// uint
		// with: Detect_StringFetch True
		public string GetAppInstallDir( AppId_t appID /*AppId_t*/ )
		{
			uint bSuccess = default( uint );
			System.Text.StringBuilder pchFolder_sb = new System.Text.StringBuilder( 4096 );
			uint cchFolderBufferSize = 4096;
			bSuccess = platform.ISteamApps_GetAppInstallDir( appID.Value, pchFolder_sb, cchFolderBufferSize );
			if ( bSuccess <= 0 ) return null;
			return pchFolder_sb.ToString();
		}
		
		// ulong
		public ulong GetAppOwner()
		{
			return platform.ISteamApps_GetAppOwner();
		}
		
		// string
		// with: Detect_StringReturn
		public string GetAvailableGameLanguages()
		{
			IntPtr string_pointer;
			string_pointer = platform.ISteamApps_GetAvailableGameLanguages();
			return Marshal.PtrToStringAnsi( string_pointer );
		}
		
		// bool
		// with: Detect_StringFetch True
		public string GetCurrentBetaName()
		{
			bool bSuccess = default( bool );
			System.Text.StringBuilder pchName_sb = new System.Text.StringBuilder( 4096 );
			int cchNameBufferSize = 4096;
			bSuccess = platform.ISteamApps_GetCurrentBetaName( pchName_sb, cchNameBufferSize );
			if ( !bSuccess ) return null;
			return pchName_sb.ToString();
		}
		
		// string
		// with: Detect_StringReturn
		public string GetCurrentGameLanguage()
		{
			IntPtr string_pointer;
			string_pointer = platform.ISteamApps_GetCurrentGameLanguage();
			return Marshal.PtrToStringAnsi( string_pointer );
		}
		
		// int
		public int GetDLCCount()
		{
			return platform.ISteamApps_GetDLCCount();
		}
		
		// bool
		public bool GetDlcDownloadProgress( AppId_t nAppID /*AppId_t*/, out ulong punBytesDownloaded /*uint64 **/, out ulong punBytesTotal /*uint64 **/ )
		{
			return platform.ISteamApps_GetDlcDownloadProgress( nAppID.Value, out punBytesDownloaded, out punBytesTotal );
		}
		
		// uint
		public uint GetEarliestPurchaseUnixTime( AppId_t nAppID /*AppId_t*/ )
		{
			return platform.ISteamApps_GetEarliestPurchaseUnixTime( nAppID.Value );
		}
		
		// SteamAPICall_t
		public CallbackHandle GetFileDetails( string pszFileName /*const char **/, Action<FileDetailsResult_t, bool> CallbackFunction = null /*Action<FileDetailsResult_t, bool>*/ )
		{
			SteamAPICall_t callback = 0;
			callback = platform.ISteamApps_GetFileDetails( pszFileName );
			
			if ( CallbackFunction == null ) return null;
			
			return FileDetailsResult_t.CallResult( steamworks, callback, CallbackFunction );
		}
		
		// uint
		public uint GetInstalledDepots( AppId_t appID /*AppId_t*/, IntPtr pvecDepots /*DepotId_t **/, uint cMaxDepots /*uint32*/ )
		{
			return platform.ISteamApps_GetInstalledDepots( appID.Value, (IntPtr) pvecDepots, cMaxDepots );
		}
		
		// string
		// with: Detect_StringReturn
		public string GetLaunchQueryParam( string pchKey /*const char **/ )
		{
			IntPtr string_pointer;
			string_pointer = platform.ISteamApps_GetLaunchQueryParam( pchKey );
			return Marshal.PtrToStringAnsi( string_pointer );
		}
		
		// void
		public void InstallDLC( AppId_t nAppID /*AppId_t*/ )
		{
			platform.ISteamApps_InstallDLC( nAppID.Value );
		}
		
		// bool
		public bool MarkContentCorrupt( bool bMissingFilesOnly /*bool*/ )
		{
			return platform.ISteamApps_MarkContentCorrupt( bMissingFilesOnly );
		}
		
		// void
		public void RequestAllProofOfPurchaseKeys()
		{
			platform.ISteamApps_RequestAllProofOfPurchaseKeys();
		}
		
		// void
		public void RequestAppProofOfPurchaseKey( AppId_t nAppID /*AppId_t*/ )
		{
			platform.ISteamApps_RequestAppProofOfPurchaseKey( nAppID.Value );
		}
		
		// void
		public void UninstallDLC( AppId_t nAppID /*AppId_t*/ )
		{
			platform.ISteamApps_UninstallDLC( nAppID.Value );
		}
		
	}
}
