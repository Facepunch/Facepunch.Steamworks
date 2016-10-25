using System;
using System.Runtime.InteropServices;

namespace SteamNative
{
	public unsafe class SteamMatchmakingServers : IDisposable
	{
		//
		// Holds a platform specific implentation
		//
		internal Platform.Interface _pi;
		
		//
		// Constructor decides which implementation to use based on current platform
		//
		public SteamMatchmakingServers( IntPtr pointer )
		{
			if ( Platform.IsWindows64 ) _pi = new Platform.Win64( pointer );
			else if ( Platform.IsWindows32 ) _pi = new Platform.Win32( pointer );
			else if ( Platform.IsLinux32 ) _pi = new Platform.Linux32( pointer );
			else if ( Platform.IsLinux64 ) _pi = new Platform.Linux64( pointer );
			else if ( Platform.IsOsx ) _pi = new Platform.Mac( pointer );
		}
		
		//
		// Class is invalid if we don't have a valid implementation
		//
		public bool IsValid{ get{ return _pi != null && _pi.IsValid; } }
		
		//
		// When shutting down clear all the internals to avoid accidental use
		//
		public virtual void Dispose()
		{
			 if ( _pi != null )
			{
				_pi.Dispose();
				_pi = null;
			}
		}
		
		// void
		public void CancelQuery( HServerListRequest hRequest /*HServerListRequest*/ )
		{
			_pi.ISteamMatchmakingServers_CancelQuery( hRequest );
		}
		
		// void
		public void CancelServerQuery( HServerQuery hServerQuery /*HServerQuery*/ )
		{
			_pi.ISteamMatchmakingServers_CancelServerQuery( hServerQuery );
		}
		
		// int
		public int GetServerCount( HServerListRequest hRequest /*HServerListRequest*/ )
		{
			return _pi.ISteamMatchmakingServers_GetServerCount( hRequest );
		}
		
		// gameserveritem_t *
		// with: Detect_ReturningStruct
		public gameserveritem_t GetServerDetails( HServerListRequest hRequest /*HServerListRequest*/, int iServer /*int*/ )
		{
			IntPtr struct_pointer;
			struct_pointer = _pi.ISteamMatchmakingServers_GetServerDetails( hRequest, iServer );
			if ( struct_pointer == IntPtr.Zero ) return default(gameserveritem_t);
			return gameserveritem_t.FromPointer( struct_pointer );
		}
		
		// bool
		public bool IsRefreshing( HServerListRequest hRequest /*HServerListRequest*/ )
		{
			return _pi.ISteamMatchmakingServers_IsRefreshing( hRequest );
		}
		
		// HServerQuery
		public HServerQuery PingServer( uint unIP /*uint32*/, ushort usPort /*uint16*/, IntPtr pRequestServersResponse /*class ISteamMatchmakingPingResponse **/ )
		{
			return _pi.ISteamMatchmakingServers_PingServer( unIP, usPort, (IntPtr) pRequestServersResponse );
		}
		
		// HServerQuery
		public HServerQuery PlayerDetails( uint unIP /*uint32*/, ushort usPort /*uint16*/, IntPtr pRequestServersResponse /*class ISteamMatchmakingPlayersResponse **/ )
		{
			return _pi.ISteamMatchmakingServers_PlayerDetails( unIP, usPort, (IntPtr) pRequestServersResponse );
		}
		
		// void
		public void RefreshQuery( HServerListRequest hRequest /*HServerListRequest*/ )
		{
			_pi.ISteamMatchmakingServers_RefreshQuery( hRequest );
		}
		
		// void
		public void RefreshServer( HServerListRequest hRequest /*HServerListRequest*/, int iServer /*int*/ )
		{
			_pi.ISteamMatchmakingServers_RefreshServer( hRequest, iServer );
		}
		
		// void
		public void ReleaseRequest( HServerListRequest hServerListRequest /*HServerListRequest*/ )
		{
			_pi.ISteamMatchmakingServers_ReleaseRequest( hServerListRequest );
		}
		
		// HServerListRequest
		// with: Detect_MatchmakingFilters
		public HServerListRequest RequestFavoritesServerList( AppId_t iApp /*AppId_t*/, IntPtr ppchFilters /*struct MatchMakingKeyValuePair_t ***/, uint nFilters /*uint32*/, IntPtr pRequestServersResponse /*class ISteamMatchmakingServerListResponse **/ )
		{
			return _pi.ISteamMatchmakingServers_RequestFavoritesServerList( iApp, (IntPtr) ppchFilters, nFilters, (IntPtr) pRequestServersResponse );
		}
		
		// HServerListRequest
		// with: Detect_MatchmakingFilters
		public HServerListRequest RequestFriendsServerList( AppId_t iApp /*AppId_t*/, IntPtr ppchFilters /*struct MatchMakingKeyValuePair_t ***/, uint nFilters /*uint32*/, IntPtr pRequestServersResponse /*class ISteamMatchmakingServerListResponse **/ )
		{
			return _pi.ISteamMatchmakingServers_RequestFriendsServerList( iApp, (IntPtr) ppchFilters, nFilters, (IntPtr) pRequestServersResponse );
		}
		
		// HServerListRequest
		// with: Detect_MatchmakingFilters
		public HServerListRequest RequestHistoryServerList( AppId_t iApp /*AppId_t*/, IntPtr ppchFilters /*struct MatchMakingKeyValuePair_t ***/, uint nFilters /*uint32*/, IntPtr pRequestServersResponse /*class ISteamMatchmakingServerListResponse **/ )
		{
			return _pi.ISteamMatchmakingServers_RequestHistoryServerList( iApp, (IntPtr) ppchFilters, nFilters, (IntPtr) pRequestServersResponse );
		}
		
		// HServerListRequest
		// with: Detect_MatchmakingFilters
		public HServerListRequest RequestInternetServerList( AppId_t iApp /*AppId_t*/, IntPtr ppchFilters /*struct MatchMakingKeyValuePair_t ***/, uint nFilters /*uint32*/, IntPtr pRequestServersResponse /*class ISteamMatchmakingServerListResponse **/ )
		{
			return _pi.ISteamMatchmakingServers_RequestInternetServerList( iApp, (IntPtr) ppchFilters, nFilters, (IntPtr) pRequestServersResponse );
		}
		
		// HServerListRequest
		public HServerListRequest RequestLANServerList( AppId_t iApp /*AppId_t*/, IntPtr pRequestServersResponse /*class ISteamMatchmakingServerListResponse **/ )
		{
			return _pi.ISteamMatchmakingServers_RequestLANServerList( iApp, (IntPtr) pRequestServersResponse );
		}
		
		// HServerListRequest
		// with: Detect_MatchmakingFilters
		public HServerListRequest RequestSpectatorServerList( AppId_t iApp /*AppId_t*/, IntPtr ppchFilters /*struct MatchMakingKeyValuePair_t ***/, uint nFilters /*uint32*/, IntPtr pRequestServersResponse /*class ISteamMatchmakingServerListResponse **/ )
		{
			return _pi.ISteamMatchmakingServers_RequestSpectatorServerList( iApp, (IntPtr) ppchFilters, nFilters, (IntPtr) pRequestServersResponse );
		}
		
		// HServerQuery
		public HServerQuery ServerRules( uint unIP /*uint32*/, ushort usPort /*uint16*/, IntPtr pRequestServersResponse /*class ISteamMatchmakingRulesResponse **/ )
		{
			return _pi.ISteamMatchmakingServers_ServerRules( unIP, usPort, (IntPtr) pRequestServersResponse );
		}
		
	}
}
