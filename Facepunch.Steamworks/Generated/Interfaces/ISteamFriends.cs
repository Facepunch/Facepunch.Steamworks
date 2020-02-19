using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Steamworks.Data;


namespace Steamworks
{
	internal class ISteamFriends : SteamInterface
	{
		
		internal ISteamFriends( bool IsGameServer )
		{
			SetupInterface( IsGameServer );
		}
		
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_SteamFriends_v017")]
		internal static extern IntPtr SteamAPI_SteamFriends_v017();
		public override IntPtr GetUserInterfacePointer() => SteamAPI_SteamFriends_v017();
		
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamFriends_GetPersonaName")]
		private static extern Utf8StringPointer _GetPersonaName( IntPtr self );
		
		#endregion
		internal string GetPersonaName()
		{
			var returnValue = _GetPersonaName( Self );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamFriends_SetPersonaName")]
		private static extern SteamAPICall_t _SetPersonaName( IntPtr self, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchPersonaName );
		
		#endregion
		internal CallbackResult<SetPersonaNameResponse_t> SetPersonaName( [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchPersonaName )
		{
			var returnValue = _SetPersonaName( Self, pchPersonaName );
			return new CallbackResult<SetPersonaNameResponse_t>( returnValue );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamFriends_GetPersonaState")]
		private static extern FriendState _GetPersonaState( IntPtr self );
		
		#endregion
		internal FriendState GetPersonaState()
		{
			var returnValue = _GetPersonaState( Self );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamFriends_GetFriendCount")]
		private static extern int _GetFriendCount( IntPtr self, int iFriendFlags );
		
		#endregion
		internal int GetFriendCount( int iFriendFlags )
		{
			var returnValue = _GetFriendCount( Self, iFriendFlags );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamFriends_GetFriendByIndex")]
		private static extern SteamId _GetFriendByIndex( IntPtr self, int iFriend, int iFriendFlags );
		
		#endregion
		internal SteamId GetFriendByIndex( int iFriend, int iFriendFlags )
		{
			var returnValue = _GetFriendByIndex( Self, iFriend, iFriendFlags );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamFriends_GetFriendRelationship")]
		private static extern Relationship _GetFriendRelationship( IntPtr self, SteamId steamIDFriend );
		
		#endregion
		internal Relationship GetFriendRelationship( SteamId steamIDFriend )
		{
			var returnValue = _GetFriendRelationship( Self, steamIDFriend );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamFriends_GetFriendPersonaState")]
		private static extern FriendState _GetFriendPersonaState( IntPtr self, SteamId steamIDFriend );
		
		#endregion
		internal FriendState GetFriendPersonaState( SteamId steamIDFriend )
		{
			var returnValue = _GetFriendPersonaState( Self, steamIDFriend );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamFriends_GetFriendPersonaName")]
		private static extern Utf8StringPointer _GetFriendPersonaName( IntPtr self, SteamId steamIDFriend );
		
		#endregion
		internal string GetFriendPersonaName( SteamId steamIDFriend )
		{
			var returnValue = _GetFriendPersonaName( Self, steamIDFriend );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamFriends_GetFriendGamePlayed")]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _GetFriendGamePlayed( IntPtr self, SteamId steamIDFriend, ref FriendGameInfo_t pFriendGameInfo );
		
		#endregion
		internal bool GetFriendGamePlayed( SteamId steamIDFriend, ref FriendGameInfo_t pFriendGameInfo )
		{
			var returnValue = _GetFriendGamePlayed( Self, steamIDFriend, ref pFriendGameInfo );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamFriends_GetFriendPersonaNameHistory")]
		private static extern Utf8StringPointer _GetFriendPersonaNameHistory( IntPtr self, SteamId steamIDFriend, int iPersonaName );
		
		#endregion
		internal string GetFriendPersonaNameHistory( SteamId steamIDFriend, int iPersonaName )
		{
			var returnValue = _GetFriendPersonaNameHistory( Self, steamIDFriend, iPersonaName );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamFriends_GetFriendSteamLevel")]
		private static extern int _GetFriendSteamLevel( IntPtr self, SteamId steamIDFriend );
		
		#endregion
		internal int GetFriendSteamLevel( SteamId steamIDFriend )
		{
			var returnValue = _GetFriendSteamLevel( Self, steamIDFriend );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamFriends_GetPlayerNickname")]
		private static extern Utf8StringPointer _GetPlayerNickname( IntPtr self, SteamId steamIDPlayer );
		
		#endregion
		internal string GetPlayerNickname( SteamId steamIDPlayer )
		{
			var returnValue = _GetPlayerNickname( Self, steamIDPlayer );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamFriends_GetFriendsGroupCount")]
		private static extern int _GetFriendsGroupCount( IntPtr self );
		
		#endregion
		internal int GetFriendsGroupCount()
		{
			var returnValue = _GetFriendsGroupCount( Self );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamFriends_GetFriendsGroupIDByIndex")]
		private static extern FriendsGroupID_t _GetFriendsGroupIDByIndex( IntPtr self, int iFG );
		
		#endregion
		internal FriendsGroupID_t GetFriendsGroupIDByIndex( int iFG )
		{
			var returnValue = _GetFriendsGroupIDByIndex( Self, iFG );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamFriends_GetFriendsGroupName")]
		private static extern Utf8StringPointer _GetFriendsGroupName( IntPtr self, FriendsGroupID_t friendsGroupID );
		
		#endregion
		internal string GetFriendsGroupName( FriendsGroupID_t friendsGroupID )
		{
			var returnValue = _GetFriendsGroupName( Self, friendsGroupID );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamFriends_GetFriendsGroupMembersCount")]
		private static extern int _GetFriendsGroupMembersCount( IntPtr self, FriendsGroupID_t friendsGroupID );
		
		#endregion
		internal int GetFriendsGroupMembersCount( FriendsGroupID_t friendsGroupID )
		{
			var returnValue = _GetFriendsGroupMembersCount( Self, friendsGroupID );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamFriends_GetFriendsGroupMembersList")]
		private static extern void _GetFriendsGroupMembersList( IntPtr self, FriendsGroupID_t friendsGroupID, [In,Out] SteamId[]  pOutSteamIDMembers, int nMembersCount );
		
		#endregion
		internal void GetFriendsGroupMembersList( FriendsGroupID_t friendsGroupID, [In,Out] SteamId[]  pOutSteamIDMembers, int nMembersCount )
		{
			_GetFriendsGroupMembersList( Self, friendsGroupID, pOutSteamIDMembers, nMembersCount );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamFriends_HasFriend")]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _HasFriend( IntPtr self, SteamId steamIDFriend, int iFriendFlags );
		
		#endregion
		internal bool HasFriend( SteamId steamIDFriend, int iFriendFlags )
		{
			var returnValue = _HasFriend( Self, steamIDFriend, iFriendFlags );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamFriends_GetClanCount")]
		private static extern int _GetClanCount( IntPtr self );
		
		#endregion
		internal int GetClanCount()
		{
			var returnValue = _GetClanCount( Self );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamFriends_GetClanByIndex")]
		private static extern SteamId _GetClanByIndex( IntPtr self, int iClan );
		
		#endregion
		internal SteamId GetClanByIndex( int iClan )
		{
			var returnValue = _GetClanByIndex( Self, iClan );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamFriends_GetClanName")]
		private static extern Utf8StringPointer _GetClanName( IntPtr self, SteamId steamIDClan );
		
		#endregion
		internal string GetClanName( SteamId steamIDClan )
		{
			var returnValue = _GetClanName( Self, steamIDClan );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamFriends_GetClanTag")]
		private static extern Utf8StringPointer _GetClanTag( IntPtr self, SteamId steamIDClan );
		
		#endregion
		internal string GetClanTag( SteamId steamIDClan )
		{
			var returnValue = _GetClanTag( Self, steamIDClan );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamFriends_GetClanActivityCounts")]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _GetClanActivityCounts( IntPtr self, SteamId steamIDClan, ref int pnOnline, ref int pnInGame, ref int pnChatting );
		
		#endregion
		internal bool GetClanActivityCounts( SteamId steamIDClan, ref int pnOnline, ref int pnInGame, ref int pnChatting )
		{
			var returnValue = _GetClanActivityCounts( Self, steamIDClan, ref pnOnline, ref pnInGame, ref pnChatting );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamFriends_DownloadClanActivityCounts")]
		private static extern SteamAPICall_t _DownloadClanActivityCounts( IntPtr self, [In,Out] SteamId[]  psteamIDClans, int cClansToRequest );
		
		#endregion
		internal CallbackResult<DownloadClanActivityCountsResult_t> DownloadClanActivityCounts( [In,Out] SteamId[]  psteamIDClans, int cClansToRequest )
		{
			var returnValue = _DownloadClanActivityCounts( Self, psteamIDClans, cClansToRequest );
			return new CallbackResult<DownloadClanActivityCountsResult_t>( returnValue );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamFriends_GetFriendCountFromSource")]
		private static extern int _GetFriendCountFromSource( IntPtr self, SteamId steamIDSource );
		
		#endregion
		internal int GetFriendCountFromSource( SteamId steamIDSource )
		{
			var returnValue = _GetFriendCountFromSource( Self, steamIDSource );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamFriends_GetFriendFromSourceByIndex")]
		private static extern SteamId _GetFriendFromSourceByIndex( IntPtr self, SteamId steamIDSource, int iFriend );
		
		#endregion
		internal SteamId GetFriendFromSourceByIndex( SteamId steamIDSource, int iFriend )
		{
			var returnValue = _GetFriendFromSourceByIndex( Self, steamIDSource, iFriend );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamFriends_IsUserInSource")]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _IsUserInSource( IntPtr self, SteamId steamIDUser, SteamId steamIDSource );
		
		#endregion
		internal bool IsUserInSource( SteamId steamIDUser, SteamId steamIDSource )
		{
			var returnValue = _IsUserInSource( Self, steamIDUser, steamIDSource );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamFriends_SetInGameVoiceSpeaking")]
		private static extern void _SetInGameVoiceSpeaking( IntPtr self, SteamId steamIDUser, [MarshalAs( UnmanagedType.U1 )] bool bSpeaking );
		
		#endregion
		internal void SetInGameVoiceSpeaking( SteamId steamIDUser, [MarshalAs( UnmanagedType.U1 )] bool bSpeaking )
		{
			_SetInGameVoiceSpeaking( Self, steamIDUser, bSpeaking );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamFriends_ActivateGameOverlay")]
		private static extern void _ActivateGameOverlay( IntPtr self, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchDialog );
		
		#endregion
		internal void ActivateGameOverlay( [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchDialog )
		{
			_ActivateGameOverlay( Self, pchDialog );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamFriends_ActivateGameOverlayToUser")]
		private static extern void _ActivateGameOverlayToUser( IntPtr self, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchDialog, SteamId steamID );
		
		#endregion
		internal void ActivateGameOverlayToUser( [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchDialog, SteamId steamID )
		{
			_ActivateGameOverlayToUser( Self, pchDialog, steamID );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamFriends_ActivateGameOverlayToWebPage")]
		private static extern void _ActivateGameOverlayToWebPage( IntPtr self, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchURL, ActivateGameOverlayToWebPageMode eMode );
		
		#endregion
		internal void ActivateGameOverlayToWebPage( [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchURL, ActivateGameOverlayToWebPageMode eMode )
		{
			_ActivateGameOverlayToWebPage( Self, pchURL, eMode );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamFriends_ActivateGameOverlayToStore")]
		private static extern void _ActivateGameOverlayToStore( IntPtr self, AppId nAppID, OverlayToStoreFlag eFlag );
		
		#endregion
		internal void ActivateGameOverlayToStore( AppId nAppID, OverlayToStoreFlag eFlag )
		{
			_ActivateGameOverlayToStore( Self, nAppID, eFlag );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamFriends_SetPlayedWith")]
		private static extern void _SetPlayedWith( IntPtr self, SteamId steamIDUserPlayedWith );
		
		#endregion
		internal void SetPlayedWith( SteamId steamIDUserPlayedWith )
		{
			_SetPlayedWith( Self, steamIDUserPlayedWith );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamFriends_ActivateGameOverlayInviteDialog")]
		private static extern void _ActivateGameOverlayInviteDialog( IntPtr self, SteamId steamIDLobby );
		
		#endregion
		internal void ActivateGameOverlayInviteDialog( SteamId steamIDLobby )
		{
			_ActivateGameOverlayInviteDialog( Self, steamIDLobby );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamFriends_GetSmallFriendAvatar")]
		private static extern int _GetSmallFriendAvatar( IntPtr self, SteamId steamIDFriend );
		
		#endregion
		internal int GetSmallFriendAvatar( SteamId steamIDFriend )
		{
			var returnValue = _GetSmallFriendAvatar( Self, steamIDFriend );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamFriends_GetMediumFriendAvatar")]
		private static extern int _GetMediumFriendAvatar( IntPtr self, SteamId steamIDFriend );
		
		#endregion
		internal int GetMediumFriendAvatar( SteamId steamIDFriend )
		{
			var returnValue = _GetMediumFriendAvatar( Self, steamIDFriend );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamFriends_GetLargeFriendAvatar")]
		private static extern int _GetLargeFriendAvatar( IntPtr self, SteamId steamIDFriend );
		
		#endregion
		internal int GetLargeFriendAvatar( SteamId steamIDFriend )
		{
			var returnValue = _GetLargeFriendAvatar( Self, steamIDFriend );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamFriends_RequestUserInformation")]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _RequestUserInformation( IntPtr self, SteamId steamIDUser, [MarshalAs( UnmanagedType.U1 )] bool bRequireNameOnly );
		
		#endregion
		internal bool RequestUserInformation( SteamId steamIDUser, [MarshalAs( UnmanagedType.U1 )] bool bRequireNameOnly )
		{
			var returnValue = _RequestUserInformation( Self, steamIDUser, bRequireNameOnly );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamFriends_RequestClanOfficerList")]
		private static extern SteamAPICall_t _RequestClanOfficerList( IntPtr self, SteamId steamIDClan );
		
		#endregion
		internal CallbackResult<ClanOfficerListResponse_t> RequestClanOfficerList( SteamId steamIDClan )
		{
			var returnValue = _RequestClanOfficerList( Self, steamIDClan );
			return new CallbackResult<ClanOfficerListResponse_t>( returnValue );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamFriends_GetClanOwner")]
		private static extern SteamId _GetClanOwner( IntPtr self, SteamId steamIDClan );
		
		#endregion
		internal SteamId GetClanOwner( SteamId steamIDClan )
		{
			var returnValue = _GetClanOwner( Self, steamIDClan );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamFriends_GetClanOfficerCount")]
		private static extern int _GetClanOfficerCount( IntPtr self, SteamId steamIDClan );
		
		#endregion
		internal int GetClanOfficerCount( SteamId steamIDClan )
		{
			var returnValue = _GetClanOfficerCount( Self, steamIDClan );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamFriends_GetClanOfficerByIndex")]
		private static extern SteamId _GetClanOfficerByIndex( IntPtr self, SteamId steamIDClan, int iOfficer );
		
		#endregion
		internal SteamId GetClanOfficerByIndex( SteamId steamIDClan, int iOfficer )
		{
			var returnValue = _GetClanOfficerByIndex( Self, steamIDClan, iOfficer );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamFriends_GetUserRestrictions")]
		private static extern uint _GetUserRestrictions( IntPtr self );
		
		#endregion
		internal uint GetUserRestrictions()
		{
			var returnValue = _GetUserRestrictions( Self );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamFriends_SetRichPresence")]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _SetRichPresence( IntPtr self, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchKey, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchValue );
		
		#endregion
		internal bool SetRichPresence( [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchKey, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchValue )
		{
			var returnValue = _SetRichPresence( Self, pchKey, pchValue );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamFriends_ClearRichPresence")]
		private static extern void _ClearRichPresence( IntPtr self );
		
		#endregion
		internal void ClearRichPresence()
		{
			_ClearRichPresence( Self );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamFriends_GetFriendRichPresence")]
		private static extern Utf8StringPointer _GetFriendRichPresence( IntPtr self, SteamId steamIDFriend, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchKey );
		
		#endregion
		internal string GetFriendRichPresence( SteamId steamIDFriend, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchKey )
		{
			var returnValue = _GetFriendRichPresence( Self, steamIDFriend, pchKey );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamFriends_GetFriendRichPresenceKeyCount")]
		private static extern int _GetFriendRichPresenceKeyCount( IntPtr self, SteamId steamIDFriend );
		
		#endregion
		internal int GetFriendRichPresenceKeyCount( SteamId steamIDFriend )
		{
			var returnValue = _GetFriendRichPresenceKeyCount( Self, steamIDFriend );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamFriends_GetFriendRichPresenceKeyByIndex")]
		private static extern Utf8StringPointer _GetFriendRichPresenceKeyByIndex( IntPtr self, SteamId steamIDFriend, int iKey );
		
		#endregion
		internal string GetFriendRichPresenceKeyByIndex( SteamId steamIDFriend, int iKey )
		{
			var returnValue = _GetFriendRichPresenceKeyByIndex( Self, steamIDFriend, iKey );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamFriends_RequestFriendRichPresence")]
		private static extern void _RequestFriendRichPresence( IntPtr self, SteamId steamIDFriend );
		
		#endregion
		internal void RequestFriendRichPresence( SteamId steamIDFriend )
		{
			_RequestFriendRichPresence( Self, steamIDFriend );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamFriends_InviteUserToGame")]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _InviteUserToGame( IntPtr self, SteamId steamIDFriend, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchConnectString );
		
		#endregion
		internal bool InviteUserToGame( SteamId steamIDFriend, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchConnectString )
		{
			var returnValue = _InviteUserToGame( Self, steamIDFriend, pchConnectString );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamFriends_GetCoplayFriendCount")]
		private static extern int _GetCoplayFriendCount( IntPtr self );
		
		#endregion
		internal int GetCoplayFriendCount()
		{
			var returnValue = _GetCoplayFriendCount( Self );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamFriends_GetCoplayFriend")]
		private static extern SteamId _GetCoplayFriend( IntPtr self, int iCoplayFriend );
		
		#endregion
		internal SteamId GetCoplayFriend( int iCoplayFriend )
		{
			var returnValue = _GetCoplayFriend( Self, iCoplayFriend );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamFriends_GetFriendCoplayTime")]
		private static extern int _GetFriendCoplayTime( IntPtr self, SteamId steamIDFriend );
		
		#endregion
		internal int GetFriendCoplayTime( SteamId steamIDFriend )
		{
			var returnValue = _GetFriendCoplayTime( Self, steamIDFriend );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamFriends_GetFriendCoplayGame")]
		private static extern AppId _GetFriendCoplayGame( IntPtr self, SteamId steamIDFriend );
		
		#endregion
		internal AppId GetFriendCoplayGame( SteamId steamIDFriend )
		{
			var returnValue = _GetFriendCoplayGame( Self, steamIDFriend );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamFriends_JoinClanChatRoom")]
		private static extern SteamAPICall_t _JoinClanChatRoom( IntPtr self, SteamId steamIDClan );
		
		#endregion
		internal CallbackResult<JoinClanChatRoomCompletionResult_t> JoinClanChatRoom( SteamId steamIDClan )
		{
			var returnValue = _JoinClanChatRoom( Self, steamIDClan );
			return new CallbackResult<JoinClanChatRoomCompletionResult_t>( returnValue );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamFriends_LeaveClanChatRoom")]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _LeaveClanChatRoom( IntPtr self, SteamId steamIDClan );
		
		#endregion
		internal bool LeaveClanChatRoom( SteamId steamIDClan )
		{
			var returnValue = _LeaveClanChatRoom( Self, steamIDClan );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamFriends_GetClanChatMemberCount")]
		private static extern int _GetClanChatMemberCount( IntPtr self, SteamId steamIDClan );
		
		#endregion
		internal int GetClanChatMemberCount( SteamId steamIDClan )
		{
			var returnValue = _GetClanChatMemberCount( Self, steamIDClan );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamFriends_GetChatMemberByIndex")]
		private static extern SteamId _GetChatMemberByIndex( IntPtr self, SteamId steamIDClan, int iUser );
		
		#endregion
		internal SteamId GetChatMemberByIndex( SteamId steamIDClan, int iUser )
		{
			var returnValue = _GetChatMemberByIndex( Self, steamIDClan, iUser );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamFriends_SendClanChatMessage")]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _SendClanChatMessage( IntPtr self, SteamId steamIDClanChat, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchText );
		
		#endregion
		internal bool SendClanChatMessage( SteamId steamIDClanChat, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchText )
		{
			var returnValue = _SendClanChatMessage( Self, steamIDClanChat, pchText );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamFriends_GetClanChatMessage")]
		private static extern int _GetClanChatMessage( IntPtr self, SteamId steamIDClanChat, int iMessage, IntPtr prgchText, int cchTextMax, ref ChatEntryType peChatEntryType, ref SteamId psteamidChatter );
		
		#endregion
		internal int GetClanChatMessage( SteamId steamIDClanChat, int iMessage, IntPtr prgchText, int cchTextMax, ref ChatEntryType peChatEntryType, ref SteamId psteamidChatter )
		{
			var returnValue = _GetClanChatMessage( Self, steamIDClanChat, iMessage, prgchText, cchTextMax, ref peChatEntryType, ref psteamidChatter );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamFriends_IsClanChatAdmin")]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _IsClanChatAdmin( IntPtr self, SteamId steamIDClanChat, SteamId steamIDUser );
		
		#endregion
		internal bool IsClanChatAdmin( SteamId steamIDClanChat, SteamId steamIDUser )
		{
			var returnValue = _IsClanChatAdmin( Self, steamIDClanChat, steamIDUser );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamFriends_IsClanChatWindowOpenInSteam")]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _IsClanChatWindowOpenInSteam( IntPtr self, SteamId steamIDClanChat );
		
		#endregion
		internal bool IsClanChatWindowOpenInSteam( SteamId steamIDClanChat )
		{
			var returnValue = _IsClanChatWindowOpenInSteam( Self, steamIDClanChat );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamFriends_OpenClanChatWindowInSteam")]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _OpenClanChatWindowInSteam( IntPtr self, SteamId steamIDClanChat );
		
		#endregion
		internal bool OpenClanChatWindowInSteam( SteamId steamIDClanChat )
		{
			var returnValue = _OpenClanChatWindowInSteam( Self, steamIDClanChat );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamFriends_CloseClanChatWindowInSteam")]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _CloseClanChatWindowInSteam( IntPtr self, SteamId steamIDClanChat );
		
		#endregion
		internal bool CloseClanChatWindowInSteam( SteamId steamIDClanChat )
		{
			var returnValue = _CloseClanChatWindowInSteam( Self, steamIDClanChat );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamFriends_SetListenForFriendsMessages")]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _SetListenForFriendsMessages( IntPtr self, [MarshalAs( UnmanagedType.U1 )] bool bInterceptEnabled );
		
		#endregion
		internal bool SetListenForFriendsMessages( [MarshalAs( UnmanagedType.U1 )] bool bInterceptEnabled )
		{
			var returnValue = _SetListenForFriendsMessages( Self, bInterceptEnabled );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamFriends_ReplyToFriendMessage")]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _ReplyToFriendMessage( IntPtr self, SteamId steamIDFriend, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchMsgToSend );
		
		#endregion
		internal bool ReplyToFriendMessage( SteamId steamIDFriend, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchMsgToSend )
		{
			var returnValue = _ReplyToFriendMessage( Self, steamIDFriend, pchMsgToSend );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamFriends_GetFriendMessage")]
		private static extern int _GetFriendMessage( IntPtr self, SteamId steamIDFriend, int iMessageID, IntPtr pvData, int cubData, ref ChatEntryType peChatEntryType );
		
		#endregion
		internal int GetFriendMessage( SteamId steamIDFriend, int iMessageID, IntPtr pvData, int cubData, ref ChatEntryType peChatEntryType )
		{
			var returnValue = _GetFriendMessage( Self, steamIDFriend, iMessageID, pvData, cubData, ref peChatEntryType );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamFriends_GetFollowerCount")]
		private static extern SteamAPICall_t _GetFollowerCount( IntPtr self, SteamId steamID );
		
		#endregion
		internal CallbackResult<FriendsGetFollowerCount_t> GetFollowerCount( SteamId steamID )
		{
			var returnValue = _GetFollowerCount( Self, steamID );
			return new CallbackResult<FriendsGetFollowerCount_t>( returnValue );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamFriends_IsFollowing")]
		private static extern SteamAPICall_t _IsFollowing( IntPtr self, SteamId steamID );
		
		#endregion
		internal CallbackResult<FriendsIsFollowing_t> IsFollowing( SteamId steamID )
		{
			var returnValue = _IsFollowing( Self, steamID );
			return new CallbackResult<FriendsIsFollowing_t>( returnValue );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamFriends_EnumerateFollowingList")]
		private static extern SteamAPICall_t _EnumerateFollowingList( IntPtr self, uint unStartIndex );
		
		#endregion
		internal CallbackResult<FriendsEnumerateFollowingList_t> EnumerateFollowingList( uint unStartIndex )
		{
			var returnValue = _EnumerateFollowingList( Self, unStartIndex );
			return new CallbackResult<FriendsEnumerateFollowingList_t>( returnValue );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamFriends_IsClanPublic")]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _IsClanPublic( IntPtr self, SteamId steamIDClan );
		
		#endregion
		internal bool IsClanPublic( SteamId steamIDClan )
		{
			var returnValue = _IsClanPublic( Self, steamIDClan );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamFriends_IsClanOfficialGameGroup")]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _IsClanOfficialGameGroup( IntPtr self, SteamId steamIDClan );
		
		#endregion
		internal bool IsClanOfficialGameGroup( SteamId steamIDClan )
		{
			var returnValue = _IsClanOfficialGameGroup( Self, steamIDClan );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamFriends_GetNumChatsWithUnreadPriorityMessages")]
		private static extern int _GetNumChatsWithUnreadPriorityMessages( IntPtr self );
		
		#endregion
		internal int GetNumChatsWithUnreadPriorityMessages()
		{
			var returnValue = _GetNumChatsWithUnreadPriorityMessages( Self );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamFriends_ActivateGameOverlayRemotePlayTogetherInviteDialog")]
		private static extern void _ActivateGameOverlayRemotePlayTogetherInviteDialog( IntPtr self, SteamId steamIDLobby );
		
		#endregion
		internal void ActivateGameOverlayRemotePlayTogetherInviteDialog( SteamId steamIDLobby )
		{
			_ActivateGameOverlayRemotePlayTogetherInviteDialog( Self, steamIDLobby );
		}
		
	}
}
