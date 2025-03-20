using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Steamworks.Data;


namespace Steamworks
{
	internal unsafe partial class ISteamUserStats : SteamInterface
	{
		public const string Version = "STEAMUSERSTATS_INTERFACE_VERSION013";
		
		internal ISteamUserStats( bool IsGameServer )
		{
			SetupInterface( IsGameServer );
		}
		
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_SteamUserStats_v013", CallingConvention = Platform.CC)]
		internal static extern IntPtr SteamAPI_SteamUserStats_v013();
		public override IntPtr GetUserInterfacePointer() => SteamAPI_SteamUserStats_v013();
		
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamUserStats_GetStatInt32", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _GetStat( IntPtr self, IntPtr pchName, ref int pData );
		
		#endregion
		internal bool GetStat( string pchName, ref int pData )
		{
			using var str__pchName = new Utf8StringToNative( pchName );
			var returnValue = _GetStat( Self, str__pchName.Pointer, ref pData );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamUserStats_GetStatFloat", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _GetStat( IntPtr self, IntPtr pchName, ref float pData );
		
		#endregion
		internal bool GetStat( string pchName, ref float pData )
		{
			using var str__pchName = new Utf8StringToNative( pchName );
			var returnValue = _GetStat( Self, str__pchName.Pointer, ref pData );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamUserStats_SetStatInt32", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _SetStat( IntPtr self, IntPtr pchName, int nData );
		
		#endregion
		internal bool SetStat( string pchName, int nData )
		{
			using var str__pchName = new Utf8StringToNative( pchName );
			var returnValue = _SetStat( Self, str__pchName.Pointer, nData );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamUserStats_SetStatFloat", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _SetStat( IntPtr self, IntPtr pchName, float fData );
		
		#endregion
		internal bool SetStat( string pchName, float fData )
		{
			using var str__pchName = new Utf8StringToNative( pchName );
			var returnValue = _SetStat( Self, str__pchName.Pointer, fData );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamUserStats_UpdateAvgRateStat", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _UpdateAvgRateStat( IntPtr self, IntPtr pchName, float flCountThisSession, double dSessionLength );
		
		#endregion
		internal bool UpdateAvgRateStat( string pchName, float flCountThisSession, double dSessionLength )
		{
			using var str__pchName = new Utf8StringToNative( pchName );
			var returnValue = _UpdateAvgRateStat( Self, str__pchName.Pointer, flCountThisSession, dSessionLength );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamUserStats_GetAchievement", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _GetAchievement( IntPtr self, IntPtr pchName, [MarshalAs( UnmanagedType.U1 )] ref bool pbAchieved );
		
		#endregion
		internal bool GetAchievement( string pchName, [MarshalAs( UnmanagedType.U1 )] ref bool pbAchieved )
		{
			using var str__pchName = new Utf8StringToNative( pchName );
			var returnValue = _GetAchievement( Self, str__pchName.Pointer, ref pbAchieved );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamUserStats_SetAchievement", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _SetAchievement( IntPtr self, IntPtr pchName );
		
		#endregion
		internal bool SetAchievement( string pchName )
		{
			using var str__pchName = new Utf8StringToNative( pchName );
			var returnValue = _SetAchievement( Self, str__pchName.Pointer );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamUserStats_ClearAchievement", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _ClearAchievement( IntPtr self, IntPtr pchName );
		
		#endregion
		internal bool ClearAchievement( string pchName )
		{
			using var str__pchName = new Utf8StringToNative( pchName );
			var returnValue = _ClearAchievement( Self, str__pchName.Pointer );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamUserStats_GetAchievementAndUnlockTime", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _GetAchievementAndUnlockTime( IntPtr self, IntPtr pchName, [MarshalAs( UnmanagedType.U1 )] ref bool pbAchieved, ref uint punUnlockTime );
		
		#endregion
		internal bool GetAchievementAndUnlockTime( string pchName, [MarshalAs( UnmanagedType.U1 )] ref bool pbAchieved, ref uint punUnlockTime )
		{
			using var str__pchName = new Utf8StringToNative( pchName );
			var returnValue = _GetAchievementAndUnlockTime( Self, str__pchName.Pointer, ref pbAchieved, ref punUnlockTime );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamUserStats_StoreStats", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _StoreStats( IntPtr self );
		
		#endregion
		internal bool StoreStats()
		{
			var returnValue = _StoreStats( Self );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamUserStats_GetAchievementIcon", CallingConvention = Platform.CC)]
		private static extern int _GetAchievementIcon( IntPtr self, IntPtr pchName );
		
		#endregion
		internal int GetAchievementIcon( string pchName )
		{
			using var str__pchName = new Utf8StringToNative( pchName );
			var returnValue = _GetAchievementIcon( Self, str__pchName.Pointer );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamUserStats_GetAchievementDisplayAttribute", CallingConvention = Platform.CC)]
		private static extern Utf8StringPointer _GetAchievementDisplayAttribute( IntPtr self, IntPtr pchName, IntPtr pchKey );
		
		#endregion
		internal string GetAchievementDisplayAttribute( string pchName, string pchKey )
		{
			using var str__pchName = new Utf8StringToNative( pchName );
			using var str__pchKey = new Utf8StringToNative( pchKey );
			var returnValue = _GetAchievementDisplayAttribute( Self, str__pchName.Pointer, str__pchKey.Pointer );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamUserStats_IndicateAchievementProgress", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _IndicateAchievementProgress( IntPtr self, IntPtr pchName, uint nCurProgress, uint nMaxProgress );
		
		#endregion
		internal bool IndicateAchievementProgress( string pchName, uint nCurProgress, uint nMaxProgress )
		{
			using var str__pchName = new Utf8StringToNative( pchName );
			var returnValue = _IndicateAchievementProgress( Self, str__pchName.Pointer, nCurProgress, nMaxProgress );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamUserStats_GetNumAchievements", CallingConvention = Platform.CC)]
		private static extern uint _GetNumAchievements( IntPtr self );
		
		#endregion
		internal uint GetNumAchievements()
		{
			var returnValue = _GetNumAchievements( Self );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamUserStats_GetAchievementName", CallingConvention = Platform.CC)]
		private static extern Utf8StringPointer _GetAchievementName( IntPtr self, uint iAchievement );
		
		#endregion
		internal string GetAchievementName( uint iAchievement )
		{
			var returnValue = _GetAchievementName( Self, iAchievement );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamUserStats_RequestUserStats", CallingConvention = Platform.CC)]
		private static extern SteamAPICall_t _RequestUserStats( IntPtr self, SteamId steamIDUser );
		
		#endregion
		internal CallResult<UserStatsReceived_t> RequestUserStats( SteamId steamIDUser )
		{
			var returnValue = _RequestUserStats( Self, steamIDUser );
			return new CallResult<UserStatsReceived_t>( returnValue, IsServer );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamUserStats_GetUserStatInt32", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _GetUserStat( IntPtr self, SteamId steamIDUser, IntPtr pchName, ref int pData );
		
		#endregion
		internal bool GetUserStat( SteamId steamIDUser, string pchName, ref int pData )
		{
			using var str__pchName = new Utf8StringToNative( pchName );
			var returnValue = _GetUserStat( Self, steamIDUser, str__pchName.Pointer, ref pData );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamUserStats_GetUserStatFloat", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _GetUserStat( IntPtr self, SteamId steamIDUser, IntPtr pchName, ref float pData );
		
		#endregion
		internal bool GetUserStat( SteamId steamIDUser, string pchName, ref float pData )
		{
			using var str__pchName = new Utf8StringToNative( pchName );
			var returnValue = _GetUserStat( Self, steamIDUser, str__pchName.Pointer, ref pData );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamUserStats_GetUserAchievement", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _GetUserAchievement( IntPtr self, SteamId steamIDUser, IntPtr pchName, [MarshalAs( UnmanagedType.U1 )] ref bool pbAchieved );
		
		#endregion
		internal bool GetUserAchievement( SteamId steamIDUser, string pchName, [MarshalAs( UnmanagedType.U1 )] ref bool pbAchieved )
		{
			using var str__pchName = new Utf8StringToNative( pchName );
			var returnValue = _GetUserAchievement( Self, steamIDUser, str__pchName.Pointer, ref pbAchieved );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamUserStats_GetUserAchievementAndUnlockTime", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _GetUserAchievementAndUnlockTime( IntPtr self, SteamId steamIDUser, IntPtr pchName, [MarshalAs( UnmanagedType.U1 )] ref bool pbAchieved, ref uint punUnlockTime );
		
		#endregion
		internal bool GetUserAchievementAndUnlockTime( SteamId steamIDUser, string pchName, [MarshalAs( UnmanagedType.U1 )] ref bool pbAchieved, ref uint punUnlockTime )
		{
			using var str__pchName = new Utf8StringToNative( pchName );
			var returnValue = _GetUserAchievementAndUnlockTime( Self, steamIDUser, str__pchName.Pointer, ref pbAchieved, ref punUnlockTime );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamUserStats_ResetAllStats", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _ResetAllStats( IntPtr self, [MarshalAs( UnmanagedType.U1 )] bool bAchievementsToo );
		
		#endregion
		internal bool ResetAllStats( [MarshalAs( UnmanagedType.U1 )] bool bAchievementsToo )
		{
			var returnValue = _ResetAllStats( Self, bAchievementsToo );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamUserStats_FindOrCreateLeaderboard", CallingConvention = Platform.CC)]
		private static extern SteamAPICall_t _FindOrCreateLeaderboard( IntPtr self, IntPtr pchLeaderboardName, LeaderboardSort eLeaderboardSortMethod, LeaderboardDisplay eLeaderboardDisplayType );
		
		#endregion
		internal CallResult<LeaderboardFindResult_t> FindOrCreateLeaderboard( string pchLeaderboardName, LeaderboardSort eLeaderboardSortMethod, LeaderboardDisplay eLeaderboardDisplayType )
		{
			using var str__pchLeaderboardName = new Utf8StringToNative( pchLeaderboardName );
			var returnValue = _FindOrCreateLeaderboard( Self, str__pchLeaderboardName.Pointer, eLeaderboardSortMethod, eLeaderboardDisplayType );
			return new CallResult<LeaderboardFindResult_t>( returnValue, IsServer );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamUserStats_FindLeaderboard", CallingConvention = Platform.CC)]
		private static extern SteamAPICall_t _FindLeaderboard( IntPtr self, IntPtr pchLeaderboardName );
		
		#endregion
		internal CallResult<LeaderboardFindResult_t> FindLeaderboard( string pchLeaderboardName )
		{
			using var str__pchLeaderboardName = new Utf8StringToNative( pchLeaderboardName );
			var returnValue = _FindLeaderboard( Self, str__pchLeaderboardName.Pointer );
			return new CallResult<LeaderboardFindResult_t>( returnValue, IsServer );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamUserStats_GetLeaderboardName", CallingConvention = Platform.CC)]
		private static extern Utf8StringPointer _GetLeaderboardName( IntPtr self, SteamLeaderboard_t hSteamLeaderboard );
		
		#endregion
		internal string GetLeaderboardName( SteamLeaderboard_t hSteamLeaderboard )
		{
			var returnValue = _GetLeaderboardName( Self, hSteamLeaderboard );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamUserStats_GetLeaderboardEntryCount", CallingConvention = Platform.CC)]
		private static extern int _GetLeaderboardEntryCount( IntPtr self, SteamLeaderboard_t hSteamLeaderboard );
		
		#endregion
		internal int GetLeaderboardEntryCount( SteamLeaderboard_t hSteamLeaderboard )
		{
			var returnValue = _GetLeaderboardEntryCount( Self, hSteamLeaderboard );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamUserStats_GetLeaderboardSortMethod", CallingConvention = Platform.CC)]
		private static extern LeaderboardSort _GetLeaderboardSortMethod( IntPtr self, SteamLeaderboard_t hSteamLeaderboard );
		
		#endregion
		internal LeaderboardSort GetLeaderboardSortMethod( SteamLeaderboard_t hSteamLeaderboard )
		{
			var returnValue = _GetLeaderboardSortMethod( Self, hSteamLeaderboard );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamUserStats_GetLeaderboardDisplayType", CallingConvention = Platform.CC)]
		private static extern LeaderboardDisplay _GetLeaderboardDisplayType( IntPtr self, SteamLeaderboard_t hSteamLeaderboard );
		
		#endregion
		internal LeaderboardDisplay GetLeaderboardDisplayType( SteamLeaderboard_t hSteamLeaderboard )
		{
			var returnValue = _GetLeaderboardDisplayType( Self, hSteamLeaderboard );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamUserStats_DownloadLeaderboardEntries", CallingConvention = Platform.CC)]
		private static extern SteamAPICall_t _DownloadLeaderboardEntries( IntPtr self, SteamLeaderboard_t hSteamLeaderboard, LeaderboardDataRequest eLeaderboardDataRequest, int nRangeStart, int nRangeEnd );
		
		#endregion
		internal CallResult<LeaderboardScoresDownloaded_t> DownloadLeaderboardEntries( SteamLeaderboard_t hSteamLeaderboard, LeaderboardDataRequest eLeaderboardDataRequest, int nRangeStart, int nRangeEnd )
		{
			var returnValue = _DownloadLeaderboardEntries( Self, hSteamLeaderboard, eLeaderboardDataRequest, nRangeStart, nRangeEnd );
			return new CallResult<LeaderboardScoresDownloaded_t>( returnValue, IsServer );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamUserStats_DownloadLeaderboardEntriesForUsers", CallingConvention = Platform.CC)]
		private static extern SteamAPICall_t _DownloadLeaderboardEntriesForUsers( IntPtr self, SteamLeaderboard_t hSteamLeaderboard, [In,Out] SteamId[]  prgUsers, int cUsers );
		
		#endregion
		internal CallResult<LeaderboardScoresDownloaded_t> DownloadLeaderboardEntriesForUsers( SteamLeaderboard_t hSteamLeaderboard, [In,Out] SteamId[]  prgUsers, int cUsers )
		{
			var returnValue = _DownloadLeaderboardEntriesForUsers( Self, hSteamLeaderboard, prgUsers, cUsers );
			return new CallResult<LeaderboardScoresDownloaded_t>( returnValue, IsServer );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamUserStats_GetDownloadedLeaderboardEntry", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _GetDownloadedLeaderboardEntry( IntPtr self, SteamLeaderboardEntries_t hSteamLeaderboardEntries, int index, ref LeaderboardEntry_t pLeaderboardEntry, [In,Out] int[]  pDetails, int cDetailsMax );
		
		#endregion
		internal bool GetDownloadedLeaderboardEntry( SteamLeaderboardEntries_t hSteamLeaderboardEntries, int index, ref LeaderboardEntry_t pLeaderboardEntry, [In,Out] int[]  pDetails, int cDetailsMax )
		{
			var returnValue = _GetDownloadedLeaderboardEntry( Self, hSteamLeaderboardEntries, index, ref pLeaderboardEntry, pDetails, cDetailsMax );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamUserStats_UploadLeaderboardScore", CallingConvention = Platform.CC)]
		private static extern SteamAPICall_t _UploadLeaderboardScore( IntPtr self, SteamLeaderboard_t hSteamLeaderboard, LeaderboardUploadScoreMethod eLeaderboardUploadScoreMethod, int nScore, [In,Out] int[]  pScoreDetails, int cScoreDetailsCount );
		
		#endregion
		internal CallResult<LeaderboardScoreUploaded_t> UploadLeaderboardScore( SteamLeaderboard_t hSteamLeaderboard, LeaderboardUploadScoreMethod eLeaderboardUploadScoreMethod, int nScore, [In,Out] int[]  pScoreDetails, int cScoreDetailsCount )
		{
			var returnValue = _UploadLeaderboardScore( Self, hSteamLeaderboard, eLeaderboardUploadScoreMethod, nScore, pScoreDetails, cScoreDetailsCount );
			return new CallResult<LeaderboardScoreUploaded_t>( returnValue, IsServer );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamUserStats_AttachLeaderboardUGC", CallingConvention = Platform.CC)]
		private static extern SteamAPICall_t _AttachLeaderboardUGC( IntPtr self, SteamLeaderboard_t hSteamLeaderboard, UGCHandle_t hUGC );
		
		#endregion
		internal CallResult<LeaderboardUGCSet_t> AttachLeaderboardUGC( SteamLeaderboard_t hSteamLeaderboard, UGCHandle_t hUGC )
		{
			var returnValue = _AttachLeaderboardUGC( Self, hSteamLeaderboard, hUGC );
			return new CallResult<LeaderboardUGCSet_t>( returnValue, IsServer );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamUserStats_GetNumberOfCurrentPlayers", CallingConvention = Platform.CC)]
		private static extern SteamAPICall_t _GetNumberOfCurrentPlayers( IntPtr self );
		
		#endregion
		internal CallResult<NumberOfCurrentPlayers_t> GetNumberOfCurrentPlayers()
		{
			var returnValue = _GetNumberOfCurrentPlayers( Self );
			return new CallResult<NumberOfCurrentPlayers_t>( returnValue, IsServer );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamUserStats_RequestGlobalAchievementPercentages", CallingConvention = Platform.CC)]
		private static extern SteamAPICall_t _RequestGlobalAchievementPercentages( IntPtr self );
		
		#endregion
		internal CallResult<GlobalAchievementPercentagesReady_t> RequestGlobalAchievementPercentages()
		{
			var returnValue = _RequestGlobalAchievementPercentages( Self );
			return new CallResult<GlobalAchievementPercentagesReady_t>( returnValue, IsServer );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamUserStats_GetMostAchievedAchievementInfo", CallingConvention = Platform.CC)]
		private static extern int _GetMostAchievedAchievementInfo( IntPtr self, IntPtr pchName, uint unNameBufLen, ref float pflPercent, [MarshalAs( UnmanagedType.U1 )] ref bool pbAchieved );
		
		#endregion
		internal int GetMostAchievedAchievementInfo( out string pchName, ref float pflPercent, [MarshalAs( UnmanagedType.U1 )] ref bool pbAchieved )
		{
			using var mem__pchName = Helpers.TakeMemory();
			var returnValue = _GetMostAchievedAchievementInfo( Self, mem__pchName, (1024 * 32), ref pflPercent, ref pbAchieved );
			pchName = Helpers.MemoryToString( mem__pchName );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamUserStats_GetNextMostAchievedAchievementInfo", CallingConvention = Platform.CC)]
		private static extern int _GetNextMostAchievedAchievementInfo( IntPtr self, int iIteratorPrevious, IntPtr pchName, uint unNameBufLen, ref float pflPercent, [MarshalAs( UnmanagedType.U1 )] ref bool pbAchieved );
		
		#endregion
		internal int GetNextMostAchievedAchievementInfo( int iIteratorPrevious, out string pchName, ref float pflPercent, [MarshalAs( UnmanagedType.U1 )] ref bool pbAchieved )
		{
			using var mem__pchName = Helpers.TakeMemory();
			var returnValue = _GetNextMostAchievedAchievementInfo( Self, iIteratorPrevious, mem__pchName, (1024 * 32), ref pflPercent, ref pbAchieved );
			pchName = Helpers.MemoryToString( mem__pchName );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamUserStats_GetAchievementAchievedPercent", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _GetAchievementAchievedPercent( IntPtr self, IntPtr pchName, ref float pflPercent );
		
		#endregion
		internal bool GetAchievementAchievedPercent( string pchName, ref float pflPercent )
		{
			using var str__pchName = new Utf8StringToNative( pchName );
			var returnValue = _GetAchievementAchievedPercent( Self, str__pchName.Pointer, ref pflPercent );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamUserStats_RequestGlobalStats", CallingConvention = Platform.CC)]
		private static extern SteamAPICall_t _RequestGlobalStats( IntPtr self, int nHistoryDays );
		
		#endregion
		internal CallResult<GlobalStatsReceived_t> RequestGlobalStats( int nHistoryDays )
		{
			var returnValue = _RequestGlobalStats( Self, nHistoryDays );
			return new CallResult<GlobalStatsReceived_t>( returnValue, IsServer );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamUserStats_GetGlobalStatInt64", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _GetGlobalStat( IntPtr self, IntPtr pchStatName, ref long pData );
		
		#endregion
		internal bool GetGlobalStat( string pchStatName, ref long pData )
		{
			using var str__pchStatName = new Utf8StringToNative( pchStatName );
			var returnValue = _GetGlobalStat( Self, str__pchStatName.Pointer, ref pData );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamUserStats_GetGlobalStatDouble", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _GetGlobalStat( IntPtr self, IntPtr pchStatName, ref double pData );
		
		#endregion
		internal bool GetGlobalStat( string pchStatName, ref double pData )
		{
			using var str__pchStatName = new Utf8StringToNative( pchStatName );
			var returnValue = _GetGlobalStat( Self, str__pchStatName.Pointer, ref pData );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamUserStats_GetGlobalStatHistoryInt64", CallingConvention = Platform.CC)]
		private static extern int _GetGlobalStatHistory( IntPtr self, IntPtr pchStatName, [In,Out] long[]  pData, uint cubData );
		
		#endregion
		internal int GetGlobalStatHistory( string pchStatName, [In,Out] long[]  pData, uint cubData )
		{
			using var str__pchStatName = new Utf8StringToNative( pchStatName );
			var returnValue = _GetGlobalStatHistory( Self, str__pchStatName.Pointer, pData, cubData );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamUserStats_GetGlobalStatHistoryDouble", CallingConvention = Platform.CC)]
		private static extern int _GetGlobalStatHistory( IntPtr self, IntPtr pchStatName, [In,Out] double[]  pData, uint cubData );
		
		#endregion
		internal int GetGlobalStatHistory( string pchStatName, [In,Out] double[]  pData, uint cubData )
		{
			using var str__pchStatName = new Utf8StringToNative( pchStatName );
			var returnValue = _GetGlobalStatHistory( Self, str__pchStatName.Pointer, pData, cubData );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamUserStats_GetAchievementProgressLimitsInt32", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _GetAchievementProgressLimits( IntPtr self, IntPtr pchName, ref int pnMinProgress, ref int pnMaxProgress );
		
		#endregion
		internal bool GetAchievementProgressLimits( string pchName, ref int pnMinProgress, ref int pnMaxProgress )
		{
			using var str__pchName = new Utf8StringToNative( pchName );
			var returnValue = _GetAchievementProgressLimits( Self, str__pchName.Pointer, ref pnMinProgress, ref pnMaxProgress );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamUserStats_GetAchievementProgressLimitsFloat", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _GetAchievementProgressLimits( IntPtr self, IntPtr pchName, ref float pfMinProgress, ref float pfMaxProgress );
		
		#endregion
		internal bool GetAchievementProgressLimits( string pchName, ref float pfMinProgress, ref float pfMaxProgress )
		{
			using var str__pchName = new Utf8StringToNative( pchName );
			var returnValue = _GetAchievementProgressLimits( Self, str__pchName.Pointer, ref pfMinProgress, ref pfMaxProgress );
			return returnValue;
		}
		
	}
}
