using System;
using System.Runtime.InteropServices;

namespace SteamNative
{
	internal unsafe class SteamFriends : IDisposable
	{
		//
		// Holds a platform specific implentation
		//
		internal Platform.Interface platform;
		internal Facepunch.Steamworks.BaseSteamworks steamworks;
		
		//
		// Constructor decides which implementation to use based on current platform
		//
		internal SteamFriends( Facepunch.Steamworks.BaseSteamworks steamworks, IntPtr pointer )
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
		public void ActivateGameOverlay( string pchDialog /*const char **/ )
		{
			platform.ISteamFriends_ActivateGameOverlay( pchDialog );
		}
		
		// void
		public void ActivateGameOverlayInviteDialog( CSteamID steamIDLobby /*class CSteamID*/ )
		{
			platform.ISteamFriends_ActivateGameOverlayInviteDialog( steamIDLobby.Value );
		}
		
		// void
		public void ActivateGameOverlayToStore( AppId_t nAppID /*AppId_t*/, OverlayToStoreFlag eFlag /*EOverlayToStoreFlag*/ )
		{
			platform.ISteamFriends_ActivateGameOverlayToStore( nAppID.Value, eFlag );
		}
		
		// void
		public void ActivateGameOverlayToUser( string pchDialog /*const char **/, CSteamID steamID /*class CSteamID*/ )
		{
			platform.ISteamFriends_ActivateGameOverlayToUser( pchDialog, steamID.Value );
		}
		
		// void
		public void ActivateGameOverlayToWebPage( string pchURL /*const char **/ )
		{
			platform.ISteamFriends_ActivateGameOverlayToWebPage( pchURL );
		}
		
		// void
		public void ClearRichPresence()
		{
			platform.ISteamFriends_ClearRichPresence();
		}
		
		// bool
		public bool CloseClanChatWindowInSteam( CSteamID steamIDClanChat /*class CSteamID*/ )
		{
			return platform.ISteamFriends_CloseClanChatWindowInSteam( steamIDClanChat.Value );
		}
		
		// SteamAPICall_t
		public SteamAPICall_t DownloadClanActivityCounts( IntPtr psteamIDClans /*class CSteamID **/, int cClansToRequest /*int*/ )
		{
			return platform.ISteamFriends_DownloadClanActivityCounts( (IntPtr) psteamIDClans, cClansToRequest );
		}
		
		// SteamAPICall_t
		public CallbackHandle EnumerateFollowingList( uint unStartIndex /*uint32*/, Action<FriendsEnumerateFollowingList_t, bool> CallbackFunction = null /*Action<FriendsEnumerateFollowingList_t, bool>*/ )
		{
			SteamAPICall_t callback = 0;
			callback = platform.ISteamFriends_EnumerateFollowingList( unStartIndex );
			
			if ( CallbackFunction == null ) return null;
			
			return FriendsEnumerateFollowingList_t.CallResult( steamworks, callback, CallbackFunction );
		}
		
		// ulong
		public ulong GetChatMemberByIndex( CSteamID steamIDClan /*class CSteamID*/, int iUser /*int*/ )
		{
			return platform.ISteamFriends_GetChatMemberByIndex( steamIDClan.Value, iUser );
		}
		
		// bool
		public bool GetClanActivityCounts( CSteamID steamIDClan /*class CSteamID*/, out int pnOnline /*int **/, out int pnInGame /*int **/, out int pnChatting /*int **/ )
		{
			return platform.ISteamFriends_GetClanActivityCounts( steamIDClan.Value, out pnOnline, out pnInGame, out pnChatting );
		}
		
		// ulong
		public ulong GetClanByIndex( int iClan /*int*/ )
		{
			return platform.ISteamFriends_GetClanByIndex( iClan );
		}
		
		// int
		public int GetClanChatMemberCount( CSteamID steamIDClan /*class CSteamID*/ )
		{
			return platform.ISteamFriends_GetClanChatMemberCount( steamIDClan.Value );
		}
		
