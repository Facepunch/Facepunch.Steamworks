using System;
using System.Runtime.InteropServices;

namespace SteamNative
{
	internal unsafe class SteamUserStats : IDisposable
	{
		//
		// Holds a platform specific implentation
		//
		internal Platform.Interface platform;
		internal Facepunch.Steamworks.BaseSteamworks steamworks;
		
		//
		// Constructor decides which implementation to use based on current platform
		//
		internal SteamUserStats( Facepunch.Steamworks.BaseSteamworks steamworks, IntPtr pointer )
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
		
		// SteamAPICall_t
		public CallbackHandle AttachLeaderboardUGC( SteamLeaderboard_t hSteamLeaderboard /*SteamLeaderboard_t*/, UGCHandle_t hUGC /*UGCHandle_t*/, Action<LeaderboardUGCSet_t, bool> CallbackFunction = null /*Action<LeaderboardUGCSet_t, bool>*/ )
		{
			SteamAPICall_t callback = 0;
			callback = platform.ISteamUserStats_AttachLeaderboardUGC( hSteamLeaderboard.Value, hUGC.Value );
			
			if ( CallbackFunction == null ) return null;
			
			return LeaderboardUGCSet_t.CallResult( steamworks, callback, CallbackFunction );
		}
		
		// bool
		public bool ClearAchievement( string pchName /*const char **/ )
		{
			return platform.ISteamUserStats_ClearAchievement( pchName );
		}
		
		// SteamAPICall_t
		public CallbackHandle DownloadLeaderboardEntries( SteamLeaderboard_t hSteamLeaderboard /*SteamLeaderboard_t*/, LeaderboardDataRequest eLeaderboardDataRequest /*ELeaderboardDataRequest*/, int nRangeStart /*int*/, int nRangeEnd /*int*/, Action<LeaderboardScoresDownloaded_t, bool> CallbackFunction = null /*Action<LeaderboardScoresDownloaded_t, bool>*/ )
		{
			SteamAPICall_t callback = 0;
			callback = platform.ISteamUserStats_DownloadLeaderboardEntries( hSteamLeaderboard.Value, eLeaderboardDataRequest, nRangeStart, nRangeEnd );
			
			if ( CallbackFunction == null ) return null;
			
			return LeaderboardScoresDownloaded_t.CallResult( steamworks, callback, CallbackFunction );
		}
		
		// SteamAPICall_t
		public CallbackHandle DownloadLeaderboardEntriesForUsers( SteamLeaderboard_t hSteamLeaderboard /*SteamLeaderboard_t*/, IntPtr prgUsers /*class CSteamID **/, int cUsers /*int*/, Action<LeaderboardScoresDownloaded_t, bool> CallbackFunction = null /*Action<LeaderboardScoresDownloaded_t, bool>*/ )
		{
			SteamAPICall_t callback = 0;
			callback = platform.ISteamUserStats_DownloadLeaderboardEntriesForUsers( hSteamLeaderboard.Value, (IntPtr) prgUsers, cUsers );
			
			if ( CallbackFunction == null ) return null;
			
			return LeaderboardScoresDownloaded_t.CallResult( steamworks, callback, CallbackFunction );
		}
		
		// SteamAPICall_t
		public CallbackHandle FindLeaderboard( string pchLeaderboardName /*const char **/, Action<LeaderboardFindResult_t, bool> CallbackFunction = null /*Action<LeaderboardFindResult_t, bool>*/ )
		{
			SteamAPICall_t callback = 0;
			callback = platform.ISteamUserStats_FindLeaderboard( pchLeaderboardName );
			
			if ( CallbackFunction == null ) return null;
			
			return LeaderboardFindResult_t.CallResult( steamworks, callback, CallbackFunction );
		}
		
		// SteamAPICall_t
		public CallbackHandle FindOrCreateLeaderboard( string pchLeaderboardName /*const char **/, LeaderboardSortMethod eLeaderboardSortMethod /*ELeaderboardSortMethod*/, LeaderboardDisplayType eLeaderboardDisplayType /*ELeaderboardDisplayType*/, Action<LeaderboardFindResult_t, bool> CallbackFunction = null /*Action<LeaderboardFindResult_t, bool>*/ )
		{
			SteamAPICall_t callback = 0;
			callback = platform.ISteamUserStats_FindOrCreateLeaderboard( pchLeaderboardName, eLeaderboardSortMethod, eLeaderboardDisplayType );
			
			if ( CallbackFunction == null ) return null;
			
			return LeaderboardFindResult_t.CallResult( steamworks, callback, CallbackFunction );
		}
		
