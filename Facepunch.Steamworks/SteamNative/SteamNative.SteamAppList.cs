using System;
using System.Runtime.InteropServices;
using System.Linq;

namespace SteamNative
{
	internal unsafe class SteamAppList : IDisposable
	{
		//
		// Holds a platform specific implentation
		//
		internal Platform.Interface platform;
		internal Facepunch.Steamworks.BaseSteamworks steamworks;
		
		//
		// Constructor decides which implementation to use based on current platform
		//
		internal SteamAppList( Facepunch.Steamworks.BaseSteamworks steamworks, IntPtr pointer )
		{
			this.steamworks = steamworks;
			
			if ( Platform.IsWindows ) platform = new Platform.Windows( pointer );
			else if ( Platform.IsLinux ) platform = new Platform.Linux( pointer );
			else if ( Platform.IsOsx ) platform = new Platform.Mac( pointer );
		}
		
		//
		// Class is invalid if we don't have a valid implementation
		//
		public bool IsValid => platform != null && platform.IsValid;
		
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
		
		// int
		public int GetAppBuildId( AppId_t nAppID /*AppId_t*/ )
		{
			return platform.ISteamAppList_GetAppBuildId( nAppID.Value );
		}
		
		// int
		// with: Detect_StringFetch True
		public string GetAppInstallDir( AppId_t nAppID /*AppId_t*/ )
		{
			int bSuccess = default( int );
			System.Text.StringBuilder pchDirectory_sb = Helpers.TakeStringBuilder();
			int cchNameMax = 4096;
			bSuccess = platform.ISteamAppList_GetAppInstallDir( nAppID.Value, pchDirectory_sb, cchNameMax );
			if ( bSuccess <= 0 ) return null;
			return pchDirectory_sb.ToString();
		}
		
		// int
		// with: Detect_StringFetch True
		public string GetAppName( AppId_t nAppID /*AppId_t*/ )
		{
			int bSuccess = default( int );
			System.Text.StringBuilder pchName_sb = Helpers.TakeStringBuilder();
			int cchNameMax = 4096;
			bSuccess = platform.ISteamAppList_GetAppName( nAppID.Value, pchName_sb, cchNameMax );
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
				return platform.ISteamAppList_GetInstalledApps( (IntPtr) pvecAppID_ptr, unMaxAppIDs );
			}
		}
		
		// uint
		public uint GetNumInstalledApps()
		{
			return platform.ISteamAppList_GetNumInstalledApps();
		}
		
	}
}
