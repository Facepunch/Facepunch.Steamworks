using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Steamworks.Data;


namespace Steamworks
{
	internal class ISteamMatchmakingServers : SteamInterface
	{
		public override string InterfaceName => "SteamMatchMakingServers002";
		
		public override void InitInternals()
		{
		}
		internal override void Shutdown()
		{
			base.Shutdown();
			
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate HServerListRequest FRequestInternetServerList( IntPtr self, AppId iApp, [In,Out] ref MatchMakingKeyValuePair[]  ppchFilters, uint nFilters, IntPtr pRequestServersResponse );
		private FRequestInternetServerList _RequestInternetServerList;
		
		#endregion
		internal HServerListRequest RequestInternetServerList( AppId iApp, [In,Out] ref MatchMakingKeyValuePair[]  ppchFilters, uint nFilters, IntPtr pRequestServersResponse )
		{
			var returnValue = _RequestInternetServerList( Self, iApp, ref ppchFilters, nFilters, pRequestServersResponse );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate HServerListRequest FRequestLANServerList( IntPtr self, AppId iApp, IntPtr pRequestServersResponse );
		private FRequestLANServerList _RequestLANServerList;
		
		#endregion
		internal HServerListRequest RequestLANServerList( AppId iApp, IntPtr pRequestServersResponse )
		{
			var returnValue = _RequestLANServerList( Self, iApp, pRequestServersResponse );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate HServerListRequest FRequestFriendsServerList( IntPtr self, AppId iApp, [In,Out] ref MatchMakingKeyValuePair[]  ppchFilters, uint nFilters, IntPtr pRequestServersResponse );
		private FRequestFriendsServerList _RequestFriendsServerList;
		
		#endregion
		internal HServerListRequest RequestFriendsServerList( AppId iApp, [In,Out] ref MatchMakingKeyValuePair[]  ppchFilters, uint nFilters, IntPtr pRequestServersResponse )
		{
			var returnValue = _RequestFriendsServerList( Self, iApp, ref ppchFilters, nFilters, pRequestServersResponse );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate HServerListRequest FRequestFavoritesServerList( IntPtr self, AppId iApp, [In,Out] ref MatchMakingKeyValuePair[]  ppchFilters, uint nFilters, IntPtr pRequestServersResponse );
		private FRequestFavoritesServerList _RequestFavoritesServerList;
		
		#endregion
		internal HServerListRequest RequestFavoritesServerList( AppId iApp, [In,Out] ref MatchMakingKeyValuePair[]  ppchFilters, uint nFilters, IntPtr pRequestServersResponse )
		{
			var returnValue = _RequestFavoritesServerList( Self, iApp, ref ppchFilters, nFilters, pRequestServersResponse );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate HServerListRequest FRequestHistoryServerList( IntPtr self, AppId iApp, [In,Out] ref MatchMakingKeyValuePair[]  ppchFilters, uint nFilters, IntPtr pRequestServersResponse );
		private FRequestHistoryServerList _RequestHistoryServerList;
		
		#endregion
		internal HServerListRequest RequestHistoryServerList( AppId iApp, [In,Out] ref MatchMakingKeyValuePair[]  ppchFilters, uint nFilters, IntPtr pRequestServersResponse )
		{
			var returnValue = _RequestHistoryServerList( Self, iApp, ref ppchFilters, nFilters, pRequestServersResponse );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate HServerListRequest FRequestSpectatorServerList( IntPtr self, AppId iApp, [In,Out] ref MatchMakingKeyValuePair[]  ppchFilters, uint nFilters, IntPtr pRequestServersResponse );
		private FRequestSpectatorServerList _RequestSpectatorServerList;
		
		#endregion
		internal HServerListRequest RequestSpectatorServerList( AppId iApp, [In,Out] ref MatchMakingKeyValuePair[]  ppchFilters, uint nFilters, IntPtr pRequestServersResponse )
		{
			var returnValue = _RequestSpectatorServerList( Self, iApp, ref ppchFilters, nFilters, pRequestServersResponse );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate void FReleaseRequest( IntPtr self, HServerListRequest hServerListRequest );
		private FReleaseRequest _ReleaseRequest;
		
		#endregion
		internal void ReleaseRequest( HServerListRequest hServerListRequest )
		{
			_ReleaseRequest( Self, hServerListRequest );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate IntPtr FGetServerDetails( IntPtr self, HServerListRequest hRequest, int iServer );
		private FGetServerDetails _GetServerDetails;
		
		#endregion
		internal gameserveritem_t GetServerDetails( HServerListRequest hRequest, int iServer )
		{
			var returnValue = _GetServerDetails( Self, hRequest, iServer );
			return gameserveritem_t.Fill( returnValue );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate void FCancelQuery( IntPtr self, HServerListRequest hRequest );
		private FCancelQuery _CancelQuery;
		
		#endregion
		internal void CancelQuery( HServerListRequest hRequest )
		{
			_CancelQuery( Self, hRequest );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate void FRefreshQuery( IntPtr self, HServerListRequest hRequest );
		private FRefreshQuery _RefreshQuery;
		
		#endregion
		internal void RefreshQuery( HServerListRequest hRequest )
		{
			_RefreshQuery( Self, hRequest );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FIsRefreshing( IntPtr self, HServerListRequest hRequest );
		private FIsRefreshing _IsRefreshing;
		
		#endregion
		internal bool IsRefreshing( HServerListRequest hRequest )
		{
			var returnValue = _IsRefreshing( Self, hRequest );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate int FGetServerCount( IntPtr self, HServerListRequest hRequest );
		private FGetServerCount _GetServerCount;
		
		#endregion
		internal int GetServerCount( HServerListRequest hRequest )
		{
			var returnValue = _GetServerCount( Self, hRequest );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate void FRefreshServer( IntPtr self, HServerListRequest hRequest, int iServer );
		private FRefreshServer _RefreshServer;
		
		#endregion
		internal void RefreshServer( HServerListRequest hRequest, int iServer )
		{
			_RefreshServer( Self, hRequest, iServer );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate HServerQuery FPingServer( IntPtr self, uint unIP, ushort usPort, IntPtr pRequestServersResponse );
		private FPingServer _PingServer;
		
		#endregion
		internal HServerQuery PingServer( uint unIP, ushort usPort, IntPtr pRequestServersResponse )
		{
			var returnValue = _PingServer( Self, unIP, usPort, pRequestServersResponse );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate HServerQuery FPlayerDetails( IntPtr self, uint unIP, ushort usPort, IntPtr pRequestServersResponse );
		private FPlayerDetails _PlayerDetails;
		
		#endregion
		internal HServerQuery PlayerDetails( uint unIP, ushort usPort, IntPtr pRequestServersResponse )
		{
			var returnValue = _PlayerDetails( Self, unIP, usPort, pRequestServersResponse );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate HServerQuery FServerRules( IntPtr self, uint unIP, ushort usPort, IntPtr pRequestServersResponse );
		private FServerRules _ServerRules;
		
		#endregion
		internal HServerQuery ServerRules( uint unIP, ushort usPort, IntPtr pRequestServersResponse )
		{
			var returnValue = _ServerRules( Self, unIP, usPort, pRequestServersResponse );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate void FCancelServerQuery( IntPtr self, HServerQuery hServerQuery );
		private FCancelServerQuery _CancelServerQuery;
		
		#endregion
		internal void CancelServerQuery( HServerQuery hServerQuery )
		{
			_CancelServerQuery( Self, hServerQuery );
		}
		
	}
}