		// int
		public int GetClanChatMessage( CSteamID steamIDClanChat /*class CSteamID*/, int iMessage /*int*/, IntPtr prgchText /*void **/, int cchTextMax /*int*/, out ChatEntryType peChatEntryType /*EChatEntryType **/, out CSteamID psteamidChatter /*class CSteamID **/ )
		{
			return platform.ISteamFriends_GetClanChatMessage( steamIDClanChat.Value, iMessage, (IntPtr) prgchText, cchTextMax, out peChatEntryType, out psteamidChatter.Value );
		}
		
		// int
		public int GetClanCount()
		{
			return platform.ISteamFriends_GetClanCount();
		}
		
		// string
		// with: Detect_StringReturn
		public string GetClanName( CSteamID steamIDClan /*class CSteamID*/ )
		{
			IntPtr string_pointer;
			string_pointer = platform.ISteamFriends_GetClanName( steamIDClan.Value );
			return Marshal.PtrToStringAnsi( string_pointer );
		}
		
		// ulong
		public ulong GetClanOfficerByIndex( CSteamID steamIDClan /*class CSteamID*/, int iOfficer /*int*/ )
		{
			return platform.ISteamFriends_GetClanOfficerByIndex( steamIDClan.Value, iOfficer );
		}
		
		// int
		public int GetClanOfficerCount( CSteamID steamIDClan /*class CSteamID*/ )
		{
			return platform.ISteamFriends_GetClanOfficerCount( steamIDClan.Value );
		}
		
		// ulong
		public ulong GetClanOwner( CSteamID steamIDClan /*class CSteamID*/ )
		{
			return platform.ISteamFriends_GetClanOwner( steamIDClan.Value );
		}
		
		// string
		// with: Detect_StringReturn
		public string GetClanTag( CSteamID steamIDClan /*class CSteamID*/ )
		{
			IntPtr string_pointer;
			string_pointer = platform.ISteamFriends_GetClanTag( steamIDClan.Value );
			return Marshal.PtrToStringAnsi( string_pointer );
		}
		
		// ulong
		public ulong GetCoplayFriend( int iCoplayFriend /*int*/ )
		{
			return platform.ISteamFriends_GetCoplayFriend( iCoplayFriend );
		}
		
		// int
		public int GetCoplayFriendCount()
		{
			return platform.ISteamFriends_GetCoplayFriendCount();
		}
		
		// SteamAPICall_t
		public CallbackHandle GetFollowerCount( CSteamID steamID /*class CSteamID*/, Action<FriendsGetFollowerCount_t, bool> CallbackFunction = null /*Action<FriendsGetFollowerCount_t, bool>*/ )
		{
			SteamAPICall_t callback = 0;
			callback = platform.ISteamFriends_GetFollowerCount( steamID.Value );
			
			if ( CallbackFunction == null ) return null;
			
			return FriendsGetFollowerCount_t.CallResult( steamworks, callback, CallbackFunction );
		}
		
		// ulong
		public ulong GetFriendByIndex( int iFriend /*int*/, int iFriendFlags /*int*/ )
		{
			return platform.ISteamFriends_GetFriendByIndex( iFriend, iFriendFlags );
		}
		
		// AppId_t
		public AppId_t GetFriendCoplayGame( CSteamID steamIDFriend /*class CSteamID*/ )
		{
			return platform.ISteamFriends_GetFriendCoplayGame( steamIDFriend.Value );
		}
		
		// int
		public int GetFriendCoplayTime( CSteamID steamIDFriend /*class CSteamID*/ )
		{
			return platform.ISteamFriends_GetFriendCoplayTime( steamIDFriend.Value );
		}
		
		// int
		public int GetFriendCount( int iFriendFlags /*int*/ )
		{
			return platform.ISteamFriends_GetFriendCount( iFriendFlags );
		}
		
		// int
		public int GetFriendCountFromSource( CSteamID steamIDSource /*class CSteamID*/ )
		{
			return platform.ISteamFriends_GetFriendCountFromSource( steamIDSource.Value );
		}
		
		// ulong
		public ulong GetFriendFromSourceByIndex( CSteamID steamIDSource /*class CSteamID*/, int iFriend /*int*/ )
		{
			return platform.ISteamFriends_GetFriendFromSourceByIndex( steamIDSource.Value, iFriend );
		}
		
