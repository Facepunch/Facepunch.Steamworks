using System;
using System.Runtime.InteropServices;

namespace SteamNative
{
	public unsafe class SteamGameServer
	{
		internal IntPtr _ptr;
		
		public SteamGameServer( IntPtr pointer )
		{
			_ptr = pointer;
		}
		
		
		// SteamAPICall_t
		public SteamAPICall_t AssociateWithClan( CSteamID steamIDClan /*class CSteamID*/ )
		{
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamGameServer.AssociateWithClan( _ptr, steamIDClan );
			else return Platform.Win64.ISteamGameServer.AssociateWithClan( _ptr, steamIDClan );
		}
		
		// BeginAuthSessionResult
		public BeginAuthSessionResult BeginAuthSession( IntPtr pAuthTicket /*const void **/, int cbAuthTicket /*int*/, CSteamID steamID /*class CSteamID*/ )
		{
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamGameServer.BeginAuthSession( _ptr, (IntPtr) pAuthTicket, cbAuthTicket, steamID );
			else return Platform.Win64.ISteamGameServer.BeginAuthSession( _ptr, (IntPtr) pAuthTicket, cbAuthTicket, steamID );
		}
		
		// bool
		public bool BLoggedOn()
		{
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamGameServer.BLoggedOn( _ptr );
			else return Platform.Win64.ISteamGameServer.BLoggedOn( _ptr );
		}
		
		// bool
		public bool BSecure()
		{
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamGameServer.BSecure( _ptr );
			else return Platform.Win64.ISteamGameServer.BSecure( _ptr );
		}
		
		// bool
		public bool BUpdateUserData( CSteamID steamIDUser /*class CSteamID*/, string pchPlayerName /*const char **/, uint uScore /*uint32*/ )
		{
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamGameServer.BUpdateUserData( _ptr, steamIDUser, pchPlayerName, uScore );
			else return Platform.Win64.ISteamGameServer.BUpdateUserData( _ptr, steamIDUser, pchPlayerName, uScore );
		}
		
		// void
		public void CancelAuthTicket( HAuthTicket hAuthTicket /*HAuthTicket*/ )
		{
			if ( Platform.IsWindows32 ) Platform.Win32.ISteamGameServer.CancelAuthTicket( _ptr, hAuthTicket );
			else Platform.Win64.ISteamGameServer.CancelAuthTicket( _ptr, hAuthTicket );
		}
		
		// void
		public void ClearAllKeyValues()
		{
			if ( Platform.IsWindows32 ) Platform.Win32.ISteamGameServer.ClearAllKeyValues( _ptr );
			else Platform.Win64.ISteamGameServer.ClearAllKeyValues( _ptr );
		}
		
		// SteamAPICall_t
		public SteamAPICall_t ComputeNewPlayerCompatibility( CSteamID steamIDNewPlayer /*class CSteamID*/ )
		{
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamGameServer.ComputeNewPlayerCompatibility( _ptr, steamIDNewPlayer );
			else return Platform.Win64.ISteamGameServer.ComputeNewPlayerCompatibility( _ptr, steamIDNewPlayer );
		}
		
		// ulong
		public ulong CreateUnauthenticatedUserConnection()
		{
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamGameServer.CreateUnauthenticatedUserConnection( _ptr );
			else return Platform.Win64.ISteamGameServer.CreateUnauthenticatedUserConnection( _ptr );
		}
		
		// void
		public void EnableHeartbeats( bool bActive /*bool*/ )
		{
			if ( Platform.IsWindows32 ) Platform.Win32.ISteamGameServer.EnableHeartbeats( _ptr, bActive );
			else Platform.Win64.ISteamGameServer.EnableHeartbeats( _ptr, bActive );
		}
		
		// void
		public void EndAuthSession( CSteamID steamID /*class CSteamID*/ )
		{
			if ( Platform.IsWindows32 ) Platform.Win32.ISteamGameServer.EndAuthSession( _ptr, steamID );
			else Platform.Win64.ISteamGameServer.EndAuthSession( _ptr, steamID );
		}
		
