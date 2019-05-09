using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Steamworks.Data;


namespace Steamworks
{
	internal class ISteamGameServerStats : SteamInterface
	{
		public override string InterfaceName => "SteamGameServerStats001";
		
		public override void InitInternals()
		{
			_RequestUserStats = Marshal.GetDelegateForFunctionPointer<FRequestUserStats>( Marshal.ReadIntPtr( VTable, 0) );
			_GetUserStat1 = Marshal.GetDelegateForFunctionPointer<FGetUserStat1>( Marshal.ReadIntPtr( VTable, 8) );
			_GetUserStat2 = Marshal.GetDelegateForFunctionPointer<FGetUserStat2>( Marshal.ReadIntPtr( VTable, 16) );
			_GetUserAchievement = Marshal.GetDelegateForFunctionPointer<FGetUserAchievement>( Marshal.ReadIntPtr( VTable, 24) );
			_SetUserStat1 = Marshal.GetDelegateForFunctionPointer<FSetUserStat1>( Marshal.ReadIntPtr( VTable, 32) );
			_SetUserStat2 = Marshal.GetDelegateForFunctionPointer<FSetUserStat2>( Marshal.ReadIntPtr( VTable, 40) );
			_UpdateUserAvgRateStat = Marshal.GetDelegateForFunctionPointer<FUpdateUserAvgRateStat>( Marshal.ReadIntPtr( VTable, 48) );
			_SetUserAchievement = Marshal.GetDelegateForFunctionPointer<FSetUserAchievement>( Marshal.ReadIntPtr( VTable, 56) );
			_ClearUserAchievement = Marshal.GetDelegateForFunctionPointer<FClearUserAchievement>( Marshal.ReadIntPtr( VTable, 64) );
			_StoreUserStats = Marshal.GetDelegateForFunctionPointer<FStoreUserStats>( Marshal.ReadIntPtr( VTable, 72) );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate SteamAPICall_t FRequestUserStats( IntPtr self, SteamId steamIDUser );
		private FRequestUserStats _RequestUserStats;
		
		#endregion
		internal async Task<GSStatsReceived_t?> RequestUserStats( SteamId steamIDUser )
		{
			return await GSStatsReceived_t.GetResultAsync( _RequestUserStats( Self, steamIDUser ) );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FGetUserStat1( IntPtr self, SteamId steamIDUser, string pchName, ref int pData );
		private FGetUserStat1 _GetUserStat1;
		
		#endregion
		internal bool GetUserStat1( SteamId steamIDUser, string pchName, ref int pData )
		{
			return _GetUserStat1( Self, steamIDUser, pchName, ref pData );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FGetUserStat2( IntPtr self, SteamId steamIDUser, string pchName, ref float pData );
		private FGetUserStat2 _GetUserStat2;
		
		#endregion
		internal bool GetUserStat2( SteamId steamIDUser, string pchName, ref float pData )
		{
			return _GetUserStat2( Self, steamIDUser, pchName, ref pData );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FGetUserAchievement( IntPtr self, SteamId steamIDUser, string pchName, [MarshalAs( UnmanagedType.U1 )] ref bool pbAchieved );
		private FGetUserAchievement _GetUserAchievement;
		
		#endregion
		internal bool GetUserAchievement( SteamId steamIDUser, string pchName, [MarshalAs( UnmanagedType.U1 )] ref bool pbAchieved )
		{
			return _GetUserAchievement( Self, steamIDUser, pchName, ref pbAchieved );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FSetUserStat1( IntPtr self, SteamId steamIDUser, string pchName, int nData );
		private FSetUserStat1 _SetUserStat1;
		
		#endregion
		internal bool SetUserStat1( SteamId steamIDUser, string pchName, int nData )
		{
			return _SetUserStat1( Self, steamIDUser, pchName, nData );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FSetUserStat2( IntPtr self, SteamId steamIDUser, string pchName, float fData );
		private FSetUserStat2 _SetUserStat2;
		
		#endregion
		internal bool SetUserStat2( SteamId steamIDUser, string pchName, float fData )
		{
			return _SetUserStat2( Self, steamIDUser, pchName, fData );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FUpdateUserAvgRateStat( IntPtr self, SteamId steamIDUser, string pchName, float flCountThisSession, double dSessionLength );
		private FUpdateUserAvgRateStat _UpdateUserAvgRateStat;
		
		#endregion
		internal bool UpdateUserAvgRateStat( SteamId steamIDUser, string pchName, float flCountThisSession, double dSessionLength )
		{
			return _UpdateUserAvgRateStat( Self, steamIDUser, pchName, flCountThisSession, dSessionLength );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FSetUserAchievement( IntPtr self, SteamId steamIDUser, string pchName );
		private FSetUserAchievement _SetUserAchievement;
		
		#endregion
		internal bool SetUserAchievement( SteamId steamIDUser, string pchName )
		{
			return _SetUserAchievement( Self, steamIDUser, pchName );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FClearUserAchievement( IntPtr self, SteamId steamIDUser, string pchName );
		private FClearUserAchievement _ClearUserAchievement;
		
		#endregion
		internal bool ClearUserAchievement( SteamId steamIDUser, string pchName )
		{
			return _ClearUserAchievement( Self, steamIDUser, pchName );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate SteamAPICall_t FStoreUserStats( IntPtr self, SteamId steamIDUser );
		private FStoreUserStats _StoreUserStats;
		
		#endregion
		internal async Task<GSStatsStored_t?> StoreUserStats( SteamId steamIDUser )
		{
			return await GSStatsStored_t.GetResultAsync( _StoreUserStats( Self, steamIDUser ) );
		}
		
	}
}
