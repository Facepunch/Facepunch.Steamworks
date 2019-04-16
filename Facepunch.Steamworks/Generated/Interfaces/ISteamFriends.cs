using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;


namespace Steamworks.Internal
{
	public class ISteamFriends : BaseSteamInterface
	{
		public ISteamFriends( bool server = false ) : base( server )
		{
		}
		
		public override string InterfaceName => "SteamFriends017";
		
		public override void InitInternals()
		{
			GetPersonaNameDelegatePointer = Marshal.GetDelegateForFunctionPointer<GetPersonaNameDelegate>( Marshal.ReadIntPtr( VTable, 0) );
			SetPersonaNameDelegatePointer = Marshal.GetDelegateForFunctionPointer<SetPersonaNameDelegate>( Marshal.ReadIntPtr( VTable, 8) );
			GetPersonaStateDelegatePointer = Marshal.GetDelegateForFunctionPointer<GetPersonaStateDelegate>( Marshal.ReadIntPtr( VTable, 16) );
			GetFriendCountDelegatePointer = Marshal.GetDelegateForFunctionPointer<GetFriendCountDelegate>( Marshal.ReadIntPtr( VTable, 24) );
			GetFriendByIndexDelegatePointer = Marshal.GetDelegateForFunctionPointer<GetFriendByIndexDelegate>( Marshal.ReadIntPtr( VTable, 32) );
			GetFriendRelationshipDelegatePointer = Marshal.GetDelegateForFunctionPointer<GetFriendRelationshipDelegate>( Marshal.ReadIntPtr( VTable, 40) );
			GetFriendPersonaStateDelegatePointer = Marshal.GetDelegateForFunctionPointer<GetFriendPersonaStateDelegate>( Marshal.ReadIntPtr( VTable, 48) );
			GetFriendPersonaNameDelegatePointer = Marshal.GetDelegateForFunctionPointer<GetFriendPersonaNameDelegate>( Marshal.ReadIntPtr( VTable, 56) );
			GetFriendGamePlayedDelegatePointer = Marshal.GetDelegateForFunctionPointer<GetFriendGamePlayedDelegate>( Marshal.ReadIntPtr( VTable, 64) );
			GetFriendPersonaNameHistoryDelegatePointer = Marshal.GetDelegateForFunctionPointer<GetFriendPersonaNameHistoryDelegate>( Marshal.ReadIntPtr( VTable, 72) );
			GetFriendSteamLevelDelegatePointer = Marshal.GetDelegateForFunctionPointer<GetFriendSteamLevelDelegate>( Marshal.ReadIntPtr( VTable, 80) );
			GetPlayerNicknameDelegatePointer = Marshal.GetDelegateForFunctionPointer<GetPlayerNicknameDelegate>( Marshal.ReadIntPtr( VTable, 88) );
			GetFriendsGroupCountDelegatePointer = Marshal.GetDelegateForFunctionPointer<GetFriendsGroupCountDelegate>( Marshal.ReadIntPtr( VTable, 96) );
			GetFriendsGroupIDByIndexDelegatePointer = Marshal.GetDelegateForFunctionPointer<GetFriendsGroupIDByIndexDelegate>( Marshal.ReadIntPtr( VTable, 104) );
			GetFriendsGroupNameDelegatePointer = Marshal.GetDelegateForFunctionPointer<GetFriendsGroupNameDelegate>( Marshal.ReadIntPtr( VTable, 112) );
			GetFriendsGroupMembersCountDelegatePointer = Marshal.GetDelegateForFunctionPointer<GetFriendsGroupMembersCountDelegate>( Marshal.ReadIntPtr( VTable, 120) );
			GetFriendsGroupMembersListDelegatePointer = Marshal.GetDelegateForFunctionPointer<GetFriendsGroupMembersListDelegate>( Marshal.ReadIntPtr( VTable, 128) );
			HasFriendDelegatePointer = Marshal.GetDelegateForFunctionPointer<HasFriendDelegate>( Marshal.ReadIntPtr( VTable, 136) );
			GetClanCountDelegatePointer = Marshal.GetDelegateForFunctionPointer<GetClanCountDelegate>( Marshal.ReadIntPtr( VTable, 144) );
			GetClanByIndexDelegatePointer = Marshal.GetDelegateForFunctionPointer<GetClanByIndexDelegate>( Marshal.ReadIntPtr( VTable, 152) );
			GetClanNameDelegatePointer = Marshal.GetDelegateForFunctionPointer<GetClanNameDelegate>( Marshal.ReadIntPtr( VTable, 160) );
			GetClanTagDelegatePointer = Marshal.GetDelegateForFunctionPointer<GetClanTagDelegate>( Marshal.ReadIntPtr( VTable, 168) );
			GetClanActivityCountsDelegatePointer = Marshal.GetDelegateForFunctionPointer<GetClanActivityCountsDelegate>( Marshal.ReadIntPtr( VTable, 176) );
			DownloadClanActivityCountsDelegatePointer = Marshal.GetDelegateForFunctionPointer<DownloadClanActivityCountsDelegate>( Marshal.ReadIntPtr( VTable, 184) );
			GetFriendCountFromSourceDelegatePointer = Marshal.GetDelegateForFunctionPointer<GetFriendCountFromSourceDelegate>( Marshal.ReadIntPtr( VTable, 192) );
			GetFriendFromSourceByIndexDelegatePointer = Marshal.GetDelegateForFunctionPointer<GetFriendFromSourceByIndexDelegate>( Marshal.ReadIntPtr( VTable, 200) );
			IsUserInSourceDelegatePointer = Marshal.GetDelegateForFunctionPointer<IsUserInSourceDelegate>( Marshal.ReadIntPtr( VTable, 208) );
			SetInGameVoiceSpeakingDelegatePointer = Marshal.GetDelegateForFunctionPointer<SetInGameVoiceSpeakingDelegate>( Marshal.ReadIntPtr( VTable, 216) );
			ActivateGameOverlayDelegatePointer = Marshal.GetDelegateForFunctionPointer<ActivateGameOverlayDelegate>( Marshal.ReadIntPtr( VTable, 224) );
			ActivateGameOverlayToUserDelegatePointer = Marshal.GetDelegateForFunctionPointer<ActivateGameOverlayToUserDelegate>( Marshal.ReadIntPtr( VTable, 232) );
			ActivateGameOverlayToWebPageDelegatePointer = Marshal.GetDelegateForFunctionPointer<ActivateGameOverlayToWebPageDelegate>( Marshal.ReadIntPtr( VTable, 240) );
			ActivateGameOverlayToStoreDelegatePointer = Marshal.GetDelegateForFunctionPointer<ActivateGameOverlayToStoreDelegate>( Marshal.ReadIntPtr( VTable, 248) );
			SetPlayedWithDelegatePointer = Marshal.GetDelegateForFunctionPointer<SetPlayedWithDelegate>( Marshal.ReadIntPtr( VTable, 256) );
			ActivateGameOverlayInviteDialogDelegatePointer = Marshal.GetDelegateForFunctionPointer<ActivateGameOverlayInviteDialogDelegate>( Marshal.ReadIntPtr( VTable, 264) );
			GetSmallFriendAvatarDelegatePointer = Marshal.GetDelegateForFunctionPointer<GetSmallFriendAvatarDelegate>( Marshal.ReadIntPtr( VTable, 272) );
			GetMediumFriendAvatarDelegatePointer = Marshal.GetDelegateForFunctionPointer<GetMediumFriendAvatarDelegate>( Marshal.ReadIntPtr( VTable, 280) );
			GetLargeFriendAvatarDelegatePointer = Marshal.GetDelegateForFunctionPointer<GetLargeFriendAvatarDelegate>( Marshal.ReadIntPtr( VTable, 288) );
			RequestUserInformationDelegatePointer = Marshal.GetDelegateForFunctionPointer<RequestUserInformationDelegate>( Marshal.ReadIntPtr( VTable, 296) );
			RequestClanOfficerListDelegatePointer = Marshal.GetDelegateForFunctionPointer<RequestClanOfficerListDelegate>( Marshal.ReadIntPtr( VTable, 304) );
			GetClanOwnerDelegatePointer = Marshal.GetDelegateForFunctionPointer<GetClanOwnerDelegate>( Marshal.ReadIntPtr( VTable, 312) );
			GetClanOfficerCountDelegatePointer = Marshal.GetDelegateForFunctionPointer<GetClanOfficerCountDelegate>( Marshal.ReadIntPtr( VTable, 320) );
			GetClanOfficerByIndexDelegatePointer = Marshal.GetDelegateForFunctionPointer<GetClanOfficerByIndexDelegate>( Marshal.ReadIntPtr( VTable, 328) );
			GetUserRestrictionsDelegatePointer = Marshal.GetDelegateForFunctionPointer<GetUserRestrictionsDelegate>( Marshal.ReadIntPtr( VTable, 336) );
			SetRichPresenceDelegatePointer = Marshal.GetDelegateForFunctionPointer<SetRichPresenceDelegate>( Marshal.ReadIntPtr( VTable, 344) );
			ClearRichPresenceDelegatePointer = Marshal.GetDelegateForFunctionPointer<ClearRichPresenceDelegate>( Marshal.ReadIntPtr( VTable, 352) );
			GetFriendRichPresenceDelegatePointer = Marshal.GetDelegateForFunctionPointer<GetFriendRichPresenceDelegate>( Marshal.ReadIntPtr( VTable, 360) );
			GetFriendRichPresenceKeyCountDelegatePointer = Marshal.GetDelegateForFunctionPointer<GetFriendRichPresenceKeyCountDelegate>( Marshal.ReadIntPtr( VTable, 368) );
			GetFriendRichPresenceKeyByIndexDelegatePointer = Marshal.GetDelegateForFunctionPointer<GetFriendRichPresenceKeyByIndexDelegate>( Marshal.ReadIntPtr( VTable, 376) );
			RequestFriendRichPresenceDelegatePointer = Marshal.GetDelegateForFunctionPointer<RequestFriendRichPresenceDelegate>( Marshal.ReadIntPtr( VTable, 384) );
			InviteUserToGameDelegatePointer = Marshal.GetDelegateForFunctionPointer<InviteUserToGameDelegate>( Marshal.ReadIntPtr( VTable, 392) );
			GetCoplayFriendCountDelegatePointer = Marshal.GetDelegateForFunctionPointer<GetCoplayFriendCountDelegate>( Marshal.ReadIntPtr( VTable, 400) );
			GetCoplayFriendDelegatePointer = Marshal.GetDelegateForFunctionPointer<GetCoplayFriendDelegate>( Marshal.ReadIntPtr( VTable, 408) );
			GetFriendCoplayTimeDelegatePointer = Marshal.GetDelegateForFunctionPointer<GetFriendCoplayTimeDelegate>( Marshal.ReadIntPtr( VTable, 416) );
			GetFriendCoplayGameDelegatePointer = Marshal.GetDelegateForFunctionPointer<GetFriendCoplayGameDelegate>( Marshal.ReadIntPtr( VTable, 424) );
			JoinClanChatRoomDelegatePointer = Marshal.GetDelegateForFunctionPointer<JoinClanChatRoomDelegate>( Marshal.ReadIntPtr( VTable, 432) );
			LeaveClanChatRoomDelegatePointer = Marshal.GetDelegateForFunctionPointer<LeaveClanChatRoomDelegate>( Marshal.ReadIntPtr( VTable, 440) );
			GetClanChatMemberCountDelegatePointer = Marshal.GetDelegateForFunctionPointer<GetClanChatMemberCountDelegate>( Marshal.ReadIntPtr( VTable, 448) );
			GetChatMemberByIndexDelegatePointer = Marshal.GetDelegateForFunctionPointer<GetChatMemberByIndexDelegate>( Marshal.ReadIntPtr( VTable, 456) );
			SendClanChatMessageDelegatePointer = Marshal.GetDelegateForFunctionPointer<SendClanChatMessageDelegate>( Marshal.ReadIntPtr( VTable, 464) );
			GetClanChatMessageDelegatePointer = Marshal.GetDelegateForFunctionPointer<GetClanChatMessageDelegate>( Marshal.ReadIntPtr( VTable, 472) );
			IsClanChatAdminDelegatePointer = Marshal.GetDelegateForFunctionPointer<IsClanChatAdminDelegate>( Marshal.ReadIntPtr( VTable, 480) );
			IsClanChatWindowOpenInSteamDelegatePointer = Marshal.GetDelegateForFunctionPointer<IsClanChatWindowOpenInSteamDelegate>( Marshal.ReadIntPtr( VTable, 488) );
			OpenClanChatWindowInSteamDelegatePointer = Marshal.GetDelegateForFunctionPointer<OpenClanChatWindowInSteamDelegate>( Marshal.ReadIntPtr( VTable, 496) );
			CloseClanChatWindowInSteamDelegatePointer = Marshal.GetDelegateForFunctionPointer<CloseClanChatWindowInSteamDelegate>( Marshal.ReadIntPtr( VTable, 504) );
			SetListenForFriendsMessagesDelegatePointer = Marshal.GetDelegateForFunctionPointer<SetListenForFriendsMessagesDelegate>( Marshal.ReadIntPtr( VTable, 512) );
			ReplyToFriendMessageDelegatePointer = Marshal.GetDelegateForFunctionPointer<ReplyToFriendMessageDelegate>( Marshal.ReadIntPtr( VTable, 520) );
			GetFriendMessageDelegatePointer = Marshal.GetDelegateForFunctionPointer<GetFriendMessageDelegate>( Marshal.ReadIntPtr( VTable, 528) );
			GetFollowerCountDelegatePointer = Marshal.GetDelegateForFunctionPointer<GetFollowerCountDelegate>( Marshal.ReadIntPtr( VTable, 536) );
			IsFollowingDelegatePointer = Marshal.GetDelegateForFunctionPointer<IsFollowingDelegate>( Marshal.ReadIntPtr( VTable, 544) );
			EnumerateFollowingListDelegatePointer = Marshal.GetDelegateForFunctionPointer<EnumerateFollowingListDelegate>( Marshal.ReadIntPtr( VTable, 552) );
			IsClanPublicDelegatePointer = Marshal.GetDelegateForFunctionPointer<IsClanPublicDelegate>( Marshal.ReadIntPtr( VTable, 560) );
			IsClanOfficialGameGroupDelegatePointer = Marshal.GetDelegateForFunctionPointer<IsClanOfficialGameGroupDelegate>( Marshal.ReadIntPtr( VTable, 568) );
			GetNumChatsWithUnreadPriorityMessagesDelegatePointer = Marshal.GetDelegateForFunctionPointer<GetNumChatsWithUnreadPriorityMessagesDelegate>( Marshal.ReadIntPtr( VTable, 576) );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate IntPtr GetPersonaNameDelegate( IntPtr self );
		private GetPersonaNameDelegate GetPersonaNameDelegatePointer;
		
		#endregion
		internal string GetPersonaName()
		{
			return GetString( GetPersonaNameDelegatePointer( Self ) );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate SteamAPICall_t SetPersonaNameDelegate( IntPtr self, string pchPersonaName );
		private SetPersonaNameDelegate SetPersonaNameDelegatePointer;
		
		#endregion
		internal async Task<SetPersonaNameResponse_t?> SetPersonaName( string pchPersonaName )
		{
			return await (new Result<SetPersonaNameResponse_t>( SetPersonaNameDelegatePointer( Self, pchPersonaName ) )).GetResult();
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate FriendState GetPersonaStateDelegate( IntPtr self );
		private GetPersonaStateDelegate GetPersonaStateDelegatePointer;
		
		#endregion
		internal FriendState GetPersonaState()
		{
			return GetPersonaStateDelegatePointer( Self );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate int GetFriendCountDelegate( IntPtr self, int iFriendFlags );
		private GetFriendCountDelegate GetFriendCountDelegatePointer;
		
		#endregion
		internal int GetFriendCount( int iFriendFlags )
		{
			return GetFriendCountDelegatePointer( Self, iFriendFlags );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate void GetFriendByIndexDelegate( IntPtr self, ref SteamId retVal, int iFriend, int iFriendFlags );
		private GetFriendByIndexDelegate GetFriendByIndexDelegatePointer;
		
		#endregion
		internal SteamId GetFriendByIndex( int iFriend, int iFriendFlags )
		{
			var retVal = default( SteamId );
			GetFriendByIndexDelegatePointer( Self, ref retVal, iFriend, iFriendFlags );
			return retVal;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate Relationship GetFriendRelationshipDelegate( IntPtr self, SteamId steamIDFriend );
		private GetFriendRelationshipDelegate GetFriendRelationshipDelegatePointer;
		
		#endregion
		internal Relationship GetFriendRelationship( SteamId steamIDFriend )
		{
			return GetFriendRelationshipDelegatePointer( Self, steamIDFriend );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate FriendState GetFriendPersonaStateDelegate( IntPtr self, SteamId steamIDFriend );
		private GetFriendPersonaStateDelegate GetFriendPersonaStateDelegatePointer;
		
		#endregion
		internal FriendState GetFriendPersonaState( SteamId steamIDFriend )
		{
			return GetFriendPersonaStateDelegatePointer( Self, steamIDFriend );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate IntPtr GetFriendPersonaNameDelegate( IntPtr self, SteamId steamIDFriend );
		private GetFriendPersonaNameDelegate GetFriendPersonaNameDelegatePointer;
		
		#endregion
		internal string GetFriendPersonaName( SteamId steamIDFriend )
		{
			return GetString( GetFriendPersonaNameDelegatePointer( Self, steamIDFriend ) );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool GetFriendGamePlayedDelegate( IntPtr self, SteamId steamIDFriend, ref FriendGameInfo_t pFriendGameInfo );
		private GetFriendGamePlayedDelegate GetFriendGamePlayedDelegatePointer;
		
		#endregion
		internal bool GetFriendGamePlayed( SteamId steamIDFriend, ref FriendGameInfo_t pFriendGameInfo )
		{
			return GetFriendGamePlayedDelegatePointer( Self, steamIDFriend, ref pFriendGameInfo );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate IntPtr GetFriendPersonaNameHistoryDelegate( IntPtr self, SteamId steamIDFriend, int iPersonaName );
		private GetFriendPersonaNameHistoryDelegate GetFriendPersonaNameHistoryDelegatePointer;
		
		#endregion
		internal string GetFriendPersonaNameHistory( SteamId steamIDFriend, int iPersonaName )
		{
			return GetString( GetFriendPersonaNameHistoryDelegatePointer( Self, steamIDFriend, iPersonaName ) );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate int GetFriendSteamLevelDelegate( IntPtr self, SteamId steamIDFriend );
		private GetFriendSteamLevelDelegate GetFriendSteamLevelDelegatePointer;
		
		#endregion
		internal int GetFriendSteamLevel( SteamId steamIDFriend )
		{
			return GetFriendSteamLevelDelegatePointer( Self, steamIDFriend );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate IntPtr GetPlayerNicknameDelegate( IntPtr self, SteamId steamIDPlayer );
		private GetPlayerNicknameDelegate GetPlayerNicknameDelegatePointer;
		
		#endregion
		internal string GetPlayerNickname( SteamId steamIDPlayer )
		{
			return GetString( GetPlayerNicknameDelegatePointer( Self, steamIDPlayer ) );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate int GetFriendsGroupCountDelegate( IntPtr self );
		private GetFriendsGroupCountDelegate GetFriendsGroupCountDelegatePointer;
		
		#endregion
		internal int GetFriendsGroupCount()
		{
			return GetFriendsGroupCountDelegatePointer( Self );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate FriendsGroupID_t GetFriendsGroupIDByIndexDelegate( IntPtr self, int iFG );
		private GetFriendsGroupIDByIndexDelegate GetFriendsGroupIDByIndexDelegatePointer;
		
		#endregion
		internal FriendsGroupID_t GetFriendsGroupIDByIndex( int iFG )
		{
			return GetFriendsGroupIDByIndexDelegatePointer( Self, iFG );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate IntPtr GetFriendsGroupNameDelegate( IntPtr self, FriendsGroupID_t friendsGroupID );
		private GetFriendsGroupNameDelegate GetFriendsGroupNameDelegatePointer;
		
		#endregion
		internal string GetFriendsGroupName( FriendsGroupID_t friendsGroupID )
		{
			return GetString( GetFriendsGroupNameDelegatePointer( Self, friendsGroupID ) );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate int GetFriendsGroupMembersCountDelegate( IntPtr self, FriendsGroupID_t friendsGroupID );
		private GetFriendsGroupMembersCountDelegate GetFriendsGroupMembersCountDelegatePointer;
		
		#endregion
		internal int GetFriendsGroupMembersCount( FriendsGroupID_t friendsGroupID )
		{
			return GetFriendsGroupMembersCountDelegatePointer( Self, friendsGroupID );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate void GetFriendsGroupMembersListDelegate( IntPtr self, FriendsGroupID_t friendsGroupID, [In,Out] SteamId[]  pOutSteamIDMembers, int nMembersCount );
		private GetFriendsGroupMembersListDelegate GetFriendsGroupMembersListDelegatePointer;
		
		#endregion
		internal void GetFriendsGroupMembersList( FriendsGroupID_t friendsGroupID, [In,Out] SteamId[]  pOutSteamIDMembers, int nMembersCount )
		{
			GetFriendsGroupMembersListDelegatePointer( Self, friendsGroupID, pOutSteamIDMembers, nMembersCount );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool HasFriendDelegate( IntPtr self, SteamId steamIDFriend, int iFriendFlags );
		private HasFriendDelegate HasFriendDelegatePointer;
		
		#endregion
		internal bool HasFriend( SteamId steamIDFriend, int iFriendFlags )
		{
			return HasFriendDelegatePointer( Self, steamIDFriend, iFriendFlags );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate int GetClanCountDelegate( IntPtr self );
		private GetClanCountDelegate GetClanCountDelegatePointer;
		
		#endregion
		internal int GetClanCount()
		{
			return GetClanCountDelegatePointer( Self );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate void GetClanByIndexDelegate( IntPtr self, ref SteamId retVal, int iClan );
		private GetClanByIndexDelegate GetClanByIndexDelegatePointer;
		
		#endregion
		internal SteamId GetClanByIndex( int iClan )
		{
			var retVal = default( SteamId );
			GetClanByIndexDelegatePointer( Self, ref retVal, iClan );
			return retVal;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate IntPtr GetClanNameDelegate( IntPtr self, SteamId steamIDClan );
		private GetClanNameDelegate GetClanNameDelegatePointer;
		
		#endregion
		internal string GetClanName( SteamId steamIDClan )
		{
			return GetString( GetClanNameDelegatePointer( Self, steamIDClan ) );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate IntPtr GetClanTagDelegate( IntPtr self, SteamId steamIDClan );
		private GetClanTagDelegate GetClanTagDelegatePointer;
		
		#endregion
		internal string GetClanTag( SteamId steamIDClan )
		{
			return GetString( GetClanTagDelegatePointer( Self, steamIDClan ) );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool GetClanActivityCountsDelegate( IntPtr self, SteamId steamIDClan, ref int pnOnline, ref int pnInGame, ref int pnChatting );
		private GetClanActivityCountsDelegate GetClanActivityCountsDelegatePointer;
		
		#endregion
		internal bool GetClanActivityCounts( SteamId steamIDClan, ref int pnOnline, ref int pnInGame, ref int pnChatting )
		{
			return GetClanActivityCountsDelegatePointer( Self, steamIDClan, ref pnOnline, ref pnInGame, ref pnChatting );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate SteamAPICall_t DownloadClanActivityCountsDelegate( IntPtr self, [In,Out] SteamId[]  psteamIDClans, int cClansToRequest );
		private DownloadClanActivityCountsDelegate DownloadClanActivityCountsDelegatePointer;
		
		#endregion
		internal async Task<DownloadClanActivityCountsResult_t?> DownloadClanActivityCounts( [In,Out] SteamId[]  psteamIDClans, int cClansToRequest )
		{
			return await (new Result<DownloadClanActivityCountsResult_t>( DownloadClanActivityCountsDelegatePointer( Self, psteamIDClans, cClansToRequest ) )).GetResult();
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate int GetFriendCountFromSourceDelegate( IntPtr self, SteamId steamIDSource );
		private GetFriendCountFromSourceDelegate GetFriendCountFromSourceDelegatePointer;
		
		#endregion
		internal int GetFriendCountFromSource( SteamId steamIDSource )
		{
			return GetFriendCountFromSourceDelegatePointer( Self, steamIDSource );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate void GetFriendFromSourceByIndexDelegate( IntPtr self, ref SteamId retVal, SteamId steamIDSource, int iFriend );
		private GetFriendFromSourceByIndexDelegate GetFriendFromSourceByIndexDelegatePointer;
		
		#endregion
		internal SteamId GetFriendFromSourceByIndex( SteamId steamIDSource, int iFriend )
		{
			var retVal = default( SteamId );
			GetFriendFromSourceByIndexDelegatePointer( Self, ref retVal, steamIDSource, iFriend );
			return retVal;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool IsUserInSourceDelegate( IntPtr self, SteamId steamIDUser, SteamId steamIDSource );
		private IsUserInSourceDelegate IsUserInSourceDelegatePointer;
		
		#endregion
		internal bool IsUserInSource( SteamId steamIDUser, SteamId steamIDSource )
		{
			return IsUserInSourceDelegatePointer( Self, steamIDUser, steamIDSource );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate void SetInGameVoiceSpeakingDelegate( IntPtr self, SteamId steamIDUser, [MarshalAs( UnmanagedType.U1 )] bool bSpeaking );
		private SetInGameVoiceSpeakingDelegate SetInGameVoiceSpeakingDelegatePointer;
		
		#endregion
		internal void SetInGameVoiceSpeaking( SteamId steamIDUser, [MarshalAs( UnmanagedType.U1 )] bool bSpeaking )
		{
			SetInGameVoiceSpeakingDelegatePointer( Self, steamIDUser, bSpeaking );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate void ActivateGameOverlayDelegate( IntPtr self, string pchDialog );
		private ActivateGameOverlayDelegate ActivateGameOverlayDelegatePointer;
		
		#endregion
		internal void ActivateGameOverlay( string pchDialog )
		{
			ActivateGameOverlayDelegatePointer( Self, pchDialog );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate void ActivateGameOverlayToUserDelegate( IntPtr self, string pchDialog, SteamId steamID );
		private ActivateGameOverlayToUserDelegate ActivateGameOverlayToUserDelegatePointer;
		
		#endregion
		internal void ActivateGameOverlayToUser( string pchDialog, SteamId steamID )
		{
			ActivateGameOverlayToUserDelegatePointer( Self, pchDialog, steamID );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate void ActivateGameOverlayToWebPageDelegate( IntPtr self, string pchURL, ActivateGameOverlayToWebPageMode eMode );
		private ActivateGameOverlayToWebPageDelegate ActivateGameOverlayToWebPageDelegatePointer;
		
		#endregion
		internal void ActivateGameOverlayToWebPage( string pchURL, ActivateGameOverlayToWebPageMode eMode )
		{
			ActivateGameOverlayToWebPageDelegatePointer( Self, pchURL, eMode );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate void ActivateGameOverlayToStoreDelegate( IntPtr self, AppId_t nAppID, OverlayToStoreFlag eFlag );
		private ActivateGameOverlayToStoreDelegate ActivateGameOverlayToStoreDelegatePointer;
		
		#endregion
		internal void ActivateGameOverlayToStore( AppId_t nAppID, OverlayToStoreFlag eFlag )
		{
			ActivateGameOverlayToStoreDelegatePointer( Self, nAppID, eFlag );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate void SetPlayedWithDelegate( IntPtr self, SteamId steamIDUserPlayedWith );
		private SetPlayedWithDelegate SetPlayedWithDelegatePointer;
		
		#endregion
		internal void SetPlayedWith( SteamId steamIDUserPlayedWith )
		{
			SetPlayedWithDelegatePointer( Self, steamIDUserPlayedWith );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate void ActivateGameOverlayInviteDialogDelegate( IntPtr self, SteamId steamIDLobby );
		private ActivateGameOverlayInviteDialogDelegate ActivateGameOverlayInviteDialogDelegatePointer;
		
		#endregion
		internal void ActivateGameOverlayInviteDialog( SteamId steamIDLobby )
		{
			ActivateGameOverlayInviteDialogDelegatePointer( Self, steamIDLobby );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate int GetSmallFriendAvatarDelegate( IntPtr self, SteamId steamIDFriend );
		private GetSmallFriendAvatarDelegate GetSmallFriendAvatarDelegatePointer;
		
		#endregion
		internal int GetSmallFriendAvatar( SteamId steamIDFriend )
		{
			return GetSmallFriendAvatarDelegatePointer( Self, steamIDFriend );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate int GetMediumFriendAvatarDelegate( IntPtr self, SteamId steamIDFriend );
		private GetMediumFriendAvatarDelegate GetMediumFriendAvatarDelegatePointer;
		
		#endregion
		internal int GetMediumFriendAvatar( SteamId steamIDFriend )
		{
			return GetMediumFriendAvatarDelegatePointer( Self, steamIDFriend );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate int GetLargeFriendAvatarDelegate( IntPtr self, SteamId steamIDFriend );
		private GetLargeFriendAvatarDelegate GetLargeFriendAvatarDelegatePointer;
		
		#endregion
		internal int GetLargeFriendAvatar( SteamId steamIDFriend )
		{
			return GetLargeFriendAvatarDelegatePointer( Self, steamIDFriend );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool RequestUserInformationDelegate( IntPtr self, SteamId steamIDUser, [MarshalAs( UnmanagedType.U1 )] bool bRequireNameOnly );
		private RequestUserInformationDelegate RequestUserInformationDelegatePointer;
		
		#endregion
		internal bool RequestUserInformation( SteamId steamIDUser, [MarshalAs( UnmanagedType.U1 )] bool bRequireNameOnly )
		{
			return RequestUserInformationDelegatePointer( Self, steamIDUser, bRequireNameOnly );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate SteamAPICall_t RequestClanOfficerListDelegate( IntPtr self, SteamId steamIDClan );
		private RequestClanOfficerListDelegate RequestClanOfficerListDelegatePointer;
		
		#endregion
		internal async Task<ClanOfficerListResponse_t?> RequestClanOfficerList( SteamId steamIDClan )
		{
			return await (new Result<ClanOfficerListResponse_t>( RequestClanOfficerListDelegatePointer( Self, steamIDClan ) )).GetResult();
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate void GetClanOwnerDelegate( IntPtr self, ref SteamId retVal, SteamId steamIDClan );
		private GetClanOwnerDelegate GetClanOwnerDelegatePointer;
		
		#endregion
		internal SteamId GetClanOwner( SteamId steamIDClan )
		{
			var retVal = default( SteamId );
			GetClanOwnerDelegatePointer( Self, ref retVal, steamIDClan );
			return retVal;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate int GetClanOfficerCountDelegate( IntPtr self, SteamId steamIDClan );
		private GetClanOfficerCountDelegate GetClanOfficerCountDelegatePointer;
		
		#endregion
		internal int GetClanOfficerCount( SteamId steamIDClan )
		{
			return GetClanOfficerCountDelegatePointer( Self, steamIDClan );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate void GetClanOfficerByIndexDelegate( IntPtr self, ref SteamId retVal, SteamId steamIDClan, int iOfficer );
		private GetClanOfficerByIndexDelegate GetClanOfficerByIndexDelegatePointer;
		
		#endregion
		internal SteamId GetClanOfficerByIndex( SteamId steamIDClan, int iOfficer )
		{
			var retVal = default( SteamId );
			GetClanOfficerByIndexDelegatePointer( Self, ref retVal, steamIDClan, iOfficer );
			return retVal;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate uint GetUserRestrictionsDelegate( IntPtr self );
		private GetUserRestrictionsDelegate GetUserRestrictionsDelegatePointer;
		
		#endregion
		internal uint GetUserRestrictions()
		{
			return GetUserRestrictionsDelegatePointer( Self );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool SetRichPresenceDelegate( IntPtr self, string pchKey, string pchValue );
		private SetRichPresenceDelegate SetRichPresenceDelegatePointer;
		
		#endregion
		internal bool SetRichPresence( string pchKey, string pchValue )
		{
			return SetRichPresenceDelegatePointer( Self, pchKey, pchValue );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate void ClearRichPresenceDelegate( IntPtr self );
		private ClearRichPresenceDelegate ClearRichPresenceDelegatePointer;
		
		#endregion
		internal void ClearRichPresence()
		{
			ClearRichPresenceDelegatePointer( Self );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate IntPtr GetFriendRichPresenceDelegate( IntPtr self, SteamId steamIDFriend, string pchKey );
		private GetFriendRichPresenceDelegate GetFriendRichPresenceDelegatePointer;
		
		#endregion
		internal string GetFriendRichPresence( SteamId steamIDFriend, string pchKey )
		{
			return GetString( GetFriendRichPresenceDelegatePointer( Self, steamIDFriend, pchKey ) );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate int GetFriendRichPresenceKeyCountDelegate( IntPtr self, SteamId steamIDFriend );
		private GetFriendRichPresenceKeyCountDelegate GetFriendRichPresenceKeyCountDelegatePointer;
		
		#endregion
		internal int GetFriendRichPresenceKeyCount( SteamId steamIDFriend )
		{
			return GetFriendRichPresenceKeyCountDelegatePointer( Self, steamIDFriend );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate IntPtr GetFriendRichPresenceKeyByIndexDelegate( IntPtr self, SteamId steamIDFriend, int iKey );
		private GetFriendRichPresenceKeyByIndexDelegate GetFriendRichPresenceKeyByIndexDelegatePointer;
		
		#endregion
		internal string GetFriendRichPresenceKeyByIndex( SteamId steamIDFriend, int iKey )
		{
			return GetString( GetFriendRichPresenceKeyByIndexDelegatePointer( Self, steamIDFriend, iKey ) );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate void RequestFriendRichPresenceDelegate( IntPtr self, SteamId steamIDFriend );
		private RequestFriendRichPresenceDelegate RequestFriendRichPresenceDelegatePointer;
		
		#endregion
		internal void RequestFriendRichPresence( SteamId steamIDFriend )
		{
			RequestFriendRichPresenceDelegatePointer( Self, steamIDFriend );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool InviteUserToGameDelegate( IntPtr self, SteamId steamIDFriend, string pchConnectString );
		private InviteUserToGameDelegate InviteUserToGameDelegatePointer;
		
		#endregion
		internal bool InviteUserToGame( SteamId steamIDFriend, string pchConnectString )
		{
			return InviteUserToGameDelegatePointer( Self, steamIDFriend, pchConnectString );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate int GetCoplayFriendCountDelegate( IntPtr self );
		private GetCoplayFriendCountDelegate GetCoplayFriendCountDelegatePointer;
		
		#endregion
		internal int GetCoplayFriendCount()
		{
			return GetCoplayFriendCountDelegatePointer( Self );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate void GetCoplayFriendDelegate( IntPtr self, ref SteamId retVal, int iCoplayFriend );
		private GetCoplayFriendDelegate GetCoplayFriendDelegatePointer;
		
		#endregion
		internal SteamId GetCoplayFriend( int iCoplayFriend )
		{
			var retVal = default( SteamId );
			GetCoplayFriendDelegatePointer( Self, ref retVal, iCoplayFriend );
			return retVal;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate int GetFriendCoplayTimeDelegate( IntPtr self, SteamId steamIDFriend );
		private GetFriendCoplayTimeDelegate GetFriendCoplayTimeDelegatePointer;
		
		#endregion
		internal int GetFriendCoplayTime( SteamId steamIDFriend )
		{
			return GetFriendCoplayTimeDelegatePointer( Self, steamIDFriend );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate AppId_t GetFriendCoplayGameDelegate( IntPtr self, SteamId steamIDFriend );
		private GetFriendCoplayGameDelegate GetFriendCoplayGameDelegatePointer;
		
		#endregion
		internal AppId_t GetFriendCoplayGame( SteamId steamIDFriend )
		{
			return GetFriendCoplayGameDelegatePointer( Self, steamIDFriend );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate SteamAPICall_t JoinClanChatRoomDelegate( IntPtr self, SteamId steamIDClan );
		private JoinClanChatRoomDelegate JoinClanChatRoomDelegatePointer;
		
		#endregion
		internal async Task<JoinClanChatRoomCompletionResult_t?> JoinClanChatRoom( SteamId steamIDClan )
		{
			return await (new Result<JoinClanChatRoomCompletionResult_t>( JoinClanChatRoomDelegatePointer( Self, steamIDClan ) )).GetResult();
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool LeaveClanChatRoomDelegate( IntPtr self, SteamId steamIDClan );
		private LeaveClanChatRoomDelegate LeaveClanChatRoomDelegatePointer;
		
		#endregion
		internal bool LeaveClanChatRoom( SteamId steamIDClan )
		{
			return LeaveClanChatRoomDelegatePointer( Self, steamIDClan );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate int GetClanChatMemberCountDelegate( IntPtr self, SteamId steamIDClan );
		private GetClanChatMemberCountDelegate GetClanChatMemberCountDelegatePointer;
		
		#endregion
		internal int GetClanChatMemberCount( SteamId steamIDClan )
		{
			return GetClanChatMemberCountDelegatePointer( Self, steamIDClan );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate void GetChatMemberByIndexDelegate( IntPtr self, ref SteamId retVal, SteamId steamIDClan, int iUser );
		private GetChatMemberByIndexDelegate GetChatMemberByIndexDelegatePointer;
		
		#endregion
		internal SteamId GetChatMemberByIndex( SteamId steamIDClan, int iUser )
		{
			var retVal = default( SteamId );
			GetChatMemberByIndexDelegatePointer( Self, ref retVal, steamIDClan, iUser );
			return retVal;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool SendClanChatMessageDelegate( IntPtr self, SteamId steamIDClanChat, string pchText );
		private SendClanChatMessageDelegate SendClanChatMessageDelegatePointer;
		
		#endregion
		internal bool SendClanChatMessage( SteamId steamIDClanChat, string pchText )
		{
			return SendClanChatMessageDelegatePointer( Self, steamIDClanChat, pchText );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate int GetClanChatMessageDelegate( IntPtr self, SteamId steamIDClanChat, int iMessage, IntPtr prgchText, int cchTextMax, ref ChatEntryType peChatEntryType, ref SteamId psteamidChatter );
		private GetClanChatMessageDelegate GetClanChatMessageDelegatePointer;
		
		#endregion
		internal int GetClanChatMessage( SteamId steamIDClanChat, int iMessage, IntPtr prgchText, int cchTextMax, ref ChatEntryType peChatEntryType, ref SteamId psteamidChatter )
		{
			return GetClanChatMessageDelegatePointer( Self, steamIDClanChat, iMessage, prgchText, cchTextMax, ref peChatEntryType, ref psteamidChatter );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool IsClanChatAdminDelegate( IntPtr self, SteamId steamIDClanChat, SteamId steamIDUser );
		private IsClanChatAdminDelegate IsClanChatAdminDelegatePointer;
		
		#endregion
		internal bool IsClanChatAdmin( SteamId steamIDClanChat, SteamId steamIDUser )
		{
			return IsClanChatAdminDelegatePointer( Self, steamIDClanChat, steamIDUser );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool IsClanChatWindowOpenInSteamDelegate( IntPtr self, SteamId steamIDClanChat );
		private IsClanChatWindowOpenInSteamDelegate IsClanChatWindowOpenInSteamDelegatePointer;
		
		#endregion
		internal bool IsClanChatWindowOpenInSteam( SteamId steamIDClanChat )
		{
			return IsClanChatWindowOpenInSteamDelegatePointer( Self, steamIDClanChat );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool OpenClanChatWindowInSteamDelegate( IntPtr self, SteamId steamIDClanChat );
		private OpenClanChatWindowInSteamDelegate OpenClanChatWindowInSteamDelegatePointer;
		
		#endregion
		internal bool OpenClanChatWindowInSteam( SteamId steamIDClanChat )
		{
			return OpenClanChatWindowInSteamDelegatePointer( Self, steamIDClanChat );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool CloseClanChatWindowInSteamDelegate( IntPtr self, SteamId steamIDClanChat );
		private CloseClanChatWindowInSteamDelegate CloseClanChatWindowInSteamDelegatePointer;
		
		#endregion
		internal bool CloseClanChatWindowInSteam( SteamId steamIDClanChat )
		{
			return CloseClanChatWindowInSteamDelegatePointer( Self, steamIDClanChat );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool SetListenForFriendsMessagesDelegate( IntPtr self, [MarshalAs( UnmanagedType.U1 )] bool bInterceptEnabled );
		private SetListenForFriendsMessagesDelegate SetListenForFriendsMessagesDelegatePointer;
		
		#endregion
		internal bool SetListenForFriendsMessages( [MarshalAs( UnmanagedType.U1 )] bool bInterceptEnabled )
		{
			return SetListenForFriendsMessagesDelegatePointer( Self, bInterceptEnabled );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool ReplyToFriendMessageDelegate( IntPtr self, SteamId steamIDFriend, string pchMsgToSend );
		private ReplyToFriendMessageDelegate ReplyToFriendMessageDelegatePointer;
		
		#endregion
		internal bool ReplyToFriendMessage( SteamId steamIDFriend, string pchMsgToSend )
		{
			return ReplyToFriendMessageDelegatePointer( Self, steamIDFriend, pchMsgToSend );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate int GetFriendMessageDelegate( IntPtr self, SteamId steamIDFriend, int iMessageID, IntPtr pvData, int cubData, ref ChatEntryType peChatEntryType );
		private GetFriendMessageDelegate GetFriendMessageDelegatePointer;
		
		#endregion
		internal int GetFriendMessage( SteamId steamIDFriend, int iMessageID, IntPtr pvData, int cubData, ref ChatEntryType peChatEntryType )
		{
			return GetFriendMessageDelegatePointer( Self, steamIDFriend, iMessageID, pvData, cubData, ref peChatEntryType );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate SteamAPICall_t GetFollowerCountDelegate( IntPtr self, SteamId steamID );
		private GetFollowerCountDelegate GetFollowerCountDelegatePointer;
		
		#endregion
		internal async Task<FriendsGetFollowerCount_t?> GetFollowerCount( SteamId steamID )
		{
			return await (new Result<FriendsGetFollowerCount_t>( GetFollowerCountDelegatePointer( Self, steamID ) )).GetResult();
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate SteamAPICall_t IsFollowingDelegate( IntPtr self, SteamId steamID );
		private IsFollowingDelegate IsFollowingDelegatePointer;
		
		#endregion
		internal async Task<FriendsIsFollowing_t?> IsFollowing( SteamId steamID )
		{
			return await (new Result<FriendsIsFollowing_t>( IsFollowingDelegatePointer( Self, steamID ) )).GetResult();
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate SteamAPICall_t EnumerateFollowingListDelegate( IntPtr self, uint unStartIndex );
		private EnumerateFollowingListDelegate EnumerateFollowingListDelegatePointer;
		
		#endregion
		internal async Task<FriendsEnumerateFollowingList_t?> EnumerateFollowingList( uint unStartIndex )
		{
			return await (new Result<FriendsEnumerateFollowingList_t>( EnumerateFollowingListDelegatePointer( Self, unStartIndex ) )).GetResult();
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool IsClanPublicDelegate( IntPtr self, SteamId steamIDClan );
		private IsClanPublicDelegate IsClanPublicDelegatePointer;
		
		#endregion
		internal bool IsClanPublic( SteamId steamIDClan )
		{
			return IsClanPublicDelegatePointer( Self, steamIDClan );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool IsClanOfficialGameGroupDelegate( IntPtr self, SteamId steamIDClan );
		private IsClanOfficialGameGroupDelegate IsClanOfficialGameGroupDelegatePointer;
		
		#endregion
		internal bool IsClanOfficialGameGroup( SteamId steamIDClan )
		{
			return IsClanOfficialGameGroupDelegatePointer( Self, steamIDClan );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate int GetNumChatsWithUnreadPriorityMessagesDelegate( IntPtr self );
		private GetNumChatsWithUnreadPriorityMessagesDelegate GetNumChatsWithUnreadPriorityMessagesDelegatePointer;
		
		#endregion
		internal int GetNumChatsWithUnreadPriorityMessages()
		{
			return GetNumChatsWithUnreadPriorityMessagesDelegatePointer( Self );
		}
		
	}
}
