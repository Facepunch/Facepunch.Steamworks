using System;
using System.Runtime.InteropServices;

namespace SteamNative
{
	public unsafe class SteamGameServerStats : IDisposable
	{
		//
		// Holds a platform specific implentation
		//
		internal Platform.Interface _pi;
		
		//
		// Constructor decides which implementation to use based on current platform
		//
		public SteamGameServerStats( IntPtr pointer )
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
		public bool ClearUserAchievement( CSteamID steamIDUser /*class CSteamID*/, string pchName /*const char **/ )
		{
			return _pi.ISteamGameServerStats_ClearUserAchievement( steamIDUser, pchName );
		}
		
		// bool
		public bool GetUserAchievement( CSteamID steamIDUser /*class CSteamID*/, string pchName /*const char **/, out bool pbAchieved /*bool **/ )
		{
			return _pi.ISteamGameServerStats_GetUserAchievement( steamIDUser, pchName, out pbAchieved );
		}
		
		// bool
		public bool GetUserStat( CSteamID steamIDUser /*class CSteamID*/, string pchName /*const char **/, out int pData /*int32 **/ )
		{
			return _pi.ISteamGameServerStats_GetUserStat( steamIDUser, pchName, out pData );
		}
		
		// bool
		public bool GetUserStat0( CSteamID steamIDUser /*class CSteamID*/, string pchName /*const char **/, out float pData /*float **/ )
		{
			return _pi.ISteamGameServerStats_GetUserStat0( steamIDUser, pchName, out pData );
		}
		
		// SteamAPICall_t
		public SteamAPICall_t RequestUserStats( CSteamID steamIDUser /*class CSteamID*/ )
		{
			return _pi.ISteamGameServerStats_RequestUserStats( steamIDUser );
		}
		
		// bool
		public bool SetUserAchievement( CSteamID steamIDUser /*class CSteamID*/, string pchName /*const char **/ )
		{
			return _pi.ISteamGameServerStats_SetUserAchievement( steamIDUser, pchName );
		}
		
		// bool
		public bool SetUserStat( CSteamID steamIDUser /*class CSteamID*/, string pchName /*const char **/, int nData /*int32*/ )
		{
			return _pi.ISteamGameServerStats_SetUserStat( steamIDUser, pchName, nData );
		}
		
		// bool
		public bool SetUserStat0( CSteamID steamIDUser /*class CSteamID*/, string pchName /*const char **/, float fData /*float*/ )
		{
			return _pi.ISteamGameServerStats_SetUserStat0( steamIDUser, pchName, fData );
		}
		
		// SteamAPICall_t
		public SteamAPICall_t StoreUserStats( CSteamID steamIDUser /*class CSteamID*/ )
		{
			return _pi.ISteamGameServerStats_StoreUserStats( steamIDUser );
		}
		
		// bool
		public bool UpdateUserAvgRateStat( CSteamID steamIDUser /*class CSteamID*/, string pchName /*const char **/, float flCountThisSession /*float*/, double dSessionLength /*double*/ )
		{
			return _pi.ISteamGameServerStats_UpdateUserAvgRateStat( steamIDUser, pchName, flCountThisSession, dSessionLength );
		}
		
	}
}
