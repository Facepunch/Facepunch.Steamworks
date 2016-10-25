using System;
using System.Runtime.InteropServices;

namespace SteamNative
{
	public unsafe class SteamFriends
	{
		internal IntPtr _ptr;
		
		public SteamFriends( IntPtr pointer )
		{
			_ptr = pointer;
		}
		
		
		// void
		public void ActivateGameOverlay( string pchDialog /*const char **/ )
		{
			if ( Platform.IsWindows32 ) Platform.Win32.ISteamFriends.ActivateGameOverlay( _ptr, pchDialog );
			else Platform.Win64.ISteamFriends.ActivateGameOverlay( _ptr, pchDialog );
		}
		
		// void
		public void ActivateGameOverlayInviteDialog( CSteamID steamIDLobby /*class CSteamID*/ )
		{
			if ( Platform.IsWindows32 ) Platform.Win32.ISteamFriends.ActivateGameOverlayInviteDialog( _ptr, steamIDLobby );
			else Platform.Win64.ISteamFriends.ActivateGameOverlayInviteDialog( _ptr, steamIDLobby );
		}
		
		// void
		public void ActivateGameOverlayToStore( AppId_t nAppID /*AppId_t*/, OverlayToStoreFlag eFlag /*EOverlayToStoreFlag*/ )
		{
			if ( Platform.IsWindows32 ) Platform.Win32.ISteamFriends.ActivateGameOverlayToStore( _ptr, nAppID, eFlag );
			else Platform.Win64.ISteamFriends.ActivateGameOverlayToStore( _ptr, nAppID, eFlag );
		}
		
		// void
		public void ActivateGameOverlayToUser( string pchDialog /*const char **/, CSteamID steamID /*class CSteamID*/ )
		{
			if ( Platform.IsWindows32 ) Platform.Win32.ISteamFriends.ActivateGameOverlayToUser( _ptr, pchDialog, steamID );
			else Platform.Win64.ISteamFriends.ActivateGameOverlayToUser( _ptr, pchDialog, steamID );
		}
		
		// void
		public void ActivateGameOverlayToWebPage( string pchURL /*const char **/ )
		{
			if ( Platform.IsWindows32 ) Platform.Win32.ISteamFriends.ActivateGameOverlayToWebPage( _ptr, pchURL );
			else Platform.Win64.ISteamFriends.ActivateGameOverlayToWebPage( _ptr, pchURL );
		}
		
		// void
		public void ClearRichPresence()
		{
			if ( Platform.IsWindows32 ) Platform.Win32.ISteamFriends.ClearRichPresence( _ptr );
			else Platform.Win64.ISteamFriends.ClearRichPresence( _ptr );
		}
		
		// bool
		public bool CloseClanChatWindowInSteam( CSteamID steamIDClanChat /*class CSteamID*/ )
		{
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamFriends.CloseClanChatWindowInSteam( _ptr, steamIDClanChat );
			else return Platform.Win64.ISteamFriends.CloseClanChatWindowInSteam( _ptr, steamIDClanChat );
		}
		
		// SteamAPICall_t
		public SteamAPICall_t DownloadClanActivityCounts( IntPtr psteamIDClans /*class CSteamID **/, int cClansToRequest /*int*/ )
		{
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamFriends.DownloadClanActivityCounts( _ptr, (IntPtr) psteamIDClans, cClansToRequest );
			else return Platform.Win64.ISteamFriends.DownloadClanActivityCounts( _ptr, (IntPtr) psteamIDClans, cClansToRequest );
		}
		
		// SteamAPICall_t
		public SteamAPICall_t EnumerateFollowingList( uint unStartIndex /*uint32*/ )
		{
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamFriends.EnumerateFollowingList( _ptr, unStartIndex );
			else return Platform.Win64.ISteamFriends.EnumerateFollowingList( _ptr, unStartIndex );
		}
		
