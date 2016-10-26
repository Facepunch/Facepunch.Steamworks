using System;
using System.Runtime.InteropServices;

namespace SteamNative
{
	public unsafe class SteamFriends : IDisposable
	{
		//
		// Holds a platform specific implentation
		//
		internal Platform.Interface _pi;
		
		//
		// Constructor decides which implementation to use based on current platform
		//
		public SteamFriends( IntPtr pointer )
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
		public void ActivateGameOverlay( string pchDialog /*const char **/ )
		{
			_pi.ISteamFriends_ActivateGameOverlay( pchDialog /*C*/ );
		}
		
		// void
		public void ActivateGameOverlayInviteDialog( CSteamID steamIDLobby /*class CSteamID*/ )
		{
			_pi.ISteamFriends_ActivateGameOverlayInviteDialog( steamIDLobby.Value /*C*/ );
		}
		
		// void
		public void ActivateGameOverlayToStore( AppId_t nAppID /*AppId_t*/, OverlayToStoreFlag eFlag /*EOverlayToStoreFlag*/ )
		{
			_pi.ISteamFriends_ActivateGameOverlayToStore( nAppID.Value /*C*/, eFlag /*C*/ );
		}
		
		// void
		public void ActivateGameOverlayToUser( string pchDialog /*const char **/, CSteamID steamID /*class CSteamID*/ )
		{
			_pi.ISteamFriends_ActivateGameOverlayToUser( pchDialog /*C*/, steamID.Value /*C*/ );
		}
		
		// void
		public void ActivateGameOverlayToWebPage( string pchURL /*const char **/ )
		{
			_pi.ISteamFriends_ActivateGameOverlayToWebPage( pchURL /*C*/ );
		}
		
		// void
		public void ClearRichPresence()
		{
			_pi.ISteamFriends_ClearRichPresence();
		}
		
		// bool
		public bool CloseClanChatWindowInSteam( CSteamID steamIDClanChat /*class CSteamID*/ )
		{
			return _pi.ISteamFriends_CloseClanChatWindowInSteam( steamIDClanChat.Value /*C*/ );
		}
		
		// SteamAPICall_t
		public SteamAPICall_t DownloadClanActivityCounts( IntPtr psteamIDClans /*class CSteamID **/, int cClansToRequest /*int*/ )
		{
			return _pi.ISteamFriends_DownloadClanActivityCounts( (IntPtr) psteamIDClans, cClansToRequest /*C*/ );
		}
		
		// SteamAPICall_t
		public SteamAPICall_t EnumerateFollowingList( uint unStartIndex /*uint32*/ )
		{
			return _pi.ISteamFriends_EnumerateFollowingList( unStartIndex /*C*/ );
		}
		
		// ulong
		public ulong GetChatMemberByIndex( CSteamID steamIDClan /*class CSteamID*/, int iUser /*int*/ )
		{
			return _pi.ISteamFriends_GetChatMemberByIndex( steamIDClan.Value /*C*/, iUser /*C*/ );
		}
		
		// bool
		public bool GetClanActivityCounts( CSteamID steamIDClan /*class CSteamID*/, out int pnOnline /*int **/, out int pnInGame /*int **/, out int pnChatting /*int **/ )
		{
			return _pi.ISteamFriends_GetClanActivityCounts( steamIDClan.Value /*C*/, out pnOnline /*B*/, out pnInGame /*B*/, out pnChatting /*B*/ );
		}
		
		// ulong
		public ulong GetClanByIndex( int iClan /*int*/ )
		{
			return _pi.ISteamFriends_GetClanByIndex( iClan /*C*/ );
		}
		
		// int
		public int GetClanChatMemberCount( CSteamID steamIDClan /*class CSteamID*/ )
		{
			return _pi.ISteamFriends_GetClanChatMemberCount( steamIDClan.Value /*C*/ );
		}
		
