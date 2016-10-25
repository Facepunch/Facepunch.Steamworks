using System;
using System.Runtime.InteropServices;

namespace SteamNative
{
	public unsafe class SteamApps
	{
		internal IntPtr _ptr;
		
		public SteamApps( IntPtr pointer )
		{
			_ptr = pointer;
		}
		
		
		// bool
		// with: Detect_StringFetch False
		public bool BGetDLCDataByIndex( int iDLC /*int*/, ref AppId_t pAppID /*AppId_t **/, out bool pbAvailable /*bool **/, out string pchName /*char **/ )
		{
			bool bSuccess = default( bool );
			pchName = string.Empty;
			var pchName_buffer = new char[4096];
			fixed ( void* pchName_ptr = pchName_buffer )
			{
				int cchNameBufferSize = 4096;
				if ( Platform.IsWindows32 ) bSuccess = Platform.Win32.ISteamApps.BGetDLCDataByIndex( _ptr, iDLC, ref pAppID, out pbAvailable, (char*)pchName_ptr, cchNameBufferSize );
				else bSuccess = Platform.Win64.ISteamApps.BGetDLCDataByIndex( _ptr, iDLC, ref pAppID, out pbAvailable, (char*)pchName_ptr, cchNameBufferSize );
				if ( !bSuccess ) return bSuccess;
				pchName = Marshal.PtrToStringAuto( (IntPtr)pchName_ptr );
			}
			return bSuccess;
		}
		
		// bool
		public bool BIsAppInstalled( AppId_t appID /*AppId_t*/ )
		{
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamApps.BIsAppInstalled( _ptr, appID );
			else return Platform.Win64.ISteamApps.BIsAppInstalled( _ptr, appID );
		}
		
		// bool
		public bool BIsCybercafe()
		{
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamApps.BIsCybercafe( _ptr );
			else return Platform.Win64.ISteamApps.BIsCybercafe( _ptr );
		}
		
		// bool
		public bool BIsDlcInstalled( AppId_t appID /*AppId_t*/ )
		{
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamApps.BIsDlcInstalled( _ptr, appID );
			else return Platform.Win64.ISteamApps.BIsDlcInstalled( _ptr, appID );
		}
		
		// bool
		public bool BIsLowViolence()
		{
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamApps.BIsLowViolence( _ptr );
			else return Platform.Win64.ISteamApps.BIsLowViolence( _ptr );
		}
		
		// bool
		public bool BIsSubscribed()
		{
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamApps.BIsSubscribed( _ptr );
			else return Platform.Win64.ISteamApps.BIsSubscribed( _ptr );
		}
		
		// bool
		public bool BIsSubscribedApp( AppId_t appID /*AppId_t*/ )
		{
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamApps.BIsSubscribedApp( _ptr, appID );
			else return Platform.Win64.ISteamApps.BIsSubscribedApp( _ptr, appID );
		}
		
		// bool
		public bool BIsSubscribedFromFreeWeekend()
		{
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamApps.BIsSubscribedFromFreeWeekend( _ptr );
			else return Platform.Win64.ISteamApps.BIsSubscribedFromFreeWeekend( _ptr );
		}
		
		// bool
		public bool BIsVACBanned()
		{
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamApps.BIsVACBanned( _ptr );
			else return Platform.Win64.ISteamApps.BIsVACBanned( _ptr );
		}
		
		// int
		public int GetAppBuildId()
		{
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamApps.GetAppBuildId( _ptr );
			else return Platform.Win64.ISteamApps.GetAppBuildId( _ptr );
		}
		
		// uint
		// with: Detect_StringFetch True
		public string GetAppInstallDir( AppId_t appID /*AppId_t*/ )
		{
			uint bSuccess = default( uint );
			var pchFolder_buffer = new char[4096];
			fixed ( void* pchFolder_ptr = pchFolder_buffer )
			{
				uint cchFolderBufferSize = 4096;
				if ( Platform.IsWindows32 ) bSuccess = Platform.Win32.ISteamApps.GetAppInstallDir( _ptr, appID, (char*)pchFolder_ptr, cchFolderBufferSize );
				else bSuccess = Platform.Win64.ISteamApps.GetAppInstallDir( _ptr, appID, (char*)pchFolder_ptr, cchFolderBufferSize );
				if ( bSuccess <= 0 ) return null;
				return Marshal.PtrToStringAuto( (IntPtr)pchFolder_ptr );
			}
		}
		
		// ulong
		public ulong GetAppOwner()
		{
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamApps.GetAppOwner( _ptr );
			else return Platform.Win64.ISteamApps.GetAppOwner( _ptr );
		}
		
		// string
		// with: Detect_StringReturn
		public string GetAvailableGameLanguages()
		{
			IntPtr string_pointer;
			if ( Platform.IsWindows32 ) string_pointer = Platform.Win32.ISteamApps.GetAvailableGameLanguages( _ptr );
			else string_pointer = Platform.Win64.ISteamApps.GetAvailableGameLanguages( _ptr );
			return Marshal.PtrToStringAnsi( string_pointer );
		}
		