		// void
		public void ForceHeartbeat()
		{
			if ( Platform.IsWindows32 ) Platform.Win32.ISteamGameServer.ForceHeartbeat( _ptr );
			else Platform.Win64.ISteamGameServer.ForceHeartbeat( _ptr );
		}
		
		// HAuthTicket
		public HAuthTicket GetAuthSessionTicket( IntPtr pTicket /*void **/, int cbMaxTicket /*int*/, out uint pcbTicket /*uint32 **/ )
		{
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamGameServer.GetAuthSessionTicket( _ptr, (IntPtr) pTicket, cbMaxTicket, out pcbTicket );
			else return Platform.Win64.ISteamGameServer.GetAuthSessionTicket( _ptr, (IntPtr) pTicket, cbMaxTicket, out pcbTicket );
		}
		
		// void
		public void GetGameplayStats()
		{
			if ( Platform.IsWindows32 ) Platform.Win32.ISteamGameServer.GetGameplayStats( _ptr );
			else Platform.Win64.ISteamGameServer.GetGameplayStats( _ptr );
		}
		
		// int
		public int GetNextOutgoingPacket( IntPtr pOut /*void **/, int cbMaxOut /*int*/, out uint pNetAdr /*uint32 **/, out ushort pPort /*uint16 **/ )
		{
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamGameServer.GetNextOutgoingPacket( _ptr, (IntPtr) pOut, cbMaxOut, out pNetAdr, out pPort );
			else return Platform.Win64.ISteamGameServer.GetNextOutgoingPacket( _ptr, (IntPtr) pOut, cbMaxOut, out pNetAdr, out pPort );
		}
		
		// uint
		public uint GetPublicIP()
		{
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamGameServer.GetPublicIP( _ptr );
			else return Platform.Win64.ISteamGameServer.GetPublicIP( _ptr );
		}
		
		// SteamAPICall_t
		public SteamAPICall_t GetServerReputation()
		{
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamGameServer.GetServerReputation( _ptr );
			else return Platform.Win64.ISteamGameServer.GetServerReputation( _ptr );
		}
		
		// ulong
		public ulong GetSteamID()
		{
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamGameServer.GetSteamID( _ptr );
			else return Platform.Win64.ISteamGameServer.GetSteamID( _ptr );
		}
		
		// bool
		public bool HandleIncomingPacket( IntPtr pData /*const void **/, int cbData /*int*/, uint srcIP /*uint32*/, ushort srcPort /*uint16*/ )
		{
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamGameServer.HandleIncomingPacket( _ptr, (IntPtr) pData, cbData, srcIP, srcPort );
			else return Platform.Win64.ISteamGameServer.HandleIncomingPacket( _ptr, (IntPtr) pData, cbData, srcIP, srcPort );
		}
		
		// bool
		public bool InitGameServer( uint unIP /*uint32*/, ushort usGamePort /*uint16*/, ushort usQueryPort /*uint16*/, uint unFlags /*uint32*/, AppId_t nGameAppId /*AppId_t*/, string pchVersionString /*const char **/ )
		{
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamGameServer.InitGameServer( _ptr, unIP, usGamePort, usQueryPort, unFlags, nGameAppId, pchVersionString );
			else return Platform.Win64.ISteamGameServer.InitGameServer( _ptr, unIP, usGamePort, usQueryPort, unFlags, nGameAppId, pchVersionString );
		}
		
		// void
		public void LogOff()
		{
			if ( Platform.IsWindows32 ) Platform.Win32.ISteamGameServer.LogOff( _ptr );
			else Platform.Win64.ISteamGameServer.LogOff( _ptr );
		}
		
		// void
		public void LogOn( string pszToken /*const char **/ )
		{
			if ( Platform.IsWindows32 ) Platform.Win32.ISteamGameServer.LogOn( _ptr, pszToken );
			else Platform.Win64.ISteamGameServer.LogOn( _ptr, pszToken );
		}
		
