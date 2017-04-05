using System;
using System.Runtime.InteropServices;
using System.Linq;

namespace SteamNative
{
	internal unsafe class SteamMatchmaking : IDisposable
	{
		//
		// Holds a platform specific implentation
		//
		internal Platform.Interface platform;
		internal Facepunch.Steamworks.BaseSteamworks steamworks;
		
		//
		// Constructor decides which implementation to use based on current platform
		//
		internal SteamMatchmaking( Facepunch.Steamworks.BaseSteamworks steamworks, IntPtr pointer )
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
		
		// int
		public int AddFavoriteGame( AppId_t nAppID /*AppId_t*/, uint nIP /*uint32*/, ushort nConnPort /*uint16*/, ushort nQueryPort /*uint16*/, uint unFlags /*uint32*/, uint rTime32LastPlayedOnServer /*uint32*/ )
		{
			return platform.ISteamMatchmaking_AddFavoriteGame( nAppID.Value, nIP, nConnPort, nQueryPort, unFlags, rTime32LastPlayedOnServer );
		}
		
		// void
		public void AddRequestLobbyListCompatibleMembersFilter( CSteamID steamIDLobby /*class CSteamID*/ )
		{
			platform.ISteamMatchmaking_AddRequestLobbyListCompatibleMembersFilter( steamIDLobby.Value );
		}
		
		// void
		public void AddRequestLobbyListDistanceFilter( LobbyDistanceFilter eLobbyDistanceFilter /*ELobbyDistanceFilter*/ )
		{
			platform.ISteamMatchmaking_AddRequestLobbyListDistanceFilter( eLobbyDistanceFilter );
		}
		
		// void
		public void AddRequestLobbyListFilterSlotsAvailable( int nSlotsAvailable /*int*/ )
		{
			platform.ISteamMatchmaking_AddRequestLobbyListFilterSlotsAvailable( nSlotsAvailable );
		}
		
		// void
		public void AddRequestLobbyListNearValueFilter( string pchKeyToMatch /*const char **/, int nValueToBeCloseTo /*int*/ )
		{
			platform.ISteamMatchmaking_AddRequestLobbyListNearValueFilter( pchKeyToMatch, nValueToBeCloseTo );
		}
		
		// void
		public void AddRequestLobbyListNumericalFilter( string pchKeyToMatch /*const char **/, int nValueToMatch /*int*/, LobbyComparison eComparisonType /*ELobbyComparison*/ )
		{
			platform.ISteamMatchmaking_AddRequestLobbyListNumericalFilter( pchKeyToMatch, nValueToMatch, eComparisonType );
		}
		
		// void
		public void AddRequestLobbyListResultCountFilter( int cMaxResults /*int*/ )
		{
			platform.ISteamMatchmaking_AddRequestLobbyListResultCountFilter( cMaxResults );
		}
		
		// void
		public void AddRequestLobbyListStringFilter( string pchKeyToMatch /*const char **/, string pchValueToMatch /*const char **/, LobbyComparison eComparisonType /*ELobbyComparison*/ )
		{
			platform.ISteamMatchmaking_AddRequestLobbyListStringFilter( pchKeyToMatch, pchValueToMatch, eComparisonType );
		}
		
		// SteamAPICall_t
		public CallbackHandle CreateLobby( LobbyType eLobbyType /*ELobbyType*/, int cMaxMembers /*int*/, Action<LobbyCreated_t, bool> CallbackFunction = null /*Action<LobbyCreated_t, bool>*/ )
		{
			SteamAPICall_t callback = 0;
			callback = platform.ISteamMatchmaking_CreateLobby( eLobbyType, cMaxMembers );
			
			if ( CallbackFunction == null ) return null;
			
			return LobbyCreated_t.CallResult( steamworks, callback, CallbackFunction );
		}
		
		// bool
		public bool DeleteLobbyData( CSteamID steamIDLobby /*class CSteamID*/, string pchKey /*const char **/ )
		{
			return platform.ISteamMatchmaking_DeleteLobbyData( steamIDLobby.Value, pchKey );
		}
		
		// bool
		public bool GetFavoriteGame( int iGame /*int*/, ref AppId_t pnAppID /*AppId_t **/, out uint pnIP /*uint32 **/, out ushort pnConnPort /*uint16 **/, out ushort pnQueryPort /*uint16 **/, out uint punFlags /*uint32 **/, out uint pRTime32LastPlayedOnServer /*uint32 **/ )
		{
			return platform.ISteamMatchmaking_GetFavoriteGame( iGame, ref pnAppID.Value, out pnIP, out pnConnPort, out pnQueryPort, out punFlags, out pRTime32LastPlayedOnServer );
		}
		
		// int
		public int GetFavoriteGameCount()
		{
			return platform.ISteamMatchmaking_GetFavoriteGameCount();
		}
		
