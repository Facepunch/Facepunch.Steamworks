using System;
using System.Runtime.InteropServices;

namespace SteamNative
{
	public unsafe class SteamMatchmaking
	{
		internal IntPtr _ptr;
		
		public SteamMatchmaking( IntPtr pointer )
		{
			_ptr = pointer;
		}
		
		
		// int
		public int AddFavoriteGame( AppId_t nAppID /*AppId_t*/, uint nIP /*uint32*/, ushort nConnPort /*uint16*/, ushort nQueryPort /*uint16*/, uint unFlags /*uint32*/, uint rTime32LastPlayedOnServer /*uint32*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamMatchmaking.AddFavoriteGame( _ptr, nAppID, nIP, nConnPort, nQueryPort, unFlags, rTime32LastPlayedOnServer );
			else return Platform.Win64.ISteamMatchmaking.AddFavoriteGame( _ptr, nAppID, nIP, nConnPort, nQueryPort, unFlags, rTime32LastPlayedOnServer );
		}
		
		// void
		public void AddRequestLobbyListCompatibleMembersFilter( CSteamID steamIDLobby /*class CSteamID*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) Platform.Win32.ISteamMatchmaking.AddRequestLobbyListCompatibleMembersFilter( _ptr, steamIDLobby );
			else Platform.Win64.ISteamMatchmaking.AddRequestLobbyListCompatibleMembersFilter( _ptr, steamIDLobby );
		}
		
		// void
		public void AddRequestLobbyListDistanceFilter( LobbyDistanceFilter eLobbyDistanceFilter /*ELobbyDistanceFilter*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) Platform.Win32.ISteamMatchmaking.AddRequestLobbyListDistanceFilter( _ptr, eLobbyDistanceFilter );
			else Platform.Win64.ISteamMatchmaking.AddRequestLobbyListDistanceFilter( _ptr, eLobbyDistanceFilter );
		}
		
		// void
		public void AddRequestLobbyListFilterSlotsAvailable( int nSlotsAvailable /*int*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) Platform.Win32.ISteamMatchmaking.AddRequestLobbyListFilterSlotsAvailable( _ptr, nSlotsAvailable );
			else Platform.Win64.ISteamMatchmaking.AddRequestLobbyListFilterSlotsAvailable( _ptr, nSlotsAvailable );
		}
		
		// void
		public void AddRequestLobbyListNearValueFilter( string pchKeyToMatch /*const char **/, int nValueToBeCloseTo /*int*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) Platform.Win32.ISteamMatchmaking.AddRequestLobbyListNearValueFilter( _ptr, pchKeyToMatch, nValueToBeCloseTo );
			else Platform.Win64.ISteamMatchmaking.AddRequestLobbyListNearValueFilter( _ptr, pchKeyToMatch, nValueToBeCloseTo );
		}
		
		// void
		public void AddRequestLobbyListNumericalFilter( string pchKeyToMatch /*const char **/, int nValueToMatch /*int*/, LobbyComparison eComparisonType /*ELobbyComparison*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) Platform.Win32.ISteamMatchmaking.AddRequestLobbyListNumericalFilter( _ptr, pchKeyToMatch, nValueToMatch, eComparisonType );
			else Platform.Win64.ISteamMatchmaking.AddRequestLobbyListNumericalFilter( _ptr, pchKeyToMatch, nValueToMatch, eComparisonType );
		}
		
		// void
		public void AddRequestLobbyListResultCountFilter( int cMaxResults /*int*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) Platform.Win32.ISteamMatchmaking.AddRequestLobbyListResultCountFilter( _ptr, cMaxResults );
			else Platform.Win64.ISteamMatchmaking.AddRequestLobbyListResultCountFilter( _ptr, cMaxResults );
		}
		
		// void
		public void AddRequestLobbyListStringFilter( string pchKeyToMatch /*const char **/, string pchValueToMatch /*const char **/, LobbyComparison eComparisonType /*ELobbyComparison*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) Platform.Win32.ISteamMatchmaking.AddRequestLobbyListStringFilter( _ptr, pchKeyToMatch, pchValueToMatch, eComparisonType );
			else Platform.Win64.ISteamMatchmaking.AddRequestLobbyListStringFilter( _ptr, pchKeyToMatch, pchValueToMatch, eComparisonType );
		}
		
		// SteamAPICall_t
		public SteamAPICall_t CreateLobby( LobbyType eLobbyType /*ELobbyType*/, int cMaxMembers /*int*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamMatchmaking.CreateLobby( _ptr, eLobbyType, cMaxMembers );
			else return Platform.Win64.ISteamMatchmaking.CreateLobby( _ptr, eLobbyType, cMaxMembers );
		}
		
		// bool
		public bool DeleteLobbyData( CSteamID steamIDLobby /*class CSteamID*/, string pchKey /*const char **/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamMatchmaking.DeleteLobbyData( _ptr, steamIDLobby, pchKey );
			else return Platform.Win64.ISteamMatchmaking.DeleteLobbyData( _ptr, steamIDLobby, pchKey );
		}
		