		// int
		public int GetClanChatMessage( CSteamID steamIDClanChat /*class CSteamID*/, int iMessage /*int*/, IntPtr prgchText /*void **/, int cchTextMax /*int*/, out ChatEntryType peChatEntryType /*EChatEntryType **/, out CSteamID psteamidChatter /*class CSteamID **/ )
		{
			return _pi.ISteamFriends_GetClanChatMessage( steamIDClanChat.Value /*C*/, iMessage /*C*/, (IntPtr) prgchText /*C*/, cchTextMax /*C*/, out peChatEntryType /*B*/, out psteamidChatter.Value /*B*/ );
		}
		
		// int
		public int GetClanCount()
		{
			return _pi.ISteamFriends_GetClanCount();
		}
		
		// string
		// with: Detect_StringReturn
		public string GetClanName( CSteamID steamIDClan /*class CSteamID*/ )
		{
			IntPtr string_pointer;
			string_pointer = _pi.ISteamFriends_GetClanName( steamIDClan.Value /*C*/ );
			return Marshal.PtrToStringAnsi( string_pointer );
		}
		
		// ulong
		public ulong GetClanOfficerByIndex( CSteamID steamIDClan /*class CSteamID*/, int iOfficer /*int*/ )
		{
			return _pi.ISteamFriends_GetClanOfficerByIndex( steamIDClan.Value /*C*/, iOfficer /*C*/ );
		}
		
		// int
		public int GetClanOfficerCount( CSteamID steamIDClan /*class CSteamID*/ )
		{
			return _pi.ISteamFriends_GetClanOfficerCount( steamIDClan.Value /*C*/ );
		}
		
		// ulong
		public ulong GetClanOwner( CSteamID steamIDClan /*class CSteamID*/ )
		{
			return _pi.ISteamFriends_GetClanOwner( steamIDClan.Value /*C*/ );
		}
		
		// string
		// with: Detect_StringReturn
		public string GetClanTag( CSteamID steamIDClan /*class CSteamID*/ )
		{
			IntPtr string_pointer;
			string_pointer = _pi.ISteamFriends_GetClanTag( steamIDClan.Value /*C*/ );
			return Marshal.PtrToStringAnsi( string_pointer );
		}
		
		// ulong
		public ulong GetCoplayFriend( int iCoplayFriend /*int*/ )
		{
			return _pi.ISteamFriends_GetCoplayFriend( iCoplayFriend /*C*/ );
		}
		
		// int
		public int GetCoplayFriendCount()
		{
			return _pi.ISteamFriends_GetCoplayFriendCount();
		}
		
		// SteamAPICall_t
		public SteamAPICall_t GetFollowerCount( CSteamID steamID /*class CSteamID*/ )
		{
			return _pi.ISteamFriends_GetFollowerCount( steamID.Value /*C*/ );
		}
		
		// ulong
		public ulong GetFriendByIndex( int iFriend /*int*/, int iFriendFlags /*int*/ )
		{
			return _pi.ISteamFriends_GetFriendByIndex( iFriend /*C*/, iFriendFlags /*C*/ );
		}
		
		// AppId_t
		public AppId_t GetFriendCoplayGame( CSteamID steamIDFriend /*class CSteamID*/ )
		{
			return _pi.ISteamFriends_GetFriendCoplayGame( steamIDFriend.Value /*C*/ );
		}
		
		// int
		public int GetFriendCoplayTime( CSteamID steamIDFriend /*class CSteamID*/ )
		{
			return _pi.ISteamFriends_GetFriendCoplayTime( steamIDFriend.Value /*C*/ );
		}
		
		// int
		public int GetFriendCount( int iFriendFlags /*int*/ )
		{
			return _pi.ISteamFriends_GetFriendCount( iFriendFlags /*C*/ );
		}
		
		// int
		public int GetFriendCountFromSource( CSteamID steamIDSource /*class CSteamID*/ )
		{
			return _pi.ISteamFriends_GetFriendCountFromSource( steamIDSource.Value /*C*/ );
		}
		
		// ulong
		public ulong GetFriendFromSourceByIndex( CSteamID steamIDSource /*class CSteamID*/, int iFriend /*int*/ )
		{
			return _pi.ISteamFriends_GetFriendFromSourceByIndex( steamIDSource.Value /*C*/, iFriend /*C*/ );
		}
		