		// ulong
		public ulong GetLobbyByIndex( int iLobby /*int*/ )
		{
			return platform.ISteamMatchmaking_GetLobbyByIndex( iLobby );
		}
		
		// int
		public int GetLobbyChatEntry( CSteamID steamIDLobby /*class CSteamID*/, int iChatID /*int*/, out CSteamID pSteamIDUser /*class CSteamID **/, IntPtr pvData /*void **/, int cubData /*int*/, out ChatEntryType peChatEntryType /*EChatEntryType **/ )
		{
			return platform.ISteamMatchmaking_GetLobbyChatEntry( steamIDLobby.Value, iChatID, out pSteamIDUser.Value, (IntPtr) pvData, cubData, out peChatEntryType );
		}
		
		// string
		// with: Detect_StringReturn
		public string GetLobbyData( CSteamID steamIDLobby /*class CSteamID*/, string pchKey /*const char **/ )
		{
			IntPtr string_pointer;
			string_pointer = platform.ISteamMatchmaking_GetLobbyData( steamIDLobby.Value, pchKey );
			return Marshal.PtrToStringAnsi( string_pointer );
		}
		
		// bool
		// with: Detect_StringFetch False
		// with: Detect_StringFetch False
		public bool GetLobbyDataByIndex( CSteamID steamIDLobby /*class CSteamID*/, int iLobbyData /*int*/, out string pchKey /*char **/, out string pchValue /*char **/ )
		{
			bool bSuccess = default( bool );
			pchKey = string.Empty;
			System.Text.StringBuilder pchKey_sb = Helpers.TakeStringBuilder();
			int cchKeyBufferSize = 4096;
			pchValue = string.Empty;
			System.Text.StringBuilder pchValue_sb = Helpers.TakeStringBuilder();
			int cchValueBufferSize = 4096;
			bSuccess = platform.ISteamMatchmaking_GetLobbyDataByIndex( steamIDLobby.Value, iLobbyData, pchKey_sb, cchKeyBufferSize, pchValue_sb, cchValueBufferSize );
			if ( !bSuccess ) return bSuccess;
			pchValue = pchValue_sb.ToString();
			if ( !bSuccess ) return bSuccess;
			pchKey = pchKey_sb.ToString();
			return bSuccess;
		}
		
		// int
		public int GetLobbyDataCount( CSteamID steamIDLobby /*class CSteamID*/ )
		{
			return platform.ISteamMatchmaking_GetLobbyDataCount( steamIDLobby.Value );
		}
		
		// bool
		public bool GetLobbyGameServer( CSteamID steamIDLobby /*class CSteamID*/, out uint punGameServerIP /*uint32 **/, out ushort punGameServerPort /*uint16 **/, out CSteamID psteamIDGameServer /*class CSteamID **/ )
		{
			return platform.ISteamMatchmaking_GetLobbyGameServer( steamIDLobby.Value, out punGameServerIP, out punGameServerPort, out psteamIDGameServer.Value );
		}
		
		// ulong
		public ulong GetLobbyMemberByIndex( CSteamID steamIDLobby /*class CSteamID*/, int iMember /*int*/ )
		{
			return platform.ISteamMatchmaking_GetLobbyMemberByIndex( steamIDLobby.Value, iMember );
		}
		
		// string
		// with: Detect_StringReturn
		public string GetLobbyMemberData( CSteamID steamIDLobby /*class CSteamID*/, CSteamID steamIDUser /*class CSteamID*/, string pchKey /*const char **/ )
		{
			IntPtr string_pointer;
			string_pointer = platform.ISteamMatchmaking_GetLobbyMemberData( steamIDLobby.Value, steamIDUser.Value, pchKey );
			return Marshal.PtrToStringAnsi( string_pointer );
		}
		
		// int
		public int GetLobbyMemberLimit( CSteamID steamIDLobby /*class CSteamID*/ )
		{
			return platform.ISteamMatchmaking_GetLobbyMemberLimit( steamIDLobby.Value );
		}
		
		// ulong
		public ulong GetLobbyOwner( CSteamID steamIDLobby /*class CSteamID*/ )
		{
			return platform.ISteamMatchmaking_GetLobbyOwner( steamIDLobby.Value );
		}
		
		// int
		public int GetNumLobbyMembers( CSteamID steamIDLobby /*class CSteamID*/ )
		{
			return platform.ISteamMatchmaking_GetNumLobbyMembers( steamIDLobby.Value );
		}
		
		// bool
		public bool InviteUserToLobby( CSteamID steamIDLobby /*class CSteamID*/, CSteamID steamIDInvitee /*class CSteamID*/ )
		{
			return platform.ISteamMatchmaking_InviteUserToLobby( steamIDLobby.Value, steamIDInvitee.Value );
		}
		
