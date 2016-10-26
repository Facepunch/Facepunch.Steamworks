using System;
using System.Runtime.InteropServices;

namespace SteamNative
{
	public unsafe class SteamGameServer : IDisposable
	{
		//
		// Holds a platform specific implentation
		//
		internal Platform.Interface _pi;
		
		//
		// Constructor decides which implementation to use based on current platform
		//
		public SteamGameServer( IntPtr pointer )
		{
			if ( Platform.IsWindows64 ) _pi = new Platform.Win64( pointer );
			else if ( Platform.IsWindows32 ) _pi = new Platform.Win32( pointer );
			else if ( Platform.IsLinux32 ) _pi = new Platform.Linux32( pointer );
			else if ( Platform.IsLinux64 ) _pi = new Platform.Linux64( pointer );
			else if ( Platform.IsOsx ) _pi = new Platform.Mac( pointer );
		}
		
		//
		// Class is invalid if we don't have a valid implementation
		//
		public bool IsValid{ get{ return _pi != null && _pi.IsValid; } }
		
		//
		// When shutting down clear all the internals to avoid accidental use
		//
		public virtual void Dispose()
		{
			 if ( _pi != null )
			{
				_pi.Dispose();
				_pi = null;
			}
		}
		
		// SteamAPICall_t
		public SteamAPICall_t AssociateWithClan( CSteamID steamIDClan /*class CSteamID*/ )
		{
			return _pi.ISteamGameServer_AssociateWithClan( steamIDClan.Value /*C*/ );
		}
		
		// BeginAuthSessionResult
		public BeginAuthSessionResult BeginAuthSession( IntPtr pAuthTicket /*const void **/, int cbAuthTicket /*int*/, CSteamID steamID /*class CSteamID*/ )
		{
			return _pi.ISteamGameServer_BeginAuthSession( (IntPtr) pAuthTicket /*C*/, cbAuthTicket /*C*/, steamID.Value /*C*/ );
		}
		
		// bool
		public bool BLoggedOn()
		{
			return _pi.ISteamGameServer_BLoggedOn();
		}
		
		// bool
		public bool BSecure()
		{
			return _pi.ISteamGameServer_BSecure();
		}
		
		// bool
		public bool BUpdateUserData( CSteamID steamIDUser /*class CSteamID*/, string pchPlayerName /*const char **/, uint uScore /*uint32*/ )
		{
			return _pi.ISteamGameServer_BUpdateUserData( steamIDUser.Value /*C*/, pchPlayerName /*C*/, uScore /*C*/ );
		}
		
		// void
		public void CancelAuthTicket( HAuthTicket hAuthTicket /*HAuthTicket*/ )
		{
			_pi.ISteamGameServer_CancelAuthTicket( hAuthTicket.Value /*C*/ );
		}
		
		// void
		public void ClearAllKeyValues()
		{
			_pi.ISteamGameServer_ClearAllKeyValues();
		}
		
		// SteamAPICall_t
		public SteamAPICall_t ComputeNewPlayerCompatibility( CSteamID steamIDNewPlayer /*class CSteamID*/ )
		{
			return _pi.ISteamGameServer_ComputeNewPlayerCompatibility( steamIDNewPlayer.Value /*C*/ );
		}
		
		// ulong
		public ulong CreateUnauthenticatedUserConnection()
		{
			return _pi.ISteamGameServer_CreateUnauthenticatedUserConnection();
		}
		
		// void
		public void EnableHeartbeats( bool bActive /*bool*/ )
		{
			_pi.ISteamGameServer_EnableHeartbeats( bActive /*C*/ );
		}
		
		// void
		public void EndAuthSession( CSteamID steamID /*class CSteamID*/ )
		{
			_pi.ISteamGameServer_EndAuthSession( steamID.Value /*C*/ );
		}
		
		// void
		public void ForceHeartbeat()
		{
			_pi.ISteamGameServer_ForceHeartbeat();
		}
		
		// HAuthTicket
		public HAuthTicket GetAuthSessionTicket( IntPtr pTicket /*void **/, int cbMaxTicket /*int*/, out uint pcbTicket /*uint32 **/ )
		{
			return _pi.ISteamGameServer_GetAuthSessionTicket( (IntPtr) pTicket /*C*/, cbMaxTicket /*C*/, out pcbTicket /*B*/ );
		}
		
		// void
		public void GetGameplayStats()
		{
			_pi.ISteamGameServer_GetGameplayStats();
		}
		
		// int
		public int GetNextOutgoingPacket( IntPtr pOut /*void **/, int cbMaxOut /*int*/, out uint pNetAdr /*uint32 **/, out ushort pPort /*uint16 **/ )
		{
			return _pi.ISteamGameServer_GetNextOutgoingPacket( (IntPtr) pOut /*C*/, cbMaxOut /*C*/, out pNetAdr /*B*/, out pPort /*B*/ );
		}
		
		// uint
		public uint GetPublicIP()
		{
			return _pi.ISteamGameServer_GetPublicIP();
		}
		
		// SteamAPICall_t
		public SteamAPICall_t GetServerReputation()
		{
			return _pi.ISteamGameServer_GetServerReputation();
		}
		
		// ulong
		public ulong GetSteamID()
		{
			return _pi.ISteamGameServer_GetSteamID();
		}
		
		// bool
		public bool HandleIncomingPacket( IntPtr pData /*const void **/, int cbData /*int*/, uint srcIP /*uint32*/, ushort srcPort /*uint16*/ )
		{
			return _pi.ISteamGameServer_HandleIncomingPacket( (IntPtr) pData /*C*/, cbData /*C*/, srcIP /*C*/, srcPort /*C*/ );
		}
		
