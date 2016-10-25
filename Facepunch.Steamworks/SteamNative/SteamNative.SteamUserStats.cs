using System;
using System.Runtime.InteropServices;

namespace SteamNative
{
	public unsafe class SteamUserStats
	{
		internal IntPtr _ptr;
		
		public SteamUserStats( IntPtr pointer )
		{
			_ptr = pointer;
		}
		
		
		// SteamAPICall_t
		public SteamAPICall_t AttachLeaderboardUGC( SteamLeaderboard_t hSteamLeaderboard /*SteamLeaderboard_t*/, UGCHandle_t hUGC /*UGCHandle_t*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamUserStats.AttachLeaderboardUGC( _ptr, hSteamLeaderboard, hUGC );
			else return Platform.Win64.ISteamUserStats.AttachLeaderboardUGC( _ptr, hSteamLeaderboard, hUGC );
		}
		
		// bool
		public bool ClearAchievement( string pchName /*const char **/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamUserStats.ClearAchievement( _ptr, pchName );
			else return Platform.Win64.ISteamUserStats.ClearAchievement( _ptr, pchName );
		}
		
		// SteamAPICall_t
		public SteamAPICall_t DownloadLeaderboardEntries( SteamLeaderboard_t hSteamLeaderboard /*SteamLeaderboard_t*/, LeaderboardDataRequest eLeaderboardDataRequest /*ELeaderboardDataRequest*/, int nRangeStart /*int*/, int nRangeEnd /*int*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamUserStats.DownloadLeaderboardEntries( _ptr, hSteamLeaderboard, eLeaderboardDataRequest, nRangeStart, nRangeEnd );
			else return Platform.Win64.ISteamUserStats.DownloadLeaderboardEntries( _ptr, hSteamLeaderboard, eLeaderboardDataRequest, nRangeStart, nRangeEnd );
		}
		
		// SteamAPICall_t
		public SteamAPICall_t DownloadLeaderboardEntriesForUsers( SteamLeaderboard_t hSteamLeaderboard /*SteamLeaderboard_t*/, IntPtr prgUsers /*class CSteamID **/, int cUsers /*int*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamUserStats.DownloadLeaderboardEntriesForUsers( _ptr, hSteamLeaderboard, (IntPtr) prgUsers, cUsers );
			else return Platform.Win64.ISteamUserStats.DownloadLeaderboardEntriesForUsers( _ptr, hSteamLeaderboard, (IntPtr) prgUsers, cUsers );
		}
		
		// SteamAPICall_t
		public SteamAPICall_t FindLeaderboard( string pchLeaderboardName /*const char **/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamUserStats.FindLeaderboard( _ptr, pchLeaderboardName );
			else return Platform.Win64.ISteamUserStats.FindLeaderboard( _ptr, pchLeaderboardName );
		}
		
		// SteamAPICall_t
		public SteamAPICall_t FindOrCreateLeaderboard( string pchLeaderboardName /*const char **/, LeaderboardSortMethod eLeaderboardSortMethod /*ELeaderboardSortMethod*/, LeaderboardDisplayType eLeaderboardDisplayType /*ELeaderboardDisplayType*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamUserStats.FindOrCreateLeaderboard( _ptr, pchLeaderboardName, eLeaderboardSortMethod, eLeaderboardDisplayType );
			else return Platform.Win64.ISteamUserStats.FindOrCreateLeaderboard( _ptr, pchLeaderboardName, eLeaderboardSortMethod, eLeaderboardDisplayType );
		}
		
		// bool
		public bool GetAchievement( string pchName /*const char **/, out bool pbAchieved /*bool **/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamUserStats.GetAchievement( _ptr, pchName, out pbAchieved );
			else return Platform.Win64.ISteamUserStats.GetAchievement( _ptr, pchName, out pbAchieved );
		}
		
		// bool
		public bool GetAchievementAchievedPercent( string pchName /*const char **/, out float pflPercent /*float **/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamUserStats.GetAchievementAchievedPercent( _ptr, pchName, out pflPercent );
			else return Platform.Win64.ISteamUserStats.GetAchievementAchievedPercent( _ptr, pchName, out pflPercent );
		}
		
		// bool
		public bool GetAchievementAndUnlockTime( string pchName /*const char **/, out bool pbAchieved /*bool **/, out uint punUnlockTime /*uint32 **/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamUserStats.GetAchievementAndUnlockTime( _ptr, pchName, out pbAchieved, out punUnlockTime );
			else return Platform.Win64.ISteamUserStats.GetAchievementAndUnlockTime( _ptr, pchName, out pbAchieved, out punUnlockTime );
		}
		