		// SteamAPICall_t
		public CallbackHandle JoinLobby( CSteamID steamIDLobby /*class CSteamID*/, Action<LobbyEnter_t, bool> CallbackFunction = null /*Action<LobbyEnter_t, bool>*/ )
		{
			SteamAPICall_t callback = 0;
			callback = platform.ISteamMatchmaking_JoinLobby( steamIDLobby.Value );
			
			if ( CallbackFunction == null ) return null;
			
			return LobbyEnter_t.CallResult( steamworks, callback, CallbackFunction );
		}
		
		// void
		public void LeaveLobby( CSteamID steamIDLobby /*class CSteamID*/ )
		{
			platform.ISteamMatchmaking_LeaveLobby( steamIDLobby.Value );
		}
		
		// bool
		public bool RemoveFavoriteGame( AppId_t nAppID /*AppId_t*/, uint nIP /*uint32*/, ushort nConnPort /*uint16*/, ushort nQueryPort /*uint16*/, uint unFlags /*uint32*/ )
		{
			return platform.ISteamMatchmaking_RemoveFavoriteGame( nAppID.Value, nIP, nConnPort, nQueryPort, unFlags );
		}
		
		// bool
		public bool RequestLobbyData( CSteamID steamIDLobby /*class CSteamID*/ )
		{
			return platform.ISteamMatchmaking_RequestLobbyData( steamIDLobby.Value );
		}
		
		// SteamAPICall_t
		public CallbackHandle RequestLobbyList( Action<LobbyMatchList_t, bool> CallbackFunction = null /*Action<LobbyMatchList_t, bool>*/ )
		{
			SteamAPICall_t callback = 0;
			callback = platform.ISteamMatchmaking_RequestLobbyList();
			
			if ( CallbackFunction == null ) return null;
			
			return LobbyMatchList_t.CallResult( steamworks, callback, CallbackFunction );
		}
		
		// bool
		public bool SendLobbyChatMsg( CSteamID steamIDLobby /*class CSteamID*/, IntPtr pvMsgBody /*const void **/, int cubMsgBody /*int*/ )
		{
			return platform.ISteamMatchmaking_SendLobbyChatMsg( steamIDLobby.Value, (IntPtr) pvMsgBody, cubMsgBody );
		}
		
		// bool
		public bool SetLinkedLobby( CSteamID steamIDLobby /*class CSteamID*/, CSteamID steamIDLobbyDependent /*class CSteamID*/ )
		{
			return platform.ISteamMatchmaking_SetLinkedLobby( steamIDLobby.Value, steamIDLobbyDependent.Value );
		}
		
		// bool
		public bool SetLobbyData( CSteamID steamIDLobby /*class CSteamID*/, string pchKey /*const char **/, string pchValue /*const char **/ )
		{
			return platform.ISteamMatchmaking_SetLobbyData( steamIDLobby.Value, pchKey, pchValue );
		}
		
		// void
		public void SetLobbyGameServer( CSteamID steamIDLobby /*class CSteamID*/, uint unGameServerIP /*uint32*/, ushort unGameServerPort /*uint16*/, CSteamID steamIDGameServer /*class CSteamID*/ )
		{
			platform.ISteamMatchmaking_SetLobbyGameServer( steamIDLobby.Value, unGameServerIP, unGameServerPort, steamIDGameServer.Value );
		}
		
		// bool
		public bool SetLobbyJoinable( CSteamID steamIDLobby /*class CSteamID*/, bool bLobbyJoinable /*bool*/ )
		{
			return platform.ISteamMatchmaking_SetLobbyJoinable( steamIDLobby.Value, bLobbyJoinable );
		}
		
		// void
		public void SetLobbyMemberData( CSteamID steamIDLobby /*class CSteamID*/, string pchKey /*const char **/, string pchValue /*const char **/ )
		{
			platform.ISteamMatchmaking_SetLobbyMemberData( steamIDLobby.Value, pchKey, pchValue );
		}
		
		// bool
		public bool SetLobbyMemberLimit( CSteamID steamIDLobby /*class CSteamID*/, int cMaxMembers /*int*/ )
		{
			return platform.ISteamMatchmaking_SetLobbyMemberLimit( steamIDLobby.Value, cMaxMembers );
		}
		
		// bool
		public bool SetLobbyOwner( CSteamID steamIDLobby /*class CSteamID*/, CSteamID steamIDNewOwner /*class CSteamID*/ )
		{
			return platform.ISteamMatchmaking_SetLobbyOwner( steamIDLobby.Value, steamIDNewOwner.Value );
		}
		
		// bool
		public bool SetLobbyType( CSteamID steamIDLobby /*class CSteamID*/, LobbyType eLobbyType /*ELobbyType*/ )
		{
			return platform.ISteamMatchmaking_SetLobbyType( steamIDLobby.Value, eLobbyType );
		}
		
	}
}
