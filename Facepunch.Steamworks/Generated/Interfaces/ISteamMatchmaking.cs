using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Steamworks.Data;


namespace Steamworks
{
	internal class ISteamMatchmaking : SteamInterface
	{
		public override string InterfaceName => "SteamMatchMaking009";
		
		public override void InitInternals()
		{
			_GetFavoriteGameCount = Marshal.GetDelegateForFunctionPointer<FGetFavoriteGameCount>( Marshal.ReadIntPtr( VTable, 0) );
			_GetFavoriteGame = Marshal.GetDelegateForFunctionPointer<FGetFavoriteGame>( Marshal.ReadIntPtr( VTable, 8) );
			_AddFavoriteGame = Marshal.GetDelegateForFunctionPointer<FAddFavoriteGame>( Marshal.ReadIntPtr( VTable, 16) );
			_RemoveFavoriteGame = Marshal.GetDelegateForFunctionPointer<FRemoveFavoriteGame>( Marshal.ReadIntPtr( VTable, 24) );
			_RequestLobbyList = Marshal.GetDelegateForFunctionPointer<FRequestLobbyList>( Marshal.ReadIntPtr( VTable, 32) );
			_AddRequestLobbyListStringFilter = Marshal.GetDelegateForFunctionPointer<FAddRequestLobbyListStringFilter>( Marshal.ReadIntPtr( VTable, 40) );
			_AddRequestLobbyListNumericalFilter = Marshal.GetDelegateForFunctionPointer<FAddRequestLobbyListNumericalFilter>( Marshal.ReadIntPtr( VTable, 48) );
			_AddRequestLobbyListNearValueFilter = Marshal.GetDelegateForFunctionPointer<FAddRequestLobbyListNearValueFilter>( Marshal.ReadIntPtr( VTable, 56) );
			_AddRequestLobbyListFilterSlotsAvailable = Marshal.GetDelegateForFunctionPointer<FAddRequestLobbyListFilterSlotsAvailable>( Marshal.ReadIntPtr( VTable, 64) );
			_AddRequestLobbyListDistanceFilter = Marshal.GetDelegateForFunctionPointer<FAddRequestLobbyListDistanceFilter>( Marshal.ReadIntPtr( VTable, 72) );
			_AddRequestLobbyListResultCountFilter = Marshal.GetDelegateForFunctionPointer<FAddRequestLobbyListResultCountFilter>( Marshal.ReadIntPtr( VTable, 80) );
			_AddRequestLobbyListCompatibleMembersFilter = Marshal.GetDelegateForFunctionPointer<FAddRequestLobbyListCompatibleMembersFilter>( Marshal.ReadIntPtr( VTable, 88) );
			_GetLobbyByIndex = Marshal.GetDelegateForFunctionPointer<FGetLobbyByIndex>( Marshal.ReadIntPtr( VTable, 96) );
			_GetLobbyByIndex_Windows = Marshal.GetDelegateForFunctionPointer<FGetLobbyByIndex_Windows>( Marshal.ReadIntPtr( VTable, 96) );
			_CreateLobby = Marshal.GetDelegateForFunctionPointer<FCreateLobby>( Marshal.ReadIntPtr( VTable, 104) );
			_JoinLobby = Marshal.GetDelegateForFunctionPointer<FJoinLobby>( Marshal.ReadIntPtr( VTable, 112) );
			_LeaveLobby = Marshal.GetDelegateForFunctionPointer<FLeaveLobby>( Marshal.ReadIntPtr( VTable, 120) );
			_InviteUserToLobby = Marshal.GetDelegateForFunctionPointer<FInviteUserToLobby>( Marshal.ReadIntPtr( VTable, 128) );
			_GetNumLobbyMembers = Marshal.GetDelegateForFunctionPointer<FGetNumLobbyMembers>( Marshal.ReadIntPtr( VTable, 136) );
			_GetLobbyMemberByIndex = Marshal.GetDelegateForFunctionPointer<FGetLobbyMemberByIndex>( Marshal.ReadIntPtr( VTable, 144) );
			_GetLobbyMemberByIndex_Windows = Marshal.GetDelegateForFunctionPointer<FGetLobbyMemberByIndex_Windows>( Marshal.ReadIntPtr( VTable, 144) );
			_GetLobbyData = Marshal.GetDelegateForFunctionPointer<FGetLobbyData>( Marshal.ReadIntPtr( VTable, 152) );
			_SetLobbyData = Marshal.GetDelegateForFunctionPointer<FSetLobbyData>( Marshal.ReadIntPtr( VTable, 160) );
			_GetLobbyDataCount = Marshal.GetDelegateForFunctionPointer<FGetLobbyDataCount>( Marshal.ReadIntPtr( VTable, 168) );
			_GetLobbyDataByIndex = Marshal.GetDelegateForFunctionPointer<FGetLobbyDataByIndex>( Marshal.ReadIntPtr( VTable, 176) );
			_DeleteLobbyData = Marshal.GetDelegateForFunctionPointer<FDeleteLobbyData>( Marshal.ReadIntPtr( VTable, 184) );
			_GetLobbyMemberData = Marshal.GetDelegateForFunctionPointer<FGetLobbyMemberData>( Marshal.ReadIntPtr( VTable, 192) );
			_SetLobbyMemberData = Marshal.GetDelegateForFunctionPointer<FSetLobbyMemberData>( Marshal.ReadIntPtr( VTable, 200) );
			_SendLobbyChatMsg = Marshal.GetDelegateForFunctionPointer<FSendLobbyChatMsg>( Marshal.ReadIntPtr( VTable, 208) );
			_GetLobbyChatEntry = Marshal.GetDelegateForFunctionPointer<FGetLobbyChatEntry>( Marshal.ReadIntPtr( VTable, 216) );
			_RequestLobbyData = Marshal.GetDelegateForFunctionPointer<FRequestLobbyData>( Marshal.ReadIntPtr( VTable, 224) );
			_SetLobbyGameServer = Marshal.GetDelegateForFunctionPointer<FSetLobbyGameServer>( Marshal.ReadIntPtr( VTable, 232) );
			_GetLobbyGameServer = Marshal.GetDelegateForFunctionPointer<FGetLobbyGameServer>( Marshal.ReadIntPtr( VTable, 240) );
			_SetLobbyMemberLimit = Marshal.GetDelegateForFunctionPointer<FSetLobbyMemberLimit>( Marshal.ReadIntPtr( VTable, 248) );
			_GetLobbyMemberLimit = Marshal.GetDelegateForFunctionPointer<FGetLobbyMemberLimit>( Marshal.ReadIntPtr( VTable, 256) );
			_SetLobbyType = Marshal.GetDelegateForFunctionPointer<FSetLobbyType>( Marshal.ReadIntPtr( VTable, 264) );
			_SetLobbyJoinable = Marshal.GetDelegateForFunctionPointer<FSetLobbyJoinable>( Marshal.ReadIntPtr( VTable, 272) );
			_GetLobbyOwner = Marshal.GetDelegateForFunctionPointer<FGetLobbyOwner>( Marshal.ReadIntPtr( VTable, 280) );
			_GetLobbyOwner_Windows = Marshal.GetDelegateForFunctionPointer<FGetLobbyOwner_Windows>( Marshal.ReadIntPtr( VTable, 280) );
			_SetLobbyOwner = Marshal.GetDelegateForFunctionPointer<FSetLobbyOwner>( Marshal.ReadIntPtr( VTable, 288) );
			_SetLinkedLobby = Marshal.GetDelegateForFunctionPointer<FSetLinkedLobby>( Marshal.ReadIntPtr( VTable, 296) );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate int FGetFavoriteGameCount( IntPtr self );
		private FGetFavoriteGameCount _GetFavoriteGameCount;
		
		#endregion
		internal int GetFavoriteGameCount()
		{
			return _GetFavoriteGameCount( Self );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FGetFavoriteGame( IntPtr self, int iGame, ref AppId pnAppID, ref uint pnIP, ref ushort pnConnPort, ref ushort pnQueryPort, ref uint punFlags, ref uint pRTime32LastPlayedOnServer );
		private FGetFavoriteGame _GetFavoriteGame;
		
		#endregion
		internal bool GetFavoriteGame( int iGame, ref AppId pnAppID, ref uint pnIP, ref ushort pnConnPort, ref ushort pnQueryPort, ref uint punFlags, ref uint pRTime32LastPlayedOnServer )
		{
			return _GetFavoriteGame( Self, iGame, ref pnAppID, ref pnIP, ref pnConnPort, ref pnQueryPort, ref punFlags, ref pRTime32LastPlayedOnServer );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate int FAddFavoriteGame( IntPtr self, AppId nAppID, uint nIP, ushort nConnPort, ushort nQueryPort, uint unFlags, uint rTime32LastPlayedOnServer );
		private FAddFavoriteGame _AddFavoriteGame;
		
		#endregion
		internal int AddFavoriteGame( AppId nAppID, uint nIP, ushort nConnPort, ushort nQueryPort, uint unFlags, uint rTime32LastPlayedOnServer )
		{
			return _AddFavoriteGame( Self, nAppID, nIP, nConnPort, nQueryPort, unFlags, rTime32LastPlayedOnServer );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FRemoveFavoriteGame( IntPtr self, AppId nAppID, uint nIP, ushort nConnPort, ushort nQueryPort, uint unFlags );
		private FRemoveFavoriteGame _RemoveFavoriteGame;
		
		#endregion
		internal bool RemoveFavoriteGame( AppId nAppID, uint nIP, ushort nConnPort, ushort nQueryPort, uint unFlags )
		{
			return _RemoveFavoriteGame( Self, nAppID, nIP, nConnPort, nQueryPort, unFlags );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate SteamAPICall_t FRequestLobbyList( IntPtr self );
		private FRequestLobbyList _RequestLobbyList;
		
		#endregion
		internal async Task<LobbyMatchList_t?> RequestLobbyList()
		{
			return await LobbyMatchList_t.GetResultAsync( _RequestLobbyList( Self ) );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate void FAddRequestLobbyListStringFilter( IntPtr self, string pchKeyToMatch, string pchValueToMatch, LobbyComparison eComparisonType );
		private FAddRequestLobbyListStringFilter _AddRequestLobbyListStringFilter;
		
		#endregion
		internal void AddRequestLobbyListStringFilter( string pchKeyToMatch, string pchValueToMatch, LobbyComparison eComparisonType )
		{
			_AddRequestLobbyListStringFilter( Self, pchKeyToMatch, pchValueToMatch, eComparisonType );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate void FAddRequestLobbyListNumericalFilter( IntPtr self, string pchKeyToMatch, int nValueToMatch, LobbyComparison eComparisonType );
		private FAddRequestLobbyListNumericalFilter _AddRequestLobbyListNumericalFilter;
		
		#endregion
		internal void AddRequestLobbyListNumericalFilter( string pchKeyToMatch, int nValueToMatch, LobbyComparison eComparisonType )
		{
			_AddRequestLobbyListNumericalFilter( Self, pchKeyToMatch, nValueToMatch, eComparisonType );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate void FAddRequestLobbyListNearValueFilter( IntPtr self, string pchKeyToMatch, int nValueToBeCloseTo );
		private FAddRequestLobbyListNearValueFilter _AddRequestLobbyListNearValueFilter;
		
		#endregion
		internal void AddRequestLobbyListNearValueFilter( string pchKeyToMatch, int nValueToBeCloseTo )
		{
			_AddRequestLobbyListNearValueFilter( Self, pchKeyToMatch, nValueToBeCloseTo );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate void FAddRequestLobbyListFilterSlotsAvailable( IntPtr self, int nSlotsAvailable );
		private FAddRequestLobbyListFilterSlotsAvailable _AddRequestLobbyListFilterSlotsAvailable;
		
		#endregion
		internal void AddRequestLobbyListFilterSlotsAvailable( int nSlotsAvailable )
		{
			_AddRequestLobbyListFilterSlotsAvailable( Self, nSlotsAvailable );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate void FAddRequestLobbyListDistanceFilter( IntPtr self, LobbyDistanceFilter eLobbyDistanceFilter );
		private FAddRequestLobbyListDistanceFilter _AddRequestLobbyListDistanceFilter;
		
		#endregion
		internal void AddRequestLobbyListDistanceFilter( LobbyDistanceFilter eLobbyDistanceFilter )
		{
			_AddRequestLobbyListDistanceFilter( Self, eLobbyDistanceFilter );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate void FAddRequestLobbyListResultCountFilter( IntPtr self, int cMaxResults );
		private FAddRequestLobbyListResultCountFilter _AddRequestLobbyListResultCountFilter;
		
		#endregion
		internal void AddRequestLobbyListResultCountFilter( int cMaxResults )
		{
			_AddRequestLobbyListResultCountFilter( Self, cMaxResults );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate void FAddRequestLobbyListCompatibleMembersFilter( IntPtr self, SteamId steamIDLobby );
		private FAddRequestLobbyListCompatibleMembersFilter _AddRequestLobbyListCompatibleMembersFilter;
		
		#endregion
		internal void AddRequestLobbyListCompatibleMembersFilter( SteamId steamIDLobby )
		{
			_AddRequestLobbyListCompatibleMembersFilter( Self, steamIDLobby );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate SteamId FGetLobbyByIndex( IntPtr self, int iLobby );
		private FGetLobbyByIndex _GetLobbyByIndex;
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate void FGetLobbyByIndex_Windows( IntPtr self, ref SteamId retVal, int iLobby );
		private FGetLobbyByIndex_Windows _GetLobbyByIndex_Windows;
		
		#endregion
		internal SteamId GetLobbyByIndex( int iLobby )
		{
			if ( Config.Os == OsType.Windows )
			{
				var retVal = default( SteamId );
				_GetLobbyByIndex_Windows( Self, ref retVal, iLobby );
				return retVal;
			}
			
			return _GetLobbyByIndex( Self, iLobby );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate SteamAPICall_t FCreateLobby( IntPtr self, LobbyType eLobbyType, int cMaxMembers );
		private FCreateLobby _CreateLobby;
		
		#endregion
		internal async Task<LobbyCreated_t?> CreateLobby( LobbyType eLobbyType, int cMaxMembers )
		{
			return await LobbyCreated_t.GetResultAsync( _CreateLobby( Self, eLobbyType, cMaxMembers ) );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate SteamAPICall_t FJoinLobby( IntPtr self, SteamId steamIDLobby );
		private FJoinLobby _JoinLobby;
		
		#endregion
		internal async Task<LobbyEnter_t?> JoinLobby( SteamId steamIDLobby )
		{
			return await LobbyEnter_t.GetResultAsync( _JoinLobby( Self, steamIDLobby ) );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate void FLeaveLobby( IntPtr self, SteamId steamIDLobby );
		private FLeaveLobby _LeaveLobby;
		
		#endregion
		internal void LeaveLobby( SteamId steamIDLobby )
		{
			_LeaveLobby( Self, steamIDLobby );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FInviteUserToLobby( IntPtr self, SteamId steamIDLobby, SteamId steamIDInvitee );
		private FInviteUserToLobby _InviteUserToLobby;
		
		#endregion
		internal bool InviteUserToLobby( SteamId steamIDLobby, SteamId steamIDInvitee )
		{
			return _InviteUserToLobby( Self, steamIDLobby, steamIDInvitee );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate int FGetNumLobbyMembers( IntPtr self, SteamId steamIDLobby );
		private FGetNumLobbyMembers _GetNumLobbyMembers;
		
		#endregion
		internal int GetNumLobbyMembers( SteamId steamIDLobby )
		{
			return _GetNumLobbyMembers( Self, steamIDLobby );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate SteamId FGetLobbyMemberByIndex( IntPtr self, SteamId steamIDLobby, int iMember );
		private FGetLobbyMemberByIndex _GetLobbyMemberByIndex;
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate void FGetLobbyMemberByIndex_Windows( IntPtr self, ref SteamId retVal, SteamId steamIDLobby, int iMember );
		private FGetLobbyMemberByIndex_Windows _GetLobbyMemberByIndex_Windows;
		
		#endregion
		internal SteamId GetLobbyMemberByIndex( SteamId steamIDLobby, int iMember )
		{
			if ( Config.Os == OsType.Windows )
			{
				var retVal = default( SteamId );
				_GetLobbyMemberByIndex_Windows( Self, ref retVal, steamIDLobby, iMember );
				return retVal;
			}
			
			return _GetLobbyMemberByIndex( Self, steamIDLobby, iMember );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate IntPtr FGetLobbyData( IntPtr self, SteamId steamIDLobby, string pchKey );
		private FGetLobbyData _GetLobbyData;
		
		#endregion
		internal string GetLobbyData( SteamId steamIDLobby, string pchKey )
		{
			return GetString( _GetLobbyData( Self, steamIDLobby, pchKey ) );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FSetLobbyData( IntPtr self, SteamId steamIDLobby, string pchKey, string pchValue );
		private FSetLobbyData _SetLobbyData;
		
		#endregion
		internal bool SetLobbyData( SteamId steamIDLobby, string pchKey, string pchValue )
		{
			return _SetLobbyData( Self, steamIDLobby, pchKey, pchValue );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate int FGetLobbyDataCount( IntPtr self, SteamId steamIDLobby );
		private FGetLobbyDataCount _GetLobbyDataCount;
		
		#endregion
		internal int GetLobbyDataCount( SteamId steamIDLobby )
		{
			return _GetLobbyDataCount( Self, steamIDLobby );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FGetLobbyDataByIndex( IntPtr self, SteamId steamIDLobby, int iLobbyData, StringBuilder pchKey, int cchKeyBufferSize, StringBuilder pchValue, int cchValueBufferSize );
		private FGetLobbyDataByIndex _GetLobbyDataByIndex;
		
		#endregion
		internal bool GetLobbyDataByIndex( SteamId steamIDLobby, int iLobbyData, StringBuilder pchKey, int cchKeyBufferSize, StringBuilder pchValue, int cchValueBufferSize )
		{
			return _GetLobbyDataByIndex( Self, steamIDLobby, iLobbyData, pchKey, cchKeyBufferSize, pchValue, cchValueBufferSize );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FDeleteLobbyData( IntPtr self, SteamId steamIDLobby, string pchKey );
		private FDeleteLobbyData _DeleteLobbyData;
		
		#endregion
		internal bool DeleteLobbyData( SteamId steamIDLobby, string pchKey )
		{
			return _DeleteLobbyData( Self, steamIDLobby, pchKey );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate IntPtr FGetLobbyMemberData( IntPtr self, SteamId steamIDLobby, SteamId steamIDUser, string pchKey );
		private FGetLobbyMemberData _GetLobbyMemberData;
		
		#endregion
		internal string GetLobbyMemberData( SteamId steamIDLobby, SteamId steamIDUser, string pchKey )
		{
			return GetString( _GetLobbyMemberData( Self, steamIDLobby, steamIDUser, pchKey ) );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate void FSetLobbyMemberData( IntPtr self, SteamId steamIDLobby, string pchKey, string pchValue );
		private FSetLobbyMemberData _SetLobbyMemberData;
		
		#endregion
		internal void SetLobbyMemberData( SteamId steamIDLobby, string pchKey, string pchValue )
		{
			_SetLobbyMemberData( Self, steamIDLobby, pchKey, pchValue );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FSendLobbyChatMsg( IntPtr self, SteamId steamIDLobby, IntPtr pvMsgBody, int cubMsgBody );
		private FSendLobbyChatMsg _SendLobbyChatMsg;
		
		#endregion
		internal bool SendLobbyChatMsg( SteamId steamIDLobby, IntPtr pvMsgBody, int cubMsgBody )
		{
			return _SendLobbyChatMsg( Self, steamIDLobby, pvMsgBody, cubMsgBody );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate int FGetLobbyChatEntry( IntPtr self, SteamId steamIDLobby, int iChatID, ref SteamId pSteamIDUser, IntPtr pvData, int cubData, ref ChatEntryType peChatEntryType );
		private FGetLobbyChatEntry _GetLobbyChatEntry;
		
		#endregion
		internal int GetLobbyChatEntry( SteamId steamIDLobby, int iChatID, ref SteamId pSteamIDUser, IntPtr pvData, int cubData, ref ChatEntryType peChatEntryType )
		{
			return _GetLobbyChatEntry( Self, steamIDLobby, iChatID, ref pSteamIDUser, pvData, cubData, ref peChatEntryType );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FRequestLobbyData( IntPtr self, SteamId steamIDLobby );
		private FRequestLobbyData _RequestLobbyData;
		
		#endregion
		internal bool RequestLobbyData( SteamId steamIDLobby )
		{
			return _RequestLobbyData( Self, steamIDLobby );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate void FSetLobbyGameServer( IntPtr self, SteamId steamIDLobby, uint unGameServerIP, ushort unGameServerPort, SteamId steamIDGameServer );
		private FSetLobbyGameServer _SetLobbyGameServer;
		
		#endregion
		internal void SetLobbyGameServer( SteamId steamIDLobby, uint unGameServerIP, ushort unGameServerPort, SteamId steamIDGameServer )
		{
			_SetLobbyGameServer( Self, steamIDLobby, unGameServerIP, unGameServerPort, steamIDGameServer );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FGetLobbyGameServer( IntPtr self, SteamId steamIDLobby, ref uint punGameServerIP, ref ushort punGameServerPort, ref SteamId psteamIDGameServer );
		private FGetLobbyGameServer _GetLobbyGameServer;
		
		#endregion
		internal bool GetLobbyGameServer( SteamId steamIDLobby, ref uint punGameServerIP, ref ushort punGameServerPort, ref SteamId psteamIDGameServer )
		{
			return _GetLobbyGameServer( Self, steamIDLobby, ref punGameServerIP, ref punGameServerPort, ref psteamIDGameServer );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FSetLobbyMemberLimit( IntPtr self, SteamId steamIDLobby, int cMaxMembers );
		private FSetLobbyMemberLimit _SetLobbyMemberLimit;
		
		#endregion
		internal bool SetLobbyMemberLimit( SteamId steamIDLobby, int cMaxMembers )
		{
			return _SetLobbyMemberLimit( Self, steamIDLobby, cMaxMembers );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate int FGetLobbyMemberLimit( IntPtr self, SteamId steamIDLobby );
		private FGetLobbyMemberLimit _GetLobbyMemberLimit;
		
		#endregion
		internal int GetLobbyMemberLimit( SteamId steamIDLobby )
		{
			return _GetLobbyMemberLimit( Self, steamIDLobby );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FSetLobbyType( IntPtr self, SteamId steamIDLobby, LobbyType eLobbyType );
		private FSetLobbyType _SetLobbyType;
		
		#endregion
		internal bool SetLobbyType( SteamId steamIDLobby, LobbyType eLobbyType )
		{
			return _SetLobbyType( Self, steamIDLobby, eLobbyType );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FSetLobbyJoinable( IntPtr self, SteamId steamIDLobby, [MarshalAs( UnmanagedType.U1 )] bool bLobbyJoinable );
		private FSetLobbyJoinable _SetLobbyJoinable;
		
		#endregion
		internal bool SetLobbyJoinable( SteamId steamIDLobby, [MarshalAs( UnmanagedType.U1 )] bool bLobbyJoinable )
		{
			return _SetLobbyJoinable( Self, steamIDLobby, bLobbyJoinable );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate SteamId FGetLobbyOwner( IntPtr self, SteamId steamIDLobby );
		private FGetLobbyOwner _GetLobbyOwner;
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate void FGetLobbyOwner_Windows( IntPtr self, ref SteamId retVal, SteamId steamIDLobby );
		private FGetLobbyOwner_Windows _GetLobbyOwner_Windows;
		
		#endregion
		internal SteamId GetLobbyOwner( SteamId steamIDLobby )
		{
			if ( Config.Os == OsType.Windows )
			{
				var retVal = default( SteamId );
				_GetLobbyOwner_Windows( Self, ref retVal, steamIDLobby );
				return retVal;
			}
			
			return _GetLobbyOwner( Self, steamIDLobby );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FSetLobbyOwner( IntPtr self, SteamId steamIDLobby, SteamId steamIDNewOwner );
		private FSetLobbyOwner _SetLobbyOwner;
		
		#endregion
		internal bool SetLobbyOwner( SteamId steamIDLobby, SteamId steamIDNewOwner )
		{
			return _SetLobbyOwner( Self, steamIDLobby, steamIDNewOwner );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FSetLinkedLobby( IntPtr self, SteamId steamIDLobby, SteamId steamIDLobbyDependent );
		private FSetLinkedLobby _SetLinkedLobby;
		
		#endregion
		internal bool SetLinkedLobby( SteamId steamIDLobby, SteamId steamIDLobbyDependent )
		{
			return _SetLinkedLobby( Self, steamIDLobby, steamIDLobbyDependent );
		}
		
	}
}
