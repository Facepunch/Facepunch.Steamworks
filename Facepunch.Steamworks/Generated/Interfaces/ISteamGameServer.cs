using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Steamworks.Data;


namespace Steamworks
{
	internal class ISteamGameServer : SteamInterface
	{
		public override string InterfaceName => "SteamGameServer012";
		
		public override void InitInternals()
		{
			_InitGameServer = Marshal.GetDelegateForFunctionPointer<FInitGameServer>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 0 ) ) );
			_SetProduct = Marshal.GetDelegateForFunctionPointer<FSetProduct>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 8 ) ) );
			_SetGameDescription = Marshal.GetDelegateForFunctionPointer<FSetGameDescription>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 16 ) ) );
			_SetModDir = Marshal.GetDelegateForFunctionPointer<FSetModDir>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 24 ) ) );
			_SetDedicatedServer = Marshal.GetDelegateForFunctionPointer<FSetDedicatedServer>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 32 ) ) );
			_LogOn = Marshal.GetDelegateForFunctionPointer<FLogOn>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 40 ) ) );
			_LogOnAnonymous = Marshal.GetDelegateForFunctionPointer<FLogOnAnonymous>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 48 ) ) );
			_LogOff = Marshal.GetDelegateForFunctionPointer<FLogOff>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 56 ) ) );
			_BLoggedOn = Marshal.GetDelegateForFunctionPointer<FBLoggedOn>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 64 ) ) );
			_BSecure = Marshal.GetDelegateForFunctionPointer<FBSecure>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 72 ) ) );
			_GetSteamID = Marshal.GetDelegateForFunctionPointer<FGetSteamID>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 80 ) ) );
			_WasRestartRequested = Marshal.GetDelegateForFunctionPointer<FWasRestartRequested>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 88 ) ) );
			_SetMaxPlayerCount = Marshal.GetDelegateForFunctionPointer<FSetMaxPlayerCount>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 96 ) ) );
			_SetBotPlayerCount = Marshal.GetDelegateForFunctionPointer<FSetBotPlayerCount>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 104 ) ) );
			_SetServerName = Marshal.GetDelegateForFunctionPointer<FSetServerName>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 112 ) ) );
			_SetMapName = Marshal.GetDelegateForFunctionPointer<FSetMapName>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 120 ) ) );
			_SetPasswordProtected = Marshal.GetDelegateForFunctionPointer<FSetPasswordProtected>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 128 ) ) );
			_SetSpectatorPort = Marshal.GetDelegateForFunctionPointer<FSetSpectatorPort>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 136 ) ) );
			_SetSpectatorServerName = Marshal.GetDelegateForFunctionPointer<FSetSpectatorServerName>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 144 ) ) );
			_ClearAllKeyValues = Marshal.GetDelegateForFunctionPointer<FClearAllKeyValues>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 152 ) ) );
			_SetKeyValue = Marshal.GetDelegateForFunctionPointer<FSetKeyValue>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 160 ) ) );
			_SetGameTags = Marshal.GetDelegateForFunctionPointer<FSetGameTags>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 168 ) ) );
			_SetGameData = Marshal.GetDelegateForFunctionPointer<FSetGameData>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 176 ) ) );
			_SetRegion = Marshal.GetDelegateForFunctionPointer<FSetRegion>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 184 ) ) );
			_SendUserConnectAndAuthenticate = Marshal.GetDelegateForFunctionPointer<FSendUserConnectAndAuthenticate>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 192 ) ) );
			_CreateUnauthenticatedUserConnection = Marshal.GetDelegateForFunctionPointer<FCreateUnauthenticatedUserConnection>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 200 ) ) );
			_SendUserDisconnect = Marshal.GetDelegateForFunctionPointer<FSendUserDisconnect>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 208 ) ) );
			_BUpdateUserData = Marshal.GetDelegateForFunctionPointer<FBUpdateUserData>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 216 ) ) );
			_GetAuthSessionTicket = Marshal.GetDelegateForFunctionPointer<FGetAuthSessionTicket>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 224 ) ) );
			_BeginAuthSession = Marshal.GetDelegateForFunctionPointer<FBeginAuthSession>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 232 ) ) );
			_EndAuthSession = Marshal.GetDelegateForFunctionPointer<FEndAuthSession>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 240 ) ) );
			_CancelAuthTicket = Marshal.GetDelegateForFunctionPointer<FCancelAuthTicket>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 248 ) ) );
			_UserHasLicenseForApp = Marshal.GetDelegateForFunctionPointer<FUserHasLicenseForApp>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 256 ) ) );
			_RequestUserGroupStatus = Marshal.GetDelegateForFunctionPointer<FRequestUserGroupStatus>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 264 ) ) );
			_GetGameplayStats = Marshal.GetDelegateForFunctionPointer<FGetGameplayStats>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 272 ) ) );
			_GetServerReputation = Marshal.GetDelegateForFunctionPointer<FGetServerReputation>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 280 ) ) );
			_GetPublicIP = Marshal.GetDelegateForFunctionPointer<FGetPublicIP>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 288 ) ) );
			_HandleIncomingPacket = Marshal.GetDelegateForFunctionPointer<FHandleIncomingPacket>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 296 ) ) );
			_GetNextOutgoingPacket = Marshal.GetDelegateForFunctionPointer<FGetNextOutgoingPacket>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 304 ) ) );
			_EnableHeartbeats = Marshal.GetDelegateForFunctionPointer<FEnableHeartbeats>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 312 ) ) );
			_SetHeartbeatInterval = Marshal.GetDelegateForFunctionPointer<FSetHeartbeatInterval>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 320 ) ) );
			_ForceHeartbeat = Marshal.GetDelegateForFunctionPointer<FForceHeartbeat>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 328 ) ) );
			_AssociateWithClan = Marshal.GetDelegateForFunctionPointer<FAssociateWithClan>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 336 ) ) );
			_ComputeNewPlayerCompatibility = Marshal.GetDelegateForFunctionPointer<FComputeNewPlayerCompatibility>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 344 ) ) );
		}
		internal override void Shutdown()
		{
			base.Shutdown();
			
			_InitGameServer = null;
			_SetProduct = null;
			_SetGameDescription = null;
			_SetModDir = null;
			_SetDedicatedServer = null;
			_LogOn = null;
			_LogOnAnonymous = null;
			_LogOff = null;
			_BLoggedOn = null;
			_BSecure = null;
			_GetSteamID = null;
			_WasRestartRequested = null;
			_SetMaxPlayerCount = null;
			_SetBotPlayerCount = null;
			_SetServerName = null;
			_SetMapName = null;
			_SetPasswordProtected = null;
			_SetSpectatorPort = null;
			_SetSpectatorServerName = null;
			_ClearAllKeyValues = null;
			_SetKeyValue = null;
			_SetGameTags = null;
			_SetGameData = null;
			_SetRegion = null;
			_SendUserConnectAndAuthenticate = null;
			_CreateUnauthenticatedUserConnection = null;
			_SendUserDisconnect = null;
			_BUpdateUserData = null;
			_GetAuthSessionTicket = null;
			_BeginAuthSession = null;
			_EndAuthSession = null;
			_CancelAuthTicket = null;
			_UserHasLicenseForApp = null;
			_RequestUserGroupStatus = null;
			_GetGameplayStats = null;
			_GetServerReputation = null;
			_GetPublicIP = null;
			_HandleIncomingPacket = null;
			_GetNextOutgoingPacket = null;
			_EnableHeartbeats = null;
			_SetHeartbeatInterval = null;
			_ForceHeartbeat = null;
			_AssociateWithClan = null;
			_ComputeNewPlayerCompatibility = null;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FInitGameServer( IntPtr self, uint unIP, ushort usGamePort, ushort usQueryPort, uint unFlags, AppId nGameAppId, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchVersionString );
		private FInitGameServer _InitGameServer;
		
		#endregion
		internal bool InitGameServer( uint unIP, ushort usGamePort, ushort usQueryPort, uint unFlags, AppId nGameAppId, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchVersionString )
		{
			var returnValue = _InitGameServer( Self, unIP, usGamePort, usQueryPort, unFlags, nGameAppId, pchVersionString );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate void FSetProduct( IntPtr self, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pszProduct );
		private FSetProduct _SetProduct;
		
		#endregion
		internal void SetProduct( [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pszProduct )
		{
			_SetProduct( Self, pszProduct );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate void FSetGameDescription( IntPtr self, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pszGameDescription );
		private FSetGameDescription _SetGameDescription;
		
		#endregion
		internal void SetGameDescription( [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pszGameDescription )
		{
			_SetGameDescription( Self, pszGameDescription );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate void FSetModDir( IntPtr self, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pszModDir );
		private FSetModDir _SetModDir;
		
		#endregion
		internal void SetModDir( [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pszModDir )
		{
			_SetModDir( Self, pszModDir );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate void FSetDedicatedServer( IntPtr self, [MarshalAs( UnmanagedType.U1 )] bool bDedicated );
		private FSetDedicatedServer _SetDedicatedServer;
		
		#endregion
		internal void SetDedicatedServer( [MarshalAs( UnmanagedType.U1 )] bool bDedicated )
		{
			_SetDedicatedServer( Self, bDedicated );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate void FLogOn( IntPtr self, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pszToken );
		private FLogOn _LogOn;
		
		#endregion
		internal void LogOn( [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pszToken )
		{
			_LogOn( Self, pszToken );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate void FLogOnAnonymous( IntPtr self );
		private FLogOnAnonymous _LogOnAnonymous;
		
		#endregion
		internal void LogOnAnonymous()
		{
			_LogOnAnonymous( Self );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate void FLogOff( IntPtr self );
		private FLogOff _LogOff;
		
		#endregion
		internal void LogOff()
		{
			_LogOff( Self );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FBLoggedOn( IntPtr self );
		private FBLoggedOn _BLoggedOn;
		
		#endregion
		internal bool BLoggedOn()
		{
			var returnValue = _BLoggedOn( Self );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FBSecure( IntPtr self );
		private FBSecure _BSecure;
		
		#endregion
		internal bool BSecure()
		{
			var returnValue = _BSecure( Self );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		#if PLATFORM_WIN
		private delegate void FGetSteamID( IntPtr self, ref SteamId retVal );
		#else
		private delegate SteamId FGetSteamID( IntPtr self );
		#endif
		private FGetSteamID _GetSteamID;
		
		#endregion
		internal SteamId GetSteamID()
		{
			#if PLATFORM_WIN
			var retVal = default( SteamId );
			_GetSteamID( Self, ref retVal );
			return retVal;
			#else
			var returnValue = _GetSteamID( Self );
			return returnValue;
			#endif
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FWasRestartRequested( IntPtr self );
		private FWasRestartRequested _WasRestartRequested;
		
		#endregion
		internal bool WasRestartRequested()
		{
			var returnValue = _WasRestartRequested( Self );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate void FSetMaxPlayerCount( IntPtr self, int cPlayersMax );
		private FSetMaxPlayerCount _SetMaxPlayerCount;
		
		#endregion
		internal void SetMaxPlayerCount( int cPlayersMax )
		{
			_SetMaxPlayerCount( Self, cPlayersMax );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate void FSetBotPlayerCount( IntPtr self, int cBotplayers );
		private FSetBotPlayerCount _SetBotPlayerCount;
		
		#endregion
		internal void SetBotPlayerCount( int cBotplayers )
		{
			_SetBotPlayerCount( Self, cBotplayers );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate void FSetServerName( IntPtr self, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pszServerName );
		private FSetServerName _SetServerName;
		
		#endregion
		internal void SetServerName( [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pszServerName )
		{
			_SetServerName( Self, pszServerName );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate void FSetMapName( IntPtr self, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pszMapName );
		private FSetMapName _SetMapName;
		
		#endregion
		internal void SetMapName( [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pszMapName )
		{
			_SetMapName( Self, pszMapName );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate void FSetPasswordProtected( IntPtr self, [MarshalAs( UnmanagedType.U1 )] bool bPasswordProtected );
		private FSetPasswordProtected _SetPasswordProtected;
		
		#endregion
		internal void SetPasswordProtected( [MarshalAs( UnmanagedType.U1 )] bool bPasswordProtected )
		{
			_SetPasswordProtected( Self, bPasswordProtected );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate void FSetSpectatorPort( IntPtr self, ushort unSpectatorPort );
		private FSetSpectatorPort _SetSpectatorPort;
		
		#endregion
		internal void SetSpectatorPort( ushort unSpectatorPort )
		{
			_SetSpectatorPort( Self, unSpectatorPort );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate void FSetSpectatorServerName( IntPtr self, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pszSpectatorServerName );
		private FSetSpectatorServerName _SetSpectatorServerName;
		
		#endregion
		internal void SetSpectatorServerName( [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pszSpectatorServerName )
		{
			_SetSpectatorServerName( Self, pszSpectatorServerName );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate void FClearAllKeyValues( IntPtr self );
		private FClearAllKeyValues _ClearAllKeyValues;
		
		#endregion
		internal void ClearAllKeyValues()
		{
			_ClearAllKeyValues( Self );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate void FSetKeyValue( IntPtr self, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pKey, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pValue );
		private FSetKeyValue _SetKeyValue;
		
		#endregion
		internal void SetKeyValue( [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pKey, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pValue )
		{
			_SetKeyValue( Self, pKey, pValue );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate void FSetGameTags( IntPtr self, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchGameTags );
		private FSetGameTags _SetGameTags;
		
		#endregion
		internal void SetGameTags( [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchGameTags )
		{
			_SetGameTags( Self, pchGameTags );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate void FSetGameData( IntPtr self, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchGameData );
		private FSetGameData _SetGameData;
		
		#endregion
		internal void SetGameData( [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchGameData )
		{
			_SetGameData( Self, pchGameData );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate void FSetRegion( IntPtr self, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pszRegion );
		private FSetRegion _SetRegion;
		
		#endregion
		internal void SetRegion( [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pszRegion )
		{
			_SetRegion( Self, pszRegion );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FSendUserConnectAndAuthenticate( IntPtr self, uint unIPClient, IntPtr pvAuthBlob, uint cubAuthBlobSize, ref SteamId pSteamIDUser );
		private FSendUserConnectAndAuthenticate _SendUserConnectAndAuthenticate;
		
		#endregion
		internal bool SendUserConnectAndAuthenticate( uint unIPClient, IntPtr pvAuthBlob, uint cubAuthBlobSize, ref SteamId pSteamIDUser )
		{
			var returnValue = _SendUserConnectAndAuthenticate( Self, unIPClient, pvAuthBlob, cubAuthBlobSize, ref pSteamIDUser );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		#if PLATFORM_WIN
		private delegate void FCreateUnauthenticatedUserConnection( IntPtr self, ref SteamId retVal );
		#else
		private delegate SteamId FCreateUnauthenticatedUserConnection( IntPtr self );
		#endif
		private FCreateUnauthenticatedUserConnection _CreateUnauthenticatedUserConnection;
		
		#endregion
		internal SteamId CreateUnauthenticatedUserConnection()
		{
			#if PLATFORM_WIN
			var retVal = default( SteamId );
			_CreateUnauthenticatedUserConnection( Self, ref retVal );
			return retVal;
			#else
			var returnValue = _CreateUnauthenticatedUserConnection( Self );
			return returnValue;
			#endif
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate void FSendUserDisconnect( IntPtr self, SteamId steamIDUser );
		private FSendUserDisconnect _SendUserDisconnect;
		
		#endregion
		internal void SendUserDisconnect( SteamId steamIDUser )
		{
			_SendUserDisconnect( Self, steamIDUser );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FBUpdateUserData( IntPtr self, SteamId steamIDUser, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchPlayerName, uint uScore );
		private FBUpdateUserData _BUpdateUserData;
		
		#endregion
		internal bool BUpdateUserData( SteamId steamIDUser, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchPlayerName, uint uScore )
		{
			var returnValue = _BUpdateUserData( Self, steamIDUser, pchPlayerName, uScore );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate HAuthTicket FGetAuthSessionTicket( IntPtr self, IntPtr pTicket, int cbMaxTicket, ref uint pcbTicket );
		private FGetAuthSessionTicket _GetAuthSessionTicket;
		
		#endregion
		internal HAuthTicket GetAuthSessionTicket( IntPtr pTicket, int cbMaxTicket, ref uint pcbTicket )
		{
			var returnValue = _GetAuthSessionTicket( Self, pTicket, cbMaxTicket, ref pcbTicket );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate BeginAuthResult FBeginAuthSession( IntPtr self, IntPtr pAuthTicket, int cbAuthTicket, SteamId steamID );
		private FBeginAuthSession _BeginAuthSession;
		
		#endregion
		internal BeginAuthResult BeginAuthSession( IntPtr pAuthTicket, int cbAuthTicket, SteamId steamID )
		{
			var returnValue = _BeginAuthSession( Self, pAuthTicket, cbAuthTicket, steamID );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate void FEndAuthSession( IntPtr self, SteamId steamID );
		private FEndAuthSession _EndAuthSession;
		
		#endregion
		internal void EndAuthSession( SteamId steamID )
		{
			_EndAuthSession( Self, steamID );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate void FCancelAuthTicket( IntPtr self, HAuthTicket hAuthTicket );
		private FCancelAuthTicket _CancelAuthTicket;
		
		#endregion
		internal void CancelAuthTicket( HAuthTicket hAuthTicket )
		{
			_CancelAuthTicket( Self, hAuthTicket );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate UserHasLicenseForAppResult FUserHasLicenseForApp( IntPtr self, SteamId steamID, AppId appID );
		private FUserHasLicenseForApp _UserHasLicenseForApp;
		
		#endregion
		internal UserHasLicenseForAppResult UserHasLicenseForApp( SteamId steamID, AppId appID )
		{
			var returnValue = _UserHasLicenseForApp( Self, steamID, appID );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FRequestUserGroupStatus( IntPtr self, SteamId steamIDUser, SteamId steamIDGroup );
		private FRequestUserGroupStatus _RequestUserGroupStatus;
		
		#endregion
		internal bool RequestUserGroupStatus( SteamId steamIDUser, SteamId steamIDGroup )
		{
			var returnValue = _RequestUserGroupStatus( Self, steamIDUser, steamIDGroup );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate void FGetGameplayStats( IntPtr self );
		private FGetGameplayStats _GetGameplayStats;
		
		#endregion
		internal void GetGameplayStats()
		{
			_GetGameplayStats( Self );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate SteamAPICall_t FGetServerReputation( IntPtr self );
		private FGetServerReputation _GetServerReputation;
		
		#endregion
		internal async Task<GSReputation_t?> GetServerReputation()
		{
			var returnValue = _GetServerReputation( Self );
			return await GSReputation_t.GetResultAsync( returnValue );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate uint FGetPublicIP( IntPtr self );
		private FGetPublicIP _GetPublicIP;
		
		#endregion
		internal uint GetPublicIP()
		{
			var returnValue = _GetPublicIP( Self );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FHandleIncomingPacket( IntPtr self, IntPtr pData, int cbData, uint srcIP, ushort srcPort );
		private FHandleIncomingPacket _HandleIncomingPacket;
		
		#endregion
		internal bool HandleIncomingPacket( IntPtr pData, int cbData, uint srcIP, ushort srcPort )
		{
			var returnValue = _HandleIncomingPacket( Self, pData, cbData, srcIP, srcPort );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate int FGetNextOutgoingPacket( IntPtr self, IntPtr pOut, int cbMaxOut, ref uint pNetAdr, ref ushort pPort );
		private FGetNextOutgoingPacket _GetNextOutgoingPacket;
		
		#endregion
		internal int GetNextOutgoingPacket( IntPtr pOut, int cbMaxOut, ref uint pNetAdr, ref ushort pPort )
		{
			var returnValue = _GetNextOutgoingPacket( Self, pOut, cbMaxOut, ref pNetAdr, ref pPort );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate void FEnableHeartbeats( IntPtr self, [MarshalAs( UnmanagedType.U1 )] bool bActive );
		private FEnableHeartbeats _EnableHeartbeats;
		
		#endregion
		internal void EnableHeartbeats( [MarshalAs( UnmanagedType.U1 )] bool bActive )
		{
			_EnableHeartbeats( Self, bActive );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate void FSetHeartbeatInterval( IntPtr self, int iHeartbeatInterval );
		private FSetHeartbeatInterval _SetHeartbeatInterval;
		
		#endregion
		internal void SetHeartbeatInterval( int iHeartbeatInterval )
		{
			_SetHeartbeatInterval( Self, iHeartbeatInterval );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate void FForceHeartbeat( IntPtr self );
		private FForceHeartbeat _ForceHeartbeat;
		
		#endregion
		internal void ForceHeartbeat()
		{
			_ForceHeartbeat( Self );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate SteamAPICall_t FAssociateWithClan( IntPtr self, SteamId steamIDClan );
		private FAssociateWithClan _AssociateWithClan;
		
		#endregion
		internal async Task<AssociateWithClanResult_t?> AssociateWithClan( SteamId steamIDClan )
		{
			var returnValue = _AssociateWithClan( Self, steamIDClan );
			return await AssociateWithClanResult_t.GetResultAsync( returnValue );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate SteamAPICall_t FComputeNewPlayerCompatibility( IntPtr self, SteamId steamIDNewPlayer );
		private FComputeNewPlayerCompatibility _ComputeNewPlayerCompatibility;
		
		#endregion
		internal async Task<ComputeNewPlayerCompatibilityResult_t?> ComputeNewPlayerCompatibility( SteamId steamIDNewPlayer )
		{
			var returnValue = _ComputeNewPlayerCompatibility( Self, steamIDNewPlayer );
			return await ComputeNewPlayerCompatibilityResult_t.GetResultAsync( returnValue );
		}
		
	}
}