		// bool
		public bool GetFriendGamePlayed( CSteamID steamIDFriend /*class CSteamID*/, ref FriendGameInfo_t pFriendGameInfo /*struct FriendGameInfo_t **/ )
		{
			return _pi.ISteamFriends_GetFriendGamePlayed( steamIDFriend.Value /*C*/, ref pFriendGameInfo /*A*/ );
		}
		
		// int
		public int GetFriendMessage( CSteamID steamIDFriend /*class CSteamID*/, int iMessageID /*int*/, IntPtr pvData /*void **/, int cubData /*int*/, out ChatEntryType peChatEntryType /*EChatEntryType **/ )
		{
			return _pi.ISteamFriends_GetFriendMessage( steamIDFriend.Value /*C*/, iMessageID /*C*/, (IntPtr) pvData /*C*/, cubData /*C*/, out peChatEntryType /*B*/ );
		}
		
		// string
		// with: Detect_StringReturn
		public string GetFriendPersonaName( CSteamID steamIDFriend /*class CSteamID*/ )
		{
			IntPtr string_pointer;
			string_pointer = _pi.ISteamFriends_GetFriendPersonaName( steamIDFriend.Value /*C*/ );
			return Marshal.PtrToStringAnsi( string_pointer );
		}
		
		// string
		// with: Detect_StringReturn
		public string GetFriendPersonaNameHistory( CSteamID steamIDFriend /*class CSteamID*/, int iPersonaName /*int*/ )
		{
			IntPtr string_pointer;
			string_pointer = _pi.ISteamFriends_GetFriendPersonaNameHistory( steamIDFriend.Value /*C*/, iPersonaName /*C*/ );
			return Marshal.PtrToStringAnsi( string_pointer );
		}
		
		// PersonaState
		public PersonaState GetFriendPersonaState( CSteamID steamIDFriend /*class CSteamID*/ )
		{
			return _pi.ISteamFriends_GetFriendPersonaState( steamIDFriend.Value /*C*/ );
		}
		
		// FriendRelationship
		public FriendRelationship GetFriendRelationship( CSteamID steamIDFriend /*class CSteamID*/ )
		{
			return _pi.ISteamFriends_GetFriendRelationship( steamIDFriend.Value /*C*/ );
		}
		
		// string
		// with: Detect_StringReturn
		public string GetFriendRichPresence( CSteamID steamIDFriend /*class CSteamID*/, string pchKey /*const char **/ )
		{
			IntPtr string_pointer;
			string_pointer = _pi.ISteamFriends_GetFriendRichPresence( steamIDFriend.Value /*C*/, pchKey /*C*/ );
			return Marshal.PtrToStringAnsi( string_pointer );
		}
		
		// string
		// with: Detect_StringReturn
		public string GetFriendRichPresenceKeyByIndex( CSteamID steamIDFriend /*class CSteamID*/, int iKey /*int*/ )
		{
			IntPtr string_pointer;
			string_pointer = _pi.ISteamFriends_GetFriendRichPresenceKeyByIndex( steamIDFriend.Value /*C*/, iKey /*C*/ );
			return Marshal.PtrToStringAnsi( string_pointer );
		}
		
		// int
		public int GetFriendRichPresenceKeyCount( CSteamID steamIDFriend /*class CSteamID*/ )
		{
			return _pi.ISteamFriends_GetFriendRichPresenceKeyCount( steamIDFriend.Value /*C*/ );
		}
		
		// int
		public int GetFriendsGroupCount()
		{
			return _pi.ISteamFriends_GetFriendsGroupCount();
		}
		
		// FriendsGroupID_t
		public FriendsGroupID_t GetFriendsGroupIDByIndex( int iFG /*int*/ )
		{
			return _pi.ISteamFriends_GetFriendsGroupIDByIndex( iFG /*C*/ );
		}
		
		// int
		public int GetFriendsGroupMembersCount( FriendsGroupID_t friendsGroupID /*FriendsGroupID_t*/ )
		{
			return _pi.ISteamFriends_GetFriendsGroupMembersCount( friendsGroupID.Value /*C*/ );
		}
		
