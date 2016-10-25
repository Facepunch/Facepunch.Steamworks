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
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			bool bSuccess = default( bool );
			pchName = string.Empty;
			System.Text.StringBuilder pchName_sb = new System.Text.StringBuilder( 4096 );
			int cchNameBufferSize = 4096;
			if ( Platform.IsWindows32 ) bSuccess = Platform.Win32.ISteamApps.BGetDLCDataByIndex( _ptr, iDLC, ref pAppID, out pbAvailable, pchName_sb, cchNameBufferSize );
			else bSuccess = Platform.Win64.ISteamApps.BGetDLCDataByIndex( _ptr, iDLC, ref pAppID, out pbAvailable, pchName_sb, cchNameBufferSize );
			if ( !bSuccess ) return bSuccess;
			pchName = pchName_sb.ToString();
			return bSuccess;
		}
		
		// bool
		public bool BIsAppInstalled( AppId_t appID /*AppId_t*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamApps.BIsAppInstalled( _ptr, appID );
			else return Platform.Win64.ISteamApps.BIsAppInstalled( _ptr, appID );
		}
		
		// bool
		public bool BIsCybercafe()
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamApps.BIsCybercafe( _ptr );
			else return Platform.Win64.ISteamApps.BIsCybercafe( _ptr );
		}
		
		// bool
		public bool BIsDlcInstalled( AppId_t appID /*AppId_t*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamApps.BIsDlcInstalled( _ptr, appID );
			else return Platform.Win64.ISteamApps.BIsDlcInstalled( _ptr, appID );
		}
		
		// bool
		public bool BIsLowViolence()
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamApps.BIsLowViolence( _ptr );
			else return Platform.Win64.ISteamApps.BIsLowViolence( _ptr );
		}
		
		// bool
		public bool BIsSubscribed()
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamApps.BIsSubscribed( _ptr );
			else return Platform.Win64.ISteamApps.BIsSubscribed( _ptr );
		}
		
		// bool
		public bool BIsSubscribedApp( AppId_t appID /*AppId_t*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamApps.BIsSubscribedApp( _ptr, appID );
			else return Platform.Win64.ISteamApps.BIsSubscribedApp( _ptr, appID );
		}
		
		// bool
		public bool BIsSubscribedFromFreeWeekend()
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamApps.BIsSubscribedFromFreeWeekend( _ptr );
			else return Platform.Win64.ISteamApps.BIsSubscribedFromFreeWeekend( _ptr );
		}
		
		// bool
		public bool BIsVACBanned()
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamApps.BIsVACBanned( _ptr );
			else return Platform.Win64.ISteamApps.BIsVACBanned( _ptr );
		}
		
		// int
		public int GetAppBuildId()
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamApps.GetAppBuildId( _ptr );
			else return Platform.Win64.ISteamApps.GetAppBuildId( _ptr );
		}
		
		// uint
		// with: Detect_StringFetch True
		public string GetAppInstallDir( AppId_t appID /*AppId_t*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			uint bSuccess = default( uint );
			System.Text.StringBuilder pchFolder_sb = new System.Text.StringBuilder( 4096 );
			uint cchFolderBufferSize = 4096;
			if ( Platform.IsWindows32 ) bSuccess = Platform.Win32.ISteamApps.GetAppInstallDir( _ptr, appID, pchFolder_sb, cchFolderBufferSize );
			else bSuccess = Platform.Win64.ISteamApps.GetAppInstallDir( _ptr, appID, pchFolder_sb, cchFolderBufferSize );
			if ( bSuccess <= 0 ) return null;
			return pchFolder_sb.ToString();
		}
		
		// ulong
		public ulong GetAppOwner()
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamApps.GetAppOwner( _ptr );
			else return Platform.Win64.ISteamApps.GetAppOwner( _ptr );
		}
		
		// string
		// with: Detect_StringReturn
		public string GetAvailableGameLanguages()
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			IntPtr string_pointer;
			if ( Platform.IsWindows32 ) string_pointer = Platform.Win32.ISteamApps.GetAvailableGameLanguages( _ptr );
			else string_pointer = Platform.Win64.ISteamApps.GetAvailableGameLanguages( _ptr );
			return Marshal.PtrToStringAnsi( string_pointer );
		}
		
		// bool
		// with: Detect_StringFetch True
		public string GetCurrentBetaName()
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			bool bSuccess = default( bool );
			System.Text.StringBuilder pchName_sb = new System.Text.StringBuilder( 4096 );
			int cchNameBufferSize = 4096;
			if ( Platform.IsWindows32 ) bSuccess = Platform.Win32.ISteamApps.GetCurrentBetaName( _ptr, pchName_sb, cchNameBufferSize );
			else bSuccess = Platform.Win64.ISteamApps.GetCurrentBetaName( _ptr, pchName_sb, cchNameBufferSize );
			if ( !bSuccess ) return null;
			return pchName_sb.ToString();
		}
		
		// string
		// with: Detect_StringReturn
		public string GetCurrentGameLanguage()
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			IntPtr string_pointer;
			if ( Platform.IsWindows32 ) string_pointer = Platform.Win32.ISteamApps.GetCurrentGameLanguage( _ptr );
			else string_pointer = Platform.Win64.ISteamApps.GetCurrentGameLanguage( _ptr );
			return Marshal.PtrToStringAnsi( string_pointer );
		}
		
		// int
		public int GetDLCCount()
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamApps.GetDLCCount( _ptr );
			else return Platform.Win64.ISteamApps.GetDLCCount( _ptr );
		}
		
		// bool
		public bool GetDlcDownloadProgress( AppId_t nAppID /*AppId_t*/, out ulong punBytesDownloaded /*uint64 **/, out ulong punBytesTotal /*uint64 **/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamApps.GetDlcDownloadProgress( _ptr, nAppID, out punBytesDownloaded, out punBytesTotal );
			else return Platform.Win64.ISteamApps.GetDlcDownloadProgress( _ptr, nAppID, out punBytesDownloaded, out punBytesTotal );
		}
		
		// uint
		public uint GetEarliestPurchaseUnixTime( AppId_t nAppID /*AppId_t*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamApps.GetEarliestPurchaseUnixTime( _ptr, nAppID );
			else return Platform.Win64.ISteamApps.GetEarliestPurchaseUnixTime( _ptr, nAppID );
		}
		
		// uint
		public uint GetInstalledDepots( AppId_t appID /*AppId_t*/, IntPtr pvecDepots /*DepotId_t **/, uint cMaxDepots /*uint32*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamApps.GetInstalledDepots( _ptr, appID, (IntPtr) pvecDepots, cMaxDepots );
			else return Platform.Win64.ISteamApps.GetInstalledDepots( _ptr, appID, (IntPtr) pvecDepots, cMaxDepots );
		}
		
		// string
		// with: Detect_StringReturn
		public string GetLaunchQueryParam( string pchKey /*const char **/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			IntPtr string_pointer;
			if ( Platform.IsWindows32 ) string_pointer = Platform.Win32.ISteamApps.GetLaunchQueryParam( _ptr, pchKey );
			else string_pointer = Platform.Win64.ISteamApps.GetLaunchQueryParam( _ptr, pchKey );
			return Marshal.PtrToStringAnsi( string_pointer );
		}
		
		// void
		public void InstallDLC( AppId_t nAppID /*AppId_t*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) Platform.Win32.ISteamApps.InstallDLC( _ptr, nAppID );
			else Platform.Win64.ISteamApps.InstallDLC( _ptr, nAppID );
		}
		
		// bool
		public bool MarkContentCorrupt( bool bMissingFilesOnly /*bool*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamApps.MarkContentCorrupt( _ptr, bMissingFilesOnly );
			else return Platform.Win64.ISteamApps.MarkContentCorrupt( _ptr, bMissingFilesOnly );
		}
		
		// void
		public void RequestAllProofOfPurchaseKeys()
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) Platform.Win32.ISteamApps.RequestAllProofOfPurchaseKeys( _ptr );
			else Platform.Win64.ISteamApps.RequestAllProofOfPurchaseKeys( _ptr );
		}
		
		// void
		public void RequestAppProofOfPurchaseKey( AppId_t nAppID /*AppId_t*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) Platform.Win32.ISteamApps.RequestAppProofOfPurchaseKey( _ptr, nAppID );
			else Platform.Win64.ISteamApps.RequestAppProofOfPurchaseKey( _ptr, nAppID );
		}
		
		// void
		public void UninstallDLC( AppId_t nAppID /*AppId_t*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) Platform.Win32.ISteamApps.UninstallDLC( _ptr, nAppID );
			else Platform.Win64.ISteamApps.UninstallDLC( _ptr, nAppID );
		}
		
	}
}
