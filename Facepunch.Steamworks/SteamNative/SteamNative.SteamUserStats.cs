using System;
using System.Runtime.InteropServices;

namespace SteamNative
{
	public unsafe class SteamUserStats : IDisposable
	{
		//
		// Holds a platform specific implentation
		//
		internal Platform.Interface _pi;
		
		//
		// Constructor decides which implementation to use based on current platform
		//
		public SteamUserStats( IntPtr pointer )
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
		
		// SteamAPICall_t
		public SteamAPICall_t AttachLeaderboardUGC( SteamLeaderboard_t hSteamLeaderboard /*SteamLeaderboard_t*/, UGCHandle_t hUGC /*UGCHandle_t*/ )
		{
			return _pi.ISteamUserStats_AttachLeaderboardUGC( hSteamLeaderboard, hUGC );
		}
		
		// bool
		public bool ClearAchievement( string pchName /*const char **/ )
		{
			return _pi.ISteamUserStats_ClearAchievement( pchName );
		}
		
		// SteamAPICall_t
		public SteamAPICall_t DownloadLeaderboardEntries( SteamLeaderboard_t hSteamLeaderboard /*SteamLeaderboard_t*/, LeaderboardDataRequest eLeaderboardDataRequest /*ELeaderboardDataRequest*/, int nRangeStart /*int*/, int nRangeEnd /*int*/ )
		{
			return _pi.ISteamUserStats_DownloadLeaderboardEntries( hSteamLeaderboard, eLeaderboardDataRequest, nRangeStart, nRangeEnd );
		}
		
		// SteamAPICall_t
		public SteamAPICall_t DownloadLeaderboardEntriesForUsers( SteamLeaderboard_t hSteamLeaderboard /*SteamLeaderboard_t*/, IntPtr prgUsers /*class CSteamID **/, int cUsers /*int*/ )
		{
			return _pi.ISteamUserStats_DownloadLeaderboardEntriesForUsers( hSteamLeaderboard, (IntPtr) prgUsers, cUsers );
		}
		
		// SteamAPICall_t
		public SteamAPICall_t FindLeaderboard( string pchLeaderboardName /*const char **/ )
		{
			return _pi.ISteamUserStats_FindLeaderboard( pchLeaderboardName );
		}
		
		// SteamAPICall_t
		public SteamAPICall_t FindOrCreateLeaderboard( string pchLeaderboardName /*const char **/, LeaderboardSortMethod eLeaderboardSortMethod /*ELeaderboardSortMethod*/, LeaderboardDisplayType eLeaderboardDisplayType /*ELeaderboardDisplayType*/ )
		{
			return _pi.ISteamUserStats_FindOrCreateLeaderboard( pchLeaderboardName, eLeaderboardSortMethod, eLeaderboardDisplayType );
		}
		
		// bool
		public bool GetAchievement( string pchName /*const char **/, out bool pbAchieved /*bool **/ )
		{
			return _pi.ISteamUserStats_GetAchievement( pchName, out pbAchieved );
		}
		
		// bool
		public bool GetAchievementAchievedPercent( string pchName /*const char **/, out float pflPercent /*float **/ )
		{
			return _pi.ISteamUserStats_GetAchievementAchievedPercent( pchName, out pflPercent );
		}
		
		// bool
		public bool GetAchievementAndUnlockTime( string pchName /*const char **/, out bool pbAchieved /*bool **/, out uint punUnlockTime /*uint32 **/ )
		{
			return _pi.ISteamUserStats_GetAchievementAndUnlockTime( pchName, out pbAchieved, out punUnlockTime );
		}
		
		// string
		// with: Detect_StringReturn
		public string GetAchievementDisplayAttribute( string pchName /*const char **/, string pchKey /*const char **/ )
		{
			IntPtr string_pointer;
			string_pointer = _pi.ISteamUserStats_GetAchievementDisplayAttribute( pchName, pchKey );
			return Marshal.PtrToStringAnsi( string_pointer );
		}
		
		// int
		public int GetAchievementIcon( string pchName /*const char **/ )
		{
			return _pi.ISteamUserStats_GetAchievementIcon( pchName );
		}
		
		// string
		// with: Detect_StringReturn
		public string GetAchievementName( uint iAchievement /*uint32*/ )
		{
			IntPtr string_pointer;
			string_pointer = _pi.ISteamUserStats_GetAchievementName( iAchievement );
			return Marshal.PtrToStringAnsi( string_pointer );
		}
		