		// void
		public void GetFriendsGroupMembersList( FriendsGroupID_t friendsGroupID /*FriendsGroupID_t*/, IntPtr pOutSteamIDMembers /*class CSteamID **/, int nMembersCount /*int*/ )
		{
			_pi.ISteamFriends_GetFriendsGroupMembersList( friendsGroupID.Value /*C*/, (IntPtr) pOutSteamIDMembers, nMembersCount /*C*/ );
		}
		
		// string
		// with: Detect_StringReturn
		public string GetFriendsGroupName( FriendsGroupID_t friendsGroupID /*FriendsGroupID_t*/ )
		{
			IntPtr string_pointer;
			string_pointer = _pi.ISteamFriends_GetFriendsGroupName( friendsGroupID.Value /*C*/ );
			return Marshal.PtrToStringAnsi( string_pointer );
		}
		
		// int
		public int GetFriendSteamLevel( CSteamID steamIDFriend /*class CSteamID*/ )
		{
			return _pi.ISteamFriends_GetFriendSteamLevel( steamIDFriend.Value /*C*/ );
		}
		
		// int
		public int GetLargeFriendAvatar( CSteamID steamIDFriend /*class CSteamID*/ )
		{
			return _pi.ISteamFriends_GetLargeFriendAvatar( steamIDFriend.Value /*C*/ );
		}
		
		// int
		public int GetMediumFriendAvatar( CSteamID steamIDFriend /*class CSteamID*/ )
		{
			return _pi.ISteamFriends_GetMediumFriendAvatar( steamIDFriend.Value /*C*/ );
		}
		
		// string
		// with: Detect_StringReturn
		public string GetPersonaName()
		{
			IntPtr string_pointer;
			string_pointer = _pi.ISteamFriends_GetPersonaName();
			return Marshal.PtrToStringAnsi( string_pointer );
		}
		
		// PersonaState
		public PersonaState GetPersonaState()
		{
			return _pi.ISteamFriends_GetPersonaState();
		}
		
		// string
		// with: Detect_StringReturn
		public string GetPlayerNickname( CSteamID steamIDPlayer /*class CSteamID*/ )
		{
			IntPtr string_pointer;
			string_pointer = _pi.ISteamFriends_GetPlayerNickname( steamIDPlayer.Value /*C*/ );
			return Marshal.PtrToStringAnsi( string_pointer );
		}
		
		// int
		public int GetSmallFriendAvatar( CSteamID steamIDFriend /*class CSteamID*/ )
		{
			return _pi.ISteamFriends_GetSmallFriendAvatar( steamIDFriend.Value /*C*/ );
		}
		
		// uint
		public uint GetUserRestrictions()
		{
			return _pi.ISteamFriends_GetUserRestrictions();
		}
		
		// bool
		public bool HasFriend( CSteamID steamIDFriend /*class CSteamID*/, int iFriendFlags /*int*/ )
		{
			return _pi.ISteamFriends_HasFriend( steamIDFriend.Value /*C*/, iFriendFlags /*C*/ );
		}
		
		// bool
		public bool InviteUserToGame( CSteamID steamIDFriend /*class CSteamID*/, string pchConnectString /*const char **/ )
		{
			return _pi.ISteamFriends_InviteUserToGame( steamIDFriend.Value /*C*/, pchConnectString /*C*/ );
		}
		
		// bool
		public bool IsClanChatAdmin( CSteamID steamIDClanChat /*class CSteamID*/, CSteamID steamIDUser /*class CSteamID*/ )
		{
			return _pi.ISteamFriends_IsClanChatAdmin( steamIDClanChat.Value /*C*/, steamIDUser.Value /*C*/ );
		}
		
		// bool
		public bool IsClanChatWindowOpenInSteam( CSteamID steamIDClanChat /*class CSteamID*/ )
		{
			return _pi.ISteamFriends_IsClanChatWindowOpenInSteam( steamIDClanChat.Value /*C*/ );
		}
		