		// bool
		public bool GetAchievement( string pchName /*const char **/, ref bool pbAchieved /*bool **/ )
		{
			return platform.ISteamUserStats_GetAchievement( pchName, ref pbAchieved );
		}
		
		// bool
		public bool GetAchievementAchievedPercent( string pchName /*const char **/, out float pflPercent /*float **/ )
		{
			return platform.ISteamUserStats_GetAchievementAchievedPercent( pchName, out pflPercent );
		}
		
		// bool
		public bool GetAchievementAndUnlockTime( string pchName /*const char **/, ref bool pbAchieved /*bool **/, out uint punUnlockTime /*uint32 **/ )
		{
			return platform.ISteamUserStats_GetAchievementAndUnlockTime( pchName, ref pbAchieved, out punUnlockTime );
		}
		
		// string
		// with: Detect_StringReturn
		public string GetAchievementDisplayAttribute( string pchName /*const char **/, string pchKey /*const char **/ )
		{
			IntPtr string_pointer;
			string_pointer = platform.ISteamUserStats_GetAchievementDisplayAttribute( pchName, pchKey );
			return Marshal.PtrToStringAnsi( string_pointer );
		}
		
		// int
		public int GetAchievementIcon( string pchName /*const char **/ )
		{
			return platform.ISteamUserStats_GetAchievementIcon( pchName );
		}
		
		// string
		// with: Detect_StringReturn
		public string GetAchievementName( uint iAchievement /*uint32*/ )
		{
			IntPtr string_pointer;
			string_pointer = platform.ISteamUserStats_GetAchievementName( iAchievement );
			return Marshal.PtrToStringAnsi( string_pointer );
		}
		
		// bool
		public bool GetDownloadedLeaderboardEntry( SteamLeaderboardEntries_t hSteamLeaderboardEntries /*SteamLeaderboardEntries_t*/, int index /*int*/, ref LeaderboardEntry_t pLeaderboardEntry /*struct LeaderboardEntry_t **/, IntPtr pDetails /*int32 **/, int cDetailsMax /*int*/ )
		{
			return platform.ISteamUserStats_GetDownloadedLeaderboardEntry( hSteamLeaderboardEntries.Value, index, ref pLeaderboardEntry, (IntPtr) pDetails, cDetailsMax );
		}
		
		// bool
		public bool GetGlobalStat( string pchStatName /*const char **/, out long pData /*int64 **/ )
		{
			return platform.ISteamUserStats_GetGlobalStat( pchStatName, out pData );
		}
		
		// bool
		public bool GetGlobalStat0( string pchStatName /*const char **/, out double pData /*double **/ )
		{
			return platform.ISteamUserStats_GetGlobalStat0( pchStatName, out pData );
		}
		
		// int
		public int GetGlobalStatHistory( string pchStatName /*const char **/, out long pData /*int64 **/, uint cubData /*uint32*/ )
		{
			return platform.ISteamUserStats_GetGlobalStatHistory( pchStatName, out pData, cubData );
		}
		
		// int
		public int GetGlobalStatHistory0( string pchStatName /*const char **/, out double pData /*double **/, uint cubData /*uint32*/ )
		{
			return platform.ISteamUserStats_GetGlobalStatHistory0( pchStatName, out pData, cubData );
		}
		
		// LeaderboardDisplayType
		public LeaderboardDisplayType GetLeaderboardDisplayType( SteamLeaderboard_t hSteamLeaderboard /*SteamLeaderboard_t*/ )
		{
			return platform.ISteamUserStats_GetLeaderboardDisplayType( hSteamLeaderboard.Value );
		}
		
		// int
		public int GetLeaderboardEntryCount( SteamLeaderboard_t hSteamLeaderboard /*SteamLeaderboard_t*/ )
		{
			return platform.ISteamUserStats_GetLeaderboardEntryCount( hSteamLeaderboard.Value );
		}
		
