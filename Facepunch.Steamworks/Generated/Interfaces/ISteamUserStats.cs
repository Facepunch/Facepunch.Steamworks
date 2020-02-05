using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Steamworks.Data;


namespace Steamworks
{
	internal class ISteamUserStats : SteamInterface
	{
		public override string InterfaceName => "STEAMUSERSTATS_INTERFACE_VERSION011";
		
		public override void InitInternals()
		{
			_RequestCurrentStats = Marshal.GetDelegateForFunctionPointer<FRequestCurrentStats>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 0 ) ) );
			_UpdateAvgRateStat = Marshal.GetDelegateForFunctionPointer<FUpdateAvgRateStat>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 40 ) ) );
			_GetAchievement = Marshal.GetDelegateForFunctionPointer<FGetAchievement>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 48 ) ) );
			_SetAchievement = Marshal.GetDelegateForFunctionPointer<FSetAchievement>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 56 ) ) );
			_ClearAchievement = Marshal.GetDelegateForFunctionPointer<FClearAchievement>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 64 ) ) );
			_GetAchievementAndUnlockTime = Marshal.GetDelegateForFunctionPointer<FGetAchievementAndUnlockTime>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 72 ) ) );
			_StoreStats = Marshal.GetDelegateForFunctionPointer<FStoreStats>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 80 ) ) );
			_GetAchievementIcon = Marshal.GetDelegateForFunctionPointer<FGetAchievementIcon>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 88 ) ) );
			_GetAchievementDisplayAttribute = Marshal.GetDelegateForFunctionPointer<FGetAchievementDisplayAttribute>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 96 ) ) );
			_IndicateAchievementProgress = Marshal.GetDelegateForFunctionPointer<FIndicateAchievementProgress>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 104 ) ) );
			_GetNumAchievements = Marshal.GetDelegateForFunctionPointer<FGetNumAchievements>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 112 ) ) );
			_GetAchievementName = Marshal.GetDelegateForFunctionPointer<FGetAchievementName>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 120 ) ) );
			_RequestUserStats = Marshal.GetDelegateForFunctionPointer<FRequestUserStats>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 128 ) ) );
			_GetUserAchievement = Marshal.GetDelegateForFunctionPointer<FGetUserAchievement>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 152 ) ) );
			_GetUserAchievementAndUnlockTime = Marshal.GetDelegateForFunctionPointer<FGetUserAchievementAndUnlockTime>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 160 ) ) );
			_ResetAllStats = Marshal.GetDelegateForFunctionPointer<FResetAllStats>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 168 ) ) );
			_FindOrCreateLeaderboard = Marshal.GetDelegateForFunctionPointer<FFindOrCreateLeaderboard>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 176 ) ) );
			_FindLeaderboard = Marshal.GetDelegateForFunctionPointer<FFindLeaderboard>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 184 ) ) );
			_GetLeaderboardName = Marshal.GetDelegateForFunctionPointer<FGetLeaderboardName>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 192 ) ) );
			_GetLeaderboardEntryCount = Marshal.GetDelegateForFunctionPointer<FGetLeaderboardEntryCount>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 200 ) ) );
			_GetLeaderboardSortMethod = Marshal.GetDelegateForFunctionPointer<FGetLeaderboardSortMethod>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 208 ) ) );
			_GetLeaderboardDisplayType = Marshal.GetDelegateForFunctionPointer<FGetLeaderboardDisplayType>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 216 ) ) );
			_DownloadLeaderboardEntries = Marshal.GetDelegateForFunctionPointer<FDownloadLeaderboardEntries>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 224 ) ) );
			_DownloadLeaderboardEntriesForUsers = Marshal.GetDelegateForFunctionPointer<FDownloadLeaderboardEntriesForUsers>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 232 ) ) );
			_GetDownloadedLeaderboardEntry = Marshal.GetDelegateForFunctionPointer<FGetDownloadedLeaderboardEntry>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 240 ) ) );
			_UploadLeaderboardScore = Marshal.GetDelegateForFunctionPointer<FUploadLeaderboardScore>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 248 ) ) );
			_AttachLeaderboardUGC = Marshal.GetDelegateForFunctionPointer<FAttachLeaderboardUGC>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 256 ) ) );
			_GetNumberOfCurrentPlayers = Marshal.GetDelegateForFunctionPointer<FGetNumberOfCurrentPlayers>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 264 ) ) );
			_RequestGlobalAchievementPercentages = Marshal.GetDelegateForFunctionPointer<FRequestGlobalAchievementPercentages>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 272 ) ) );
			_GetMostAchievedAchievementInfo = Marshal.GetDelegateForFunctionPointer<FGetMostAchievedAchievementInfo>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 280 ) ) );
			_GetNextMostAchievedAchievementInfo = Marshal.GetDelegateForFunctionPointer<FGetNextMostAchievedAchievementInfo>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 288 ) ) );
			_GetAchievementAchievedPercent = Marshal.GetDelegateForFunctionPointer<FGetAchievementAchievedPercent>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 296 ) ) );
			_RequestGlobalStats = Marshal.GetDelegateForFunctionPointer<FRequestGlobalStats>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 304 ) ) );
			
			#if PLATFORM_WIN
			_GetStat1 = Marshal.GetDelegateForFunctionPointer<FGetStat1>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 16 ) ) );
			_GetStat2 = Marshal.GetDelegateForFunctionPointer<FGetStat2>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 8 ) ) );
			_SetStat1 = Marshal.GetDelegateForFunctionPointer<FSetStat1>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 32 ) ) );
			_SetStat2 = Marshal.GetDelegateForFunctionPointer<FSetStat2>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 24 ) ) );
			_GetUserStat1 = Marshal.GetDelegateForFunctionPointer<FGetUserStat1>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 144 ) ) );
			_GetUserStat2 = Marshal.GetDelegateForFunctionPointer<FGetUserStat2>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 136 ) ) );
			_GetGlobalStat1 = Marshal.GetDelegateForFunctionPointer<FGetGlobalStat1>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 320 ) ) );
			_GetGlobalStat2 = Marshal.GetDelegateForFunctionPointer<FGetGlobalStat2>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 312 ) ) );
			_GetGlobalStatHistory1 = Marshal.GetDelegateForFunctionPointer<FGetGlobalStatHistory1>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 336 ) ) );
			_GetGlobalStatHistory2 = Marshal.GetDelegateForFunctionPointer<FGetGlobalStatHistory2>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 328 ) ) );
			#else
			_GetStat1 = Marshal.GetDelegateForFunctionPointer<FGetStat1>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 8 ) ) );
			_GetStat2 = Marshal.GetDelegateForFunctionPointer<FGetStat2>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 16 ) ) );
			_SetStat1 = Marshal.GetDelegateForFunctionPointer<FSetStat1>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 24 ) ) );
			_SetStat2 = Marshal.GetDelegateForFunctionPointer<FSetStat2>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 32 ) ) );
			_GetUserStat1 = Marshal.GetDelegateForFunctionPointer<FGetUserStat1>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 136 ) ) );
			_GetUserStat2 = Marshal.GetDelegateForFunctionPointer<FGetUserStat2>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 144 ) ) );
			_GetGlobalStat1 = Marshal.GetDelegateForFunctionPointer<FGetGlobalStat1>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 312 ) ) );
			_GetGlobalStat2 = Marshal.GetDelegateForFunctionPointer<FGetGlobalStat2>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 320 ) ) );
			_GetGlobalStatHistory1 = Marshal.GetDelegateForFunctionPointer<FGetGlobalStatHistory1>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 328 ) ) );
			_GetGlobalStatHistory2 = Marshal.GetDelegateForFunctionPointer<FGetGlobalStatHistory2>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 336 ) ) );
			#endif
		}
		internal override void Shutdown()
		{
			base.Shutdown();
			
			_RequestCurrentStats = null;
			_GetStat1 = null;
			_GetStat2 = null;
			_SetStat1 = null;
			_SetStat2 = null;
			_UpdateAvgRateStat = null;
			_GetAchievement = null;
			_SetAchievement = null;
			_ClearAchievement = null;
			_GetAchievementAndUnlockTime = null;
			_StoreStats = null;
			_GetAchievementIcon = null;
			_GetAchievementDisplayAttribute = null;
			_IndicateAchievementProgress = null;
			_GetNumAchievements = null;
			_GetAchievementName = null;
			_RequestUserStats = null;
			_GetUserStat1 = null;
			_GetUserStat2 = null;
			_GetUserAchievement = null;
			_GetUserAchievementAndUnlockTime = null;
			_ResetAllStats = null;
			_FindOrCreateLeaderboard = null;
			_FindLeaderboard = null;
			_GetLeaderboardName = null;
			_GetLeaderboardEntryCount = null;
			_GetLeaderboardSortMethod = null;
			_GetLeaderboardDisplayType = null;
			_DownloadLeaderboardEntries = null;
			_DownloadLeaderboardEntriesForUsers = null;
			_GetDownloadedLeaderboardEntry = null;
			_UploadLeaderboardScore = null;
			_AttachLeaderboardUGC = null;
			_GetNumberOfCurrentPlayers = null;
			_RequestGlobalAchievementPercentages = null;
			_GetMostAchievedAchievementInfo = null;
			_GetNextMostAchievedAchievementInfo = null;
			_GetAchievementAchievedPercent = null;
			_RequestGlobalStats = null;
			_GetGlobalStat1 = null;
			_GetGlobalStat2 = null;
			_GetGlobalStatHistory1 = null;
			_GetGlobalStatHistory2 = null;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FRequestCurrentStats( IntPtr self );
		private FRequestCurrentStats _RequestCurrentStats;
		
		#endregion
		internal bool RequestCurrentStats()
		{
			var returnValue = _RequestCurrentStats( Self );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FGetStat1( IntPtr self, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchName, ref int pData );
		private FGetStat1 _GetStat1;
		
		#endregion
		internal bool GetStat1( [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchName, ref int pData )
		{
			var returnValue = _GetStat1( Self, pchName, ref pData );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FGetStat2( IntPtr self, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchName, ref float pData );
		private FGetStat2 _GetStat2;
		
		#endregion
		internal bool GetStat2( [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchName, ref float pData )
		{
			var returnValue = _GetStat2( Self, pchName, ref pData );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FSetStat1( IntPtr self, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchName, int nData );
		private FSetStat1 _SetStat1;
		
		#endregion
		internal bool SetStat1( [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchName, int nData )
		{
			var returnValue = _SetStat1( Self, pchName, nData );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FSetStat2( IntPtr self, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchName, float fData );
		private FSetStat2 _SetStat2;
		
		#endregion
		internal bool SetStat2( [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchName, float fData )
		{
			var returnValue = _SetStat2( Self, pchName, fData );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FUpdateAvgRateStat( IntPtr self, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchName, float flCountThisSession, double dSessionLength );
		private FUpdateAvgRateStat _UpdateAvgRateStat;
		
		#endregion
		internal bool UpdateAvgRateStat( [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchName, float flCountThisSession, double dSessionLength )
		{
			var returnValue = _UpdateAvgRateStat( Self, pchName, flCountThisSession, dSessionLength );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FGetAchievement( IntPtr self, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchName, [MarshalAs( UnmanagedType.U1 )] ref bool pbAchieved );
		private FGetAchievement _GetAchievement;
		
		#endregion
		internal bool GetAchievement( [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchName, [MarshalAs( UnmanagedType.U1 )] ref bool pbAchieved )
		{
			var returnValue = _GetAchievement( Self, pchName, ref pbAchieved );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FSetAchievement( IntPtr self, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchName );
		private FSetAchievement _SetAchievement;
		
		#endregion
		internal bool SetAchievement( [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchName )
		{
			var returnValue = _SetAchievement( Self, pchName );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FClearAchievement( IntPtr self, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchName );
		private FClearAchievement _ClearAchievement;
		
		#endregion
		internal bool ClearAchievement( [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchName )
		{
			var returnValue = _ClearAchievement( Self, pchName );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FGetAchievementAndUnlockTime( IntPtr self, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchName, [MarshalAs( UnmanagedType.U1 )] ref bool pbAchieved, ref uint punUnlockTime );
		private FGetAchievementAndUnlockTime _GetAchievementAndUnlockTime;
		
		#endregion
		internal bool GetAchievementAndUnlockTime( [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchName, [MarshalAs( UnmanagedType.U1 )] ref bool pbAchieved, ref uint punUnlockTime )
		{
			var returnValue = _GetAchievementAndUnlockTime( Self, pchName, ref pbAchieved, ref punUnlockTime );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FStoreStats( IntPtr self );
		private FStoreStats _StoreStats;
		
		#endregion
		internal bool StoreStats()
		{
			var returnValue = _StoreStats( Self );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate int FGetAchievementIcon( IntPtr self, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchName );
		private FGetAchievementIcon _GetAchievementIcon;
		
		#endregion
		internal int GetAchievementIcon( [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchName )
		{
			var returnValue = _GetAchievementIcon( Self, pchName );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate Utf8StringPointer FGetAchievementDisplayAttribute( IntPtr self, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchName, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchKey );
		private FGetAchievementDisplayAttribute _GetAchievementDisplayAttribute;
		
		#endregion
		internal string GetAchievementDisplayAttribute( [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchName, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchKey )
		{
			var returnValue = _GetAchievementDisplayAttribute( Self, pchName, pchKey );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FIndicateAchievementProgress( IntPtr self, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchName, uint nCurProgress, uint nMaxProgress );
		private FIndicateAchievementProgress _IndicateAchievementProgress;
		
		#endregion
		internal bool IndicateAchievementProgress( [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchName, uint nCurProgress, uint nMaxProgress )
		{
			var returnValue = _IndicateAchievementProgress( Self, pchName, nCurProgress, nMaxProgress );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate uint FGetNumAchievements( IntPtr self );
		private FGetNumAchievements _GetNumAchievements;
		
		#endregion
		internal uint GetNumAchievements()
		{
			var returnValue = _GetNumAchievements( Self );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate Utf8StringPointer FGetAchievementName( IntPtr self, uint iAchievement );
		private FGetAchievementName _GetAchievementName;
		
		#endregion
		internal string GetAchievementName( uint iAchievement )
		{
			var returnValue = _GetAchievementName( Self, iAchievement );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate SteamAPICall_t FRequestUserStats( IntPtr self, SteamId steamIDUser );
		private FRequestUserStats _RequestUserStats;
		
		#endregion
		internal async Task<UserStatsReceived_t?> RequestUserStats( SteamId steamIDUser )
		{
			var returnValue = _RequestUserStats( Self, steamIDUser );
			return await UserStatsReceived_t.GetResultAsync( returnValue );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FGetUserStat1( IntPtr self, SteamId steamIDUser, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchName, ref int pData );
		private FGetUserStat1 _GetUserStat1;
		
		#endregion
		internal bool GetUserStat1( SteamId steamIDUser, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchName, ref int pData )
		{
			var returnValue = _GetUserStat1( Self, steamIDUser, pchName, ref pData );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FGetUserStat2( IntPtr self, SteamId steamIDUser, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchName, ref float pData );
		private FGetUserStat2 _GetUserStat2;
		
		#endregion
		internal bool GetUserStat2( SteamId steamIDUser, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchName, ref float pData )
		{
			var returnValue = _GetUserStat2( Self, steamIDUser, pchName, ref pData );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FGetUserAchievement( IntPtr self, SteamId steamIDUser, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchName, [MarshalAs( UnmanagedType.U1 )] ref bool pbAchieved );
		private FGetUserAchievement _GetUserAchievement;
		
		#endregion
		internal bool GetUserAchievement( SteamId steamIDUser, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchName, [MarshalAs( UnmanagedType.U1 )] ref bool pbAchieved )
		{
			var returnValue = _GetUserAchievement( Self, steamIDUser, pchName, ref pbAchieved );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FGetUserAchievementAndUnlockTime( IntPtr self, SteamId steamIDUser, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchName, [MarshalAs( UnmanagedType.U1 )] ref bool pbAchieved, ref uint punUnlockTime );
		private FGetUserAchievementAndUnlockTime _GetUserAchievementAndUnlockTime;
		
		#endregion
		internal bool GetUserAchievementAndUnlockTime( SteamId steamIDUser, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchName, [MarshalAs( UnmanagedType.U1 )] ref bool pbAchieved, ref uint punUnlockTime )
		{
			var returnValue = _GetUserAchievementAndUnlockTime( Self, steamIDUser, pchName, ref pbAchieved, ref punUnlockTime );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FResetAllStats( IntPtr self, [MarshalAs( UnmanagedType.U1 )] bool bAchievementsToo );
		private FResetAllStats _ResetAllStats;
		
		#endregion
		internal bool ResetAllStats( [MarshalAs( UnmanagedType.U1 )] bool bAchievementsToo )
		{
			var returnValue = _ResetAllStats( Self, bAchievementsToo );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate SteamAPICall_t FFindOrCreateLeaderboard( IntPtr self, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchLeaderboardName, LeaderboardSort eLeaderboardSortMethod, LeaderboardDisplay eLeaderboardDisplayType );
		private FFindOrCreateLeaderboard _FindOrCreateLeaderboard;
		
		#endregion
		internal async Task<LeaderboardFindResult_t?> FindOrCreateLeaderboard( [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchLeaderboardName, LeaderboardSort eLeaderboardSortMethod, LeaderboardDisplay eLeaderboardDisplayType )
		{
			var returnValue = _FindOrCreateLeaderboard( Self, pchLeaderboardName, eLeaderboardSortMethod, eLeaderboardDisplayType );
			return await LeaderboardFindResult_t.GetResultAsync( returnValue );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate SteamAPICall_t FFindLeaderboard( IntPtr self, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchLeaderboardName );
		private FFindLeaderboard _FindLeaderboard;
		
		#endregion
		internal async Task<LeaderboardFindResult_t?> FindLeaderboard( [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchLeaderboardName )
		{
			var returnValue = _FindLeaderboard( Self, pchLeaderboardName );
			return await LeaderboardFindResult_t.GetResultAsync( returnValue );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate Utf8StringPointer FGetLeaderboardName( IntPtr self, SteamLeaderboard_t hSteamLeaderboard );
		private FGetLeaderboardName _GetLeaderboardName;
		
		#endregion
		internal string GetLeaderboardName( SteamLeaderboard_t hSteamLeaderboard )
		{
			var returnValue = _GetLeaderboardName( Self, hSteamLeaderboard );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate int FGetLeaderboardEntryCount( IntPtr self, SteamLeaderboard_t hSteamLeaderboard );
		private FGetLeaderboardEntryCount _GetLeaderboardEntryCount;
		
		#endregion
		internal int GetLeaderboardEntryCount( SteamLeaderboard_t hSteamLeaderboard )
		{
			var returnValue = _GetLeaderboardEntryCount( Self, hSteamLeaderboard );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate LeaderboardSort FGetLeaderboardSortMethod( IntPtr self, SteamLeaderboard_t hSteamLeaderboard );
		private FGetLeaderboardSortMethod _GetLeaderboardSortMethod;
		
		#endregion
		internal LeaderboardSort GetLeaderboardSortMethod( SteamLeaderboard_t hSteamLeaderboard )
		{
			var returnValue = _GetLeaderboardSortMethod( Self, hSteamLeaderboard );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate LeaderboardDisplay FGetLeaderboardDisplayType( IntPtr self, SteamLeaderboard_t hSteamLeaderboard );
		private FGetLeaderboardDisplayType _GetLeaderboardDisplayType;
		
		#endregion
		internal LeaderboardDisplay GetLeaderboardDisplayType( SteamLeaderboard_t hSteamLeaderboard )
		{
			var returnValue = _GetLeaderboardDisplayType( Self, hSteamLeaderboard );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate SteamAPICall_t FDownloadLeaderboardEntries( IntPtr self, SteamLeaderboard_t hSteamLeaderboard, LeaderboardDataRequest eLeaderboardDataRequest, int nRangeStart, int nRangeEnd );
		private FDownloadLeaderboardEntries _DownloadLeaderboardEntries;
		
		#endregion
		internal async Task<LeaderboardScoresDownloaded_t?> DownloadLeaderboardEntries( SteamLeaderboard_t hSteamLeaderboard, LeaderboardDataRequest eLeaderboardDataRequest, int nRangeStart, int nRangeEnd )
		{
			var returnValue = _DownloadLeaderboardEntries( Self, hSteamLeaderboard, eLeaderboardDataRequest, nRangeStart, nRangeEnd );
			return await LeaderboardScoresDownloaded_t.GetResultAsync( returnValue );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate SteamAPICall_t FDownloadLeaderboardEntriesForUsers( IntPtr self, SteamLeaderboard_t hSteamLeaderboard, [In,Out] SteamId[]  prgUsers, int cUsers );
		private FDownloadLeaderboardEntriesForUsers _DownloadLeaderboardEntriesForUsers;
		
		#endregion
		internal async Task<LeaderboardScoresDownloaded_t?> DownloadLeaderboardEntriesForUsers( SteamLeaderboard_t hSteamLeaderboard, [In,Out] SteamId[]  prgUsers, int cUsers )
		{
			var returnValue = _DownloadLeaderboardEntriesForUsers( Self, hSteamLeaderboard, prgUsers, cUsers );
			return await LeaderboardScoresDownloaded_t.GetResultAsync( returnValue );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FGetDownloadedLeaderboardEntry( IntPtr self, SteamLeaderboardEntries_t hSteamLeaderboardEntries, int index, ref LeaderboardEntry_t pLeaderboardEntry, [In,Out] int[]  pDetails, int cDetailsMax );
		private FGetDownloadedLeaderboardEntry _GetDownloadedLeaderboardEntry;
		
		#endregion
		internal bool GetDownloadedLeaderboardEntry( SteamLeaderboardEntries_t hSteamLeaderboardEntries, int index, ref LeaderboardEntry_t pLeaderboardEntry, [In,Out] int[]  pDetails, int cDetailsMax )
		{
			var returnValue = _GetDownloadedLeaderboardEntry( Self, hSteamLeaderboardEntries, index, ref pLeaderboardEntry, pDetails, cDetailsMax );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate SteamAPICall_t FUploadLeaderboardScore( IntPtr self, SteamLeaderboard_t hSteamLeaderboard, LeaderboardUploadScoreMethod eLeaderboardUploadScoreMethod, int nScore, [In,Out] int[]  pScoreDetails, int cScoreDetailsCount );
		private FUploadLeaderboardScore _UploadLeaderboardScore;
		
		#endregion
		internal async Task<LeaderboardScoreUploaded_t?> UploadLeaderboardScore( SteamLeaderboard_t hSteamLeaderboard, LeaderboardUploadScoreMethod eLeaderboardUploadScoreMethod, int nScore, [In,Out] int[]  pScoreDetails, int cScoreDetailsCount )
		{
			var returnValue = _UploadLeaderboardScore( Self, hSteamLeaderboard, eLeaderboardUploadScoreMethod, nScore, pScoreDetails, cScoreDetailsCount );
			return await LeaderboardScoreUploaded_t.GetResultAsync( returnValue );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate SteamAPICall_t FAttachLeaderboardUGC( IntPtr self, SteamLeaderboard_t hSteamLeaderboard, UGCHandle_t hUGC );
		private FAttachLeaderboardUGC _AttachLeaderboardUGC;
		
		#endregion
		internal async Task<LeaderboardUGCSet_t?> AttachLeaderboardUGC( SteamLeaderboard_t hSteamLeaderboard, UGCHandle_t hUGC )
		{
			var returnValue = _AttachLeaderboardUGC( Self, hSteamLeaderboard, hUGC );
			return await LeaderboardUGCSet_t.GetResultAsync( returnValue );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate SteamAPICall_t FGetNumberOfCurrentPlayers( IntPtr self );
		private FGetNumberOfCurrentPlayers _GetNumberOfCurrentPlayers;
		
		#endregion
		internal async Task<NumberOfCurrentPlayers_t?> GetNumberOfCurrentPlayers()
		{
			var returnValue = _GetNumberOfCurrentPlayers( Self );
			return await NumberOfCurrentPlayers_t.GetResultAsync( returnValue );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate SteamAPICall_t FRequestGlobalAchievementPercentages( IntPtr self );
		private FRequestGlobalAchievementPercentages _RequestGlobalAchievementPercentages;
		
		#endregion
		internal async Task<GlobalAchievementPercentagesReady_t?> RequestGlobalAchievementPercentages()
		{
			var returnValue = _RequestGlobalAchievementPercentages( Self );
			return await GlobalAchievementPercentagesReady_t.GetResultAsync( returnValue );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate int FGetMostAchievedAchievementInfo( IntPtr self, IntPtr pchName, uint unNameBufLen, ref float pflPercent, [MarshalAs( UnmanagedType.U1 )] ref bool pbAchieved );
		private FGetMostAchievedAchievementInfo _GetMostAchievedAchievementInfo;
		
		#endregion
		internal int GetMostAchievedAchievementInfo( out string pchName, ref float pflPercent, [MarshalAs( UnmanagedType.U1 )] ref bool pbAchieved )
		{
			IntPtr mempchName = Helpers.TakeMemory();
			var returnValue = _GetMostAchievedAchievementInfo( Self, mempchName, (1024 * 32), ref pflPercent, ref pbAchieved );
			pchName = Helpers.MemoryToString( mempchName );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate int FGetNextMostAchievedAchievementInfo( IntPtr self, int iIteratorPrevious, IntPtr pchName, uint unNameBufLen, ref float pflPercent, [MarshalAs( UnmanagedType.U1 )] ref bool pbAchieved );
		private FGetNextMostAchievedAchievementInfo _GetNextMostAchievedAchievementInfo;
		
		#endregion
		internal int GetNextMostAchievedAchievementInfo( int iIteratorPrevious, out string pchName, ref float pflPercent, [MarshalAs( UnmanagedType.U1 )] ref bool pbAchieved )
		{
			IntPtr mempchName = Helpers.TakeMemory();
			var returnValue = _GetNextMostAchievedAchievementInfo( Self, iIteratorPrevious, mempchName, (1024 * 32), ref pflPercent, ref pbAchieved );
			pchName = Helpers.MemoryToString( mempchName );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FGetAchievementAchievedPercent( IntPtr self, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchName, ref float pflPercent );
		private FGetAchievementAchievedPercent _GetAchievementAchievedPercent;
		
		#endregion
		internal bool GetAchievementAchievedPercent( [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchName, ref float pflPercent )
		{
			var returnValue = _GetAchievementAchievedPercent( Self, pchName, ref pflPercent );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate SteamAPICall_t FRequestGlobalStats( IntPtr self, int nHistoryDays );
		private FRequestGlobalStats _RequestGlobalStats;
		
		#endregion
		internal async Task<GlobalStatsReceived_t?> RequestGlobalStats( int nHistoryDays )
		{
			var returnValue = _RequestGlobalStats( Self, nHistoryDays );
			return await GlobalStatsReceived_t.GetResultAsync( returnValue );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FGetGlobalStat1( IntPtr self, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchStatName, ref long pData );
		private FGetGlobalStat1 _GetGlobalStat1;
		
		#endregion
		internal bool GetGlobalStat1( [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchStatName, ref long pData )
		{
			var returnValue = _GetGlobalStat1( Self, pchStatName, ref pData );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FGetGlobalStat2( IntPtr self, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchStatName, ref double pData );
		private FGetGlobalStat2 _GetGlobalStat2;
		
		#endregion
		internal bool GetGlobalStat2( [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchStatName, ref double pData )
		{
			var returnValue = _GetGlobalStat2( Self, pchStatName, ref pData );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate int FGetGlobalStatHistory1( IntPtr self, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchStatName, [In,Out] long[]  pData, uint cubData );
		private FGetGlobalStatHistory1 _GetGlobalStatHistory1;
		
		#endregion
		internal int GetGlobalStatHistory1( [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchStatName, [In,Out] long[]  pData, uint cubData )
		{
			var returnValue = _GetGlobalStatHistory1( Self, pchStatName, pData, cubData );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate int FGetGlobalStatHistory2( IntPtr self, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchStatName, [In,Out] double[]  pData, uint cubData );
		private FGetGlobalStatHistory2 _GetGlobalStatHistory2;
		
		#endregion
		internal int GetGlobalStatHistory2( [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchStatName, [In,Out] double[]  pData, uint cubData )
		{
			var returnValue = _GetGlobalStatHistory2( Self, pchStatName, pData, cubData );
			return returnValue;
		}
		
	}
}
