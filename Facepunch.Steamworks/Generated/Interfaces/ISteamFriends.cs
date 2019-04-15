using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using SteamNative;


namespace Steamworks.Internal
{
	public class ISteamFriends : BaseSteamInterface
	{
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
		public delegate IntPtr GetPersonaNameDelegate( IntPtr self );
		private GetPersonaNameDelegate GetPersonaNameDelegatePointer;
		
		#endregion
		public string GetPersonaName()
		{
			return GetString( GetPersonaNameDelegatePointer( Self ) );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		public delegate SteamAPICall_t SetPersonaNameDelegate( IntPtr self, string pchPersonaName );
		private SetPersonaNameDelegate SetPersonaNameDelegatePointer;
		
		#endregion
		public async Task<SetPersonaNameResponse_t?> SetPersonaName( string pchPersonaName )
		{
			return await (new Result<SetPersonaNameResponse_t>( SetPersonaNameDelegatePointer( Self, pchPersonaName ) )).GetResult();
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		public delegate PersonaState GetPersonaStateDelegate( IntPtr self );
		private GetPersonaStateDelegate GetPersonaStateDelegatePointer;
		
		#endregion
		public PersonaState GetPersonaState()
		{
			return GetPersonaStateDelegatePointer( Self );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		public delegate int GetFriendCountDelegate( IntPtr self, int iFriendFlags );
		private GetFriendCountDelegate GetFriendCountDelegatePointer;
		
		#endregion
		public int GetFriendCount( int iFriendFlags )
		{
			return GetFriendCountDelegatePointer( Self, iFriendFlags );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		public delegate void GetFriendByIndexDelegate( IntPtr self, ref CSteamID retVal, int iFriend, int iFriendFlags );
		private GetFriendByIndexDelegate GetFriendByIndexDelegatePointer;
		
		#endregion
		public CSteamID GetFriendByIndex( int iFriend, int iFriendFlags )
		{
			var retVal = default( CSteamID );
			GetFriendByIndexDelegatePointer( Self, ref retVal, iFriend, iFriendFlags );
			return retVal;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		public delegate FriendRelationship GetFriendRelationshipDelegate( IntPtr self, CSteamID steamIDFriend );
		private GetFriendRelationshipDelegate GetFriendRelationshipDelegatePointer;
		
		#endregion
		public FriendRelationship GetFriendRelationship( CSteamID steamIDFriend )
		{
			return GetFriendRelationshipDelegatePointer( Self, steamIDFriend );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		public delegate PersonaState GetFriendPersonaStateDelegate( IntPtr self, CSteamID steamIDFriend );
		private GetFriendPersonaStateDelegate GetFriendPersonaStateDelegatePointer;
		
		#endregion
		public PersonaState GetFriendPersonaState( CSteamID steamIDFriend )
		{
			return GetFriendPersonaStateDelegatePointer( Self, steamIDFriend );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		public delegate IntPtr GetFriendPersonaNameDelegate( IntPtr self, CSteamID steamIDFriend );
		private GetFriendPersonaNameDelegate GetFriendPersonaNameDelegatePointer;
		
		#endregion
		public string GetFriendPersonaName( CSteamID steamIDFriend )
		{
			return GetString( GetFriendPersonaNameDelegatePointer( Self, steamIDFriend ) );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		public delegate bool GetFriendGamePlayedDelegate( IntPtr self, CSteamID steamIDFriend, ref FriendGameInfo_t pFriendGameInfo );
		private GetFriendGamePlayedDelegate GetFriendGamePlayedDelegatePointer;
		
		#endregion
		public bool GetFriendGamePlayed( CSteamID steamIDFriend, ref FriendGameInfo_t pFriendGameInfo )
		{
			return GetFriendGamePlayedDelegatePointer( Self, steamIDFriend, ref pFriendGameInfo );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		public delegate IntPtr GetFriendPersonaNameHistoryDelegate( IntPtr self, CSteamID steamIDFriend, int iPersonaName );
		private GetFriendPersonaNameHistoryDelegate GetFriendPersonaNameHistoryDelegatePointer;
		
		#endregion
		public string GetFriendPersonaNameHistory( CSteamID steamIDFriend, int iPersonaName )
		{
			return GetString( GetFriendPersonaNameHistoryDelegatePointer( Self, steamIDFriend, iPersonaName ) );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		public delegate int GetFriendSteamLevelDelegate( IntPtr self, CSteamID steamIDFriend );
		private GetFriendSteamLevelDelegate GetFriendSteamLevelDelegatePointer;
		
		#endregion
		public int GetFriendSteamLevel( CSteamID steamIDFriend )
		{
			return GetFriendSteamLevelDelegatePointer( Self, steamIDFriend );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		public delegate IntPtr GetPlayerNicknameDelegate( IntPtr self, CSteamID steamIDPlayer );
		private GetPlayerNicknameDelegate GetPlayerNicknameDelegatePointer;
		
		#endregion
		public string GetPlayerNickname( CSteamID steamIDPlayer )
		{
			return GetString( GetPlayerNicknameDelegatePointer( Self, steamIDPlayer ) );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		public delegate int GetFriendsGroupCountDelegate( IntPtr self );
		private GetFriendsGroupCountDelegate GetFriendsGroupCountDelegatePointer;
		
		#endregion
		public int GetFriendsGroupCount()
		{
			return GetFriendsGroupCountDelegatePointer( Self );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		public delegate FriendsGroupID_t GetFriendsGroupIDByIndexDelegate( IntPtr self, int iFG );
		private GetFriendsGroupIDByIndexDelegate GetFriendsGroupIDByIndexDelegatePointer;
		
		#endregion
		public FriendsGroupID_t GetFriendsGroupIDByIndex( int iFG )
		{
			return GetFriendsGroupIDByIndexDelegatePointer( Self, iFG );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		public delegate IntPtr GetFriendsGroupNameDelegate( IntPtr self, FriendsGroupID_t friendsGroupID );
		private GetFriendsGroupNameDelegate GetFriendsGroupNameDelegatePointer;
		
		#endregion
		public string GetFriendsGroupName( FriendsGroupID_t friendsGroupID )
		{
			return GetString( GetFriendsGroupNameDelegatePointer( Self, friendsGroupID ) );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		public delegate int GetFriendsGroupMembersCountDelegate( IntPtr self, FriendsGroupID_t friendsGroupID );
		private GetFriendsGroupMembersCountDelegate GetFriendsGroupMembersCountDelegatePointer;
		
		#endregion
		public int GetFriendsGroupMembersCount( FriendsGroupID_t friendsGroupID )
		{
			return GetFriendsGroupMembersCountDelegatePointer( Self, friendsGroupID );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		public delegate void GetFriendsGroupMembersListDelegate( IntPtr self, FriendsGroupID_t friendsGroupID, [In,Out] CSteamID[]  pOutSteamIDMembers, int nMembersCount );
		private GetFriendsGroupMembersListDelegate GetFriendsGroupMembersListDelegatePointer;
		
		#endregion
		public void GetFriendsGroupMembersList( FriendsGroupID_t friendsGroupID, [In,Out] CSteamID[]  pOutSteamIDMembers, int nMembersCount )
		{
			GetFriendsGroupMembersListDelegatePointer( Self, friendsGroupID, pOutSteamIDMembers, nMembersCount );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		public delegate bool HasFriendDelegate( IntPtr self, CSteamID steamIDFriend, int iFriendFlags );
		private HasFriendDelegate HasFriendDelegatePointer;
		
		#endregion
		public bool HasFriend( CSteamID steamIDFriend, int iFriendFlags )
		{
			return HasFriendDelegatePointer( Self, steamIDFriend, iFriendFlags );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		public delegate int GetClanCountDelegate( IntPtr self );
		private GetClanCountDelegate GetClanCountDelegatePointer;
		
		#endregion
		public int GetClanCount()
		{
			return GetClanCountDelegatePointer( Self );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		public delegate void GetClanByIndexDelegate( IntPtr self, ref CSteamID retVal, int iClan );
		private GetClanByIndexDelegate GetClanByIndexDelegatePointer;
		
		#endregion
		public CSteamID GetClanByIndex( int iClan )
		{
			var retVal = default( CSteamID );
			GetClanByIndexDelegatePointer( Self, ref retVal, iClan );
			return retVal;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		public delegate IntPtr GetClanNameDelegate( IntPtr self, CSteamID steamIDClan );
		private GetClanNameDelegate GetClanNameDelegatePointer;
		
		#endregion
		public string GetClanName( CSteamID steamIDClan )
		{
			return GetString( GetClanNameDelegatePointer( Self, steamIDClan ) );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		public delegate IntPtr GetClanTagDelegate( IntPtr self, CSteamID steamIDClan );
		private GetClanTagDelegate GetClanTagDelegatePointer;
		
		#endregion
		public string GetClanTag( CSteamID steamIDClan )
		{
			return GetString( GetClanTagDelegatePointer( Self, steamIDClan ) );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		public delegate bool GetClanActivityCountsDelegate( IntPtr self, CSteamID steamIDClan, ref int pnOnline, ref int pnInGame, ref int pnChatting );
		private GetClanActivityCountsDelegate GetClanActivityCountsDelegatePointer;
		
		#endregion
		public bool GetClanActivityCounts( CSteamID steamIDClan, ref int pnOnline, ref int pnInGame, ref int pnChatting )
		{
			return GetClanActivityCountsDelegatePointer( Self, steamIDClan, ref pnOnline, ref pnInGame, ref pnChatting );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		public delegate SteamAPICall_t DownloadClanActivityCountsDelegate( IntPtr self, [In,Out] CSteamID[]  psteamIDClans, int cClansToRequest );
		private DownloadClanActivityCountsDelegate DownloadClanActivityCountsDelegatePointer;
		
		#endregion
		public async Task<DownloadClanActivityCountsResult_t?> DownloadClanActivityCounts( [In,Out] CSteamID[]  psteamIDClans, int cClansToRequest )
		{
			return await (new Result<DownloadClanActivityCountsResult_t>( DownloadClanActivityCountsDelegatePointer( Self, psteamIDClans, cClansToRequest ) )).GetResult();
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		public delegate int GetFriendCountFromSourceDelegate( IntPtr self, CSteamID steamIDSource );
		private GetFriendCountFromSourceDelegate GetFriendCountFromSourceDelegatePointer;
		
		#endregion
		public int GetFriendCountFromSource( CSteamID steamIDSource )
		{
			return GetFriendCountFromSourceDelegatePointer( Self, steamIDSource );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		public delegate void GetFriendFromSourceByIndexDelegate( IntPtr self, ref CSteamID retVal, CSteamID steamIDSource, int iFriend );
		private GetFriendFromSourceByIndexDelegate GetFriendFromSourceByIndexDelegatePointer;
		
		#endregion
		public CSteamID GetFriendFromSourceByIndex( CSteamID steamIDSource, int iFriend )
		{
			var retVal = default( CSteamID );
			GetFriendFromSourceByIndexDelegatePointer( Self, ref retVal, steamIDSource, iFriend );
			return retVal;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		public delegate bool IsUserInSourceDelegate( IntPtr self, CSteamID steamIDUser, CSteamID steamIDSource );
		private IsUserInSourceDelegate IsUserInSourceDelegatePointer;
		
		#endregion
		public bool IsUserInSource( CSteamID steamIDUser, CSteamID steamIDSource )
		{
			return IsUserInSourceDelegatePointer( Self, steamIDUser, steamIDSource );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		public delegate void SetInGameVoiceSpeakingDelegate( IntPtr self, CSteamID steamIDUser, [MarshalAs( UnmanagedType.U1 )] bool bSpeaking );
		private SetInGameVoiceSpeakingDelegate SetInGameVoiceSpeakingDelegatePointer;
		
		#endregion
		public void SetInGameVoiceSpeaking( CSteamID steamIDUser, [MarshalAs( UnmanagedType.U1 )] bool bSpeaking )
		{
			SetInGameVoiceSpeakingDelegatePointer( Self, steamIDUser, bSpeaking );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		public delegate void ActivateGameOverlayDelegate( IntPtr self, string pchDialog );
		private ActivateGameOverlayDelegate ActivateGameOverlayDelegatePointer;
		
		#endregion
		public void ActivateGameOverlay( string pchDialog )
		{
			ActivateGameOverlayDelegatePointer( Self, pchDialog );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		public delegate void ActivateGameOverlayToUserDelegate( IntPtr self, string pchDialog, CSteamID steamID );
		private ActivateGameOverlayToUserDelegate ActivateGameOverlayToUserDelegatePointer;
		
		#endregion
		public void ActivateGameOverlayToUser( string pchDialog, CSteamID steamID )
		{
			ActivateGameOverlayToUserDelegatePointer( Self, pchDialog, steamID );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		public delegate void ActivateGameOverlayToWebPageDelegate( IntPtr self, string pchURL, ActivateGameOverlayToWebPageMode eMode );
		private ActivateGameOverlayToWebPageDelegate ActivateGameOverlayToWebPageDelegatePointer;
		
		#endregion
		public void ActivateGameOverlayToWebPage( string pchURL, ActivateGameOverlayToWebPageMode eMode )
		{
			ActivateGameOverlayToWebPageDelegatePointer( Self, pchURL, eMode );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		public delegate void ActivateGameOverlayToStoreDelegate( IntPtr self, AppId_t nAppID, OverlayToStoreFlag eFlag );
		private ActivateGameOverlayToStoreDelegate ActivateGameOverlayToStoreDelegatePointer;
		
		#endregion
		public void ActivateGameOverlayToStore( AppId_t nAppID, OverlayToStoreFlag eFlag )
		{
			ActivateGameOverlayToStoreDelegatePointer( Self, nAppID, eFlag );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		public delegate void SetPlayedWithDelegate( IntPtr self, CSteamID steamIDUserPlayedWith );
		private SetPlayedWithDelegate SetPlayedWithDelegatePointer;
		
		#endregion
		public void SetPlayedWith( CSteamID steamIDUserPlayedWith )
		{
			SetPlayedWithDelegatePointer( Self, steamIDUserPlayedWith );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		public delegate void ActivateGameOverlayInviteDialogDelegate( IntPtr self, CSteamID steamIDLobby );
		private ActivateGameOverlayInviteDialogDelegate ActivateGameOverlayInviteDialogDelegatePointer;
		
		#endregion
		public void ActivateGameOverlayInviteDialog( CSteamID steamIDLobby )
		{
			ActivateGameOverlayInviteDialogDelegatePointer( Self, steamIDLobby );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		public delegate int GetSmallFriendAvatarDelegate( IntPtr self, CSteamID steamIDFriend );
		private GetSmallFriendAvatarDelegate GetSmallFriendAvatarDelegatePointer;
		
		#endregion
		public int GetSmallFriendAvatar( CSteamID steamIDFriend )
		{
			return GetSmallFriendAvatarDelegatePointer( Self, steamIDFriend );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		public delegate int GetMediumFriendAvatarDelegate( IntPtr self, CSteamID steamIDFriend );
		private GetMediumFriendAvatarDelegate GetMediumFriendAvatarDelegatePointer;
		
		#endregion
		public int GetMediumFriendAvatar( CSteamID steamIDFriend )
		{
			return GetMediumFriendAvatarDelegatePointer( Self, steamIDFriend );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		public delegate int GetLargeFriendAvatarDelegate( IntPtr self, CSteamID steamIDFriend );
		private GetLargeFriendAvatarDelegate GetLargeFriendAvatarDelegatePointer;
		
		#endregion
		public int GetLargeFriendAvatar( CSteamID steamIDFriend )
		{
			return GetLargeFriendAvatarDelegatePointer( Self, steamIDFriend );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		public delegate bool RequestUserInformationDelegate( IntPtr self, CSteamID steamIDUser, [MarshalAs( UnmanagedType.U1 )] bool bRequireNameOnly );
		private RequestUserInformationDelegate RequestUserInformationDelegatePointer;
		
		#endregion
		public bool RequestUserInformation( CSteamID steamIDUser, [MarshalAs( UnmanagedType.U1 )] bool bRequireNameOnly )
		{
			return RequestUserInformationDelegatePointer( Self, steamIDUser, bRequireNameOnly );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		public delegate SteamAPICall_t RequestClanOfficerListDelegate( IntPtr self, CSteamID steamIDClan );
		private RequestClanOfficerListDelegate RequestClanOfficerListDelegatePointer;
		
		#endregion
		public async Task<ClanOfficerListResponse_t?> RequestClanOfficerList( CSteamID steamIDClan )
		{
			return await (new Result<ClanOfficerListResponse_t>( RequestClanOfficerListDelegatePointer( Self, steamIDClan ) )).GetResult();
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		public delegate void GetClanOwnerDelegate( IntPtr self, ref CSteamID retVal, CSteamID steamIDClan );
		private GetClanOwnerDelegate GetClanOwnerDelegatePointer;
		
		#endregion
		public CSteamID GetClanOwner( CSteamID steamIDClan )
		{
			var retVal = default( CSteamID );
			GetClanOwnerDelegatePointer( Self, ref retVal, steamIDClan );
			return retVal;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		public delegate int GetClanOfficerCountDelegate( IntPtr self, CSteamID steamIDClan );
		private GetClanOfficerCountDelegate GetClanOfficerCountDelegatePointer;
		
		#endregion
		public int GetClanOfficerCount( CSteamID steamIDClan )
		{
			return GetClanOfficerCountDelegatePointer( Self, steamIDClan );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		public delegate void GetClanOfficerByIndexDelegate( IntPtr self, ref CSteamID retVal, CSteamID steamIDClan, int iOfficer );
		private GetClanOfficerByIndexDelegate GetClanOfficerByIndexDelegatePointer;
		
		#endregion
		public CSteamID GetClanOfficerByIndex( CSteamID steamIDClan, int iOfficer )
		{
			var retVal = default( CSteamID );
			GetClanOfficerByIndexDelegatePointer( Self, ref retVal, steamIDClan, iOfficer );
			return retVal;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		public delegate uint GetUserRestrictionsDelegate( IntPtr self );
		private GetUserRestrictionsDelegate GetUserRestrictionsDelegatePointer;
		
		#endregion
		public uint GetUserRestrictions()
		{
			return GetUserRestrictionsDelegatePointer( Self );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		public delegate bool SetRichPresenceDelegate( IntPtr self, string pchKey, string pchValue );
		private SetRichPresenceDelegate SetRichPresenceDelegatePointer;
		
		#endregion
		public bool SetRichPresence( string pchKey, string pchValue )
		{
			return SetRichPresenceDelegatePointer( Self, pchKey, pchValue );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		public delegate void ClearRichPresenceDelegate( IntPtr self );
		private ClearRichPresenceDelegate ClearRichPresenceDelegatePointer;
		
		#endregion
		public void ClearRichPresence()
		{
			ClearRichPresenceDelegatePointer( Self );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		public delegate IntPtr GetFriendRichPresenceDelegate( IntPtr self, CSteamID steamIDFriend, string pchKey );
		private GetFriendRichPresenceDelegate GetFriendRichPresenceDelegatePointer;
		
		#endregion
		public string GetFriendRichPresence( CSteamID steamIDFriend, string pchKey )
		{
			return GetString( GetFriendRichPresenceDelegatePointer( Self, steamIDFriend, pchKey ) );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		public delegate int GetFriendRichPresenceKeyCountDelegate( IntPtr self, CSteamID steamIDFriend );
		private GetFriendRichPresenceKeyCountDelegate GetFriendRichPresenceKeyCountDelegatePointer;
		
		#endregion
		public int GetFriendRichPresenceKeyCount( CSteamID steamIDFriend )
		{
			return GetFriendRichPresenceKeyCountDelegatePointer( Self, steamIDFriend );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		public delegate IntPtr GetFriendRichPresenceKeyByIndexDelegate( IntPtr self, CSteamID steamIDFriend, int iKey );
		private GetFriendRichPresenceKeyByIndexDelegate GetFriendRichPresenceKeyByIndexDelegatePointer;
		
		#endregion
		public string GetFriendRichPresenceKeyByIndex( CSteamID steamIDFriend, int iKey )
		{
			return GetString( GetFriendRichPresenceKeyByIndexDelegatePointer( Self, steamIDFriend, iKey ) );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		public delegate void RequestFriendRichPresenceDelegate( IntPtr self, CSteamID steamIDFriend );
		private RequestFriendRichPresenceDelegate RequestFriendRichPresenceDelegatePointer;
		
		#endregion
		public void RequestFriendRichPresence( CSteamID steamIDFriend )
		{
			RequestFriendRichPresenceDelegatePointer( Self, steamIDFriend );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		public delegate bool InviteUserToGameDelegate( IntPtr self, CSteamID steamIDFriend, string pchConnectString );
		private InviteUserToGameDelegate InviteUserToGameDelegatePointer;
		
		#endregion
		public bool InviteUserToGame( CSteamID steamIDFriend, string pchConnectString )
		{
			return InviteUserToGameDelegatePointer( Self, steamIDFriend, pchConnectString );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		public delegate int GetCoplayFriendCountDelegate( IntPtr self );
		private GetCoplayFriendCountDelegate GetCoplayFriendCountDelegatePointer;
		
		#endregion
		public int GetCoplayFriendCount()
		{
			return GetCoplayFriendCountDelegatePointer( Self );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		public delegate void GetCoplayFriendDelegate( IntPtr self, ref CSteamID retVal, int iCoplayFriend );
		private GetCoplayFriendDelegate GetCoplayFriendDelegatePointer;
		
		#endregion
		public CSteamID GetCoplayFriend( int iCoplayFriend )
		{
			var retVal = default( CSteamID );
			GetCoplayFriendDelegatePointer( Self, ref retVal, iCoplayFriend );
			return retVal;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		public delegate int GetFriendCoplayTimeDelegate( IntPtr self, CSteamID steamIDFriend );
		private GetFriendCoplayTimeDelegate GetFriendCoplayTimeDelegatePointer;
		
		#endregion
		public int GetFriendCoplayTime( CSteamID steamIDFriend )
		{
			return GetFriendCoplayTimeDelegatePointer( Self, steamIDFriend );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		public delegate AppId_t GetFriendCoplayGameDelegate( IntPtr self, CSteamID steamIDFriend );
		private GetFriendCoplayGameDelegate GetFriendCoplayGameDelegatePointer;
		
		#endregion
		public AppId_t GetFriendCoplayGame( CSteamID steamIDFriend )
		{
			return GetFriendCoplayGameDelegatePointer( Self, steamIDFriend );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		public delegate SteamAPICall_t JoinClanChatRoomDelegate( IntPtr self, CSteamID steamIDClan );
		private JoinClanChatRoomDelegate JoinClanChatRoomDelegatePointer;
		
		#endregion
		public async Task<JoinClanChatRoomCompletionResult_t?> JoinClanChatRoom( CSteamID steamIDClan )
		{
			return await (new Result<JoinClanChatRoomCompletionResult_t>( JoinClanChatRoomDelegatePointer( Self, steamIDClan ) )).GetResult();
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		public delegate bool LeaveClanChatRoomDelegate( IntPtr self, CSteamID steamIDClan );
		private LeaveClanChatRoomDelegate LeaveClanChatRoomDelegatePointer;
		
		#endregion
		public bool LeaveClanChatRoom( CSteamID steamIDClan )
		{
			return LeaveClanChatRoomDelegatePointer( Self, steamIDClan );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		public delegate int GetClanChatMemberCountDelegate( IntPtr self, CSteamID steamIDClan );
		private GetClanChatMemberCountDelegate GetClanChatMemberCountDelegatePointer;
		
		#endregion
		public int GetClanChatMemberCount( CSteamID steamIDClan )
		{
			return GetClanChatMemberCountDelegatePointer( Self, steamIDClan );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		public delegate void GetChatMemberByIndexDelegate( IntPtr self, ref CSteamID retVal, CSteamID steamIDClan, int iUser );
		private GetChatMemberByIndexDelegate GetChatMemberByIndexDelegatePointer;
		
		#endregion
		public CSteamID GetChatMemberByIndex( CSteamID steamIDClan, int iUser )
		{
			var retVal = default( CSteamID );
			GetChatMemberByIndexDelegatePointer( Self, ref retVal, steamIDClan, iUser );
			return retVal;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		public delegate bool SendClanChatMessageDelegate( IntPtr self, CSteamID steamIDClanChat, string pchText );
		private SendClanChatMessageDelegate SendClanChatMessageDelegatePointer;
		
		#endregion
		public bool SendClanChatMessage( CSteamID steamIDClanChat, string pchText )
		{
			return SendClanChatMessageDelegatePointer( Self, steamIDClanChat, pchText );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		public delegate int GetClanChatMessageDelegate( IntPtr self, CSteamID steamIDClanChat, int iMessage, IntPtr prgchText, int cchTextMax, ref ChatEntryType peChatEntryType, ref CSteamID psteamidChatter );
		private GetClanChatMessageDelegate GetClanChatMessageDelegatePointer;
		
		#endregion
		public int GetClanChatMessage( CSteamID steamIDClanChat, int iMessage, IntPtr prgchText, int cchTextMax, ref ChatEntryType peChatEntryType, ref CSteamID psteamidChatter )
		{
			return GetClanChatMessageDelegatePointer( Self, steamIDClanChat, iMessage, prgchText, cchTextMax, ref peChatEntryType, ref psteamidChatter );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		public delegate bool IsClanChatAdminDelegate( IntPtr self, CSteamID steamIDClanChat, CSteamID steamIDUser );
		private IsClanChatAdminDelegate IsClanChatAdminDelegatePointer;
		
		#endregion
		public bool IsClanChatAdmin( CSteamID steamIDClanChat, CSteamID steamIDUser )
		{
			return IsClanChatAdminDelegatePointer( Self, steamIDClanChat, steamIDUser );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		public delegate bool IsClanChatWindowOpenInSteamDelegate( IntPtr self, CSteamID steamIDClanChat );
		private IsClanChatWindowOpenInSteamDelegate IsClanChatWindowOpenInSteamDelegatePointer;
		
		#endregion
		public bool IsClanChatWindowOpenInSteam( CSteamID steamIDClanChat )
		{
			return IsClanChatWindowOpenInSteamDelegatePointer( Self, steamIDClanChat );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		public delegate bool OpenClanChatWindowInSteamDelegate( IntPtr self, CSteamID steamIDClanChat );
		private OpenClanChatWindowInSteamDelegate OpenClanChatWindowInSteamDelegatePointer;
		
		#endregion
		public bool OpenClanChatWindowInSteam( CSteamID steamIDClanChat )
		{
			return OpenClanChatWindowInSteamDelegatePointer( Self, steamIDClanChat );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		public delegate bool CloseClanChatWindowInSteamDelegate( IntPtr self, CSteamID steamIDClanChat );
		private CloseClanChatWindowInSteamDelegate CloseClanChatWindowInSteamDelegatePointer;
		
		#endregion
		public bool CloseClanChatWindowInSteam( CSteamID steamIDClanChat )
		{
			return CloseClanChatWindowInSteamDelegatePointer( Self, steamIDClanChat );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		public delegate bool SetListenForFriendsMessagesDelegate( IntPtr self, [MarshalAs( UnmanagedType.U1 )] bool bInterceptEnabled );
		private SetListenForFriendsMessagesDelegate SetListenForFriendsMessagesDelegatePointer;
		
		#endregion
		public bool SetListenForFriendsMessages( [MarshalAs( UnmanagedType.U1 )] bool bInterceptEnabled )
		{
			return SetListenForFriendsMessagesDelegatePointer( Self, bInterceptEnabled );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		public delegate bool ReplyToFriendMessageDelegate( IntPtr self, CSteamID steamIDFriend, string pchMsgToSend );
		private ReplyToFriendMessageDelegate ReplyToFriendMessageDelegatePointer;
		
		#endregion
		public bool ReplyToFriendMessage( CSteamID steamIDFriend, string pchMsgToSend )
		{
			return ReplyToFriendMessageDelegatePointer( Self, steamIDFriend, pchMsgToSend );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		public delegate int GetFriendMessageDelegate( IntPtr self, CSteamID steamIDFriend, int iMessageID, IntPtr pvData, int cubData, ref ChatEntryType peChatEntryType );
		private GetFriendMessageDelegate GetFriendMessageDelegatePointer;
		
		#endregion
		public int GetFriendMessage( CSteamID steamIDFriend, int iMessageID, IntPtr pvData, int cubData, ref ChatEntryType peChatEntryType )
		{
			return GetFriendMessageDelegatePointer( Self, steamIDFriend, iMessageID, pvData, cubData, ref peChatEntryType );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		public delegate SteamAPICall_t GetFollowerCountDelegate( IntPtr self, CSteamID steamID );
		private GetFollowerCountDelegate GetFollowerCountDelegatePointer;
		
		#endregion
		public async Task<FriendsGetFollowerCount_t?> GetFollowerCount( CSteamID steamID )
		{
			return await (new Result<FriendsGetFollowerCount_t>( GetFollowerCountDelegatePointer( Self, steamID ) )).GetResult();
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		public delegate SteamAPICall_t IsFollowingDelegate( IntPtr self, CSteamID steamID );
		private IsFollowingDelegate IsFollowingDelegatePointer;
		
		#endregion
		public async Task<FriendsIsFollowing_t?> IsFollowing( CSteamID steamID )
		{
			return await (new Result<FriendsIsFollowing_t>( IsFollowingDelegatePointer( Self, steamID ) )).GetResult();
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		public delegate SteamAPICall_t EnumerateFollowingListDelegate( IntPtr self, uint unStartIndex );
		private EnumerateFollowingListDelegate EnumerateFollowingListDelegatePointer;
		
		#endregion
		public async Task<FriendsEnumerateFollowingList_t?> EnumerateFollowingList( uint unStartIndex )
		{
			return await (new Result<FriendsEnumerateFollowingList_t>( EnumerateFollowingListDelegatePointer( Self, unStartIndex ) )).GetResult();
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		public delegate bool IsClanPublicDelegate( IntPtr self, CSteamID steamIDClan );
		private IsClanPublicDelegate IsClanPublicDelegatePointer;
		
		#endregion
		public bool IsClanPublic( CSteamID steamIDClan )
		{
			return IsClanPublicDelegatePointer( Self, steamIDClan );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		public delegate bool IsClanOfficialGameGroupDelegate( IntPtr self, CSteamID steamIDClan );
		private IsClanOfficialGameGroupDelegate IsClanOfficialGameGroupDelegatePointer;
		
		#endregion
		public bool IsClanOfficialGameGroup( CSteamID steamIDClan )
		{
			return IsClanOfficialGameGroupDelegatePointer( Self, steamIDClan );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		public delegate int GetNumChatsWithUnreadPriorityMessagesDelegate( IntPtr self );
		private GetNumChatsWithUnreadPriorityMessagesDelegate GetNumChatsWithUnreadPriorityMessagesDelegatePointer;
		
		#endregion
		public int GetNumChatsWithUnreadPriorityMessages()
		{
			return GetNumChatsWithUnreadPriorityMessagesDelegatePointer( Self );
		}
		
	}
}
