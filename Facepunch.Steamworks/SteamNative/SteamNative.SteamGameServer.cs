using System;
using System.Runtime.InteropServices;

namespace SteamNative
{
	internal unsafe class SteamGameServer : IDisposable
	{
		//
		// Holds a platform specific implentation
		//
		internal Platform.Interface platform;
		internal Facepunch.Steamworks.BaseSteamworks steamworks;
		
		//
		// Constructor decides which implementation to use based on current platform
		//
		public SteamGameServer( Facepunch.Steamworks.BaseSteamworks steamworks, IntPtr pointer )
		{
			this.steamworks = steamworks;
			
			if ( Platform.IsWindows64 ) platform = new Platform.Win64( pointer );
			else if ( Platform.IsWindows32 ) platform = new Platform.Win32( pointer );
			else if ( Platform.IsLinux32 ) platform = new Platform.Linux32( pointer );
			else if ( Platform.IsLinux64 ) platform = new Platform.Linux64( pointer );
			else if ( Platform.IsOsx ) platform = new Platform.Mac( pointer );
		}
		
		//
		// Class is invalid if we don't have a valid implementation
		//
		public bool IsValid{ get{ return platform != null && platform.IsValid; } }
		
		//
		// When shutting down clear all the internals to avoid accidental use
		//
		public virtual void Dispose()
		{
			 if ( platform != null )
			{
				platform.Dispose();
				platform = null;
			}
		}
		
		// SteamAPICall_t
		public CallbackHandle AssociateWithClan( CSteamID steamIDClan /*class CSteamID*/, Action<AssociateWithClanResult_t, bool> CallbackFunction = null /*Action<AssociateWithClanResult_t, bool>*/ )
		{
			SteamAPICall_t callback = 0;
			callback = platform.ISteamGameServer_AssociateWithClan( steamIDClan.Value );
			
			if ( CallbackFunction == null ) return null;
			
			return AssociateWithClanResult_t.CallResult( steamworks, callback, CallbackFunction );
		}
		
		// BeginAuthSessionResult
		public BeginAuthSessionResult BeginAuthSession( IntPtr pAuthTicket /*const void **/, int cbAuthTicket /*int*/, CSteamID steamID /*class CSteamID*/ )
		{
			return platform.ISteamGameServer_BeginAuthSession( (IntPtr) pAuthTicket, cbAuthTicket, steamID.Value );
		}
		
		// bool
		public bool BLoggedOn()
		{
			return platform.ISteamGameServer_BLoggedOn();
		}
		
		// bool
		public bool BSecure()
		{
			return platform.ISteamGameServer_BSecure();
		}
		
		// bool
		public bool BUpdateUserData( CSteamID steamIDUser /*class CSteamID*/, string pchPlayerName /*const char **/, uint uScore /*uint32*/ )
		{
			return platform.ISteamGameServer_BUpdateUserData( steamIDUser.Value, pchPlayerName, uScore );
		}
		
		// void
		public void CancelAuthTicket( HAuthTicket hAuthTicket /*HAuthTicket*/ )
		{
			platform.ISteamGameServer_CancelAuthTicket( hAuthTicket.Value );
		}
		
		// void
		public void ClearAllKeyValues()
		{
			platform.ISteamGameServer_ClearAllKeyValues();
		}
		
		// SteamAPICall_t
		public CallbackHandle ComputeNewPlayerCompatibility( CSteamID steamIDNewPlayer /*class CSteamID*/, Action<ComputeNewPlayerCompatibilityResult_t, bool> CallbackFunction = null /*Action<ComputeNewPlayerCompatibilityResult_t, bool>*/ )
		{
			SteamAPICall_t callback = 0;
			callback = platform.ISteamGameServer_ComputeNewPlayerCompatibility( steamIDNewPlayer.Value );
			
			if ( CallbackFunction == null ) return null;
			
			return ComputeNewPlayerCompatibilityResult_t.CallResult( steamworks, callback, CallbackFunction );
		}
		