		// bool
		public bool GetFriendGamePlayed( CSteamID steamIDFriend /*class CSteamID*/, ref FriendGameInfo_t pFriendGameInfo /*struct FriendGameInfo_t **/ )
		{
			return platform.ISteamFriends_GetFriendGamePlayed( steamIDFriend.Value, ref pFriendGameInfo );
		}
		
		// int
		public int GetFriendMessage( CSteamID steamIDFriend /*class CSteamID*/, int iMessageID /*int*/, IntPtr pvData /*void **/, int cubData /*int*/, out ChatEntryType peChatEntryType /*EChatEntryType **/ )
		{
			return platform.ISteamFriends_GetFriendMessage( steamIDFriend.Value, iMessageID, (IntPtr) pvData, cubData, out peChatEntryType );
		}
		
		// string
		// with: Detect_StringReturn
		public string GetFriendPersonaName( CSteamID steamIDFriend /*class CSteamID*/ )
		{
			IntPtr string_pointer;
			string_pointer = platform.ISteamFriends_GetFriendPersonaName( steamIDFriend.Value );
			return Marshal.PtrToStringAnsi( string_pointer );
		}
		
		// string
		// with: Detect_StringReturn
		public string GetFriendPersonaNameHistory( CSteamID steamIDFriend /*class CSteamID*/, int iPersonaName /*int*/ )
		{
			IntPtr string_pointer;
			string_pointer = platform.ISteamFriends_GetFriendPersonaNameHistory( steamIDFriend.Value, iPersonaName );
			return Marshal.PtrToStringAnsi( string_pointer );
		}
		
		// PersonaState
		public PersonaState GetFriendPersonaState( CSteamID steamIDFriend /*class CSteamID*/ )
		{
			return platform.ISteamFriends_GetFriendPersonaState( steamIDFriend.Value );
		}
		
		// FriendRelationship
		public FriendRelationship GetFriendRelationship( CSteamID steamIDFriend /*class CSteamID*/ )
		{
			return platform.ISteamFriends_GetFriendRelationship( steamIDFriend.Value );
		}
		
		// string
		// with: Detect_StringReturn
		public string GetFriendRichPresence( CSteamID steamIDFriend /*class CSteamID*/, string pchKey /*const char **/ )
		{
			IntPtr string_pointer;
			string_pointer = platform.ISteamFriends_GetFriendRichPresence( steamIDFriend.Value, pchKey );
			return Marshal.PtrToStringAnsi( string_pointer );
		}
		
		// string
		// with: Detect_StringReturn
		public string GetFriendRichPresenceKeyByIndex( CSteamID steamIDFriend /*class CSteamID*/, int iKey /*int*/ )
		{
			IntPtr string_pointer;
			string_pointer = platform.ISteamFriends_GetFriendRichPresenceKeyByIndex( steamIDFriend.Value, iKey );
			return Marshal.PtrToStringAnsi( string_pointer );
		}
		
		// int
		public int GetFriendRichPresenceKeyCount( CSteamID steamIDFriend /*class CSteamID*/ )
		{
			return platform.ISteamFriends_GetFriendRichPresenceKeyCount( steamIDFriend.Value );
		}
		
		// int
		public int GetFriendsGroupCount()
		{
			return platform.ISteamFriends_GetFriendsGroupCount();
		}
		
		// FriendsGroupID_t
		public FriendsGroupID_t GetFriendsGroupIDByIndex( int iFG /*int*/ )
		{
			return platform.ISteamFriends_GetFriendsGroupIDByIndex( iFG );
		}
		
		// int
		public int GetFriendsGroupMembersCount( FriendsGroupID_t friendsGroupID /*FriendsGroupID_t*/ )
		{
			return platform.ISteamFriends_GetFriendsGroupMembersCount( friendsGroupID.Value );
		}
		
		// void
		public void GetFriendsGroupMembersList( FriendsGroupID_t friendsGroupID /*FriendsGroupID_t*/, IntPtr pOutSteamIDMembers /*class CSteamID **/, int nMembersCount /*int*/ )
		{
			platform.ISteamFriends_GetFriendsGroupMembersList( friendsGroupID.Value, (IntPtr) pOutSteamIDMembers, nMembersCount );
		}
		
