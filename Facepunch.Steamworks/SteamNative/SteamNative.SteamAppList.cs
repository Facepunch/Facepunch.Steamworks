using System;
using System.Runtime.InteropServices;

namespace SteamNative
{
	public unsafe class SteamAppList
	{
		internal IntPtr _ptr;
		
		public SteamAppList( IntPtr pointer )
		{
			_ptr = pointer;
		}
		
		
		// int
		public int GetAppBuildId( AppId_t nAppID /*AppId_t*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamAppList.GetAppBuildId( _ptr, nAppID );
			else return Platform.Win64.ISteamAppList.GetAppBuildId( _ptr, nAppID );
		}
		
		// int
		// with: Detect_StringFetch True
		public string GetAppInstallDir( AppId_t nAppID /*AppId_t*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			int bSuccess = default( int );
			System.Text.StringBuilder pchDirectory_sb = new System.Text.StringBuilder( 4096 );
			int cchNameMax = 4096;
			if ( Platform.IsWindows32 ) bSuccess = Platform.Win32.ISteamAppList.GetAppInstallDir( _ptr, nAppID, pchDirectory_sb, cchNameMax );
			else bSuccess = Platform.Win64.ISteamAppList.GetAppInstallDir( _ptr, nAppID, pchDirectory_sb, cchNameMax );
			if ( bSuccess <= 0 ) return null;
			return pchDirectory_sb.ToString();
		}
		
		// int
		// with: Detect_StringFetch True
		public string GetAppName( AppId_t nAppID /*AppId_t*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			int bSuccess = default( int );
			System.Text.StringBuilder pchName_sb = new System.Text.StringBuilder( 4096 );
			int cchNameMax = 4096;
			if ( Platform.IsWindows32 ) bSuccess = Platform.Win32.ISteamAppList.GetAppName( _ptr, nAppID, pchName_sb, cchNameMax );
			else bSuccess = Platform.Win64.ISteamAppList.GetAppName( _ptr, nAppID, pchName_sb, cchNameMax );
			if ( bSuccess <= 0 ) return null;
			return pchName_sb.ToString();
		}
		
		// with: Detect_VectorReturn
		// uint
		public uint GetInstalledApps( AppId_t[] pvecAppID /*AppId_t **/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			var unMaxAppIDs = (uint) pvecAppID.Length;
			fixed ( AppId_t* pvecAppID_ptr = pvecAppID  )
			{
				if ( Platform.IsWindows32 ) return Platform.Win32.ISteamAppList.GetInstalledApps( _ptr, (IntPtr) pvecAppID_ptr, unMaxAppIDs );
				else return Platform.Win64.ISteamAppList.GetInstalledApps( _ptr, (IntPtr) pvecAppID_ptr, unMaxAppIDs );
			}
		}
		
		// uint
		public uint GetNumInstalledApps()
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamAppList.GetNumInstalledApps( _ptr );
			else return Platform.Win64.ISteamAppList.GetNumInstalledApps( _ptr );
		}
		
	}
}
