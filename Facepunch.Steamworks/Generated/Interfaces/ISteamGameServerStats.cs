using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Steamworks.Data;


namespace Steamworks
{
	internal unsafe partial class ISteamGameServerStats : SteamInterface
	{
		public const string Version = "SteamGameServerStats001";
		
		internal ISteamGameServerStats( bool IsGameServer )
		{
			SetupInterface( IsGameServer );
		}
		
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_SteamGameServerStats_v001", CallingConvention = Platform.CC)]
		internal static extern IntPtr SteamAPI_SteamGameServerStats_v001();
		public override IntPtr GetServerInterfacePointer() => SteamAPI_SteamGameServerStats_v001();
		
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamGameServerStats_RequestUserStats", CallingConvention = Platform.CC)]
		private static extern SteamAPICall_t _RequestUserStats( IntPtr self, SteamId steamIDUser );
		
		#endregion
		internal CallResult<GSStatsReceived_t> RequestUserStats( SteamId steamIDUser )
		{
			var returnValue = _RequestUserStats( Self, steamIDUser );
			return new CallResult<GSStatsReceived_t>( returnValue, IsServer );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamGameServerStats_GetUserStatInt32", CallingConvention = Platform.CC)]
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
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamGameServerStats_GetUserStatFloat", CallingConvention = Platform.CC)]
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
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamGameServerStats_GetUserAchievement", CallingConvention = Platform.CC)]
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
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamGameServerStats_SetUserStatInt32", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _SetUserStat( IntPtr self, SteamId steamIDUser, IntPtr pchName, int nData );
		
		#endregion
		internal bool SetUserStat( SteamId steamIDUser, string pchName, int nData )
		{
			using var str__pchName = new Utf8StringToNative( pchName );
			var returnValue = _SetUserStat( Self, steamIDUser, str__pchName.Pointer, nData );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamGameServerStats_SetUserStatFloat", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _SetUserStat( IntPtr self, SteamId steamIDUser, IntPtr pchName, float fData );
		
		#endregion
		internal bool SetUserStat( SteamId steamIDUser, string pchName, float fData )
		{
			using var str__pchName = new Utf8StringToNative( pchName );
			var returnValue = _SetUserStat( Self, steamIDUser, str__pchName.Pointer, fData );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamGameServerStats_UpdateUserAvgRateStat", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _UpdateUserAvgRateStat( IntPtr self, SteamId steamIDUser, IntPtr pchName, float flCountThisSession, double dSessionLength );
		
		#endregion
		internal bool UpdateUserAvgRateStat( SteamId steamIDUser, string pchName, float flCountThisSession, double dSessionLength )
		{
			using var str__pchName = new Utf8StringToNative( pchName );
			var returnValue = _UpdateUserAvgRateStat( Self, steamIDUser, str__pchName.Pointer, flCountThisSession, dSessionLength );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamGameServerStats_SetUserAchievement", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _SetUserAchievement( IntPtr self, SteamId steamIDUser, IntPtr pchName );
		
		#endregion
		internal bool SetUserAchievement( SteamId steamIDUser, string pchName )
		{
			using var str__pchName = new Utf8StringToNative( pchName );
			var returnValue = _SetUserAchievement( Self, steamIDUser, str__pchName.Pointer );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamGameServerStats_ClearUserAchievement", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _ClearUserAchievement( IntPtr self, SteamId steamIDUser, IntPtr pchName );
		
		#endregion
		internal bool ClearUserAchievement( SteamId steamIDUser, string pchName )
		{
			using var str__pchName = new Utf8StringToNative( pchName );
			var returnValue = _ClearUserAchievement( Self, steamIDUser, str__pchName.Pointer );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamGameServerStats_StoreUserStats", CallingConvention = Platform.CC)]
		private static extern SteamAPICall_t _StoreUserStats( IntPtr self, SteamId steamIDUser );
		
		#endregion
		internal CallResult<GSStatsStored_t> StoreUserStats( SteamId steamIDUser )
		{
			var returnValue = _StoreUserStats( Self, steamIDUser );
			return new CallResult<GSStatsStored_t>( returnValue, IsServer );
		}
		
	}
}