		// string
		// with: Detect_StringReturn
		public string GetFriendsGroupName( FriendsGroupID_t friendsGroupID /*FriendsGroupID_t*/ )
		{
			IntPtr string_pointer;
			string_pointer = platform.ISteamFriends_GetFriendsGroupName( friendsGroupID.Value );
			return Marshal.PtrToStringAnsi( string_pointer );
		}
		
		// int
		public int GetFriendSteamLevel( CSteamID steamIDFriend /*class CSteamID*/ )
		{
			return platform.ISteamFriends_GetFriendSteamLevel( steamIDFriend.Value );
		}
		
		// int
		public int GetLargeFriendAvatar( CSteamID steamIDFriend /*class CSteamID*/ )
		{
			return platform.ISteamFriends_GetLargeFriendAvatar( steamIDFriend.Value );
		}
		
		// int
		public int GetMediumFriendAvatar( CSteamID steamIDFriend /*class CSteamID*/ )
		{
			return platform.ISteamFriends_GetMediumFriendAvatar( steamIDFriend.Value );
		}
		
		// string
		// with: Detect_StringReturn
		public string GetPersonaName()
		{
			IntPtr string_pointer;
			string_pointer = platform.ISteamFriends_GetPersonaName();
			return Marshal.PtrToStringAnsi( string_pointer );
		}
		
		// PersonaState
		public PersonaState GetPersonaState()
		{
			return platform.ISteamFriends_GetPersonaState();
		}
		
		// string
		// with: Detect_StringReturn
		public string GetPlayerNickname( CSteamID steamIDPlayer /*class CSteamID*/ )
		{
			IntPtr string_pointer;
			string_pointer = platform.ISteamFriends_GetPlayerNickname( steamIDPlayer.Value );
			return Marshal.PtrToStringAnsi( string_pointer );
		}
		
		// int
		public int GetSmallFriendAvatar( CSteamID steamIDFriend /*class CSteamID*/ )
		{
			return platform.ISteamFriends_GetSmallFriendAvatar( steamIDFriend.Value );
		}
		
		// uint
		public uint GetUserRestrictions()
		{
			return platform.ISteamFriends_GetUserRestrictions();
		}
		
		// bool
		public bool HasFriend( CSteamID steamIDFriend /*class CSteamID*/, int iFriendFlags /*int*/ )
		{
			return platform.ISteamFriends_HasFriend( steamIDFriend.Value, iFriendFlags );
		}
		
		// bool
		public bool InviteUserToGame( CSteamID steamIDFriend /*class CSteamID*/, string pchConnectString /*const char **/ )
		{
			return platform.ISteamFriends_InviteUserToGame( steamIDFriend.Value, pchConnectString );
		}
		
		// bool
		public bool IsClanChatAdmin( CSteamID steamIDClanChat /*class CSteamID*/, CSteamID steamIDUser /*class CSteamID*/ )
		{
			return platform.ISteamFriends_IsClanChatAdmin( steamIDClanChat.Value, steamIDUser.Value );
		}
		
		// bool
		public bool IsClanChatWindowOpenInSteam( CSteamID steamIDClanChat /*class CSteamID*/ )
		{
			return platform.ISteamFriends_IsClanChatWindowOpenInSteam( steamIDClanChat.Value );
		}
		
		// SteamAPICall_t
		public CallbackHandle IsFollowing( CSteamID steamID /*class CSteamID*/, Action<FriendsIsFollowing_t, bool> CallbackFunction = null /*Action<FriendsIsFollowing_t, bool>*/ )
		{
			SteamAPICall_t callback = 0;
			callback = platform.ISteamFriends_IsFollowing( steamID.Value );
			
			if ( CallbackFunction == null ) return null;
			
			return FriendsIsFollowing_t.CallResult( steamworks, callback, CallbackFunction );
		}
		
		// bool
		public bool IsUserInSource( CSteamID steamIDUser /*class CSteamID*/, CSteamID steamIDSource /*class CSteamID*/ )
		{
			return platform.ISteamFriends_IsUserInSource( steamIDUser.Value, steamIDSource.Value );
		}
		