		// void
		public void LogOnAnonymous()
		{
			if ( Platform.IsWindows32 ) Platform.Win32.ISteamGameServer.LogOnAnonymous( _ptr );
			else Platform.Win64.ISteamGameServer.LogOnAnonymous( _ptr );
		}
		
		// bool
		public bool RequestUserGroupStatus( CSteamID steamIDUser /*class CSteamID*/, CSteamID steamIDGroup /*class CSteamID*/ )
		{
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamGameServer.RequestUserGroupStatus( _ptr, steamIDUser, steamIDGroup );
			else return Platform.Win64.ISteamGameServer.RequestUserGroupStatus( _ptr, steamIDUser, steamIDGroup );
		}
		
		// bool
		public bool SendUserConnectAndAuthenticate( uint unIPClient /*uint32*/, IntPtr pvAuthBlob /*const void **/, uint cubAuthBlobSize /*uint32*/, out CSteamID pSteamIDUser /*class CSteamID **/ )
		{
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamGameServer.SendUserConnectAndAuthenticate( _ptr, unIPClient, (IntPtr) pvAuthBlob, cubAuthBlobSize, out pSteamIDUser );
			else return Platform.Win64.ISteamGameServer.SendUserConnectAndAuthenticate( _ptr, unIPClient, (IntPtr) pvAuthBlob, cubAuthBlobSize, out pSteamIDUser );
		}
		
		// void
		public void SendUserDisconnect( CSteamID steamIDUser /*class CSteamID*/ )
		{
			if ( Platform.IsWindows32 ) Platform.Win32.ISteamGameServer.SendUserDisconnect( _ptr, steamIDUser );
			else Platform.Win64.ISteamGameServer.SendUserDisconnect( _ptr, steamIDUser );
		}
		
		// void
		public void SetBotPlayerCount( int cBotplayers /*int*/ )
		{
			if ( Platform.IsWindows32 ) Platform.Win32.ISteamGameServer.SetBotPlayerCount( _ptr, cBotplayers );
			else Platform.Win64.ISteamGameServer.SetBotPlayerCount( _ptr, cBotplayers );
		}
		
		// void
		public void SetDedicatedServer( bool bDedicated /*bool*/ )
		{
			if ( Platform.IsWindows32 ) Platform.Win32.ISteamGameServer.SetDedicatedServer( _ptr, bDedicated );
			else Platform.Win64.ISteamGameServer.SetDedicatedServer( _ptr, bDedicated );
		}
		
		// void
		public void SetGameData( string pchGameData /*const char **/ )
		{
			if ( Platform.IsWindows32 ) Platform.Win32.ISteamGameServer.SetGameData( _ptr, pchGameData );
			else Platform.Win64.ISteamGameServer.SetGameData( _ptr, pchGameData );
		}
		
		// void
		public void SetGameDescription( string pszGameDescription /*const char **/ )
		{
			if ( Platform.IsWindows32 ) Platform.Win32.ISteamGameServer.SetGameDescription( _ptr, pszGameDescription );
			else Platform.Win64.ISteamGameServer.SetGameDescription( _ptr, pszGameDescription );
		}
		
		// void
		public void SetGameTags( string pchGameTags /*const char **/ )
		{
			if ( Platform.IsWindows32 ) Platform.Win32.ISteamGameServer.SetGameTags( _ptr, pchGameTags );
			else Platform.Win64.ISteamGameServer.SetGameTags( _ptr, pchGameTags );
		}
		
		// void
		public void SetHeartbeatInterval( int iHeartbeatInterval /*int*/ )
		{
			if ( Platform.IsWindows32 ) Platform.Win32.ISteamGameServer.SetHeartbeatInterval( _ptr, iHeartbeatInterval );
			else Platform.Win64.ISteamGameServer.SetHeartbeatInterval( _ptr, iHeartbeatInterval );
		}
		
