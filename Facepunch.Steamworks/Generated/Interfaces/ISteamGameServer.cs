using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Steamworks.Data;


namespace Steamworks
{
	internal unsafe class ISteamGameServer : SteamInterface
	{
		
		internal ISteamGameServer( bool IsGameServer )
		{
			SetupInterface( IsGameServer );
		}
		
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_SteamGameServer_v014", CallingConvention = Platform.CC)]
		internal static extern IntPtr SteamAPI_SteamGameServer_v014();
		public override IntPtr GetServerInterfacePointer() => SteamAPI_SteamGameServer_v014();
		
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamGameServer_SetProduct", CallingConvention = Platform.CC)]
		private static extern void _SetProduct( IntPtr self, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pszProduct );
		
		#endregion
		internal void SetProduct( [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pszProduct )
		{
			_SetProduct( Self, pszProduct );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamGameServer_SetGameDescription", CallingConvention = Platform.CC)]
		private static extern void _SetGameDescription( IntPtr self, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pszGameDescription );
		
		#endregion
		internal void SetGameDescription( [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pszGameDescription )
		{
			_SetGameDescription( Self, pszGameDescription );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamGameServer_SetModDir", CallingConvention = Platform.CC)]
		private static extern void _SetModDir( IntPtr self, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pszModDir );
		
		#endregion
		internal void SetModDir( [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pszModDir )
		{
			_SetModDir( Self, pszModDir );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamGameServer_SetDedicatedServer", CallingConvention = Platform.CC)]
		private static extern void _SetDedicatedServer( IntPtr self, [MarshalAs( UnmanagedType.U1 )] bool bDedicated );
		
		#endregion
		internal void SetDedicatedServer( [MarshalAs( UnmanagedType.U1 )] bool bDedicated )
		{
			_SetDedicatedServer( Self, bDedicated );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamGameServer_LogOn", CallingConvention = Platform.CC)]
		private static extern void _LogOn( IntPtr self, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pszToken );
		
		#endregion
		internal void LogOn( [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pszToken )
		{
			_LogOn( Self, pszToken );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamGameServer_LogOnAnonymous", CallingConvention = Platform.CC)]
		private static extern void _LogOnAnonymous( IntPtr self );
		
		#endregion
		internal void LogOnAnonymous()
		{
			_LogOnAnonymous( Self );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamGameServer_LogOff", CallingConvention = Platform.CC)]
		private static extern void _LogOff( IntPtr self );
		
		#endregion
		internal void LogOff()
		{
			_LogOff( Self );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamGameServer_BLoggedOn", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _BLoggedOn( IntPtr self );
		
		#endregion
		internal bool BLoggedOn()
		{
			var returnValue = _BLoggedOn( Self );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamGameServer_BSecure", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _BSecure( IntPtr self );
		
		#endregion
		internal bool BSecure()
		{
			var returnValue = _BSecure( Self );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamGameServer_GetSteamID", CallingConvention = Platform.CC)]
		private static extern SteamId _GetSteamID( IntPtr self );
		
		#endregion
		internal SteamId GetSteamID()
		{
			var returnValue = _GetSteamID( Self );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamGameServer_WasRestartRequested", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _WasRestartRequested( IntPtr self );
		
		#endregion
		internal bool WasRestartRequested()
		{
			var returnValue = _WasRestartRequested( Self );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamGameServer_SetMaxPlayerCount", CallingConvention = Platform.CC)]
		private static extern void _SetMaxPlayerCount( IntPtr self, int cPlayersMax );
		
		#endregion
		internal void SetMaxPlayerCount( int cPlayersMax )
		{
			_SetMaxPlayerCount( Self, cPlayersMax );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamGameServer_SetBotPlayerCount", CallingConvention = Platform.CC)]
		private static extern void _SetBotPlayerCount( IntPtr self, int cBotplayers );
		
		#endregion
		internal void SetBotPlayerCount( int cBotplayers )
		{
			_SetBotPlayerCount( Self, cBotplayers );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamGameServer_SetServerName", CallingConvention = Platform.CC)]
		private static extern void _SetServerName( IntPtr self, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pszServerName );
		
		#endregion
		internal void SetServerName( [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pszServerName )
		{
			_SetServerName( Self, pszServerName );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamGameServer_SetMapName", CallingConvention = Platform.CC)]
		private static extern void _SetMapName( IntPtr self, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pszMapName );
		
		#endregion
		internal void SetMapName( [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pszMapName )
		{
			_SetMapName( Self, pszMapName );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamGameServer_SetPasswordProtected", CallingConvention = Platform.CC)]
		private static extern void _SetPasswordProtected( IntPtr self, [MarshalAs( UnmanagedType.U1 )] bool bPasswordProtected );
		
		#endregion
		internal void SetPasswordProtected( [MarshalAs( UnmanagedType.U1 )] bool bPasswordProtected )
		{
			_SetPasswordProtected( Self, bPasswordProtected );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamGameServer_SetSpectatorPort", CallingConvention = Platform.CC)]
		private static extern void _SetSpectatorPort( IntPtr self, ushort unSpectatorPort );
		
		#endregion
		internal void SetSpectatorPort( ushort unSpectatorPort )
		{
			_SetSpectatorPort( Self, unSpectatorPort );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamGameServer_SetSpectatorServerName", CallingConvention = Platform.CC)]
		private static extern void _SetSpectatorServerName( IntPtr self, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pszSpectatorServerName );
		
		#endregion
		internal void SetSpectatorServerName( [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pszSpectatorServerName )
		{
			_SetSpectatorServerName( Self, pszSpectatorServerName );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamGameServer_ClearAllKeyValues", CallingConvention = Platform.CC)]
		private static extern void _ClearAllKeyValues( IntPtr self );
		
		#endregion
		internal void ClearAllKeyValues()
		{
			_ClearAllKeyValues( Self );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamGameServer_SetKeyValue", CallingConvention = Platform.CC)]
		private static extern void _SetKeyValue( IntPtr self, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pKey, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pValue );
		
		#endregion
		internal void SetKeyValue( [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pKey, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pValue )
		{
			_SetKeyValue( Self, pKey, pValue );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamGameServer_SetGameTags", CallingConvention = Platform.CC)]
		private static extern void _SetGameTags( IntPtr self, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchGameTags );
		
		#endregion
		internal void SetGameTags( [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchGameTags )
		{
			_SetGameTags( Self, pchGameTags );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamGameServer_SetGameData", CallingConvention = Platform.CC)]
		private static extern void _SetGameData( IntPtr self, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchGameData );
		
		#endregion
		internal void SetGameData( [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchGameData )
		{
			_SetGameData( Self, pchGameData );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamGameServer_SetRegion", CallingConvention = Platform.CC)]
		private static extern void _SetRegion( IntPtr self, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pszRegion );
		
		#endregion
		internal void SetRegion( [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pszRegion )
		{
			_SetRegion( Self, pszRegion );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamGameServer_SetAdvertiseServerActive", CallingConvention = Platform.CC)]
		private static extern void _SetAdvertiseServerActive( IntPtr self, [MarshalAs( UnmanagedType.U1 )] bool bActive );
		
		#endregion
		internal void SetAdvertiseServerActive( [MarshalAs( UnmanagedType.U1 )] bool bActive )
		{
			_SetAdvertiseServerActive( Self, bActive );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamGameServer_GetAuthSessionTicket", CallingConvention = Platform.CC)]
		private static extern HAuthTicket _GetAuthSessionTicket( IntPtr self, IntPtr pTicket, int cbMaxTicket, ref uint pcbTicket );
		
		#endregion
		internal HAuthTicket GetAuthSessionTicket( IntPtr pTicket, int cbMaxTicket, ref uint pcbTicket )
		{
			var returnValue = _GetAuthSessionTicket( Self, pTicket, cbMaxTicket, ref pcbTicket );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamGameServer_BeginAuthSession", CallingConvention = Platform.CC)]
		private static extern BeginAuthResult _BeginAuthSession( IntPtr self, IntPtr pAuthTicket, int cbAuthTicket, SteamId steamID );
		
		#endregion
		internal BeginAuthResult BeginAuthSession( IntPtr pAuthTicket, int cbAuthTicket, SteamId steamID )
		{
			var returnValue = _BeginAuthSession( Self, pAuthTicket, cbAuthTicket, steamID );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamGameServer_EndAuthSession", CallingConvention = Platform.CC)]
		private static extern void _EndAuthSession( IntPtr self, SteamId steamID );
		
		#endregion
		internal void EndAuthSession( SteamId steamID )
		{
			_EndAuthSession( Self, steamID );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamGameServer_CancelAuthTicket", CallingConvention = Platform.CC)]
		private static extern void _CancelAuthTicket( IntPtr self, HAuthTicket hAuthTicket );
		
		#endregion
		internal void CancelAuthTicket( HAuthTicket hAuthTicket )
		{
			_CancelAuthTicket( Self, hAuthTicket );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamGameServer_UserHasLicenseForApp", CallingConvention = Platform.CC)]
		private static extern UserHasLicenseForAppResult _UserHasLicenseForApp( IntPtr self, SteamId steamID, AppId appID );
		
		#endregion
		internal UserHasLicenseForAppResult UserHasLicenseForApp( SteamId steamID, AppId appID )
		{
			var returnValue = _UserHasLicenseForApp( Self, steamID, appID );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamGameServer_RequestUserGroupStatus", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _RequestUserGroupStatus( IntPtr self, SteamId steamIDUser, SteamId steamIDGroup );
		
		#endregion
		internal bool RequestUserGroupStatus( SteamId steamIDUser, SteamId steamIDGroup )
		{
			var returnValue = _RequestUserGroupStatus( Self, steamIDUser, steamIDGroup );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamGameServer_GetGameplayStats", CallingConvention = Platform.CC)]
		private static extern void _GetGameplayStats( IntPtr self );
		
		#endregion
		internal void GetGameplayStats()
		{
			_GetGameplayStats( Self );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamGameServer_GetServerReputation", CallingConvention = Platform.CC)]
		private static extern SteamAPICall_t _GetServerReputation( IntPtr self );
		
		#endregion
		internal CallResult<GSReputation_t> GetServerReputation()
		{
			var returnValue = _GetServerReputation( Self );
			return new CallResult<GSReputation_t>( returnValue, IsServer );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamGameServer_GetPublicIP", CallingConvention = Platform.CC)]
		private static extern SteamIPAddress _GetPublicIP( IntPtr self );
		
		#endregion
		internal SteamIPAddress GetPublicIP()
		{
			var returnValue = _GetPublicIP( Self );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamGameServer_HandleIncomingPacket", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _HandleIncomingPacket( IntPtr self, IntPtr pData, int cbData, uint srcIP, ushort srcPort );
		
		#endregion
		internal bool HandleIncomingPacket( IntPtr pData, int cbData, uint srcIP, ushort srcPort )
		{
			var returnValue = _HandleIncomingPacket( Self, pData, cbData, srcIP, srcPort );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamGameServer_GetNextOutgoingPacket", CallingConvention = Platform.CC)]
		private static extern int _GetNextOutgoingPacket( IntPtr self, IntPtr pOut, int cbMaxOut, ref uint pNetAdr, ref ushort pPort );
		
		#endregion
		internal int GetNextOutgoingPacket( IntPtr pOut, int cbMaxOut, ref uint pNetAdr, ref ushort pPort )
		{
			var returnValue = _GetNextOutgoingPacket( Self, pOut, cbMaxOut, ref pNetAdr, ref pPort );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamGameServer_AssociateWithClan", CallingConvention = Platform.CC)]
		private static extern SteamAPICall_t _AssociateWithClan( IntPtr self, SteamId steamIDClan );
		
		#endregion
		internal CallResult<AssociateWithClanResult_t> AssociateWithClan( SteamId steamIDClan )
		{
			var returnValue = _AssociateWithClan( Self, steamIDClan );
			return new CallResult<AssociateWithClanResult_t>( returnValue, IsServer );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamGameServer_ComputeNewPlayerCompatibility", CallingConvention = Platform.CC)]
		private static extern SteamAPICall_t _ComputeNewPlayerCompatibility( IntPtr self, SteamId steamIDNewPlayer );
		
		#endregion
		internal CallResult<ComputeNewPlayerCompatibilityResult_t> ComputeNewPlayerCompatibility( SteamId steamIDNewPlayer )
		{
			var returnValue = _ComputeNewPlayerCompatibility( Self, steamIDNewPlayer );
			return new CallResult<ComputeNewPlayerCompatibilityResult_t>( returnValue, IsServer );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamGameServer_SendUserConnectAndAuthenticate_DEPRECATED", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _SendUserConnectAndAuthenticate_DEPRECATED( IntPtr self, uint unIPClient, IntPtr pvAuthBlob, uint cubAuthBlobSize, ref SteamId pSteamIDUser );
		
		#endregion
		internal bool SendUserConnectAndAuthenticate_DEPRECATED( uint unIPClient, IntPtr pvAuthBlob, uint cubAuthBlobSize, ref SteamId pSteamIDUser )
		{
			var returnValue = _SendUserConnectAndAuthenticate_DEPRECATED( Self, unIPClient, pvAuthBlob, cubAuthBlobSize, ref pSteamIDUser );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamGameServer_CreateUnauthenticatedUserConnection", CallingConvention = Platform.CC)]
		private static extern SteamId _CreateUnauthenticatedUserConnection( IntPtr self );
		
		#endregion
		internal SteamId CreateUnauthenticatedUserConnection()
		{
			var returnValue = _CreateUnauthenticatedUserConnection( Self );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamGameServer_SendUserDisconnect_DEPRECATED", CallingConvention = Platform.CC)]
		private static extern void _SendUserDisconnect_DEPRECATED( IntPtr self, SteamId steamIDUser );
		
		#endregion
		internal void SendUserDisconnect_DEPRECATED( SteamId steamIDUser )
		{
			_SendUserDisconnect_DEPRECATED( Self, steamIDUser );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamGameServer_BUpdateUserData", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _BUpdateUserData( IntPtr self, SteamId steamIDUser, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchPlayerName, uint uScore );
		
		#endregion
		internal bool BUpdateUserData( SteamId steamIDUser, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchPlayerName, uint uScore )
		{
			var returnValue = _BUpdateUserData( Self, steamIDUser, pchPlayerName, uScore );
			return returnValue;
		}
		
	}
}