		// string
		// with: Detect_StringReturn
		public string GetLeaderboardName( SteamLeaderboard_t hSteamLeaderboard /*SteamLeaderboard_t*/ )
		{
			IntPtr string_pointer;
			string_pointer = platform.ISteamUserStats_GetLeaderboardName( hSteamLeaderboard.Value );
			return Marshal.PtrToStringAnsi( string_pointer );
		}
		
		// LeaderboardSortMethod
		public LeaderboardSortMethod GetLeaderboardSortMethod( SteamLeaderboard_t hSteamLeaderboard /*SteamLeaderboard_t*/ )
		{
			return platform.ISteamUserStats_GetLeaderboardSortMethod( hSteamLeaderboard.Value );
		}
		
		// int
		// with: Detect_StringFetch False
		public int GetMostAchievedAchievementInfo( out string pchName /*char **/, out float pflPercent /*float **/, ref bool pbAchieved /*bool **/ )
		{
			int bSuccess = default( int );
			pchName = string.Empty;
			System.Text.StringBuilder pchName_sb = Helpers.TakeStringBuilder();
			uint unNameBufLen = 4096;
			bSuccess = platform.ISteamUserStats_GetMostAchievedAchievementInfo( pchName_sb, unNameBufLen, out pflPercent, ref pbAchieved );
			if ( bSuccess <= 0 ) return bSuccess;
			pchName = pchName_sb.ToString();
			return bSuccess;
		}
		
		// int
		// with: Detect_StringFetch False
		public int GetNextMostAchievedAchievementInfo( int iIteratorPrevious /*int*/, out string pchName /*char **/, out float pflPercent /*float **/, ref bool pbAchieved /*bool **/ )
		{
			int bSuccess = default( int );
			pchName = string.Empty;
			System.Text.StringBuilder pchName_sb = Helpers.TakeStringBuilder();
			uint unNameBufLen = 4096;
			bSuccess = platform.ISteamUserStats_GetNextMostAchievedAchievementInfo( iIteratorPrevious, pchName_sb, unNameBufLen, out pflPercent, ref pbAchieved );
			if ( bSuccess <= 0 ) return bSuccess;
			pchName = pchName_sb.ToString();
			return bSuccess;
		}
		
		// uint
		public uint GetNumAchievements()
		{
			return platform.ISteamUserStats_GetNumAchievements();
		}
		
		// SteamAPICall_t
		public CallbackHandle GetNumberOfCurrentPlayers( Action<NumberOfCurrentPlayers_t, bool> CallbackFunction = null /*Action<NumberOfCurrentPlayers_t, bool>*/ )
		{
			SteamAPICall_t callback = 0;
			callback = platform.ISteamUserStats_GetNumberOfCurrentPlayers();
			
			if ( CallbackFunction == null ) return null;
			
			return NumberOfCurrentPlayers_t.CallResult( steamworks, callback, CallbackFunction );
		}
		
		// bool
		public bool GetStat( string pchName /*const char **/, out int pData /*int32 **/ )
		{
			return platform.ISteamUserStats_GetStat( pchName, out pData );
		}
		
		// bool
		public bool GetStat0( string pchName /*const char **/, out float pData /*float **/ )
		{
			return platform.ISteamUserStats_GetStat0( pchName, out pData );
		}
		
		// bool
		public bool GetUserAchievement( CSteamID steamIDUser /*class CSteamID*/, string pchName /*const char **/, ref bool pbAchieved /*bool **/ )
		{
			return platform.ISteamUserStats_GetUserAchievement( steamIDUser.Value, pchName, ref pbAchieved );
		}
		
		// bool
		public bool GetUserAchievementAndUnlockTime( CSteamID steamIDUser /*class CSteamID*/, string pchName /*const char **/, ref bool pbAchieved /*bool **/, out uint punUnlockTime /*uint32 **/ )
		{
			return platform.ISteamUserStats_GetUserAchievementAndUnlockTime( steamIDUser.Value, pchName, ref pbAchieved, out punUnlockTime );
		}
		
		// bool
		public bool GetUserStat( CSteamID steamIDUser /*class CSteamID*/, string pchName /*const char **/, out int pData /*int32 **/ )
		{
			return platform.ISteamUserStats_GetUserStat( steamIDUser.Value, pchName, out pData );
		}
		
		// bool
		public bool GetUserStat0( CSteamID steamIDUser /*class CSteamID*/, string pchName /*const char **/, out float pData /*float **/ )
		{
			return platform.ISteamUserStats_GetUserStat0( steamIDUser.Value, pchName, out pData );
		}
		