		// bool
		public bool GetDownloadedLeaderboardEntry( SteamLeaderboardEntries_t hSteamLeaderboardEntries /*SteamLeaderboardEntries_t*/, int index /*int*/, ref LeaderboardEntry_t pLeaderboardEntry /*struct LeaderboardEntry_t **/, IntPtr pDetails /*int32 **/, int cDetailsMax /*int*/ )
		{
			return _pi.ISteamUserStats_GetDownloadedLeaderboardEntry( hSteamLeaderboardEntries, index, ref pLeaderboardEntry, (IntPtr) pDetails, cDetailsMax );
		}
		
		// bool
		public bool GetGlobalStat( string pchStatName /*const char **/, out long pData /*int64 **/ )
		{
			return _pi.ISteamUserStats_GetGlobalStat( pchStatName, out pData );
		}
		
		// bool
		public bool GetGlobalStat0( string pchStatName /*const char **/, out double pData /*double **/ )
		{
			return _pi.ISteamUserStats_GetGlobalStat0( pchStatName, out pData );
		}
		
		// int
		public int GetGlobalStatHistory( string pchStatName /*const char **/, out long pData /*int64 **/, uint cubData /*uint32*/ )
		{
			return _pi.ISteamUserStats_GetGlobalStatHistory( pchStatName, out pData, cubData );
		}
		
		// int
		public int GetGlobalStatHistory0( string pchStatName /*const char **/, out double pData /*double **/, uint cubData /*uint32*/ )
		{
			return _pi.ISteamUserStats_GetGlobalStatHistory0( pchStatName, out pData, cubData );
		}
		
		// LeaderboardDisplayType
		public LeaderboardDisplayType GetLeaderboardDisplayType( SteamLeaderboard_t hSteamLeaderboard /*SteamLeaderboard_t*/ )
		{
			return _pi.ISteamUserStats_GetLeaderboardDisplayType( hSteamLeaderboard );
		}
		
		// int
		public int GetLeaderboardEntryCount( SteamLeaderboard_t hSteamLeaderboard /*SteamLeaderboard_t*/ )
		{
			return _pi.ISteamUserStats_GetLeaderboardEntryCount( hSteamLeaderboard );
		}
		
		// string
		// with: Detect_StringReturn
		public string GetLeaderboardName( SteamLeaderboard_t hSteamLeaderboard /*SteamLeaderboard_t*/ )
		{
			IntPtr string_pointer;
			string_pointer = _pi.ISteamUserStats_GetLeaderboardName( hSteamLeaderboard );
			return Marshal.PtrToStringAnsi( string_pointer );
		}
		
		// LeaderboardSortMethod
		public LeaderboardSortMethod GetLeaderboardSortMethod( SteamLeaderboard_t hSteamLeaderboard /*SteamLeaderboard_t*/ )
		{
			return _pi.ISteamUserStats_GetLeaderboardSortMethod( hSteamLeaderboard );
		}
		
		// int
		// with: Detect_StringFetch False
		public int GetMostAchievedAchievementInfo( out string pchName /*char **/, out float pflPercent /*float **/, out bool pbAchieved /*bool **/ )
		{
			int bSuccess = default( int );
			pchName = string.Empty;
			System.Text.StringBuilder pchName_sb = new System.Text.StringBuilder( 4096 );
			uint unNameBufLen = 4096;
			bSuccess = _pi.ISteamUserStats_GetMostAchievedAchievementInfo( pchName_sb, unNameBufLen, out pflPercent, out pbAchieved );
			if ( bSuccess <= 0 ) return bSuccess;
			pchName = pchName_sb.ToString();
			return bSuccess;
		}
		
		// int
		// with: Detect_StringFetch False
		public int GetNextMostAchievedAchievementInfo( int iIteratorPrevious /*int*/, out string pchName /*char **/, out float pflPercent /*float **/, out bool pbAchieved /*bool **/ )
		{
			int bSuccess = default( int );
			pchName = string.Empty;
			System.Text.StringBuilder pchName_sb = new System.Text.StringBuilder( 4096 );
			uint unNameBufLen = 4096;
			bSuccess = _pi.ISteamUserStats_GetNextMostAchievedAchievementInfo( iIteratorPrevious, pchName_sb, unNameBufLen, out pflPercent, out pbAchieved );
			if ( bSuccess <= 0 ) return bSuccess;
			pchName = pchName_sb.ToString();
			return bSuccess;
		}
		
		// uint
		public uint GetNumAchievements()
		{
			return _pi.ISteamUserStats_GetNumAchievements();
		}
		
		// SteamAPICall_t
		public SteamAPICall_t GetNumberOfCurrentPlayers()
		{
			return _pi.ISteamUserStats_GetNumberOfCurrentPlayers();
		}
		