		// SteamAPICall_t
		public CallbackHandle JoinClanChatRoom( CSteamID steamIDClan /*class CSteamID*/, Action<JoinClanChatRoomCompletionResult_t, bool> CallbackFunction = null /*Action<JoinClanChatRoomCompletionResult_t, bool>*/ )
		{
			SteamAPICall_t callback = 0;
			callback = platform.ISteamFriends_JoinClanChatRoom( steamIDClan.Value );
			
			if ( CallbackFunction == null ) return null;
			
			return JoinClanChatRoomCompletionResult_t.CallResult( steamworks, callback, CallbackFunction );
		}
		
		// bool
		public bool LeaveClanChatRoom( CSteamID steamIDClan /*class CSteamID*/ )
		{
			return platform.ISteamFriends_LeaveClanChatRoom( steamIDClan.Value );
		}
		
		// bool
		public bool OpenClanChatWindowInSteam( CSteamID steamIDClanChat /*class CSteamID*/ )
		{
			return platform.ISteamFriends_OpenClanChatWindowInSteam( steamIDClanChat.Value );
		}
		
		// bool
		public bool ReplyToFriendMessage( CSteamID steamIDFriend /*class CSteamID*/, string pchMsgToSend /*const char **/ )
		{
			return platform.ISteamFriends_ReplyToFriendMessage( steamIDFriend.Value, pchMsgToSend );
		}
		
		// SteamAPICall_t
		public CallbackHandle RequestClanOfficerList( CSteamID steamIDClan /*class CSteamID*/, Action<ClanOfficerListResponse_t, bool> CallbackFunction = null /*Action<ClanOfficerListResponse_t, bool>*/ )
		{
			SteamAPICall_t callback = 0;
			callback = platform.ISteamFriends_RequestClanOfficerList( steamIDClan.Value );
			
			if ( CallbackFunction == null ) return null;
			
			return ClanOfficerListResponse_t.CallResult( steamworks, callback, CallbackFunction );
		}
		
		// void
		public void RequestFriendRichPresence( CSteamID steamIDFriend /*class CSteamID*/ )
		{
			platform.ISteamFriends_RequestFriendRichPresence( steamIDFriend.Value );
		}
		
		// bool
		public bool RequestUserInformation( CSteamID steamIDUser /*class CSteamID*/, bool bRequireNameOnly /*bool*/ )
		{
			return platform.ISteamFriends_RequestUserInformation( steamIDUser.Value, bRequireNameOnly );
		}
		
		// bool
		public bool SendClanChatMessage( CSteamID steamIDClanChat /*class CSteamID*/, string pchText /*const char **/ )
		{
			return platform.ISteamFriends_SendClanChatMessage( steamIDClanChat.Value, pchText );
		}
		
		// void
		public void SetInGameVoiceSpeaking( CSteamID steamIDUser /*class CSteamID*/, bool bSpeaking /*bool*/ )
		{
			platform.ISteamFriends_SetInGameVoiceSpeaking( steamIDUser.Value, bSpeaking );
		}
		
		// bool
		public bool SetListenForFriendsMessages( bool bInterceptEnabled /*bool*/ )
		{
			return platform.ISteamFriends_SetListenForFriendsMessages( bInterceptEnabled );
		}
		
		// SteamAPICall_t
		public CallbackHandle SetPersonaName( string pchPersonaName /*const char **/, Action<SetPersonaNameResponse_t, bool> CallbackFunction = null /*Action<SetPersonaNameResponse_t, bool>*/ )
		{
			SteamAPICall_t callback = 0;
			callback = platform.ISteamFriends_SetPersonaName( pchPersonaName );
			
			if ( CallbackFunction == null ) return null;
			
			return SetPersonaNameResponse_t.CallResult( steamworks, callback, CallbackFunction );
		}
		
		// void
		public void SetPlayedWith( CSteamID steamIDUserPlayedWith /*class CSteamID*/ )
		{
			platform.ISteamFriends_SetPlayedWith( steamIDUserPlayedWith.Value );
		}
		
		// bool
		public bool SetRichPresence( string pchKey /*const char **/, string pchValue /*const char **/ )
		{
			return platform.ISteamFriends_SetRichPresence( pchKey, pchValue );
		}
		
	}
}
