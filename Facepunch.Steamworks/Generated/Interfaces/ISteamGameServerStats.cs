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
			_RequestUserStats = Marshal.GetDelegateForFunctionPointer<FRequestUserStats>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 0 ) ) );
			_GetUserAchievement = Marshal.GetDelegateForFunctionPointer<FGetUserAchievement>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 24 ) ) );
			_UpdateUserAvgRateStat = Marshal.GetDelegateForFunctionPointer<FUpdateUserAvgRateStat>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 48 ) ) );
			_SetUserAchievement = Marshal.GetDelegateForFunctionPointer<FSetUserAchievement>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 56 ) ) );
			_ClearUserAchievement = Marshal.GetDelegateForFunctionPointer<FClearUserAchievement>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 64 ) ) );
			_StoreUserStats = Marshal.GetDelegateForFunctionPointer<FStoreUserStats>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 72 ) ) );
			
			#if PLATFORM_WIN
			_GetUserStat1 = Marshal.GetDelegateForFunctionPointer<FGetUserStat1>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 16 ) ) );
			_GetUserStat2 = Marshal.GetDelegateForFunctionPointer<FGetUserStat2>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 8 ) ) );
			_SetUserStat1 = Marshal.GetDelegateForFunctionPointer<FSetUserStat1>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 40 ) ) );
			_SetUserStat2 = Marshal.GetDelegateForFunctionPointer<FSetUserStat2>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 32 ) ) );
			#else
			_GetUserStat1 = Marshal.GetDelegateForFunctionPointer<FGetUserStat1>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 8 ) ) );
			_GetUserStat2 = Marshal.GetDelegateForFunctionPointer<FGetUserStat2>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 16 ) ) );
			_SetUserStat1 = Marshal.GetDelegateForFunctionPointer<FSetUserStat1>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 32 ) ) );
			_SetUserStat2 = Marshal.GetDelegateForFunctionPointer<FSetUserStat2>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 40 ) ) );
			#endif
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
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate SteamAPICall_t FRequestUserStats( IntPtr self, SteamId steamIDUser );
		private FRequestUserStats _RequestUserStats;
		
		#endregion
		internal async Task<GSStatsReceived_t?> RequestUserStats( SteamId steamIDUser )
		{
			var returnValue = _RequestUserStats( Self, steamIDUser );
			return await GSStatsReceived_t.GetResultAsync( returnValue );
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
		private delegate bool FSetUserStat1( IntPtr self, SteamId steamIDUser, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchName, int nData );
		private FSetUserStat1 _SetUserStat1;
		
		#endregion
		internal bool SetUserStat1( SteamId steamIDUser, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchName, int nData )
		{
			var returnValue = _SetUserStat1( Self, steamIDUser, pchName, nData );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FSetUserStat2( IntPtr self, SteamId steamIDUser, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchName, float fData );
		private FSetUserStat2 _SetUserStat2;
		
		#endregion
		internal bool SetUserStat2( SteamId steamIDUser, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchName, float fData )
		{
			var returnValue = _SetUserStat2( Self, steamIDUser, pchName, fData );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FUpdateUserAvgRateStat( IntPtr self, SteamId steamIDUser, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchName, float flCountThisSession, double dSessionLength );
		private FUpdateUserAvgRateStat _UpdateUserAvgRateStat;
		
		#endregion
		internal bool UpdateUserAvgRateStat( SteamId steamIDUser, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchName, float flCountThisSession, double dSessionLength )
		{
			var returnValue = _UpdateUserAvgRateStat( Self, steamIDUser, pchName, flCountThisSession, dSessionLength );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FSetUserAchievement( IntPtr self, SteamId steamIDUser, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchName );
		private FSetUserAchievement _SetUserAchievement;
		
		#endregion
		internal bool SetUserAchievement( SteamId steamIDUser, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchName )
		{
			var returnValue = _SetUserAchievement( Self, steamIDUser, pchName );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FClearUserAchievement( IntPtr self, SteamId steamIDUser, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchName );
		private FClearUserAchievement _ClearUserAchievement;
		
		#endregion
		internal bool ClearUserAchievement( SteamId steamIDUser, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchName )
		{
			var returnValue = _ClearUserAchievement( Self, steamIDUser, pchName );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate SteamAPICall_t FStoreUserStats( IntPtr self, SteamId steamIDUser );
		private FStoreUserStats _StoreUserStats;
		
		#endregion
		internal async Task<GSStatsStored_t?> StoreUserStats( SteamId steamIDUser )
		{
			var returnValue = _StoreUserStats( Self, steamIDUser );
			return await GSStatsStored_t.GetResultAsync( returnValue );
		}
		
	}
}
