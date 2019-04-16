using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Steamworks.Data;


namespace Steamworks
{
	internal class ISteamMatchmakingServers : BaseSteamInterface
	{
		public ISteamMatchmakingServers( bool server = false ) : base( server )
		{
		}
		
		public override string InterfaceName => "SteamMatchMakingServers002";
		
		public override void InitInternals()
		{
			RequestInternetServerListDelegatePointer = Marshal.GetDelegateForFunctionPointer<RequestInternetServerListDelegate>( Marshal.ReadIntPtr( VTable, 0) );
			RequestLANServerListDelegatePointer = Marshal.GetDelegateForFunctionPointer<RequestLANServerListDelegate>( Marshal.ReadIntPtr( VTable, 8) );
			RequestFriendsServerListDelegatePointer = Marshal.GetDelegateForFunctionPointer<RequestFriendsServerListDelegate>( Marshal.ReadIntPtr( VTable, 16) );
			RequestFavoritesServerListDelegatePointer = Marshal.GetDelegateForFunctionPointer<RequestFavoritesServerListDelegate>( Marshal.ReadIntPtr( VTable, 24) );
			RequestHistoryServerListDelegatePointer = Marshal.GetDelegateForFunctionPointer<RequestHistoryServerListDelegate>( Marshal.ReadIntPtr( VTable, 32) );
			RequestSpectatorServerListDelegatePointer = Marshal.GetDelegateForFunctionPointer<RequestSpectatorServerListDelegate>( Marshal.ReadIntPtr( VTable, 40) );
			ReleaseRequestDelegatePointer = Marshal.GetDelegateForFunctionPointer<ReleaseRequestDelegate>( Marshal.ReadIntPtr( VTable, 48) );
			GetServerDetailsDelegatePointer = Marshal.GetDelegateForFunctionPointer<GetServerDetailsDelegate>( Marshal.ReadIntPtr( VTable, 56) );
			CancelQueryDelegatePointer = Marshal.GetDelegateForFunctionPointer<CancelQueryDelegate>( Marshal.ReadIntPtr( VTable, 64) );
			RefreshQueryDelegatePointer = Marshal.GetDelegateForFunctionPointer<RefreshQueryDelegate>( Marshal.ReadIntPtr( VTable, 72) );
			IsRefreshingDelegatePointer = Marshal.GetDelegateForFunctionPointer<IsRefreshingDelegate>( Marshal.ReadIntPtr( VTable, 80) );
			GetServerCountDelegatePointer = Marshal.GetDelegateForFunctionPointer<GetServerCountDelegate>( Marshal.ReadIntPtr( VTable, 88) );
			RefreshServerDelegatePointer = Marshal.GetDelegateForFunctionPointer<RefreshServerDelegate>( Marshal.ReadIntPtr( VTable, 96) );
			PingServerDelegatePointer = Marshal.GetDelegateForFunctionPointer<PingServerDelegate>( Marshal.ReadIntPtr( VTable, 104) );
			PlayerDetailsDelegatePointer = Marshal.GetDelegateForFunctionPointer<PlayerDetailsDelegate>( Marshal.ReadIntPtr( VTable, 112) );
			ServerRulesDelegatePointer = Marshal.GetDelegateForFunctionPointer<ServerRulesDelegate>( Marshal.ReadIntPtr( VTable, 120) );
			CancelServerQueryDelegatePointer = Marshal.GetDelegateForFunctionPointer<CancelServerQueryDelegate>( Marshal.ReadIntPtr( VTable, 128) );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate HServerListRequest RequestInternetServerListDelegate( IntPtr self, AppId_t iApp, [In,Out] ref MatchMakingKeyValuePair_t[]  ppchFilters, uint nFilters, IntPtr pRequestServersResponse );
		private RequestInternetServerListDelegate RequestInternetServerListDelegatePointer;
		
		#endregion
		internal HServerListRequest RequestInternetServerList( AppId_t iApp, [In,Out] ref MatchMakingKeyValuePair_t[]  ppchFilters, uint nFilters, IntPtr pRequestServersResponse )
		{
			return RequestInternetServerListDelegatePointer( Self, iApp, ref ppchFilters, nFilters, pRequestServersResponse );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate HServerListRequest RequestLANServerListDelegate( IntPtr self, AppId_t iApp, IntPtr pRequestServersResponse );
		private RequestLANServerListDelegate RequestLANServerListDelegatePointer;
		
		#endregion
		internal HServerListRequest RequestLANServerList( AppId_t iApp, IntPtr pRequestServersResponse )
		{
			return RequestLANServerListDelegatePointer( Self, iApp, pRequestServersResponse );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate HServerListRequest RequestFriendsServerListDelegate( IntPtr self, AppId_t iApp, [In,Out] ref MatchMakingKeyValuePair_t[]  ppchFilters, uint nFilters, IntPtr pRequestServersResponse );
		private RequestFriendsServerListDelegate RequestFriendsServerListDelegatePointer;
		
		#endregion
		internal HServerListRequest RequestFriendsServerList( AppId_t iApp, [In,Out] ref MatchMakingKeyValuePair_t[]  ppchFilters, uint nFilters, IntPtr pRequestServersResponse )
		{
			return RequestFriendsServerListDelegatePointer( Self, iApp, ref ppchFilters, nFilters, pRequestServersResponse );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate HServerListRequest RequestFavoritesServerListDelegate( IntPtr self, AppId_t iApp, [In,Out] ref MatchMakingKeyValuePair_t[]  ppchFilters, uint nFilters, IntPtr pRequestServersResponse );
		private RequestFavoritesServerListDelegate RequestFavoritesServerListDelegatePointer;
		
		#endregion
		internal HServerListRequest RequestFavoritesServerList( AppId_t iApp, [In,Out] ref MatchMakingKeyValuePair_t[]  ppchFilters, uint nFilters, IntPtr pRequestServersResponse )
		{
			return RequestFavoritesServerListDelegatePointer( Self, iApp, ref ppchFilters, nFilters, pRequestServersResponse );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate HServerListRequest RequestHistoryServerListDelegate( IntPtr self, AppId_t iApp, [In,Out] ref MatchMakingKeyValuePair_t[]  ppchFilters, uint nFilters, IntPtr pRequestServersResponse );
		private RequestHistoryServerListDelegate RequestHistoryServerListDelegatePointer;
		
		#endregion
		internal HServerListRequest RequestHistoryServerList( AppId_t iApp, [In,Out] ref MatchMakingKeyValuePair_t[]  ppchFilters, uint nFilters, IntPtr pRequestServersResponse )
		{
			return RequestHistoryServerListDelegatePointer( Self, iApp, ref ppchFilters, nFilters, pRequestServersResponse );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate HServerListRequest RequestSpectatorServerListDelegate( IntPtr self, AppId_t iApp, [In,Out] ref MatchMakingKeyValuePair_t[]  ppchFilters, uint nFilters, IntPtr pRequestServersResponse );
		private RequestSpectatorServerListDelegate RequestSpectatorServerListDelegatePointer;
		
		#endregion
		internal HServerListRequest RequestSpectatorServerList( AppId_t iApp, [In,Out] ref MatchMakingKeyValuePair_t[]  ppchFilters, uint nFilters, IntPtr pRequestServersResponse )
		{
			return RequestSpectatorServerListDelegatePointer( Self, iApp, ref ppchFilters, nFilters, pRequestServersResponse );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate void ReleaseRequestDelegate( IntPtr self, HServerListRequest hServerListRequest );
		private ReleaseRequestDelegate ReleaseRequestDelegatePointer;
		
		#endregion
		internal void ReleaseRequest( HServerListRequest hServerListRequest )
		{
			ReleaseRequestDelegatePointer( Self, hServerListRequest );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate IntPtr GetServerDetailsDelegate( IntPtr self, HServerListRequest hRequest, int iServer );
		private GetServerDetailsDelegate GetServerDetailsDelegatePointer;
		
		#endregion
		internal gameserveritem_t GetServerDetails( HServerListRequest hRequest, int iServer )
		{
			return new gameserveritem_t().Fill( GetServerDetailsDelegatePointer( Self, hRequest, iServer ) );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate void CancelQueryDelegate( IntPtr self, HServerListRequest hRequest );
		private CancelQueryDelegate CancelQueryDelegatePointer;
		
		#endregion
		internal void CancelQuery( HServerListRequest hRequest )
		{
			CancelQueryDelegatePointer( Self, hRequest );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate void RefreshQueryDelegate( IntPtr self, HServerListRequest hRequest );
		private RefreshQueryDelegate RefreshQueryDelegatePointer;
		
		#endregion
		internal void RefreshQuery( HServerListRequest hRequest )
		{
			RefreshQueryDelegatePointer( Self, hRequest );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool IsRefreshingDelegate( IntPtr self, HServerListRequest hRequest );
		private IsRefreshingDelegate IsRefreshingDelegatePointer;
		
		#endregion
		internal bool IsRefreshing( HServerListRequest hRequest )
		{
			return IsRefreshingDelegatePointer( Self, hRequest );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate int GetServerCountDelegate( IntPtr self, HServerListRequest hRequest );
		private GetServerCountDelegate GetServerCountDelegatePointer;
		
		#endregion
		internal int GetServerCount( HServerListRequest hRequest )
		{
			return GetServerCountDelegatePointer( Self, hRequest );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate void RefreshServerDelegate( IntPtr self, HServerListRequest hRequest, int iServer );
		private RefreshServerDelegate RefreshServerDelegatePointer;
		
		#endregion
		internal void RefreshServer( HServerListRequest hRequest, int iServer )
		{
			RefreshServerDelegatePointer( Self, hRequest, iServer );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate HServerQuery PingServerDelegate( IntPtr self, uint unIP, ushort usPort, IntPtr pRequestServersResponse );
		private PingServerDelegate PingServerDelegatePointer;
		
		#endregion
		internal HServerQuery PingServer( uint unIP, ushort usPort, IntPtr pRequestServersResponse )
		{
			return PingServerDelegatePointer( Self, unIP, usPort, pRequestServersResponse );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate HServerQuery PlayerDetailsDelegate( IntPtr self, uint unIP, ushort usPort, IntPtr pRequestServersResponse );
		private PlayerDetailsDelegate PlayerDetailsDelegatePointer;
		
		#endregion
		internal HServerQuery PlayerDetails( uint unIP, ushort usPort, IntPtr pRequestServersResponse )
		{
			return PlayerDetailsDelegatePointer( Self, unIP, usPort, pRequestServersResponse );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate HServerQuery ServerRulesDelegate( IntPtr self, uint unIP, ushort usPort, IntPtr pRequestServersResponse );
		private ServerRulesDelegate ServerRulesDelegatePointer;
		
		#endregion
		internal HServerQuery ServerRules( uint unIP, ushort usPort, IntPtr pRequestServersResponse )
		{
			return ServerRulesDelegatePointer( Self, unIP, usPort, pRequestServersResponse );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate void CancelServerQueryDelegate( IntPtr self, HServerQuery hServerQuery );
		private CancelServerQueryDelegate CancelServerQueryDelegatePointer;
		
		#endregion
		internal void CancelServerQuery( HServerQuery hServerQuery )
		{
			CancelServerQueryDelegatePointer( Self, hServerQuery );
		}
		
	}
}
