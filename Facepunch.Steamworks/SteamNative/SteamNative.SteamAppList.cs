using System;
using System.Runtime.InteropServices;

namespace SteamNative
{
	public unsafe class SteamAppList
	{
		internal Platform.Interface _pi;
		
		public SteamAppList( IntPtr pointer )
		{
			if ( Platform.IsWindows64 ) _pi = new Platform.Win64( pointer );
			else if ( Platform.IsWindows32 ) _pi = new Platform.Win32( pointer );
			else if ( Platform.IsLinux32 ) _pi = new Platform.Linux32( pointer );
			else if ( Platform.IsLinux64 ) _pi = new Platform.Linux64( pointer );
			else if ( Platform.IsOsx ) _pi = new Platform.Mac( pointer );
		}
		
		public bool IsValid{ get{ return _pi != null && _pi.IsValid; } }
		
		// int
		public int GetAppBuildId( AppId_t nAppID /*AppId_t*/ )
		{
			return _pi.ISteamAppList_GetAppBuildId( nAppID );
		}
		
		// int
		// with: Detect_StringFetch True
		public string GetAppInstallDir( AppId_t nAppID /*AppId_t*/ )
		{
			int bSuccess = default( int );
			System.Text.StringBuilder pchDirectory_sb = new System.Text.StringBuilder( 4096 );
			int cchNameMax = 4096;
			bSuccess = _pi.ISteamAppList_GetAppInstallDir( nAppID, pchDirectory_sb, cchNameMax );
			if ( bSuccess <= 0 ) return null;
			return pchDirectory_sb.ToString();
		}
		
		// int
		// with: Detect_StringFetch True
		public string GetAppName( AppId_t nAppID /*AppId_t*/ )
		{
			int bSuccess = default( int );
			System.Text.StringBuilder pchName_sb = new System.Text.StringBuilder( 4096 );
			int cchNameMax = 4096;
			bSuccess = _pi.ISteamAppList_GetAppName( nAppID, pchName_sb, cchNameMax );
			if ( bSuccess <= 0 ) return null;
			return pchName_sb.ToString();
		}
		
		// with: Detect_VectorReturn
		// uint
		public uint GetInstalledApps( AppId_t[] pvecAppID /*AppId_t **/ )
		{
			var unMaxAppIDs = (uint) pvecAppID.Length;
			fixed ( AppId_t* pvecAppID_ptr = pvecAppID  )
			{
				return _pi.ISteamAppList_GetInstalledApps( (IntPtr) pvecAppID_ptr, unMaxAppIDs );
			}
		}
		
		// uint
		public uint GetNumInstalledApps()
		{
			return _pi.ISteamAppList_GetNumInstalledApps();
		}
		
	}
}