		// bool
		// with: Detect_StringFetch True
		public string GetCurrentBetaName()
		{
			bool bSuccess = default( bool );
			var pchName_buffer = new char[4096];
			fixed ( void* pchName_ptr = pchName_buffer )
			{
				int cchNameBufferSize = 4096;
				if ( Platform.IsWindows32 ) bSuccess = Platform.Win32.ISteamApps.GetCurrentBetaName( _ptr, (char*)pchName_ptr, cchNameBufferSize );
				else bSuccess = Platform.Win64.ISteamApps.GetCurrentBetaName( _ptr, (char*)pchName_ptr, cchNameBufferSize );
				if ( !bSuccess ) return null;
				return Marshal.PtrToStringAuto( (IntPtr)pchName_ptr );
			}
		}
		
		// string
		// with: Detect_StringReturn
		public string GetCurrentGameLanguage()
		{
			IntPtr string_pointer;
			if ( Platform.IsWindows32 ) string_pointer = Platform.Win32.ISteamApps.GetCurrentGameLanguage( _ptr );
			else string_pointer = Platform.Win64.ISteamApps.GetCurrentGameLanguage( _ptr );
			return Marshal.PtrToStringAnsi( string_pointer );
		}
		
		// int
		public int GetDLCCount()
		{
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamApps.GetDLCCount( _ptr );
			else return Platform.Win64.ISteamApps.GetDLCCount( _ptr );
		}
		
		// bool
		public bool GetDlcDownloadProgress( AppId_t nAppID /*AppId_t*/, out ulong punBytesDownloaded /*uint64 **/, out ulong punBytesTotal /*uint64 **/ )
		{
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamApps.GetDlcDownloadProgress( _ptr, nAppID, out punBytesDownloaded, out punBytesTotal );
			else return Platform.Win64.ISteamApps.GetDlcDownloadProgress( _ptr, nAppID, out punBytesDownloaded, out punBytesTotal );
		}
		
		// uint
		public uint GetEarliestPurchaseUnixTime( AppId_t nAppID /*AppId_t*/ )
		{
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamApps.GetEarliestPurchaseUnixTime( _ptr, nAppID );
			else return Platform.Win64.ISteamApps.GetEarliestPurchaseUnixTime( _ptr, nAppID );
		}
		
		// uint
		public uint GetInstalledDepots( AppId_t appID /*AppId_t*/, IntPtr pvecDepots /*DepotId_t **/, uint cMaxDepots /*uint32*/ )
		{
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamApps.GetInstalledDepots( _ptr, appID, (IntPtr) pvecDepots, cMaxDepots );
			else return Platform.Win64.ISteamApps.GetInstalledDepots( _ptr, appID, (IntPtr) pvecDepots, cMaxDepots );
		}
		
		// string
		// with: Detect_StringReturn
		public string GetLaunchQueryParam( string pchKey /*const char **/ )
		{
			IntPtr string_pointer;
			if ( Platform.IsWindows32 ) string_pointer = Platform.Win32.ISteamApps.GetLaunchQueryParam( _ptr, pchKey );
			else string_pointer = Platform.Win64.ISteamApps.GetLaunchQueryParam( _ptr, pchKey );
			return Marshal.PtrToStringAnsi( string_pointer );
		}
		
		// void
		public void InstallDLC( AppId_t nAppID /*AppId_t*/ )
		{
			if ( Platform.IsWindows32 ) Platform.Win32.ISteamApps.InstallDLC( _ptr, nAppID );
			else Platform.Win64.ISteamApps.InstallDLC( _ptr, nAppID );
		}
		
		// bool
		public bool MarkContentCorrupt( bool bMissingFilesOnly /*bool*/ )
		{
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamApps.MarkContentCorrupt( _ptr, bMissingFilesOnly );
			else return Platform.Win64.ISteamApps.MarkContentCorrupt( _ptr, bMissingFilesOnly );
		}
		
		// void
		public void RequestAllProofOfPurchaseKeys()
		{
			if ( Platform.IsWindows32 ) Platform.Win32.ISteamApps.RequestAllProofOfPurchaseKeys( _ptr );
			else Platform.Win64.ISteamApps.RequestAllProofOfPurchaseKeys( _ptr );
		}
		
		// void
		public void RequestAppProofOfPurchaseKey( AppId_t nAppID /*AppId_t*/ )
		{
			if ( Platform.IsWindows32 ) Platform.Win32.ISteamApps.RequestAppProofOfPurchaseKey( _ptr, nAppID );
			else Platform.Win64.ISteamApps.RequestAppProofOfPurchaseKey( _ptr, nAppID );
		}
		
		// void
		public void UninstallDLC( AppId_t nAppID /*AppId_t*/ )
		{
			if ( Platform.IsWindows32 ) Platform.Win32.ISteamApps.UninstallDLC( _ptr, nAppID );
			else Platform.Win64.ISteamApps.UninstallDLC( _ptr, nAppID );
		}
		
	}
}
