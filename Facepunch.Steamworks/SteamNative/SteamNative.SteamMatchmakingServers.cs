using System;
using System.Runtime.InteropServices;

namespace SteamNative
{
	internal unsafe class SteamMatchmakingServers : IDisposable
	{
		//
		// Holds a platform specific implentation
		//
		internal Platform.Interface platform;
		internal Facepunch.Steamworks.BaseSteamworks steamworks;
		
		//
		// Constructor decides which implementation to use based on current platform
		//
		public SteamMatchmakingServers( Facepunch.Steamworks.BaseSteamworks steamworks, IntPtr pointer )
		{
			this.steamworks = steamworks;
			
			if ( Platform.IsWindows64 ) platform = new Platform.Win64( pointer );
			else if ( Platform.IsWindows32 ) platform = new Platform.Win32( pointer );
			else if ( Platform.IsLinux32 ) platform = new Platform.Linux32( pointer );
			else if ( Platform.IsLinux64 ) platform = new Platform.Linux64( pointer );
			else if ( Platform.IsOsx ) platform = new Platform.Mac( pointer );
		}
		
		//
		// Class is invalid if we don't have a valid implementation
		//
		public bool IsValid{ get{ return platform != null && platform.IsValid; } }
		
		//
		// When shutting down clear all the internals to avoid accidental use
		//
		public virtual void Dispose()
		{
			 if ( platform != null )
			{
				platform.Dispose();
				platform = null;
			}
		}
		
		// void
		public void CancelQuery( HServerListRequest hRequest /*HServerListRequest*/ )
		{
			platform.ISteamMatchmakingServers_CancelQuery( hRequest.Value );
		}
		
		// void
		public void CancelServerQuery( HServerQuery hServerQuery /*HServerQuery*/ )
		{
			platform.ISteamMatchmakingServers_CancelServerQuery( hServerQuery.Value );
		}
		
		// int
		public int GetServerCount( HServerListRequest hRequest /*HServerListRequest*/ )
		{
			return platform.ISteamMatchmakingServers_GetServerCount( hRequest.Value );
		}
		
		// gameserveritem_t *
		// with: Detect_ReturningStruct
		public gameserveritem_t GetServerDetails( HServerListRequest hRequest /*HServerListRequest*/, int iServer /*int*/ )
		{
			IntPtr struct_pointer;
			struct_pointer = platform.ISteamMatchmakingServers_GetServerDetails( hRequest.Value, iServer );
			if ( struct_pointer == IntPtr.Zero ) return default(gameserveritem_t);
			return gameserveritem_t.FromPointer( struct_pointer );
		}
		
		// bool
		public bool IsRefreshing( HServerListRequest hRequest /*HServerListRequest*/ )
		{
			return platform.ISteamMatchmakingServers_IsRefreshing( hRequest.Value );
		}
		
		// HServerQuery
		public HServerQuery PingServer( uint unIP /*uint32*/, ushort usPort /*uint16*/, IntPtr pRequestServersResponse /*class ISteamMatchmakingPingResponse **/ )
		{
			return platform.ISteamMatchmakingServers_PingServer( unIP, usPort, (IntPtr) pRequestServersResponse );
		}
		
		// HServerQuery
		public HServerQuery PlayerDetails( uint unIP /*uint32*/, ushort usPort /*uint16*/, IntPtr pRequestServersResponse /*class ISteamMatchmakingPlayersResponse **/ )
		{
			return platform.ISteamMatchmakingServers_PlayerDetails( unIP, usPort, (IntPtr) pRequestServersResponse );
		}
		
		// void
		public void RefreshQuery( HServerListRequest hRequest /*HServerListRequest*/ )
		{
			platform.ISteamMatchmakingServers_RefreshQuery( hRequest.Value );
		}
		
		// void
		public void RefreshServer( HServerListRequest hRequest /*HServerListRequest*/, int iServer /*int*/ )
		{
			platform.ISteamMatchmakingServers_RefreshServer( hRequest.Value, iServer );
		}
		
