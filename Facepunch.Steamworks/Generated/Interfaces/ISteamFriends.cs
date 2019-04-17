using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Steamworks.Data;


namespace Steamworks
{
	internal class ISteamFriends : SteamInterface
	{
		public ISteamFriends( bool server = false ) : base( server )
		{
		}
		
		public override string InterfaceName => "SteamFriends017";
		
		public override void InitInternals()
		{
			_GetPersonaName = Marshal.GetDelegateForFunctionPointer<FGetPersonaName>( Marshal.ReadIntPtr( VTable, 0) );
			_SetPersonaName = Marshal.GetDelegateForFunctionPointer<FSetPersonaName>( Marshal.ReadIntPtr( VTable, 8) );
			_GetPersonaState = Marshal.GetDelegateForFunctionPointer<FGetPersonaState>( Marshal.ReadIntPtr( VTable, 16) );
			_GetFriendCount = Marshal.GetDelegateForFunctionPointer<FGetFriendCount>( Marshal.ReadIntPtr( VTable, 24) );
			_GetFriendByIndex = Marshal.GetDelegateForFunctionPointer<FGetFriendByIndex>( Marshal.ReadIntPtr( VTable, 32) );
			_GetFriendRelationship = Marshal.GetDelegateForFunctionPointer<FGetFriendRelationship>( Marshal.ReadIntPtr( VTable, 40) );
			_GetFriendPersonaState = Marshal.GetDelegateForFunctionPointer<FGetFriendPersonaState>( Marshal.ReadIntPtr( VTable, 48) );
			_GetFriendPersonaName = Marshal.GetDelegateForFunctionPointer<FGetFriendPersonaName>( Marshal.ReadIntPtr( VTable, 56) );
			_GetFriendGamePlayed = Marshal.GetDelegateForFunctionPointer<FGetFriendGamePlayed>( Marshal.ReadIntPtr( VTable, 64) );
			_GetFriendPersonaNameHistory = Marshal.GetDelegateForFunctionPointer<FGetFriendPersonaNameHistory>( Marshal.ReadIntPtr( VTable, 72) );
			_GetFriendSteamLevel = Marshal.GetDelegateForFunctionPointer<FGetFriendSteamLevel>( Marshal.ReadIntPtr( VTable, 80) );
			_GetPlayerNickname = Marshal.GetDelegateForFunctionPointer<FGetPlayerNickname>( Marshal.ReadIntPtr( VTable, 88) );
			_GetFriendsGroupCount = Marshal.GetDelegateForFunctionPointer<FGetFriendsGroupCount>( Marshal.ReadIntPtr( VTable, 96) );
			_GetFriendsGroupIDByIndex = Marshal.GetDelegateForFunctionPointer<FGetFriendsGroupIDByIndex>( Marshal.ReadIntPtr( VTable, 104) );
			_GetFriendsGroupName = Marshal.GetDelegateForFunctionPointer<FGetFriendsGroupName>( Marshal.ReadIntPtr( VTable, 112) );
			_GetFriendsGroupMembersCount = Marshal.GetDelegateForFunctionPointer<FGetFriendsGroupMembersCount>( Marshal.ReadIntPtr( VTable, 120) );
			_GetFriendsGroupMembersList = Marshal.GetDelegateForFunctionPointer<FGetFriendsGroupMembersList>( Marshal.ReadIntPtr( VTable, 128) );
			_HasFriend = Marshal.GetDelegateForFunctionPointer<FHasFriend>( Marshal.ReadIntPtr( VTable, 136) );
			_GetClanCount = Marshal.GetDelegateForFunctionPointer<FGetClanCount>( Marshal.ReadIntPtr( VTable, 144) );
			_GetClanByIndex = Marshal.GetDelegateForFunctionPointer<FGetClanByIndex>( Marshal.ReadIntPtr( VTable, 152) );
			_GetClanName = Marshal.GetDelegateForFunctionPointer<FGetClanName>( Marshal.ReadIntPtr( VTable, 160) );
			_GetClanTag = Marshal.GetDelegateForFunctionPointer<FGetClanTag>( Marshal.ReadIntPtr( VTable, 168) );
			_GetClanActivityCounts = Marshal.GetDelegateForFunctionPointer<FGetClanActivityCounts>( Marshal.ReadIntPtr( VTable, 176) );
			_DownloadClanActivityCounts = Marshal.GetDelegateForFunctionPointer<FDownloadClanActivityCounts>( Marshal.ReadIntPtr( VTable, 184) );
			_GetFriendCountFromSource = Marshal.GetDelegateForFunctionPointer<FGetFriendCountFromSource>( Marshal.ReadIntPtr( VTable, 192) );
			_GetFriendFromSourceByIndex = Marshal.GetDelegateForFunctionPointer<FGetFriendFromSourceByIndex>( Marshal.ReadIntPtr( VTable, 200) );
			_IsUserInSource = Marshal.GetDelegateForFunctionPointer<FIsUserInSource>( Marshal.ReadIntPtr( VTable, 208) );
			_SetInGameVoiceSpeaking = Marshal.GetDelegateForFunctionPointer<FSetInGameVoiceSpeaking>( Marshal.ReadIntPtr( VTable, 216) );
			_ActivateGameOverlay = Marshal.GetDelegateForFunctionPointer<FActivateGameOverlay>( Marshal.ReadIntPtr( VTable, 224) );
			_ActivateGameOverlayToUser = Marshal.GetDelegateForFunctionPointer<FActivateGameOverlayToUser>( Marshal.ReadIntPtr( VTable, 232) );
			_ActivateGameOverlayToWebPage = Marshal.GetDelegateForFunctionPointer<FActivateGameOverlayToWebPage>( Marshal.ReadIntPtr( VTable, 240) );
			_ActivateGameOverlayToStore = Marshal.GetDelegateForFunctionPointer<FActivateGameOverlayToStore>( Marshal.ReadIntPtr( VTable, 248) );
			_SetPlayedWith = Marshal.GetDelegateForFunctionPointer<FSetPlayedWith>( Marshal.ReadIntPtr( VTable, 256) );
			_ActivateGameOverlayInviteDialog = Marshal.GetDelegateForFunctionPointer<FActivateGameOverlayInviteDialog>( Marshal.ReadIntPtr( VTable, 264) );
			_GetSmallFriendAvatar = Marshal.GetDelegateForFunctionPointer<FGetSmallFriendAvatar>( Marshal.ReadIntPtr( VTable, 272) );
			_GetMediumFriendAvatar = Marshal.GetDelegateForFunctionPointer<FGetMediumFriendAvatar>( Marshal.ReadIntPtr( VTable, 280) );
			_GetLargeFriendAvatar = Marshal.GetDelegateForFunctionPointer<FGetLargeFriendAvatar>( Marshal.ReadIntPtr( VTable, 288) );
			_RequestUserInformation = Marshal.GetDelegateForFunctionPointer<FRequestUserInformation>( Marshal.ReadIntPtr( VTable, 296) );
			_RequestClanOfficerList = Marshal.GetDelegateForFunctionPointer<FRequestClanOfficerList>( Marshal.ReadIntPtr( VTable, 304) );
			_GetClanOwner = Marshal.GetDelegateForFunctionPointer<FGetClanOwner>( Marshal.ReadIntPtr( VTable, 312) );
			_GetClanOfficerCount = Marshal.GetDelegateForFunctionPointer<FGetClanOfficerCount>( Marshal.ReadIntPtr( VTable, 320) );
			_GetClanOfficerByIndex = Marshal.GetDelegateForFunctionPointer<FGetClanOfficerByIndex>( Marshal.ReadIntPtr( VTable, 328) );
			_GetUserRestrictions = Marshal.GetDelegateForFunctionPointer<FGetUserRestrictions>( Marshal.ReadIntPtr( VTable, 336) );
			_SetRichPresence = Marshal.GetDelegateForFunctionPointer<FSetRichPresence>( Marshal.ReadIntPtr( VTable, 344) );
			_ClearRichPresence = Marshal.GetDelegateForFunctionPointer<FClearRichPresence>( Marshal.ReadIntPtr( VTable, 352) );
			_GetFriendRichPresence = Marshal.GetDelegateForFunctionPointer<FGetFriendRichPresence>( Marshal.ReadIntPtr( VTable, 360) );
			_GetFriendRichPresenceKeyCount = Marshal.GetDelegateForFunctionPointer<FGetFriendRichPresenceKeyCount>( Marshal.ReadIntPtr( VTable, 368) );
			_GetFriendRichPresenceKeyByIndex = Marshal.GetDelegateForFunctionPointer<FGetFriendRichPresenceKeyByIndex>( Marshal.ReadIntPtr( VTable, 376) );
			_RequestFriendRichPresence = Marshal.GetDelegateForFunctionPointer<FRequestFriendRichPresence>( Marshal.ReadIntPtr( VTable, 384) );
			_InviteUserToGame = Marshal.GetDelegateForFunctionPointer<FInviteUserToGame>( Marshal.ReadIntPtr( VTable, 392) );
			_GetCoplayFriendCount = Marshal.GetDelegateForFunctionPointer<FGetCoplayFriendCount>( Marshal.ReadIntPtr( VTable, 400) );
			_GetCoplayFriend = Marshal.GetDelegateForFunctionPointer<FGetCoplayFriend>( Marshal.ReadIntPtr( VTable, 408) );
			_GetFriendCoplayTime = Marshal.GetDelegateForFunctionPointer<FGetFriendCoplayTime>( Marshal.ReadIntPtr( VTable, 416) );
			_GetFriendCoplayGame = Marshal.GetDelegateForFunctionPointer<FGetFriendCoplayGame>( Marshal.ReadIntPtr( VTable, 424) );
			_JoinClanChatRoom = Marshal.GetDelegateForFunctionPointer<FJoinClanChatRoom>( Marshal.ReadIntPtr( VTable, 432) );
			_LeaveClanChatRoom = Marshal.GetDelegateForFunctionPointer<FLeaveClanChatRoom>( Marshal.ReadIntPtr( VTable, 440) );
			_GetClanChatMemberCount = Marshal.GetDelegateForFunctionPointer<FGetClanChatMemberCount>( Marshal.ReadIntPtr( VTable, 448) );
			_GetChatMemberByIndex = Marshal.GetDelegateForFunctionPointer<FGetChatMemberByIndex>( Marshal.ReadIntPtr( VTable, 456) );
			_SendClanChatMessage = Marshal.GetDelegateForFunctionPointer<FSendClanChatMessage>( Marshal.ReadIntPtr( VTable, 464) );
			_GetClanChatMessage = Marshal.GetDelegateForFunctionPointer<FGetClanChatMessage>( Marshal.ReadIntPtr( VTable, 472) );
			_IsClanChatAdmin = Marshal.GetDelegateForFunctionPointer<FIsClanChatAdmin>( Marshal.ReadIntPtr( VTable, 480) );
			_IsClanChatWindowOpenInSteam = Marshal.GetDelegateForFunctionPointer<FIsClanChatWindowOpenInSteam>( Marshal.ReadIntPtr( VTable, 488) );
			_OpenClanChatWindowInSteam = Marshal.GetDelegateForFunctionPointer<FOpenClanChatWindowInSteam>( Marshal.ReadIntPtr( VTable, 496) );
			_CloseClanChatWindowInSteam = Marshal.GetDelegateForFunctionPointer<FCloseClanChatWindowInSteam>( Marshal.ReadIntPtr( VTable, 504) );
			_SetListenForFriendsMessages = Marshal.GetDelegateForFunctionPointer<FSetListenForFriendsMessages>( Marshal.ReadIntPtr( VTable, 512) );
			_ReplyToFriendMessage = Marshal.GetDelegateForFunctionPointer<FReplyToFriendMessage>( Marshal.ReadIntPtr( VTable, 520) );
			_GetFriendMessage = Marshal.GetDelegateForFunctionPointer<FGetFriendMessage>( Marshal.ReadIntPtr( VTable, 528) );
			_GetFollowerCount = Marshal.GetDelegateForFunctionPointer<FGetFollowerCount>( Marshal.ReadIntPtr( VTable, 536) );
			_IsFollowing = Marshal.GetDelegateForFunctionPointer<FIsFollowing>( Marshal.ReadIntPtr( VTable, 544) );
			_EnumerateFollowingList = Marshal.GetDelegateForFunctionPointer<FEnumerateFollowingList>( Marshal.ReadIntPtr( VTable, 552) );
			_IsClanPublic = Marshal.GetDelegateForFunctionPointer<FIsClanPublic>( Marshal.ReadIntPtr( VTable, 560) );
			_IsClanOfficialGameGroup = Marshal.GetDelegateForFunctionPointer<FIsClanOfficialGameGroup>( Marshal.ReadIntPtr( VTable, 568) );
			_GetNumChatsWithUnreadPriorityMessages = Marshal.GetDelegateForFunctionPointer<FGetNumChatsWithUnreadPriorityMessages>( Marshal.ReadIntPtr( VTable, 576) );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate IntPtr FGetPersonaName( IntPtr self );
		private FGetPersonaName _GetPersonaName;
		
		#endregion
		internal string GetPersonaName()
		{
			return GetString( _GetPersonaName( Self ) );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate SteamAPICall_t FSetPersonaName( IntPtr self, string pchPersonaName );
		private FSetPersonaName _SetPersonaName;
		
		#endregion
		internal async Task<SetPersonaNameResponse_t?> SetPersonaName( string pchPersonaName )
		{
			return await (new Result<SetPersonaNameResponse_t>( _SetPersonaName( Self, pchPersonaName ) )).GetResult();
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate FriendState FGetPersonaState( IntPtr self );
		private FGetPersonaState _GetPersonaState;
		
		#endregion
		internal FriendState GetPersonaState()
		{
			return _GetPersonaState( Self );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate int FGetFriendCount( IntPtr self, int iFriendFlags );
		private FGetFriendCount _GetFriendCount;
		
		#endregion
		internal int GetFriendCount( int iFriendFlags )
		{
			return _GetFriendCount( Self, iFriendFlags );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate void FGetFriendByIndex( IntPtr self, ref SteamId retVal, int iFriend, int iFriendFlags );
		private FGetFriendByIndex _GetFriendByIndex;
		
		#endregion
		internal SteamId GetFriendByIndex( int iFriend, int iFriendFlags )
		{
			var retVal = default( SteamId );
			_GetFriendByIndex( Self, ref retVal, iFriend, iFriendFlags );
			return retVal;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate Relationship FGetFriendRelationship( IntPtr self, SteamId steamIDFriend );
		private FGetFriendRelationship _GetFriendRelationship;
		
		#endregion
		internal Relationship GetFriendRelationship( SteamId steamIDFriend )
		{
			return _GetFriendRelationship( Self, steamIDFriend );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate FriendState FGetFriendPersonaState( IntPtr self, SteamId steamIDFriend );
		private FGetFriendPersonaState _GetFriendPersonaState;
		
		#endregion
		internal FriendState GetFriendPersonaState( SteamId steamIDFriend )
		{
			return _GetFriendPersonaState( Self, steamIDFriend );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate IntPtr FGetFriendPersonaName( IntPtr self, SteamId steamIDFriend );
		private FGetFriendPersonaName _GetFriendPersonaName;
		
		#endregion
		internal string GetFriendPersonaName( SteamId steamIDFriend )
		{
			return GetString( _GetFriendPersonaName( Self, steamIDFriend ) );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FGetFriendGamePlayed( IntPtr self, SteamId steamIDFriend, ref FriendGameInfo_t pFriendGameInfo );
		private FGetFriendGamePlayed _GetFriendGamePlayed;
		
		#endregion
		internal bool GetFriendGamePlayed( SteamId steamIDFriend, ref FriendGameInfo_t pFriendGameInfo )
		{
			return _GetFriendGamePlayed( Self, steamIDFriend, ref pFriendGameInfo );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate IntPtr FGetFriendPersonaNameHistory( IntPtr self, SteamId steamIDFriend, int iPersonaName );
		private FGetFriendPersonaNameHistory _GetFriendPersonaNameHistory;
		
		#endregion
		internal string GetFriendPersonaNameHistory( SteamId steamIDFriend, int iPersonaName )
		{
			return GetString( _GetFriendPersonaNameHistory( Self, steamIDFriend, iPersonaName ) );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate int FGetFriendSteamLevel( IntPtr self, SteamId steamIDFriend );
		private FGetFriendSteamLevel _GetFriendSteamLevel;
		
		#endregion
		internal int GetFriendSteamLevel( SteamId steamIDFriend )
		{
			return _GetFriendSteamLevel( Self, steamIDFriend );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate IntPtr FGetPlayerNickname( IntPtr self, SteamId steamIDPlayer );
		private FGetPlayerNickname _GetPlayerNickname;
		
		#endregion
		internal string GetPlayerNickname( SteamId steamIDPlayer )
		{
			return GetString( _GetPlayerNickname( Self, steamIDPlayer ) );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate int FGetFriendsGroupCount( IntPtr self );
		private FGetFriendsGroupCount _GetFriendsGroupCount;
		
		#endregion
		internal int GetFriendsGroupCount()
		{
			return _GetFriendsGroupCount( Self );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate FriendsGroupID_t FGetFriendsGroupIDByIndex( IntPtr self, int iFG );
		private FGetFriendsGroupIDByIndex _GetFriendsGroupIDByIndex;
		
		#endregion
		internal FriendsGroupID_t GetFriendsGroupIDByIndex( int iFG )
		{
			return _GetFriendsGroupIDByIndex( Self, iFG );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate IntPtr FGetFriendsGroupName( IntPtr self, FriendsGroupID_t friendsGroupID );
		private FGetFriendsGroupName _GetFriendsGroupName;
		
		#endregion
		internal string GetFriendsGroupName( FriendsGroupID_t friendsGroupID )
		{
			return GetString( _GetFriendsGroupName( Self, friendsGroupID ) );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate int FGetFriendsGroupMembersCount( IntPtr self, FriendsGroupID_t friendsGroupID );
		private FGetFriendsGroupMembersCount _GetFriendsGroupMembersCount;
		
		#endregion
		internal int GetFriendsGroupMembersCount( FriendsGroupID_t friendsGroupID )
		{
			return _GetFriendsGroupMembersCount( Self, friendsGroupID );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate void FGetFriendsGroupMembersList( IntPtr self, FriendsGroupID_t friendsGroupID, [In,Out] SteamId[]  pOutSteamIDMembers, int nMembersCount );
		private FGetFriendsGroupMembersList _GetFriendsGroupMembersList;
		
		#endregion
		internal void GetFriendsGroupMembersList( FriendsGroupID_t friendsGroupID, [In,Out] SteamId[]  pOutSteamIDMembers, int nMembersCount )
		{
			_GetFriendsGroupMembersList( Self, friendsGroupID, pOutSteamIDMembers, nMembersCount );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FHasFriend( IntPtr self, SteamId steamIDFriend, int iFriendFlags );
		private FHasFriend _HasFriend;
		
		#endregion
		internal bool HasFriend( SteamId steamIDFriend, int iFriendFlags )
		{
			return _HasFriend( Self, steamIDFriend, iFriendFlags );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate int FGetClanCount( IntPtr self );
		private FGetClanCount _GetClanCount;
		
		#endregion
		internal int GetClanCount()
		{
			return _GetClanCount( Self );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate void FGetClanByIndex( IntPtr self, ref SteamId retVal, int iClan );
		private FGetClanByIndex _GetClanByIndex;
		
		#endregion
		internal SteamId GetClanByIndex( int iClan )
		{
			var retVal = default( SteamId );
			_GetClanByIndex( Self, ref retVal, iClan );
			return retVal;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate IntPtr FGetClanName( IntPtr self, SteamId steamIDClan );
		private FGetClanName _GetClanName;
		
		#endregion
		internal string GetClanName( SteamId steamIDClan )
		{
			return GetString( _GetClanName( Self, steamIDClan ) );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate IntPtr FGetClanTag( IntPtr self, SteamId steamIDClan );
		private FGetClanTag _GetClanTag;
		
		#endregion
		internal string GetClanTag( SteamId steamIDClan )
		{
			return GetString( _GetClanTag( Self, steamIDClan ) );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FGetClanActivityCounts( IntPtr self, SteamId steamIDClan, ref int pnOnline, ref int pnInGame, ref int pnChatting );
		private FGetClanActivityCounts _GetClanActivityCounts;
		
		#endregion
		internal bool GetClanActivityCounts( SteamId steamIDClan, ref int pnOnline, ref int pnInGame, ref int pnChatting )
		{
			return _GetClanActivityCounts( Self, steamIDClan, ref pnOnline, ref pnInGame, ref pnChatting );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate SteamAPICall_t FDownloadClanActivityCounts( IntPtr self, [In,Out] SteamId[]  psteamIDClans, int cClansToRequest );
		private FDownloadClanActivityCounts _DownloadClanActivityCounts;
		
		#endregion
		internal async Task<DownloadClanActivityCountsResult_t?> DownloadClanActivityCounts( [In,Out] SteamId[]  psteamIDClans, int cClansToRequest )
		{
			return await (new Result<DownloadClanActivityCountsResult_t>( _DownloadClanActivityCounts( Self, psteamIDClans, cClansToRequest ) )).GetResult();
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate int FGetFriendCountFromSource( IntPtr self, SteamId steamIDSource );
		private FGetFriendCountFromSource _GetFriendCountFromSource;
		
		#endregion
		internal int GetFriendCountFromSource( SteamId steamIDSource )
		{
			return _GetFriendCountFromSource( Self, steamIDSource );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate void FGetFriendFromSourceByIndex( IntPtr self, ref SteamId retVal, SteamId steamIDSource, int iFriend );
		private FGetFriendFromSourceByIndex _GetFriendFromSourceByIndex;
		
		#endregion
		internal SteamId GetFriendFromSourceByIndex( SteamId steamIDSource, int iFriend )
		{
			var retVal = default( SteamId );
			_GetFriendFromSourceByIndex( Self, ref retVal, steamIDSource, iFriend );
			return retVal;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FIsUserInSource( IntPtr self, SteamId steamIDUser, SteamId steamIDSource );
		private FIsUserInSource _IsUserInSource;
		
		#endregion
		internal bool IsUserInSource( SteamId steamIDUser, SteamId steamIDSource )
		{
			return _IsUserInSource( Self, steamIDUser, steamIDSource );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate void FSetInGameVoiceSpeaking( IntPtr self, SteamId steamIDUser, [MarshalAs( UnmanagedType.U1 )] bool bSpeaking );
		private FSetInGameVoiceSpeaking _SetInGameVoiceSpeaking;
		
		#endregion
		internal void SetInGameVoiceSpeaking( SteamId steamIDUser, [MarshalAs( UnmanagedType.U1 )] bool bSpeaking )
		{
			_SetInGameVoiceSpeaking( Self, steamIDUser, bSpeaking );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate void FActivateGameOverlay( IntPtr self, string pchDialog );
		private FActivateGameOverlay _ActivateGameOverlay;
		
		#endregion
		internal void ActivateGameOverlay( string pchDialog )
		{
			_ActivateGameOverlay( Self, pchDialog );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate void FActivateGameOverlayToUser( IntPtr self, string pchDialog, SteamId steamID );
		private FActivateGameOverlayToUser _ActivateGameOverlayToUser;
		
		#endregion
		internal void ActivateGameOverlayToUser( string pchDialog, SteamId steamID )
		{
			_ActivateGameOverlayToUser( Self, pchDialog, steamID );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate void FActivateGameOverlayToWebPage( IntPtr self, string pchURL, ActivateGameOverlayToWebPageMode eMode );
		private FActivateGameOverlayToWebPage _ActivateGameOverlayToWebPage;
		
		#endregion
		internal void ActivateGameOverlayToWebPage( string pchURL, ActivateGameOverlayToWebPageMode eMode )
		{
			_ActivateGameOverlayToWebPage( Self, pchURL, eMode );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate void FActivateGameOverlayToStore( IntPtr self, AppId_t nAppID, OverlayToStoreFlag eFlag );
		private FActivateGameOverlayToStore _ActivateGameOverlayToStore;
		
		#endregion
		internal void ActivateGameOverlayToStore( AppId_t nAppID, OverlayToStoreFlag eFlag )
		{
			_ActivateGameOverlayToStore( Self, nAppID, eFlag );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate void FSetPlayedWith( IntPtr self, SteamId steamIDUserPlayedWith );
		private FSetPlayedWith _SetPlayedWith;
		
		#endregion
		internal void SetPlayedWith( SteamId steamIDUserPlayedWith )
		{
			_SetPlayedWith( Self, steamIDUserPlayedWith );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate void FActivateGameOverlayInviteDialog( IntPtr self, SteamId steamIDLobby );
		private FActivateGameOverlayInviteDialog _ActivateGameOverlayInviteDialog;
		
		#endregion
		internal void ActivateGameOverlayInviteDialog( SteamId steamIDLobby )
		{
			_ActivateGameOverlayInviteDialog( Self, steamIDLobby );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate int FGetSmallFriendAvatar( IntPtr self, SteamId steamIDFriend );
		private FGetSmallFriendAvatar _GetSmallFriendAvatar;
		
		#endregion
		internal int GetSmallFriendAvatar( SteamId steamIDFriend )
		{
			return _GetSmallFriendAvatar( Self, steamIDFriend );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate int FGetMediumFriendAvatar( IntPtr self, SteamId steamIDFriend );
		private FGetMediumFriendAvatar _GetMediumFriendAvatar;
		
		#endregion
		internal int GetMediumFriendAvatar( SteamId steamIDFriend )
		{
			return _GetMediumFriendAvatar( Self, steamIDFriend );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate int FGetLargeFriendAvatar( IntPtr self, SteamId steamIDFriend );
		private FGetLargeFriendAvatar _GetLargeFriendAvatar;
		
		#endregion
		internal int GetLargeFriendAvatar( SteamId steamIDFriend )
		{
			return _GetLargeFriendAvatar( Self, steamIDFriend );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FRequestUserInformation( IntPtr self, SteamId steamIDUser, [MarshalAs( UnmanagedType.U1 )] bool bRequireNameOnly );
		private FRequestUserInformation _RequestUserInformation;
		
		#endregion
		internal bool RequestUserInformation( SteamId steamIDUser, [MarshalAs( UnmanagedType.U1 )] bool bRequireNameOnly )
		{
			return _RequestUserInformation( Self, steamIDUser, bRequireNameOnly );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate SteamAPICall_t FRequestClanOfficerList( IntPtr self, SteamId steamIDClan );
		private FRequestClanOfficerList _RequestClanOfficerList;
		
		#endregion
		internal async Task<ClanOfficerListResponse_t?> RequestClanOfficerList( SteamId steamIDClan )
		{
			return await (new Result<ClanOfficerListResponse_t>( _RequestClanOfficerList( Self, steamIDClan ) )).GetResult();
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate void FGetClanOwner( IntPtr self, ref SteamId retVal, SteamId steamIDClan );
		private FGetClanOwner _GetClanOwner;
		
		#endregion
		internal SteamId GetClanOwner( SteamId steamIDClan )
		{
			var retVal = default( SteamId );
			_GetClanOwner( Self, ref retVal, steamIDClan );
			return retVal;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate int FGetClanOfficerCount( IntPtr self, SteamId steamIDClan );
		private FGetClanOfficerCount _GetClanOfficerCount;
		
		#endregion
		internal int GetClanOfficerCount( SteamId steamIDClan )
		{
			return _GetClanOfficerCount( Self, steamIDClan );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate void FGetClanOfficerByIndex( IntPtr self, ref SteamId retVal, SteamId steamIDClan, int iOfficer );
		private FGetClanOfficerByIndex _GetClanOfficerByIndex;
		
		#endregion
		internal SteamId GetClanOfficerByIndex( SteamId steamIDClan, int iOfficer )
		{
			var retVal = default( SteamId );
			_GetClanOfficerByIndex( Self, ref retVal, steamIDClan, iOfficer );
			return retVal;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate uint FGetUserRestrictions( IntPtr self );
		private FGetUserRestrictions _GetUserRestrictions;
		
		#endregion
		internal uint GetUserRestrictions()
		{
			return _GetUserRestrictions( Self );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FSetRichPresence( IntPtr self, string pchKey, string pchValue );
		private FSetRichPresence _SetRichPresence;
		
		#endregion
		internal bool SetRichPresence( string pchKey, string pchValue )
		{
			return _SetRichPresence( Self, pchKey, pchValue );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate void FClearRichPresence( IntPtr self );
		private FClearRichPresence _ClearRichPresence;
		
		#endregion
		internal void ClearRichPresence()
		{
			_ClearRichPresence( Self );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate IntPtr FGetFriendRichPresence( IntPtr self, SteamId steamIDFriend, string pchKey );
		private FGetFriendRichPresence _GetFriendRichPresence;
		
		#endregion
		internal string GetFriendRichPresence( SteamId steamIDFriend, string pchKey )
		{
			return GetString( _GetFriendRichPresence( Self, steamIDFriend, pchKey ) );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate int FGetFriendRichPresenceKeyCount( IntPtr self, SteamId steamIDFriend );
		private FGetFriendRichPresenceKeyCount _GetFriendRichPresenceKeyCount;
		
		#endregion
		internal int GetFriendRichPresenceKeyCount( SteamId steamIDFriend )
		{
			return _GetFriendRichPresenceKeyCount( Self, steamIDFriend );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate IntPtr FGetFriendRichPresenceKeyByIndex( IntPtr self, SteamId steamIDFriend, int iKey );
		private FGetFriendRichPresenceKeyByIndex _GetFriendRichPresenceKeyByIndex;
		
		#endregion
		internal string GetFriendRichPresenceKeyByIndex( SteamId steamIDFriend, int iKey )
		{
			return GetString( _GetFriendRichPresenceKeyByIndex( Self, steamIDFriend, iKey ) );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate void FRequestFriendRichPresence( IntPtr self, SteamId steamIDFriend );
		private FRequestFriendRichPresence _RequestFriendRichPresence;
		
		#endregion
		internal void RequestFriendRichPresence( SteamId steamIDFriend )
		{
			_RequestFriendRichPresence( Self, steamIDFriend );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FInviteUserToGame( IntPtr self, SteamId steamIDFriend, string pchConnectString );
		private FInviteUserToGame _InviteUserToGame;
		
		#endregion
		internal bool InviteUserToGame( SteamId steamIDFriend, string pchConnectString )
		{
			return _InviteUserToGame( Self, steamIDFriend, pchConnectString );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate int FGetCoplayFriendCount( IntPtr self );
		private FGetCoplayFriendCount _GetCoplayFriendCount;
		
		#endregion
		internal int GetCoplayFriendCount()
		{
			return _GetCoplayFriendCount( Self );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate void FGetCoplayFriend( IntPtr self, ref SteamId retVal, int iCoplayFriend );
		private FGetCoplayFriend _GetCoplayFriend;
		
		#endregion
		internal SteamId GetCoplayFriend( int iCoplayFriend )
		{
			var retVal = default( SteamId );
			_GetCoplayFriend( Self, ref retVal, iCoplayFriend );
			return retVal;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate int FGetFriendCoplayTime( IntPtr self, SteamId steamIDFriend );
		private FGetFriendCoplayTime _GetFriendCoplayTime;
		
		#endregion
		internal int GetFriendCoplayTime( SteamId steamIDFriend )
		{
			return _GetFriendCoplayTime( Self, steamIDFriend );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate AppId_t FGetFriendCoplayGame( IntPtr self, SteamId steamIDFriend );
		private FGetFriendCoplayGame _GetFriendCoplayGame;
		
		#endregion
		internal AppId_t GetFriendCoplayGame( SteamId steamIDFriend )
		{
			return _GetFriendCoplayGame( Self, steamIDFriend );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate SteamAPICall_t FJoinClanChatRoom( IntPtr self, SteamId steamIDClan );
		private FJoinClanChatRoom _JoinClanChatRoom;
		
		#endregion
		internal async Task<JoinClanChatRoomCompletionResult_t?> JoinClanChatRoom( SteamId steamIDClan )
		{
			return await (new Result<JoinClanChatRoomCompletionResult_t>( _JoinClanChatRoom( Self, steamIDClan ) )).GetResult();
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FLeaveClanChatRoom( IntPtr self, SteamId steamIDClan );
		private FLeaveClanChatRoom _LeaveClanChatRoom;
		
		#endregion
		internal bool LeaveClanChatRoom( SteamId steamIDClan )
		{
			return _LeaveClanChatRoom( Self, steamIDClan );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate int FGetClanChatMemberCount( IntPtr self, SteamId steamIDClan );
		private FGetClanChatMemberCount _GetClanChatMemberCount;
		
		#endregion
		internal int GetClanChatMemberCount( SteamId steamIDClan )
		{
			return _GetClanChatMemberCount( Self, steamIDClan );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate void FGetChatMemberByIndex( IntPtr self, ref SteamId retVal, SteamId steamIDClan, int iUser );
		private FGetChatMemberByIndex _GetChatMemberByIndex;
		
		#endregion
		internal SteamId GetChatMemberByIndex( SteamId steamIDClan, int iUser )
		{
			var retVal = default( SteamId );
			_GetChatMemberByIndex( Self, ref retVal, steamIDClan, iUser );
			return retVal;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FSendClanChatMessage( IntPtr self, SteamId steamIDClanChat, string pchText );
		private FSendClanChatMessage _SendClanChatMessage;
		
		#endregion
		internal bool SendClanChatMessage( SteamId steamIDClanChat, string pchText )
		{
			return _SendClanChatMessage( Self, steamIDClanChat, pchText );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate int FGetClanChatMessage( IntPtr self, SteamId steamIDClanChat, int iMessage, IntPtr prgchText, int cchTextMax, ref ChatEntryType peChatEntryType, ref SteamId psteamidChatter );
		private FGetClanChatMessage _GetClanChatMessage;
		
		#endregion
		internal int GetClanChatMessage( SteamId steamIDClanChat, int iMessage, IntPtr prgchText, int cchTextMax, ref ChatEntryType peChatEntryType, ref SteamId psteamidChatter )
		{
			return _GetClanChatMessage( Self, steamIDClanChat, iMessage, prgchText, cchTextMax, ref peChatEntryType, ref psteamidChatter );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FIsClanChatAdmin( IntPtr self, SteamId steamIDClanChat, SteamId steamIDUser );
		private FIsClanChatAdmin _IsClanChatAdmin;
		
		#endregion
		internal bool IsClanChatAdmin( SteamId steamIDClanChat, SteamId steamIDUser )
		{
			return _IsClanChatAdmin( Self, steamIDClanChat, steamIDUser );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FIsClanChatWindowOpenInSteam( IntPtr self, SteamId steamIDClanChat );
		private FIsClanChatWindowOpenInSteam _IsClanChatWindowOpenInSteam;
		
		#endregion
		internal bool IsClanChatWindowOpenInSteam( SteamId steamIDClanChat )
		{
			return _IsClanChatWindowOpenInSteam( Self, steamIDClanChat );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FOpenClanChatWindowInSteam( IntPtr self, SteamId steamIDClanChat );
		private FOpenClanChatWindowInSteam _OpenClanChatWindowInSteam;
		
		#endregion
		internal bool OpenClanChatWindowInSteam( SteamId steamIDClanChat )
		{
			return _OpenClanChatWindowInSteam( Self, steamIDClanChat );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FCloseClanChatWindowInSteam( IntPtr self, SteamId steamIDClanChat );
		private FCloseClanChatWindowInSteam _CloseClanChatWindowInSteam;
		
		#endregion
		internal bool CloseClanChatWindowInSteam( SteamId steamIDClanChat )
		{
			return _CloseClanChatWindowInSteam( Self, steamIDClanChat );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FSetListenForFriendsMessages( IntPtr self, [MarshalAs( UnmanagedType.U1 )] bool bInterceptEnabled );
		private FSetListenForFriendsMessages _SetListenForFriendsMessages;
		
		#endregion
		internal bool SetListenForFriendsMessages( [MarshalAs( UnmanagedType.U1 )] bool bInterceptEnabled )
		{
			return _SetListenForFriendsMessages( Self, bInterceptEnabled );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FReplyToFriendMessage( IntPtr self, SteamId steamIDFriend, string pchMsgToSend );
		private FReplyToFriendMessage _ReplyToFriendMessage;
		
		#endregion
		internal bool ReplyToFriendMessage( SteamId steamIDFriend, string pchMsgToSend )
		{
			return _ReplyToFriendMessage( Self, steamIDFriend, pchMsgToSend );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate int FGetFriendMessage( IntPtr self, SteamId steamIDFriend, int iMessageID, IntPtr pvData, int cubData, ref ChatEntryType peChatEntryType );
		private FGetFriendMessage _GetFriendMessage;
		
		#endregion
		internal int GetFriendMessage( SteamId steamIDFriend, int iMessageID, IntPtr pvData, int cubData, ref ChatEntryType peChatEntryType )
		{
			return _GetFriendMessage( Self, steamIDFriend, iMessageID, pvData, cubData, ref peChatEntryType );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate SteamAPICall_t FGetFollowerCount( IntPtr self, SteamId steamID );
		private FGetFollowerCount _GetFollowerCount;
		
		#endregion
		internal async Task<FriendsGetFollowerCount_t?> GetFollowerCount( SteamId steamID )
		{
			return await (new Result<FriendsGetFollowerCount_t>( _GetFollowerCount( Self, steamID ) )).GetResult();
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate SteamAPICall_t FIsFollowing( IntPtr self, SteamId steamID );
		private FIsFollowing _IsFollowing;
		
		#endregion
		internal async Task<FriendsIsFollowing_t?> IsFollowing( SteamId steamID )
		{
			return await (new Result<FriendsIsFollowing_t>( _IsFollowing( Self, steamID ) )).GetResult();
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate SteamAPICall_t FEnumerateFollowingList( IntPtr self, uint unStartIndex );
		private FEnumerateFollowingList _EnumerateFollowingList;
		
		#endregion
		internal async Task<FriendsEnumerateFollowingList_t?> EnumerateFollowingList( uint unStartIndex )
		{
			return await (new Result<FriendsEnumerateFollowingList_t>( _EnumerateFollowingList( Self, unStartIndex ) )).GetResult();
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FIsClanPublic( IntPtr self, SteamId steamIDClan );
		private FIsClanPublic _IsClanPublic;
		
		#endregion
		internal bool IsClanPublic( SteamId steamIDClan )
		{
			return _IsClanPublic( Self, steamIDClan );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FIsClanOfficialGameGroup( IntPtr self, SteamId steamIDClan );
		private FIsClanOfficialGameGroup _IsClanOfficialGameGroup;
		
		#endregion
		internal bool IsClanOfficialGameGroup( SteamId steamIDClan )
		{
			return _IsClanOfficialGameGroup( Self, steamIDClan );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate int FGetNumChatsWithUnreadPriorityMessages( IntPtr self );
		private FGetNumChatsWithUnreadPriorityMessages _GetNumChatsWithUnreadPriorityMessages;
		
		#endregion
		internal int GetNumChatsWithUnreadPriorityMessages()
		{
			return _GetNumChatsWithUnreadPriorityMessages( Self );
		}
		
	}
}
