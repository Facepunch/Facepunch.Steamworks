using System;
using System.Runtime.InteropServices;

namespace SteamNative
{
	public unsafe class SteamMatchmakingServers
	{
		internal IntPtr _ptr;
		
		public SteamMatchmakingServers( IntPtr pointer )
		{
			_ptr = pointer;
		}
		
		
		// void
		public void CancelQuery( HServerListRequest hRequest /*HServerListRequest*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) Platform.Win32.ISteamMatchmakingServers.CancelQuery( _ptr, hRequest );
			else Platform.Win64.ISteamMatchmakingServers.CancelQuery( _ptr, hRequest );
		}
		
		// void
		public void CancelServerQuery( HServerQuery hServerQuery /*HServerQuery*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) Platform.Win32.ISteamMatchmakingServers.CancelServerQuery( _ptr, hServerQuery );
			else Platform.Win64.ISteamMatchmakingServers.CancelServerQuery( _ptr, hServerQuery );
		}
		
		// int
		public int GetServerCount( HServerListRequest hRequest /*HServerListRequest*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamMatchmakingServers.GetServerCount( _ptr, hRequest );
			else return Platform.Win64.ISteamMatchmakingServers.GetServerCount( _ptr, hRequest );
		}
		
		// gameserveritem_t *
		// with: Detect_ReturningStruct
		public gameserveritem_t GetServerDetails( HServerListRequest hRequest /*HServerListRequest*/, int iServer /*int*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			IntPtr struct_pointer;
			if ( Platform.IsWindows32 ) struct_pointer = Platform.Win32.ISteamMatchmakingServers.GetServerDetails( _ptr, hRequest, iServer );
			else struct_pointer = Platform.Win64.ISteamMatchmakingServers.GetServerDetails( _ptr, hRequest, iServer );
			if ( struct_pointer == IntPtr.Zero ) return default(gameserveritem_t);
			return gameserveritem_t.FromPointer( struct_pointer );
		}
		
		// bool
		public bool IsRefreshing( HServerListRequest hRequest /*HServerListRequest*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamMatchmakingServers.IsRefreshing( _ptr, hRequest );
			else return Platform.Win64.ISteamMatchmakingServers.IsRefreshing( _ptr, hRequest );
		}
		
		// HServerQuery
		public HServerQuery PingServer( uint unIP /*uint32*/, ushort usPort /*uint16*/, IntPtr pRequestServersResponse /*class ISteamMatchmakingPingResponse **/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamMatchmakingServers.PingServer( _ptr, unIP, usPort, (IntPtr) pRequestServersResponse );
			else return Platform.Win64.ISteamMatchmakingServers.PingServer( _ptr, unIP, usPort, (IntPtr) pRequestServersResponse );
		}
		
		// HServerQuery
		public HServerQuery PlayerDetails( uint unIP /*uint32*/, ushort usPort /*uint16*/, IntPtr pRequestServersResponse /*class ISteamMatchmakingPlayersResponse **/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamMatchmakingServers.PlayerDetails( _ptr, unIP, usPort, (IntPtr) pRequestServersResponse );
			else return Platform.Win64.ISteamMatchmakingServers.PlayerDetails( _ptr, unIP, usPort, (IntPtr) pRequestServersResponse );
		}
		
		// void
		public void RefreshQuery( HServerListRequest hRequest /*HServerListRequest*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) Platform.Win32.ISteamMatchmakingServers.RefreshQuery( _ptr, hRequest );
			else Platform.Win64.ISteamMatchmakingServers.RefreshQuery( _ptr, hRequest );
		}
		
		// void
		public void RefreshServer( HServerListRequest hRequest /*HServerListRequest*/, int iServer /*int*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) Platform.Win32.ISteamMatchmakingServers.RefreshServer( _ptr, hRequest, iServer );
			else Platform.Win64.ISteamMatchmakingServers.RefreshServer( _ptr, hRequest, iServer );
		}
		
		// void
		public void ReleaseRequest( HServerListRequest hServerListRequest /*HServerListRequest*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) Platform.Win32.ISteamMatchmakingServers.ReleaseRequest( _ptr, hServerListRequest );
			else Platform.Win64.ISteamMatchmakingServers.ReleaseRequest( _ptr, hServerListRequest );
		}
		
		// HServerListRequest
		// with: Detect_MatchmakingFilters
		public HServerListRequest RequestFavoritesServerList( AppId_t iApp /*AppId_t*/, IntPtr ppchFilters /*struct MatchMakingKeyValuePair_t ***/, uint nFilters /*uint32*/, IntPtr pRequestServersResponse /*class ISteamMatchmakingServerListResponse **/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamMatchmakingServers.RequestFavoritesServerList( _ptr, iApp, (IntPtr) ppchFilters, nFilters, (IntPtr) pRequestServersResponse );
			else return Platform.Win64.ISteamMatchmakingServers.RequestFavoritesServerList( _ptr, iApp, (IntPtr) ppchFilters, nFilters, (IntPtr) pRequestServersResponse );
		}
		
