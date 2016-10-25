using System;
using System.Runtime.InteropServices;

namespace SteamNative
{
	public unsafe class SteamMatchmaking : IDisposable
	{
		//
		// Holds a platform specific implentation
		//
		internal Platform.Interface _pi;
		
		//
		// Constructor decides which implementation to use based on current platform
		//
		public SteamMatchmaking( IntPtr pointer )
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
		
		// int
		public int AddFavoriteGame( AppId_t nAppID /*AppId_t*/, uint nIP /*uint32*/, ushort nConnPort /*uint16*/, ushort nQueryPort /*uint16*/, uint unFlags /*uint32*/, uint rTime32LastPlayedOnServer /*uint32*/ )
		{
			return _pi.ISteamMatchmaking_AddFavoriteGame( nAppID, nIP, nConnPort, nQueryPort, unFlags, rTime32LastPlayedOnServer );
		}
		
		// void
		public void AddRequestLobbyListCompatibleMembersFilter( CSteamID steamIDLobby /*class CSteamID*/ )
		{
			_pi.ISteamMatchmaking_AddRequestLobbyListCompatibleMembersFilter( steamIDLobby );
		}
		
		// void
		public void AddRequestLobbyListDistanceFilter( LobbyDistanceFilter eLobbyDistanceFilter /*ELobbyDistanceFilter*/ )
		{
			_pi.ISteamMatchmaking_AddRequestLobbyListDistanceFilter( eLobbyDistanceFilter );
		}
		
		// void
		public void AddRequestLobbyListFilterSlotsAvailable( int nSlotsAvailable /*int*/ )
		{
			_pi.ISteamMatchmaking_AddRequestLobbyListFilterSlotsAvailable( nSlotsAvailable );
		}
		
		// void
		public void AddRequestLobbyListNearValueFilter( string pchKeyToMatch /*const char **/, int nValueToBeCloseTo /*int*/ )
		{
			_pi.ISteamMatchmaking_AddRequestLobbyListNearValueFilter( pchKeyToMatch, nValueToBeCloseTo );
		}
		
		// void
		public void AddRequestLobbyListNumericalFilter( string pchKeyToMatch /*const char **/, int nValueToMatch /*int*/, LobbyComparison eComparisonType /*ELobbyComparison*/ )
		{
			_pi.ISteamMatchmaking_AddRequestLobbyListNumericalFilter( pchKeyToMatch, nValueToMatch, eComparisonType );
		}
		
		// void
		public void AddRequestLobbyListResultCountFilter( int cMaxResults /*int*/ )
		{
			_pi.ISteamMatchmaking_AddRequestLobbyListResultCountFilter( cMaxResults );
		}
		
		// void
		public void AddRequestLobbyListStringFilter( string pchKeyToMatch /*const char **/, string pchValueToMatch /*const char **/, LobbyComparison eComparisonType /*ELobbyComparison*/ )
		{
			_pi.ISteamMatchmaking_AddRequestLobbyListStringFilter( pchKeyToMatch, pchValueToMatch, eComparisonType );
		}
		
		// SteamAPICall_t
		public SteamAPICall_t CreateLobby( LobbyType eLobbyType /*ELobbyType*/, int cMaxMembers /*int*/ )
		{
			return _pi.ISteamMatchmaking_CreateLobby( eLobbyType, cMaxMembers );
		}
		
		// bool
		public bool DeleteLobbyData( CSteamID steamIDLobby /*class CSteamID*/, string pchKey /*const char **/ )
		{
			return _pi.ISteamMatchmaking_DeleteLobbyData( steamIDLobby, pchKey );
		}
		
		// bool
		public bool GetFavoriteGame( int iGame /*int*/, ref AppId_t pnAppID /*AppId_t **/, out uint pnIP /*uint32 **/, out ushort pnConnPort /*uint16 **/, out ushort pnQueryPort /*uint16 **/, IntPtr punFlags /*uint32 **/, out uint pRTime32LastPlayedOnServer /*uint32 **/ )
		{
			return _pi.ISteamMatchmaking_GetFavoriteGame( iGame, ref pnAppID, out pnIP, out pnConnPort, out pnQueryPort, (IntPtr) punFlags, out pRTime32LastPlayedOnServer );
		}
		
		// int
		public int GetFavoriteGameCount()
		{
			return _pi.ISteamMatchmaking_GetFavoriteGameCount();
		}
		