		// ulong
		public ulong CreateUnauthenticatedUserConnection()
		{
			return platform.ISteamGameServer_CreateUnauthenticatedUserConnection();
		}
		
		// void
		public void EnableHeartbeats( bool bActive /*bool*/ )
		{
			platform.ISteamGameServer_EnableHeartbeats( bActive );
		}
		
		// void
		public void EndAuthSession( CSteamID steamID /*class CSteamID*/ )
		{
			platform.ISteamGameServer_EndAuthSession( steamID.Value );
		}
		
		// void
		public void ForceHeartbeat()
		{
			platform.ISteamGameServer_ForceHeartbeat();
		}
		
		// HAuthTicket
		public HAuthTicket GetAuthSessionTicket( IntPtr pTicket /*void **/, int cbMaxTicket /*int*/, out uint pcbTicket /*uint32 **/ )
		{
			return platform.ISteamGameServer_GetAuthSessionTicket( (IntPtr) pTicket, cbMaxTicket, out pcbTicket );
		}
		
		// void
		public void GetGameplayStats()
		{
			platform.ISteamGameServer_GetGameplayStats();
		}
		
		// int
		public int GetNextOutgoingPacket( IntPtr pOut /*void **/, int cbMaxOut /*int*/, out uint pNetAdr /*uint32 **/, out ushort pPort /*uint16 **/ )
		{
			return platform.ISteamGameServer_GetNextOutgoingPacket( (IntPtr) pOut, cbMaxOut, out pNetAdr, out pPort );
		}
		
		// uint
		public uint GetPublicIP()
		{
			return platform.ISteamGameServer_GetPublicIP();
		}
		
		// SteamAPICall_t
		public CallbackHandle GetServerReputation( Action<GSReputation_t, bool> CallbackFunction = null /*Action<GSReputation_t, bool>*/ )
		{
			SteamAPICall_t callback = 0;
			callback = platform.ISteamGameServer_GetServerReputation();
			
			if ( CallbackFunction == null ) return null;
			
			return GSReputation_t.CallResult( steamworks, callback, CallbackFunction );
		}
		
		// ulong
		public ulong GetSteamID()
		{
			return platform.ISteamGameServer_GetSteamID();
		}
		
		// bool
		public bool HandleIncomingPacket( IntPtr pData /*const void **/, int cbData /*int*/, uint srcIP /*uint32*/, ushort srcPort /*uint16*/ )
		{
			return platform.ISteamGameServer_HandleIncomingPacket( (IntPtr) pData, cbData, srcIP, srcPort );
		}
		
		// bool
		public bool InitGameServer( uint unIP /*uint32*/, ushort usGamePort /*uint16*/, ushort usQueryPort /*uint16*/, uint unFlags /*uint32*/, AppId_t nGameAppId /*AppId_t*/, string pchVersionString /*const char **/ )
		{
			return platform.ISteamGameServer_InitGameServer( unIP, usGamePort, usQueryPort, unFlags, nGameAppId.Value, pchVersionString );
		}
		
		// void
		public void LogOff()
		{
			platform.ISteamGameServer_LogOff();
		}
		
		// void
		public void LogOn( string pszToken /*const char **/ )
		{
			platform.ISteamGameServer_LogOn( pszToken );
		}
		
		// void
		public void LogOnAnonymous()
		{
			platform.ISteamGameServer_LogOnAnonymous();
		}
		
		// bool
		public bool RequestUserGroupStatus( CSteamID steamIDUser /*class CSteamID*/, CSteamID steamIDGroup /*class CSteamID*/ )
		{
			return platform.ISteamGameServer_RequestUserGroupStatus( steamIDUser.Value, steamIDGroup.Value );
		}
		