		// bool
		public bool GetFavoriteGame( int iGame /*int*/, ref AppId_t pnAppID /*AppId_t **/, out uint pnIP /*uint32 **/, out ushort pnConnPort /*uint16 **/, out ushort pnQueryPort /*uint16 **/, IntPtr punFlags /*uint32 **/, out uint pRTime32LastPlayedOnServer /*uint32 **/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamMatchmaking.GetFavoriteGame( _ptr, iGame, ref pnAppID, out pnIP, out pnConnPort, out pnQueryPort, (IntPtr) punFlags, out pRTime32LastPlayedOnServer );
			else return Platform.Win64.ISteamMatchmaking.GetFavoriteGame( _ptr, iGame, ref pnAppID, out pnIP, out pnConnPort, out pnQueryPort, (IntPtr) punFlags, out pRTime32LastPlayedOnServer );
		}
		
		// int
		public int GetFavoriteGameCount()
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamMatchmaking.GetFavoriteGameCount( _ptr );
			else return Platform.Win64.ISteamMatchmaking.GetFavoriteGameCount( _ptr );
		}
		
		// ulong
		public ulong GetLobbyByIndex( int iLobby /*int*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamMatchmaking.GetLobbyByIndex( _ptr, iLobby );
			else return Platform.Win64.ISteamMatchmaking.GetLobbyByIndex( _ptr, iLobby );
		}
		
		// int
		public int GetLobbyChatEntry( CSteamID steamIDLobby /*class CSteamID*/, int iChatID /*int*/, out CSteamID pSteamIDUser /*class CSteamID **/, IntPtr pvData /*void **/, int cubData /*int*/, out ChatEntryType peChatEntryType /*EChatEntryType **/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamMatchmaking.GetLobbyChatEntry( _ptr, steamIDLobby, iChatID, out pSteamIDUser, (IntPtr) pvData, cubData, out peChatEntryType );
			else return Platform.Win64.ISteamMatchmaking.GetLobbyChatEntry( _ptr, steamIDLobby, iChatID, out pSteamIDUser, (IntPtr) pvData, cubData, out peChatEntryType );
		}
		
		// string
		// with: Detect_StringReturn
		public string GetLobbyData( CSteamID steamIDLobby /*class CSteamID*/, string pchKey /*const char **/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			IntPtr string_pointer;
			if ( Platform.IsWindows32 ) string_pointer = Platform.Win32.ISteamMatchmaking.GetLobbyData( _ptr, steamIDLobby, pchKey );
			else string_pointer = Platform.Win64.ISteamMatchmaking.GetLobbyData( _ptr, steamIDLobby, pchKey );
			return Marshal.PtrToStringAnsi( string_pointer );
		}
		
		// bool
		// with: Detect_StringFetch False
		// with: Detect_StringFetch False
		public bool GetLobbyDataByIndex( CSteamID steamIDLobby /*class CSteamID*/, int iLobbyData /*int*/, out string pchKey /*char **/, out string pchValue /*char **/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			bool bSuccess = default( bool );
			pchKey = string.Empty;
			System.Text.StringBuilder pchKey_sb = new System.Text.StringBuilder( 4096 );
			int cchKeyBufferSize = 4096;
			pchValue = string.Empty;
			System.Text.StringBuilder pchValue_sb = new System.Text.StringBuilder( 4096 );
			int cchValueBufferSize = 4096;
			if ( Platform.IsWindows32 ) bSuccess = Platform.Win32.ISteamMatchmaking.GetLobbyDataByIndex( _ptr, steamIDLobby, iLobbyData, pchKey_sb, cchKeyBufferSize, pchValue_sb, cchValueBufferSize );
			else bSuccess = Platform.Win64.ISteamMatchmaking.GetLobbyDataByIndex( _ptr, steamIDLobby, iLobbyData, pchKey_sb, cchKeyBufferSize, pchValue_sb, cchValueBufferSize );
			if ( !bSuccess ) return bSuccess;
			pchValue = pchValue_sb.ToString();
			if ( !bSuccess ) return bSuccess;
			pchKey = pchKey_sb.ToString();
			return bSuccess;
		}
		
