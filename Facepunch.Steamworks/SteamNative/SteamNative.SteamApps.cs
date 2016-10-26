using System;
using System.Runtime.InteropServices;

namespace SteamNative
{
	public unsafe class SteamApps : IDisposable
	{
		//
		// Holds a platform specific implentation
		//
		internal Platform.Interface _pi;
		
		//
		// Constructor decides which implementation to use based on current platform
		//
		public SteamApps( IntPtr pointer )
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
		
		// bool
		// with: Detect_StringFetch False
		public bool BGetDLCDataByIndex( int iDLC /*int*/, ref AppId_t pAppID /*AppId_t **/, ref bool pbAvailable /*bool **/, out string pchName /*char **/ )
		{
			bool bSuccess = default( bool );
			pchName = string.Empty;
			System.Text.StringBuilder pchName_sb = new System.Text.StringBuilder( 4096 );
			int cchNameBufferSize = 4096;
			bSuccess = _pi.ISteamApps_BGetDLCDataByIndex( iDLC, ref pAppID.Value, ref pbAvailable, pchName_sb, cchNameBufferSize );
			if ( !bSuccess ) return bSuccess;
			pchName = pchName_sb.ToString();
			return bSuccess;
		}
		
		// bool
		public bool BIsAppInstalled( AppId_t appID /*AppId_t*/ )
		{
			return _pi.ISteamApps_BIsAppInstalled( appID.Value );
		}
		
		// bool
		public bool BIsCybercafe()
		{
			return _pi.ISteamApps_BIsCybercafe();
		}
		
		// bool
		public bool BIsDlcInstalled( AppId_t appID /*AppId_t*/ )
		{
			return _pi.ISteamApps_BIsDlcInstalled( appID.Value );
		}
		
		// bool
		public bool BIsLowViolence()
		{
			return _pi.ISteamApps_BIsLowViolence();
		}
		
		// bool
		public bool BIsSubscribed()
		{
			return _pi.ISteamApps_BIsSubscribed();
		}
		
		// bool
		public bool BIsSubscribedApp( AppId_t appID /*AppId_t*/ )
		{
			return _pi.ISteamApps_BIsSubscribedApp( appID.Value );
		}
		
		// bool
		public bool BIsSubscribedFromFreeWeekend()
		{
			return _pi.ISteamApps_BIsSubscribedFromFreeWeekend();
		}
		
		// bool
		public bool BIsVACBanned()
		{
			return _pi.ISteamApps_BIsVACBanned();
		}
		
		// int
		public int GetAppBuildId()
		{
			return _pi.ISteamApps_GetAppBuildId();
		}
		
		// uint
		// with: Detect_StringFetch True
		public string GetAppInstallDir( AppId_t appID /*AppId_t*/ )
		{
			uint bSuccess = default( uint );
			System.Text.StringBuilder pchFolder_sb = new System.Text.StringBuilder( 4096 );
			uint cchFolderBufferSize = 4096;
			bSuccess = _pi.ISteamApps_GetAppInstallDir( appID.Value, pchFolder_sb, cchFolderBufferSize );
			if ( bSuccess <= 0 ) return null;
			return pchFolder_sb.ToString();
		}
		
		// ulong
		public ulong GetAppOwner()
		{
			return _pi.ISteamApps_GetAppOwner();
		}
		
		// string
		// with: Detect_StringReturn
		public string GetAvailableGameLanguages()
		{
			IntPtr string_pointer;
			string_pointer = _pi.ISteamApps_GetAvailableGameLanguages();
			return Marshal.PtrToStringAnsi( string_pointer );
		}
		
		// bool
		// with: Detect_StringFetch True
		public string GetCurrentBetaName()
		{
			bool bSuccess = default( bool );
			System.Text.StringBuilder pchName_sb = new System.Text.StringBuilder( 4096 );
			int cchNameBufferSize = 4096;
			bSuccess = _pi.ISteamApps_GetCurrentBetaName( pchName_sb, cchNameBufferSize );
			if ( !bSuccess ) return null;
			return pchName_sb.ToString();
		}
		
		// string
		// with: Detect_StringReturn
		public string GetCurrentGameLanguage()
		{
			IntPtr string_pointer;
			string_pointer = _pi.ISteamApps_GetCurrentGameLanguage();
			return Marshal.PtrToStringAnsi( string_pointer );
		}
		
		// int
		public int GetDLCCount()
		{
			return _pi.ISteamApps_GetDLCCount();
		}
		
		// bool
		public bool GetDlcDownloadProgress( AppId_t nAppID /*AppId_t*/, out ulong punBytesDownloaded /*uint64 **/, out ulong punBytesTotal /*uint64 **/ )
		{
			return _pi.ISteamApps_GetDlcDownloadProgress( nAppID.Value, out punBytesDownloaded, out punBytesTotal );
		}
		
		// uint
		public uint GetEarliestPurchaseUnixTime( AppId_t nAppID /*AppId_t*/ )
		{
			return _pi.ISteamApps_GetEarliestPurchaseUnixTime( nAppID.Value );
		}
		
		// uint
		public uint GetInstalledDepots( AppId_t appID /*AppId_t*/, IntPtr pvecDepots /*DepotId_t **/, uint cMaxDepots /*uint32*/ )
		{
			return _pi.ISteamApps_GetInstalledDepots( appID.Value, (IntPtr) pvecDepots, cMaxDepots );
		}
		
		// string
		// with: Detect_StringReturn
		public string GetLaunchQueryParam( string pchKey /*const char **/ )
		{
			IntPtr string_pointer;
			string_pointer = _pi.ISteamApps_GetLaunchQueryParam( pchKey );
			return Marshal.PtrToStringAnsi( string_pointer );
		}
		
		// void
		public void InstallDLC( AppId_t nAppID /*AppId_t*/ )
		{
			_pi.ISteamApps_InstallDLC( nAppID.Value );
		}
		
		// bool
		public bool MarkContentCorrupt( bool bMissingFilesOnly /*bool*/ )
		{
			return _pi.ISteamApps_MarkContentCorrupt( bMissingFilesOnly );
		}
		
		// void
		public void RequestAllProofOfPurchaseKeys()
		{
			_pi.ISteamApps_RequestAllProofOfPurchaseKeys();
		}
		
		// void
		public void RequestAppProofOfPurchaseKey( AppId_t nAppID /*AppId_t*/ )
		{
			_pi.ISteamApps_RequestAppProofOfPurchaseKey( nAppID.Value );
		}
		
		// void
		public void UninstallDLC( AppId_t nAppID /*AppId_t*/ )
		{
			_pi.ISteamApps_UninstallDLC( nAppID.Value );
		}
		
	}
}