		// void
		public void ReleaseRequest( HServerListRequest hServerListRequest /*HServerListRequest*/ )
		{
			platform.ISteamMatchmakingServers_ReleaseRequest( hServerListRequest.Value );
		}
		
		// HServerListRequest
		// with: Detect_MatchmakingFilters
		public HServerListRequest RequestFavoritesServerList( AppId_t iApp /*AppId_t*/, IntPtr ppchFilters /*struct MatchMakingKeyValuePair_t ***/, uint nFilters /*uint32*/, IntPtr pRequestServersResponse /*class ISteamMatchmakingServerListResponse **/ )
		{
			return platform.ISteamMatchmakingServers_RequestFavoritesServerList( iApp.Value, (IntPtr) ppchFilters, nFilters, (IntPtr) pRequestServersResponse );
		}
		
		// HServerListRequest
		// with: Detect_MatchmakingFilters
		public HServerListRequest RequestFriendsServerList( AppId_t iApp /*AppId_t*/, IntPtr ppchFilters /*struct MatchMakingKeyValuePair_t ***/, uint nFilters /*uint32*/, IntPtr pRequestServersResponse /*class ISteamMatchmakingServerListResponse **/ )
		{
			return platform.ISteamMatchmakingServers_RequestFriendsServerList( iApp.Value, (IntPtr) ppchFilters, nFilters, (IntPtr) pRequestServersResponse );
		}
		
		// HServerListRequest
		// with: Detect_MatchmakingFilters
		public HServerListRequest RequestHistoryServerList( AppId_t iApp /*AppId_t*/, IntPtr ppchFilters /*struct MatchMakingKeyValuePair_t ***/, uint nFilters /*uint32*/, IntPtr pRequestServersResponse /*class ISteamMatchmakingServerListResponse **/ )
		{
			return platform.ISteamMatchmakingServers_RequestHistoryServerList( iApp.Value, (IntPtr) ppchFilters, nFilters, (IntPtr) pRequestServersResponse );
		}
		
		// HServerListRequest
		// with: Detect_MatchmakingFilters
		public HServerListRequest RequestInternetServerList( AppId_t iApp /*AppId_t*/, IntPtr ppchFilters /*struct MatchMakingKeyValuePair_t ***/, uint nFilters /*uint32*/, IntPtr pRequestServersResponse /*class ISteamMatchmakingServerListResponse **/ )
		{
			return platform.ISteamMatchmakingServers_RequestInternetServerList( iApp.Value, (IntPtr) ppchFilters, nFilters, (IntPtr) pRequestServersResponse );
		}
		
		// HServerListRequest
		public HServerListRequest RequestLANServerList( AppId_t iApp /*AppId_t*/, IntPtr pRequestServersResponse /*class ISteamMatchmakingServerListResponse **/ )
		{
			return platform.ISteamMatchmakingServers_RequestLANServerList( iApp.Value, (IntPtr) pRequestServersResponse );
		}
		
		// HServerListRequest
		// with: Detect_MatchmakingFilters
		public HServerListRequest RequestSpectatorServerList( AppId_t iApp /*AppId_t*/, IntPtr ppchFilters /*struct MatchMakingKeyValuePair_t ***/, uint nFilters /*uint32*/, IntPtr pRequestServersResponse /*class ISteamMatchmakingServerListResponse **/ )
		{
			return platform.ISteamMatchmakingServers_RequestSpectatorServerList( iApp.Value, (IntPtr) ppchFilters, nFilters, (IntPtr) pRequestServersResponse );
		}
		
		// HServerQuery
		public HServerQuery ServerRules( uint unIP /*uint32*/, ushort usPort /*uint16*/, IntPtr pRequestServersResponse /*class ISteamMatchmakingRulesResponse **/ )
		{
			return platform.ISteamMatchmakingServers_ServerRules( unIP, usPort, (IntPtr) pRequestServersResponse );
		}
		
	}
}