		// ulong
		public ulong GetLobbyByIndex( int iLobby /*int*/ )
		{
			return _pi.ISteamMatchmaking_GetLobbyByIndex( iLobby );
		}
		
		// int
		public int GetLobbyChatEntry( CSteamID steamIDLobby /*class CSteamID*/, int iChatID /*int*/, out CSteamID pSteamIDUser /*class CSteamID **/, IntPtr pvData /*void **/, int cubData /*int*/, out ChatEntryType peChatEntryType /*EChatEntryType **/ )
		{
			return _pi.ISteamMatchmaking_GetLobbyChatEntry( steamIDLobby, iChatID, out pSteamIDUser, (IntPtr) pvData, cubData, out peChatEntryType );
		}
		
		// string
		// with: Detect_StringReturn
		public string GetLobbyData( CSteamID steamIDLobby /*class CSteamID*/, string pchKey /*const char **/ )
		{
			IntPtr string_pointer;
			string_pointer = _pi.ISteamMatchmaking_GetLobbyData( steamIDLobby, pchKey );
			return Marshal.PtrToStringAnsi( string_pointer );
		}
		
		// bool
		// with: Detect_StringFetch False
		// with: Detect_StringFetch False
		public bool GetLobbyDataByIndex( CSteamID steamIDLobby /*class CSteamID*/, int iLobbyData /*int*/, out string pchKey /*char **/, out string pchValue /*char **/ )
		{
			bool bSuccess = default( bool );
			pchKey = string.Empty;
			System.Text.StringBuilder pchKey_sb = new System.Text.StringBuilder( 4096 );
			int cchKeyBufferSize = 4096;
			pchValue = string.Empty;
			System.Text.StringBuilder pchValue_sb = new System.Text.StringBuilder( 4096 );
			int cchValueBufferSize = 4096;
			bSuccess = _pi.ISteamMatchmaking_GetLobbyDataByIndex( steamIDLobby, iLobbyData, pchKey_sb, cchKeyBufferSize, pchValue_sb, cchValueBufferSize );
			if ( !bSuccess ) return bSuccess;
			pchValue = pchValue_sb.ToString();
			if ( !bSuccess ) return bSuccess;
			pchKey = pchKey_sb.ToString();
			return bSuccess;
		}
		
		// int
		public int GetLobbyDataCount( CSteamID steamIDLobby /*class CSteamID*/ )
		{
			return _pi.ISteamMatchmaking_GetLobbyDataCount( steamIDLobby );
		}
		
		// bool
		public bool GetLobbyGameServer( CSteamID steamIDLobby /*class CSteamID*/, out uint punGameServerIP /*uint32 **/, out ushort punGameServerPort /*uint16 **/, out CSteamID psteamIDGameServer /*class CSteamID **/ )
		{
			return _pi.ISteamMatchmaking_GetLobbyGameServer( steamIDLobby, out punGameServerIP, out punGameServerPort, out psteamIDGameServer );
		}
		
		// ulong
		public ulong GetLobbyMemberByIndex( CSteamID steamIDLobby /*class CSteamID*/, int iMember /*int*/ )
		{
			return _pi.ISteamMatchmaking_GetLobbyMemberByIndex( steamIDLobby, iMember );
		}
		
		// string
		// with: Detect_StringReturn
		public string GetLobbyMemberData( CSteamID steamIDLobby /*class CSteamID*/, CSteamID steamIDUser /*class CSteamID*/, string pchKey /*const char **/ )
		{
			IntPtr string_pointer;
			string_pointer = _pi.ISteamMatchmaking_GetLobbyMemberData( steamIDLobby, steamIDUser, pchKey );
			return Marshal.PtrToStringAnsi( string_pointer );
		}
		
		// int
		public int GetLobbyMemberLimit( CSteamID steamIDLobby /*class CSteamID*/ )
		{
			return _pi.ISteamMatchmaking_GetLobbyMemberLimit( steamIDLobby );
		}
		
		// ulong
		public ulong GetLobbyOwner( CSteamID steamIDLobby /*class CSteamID*/ )
		{
			return _pi.ISteamMatchmaking_GetLobbyOwner( steamIDLobby );
		}
		
		// int
		public int GetNumLobbyMembers( CSteamID steamIDLobby /*class CSteamID*/ )
		{
			return _pi.ISteamMatchmaking_GetNumLobbyMembers( steamIDLobby );
		}
		