		// SteamAPICall_t
		public SteamAPICall_t IsFollowing( CSteamID steamID /*class CSteamID*/ )
		{
			return _pi.ISteamFriends_IsFollowing( steamID.Value /*C*/ );
		}
		
		// bool
		public bool IsUserInSource( CSteamID steamIDUser /*class CSteamID*/, CSteamID steamIDSource /*class CSteamID*/ )
		{
			return _pi.ISteamFriends_IsUserInSource( steamIDUser.Value /*C*/, steamIDSource.Value /*C*/ );
		}
		
		// SteamAPICall_t
		public SteamAPICall_t JoinClanChatRoom( CSteamID steamIDClan /*class CSteamID*/ )
		{
			return _pi.ISteamFriends_JoinClanChatRoom( steamIDClan.Value /*C*/ );
		}
		
		// bool
		public bool LeaveClanChatRoom( CSteamID steamIDClan /*class CSteamID*/ )
		{
			return _pi.ISteamFriends_LeaveClanChatRoom( steamIDClan.Value /*C*/ );
		}
		
		// bool
		public bool OpenClanChatWindowInSteam( CSteamID steamIDClanChat /*class CSteamID*/ )
		{
			return _pi.ISteamFriends_OpenClanChatWindowInSteam( steamIDClanChat.Value /*C*/ );
		}
		
		// bool
		public bool ReplyToFriendMessage( CSteamID steamIDFriend /*class CSteamID*/, string pchMsgToSend /*const char **/ )
		{
			return _pi.ISteamFriends_ReplyToFriendMessage( steamIDFriend.Value /*C*/, pchMsgToSend /*C*/ );
		}
		
		// SteamAPICall_t
		public SteamAPICall_t RequestClanOfficerList( CSteamID steamIDClan /*class CSteamID*/ )
		{
			return _pi.ISteamFriends_RequestClanOfficerList( steamIDClan.Value /*C*/ );
		}
		
		// void
		public void RequestFriendRichPresence( CSteamID steamIDFriend /*class CSteamID*/ )
		{
			_pi.ISteamFriends_RequestFriendRichPresence( steamIDFriend.Value /*C*/ );
		}
		
		// bool
		public bool RequestUserInformation( CSteamID steamIDUser /*class CSteamID*/, bool bRequireNameOnly /*bool*/ )
		{
			return _pi.ISteamFriends_RequestUserInformation( steamIDUser.Value /*C*/, bRequireNameOnly /*C*/ );
		}
		
		// bool
		public bool SendClanChatMessage( CSteamID steamIDClanChat /*class CSteamID*/, string pchText /*const char **/ )
		{
			return _pi.ISteamFriends_SendClanChatMessage( steamIDClanChat.Value /*C*/, pchText /*C*/ );
		}
		
		// void
		public void SetInGameVoiceSpeaking( CSteamID steamIDUser /*class CSteamID*/, bool bSpeaking /*bool*/ )
		{
			_pi.ISteamFriends_SetInGameVoiceSpeaking( steamIDUser.Value /*C*/, bSpeaking /*C*/ );
		}
		
		// bool
		public bool SetListenForFriendsMessages( bool bInterceptEnabled /*bool*/ )
		{
			return _pi.ISteamFriends_SetListenForFriendsMessages( bInterceptEnabled /*C*/ );
		}
		
		// SteamAPICall_t
		public SteamAPICall_t SetPersonaName( string pchPersonaName /*const char **/ )
		{
			return _pi.ISteamFriends_SetPersonaName( pchPersonaName /*C*/ );
		}
		
		// void
		public void SetPlayedWith( CSteamID steamIDUserPlayedWith /*class CSteamID*/ )
		{
			_pi.ISteamFriends_SetPlayedWith( steamIDUserPlayedWith.Value /*C*/ );
		}
		
		// bool
		public bool SetRichPresence( string pchKey /*const char **/, string pchValue /*const char **/ )
		{
			return _pi.ISteamFriends_SetRichPresence( pchKey /*C*/, pchValue /*C*/ );
		}
		
	}
}
