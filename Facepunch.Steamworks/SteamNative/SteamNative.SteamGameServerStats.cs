using System;
using System.Runtime.InteropServices;

namespace SteamNative
{
	internal unsafe class SteamGameServerStats : IDisposable
	{
		//
		// Holds a platform specific implentation
		//
		internal Platform.Interface platform;
		internal Facepunch.Steamworks.BaseSteamworks steamworks;
		
		//
		// Constructor decides which implementation to use based on current platform
		//
		internal SteamGameServerStats( Facepunch.Steamworks.BaseSteamworks steamworks, IntPtr pointer )
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
		public bool ClearUserAchievement( CSteamID steamIDUser /*class CSteamID*/, string pchName /*const char **/ )
		{
			return platform.ISteamGameServerStats_ClearUserAchievement( steamIDUser.Value, pchName );
		}
		
		// bool
		public bool GetUserAchievement( CSteamID steamIDUser /*class CSteamID*/, string pchName /*const char **/, ref bool pbAchieved /*bool **/ )
		{
			return platform.ISteamGameServerStats_GetUserAchievement( steamIDUser.Value, pchName, ref pbAchieved );
		}
		
		// bool
		public bool GetUserStat( CSteamID steamIDUser /*class CSteamID*/, string pchName /*const char **/, out int pData /*int32 **/ )
		{
			return platform.ISteamGameServerStats_GetUserStat( steamIDUser.Value, pchName, out pData );
		}
		
		// bool
		public bool GetUserStat0( CSteamID steamIDUser /*class CSteamID*/, string pchName /*const char **/, out float pData /*float **/ )
		{
			return platform.ISteamGameServerStats_GetUserStat0( steamIDUser.Value, pchName, out pData );
		}
		
		// SteamAPICall_t
		public CallbackHandle RequestUserStats( CSteamID steamIDUser /*class CSteamID*/, Action<UserStatsReceived_t, bool> CallbackFunction = null /*Action<UserStatsReceived_t, bool>*/ )
		{
			SteamAPICall_t callback = 0;
			callback = platform.ISteamGameServerStats_RequestUserStats( steamIDUser.Value );
			
			if ( CallbackFunction == null ) return null;
			
			return UserStatsReceived_t.CallResult( steamworks, callback, CallbackFunction );
		}
		
		// bool
		public bool SetUserAchievement( CSteamID steamIDUser /*class CSteamID*/, string pchName /*const char **/ )
		{
			return platform.ISteamGameServerStats_SetUserAchievement( steamIDUser.Value, pchName );
		}
		
		// bool
		public bool SetUserStat( CSteamID steamIDUser /*class CSteamID*/, string pchName /*const char **/, int nData /*int32*/ )
		{
			return platform.ISteamGameServerStats_SetUserStat( steamIDUser.Value, pchName, nData );
		}
		
		// bool
		public bool SetUserStat0( CSteamID steamIDUser /*class CSteamID*/, string pchName /*const char **/, float fData /*float*/ )
		{
			return platform.ISteamGameServerStats_SetUserStat0( steamIDUser.Value, pchName, fData );
		}
		
		// SteamAPICall_t
		public CallbackHandle StoreUserStats( CSteamID steamIDUser /*class CSteamID*/, Action<GSStatsStored_t, bool> CallbackFunction = null /*Action<GSStatsStored_t, bool>*/ )
		{
			SteamAPICall_t callback = 0;
			callback = platform.ISteamGameServerStats_StoreUserStats( steamIDUser.Value );
			
			if ( CallbackFunction == null ) return null;
			
			return GSStatsStored_t.CallResult( steamworks, callback, CallbackFunction );
		}
		
		// bool
		public bool UpdateUserAvgRateStat( CSteamID steamIDUser /*class CSteamID*/, string pchName /*const char **/, float flCountThisSession /*float*/, double dSessionLength /*double*/ )
		{
			return platform.ISteamGameServerStats_UpdateUserAvgRateStat( steamIDUser.Value, pchName, flCountThisSession, dSessionLength );
		}
		
	}
}