		// ulong
		public ulong GetChatMemberByIndex( CSteamID steamIDClan /*class CSteamID*/, int iUser /*int*/ )
		{
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamFriends.GetChatMemberByIndex( _ptr, steamIDClan, iUser );
			else return Platform.Win64.ISteamFriends.GetChatMemberByIndex( _ptr, steamIDClan, iUser );
		}
		
		// bool
		public bool GetClanActivityCounts( CSteamID steamIDClan /*class CSteamID*/, out int pnOnline /*int **/, out int pnInGame /*int **/, out int pnChatting /*int **/ )
		{
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamFriends.GetClanActivityCounts( _ptr, steamIDClan, out pnOnline, out pnInGame, out pnChatting );
			else return Platform.Win64.ISteamFriends.GetClanActivityCounts( _ptr, steamIDClan, out pnOnline, out pnInGame, out pnChatting );
		}
		
		// ulong
		public ulong GetClanByIndex( int iClan /*int*/ )
		{
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamFriends.GetClanByIndex( _ptr, iClan );
			else return Platform.Win64.ISteamFriends.GetClanByIndex( _ptr, iClan );
		}
		
		// int
		public int GetClanChatMemberCount( CSteamID steamIDClan /*class CSteamID*/ )
		{
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamFriends.GetClanChatMemberCount( _ptr, steamIDClan );
			else return Platform.Win64.ISteamFriends.GetClanChatMemberCount( _ptr, steamIDClan );
		}
		
		// int
		public int GetClanChatMessage( CSteamID steamIDClanChat /*class CSteamID*/, int iMessage /*int*/, IntPtr prgchText /*void **/, int cchTextMax /*int*/, out ChatEntryType peChatEntryType /*EChatEntryType **/, out CSteamID psteamidChatter /*class CSteamID **/ )
		{
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamFriends.GetClanChatMessage( _ptr, steamIDClanChat, iMessage, (IntPtr) prgchText, cchTextMax, out peChatEntryType, out psteamidChatter );
			else return Platform.Win64.ISteamFriends.GetClanChatMessage( _ptr, steamIDClanChat, iMessage, (IntPtr) prgchText, cchTextMax, out peChatEntryType, out psteamidChatter );
		}
		
		// int
		public int GetClanCount()
		{
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamFriends.GetClanCount( _ptr );
			else return Platform.Win64.ISteamFriends.GetClanCount( _ptr );
		}
		
		// string
		// with: Detect_StringReturn
		public string GetClanName( CSteamID steamIDClan /*class CSteamID*/ )
		{
			IntPtr string_pointer;
			if ( Platform.IsWindows32 ) string_pointer = Platform.Win32.ISteamFriends.GetClanName( _ptr, steamIDClan );
			else string_pointer = Platform.Win64.ISteamFriends.GetClanName( _ptr, steamIDClan );
			return Marshal.PtrToStringAnsi( string_pointer );
		}
		
		// ulong
		public ulong GetClanOfficerByIndex( CSteamID steamIDClan /*class CSteamID*/, int iOfficer /*int*/ )
		{
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamFriends.GetClanOfficerByIndex( _ptr, steamIDClan, iOfficer );
			else return Platform.Win64.ISteamFriends.GetClanOfficerByIndex( _ptr, steamIDClan, iOfficer );
		}
		
		// int
		public int GetClanOfficerCount( CSteamID steamIDClan /*class CSteamID*/ )
		{
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamFriends.GetClanOfficerCount( _ptr, steamIDClan );
			else return Platform.Win64.ISteamFriends.GetClanOfficerCount( _ptr, steamIDClan );
		}
		
		// ulong
		public ulong GetClanOwner( CSteamID steamIDClan /*class CSteamID*/ )
		{
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamFriends.GetClanOwner( _ptr, steamIDClan );
			else return Platform.Win64.ISteamFriends.GetClanOwner( _ptr, steamIDClan );
		}
		
