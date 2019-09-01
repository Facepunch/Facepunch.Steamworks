using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Steamworks.Data;


namespace Steamworks
{
	internal class ISteamFriends : SteamInterface
	{
		public override string InterfaceName => "SteamFriends017";
		
		public override void InitInternals()
		{
			_GetPersonaName = Marshal.GetDelegateForFunctionPointer<FGetPersonaName>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 0 ) ) );
			_SetPersonaName = Marshal.GetDelegateForFunctionPointer<FSetPersonaName>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 8 ) ) );
			_GetPersonaState = Marshal.GetDelegateForFunctionPointer<FGetPersonaState>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 16 ) ) );
			_GetFriendCount = Marshal.GetDelegateForFunctionPointer<FGetFriendCount>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 24 ) ) );
			_GetFriendByIndex = Marshal.GetDelegateForFunctionPointer<FGetFriendByIndex>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 32 ) ) );
			_GetFriendRelationship = Marshal.GetDelegateForFunctionPointer<FGetFriendRelationship>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 40 ) ) );
			_GetFriendPersonaState = Marshal.GetDelegateForFunctionPointer<FGetFriendPersonaState>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 48 ) ) );
			_GetFriendPersonaName = Marshal.GetDelegateForFunctionPointer<FGetFriendPersonaName>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 56 ) ) );
			_GetFriendGamePlayed = Marshal.GetDelegateForFunctionPointer<FGetFriendGamePlayed>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 64 ) ) );
			_GetFriendPersonaNameHistory = Marshal.GetDelegateForFunctionPointer<FGetFriendPersonaNameHistory>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 72 ) ) );
			_GetFriendSteamLevel = Marshal.GetDelegateForFunctionPointer<FGetFriendSteamLevel>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 80 ) ) );
			_GetPlayerNickname = Marshal.GetDelegateForFunctionPointer<FGetPlayerNickname>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 88 ) ) );
			_GetFriendsGroupCount = Marshal.GetDelegateForFunctionPointer<FGetFriendsGroupCount>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 96 ) ) );
			_GetFriendsGroupIDByIndex = Marshal.GetDelegateForFunctionPointer<FGetFriendsGroupIDByIndex>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 104 ) ) );
			_GetFriendsGroupName = Marshal.GetDelegateForFunctionPointer<FGetFriendsGroupName>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 112 ) ) );
			_GetFriendsGroupMembersCount = Marshal.GetDelegateForFunctionPointer<FGetFriendsGroupMembersCount>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 120 ) ) );
			_GetFriendsGroupMembersList = Marshal.GetDelegateForFunctionPointer<FGetFriendsGroupMembersList>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 128 ) ) );
			_HasFriend = Marshal.GetDelegateForFunctionPointer<FHasFriend>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 136 ) ) );
			_GetClanCount = Marshal.GetDelegateForFunctionPointer<FGetClanCount>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 144 ) ) );
			_GetClanByIndex = Marshal.GetDelegateForFunctionPointer<FGetClanByIndex>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 152 ) ) );
			_GetClanName = Marshal.GetDelegateForFunctionPointer<FGetClanName>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 160 ) ) );
			_GetClanTag = Marshal.GetDelegateForFunctionPointer<FGetClanTag>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 168 ) ) );
			_GetClanActivityCounts = Marshal.GetDelegateForFunctionPointer<FGetClanActivityCounts>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 176 ) ) );
			_DownloadClanActivityCounts = Marshal.GetDelegateForFunctionPointer<FDownloadClanActivityCounts>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 184 ) ) );
			_GetFriendCountFromSource = Marshal.GetDelegateForFunctionPointer<FGetFriendCountFromSource>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 192 ) ) );
			_GetFriendFromSourceByIndex = Marshal.GetDelegateForFunctionPointer<FGetFriendFromSourceByIndex>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 200 ) ) );
			_IsUserInSource = Marshal.GetDelegateForFunctionPointer<FIsUserInSource>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 208 ) ) );
			_SetInGameVoiceSpeaking = Marshal.GetDelegateForFunctionPointer<FSetInGameVoiceSpeaking>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 216 ) ) );
			_ActivateGameOverlay = Marshal.GetDelegateForFunctionPointer<FActivateGameOverlay>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 224 ) ) );
			_ActivateGameOverlayToUser = Marshal.GetDelegateForFunctionPointer<FActivateGameOverlayToUser>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 232 ) ) );
			_ActivateGameOverlayToWebPage = Marshal.GetDelegateForFunctionPointer<FActivateGameOverlayToWebPage>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 240 ) ) );
			_ActivateGameOverlayToStore = Marshal.GetDelegateForFunctionPointer<FActivateGameOverlayToStore>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 248 ) ) );
			_SetPlayedWith = Marshal.GetDelegateForFunctionPointer<FSetPlayedWith>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 256 ) ) );
			_ActivateGameOverlayInviteDialog = Marshal.GetDelegateForFunctionPointer<FActivateGameOverlayInviteDialog>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 264 ) ) );
			_GetSmallFriendAvatar = Marshal.GetDelegateForFunctionPointer<FGetSmallFriendAvatar>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 272 ) ) );
			_GetMediumFriendAvatar = Marshal.GetDelegateForFunctionPointer<FGetMediumFriendAvatar>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 280 ) ) );
			_GetLargeFriendAvatar = Marshal.GetDelegateForFunctionPointer<FGetLargeFriendAvatar>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 288 ) ) );
			_RequestUserInformation = Marshal.GetDelegateForFunctionPointer<FRequestUserInformation>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 296 ) ) );
			_RequestClanOfficerList = Marshal.GetDelegateForFunctionPointer<FRequestClanOfficerList>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 304 ) ) );
			_GetClanOwner = Marshal.GetDelegateForFunctionPointer<FGetClanOwner>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 312 ) ) );
			_GetClanOfficerCount = Marshal.GetDelegateForFunctionPointer<FGetClanOfficerCount>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 320 ) ) );
			_GetClanOfficerByIndex = Marshal.GetDelegateForFunctionPointer<FGetClanOfficerByIndex>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 328 ) ) );
			_GetUserRestrictions = Marshal.GetDelegateForFunctionPointer<FGetUserRestrictions>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 336 ) ) );
			_SetRichPresence = Marshal.GetDelegateForFunctionPointer<FSetRichPresence>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 344 ) ) );
			_ClearRichPresence = Marshal.GetDelegateForFunctionPointer<FClearRichPresence>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 352 ) ) );
			_GetFriendRichPresence = Marshal.GetDelegateForFunctionPointer<FGetFriendRichPresence>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 360 ) ) );
			_GetFriendRichPresenceKeyCount = Marshal.GetDelegateForFunctionPointer<FGetFriendRichPresenceKeyCount>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 368 ) ) );
			_GetFriendRichPresenceKeyByIndex = Marshal.GetDelegateForFunctionPointer<FGetFriendRichPresenceKeyByIndex>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 376 ) ) );
			_RequestFriendRichPresence = Marshal.GetDelegateForFunctionPointer<FRequestFriendRichPresence>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 384 ) ) );
			_InviteUserToGame = Marshal.GetDelegateForFunctionPointer<FInviteUserToGame>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 392 ) ) );
			_GetCoplayFriendCount = Marshal.GetDelegateForFunctionPointer<FGetCoplayFriendCount>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 400 ) ) );
			_GetCoplayFriend = Marshal.GetDelegateForFunctionPointer<FGetCoplayFriend>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 408 ) ) );
			_GetFriendCoplayTime = Marshal.GetDelegateForFunctionPointer<FGetFriendCoplayTime>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 416 ) ) );
			_GetFriendCoplayGame = Marshal.GetDelegateForFunctionPointer<FGetFriendCoplayGame>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 424 ) ) );
			_JoinClanChatRoom = Marshal.GetDelegateForFunctionPointer<FJoinClanChatRoom>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 432 ) ) );
			_LeaveClanChatRoom = Marshal.GetDelegateForFunctionPointer<FLeaveClanChatRoom>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 440 ) ) );
			_GetClanChatMemberCount = Marshal.GetDelegateForFunctionPointer<FGetClanChatMemberCount>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 448 ) ) );
			_GetChatMemberByIndex = Marshal.GetDelegateForFunctionPointer<FGetChatMemberByIndex>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 456 ) ) );
			_SendClanChatMessage = Marshal.GetDelegateForFunctionPointer<FSendClanChatMessage>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 464 ) ) );
			_GetClanChatMessage = Marshal.GetDelegateForFunctionPointer<FGetClanChatMessage>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 472 ) ) );
			_IsClanChatAdmin = Marshal.GetDelegateForFunctionPointer<FIsClanChatAdmin>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 480 ) ) );
			_IsClanChatWindowOpenInSteam = Marshal.GetDelegateForFunctionPointer<FIsClanChatWindowOpenInSteam>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 488 ) ) );
			_OpenClanChatWindowInSteam = Marshal.GetDelegateForFunctionPointer<FOpenClanChatWindowInSteam>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 496 ) ) );
			_CloseClanChatWindowInSteam = Marshal.GetDelegateForFunctionPointer<FCloseClanChatWindowInSteam>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 504 ) ) );
			_SetListenForFriendsMessages = Marshal.GetDelegateForFunctionPointer<FSetListenForFriendsMessages>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 512 ) ) );
			_ReplyToFriendMessage = Marshal.GetDelegateForFunctionPointer<FReplyToFriendMessage>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 520 ) ) );
			_GetFriendMessage = Marshal.GetDelegateForFunctionPointer<FGetFriendMessage>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 528 ) ) );
			_GetFollowerCount = Marshal.GetDelegateForFunctionPointer<FGetFollowerCount>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 536 ) ) );
			_IsFollowing = Marshal.GetDelegateForFunctionPointer<FIsFollowing>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 544 ) ) );
			_EnumerateFollowingList = Marshal.GetDelegateForFunctionPointer<FEnumerateFollowingList>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 552 ) ) );
			_IsClanPublic = Marshal.GetDelegateForFunctionPointer<FIsClanPublic>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 560 ) ) );
			_IsClanOfficialGameGroup = Marshal.GetDelegateForFunctionPointer<FIsClanOfficialGameGroup>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 568 ) ) );
			_GetNumChatsWithUnreadPriorityMessages = Marshal.GetDelegateForFunctionPointer<FGetNumChatsWithUnreadPriorityMessages>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 576 ) ) );
		}
		internal override void Shutdown()
		{
			base.Shutdown();
			
			_GetPersonaName = null;
			_SetPersonaName = null;
			_GetPersonaState = null;
			_GetFriendCount = null;
			_GetFriendByIndex = null;
			_GetFriendRelationship = null;
			_GetFriendPersonaState = null;
			_GetFriendPersonaName = null;
			_GetFriendGamePlayed = null;
			_GetFriendPersonaNameHistory = null;
			_GetFriendSteamLevel = null;
			_GetPlayerNickname = null;
			_GetFriendsGroupCount = null;
			_GetFriendsGroupIDByIndex = null;
			_GetFriendsGroupName = null;
			_GetFriendsGroupMembersCount = null;
			_GetFriendsGroupMembersList = null;
			_HasFriend = null;
			_GetClanCount = null;
			_GetClanByIndex = null;
			_GetClanName = null;
			_GetClanTag = null;
			_GetClanActivityCounts = null;
			_DownloadClanActivityCounts = null;
			_GetFriendCountFromSource = null;
			_GetFriendFromSourceByIndex = null;
			_IsUserInSource = null;
			_SetInGameVoiceSpeaking = null;
			_ActivateGameOverlay = null;
			_ActivateGameOverlayToUser = null;
			_ActivateGameOverlayToWebPage = null;
			_ActivateGameOverlayToStore = null;
			_SetPlayedWith = null;
			_ActivateGameOverlayInviteDialog = null;
			_GetSmallFriendAvatar = null;
			_GetMediumFriendAvatar = null;
			_GetLargeFriendAvatar = null;
			_RequestUserInformation = null;
			_RequestClanOfficerList = null;
			_GetClanOwner = null;
			_GetClanOfficerCount = null;
			_GetClanOfficerByIndex = null;
			_GetUserRestrictions = null;
			_SetRichPresence = null;
			_ClearRichPresence = null;
			_GetFriendRichPresence = null;
			_GetFriendRichPresenceKeyCount = null;
			_GetFriendRichPresenceKeyByIndex = null;
			_RequestFriendRichPresence = null;
			_InviteUserToGame = null;
			_GetCoplayFriendCount = null;
			_GetCoplayFriend = null;
			_GetFriendCoplayTime = null;
			_GetFriendCoplayGame = null;
			_JoinClanChatRoom = null;
			_LeaveClanChatRoom = null;
			_GetClanChatMemberCount = null;
			_GetChatMemberByIndex = null;
			_SendClanChatMessage = null;
			_GetClanChatMessage = null;
			_IsClanChatAdmin = null;
			_IsClanChatWindowOpenInSteam = null;
			_OpenClanChatWindowInSteam = null;
			_CloseClanChatWindowInSteam = null;
			_SetListenForFriendsMessages = null;
			_ReplyToFriendMessage = null;
			_GetFriendMessage = null;
			_GetFollowerCount = null;
			_IsFollowing = null;
			_EnumerateFollowingList = null;
			_IsClanPublic = null;
			_IsClanOfficialGameGroup = null;
			_GetNumChatsWithUnreadPriorityMessages = null;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate Utf8StringPointer FGetPersonaName( IntPtr self );
		private FGetPersonaName _GetPersonaName;
		
		#endregion
		internal string GetPersonaName()
		{
			var returnValue = _GetPersonaName( Self );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate SteamAPICall_t FSetPersonaName( IntPtr self, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchPersonaName );
		private FSetPersonaName _SetPersonaName;
		
		#endregion
		internal async Task<SetPersonaNameResponse_t?> SetPersonaName( [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchPersonaName )
		{
			var returnValue = _SetPersonaName( Self, pchPersonaName );
			return await SetPersonaNameResponse_t.GetResultAsync( returnValue );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate FriendState FGetPersonaState( IntPtr self );
		private FGetPersonaState _GetPersonaState;
		
		#endregion
		internal FriendState GetPersonaState()
		{
			var returnValue = _GetPersonaState( Self );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate int FGetFriendCount( IntPtr self, int iFriendFlags );
		private FGetFriendCount _GetFriendCount;
		
		#endregion
		internal int GetFriendCount( int iFriendFlags )
		{
			var returnValue = _GetFriendCount( Self, iFriendFlags );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		#if PLATFORM_WIN
		private delegate void FGetFriendByIndex( IntPtr self, ref SteamId retVal, int iFriend, int iFriendFlags );
		#else
		private delegate SteamId FGetFriendByIndex( IntPtr self, int iFriend, int iFriendFlags );
		#endif
		private FGetFriendByIndex _GetFriendByIndex;
		
		#endregion
		internal SteamId GetFriendByIndex( int iFriend, int iFriendFlags )
		{
			#if PLATFORM_WIN
			var retVal = default( SteamId );
			_GetFriendByIndex( Self, ref retVal, iFriend, iFriendFlags );
			return retVal;
			#else
			var returnValue = _GetFriendByIndex( Self, iFriend, iFriendFlags );
			return returnValue;
			#endif
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate Relationship FGetFriendRelationship( IntPtr self, SteamId steamIDFriend );
		private FGetFriendRelationship _GetFriendRelationship;
		
		#endregion
		internal Relationship GetFriendRelationship( SteamId steamIDFriend )
		{
			var returnValue = _GetFriendRelationship( Self, steamIDFriend );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate FriendState FGetFriendPersonaState( IntPtr self, SteamId steamIDFriend );
		private FGetFriendPersonaState _GetFriendPersonaState;
		
		#endregion
		internal FriendState GetFriendPersonaState( SteamId steamIDFriend )
		{
			var returnValue = _GetFriendPersonaState( Self, steamIDFriend );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate Utf8StringPointer FGetFriendPersonaName( IntPtr self, SteamId steamIDFriend );
		private FGetFriendPersonaName _GetFriendPersonaName;
		
		#endregion
		internal string GetFriendPersonaName( SteamId steamIDFriend )
		{
			var returnValue = _GetFriendPersonaName( Self, steamIDFriend );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FGetFriendGamePlayed( IntPtr self, SteamId steamIDFriend, ref FriendGameInfo_t pFriendGameInfo );
		private FGetFriendGamePlayed _GetFriendGamePlayed;
		
		#endregion
		internal bool GetFriendGamePlayed( SteamId steamIDFriend, ref FriendGameInfo_t pFriendGameInfo )
		{
			var returnValue = _GetFriendGamePlayed( Self, steamIDFriend, ref pFriendGameInfo );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate Utf8StringPointer FGetFriendPersonaNameHistory( IntPtr self, SteamId steamIDFriend, int iPersonaName );
		private FGetFriendPersonaNameHistory _GetFriendPersonaNameHistory;
		
		#endregion
		internal string GetFriendPersonaNameHistory( SteamId steamIDFriend, int iPersonaName )
		{
			var returnValue = _GetFriendPersonaNameHistory( Self, steamIDFriend, iPersonaName );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate int FGetFriendSteamLevel( IntPtr self, SteamId steamIDFriend );
		private FGetFriendSteamLevel _GetFriendSteamLevel;
		
		#endregion
		internal int GetFriendSteamLevel( SteamId steamIDFriend )
		{
			var returnValue = _GetFriendSteamLevel( Self, steamIDFriend );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate Utf8StringPointer FGetPlayerNickname( IntPtr self, SteamId steamIDPlayer );
		private FGetPlayerNickname _GetPlayerNickname;
		
		#endregion
		internal string GetPlayerNickname( SteamId steamIDPlayer )
		{
			var returnValue = _GetPlayerNickname( Self, steamIDPlayer );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate int FGetFriendsGroupCount( IntPtr self );
		private FGetFriendsGroupCount _GetFriendsGroupCount;
		
		#endregion
		internal int GetFriendsGroupCount()
		{
			var returnValue = _GetFriendsGroupCount( Self );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate FriendsGroupID_t FGetFriendsGroupIDByIndex( IntPtr self, int iFG );
		private FGetFriendsGroupIDByIndex _GetFriendsGroupIDByIndex;
		
		#endregion
		internal FriendsGroupID_t GetFriendsGroupIDByIndex( int iFG )
		{
			var returnValue = _GetFriendsGroupIDByIndex( Self, iFG );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate Utf8StringPointer FGetFriendsGroupName( IntPtr self, FriendsGroupID_t friendsGroupID );
		private FGetFriendsGroupName _GetFriendsGroupName;
		
		#endregion
		internal string GetFriendsGroupName( FriendsGroupID_t friendsGroupID )
		{
			var returnValue = _GetFriendsGroupName( Self, friendsGroupID );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate int FGetFriendsGroupMembersCount( IntPtr self, FriendsGroupID_t friendsGroupID );
		private FGetFriendsGroupMembersCount _GetFriendsGroupMembersCount;
		
		#endregion
		internal int GetFriendsGroupMembersCount( FriendsGroupID_t friendsGroupID )
		{
			var returnValue = _GetFriendsGroupMembersCount( Self, friendsGroupID );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate void FGetFriendsGroupMembersList( IntPtr self, FriendsGroupID_t friendsGroupID, [In,Out] SteamId[]  pOutSteamIDMembers, int nMembersCount );
		private FGetFriendsGroupMembersList _GetFriendsGroupMembersList;
		
		#endregion
		internal void GetFriendsGroupMembersList( FriendsGroupID_t friendsGroupID, [In,Out] SteamId[]  pOutSteamIDMembers, int nMembersCount )
		{
			_GetFriendsGroupMembersList( Self, friendsGroupID, pOutSteamIDMembers, nMembersCount );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FHasFriend( IntPtr self, SteamId steamIDFriend, int iFriendFlags );
		private FHasFriend _HasFriend;
		
		#endregion
		internal bool HasFriend( SteamId steamIDFriend, int iFriendFlags )
		{
			var returnValue = _HasFriend( Self, steamIDFriend, iFriendFlags );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate int FGetClanCount( IntPtr self );
		private FGetClanCount _GetClanCount;
		
		#endregion
		internal int GetClanCount()
		{
			var returnValue = _GetClanCount( Self );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		#if PLATFORM_WIN
		private delegate void FGetClanByIndex( IntPtr self, ref SteamId retVal, int iClan );
		#else
		private delegate SteamId FGetClanByIndex( IntPtr self, int iClan );
		#endif
		private FGetClanByIndex _GetClanByIndex;
		
		#endregion
		internal SteamId GetClanByIndex( int iClan )
		{
			#if PLATFORM_WIN
			var retVal = default( SteamId );
			_GetClanByIndex( Self, ref retVal, iClan );
			return retVal;
			#else
			var returnValue = _GetClanByIndex( Self, iClan );
			return returnValue;
			#endif
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate Utf8StringPointer FGetClanName( IntPtr self, SteamId steamIDClan );
		private FGetClanName _GetClanName;
		
		#endregion
		internal string GetClanName( SteamId steamIDClan )
		{
			var returnValue = _GetClanName( Self, steamIDClan );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate Utf8StringPointer FGetClanTag( IntPtr self, SteamId steamIDClan );
		private FGetClanTag _GetClanTag;
		
		#endregion
		internal string GetClanTag( SteamId steamIDClan )
		{
			var returnValue = _GetClanTag( Self, steamIDClan );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FGetClanActivityCounts( IntPtr self, SteamId steamIDClan, ref int pnOnline, ref int pnInGame, ref int pnChatting );
		private FGetClanActivityCounts _GetClanActivityCounts;
		
		#endregion
		internal bool GetClanActivityCounts( SteamId steamIDClan, ref int pnOnline, ref int pnInGame, ref int pnChatting )
		{
			var returnValue = _GetClanActivityCounts( Self, steamIDClan, ref pnOnline, ref pnInGame, ref pnChatting );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate SteamAPICall_t FDownloadClanActivityCounts( IntPtr self, [In,Out] SteamId[]  psteamIDClans, int cClansToRequest );
		private FDownloadClanActivityCounts _DownloadClanActivityCounts;
		
		#endregion
		internal async Task<DownloadClanActivityCountsResult_t?> DownloadClanActivityCounts( [In,Out] SteamId[]  psteamIDClans, int cClansToRequest )
		{
			var returnValue = _DownloadClanActivityCounts( Self, psteamIDClans, cClansToRequest );
			return await DownloadClanActivityCountsResult_t.GetResultAsync( returnValue );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate int FGetFriendCountFromSource( IntPtr self, SteamId steamIDSource );
		private FGetFriendCountFromSource _GetFriendCountFromSource;
		
		#endregion
		internal int GetFriendCountFromSource( SteamId steamIDSource )
		{
			var returnValue = _GetFriendCountFromSource( Self, steamIDSource );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		#if PLATFORM_WIN
		private delegate void FGetFriendFromSourceByIndex( IntPtr self, ref SteamId retVal, SteamId steamIDSource, int iFriend );
		#else
		private delegate SteamId FGetFriendFromSourceByIndex( IntPtr self, SteamId steamIDSource, int iFriend );
		#endif
		private FGetFriendFromSourceByIndex _GetFriendFromSourceByIndex;
		
		#endregion
		internal SteamId GetFriendFromSourceByIndex( SteamId steamIDSource, int iFriend )
		{
			#if PLATFORM_WIN
			var retVal = default( SteamId );
			_GetFriendFromSourceByIndex( Self, ref retVal, steamIDSource, iFriend );
			return retVal;
			#else
			var returnValue = _GetFriendFromSourceByIndex( Self, steamIDSource, iFriend );
			return returnValue;
			#endif
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FIsUserInSource( IntPtr self, SteamId steamIDUser, SteamId steamIDSource );
		private FIsUserInSource _IsUserInSource;
		
		#endregion
		internal bool IsUserInSource( SteamId steamIDUser, SteamId steamIDSource )
		{
			var returnValue = _IsUserInSource( Self, steamIDUser, steamIDSource );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate void FSetInGameVoiceSpeaking( IntPtr self, SteamId steamIDUser, [MarshalAs( UnmanagedType.U1 )] bool bSpeaking );
		private FSetInGameVoiceSpeaking _SetInGameVoiceSpeaking;
		
		#endregion
		internal void SetInGameVoiceSpeaking( SteamId steamIDUser, [MarshalAs( UnmanagedType.U1 )] bool bSpeaking )
		{
			_SetInGameVoiceSpeaking( Self, steamIDUser, bSpeaking );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate void FActivateGameOverlay( IntPtr self, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchDialog );
		private FActivateGameOverlay _ActivateGameOverlay;
		
		#endregion
		internal void ActivateGameOverlay( [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchDialog )
		{
			_ActivateGameOverlay( Self, pchDialog );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate void FActivateGameOverlayToUser( IntPtr self, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchDialog, SteamId steamID );
		private FActivateGameOverlayToUser _ActivateGameOverlayToUser;
		
		#endregion
		internal void ActivateGameOverlayToUser( [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchDialog, SteamId steamID )
		{
			_ActivateGameOverlayToUser( Self, pchDialog, steamID );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate void FActivateGameOverlayToWebPage( IntPtr self, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchURL, ActivateGameOverlayToWebPageMode eMode );
		private FActivateGameOverlayToWebPage _ActivateGameOverlayToWebPage;
		
		#endregion
		internal void ActivateGameOverlayToWebPage( [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchURL, ActivateGameOverlayToWebPageMode eMode )
		{
			_ActivateGameOverlayToWebPage( Self, pchURL, eMode );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate void FActivateGameOverlayToStore( IntPtr self, AppId nAppID, OverlayToStoreFlag eFlag );
		private FActivateGameOverlayToStore _ActivateGameOverlayToStore;
		
		#endregion
		internal void ActivateGameOverlayToStore( AppId nAppID, OverlayToStoreFlag eFlag )
		{
			_ActivateGameOverlayToStore( Self, nAppID, eFlag );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate void FSetPlayedWith( IntPtr self, SteamId steamIDUserPlayedWith );
		private FSetPlayedWith _SetPlayedWith;
		
		#endregion
		internal void SetPlayedWith( SteamId steamIDUserPlayedWith )
		{
			_SetPlayedWith( Self, steamIDUserPlayedWith );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate void FActivateGameOverlayInviteDialog( IntPtr self, SteamId steamIDLobby );
		private FActivateGameOverlayInviteDialog _ActivateGameOverlayInviteDialog;
		
		#endregion
		internal void ActivateGameOverlayInviteDialog( SteamId steamIDLobby )
		{
			_ActivateGameOverlayInviteDialog( Self, steamIDLobby );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate int FGetSmallFriendAvatar( IntPtr self, SteamId steamIDFriend );
		private FGetSmallFriendAvatar _GetSmallFriendAvatar;
		
		#endregion
		internal int GetSmallFriendAvatar( SteamId steamIDFriend )
		{
			var returnValue = _GetSmallFriendAvatar( Self, steamIDFriend );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate int FGetMediumFriendAvatar( IntPtr self, SteamId steamIDFriend );
		private FGetMediumFriendAvatar _GetMediumFriendAvatar;
		
		#endregion
		internal int GetMediumFriendAvatar( SteamId steamIDFriend )
		{
			var returnValue = _GetMediumFriendAvatar( Self, steamIDFriend );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate int FGetLargeFriendAvatar( IntPtr self, SteamId steamIDFriend );
		private FGetLargeFriendAvatar _GetLargeFriendAvatar;
		
		#endregion
		internal int GetLargeFriendAvatar( SteamId steamIDFriend )
		{
			var returnValue = _GetLargeFriendAvatar( Self, steamIDFriend );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FRequestUserInformation( IntPtr self, SteamId steamIDUser, [MarshalAs( UnmanagedType.U1 )] bool bRequireNameOnly );
		private FRequestUserInformation _RequestUserInformation;
		
		#endregion
		internal bool RequestUserInformation( SteamId steamIDUser, [MarshalAs( UnmanagedType.U1 )] bool bRequireNameOnly )
		{
			var returnValue = _RequestUserInformation( Self, steamIDUser, bRequireNameOnly );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate SteamAPICall_t FRequestClanOfficerList( IntPtr self, SteamId steamIDClan );
		private FRequestClanOfficerList _RequestClanOfficerList;
		
		#endregion
		internal async Task<ClanOfficerListResponse_t?> RequestClanOfficerList( SteamId steamIDClan )
		{
			var returnValue = _RequestClanOfficerList( Self, steamIDClan );
			return await ClanOfficerListResponse_t.GetResultAsync( returnValue );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		#if PLATFORM_WIN
		private delegate void FGetClanOwner( IntPtr self, ref SteamId retVal, SteamId steamIDClan );
		#else
		private delegate SteamId FGetClanOwner( IntPtr self, SteamId steamIDClan );
		#endif
		private FGetClanOwner _GetClanOwner;
		
		#endregion
		internal SteamId GetClanOwner( SteamId steamIDClan )
		{
			#if PLATFORM_WIN
			var retVal = default( SteamId );
			_GetClanOwner( Self, ref retVal, steamIDClan );
			return retVal;
			#else
			var returnValue = _GetClanOwner( Self, steamIDClan );
			return returnValue;
			#endif
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate int FGetClanOfficerCount( IntPtr self, SteamId steamIDClan );
		private FGetClanOfficerCount _GetClanOfficerCount;
		
		#endregion
		internal int GetClanOfficerCount( SteamId steamIDClan )
		{
			var returnValue = _GetClanOfficerCount( Self, steamIDClan );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		#if PLATFORM_WIN
		private delegate void FGetClanOfficerByIndex( IntPtr self, ref SteamId retVal, SteamId steamIDClan, int iOfficer );
		#else
		private delegate SteamId FGetClanOfficerByIndex( IntPtr self, SteamId steamIDClan, int iOfficer );
		#endif
		private FGetClanOfficerByIndex _GetClanOfficerByIndex;
		
		#endregion
		internal SteamId GetClanOfficerByIndex( SteamId steamIDClan, int iOfficer )
		{
			#if PLATFORM_WIN
			var retVal = default( SteamId );
			_GetClanOfficerByIndex( Self, ref retVal, steamIDClan, iOfficer );
			return retVal;
			#else
			var returnValue = _GetClanOfficerByIndex( Self, steamIDClan, iOfficer );
			return returnValue;
			#endif
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate uint FGetUserRestrictions( IntPtr self );
		private FGetUserRestrictions _GetUserRestrictions;
		
		#endregion
		internal uint GetUserRestrictions()
		{
			var returnValue = _GetUserRestrictions( Self );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FSetRichPresence( IntPtr self, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchKey, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchValue );
		private FSetRichPresence _SetRichPresence;
		
		#endregion
		internal bool SetRichPresence( [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchKey, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchValue )
		{
			var returnValue = _SetRichPresence( Self, pchKey, pchValue );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate void FClearRichPresence( IntPtr self );
		private FClearRichPresence _ClearRichPresence;
		
		#endregion
		internal void ClearRichPresence()
		{
			_ClearRichPresence( Self );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate Utf8StringPointer FGetFriendRichPresence( IntPtr self, SteamId steamIDFriend, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchKey );
		private FGetFriendRichPresence _GetFriendRichPresence;
		
		#endregion
		internal string GetFriendRichPresence( SteamId steamIDFriend, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchKey )
		{
			var returnValue = _GetFriendRichPresence( Self, steamIDFriend, pchKey );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate int FGetFriendRichPresenceKeyCount( IntPtr self, SteamId steamIDFriend );
		private FGetFriendRichPresenceKeyCount _GetFriendRichPresenceKeyCount;
		
		#endregion
		internal int GetFriendRichPresenceKeyCount( SteamId steamIDFriend )
		{
			var returnValue = _GetFriendRichPresenceKeyCount( Self, steamIDFriend );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate Utf8StringPointer FGetFriendRichPresenceKeyByIndex( IntPtr self, SteamId steamIDFriend, int iKey );
		private FGetFriendRichPresenceKeyByIndex _GetFriendRichPresenceKeyByIndex;
		
		#endregion
		internal string GetFriendRichPresenceKeyByIndex( SteamId steamIDFriend, int iKey )
		{
			var returnValue = _GetFriendRichPresenceKeyByIndex( Self, steamIDFriend, iKey );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate void FRequestFriendRichPresence( IntPtr self, SteamId steamIDFriend );
		private FRequestFriendRichPresence _RequestFriendRichPresence;
		
		#endregion
		internal void RequestFriendRichPresence( SteamId steamIDFriend )
		{
			_RequestFriendRichPresence( Self, steamIDFriend );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FInviteUserToGame( IntPtr self, SteamId steamIDFriend, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchConnectString );
		private FInviteUserToGame _InviteUserToGame;
		
		#endregion
		internal bool InviteUserToGame( SteamId steamIDFriend, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchConnectString )
		{
			var returnValue = _InviteUserToGame( Self, steamIDFriend, pchConnectString );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate int FGetCoplayFriendCount( IntPtr self );
		private FGetCoplayFriendCount _GetCoplayFriendCount;
		
		#endregion
		internal int GetCoplayFriendCount()
		{
			var returnValue = _GetCoplayFriendCount( Self );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		#if PLATFORM_WIN
		private delegate void FGetCoplayFriend( IntPtr self, ref SteamId retVal, int iCoplayFriend );
		#else
		private delegate SteamId FGetCoplayFriend( IntPtr self, int iCoplayFriend );
		#endif
		private FGetCoplayFriend _GetCoplayFriend;
		
		#endregion
		internal SteamId GetCoplayFriend( int iCoplayFriend )
		{
			#if PLATFORM_WIN
			var retVal = default( SteamId );
			_GetCoplayFriend( Self, ref retVal, iCoplayFriend );
			return retVal;
			#else
			var returnValue = _GetCoplayFriend( Self, iCoplayFriend );
			return returnValue;
			#endif
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate int FGetFriendCoplayTime( IntPtr self, SteamId steamIDFriend );
		private FGetFriendCoplayTime _GetFriendCoplayTime;
		
		#endregion
		internal int GetFriendCoplayTime( SteamId steamIDFriend )
		{
			var returnValue = _GetFriendCoplayTime( Self, steamIDFriend );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate AppId FGetFriendCoplayGame( IntPtr self, SteamId steamIDFriend );
		private FGetFriendCoplayGame _GetFriendCoplayGame;
		
		#endregion
		internal AppId GetFriendCoplayGame( SteamId steamIDFriend )
		{
			var returnValue = _GetFriendCoplayGame( Self, steamIDFriend );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate SteamAPICall_t FJoinClanChatRoom( IntPtr self, SteamId steamIDClan );
		private FJoinClanChatRoom _JoinClanChatRoom;
		
		#endregion
		internal async Task<JoinClanChatRoomCompletionResult_t?> JoinClanChatRoom( SteamId steamIDClan )
		{
			var returnValue = _JoinClanChatRoom( Self, steamIDClan );
			return await JoinClanChatRoomCompletionResult_t.GetResultAsync( returnValue );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FLeaveClanChatRoom( IntPtr self, SteamId steamIDClan );
		private FLeaveClanChatRoom _LeaveClanChatRoom;
		
		#endregion
		internal bool LeaveClanChatRoom( SteamId steamIDClan )
		{
			var returnValue = _LeaveClanChatRoom( Self, steamIDClan );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate int FGetClanChatMemberCount( IntPtr self, SteamId steamIDClan );
		private FGetClanChatMemberCount _GetClanChatMemberCount;
		
		#endregion
		internal int GetClanChatMemberCount( SteamId steamIDClan )
		{
			var returnValue = _GetClanChatMemberCount( Self, steamIDClan );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		#if PLATFORM_WIN
		private delegate void FGetChatMemberByIndex( IntPtr self, ref SteamId retVal, SteamId steamIDClan, int iUser );
		#else
		private delegate SteamId FGetChatMemberByIndex( IntPtr self, SteamId steamIDClan, int iUser );
		#endif
		private FGetChatMemberByIndex _GetChatMemberByIndex;
		
		#endregion
		internal SteamId GetChatMemberByIndex( SteamId steamIDClan, int iUser )
		{
			#if PLATFORM_WIN
			var retVal = default( SteamId );
			_GetChatMemberByIndex( Self, ref retVal, steamIDClan, iUser );
			return retVal;
			#else
			var returnValue = _GetChatMemberByIndex( Self, steamIDClan, iUser );
			return returnValue;
			#endif
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FSendClanChatMessage( IntPtr self, SteamId steamIDClanChat, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchText );
		private FSendClanChatMessage _SendClanChatMessage;
		
		#endregion
		internal bool SendClanChatMessage( SteamId steamIDClanChat, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchText )
		{
			var returnValue = _SendClanChatMessage( Self, steamIDClanChat, pchText );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate int FGetClanChatMessage( IntPtr self, SteamId steamIDClanChat, int iMessage, IntPtr prgchText, int cchTextMax, ref ChatEntryType peChatEntryType, ref SteamId psteamidChatter );
		private FGetClanChatMessage _GetClanChatMessage;
		
		#endregion
		internal int GetClanChatMessage( SteamId steamIDClanChat, int iMessage, IntPtr prgchText, int cchTextMax, ref ChatEntryType peChatEntryType, ref SteamId psteamidChatter )
		{
			var returnValue = _GetClanChatMessage( Self, steamIDClanChat, iMessage, prgchText, cchTextMax, ref peChatEntryType, ref psteamidChatter );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FIsClanChatAdmin( IntPtr self, SteamId steamIDClanChat, SteamId steamIDUser );
		private FIsClanChatAdmin _IsClanChatAdmin;
		
		#endregion
		internal bool IsClanChatAdmin( SteamId steamIDClanChat, SteamId steamIDUser )
		{
			var returnValue = _IsClanChatAdmin( Self, steamIDClanChat, steamIDUser );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FIsClanChatWindowOpenInSteam( IntPtr self, SteamId steamIDClanChat );
		private FIsClanChatWindowOpenInSteam _IsClanChatWindowOpenInSteam;
		
		#endregion
		internal bool IsClanChatWindowOpenInSteam( SteamId steamIDClanChat )
		{
			var returnValue = _IsClanChatWindowOpenInSteam( Self, steamIDClanChat );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FOpenClanChatWindowInSteam( IntPtr self, SteamId steamIDClanChat );
		private FOpenClanChatWindowInSteam _OpenClanChatWindowInSteam;
		
		#endregion
		internal bool OpenClanChatWindowInSteam( SteamId steamIDClanChat )
		{
			var returnValue = _OpenClanChatWindowInSteam( Self, steamIDClanChat );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FCloseClanChatWindowInSteam( IntPtr self, SteamId steamIDClanChat );
		private FCloseClanChatWindowInSteam _CloseClanChatWindowInSteam;
		
		#endregion
		internal bool CloseClanChatWindowInSteam( SteamId steamIDClanChat )
		{
			var returnValue = _CloseClanChatWindowInSteam( Self, steamIDClanChat );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FSetListenForFriendsMessages( IntPtr self, [MarshalAs( UnmanagedType.U1 )] bool bInterceptEnabled );
		private FSetListenForFriendsMessages _SetListenForFriendsMessages;
		
		#endregion
		internal bool SetListenForFriendsMessages( [MarshalAs( UnmanagedType.U1 )] bool bInterceptEnabled )
		{
			var returnValue = _SetListenForFriendsMessages( Self, bInterceptEnabled );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FReplyToFriendMessage( IntPtr self, SteamId steamIDFriend, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchMsgToSend );
		private FReplyToFriendMessage _ReplyToFriendMessage;
		
		#endregion
		internal bool ReplyToFriendMessage( SteamId steamIDFriend, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchMsgToSend )
		{
			var returnValue = _ReplyToFriendMessage( Self, steamIDFriend, pchMsgToSend );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate int FGetFriendMessage( IntPtr self, SteamId steamIDFriend, int iMessageID, IntPtr pvData, int cubData, ref ChatEntryType peChatEntryType );
		private FGetFriendMessage _GetFriendMessage;
		
		#endregion
		internal int GetFriendMessage( SteamId steamIDFriend, int iMessageID, IntPtr pvData, int cubData, ref ChatEntryType peChatEntryType )
		{
			var returnValue = _GetFriendMessage( Self, steamIDFriend, iMessageID, pvData, cubData, ref peChatEntryType );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate SteamAPICall_t FGetFollowerCount( IntPtr self, SteamId steamID );
		private FGetFollowerCount _GetFollowerCount;
		
		#endregion
		internal async Task<FriendsGetFollowerCount_t?> GetFollowerCount( SteamId steamID )
		{
			var returnValue = _GetFollowerCount( Self, steamID );
			return await FriendsGetFollowerCount_t.GetResultAsync( returnValue );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate SteamAPICall_t FIsFollowing( IntPtr self, SteamId steamID );
		private FIsFollowing _IsFollowing;
		
		#endregion
		internal async Task<FriendsIsFollowing_t?> IsFollowing( SteamId steamID )
		{
			var returnValue = _IsFollowing( Self, steamID );
			return await FriendsIsFollowing_t.GetResultAsync( returnValue );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate SteamAPICall_t FEnumerateFollowingList( IntPtr self, uint unStartIndex );
		private FEnumerateFollowingList _EnumerateFollowingList;
		
		#endregion
		internal async Task<FriendsEnumerateFollowingList_t?> EnumerateFollowingList( uint unStartIndex )
		{
			var returnValue = _EnumerateFollowingList( Self, unStartIndex );
			return await FriendsEnumerateFollowingList_t.GetResultAsync( returnValue );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FIsClanPublic( IntPtr self, SteamId steamIDClan );
		private FIsClanPublic _IsClanPublic;
		
		#endregion
		internal bool IsClanPublic( SteamId steamIDClan )
		{
			var returnValue = _IsClanPublic( Self, steamIDClan );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FIsClanOfficialGameGroup( IntPtr self, SteamId steamIDClan );
		private FIsClanOfficialGameGroup _IsClanOfficialGameGroup;
		
		#endregion
		internal bool IsClanOfficialGameGroup( SteamId steamIDClan )
		{
			var returnValue = _IsClanOfficialGameGroup( Self, steamIDClan );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate int FGetNumChatsWithUnreadPriorityMessages( IntPtr self );
		private FGetNumChatsWithUnreadPriorityMessages _GetNumChatsWithUnreadPriorityMessages;
		
		#endregion
		internal int GetNumChatsWithUnreadPriorityMessages()
		{
			var returnValue = _GetNumChatsWithUnreadPriorityMessages( Self );
			return returnValue;
		}
		
	}
}
