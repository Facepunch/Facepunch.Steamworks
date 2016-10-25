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
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamAppList.GetAppBuildId( _ptr, nAppID );
			else return Platform.Win64.ISteamAppList.GetAppBuildId( _ptr, nAppID );
		}
		
		// int
		// with: Detect_StringFetch True
		public string GetAppInstallDir( AppId_t nAppID /*AppId_t*/ )
		{
			int bSuccess = default( int );
			var pchDirectory_buffer = new char[4096];
			fixed ( void* pchDirectory_ptr = pchDirectory_buffer )
			{
				int cchNameMax = 4096;
				if ( Platform.IsWindows32 ) bSuccess = Platform.Win32.ISteamAppList.GetAppInstallDir( _ptr, nAppID, (char*)pchDirectory_ptr, cchNameMax );
				else bSuccess = Platform.Win64.ISteamAppList.GetAppInstallDir( _ptr, nAppID, (char*)pchDirectory_ptr, cchNameMax );
				if ( bSuccess <= 0 ) return null;
				return Marshal.PtrToStringAuto( (IntPtr)pchDirectory_ptr );
			}
		}
		
		// int
		// with: Detect_StringFetch True
		public string GetAppName( AppId_t nAppID /*AppId_t*/ )
		{
			int bSuccess = default( int );
			var pchName_buffer = new char[4096];
			fixed ( void* pchName_ptr = pchName_buffer )
			{
				int cchNameMax = 4096;
				if ( Platform.IsWindows32 ) bSuccess = Platform.Win32.ISteamAppList.GetAppName( _ptr, nAppID, (char*)pchName_ptr, cchNameMax );
				else bSuccess = Platform.Win64.ISteamAppList.GetAppName( _ptr, nAppID, (char*)pchName_ptr, cchNameMax );
				if ( bSuccess <= 0 ) return null;
				return Marshal.PtrToStringAuto( (IntPtr)pchName_ptr );
			}
		}
		
		// with: Detect_VectorReturn
		// uint
		public uint GetInstalledApps( AppId_t[] pvecAppID /*AppId_t **/ )
		{
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
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamAppList.GetNumInstalledApps( _ptr );
			else return Platform.Win64.ISteamAppList.GetNumInstalledApps( _ptr );
		}
		
	}
}