		// string
		// with: Detect_StringReturn
		public string GetClanTag( CSteamID steamIDClan /*class CSteamID*/ )
		{
			IntPtr string_pointer;
			if ( Platform.IsWindows32 ) string_pointer = Platform.Win32.ISteamFriends.GetClanTag( _ptr, steamIDClan );
			else string_pointer = Platform.Win64.ISteamFriends.GetClanTag( _ptr, steamIDClan );
			return Marshal.PtrToStringAnsi( string_pointer );
		}
		
		// ulong
		public ulong GetCoplayFriend( int iCoplayFriend /*int*/ )
		{
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamFriends.GetCoplayFriend( _ptr, iCoplayFriend );
			else return Platform.Win64.ISteamFriends.GetCoplayFriend( _ptr, iCoplayFriend );
		}
		
		// int
		public int GetCoplayFriendCount()
		{
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamFriends.GetCoplayFriendCount( _ptr );
			else return Platform.Win64.ISteamFriends.GetCoplayFriendCount( _ptr );
		}
		
		// SteamAPICall_t
		public SteamAPICall_t GetFollowerCount( CSteamID steamID /*class CSteamID*/ )
		{
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamFriends.GetFollowerCount( _ptr, steamID );
			else return Platform.Win64.ISteamFriends.GetFollowerCount( _ptr, steamID );
		}
		
		// ulong
		public ulong GetFriendByIndex( int iFriend /*int*/, int iFriendFlags /*int*/ )
		{
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamFriends.GetFriendByIndex( _ptr, iFriend, iFriendFlags );
			else return Platform.Win64.ISteamFriends.GetFriendByIndex( _ptr, iFriend, iFriendFlags );
		}
		
		// AppId_t
		public AppId_t GetFriendCoplayGame( CSteamID steamIDFriend /*class CSteamID*/ )
		{
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamFriends.GetFriendCoplayGame( _ptr, steamIDFriend );
			else return Platform.Win64.ISteamFriends.GetFriendCoplayGame( _ptr, steamIDFriend );
		}
		
		// int
		public int GetFriendCoplayTime( CSteamID steamIDFriend /*class CSteamID*/ )
		{
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamFriends.GetFriendCoplayTime( _ptr, steamIDFriend );
			else return Platform.Win64.ISteamFriends.GetFriendCoplayTime( _ptr, steamIDFriend );
		}
		
		// int
		public int GetFriendCount( int iFriendFlags /*int*/ )
		{
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamFriends.GetFriendCount( _ptr, iFriendFlags );
			else return Platform.Win64.ISteamFriends.GetFriendCount( _ptr, iFriendFlags );
		}
		
		// int
		public int GetFriendCountFromSource( CSteamID steamIDSource /*class CSteamID*/ )
		{
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamFriends.GetFriendCountFromSource( _ptr, steamIDSource );
			else return Platform.Win64.ISteamFriends.GetFriendCountFromSource( _ptr, steamIDSource );
		}
		
		// ulong
		public ulong GetFriendFromSourceByIndex( CSteamID steamIDSource /*class CSteamID*/, int iFriend /*int*/ )
		{
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamFriends.GetFriendFromSourceByIndex( _ptr, steamIDSource, iFriend );
			else return Platform.Win64.ISteamFriends.GetFriendFromSourceByIndex( _ptr, steamIDSource, iFriend );
		}
		
		// bool
		public bool GetFriendGamePlayed( CSteamID steamIDFriend /*class CSteamID*/, ref FriendGameInfo_t pFriendGameInfo /*struct FriendGameInfo_t **/ )
		{
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamFriends.GetFriendGamePlayed( _ptr, steamIDFriend, ref pFriendGameInfo );
			else return Platform.Win64.ISteamFriends.GetFriendGamePlayed( _ptr, steamIDFriend, ref pFriendGameInfo );
		}
		