		// string
		// with: Detect_StringReturn
		public string GetAchievementDisplayAttribute( string pchName /*const char **/, string pchKey /*const char **/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			IntPtr string_pointer;
			if ( Platform.IsWindows32 ) string_pointer = Platform.Win32.ISteamUserStats.GetAchievementDisplayAttribute( _ptr, pchName, pchKey );
			else string_pointer = Platform.Win64.ISteamUserStats.GetAchievementDisplayAttribute( _ptr, pchName, pchKey );
			return Marshal.PtrToStringAnsi( string_pointer );
		}
		
		// int
		public int GetAchievementIcon( string pchName /*const char **/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamUserStats.GetAchievementIcon( _ptr, pchName );
			else return Platform.Win64.ISteamUserStats.GetAchievementIcon( _ptr, pchName );
		}
		
		// string
		// with: Detect_StringReturn
		public string GetAchievementName( uint iAchievement /*uint32*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			IntPtr string_pointer;
			if ( Platform.IsWindows32 ) string_pointer = Platform.Win32.ISteamUserStats.GetAchievementName( _ptr, iAchievement );
			else string_pointer = Platform.Win64.ISteamUserStats.GetAchievementName( _ptr, iAchievement );
			return Marshal.PtrToStringAnsi( string_pointer );
		}
		
		// bool
		public bool GetDownloadedLeaderboardEntry( SteamLeaderboardEntries_t hSteamLeaderboardEntries /*SteamLeaderboardEntries_t*/, int index /*int*/, ref LeaderboardEntry_t pLeaderboardEntry /*struct LeaderboardEntry_t **/, IntPtr pDetails /*int32 **/, int cDetailsMax /*int*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamUserStats.GetDownloadedLeaderboardEntry( _ptr, hSteamLeaderboardEntries, index, ref pLeaderboardEntry, (IntPtr) pDetails, cDetailsMax );
			else return Platform.Win64.ISteamUserStats.GetDownloadedLeaderboardEntry( _ptr, hSteamLeaderboardEntries, index, ref pLeaderboardEntry, (IntPtr) pDetails, cDetailsMax );
		}
		
		// bool
		public bool GetGlobalStat( string pchStatName /*const char **/, out long pData /*int64 **/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamUserStats.GetGlobalStat( _ptr, pchStatName, out pData );
			else return Platform.Win64.ISteamUserStats.GetGlobalStat( _ptr, pchStatName, out pData );
		}
		
		// bool
		public bool GetGlobalStat0( string pchStatName /*const char **/, out double pData /*double **/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamUserStats.GetGlobalStat0( _ptr, pchStatName, out pData );
			else return Platform.Win64.ISteamUserStats.GetGlobalStat0( _ptr, pchStatName, out pData );
		}
		
		// int
		public int GetGlobalStatHistory( string pchStatName /*const char **/, out long pData /*int64 **/, uint cubData /*uint32*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamUserStats.GetGlobalStatHistory( _ptr, pchStatName, out pData, cubData );
			else return Platform.Win64.ISteamUserStats.GetGlobalStatHistory( _ptr, pchStatName, out pData, cubData );
		}
		
		// int
		public int GetGlobalStatHistory0( string pchStatName /*const char **/, out double pData /*double **/, uint cubData /*uint32*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamUserStats.GetGlobalStatHistory0( _ptr, pchStatName, out pData, cubData );
			else return Platform.Win64.ISteamUserStats.GetGlobalStatHistory0( _ptr, pchStatName, out pData, cubData );
		}
		
		// LeaderboardDisplayType
		public LeaderboardDisplayType GetLeaderboardDisplayType( SteamLeaderboard_t hSteamLeaderboard /*SteamLeaderboard_t*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamUserStats.GetLeaderboardDisplayType( _ptr, hSteamLeaderboard );
			else return Platform.Win64.ISteamUserStats.GetLeaderboardDisplayType( _ptr, hSteamLeaderboard );
		}
		
		// int
		public int GetLeaderboardEntryCount( SteamLeaderboard_t hSteamLeaderboard /*SteamLeaderboard_t*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamUserStats.GetLeaderboardEntryCount( _ptr, hSteamLeaderboard );
			else return Platform.Win64.ISteamUserStats.GetLeaderboardEntryCount( _ptr, hSteamLeaderboard );
		}
		
		// string
		// with: Detect_StringReturn
		public string GetLeaderboardName( SteamLeaderboard_t hSteamLeaderboard /*SteamLeaderboard_t*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			IntPtr string_pointer;
			if ( Platform.IsWindows32 ) string_pointer = Platform.Win32.ISteamUserStats.GetLeaderboardName( _ptr, hSteamLeaderboard );
			else string_pointer = Platform.Win64.ISteamUserStats.GetLeaderboardName( _ptr, hSteamLeaderboard );
			return Marshal.PtrToStringAnsi( string_pointer );
		}
		
		// LeaderboardSortMethod
		public LeaderboardSortMethod GetLeaderboardSortMethod( SteamLeaderboard_t hSteamLeaderboard /*SteamLeaderboard_t*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamUserStats.GetLeaderboardSortMethod( _ptr, hSteamLeaderboard );
			else return Platform.Win64.ISteamUserStats.GetLeaderboardSortMethod( _ptr, hSteamLeaderboard );
		}
		
		// int
		// with: Detect_StringFetch False
		public int GetMostAchievedAchievementInfo( out string pchName /*char **/, out float pflPercent /*float **/, out bool pbAchieved /*bool **/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			int bSuccess = default( int );
			pchName = string.Empty;
			System.Text.StringBuilder pchName_sb = new System.Text.StringBuilder( 4096 );
			uint unNameBufLen = 4096;
			if ( Platform.IsWindows32 ) bSuccess = Platform.Win32.ISteamUserStats.GetMostAchievedAchievementInfo( _ptr, pchName_sb, unNameBufLen, out pflPercent, out pbAchieved );
			else bSuccess = Platform.Win64.ISteamUserStats.GetMostAchievedAchievementInfo( _ptr, pchName_sb, unNameBufLen, out pflPercent, out pbAchieved );
			if ( bSuccess <= 0 ) return bSuccess;
			pchName = pchName_sb.ToString();
			return bSuccess;
		}
		
