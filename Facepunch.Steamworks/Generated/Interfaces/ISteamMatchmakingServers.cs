using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Steamworks.Data;


namespace Steamworks
{
	internal class ISteamMatchmakingServers : SteamInterface
	{
		public ISteamMatchmakingServers( bool server = false ) : base( server )
		{
		}
		
		public override string InterfaceName => "SteamMatchMakingServers002";
		
		public override void InitInternals()
		{
			_RequestInternetServerList = Marshal.GetDelegateForFunctionPointer<FRequestInternetServerList>( Marshal.ReadIntPtr( VTable, 0) );
			_RequestLANServerList = Marshal.GetDelegateForFunctionPointer<FRequestLANServerList>( Marshal.ReadIntPtr( VTable, 8) );
			_RequestFriendsServerList = Marshal.GetDelegateForFunctionPointer<FRequestFriendsServerList>( Marshal.ReadIntPtr( VTable, 16) );
			_RequestFavoritesServerList = Marshal.GetDelegateForFunctionPointer<FRequestFavoritesServerList>( Marshal.ReadIntPtr( VTable, 24) );
			_RequestHistoryServerList = Marshal.GetDelegateForFunctionPointer<FRequestHistoryServerList>( Marshal.ReadIntPtr( VTable, 32) );
			_RequestSpectatorServerList = Marshal.GetDelegateForFunctionPointer<FRequestSpectatorServerList>( Marshal.ReadIntPtr( VTable, 40) );
			_ReleaseRequest = Marshal.GetDelegateForFunctionPointer<FReleaseRequest>( Marshal.ReadIntPtr( VTable, 48) );
			_GetServerDetails = Marshal.GetDelegateForFunctionPointer<FGetServerDetails>( Marshal.ReadIntPtr( VTable, 56) );
			_CancelQuery = Marshal.GetDelegateForFunctionPointer<FCancelQuery>( Marshal.ReadIntPtr( VTable, 64) );
			_RefreshQuery = Marshal.GetDelegateForFunctionPointer<FRefreshQuery>( Marshal.ReadIntPtr( VTable, 72) );
			_IsRefreshing = Marshal.GetDelegateForFunctionPointer<FIsRefreshing>( Marshal.ReadIntPtr( VTable, 80) );
			_GetServerCount = Marshal.GetDelegateForFunctionPointer<FGetServerCount>( Marshal.ReadIntPtr( VTable, 88) );
			_RefreshServer = Marshal.GetDelegateForFunctionPointer<FRefreshServer>( Marshal.ReadIntPtr( VTable, 96) );
			_PingServer = Marshal.GetDelegateForFunctionPointer<FPingServer>( Marshal.ReadIntPtr( VTable, 104) );
			_PlayerDetails = Marshal.GetDelegateForFunctionPointer<FPlayerDetails>( Marshal.ReadIntPtr( VTable, 112) );
			_ServerRules = Marshal.GetDelegateForFunctionPointer<FServerRules>( Marshal.ReadIntPtr( VTable, 120) );
			_CancelServerQuery = Marshal.GetDelegateForFunctionPointer<FCancelServerQuery>( Marshal.ReadIntPtr( VTable, 128) );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate HServerListRequest FRequestInternetServerList( IntPtr self, AppId iApp, [In,Out] ref MatchMakingKeyValuePair_t[]  ppchFilters, uint nFilters, IntPtr pRequestServersResponse );
		private FRequestInternetServerList _RequestInternetServerList;
		
		#endregion
		internal HServerListRequest RequestInternetServerList( AppId iApp, [In,Out] ref MatchMakingKeyValuePair_t[]  ppchFilters, uint nFilters, IntPtr pRequestServersResponse )
		{
			return _RequestInternetServerList( Self, iApp, ref ppchFilters, nFilters, pRequestServersResponse );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate HServerListRequest FRequestLANServerList( IntPtr self, AppId iApp, IntPtr pRequestServersResponse );
		private FRequestLANServerList _RequestLANServerList;
		
		#endregion
		internal HServerListRequest RequestLANServerList( AppId iApp, IntPtr pRequestServersResponse )
		{
			return _RequestLANServerList( Self, iApp, pRequestServersResponse );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate HServerListRequest FRequestFriendsServerList( IntPtr self, AppId iApp, [In,Out] ref MatchMakingKeyValuePair_t[]  ppchFilters, uint nFilters, IntPtr pRequestServersResponse );
		private FRequestFriendsServerList _RequestFriendsServerList;
		
		#endregion
		internal HServerListRequest RequestFriendsServerList( AppId iApp, [In,Out] ref MatchMakingKeyValuePair_t[]  ppchFilters, uint nFilters, IntPtr pRequestServersResponse )
		{
			return _RequestFriendsServerList( Self, iApp, ref ppchFilters, nFilters, pRequestServersResponse );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate HServerListRequest FRequestFavoritesServerList( IntPtr self, AppId iApp, [In,Out] ref MatchMakingKeyValuePair_t[]  ppchFilters, uint nFilters, IntPtr pRequestServersResponse );
		private FRequestFavoritesServerList _RequestFavoritesServerList;
		
		#endregion
		internal HServerListRequest RequestFavoritesServerList( AppId iApp, [In,Out] ref MatchMakingKeyValuePair_t[]  ppchFilters, uint nFilters, IntPtr pRequestServersResponse )
		{
			return _RequestFavoritesServerList( Self, iApp, ref ppchFilters, nFilters, pRequestServersResponse );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate HServerListRequest FRequestHistoryServerList( IntPtr self, AppId iApp, [In,Out] ref MatchMakingKeyValuePair_t[]  ppchFilters, uint nFilters, IntPtr pRequestServersResponse );
		private FRequestHistoryServerList _RequestHistoryServerList;
		
		#endregion
		internal HServerListRequest RequestHistoryServerList( AppId iApp, [In,Out] ref MatchMakingKeyValuePair_t[]  ppchFilters, uint nFilters, IntPtr pRequestServersResponse )
		{
			return _RequestHistoryServerList( Self, iApp, ref ppchFilters, nFilters, pRequestServersResponse );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate HServerListRequest FRequestSpectatorServerList( IntPtr self, AppId iApp, [In,Out] ref MatchMakingKeyValuePair_t[]  ppchFilters, uint nFilters, IntPtr pRequestServersResponse );
		private FRequestSpectatorServerList _RequestSpectatorServerList;
		
		#endregion
		internal HServerListRequest RequestSpectatorServerList( AppId iApp, [In,Out] ref MatchMakingKeyValuePair_t[]  ppchFilters, uint nFilters, IntPtr pRequestServersResponse )
		{
			return _RequestSpectatorServerList( Self, iApp, ref ppchFilters, nFilters, pRequestServersResponse );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate void FReleaseRequest( IntPtr self, HServerListRequest hServerListRequest );
		private FReleaseRequest _ReleaseRequest;
		
		#endregion
		internal void ReleaseRequest( HServerListRequest hServerListRequest )
		{
			_ReleaseRequest( Self, hServerListRequest );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate IntPtr FGetServerDetails( IntPtr self, HServerListRequest hRequest, int iServer );
		private FGetServerDetails _GetServerDetails;
		
		#endregion
		internal gameserveritem_t GetServerDetails( HServerListRequest hRequest, int iServer )
		{
			return new gameserveritem_t().Fill( _GetServerDetails( Self, hRequest, iServer ) );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate void FCancelQuery( IntPtr self, HServerListRequest hRequest );
		private FCancelQuery _CancelQuery;
		
		#endregion
		internal void CancelQuery( HServerListRequest hRequest )
		{
			_CancelQuery( Self, hRequest );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate void FRefreshQuery( IntPtr self, HServerListRequest hRequest );
		private FRefreshQuery _RefreshQuery;
		
		#endregion
		internal void RefreshQuery( HServerListRequest hRequest )
		{
			_RefreshQuery( Self, hRequest );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FIsRefreshing( IntPtr self, HServerListRequest hRequest );
		private FIsRefreshing _IsRefreshing;
		
		#endregion
		internal bool IsRefreshing( HServerListRequest hRequest )
		{
			return _IsRefreshing( Self, hRequest );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate int FGetServerCount( IntPtr self, HServerListRequest hRequest );
		private FGetServerCount _GetServerCount;
		
		#endregion
		internal int GetServerCount( HServerListRequest hRequest )
		{
			return _GetServerCount( Self, hRequest );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate void FRefreshServer( IntPtr self, HServerListRequest hRequest, int iServer );
		private FRefreshServer _RefreshServer;
		
		#endregion
		internal void RefreshServer( HServerListRequest hRequest, int iServer )
		{
			_RefreshServer( Self, hRequest, iServer );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate HServerQuery FPingServer( IntPtr self, uint unIP, ushort usPort, IntPtr pRequestServersResponse );
		private FPingServer _PingServer;
		
		#endregion
		internal HServerQuery PingServer( uint unIP, ushort usPort, IntPtr pRequestServersResponse )
		{
			return _PingServer( Self, unIP, usPort, pRequestServersResponse );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate HServerQuery FPlayerDetails( IntPtr self, uint unIP, ushort usPort, IntPtr pRequestServersResponse );
		private FPlayerDetails _PlayerDetails;
		
		#endregion
		internal HServerQuery PlayerDetails( uint unIP, ushort usPort, IntPtr pRequestServersResponse )
		{
			return _PlayerDetails( Self, unIP, usPort, pRequestServersResponse );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate HServerQuery FServerRules( IntPtr self, uint unIP, ushort usPort, IntPtr pRequestServersResponse );
		private FServerRules _ServerRules;
		
		#endregion
		internal HServerQuery ServerRules( uint unIP, ushort usPort, IntPtr pRequestServersResponse )
		{
			return _ServerRules( Self, unIP, usPort, pRequestServersResponse );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate void FCancelServerQuery( IntPtr self, HServerQuery hServerQuery );
		private FCancelServerQuery _CancelServerQuery;
		
		#endregion
		internal void CancelServerQuery( HServerQuery hServerQuery )
		{
			_CancelServerQuery( Self, hServerQuery );
		}
		
	}
}