		// int
		public int GetFriendMessage( CSteamID steamIDFriend /*class CSteamID*/, int iMessageID /*int*/, IntPtr pvData /*void **/, int cubData /*int*/, out ChatEntryType peChatEntryType /*EChatEntryType **/ )
		{
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamFriends.GetFriendMessage( _ptr, steamIDFriend, iMessageID, (IntPtr) pvData, cubData, out peChatEntryType );
			else return Platform.Win64.ISteamFriends.GetFriendMessage( _ptr, steamIDFriend, iMessageID, (IntPtr) pvData, cubData, out peChatEntryType );
		}
		
		// string
		// with: Detect_StringReturn
		public string GetFriendPersonaName( CSteamID steamIDFriend /*class CSteamID*/ )
		{
			IntPtr string_pointer;
			if ( Platform.IsWindows32 ) string_pointer = Platform.Win32.ISteamFriends.GetFriendPersonaName( _ptr, steamIDFriend );
			else string_pointer = Platform.Win64.ISteamFriends.GetFriendPersonaName( _ptr, steamIDFriend );
			return Marshal.PtrToStringAnsi( string_pointer );
		}
		
		// string
		// with: Detect_StringReturn
		public string GetFriendPersonaNameHistory( CSteamID steamIDFriend /*class CSteamID*/, int iPersonaName /*int*/ )
		{
			IntPtr string_pointer;
			if ( Platform.IsWindows32 ) string_pointer = Platform.Win32.ISteamFriends.GetFriendPersonaNameHistory( _ptr, steamIDFriend, iPersonaName );
			else string_pointer = Platform.Win64.ISteamFriends.GetFriendPersonaNameHistory( _ptr, steamIDFriend, iPersonaName );
			return Marshal.PtrToStringAnsi( string_pointer );
		}
		
		// PersonaState
		public PersonaState GetFriendPersonaState( CSteamID steamIDFriend /*class CSteamID*/ )
		{
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamFriends.GetFriendPersonaState( _ptr, steamIDFriend );
			else return Platform.Win64.ISteamFriends.GetFriendPersonaState( _ptr, steamIDFriend );
		}
		
		// FriendRelationship
		public FriendRelationship GetFriendRelationship( CSteamID steamIDFriend /*class CSteamID*/ )
		{
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamFriends.GetFriendRelationship( _ptr, steamIDFriend );
			else return Platform.Win64.ISteamFriends.GetFriendRelationship( _ptr, steamIDFriend );
		}
		
		// string
		// with: Detect_StringReturn
		public string GetFriendRichPresence( CSteamID steamIDFriend /*class CSteamID*/, string pchKey /*const char **/ )
		{
			IntPtr string_pointer;
			if ( Platform.IsWindows32 ) string_pointer = Platform.Win32.ISteamFriends.GetFriendRichPresence( _ptr, steamIDFriend, pchKey );
			else string_pointer = Platform.Win64.ISteamFriends.GetFriendRichPresence( _ptr, steamIDFriend, pchKey );
			return Marshal.PtrToStringAnsi( string_pointer );
		}
		
		// string
		// with: Detect_StringReturn
		public string GetFriendRichPresenceKeyByIndex( CSteamID steamIDFriend /*class CSteamID*/, int iKey /*int*/ )
		{
			IntPtr string_pointer;
			if ( Platform.IsWindows32 ) string_pointer = Platform.Win32.ISteamFriends.GetFriendRichPresenceKeyByIndex( _ptr, steamIDFriend, iKey );
			else string_pointer = Platform.Win64.ISteamFriends.GetFriendRichPresenceKeyByIndex( _ptr, steamIDFriend, iKey );
			return Marshal.PtrToStringAnsi( string_pointer );
		}
		
		// int
		public int GetFriendRichPresenceKeyCount( CSteamID steamIDFriend /*class CSteamID*/ )
		{
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamFriends.GetFriendRichPresenceKeyCount( _ptr, steamIDFriend );
			else return Platform.Win64.ISteamFriends.GetFriendRichPresenceKeyCount( _ptr, steamIDFriend );
		}
		
