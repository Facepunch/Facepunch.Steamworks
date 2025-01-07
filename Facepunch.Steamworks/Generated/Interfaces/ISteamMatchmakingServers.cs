using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Steamworks.Data;


namespace Steamworks
{
	internal unsafe class ISteamMatchmakingServers : SteamInterface
	{
		
		internal ISteamMatchmakingServers( bool IsGameServer )
		{
			SetupInterface( IsGameServer );
		}
		
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_SteamMatchmakingServers_v002", CallingConvention = Platform.CC)]
		internal static extern IntPtr SteamAPI_SteamMatchmakingServers_v002();
		public override IntPtr GetUserInterfacePointer() => SteamAPI_SteamMatchmakingServers_v002();
		
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamMatchmakingServers_RequestInternetServerList", CallingConvention = Platform.CC)]
		private static extern HServerListRequest _RequestInternetServerList( IntPtr self, AppId iApp, [In,Out] ref MatchMakingKeyValuePair[]  ppchFilters, uint nFilters, IntPtr pRequestServersResponse );
		
		#endregion
		internal HServerListRequest RequestInternetServerList( AppId iApp, [In,Out] ref MatchMakingKeyValuePair[]  ppchFilters, uint nFilters, IntPtr pRequestServersResponse )
		{
			var returnValue = _RequestInternetServerList( Self, iApp, ref ppchFilters, nFilters, pRequestServersResponse );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamMatchmakingServers_RequestLANServerList", CallingConvention = Platform.CC)]
		private static extern HServerListRequest _RequestLANServerList( IntPtr self, AppId iApp, IntPtr pRequestServersResponse );
		
		#endregion
		internal HServerListRequest RequestLANServerList( AppId iApp, IntPtr pRequestServersResponse )
		{
			var returnValue = _RequestLANServerList( Self, iApp, pRequestServersResponse );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamMatchmakingServers_RequestFriendsServerList", CallingConvention = Platform.CC)]
		private static extern HServerListRequest _RequestFriendsServerList( IntPtr self, AppId iApp, [In,Out] ref MatchMakingKeyValuePair[]  ppchFilters, uint nFilters, IntPtr pRequestServersResponse );
		
		#endregion
		internal HServerListRequest RequestFriendsServerList( AppId iApp, [In,Out] ref MatchMakingKeyValuePair[]  ppchFilters, uint nFilters, IntPtr pRequestServersResponse )
		{
			var returnValue = _RequestFriendsServerList( Self, iApp, ref ppchFilters, nFilters, pRequestServersResponse );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamMatchmakingServers_RequestFavoritesServerList", CallingConvention = Platform.CC)]
		private static extern HServerListRequest _RequestFavoritesServerList( IntPtr self, AppId iApp, [In,Out] ref MatchMakingKeyValuePair[]  ppchFilters, uint nFilters, IntPtr pRequestServersResponse );
		
		#endregion
		internal HServerListRequest RequestFavoritesServerList( AppId iApp, [In,Out] ref MatchMakingKeyValuePair[]  ppchFilters, uint nFilters, IntPtr pRequestServersResponse )
		{
			var returnValue = _RequestFavoritesServerList( Self, iApp, ref ppchFilters, nFilters, pRequestServersResponse );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamMatchmakingServers_RequestHistoryServerList", CallingConvention = Platform.CC)]
		private static extern HServerListRequest _RequestHistoryServerList( IntPtr self, AppId iApp, [In,Out] ref MatchMakingKeyValuePair[]  ppchFilters, uint nFilters, IntPtr pRequestServersResponse );
		
		#endregion
		internal HServerListRequest RequestHistoryServerList( AppId iApp, [In,Out] ref MatchMakingKeyValuePair[]  ppchFilters, uint nFilters, IntPtr pRequestServersResponse )
		{
			var returnValue = _RequestHistoryServerList( Self, iApp, ref ppchFilters, nFilters, pRequestServersResponse );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamMatchmakingServers_RequestSpectatorServerList", CallingConvention = Platform.CC)]
		private static extern HServerListRequest _RequestSpectatorServerList( IntPtr self, AppId iApp, [In,Out] ref MatchMakingKeyValuePair[]  ppchFilters, uint nFilters, IntPtr pRequestServersResponse );
		
		#endregion
		internal HServerListRequest RequestSpectatorServerList( AppId iApp, [In,Out] ref MatchMakingKeyValuePair[]  ppchFilters, uint nFilters, IntPtr pRequestServersResponse )
		{
			var returnValue = _RequestSpectatorServerList( Self, iApp, ref ppchFilters, nFilters, pRequestServersResponse );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamMatchmakingServers_ReleaseRequest", CallingConvention = Platform.CC)]
		private static extern void _ReleaseRequest( IntPtr self, HServerListRequest hServerListRequest );
		
		#endregion
		internal void ReleaseRequest( HServerListRequest hServerListRequest )
		{
			_ReleaseRequest( Self, hServerListRequest );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamMatchmakingServers_GetServerDetails", CallingConvention = Platform.CC)]
		private static extern IntPtr _GetServerDetails( IntPtr self, HServerListRequest hRequest, int iServer );
		
		#endregion
		internal gameserveritem_t GetServerDetails( HServerListRequest hRequest, int iServer )
		{
			var returnValue = _GetServerDetails( Self, hRequest, iServer );
			return returnValue.ToType<gameserveritem_t>();
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamMatchmakingServers_CancelQuery", CallingConvention = Platform.CC)]
		private static extern void _CancelQuery( IntPtr self, HServerListRequest hRequest );
		
		#endregion
		internal void CancelQuery( HServerListRequest hRequest )
		{
			_CancelQuery( Self, hRequest );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamMatchmakingServers_RefreshQuery", CallingConvention = Platform.CC)]
		private static extern void _RefreshQuery( IntPtr self, HServerListRequest hRequest );
		
		#endregion
		internal void RefreshQuery( HServerListRequest hRequest )
		{
			_RefreshQuery( Self, hRequest );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamMatchmakingServers_IsRefreshing", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _IsRefreshing( IntPtr self, HServerListRequest hRequest );
		
		#endregion
		internal bool IsRefreshing( HServerListRequest hRequest )
		{
			var returnValue = _IsRefreshing( Self, hRequest );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamMatchmakingServers_GetServerCount", CallingConvention = Platform.CC)]
		private static extern int _GetServerCount( IntPtr self, HServerListRequest hRequest );
		
		#endregion
		internal int GetServerCount( HServerListRequest hRequest )
		{
			var returnValue = _GetServerCount( Self, hRequest );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamMatchmakingServers_RefreshServer", CallingConvention = Platform.CC)]
		private static extern void _RefreshServer( IntPtr self, HServerListRequest hRequest, int iServer );
		
		#endregion
		internal void RefreshServer( HServerListRequest hRequest, int iServer )
		{
			_RefreshServer( Self, hRequest, iServer );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamMatchmakingServers_PingServer", CallingConvention = Platform.CC)]
		private static extern HServerQuery _PingServer( IntPtr self, uint unIP, ushort usPort, IntPtr pRequestServersResponse );
		
		#endregion
		internal HServerQuery PingServer( uint unIP, ushort usPort, IntPtr pRequestServersResponse )
		{
			var returnValue = _PingServer( Self, unIP, usPort, pRequestServersResponse );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamMatchmakingServers_PlayerDetails", CallingConvention = Platform.CC)]
		private static extern HServerQuery _PlayerDetails( IntPtr self, uint unIP, ushort usPort, IntPtr pRequestServersResponse );
		
		#endregion
		internal HServerQuery PlayerDetails( uint unIP, ushort usPort, IntPtr pRequestServersResponse )
		{
			var returnValue = _PlayerDetails( Self, unIP, usPort, pRequestServersResponse );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamMatchmakingServers_ServerRules", CallingConvention = Platform.CC)]
		private static extern HServerQuery _ServerRules( IntPtr self, uint unIP, ushort usPort, IntPtr pRequestServersResponse );
		
		#endregion
		internal HServerQuery ServerRules( uint unIP, ushort usPort, IntPtr pRequestServersResponse )
		{
			var returnValue = _ServerRules( Self, unIP, usPort, pRequestServersResponse );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamMatchmakingServers_CancelServerQuery", CallingConvention = Platform.CC)]
		private static extern void _CancelServerQuery( IntPtr self, HServerQuery hServerQuery );
		
		#endregion
		internal void CancelServerQuery( HServerQuery hServerQuery )
		{
			_CancelServerQuery( Self, hServerQuery );
		}
		
	}
}