		// void
		public void SetKeyValue( string pKey /*const char **/, string pValue /*const char **/ )
		{
			if ( Platform.IsWindows32 ) Platform.Win32.ISteamGameServer.SetKeyValue( _ptr, pKey, pValue );
			else Platform.Win64.ISteamGameServer.SetKeyValue( _ptr, pKey, pValue );
		}
		
		// void
		public void SetMapName( string pszMapName /*const char **/ )
		{
			if ( Platform.IsWindows32 ) Platform.Win32.ISteamGameServer.SetMapName( _ptr, pszMapName );
			else Platform.Win64.ISteamGameServer.SetMapName( _ptr, pszMapName );
		}
		
		// void
		public void SetMaxPlayerCount( int cPlayersMax /*int*/ )
		{
			if ( Platform.IsWindows32 ) Platform.Win32.ISteamGameServer.SetMaxPlayerCount( _ptr, cPlayersMax );
			else Platform.Win64.ISteamGameServer.SetMaxPlayerCount( _ptr, cPlayersMax );
		}
		
		// void
		public void SetModDir( string pszModDir /*const char **/ )
		{
			if ( Platform.IsWindows32 ) Platform.Win32.ISteamGameServer.SetModDir( _ptr, pszModDir );
			else Platform.Win64.ISteamGameServer.SetModDir( _ptr, pszModDir );
		}
		
		// void
		public void SetPasswordProtected( bool bPasswordProtected /*bool*/ )
		{
			if ( Platform.IsWindows32 ) Platform.Win32.ISteamGameServer.SetPasswordProtected( _ptr, bPasswordProtected );
			else Platform.Win64.ISteamGameServer.SetPasswordProtected( _ptr, bPasswordProtected );
		}
		
		// void
		public void SetProduct( string pszProduct /*const char **/ )
		{
			if ( Platform.IsWindows32 ) Platform.Win32.ISteamGameServer.SetProduct( _ptr, pszProduct );
			else Platform.Win64.ISteamGameServer.SetProduct( _ptr, pszProduct );
		}
		
		// void
		public void SetRegion( string pszRegion /*const char **/ )
		{
			if ( Platform.IsWindows32 ) Platform.Win32.ISteamGameServer.SetRegion( _ptr, pszRegion );
			else Platform.Win64.ISteamGameServer.SetRegion( _ptr, pszRegion );
		}
		
		// void
		public void SetServerName( string pszServerName /*const char **/ )
		{
			if ( Platform.IsWindows32 ) Platform.Win32.ISteamGameServer.SetServerName( _ptr, pszServerName );
			else Platform.Win64.ISteamGameServer.SetServerName( _ptr, pszServerName );
		}
		
		// void
		public void SetSpectatorPort( ushort unSpectatorPort /*uint16*/ )
		{
			if ( Platform.IsWindows32 ) Platform.Win32.ISteamGameServer.SetSpectatorPort( _ptr, unSpectatorPort );
			else Platform.Win64.ISteamGameServer.SetSpectatorPort( _ptr, unSpectatorPort );
		}
		
		// void
		public void SetSpectatorServerName( string pszSpectatorServerName /*const char **/ )
		{
			if ( Platform.IsWindows32 ) Platform.Win32.ISteamGameServer.SetSpectatorServerName( _ptr, pszSpectatorServerName );
			else Platform.Win64.ISteamGameServer.SetSpectatorServerName( _ptr, pszSpectatorServerName );
		}
		
		// UserHasLicenseForAppResult
		public UserHasLicenseForAppResult UserHasLicenseForApp( CSteamID steamID /*class CSteamID*/, AppId_t appID /*AppId_t*/ )
		{
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamGameServer.UserHasLicenseForApp( _ptr, steamID, appID );
			else return Platform.Win64.ISteamGameServer.UserHasLicenseForApp( _ptr, steamID, appID );
		}
		
		// bool
		public bool WasRestartRequested()
		{
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamGameServer.WasRestartRequested( _ptr );
			else return Platform.Win64.ISteamGameServer.WasRestartRequested( _ptr );
		}
		
	}
}
