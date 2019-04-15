using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using SteamNative;


namespace Steamworks.Internal
{
	public class ISteamGameServer : BaseSteamInterface
	{
		public ISteamGameServer( bool server = false ) : base( server )
		{
		}
		
		public override string InterfaceName => "SteamGameServer012";
		
		public override void InitInternals()
		{
			InitGameServerDelegatePointer = Marshal.GetDelegateForFunctionPointer<InitGameServerDelegate>( Marshal.ReadIntPtr( VTable, 0) );
			SetProductDelegatePointer = Marshal.GetDelegateForFunctionPointer<SetProductDelegate>( Marshal.ReadIntPtr( VTable, 8) );
			SetGameDescriptionDelegatePointer = Marshal.GetDelegateForFunctionPointer<SetGameDescriptionDelegate>( Marshal.ReadIntPtr( VTable, 16) );
			SetModDirDelegatePointer = Marshal.GetDelegateForFunctionPointer<SetModDirDelegate>( Marshal.ReadIntPtr( VTable, 24) );
			SetDedicatedServerDelegatePointer = Marshal.GetDelegateForFunctionPointer<SetDedicatedServerDelegate>( Marshal.ReadIntPtr( VTable, 32) );
			LogOnDelegatePointer = Marshal.GetDelegateForFunctionPointer<LogOnDelegate>( Marshal.ReadIntPtr( VTable, 40) );
			LogOnAnonymousDelegatePointer = Marshal.GetDelegateForFunctionPointer<LogOnAnonymousDelegate>( Marshal.ReadIntPtr( VTable, 48) );
			LogOffDelegatePointer = Marshal.GetDelegateForFunctionPointer<LogOffDelegate>( Marshal.ReadIntPtr( VTable, 56) );
			BLoggedOnDelegatePointer = Marshal.GetDelegateForFunctionPointer<BLoggedOnDelegate>( Marshal.ReadIntPtr( VTable, 64) );
			BSecureDelegatePointer = Marshal.GetDelegateForFunctionPointer<BSecureDelegate>( Marshal.ReadIntPtr( VTable, 72) );
			GetSteamIDDelegatePointer = Marshal.GetDelegateForFunctionPointer<GetSteamIDDelegate>( Marshal.ReadIntPtr( VTable, 80) );
			WasRestartRequestedDelegatePointer = Marshal.GetDelegateForFunctionPointer<WasRestartRequestedDelegate>( Marshal.ReadIntPtr( VTable, 88) );
			SetMaxPlayerCountDelegatePointer = Marshal.GetDelegateForFunctionPointer<SetMaxPlayerCountDelegate>( Marshal.ReadIntPtr( VTable, 96) );
			SetBotPlayerCountDelegatePointer = Marshal.GetDelegateForFunctionPointer<SetBotPlayerCountDelegate>( Marshal.ReadIntPtr( VTable, 104) );
			SetServerNameDelegatePointer = Marshal.GetDelegateForFunctionPointer<SetServerNameDelegate>( Marshal.ReadIntPtr( VTable, 112) );
			SetMapNameDelegatePointer = Marshal.GetDelegateForFunctionPointer<SetMapNameDelegate>( Marshal.ReadIntPtr( VTable, 120) );
			SetPasswordProtectedDelegatePointer = Marshal.GetDelegateForFunctionPointer<SetPasswordProtectedDelegate>( Marshal.ReadIntPtr( VTable, 128) );
			SetSpectatorPortDelegatePointer = Marshal.GetDelegateForFunctionPointer<SetSpectatorPortDelegate>( Marshal.ReadIntPtr( VTable, 136) );
			SetSpectatorServerNameDelegatePointer = Marshal.GetDelegateForFunctionPointer<SetSpectatorServerNameDelegate>( Marshal.ReadIntPtr( VTable, 144) );
			ClearAllKeyValuesDelegatePointer = Marshal.GetDelegateForFunctionPointer<ClearAllKeyValuesDelegate>( Marshal.ReadIntPtr( VTable, 152) );
			SetKeyValueDelegatePointer = Marshal.GetDelegateForFunctionPointer<SetKeyValueDelegate>( Marshal.ReadIntPtr( VTable, 160) );
			SetGameTagsDelegatePointer = Marshal.GetDelegateForFunctionPointer<SetGameTagsDelegate>( Marshal.ReadIntPtr( VTable, 168) );
			SetGameDataDelegatePointer = Marshal.GetDelegateForFunctionPointer<SetGameDataDelegate>( Marshal.ReadIntPtr( VTable, 176) );
			SetRegionDelegatePointer = Marshal.GetDelegateForFunctionPointer<SetRegionDelegate>( Marshal.ReadIntPtr( VTable, 184) );
			SendUserConnectAndAuthenticateDelegatePointer = Marshal.GetDelegateForFunctionPointer<SendUserConnectAndAuthenticateDelegate>( Marshal.ReadIntPtr( VTable, 192) );
			CreateUnauthenticatedUserConnectionDelegatePointer = Marshal.GetDelegateForFunctionPointer<CreateUnauthenticatedUserConnectionDelegate>( Marshal.ReadIntPtr( VTable, 200) );
			SendUserDisconnectDelegatePointer = Marshal.GetDelegateForFunctionPointer<SendUserDisconnectDelegate>( Marshal.ReadIntPtr( VTable, 208) );
			BUpdateUserDataDelegatePointer = Marshal.GetDelegateForFunctionPointer<BUpdateUserDataDelegate>( Marshal.ReadIntPtr( VTable, 216) );
			GetAuthSessionTicketDelegatePointer = Marshal.GetDelegateForFunctionPointer<GetAuthSessionTicketDelegate>( Marshal.ReadIntPtr( VTable, 224) );
			BeginAuthSessionDelegatePointer = Marshal.GetDelegateForFunctionPointer<BeginAuthSessionDelegate>( Marshal.ReadIntPtr( VTable, 232) );
			EndAuthSessionDelegatePointer = Marshal.GetDelegateForFunctionPointer<EndAuthSessionDelegate>( Marshal.ReadIntPtr( VTable, 240) );
			CancelAuthTicketDelegatePointer = Marshal.GetDelegateForFunctionPointer<CancelAuthTicketDelegate>( Marshal.ReadIntPtr( VTable, 248) );
			UserHasLicenseForAppDelegatePointer = Marshal.GetDelegateForFunctionPointer<UserHasLicenseForAppDelegate>( Marshal.ReadIntPtr( VTable, 256) );
			RequestUserGroupStatusDelegatePointer = Marshal.GetDelegateForFunctionPointer<RequestUserGroupStatusDelegate>( Marshal.ReadIntPtr( VTable, 264) );
			GetGameplayStatsDelegatePointer = Marshal.GetDelegateForFunctionPointer<GetGameplayStatsDelegate>( Marshal.ReadIntPtr( VTable, 272) );
			GetServerReputationDelegatePointer = Marshal.GetDelegateForFunctionPointer<GetServerReputationDelegate>( Marshal.ReadIntPtr( VTable, 280) );
			GetPublicIPDelegatePointer = Marshal.GetDelegateForFunctionPointer<GetPublicIPDelegate>( Marshal.ReadIntPtr( VTable, 288) );
			HandleIncomingPacketDelegatePointer = Marshal.GetDelegateForFunctionPointer<HandleIncomingPacketDelegate>( Marshal.ReadIntPtr( VTable, 296) );
			GetNextOutgoingPacketDelegatePointer = Marshal.GetDelegateForFunctionPointer<GetNextOutgoingPacketDelegate>( Marshal.ReadIntPtr( VTable, 304) );
			EnableHeartbeatsDelegatePointer = Marshal.GetDelegateForFunctionPointer<EnableHeartbeatsDelegate>( Marshal.ReadIntPtr( VTable, 312) );
			SetHeartbeatIntervalDelegatePointer = Marshal.GetDelegateForFunctionPointer<SetHeartbeatIntervalDelegate>( Marshal.ReadIntPtr( VTable, 320) );
			ForceHeartbeatDelegatePointer = Marshal.GetDelegateForFunctionPointer<ForceHeartbeatDelegate>( Marshal.ReadIntPtr( VTable, 328) );
			AssociateWithClanDelegatePointer = Marshal.GetDelegateForFunctionPointer<AssociateWithClanDelegate>( Marshal.ReadIntPtr( VTable, 336) );
			ComputeNewPlayerCompatibilityDelegatePointer = Marshal.GetDelegateForFunctionPointer<ComputeNewPlayerCompatibilityDelegate>( Marshal.ReadIntPtr( VTable, 344) );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		public delegate bool InitGameServerDelegate( IntPtr self, uint unIP, ushort usGamePort, ushort usQueryPort, uint unFlags, AppId_t nGameAppId, string pchVersionString );
		private InitGameServerDelegate InitGameServerDelegatePointer;
		
		#endregion
		public bool InitGameServer( uint unIP, ushort usGamePort, ushort usQueryPort, uint unFlags, AppId_t nGameAppId, string pchVersionString )
		{
			return InitGameServerDelegatePointer( Self, unIP, usGamePort, usQueryPort, unFlags, nGameAppId, pchVersionString );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		public delegate void SetProductDelegate( IntPtr self, string pszProduct );
		private SetProductDelegate SetProductDelegatePointer;
		
		#endregion
		public void SetProduct( string pszProduct )
		{
			SetProductDelegatePointer( Self, pszProduct );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		public delegate void SetGameDescriptionDelegate( IntPtr self, string pszGameDescription );
		private SetGameDescriptionDelegate SetGameDescriptionDelegatePointer;
		
		#endregion
		public void SetGameDescription( string pszGameDescription )
		{
			SetGameDescriptionDelegatePointer( Self, pszGameDescription );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		public delegate void SetModDirDelegate( IntPtr self, string pszModDir );
		private SetModDirDelegate SetModDirDelegatePointer;
		
		#endregion
		public void SetModDir( string pszModDir )
		{
			SetModDirDelegatePointer( Self, pszModDir );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		public delegate void SetDedicatedServerDelegate( IntPtr self, [MarshalAs( UnmanagedType.U1 )] bool bDedicated );
		private SetDedicatedServerDelegate SetDedicatedServerDelegatePointer;
		
		#endregion
		public void SetDedicatedServer( [MarshalAs( UnmanagedType.U1 )] bool bDedicated )
		{
			SetDedicatedServerDelegatePointer( Self, bDedicated );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		public delegate void LogOnDelegate( IntPtr self, string pszToken );
		private LogOnDelegate LogOnDelegatePointer;
		
		#endregion
		public void LogOn( string pszToken )
		{
			LogOnDelegatePointer( Self, pszToken );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		public delegate void LogOnAnonymousDelegate( IntPtr self );
		private LogOnAnonymousDelegate LogOnAnonymousDelegatePointer;
		
		#endregion
		public void LogOnAnonymous()
		{
			LogOnAnonymousDelegatePointer( Self );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		public delegate void LogOffDelegate( IntPtr self );
		private LogOffDelegate LogOffDelegatePointer;
		
		#endregion
		public void LogOff()
		{
			LogOffDelegatePointer( Self );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		public delegate bool BLoggedOnDelegate( IntPtr self );
		private BLoggedOnDelegate BLoggedOnDelegatePointer;
		
		#endregion
		public bool BLoggedOn()
		{
			return BLoggedOnDelegatePointer( Self );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		public delegate bool BSecureDelegate( IntPtr self );
		private BSecureDelegate BSecureDelegatePointer;
		
		#endregion
		public bool BSecure()
		{
			return BSecureDelegatePointer( Self );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		public delegate void GetSteamIDDelegate( IntPtr self, ref CSteamID retVal );
		private GetSteamIDDelegate GetSteamIDDelegatePointer;
		
		#endregion
		public CSteamID GetSteamID()
		{
			var retVal = default( CSteamID );
			GetSteamIDDelegatePointer( Self, ref retVal );
			return retVal;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		public delegate bool WasRestartRequestedDelegate( IntPtr self );
		private WasRestartRequestedDelegate WasRestartRequestedDelegatePointer;
		
		#endregion
		public bool WasRestartRequested()
		{
			return WasRestartRequestedDelegatePointer( Self );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		public delegate void SetMaxPlayerCountDelegate( IntPtr self, int cPlayersMax );
		private SetMaxPlayerCountDelegate SetMaxPlayerCountDelegatePointer;
		
		#endregion
		public void SetMaxPlayerCount( int cPlayersMax )
		{
			SetMaxPlayerCountDelegatePointer( Self, cPlayersMax );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		public delegate void SetBotPlayerCountDelegate( IntPtr self, int cBotplayers );
		private SetBotPlayerCountDelegate SetBotPlayerCountDelegatePointer;
		
		#endregion
		public void SetBotPlayerCount( int cBotplayers )
		{
			SetBotPlayerCountDelegatePointer( Self, cBotplayers );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		public delegate void SetServerNameDelegate( IntPtr self, string pszServerName );
		private SetServerNameDelegate SetServerNameDelegatePointer;
		
		#endregion
		public void SetServerName( string pszServerName )
		{
			SetServerNameDelegatePointer( Self, pszServerName );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		public delegate void SetMapNameDelegate( IntPtr self, string pszMapName );
		private SetMapNameDelegate SetMapNameDelegatePointer;
		
		#endregion
		public void SetMapName( string pszMapName )
		{
			SetMapNameDelegatePointer( Self, pszMapName );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		public delegate void SetPasswordProtectedDelegate( IntPtr self, [MarshalAs( UnmanagedType.U1 )] bool bPasswordProtected );
		private SetPasswordProtectedDelegate SetPasswordProtectedDelegatePointer;
		
		#endregion
		public void SetPasswordProtected( [MarshalAs( UnmanagedType.U1 )] bool bPasswordProtected )
		{
			SetPasswordProtectedDelegatePointer( Self, bPasswordProtected );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		public delegate void SetSpectatorPortDelegate( IntPtr self, ushort unSpectatorPort );
		private SetSpectatorPortDelegate SetSpectatorPortDelegatePointer;
		
		#endregion
		public void SetSpectatorPort( ushort unSpectatorPort )
		{
			SetSpectatorPortDelegatePointer( Self, unSpectatorPort );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		public delegate void SetSpectatorServerNameDelegate( IntPtr self, string pszSpectatorServerName );
		private SetSpectatorServerNameDelegate SetSpectatorServerNameDelegatePointer;
		
		#endregion
		public void SetSpectatorServerName( string pszSpectatorServerName )
		{
			SetSpectatorServerNameDelegatePointer( Self, pszSpectatorServerName );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		public delegate void ClearAllKeyValuesDelegate( IntPtr self );
		private ClearAllKeyValuesDelegate ClearAllKeyValuesDelegatePointer;
		
		#endregion
		public void ClearAllKeyValues()
		{
			ClearAllKeyValuesDelegatePointer( Self );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		public delegate void SetKeyValueDelegate( IntPtr self, string pKey, string pValue );
		private SetKeyValueDelegate SetKeyValueDelegatePointer;
		
		#endregion
		public void SetKeyValue( string pKey, string pValue )
		{
			SetKeyValueDelegatePointer( Self, pKey, pValue );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		public delegate void SetGameTagsDelegate( IntPtr self, string pchGameTags );
		private SetGameTagsDelegate SetGameTagsDelegatePointer;
		
		#endregion
		public void SetGameTags( string pchGameTags )
		{
			SetGameTagsDelegatePointer( Self, pchGameTags );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		public delegate void SetGameDataDelegate( IntPtr self, string pchGameData );
		private SetGameDataDelegate SetGameDataDelegatePointer;
		
		#endregion
		public void SetGameData( string pchGameData )
		{
			SetGameDataDelegatePointer( Self, pchGameData );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		public delegate void SetRegionDelegate( IntPtr self, string pszRegion );
		private SetRegionDelegate SetRegionDelegatePointer;
		
		#endregion
		public void SetRegion( string pszRegion )
		{
			SetRegionDelegatePointer( Self, pszRegion );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		public delegate bool SendUserConnectAndAuthenticateDelegate( IntPtr self, uint unIPClient, IntPtr pvAuthBlob, uint cubAuthBlobSize, ref CSteamID pSteamIDUser );
		private SendUserConnectAndAuthenticateDelegate SendUserConnectAndAuthenticateDelegatePointer;
		
		#endregion
		public bool SendUserConnectAndAuthenticate( uint unIPClient, IntPtr pvAuthBlob, uint cubAuthBlobSize, ref CSteamID pSteamIDUser )
		{
			return SendUserConnectAndAuthenticateDelegatePointer( Self, unIPClient, pvAuthBlob, cubAuthBlobSize, ref pSteamIDUser );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		public delegate void CreateUnauthenticatedUserConnectionDelegate( IntPtr self, ref CSteamID retVal );
		private CreateUnauthenticatedUserConnectionDelegate CreateUnauthenticatedUserConnectionDelegatePointer;
		
		#endregion
		public CSteamID CreateUnauthenticatedUserConnection()
		{
			var retVal = default( CSteamID );
			CreateUnauthenticatedUserConnectionDelegatePointer( Self, ref retVal );
			return retVal;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		public delegate void SendUserDisconnectDelegate( IntPtr self, CSteamID steamIDUser );
		private SendUserDisconnectDelegate SendUserDisconnectDelegatePointer;
		
		#endregion
		public void SendUserDisconnect( CSteamID steamIDUser )
		{
			SendUserDisconnectDelegatePointer( Self, steamIDUser );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		public delegate bool BUpdateUserDataDelegate( IntPtr self, CSteamID steamIDUser, string pchPlayerName, uint uScore );
		private BUpdateUserDataDelegate BUpdateUserDataDelegatePointer;
		
		#endregion
		public bool BUpdateUserData( CSteamID steamIDUser, string pchPlayerName, uint uScore )
		{
			return BUpdateUserDataDelegatePointer( Self, steamIDUser, pchPlayerName, uScore );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		public delegate HAuthTicket GetAuthSessionTicketDelegate( IntPtr self, IntPtr pTicket, int cbMaxTicket, ref uint pcbTicket );
		private GetAuthSessionTicketDelegate GetAuthSessionTicketDelegatePointer;
		
		#endregion
		public HAuthTicket GetAuthSessionTicket( IntPtr pTicket, int cbMaxTicket, ref uint pcbTicket )
		{
			return GetAuthSessionTicketDelegatePointer( Self, pTicket, cbMaxTicket, ref pcbTicket );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		public delegate BeginAuthSessionResult BeginAuthSessionDelegate( IntPtr self, IntPtr pAuthTicket, int cbAuthTicket, CSteamID steamID );
		private BeginAuthSessionDelegate BeginAuthSessionDelegatePointer;
		
		#endregion
		public BeginAuthSessionResult BeginAuthSession( IntPtr pAuthTicket, int cbAuthTicket, CSteamID steamID )
		{
			return BeginAuthSessionDelegatePointer( Self, pAuthTicket, cbAuthTicket, steamID );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		public delegate void EndAuthSessionDelegate( IntPtr self, CSteamID steamID );
		private EndAuthSessionDelegate EndAuthSessionDelegatePointer;
		
		#endregion
		public void EndAuthSession( CSteamID steamID )
		{
			EndAuthSessionDelegatePointer( Self, steamID );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		public delegate void CancelAuthTicketDelegate( IntPtr self, HAuthTicket hAuthTicket );
		private CancelAuthTicketDelegate CancelAuthTicketDelegatePointer;
		
		#endregion
		public void CancelAuthTicket( HAuthTicket hAuthTicket )
		{
			CancelAuthTicketDelegatePointer( Self, hAuthTicket );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		public delegate UserHasLicenseForAppResult UserHasLicenseForAppDelegate( IntPtr self, CSteamID steamID, AppId_t appID );
		private UserHasLicenseForAppDelegate UserHasLicenseForAppDelegatePointer;
		
		#endregion
		public UserHasLicenseForAppResult UserHasLicenseForApp( CSteamID steamID, AppId_t appID )
		{
			return UserHasLicenseForAppDelegatePointer( Self, steamID, appID );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		public delegate bool RequestUserGroupStatusDelegate( IntPtr self, CSteamID steamIDUser, CSteamID steamIDGroup );
		private RequestUserGroupStatusDelegate RequestUserGroupStatusDelegatePointer;
		
		#endregion
		public bool RequestUserGroupStatus( CSteamID steamIDUser, CSteamID steamIDGroup )
		{
			return RequestUserGroupStatusDelegatePointer( Self, steamIDUser, steamIDGroup );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		public delegate void GetGameplayStatsDelegate( IntPtr self );
		private GetGameplayStatsDelegate GetGameplayStatsDelegatePointer;
		
		#endregion
		public void GetGameplayStats()
		{
			GetGameplayStatsDelegatePointer( Self );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		public delegate SteamAPICall_t GetServerReputationDelegate( IntPtr self );
		private GetServerReputationDelegate GetServerReputationDelegatePointer;
		
		#endregion
		public async Task<GSReputation_t?> GetServerReputation()
		{
			return await (new Result<GSReputation_t>( GetServerReputationDelegatePointer( Self ) )).GetResult();
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		public delegate uint GetPublicIPDelegate( IntPtr self );
		private GetPublicIPDelegate GetPublicIPDelegatePointer;
		
		#endregion
		public uint GetPublicIP()
		{
			return GetPublicIPDelegatePointer( Self );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		public delegate bool HandleIncomingPacketDelegate( IntPtr self, IntPtr pData, int cbData, uint srcIP, ushort srcPort );
		private HandleIncomingPacketDelegate HandleIncomingPacketDelegatePointer;
		
		#endregion
		public bool HandleIncomingPacket( IntPtr pData, int cbData, uint srcIP, ushort srcPort )
		{
			return HandleIncomingPacketDelegatePointer( Self, pData, cbData, srcIP, srcPort );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		public delegate int GetNextOutgoingPacketDelegate( IntPtr self, IntPtr pOut, int cbMaxOut, ref uint pNetAdr, ref ushort pPort );
		private GetNextOutgoingPacketDelegate GetNextOutgoingPacketDelegatePointer;
		
		#endregion
		public int GetNextOutgoingPacket( IntPtr pOut, int cbMaxOut, ref uint pNetAdr, ref ushort pPort )
		{
			return GetNextOutgoingPacketDelegatePointer( Self, pOut, cbMaxOut, ref pNetAdr, ref pPort );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		public delegate void EnableHeartbeatsDelegate( IntPtr self, [MarshalAs( UnmanagedType.U1 )] bool bActive );
		private EnableHeartbeatsDelegate EnableHeartbeatsDelegatePointer;
		
		#endregion
		public void EnableHeartbeats( [MarshalAs( UnmanagedType.U1 )] bool bActive )
		{
			EnableHeartbeatsDelegatePointer( Self, bActive );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		public delegate void SetHeartbeatIntervalDelegate( IntPtr self, int iHeartbeatInterval );
		private SetHeartbeatIntervalDelegate SetHeartbeatIntervalDelegatePointer;
		
		#endregion
		public void SetHeartbeatInterval( int iHeartbeatInterval )
		{
			SetHeartbeatIntervalDelegatePointer( Self, iHeartbeatInterval );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		public delegate void ForceHeartbeatDelegate( IntPtr self );
		private ForceHeartbeatDelegate ForceHeartbeatDelegatePointer;
		
		#endregion
		public void ForceHeartbeat()
		{
			ForceHeartbeatDelegatePointer( Self );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		public delegate SteamAPICall_t AssociateWithClanDelegate( IntPtr self, CSteamID steamIDClan );
		private AssociateWithClanDelegate AssociateWithClanDelegatePointer;
		
		#endregion
		public async Task<AssociateWithClanResult_t?> AssociateWithClan( CSteamID steamIDClan )
		{
			return await (new Result<AssociateWithClanResult_t>( AssociateWithClanDelegatePointer( Self, steamIDClan ) )).GetResult();
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		public delegate SteamAPICall_t ComputeNewPlayerCompatibilityDelegate( IntPtr self, CSteamID steamIDNewPlayer );
		private ComputeNewPlayerCompatibilityDelegate ComputeNewPlayerCompatibilityDelegatePointer;
		
		#endregion
		public async Task<ComputeNewPlayerCompatibilityResult_t?> ComputeNewPlayerCompatibility( CSteamID steamIDNewPlayer )
		{
			return await (new Result<ComputeNewPlayerCompatibilityResult_t>( ComputeNewPlayerCompatibilityDelegatePointer( Self, steamIDNewPlayer ) )).GetResult();
		}
		
	}
}