		// int
		public int GetFriendsGroupCount()
		{
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamFriends.GetFriendsGroupCount( _ptr );
			else return Platform.Win64.ISteamFriends.GetFriendsGroupCount( _ptr );
		}
		
		// FriendsGroupID_t
		public FriendsGroupID_t GetFriendsGroupIDByIndex( int iFG /*int*/ )
		{
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamFriends.GetFriendsGroupIDByIndex( _ptr, iFG );
			else return Platform.Win64.ISteamFriends.GetFriendsGroupIDByIndex( _ptr, iFG );
		}
		
		// int
		public int GetFriendsGroupMembersCount( FriendsGroupID_t friendsGroupID /*FriendsGroupID_t*/ )
		{
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamFriends.GetFriendsGroupMembersCount( _ptr, friendsGroupID );
			else return Platform.Win64.ISteamFriends.GetFriendsGroupMembersCount( _ptr, friendsGroupID );
		}
		
		// void
		public void GetFriendsGroupMembersList( FriendsGroupID_t friendsGroupID /*FriendsGroupID_t*/, IntPtr pOutSteamIDMembers /*class CSteamID **/, int nMembersCount /*int*/ )
		{
			if ( Platform.IsWindows32 ) Platform.Win32.ISteamFriends.GetFriendsGroupMembersList( _ptr, friendsGroupID, (IntPtr) pOutSteamIDMembers, nMembersCount );
			else Platform.Win64.ISteamFriends.GetFriendsGroupMembersList( _ptr, friendsGroupID, (IntPtr) pOutSteamIDMembers, nMembersCount );
		}
		
		// string
		// with: Detect_StringReturn
		public string GetFriendsGroupName( FriendsGroupID_t friendsGroupID /*FriendsGroupID_t*/ )
		{
			IntPtr string_pointer;
			if ( Platform.IsWindows32 ) string_pointer = Platform.Win32.ISteamFriends.GetFriendsGroupName( _ptr, friendsGroupID );
			else string_pointer = Platform.Win64.ISteamFriends.GetFriendsGroupName( _ptr, friendsGroupID );
			return Marshal.PtrToStringAnsi( string_pointer );
		}
		
		// int
		public int GetFriendSteamLevel( CSteamID steamIDFriend /*class CSteamID*/ )
		{
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamFriends.GetFriendSteamLevel( _ptr, steamIDFriend );
			else return Platform.Win64.ISteamFriends.GetFriendSteamLevel( _ptr, steamIDFriend );
		}
		
		// int
		public int GetLargeFriendAvatar( CSteamID steamIDFriend /*class CSteamID*/ )
		{
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamFriends.GetLargeFriendAvatar( _ptr, steamIDFriend );
			else return Platform.Win64.ISteamFriends.GetLargeFriendAvatar( _ptr, steamIDFriend );
		}
		
		// int
		public int GetMediumFriendAvatar( CSteamID steamIDFriend /*class CSteamID*/ )
		{
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamFriends.GetMediumFriendAvatar( _ptr, steamIDFriend );
			else return Platform.Win64.ISteamFriends.GetMediumFriendAvatar( _ptr, steamIDFriend );
		}
		
		// string
		// with: Detect_StringReturn
		public string GetPersonaName()
		{
			IntPtr string_pointer;
			if ( Platform.IsWindows32 ) string_pointer = Platform.Win32.ISteamFriends.GetPersonaName( _ptr );
			else string_pointer = Platform.Win64.ISteamFriends.GetPersonaName( _ptr );
			return Marshal.PtrToStringAnsi( string_pointer );
		}
		
		// PersonaState
		public PersonaState GetPersonaState()
		{
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamFriends.GetPersonaState( _ptr );
			else return Platform.Win64.ISteamFriends.GetPersonaState( _ptr );
		}
		