		// int
		public int GetLobbyDataCount( CSteamID steamIDLobby /*class CSteamID*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamMatchmaking.GetLobbyDataCount( _ptr, steamIDLobby );
			else return Platform.Win64.ISteamMatchmaking.GetLobbyDataCount( _ptr, steamIDLobby );
		}
		
		// bool
		public bool GetLobbyGameServer( CSteamID steamIDLobby /*class CSteamID*/, out uint punGameServerIP /*uint32 **/, out ushort punGameServerPort /*uint16 **/, out CSteamID psteamIDGameServer /*class CSteamID **/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamMatchmaking.GetLobbyGameServer( _ptr, steamIDLobby, out punGameServerIP, out punGameServerPort, out psteamIDGameServer );
			else return Platform.Win64.ISteamMatchmaking.GetLobbyGameServer( _ptr, steamIDLobby, out punGameServerIP, out punGameServerPort, out psteamIDGameServer );
		}
		
		// ulong
		public ulong GetLobbyMemberByIndex( CSteamID steamIDLobby /*class CSteamID*/, int iMember /*int*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamMatchmaking.GetLobbyMemberByIndex( _ptr, steamIDLobby, iMember );
			else return Platform.Win64.ISteamMatchmaking.GetLobbyMemberByIndex( _ptr, steamIDLobby, iMember );
		}
		
		// string
		// with: Detect_StringReturn
		public string GetLobbyMemberData( CSteamID steamIDLobby /*class CSteamID*/, CSteamID steamIDUser /*class CSteamID*/, string pchKey /*const char **/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			IntPtr string_pointer;
			if ( Platform.IsWindows32 ) string_pointer = Platform.Win32.ISteamMatchmaking.GetLobbyMemberData( _ptr, steamIDLobby, steamIDUser, pchKey );
			else string_pointer = Platform.Win64.ISteamMatchmaking.GetLobbyMemberData( _ptr, steamIDLobby, steamIDUser, pchKey );
			return Marshal.PtrToStringAnsi( string_pointer );
		}
		
		// int
		public int GetLobbyMemberLimit( CSteamID steamIDLobby /*class CSteamID*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamMatchmaking.GetLobbyMemberLimit( _ptr, steamIDLobby );
			else return Platform.Win64.ISteamMatchmaking.GetLobbyMemberLimit( _ptr, steamIDLobby );
		}
		
		// ulong
		public ulong GetLobbyOwner( CSteamID steamIDLobby /*class CSteamID*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamMatchmaking.GetLobbyOwner( _ptr, steamIDLobby );
			else return Platform.Win64.ISteamMatchmaking.GetLobbyOwner( _ptr, steamIDLobby );
		}
		
		// int
		public int GetNumLobbyMembers( CSteamID steamIDLobby /*class CSteamID*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamMatchmaking.GetNumLobbyMembers( _ptr, steamIDLobby );
			else return Platform.Win64.ISteamMatchmaking.GetNumLobbyMembers( _ptr, steamIDLobby );
		}
		
		// bool
		public bool InviteUserToLobby( CSteamID steamIDLobby /*class CSteamID*/, CSteamID steamIDInvitee /*class CSteamID*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamMatchmaking.InviteUserToLobby( _ptr, steamIDLobby, steamIDInvitee );
			else return Platform.Win64.ISteamMatchmaking.InviteUserToLobby( _ptr, steamIDLobby, steamIDInvitee );
		}
		
		// SteamAPICall_t
		public SteamAPICall_t JoinLobby( CSteamID steamIDLobby /*class CSteamID*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamMatchmaking.JoinLobby( _ptr, steamIDLobby );
			else return Platform.Win64.ISteamMatchmaking.JoinLobby( _ptr, steamIDLobby );
		}
		
		// void
		public void LeaveLobby( CSteamID steamIDLobby /*class CSteamID*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) Platform.Win32.ISteamMatchmaking.LeaveLobby( _ptr, steamIDLobby );
			else Platform.Win64.ISteamMatchmaking.LeaveLobby( _ptr, steamIDLobby );
		}
		
		// bool
		public bool RemoveFavoriteGame( AppId_t nAppID /*AppId_t*/, uint nIP /*uint32*/, ushort nConnPort /*uint16*/, ushort nQueryPort /*uint16*/, uint unFlags /*uint32*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamMatchmaking.RemoveFavoriteGame( _ptr, nAppID, nIP, nConnPort, nQueryPort, unFlags );
			else return Platform.Win64.ISteamMatchmaking.RemoveFavoriteGame( _ptr, nAppID, nIP, nConnPort, nQueryPort, unFlags );
		}
		
		// bool
		public bool RequestLobbyData( CSteamID steamIDLobby /*class CSteamID*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamMatchmaking.RequestLobbyData( _ptr, steamIDLobby );
			else return Platform.Win64.ISteamMatchmaking.RequestLobbyData( _ptr, steamIDLobby );
		}
		
		// SteamAPICall_t
		public SteamAPICall_t RequestLobbyList()
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamMatchmaking.RequestLobbyList( _ptr );
			else return Platform.Win64.ISteamMatchmaking.RequestLobbyList( _ptr );
		}
		
		// bool
		public bool SendLobbyChatMsg( CSteamID steamIDLobby /*class CSteamID*/, IntPtr pvMsgBody /*const void **/, int cubMsgBody /*int*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamMatchmaking.SendLobbyChatMsg( _ptr, steamIDLobby, (IntPtr) pvMsgBody, cubMsgBody );
			else return Platform.Win64.ISteamMatchmaking.SendLobbyChatMsg( _ptr, steamIDLobby, (IntPtr) pvMsgBody, cubMsgBody );
		}
		
		// bool
		public bool SetLinkedLobby( CSteamID steamIDLobby /*class CSteamID*/, CSteamID steamIDLobbyDependent /*class CSteamID*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamMatchmaking.SetLinkedLobby( _ptr, steamIDLobby, steamIDLobbyDependent );
			else return Platform.Win64.ISteamMatchmaking.SetLinkedLobby( _ptr, steamIDLobby, steamIDLobbyDependent );
		}
		
		// bool
		public bool SetLobbyData( CSteamID steamIDLobby /*class CSteamID*/, string pchKey /*const char **/, string pchValue /*const char **/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamMatchmaking.SetLobbyData( _ptr, steamIDLobby, pchKey, pchValue );
			else return Platform.Win64.ISteamMatchmaking.SetLobbyData( _ptr, steamIDLobby, pchKey, pchValue );
		}
		