		// bool
		public bool SendUserConnectAndAuthenticate( uint unIPClient /*uint32*/, IntPtr pvAuthBlob /*const void **/, uint cubAuthBlobSize /*uint32*/, out CSteamID pSteamIDUser /*class CSteamID **/ )
		{
			return platform.ISteamGameServer_SendUserConnectAndAuthenticate( unIPClient, (IntPtr) pvAuthBlob, cubAuthBlobSize, out pSteamIDUser.Value );
		}
		
		// void
		public void SendUserDisconnect( CSteamID steamIDUser /*class CSteamID*/ )
		{
			platform.ISteamGameServer_SendUserDisconnect( steamIDUser.Value );
		}
		
		// void
		public void SetBotPlayerCount( int cBotplayers /*int*/ )
		{
			platform.ISteamGameServer_SetBotPlayerCount( cBotplayers );
		}
		
		// void
		public void SetDedicatedServer( bool bDedicated /*bool*/ )
		{
			platform.ISteamGameServer_SetDedicatedServer( bDedicated );
		}
		
		// void
		public void SetGameData( string pchGameData /*const char **/ )
		{
			platform.ISteamGameServer_SetGameData( pchGameData );
		}
		
		// void
		public void SetGameDescription( string pszGameDescription /*const char **/ )
		{
			platform.ISteamGameServer_SetGameDescription( pszGameDescription );
		}
		
		// void
		public void SetGameTags( string pchGameTags /*const char **/ )
		{
			platform.ISteamGameServer_SetGameTags( pchGameTags );
		}
		
		// void
		public void SetHeartbeatInterval( int iHeartbeatInterval /*int*/ )
		{
			platform.ISteamGameServer_SetHeartbeatInterval( iHeartbeatInterval );
		}
		
		// void
		public void SetKeyValue( string pKey /*const char **/, string pValue /*const char **/ )
		{
			platform.ISteamGameServer_SetKeyValue( pKey, pValue );
		}
		
		// void
		public void SetMapName( string pszMapName /*const char **/ )
		{
			platform.ISteamGameServer_SetMapName( pszMapName );
		}
		
		// void
		public void SetMaxPlayerCount( int cPlayersMax /*int*/ )
		{
			platform.ISteamGameServer_SetMaxPlayerCount( cPlayersMax );
		}
		
		// void
		public void SetModDir( string pszModDir /*const char **/ )
		{
			platform.ISteamGameServer_SetModDir( pszModDir );
		}
		
		// void
		public void SetPasswordProtected( bool bPasswordProtected /*bool*/ )
		{
			platform.ISteamGameServer_SetPasswordProtected( bPasswordProtected );
		}
		
		// void
		public void SetProduct( string pszProduct /*const char **/ )
		{
			platform.ISteamGameServer_SetProduct( pszProduct );
		}
		
		// void
		public void SetRegion( string pszRegion /*const char **/ )
		{
			platform.ISteamGameServer_SetRegion( pszRegion );
		}
		
		// void
		public void SetServerName( string pszServerName /*const char **/ )
		{
			platform.ISteamGameServer_SetServerName( pszServerName );
		}
		
		// void
		public void SetSpectatorPort( ushort unSpectatorPort /*uint16*/ )
		{
			platform.ISteamGameServer_SetSpectatorPort( unSpectatorPort );
		}
		
		// void
		public void SetSpectatorServerName( string pszSpectatorServerName /*const char **/ )
		{
			platform.ISteamGameServer_SetSpectatorServerName( pszSpectatorServerName );
		}
		
		// UserHasLicenseForAppResult
		public UserHasLicenseForAppResult UserHasLicenseForApp( CSteamID steamID /*class CSteamID*/, AppId_t appID /*AppId_t*/ )
		{
			return platform.ISteamGameServer_UserHasLicenseForApp( steamID.Value, appID.Value );
		}
		
		// bool
		public bool WasRestartRequested()
		{
			return platform.ISteamGameServer_WasRestartRequested();
		}
		
	}
}
