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
			_GetFavoriteGameCount = Marshal.GetDelegateForFunctionPointer<FGetFavoriteGameCount>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 0 ) ) );
			_GetFavoriteGame = Marshal.GetDelegateForFunctionPointer<FGetFavoriteGame>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 8 ) ) );
			_AddFavoriteGame = Marshal.GetDelegateForFunctionPointer<FAddFavoriteGame>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 16 ) ) );
			_RemoveFavoriteGame = Marshal.GetDelegateForFunctionPointer<FRemoveFavoriteGame>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 24 ) ) );
			_RequestLobbyList = Marshal.GetDelegateForFunctionPointer<FRequestLobbyList>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 32 ) ) );
			_AddRequestLobbyListStringFilter = Marshal.GetDelegateForFunctionPointer<FAddRequestLobbyListStringFilter>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 40 ) ) );
			_AddRequestLobbyListNumericalFilter = Marshal.GetDelegateForFunctionPointer<FAddRequestLobbyListNumericalFilter>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 48 ) ) );
			_AddRequestLobbyListNearValueFilter = Marshal.GetDelegateForFunctionPointer<FAddRequestLobbyListNearValueFilter>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 56 ) ) );
			_AddRequestLobbyListFilterSlotsAvailable = Marshal.GetDelegateForFunctionPointer<FAddRequestLobbyListFilterSlotsAvailable>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 64 ) ) );
			_AddRequestLobbyListDistanceFilter = Marshal.GetDelegateForFunctionPointer<FAddRequestLobbyListDistanceFilter>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 72 ) ) );
			_AddRequestLobbyListResultCountFilter = Marshal.GetDelegateForFunctionPointer<FAddRequestLobbyListResultCountFilter>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 80 ) ) );
			_AddRequestLobbyListCompatibleMembersFilter = Marshal.GetDelegateForFunctionPointer<FAddRequestLobbyListCompatibleMembersFilter>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 88 ) ) );
			_GetLobbyByIndex = Marshal.GetDelegateForFunctionPointer<FGetLobbyByIndex>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 96 ) ) );
			_CreateLobby = Marshal.GetDelegateForFunctionPointer<FCreateLobby>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 104 ) ) );
			_JoinLobby = Marshal.GetDelegateForFunctionPointer<FJoinLobby>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 112 ) ) );
			_LeaveLobby = Marshal.GetDelegateForFunctionPointer<FLeaveLobby>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 120 ) ) );
			_InviteUserToLobby = Marshal.GetDelegateForFunctionPointer<FInviteUserToLobby>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 128 ) ) );
			_GetNumLobbyMembers = Marshal.GetDelegateForFunctionPointer<FGetNumLobbyMembers>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 136 ) ) );
			_GetLobbyMemberByIndex = Marshal.GetDelegateForFunctionPointer<FGetLobbyMemberByIndex>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 144 ) ) );
			_GetLobbyData = Marshal.GetDelegateForFunctionPointer<FGetLobbyData>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 152 ) ) );
			_SetLobbyData = Marshal.GetDelegateForFunctionPointer<FSetLobbyData>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 160 ) ) );
			_GetLobbyDataCount = Marshal.GetDelegateForFunctionPointer<FGetLobbyDataCount>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 168 ) ) );
			_GetLobbyDataByIndex = Marshal.GetDelegateForFunctionPointer<FGetLobbyDataByIndex>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 176 ) ) );
			_DeleteLobbyData = Marshal.GetDelegateForFunctionPointer<FDeleteLobbyData>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 184 ) ) );
			_GetLobbyMemberData = Marshal.GetDelegateForFunctionPointer<FGetLobbyMemberData>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 192 ) ) );
			_SetLobbyMemberData = Marshal.GetDelegateForFunctionPointer<FSetLobbyMemberData>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 200 ) ) );
			_SendLobbyChatMsg = Marshal.GetDelegateForFunctionPointer<FSendLobbyChatMsg>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 208 ) ) );
			_GetLobbyChatEntry = Marshal.GetDelegateForFunctionPointer<FGetLobbyChatEntry>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 216 ) ) );
			_RequestLobbyData = Marshal.GetDelegateForFunctionPointer<FRequestLobbyData>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 224 ) ) );
			_SetLobbyGameServer = Marshal.GetDelegateForFunctionPointer<FSetLobbyGameServer>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 232 ) ) );
			_GetLobbyGameServer = Marshal.GetDelegateForFunctionPointer<FGetLobbyGameServer>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 240 ) ) );
			_SetLobbyMemberLimit = Marshal.GetDelegateForFunctionPointer<FSetLobbyMemberLimit>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 248 ) ) );
			_GetLobbyMemberLimit = Marshal.GetDelegateForFunctionPointer<FGetLobbyMemberLimit>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 256 ) ) );
			_SetLobbyType = Marshal.GetDelegateForFunctionPointer<FSetLobbyType>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 264 ) ) );
			_SetLobbyJoinable = Marshal.GetDelegateForFunctionPointer<FSetLobbyJoinable>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 272 ) ) );
			_GetLobbyOwner = Marshal.GetDelegateForFunctionPointer<FGetLobbyOwner>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 280 ) ) );
			_SetLobbyOwner = Marshal.GetDelegateForFunctionPointer<FSetLobbyOwner>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 288 ) ) );
			_SetLinkedLobby = Marshal.GetDelegateForFunctionPointer<FSetLinkedLobby>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 296 ) ) );
		}
		internal override void Shutdown()
		{
			base.Shutdown();
			
			_GetFavoriteGameCount = null;
			_GetFavoriteGame = null;
			_AddFavoriteGame = null;
			_RemoveFavoriteGame = null;
			_RequestLobbyList = null;
			_AddRequestLobbyListStringFilter = null;
			_AddRequestLobbyListNumericalFilter = null;
			_AddRequestLobbyListNearValueFilter = null;
			_AddRequestLobbyListFilterSlotsAvailable = null;
			_AddRequestLobbyListDistanceFilter = null;
			_AddRequestLobbyListResultCountFilter = null;
			_AddRequestLobbyListCompatibleMembersFilter = null;
			_GetLobbyByIndex = null;
			_CreateLobby = null;
			_JoinLobby = null;
			_LeaveLobby = null;
			_InviteUserToLobby = null;
			_GetNumLobbyMembers = null;
			_GetLobbyMemberByIndex = null;
			_GetLobbyData = null;
			_SetLobbyData = null;
			_GetLobbyDataCount = null;
			_GetLobbyDataByIndex = null;
			_DeleteLobbyData = null;
			_GetLobbyMemberData = null;
			_SetLobbyMemberData = null;
			_SendLobbyChatMsg = null;
			_GetLobbyChatEntry = null;
			_RequestLobbyData = null;
			_SetLobbyGameServer = null;
			_GetLobbyGameServer = null;
			_SetLobbyMemberLimit = null;
			_GetLobbyMemberLimit = null;
			_SetLobbyType = null;
			_SetLobbyJoinable = null;
			_GetLobbyOwner = null;
			_SetLobbyOwner = null;
			_SetLinkedLobby = null;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate int FGetFavoriteGameCount( IntPtr self );
		private FGetFavoriteGameCount _GetFavoriteGameCount;
		
		#endregion
		internal int GetFavoriteGameCount()
		{
			var returnValue = _GetFavoriteGameCount( Self );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FGetFavoriteGame( IntPtr self, int iGame, ref AppId pnAppID, ref uint pnIP, ref ushort pnConnPort, ref ushort pnQueryPort, ref uint punFlags, ref uint pRTime32LastPlayedOnServer );
		private FGetFavoriteGame _GetFavoriteGame;
		
		#endregion
		internal bool GetFavoriteGame( int iGame, ref AppId pnAppID, ref uint pnIP, ref ushort pnConnPort, ref ushort pnQueryPort, ref uint punFlags, ref uint pRTime32LastPlayedOnServer )
		{
			var returnValue = _GetFavoriteGame( Self, iGame, ref pnAppID, ref pnIP, ref pnConnPort, ref pnQueryPort, ref punFlags, ref pRTime32LastPlayedOnServer );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate int FAddFavoriteGame( IntPtr self, AppId nAppID, uint nIP, ushort nConnPort, ushort nQueryPort, uint unFlags, uint rTime32LastPlayedOnServer );
		private FAddFavoriteGame _AddFavoriteGame;
		
		#endregion
		internal int AddFavoriteGame( AppId nAppID, uint nIP, ushort nConnPort, ushort nQueryPort, uint unFlags, uint rTime32LastPlayedOnServer )
		{
			var returnValue = _AddFavoriteGame( Self, nAppID, nIP, nConnPort, nQueryPort, unFlags, rTime32LastPlayedOnServer );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FRemoveFavoriteGame( IntPtr self, AppId nAppID, uint nIP, ushort nConnPort, ushort nQueryPort, uint unFlags );
		private FRemoveFavoriteGame _RemoveFavoriteGame;
		
		#endregion
		internal bool RemoveFavoriteGame( AppId nAppID, uint nIP, ushort nConnPort, ushort nQueryPort, uint unFlags )
		{
			var returnValue = _RemoveFavoriteGame( Self, nAppID, nIP, nConnPort, nQueryPort, unFlags );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate SteamAPICall_t FRequestLobbyList( IntPtr self );
		private FRequestLobbyList _RequestLobbyList;
		
		#endregion
		internal async Task<LobbyMatchList_t?> RequestLobbyList()
		{
			var returnValue = _RequestLobbyList( Self );
			return await LobbyMatchList_t.GetResultAsync( returnValue );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate void FAddRequestLobbyListStringFilter( IntPtr self, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchKeyToMatch, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchValueToMatch, LobbyComparison eComparisonType );
		private FAddRequestLobbyListStringFilter _AddRequestLobbyListStringFilter;
		
		#endregion
		internal void AddRequestLobbyListStringFilter( [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchKeyToMatch, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchValueToMatch, LobbyComparison eComparisonType )
		{
			_AddRequestLobbyListStringFilter( Self, pchKeyToMatch, pchValueToMatch, eComparisonType );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate void FAddRequestLobbyListNumericalFilter( IntPtr self, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchKeyToMatch, int nValueToMatch, LobbyComparison eComparisonType );
		private FAddRequestLobbyListNumericalFilter _AddRequestLobbyListNumericalFilter;
		
		#endregion
		internal void AddRequestLobbyListNumericalFilter( [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchKeyToMatch, int nValueToMatch, LobbyComparison eComparisonType )
		{
			_AddRequestLobbyListNumericalFilter( Self, pchKeyToMatch, nValueToMatch, eComparisonType );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate void FAddRequestLobbyListNearValueFilter( IntPtr self, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchKeyToMatch, int nValueToBeCloseTo );
		private FAddRequestLobbyListNearValueFilter _AddRequestLobbyListNearValueFilter;
		
		#endregion
		internal void AddRequestLobbyListNearValueFilter( [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchKeyToMatch, int nValueToBeCloseTo )
		{
			_AddRequestLobbyListNearValueFilter( Self, pchKeyToMatch, nValueToBeCloseTo );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate void FAddRequestLobbyListFilterSlotsAvailable( IntPtr self, int nSlotsAvailable );
		private FAddRequestLobbyListFilterSlotsAvailable _AddRequestLobbyListFilterSlotsAvailable;
		
		#endregion
		internal void AddRequestLobbyListFilterSlotsAvailable( int nSlotsAvailable )
		{
			_AddRequestLobbyListFilterSlotsAvailable( Self, nSlotsAvailable );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate void FAddRequestLobbyListDistanceFilter( IntPtr self, LobbyDistanceFilter eLobbyDistanceFilter );
		private FAddRequestLobbyListDistanceFilter _AddRequestLobbyListDistanceFilter;
		
		#endregion
		internal void AddRequestLobbyListDistanceFilter( LobbyDistanceFilter eLobbyDistanceFilter )
		{
			_AddRequestLobbyListDistanceFilter( Self, eLobbyDistanceFilter );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate void FAddRequestLobbyListResultCountFilter( IntPtr self, int cMaxResults );
		private FAddRequestLobbyListResultCountFilter _AddRequestLobbyListResultCountFilter;
		
		#endregion
		internal void AddRequestLobbyListResultCountFilter( int cMaxResults )
		{
			_AddRequestLobbyListResultCountFilter( Self, cMaxResults );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate void FAddRequestLobbyListCompatibleMembersFilter( IntPtr self, SteamId steamIDLobby );
		private FAddRequestLobbyListCompatibleMembersFilter _AddRequestLobbyListCompatibleMembersFilter;
		
		#endregion
		internal void AddRequestLobbyListCompatibleMembersFilter( SteamId steamIDLobby )
		{
			_AddRequestLobbyListCompatibleMembersFilter( Self, steamIDLobby );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		#if PLATFORM_WIN
		private delegate void FGetLobbyByIndex( IntPtr self, ref SteamId retVal, int iLobby );
		#else
		private delegate SteamId FGetLobbyByIndex( IntPtr self, int iLobby );
		#endif
		private FGetLobbyByIndex _GetLobbyByIndex;
		
		#endregion
		internal SteamId GetLobbyByIndex( int iLobby )
		{
			#if PLATFORM_WIN
			var retVal = default( SteamId );
			_GetLobbyByIndex( Self, ref retVal, iLobby );
			return retVal;
			#else
			var returnValue = _GetLobbyByIndex( Self, iLobby );
			return returnValue;
			#endif
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate SteamAPICall_t FCreateLobby( IntPtr self, LobbyType eLobbyType, int cMaxMembers );
		private FCreateLobby _CreateLobby;
		
		#endregion
		internal async Task<LobbyCreated_t?> CreateLobby( LobbyType eLobbyType, int cMaxMembers )
		{
			var returnValue = _CreateLobby( Self, eLobbyType, cMaxMembers );
			return await LobbyCreated_t.GetResultAsync( returnValue );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate SteamAPICall_t FJoinLobby( IntPtr self, SteamId steamIDLobby );
		private FJoinLobby _JoinLobby;
		
		#endregion
		internal async Task<LobbyEnter_t?> JoinLobby( SteamId steamIDLobby )
		{
			var returnValue = _JoinLobby( Self, steamIDLobby );
			return await LobbyEnter_t.GetResultAsync( returnValue );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate void FLeaveLobby( IntPtr self, SteamId steamIDLobby );
		private FLeaveLobby _LeaveLobby;
		
		#endregion
		internal void LeaveLobby( SteamId steamIDLobby )
		{
			_LeaveLobby( Self, steamIDLobby );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FInviteUserToLobby( IntPtr self, SteamId steamIDLobby, SteamId steamIDInvitee );
		private FInviteUserToLobby _InviteUserToLobby;
		
		#endregion
		internal bool InviteUserToLobby( SteamId steamIDLobby, SteamId steamIDInvitee )
		{
			var returnValue = _InviteUserToLobby( Self, steamIDLobby, steamIDInvitee );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate int FGetNumLobbyMembers( IntPtr self, SteamId steamIDLobby );
		private FGetNumLobbyMembers _GetNumLobbyMembers;
		
		#endregion
		internal int GetNumLobbyMembers( SteamId steamIDLobby )
		{
			var returnValue = _GetNumLobbyMembers( Self, steamIDLobby );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		#if PLATFORM_WIN
		private delegate void FGetLobbyMemberByIndex( IntPtr self, ref SteamId retVal, SteamId steamIDLobby, int iMember );
		#else
		private delegate SteamId FGetLobbyMemberByIndex( IntPtr self, SteamId steamIDLobby, int iMember );
		#endif
		private FGetLobbyMemberByIndex _GetLobbyMemberByIndex;
		
		#endregion
		internal SteamId GetLobbyMemberByIndex( SteamId steamIDLobby, int iMember )
		{
			#if PLATFORM_WIN
			var retVal = default( SteamId );
			_GetLobbyMemberByIndex( Self, ref retVal, steamIDLobby, iMember );
			return retVal;
			#else
			var returnValue = _GetLobbyMemberByIndex( Self, steamIDLobby, iMember );
			return returnValue;
			#endif
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate Utf8StringPointer FGetLobbyData( IntPtr self, SteamId steamIDLobby, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchKey );
		private FGetLobbyData _GetLobbyData;
		
		#endregion
		internal string GetLobbyData( SteamId steamIDLobby, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchKey )
		{
			var returnValue = _GetLobbyData( Self, steamIDLobby, pchKey );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FSetLobbyData( IntPtr self, SteamId steamIDLobby, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchKey, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchValue );
		private FSetLobbyData _SetLobbyData;
		
		#endregion
		internal bool SetLobbyData( SteamId steamIDLobby, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchKey, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchValue )
		{
			var returnValue = _SetLobbyData( Self, steamIDLobby, pchKey, pchValue );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate int FGetLobbyDataCount( IntPtr self, SteamId steamIDLobby );
		private FGetLobbyDataCount _GetLobbyDataCount;
		
		#endregion
		internal int GetLobbyDataCount( SteamId steamIDLobby )
		{
			var returnValue = _GetLobbyDataCount( Self, steamIDLobby );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FGetLobbyDataByIndex( IntPtr self, SteamId steamIDLobby, int iLobbyData, IntPtr pchKey, int cchKeyBufferSize, IntPtr pchValue, int cchValueBufferSize );
		private FGetLobbyDataByIndex _GetLobbyDataByIndex;
		
		#endregion
		internal bool GetLobbyDataByIndex( SteamId steamIDLobby, int iLobbyData, out string pchKey, out string pchValue )
		{
			IntPtr mempchKey = Helpers.TakeMemory();
			IntPtr mempchValue = Helpers.TakeMemory();
			var returnValue = _GetLobbyDataByIndex( Self, steamIDLobby, iLobbyData, mempchKey, (1024 * 32), mempchValue, (1024 * 32) );
			pchKey = Helpers.MemoryToString( mempchKey );
			pchValue = Helpers.MemoryToString( mempchValue );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FDeleteLobbyData( IntPtr self, SteamId steamIDLobby, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchKey );
		private FDeleteLobbyData _DeleteLobbyData;
		
		#endregion
		internal bool DeleteLobbyData( SteamId steamIDLobby, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchKey )
		{
			var returnValue = _DeleteLobbyData( Self, steamIDLobby, pchKey );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate Utf8StringPointer FGetLobbyMemberData( IntPtr self, SteamId steamIDLobby, SteamId steamIDUser, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchKey );
		private FGetLobbyMemberData _GetLobbyMemberData;
		
		#endregion
		internal string GetLobbyMemberData( SteamId steamIDLobby, SteamId steamIDUser, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchKey )
		{
			var returnValue = _GetLobbyMemberData( Self, steamIDLobby, steamIDUser, pchKey );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate void FSetLobbyMemberData( IntPtr self, SteamId steamIDLobby, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchKey, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchValue );
		private FSetLobbyMemberData _SetLobbyMemberData;
		
		#endregion
		internal void SetLobbyMemberData( SteamId steamIDLobby, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchKey, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchValue )
		{
			_SetLobbyMemberData( Self, steamIDLobby, pchKey, pchValue );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FSendLobbyChatMsg( IntPtr self, SteamId steamIDLobby, IntPtr pvMsgBody, int cubMsgBody );
		private FSendLobbyChatMsg _SendLobbyChatMsg;
		
		#endregion
		internal bool SendLobbyChatMsg( SteamId steamIDLobby, IntPtr pvMsgBody, int cubMsgBody )
		{
			var returnValue = _SendLobbyChatMsg( Self, steamIDLobby, pvMsgBody, cubMsgBody );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate int FGetLobbyChatEntry( IntPtr self, SteamId steamIDLobby, int iChatID, ref SteamId pSteamIDUser, IntPtr pvData, int cubData, ref ChatEntryType peChatEntryType );
		private FGetLobbyChatEntry _GetLobbyChatEntry;
		
		#endregion
		internal int GetLobbyChatEntry( SteamId steamIDLobby, int iChatID, ref SteamId pSteamIDUser, IntPtr pvData, int cubData, ref ChatEntryType peChatEntryType )
		{
			var returnValue = _GetLobbyChatEntry( Self, steamIDLobby, iChatID, ref pSteamIDUser, pvData, cubData, ref peChatEntryType );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FRequestLobbyData( IntPtr self, SteamId steamIDLobby );
		private FRequestLobbyData _RequestLobbyData;
		
		#endregion
		internal bool RequestLobbyData( SteamId steamIDLobby )
		{
			var returnValue = _RequestLobbyData( Self, steamIDLobby );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate void FSetLobbyGameServer( IntPtr self, SteamId steamIDLobby, uint unGameServerIP, ushort unGameServerPort, SteamId steamIDGameServer );
		private FSetLobbyGameServer _SetLobbyGameServer;
		
		#endregion
		internal void SetLobbyGameServer( SteamId steamIDLobby, uint unGameServerIP, ushort unGameServerPort, SteamId steamIDGameServer )
		{
			_SetLobbyGameServer( Self, steamIDLobby, unGameServerIP, unGameServerPort, steamIDGameServer );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FGetLobbyGameServer( IntPtr self, SteamId steamIDLobby, ref uint punGameServerIP, ref ushort punGameServerPort, ref SteamId psteamIDGameServer );
		private FGetLobbyGameServer _GetLobbyGameServer;
		
		#endregion
		internal bool GetLobbyGameServer( SteamId steamIDLobby, ref uint punGameServerIP, ref ushort punGameServerPort, ref SteamId psteamIDGameServer )
		{
			var returnValue = _GetLobbyGameServer( Self, steamIDLobby, ref punGameServerIP, ref punGameServerPort, ref psteamIDGameServer );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FSetLobbyMemberLimit( IntPtr self, SteamId steamIDLobby, int cMaxMembers );
		private FSetLobbyMemberLimit _SetLobbyMemberLimit;
		
		#endregion
		internal bool SetLobbyMemberLimit( SteamId steamIDLobby, int cMaxMembers )
		{
			var returnValue = _SetLobbyMemberLimit( Self, steamIDLobby, cMaxMembers );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate int FGetLobbyMemberLimit( IntPtr self, SteamId steamIDLobby );
		private FGetLobbyMemberLimit _GetLobbyMemberLimit;
		
		#endregion
		internal int GetLobbyMemberLimit( SteamId steamIDLobby )
		{
			var returnValue = _GetLobbyMemberLimit( Self, steamIDLobby );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FSetLobbyType( IntPtr self, SteamId steamIDLobby, LobbyType eLobbyType );
		private FSetLobbyType _SetLobbyType;
		
		#endregion
		internal bool SetLobbyType( SteamId steamIDLobby, LobbyType eLobbyType )
		{
			var returnValue = _SetLobbyType( Self, steamIDLobby, eLobbyType );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FSetLobbyJoinable( IntPtr self, SteamId steamIDLobby, [MarshalAs( UnmanagedType.U1 )] bool bLobbyJoinable );
		private FSetLobbyJoinable _SetLobbyJoinable;
		
		#endregion
		internal bool SetLobbyJoinable( SteamId steamIDLobby, [MarshalAs( UnmanagedType.U1 )] bool bLobbyJoinable )
		{
			var returnValue = _SetLobbyJoinable( Self, steamIDLobby, bLobbyJoinable );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		#if PLATFORM_WIN
		private delegate void FGetLobbyOwner( IntPtr self, ref SteamId retVal, SteamId steamIDLobby );
		#else
		private delegate SteamId FGetLobbyOwner( IntPtr self, SteamId steamIDLobby );
		#endif
		private FGetLobbyOwner _GetLobbyOwner;
		
		#endregion
		internal SteamId GetLobbyOwner( SteamId steamIDLobby )
		{
			#if PLATFORM_WIN
			var retVal = default( SteamId );
			_GetLobbyOwner( Self, ref retVal, steamIDLobby );
			return retVal;
			#else
			var returnValue = _GetLobbyOwner( Self, steamIDLobby );
			return returnValue;
			#endif
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FSetLobbyOwner( IntPtr self, SteamId steamIDLobby, SteamId steamIDNewOwner );
		private FSetLobbyOwner _SetLobbyOwner;
		
		#endregion
		internal bool SetLobbyOwner( SteamId steamIDLobby, SteamId steamIDNewOwner )
		{
			var returnValue = _SetLobbyOwner( Self, steamIDLobby, steamIDNewOwner );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FSetLinkedLobby( IntPtr self, SteamId steamIDLobby, SteamId steamIDLobbyDependent );
		private FSetLinkedLobby _SetLinkedLobby;
		
		#endregion
		internal bool SetLinkedLobby( SteamId steamIDLobby, SteamId steamIDLobbyDependent )
		{
			var returnValue = _SetLinkedLobby( Self, steamIDLobby, steamIDLobbyDependent );
			return returnValue;
		}
		
	}
}