		// bool
		public bool InviteUserToLobby( CSteamID steamIDLobby /*class CSteamID*/, CSteamID steamIDInvitee /*class CSteamID*/ )
		{
			return _pi.ISteamMatchmaking_InviteUserToLobby( steamIDLobby, steamIDInvitee );
		}
		
		// SteamAPICall_t
		public SteamAPICall_t JoinLobby( CSteamID steamIDLobby /*class CSteamID*/ )
		{
			return _pi.ISteamMatchmaking_JoinLobby( steamIDLobby );
		}
		
		// void
		public void LeaveLobby( CSteamID steamIDLobby /*class CSteamID*/ )
		{
			_pi.ISteamMatchmaking_LeaveLobby( steamIDLobby );
		}
		
		// bool
		public bool RemoveFavoriteGame( AppId_t nAppID /*AppId_t*/, uint nIP /*uint32*/, ushort nConnPort /*uint16*/, ushort nQueryPort /*uint16*/, uint unFlags /*uint32*/ )
		{
			return _pi.ISteamMatchmaking_RemoveFavoriteGame( nAppID, nIP, nConnPort, nQueryPort, unFlags );
		}
		
		// bool
		public bool RequestLobbyData( CSteamID steamIDLobby /*class CSteamID*/ )
		{
			return _pi.ISteamMatchmaking_RequestLobbyData( steamIDLobby );
		}
		
		// SteamAPICall_t
		public SteamAPICall_t RequestLobbyList()
		{
			return _pi.ISteamMatchmaking_RequestLobbyList();
		}
		
		// bool
		public bool SendLobbyChatMsg( CSteamID steamIDLobby /*class CSteamID*/, IntPtr pvMsgBody /*const void **/, int cubMsgBody /*int*/ )
		{
			return _pi.ISteamMatchmaking_SendLobbyChatMsg( steamIDLobby, (IntPtr) pvMsgBody, cubMsgBody );
		}
		
		// bool
		public bool SetLinkedLobby( CSteamID steamIDLobby /*class CSteamID*/, CSteamID steamIDLobbyDependent /*class CSteamID*/ )
		{
			return _pi.ISteamMatchmaking_SetLinkedLobby( steamIDLobby, steamIDLobbyDependent );
		}
		
		// bool
		public bool SetLobbyData( CSteamID steamIDLobby /*class CSteamID*/, string pchKey /*const char **/, string pchValue /*const char **/ )
		{
			return _pi.ISteamMatchmaking_SetLobbyData( steamIDLobby, pchKey, pchValue );
		}
		
		// void
		public void SetLobbyGameServer( CSteamID steamIDLobby /*class CSteamID*/, uint unGameServerIP /*uint32*/, ushort unGameServerPort /*uint16*/, CSteamID steamIDGameServer /*class CSteamID*/ )
		{
			_pi.ISteamMatchmaking_SetLobbyGameServer( steamIDLobby, unGameServerIP, unGameServerPort, steamIDGameServer );
		}
		
		// bool
		public bool SetLobbyJoinable( CSteamID steamIDLobby /*class CSteamID*/, bool bLobbyJoinable /*bool*/ )
		{
			return _pi.ISteamMatchmaking_SetLobbyJoinable( steamIDLobby, bLobbyJoinable );
		}
		
		// void
		public void SetLobbyMemberData( CSteamID steamIDLobby /*class CSteamID*/, string pchKey /*const char **/, string pchValue /*const char **/ )
		{
			_pi.ISteamMatchmaking_SetLobbyMemberData( steamIDLobby, pchKey, pchValue );
		}
		
		// bool
		public bool SetLobbyMemberLimit( CSteamID steamIDLobby /*class CSteamID*/, int cMaxMembers /*int*/ )
		{
			return _pi.ISteamMatchmaking_SetLobbyMemberLimit( steamIDLobby, cMaxMembers );
		}
		
		// bool
		public bool SetLobbyOwner( CSteamID steamIDLobby /*class CSteamID*/, CSteamID steamIDNewOwner /*class CSteamID*/ )
		{
			return _pi.ISteamMatchmaking_SetLobbyOwner( steamIDLobby, steamIDNewOwner );
		}
		
		// bool
		public bool SetLobbyType( CSteamID steamIDLobby /*class CSteamID*/, LobbyType eLobbyType /*ELobbyType*/ )
		{
			return _pi.ISteamMatchmaking_SetLobbyType( steamIDLobby, eLobbyType );
		}
		
	}
}
