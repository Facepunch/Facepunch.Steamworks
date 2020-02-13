using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Steamworks.Data;


namespace Steamworks
{
	internal class ISteamUserStats : SteamInterface
	{
		public override IntPtr GetInterfacePointer() => GetApi.SteamUserStats();
		
		
		internal ISteamUserStats()
		{
			SetupInterface();
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamUserStats_RequestCurrentStats")]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _RequestCurrentStats( IntPtr self );
		
		#endregion
		internal bool RequestCurrentStats()
		{
			var returnValue = _RequestCurrentStats( Self );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamUserStats_GetStatInt32")]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _GetStat( IntPtr self, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchName, ref int pData );
		
		#endregion
		internal bool GetStat( [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchName, ref int pData )
		{
			var returnValue = _GetStat( Self, pchName, ref pData );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamUserStats_GetStatFloat")]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _GetStat( IntPtr self, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchName, ref float pData );
		
		#endregion
		internal bool GetStat( [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchName, ref float pData )
		{
			var returnValue = _GetStat( Self, pchName, ref pData );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamUserStats_SetStatInt32")]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _SetStat( IntPtr self, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchName, int nData );
		
		#endregion
		internal bool SetStat( [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchName, int nData )
		{
			var returnValue = _SetStat( Self, pchName, nData );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamUserStats_SetStatFloat")]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _SetStat( IntPtr self, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchName, float fData );
		
		#endregion
		internal bool SetStat( [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchName, float fData )
		{
			var returnValue = _SetStat( Self, pchName, fData );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamUserStats_UpdateAvgRateStat")]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _UpdateAvgRateStat( IntPtr self, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchName, float flCountThisSession, double dSessionLength );
		
		#endregion
		internal bool UpdateAvgRateStat( [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchName, float flCountThisSession, double dSessionLength )
		{
			var returnValue = _UpdateAvgRateStat( Self, pchName, flCountThisSession, dSessionLength );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamUserStats_GetAchievement")]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _GetAchievement( IntPtr self, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchName, [MarshalAs( UnmanagedType.U1 )] ref bool pbAchieved );
		
		#endregion
		internal bool GetAchievement( [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchName, [MarshalAs( UnmanagedType.U1 )] ref bool pbAchieved )
		{
			var returnValue = _GetAchievement( Self, pchName, ref pbAchieved );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamUserStats_SetAchievement")]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _SetAchievement( IntPtr self, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchName );
		
		#endregion
		internal bool SetAchievement( [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchName )
		{
			var returnValue = _SetAchievement( Self, pchName );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamUserStats_ClearAchievement")]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _ClearAchievement( IntPtr self, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchName );
		
		#endregion
		internal bool ClearAchievement( [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchName )
		{
			var returnValue = _ClearAchievement( Self, pchName );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamUserStats_GetAchievementAndUnlockTime")]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _GetAchievementAndUnlockTime( IntPtr self, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchName, [MarshalAs( UnmanagedType.U1 )] ref bool pbAchieved, ref uint punUnlockTime );
		
		#endregion
		internal bool GetAchievementAndUnlockTime( [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchName, [MarshalAs( UnmanagedType.U1 )] ref bool pbAchieved, ref uint punUnlockTime )
		{
			var returnValue = _GetAchievementAndUnlockTime( Self, pchName, ref pbAchieved, ref punUnlockTime );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamUserStats_StoreStats")]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _StoreStats( IntPtr self );
		
		#endregion
		internal bool StoreStats()
		{
			var returnValue = _StoreStats( Self );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamUserStats_GetAchievementIcon")]
		private static extern int _GetAchievementIcon( IntPtr self, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchName );
		
		#endregion
		internal int GetAchievementIcon( [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchName )
		{
			var returnValue = _GetAchievementIcon( Self, pchName );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamUserStats_GetAchievementDisplayAttribute")]
		private static extern Utf8StringPointer _GetAchievementDisplayAttribute( IntPtr self, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchName, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchKey );
		
		#endregion
		internal string GetAchievementDisplayAttribute( [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchName, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchKey )
		{
			var returnValue = _GetAchievementDisplayAttribute( Self, pchName, pchKey );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamUserStats_IndicateAchievementProgress")]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _IndicateAchievementProgress( IntPtr self, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchName, uint nCurProgress, uint nMaxProgress );
		
		#endregion
		internal bool IndicateAchievementProgress( [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchName, uint nCurProgress, uint nMaxProgress )
		{
			var returnValue = _IndicateAchievementProgress( Self, pchName, nCurProgress, nMaxProgress );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamUserStats_GetNumAchievements")]
		private static extern uint _GetNumAchievements( IntPtr self );
		
		#endregion
		internal uint GetNumAchievements()
		{
			var returnValue = _GetNumAchievements( Self );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamUserStats_GetAchievementName")]
		private static extern Utf8StringPointer _GetAchievementName( IntPtr self, uint iAchievement );
		
		#endregion
		internal string GetAchievementName( uint iAchievement )
		{
			var returnValue = _GetAchievementName( Self, iAchievement );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamUserStats_RequestUserStats")]
		private static extern SteamAPICall_t _RequestUserStats( IntPtr self, SteamId steamIDUser );
		
		#endregion
		internal CallbackResult<UserStatsReceived_t> RequestUserStats( SteamId steamIDUser )
		{
			var returnValue = _RequestUserStats( Self, steamIDUser );
			return new CallbackResult<UserStatsReceived_t>( returnValue );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamUserStats_GetUserStat")]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _GetUserStat( IntPtr self, SteamId steamIDUser, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchName, ref int pData );
		
		#endregion
		internal bool GetUserStat( SteamId steamIDUser, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchName, ref int pData )
		{
			var returnValue = _GetUserStat( Self, steamIDUser, pchName, ref pData );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamUserStats_GetUserStat")]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _GetUserStat( IntPtr self, SteamId steamIDUser, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchName, ref float pData );
		
		#endregion
		internal bool GetUserStat( SteamId steamIDUser, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchName, ref float pData )
		{
			var returnValue = _GetUserStat( Self, steamIDUser, pchName, ref pData );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamUserStats_GetUserAchievement")]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _GetUserAchievement( IntPtr self, SteamId steamIDUser, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchName, [MarshalAs( UnmanagedType.U1 )] ref bool pbAchieved );
		
		#endregion
		internal bool GetUserAchievement( SteamId steamIDUser, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchName, [MarshalAs( UnmanagedType.U1 )] ref bool pbAchieved )
		{
			var returnValue = _GetUserAchievement( Self, steamIDUser, pchName, ref pbAchieved );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamUserStats_GetUserAchievementAndUnlockTime")]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _GetUserAchievementAndUnlockTime( IntPtr self, SteamId steamIDUser, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchName, [MarshalAs( UnmanagedType.U1 )] ref bool pbAchieved, ref uint punUnlockTime );
		
		#endregion
		internal bool GetUserAchievementAndUnlockTime( SteamId steamIDUser, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchName, [MarshalAs( UnmanagedType.U1 )] ref bool pbAchieved, ref uint punUnlockTime )
		{
			var returnValue = _GetUserAchievementAndUnlockTime( Self, steamIDUser, pchName, ref pbAchieved, ref punUnlockTime );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamUserStats_ResetAllStats")]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _ResetAllStats( IntPtr self, [MarshalAs( UnmanagedType.U1 )] bool bAchievementsToo );
		
		#endregion
		internal bool ResetAllStats( [MarshalAs( UnmanagedType.U1 )] bool bAchievementsToo )
		{
			var returnValue = _ResetAllStats( Self, bAchievementsToo );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamUserStats_FindOrCreateLeaderboard")]
		private static extern SteamAPICall_t _FindOrCreateLeaderboard( IntPtr self, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchLeaderboardName, LeaderboardSort eLeaderboardSortMethod, LeaderboardDisplay eLeaderboardDisplayType );
		
		#endregion
		internal CallbackResult<LeaderboardFindResult_t> FindOrCreateLeaderboard( [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchLeaderboardName, LeaderboardSort eLeaderboardSortMethod, LeaderboardDisplay eLeaderboardDisplayType )
		{
			var returnValue = _FindOrCreateLeaderboard( Self, pchLeaderboardName, eLeaderboardSortMethod, eLeaderboardDisplayType );
			return new CallbackResult<LeaderboardFindResult_t>( returnValue );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamUserStats_FindLeaderboard")]
		private static extern SteamAPICall_t _FindLeaderboard( IntPtr self, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchLeaderboardName );
		
		#endregion
		internal CallbackResult<LeaderboardFindResult_t> FindLeaderboard( [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchLeaderboardName )
		{
			var returnValue = _FindLeaderboard( Self, pchLeaderboardName );
			return new CallbackResult<LeaderboardFindResult_t>( returnValue );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamUserStats_GetLeaderboardName")]
		private static extern Utf8StringPointer _GetLeaderboardName( IntPtr self, SteamLeaderboard_t hSteamLeaderboard );
		
		#endregion
		internal string GetLeaderboardName( SteamLeaderboard_t hSteamLeaderboard )
		{
			var returnValue = _GetLeaderboardName( Self, hSteamLeaderboard );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamUserStats_GetLeaderboardEntryCount")]
		private static extern int _GetLeaderboardEntryCount( IntPtr self, SteamLeaderboard_t hSteamLeaderboard );
		
		#endregion
		internal int GetLeaderboardEntryCount( SteamLeaderboard_t hSteamLeaderboard )
		{
			var returnValue = _GetLeaderboardEntryCount( Self, hSteamLeaderboard );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamUserStats_GetLeaderboardSortMethod")]
		private static extern LeaderboardSort _GetLeaderboardSortMethod( IntPtr self, SteamLeaderboard_t hSteamLeaderboard );
		
		#endregion
		internal LeaderboardSort GetLeaderboardSortMethod( SteamLeaderboard_t hSteamLeaderboard )
		{
			var returnValue = _GetLeaderboardSortMethod( Self, hSteamLeaderboard );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamUserStats_GetLeaderboardDisplayType")]
		private static extern LeaderboardDisplay _GetLeaderboardDisplayType( IntPtr self, SteamLeaderboard_t hSteamLeaderboard );
		
		#endregion
		internal LeaderboardDisplay GetLeaderboardDisplayType( SteamLeaderboard_t hSteamLeaderboard )
		{
			var returnValue = _GetLeaderboardDisplayType( Self, hSteamLeaderboard );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamUserStats_DownloadLeaderboardEntries")]
		private static extern SteamAPICall_t _DownloadLeaderboardEntries( IntPtr self, SteamLeaderboard_t hSteamLeaderboard, LeaderboardDataRequest eLeaderboardDataRequest, int nRangeStart, int nRangeEnd );
		
		#endregion
		internal CallbackResult<LeaderboardScoresDownloaded_t> DownloadLeaderboardEntries( SteamLeaderboard_t hSteamLeaderboard, LeaderboardDataRequest eLeaderboardDataRequest, int nRangeStart, int nRangeEnd )
		{
			var returnValue = _DownloadLeaderboardEntries( Self, hSteamLeaderboard, eLeaderboardDataRequest, nRangeStart, nRangeEnd );
			return new CallbackResult<LeaderboardScoresDownloaded_t>( returnValue );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamUserStats_DownloadLeaderboardEntriesForUsers")]
		private static extern SteamAPICall_t _DownloadLeaderboardEntriesForUsers( IntPtr self, SteamLeaderboard_t hSteamLeaderboard, [In,Out] SteamId[]  prgUsers, int cUsers );
		
		#endregion
		internal CallbackResult<LeaderboardScoresDownloaded_t> DownloadLeaderboardEntriesForUsers( SteamLeaderboard_t hSteamLeaderboard, [In,Out] SteamId[]  prgUsers, int cUsers )
		{
			var returnValue = _DownloadLeaderboardEntriesForUsers( Self, hSteamLeaderboard, prgUsers, cUsers );
			return new CallbackResult<LeaderboardScoresDownloaded_t>( returnValue );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamUserStats_GetDownloadedLeaderboardEntry")]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _GetDownloadedLeaderboardEntry( IntPtr self, SteamLeaderboardEntries_t hSteamLeaderboardEntries, int index, ref LeaderboardEntry_t pLeaderboardEntry, [In,Out] int[]  pDetails, int cDetailsMax );
		
		#endregion
		internal bool GetDownloadedLeaderboardEntry( SteamLeaderboardEntries_t hSteamLeaderboardEntries, int index, ref LeaderboardEntry_t pLeaderboardEntry, [In,Out] int[]  pDetails, int cDetailsMax )
		{
			var returnValue = _GetDownloadedLeaderboardEntry( Self, hSteamLeaderboardEntries, index, ref pLeaderboardEntry, pDetails, cDetailsMax );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamUserStats_UploadLeaderboardScore")]
		private static extern SteamAPICall_t _UploadLeaderboardScore( IntPtr self, SteamLeaderboard_t hSteamLeaderboard, LeaderboardUploadScoreMethod eLeaderboardUploadScoreMethod, int nScore, [In,Out] int[]  pScoreDetails, int cScoreDetailsCount );
		
		#endregion
		internal CallbackResult<LeaderboardScoreUploaded_t> UploadLeaderboardScore( SteamLeaderboard_t hSteamLeaderboard, LeaderboardUploadScoreMethod eLeaderboardUploadScoreMethod, int nScore, [In,Out] int[]  pScoreDetails, int cScoreDetailsCount )
		{
			var returnValue = _UploadLeaderboardScore( Self, hSteamLeaderboard, eLeaderboardUploadScoreMethod, nScore, pScoreDetails, cScoreDetailsCount );
			return new CallbackResult<LeaderboardScoreUploaded_t>( returnValue );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamUserStats_AttachLeaderboardUGC")]
		private static extern SteamAPICall_t _AttachLeaderboardUGC( IntPtr self, SteamLeaderboard_t hSteamLeaderboard, UGCHandle_t hUGC );
		
		#endregion
		internal CallbackResult<LeaderboardUGCSet_t> AttachLeaderboardUGC( SteamLeaderboard_t hSteamLeaderboard, UGCHandle_t hUGC )
		{
			var returnValue = _AttachLeaderboardUGC( Self, hSteamLeaderboard, hUGC );
			return new CallbackResult<LeaderboardUGCSet_t>( returnValue );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamUserStats_GetNumberOfCurrentPlayers")]
		private static extern SteamAPICall_t _GetNumberOfCurrentPlayers( IntPtr self );
		
		#endregion
		internal CallbackResult<NumberOfCurrentPlayers_t> GetNumberOfCurrentPlayers()
		{
			var returnValue = _GetNumberOfCurrentPlayers( Self );
			return new CallbackResult<NumberOfCurrentPlayers_t>( returnValue );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamUserStats_RequestGlobalAchievementPercentages")]
		private static extern SteamAPICall_t _RequestGlobalAchievementPercentages( IntPtr self );
		
		#endregion
		internal CallbackResult<GlobalAchievementPercentagesReady_t> RequestGlobalAchievementPercentages()
		{
			var returnValue = _RequestGlobalAchievementPercentages( Self );
			return new CallbackResult<GlobalAchievementPercentagesReady_t>( returnValue );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamUserStats_GetMostAchievedAchievementInfo")]
		private static extern int _GetMostAchievedAchievementInfo( IntPtr self, IntPtr pchName, uint unNameBufLen, ref float pflPercent, [MarshalAs( UnmanagedType.U1 )] ref bool pbAchieved );
		
		#endregion
		internal int GetMostAchievedAchievementInfo( out string pchName, ref float pflPercent, [MarshalAs( UnmanagedType.U1 )] ref bool pbAchieved )
		{
			IntPtr mempchName = Helpers.TakeMemory();
			var returnValue = _GetMostAchievedAchievementInfo( Self, mempchName, (1024 * 32), ref pflPercent, ref pbAchieved );
			pchName = Helpers.MemoryToString( mempchName );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamUserStats_GetNextMostAchievedAchievementInfo")]
		private static extern int _GetNextMostAchievedAchievementInfo( IntPtr self, int iIteratorPrevious, IntPtr pchName, uint unNameBufLen, ref float pflPercent, [MarshalAs( UnmanagedType.U1 )] ref bool pbAchieved );
		
		#endregion
		internal int GetNextMostAchievedAchievementInfo( int iIteratorPrevious, out string pchName, ref float pflPercent, [MarshalAs( UnmanagedType.U1 )] ref bool pbAchieved )
		{
			IntPtr mempchName = Helpers.TakeMemory();
			var returnValue = _GetNextMostAchievedAchievementInfo( Self, iIteratorPrevious, mempchName, (1024 * 32), ref pflPercent, ref pbAchieved );
			pchName = Helpers.MemoryToString( mempchName );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamUserStats_GetAchievementAchievedPercent")]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _GetAchievementAchievedPercent( IntPtr self, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchName, ref float pflPercent );
		
		#endregion
		internal bool GetAchievementAchievedPercent( [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchName, ref float pflPercent )
		{
			var returnValue = _GetAchievementAchievedPercent( Self, pchName, ref pflPercent );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamUserStats_RequestGlobalStats")]
		private static extern SteamAPICall_t _RequestGlobalStats( IntPtr self, int nHistoryDays );
		
		#endregion
		internal CallbackResult<GlobalStatsReceived_t> RequestGlobalStats( int nHistoryDays )
		{
			var returnValue = _RequestGlobalStats( Self, nHistoryDays );
			return new CallbackResult<GlobalStatsReceived_t>( returnValue );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamUserStats_GetGlobalStat")]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _GetGlobalStat( IntPtr self, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchStatName, ref long pData );
		
		#endregion
		internal bool GetGlobalStat( [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchStatName, ref long pData )
		{
			var returnValue = _GetGlobalStat( Self, pchStatName, ref pData );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamUserStats_GetGlobalStat")]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _GetGlobalStat( IntPtr self, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchStatName, ref double pData );
		
		#endregion
		internal bool GetGlobalStat( [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchStatName, ref double pData )
		{
			var returnValue = _GetGlobalStat( Self, pchStatName, ref pData );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamUserStats_GetGlobalStatHistory")]
		private static extern int _GetGlobalStatHistory( IntPtr self, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchStatName, [In,Out] long[]  pData, uint cubData );
		
		#endregion
		internal int GetGlobalStatHistory( [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchStatName, [In,Out] long[]  pData, uint cubData )
		{
			var returnValue = _GetGlobalStatHistory( Self, pchStatName, pData, cubData );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamUserStats_GetGlobalStatHistory")]
		private static extern int _GetGlobalStatHistory( IntPtr self, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchStatName, [In,Out] double[]  pData, uint cubData );
		
		#endregion
		internal int GetGlobalStatHistory( [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchStatName, [In,Out] double[]  pData, uint cubData )
		{
			var returnValue = _GetGlobalStatHistory( Self, pchStatName, pData, cubData );
			return returnValue;
		}
		
	}
}
