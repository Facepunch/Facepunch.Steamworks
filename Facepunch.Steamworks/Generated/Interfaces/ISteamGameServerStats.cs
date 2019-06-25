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
			#if PLATFORM_WIN64
			int[] loc = new[] { 0, 16, 8, 24, 40, 32, 48, 56, 64, 72 };
			#else
			int[] loc = new[] { 0, 8, 16, 24, 32, 40, 48, 56, 64, 72 };
			#endif
			
			_RequestUserStats = Marshal.GetDelegateForFunctionPointer<FRequestUserStats>( Marshal.ReadIntPtr( VTable, loc[0]) );
			_GetUserStat1 = Marshal.GetDelegateForFunctionPointer<FGetUserStat1>( Marshal.ReadIntPtr( VTable, loc[1]) );
			_GetUserStat2 = Marshal.GetDelegateForFunctionPointer<FGetUserStat2>( Marshal.ReadIntPtr( VTable, loc[2]) );
			_GetUserAchievement = Marshal.GetDelegateForFunctionPointer<FGetUserAchievement>( Marshal.ReadIntPtr( VTable, loc[3]) );
			_SetUserStat1 = Marshal.GetDelegateForFunctionPointer<FSetUserStat1>( Marshal.ReadIntPtr( VTable, loc[4]) );
			_SetUserStat2 = Marshal.GetDelegateForFunctionPointer<FSetUserStat2>( Marshal.ReadIntPtr( VTable, loc[5]) );
			_UpdateUserAvgRateStat = Marshal.GetDelegateForFunctionPointer<FUpdateUserAvgRateStat>( Marshal.ReadIntPtr( VTable, loc[6]) );
			_SetUserAchievement = Marshal.GetDelegateForFunctionPointer<FSetUserAchievement>( Marshal.ReadIntPtr( VTable, loc[7]) );
			_ClearUserAchievement = Marshal.GetDelegateForFunctionPointer<FClearUserAchievement>( Marshal.ReadIntPtr( VTable, loc[8]) );
			_StoreUserStats = Marshal.GetDelegateForFunctionPointer<FStoreUserStats>( Marshal.ReadIntPtr( VTable, loc[9]) );
		}
		internal override void Shutdown()
		{
			base.Shutdown();
			
			_RequestUserStats = null;
			_GetUserStat1 = null;
			_GetUserStat2 = null;
			_GetUserAchievement = null;
			_SetUserStat1 = null;
			_SetUserStat2 = null;
			_UpdateUserAvgRateStat = null;
			_SetUserAchievement = null;
			_ClearUserAchievement = null;
			_StoreUserStats = null;
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
		private delegate bool FGetUserStat1( IntPtr self, SteamId steamIDUser, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchName, ref int pData );
		private FGetUserStat1 _GetUserStat1;
		
		#endregion
		internal bool GetUserStat1( SteamId steamIDUser, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchName, ref int pData )
		{
			return _GetUserStat1( Self, steamIDUser, pchName, ref pData );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FGetUserStat2( IntPtr self, SteamId steamIDUser, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchName, ref float pData );
		private FGetUserStat2 _GetUserStat2;
		
		#endregion
		internal bool GetUserStat2( SteamId steamIDUser, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchName, ref float pData )
		{
			return _GetUserStat2( Self, steamIDUser, pchName, ref pData );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FGetUserAchievement( IntPtr self, SteamId steamIDUser, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchName, [MarshalAs( UnmanagedType.U1 )] ref bool pbAchieved );
		private FGetUserAchievement _GetUserAchievement;
		
		#endregion
		internal bool GetUserAchievement( SteamId steamIDUser, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchName, [MarshalAs( UnmanagedType.U1 )] ref bool pbAchieved )
		{
			return _GetUserAchievement( Self, steamIDUser, pchName, ref pbAchieved );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FSetUserStat1( IntPtr self, SteamId steamIDUser, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchName, int nData );
		private FSetUserStat1 _SetUserStat1;
		
		#endregion
		internal bool SetUserStat1( SteamId steamIDUser, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchName, int nData )
		{
			return _SetUserStat1( Self, steamIDUser, pchName, nData );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FSetUserStat2( IntPtr self, SteamId steamIDUser, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchName, float fData );
		private FSetUserStat2 _SetUserStat2;
		
		#endregion
		internal bool SetUserStat2( SteamId steamIDUser, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchName, float fData )
		{
			return _SetUserStat2( Self, steamIDUser, pchName, fData );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FUpdateUserAvgRateStat( IntPtr self, SteamId steamIDUser, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchName, float flCountThisSession, double dSessionLength );
		private FUpdateUserAvgRateStat _UpdateUserAvgRateStat;
		
		#endregion
		internal bool UpdateUserAvgRateStat( SteamId steamIDUser, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchName, float flCountThisSession, double dSessionLength )
		{
			return _UpdateUserAvgRateStat( Self, steamIDUser, pchName, flCountThisSession, dSessionLength );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FSetUserAchievement( IntPtr self, SteamId steamIDUser, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchName );
		private FSetUserAchievement _SetUserAchievement;
		
		#endregion
		internal bool SetUserAchievement( SteamId steamIDUser, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchName )
		{
			return _SetUserAchievement( Self, steamIDUser, pchName );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FClearUserAchievement( IntPtr self, SteamId steamIDUser, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchName );
		private FClearUserAchievement _ClearUserAchievement;
		
		#endregion
		internal bool ClearUserAchievement( SteamId steamIDUser, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchName )
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
