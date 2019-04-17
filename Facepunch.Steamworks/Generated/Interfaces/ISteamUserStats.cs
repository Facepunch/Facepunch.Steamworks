using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Steamworks.Data;


namespace Steamworks
{
	internal class ISteamUserStats : SteamInterface
	{
		public ISteamUserStats( bool server = false ) : base( server )
		{
		}
		
		public override string InterfaceName => "STEAMUSERSTATS_INTERFACE_VERSION011";
		
		public override void InitInternals()
		{
			RequestCurrentStatsDelegatePointer = Marshal.GetDelegateForFunctionPointer<RequestCurrentStatsDelegate>( Marshal.ReadIntPtr( VTable, 0) );
			GetStat1DelegatePointer = Marshal.GetDelegateForFunctionPointer<GetStat1Delegate>( Marshal.ReadIntPtr( VTable, 8) );
			GetStat2DelegatePointer = Marshal.GetDelegateForFunctionPointer<GetStat2Delegate>( Marshal.ReadIntPtr( VTable, 16) );
			SetStat1DelegatePointer = Marshal.GetDelegateForFunctionPointer<SetStat1Delegate>( Marshal.ReadIntPtr( VTable, 24) );
			SetStat2DelegatePointer = Marshal.GetDelegateForFunctionPointer<SetStat2Delegate>( Marshal.ReadIntPtr( VTable, 32) );
			UpdateAvgRateStatDelegatePointer = Marshal.GetDelegateForFunctionPointer<UpdateAvgRateStatDelegate>( Marshal.ReadIntPtr( VTable, 40) );
			GetAchievementDelegatePointer = Marshal.GetDelegateForFunctionPointer<GetAchievementDelegate>( Marshal.ReadIntPtr( VTable, 48) );
			SetAchievementDelegatePointer = Marshal.GetDelegateForFunctionPointer<SetAchievementDelegate>( Marshal.ReadIntPtr( VTable, 56) );
			ClearAchievementDelegatePointer = Marshal.GetDelegateForFunctionPointer<ClearAchievementDelegate>( Marshal.ReadIntPtr( VTable, 64) );
			GetAchievementAndUnlockTimeDelegatePointer = Marshal.GetDelegateForFunctionPointer<GetAchievementAndUnlockTimeDelegate>( Marshal.ReadIntPtr( VTable, 72) );
			StoreStatsDelegatePointer = Marshal.GetDelegateForFunctionPointer<StoreStatsDelegate>( Marshal.ReadIntPtr( VTable, 80) );
			GetAchievementIconDelegatePointer = Marshal.GetDelegateForFunctionPointer<GetAchievementIconDelegate>( Marshal.ReadIntPtr( VTable, 88) );
			GetAchievementDisplayAttributeDelegatePointer = Marshal.GetDelegateForFunctionPointer<GetAchievementDisplayAttributeDelegate>( Marshal.ReadIntPtr( VTable, 96) );
			IndicateAchievementProgressDelegatePointer = Marshal.GetDelegateForFunctionPointer<IndicateAchievementProgressDelegate>( Marshal.ReadIntPtr( VTable, 104) );
			GetNumAchievementsDelegatePointer = Marshal.GetDelegateForFunctionPointer<GetNumAchievementsDelegate>( Marshal.ReadIntPtr( VTable, 112) );
			GetAchievementNameDelegatePointer = Marshal.GetDelegateForFunctionPointer<GetAchievementNameDelegate>( Marshal.ReadIntPtr( VTable, 120) );
			RequestUserStatsDelegatePointer = Marshal.GetDelegateForFunctionPointer<RequestUserStatsDelegate>( Marshal.ReadIntPtr( VTable, 128) );
			GetUserStat1DelegatePointer = Marshal.GetDelegateForFunctionPointer<GetUserStat1Delegate>( Marshal.ReadIntPtr( VTable, 136) );
			GetUserStat2DelegatePointer = Marshal.GetDelegateForFunctionPointer<GetUserStat2Delegate>( Marshal.ReadIntPtr( VTable, 144) );
			GetUserAchievementDelegatePointer = Marshal.GetDelegateForFunctionPointer<GetUserAchievementDelegate>( Marshal.ReadIntPtr( VTable, 152) );
			GetUserAchievementAndUnlockTimeDelegatePointer = Marshal.GetDelegateForFunctionPointer<GetUserAchievementAndUnlockTimeDelegate>( Marshal.ReadIntPtr( VTable, 160) );
			ResetAllStatsDelegatePointer = Marshal.GetDelegateForFunctionPointer<ResetAllStatsDelegate>( Marshal.ReadIntPtr( VTable, 168) );
			FindOrCreateLeaderboardDelegatePointer = Marshal.GetDelegateForFunctionPointer<FindOrCreateLeaderboardDelegate>( Marshal.ReadIntPtr( VTable, 176) );
			FindLeaderboardDelegatePointer = Marshal.GetDelegateForFunctionPointer<FindLeaderboardDelegate>( Marshal.ReadIntPtr( VTable, 184) );
			GetLeaderboardNameDelegatePointer = Marshal.GetDelegateForFunctionPointer<GetLeaderboardNameDelegate>( Marshal.ReadIntPtr( VTable, 192) );
			GetLeaderboardEntryCountDelegatePointer = Marshal.GetDelegateForFunctionPointer<GetLeaderboardEntryCountDelegate>( Marshal.ReadIntPtr( VTable, 200) );
			GetLeaderboardSortMethodDelegatePointer = Marshal.GetDelegateForFunctionPointer<GetLeaderboardSortMethodDelegate>( Marshal.ReadIntPtr( VTable, 208) );
			GetLeaderboardDisplayTypeDelegatePointer = Marshal.GetDelegateForFunctionPointer<GetLeaderboardDisplayTypeDelegate>( Marshal.ReadIntPtr( VTable, 216) );
			DownloadLeaderboardEntriesDelegatePointer = Marshal.GetDelegateForFunctionPointer<DownloadLeaderboardEntriesDelegate>( Marshal.ReadIntPtr( VTable, 224) );
			DownloadLeaderboardEntriesForUsersDelegatePointer = Marshal.GetDelegateForFunctionPointer<DownloadLeaderboardEntriesForUsersDelegate>( Marshal.ReadIntPtr( VTable, 232) );
			GetDownloadedLeaderboardEntryDelegatePointer = Marshal.GetDelegateForFunctionPointer<GetDownloadedLeaderboardEntryDelegate>( Marshal.ReadIntPtr( VTable, 240) );
			UploadLeaderboardScoreDelegatePointer = Marshal.GetDelegateForFunctionPointer<UploadLeaderboardScoreDelegate>( Marshal.ReadIntPtr( VTable, 248) );
			AttachLeaderboardUGCDelegatePointer = Marshal.GetDelegateForFunctionPointer<AttachLeaderboardUGCDelegate>( Marshal.ReadIntPtr( VTable, 256) );
			GetNumberOfCurrentPlayersDelegatePointer = Marshal.GetDelegateForFunctionPointer<GetNumberOfCurrentPlayersDelegate>( Marshal.ReadIntPtr( VTable, 264) );
			RequestGlobalAchievementPercentagesDelegatePointer = Marshal.GetDelegateForFunctionPointer<RequestGlobalAchievementPercentagesDelegate>( Marshal.ReadIntPtr( VTable, 272) );
			GetMostAchievedAchievementInfoDelegatePointer = Marshal.GetDelegateForFunctionPointer<GetMostAchievedAchievementInfoDelegate>( Marshal.ReadIntPtr( VTable, 280) );
			GetNextMostAchievedAchievementInfoDelegatePointer = Marshal.GetDelegateForFunctionPointer<GetNextMostAchievedAchievementInfoDelegate>( Marshal.ReadIntPtr( VTable, 288) );
			GetAchievementAchievedPercentDelegatePointer = Marshal.GetDelegateForFunctionPointer<GetAchievementAchievedPercentDelegate>( Marshal.ReadIntPtr( VTable, 296) );
			RequestGlobalStatsDelegatePointer = Marshal.GetDelegateForFunctionPointer<RequestGlobalStatsDelegate>( Marshal.ReadIntPtr( VTable, 304) );
			GetGlobalStat1DelegatePointer = Marshal.GetDelegateForFunctionPointer<GetGlobalStat1Delegate>( Marshal.ReadIntPtr( VTable, 312) );
			GetGlobalStat2DelegatePointer = Marshal.GetDelegateForFunctionPointer<GetGlobalStat2Delegate>( Marshal.ReadIntPtr( VTable, 320) );
			GetGlobalStatHistory1DelegatePointer = Marshal.GetDelegateForFunctionPointer<GetGlobalStatHistory1Delegate>( Marshal.ReadIntPtr( VTable, 328) );
			GetGlobalStatHistory2DelegatePointer = Marshal.GetDelegateForFunctionPointer<GetGlobalStatHistory2Delegate>( Marshal.ReadIntPtr( VTable, 336) );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool RequestCurrentStatsDelegate( IntPtr self );
		private RequestCurrentStatsDelegate RequestCurrentStatsDelegatePointer;
		
		#endregion
		internal bool RequestCurrentStats()
		{
			return RequestCurrentStatsDelegatePointer( Self );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool GetStat1Delegate( IntPtr self, string pchName, ref int pData );
		private GetStat1Delegate GetStat1DelegatePointer;
		
		#endregion
		internal bool GetStat1( string pchName, ref int pData )
		{
			return GetStat1DelegatePointer( Self, pchName, ref pData );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool GetStat2Delegate( IntPtr self, string pchName, ref float pData );
		private GetStat2Delegate GetStat2DelegatePointer;
		
		#endregion
		internal bool GetStat2( string pchName, ref float pData )
		{
			return GetStat2DelegatePointer( Self, pchName, ref pData );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool SetStat1Delegate( IntPtr self, string pchName, int nData );
		private SetStat1Delegate SetStat1DelegatePointer;
		
		#endregion
		internal bool SetStat1( string pchName, int nData )
		{
			return SetStat1DelegatePointer( Self, pchName, nData );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool SetStat2Delegate( IntPtr self, string pchName, float fData );
		private SetStat2Delegate SetStat2DelegatePointer;
		
		#endregion
		internal bool SetStat2( string pchName, float fData )
		{
			return SetStat2DelegatePointer( Self, pchName, fData );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool UpdateAvgRateStatDelegate( IntPtr self, string pchName, float flCountThisSession, double dSessionLength );
		private UpdateAvgRateStatDelegate UpdateAvgRateStatDelegatePointer;
		
		#endregion
		internal bool UpdateAvgRateStat( string pchName, float flCountThisSession, double dSessionLength )
		{
			return UpdateAvgRateStatDelegatePointer( Self, pchName, flCountThisSession, dSessionLength );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool GetAchievementDelegate( IntPtr self, string pchName, [MarshalAs( UnmanagedType.U1 )] ref bool pbAchieved );
		private GetAchievementDelegate GetAchievementDelegatePointer;
		
		#endregion
		internal bool GetAchievement( string pchName, [MarshalAs( UnmanagedType.U1 )] ref bool pbAchieved )
		{
			return GetAchievementDelegatePointer( Self, pchName, ref pbAchieved );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool SetAchievementDelegate( IntPtr self, string pchName );
		private SetAchievementDelegate SetAchievementDelegatePointer;
		
		#endregion
		internal bool SetAchievement( string pchName )
		{
			return SetAchievementDelegatePointer( Self, pchName );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool ClearAchievementDelegate( IntPtr self, string pchName );
		private ClearAchievementDelegate ClearAchievementDelegatePointer;
		
		#endregion
		internal bool ClearAchievement( string pchName )
		{
			return ClearAchievementDelegatePointer( Self, pchName );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool GetAchievementAndUnlockTimeDelegate( IntPtr self, string pchName, [MarshalAs( UnmanagedType.U1 )] ref bool pbAchieved, ref uint punUnlockTime );
		private GetAchievementAndUnlockTimeDelegate GetAchievementAndUnlockTimeDelegatePointer;
		
		#endregion
		internal bool GetAchievementAndUnlockTime( string pchName, [MarshalAs( UnmanagedType.U1 )] ref bool pbAchieved, ref uint punUnlockTime )
		{
			return GetAchievementAndUnlockTimeDelegatePointer( Self, pchName, ref pbAchieved, ref punUnlockTime );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool StoreStatsDelegate( IntPtr self );
		private StoreStatsDelegate StoreStatsDelegatePointer;
		
		#endregion
		internal bool StoreStats()
		{
			return StoreStatsDelegatePointer( Self );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate int GetAchievementIconDelegate( IntPtr self, string pchName );
		private GetAchievementIconDelegate GetAchievementIconDelegatePointer;
		
		#endregion
		internal int GetAchievementIcon( string pchName )
		{
			return GetAchievementIconDelegatePointer( Self, pchName );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate IntPtr GetAchievementDisplayAttributeDelegate( IntPtr self, string pchName, string pchKey );
		private GetAchievementDisplayAttributeDelegate GetAchievementDisplayAttributeDelegatePointer;
		
		#endregion
		internal string GetAchievementDisplayAttribute( string pchName, string pchKey )
		{
			return GetString( GetAchievementDisplayAttributeDelegatePointer( Self, pchName, pchKey ) );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool IndicateAchievementProgressDelegate( IntPtr self, string pchName, uint nCurProgress, uint nMaxProgress );
		private IndicateAchievementProgressDelegate IndicateAchievementProgressDelegatePointer;
		
		#endregion
		internal bool IndicateAchievementProgress( string pchName, uint nCurProgress, uint nMaxProgress )
		{
			return IndicateAchievementProgressDelegatePointer( Self, pchName, nCurProgress, nMaxProgress );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate uint GetNumAchievementsDelegate( IntPtr self );
		private GetNumAchievementsDelegate GetNumAchievementsDelegatePointer;
		
		#endregion
		internal uint GetNumAchievements()
		{
			return GetNumAchievementsDelegatePointer( Self );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate IntPtr GetAchievementNameDelegate( IntPtr self, uint iAchievement );
		private GetAchievementNameDelegate GetAchievementNameDelegatePointer;
		
		#endregion
		internal string GetAchievementName( uint iAchievement )
		{
			return GetString( GetAchievementNameDelegatePointer( Self, iAchievement ) );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate SteamAPICall_t RequestUserStatsDelegate( IntPtr self, SteamId steamIDUser );
		private RequestUserStatsDelegate RequestUserStatsDelegatePointer;
		
		#endregion
		internal async Task<UserStatsReceived_t?> RequestUserStats( SteamId steamIDUser )
		{
			return await (new Result<UserStatsReceived_t>( RequestUserStatsDelegatePointer( Self, steamIDUser ) )).GetResult();
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool GetUserStat1Delegate( IntPtr self, SteamId steamIDUser, string pchName, ref int pData );
		private GetUserStat1Delegate GetUserStat1DelegatePointer;
		
		#endregion
		internal bool GetUserStat1( SteamId steamIDUser, string pchName, ref int pData )
		{
			return GetUserStat1DelegatePointer( Self, steamIDUser, pchName, ref pData );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool GetUserStat2Delegate( IntPtr self, SteamId steamIDUser, string pchName, ref float pData );
		private GetUserStat2Delegate GetUserStat2DelegatePointer;
		
		#endregion
		internal bool GetUserStat2( SteamId steamIDUser, string pchName, ref float pData )
		{
			return GetUserStat2DelegatePointer( Self, steamIDUser, pchName, ref pData );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool GetUserAchievementDelegate( IntPtr self, SteamId steamIDUser, string pchName, [MarshalAs( UnmanagedType.U1 )] ref bool pbAchieved );
		private GetUserAchievementDelegate GetUserAchievementDelegatePointer;
		
		#endregion
		internal bool GetUserAchievement( SteamId steamIDUser, string pchName, [MarshalAs( UnmanagedType.U1 )] ref bool pbAchieved )
		{
			return GetUserAchievementDelegatePointer( Self, steamIDUser, pchName, ref pbAchieved );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool GetUserAchievementAndUnlockTimeDelegate( IntPtr self, SteamId steamIDUser, string pchName, [MarshalAs( UnmanagedType.U1 )] ref bool pbAchieved, ref uint punUnlockTime );
		private GetUserAchievementAndUnlockTimeDelegate GetUserAchievementAndUnlockTimeDelegatePointer;
		
		#endregion
		internal bool GetUserAchievementAndUnlockTime( SteamId steamIDUser, string pchName, [MarshalAs( UnmanagedType.U1 )] ref bool pbAchieved, ref uint punUnlockTime )
		{
			return GetUserAchievementAndUnlockTimeDelegatePointer( Self, steamIDUser, pchName, ref pbAchieved, ref punUnlockTime );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool ResetAllStatsDelegate( IntPtr self, [MarshalAs( UnmanagedType.U1 )] bool bAchievementsToo );
		private ResetAllStatsDelegate ResetAllStatsDelegatePointer;
		
		#endregion
		internal bool ResetAllStats( [MarshalAs( UnmanagedType.U1 )] bool bAchievementsToo )
		{
			return ResetAllStatsDelegatePointer( Self, bAchievementsToo );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate SteamAPICall_t FindOrCreateLeaderboardDelegate( IntPtr self, string pchLeaderboardName, LeaderboardSort eLeaderboardSortMethod, LeaderboardDisplay eLeaderboardDisplayType );
		private FindOrCreateLeaderboardDelegate FindOrCreateLeaderboardDelegatePointer;
		
		#endregion
		internal async Task<LeaderboardFindResult_t?> FindOrCreateLeaderboard( string pchLeaderboardName, LeaderboardSort eLeaderboardSortMethod, LeaderboardDisplay eLeaderboardDisplayType )
		{
			return await (new Result<LeaderboardFindResult_t>( FindOrCreateLeaderboardDelegatePointer( Self, pchLeaderboardName, eLeaderboardSortMethod, eLeaderboardDisplayType ) )).GetResult();
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate SteamAPICall_t FindLeaderboardDelegate( IntPtr self, string pchLeaderboardName );
		private FindLeaderboardDelegate FindLeaderboardDelegatePointer;
		
		#endregion
		internal async Task<LeaderboardFindResult_t?> FindLeaderboard( string pchLeaderboardName )
		{
			return await (new Result<LeaderboardFindResult_t>( FindLeaderboardDelegatePointer( Self, pchLeaderboardName ) )).GetResult();
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate IntPtr GetLeaderboardNameDelegate( IntPtr self, SteamLeaderboard_t hSteamLeaderboard );
		private GetLeaderboardNameDelegate GetLeaderboardNameDelegatePointer;
		
		#endregion
		internal string GetLeaderboardName( SteamLeaderboard_t hSteamLeaderboard )
		{
			return GetString( GetLeaderboardNameDelegatePointer( Self, hSteamLeaderboard ) );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate int GetLeaderboardEntryCountDelegate( IntPtr self, SteamLeaderboard_t hSteamLeaderboard );
		private GetLeaderboardEntryCountDelegate GetLeaderboardEntryCountDelegatePointer;
		
		#endregion
		internal int GetLeaderboardEntryCount( SteamLeaderboard_t hSteamLeaderboard )
		{
			return GetLeaderboardEntryCountDelegatePointer( Self, hSteamLeaderboard );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate LeaderboardSort GetLeaderboardSortMethodDelegate( IntPtr self, SteamLeaderboard_t hSteamLeaderboard );
		private GetLeaderboardSortMethodDelegate GetLeaderboardSortMethodDelegatePointer;
		
		#endregion
		internal LeaderboardSort GetLeaderboardSortMethod( SteamLeaderboard_t hSteamLeaderboard )
		{
			return GetLeaderboardSortMethodDelegatePointer( Self, hSteamLeaderboard );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate LeaderboardDisplay GetLeaderboardDisplayTypeDelegate( IntPtr self, SteamLeaderboard_t hSteamLeaderboard );
		private GetLeaderboardDisplayTypeDelegate GetLeaderboardDisplayTypeDelegatePointer;
		
		#endregion
		internal LeaderboardDisplay GetLeaderboardDisplayType( SteamLeaderboard_t hSteamLeaderboard )
		{
			return GetLeaderboardDisplayTypeDelegatePointer( Self, hSteamLeaderboard );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate SteamAPICall_t DownloadLeaderboardEntriesDelegate( IntPtr self, SteamLeaderboard_t hSteamLeaderboard, LeaderboardDataRequest eLeaderboardDataRequest, int nRangeStart, int nRangeEnd );
		private DownloadLeaderboardEntriesDelegate DownloadLeaderboardEntriesDelegatePointer;
		
		#endregion
		internal async Task<LeaderboardScoresDownloaded_t?> DownloadLeaderboardEntries( SteamLeaderboard_t hSteamLeaderboard, LeaderboardDataRequest eLeaderboardDataRequest, int nRangeStart, int nRangeEnd )
		{
			return await (new Result<LeaderboardScoresDownloaded_t>( DownloadLeaderboardEntriesDelegatePointer( Self, hSteamLeaderboard, eLeaderboardDataRequest, nRangeStart, nRangeEnd ) )).GetResult();
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate SteamAPICall_t DownloadLeaderboardEntriesForUsersDelegate( IntPtr self, SteamLeaderboard_t hSteamLeaderboard, [In,Out] SteamId[]  prgUsers, int cUsers );
		private DownloadLeaderboardEntriesForUsersDelegate DownloadLeaderboardEntriesForUsersDelegatePointer;
		
		#endregion
		internal async Task<LeaderboardScoresDownloaded_t?> DownloadLeaderboardEntriesForUsers( SteamLeaderboard_t hSteamLeaderboard, [In,Out] SteamId[]  prgUsers, int cUsers )
		{
			return await (new Result<LeaderboardScoresDownloaded_t>( DownloadLeaderboardEntriesForUsersDelegatePointer( Self, hSteamLeaderboard, prgUsers, cUsers ) )).GetResult();
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool GetDownloadedLeaderboardEntryDelegate( IntPtr self, SteamLeaderboardEntries_t hSteamLeaderboardEntries, int index, ref LeaderboardEntry_t pLeaderboardEntry, [In,Out] int[]  pDetails, int cDetailsMax );
		private GetDownloadedLeaderboardEntryDelegate GetDownloadedLeaderboardEntryDelegatePointer;
		
		#endregion
		internal bool GetDownloadedLeaderboardEntry( SteamLeaderboardEntries_t hSteamLeaderboardEntries, int index, ref LeaderboardEntry_t pLeaderboardEntry, [In,Out] int[]  pDetails, int cDetailsMax )
		{
			return GetDownloadedLeaderboardEntryDelegatePointer( Self, hSteamLeaderboardEntries, index, ref pLeaderboardEntry, pDetails, cDetailsMax );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate SteamAPICall_t UploadLeaderboardScoreDelegate( IntPtr self, SteamLeaderboard_t hSteamLeaderboard, LeaderboardUploadScoreMethod eLeaderboardUploadScoreMethod, int nScore, [In,Out] int[]  pScoreDetails, int cScoreDetailsCount );
		private UploadLeaderboardScoreDelegate UploadLeaderboardScoreDelegatePointer;
		
		#endregion
		internal async Task<LeaderboardScoreUploaded_t?> UploadLeaderboardScore( SteamLeaderboard_t hSteamLeaderboard, LeaderboardUploadScoreMethod eLeaderboardUploadScoreMethod, int nScore, [In,Out] int[]  pScoreDetails, int cScoreDetailsCount )
		{
			return await (new Result<LeaderboardScoreUploaded_t>( UploadLeaderboardScoreDelegatePointer( Self, hSteamLeaderboard, eLeaderboardUploadScoreMethod, nScore, pScoreDetails, cScoreDetailsCount ) )).GetResult();
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate SteamAPICall_t AttachLeaderboardUGCDelegate( IntPtr self, SteamLeaderboard_t hSteamLeaderboard, UGCHandle_t hUGC );
		private AttachLeaderboardUGCDelegate AttachLeaderboardUGCDelegatePointer;
		
		#endregion
		internal async Task<LeaderboardUGCSet_t?> AttachLeaderboardUGC( SteamLeaderboard_t hSteamLeaderboard, UGCHandle_t hUGC )
		{
			return await (new Result<LeaderboardUGCSet_t>( AttachLeaderboardUGCDelegatePointer( Self, hSteamLeaderboard, hUGC ) )).GetResult();
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate SteamAPICall_t GetNumberOfCurrentPlayersDelegate( IntPtr self );
		private GetNumberOfCurrentPlayersDelegate GetNumberOfCurrentPlayersDelegatePointer;
		
		#endregion
		internal async Task<NumberOfCurrentPlayers_t?> GetNumberOfCurrentPlayers()
		{
			return await (new Result<NumberOfCurrentPlayers_t>( GetNumberOfCurrentPlayersDelegatePointer( Self ) )).GetResult();
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate SteamAPICall_t RequestGlobalAchievementPercentagesDelegate( IntPtr self );
		private RequestGlobalAchievementPercentagesDelegate RequestGlobalAchievementPercentagesDelegatePointer;
		
		#endregion
		internal async Task<GlobalAchievementPercentagesReady_t?> RequestGlobalAchievementPercentages()
		{
			return await (new Result<GlobalAchievementPercentagesReady_t>( RequestGlobalAchievementPercentagesDelegatePointer( Self ) )).GetResult();
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate int GetMostAchievedAchievementInfoDelegate( IntPtr self, StringBuilder pchName, uint unNameBufLen, ref float pflPercent, [MarshalAs( UnmanagedType.U1 )] ref bool pbAchieved );
		private GetMostAchievedAchievementInfoDelegate GetMostAchievedAchievementInfoDelegatePointer;
		
		#endregion
		internal int GetMostAchievedAchievementInfo( StringBuilder pchName, uint unNameBufLen, ref float pflPercent, [MarshalAs( UnmanagedType.U1 )] ref bool pbAchieved )
		{
			return GetMostAchievedAchievementInfoDelegatePointer( Self, pchName, unNameBufLen, ref pflPercent, ref pbAchieved );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate int GetNextMostAchievedAchievementInfoDelegate( IntPtr self, int iIteratorPrevious, StringBuilder pchName, uint unNameBufLen, ref float pflPercent, [MarshalAs( UnmanagedType.U1 )] ref bool pbAchieved );
		private GetNextMostAchievedAchievementInfoDelegate GetNextMostAchievedAchievementInfoDelegatePointer;
		
		#endregion
		internal int GetNextMostAchievedAchievementInfo( int iIteratorPrevious, StringBuilder pchName, uint unNameBufLen, ref float pflPercent, [MarshalAs( UnmanagedType.U1 )] ref bool pbAchieved )
		{
			return GetNextMostAchievedAchievementInfoDelegatePointer( Self, iIteratorPrevious, pchName, unNameBufLen, ref pflPercent, ref pbAchieved );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool GetAchievementAchievedPercentDelegate( IntPtr self, string pchName, ref float pflPercent );
		private GetAchievementAchievedPercentDelegate GetAchievementAchievedPercentDelegatePointer;
		
		#endregion
		internal bool GetAchievementAchievedPercent( string pchName, ref float pflPercent )
		{
			return GetAchievementAchievedPercentDelegatePointer( Self, pchName, ref pflPercent );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate SteamAPICall_t RequestGlobalStatsDelegate( IntPtr self, int nHistoryDays );
		private RequestGlobalStatsDelegate RequestGlobalStatsDelegatePointer;
		
		#endregion
		internal async Task<GlobalStatsReceived_t?> RequestGlobalStats( int nHistoryDays )
		{
			return await (new Result<GlobalStatsReceived_t>( RequestGlobalStatsDelegatePointer( Self, nHistoryDays ) )).GetResult();
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool GetGlobalStat1Delegate( IntPtr self, string pchStatName, ref long pData );
		private GetGlobalStat1Delegate GetGlobalStat1DelegatePointer;
		
		#endregion
		internal bool GetGlobalStat1( string pchStatName, ref long pData )
		{
			return GetGlobalStat1DelegatePointer( Self, pchStatName, ref pData );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool GetGlobalStat2Delegate( IntPtr self, string pchStatName, ref double pData );
		private GetGlobalStat2Delegate GetGlobalStat2DelegatePointer;
		
		#endregion
		internal bool GetGlobalStat2( string pchStatName, ref double pData )
		{
			return GetGlobalStat2DelegatePointer( Self, pchStatName, ref pData );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate int GetGlobalStatHistory1Delegate( IntPtr self, string pchStatName, [In,Out] long[]  pData, uint cubData );
		private GetGlobalStatHistory1Delegate GetGlobalStatHistory1DelegatePointer;
		
		#endregion
		internal int GetGlobalStatHistory1( string pchStatName, [In,Out] long[]  pData, uint cubData )
		{
			return GetGlobalStatHistory1DelegatePointer( Self, pchStatName, pData, cubData );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate int GetGlobalStatHistory2Delegate( IntPtr self, string pchStatName, [In,Out] double[]  pData, uint cubData );
		private GetGlobalStatHistory2Delegate GetGlobalStatHistory2DelegatePointer;
		
		#endregion
		internal int GetGlobalStatHistory2( string pchStatName, [In,Out] double[]  pData, uint cubData )
		{
			return GetGlobalStatHistory2DelegatePointer( Self, pchStatName, pData, cubData );
		}
		
	}
}