		// string
		// with: Detect_StringReturn
		public string GetPlayerNickname( CSteamID steamIDPlayer /*class CSteamID*/ )
		{
			IntPtr string_pointer;
			if ( Platform.IsWindows32 ) string_pointer = Platform.Win32.ISteamFriends.GetPlayerNickname( _ptr, steamIDPlayer );
			else string_pointer = Platform.Win64.ISteamFriends.GetPlayerNickname( _ptr, steamIDPlayer );
			return Marshal.PtrToStringAnsi( string_pointer );
		}
		
		// int
		public int GetSmallFriendAvatar( CSteamID steamIDFriend /*class CSteamID*/ )
		{
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamFriends.GetSmallFriendAvatar( _ptr, steamIDFriend );
			else return Platform.Win64.ISteamFriends.GetSmallFriendAvatar( _ptr, steamIDFriend );
		}
		
		// uint
		public uint GetUserRestrictions()
		{
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamFriends.GetUserRestrictions( _ptr );
			else return Platform.Win64.ISteamFriends.GetUserRestrictions( _ptr );
		}
		
		// bool
		public bool HasFriend( CSteamID steamIDFriend /*class CSteamID*/, int iFriendFlags /*int*/ )
		{
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamFriends.HasFriend( _ptr, steamIDFriend, iFriendFlags );
			else return Platform.Win64.ISteamFriends.HasFriend( _ptr, steamIDFriend, iFriendFlags );
		}
		
		// bool
		public bool InviteUserToGame( CSteamID steamIDFriend /*class CSteamID*/, string pchConnectString /*const char **/ )
		{
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamFriends.InviteUserToGame( _ptr, steamIDFriend, pchConnectString );
			else return Platform.Win64.ISteamFriends.InviteUserToGame( _ptr, steamIDFriend, pchConnectString );
		}
		
		// bool
		public bool IsClanChatAdmin( CSteamID steamIDClanChat /*class CSteamID*/, CSteamID steamIDUser /*class CSteamID*/ )
		{
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamFriends.IsClanChatAdmin( _ptr, steamIDClanChat, steamIDUser );
			else return Platform.Win64.ISteamFriends.IsClanChatAdmin( _ptr, steamIDClanChat, steamIDUser );
		}
		
		// bool
		public bool IsClanChatWindowOpenInSteam( CSteamID steamIDClanChat /*class CSteamID*/ )
		{
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamFriends.IsClanChatWindowOpenInSteam( _ptr, steamIDClanChat );
			else return Platform.Win64.ISteamFriends.IsClanChatWindowOpenInSteam( _ptr, steamIDClanChat );
		}
		
		// SteamAPICall_t
		public SteamAPICall_t IsFollowing( CSteamID steamID /*class CSteamID*/ )
		{
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamFriends.IsFollowing( _ptr, steamID );
			else return Platform.Win64.ISteamFriends.IsFollowing( _ptr, steamID );
		}
		
		// bool
		public bool IsUserInSource( CSteamID steamIDUser /*class CSteamID*/, CSteamID steamIDSource /*class CSteamID*/ )
		{
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamFriends.IsUserInSource( _ptr, steamIDUser, steamIDSource );
			else return Platform.Win64.ISteamFriends.IsUserInSource( _ptr, steamIDUser, steamIDSource );
		}
		
		// SteamAPICall_t
		public SteamAPICall_t JoinClanChatRoom( CSteamID steamIDClan /*class CSteamID*/ )
		{
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamFriends.JoinClanChatRoom( _ptr, steamIDClan );
			else return Platform.Win64.ISteamFriends.JoinClanChatRoom( _ptr, steamIDClan );
		}
		
		// bool
		public bool LeaveClanChatRoom( CSteamID steamIDClan /*class CSteamID*/ )
		{
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamFriends.LeaveClanChatRoom( _ptr, steamIDClan );
			else return Platform.Win64.ISteamFriends.LeaveClanChatRoom( _ptr, steamIDClan );
		}
		
		// bool
		public bool OpenClanChatWindowInSteam( CSteamID steamIDClanChat /*class CSteamID*/ )
		{
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamFriends.OpenClanChatWindowInSteam( _ptr, steamIDClanChat );
			else return Platform.Win64.ISteamFriends.OpenClanChatWindowInSteam( _ptr, steamIDClanChat );
		}
		