		// bool
		public bool GetStat( string pchName /*const char **/, out int pData /*int32 **/ )
		{
			return _pi.ISteamUserStats_GetStat( pchName, out pData );
		}
		
		// bool
		public bool GetStat0( string pchName /*const char **/, out float pData /*float **/ )
		{
			return _pi.ISteamUserStats_GetStat0( pchName, out pData );
		}
		
		// bool
		public bool GetUserAchievement( CSteamID steamIDUser /*class CSteamID*/, string pchName /*const char **/, out bool pbAchieved /*bool **/ )
		{
			return _pi.ISteamUserStats_GetUserAchievement( steamIDUser, pchName, out pbAchieved );
		}
		
		// bool
		public bool GetUserAchievementAndUnlockTime( CSteamID steamIDUser /*class CSteamID*/, string pchName /*const char **/, out bool pbAchieved /*bool **/, out uint punUnlockTime /*uint32 **/ )
		{
			return _pi.ISteamUserStats_GetUserAchievementAndUnlockTime( steamIDUser, pchName, out pbAchieved, out punUnlockTime );
		}
		
		// bool
		public bool GetUserStat( CSteamID steamIDUser /*class CSteamID*/, string pchName /*const char **/, out int pData /*int32 **/ )
		{
			return _pi.ISteamUserStats_GetUserStat( steamIDUser, pchName, out pData );
		}
		
		// bool
		public bool GetUserStat0( CSteamID steamIDUser /*class CSteamID*/, string pchName /*const char **/, out float pData /*float **/ )
		{
			return _pi.ISteamUserStats_GetUserStat0( steamIDUser, pchName, out pData );
		}
		
		// bool
		public bool IndicateAchievementProgress( string pchName /*const char **/, uint nCurProgress /*uint32*/, uint nMaxProgress /*uint32*/ )
		{
			return _pi.ISteamUserStats_IndicateAchievementProgress( pchName, nCurProgress, nMaxProgress );
		}
		
		// bool
		public bool RequestCurrentStats()
		{
			return _pi.ISteamUserStats_RequestCurrentStats();
		}
		
		// SteamAPICall_t
		public SteamAPICall_t RequestGlobalAchievementPercentages()
		{
			return _pi.ISteamUserStats_RequestGlobalAchievementPercentages();
		}
		
		// SteamAPICall_t
		public SteamAPICall_t RequestGlobalStats( int nHistoryDays /*int*/ )
		{
			return _pi.ISteamUserStats_RequestGlobalStats( nHistoryDays );
		}
		
		// SteamAPICall_t
		public SteamAPICall_t RequestUserStats( CSteamID steamIDUser /*class CSteamID*/ )
		{
			return _pi.ISteamUserStats_RequestUserStats( steamIDUser );
		}
		
		// bool
		public bool ResetAllStats( bool bAchievementsToo /*bool*/ )
		{
			return _pi.ISteamUserStats_ResetAllStats( bAchievementsToo );
		}
		
		// bool
		public bool SetAchievement( string pchName /*const char **/ )
		{
			return _pi.ISteamUserStats_SetAchievement( pchName );
		}
		
		// bool
		public bool SetStat( string pchName /*const char **/, int nData /*int32*/ )
		{
			return _pi.ISteamUserStats_SetStat( pchName, nData );
		}
		
		// bool
		public bool SetStat0( string pchName /*const char **/, float fData /*float*/ )
		{
			return _pi.ISteamUserStats_SetStat0( pchName, fData );
		}
		
		// bool
		public bool StoreStats()
		{
			return _pi.ISteamUserStats_StoreStats();
		}
		
		// bool
		public bool UpdateAvgRateStat( string pchName /*const char **/, float flCountThisSession /*float*/, double dSessionLength /*double*/ )
		{
			return _pi.ISteamUserStats_UpdateAvgRateStat( pchName, flCountThisSession, dSessionLength );
		}
		
		// SteamAPICall_t
		public SteamAPICall_t UploadLeaderboardScore( SteamLeaderboard_t hSteamLeaderboard /*SteamLeaderboard_t*/, LeaderboardUploadScoreMethod eLeaderboardUploadScoreMethod /*ELeaderboardUploadScoreMethod*/, int nScore /*int32*/, IntPtr pScoreDetails /*const int32 **/, int cScoreDetailsCount /*int*/ )
		{
			return _pi.ISteamUserStats_UploadLeaderboardScore( hSteamLeaderboard, eLeaderboardUploadScoreMethod, nScore, (IntPtr) pScoreDetails, cScoreDetailsCount );
		}
		
	}
}
