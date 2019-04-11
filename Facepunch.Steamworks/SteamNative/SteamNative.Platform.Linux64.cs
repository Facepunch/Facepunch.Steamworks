using System;
using System.Runtime.InteropServices;
using System.Linq;

namespace SteamNative
{
	internal static partial class Platform
	{
		internal class Linux : Interface
		{
			internal IntPtr _ptr;
			public bool IsValid { get{ return _ptr != IntPtr.Zero; } }
			
			//
			// Constructor sets pointer to native class
			//
			internal Linux( IntPtr pointer )
			{
				_ptr = pointer;
			}
			//
			// When shutting down clear all the internals to avoid accidental use
			//
			public virtual void Dispose()
			{
				_ptr = IntPtr.Zero;
			}
			
			public virtual HSteamPipe /*(HSteamPipe)*/ ISteamClient_CreateSteamPipe()
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamClient _ptr is null!" );
				
				return Native.SteamAPI_ISteamClient_CreateSteamPipe(_ptr);
			}
			public virtual bool /*bool*/ ISteamClient_BReleaseSteamPipe( int hSteamPipe )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamClient _ptr is null!" );
				
				return Native.SteamAPI_ISteamClient_BReleaseSteamPipe(_ptr, hSteamPipe);
			}
			public virtual HSteamUser /*(HSteamUser)*/ ISteamClient_ConnectToGlobalUser( int hSteamPipe )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamClient _ptr is null!" );
				
				return Native.SteamAPI_ISteamClient_ConnectToGlobalUser(_ptr, hSteamPipe);
			}
			public virtual HSteamUser /*(HSteamUser)*/ ISteamClient_CreateLocalUser( out int phSteamPipe, AccountType /*EAccountType*/ eAccountType )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamClient _ptr is null!" );
				
				return Native.SteamAPI_ISteamClient_CreateLocalUser(_ptr, out phSteamPipe, eAccountType);
			}
			public virtual void /*void*/ ISteamClient_ReleaseUser( int hSteamPipe, int hUser )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamClient _ptr is null!" );
				
				Native.SteamAPI_ISteamClient_ReleaseUser(_ptr, hSteamPipe, hUser);
			}
			public virtual IntPtr /*class ISteamUser **/ ISteamClient_GetISteamUser( int hSteamUser, int hSteamPipe, string /*const char **/ pchVersion )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamClient _ptr is null!" );
				
				return Native.SteamAPI_ISteamClient_GetISteamUser(_ptr, hSteamUser, hSteamPipe, pchVersion);
			}
			public virtual IntPtr /*class ISteamGameServer **/ ISteamClient_GetISteamGameServer( int hSteamUser, int hSteamPipe, string /*const char **/ pchVersion )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamClient _ptr is null!" );
				
				return Native.SteamAPI_ISteamClient_GetISteamGameServer(_ptr, hSteamUser, hSteamPipe, pchVersion);
			}
			public virtual void /*void*/ ISteamClient_SetLocalIPBinding( uint /*uint32*/ unIP, ushort /*uint16*/ usPort )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamClient _ptr is null!" );
				
				Native.SteamAPI_ISteamClient_SetLocalIPBinding(_ptr, unIP, usPort);
			}
			public virtual IntPtr /*class ISteamFriends **/ ISteamClient_GetISteamFriends( int hSteamUser, int hSteamPipe, string /*const char **/ pchVersion )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamClient _ptr is null!" );
				
				return Native.SteamAPI_ISteamClient_GetISteamFriends(_ptr, hSteamUser, hSteamPipe, pchVersion);
			}
			public virtual IntPtr /*class ISteamUtils **/ ISteamClient_GetISteamUtils( int hSteamPipe, string /*const char **/ pchVersion )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamClient _ptr is null!" );
				
				return Native.SteamAPI_ISteamClient_GetISteamUtils(_ptr, hSteamPipe, pchVersion);
			}
			public virtual IntPtr /*class ISteamMatchmaking **/ ISteamClient_GetISteamMatchmaking( int hSteamUser, int hSteamPipe, string /*const char **/ pchVersion )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamClient _ptr is null!" );
				
				return Native.SteamAPI_ISteamClient_GetISteamMatchmaking(_ptr, hSteamUser, hSteamPipe, pchVersion);
			}
			public virtual IntPtr /*class ISteamMatchmakingServers **/ ISteamClient_GetISteamMatchmakingServers( int hSteamUser, int hSteamPipe, string /*const char **/ pchVersion )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamClient _ptr is null!" );
				
				return Native.SteamAPI_ISteamClient_GetISteamMatchmakingServers(_ptr, hSteamUser, hSteamPipe, pchVersion);
			}
			public virtual IntPtr /*void **/ ISteamClient_GetISteamGenericInterface( int hSteamUser, int hSteamPipe, string /*const char **/ pchVersion )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamClient _ptr is null!" );
				
				return Native.SteamAPI_ISteamClient_GetISteamGenericInterface(_ptr, hSteamUser, hSteamPipe, pchVersion);
			}
			public virtual IntPtr /*class ISteamUserStats **/ ISteamClient_GetISteamUserStats( int hSteamUser, int hSteamPipe, string /*const char **/ pchVersion )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamClient _ptr is null!" );
				
				return Native.SteamAPI_ISteamClient_GetISteamUserStats(_ptr, hSteamUser, hSteamPipe, pchVersion);
			}
			public virtual IntPtr /*class ISteamGameServerStats **/ ISteamClient_GetISteamGameServerStats( int hSteamuser, int hSteamPipe, string /*const char **/ pchVersion )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamClient _ptr is null!" );
				
				return Native.SteamAPI_ISteamClient_GetISteamGameServerStats(_ptr, hSteamuser, hSteamPipe, pchVersion);
			}
			public virtual IntPtr /*class ISteamApps **/ ISteamClient_GetISteamApps( int hSteamUser, int hSteamPipe, string /*const char **/ pchVersion )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamClient _ptr is null!" );
				
				return Native.SteamAPI_ISteamClient_GetISteamApps(_ptr, hSteamUser, hSteamPipe, pchVersion);
			}
			public virtual IntPtr /*class ISteamNetworking **/ ISteamClient_GetISteamNetworking( int hSteamUser, int hSteamPipe, string /*const char **/ pchVersion )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamClient _ptr is null!" );
				
				return Native.SteamAPI_ISteamClient_GetISteamNetworking(_ptr, hSteamUser, hSteamPipe, pchVersion);
			}
			public virtual IntPtr /*class ISteamRemoteStorage **/ ISteamClient_GetISteamRemoteStorage( int hSteamuser, int hSteamPipe, string /*const char **/ pchVersion )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamClient _ptr is null!" );
				
				return Native.SteamAPI_ISteamClient_GetISteamRemoteStorage(_ptr, hSteamuser, hSteamPipe, pchVersion);
			}
			public virtual IntPtr /*class ISteamScreenshots **/ ISteamClient_GetISteamScreenshots( int hSteamuser, int hSteamPipe, string /*const char **/ pchVersion )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamClient _ptr is null!" );
				
				return Native.SteamAPI_ISteamClient_GetISteamScreenshots(_ptr, hSteamuser, hSteamPipe, pchVersion);
			}
			public virtual IntPtr /*class ISteamGameSearch **/ ISteamClient_GetISteamGameSearch( int hSteamuser, int hSteamPipe, string /*const char **/ pchVersion )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamClient _ptr is null!" );
				
				return Native.SteamAPI_ISteamClient_GetISteamGameSearch(_ptr, hSteamuser, hSteamPipe, pchVersion);
			}
			public virtual uint /*uint32*/ ISteamClient_GetIPCCallCount()
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamClient _ptr is null!" );
				
				return Native.SteamAPI_ISteamClient_GetIPCCallCount(_ptr);
			}
			public virtual void /*void*/ ISteamClient_SetWarningMessageHook( IntPtr /*SteamAPIWarningMessageHook_t*/ pFunction )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamClient _ptr is null!" );
				
				Native.SteamAPI_ISteamClient_SetWarningMessageHook(_ptr, pFunction);
			}
			public virtual bool /*bool*/ ISteamClient_BShutdownIfAllPipesClosed()
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamClient _ptr is null!" );
				
				return Native.SteamAPI_ISteamClient_BShutdownIfAllPipesClosed(_ptr);
			}
			public virtual IntPtr /*class ISteamHTTP **/ ISteamClient_GetISteamHTTP( int hSteamuser, int hSteamPipe, string /*const char **/ pchVersion )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamClient _ptr is null!" );
				
				return Native.SteamAPI_ISteamClient_GetISteamHTTP(_ptr, hSteamuser, hSteamPipe, pchVersion);
			}
			public virtual IntPtr /*class ISteamController **/ ISteamClient_GetISteamController( int hSteamUser, int hSteamPipe, string /*const char **/ pchVersion )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamClient _ptr is null!" );
				
				return Native.SteamAPI_ISteamClient_GetISteamController(_ptr, hSteamUser, hSteamPipe, pchVersion);
			}
			public virtual IntPtr /*class ISteamUGC **/ ISteamClient_GetISteamUGC( int hSteamUser, int hSteamPipe, string /*const char **/ pchVersion )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamClient _ptr is null!" );
				
				return Native.SteamAPI_ISteamClient_GetISteamUGC(_ptr, hSteamUser, hSteamPipe, pchVersion);
			}
			public virtual IntPtr /*class ISteamAppList **/ ISteamClient_GetISteamAppList( int hSteamUser, int hSteamPipe, string /*const char **/ pchVersion )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamClient _ptr is null!" );
				
				return Native.SteamAPI_ISteamClient_GetISteamAppList(_ptr, hSteamUser, hSteamPipe, pchVersion);
			}
			public virtual IntPtr /*class ISteamMusic **/ ISteamClient_GetISteamMusic( int hSteamuser, int hSteamPipe, string /*const char **/ pchVersion )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamClient _ptr is null!" );
				
				return Native.SteamAPI_ISteamClient_GetISteamMusic(_ptr, hSteamuser, hSteamPipe, pchVersion);
			}
			public virtual IntPtr /*class ISteamMusicRemote **/ ISteamClient_GetISteamMusicRemote( int hSteamuser, int hSteamPipe, string /*const char **/ pchVersion )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamClient _ptr is null!" );
				
				return Native.SteamAPI_ISteamClient_GetISteamMusicRemote(_ptr, hSteamuser, hSteamPipe, pchVersion);
			}
			public virtual IntPtr /*class ISteamHTMLSurface **/ ISteamClient_GetISteamHTMLSurface( int hSteamuser, int hSteamPipe, string /*const char **/ pchVersion )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamClient _ptr is null!" );
				
				return Native.SteamAPI_ISteamClient_GetISteamHTMLSurface(_ptr, hSteamuser, hSteamPipe, pchVersion);
			}
			public virtual IntPtr /*class ISteamInventory **/ ISteamClient_GetISteamInventory( int hSteamuser, int hSteamPipe, string /*const char **/ pchVersion )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamClient _ptr is null!" );
				
				return Native.SteamAPI_ISteamClient_GetISteamInventory(_ptr, hSteamuser, hSteamPipe, pchVersion);
			}
			public virtual IntPtr /*class ISteamVideo **/ ISteamClient_GetISteamVideo( int hSteamuser, int hSteamPipe, string /*const char **/ pchVersion )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamClient _ptr is null!" );
				
				return Native.SteamAPI_ISteamClient_GetISteamVideo(_ptr, hSteamuser, hSteamPipe, pchVersion);
			}
			public virtual IntPtr /*class ISteamParentalSettings **/ ISteamClient_GetISteamParentalSettings( int hSteamuser, int hSteamPipe, string /*const char **/ pchVersion )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamClient _ptr is null!" );
				
				return Native.SteamAPI_ISteamClient_GetISteamParentalSettings(_ptr, hSteamuser, hSteamPipe, pchVersion);
			}
			public virtual IntPtr /*class ISteamInput **/ ISteamClient_GetISteamInput( int hSteamUser, int hSteamPipe, string /*const char **/ pchVersion )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamClient _ptr is null!" );
				
				return Native.SteamAPI_ISteamClient_GetISteamInput(_ptr, hSteamUser, hSteamPipe, pchVersion);
			}
			public virtual IntPtr /*class ISteamParties **/ ISteamClient_GetISteamParties( int hSteamUser, int hSteamPipe, string /*const char **/ pchVersion )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamClient _ptr is null!" );
				
				return Native.SteamAPI_ISteamClient_GetISteamParties(_ptr, hSteamUser, hSteamPipe, pchVersion);
			}
			
			public virtual HSteamUser /*(HSteamUser)*/ ISteamUser_GetHSteamUser()
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamUser _ptr is null!" );
				
				return Native.SteamAPI_ISteamUser_GetHSteamUser(_ptr);
			}
			public virtual bool /*bool*/ ISteamUser_BLoggedOn()
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamUser _ptr is null!" );
				
				return Native.SteamAPI_ISteamUser_BLoggedOn(_ptr);
			}
			public virtual CSteamID /*(class CSteamID)*/ ISteamUser_GetSteamID()
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamUser _ptr is null!" );
				
				return Native.SteamAPI_ISteamUser_GetSteamID(_ptr);
			}
			public virtual int /*int*/ ISteamUser_InitiateGameConnection( IntPtr /*void **/ pAuthBlob, int /*int*/ cbMaxAuthBlob, ulong steamIDGameServer, uint /*uint32*/ unIPServer, ushort /*uint16*/ usPortServer, [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bSecure )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamUser _ptr is null!" );
				
				return Native.SteamAPI_ISteamUser_InitiateGameConnection(_ptr, pAuthBlob, cbMaxAuthBlob, steamIDGameServer, unIPServer, usPortServer, bSecure);
			}
			public virtual void /*void*/ ISteamUser_TerminateGameConnection( uint /*uint32*/ unIPServer, ushort /*uint16*/ usPortServer )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamUser _ptr is null!" );
				
				Native.SteamAPI_ISteamUser_TerminateGameConnection(_ptr, unIPServer, usPortServer);
			}
			public virtual void /*void*/ ISteamUser_TrackAppUsageEvent( ulong gameID, int /*int*/ eAppUsageEvent, string /*const char **/ pchExtraInfo )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamUser _ptr is null!" );
				
				Native.SteamAPI_ISteamUser_TrackAppUsageEvent(_ptr, gameID, eAppUsageEvent, pchExtraInfo);
			}
			public virtual bool /*bool*/ ISteamUser_GetUserDataFolder( System.Text.StringBuilder /*char **/ pchBuffer, int /*int*/ cubBuffer )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamUser _ptr is null!" );
				
				return Native.SteamAPI_ISteamUser_GetUserDataFolder(_ptr, pchBuffer, cubBuffer);
			}
			public virtual void /*void*/ ISteamUser_StartVoiceRecording()
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamUser _ptr is null!" );
				
				Native.SteamAPI_ISteamUser_StartVoiceRecording(_ptr);
			}
			public virtual void /*void*/ ISteamUser_StopVoiceRecording()
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamUser _ptr is null!" );
				
				Native.SteamAPI_ISteamUser_StopVoiceRecording(_ptr);
			}
			public virtual VoiceResult /*EVoiceResult*/ ISteamUser_GetAvailableVoice( out uint /*uint32 **/ pcbCompressed, out uint /*uint32 **/ pcbUncompressed_Deprecated, uint /*uint32*/ nUncompressedVoiceDesiredSampleRate_Deprecated )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamUser _ptr is null!" );
				
				return Native.SteamAPI_ISteamUser_GetAvailableVoice(_ptr, out pcbCompressed, out pcbUncompressed_Deprecated, nUncompressedVoiceDesiredSampleRate_Deprecated);
			}
			public virtual VoiceResult /*EVoiceResult*/ ISteamUser_GetVoice( [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bWantCompressed, IntPtr /*void **/ pDestBuffer, uint /*uint32*/ cbDestBufferSize, out uint /*uint32 **/ nBytesWritten, [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bWantUncompressed_Deprecated, IntPtr /*void **/ pUncompressedDestBuffer_Deprecated, uint /*uint32*/ cbUncompressedDestBufferSize_Deprecated, out uint /*uint32 **/ nUncompressBytesWritten_Deprecated, uint /*uint32*/ nUncompressedVoiceDesiredSampleRate_Deprecated )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamUser _ptr is null!" );
				
				return Native.SteamAPI_ISteamUser_GetVoice(_ptr, bWantCompressed, pDestBuffer, cbDestBufferSize, out nBytesWritten, bWantUncompressed_Deprecated, pUncompressedDestBuffer_Deprecated, cbUncompressedDestBufferSize_Deprecated, out nUncompressBytesWritten_Deprecated, nUncompressedVoiceDesiredSampleRate_Deprecated);
			}
			public virtual VoiceResult /*EVoiceResult*/ ISteamUser_DecompressVoice( IntPtr /*const void **/ pCompressed, uint /*uint32*/ cbCompressed, IntPtr /*void **/ pDestBuffer, uint /*uint32*/ cbDestBufferSize, out uint /*uint32 **/ nBytesWritten, uint /*uint32*/ nDesiredSampleRate )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamUser _ptr is null!" );
				
				return Native.SteamAPI_ISteamUser_DecompressVoice(_ptr, pCompressed, cbCompressed, pDestBuffer, cbDestBufferSize, out nBytesWritten, nDesiredSampleRate);
			}
			public virtual uint /*uint32*/ ISteamUser_GetVoiceOptimalSampleRate()
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamUser _ptr is null!" );
				
				return Native.SteamAPI_ISteamUser_GetVoiceOptimalSampleRate(_ptr);
			}
			public virtual HAuthTicket /*(HAuthTicket)*/ ISteamUser_GetAuthSessionTicket( IntPtr /*void **/ pTicket, int /*int*/ cbMaxTicket, out uint /*uint32 **/ pcbTicket )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamUser _ptr is null!" );
				
				return Native.SteamAPI_ISteamUser_GetAuthSessionTicket(_ptr, pTicket, cbMaxTicket, out pcbTicket);
			}
			public virtual BeginAuthSessionResult /*EBeginAuthSessionResult*/ ISteamUser_BeginAuthSession( IntPtr /*const void **/ pAuthTicket, int /*int*/ cbAuthTicket, ulong steamID )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamUser _ptr is null!" );
				
				return Native.SteamAPI_ISteamUser_BeginAuthSession(_ptr, pAuthTicket, cbAuthTicket, steamID);
			}
			public virtual void /*void*/ ISteamUser_EndAuthSession( ulong steamID )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamUser _ptr is null!" );
				
				Native.SteamAPI_ISteamUser_EndAuthSession(_ptr, steamID);
			}
			public virtual void /*void*/ ISteamUser_CancelAuthTicket( uint hAuthTicket )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamUser _ptr is null!" );
				
				Native.SteamAPI_ISteamUser_CancelAuthTicket(_ptr, hAuthTicket);
			}
			public virtual UserHasLicenseForAppResult /*EUserHasLicenseForAppResult*/ ISteamUser_UserHasLicenseForApp( ulong steamID, uint appID )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamUser _ptr is null!" );
				
				return Native.SteamAPI_ISteamUser_UserHasLicenseForApp(_ptr, steamID, appID);
			}
			public virtual bool /*bool*/ ISteamUser_BIsBehindNAT()
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamUser _ptr is null!" );
				
				return Native.SteamAPI_ISteamUser_BIsBehindNAT(_ptr);
			}
			public virtual void /*void*/ ISteamUser_AdvertiseGame( ulong steamIDGameServer, uint /*uint32*/ unIPServer, ushort /*uint16*/ usPortServer )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamUser _ptr is null!" );
				
				Native.SteamAPI_ISteamUser_AdvertiseGame(_ptr, steamIDGameServer, unIPServer, usPortServer);
			}
			public virtual SteamAPICall_t /*(SteamAPICall_t)*/ ISteamUser_RequestEncryptedAppTicket( IntPtr /*void **/ pDataToInclude, int /*int*/ cbDataToInclude )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamUser _ptr is null!" );
				
				return Native.SteamAPI_ISteamUser_RequestEncryptedAppTicket(_ptr, pDataToInclude, cbDataToInclude);
			}
			public virtual bool /*bool*/ ISteamUser_GetEncryptedAppTicket( IntPtr /*void **/ pTicket, int /*int*/ cbMaxTicket, out uint /*uint32 **/ pcbTicket )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamUser _ptr is null!" );
				
				return Native.SteamAPI_ISteamUser_GetEncryptedAppTicket(_ptr, pTicket, cbMaxTicket, out pcbTicket);
			}
			public virtual int /*int*/ ISteamUser_GetGameBadgeLevel( int /*int*/ nSeries, [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bFoil )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamUser _ptr is null!" );
				
				return Native.SteamAPI_ISteamUser_GetGameBadgeLevel(_ptr, nSeries, bFoil);
			}
			public virtual int /*int*/ ISteamUser_GetPlayerSteamLevel()
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamUser _ptr is null!" );
				
				return Native.SteamAPI_ISteamUser_GetPlayerSteamLevel(_ptr);
			}
			public virtual SteamAPICall_t /*(SteamAPICall_t)*/ ISteamUser_RequestStoreAuthURL( string /*const char **/ pchRedirectURL )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamUser _ptr is null!" );
				
				return Native.SteamAPI_ISteamUser_RequestStoreAuthURL(_ptr, pchRedirectURL);
			}
			public virtual bool /*bool*/ ISteamUser_BIsPhoneVerified()
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamUser _ptr is null!" );
				
				return Native.SteamAPI_ISteamUser_BIsPhoneVerified(_ptr);
			}
			public virtual bool /*bool*/ ISteamUser_BIsTwoFactorEnabled()
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamUser _ptr is null!" );
				
				return Native.SteamAPI_ISteamUser_BIsTwoFactorEnabled(_ptr);
			}
			public virtual bool /*bool*/ ISteamUser_BIsPhoneIdentifying()
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamUser _ptr is null!" );
				
				return Native.SteamAPI_ISteamUser_BIsPhoneIdentifying(_ptr);
			}
			public virtual bool /*bool*/ ISteamUser_BIsPhoneRequiringVerification()
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamUser _ptr is null!" );
				
				return Native.SteamAPI_ISteamUser_BIsPhoneRequiringVerification(_ptr);
			}
			public virtual SteamAPICall_t /*(SteamAPICall_t)*/ ISteamUser_GetMarketEligibility()
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamUser _ptr is null!" );
				
				return Native.SteamAPI_ISteamUser_GetMarketEligibility(_ptr);
			}
			
			public virtual IntPtr ISteamFriends_GetPersonaName()
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamFriends _ptr is null!" );
				
				return Native.SteamAPI_ISteamFriends_GetPersonaName(_ptr);
			}
			public virtual SteamAPICall_t /*(SteamAPICall_t)*/ ISteamFriends_SetPersonaName( string /*const char **/ pchPersonaName )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamFriends _ptr is null!" );
				
				return Native.SteamAPI_ISteamFriends_SetPersonaName(_ptr, pchPersonaName);
			}
			public virtual PersonaState /*EPersonaState*/ ISteamFriends_GetPersonaState()
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamFriends _ptr is null!" );
				
				return Native.SteamAPI_ISteamFriends_GetPersonaState(_ptr);
			}
			public virtual int /*int*/ ISteamFriends_GetFriendCount( int /*int*/ iFriendFlags )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamFriends _ptr is null!" );
				
				return Native.SteamAPI_ISteamFriends_GetFriendCount(_ptr, iFriendFlags);
			}
			public virtual CSteamID /*(class CSteamID)*/ ISteamFriends_GetFriendByIndex( int /*int*/ iFriend, int /*int*/ iFriendFlags )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamFriends _ptr is null!" );
				
				return Native.SteamAPI_ISteamFriends_GetFriendByIndex(_ptr, iFriend, iFriendFlags);
			}
			public virtual FriendRelationship /*EFriendRelationship*/ ISteamFriends_GetFriendRelationship( ulong steamIDFriend )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamFriends _ptr is null!" );
				
				return Native.SteamAPI_ISteamFriends_GetFriendRelationship(_ptr, steamIDFriend);
			}
			public virtual PersonaState /*EPersonaState*/ ISteamFriends_GetFriendPersonaState( ulong steamIDFriend )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamFriends _ptr is null!" );
				
				return Native.SteamAPI_ISteamFriends_GetFriendPersonaState(_ptr, steamIDFriend);
			}
			public virtual IntPtr ISteamFriends_GetFriendPersonaName( ulong steamIDFriend )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamFriends _ptr is null!" );
				
				return Native.SteamAPI_ISteamFriends_GetFriendPersonaName(_ptr, steamIDFriend);
			}
			public virtual bool /*bool*/ ISteamFriends_GetFriendGamePlayed( ulong steamIDFriend, ref FriendGameInfo_t /*struct FriendGameInfo_t **/ pFriendGameInfo )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamFriends _ptr is null!" );
				
				var pFriendGameInfo_ps = new FriendGameInfo_t.PackSmall();
				var ret = Native.SteamAPI_ISteamFriends_GetFriendGamePlayed(_ptr, steamIDFriend, ref pFriendGameInfo_ps);
				pFriendGameInfo = pFriendGameInfo_ps;
				return ret;
			}
			public virtual IntPtr ISteamFriends_GetFriendPersonaNameHistory( ulong steamIDFriend, int /*int*/ iPersonaName )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamFriends _ptr is null!" );
				
				return Native.SteamAPI_ISteamFriends_GetFriendPersonaNameHistory(_ptr, steamIDFriend, iPersonaName);
			}
			public virtual int /*int*/ ISteamFriends_GetFriendSteamLevel( ulong steamIDFriend )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamFriends _ptr is null!" );
				
				return Native.SteamAPI_ISteamFriends_GetFriendSteamLevel(_ptr, steamIDFriend);
			}
			public virtual IntPtr ISteamFriends_GetPlayerNickname( ulong steamIDPlayer )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamFriends _ptr is null!" );
				
				return Native.SteamAPI_ISteamFriends_GetPlayerNickname(_ptr, steamIDPlayer);
			}
			public virtual int /*int*/ ISteamFriends_GetFriendsGroupCount()
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamFriends _ptr is null!" );
				
				return Native.SteamAPI_ISteamFriends_GetFriendsGroupCount(_ptr);
			}
			public virtual FriendsGroupID_t /*(FriendsGroupID_t)*/ ISteamFriends_GetFriendsGroupIDByIndex( int /*int*/ iFG )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamFriends _ptr is null!" );
				
				return Native.SteamAPI_ISteamFriends_GetFriendsGroupIDByIndex(_ptr, iFG);
			}
			public virtual IntPtr ISteamFriends_GetFriendsGroupName( short friendsGroupID )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamFriends _ptr is null!" );
				
				return Native.SteamAPI_ISteamFriends_GetFriendsGroupName(_ptr, friendsGroupID);
			}
			public virtual int /*int*/ ISteamFriends_GetFriendsGroupMembersCount( short friendsGroupID )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamFriends _ptr is null!" );
				
				return Native.SteamAPI_ISteamFriends_GetFriendsGroupMembersCount(_ptr, friendsGroupID);
			}
			public virtual void /*void*/ ISteamFriends_GetFriendsGroupMembersList( short friendsGroupID, IntPtr /*class CSteamID **/ pOutSteamIDMembers, int /*int*/ nMembersCount )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamFriends _ptr is null!" );
				
				Native.SteamAPI_ISteamFriends_GetFriendsGroupMembersList(_ptr, friendsGroupID, pOutSteamIDMembers, nMembersCount);
			}
			public virtual bool /*bool*/ ISteamFriends_HasFriend( ulong steamIDFriend, int /*int*/ iFriendFlags )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamFriends _ptr is null!" );
				
				return Native.SteamAPI_ISteamFriends_HasFriend(_ptr, steamIDFriend, iFriendFlags);
			}
			public virtual int /*int*/ ISteamFriends_GetClanCount()
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamFriends _ptr is null!" );
				
				return Native.SteamAPI_ISteamFriends_GetClanCount(_ptr);
			}
			public virtual CSteamID /*(class CSteamID)*/ ISteamFriends_GetClanByIndex( int /*int*/ iClan )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamFriends _ptr is null!" );
				
				return Native.SteamAPI_ISteamFriends_GetClanByIndex(_ptr, iClan);
			}
			public virtual IntPtr ISteamFriends_GetClanName( ulong steamIDClan )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamFriends _ptr is null!" );
				
				return Native.SteamAPI_ISteamFriends_GetClanName(_ptr, steamIDClan);
			}
			public virtual IntPtr ISteamFriends_GetClanTag( ulong steamIDClan )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamFriends _ptr is null!" );
				
				return Native.SteamAPI_ISteamFriends_GetClanTag(_ptr, steamIDClan);
			}
			public virtual bool /*bool*/ ISteamFriends_GetClanActivityCounts( ulong steamIDClan, out int /*int **/ pnOnline, out int /*int **/ pnInGame, out int /*int **/ pnChatting )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamFriends _ptr is null!" );
				
				return Native.SteamAPI_ISteamFriends_GetClanActivityCounts(_ptr, steamIDClan, out pnOnline, out pnInGame, out pnChatting);
			}
			public virtual SteamAPICall_t /*(SteamAPICall_t)*/ ISteamFriends_DownloadClanActivityCounts( IntPtr /*class CSteamID **/ psteamIDClans, int /*int*/ cClansToRequest )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamFriends _ptr is null!" );
				
				return Native.SteamAPI_ISteamFriends_DownloadClanActivityCounts(_ptr, psteamIDClans, cClansToRequest);
			}
			public virtual int /*int*/ ISteamFriends_GetFriendCountFromSource( ulong steamIDSource )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamFriends _ptr is null!" );
				
				return Native.SteamAPI_ISteamFriends_GetFriendCountFromSource(_ptr, steamIDSource);
			}
			public virtual CSteamID /*(class CSteamID)*/ ISteamFriends_GetFriendFromSourceByIndex( ulong steamIDSource, int /*int*/ iFriend )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamFriends _ptr is null!" );
				
				return Native.SteamAPI_ISteamFriends_GetFriendFromSourceByIndex(_ptr, steamIDSource, iFriend);
			}
			public virtual bool /*bool*/ ISteamFriends_IsUserInSource( ulong steamIDUser, ulong steamIDSource )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamFriends _ptr is null!" );
				
				return Native.SteamAPI_ISteamFriends_IsUserInSource(_ptr, steamIDUser, steamIDSource);
			}
			public virtual void /*void*/ ISteamFriends_SetInGameVoiceSpeaking( ulong steamIDUser, [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bSpeaking )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamFriends _ptr is null!" );
				
				Native.SteamAPI_ISteamFriends_SetInGameVoiceSpeaking(_ptr, steamIDUser, bSpeaking);
			}
			public virtual void /*void*/ ISteamFriends_ActivateGameOverlay( string /*const char **/ pchDialog )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamFriends _ptr is null!" );
				
				Native.SteamAPI_ISteamFriends_ActivateGameOverlay(_ptr, pchDialog);
			}
			public virtual void /*void*/ ISteamFriends_ActivateGameOverlayToUser( string /*const char **/ pchDialog, ulong steamID )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamFriends _ptr is null!" );
				
				Native.SteamAPI_ISteamFriends_ActivateGameOverlayToUser(_ptr, pchDialog, steamID);
			}
			public virtual void /*void*/ ISteamFriends_ActivateGameOverlayToWebPage( string /*const char **/ pchURL, ActivateGameOverlayToWebPageMode /*EActivateGameOverlayToWebPageMode*/ eMode )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamFriends _ptr is null!" );
				
				Native.SteamAPI_ISteamFriends_ActivateGameOverlayToWebPage(_ptr, pchURL, eMode);
			}
			public virtual void /*void*/ ISteamFriends_ActivateGameOverlayToStore( uint nAppID, OverlayToStoreFlag /*EOverlayToStoreFlag*/ eFlag )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamFriends _ptr is null!" );
				
				Native.SteamAPI_ISteamFriends_ActivateGameOverlayToStore(_ptr, nAppID, eFlag);
			}
			public virtual void /*void*/ ISteamFriends_SetPlayedWith( ulong steamIDUserPlayedWith )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamFriends _ptr is null!" );
				
				Native.SteamAPI_ISteamFriends_SetPlayedWith(_ptr, steamIDUserPlayedWith);
			}
			public virtual void /*void*/ ISteamFriends_ActivateGameOverlayInviteDialog( ulong steamIDLobby )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamFriends _ptr is null!" );
				
				Native.SteamAPI_ISteamFriends_ActivateGameOverlayInviteDialog(_ptr, steamIDLobby);
			}
			public virtual int /*int*/ ISteamFriends_GetSmallFriendAvatar( ulong steamIDFriend )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamFriends _ptr is null!" );
				
				return Native.SteamAPI_ISteamFriends_GetSmallFriendAvatar(_ptr, steamIDFriend);
			}
			public virtual int /*int*/ ISteamFriends_GetMediumFriendAvatar( ulong steamIDFriend )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamFriends _ptr is null!" );
				
				return Native.SteamAPI_ISteamFriends_GetMediumFriendAvatar(_ptr, steamIDFriend);
			}
			public virtual int /*int*/ ISteamFriends_GetLargeFriendAvatar( ulong steamIDFriend )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamFriends _ptr is null!" );
				
				return Native.SteamAPI_ISteamFriends_GetLargeFriendAvatar(_ptr, steamIDFriend);
			}
			public virtual bool /*bool*/ ISteamFriends_RequestUserInformation( ulong steamIDUser, [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bRequireNameOnly )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamFriends _ptr is null!" );
				
				return Native.SteamAPI_ISteamFriends_RequestUserInformation(_ptr, steamIDUser, bRequireNameOnly);
			}
			public virtual SteamAPICall_t /*(SteamAPICall_t)*/ ISteamFriends_RequestClanOfficerList( ulong steamIDClan )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamFriends _ptr is null!" );
				
				return Native.SteamAPI_ISteamFriends_RequestClanOfficerList(_ptr, steamIDClan);
			}
			public virtual CSteamID /*(class CSteamID)*/ ISteamFriends_GetClanOwner( ulong steamIDClan )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamFriends _ptr is null!" );
				
				return Native.SteamAPI_ISteamFriends_GetClanOwner(_ptr, steamIDClan);
			}
			public virtual int /*int*/ ISteamFriends_GetClanOfficerCount( ulong steamIDClan )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamFriends _ptr is null!" );
				
				return Native.SteamAPI_ISteamFriends_GetClanOfficerCount(_ptr, steamIDClan);
			}
			public virtual CSteamID /*(class CSteamID)*/ ISteamFriends_GetClanOfficerByIndex( ulong steamIDClan, int /*int*/ iOfficer )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamFriends _ptr is null!" );
				
				return Native.SteamAPI_ISteamFriends_GetClanOfficerByIndex(_ptr, steamIDClan, iOfficer);
			}
			public virtual uint /*uint32*/ ISteamFriends_GetUserRestrictions()
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamFriends _ptr is null!" );
				
				return Native.SteamAPI_ISteamFriends_GetUserRestrictions(_ptr);
			}
			public virtual bool /*bool*/ ISteamFriends_SetRichPresence( string /*const char **/ pchKey, string /*const char **/ pchValue )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamFriends _ptr is null!" );
				
				return Native.SteamAPI_ISteamFriends_SetRichPresence(_ptr, pchKey, pchValue);
			}
			public virtual void /*void*/ ISteamFriends_ClearRichPresence()
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamFriends _ptr is null!" );
				
				Native.SteamAPI_ISteamFriends_ClearRichPresence(_ptr);
			}
			public virtual IntPtr ISteamFriends_GetFriendRichPresence( ulong steamIDFriend, string /*const char **/ pchKey )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamFriends _ptr is null!" );
				
				return Native.SteamAPI_ISteamFriends_GetFriendRichPresence(_ptr, steamIDFriend, pchKey);
			}
			public virtual int /*int*/ ISteamFriends_GetFriendRichPresenceKeyCount( ulong steamIDFriend )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamFriends _ptr is null!" );
				
				return Native.SteamAPI_ISteamFriends_GetFriendRichPresenceKeyCount(_ptr, steamIDFriend);
			}
			public virtual IntPtr ISteamFriends_GetFriendRichPresenceKeyByIndex( ulong steamIDFriend, int /*int*/ iKey )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamFriends _ptr is null!" );
				
				return Native.SteamAPI_ISteamFriends_GetFriendRichPresenceKeyByIndex(_ptr, steamIDFriend, iKey);
			}
			public virtual void /*void*/ ISteamFriends_RequestFriendRichPresence( ulong steamIDFriend )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamFriends _ptr is null!" );
				
				Native.SteamAPI_ISteamFriends_RequestFriendRichPresence(_ptr, steamIDFriend);
			}
			public virtual bool /*bool*/ ISteamFriends_InviteUserToGame( ulong steamIDFriend, string /*const char **/ pchConnectString )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamFriends _ptr is null!" );
				
				return Native.SteamAPI_ISteamFriends_InviteUserToGame(_ptr, steamIDFriend, pchConnectString);
			}
			public virtual int /*int*/ ISteamFriends_GetCoplayFriendCount()
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamFriends _ptr is null!" );
				
				return Native.SteamAPI_ISteamFriends_GetCoplayFriendCount(_ptr);
			}
			public virtual CSteamID /*(class CSteamID)*/ ISteamFriends_GetCoplayFriend( int /*int*/ iCoplayFriend )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamFriends _ptr is null!" );
				
				return Native.SteamAPI_ISteamFriends_GetCoplayFriend(_ptr, iCoplayFriend);
			}
			public virtual int /*int*/ ISteamFriends_GetFriendCoplayTime( ulong steamIDFriend )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamFriends _ptr is null!" );
				
				return Native.SteamAPI_ISteamFriends_GetFriendCoplayTime(_ptr, steamIDFriend);
			}
			public virtual AppId_t /*(AppId_t)*/ ISteamFriends_GetFriendCoplayGame( ulong steamIDFriend )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamFriends _ptr is null!" );
				
				return Native.SteamAPI_ISteamFriends_GetFriendCoplayGame(_ptr, steamIDFriend);
			}
			public virtual SteamAPICall_t /*(SteamAPICall_t)*/ ISteamFriends_JoinClanChatRoom( ulong steamIDClan )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamFriends _ptr is null!" );
				
				return Native.SteamAPI_ISteamFriends_JoinClanChatRoom(_ptr, steamIDClan);
			}
			public virtual bool /*bool*/ ISteamFriends_LeaveClanChatRoom( ulong steamIDClan )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamFriends _ptr is null!" );
				
				return Native.SteamAPI_ISteamFriends_LeaveClanChatRoom(_ptr, steamIDClan);
			}
			public virtual int /*int*/ ISteamFriends_GetClanChatMemberCount( ulong steamIDClan )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamFriends _ptr is null!" );
				
				return Native.SteamAPI_ISteamFriends_GetClanChatMemberCount(_ptr, steamIDClan);
			}
			public virtual CSteamID /*(class CSteamID)*/ ISteamFriends_GetChatMemberByIndex( ulong steamIDClan, int /*int*/ iUser )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamFriends _ptr is null!" );
				
				return Native.SteamAPI_ISteamFriends_GetChatMemberByIndex(_ptr, steamIDClan, iUser);
			}
			public virtual bool /*bool*/ ISteamFriends_SendClanChatMessage( ulong steamIDClanChat, string /*const char **/ pchText )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamFriends _ptr is null!" );
				
				return Native.SteamAPI_ISteamFriends_SendClanChatMessage(_ptr, steamIDClanChat, pchText);
			}
			public virtual int /*int*/ ISteamFriends_GetClanChatMessage( ulong steamIDClanChat, int /*int*/ iMessage, IntPtr /*void **/ prgchText, int /*int*/ cchTextMax, out ChatEntryType /*EChatEntryType **/ peChatEntryType, out ulong psteamidChatter )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamFriends _ptr is null!" );
				
				return Native.SteamAPI_ISteamFriends_GetClanChatMessage(_ptr, steamIDClanChat, iMessage, prgchText, cchTextMax, out peChatEntryType, out psteamidChatter);
			}
			public virtual bool /*bool*/ ISteamFriends_IsClanChatAdmin( ulong steamIDClanChat, ulong steamIDUser )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamFriends _ptr is null!" );
				
				return Native.SteamAPI_ISteamFriends_IsClanChatAdmin(_ptr, steamIDClanChat, steamIDUser);
			}
			public virtual bool /*bool*/ ISteamFriends_IsClanChatWindowOpenInSteam( ulong steamIDClanChat )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamFriends _ptr is null!" );
				
				return Native.SteamAPI_ISteamFriends_IsClanChatWindowOpenInSteam(_ptr, steamIDClanChat);
			}
			public virtual bool /*bool*/ ISteamFriends_OpenClanChatWindowInSteam( ulong steamIDClanChat )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamFriends _ptr is null!" );
				
				return Native.SteamAPI_ISteamFriends_OpenClanChatWindowInSteam(_ptr, steamIDClanChat);
			}
			public virtual bool /*bool*/ ISteamFriends_CloseClanChatWindowInSteam( ulong steamIDClanChat )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamFriends _ptr is null!" );
				
				return Native.SteamAPI_ISteamFriends_CloseClanChatWindowInSteam(_ptr, steamIDClanChat);
			}
			public virtual bool /*bool*/ ISteamFriends_SetListenForFriendsMessages( [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bInterceptEnabled )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamFriends _ptr is null!" );
				
				return Native.SteamAPI_ISteamFriends_SetListenForFriendsMessages(_ptr, bInterceptEnabled);
			}
			public virtual bool /*bool*/ ISteamFriends_ReplyToFriendMessage( ulong steamIDFriend, string /*const char **/ pchMsgToSend )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamFriends _ptr is null!" );
				
				return Native.SteamAPI_ISteamFriends_ReplyToFriendMessage(_ptr, steamIDFriend, pchMsgToSend);
			}
			public virtual int /*int*/ ISteamFriends_GetFriendMessage( ulong steamIDFriend, int /*int*/ iMessageID, IntPtr /*void **/ pvData, int /*int*/ cubData, out ChatEntryType /*EChatEntryType **/ peChatEntryType )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamFriends _ptr is null!" );
				
				return Native.SteamAPI_ISteamFriends_GetFriendMessage(_ptr, steamIDFriend, iMessageID, pvData, cubData, out peChatEntryType);
			}
			public virtual SteamAPICall_t /*(SteamAPICall_t)*/ ISteamFriends_GetFollowerCount( ulong steamID )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamFriends _ptr is null!" );
				
				return Native.SteamAPI_ISteamFriends_GetFollowerCount(_ptr, steamID);
			}
			public virtual SteamAPICall_t /*(SteamAPICall_t)*/ ISteamFriends_IsFollowing( ulong steamID )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamFriends _ptr is null!" );
				
				return Native.SteamAPI_ISteamFriends_IsFollowing(_ptr, steamID);
			}
			public virtual SteamAPICall_t /*(SteamAPICall_t)*/ ISteamFriends_EnumerateFollowingList( uint /*uint32*/ unStartIndex )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamFriends _ptr is null!" );
				
				return Native.SteamAPI_ISteamFriends_EnumerateFollowingList(_ptr, unStartIndex);
			}
			public virtual bool /*bool*/ ISteamFriends_IsClanPublic( ulong steamIDClan )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamFriends _ptr is null!" );
				
				return Native.SteamAPI_ISteamFriends_IsClanPublic(_ptr, steamIDClan);
			}
			public virtual bool /*bool*/ ISteamFriends_IsClanOfficialGameGroup( ulong steamIDClan )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamFriends _ptr is null!" );
				
				return Native.SteamAPI_ISteamFriends_IsClanOfficialGameGroup(_ptr, steamIDClan);
			}
			public virtual int /*int*/ ISteamFriends_GetNumChatsWithUnreadPriorityMessages()
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamFriends _ptr is null!" );
				
				return Native.SteamAPI_ISteamFriends_GetNumChatsWithUnreadPriorityMessages(_ptr);
			}
			
			public virtual uint /*uint32*/ ISteamUtils_GetSecondsSinceAppActive()
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamUtils _ptr is null!" );
				
				return Native.SteamAPI_ISteamUtils_GetSecondsSinceAppActive(_ptr);
			}
			public virtual uint /*uint32*/ ISteamUtils_GetSecondsSinceComputerActive()
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamUtils _ptr is null!" );
				
				return Native.SteamAPI_ISteamUtils_GetSecondsSinceComputerActive(_ptr);
			}
			public virtual Universe /*EUniverse*/ ISteamUtils_GetConnectedUniverse()
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamUtils _ptr is null!" );
				
				return Native.SteamAPI_ISteamUtils_GetConnectedUniverse(_ptr);
			}
			public virtual uint /*uint32*/ ISteamUtils_GetServerRealTime()
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamUtils _ptr is null!" );
				
				return Native.SteamAPI_ISteamUtils_GetServerRealTime(_ptr);
			}
			public virtual IntPtr ISteamUtils_GetIPCountry()
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamUtils _ptr is null!" );
				
				return Native.SteamAPI_ISteamUtils_GetIPCountry(_ptr);
			}
			public virtual bool /*bool*/ ISteamUtils_GetImageSize( int /*int*/ iImage, out uint /*uint32 **/ pnWidth, out uint /*uint32 **/ pnHeight )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamUtils _ptr is null!" );
				
				return Native.SteamAPI_ISteamUtils_GetImageSize(_ptr, iImage, out pnWidth, out pnHeight);
			}
			public virtual bool /*bool*/ ISteamUtils_GetImageRGBA( int /*int*/ iImage, IntPtr /*uint8 **/ pubDest, int /*int*/ nDestBufferSize )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamUtils _ptr is null!" );
				
				return Native.SteamAPI_ISteamUtils_GetImageRGBA(_ptr, iImage, pubDest, nDestBufferSize);
			}
			public virtual bool /*bool*/ ISteamUtils_GetCSERIPPort( out uint /*uint32 **/ unIP, out ushort /*uint16 **/ usPort )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamUtils _ptr is null!" );
				
				return Native.SteamAPI_ISteamUtils_GetCSERIPPort(_ptr, out unIP, out usPort);
			}
			public virtual byte /*uint8*/ ISteamUtils_GetCurrentBatteryPower()
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamUtils _ptr is null!" );
				
				return Native.SteamAPI_ISteamUtils_GetCurrentBatteryPower(_ptr);
			}
			public virtual uint /*uint32*/ ISteamUtils_GetAppID()
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamUtils _ptr is null!" );
				
				return Native.SteamAPI_ISteamUtils_GetAppID(_ptr);
			}
			public virtual void /*void*/ ISteamUtils_SetOverlayNotificationPosition( NotificationPosition /*ENotificationPosition*/ eNotificationPosition )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamUtils _ptr is null!" );
				
				Native.SteamAPI_ISteamUtils_SetOverlayNotificationPosition(_ptr, eNotificationPosition);
			}
			public virtual bool /*bool*/ ISteamUtils_IsAPICallCompleted( ulong hSteamAPICall, [MarshalAs(UnmanagedType.U1)] ref bool /*bool **/ pbFailed )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamUtils _ptr is null!" );
				
				return Native.SteamAPI_ISteamUtils_IsAPICallCompleted(_ptr, hSteamAPICall, ref pbFailed);
			}
			public virtual SteamAPICallFailure /*ESteamAPICallFailure*/ ISteamUtils_GetAPICallFailureReason( ulong hSteamAPICall )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamUtils _ptr is null!" );
				
				return Native.SteamAPI_ISteamUtils_GetAPICallFailureReason(_ptr, hSteamAPICall);
			}
			public virtual bool /*bool*/ ISteamUtils_GetAPICallResult( ulong hSteamAPICall, IntPtr /*void **/ pCallback, int /*int*/ cubCallback, int /*int*/ iCallbackExpected, [MarshalAs(UnmanagedType.U1)] ref bool /*bool **/ pbFailed )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamUtils _ptr is null!" );
				
				return Native.SteamAPI_ISteamUtils_GetAPICallResult(_ptr, hSteamAPICall, pCallback, cubCallback, iCallbackExpected, ref pbFailed);
			}
			public virtual uint /*uint32*/ ISteamUtils_GetIPCCallCount()
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamUtils _ptr is null!" );
				
				return Native.SteamAPI_ISteamUtils_GetIPCCallCount(_ptr);
			}
			public virtual void /*void*/ ISteamUtils_SetWarningMessageHook( IntPtr /*SteamAPIWarningMessageHook_t*/ pFunction )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamUtils _ptr is null!" );
				
				Native.SteamAPI_ISteamUtils_SetWarningMessageHook(_ptr, pFunction);
			}
			public virtual bool /*bool*/ ISteamUtils_IsOverlayEnabled()
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamUtils _ptr is null!" );
				
				return Native.SteamAPI_ISteamUtils_IsOverlayEnabled(_ptr);
			}
			public virtual bool /*bool*/ ISteamUtils_BOverlayNeedsPresent()
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamUtils _ptr is null!" );
				
				return Native.SteamAPI_ISteamUtils_BOverlayNeedsPresent(_ptr);
			}
			public virtual SteamAPICall_t /*(SteamAPICall_t)*/ ISteamUtils_CheckFileSignature( string /*const char **/ szFileName )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamUtils _ptr is null!" );
				
				return Native.SteamAPI_ISteamUtils_CheckFileSignature(_ptr, szFileName);
			}
			public virtual bool /*bool*/ ISteamUtils_ShowGamepadTextInput( GamepadTextInputMode /*EGamepadTextInputMode*/ eInputMode, GamepadTextInputLineMode /*EGamepadTextInputLineMode*/ eLineInputMode, string /*const char **/ pchDescription, uint /*uint32*/ unCharMax, string /*const char **/ pchExistingText )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamUtils _ptr is null!" );
				
				return Native.SteamAPI_ISteamUtils_ShowGamepadTextInput(_ptr, eInputMode, eLineInputMode, pchDescription, unCharMax, pchExistingText);
			}
			public virtual uint /*uint32*/ ISteamUtils_GetEnteredGamepadTextLength()
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamUtils _ptr is null!" );
				
				return Native.SteamAPI_ISteamUtils_GetEnteredGamepadTextLength(_ptr);
			}
			public virtual bool /*bool*/ ISteamUtils_GetEnteredGamepadTextInput( System.Text.StringBuilder /*char **/ pchText, uint /*uint32*/ cchText )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamUtils _ptr is null!" );
				
				return Native.SteamAPI_ISteamUtils_GetEnteredGamepadTextInput(_ptr, pchText, cchText);
			}
			public virtual IntPtr ISteamUtils_GetSteamUILanguage()
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamUtils _ptr is null!" );
				
				return Native.SteamAPI_ISteamUtils_GetSteamUILanguage(_ptr);
			}
			public virtual bool /*bool*/ ISteamUtils_IsSteamRunningInVR()
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamUtils _ptr is null!" );
				
				return Native.SteamAPI_ISteamUtils_IsSteamRunningInVR(_ptr);
			}
			public virtual void /*void*/ ISteamUtils_SetOverlayNotificationInset( int /*int*/ nHorizontalInset, int /*int*/ nVerticalInset )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamUtils _ptr is null!" );
				
				Native.SteamAPI_ISteamUtils_SetOverlayNotificationInset(_ptr, nHorizontalInset, nVerticalInset);
			}
			public virtual bool /*bool*/ ISteamUtils_IsSteamInBigPictureMode()
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamUtils _ptr is null!" );
				
				return Native.SteamAPI_ISteamUtils_IsSteamInBigPictureMode(_ptr);
			}
			public virtual void /*void*/ ISteamUtils_StartVRDashboard()
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamUtils _ptr is null!" );
				
				Native.SteamAPI_ISteamUtils_StartVRDashboard(_ptr);
			}
			public virtual bool /*bool*/ ISteamUtils_IsVRHeadsetStreamingEnabled()
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamUtils _ptr is null!" );
				
				return Native.SteamAPI_ISteamUtils_IsVRHeadsetStreamingEnabled(_ptr);
			}
			public virtual void /*void*/ ISteamUtils_SetVRHeadsetStreamingEnabled( [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bEnabled )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamUtils _ptr is null!" );
				
				Native.SteamAPI_ISteamUtils_SetVRHeadsetStreamingEnabled(_ptr, bEnabled);
			}
			
			public virtual int /*int*/ ISteamMatchmaking_GetFavoriteGameCount()
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamMatchmaking _ptr is null!" );
				
				return Native.SteamAPI_ISteamMatchmaking_GetFavoriteGameCount(_ptr);
			}
			public virtual bool /*bool*/ ISteamMatchmaking_GetFavoriteGame( int /*int*/ iGame, ref uint pnAppID, out uint /*uint32 **/ pnIP, out ushort /*uint16 **/ pnConnPort, out ushort /*uint16 **/ pnQueryPort, out uint /*uint32 **/ punFlags, out uint /*uint32 **/ pRTime32LastPlayedOnServer )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamMatchmaking _ptr is null!" );
				
				return Native.SteamAPI_ISteamMatchmaking_GetFavoriteGame(_ptr, iGame, ref pnAppID, out pnIP, out pnConnPort, out pnQueryPort, out punFlags, out pRTime32LastPlayedOnServer);
			}
			public virtual int /*int*/ ISteamMatchmaking_AddFavoriteGame( uint nAppID, uint /*uint32*/ nIP, ushort /*uint16*/ nConnPort, ushort /*uint16*/ nQueryPort, uint /*uint32*/ unFlags, uint /*uint32*/ rTime32LastPlayedOnServer )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamMatchmaking _ptr is null!" );
				
				return Native.SteamAPI_ISteamMatchmaking_AddFavoriteGame(_ptr, nAppID, nIP, nConnPort, nQueryPort, unFlags, rTime32LastPlayedOnServer);
			}
			public virtual bool /*bool*/ ISteamMatchmaking_RemoveFavoriteGame( uint nAppID, uint /*uint32*/ nIP, ushort /*uint16*/ nConnPort, ushort /*uint16*/ nQueryPort, uint /*uint32*/ unFlags )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamMatchmaking _ptr is null!" );
				
				return Native.SteamAPI_ISteamMatchmaking_RemoveFavoriteGame(_ptr, nAppID, nIP, nConnPort, nQueryPort, unFlags);
			}
			public virtual SteamAPICall_t /*(SteamAPICall_t)*/ ISteamMatchmaking_RequestLobbyList()
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamMatchmaking _ptr is null!" );
				
				return Native.SteamAPI_ISteamMatchmaking_RequestLobbyList(_ptr);
			}
			public virtual void /*void*/ ISteamMatchmaking_AddRequestLobbyListStringFilter( string /*const char **/ pchKeyToMatch, string /*const char **/ pchValueToMatch, LobbyComparison /*ELobbyComparison*/ eComparisonType )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamMatchmaking _ptr is null!" );
				
				Native.SteamAPI_ISteamMatchmaking_AddRequestLobbyListStringFilter(_ptr, pchKeyToMatch, pchValueToMatch, eComparisonType);
			}
			public virtual void /*void*/ ISteamMatchmaking_AddRequestLobbyListNumericalFilter( string /*const char **/ pchKeyToMatch, int /*int*/ nValueToMatch, LobbyComparison /*ELobbyComparison*/ eComparisonType )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamMatchmaking _ptr is null!" );
				
				Native.SteamAPI_ISteamMatchmaking_AddRequestLobbyListNumericalFilter(_ptr, pchKeyToMatch, nValueToMatch, eComparisonType);
			}
			public virtual void /*void*/ ISteamMatchmaking_AddRequestLobbyListNearValueFilter( string /*const char **/ pchKeyToMatch, int /*int*/ nValueToBeCloseTo )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamMatchmaking _ptr is null!" );
				
				Native.SteamAPI_ISteamMatchmaking_AddRequestLobbyListNearValueFilter(_ptr, pchKeyToMatch, nValueToBeCloseTo);
			}
			public virtual void /*void*/ ISteamMatchmaking_AddRequestLobbyListFilterSlotsAvailable( int /*int*/ nSlotsAvailable )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamMatchmaking _ptr is null!" );
				
				Native.SteamAPI_ISteamMatchmaking_AddRequestLobbyListFilterSlotsAvailable(_ptr, nSlotsAvailable);
			}
			public virtual void /*void*/ ISteamMatchmaking_AddRequestLobbyListDistanceFilter( LobbyDistanceFilter /*ELobbyDistanceFilter*/ eLobbyDistanceFilter )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamMatchmaking _ptr is null!" );
				
				Native.SteamAPI_ISteamMatchmaking_AddRequestLobbyListDistanceFilter(_ptr, eLobbyDistanceFilter);
			}
			public virtual void /*void*/ ISteamMatchmaking_AddRequestLobbyListResultCountFilter( int /*int*/ cMaxResults )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamMatchmaking _ptr is null!" );
				
				Native.SteamAPI_ISteamMatchmaking_AddRequestLobbyListResultCountFilter(_ptr, cMaxResults);
			}
			public virtual void /*void*/ ISteamMatchmaking_AddRequestLobbyListCompatibleMembersFilter( ulong steamIDLobby )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamMatchmaking _ptr is null!" );
				
				Native.SteamAPI_ISteamMatchmaking_AddRequestLobbyListCompatibleMembersFilter(_ptr, steamIDLobby);
			}
			public virtual CSteamID /*(class CSteamID)*/ ISteamMatchmaking_GetLobbyByIndex( int /*int*/ iLobby )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamMatchmaking _ptr is null!" );
				
				return Native.SteamAPI_ISteamMatchmaking_GetLobbyByIndex(_ptr, iLobby);
			}
			public virtual SteamAPICall_t /*(SteamAPICall_t)*/ ISteamMatchmaking_CreateLobby( LobbyType /*ELobbyType*/ eLobbyType, int /*int*/ cMaxMembers )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamMatchmaking _ptr is null!" );
				
				return Native.SteamAPI_ISteamMatchmaking_CreateLobby(_ptr, eLobbyType, cMaxMembers);
			}
			public virtual SteamAPICall_t /*(SteamAPICall_t)*/ ISteamMatchmaking_JoinLobby( ulong steamIDLobby )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamMatchmaking _ptr is null!" );
				
				return Native.SteamAPI_ISteamMatchmaking_JoinLobby(_ptr, steamIDLobby);
			}
			public virtual void /*void*/ ISteamMatchmaking_LeaveLobby( ulong steamIDLobby )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamMatchmaking _ptr is null!" );
				
				Native.SteamAPI_ISteamMatchmaking_LeaveLobby(_ptr, steamIDLobby);
			}
			public virtual bool /*bool*/ ISteamMatchmaking_InviteUserToLobby( ulong steamIDLobby, ulong steamIDInvitee )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamMatchmaking _ptr is null!" );
				
				return Native.SteamAPI_ISteamMatchmaking_InviteUserToLobby(_ptr, steamIDLobby, steamIDInvitee);
			}
			public virtual int /*int*/ ISteamMatchmaking_GetNumLobbyMembers( ulong steamIDLobby )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamMatchmaking _ptr is null!" );
				
				return Native.SteamAPI_ISteamMatchmaking_GetNumLobbyMembers(_ptr, steamIDLobby);
			}
			public virtual CSteamID /*(class CSteamID)*/ ISteamMatchmaking_GetLobbyMemberByIndex( ulong steamIDLobby, int /*int*/ iMember )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamMatchmaking _ptr is null!" );
				
				return Native.SteamAPI_ISteamMatchmaking_GetLobbyMemberByIndex(_ptr, steamIDLobby, iMember);
			}
			public virtual IntPtr ISteamMatchmaking_GetLobbyData( ulong steamIDLobby, string /*const char **/ pchKey )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamMatchmaking _ptr is null!" );
				
				return Native.SteamAPI_ISteamMatchmaking_GetLobbyData(_ptr, steamIDLobby, pchKey);
			}
			public virtual bool /*bool*/ ISteamMatchmaking_SetLobbyData( ulong steamIDLobby, string /*const char **/ pchKey, string /*const char **/ pchValue )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamMatchmaking _ptr is null!" );
				
				return Native.SteamAPI_ISteamMatchmaking_SetLobbyData(_ptr, steamIDLobby, pchKey, pchValue);
			}
			public virtual int /*int*/ ISteamMatchmaking_GetLobbyDataCount( ulong steamIDLobby )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamMatchmaking _ptr is null!" );
				
				return Native.SteamAPI_ISteamMatchmaking_GetLobbyDataCount(_ptr, steamIDLobby);
			}
			public virtual bool /*bool*/ ISteamMatchmaking_GetLobbyDataByIndex( ulong steamIDLobby, int /*int*/ iLobbyData, System.Text.StringBuilder /*char **/ pchKey, int /*int*/ cchKeyBufferSize, System.Text.StringBuilder /*char **/ pchValue, int /*int*/ cchValueBufferSize )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamMatchmaking _ptr is null!" );
				
				return Native.SteamAPI_ISteamMatchmaking_GetLobbyDataByIndex(_ptr, steamIDLobby, iLobbyData, pchKey, cchKeyBufferSize, pchValue, cchValueBufferSize);
			}
			public virtual bool /*bool*/ ISteamMatchmaking_DeleteLobbyData( ulong steamIDLobby, string /*const char **/ pchKey )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamMatchmaking _ptr is null!" );
				
				return Native.SteamAPI_ISteamMatchmaking_DeleteLobbyData(_ptr, steamIDLobby, pchKey);
			}
			public virtual IntPtr ISteamMatchmaking_GetLobbyMemberData( ulong steamIDLobby, ulong steamIDUser, string /*const char **/ pchKey )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamMatchmaking _ptr is null!" );
				
				return Native.SteamAPI_ISteamMatchmaking_GetLobbyMemberData(_ptr, steamIDLobby, steamIDUser, pchKey);
			}
			public virtual void /*void*/ ISteamMatchmaking_SetLobbyMemberData( ulong steamIDLobby, string /*const char **/ pchKey, string /*const char **/ pchValue )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamMatchmaking _ptr is null!" );
				
				Native.SteamAPI_ISteamMatchmaking_SetLobbyMemberData(_ptr, steamIDLobby, pchKey, pchValue);
			}
			public virtual bool /*bool*/ ISteamMatchmaking_SendLobbyChatMsg( ulong steamIDLobby, IntPtr /*const void **/ pvMsgBody, int /*int*/ cubMsgBody )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamMatchmaking _ptr is null!" );
				
				return Native.SteamAPI_ISteamMatchmaking_SendLobbyChatMsg(_ptr, steamIDLobby, pvMsgBody, cubMsgBody);
			}
			public virtual int /*int*/ ISteamMatchmaking_GetLobbyChatEntry( ulong steamIDLobby, int /*int*/ iChatID, out ulong pSteamIDUser, IntPtr /*void **/ pvData, int /*int*/ cubData, out ChatEntryType /*EChatEntryType **/ peChatEntryType )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamMatchmaking _ptr is null!" );
				
				return Native.SteamAPI_ISteamMatchmaking_GetLobbyChatEntry(_ptr, steamIDLobby, iChatID, out pSteamIDUser, pvData, cubData, out peChatEntryType);
			}
			public virtual bool /*bool*/ ISteamMatchmaking_RequestLobbyData( ulong steamIDLobby )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamMatchmaking _ptr is null!" );
				
				return Native.SteamAPI_ISteamMatchmaking_RequestLobbyData(_ptr, steamIDLobby);
			}
			public virtual void /*void*/ ISteamMatchmaking_SetLobbyGameServer( ulong steamIDLobby, uint /*uint32*/ unGameServerIP, ushort /*uint16*/ unGameServerPort, ulong steamIDGameServer )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamMatchmaking _ptr is null!" );
				
				Native.SteamAPI_ISteamMatchmaking_SetLobbyGameServer(_ptr, steamIDLobby, unGameServerIP, unGameServerPort, steamIDGameServer);
			}
			public virtual bool /*bool*/ ISteamMatchmaking_GetLobbyGameServer( ulong steamIDLobby, out uint /*uint32 **/ punGameServerIP, out ushort /*uint16 **/ punGameServerPort, out ulong psteamIDGameServer )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamMatchmaking _ptr is null!" );
				
				return Native.SteamAPI_ISteamMatchmaking_GetLobbyGameServer(_ptr, steamIDLobby, out punGameServerIP, out punGameServerPort, out psteamIDGameServer);
			}
			public virtual bool /*bool*/ ISteamMatchmaking_SetLobbyMemberLimit( ulong steamIDLobby, int /*int*/ cMaxMembers )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamMatchmaking _ptr is null!" );
				
				return Native.SteamAPI_ISteamMatchmaking_SetLobbyMemberLimit(_ptr, steamIDLobby, cMaxMembers);
			}
			public virtual int /*int*/ ISteamMatchmaking_GetLobbyMemberLimit( ulong steamIDLobby )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamMatchmaking _ptr is null!" );
				
				return Native.SteamAPI_ISteamMatchmaking_GetLobbyMemberLimit(_ptr, steamIDLobby);
			}
			public virtual bool /*bool*/ ISteamMatchmaking_SetLobbyType( ulong steamIDLobby, LobbyType /*ELobbyType*/ eLobbyType )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamMatchmaking _ptr is null!" );
				
				return Native.SteamAPI_ISteamMatchmaking_SetLobbyType(_ptr, steamIDLobby, eLobbyType);
			}
			public virtual bool /*bool*/ ISteamMatchmaking_SetLobbyJoinable( ulong steamIDLobby, [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bLobbyJoinable )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamMatchmaking _ptr is null!" );
				
				return Native.SteamAPI_ISteamMatchmaking_SetLobbyJoinable(_ptr, steamIDLobby, bLobbyJoinable);
			}
			public virtual CSteamID /*(class CSteamID)*/ ISteamMatchmaking_GetLobbyOwner( ulong steamIDLobby )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamMatchmaking _ptr is null!" );
				
				return Native.SteamAPI_ISteamMatchmaking_GetLobbyOwner(_ptr, steamIDLobby);
			}
			public virtual bool /*bool*/ ISteamMatchmaking_SetLobbyOwner( ulong steamIDLobby, ulong steamIDNewOwner )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamMatchmaking _ptr is null!" );
				
				return Native.SteamAPI_ISteamMatchmaking_SetLobbyOwner(_ptr, steamIDLobby, steamIDNewOwner);
			}
			public virtual bool /*bool*/ ISteamMatchmaking_SetLinkedLobby( ulong steamIDLobby, ulong steamIDLobbyDependent )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamMatchmaking _ptr is null!" );
				
				return Native.SteamAPI_ISteamMatchmaking_SetLinkedLobby(_ptr, steamIDLobby, steamIDLobbyDependent);
			}
			
			public virtual HServerListRequest /*(HServerListRequest)*/ ISteamMatchmakingServers_RequestInternetServerList( uint iApp, IntPtr /*struct MatchMakingKeyValuePair_t ***/ ppchFilters, uint /*uint32*/ nFilters, IntPtr /*class ISteamMatchmakingServerListResponse **/ pRequestServersResponse )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamMatchmakingServers _ptr is null!" );
				
				return Native.SteamAPI_ISteamMatchmakingServers_RequestInternetServerList(_ptr, iApp, ppchFilters, nFilters, pRequestServersResponse);
			}
			public virtual HServerListRequest /*(HServerListRequest)*/ ISteamMatchmakingServers_RequestLANServerList( uint iApp, IntPtr /*class ISteamMatchmakingServerListResponse **/ pRequestServersResponse )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamMatchmakingServers _ptr is null!" );
				
				return Native.SteamAPI_ISteamMatchmakingServers_RequestLANServerList(_ptr, iApp, pRequestServersResponse);
			}
			public virtual HServerListRequest /*(HServerListRequest)*/ ISteamMatchmakingServers_RequestFriendsServerList( uint iApp, IntPtr /*struct MatchMakingKeyValuePair_t ***/ ppchFilters, uint /*uint32*/ nFilters, IntPtr /*class ISteamMatchmakingServerListResponse **/ pRequestServersResponse )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamMatchmakingServers _ptr is null!" );
				
				return Native.SteamAPI_ISteamMatchmakingServers_RequestFriendsServerList(_ptr, iApp, ppchFilters, nFilters, pRequestServersResponse);
			}
			public virtual HServerListRequest /*(HServerListRequest)*/ ISteamMatchmakingServers_RequestFavoritesServerList( uint iApp, IntPtr /*struct MatchMakingKeyValuePair_t ***/ ppchFilters, uint /*uint32*/ nFilters, IntPtr /*class ISteamMatchmakingServerListResponse **/ pRequestServersResponse )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamMatchmakingServers _ptr is null!" );
				
				return Native.SteamAPI_ISteamMatchmakingServers_RequestFavoritesServerList(_ptr, iApp, ppchFilters, nFilters, pRequestServersResponse);
			}
			public virtual HServerListRequest /*(HServerListRequest)*/ ISteamMatchmakingServers_RequestHistoryServerList( uint iApp, IntPtr /*struct MatchMakingKeyValuePair_t ***/ ppchFilters, uint /*uint32*/ nFilters, IntPtr /*class ISteamMatchmakingServerListResponse **/ pRequestServersResponse )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamMatchmakingServers _ptr is null!" );
				
				return Native.SteamAPI_ISteamMatchmakingServers_RequestHistoryServerList(_ptr, iApp, ppchFilters, nFilters, pRequestServersResponse);
			}
			public virtual HServerListRequest /*(HServerListRequest)*/ ISteamMatchmakingServers_RequestSpectatorServerList( uint iApp, IntPtr /*struct MatchMakingKeyValuePair_t ***/ ppchFilters, uint /*uint32*/ nFilters, IntPtr /*class ISteamMatchmakingServerListResponse **/ pRequestServersResponse )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamMatchmakingServers _ptr is null!" );
				
				return Native.SteamAPI_ISteamMatchmakingServers_RequestSpectatorServerList(_ptr, iApp, ppchFilters, nFilters, pRequestServersResponse);
			}
			public virtual void /*void*/ ISteamMatchmakingServers_ReleaseRequest( IntPtr hServerListRequest )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamMatchmakingServers _ptr is null!" );
				
				Native.SteamAPI_ISteamMatchmakingServers_ReleaseRequest(_ptr, hServerListRequest);
			}
			public virtual IntPtr /*class gameserveritem_t **/ ISteamMatchmakingServers_GetServerDetails( IntPtr hRequest, int /*int*/ iServer )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamMatchmakingServers _ptr is null!" );
				
				return Native.SteamAPI_ISteamMatchmakingServers_GetServerDetails(_ptr, hRequest, iServer);
			}
			public virtual void /*void*/ ISteamMatchmakingServers_CancelQuery( IntPtr hRequest )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamMatchmakingServers _ptr is null!" );
				
				Native.SteamAPI_ISteamMatchmakingServers_CancelQuery(_ptr, hRequest);
			}
			public virtual void /*void*/ ISteamMatchmakingServers_RefreshQuery( IntPtr hRequest )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamMatchmakingServers _ptr is null!" );
				
				Native.SteamAPI_ISteamMatchmakingServers_RefreshQuery(_ptr, hRequest);
			}
			public virtual bool /*bool*/ ISteamMatchmakingServers_IsRefreshing( IntPtr hRequest )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamMatchmakingServers _ptr is null!" );
				
				return Native.SteamAPI_ISteamMatchmakingServers_IsRefreshing(_ptr, hRequest);
			}
			public virtual int /*int*/ ISteamMatchmakingServers_GetServerCount( IntPtr hRequest )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamMatchmakingServers _ptr is null!" );
				
				return Native.SteamAPI_ISteamMatchmakingServers_GetServerCount(_ptr, hRequest);
			}
			public virtual void /*void*/ ISteamMatchmakingServers_RefreshServer( IntPtr hRequest, int /*int*/ iServer )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamMatchmakingServers _ptr is null!" );
				
				Native.SteamAPI_ISteamMatchmakingServers_RefreshServer(_ptr, hRequest, iServer);
			}
			public virtual HServerQuery /*(HServerQuery)*/ ISteamMatchmakingServers_PingServer( uint /*uint32*/ unIP, ushort /*uint16*/ usPort, IntPtr /*class ISteamMatchmakingPingResponse **/ pRequestServersResponse )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamMatchmakingServers _ptr is null!" );
				
				return Native.SteamAPI_ISteamMatchmakingServers_PingServer(_ptr, unIP, usPort, pRequestServersResponse);
			}
			public virtual HServerQuery /*(HServerQuery)*/ ISteamMatchmakingServers_PlayerDetails( uint /*uint32*/ unIP, ushort /*uint16*/ usPort, IntPtr /*class ISteamMatchmakingPlayersResponse **/ pRequestServersResponse )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamMatchmakingServers _ptr is null!" );
				
				return Native.SteamAPI_ISteamMatchmakingServers_PlayerDetails(_ptr, unIP, usPort, pRequestServersResponse);
			}
			public virtual HServerQuery /*(HServerQuery)*/ ISteamMatchmakingServers_ServerRules( uint /*uint32*/ unIP, ushort /*uint16*/ usPort, IntPtr /*class ISteamMatchmakingRulesResponse **/ pRequestServersResponse )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamMatchmakingServers _ptr is null!" );
				
				return Native.SteamAPI_ISteamMatchmakingServers_ServerRules(_ptr, unIP, usPort, pRequestServersResponse);
			}
			public virtual void /*void*/ ISteamMatchmakingServers_CancelServerQuery( int hServerQuery )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamMatchmakingServers _ptr is null!" );
				
				Native.SteamAPI_ISteamMatchmakingServers_CancelServerQuery(_ptr, hServerQuery);
			}
			
			public virtual GameSearchErrorCode_t /*EGameSearchErrorCode_t*/ ISteamGameSearch_AddGameSearchParams( string /*const char **/ pchKeyToFind, string /*const char **/ pchValuesToFind )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamGameSearch _ptr is null!" );
				
				return Native.SteamAPI_ISteamGameSearch_AddGameSearchParams(_ptr, pchKeyToFind, pchValuesToFind);
			}
			public virtual GameSearchErrorCode_t /*EGameSearchErrorCode_t*/ ISteamGameSearch_SearchForGameWithLobby( ulong steamIDLobby, int /*int*/ nPlayerMin, int /*int*/ nPlayerMax )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamGameSearch _ptr is null!" );
				
				return Native.SteamAPI_ISteamGameSearch_SearchForGameWithLobby(_ptr, steamIDLobby, nPlayerMin, nPlayerMax);
			}
			public virtual GameSearchErrorCode_t /*EGameSearchErrorCode_t*/ ISteamGameSearch_SearchForGameSolo( int /*int*/ nPlayerMin, int /*int*/ nPlayerMax )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamGameSearch _ptr is null!" );
				
				return Native.SteamAPI_ISteamGameSearch_SearchForGameSolo(_ptr, nPlayerMin, nPlayerMax);
			}
			public virtual GameSearchErrorCode_t /*EGameSearchErrorCode_t*/ ISteamGameSearch_AcceptGame()
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamGameSearch _ptr is null!" );
				
				return Native.SteamAPI_ISteamGameSearch_AcceptGame(_ptr);
			}
			public virtual GameSearchErrorCode_t /*EGameSearchErrorCode_t*/ ISteamGameSearch_DeclineGame()
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamGameSearch _ptr is null!" );
				
				return Native.SteamAPI_ISteamGameSearch_DeclineGame(_ptr);
			}
			public virtual GameSearchErrorCode_t /*EGameSearchErrorCode_t*/ ISteamGameSearch_RetrieveConnectionDetails( ulong steamIDHost, System.Text.StringBuilder /*char **/ pchConnectionDetails, int /*int*/ cubConnectionDetails )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamGameSearch _ptr is null!" );
				
				return Native.SteamAPI_ISteamGameSearch_RetrieveConnectionDetails(_ptr, steamIDHost, pchConnectionDetails, cubConnectionDetails);
			}
			public virtual GameSearchErrorCode_t /*EGameSearchErrorCode_t*/ ISteamGameSearch_EndGameSearch()
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamGameSearch _ptr is null!" );
				
				return Native.SteamAPI_ISteamGameSearch_EndGameSearch(_ptr);
			}
			public virtual GameSearchErrorCode_t /*EGameSearchErrorCode_t*/ ISteamGameSearch_SetGameHostParams( string /*const char **/ pchKey, string /*const char **/ pchValue )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamGameSearch _ptr is null!" );
				
				return Native.SteamAPI_ISteamGameSearch_SetGameHostParams(_ptr, pchKey, pchValue);
			}
			public virtual GameSearchErrorCode_t /*EGameSearchErrorCode_t*/ ISteamGameSearch_SetConnectionDetails( string /*const char **/ pchConnectionDetails, int /*int*/ cubConnectionDetails )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamGameSearch _ptr is null!" );
				
				return Native.SteamAPI_ISteamGameSearch_SetConnectionDetails(_ptr, pchConnectionDetails, cubConnectionDetails);
			}
			public virtual GameSearchErrorCode_t /*EGameSearchErrorCode_t*/ ISteamGameSearch_RequestPlayersForGame( int /*int*/ nPlayerMin, int /*int*/ nPlayerMax, int /*int*/ nMaxTeamSize )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamGameSearch _ptr is null!" );
				
				return Native.SteamAPI_ISteamGameSearch_RequestPlayersForGame(_ptr, nPlayerMin, nPlayerMax, nMaxTeamSize);
			}
			public virtual GameSearchErrorCode_t /*EGameSearchErrorCode_t*/ ISteamGameSearch_HostConfirmGameStart( ulong /*uint64*/ ullUniqueGameID )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamGameSearch _ptr is null!" );
				
				return Native.SteamAPI_ISteamGameSearch_HostConfirmGameStart(_ptr, ullUniqueGameID);
			}
			public virtual GameSearchErrorCode_t /*EGameSearchErrorCode_t*/ ISteamGameSearch_CancelRequestPlayersForGame()
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamGameSearch _ptr is null!" );
				
				return Native.SteamAPI_ISteamGameSearch_CancelRequestPlayersForGame(_ptr);
			}
			public virtual GameSearchErrorCode_t /*EGameSearchErrorCode_t*/ ISteamGameSearch_SubmitPlayerResult( ulong /*uint64*/ ullUniqueGameID, ulong steamIDPlayer, PlayerResult_t /*EPlayerResult_t*/ EPlayerResult )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamGameSearch _ptr is null!" );
				
				return Native.SteamAPI_ISteamGameSearch_SubmitPlayerResult(_ptr, ullUniqueGameID, steamIDPlayer, EPlayerResult);
			}
			public virtual GameSearchErrorCode_t /*EGameSearchErrorCode_t*/ ISteamGameSearch_EndGame( ulong /*uint64*/ ullUniqueGameID )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamGameSearch _ptr is null!" );
				
				return Native.SteamAPI_ISteamGameSearch_EndGame(_ptr, ullUniqueGameID);
			}
			
			public virtual uint /*uint32*/ ISteamParties_GetNumActiveBeacons()
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamParties _ptr is null!" );
				
				return Native.SteamAPI_ISteamParties_GetNumActiveBeacons(_ptr);
			}
			public virtual PartyBeaconID_t /*(PartyBeaconID_t)*/ ISteamParties_GetBeaconByIndex( uint /*uint32*/ unIndex )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamParties _ptr is null!" );
				
				return Native.SteamAPI_ISteamParties_GetBeaconByIndex(_ptr, unIndex);
			}
			public virtual bool /*bool*/ ISteamParties_GetBeaconDetails( ulong ulBeaconID, out ulong pSteamIDBeaconOwner, ref SteamPartyBeaconLocation_t /*struct SteamPartyBeaconLocation_t **/ pLocation, System.Text.StringBuilder /*char **/ pchMetadata, int /*int*/ cchMetadata )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamParties _ptr is null!" );
				
				var pLocation_ps = new SteamPartyBeaconLocation_t.PackSmall();
				var ret = Native.SteamAPI_ISteamParties_GetBeaconDetails(_ptr, ulBeaconID, out pSteamIDBeaconOwner, ref pLocation_ps, pchMetadata, cchMetadata);
				pLocation = pLocation_ps;
				return ret;
			}
			public virtual SteamAPICall_t /*(SteamAPICall_t)*/ ISteamParties_JoinParty( ulong ulBeaconID )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamParties _ptr is null!" );
				
				return Native.SteamAPI_ISteamParties_JoinParty(_ptr, ulBeaconID);
			}
			public virtual bool /*bool*/ ISteamParties_GetNumAvailableBeaconLocations( IntPtr /*uint32 **/ puNumLocations )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamParties _ptr is null!" );
				
				return Native.SteamAPI_ISteamParties_GetNumAvailableBeaconLocations(_ptr, puNumLocations);
			}
			public virtual bool /*bool*/ ISteamParties_GetAvailableBeaconLocations( ref SteamPartyBeaconLocation_t /*struct SteamPartyBeaconLocation_t **/ pLocationList, uint /*uint32*/ uMaxNumLocations )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamParties _ptr is null!" );
				
				var pLocationList_ps = new SteamPartyBeaconLocation_t.PackSmall();
				var ret = Native.SteamAPI_ISteamParties_GetAvailableBeaconLocations(_ptr, ref pLocationList_ps, uMaxNumLocations);
				pLocationList = pLocationList_ps;
				return ret;
			}
			public virtual SteamAPICall_t /*(SteamAPICall_t)*/ ISteamParties_CreateBeacon( uint /*uint32*/ unOpenSlots, ref SteamPartyBeaconLocation_t /*struct SteamPartyBeaconLocation_t **/ pBeaconLocation, string /*const char **/ pchConnectString, string /*const char **/ pchMetadata )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamParties _ptr is null!" );
				
				var pBeaconLocation_ps = new SteamPartyBeaconLocation_t.PackSmall();
				var ret = Native.SteamAPI_ISteamParties_CreateBeacon(_ptr, unOpenSlots, ref pBeaconLocation_ps, pchConnectString, pchMetadata);
				pBeaconLocation = pBeaconLocation_ps;
				return ret;
			}
			public virtual void /*void*/ ISteamParties_OnReservationCompleted( ulong ulBeacon, ulong steamIDUser )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamParties _ptr is null!" );
				
				Native.SteamAPI_ISteamParties_OnReservationCompleted(_ptr, ulBeacon, steamIDUser);
			}
			public virtual void /*void*/ ISteamParties_CancelReservation( ulong ulBeacon, ulong steamIDUser )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamParties _ptr is null!" );
				
				Native.SteamAPI_ISteamParties_CancelReservation(_ptr, ulBeacon, steamIDUser);
			}
			public virtual SteamAPICall_t /*(SteamAPICall_t)*/ ISteamParties_ChangeNumOpenSlots( ulong ulBeacon, uint /*uint32*/ unOpenSlots )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamParties _ptr is null!" );
				
				return Native.SteamAPI_ISteamParties_ChangeNumOpenSlots(_ptr, ulBeacon, unOpenSlots);
			}
			public virtual bool /*bool*/ ISteamParties_DestroyBeacon( ulong ulBeacon )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamParties _ptr is null!" );
				
				return Native.SteamAPI_ISteamParties_DestroyBeacon(_ptr, ulBeacon);
			}
			public virtual bool /*bool*/ ISteamParties_GetBeaconLocationData( SteamPartyBeaconLocation_t /*struct SteamPartyBeaconLocation_t*/ BeaconLocation, SteamPartyBeaconLocationData /*ESteamPartyBeaconLocationData*/ eData, System.Text.StringBuilder /*char **/ pchDataStringOut, int /*int*/ cchDataStringOut )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamParties _ptr is null!" );
				
				return Native.SteamAPI_ISteamParties_GetBeaconLocationData(_ptr, BeaconLocation, eData, pchDataStringOut, cchDataStringOut);
			}
			
			public virtual bool /*bool*/ ISteamRemoteStorage_FileWrite( string /*const char **/ pchFile, IntPtr /*const void **/ pvData, int /*int32*/ cubData )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamRemoteStorage _ptr is null!" );
				
				return Native.SteamAPI_ISteamRemoteStorage_FileWrite(_ptr, pchFile, pvData, cubData);
			}
			public virtual int /*int32*/ ISteamRemoteStorage_FileRead( string /*const char **/ pchFile, IntPtr /*void **/ pvData, int /*int32*/ cubDataToRead )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamRemoteStorage _ptr is null!" );
				
				return Native.SteamAPI_ISteamRemoteStorage_FileRead(_ptr, pchFile, pvData, cubDataToRead);
			}
			public virtual SteamAPICall_t /*(SteamAPICall_t)*/ ISteamRemoteStorage_FileWriteAsync( string /*const char **/ pchFile, IntPtr /*const void **/ pvData, uint /*uint32*/ cubData )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamRemoteStorage _ptr is null!" );
				
				return Native.SteamAPI_ISteamRemoteStorage_FileWriteAsync(_ptr, pchFile, pvData, cubData);
			}
			public virtual SteamAPICall_t /*(SteamAPICall_t)*/ ISteamRemoteStorage_FileReadAsync( string /*const char **/ pchFile, uint /*uint32*/ nOffset, uint /*uint32*/ cubToRead )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamRemoteStorage _ptr is null!" );
				
				return Native.SteamAPI_ISteamRemoteStorage_FileReadAsync(_ptr, pchFile, nOffset, cubToRead);
			}
			public virtual bool /*bool*/ ISteamRemoteStorage_FileReadAsyncComplete( ulong hReadCall, IntPtr /*void **/ pvBuffer, uint /*uint32*/ cubToRead )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamRemoteStorage _ptr is null!" );
				
				return Native.SteamAPI_ISteamRemoteStorage_FileReadAsyncComplete(_ptr, hReadCall, pvBuffer, cubToRead);
			}
			public virtual bool /*bool*/ ISteamRemoteStorage_FileForget( string /*const char **/ pchFile )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamRemoteStorage _ptr is null!" );
				
				return Native.SteamAPI_ISteamRemoteStorage_FileForget(_ptr, pchFile);
			}
			public virtual bool /*bool*/ ISteamRemoteStorage_FileDelete( string /*const char **/ pchFile )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamRemoteStorage _ptr is null!" );
				
				return Native.SteamAPI_ISteamRemoteStorage_FileDelete(_ptr, pchFile);
			}
			public virtual SteamAPICall_t /*(SteamAPICall_t)*/ ISteamRemoteStorage_FileShare( string /*const char **/ pchFile )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamRemoteStorage _ptr is null!" );
				
				return Native.SteamAPI_ISteamRemoteStorage_FileShare(_ptr, pchFile);
			}
			public virtual bool /*bool*/ ISteamRemoteStorage_SetSyncPlatforms( string /*const char **/ pchFile, RemoteStoragePlatform /*ERemoteStoragePlatform*/ eRemoteStoragePlatform )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamRemoteStorage _ptr is null!" );
				
				return Native.SteamAPI_ISteamRemoteStorage_SetSyncPlatforms(_ptr, pchFile, eRemoteStoragePlatform);
			}
			public virtual UGCFileWriteStreamHandle_t /*(UGCFileWriteStreamHandle_t)*/ ISteamRemoteStorage_FileWriteStreamOpen( string /*const char **/ pchFile )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamRemoteStorage _ptr is null!" );
				
				return Native.SteamAPI_ISteamRemoteStorage_FileWriteStreamOpen(_ptr, pchFile);
			}
			public virtual bool /*bool*/ ISteamRemoteStorage_FileWriteStreamWriteChunk( ulong writeHandle, IntPtr /*const void **/ pvData, int /*int32*/ cubData )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamRemoteStorage _ptr is null!" );
				
				return Native.SteamAPI_ISteamRemoteStorage_FileWriteStreamWriteChunk(_ptr, writeHandle, pvData, cubData);
			}
			public virtual bool /*bool*/ ISteamRemoteStorage_FileWriteStreamClose( ulong writeHandle )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamRemoteStorage _ptr is null!" );
				
				return Native.SteamAPI_ISteamRemoteStorage_FileWriteStreamClose(_ptr, writeHandle);
			}
			public virtual bool /*bool*/ ISteamRemoteStorage_FileWriteStreamCancel( ulong writeHandle )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamRemoteStorage _ptr is null!" );
				
				return Native.SteamAPI_ISteamRemoteStorage_FileWriteStreamCancel(_ptr, writeHandle);
			}
			public virtual bool /*bool*/ ISteamRemoteStorage_FileExists( string /*const char **/ pchFile )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamRemoteStorage _ptr is null!" );
				
				return Native.SteamAPI_ISteamRemoteStorage_FileExists(_ptr, pchFile);
			}
			public virtual bool /*bool*/ ISteamRemoteStorage_FilePersisted( string /*const char **/ pchFile )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamRemoteStorage _ptr is null!" );
				
				return Native.SteamAPI_ISteamRemoteStorage_FilePersisted(_ptr, pchFile);
			}
			public virtual int /*int32*/ ISteamRemoteStorage_GetFileSize( string /*const char **/ pchFile )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamRemoteStorage _ptr is null!" );
				
				return Native.SteamAPI_ISteamRemoteStorage_GetFileSize(_ptr, pchFile);
			}
			public virtual long /*int64*/ ISteamRemoteStorage_GetFileTimestamp( string /*const char **/ pchFile )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamRemoteStorage _ptr is null!" );
				
				return Native.SteamAPI_ISteamRemoteStorage_GetFileTimestamp(_ptr, pchFile);
			}
			public virtual RemoteStoragePlatform /*ERemoteStoragePlatform*/ ISteamRemoteStorage_GetSyncPlatforms( string /*const char **/ pchFile )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamRemoteStorage _ptr is null!" );
				
				return Native.SteamAPI_ISteamRemoteStorage_GetSyncPlatforms(_ptr, pchFile);
			}
			public virtual int /*int32*/ ISteamRemoteStorage_GetFileCount()
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamRemoteStorage _ptr is null!" );
				
				return Native.SteamAPI_ISteamRemoteStorage_GetFileCount(_ptr);
			}
			public virtual IntPtr ISteamRemoteStorage_GetFileNameAndSize( int /*int*/ iFile, out int /*int32 **/ pnFileSizeInBytes )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamRemoteStorage _ptr is null!" );
				
				return Native.SteamAPI_ISteamRemoteStorage_GetFileNameAndSize(_ptr, iFile, out pnFileSizeInBytes);
			}
			public virtual bool /*bool*/ ISteamRemoteStorage_GetQuota( out ulong /*uint64 **/ pnTotalBytes, out ulong /*uint64 **/ puAvailableBytes )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamRemoteStorage _ptr is null!" );
				
				return Native.SteamAPI_ISteamRemoteStorage_GetQuota(_ptr, out pnTotalBytes, out puAvailableBytes);
			}
			public virtual bool /*bool*/ ISteamRemoteStorage_IsCloudEnabledForAccount()
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamRemoteStorage _ptr is null!" );
				
				return Native.SteamAPI_ISteamRemoteStorage_IsCloudEnabledForAccount(_ptr);
			}
			public virtual bool /*bool*/ ISteamRemoteStorage_IsCloudEnabledForApp()
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamRemoteStorage _ptr is null!" );
				
				return Native.SteamAPI_ISteamRemoteStorage_IsCloudEnabledForApp(_ptr);
			}
			public virtual void /*void*/ ISteamRemoteStorage_SetCloudEnabledForApp( [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bEnabled )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamRemoteStorage _ptr is null!" );
				
				Native.SteamAPI_ISteamRemoteStorage_SetCloudEnabledForApp(_ptr, bEnabled);
			}
			public virtual SteamAPICall_t /*(SteamAPICall_t)*/ ISteamRemoteStorage_UGCDownload( ulong hContent, uint /*uint32*/ unPriority )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamRemoteStorage _ptr is null!" );
				
				return Native.SteamAPI_ISteamRemoteStorage_UGCDownload(_ptr, hContent, unPriority);
			}
			public virtual bool /*bool*/ ISteamRemoteStorage_GetUGCDownloadProgress( ulong hContent, out int /*int32 **/ pnBytesDownloaded, out int /*int32 **/ pnBytesExpected )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamRemoteStorage _ptr is null!" );
				
				return Native.SteamAPI_ISteamRemoteStorage_GetUGCDownloadProgress(_ptr, hContent, out pnBytesDownloaded, out pnBytesExpected);
			}
			public virtual bool /*bool*/ ISteamRemoteStorage_GetUGCDetails( ulong hContent, ref uint pnAppID, System.Text.StringBuilder /*char ***/ ppchName, out int /*int32 **/ pnFileSizeInBytes, out ulong pSteamIDOwner )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamRemoteStorage _ptr is null!" );
				
				return Native.SteamAPI_ISteamRemoteStorage_GetUGCDetails(_ptr, hContent, ref pnAppID, ppchName, out pnFileSizeInBytes, out pSteamIDOwner);
			}
			public virtual int /*int32*/ ISteamRemoteStorage_UGCRead( ulong hContent, IntPtr /*void **/ pvData, int /*int32*/ cubDataToRead, uint /*uint32*/ cOffset, UGCReadAction /*EUGCReadAction*/ eAction )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamRemoteStorage _ptr is null!" );
				
				return Native.SteamAPI_ISteamRemoteStorage_UGCRead(_ptr, hContent, pvData, cubDataToRead, cOffset, eAction);
			}
			public virtual int /*int32*/ ISteamRemoteStorage_GetCachedUGCCount()
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamRemoteStorage _ptr is null!" );
				
				return Native.SteamAPI_ISteamRemoteStorage_GetCachedUGCCount(_ptr);
			}
			public virtual UGCHandle_t /*(UGCHandle_t)*/ ISteamRemoteStorage_GetCachedUGCHandle( int /*int32*/ iCachedContent )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamRemoteStorage _ptr is null!" );
				
				return Native.SteamAPI_ISteamRemoteStorage_GetCachedUGCHandle(_ptr, iCachedContent);
			}
			public virtual SteamAPICall_t /*(SteamAPICall_t)*/ ISteamRemoteStorage_PublishWorkshopFile( string /*const char **/ pchFile, string /*const char **/ pchPreviewFile, uint nConsumerAppId, string /*const char **/ pchTitle, string /*const char **/ pchDescription, RemoteStoragePublishedFileVisibility /*ERemoteStoragePublishedFileVisibility*/ eVisibility, ref SteamParamStringArray_t /*struct SteamParamStringArray_t **/ pTags, WorkshopFileType /*EWorkshopFileType*/ eWorkshopFileType )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamRemoteStorage _ptr is null!" );
				
				var pTags_ps = new SteamParamStringArray_t.PackSmall();
				var ret = Native.SteamAPI_ISteamRemoteStorage_PublishWorkshopFile(_ptr, pchFile, pchPreviewFile, nConsumerAppId, pchTitle, pchDescription, eVisibility, ref pTags_ps, eWorkshopFileType);
				pTags = pTags_ps;
				return ret;
			}
			public virtual PublishedFileUpdateHandle_t /*(PublishedFileUpdateHandle_t)*/ ISteamRemoteStorage_CreatePublishedFileUpdateRequest( ulong unPublishedFileId )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamRemoteStorage _ptr is null!" );
				
				return Native.SteamAPI_ISteamRemoteStorage_CreatePublishedFileUpdateRequest(_ptr, unPublishedFileId);
			}
			public virtual bool /*bool*/ ISteamRemoteStorage_UpdatePublishedFileFile( ulong updateHandle, string /*const char **/ pchFile )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamRemoteStorage _ptr is null!" );
				
				return Native.SteamAPI_ISteamRemoteStorage_UpdatePublishedFileFile(_ptr, updateHandle, pchFile);
			}
			public virtual bool /*bool*/ ISteamRemoteStorage_UpdatePublishedFilePreviewFile( ulong updateHandle, string /*const char **/ pchPreviewFile )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamRemoteStorage _ptr is null!" );
				
				return Native.SteamAPI_ISteamRemoteStorage_UpdatePublishedFilePreviewFile(_ptr, updateHandle, pchPreviewFile);
			}
			public virtual bool /*bool*/ ISteamRemoteStorage_UpdatePublishedFileTitle( ulong updateHandle, string /*const char **/ pchTitle )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamRemoteStorage _ptr is null!" );
				
				return Native.SteamAPI_ISteamRemoteStorage_UpdatePublishedFileTitle(_ptr, updateHandle, pchTitle);
			}
			public virtual bool /*bool*/ ISteamRemoteStorage_UpdatePublishedFileDescription( ulong updateHandle, string /*const char **/ pchDescription )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamRemoteStorage _ptr is null!" );
				
				return Native.SteamAPI_ISteamRemoteStorage_UpdatePublishedFileDescription(_ptr, updateHandle, pchDescription);
			}
			public virtual bool /*bool*/ ISteamRemoteStorage_UpdatePublishedFileVisibility( ulong updateHandle, RemoteStoragePublishedFileVisibility /*ERemoteStoragePublishedFileVisibility*/ eVisibility )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamRemoteStorage _ptr is null!" );
				
				return Native.SteamAPI_ISteamRemoteStorage_UpdatePublishedFileVisibility(_ptr, updateHandle, eVisibility);
			}
			public virtual bool /*bool*/ ISteamRemoteStorage_UpdatePublishedFileTags( ulong updateHandle, ref SteamParamStringArray_t /*struct SteamParamStringArray_t **/ pTags )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamRemoteStorage _ptr is null!" );
				
				var pTags_ps = new SteamParamStringArray_t.PackSmall();
				var ret = Native.SteamAPI_ISteamRemoteStorage_UpdatePublishedFileTags(_ptr, updateHandle, ref pTags_ps);
				pTags = pTags_ps;
				return ret;
			}
			public virtual SteamAPICall_t /*(SteamAPICall_t)*/ ISteamRemoteStorage_CommitPublishedFileUpdate( ulong updateHandle )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamRemoteStorage _ptr is null!" );
				
				return Native.SteamAPI_ISteamRemoteStorage_CommitPublishedFileUpdate(_ptr, updateHandle);
			}
			public virtual SteamAPICall_t /*(SteamAPICall_t)*/ ISteamRemoteStorage_GetPublishedFileDetails( ulong unPublishedFileId, uint /*uint32*/ unMaxSecondsOld )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamRemoteStorage _ptr is null!" );
				
				return Native.SteamAPI_ISteamRemoteStorage_GetPublishedFileDetails(_ptr, unPublishedFileId, unMaxSecondsOld);
			}
			public virtual SteamAPICall_t /*(SteamAPICall_t)*/ ISteamRemoteStorage_DeletePublishedFile( ulong unPublishedFileId )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamRemoteStorage _ptr is null!" );
				
				return Native.SteamAPI_ISteamRemoteStorage_DeletePublishedFile(_ptr, unPublishedFileId);
			}
			public virtual SteamAPICall_t /*(SteamAPICall_t)*/ ISteamRemoteStorage_EnumerateUserPublishedFiles( uint /*uint32*/ unStartIndex )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamRemoteStorage _ptr is null!" );
				
				return Native.SteamAPI_ISteamRemoteStorage_EnumerateUserPublishedFiles(_ptr, unStartIndex);
			}
			public virtual SteamAPICall_t /*(SteamAPICall_t)*/ ISteamRemoteStorage_SubscribePublishedFile( ulong unPublishedFileId )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamRemoteStorage _ptr is null!" );
				
				return Native.SteamAPI_ISteamRemoteStorage_SubscribePublishedFile(_ptr, unPublishedFileId);
			}
			public virtual SteamAPICall_t /*(SteamAPICall_t)*/ ISteamRemoteStorage_EnumerateUserSubscribedFiles( uint /*uint32*/ unStartIndex )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamRemoteStorage _ptr is null!" );
				
				return Native.SteamAPI_ISteamRemoteStorage_EnumerateUserSubscribedFiles(_ptr, unStartIndex);
			}
			public virtual SteamAPICall_t /*(SteamAPICall_t)*/ ISteamRemoteStorage_UnsubscribePublishedFile( ulong unPublishedFileId )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamRemoteStorage _ptr is null!" );
				
				return Native.SteamAPI_ISteamRemoteStorage_UnsubscribePublishedFile(_ptr, unPublishedFileId);
			}
			public virtual bool /*bool*/ ISteamRemoteStorage_UpdatePublishedFileSetChangeDescription( ulong updateHandle, string /*const char **/ pchChangeDescription )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamRemoteStorage _ptr is null!" );
				
				return Native.SteamAPI_ISteamRemoteStorage_UpdatePublishedFileSetChangeDescription(_ptr, updateHandle, pchChangeDescription);
			}
			public virtual SteamAPICall_t /*(SteamAPICall_t)*/ ISteamRemoteStorage_GetPublishedItemVoteDetails( ulong unPublishedFileId )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamRemoteStorage _ptr is null!" );
				
				return Native.SteamAPI_ISteamRemoteStorage_GetPublishedItemVoteDetails(_ptr, unPublishedFileId);
			}
			public virtual SteamAPICall_t /*(SteamAPICall_t)*/ ISteamRemoteStorage_UpdateUserPublishedItemVote( ulong unPublishedFileId, [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bVoteUp )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamRemoteStorage _ptr is null!" );
				
				return Native.SteamAPI_ISteamRemoteStorage_UpdateUserPublishedItemVote(_ptr, unPublishedFileId, bVoteUp);
			}
			public virtual SteamAPICall_t /*(SteamAPICall_t)*/ ISteamRemoteStorage_GetUserPublishedItemVoteDetails( ulong unPublishedFileId )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamRemoteStorage _ptr is null!" );
				
				return Native.SteamAPI_ISteamRemoteStorage_GetUserPublishedItemVoteDetails(_ptr, unPublishedFileId);
			}
			public virtual SteamAPICall_t /*(SteamAPICall_t)*/ ISteamRemoteStorage_EnumerateUserSharedWorkshopFiles( ulong steamId, uint /*uint32*/ unStartIndex, ref SteamParamStringArray_t /*struct SteamParamStringArray_t **/ pRequiredTags, ref SteamParamStringArray_t /*struct SteamParamStringArray_t **/ pExcludedTags )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamRemoteStorage _ptr is null!" );
				
				var pRequiredTags_ps = new SteamParamStringArray_t.PackSmall();
				var pExcludedTags_ps = new SteamParamStringArray_t.PackSmall();
				var ret = Native.SteamAPI_ISteamRemoteStorage_EnumerateUserSharedWorkshopFiles(_ptr, steamId, unStartIndex, ref pRequiredTags_ps, ref pExcludedTags_ps);
				pRequiredTags = pRequiredTags_ps;
				pExcludedTags = pExcludedTags_ps;
				return ret;
			}
			public virtual SteamAPICall_t /*(SteamAPICall_t)*/ ISteamRemoteStorage_PublishVideo( WorkshopVideoProvider /*EWorkshopVideoProvider*/ eVideoProvider, string /*const char **/ pchVideoAccount, string /*const char **/ pchVideoIdentifier, string /*const char **/ pchPreviewFile, uint nConsumerAppId, string /*const char **/ pchTitle, string /*const char **/ pchDescription, RemoteStoragePublishedFileVisibility /*ERemoteStoragePublishedFileVisibility*/ eVisibility, ref SteamParamStringArray_t /*struct SteamParamStringArray_t **/ pTags )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamRemoteStorage _ptr is null!" );
				
				var pTags_ps = new SteamParamStringArray_t.PackSmall();
				var ret = Native.SteamAPI_ISteamRemoteStorage_PublishVideo(_ptr, eVideoProvider, pchVideoAccount, pchVideoIdentifier, pchPreviewFile, nConsumerAppId, pchTitle, pchDescription, eVisibility, ref pTags_ps);
				pTags = pTags_ps;
				return ret;
			}
			public virtual SteamAPICall_t /*(SteamAPICall_t)*/ ISteamRemoteStorage_SetUserPublishedFileAction( ulong unPublishedFileId, WorkshopFileAction /*EWorkshopFileAction*/ eAction )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamRemoteStorage _ptr is null!" );
				
				return Native.SteamAPI_ISteamRemoteStorage_SetUserPublishedFileAction(_ptr, unPublishedFileId, eAction);
			}
			public virtual SteamAPICall_t /*(SteamAPICall_t)*/ ISteamRemoteStorage_EnumeratePublishedFilesByUserAction( WorkshopFileAction /*EWorkshopFileAction*/ eAction, uint /*uint32*/ unStartIndex )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamRemoteStorage _ptr is null!" );
				
				return Native.SteamAPI_ISteamRemoteStorage_EnumeratePublishedFilesByUserAction(_ptr, eAction, unStartIndex);
			}
			public virtual SteamAPICall_t /*(SteamAPICall_t)*/ ISteamRemoteStorage_EnumeratePublishedWorkshopFiles( WorkshopEnumerationType /*EWorkshopEnumerationType*/ eEnumerationType, uint /*uint32*/ unStartIndex, uint /*uint32*/ unCount, uint /*uint32*/ unDays, ref SteamParamStringArray_t /*struct SteamParamStringArray_t **/ pTags, ref SteamParamStringArray_t /*struct SteamParamStringArray_t **/ pUserTags )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamRemoteStorage _ptr is null!" );
				
				var pTags_ps = new SteamParamStringArray_t.PackSmall();
				var pUserTags_ps = new SteamParamStringArray_t.PackSmall();
				var ret = Native.SteamAPI_ISteamRemoteStorage_EnumeratePublishedWorkshopFiles(_ptr, eEnumerationType, unStartIndex, unCount, unDays, ref pTags_ps, ref pUserTags_ps);
				pTags = pTags_ps;
				pUserTags = pUserTags_ps;
				return ret;
			}
			public virtual SteamAPICall_t /*(SteamAPICall_t)*/ ISteamRemoteStorage_UGCDownloadToLocation( ulong hContent, string /*const char **/ pchLocation, uint /*uint32*/ unPriority )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamRemoteStorage _ptr is null!" );
				
				return Native.SteamAPI_ISteamRemoteStorage_UGCDownloadToLocation(_ptr, hContent, pchLocation, unPriority);
			}
			
			public virtual bool /*bool*/ ISteamUserStats_RequestCurrentStats()
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamUserStats _ptr is null!" );
				
				return Native.SteamAPI_ISteamUserStats_RequestCurrentStats(_ptr);
			}
			public virtual bool /*bool*/ ISteamUserStats_GetStat( string /*const char **/ pchName, out int /*int32 **/ pData )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamUserStats _ptr is null!" );
				
				return Native.SteamAPI_ISteamUserStats_GetStat(_ptr, pchName, out pData);
			}
			public virtual bool /*bool*/ ISteamUserStats_GetStat0( string /*const char **/ pchName, out float /*float **/ pData )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamUserStats _ptr is null!" );
				
				return Native.SteamAPI_ISteamUserStats_GetStat0(_ptr, pchName, out pData);
			}
			public virtual bool /*bool*/ ISteamUserStats_SetStat( string /*const char **/ pchName, int /*int32*/ nData )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamUserStats _ptr is null!" );
				
				return Native.SteamAPI_ISteamUserStats_SetStat(_ptr, pchName, nData);
			}
			public virtual bool /*bool*/ ISteamUserStats_SetStat0( string /*const char **/ pchName, float /*float*/ fData )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamUserStats _ptr is null!" );
				
				return Native.SteamAPI_ISteamUserStats_SetStat0(_ptr, pchName, fData);
			}
			public virtual bool /*bool*/ ISteamUserStats_UpdateAvgRateStat( string /*const char **/ pchName, float /*float*/ flCountThisSession, double /*double*/ dSessionLength )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamUserStats _ptr is null!" );
				
				return Native.SteamAPI_ISteamUserStats_UpdateAvgRateStat(_ptr, pchName, flCountThisSession, dSessionLength);
			}
			public virtual bool /*bool*/ ISteamUserStats_GetAchievement( string /*const char **/ pchName, [MarshalAs(UnmanagedType.U1)] ref bool /*bool **/ pbAchieved )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamUserStats _ptr is null!" );
				
				return Native.SteamAPI_ISteamUserStats_GetAchievement(_ptr, pchName, ref pbAchieved);
			}
			public virtual bool /*bool*/ ISteamUserStats_SetAchievement( string /*const char **/ pchName )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamUserStats _ptr is null!" );
				
				return Native.SteamAPI_ISteamUserStats_SetAchievement(_ptr, pchName);
			}
			public virtual bool /*bool*/ ISteamUserStats_ClearAchievement( string /*const char **/ pchName )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamUserStats _ptr is null!" );
				
				return Native.SteamAPI_ISteamUserStats_ClearAchievement(_ptr, pchName);
			}
			public virtual bool /*bool*/ ISteamUserStats_GetAchievementAndUnlockTime( string /*const char **/ pchName, [MarshalAs(UnmanagedType.U1)] ref bool /*bool **/ pbAchieved, out uint /*uint32 **/ punUnlockTime )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamUserStats _ptr is null!" );
				
				return Native.SteamAPI_ISteamUserStats_GetAchievementAndUnlockTime(_ptr, pchName, ref pbAchieved, out punUnlockTime);
			}
			public virtual bool /*bool*/ ISteamUserStats_StoreStats()
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamUserStats _ptr is null!" );
				
				return Native.SteamAPI_ISteamUserStats_StoreStats(_ptr);
			}
			public virtual int /*int*/ ISteamUserStats_GetAchievementIcon( string /*const char **/ pchName )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamUserStats _ptr is null!" );
				
				return Native.SteamAPI_ISteamUserStats_GetAchievementIcon(_ptr, pchName);
			}
			public virtual IntPtr ISteamUserStats_GetAchievementDisplayAttribute( string /*const char **/ pchName, string /*const char **/ pchKey )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamUserStats _ptr is null!" );
				
				return Native.SteamAPI_ISteamUserStats_GetAchievementDisplayAttribute(_ptr, pchName, pchKey);
			}
			public virtual bool /*bool*/ ISteamUserStats_IndicateAchievementProgress( string /*const char **/ pchName, uint /*uint32*/ nCurProgress, uint /*uint32*/ nMaxProgress )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamUserStats _ptr is null!" );
				
				return Native.SteamAPI_ISteamUserStats_IndicateAchievementProgress(_ptr, pchName, nCurProgress, nMaxProgress);
			}
			public virtual uint /*uint32*/ ISteamUserStats_GetNumAchievements()
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamUserStats _ptr is null!" );
				
				return Native.SteamAPI_ISteamUserStats_GetNumAchievements(_ptr);
			}
			public virtual IntPtr ISteamUserStats_GetAchievementName( uint /*uint32*/ iAchievement )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamUserStats _ptr is null!" );
				
				return Native.SteamAPI_ISteamUserStats_GetAchievementName(_ptr, iAchievement);
			}
			public virtual SteamAPICall_t /*(SteamAPICall_t)*/ ISteamUserStats_RequestUserStats( ulong steamIDUser )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamUserStats _ptr is null!" );
				
				return Native.SteamAPI_ISteamUserStats_RequestUserStats(_ptr, steamIDUser);
			}
			public virtual bool /*bool*/ ISteamUserStats_GetUserStat( ulong steamIDUser, string /*const char **/ pchName, out int /*int32 **/ pData )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamUserStats _ptr is null!" );
				
				return Native.SteamAPI_ISteamUserStats_GetUserStat(_ptr, steamIDUser, pchName, out pData);
			}
			public virtual bool /*bool*/ ISteamUserStats_GetUserStat0( ulong steamIDUser, string /*const char **/ pchName, out float /*float **/ pData )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamUserStats _ptr is null!" );
				
				return Native.SteamAPI_ISteamUserStats_GetUserStat0(_ptr, steamIDUser, pchName, out pData);
			}
			public virtual bool /*bool*/ ISteamUserStats_GetUserAchievement( ulong steamIDUser, string /*const char **/ pchName, [MarshalAs(UnmanagedType.U1)] ref bool /*bool **/ pbAchieved )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamUserStats _ptr is null!" );
				
				return Native.SteamAPI_ISteamUserStats_GetUserAchievement(_ptr, steamIDUser, pchName, ref pbAchieved);
			}
			public virtual bool /*bool*/ ISteamUserStats_GetUserAchievementAndUnlockTime( ulong steamIDUser, string /*const char **/ pchName, [MarshalAs(UnmanagedType.U1)] ref bool /*bool **/ pbAchieved, out uint /*uint32 **/ punUnlockTime )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamUserStats _ptr is null!" );
				
				return Native.SteamAPI_ISteamUserStats_GetUserAchievementAndUnlockTime(_ptr, steamIDUser, pchName, ref pbAchieved, out punUnlockTime);
			}
			public virtual bool /*bool*/ ISteamUserStats_ResetAllStats( [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bAchievementsToo )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamUserStats _ptr is null!" );
				
				return Native.SteamAPI_ISteamUserStats_ResetAllStats(_ptr, bAchievementsToo);
			}
			public virtual SteamAPICall_t /*(SteamAPICall_t)*/ ISteamUserStats_FindOrCreateLeaderboard( string /*const char **/ pchLeaderboardName, LeaderboardSortMethod /*ELeaderboardSortMethod*/ eLeaderboardSortMethod, LeaderboardDisplayType /*ELeaderboardDisplayType*/ eLeaderboardDisplayType )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamUserStats _ptr is null!" );
				
				return Native.SteamAPI_ISteamUserStats_FindOrCreateLeaderboard(_ptr, pchLeaderboardName, eLeaderboardSortMethod, eLeaderboardDisplayType);
			}
			public virtual SteamAPICall_t /*(SteamAPICall_t)*/ ISteamUserStats_FindLeaderboard( string /*const char **/ pchLeaderboardName )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamUserStats _ptr is null!" );
				
				return Native.SteamAPI_ISteamUserStats_FindLeaderboard(_ptr, pchLeaderboardName);
			}
			public virtual IntPtr ISteamUserStats_GetLeaderboardName( ulong hSteamLeaderboard )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamUserStats _ptr is null!" );
				
				return Native.SteamAPI_ISteamUserStats_GetLeaderboardName(_ptr, hSteamLeaderboard);
			}
			public virtual int /*int*/ ISteamUserStats_GetLeaderboardEntryCount( ulong hSteamLeaderboard )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamUserStats _ptr is null!" );
				
				return Native.SteamAPI_ISteamUserStats_GetLeaderboardEntryCount(_ptr, hSteamLeaderboard);
			}
			public virtual LeaderboardSortMethod /*ELeaderboardSortMethod*/ ISteamUserStats_GetLeaderboardSortMethod( ulong hSteamLeaderboard )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamUserStats _ptr is null!" );
				
				return Native.SteamAPI_ISteamUserStats_GetLeaderboardSortMethod(_ptr, hSteamLeaderboard);
			}
			public virtual LeaderboardDisplayType /*ELeaderboardDisplayType*/ ISteamUserStats_GetLeaderboardDisplayType( ulong hSteamLeaderboard )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamUserStats _ptr is null!" );
				
				return Native.SteamAPI_ISteamUserStats_GetLeaderboardDisplayType(_ptr, hSteamLeaderboard);
			}
			public virtual SteamAPICall_t /*(SteamAPICall_t)*/ ISteamUserStats_DownloadLeaderboardEntries( ulong hSteamLeaderboard, LeaderboardDataRequest /*ELeaderboardDataRequest*/ eLeaderboardDataRequest, int /*int*/ nRangeStart, int /*int*/ nRangeEnd )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamUserStats _ptr is null!" );
				
				return Native.SteamAPI_ISteamUserStats_DownloadLeaderboardEntries(_ptr, hSteamLeaderboard, eLeaderboardDataRequest, nRangeStart, nRangeEnd);
			}
			public virtual SteamAPICall_t /*(SteamAPICall_t)*/ ISteamUserStats_DownloadLeaderboardEntriesForUsers( ulong hSteamLeaderboard, IntPtr /*class CSteamID **/ prgUsers, int /*int*/ cUsers )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamUserStats _ptr is null!" );
				
				return Native.SteamAPI_ISteamUserStats_DownloadLeaderboardEntriesForUsers(_ptr, hSteamLeaderboard, prgUsers, cUsers);
			}
			public virtual bool /*bool*/ ISteamUserStats_GetDownloadedLeaderboardEntry( ulong hSteamLeaderboardEntries, int /*int*/ index, ref LeaderboardEntry_t /*struct LeaderboardEntry_t **/ pLeaderboardEntry, IntPtr /*int32 **/ pDetails, int /*int*/ cDetailsMax )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamUserStats _ptr is null!" );
				
				var pLeaderboardEntry_ps = new LeaderboardEntry_t.PackSmall();
				var ret = Native.SteamAPI_ISteamUserStats_GetDownloadedLeaderboardEntry(_ptr, hSteamLeaderboardEntries, index, ref pLeaderboardEntry_ps, pDetails, cDetailsMax);
				pLeaderboardEntry = pLeaderboardEntry_ps;
				return ret;
			}
			public virtual SteamAPICall_t /*(SteamAPICall_t)*/ ISteamUserStats_UploadLeaderboardScore( ulong hSteamLeaderboard, LeaderboardUploadScoreMethod /*ELeaderboardUploadScoreMethod*/ eLeaderboardUploadScoreMethod, int /*int32*/ nScore, int[] /*const int32 **/ pScoreDetails, int /*int*/ cScoreDetailsCount )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamUserStats _ptr is null!" );
				
				return Native.SteamAPI_ISteamUserStats_UploadLeaderboardScore(_ptr, hSteamLeaderboard, eLeaderboardUploadScoreMethod, nScore, pScoreDetails, cScoreDetailsCount);
			}
			public virtual SteamAPICall_t /*(SteamAPICall_t)*/ ISteamUserStats_AttachLeaderboardUGC( ulong hSteamLeaderboard, ulong hUGC )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamUserStats _ptr is null!" );
				
				return Native.SteamAPI_ISteamUserStats_AttachLeaderboardUGC(_ptr, hSteamLeaderboard, hUGC);
			}
			public virtual SteamAPICall_t /*(SteamAPICall_t)*/ ISteamUserStats_GetNumberOfCurrentPlayers()
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamUserStats _ptr is null!" );
				
				return Native.SteamAPI_ISteamUserStats_GetNumberOfCurrentPlayers(_ptr);
			}
			public virtual SteamAPICall_t /*(SteamAPICall_t)*/ ISteamUserStats_RequestGlobalAchievementPercentages()
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamUserStats _ptr is null!" );
				
				return Native.SteamAPI_ISteamUserStats_RequestGlobalAchievementPercentages(_ptr);
			}
			public virtual int /*int*/ ISteamUserStats_GetMostAchievedAchievementInfo( System.Text.StringBuilder /*char **/ pchName, uint /*uint32*/ unNameBufLen, out float /*float **/ pflPercent, [MarshalAs(UnmanagedType.U1)] ref bool /*bool **/ pbAchieved )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamUserStats _ptr is null!" );
				
				return Native.SteamAPI_ISteamUserStats_GetMostAchievedAchievementInfo(_ptr, pchName, unNameBufLen, out pflPercent, ref pbAchieved);
			}
			public virtual int /*int*/ ISteamUserStats_GetNextMostAchievedAchievementInfo( int /*int*/ iIteratorPrevious, System.Text.StringBuilder /*char **/ pchName, uint /*uint32*/ unNameBufLen, out float /*float **/ pflPercent, [MarshalAs(UnmanagedType.U1)] ref bool /*bool **/ pbAchieved )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamUserStats _ptr is null!" );
				
				return Native.SteamAPI_ISteamUserStats_GetNextMostAchievedAchievementInfo(_ptr, iIteratorPrevious, pchName, unNameBufLen, out pflPercent, ref pbAchieved);
			}
			public virtual bool /*bool*/ ISteamUserStats_GetAchievementAchievedPercent( string /*const char **/ pchName, out float /*float **/ pflPercent )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamUserStats _ptr is null!" );
				
				return Native.SteamAPI_ISteamUserStats_GetAchievementAchievedPercent(_ptr, pchName, out pflPercent);
			}
			public virtual SteamAPICall_t /*(SteamAPICall_t)*/ ISteamUserStats_RequestGlobalStats( int /*int*/ nHistoryDays )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamUserStats _ptr is null!" );
				
				return Native.SteamAPI_ISteamUserStats_RequestGlobalStats(_ptr, nHistoryDays);
			}
			public virtual bool /*bool*/ ISteamUserStats_GetGlobalStat( string /*const char **/ pchStatName, out long /*int64 **/ pData )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamUserStats _ptr is null!" );
				
				return Native.SteamAPI_ISteamUserStats_GetGlobalStat(_ptr, pchStatName, out pData);
			}
			public virtual bool /*bool*/ ISteamUserStats_GetGlobalStat0( string /*const char **/ pchStatName, out double /*double **/ pData )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamUserStats _ptr is null!" );
				
				return Native.SteamAPI_ISteamUserStats_GetGlobalStat0(_ptr, pchStatName, out pData);
			}
			public virtual int /*int32*/ ISteamUserStats_GetGlobalStatHistory( string /*const char **/ pchStatName, out long /*int64 **/ pData, uint /*uint32*/ cubData )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamUserStats _ptr is null!" );
				
				return Native.SteamAPI_ISteamUserStats_GetGlobalStatHistory(_ptr, pchStatName, out pData, cubData);
			}
			public virtual int /*int32*/ ISteamUserStats_GetGlobalStatHistory0( string /*const char **/ pchStatName, out double /*double **/ pData, uint /*uint32*/ cubData )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamUserStats _ptr is null!" );
				
				return Native.SteamAPI_ISteamUserStats_GetGlobalStatHistory0(_ptr, pchStatName, out pData, cubData);
			}
			
			public virtual bool /*bool*/ ISteamApps_BIsSubscribed()
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamApps _ptr is null!" );
				
				return Native.SteamAPI_ISteamApps_BIsSubscribed(_ptr);
			}
			public virtual bool /*bool*/ ISteamApps_BIsLowViolence()
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamApps _ptr is null!" );
				
				return Native.SteamAPI_ISteamApps_BIsLowViolence(_ptr);
			}
			public virtual bool /*bool*/ ISteamApps_BIsCybercafe()
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamApps _ptr is null!" );
				
				return Native.SteamAPI_ISteamApps_BIsCybercafe(_ptr);
			}
			public virtual bool /*bool*/ ISteamApps_BIsVACBanned()
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamApps _ptr is null!" );
				
				return Native.SteamAPI_ISteamApps_BIsVACBanned(_ptr);
			}
			public virtual IntPtr ISteamApps_GetCurrentGameLanguage()
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamApps _ptr is null!" );
				
				return Native.SteamAPI_ISteamApps_GetCurrentGameLanguage(_ptr);
			}
			public virtual IntPtr ISteamApps_GetAvailableGameLanguages()
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamApps _ptr is null!" );
				
				return Native.SteamAPI_ISteamApps_GetAvailableGameLanguages(_ptr);
			}
			public virtual bool /*bool*/ ISteamApps_BIsSubscribedApp( uint appID )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamApps _ptr is null!" );
				
				return Native.SteamAPI_ISteamApps_BIsSubscribedApp(_ptr, appID);
			}
			public virtual bool /*bool*/ ISteamApps_BIsDlcInstalled( uint appID )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamApps _ptr is null!" );
				
				return Native.SteamAPI_ISteamApps_BIsDlcInstalled(_ptr, appID);
			}
			public virtual uint /*uint32*/ ISteamApps_GetEarliestPurchaseUnixTime( uint nAppID )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamApps _ptr is null!" );
				
				return Native.SteamAPI_ISteamApps_GetEarliestPurchaseUnixTime(_ptr, nAppID);
			}
			public virtual bool /*bool*/ ISteamApps_BIsSubscribedFromFreeWeekend()
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamApps _ptr is null!" );
				
				return Native.SteamAPI_ISteamApps_BIsSubscribedFromFreeWeekend(_ptr);
			}
			public virtual int /*int*/ ISteamApps_GetDLCCount()
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamApps _ptr is null!" );
				
				return Native.SteamAPI_ISteamApps_GetDLCCount(_ptr);
			}
			public virtual bool /*bool*/ ISteamApps_BGetDLCDataByIndex( int /*int*/ iDLC, ref uint pAppID, [MarshalAs(UnmanagedType.U1)] ref bool /*bool **/ pbAvailable, System.Text.StringBuilder /*char **/ pchName, int /*int*/ cchNameBufferSize )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamApps _ptr is null!" );
				
				return Native.SteamAPI_ISteamApps_BGetDLCDataByIndex(_ptr, iDLC, ref pAppID, ref pbAvailable, pchName, cchNameBufferSize);
			}
			public virtual void /*void*/ ISteamApps_InstallDLC( uint nAppID )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamApps _ptr is null!" );
				
				Native.SteamAPI_ISteamApps_InstallDLC(_ptr, nAppID);
			}
			public virtual void /*void*/ ISteamApps_UninstallDLC( uint nAppID )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamApps _ptr is null!" );
				
				Native.SteamAPI_ISteamApps_UninstallDLC(_ptr, nAppID);
			}
			public virtual void /*void*/ ISteamApps_RequestAppProofOfPurchaseKey( uint nAppID )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamApps _ptr is null!" );
				
				Native.SteamAPI_ISteamApps_RequestAppProofOfPurchaseKey(_ptr, nAppID);
			}
			public virtual bool /*bool*/ ISteamApps_GetCurrentBetaName( System.Text.StringBuilder /*char **/ pchName, int /*int*/ cchNameBufferSize )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamApps _ptr is null!" );
				
				return Native.SteamAPI_ISteamApps_GetCurrentBetaName(_ptr, pchName, cchNameBufferSize);
			}
			public virtual bool /*bool*/ ISteamApps_MarkContentCorrupt( [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bMissingFilesOnly )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamApps _ptr is null!" );
				
				return Native.SteamAPI_ISteamApps_MarkContentCorrupt(_ptr, bMissingFilesOnly);
			}
			public virtual uint /*uint32*/ ISteamApps_GetInstalledDepots( uint appID, IntPtr /*DepotId_t **/ pvecDepots, uint /*uint32*/ cMaxDepots )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamApps _ptr is null!" );
				
				return Native.SteamAPI_ISteamApps_GetInstalledDepots(_ptr, appID, pvecDepots, cMaxDepots);
			}
			public virtual uint /*uint32*/ ISteamApps_GetAppInstallDir( uint appID, System.Text.StringBuilder /*char **/ pchFolder, uint /*uint32*/ cchFolderBufferSize )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamApps _ptr is null!" );
				
				return Native.SteamAPI_ISteamApps_GetAppInstallDir(_ptr, appID, pchFolder, cchFolderBufferSize);
			}
			public virtual bool /*bool*/ ISteamApps_BIsAppInstalled( uint appID )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamApps _ptr is null!" );
				
				return Native.SteamAPI_ISteamApps_BIsAppInstalled(_ptr, appID);
			}
			public virtual CSteamID /*(class CSteamID)*/ ISteamApps_GetAppOwner()
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamApps _ptr is null!" );
				
				return Native.SteamAPI_ISteamApps_GetAppOwner(_ptr);
			}
			public virtual IntPtr ISteamApps_GetLaunchQueryParam( string /*const char **/ pchKey )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamApps _ptr is null!" );
				
				return Native.SteamAPI_ISteamApps_GetLaunchQueryParam(_ptr, pchKey);
			}
			public virtual bool /*bool*/ ISteamApps_GetDlcDownloadProgress( uint nAppID, out ulong /*uint64 **/ punBytesDownloaded, out ulong /*uint64 **/ punBytesTotal )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamApps _ptr is null!" );
				
				return Native.SteamAPI_ISteamApps_GetDlcDownloadProgress(_ptr, nAppID, out punBytesDownloaded, out punBytesTotal);
			}
			public virtual int /*int*/ ISteamApps_GetAppBuildId()
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamApps _ptr is null!" );
				
				return Native.SteamAPI_ISteamApps_GetAppBuildId(_ptr);
			}
			public virtual void /*void*/ ISteamApps_RequestAllProofOfPurchaseKeys()
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamApps _ptr is null!" );
				
				Native.SteamAPI_ISteamApps_RequestAllProofOfPurchaseKeys(_ptr);
			}
			public virtual SteamAPICall_t /*(SteamAPICall_t)*/ ISteamApps_GetFileDetails( string /*const char **/ pszFileName )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamApps _ptr is null!" );
				
				return Native.SteamAPI_ISteamApps_GetFileDetails(_ptr, pszFileName);
			}
			public virtual int /*int*/ ISteamApps_GetLaunchCommandLine( System.Text.StringBuilder /*char **/ pszCommandLine, int /*int*/ cubCommandLine )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamApps _ptr is null!" );
				
				return Native.SteamAPI_ISteamApps_GetLaunchCommandLine(_ptr, pszCommandLine, cubCommandLine);
			}
			public virtual bool /*bool*/ ISteamApps_BIsSubscribedFromFamilySharing()
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamApps _ptr is null!" );
				
				return Native.SteamAPI_ISteamApps_BIsSubscribedFromFamilySharing(_ptr);
			}
			
			public virtual bool /*bool*/ ISteamNetworking_SendP2PPacket( ulong steamIDRemote, IntPtr /*const void **/ pubData, uint /*uint32*/ cubData, P2PSend /*EP2PSend*/ eP2PSendType, int /*int*/ nChannel )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamNetworking _ptr is null!" );
				
				return Native.SteamAPI_ISteamNetworking_SendP2PPacket(_ptr, steamIDRemote, pubData, cubData, eP2PSendType, nChannel);
			}
			public virtual bool /*bool*/ ISteamNetworking_IsP2PPacketAvailable( out uint /*uint32 **/ pcubMsgSize, int /*int*/ nChannel )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamNetworking _ptr is null!" );
				
				return Native.SteamAPI_ISteamNetworking_IsP2PPacketAvailable(_ptr, out pcubMsgSize, nChannel);
			}
			public virtual bool /*bool*/ ISteamNetworking_ReadP2PPacket( IntPtr /*void **/ pubDest, uint /*uint32*/ cubDest, out uint /*uint32 **/ pcubMsgSize, out ulong psteamIDRemote, int /*int*/ nChannel )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamNetworking _ptr is null!" );
				
				return Native.SteamAPI_ISteamNetworking_ReadP2PPacket(_ptr, pubDest, cubDest, out pcubMsgSize, out psteamIDRemote, nChannel);
			}
			public virtual bool /*bool*/ ISteamNetworking_AcceptP2PSessionWithUser( ulong steamIDRemote )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamNetworking _ptr is null!" );
				
				return Native.SteamAPI_ISteamNetworking_AcceptP2PSessionWithUser(_ptr, steamIDRemote);
			}
			public virtual bool /*bool*/ ISteamNetworking_CloseP2PSessionWithUser( ulong steamIDRemote )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamNetworking _ptr is null!" );
				
				return Native.SteamAPI_ISteamNetworking_CloseP2PSessionWithUser(_ptr, steamIDRemote);
			}
			public virtual bool /*bool*/ ISteamNetworking_CloseP2PChannelWithUser( ulong steamIDRemote, int /*int*/ nChannel )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamNetworking _ptr is null!" );
				
				return Native.SteamAPI_ISteamNetworking_CloseP2PChannelWithUser(_ptr, steamIDRemote, nChannel);
			}
			public virtual bool /*bool*/ ISteamNetworking_GetP2PSessionState( ulong steamIDRemote, ref P2PSessionState_t /*struct P2PSessionState_t **/ pConnectionState )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamNetworking _ptr is null!" );
				
				var pConnectionState_ps = new P2PSessionState_t.PackSmall();
				var ret = Native.SteamAPI_ISteamNetworking_GetP2PSessionState(_ptr, steamIDRemote, ref pConnectionState_ps);
				pConnectionState = pConnectionState_ps;
				return ret;
			}
			public virtual bool /*bool*/ ISteamNetworking_AllowP2PPacketRelay( [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bAllow )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamNetworking _ptr is null!" );
				
				return Native.SteamAPI_ISteamNetworking_AllowP2PPacketRelay(_ptr, bAllow);
			}
			public virtual SNetListenSocket_t /*(SNetListenSocket_t)*/ ISteamNetworking_CreateListenSocket( int /*int*/ nVirtualP2PPort, uint /*uint32*/ nIP, ushort /*uint16*/ nPort, [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bAllowUseOfPacketRelay )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamNetworking _ptr is null!" );
				
				return Native.SteamAPI_ISteamNetworking_CreateListenSocket(_ptr, nVirtualP2PPort, nIP, nPort, bAllowUseOfPacketRelay);
			}
			public virtual SNetSocket_t /*(SNetSocket_t)*/ ISteamNetworking_CreateP2PConnectionSocket( ulong steamIDTarget, int /*int*/ nVirtualPort, int /*int*/ nTimeoutSec, [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bAllowUseOfPacketRelay )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamNetworking _ptr is null!" );
				
				return Native.SteamAPI_ISteamNetworking_CreateP2PConnectionSocket(_ptr, steamIDTarget, nVirtualPort, nTimeoutSec, bAllowUseOfPacketRelay);
			}
			public virtual SNetSocket_t /*(SNetSocket_t)*/ ISteamNetworking_CreateConnectionSocket( uint /*uint32*/ nIP, ushort /*uint16*/ nPort, int /*int*/ nTimeoutSec )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamNetworking _ptr is null!" );
				
				return Native.SteamAPI_ISteamNetworking_CreateConnectionSocket(_ptr, nIP, nPort, nTimeoutSec);
			}
			public virtual bool /*bool*/ ISteamNetworking_DestroySocket( uint hSocket, [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bNotifyRemoteEnd )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamNetworking _ptr is null!" );
				
				return Native.SteamAPI_ISteamNetworking_DestroySocket(_ptr, hSocket, bNotifyRemoteEnd);
			}
			public virtual bool /*bool*/ ISteamNetworking_DestroyListenSocket( uint hSocket, [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bNotifyRemoteEnd )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamNetworking _ptr is null!" );
				
				return Native.SteamAPI_ISteamNetworking_DestroyListenSocket(_ptr, hSocket, bNotifyRemoteEnd);
			}
			public virtual bool /*bool*/ ISteamNetworking_SendDataOnSocket( uint hSocket, IntPtr /*void **/ pubData, uint /*uint32*/ cubData, [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bReliable )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamNetworking _ptr is null!" );
				
				return Native.SteamAPI_ISteamNetworking_SendDataOnSocket(_ptr, hSocket, pubData, cubData, bReliable);
			}
			public virtual bool /*bool*/ ISteamNetworking_IsDataAvailableOnSocket( uint hSocket, out uint /*uint32 **/ pcubMsgSize )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamNetworking _ptr is null!" );
				
				return Native.SteamAPI_ISteamNetworking_IsDataAvailableOnSocket(_ptr, hSocket, out pcubMsgSize);
			}
			public virtual bool /*bool*/ ISteamNetworking_RetrieveDataFromSocket( uint hSocket, IntPtr /*void **/ pubDest, uint /*uint32*/ cubDest, out uint /*uint32 **/ pcubMsgSize )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamNetworking _ptr is null!" );
				
				return Native.SteamAPI_ISteamNetworking_RetrieveDataFromSocket(_ptr, hSocket, pubDest, cubDest, out pcubMsgSize);
			}
			public virtual bool /*bool*/ ISteamNetworking_IsDataAvailable( uint hListenSocket, out uint /*uint32 **/ pcubMsgSize, ref uint phSocket )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamNetworking _ptr is null!" );
				
				return Native.SteamAPI_ISteamNetworking_IsDataAvailable(_ptr, hListenSocket, out pcubMsgSize, ref phSocket);
			}
			public virtual bool /*bool*/ ISteamNetworking_RetrieveData( uint hListenSocket, IntPtr /*void **/ pubDest, uint /*uint32*/ cubDest, out uint /*uint32 **/ pcubMsgSize, ref uint phSocket )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamNetworking _ptr is null!" );
				
				return Native.SteamAPI_ISteamNetworking_RetrieveData(_ptr, hListenSocket, pubDest, cubDest, out pcubMsgSize, ref phSocket);
			}
			public virtual bool /*bool*/ ISteamNetworking_GetSocketInfo( uint hSocket, out ulong pSteamIDRemote, IntPtr /*int **/ peSocketStatus, out uint /*uint32 **/ punIPRemote, out ushort /*uint16 **/ punPortRemote )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamNetworking _ptr is null!" );
				
				return Native.SteamAPI_ISteamNetworking_GetSocketInfo(_ptr, hSocket, out pSteamIDRemote, peSocketStatus, out punIPRemote, out punPortRemote);
			}
			public virtual bool /*bool*/ ISteamNetworking_GetListenSocketInfo( uint hListenSocket, out uint /*uint32 **/ pnIP, out ushort /*uint16 **/ pnPort )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamNetworking _ptr is null!" );
				
				return Native.SteamAPI_ISteamNetworking_GetListenSocketInfo(_ptr, hListenSocket, out pnIP, out pnPort);
			}
			public virtual SNetSocketConnectionType /*ESNetSocketConnectionType*/ ISteamNetworking_GetSocketConnectionType( uint hSocket )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamNetworking _ptr is null!" );
				
				return Native.SteamAPI_ISteamNetworking_GetSocketConnectionType(_ptr, hSocket);
			}
			public virtual int /*int*/ ISteamNetworking_GetMaxPacketSize( uint hSocket )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamNetworking _ptr is null!" );
				
				return Native.SteamAPI_ISteamNetworking_GetMaxPacketSize(_ptr, hSocket);
			}
			
			public virtual ScreenshotHandle /*(ScreenshotHandle)*/ ISteamScreenshots_WriteScreenshot( IntPtr /*void **/ pubRGB, uint /*uint32*/ cubRGB, int /*int*/ nWidth, int /*int*/ nHeight )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamScreenshots _ptr is null!" );
				
				return Native.SteamAPI_ISteamScreenshots_WriteScreenshot(_ptr, pubRGB, cubRGB, nWidth, nHeight);
			}
			public virtual ScreenshotHandle /*(ScreenshotHandle)*/ ISteamScreenshots_AddScreenshotToLibrary( string /*const char **/ pchFilename, string /*const char **/ pchThumbnailFilename, int /*int*/ nWidth, int /*int*/ nHeight )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamScreenshots _ptr is null!" );
				
				return Native.SteamAPI_ISteamScreenshots_AddScreenshotToLibrary(_ptr, pchFilename, pchThumbnailFilename, nWidth, nHeight);
			}
			public virtual void /*void*/ ISteamScreenshots_TriggerScreenshot()
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamScreenshots _ptr is null!" );
				
				Native.SteamAPI_ISteamScreenshots_TriggerScreenshot(_ptr);
			}
			public virtual void /*void*/ ISteamScreenshots_HookScreenshots( [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bHook )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamScreenshots _ptr is null!" );
				
				Native.SteamAPI_ISteamScreenshots_HookScreenshots(_ptr, bHook);
			}
			public virtual bool /*bool*/ ISteamScreenshots_SetLocation( uint hScreenshot, string /*const char **/ pchLocation )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamScreenshots _ptr is null!" );
				
				return Native.SteamAPI_ISteamScreenshots_SetLocation(_ptr, hScreenshot, pchLocation);
			}
			public virtual bool /*bool*/ ISteamScreenshots_TagUser( uint hScreenshot, ulong steamID )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamScreenshots _ptr is null!" );
				
				return Native.SteamAPI_ISteamScreenshots_TagUser(_ptr, hScreenshot, steamID);
			}
			public virtual bool /*bool*/ ISteamScreenshots_TagPublishedFile( uint hScreenshot, ulong unPublishedFileID )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamScreenshots _ptr is null!" );
				
				return Native.SteamAPI_ISteamScreenshots_TagPublishedFile(_ptr, hScreenshot, unPublishedFileID);
			}
			public virtual bool /*bool*/ ISteamScreenshots_IsScreenshotsHooked()
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamScreenshots _ptr is null!" );
				
				return Native.SteamAPI_ISteamScreenshots_IsScreenshotsHooked(_ptr);
			}
			public virtual ScreenshotHandle /*(ScreenshotHandle)*/ ISteamScreenshots_AddVRScreenshotToLibrary( VRScreenshotType /*EVRScreenshotType*/ eType, string /*const char **/ pchFilename, string /*const char **/ pchVRFilename )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamScreenshots _ptr is null!" );
				
				return Native.SteamAPI_ISteamScreenshots_AddVRScreenshotToLibrary(_ptr, eType, pchFilename, pchVRFilename);
			}
			
			public virtual bool /*bool*/ ISteamMusic_BIsEnabled()
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamMusic _ptr is null!" );
				
				return Native.SteamAPI_ISteamMusic_BIsEnabled(_ptr);
			}
			public virtual bool /*bool*/ ISteamMusic_BIsPlaying()
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamMusic _ptr is null!" );
				
				return Native.SteamAPI_ISteamMusic_BIsPlaying(_ptr);
			}
			public virtual AudioPlayback_Status /*AudioPlayback_Status*/ ISteamMusic_GetPlaybackStatus()
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamMusic _ptr is null!" );
				
				return Native.SteamAPI_ISteamMusic_GetPlaybackStatus(_ptr);
			}
			public virtual void /*void*/ ISteamMusic_Play()
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamMusic _ptr is null!" );
				
				Native.SteamAPI_ISteamMusic_Play(_ptr);
			}
			public virtual void /*void*/ ISteamMusic_Pause()
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamMusic _ptr is null!" );
				
				Native.SteamAPI_ISteamMusic_Pause(_ptr);
			}
			public virtual void /*void*/ ISteamMusic_PlayPrevious()
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamMusic _ptr is null!" );
				
				Native.SteamAPI_ISteamMusic_PlayPrevious(_ptr);
			}
			public virtual void /*void*/ ISteamMusic_PlayNext()
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamMusic _ptr is null!" );
				
				Native.SteamAPI_ISteamMusic_PlayNext(_ptr);
			}
			public virtual void /*void*/ ISteamMusic_SetVolume( float /*float*/ flVolume )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamMusic _ptr is null!" );
				
				Native.SteamAPI_ISteamMusic_SetVolume(_ptr, flVolume);
			}
			public virtual float /*float*/ ISteamMusic_GetVolume()
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamMusic _ptr is null!" );
				
				return Native.SteamAPI_ISteamMusic_GetVolume(_ptr);
			}
			
			public virtual bool /*bool*/ ISteamMusicRemote_RegisterSteamMusicRemote( string /*const char **/ pchName )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamMusicRemote _ptr is null!" );
				
				return Native.SteamAPI_ISteamMusicRemote_RegisterSteamMusicRemote(_ptr, pchName);
			}
			public virtual bool /*bool*/ ISteamMusicRemote_DeregisterSteamMusicRemote()
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamMusicRemote _ptr is null!" );
				
				return Native.SteamAPI_ISteamMusicRemote_DeregisterSteamMusicRemote(_ptr);
			}
			public virtual bool /*bool*/ ISteamMusicRemote_BIsCurrentMusicRemote()
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamMusicRemote _ptr is null!" );
				
				return Native.SteamAPI_ISteamMusicRemote_BIsCurrentMusicRemote(_ptr);
			}
			public virtual bool /*bool*/ ISteamMusicRemote_BActivationSuccess( [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bValue )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamMusicRemote _ptr is null!" );
				
				return Native.SteamAPI_ISteamMusicRemote_BActivationSuccess(_ptr, bValue);
			}
			public virtual bool /*bool*/ ISteamMusicRemote_SetDisplayName( string /*const char **/ pchDisplayName )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamMusicRemote _ptr is null!" );
				
				return Native.SteamAPI_ISteamMusicRemote_SetDisplayName(_ptr, pchDisplayName);
			}
			public virtual bool /*bool*/ ISteamMusicRemote_SetPNGIcon_64x64( IntPtr /*void **/ pvBuffer, uint /*uint32*/ cbBufferLength )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamMusicRemote _ptr is null!" );
				
				return Native.SteamAPI_ISteamMusicRemote_SetPNGIcon_64x64(_ptr, pvBuffer, cbBufferLength);
			}
			public virtual bool /*bool*/ ISteamMusicRemote_EnablePlayPrevious( [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bValue )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamMusicRemote _ptr is null!" );
				
				return Native.SteamAPI_ISteamMusicRemote_EnablePlayPrevious(_ptr, bValue);
			}
			public virtual bool /*bool*/ ISteamMusicRemote_EnablePlayNext( [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bValue )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamMusicRemote _ptr is null!" );
				
				return Native.SteamAPI_ISteamMusicRemote_EnablePlayNext(_ptr, bValue);
			}
			public virtual bool /*bool*/ ISteamMusicRemote_EnableShuffled( [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bValue )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamMusicRemote _ptr is null!" );
				
				return Native.SteamAPI_ISteamMusicRemote_EnableShuffled(_ptr, bValue);
			}
			public virtual bool /*bool*/ ISteamMusicRemote_EnableLooped( [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bValue )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamMusicRemote _ptr is null!" );
				
				return Native.SteamAPI_ISteamMusicRemote_EnableLooped(_ptr, bValue);
			}
			public virtual bool /*bool*/ ISteamMusicRemote_EnableQueue( [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bValue )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamMusicRemote _ptr is null!" );
				
				return Native.SteamAPI_ISteamMusicRemote_EnableQueue(_ptr, bValue);
			}
			public virtual bool /*bool*/ ISteamMusicRemote_EnablePlaylists( [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bValue )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamMusicRemote _ptr is null!" );
				
				return Native.SteamAPI_ISteamMusicRemote_EnablePlaylists(_ptr, bValue);
			}
			public virtual bool /*bool*/ ISteamMusicRemote_UpdatePlaybackStatus( AudioPlayback_Status /*AudioPlayback_Status*/ nStatus )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamMusicRemote _ptr is null!" );
				
				return Native.SteamAPI_ISteamMusicRemote_UpdatePlaybackStatus(_ptr, nStatus);
			}
			public virtual bool /*bool*/ ISteamMusicRemote_UpdateShuffled( [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bValue )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamMusicRemote _ptr is null!" );
				
				return Native.SteamAPI_ISteamMusicRemote_UpdateShuffled(_ptr, bValue);
			}
			public virtual bool /*bool*/ ISteamMusicRemote_UpdateLooped( [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bValue )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamMusicRemote _ptr is null!" );
				
				return Native.SteamAPI_ISteamMusicRemote_UpdateLooped(_ptr, bValue);
			}
			public virtual bool /*bool*/ ISteamMusicRemote_UpdateVolume( float /*float*/ flValue )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamMusicRemote _ptr is null!" );
				
				return Native.SteamAPI_ISteamMusicRemote_UpdateVolume(_ptr, flValue);
			}
			public virtual bool /*bool*/ ISteamMusicRemote_CurrentEntryWillChange()
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamMusicRemote _ptr is null!" );
				
				return Native.SteamAPI_ISteamMusicRemote_CurrentEntryWillChange(_ptr);
			}
			public virtual bool /*bool*/ ISteamMusicRemote_CurrentEntryIsAvailable( [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bAvailable )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamMusicRemote _ptr is null!" );
				
				return Native.SteamAPI_ISteamMusicRemote_CurrentEntryIsAvailable(_ptr, bAvailable);
			}
			public virtual bool /*bool*/ ISteamMusicRemote_UpdateCurrentEntryText( string /*const char **/ pchText )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamMusicRemote _ptr is null!" );
				
				return Native.SteamAPI_ISteamMusicRemote_UpdateCurrentEntryText(_ptr, pchText);
			}
			public virtual bool /*bool*/ ISteamMusicRemote_UpdateCurrentEntryElapsedSeconds( int /*int*/ nValue )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamMusicRemote _ptr is null!" );
				
				return Native.SteamAPI_ISteamMusicRemote_UpdateCurrentEntryElapsedSeconds(_ptr, nValue);
			}
			public virtual bool /*bool*/ ISteamMusicRemote_UpdateCurrentEntryCoverArt( IntPtr /*void **/ pvBuffer, uint /*uint32*/ cbBufferLength )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamMusicRemote _ptr is null!" );
				
				return Native.SteamAPI_ISteamMusicRemote_UpdateCurrentEntryCoverArt(_ptr, pvBuffer, cbBufferLength);
			}
			public virtual bool /*bool*/ ISteamMusicRemote_CurrentEntryDidChange()
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamMusicRemote _ptr is null!" );
				
				return Native.SteamAPI_ISteamMusicRemote_CurrentEntryDidChange(_ptr);
			}
			public virtual bool /*bool*/ ISteamMusicRemote_QueueWillChange()
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamMusicRemote _ptr is null!" );
				
				return Native.SteamAPI_ISteamMusicRemote_QueueWillChange(_ptr);
			}
			public virtual bool /*bool*/ ISteamMusicRemote_ResetQueueEntries()
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamMusicRemote _ptr is null!" );
				
				return Native.SteamAPI_ISteamMusicRemote_ResetQueueEntries(_ptr);
			}
			public virtual bool /*bool*/ ISteamMusicRemote_SetQueueEntry( int /*int*/ nID, int /*int*/ nPosition, string /*const char **/ pchEntryText )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamMusicRemote _ptr is null!" );
				
				return Native.SteamAPI_ISteamMusicRemote_SetQueueEntry(_ptr, nID, nPosition, pchEntryText);
			}
			public virtual bool /*bool*/ ISteamMusicRemote_SetCurrentQueueEntry( int /*int*/ nID )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamMusicRemote _ptr is null!" );
				
				return Native.SteamAPI_ISteamMusicRemote_SetCurrentQueueEntry(_ptr, nID);
			}
			public virtual bool /*bool*/ ISteamMusicRemote_QueueDidChange()
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamMusicRemote _ptr is null!" );
				
				return Native.SteamAPI_ISteamMusicRemote_QueueDidChange(_ptr);
			}
			public virtual bool /*bool*/ ISteamMusicRemote_PlaylistWillChange()
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamMusicRemote _ptr is null!" );
				
				return Native.SteamAPI_ISteamMusicRemote_PlaylistWillChange(_ptr);
			}
			public virtual bool /*bool*/ ISteamMusicRemote_ResetPlaylistEntries()
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamMusicRemote _ptr is null!" );
				
				return Native.SteamAPI_ISteamMusicRemote_ResetPlaylistEntries(_ptr);
			}
			public virtual bool /*bool*/ ISteamMusicRemote_SetPlaylistEntry( int /*int*/ nID, int /*int*/ nPosition, string /*const char **/ pchEntryText )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamMusicRemote _ptr is null!" );
				
				return Native.SteamAPI_ISteamMusicRemote_SetPlaylistEntry(_ptr, nID, nPosition, pchEntryText);
			}
			public virtual bool /*bool*/ ISteamMusicRemote_SetCurrentPlaylistEntry( int /*int*/ nID )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamMusicRemote _ptr is null!" );
				
				return Native.SteamAPI_ISteamMusicRemote_SetCurrentPlaylistEntry(_ptr, nID);
			}
			public virtual bool /*bool*/ ISteamMusicRemote_PlaylistDidChange()
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamMusicRemote _ptr is null!" );
				
				return Native.SteamAPI_ISteamMusicRemote_PlaylistDidChange(_ptr);
			}
			
			public virtual HTTPRequestHandle /*(HTTPRequestHandle)*/ ISteamHTTP_CreateHTTPRequest( HTTPMethod /*EHTTPMethod*/ eHTTPRequestMethod, string /*const char **/ pchAbsoluteURL )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamHTTP _ptr is null!" );
				
				return Native.SteamAPI_ISteamHTTP_CreateHTTPRequest(_ptr, eHTTPRequestMethod, pchAbsoluteURL);
			}
			public virtual bool /*bool*/ ISteamHTTP_SetHTTPRequestContextValue( uint hRequest, ulong /*uint64*/ ulContextValue )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamHTTP _ptr is null!" );
				
				return Native.SteamAPI_ISteamHTTP_SetHTTPRequestContextValue(_ptr, hRequest, ulContextValue);
			}
			public virtual bool /*bool*/ ISteamHTTP_SetHTTPRequestNetworkActivityTimeout( uint hRequest, uint /*uint32*/ unTimeoutSeconds )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamHTTP _ptr is null!" );
				
				return Native.SteamAPI_ISteamHTTP_SetHTTPRequestNetworkActivityTimeout(_ptr, hRequest, unTimeoutSeconds);
			}
			public virtual bool /*bool*/ ISteamHTTP_SetHTTPRequestHeaderValue( uint hRequest, string /*const char **/ pchHeaderName, string /*const char **/ pchHeaderValue )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamHTTP _ptr is null!" );
				
				return Native.SteamAPI_ISteamHTTP_SetHTTPRequestHeaderValue(_ptr, hRequest, pchHeaderName, pchHeaderValue);
			}
			public virtual bool /*bool*/ ISteamHTTP_SetHTTPRequestGetOrPostParameter( uint hRequest, string /*const char **/ pchParamName, string /*const char **/ pchParamValue )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamHTTP _ptr is null!" );
				
				return Native.SteamAPI_ISteamHTTP_SetHTTPRequestGetOrPostParameter(_ptr, hRequest, pchParamName, pchParamValue);
			}
			public virtual bool /*bool*/ ISteamHTTP_SendHTTPRequest( uint hRequest, ref ulong pCallHandle )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamHTTP _ptr is null!" );
				
				return Native.SteamAPI_ISteamHTTP_SendHTTPRequest(_ptr, hRequest, ref pCallHandle);
			}
			public virtual bool /*bool*/ ISteamHTTP_SendHTTPRequestAndStreamResponse( uint hRequest, ref ulong pCallHandle )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamHTTP _ptr is null!" );
				
				return Native.SteamAPI_ISteamHTTP_SendHTTPRequestAndStreamResponse(_ptr, hRequest, ref pCallHandle);
			}
			public virtual bool /*bool*/ ISteamHTTP_DeferHTTPRequest( uint hRequest )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamHTTP _ptr is null!" );
				
				return Native.SteamAPI_ISteamHTTP_DeferHTTPRequest(_ptr, hRequest);
			}
			public virtual bool /*bool*/ ISteamHTTP_PrioritizeHTTPRequest( uint hRequest )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamHTTP _ptr is null!" );
				
				return Native.SteamAPI_ISteamHTTP_PrioritizeHTTPRequest(_ptr, hRequest);
			}
			public virtual bool /*bool*/ ISteamHTTP_GetHTTPResponseHeaderSize( uint hRequest, string /*const char **/ pchHeaderName, out uint /*uint32 **/ unResponseHeaderSize )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamHTTP _ptr is null!" );
				
				return Native.SteamAPI_ISteamHTTP_GetHTTPResponseHeaderSize(_ptr, hRequest, pchHeaderName, out unResponseHeaderSize);
			}
			public virtual bool /*bool*/ ISteamHTTP_GetHTTPResponseHeaderValue( uint hRequest, string /*const char **/ pchHeaderName, out byte /*uint8 **/ pHeaderValueBuffer, uint /*uint32*/ unBufferSize )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamHTTP _ptr is null!" );
				
				return Native.SteamAPI_ISteamHTTP_GetHTTPResponseHeaderValue(_ptr, hRequest, pchHeaderName, out pHeaderValueBuffer, unBufferSize);
			}
			public virtual bool /*bool*/ ISteamHTTP_GetHTTPResponseBodySize( uint hRequest, out uint /*uint32 **/ unBodySize )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamHTTP _ptr is null!" );
				
				return Native.SteamAPI_ISteamHTTP_GetHTTPResponseBodySize(_ptr, hRequest, out unBodySize);
			}
			public virtual bool /*bool*/ ISteamHTTP_GetHTTPResponseBodyData( uint hRequest, out byte /*uint8 **/ pBodyDataBuffer, uint /*uint32*/ unBufferSize )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamHTTP _ptr is null!" );
				
				return Native.SteamAPI_ISteamHTTP_GetHTTPResponseBodyData(_ptr, hRequest, out pBodyDataBuffer, unBufferSize);
			}
			public virtual bool /*bool*/ ISteamHTTP_GetHTTPStreamingResponseBodyData( uint hRequest, uint /*uint32*/ cOffset, out byte /*uint8 **/ pBodyDataBuffer, uint /*uint32*/ unBufferSize )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamHTTP _ptr is null!" );
				
				return Native.SteamAPI_ISteamHTTP_GetHTTPStreamingResponseBodyData(_ptr, hRequest, cOffset, out pBodyDataBuffer, unBufferSize);
			}
			public virtual bool /*bool*/ ISteamHTTP_ReleaseHTTPRequest( uint hRequest )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamHTTP _ptr is null!" );
				
				return Native.SteamAPI_ISteamHTTP_ReleaseHTTPRequest(_ptr, hRequest);
			}
			public virtual bool /*bool*/ ISteamHTTP_GetHTTPDownloadProgressPct( uint hRequest, out float /*float **/ pflPercentOut )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamHTTP _ptr is null!" );
				
				return Native.SteamAPI_ISteamHTTP_GetHTTPDownloadProgressPct(_ptr, hRequest, out pflPercentOut);
			}
			public virtual bool /*bool*/ ISteamHTTP_SetHTTPRequestRawPostBody( uint hRequest, string /*const char **/ pchContentType, out byte /*uint8 **/ pubBody, uint /*uint32*/ unBodyLen )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamHTTP _ptr is null!" );
				
				return Native.SteamAPI_ISteamHTTP_SetHTTPRequestRawPostBody(_ptr, hRequest, pchContentType, out pubBody, unBodyLen);
			}
			public virtual HTTPCookieContainerHandle /*(HTTPCookieContainerHandle)*/ ISteamHTTP_CreateCookieContainer( [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bAllowResponsesToModify )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamHTTP _ptr is null!" );
				
				return Native.SteamAPI_ISteamHTTP_CreateCookieContainer(_ptr, bAllowResponsesToModify);
			}
			public virtual bool /*bool*/ ISteamHTTP_ReleaseCookieContainer( uint hCookieContainer )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamHTTP _ptr is null!" );
				
				return Native.SteamAPI_ISteamHTTP_ReleaseCookieContainer(_ptr, hCookieContainer);
			}
			public virtual bool /*bool*/ ISteamHTTP_SetCookie( uint hCookieContainer, string /*const char **/ pchHost, string /*const char **/ pchUrl, string /*const char **/ pchCookie )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamHTTP _ptr is null!" );
				
				return Native.SteamAPI_ISteamHTTP_SetCookie(_ptr, hCookieContainer, pchHost, pchUrl, pchCookie);
			}
			public virtual bool /*bool*/ ISteamHTTP_SetHTTPRequestCookieContainer( uint hRequest, uint hCookieContainer )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamHTTP _ptr is null!" );
				
				return Native.SteamAPI_ISteamHTTP_SetHTTPRequestCookieContainer(_ptr, hRequest, hCookieContainer);
			}
			public virtual bool /*bool*/ ISteamHTTP_SetHTTPRequestUserAgentInfo( uint hRequest, string /*const char **/ pchUserAgentInfo )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamHTTP _ptr is null!" );
				
				return Native.SteamAPI_ISteamHTTP_SetHTTPRequestUserAgentInfo(_ptr, hRequest, pchUserAgentInfo);
			}
			public virtual bool /*bool*/ ISteamHTTP_SetHTTPRequestRequiresVerifiedCertificate( uint hRequest, [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bRequireVerifiedCertificate )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamHTTP _ptr is null!" );
				
				return Native.SteamAPI_ISteamHTTP_SetHTTPRequestRequiresVerifiedCertificate(_ptr, hRequest, bRequireVerifiedCertificate);
			}
			public virtual bool /*bool*/ ISteamHTTP_SetHTTPRequestAbsoluteTimeoutMS( uint hRequest, uint /*uint32*/ unMilliseconds )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamHTTP _ptr is null!" );
				
				return Native.SteamAPI_ISteamHTTP_SetHTTPRequestAbsoluteTimeoutMS(_ptr, hRequest, unMilliseconds);
			}
			public virtual bool /*bool*/ ISteamHTTP_GetHTTPRequestWasTimedOut( uint hRequest, [MarshalAs(UnmanagedType.U1)] ref bool /*bool **/ pbWasTimedOut )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamHTTP _ptr is null!" );
				
				return Native.SteamAPI_ISteamHTTP_GetHTTPRequestWasTimedOut(_ptr, hRequest, ref pbWasTimedOut);
			}
			
			public virtual bool /*bool*/ ISteamInput_Init()
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamInput _ptr is null!" );
				
				return Native.SteamAPI_ISteamInput_Init(_ptr);
			}
			public virtual bool /*bool*/ ISteamInput_Shutdown()
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamInput _ptr is null!" );
				
				return Native.SteamAPI_ISteamInput_Shutdown(_ptr);
			}
			public virtual void /*void*/ ISteamInput_RunFrame()
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamInput _ptr is null!" );
				
				Native.SteamAPI_ISteamInput_RunFrame(_ptr);
			}
			public virtual int /*int*/ ISteamInput_GetConnectedControllers( IntPtr /*InputHandle_t **/ handlesOut )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamInput _ptr is null!" );
				
				return Native.SteamAPI_ISteamInput_GetConnectedControllers(_ptr, handlesOut);
			}
			public virtual InputActionSetHandle_t /*(InputActionSetHandle_t)*/ ISteamInput_GetActionSetHandle( string /*const char **/ pszActionSetName )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamInput _ptr is null!" );
				
				return Native.SteamAPI_ISteamInput_GetActionSetHandle(_ptr, pszActionSetName);
			}
			public virtual void /*void*/ ISteamInput_ActivateActionSet( ulong inputHandle, ulong actionSetHandle )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamInput _ptr is null!" );
				
				Native.SteamAPI_ISteamInput_ActivateActionSet(_ptr, inputHandle, actionSetHandle);
			}
			public virtual InputActionSetHandle_t /*(InputActionSetHandle_t)*/ ISteamInput_GetCurrentActionSet( ulong inputHandle )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamInput _ptr is null!" );
				
				return Native.SteamAPI_ISteamInput_GetCurrentActionSet(_ptr, inputHandle);
			}
			public virtual void /*void*/ ISteamInput_ActivateActionSetLayer( ulong inputHandle, ulong actionSetLayerHandle )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamInput _ptr is null!" );
				
				Native.SteamAPI_ISteamInput_ActivateActionSetLayer(_ptr, inputHandle, actionSetLayerHandle);
			}
			public virtual void /*void*/ ISteamInput_DeactivateActionSetLayer( ulong inputHandle, ulong actionSetLayerHandle )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamInput _ptr is null!" );
				
				Native.SteamAPI_ISteamInput_DeactivateActionSetLayer(_ptr, inputHandle, actionSetLayerHandle);
			}
			public virtual void /*void*/ ISteamInput_DeactivateAllActionSetLayers( ulong inputHandle )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamInput _ptr is null!" );
				
				Native.SteamAPI_ISteamInput_DeactivateAllActionSetLayers(_ptr, inputHandle);
			}
			public virtual int /*int*/ ISteamInput_GetActiveActionSetLayers( ulong inputHandle, IntPtr /*InputActionSetHandle_t **/ handlesOut )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamInput _ptr is null!" );
				
				return Native.SteamAPI_ISteamInput_GetActiveActionSetLayers(_ptr, inputHandle, handlesOut);
			}
			public virtual InputDigitalActionHandle_t /*(InputDigitalActionHandle_t)*/ ISteamInput_GetDigitalActionHandle( string /*const char **/ pszActionName )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamInput _ptr is null!" );
				
				return Native.SteamAPI_ISteamInput_GetDigitalActionHandle(_ptr, pszActionName);
			}
			public virtual InputDigitalActionData_t /*struct InputDigitalActionData_t*/ ISteamInput_GetDigitalActionData( ulong inputHandle, ulong digitalActionHandle )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamInput _ptr is null!" );
				
				return Native.SteamAPI_ISteamInput_GetDigitalActionData(_ptr, inputHandle, digitalActionHandle);
			}
			public virtual int /*int*/ ISteamInput_GetDigitalActionOrigins( ulong inputHandle, ulong actionSetHandle, ulong digitalActionHandle, out InputActionOrigin /*EInputActionOrigin **/ originsOut )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamInput _ptr is null!" );
				
				return Native.SteamAPI_ISteamInput_GetDigitalActionOrigins(_ptr, inputHandle, actionSetHandle, digitalActionHandle, out originsOut);
			}
			public virtual InputAnalogActionHandle_t /*(InputAnalogActionHandle_t)*/ ISteamInput_GetAnalogActionHandle( string /*const char **/ pszActionName )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamInput _ptr is null!" );
				
				return Native.SteamAPI_ISteamInput_GetAnalogActionHandle(_ptr, pszActionName);
			}
			public virtual InputAnalogActionData_t /*struct InputAnalogActionData_t*/ ISteamInput_GetAnalogActionData( ulong inputHandle, ulong analogActionHandle )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamInput _ptr is null!" );
				
				return Native.SteamAPI_ISteamInput_GetAnalogActionData(_ptr, inputHandle, analogActionHandle);
			}
			public virtual int /*int*/ ISteamInput_GetAnalogActionOrigins( ulong inputHandle, ulong actionSetHandle, ulong analogActionHandle, out InputActionOrigin /*EInputActionOrigin **/ originsOut )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamInput _ptr is null!" );
				
				return Native.SteamAPI_ISteamInput_GetAnalogActionOrigins(_ptr, inputHandle, actionSetHandle, analogActionHandle, out originsOut);
			}
			public virtual IntPtr ISteamInput_GetGlyphForActionOrigin( InputActionOrigin /*EInputActionOrigin*/ eOrigin )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamInput _ptr is null!" );
				
				return Native.SteamAPI_ISteamInput_GetGlyphForActionOrigin(_ptr, eOrigin);
			}
			public virtual IntPtr ISteamInput_GetStringForActionOrigin( InputActionOrigin /*EInputActionOrigin*/ eOrigin )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamInput _ptr is null!" );
				
				return Native.SteamAPI_ISteamInput_GetStringForActionOrigin(_ptr, eOrigin);
			}
			public virtual void /*void*/ ISteamInput_StopAnalogActionMomentum( ulong inputHandle, ulong eAction )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamInput _ptr is null!" );
				
				Native.SteamAPI_ISteamInput_StopAnalogActionMomentum(_ptr, inputHandle, eAction);
			}
			public virtual InputMotionData_t /*struct InputMotionData_t*/ ISteamInput_GetMotionData( ulong inputHandle )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamInput _ptr is null!" );
				
				return Native.SteamAPI_ISteamInput_GetMotionData(_ptr, inputHandle);
			}
			public virtual void /*void*/ ISteamInput_TriggerVibration( ulong inputHandle, ushort /*unsigned short*/ usLeftSpeed, ushort /*unsigned short*/ usRightSpeed )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamInput _ptr is null!" );
				
				Native.SteamAPI_ISteamInput_TriggerVibration(_ptr, inputHandle, usLeftSpeed, usRightSpeed);
			}
			public virtual void /*void*/ ISteamInput_SetLEDColor( ulong inputHandle, byte /*uint8*/ nColorR, byte /*uint8*/ nColorG, byte /*uint8*/ nColorB, uint /*unsigned int*/ nFlags )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamInput _ptr is null!" );
				
				Native.SteamAPI_ISteamInput_SetLEDColor(_ptr, inputHandle, nColorR, nColorG, nColorB, nFlags);
			}
			public virtual void /*void*/ ISteamInput_TriggerHapticPulse( ulong inputHandle, SteamControllerPad /*ESteamControllerPad*/ eTargetPad, ushort /*unsigned short*/ usDurationMicroSec )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamInput _ptr is null!" );
				
				Native.SteamAPI_ISteamInput_TriggerHapticPulse(_ptr, inputHandle, eTargetPad, usDurationMicroSec);
			}
			public virtual void /*void*/ ISteamInput_TriggerRepeatedHapticPulse( ulong inputHandle, SteamControllerPad /*ESteamControllerPad*/ eTargetPad, ushort /*unsigned short*/ usDurationMicroSec, ushort /*unsigned short*/ usOffMicroSec, ushort /*unsigned short*/ unRepeat, uint /*unsigned int*/ nFlags )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamInput _ptr is null!" );
				
				Native.SteamAPI_ISteamInput_TriggerRepeatedHapticPulse(_ptr, inputHandle, eTargetPad, usDurationMicroSec, usOffMicroSec, unRepeat, nFlags);
			}
			public virtual bool /*bool*/ ISteamInput_ShowBindingPanel( ulong inputHandle )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamInput _ptr is null!" );
				
				return Native.SteamAPI_ISteamInput_ShowBindingPanel(_ptr, inputHandle);
			}
			public virtual SteamInputType /*ESteamInputType*/ ISteamInput_GetInputTypeForHandle( ulong inputHandle )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamInput _ptr is null!" );
				
				return Native.SteamAPI_ISteamInput_GetInputTypeForHandle(_ptr, inputHandle);
			}
			public virtual InputHandle_t /*(InputHandle_t)*/ ISteamInput_GetControllerForGamepadIndex( int /*int*/ nIndex )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamInput _ptr is null!" );
				
				return Native.SteamAPI_ISteamInput_GetControllerForGamepadIndex(_ptr, nIndex);
			}
			public virtual int /*int*/ ISteamInput_GetGamepadIndexForController( ulong ulinputHandle )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamInput _ptr is null!" );
				
				return Native.SteamAPI_ISteamInput_GetGamepadIndexForController(_ptr, ulinputHandle);
			}
			public virtual IntPtr ISteamInput_GetStringForXboxOrigin( XboxOrigin /*EXboxOrigin*/ eOrigin )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamInput _ptr is null!" );
				
				return Native.SteamAPI_ISteamInput_GetStringForXboxOrigin(_ptr, eOrigin);
			}
			public virtual IntPtr ISteamInput_GetGlyphForXboxOrigin( XboxOrigin /*EXboxOrigin*/ eOrigin )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamInput _ptr is null!" );
				
				return Native.SteamAPI_ISteamInput_GetGlyphForXboxOrigin(_ptr, eOrigin);
			}
			public virtual InputActionOrigin /*EInputActionOrigin*/ ISteamInput_GetActionOriginFromXboxOrigin( ulong inputHandle, XboxOrigin /*EXboxOrigin*/ eOrigin )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamInput _ptr is null!" );
				
				return Native.SteamAPI_ISteamInput_GetActionOriginFromXboxOrigin(_ptr, inputHandle, eOrigin);
			}
			public virtual InputActionOrigin /*EInputActionOrigin*/ ISteamInput_TranslateActionOrigin( SteamInputType /*ESteamInputType*/ eDestinationInputType, InputActionOrigin /*EInputActionOrigin*/ eSourceOrigin )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamInput _ptr is null!" );
				
				return Native.SteamAPI_ISteamInput_TranslateActionOrigin(_ptr, eDestinationInputType, eSourceOrigin);
			}
			
			public virtual bool /*bool*/ ISteamController_Init()
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamController _ptr is null!" );
				
				return Native.SteamAPI_ISteamController_Init(_ptr);
			}
			public virtual bool /*bool*/ ISteamController_Shutdown()
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamController _ptr is null!" );
				
				return Native.SteamAPI_ISteamController_Shutdown(_ptr);
			}
			public virtual void /*void*/ ISteamController_RunFrame()
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamController _ptr is null!" );
				
				Native.SteamAPI_ISteamController_RunFrame(_ptr);
			}
			public virtual int /*int*/ ISteamController_GetConnectedControllers( IntPtr /*ControllerHandle_t **/ handlesOut )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamController _ptr is null!" );
				
				return Native.SteamAPI_ISteamController_GetConnectedControllers(_ptr, handlesOut);
			}
			public virtual ControllerActionSetHandle_t /*(ControllerActionSetHandle_t)*/ ISteamController_GetActionSetHandle( string /*const char **/ pszActionSetName )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamController _ptr is null!" );
				
				return Native.SteamAPI_ISteamController_GetActionSetHandle(_ptr, pszActionSetName);
			}
			public virtual void /*void*/ ISteamController_ActivateActionSet( ulong controllerHandle, ulong actionSetHandle )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamController _ptr is null!" );
				
				Native.SteamAPI_ISteamController_ActivateActionSet(_ptr, controllerHandle, actionSetHandle);
			}
			public virtual ControllerActionSetHandle_t /*(ControllerActionSetHandle_t)*/ ISteamController_GetCurrentActionSet( ulong controllerHandle )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamController _ptr is null!" );
				
				return Native.SteamAPI_ISteamController_GetCurrentActionSet(_ptr, controllerHandle);
			}
			public virtual void /*void*/ ISteamController_ActivateActionSetLayer( ulong controllerHandle, ulong actionSetLayerHandle )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamController _ptr is null!" );
				
				Native.SteamAPI_ISteamController_ActivateActionSetLayer(_ptr, controllerHandle, actionSetLayerHandle);
			}
			public virtual void /*void*/ ISteamController_DeactivateActionSetLayer( ulong controllerHandle, ulong actionSetLayerHandle )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamController _ptr is null!" );
				
				Native.SteamAPI_ISteamController_DeactivateActionSetLayer(_ptr, controllerHandle, actionSetLayerHandle);
			}
			public virtual void /*void*/ ISteamController_DeactivateAllActionSetLayers( ulong controllerHandle )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamController _ptr is null!" );
				
				Native.SteamAPI_ISteamController_DeactivateAllActionSetLayers(_ptr, controllerHandle);
			}
			public virtual int /*int*/ ISteamController_GetActiveActionSetLayers( ulong controllerHandle, IntPtr /*ControllerActionSetHandle_t **/ handlesOut )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamController _ptr is null!" );
				
				return Native.SteamAPI_ISteamController_GetActiveActionSetLayers(_ptr, controllerHandle, handlesOut);
			}
			public virtual ControllerDigitalActionHandle_t /*(ControllerDigitalActionHandle_t)*/ ISteamController_GetDigitalActionHandle( string /*const char **/ pszActionName )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamController _ptr is null!" );
				
				return Native.SteamAPI_ISteamController_GetDigitalActionHandle(_ptr, pszActionName);
			}
			public virtual InputDigitalActionData_t /*struct InputDigitalActionData_t*/ ISteamController_GetDigitalActionData( ulong controllerHandle, ulong digitalActionHandle )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamController _ptr is null!" );
				
				return Native.SteamAPI_ISteamController_GetDigitalActionData(_ptr, controllerHandle, digitalActionHandle);
			}
			public virtual int /*int*/ ISteamController_GetDigitalActionOrigins( ulong controllerHandle, ulong actionSetHandle, ulong digitalActionHandle, out ControllerActionOrigin /*EControllerActionOrigin **/ originsOut )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamController _ptr is null!" );
				
				return Native.SteamAPI_ISteamController_GetDigitalActionOrigins(_ptr, controllerHandle, actionSetHandle, digitalActionHandle, out originsOut);
			}
			public virtual ControllerAnalogActionHandle_t /*(ControllerAnalogActionHandle_t)*/ ISteamController_GetAnalogActionHandle( string /*const char **/ pszActionName )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamController _ptr is null!" );
				
				return Native.SteamAPI_ISteamController_GetAnalogActionHandle(_ptr, pszActionName);
			}
			public virtual InputAnalogActionData_t /*struct InputAnalogActionData_t*/ ISteamController_GetAnalogActionData( ulong controllerHandle, ulong analogActionHandle )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamController _ptr is null!" );
				
				return Native.SteamAPI_ISteamController_GetAnalogActionData(_ptr, controllerHandle, analogActionHandle);
			}
			public virtual int /*int*/ ISteamController_GetAnalogActionOrigins( ulong controllerHandle, ulong actionSetHandle, ulong analogActionHandle, out ControllerActionOrigin /*EControllerActionOrigin **/ originsOut )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamController _ptr is null!" );
				
				return Native.SteamAPI_ISteamController_GetAnalogActionOrigins(_ptr, controllerHandle, actionSetHandle, analogActionHandle, out originsOut);
			}
			public virtual IntPtr ISteamController_GetGlyphForActionOrigin( ControllerActionOrigin /*EControllerActionOrigin*/ eOrigin )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamController _ptr is null!" );
				
				return Native.SteamAPI_ISteamController_GetGlyphForActionOrigin(_ptr, eOrigin);
			}
			public virtual IntPtr ISteamController_GetStringForActionOrigin( ControllerActionOrigin /*EControllerActionOrigin*/ eOrigin )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamController _ptr is null!" );
				
				return Native.SteamAPI_ISteamController_GetStringForActionOrigin(_ptr, eOrigin);
			}
			public virtual void /*void*/ ISteamController_StopAnalogActionMomentum( ulong controllerHandle, ulong eAction )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamController _ptr is null!" );
				
				Native.SteamAPI_ISteamController_StopAnalogActionMomentum(_ptr, controllerHandle, eAction);
			}
			public virtual InputMotionData_t /*struct InputMotionData_t*/ ISteamController_GetMotionData( ulong controllerHandle )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamController _ptr is null!" );
				
				return Native.SteamAPI_ISteamController_GetMotionData(_ptr, controllerHandle);
			}
			public virtual void /*void*/ ISteamController_TriggerHapticPulse( ulong controllerHandle, SteamControllerPad /*ESteamControllerPad*/ eTargetPad, ushort /*unsigned short*/ usDurationMicroSec )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamController _ptr is null!" );
				
				Native.SteamAPI_ISteamController_TriggerHapticPulse(_ptr, controllerHandle, eTargetPad, usDurationMicroSec);
			}
			public virtual void /*void*/ ISteamController_TriggerRepeatedHapticPulse( ulong controllerHandle, SteamControllerPad /*ESteamControllerPad*/ eTargetPad, ushort /*unsigned short*/ usDurationMicroSec, ushort /*unsigned short*/ usOffMicroSec, ushort /*unsigned short*/ unRepeat, uint /*unsigned int*/ nFlags )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamController _ptr is null!" );
				
				Native.SteamAPI_ISteamController_TriggerRepeatedHapticPulse(_ptr, controllerHandle, eTargetPad, usDurationMicroSec, usOffMicroSec, unRepeat, nFlags);
			}
			public virtual void /*void*/ ISteamController_TriggerVibration( ulong controllerHandle, ushort /*unsigned short*/ usLeftSpeed, ushort /*unsigned short*/ usRightSpeed )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamController _ptr is null!" );
				
				Native.SteamAPI_ISteamController_TriggerVibration(_ptr, controllerHandle, usLeftSpeed, usRightSpeed);
			}
			public virtual void /*void*/ ISteamController_SetLEDColor( ulong controllerHandle, byte /*uint8*/ nColorR, byte /*uint8*/ nColorG, byte /*uint8*/ nColorB, uint /*unsigned int*/ nFlags )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamController _ptr is null!" );
				
				Native.SteamAPI_ISteamController_SetLEDColor(_ptr, controllerHandle, nColorR, nColorG, nColorB, nFlags);
			}
			public virtual bool /*bool*/ ISteamController_ShowBindingPanel( ulong controllerHandle )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamController _ptr is null!" );
				
				return Native.SteamAPI_ISteamController_ShowBindingPanel(_ptr, controllerHandle);
			}
			public virtual SteamInputType /*ESteamInputType*/ ISteamController_GetInputTypeForHandle( ulong controllerHandle )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamController _ptr is null!" );
				
				return Native.SteamAPI_ISteamController_GetInputTypeForHandle(_ptr, controllerHandle);
			}
			public virtual ControllerHandle_t /*(ControllerHandle_t)*/ ISteamController_GetControllerForGamepadIndex( int /*int*/ nIndex )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamController _ptr is null!" );
				
				return Native.SteamAPI_ISteamController_GetControllerForGamepadIndex(_ptr, nIndex);
			}
			public virtual int /*int*/ ISteamController_GetGamepadIndexForController( ulong ulControllerHandle )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamController _ptr is null!" );
				
				return Native.SteamAPI_ISteamController_GetGamepadIndexForController(_ptr, ulControllerHandle);
			}
			public virtual IntPtr ISteamController_GetStringForXboxOrigin( XboxOrigin /*EXboxOrigin*/ eOrigin )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamController _ptr is null!" );
				
				return Native.SteamAPI_ISteamController_GetStringForXboxOrigin(_ptr, eOrigin);
			}
			public virtual IntPtr ISteamController_GetGlyphForXboxOrigin( XboxOrigin /*EXboxOrigin*/ eOrigin )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamController _ptr is null!" );
				
				return Native.SteamAPI_ISteamController_GetGlyphForXboxOrigin(_ptr, eOrigin);
			}
			public virtual ControllerActionOrigin /*EControllerActionOrigin*/ ISteamController_GetActionOriginFromXboxOrigin( ulong controllerHandle, XboxOrigin /*EXboxOrigin*/ eOrigin )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamController _ptr is null!" );
				
				return Native.SteamAPI_ISteamController_GetActionOriginFromXboxOrigin(_ptr, controllerHandle, eOrigin);
			}
			public virtual ControllerActionOrigin /*EControllerActionOrigin*/ ISteamController_TranslateActionOrigin( SteamInputType /*ESteamInputType*/ eDestinationInputType, ControllerActionOrigin /*EControllerActionOrigin*/ eSourceOrigin )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamController _ptr is null!" );
				
				return Native.SteamAPI_ISteamController_TranslateActionOrigin(_ptr, eDestinationInputType, eSourceOrigin);
			}
			
			public virtual UGCQueryHandle_t /*(UGCQueryHandle_t)*/ ISteamUGC_CreateQueryUserUGCRequest( uint unAccountID, UserUGCList /*EUserUGCList*/ eListType, UGCMatchingUGCType /*EUGCMatchingUGCType*/ eMatchingUGCType, UserUGCListSortOrder /*EUserUGCListSortOrder*/ eSortOrder, uint nCreatorAppID, uint nConsumerAppID, uint /*uint32*/ unPage )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamUGC _ptr is null!" );
				
				return Native.SteamAPI_ISteamUGC_CreateQueryUserUGCRequest(_ptr, unAccountID, eListType, eMatchingUGCType, eSortOrder, nCreatorAppID, nConsumerAppID, unPage);
			}
			public virtual UGCQueryHandle_t /*(UGCQueryHandle_t)*/ ISteamUGC_CreateQueryAllUGCRequest( UGCQuery /*EUGCQuery*/ eQueryType, UGCMatchingUGCType /*EUGCMatchingUGCType*/ eMatchingeMatchingUGCTypeFileType, uint nCreatorAppID, uint nConsumerAppID, uint /*uint32*/ unPage )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamUGC _ptr is null!" );
				
				return Native.SteamAPI_ISteamUGC_CreateQueryAllUGCRequest(_ptr, eQueryType, eMatchingeMatchingUGCTypeFileType, nCreatorAppID, nConsumerAppID, unPage);
			}
			public virtual UGCQueryHandle_t /*(UGCQueryHandle_t)*/ ISteamUGC_CreateQueryAllUGCRequest0( UGCQuery /*EUGCQuery*/ eQueryType, UGCMatchingUGCType /*EUGCMatchingUGCType*/ eMatchingeMatchingUGCTypeFileType, uint nCreatorAppID, uint nConsumerAppID, string /*const char **/ pchCursor )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamUGC _ptr is null!" );
				
				return Native.SteamAPI_ISteamUGC_CreateQueryAllUGCRequest0(_ptr, eQueryType, eMatchingeMatchingUGCTypeFileType, nCreatorAppID, nConsumerAppID, pchCursor);
			}
			public virtual UGCQueryHandle_t /*(UGCQueryHandle_t)*/ ISteamUGC_CreateQueryUGCDetailsRequest( IntPtr /*PublishedFileId_t **/ pvecPublishedFileID, uint /*uint32*/ unNumPublishedFileIDs )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamUGC _ptr is null!" );
				
				return Native.SteamAPI_ISteamUGC_CreateQueryUGCDetailsRequest(_ptr, pvecPublishedFileID, unNumPublishedFileIDs);
			}
			public virtual SteamAPICall_t /*(SteamAPICall_t)*/ ISteamUGC_SendQueryUGCRequest( ulong handle )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamUGC _ptr is null!" );
				
				return Native.SteamAPI_ISteamUGC_SendQueryUGCRequest(_ptr, handle);
			}
			public virtual bool /*bool*/ ISteamUGC_GetQueryUGCResult( ulong handle, uint /*uint32*/ index, ref SteamUGCDetails_t /*struct SteamUGCDetails_t **/ pDetails )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamUGC _ptr is null!" );
				
				var pDetails_ps = new SteamUGCDetails_t.PackSmall();
				var ret = Native.SteamAPI_ISteamUGC_GetQueryUGCResult(_ptr, handle, index, ref pDetails_ps);
				pDetails = pDetails_ps;
				return ret;
			}
			public virtual bool /*bool*/ ISteamUGC_GetQueryUGCPreviewURL( ulong handle, uint /*uint32*/ index, System.Text.StringBuilder /*char **/ pchURL, uint /*uint32*/ cchURLSize )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamUGC _ptr is null!" );
				
				return Native.SteamAPI_ISteamUGC_GetQueryUGCPreviewURL(_ptr, handle, index, pchURL, cchURLSize);
			}
			public virtual bool /*bool*/ ISteamUGC_GetQueryUGCMetadata( ulong handle, uint /*uint32*/ index, System.Text.StringBuilder /*char **/ pchMetadata, uint /*uint32*/ cchMetadatasize )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamUGC _ptr is null!" );
				
				return Native.SteamAPI_ISteamUGC_GetQueryUGCMetadata(_ptr, handle, index, pchMetadata, cchMetadatasize);
			}
			public virtual bool /*bool*/ ISteamUGC_GetQueryUGCChildren( ulong handle, uint /*uint32*/ index, IntPtr /*PublishedFileId_t **/ pvecPublishedFileID, uint /*uint32*/ cMaxEntries )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamUGC _ptr is null!" );
				
				return Native.SteamAPI_ISteamUGC_GetQueryUGCChildren(_ptr, handle, index, pvecPublishedFileID, cMaxEntries);
			}
			public virtual bool /*bool*/ ISteamUGC_GetQueryUGCStatistic( ulong handle, uint /*uint32*/ index, ItemStatistic /*EItemStatistic*/ eStatType, out ulong /*uint64 **/ pStatValue )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamUGC _ptr is null!" );
				
				return Native.SteamAPI_ISteamUGC_GetQueryUGCStatistic(_ptr, handle, index, eStatType, out pStatValue);
			}
			public virtual uint /*uint32*/ ISteamUGC_GetQueryUGCNumAdditionalPreviews( ulong handle, uint /*uint32*/ index )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamUGC _ptr is null!" );
				
				return Native.SteamAPI_ISteamUGC_GetQueryUGCNumAdditionalPreviews(_ptr, handle, index);
			}
			public virtual bool /*bool*/ ISteamUGC_GetQueryUGCAdditionalPreview( ulong handle, uint /*uint32*/ index, uint /*uint32*/ previewIndex, System.Text.StringBuilder /*char **/ pchURLOrVideoID, uint /*uint32*/ cchURLSize, System.Text.StringBuilder /*char **/ pchOriginalFileName, uint /*uint32*/ cchOriginalFileNameSize, out ItemPreviewType /*EItemPreviewType **/ pPreviewType )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamUGC _ptr is null!" );
				
				return Native.SteamAPI_ISteamUGC_GetQueryUGCAdditionalPreview(_ptr, handle, index, previewIndex, pchURLOrVideoID, cchURLSize, pchOriginalFileName, cchOriginalFileNameSize, out pPreviewType);
			}
			public virtual uint /*uint32*/ ISteamUGC_GetQueryUGCNumKeyValueTags( ulong handle, uint /*uint32*/ index )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamUGC _ptr is null!" );
				
				return Native.SteamAPI_ISteamUGC_GetQueryUGCNumKeyValueTags(_ptr, handle, index);
			}
			public virtual bool /*bool*/ ISteamUGC_GetQueryUGCKeyValueTag( ulong handle, uint /*uint32*/ index, uint /*uint32*/ keyValueTagIndex, System.Text.StringBuilder /*char **/ pchKey, uint /*uint32*/ cchKeySize, System.Text.StringBuilder /*char **/ pchValue, uint /*uint32*/ cchValueSize )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamUGC _ptr is null!" );
				
				return Native.SteamAPI_ISteamUGC_GetQueryUGCKeyValueTag(_ptr, handle, index, keyValueTagIndex, pchKey, cchKeySize, pchValue, cchValueSize);
			}
			public virtual bool /*bool*/ ISteamUGC_ReleaseQueryUGCRequest( ulong handle )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamUGC _ptr is null!" );
				
				return Native.SteamAPI_ISteamUGC_ReleaseQueryUGCRequest(_ptr, handle);
			}
			public virtual bool /*bool*/ ISteamUGC_AddRequiredTag( ulong handle, string /*const char **/ pTagName )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamUGC _ptr is null!" );
				
				return Native.SteamAPI_ISteamUGC_AddRequiredTag(_ptr, handle, pTagName);
			}
			public virtual bool /*bool*/ ISteamUGC_AddExcludedTag( ulong handle, string /*const char **/ pTagName )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamUGC _ptr is null!" );
				
				return Native.SteamAPI_ISteamUGC_AddExcludedTag(_ptr, handle, pTagName);
			}
			public virtual bool /*bool*/ ISteamUGC_SetReturnOnlyIDs( ulong handle, [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bReturnOnlyIDs )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamUGC _ptr is null!" );
				
				return Native.SteamAPI_ISteamUGC_SetReturnOnlyIDs(_ptr, handle, bReturnOnlyIDs);
			}
			public virtual bool /*bool*/ ISteamUGC_SetReturnKeyValueTags( ulong handle, [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bReturnKeyValueTags )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamUGC _ptr is null!" );
				
				return Native.SteamAPI_ISteamUGC_SetReturnKeyValueTags(_ptr, handle, bReturnKeyValueTags);
			}
			public virtual bool /*bool*/ ISteamUGC_SetReturnLongDescription( ulong handle, [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bReturnLongDescription )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamUGC _ptr is null!" );
				
				return Native.SteamAPI_ISteamUGC_SetReturnLongDescription(_ptr, handle, bReturnLongDescription);
			}
			public virtual bool /*bool*/ ISteamUGC_SetReturnMetadata( ulong handle, [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bReturnMetadata )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamUGC _ptr is null!" );
				
				return Native.SteamAPI_ISteamUGC_SetReturnMetadata(_ptr, handle, bReturnMetadata);
			}
			public virtual bool /*bool*/ ISteamUGC_SetReturnChildren( ulong handle, [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bReturnChildren )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamUGC _ptr is null!" );
				
				return Native.SteamAPI_ISteamUGC_SetReturnChildren(_ptr, handle, bReturnChildren);
			}
			public virtual bool /*bool*/ ISteamUGC_SetReturnAdditionalPreviews( ulong handle, [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bReturnAdditionalPreviews )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamUGC _ptr is null!" );
				
				return Native.SteamAPI_ISteamUGC_SetReturnAdditionalPreviews(_ptr, handle, bReturnAdditionalPreviews);
			}
			public virtual bool /*bool*/ ISteamUGC_SetReturnTotalOnly( ulong handle, [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bReturnTotalOnly )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamUGC _ptr is null!" );
				
				return Native.SteamAPI_ISteamUGC_SetReturnTotalOnly(_ptr, handle, bReturnTotalOnly);
			}
			public virtual bool /*bool*/ ISteamUGC_SetReturnPlaytimeStats( ulong handle, uint /*uint32*/ unDays )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamUGC _ptr is null!" );
				
				return Native.SteamAPI_ISteamUGC_SetReturnPlaytimeStats(_ptr, handle, unDays);
			}
			public virtual bool /*bool*/ ISteamUGC_SetLanguage( ulong handle, string /*const char **/ pchLanguage )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamUGC _ptr is null!" );
				
				return Native.SteamAPI_ISteamUGC_SetLanguage(_ptr, handle, pchLanguage);
			}
			public virtual bool /*bool*/ ISteamUGC_SetAllowCachedResponse( ulong handle, uint /*uint32*/ unMaxAgeSeconds )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamUGC _ptr is null!" );
				
				return Native.SteamAPI_ISteamUGC_SetAllowCachedResponse(_ptr, handle, unMaxAgeSeconds);
			}
			public virtual bool /*bool*/ ISteamUGC_SetCloudFileNameFilter( ulong handle, string /*const char **/ pMatchCloudFileName )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamUGC _ptr is null!" );
				
				return Native.SteamAPI_ISteamUGC_SetCloudFileNameFilter(_ptr, handle, pMatchCloudFileName);
			}
			public virtual bool /*bool*/ ISteamUGC_SetMatchAnyTag( ulong handle, [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bMatchAnyTag )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamUGC _ptr is null!" );
				
				return Native.SteamAPI_ISteamUGC_SetMatchAnyTag(_ptr, handle, bMatchAnyTag);
			}
			public virtual bool /*bool*/ ISteamUGC_SetSearchText( ulong handle, string /*const char **/ pSearchText )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamUGC _ptr is null!" );
				
				return Native.SteamAPI_ISteamUGC_SetSearchText(_ptr, handle, pSearchText);
			}
			public virtual bool /*bool*/ ISteamUGC_SetRankedByTrendDays( ulong handle, uint /*uint32*/ unDays )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamUGC _ptr is null!" );
				
				return Native.SteamAPI_ISteamUGC_SetRankedByTrendDays(_ptr, handle, unDays);
			}
			public virtual bool /*bool*/ ISteamUGC_AddRequiredKeyValueTag( ulong handle, string /*const char **/ pKey, string /*const char **/ pValue )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamUGC _ptr is null!" );
				
				return Native.SteamAPI_ISteamUGC_AddRequiredKeyValueTag(_ptr, handle, pKey, pValue);
			}
			public virtual SteamAPICall_t /*(SteamAPICall_t)*/ ISteamUGC_RequestUGCDetails( ulong nPublishedFileID, uint /*uint32*/ unMaxAgeSeconds )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamUGC _ptr is null!" );
				
				return Native.SteamAPI_ISteamUGC_RequestUGCDetails(_ptr, nPublishedFileID, unMaxAgeSeconds);
			}
			public virtual SteamAPICall_t /*(SteamAPICall_t)*/ ISteamUGC_CreateItem( uint nConsumerAppId, WorkshopFileType /*EWorkshopFileType*/ eFileType )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamUGC _ptr is null!" );
				
				return Native.SteamAPI_ISteamUGC_CreateItem(_ptr, nConsumerAppId, eFileType);
			}
			public virtual UGCUpdateHandle_t /*(UGCUpdateHandle_t)*/ ISteamUGC_StartItemUpdate( uint nConsumerAppId, ulong nPublishedFileID )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamUGC _ptr is null!" );
				
				return Native.SteamAPI_ISteamUGC_StartItemUpdate(_ptr, nConsumerAppId, nPublishedFileID);
			}
			public virtual bool /*bool*/ ISteamUGC_SetItemTitle( ulong handle, string /*const char **/ pchTitle )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamUGC _ptr is null!" );
				
				return Native.SteamAPI_ISteamUGC_SetItemTitle(_ptr, handle, pchTitle);
			}
			public virtual bool /*bool*/ ISteamUGC_SetItemDescription( ulong handle, string /*const char **/ pchDescription )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamUGC _ptr is null!" );
				
				return Native.SteamAPI_ISteamUGC_SetItemDescription(_ptr, handle, pchDescription);
			}
			public virtual bool /*bool*/ ISteamUGC_SetItemUpdateLanguage( ulong handle, string /*const char **/ pchLanguage )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamUGC _ptr is null!" );
				
				return Native.SteamAPI_ISteamUGC_SetItemUpdateLanguage(_ptr, handle, pchLanguage);
			}
			public virtual bool /*bool*/ ISteamUGC_SetItemMetadata( ulong handle, string /*const char **/ pchMetaData )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamUGC _ptr is null!" );
				
				return Native.SteamAPI_ISteamUGC_SetItemMetadata(_ptr, handle, pchMetaData);
			}
			public virtual bool /*bool*/ ISteamUGC_SetItemVisibility( ulong handle, RemoteStoragePublishedFileVisibility /*ERemoteStoragePublishedFileVisibility*/ eVisibility )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamUGC _ptr is null!" );
				
				return Native.SteamAPI_ISteamUGC_SetItemVisibility(_ptr, handle, eVisibility);
			}
			public virtual bool /*bool*/ ISteamUGC_SetItemTags( ulong updateHandle, ref SteamParamStringArray_t /*const struct SteamParamStringArray_t **/ pTags )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamUGC _ptr is null!" );
				
				var pTags_ps = new SteamParamStringArray_t.PackSmall();
				var ret = Native.SteamAPI_ISteamUGC_SetItemTags(_ptr, updateHandle, ref pTags_ps);
				pTags = pTags_ps;
				return ret;
			}
			public virtual bool /*bool*/ ISteamUGC_SetItemContent( ulong handle, string /*const char **/ pszContentFolder )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamUGC _ptr is null!" );
				
				return Native.SteamAPI_ISteamUGC_SetItemContent(_ptr, handle, pszContentFolder);
			}
			public virtual bool /*bool*/ ISteamUGC_SetItemPreview( ulong handle, string /*const char **/ pszPreviewFile )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamUGC _ptr is null!" );
				
				return Native.SteamAPI_ISteamUGC_SetItemPreview(_ptr, handle, pszPreviewFile);
			}
			public virtual bool /*bool*/ ISteamUGC_SetAllowLegacyUpload( ulong handle, [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bAllowLegacyUpload )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamUGC _ptr is null!" );
				
				return Native.SteamAPI_ISteamUGC_SetAllowLegacyUpload(_ptr, handle, bAllowLegacyUpload);
			}
			public virtual bool /*bool*/ ISteamUGC_RemoveItemKeyValueTags( ulong handle, string /*const char **/ pchKey )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamUGC _ptr is null!" );
				
				return Native.SteamAPI_ISteamUGC_RemoveItemKeyValueTags(_ptr, handle, pchKey);
			}
			public virtual bool /*bool*/ ISteamUGC_AddItemKeyValueTag( ulong handle, string /*const char **/ pchKey, string /*const char **/ pchValue )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamUGC _ptr is null!" );
				
				return Native.SteamAPI_ISteamUGC_AddItemKeyValueTag(_ptr, handle, pchKey, pchValue);
			}
			public virtual bool /*bool*/ ISteamUGC_AddItemPreviewFile( ulong handle, string /*const char **/ pszPreviewFile, ItemPreviewType /*EItemPreviewType*/ type )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamUGC _ptr is null!" );
				
				return Native.SteamAPI_ISteamUGC_AddItemPreviewFile(_ptr, handle, pszPreviewFile, type);
			}
			public virtual bool /*bool*/ ISteamUGC_AddItemPreviewVideo( ulong handle, string /*const char **/ pszVideoID )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamUGC _ptr is null!" );
				
				return Native.SteamAPI_ISteamUGC_AddItemPreviewVideo(_ptr, handle, pszVideoID);
			}
			public virtual bool /*bool*/ ISteamUGC_UpdateItemPreviewFile( ulong handle, uint /*uint32*/ index, string /*const char **/ pszPreviewFile )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamUGC _ptr is null!" );
				
				return Native.SteamAPI_ISteamUGC_UpdateItemPreviewFile(_ptr, handle, index, pszPreviewFile);
			}
			public virtual bool /*bool*/ ISteamUGC_UpdateItemPreviewVideo( ulong handle, uint /*uint32*/ index, string /*const char **/ pszVideoID )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamUGC _ptr is null!" );
				
				return Native.SteamAPI_ISteamUGC_UpdateItemPreviewVideo(_ptr, handle, index, pszVideoID);
			}
			public virtual bool /*bool*/ ISteamUGC_RemoveItemPreview( ulong handle, uint /*uint32*/ index )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamUGC _ptr is null!" );
				
				return Native.SteamAPI_ISteamUGC_RemoveItemPreview(_ptr, handle, index);
			}
			public virtual SteamAPICall_t /*(SteamAPICall_t)*/ ISteamUGC_SubmitItemUpdate( ulong handle, string /*const char **/ pchChangeNote )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamUGC _ptr is null!" );
				
				return Native.SteamAPI_ISteamUGC_SubmitItemUpdate(_ptr, handle, pchChangeNote);
			}
			public virtual ItemUpdateStatus /*EItemUpdateStatus*/ ISteamUGC_GetItemUpdateProgress( ulong handle, out ulong /*uint64 **/ punBytesProcessed, out ulong /*uint64 **/ punBytesTotal )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamUGC _ptr is null!" );
				
				return Native.SteamAPI_ISteamUGC_GetItemUpdateProgress(_ptr, handle, out punBytesProcessed, out punBytesTotal);
			}
			public virtual SteamAPICall_t /*(SteamAPICall_t)*/ ISteamUGC_SetUserItemVote( ulong nPublishedFileID, [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bVoteUp )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamUGC _ptr is null!" );
				
				return Native.SteamAPI_ISteamUGC_SetUserItemVote(_ptr, nPublishedFileID, bVoteUp);
			}
			public virtual SteamAPICall_t /*(SteamAPICall_t)*/ ISteamUGC_GetUserItemVote( ulong nPublishedFileID )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamUGC _ptr is null!" );
				
				return Native.SteamAPI_ISteamUGC_GetUserItemVote(_ptr, nPublishedFileID);
			}
			public virtual SteamAPICall_t /*(SteamAPICall_t)*/ ISteamUGC_AddItemToFavorites( uint nAppId, ulong nPublishedFileID )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamUGC _ptr is null!" );
				
				return Native.SteamAPI_ISteamUGC_AddItemToFavorites(_ptr, nAppId, nPublishedFileID);
			}
			public virtual SteamAPICall_t /*(SteamAPICall_t)*/ ISteamUGC_RemoveItemFromFavorites( uint nAppId, ulong nPublishedFileID )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamUGC _ptr is null!" );
				
				return Native.SteamAPI_ISteamUGC_RemoveItemFromFavorites(_ptr, nAppId, nPublishedFileID);
			}
			public virtual SteamAPICall_t /*(SteamAPICall_t)*/ ISteamUGC_SubscribeItem( ulong nPublishedFileID )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamUGC _ptr is null!" );
				
				return Native.SteamAPI_ISteamUGC_SubscribeItem(_ptr, nPublishedFileID);
			}
			public virtual SteamAPICall_t /*(SteamAPICall_t)*/ ISteamUGC_UnsubscribeItem( ulong nPublishedFileID )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamUGC _ptr is null!" );
				
				return Native.SteamAPI_ISteamUGC_UnsubscribeItem(_ptr, nPublishedFileID);
			}
			public virtual uint /*uint32*/ ISteamUGC_GetNumSubscribedItems()
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamUGC _ptr is null!" );
				
				return Native.SteamAPI_ISteamUGC_GetNumSubscribedItems(_ptr);
			}
			public virtual uint /*uint32*/ ISteamUGC_GetSubscribedItems( IntPtr /*PublishedFileId_t **/ pvecPublishedFileID, uint /*uint32*/ cMaxEntries )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamUGC _ptr is null!" );
				
				return Native.SteamAPI_ISteamUGC_GetSubscribedItems(_ptr, pvecPublishedFileID, cMaxEntries);
			}
			public virtual uint /*uint32*/ ISteamUGC_GetItemState( ulong nPublishedFileID )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamUGC _ptr is null!" );
				
				return Native.SteamAPI_ISteamUGC_GetItemState(_ptr, nPublishedFileID);
			}
			public virtual bool /*bool*/ ISteamUGC_GetItemInstallInfo( ulong nPublishedFileID, out ulong /*uint64 **/ punSizeOnDisk, System.Text.StringBuilder /*char **/ pchFolder, uint /*uint32*/ cchFolderSize, out uint /*uint32 **/ punTimeStamp )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamUGC _ptr is null!" );
				
				return Native.SteamAPI_ISteamUGC_GetItemInstallInfo(_ptr, nPublishedFileID, out punSizeOnDisk, pchFolder, cchFolderSize, out punTimeStamp);
			}
			public virtual bool /*bool*/ ISteamUGC_GetItemDownloadInfo( ulong nPublishedFileID, out ulong /*uint64 **/ punBytesDownloaded, out ulong /*uint64 **/ punBytesTotal )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamUGC _ptr is null!" );
				
				return Native.SteamAPI_ISteamUGC_GetItemDownloadInfo(_ptr, nPublishedFileID, out punBytesDownloaded, out punBytesTotal);
			}
			public virtual bool /*bool*/ ISteamUGC_DownloadItem( ulong nPublishedFileID, [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bHighPriority )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamUGC _ptr is null!" );
				
				return Native.SteamAPI_ISteamUGC_DownloadItem(_ptr, nPublishedFileID, bHighPriority);
			}
			public virtual bool /*bool*/ ISteamUGC_BInitWorkshopForGameServer( uint unWorkshopDepotID, string /*const char **/ pszFolder )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamUGC _ptr is null!" );
				
				return Native.SteamAPI_ISteamUGC_BInitWorkshopForGameServer(_ptr, unWorkshopDepotID, pszFolder);
			}
			public virtual void /*void*/ ISteamUGC_SuspendDownloads( [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bSuspend )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamUGC _ptr is null!" );
				
				Native.SteamAPI_ISteamUGC_SuspendDownloads(_ptr, bSuspend);
			}
			public virtual SteamAPICall_t /*(SteamAPICall_t)*/ ISteamUGC_StartPlaytimeTracking( IntPtr /*PublishedFileId_t **/ pvecPublishedFileID, uint /*uint32*/ unNumPublishedFileIDs )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamUGC _ptr is null!" );
				
				return Native.SteamAPI_ISteamUGC_StartPlaytimeTracking(_ptr, pvecPublishedFileID, unNumPublishedFileIDs);
			}
			public virtual SteamAPICall_t /*(SteamAPICall_t)*/ ISteamUGC_StopPlaytimeTracking( IntPtr /*PublishedFileId_t **/ pvecPublishedFileID, uint /*uint32*/ unNumPublishedFileIDs )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamUGC _ptr is null!" );
				
				return Native.SteamAPI_ISteamUGC_StopPlaytimeTracking(_ptr, pvecPublishedFileID, unNumPublishedFileIDs);
			}
			public virtual SteamAPICall_t /*(SteamAPICall_t)*/ ISteamUGC_StopPlaytimeTrackingForAllItems()
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamUGC _ptr is null!" );
				
				return Native.SteamAPI_ISteamUGC_StopPlaytimeTrackingForAllItems(_ptr);
			}
			public virtual SteamAPICall_t /*(SteamAPICall_t)*/ ISteamUGC_AddDependency( ulong nParentPublishedFileID, ulong nChildPublishedFileID )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamUGC _ptr is null!" );
				
				return Native.SteamAPI_ISteamUGC_AddDependency(_ptr, nParentPublishedFileID, nChildPublishedFileID);
			}
			public virtual SteamAPICall_t /*(SteamAPICall_t)*/ ISteamUGC_RemoveDependency( ulong nParentPublishedFileID, ulong nChildPublishedFileID )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamUGC _ptr is null!" );
				
				return Native.SteamAPI_ISteamUGC_RemoveDependency(_ptr, nParentPublishedFileID, nChildPublishedFileID);
			}
			public virtual SteamAPICall_t /*(SteamAPICall_t)*/ ISteamUGC_AddAppDependency( ulong nPublishedFileID, uint nAppID )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamUGC _ptr is null!" );
				
				return Native.SteamAPI_ISteamUGC_AddAppDependency(_ptr, nPublishedFileID, nAppID);
			}
			public virtual SteamAPICall_t /*(SteamAPICall_t)*/ ISteamUGC_RemoveAppDependency( ulong nPublishedFileID, uint nAppID )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamUGC _ptr is null!" );
				
				return Native.SteamAPI_ISteamUGC_RemoveAppDependency(_ptr, nPublishedFileID, nAppID);
			}
			public virtual SteamAPICall_t /*(SteamAPICall_t)*/ ISteamUGC_GetAppDependencies( ulong nPublishedFileID )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamUGC _ptr is null!" );
				
				return Native.SteamAPI_ISteamUGC_GetAppDependencies(_ptr, nPublishedFileID);
			}
			public virtual SteamAPICall_t /*(SteamAPICall_t)*/ ISteamUGC_DeleteItem( ulong nPublishedFileID )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamUGC _ptr is null!" );
				
				return Native.SteamAPI_ISteamUGC_DeleteItem(_ptr, nPublishedFileID);
			}
			
			public virtual uint /*uint32*/ ISteamAppList_GetNumInstalledApps()
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamAppList _ptr is null!" );
				
				return Native.SteamAPI_ISteamAppList_GetNumInstalledApps(_ptr);
			}
			public virtual uint /*uint32*/ ISteamAppList_GetInstalledApps( IntPtr /*AppId_t **/ pvecAppID, uint /*uint32*/ unMaxAppIDs )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamAppList _ptr is null!" );
				
				return Native.SteamAPI_ISteamAppList_GetInstalledApps(_ptr, pvecAppID, unMaxAppIDs);
			}
			public virtual int /*int*/ ISteamAppList_GetAppName( uint nAppID, System.Text.StringBuilder /*char **/ pchName, int /*int*/ cchNameMax )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamAppList _ptr is null!" );
				
				return Native.SteamAPI_ISteamAppList_GetAppName(_ptr, nAppID, pchName, cchNameMax);
			}
			public virtual int /*int*/ ISteamAppList_GetAppInstallDir( uint nAppID, System.Text.StringBuilder /*char **/ pchDirectory, int /*int*/ cchNameMax )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamAppList _ptr is null!" );
				
				return Native.SteamAPI_ISteamAppList_GetAppInstallDir(_ptr, nAppID, pchDirectory, cchNameMax);
			}
			public virtual int /*int*/ ISteamAppList_GetAppBuildId( uint nAppID )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamAppList _ptr is null!" );
				
				return Native.SteamAPI_ISteamAppList_GetAppBuildId(_ptr, nAppID);
			}
			
			public virtual void /*void*/ ISteamHTMLSurface_DestructISteamHTMLSurface()
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamHTMLSurface _ptr is null!" );
				
				Native.SteamAPI_ISteamHTMLSurface_DestructISteamHTMLSurface(_ptr);
			}
			public virtual bool /*bool*/ ISteamHTMLSurface_Init()
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamHTMLSurface _ptr is null!" );
				
				return Native.SteamAPI_ISteamHTMLSurface_Init(_ptr);
			}
			public virtual bool /*bool*/ ISteamHTMLSurface_Shutdown()
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamHTMLSurface _ptr is null!" );
				
				return Native.SteamAPI_ISteamHTMLSurface_Shutdown(_ptr);
			}
			public virtual SteamAPICall_t /*(SteamAPICall_t)*/ ISteamHTMLSurface_CreateBrowser( string /*const char **/ pchUserAgent, string /*const char **/ pchUserCSS )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamHTMLSurface _ptr is null!" );
				
				return Native.SteamAPI_ISteamHTMLSurface_CreateBrowser(_ptr, pchUserAgent, pchUserCSS);
			}
			public virtual void /*void*/ ISteamHTMLSurface_RemoveBrowser( uint unBrowserHandle )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamHTMLSurface _ptr is null!" );
				
				Native.SteamAPI_ISteamHTMLSurface_RemoveBrowser(_ptr, unBrowserHandle);
			}
			public virtual void /*void*/ ISteamHTMLSurface_LoadURL( uint unBrowserHandle, string /*const char **/ pchURL, string /*const char **/ pchPostData )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamHTMLSurface _ptr is null!" );
				
				Native.SteamAPI_ISteamHTMLSurface_LoadURL(_ptr, unBrowserHandle, pchURL, pchPostData);
			}
			public virtual void /*void*/ ISteamHTMLSurface_SetSize( uint unBrowserHandle, uint /*uint32*/ unWidth, uint /*uint32*/ unHeight )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamHTMLSurface _ptr is null!" );
				
				Native.SteamAPI_ISteamHTMLSurface_SetSize(_ptr, unBrowserHandle, unWidth, unHeight);
			}
			public virtual void /*void*/ ISteamHTMLSurface_StopLoad( uint unBrowserHandle )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamHTMLSurface _ptr is null!" );
				
				Native.SteamAPI_ISteamHTMLSurface_StopLoad(_ptr, unBrowserHandle);
			}
			public virtual void /*void*/ ISteamHTMLSurface_Reload( uint unBrowserHandle )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamHTMLSurface _ptr is null!" );
				
				Native.SteamAPI_ISteamHTMLSurface_Reload(_ptr, unBrowserHandle);
			}
			public virtual void /*void*/ ISteamHTMLSurface_GoBack( uint unBrowserHandle )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamHTMLSurface _ptr is null!" );
				
				Native.SteamAPI_ISteamHTMLSurface_GoBack(_ptr, unBrowserHandle);
			}
			public virtual void /*void*/ ISteamHTMLSurface_GoForward( uint unBrowserHandle )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamHTMLSurface _ptr is null!" );
				
				Native.SteamAPI_ISteamHTMLSurface_GoForward(_ptr, unBrowserHandle);
			}
			public virtual void /*void*/ ISteamHTMLSurface_AddHeader( uint unBrowserHandle, string /*const char **/ pchKey, string /*const char **/ pchValue )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamHTMLSurface _ptr is null!" );
				
				Native.SteamAPI_ISteamHTMLSurface_AddHeader(_ptr, unBrowserHandle, pchKey, pchValue);
			}
			public virtual void /*void*/ ISteamHTMLSurface_ExecuteJavascript( uint unBrowserHandle, string /*const char **/ pchScript )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamHTMLSurface _ptr is null!" );
				
				Native.SteamAPI_ISteamHTMLSurface_ExecuteJavascript(_ptr, unBrowserHandle, pchScript);
			}
			public virtual void /*void*/ ISteamHTMLSurface_MouseUp( uint unBrowserHandle, HTMLMouseButton /*ISteamHTMLSurface::EHTMLMouseButton*/ eMouseButton )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamHTMLSurface _ptr is null!" );
				
				Native.SteamAPI_ISteamHTMLSurface_MouseUp(_ptr, unBrowserHandle, eMouseButton);
			}
			public virtual void /*void*/ ISteamHTMLSurface_MouseDown( uint unBrowserHandle, HTMLMouseButton /*ISteamHTMLSurface::EHTMLMouseButton*/ eMouseButton )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamHTMLSurface _ptr is null!" );
				
				Native.SteamAPI_ISteamHTMLSurface_MouseDown(_ptr, unBrowserHandle, eMouseButton);
			}
			public virtual void /*void*/ ISteamHTMLSurface_MouseDoubleClick( uint unBrowserHandle, HTMLMouseButton /*ISteamHTMLSurface::EHTMLMouseButton*/ eMouseButton )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamHTMLSurface _ptr is null!" );
				
				Native.SteamAPI_ISteamHTMLSurface_MouseDoubleClick(_ptr, unBrowserHandle, eMouseButton);
			}
			public virtual void /*void*/ ISteamHTMLSurface_MouseMove( uint unBrowserHandle, int /*int*/ x, int /*int*/ y )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamHTMLSurface _ptr is null!" );
				
				Native.SteamAPI_ISteamHTMLSurface_MouseMove(_ptr, unBrowserHandle, x, y);
			}
			public virtual void /*void*/ ISteamHTMLSurface_MouseWheel( uint unBrowserHandle, int /*int32*/ nDelta )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamHTMLSurface _ptr is null!" );
				
				Native.SteamAPI_ISteamHTMLSurface_MouseWheel(_ptr, unBrowserHandle, nDelta);
			}
			public virtual void /*void*/ ISteamHTMLSurface_KeyDown( uint unBrowserHandle, uint /*uint32*/ nNativeKeyCode, HTMLKeyModifiers /*ISteamHTMLSurface::EHTMLKeyModifiers*/ eHTMLKeyModifiers, [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bIsSystemKey )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamHTMLSurface _ptr is null!" );
				
				Native.SteamAPI_ISteamHTMLSurface_KeyDown(_ptr, unBrowserHandle, nNativeKeyCode, eHTMLKeyModifiers, bIsSystemKey);
			}
			public virtual void /*void*/ ISteamHTMLSurface_KeyUp( uint unBrowserHandle, uint /*uint32*/ nNativeKeyCode, HTMLKeyModifiers /*ISteamHTMLSurface::EHTMLKeyModifiers*/ eHTMLKeyModifiers )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamHTMLSurface _ptr is null!" );
				
				Native.SteamAPI_ISteamHTMLSurface_KeyUp(_ptr, unBrowserHandle, nNativeKeyCode, eHTMLKeyModifiers);
			}
			public virtual void /*void*/ ISteamHTMLSurface_KeyChar( uint unBrowserHandle, uint /*uint32*/ cUnicodeChar, HTMLKeyModifiers /*ISteamHTMLSurface::EHTMLKeyModifiers*/ eHTMLKeyModifiers )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamHTMLSurface _ptr is null!" );
				
				Native.SteamAPI_ISteamHTMLSurface_KeyChar(_ptr, unBrowserHandle, cUnicodeChar, eHTMLKeyModifiers);
			}
			public virtual void /*void*/ ISteamHTMLSurface_SetHorizontalScroll( uint unBrowserHandle, uint /*uint32*/ nAbsolutePixelScroll )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamHTMLSurface _ptr is null!" );
				
				Native.SteamAPI_ISteamHTMLSurface_SetHorizontalScroll(_ptr, unBrowserHandle, nAbsolutePixelScroll);
			}
			public virtual void /*void*/ ISteamHTMLSurface_SetVerticalScroll( uint unBrowserHandle, uint /*uint32*/ nAbsolutePixelScroll )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamHTMLSurface _ptr is null!" );
				
				Native.SteamAPI_ISteamHTMLSurface_SetVerticalScroll(_ptr, unBrowserHandle, nAbsolutePixelScroll);
			}
			public virtual void /*void*/ ISteamHTMLSurface_SetKeyFocus( uint unBrowserHandle, [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bHasKeyFocus )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamHTMLSurface _ptr is null!" );
				
				Native.SteamAPI_ISteamHTMLSurface_SetKeyFocus(_ptr, unBrowserHandle, bHasKeyFocus);
			}
			public virtual void /*void*/ ISteamHTMLSurface_ViewSource( uint unBrowserHandle )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamHTMLSurface _ptr is null!" );
				
				Native.SteamAPI_ISteamHTMLSurface_ViewSource(_ptr, unBrowserHandle);
			}
			public virtual void /*void*/ ISteamHTMLSurface_CopyToClipboard( uint unBrowserHandle )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamHTMLSurface _ptr is null!" );
				
				Native.SteamAPI_ISteamHTMLSurface_CopyToClipboard(_ptr, unBrowserHandle);
			}
			public virtual void /*void*/ ISteamHTMLSurface_PasteFromClipboard( uint unBrowserHandle )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamHTMLSurface _ptr is null!" );
				
				Native.SteamAPI_ISteamHTMLSurface_PasteFromClipboard(_ptr, unBrowserHandle);
			}
			public virtual void /*void*/ ISteamHTMLSurface_Find( uint unBrowserHandle, string /*const char **/ pchSearchStr, [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bCurrentlyInFind, [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bReverse )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamHTMLSurface _ptr is null!" );
				
				Native.SteamAPI_ISteamHTMLSurface_Find(_ptr, unBrowserHandle, pchSearchStr, bCurrentlyInFind, bReverse);
			}
			public virtual void /*void*/ ISteamHTMLSurface_StopFind( uint unBrowserHandle )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamHTMLSurface _ptr is null!" );
				
				Native.SteamAPI_ISteamHTMLSurface_StopFind(_ptr, unBrowserHandle);
			}
			public virtual void /*void*/ ISteamHTMLSurface_GetLinkAtPosition( uint unBrowserHandle, int /*int*/ x, int /*int*/ y )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamHTMLSurface _ptr is null!" );
				
				Native.SteamAPI_ISteamHTMLSurface_GetLinkAtPosition(_ptr, unBrowserHandle, x, y);
			}
			public virtual void /*void*/ ISteamHTMLSurface_SetCookie( string /*const char **/ pchHostname, string /*const char **/ pchKey, string /*const char **/ pchValue, string /*const char **/ pchPath, uint nExpires, [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bSecure, [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bHTTPOnly )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamHTMLSurface _ptr is null!" );
				
				Native.SteamAPI_ISteamHTMLSurface_SetCookie(_ptr, pchHostname, pchKey, pchValue, pchPath, nExpires, bSecure, bHTTPOnly);
			}
			public virtual void /*void*/ ISteamHTMLSurface_SetPageScaleFactor( uint unBrowserHandle, float /*float*/ flZoom, int /*int*/ nPointX, int /*int*/ nPointY )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamHTMLSurface _ptr is null!" );
				
				Native.SteamAPI_ISteamHTMLSurface_SetPageScaleFactor(_ptr, unBrowserHandle, flZoom, nPointX, nPointY);
			}
			public virtual void /*void*/ ISteamHTMLSurface_SetBackgroundMode( uint unBrowserHandle, [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bBackgroundMode )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamHTMLSurface _ptr is null!" );
				
				Native.SteamAPI_ISteamHTMLSurface_SetBackgroundMode(_ptr, unBrowserHandle, bBackgroundMode);
			}
			public virtual void /*void*/ ISteamHTMLSurface_SetDPIScalingFactor( uint unBrowserHandle, float /*float*/ flDPIScaling )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamHTMLSurface _ptr is null!" );
				
				Native.SteamAPI_ISteamHTMLSurface_SetDPIScalingFactor(_ptr, unBrowserHandle, flDPIScaling);
			}
			public virtual void /*void*/ ISteamHTMLSurface_OpenDeveloperTools( uint unBrowserHandle )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamHTMLSurface _ptr is null!" );
				
				Native.SteamAPI_ISteamHTMLSurface_OpenDeveloperTools(_ptr, unBrowserHandle);
			}
			public virtual void /*void*/ ISteamHTMLSurface_AllowStartRequest( uint unBrowserHandle, [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bAllowed )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamHTMLSurface _ptr is null!" );
				
				Native.SteamAPI_ISteamHTMLSurface_AllowStartRequest(_ptr, unBrowserHandle, bAllowed);
			}
			public virtual void /*void*/ ISteamHTMLSurface_JSDialogResponse( uint unBrowserHandle, [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bResult )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamHTMLSurface _ptr is null!" );
				
				Native.SteamAPI_ISteamHTMLSurface_JSDialogResponse(_ptr, unBrowserHandle, bResult);
			}
			
			public virtual Result /*EResult*/ ISteamInventory_GetResultStatus( int resultHandle )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamInventory _ptr is null!" );
				
				return Native.SteamAPI_ISteamInventory_GetResultStatus(_ptr, resultHandle);
			}
			public virtual bool /*bool*/ ISteamInventory_GetResultItems( int resultHandle, IntPtr /*struct SteamItemDetails_t **/ pOutItemsArray, out uint /*uint32 **/ punOutItemsArraySize )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamInventory _ptr is null!" );
				
				return Native.SteamAPI_ISteamInventory_GetResultItems(_ptr, resultHandle, pOutItemsArray, out punOutItemsArraySize);
			}
			public virtual bool /*bool*/ ISteamInventory_GetResultItemProperty( int resultHandle, uint /*uint32*/ unItemIndex, string /*const char **/ pchPropertyName, System.Text.StringBuilder /*char **/ pchValueBuffer, out uint /*uint32 **/ punValueBufferSizeOut )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamInventory _ptr is null!" );
				
				return Native.SteamAPI_ISteamInventory_GetResultItemProperty(_ptr, resultHandle, unItemIndex, pchPropertyName, pchValueBuffer, out punValueBufferSizeOut);
			}
			public virtual uint /*uint32*/ ISteamInventory_GetResultTimestamp( int resultHandle )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamInventory _ptr is null!" );
				
				return Native.SteamAPI_ISteamInventory_GetResultTimestamp(_ptr, resultHandle);
			}
			public virtual bool /*bool*/ ISteamInventory_CheckResultSteamID( int resultHandle, ulong steamIDExpected )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamInventory _ptr is null!" );
				
				return Native.SteamAPI_ISteamInventory_CheckResultSteamID(_ptr, resultHandle, steamIDExpected);
			}
			public virtual void /*void*/ ISteamInventory_DestroyResult( int resultHandle )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamInventory _ptr is null!" );
				
				Native.SteamAPI_ISteamInventory_DestroyResult(_ptr, resultHandle);
			}
			public virtual bool /*bool*/ ISteamInventory_GetAllItems( ref int pResultHandle )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamInventory _ptr is null!" );
				
				return Native.SteamAPI_ISteamInventory_GetAllItems(_ptr, ref pResultHandle);
			}
			public virtual bool /*bool*/ ISteamInventory_GetItemsByID( ref int pResultHandle, ulong[] pInstanceIDs, uint /*uint32*/ unCountInstanceIDs )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamInventory _ptr is null!" );
				
				return Native.SteamAPI_ISteamInventory_GetItemsByID(_ptr, ref pResultHandle, pInstanceIDs, unCountInstanceIDs);
			}
			public virtual bool /*bool*/ ISteamInventory_SerializeResult( int resultHandle, IntPtr /*void **/ pOutBuffer, out uint /*uint32 **/ punOutBufferSize )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamInventory _ptr is null!" );
				
				return Native.SteamAPI_ISteamInventory_SerializeResult(_ptr, resultHandle, pOutBuffer, out punOutBufferSize);
			}
			public virtual bool /*bool*/ ISteamInventory_DeserializeResult( ref int pOutResultHandle, IntPtr /*const void **/ pBuffer, uint /*uint32*/ unBufferSize, [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bRESERVED_MUST_BE_FALSE )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamInventory _ptr is null!" );
				
				return Native.SteamAPI_ISteamInventory_DeserializeResult(_ptr, ref pOutResultHandle, pBuffer, unBufferSize, bRESERVED_MUST_BE_FALSE);
			}
			public virtual bool /*bool*/ ISteamInventory_GenerateItems( ref int pResultHandle, int[] pArrayItemDefs, uint[] /*const uint32 **/ punArrayQuantity, uint /*uint32*/ unArrayLength )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamInventory _ptr is null!" );
				
				return Native.SteamAPI_ISteamInventory_GenerateItems(_ptr, ref pResultHandle, pArrayItemDefs, punArrayQuantity, unArrayLength);
			}
			public virtual bool /*bool*/ ISteamInventory_GrantPromoItems( ref int pResultHandle )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamInventory _ptr is null!" );
				
				return Native.SteamAPI_ISteamInventory_GrantPromoItems(_ptr, ref pResultHandle);
			}
			public virtual bool /*bool*/ ISteamInventory_AddPromoItem( ref int pResultHandle, int itemDef )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamInventory _ptr is null!" );
				
				return Native.SteamAPI_ISteamInventory_AddPromoItem(_ptr, ref pResultHandle, itemDef);
			}
			public virtual bool /*bool*/ ISteamInventory_AddPromoItems( ref int pResultHandle, int[] pArrayItemDefs, uint /*uint32*/ unArrayLength )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamInventory _ptr is null!" );
				
				return Native.SteamAPI_ISteamInventory_AddPromoItems(_ptr, ref pResultHandle, pArrayItemDefs, unArrayLength);
			}
			public virtual bool /*bool*/ ISteamInventory_ConsumeItem( ref int pResultHandle, ulong itemConsume, uint /*uint32*/ unQuantity )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamInventory _ptr is null!" );
				
				return Native.SteamAPI_ISteamInventory_ConsumeItem(_ptr, ref pResultHandle, itemConsume, unQuantity);
			}
			public virtual bool /*bool*/ ISteamInventory_ExchangeItems( ref int pResultHandle, int[] pArrayGenerate, uint[] /*const uint32 **/ punArrayGenerateQuantity, uint /*uint32*/ unArrayGenerateLength, ulong[] pArrayDestroy, uint[] /*const uint32 **/ punArrayDestroyQuantity, uint /*uint32*/ unArrayDestroyLength )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamInventory _ptr is null!" );
				
				return Native.SteamAPI_ISteamInventory_ExchangeItems(_ptr, ref pResultHandle, pArrayGenerate, punArrayGenerateQuantity, unArrayGenerateLength, pArrayDestroy, punArrayDestroyQuantity, unArrayDestroyLength);
			}
			public virtual bool /*bool*/ ISteamInventory_TransferItemQuantity( ref int pResultHandle, ulong itemIdSource, uint /*uint32*/ unQuantity, ulong itemIdDest )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamInventory _ptr is null!" );
				
				return Native.SteamAPI_ISteamInventory_TransferItemQuantity(_ptr, ref pResultHandle, itemIdSource, unQuantity, itemIdDest);
			}
			public virtual void /*void*/ ISteamInventory_SendItemDropHeartbeat()
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamInventory _ptr is null!" );
				
				Native.SteamAPI_ISteamInventory_SendItemDropHeartbeat(_ptr);
			}
			public virtual bool /*bool*/ ISteamInventory_TriggerItemDrop( ref int pResultHandle, int dropListDefinition )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamInventory _ptr is null!" );
				
				return Native.SteamAPI_ISteamInventory_TriggerItemDrop(_ptr, ref pResultHandle, dropListDefinition);
			}
			public virtual bool /*bool*/ ISteamInventory_TradeItems( ref int pResultHandle, ulong steamIDTradePartner, ulong[] pArrayGive, uint[] /*const uint32 **/ pArrayGiveQuantity, uint /*uint32*/ nArrayGiveLength, ulong[] pArrayGet, uint[] /*const uint32 **/ pArrayGetQuantity, uint /*uint32*/ nArrayGetLength )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamInventory _ptr is null!" );
				
				return Native.SteamAPI_ISteamInventory_TradeItems(_ptr, ref pResultHandle, steamIDTradePartner, pArrayGive, pArrayGiveQuantity, nArrayGiveLength, pArrayGet, pArrayGetQuantity, nArrayGetLength);
			}
			public virtual bool /*bool*/ ISteamInventory_LoadItemDefinitions()
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamInventory _ptr is null!" );
				
				return Native.SteamAPI_ISteamInventory_LoadItemDefinitions(_ptr);
			}
			public virtual bool /*bool*/ ISteamInventory_GetItemDefinitionIDs( IntPtr /*SteamItemDef_t **/ pItemDefIDs, out uint /*uint32 **/ punItemDefIDsArraySize )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamInventory _ptr is null!" );
				
				return Native.SteamAPI_ISteamInventory_GetItemDefinitionIDs(_ptr, pItemDefIDs, out punItemDefIDsArraySize);
			}
			public virtual bool /*bool*/ ISteamInventory_GetItemDefinitionProperty( int iDefinition, string /*const char **/ pchPropertyName, System.Text.StringBuilder /*char **/ pchValueBuffer, out uint /*uint32 **/ punValueBufferSizeOut )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamInventory _ptr is null!" );
				
				return Native.SteamAPI_ISteamInventory_GetItemDefinitionProperty(_ptr, iDefinition, pchPropertyName, pchValueBuffer, out punValueBufferSizeOut);
			}
			public virtual SteamAPICall_t /*(SteamAPICall_t)*/ ISteamInventory_RequestEligiblePromoItemDefinitionsIDs( ulong steamID )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamInventory _ptr is null!" );
				
				return Native.SteamAPI_ISteamInventory_RequestEligiblePromoItemDefinitionsIDs(_ptr, steamID);
			}
			public virtual bool /*bool*/ ISteamInventory_GetEligiblePromoItemDefinitionIDs( ulong steamID, IntPtr /*SteamItemDef_t **/ pItemDefIDs, out uint /*uint32 **/ punItemDefIDsArraySize )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamInventory _ptr is null!" );
				
				return Native.SteamAPI_ISteamInventory_GetEligiblePromoItemDefinitionIDs(_ptr, steamID, pItemDefIDs, out punItemDefIDsArraySize);
			}
			public virtual SteamAPICall_t /*(SteamAPICall_t)*/ ISteamInventory_StartPurchase( int[] pArrayItemDefs, uint[] /*const uint32 **/ punArrayQuantity, uint /*uint32*/ unArrayLength )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamInventory _ptr is null!" );
				
				return Native.SteamAPI_ISteamInventory_StartPurchase(_ptr, pArrayItemDefs, punArrayQuantity, unArrayLength);
			}
			public virtual SteamAPICall_t /*(SteamAPICall_t)*/ ISteamInventory_RequestPrices()
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamInventory _ptr is null!" );
				
				return Native.SteamAPI_ISteamInventory_RequestPrices(_ptr);
			}
			public virtual uint /*uint32*/ ISteamInventory_GetNumItemsWithPrices()
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamInventory _ptr is null!" );
				
				return Native.SteamAPI_ISteamInventory_GetNumItemsWithPrices(_ptr);
			}
			public virtual bool /*bool*/ ISteamInventory_GetItemsWithPrices( IntPtr /*SteamItemDef_t **/ pArrayItemDefs, IntPtr /*uint64 **/ pCurrentPrices, IntPtr /*uint64 **/ pBasePrices, uint /*uint32*/ unArrayLength )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamInventory _ptr is null!" );
				
				return Native.SteamAPI_ISteamInventory_GetItemsWithPrices(_ptr, pArrayItemDefs, pCurrentPrices, pBasePrices, unArrayLength);
			}
			public virtual bool /*bool*/ ISteamInventory_GetItemPrice( int iDefinition, out ulong /*uint64 **/ pCurrentPrice, out ulong /*uint64 **/ pBasePrice )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamInventory _ptr is null!" );
				
				return Native.SteamAPI_ISteamInventory_GetItemPrice(_ptr, iDefinition, out pCurrentPrice, out pBasePrice);
			}
			public virtual SteamInventoryUpdateHandle_t /*(SteamInventoryUpdateHandle_t)*/ ISteamInventory_StartUpdateProperties()
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamInventory _ptr is null!" );
				
				return Native.SteamAPI_ISteamInventory_StartUpdateProperties(_ptr);
			}
			public virtual bool /*bool*/ ISteamInventory_RemoveProperty( ulong handle, ulong nItemID, string /*const char **/ pchPropertyName )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamInventory _ptr is null!" );
				
				return Native.SteamAPI_ISteamInventory_RemoveProperty(_ptr, handle, nItemID, pchPropertyName);
			}
			public virtual bool /*bool*/ ISteamInventory_SetProperty( ulong handle, ulong nItemID, string /*const char **/ pchPropertyName, string /*const char **/ pchPropertyValue )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamInventory _ptr is null!" );
				
				return Native.SteamAPI_ISteamInventory_SetProperty(_ptr, handle, nItemID, pchPropertyName, pchPropertyValue);
			}
			public virtual bool /*bool*/ ISteamInventory_SetProperty0( ulong handle, ulong nItemID, string /*const char **/ pchPropertyName, [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bValue )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamInventory _ptr is null!" );
				
				return Native.SteamAPI_ISteamInventory_SetProperty0(_ptr, handle, nItemID, pchPropertyName, bValue);
			}
			public virtual bool /*bool*/ ISteamInventory_SetProperty0( ulong handle, ulong nItemID, string /*const char **/ pchPropertyName, long /*int64*/ nValue )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamInventory _ptr is null!" );
				
				return Native.SteamAPI_ISteamInventory_SetProperty0(_ptr, handle, nItemID, pchPropertyName, nValue);
			}
			public virtual bool /*bool*/ ISteamInventory_SetProperty0( ulong handle, ulong nItemID, string /*const char **/ pchPropertyName, float /*float*/ flValue )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamInventory _ptr is null!" );
				
				return Native.SteamAPI_ISteamInventory_SetProperty0(_ptr, handle, nItemID, pchPropertyName, flValue);
			}
			public virtual bool /*bool*/ ISteamInventory_SubmitUpdateProperties( ulong handle, ref int pResultHandle )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamInventory _ptr is null!" );
				
				return Native.SteamAPI_ISteamInventory_SubmitUpdateProperties(_ptr, handle, ref pResultHandle);
			}
			
			public virtual void /*void*/ ISteamVideo_GetVideoURL( uint unVideoAppID )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamVideo _ptr is null!" );
				
				Native.SteamAPI_ISteamVideo_GetVideoURL(_ptr, unVideoAppID);
			}
			public virtual bool /*bool*/ ISteamVideo_IsBroadcasting( IntPtr /*int **/ pnNumViewers )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamVideo _ptr is null!" );
				
				return Native.SteamAPI_ISteamVideo_IsBroadcasting(_ptr, pnNumViewers);
			}
			public virtual void /*void*/ ISteamVideo_GetOPFSettings( uint unVideoAppID )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamVideo _ptr is null!" );
				
				Native.SteamAPI_ISteamVideo_GetOPFSettings(_ptr, unVideoAppID);
			}
			public virtual bool /*bool*/ ISteamVideo_GetOPFStringForApp( uint unVideoAppID, System.Text.StringBuilder /*char **/ pchBuffer, out int /*int32 **/ pnBufferSize )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamVideo _ptr is null!" );
				
				return Native.SteamAPI_ISteamVideo_GetOPFStringForApp(_ptr, unVideoAppID, pchBuffer, out pnBufferSize);
			}
			
			public virtual bool /*bool*/ ISteamParentalSettings_BIsParentalLockEnabled()
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamParentalSettings _ptr is null!" );
				
				return Native.SteamAPI_ISteamParentalSettings_BIsParentalLockEnabled(_ptr);
			}
			public virtual bool /*bool*/ ISteamParentalSettings_BIsParentalLockLocked()
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamParentalSettings _ptr is null!" );
				
				return Native.SteamAPI_ISteamParentalSettings_BIsParentalLockLocked(_ptr);
			}
			public virtual bool /*bool*/ ISteamParentalSettings_BIsAppBlocked( uint nAppID )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamParentalSettings _ptr is null!" );
				
				return Native.SteamAPI_ISteamParentalSettings_BIsAppBlocked(_ptr, nAppID);
			}
			public virtual bool /*bool*/ ISteamParentalSettings_BIsAppInBlockList( uint nAppID )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamParentalSettings _ptr is null!" );
				
				return Native.SteamAPI_ISteamParentalSettings_BIsAppInBlockList(_ptr, nAppID);
			}
			public virtual bool /*bool*/ ISteamParentalSettings_BIsFeatureBlocked( ParentalFeature /*EParentalFeature*/ eFeature )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamParentalSettings _ptr is null!" );
				
				return Native.SteamAPI_ISteamParentalSettings_BIsFeatureBlocked(_ptr, eFeature);
			}
			public virtual bool /*bool*/ ISteamParentalSettings_BIsFeatureInBlockList( ParentalFeature /*EParentalFeature*/ eFeature )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamParentalSettings _ptr is null!" );
				
				return Native.SteamAPI_ISteamParentalSettings_BIsFeatureInBlockList(_ptr, eFeature);
			}
			
			public virtual bool /*bool*/ ISteamGameServer_InitGameServer( uint /*uint32*/ unIP, ushort /*uint16*/ usGamePort, ushort /*uint16*/ usQueryPort, uint /*uint32*/ unFlags, uint nGameAppId, string /*const char **/ pchVersionString )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamGameServer _ptr is null!" );
				
				return Native.SteamAPI_ISteamGameServer_InitGameServer(_ptr, unIP, usGamePort, usQueryPort, unFlags, nGameAppId, pchVersionString);
			}
			public virtual void /*void*/ ISteamGameServer_SetProduct( string /*const char **/ pszProduct )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamGameServer _ptr is null!" );
				
				Native.SteamAPI_ISteamGameServer_SetProduct(_ptr, pszProduct);
			}
			public virtual void /*void*/ ISteamGameServer_SetGameDescription( string /*const char **/ pszGameDescription )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamGameServer _ptr is null!" );
				
				Native.SteamAPI_ISteamGameServer_SetGameDescription(_ptr, pszGameDescription);
			}
			public virtual void /*void*/ ISteamGameServer_SetModDir( string /*const char **/ pszModDir )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamGameServer _ptr is null!" );
				
				Native.SteamAPI_ISteamGameServer_SetModDir(_ptr, pszModDir);
			}
			public virtual void /*void*/ ISteamGameServer_SetDedicatedServer( [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bDedicated )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamGameServer _ptr is null!" );
				
				Native.SteamAPI_ISteamGameServer_SetDedicatedServer(_ptr, bDedicated);
			}
			public virtual void /*void*/ ISteamGameServer_LogOn( string /*const char **/ pszToken )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamGameServer _ptr is null!" );
				
				Native.SteamAPI_ISteamGameServer_LogOn(_ptr, pszToken);
			}
			public virtual void /*void*/ ISteamGameServer_LogOnAnonymous()
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamGameServer _ptr is null!" );
				
				Native.SteamAPI_ISteamGameServer_LogOnAnonymous(_ptr);
			}
			public virtual void /*void*/ ISteamGameServer_LogOff()
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamGameServer _ptr is null!" );
				
				Native.SteamAPI_ISteamGameServer_LogOff(_ptr);
			}
			public virtual bool /*bool*/ ISteamGameServer_BLoggedOn()
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamGameServer _ptr is null!" );
				
				return Native.SteamAPI_ISteamGameServer_BLoggedOn(_ptr);
			}
			public virtual bool /*bool*/ ISteamGameServer_BSecure()
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamGameServer _ptr is null!" );
				
				return Native.SteamAPI_ISteamGameServer_BSecure(_ptr);
			}
			public virtual CSteamID /*(class CSteamID)*/ ISteamGameServer_GetSteamID()
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamGameServer _ptr is null!" );
				
				return Native.SteamAPI_ISteamGameServer_GetSteamID(_ptr);
			}
			public virtual bool /*bool*/ ISteamGameServer_WasRestartRequested()
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamGameServer _ptr is null!" );
				
				return Native.SteamAPI_ISteamGameServer_WasRestartRequested(_ptr);
			}
			public virtual void /*void*/ ISteamGameServer_SetMaxPlayerCount( int /*int*/ cPlayersMax )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamGameServer _ptr is null!" );
				
				Native.SteamAPI_ISteamGameServer_SetMaxPlayerCount(_ptr, cPlayersMax);
			}
			public virtual void /*void*/ ISteamGameServer_SetBotPlayerCount( int /*int*/ cBotplayers )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamGameServer _ptr is null!" );
				
				Native.SteamAPI_ISteamGameServer_SetBotPlayerCount(_ptr, cBotplayers);
			}
			public virtual void /*void*/ ISteamGameServer_SetServerName( string /*const char **/ pszServerName )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamGameServer _ptr is null!" );
				
				Native.SteamAPI_ISteamGameServer_SetServerName(_ptr, pszServerName);
			}
			public virtual void /*void*/ ISteamGameServer_SetMapName( string /*const char **/ pszMapName )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamGameServer _ptr is null!" );
				
				Native.SteamAPI_ISteamGameServer_SetMapName(_ptr, pszMapName);
			}
			public virtual void /*void*/ ISteamGameServer_SetPasswordProtected( [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bPasswordProtected )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamGameServer _ptr is null!" );
				
				Native.SteamAPI_ISteamGameServer_SetPasswordProtected(_ptr, bPasswordProtected);
			}
			public virtual void /*void*/ ISteamGameServer_SetSpectatorPort( ushort /*uint16*/ unSpectatorPort )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamGameServer _ptr is null!" );
				
				Native.SteamAPI_ISteamGameServer_SetSpectatorPort(_ptr, unSpectatorPort);
			}
			public virtual void /*void*/ ISteamGameServer_SetSpectatorServerName( string /*const char **/ pszSpectatorServerName )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamGameServer _ptr is null!" );
				
				Native.SteamAPI_ISteamGameServer_SetSpectatorServerName(_ptr, pszSpectatorServerName);
			}
			public virtual void /*void*/ ISteamGameServer_ClearAllKeyValues()
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamGameServer _ptr is null!" );
				
				Native.SteamAPI_ISteamGameServer_ClearAllKeyValues(_ptr);
			}
			public virtual void /*void*/ ISteamGameServer_SetKeyValue( string /*const char **/ pKey, string /*const char **/ pValue )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamGameServer _ptr is null!" );
				
				Native.SteamAPI_ISteamGameServer_SetKeyValue(_ptr, pKey, pValue);
			}
			public virtual void /*void*/ ISteamGameServer_SetGameTags( string /*const char **/ pchGameTags )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamGameServer _ptr is null!" );
				
				Native.SteamAPI_ISteamGameServer_SetGameTags(_ptr, pchGameTags);
			}
			public virtual void /*void*/ ISteamGameServer_SetGameData( string /*const char **/ pchGameData )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamGameServer _ptr is null!" );
				
				Native.SteamAPI_ISteamGameServer_SetGameData(_ptr, pchGameData);
			}
			public virtual void /*void*/ ISteamGameServer_SetRegion( string /*const char **/ pszRegion )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamGameServer _ptr is null!" );
				
				Native.SteamAPI_ISteamGameServer_SetRegion(_ptr, pszRegion);
			}
			public virtual bool /*bool*/ ISteamGameServer_SendUserConnectAndAuthenticate( uint /*uint32*/ unIPClient, IntPtr /*const void **/ pvAuthBlob, uint /*uint32*/ cubAuthBlobSize, out ulong pSteamIDUser )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamGameServer _ptr is null!" );
				
				return Native.SteamAPI_ISteamGameServer_SendUserConnectAndAuthenticate(_ptr, unIPClient, pvAuthBlob, cubAuthBlobSize, out pSteamIDUser);
			}
			public virtual CSteamID /*(class CSteamID)*/ ISteamGameServer_CreateUnauthenticatedUserConnection()
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamGameServer _ptr is null!" );
				
				return Native.SteamAPI_ISteamGameServer_CreateUnauthenticatedUserConnection(_ptr);
			}
			public virtual void /*void*/ ISteamGameServer_SendUserDisconnect( ulong steamIDUser )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamGameServer _ptr is null!" );
				
				Native.SteamAPI_ISteamGameServer_SendUserDisconnect(_ptr, steamIDUser);
			}
			public virtual bool /*bool*/ ISteamGameServer_BUpdateUserData( ulong steamIDUser, string /*const char **/ pchPlayerName, uint /*uint32*/ uScore )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamGameServer _ptr is null!" );
				
				return Native.SteamAPI_ISteamGameServer_BUpdateUserData(_ptr, steamIDUser, pchPlayerName, uScore);
			}
			public virtual HAuthTicket /*(HAuthTicket)*/ ISteamGameServer_GetAuthSessionTicket( IntPtr /*void **/ pTicket, int /*int*/ cbMaxTicket, out uint /*uint32 **/ pcbTicket )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamGameServer _ptr is null!" );
				
				return Native.SteamAPI_ISteamGameServer_GetAuthSessionTicket(_ptr, pTicket, cbMaxTicket, out pcbTicket);
			}
			public virtual BeginAuthSessionResult /*EBeginAuthSessionResult*/ ISteamGameServer_BeginAuthSession( IntPtr /*const void **/ pAuthTicket, int /*int*/ cbAuthTicket, ulong steamID )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamGameServer _ptr is null!" );
				
				return Native.SteamAPI_ISteamGameServer_BeginAuthSession(_ptr, pAuthTicket, cbAuthTicket, steamID);
			}
			public virtual void /*void*/ ISteamGameServer_EndAuthSession( ulong steamID )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamGameServer _ptr is null!" );
				
				Native.SteamAPI_ISteamGameServer_EndAuthSession(_ptr, steamID);
			}
			public virtual void /*void*/ ISteamGameServer_CancelAuthTicket( uint hAuthTicket )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamGameServer _ptr is null!" );
				
				Native.SteamAPI_ISteamGameServer_CancelAuthTicket(_ptr, hAuthTicket);
			}
			public virtual UserHasLicenseForAppResult /*EUserHasLicenseForAppResult*/ ISteamGameServer_UserHasLicenseForApp( ulong steamID, uint appID )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamGameServer _ptr is null!" );
				
				return Native.SteamAPI_ISteamGameServer_UserHasLicenseForApp(_ptr, steamID, appID);
			}
			public virtual bool /*bool*/ ISteamGameServer_RequestUserGroupStatus( ulong steamIDUser, ulong steamIDGroup )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamGameServer _ptr is null!" );
				
				return Native.SteamAPI_ISteamGameServer_RequestUserGroupStatus(_ptr, steamIDUser, steamIDGroup);
			}
			public virtual void /*void*/ ISteamGameServer_GetGameplayStats()
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamGameServer _ptr is null!" );
				
				Native.SteamAPI_ISteamGameServer_GetGameplayStats(_ptr);
			}
			public virtual SteamAPICall_t /*(SteamAPICall_t)*/ ISteamGameServer_GetServerReputation()
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamGameServer _ptr is null!" );
				
				return Native.SteamAPI_ISteamGameServer_GetServerReputation(_ptr);
			}
			public virtual uint /*uint32*/ ISteamGameServer_GetPublicIP()
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamGameServer _ptr is null!" );
				
				return Native.SteamAPI_ISteamGameServer_GetPublicIP(_ptr);
			}
			public virtual bool /*bool*/ ISteamGameServer_HandleIncomingPacket( IntPtr /*const void **/ pData, int /*int*/ cbData, uint /*uint32*/ srcIP, ushort /*uint16*/ srcPort )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamGameServer _ptr is null!" );
				
				return Native.SteamAPI_ISteamGameServer_HandleIncomingPacket(_ptr, pData, cbData, srcIP, srcPort);
			}
			public virtual int /*int*/ ISteamGameServer_GetNextOutgoingPacket( IntPtr /*void **/ pOut, int /*int*/ cbMaxOut, out uint /*uint32 **/ pNetAdr, out ushort /*uint16 **/ pPort )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamGameServer _ptr is null!" );
				
				return Native.SteamAPI_ISteamGameServer_GetNextOutgoingPacket(_ptr, pOut, cbMaxOut, out pNetAdr, out pPort);
			}
			public virtual void /*void*/ ISteamGameServer_EnableHeartbeats( [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bActive )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamGameServer _ptr is null!" );
				
				Native.SteamAPI_ISteamGameServer_EnableHeartbeats(_ptr, bActive);
			}
			public virtual void /*void*/ ISteamGameServer_SetHeartbeatInterval( int /*int*/ iHeartbeatInterval )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamGameServer _ptr is null!" );
				
				Native.SteamAPI_ISteamGameServer_SetHeartbeatInterval(_ptr, iHeartbeatInterval);
			}
			public virtual void /*void*/ ISteamGameServer_ForceHeartbeat()
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamGameServer _ptr is null!" );
				
				Native.SteamAPI_ISteamGameServer_ForceHeartbeat(_ptr);
			}
			public virtual SteamAPICall_t /*(SteamAPICall_t)*/ ISteamGameServer_AssociateWithClan( ulong steamIDClan )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamGameServer _ptr is null!" );
				
				return Native.SteamAPI_ISteamGameServer_AssociateWithClan(_ptr, steamIDClan);
			}
			public virtual SteamAPICall_t /*(SteamAPICall_t)*/ ISteamGameServer_ComputeNewPlayerCompatibility( ulong steamIDNewPlayer )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamGameServer _ptr is null!" );
				
				return Native.SteamAPI_ISteamGameServer_ComputeNewPlayerCompatibility(_ptr, steamIDNewPlayer);
			}
			
			public virtual SteamAPICall_t /*(SteamAPICall_t)*/ ISteamGameServerStats_RequestUserStats( ulong steamIDUser )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamGameServerStats _ptr is null!" );
				
				return Native.SteamAPI_ISteamGameServerStats_RequestUserStats(_ptr, steamIDUser);
			}
			public virtual bool /*bool*/ ISteamGameServerStats_GetUserStat( ulong steamIDUser, string /*const char **/ pchName, out int /*int32 **/ pData )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamGameServerStats _ptr is null!" );
				
				return Native.SteamAPI_ISteamGameServerStats_GetUserStat(_ptr, steamIDUser, pchName, out pData);
			}
			public virtual bool /*bool*/ ISteamGameServerStats_GetUserStat0( ulong steamIDUser, string /*const char **/ pchName, out float /*float **/ pData )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamGameServerStats _ptr is null!" );
				
				return Native.SteamAPI_ISteamGameServerStats_GetUserStat0(_ptr, steamIDUser, pchName, out pData);
			}
			public virtual bool /*bool*/ ISteamGameServerStats_GetUserAchievement( ulong steamIDUser, string /*const char **/ pchName, [MarshalAs(UnmanagedType.U1)] ref bool /*bool **/ pbAchieved )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamGameServerStats _ptr is null!" );
				
				return Native.SteamAPI_ISteamGameServerStats_GetUserAchievement(_ptr, steamIDUser, pchName, ref pbAchieved);
			}
			public virtual bool /*bool*/ ISteamGameServerStats_SetUserStat( ulong steamIDUser, string /*const char **/ pchName, int /*int32*/ nData )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamGameServerStats _ptr is null!" );
				
				return Native.SteamAPI_ISteamGameServerStats_SetUserStat(_ptr, steamIDUser, pchName, nData);
			}
			public virtual bool /*bool*/ ISteamGameServerStats_SetUserStat0( ulong steamIDUser, string /*const char **/ pchName, float /*float*/ fData )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamGameServerStats _ptr is null!" );
				
				return Native.SteamAPI_ISteamGameServerStats_SetUserStat0(_ptr, steamIDUser, pchName, fData);
			}
			public virtual bool /*bool*/ ISteamGameServerStats_UpdateUserAvgRateStat( ulong steamIDUser, string /*const char **/ pchName, float /*float*/ flCountThisSession, double /*double*/ dSessionLength )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamGameServerStats _ptr is null!" );
				
				return Native.SteamAPI_ISteamGameServerStats_UpdateUserAvgRateStat(_ptr, steamIDUser, pchName, flCountThisSession, dSessionLength);
			}
			public virtual bool /*bool*/ ISteamGameServerStats_SetUserAchievement( ulong steamIDUser, string /*const char **/ pchName )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamGameServerStats _ptr is null!" );
				
				return Native.SteamAPI_ISteamGameServerStats_SetUserAchievement(_ptr, steamIDUser, pchName);
			}
			public virtual bool /*bool*/ ISteamGameServerStats_ClearUserAchievement( ulong steamIDUser, string /*const char **/ pchName )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamGameServerStats _ptr is null!" );
				
				return Native.SteamAPI_ISteamGameServerStats_ClearUserAchievement(_ptr, steamIDUser, pchName);
			}
			public virtual SteamAPICall_t /*(SteamAPICall_t)*/ ISteamGameServerStats_StoreUserStats( ulong steamIDUser )
			{
				if ( _ptr == IntPtr.Zero ) throw new System.Exception( "ISteamGameServerStats _ptr is null!" );
				
				return Native.SteamAPI_ISteamGameServerStats_StoreUserStats(_ptr, steamIDUser);
			}
			
			public virtual bool /*bool*/ SteamApi_SteamAPI_Init()
			{
				return Native.SteamAPI_Init();
			}
			public virtual void /*void*/ SteamApi_SteamAPI_RunCallbacks()
			{
				Native.SteamAPI_RunCallbacks();
			}
			public virtual void /*void*/ SteamApi_SteamGameServer_RunCallbacks()
			{
				Native.SteamGameServer_RunCallbacks();
			}
			public virtual void /*void*/ SteamApi_SteamAPI_RegisterCallback( IntPtr /*void **/ pCallback, int /*int*/ callback )
			{
				Native.SteamAPI_RegisterCallback(pCallback, callback);
			}
			public virtual void /*void*/ SteamApi_SteamAPI_UnregisterCallback( IntPtr /*void **/ pCallback )
			{
				Native.SteamAPI_UnregisterCallback(pCallback);
			}
			public virtual void /*void*/ SteamApi_SteamAPI_RegisterCallResult( IntPtr /*void **/ pCallback, ulong callback )
			{
				Native.SteamAPI_RegisterCallResult(pCallback, callback);
			}
			public virtual void /*void*/ SteamApi_SteamAPI_UnregisterCallResult( IntPtr /*void **/ pCallback, ulong callback )
			{
				Native.SteamAPI_UnregisterCallResult(pCallback, callback);
			}
			public virtual bool /*bool*/ SteamApi_SteamInternal_GameServer_Init( uint /*uint32*/ unIP, ushort /*uint16*/ usPort, ushort /*uint16*/ usGamePort, ushort /*uint16*/ usQueryPort, int /*int*/ eServerMode, string /*const char **/ pchVersionString )
			{
				return Native.SteamInternal_GameServer_Init(unIP, usPort, usGamePort, usQueryPort, eServerMode, pchVersionString);
			}
			public virtual void /*void*/ SteamApi_SteamAPI_Shutdown()
			{
				Native.SteamAPI_Shutdown();
			}
			public virtual void /*void*/ SteamApi_SteamGameServer_Shutdown()
			{
				Native.SteamGameServer_Shutdown();
			}
			public virtual HSteamUser /*(HSteamUser)*/ SteamApi_SteamAPI_GetHSteamUser()
			{
				return Native.SteamAPI_GetHSteamUser();
			}
			public virtual HSteamPipe /*(HSteamPipe)*/ SteamApi_SteamAPI_GetHSteamPipe()
			{
				return Native.SteamAPI_GetHSteamPipe();
			}
			public virtual HSteamUser /*(HSteamUser)*/ SteamApi_SteamGameServer_GetHSteamUser()
			{
				return Native.SteamGameServer_GetHSteamUser();
			}
			public virtual HSteamPipe /*(HSteamPipe)*/ SteamApi_SteamGameServer_GetHSteamPipe()
			{
				return Native.SteamGameServer_GetHSteamPipe();
			}
			public virtual IntPtr /*void **/ SteamApi_SteamInternal_CreateInterface( string /*const char **/ version )
			{
				return Native.SteamInternal_CreateInterface(version);
			}
			public virtual bool /*bool*/ SteamApi_SteamAPI_RestartAppIfNecessary( uint /*uint32*/ unOwnAppID )
			{
				return Native.SteamAPI_RestartAppIfNecessary(unOwnAppID);
			}
			
			internal static unsafe class Native
			{
				//
				// ISteamClient 
				//
				[DllImport( "libsteam_api.so" )] internal static extern HSteamPipe /*(HSteamPipe)*/ SteamAPI_ISteamClient_CreateSteamPipe( IntPtr ISteamClient );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamClient_BReleaseSteamPipe( IntPtr ISteamClient, int hSteamPipe );
				[DllImport( "libsteam_api.so" )] internal static extern HSteamUser /*(HSteamUser)*/ SteamAPI_ISteamClient_ConnectToGlobalUser( IntPtr ISteamClient, int hSteamPipe );
				[DllImport( "libsteam_api.so" )] internal static extern HSteamUser /*(HSteamUser)*/ SteamAPI_ISteamClient_CreateLocalUser( IntPtr ISteamClient, out int phSteamPipe, AccountType /*EAccountType*/ eAccountType );
				[DllImport( "libsteam_api.so" )] internal static extern void /*void*/ SteamAPI_ISteamClient_ReleaseUser( IntPtr ISteamClient, int hSteamPipe, int hUser );
				[DllImport( "libsteam_api.so" )] internal static extern IntPtr /*class ISteamUser **/ SteamAPI_ISteamClient_GetISteamUser( IntPtr ISteamClient, int hSteamUser, int hSteamPipe, string /*const char **/ pchVersion );
				[DllImport( "libsteam_api.so" )] internal static extern IntPtr /*class ISteamGameServer **/ SteamAPI_ISteamClient_GetISteamGameServer( IntPtr ISteamClient, int hSteamUser, int hSteamPipe, string /*const char **/ pchVersion );
				[DllImport( "libsteam_api.so" )] internal static extern void /*void*/ SteamAPI_ISteamClient_SetLocalIPBinding( IntPtr ISteamClient, uint /*uint32*/ unIP, ushort /*uint16*/ usPort );
				[DllImport( "libsteam_api.so" )] internal static extern IntPtr /*class ISteamFriends **/ SteamAPI_ISteamClient_GetISteamFriends( IntPtr ISteamClient, int hSteamUser, int hSteamPipe, string /*const char **/ pchVersion );
				[DllImport( "libsteam_api.so" )] internal static extern IntPtr /*class ISteamUtils **/ SteamAPI_ISteamClient_GetISteamUtils( IntPtr ISteamClient, int hSteamPipe, string /*const char **/ pchVersion );
				[DllImport( "libsteam_api.so" )] internal static extern IntPtr /*class ISteamMatchmaking **/ SteamAPI_ISteamClient_GetISteamMatchmaking( IntPtr ISteamClient, int hSteamUser, int hSteamPipe, string /*const char **/ pchVersion );
				[DllImport( "libsteam_api.so" )] internal static extern IntPtr /*class ISteamMatchmakingServers **/ SteamAPI_ISteamClient_GetISteamMatchmakingServers( IntPtr ISteamClient, int hSteamUser, int hSteamPipe, string /*const char **/ pchVersion );
				[DllImport( "libsteam_api.so" )] internal static extern IntPtr /*void **/ SteamAPI_ISteamClient_GetISteamGenericInterface( IntPtr ISteamClient, int hSteamUser, int hSteamPipe, string /*const char **/ pchVersion );
				[DllImport( "libsteam_api.so" )] internal static extern IntPtr /*class ISteamUserStats **/ SteamAPI_ISteamClient_GetISteamUserStats( IntPtr ISteamClient, int hSteamUser, int hSteamPipe, string /*const char **/ pchVersion );
				[DllImport( "libsteam_api.so" )] internal static extern IntPtr /*class ISteamGameServerStats **/ SteamAPI_ISteamClient_GetISteamGameServerStats( IntPtr ISteamClient, int hSteamuser, int hSteamPipe, string /*const char **/ pchVersion );
				[DllImport( "libsteam_api.so" )] internal static extern IntPtr /*class ISteamApps **/ SteamAPI_ISteamClient_GetISteamApps( IntPtr ISteamClient, int hSteamUser, int hSteamPipe, string /*const char **/ pchVersion );
				[DllImport( "libsteam_api.so" )] internal static extern IntPtr /*class ISteamNetworking **/ SteamAPI_ISteamClient_GetISteamNetworking( IntPtr ISteamClient, int hSteamUser, int hSteamPipe, string /*const char **/ pchVersion );
				[DllImport( "libsteam_api.so" )] internal static extern IntPtr /*class ISteamRemoteStorage **/ SteamAPI_ISteamClient_GetISteamRemoteStorage( IntPtr ISteamClient, int hSteamuser, int hSteamPipe, string /*const char **/ pchVersion );
				[DllImport( "libsteam_api.so" )] internal static extern IntPtr /*class ISteamScreenshots **/ SteamAPI_ISteamClient_GetISteamScreenshots( IntPtr ISteamClient, int hSteamuser, int hSteamPipe, string /*const char **/ pchVersion );
				[DllImport( "libsteam_api.so" )] internal static extern IntPtr /*class ISteamGameSearch **/ SteamAPI_ISteamClient_GetISteamGameSearch( IntPtr ISteamClient, int hSteamuser, int hSteamPipe, string /*const char **/ pchVersion );
				[DllImport( "libsteam_api.so" )] internal static extern uint /*uint32*/ SteamAPI_ISteamClient_GetIPCCallCount( IntPtr ISteamClient );
				[DllImport( "libsteam_api.so" )] internal static extern void /*void*/ SteamAPI_ISteamClient_SetWarningMessageHook( IntPtr ISteamClient, IntPtr /*SteamAPIWarningMessageHook_t*/ pFunction );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamClient_BShutdownIfAllPipesClosed( IntPtr ISteamClient );
				[DllImport( "libsteam_api.so" )] internal static extern IntPtr /*class ISteamHTTP **/ SteamAPI_ISteamClient_GetISteamHTTP( IntPtr ISteamClient, int hSteamuser, int hSteamPipe, string /*const char **/ pchVersion );
				[DllImport( "libsteam_api.so" )] internal static extern IntPtr /*class ISteamController **/ SteamAPI_ISteamClient_GetISteamController( IntPtr ISteamClient, int hSteamUser, int hSteamPipe, string /*const char **/ pchVersion );
				[DllImport( "libsteam_api.so" )] internal static extern IntPtr /*class ISteamUGC **/ SteamAPI_ISteamClient_GetISteamUGC( IntPtr ISteamClient, int hSteamUser, int hSteamPipe, string /*const char **/ pchVersion );
				[DllImport( "libsteam_api.so" )] internal static extern IntPtr /*class ISteamAppList **/ SteamAPI_ISteamClient_GetISteamAppList( IntPtr ISteamClient, int hSteamUser, int hSteamPipe, string /*const char **/ pchVersion );
				[DllImport( "libsteam_api.so" )] internal static extern IntPtr /*class ISteamMusic **/ SteamAPI_ISteamClient_GetISteamMusic( IntPtr ISteamClient, int hSteamuser, int hSteamPipe, string /*const char **/ pchVersion );
				[DllImport( "libsteam_api.so" )] internal static extern IntPtr /*class ISteamMusicRemote **/ SteamAPI_ISteamClient_GetISteamMusicRemote( IntPtr ISteamClient, int hSteamuser, int hSteamPipe, string /*const char **/ pchVersion );
				[DllImport( "libsteam_api.so" )] internal static extern IntPtr /*class ISteamHTMLSurface **/ SteamAPI_ISteamClient_GetISteamHTMLSurface( IntPtr ISteamClient, int hSteamuser, int hSteamPipe, string /*const char **/ pchVersion );
				[DllImport( "libsteam_api.so" )] internal static extern IntPtr /*class ISteamInventory **/ SteamAPI_ISteamClient_GetISteamInventory( IntPtr ISteamClient, int hSteamuser, int hSteamPipe, string /*const char **/ pchVersion );
				[DllImport( "libsteam_api.so" )] internal static extern IntPtr /*class ISteamVideo **/ SteamAPI_ISteamClient_GetISteamVideo( IntPtr ISteamClient, int hSteamuser, int hSteamPipe, string /*const char **/ pchVersion );
				[DllImport( "libsteam_api.so" )] internal static extern IntPtr /*class ISteamParentalSettings **/ SteamAPI_ISteamClient_GetISteamParentalSettings( IntPtr ISteamClient, int hSteamuser, int hSteamPipe, string /*const char **/ pchVersion );
				[DllImport( "libsteam_api.so" )] internal static extern IntPtr /*class ISteamInput **/ SteamAPI_ISteamClient_GetISteamInput( IntPtr ISteamClient, int hSteamUser, int hSteamPipe, string /*const char **/ pchVersion );
				[DllImport( "libsteam_api.so" )] internal static extern IntPtr /*class ISteamParties **/ SteamAPI_ISteamClient_GetISteamParties( IntPtr ISteamClient, int hSteamUser, int hSteamPipe, string /*const char **/ pchVersion );
				
				//
				// ISteamUser 
				//
				[DllImport( "libsteam_api.so" )] internal static extern HSteamUser /*(HSteamUser)*/ SteamAPI_ISteamUser_GetHSteamUser( IntPtr ISteamUser );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamUser_BLoggedOn( IntPtr ISteamUser );
				[DllImport( "libsteam_api.so" )] internal static extern CSteamID /*(class CSteamID)*/ SteamAPI_ISteamUser_GetSteamID( IntPtr ISteamUser );
				[DllImport( "libsteam_api.so" )] internal static extern int /*int*/ SteamAPI_ISteamUser_InitiateGameConnection( IntPtr ISteamUser, IntPtr /*void **/ pAuthBlob, int /*int*/ cbMaxAuthBlob, ulong steamIDGameServer, uint /*uint32*/ unIPServer, ushort /*uint16*/ usPortServer, [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bSecure );
				[DllImport( "libsteam_api.so" )] internal static extern void /*void*/ SteamAPI_ISteamUser_TerminateGameConnection( IntPtr ISteamUser, uint /*uint32*/ unIPServer, ushort /*uint16*/ usPortServer );
				[DllImport( "libsteam_api.so" )] internal static extern void /*void*/ SteamAPI_ISteamUser_TrackAppUsageEvent( IntPtr ISteamUser, ulong gameID, int /*int*/ eAppUsageEvent, string /*const char **/ pchExtraInfo );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamUser_GetUserDataFolder( IntPtr ISteamUser, System.Text.StringBuilder /*char **/ pchBuffer, int /*int*/ cubBuffer );
				[DllImport( "libsteam_api.so" )] internal static extern void /*void*/ SteamAPI_ISteamUser_StartVoiceRecording( IntPtr ISteamUser );
				[DllImport( "libsteam_api.so" )] internal static extern void /*void*/ SteamAPI_ISteamUser_StopVoiceRecording( IntPtr ISteamUser );
				[DllImport( "libsteam_api.so" )] internal static extern VoiceResult /*EVoiceResult*/ SteamAPI_ISteamUser_GetAvailableVoice( IntPtr ISteamUser, out uint /*uint32 **/ pcbCompressed, out uint /*uint32 **/ pcbUncompressed_Deprecated, uint /*uint32*/ nUncompressedVoiceDesiredSampleRate_Deprecated );
				[DllImport( "libsteam_api.so" )] internal static extern VoiceResult /*EVoiceResult*/ SteamAPI_ISteamUser_GetVoice( IntPtr ISteamUser, [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bWantCompressed, IntPtr /*void **/ pDestBuffer, uint /*uint32*/ cbDestBufferSize, out uint /*uint32 **/ nBytesWritten, [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bWantUncompressed_Deprecated, IntPtr /*void **/ pUncompressedDestBuffer_Deprecated, uint /*uint32*/ cbUncompressedDestBufferSize_Deprecated, out uint /*uint32 **/ nUncompressBytesWritten_Deprecated, uint /*uint32*/ nUncompressedVoiceDesiredSampleRate_Deprecated );
				[DllImport( "libsteam_api.so" )] internal static extern VoiceResult /*EVoiceResult*/ SteamAPI_ISteamUser_DecompressVoice( IntPtr ISteamUser, IntPtr /*const void **/ pCompressed, uint /*uint32*/ cbCompressed, IntPtr /*void **/ pDestBuffer, uint /*uint32*/ cbDestBufferSize, out uint /*uint32 **/ nBytesWritten, uint /*uint32*/ nDesiredSampleRate );
				[DllImport( "libsteam_api.so" )] internal static extern uint /*uint32*/ SteamAPI_ISteamUser_GetVoiceOptimalSampleRate( IntPtr ISteamUser );
				[DllImport( "libsteam_api.so" )] internal static extern HAuthTicket /*(HAuthTicket)*/ SteamAPI_ISteamUser_GetAuthSessionTicket( IntPtr ISteamUser, IntPtr /*void **/ pTicket, int /*int*/ cbMaxTicket, out uint /*uint32 **/ pcbTicket );
				[DllImport( "libsteam_api.so" )] internal static extern BeginAuthSessionResult /*EBeginAuthSessionResult*/ SteamAPI_ISteamUser_BeginAuthSession( IntPtr ISteamUser, IntPtr /*const void **/ pAuthTicket, int /*int*/ cbAuthTicket, ulong steamID );
				[DllImport( "libsteam_api.so" )] internal static extern void /*void*/ SteamAPI_ISteamUser_EndAuthSession( IntPtr ISteamUser, ulong steamID );
				[DllImport( "libsteam_api.so" )] internal static extern void /*void*/ SteamAPI_ISteamUser_CancelAuthTicket( IntPtr ISteamUser, uint hAuthTicket );
				[DllImport( "libsteam_api.so" )] internal static extern UserHasLicenseForAppResult /*EUserHasLicenseForAppResult*/ SteamAPI_ISteamUser_UserHasLicenseForApp( IntPtr ISteamUser, ulong steamID, uint appID );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamUser_BIsBehindNAT( IntPtr ISteamUser );
				[DllImport( "libsteam_api.so" )] internal static extern void /*void*/ SteamAPI_ISteamUser_AdvertiseGame( IntPtr ISteamUser, ulong steamIDGameServer, uint /*uint32*/ unIPServer, ushort /*uint16*/ usPortServer );
				[DllImport( "libsteam_api.so" )] internal static extern SteamAPICall_t /*(SteamAPICall_t)*/ SteamAPI_ISteamUser_RequestEncryptedAppTicket( IntPtr ISteamUser, IntPtr /*void **/ pDataToInclude, int /*int*/ cbDataToInclude );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamUser_GetEncryptedAppTicket( IntPtr ISteamUser, IntPtr /*void **/ pTicket, int /*int*/ cbMaxTicket, out uint /*uint32 **/ pcbTicket );
				[DllImport( "libsteam_api.so" )] internal static extern int /*int*/ SteamAPI_ISteamUser_GetGameBadgeLevel( IntPtr ISteamUser, int /*int*/ nSeries, [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bFoil );
				[DllImport( "libsteam_api.so" )] internal static extern int /*int*/ SteamAPI_ISteamUser_GetPlayerSteamLevel( IntPtr ISteamUser );
				[DllImport( "libsteam_api.so" )] internal static extern SteamAPICall_t /*(SteamAPICall_t)*/ SteamAPI_ISteamUser_RequestStoreAuthURL( IntPtr ISteamUser, string /*const char **/ pchRedirectURL );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamUser_BIsPhoneVerified( IntPtr ISteamUser );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamUser_BIsTwoFactorEnabled( IntPtr ISteamUser );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamUser_BIsPhoneIdentifying( IntPtr ISteamUser );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamUser_BIsPhoneRequiringVerification( IntPtr ISteamUser );
				[DllImport( "libsteam_api.so" )] internal static extern SteamAPICall_t /*(SteamAPICall_t)*/ SteamAPI_ISteamUser_GetMarketEligibility( IntPtr ISteamUser );
				
				//
				// ISteamFriends 
				//
				[DllImport( "libsteam_api.so" )] internal static extern IntPtr SteamAPI_ISteamFriends_GetPersonaName( IntPtr ISteamFriends );
				[DllImport( "libsteam_api.so" )] internal static extern SteamAPICall_t /*(SteamAPICall_t)*/ SteamAPI_ISteamFriends_SetPersonaName( IntPtr ISteamFriends, string /*const char **/ pchPersonaName );
				[DllImport( "libsteam_api.so" )] internal static extern PersonaState /*EPersonaState*/ SteamAPI_ISteamFriends_GetPersonaState( IntPtr ISteamFriends );
				[DllImport( "libsteam_api.so" )] internal static extern int /*int*/ SteamAPI_ISteamFriends_GetFriendCount( IntPtr ISteamFriends, int /*int*/ iFriendFlags );
				[DllImport( "libsteam_api.so" )] internal static extern CSteamID /*(class CSteamID)*/ SteamAPI_ISteamFriends_GetFriendByIndex( IntPtr ISteamFriends, int /*int*/ iFriend, int /*int*/ iFriendFlags );
				[DllImport( "libsteam_api.so" )] internal static extern FriendRelationship /*EFriendRelationship*/ SteamAPI_ISteamFriends_GetFriendRelationship( IntPtr ISteamFriends, ulong steamIDFriend );
				[DllImport( "libsteam_api.so" )] internal static extern PersonaState /*EPersonaState*/ SteamAPI_ISteamFriends_GetFriendPersonaState( IntPtr ISteamFriends, ulong steamIDFriend );
				[DllImport( "libsteam_api.so" )] internal static extern IntPtr SteamAPI_ISteamFriends_GetFriendPersonaName( IntPtr ISteamFriends, ulong steamIDFriend );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamFriends_GetFriendGamePlayed( IntPtr ISteamFriends, ulong steamIDFriend, ref FriendGameInfo_t.PackSmall /*struct FriendGameInfo_t **/ pFriendGameInfo );
				[DllImport( "libsteam_api.so" )] internal static extern IntPtr SteamAPI_ISteamFriends_GetFriendPersonaNameHistory( IntPtr ISteamFriends, ulong steamIDFriend, int /*int*/ iPersonaName );
				[DllImport( "libsteam_api.so" )] internal static extern int /*int*/ SteamAPI_ISteamFriends_GetFriendSteamLevel( IntPtr ISteamFriends, ulong steamIDFriend );
				[DllImport( "libsteam_api.so" )] internal static extern IntPtr SteamAPI_ISteamFriends_GetPlayerNickname( IntPtr ISteamFriends, ulong steamIDPlayer );
				[DllImport( "libsteam_api.so" )] internal static extern int /*int*/ SteamAPI_ISteamFriends_GetFriendsGroupCount( IntPtr ISteamFriends );
				[DllImport( "libsteam_api.so" )] internal static extern FriendsGroupID_t /*(FriendsGroupID_t)*/ SteamAPI_ISteamFriends_GetFriendsGroupIDByIndex( IntPtr ISteamFriends, int /*int*/ iFG );
				[DllImport( "libsteam_api.so" )] internal static extern IntPtr SteamAPI_ISteamFriends_GetFriendsGroupName( IntPtr ISteamFriends, short friendsGroupID );
				[DllImport( "libsteam_api.so" )] internal static extern int /*int*/ SteamAPI_ISteamFriends_GetFriendsGroupMembersCount( IntPtr ISteamFriends, short friendsGroupID );
				[DllImport( "libsteam_api.so" )] internal static extern void /*void*/ SteamAPI_ISteamFriends_GetFriendsGroupMembersList( IntPtr ISteamFriends, short friendsGroupID, IntPtr /*class CSteamID **/ pOutSteamIDMembers, int /*int*/ nMembersCount );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamFriends_HasFriend( IntPtr ISteamFriends, ulong steamIDFriend, int /*int*/ iFriendFlags );
				[DllImport( "libsteam_api.so" )] internal static extern int /*int*/ SteamAPI_ISteamFriends_GetClanCount( IntPtr ISteamFriends );
				[DllImport( "libsteam_api.so" )] internal static extern CSteamID /*(class CSteamID)*/ SteamAPI_ISteamFriends_GetClanByIndex( IntPtr ISteamFriends, int /*int*/ iClan );
				[DllImport( "libsteam_api.so" )] internal static extern IntPtr SteamAPI_ISteamFriends_GetClanName( IntPtr ISteamFriends, ulong steamIDClan );
				[DllImport( "libsteam_api.so" )] internal static extern IntPtr SteamAPI_ISteamFriends_GetClanTag( IntPtr ISteamFriends, ulong steamIDClan );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamFriends_GetClanActivityCounts( IntPtr ISteamFriends, ulong steamIDClan, out int /*int **/ pnOnline, out int /*int **/ pnInGame, out int /*int **/ pnChatting );
				[DllImport( "libsteam_api.so" )] internal static extern SteamAPICall_t /*(SteamAPICall_t)*/ SteamAPI_ISteamFriends_DownloadClanActivityCounts( IntPtr ISteamFriends, IntPtr /*class CSteamID **/ psteamIDClans, int /*int*/ cClansToRequest );
				[DllImport( "libsteam_api.so" )] internal static extern int /*int*/ SteamAPI_ISteamFriends_GetFriendCountFromSource( IntPtr ISteamFriends, ulong steamIDSource );
				[DllImport( "libsteam_api.so" )] internal static extern CSteamID /*(class CSteamID)*/ SteamAPI_ISteamFriends_GetFriendFromSourceByIndex( IntPtr ISteamFriends, ulong steamIDSource, int /*int*/ iFriend );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamFriends_IsUserInSource( IntPtr ISteamFriends, ulong steamIDUser, ulong steamIDSource );
				[DllImport( "libsteam_api.so" )] internal static extern void /*void*/ SteamAPI_ISteamFriends_SetInGameVoiceSpeaking( IntPtr ISteamFriends, ulong steamIDUser, [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bSpeaking );
				[DllImport( "libsteam_api.so" )] internal static extern void /*void*/ SteamAPI_ISteamFriends_ActivateGameOverlay( IntPtr ISteamFriends, string /*const char **/ pchDialog );
				[DllImport( "libsteam_api.so" )] internal static extern void /*void*/ SteamAPI_ISteamFriends_ActivateGameOverlayToUser( IntPtr ISteamFriends, string /*const char **/ pchDialog, ulong steamID );
				[DllImport( "libsteam_api.so" )] internal static extern void /*void*/ SteamAPI_ISteamFriends_ActivateGameOverlayToWebPage( IntPtr ISteamFriends, string /*const char **/ pchURL, ActivateGameOverlayToWebPageMode /*EActivateGameOverlayToWebPageMode*/ eMode );
				[DllImport( "libsteam_api.so" )] internal static extern void /*void*/ SteamAPI_ISteamFriends_ActivateGameOverlayToStore( IntPtr ISteamFriends, uint nAppID, OverlayToStoreFlag /*EOverlayToStoreFlag*/ eFlag );
				[DllImport( "libsteam_api.so" )] internal static extern void /*void*/ SteamAPI_ISteamFriends_SetPlayedWith( IntPtr ISteamFriends, ulong steamIDUserPlayedWith );
				[DllImport( "libsteam_api.so" )] internal static extern void /*void*/ SteamAPI_ISteamFriends_ActivateGameOverlayInviteDialog( IntPtr ISteamFriends, ulong steamIDLobby );
				[DllImport( "libsteam_api.so" )] internal static extern int /*int*/ SteamAPI_ISteamFriends_GetSmallFriendAvatar( IntPtr ISteamFriends, ulong steamIDFriend );
				[DllImport( "libsteam_api.so" )] internal static extern int /*int*/ SteamAPI_ISteamFriends_GetMediumFriendAvatar( IntPtr ISteamFriends, ulong steamIDFriend );
				[DllImport( "libsteam_api.so" )] internal static extern int /*int*/ SteamAPI_ISteamFriends_GetLargeFriendAvatar( IntPtr ISteamFriends, ulong steamIDFriend );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamFriends_RequestUserInformation( IntPtr ISteamFriends, ulong steamIDUser, [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bRequireNameOnly );
				[DllImport( "libsteam_api.so" )] internal static extern SteamAPICall_t /*(SteamAPICall_t)*/ SteamAPI_ISteamFriends_RequestClanOfficerList( IntPtr ISteamFriends, ulong steamIDClan );
				[DllImport( "libsteam_api.so" )] internal static extern CSteamID /*(class CSteamID)*/ SteamAPI_ISteamFriends_GetClanOwner( IntPtr ISteamFriends, ulong steamIDClan );
				[DllImport( "libsteam_api.so" )] internal static extern int /*int*/ SteamAPI_ISteamFriends_GetClanOfficerCount( IntPtr ISteamFriends, ulong steamIDClan );
				[DllImport( "libsteam_api.so" )] internal static extern CSteamID /*(class CSteamID)*/ SteamAPI_ISteamFriends_GetClanOfficerByIndex( IntPtr ISteamFriends, ulong steamIDClan, int /*int*/ iOfficer );
				[DllImport( "libsteam_api.so" )] internal static extern uint /*uint32*/ SteamAPI_ISteamFriends_GetUserRestrictions( IntPtr ISteamFriends );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamFriends_SetRichPresence( IntPtr ISteamFriends, string /*const char **/ pchKey, string /*const char **/ pchValue );
				[DllImport( "libsteam_api.so" )] internal static extern void /*void*/ SteamAPI_ISteamFriends_ClearRichPresence( IntPtr ISteamFriends );
				[DllImport( "libsteam_api.so" )] internal static extern IntPtr SteamAPI_ISteamFriends_GetFriendRichPresence( IntPtr ISteamFriends, ulong steamIDFriend, string /*const char **/ pchKey );
				[DllImport( "libsteam_api.so" )] internal static extern int /*int*/ SteamAPI_ISteamFriends_GetFriendRichPresenceKeyCount( IntPtr ISteamFriends, ulong steamIDFriend );
				[DllImport( "libsteam_api.so" )] internal static extern IntPtr SteamAPI_ISteamFriends_GetFriendRichPresenceKeyByIndex( IntPtr ISteamFriends, ulong steamIDFriend, int /*int*/ iKey );
				[DllImport( "libsteam_api.so" )] internal static extern void /*void*/ SteamAPI_ISteamFriends_RequestFriendRichPresence( IntPtr ISteamFriends, ulong steamIDFriend );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamFriends_InviteUserToGame( IntPtr ISteamFriends, ulong steamIDFriend, string /*const char **/ pchConnectString );
				[DllImport( "libsteam_api.so" )] internal static extern int /*int*/ SteamAPI_ISteamFriends_GetCoplayFriendCount( IntPtr ISteamFriends );
				[DllImport( "libsteam_api.so" )] internal static extern CSteamID /*(class CSteamID)*/ SteamAPI_ISteamFriends_GetCoplayFriend( IntPtr ISteamFriends, int /*int*/ iCoplayFriend );
				[DllImport( "libsteam_api.so" )] internal static extern int /*int*/ SteamAPI_ISteamFriends_GetFriendCoplayTime( IntPtr ISteamFriends, ulong steamIDFriend );
				[DllImport( "libsteam_api.so" )] internal static extern AppId_t /*(AppId_t)*/ SteamAPI_ISteamFriends_GetFriendCoplayGame( IntPtr ISteamFriends, ulong steamIDFriend );
				[DllImport( "libsteam_api.so" )] internal static extern SteamAPICall_t /*(SteamAPICall_t)*/ SteamAPI_ISteamFriends_JoinClanChatRoom( IntPtr ISteamFriends, ulong steamIDClan );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamFriends_LeaveClanChatRoom( IntPtr ISteamFriends, ulong steamIDClan );
				[DllImport( "libsteam_api.so" )] internal static extern int /*int*/ SteamAPI_ISteamFriends_GetClanChatMemberCount( IntPtr ISteamFriends, ulong steamIDClan );
				[DllImport( "libsteam_api.so" )] internal static extern CSteamID /*(class CSteamID)*/ SteamAPI_ISteamFriends_GetChatMemberByIndex( IntPtr ISteamFriends, ulong steamIDClan, int /*int*/ iUser );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamFriends_SendClanChatMessage( IntPtr ISteamFriends, ulong steamIDClanChat, string /*const char **/ pchText );
				[DllImport( "libsteam_api.so" )] internal static extern int /*int*/ SteamAPI_ISteamFriends_GetClanChatMessage( IntPtr ISteamFriends, ulong steamIDClanChat, int /*int*/ iMessage, IntPtr /*void **/ prgchText, int /*int*/ cchTextMax, out ChatEntryType /*EChatEntryType **/ peChatEntryType, out ulong psteamidChatter );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamFriends_IsClanChatAdmin( IntPtr ISteamFriends, ulong steamIDClanChat, ulong steamIDUser );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamFriends_IsClanChatWindowOpenInSteam( IntPtr ISteamFriends, ulong steamIDClanChat );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamFriends_OpenClanChatWindowInSteam( IntPtr ISteamFriends, ulong steamIDClanChat );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamFriends_CloseClanChatWindowInSteam( IntPtr ISteamFriends, ulong steamIDClanChat );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamFriends_SetListenForFriendsMessages( IntPtr ISteamFriends, [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bInterceptEnabled );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamFriends_ReplyToFriendMessage( IntPtr ISteamFriends, ulong steamIDFriend, string /*const char **/ pchMsgToSend );
				[DllImport( "libsteam_api.so" )] internal static extern int /*int*/ SteamAPI_ISteamFriends_GetFriendMessage( IntPtr ISteamFriends, ulong steamIDFriend, int /*int*/ iMessageID, IntPtr /*void **/ pvData, int /*int*/ cubData, out ChatEntryType /*EChatEntryType **/ peChatEntryType );
				[DllImport( "libsteam_api.so" )] internal static extern SteamAPICall_t /*(SteamAPICall_t)*/ SteamAPI_ISteamFriends_GetFollowerCount( IntPtr ISteamFriends, ulong steamID );
				[DllImport( "libsteam_api.so" )] internal static extern SteamAPICall_t /*(SteamAPICall_t)*/ SteamAPI_ISteamFriends_IsFollowing( IntPtr ISteamFriends, ulong steamID );
				[DllImport( "libsteam_api.so" )] internal static extern SteamAPICall_t /*(SteamAPICall_t)*/ SteamAPI_ISteamFriends_EnumerateFollowingList( IntPtr ISteamFriends, uint /*uint32*/ unStartIndex );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamFriends_IsClanPublic( IntPtr ISteamFriends, ulong steamIDClan );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamFriends_IsClanOfficialGameGroup( IntPtr ISteamFriends, ulong steamIDClan );
				[DllImport( "libsteam_api.so" )] internal static extern int /*int*/ SteamAPI_ISteamFriends_GetNumChatsWithUnreadPriorityMessages( IntPtr ISteamFriends );
				
				//
				// ISteamUtils 
				//
				[DllImport( "libsteam_api.so" )] internal static extern uint /*uint32*/ SteamAPI_ISteamUtils_GetSecondsSinceAppActive( IntPtr ISteamUtils );
				[DllImport( "libsteam_api.so" )] internal static extern uint /*uint32*/ SteamAPI_ISteamUtils_GetSecondsSinceComputerActive( IntPtr ISteamUtils );
				[DllImport( "libsteam_api.so" )] internal static extern Universe /*EUniverse*/ SteamAPI_ISteamUtils_GetConnectedUniverse( IntPtr ISteamUtils );
				[DllImport( "libsteam_api.so" )] internal static extern uint /*uint32*/ SteamAPI_ISteamUtils_GetServerRealTime( IntPtr ISteamUtils );
				[DllImport( "libsteam_api.so" )] internal static extern IntPtr SteamAPI_ISteamUtils_GetIPCountry( IntPtr ISteamUtils );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamUtils_GetImageSize( IntPtr ISteamUtils, int /*int*/ iImage, out uint /*uint32 **/ pnWidth, out uint /*uint32 **/ pnHeight );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamUtils_GetImageRGBA( IntPtr ISteamUtils, int /*int*/ iImage, IntPtr /*uint8 **/ pubDest, int /*int*/ nDestBufferSize );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamUtils_GetCSERIPPort( IntPtr ISteamUtils, out uint /*uint32 **/ unIP, out ushort /*uint16 **/ usPort );
				[DllImport( "libsteam_api.so" )] internal static extern byte /*uint8*/ SteamAPI_ISteamUtils_GetCurrentBatteryPower( IntPtr ISteamUtils );
				[DllImport( "libsteam_api.so" )] internal static extern uint /*uint32*/ SteamAPI_ISteamUtils_GetAppID( IntPtr ISteamUtils );
				[DllImport( "libsteam_api.so" )] internal static extern void /*void*/ SteamAPI_ISteamUtils_SetOverlayNotificationPosition( IntPtr ISteamUtils, NotificationPosition /*ENotificationPosition*/ eNotificationPosition );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamUtils_IsAPICallCompleted( IntPtr ISteamUtils, ulong hSteamAPICall, [MarshalAs(UnmanagedType.U1)] ref bool /*bool **/ pbFailed );
				[DllImport( "libsteam_api.so" )] internal static extern SteamAPICallFailure /*ESteamAPICallFailure*/ SteamAPI_ISteamUtils_GetAPICallFailureReason( IntPtr ISteamUtils, ulong hSteamAPICall );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamUtils_GetAPICallResult( IntPtr ISteamUtils, ulong hSteamAPICall, IntPtr /*void **/ pCallback, int /*int*/ cubCallback, int /*int*/ iCallbackExpected, [MarshalAs(UnmanagedType.U1)] ref bool /*bool **/ pbFailed );
				[DllImport( "libsteam_api.so" )] internal static extern uint /*uint32*/ SteamAPI_ISteamUtils_GetIPCCallCount( IntPtr ISteamUtils );
				[DllImport( "libsteam_api.so" )] internal static extern void /*void*/ SteamAPI_ISteamUtils_SetWarningMessageHook( IntPtr ISteamUtils, IntPtr /*SteamAPIWarningMessageHook_t*/ pFunction );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamUtils_IsOverlayEnabled( IntPtr ISteamUtils );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamUtils_BOverlayNeedsPresent( IntPtr ISteamUtils );
				[DllImport( "libsteam_api.so" )] internal static extern SteamAPICall_t /*(SteamAPICall_t)*/ SteamAPI_ISteamUtils_CheckFileSignature( IntPtr ISteamUtils, string /*const char **/ szFileName );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamUtils_ShowGamepadTextInput( IntPtr ISteamUtils, GamepadTextInputMode /*EGamepadTextInputMode*/ eInputMode, GamepadTextInputLineMode /*EGamepadTextInputLineMode*/ eLineInputMode, string /*const char **/ pchDescription, uint /*uint32*/ unCharMax, string /*const char **/ pchExistingText );
				[DllImport( "libsteam_api.so" )] internal static extern uint /*uint32*/ SteamAPI_ISteamUtils_GetEnteredGamepadTextLength( IntPtr ISteamUtils );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamUtils_GetEnteredGamepadTextInput( IntPtr ISteamUtils, System.Text.StringBuilder /*char **/ pchText, uint /*uint32*/ cchText );
				[DllImport( "libsteam_api.so" )] internal static extern IntPtr SteamAPI_ISteamUtils_GetSteamUILanguage( IntPtr ISteamUtils );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamUtils_IsSteamRunningInVR( IntPtr ISteamUtils );
				[DllImport( "libsteam_api.so" )] internal static extern void /*void*/ SteamAPI_ISteamUtils_SetOverlayNotificationInset( IntPtr ISteamUtils, int /*int*/ nHorizontalInset, int /*int*/ nVerticalInset );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamUtils_IsSteamInBigPictureMode( IntPtr ISteamUtils );
				[DllImport( "libsteam_api.so" )] internal static extern void /*void*/ SteamAPI_ISteamUtils_StartVRDashboard( IntPtr ISteamUtils );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamUtils_IsVRHeadsetStreamingEnabled( IntPtr ISteamUtils );
				[DllImport( "libsteam_api.so" )] internal static extern void /*void*/ SteamAPI_ISteamUtils_SetVRHeadsetStreamingEnabled( IntPtr ISteamUtils, [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bEnabled );
				
				//
				// ISteamMatchmaking 
				//
				[DllImport( "libsteam_api.so" )] internal static extern int /*int*/ SteamAPI_ISteamMatchmaking_GetFavoriteGameCount( IntPtr ISteamMatchmaking );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamMatchmaking_GetFavoriteGame( IntPtr ISteamMatchmaking, int /*int*/ iGame, ref uint pnAppID, out uint /*uint32 **/ pnIP, out ushort /*uint16 **/ pnConnPort, out ushort /*uint16 **/ pnQueryPort, out uint /*uint32 **/ punFlags, out uint /*uint32 **/ pRTime32LastPlayedOnServer );
				[DllImport( "libsteam_api.so" )] internal static extern int /*int*/ SteamAPI_ISteamMatchmaking_AddFavoriteGame( IntPtr ISteamMatchmaking, uint nAppID, uint /*uint32*/ nIP, ushort /*uint16*/ nConnPort, ushort /*uint16*/ nQueryPort, uint /*uint32*/ unFlags, uint /*uint32*/ rTime32LastPlayedOnServer );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamMatchmaking_RemoveFavoriteGame( IntPtr ISteamMatchmaking, uint nAppID, uint /*uint32*/ nIP, ushort /*uint16*/ nConnPort, ushort /*uint16*/ nQueryPort, uint /*uint32*/ unFlags );
				[DllImport( "libsteam_api.so" )] internal static extern SteamAPICall_t /*(SteamAPICall_t)*/ SteamAPI_ISteamMatchmaking_RequestLobbyList( IntPtr ISteamMatchmaking );
				[DllImport( "libsteam_api.so" )] internal static extern void /*void*/ SteamAPI_ISteamMatchmaking_AddRequestLobbyListStringFilter( IntPtr ISteamMatchmaking, string /*const char **/ pchKeyToMatch, string /*const char **/ pchValueToMatch, LobbyComparison /*ELobbyComparison*/ eComparisonType );
				[DllImport( "libsteam_api.so" )] internal static extern void /*void*/ SteamAPI_ISteamMatchmaking_AddRequestLobbyListNumericalFilter( IntPtr ISteamMatchmaking, string /*const char **/ pchKeyToMatch, int /*int*/ nValueToMatch, LobbyComparison /*ELobbyComparison*/ eComparisonType );
				[DllImport( "libsteam_api.so" )] internal static extern void /*void*/ SteamAPI_ISteamMatchmaking_AddRequestLobbyListNearValueFilter( IntPtr ISteamMatchmaking, string /*const char **/ pchKeyToMatch, int /*int*/ nValueToBeCloseTo );
				[DllImport( "libsteam_api.so" )] internal static extern void /*void*/ SteamAPI_ISteamMatchmaking_AddRequestLobbyListFilterSlotsAvailable( IntPtr ISteamMatchmaking, int /*int*/ nSlotsAvailable );
				[DllImport( "libsteam_api.so" )] internal static extern void /*void*/ SteamAPI_ISteamMatchmaking_AddRequestLobbyListDistanceFilter( IntPtr ISteamMatchmaking, LobbyDistanceFilter /*ELobbyDistanceFilter*/ eLobbyDistanceFilter );
				[DllImport( "libsteam_api.so" )] internal static extern void /*void*/ SteamAPI_ISteamMatchmaking_AddRequestLobbyListResultCountFilter( IntPtr ISteamMatchmaking, int /*int*/ cMaxResults );
				[DllImport( "libsteam_api.so" )] internal static extern void /*void*/ SteamAPI_ISteamMatchmaking_AddRequestLobbyListCompatibleMembersFilter( IntPtr ISteamMatchmaking, ulong steamIDLobby );
				[DllImport( "libsteam_api.so" )] internal static extern CSteamID /*(class CSteamID)*/ SteamAPI_ISteamMatchmaking_GetLobbyByIndex( IntPtr ISteamMatchmaking, int /*int*/ iLobby );
				[DllImport( "libsteam_api.so" )] internal static extern SteamAPICall_t /*(SteamAPICall_t)*/ SteamAPI_ISteamMatchmaking_CreateLobby( IntPtr ISteamMatchmaking, LobbyType /*ELobbyType*/ eLobbyType, int /*int*/ cMaxMembers );
				[DllImport( "libsteam_api.so" )] internal static extern SteamAPICall_t /*(SteamAPICall_t)*/ SteamAPI_ISteamMatchmaking_JoinLobby( IntPtr ISteamMatchmaking, ulong steamIDLobby );
				[DllImport( "libsteam_api.so" )] internal static extern void /*void*/ SteamAPI_ISteamMatchmaking_LeaveLobby( IntPtr ISteamMatchmaking, ulong steamIDLobby );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamMatchmaking_InviteUserToLobby( IntPtr ISteamMatchmaking, ulong steamIDLobby, ulong steamIDInvitee );
				[DllImport( "libsteam_api.so" )] internal static extern int /*int*/ SteamAPI_ISteamMatchmaking_GetNumLobbyMembers( IntPtr ISteamMatchmaking, ulong steamIDLobby );
				[DllImport( "libsteam_api.so" )] internal static extern CSteamID /*(class CSteamID)*/ SteamAPI_ISteamMatchmaking_GetLobbyMemberByIndex( IntPtr ISteamMatchmaking, ulong steamIDLobby, int /*int*/ iMember );
				[DllImport( "libsteam_api.so" )] internal static extern IntPtr SteamAPI_ISteamMatchmaking_GetLobbyData( IntPtr ISteamMatchmaking, ulong steamIDLobby, string /*const char **/ pchKey );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamMatchmaking_SetLobbyData( IntPtr ISteamMatchmaking, ulong steamIDLobby, string /*const char **/ pchKey, string /*const char **/ pchValue );
				[DllImport( "libsteam_api.so" )] internal static extern int /*int*/ SteamAPI_ISteamMatchmaking_GetLobbyDataCount( IntPtr ISteamMatchmaking, ulong steamIDLobby );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamMatchmaking_GetLobbyDataByIndex( IntPtr ISteamMatchmaking, ulong steamIDLobby, int /*int*/ iLobbyData, System.Text.StringBuilder /*char **/ pchKey, int /*int*/ cchKeyBufferSize, System.Text.StringBuilder /*char **/ pchValue, int /*int*/ cchValueBufferSize );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamMatchmaking_DeleteLobbyData( IntPtr ISteamMatchmaking, ulong steamIDLobby, string /*const char **/ pchKey );
				[DllImport( "libsteam_api.so" )] internal static extern IntPtr SteamAPI_ISteamMatchmaking_GetLobbyMemberData( IntPtr ISteamMatchmaking, ulong steamIDLobby, ulong steamIDUser, string /*const char **/ pchKey );
				[DllImport( "libsteam_api.so" )] internal static extern void /*void*/ SteamAPI_ISteamMatchmaking_SetLobbyMemberData( IntPtr ISteamMatchmaking, ulong steamIDLobby, string /*const char **/ pchKey, string /*const char **/ pchValue );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamMatchmaking_SendLobbyChatMsg( IntPtr ISteamMatchmaking, ulong steamIDLobby, IntPtr /*const void **/ pvMsgBody, int /*int*/ cubMsgBody );
				[DllImport( "libsteam_api.so" )] internal static extern int /*int*/ SteamAPI_ISteamMatchmaking_GetLobbyChatEntry( IntPtr ISteamMatchmaking, ulong steamIDLobby, int /*int*/ iChatID, out ulong pSteamIDUser, IntPtr /*void **/ pvData, int /*int*/ cubData, out ChatEntryType /*EChatEntryType **/ peChatEntryType );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamMatchmaking_RequestLobbyData( IntPtr ISteamMatchmaking, ulong steamIDLobby );
				[DllImport( "libsteam_api.so" )] internal static extern void /*void*/ SteamAPI_ISteamMatchmaking_SetLobbyGameServer( IntPtr ISteamMatchmaking, ulong steamIDLobby, uint /*uint32*/ unGameServerIP, ushort /*uint16*/ unGameServerPort, ulong steamIDGameServer );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamMatchmaking_GetLobbyGameServer( IntPtr ISteamMatchmaking, ulong steamIDLobby, out uint /*uint32 **/ punGameServerIP, out ushort /*uint16 **/ punGameServerPort, out ulong psteamIDGameServer );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamMatchmaking_SetLobbyMemberLimit( IntPtr ISteamMatchmaking, ulong steamIDLobby, int /*int*/ cMaxMembers );
				[DllImport( "libsteam_api.so" )] internal static extern int /*int*/ SteamAPI_ISteamMatchmaking_GetLobbyMemberLimit( IntPtr ISteamMatchmaking, ulong steamIDLobby );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamMatchmaking_SetLobbyType( IntPtr ISteamMatchmaking, ulong steamIDLobby, LobbyType /*ELobbyType*/ eLobbyType );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamMatchmaking_SetLobbyJoinable( IntPtr ISteamMatchmaking, ulong steamIDLobby, [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bLobbyJoinable );
				[DllImport( "libsteam_api.so" )] internal static extern CSteamID /*(class CSteamID)*/ SteamAPI_ISteamMatchmaking_GetLobbyOwner( IntPtr ISteamMatchmaking, ulong steamIDLobby );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamMatchmaking_SetLobbyOwner( IntPtr ISteamMatchmaking, ulong steamIDLobby, ulong steamIDNewOwner );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamMatchmaking_SetLinkedLobby( IntPtr ISteamMatchmaking, ulong steamIDLobby, ulong steamIDLobbyDependent );
				
				//
				// ISteamMatchmakingServers 
				//
				[DllImport( "libsteam_api.so" )] internal static extern HServerListRequest /*(HServerListRequest)*/ SteamAPI_ISteamMatchmakingServers_RequestInternetServerList( IntPtr ISteamMatchmakingServers, uint iApp, IntPtr /*struct MatchMakingKeyValuePair_t ***/ ppchFilters, uint /*uint32*/ nFilters, IntPtr /*class ISteamMatchmakingServerListResponse **/ pRequestServersResponse );
				[DllImport( "libsteam_api.so" )] internal static extern HServerListRequest /*(HServerListRequest)*/ SteamAPI_ISteamMatchmakingServers_RequestLANServerList( IntPtr ISteamMatchmakingServers, uint iApp, IntPtr /*class ISteamMatchmakingServerListResponse **/ pRequestServersResponse );
				[DllImport( "libsteam_api.so" )] internal static extern HServerListRequest /*(HServerListRequest)*/ SteamAPI_ISteamMatchmakingServers_RequestFriendsServerList( IntPtr ISteamMatchmakingServers, uint iApp, IntPtr /*struct MatchMakingKeyValuePair_t ***/ ppchFilters, uint /*uint32*/ nFilters, IntPtr /*class ISteamMatchmakingServerListResponse **/ pRequestServersResponse );
				[DllImport( "libsteam_api.so" )] internal static extern HServerListRequest /*(HServerListRequest)*/ SteamAPI_ISteamMatchmakingServers_RequestFavoritesServerList( IntPtr ISteamMatchmakingServers, uint iApp, IntPtr /*struct MatchMakingKeyValuePair_t ***/ ppchFilters, uint /*uint32*/ nFilters, IntPtr /*class ISteamMatchmakingServerListResponse **/ pRequestServersResponse );
				[DllImport( "libsteam_api.so" )] internal static extern HServerListRequest /*(HServerListRequest)*/ SteamAPI_ISteamMatchmakingServers_RequestHistoryServerList( IntPtr ISteamMatchmakingServers, uint iApp, IntPtr /*struct MatchMakingKeyValuePair_t ***/ ppchFilters, uint /*uint32*/ nFilters, IntPtr /*class ISteamMatchmakingServerListResponse **/ pRequestServersResponse );
				[DllImport( "libsteam_api.so" )] internal static extern HServerListRequest /*(HServerListRequest)*/ SteamAPI_ISteamMatchmakingServers_RequestSpectatorServerList( IntPtr ISteamMatchmakingServers, uint iApp, IntPtr /*struct MatchMakingKeyValuePair_t ***/ ppchFilters, uint /*uint32*/ nFilters, IntPtr /*class ISteamMatchmakingServerListResponse **/ pRequestServersResponse );
				[DllImport( "libsteam_api.so" )] internal static extern void /*void*/ SteamAPI_ISteamMatchmakingServers_ReleaseRequest( IntPtr ISteamMatchmakingServers, IntPtr hServerListRequest );
				[DllImport( "libsteam_api.so" )] internal static extern IntPtr /*class gameserveritem_t **/ SteamAPI_ISteamMatchmakingServers_GetServerDetails( IntPtr ISteamMatchmakingServers, IntPtr hRequest, int /*int*/ iServer );
				[DllImport( "libsteam_api.so" )] internal static extern void /*void*/ SteamAPI_ISteamMatchmakingServers_CancelQuery( IntPtr ISteamMatchmakingServers, IntPtr hRequest );
				[DllImport( "libsteam_api.so" )] internal static extern void /*void*/ SteamAPI_ISteamMatchmakingServers_RefreshQuery( IntPtr ISteamMatchmakingServers, IntPtr hRequest );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamMatchmakingServers_IsRefreshing( IntPtr ISteamMatchmakingServers, IntPtr hRequest );
				[DllImport( "libsteam_api.so" )] internal static extern int /*int*/ SteamAPI_ISteamMatchmakingServers_GetServerCount( IntPtr ISteamMatchmakingServers, IntPtr hRequest );
				[DllImport( "libsteam_api.so" )] internal static extern void /*void*/ SteamAPI_ISteamMatchmakingServers_RefreshServer( IntPtr ISteamMatchmakingServers, IntPtr hRequest, int /*int*/ iServer );
				[DllImport( "libsteam_api.so" )] internal static extern HServerQuery /*(HServerQuery)*/ SteamAPI_ISteamMatchmakingServers_PingServer( IntPtr ISteamMatchmakingServers, uint /*uint32*/ unIP, ushort /*uint16*/ usPort, IntPtr /*class ISteamMatchmakingPingResponse **/ pRequestServersResponse );
				[DllImport( "libsteam_api.so" )] internal static extern HServerQuery /*(HServerQuery)*/ SteamAPI_ISteamMatchmakingServers_PlayerDetails( IntPtr ISteamMatchmakingServers, uint /*uint32*/ unIP, ushort /*uint16*/ usPort, IntPtr /*class ISteamMatchmakingPlayersResponse **/ pRequestServersResponse );
				[DllImport( "libsteam_api.so" )] internal static extern HServerQuery /*(HServerQuery)*/ SteamAPI_ISteamMatchmakingServers_ServerRules( IntPtr ISteamMatchmakingServers, uint /*uint32*/ unIP, ushort /*uint16*/ usPort, IntPtr /*class ISteamMatchmakingRulesResponse **/ pRequestServersResponse );
				[DllImport( "libsteam_api.so" )] internal static extern void /*void*/ SteamAPI_ISteamMatchmakingServers_CancelServerQuery( IntPtr ISteamMatchmakingServers, int hServerQuery );
				
				//
				// ISteamGameSearch 
				//
				[DllImport( "libsteam_api.so" )] internal static extern GameSearchErrorCode_t /*EGameSearchErrorCode_t*/ SteamAPI_ISteamGameSearch_AddGameSearchParams( IntPtr ISteamGameSearch, string /*const char **/ pchKeyToFind, string /*const char **/ pchValuesToFind );
				[DllImport( "libsteam_api.so" )] internal static extern GameSearchErrorCode_t /*EGameSearchErrorCode_t*/ SteamAPI_ISteamGameSearch_SearchForGameWithLobby( IntPtr ISteamGameSearch, ulong steamIDLobby, int /*int*/ nPlayerMin, int /*int*/ nPlayerMax );
				[DllImport( "libsteam_api.so" )] internal static extern GameSearchErrorCode_t /*EGameSearchErrorCode_t*/ SteamAPI_ISteamGameSearch_SearchForGameSolo( IntPtr ISteamGameSearch, int /*int*/ nPlayerMin, int /*int*/ nPlayerMax );
				[DllImport( "libsteam_api.so" )] internal static extern GameSearchErrorCode_t /*EGameSearchErrorCode_t*/ SteamAPI_ISteamGameSearch_AcceptGame( IntPtr ISteamGameSearch );
				[DllImport( "libsteam_api.so" )] internal static extern GameSearchErrorCode_t /*EGameSearchErrorCode_t*/ SteamAPI_ISteamGameSearch_DeclineGame( IntPtr ISteamGameSearch );
				[DllImport( "libsteam_api.so" )] internal static extern GameSearchErrorCode_t /*EGameSearchErrorCode_t*/ SteamAPI_ISteamGameSearch_RetrieveConnectionDetails( IntPtr ISteamGameSearch, ulong steamIDHost, System.Text.StringBuilder /*char **/ pchConnectionDetails, int /*int*/ cubConnectionDetails );
				[DllImport( "libsteam_api.so" )] internal static extern GameSearchErrorCode_t /*EGameSearchErrorCode_t*/ SteamAPI_ISteamGameSearch_EndGameSearch( IntPtr ISteamGameSearch );
				[DllImport( "libsteam_api.so" )] internal static extern GameSearchErrorCode_t /*EGameSearchErrorCode_t*/ SteamAPI_ISteamGameSearch_SetGameHostParams( IntPtr ISteamGameSearch, string /*const char **/ pchKey, string /*const char **/ pchValue );
				[DllImport( "libsteam_api.so" )] internal static extern GameSearchErrorCode_t /*EGameSearchErrorCode_t*/ SteamAPI_ISteamGameSearch_SetConnectionDetails( IntPtr ISteamGameSearch, string /*const char **/ pchConnectionDetails, int /*int*/ cubConnectionDetails );
				[DllImport( "libsteam_api.so" )] internal static extern GameSearchErrorCode_t /*EGameSearchErrorCode_t*/ SteamAPI_ISteamGameSearch_RequestPlayersForGame( IntPtr ISteamGameSearch, int /*int*/ nPlayerMin, int /*int*/ nPlayerMax, int /*int*/ nMaxTeamSize );
				[DllImport( "libsteam_api.so" )] internal static extern GameSearchErrorCode_t /*EGameSearchErrorCode_t*/ SteamAPI_ISteamGameSearch_HostConfirmGameStart( IntPtr ISteamGameSearch, ulong /*uint64*/ ullUniqueGameID );
				[DllImport( "libsteam_api.so" )] internal static extern GameSearchErrorCode_t /*EGameSearchErrorCode_t*/ SteamAPI_ISteamGameSearch_CancelRequestPlayersForGame( IntPtr ISteamGameSearch );
				[DllImport( "libsteam_api.so" )] internal static extern GameSearchErrorCode_t /*EGameSearchErrorCode_t*/ SteamAPI_ISteamGameSearch_SubmitPlayerResult( IntPtr ISteamGameSearch, ulong /*uint64*/ ullUniqueGameID, ulong steamIDPlayer, PlayerResult_t /*EPlayerResult_t*/ EPlayerResult );
				[DllImport( "libsteam_api.so" )] internal static extern GameSearchErrorCode_t /*EGameSearchErrorCode_t*/ SteamAPI_ISteamGameSearch_EndGame( IntPtr ISteamGameSearch, ulong /*uint64*/ ullUniqueGameID );
				
				//
				// ISteamParties 
				//
				[DllImport( "libsteam_api.so" )] internal static extern uint /*uint32*/ SteamAPI_ISteamParties_GetNumActiveBeacons( IntPtr ISteamParties );
				[DllImport( "libsteam_api.so" )] internal static extern PartyBeaconID_t /*(PartyBeaconID_t)*/ SteamAPI_ISteamParties_GetBeaconByIndex( IntPtr ISteamParties, uint /*uint32*/ unIndex );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamParties_GetBeaconDetails( IntPtr ISteamParties, ulong ulBeaconID, out ulong pSteamIDBeaconOwner, ref SteamPartyBeaconLocation_t.PackSmall /*struct SteamPartyBeaconLocation_t **/ pLocation, System.Text.StringBuilder /*char **/ pchMetadata, int /*int*/ cchMetadata );
				[DllImport( "libsteam_api.so" )] internal static extern SteamAPICall_t /*(SteamAPICall_t)*/ SteamAPI_ISteamParties_JoinParty( IntPtr ISteamParties, ulong ulBeaconID );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamParties_GetNumAvailableBeaconLocations( IntPtr ISteamParties, IntPtr /*uint32 **/ puNumLocations );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamParties_GetAvailableBeaconLocations( IntPtr ISteamParties, ref SteamPartyBeaconLocation_t.PackSmall /*struct SteamPartyBeaconLocation_t **/ pLocationList, uint /*uint32*/ uMaxNumLocations );
				[DllImport( "libsteam_api.so" )] internal static extern SteamAPICall_t /*(SteamAPICall_t)*/ SteamAPI_ISteamParties_CreateBeacon( IntPtr ISteamParties, uint /*uint32*/ unOpenSlots, ref SteamPartyBeaconLocation_t.PackSmall /*struct SteamPartyBeaconLocation_t **/ pBeaconLocation, string /*const char **/ pchConnectString, string /*const char **/ pchMetadata );
				[DllImport( "libsteam_api.so" )] internal static extern void /*void*/ SteamAPI_ISteamParties_OnReservationCompleted( IntPtr ISteamParties, ulong ulBeacon, ulong steamIDUser );
				[DllImport( "libsteam_api.so" )] internal static extern void /*void*/ SteamAPI_ISteamParties_CancelReservation( IntPtr ISteamParties, ulong ulBeacon, ulong steamIDUser );
				[DllImport( "libsteam_api.so" )] internal static extern SteamAPICall_t /*(SteamAPICall_t)*/ SteamAPI_ISteamParties_ChangeNumOpenSlots( IntPtr ISteamParties, ulong ulBeacon, uint /*uint32*/ unOpenSlots );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamParties_DestroyBeacon( IntPtr ISteamParties, ulong ulBeacon );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamParties_GetBeaconLocationData( IntPtr ISteamParties, SteamPartyBeaconLocation_t /*struct SteamPartyBeaconLocation_t*/ BeaconLocation, SteamPartyBeaconLocationData /*ESteamPartyBeaconLocationData*/ eData, System.Text.StringBuilder /*char **/ pchDataStringOut, int /*int*/ cchDataStringOut );
				
				//
				// ISteamRemoteStorage 
				//
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamRemoteStorage_FileWrite( IntPtr ISteamRemoteStorage, string /*const char **/ pchFile, IntPtr /*const void **/ pvData, int /*int32*/ cubData );
				[DllImport( "libsteam_api.so" )] internal static extern int /*int32*/ SteamAPI_ISteamRemoteStorage_FileRead( IntPtr ISteamRemoteStorage, string /*const char **/ pchFile, IntPtr /*void **/ pvData, int /*int32*/ cubDataToRead );
				[DllImport( "libsteam_api.so" )] internal static extern SteamAPICall_t /*(SteamAPICall_t)*/ SteamAPI_ISteamRemoteStorage_FileWriteAsync( IntPtr ISteamRemoteStorage, string /*const char **/ pchFile, IntPtr /*const void **/ pvData, uint /*uint32*/ cubData );
				[DllImport( "libsteam_api.so" )] internal static extern SteamAPICall_t /*(SteamAPICall_t)*/ SteamAPI_ISteamRemoteStorage_FileReadAsync( IntPtr ISteamRemoteStorage, string /*const char **/ pchFile, uint /*uint32*/ nOffset, uint /*uint32*/ cubToRead );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamRemoteStorage_FileReadAsyncComplete( IntPtr ISteamRemoteStorage, ulong hReadCall, IntPtr /*void **/ pvBuffer, uint /*uint32*/ cubToRead );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamRemoteStorage_FileForget( IntPtr ISteamRemoteStorage, string /*const char **/ pchFile );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamRemoteStorage_FileDelete( IntPtr ISteamRemoteStorage, string /*const char **/ pchFile );
				[DllImport( "libsteam_api.so" )] internal static extern SteamAPICall_t /*(SteamAPICall_t)*/ SteamAPI_ISteamRemoteStorage_FileShare( IntPtr ISteamRemoteStorage, string /*const char **/ pchFile );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamRemoteStorage_SetSyncPlatforms( IntPtr ISteamRemoteStorage, string /*const char **/ pchFile, RemoteStoragePlatform /*ERemoteStoragePlatform*/ eRemoteStoragePlatform );
				[DllImport( "libsteam_api.so" )] internal static extern UGCFileWriteStreamHandle_t /*(UGCFileWriteStreamHandle_t)*/ SteamAPI_ISteamRemoteStorage_FileWriteStreamOpen( IntPtr ISteamRemoteStorage, string /*const char **/ pchFile );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamRemoteStorage_FileWriteStreamWriteChunk( IntPtr ISteamRemoteStorage, ulong writeHandle, IntPtr /*const void **/ pvData, int /*int32*/ cubData );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamRemoteStorage_FileWriteStreamClose( IntPtr ISteamRemoteStorage, ulong writeHandle );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamRemoteStorage_FileWriteStreamCancel( IntPtr ISteamRemoteStorage, ulong writeHandle );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamRemoteStorage_FileExists( IntPtr ISteamRemoteStorage, string /*const char **/ pchFile );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamRemoteStorage_FilePersisted( IntPtr ISteamRemoteStorage, string /*const char **/ pchFile );
				[DllImport( "libsteam_api.so" )] internal static extern int /*int32*/ SteamAPI_ISteamRemoteStorage_GetFileSize( IntPtr ISteamRemoteStorage, string /*const char **/ pchFile );
				[DllImport( "libsteam_api.so" )] internal static extern long /*int64*/ SteamAPI_ISteamRemoteStorage_GetFileTimestamp( IntPtr ISteamRemoteStorage, string /*const char **/ pchFile );
				[DllImport( "libsteam_api.so" )] internal static extern RemoteStoragePlatform /*ERemoteStoragePlatform*/ SteamAPI_ISteamRemoteStorage_GetSyncPlatforms( IntPtr ISteamRemoteStorage, string /*const char **/ pchFile );
				[DllImport( "libsteam_api.so" )] internal static extern int /*int32*/ SteamAPI_ISteamRemoteStorage_GetFileCount( IntPtr ISteamRemoteStorage );
				[DllImport( "libsteam_api.so" )] internal static extern IntPtr SteamAPI_ISteamRemoteStorage_GetFileNameAndSize( IntPtr ISteamRemoteStorage, int /*int*/ iFile, out int /*int32 **/ pnFileSizeInBytes );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamRemoteStorage_GetQuota( IntPtr ISteamRemoteStorage, out ulong /*uint64 **/ pnTotalBytes, out ulong /*uint64 **/ puAvailableBytes );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamRemoteStorage_IsCloudEnabledForAccount( IntPtr ISteamRemoteStorage );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamRemoteStorage_IsCloudEnabledForApp( IntPtr ISteamRemoteStorage );
				[DllImport( "libsteam_api.so" )] internal static extern void /*void*/ SteamAPI_ISteamRemoteStorage_SetCloudEnabledForApp( IntPtr ISteamRemoteStorage, [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bEnabled );
				[DllImport( "libsteam_api.so" )] internal static extern SteamAPICall_t /*(SteamAPICall_t)*/ SteamAPI_ISteamRemoteStorage_UGCDownload( IntPtr ISteamRemoteStorage, ulong hContent, uint /*uint32*/ unPriority );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamRemoteStorage_GetUGCDownloadProgress( IntPtr ISteamRemoteStorage, ulong hContent, out int /*int32 **/ pnBytesDownloaded, out int /*int32 **/ pnBytesExpected );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamRemoteStorage_GetUGCDetails( IntPtr ISteamRemoteStorage, ulong hContent, ref uint pnAppID, System.Text.StringBuilder /*char ***/ ppchName, out int /*int32 **/ pnFileSizeInBytes, out ulong pSteamIDOwner );
				[DllImport( "libsteam_api.so" )] internal static extern int /*int32*/ SteamAPI_ISteamRemoteStorage_UGCRead( IntPtr ISteamRemoteStorage, ulong hContent, IntPtr /*void **/ pvData, int /*int32*/ cubDataToRead, uint /*uint32*/ cOffset, UGCReadAction /*EUGCReadAction*/ eAction );
				[DllImport( "libsteam_api.so" )] internal static extern int /*int32*/ SteamAPI_ISteamRemoteStorage_GetCachedUGCCount( IntPtr ISteamRemoteStorage );
				[DllImport( "libsteam_api.so" )] internal static extern UGCHandle_t /*(UGCHandle_t)*/ SteamAPI_ISteamRemoteStorage_GetCachedUGCHandle( IntPtr ISteamRemoteStorage, int /*int32*/ iCachedContent );
				[DllImport( "libsteam_api.so" )] internal static extern SteamAPICall_t /*(SteamAPICall_t)*/ SteamAPI_ISteamRemoteStorage_PublishWorkshopFile( IntPtr ISteamRemoteStorage, string /*const char **/ pchFile, string /*const char **/ pchPreviewFile, uint nConsumerAppId, string /*const char **/ pchTitle, string /*const char **/ pchDescription, RemoteStoragePublishedFileVisibility /*ERemoteStoragePublishedFileVisibility*/ eVisibility, ref SteamParamStringArray_t.PackSmall /*struct SteamParamStringArray_t **/ pTags, WorkshopFileType /*EWorkshopFileType*/ eWorkshopFileType );
				[DllImport( "libsteam_api.so" )] internal static extern PublishedFileUpdateHandle_t /*(PublishedFileUpdateHandle_t)*/ SteamAPI_ISteamRemoteStorage_CreatePublishedFileUpdateRequest( IntPtr ISteamRemoteStorage, ulong unPublishedFileId );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamRemoteStorage_UpdatePublishedFileFile( IntPtr ISteamRemoteStorage, ulong updateHandle, string /*const char **/ pchFile );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamRemoteStorage_UpdatePublishedFilePreviewFile( IntPtr ISteamRemoteStorage, ulong updateHandle, string /*const char **/ pchPreviewFile );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamRemoteStorage_UpdatePublishedFileTitle( IntPtr ISteamRemoteStorage, ulong updateHandle, string /*const char **/ pchTitle );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamRemoteStorage_UpdatePublishedFileDescription( IntPtr ISteamRemoteStorage, ulong updateHandle, string /*const char **/ pchDescription );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamRemoteStorage_UpdatePublishedFileVisibility( IntPtr ISteamRemoteStorage, ulong updateHandle, RemoteStoragePublishedFileVisibility /*ERemoteStoragePublishedFileVisibility*/ eVisibility );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamRemoteStorage_UpdatePublishedFileTags( IntPtr ISteamRemoteStorage, ulong updateHandle, ref SteamParamStringArray_t.PackSmall /*struct SteamParamStringArray_t **/ pTags );
				[DllImport( "libsteam_api.so" )] internal static extern SteamAPICall_t /*(SteamAPICall_t)*/ SteamAPI_ISteamRemoteStorage_CommitPublishedFileUpdate( IntPtr ISteamRemoteStorage, ulong updateHandle );
				[DllImport( "libsteam_api.so" )] internal static extern SteamAPICall_t /*(SteamAPICall_t)*/ SteamAPI_ISteamRemoteStorage_GetPublishedFileDetails( IntPtr ISteamRemoteStorage, ulong unPublishedFileId, uint /*uint32*/ unMaxSecondsOld );
				[DllImport( "libsteam_api.so" )] internal static extern SteamAPICall_t /*(SteamAPICall_t)*/ SteamAPI_ISteamRemoteStorage_DeletePublishedFile( IntPtr ISteamRemoteStorage, ulong unPublishedFileId );
				[DllImport( "libsteam_api.so" )] internal static extern SteamAPICall_t /*(SteamAPICall_t)*/ SteamAPI_ISteamRemoteStorage_EnumerateUserPublishedFiles( IntPtr ISteamRemoteStorage, uint /*uint32*/ unStartIndex );
				[DllImport( "libsteam_api.so" )] internal static extern SteamAPICall_t /*(SteamAPICall_t)*/ SteamAPI_ISteamRemoteStorage_SubscribePublishedFile( IntPtr ISteamRemoteStorage, ulong unPublishedFileId );
				[DllImport( "libsteam_api.so" )] internal static extern SteamAPICall_t /*(SteamAPICall_t)*/ SteamAPI_ISteamRemoteStorage_EnumerateUserSubscribedFiles( IntPtr ISteamRemoteStorage, uint /*uint32*/ unStartIndex );
				[DllImport( "libsteam_api.so" )] internal static extern SteamAPICall_t /*(SteamAPICall_t)*/ SteamAPI_ISteamRemoteStorage_UnsubscribePublishedFile( IntPtr ISteamRemoteStorage, ulong unPublishedFileId );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamRemoteStorage_UpdatePublishedFileSetChangeDescription( IntPtr ISteamRemoteStorage, ulong updateHandle, string /*const char **/ pchChangeDescription );
				[DllImport( "libsteam_api.so" )] internal static extern SteamAPICall_t /*(SteamAPICall_t)*/ SteamAPI_ISteamRemoteStorage_GetPublishedItemVoteDetails( IntPtr ISteamRemoteStorage, ulong unPublishedFileId );
				[DllImport( "libsteam_api.so" )] internal static extern SteamAPICall_t /*(SteamAPICall_t)*/ SteamAPI_ISteamRemoteStorage_UpdateUserPublishedItemVote( IntPtr ISteamRemoteStorage, ulong unPublishedFileId, [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bVoteUp );
				[DllImport( "libsteam_api.so" )] internal static extern SteamAPICall_t /*(SteamAPICall_t)*/ SteamAPI_ISteamRemoteStorage_GetUserPublishedItemVoteDetails( IntPtr ISteamRemoteStorage, ulong unPublishedFileId );
				[DllImport( "libsteam_api.so" )] internal static extern SteamAPICall_t /*(SteamAPICall_t)*/ SteamAPI_ISteamRemoteStorage_EnumerateUserSharedWorkshopFiles( IntPtr ISteamRemoteStorage, ulong steamId, uint /*uint32*/ unStartIndex, ref SteamParamStringArray_t.PackSmall /*struct SteamParamStringArray_t **/ pRequiredTags, ref SteamParamStringArray_t.PackSmall /*struct SteamParamStringArray_t **/ pExcludedTags );
				[DllImport( "libsteam_api.so" )] internal static extern SteamAPICall_t /*(SteamAPICall_t)*/ SteamAPI_ISteamRemoteStorage_PublishVideo( IntPtr ISteamRemoteStorage, WorkshopVideoProvider /*EWorkshopVideoProvider*/ eVideoProvider, string /*const char **/ pchVideoAccount, string /*const char **/ pchVideoIdentifier, string /*const char **/ pchPreviewFile, uint nConsumerAppId, string /*const char **/ pchTitle, string /*const char **/ pchDescription, RemoteStoragePublishedFileVisibility /*ERemoteStoragePublishedFileVisibility*/ eVisibility, ref SteamParamStringArray_t.PackSmall /*struct SteamParamStringArray_t **/ pTags );
				[DllImport( "libsteam_api.so" )] internal static extern SteamAPICall_t /*(SteamAPICall_t)*/ SteamAPI_ISteamRemoteStorage_SetUserPublishedFileAction( IntPtr ISteamRemoteStorage, ulong unPublishedFileId, WorkshopFileAction /*EWorkshopFileAction*/ eAction );
				[DllImport( "libsteam_api.so" )] internal static extern SteamAPICall_t /*(SteamAPICall_t)*/ SteamAPI_ISteamRemoteStorage_EnumeratePublishedFilesByUserAction( IntPtr ISteamRemoteStorage, WorkshopFileAction /*EWorkshopFileAction*/ eAction, uint /*uint32*/ unStartIndex );
				[DllImport( "libsteam_api.so" )] internal static extern SteamAPICall_t /*(SteamAPICall_t)*/ SteamAPI_ISteamRemoteStorage_EnumeratePublishedWorkshopFiles( IntPtr ISteamRemoteStorage, WorkshopEnumerationType /*EWorkshopEnumerationType*/ eEnumerationType, uint /*uint32*/ unStartIndex, uint /*uint32*/ unCount, uint /*uint32*/ unDays, ref SteamParamStringArray_t.PackSmall /*struct SteamParamStringArray_t **/ pTags, ref SteamParamStringArray_t.PackSmall /*struct SteamParamStringArray_t **/ pUserTags );
				[DllImport( "libsteam_api.so" )] internal static extern SteamAPICall_t /*(SteamAPICall_t)*/ SteamAPI_ISteamRemoteStorage_UGCDownloadToLocation( IntPtr ISteamRemoteStorage, ulong hContent, string /*const char **/ pchLocation, uint /*uint32*/ unPriority );
				
				//
				// ISteamUserStats 
				//
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamUserStats_RequestCurrentStats( IntPtr ISteamUserStats );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamUserStats_GetStat( IntPtr ISteamUserStats, string /*const char **/ pchName, out int /*int32 **/ pData );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamUserStats_GetStat0( IntPtr ISteamUserStats, string /*const char **/ pchName, out float /*float **/ pData );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamUserStats_SetStat( IntPtr ISteamUserStats, string /*const char **/ pchName, int /*int32*/ nData );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamUserStats_SetStat0( IntPtr ISteamUserStats, string /*const char **/ pchName, float /*float*/ fData );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamUserStats_UpdateAvgRateStat( IntPtr ISteamUserStats, string /*const char **/ pchName, float /*float*/ flCountThisSession, double /*double*/ dSessionLength );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamUserStats_GetAchievement( IntPtr ISteamUserStats, string /*const char **/ pchName, [MarshalAs(UnmanagedType.U1)] ref bool /*bool **/ pbAchieved );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamUserStats_SetAchievement( IntPtr ISteamUserStats, string /*const char **/ pchName );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamUserStats_ClearAchievement( IntPtr ISteamUserStats, string /*const char **/ pchName );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamUserStats_GetAchievementAndUnlockTime( IntPtr ISteamUserStats, string /*const char **/ pchName, [MarshalAs(UnmanagedType.U1)] ref bool /*bool **/ pbAchieved, out uint /*uint32 **/ punUnlockTime );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamUserStats_StoreStats( IntPtr ISteamUserStats );
				[DllImport( "libsteam_api.so" )] internal static extern int /*int*/ SteamAPI_ISteamUserStats_GetAchievementIcon( IntPtr ISteamUserStats, string /*const char **/ pchName );
				[DllImport( "libsteam_api.so" )] internal static extern IntPtr SteamAPI_ISteamUserStats_GetAchievementDisplayAttribute( IntPtr ISteamUserStats, string /*const char **/ pchName, string /*const char **/ pchKey );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamUserStats_IndicateAchievementProgress( IntPtr ISteamUserStats, string /*const char **/ pchName, uint /*uint32*/ nCurProgress, uint /*uint32*/ nMaxProgress );
				[DllImport( "libsteam_api.so" )] internal static extern uint /*uint32*/ SteamAPI_ISteamUserStats_GetNumAchievements( IntPtr ISteamUserStats );
				[DllImport( "libsteam_api.so" )] internal static extern IntPtr SteamAPI_ISteamUserStats_GetAchievementName( IntPtr ISteamUserStats, uint /*uint32*/ iAchievement );
				[DllImport( "libsteam_api.so" )] internal static extern SteamAPICall_t /*(SteamAPICall_t)*/ SteamAPI_ISteamUserStats_RequestUserStats( IntPtr ISteamUserStats, ulong steamIDUser );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamUserStats_GetUserStat( IntPtr ISteamUserStats, ulong steamIDUser, string /*const char **/ pchName, out int /*int32 **/ pData );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamUserStats_GetUserStat0( IntPtr ISteamUserStats, ulong steamIDUser, string /*const char **/ pchName, out float /*float **/ pData );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamUserStats_GetUserAchievement( IntPtr ISteamUserStats, ulong steamIDUser, string /*const char **/ pchName, [MarshalAs(UnmanagedType.U1)] ref bool /*bool **/ pbAchieved );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamUserStats_GetUserAchievementAndUnlockTime( IntPtr ISteamUserStats, ulong steamIDUser, string /*const char **/ pchName, [MarshalAs(UnmanagedType.U1)] ref bool /*bool **/ pbAchieved, out uint /*uint32 **/ punUnlockTime );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamUserStats_ResetAllStats( IntPtr ISteamUserStats, [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bAchievementsToo );
				[DllImport( "libsteam_api.so" )] internal static extern SteamAPICall_t /*(SteamAPICall_t)*/ SteamAPI_ISteamUserStats_FindOrCreateLeaderboard( IntPtr ISteamUserStats, string /*const char **/ pchLeaderboardName, LeaderboardSortMethod /*ELeaderboardSortMethod*/ eLeaderboardSortMethod, LeaderboardDisplayType /*ELeaderboardDisplayType*/ eLeaderboardDisplayType );
				[DllImport( "libsteam_api.so" )] internal static extern SteamAPICall_t /*(SteamAPICall_t)*/ SteamAPI_ISteamUserStats_FindLeaderboard( IntPtr ISteamUserStats, string /*const char **/ pchLeaderboardName );
				[DllImport( "libsteam_api.so" )] internal static extern IntPtr SteamAPI_ISteamUserStats_GetLeaderboardName( IntPtr ISteamUserStats, ulong hSteamLeaderboard );
				[DllImport( "libsteam_api.so" )] internal static extern int /*int*/ SteamAPI_ISteamUserStats_GetLeaderboardEntryCount( IntPtr ISteamUserStats, ulong hSteamLeaderboard );
				[DllImport( "libsteam_api.so" )] internal static extern LeaderboardSortMethod /*ELeaderboardSortMethod*/ SteamAPI_ISteamUserStats_GetLeaderboardSortMethod( IntPtr ISteamUserStats, ulong hSteamLeaderboard );
				[DllImport( "libsteam_api.so" )] internal static extern LeaderboardDisplayType /*ELeaderboardDisplayType*/ SteamAPI_ISteamUserStats_GetLeaderboardDisplayType( IntPtr ISteamUserStats, ulong hSteamLeaderboard );
				[DllImport( "libsteam_api.so" )] internal static extern SteamAPICall_t /*(SteamAPICall_t)*/ SteamAPI_ISteamUserStats_DownloadLeaderboardEntries( IntPtr ISteamUserStats, ulong hSteamLeaderboard, LeaderboardDataRequest /*ELeaderboardDataRequest*/ eLeaderboardDataRequest, int /*int*/ nRangeStart, int /*int*/ nRangeEnd );
				[DllImport( "libsteam_api.so" )] internal static extern SteamAPICall_t /*(SteamAPICall_t)*/ SteamAPI_ISteamUserStats_DownloadLeaderboardEntriesForUsers( IntPtr ISteamUserStats, ulong hSteamLeaderboard, IntPtr /*class CSteamID **/ prgUsers, int /*int*/ cUsers );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamUserStats_GetDownloadedLeaderboardEntry( IntPtr ISteamUserStats, ulong hSteamLeaderboardEntries, int /*int*/ index, ref LeaderboardEntry_t.PackSmall /*struct LeaderboardEntry_t **/ pLeaderboardEntry, IntPtr /*int32 **/ pDetails, int /*int*/ cDetailsMax );
				[DllImport( "libsteam_api.so" )] internal static extern SteamAPICall_t /*(SteamAPICall_t)*/ SteamAPI_ISteamUserStats_UploadLeaderboardScore( IntPtr ISteamUserStats, ulong hSteamLeaderboard, LeaderboardUploadScoreMethod /*ELeaderboardUploadScoreMethod*/ eLeaderboardUploadScoreMethod, int /*int32*/ nScore, int[] /*const int32 **/ pScoreDetails, int /*int*/ cScoreDetailsCount );
				[DllImport( "libsteam_api.so" )] internal static extern SteamAPICall_t /*(SteamAPICall_t)*/ SteamAPI_ISteamUserStats_AttachLeaderboardUGC( IntPtr ISteamUserStats, ulong hSteamLeaderboard, ulong hUGC );
				[DllImport( "libsteam_api.so" )] internal static extern SteamAPICall_t /*(SteamAPICall_t)*/ SteamAPI_ISteamUserStats_GetNumberOfCurrentPlayers( IntPtr ISteamUserStats );
				[DllImport( "libsteam_api.so" )] internal static extern SteamAPICall_t /*(SteamAPICall_t)*/ SteamAPI_ISteamUserStats_RequestGlobalAchievementPercentages( IntPtr ISteamUserStats );
				[DllImport( "libsteam_api.so" )] internal static extern int /*int*/ SteamAPI_ISteamUserStats_GetMostAchievedAchievementInfo( IntPtr ISteamUserStats, System.Text.StringBuilder /*char **/ pchName, uint /*uint32*/ unNameBufLen, out float /*float **/ pflPercent, [MarshalAs(UnmanagedType.U1)] ref bool /*bool **/ pbAchieved );
				[DllImport( "libsteam_api.so" )] internal static extern int /*int*/ SteamAPI_ISteamUserStats_GetNextMostAchievedAchievementInfo( IntPtr ISteamUserStats, int /*int*/ iIteratorPrevious, System.Text.StringBuilder /*char **/ pchName, uint /*uint32*/ unNameBufLen, out float /*float **/ pflPercent, [MarshalAs(UnmanagedType.U1)] ref bool /*bool **/ pbAchieved );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamUserStats_GetAchievementAchievedPercent( IntPtr ISteamUserStats, string /*const char **/ pchName, out float /*float **/ pflPercent );
				[DllImport( "libsteam_api.so" )] internal static extern SteamAPICall_t /*(SteamAPICall_t)*/ SteamAPI_ISteamUserStats_RequestGlobalStats( IntPtr ISteamUserStats, int /*int*/ nHistoryDays );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamUserStats_GetGlobalStat( IntPtr ISteamUserStats, string /*const char **/ pchStatName, out long /*int64 **/ pData );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamUserStats_GetGlobalStat0( IntPtr ISteamUserStats, string /*const char **/ pchStatName, out double /*double **/ pData );
				[DllImport( "libsteam_api.so" )] internal static extern int /*int32*/ SteamAPI_ISteamUserStats_GetGlobalStatHistory( IntPtr ISteamUserStats, string /*const char **/ pchStatName, out long /*int64 **/ pData, uint /*uint32*/ cubData );
				[DllImport( "libsteam_api.so" )] internal static extern int /*int32*/ SteamAPI_ISteamUserStats_GetGlobalStatHistory0( IntPtr ISteamUserStats, string /*const char **/ pchStatName, out double /*double **/ pData, uint /*uint32*/ cubData );
				
				//
				// ISteamApps 
				//
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamApps_BIsSubscribed( IntPtr ISteamApps );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamApps_BIsLowViolence( IntPtr ISteamApps );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamApps_BIsCybercafe( IntPtr ISteamApps );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamApps_BIsVACBanned( IntPtr ISteamApps );
				[DllImport( "libsteam_api.so" )] internal static extern IntPtr SteamAPI_ISteamApps_GetCurrentGameLanguage( IntPtr ISteamApps );
				[DllImport( "libsteam_api.so" )] internal static extern IntPtr SteamAPI_ISteamApps_GetAvailableGameLanguages( IntPtr ISteamApps );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamApps_BIsSubscribedApp( IntPtr ISteamApps, uint appID );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamApps_BIsDlcInstalled( IntPtr ISteamApps, uint appID );
				[DllImport( "libsteam_api.so" )] internal static extern uint /*uint32*/ SteamAPI_ISteamApps_GetEarliestPurchaseUnixTime( IntPtr ISteamApps, uint nAppID );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamApps_BIsSubscribedFromFreeWeekend( IntPtr ISteamApps );
				[DllImport( "libsteam_api.so" )] internal static extern int /*int*/ SteamAPI_ISteamApps_GetDLCCount( IntPtr ISteamApps );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamApps_BGetDLCDataByIndex( IntPtr ISteamApps, int /*int*/ iDLC, ref uint pAppID, [MarshalAs(UnmanagedType.U1)] ref bool /*bool **/ pbAvailable, System.Text.StringBuilder /*char **/ pchName, int /*int*/ cchNameBufferSize );
				[DllImport( "libsteam_api.so" )] internal static extern void /*void*/ SteamAPI_ISteamApps_InstallDLC( IntPtr ISteamApps, uint nAppID );
				[DllImport( "libsteam_api.so" )] internal static extern void /*void*/ SteamAPI_ISteamApps_UninstallDLC( IntPtr ISteamApps, uint nAppID );
				[DllImport( "libsteam_api.so" )] internal static extern void /*void*/ SteamAPI_ISteamApps_RequestAppProofOfPurchaseKey( IntPtr ISteamApps, uint nAppID );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamApps_GetCurrentBetaName( IntPtr ISteamApps, System.Text.StringBuilder /*char **/ pchName, int /*int*/ cchNameBufferSize );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamApps_MarkContentCorrupt( IntPtr ISteamApps, [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bMissingFilesOnly );
				[DllImport( "libsteam_api.so" )] internal static extern uint /*uint32*/ SteamAPI_ISteamApps_GetInstalledDepots( IntPtr ISteamApps, uint appID, IntPtr /*DepotId_t **/ pvecDepots, uint /*uint32*/ cMaxDepots );
				[DllImport( "libsteam_api.so" )] internal static extern uint /*uint32*/ SteamAPI_ISteamApps_GetAppInstallDir( IntPtr ISteamApps, uint appID, System.Text.StringBuilder /*char **/ pchFolder, uint /*uint32*/ cchFolderBufferSize );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamApps_BIsAppInstalled( IntPtr ISteamApps, uint appID );
				[DllImport( "libsteam_api.so" )] internal static extern CSteamID /*(class CSteamID)*/ SteamAPI_ISteamApps_GetAppOwner( IntPtr ISteamApps );
				[DllImport( "libsteam_api.so" )] internal static extern IntPtr SteamAPI_ISteamApps_GetLaunchQueryParam( IntPtr ISteamApps, string /*const char **/ pchKey );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamApps_GetDlcDownloadProgress( IntPtr ISteamApps, uint nAppID, out ulong /*uint64 **/ punBytesDownloaded, out ulong /*uint64 **/ punBytesTotal );
				[DllImport( "libsteam_api.so" )] internal static extern int /*int*/ SteamAPI_ISteamApps_GetAppBuildId( IntPtr ISteamApps );
				[DllImport( "libsteam_api.so" )] internal static extern void /*void*/ SteamAPI_ISteamApps_RequestAllProofOfPurchaseKeys( IntPtr ISteamApps );
				[DllImport( "libsteam_api.so" )] internal static extern SteamAPICall_t /*(SteamAPICall_t)*/ SteamAPI_ISteamApps_GetFileDetails( IntPtr ISteamApps, string /*const char **/ pszFileName );
				[DllImport( "libsteam_api.so" )] internal static extern int /*int*/ SteamAPI_ISteamApps_GetLaunchCommandLine( IntPtr ISteamApps, System.Text.StringBuilder /*char **/ pszCommandLine, int /*int*/ cubCommandLine );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamApps_BIsSubscribedFromFamilySharing( IntPtr ISteamApps );
				
				//
				// ISteamNetworking 
				//
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamNetworking_SendP2PPacket( IntPtr ISteamNetworking, ulong steamIDRemote, IntPtr /*const void **/ pubData, uint /*uint32*/ cubData, P2PSend /*EP2PSend*/ eP2PSendType, int /*int*/ nChannel );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamNetworking_IsP2PPacketAvailable( IntPtr ISteamNetworking, out uint /*uint32 **/ pcubMsgSize, int /*int*/ nChannel );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamNetworking_ReadP2PPacket( IntPtr ISteamNetworking, IntPtr /*void **/ pubDest, uint /*uint32*/ cubDest, out uint /*uint32 **/ pcubMsgSize, out ulong psteamIDRemote, int /*int*/ nChannel );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamNetworking_AcceptP2PSessionWithUser( IntPtr ISteamNetworking, ulong steamIDRemote );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamNetworking_CloseP2PSessionWithUser( IntPtr ISteamNetworking, ulong steamIDRemote );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamNetworking_CloseP2PChannelWithUser( IntPtr ISteamNetworking, ulong steamIDRemote, int /*int*/ nChannel );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamNetworking_GetP2PSessionState( IntPtr ISteamNetworking, ulong steamIDRemote, ref P2PSessionState_t.PackSmall /*struct P2PSessionState_t **/ pConnectionState );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamNetworking_AllowP2PPacketRelay( IntPtr ISteamNetworking, [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bAllow );
				[DllImport( "libsteam_api.so" )] internal static extern SNetListenSocket_t /*(SNetListenSocket_t)*/ SteamAPI_ISteamNetworking_CreateListenSocket( IntPtr ISteamNetworking, int /*int*/ nVirtualP2PPort, uint /*uint32*/ nIP, ushort /*uint16*/ nPort, [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bAllowUseOfPacketRelay );
				[DllImport( "libsteam_api.so" )] internal static extern SNetSocket_t /*(SNetSocket_t)*/ SteamAPI_ISteamNetworking_CreateP2PConnectionSocket( IntPtr ISteamNetworking, ulong steamIDTarget, int /*int*/ nVirtualPort, int /*int*/ nTimeoutSec, [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bAllowUseOfPacketRelay );
				[DllImport( "libsteam_api.so" )] internal static extern SNetSocket_t /*(SNetSocket_t)*/ SteamAPI_ISteamNetworking_CreateConnectionSocket( IntPtr ISteamNetworking, uint /*uint32*/ nIP, ushort /*uint16*/ nPort, int /*int*/ nTimeoutSec );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamNetworking_DestroySocket( IntPtr ISteamNetworking, uint hSocket, [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bNotifyRemoteEnd );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamNetworking_DestroyListenSocket( IntPtr ISteamNetworking, uint hSocket, [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bNotifyRemoteEnd );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamNetworking_SendDataOnSocket( IntPtr ISteamNetworking, uint hSocket, IntPtr /*void **/ pubData, uint /*uint32*/ cubData, [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bReliable );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamNetworking_IsDataAvailableOnSocket( IntPtr ISteamNetworking, uint hSocket, out uint /*uint32 **/ pcubMsgSize );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamNetworking_RetrieveDataFromSocket( IntPtr ISteamNetworking, uint hSocket, IntPtr /*void **/ pubDest, uint /*uint32*/ cubDest, out uint /*uint32 **/ pcubMsgSize );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamNetworking_IsDataAvailable( IntPtr ISteamNetworking, uint hListenSocket, out uint /*uint32 **/ pcubMsgSize, ref uint phSocket );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamNetworking_RetrieveData( IntPtr ISteamNetworking, uint hListenSocket, IntPtr /*void **/ pubDest, uint /*uint32*/ cubDest, out uint /*uint32 **/ pcubMsgSize, ref uint phSocket );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamNetworking_GetSocketInfo( IntPtr ISteamNetworking, uint hSocket, out ulong pSteamIDRemote, IntPtr /*int **/ peSocketStatus, out uint /*uint32 **/ punIPRemote, out ushort /*uint16 **/ punPortRemote );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamNetworking_GetListenSocketInfo( IntPtr ISteamNetworking, uint hListenSocket, out uint /*uint32 **/ pnIP, out ushort /*uint16 **/ pnPort );
				[DllImport( "libsteam_api.so" )] internal static extern SNetSocketConnectionType /*ESNetSocketConnectionType*/ SteamAPI_ISteamNetworking_GetSocketConnectionType( IntPtr ISteamNetworking, uint hSocket );
				[DllImport( "libsteam_api.so" )] internal static extern int /*int*/ SteamAPI_ISteamNetworking_GetMaxPacketSize( IntPtr ISteamNetworking, uint hSocket );
				
				//
				// ISteamScreenshots 
				//
				[DllImport( "libsteam_api.so" )] internal static extern ScreenshotHandle /*(ScreenshotHandle)*/ SteamAPI_ISteamScreenshots_WriteScreenshot( IntPtr ISteamScreenshots, IntPtr /*void **/ pubRGB, uint /*uint32*/ cubRGB, int /*int*/ nWidth, int /*int*/ nHeight );
				[DllImport( "libsteam_api.so" )] internal static extern ScreenshotHandle /*(ScreenshotHandle)*/ SteamAPI_ISteamScreenshots_AddScreenshotToLibrary( IntPtr ISteamScreenshots, string /*const char **/ pchFilename, string /*const char **/ pchThumbnailFilename, int /*int*/ nWidth, int /*int*/ nHeight );
				[DllImport( "libsteam_api.so" )] internal static extern void /*void*/ SteamAPI_ISteamScreenshots_TriggerScreenshot( IntPtr ISteamScreenshots );
				[DllImport( "libsteam_api.so" )] internal static extern void /*void*/ SteamAPI_ISteamScreenshots_HookScreenshots( IntPtr ISteamScreenshots, [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bHook );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamScreenshots_SetLocation( IntPtr ISteamScreenshots, uint hScreenshot, string /*const char **/ pchLocation );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamScreenshots_TagUser( IntPtr ISteamScreenshots, uint hScreenshot, ulong steamID );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamScreenshots_TagPublishedFile( IntPtr ISteamScreenshots, uint hScreenshot, ulong unPublishedFileID );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamScreenshots_IsScreenshotsHooked( IntPtr ISteamScreenshots );
				[DllImport( "libsteam_api.so" )] internal static extern ScreenshotHandle /*(ScreenshotHandle)*/ SteamAPI_ISteamScreenshots_AddVRScreenshotToLibrary( IntPtr ISteamScreenshots, VRScreenshotType /*EVRScreenshotType*/ eType, string /*const char **/ pchFilename, string /*const char **/ pchVRFilename );
				
				//
				// ISteamMusic 
				//
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamMusic_BIsEnabled( IntPtr ISteamMusic );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamMusic_BIsPlaying( IntPtr ISteamMusic );
				[DllImport( "libsteam_api.so" )] internal static extern AudioPlayback_Status /*AudioPlayback_Status*/ SteamAPI_ISteamMusic_GetPlaybackStatus( IntPtr ISteamMusic );
				[DllImport( "libsteam_api.so" )] internal static extern void /*void*/ SteamAPI_ISteamMusic_Play( IntPtr ISteamMusic );
				[DllImport( "libsteam_api.so" )] internal static extern void /*void*/ SteamAPI_ISteamMusic_Pause( IntPtr ISteamMusic );
				[DllImport( "libsteam_api.so" )] internal static extern void /*void*/ SteamAPI_ISteamMusic_PlayPrevious( IntPtr ISteamMusic );
				[DllImport( "libsteam_api.so" )] internal static extern void /*void*/ SteamAPI_ISteamMusic_PlayNext( IntPtr ISteamMusic );
				[DllImport( "libsteam_api.so" )] internal static extern void /*void*/ SteamAPI_ISteamMusic_SetVolume( IntPtr ISteamMusic, float /*float*/ flVolume );
				[DllImport( "libsteam_api.so" )] internal static extern float /*float*/ SteamAPI_ISteamMusic_GetVolume( IntPtr ISteamMusic );
				
				//
				// ISteamMusicRemote 
				//
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamMusicRemote_RegisterSteamMusicRemote( IntPtr ISteamMusicRemote, string /*const char **/ pchName );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamMusicRemote_DeregisterSteamMusicRemote( IntPtr ISteamMusicRemote );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamMusicRemote_BIsCurrentMusicRemote( IntPtr ISteamMusicRemote );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamMusicRemote_BActivationSuccess( IntPtr ISteamMusicRemote, [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bValue );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamMusicRemote_SetDisplayName( IntPtr ISteamMusicRemote, string /*const char **/ pchDisplayName );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamMusicRemote_SetPNGIcon_64x64( IntPtr ISteamMusicRemote, IntPtr /*void **/ pvBuffer, uint /*uint32*/ cbBufferLength );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamMusicRemote_EnablePlayPrevious( IntPtr ISteamMusicRemote, [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bValue );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamMusicRemote_EnablePlayNext( IntPtr ISteamMusicRemote, [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bValue );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamMusicRemote_EnableShuffled( IntPtr ISteamMusicRemote, [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bValue );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamMusicRemote_EnableLooped( IntPtr ISteamMusicRemote, [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bValue );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamMusicRemote_EnableQueue( IntPtr ISteamMusicRemote, [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bValue );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamMusicRemote_EnablePlaylists( IntPtr ISteamMusicRemote, [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bValue );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamMusicRemote_UpdatePlaybackStatus( IntPtr ISteamMusicRemote, AudioPlayback_Status /*AudioPlayback_Status*/ nStatus );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamMusicRemote_UpdateShuffled( IntPtr ISteamMusicRemote, [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bValue );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamMusicRemote_UpdateLooped( IntPtr ISteamMusicRemote, [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bValue );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamMusicRemote_UpdateVolume( IntPtr ISteamMusicRemote, float /*float*/ flValue );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamMusicRemote_CurrentEntryWillChange( IntPtr ISteamMusicRemote );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamMusicRemote_CurrentEntryIsAvailable( IntPtr ISteamMusicRemote, [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bAvailable );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamMusicRemote_UpdateCurrentEntryText( IntPtr ISteamMusicRemote, string /*const char **/ pchText );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamMusicRemote_UpdateCurrentEntryElapsedSeconds( IntPtr ISteamMusicRemote, int /*int*/ nValue );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamMusicRemote_UpdateCurrentEntryCoverArt( IntPtr ISteamMusicRemote, IntPtr /*void **/ pvBuffer, uint /*uint32*/ cbBufferLength );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamMusicRemote_CurrentEntryDidChange( IntPtr ISteamMusicRemote );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamMusicRemote_QueueWillChange( IntPtr ISteamMusicRemote );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamMusicRemote_ResetQueueEntries( IntPtr ISteamMusicRemote );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamMusicRemote_SetQueueEntry( IntPtr ISteamMusicRemote, int /*int*/ nID, int /*int*/ nPosition, string /*const char **/ pchEntryText );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamMusicRemote_SetCurrentQueueEntry( IntPtr ISteamMusicRemote, int /*int*/ nID );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamMusicRemote_QueueDidChange( IntPtr ISteamMusicRemote );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamMusicRemote_PlaylistWillChange( IntPtr ISteamMusicRemote );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamMusicRemote_ResetPlaylistEntries( IntPtr ISteamMusicRemote );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamMusicRemote_SetPlaylistEntry( IntPtr ISteamMusicRemote, int /*int*/ nID, int /*int*/ nPosition, string /*const char **/ pchEntryText );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamMusicRemote_SetCurrentPlaylistEntry( IntPtr ISteamMusicRemote, int /*int*/ nID );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamMusicRemote_PlaylistDidChange( IntPtr ISteamMusicRemote );
				
				//
				// ISteamHTTP 
				//
				[DllImport( "libsteam_api.so" )] internal static extern HTTPRequestHandle /*(HTTPRequestHandle)*/ SteamAPI_ISteamHTTP_CreateHTTPRequest( IntPtr ISteamHTTP, HTTPMethod /*EHTTPMethod*/ eHTTPRequestMethod, string /*const char **/ pchAbsoluteURL );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamHTTP_SetHTTPRequestContextValue( IntPtr ISteamHTTP, uint hRequest, ulong /*uint64*/ ulContextValue );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamHTTP_SetHTTPRequestNetworkActivityTimeout( IntPtr ISteamHTTP, uint hRequest, uint /*uint32*/ unTimeoutSeconds );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamHTTP_SetHTTPRequestHeaderValue( IntPtr ISteamHTTP, uint hRequest, string /*const char **/ pchHeaderName, string /*const char **/ pchHeaderValue );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamHTTP_SetHTTPRequestGetOrPostParameter( IntPtr ISteamHTTP, uint hRequest, string /*const char **/ pchParamName, string /*const char **/ pchParamValue );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamHTTP_SendHTTPRequest( IntPtr ISteamHTTP, uint hRequest, ref ulong pCallHandle );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamHTTP_SendHTTPRequestAndStreamResponse( IntPtr ISteamHTTP, uint hRequest, ref ulong pCallHandle );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamHTTP_DeferHTTPRequest( IntPtr ISteamHTTP, uint hRequest );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamHTTP_PrioritizeHTTPRequest( IntPtr ISteamHTTP, uint hRequest );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamHTTP_GetHTTPResponseHeaderSize( IntPtr ISteamHTTP, uint hRequest, string /*const char **/ pchHeaderName, out uint /*uint32 **/ unResponseHeaderSize );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamHTTP_GetHTTPResponseHeaderValue( IntPtr ISteamHTTP, uint hRequest, string /*const char **/ pchHeaderName, out byte /*uint8 **/ pHeaderValueBuffer, uint /*uint32*/ unBufferSize );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamHTTP_GetHTTPResponseBodySize( IntPtr ISteamHTTP, uint hRequest, out uint /*uint32 **/ unBodySize );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamHTTP_GetHTTPResponseBodyData( IntPtr ISteamHTTP, uint hRequest, out byte /*uint8 **/ pBodyDataBuffer, uint /*uint32*/ unBufferSize );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamHTTP_GetHTTPStreamingResponseBodyData( IntPtr ISteamHTTP, uint hRequest, uint /*uint32*/ cOffset, out byte /*uint8 **/ pBodyDataBuffer, uint /*uint32*/ unBufferSize );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamHTTP_ReleaseHTTPRequest( IntPtr ISteamHTTP, uint hRequest );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamHTTP_GetHTTPDownloadProgressPct( IntPtr ISteamHTTP, uint hRequest, out float /*float **/ pflPercentOut );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamHTTP_SetHTTPRequestRawPostBody( IntPtr ISteamHTTP, uint hRequest, string /*const char **/ pchContentType, out byte /*uint8 **/ pubBody, uint /*uint32*/ unBodyLen );
				[DllImport( "libsteam_api.so" )] internal static extern HTTPCookieContainerHandle /*(HTTPCookieContainerHandle)*/ SteamAPI_ISteamHTTP_CreateCookieContainer( IntPtr ISteamHTTP, [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bAllowResponsesToModify );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamHTTP_ReleaseCookieContainer( IntPtr ISteamHTTP, uint hCookieContainer );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamHTTP_SetCookie( IntPtr ISteamHTTP, uint hCookieContainer, string /*const char **/ pchHost, string /*const char **/ pchUrl, string /*const char **/ pchCookie );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamHTTP_SetHTTPRequestCookieContainer( IntPtr ISteamHTTP, uint hRequest, uint hCookieContainer );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamHTTP_SetHTTPRequestUserAgentInfo( IntPtr ISteamHTTP, uint hRequest, string /*const char **/ pchUserAgentInfo );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamHTTP_SetHTTPRequestRequiresVerifiedCertificate( IntPtr ISteamHTTP, uint hRequest, [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bRequireVerifiedCertificate );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamHTTP_SetHTTPRequestAbsoluteTimeoutMS( IntPtr ISteamHTTP, uint hRequest, uint /*uint32*/ unMilliseconds );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamHTTP_GetHTTPRequestWasTimedOut( IntPtr ISteamHTTP, uint hRequest, [MarshalAs(UnmanagedType.U1)] ref bool /*bool **/ pbWasTimedOut );
				
				//
				// ISteamInput 
				//
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamInput_Init( IntPtr ISteamInput );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamInput_Shutdown( IntPtr ISteamInput );
				[DllImport( "libsteam_api.so" )] internal static extern void /*void*/ SteamAPI_ISteamInput_RunFrame( IntPtr ISteamInput );
				[DllImport( "libsteam_api.so" )] internal static extern int /*int*/ SteamAPI_ISteamInput_GetConnectedControllers( IntPtr ISteamInput, IntPtr /*InputHandle_t **/ handlesOut );
				[DllImport( "libsteam_api.so" )] internal static extern InputActionSetHandle_t /*(InputActionSetHandle_t)*/ SteamAPI_ISteamInput_GetActionSetHandle( IntPtr ISteamInput, string /*const char **/ pszActionSetName );
				[DllImport( "libsteam_api.so" )] internal static extern void /*void*/ SteamAPI_ISteamInput_ActivateActionSet( IntPtr ISteamInput, ulong inputHandle, ulong actionSetHandle );
				[DllImport( "libsteam_api.so" )] internal static extern InputActionSetHandle_t /*(InputActionSetHandle_t)*/ SteamAPI_ISteamInput_GetCurrentActionSet( IntPtr ISteamInput, ulong inputHandle );
				[DllImport( "libsteam_api.so" )] internal static extern void /*void*/ SteamAPI_ISteamInput_ActivateActionSetLayer( IntPtr ISteamInput, ulong inputHandle, ulong actionSetLayerHandle );
				[DllImport( "libsteam_api.so" )] internal static extern void /*void*/ SteamAPI_ISteamInput_DeactivateActionSetLayer( IntPtr ISteamInput, ulong inputHandle, ulong actionSetLayerHandle );
				[DllImport( "libsteam_api.so" )] internal static extern void /*void*/ SteamAPI_ISteamInput_DeactivateAllActionSetLayers( IntPtr ISteamInput, ulong inputHandle );
				[DllImport( "libsteam_api.so" )] internal static extern int /*int*/ SteamAPI_ISteamInput_GetActiveActionSetLayers( IntPtr ISteamInput, ulong inputHandle, IntPtr /*InputActionSetHandle_t **/ handlesOut );
				[DllImport( "libsteam_api.so" )] internal static extern InputDigitalActionHandle_t /*(InputDigitalActionHandle_t)*/ SteamAPI_ISteamInput_GetDigitalActionHandle( IntPtr ISteamInput, string /*const char **/ pszActionName );
				[DllImport( "libsteam_api.so" )] internal static extern InputDigitalActionData_t /*struct InputDigitalActionData_t*/ SteamAPI_ISteamInput_GetDigitalActionData( IntPtr ISteamInput, ulong inputHandle, ulong digitalActionHandle );
				[DllImport( "libsteam_api.so" )] internal static extern int /*int*/ SteamAPI_ISteamInput_GetDigitalActionOrigins( IntPtr ISteamInput, ulong inputHandle, ulong actionSetHandle, ulong digitalActionHandle, out InputActionOrigin /*EInputActionOrigin **/ originsOut );
				[DllImport( "libsteam_api.so" )] internal static extern InputAnalogActionHandle_t /*(InputAnalogActionHandle_t)*/ SteamAPI_ISteamInput_GetAnalogActionHandle( IntPtr ISteamInput, string /*const char **/ pszActionName );
				[DllImport( "libsteam_api.so" )] internal static extern InputAnalogActionData_t /*struct InputAnalogActionData_t*/ SteamAPI_ISteamInput_GetAnalogActionData( IntPtr ISteamInput, ulong inputHandle, ulong analogActionHandle );
				[DllImport( "libsteam_api.so" )] internal static extern int /*int*/ SteamAPI_ISteamInput_GetAnalogActionOrigins( IntPtr ISteamInput, ulong inputHandle, ulong actionSetHandle, ulong analogActionHandle, out InputActionOrigin /*EInputActionOrigin **/ originsOut );
				[DllImport( "libsteam_api.so" )] internal static extern IntPtr SteamAPI_ISteamInput_GetGlyphForActionOrigin( IntPtr ISteamInput, InputActionOrigin /*EInputActionOrigin*/ eOrigin );
				[DllImport( "libsteam_api.so" )] internal static extern IntPtr SteamAPI_ISteamInput_GetStringForActionOrigin( IntPtr ISteamInput, InputActionOrigin /*EInputActionOrigin*/ eOrigin );
				[DllImport( "libsteam_api.so" )] internal static extern void /*void*/ SteamAPI_ISteamInput_StopAnalogActionMomentum( IntPtr ISteamInput, ulong inputHandle, ulong eAction );
				[DllImport( "libsteam_api.so" )] internal static extern InputMotionData_t /*struct InputMotionData_t*/ SteamAPI_ISteamInput_GetMotionData( IntPtr ISteamInput, ulong inputHandle );
				[DllImport( "libsteam_api.so" )] internal static extern void /*void*/ SteamAPI_ISteamInput_TriggerVibration( IntPtr ISteamInput, ulong inputHandle, ushort /*unsigned short*/ usLeftSpeed, ushort /*unsigned short*/ usRightSpeed );
				[DllImport( "libsteam_api.so" )] internal static extern void /*void*/ SteamAPI_ISteamInput_SetLEDColor( IntPtr ISteamInput, ulong inputHandle, byte /*uint8*/ nColorR, byte /*uint8*/ nColorG, byte /*uint8*/ nColorB, uint /*unsigned int*/ nFlags );
				[DllImport( "libsteam_api.so" )] internal static extern void /*void*/ SteamAPI_ISteamInput_TriggerHapticPulse( IntPtr ISteamInput, ulong inputHandle, SteamControllerPad /*ESteamControllerPad*/ eTargetPad, ushort /*unsigned short*/ usDurationMicroSec );
				[DllImport( "libsteam_api.so" )] internal static extern void /*void*/ SteamAPI_ISteamInput_TriggerRepeatedHapticPulse( IntPtr ISteamInput, ulong inputHandle, SteamControllerPad /*ESteamControllerPad*/ eTargetPad, ushort /*unsigned short*/ usDurationMicroSec, ushort /*unsigned short*/ usOffMicroSec, ushort /*unsigned short*/ unRepeat, uint /*unsigned int*/ nFlags );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamInput_ShowBindingPanel( IntPtr ISteamInput, ulong inputHandle );
				[DllImport( "libsteam_api.so" )] internal static extern SteamInputType /*ESteamInputType*/ SteamAPI_ISteamInput_GetInputTypeForHandle( IntPtr ISteamInput, ulong inputHandle );
				[DllImport( "libsteam_api.so" )] internal static extern InputHandle_t /*(InputHandle_t)*/ SteamAPI_ISteamInput_GetControllerForGamepadIndex( IntPtr ISteamInput, int /*int*/ nIndex );
				[DllImport( "libsteam_api.so" )] internal static extern int /*int*/ SteamAPI_ISteamInput_GetGamepadIndexForController( IntPtr ISteamInput, ulong ulinputHandle );
				[DllImport( "libsteam_api.so" )] internal static extern IntPtr SteamAPI_ISteamInput_GetStringForXboxOrigin( IntPtr ISteamInput, XboxOrigin /*EXboxOrigin*/ eOrigin );
				[DllImport( "libsteam_api.so" )] internal static extern IntPtr SteamAPI_ISteamInput_GetGlyphForXboxOrigin( IntPtr ISteamInput, XboxOrigin /*EXboxOrigin*/ eOrigin );
				[DllImport( "libsteam_api.so" )] internal static extern InputActionOrigin /*EInputActionOrigin*/ SteamAPI_ISteamInput_GetActionOriginFromXboxOrigin( IntPtr ISteamInput, ulong inputHandle, XboxOrigin /*EXboxOrigin*/ eOrigin );
				[DllImport( "libsteam_api.so" )] internal static extern InputActionOrigin /*EInputActionOrigin*/ SteamAPI_ISteamInput_TranslateActionOrigin( IntPtr ISteamInput, SteamInputType /*ESteamInputType*/ eDestinationInputType, InputActionOrigin /*EInputActionOrigin*/ eSourceOrigin );
				
				//
				// ISteamController 
				//
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamController_Init( IntPtr ISteamController );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamController_Shutdown( IntPtr ISteamController );
				[DllImport( "libsteam_api.so" )] internal static extern void /*void*/ SteamAPI_ISteamController_RunFrame( IntPtr ISteamController );
				[DllImport( "libsteam_api.so" )] internal static extern int /*int*/ SteamAPI_ISteamController_GetConnectedControllers( IntPtr ISteamController, IntPtr /*ControllerHandle_t **/ handlesOut );
				[DllImport( "libsteam_api.so" )] internal static extern ControllerActionSetHandle_t /*(ControllerActionSetHandle_t)*/ SteamAPI_ISteamController_GetActionSetHandle( IntPtr ISteamController, string /*const char **/ pszActionSetName );
				[DllImport( "libsteam_api.so" )] internal static extern void /*void*/ SteamAPI_ISteamController_ActivateActionSet( IntPtr ISteamController, ulong controllerHandle, ulong actionSetHandle );
				[DllImport( "libsteam_api.so" )] internal static extern ControllerActionSetHandle_t /*(ControllerActionSetHandle_t)*/ SteamAPI_ISteamController_GetCurrentActionSet( IntPtr ISteamController, ulong controllerHandle );
				[DllImport( "libsteam_api.so" )] internal static extern void /*void*/ SteamAPI_ISteamController_ActivateActionSetLayer( IntPtr ISteamController, ulong controllerHandle, ulong actionSetLayerHandle );
				[DllImport( "libsteam_api.so" )] internal static extern void /*void*/ SteamAPI_ISteamController_DeactivateActionSetLayer( IntPtr ISteamController, ulong controllerHandle, ulong actionSetLayerHandle );
				[DllImport( "libsteam_api.so" )] internal static extern void /*void*/ SteamAPI_ISteamController_DeactivateAllActionSetLayers( IntPtr ISteamController, ulong controllerHandle );
				[DllImport( "libsteam_api.so" )] internal static extern int /*int*/ SteamAPI_ISteamController_GetActiveActionSetLayers( IntPtr ISteamController, ulong controllerHandle, IntPtr /*ControllerActionSetHandle_t **/ handlesOut );
				[DllImport( "libsteam_api.so" )] internal static extern ControllerDigitalActionHandle_t /*(ControllerDigitalActionHandle_t)*/ SteamAPI_ISteamController_GetDigitalActionHandle( IntPtr ISteamController, string /*const char **/ pszActionName );
				[DllImport( "libsteam_api.so" )] internal static extern InputDigitalActionData_t /*struct InputDigitalActionData_t*/ SteamAPI_ISteamController_GetDigitalActionData( IntPtr ISteamController, ulong controllerHandle, ulong digitalActionHandle );
				[DllImport( "libsteam_api.so" )] internal static extern int /*int*/ SteamAPI_ISteamController_GetDigitalActionOrigins( IntPtr ISteamController, ulong controllerHandle, ulong actionSetHandle, ulong digitalActionHandle, out ControllerActionOrigin /*EControllerActionOrigin **/ originsOut );
				[DllImport( "libsteam_api.so" )] internal static extern ControllerAnalogActionHandle_t /*(ControllerAnalogActionHandle_t)*/ SteamAPI_ISteamController_GetAnalogActionHandle( IntPtr ISteamController, string /*const char **/ pszActionName );
				[DllImport( "libsteam_api.so" )] internal static extern InputAnalogActionData_t /*struct InputAnalogActionData_t*/ SteamAPI_ISteamController_GetAnalogActionData( IntPtr ISteamController, ulong controllerHandle, ulong analogActionHandle );
				[DllImport( "libsteam_api.so" )] internal static extern int /*int*/ SteamAPI_ISteamController_GetAnalogActionOrigins( IntPtr ISteamController, ulong controllerHandle, ulong actionSetHandle, ulong analogActionHandle, out ControllerActionOrigin /*EControllerActionOrigin **/ originsOut );
				[DllImport( "libsteam_api.so" )] internal static extern IntPtr SteamAPI_ISteamController_GetGlyphForActionOrigin( IntPtr ISteamController, ControllerActionOrigin /*EControllerActionOrigin*/ eOrigin );
				[DllImport( "libsteam_api.so" )] internal static extern IntPtr SteamAPI_ISteamController_GetStringForActionOrigin( IntPtr ISteamController, ControllerActionOrigin /*EControllerActionOrigin*/ eOrigin );
				[DllImport( "libsteam_api.so" )] internal static extern void /*void*/ SteamAPI_ISteamController_StopAnalogActionMomentum( IntPtr ISteamController, ulong controllerHandle, ulong eAction );
				[DllImport( "libsteam_api.so" )] internal static extern InputMotionData_t /*struct InputMotionData_t*/ SteamAPI_ISteamController_GetMotionData( IntPtr ISteamController, ulong controllerHandle );
				[DllImport( "libsteam_api.so" )] internal static extern void /*void*/ SteamAPI_ISteamController_TriggerHapticPulse( IntPtr ISteamController, ulong controllerHandle, SteamControllerPad /*ESteamControllerPad*/ eTargetPad, ushort /*unsigned short*/ usDurationMicroSec );
				[DllImport( "libsteam_api.so" )] internal static extern void /*void*/ SteamAPI_ISteamController_TriggerRepeatedHapticPulse( IntPtr ISteamController, ulong controllerHandle, SteamControllerPad /*ESteamControllerPad*/ eTargetPad, ushort /*unsigned short*/ usDurationMicroSec, ushort /*unsigned short*/ usOffMicroSec, ushort /*unsigned short*/ unRepeat, uint /*unsigned int*/ nFlags );
				[DllImport( "libsteam_api.so" )] internal static extern void /*void*/ SteamAPI_ISteamController_TriggerVibration( IntPtr ISteamController, ulong controllerHandle, ushort /*unsigned short*/ usLeftSpeed, ushort /*unsigned short*/ usRightSpeed );
				[DllImport( "libsteam_api.so" )] internal static extern void /*void*/ SteamAPI_ISteamController_SetLEDColor( IntPtr ISteamController, ulong controllerHandle, byte /*uint8*/ nColorR, byte /*uint8*/ nColorG, byte /*uint8*/ nColorB, uint /*unsigned int*/ nFlags );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamController_ShowBindingPanel( IntPtr ISteamController, ulong controllerHandle );
				[DllImport( "libsteam_api.so" )] internal static extern SteamInputType /*ESteamInputType*/ SteamAPI_ISteamController_GetInputTypeForHandle( IntPtr ISteamController, ulong controllerHandle );
				[DllImport( "libsteam_api.so" )] internal static extern ControllerHandle_t /*(ControllerHandle_t)*/ SteamAPI_ISteamController_GetControllerForGamepadIndex( IntPtr ISteamController, int /*int*/ nIndex );
				[DllImport( "libsteam_api.so" )] internal static extern int /*int*/ SteamAPI_ISteamController_GetGamepadIndexForController( IntPtr ISteamController, ulong ulControllerHandle );
				[DllImport( "libsteam_api.so" )] internal static extern IntPtr SteamAPI_ISteamController_GetStringForXboxOrigin( IntPtr ISteamController, XboxOrigin /*EXboxOrigin*/ eOrigin );
				[DllImport( "libsteam_api.so" )] internal static extern IntPtr SteamAPI_ISteamController_GetGlyphForXboxOrigin( IntPtr ISteamController, XboxOrigin /*EXboxOrigin*/ eOrigin );
				[DllImport( "libsteam_api.so" )] internal static extern ControllerActionOrigin /*EControllerActionOrigin*/ SteamAPI_ISteamController_GetActionOriginFromXboxOrigin( IntPtr ISteamController, ulong controllerHandle, XboxOrigin /*EXboxOrigin*/ eOrigin );
				[DllImport( "libsteam_api.so" )] internal static extern ControllerActionOrigin /*EControllerActionOrigin*/ SteamAPI_ISteamController_TranslateActionOrigin( IntPtr ISteamController, SteamInputType /*ESteamInputType*/ eDestinationInputType, ControllerActionOrigin /*EControllerActionOrigin*/ eSourceOrigin );
				
				//
				// ISteamUGC 
				//
				[DllImport( "libsteam_api.so" )] internal static extern UGCQueryHandle_t /*(UGCQueryHandle_t)*/ SteamAPI_ISteamUGC_CreateQueryUserUGCRequest( IntPtr ISteamUGC, uint unAccountID, UserUGCList /*EUserUGCList*/ eListType, UGCMatchingUGCType /*EUGCMatchingUGCType*/ eMatchingUGCType, UserUGCListSortOrder /*EUserUGCListSortOrder*/ eSortOrder, uint nCreatorAppID, uint nConsumerAppID, uint /*uint32*/ unPage );
				[DllImport( "libsteam_api.so" )] internal static extern UGCQueryHandle_t /*(UGCQueryHandle_t)*/ SteamAPI_ISteamUGC_CreateQueryAllUGCRequest( IntPtr ISteamUGC, UGCQuery /*EUGCQuery*/ eQueryType, UGCMatchingUGCType /*EUGCMatchingUGCType*/ eMatchingeMatchingUGCTypeFileType, uint nCreatorAppID, uint nConsumerAppID, uint /*uint32*/ unPage );
				[DllImport( "libsteam_api.so" )] internal static extern UGCQueryHandle_t /*(UGCQueryHandle_t)*/ SteamAPI_ISteamUGC_CreateQueryAllUGCRequest0( IntPtr ISteamUGC, UGCQuery /*EUGCQuery*/ eQueryType, UGCMatchingUGCType /*EUGCMatchingUGCType*/ eMatchingeMatchingUGCTypeFileType, uint nCreatorAppID, uint nConsumerAppID, string /*const char **/ pchCursor );
				[DllImport( "libsteam_api.so" )] internal static extern UGCQueryHandle_t /*(UGCQueryHandle_t)*/ SteamAPI_ISteamUGC_CreateQueryUGCDetailsRequest( IntPtr ISteamUGC, IntPtr /*PublishedFileId_t **/ pvecPublishedFileID, uint /*uint32*/ unNumPublishedFileIDs );
				[DllImport( "libsteam_api.so" )] internal static extern SteamAPICall_t /*(SteamAPICall_t)*/ SteamAPI_ISteamUGC_SendQueryUGCRequest( IntPtr ISteamUGC, ulong handle );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamUGC_GetQueryUGCResult( IntPtr ISteamUGC, ulong handle, uint /*uint32*/ index, ref SteamUGCDetails_t.PackSmall /*struct SteamUGCDetails_t **/ pDetails );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamUGC_GetQueryUGCPreviewURL( IntPtr ISteamUGC, ulong handle, uint /*uint32*/ index, System.Text.StringBuilder /*char **/ pchURL, uint /*uint32*/ cchURLSize );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamUGC_GetQueryUGCMetadata( IntPtr ISteamUGC, ulong handle, uint /*uint32*/ index, System.Text.StringBuilder /*char **/ pchMetadata, uint /*uint32*/ cchMetadatasize );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamUGC_GetQueryUGCChildren( IntPtr ISteamUGC, ulong handle, uint /*uint32*/ index, IntPtr /*PublishedFileId_t **/ pvecPublishedFileID, uint /*uint32*/ cMaxEntries );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamUGC_GetQueryUGCStatistic( IntPtr ISteamUGC, ulong handle, uint /*uint32*/ index, ItemStatistic /*EItemStatistic*/ eStatType, out ulong /*uint64 **/ pStatValue );
				[DllImport( "libsteam_api.so" )] internal static extern uint /*uint32*/ SteamAPI_ISteamUGC_GetQueryUGCNumAdditionalPreviews( IntPtr ISteamUGC, ulong handle, uint /*uint32*/ index );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamUGC_GetQueryUGCAdditionalPreview( IntPtr ISteamUGC, ulong handle, uint /*uint32*/ index, uint /*uint32*/ previewIndex, System.Text.StringBuilder /*char **/ pchURLOrVideoID, uint /*uint32*/ cchURLSize, System.Text.StringBuilder /*char **/ pchOriginalFileName, uint /*uint32*/ cchOriginalFileNameSize, out ItemPreviewType /*EItemPreviewType **/ pPreviewType );
				[DllImport( "libsteam_api.so" )] internal static extern uint /*uint32*/ SteamAPI_ISteamUGC_GetQueryUGCNumKeyValueTags( IntPtr ISteamUGC, ulong handle, uint /*uint32*/ index );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamUGC_GetQueryUGCKeyValueTag( IntPtr ISteamUGC, ulong handle, uint /*uint32*/ index, uint /*uint32*/ keyValueTagIndex, System.Text.StringBuilder /*char **/ pchKey, uint /*uint32*/ cchKeySize, System.Text.StringBuilder /*char **/ pchValue, uint /*uint32*/ cchValueSize );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamUGC_ReleaseQueryUGCRequest( IntPtr ISteamUGC, ulong handle );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamUGC_AddRequiredTag( IntPtr ISteamUGC, ulong handle, string /*const char **/ pTagName );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamUGC_AddExcludedTag( IntPtr ISteamUGC, ulong handle, string /*const char **/ pTagName );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamUGC_SetReturnOnlyIDs( IntPtr ISteamUGC, ulong handle, [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bReturnOnlyIDs );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamUGC_SetReturnKeyValueTags( IntPtr ISteamUGC, ulong handle, [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bReturnKeyValueTags );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamUGC_SetReturnLongDescription( IntPtr ISteamUGC, ulong handle, [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bReturnLongDescription );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamUGC_SetReturnMetadata( IntPtr ISteamUGC, ulong handle, [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bReturnMetadata );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamUGC_SetReturnChildren( IntPtr ISteamUGC, ulong handle, [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bReturnChildren );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamUGC_SetReturnAdditionalPreviews( IntPtr ISteamUGC, ulong handle, [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bReturnAdditionalPreviews );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamUGC_SetReturnTotalOnly( IntPtr ISteamUGC, ulong handle, [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bReturnTotalOnly );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamUGC_SetReturnPlaytimeStats( IntPtr ISteamUGC, ulong handle, uint /*uint32*/ unDays );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamUGC_SetLanguage( IntPtr ISteamUGC, ulong handle, string /*const char **/ pchLanguage );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamUGC_SetAllowCachedResponse( IntPtr ISteamUGC, ulong handle, uint /*uint32*/ unMaxAgeSeconds );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamUGC_SetCloudFileNameFilter( IntPtr ISteamUGC, ulong handle, string /*const char **/ pMatchCloudFileName );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamUGC_SetMatchAnyTag( IntPtr ISteamUGC, ulong handle, [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bMatchAnyTag );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamUGC_SetSearchText( IntPtr ISteamUGC, ulong handle, string /*const char **/ pSearchText );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamUGC_SetRankedByTrendDays( IntPtr ISteamUGC, ulong handle, uint /*uint32*/ unDays );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamUGC_AddRequiredKeyValueTag( IntPtr ISteamUGC, ulong handle, string /*const char **/ pKey, string /*const char **/ pValue );
				[DllImport( "libsteam_api.so" )] internal static extern SteamAPICall_t /*(SteamAPICall_t)*/ SteamAPI_ISteamUGC_RequestUGCDetails( IntPtr ISteamUGC, ulong nPublishedFileID, uint /*uint32*/ unMaxAgeSeconds );
				[DllImport( "libsteam_api.so" )] internal static extern SteamAPICall_t /*(SteamAPICall_t)*/ SteamAPI_ISteamUGC_CreateItem( IntPtr ISteamUGC, uint nConsumerAppId, WorkshopFileType /*EWorkshopFileType*/ eFileType );
				[DllImport( "libsteam_api.so" )] internal static extern UGCUpdateHandle_t /*(UGCUpdateHandle_t)*/ SteamAPI_ISteamUGC_StartItemUpdate( IntPtr ISteamUGC, uint nConsumerAppId, ulong nPublishedFileID );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamUGC_SetItemTitle( IntPtr ISteamUGC, ulong handle, string /*const char **/ pchTitle );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamUGC_SetItemDescription( IntPtr ISteamUGC, ulong handle, string /*const char **/ pchDescription );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamUGC_SetItemUpdateLanguage( IntPtr ISteamUGC, ulong handle, string /*const char **/ pchLanguage );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamUGC_SetItemMetadata( IntPtr ISteamUGC, ulong handle, string /*const char **/ pchMetaData );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamUGC_SetItemVisibility( IntPtr ISteamUGC, ulong handle, RemoteStoragePublishedFileVisibility /*ERemoteStoragePublishedFileVisibility*/ eVisibility );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamUGC_SetItemTags( IntPtr ISteamUGC, ulong updateHandle, ref SteamParamStringArray_t.PackSmall /*const struct SteamParamStringArray_t **/ pTags );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamUGC_SetItemContent( IntPtr ISteamUGC, ulong handle, string /*const char **/ pszContentFolder );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamUGC_SetItemPreview( IntPtr ISteamUGC, ulong handle, string /*const char **/ pszPreviewFile );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamUGC_SetAllowLegacyUpload( IntPtr ISteamUGC, ulong handle, [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bAllowLegacyUpload );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamUGC_RemoveItemKeyValueTags( IntPtr ISteamUGC, ulong handle, string /*const char **/ pchKey );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamUGC_AddItemKeyValueTag( IntPtr ISteamUGC, ulong handle, string /*const char **/ pchKey, string /*const char **/ pchValue );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamUGC_AddItemPreviewFile( IntPtr ISteamUGC, ulong handle, string /*const char **/ pszPreviewFile, ItemPreviewType /*EItemPreviewType*/ type );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamUGC_AddItemPreviewVideo( IntPtr ISteamUGC, ulong handle, string /*const char **/ pszVideoID );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamUGC_UpdateItemPreviewFile( IntPtr ISteamUGC, ulong handle, uint /*uint32*/ index, string /*const char **/ pszPreviewFile );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamUGC_UpdateItemPreviewVideo( IntPtr ISteamUGC, ulong handle, uint /*uint32*/ index, string /*const char **/ pszVideoID );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamUGC_RemoveItemPreview( IntPtr ISteamUGC, ulong handle, uint /*uint32*/ index );
				[DllImport( "libsteam_api.so" )] internal static extern SteamAPICall_t /*(SteamAPICall_t)*/ SteamAPI_ISteamUGC_SubmitItemUpdate( IntPtr ISteamUGC, ulong handle, string /*const char **/ pchChangeNote );
				[DllImport( "libsteam_api.so" )] internal static extern ItemUpdateStatus /*EItemUpdateStatus*/ SteamAPI_ISteamUGC_GetItemUpdateProgress( IntPtr ISteamUGC, ulong handle, out ulong /*uint64 **/ punBytesProcessed, out ulong /*uint64 **/ punBytesTotal );
				[DllImport( "libsteam_api.so" )] internal static extern SteamAPICall_t /*(SteamAPICall_t)*/ SteamAPI_ISteamUGC_SetUserItemVote( IntPtr ISteamUGC, ulong nPublishedFileID, [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bVoteUp );
				[DllImport( "libsteam_api.so" )] internal static extern SteamAPICall_t /*(SteamAPICall_t)*/ SteamAPI_ISteamUGC_GetUserItemVote( IntPtr ISteamUGC, ulong nPublishedFileID );
				[DllImport( "libsteam_api.so" )] internal static extern SteamAPICall_t /*(SteamAPICall_t)*/ SteamAPI_ISteamUGC_AddItemToFavorites( IntPtr ISteamUGC, uint nAppId, ulong nPublishedFileID );
				[DllImport( "libsteam_api.so" )] internal static extern SteamAPICall_t /*(SteamAPICall_t)*/ SteamAPI_ISteamUGC_RemoveItemFromFavorites( IntPtr ISteamUGC, uint nAppId, ulong nPublishedFileID );
				[DllImport( "libsteam_api.so" )] internal static extern SteamAPICall_t /*(SteamAPICall_t)*/ SteamAPI_ISteamUGC_SubscribeItem( IntPtr ISteamUGC, ulong nPublishedFileID );
				[DllImport( "libsteam_api.so" )] internal static extern SteamAPICall_t /*(SteamAPICall_t)*/ SteamAPI_ISteamUGC_UnsubscribeItem( IntPtr ISteamUGC, ulong nPublishedFileID );
				[DllImport( "libsteam_api.so" )] internal static extern uint /*uint32*/ SteamAPI_ISteamUGC_GetNumSubscribedItems( IntPtr ISteamUGC );
				[DllImport( "libsteam_api.so" )] internal static extern uint /*uint32*/ SteamAPI_ISteamUGC_GetSubscribedItems( IntPtr ISteamUGC, IntPtr /*PublishedFileId_t **/ pvecPublishedFileID, uint /*uint32*/ cMaxEntries );
				[DllImport( "libsteam_api.so" )] internal static extern uint /*uint32*/ SteamAPI_ISteamUGC_GetItemState( IntPtr ISteamUGC, ulong nPublishedFileID );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamUGC_GetItemInstallInfo( IntPtr ISteamUGC, ulong nPublishedFileID, out ulong /*uint64 **/ punSizeOnDisk, System.Text.StringBuilder /*char **/ pchFolder, uint /*uint32*/ cchFolderSize, out uint /*uint32 **/ punTimeStamp );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamUGC_GetItemDownloadInfo( IntPtr ISteamUGC, ulong nPublishedFileID, out ulong /*uint64 **/ punBytesDownloaded, out ulong /*uint64 **/ punBytesTotal );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamUGC_DownloadItem( IntPtr ISteamUGC, ulong nPublishedFileID, [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bHighPriority );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamUGC_BInitWorkshopForGameServer( IntPtr ISteamUGC, uint unWorkshopDepotID, string /*const char **/ pszFolder );
				[DllImport( "libsteam_api.so" )] internal static extern void /*void*/ SteamAPI_ISteamUGC_SuspendDownloads( IntPtr ISteamUGC, [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bSuspend );
				[DllImport( "libsteam_api.so" )] internal static extern SteamAPICall_t /*(SteamAPICall_t)*/ SteamAPI_ISteamUGC_StartPlaytimeTracking( IntPtr ISteamUGC, IntPtr /*PublishedFileId_t **/ pvecPublishedFileID, uint /*uint32*/ unNumPublishedFileIDs );
				[DllImport( "libsteam_api.so" )] internal static extern SteamAPICall_t /*(SteamAPICall_t)*/ SteamAPI_ISteamUGC_StopPlaytimeTracking( IntPtr ISteamUGC, IntPtr /*PublishedFileId_t **/ pvecPublishedFileID, uint /*uint32*/ unNumPublishedFileIDs );
				[DllImport( "libsteam_api.so" )] internal static extern SteamAPICall_t /*(SteamAPICall_t)*/ SteamAPI_ISteamUGC_StopPlaytimeTrackingForAllItems( IntPtr ISteamUGC );
				[DllImport( "libsteam_api.so" )] internal static extern SteamAPICall_t /*(SteamAPICall_t)*/ SteamAPI_ISteamUGC_AddDependency( IntPtr ISteamUGC, ulong nParentPublishedFileID, ulong nChildPublishedFileID );
				[DllImport( "libsteam_api.so" )] internal static extern SteamAPICall_t /*(SteamAPICall_t)*/ SteamAPI_ISteamUGC_RemoveDependency( IntPtr ISteamUGC, ulong nParentPublishedFileID, ulong nChildPublishedFileID );
				[DllImport( "libsteam_api.so" )] internal static extern SteamAPICall_t /*(SteamAPICall_t)*/ SteamAPI_ISteamUGC_AddAppDependency( IntPtr ISteamUGC, ulong nPublishedFileID, uint nAppID );
				[DllImport( "libsteam_api.so" )] internal static extern SteamAPICall_t /*(SteamAPICall_t)*/ SteamAPI_ISteamUGC_RemoveAppDependency( IntPtr ISteamUGC, ulong nPublishedFileID, uint nAppID );
				[DllImport( "libsteam_api.so" )] internal static extern SteamAPICall_t /*(SteamAPICall_t)*/ SteamAPI_ISteamUGC_GetAppDependencies( IntPtr ISteamUGC, ulong nPublishedFileID );
				[DllImport( "libsteam_api.so" )] internal static extern SteamAPICall_t /*(SteamAPICall_t)*/ SteamAPI_ISteamUGC_DeleteItem( IntPtr ISteamUGC, ulong nPublishedFileID );
				
				//
				// ISteamAppList 
				//
				[DllImport( "libsteam_api.so" )] internal static extern uint /*uint32*/ SteamAPI_ISteamAppList_GetNumInstalledApps( IntPtr ISteamAppList );
				[DllImport( "libsteam_api.so" )] internal static extern uint /*uint32*/ SteamAPI_ISteamAppList_GetInstalledApps( IntPtr ISteamAppList, IntPtr /*AppId_t **/ pvecAppID, uint /*uint32*/ unMaxAppIDs );
				[DllImport( "libsteam_api.so" )] internal static extern int /*int*/ SteamAPI_ISteamAppList_GetAppName( IntPtr ISteamAppList, uint nAppID, System.Text.StringBuilder /*char **/ pchName, int /*int*/ cchNameMax );
				[DllImport( "libsteam_api.so" )] internal static extern int /*int*/ SteamAPI_ISteamAppList_GetAppInstallDir( IntPtr ISteamAppList, uint nAppID, System.Text.StringBuilder /*char **/ pchDirectory, int /*int*/ cchNameMax );
				[DllImport( "libsteam_api.so" )] internal static extern int /*int*/ SteamAPI_ISteamAppList_GetAppBuildId( IntPtr ISteamAppList, uint nAppID );
				
				//
				// ISteamHTMLSurface 
				//
				[DllImport( "libsteam_api.so" )] internal static extern void /*void*/ SteamAPI_ISteamHTMLSurface_DestructISteamHTMLSurface( IntPtr ISteamHTMLSurface );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamHTMLSurface_Init( IntPtr ISteamHTMLSurface );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamHTMLSurface_Shutdown( IntPtr ISteamHTMLSurface );
				[DllImport( "libsteam_api.so" )] internal static extern SteamAPICall_t /*(SteamAPICall_t)*/ SteamAPI_ISteamHTMLSurface_CreateBrowser( IntPtr ISteamHTMLSurface, string /*const char **/ pchUserAgent, string /*const char **/ pchUserCSS );
				[DllImport( "libsteam_api.so" )] internal static extern void /*void*/ SteamAPI_ISteamHTMLSurface_RemoveBrowser( IntPtr ISteamHTMLSurface, uint unBrowserHandle );
				[DllImport( "libsteam_api.so" )] internal static extern void /*void*/ SteamAPI_ISteamHTMLSurface_LoadURL( IntPtr ISteamHTMLSurface, uint unBrowserHandle, string /*const char **/ pchURL, string /*const char **/ pchPostData );
				[DllImport( "libsteam_api.so" )] internal static extern void /*void*/ SteamAPI_ISteamHTMLSurface_SetSize( IntPtr ISteamHTMLSurface, uint unBrowserHandle, uint /*uint32*/ unWidth, uint /*uint32*/ unHeight );
				[DllImport( "libsteam_api.so" )] internal static extern void /*void*/ SteamAPI_ISteamHTMLSurface_StopLoad( IntPtr ISteamHTMLSurface, uint unBrowserHandle );
				[DllImport( "libsteam_api.so" )] internal static extern void /*void*/ SteamAPI_ISteamHTMLSurface_Reload( IntPtr ISteamHTMLSurface, uint unBrowserHandle );
				[DllImport( "libsteam_api.so" )] internal static extern void /*void*/ SteamAPI_ISteamHTMLSurface_GoBack( IntPtr ISteamHTMLSurface, uint unBrowserHandle );
				[DllImport( "libsteam_api.so" )] internal static extern void /*void*/ SteamAPI_ISteamHTMLSurface_GoForward( IntPtr ISteamHTMLSurface, uint unBrowserHandle );
				[DllImport( "libsteam_api.so" )] internal static extern void /*void*/ SteamAPI_ISteamHTMLSurface_AddHeader( IntPtr ISteamHTMLSurface, uint unBrowserHandle, string /*const char **/ pchKey, string /*const char **/ pchValue );
				[DllImport( "libsteam_api.so" )] internal static extern void /*void*/ SteamAPI_ISteamHTMLSurface_ExecuteJavascript( IntPtr ISteamHTMLSurface, uint unBrowserHandle, string /*const char **/ pchScript );
				[DllImport( "libsteam_api.so" )] internal static extern void /*void*/ SteamAPI_ISteamHTMLSurface_MouseUp( IntPtr ISteamHTMLSurface, uint unBrowserHandle, HTMLMouseButton /*ISteamHTMLSurface::EHTMLMouseButton*/ eMouseButton );
				[DllImport( "libsteam_api.so" )] internal static extern void /*void*/ SteamAPI_ISteamHTMLSurface_MouseDown( IntPtr ISteamHTMLSurface, uint unBrowserHandle, HTMLMouseButton /*ISteamHTMLSurface::EHTMLMouseButton*/ eMouseButton );
				[DllImport( "libsteam_api.so" )] internal static extern void /*void*/ SteamAPI_ISteamHTMLSurface_MouseDoubleClick( IntPtr ISteamHTMLSurface, uint unBrowserHandle, HTMLMouseButton /*ISteamHTMLSurface::EHTMLMouseButton*/ eMouseButton );
				[DllImport( "libsteam_api.so" )] internal static extern void /*void*/ SteamAPI_ISteamHTMLSurface_MouseMove( IntPtr ISteamHTMLSurface, uint unBrowserHandle, int /*int*/ x, int /*int*/ y );
				[DllImport( "libsteam_api.so" )] internal static extern void /*void*/ SteamAPI_ISteamHTMLSurface_MouseWheel( IntPtr ISteamHTMLSurface, uint unBrowserHandle, int /*int32*/ nDelta );
				[DllImport( "libsteam_api.so" )] internal static extern void /*void*/ SteamAPI_ISteamHTMLSurface_KeyDown( IntPtr ISteamHTMLSurface, uint unBrowserHandle, uint /*uint32*/ nNativeKeyCode, HTMLKeyModifiers /*ISteamHTMLSurface::EHTMLKeyModifiers*/ eHTMLKeyModifiers, [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bIsSystemKey );
				[DllImport( "libsteam_api.so" )] internal static extern void /*void*/ SteamAPI_ISteamHTMLSurface_KeyUp( IntPtr ISteamHTMLSurface, uint unBrowserHandle, uint /*uint32*/ nNativeKeyCode, HTMLKeyModifiers /*ISteamHTMLSurface::EHTMLKeyModifiers*/ eHTMLKeyModifiers );
				[DllImport( "libsteam_api.so" )] internal static extern void /*void*/ SteamAPI_ISteamHTMLSurface_KeyChar( IntPtr ISteamHTMLSurface, uint unBrowserHandle, uint /*uint32*/ cUnicodeChar, HTMLKeyModifiers /*ISteamHTMLSurface::EHTMLKeyModifiers*/ eHTMLKeyModifiers );
				[DllImport( "libsteam_api.so" )] internal static extern void /*void*/ SteamAPI_ISteamHTMLSurface_SetHorizontalScroll( IntPtr ISteamHTMLSurface, uint unBrowserHandle, uint /*uint32*/ nAbsolutePixelScroll );
				[DllImport( "libsteam_api.so" )] internal static extern void /*void*/ SteamAPI_ISteamHTMLSurface_SetVerticalScroll( IntPtr ISteamHTMLSurface, uint unBrowserHandle, uint /*uint32*/ nAbsolutePixelScroll );
				[DllImport( "libsteam_api.so" )] internal static extern void /*void*/ SteamAPI_ISteamHTMLSurface_SetKeyFocus( IntPtr ISteamHTMLSurface, uint unBrowserHandle, [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bHasKeyFocus );
				[DllImport( "libsteam_api.so" )] internal static extern void /*void*/ SteamAPI_ISteamHTMLSurface_ViewSource( IntPtr ISteamHTMLSurface, uint unBrowserHandle );
				[DllImport( "libsteam_api.so" )] internal static extern void /*void*/ SteamAPI_ISteamHTMLSurface_CopyToClipboard( IntPtr ISteamHTMLSurface, uint unBrowserHandle );
				[DllImport( "libsteam_api.so" )] internal static extern void /*void*/ SteamAPI_ISteamHTMLSurface_PasteFromClipboard( IntPtr ISteamHTMLSurface, uint unBrowserHandle );
				[DllImport( "libsteam_api.so" )] internal static extern void /*void*/ SteamAPI_ISteamHTMLSurface_Find( IntPtr ISteamHTMLSurface, uint unBrowserHandle, string /*const char **/ pchSearchStr, [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bCurrentlyInFind, [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bReverse );
				[DllImport( "libsteam_api.so" )] internal static extern void /*void*/ SteamAPI_ISteamHTMLSurface_StopFind( IntPtr ISteamHTMLSurface, uint unBrowserHandle );
				[DllImport( "libsteam_api.so" )] internal static extern void /*void*/ SteamAPI_ISteamHTMLSurface_GetLinkAtPosition( IntPtr ISteamHTMLSurface, uint unBrowserHandle, int /*int*/ x, int /*int*/ y );
				[DllImport( "libsteam_api.so" )] internal static extern void /*void*/ SteamAPI_ISteamHTMLSurface_SetCookie( IntPtr ISteamHTMLSurface, string /*const char **/ pchHostname, string /*const char **/ pchKey, string /*const char **/ pchValue, string /*const char **/ pchPath, uint nExpires, [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bSecure, [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bHTTPOnly );
				[DllImport( "libsteam_api.so" )] internal static extern void /*void*/ SteamAPI_ISteamHTMLSurface_SetPageScaleFactor( IntPtr ISteamHTMLSurface, uint unBrowserHandle, float /*float*/ flZoom, int /*int*/ nPointX, int /*int*/ nPointY );
				[DllImport( "libsteam_api.so" )] internal static extern void /*void*/ SteamAPI_ISteamHTMLSurface_SetBackgroundMode( IntPtr ISteamHTMLSurface, uint unBrowserHandle, [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bBackgroundMode );
				[DllImport( "libsteam_api.so" )] internal static extern void /*void*/ SteamAPI_ISteamHTMLSurface_SetDPIScalingFactor( IntPtr ISteamHTMLSurface, uint unBrowserHandle, float /*float*/ flDPIScaling );
				[DllImport( "libsteam_api.so" )] internal static extern void /*void*/ SteamAPI_ISteamHTMLSurface_OpenDeveloperTools( IntPtr ISteamHTMLSurface, uint unBrowserHandle );
				[DllImport( "libsteam_api.so" )] internal static extern void /*void*/ SteamAPI_ISteamHTMLSurface_AllowStartRequest( IntPtr ISteamHTMLSurface, uint unBrowserHandle, [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bAllowed );
				[DllImport( "libsteam_api.so" )] internal static extern void /*void*/ SteamAPI_ISteamHTMLSurface_JSDialogResponse( IntPtr ISteamHTMLSurface, uint unBrowserHandle, [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bResult );
				
				//
				// ISteamInventory 
				//
				[DllImport( "libsteam_api.so" )] internal static extern Result /*EResult*/ SteamAPI_ISteamInventory_GetResultStatus( IntPtr ISteamInventory, int resultHandle );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamInventory_GetResultItems( IntPtr ISteamInventory, int resultHandle, IntPtr /*struct SteamItemDetails_t **/ pOutItemsArray, out uint /*uint32 **/ punOutItemsArraySize );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamInventory_GetResultItemProperty( IntPtr ISteamInventory, int resultHandle, uint /*uint32*/ unItemIndex, string /*const char **/ pchPropertyName, System.Text.StringBuilder /*char **/ pchValueBuffer, out uint /*uint32 **/ punValueBufferSizeOut );
				[DllImport( "libsteam_api.so" )] internal static extern uint /*uint32*/ SteamAPI_ISteamInventory_GetResultTimestamp( IntPtr ISteamInventory, int resultHandle );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamInventory_CheckResultSteamID( IntPtr ISteamInventory, int resultHandle, ulong steamIDExpected );
				[DllImport( "libsteam_api.so" )] internal static extern void /*void*/ SteamAPI_ISteamInventory_DestroyResult( IntPtr ISteamInventory, int resultHandle );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamInventory_GetAllItems( IntPtr ISteamInventory, ref int pResultHandle );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamInventory_GetItemsByID( IntPtr ISteamInventory, ref int pResultHandle, ulong[] pInstanceIDs, uint /*uint32*/ unCountInstanceIDs );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamInventory_SerializeResult( IntPtr ISteamInventory, int resultHandle, IntPtr /*void **/ pOutBuffer, out uint /*uint32 **/ punOutBufferSize );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamInventory_DeserializeResult( IntPtr ISteamInventory, ref int pOutResultHandle, IntPtr /*const void **/ pBuffer, uint /*uint32*/ unBufferSize, [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bRESERVED_MUST_BE_FALSE );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamInventory_GenerateItems( IntPtr ISteamInventory, ref int pResultHandle, int[] pArrayItemDefs, uint[] /*const uint32 **/ punArrayQuantity, uint /*uint32*/ unArrayLength );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamInventory_GrantPromoItems( IntPtr ISteamInventory, ref int pResultHandle );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamInventory_AddPromoItem( IntPtr ISteamInventory, ref int pResultHandle, int itemDef );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamInventory_AddPromoItems( IntPtr ISteamInventory, ref int pResultHandle, int[] pArrayItemDefs, uint /*uint32*/ unArrayLength );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamInventory_ConsumeItem( IntPtr ISteamInventory, ref int pResultHandle, ulong itemConsume, uint /*uint32*/ unQuantity );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamInventory_ExchangeItems( IntPtr ISteamInventory, ref int pResultHandle, int[] pArrayGenerate, uint[] /*const uint32 **/ punArrayGenerateQuantity, uint /*uint32*/ unArrayGenerateLength, ulong[] pArrayDestroy, uint[] /*const uint32 **/ punArrayDestroyQuantity, uint /*uint32*/ unArrayDestroyLength );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamInventory_TransferItemQuantity( IntPtr ISteamInventory, ref int pResultHandle, ulong itemIdSource, uint /*uint32*/ unQuantity, ulong itemIdDest );
				[DllImport( "libsteam_api.so" )] internal static extern void /*void*/ SteamAPI_ISteamInventory_SendItemDropHeartbeat( IntPtr ISteamInventory );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamInventory_TriggerItemDrop( IntPtr ISteamInventory, ref int pResultHandle, int dropListDefinition );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamInventory_TradeItems( IntPtr ISteamInventory, ref int pResultHandle, ulong steamIDTradePartner, ulong[] pArrayGive, uint[] /*const uint32 **/ pArrayGiveQuantity, uint /*uint32*/ nArrayGiveLength, ulong[] pArrayGet, uint[] /*const uint32 **/ pArrayGetQuantity, uint /*uint32*/ nArrayGetLength );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamInventory_LoadItemDefinitions( IntPtr ISteamInventory );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamInventory_GetItemDefinitionIDs( IntPtr ISteamInventory, IntPtr /*SteamItemDef_t **/ pItemDefIDs, out uint /*uint32 **/ punItemDefIDsArraySize );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamInventory_GetItemDefinitionProperty( IntPtr ISteamInventory, int iDefinition, string /*const char **/ pchPropertyName, System.Text.StringBuilder /*char **/ pchValueBuffer, out uint /*uint32 **/ punValueBufferSizeOut );
				[DllImport( "libsteam_api.so" )] internal static extern SteamAPICall_t /*(SteamAPICall_t)*/ SteamAPI_ISteamInventory_RequestEligiblePromoItemDefinitionsIDs( IntPtr ISteamInventory, ulong steamID );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamInventory_GetEligiblePromoItemDefinitionIDs( IntPtr ISteamInventory, ulong steamID, IntPtr /*SteamItemDef_t **/ pItemDefIDs, out uint /*uint32 **/ punItemDefIDsArraySize );
				[DllImport( "libsteam_api.so" )] internal static extern SteamAPICall_t /*(SteamAPICall_t)*/ SteamAPI_ISteamInventory_StartPurchase( IntPtr ISteamInventory, int[] pArrayItemDefs, uint[] /*const uint32 **/ punArrayQuantity, uint /*uint32*/ unArrayLength );
				[DllImport( "libsteam_api.so" )] internal static extern SteamAPICall_t /*(SteamAPICall_t)*/ SteamAPI_ISteamInventory_RequestPrices( IntPtr ISteamInventory );
				[DllImport( "libsteam_api.so" )] internal static extern uint /*uint32*/ SteamAPI_ISteamInventory_GetNumItemsWithPrices( IntPtr ISteamInventory );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamInventory_GetItemsWithPrices( IntPtr ISteamInventory, IntPtr /*SteamItemDef_t **/ pArrayItemDefs, IntPtr /*uint64 **/ pCurrentPrices, IntPtr /*uint64 **/ pBasePrices, uint /*uint32*/ unArrayLength );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamInventory_GetItemPrice( IntPtr ISteamInventory, int iDefinition, out ulong /*uint64 **/ pCurrentPrice, out ulong /*uint64 **/ pBasePrice );
				[DllImport( "libsteam_api.so" )] internal static extern SteamInventoryUpdateHandle_t /*(SteamInventoryUpdateHandle_t)*/ SteamAPI_ISteamInventory_StartUpdateProperties( IntPtr ISteamInventory );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamInventory_RemoveProperty( IntPtr ISteamInventory, ulong handle, ulong nItemID, string /*const char **/ pchPropertyName );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamInventory_SetProperty( IntPtr ISteamInventory, ulong handle, ulong nItemID, string /*const char **/ pchPropertyName, string /*const char **/ pchPropertyValue );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamInventory_SetProperty0( IntPtr ISteamInventory, ulong handle, ulong nItemID, string /*const char **/ pchPropertyName, [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bValue );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamInventory_SetProperty0( IntPtr ISteamInventory, ulong handle, ulong nItemID, string /*const char **/ pchPropertyName, long /*int64*/ nValue );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamInventory_SetProperty0( IntPtr ISteamInventory, ulong handle, ulong nItemID, string /*const char **/ pchPropertyName, float /*float*/ flValue );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamInventory_SubmitUpdateProperties( IntPtr ISteamInventory, ulong handle, ref int pResultHandle );
				
				//
				// ISteamVideo 
				//
				[DllImport( "libsteam_api.so" )] internal static extern void /*void*/ SteamAPI_ISteamVideo_GetVideoURL( IntPtr ISteamVideo, uint unVideoAppID );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamVideo_IsBroadcasting( IntPtr ISteamVideo, IntPtr /*int **/ pnNumViewers );
				[DllImport( "libsteam_api.so" )] internal static extern void /*void*/ SteamAPI_ISteamVideo_GetOPFSettings( IntPtr ISteamVideo, uint unVideoAppID );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamVideo_GetOPFStringForApp( IntPtr ISteamVideo, uint unVideoAppID, System.Text.StringBuilder /*char **/ pchBuffer, out int /*int32 **/ pnBufferSize );
				
				//
				// ISteamParentalSettings 
				//
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamParentalSettings_BIsParentalLockEnabled( IntPtr ISteamParentalSettings );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamParentalSettings_BIsParentalLockLocked( IntPtr ISteamParentalSettings );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamParentalSettings_BIsAppBlocked( IntPtr ISteamParentalSettings, uint nAppID );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamParentalSettings_BIsAppInBlockList( IntPtr ISteamParentalSettings, uint nAppID );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamParentalSettings_BIsFeatureBlocked( IntPtr ISteamParentalSettings, ParentalFeature /*EParentalFeature*/ eFeature );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamParentalSettings_BIsFeatureInBlockList( IntPtr ISteamParentalSettings, ParentalFeature /*EParentalFeature*/ eFeature );
				
				//
				// ISteamGameServer 
				//
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamGameServer_InitGameServer( IntPtr ISteamGameServer, uint /*uint32*/ unIP, ushort /*uint16*/ usGamePort, ushort /*uint16*/ usQueryPort, uint /*uint32*/ unFlags, uint nGameAppId, string /*const char **/ pchVersionString );
				[DllImport( "libsteam_api.so" )] internal static extern void /*void*/ SteamAPI_ISteamGameServer_SetProduct( IntPtr ISteamGameServer, string /*const char **/ pszProduct );
				[DllImport( "libsteam_api.so" )] internal static extern void /*void*/ SteamAPI_ISteamGameServer_SetGameDescription( IntPtr ISteamGameServer, string /*const char **/ pszGameDescription );
				[DllImport( "libsteam_api.so" )] internal static extern void /*void*/ SteamAPI_ISteamGameServer_SetModDir( IntPtr ISteamGameServer, string /*const char **/ pszModDir );
				[DllImport( "libsteam_api.so" )] internal static extern void /*void*/ SteamAPI_ISteamGameServer_SetDedicatedServer( IntPtr ISteamGameServer, [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bDedicated );
				[DllImport( "libsteam_api.so" )] internal static extern void /*void*/ SteamAPI_ISteamGameServer_LogOn( IntPtr ISteamGameServer, string /*const char **/ pszToken );
				[DllImport( "libsteam_api.so" )] internal static extern void /*void*/ SteamAPI_ISteamGameServer_LogOnAnonymous( IntPtr ISteamGameServer );
				[DllImport( "libsteam_api.so" )] internal static extern void /*void*/ SteamAPI_ISteamGameServer_LogOff( IntPtr ISteamGameServer );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamGameServer_BLoggedOn( IntPtr ISteamGameServer );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamGameServer_BSecure( IntPtr ISteamGameServer );
				[DllImport( "libsteam_api.so" )] internal static extern CSteamID /*(class CSteamID)*/ SteamAPI_ISteamGameServer_GetSteamID( IntPtr ISteamGameServer );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamGameServer_WasRestartRequested( IntPtr ISteamGameServer );
				[DllImport( "libsteam_api.so" )] internal static extern void /*void*/ SteamAPI_ISteamGameServer_SetMaxPlayerCount( IntPtr ISteamGameServer, int /*int*/ cPlayersMax );
				[DllImport( "libsteam_api.so" )] internal static extern void /*void*/ SteamAPI_ISteamGameServer_SetBotPlayerCount( IntPtr ISteamGameServer, int /*int*/ cBotplayers );
				[DllImport( "libsteam_api.so" )] internal static extern void /*void*/ SteamAPI_ISteamGameServer_SetServerName( IntPtr ISteamGameServer, string /*const char **/ pszServerName );
				[DllImport( "libsteam_api.so" )] internal static extern void /*void*/ SteamAPI_ISteamGameServer_SetMapName( IntPtr ISteamGameServer, string /*const char **/ pszMapName );
				[DllImport( "libsteam_api.so" )] internal static extern void /*void*/ SteamAPI_ISteamGameServer_SetPasswordProtected( IntPtr ISteamGameServer, [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bPasswordProtected );
				[DllImport( "libsteam_api.so" )] internal static extern void /*void*/ SteamAPI_ISteamGameServer_SetSpectatorPort( IntPtr ISteamGameServer, ushort /*uint16*/ unSpectatorPort );
				[DllImport( "libsteam_api.so" )] internal static extern void /*void*/ SteamAPI_ISteamGameServer_SetSpectatorServerName( IntPtr ISteamGameServer, string /*const char **/ pszSpectatorServerName );
				[DllImport( "libsteam_api.so" )] internal static extern void /*void*/ SteamAPI_ISteamGameServer_ClearAllKeyValues( IntPtr ISteamGameServer );
				[DllImport( "libsteam_api.so" )] internal static extern void /*void*/ SteamAPI_ISteamGameServer_SetKeyValue( IntPtr ISteamGameServer, string /*const char **/ pKey, string /*const char **/ pValue );
				[DllImport( "libsteam_api.so" )] internal static extern void /*void*/ SteamAPI_ISteamGameServer_SetGameTags( IntPtr ISteamGameServer, string /*const char **/ pchGameTags );
				[DllImport( "libsteam_api.so" )] internal static extern void /*void*/ SteamAPI_ISteamGameServer_SetGameData( IntPtr ISteamGameServer, string /*const char **/ pchGameData );
				[DllImport( "libsteam_api.so" )] internal static extern void /*void*/ SteamAPI_ISteamGameServer_SetRegion( IntPtr ISteamGameServer, string /*const char **/ pszRegion );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamGameServer_SendUserConnectAndAuthenticate( IntPtr ISteamGameServer, uint /*uint32*/ unIPClient, IntPtr /*const void **/ pvAuthBlob, uint /*uint32*/ cubAuthBlobSize, out ulong pSteamIDUser );
				[DllImport( "libsteam_api.so" )] internal static extern CSteamID /*(class CSteamID)*/ SteamAPI_ISteamGameServer_CreateUnauthenticatedUserConnection( IntPtr ISteamGameServer );
				[DllImport( "libsteam_api.so" )] internal static extern void /*void*/ SteamAPI_ISteamGameServer_SendUserDisconnect( IntPtr ISteamGameServer, ulong steamIDUser );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamGameServer_BUpdateUserData( IntPtr ISteamGameServer, ulong steamIDUser, string /*const char **/ pchPlayerName, uint /*uint32*/ uScore );
				[DllImport( "libsteam_api.so" )] internal static extern HAuthTicket /*(HAuthTicket)*/ SteamAPI_ISteamGameServer_GetAuthSessionTicket( IntPtr ISteamGameServer, IntPtr /*void **/ pTicket, int /*int*/ cbMaxTicket, out uint /*uint32 **/ pcbTicket );
				[DllImport( "libsteam_api.so" )] internal static extern BeginAuthSessionResult /*EBeginAuthSessionResult*/ SteamAPI_ISteamGameServer_BeginAuthSession( IntPtr ISteamGameServer, IntPtr /*const void **/ pAuthTicket, int /*int*/ cbAuthTicket, ulong steamID );
				[DllImport( "libsteam_api.so" )] internal static extern void /*void*/ SteamAPI_ISteamGameServer_EndAuthSession( IntPtr ISteamGameServer, ulong steamID );
				[DllImport( "libsteam_api.so" )] internal static extern void /*void*/ SteamAPI_ISteamGameServer_CancelAuthTicket( IntPtr ISteamGameServer, uint hAuthTicket );
				[DllImport( "libsteam_api.so" )] internal static extern UserHasLicenseForAppResult /*EUserHasLicenseForAppResult*/ SteamAPI_ISteamGameServer_UserHasLicenseForApp( IntPtr ISteamGameServer, ulong steamID, uint appID );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamGameServer_RequestUserGroupStatus( IntPtr ISteamGameServer, ulong steamIDUser, ulong steamIDGroup );
				[DllImport( "libsteam_api.so" )] internal static extern void /*void*/ SteamAPI_ISteamGameServer_GetGameplayStats( IntPtr ISteamGameServer );
				[DllImport( "libsteam_api.so" )] internal static extern SteamAPICall_t /*(SteamAPICall_t)*/ SteamAPI_ISteamGameServer_GetServerReputation( IntPtr ISteamGameServer );
				[DllImport( "libsteam_api.so" )] internal static extern uint /*uint32*/ SteamAPI_ISteamGameServer_GetPublicIP( IntPtr ISteamGameServer );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamGameServer_HandleIncomingPacket( IntPtr ISteamGameServer, IntPtr /*const void **/ pData, int /*int*/ cbData, uint /*uint32*/ srcIP, ushort /*uint16*/ srcPort );
				[DllImport( "libsteam_api.so" )] internal static extern int /*int*/ SteamAPI_ISteamGameServer_GetNextOutgoingPacket( IntPtr ISteamGameServer, IntPtr /*void **/ pOut, int /*int*/ cbMaxOut, out uint /*uint32 **/ pNetAdr, out ushort /*uint16 **/ pPort );
				[DllImport( "libsteam_api.so" )] internal static extern void /*void*/ SteamAPI_ISteamGameServer_EnableHeartbeats( IntPtr ISteamGameServer, [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bActive );
				[DllImport( "libsteam_api.so" )] internal static extern void /*void*/ SteamAPI_ISteamGameServer_SetHeartbeatInterval( IntPtr ISteamGameServer, int /*int*/ iHeartbeatInterval );
				[DllImport( "libsteam_api.so" )] internal static extern void /*void*/ SteamAPI_ISteamGameServer_ForceHeartbeat( IntPtr ISteamGameServer );
				[DllImport( "libsteam_api.so" )] internal static extern SteamAPICall_t /*(SteamAPICall_t)*/ SteamAPI_ISteamGameServer_AssociateWithClan( IntPtr ISteamGameServer, ulong steamIDClan );
				[DllImport( "libsteam_api.so" )] internal static extern SteamAPICall_t /*(SteamAPICall_t)*/ SteamAPI_ISteamGameServer_ComputeNewPlayerCompatibility( IntPtr ISteamGameServer, ulong steamIDNewPlayer );
				
				//
				// ISteamGameServerStats 
				//
				[DllImport( "libsteam_api.so" )] internal static extern SteamAPICall_t /*(SteamAPICall_t)*/ SteamAPI_ISteamGameServerStats_RequestUserStats( IntPtr ISteamGameServerStats, ulong steamIDUser );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamGameServerStats_GetUserStat( IntPtr ISteamGameServerStats, ulong steamIDUser, string /*const char **/ pchName, out int /*int32 **/ pData );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamGameServerStats_GetUserStat0( IntPtr ISteamGameServerStats, ulong steamIDUser, string /*const char **/ pchName, out float /*float **/ pData );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamGameServerStats_GetUserAchievement( IntPtr ISteamGameServerStats, ulong steamIDUser, string /*const char **/ pchName, [MarshalAs(UnmanagedType.U1)] ref bool /*bool **/ pbAchieved );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamGameServerStats_SetUserStat( IntPtr ISteamGameServerStats, ulong steamIDUser, string /*const char **/ pchName, int /*int32*/ nData );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamGameServerStats_SetUserStat0( IntPtr ISteamGameServerStats, ulong steamIDUser, string /*const char **/ pchName, float /*float*/ fData );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamGameServerStats_UpdateUserAvgRateStat( IntPtr ISteamGameServerStats, ulong steamIDUser, string /*const char **/ pchName, float /*float*/ flCountThisSession, double /*double*/ dSessionLength );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamGameServerStats_SetUserAchievement( IntPtr ISteamGameServerStats, ulong steamIDUser, string /*const char **/ pchName );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_ISteamGameServerStats_ClearUserAchievement( IntPtr ISteamGameServerStats, ulong steamIDUser, string /*const char **/ pchName );
				[DllImport( "libsteam_api.so" )] internal static extern SteamAPICall_t /*(SteamAPICall_t)*/ SteamAPI_ISteamGameServerStats_StoreUserStats( IntPtr ISteamGameServerStats, ulong steamIDUser );
				
				//
				// SteamApi 
				//
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_Init();
				[DllImport( "libsteam_api.so" )] internal static extern void /*void*/ SteamAPI_RunCallbacks();
				[DllImport( "libsteam_api.so" )] internal static extern void /*void*/ SteamGameServer_RunCallbacks();
				[DllImport( "libsteam_api.so" )] internal static extern void /*void*/ SteamAPI_RegisterCallback( IntPtr /*void **/ pCallback, int /*int*/ callback );
				[DllImport( "libsteam_api.so" )] internal static extern void /*void*/ SteamAPI_UnregisterCallback( IntPtr /*void **/ pCallback );
				[DllImport( "libsteam_api.so" )] internal static extern void /*void*/ SteamAPI_RegisterCallResult( IntPtr /*void **/ pCallback, ulong callback );
				[DllImport( "libsteam_api.so" )] internal static extern void /*void*/ SteamAPI_UnregisterCallResult( IntPtr /*void **/ pCallback, ulong callback );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamInternal_GameServer_Init( uint /*uint32*/ unIP, ushort /*uint16*/ usPort, ushort /*uint16*/ usGamePort, ushort /*uint16*/ usQueryPort, int /*int*/ eServerMode, string /*const char **/ pchVersionString );
				[DllImport( "libsteam_api.so" )] internal static extern void /*void*/ SteamAPI_Shutdown();
				[DllImport( "libsteam_api.so" )] internal static extern void /*void*/ SteamGameServer_Shutdown();
				[DllImport( "libsteam_api.so" )] internal static extern HSteamUser /*(HSteamUser)*/ SteamAPI_GetHSteamUser();
				[DllImport( "libsteam_api.so" )] internal static extern HSteamPipe /*(HSteamPipe)*/ SteamAPI_GetHSteamPipe();
				[DllImport( "libsteam_api.so" )] internal static extern HSteamUser /*(HSteamUser)*/ SteamGameServer_GetHSteamUser();
				[DllImport( "libsteam_api.so" )] internal static extern HSteamPipe /*(HSteamPipe)*/ SteamGameServer_GetHSteamPipe();
				[DllImport( "libsteam_api.so" )] internal static extern IntPtr /*void **/ SteamInternal_CreateInterface( string /*const char **/ version );
				[DllImport( "libsteam_api.so" )] internal static extern bool /*bool*/ SteamAPI_RestartAppIfNecessary( uint /*uint32*/ unOwnAppID );
				
			}
		}
	}
}