		// HServerListRequest
		// with: Detect_MatchmakingFilters
		public HServerListRequest RequestFriendsServerList( AppId_t iApp /*AppId_t*/, IntPtr ppchFilters /*struct MatchMakingKeyValuePair_t ***/, uint nFilters /*uint32*/, IntPtr pRequestServersResponse /*class ISteamMatchmakingServerListResponse **/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamMatchmakingServers.RequestFriendsServerList( _ptr, iApp, (IntPtr) ppchFilters, nFilters, (IntPtr) pRequestServersResponse );
			else return Platform.Win64.ISteamMatchmakingServers.RequestFriendsServerList( _ptr, iApp, (IntPtr) ppchFilters, nFilters, (IntPtr) pRequestServersResponse );
		}
		
		// HServerListRequest
		// with: Detect_MatchmakingFilters
		public HServerListRequest RequestHistoryServerList( AppId_t iApp /*AppId_t*/, IntPtr ppchFilters /*struct MatchMakingKeyValuePair_t ***/, uint nFilters /*uint32*/, IntPtr pRequestServersResponse /*class ISteamMatchmakingServerListResponse **/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamMatchmakingServers.RequestHistoryServerList( _ptr, iApp, (IntPtr) ppchFilters, nFilters, (IntPtr) pRequestServersResponse );
			else return Platform.Win64.ISteamMatchmakingServers.RequestHistoryServerList( _ptr, iApp, (IntPtr) ppchFilters, nFilters, (IntPtr) pRequestServersResponse );
		}
		
		// HServerListRequest
		// with: Detect_MatchmakingFilters
		public HServerListRequest RequestInternetServerList( AppId_t iApp /*AppId_t*/, IntPtr ppchFilters /*struct MatchMakingKeyValuePair_t ***/, uint nFilters /*uint32*/, IntPtr pRequestServersResponse /*class ISteamMatchmakingServerListResponse **/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamMatchmakingServers.RequestInternetServerList( _ptr, iApp, (IntPtr) ppchFilters, nFilters, (IntPtr) pRequestServersResponse );
			else return Platform.Win64.ISteamMatchmakingServers.RequestInternetServerList( _ptr, iApp, (IntPtr) ppchFilters, nFilters, (IntPtr) pRequestServersResponse );
		}
		
		// HServerListRequest
		public HServerListRequest RequestLANServerList( AppId_t iApp /*AppId_t*/, IntPtr pRequestServersResponse /*class ISteamMatchmakingServerListResponse **/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamMatchmakingServers.RequestLANServerList( _ptr, iApp, (IntPtr) pRequestServersResponse );
			else return Platform.Win64.ISteamMatchmakingServers.RequestLANServerList( _ptr, iApp, (IntPtr) pRequestServersResponse );
		}
		
		// HServerListRequest
		// with: Detect_MatchmakingFilters
		public HServerListRequest RequestSpectatorServerList( AppId_t iApp /*AppId_t*/, IntPtr ppchFilters /*struct MatchMakingKeyValuePair_t ***/, uint nFilters /*uint32*/, IntPtr pRequestServersResponse /*class ISteamMatchmakingServerListResponse **/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamMatchmakingServers.RequestSpectatorServerList( _ptr, iApp, (IntPtr) ppchFilters, nFilters, (IntPtr) pRequestServersResponse );
			else return Platform.Win64.ISteamMatchmakingServers.RequestSpectatorServerList( _ptr, iApp, (IntPtr) ppchFilters, nFilters, (IntPtr) pRequestServersResponse );
		}
		
		// HServerQuery
		public HServerQuery ServerRules( uint unIP /*uint32*/, ushort usPort /*uint16*/, IntPtr pRequestServersResponse /*class ISteamMatchmakingRulesResponse **/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamMatchmakingServers.ServerRules( _ptr, unIP, usPort, (IntPtr) pRequestServersResponse );
			else return Platform.Win64.ISteamMatchmakingServers.ServerRules( _ptr, unIP, usPort, (IntPtr) pRequestServersResponse );
		}
		
	}
}