		// bool
		public bool ReplyToFriendMessage( CSteamID steamIDFriend /*class CSteamID*/, string pchMsgToSend /*const char **/ )
		{
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamFriends.ReplyToFriendMessage( _ptr, steamIDFriend, pchMsgToSend );
			else return Platform.Win64.ISteamFriends.ReplyToFriendMessage( _ptr, steamIDFriend, pchMsgToSend );
		}
		
		// SteamAPICall_t
		public SteamAPICall_t RequestClanOfficerList( CSteamID steamIDClan /*class CSteamID*/ )
		{
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamFriends.RequestClanOfficerList( _ptr, steamIDClan );
			else return Platform.Win64.ISteamFriends.RequestClanOfficerList( _ptr, steamIDClan );
		}
		
		// void
		public void RequestFriendRichPresence( CSteamID steamIDFriend /*class CSteamID*/ )
		{
			if ( Platform.IsWindows32 ) Platform.Win32.ISteamFriends.RequestFriendRichPresence( _ptr, steamIDFriend );
			else Platform.Win64.ISteamFriends.RequestFriendRichPresence( _ptr, steamIDFriend );
		}
		
		// bool
		public bool RequestUserInformation( CSteamID steamIDUser /*class CSteamID*/, bool bRequireNameOnly /*bool*/ )
		{
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamFriends.RequestUserInformation( _ptr, steamIDUser, bRequireNameOnly );
			else return Platform.Win64.ISteamFriends.RequestUserInformation( _ptr, steamIDUser, bRequireNameOnly );
		}
		
		// bool
		public bool SendClanChatMessage( CSteamID steamIDClanChat /*class CSteamID*/, string pchText /*const char **/ )
		{
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamFriends.SendClanChatMessage( _ptr, steamIDClanChat, pchText );
			else return Platform.Win64.ISteamFriends.SendClanChatMessage( _ptr, steamIDClanChat, pchText );
		}
		
		// void
		public void SetInGameVoiceSpeaking( CSteamID steamIDUser /*class CSteamID*/, bool bSpeaking /*bool*/ )
		{
			if ( Platform.IsWindows32 ) Platform.Win32.ISteamFriends.SetInGameVoiceSpeaking( _ptr, steamIDUser, bSpeaking );
			else Platform.Win64.ISteamFriends.SetInGameVoiceSpeaking( _ptr, steamIDUser, bSpeaking );
		}
		
		// bool
		public bool SetListenForFriendsMessages( bool bInterceptEnabled /*bool*/ )
		{
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamFriends.SetListenForFriendsMessages( _ptr, bInterceptEnabled );
			else return Platform.Win64.ISteamFriends.SetListenForFriendsMessages( _ptr, bInterceptEnabled );
		}
		
		// SteamAPICall_t
		public SteamAPICall_t SetPersonaName( string pchPersonaName /*const char **/ )
		{
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamFriends.SetPersonaName( _ptr, pchPersonaName );
			else return Platform.Win64.ISteamFriends.SetPersonaName( _ptr, pchPersonaName );
		}
		
		// void
		public void SetPlayedWith( CSteamID steamIDUserPlayedWith /*class CSteamID*/ )
		{
			if ( Platform.IsWindows32 ) Platform.Win32.ISteamFriends.SetPlayedWith( _ptr, steamIDUserPlayedWith );
			else Platform.Win64.ISteamFriends.SetPlayedWith( _ptr, steamIDUserPlayedWith );
		}
		
		// bool
		public bool SetRichPresence( string pchKey /*const char **/, string pchValue /*const char **/ )
		{
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamFriends.SetRichPresence( _ptr, pchKey, pchValue );
			else return Platform.Win64.ISteamFriends.SetRichPresence( _ptr, pchKey, pchValue );
		}
		
	}
}
