using System;
using System.Runtime.InteropServices;

namespace SteamNative
{
	public unsafe class SteamGameServerStats
	{
		internal IntPtr _ptr;
		
		public SteamGameServerStats( IntPtr pointer )
		{
			_ptr = pointer;
		}
		
		
		// bool
		public bool ClearUserAchievement( CSteamID steamIDUser /*class CSteamID*/, string pchName /*const char **/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamGameServerStats.ClearUserAchievement( _ptr, steamIDUser, pchName );
			else return Platform.Win64.ISteamGameServerStats.ClearUserAchievement( _ptr, steamIDUser, pchName );
		}
		
		// bool
		public bool GetUserAchievement( CSteamID steamIDUser /*class CSteamID*/, string pchName /*const char **/, out bool pbAchieved /*bool **/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamGameServerStats.GetUserAchievement( _ptr, steamIDUser, pchName, out pbAchieved );
			else return Platform.Win64.ISteamGameServerStats.GetUserAchievement( _ptr, steamIDUser, pchName, out pbAchieved );
		}
		
		// bool
		public bool GetUserStat( CSteamID steamIDUser /*class CSteamID*/, string pchName /*const char **/, out int pData /*int32 **/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamGameServerStats.GetUserStat( _ptr, steamIDUser, pchName, out pData );
			else return Platform.Win64.ISteamGameServerStats.GetUserStat( _ptr, steamIDUser, pchName, out pData );
		}
		
		// bool
		public bool GetUserStat0( CSteamID steamIDUser /*class CSteamID*/, string pchName /*const char **/, out float pData /*float **/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamGameServerStats.GetUserStat0( _ptr, steamIDUser, pchName, out pData );
			else return Platform.Win64.ISteamGameServerStats.GetUserStat0( _ptr, steamIDUser, pchName, out pData );
		}
		
		// SteamAPICall_t
		public SteamAPICall_t RequestUserStats( CSteamID steamIDUser /*class CSteamID*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamGameServerStats.RequestUserStats( _ptr, steamIDUser );
			else return Platform.Win64.ISteamGameServerStats.RequestUserStats( _ptr, steamIDUser );
		}
		
		// bool
		public bool SetUserAchievement( CSteamID steamIDUser /*class CSteamID*/, string pchName /*const char **/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamGameServerStats.SetUserAchievement( _ptr, steamIDUser, pchName );
			else return Platform.Win64.ISteamGameServerStats.SetUserAchievement( _ptr, steamIDUser, pchName );
		}
		
		// bool
		public bool SetUserStat( CSteamID steamIDUser /*class CSteamID*/, string pchName /*const char **/, int nData /*int32*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamGameServerStats.SetUserStat( _ptr, steamIDUser, pchName, nData );
			else return Platform.Win64.ISteamGameServerStats.SetUserStat( _ptr, steamIDUser, pchName, nData );
		}
		
		// bool
		public bool SetUserStat0( CSteamID steamIDUser /*class CSteamID*/, string pchName /*const char **/, float fData /*float*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamGameServerStats.SetUserStat0( _ptr, steamIDUser, pchName, fData );
			else return Platform.Win64.ISteamGameServerStats.SetUserStat0( _ptr, steamIDUser, pchName, fData );
		}
		
		// SteamAPICall_t
		public SteamAPICall_t StoreUserStats( CSteamID steamIDUser /*class CSteamID*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamGameServerStats.StoreUserStats( _ptr, steamIDUser );
			else return Platform.Win64.ISteamGameServerStats.StoreUserStats( _ptr, steamIDUser );
		}
		
		// bool
		public bool UpdateUserAvgRateStat( CSteamID steamIDUser /*class CSteamID*/, string pchName /*const char **/, float flCountThisSession /*float*/, double dSessionLength /*double*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamGameServerStats.UpdateUserAvgRateStat( _ptr, steamIDUser, pchName, flCountThisSession, dSessionLength );
			else return Platform.Win64.ISteamGameServerStats.UpdateUserAvgRateStat( _ptr, steamIDUser, pchName, flCountThisSession, dSessionLength );
		}
		
	}
}