		// bool
		public bool InitGameServer( uint unIP /*uint32*/, ushort usGamePort /*uint16*/, ushort usQueryPort /*uint16*/, uint unFlags /*uint32*/, AppId_t nGameAppId /*AppId_t*/, string pchVersionString /*const char **/ )
		{
			return _pi.ISteamGameServer_InitGameServer( unIP /*C*/, usGamePort /*C*/, usQueryPort /*C*/, unFlags /*C*/, nGameAppId.Value /*C*/, pchVersionString /*C*/ );
		}
		
		// void
		public void LogOff()
		{
			_pi.ISteamGameServer_LogOff();
		}
		
		// void
		public void LogOn( string pszToken /*const char **/ )
		{
			_pi.ISteamGameServer_LogOn( pszToken /*C*/ );
		}
		
		// void
		public void LogOnAnonymous()
		{
			_pi.ISteamGameServer_LogOnAnonymous();
		}
		
		// bool
		public bool RequestUserGroupStatus( CSteamID steamIDUser /*class CSteamID*/, CSteamID steamIDGroup /*class CSteamID*/ )
		{
			return _pi.ISteamGameServer_RequestUserGroupStatus( steamIDUser.Value /*C*/, steamIDGroup.Value /*C*/ );
		}
		
		// bool
		public bool SendUserConnectAndAuthenticate( uint unIPClient /*uint32*/, IntPtr pvAuthBlob /*const void **/, uint cubAuthBlobSize /*uint32*/, out CSteamID pSteamIDUser /*class CSteamID **/ )
		{
			return _pi.ISteamGameServer_SendUserConnectAndAuthenticate( unIPClient /*C*/, (IntPtr) pvAuthBlob /*C*/, cubAuthBlobSize /*C*/, out pSteamIDUser.Value /*B*/ );
		}
		
		// void
		public void SendUserDisconnect( CSteamID steamIDUser /*class CSteamID*/ )
		{
			_pi.ISteamGameServer_SendUserDisconnect( steamIDUser.Value /*C*/ );
		}
		
		// void
		public void SetBotPlayerCount( int cBotplayers /*int*/ )
		{
			_pi.ISteamGameServer_SetBotPlayerCount( cBotplayers /*C*/ );
		}
		
		// void
		public void SetDedicatedServer( bool bDedicated /*bool*/ )
		{
			_pi.ISteamGameServer_SetDedicatedServer( bDedicated /*C*/ );
		}
		
		// void
		public void SetGameData( string pchGameData /*const char **/ )
		{
			_pi.ISteamGameServer_SetGameData( pchGameData /*C*/ );
		}
		
		// void
		public void SetGameDescription( string pszGameDescription /*const char **/ )
		{
			_pi.ISteamGameServer_SetGameDescription( pszGameDescription /*C*/ );
		}
		
		// void
		public void SetGameTags( string pchGameTags /*const char **/ )
		{
			_pi.ISteamGameServer_SetGameTags( pchGameTags /*C*/ );
		}
		
		// void
		public void SetHeartbeatInterval( int iHeartbeatInterval /*int*/ )
		{
			_pi.ISteamGameServer_SetHeartbeatInterval( iHeartbeatInterval /*C*/ );
		}
		
		// void
		public void SetKeyValue( string pKey /*const char **/, string pValue /*const char **/ )
		{
			_pi.ISteamGameServer_SetKeyValue( pKey /*C*/, pValue /*C*/ );
		}
		
		// void
		public void SetMapName( string pszMapName /*const char **/ )
		{
			_pi.ISteamGameServer_SetMapName( pszMapName /*C*/ );
		}
		
		// void
		public void SetMaxPlayerCount( int cPlayersMax /*int*/ )
		{
			_pi.ISteamGameServer_SetMaxPlayerCount( cPlayersMax /*C*/ );
		}
		
		// void
		public void SetModDir( string pszModDir /*const char **/ )
		{
			_pi.ISteamGameServer_SetModDir( pszModDir /*C*/ );
		}
		
		// void
		public void SetPasswordProtected( bool bPasswordProtected /*bool*/ )
		{
			_pi.ISteamGameServer_SetPasswordProtected( bPasswordProtected /*C*/ );
		}
		
		// void
		public void SetProduct( string pszProduct /*const char **/ )
		{
			_pi.ISteamGameServer_SetProduct( pszProduct /*C*/ );
		}
		
		// void
		public void SetRegion( string pszRegion /*const char **/ )
		{
			_pi.ISteamGameServer_SetRegion( pszRegion /*C*/ );
		}
		
		// void
		public void SetServerName( string pszServerName /*const char **/ )
		{
			_pi.ISteamGameServer_SetServerName( pszServerName /*C*/ );
		}
		
		// void
		public void SetSpectatorPort( ushort unSpectatorPort /*uint16*/ )
		{
			_pi.ISteamGameServer_SetSpectatorPort( unSpectatorPort /*C*/ );
		}
		
		// void
		public void SetSpectatorServerName( string pszSpectatorServerName /*const char **/ )
		{
			_pi.ISteamGameServer_SetSpectatorServerName( pszSpectatorServerName /*C*/ );
		}
		
		// UserHasLicenseForAppResult
		public UserHasLicenseForAppResult UserHasLicenseForApp( CSteamID steamID /*class CSteamID*/, AppId_t appID /*AppId_t*/ )
		{
			return _pi.ISteamGameServer_UserHasLicenseForApp( steamID.Value /*C*/, appID.Value /*C*/ );
		}
		
		// bool
		public bool WasRestartRequested()
		{
			return _pi.ISteamGameServer_WasRestartRequested();
		}
		
	}
}