		// void
		public void SetLobbyGameServer( CSteamID steamIDLobby /*class CSteamID*/, uint unGameServerIP /*uint32*/, ushort unGameServerPort /*uint16*/, CSteamID steamIDGameServer /*class CSteamID*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) Platform.Win32.ISteamMatchmaking.SetLobbyGameServer( _ptr, steamIDLobby, unGameServerIP, unGameServerPort, steamIDGameServer );
			else Platform.Win64.ISteamMatchmaking.SetLobbyGameServer( _ptr, steamIDLobby, unGameServerIP, unGameServerPort, steamIDGameServer );
		}
		
		// bool
		public bool SetLobbyJoinable( CSteamID steamIDLobby /*class CSteamID*/, bool bLobbyJoinable /*bool*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamMatchmaking.SetLobbyJoinable( _ptr, steamIDLobby, bLobbyJoinable );
			else return Platform.Win64.ISteamMatchmaking.SetLobbyJoinable( _ptr, steamIDLobby, bLobbyJoinable );
		}
		
		// void
		public void SetLobbyMemberData( CSteamID steamIDLobby /*class CSteamID*/, string pchKey /*const char **/, string pchValue /*const char **/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) Platform.Win32.ISteamMatchmaking.SetLobbyMemberData( _ptr, steamIDLobby, pchKey, pchValue );
			else Platform.Win64.ISteamMatchmaking.SetLobbyMemberData( _ptr, steamIDLobby, pchKey, pchValue );
		}
		
		// bool
		public bool SetLobbyMemberLimit( CSteamID steamIDLobby /*class CSteamID*/, int cMaxMembers /*int*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamMatchmaking.SetLobbyMemberLimit( _ptr, steamIDLobby, cMaxMembers );
			else return Platform.Win64.ISteamMatchmaking.SetLobbyMemberLimit( _ptr, steamIDLobby, cMaxMembers );
		}
		
		// bool
		public bool SetLobbyOwner( CSteamID steamIDLobby /*class CSteamID*/, CSteamID steamIDNewOwner /*class CSteamID*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamMatchmaking.SetLobbyOwner( _ptr, steamIDLobby, steamIDNewOwner );
			else return Platform.Win64.ISteamMatchmaking.SetLobbyOwner( _ptr, steamIDLobby, steamIDNewOwner );
		}
		
		// bool
		public bool SetLobbyType( CSteamID steamIDLobby /*class CSteamID*/, LobbyType eLobbyType /*ELobbyType*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamMatchmaking.SetLobbyType( _ptr, steamIDLobby, eLobbyType );
			else return Platform.Win64.ISteamMatchmaking.SetLobbyType( _ptr, steamIDLobby, eLobbyType );
		}
		
	}
}