		// bool
		public bool IndicateAchievementProgress( string pchName /*const char **/, uint nCurProgress /*uint32*/, uint nMaxProgress /*uint32*/ )
		{
			return platform.ISteamUserStats_IndicateAchievementProgress( pchName, nCurProgress, nMaxProgress );
		}
		
		// bool
		public bool RequestCurrentStats()
		{
			return platform.ISteamUserStats_RequestCurrentStats();
		}
		
		// SteamAPICall_t
		public CallbackHandle RequestGlobalAchievementPercentages( Action<GlobalAchievementPercentagesReady_t, bool> CallbackFunction = null /*Action<GlobalAchievementPercentagesReady_t, bool>*/ )
		{
			SteamAPICall_t callback = 0;
			callback = platform.ISteamUserStats_RequestGlobalAchievementPercentages();
			
			if ( CallbackFunction == null ) return null;
			
			return GlobalAchievementPercentagesReady_t.CallResult( steamworks, callback, CallbackFunction );
		}
		
		// SteamAPICall_t
		public CallbackHandle RequestGlobalStats( int nHistoryDays /*int*/, Action<GlobalStatsReceived_t, bool> CallbackFunction = null /*Action<GlobalStatsReceived_t, bool>*/ )
		{
			SteamAPICall_t callback = 0;
			callback = platform.ISteamUserStats_RequestGlobalStats( nHistoryDays );
			
			if ( CallbackFunction == null ) return null;
			
			return GlobalStatsReceived_t.CallResult( steamworks, callback, CallbackFunction );
		}
		
		// SteamAPICall_t
		public CallbackHandle RequestUserStats( CSteamID steamIDUser /*class CSteamID*/, Action<UserStatsReceived_t, bool> CallbackFunction = null /*Action<UserStatsReceived_t, bool>*/ )
		{
			SteamAPICall_t callback = 0;
			callback = platform.ISteamUserStats_RequestUserStats( steamIDUser.Value );
			
			if ( CallbackFunction == null ) return null;
			
			return UserStatsReceived_t.CallResult( steamworks, callback, CallbackFunction );
		}
		
		// bool
		public bool ResetAllStats( bool bAchievementsToo /*bool*/ )
		{
			return platform.ISteamUserStats_ResetAllStats( bAchievementsToo );
		}
		
		// bool
		public bool SetAchievement( string pchName /*const char **/ )
		{
			return platform.ISteamUserStats_SetAchievement( pchName );
		}
		
		// bool
		public bool SetStat( string pchName /*const char **/, int nData /*int32*/ )
		{
			return platform.ISteamUserStats_SetStat( pchName, nData );
		}
		
		// bool
		public bool SetStat0( string pchName /*const char **/, float fData /*float*/ )
		{
			return platform.ISteamUserStats_SetStat0( pchName, fData );
		}
		
		// bool
		public bool StoreStats()
		{
			return platform.ISteamUserStats_StoreStats();
		}
		
		// bool
		public bool UpdateAvgRateStat( string pchName /*const char **/, float flCountThisSession /*float*/, double dSessionLength /*double*/ )
		{
			return platform.ISteamUserStats_UpdateAvgRateStat( pchName, flCountThisSession, dSessionLength );
		}
		
		// SteamAPICall_t
		public CallbackHandle UploadLeaderboardScore( SteamLeaderboard_t hSteamLeaderboard /*SteamLeaderboard_t*/, LeaderboardUploadScoreMethod eLeaderboardUploadScoreMethod /*ELeaderboardUploadScoreMethod*/, int nScore /*int32*/, int[] pScoreDetails /*const int32 **/, int cScoreDetailsCount /*int*/, Action<LeaderboardScoreUploaded_t, bool> CallbackFunction = null /*Action<LeaderboardScoreUploaded_t, bool>*/ )
		{
			SteamAPICall_t callback = 0;
			callback = platform.ISteamUserStats_UploadLeaderboardScore( hSteamLeaderboard.Value, eLeaderboardUploadScoreMethod, nScore, pScoreDetails, cScoreDetailsCount );
			
			if ( CallbackFunction == null ) return null;
			
			return LeaderboardScoreUploaded_t.CallResult( steamworks, callback, CallbackFunction );
		}
		
	}
}