		// int
		// with: Detect_StringFetch False
		public int GetNextMostAchievedAchievementInfo( int iIteratorPrevious /*int*/, out string pchName /*char **/, out float pflPercent /*float **/, out bool pbAchieved /*bool **/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			int bSuccess = default( int );
			pchName = string.Empty;
			System.Text.StringBuilder pchName_sb = new System.Text.StringBuilder( 4096 );
			uint unNameBufLen = 4096;
			if ( Platform.IsWindows32 ) bSuccess = Platform.Win32.ISteamUserStats.GetNextMostAchievedAchievementInfo( _ptr, iIteratorPrevious, pchName_sb, unNameBufLen, out pflPercent, out pbAchieved );
			else bSuccess = Platform.Win64.ISteamUserStats.GetNextMostAchievedAchievementInfo( _ptr, iIteratorPrevious, pchName_sb, unNameBufLen, out pflPercent, out pbAchieved );
			if ( bSuccess <= 0 ) return bSuccess;
			pchName = pchName_sb.ToString();
			return bSuccess;
		}
		
		// uint
		public uint GetNumAchievements()
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamUserStats.GetNumAchievements( _ptr );
			else return Platform.Win64.ISteamUserStats.GetNumAchievements( _ptr );
		}
		
		// SteamAPICall_t
		public SteamAPICall_t GetNumberOfCurrentPlayers()
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamUserStats.GetNumberOfCurrentPlayers( _ptr );
			else return Platform.Win64.ISteamUserStats.GetNumberOfCurrentPlayers( _ptr );
		}
		
		// bool
		public bool GetStat( string pchName /*const char **/, out int pData /*int32 **/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamUserStats.GetStat( _ptr, pchName, out pData );
			else return Platform.Win64.ISteamUserStats.GetStat( _ptr, pchName, out pData );
		}
		
		// bool
		public bool GetStat0( string pchName /*const char **/, out float pData /*float **/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamUserStats.GetStat0( _ptr, pchName, out pData );
			else return Platform.Win64.ISteamUserStats.GetStat0( _ptr, pchName, out pData );
		}
		
		// bool
		public bool GetUserAchievement( CSteamID steamIDUser /*class CSteamID*/, string pchName /*const char **/, out bool pbAchieved /*bool **/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamUserStats.GetUserAchievement( _ptr, steamIDUser, pchName, out pbAchieved );
			else return Platform.Win64.ISteamUserStats.GetUserAchievement( _ptr, steamIDUser, pchName, out pbAchieved );
		}
		
		// bool
		public bool GetUserAchievementAndUnlockTime( CSteamID steamIDUser /*class CSteamID*/, string pchName /*const char **/, out bool pbAchieved /*bool **/, out uint punUnlockTime /*uint32 **/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamUserStats.GetUserAchievementAndUnlockTime( _ptr, steamIDUser, pchName, out pbAchieved, out punUnlockTime );
			else return Platform.Win64.ISteamUserStats.GetUserAchievementAndUnlockTime( _ptr, steamIDUser, pchName, out pbAchieved, out punUnlockTime );
		}
		
		// bool
		public bool GetUserStat( CSteamID steamIDUser /*class CSteamID*/, string pchName /*const char **/, out int pData /*int32 **/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamUserStats.GetUserStat( _ptr, steamIDUser, pchName, out pData );
			else return Platform.Win64.ISteamUserStats.GetUserStat( _ptr, steamIDUser, pchName, out pData );
		}
		
		// bool
		public bool GetUserStat0( CSteamID steamIDUser /*class CSteamID*/, string pchName /*const char **/, out float pData /*float **/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamUserStats.GetUserStat0( _ptr, steamIDUser, pchName, out pData );
			else return Platform.Win64.ISteamUserStats.GetUserStat0( _ptr, steamIDUser, pchName, out pData );
		}
		
		// bool
		public bool IndicateAchievementProgress( string pchName /*const char **/, uint nCurProgress /*uint32*/, uint nMaxProgress /*uint32*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamUserStats.IndicateAchievementProgress( _ptr, pchName, nCurProgress, nMaxProgress );
			else return Platform.Win64.ISteamUserStats.IndicateAchievementProgress( _ptr, pchName, nCurProgress, nMaxProgress );
		}
		
		// bool
		public bool RequestCurrentStats()
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamUserStats.RequestCurrentStats( _ptr );
			else return Platform.Win64.ISteamUserStats.RequestCurrentStats( _ptr );
		}
		
		// SteamAPICall_t
		public SteamAPICall_t RequestGlobalAchievementPercentages()
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamUserStats.RequestGlobalAchievementPercentages( _ptr );
			else return Platform.Win64.ISteamUserStats.RequestGlobalAchievementPercentages( _ptr );
		}
		
		// SteamAPICall_t
		public SteamAPICall_t RequestGlobalStats( int nHistoryDays /*int*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamUserStats.RequestGlobalStats( _ptr, nHistoryDays );
			else return Platform.Win64.ISteamUserStats.RequestGlobalStats( _ptr, nHistoryDays );
		}
		
		// SteamAPICall_t
		public SteamAPICall_t RequestUserStats( CSteamID steamIDUser /*class CSteamID*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamUserStats.RequestUserStats( _ptr, steamIDUser );
			else return Platform.Win64.ISteamUserStats.RequestUserStats( _ptr, steamIDUser );
		}
		
		// bool
		public bool ResetAllStats( bool bAchievementsToo /*bool*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamUserStats.ResetAllStats( _ptr, bAchievementsToo );
			else return Platform.Win64.ISteamUserStats.ResetAllStats( _ptr, bAchievementsToo );
		}
		
		// bool
		public bool SetAchievement( string pchName /*const char **/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamUserStats.SetAchievement( _ptr, pchName );
			else return Platform.Win64.ISteamUserStats.SetAchievement( _ptr, pchName );
		}
		
		// bool
		public bool SetStat( string pchName /*const char **/, int nData /*int32*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamUserStats.SetStat( _ptr, pchName, nData );
			else return Platform.Win64.ISteamUserStats.SetStat( _ptr, pchName, nData );
		}
		
		// bool
		public bool SetStat0( string pchName /*const char **/, float fData /*float*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamUserStats.SetStat0( _ptr, pchName, fData );
			else return Platform.Win64.ISteamUserStats.SetStat0( _ptr, pchName, fData );
		}
		
		// bool
		public bool StoreStats()
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamUserStats.StoreStats( _ptr );
			else return Platform.Win64.ISteamUserStats.StoreStats( _ptr );
		}
		
		// bool
		public bool UpdateAvgRateStat( string pchName /*const char **/, float flCountThisSession /*float*/, double dSessionLength /*double*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamUserStats.UpdateAvgRateStat( _ptr, pchName, flCountThisSession, dSessionLength );
			else return Platform.Win64.ISteamUserStats.UpdateAvgRateStat( _ptr, pchName, flCountThisSession, dSessionLength );
		}
		
		// SteamAPICall_t
		public SteamAPICall_t UploadLeaderboardScore( SteamLeaderboard_t hSteamLeaderboard /*SteamLeaderboard_t*/, LeaderboardUploadScoreMethod eLeaderboardUploadScoreMethod /*ELeaderboardUploadScoreMethod*/, int nScore /*int32*/, IntPtr pScoreDetails /*const int32 **/, int cScoreDetailsCount /*int*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamUserStats.UploadLeaderboardScore( _ptr, hSteamLeaderboard, eLeaderboardUploadScoreMethod, nScore, (IntPtr) pScoreDetails, cScoreDetailsCount );
			else return Platform.Win64.ISteamUserStats.UploadLeaderboardScore( _ptr, hSteamLeaderboard, eLeaderboardUploadScoreMethod, nScore, (IntPtr) pScoreDetails, cScoreDetailsCount );
		}
		
	}
}
