using System;
using System.Runtime.InteropServices;

namespace SteamNative
{
	internal static partial class Platform
	{
		public class Mac : Interface
		{
			internal IntPtr _ptr;
			public bool IsValid { get{ return _ptr != null; } }
			
			//
			// Constructor sets pointer to native class
			//
			public Mac( IntPtr pointer )
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
				if ( _ptr == null ) throw new System.Exception( "ISteamClient _ptr is null!" );
				
				return Native.ISteamClient.CreateSteamPipe(_ptr);
			}
			public virtual bool /*bool*/ ISteamClient_BReleaseSteamPipe( int hSteamPipe )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamClient _ptr is null!" );
				
				return Native.ISteamClient.BReleaseSteamPipe(_ptr, hSteamPipe /*C*/);
			}
			public virtual HSteamUser /*(HSteamUser)*/ ISteamClient_ConnectToGlobalUser( int hSteamPipe )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamClient _ptr is null!" );
				
				return Native.ISteamClient.ConnectToGlobalUser(_ptr, hSteamPipe /*C*/);
			}
			public virtual HSteamUser /*(HSteamUser)*/ ISteamClient_CreateLocalUser( out int phSteamPipe, AccountType /*EAccountType*/ eAccountType )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamClient _ptr is null!" );
				
				return Native.ISteamClient.CreateLocalUser(_ptr, out phSteamPipe /*B*/, eAccountType /*C*/);
			}
			public virtual void /*void*/ ISteamClient_ReleaseUser( int hSteamPipe, int hUser )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamClient _ptr is null!" );
				
				Native.ISteamClient.ReleaseUser(_ptr, hSteamPipe /*C*/, hUser /*C*/);
			}
			public virtual IntPtr /*class ISteamUser **/ ISteamClient_GetISteamUser( int hSteamUser, int hSteamPipe, string /*const char **/ pchVersion )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamClient _ptr is null!" );
				
				return Native.ISteamClient.GetISteamUser(_ptr, hSteamUser /*C*/, hSteamPipe /*C*/, pchVersion /*C*/);
			}
			public virtual IntPtr /*class ISteamGameServer **/ ISteamClient_GetISteamGameServer( int hSteamUser, int hSteamPipe, string /*const char **/ pchVersion )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamClient _ptr is null!" );
				
				return Native.ISteamClient.GetISteamGameServer(_ptr, hSteamUser /*C*/, hSteamPipe /*C*/, pchVersion /*C*/);
			}
			public virtual void /*void*/ ISteamClient_SetLocalIPBinding( uint /*uint32*/ unIP, ushort /*uint16*/ usPort )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamClient _ptr is null!" );
				
				Native.ISteamClient.SetLocalIPBinding(_ptr, unIP /*C*/, usPort /*C*/);
			}
			public virtual IntPtr /*class ISteamFriends **/ ISteamClient_GetISteamFriends( int hSteamUser, int hSteamPipe, string /*const char **/ pchVersion )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamClient _ptr is null!" );
				
				return Native.ISteamClient.GetISteamFriends(_ptr, hSteamUser /*C*/, hSteamPipe /*C*/, pchVersion /*C*/);
			}
			public virtual IntPtr /*class ISteamUtils **/ ISteamClient_GetISteamUtils( int hSteamPipe, string /*const char **/ pchVersion )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamClient _ptr is null!" );
				
				return Native.ISteamClient.GetISteamUtils(_ptr, hSteamPipe /*C*/, pchVersion /*C*/);
			}
			public virtual IntPtr /*class ISteamMatchmaking **/ ISteamClient_GetISteamMatchmaking( int hSteamUser, int hSteamPipe, string /*const char **/ pchVersion )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamClient _ptr is null!" );
				
				return Native.ISteamClient.GetISteamMatchmaking(_ptr, hSteamUser /*C*/, hSteamPipe /*C*/, pchVersion /*C*/);
			}
			public virtual IntPtr /*class ISteamMatchmakingServers **/ ISteamClient_GetISteamMatchmakingServers( int hSteamUser, int hSteamPipe, string /*const char **/ pchVersion )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamClient _ptr is null!" );
				
				return Native.ISteamClient.GetISteamMatchmakingServers(_ptr, hSteamUser /*C*/, hSteamPipe /*C*/, pchVersion /*C*/);
			}
			public virtual IntPtr /*void **/ ISteamClient_GetISteamGenericInterface( int hSteamUser, int hSteamPipe, string /*const char **/ pchVersion )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamClient _ptr is null!" );
				
				return Native.ISteamClient.GetISteamGenericInterface(_ptr, hSteamUser /*C*/, hSteamPipe /*C*/, pchVersion /*C*/);
			}
			public virtual IntPtr /*class ISteamUserStats **/ ISteamClient_GetISteamUserStats( int hSteamUser, int hSteamPipe, string /*const char **/ pchVersion )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamClient _ptr is null!" );
				
				return Native.ISteamClient.GetISteamUserStats(_ptr, hSteamUser /*C*/, hSteamPipe /*C*/, pchVersion /*C*/);
			}
			public virtual IntPtr /*class ISteamGameServerStats **/ ISteamClient_GetISteamGameServerStats( int hSteamuser, int hSteamPipe, string /*const char **/ pchVersion )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamClient _ptr is null!" );
				
				return Native.ISteamClient.GetISteamGameServerStats(_ptr, hSteamuser /*C*/, hSteamPipe /*C*/, pchVersion /*C*/);
			}
			public virtual IntPtr /*class ISteamApps **/ ISteamClient_GetISteamApps( int hSteamUser, int hSteamPipe, string /*const char **/ pchVersion )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamClient _ptr is null!" );
				
				return Native.ISteamClient.GetISteamApps(_ptr, hSteamUser /*C*/, hSteamPipe /*C*/, pchVersion /*C*/);
			}
			public virtual IntPtr /*class ISteamNetworking **/ ISteamClient_GetISteamNetworking( int hSteamUser, int hSteamPipe, string /*const char **/ pchVersion )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamClient _ptr is null!" );
				
				return Native.ISteamClient.GetISteamNetworking(_ptr, hSteamUser /*C*/, hSteamPipe /*C*/, pchVersion /*C*/);
			}
			public virtual IntPtr /*class ISteamRemoteStorage **/ ISteamClient_GetISteamRemoteStorage( int hSteamuser, int hSteamPipe, string /*const char **/ pchVersion )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamClient _ptr is null!" );
				
				return Native.ISteamClient.GetISteamRemoteStorage(_ptr, hSteamuser /*C*/, hSteamPipe /*C*/, pchVersion /*C*/);
			}
			public virtual IntPtr /*class ISteamScreenshots **/ ISteamClient_GetISteamScreenshots( int hSteamuser, int hSteamPipe, string /*const char **/ pchVersion )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamClient _ptr is null!" );
				
				return Native.ISteamClient.GetISteamScreenshots(_ptr, hSteamuser /*C*/, hSteamPipe /*C*/, pchVersion /*C*/);
			}
			public virtual uint /*uint32*/ ISteamClient_GetIPCCallCount()
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamClient _ptr is null!" );
				
				return Native.ISteamClient.GetIPCCallCount(_ptr);
			}
			public virtual void /*void*/ ISteamClient_SetWarningMessageHook( IntPtr /*SteamAPIWarningMessageHook_t*/ pFunction )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamClient _ptr is null!" );
				
				Native.ISteamClient.SetWarningMessageHook(_ptr, pFunction /*C*/);
			}
			public virtual bool /*bool*/ ISteamClient_BShutdownIfAllPipesClosed()
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamClient _ptr is null!" );
				
				return Native.ISteamClient.BShutdownIfAllPipesClosed(_ptr);
			}
			public virtual IntPtr /*class ISteamHTTP **/ ISteamClient_GetISteamHTTP( int hSteamuser, int hSteamPipe, string /*const char **/ pchVersion )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamClient _ptr is null!" );
				
				return Native.ISteamClient.GetISteamHTTP(_ptr, hSteamuser /*C*/, hSteamPipe /*C*/, pchVersion /*C*/);
			}
			public virtual IntPtr /*class ISteamUnifiedMessages **/ ISteamClient_GetISteamUnifiedMessages( int hSteamuser, int hSteamPipe, string /*const char **/ pchVersion )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamClient _ptr is null!" );
				
				return Native.ISteamClient.GetISteamUnifiedMessages(_ptr, hSteamuser /*C*/, hSteamPipe /*C*/, pchVersion /*C*/);
			}
			public virtual IntPtr /*class ISteamController **/ ISteamClient_GetISteamController( int hSteamUser, int hSteamPipe, string /*const char **/ pchVersion )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamClient _ptr is null!" );
				
				return Native.ISteamClient.GetISteamController(_ptr, hSteamUser /*C*/, hSteamPipe /*C*/, pchVersion /*C*/);
			}
			public virtual IntPtr /*class ISteamUGC **/ ISteamClient_GetISteamUGC( int hSteamUser, int hSteamPipe, string /*const char **/ pchVersion )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamClient _ptr is null!" );
				
				return Native.ISteamClient.GetISteamUGC(_ptr, hSteamUser /*C*/, hSteamPipe /*C*/, pchVersion /*C*/);
			}
			public virtual IntPtr /*class ISteamAppList **/ ISteamClient_GetISteamAppList( int hSteamUser, int hSteamPipe, string /*const char **/ pchVersion )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamClient _ptr is null!" );
				
				return Native.ISteamClient.GetISteamAppList(_ptr, hSteamUser /*C*/, hSteamPipe /*C*/, pchVersion /*C*/);
			}
			public virtual IntPtr /*class ISteamMusic **/ ISteamClient_GetISteamMusic( int hSteamuser, int hSteamPipe, string /*const char **/ pchVersion )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamClient _ptr is null!" );
				
				return Native.ISteamClient.GetISteamMusic(_ptr, hSteamuser /*C*/, hSteamPipe /*C*/, pchVersion /*C*/);
			}
			public virtual IntPtr /*class ISteamMusicRemote **/ ISteamClient_GetISteamMusicRemote( int hSteamuser, int hSteamPipe, string /*const char **/ pchVersion )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamClient _ptr is null!" );
				
				return Native.ISteamClient.GetISteamMusicRemote(_ptr, hSteamuser /*C*/, hSteamPipe /*C*/, pchVersion /*C*/);
			}
			public virtual IntPtr /*class ISteamHTMLSurface **/ ISteamClient_GetISteamHTMLSurface( int hSteamuser, int hSteamPipe, string /*const char **/ pchVersion )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamClient _ptr is null!" );
				
				return Native.ISteamClient.GetISteamHTMLSurface(_ptr, hSteamuser /*C*/, hSteamPipe /*C*/, pchVersion /*C*/);
			}
			public virtual IntPtr /*class ISteamInventory **/ ISteamClient_GetISteamInventory( int hSteamuser, int hSteamPipe, string /*const char **/ pchVersion )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamClient _ptr is null!" );
				
				return Native.ISteamClient.GetISteamInventory(_ptr, hSteamuser /*C*/, hSteamPipe /*C*/, pchVersion /*C*/);
			}
			public virtual IntPtr /*class ISteamVideo **/ ISteamClient_GetISteamVideo( int hSteamuser, int hSteamPipe, string /*const char **/ pchVersion )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamClient _ptr is null!" );
				
				return Native.ISteamClient.GetISteamVideo(_ptr, hSteamuser /*C*/, hSteamPipe /*C*/, pchVersion /*C*/);
			}
			
			public virtual HSteamUser /*(HSteamUser)*/ ISteamUser_GetHSteamUser()
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamUser _ptr is null!" );
				
				return Native.ISteamUser.GetHSteamUser(_ptr);
			}
			public virtual bool /*bool*/ ISteamUser_BLoggedOn()
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamUser _ptr is null!" );
				
				return Native.ISteamUser.BLoggedOn(_ptr);
			}
			public virtual CSteamID /*(class CSteamID)*/ ISteamUser_GetSteamID()
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamUser _ptr is null!" );
				
				return Native.ISteamUser.GetSteamID(_ptr);
			}
			public virtual int /*int*/ ISteamUser_InitiateGameConnection( IntPtr /*void **/ pAuthBlob, int /*int*/ cbMaxAuthBlob, ulong steamIDGameServer, uint /*uint32*/ unIPServer, ushort /*uint16*/ usPortServer, [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bSecure )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamUser _ptr is null!" );
				
				return Native.ISteamUser.InitiateGameConnection(_ptr, pAuthBlob /*C*/, cbMaxAuthBlob /*C*/, steamIDGameServer /*C*/, unIPServer /*C*/, usPortServer /*C*/, bSecure /*C*/);
			}
			public virtual void /*void*/ ISteamUser_TerminateGameConnection( uint /*uint32*/ unIPServer, ushort /*uint16*/ usPortServer )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamUser _ptr is null!" );
				
				Native.ISteamUser.TerminateGameConnection(_ptr, unIPServer /*C*/, usPortServer /*C*/);
			}
			public virtual void /*void*/ ISteamUser_TrackAppUsageEvent( ulong gameID, int /*int*/ eAppUsageEvent, string /*const char **/ pchExtraInfo )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamUser _ptr is null!" );
				
				Native.ISteamUser.TrackAppUsageEvent(_ptr, gameID /*C*/, eAppUsageEvent /*C*/, pchExtraInfo /*C*/);
			}
			public virtual bool /*bool*/ ISteamUser_GetUserDataFolder( System.Text.StringBuilder /*char **/ pchBuffer, int /*int*/ cubBuffer )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamUser _ptr is null!" );
				
				return Native.ISteamUser.GetUserDataFolder(_ptr, pchBuffer /*C*/, cubBuffer /*C*/);
			}
			public virtual void /*void*/ ISteamUser_StartVoiceRecording()
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamUser _ptr is null!" );
				
				Native.ISteamUser.StartVoiceRecording(_ptr);
			}
			public virtual void /*void*/ ISteamUser_StopVoiceRecording()
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamUser _ptr is null!" );
				
				Native.ISteamUser.StopVoiceRecording(_ptr);
			}
			public virtual VoiceResult /*EVoiceResult*/ ISteamUser_GetAvailableVoice( out uint /*uint32 **/ pcbCompressed, out uint /*uint32 **/ pcbUncompressed, uint /*uint32*/ nUncompressedVoiceDesiredSampleRate )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamUser _ptr is null!" );
				
				return Native.ISteamUser.GetAvailableVoice(_ptr, out pcbCompressed /*B*/, out pcbUncompressed /*B*/, nUncompressedVoiceDesiredSampleRate /*C*/);
			}
			public virtual VoiceResult /*EVoiceResult*/ ISteamUser_GetVoice( [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bWantCompressed, IntPtr /*void **/ pDestBuffer, uint /*uint32*/ cbDestBufferSize, out uint /*uint32 **/ nBytesWritten, [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bWantUncompressed, IntPtr /*void **/ pUncompressedDestBuffer, uint /*uint32*/ cbUncompressedDestBufferSize, out uint /*uint32 **/ nUncompressBytesWritten, uint /*uint32*/ nUncompressedVoiceDesiredSampleRate )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamUser _ptr is null!" );
				
				return Native.ISteamUser.GetVoice(_ptr, bWantCompressed /*C*/, pDestBuffer /*C*/, cbDestBufferSize /*C*/, out nBytesWritten /*B*/, bWantUncompressed /*C*/, pUncompressedDestBuffer /*C*/, cbUncompressedDestBufferSize /*C*/, out nUncompressBytesWritten /*B*/, nUncompressedVoiceDesiredSampleRate /*C*/);
			}
			public virtual VoiceResult /*EVoiceResult*/ ISteamUser_DecompressVoice( IntPtr /*const void **/ pCompressed, uint /*uint32*/ cbCompressed, IntPtr /*void **/ pDestBuffer, uint /*uint32*/ cbDestBufferSize, out uint /*uint32 **/ nBytesWritten, uint /*uint32*/ nDesiredSampleRate )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamUser _ptr is null!" );
				
				return Native.ISteamUser.DecompressVoice(_ptr, pCompressed /*C*/, cbCompressed /*C*/, pDestBuffer /*C*/, cbDestBufferSize /*C*/, out nBytesWritten /*B*/, nDesiredSampleRate /*C*/);
			}
			public virtual uint /*uint32*/ ISteamUser_GetVoiceOptimalSampleRate()
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamUser _ptr is null!" );
				
				return Native.ISteamUser.GetVoiceOptimalSampleRate(_ptr);
			}
			public virtual HAuthTicket /*(HAuthTicket)*/ ISteamUser_GetAuthSessionTicket( IntPtr /*void **/ pTicket, int /*int*/ cbMaxTicket, out uint /*uint32 **/ pcbTicket )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamUser _ptr is null!" );
				
				return Native.ISteamUser.GetAuthSessionTicket(_ptr, pTicket /*C*/, cbMaxTicket /*C*/, out pcbTicket /*B*/);
			}
			public virtual BeginAuthSessionResult /*EBeginAuthSessionResult*/ ISteamUser_BeginAuthSession( IntPtr /*const void **/ pAuthTicket, int /*int*/ cbAuthTicket, ulong steamID )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamUser _ptr is null!" );
				
				return Native.ISteamUser.BeginAuthSession(_ptr, pAuthTicket /*C*/, cbAuthTicket /*C*/, steamID /*C*/);
			}
			public virtual void /*void*/ ISteamUser_EndAuthSession( ulong steamID )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamUser _ptr is null!" );
				
				Native.ISteamUser.EndAuthSession(_ptr, steamID /*C*/);
			}
			public virtual void /*void*/ ISteamUser_CancelAuthTicket( uint hAuthTicket )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamUser _ptr is null!" );
				
				Native.ISteamUser.CancelAuthTicket(_ptr, hAuthTicket /*C*/);
			}
			public virtual UserHasLicenseForAppResult /*EUserHasLicenseForAppResult*/ ISteamUser_UserHasLicenseForApp( ulong steamID, uint appID )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamUser _ptr is null!" );
				
				return Native.ISteamUser.UserHasLicenseForApp(_ptr, steamID /*C*/, appID /*C*/);
			}
			public virtual bool /*bool*/ ISteamUser_BIsBehindNAT()
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamUser _ptr is null!" );
				
				return Native.ISteamUser.BIsBehindNAT(_ptr);
			}
			public virtual void /*void*/ ISteamUser_AdvertiseGame( ulong steamIDGameServer, uint /*uint32*/ unIPServer, ushort /*uint16*/ usPortServer )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamUser _ptr is null!" );
				
				Native.ISteamUser.AdvertiseGame(_ptr, steamIDGameServer /*C*/, unIPServer /*C*/, usPortServer /*C*/);
			}
			public virtual SteamAPICall_t /*(SteamAPICall_t)*/ ISteamUser_RequestEncryptedAppTicket( IntPtr /*void **/ pDataToInclude, int /*int*/ cbDataToInclude )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamUser _ptr is null!" );
				
				return Native.ISteamUser.RequestEncryptedAppTicket(_ptr, pDataToInclude /*C*/, cbDataToInclude /*C*/);
			}
			public virtual bool /*bool*/ ISteamUser_GetEncryptedAppTicket( IntPtr /*void **/ pTicket, int /*int*/ cbMaxTicket, out uint /*uint32 **/ pcbTicket )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamUser _ptr is null!" );
				
				return Native.ISteamUser.GetEncryptedAppTicket(_ptr, pTicket /*C*/, cbMaxTicket /*C*/, out pcbTicket /*B*/);
			}
			public virtual int /*int*/ ISteamUser_GetGameBadgeLevel( int /*int*/ nSeries, [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bFoil )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamUser _ptr is null!" );
				
				return Native.ISteamUser.GetGameBadgeLevel(_ptr, nSeries /*C*/, bFoil /*C*/);
			}
			public virtual int /*int*/ ISteamUser_GetPlayerSteamLevel()
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamUser _ptr is null!" );
				
				return Native.ISteamUser.GetPlayerSteamLevel(_ptr);
			}
			public virtual SteamAPICall_t /*(SteamAPICall_t)*/ ISteamUser_RequestStoreAuthURL( string /*const char **/ pchRedirectURL )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamUser _ptr is null!" );
				
				return Native.ISteamUser.RequestStoreAuthURL(_ptr, pchRedirectURL /*C*/);
			}
			public virtual bool /*bool*/ ISteamUser_BIsPhoneVerified()
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamUser _ptr is null!" );
				
				return Native.ISteamUser.BIsPhoneVerified(_ptr);
			}
			public virtual bool /*bool*/ ISteamUser_BIsTwoFactorEnabled()
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamUser _ptr is null!" );
				
				return Native.ISteamUser.BIsTwoFactorEnabled(_ptr);
			}
			
			public virtual IntPtr ISteamFriends_GetPersonaName()
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamFriends _ptr is null!" );
				
				return Native.ISteamFriends.GetPersonaName(_ptr);
			}
			public virtual SteamAPICall_t /*(SteamAPICall_t)*/ ISteamFriends_SetPersonaName( string /*const char **/ pchPersonaName )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamFriends _ptr is null!" );
				
				return Native.ISteamFriends.SetPersonaName(_ptr, pchPersonaName /*C*/);
			}
			public virtual PersonaState /*EPersonaState*/ ISteamFriends_GetPersonaState()
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamFriends _ptr is null!" );
				
				return Native.ISteamFriends.GetPersonaState(_ptr);
			}
			public virtual int /*int*/ ISteamFriends_GetFriendCount( int /*int*/ iFriendFlags )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamFriends _ptr is null!" );
				
				return Native.ISteamFriends.GetFriendCount(_ptr, iFriendFlags /*C*/);
			}
			public virtual CSteamID /*(class CSteamID)*/ ISteamFriends_GetFriendByIndex( int /*int*/ iFriend, int /*int*/ iFriendFlags )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamFriends _ptr is null!" );
				
				return Native.ISteamFriends.GetFriendByIndex(_ptr, iFriend /*C*/, iFriendFlags /*C*/);
			}
			public virtual FriendRelationship /*EFriendRelationship*/ ISteamFriends_GetFriendRelationship( ulong steamIDFriend )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamFriends _ptr is null!" );
				
				return Native.ISteamFriends.GetFriendRelationship(_ptr, steamIDFriend /*C*/);
			}
			public virtual PersonaState /*EPersonaState*/ ISteamFriends_GetFriendPersonaState( ulong steamIDFriend )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamFriends _ptr is null!" );
				
				return Native.ISteamFriends.GetFriendPersonaState(_ptr, steamIDFriend /*C*/);
			}
			public virtual IntPtr ISteamFriends_GetFriendPersonaName( ulong steamIDFriend )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamFriends _ptr is null!" );
				
				return Native.ISteamFriends.GetFriendPersonaName(_ptr, steamIDFriend /*C*/);
			}
			public virtual bool /*bool*/ ISteamFriends_GetFriendGamePlayed( ulong steamIDFriend, ref FriendGameInfo_t /*struct FriendGameInfo_t **/ pFriendGameInfo )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamFriends _ptr is null!" );
				
				var pFriendGameInfo_ps = new FriendGameInfo_t.PackSmall();
				var ret = Native.ISteamFriends.GetFriendGamePlayed(_ptr, steamIDFriend /*C*/, ref pFriendGameInfo_ps);
				pFriendGameInfo = pFriendGameInfo_ps;
				return ret;
			}
			public virtual IntPtr ISteamFriends_GetFriendPersonaNameHistory( ulong steamIDFriend, int /*int*/ iPersonaName )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamFriends _ptr is null!" );
				
				return Native.ISteamFriends.GetFriendPersonaNameHistory(_ptr, steamIDFriend /*C*/, iPersonaName /*C*/);
			}
			public virtual int /*int*/ ISteamFriends_GetFriendSteamLevel( ulong steamIDFriend )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamFriends _ptr is null!" );
				
				return Native.ISteamFriends.GetFriendSteamLevel(_ptr, steamIDFriend /*C*/);
			}
			public virtual IntPtr ISteamFriends_GetPlayerNickname( ulong steamIDPlayer )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamFriends _ptr is null!" );
				
				return Native.ISteamFriends.GetPlayerNickname(_ptr, steamIDPlayer /*C*/);
			}
			public virtual int /*int*/ ISteamFriends_GetFriendsGroupCount()
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamFriends _ptr is null!" );
				
				return Native.ISteamFriends.GetFriendsGroupCount(_ptr);
			}
			public virtual FriendsGroupID_t /*(FriendsGroupID_t)*/ ISteamFriends_GetFriendsGroupIDByIndex( int /*int*/ iFG )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamFriends _ptr is null!" );
				
				return Native.ISteamFriends.GetFriendsGroupIDByIndex(_ptr, iFG /*C*/);
			}
			public virtual IntPtr ISteamFriends_GetFriendsGroupName( short friendsGroupID )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamFriends _ptr is null!" );
				
				return Native.ISteamFriends.GetFriendsGroupName(_ptr, friendsGroupID /*C*/);
			}
			public virtual int /*int*/ ISteamFriends_GetFriendsGroupMembersCount( short friendsGroupID )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamFriends _ptr is null!" );
				
				return Native.ISteamFriends.GetFriendsGroupMembersCount(_ptr, friendsGroupID /*C*/);
			}
			public virtual void /*void*/ ISteamFriends_GetFriendsGroupMembersList( short friendsGroupID, IntPtr /*class CSteamID **/ pOutSteamIDMembers, int /*int*/ nMembersCount )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamFriends _ptr is null!" );
				
				Native.ISteamFriends.GetFriendsGroupMembersList(_ptr, friendsGroupID /*C*/, pOutSteamIDMembers, nMembersCount /*C*/);
			}
			public virtual bool /*bool*/ ISteamFriends_HasFriend( ulong steamIDFriend, int /*int*/ iFriendFlags )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamFriends _ptr is null!" );
				
				return Native.ISteamFriends.HasFriend(_ptr, steamIDFriend /*C*/, iFriendFlags /*C*/);
			}
			public virtual int /*int*/ ISteamFriends_GetClanCount()
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamFriends _ptr is null!" );
				
				return Native.ISteamFriends.GetClanCount(_ptr);
			}
			public virtual CSteamID /*(class CSteamID)*/ ISteamFriends_GetClanByIndex( int /*int*/ iClan )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamFriends _ptr is null!" );
				
				return Native.ISteamFriends.GetClanByIndex(_ptr, iClan /*C*/);
			}
			public virtual IntPtr ISteamFriends_GetClanName( ulong steamIDClan )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamFriends _ptr is null!" );
				
				return Native.ISteamFriends.GetClanName(_ptr, steamIDClan /*C*/);
			}
			public virtual IntPtr ISteamFriends_GetClanTag( ulong steamIDClan )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamFriends _ptr is null!" );
				
				return Native.ISteamFriends.GetClanTag(_ptr, steamIDClan /*C*/);
			}
			public virtual bool /*bool*/ ISteamFriends_GetClanActivityCounts( ulong steamIDClan, out int /*int **/ pnOnline, out int /*int **/ pnInGame, out int /*int **/ pnChatting )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamFriends _ptr is null!" );
				
				return Native.ISteamFriends.GetClanActivityCounts(_ptr, steamIDClan /*C*/, out pnOnline /*B*/, out pnInGame /*B*/, out pnChatting /*B*/);
			}
			public virtual SteamAPICall_t /*(SteamAPICall_t)*/ ISteamFriends_DownloadClanActivityCounts( IntPtr /*class CSteamID **/ psteamIDClans, int /*int*/ cClansToRequest )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamFriends _ptr is null!" );
				
				return Native.ISteamFriends.DownloadClanActivityCounts(_ptr, psteamIDClans, cClansToRequest /*C*/);
			}
			public virtual int /*int*/ ISteamFriends_GetFriendCountFromSource( ulong steamIDSource )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamFriends _ptr is null!" );
				
				return Native.ISteamFriends.GetFriendCountFromSource(_ptr, steamIDSource /*C*/);
			}
			public virtual CSteamID /*(class CSteamID)*/ ISteamFriends_GetFriendFromSourceByIndex( ulong steamIDSource, int /*int*/ iFriend )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamFriends _ptr is null!" );
				
				return Native.ISteamFriends.GetFriendFromSourceByIndex(_ptr, steamIDSource /*C*/, iFriend /*C*/);
			}
			public virtual bool /*bool*/ ISteamFriends_IsUserInSource( ulong steamIDUser, ulong steamIDSource )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamFriends _ptr is null!" );
				
				return Native.ISteamFriends.IsUserInSource(_ptr, steamIDUser /*C*/, steamIDSource /*C*/);
			}
			public virtual void /*void*/ ISteamFriends_SetInGameVoiceSpeaking( ulong steamIDUser, [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bSpeaking )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamFriends _ptr is null!" );
				
				Native.ISteamFriends.SetInGameVoiceSpeaking(_ptr, steamIDUser /*C*/, bSpeaking /*C*/);
			}
			public virtual void /*void*/ ISteamFriends_ActivateGameOverlay( string /*const char **/ pchDialog )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamFriends _ptr is null!" );
				
				Native.ISteamFriends.ActivateGameOverlay(_ptr, pchDialog /*C*/);
			}
			public virtual void /*void*/ ISteamFriends_ActivateGameOverlayToUser( string /*const char **/ pchDialog, ulong steamID )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamFriends _ptr is null!" );
				
				Native.ISteamFriends.ActivateGameOverlayToUser(_ptr, pchDialog /*C*/, steamID /*C*/);
			}
			public virtual void /*void*/ ISteamFriends_ActivateGameOverlayToWebPage( string /*const char **/ pchURL )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamFriends _ptr is null!" );
				
				Native.ISteamFriends.ActivateGameOverlayToWebPage(_ptr, pchURL /*C*/);
			}
			public virtual void /*void*/ ISteamFriends_ActivateGameOverlayToStore( uint nAppID, OverlayToStoreFlag /*EOverlayToStoreFlag*/ eFlag )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamFriends _ptr is null!" );
				
				Native.ISteamFriends.ActivateGameOverlayToStore(_ptr, nAppID /*C*/, eFlag /*C*/);
			}
			public virtual void /*void*/ ISteamFriends_SetPlayedWith( ulong steamIDUserPlayedWith )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamFriends _ptr is null!" );
				
				Native.ISteamFriends.SetPlayedWith(_ptr, steamIDUserPlayedWith /*C*/);
			}
			public virtual void /*void*/ ISteamFriends_ActivateGameOverlayInviteDialog( ulong steamIDLobby )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamFriends _ptr is null!" );
				
				Native.ISteamFriends.ActivateGameOverlayInviteDialog(_ptr, steamIDLobby /*C*/);
			}
			public virtual int /*int*/ ISteamFriends_GetSmallFriendAvatar( ulong steamIDFriend )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamFriends _ptr is null!" );
				
				return Native.ISteamFriends.GetSmallFriendAvatar(_ptr, steamIDFriend /*C*/);
			}
			public virtual int /*int*/ ISteamFriends_GetMediumFriendAvatar( ulong steamIDFriend )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamFriends _ptr is null!" );
				
				return Native.ISteamFriends.GetMediumFriendAvatar(_ptr, steamIDFriend /*C*/);
			}
			public virtual int /*int*/ ISteamFriends_GetLargeFriendAvatar( ulong steamIDFriend )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamFriends _ptr is null!" );
				
				return Native.ISteamFriends.GetLargeFriendAvatar(_ptr, steamIDFriend /*C*/);
			}
			public virtual bool /*bool*/ ISteamFriends_RequestUserInformation( ulong steamIDUser, [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bRequireNameOnly )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamFriends _ptr is null!" );
				
				return Native.ISteamFriends.RequestUserInformation(_ptr, steamIDUser /*C*/, bRequireNameOnly /*C*/);
			}
			public virtual SteamAPICall_t /*(SteamAPICall_t)*/ ISteamFriends_RequestClanOfficerList( ulong steamIDClan )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamFriends _ptr is null!" );
				
				return Native.ISteamFriends.RequestClanOfficerList(_ptr, steamIDClan /*C*/);
			}
			public virtual CSteamID /*(class CSteamID)*/ ISteamFriends_GetClanOwner( ulong steamIDClan )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamFriends _ptr is null!" );
				
				return Native.ISteamFriends.GetClanOwner(_ptr, steamIDClan /*C*/);
			}
			public virtual int /*int*/ ISteamFriends_GetClanOfficerCount( ulong steamIDClan )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamFriends _ptr is null!" );
				
				return Native.ISteamFriends.GetClanOfficerCount(_ptr, steamIDClan /*C*/);
			}
			public virtual CSteamID /*(class CSteamID)*/ ISteamFriends_GetClanOfficerByIndex( ulong steamIDClan, int /*int*/ iOfficer )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamFriends _ptr is null!" );
				
				return Native.ISteamFriends.GetClanOfficerByIndex(_ptr, steamIDClan /*C*/, iOfficer /*C*/);
			}
			public virtual uint /*uint32*/ ISteamFriends_GetUserRestrictions()
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamFriends _ptr is null!" );
				
				return Native.ISteamFriends.GetUserRestrictions(_ptr);
			}
			public virtual bool /*bool*/ ISteamFriends_SetRichPresence( string /*const char **/ pchKey, string /*const char **/ pchValue )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamFriends _ptr is null!" );
				
				return Native.ISteamFriends.SetRichPresence(_ptr, pchKey /*C*/, pchValue /*C*/);
			}
			public virtual void /*void*/ ISteamFriends_ClearRichPresence()
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamFriends _ptr is null!" );
				
				Native.ISteamFriends.ClearRichPresence(_ptr);
			}
			public virtual IntPtr ISteamFriends_GetFriendRichPresence( ulong steamIDFriend, string /*const char **/ pchKey )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamFriends _ptr is null!" );
				
				return Native.ISteamFriends.GetFriendRichPresence(_ptr, steamIDFriend /*C*/, pchKey /*C*/);
			}
			public virtual int /*int*/ ISteamFriends_GetFriendRichPresenceKeyCount( ulong steamIDFriend )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamFriends _ptr is null!" );
				
				return Native.ISteamFriends.GetFriendRichPresenceKeyCount(_ptr, steamIDFriend /*C*/);
			}
			public virtual IntPtr ISteamFriends_GetFriendRichPresenceKeyByIndex( ulong steamIDFriend, int /*int*/ iKey )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamFriends _ptr is null!" );
				
				return Native.ISteamFriends.GetFriendRichPresenceKeyByIndex(_ptr, steamIDFriend /*C*/, iKey /*C*/);
			}
			public virtual void /*void*/ ISteamFriends_RequestFriendRichPresence( ulong steamIDFriend )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamFriends _ptr is null!" );
				
				Native.ISteamFriends.RequestFriendRichPresence(_ptr, steamIDFriend /*C*/);
			}
			public virtual bool /*bool*/ ISteamFriends_InviteUserToGame( ulong steamIDFriend, string /*const char **/ pchConnectString )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamFriends _ptr is null!" );
				
				return Native.ISteamFriends.InviteUserToGame(_ptr, steamIDFriend /*C*/, pchConnectString /*C*/);
			}
			public virtual int /*int*/ ISteamFriends_GetCoplayFriendCount()
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamFriends _ptr is null!" );
				
				return Native.ISteamFriends.GetCoplayFriendCount(_ptr);
			}
			public virtual CSteamID /*(class CSteamID)*/ ISteamFriends_GetCoplayFriend( int /*int*/ iCoplayFriend )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamFriends _ptr is null!" );
				
				return Native.ISteamFriends.GetCoplayFriend(_ptr, iCoplayFriend /*C*/);
			}
			public virtual int /*int*/ ISteamFriends_GetFriendCoplayTime( ulong steamIDFriend )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamFriends _ptr is null!" );
				
				return Native.ISteamFriends.GetFriendCoplayTime(_ptr, steamIDFriend /*C*/);
			}
			public virtual AppId_t /*(AppId_t)*/ ISteamFriends_GetFriendCoplayGame( ulong steamIDFriend )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamFriends _ptr is null!" );
				
				return Native.ISteamFriends.GetFriendCoplayGame(_ptr, steamIDFriend /*C*/);
			}
			public virtual SteamAPICall_t /*(SteamAPICall_t)*/ ISteamFriends_JoinClanChatRoom( ulong steamIDClan )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamFriends _ptr is null!" );
				
				return Native.ISteamFriends.JoinClanChatRoom(_ptr, steamIDClan /*C*/);
			}
			public virtual bool /*bool*/ ISteamFriends_LeaveClanChatRoom( ulong steamIDClan )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamFriends _ptr is null!" );
				
				return Native.ISteamFriends.LeaveClanChatRoom(_ptr, steamIDClan /*C*/);
			}
			public virtual int /*int*/ ISteamFriends_GetClanChatMemberCount( ulong steamIDClan )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamFriends _ptr is null!" );
				
				return Native.ISteamFriends.GetClanChatMemberCount(_ptr, steamIDClan /*C*/);
			}
			public virtual CSteamID /*(class CSteamID)*/ ISteamFriends_GetChatMemberByIndex( ulong steamIDClan, int /*int*/ iUser )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamFriends _ptr is null!" );
				
				return Native.ISteamFriends.GetChatMemberByIndex(_ptr, steamIDClan /*C*/, iUser /*C*/);
			}
			public virtual bool /*bool*/ ISteamFriends_SendClanChatMessage( ulong steamIDClanChat, string /*const char **/ pchText )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamFriends _ptr is null!" );
				
				return Native.ISteamFriends.SendClanChatMessage(_ptr, steamIDClanChat /*C*/, pchText /*C*/);
			}
			public virtual int /*int*/ ISteamFriends_GetClanChatMessage( ulong steamIDClanChat, int /*int*/ iMessage, IntPtr /*void **/ prgchText, int /*int*/ cchTextMax, out ChatEntryType /*EChatEntryType **/ peChatEntryType, out ulong psteamidChatter )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamFriends _ptr is null!" );
				
				return Native.ISteamFriends.GetClanChatMessage(_ptr, steamIDClanChat /*C*/, iMessage /*C*/, prgchText /*C*/, cchTextMax /*C*/, out peChatEntryType /*B*/, out psteamidChatter /*B*/);
			}
			public virtual bool /*bool*/ ISteamFriends_IsClanChatAdmin( ulong steamIDClanChat, ulong steamIDUser )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamFriends _ptr is null!" );
				
				return Native.ISteamFriends.IsClanChatAdmin(_ptr, steamIDClanChat /*C*/, steamIDUser /*C*/);
			}
			public virtual bool /*bool*/ ISteamFriends_IsClanChatWindowOpenInSteam( ulong steamIDClanChat )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamFriends _ptr is null!" );
				
				return Native.ISteamFriends.IsClanChatWindowOpenInSteam(_ptr, steamIDClanChat /*C*/);
			}
			public virtual bool /*bool*/ ISteamFriends_OpenClanChatWindowInSteam( ulong steamIDClanChat )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamFriends _ptr is null!" );
				
				return Native.ISteamFriends.OpenClanChatWindowInSteam(_ptr, steamIDClanChat /*C*/);
			}
			public virtual bool /*bool*/ ISteamFriends_CloseClanChatWindowInSteam( ulong steamIDClanChat )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamFriends _ptr is null!" );
				
				return Native.ISteamFriends.CloseClanChatWindowInSteam(_ptr, steamIDClanChat /*C*/);
			}
			public virtual bool /*bool*/ ISteamFriends_SetListenForFriendsMessages( [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bInterceptEnabled )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamFriends _ptr is null!" );
				
				return Native.ISteamFriends.SetListenForFriendsMessages(_ptr, bInterceptEnabled /*C*/);
			}
			public virtual bool /*bool*/ ISteamFriends_ReplyToFriendMessage( ulong steamIDFriend, string /*const char **/ pchMsgToSend )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamFriends _ptr is null!" );
				
				return Native.ISteamFriends.ReplyToFriendMessage(_ptr, steamIDFriend /*C*/, pchMsgToSend /*C*/);
			}
			public virtual int /*int*/ ISteamFriends_GetFriendMessage( ulong steamIDFriend, int /*int*/ iMessageID, IntPtr /*void **/ pvData, int /*int*/ cubData, out ChatEntryType /*EChatEntryType **/ peChatEntryType )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamFriends _ptr is null!" );
				
				return Native.ISteamFriends.GetFriendMessage(_ptr, steamIDFriend /*C*/, iMessageID /*C*/, pvData /*C*/, cubData /*C*/, out peChatEntryType /*B*/);
			}
			public virtual SteamAPICall_t /*(SteamAPICall_t)*/ ISteamFriends_GetFollowerCount( ulong steamID )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamFriends _ptr is null!" );
				
				return Native.ISteamFriends.GetFollowerCount(_ptr, steamID /*C*/);
			}
			public virtual SteamAPICall_t /*(SteamAPICall_t)*/ ISteamFriends_IsFollowing( ulong steamID )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamFriends _ptr is null!" );
				
				return Native.ISteamFriends.IsFollowing(_ptr, steamID /*C*/);
			}
			public virtual SteamAPICall_t /*(SteamAPICall_t)*/ ISteamFriends_EnumerateFollowingList( uint /*uint32*/ unStartIndex )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamFriends _ptr is null!" );
				
				return Native.ISteamFriends.EnumerateFollowingList(_ptr, unStartIndex /*C*/);
			}
			
			public virtual uint /*uint32*/ ISteamUtils_GetSecondsSinceAppActive()
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamUtils _ptr is null!" );
				
				return Native.ISteamUtils.GetSecondsSinceAppActive(_ptr);
			}
			public virtual uint /*uint32*/ ISteamUtils_GetSecondsSinceComputerActive()
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamUtils _ptr is null!" );
				
				return Native.ISteamUtils.GetSecondsSinceComputerActive(_ptr);
			}
			public virtual Universe /*EUniverse*/ ISteamUtils_GetConnectedUniverse()
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamUtils _ptr is null!" );
				
				return Native.ISteamUtils.GetConnectedUniverse(_ptr);
			}
			public virtual uint /*uint32*/ ISteamUtils_GetServerRealTime()
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamUtils _ptr is null!" );
				
				return Native.ISteamUtils.GetServerRealTime(_ptr);
			}
			public virtual IntPtr ISteamUtils_GetIPCountry()
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamUtils _ptr is null!" );
				
				return Native.ISteamUtils.GetIPCountry(_ptr);
			}
			public virtual bool /*bool*/ ISteamUtils_GetImageSize( int /*int*/ iImage, out uint /*uint32 **/ pnWidth, out uint /*uint32 **/ pnHeight )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamUtils _ptr is null!" );
				
				return Native.ISteamUtils.GetImageSize(_ptr, iImage /*C*/, out pnWidth /*B*/, out pnHeight /*B*/);
			}
			public virtual bool /*bool*/ ISteamUtils_GetImageRGBA( int /*int*/ iImage, IntPtr /*uint8 **/ pubDest, int /*int*/ nDestBufferSize )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamUtils _ptr is null!" );
				
				return Native.ISteamUtils.GetImageRGBA(_ptr, iImage /*C*/, pubDest, nDestBufferSize /*C*/);
			}
			public virtual bool /*bool*/ ISteamUtils_GetCSERIPPort( out uint /*uint32 **/ unIP, out ushort /*uint16 **/ usPort )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamUtils _ptr is null!" );
				
				return Native.ISteamUtils.GetCSERIPPort(_ptr, out unIP /*B*/, out usPort /*B*/);
			}
			public virtual byte /*uint8*/ ISteamUtils_GetCurrentBatteryPower()
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamUtils _ptr is null!" );
				
				return Native.ISteamUtils.GetCurrentBatteryPower(_ptr);
			}
			public virtual uint /*uint32*/ ISteamUtils_GetAppID()
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamUtils _ptr is null!" );
				
				return Native.ISteamUtils.GetAppID(_ptr);
			}
			public virtual void /*void*/ ISteamUtils_SetOverlayNotificationPosition( NotificationPosition /*ENotificationPosition*/ eNotificationPosition )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamUtils _ptr is null!" );
				
				Native.ISteamUtils.SetOverlayNotificationPosition(_ptr, eNotificationPosition /*C*/);
			}
			public virtual bool /*bool*/ ISteamUtils_IsAPICallCompleted( ulong hSteamAPICall, [MarshalAs(UnmanagedType.U1)] ref bool /*bool **/ pbFailed )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamUtils _ptr is null!" );
				
				return Native.ISteamUtils.IsAPICallCompleted(_ptr, hSteamAPICall /*C*/, ref pbFailed /*A*/);
			}
			public virtual SteamAPICallFailure /*ESteamAPICallFailure*/ ISteamUtils_GetAPICallFailureReason( ulong hSteamAPICall )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamUtils _ptr is null!" );
				
				return Native.ISteamUtils.GetAPICallFailureReason(_ptr, hSteamAPICall /*C*/);
			}
			public virtual bool /*bool*/ ISteamUtils_GetAPICallResult( ulong hSteamAPICall, IntPtr /*void **/ pCallback, int /*int*/ cubCallback, int /*int*/ iCallbackExpected, [MarshalAs(UnmanagedType.U1)] ref bool /*bool **/ pbFailed )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamUtils _ptr is null!" );
				
				return Native.ISteamUtils.GetAPICallResult(_ptr, hSteamAPICall /*C*/, pCallback /*C*/, cubCallback /*C*/, iCallbackExpected /*C*/, ref pbFailed /*A*/);
			}
			public virtual uint /*uint32*/ ISteamUtils_GetIPCCallCount()
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamUtils _ptr is null!" );
				
				return Native.ISteamUtils.GetIPCCallCount(_ptr);
			}
			public virtual void /*void*/ ISteamUtils_SetWarningMessageHook( IntPtr /*SteamAPIWarningMessageHook_t*/ pFunction )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamUtils _ptr is null!" );
				
				Native.ISteamUtils.SetWarningMessageHook(_ptr, pFunction /*C*/);
			}
			public virtual bool /*bool*/ ISteamUtils_IsOverlayEnabled()
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamUtils _ptr is null!" );
				
				return Native.ISteamUtils.IsOverlayEnabled(_ptr);
			}
			public virtual bool /*bool*/ ISteamUtils_BOverlayNeedsPresent()
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamUtils _ptr is null!" );
				
				return Native.ISteamUtils.BOverlayNeedsPresent(_ptr);
			}
			public virtual SteamAPICall_t /*(SteamAPICall_t)*/ ISteamUtils_CheckFileSignature( string /*const char **/ szFileName )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamUtils _ptr is null!" );
				
				return Native.ISteamUtils.CheckFileSignature(_ptr, szFileName /*C*/);
			}
			public virtual bool /*bool*/ ISteamUtils_ShowGamepadTextInput( GamepadTextInputMode /*EGamepadTextInputMode*/ eInputMode, GamepadTextInputLineMode /*EGamepadTextInputLineMode*/ eLineInputMode, string /*const char **/ pchDescription, uint /*uint32*/ unCharMax, string /*const char **/ pchExistingText )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamUtils _ptr is null!" );
				
				return Native.ISteamUtils.ShowGamepadTextInput(_ptr, eInputMode /*C*/, eLineInputMode /*C*/, pchDescription /*C*/, unCharMax /*C*/, pchExistingText /*C*/);
			}
			public virtual uint /*uint32*/ ISteamUtils_GetEnteredGamepadTextLength()
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamUtils _ptr is null!" );
				
				return Native.ISteamUtils.GetEnteredGamepadTextLength(_ptr);
			}
			public virtual bool /*bool*/ ISteamUtils_GetEnteredGamepadTextInput( System.Text.StringBuilder /*char **/ pchText, uint /*uint32*/ cchText )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamUtils _ptr is null!" );
				
				return Native.ISteamUtils.GetEnteredGamepadTextInput(_ptr, pchText /*C*/, cchText /*C*/);
			}
			public virtual IntPtr ISteamUtils_GetSteamUILanguage()
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamUtils _ptr is null!" );
				
				return Native.ISteamUtils.GetSteamUILanguage(_ptr);
			}
			public virtual bool /*bool*/ ISteamUtils_IsSteamRunningInVR()
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamUtils _ptr is null!" );
				
				return Native.ISteamUtils.IsSteamRunningInVR(_ptr);
			}
			public virtual void /*void*/ ISteamUtils_SetOverlayNotificationInset( int /*int*/ nHorizontalInset, int /*int*/ nVerticalInset )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamUtils _ptr is null!" );
				
				Native.ISteamUtils.SetOverlayNotificationInset(_ptr, nHorizontalInset /*C*/, nVerticalInset /*C*/);
			}
			public virtual bool /*bool*/ ISteamUtils_IsSteamInBigPictureMode()
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamUtils _ptr is null!" );
				
				return Native.ISteamUtils.IsSteamInBigPictureMode(_ptr);
			}
			public virtual void /*void*/ ISteamUtils_StartVRDashboard()
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamUtils _ptr is null!" );
				
				Native.ISteamUtils.StartVRDashboard(_ptr);
			}
			
			public virtual int /*int*/ ISteamMatchmaking_GetFavoriteGameCount()
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamMatchmaking _ptr is null!" );
				
				return Native.ISteamMatchmaking.GetFavoriteGameCount(_ptr);
			}
			public virtual bool /*bool*/ ISteamMatchmaking_GetFavoriteGame( int /*int*/ iGame, ref uint pnAppID, out uint /*uint32 **/ pnIP, out ushort /*uint16 **/ pnConnPort, out ushort /*uint16 **/ pnQueryPort, IntPtr /*uint32 **/ punFlags, out uint /*uint32 **/ pRTime32LastPlayedOnServer )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamMatchmaking _ptr is null!" );
				
				return Native.ISteamMatchmaking.GetFavoriteGame(_ptr, iGame /*C*/, ref pnAppID /*A*/, out pnIP /*B*/, out pnConnPort /*B*/, out pnQueryPort /*B*/, punFlags, out pRTime32LastPlayedOnServer /*B*/);
			}
			public virtual int /*int*/ ISteamMatchmaking_AddFavoriteGame( uint nAppID, uint /*uint32*/ nIP, ushort /*uint16*/ nConnPort, ushort /*uint16*/ nQueryPort, uint /*uint32*/ unFlags, uint /*uint32*/ rTime32LastPlayedOnServer )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamMatchmaking _ptr is null!" );
				
				return Native.ISteamMatchmaking.AddFavoriteGame(_ptr, nAppID /*C*/, nIP /*C*/, nConnPort /*C*/, nQueryPort /*C*/, unFlags /*C*/, rTime32LastPlayedOnServer /*C*/);
			}
			public virtual bool /*bool*/ ISteamMatchmaking_RemoveFavoriteGame( uint nAppID, uint /*uint32*/ nIP, ushort /*uint16*/ nConnPort, ushort /*uint16*/ nQueryPort, uint /*uint32*/ unFlags )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamMatchmaking _ptr is null!" );
				
				return Native.ISteamMatchmaking.RemoveFavoriteGame(_ptr, nAppID /*C*/, nIP /*C*/, nConnPort /*C*/, nQueryPort /*C*/, unFlags /*C*/);
			}
			public virtual SteamAPICall_t /*(SteamAPICall_t)*/ ISteamMatchmaking_RequestLobbyList()
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamMatchmaking _ptr is null!" );
				
				return Native.ISteamMatchmaking.RequestLobbyList(_ptr);
			}
			public virtual void /*void*/ ISteamMatchmaking_AddRequestLobbyListStringFilter( string /*const char **/ pchKeyToMatch, string /*const char **/ pchValueToMatch, LobbyComparison /*ELobbyComparison*/ eComparisonType )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamMatchmaking _ptr is null!" );
				
				Native.ISteamMatchmaking.AddRequestLobbyListStringFilter(_ptr, pchKeyToMatch /*C*/, pchValueToMatch /*C*/, eComparisonType /*C*/);
			}
			public virtual void /*void*/ ISteamMatchmaking_AddRequestLobbyListNumericalFilter( string /*const char **/ pchKeyToMatch, int /*int*/ nValueToMatch, LobbyComparison /*ELobbyComparison*/ eComparisonType )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamMatchmaking _ptr is null!" );
				
				Native.ISteamMatchmaking.AddRequestLobbyListNumericalFilter(_ptr, pchKeyToMatch /*C*/, nValueToMatch /*C*/, eComparisonType /*C*/);
			}
			public virtual void /*void*/ ISteamMatchmaking_AddRequestLobbyListNearValueFilter( string /*const char **/ pchKeyToMatch, int /*int*/ nValueToBeCloseTo )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamMatchmaking _ptr is null!" );
				
				Native.ISteamMatchmaking.AddRequestLobbyListNearValueFilter(_ptr, pchKeyToMatch /*C*/, nValueToBeCloseTo /*C*/);
			}
			public virtual void /*void*/ ISteamMatchmaking_AddRequestLobbyListFilterSlotsAvailable( int /*int*/ nSlotsAvailable )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamMatchmaking _ptr is null!" );
				
				Native.ISteamMatchmaking.AddRequestLobbyListFilterSlotsAvailable(_ptr, nSlotsAvailable /*C*/);
			}
			public virtual void /*void*/ ISteamMatchmaking_AddRequestLobbyListDistanceFilter( LobbyDistanceFilter /*ELobbyDistanceFilter*/ eLobbyDistanceFilter )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamMatchmaking _ptr is null!" );
				
				Native.ISteamMatchmaking.AddRequestLobbyListDistanceFilter(_ptr, eLobbyDistanceFilter /*C*/);
			}
			public virtual void /*void*/ ISteamMatchmaking_AddRequestLobbyListResultCountFilter( int /*int*/ cMaxResults )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamMatchmaking _ptr is null!" );
				
				Native.ISteamMatchmaking.AddRequestLobbyListResultCountFilter(_ptr, cMaxResults /*C*/);
			}
			public virtual void /*void*/ ISteamMatchmaking_AddRequestLobbyListCompatibleMembersFilter( ulong steamIDLobby )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamMatchmaking _ptr is null!" );
				
				Native.ISteamMatchmaking.AddRequestLobbyListCompatibleMembersFilter(_ptr, steamIDLobby /*C*/);
			}
			public virtual CSteamID /*(class CSteamID)*/ ISteamMatchmaking_GetLobbyByIndex( int /*int*/ iLobby )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamMatchmaking _ptr is null!" );
				
				return Native.ISteamMatchmaking.GetLobbyByIndex(_ptr, iLobby /*C*/);
			}
			public virtual SteamAPICall_t /*(SteamAPICall_t)*/ ISteamMatchmaking_CreateLobby( LobbyType /*ELobbyType*/ eLobbyType, int /*int*/ cMaxMembers )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamMatchmaking _ptr is null!" );
				
				return Native.ISteamMatchmaking.CreateLobby(_ptr, eLobbyType /*C*/, cMaxMembers /*C*/);
			}
			public virtual SteamAPICall_t /*(SteamAPICall_t)*/ ISteamMatchmaking_JoinLobby( ulong steamIDLobby )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamMatchmaking _ptr is null!" );
				
				return Native.ISteamMatchmaking.JoinLobby(_ptr, steamIDLobby /*C*/);
			}
			public virtual void /*void*/ ISteamMatchmaking_LeaveLobby( ulong steamIDLobby )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamMatchmaking _ptr is null!" );
				
				Native.ISteamMatchmaking.LeaveLobby(_ptr, steamIDLobby /*C*/);
			}
			public virtual bool /*bool*/ ISteamMatchmaking_InviteUserToLobby( ulong steamIDLobby, ulong steamIDInvitee )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamMatchmaking _ptr is null!" );
				
				return Native.ISteamMatchmaking.InviteUserToLobby(_ptr, steamIDLobby /*C*/, steamIDInvitee /*C*/);
			}
			public virtual int /*int*/ ISteamMatchmaking_GetNumLobbyMembers( ulong steamIDLobby )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamMatchmaking _ptr is null!" );
				
				return Native.ISteamMatchmaking.GetNumLobbyMembers(_ptr, steamIDLobby /*C*/);
			}
			public virtual CSteamID /*(class CSteamID)*/ ISteamMatchmaking_GetLobbyMemberByIndex( ulong steamIDLobby, int /*int*/ iMember )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamMatchmaking _ptr is null!" );
				
				return Native.ISteamMatchmaking.GetLobbyMemberByIndex(_ptr, steamIDLobby /*C*/, iMember /*C*/);
			}
			public virtual IntPtr ISteamMatchmaking_GetLobbyData( ulong steamIDLobby, string /*const char **/ pchKey )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamMatchmaking _ptr is null!" );
				
				return Native.ISteamMatchmaking.GetLobbyData(_ptr, steamIDLobby /*C*/, pchKey /*C*/);
			}
			public virtual bool /*bool*/ ISteamMatchmaking_SetLobbyData( ulong steamIDLobby, string /*const char **/ pchKey, string /*const char **/ pchValue )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamMatchmaking _ptr is null!" );
				
				return Native.ISteamMatchmaking.SetLobbyData(_ptr, steamIDLobby /*C*/, pchKey /*C*/, pchValue /*C*/);
			}
			public virtual int /*int*/ ISteamMatchmaking_GetLobbyDataCount( ulong steamIDLobby )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamMatchmaking _ptr is null!" );
				
				return Native.ISteamMatchmaking.GetLobbyDataCount(_ptr, steamIDLobby /*C*/);
			}
			public virtual bool /*bool*/ ISteamMatchmaking_GetLobbyDataByIndex( ulong steamIDLobby, int /*int*/ iLobbyData, System.Text.StringBuilder /*char **/ pchKey, int /*int*/ cchKeyBufferSize, System.Text.StringBuilder /*char **/ pchValue, int /*int*/ cchValueBufferSize )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamMatchmaking _ptr is null!" );
				
				return Native.ISteamMatchmaking.GetLobbyDataByIndex(_ptr, steamIDLobby /*C*/, iLobbyData /*C*/, pchKey /*C*/, cchKeyBufferSize /*C*/, pchValue /*C*/, cchValueBufferSize /*C*/);
			}
			public virtual bool /*bool*/ ISteamMatchmaking_DeleteLobbyData( ulong steamIDLobby, string /*const char **/ pchKey )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamMatchmaking _ptr is null!" );
				
				return Native.ISteamMatchmaking.DeleteLobbyData(_ptr, steamIDLobby /*C*/, pchKey /*C*/);
			}
			public virtual IntPtr ISteamMatchmaking_GetLobbyMemberData( ulong steamIDLobby, ulong steamIDUser, string /*const char **/ pchKey )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamMatchmaking _ptr is null!" );
				
				return Native.ISteamMatchmaking.GetLobbyMemberData(_ptr, steamIDLobby /*C*/, steamIDUser /*C*/, pchKey /*C*/);
			}
			public virtual void /*void*/ ISteamMatchmaking_SetLobbyMemberData( ulong steamIDLobby, string /*const char **/ pchKey, string /*const char **/ pchValue )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamMatchmaking _ptr is null!" );
				
				Native.ISteamMatchmaking.SetLobbyMemberData(_ptr, steamIDLobby /*C*/, pchKey /*C*/, pchValue /*C*/);
			}
			public virtual bool /*bool*/ ISteamMatchmaking_SendLobbyChatMsg( ulong steamIDLobby, IntPtr /*const void **/ pvMsgBody, int /*int*/ cubMsgBody )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamMatchmaking _ptr is null!" );
				
				return Native.ISteamMatchmaking.SendLobbyChatMsg(_ptr, steamIDLobby /*C*/, pvMsgBody /*C*/, cubMsgBody /*C*/);
			}
			public virtual int /*int*/ ISteamMatchmaking_GetLobbyChatEntry( ulong steamIDLobby, int /*int*/ iChatID, out ulong pSteamIDUser, IntPtr /*void **/ pvData, int /*int*/ cubData, out ChatEntryType /*EChatEntryType **/ peChatEntryType )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamMatchmaking _ptr is null!" );
				
				return Native.ISteamMatchmaking.GetLobbyChatEntry(_ptr, steamIDLobby /*C*/, iChatID /*C*/, out pSteamIDUser /*B*/, pvData /*C*/, cubData /*C*/, out peChatEntryType /*B*/);
			}
			public virtual bool /*bool*/ ISteamMatchmaking_RequestLobbyData( ulong steamIDLobby )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamMatchmaking _ptr is null!" );
				
				return Native.ISteamMatchmaking.RequestLobbyData(_ptr, steamIDLobby /*C*/);
			}
			public virtual void /*void*/ ISteamMatchmaking_SetLobbyGameServer( ulong steamIDLobby, uint /*uint32*/ unGameServerIP, ushort /*uint16*/ unGameServerPort, ulong steamIDGameServer )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamMatchmaking _ptr is null!" );
				
				Native.ISteamMatchmaking.SetLobbyGameServer(_ptr, steamIDLobby /*C*/, unGameServerIP /*C*/, unGameServerPort /*C*/, steamIDGameServer /*C*/);
			}
			public virtual bool /*bool*/ ISteamMatchmaking_GetLobbyGameServer( ulong steamIDLobby, out uint /*uint32 **/ punGameServerIP, out ushort /*uint16 **/ punGameServerPort, out ulong psteamIDGameServer )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamMatchmaking _ptr is null!" );
				
				return Native.ISteamMatchmaking.GetLobbyGameServer(_ptr, steamIDLobby /*C*/, out punGameServerIP /*B*/, out punGameServerPort /*B*/, out psteamIDGameServer /*B*/);
			}
			public virtual bool /*bool*/ ISteamMatchmaking_SetLobbyMemberLimit( ulong steamIDLobby, int /*int*/ cMaxMembers )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamMatchmaking _ptr is null!" );
				
				return Native.ISteamMatchmaking.SetLobbyMemberLimit(_ptr, steamIDLobby /*C*/, cMaxMembers /*C*/);
			}
			public virtual int /*int*/ ISteamMatchmaking_GetLobbyMemberLimit( ulong steamIDLobby )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamMatchmaking _ptr is null!" );
				
				return Native.ISteamMatchmaking.GetLobbyMemberLimit(_ptr, steamIDLobby /*C*/);
			}
			public virtual bool /*bool*/ ISteamMatchmaking_SetLobbyType( ulong steamIDLobby, LobbyType /*ELobbyType*/ eLobbyType )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamMatchmaking _ptr is null!" );
				
				return Native.ISteamMatchmaking.SetLobbyType(_ptr, steamIDLobby /*C*/, eLobbyType /*C*/);
			}
			public virtual bool /*bool*/ ISteamMatchmaking_SetLobbyJoinable( ulong steamIDLobby, [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bLobbyJoinable )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamMatchmaking _ptr is null!" );
				
				return Native.ISteamMatchmaking.SetLobbyJoinable(_ptr, steamIDLobby /*C*/, bLobbyJoinable /*C*/);
			}
			public virtual CSteamID /*(class CSteamID)*/ ISteamMatchmaking_GetLobbyOwner( ulong steamIDLobby )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamMatchmaking _ptr is null!" );
				
				return Native.ISteamMatchmaking.GetLobbyOwner(_ptr, steamIDLobby /*C*/);
			}
			public virtual bool /*bool*/ ISteamMatchmaking_SetLobbyOwner( ulong steamIDLobby, ulong steamIDNewOwner )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamMatchmaking _ptr is null!" );
				
				return Native.ISteamMatchmaking.SetLobbyOwner(_ptr, steamIDLobby /*C*/, steamIDNewOwner /*C*/);
			}
			public virtual bool /*bool*/ ISteamMatchmaking_SetLinkedLobby( ulong steamIDLobby, ulong steamIDLobbyDependent )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamMatchmaking _ptr is null!" );
				
				return Native.ISteamMatchmaking.SetLinkedLobby(_ptr, steamIDLobby /*C*/, steamIDLobbyDependent /*C*/);
			}
			
			public virtual HServerListRequest /*(HServerListRequest)*/ ISteamMatchmakingServers_RequestInternetServerList( uint iApp, IntPtr /*struct MatchMakingKeyValuePair_t ***/ ppchFilters, uint /*uint32*/ nFilters, IntPtr /*class ISteamMatchmakingServerListResponse **/ pRequestServersResponse )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamMatchmakingServers _ptr is null!" );
				
				return Native.ISteamMatchmakingServers.RequestInternetServerList(_ptr, iApp /*C*/, ppchFilters, nFilters /*C*/, pRequestServersResponse /*C*/);
			}
			public virtual HServerListRequest /*(HServerListRequest)*/ ISteamMatchmakingServers_RequestLANServerList( uint iApp, IntPtr /*class ISteamMatchmakingServerListResponse **/ pRequestServersResponse )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamMatchmakingServers _ptr is null!" );
				
				return Native.ISteamMatchmakingServers.RequestLANServerList(_ptr, iApp /*C*/, pRequestServersResponse /*C*/);
			}
			public virtual HServerListRequest /*(HServerListRequest)*/ ISteamMatchmakingServers_RequestFriendsServerList( uint iApp, IntPtr /*struct MatchMakingKeyValuePair_t ***/ ppchFilters, uint /*uint32*/ nFilters, IntPtr /*class ISteamMatchmakingServerListResponse **/ pRequestServersResponse )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamMatchmakingServers _ptr is null!" );
				
				return Native.ISteamMatchmakingServers.RequestFriendsServerList(_ptr, iApp /*C*/, ppchFilters, nFilters /*C*/, pRequestServersResponse /*C*/);
			}
			public virtual HServerListRequest /*(HServerListRequest)*/ ISteamMatchmakingServers_RequestFavoritesServerList( uint iApp, IntPtr /*struct MatchMakingKeyValuePair_t ***/ ppchFilters, uint /*uint32*/ nFilters, IntPtr /*class ISteamMatchmakingServerListResponse **/ pRequestServersResponse )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamMatchmakingServers _ptr is null!" );
				
				return Native.ISteamMatchmakingServers.RequestFavoritesServerList(_ptr, iApp /*C*/, ppchFilters, nFilters /*C*/, pRequestServersResponse /*C*/);
			}
			public virtual HServerListRequest /*(HServerListRequest)*/ ISteamMatchmakingServers_RequestHistoryServerList( uint iApp, IntPtr /*struct MatchMakingKeyValuePair_t ***/ ppchFilters, uint /*uint32*/ nFilters, IntPtr /*class ISteamMatchmakingServerListResponse **/ pRequestServersResponse )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamMatchmakingServers _ptr is null!" );
				
				return Native.ISteamMatchmakingServers.RequestHistoryServerList(_ptr, iApp /*C*/, ppchFilters, nFilters /*C*/, pRequestServersResponse /*C*/);
			}
			public virtual HServerListRequest /*(HServerListRequest)*/ ISteamMatchmakingServers_RequestSpectatorServerList( uint iApp, IntPtr /*struct MatchMakingKeyValuePair_t ***/ ppchFilters, uint /*uint32*/ nFilters, IntPtr /*class ISteamMatchmakingServerListResponse **/ pRequestServersResponse )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamMatchmakingServers _ptr is null!" );
				
				return Native.ISteamMatchmakingServers.RequestSpectatorServerList(_ptr, iApp /*C*/, ppchFilters, nFilters /*C*/, pRequestServersResponse /*C*/);
			}
			public virtual void /*void*/ ISteamMatchmakingServers_ReleaseRequest( IntPtr hServerListRequest )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamMatchmakingServers _ptr is null!" );
				
				Native.ISteamMatchmakingServers.ReleaseRequest(_ptr, hServerListRequest /*C*/);
			}
			public virtual IntPtr /*class gameserveritem_t **/ ISteamMatchmakingServers_GetServerDetails( IntPtr hRequest, int /*int*/ iServer )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamMatchmakingServers _ptr is null!" );
				
				return Native.ISteamMatchmakingServers.GetServerDetails(_ptr, hRequest /*C*/, iServer /*C*/);
			}
			public virtual void /*void*/ ISteamMatchmakingServers_CancelQuery( IntPtr hRequest )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamMatchmakingServers _ptr is null!" );
				
				Native.ISteamMatchmakingServers.CancelQuery(_ptr, hRequest /*C*/);
			}
			public virtual void /*void*/ ISteamMatchmakingServers_RefreshQuery( IntPtr hRequest )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamMatchmakingServers _ptr is null!" );
				
				Native.ISteamMatchmakingServers.RefreshQuery(_ptr, hRequest /*C*/);
			}
			public virtual bool /*bool*/ ISteamMatchmakingServers_IsRefreshing( IntPtr hRequest )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamMatchmakingServers _ptr is null!" );
				
				return Native.ISteamMatchmakingServers.IsRefreshing(_ptr, hRequest /*C*/);
			}
			public virtual int /*int*/ ISteamMatchmakingServers_GetServerCount( IntPtr hRequest )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamMatchmakingServers _ptr is null!" );
				
				return Native.ISteamMatchmakingServers.GetServerCount(_ptr, hRequest /*C*/);
			}
			public virtual void /*void*/ ISteamMatchmakingServers_RefreshServer( IntPtr hRequest, int /*int*/ iServer )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamMatchmakingServers _ptr is null!" );
				
				Native.ISteamMatchmakingServers.RefreshServer(_ptr, hRequest /*C*/, iServer /*C*/);
			}
			public virtual HServerQuery /*(HServerQuery)*/ ISteamMatchmakingServers_PingServer( uint /*uint32*/ unIP, ushort /*uint16*/ usPort, IntPtr /*class ISteamMatchmakingPingResponse **/ pRequestServersResponse )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamMatchmakingServers _ptr is null!" );
				
				return Native.ISteamMatchmakingServers.PingServer(_ptr, unIP /*C*/, usPort /*C*/, pRequestServersResponse /*C*/);
			}
			public virtual HServerQuery /*(HServerQuery)*/ ISteamMatchmakingServers_PlayerDetails( uint /*uint32*/ unIP, ushort /*uint16*/ usPort, IntPtr /*class ISteamMatchmakingPlayersResponse **/ pRequestServersResponse )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamMatchmakingServers _ptr is null!" );
				
				return Native.ISteamMatchmakingServers.PlayerDetails(_ptr, unIP /*C*/, usPort /*C*/, pRequestServersResponse /*C*/);
			}
			public virtual HServerQuery /*(HServerQuery)*/ ISteamMatchmakingServers_ServerRules( uint /*uint32*/ unIP, ushort /*uint16*/ usPort, IntPtr /*class ISteamMatchmakingRulesResponse **/ pRequestServersResponse )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamMatchmakingServers _ptr is null!" );
				
				return Native.ISteamMatchmakingServers.ServerRules(_ptr, unIP /*C*/, usPort /*C*/, pRequestServersResponse /*C*/);
			}
			public virtual void /*void*/ ISteamMatchmakingServers_CancelServerQuery( int hServerQuery )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamMatchmakingServers _ptr is null!" );
				
				Native.ISteamMatchmakingServers.CancelServerQuery(_ptr, hServerQuery /*C*/);
			}
			
			public virtual bool /*bool*/ ISteamRemoteStorage_FileWrite( string /*const char **/ pchFile, IntPtr /*const void **/ pvData, int /*int32*/ cubData )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamRemoteStorage _ptr is null!" );
				
				return Native.ISteamRemoteStorage.FileWrite(_ptr, pchFile /*C*/, pvData /*C*/, cubData /*C*/);
			}
			public virtual int /*int32*/ ISteamRemoteStorage_FileRead( string /*const char **/ pchFile, IntPtr /*void **/ pvData, int /*int32*/ cubDataToRead )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamRemoteStorage _ptr is null!" );
				
				return Native.ISteamRemoteStorage.FileRead(_ptr, pchFile /*C*/, pvData /*C*/, cubDataToRead /*C*/);
			}
			public virtual SteamAPICall_t /*(SteamAPICall_t)*/ ISteamRemoteStorage_FileWriteAsync( string /*const char **/ pchFile, IntPtr /*const void **/ pvData, uint /*uint32*/ cubData )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamRemoteStorage _ptr is null!" );
				
				return Native.ISteamRemoteStorage.FileWriteAsync(_ptr, pchFile /*C*/, pvData /*C*/, cubData /*C*/);
			}
			public virtual SteamAPICall_t /*(SteamAPICall_t)*/ ISteamRemoteStorage_FileReadAsync( string /*const char **/ pchFile, uint /*uint32*/ nOffset, uint /*uint32*/ cubToRead )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamRemoteStorage _ptr is null!" );
				
				return Native.ISteamRemoteStorage.FileReadAsync(_ptr, pchFile /*C*/, nOffset /*C*/, cubToRead /*C*/);
			}
			public virtual bool /*bool*/ ISteamRemoteStorage_FileReadAsyncComplete( ulong hReadCall, IntPtr /*void **/ pvBuffer, uint /*uint32*/ cubToRead )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamRemoteStorage _ptr is null!" );
				
				return Native.ISteamRemoteStorage.FileReadAsyncComplete(_ptr, hReadCall /*C*/, pvBuffer /*C*/, cubToRead /*C*/);
			}
			public virtual bool /*bool*/ ISteamRemoteStorage_FileForget( string /*const char **/ pchFile )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamRemoteStorage _ptr is null!" );
				
				return Native.ISteamRemoteStorage.FileForget(_ptr, pchFile /*C*/);
			}
			public virtual bool /*bool*/ ISteamRemoteStorage_FileDelete( string /*const char **/ pchFile )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamRemoteStorage _ptr is null!" );
				
				return Native.ISteamRemoteStorage.FileDelete(_ptr, pchFile /*C*/);
			}
			public virtual SteamAPICall_t /*(SteamAPICall_t)*/ ISteamRemoteStorage_FileShare( string /*const char **/ pchFile )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamRemoteStorage _ptr is null!" );
				
				return Native.ISteamRemoteStorage.FileShare(_ptr, pchFile /*C*/);
			}
			public virtual bool /*bool*/ ISteamRemoteStorage_SetSyncPlatforms( string /*const char **/ pchFile, RemoteStoragePlatform /*ERemoteStoragePlatform*/ eRemoteStoragePlatform )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamRemoteStorage _ptr is null!" );
				
				return Native.ISteamRemoteStorage.SetSyncPlatforms(_ptr, pchFile /*C*/, eRemoteStoragePlatform /*C*/);
			}
			public virtual UGCFileWriteStreamHandle_t /*(UGCFileWriteStreamHandle_t)*/ ISteamRemoteStorage_FileWriteStreamOpen( string /*const char **/ pchFile )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamRemoteStorage _ptr is null!" );
				
				return Native.ISteamRemoteStorage.FileWriteStreamOpen(_ptr, pchFile /*C*/);
			}
			public virtual bool /*bool*/ ISteamRemoteStorage_FileWriteStreamWriteChunk( ulong writeHandle, IntPtr /*const void **/ pvData, int /*int32*/ cubData )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamRemoteStorage _ptr is null!" );
				
				return Native.ISteamRemoteStorage.FileWriteStreamWriteChunk(_ptr, writeHandle /*C*/, pvData /*C*/, cubData /*C*/);
			}
			public virtual bool /*bool*/ ISteamRemoteStorage_FileWriteStreamClose( ulong writeHandle )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamRemoteStorage _ptr is null!" );
				
				return Native.ISteamRemoteStorage.FileWriteStreamClose(_ptr, writeHandle /*C*/);
			}
			public virtual bool /*bool*/ ISteamRemoteStorage_FileWriteStreamCancel( ulong writeHandle )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamRemoteStorage _ptr is null!" );
				
				return Native.ISteamRemoteStorage.FileWriteStreamCancel(_ptr, writeHandle /*C*/);
			}
			public virtual bool /*bool*/ ISteamRemoteStorage_FileExists( string /*const char **/ pchFile )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamRemoteStorage _ptr is null!" );
				
				return Native.ISteamRemoteStorage.FileExists(_ptr, pchFile /*C*/);
			}
			public virtual bool /*bool*/ ISteamRemoteStorage_FilePersisted( string /*const char **/ pchFile )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamRemoteStorage _ptr is null!" );
				
				return Native.ISteamRemoteStorage.FilePersisted(_ptr, pchFile /*C*/);
			}
			public virtual int /*int32*/ ISteamRemoteStorage_GetFileSize( string /*const char **/ pchFile )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamRemoteStorage _ptr is null!" );
				
				return Native.ISteamRemoteStorage.GetFileSize(_ptr, pchFile /*C*/);
			}
			public virtual long /*int64*/ ISteamRemoteStorage_GetFileTimestamp( string /*const char **/ pchFile )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamRemoteStorage _ptr is null!" );
				
				return Native.ISteamRemoteStorage.GetFileTimestamp(_ptr, pchFile /*C*/);
			}
			public virtual RemoteStoragePlatform /*ERemoteStoragePlatform*/ ISteamRemoteStorage_GetSyncPlatforms( string /*const char **/ pchFile )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamRemoteStorage _ptr is null!" );
				
				return Native.ISteamRemoteStorage.GetSyncPlatforms(_ptr, pchFile /*C*/);
			}
			public virtual int /*int32*/ ISteamRemoteStorage_GetFileCount()
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamRemoteStorage _ptr is null!" );
				
				return Native.ISteamRemoteStorage.GetFileCount(_ptr);
			}
			public virtual IntPtr ISteamRemoteStorage_GetFileNameAndSize( int /*int*/ iFile, IntPtr /*int32 **/ pnFileSizeInBytes )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamRemoteStorage _ptr is null!" );
				
				return Native.ISteamRemoteStorage.GetFileNameAndSize(_ptr, iFile /*C*/, pnFileSizeInBytes);
			}
			public virtual bool /*bool*/ ISteamRemoteStorage_GetQuota( IntPtr /*int32 **/ pnTotalBytes, IntPtr /*int32 **/ puAvailableBytes )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamRemoteStorage _ptr is null!" );
				
				return Native.ISteamRemoteStorage.GetQuota(_ptr, pnTotalBytes, puAvailableBytes);
			}
			public virtual bool /*bool*/ ISteamRemoteStorage_IsCloudEnabledForAccount()
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamRemoteStorage _ptr is null!" );
				
				return Native.ISteamRemoteStorage.IsCloudEnabledForAccount(_ptr);
			}
			public virtual bool /*bool*/ ISteamRemoteStorage_IsCloudEnabledForApp()
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamRemoteStorage _ptr is null!" );
				
				return Native.ISteamRemoteStorage.IsCloudEnabledForApp(_ptr);
			}
			public virtual void /*void*/ ISteamRemoteStorage_SetCloudEnabledForApp( [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bEnabled )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamRemoteStorage _ptr is null!" );
				
				Native.ISteamRemoteStorage.SetCloudEnabledForApp(_ptr, bEnabled /*C*/);
			}
			public virtual SteamAPICall_t /*(SteamAPICall_t)*/ ISteamRemoteStorage_UGCDownload( ulong hContent, uint /*uint32*/ unPriority )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamRemoteStorage _ptr is null!" );
				
				return Native.ISteamRemoteStorage.UGCDownload(_ptr, hContent /*C*/, unPriority /*C*/);
			}
			public virtual bool /*bool*/ ISteamRemoteStorage_GetUGCDownloadProgress( ulong hContent, out int /*int32 **/ pnBytesDownloaded, out int /*int32 **/ pnBytesExpected )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamRemoteStorage _ptr is null!" );
				
				return Native.ISteamRemoteStorage.GetUGCDownloadProgress(_ptr, hContent /*C*/, out pnBytesDownloaded /*B*/, out pnBytesExpected /*B*/);
			}
			public virtual bool /*bool*/ ISteamRemoteStorage_GetUGCDetails( ulong hContent, ref uint pnAppID, System.Text.StringBuilder /*char ***/ ppchName, IntPtr /*int32 **/ pnFileSizeInBytes, out ulong pSteamIDOwner )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamRemoteStorage _ptr is null!" );
				
				return Native.ISteamRemoteStorage.GetUGCDetails(_ptr, hContent /*C*/, ref pnAppID /*A*/, ppchName /*C*/, pnFileSizeInBytes, out pSteamIDOwner /*B*/);
			}
			public virtual int /*int32*/ ISteamRemoteStorage_UGCRead( ulong hContent, IntPtr /*void **/ pvData, int /*int32*/ cubDataToRead, uint /*uint32*/ cOffset, UGCReadAction /*EUGCReadAction*/ eAction )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamRemoteStorage _ptr is null!" );
				
				return Native.ISteamRemoteStorage.UGCRead(_ptr, hContent /*C*/, pvData /*C*/, cubDataToRead /*C*/, cOffset /*C*/, eAction /*C*/);
			}
			public virtual int /*int32*/ ISteamRemoteStorage_GetCachedUGCCount()
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamRemoteStorage _ptr is null!" );
				
				return Native.ISteamRemoteStorage.GetCachedUGCCount(_ptr);
			}
			public virtual UGCHandle_t /*(UGCHandle_t)*/ ISteamRemoteStorage_GetCachedUGCHandle( int /*int32*/ iCachedContent )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamRemoteStorage _ptr is null!" );
				
				return Native.ISteamRemoteStorage.GetCachedUGCHandle(_ptr, iCachedContent /*C*/);
			}
			public virtual SteamAPICall_t /*(SteamAPICall_t)*/ ISteamRemoteStorage_PublishWorkshopFile( string /*const char **/ pchFile, string /*const char **/ pchPreviewFile, uint nConsumerAppId, string /*const char **/ pchTitle, string /*const char **/ pchDescription, RemoteStoragePublishedFileVisibility /*ERemoteStoragePublishedFileVisibility*/ eVisibility, IntPtr /*struct SteamParamStringArray_t **/ pTags, WorkshopFileType /*EWorkshopFileType*/ eWorkshopFileType )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamRemoteStorage _ptr is null!" );
				
				return Native.ISteamRemoteStorage.PublishWorkshopFile(_ptr, pchFile /*C*/, pchPreviewFile /*C*/, nConsumerAppId /*C*/, pchTitle /*C*/, pchDescription /*C*/, eVisibility /*C*/, pTags, eWorkshopFileType /*C*/);
			}
			public virtual PublishedFileUpdateHandle_t /*(PublishedFileUpdateHandle_t)*/ ISteamRemoteStorage_CreatePublishedFileUpdateRequest( ulong unPublishedFileId )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamRemoteStorage _ptr is null!" );
				
				return Native.ISteamRemoteStorage.CreatePublishedFileUpdateRequest(_ptr, unPublishedFileId /*C*/);
			}
			public virtual bool /*bool*/ ISteamRemoteStorage_UpdatePublishedFileFile( ulong updateHandle, string /*const char **/ pchFile )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamRemoteStorage _ptr is null!" );
				
				return Native.ISteamRemoteStorage.UpdatePublishedFileFile(_ptr, updateHandle /*C*/, pchFile /*C*/);
			}
			public virtual bool /*bool*/ ISteamRemoteStorage_UpdatePublishedFilePreviewFile( ulong updateHandle, string /*const char **/ pchPreviewFile )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamRemoteStorage _ptr is null!" );
				
				return Native.ISteamRemoteStorage.UpdatePublishedFilePreviewFile(_ptr, updateHandle /*C*/, pchPreviewFile /*C*/);
			}
			public virtual bool /*bool*/ ISteamRemoteStorage_UpdatePublishedFileTitle( ulong updateHandle, string /*const char **/ pchTitle )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamRemoteStorage _ptr is null!" );
				
				return Native.ISteamRemoteStorage.UpdatePublishedFileTitle(_ptr, updateHandle /*C*/, pchTitle /*C*/);
			}
			public virtual bool /*bool*/ ISteamRemoteStorage_UpdatePublishedFileDescription( ulong updateHandle, string /*const char **/ pchDescription )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamRemoteStorage _ptr is null!" );
				
				return Native.ISteamRemoteStorage.UpdatePublishedFileDescription(_ptr, updateHandle /*C*/, pchDescription /*C*/);
			}
			public virtual bool /*bool*/ ISteamRemoteStorage_UpdatePublishedFileVisibility( ulong updateHandle, RemoteStoragePublishedFileVisibility /*ERemoteStoragePublishedFileVisibility*/ eVisibility )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamRemoteStorage _ptr is null!" );
				
				return Native.ISteamRemoteStorage.UpdatePublishedFileVisibility(_ptr, updateHandle /*C*/, eVisibility /*C*/);
			}
			public virtual bool /*bool*/ ISteamRemoteStorage_UpdatePublishedFileTags( ulong updateHandle, IntPtr /*struct SteamParamStringArray_t **/ pTags )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamRemoteStorage _ptr is null!" );
				
				return Native.ISteamRemoteStorage.UpdatePublishedFileTags(_ptr, updateHandle /*C*/, pTags);
			}
			public virtual SteamAPICall_t /*(SteamAPICall_t)*/ ISteamRemoteStorage_CommitPublishedFileUpdate( ulong updateHandle )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamRemoteStorage _ptr is null!" );
				
				return Native.ISteamRemoteStorage.CommitPublishedFileUpdate(_ptr, updateHandle /*C*/);
			}
			public virtual SteamAPICall_t /*(SteamAPICall_t)*/ ISteamRemoteStorage_GetPublishedFileDetails( ulong unPublishedFileId, uint /*uint32*/ unMaxSecondsOld )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamRemoteStorage _ptr is null!" );
				
				return Native.ISteamRemoteStorage.GetPublishedFileDetails(_ptr, unPublishedFileId /*C*/, unMaxSecondsOld /*C*/);
			}
			public virtual SteamAPICall_t /*(SteamAPICall_t)*/ ISteamRemoteStorage_DeletePublishedFile( ulong unPublishedFileId )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamRemoteStorage _ptr is null!" );
				
				return Native.ISteamRemoteStorage.DeletePublishedFile(_ptr, unPublishedFileId /*C*/);
			}
			public virtual SteamAPICall_t /*(SteamAPICall_t)*/ ISteamRemoteStorage_EnumerateUserPublishedFiles( uint /*uint32*/ unStartIndex )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamRemoteStorage _ptr is null!" );
				
				return Native.ISteamRemoteStorage.EnumerateUserPublishedFiles(_ptr, unStartIndex /*C*/);
			}
			public virtual SteamAPICall_t /*(SteamAPICall_t)*/ ISteamRemoteStorage_SubscribePublishedFile( ulong unPublishedFileId )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamRemoteStorage _ptr is null!" );
				
				return Native.ISteamRemoteStorage.SubscribePublishedFile(_ptr, unPublishedFileId /*C*/);
			}
			public virtual SteamAPICall_t /*(SteamAPICall_t)*/ ISteamRemoteStorage_EnumerateUserSubscribedFiles( uint /*uint32*/ unStartIndex )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamRemoteStorage _ptr is null!" );
				
				return Native.ISteamRemoteStorage.EnumerateUserSubscribedFiles(_ptr, unStartIndex /*C*/);
			}
			public virtual SteamAPICall_t /*(SteamAPICall_t)*/ ISteamRemoteStorage_UnsubscribePublishedFile( ulong unPublishedFileId )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamRemoteStorage _ptr is null!" );
				
				return Native.ISteamRemoteStorage.UnsubscribePublishedFile(_ptr, unPublishedFileId /*C*/);
			}
			public virtual bool /*bool*/ ISteamRemoteStorage_UpdatePublishedFileSetChangeDescription( ulong updateHandle, string /*const char **/ pchChangeDescription )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamRemoteStorage _ptr is null!" );
				
				return Native.ISteamRemoteStorage.UpdatePublishedFileSetChangeDescription(_ptr, updateHandle /*C*/, pchChangeDescription /*C*/);
			}
			public virtual SteamAPICall_t /*(SteamAPICall_t)*/ ISteamRemoteStorage_GetPublishedItemVoteDetails( ulong unPublishedFileId )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamRemoteStorage _ptr is null!" );
				
				return Native.ISteamRemoteStorage.GetPublishedItemVoteDetails(_ptr, unPublishedFileId /*C*/);
			}
			public virtual SteamAPICall_t /*(SteamAPICall_t)*/ ISteamRemoteStorage_UpdateUserPublishedItemVote( ulong unPublishedFileId, [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bVoteUp )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamRemoteStorage _ptr is null!" );
				
				return Native.ISteamRemoteStorage.UpdateUserPublishedItemVote(_ptr, unPublishedFileId /*C*/, bVoteUp /*C*/);
			}
			public virtual SteamAPICall_t /*(SteamAPICall_t)*/ ISteamRemoteStorage_GetUserPublishedItemVoteDetails( ulong unPublishedFileId )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamRemoteStorage _ptr is null!" );
				
				return Native.ISteamRemoteStorage.GetUserPublishedItemVoteDetails(_ptr, unPublishedFileId /*C*/);
			}
			public virtual SteamAPICall_t /*(SteamAPICall_t)*/ ISteamRemoteStorage_EnumerateUserSharedWorkshopFiles( ulong steamId, uint /*uint32*/ unStartIndex, IntPtr /*struct SteamParamStringArray_t **/ pRequiredTags, IntPtr /*struct SteamParamStringArray_t **/ pExcludedTags )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamRemoteStorage _ptr is null!" );
				
				return Native.ISteamRemoteStorage.EnumerateUserSharedWorkshopFiles(_ptr, steamId /*C*/, unStartIndex /*C*/, pRequiredTags, pExcludedTags);
			}
			public virtual SteamAPICall_t /*(SteamAPICall_t)*/ ISteamRemoteStorage_PublishVideo( WorkshopVideoProvider /*EWorkshopVideoProvider*/ eVideoProvider, string /*const char **/ pchVideoAccount, string /*const char **/ pchVideoIdentifier, string /*const char **/ pchPreviewFile, uint nConsumerAppId, string /*const char **/ pchTitle, string /*const char **/ pchDescription, RemoteStoragePublishedFileVisibility /*ERemoteStoragePublishedFileVisibility*/ eVisibility, IntPtr /*struct SteamParamStringArray_t **/ pTags )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamRemoteStorage _ptr is null!" );
				
				return Native.ISteamRemoteStorage.PublishVideo(_ptr, eVideoProvider /*C*/, pchVideoAccount /*C*/, pchVideoIdentifier /*C*/, pchPreviewFile /*C*/, nConsumerAppId /*C*/, pchTitle /*C*/, pchDescription /*C*/, eVisibility /*C*/, pTags);
			}
			public virtual SteamAPICall_t /*(SteamAPICall_t)*/ ISteamRemoteStorage_SetUserPublishedFileAction( ulong unPublishedFileId, WorkshopFileAction /*EWorkshopFileAction*/ eAction )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamRemoteStorage _ptr is null!" );
				
				return Native.ISteamRemoteStorage.SetUserPublishedFileAction(_ptr, unPublishedFileId /*C*/, eAction /*C*/);
			}
			public virtual SteamAPICall_t /*(SteamAPICall_t)*/ ISteamRemoteStorage_EnumeratePublishedFilesByUserAction( WorkshopFileAction /*EWorkshopFileAction*/ eAction, uint /*uint32*/ unStartIndex )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamRemoteStorage _ptr is null!" );
				
				return Native.ISteamRemoteStorage.EnumeratePublishedFilesByUserAction(_ptr, eAction /*C*/, unStartIndex /*C*/);
			}
			public virtual SteamAPICall_t /*(SteamAPICall_t)*/ ISteamRemoteStorage_EnumeratePublishedWorkshopFiles( WorkshopEnumerationType /*EWorkshopEnumerationType*/ eEnumerationType, uint /*uint32*/ unStartIndex, uint /*uint32*/ unCount, uint /*uint32*/ unDays, IntPtr /*struct SteamParamStringArray_t **/ pTags, IntPtr /*struct SteamParamStringArray_t **/ pUserTags )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamRemoteStorage _ptr is null!" );
				
				return Native.ISteamRemoteStorage.EnumeratePublishedWorkshopFiles(_ptr, eEnumerationType /*C*/, unStartIndex /*C*/, unCount /*C*/, unDays /*C*/, pTags, pUserTags);
			}
			public virtual SteamAPICall_t /*(SteamAPICall_t)*/ ISteamRemoteStorage_UGCDownloadToLocation( ulong hContent, string /*const char **/ pchLocation, uint /*uint32*/ unPriority )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamRemoteStorage _ptr is null!" );
				
				return Native.ISteamRemoteStorage.UGCDownloadToLocation(_ptr, hContent /*C*/, pchLocation /*C*/, unPriority /*C*/);
			}
			
			public virtual bool /*bool*/ ISteamUserStats_RequestCurrentStats()
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamUserStats _ptr is null!" );
				
				return Native.ISteamUserStats.RequestCurrentStats(_ptr);
			}
			public virtual bool /*bool*/ ISteamUserStats_GetStat( string /*const char **/ pchName, out int /*int32 **/ pData )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamUserStats _ptr is null!" );
				
				return Native.ISteamUserStats.GetStat(_ptr, pchName /*C*/, out pData /*B*/);
			}
			public virtual bool /*bool*/ ISteamUserStats_GetStat0( string /*const char **/ pchName, out float /*float **/ pData )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamUserStats _ptr is null!" );
				
				return Native.ISteamUserStats.GetStat0(_ptr, pchName /*C*/, out pData /*B*/);
			}
			public virtual bool /*bool*/ ISteamUserStats_SetStat( string /*const char **/ pchName, int /*int32*/ nData )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamUserStats _ptr is null!" );
				
				return Native.ISteamUserStats.SetStat(_ptr, pchName /*C*/, nData /*C*/);
			}
			public virtual bool /*bool*/ ISteamUserStats_SetStat0( string /*const char **/ pchName, float /*float*/ fData )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamUserStats _ptr is null!" );
				
				return Native.ISteamUserStats.SetStat0(_ptr, pchName /*C*/, fData /*C*/);
			}
			public virtual bool /*bool*/ ISteamUserStats_UpdateAvgRateStat( string /*const char **/ pchName, float /*float*/ flCountThisSession, double /*double*/ dSessionLength )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamUserStats _ptr is null!" );
				
				return Native.ISteamUserStats.UpdateAvgRateStat(_ptr, pchName /*C*/, flCountThisSession /*C*/, dSessionLength /*C*/);
			}
			public virtual bool /*bool*/ ISteamUserStats_GetAchievement( string /*const char **/ pchName, [MarshalAs(UnmanagedType.U1)] ref bool /*bool **/ pbAchieved )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamUserStats _ptr is null!" );
				
				return Native.ISteamUserStats.GetAchievement(_ptr, pchName /*C*/, ref pbAchieved /*A*/);
			}
			public virtual bool /*bool*/ ISteamUserStats_SetAchievement( string /*const char **/ pchName )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamUserStats _ptr is null!" );
				
				return Native.ISteamUserStats.SetAchievement(_ptr, pchName /*C*/);
			}
			public virtual bool /*bool*/ ISteamUserStats_ClearAchievement( string /*const char **/ pchName )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamUserStats _ptr is null!" );
				
				return Native.ISteamUserStats.ClearAchievement(_ptr, pchName /*C*/);
			}
			public virtual bool /*bool*/ ISteamUserStats_GetAchievementAndUnlockTime( string /*const char **/ pchName, [MarshalAs(UnmanagedType.U1)] ref bool /*bool **/ pbAchieved, out uint /*uint32 **/ punUnlockTime )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamUserStats _ptr is null!" );
				
				return Native.ISteamUserStats.GetAchievementAndUnlockTime(_ptr, pchName /*C*/, ref pbAchieved /*A*/, out punUnlockTime /*B*/);
			}
			public virtual bool /*bool*/ ISteamUserStats_StoreStats()
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamUserStats _ptr is null!" );
				
				return Native.ISteamUserStats.StoreStats(_ptr);
			}
			public virtual int /*int*/ ISteamUserStats_GetAchievementIcon( string /*const char **/ pchName )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamUserStats _ptr is null!" );
				
				return Native.ISteamUserStats.GetAchievementIcon(_ptr, pchName /*C*/);
			}
			public virtual IntPtr ISteamUserStats_GetAchievementDisplayAttribute( string /*const char **/ pchName, string /*const char **/ pchKey )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamUserStats _ptr is null!" );
				
				return Native.ISteamUserStats.GetAchievementDisplayAttribute(_ptr, pchName /*C*/, pchKey /*C*/);
			}
			public virtual bool /*bool*/ ISteamUserStats_IndicateAchievementProgress( string /*const char **/ pchName, uint /*uint32*/ nCurProgress, uint /*uint32*/ nMaxProgress )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamUserStats _ptr is null!" );
				
				return Native.ISteamUserStats.IndicateAchievementProgress(_ptr, pchName /*C*/, nCurProgress /*C*/, nMaxProgress /*C*/);
			}
			public virtual uint /*uint32*/ ISteamUserStats_GetNumAchievements()
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamUserStats _ptr is null!" );
				
				return Native.ISteamUserStats.GetNumAchievements(_ptr);
			}
			public virtual IntPtr ISteamUserStats_GetAchievementName( uint /*uint32*/ iAchievement )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamUserStats _ptr is null!" );
				
				return Native.ISteamUserStats.GetAchievementName(_ptr, iAchievement /*C*/);
			}
			public virtual SteamAPICall_t /*(SteamAPICall_t)*/ ISteamUserStats_RequestUserStats( ulong steamIDUser )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamUserStats _ptr is null!" );
				
				return Native.ISteamUserStats.RequestUserStats(_ptr, steamIDUser /*C*/);
			}
			public virtual bool /*bool*/ ISteamUserStats_GetUserStat( ulong steamIDUser, string /*const char **/ pchName, out int /*int32 **/ pData )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamUserStats _ptr is null!" );
				
				return Native.ISteamUserStats.GetUserStat(_ptr, steamIDUser /*C*/, pchName /*C*/, out pData /*B*/);
			}
			public virtual bool /*bool*/ ISteamUserStats_GetUserStat0( ulong steamIDUser, string /*const char **/ pchName, out float /*float **/ pData )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamUserStats _ptr is null!" );
				
				return Native.ISteamUserStats.GetUserStat0(_ptr, steamIDUser /*C*/, pchName /*C*/, out pData /*B*/);
			}
			public virtual bool /*bool*/ ISteamUserStats_GetUserAchievement( ulong steamIDUser, string /*const char **/ pchName, [MarshalAs(UnmanagedType.U1)] ref bool /*bool **/ pbAchieved )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamUserStats _ptr is null!" );
				
				return Native.ISteamUserStats.GetUserAchievement(_ptr, steamIDUser /*C*/, pchName /*C*/, ref pbAchieved /*A*/);
			}
			public virtual bool /*bool*/ ISteamUserStats_GetUserAchievementAndUnlockTime( ulong steamIDUser, string /*const char **/ pchName, [MarshalAs(UnmanagedType.U1)] ref bool /*bool **/ pbAchieved, out uint /*uint32 **/ punUnlockTime )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamUserStats _ptr is null!" );
				
				return Native.ISteamUserStats.GetUserAchievementAndUnlockTime(_ptr, steamIDUser /*C*/, pchName /*C*/, ref pbAchieved /*A*/, out punUnlockTime /*B*/);
			}
			public virtual bool /*bool*/ ISteamUserStats_ResetAllStats( [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bAchievementsToo )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamUserStats _ptr is null!" );
				
				return Native.ISteamUserStats.ResetAllStats(_ptr, bAchievementsToo /*C*/);
			}
			public virtual SteamAPICall_t /*(SteamAPICall_t)*/ ISteamUserStats_FindOrCreateLeaderboard( string /*const char **/ pchLeaderboardName, LeaderboardSortMethod /*ELeaderboardSortMethod*/ eLeaderboardSortMethod, LeaderboardDisplayType /*ELeaderboardDisplayType*/ eLeaderboardDisplayType )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamUserStats _ptr is null!" );
				
				return Native.ISteamUserStats.FindOrCreateLeaderboard(_ptr, pchLeaderboardName /*C*/, eLeaderboardSortMethod /*C*/, eLeaderboardDisplayType /*C*/);
			}
			public virtual SteamAPICall_t /*(SteamAPICall_t)*/ ISteamUserStats_FindLeaderboard( string /*const char **/ pchLeaderboardName )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamUserStats _ptr is null!" );
				
				return Native.ISteamUserStats.FindLeaderboard(_ptr, pchLeaderboardName /*C*/);
			}
			public virtual IntPtr ISteamUserStats_GetLeaderboardName( ulong hSteamLeaderboard )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamUserStats _ptr is null!" );
				
				return Native.ISteamUserStats.GetLeaderboardName(_ptr, hSteamLeaderboard /*C*/);
			}
			public virtual int /*int*/ ISteamUserStats_GetLeaderboardEntryCount( ulong hSteamLeaderboard )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamUserStats _ptr is null!" );
				
				return Native.ISteamUserStats.GetLeaderboardEntryCount(_ptr, hSteamLeaderboard /*C*/);
			}
			public virtual LeaderboardSortMethod /*ELeaderboardSortMethod*/ ISteamUserStats_GetLeaderboardSortMethod( ulong hSteamLeaderboard )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamUserStats _ptr is null!" );
				
				return Native.ISteamUserStats.GetLeaderboardSortMethod(_ptr, hSteamLeaderboard /*C*/);
			}
			public virtual LeaderboardDisplayType /*ELeaderboardDisplayType*/ ISteamUserStats_GetLeaderboardDisplayType( ulong hSteamLeaderboard )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamUserStats _ptr is null!" );
				
				return Native.ISteamUserStats.GetLeaderboardDisplayType(_ptr, hSteamLeaderboard /*C*/);
			}
			public virtual SteamAPICall_t /*(SteamAPICall_t)*/ ISteamUserStats_DownloadLeaderboardEntries( ulong hSteamLeaderboard, LeaderboardDataRequest /*ELeaderboardDataRequest*/ eLeaderboardDataRequest, int /*int*/ nRangeStart, int /*int*/ nRangeEnd )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamUserStats _ptr is null!" );
				
				return Native.ISteamUserStats.DownloadLeaderboardEntries(_ptr, hSteamLeaderboard /*C*/, eLeaderboardDataRequest /*C*/, nRangeStart /*C*/, nRangeEnd /*C*/);
			}
			public virtual SteamAPICall_t /*(SteamAPICall_t)*/ ISteamUserStats_DownloadLeaderboardEntriesForUsers( ulong hSteamLeaderboard, IntPtr /*class CSteamID **/ prgUsers, int /*int*/ cUsers )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamUserStats _ptr is null!" );
				
				return Native.ISteamUserStats.DownloadLeaderboardEntriesForUsers(_ptr, hSteamLeaderboard /*C*/, prgUsers, cUsers /*C*/);
			}
			public virtual bool /*bool*/ ISteamUserStats_GetDownloadedLeaderboardEntry( ulong hSteamLeaderboardEntries, int /*int*/ index, ref LeaderboardEntry_t /*struct LeaderboardEntry_t **/ pLeaderboardEntry, IntPtr /*int32 **/ pDetails, int /*int*/ cDetailsMax )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamUserStats _ptr is null!" );
				
				var pLeaderboardEntry_ps = new LeaderboardEntry_t.PackSmall();
				var ret = Native.ISteamUserStats.GetDownloadedLeaderboardEntry(_ptr, hSteamLeaderboardEntries /*C*/, index /*C*/, ref pLeaderboardEntry_ps, pDetails, cDetailsMax /*C*/);
				pLeaderboardEntry = pLeaderboardEntry_ps;
				return ret;
			}
			public virtual SteamAPICall_t /*(SteamAPICall_t)*/ ISteamUserStats_UploadLeaderboardScore( ulong hSteamLeaderboard, LeaderboardUploadScoreMethod /*ELeaderboardUploadScoreMethod*/ eLeaderboardUploadScoreMethod, int /*int32*/ nScore, IntPtr /*const int32 **/ pScoreDetails, int /*int*/ cScoreDetailsCount )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamUserStats _ptr is null!" );
				
				return Native.ISteamUserStats.UploadLeaderboardScore(_ptr, hSteamLeaderboard /*C*/, eLeaderboardUploadScoreMethod /*C*/, nScore /*C*/, pScoreDetails, cScoreDetailsCount /*C*/);
			}
			public virtual SteamAPICall_t /*(SteamAPICall_t)*/ ISteamUserStats_AttachLeaderboardUGC( ulong hSteamLeaderboard, ulong hUGC )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamUserStats _ptr is null!" );
				
				return Native.ISteamUserStats.AttachLeaderboardUGC(_ptr, hSteamLeaderboard /*C*/, hUGC /*C*/);
			}
			public virtual SteamAPICall_t /*(SteamAPICall_t)*/ ISteamUserStats_GetNumberOfCurrentPlayers()
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamUserStats _ptr is null!" );
				
				return Native.ISteamUserStats.GetNumberOfCurrentPlayers(_ptr);
			}
			public virtual SteamAPICall_t /*(SteamAPICall_t)*/ ISteamUserStats_RequestGlobalAchievementPercentages()
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamUserStats _ptr is null!" );
				
				return Native.ISteamUserStats.RequestGlobalAchievementPercentages(_ptr);
			}
			public virtual int /*int*/ ISteamUserStats_GetMostAchievedAchievementInfo( System.Text.StringBuilder /*char **/ pchName, uint /*uint32*/ unNameBufLen, out float /*float **/ pflPercent, [MarshalAs(UnmanagedType.U1)] ref bool /*bool **/ pbAchieved )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamUserStats _ptr is null!" );
				
				return Native.ISteamUserStats.GetMostAchievedAchievementInfo(_ptr, pchName /*C*/, unNameBufLen /*C*/, out pflPercent /*B*/, ref pbAchieved /*A*/);
			}
			public virtual int /*int*/ ISteamUserStats_GetNextMostAchievedAchievementInfo( int /*int*/ iIteratorPrevious, System.Text.StringBuilder /*char **/ pchName, uint /*uint32*/ unNameBufLen, out float /*float **/ pflPercent, [MarshalAs(UnmanagedType.U1)] ref bool /*bool **/ pbAchieved )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamUserStats _ptr is null!" );
				
				return Native.ISteamUserStats.GetNextMostAchievedAchievementInfo(_ptr, iIteratorPrevious /*C*/, pchName /*C*/, unNameBufLen /*C*/, out pflPercent /*B*/, ref pbAchieved /*A*/);
			}
			public virtual bool /*bool*/ ISteamUserStats_GetAchievementAchievedPercent( string /*const char **/ pchName, out float /*float **/ pflPercent )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamUserStats _ptr is null!" );
				
				return Native.ISteamUserStats.GetAchievementAchievedPercent(_ptr, pchName /*C*/, out pflPercent /*B*/);
			}
			public virtual SteamAPICall_t /*(SteamAPICall_t)*/ ISteamUserStats_RequestGlobalStats( int /*int*/ nHistoryDays )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamUserStats _ptr is null!" );
				
				return Native.ISteamUserStats.RequestGlobalStats(_ptr, nHistoryDays /*C*/);
			}
			public virtual bool /*bool*/ ISteamUserStats_GetGlobalStat( string /*const char **/ pchStatName, out long /*int64 **/ pData )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamUserStats _ptr is null!" );
				
				return Native.ISteamUserStats.GetGlobalStat(_ptr, pchStatName /*C*/, out pData /*B*/);
			}
			public virtual bool /*bool*/ ISteamUserStats_GetGlobalStat0( string /*const char **/ pchStatName, out double /*double **/ pData )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamUserStats _ptr is null!" );
				
				return Native.ISteamUserStats.GetGlobalStat0(_ptr, pchStatName /*C*/, out pData /*B*/);
			}
			public virtual int /*int32*/ ISteamUserStats_GetGlobalStatHistory( string /*const char **/ pchStatName, out long /*int64 **/ pData, uint /*uint32*/ cubData )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamUserStats _ptr is null!" );
				
				return Native.ISteamUserStats.GetGlobalStatHistory(_ptr, pchStatName /*C*/, out pData /*B*/, cubData /*C*/);
			}
			public virtual int /*int32*/ ISteamUserStats_GetGlobalStatHistory0( string /*const char **/ pchStatName, out double /*double **/ pData, uint /*uint32*/ cubData )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamUserStats _ptr is null!" );
				
				return Native.ISteamUserStats.GetGlobalStatHistory0(_ptr, pchStatName /*C*/, out pData /*B*/, cubData /*C*/);
			}
			
			public virtual bool /*bool*/ ISteamApps_BIsSubscribed()
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamApps _ptr is null!" );
				
				return Native.ISteamApps.BIsSubscribed(_ptr);
			}
			public virtual bool /*bool*/ ISteamApps_BIsLowViolence()
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamApps _ptr is null!" );
				
				return Native.ISteamApps.BIsLowViolence(_ptr);
			}
			public virtual bool /*bool*/ ISteamApps_BIsCybercafe()
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamApps _ptr is null!" );
				
				return Native.ISteamApps.BIsCybercafe(_ptr);
			}
			public virtual bool /*bool*/ ISteamApps_BIsVACBanned()
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamApps _ptr is null!" );
				
				return Native.ISteamApps.BIsVACBanned(_ptr);
			}
			public virtual IntPtr ISteamApps_GetCurrentGameLanguage()
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamApps _ptr is null!" );
				
				return Native.ISteamApps.GetCurrentGameLanguage(_ptr);
			}
			public virtual IntPtr ISteamApps_GetAvailableGameLanguages()
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamApps _ptr is null!" );
				
				return Native.ISteamApps.GetAvailableGameLanguages(_ptr);
			}
			public virtual bool /*bool*/ ISteamApps_BIsSubscribedApp( uint appID )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamApps _ptr is null!" );
				
				return Native.ISteamApps.BIsSubscribedApp(_ptr, appID /*C*/);
			}
			public virtual bool /*bool*/ ISteamApps_BIsDlcInstalled( uint appID )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamApps _ptr is null!" );
				
				return Native.ISteamApps.BIsDlcInstalled(_ptr, appID /*C*/);
			}
			public virtual uint /*uint32*/ ISteamApps_GetEarliestPurchaseUnixTime( uint nAppID )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamApps _ptr is null!" );
				
				return Native.ISteamApps.GetEarliestPurchaseUnixTime(_ptr, nAppID /*C*/);
			}
			public virtual bool /*bool*/ ISteamApps_BIsSubscribedFromFreeWeekend()
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamApps _ptr is null!" );
				
				return Native.ISteamApps.BIsSubscribedFromFreeWeekend(_ptr);
			}
			public virtual int /*int*/ ISteamApps_GetDLCCount()
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamApps _ptr is null!" );
				
				return Native.ISteamApps.GetDLCCount(_ptr);
			}
			public virtual bool /*bool*/ ISteamApps_BGetDLCDataByIndex( int /*int*/ iDLC, ref uint pAppID, [MarshalAs(UnmanagedType.U1)] ref bool /*bool **/ pbAvailable, System.Text.StringBuilder /*char **/ pchName, int /*int*/ cchNameBufferSize )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamApps _ptr is null!" );
				
				return Native.ISteamApps.BGetDLCDataByIndex(_ptr, iDLC /*C*/, ref pAppID /*A*/, ref pbAvailable /*A*/, pchName /*C*/, cchNameBufferSize /*C*/);
			}
			public virtual void /*void*/ ISteamApps_InstallDLC( uint nAppID )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamApps _ptr is null!" );
				
				Native.ISteamApps.InstallDLC(_ptr, nAppID /*C*/);
			}
			public virtual void /*void*/ ISteamApps_UninstallDLC( uint nAppID )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamApps _ptr is null!" );
				
				Native.ISteamApps.UninstallDLC(_ptr, nAppID /*C*/);
			}
			public virtual void /*void*/ ISteamApps_RequestAppProofOfPurchaseKey( uint nAppID )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamApps _ptr is null!" );
				
				Native.ISteamApps.RequestAppProofOfPurchaseKey(_ptr, nAppID /*C*/);
			}
			public virtual bool /*bool*/ ISteamApps_GetCurrentBetaName( System.Text.StringBuilder /*char **/ pchName, int /*int*/ cchNameBufferSize )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamApps _ptr is null!" );
				
				return Native.ISteamApps.GetCurrentBetaName(_ptr, pchName /*C*/, cchNameBufferSize /*C*/);
			}
			public virtual bool /*bool*/ ISteamApps_MarkContentCorrupt( [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bMissingFilesOnly )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamApps _ptr is null!" );
				
				return Native.ISteamApps.MarkContentCorrupt(_ptr, bMissingFilesOnly /*C*/);
			}
			public virtual uint /*uint32*/ ISteamApps_GetInstalledDepots( uint appID, IntPtr /*DepotId_t **/ pvecDepots, uint /*uint32*/ cMaxDepots )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamApps _ptr is null!" );
				
				return Native.ISteamApps.GetInstalledDepots(_ptr, appID /*C*/, pvecDepots, cMaxDepots /*C*/);
			}
			public virtual uint /*uint32*/ ISteamApps_GetAppInstallDir( uint appID, System.Text.StringBuilder /*char **/ pchFolder, uint /*uint32*/ cchFolderBufferSize )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamApps _ptr is null!" );
				
				return Native.ISteamApps.GetAppInstallDir(_ptr, appID /*C*/, pchFolder /*C*/, cchFolderBufferSize /*C*/);
			}
			public virtual bool /*bool*/ ISteamApps_BIsAppInstalled( uint appID )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamApps _ptr is null!" );
				
				return Native.ISteamApps.BIsAppInstalled(_ptr, appID /*C*/);
			}
			public virtual CSteamID /*(class CSteamID)*/ ISteamApps_GetAppOwner()
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamApps _ptr is null!" );
				
				return Native.ISteamApps.GetAppOwner(_ptr);
			}
			public virtual IntPtr ISteamApps_GetLaunchQueryParam( string /*const char **/ pchKey )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamApps _ptr is null!" );
				
				return Native.ISteamApps.GetLaunchQueryParam(_ptr, pchKey /*C*/);
			}
			public virtual bool /*bool*/ ISteamApps_GetDlcDownloadProgress( uint nAppID, out ulong /*uint64 **/ punBytesDownloaded, out ulong /*uint64 **/ punBytesTotal )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamApps _ptr is null!" );
				
				return Native.ISteamApps.GetDlcDownloadProgress(_ptr, nAppID /*C*/, out punBytesDownloaded /*B*/, out punBytesTotal /*B*/);
			}
			public virtual int /*int*/ ISteamApps_GetAppBuildId()
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamApps _ptr is null!" );
				
				return Native.ISteamApps.GetAppBuildId(_ptr);
			}
			public virtual void /*void*/ ISteamApps_RequestAllProofOfPurchaseKeys()
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamApps _ptr is null!" );
				
				Native.ISteamApps.RequestAllProofOfPurchaseKeys(_ptr);
			}
			
			public virtual bool /*bool*/ ISteamNetworking_SendP2PPacket( ulong steamIDRemote, IntPtr /*const void **/ pubData, uint /*uint32*/ cubData, P2PSend /*EP2PSend*/ eP2PSendType, int /*int*/ nChannel )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamNetworking _ptr is null!" );
				
				return Native.ISteamNetworking.SendP2PPacket(_ptr, steamIDRemote /*C*/, pubData /*C*/, cubData /*C*/, eP2PSendType /*C*/, nChannel /*C*/);
			}
			public virtual bool /*bool*/ ISteamNetworking_IsP2PPacketAvailable( out uint /*uint32 **/ pcubMsgSize, int /*int*/ nChannel )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamNetworking _ptr is null!" );
				
				return Native.ISteamNetworking.IsP2PPacketAvailable(_ptr, out pcubMsgSize /*B*/, nChannel /*C*/);
			}
			public virtual bool /*bool*/ ISteamNetworking_ReadP2PPacket( IntPtr /*void **/ pubDest, uint /*uint32*/ cubDest, out uint /*uint32 **/ pcubMsgSize, out ulong psteamIDRemote, int /*int*/ nChannel )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamNetworking _ptr is null!" );
				
				return Native.ISteamNetworking.ReadP2PPacket(_ptr, pubDest /*C*/, cubDest /*C*/, out pcubMsgSize /*B*/, out psteamIDRemote /*B*/, nChannel /*C*/);
			}
			public virtual bool /*bool*/ ISteamNetworking_AcceptP2PSessionWithUser( ulong steamIDRemote )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamNetworking _ptr is null!" );
				
				return Native.ISteamNetworking.AcceptP2PSessionWithUser(_ptr, steamIDRemote /*C*/);
			}
			public virtual bool /*bool*/ ISteamNetworking_CloseP2PSessionWithUser( ulong steamIDRemote )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamNetworking _ptr is null!" );
				
				return Native.ISteamNetworking.CloseP2PSessionWithUser(_ptr, steamIDRemote /*C*/);
			}
			public virtual bool /*bool*/ ISteamNetworking_CloseP2PChannelWithUser( ulong steamIDRemote, int /*int*/ nChannel )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamNetworking _ptr is null!" );
				
				return Native.ISteamNetworking.CloseP2PChannelWithUser(_ptr, steamIDRemote /*C*/, nChannel /*C*/);
			}
			public virtual bool /*bool*/ ISteamNetworking_GetP2PSessionState( ulong steamIDRemote, ref P2PSessionState_t /*struct P2PSessionState_t **/ pConnectionState )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamNetworking _ptr is null!" );
				
				var pConnectionState_ps = new P2PSessionState_t.PackSmall();
				var ret = Native.ISteamNetworking.GetP2PSessionState(_ptr, steamIDRemote /*C*/, ref pConnectionState_ps);
				pConnectionState = pConnectionState_ps;
				return ret;
			}
			public virtual bool /*bool*/ ISteamNetworking_AllowP2PPacketRelay( [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bAllow )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamNetworking _ptr is null!" );
				
				return Native.ISteamNetworking.AllowP2PPacketRelay(_ptr, bAllow /*C*/);
			}
			public virtual SNetListenSocket_t /*(SNetListenSocket_t)*/ ISteamNetworking_CreateListenSocket( int /*int*/ nVirtualP2PPort, uint /*uint32*/ nIP, ushort /*uint16*/ nPort, [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bAllowUseOfPacketRelay )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamNetworking _ptr is null!" );
				
				return Native.ISteamNetworking.CreateListenSocket(_ptr, nVirtualP2PPort /*C*/, nIP /*C*/, nPort /*C*/, bAllowUseOfPacketRelay /*C*/);
			}
			public virtual SNetSocket_t /*(SNetSocket_t)*/ ISteamNetworking_CreateP2PConnectionSocket( ulong steamIDTarget, int /*int*/ nVirtualPort, int /*int*/ nTimeoutSec, [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bAllowUseOfPacketRelay )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamNetworking _ptr is null!" );
				
				return Native.ISteamNetworking.CreateP2PConnectionSocket(_ptr, steamIDTarget /*C*/, nVirtualPort /*C*/, nTimeoutSec /*C*/, bAllowUseOfPacketRelay /*C*/);
			}
			public virtual SNetSocket_t /*(SNetSocket_t)*/ ISteamNetworking_CreateConnectionSocket( uint /*uint32*/ nIP, ushort /*uint16*/ nPort, int /*int*/ nTimeoutSec )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamNetworking _ptr is null!" );
				
				return Native.ISteamNetworking.CreateConnectionSocket(_ptr, nIP /*C*/, nPort /*C*/, nTimeoutSec /*C*/);
			}
			public virtual bool /*bool*/ ISteamNetworking_DestroySocket( uint hSocket, [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bNotifyRemoteEnd )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamNetworking _ptr is null!" );
				
				return Native.ISteamNetworking.DestroySocket(_ptr, hSocket /*C*/, bNotifyRemoteEnd /*C*/);
			}
			public virtual bool /*bool*/ ISteamNetworking_DestroyListenSocket( uint hSocket, [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bNotifyRemoteEnd )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamNetworking _ptr is null!" );
				
				return Native.ISteamNetworking.DestroyListenSocket(_ptr, hSocket /*C*/, bNotifyRemoteEnd /*C*/);
			}
			public virtual bool /*bool*/ ISteamNetworking_SendDataOnSocket( uint hSocket, IntPtr /*void **/ pubData, uint /*uint32*/ cubData, [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bReliable )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamNetworking _ptr is null!" );
				
				return Native.ISteamNetworking.SendDataOnSocket(_ptr, hSocket /*C*/, pubData /*C*/, cubData /*C*/, bReliable /*C*/);
			}
			public virtual bool /*bool*/ ISteamNetworking_IsDataAvailableOnSocket( uint hSocket, out uint /*uint32 **/ pcubMsgSize )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamNetworking _ptr is null!" );
				
				return Native.ISteamNetworking.IsDataAvailableOnSocket(_ptr, hSocket /*C*/, out pcubMsgSize /*B*/);
			}
			public virtual bool /*bool*/ ISteamNetworking_RetrieveDataFromSocket( uint hSocket, IntPtr /*void **/ pubDest, uint /*uint32*/ cubDest, out uint /*uint32 **/ pcubMsgSize )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamNetworking _ptr is null!" );
				
				return Native.ISteamNetworking.RetrieveDataFromSocket(_ptr, hSocket /*C*/, pubDest /*C*/, cubDest /*C*/, out pcubMsgSize /*B*/);
			}
			public virtual bool /*bool*/ ISteamNetworking_IsDataAvailable( uint hListenSocket, out uint /*uint32 **/ pcubMsgSize, ref uint phSocket )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamNetworking _ptr is null!" );
				
				return Native.ISteamNetworking.IsDataAvailable(_ptr, hListenSocket /*C*/, out pcubMsgSize /*B*/, ref phSocket /*A*/);
			}
			public virtual bool /*bool*/ ISteamNetworking_RetrieveData( uint hListenSocket, IntPtr /*void **/ pubDest, uint /*uint32*/ cubDest, out uint /*uint32 **/ pcubMsgSize, ref uint phSocket )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamNetworking _ptr is null!" );
				
				return Native.ISteamNetworking.RetrieveData(_ptr, hListenSocket /*C*/, pubDest /*C*/, cubDest /*C*/, out pcubMsgSize /*B*/, ref phSocket /*A*/);
			}
			public virtual bool /*bool*/ ISteamNetworking_GetSocketInfo( uint hSocket, out ulong pSteamIDRemote, IntPtr /*int **/ peSocketStatus, out uint /*uint32 **/ punIPRemote, out ushort /*uint16 **/ punPortRemote )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamNetworking _ptr is null!" );
				
				return Native.ISteamNetworking.GetSocketInfo(_ptr, hSocket /*C*/, out pSteamIDRemote /*B*/, peSocketStatus, out punIPRemote /*B*/, out punPortRemote /*B*/);
			}
			public virtual bool /*bool*/ ISteamNetworking_GetListenSocketInfo( uint hListenSocket, out uint /*uint32 **/ pnIP, out ushort /*uint16 **/ pnPort )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamNetworking _ptr is null!" );
				
				return Native.ISteamNetworking.GetListenSocketInfo(_ptr, hListenSocket /*C*/, out pnIP /*B*/, out pnPort /*B*/);
			}
			public virtual SNetSocketConnectionType /*ESNetSocketConnectionType*/ ISteamNetworking_GetSocketConnectionType( uint hSocket )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamNetworking _ptr is null!" );
				
				return Native.ISteamNetworking.GetSocketConnectionType(_ptr, hSocket /*C*/);
			}
			public virtual int /*int*/ ISteamNetworking_GetMaxPacketSize( uint hSocket )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamNetworking _ptr is null!" );
				
				return Native.ISteamNetworking.GetMaxPacketSize(_ptr, hSocket /*C*/);
			}
			
			public virtual ScreenshotHandle /*(ScreenshotHandle)*/ ISteamScreenshots_WriteScreenshot( IntPtr /*void **/ pubRGB, uint /*uint32*/ cubRGB, int /*int*/ nWidth, int /*int*/ nHeight )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamScreenshots _ptr is null!" );
				
				return Native.ISteamScreenshots.WriteScreenshot(_ptr, pubRGB /*C*/, cubRGB /*C*/, nWidth /*C*/, nHeight /*C*/);
			}
			public virtual ScreenshotHandle /*(ScreenshotHandle)*/ ISteamScreenshots_AddScreenshotToLibrary( string /*const char **/ pchFilename, string /*const char **/ pchThumbnailFilename, int /*int*/ nWidth, int /*int*/ nHeight )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamScreenshots _ptr is null!" );
				
				return Native.ISteamScreenshots.AddScreenshotToLibrary(_ptr, pchFilename /*C*/, pchThumbnailFilename /*C*/, nWidth /*C*/, nHeight /*C*/);
			}
			public virtual void /*void*/ ISteamScreenshots_TriggerScreenshot()
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamScreenshots _ptr is null!" );
				
				Native.ISteamScreenshots.TriggerScreenshot(_ptr);
			}
			public virtual void /*void*/ ISteamScreenshots_HookScreenshots( [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bHook )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamScreenshots _ptr is null!" );
				
				Native.ISteamScreenshots.HookScreenshots(_ptr, bHook /*C*/);
			}
			public virtual bool /*bool*/ ISteamScreenshots_SetLocation( uint hScreenshot, string /*const char **/ pchLocation )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamScreenshots _ptr is null!" );
				
				return Native.ISteamScreenshots.SetLocation(_ptr, hScreenshot /*C*/, pchLocation /*C*/);
			}
			public virtual bool /*bool*/ ISteamScreenshots_TagUser( uint hScreenshot, ulong steamID )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamScreenshots _ptr is null!" );
				
				return Native.ISteamScreenshots.TagUser(_ptr, hScreenshot /*C*/, steamID /*C*/);
			}
			public virtual bool /*bool*/ ISteamScreenshots_TagPublishedFile( uint hScreenshot, ulong unPublishedFileID )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamScreenshots _ptr is null!" );
				
				return Native.ISteamScreenshots.TagPublishedFile(_ptr, hScreenshot /*C*/, unPublishedFileID /*C*/);
			}
			
			public virtual bool /*bool*/ ISteamMusic_BIsEnabled()
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamMusic _ptr is null!" );
				
				return Native.ISteamMusic.BIsEnabled(_ptr);
			}
			public virtual bool /*bool*/ ISteamMusic_BIsPlaying()
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamMusic _ptr is null!" );
				
				return Native.ISteamMusic.BIsPlaying(_ptr);
			}
			public virtual AudioPlayback_Status /*AudioPlayback_Status*/ ISteamMusic_GetPlaybackStatus()
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamMusic _ptr is null!" );
				
				return Native.ISteamMusic.GetPlaybackStatus(_ptr);
			}
			public virtual void /*void*/ ISteamMusic_Play()
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamMusic _ptr is null!" );
				
				Native.ISteamMusic.Play(_ptr);
			}
			public virtual void /*void*/ ISteamMusic_Pause()
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamMusic _ptr is null!" );
				
				Native.ISteamMusic.Pause(_ptr);
			}
			public virtual void /*void*/ ISteamMusic_PlayPrevious()
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamMusic _ptr is null!" );
				
				Native.ISteamMusic.PlayPrevious(_ptr);
			}
			public virtual void /*void*/ ISteamMusic_PlayNext()
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamMusic _ptr is null!" );
				
				Native.ISteamMusic.PlayNext(_ptr);
			}
			public virtual void /*void*/ ISteamMusic_SetVolume( float /*float*/ flVolume )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamMusic _ptr is null!" );
				
				Native.ISteamMusic.SetVolume(_ptr, flVolume /*C*/);
			}
			public virtual float /*float*/ ISteamMusic_GetVolume()
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamMusic _ptr is null!" );
				
				return Native.ISteamMusic.GetVolume(_ptr);
			}
			
			public virtual bool /*bool*/ ISteamMusicRemote_RegisterSteamMusicRemote( string /*const char **/ pchName )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamMusicRemote _ptr is null!" );
				
				return Native.ISteamMusicRemote.RegisterSteamMusicRemote(_ptr, pchName /*C*/);
			}
			public virtual bool /*bool*/ ISteamMusicRemote_DeregisterSteamMusicRemote()
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamMusicRemote _ptr is null!" );
				
				return Native.ISteamMusicRemote.DeregisterSteamMusicRemote(_ptr);
			}
			public virtual bool /*bool*/ ISteamMusicRemote_BIsCurrentMusicRemote()
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamMusicRemote _ptr is null!" );
				
				return Native.ISteamMusicRemote.BIsCurrentMusicRemote(_ptr);
			}
			public virtual bool /*bool*/ ISteamMusicRemote_BActivationSuccess( [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bValue )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamMusicRemote _ptr is null!" );
				
				return Native.ISteamMusicRemote.BActivationSuccess(_ptr, bValue /*C*/);
			}
			public virtual bool /*bool*/ ISteamMusicRemote_SetDisplayName( string /*const char **/ pchDisplayName )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamMusicRemote _ptr is null!" );
				
				return Native.ISteamMusicRemote.SetDisplayName(_ptr, pchDisplayName /*C*/);
			}
			public virtual bool /*bool*/ ISteamMusicRemote_SetPNGIcon_64x64( IntPtr /*void **/ pvBuffer, uint /*uint32*/ cbBufferLength )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamMusicRemote _ptr is null!" );
				
				return Native.ISteamMusicRemote.SetPNGIcon_64x64(_ptr, pvBuffer /*C*/, cbBufferLength /*C*/);
			}
			public virtual bool /*bool*/ ISteamMusicRemote_EnablePlayPrevious( [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bValue )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamMusicRemote _ptr is null!" );
				
				return Native.ISteamMusicRemote.EnablePlayPrevious(_ptr, bValue /*C*/);
			}
			public virtual bool /*bool*/ ISteamMusicRemote_EnablePlayNext( [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bValue )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamMusicRemote _ptr is null!" );
				
				return Native.ISteamMusicRemote.EnablePlayNext(_ptr, bValue /*C*/);
			}
			public virtual bool /*bool*/ ISteamMusicRemote_EnableShuffled( [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bValue )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamMusicRemote _ptr is null!" );
				
				return Native.ISteamMusicRemote.EnableShuffled(_ptr, bValue /*C*/);
			}
			public virtual bool /*bool*/ ISteamMusicRemote_EnableLooped( [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bValue )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamMusicRemote _ptr is null!" );
				
				return Native.ISteamMusicRemote.EnableLooped(_ptr, bValue /*C*/);
			}
			public virtual bool /*bool*/ ISteamMusicRemote_EnableQueue( [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bValue )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamMusicRemote _ptr is null!" );
				
				return Native.ISteamMusicRemote.EnableQueue(_ptr, bValue /*C*/);
			}
			public virtual bool /*bool*/ ISteamMusicRemote_EnablePlaylists( [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bValue )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamMusicRemote _ptr is null!" );
				
				return Native.ISteamMusicRemote.EnablePlaylists(_ptr, bValue /*C*/);
			}
			public virtual bool /*bool*/ ISteamMusicRemote_UpdatePlaybackStatus( AudioPlayback_Status /*AudioPlayback_Status*/ nStatus )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamMusicRemote _ptr is null!" );
				
				return Native.ISteamMusicRemote.UpdatePlaybackStatus(_ptr, nStatus /*C*/);
			}
			public virtual bool /*bool*/ ISteamMusicRemote_UpdateShuffled( [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bValue )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamMusicRemote _ptr is null!" );
				
				return Native.ISteamMusicRemote.UpdateShuffled(_ptr, bValue /*C*/);
			}
			public virtual bool /*bool*/ ISteamMusicRemote_UpdateLooped( [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bValue )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamMusicRemote _ptr is null!" );
				
				return Native.ISteamMusicRemote.UpdateLooped(_ptr, bValue /*C*/);
			}
			public virtual bool /*bool*/ ISteamMusicRemote_UpdateVolume( float /*float*/ flValue )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamMusicRemote _ptr is null!" );
				
				return Native.ISteamMusicRemote.UpdateVolume(_ptr, flValue /*C*/);
			}
			public virtual bool /*bool*/ ISteamMusicRemote_CurrentEntryWillChange()
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamMusicRemote _ptr is null!" );
				
				return Native.ISteamMusicRemote.CurrentEntryWillChange(_ptr);
			}
			public virtual bool /*bool*/ ISteamMusicRemote_CurrentEntryIsAvailable( [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bAvailable )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamMusicRemote _ptr is null!" );
				
				return Native.ISteamMusicRemote.CurrentEntryIsAvailable(_ptr, bAvailable /*C*/);
			}
			public virtual bool /*bool*/ ISteamMusicRemote_UpdateCurrentEntryText( string /*const char **/ pchText )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamMusicRemote _ptr is null!" );
				
				return Native.ISteamMusicRemote.UpdateCurrentEntryText(_ptr, pchText /*C*/);
			}
			public virtual bool /*bool*/ ISteamMusicRemote_UpdateCurrentEntryElapsedSeconds( int /*int*/ nValue )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamMusicRemote _ptr is null!" );
				
				return Native.ISteamMusicRemote.UpdateCurrentEntryElapsedSeconds(_ptr, nValue /*C*/);
			}
			public virtual bool /*bool*/ ISteamMusicRemote_UpdateCurrentEntryCoverArt( IntPtr /*void **/ pvBuffer, uint /*uint32*/ cbBufferLength )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamMusicRemote _ptr is null!" );
				
				return Native.ISteamMusicRemote.UpdateCurrentEntryCoverArt(_ptr, pvBuffer /*C*/, cbBufferLength /*C*/);
			}
			public virtual bool /*bool*/ ISteamMusicRemote_CurrentEntryDidChange()
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamMusicRemote _ptr is null!" );
				
				return Native.ISteamMusicRemote.CurrentEntryDidChange(_ptr);
			}
			public virtual bool /*bool*/ ISteamMusicRemote_QueueWillChange()
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamMusicRemote _ptr is null!" );
				
				return Native.ISteamMusicRemote.QueueWillChange(_ptr);
			}
			public virtual bool /*bool*/ ISteamMusicRemote_ResetQueueEntries()
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamMusicRemote _ptr is null!" );
				
				return Native.ISteamMusicRemote.ResetQueueEntries(_ptr);
			}
			public virtual bool /*bool*/ ISteamMusicRemote_SetQueueEntry( int /*int*/ nID, int /*int*/ nPosition, string /*const char **/ pchEntryText )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamMusicRemote _ptr is null!" );
				
				return Native.ISteamMusicRemote.SetQueueEntry(_ptr, nID /*C*/, nPosition /*C*/, pchEntryText /*C*/);
			}
			public virtual bool /*bool*/ ISteamMusicRemote_SetCurrentQueueEntry( int /*int*/ nID )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamMusicRemote _ptr is null!" );
				
				return Native.ISteamMusicRemote.SetCurrentQueueEntry(_ptr, nID /*C*/);
			}
			public virtual bool /*bool*/ ISteamMusicRemote_QueueDidChange()
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamMusicRemote _ptr is null!" );
				
				return Native.ISteamMusicRemote.QueueDidChange(_ptr);
			}
			public virtual bool /*bool*/ ISteamMusicRemote_PlaylistWillChange()
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamMusicRemote _ptr is null!" );
				
				return Native.ISteamMusicRemote.PlaylistWillChange(_ptr);
			}
			public virtual bool /*bool*/ ISteamMusicRemote_ResetPlaylistEntries()
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamMusicRemote _ptr is null!" );
				
				return Native.ISteamMusicRemote.ResetPlaylistEntries(_ptr);
			}
			public virtual bool /*bool*/ ISteamMusicRemote_SetPlaylistEntry( int /*int*/ nID, int /*int*/ nPosition, string /*const char **/ pchEntryText )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamMusicRemote _ptr is null!" );
				
				return Native.ISteamMusicRemote.SetPlaylistEntry(_ptr, nID /*C*/, nPosition /*C*/, pchEntryText /*C*/);
			}
			public virtual bool /*bool*/ ISteamMusicRemote_SetCurrentPlaylistEntry( int /*int*/ nID )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamMusicRemote _ptr is null!" );
				
				return Native.ISteamMusicRemote.SetCurrentPlaylistEntry(_ptr, nID /*C*/);
			}
			public virtual bool /*bool*/ ISteamMusicRemote_PlaylistDidChange()
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamMusicRemote _ptr is null!" );
				
				return Native.ISteamMusicRemote.PlaylistDidChange(_ptr);
			}
			
			public virtual HTTPRequestHandle /*(HTTPRequestHandle)*/ ISteamHTTP_CreateHTTPRequest( HTTPMethod /*EHTTPMethod*/ eHTTPRequestMethod, string /*const char **/ pchAbsoluteURL )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamHTTP _ptr is null!" );
				
				return Native.ISteamHTTP.CreateHTTPRequest(_ptr, eHTTPRequestMethod /*C*/, pchAbsoluteURL /*C*/);
			}
			public virtual bool /*bool*/ ISteamHTTP_SetHTTPRequestContextValue( uint hRequest, ulong /*uint64*/ ulContextValue )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamHTTP _ptr is null!" );
				
				return Native.ISteamHTTP.SetHTTPRequestContextValue(_ptr, hRequest /*C*/, ulContextValue /*C*/);
			}
			public virtual bool /*bool*/ ISteamHTTP_SetHTTPRequestNetworkActivityTimeout( uint hRequest, uint /*uint32*/ unTimeoutSeconds )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamHTTP _ptr is null!" );
				
				return Native.ISteamHTTP.SetHTTPRequestNetworkActivityTimeout(_ptr, hRequest /*C*/, unTimeoutSeconds /*C*/);
			}
			public virtual bool /*bool*/ ISteamHTTP_SetHTTPRequestHeaderValue( uint hRequest, string /*const char **/ pchHeaderName, string /*const char **/ pchHeaderValue )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamHTTP _ptr is null!" );
				
				return Native.ISteamHTTP.SetHTTPRequestHeaderValue(_ptr, hRequest /*C*/, pchHeaderName /*C*/, pchHeaderValue /*C*/);
			}
			public virtual bool /*bool*/ ISteamHTTP_SetHTTPRequestGetOrPostParameter( uint hRequest, string /*const char **/ pchParamName, string /*const char **/ pchParamValue )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamHTTP _ptr is null!" );
				
				return Native.ISteamHTTP.SetHTTPRequestGetOrPostParameter(_ptr, hRequest /*C*/, pchParamName /*C*/, pchParamValue /*C*/);
			}
			public virtual bool /*bool*/ ISteamHTTP_SendHTTPRequest( uint hRequest, ref ulong pCallHandle )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamHTTP _ptr is null!" );
				
				return Native.ISteamHTTP.SendHTTPRequest(_ptr, hRequest /*C*/, ref pCallHandle /*A*/);
			}
			public virtual bool /*bool*/ ISteamHTTP_SendHTTPRequestAndStreamResponse( uint hRequest, ref ulong pCallHandle )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamHTTP _ptr is null!" );
				
				return Native.ISteamHTTP.SendHTTPRequestAndStreamResponse(_ptr, hRequest /*C*/, ref pCallHandle /*A*/);
			}
			public virtual bool /*bool*/ ISteamHTTP_DeferHTTPRequest( uint hRequest )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamHTTP _ptr is null!" );
				
				return Native.ISteamHTTP.DeferHTTPRequest(_ptr, hRequest /*C*/);
			}
			public virtual bool /*bool*/ ISteamHTTP_PrioritizeHTTPRequest( uint hRequest )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamHTTP _ptr is null!" );
				
				return Native.ISteamHTTP.PrioritizeHTTPRequest(_ptr, hRequest /*C*/);
			}
			public virtual bool /*bool*/ ISteamHTTP_GetHTTPResponseHeaderSize( uint hRequest, string /*const char **/ pchHeaderName, out uint /*uint32 **/ unResponseHeaderSize )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamHTTP _ptr is null!" );
				
				return Native.ISteamHTTP.GetHTTPResponseHeaderSize(_ptr, hRequest /*C*/, pchHeaderName /*C*/, out unResponseHeaderSize /*B*/);
			}
			public virtual bool /*bool*/ ISteamHTTP_GetHTTPResponseHeaderValue( uint hRequest, string /*const char **/ pchHeaderName, out byte /*uint8 **/ pHeaderValueBuffer, uint /*uint32*/ unBufferSize )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamHTTP _ptr is null!" );
				
				return Native.ISteamHTTP.GetHTTPResponseHeaderValue(_ptr, hRequest /*C*/, pchHeaderName /*C*/, out pHeaderValueBuffer /*B*/, unBufferSize /*C*/);
			}
			public virtual bool /*bool*/ ISteamHTTP_GetHTTPResponseBodySize( uint hRequest, out uint /*uint32 **/ unBodySize )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamHTTP _ptr is null!" );
				
				return Native.ISteamHTTP.GetHTTPResponseBodySize(_ptr, hRequest /*C*/, out unBodySize /*B*/);
			}
			public virtual bool /*bool*/ ISteamHTTP_GetHTTPResponseBodyData( uint hRequest, out byte /*uint8 **/ pBodyDataBuffer, uint /*uint32*/ unBufferSize )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamHTTP _ptr is null!" );
				
				return Native.ISteamHTTP.GetHTTPResponseBodyData(_ptr, hRequest /*C*/, out pBodyDataBuffer /*B*/, unBufferSize /*C*/);
			}
			public virtual bool /*bool*/ ISteamHTTP_GetHTTPStreamingResponseBodyData( uint hRequest, uint /*uint32*/ cOffset, out byte /*uint8 **/ pBodyDataBuffer, uint /*uint32*/ unBufferSize )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamHTTP _ptr is null!" );
				
				return Native.ISteamHTTP.GetHTTPStreamingResponseBodyData(_ptr, hRequest /*C*/, cOffset /*C*/, out pBodyDataBuffer /*B*/, unBufferSize /*C*/);
			}
			public virtual bool /*bool*/ ISteamHTTP_ReleaseHTTPRequest( uint hRequest )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamHTTP _ptr is null!" );
				
				return Native.ISteamHTTP.ReleaseHTTPRequest(_ptr, hRequest /*C*/);
			}
			public virtual bool /*bool*/ ISteamHTTP_GetHTTPDownloadProgressPct( uint hRequest, out float /*float **/ pflPercentOut )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamHTTP _ptr is null!" );
				
				return Native.ISteamHTTP.GetHTTPDownloadProgressPct(_ptr, hRequest /*C*/, out pflPercentOut /*B*/);
			}
			public virtual bool /*bool*/ ISteamHTTP_SetHTTPRequestRawPostBody( uint hRequest, string /*const char **/ pchContentType, out byte /*uint8 **/ pubBody, uint /*uint32*/ unBodyLen )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamHTTP _ptr is null!" );
				
				return Native.ISteamHTTP.SetHTTPRequestRawPostBody(_ptr, hRequest /*C*/, pchContentType /*C*/, out pubBody /*B*/, unBodyLen /*C*/);
			}
			public virtual HTTPCookieContainerHandle /*(HTTPCookieContainerHandle)*/ ISteamHTTP_CreateCookieContainer( [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bAllowResponsesToModify )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamHTTP _ptr is null!" );
				
				return Native.ISteamHTTP.CreateCookieContainer(_ptr, bAllowResponsesToModify /*C*/);
			}
			public virtual bool /*bool*/ ISteamHTTP_ReleaseCookieContainer( uint hCookieContainer )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamHTTP _ptr is null!" );
				
				return Native.ISteamHTTP.ReleaseCookieContainer(_ptr, hCookieContainer /*C*/);
			}
			public virtual bool /*bool*/ ISteamHTTP_SetCookie( uint hCookieContainer, string /*const char **/ pchHost, string /*const char **/ pchUrl, string /*const char **/ pchCookie )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamHTTP _ptr is null!" );
				
				return Native.ISteamHTTP.SetCookie(_ptr, hCookieContainer /*C*/, pchHost /*C*/, pchUrl /*C*/, pchCookie /*C*/);
			}
			public virtual bool /*bool*/ ISteamHTTP_SetHTTPRequestCookieContainer( uint hRequest, uint hCookieContainer )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamHTTP _ptr is null!" );
				
				return Native.ISteamHTTP.SetHTTPRequestCookieContainer(_ptr, hRequest /*C*/, hCookieContainer /*C*/);
			}
			public virtual bool /*bool*/ ISteamHTTP_SetHTTPRequestUserAgentInfo( uint hRequest, string /*const char **/ pchUserAgentInfo )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamHTTP _ptr is null!" );
				
				return Native.ISteamHTTP.SetHTTPRequestUserAgentInfo(_ptr, hRequest /*C*/, pchUserAgentInfo /*C*/);
			}
			public virtual bool /*bool*/ ISteamHTTP_SetHTTPRequestRequiresVerifiedCertificate( uint hRequest, [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bRequireVerifiedCertificate )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamHTTP _ptr is null!" );
				
				return Native.ISteamHTTP.SetHTTPRequestRequiresVerifiedCertificate(_ptr, hRequest /*C*/, bRequireVerifiedCertificate /*C*/);
			}
			public virtual bool /*bool*/ ISteamHTTP_SetHTTPRequestAbsoluteTimeoutMS( uint hRequest, uint /*uint32*/ unMilliseconds )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamHTTP _ptr is null!" );
				
				return Native.ISteamHTTP.SetHTTPRequestAbsoluteTimeoutMS(_ptr, hRequest /*C*/, unMilliseconds /*C*/);
			}
			public virtual bool /*bool*/ ISteamHTTP_GetHTTPRequestWasTimedOut( uint hRequest, [MarshalAs(UnmanagedType.U1)] ref bool /*bool **/ pbWasTimedOut )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamHTTP _ptr is null!" );
				
				return Native.ISteamHTTP.GetHTTPRequestWasTimedOut(_ptr, hRequest /*C*/, ref pbWasTimedOut /*A*/);
			}
			
			public virtual ClientUnifiedMessageHandle /*(ClientUnifiedMessageHandle)*/ ISteamUnifiedMessages_SendMethod( string /*const char **/ pchServiceMethod, IntPtr /*const void **/ pRequestBuffer, uint /*uint32*/ unRequestBufferSize, ulong /*uint64*/ unContext )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamUnifiedMessages _ptr is null!" );
				
				return Native.ISteamUnifiedMessages.SendMethod(_ptr, pchServiceMethod /*C*/, pRequestBuffer /*C*/, unRequestBufferSize /*C*/, unContext /*C*/);
			}
			public virtual bool /*bool*/ ISteamUnifiedMessages_GetMethodResponseInfo( ulong hHandle, out uint /*uint32 **/ punResponseSize, out Result /*EResult **/ peResult )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamUnifiedMessages _ptr is null!" );
				
				return Native.ISteamUnifiedMessages.GetMethodResponseInfo(_ptr, hHandle /*C*/, out punResponseSize /*B*/, out peResult /*B*/);
			}
			public virtual bool /*bool*/ ISteamUnifiedMessages_GetMethodResponseData( ulong hHandle, IntPtr /*void **/ pResponseBuffer, uint /*uint32*/ unResponseBufferSize, [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bAutoRelease )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamUnifiedMessages _ptr is null!" );
				
				return Native.ISteamUnifiedMessages.GetMethodResponseData(_ptr, hHandle /*C*/, pResponseBuffer /*C*/, unResponseBufferSize /*C*/, bAutoRelease /*C*/);
			}
			public virtual bool /*bool*/ ISteamUnifiedMessages_ReleaseMethod( ulong hHandle )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamUnifiedMessages _ptr is null!" );
				
				return Native.ISteamUnifiedMessages.ReleaseMethod(_ptr, hHandle /*C*/);
			}
			public virtual bool /*bool*/ ISteamUnifiedMessages_SendNotification( string /*const char **/ pchServiceNotification, IntPtr /*const void **/ pNotificationBuffer, uint /*uint32*/ unNotificationBufferSize )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamUnifiedMessages _ptr is null!" );
				
				return Native.ISteamUnifiedMessages.SendNotification(_ptr, pchServiceNotification /*C*/, pNotificationBuffer /*C*/, unNotificationBufferSize /*C*/);
			}
			
			public virtual bool /*bool*/ ISteamController_Init()
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamController _ptr is null!" );
				
				return Native.ISteamController.Init(_ptr);
			}
			public virtual bool /*bool*/ ISteamController_Shutdown()
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamController _ptr is null!" );
				
				return Native.ISteamController.Shutdown(_ptr);
			}
			public virtual void /*void*/ ISteamController_RunFrame()
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamController _ptr is null!" );
				
				Native.ISteamController.RunFrame(_ptr);
			}
			public virtual int /*int*/ ISteamController_GetConnectedControllers( IntPtr /*ControllerHandle_t **/ handlesOut )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamController _ptr is null!" );
				
				return Native.ISteamController.GetConnectedControllers(_ptr, handlesOut);
			}
			public virtual bool /*bool*/ ISteamController_ShowBindingPanel( ulong controllerHandle )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamController _ptr is null!" );
				
				return Native.ISteamController.ShowBindingPanel(_ptr, controllerHandle /*C*/);
			}
			public virtual ControllerActionSetHandle_t /*(ControllerActionSetHandle_t)*/ ISteamController_GetActionSetHandle( string /*const char **/ pszActionSetName )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamController _ptr is null!" );
				
				return Native.ISteamController.GetActionSetHandle(_ptr, pszActionSetName /*C*/);
			}
			public virtual void /*void*/ ISteamController_ActivateActionSet( ulong controllerHandle, ulong actionSetHandle )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamController _ptr is null!" );
				
				Native.ISteamController.ActivateActionSet(_ptr, controllerHandle /*C*/, actionSetHandle /*C*/);
			}
			public virtual ControllerActionSetHandle_t /*(ControllerActionSetHandle_t)*/ ISteamController_GetCurrentActionSet( ulong controllerHandle )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamController _ptr is null!" );
				
				return Native.ISteamController.GetCurrentActionSet(_ptr, controllerHandle /*C*/);
			}
			public virtual ControllerDigitalActionHandle_t /*(ControllerDigitalActionHandle_t)*/ ISteamController_GetDigitalActionHandle( string /*const char **/ pszActionName )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamController _ptr is null!" );
				
				return Native.ISteamController.GetDigitalActionHandle(_ptr, pszActionName /*C*/);
			}
			public virtual ControllerDigitalActionData_t /*struct ControllerDigitalActionData_t*/ ISteamController_GetDigitalActionData( ulong controllerHandle, ulong digitalActionHandle )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamController _ptr is null!" );
				
				return Native.ISteamController.GetDigitalActionData(_ptr, controllerHandle /*C*/, digitalActionHandle /*C*/);
			}
			public virtual int /*int*/ ISteamController_GetDigitalActionOrigins( ulong controllerHandle, ulong actionSetHandle, ulong digitalActionHandle, out ControllerActionOrigin /*EControllerActionOrigin **/ originsOut )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamController _ptr is null!" );
				
				return Native.ISteamController.GetDigitalActionOrigins(_ptr, controllerHandle /*C*/, actionSetHandle /*C*/, digitalActionHandle /*C*/, out originsOut /*B*/);
			}
			public virtual ControllerAnalogActionHandle_t /*(ControllerAnalogActionHandle_t)*/ ISteamController_GetAnalogActionHandle( string /*const char **/ pszActionName )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamController _ptr is null!" );
				
				return Native.ISteamController.GetAnalogActionHandle(_ptr, pszActionName /*C*/);
			}
			public virtual ControllerAnalogActionData_t /*struct ControllerAnalogActionData_t*/ ISteamController_GetAnalogActionData( ulong controllerHandle, ulong analogActionHandle )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamController _ptr is null!" );
				
				return Native.ISteamController.GetAnalogActionData(_ptr, controllerHandle /*C*/, analogActionHandle /*C*/);
			}
			public virtual int /*int*/ ISteamController_GetAnalogActionOrigins( ulong controllerHandle, ulong actionSetHandle, ulong analogActionHandle, out ControllerActionOrigin /*EControllerActionOrigin **/ originsOut )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamController _ptr is null!" );
				
				return Native.ISteamController.GetAnalogActionOrigins(_ptr, controllerHandle /*C*/, actionSetHandle /*C*/, analogActionHandle /*C*/, out originsOut /*B*/);
			}
			public virtual void /*void*/ ISteamController_StopAnalogActionMomentum( ulong controllerHandle, ulong eAction )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamController _ptr is null!" );
				
				Native.ISteamController.StopAnalogActionMomentum(_ptr, controllerHandle /*C*/, eAction /*C*/);
			}
			public virtual void /*void*/ ISteamController_TriggerHapticPulse( ulong controllerHandle, SteamControllerPad /*ESteamControllerPad*/ eTargetPad, ushort /*unsigned short*/ usDurationMicroSec )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamController _ptr is null!" );
				
				Native.ISteamController.TriggerHapticPulse(_ptr, controllerHandle /*C*/, eTargetPad /*C*/, usDurationMicroSec /*C*/);
			}
			public virtual void /*void*/ ISteamController_TriggerRepeatedHapticPulse( ulong controllerHandle, SteamControllerPad /*ESteamControllerPad*/ eTargetPad, ushort /*unsigned short*/ usDurationMicroSec, ushort /*unsigned short*/ usOffMicroSec, ushort /*unsigned short*/ unRepeat, uint /*unsigned int*/ nFlags )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamController _ptr is null!" );
				
				Native.ISteamController.TriggerRepeatedHapticPulse(_ptr, controllerHandle /*C*/, eTargetPad /*C*/, usDurationMicroSec /*C*/, usOffMicroSec /*C*/, unRepeat /*C*/, nFlags /*C*/);
			}
			
			public virtual UGCQueryHandle_t /*(UGCQueryHandle_t)*/ ISteamUGC_CreateQueryUserUGCRequest( uint unAccountID, UserUGCList /*EUserUGCList*/ eListType, UGCMatchingUGCType /*EUGCMatchingUGCType*/ eMatchingUGCType, UserUGCListSortOrder /*EUserUGCListSortOrder*/ eSortOrder, uint nCreatorAppID, uint nConsumerAppID, uint /*uint32*/ unPage )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamUGC _ptr is null!" );
				
				return Native.ISteamUGC.CreateQueryUserUGCRequest(_ptr, unAccountID /*C*/, eListType /*C*/, eMatchingUGCType /*C*/, eSortOrder /*C*/, nCreatorAppID /*C*/, nConsumerAppID /*C*/, unPage /*C*/);
			}
			public virtual UGCQueryHandle_t /*(UGCQueryHandle_t)*/ ISteamUGC_CreateQueryAllUGCRequest( UGCQuery /*EUGCQuery*/ eQueryType, UGCMatchingUGCType /*EUGCMatchingUGCType*/ eMatchingeMatchingUGCTypeFileType, uint nCreatorAppID, uint nConsumerAppID, uint /*uint32*/ unPage )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamUGC _ptr is null!" );
				
				return Native.ISteamUGC.CreateQueryAllUGCRequest(_ptr, eQueryType /*C*/, eMatchingeMatchingUGCTypeFileType /*C*/, nCreatorAppID /*C*/, nConsumerAppID /*C*/, unPage /*C*/);
			}
			public virtual UGCQueryHandle_t /*(UGCQueryHandle_t)*/ ISteamUGC_CreateQueryUGCDetailsRequest( IntPtr /*PublishedFileId_t **/ pvecPublishedFileID, uint /*uint32*/ unNumPublishedFileIDs )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamUGC _ptr is null!" );
				
				return Native.ISteamUGC.CreateQueryUGCDetailsRequest(_ptr, pvecPublishedFileID /*C*/, unNumPublishedFileIDs /*C*/);
			}
			public virtual SteamAPICall_t /*(SteamAPICall_t)*/ ISteamUGC_SendQueryUGCRequest( ulong handle )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamUGC _ptr is null!" );
				
				return Native.ISteamUGC.SendQueryUGCRequest(_ptr, handle /*C*/);
			}
			public virtual bool /*bool*/ ISteamUGC_GetQueryUGCResult( ulong handle, uint /*uint32*/ index, ref SteamUGCDetails_t /*struct SteamUGCDetails_t **/ pDetails )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamUGC _ptr is null!" );
				
				var pDetails_ps = new SteamUGCDetails_t.PackSmall();
				var ret = Native.ISteamUGC.GetQueryUGCResult(_ptr, handle /*C*/, index /*C*/, ref pDetails_ps /*C*/);
				pDetails = pDetails_ps;
				return ret;
			}
			public virtual bool /*bool*/ ISteamUGC_GetQueryUGCPreviewURL( ulong handle, uint /*uint32*/ index, System.Text.StringBuilder /*char **/ pchURL, uint /*uint32*/ cchURLSize )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamUGC _ptr is null!" );
				
				return Native.ISteamUGC.GetQueryUGCPreviewURL(_ptr, handle /*C*/, index /*C*/, pchURL /*C*/, cchURLSize /*C*/);
			}
			public virtual bool /*bool*/ ISteamUGC_GetQueryUGCMetadata( ulong handle, uint /*uint32*/ index, System.Text.StringBuilder /*char **/ pchMetadata, uint /*uint32*/ cchMetadatasize )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamUGC _ptr is null!" );
				
				return Native.ISteamUGC.GetQueryUGCMetadata(_ptr, handle /*C*/, index /*C*/, pchMetadata /*C*/, cchMetadatasize /*C*/);
			}
			public virtual bool /*bool*/ ISteamUGC_GetQueryUGCChildren( ulong handle, uint /*uint32*/ index, IntPtr /*PublishedFileId_t **/ pvecPublishedFileID, uint /*uint32*/ cMaxEntries )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamUGC _ptr is null!" );
				
				return Native.ISteamUGC.GetQueryUGCChildren(_ptr, handle /*C*/, index /*C*/, pvecPublishedFileID /*C*/, cMaxEntries /*C*/);
			}
			public virtual bool /*bool*/ ISteamUGC_GetQueryUGCStatistic( ulong handle, uint /*uint32*/ index, ItemStatistic /*EItemStatistic*/ eStatType, out uint /*uint32 **/ pStatValue )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamUGC _ptr is null!" );
				
				return Native.ISteamUGC.GetQueryUGCStatistic(_ptr, handle /*C*/, index /*C*/, eStatType /*C*/, out pStatValue /*B*/);
			}
			public virtual uint /*uint32*/ ISteamUGC_GetQueryUGCNumAdditionalPreviews( ulong handle, uint /*uint32*/ index )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamUGC _ptr is null!" );
				
				return Native.ISteamUGC.GetQueryUGCNumAdditionalPreviews(_ptr, handle /*C*/, index /*C*/);
			}
			public virtual bool /*bool*/ ISteamUGC_GetQueryUGCAdditionalPreview( ulong handle, uint /*uint32*/ index, uint /*uint32*/ previewIndex, System.Text.StringBuilder /*char **/ pchURLOrVideoID, uint /*uint32*/ cchURLSize, System.Text.StringBuilder /*char **/ pchOriginalFileName, uint /*uint32*/ cchOriginalFileNameSize, out ItemPreviewType /*EItemPreviewType **/ pPreviewType )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamUGC _ptr is null!" );
				
				return Native.ISteamUGC.GetQueryUGCAdditionalPreview(_ptr, handle /*C*/, index /*C*/, previewIndex /*C*/, pchURLOrVideoID /*C*/, cchURLSize /*C*/, pchOriginalFileName /*C*/, cchOriginalFileNameSize /*C*/, out pPreviewType /*B*/);
			}
			public virtual uint /*uint32*/ ISteamUGC_GetQueryUGCNumKeyValueTags( ulong handle, uint /*uint32*/ index )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamUGC _ptr is null!" );
				
				return Native.ISteamUGC.GetQueryUGCNumKeyValueTags(_ptr, handle /*C*/, index /*C*/);
			}
			public virtual bool /*bool*/ ISteamUGC_GetQueryUGCKeyValueTag( ulong handle, uint /*uint32*/ index, uint /*uint32*/ keyValueTagIndex, System.Text.StringBuilder /*char **/ pchKey, uint /*uint32*/ cchKeySize, System.Text.StringBuilder /*char **/ pchValue, uint /*uint32*/ cchValueSize )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamUGC _ptr is null!" );
				
				return Native.ISteamUGC.GetQueryUGCKeyValueTag(_ptr, handle /*C*/, index /*C*/, keyValueTagIndex /*C*/, pchKey /*C*/, cchKeySize /*C*/, pchValue /*C*/, cchValueSize /*C*/);
			}
			public virtual bool /*bool*/ ISteamUGC_ReleaseQueryUGCRequest( ulong handle )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamUGC _ptr is null!" );
				
				return Native.ISteamUGC.ReleaseQueryUGCRequest(_ptr, handle /*C*/);
			}
			public virtual bool /*bool*/ ISteamUGC_AddRequiredTag( ulong handle, string /*const char **/ pTagName )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamUGC _ptr is null!" );
				
				return Native.ISteamUGC.AddRequiredTag(_ptr, handle /*C*/, pTagName /*C*/);
			}
			public virtual bool /*bool*/ ISteamUGC_AddExcludedTag( ulong handle, string /*const char **/ pTagName )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamUGC _ptr is null!" );
				
				return Native.ISteamUGC.AddExcludedTag(_ptr, handle /*C*/, pTagName /*C*/);
			}
			public virtual bool /*bool*/ ISteamUGC_SetReturnKeyValueTags( ulong handle, [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bReturnKeyValueTags )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamUGC _ptr is null!" );
				
				return Native.ISteamUGC.SetReturnKeyValueTags(_ptr, handle /*C*/, bReturnKeyValueTags /*C*/);
			}
			public virtual bool /*bool*/ ISteamUGC_SetReturnLongDescription( ulong handle, [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bReturnLongDescription )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamUGC _ptr is null!" );
				
				return Native.ISteamUGC.SetReturnLongDescription(_ptr, handle /*C*/, bReturnLongDescription /*C*/);
			}
			public virtual bool /*bool*/ ISteamUGC_SetReturnMetadata( ulong handle, [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bReturnMetadata )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamUGC _ptr is null!" );
				
				return Native.ISteamUGC.SetReturnMetadata(_ptr, handle /*C*/, bReturnMetadata /*C*/);
			}
			public virtual bool /*bool*/ ISteamUGC_SetReturnChildren( ulong handle, [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bReturnChildren )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamUGC _ptr is null!" );
				
				return Native.ISteamUGC.SetReturnChildren(_ptr, handle /*C*/, bReturnChildren /*C*/);
			}
			public virtual bool /*bool*/ ISteamUGC_SetReturnAdditionalPreviews( ulong handle, [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bReturnAdditionalPreviews )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamUGC _ptr is null!" );
				
				return Native.ISteamUGC.SetReturnAdditionalPreviews(_ptr, handle /*C*/, bReturnAdditionalPreviews /*C*/);
			}
			public virtual bool /*bool*/ ISteamUGC_SetReturnTotalOnly( ulong handle, [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bReturnTotalOnly )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamUGC _ptr is null!" );
				
				return Native.ISteamUGC.SetReturnTotalOnly(_ptr, handle /*C*/, bReturnTotalOnly /*C*/);
			}
			public virtual bool /*bool*/ ISteamUGC_SetLanguage( ulong handle, string /*const char **/ pchLanguage )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamUGC _ptr is null!" );
				
				return Native.ISteamUGC.SetLanguage(_ptr, handle /*C*/, pchLanguage /*C*/);
			}
			public virtual bool /*bool*/ ISteamUGC_SetAllowCachedResponse( ulong handle, uint /*uint32*/ unMaxAgeSeconds )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamUGC _ptr is null!" );
				
				return Native.ISteamUGC.SetAllowCachedResponse(_ptr, handle /*C*/, unMaxAgeSeconds /*C*/);
			}
			public virtual bool /*bool*/ ISteamUGC_SetCloudFileNameFilter( ulong handle, string /*const char **/ pMatchCloudFileName )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamUGC _ptr is null!" );
				
				return Native.ISteamUGC.SetCloudFileNameFilter(_ptr, handle /*C*/, pMatchCloudFileName /*C*/);
			}
			public virtual bool /*bool*/ ISteamUGC_SetMatchAnyTag( ulong handle, [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bMatchAnyTag )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamUGC _ptr is null!" );
				
				return Native.ISteamUGC.SetMatchAnyTag(_ptr, handle /*C*/, bMatchAnyTag /*C*/);
			}
			public virtual bool /*bool*/ ISteamUGC_SetSearchText( ulong handle, string /*const char **/ pSearchText )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamUGC _ptr is null!" );
				
				return Native.ISteamUGC.SetSearchText(_ptr, handle /*C*/, pSearchText /*C*/);
			}
			public virtual bool /*bool*/ ISteamUGC_SetRankedByTrendDays( ulong handle, uint /*uint32*/ unDays )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamUGC _ptr is null!" );
				
				return Native.ISteamUGC.SetRankedByTrendDays(_ptr, handle /*C*/, unDays /*C*/);
			}
			public virtual bool /*bool*/ ISteamUGC_AddRequiredKeyValueTag( ulong handle, string /*const char **/ pKey, string /*const char **/ pValue )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamUGC _ptr is null!" );
				
				return Native.ISteamUGC.AddRequiredKeyValueTag(_ptr, handle /*C*/, pKey /*C*/, pValue /*C*/);
			}
			public virtual SteamAPICall_t /*(SteamAPICall_t)*/ ISteamUGC_RequestUGCDetails( ulong nPublishedFileID, uint /*uint32*/ unMaxAgeSeconds )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamUGC _ptr is null!" );
				
				return Native.ISteamUGC.RequestUGCDetails(_ptr, nPublishedFileID /*C*/, unMaxAgeSeconds /*C*/);
			}
			public virtual SteamAPICall_t /*(SteamAPICall_t)*/ ISteamUGC_CreateItem( uint nConsumerAppId, WorkshopFileType /*EWorkshopFileType*/ eFileType )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamUGC _ptr is null!" );
				
				return Native.ISteamUGC.CreateItem(_ptr, nConsumerAppId /*C*/, eFileType /*C*/);
			}
			public virtual UGCUpdateHandle_t /*(UGCUpdateHandle_t)*/ ISteamUGC_StartItemUpdate( uint nConsumerAppId, ulong nPublishedFileID )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamUGC _ptr is null!" );
				
				return Native.ISteamUGC.StartItemUpdate(_ptr, nConsumerAppId /*C*/, nPublishedFileID /*C*/);
			}
			public virtual bool /*bool*/ ISteamUGC_SetItemTitle( ulong handle, string /*const char **/ pchTitle )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamUGC _ptr is null!" );
				
				return Native.ISteamUGC.SetItemTitle(_ptr, handle /*C*/, pchTitle /*C*/);
			}
			public virtual bool /*bool*/ ISteamUGC_SetItemDescription( ulong handle, string /*const char **/ pchDescription )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamUGC _ptr is null!" );
				
				return Native.ISteamUGC.SetItemDescription(_ptr, handle /*C*/, pchDescription /*C*/);
			}
			public virtual bool /*bool*/ ISteamUGC_SetItemUpdateLanguage( ulong handle, string /*const char **/ pchLanguage )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamUGC _ptr is null!" );
				
				return Native.ISteamUGC.SetItemUpdateLanguage(_ptr, handle /*C*/, pchLanguage /*C*/);
			}
			public virtual bool /*bool*/ ISteamUGC_SetItemMetadata( ulong handle, string /*const char **/ pchMetaData )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamUGC _ptr is null!" );
				
				return Native.ISteamUGC.SetItemMetadata(_ptr, handle /*C*/, pchMetaData /*C*/);
			}
			public virtual bool /*bool*/ ISteamUGC_SetItemVisibility( ulong handle, RemoteStoragePublishedFileVisibility /*ERemoteStoragePublishedFileVisibility*/ eVisibility )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamUGC _ptr is null!" );
				
				return Native.ISteamUGC.SetItemVisibility(_ptr, handle /*C*/, eVisibility /*C*/);
			}
			public virtual bool /*bool*/ ISteamUGC_SetItemTags( ulong updateHandle, IntPtr /*const struct SteamParamStringArray_t **/ pTags )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamUGC _ptr is null!" );
				
				return Native.ISteamUGC.SetItemTags(_ptr, updateHandle /*C*/, pTags);
			}
			public virtual bool /*bool*/ ISteamUGC_SetItemContent( ulong handle, string /*const char **/ pszContentFolder )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamUGC _ptr is null!" );
				
				return Native.ISteamUGC.SetItemContent(_ptr, handle /*C*/, pszContentFolder /*C*/);
			}
			public virtual bool /*bool*/ ISteamUGC_SetItemPreview( ulong handle, string /*const char **/ pszPreviewFile )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamUGC _ptr is null!" );
				
				return Native.ISteamUGC.SetItemPreview(_ptr, handle /*C*/, pszPreviewFile /*C*/);
			}
			public virtual bool /*bool*/ ISteamUGC_RemoveItemKeyValueTags( ulong handle, string /*const char **/ pchKey )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamUGC _ptr is null!" );
				
				return Native.ISteamUGC.RemoveItemKeyValueTags(_ptr, handle /*C*/, pchKey /*C*/);
			}
			public virtual bool /*bool*/ ISteamUGC_AddItemKeyValueTag( ulong handle, string /*const char **/ pchKey, string /*const char **/ pchValue )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamUGC _ptr is null!" );
				
				return Native.ISteamUGC.AddItemKeyValueTag(_ptr, handle /*C*/, pchKey /*C*/, pchValue /*C*/);
			}
			public virtual bool /*bool*/ ISteamUGC_AddItemPreviewFile( ulong handle, string /*const char **/ pszPreviewFile, ItemPreviewType /*EItemPreviewType*/ type )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamUGC _ptr is null!" );
				
				return Native.ISteamUGC.AddItemPreviewFile(_ptr, handle /*C*/, pszPreviewFile /*C*/, type /*C*/);
			}
			public virtual bool /*bool*/ ISteamUGC_AddItemPreviewVideo( ulong handle, string /*const char **/ pszVideoID )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamUGC _ptr is null!" );
				
				return Native.ISteamUGC.AddItemPreviewVideo(_ptr, handle /*C*/, pszVideoID /*C*/);
			}
			public virtual bool /*bool*/ ISteamUGC_UpdateItemPreviewFile( ulong handle, uint /*uint32*/ index, string /*const char **/ pszPreviewFile )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamUGC _ptr is null!" );
				
				return Native.ISteamUGC.UpdateItemPreviewFile(_ptr, handle /*C*/, index /*C*/, pszPreviewFile /*C*/);
			}
			public virtual bool /*bool*/ ISteamUGC_UpdateItemPreviewVideo( ulong handle, uint /*uint32*/ index, string /*const char **/ pszVideoID )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamUGC _ptr is null!" );
				
				return Native.ISteamUGC.UpdateItemPreviewVideo(_ptr, handle /*C*/, index /*C*/, pszVideoID /*C*/);
			}
			public virtual bool /*bool*/ ISteamUGC_RemoveItemPreview( ulong handle, uint /*uint32*/ index )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamUGC _ptr is null!" );
				
				return Native.ISteamUGC.RemoveItemPreview(_ptr, handle /*C*/, index /*C*/);
			}
			public virtual SteamAPICall_t /*(SteamAPICall_t)*/ ISteamUGC_SubmitItemUpdate( ulong handle, string /*const char **/ pchChangeNote )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamUGC _ptr is null!" );
				
				return Native.ISteamUGC.SubmitItemUpdate(_ptr, handle /*C*/, pchChangeNote /*C*/);
			}
			public virtual ItemUpdateStatus /*EItemUpdateStatus*/ ISteamUGC_GetItemUpdateProgress( ulong handle, out ulong /*uint64 **/ punBytesProcessed, out ulong /*uint64 **/ punBytesTotal )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamUGC _ptr is null!" );
				
				return Native.ISteamUGC.GetItemUpdateProgress(_ptr, handle /*C*/, out punBytesProcessed /*B*/, out punBytesTotal /*B*/);
			}
			public virtual SteamAPICall_t /*(SteamAPICall_t)*/ ISteamUGC_SetUserItemVote( ulong nPublishedFileID, [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bVoteUp )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamUGC _ptr is null!" );
				
				return Native.ISteamUGC.SetUserItemVote(_ptr, nPublishedFileID /*C*/, bVoteUp /*C*/);
			}
			public virtual SteamAPICall_t /*(SteamAPICall_t)*/ ISteamUGC_GetUserItemVote( ulong nPublishedFileID )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamUGC _ptr is null!" );
				
				return Native.ISteamUGC.GetUserItemVote(_ptr, nPublishedFileID /*C*/);
			}
			public virtual SteamAPICall_t /*(SteamAPICall_t)*/ ISteamUGC_AddItemToFavorites( uint nAppId, ulong nPublishedFileID )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamUGC _ptr is null!" );
				
				return Native.ISteamUGC.AddItemToFavorites(_ptr, nAppId /*C*/, nPublishedFileID /*C*/);
			}
			public virtual SteamAPICall_t /*(SteamAPICall_t)*/ ISteamUGC_RemoveItemFromFavorites( uint nAppId, ulong nPublishedFileID )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamUGC _ptr is null!" );
				
				return Native.ISteamUGC.RemoveItemFromFavorites(_ptr, nAppId /*C*/, nPublishedFileID /*C*/);
			}
			public virtual SteamAPICall_t /*(SteamAPICall_t)*/ ISteamUGC_SubscribeItem( ulong nPublishedFileID )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamUGC _ptr is null!" );
				
				return Native.ISteamUGC.SubscribeItem(_ptr, nPublishedFileID /*C*/);
			}
			public virtual SteamAPICall_t /*(SteamAPICall_t)*/ ISteamUGC_UnsubscribeItem( ulong nPublishedFileID )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamUGC _ptr is null!" );
				
				return Native.ISteamUGC.UnsubscribeItem(_ptr, nPublishedFileID /*C*/);
			}
			public virtual uint /*uint32*/ ISteamUGC_GetNumSubscribedItems()
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamUGC _ptr is null!" );
				
				return Native.ISteamUGC.GetNumSubscribedItems(_ptr);
			}
			public virtual uint /*uint32*/ ISteamUGC_GetSubscribedItems( IntPtr /*PublishedFileId_t **/ pvecPublishedFileID, uint /*uint32*/ cMaxEntries )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamUGC _ptr is null!" );
				
				return Native.ISteamUGC.GetSubscribedItems(_ptr, pvecPublishedFileID /*C*/, cMaxEntries /*C*/);
			}
			public virtual uint /*uint32*/ ISteamUGC_GetItemState( ulong nPublishedFileID )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamUGC _ptr is null!" );
				
				return Native.ISteamUGC.GetItemState(_ptr, nPublishedFileID /*C*/);
			}
			public virtual bool /*bool*/ ISteamUGC_GetItemInstallInfo( ulong nPublishedFileID, out ulong /*uint64 **/ punSizeOnDisk, System.Text.StringBuilder /*char **/ pchFolder, uint /*uint32*/ cchFolderSize, out uint /*uint32 **/ punTimeStamp )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamUGC _ptr is null!" );
				
				return Native.ISteamUGC.GetItemInstallInfo(_ptr, nPublishedFileID /*C*/, out punSizeOnDisk /*B*/, pchFolder /*C*/, cchFolderSize /*C*/, out punTimeStamp /*B*/);
			}
			public virtual bool /*bool*/ ISteamUGC_GetItemDownloadInfo( ulong nPublishedFileID, out ulong /*uint64 **/ punBytesDownloaded, out ulong /*uint64 **/ punBytesTotal )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamUGC _ptr is null!" );
				
				return Native.ISteamUGC.GetItemDownloadInfo(_ptr, nPublishedFileID /*C*/, out punBytesDownloaded /*B*/, out punBytesTotal /*B*/);
			}
			public virtual bool /*bool*/ ISteamUGC_DownloadItem( ulong nPublishedFileID, [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bHighPriority )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamUGC _ptr is null!" );
				
				return Native.ISteamUGC.DownloadItem(_ptr, nPublishedFileID /*C*/, bHighPriority /*C*/);
			}
			public virtual bool /*bool*/ ISteamUGC_BInitWorkshopForGameServer( uint unWorkshopDepotID, string /*const char **/ pszFolder )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamUGC _ptr is null!" );
				
				return Native.ISteamUGC.BInitWorkshopForGameServer(_ptr, unWorkshopDepotID /*C*/, pszFolder /*C*/);
			}
			public virtual void /*void*/ ISteamUGC_SuspendDownloads( [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bSuspend )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamUGC _ptr is null!" );
				
				Native.ISteamUGC.SuspendDownloads(_ptr, bSuspend /*C*/);
			}
			
			public virtual uint /*uint32*/ ISteamAppList_GetNumInstalledApps()
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamAppList _ptr is null!" );
				
				return Native.ISteamAppList.GetNumInstalledApps(_ptr);
			}
			public virtual uint /*uint32*/ ISteamAppList_GetInstalledApps( IntPtr /*AppId_t **/ pvecAppID, uint /*uint32*/ unMaxAppIDs )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamAppList _ptr is null!" );
				
				return Native.ISteamAppList.GetInstalledApps(_ptr, pvecAppID /*C*/, unMaxAppIDs /*C*/);
			}
			public virtual int /*int*/ ISteamAppList_GetAppName( uint nAppID, System.Text.StringBuilder /*char **/ pchName, int /*int*/ cchNameMax )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamAppList _ptr is null!" );
				
				return Native.ISteamAppList.GetAppName(_ptr, nAppID /*C*/, pchName /*C*/, cchNameMax /*C*/);
			}
			public virtual int /*int*/ ISteamAppList_GetAppInstallDir( uint nAppID, System.Text.StringBuilder /*char **/ pchDirectory, int /*int*/ cchNameMax )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamAppList _ptr is null!" );
				
				return Native.ISteamAppList.GetAppInstallDir(_ptr, nAppID /*C*/, pchDirectory /*C*/, cchNameMax /*C*/);
			}
			public virtual int /*int*/ ISteamAppList_GetAppBuildId( uint nAppID )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamAppList _ptr is null!" );
				
				return Native.ISteamAppList.GetAppBuildId(_ptr, nAppID /*C*/);
			}
			
			public virtual void /*void*/ ISteamHTMLSurface_DestructISteamHTMLSurface()
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamHTMLSurface _ptr is null!" );
				
				Native.ISteamHTMLSurface.DestructISteamHTMLSurface(_ptr);
			}
			public virtual bool /*bool*/ ISteamHTMLSurface_Init()
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamHTMLSurface _ptr is null!" );
				
				return Native.ISteamHTMLSurface.Init(_ptr);
			}
			public virtual bool /*bool*/ ISteamHTMLSurface_Shutdown()
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamHTMLSurface _ptr is null!" );
				
				return Native.ISteamHTMLSurface.Shutdown(_ptr);
			}
			public virtual SteamAPICall_t /*(SteamAPICall_t)*/ ISteamHTMLSurface_CreateBrowser( string /*const char **/ pchUserAgent, string /*const char **/ pchUserCSS )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamHTMLSurface _ptr is null!" );
				
				return Native.ISteamHTMLSurface.CreateBrowser(_ptr, pchUserAgent /*C*/, pchUserCSS /*C*/);
			}
			public virtual void /*void*/ ISteamHTMLSurface_RemoveBrowser( uint unBrowserHandle )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamHTMLSurface _ptr is null!" );
				
				Native.ISteamHTMLSurface.RemoveBrowser(_ptr, unBrowserHandle /*C*/);
			}
			public virtual void /*void*/ ISteamHTMLSurface_LoadURL( uint unBrowserHandle, string /*const char **/ pchURL, string /*const char **/ pchPostData )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamHTMLSurface _ptr is null!" );
				
				Native.ISteamHTMLSurface.LoadURL(_ptr, unBrowserHandle /*C*/, pchURL /*C*/, pchPostData /*C*/);
			}
			public virtual void /*void*/ ISteamHTMLSurface_SetSize( uint unBrowserHandle, uint /*uint32*/ unWidth, uint /*uint32*/ unHeight )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamHTMLSurface _ptr is null!" );
				
				Native.ISteamHTMLSurface.SetSize(_ptr, unBrowserHandle /*C*/, unWidth /*C*/, unHeight /*C*/);
			}
			public virtual void /*void*/ ISteamHTMLSurface_StopLoad( uint unBrowserHandle )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamHTMLSurface _ptr is null!" );
				
				Native.ISteamHTMLSurface.StopLoad(_ptr, unBrowserHandle /*C*/);
			}
			public virtual void /*void*/ ISteamHTMLSurface_Reload( uint unBrowserHandle )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamHTMLSurface _ptr is null!" );
				
				Native.ISteamHTMLSurface.Reload(_ptr, unBrowserHandle /*C*/);
			}
			public virtual void /*void*/ ISteamHTMLSurface_GoBack( uint unBrowserHandle )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamHTMLSurface _ptr is null!" );
				
				Native.ISteamHTMLSurface.GoBack(_ptr, unBrowserHandle /*C*/);
			}
			public virtual void /*void*/ ISteamHTMLSurface_GoForward( uint unBrowserHandle )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamHTMLSurface _ptr is null!" );
				
				Native.ISteamHTMLSurface.GoForward(_ptr, unBrowserHandle /*C*/);
			}
			public virtual void /*void*/ ISteamHTMLSurface_AddHeader( uint unBrowserHandle, string /*const char **/ pchKey, string /*const char **/ pchValue )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamHTMLSurface _ptr is null!" );
				
				Native.ISteamHTMLSurface.AddHeader(_ptr, unBrowserHandle /*C*/, pchKey /*C*/, pchValue /*C*/);
			}
			public virtual void /*void*/ ISteamHTMLSurface_ExecuteJavascript( uint unBrowserHandle, string /*const char **/ pchScript )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamHTMLSurface _ptr is null!" );
				
				Native.ISteamHTMLSurface.ExecuteJavascript(_ptr, unBrowserHandle /*C*/, pchScript /*C*/);
			}
			public virtual void /*void*/ ISteamHTMLSurface_MouseUp( uint unBrowserHandle, HTMLMouseButton /*ISteamHTMLSurface::EHTMLMouseButton*/ eMouseButton )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamHTMLSurface _ptr is null!" );
				
				Native.ISteamHTMLSurface.MouseUp(_ptr, unBrowserHandle /*C*/, eMouseButton /*C*/);
			}
			public virtual void /*void*/ ISteamHTMLSurface_MouseDown( uint unBrowserHandle, HTMLMouseButton /*ISteamHTMLSurface::EHTMLMouseButton*/ eMouseButton )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamHTMLSurface _ptr is null!" );
				
				Native.ISteamHTMLSurface.MouseDown(_ptr, unBrowserHandle /*C*/, eMouseButton /*C*/);
			}
			public virtual void /*void*/ ISteamHTMLSurface_MouseDoubleClick( uint unBrowserHandle, HTMLMouseButton /*ISteamHTMLSurface::EHTMLMouseButton*/ eMouseButton )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamHTMLSurface _ptr is null!" );
				
				Native.ISteamHTMLSurface.MouseDoubleClick(_ptr, unBrowserHandle /*C*/, eMouseButton /*C*/);
			}
			public virtual void /*void*/ ISteamHTMLSurface_MouseMove( uint unBrowserHandle, int /*int*/ x, int /*int*/ y )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamHTMLSurface _ptr is null!" );
				
				Native.ISteamHTMLSurface.MouseMove(_ptr, unBrowserHandle /*C*/, x /*C*/, y /*C*/);
			}
			public virtual void /*void*/ ISteamHTMLSurface_MouseWheel( uint unBrowserHandle, int /*int32*/ nDelta )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamHTMLSurface _ptr is null!" );
				
				Native.ISteamHTMLSurface.MouseWheel(_ptr, unBrowserHandle /*C*/, nDelta /*C*/);
			}
			public virtual void /*void*/ ISteamHTMLSurface_KeyDown( uint unBrowserHandle, uint /*uint32*/ nNativeKeyCode, HTMLKeyModifiers /*ISteamHTMLSurface::EHTMLKeyModifiers*/ eHTMLKeyModifiers )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamHTMLSurface _ptr is null!" );
				
				Native.ISteamHTMLSurface.KeyDown(_ptr, unBrowserHandle /*C*/, nNativeKeyCode /*C*/, eHTMLKeyModifiers /*C*/);
			}
			public virtual void /*void*/ ISteamHTMLSurface_KeyUp( uint unBrowserHandle, uint /*uint32*/ nNativeKeyCode, HTMLKeyModifiers /*ISteamHTMLSurface::EHTMLKeyModifiers*/ eHTMLKeyModifiers )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamHTMLSurface _ptr is null!" );
				
				Native.ISteamHTMLSurface.KeyUp(_ptr, unBrowserHandle /*C*/, nNativeKeyCode /*C*/, eHTMLKeyModifiers /*C*/);
			}
			public virtual void /*void*/ ISteamHTMLSurface_KeyChar( uint unBrowserHandle, uint /*uint32*/ cUnicodeChar, HTMLKeyModifiers /*ISteamHTMLSurface::EHTMLKeyModifiers*/ eHTMLKeyModifiers )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamHTMLSurface _ptr is null!" );
				
				Native.ISteamHTMLSurface.KeyChar(_ptr, unBrowserHandle /*C*/, cUnicodeChar /*C*/, eHTMLKeyModifiers /*C*/);
			}
			public virtual void /*void*/ ISteamHTMLSurface_SetHorizontalScroll( uint unBrowserHandle, uint /*uint32*/ nAbsolutePixelScroll )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamHTMLSurface _ptr is null!" );
				
				Native.ISteamHTMLSurface.SetHorizontalScroll(_ptr, unBrowserHandle /*C*/, nAbsolutePixelScroll /*C*/);
			}
			public virtual void /*void*/ ISteamHTMLSurface_SetVerticalScroll( uint unBrowserHandle, uint /*uint32*/ nAbsolutePixelScroll )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamHTMLSurface _ptr is null!" );
				
				Native.ISteamHTMLSurface.SetVerticalScroll(_ptr, unBrowserHandle /*C*/, nAbsolutePixelScroll /*C*/);
			}
			public virtual void /*void*/ ISteamHTMLSurface_SetKeyFocus( uint unBrowserHandle, [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bHasKeyFocus )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamHTMLSurface _ptr is null!" );
				
				Native.ISteamHTMLSurface.SetKeyFocus(_ptr, unBrowserHandle /*C*/, bHasKeyFocus /*C*/);
			}
			public virtual void /*void*/ ISteamHTMLSurface_ViewSource( uint unBrowserHandle )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamHTMLSurface _ptr is null!" );
				
				Native.ISteamHTMLSurface.ViewSource(_ptr, unBrowserHandle /*C*/);
			}
			public virtual void /*void*/ ISteamHTMLSurface_CopyToClipboard( uint unBrowserHandle )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamHTMLSurface _ptr is null!" );
				
				Native.ISteamHTMLSurface.CopyToClipboard(_ptr, unBrowserHandle /*C*/);
			}
			public virtual void /*void*/ ISteamHTMLSurface_PasteFromClipboard( uint unBrowserHandle )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamHTMLSurface _ptr is null!" );
				
				Native.ISteamHTMLSurface.PasteFromClipboard(_ptr, unBrowserHandle /*C*/);
			}
			public virtual void /*void*/ ISteamHTMLSurface_Find( uint unBrowserHandle, string /*const char **/ pchSearchStr, [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bCurrentlyInFind, [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bReverse )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamHTMLSurface _ptr is null!" );
				
				Native.ISteamHTMLSurface.Find(_ptr, unBrowserHandle /*C*/, pchSearchStr /*C*/, bCurrentlyInFind /*C*/, bReverse /*C*/);
			}
			public virtual void /*void*/ ISteamHTMLSurface_StopFind( uint unBrowserHandle )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamHTMLSurface _ptr is null!" );
				
				Native.ISteamHTMLSurface.StopFind(_ptr, unBrowserHandle /*C*/);
			}
			public virtual void /*void*/ ISteamHTMLSurface_GetLinkAtPosition( uint unBrowserHandle, int /*int*/ x, int /*int*/ y )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamHTMLSurface _ptr is null!" );
				
				Native.ISteamHTMLSurface.GetLinkAtPosition(_ptr, unBrowserHandle /*C*/, x /*C*/, y /*C*/);
			}
			public virtual void /*void*/ ISteamHTMLSurface_SetCookie( string /*const char **/ pchHostname, string /*const char **/ pchKey, string /*const char **/ pchValue, string /*const char **/ pchPath, uint nExpires, [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bSecure, [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bHTTPOnly )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamHTMLSurface _ptr is null!" );
				
				Native.ISteamHTMLSurface.SetCookie(_ptr, pchHostname /*C*/, pchKey /*C*/, pchValue /*C*/, pchPath /*C*/, nExpires /*C*/, bSecure /*C*/, bHTTPOnly /*C*/);
			}
			public virtual void /*void*/ ISteamHTMLSurface_SetPageScaleFactor( uint unBrowserHandle, float /*float*/ flZoom, int /*int*/ nPointX, int /*int*/ nPointY )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamHTMLSurface _ptr is null!" );
				
				Native.ISteamHTMLSurface.SetPageScaleFactor(_ptr, unBrowserHandle /*C*/, flZoom /*C*/, nPointX /*C*/, nPointY /*C*/);
			}
			public virtual void /*void*/ ISteamHTMLSurface_SetBackgroundMode( uint unBrowserHandle, [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bBackgroundMode )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamHTMLSurface _ptr is null!" );
				
				Native.ISteamHTMLSurface.SetBackgroundMode(_ptr, unBrowserHandle /*C*/, bBackgroundMode /*C*/);
			}
			public virtual void /*void*/ ISteamHTMLSurface_AllowStartRequest( uint unBrowserHandle, [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bAllowed )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamHTMLSurface _ptr is null!" );
				
				Native.ISteamHTMLSurface.AllowStartRequest(_ptr, unBrowserHandle /*C*/, bAllowed /*C*/);
			}
			public virtual void /*void*/ ISteamHTMLSurface_JSDialogResponse( uint unBrowserHandle, [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bResult )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamHTMLSurface _ptr is null!" );
				
				Native.ISteamHTMLSurface.JSDialogResponse(_ptr, unBrowserHandle /*C*/, bResult /*C*/);
			}
			
			public virtual Result /*EResult*/ ISteamInventory_GetResultStatus( int resultHandle )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamInventory _ptr is null!" );
				
				return Native.ISteamInventory.GetResultStatus(_ptr, resultHandle /*C*/);
			}
			public virtual bool /*bool*/ ISteamInventory_GetResultItems( int resultHandle, IntPtr /*struct SteamItemDetails_t **/ pOutItemsArray, out uint /*uint32 **/ punOutItemsArraySize )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamInventory _ptr is null!" );
				
				return Native.ISteamInventory.GetResultItems(_ptr, resultHandle /*C*/, pOutItemsArray, out punOutItemsArraySize /*B*/);
			}
			public virtual uint /*uint32*/ ISteamInventory_GetResultTimestamp( int resultHandle )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamInventory _ptr is null!" );
				
				return Native.ISteamInventory.GetResultTimestamp(_ptr, resultHandle /*C*/);
			}
			public virtual bool /*bool*/ ISteamInventory_CheckResultSteamID( int resultHandle, ulong steamIDExpected )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamInventory _ptr is null!" );
				
				return Native.ISteamInventory.CheckResultSteamID(_ptr, resultHandle /*C*/, steamIDExpected /*C*/);
			}
			public virtual void /*void*/ ISteamInventory_DestroyResult( int resultHandle )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamInventory _ptr is null!" );
				
				Native.ISteamInventory.DestroyResult(_ptr, resultHandle /*C*/);
			}
			public virtual bool /*bool*/ ISteamInventory_GetAllItems( ref int pResultHandle )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamInventory _ptr is null!" );
				
				return Native.ISteamInventory.GetAllItems(_ptr, ref pResultHandle /*A*/);
			}
			public virtual bool /*bool*/ ISteamInventory_GetItemsByID( ref int pResultHandle, IntPtr /*const SteamItemInstanceID_t **/ pInstanceIDs, uint /*uint32*/ unCountInstanceIDs )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamInventory _ptr is null!" );
				
				return Native.ISteamInventory.GetItemsByID(_ptr, ref pResultHandle /*A*/, pInstanceIDs, unCountInstanceIDs /*C*/);
			}
			public virtual bool /*bool*/ ISteamInventory_SerializeResult( int resultHandle, IntPtr /*void **/ pOutBuffer, out uint /*uint32 **/ punOutBufferSize )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamInventory _ptr is null!" );
				
				return Native.ISteamInventory.SerializeResult(_ptr, resultHandle /*C*/, pOutBuffer /*C*/, out punOutBufferSize /*B*/);
			}
			public virtual bool /*bool*/ ISteamInventory_DeserializeResult( ref int pOutResultHandle, IntPtr /*const void **/ pBuffer, uint /*uint32*/ unBufferSize, [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bRESERVED_MUST_BE_FALSE )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamInventory _ptr is null!" );
				
				return Native.ISteamInventory.DeserializeResult(_ptr, ref pOutResultHandle /*A*/, pBuffer /*C*/, unBufferSize /*C*/, bRESERVED_MUST_BE_FALSE /*C*/);
			}
			public virtual bool /*bool*/ ISteamInventory_GenerateItems( ref int pResultHandle, IntPtr /*const SteamItemDef_t **/ pArrayItemDefs, out uint /*const uint32 **/ punArrayQuantity, uint /*uint32*/ unArrayLength )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamInventory _ptr is null!" );
				
				return Native.ISteamInventory.GenerateItems(_ptr, ref pResultHandle /*A*/, pArrayItemDefs, out punArrayQuantity /*B*/, unArrayLength /*C*/);
			}
			public virtual bool /*bool*/ ISteamInventory_GrantPromoItems( ref int pResultHandle )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamInventory _ptr is null!" );
				
				return Native.ISteamInventory.GrantPromoItems(_ptr, ref pResultHandle /*A*/);
			}
			public virtual bool /*bool*/ ISteamInventory_AddPromoItem( ref int pResultHandle, int itemDef )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamInventory _ptr is null!" );
				
				return Native.ISteamInventory.AddPromoItem(_ptr, ref pResultHandle /*A*/, itemDef /*C*/);
			}
			public virtual bool /*bool*/ ISteamInventory_AddPromoItems( ref int pResultHandle, IntPtr /*const SteamItemDef_t **/ pArrayItemDefs, uint /*uint32*/ unArrayLength )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamInventory _ptr is null!" );
				
				return Native.ISteamInventory.AddPromoItems(_ptr, ref pResultHandle /*A*/, pArrayItemDefs, unArrayLength /*C*/);
			}
			public virtual bool /*bool*/ ISteamInventory_ConsumeItem( ref int pResultHandle, ulong itemConsume, uint /*uint32*/ unQuantity )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamInventory _ptr is null!" );
				
				return Native.ISteamInventory.ConsumeItem(_ptr, ref pResultHandle /*A*/, itemConsume /*C*/, unQuantity /*C*/);
			}
			public virtual bool /*bool*/ ISteamInventory_ExchangeItems( ref int pResultHandle, ref int pArrayGenerate, out uint /*const uint32 **/ punArrayGenerateQuantity, uint /*uint32*/ unArrayGenerateLength, IntPtr /*const SteamItemInstanceID_t **/ pArrayDestroy, IntPtr /*const uint32 **/ punArrayDestroyQuantity, uint /*uint32*/ unArrayDestroyLength )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamInventory _ptr is null!" );
				
				return Native.ISteamInventory.ExchangeItems(_ptr, ref pResultHandle /*A*/, ref pArrayGenerate /*A*/, out punArrayGenerateQuantity /*B*/, unArrayGenerateLength /*C*/, pArrayDestroy, punArrayDestroyQuantity, unArrayDestroyLength /*C*/);
			}
			public virtual bool /*bool*/ ISteamInventory_TransferItemQuantity( ref int pResultHandle, ulong itemIdSource, uint /*uint32*/ unQuantity, ulong itemIdDest )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamInventory _ptr is null!" );
				
				return Native.ISteamInventory.TransferItemQuantity(_ptr, ref pResultHandle /*A*/, itemIdSource /*C*/, unQuantity /*C*/, itemIdDest /*C*/);
			}
			public virtual void /*void*/ ISteamInventory_SendItemDropHeartbeat()
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamInventory _ptr is null!" );
				
				Native.ISteamInventory.SendItemDropHeartbeat(_ptr);
			}
			public virtual bool /*bool*/ ISteamInventory_TriggerItemDrop( ref int pResultHandle, int dropListDefinition )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamInventory _ptr is null!" );
				
				return Native.ISteamInventory.TriggerItemDrop(_ptr, ref pResultHandle /*A*/, dropListDefinition /*C*/);
			}
			public virtual bool /*bool*/ ISteamInventory_TradeItems( ref int pResultHandle, ulong steamIDTradePartner, ref ulong pArrayGive, out uint /*const uint32 **/ pArrayGiveQuantity, uint /*uint32*/ nArrayGiveLength, ref ulong pArrayGet, out uint /*const uint32 **/ pArrayGetQuantity, uint /*uint32*/ nArrayGetLength )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamInventory _ptr is null!" );
				
				return Native.ISteamInventory.TradeItems(_ptr, ref pResultHandle /*A*/, steamIDTradePartner /*C*/, ref pArrayGive /*A*/, out pArrayGiveQuantity /*B*/, nArrayGiveLength /*C*/, ref pArrayGet /*A*/, out pArrayGetQuantity /*B*/, nArrayGetLength /*C*/);
			}
			public virtual bool /*bool*/ ISteamInventory_LoadItemDefinitions()
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamInventory _ptr is null!" );
				
				return Native.ISteamInventory.LoadItemDefinitions(_ptr);
			}
			public virtual bool /*bool*/ ISteamInventory_GetItemDefinitionIDs( IntPtr /*SteamItemDef_t **/ pItemDefIDs, out uint /*uint32 **/ punItemDefIDsArraySize )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamInventory _ptr is null!" );
				
				return Native.ISteamInventory.GetItemDefinitionIDs(_ptr, pItemDefIDs, out punItemDefIDsArraySize /*B*/);
			}
			public virtual bool /*bool*/ ISteamInventory_GetItemDefinitionProperty( int iDefinition, string /*const char **/ pchPropertyName, System.Text.StringBuilder /*char **/ pchValueBuffer, out uint /*uint32 **/ punValueBufferSize )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamInventory _ptr is null!" );
				
				return Native.ISteamInventory.GetItemDefinitionProperty(_ptr, iDefinition /*C*/, pchPropertyName /*C*/, pchValueBuffer /*C*/, out punValueBufferSize /*B*/);
			}
			
			public virtual void /*void*/ ISteamVideo_GetVideoURL( uint unVideoAppID )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamVideo _ptr is null!" );
				
				Native.ISteamVideo.GetVideoURL(_ptr, unVideoAppID /*C*/);
			}
			public virtual bool /*bool*/ ISteamVideo_IsBroadcasting( IntPtr /*int **/ pnNumViewers )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamVideo _ptr is null!" );
				
				return Native.ISteamVideo.IsBroadcasting(_ptr, pnNumViewers);
			}
			
			public virtual bool /*bool*/ ISteamGameServer_InitGameServer( uint /*uint32*/ unIP, ushort /*uint16*/ usGamePort, ushort /*uint16*/ usQueryPort, uint /*uint32*/ unFlags, uint nGameAppId, string /*const char **/ pchVersionString )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamGameServer _ptr is null!" );
				
				return Native.ISteamGameServer.InitGameServer(_ptr, unIP /*C*/, usGamePort /*C*/, usQueryPort /*C*/, unFlags /*C*/, nGameAppId /*C*/, pchVersionString /*C*/);
			}
			public virtual void /*void*/ ISteamGameServer_SetProduct( string /*const char **/ pszProduct )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamGameServer _ptr is null!" );
				
				Native.ISteamGameServer.SetProduct(_ptr, pszProduct /*C*/);
			}
			public virtual void /*void*/ ISteamGameServer_SetGameDescription( string /*const char **/ pszGameDescription )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamGameServer _ptr is null!" );
				
				Native.ISteamGameServer.SetGameDescription(_ptr, pszGameDescription /*C*/);
			}
			public virtual void /*void*/ ISteamGameServer_SetModDir( string /*const char **/ pszModDir )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamGameServer _ptr is null!" );
				
				Native.ISteamGameServer.SetModDir(_ptr, pszModDir /*C*/);
			}
			public virtual void /*void*/ ISteamGameServer_SetDedicatedServer( [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bDedicated )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamGameServer _ptr is null!" );
				
				Native.ISteamGameServer.SetDedicatedServer(_ptr, bDedicated /*C*/);
			}
			public virtual void /*void*/ ISteamGameServer_LogOn( string /*const char **/ pszToken )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamGameServer _ptr is null!" );
				
				Native.ISteamGameServer.LogOn(_ptr, pszToken /*C*/);
			}
			public virtual void /*void*/ ISteamGameServer_LogOnAnonymous()
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamGameServer _ptr is null!" );
				
				Native.ISteamGameServer.LogOnAnonymous(_ptr);
			}
			public virtual void /*void*/ ISteamGameServer_LogOff()
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamGameServer _ptr is null!" );
				
				Native.ISteamGameServer.LogOff(_ptr);
			}
			public virtual bool /*bool*/ ISteamGameServer_BLoggedOn()
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamGameServer _ptr is null!" );
				
				return Native.ISteamGameServer.BLoggedOn(_ptr);
			}
			public virtual bool /*bool*/ ISteamGameServer_BSecure()
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamGameServer _ptr is null!" );
				
				return Native.ISteamGameServer.BSecure(_ptr);
			}
			public virtual CSteamID /*(class CSteamID)*/ ISteamGameServer_GetSteamID()
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamGameServer _ptr is null!" );
				
				return Native.ISteamGameServer.GetSteamID(_ptr);
			}
			public virtual bool /*bool*/ ISteamGameServer_WasRestartRequested()
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamGameServer _ptr is null!" );
				
				return Native.ISteamGameServer.WasRestartRequested(_ptr);
			}
			public virtual void /*void*/ ISteamGameServer_SetMaxPlayerCount( int /*int*/ cPlayersMax )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamGameServer _ptr is null!" );
				
				Native.ISteamGameServer.SetMaxPlayerCount(_ptr, cPlayersMax /*C*/);
			}
			public virtual void /*void*/ ISteamGameServer_SetBotPlayerCount( int /*int*/ cBotplayers )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamGameServer _ptr is null!" );
				
				Native.ISteamGameServer.SetBotPlayerCount(_ptr, cBotplayers /*C*/);
			}
			public virtual void /*void*/ ISteamGameServer_SetServerName( string /*const char **/ pszServerName )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamGameServer _ptr is null!" );
				
				Native.ISteamGameServer.SetServerName(_ptr, pszServerName /*C*/);
			}
			public virtual void /*void*/ ISteamGameServer_SetMapName( string /*const char **/ pszMapName )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamGameServer _ptr is null!" );
				
				Native.ISteamGameServer.SetMapName(_ptr, pszMapName /*C*/);
			}
			public virtual void /*void*/ ISteamGameServer_SetPasswordProtected( [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bPasswordProtected )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamGameServer _ptr is null!" );
				
				Native.ISteamGameServer.SetPasswordProtected(_ptr, bPasswordProtected /*C*/);
			}
			public virtual void /*void*/ ISteamGameServer_SetSpectatorPort( ushort /*uint16*/ unSpectatorPort )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamGameServer _ptr is null!" );
				
				Native.ISteamGameServer.SetSpectatorPort(_ptr, unSpectatorPort /*C*/);
			}
			public virtual void /*void*/ ISteamGameServer_SetSpectatorServerName( string /*const char **/ pszSpectatorServerName )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamGameServer _ptr is null!" );
				
				Native.ISteamGameServer.SetSpectatorServerName(_ptr, pszSpectatorServerName /*C*/);
			}
			public virtual void /*void*/ ISteamGameServer_ClearAllKeyValues()
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamGameServer _ptr is null!" );
				
				Native.ISteamGameServer.ClearAllKeyValues(_ptr);
			}
			public virtual void /*void*/ ISteamGameServer_SetKeyValue( string /*const char **/ pKey, string /*const char **/ pValue )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamGameServer _ptr is null!" );
				
				Native.ISteamGameServer.SetKeyValue(_ptr, pKey /*C*/, pValue /*C*/);
			}
			public virtual void /*void*/ ISteamGameServer_SetGameTags( string /*const char **/ pchGameTags )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamGameServer _ptr is null!" );
				
				Native.ISteamGameServer.SetGameTags(_ptr, pchGameTags /*C*/);
			}
			public virtual void /*void*/ ISteamGameServer_SetGameData( string /*const char **/ pchGameData )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamGameServer _ptr is null!" );
				
				Native.ISteamGameServer.SetGameData(_ptr, pchGameData /*C*/);
			}
			public virtual void /*void*/ ISteamGameServer_SetRegion( string /*const char **/ pszRegion )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamGameServer _ptr is null!" );
				
				Native.ISteamGameServer.SetRegion(_ptr, pszRegion /*C*/);
			}
			public virtual bool /*bool*/ ISteamGameServer_SendUserConnectAndAuthenticate( uint /*uint32*/ unIPClient, IntPtr /*const void **/ pvAuthBlob, uint /*uint32*/ cubAuthBlobSize, out ulong pSteamIDUser )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamGameServer _ptr is null!" );
				
				return Native.ISteamGameServer.SendUserConnectAndAuthenticate(_ptr, unIPClient /*C*/, pvAuthBlob /*C*/, cubAuthBlobSize /*C*/, out pSteamIDUser /*B*/);
			}
			public virtual CSteamID /*(class CSteamID)*/ ISteamGameServer_CreateUnauthenticatedUserConnection()
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamGameServer _ptr is null!" );
				
				return Native.ISteamGameServer.CreateUnauthenticatedUserConnection(_ptr);
			}
			public virtual void /*void*/ ISteamGameServer_SendUserDisconnect( ulong steamIDUser )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamGameServer _ptr is null!" );
				
				Native.ISteamGameServer.SendUserDisconnect(_ptr, steamIDUser /*C*/);
			}
			public virtual bool /*bool*/ ISteamGameServer_BUpdateUserData( ulong steamIDUser, string /*const char **/ pchPlayerName, uint /*uint32*/ uScore )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamGameServer _ptr is null!" );
				
				return Native.ISteamGameServer.BUpdateUserData(_ptr, steamIDUser /*C*/, pchPlayerName /*C*/, uScore /*C*/);
			}
			public virtual HAuthTicket /*(HAuthTicket)*/ ISteamGameServer_GetAuthSessionTicket( IntPtr /*void **/ pTicket, int /*int*/ cbMaxTicket, out uint /*uint32 **/ pcbTicket )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamGameServer _ptr is null!" );
				
				return Native.ISteamGameServer.GetAuthSessionTicket(_ptr, pTicket /*C*/, cbMaxTicket /*C*/, out pcbTicket /*B*/);
			}
			public virtual BeginAuthSessionResult /*EBeginAuthSessionResult*/ ISteamGameServer_BeginAuthSession( IntPtr /*const void **/ pAuthTicket, int /*int*/ cbAuthTicket, ulong steamID )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamGameServer _ptr is null!" );
				
				return Native.ISteamGameServer.BeginAuthSession(_ptr, pAuthTicket /*C*/, cbAuthTicket /*C*/, steamID /*C*/);
			}
			public virtual void /*void*/ ISteamGameServer_EndAuthSession( ulong steamID )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamGameServer _ptr is null!" );
				
				Native.ISteamGameServer.EndAuthSession(_ptr, steamID /*C*/);
			}
			public virtual void /*void*/ ISteamGameServer_CancelAuthTicket( uint hAuthTicket )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamGameServer _ptr is null!" );
				
				Native.ISteamGameServer.CancelAuthTicket(_ptr, hAuthTicket /*C*/);
			}
			public virtual UserHasLicenseForAppResult /*EUserHasLicenseForAppResult*/ ISteamGameServer_UserHasLicenseForApp( ulong steamID, uint appID )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamGameServer _ptr is null!" );
				
				return Native.ISteamGameServer.UserHasLicenseForApp(_ptr, steamID /*C*/, appID /*C*/);
			}
			public virtual bool /*bool*/ ISteamGameServer_RequestUserGroupStatus( ulong steamIDUser, ulong steamIDGroup )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamGameServer _ptr is null!" );
				
				return Native.ISteamGameServer.RequestUserGroupStatus(_ptr, steamIDUser /*C*/, steamIDGroup /*C*/);
			}
			public virtual void /*void*/ ISteamGameServer_GetGameplayStats()
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamGameServer _ptr is null!" );
				
				Native.ISteamGameServer.GetGameplayStats(_ptr);
			}
			public virtual SteamAPICall_t /*(SteamAPICall_t)*/ ISteamGameServer_GetServerReputation()
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamGameServer _ptr is null!" );
				
				return Native.ISteamGameServer.GetServerReputation(_ptr);
			}
			public virtual uint /*uint32*/ ISteamGameServer_GetPublicIP()
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamGameServer _ptr is null!" );
				
				return Native.ISteamGameServer.GetPublicIP(_ptr);
			}
			public virtual bool /*bool*/ ISteamGameServer_HandleIncomingPacket( IntPtr /*const void **/ pData, int /*int*/ cbData, uint /*uint32*/ srcIP, ushort /*uint16*/ srcPort )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamGameServer _ptr is null!" );
				
				return Native.ISteamGameServer.HandleIncomingPacket(_ptr, pData /*C*/, cbData /*C*/, srcIP /*C*/, srcPort /*C*/);
			}
			public virtual int /*int*/ ISteamGameServer_GetNextOutgoingPacket( IntPtr /*void **/ pOut, int /*int*/ cbMaxOut, out uint /*uint32 **/ pNetAdr, out ushort /*uint16 **/ pPort )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamGameServer _ptr is null!" );
				
				return Native.ISteamGameServer.GetNextOutgoingPacket(_ptr, pOut /*C*/, cbMaxOut /*C*/, out pNetAdr /*B*/, out pPort /*B*/);
			}
			public virtual void /*void*/ ISteamGameServer_EnableHeartbeats( [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bActive )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamGameServer _ptr is null!" );
				
				Native.ISteamGameServer.EnableHeartbeats(_ptr, bActive /*C*/);
			}
			public virtual void /*void*/ ISteamGameServer_SetHeartbeatInterval( int /*int*/ iHeartbeatInterval )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamGameServer _ptr is null!" );
				
				Native.ISteamGameServer.SetHeartbeatInterval(_ptr, iHeartbeatInterval /*C*/);
			}
			public virtual void /*void*/ ISteamGameServer_ForceHeartbeat()
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamGameServer _ptr is null!" );
				
				Native.ISteamGameServer.ForceHeartbeat(_ptr);
			}
			public virtual SteamAPICall_t /*(SteamAPICall_t)*/ ISteamGameServer_AssociateWithClan( ulong steamIDClan )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamGameServer _ptr is null!" );
				
				return Native.ISteamGameServer.AssociateWithClan(_ptr, steamIDClan /*C*/);
			}
			public virtual SteamAPICall_t /*(SteamAPICall_t)*/ ISteamGameServer_ComputeNewPlayerCompatibility( ulong steamIDNewPlayer )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamGameServer _ptr is null!" );
				
				return Native.ISteamGameServer.ComputeNewPlayerCompatibility(_ptr, steamIDNewPlayer /*C*/);
			}
			
			public virtual SteamAPICall_t /*(SteamAPICall_t)*/ ISteamGameServerStats_RequestUserStats( ulong steamIDUser )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamGameServerStats _ptr is null!" );
				
				return Native.ISteamGameServerStats.RequestUserStats(_ptr, steamIDUser /*C*/);
			}
			public virtual bool /*bool*/ ISteamGameServerStats_GetUserStat( ulong steamIDUser, string /*const char **/ pchName, out int /*int32 **/ pData )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamGameServerStats _ptr is null!" );
				
				return Native.ISteamGameServerStats.GetUserStat(_ptr, steamIDUser /*C*/, pchName /*C*/, out pData /*B*/);
			}
			public virtual bool /*bool*/ ISteamGameServerStats_GetUserStat0( ulong steamIDUser, string /*const char **/ pchName, out float /*float **/ pData )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamGameServerStats _ptr is null!" );
				
				return Native.ISteamGameServerStats.GetUserStat0(_ptr, steamIDUser /*C*/, pchName /*C*/, out pData /*B*/);
			}
			public virtual bool /*bool*/ ISteamGameServerStats_GetUserAchievement( ulong steamIDUser, string /*const char **/ pchName, [MarshalAs(UnmanagedType.U1)] ref bool /*bool **/ pbAchieved )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamGameServerStats _ptr is null!" );
				
				return Native.ISteamGameServerStats.GetUserAchievement(_ptr, steamIDUser /*C*/, pchName /*C*/, ref pbAchieved /*A*/);
			}
			public virtual bool /*bool*/ ISteamGameServerStats_SetUserStat( ulong steamIDUser, string /*const char **/ pchName, int /*int32*/ nData )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamGameServerStats _ptr is null!" );
				
				return Native.ISteamGameServerStats.SetUserStat(_ptr, steamIDUser /*C*/, pchName /*C*/, nData /*C*/);
			}
			public virtual bool /*bool*/ ISteamGameServerStats_SetUserStat0( ulong steamIDUser, string /*const char **/ pchName, float /*float*/ fData )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamGameServerStats _ptr is null!" );
				
				return Native.ISteamGameServerStats.SetUserStat0(_ptr, steamIDUser /*C*/, pchName /*C*/, fData /*C*/);
			}
			public virtual bool /*bool*/ ISteamGameServerStats_UpdateUserAvgRateStat( ulong steamIDUser, string /*const char **/ pchName, float /*float*/ flCountThisSession, double /*double*/ dSessionLength )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamGameServerStats _ptr is null!" );
				
				return Native.ISteamGameServerStats.UpdateUserAvgRateStat(_ptr, steamIDUser /*C*/, pchName /*C*/, flCountThisSession /*C*/, dSessionLength /*C*/);
			}
			public virtual bool /*bool*/ ISteamGameServerStats_SetUserAchievement( ulong steamIDUser, string /*const char **/ pchName )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamGameServerStats _ptr is null!" );
				
				return Native.ISteamGameServerStats.SetUserAchievement(_ptr, steamIDUser /*C*/, pchName /*C*/);
			}
			public virtual bool /*bool*/ ISteamGameServerStats_ClearUserAchievement( ulong steamIDUser, string /*const char **/ pchName )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamGameServerStats _ptr is null!" );
				
				return Native.ISteamGameServerStats.ClearUserAchievement(_ptr, steamIDUser /*C*/, pchName /*C*/);
			}
			public virtual SteamAPICall_t /*(SteamAPICall_t)*/ ISteamGameServerStats_StoreUserStats( ulong steamIDUser )
			{
				if ( _ptr == null ) throw new System.Exception( "ISteamGameServerStats _ptr is null!" );
				
				return Native.ISteamGameServerStats.StoreUserStats(_ptr, steamIDUser /*C*/);
			}
			
			public virtual void /*void*/ SteamApi_SteamAPI_Init()
			{
				Native.SteamApi.SteamAPI_Init();
			}
			public virtual void /*void*/ SteamApi_SteamAPI_RunCallbacks()
			{
				Native.SteamApi.SteamAPI_RunCallbacks();
			}
			public virtual void /*void*/ SteamApi_SteamGameServer_RunCallbacks()
			{
				Native.SteamApi.SteamGameServer_RunCallbacks();
			}
			public virtual void /*void*/ SteamApi_SteamAPI_RegisterCallback( IntPtr /*void **/ pCallback, int /*int*/ callback )
			{
				Native.SteamApi.SteamAPI_RegisterCallback(pCallback /*C*/, callback /*C*/);
			}
			public virtual void /*void*/ SteamApi_SteamAPI_UnregisterCallback( IntPtr /*void **/ pCallback )
			{
				Native.SteamApi.SteamAPI_UnregisterCallback(pCallback /*C*/);
			}
			public virtual bool /*bool*/ SteamApi_SteamInternal_GameServer_Init( uint /*uint32*/ unIP, ushort /*uint16*/ usPort, ushort /*uint16*/ usGamePort, ushort /*uint16*/ usQueryPort, int /*int*/ eServerMode, string /*const char **/ pchVersionString )
			{
				return Native.SteamApi.SteamInternal_GameServer_Init(unIP /*C*/, usPort /*C*/, usGamePort /*C*/, usQueryPort /*C*/, eServerMode /*C*/, pchVersionString /*C*/);
			}
			public virtual void /*void*/ SteamApi_SteamAPI_Shutdown()
			{
				Native.SteamApi.SteamAPI_Shutdown();
			}
			public virtual HSteamUser /*(HSteamUser)*/ SteamApi_SteamAPI_GetHSteamUser()
			{
				return Native.SteamApi.SteamAPI_GetHSteamUser();
			}
			public virtual HSteamPipe /*(HSteamPipe)*/ SteamApi_SteamAPI_GetHSteamPipe()
			{
				return Native.SteamApi.SteamAPI_GetHSteamPipe();
			}
			public virtual HSteamUser /*(HSteamUser)*/ SteamApi_SteamGameServer_GetHSteamUser()
			{
				return Native.SteamApi.SteamGameServer_GetHSteamUser();
			}
			public virtual HSteamPipe /*(HSteamPipe)*/ SteamApi_SteamGameServer_GetHSteamPipe()
			{
				return Native.SteamApi.SteamGameServer_GetHSteamPipe();
			}
			public virtual IntPtr /*void **/ SteamApi_SteamInternal_CreateInterface( string /*const char **/ version )
			{
				return Native.SteamApi.SteamInternal_CreateInterface(version /*C*/);
			}
			
			internal static unsafe class Native
			{
				internal static unsafe class ISteamClient
				{
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamClient_CreateSteamPipe" )]
					internal static extern HSteamPipe /*(HSteamPipe)*/ CreateSteamPipe( IntPtr ISteamClient );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamClient_BReleaseSteamPipe" )]
					internal static extern bool /*bool*/ BReleaseSteamPipe( IntPtr ISteamClient, int hSteamPipe );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamClient_ConnectToGlobalUser" )]
					internal static extern HSteamUser /*(HSteamUser)*/ ConnectToGlobalUser( IntPtr ISteamClient, int hSteamPipe );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamClient_CreateLocalUser" )]
					internal static extern HSteamUser /*(HSteamUser)*/ CreateLocalUser( IntPtr ISteamClient, out int phSteamPipe, AccountType /*EAccountType*/ eAccountType );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamClient_ReleaseUser" )]
					internal static extern void /*void*/ ReleaseUser( IntPtr ISteamClient, int hSteamPipe, int hUser );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamClient_GetISteamUser" )]
					internal static extern IntPtr /*class ISteamUser **/ GetISteamUser( IntPtr ISteamClient, int hSteamUser, int hSteamPipe, string /*const char **/ pchVersion );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamClient_GetISteamGameServer" )]
					internal static extern IntPtr /*class ISteamGameServer **/ GetISteamGameServer( IntPtr ISteamClient, int hSteamUser, int hSteamPipe, string /*const char **/ pchVersion );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamClient_SetLocalIPBinding" )]
					internal static extern void /*void*/ SetLocalIPBinding( IntPtr ISteamClient, uint /*uint32*/ unIP, ushort /*uint16*/ usPort );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamClient_GetISteamFriends" )]
					internal static extern IntPtr /*class ISteamFriends **/ GetISteamFriends( IntPtr ISteamClient, int hSteamUser, int hSteamPipe, string /*const char **/ pchVersion );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamClient_GetISteamUtils" )]
					internal static extern IntPtr /*class ISteamUtils **/ GetISteamUtils( IntPtr ISteamClient, int hSteamPipe, string /*const char **/ pchVersion );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamClient_GetISteamMatchmaking" )]
					internal static extern IntPtr /*class ISteamMatchmaking **/ GetISteamMatchmaking( IntPtr ISteamClient, int hSteamUser, int hSteamPipe, string /*const char **/ pchVersion );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamClient_GetISteamMatchmakingServers" )]
					internal static extern IntPtr /*class ISteamMatchmakingServers **/ GetISteamMatchmakingServers( IntPtr ISteamClient, int hSteamUser, int hSteamPipe, string /*const char **/ pchVersion );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamClient_GetISteamGenericInterface" )]
					internal static extern IntPtr /*void **/ GetISteamGenericInterface( IntPtr ISteamClient, int hSteamUser, int hSteamPipe, string /*const char **/ pchVersion );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamClient_GetISteamUserStats" )]
					internal static extern IntPtr /*class ISteamUserStats **/ GetISteamUserStats( IntPtr ISteamClient, int hSteamUser, int hSteamPipe, string /*const char **/ pchVersion );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamClient_GetISteamGameServerStats" )]
					internal static extern IntPtr /*class ISteamGameServerStats **/ GetISteamGameServerStats( IntPtr ISteamClient, int hSteamuser, int hSteamPipe, string /*const char **/ pchVersion );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamClient_GetISteamApps" )]
					internal static extern IntPtr /*class ISteamApps **/ GetISteamApps( IntPtr ISteamClient, int hSteamUser, int hSteamPipe, string /*const char **/ pchVersion );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamClient_GetISteamNetworking" )]
					internal static extern IntPtr /*class ISteamNetworking **/ GetISteamNetworking( IntPtr ISteamClient, int hSteamUser, int hSteamPipe, string /*const char **/ pchVersion );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamClient_GetISteamRemoteStorage" )]
					internal static extern IntPtr /*class ISteamRemoteStorage **/ GetISteamRemoteStorage( IntPtr ISteamClient, int hSteamuser, int hSteamPipe, string /*const char **/ pchVersion );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamClient_GetISteamScreenshots" )]
					internal static extern IntPtr /*class ISteamScreenshots **/ GetISteamScreenshots( IntPtr ISteamClient, int hSteamuser, int hSteamPipe, string /*const char **/ pchVersion );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamClient_GetIPCCallCount" )]
					internal static extern uint /*uint32*/ GetIPCCallCount( IntPtr ISteamClient );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamClient_SetWarningMessageHook" )]
					internal static extern void /*void*/ SetWarningMessageHook( IntPtr ISteamClient, IntPtr /*SteamAPIWarningMessageHook_t*/ pFunction );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamClient_BShutdownIfAllPipesClosed" )]
					internal static extern bool /*bool*/ BShutdownIfAllPipesClosed( IntPtr ISteamClient );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamClient_GetISteamHTTP" )]
					internal static extern IntPtr /*class ISteamHTTP **/ GetISteamHTTP( IntPtr ISteamClient, int hSteamuser, int hSteamPipe, string /*const char **/ pchVersion );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamClient_GetISteamUnifiedMessages" )]
					internal static extern IntPtr /*class ISteamUnifiedMessages **/ GetISteamUnifiedMessages( IntPtr ISteamClient, int hSteamuser, int hSteamPipe, string /*const char **/ pchVersion );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamClient_GetISteamController" )]
					internal static extern IntPtr /*class ISteamController **/ GetISteamController( IntPtr ISteamClient, int hSteamUser, int hSteamPipe, string /*const char **/ pchVersion );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamClient_GetISteamUGC" )]
					internal static extern IntPtr /*class ISteamUGC **/ GetISteamUGC( IntPtr ISteamClient, int hSteamUser, int hSteamPipe, string /*const char **/ pchVersion );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamClient_GetISteamAppList" )]
					internal static extern IntPtr /*class ISteamAppList **/ GetISteamAppList( IntPtr ISteamClient, int hSteamUser, int hSteamPipe, string /*const char **/ pchVersion );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamClient_GetISteamMusic" )]
					internal static extern IntPtr /*class ISteamMusic **/ GetISteamMusic( IntPtr ISteamClient, int hSteamuser, int hSteamPipe, string /*const char **/ pchVersion );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamClient_GetISteamMusicRemote" )]
					internal static extern IntPtr /*class ISteamMusicRemote **/ GetISteamMusicRemote( IntPtr ISteamClient, int hSteamuser, int hSteamPipe, string /*const char **/ pchVersion );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamClient_GetISteamHTMLSurface" )]
					internal static extern IntPtr /*class ISteamHTMLSurface **/ GetISteamHTMLSurface( IntPtr ISteamClient, int hSteamuser, int hSteamPipe, string /*const char **/ pchVersion );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamClient_GetISteamInventory" )]
					internal static extern IntPtr /*class ISteamInventory **/ GetISteamInventory( IntPtr ISteamClient, int hSteamuser, int hSteamPipe, string /*const char **/ pchVersion );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamClient_GetISteamVideo" )]
					internal static extern IntPtr /*class ISteamVideo **/ GetISteamVideo( IntPtr ISteamClient, int hSteamuser, int hSteamPipe, string /*const char **/ pchVersion );
				}
				
				internal static unsafe class ISteamUser
				{
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamUser_GetHSteamUser" )]
					internal static extern HSteamUser /*(HSteamUser)*/ GetHSteamUser( IntPtr ISteamUser );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamUser_BLoggedOn" )]
					internal static extern bool /*bool*/ BLoggedOn( IntPtr ISteamUser );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamUser_GetSteamID" )]
					internal static extern CSteamID /*(class CSteamID)*/ GetSteamID( IntPtr ISteamUser );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamUser_InitiateGameConnection" )]
					internal static extern int /*int*/ InitiateGameConnection( IntPtr ISteamUser, IntPtr /*void **/ pAuthBlob, int /*int*/ cbMaxAuthBlob, ulong steamIDGameServer, uint /*uint32*/ unIPServer, ushort /*uint16*/ usPortServer, [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bSecure );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamUser_TerminateGameConnection" )]
					internal static extern void /*void*/ TerminateGameConnection( IntPtr ISteamUser, uint /*uint32*/ unIPServer, ushort /*uint16*/ usPortServer );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamUser_TrackAppUsageEvent" )]
					internal static extern void /*void*/ TrackAppUsageEvent( IntPtr ISteamUser, ulong gameID, int /*int*/ eAppUsageEvent, string /*const char **/ pchExtraInfo );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamUser_GetUserDataFolder" )]
					internal static extern bool /*bool*/ GetUserDataFolder( IntPtr ISteamUser, System.Text.StringBuilder /*char **/ pchBuffer, int /*int*/ cubBuffer );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamUser_StartVoiceRecording" )]
					internal static extern void /*void*/ StartVoiceRecording( IntPtr ISteamUser );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamUser_StopVoiceRecording" )]
					internal static extern void /*void*/ StopVoiceRecording( IntPtr ISteamUser );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamUser_GetAvailableVoice" )]
					internal static extern VoiceResult /*EVoiceResult*/ GetAvailableVoice( IntPtr ISteamUser, out uint /*uint32 **/ pcbCompressed, out uint /*uint32 **/ pcbUncompressed, uint /*uint32*/ nUncompressedVoiceDesiredSampleRate );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamUser_GetVoice" )]
					internal static extern VoiceResult /*EVoiceResult*/ GetVoice( IntPtr ISteamUser, [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bWantCompressed, IntPtr /*void **/ pDestBuffer, uint /*uint32*/ cbDestBufferSize, out uint /*uint32 **/ nBytesWritten, [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bWantUncompressed, IntPtr /*void **/ pUncompressedDestBuffer, uint /*uint32*/ cbUncompressedDestBufferSize, out uint /*uint32 **/ nUncompressBytesWritten, uint /*uint32*/ nUncompressedVoiceDesiredSampleRate );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamUser_DecompressVoice" )]
					internal static extern VoiceResult /*EVoiceResult*/ DecompressVoice( IntPtr ISteamUser, IntPtr /*const void **/ pCompressed, uint /*uint32*/ cbCompressed, IntPtr /*void **/ pDestBuffer, uint /*uint32*/ cbDestBufferSize, out uint /*uint32 **/ nBytesWritten, uint /*uint32*/ nDesiredSampleRate );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamUser_GetVoiceOptimalSampleRate" )]
					internal static extern uint /*uint32*/ GetVoiceOptimalSampleRate( IntPtr ISteamUser );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamUser_GetAuthSessionTicket" )]
					internal static extern HAuthTicket /*(HAuthTicket)*/ GetAuthSessionTicket( IntPtr ISteamUser, IntPtr /*void **/ pTicket, int /*int*/ cbMaxTicket, out uint /*uint32 **/ pcbTicket );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamUser_BeginAuthSession" )]
					internal static extern BeginAuthSessionResult /*EBeginAuthSessionResult*/ BeginAuthSession( IntPtr ISteamUser, IntPtr /*const void **/ pAuthTicket, int /*int*/ cbAuthTicket, ulong steamID );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamUser_EndAuthSession" )]
					internal static extern void /*void*/ EndAuthSession( IntPtr ISteamUser, ulong steamID );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamUser_CancelAuthTicket" )]
					internal static extern void /*void*/ CancelAuthTicket( IntPtr ISteamUser, uint hAuthTicket );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamUser_UserHasLicenseForApp" )]
					internal static extern UserHasLicenseForAppResult /*EUserHasLicenseForAppResult*/ UserHasLicenseForApp( IntPtr ISteamUser, ulong steamID, uint appID );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamUser_BIsBehindNAT" )]
					internal static extern bool /*bool*/ BIsBehindNAT( IntPtr ISteamUser );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamUser_AdvertiseGame" )]
					internal static extern void /*void*/ AdvertiseGame( IntPtr ISteamUser, ulong steamIDGameServer, uint /*uint32*/ unIPServer, ushort /*uint16*/ usPortServer );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamUser_RequestEncryptedAppTicket" )]
					internal static extern SteamAPICall_t /*(SteamAPICall_t)*/ RequestEncryptedAppTicket( IntPtr ISteamUser, IntPtr /*void **/ pDataToInclude, int /*int*/ cbDataToInclude );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamUser_GetEncryptedAppTicket" )]
					internal static extern bool /*bool*/ GetEncryptedAppTicket( IntPtr ISteamUser, IntPtr /*void **/ pTicket, int /*int*/ cbMaxTicket, out uint /*uint32 **/ pcbTicket );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamUser_GetGameBadgeLevel" )]
					internal static extern int /*int*/ GetGameBadgeLevel( IntPtr ISteamUser, int /*int*/ nSeries, [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bFoil );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamUser_GetPlayerSteamLevel" )]
					internal static extern int /*int*/ GetPlayerSteamLevel( IntPtr ISteamUser );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamUser_RequestStoreAuthURL" )]
					internal static extern SteamAPICall_t /*(SteamAPICall_t)*/ RequestStoreAuthURL( IntPtr ISteamUser, string /*const char **/ pchRedirectURL );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamUser_BIsPhoneVerified" )]
					internal static extern bool /*bool*/ BIsPhoneVerified( IntPtr ISteamUser );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamUser_BIsTwoFactorEnabled" )]
					internal static extern bool /*bool*/ BIsTwoFactorEnabled( IntPtr ISteamUser );
				}
				
				internal static unsafe class ISteamFriends
				{
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamFriends_GetPersonaName" )]
					internal static extern IntPtr GetPersonaName( IntPtr ISteamFriends );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamFriends_SetPersonaName" )]
					internal static extern SteamAPICall_t /*(SteamAPICall_t)*/ SetPersonaName( IntPtr ISteamFriends, string /*const char **/ pchPersonaName );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamFriends_GetPersonaState" )]
					internal static extern PersonaState /*EPersonaState*/ GetPersonaState( IntPtr ISteamFriends );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamFriends_GetFriendCount" )]
					internal static extern int /*int*/ GetFriendCount( IntPtr ISteamFriends, int /*int*/ iFriendFlags );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamFriends_GetFriendByIndex" )]
					internal static extern CSteamID /*(class CSteamID)*/ GetFriendByIndex( IntPtr ISteamFriends, int /*int*/ iFriend, int /*int*/ iFriendFlags );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamFriends_GetFriendRelationship" )]
					internal static extern FriendRelationship /*EFriendRelationship*/ GetFriendRelationship( IntPtr ISteamFriends, ulong steamIDFriend );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamFriends_GetFriendPersonaState" )]
					internal static extern PersonaState /*EPersonaState*/ GetFriendPersonaState( IntPtr ISteamFriends, ulong steamIDFriend );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamFriends_GetFriendPersonaName" )]
					internal static extern IntPtr GetFriendPersonaName( IntPtr ISteamFriends, ulong steamIDFriend );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamFriends_GetFriendGamePlayed" )]
					internal static extern bool /*bool*/ GetFriendGamePlayed( IntPtr ISteamFriends, ulong steamIDFriend, ref FriendGameInfo_t.PackSmall /*struct FriendGameInfo_t **/ pFriendGameInfo );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamFriends_GetFriendPersonaNameHistory" )]
					internal static extern IntPtr GetFriendPersonaNameHistory( IntPtr ISteamFriends, ulong steamIDFriend, int /*int*/ iPersonaName );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamFriends_GetFriendSteamLevel" )]
					internal static extern int /*int*/ GetFriendSteamLevel( IntPtr ISteamFriends, ulong steamIDFriend );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamFriends_GetPlayerNickname" )]
					internal static extern IntPtr GetPlayerNickname( IntPtr ISteamFriends, ulong steamIDPlayer );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamFriends_GetFriendsGroupCount" )]
					internal static extern int /*int*/ GetFriendsGroupCount( IntPtr ISteamFriends );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamFriends_GetFriendsGroupIDByIndex" )]
					internal static extern FriendsGroupID_t /*(FriendsGroupID_t)*/ GetFriendsGroupIDByIndex( IntPtr ISteamFriends, int /*int*/ iFG );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamFriends_GetFriendsGroupName" )]
					internal static extern IntPtr GetFriendsGroupName( IntPtr ISteamFriends, short friendsGroupID );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamFriends_GetFriendsGroupMembersCount" )]
					internal static extern int /*int*/ GetFriendsGroupMembersCount( IntPtr ISteamFriends, short friendsGroupID );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamFriends_GetFriendsGroupMembersList" )]
					internal static extern void /*void*/ GetFriendsGroupMembersList( IntPtr ISteamFriends, short friendsGroupID, IntPtr /*class CSteamID **/ pOutSteamIDMembers, int /*int*/ nMembersCount );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamFriends_HasFriend" )]
					internal static extern bool /*bool*/ HasFriend( IntPtr ISteamFriends, ulong steamIDFriend, int /*int*/ iFriendFlags );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamFriends_GetClanCount" )]
					internal static extern int /*int*/ GetClanCount( IntPtr ISteamFriends );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamFriends_GetClanByIndex" )]
					internal static extern CSteamID /*(class CSteamID)*/ GetClanByIndex( IntPtr ISteamFriends, int /*int*/ iClan );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamFriends_GetClanName" )]
					internal static extern IntPtr GetClanName( IntPtr ISteamFriends, ulong steamIDClan );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamFriends_GetClanTag" )]
					internal static extern IntPtr GetClanTag( IntPtr ISteamFriends, ulong steamIDClan );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamFriends_GetClanActivityCounts" )]
					internal static extern bool /*bool*/ GetClanActivityCounts( IntPtr ISteamFriends, ulong steamIDClan, out int /*int **/ pnOnline, out int /*int **/ pnInGame, out int /*int **/ pnChatting );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamFriends_DownloadClanActivityCounts" )]
					internal static extern SteamAPICall_t /*(SteamAPICall_t)*/ DownloadClanActivityCounts( IntPtr ISteamFriends, IntPtr /*class CSteamID **/ psteamIDClans, int /*int*/ cClansToRequest );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamFriends_GetFriendCountFromSource" )]
					internal static extern int /*int*/ GetFriendCountFromSource( IntPtr ISteamFriends, ulong steamIDSource );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamFriends_GetFriendFromSourceByIndex" )]
					internal static extern CSteamID /*(class CSteamID)*/ GetFriendFromSourceByIndex( IntPtr ISteamFriends, ulong steamIDSource, int /*int*/ iFriend );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamFriends_IsUserInSource" )]
					internal static extern bool /*bool*/ IsUserInSource( IntPtr ISteamFriends, ulong steamIDUser, ulong steamIDSource );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamFriends_SetInGameVoiceSpeaking" )]
					internal static extern void /*void*/ SetInGameVoiceSpeaking( IntPtr ISteamFriends, ulong steamIDUser, [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bSpeaking );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamFriends_ActivateGameOverlay" )]
					internal static extern void /*void*/ ActivateGameOverlay( IntPtr ISteamFriends, string /*const char **/ pchDialog );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamFriends_ActivateGameOverlayToUser" )]
					internal static extern void /*void*/ ActivateGameOverlayToUser( IntPtr ISteamFriends, string /*const char **/ pchDialog, ulong steamID );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamFriends_ActivateGameOverlayToWebPage" )]
					internal static extern void /*void*/ ActivateGameOverlayToWebPage( IntPtr ISteamFriends, string /*const char **/ pchURL );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamFriends_ActivateGameOverlayToStore" )]
					internal static extern void /*void*/ ActivateGameOverlayToStore( IntPtr ISteamFriends, uint nAppID, OverlayToStoreFlag /*EOverlayToStoreFlag*/ eFlag );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamFriends_SetPlayedWith" )]
					internal static extern void /*void*/ SetPlayedWith( IntPtr ISteamFriends, ulong steamIDUserPlayedWith );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamFriends_ActivateGameOverlayInviteDialog" )]
					internal static extern void /*void*/ ActivateGameOverlayInviteDialog( IntPtr ISteamFriends, ulong steamIDLobby );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamFriends_GetSmallFriendAvatar" )]
					internal static extern int /*int*/ GetSmallFriendAvatar( IntPtr ISteamFriends, ulong steamIDFriend );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamFriends_GetMediumFriendAvatar" )]
					internal static extern int /*int*/ GetMediumFriendAvatar( IntPtr ISteamFriends, ulong steamIDFriend );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamFriends_GetLargeFriendAvatar" )]
					internal static extern int /*int*/ GetLargeFriendAvatar( IntPtr ISteamFriends, ulong steamIDFriend );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamFriends_RequestUserInformation" )]
					internal static extern bool /*bool*/ RequestUserInformation( IntPtr ISteamFriends, ulong steamIDUser, [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bRequireNameOnly );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamFriends_RequestClanOfficerList" )]
					internal static extern SteamAPICall_t /*(SteamAPICall_t)*/ RequestClanOfficerList( IntPtr ISteamFriends, ulong steamIDClan );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamFriends_GetClanOwner" )]
					internal static extern CSteamID /*(class CSteamID)*/ GetClanOwner( IntPtr ISteamFriends, ulong steamIDClan );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamFriends_GetClanOfficerCount" )]
					internal static extern int /*int*/ GetClanOfficerCount( IntPtr ISteamFriends, ulong steamIDClan );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamFriends_GetClanOfficerByIndex" )]
					internal static extern CSteamID /*(class CSteamID)*/ GetClanOfficerByIndex( IntPtr ISteamFriends, ulong steamIDClan, int /*int*/ iOfficer );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamFriends_GetUserRestrictions" )]
					internal static extern uint /*uint32*/ GetUserRestrictions( IntPtr ISteamFriends );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamFriends_SetRichPresence" )]
					internal static extern bool /*bool*/ SetRichPresence( IntPtr ISteamFriends, string /*const char **/ pchKey, string /*const char **/ pchValue );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamFriends_ClearRichPresence" )]
					internal static extern void /*void*/ ClearRichPresence( IntPtr ISteamFriends );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamFriends_GetFriendRichPresence" )]
					internal static extern IntPtr GetFriendRichPresence( IntPtr ISteamFriends, ulong steamIDFriend, string /*const char **/ pchKey );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamFriends_GetFriendRichPresenceKeyCount" )]
					internal static extern int /*int*/ GetFriendRichPresenceKeyCount( IntPtr ISteamFriends, ulong steamIDFriend );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamFriends_GetFriendRichPresenceKeyByIndex" )]
					internal static extern IntPtr GetFriendRichPresenceKeyByIndex( IntPtr ISteamFriends, ulong steamIDFriend, int /*int*/ iKey );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamFriends_RequestFriendRichPresence" )]
					internal static extern void /*void*/ RequestFriendRichPresence( IntPtr ISteamFriends, ulong steamIDFriend );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamFriends_InviteUserToGame" )]
					internal static extern bool /*bool*/ InviteUserToGame( IntPtr ISteamFriends, ulong steamIDFriend, string /*const char **/ pchConnectString );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamFriends_GetCoplayFriendCount" )]
					internal static extern int /*int*/ GetCoplayFriendCount( IntPtr ISteamFriends );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamFriends_GetCoplayFriend" )]
					internal static extern CSteamID /*(class CSteamID)*/ GetCoplayFriend( IntPtr ISteamFriends, int /*int*/ iCoplayFriend );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamFriends_GetFriendCoplayTime" )]
					internal static extern int /*int*/ GetFriendCoplayTime( IntPtr ISteamFriends, ulong steamIDFriend );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamFriends_GetFriendCoplayGame" )]
					internal static extern AppId_t /*(AppId_t)*/ GetFriendCoplayGame( IntPtr ISteamFriends, ulong steamIDFriend );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamFriends_JoinClanChatRoom" )]
					internal static extern SteamAPICall_t /*(SteamAPICall_t)*/ JoinClanChatRoom( IntPtr ISteamFriends, ulong steamIDClan );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamFriends_LeaveClanChatRoom" )]
					internal static extern bool /*bool*/ LeaveClanChatRoom( IntPtr ISteamFriends, ulong steamIDClan );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamFriends_GetClanChatMemberCount" )]
					internal static extern int /*int*/ GetClanChatMemberCount( IntPtr ISteamFriends, ulong steamIDClan );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamFriends_GetChatMemberByIndex" )]
					internal static extern CSteamID /*(class CSteamID)*/ GetChatMemberByIndex( IntPtr ISteamFriends, ulong steamIDClan, int /*int*/ iUser );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamFriends_SendClanChatMessage" )]
					internal static extern bool /*bool*/ SendClanChatMessage( IntPtr ISteamFriends, ulong steamIDClanChat, string /*const char **/ pchText );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamFriends_GetClanChatMessage" )]
					internal static extern int /*int*/ GetClanChatMessage( IntPtr ISteamFriends, ulong steamIDClanChat, int /*int*/ iMessage, IntPtr /*void **/ prgchText, int /*int*/ cchTextMax, out ChatEntryType /*EChatEntryType **/ peChatEntryType, out ulong psteamidChatter );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamFriends_IsClanChatAdmin" )]
					internal static extern bool /*bool*/ IsClanChatAdmin( IntPtr ISteamFriends, ulong steamIDClanChat, ulong steamIDUser );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamFriends_IsClanChatWindowOpenInSteam" )]
					internal static extern bool /*bool*/ IsClanChatWindowOpenInSteam( IntPtr ISteamFriends, ulong steamIDClanChat );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamFriends_OpenClanChatWindowInSteam" )]
					internal static extern bool /*bool*/ OpenClanChatWindowInSteam( IntPtr ISteamFriends, ulong steamIDClanChat );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamFriends_CloseClanChatWindowInSteam" )]
					internal static extern bool /*bool*/ CloseClanChatWindowInSteam( IntPtr ISteamFriends, ulong steamIDClanChat );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamFriends_SetListenForFriendsMessages" )]
					internal static extern bool /*bool*/ SetListenForFriendsMessages( IntPtr ISteamFriends, [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bInterceptEnabled );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamFriends_ReplyToFriendMessage" )]
					internal static extern bool /*bool*/ ReplyToFriendMessage( IntPtr ISteamFriends, ulong steamIDFriend, string /*const char **/ pchMsgToSend );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamFriends_GetFriendMessage" )]
					internal static extern int /*int*/ GetFriendMessage( IntPtr ISteamFriends, ulong steamIDFriend, int /*int*/ iMessageID, IntPtr /*void **/ pvData, int /*int*/ cubData, out ChatEntryType /*EChatEntryType **/ peChatEntryType );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamFriends_GetFollowerCount" )]
					internal static extern SteamAPICall_t /*(SteamAPICall_t)*/ GetFollowerCount( IntPtr ISteamFriends, ulong steamID );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamFriends_IsFollowing" )]
					internal static extern SteamAPICall_t /*(SteamAPICall_t)*/ IsFollowing( IntPtr ISteamFriends, ulong steamID );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamFriends_EnumerateFollowingList" )]
					internal static extern SteamAPICall_t /*(SteamAPICall_t)*/ EnumerateFollowingList( IntPtr ISteamFriends, uint /*uint32*/ unStartIndex );
				}
				
				internal static unsafe class ISteamUtils
				{
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamUtils_GetSecondsSinceAppActive" )]
					internal static extern uint /*uint32*/ GetSecondsSinceAppActive( IntPtr ISteamUtils );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamUtils_GetSecondsSinceComputerActive" )]
					internal static extern uint /*uint32*/ GetSecondsSinceComputerActive( IntPtr ISteamUtils );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamUtils_GetConnectedUniverse" )]
					internal static extern Universe /*EUniverse*/ GetConnectedUniverse( IntPtr ISteamUtils );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamUtils_GetServerRealTime" )]
					internal static extern uint /*uint32*/ GetServerRealTime( IntPtr ISteamUtils );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamUtils_GetIPCountry" )]
					internal static extern IntPtr GetIPCountry( IntPtr ISteamUtils );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamUtils_GetImageSize" )]
					internal static extern bool /*bool*/ GetImageSize( IntPtr ISteamUtils, int /*int*/ iImage, out uint /*uint32 **/ pnWidth, out uint /*uint32 **/ pnHeight );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamUtils_GetImageRGBA" )]
					internal static extern bool /*bool*/ GetImageRGBA( IntPtr ISteamUtils, int /*int*/ iImage, IntPtr /*uint8 **/ pubDest, int /*int*/ nDestBufferSize );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamUtils_GetCSERIPPort" )]
					internal static extern bool /*bool*/ GetCSERIPPort( IntPtr ISteamUtils, out uint /*uint32 **/ unIP, out ushort /*uint16 **/ usPort );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamUtils_GetCurrentBatteryPower" )]
					internal static extern byte /*uint8*/ GetCurrentBatteryPower( IntPtr ISteamUtils );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamUtils_GetAppID" )]
					internal static extern uint /*uint32*/ GetAppID( IntPtr ISteamUtils );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamUtils_SetOverlayNotificationPosition" )]
					internal static extern void /*void*/ SetOverlayNotificationPosition( IntPtr ISteamUtils, NotificationPosition /*ENotificationPosition*/ eNotificationPosition );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamUtils_IsAPICallCompleted" )]
					internal static extern bool /*bool*/ IsAPICallCompleted( IntPtr ISteamUtils, ulong hSteamAPICall, [MarshalAs(UnmanagedType.U1)] ref bool /*bool **/ pbFailed );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamUtils_GetAPICallFailureReason" )]
					internal static extern SteamAPICallFailure /*ESteamAPICallFailure*/ GetAPICallFailureReason( IntPtr ISteamUtils, ulong hSteamAPICall );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamUtils_GetAPICallResult" )]
					internal static extern bool /*bool*/ GetAPICallResult( IntPtr ISteamUtils, ulong hSteamAPICall, IntPtr /*void **/ pCallback, int /*int*/ cubCallback, int /*int*/ iCallbackExpected, [MarshalAs(UnmanagedType.U1)] ref bool /*bool **/ pbFailed );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamUtils_GetIPCCallCount" )]
					internal static extern uint /*uint32*/ GetIPCCallCount( IntPtr ISteamUtils );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamUtils_SetWarningMessageHook" )]
					internal static extern void /*void*/ SetWarningMessageHook( IntPtr ISteamUtils, IntPtr /*SteamAPIWarningMessageHook_t*/ pFunction );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamUtils_IsOverlayEnabled" )]
					internal static extern bool /*bool*/ IsOverlayEnabled( IntPtr ISteamUtils );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamUtils_BOverlayNeedsPresent" )]
					internal static extern bool /*bool*/ BOverlayNeedsPresent( IntPtr ISteamUtils );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamUtils_CheckFileSignature" )]
					internal static extern SteamAPICall_t /*(SteamAPICall_t)*/ CheckFileSignature( IntPtr ISteamUtils, string /*const char **/ szFileName );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamUtils_ShowGamepadTextInput" )]
					internal static extern bool /*bool*/ ShowGamepadTextInput( IntPtr ISteamUtils, GamepadTextInputMode /*EGamepadTextInputMode*/ eInputMode, GamepadTextInputLineMode /*EGamepadTextInputLineMode*/ eLineInputMode, string /*const char **/ pchDescription, uint /*uint32*/ unCharMax, string /*const char **/ pchExistingText );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamUtils_GetEnteredGamepadTextLength" )]
					internal static extern uint /*uint32*/ GetEnteredGamepadTextLength( IntPtr ISteamUtils );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamUtils_GetEnteredGamepadTextInput" )]
					internal static extern bool /*bool*/ GetEnteredGamepadTextInput( IntPtr ISteamUtils, System.Text.StringBuilder /*char **/ pchText, uint /*uint32*/ cchText );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamUtils_GetSteamUILanguage" )]
					internal static extern IntPtr GetSteamUILanguage( IntPtr ISteamUtils );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamUtils_IsSteamRunningInVR" )]
					internal static extern bool /*bool*/ IsSteamRunningInVR( IntPtr ISteamUtils );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamUtils_SetOverlayNotificationInset" )]
					internal static extern void /*void*/ SetOverlayNotificationInset( IntPtr ISteamUtils, int /*int*/ nHorizontalInset, int /*int*/ nVerticalInset );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamUtils_IsSteamInBigPictureMode" )]
					internal static extern bool /*bool*/ IsSteamInBigPictureMode( IntPtr ISteamUtils );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamUtils_StartVRDashboard" )]
					internal static extern void /*void*/ StartVRDashboard( IntPtr ISteamUtils );
				}
				
				internal static unsafe class ISteamMatchmaking
				{
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamMatchmaking_GetFavoriteGameCount" )]
					internal static extern int /*int*/ GetFavoriteGameCount( IntPtr ISteamMatchmaking );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamMatchmaking_GetFavoriteGame" )]
					internal static extern bool /*bool*/ GetFavoriteGame( IntPtr ISteamMatchmaking, int /*int*/ iGame, ref uint pnAppID, out uint /*uint32 **/ pnIP, out ushort /*uint16 **/ pnConnPort, out ushort /*uint16 **/ pnQueryPort, IntPtr /*uint32 **/ punFlags, out uint /*uint32 **/ pRTime32LastPlayedOnServer );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamMatchmaking_AddFavoriteGame" )]
					internal static extern int /*int*/ AddFavoriteGame( IntPtr ISteamMatchmaking, uint nAppID, uint /*uint32*/ nIP, ushort /*uint16*/ nConnPort, ushort /*uint16*/ nQueryPort, uint /*uint32*/ unFlags, uint /*uint32*/ rTime32LastPlayedOnServer );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamMatchmaking_RemoveFavoriteGame" )]
					internal static extern bool /*bool*/ RemoveFavoriteGame( IntPtr ISteamMatchmaking, uint nAppID, uint /*uint32*/ nIP, ushort /*uint16*/ nConnPort, ushort /*uint16*/ nQueryPort, uint /*uint32*/ unFlags );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamMatchmaking_RequestLobbyList" )]
					internal static extern SteamAPICall_t /*(SteamAPICall_t)*/ RequestLobbyList( IntPtr ISteamMatchmaking );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamMatchmaking_AddRequestLobbyListStringFilter" )]
					internal static extern void /*void*/ AddRequestLobbyListStringFilter( IntPtr ISteamMatchmaking, string /*const char **/ pchKeyToMatch, string /*const char **/ pchValueToMatch, LobbyComparison /*ELobbyComparison*/ eComparisonType );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamMatchmaking_AddRequestLobbyListNumericalFilter" )]
					internal static extern void /*void*/ AddRequestLobbyListNumericalFilter( IntPtr ISteamMatchmaking, string /*const char **/ pchKeyToMatch, int /*int*/ nValueToMatch, LobbyComparison /*ELobbyComparison*/ eComparisonType );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamMatchmaking_AddRequestLobbyListNearValueFilter" )]
					internal static extern void /*void*/ AddRequestLobbyListNearValueFilter( IntPtr ISteamMatchmaking, string /*const char **/ pchKeyToMatch, int /*int*/ nValueToBeCloseTo );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamMatchmaking_AddRequestLobbyListFilterSlotsAvailable" )]
					internal static extern void /*void*/ AddRequestLobbyListFilterSlotsAvailable( IntPtr ISteamMatchmaking, int /*int*/ nSlotsAvailable );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamMatchmaking_AddRequestLobbyListDistanceFilter" )]
					internal static extern void /*void*/ AddRequestLobbyListDistanceFilter( IntPtr ISteamMatchmaking, LobbyDistanceFilter /*ELobbyDistanceFilter*/ eLobbyDistanceFilter );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamMatchmaking_AddRequestLobbyListResultCountFilter" )]
					internal static extern void /*void*/ AddRequestLobbyListResultCountFilter( IntPtr ISteamMatchmaking, int /*int*/ cMaxResults );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamMatchmaking_AddRequestLobbyListCompatibleMembersFilter" )]
					internal static extern void /*void*/ AddRequestLobbyListCompatibleMembersFilter( IntPtr ISteamMatchmaking, ulong steamIDLobby );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamMatchmaking_GetLobbyByIndex" )]
					internal static extern CSteamID /*(class CSteamID)*/ GetLobbyByIndex( IntPtr ISteamMatchmaking, int /*int*/ iLobby );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamMatchmaking_CreateLobby" )]
					internal static extern SteamAPICall_t /*(SteamAPICall_t)*/ CreateLobby( IntPtr ISteamMatchmaking, LobbyType /*ELobbyType*/ eLobbyType, int /*int*/ cMaxMembers );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamMatchmaking_JoinLobby" )]
					internal static extern SteamAPICall_t /*(SteamAPICall_t)*/ JoinLobby( IntPtr ISteamMatchmaking, ulong steamIDLobby );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamMatchmaking_LeaveLobby" )]
					internal static extern void /*void*/ LeaveLobby( IntPtr ISteamMatchmaking, ulong steamIDLobby );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamMatchmaking_InviteUserToLobby" )]
					internal static extern bool /*bool*/ InviteUserToLobby( IntPtr ISteamMatchmaking, ulong steamIDLobby, ulong steamIDInvitee );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamMatchmaking_GetNumLobbyMembers" )]
					internal static extern int /*int*/ GetNumLobbyMembers( IntPtr ISteamMatchmaking, ulong steamIDLobby );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamMatchmaking_GetLobbyMemberByIndex" )]
					internal static extern CSteamID /*(class CSteamID)*/ GetLobbyMemberByIndex( IntPtr ISteamMatchmaking, ulong steamIDLobby, int /*int*/ iMember );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamMatchmaking_GetLobbyData" )]
					internal static extern IntPtr GetLobbyData( IntPtr ISteamMatchmaking, ulong steamIDLobby, string /*const char **/ pchKey );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamMatchmaking_SetLobbyData" )]
					internal static extern bool /*bool*/ SetLobbyData( IntPtr ISteamMatchmaking, ulong steamIDLobby, string /*const char **/ pchKey, string /*const char **/ pchValue );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamMatchmaking_GetLobbyDataCount" )]
					internal static extern int /*int*/ GetLobbyDataCount( IntPtr ISteamMatchmaking, ulong steamIDLobby );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamMatchmaking_GetLobbyDataByIndex" )]
					internal static extern bool /*bool*/ GetLobbyDataByIndex( IntPtr ISteamMatchmaking, ulong steamIDLobby, int /*int*/ iLobbyData, System.Text.StringBuilder /*char **/ pchKey, int /*int*/ cchKeyBufferSize, System.Text.StringBuilder /*char **/ pchValue, int /*int*/ cchValueBufferSize );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamMatchmaking_DeleteLobbyData" )]
					internal static extern bool /*bool*/ DeleteLobbyData( IntPtr ISteamMatchmaking, ulong steamIDLobby, string /*const char **/ pchKey );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamMatchmaking_GetLobbyMemberData" )]
					internal static extern IntPtr GetLobbyMemberData( IntPtr ISteamMatchmaking, ulong steamIDLobby, ulong steamIDUser, string /*const char **/ pchKey );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamMatchmaking_SetLobbyMemberData" )]
					internal static extern void /*void*/ SetLobbyMemberData( IntPtr ISteamMatchmaking, ulong steamIDLobby, string /*const char **/ pchKey, string /*const char **/ pchValue );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamMatchmaking_SendLobbyChatMsg" )]
					internal static extern bool /*bool*/ SendLobbyChatMsg( IntPtr ISteamMatchmaking, ulong steamIDLobby, IntPtr /*const void **/ pvMsgBody, int /*int*/ cubMsgBody );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamMatchmaking_GetLobbyChatEntry" )]
					internal static extern int /*int*/ GetLobbyChatEntry( IntPtr ISteamMatchmaking, ulong steamIDLobby, int /*int*/ iChatID, out ulong pSteamIDUser, IntPtr /*void **/ pvData, int /*int*/ cubData, out ChatEntryType /*EChatEntryType **/ peChatEntryType );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamMatchmaking_RequestLobbyData" )]
					internal static extern bool /*bool*/ RequestLobbyData( IntPtr ISteamMatchmaking, ulong steamIDLobby );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamMatchmaking_SetLobbyGameServer" )]
					internal static extern void /*void*/ SetLobbyGameServer( IntPtr ISteamMatchmaking, ulong steamIDLobby, uint /*uint32*/ unGameServerIP, ushort /*uint16*/ unGameServerPort, ulong steamIDGameServer );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamMatchmaking_GetLobbyGameServer" )]
					internal static extern bool /*bool*/ GetLobbyGameServer( IntPtr ISteamMatchmaking, ulong steamIDLobby, out uint /*uint32 **/ punGameServerIP, out ushort /*uint16 **/ punGameServerPort, out ulong psteamIDGameServer );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamMatchmaking_SetLobbyMemberLimit" )]
					internal static extern bool /*bool*/ SetLobbyMemberLimit( IntPtr ISteamMatchmaking, ulong steamIDLobby, int /*int*/ cMaxMembers );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamMatchmaking_GetLobbyMemberLimit" )]
					internal static extern int /*int*/ GetLobbyMemberLimit( IntPtr ISteamMatchmaking, ulong steamIDLobby );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamMatchmaking_SetLobbyType" )]
					internal static extern bool /*bool*/ SetLobbyType( IntPtr ISteamMatchmaking, ulong steamIDLobby, LobbyType /*ELobbyType*/ eLobbyType );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamMatchmaking_SetLobbyJoinable" )]
					internal static extern bool /*bool*/ SetLobbyJoinable( IntPtr ISteamMatchmaking, ulong steamIDLobby, [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bLobbyJoinable );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamMatchmaking_GetLobbyOwner" )]
					internal static extern CSteamID /*(class CSteamID)*/ GetLobbyOwner( IntPtr ISteamMatchmaking, ulong steamIDLobby );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamMatchmaking_SetLobbyOwner" )]
					internal static extern bool /*bool*/ SetLobbyOwner( IntPtr ISteamMatchmaking, ulong steamIDLobby, ulong steamIDNewOwner );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamMatchmaking_SetLinkedLobby" )]
					internal static extern bool /*bool*/ SetLinkedLobby( IntPtr ISteamMatchmaking, ulong steamIDLobby, ulong steamIDLobbyDependent );
				}
				
				internal static unsafe class ISteamMatchmakingServers
				{
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamMatchmakingServers_RequestInternetServerList" )]
					internal static extern HServerListRequest /*(HServerListRequest)*/ RequestInternetServerList( IntPtr ISteamMatchmakingServers, uint iApp, IntPtr /*struct MatchMakingKeyValuePair_t ***/ ppchFilters, uint /*uint32*/ nFilters, IntPtr /*class ISteamMatchmakingServerListResponse **/ pRequestServersResponse );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamMatchmakingServers_RequestLANServerList" )]
					internal static extern HServerListRequest /*(HServerListRequest)*/ RequestLANServerList( IntPtr ISteamMatchmakingServers, uint iApp, IntPtr /*class ISteamMatchmakingServerListResponse **/ pRequestServersResponse );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamMatchmakingServers_RequestFriendsServerList" )]
					internal static extern HServerListRequest /*(HServerListRequest)*/ RequestFriendsServerList( IntPtr ISteamMatchmakingServers, uint iApp, IntPtr /*struct MatchMakingKeyValuePair_t ***/ ppchFilters, uint /*uint32*/ nFilters, IntPtr /*class ISteamMatchmakingServerListResponse **/ pRequestServersResponse );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamMatchmakingServers_RequestFavoritesServerList" )]
					internal static extern HServerListRequest /*(HServerListRequest)*/ RequestFavoritesServerList( IntPtr ISteamMatchmakingServers, uint iApp, IntPtr /*struct MatchMakingKeyValuePair_t ***/ ppchFilters, uint /*uint32*/ nFilters, IntPtr /*class ISteamMatchmakingServerListResponse **/ pRequestServersResponse );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamMatchmakingServers_RequestHistoryServerList" )]
					internal static extern HServerListRequest /*(HServerListRequest)*/ RequestHistoryServerList( IntPtr ISteamMatchmakingServers, uint iApp, IntPtr /*struct MatchMakingKeyValuePair_t ***/ ppchFilters, uint /*uint32*/ nFilters, IntPtr /*class ISteamMatchmakingServerListResponse **/ pRequestServersResponse );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamMatchmakingServers_RequestSpectatorServerList" )]
					internal static extern HServerListRequest /*(HServerListRequest)*/ RequestSpectatorServerList( IntPtr ISteamMatchmakingServers, uint iApp, IntPtr /*struct MatchMakingKeyValuePair_t ***/ ppchFilters, uint /*uint32*/ nFilters, IntPtr /*class ISteamMatchmakingServerListResponse **/ pRequestServersResponse );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamMatchmakingServers_ReleaseRequest" )]
					internal static extern void /*void*/ ReleaseRequest( IntPtr ISteamMatchmakingServers, IntPtr hServerListRequest );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamMatchmakingServers_GetServerDetails" )]
					internal static extern IntPtr /*class gameserveritem_t **/ GetServerDetails( IntPtr ISteamMatchmakingServers, IntPtr hRequest, int /*int*/ iServer );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamMatchmakingServers_CancelQuery" )]
					internal static extern void /*void*/ CancelQuery( IntPtr ISteamMatchmakingServers, IntPtr hRequest );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamMatchmakingServers_RefreshQuery" )]
					internal static extern void /*void*/ RefreshQuery( IntPtr ISteamMatchmakingServers, IntPtr hRequest );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamMatchmakingServers_IsRefreshing" )]
					internal static extern bool /*bool*/ IsRefreshing( IntPtr ISteamMatchmakingServers, IntPtr hRequest );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamMatchmakingServers_GetServerCount" )]
					internal static extern int /*int*/ GetServerCount( IntPtr ISteamMatchmakingServers, IntPtr hRequest );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamMatchmakingServers_RefreshServer" )]
					internal static extern void /*void*/ RefreshServer( IntPtr ISteamMatchmakingServers, IntPtr hRequest, int /*int*/ iServer );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamMatchmakingServers_PingServer" )]
					internal static extern HServerQuery /*(HServerQuery)*/ PingServer( IntPtr ISteamMatchmakingServers, uint /*uint32*/ unIP, ushort /*uint16*/ usPort, IntPtr /*class ISteamMatchmakingPingResponse **/ pRequestServersResponse );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamMatchmakingServers_PlayerDetails" )]
					internal static extern HServerQuery /*(HServerQuery)*/ PlayerDetails( IntPtr ISteamMatchmakingServers, uint /*uint32*/ unIP, ushort /*uint16*/ usPort, IntPtr /*class ISteamMatchmakingPlayersResponse **/ pRequestServersResponse );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamMatchmakingServers_ServerRules" )]
					internal static extern HServerQuery /*(HServerQuery)*/ ServerRules( IntPtr ISteamMatchmakingServers, uint /*uint32*/ unIP, ushort /*uint16*/ usPort, IntPtr /*class ISteamMatchmakingRulesResponse **/ pRequestServersResponse );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamMatchmakingServers_CancelServerQuery" )]
					internal static extern void /*void*/ CancelServerQuery( IntPtr ISteamMatchmakingServers, int hServerQuery );
				}
				
				internal static unsafe class ISteamRemoteStorage
				{
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamRemoteStorage_FileWrite" )]
					internal static extern bool /*bool*/ FileWrite( IntPtr ISteamRemoteStorage, string /*const char **/ pchFile, IntPtr /*const void **/ pvData, int /*int32*/ cubData );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamRemoteStorage_FileRead" )]
					internal static extern int /*int32*/ FileRead( IntPtr ISteamRemoteStorage, string /*const char **/ pchFile, IntPtr /*void **/ pvData, int /*int32*/ cubDataToRead );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamRemoteStorage_FileWriteAsync" )]
					internal static extern SteamAPICall_t /*(SteamAPICall_t)*/ FileWriteAsync( IntPtr ISteamRemoteStorage, string /*const char **/ pchFile, IntPtr /*const void **/ pvData, uint /*uint32*/ cubData );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamRemoteStorage_FileReadAsync" )]
					internal static extern SteamAPICall_t /*(SteamAPICall_t)*/ FileReadAsync( IntPtr ISteamRemoteStorage, string /*const char **/ pchFile, uint /*uint32*/ nOffset, uint /*uint32*/ cubToRead );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamRemoteStorage_FileReadAsyncComplete" )]
					internal static extern bool /*bool*/ FileReadAsyncComplete( IntPtr ISteamRemoteStorage, ulong hReadCall, IntPtr /*void **/ pvBuffer, uint /*uint32*/ cubToRead );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamRemoteStorage_FileForget" )]
					internal static extern bool /*bool*/ FileForget( IntPtr ISteamRemoteStorage, string /*const char **/ pchFile );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamRemoteStorage_FileDelete" )]
					internal static extern bool /*bool*/ FileDelete( IntPtr ISteamRemoteStorage, string /*const char **/ pchFile );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamRemoteStorage_FileShare" )]
					internal static extern SteamAPICall_t /*(SteamAPICall_t)*/ FileShare( IntPtr ISteamRemoteStorage, string /*const char **/ pchFile );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamRemoteStorage_SetSyncPlatforms" )]
					internal static extern bool /*bool*/ SetSyncPlatforms( IntPtr ISteamRemoteStorage, string /*const char **/ pchFile, RemoteStoragePlatform /*ERemoteStoragePlatform*/ eRemoteStoragePlatform );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamRemoteStorage_FileWriteStreamOpen" )]
					internal static extern UGCFileWriteStreamHandle_t /*(UGCFileWriteStreamHandle_t)*/ FileWriteStreamOpen( IntPtr ISteamRemoteStorage, string /*const char **/ pchFile );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamRemoteStorage_FileWriteStreamWriteChunk" )]
					internal static extern bool /*bool*/ FileWriteStreamWriteChunk( IntPtr ISteamRemoteStorage, ulong writeHandle, IntPtr /*const void **/ pvData, int /*int32*/ cubData );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamRemoteStorage_FileWriteStreamClose" )]
					internal static extern bool /*bool*/ FileWriteStreamClose( IntPtr ISteamRemoteStorage, ulong writeHandle );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamRemoteStorage_FileWriteStreamCancel" )]
					internal static extern bool /*bool*/ FileWriteStreamCancel( IntPtr ISteamRemoteStorage, ulong writeHandle );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamRemoteStorage_FileExists" )]
					internal static extern bool /*bool*/ FileExists( IntPtr ISteamRemoteStorage, string /*const char **/ pchFile );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamRemoteStorage_FilePersisted" )]
					internal static extern bool /*bool*/ FilePersisted( IntPtr ISteamRemoteStorage, string /*const char **/ pchFile );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamRemoteStorage_GetFileSize" )]
					internal static extern int /*int32*/ GetFileSize( IntPtr ISteamRemoteStorage, string /*const char **/ pchFile );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamRemoteStorage_GetFileTimestamp" )]
					internal static extern long /*int64*/ GetFileTimestamp( IntPtr ISteamRemoteStorage, string /*const char **/ pchFile );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamRemoteStorage_GetSyncPlatforms" )]
					internal static extern RemoteStoragePlatform /*ERemoteStoragePlatform*/ GetSyncPlatforms( IntPtr ISteamRemoteStorage, string /*const char **/ pchFile );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamRemoteStorage_GetFileCount" )]
					internal static extern int /*int32*/ GetFileCount( IntPtr ISteamRemoteStorage );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamRemoteStorage_GetFileNameAndSize" )]
					internal static extern IntPtr GetFileNameAndSize( IntPtr ISteamRemoteStorage, int /*int*/ iFile, IntPtr /*int32 **/ pnFileSizeInBytes );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamRemoteStorage_GetQuota" )]
					internal static extern bool /*bool*/ GetQuota( IntPtr ISteamRemoteStorage, IntPtr /*int32 **/ pnTotalBytes, IntPtr /*int32 **/ puAvailableBytes );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamRemoteStorage_IsCloudEnabledForAccount" )]
					internal static extern bool /*bool*/ IsCloudEnabledForAccount( IntPtr ISteamRemoteStorage );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamRemoteStorage_IsCloudEnabledForApp" )]
					internal static extern bool /*bool*/ IsCloudEnabledForApp( IntPtr ISteamRemoteStorage );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamRemoteStorage_SetCloudEnabledForApp" )]
					internal static extern void /*void*/ SetCloudEnabledForApp( IntPtr ISteamRemoteStorage, [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bEnabled );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamRemoteStorage_UGCDownload" )]
					internal static extern SteamAPICall_t /*(SteamAPICall_t)*/ UGCDownload( IntPtr ISteamRemoteStorage, ulong hContent, uint /*uint32*/ unPriority );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamRemoteStorage_GetUGCDownloadProgress" )]
					internal static extern bool /*bool*/ GetUGCDownloadProgress( IntPtr ISteamRemoteStorage, ulong hContent, out int /*int32 **/ pnBytesDownloaded, out int /*int32 **/ pnBytesExpected );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamRemoteStorage_GetUGCDetails" )]
					internal static extern bool /*bool*/ GetUGCDetails( IntPtr ISteamRemoteStorage, ulong hContent, ref uint pnAppID, System.Text.StringBuilder /*char ***/ ppchName, IntPtr /*int32 **/ pnFileSizeInBytes, out ulong pSteamIDOwner );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamRemoteStorage_UGCRead" )]
					internal static extern int /*int32*/ UGCRead( IntPtr ISteamRemoteStorage, ulong hContent, IntPtr /*void **/ pvData, int /*int32*/ cubDataToRead, uint /*uint32*/ cOffset, UGCReadAction /*EUGCReadAction*/ eAction );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamRemoteStorage_GetCachedUGCCount" )]
					internal static extern int /*int32*/ GetCachedUGCCount( IntPtr ISteamRemoteStorage );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamRemoteStorage_GetCachedUGCHandle" )]
					internal static extern UGCHandle_t /*(UGCHandle_t)*/ GetCachedUGCHandle( IntPtr ISteamRemoteStorage, int /*int32*/ iCachedContent );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamRemoteStorage_PublishWorkshopFile" )]
					internal static extern SteamAPICall_t /*(SteamAPICall_t)*/ PublishWorkshopFile( IntPtr ISteamRemoteStorage, string /*const char **/ pchFile, string /*const char **/ pchPreviewFile, uint nConsumerAppId, string /*const char **/ pchTitle, string /*const char **/ pchDescription, RemoteStoragePublishedFileVisibility /*ERemoteStoragePublishedFileVisibility*/ eVisibility, IntPtr /*struct SteamParamStringArray_t **/ pTags, WorkshopFileType /*EWorkshopFileType*/ eWorkshopFileType );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamRemoteStorage_CreatePublishedFileUpdateRequest" )]
					internal static extern PublishedFileUpdateHandle_t /*(PublishedFileUpdateHandle_t)*/ CreatePublishedFileUpdateRequest( IntPtr ISteamRemoteStorage, ulong unPublishedFileId );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamRemoteStorage_UpdatePublishedFileFile" )]
					internal static extern bool /*bool*/ UpdatePublishedFileFile( IntPtr ISteamRemoteStorage, ulong updateHandle, string /*const char **/ pchFile );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamRemoteStorage_UpdatePublishedFilePreviewFile" )]
					internal static extern bool /*bool*/ UpdatePublishedFilePreviewFile( IntPtr ISteamRemoteStorage, ulong updateHandle, string /*const char **/ pchPreviewFile );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamRemoteStorage_UpdatePublishedFileTitle" )]
					internal static extern bool /*bool*/ UpdatePublishedFileTitle( IntPtr ISteamRemoteStorage, ulong updateHandle, string /*const char **/ pchTitle );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamRemoteStorage_UpdatePublishedFileDescription" )]
					internal static extern bool /*bool*/ UpdatePublishedFileDescription( IntPtr ISteamRemoteStorage, ulong updateHandle, string /*const char **/ pchDescription );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamRemoteStorage_UpdatePublishedFileVisibility" )]
					internal static extern bool /*bool*/ UpdatePublishedFileVisibility( IntPtr ISteamRemoteStorage, ulong updateHandle, RemoteStoragePublishedFileVisibility /*ERemoteStoragePublishedFileVisibility*/ eVisibility );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamRemoteStorage_UpdatePublishedFileTags" )]
					internal static extern bool /*bool*/ UpdatePublishedFileTags( IntPtr ISteamRemoteStorage, ulong updateHandle, IntPtr /*struct SteamParamStringArray_t **/ pTags );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamRemoteStorage_CommitPublishedFileUpdate" )]
					internal static extern SteamAPICall_t /*(SteamAPICall_t)*/ CommitPublishedFileUpdate( IntPtr ISteamRemoteStorage, ulong updateHandle );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamRemoteStorage_GetPublishedFileDetails" )]
					internal static extern SteamAPICall_t /*(SteamAPICall_t)*/ GetPublishedFileDetails( IntPtr ISteamRemoteStorage, ulong unPublishedFileId, uint /*uint32*/ unMaxSecondsOld );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamRemoteStorage_DeletePublishedFile" )]
					internal static extern SteamAPICall_t /*(SteamAPICall_t)*/ DeletePublishedFile( IntPtr ISteamRemoteStorage, ulong unPublishedFileId );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamRemoteStorage_EnumerateUserPublishedFiles" )]
					internal static extern SteamAPICall_t /*(SteamAPICall_t)*/ EnumerateUserPublishedFiles( IntPtr ISteamRemoteStorage, uint /*uint32*/ unStartIndex );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamRemoteStorage_SubscribePublishedFile" )]
					internal static extern SteamAPICall_t /*(SteamAPICall_t)*/ SubscribePublishedFile( IntPtr ISteamRemoteStorage, ulong unPublishedFileId );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamRemoteStorage_EnumerateUserSubscribedFiles" )]
					internal static extern SteamAPICall_t /*(SteamAPICall_t)*/ EnumerateUserSubscribedFiles( IntPtr ISteamRemoteStorage, uint /*uint32*/ unStartIndex );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamRemoteStorage_UnsubscribePublishedFile" )]
					internal static extern SteamAPICall_t /*(SteamAPICall_t)*/ UnsubscribePublishedFile( IntPtr ISteamRemoteStorage, ulong unPublishedFileId );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamRemoteStorage_UpdatePublishedFileSetChangeDescription" )]
					internal static extern bool /*bool*/ UpdatePublishedFileSetChangeDescription( IntPtr ISteamRemoteStorage, ulong updateHandle, string /*const char **/ pchChangeDescription );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamRemoteStorage_GetPublishedItemVoteDetails" )]
					internal static extern SteamAPICall_t /*(SteamAPICall_t)*/ GetPublishedItemVoteDetails( IntPtr ISteamRemoteStorage, ulong unPublishedFileId );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamRemoteStorage_UpdateUserPublishedItemVote" )]
					internal static extern SteamAPICall_t /*(SteamAPICall_t)*/ UpdateUserPublishedItemVote( IntPtr ISteamRemoteStorage, ulong unPublishedFileId, [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bVoteUp );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamRemoteStorage_GetUserPublishedItemVoteDetails" )]
					internal static extern SteamAPICall_t /*(SteamAPICall_t)*/ GetUserPublishedItemVoteDetails( IntPtr ISteamRemoteStorage, ulong unPublishedFileId );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamRemoteStorage_EnumerateUserSharedWorkshopFiles" )]
					internal static extern SteamAPICall_t /*(SteamAPICall_t)*/ EnumerateUserSharedWorkshopFiles( IntPtr ISteamRemoteStorage, ulong steamId, uint /*uint32*/ unStartIndex, IntPtr /*struct SteamParamStringArray_t **/ pRequiredTags, IntPtr /*struct SteamParamStringArray_t **/ pExcludedTags );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamRemoteStorage_PublishVideo" )]
					internal static extern SteamAPICall_t /*(SteamAPICall_t)*/ PublishVideo( IntPtr ISteamRemoteStorage, WorkshopVideoProvider /*EWorkshopVideoProvider*/ eVideoProvider, string /*const char **/ pchVideoAccount, string /*const char **/ pchVideoIdentifier, string /*const char **/ pchPreviewFile, uint nConsumerAppId, string /*const char **/ pchTitle, string /*const char **/ pchDescription, RemoteStoragePublishedFileVisibility /*ERemoteStoragePublishedFileVisibility*/ eVisibility, IntPtr /*struct SteamParamStringArray_t **/ pTags );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamRemoteStorage_SetUserPublishedFileAction" )]
					internal static extern SteamAPICall_t /*(SteamAPICall_t)*/ SetUserPublishedFileAction( IntPtr ISteamRemoteStorage, ulong unPublishedFileId, WorkshopFileAction /*EWorkshopFileAction*/ eAction );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamRemoteStorage_EnumeratePublishedFilesByUserAction" )]
					internal static extern SteamAPICall_t /*(SteamAPICall_t)*/ EnumeratePublishedFilesByUserAction( IntPtr ISteamRemoteStorage, WorkshopFileAction /*EWorkshopFileAction*/ eAction, uint /*uint32*/ unStartIndex );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamRemoteStorage_EnumeratePublishedWorkshopFiles" )]
					internal static extern SteamAPICall_t /*(SteamAPICall_t)*/ EnumeratePublishedWorkshopFiles( IntPtr ISteamRemoteStorage, WorkshopEnumerationType /*EWorkshopEnumerationType*/ eEnumerationType, uint /*uint32*/ unStartIndex, uint /*uint32*/ unCount, uint /*uint32*/ unDays, IntPtr /*struct SteamParamStringArray_t **/ pTags, IntPtr /*struct SteamParamStringArray_t **/ pUserTags );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamRemoteStorage_UGCDownloadToLocation" )]
					internal static extern SteamAPICall_t /*(SteamAPICall_t)*/ UGCDownloadToLocation( IntPtr ISteamRemoteStorage, ulong hContent, string /*const char **/ pchLocation, uint /*uint32*/ unPriority );
				}
				
				internal static unsafe class ISteamUserStats
				{
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamUserStats_RequestCurrentStats" )]
					internal static extern bool /*bool*/ RequestCurrentStats( IntPtr ISteamUserStats );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamUserStats_GetStat" )]
					internal static extern bool /*bool*/ GetStat( IntPtr ISteamUserStats, string /*const char **/ pchName, out int /*int32 **/ pData );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamUserStats_GetStat0" )]
					internal static extern bool /*bool*/ GetStat0( IntPtr ISteamUserStats, string /*const char **/ pchName, out float /*float **/ pData );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamUserStats_SetStat" )]
					internal static extern bool /*bool*/ SetStat( IntPtr ISteamUserStats, string /*const char **/ pchName, int /*int32*/ nData );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamUserStats_SetStat0" )]
					internal static extern bool /*bool*/ SetStat0( IntPtr ISteamUserStats, string /*const char **/ pchName, float /*float*/ fData );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamUserStats_UpdateAvgRateStat" )]
					internal static extern bool /*bool*/ UpdateAvgRateStat( IntPtr ISteamUserStats, string /*const char **/ pchName, float /*float*/ flCountThisSession, double /*double*/ dSessionLength );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamUserStats_GetAchievement" )]
					internal static extern bool /*bool*/ GetAchievement( IntPtr ISteamUserStats, string /*const char **/ pchName, [MarshalAs(UnmanagedType.U1)] ref bool /*bool **/ pbAchieved );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamUserStats_SetAchievement" )]
					internal static extern bool /*bool*/ SetAchievement( IntPtr ISteamUserStats, string /*const char **/ pchName );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamUserStats_ClearAchievement" )]
					internal static extern bool /*bool*/ ClearAchievement( IntPtr ISteamUserStats, string /*const char **/ pchName );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamUserStats_GetAchievementAndUnlockTime" )]
					internal static extern bool /*bool*/ GetAchievementAndUnlockTime( IntPtr ISteamUserStats, string /*const char **/ pchName, [MarshalAs(UnmanagedType.U1)] ref bool /*bool **/ pbAchieved, out uint /*uint32 **/ punUnlockTime );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamUserStats_StoreStats" )]
					internal static extern bool /*bool*/ StoreStats( IntPtr ISteamUserStats );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamUserStats_GetAchievementIcon" )]
					internal static extern int /*int*/ GetAchievementIcon( IntPtr ISteamUserStats, string /*const char **/ pchName );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamUserStats_GetAchievementDisplayAttribute" )]
					internal static extern IntPtr GetAchievementDisplayAttribute( IntPtr ISteamUserStats, string /*const char **/ pchName, string /*const char **/ pchKey );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamUserStats_IndicateAchievementProgress" )]
					internal static extern bool /*bool*/ IndicateAchievementProgress( IntPtr ISteamUserStats, string /*const char **/ pchName, uint /*uint32*/ nCurProgress, uint /*uint32*/ nMaxProgress );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamUserStats_GetNumAchievements" )]
					internal static extern uint /*uint32*/ GetNumAchievements( IntPtr ISteamUserStats );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamUserStats_GetAchievementName" )]
					internal static extern IntPtr GetAchievementName( IntPtr ISteamUserStats, uint /*uint32*/ iAchievement );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamUserStats_RequestUserStats" )]
					internal static extern SteamAPICall_t /*(SteamAPICall_t)*/ RequestUserStats( IntPtr ISteamUserStats, ulong steamIDUser );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamUserStats_GetUserStat" )]
					internal static extern bool /*bool*/ GetUserStat( IntPtr ISteamUserStats, ulong steamIDUser, string /*const char **/ pchName, out int /*int32 **/ pData );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamUserStats_GetUserStat0" )]
					internal static extern bool /*bool*/ GetUserStat0( IntPtr ISteamUserStats, ulong steamIDUser, string /*const char **/ pchName, out float /*float **/ pData );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamUserStats_GetUserAchievement" )]
					internal static extern bool /*bool*/ GetUserAchievement( IntPtr ISteamUserStats, ulong steamIDUser, string /*const char **/ pchName, [MarshalAs(UnmanagedType.U1)] ref bool /*bool **/ pbAchieved );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamUserStats_GetUserAchievementAndUnlockTime" )]
					internal static extern bool /*bool*/ GetUserAchievementAndUnlockTime( IntPtr ISteamUserStats, ulong steamIDUser, string /*const char **/ pchName, [MarshalAs(UnmanagedType.U1)] ref bool /*bool **/ pbAchieved, out uint /*uint32 **/ punUnlockTime );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamUserStats_ResetAllStats" )]
					internal static extern bool /*bool*/ ResetAllStats( IntPtr ISteamUserStats, [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bAchievementsToo );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamUserStats_FindOrCreateLeaderboard" )]
					internal static extern SteamAPICall_t /*(SteamAPICall_t)*/ FindOrCreateLeaderboard( IntPtr ISteamUserStats, string /*const char **/ pchLeaderboardName, LeaderboardSortMethod /*ELeaderboardSortMethod*/ eLeaderboardSortMethod, LeaderboardDisplayType /*ELeaderboardDisplayType*/ eLeaderboardDisplayType );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamUserStats_FindLeaderboard" )]
					internal static extern SteamAPICall_t /*(SteamAPICall_t)*/ FindLeaderboard( IntPtr ISteamUserStats, string /*const char **/ pchLeaderboardName );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamUserStats_GetLeaderboardName" )]
					internal static extern IntPtr GetLeaderboardName( IntPtr ISteamUserStats, ulong hSteamLeaderboard );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamUserStats_GetLeaderboardEntryCount" )]
					internal static extern int /*int*/ GetLeaderboardEntryCount( IntPtr ISteamUserStats, ulong hSteamLeaderboard );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamUserStats_GetLeaderboardSortMethod" )]
					internal static extern LeaderboardSortMethod /*ELeaderboardSortMethod*/ GetLeaderboardSortMethod( IntPtr ISteamUserStats, ulong hSteamLeaderboard );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamUserStats_GetLeaderboardDisplayType" )]
					internal static extern LeaderboardDisplayType /*ELeaderboardDisplayType*/ GetLeaderboardDisplayType( IntPtr ISteamUserStats, ulong hSteamLeaderboard );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamUserStats_DownloadLeaderboardEntries" )]
					internal static extern SteamAPICall_t /*(SteamAPICall_t)*/ DownloadLeaderboardEntries( IntPtr ISteamUserStats, ulong hSteamLeaderboard, LeaderboardDataRequest /*ELeaderboardDataRequest*/ eLeaderboardDataRequest, int /*int*/ nRangeStart, int /*int*/ nRangeEnd );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamUserStats_DownloadLeaderboardEntriesForUsers" )]
					internal static extern SteamAPICall_t /*(SteamAPICall_t)*/ DownloadLeaderboardEntriesForUsers( IntPtr ISteamUserStats, ulong hSteamLeaderboard, IntPtr /*class CSteamID **/ prgUsers, int /*int*/ cUsers );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamUserStats_GetDownloadedLeaderboardEntry" )]
					internal static extern bool /*bool*/ GetDownloadedLeaderboardEntry( IntPtr ISteamUserStats, ulong hSteamLeaderboardEntries, int /*int*/ index, ref LeaderboardEntry_t.PackSmall /*struct LeaderboardEntry_t **/ pLeaderboardEntry, IntPtr /*int32 **/ pDetails, int /*int*/ cDetailsMax );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamUserStats_UploadLeaderboardScore" )]
					internal static extern SteamAPICall_t /*(SteamAPICall_t)*/ UploadLeaderboardScore( IntPtr ISteamUserStats, ulong hSteamLeaderboard, LeaderboardUploadScoreMethod /*ELeaderboardUploadScoreMethod*/ eLeaderboardUploadScoreMethod, int /*int32*/ nScore, IntPtr /*const int32 **/ pScoreDetails, int /*int*/ cScoreDetailsCount );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamUserStats_AttachLeaderboardUGC" )]
					internal static extern SteamAPICall_t /*(SteamAPICall_t)*/ AttachLeaderboardUGC( IntPtr ISteamUserStats, ulong hSteamLeaderboard, ulong hUGC );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamUserStats_GetNumberOfCurrentPlayers" )]
					internal static extern SteamAPICall_t /*(SteamAPICall_t)*/ GetNumberOfCurrentPlayers( IntPtr ISteamUserStats );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamUserStats_RequestGlobalAchievementPercentages" )]
					internal static extern SteamAPICall_t /*(SteamAPICall_t)*/ RequestGlobalAchievementPercentages( IntPtr ISteamUserStats );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamUserStats_GetMostAchievedAchievementInfo" )]
					internal static extern int /*int*/ GetMostAchievedAchievementInfo( IntPtr ISteamUserStats, System.Text.StringBuilder /*char **/ pchName, uint /*uint32*/ unNameBufLen, out float /*float **/ pflPercent, [MarshalAs(UnmanagedType.U1)] ref bool /*bool **/ pbAchieved );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamUserStats_GetNextMostAchievedAchievementInfo" )]
					internal static extern int /*int*/ GetNextMostAchievedAchievementInfo( IntPtr ISteamUserStats, int /*int*/ iIteratorPrevious, System.Text.StringBuilder /*char **/ pchName, uint /*uint32*/ unNameBufLen, out float /*float **/ pflPercent, [MarshalAs(UnmanagedType.U1)] ref bool /*bool **/ pbAchieved );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamUserStats_GetAchievementAchievedPercent" )]
					internal static extern bool /*bool*/ GetAchievementAchievedPercent( IntPtr ISteamUserStats, string /*const char **/ pchName, out float /*float **/ pflPercent );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamUserStats_RequestGlobalStats" )]
					internal static extern SteamAPICall_t /*(SteamAPICall_t)*/ RequestGlobalStats( IntPtr ISteamUserStats, int /*int*/ nHistoryDays );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamUserStats_GetGlobalStat" )]
					internal static extern bool /*bool*/ GetGlobalStat( IntPtr ISteamUserStats, string /*const char **/ pchStatName, out long /*int64 **/ pData );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamUserStats_GetGlobalStat0" )]
					internal static extern bool /*bool*/ GetGlobalStat0( IntPtr ISteamUserStats, string /*const char **/ pchStatName, out double /*double **/ pData );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamUserStats_GetGlobalStatHistory" )]
					internal static extern int /*int32*/ GetGlobalStatHistory( IntPtr ISteamUserStats, string /*const char **/ pchStatName, out long /*int64 **/ pData, uint /*uint32*/ cubData );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamUserStats_GetGlobalStatHistory0" )]
					internal static extern int /*int32*/ GetGlobalStatHistory0( IntPtr ISteamUserStats, string /*const char **/ pchStatName, out double /*double **/ pData, uint /*uint32*/ cubData );
				}
				
				internal static unsafe class ISteamApps
				{
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamApps_BIsSubscribed" )]
					internal static extern bool /*bool*/ BIsSubscribed( IntPtr ISteamApps );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamApps_BIsLowViolence" )]
					internal static extern bool /*bool*/ BIsLowViolence( IntPtr ISteamApps );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamApps_BIsCybercafe" )]
					internal static extern bool /*bool*/ BIsCybercafe( IntPtr ISteamApps );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamApps_BIsVACBanned" )]
					internal static extern bool /*bool*/ BIsVACBanned( IntPtr ISteamApps );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamApps_GetCurrentGameLanguage" )]
					internal static extern IntPtr GetCurrentGameLanguage( IntPtr ISteamApps );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamApps_GetAvailableGameLanguages" )]
					internal static extern IntPtr GetAvailableGameLanguages( IntPtr ISteamApps );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamApps_BIsSubscribedApp" )]
					internal static extern bool /*bool*/ BIsSubscribedApp( IntPtr ISteamApps, uint appID );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamApps_BIsDlcInstalled" )]
					internal static extern bool /*bool*/ BIsDlcInstalled( IntPtr ISteamApps, uint appID );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamApps_GetEarliestPurchaseUnixTime" )]
					internal static extern uint /*uint32*/ GetEarliestPurchaseUnixTime( IntPtr ISteamApps, uint nAppID );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamApps_BIsSubscribedFromFreeWeekend" )]
					internal static extern bool /*bool*/ BIsSubscribedFromFreeWeekend( IntPtr ISteamApps );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamApps_GetDLCCount" )]
					internal static extern int /*int*/ GetDLCCount( IntPtr ISteamApps );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamApps_BGetDLCDataByIndex" )]
					internal static extern bool /*bool*/ BGetDLCDataByIndex( IntPtr ISteamApps, int /*int*/ iDLC, ref uint pAppID, [MarshalAs(UnmanagedType.U1)] ref bool /*bool **/ pbAvailable, System.Text.StringBuilder /*char **/ pchName, int /*int*/ cchNameBufferSize );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamApps_InstallDLC" )]
					internal static extern void /*void*/ InstallDLC( IntPtr ISteamApps, uint nAppID );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamApps_UninstallDLC" )]
					internal static extern void /*void*/ UninstallDLC( IntPtr ISteamApps, uint nAppID );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamApps_RequestAppProofOfPurchaseKey" )]
					internal static extern void /*void*/ RequestAppProofOfPurchaseKey( IntPtr ISteamApps, uint nAppID );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamApps_GetCurrentBetaName" )]
					internal static extern bool /*bool*/ GetCurrentBetaName( IntPtr ISteamApps, System.Text.StringBuilder /*char **/ pchName, int /*int*/ cchNameBufferSize );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamApps_MarkContentCorrupt" )]
					internal static extern bool /*bool*/ MarkContentCorrupt( IntPtr ISteamApps, [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bMissingFilesOnly );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamApps_GetInstalledDepots" )]
					internal static extern uint /*uint32*/ GetInstalledDepots( IntPtr ISteamApps, uint appID, IntPtr /*DepotId_t **/ pvecDepots, uint /*uint32*/ cMaxDepots );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamApps_GetAppInstallDir" )]
					internal static extern uint /*uint32*/ GetAppInstallDir( IntPtr ISteamApps, uint appID, System.Text.StringBuilder /*char **/ pchFolder, uint /*uint32*/ cchFolderBufferSize );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamApps_BIsAppInstalled" )]
					internal static extern bool /*bool*/ BIsAppInstalled( IntPtr ISteamApps, uint appID );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamApps_GetAppOwner" )]
					internal static extern CSteamID /*(class CSteamID)*/ GetAppOwner( IntPtr ISteamApps );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamApps_GetLaunchQueryParam" )]
					internal static extern IntPtr GetLaunchQueryParam( IntPtr ISteamApps, string /*const char **/ pchKey );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamApps_GetDlcDownloadProgress" )]
					internal static extern bool /*bool*/ GetDlcDownloadProgress( IntPtr ISteamApps, uint nAppID, out ulong /*uint64 **/ punBytesDownloaded, out ulong /*uint64 **/ punBytesTotal );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamApps_GetAppBuildId" )]
					internal static extern int /*int*/ GetAppBuildId( IntPtr ISteamApps );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamApps_RequestAllProofOfPurchaseKeys" )]
					internal static extern void /*void*/ RequestAllProofOfPurchaseKeys( IntPtr ISteamApps );
				}
				
				internal static unsafe class ISteamNetworking
				{
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamNetworking_SendP2PPacket" )]
					internal static extern bool /*bool*/ SendP2PPacket( IntPtr ISteamNetworking, ulong steamIDRemote, IntPtr /*const void **/ pubData, uint /*uint32*/ cubData, P2PSend /*EP2PSend*/ eP2PSendType, int /*int*/ nChannel );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamNetworking_IsP2PPacketAvailable" )]
					internal static extern bool /*bool*/ IsP2PPacketAvailable( IntPtr ISteamNetworking, out uint /*uint32 **/ pcubMsgSize, int /*int*/ nChannel );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamNetworking_ReadP2PPacket" )]
					internal static extern bool /*bool*/ ReadP2PPacket( IntPtr ISteamNetworking, IntPtr /*void **/ pubDest, uint /*uint32*/ cubDest, out uint /*uint32 **/ pcubMsgSize, out ulong psteamIDRemote, int /*int*/ nChannel );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamNetworking_AcceptP2PSessionWithUser" )]
					internal static extern bool /*bool*/ AcceptP2PSessionWithUser( IntPtr ISteamNetworking, ulong steamIDRemote );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamNetworking_CloseP2PSessionWithUser" )]
					internal static extern bool /*bool*/ CloseP2PSessionWithUser( IntPtr ISteamNetworking, ulong steamIDRemote );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamNetworking_CloseP2PChannelWithUser" )]
					internal static extern bool /*bool*/ CloseP2PChannelWithUser( IntPtr ISteamNetworking, ulong steamIDRemote, int /*int*/ nChannel );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamNetworking_GetP2PSessionState" )]
					internal static extern bool /*bool*/ GetP2PSessionState( IntPtr ISteamNetworking, ulong steamIDRemote, ref P2PSessionState_t.PackSmall /*struct P2PSessionState_t **/ pConnectionState );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamNetworking_AllowP2PPacketRelay" )]
					internal static extern bool /*bool*/ AllowP2PPacketRelay( IntPtr ISteamNetworking, [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bAllow );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamNetworking_CreateListenSocket" )]
					internal static extern SNetListenSocket_t /*(SNetListenSocket_t)*/ CreateListenSocket( IntPtr ISteamNetworking, int /*int*/ nVirtualP2PPort, uint /*uint32*/ nIP, ushort /*uint16*/ nPort, [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bAllowUseOfPacketRelay );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamNetworking_CreateP2PConnectionSocket" )]
					internal static extern SNetSocket_t /*(SNetSocket_t)*/ CreateP2PConnectionSocket( IntPtr ISteamNetworking, ulong steamIDTarget, int /*int*/ nVirtualPort, int /*int*/ nTimeoutSec, [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bAllowUseOfPacketRelay );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamNetworking_CreateConnectionSocket" )]
					internal static extern SNetSocket_t /*(SNetSocket_t)*/ CreateConnectionSocket( IntPtr ISteamNetworking, uint /*uint32*/ nIP, ushort /*uint16*/ nPort, int /*int*/ nTimeoutSec );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamNetworking_DestroySocket" )]
					internal static extern bool /*bool*/ DestroySocket( IntPtr ISteamNetworking, uint hSocket, [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bNotifyRemoteEnd );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamNetworking_DestroyListenSocket" )]
					internal static extern bool /*bool*/ DestroyListenSocket( IntPtr ISteamNetworking, uint hSocket, [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bNotifyRemoteEnd );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamNetworking_SendDataOnSocket" )]
					internal static extern bool /*bool*/ SendDataOnSocket( IntPtr ISteamNetworking, uint hSocket, IntPtr /*void **/ pubData, uint /*uint32*/ cubData, [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bReliable );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamNetworking_IsDataAvailableOnSocket" )]
					internal static extern bool /*bool*/ IsDataAvailableOnSocket( IntPtr ISteamNetworking, uint hSocket, out uint /*uint32 **/ pcubMsgSize );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamNetworking_RetrieveDataFromSocket" )]
					internal static extern bool /*bool*/ RetrieveDataFromSocket( IntPtr ISteamNetworking, uint hSocket, IntPtr /*void **/ pubDest, uint /*uint32*/ cubDest, out uint /*uint32 **/ pcubMsgSize );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamNetworking_IsDataAvailable" )]
					internal static extern bool /*bool*/ IsDataAvailable( IntPtr ISteamNetworking, uint hListenSocket, out uint /*uint32 **/ pcubMsgSize, ref uint phSocket );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamNetworking_RetrieveData" )]
					internal static extern bool /*bool*/ RetrieveData( IntPtr ISteamNetworking, uint hListenSocket, IntPtr /*void **/ pubDest, uint /*uint32*/ cubDest, out uint /*uint32 **/ pcubMsgSize, ref uint phSocket );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamNetworking_GetSocketInfo" )]
					internal static extern bool /*bool*/ GetSocketInfo( IntPtr ISteamNetworking, uint hSocket, out ulong pSteamIDRemote, IntPtr /*int **/ peSocketStatus, out uint /*uint32 **/ punIPRemote, out ushort /*uint16 **/ punPortRemote );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamNetworking_GetListenSocketInfo" )]
					internal static extern bool /*bool*/ GetListenSocketInfo( IntPtr ISteamNetworking, uint hListenSocket, out uint /*uint32 **/ pnIP, out ushort /*uint16 **/ pnPort );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamNetworking_GetSocketConnectionType" )]
					internal static extern SNetSocketConnectionType /*ESNetSocketConnectionType*/ GetSocketConnectionType( IntPtr ISteamNetworking, uint hSocket );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamNetworking_GetMaxPacketSize" )]
					internal static extern int /*int*/ GetMaxPacketSize( IntPtr ISteamNetworking, uint hSocket );
				}
				
				internal static unsafe class ISteamScreenshots
				{
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamScreenshots_WriteScreenshot" )]
					internal static extern ScreenshotHandle /*(ScreenshotHandle)*/ WriteScreenshot( IntPtr ISteamScreenshots, IntPtr /*void **/ pubRGB, uint /*uint32*/ cubRGB, int /*int*/ nWidth, int /*int*/ nHeight );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamScreenshots_AddScreenshotToLibrary" )]
					internal static extern ScreenshotHandle /*(ScreenshotHandle)*/ AddScreenshotToLibrary( IntPtr ISteamScreenshots, string /*const char **/ pchFilename, string /*const char **/ pchThumbnailFilename, int /*int*/ nWidth, int /*int*/ nHeight );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamScreenshots_TriggerScreenshot" )]
					internal static extern void /*void*/ TriggerScreenshot( IntPtr ISteamScreenshots );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamScreenshots_HookScreenshots" )]
					internal static extern void /*void*/ HookScreenshots( IntPtr ISteamScreenshots, [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bHook );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamScreenshots_SetLocation" )]
					internal static extern bool /*bool*/ SetLocation( IntPtr ISteamScreenshots, uint hScreenshot, string /*const char **/ pchLocation );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamScreenshots_TagUser" )]
					internal static extern bool /*bool*/ TagUser( IntPtr ISteamScreenshots, uint hScreenshot, ulong steamID );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamScreenshots_TagPublishedFile" )]
					internal static extern bool /*bool*/ TagPublishedFile( IntPtr ISteamScreenshots, uint hScreenshot, ulong unPublishedFileID );
				}
				
				internal static unsafe class ISteamMusic
				{
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamMusic_BIsEnabled" )]
					internal static extern bool /*bool*/ BIsEnabled( IntPtr ISteamMusic );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamMusic_BIsPlaying" )]
					internal static extern bool /*bool*/ BIsPlaying( IntPtr ISteamMusic );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamMusic_GetPlaybackStatus" )]
					internal static extern AudioPlayback_Status /*AudioPlayback_Status*/ GetPlaybackStatus( IntPtr ISteamMusic );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamMusic_Play" )]
					internal static extern void /*void*/ Play( IntPtr ISteamMusic );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamMusic_Pause" )]
					internal static extern void /*void*/ Pause( IntPtr ISteamMusic );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamMusic_PlayPrevious" )]
					internal static extern void /*void*/ PlayPrevious( IntPtr ISteamMusic );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamMusic_PlayNext" )]
					internal static extern void /*void*/ PlayNext( IntPtr ISteamMusic );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamMusic_SetVolume" )]
					internal static extern void /*void*/ SetVolume( IntPtr ISteamMusic, float /*float*/ flVolume );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamMusic_GetVolume" )]
					internal static extern float /*float*/ GetVolume( IntPtr ISteamMusic );
				}
				
				internal static unsafe class ISteamMusicRemote
				{
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamMusicRemote_RegisterSteamMusicRemote" )]
					internal static extern bool /*bool*/ RegisterSteamMusicRemote( IntPtr ISteamMusicRemote, string /*const char **/ pchName );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamMusicRemote_DeregisterSteamMusicRemote" )]
					internal static extern bool /*bool*/ DeregisterSteamMusicRemote( IntPtr ISteamMusicRemote );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamMusicRemote_BIsCurrentMusicRemote" )]
					internal static extern bool /*bool*/ BIsCurrentMusicRemote( IntPtr ISteamMusicRemote );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamMusicRemote_BActivationSuccess" )]
					internal static extern bool /*bool*/ BActivationSuccess( IntPtr ISteamMusicRemote, [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bValue );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamMusicRemote_SetDisplayName" )]
					internal static extern bool /*bool*/ SetDisplayName( IntPtr ISteamMusicRemote, string /*const char **/ pchDisplayName );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamMusicRemote_SetPNGIcon_64x64" )]
					internal static extern bool /*bool*/ SetPNGIcon_64x64( IntPtr ISteamMusicRemote, IntPtr /*void **/ pvBuffer, uint /*uint32*/ cbBufferLength );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamMusicRemote_EnablePlayPrevious" )]
					internal static extern bool /*bool*/ EnablePlayPrevious( IntPtr ISteamMusicRemote, [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bValue );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamMusicRemote_EnablePlayNext" )]
					internal static extern bool /*bool*/ EnablePlayNext( IntPtr ISteamMusicRemote, [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bValue );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamMusicRemote_EnableShuffled" )]
					internal static extern bool /*bool*/ EnableShuffled( IntPtr ISteamMusicRemote, [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bValue );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamMusicRemote_EnableLooped" )]
					internal static extern bool /*bool*/ EnableLooped( IntPtr ISteamMusicRemote, [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bValue );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamMusicRemote_EnableQueue" )]
					internal static extern bool /*bool*/ EnableQueue( IntPtr ISteamMusicRemote, [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bValue );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamMusicRemote_EnablePlaylists" )]
					internal static extern bool /*bool*/ EnablePlaylists( IntPtr ISteamMusicRemote, [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bValue );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamMusicRemote_UpdatePlaybackStatus" )]
					internal static extern bool /*bool*/ UpdatePlaybackStatus( IntPtr ISteamMusicRemote, AudioPlayback_Status /*AudioPlayback_Status*/ nStatus );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamMusicRemote_UpdateShuffled" )]
					internal static extern bool /*bool*/ UpdateShuffled( IntPtr ISteamMusicRemote, [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bValue );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamMusicRemote_UpdateLooped" )]
					internal static extern bool /*bool*/ UpdateLooped( IntPtr ISteamMusicRemote, [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bValue );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamMusicRemote_UpdateVolume" )]
					internal static extern bool /*bool*/ UpdateVolume( IntPtr ISteamMusicRemote, float /*float*/ flValue );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamMusicRemote_CurrentEntryWillChange" )]
					internal static extern bool /*bool*/ CurrentEntryWillChange( IntPtr ISteamMusicRemote );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamMusicRemote_CurrentEntryIsAvailable" )]
					internal static extern bool /*bool*/ CurrentEntryIsAvailable( IntPtr ISteamMusicRemote, [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bAvailable );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamMusicRemote_UpdateCurrentEntryText" )]
					internal static extern bool /*bool*/ UpdateCurrentEntryText( IntPtr ISteamMusicRemote, string /*const char **/ pchText );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamMusicRemote_UpdateCurrentEntryElapsedSeconds" )]
					internal static extern bool /*bool*/ UpdateCurrentEntryElapsedSeconds( IntPtr ISteamMusicRemote, int /*int*/ nValue );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamMusicRemote_UpdateCurrentEntryCoverArt" )]
					internal static extern bool /*bool*/ UpdateCurrentEntryCoverArt( IntPtr ISteamMusicRemote, IntPtr /*void **/ pvBuffer, uint /*uint32*/ cbBufferLength );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamMusicRemote_CurrentEntryDidChange" )]
					internal static extern bool /*bool*/ CurrentEntryDidChange( IntPtr ISteamMusicRemote );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamMusicRemote_QueueWillChange" )]
					internal static extern bool /*bool*/ QueueWillChange( IntPtr ISteamMusicRemote );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamMusicRemote_ResetQueueEntries" )]
					internal static extern bool /*bool*/ ResetQueueEntries( IntPtr ISteamMusicRemote );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamMusicRemote_SetQueueEntry" )]
					internal static extern bool /*bool*/ SetQueueEntry( IntPtr ISteamMusicRemote, int /*int*/ nID, int /*int*/ nPosition, string /*const char **/ pchEntryText );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamMusicRemote_SetCurrentQueueEntry" )]
					internal static extern bool /*bool*/ SetCurrentQueueEntry( IntPtr ISteamMusicRemote, int /*int*/ nID );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamMusicRemote_QueueDidChange" )]
					internal static extern bool /*bool*/ QueueDidChange( IntPtr ISteamMusicRemote );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamMusicRemote_PlaylistWillChange" )]
					internal static extern bool /*bool*/ PlaylistWillChange( IntPtr ISteamMusicRemote );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamMusicRemote_ResetPlaylistEntries" )]
					internal static extern bool /*bool*/ ResetPlaylistEntries( IntPtr ISteamMusicRemote );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamMusicRemote_SetPlaylistEntry" )]
					internal static extern bool /*bool*/ SetPlaylistEntry( IntPtr ISteamMusicRemote, int /*int*/ nID, int /*int*/ nPosition, string /*const char **/ pchEntryText );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamMusicRemote_SetCurrentPlaylistEntry" )]
					internal static extern bool /*bool*/ SetCurrentPlaylistEntry( IntPtr ISteamMusicRemote, int /*int*/ nID );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamMusicRemote_PlaylistDidChange" )]
					internal static extern bool /*bool*/ PlaylistDidChange( IntPtr ISteamMusicRemote );
				}
				
				internal static unsafe class ISteamHTTP
				{
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamHTTP_CreateHTTPRequest" )]
					internal static extern HTTPRequestHandle /*(HTTPRequestHandle)*/ CreateHTTPRequest( IntPtr ISteamHTTP, HTTPMethod /*EHTTPMethod*/ eHTTPRequestMethod, string /*const char **/ pchAbsoluteURL );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamHTTP_SetHTTPRequestContextValue" )]
					internal static extern bool /*bool*/ SetHTTPRequestContextValue( IntPtr ISteamHTTP, uint hRequest, ulong /*uint64*/ ulContextValue );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamHTTP_SetHTTPRequestNetworkActivityTimeout" )]
					internal static extern bool /*bool*/ SetHTTPRequestNetworkActivityTimeout( IntPtr ISteamHTTP, uint hRequest, uint /*uint32*/ unTimeoutSeconds );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamHTTP_SetHTTPRequestHeaderValue" )]
					internal static extern bool /*bool*/ SetHTTPRequestHeaderValue( IntPtr ISteamHTTP, uint hRequest, string /*const char **/ pchHeaderName, string /*const char **/ pchHeaderValue );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamHTTP_SetHTTPRequestGetOrPostParameter" )]
					internal static extern bool /*bool*/ SetHTTPRequestGetOrPostParameter( IntPtr ISteamHTTP, uint hRequest, string /*const char **/ pchParamName, string /*const char **/ pchParamValue );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamHTTP_SendHTTPRequest" )]
					internal static extern bool /*bool*/ SendHTTPRequest( IntPtr ISteamHTTP, uint hRequest, ref ulong pCallHandle );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamHTTP_SendHTTPRequestAndStreamResponse" )]
					internal static extern bool /*bool*/ SendHTTPRequestAndStreamResponse( IntPtr ISteamHTTP, uint hRequest, ref ulong pCallHandle );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamHTTP_DeferHTTPRequest" )]
					internal static extern bool /*bool*/ DeferHTTPRequest( IntPtr ISteamHTTP, uint hRequest );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamHTTP_PrioritizeHTTPRequest" )]
					internal static extern bool /*bool*/ PrioritizeHTTPRequest( IntPtr ISteamHTTP, uint hRequest );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamHTTP_GetHTTPResponseHeaderSize" )]
					internal static extern bool /*bool*/ GetHTTPResponseHeaderSize( IntPtr ISteamHTTP, uint hRequest, string /*const char **/ pchHeaderName, out uint /*uint32 **/ unResponseHeaderSize );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamHTTP_GetHTTPResponseHeaderValue" )]
					internal static extern bool /*bool*/ GetHTTPResponseHeaderValue( IntPtr ISteamHTTP, uint hRequest, string /*const char **/ pchHeaderName, out byte /*uint8 **/ pHeaderValueBuffer, uint /*uint32*/ unBufferSize );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamHTTP_GetHTTPResponseBodySize" )]
					internal static extern bool /*bool*/ GetHTTPResponseBodySize( IntPtr ISteamHTTP, uint hRequest, out uint /*uint32 **/ unBodySize );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamHTTP_GetHTTPResponseBodyData" )]
					internal static extern bool /*bool*/ GetHTTPResponseBodyData( IntPtr ISteamHTTP, uint hRequest, out byte /*uint8 **/ pBodyDataBuffer, uint /*uint32*/ unBufferSize );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamHTTP_GetHTTPStreamingResponseBodyData" )]
					internal static extern bool /*bool*/ GetHTTPStreamingResponseBodyData( IntPtr ISteamHTTP, uint hRequest, uint /*uint32*/ cOffset, out byte /*uint8 **/ pBodyDataBuffer, uint /*uint32*/ unBufferSize );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamHTTP_ReleaseHTTPRequest" )]
					internal static extern bool /*bool*/ ReleaseHTTPRequest( IntPtr ISteamHTTP, uint hRequest );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamHTTP_GetHTTPDownloadProgressPct" )]
					internal static extern bool /*bool*/ GetHTTPDownloadProgressPct( IntPtr ISteamHTTP, uint hRequest, out float /*float **/ pflPercentOut );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamHTTP_SetHTTPRequestRawPostBody" )]
					internal static extern bool /*bool*/ SetHTTPRequestRawPostBody( IntPtr ISteamHTTP, uint hRequest, string /*const char **/ pchContentType, out byte /*uint8 **/ pubBody, uint /*uint32*/ unBodyLen );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamHTTP_CreateCookieContainer" )]
					internal static extern HTTPCookieContainerHandle /*(HTTPCookieContainerHandle)*/ CreateCookieContainer( IntPtr ISteamHTTP, [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bAllowResponsesToModify );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamHTTP_ReleaseCookieContainer" )]
					internal static extern bool /*bool*/ ReleaseCookieContainer( IntPtr ISteamHTTP, uint hCookieContainer );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamHTTP_SetCookie" )]
					internal static extern bool /*bool*/ SetCookie( IntPtr ISteamHTTP, uint hCookieContainer, string /*const char **/ pchHost, string /*const char **/ pchUrl, string /*const char **/ pchCookie );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamHTTP_SetHTTPRequestCookieContainer" )]
					internal static extern bool /*bool*/ SetHTTPRequestCookieContainer( IntPtr ISteamHTTP, uint hRequest, uint hCookieContainer );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamHTTP_SetHTTPRequestUserAgentInfo" )]
					internal static extern bool /*bool*/ SetHTTPRequestUserAgentInfo( IntPtr ISteamHTTP, uint hRequest, string /*const char **/ pchUserAgentInfo );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamHTTP_SetHTTPRequestRequiresVerifiedCertificate" )]
					internal static extern bool /*bool*/ SetHTTPRequestRequiresVerifiedCertificate( IntPtr ISteamHTTP, uint hRequest, [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bRequireVerifiedCertificate );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamHTTP_SetHTTPRequestAbsoluteTimeoutMS" )]
					internal static extern bool /*bool*/ SetHTTPRequestAbsoluteTimeoutMS( IntPtr ISteamHTTP, uint hRequest, uint /*uint32*/ unMilliseconds );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamHTTP_GetHTTPRequestWasTimedOut" )]
					internal static extern bool /*bool*/ GetHTTPRequestWasTimedOut( IntPtr ISteamHTTP, uint hRequest, [MarshalAs(UnmanagedType.U1)] ref bool /*bool **/ pbWasTimedOut );
				}
				
				internal static unsafe class ISteamUnifiedMessages
				{
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamUnifiedMessages_SendMethod" )]
					internal static extern ClientUnifiedMessageHandle /*(ClientUnifiedMessageHandle)*/ SendMethod( IntPtr ISteamUnifiedMessages, string /*const char **/ pchServiceMethod, IntPtr /*const void **/ pRequestBuffer, uint /*uint32*/ unRequestBufferSize, ulong /*uint64*/ unContext );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamUnifiedMessages_GetMethodResponseInfo" )]
					internal static extern bool /*bool*/ GetMethodResponseInfo( IntPtr ISteamUnifiedMessages, ulong hHandle, out uint /*uint32 **/ punResponseSize, out Result /*EResult **/ peResult );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamUnifiedMessages_GetMethodResponseData" )]
					internal static extern bool /*bool*/ GetMethodResponseData( IntPtr ISteamUnifiedMessages, ulong hHandle, IntPtr /*void **/ pResponseBuffer, uint /*uint32*/ unResponseBufferSize, [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bAutoRelease );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamUnifiedMessages_ReleaseMethod" )]
					internal static extern bool /*bool*/ ReleaseMethod( IntPtr ISteamUnifiedMessages, ulong hHandle );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamUnifiedMessages_SendNotification" )]
					internal static extern bool /*bool*/ SendNotification( IntPtr ISteamUnifiedMessages, string /*const char **/ pchServiceNotification, IntPtr /*const void **/ pNotificationBuffer, uint /*uint32*/ unNotificationBufferSize );
				}
				
				internal static unsafe class ISteamController
				{
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamController_Init" )]
					internal static extern bool /*bool*/ Init( IntPtr ISteamController );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamController_Shutdown" )]
					internal static extern bool /*bool*/ Shutdown( IntPtr ISteamController );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamController_RunFrame" )]
					internal static extern void /*void*/ RunFrame( IntPtr ISteamController );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamController_GetConnectedControllers" )]
					internal static extern int /*int*/ GetConnectedControllers( IntPtr ISteamController, IntPtr /*ControllerHandle_t **/ handlesOut );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamController_ShowBindingPanel" )]
					internal static extern bool /*bool*/ ShowBindingPanel( IntPtr ISteamController, ulong controllerHandle );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamController_GetActionSetHandle" )]
					internal static extern ControllerActionSetHandle_t /*(ControllerActionSetHandle_t)*/ GetActionSetHandle( IntPtr ISteamController, string /*const char **/ pszActionSetName );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamController_ActivateActionSet" )]
					internal static extern void /*void*/ ActivateActionSet( IntPtr ISteamController, ulong controllerHandle, ulong actionSetHandle );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamController_GetCurrentActionSet" )]
					internal static extern ControllerActionSetHandle_t /*(ControllerActionSetHandle_t)*/ GetCurrentActionSet( IntPtr ISteamController, ulong controllerHandle );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamController_GetDigitalActionHandle" )]
					internal static extern ControllerDigitalActionHandle_t /*(ControllerDigitalActionHandle_t)*/ GetDigitalActionHandle( IntPtr ISteamController, string /*const char **/ pszActionName );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamController_GetDigitalActionData" )]
					internal static extern ControllerDigitalActionData_t /*struct ControllerDigitalActionData_t*/ GetDigitalActionData( IntPtr ISteamController, ulong controllerHandle, ulong digitalActionHandle );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamController_GetDigitalActionOrigins" )]
					internal static extern int /*int*/ GetDigitalActionOrigins( IntPtr ISteamController, ulong controllerHandle, ulong actionSetHandle, ulong digitalActionHandle, out ControllerActionOrigin /*EControllerActionOrigin **/ originsOut );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamController_GetAnalogActionHandle" )]
					internal static extern ControllerAnalogActionHandle_t /*(ControllerAnalogActionHandle_t)*/ GetAnalogActionHandle( IntPtr ISteamController, string /*const char **/ pszActionName );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamController_GetAnalogActionData" )]
					internal static extern ControllerAnalogActionData_t /*struct ControllerAnalogActionData_t*/ GetAnalogActionData( IntPtr ISteamController, ulong controllerHandle, ulong analogActionHandle );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamController_GetAnalogActionOrigins" )]
					internal static extern int /*int*/ GetAnalogActionOrigins( IntPtr ISteamController, ulong controllerHandle, ulong actionSetHandle, ulong analogActionHandle, out ControllerActionOrigin /*EControllerActionOrigin **/ originsOut );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamController_StopAnalogActionMomentum" )]
					internal static extern void /*void*/ StopAnalogActionMomentum( IntPtr ISteamController, ulong controllerHandle, ulong eAction );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamController_TriggerHapticPulse" )]
					internal static extern void /*void*/ TriggerHapticPulse( IntPtr ISteamController, ulong controllerHandle, SteamControllerPad /*ESteamControllerPad*/ eTargetPad, ushort /*unsigned short*/ usDurationMicroSec );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamController_TriggerRepeatedHapticPulse" )]
					internal static extern void /*void*/ TriggerRepeatedHapticPulse( IntPtr ISteamController, ulong controllerHandle, SteamControllerPad /*ESteamControllerPad*/ eTargetPad, ushort /*unsigned short*/ usDurationMicroSec, ushort /*unsigned short*/ usOffMicroSec, ushort /*unsigned short*/ unRepeat, uint /*unsigned int*/ nFlags );
				}
				
				internal static unsafe class ISteamUGC
				{
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamUGC_CreateQueryUserUGCRequest" )]
					internal static extern UGCQueryHandle_t /*(UGCQueryHandle_t)*/ CreateQueryUserUGCRequest( IntPtr ISteamUGC, uint unAccountID, UserUGCList /*EUserUGCList*/ eListType, UGCMatchingUGCType /*EUGCMatchingUGCType*/ eMatchingUGCType, UserUGCListSortOrder /*EUserUGCListSortOrder*/ eSortOrder, uint nCreatorAppID, uint nConsumerAppID, uint /*uint32*/ unPage );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamUGC_CreateQueryAllUGCRequest" )]
					internal static extern UGCQueryHandle_t /*(UGCQueryHandle_t)*/ CreateQueryAllUGCRequest( IntPtr ISteamUGC, UGCQuery /*EUGCQuery*/ eQueryType, UGCMatchingUGCType /*EUGCMatchingUGCType*/ eMatchingeMatchingUGCTypeFileType, uint nCreatorAppID, uint nConsumerAppID, uint /*uint32*/ unPage );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamUGC_CreateQueryUGCDetailsRequest" )]
					internal static extern UGCQueryHandle_t /*(UGCQueryHandle_t)*/ CreateQueryUGCDetailsRequest( IntPtr ISteamUGC, IntPtr /*PublishedFileId_t **/ pvecPublishedFileID, uint /*uint32*/ unNumPublishedFileIDs );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamUGC_SendQueryUGCRequest" )]
					internal static extern SteamAPICall_t /*(SteamAPICall_t)*/ SendQueryUGCRequest( IntPtr ISteamUGC, ulong handle );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamUGC_GetQueryUGCResult" )]
					internal static extern bool /*bool*/ GetQueryUGCResult( IntPtr ISteamUGC, ulong handle, uint /*uint32*/ index, ref SteamUGCDetails_t.PackSmall /*struct SteamUGCDetails_t **/ pDetails );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamUGC_GetQueryUGCPreviewURL" )]
					internal static extern bool /*bool*/ GetQueryUGCPreviewURL( IntPtr ISteamUGC, ulong handle, uint /*uint32*/ index, System.Text.StringBuilder /*char **/ pchURL, uint /*uint32*/ cchURLSize );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamUGC_GetQueryUGCMetadata" )]
					internal static extern bool /*bool*/ GetQueryUGCMetadata( IntPtr ISteamUGC, ulong handle, uint /*uint32*/ index, System.Text.StringBuilder /*char **/ pchMetadata, uint /*uint32*/ cchMetadatasize );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamUGC_GetQueryUGCChildren" )]
					internal static extern bool /*bool*/ GetQueryUGCChildren( IntPtr ISteamUGC, ulong handle, uint /*uint32*/ index, IntPtr /*PublishedFileId_t **/ pvecPublishedFileID, uint /*uint32*/ cMaxEntries );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamUGC_GetQueryUGCStatistic" )]
					internal static extern bool /*bool*/ GetQueryUGCStatistic( IntPtr ISteamUGC, ulong handle, uint /*uint32*/ index, ItemStatistic /*EItemStatistic*/ eStatType, out uint /*uint32 **/ pStatValue );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamUGC_GetQueryUGCNumAdditionalPreviews" )]
					internal static extern uint /*uint32*/ GetQueryUGCNumAdditionalPreviews( IntPtr ISteamUGC, ulong handle, uint /*uint32*/ index );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamUGC_GetQueryUGCAdditionalPreview" )]
					internal static extern bool /*bool*/ GetQueryUGCAdditionalPreview( IntPtr ISteamUGC, ulong handle, uint /*uint32*/ index, uint /*uint32*/ previewIndex, System.Text.StringBuilder /*char **/ pchURLOrVideoID, uint /*uint32*/ cchURLSize, System.Text.StringBuilder /*char **/ pchOriginalFileName, uint /*uint32*/ cchOriginalFileNameSize, out ItemPreviewType /*EItemPreviewType **/ pPreviewType );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamUGC_GetQueryUGCNumKeyValueTags" )]
					internal static extern uint /*uint32*/ GetQueryUGCNumKeyValueTags( IntPtr ISteamUGC, ulong handle, uint /*uint32*/ index );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamUGC_GetQueryUGCKeyValueTag" )]
					internal static extern bool /*bool*/ GetQueryUGCKeyValueTag( IntPtr ISteamUGC, ulong handle, uint /*uint32*/ index, uint /*uint32*/ keyValueTagIndex, System.Text.StringBuilder /*char **/ pchKey, uint /*uint32*/ cchKeySize, System.Text.StringBuilder /*char **/ pchValue, uint /*uint32*/ cchValueSize );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamUGC_ReleaseQueryUGCRequest" )]
					internal static extern bool /*bool*/ ReleaseQueryUGCRequest( IntPtr ISteamUGC, ulong handle );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamUGC_AddRequiredTag" )]
					internal static extern bool /*bool*/ AddRequiredTag( IntPtr ISteamUGC, ulong handle, string /*const char **/ pTagName );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamUGC_AddExcludedTag" )]
					internal static extern bool /*bool*/ AddExcludedTag( IntPtr ISteamUGC, ulong handle, string /*const char **/ pTagName );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamUGC_SetReturnKeyValueTags" )]
					internal static extern bool /*bool*/ SetReturnKeyValueTags( IntPtr ISteamUGC, ulong handle, [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bReturnKeyValueTags );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamUGC_SetReturnLongDescription" )]
					internal static extern bool /*bool*/ SetReturnLongDescription( IntPtr ISteamUGC, ulong handle, [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bReturnLongDescription );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamUGC_SetReturnMetadata" )]
					internal static extern bool /*bool*/ SetReturnMetadata( IntPtr ISteamUGC, ulong handle, [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bReturnMetadata );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamUGC_SetReturnChildren" )]
					internal static extern bool /*bool*/ SetReturnChildren( IntPtr ISteamUGC, ulong handle, [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bReturnChildren );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamUGC_SetReturnAdditionalPreviews" )]
					internal static extern bool /*bool*/ SetReturnAdditionalPreviews( IntPtr ISteamUGC, ulong handle, [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bReturnAdditionalPreviews );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamUGC_SetReturnTotalOnly" )]
					internal static extern bool /*bool*/ SetReturnTotalOnly( IntPtr ISteamUGC, ulong handle, [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bReturnTotalOnly );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamUGC_SetLanguage" )]
					internal static extern bool /*bool*/ SetLanguage( IntPtr ISteamUGC, ulong handle, string /*const char **/ pchLanguage );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamUGC_SetAllowCachedResponse" )]
					internal static extern bool /*bool*/ SetAllowCachedResponse( IntPtr ISteamUGC, ulong handle, uint /*uint32*/ unMaxAgeSeconds );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamUGC_SetCloudFileNameFilter" )]
					internal static extern bool /*bool*/ SetCloudFileNameFilter( IntPtr ISteamUGC, ulong handle, string /*const char **/ pMatchCloudFileName );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamUGC_SetMatchAnyTag" )]
					internal static extern bool /*bool*/ SetMatchAnyTag( IntPtr ISteamUGC, ulong handle, [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bMatchAnyTag );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamUGC_SetSearchText" )]
					internal static extern bool /*bool*/ SetSearchText( IntPtr ISteamUGC, ulong handle, string /*const char **/ pSearchText );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamUGC_SetRankedByTrendDays" )]
					internal static extern bool /*bool*/ SetRankedByTrendDays( IntPtr ISteamUGC, ulong handle, uint /*uint32*/ unDays );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamUGC_AddRequiredKeyValueTag" )]
					internal static extern bool /*bool*/ AddRequiredKeyValueTag( IntPtr ISteamUGC, ulong handle, string /*const char **/ pKey, string /*const char **/ pValue );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamUGC_RequestUGCDetails" )]
					internal static extern SteamAPICall_t /*(SteamAPICall_t)*/ RequestUGCDetails( IntPtr ISteamUGC, ulong nPublishedFileID, uint /*uint32*/ unMaxAgeSeconds );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamUGC_CreateItem" )]
					internal static extern SteamAPICall_t /*(SteamAPICall_t)*/ CreateItem( IntPtr ISteamUGC, uint nConsumerAppId, WorkshopFileType /*EWorkshopFileType*/ eFileType );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamUGC_StartItemUpdate" )]
					internal static extern UGCUpdateHandle_t /*(UGCUpdateHandle_t)*/ StartItemUpdate( IntPtr ISteamUGC, uint nConsumerAppId, ulong nPublishedFileID );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamUGC_SetItemTitle" )]
					internal static extern bool /*bool*/ SetItemTitle( IntPtr ISteamUGC, ulong handle, string /*const char **/ pchTitle );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamUGC_SetItemDescription" )]
					internal static extern bool /*bool*/ SetItemDescription( IntPtr ISteamUGC, ulong handle, string /*const char **/ pchDescription );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamUGC_SetItemUpdateLanguage" )]
					internal static extern bool /*bool*/ SetItemUpdateLanguage( IntPtr ISteamUGC, ulong handle, string /*const char **/ pchLanguage );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamUGC_SetItemMetadata" )]
					internal static extern bool /*bool*/ SetItemMetadata( IntPtr ISteamUGC, ulong handle, string /*const char **/ pchMetaData );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamUGC_SetItemVisibility" )]
					internal static extern bool /*bool*/ SetItemVisibility( IntPtr ISteamUGC, ulong handle, RemoteStoragePublishedFileVisibility /*ERemoteStoragePublishedFileVisibility*/ eVisibility );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamUGC_SetItemTags" )]
					internal static extern bool /*bool*/ SetItemTags( IntPtr ISteamUGC, ulong updateHandle, IntPtr /*const struct SteamParamStringArray_t **/ pTags );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamUGC_SetItemContent" )]
					internal static extern bool /*bool*/ SetItemContent( IntPtr ISteamUGC, ulong handle, string /*const char **/ pszContentFolder );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamUGC_SetItemPreview" )]
					internal static extern bool /*bool*/ SetItemPreview( IntPtr ISteamUGC, ulong handle, string /*const char **/ pszPreviewFile );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamUGC_RemoveItemKeyValueTags" )]
					internal static extern bool /*bool*/ RemoveItemKeyValueTags( IntPtr ISteamUGC, ulong handle, string /*const char **/ pchKey );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamUGC_AddItemKeyValueTag" )]
					internal static extern bool /*bool*/ AddItemKeyValueTag( IntPtr ISteamUGC, ulong handle, string /*const char **/ pchKey, string /*const char **/ pchValue );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamUGC_AddItemPreviewFile" )]
					internal static extern bool /*bool*/ AddItemPreviewFile( IntPtr ISteamUGC, ulong handle, string /*const char **/ pszPreviewFile, ItemPreviewType /*EItemPreviewType*/ type );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamUGC_AddItemPreviewVideo" )]
					internal static extern bool /*bool*/ AddItemPreviewVideo( IntPtr ISteamUGC, ulong handle, string /*const char **/ pszVideoID );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamUGC_UpdateItemPreviewFile" )]
					internal static extern bool /*bool*/ UpdateItemPreviewFile( IntPtr ISteamUGC, ulong handle, uint /*uint32*/ index, string /*const char **/ pszPreviewFile );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamUGC_UpdateItemPreviewVideo" )]
					internal static extern bool /*bool*/ UpdateItemPreviewVideo( IntPtr ISteamUGC, ulong handle, uint /*uint32*/ index, string /*const char **/ pszVideoID );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamUGC_RemoveItemPreview" )]
					internal static extern bool /*bool*/ RemoveItemPreview( IntPtr ISteamUGC, ulong handle, uint /*uint32*/ index );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamUGC_SubmitItemUpdate" )]
					internal static extern SteamAPICall_t /*(SteamAPICall_t)*/ SubmitItemUpdate( IntPtr ISteamUGC, ulong handle, string /*const char **/ pchChangeNote );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamUGC_GetItemUpdateProgress" )]
					internal static extern ItemUpdateStatus /*EItemUpdateStatus*/ GetItemUpdateProgress( IntPtr ISteamUGC, ulong handle, out ulong /*uint64 **/ punBytesProcessed, out ulong /*uint64 **/ punBytesTotal );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamUGC_SetUserItemVote" )]
					internal static extern SteamAPICall_t /*(SteamAPICall_t)*/ SetUserItemVote( IntPtr ISteamUGC, ulong nPublishedFileID, [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bVoteUp );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamUGC_GetUserItemVote" )]
					internal static extern SteamAPICall_t /*(SteamAPICall_t)*/ GetUserItemVote( IntPtr ISteamUGC, ulong nPublishedFileID );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamUGC_AddItemToFavorites" )]
					internal static extern SteamAPICall_t /*(SteamAPICall_t)*/ AddItemToFavorites( IntPtr ISteamUGC, uint nAppId, ulong nPublishedFileID );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamUGC_RemoveItemFromFavorites" )]
					internal static extern SteamAPICall_t /*(SteamAPICall_t)*/ RemoveItemFromFavorites( IntPtr ISteamUGC, uint nAppId, ulong nPublishedFileID );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamUGC_SubscribeItem" )]
					internal static extern SteamAPICall_t /*(SteamAPICall_t)*/ SubscribeItem( IntPtr ISteamUGC, ulong nPublishedFileID );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamUGC_UnsubscribeItem" )]
					internal static extern SteamAPICall_t /*(SteamAPICall_t)*/ UnsubscribeItem( IntPtr ISteamUGC, ulong nPublishedFileID );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamUGC_GetNumSubscribedItems" )]
					internal static extern uint /*uint32*/ GetNumSubscribedItems( IntPtr ISteamUGC );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamUGC_GetSubscribedItems" )]
					internal static extern uint /*uint32*/ GetSubscribedItems( IntPtr ISteamUGC, IntPtr /*PublishedFileId_t **/ pvecPublishedFileID, uint /*uint32*/ cMaxEntries );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamUGC_GetItemState" )]
					internal static extern uint /*uint32*/ GetItemState( IntPtr ISteamUGC, ulong nPublishedFileID );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamUGC_GetItemInstallInfo" )]
					internal static extern bool /*bool*/ GetItemInstallInfo( IntPtr ISteamUGC, ulong nPublishedFileID, out ulong /*uint64 **/ punSizeOnDisk, System.Text.StringBuilder /*char **/ pchFolder, uint /*uint32*/ cchFolderSize, out uint /*uint32 **/ punTimeStamp );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamUGC_GetItemDownloadInfo" )]
					internal static extern bool /*bool*/ GetItemDownloadInfo( IntPtr ISteamUGC, ulong nPublishedFileID, out ulong /*uint64 **/ punBytesDownloaded, out ulong /*uint64 **/ punBytesTotal );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamUGC_DownloadItem" )]
					internal static extern bool /*bool*/ DownloadItem( IntPtr ISteamUGC, ulong nPublishedFileID, [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bHighPriority );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamUGC_BInitWorkshopForGameServer" )]
					internal static extern bool /*bool*/ BInitWorkshopForGameServer( IntPtr ISteamUGC, uint unWorkshopDepotID, string /*const char **/ pszFolder );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamUGC_SuspendDownloads" )]
					internal static extern void /*void*/ SuspendDownloads( IntPtr ISteamUGC, [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bSuspend );
				}
				
				internal static unsafe class ISteamAppList
				{
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamAppList_GetNumInstalledApps" )]
					internal static extern uint /*uint32*/ GetNumInstalledApps( IntPtr ISteamAppList );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamAppList_GetInstalledApps" )]
					internal static extern uint /*uint32*/ GetInstalledApps( IntPtr ISteamAppList, IntPtr /*AppId_t **/ pvecAppID, uint /*uint32*/ unMaxAppIDs );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamAppList_GetAppName" )]
					internal static extern int /*int*/ GetAppName( IntPtr ISteamAppList, uint nAppID, System.Text.StringBuilder /*char **/ pchName, int /*int*/ cchNameMax );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamAppList_GetAppInstallDir" )]
					internal static extern int /*int*/ GetAppInstallDir( IntPtr ISteamAppList, uint nAppID, System.Text.StringBuilder /*char **/ pchDirectory, int /*int*/ cchNameMax );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamAppList_GetAppBuildId" )]
					internal static extern int /*int*/ GetAppBuildId( IntPtr ISteamAppList, uint nAppID );
				}
				
				internal static unsafe class ISteamHTMLSurface
				{
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamHTMLSurface_DestructISteamHTMLSurface" )]
					internal static extern void /*void*/ DestructISteamHTMLSurface( IntPtr ISteamHTMLSurface );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamHTMLSurface_Init" )]
					internal static extern bool /*bool*/ Init( IntPtr ISteamHTMLSurface );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamHTMLSurface_Shutdown" )]
					internal static extern bool /*bool*/ Shutdown( IntPtr ISteamHTMLSurface );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamHTMLSurface_CreateBrowser" )]
					internal static extern SteamAPICall_t /*(SteamAPICall_t)*/ CreateBrowser( IntPtr ISteamHTMLSurface, string /*const char **/ pchUserAgent, string /*const char **/ pchUserCSS );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamHTMLSurface_RemoveBrowser" )]
					internal static extern void /*void*/ RemoveBrowser( IntPtr ISteamHTMLSurface, uint unBrowserHandle );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamHTMLSurface_LoadURL" )]
					internal static extern void /*void*/ LoadURL( IntPtr ISteamHTMLSurface, uint unBrowserHandle, string /*const char **/ pchURL, string /*const char **/ pchPostData );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamHTMLSurface_SetSize" )]
					internal static extern void /*void*/ SetSize( IntPtr ISteamHTMLSurface, uint unBrowserHandle, uint /*uint32*/ unWidth, uint /*uint32*/ unHeight );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamHTMLSurface_StopLoad" )]
					internal static extern void /*void*/ StopLoad( IntPtr ISteamHTMLSurface, uint unBrowserHandle );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamHTMLSurface_Reload" )]
					internal static extern void /*void*/ Reload( IntPtr ISteamHTMLSurface, uint unBrowserHandle );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamHTMLSurface_GoBack" )]
					internal static extern void /*void*/ GoBack( IntPtr ISteamHTMLSurface, uint unBrowserHandle );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamHTMLSurface_GoForward" )]
					internal static extern void /*void*/ GoForward( IntPtr ISteamHTMLSurface, uint unBrowserHandle );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamHTMLSurface_AddHeader" )]
					internal static extern void /*void*/ AddHeader( IntPtr ISteamHTMLSurface, uint unBrowserHandle, string /*const char **/ pchKey, string /*const char **/ pchValue );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamHTMLSurface_ExecuteJavascript" )]
					internal static extern void /*void*/ ExecuteJavascript( IntPtr ISteamHTMLSurface, uint unBrowserHandle, string /*const char **/ pchScript );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamHTMLSurface_MouseUp" )]
					internal static extern void /*void*/ MouseUp( IntPtr ISteamHTMLSurface, uint unBrowserHandle, HTMLMouseButton /*ISteamHTMLSurface::EHTMLMouseButton*/ eMouseButton );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamHTMLSurface_MouseDown" )]
					internal static extern void /*void*/ MouseDown( IntPtr ISteamHTMLSurface, uint unBrowserHandle, HTMLMouseButton /*ISteamHTMLSurface::EHTMLMouseButton*/ eMouseButton );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamHTMLSurface_MouseDoubleClick" )]
					internal static extern void /*void*/ MouseDoubleClick( IntPtr ISteamHTMLSurface, uint unBrowserHandle, HTMLMouseButton /*ISteamHTMLSurface::EHTMLMouseButton*/ eMouseButton );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamHTMLSurface_MouseMove" )]
					internal static extern void /*void*/ MouseMove( IntPtr ISteamHTMLSurface, uint unBrowserHandle, int /*int*/ x, int /*int*/ y );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamHTMLSurface_MouseWheel" )]
					internal static extern void /*void*/ MouseWheel( IntPtr ISteamHTMLSurface, uint unBrowserHandle, int /*int32*/ nDelta );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamHTMLSurface_KeyDown" )]
					internal static extern void /*void*/ KeyDown( IntPtr ISteamHTMLSurface, uint unBrowserHandle, uint /*uint32*/ nNativeKeyCode, HTMLKeyModifiers /*ISteamHTMLSurface::EHTMLKeyModifiers*/ eHTMLKeyModifiers );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamHTMLSurface_KeyUp" )]
					internal static extern void /*void*/ KeyUp( IntPtr ISteamHTMLSurface, uint unBrowserHandle, uint /*uint32*/ nNativeKeyCode, HTMLKeyModifiers /*ISteamHTMLSurface::EHTMLKeyModifiers*/ eHTMLKeyModifiers );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamHTMLSurface_KeyChar" )]
					internal static extern void /*void*/ KeyChar( IntPtr ISteamHTMLSurface, uint unBrowserHandle, uint /*uint32*/ cUnicodeChar, HTMLKeyModifiers /*ISteamHTMLSurface::EHTMLKeyModifiers*/ eHTMLKeyModifiers );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamHTMLSurface_SetHorizontalScroll" )]
					internal static extern void /*void*/ SetHorizontalScroll( IntPtr ISteamHTMLSurface, uint unBrowserHandle, uint /*uint32*/ nAbsolutePixelScroll );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamHTMLSurface_SetVerticalScroll" )]
					internal static extern void /*void*/ SetVerticalScroll( IntPtr ISteamHTMLSurface, uint unBrowserHandle, uint /*uint32*/ nAbsolutePixelScroll );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamHTMLSurface_SetKeyFocus" )]
					internal static extern void /*void*/ SetKeyFocus( IntPtr ISteamHTMLSurface, uint unBrowserHandle, [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bHasKeyFocus );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamHTMLSurface_ViewSource" )]
					internal static extern void /*void*/ ViewSource( IntPtr ISteamHTMLSurface, uint unBrowserHandle );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamHTMLSurface_CopyToClipboard" )]
					internal static extern void /*void*/ CopyToClipboard( IntPtr ISteamHTMLSurface, uint unBrowserHandle );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamHTMLSurface_PasteFromClipboard" )]
					internal static extern void /*void*/ PasteFromClipboard( IntPtr ISteamHTMLSurface, uint unBrowserHandle );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamHTMLSurface_Find" )]
					internal static extern void /*void*/ Find( IntPtr ISteamHTMLSurface, uint unBrowserHandle, string /*const char **/ pchSearchStr, [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bCurrentlyInFind, [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bReverse );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamHTMLSurface_StopFind" )]
					internal static extern void /*void*/ StopFind( IntPtr ISteamHTMLSurface, uint unBrowserHandle );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamHTMLSurface_GetLinkAtPosition" )]
					internal static extern void /*void*/ GetLinkAtPosition( IntPtr ISteamHTMLSurface, uint unBrowserHandle, int /*int*/ x, int /*int*/ y );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamHTMLSurface_SetCookie" )]
					internal static extern void /*void*/ SetCookie( IntPtr ISteamHTMLSurface, string /*const char **/ pchHostname, string /*const char **/ pchKey, string /*const char **/ pchValue, string /*const char **/ pchPath, uint nExpires, [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bSecure, [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bHTTPOnly );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamHTMLSurface_SetPageScaleFactor" )]
					internal static extern void /*void*/ SetPageScaleFactor( IntPtr ISteamHTMLSurface, uint unBrowserHandle, float /*float*/ flZoom, int /*int*/ nPointX, int /*int*/ nPointY );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamHTMLSurface_SetBackgroundMode" )]
					internal static extern void /*void*/ SetBackgroundMode( IntPtr ISteamHTMLSurface, uint unBrowserHandle, [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bBackgroundMode );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamHTMLSurface_AllowStartRequest" )]
					internal static extern void /*void*/ AllowStartRequest( IntPtr ISteamHTMLSurface, uint unBrowserHandle, [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bAllowed );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamHTMLSurface_JSDialogResponse" )]
					internal static extern void /*void*/ JSDialogResponse( IntPtr ISteamHTMLSurface, uint unBrowserHandle, [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bResult );
				}
				
				internal static unsafe class ISteamInventory
				{
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamInventory_GetResultStatus" )]
					internal static extern Result /*EResult*/ GetResultStatus( IntPtr ISteamInventory, int resultHandle );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamInventory_GetResultItems" )]
					internal static extern bool /*bool*/ GetResultItems( IntPtr ISteamInventory, int resultHandle, IntPtr /*struct SteamItemDetails_t **/ pOutItemsArray, out uint /*uint32 **/ punOutItemsArraySize );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamInventory_GetResultTimestamp" )]
					internal static extern uint /*uint32*/ GetResultTimestamp( IntPtr ISteamInventory, int resultHandle );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamInventory_CheckResultSteamID" )]
					internal static extern bool /*bool*/ CheckResultSteamID( IntPtr ISteamInventory, int resultHandle, ulong steamIDExpected );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamInventory_DestroyResult" )]
					internal static extern void /*void*/ DestroyResult( IntPtr ISteamInventory, int resultHandle );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamInventory_GetAllItems" )]
					internal static extern bool /*bool*/ GetAllItems( IntPtr ISteamInventory, ref int pResultHandle );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamInventory_GetItemsByID" )]
					internal static extern bool /*bool*/ GetItemsByID( IntPtr ISteamInventory, ref int pResultHandle, IntPtr /*const SteamItemInstanceID_t **/ pInstanceIDs, uint /*uint32*/ unCountInstanceIDs );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamInventory_SerializeResult" )]
					internal static extern bool /*bool*/ SerializeResult( IntPtr ISteamInventory, int resultHandle, IntPtr /*void **/ pOutBuffer, out uint /*uint32 **/ punOutBufferSize );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamInventory_DeserializeResult" )]
					internal static extern bool /*bool*/ DeserializeResult( IntPtr ISteamInventory, ref int pOutResultHandle, IntPtr /*const void **/ pBuffer, uint /*uint32*/ unBufferSize, [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bRESERVED_MUST_BE_FALSE );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamInventory_GenerateItems" )]
					internal static extern bool /*bool*/ GenerateItems( IntPtr ISteamInventory, ref int pResultHandle, IntPtr /*const SteamItemDef_t **/ pArrayItemDefs, out uint /*const uint32 **/ punArrayQuantity, uint /*uint32*/ unArrayLength );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamInventory_GrantPromoItems" )]
					internal static extern bool /*bool*/ GrantPromoItems( IntPtr ISteamInventory, ref int pResultHandle );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamInventory_AddPromoItem" )]
					internal static extern bool /*bool*/ AddPromoItem( IntPtr ISteamInventory, ref int pResultHandle, int itemDef );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamInventory_AddPromoItems" )]
					internal static extern bool /*bool*/ AddPromoItems( IntPtr ISteamInventory, ref int pResultHandle, IntPtr /*const SteamItemDef_t **/ pArrayItemDefs, uint /*uint32*/ unArrayLength );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamInventory_ConsumeItem" )]
					internal static extern bool /*bool*/ ConsumeItem( IntPtr ISteamInventory, ref int pResultHandle, ulong itemConsume, uint /*uint32*/ unQuantity );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamInventory_ExchangeItems" )]
					internal static extern bool /*bool*/ ExchangeItems( IntPtr ISteamInventory, ref int pResultHandle, ref int pArrayGenerate, out uint /*const uint32 **/ punArrayGenerateQuantity, uint /*uint32*/ unArrayGenerateLength, IntPtr /*const SteamItemInstanceID_t **/ pArrayDestroy, IntPtr /*const uint32 **/ punArrayDestroyQuantity, uint /*uint32*/ unArrayDestroyLength );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamInventory_TransferItemQuantity" )]
					internal static extern bool /*bool*/ TransferItemQuantity( IntPtr ISteamInventory, ref int pResultHandle, ulong itemIdSource, uint /*uint32*/ unQuantity, ulong itemIdDest );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamInventory_SendItemDropHeartbeat" )]
					internal static extern void /*void*/ SendItemDropHeartbeat( IntPtr ISteamInventory );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamInventory_TriggerItemDrop" )]
					internal static extern bool /*bool*/ TriggerItemDrop( IntPtr ISteamInventory, ref int pResultHandle, int dropListDefinition );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamInventory_TradeItems" )]
					internal static extern bool /*bool*/ TradeItems( IntPtr ISteamInventory, ref int pResultHandle, ulong steamIDTradePartner, ref ulong pArrayGive, out uint /*const uint32 **/ pArrayGiveQuantity, uint /*uint32*/ nArrayGiveLength, ref ulong pArrayGet, out uint /*const uint32 **/ pArrayGetQuantity, uint /*uint32*/ nArrayGetLength );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamInventory_LoadItemDefinitions" )]
					internal static extern bool /*bool*/ LoadItemDefinitions( IntPtr ISteamInventory );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamInventory_GetItemDefinitionIDs" )]
					internal static extern bool /*bool*/ GetItemDefinitionIDs( IntPtr ISteamInventory, IntPtr /*SteamItemDef_t **/ pItemDefIDs, out uint /*uint32 **/ punItemDefIDsArraySize );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamInventory_GetItemDefinitionProperty" )]
					internal static extern bool /*bool*/ GetItemDefinitionProperty( IntPtr ISteamInventory, int iDefinition, string /*const char **/ pchPropertyName, System.Text.StringBuilder /*char **/ pchValueBuffer, out uint /*uint32 **/ punValueBufferSize );
				}
				
				internal static unsafe class ISteamVideo
				{
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamVideo_GetVideoURL" )]
					internal static extern void /*void*/ GetVideoURL( IntPtr ISteamVideo, uint unVideoAppID );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamVideo_IsBroadcasting" )]
					internal static extern bool /*bool*/ IsBroadcasting( IntPtr ISteamVideo, IntPtr /*int **/ pnNumViewers );
				}
				
				internal static unsafe class ISteamGameServer
				{
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamGameServer_InitGameServer" )]
					internal static extern bool /*bool*/ InitGameServer( IntPtr ISteamGameServer, uint /*uint32*/ unIP, ushort /*uint16*/ usGamePort, ushort /*uint16*/ usQueryPort, uint /*uint32*/ unFlags, uint nGameAppId, string /*const char **/ pchVersionString );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamGameServer_SetProduct" )]
					internal static extern void /*void*/ SetProduct( IntPtr ISteamGameServer, string /*const char **/ pszProduct );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamGameServer_SetGameDescription" )]
					internal static extern void /*void*/ SetGameDescription( IntPtr ISteamGameServer, string /*const char **/ pszGameDescription );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamGameServer_SetModDir" )]
					internal static extern void /*void*/ SetModDir( IntPtr ISteamGameServer, string /*const char **/ pszModDir );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamGameServer_SetDedicatedServer" )]
					internal static extern void /*void*/ SetDedicatedServer( IntPtr ISteamGameServer, [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bDedicated );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamGameServer_LogOn" )]
					internal static extern void /*void*/ LogOn( IntPtr ISteamGameServer, string /*const char **/ pszToken );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamGameServer_LogOnAnonymous" )]
					internal static extern void /*void*/ LogOnAnonymous( IntPtr ISteamGameServer );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamGameServer_LogOff" )]
					internal static extern void /*void*/ LogOff( IntPtr ISteamGameServer );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamGameServer_BLoggedOn" )]
					internal static extern bool /*bool*/ BLoggedOn( IntPtr ISteamGameServer );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamGameServer_BSecure" )]
					internal static extern bool /*bool*/ BSecure( IntPtr ISteamGameServer );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamGameServer_GetSteamID" )]
					internal static extern CSteamID /*(class CSteamID)*/ GetSteamID( IntPtr ISteamGameServer );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamGameServer_WasRestartRequested" )]
					internal static extern bool /*bool*/ WasRestartRequested( IntPtr ISteamGameServer );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamGameServer_SetMaxPlayerCount" )]
					internal static extern void /*void*/ SetMaxPlayerCount( IntPtr ISteamGameServer, int /*int*/ cPlayersMax );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamGameServer_SetBotPlayerCount" )]
					internal static extern void /*void*/ SetBotPlayerCount( IntPtr ISteamGameServer, int /*int*/ cBotplayers );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamGameServer_SetServerName" )]
					internal static extern void /*void*/ SetServerName( IntPtr ISteamGameServer, string /*const char **/ pszServerName );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamGameServer_SetMapName" )]
					internal static extern void /*void*/ SetMapName( IntPtr ISteamGameServer, string /*const char **/ pszMapName );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamGameServer_SetPasswordProtected" )]
					internal static extern void /*void*/ SetPasswordProtected( IntPtr ISteamGameServer, [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bPasswordProtected );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamGameServer_SetSpectatorPort" )]
					internal static extern void /*void*/ SetSpectatorPort( IntPtr ISteamGameServer, ushort /*uint16*/ unSpectatorPort );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamGameServer_SetSpectatorServerName" )]
					internal static extern void /*void*/ SetSpectatorServerName( IntPtr ISteamGameServer, string /*const char **/ pszSpectatorServerName );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamGameServer_ClearAllKeyValues" )]
					internal static extern void /*void*/ ClearAllKeyValues( IntPtr ISteamGameServer );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamGameServer_SetKeyValue" )]
					internal static extern void /*void*/ SetKeyValue( IntPtr ISteamGameServer, string /*const char **/ pKey, string /*const char **/ pValue );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamGameServer_SetGameTags" )]
					internal static extern void /*void*/ SetGameTags( IntPtr ISteamGameServer, string /*const char **/ pchGameTags );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamGameServer_SetGameData" )]
					internal static extern void /*void*/ SetGameData( IntPtr ISteamGameServer, string /*const char **/ pchGameData );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamGameServer_SetRegion" )]
					internal static extern void /*void*/ SetRegion( IntPtr ISteamGameServer, string /*const char **/ pszRegion );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamGameServer_SendUserConnectAndAuthenticate" )]
					internal static extern bool /*bool*/ SendUserConnectAndAuthenticate( IntPtr ISteamGameServer, uint /*uint32*/ unIPClient, IntPtr /*const void **/ pvAuthBlob, uint /*uint32*/ cubAuthBlobSize, out ulong pSteamIDUser );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamGameServer_CreateUnauthenticatedUserConnection" )]
					internal static extern CSteamID /*(class CSteamID)*/ CreateUnauthenticatedUserConnection( IntPtr ISteamGameServer );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamGameServer_SendUserDisconnect" )]
					internal static extern void /*void*/ SendUserDisconnect( IntPtr ISteamGameServer, ulong steamIDUser );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamGameServer_BUpdateUserData" )]
					internal static extern bool /*bool*/ BUpdateUserData( IntPtr ISteamGameServer, ulong steamIDUser, string /*const char **/ pchPlayerName, uint /*uint32*/ uScore );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamGameServer_GetAuthSessionTicket" )]
					internal static extern HAuthTicket /*(HAuthTicket)*/ GetAuthSessionTicket( IntPtr ISteamGameServer, IntPtr /*void **/ pTicket, int /*int*/ cbMaxTicket, out uint /*uint32 **/ pcbTicket );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamGameServer_BeginAuthSession" )]
					internal static extern BeginAuthSessionResult /*EBeginAuthSessionResult*/ BeginAuthSession( IntPtr ISteamGameServer, IntPtr /*const void **/ pAuthTicket, int /*int*/ cbAuthTicket, ulong steamID );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamGameServer_EndAuthSession" )]
					internal static extern void /*void*/ EndAuthSession( IntPtr ISteamGameServer, ulong steamID );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamGameServer_CancelAuthTicket" )]
					internal static extern void /*void*/ CancelAuthTicket( IntPtr ISteamGameServer, uint hAuthTicket );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamGameServer_UserHasLicenseForApp" )]
					internal static extern UserHasLicenseForAppResult /*EUserHasLicenseForAppResult*/ UserHasLicenseForApp( IntPtr ISteamGameServer, ulong steamID, uint appID );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamGameServer_RequestUserGroupStatus" )]
					internal static extern bool /*bool*/ RequestUserGroupStatus( IntPtr ISteamGameServer, ulong steamIDUser, ulong steamIDGroup );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamGameServer_GetGameplayStats" )]
					internal static extern void /*void*/ GetGameplayStats( IntPtr ISteamGameServer );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamGameServer_GetServerReputation" )]
					internal static extern SteamAPICall_t /*(SteamAPICall_t)*/ GetServerReputation( IntPtr ISteamGameServer );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamGameServer_GetPublicIP" )]
					internal static extern uint /*uint32*/ GetPublicIP( IntPtr ISteamGameServer );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamGameServer_HandleIncomingPacket" )]
					internal static extern bool /*bool*/ HandleIncomingPacket( IntPtr ISteamGameServer, IntPtr /*const void **/ pData, int /*int*/ cbData, uint /*uint32*/ srcIP, ushort /*uint16*/ srcPort );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamGameServer_GetNextOutgoingPacket" )]
					internal static extern int /*int*/ GetNextOutgoingPacket( IntPtr ISteamGameServer, IntPtr /*void **/ pOut, int /*int*/ cbMaxOut, out uint /*uint32 **/ pNetAdr, out ushort /*uint16 **/ pPort );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamGameServer_EnableHeartbeats" )]
					internal static extern void /*void*/ EnableHeartbeats( IntPtr ISteamGameServer, [MarshalAs(UnmanagedType.U1)] bool /*bool*/ bActive );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamGameServer_SetHeartbeatInterval" )]
					internal static extern void /*void*/ SetHeartbeatInterval( IntPtr ISteamGameServer, int /*int*/ iHeartbeatInterval );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamGameServer_ForceHeartbeat" )]
					internal static extern void /*void*/ ForceHeartbeat( IntPtr ISteamGameServer );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamGameServer_AssociateWithClan" )]
					internal static extern SteamAPICall_t /*(SteamAPICall_t)*/ AssociateWithClan( IntPtr ISteamGameServer, ulong steamIDClan );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamGameServer_ComputeNewPlayerCompatibility" )]
					internal static extern SteamAPICall_t /*(SteamAPICall_t)*/ ComputeNewPlayerCompatibility( IntPtr ISteamGameServer, ulong steamIDNewPlayer );
				}
				
				internal static unsafe class ISteamGameServerStats
				{
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamGameServerStats_RequestUserStats" )]
					internal static extern SteamAPICall_t /*(SteamAPICall_t)*/ RequestUserStats( IntPtr ISteamGameServerStats, ulong steamIDUser );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamGameServerStats_GetUserStat" )]
					internal static extern bool /*bool*/ GetUserStat( IntPtr ISteamGameServerStats, ulong steamIDUser, string /*const char **/ pchName, out int /*int32 **/ pData );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamGameServerStats_GetUserStat0" )]
					internal static extern bool /*bool*/ GetUserStat0( IntPtr ISteamGameServerStats, ulong steamIDUser, string /*const char **/ pchName, out float /*float **/ pData );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamGameServerStats_GetUserAchievement" )]
					internal static extern bool /*bool*/ GetUserAchievement( IntPtr ISteamGameServerStats, ulong steamIDUser, string /*const char **/ pchName, [MarshalAs(UnmanagedType.U1)] ref bool /*bool **/ pbAchieved );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamGameServerStats_SetUserStat" )]
					internal static extern bool /*bool*/ SetUserStat( IntPtr ISteamGameServerStats, ulong steamIDUser, string /*const char **/ pchName, int /*int32*/ nData );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamGameServerStats_SetUserStat0" )]
					internal static extern bool /*bool*/ SetUserStat0( IntPtr ISteamGameServerStats, ulong steamIDUser, string /*const char **/ pchName, float /*float*/ fData );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamGameServerStats_UpdateUserAvgRateStat" )]
					internal static extern bool /*bool*/ UpdateUserAvgRateStat( IntPtr ISteamGameServerStats, ulong steamIDUser, string /*const char **/ pchName, float /*float*/ flCountThisSession, double /*double*/ dSessionLength );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamGameServerStats_SetUserAchievement" )]
					internal static extern bool /*bool*/ SetUserAchievement( IntPtr ISteamGameServerStats, ulong steamIDUser, string /*const char **/ pchName );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamGameServerStats_ClearUserAchievement" )]
					internal static extern bool /*bool*/ ClearUserAchievement( IntPtr ISteamGameServerStats, ulong steamIDUser, string /*const char **/ pchName );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_ISteamGameServerStats_StoreUserStats" )]
					internal static extern SteamAPICall_t /*(SteamAPICall_t)*/ StoreUserStats( IntPtr ISteamGameServerStats, ulong steamIDUser );
				}
				
				internal static unsafe class SteamApi
				{
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_Init" )]
					internal static extern void /*void*/ SteamAPI_Init();
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_RunCallbacks" )]
					internal static extern void /*void*/ SteamAPI_RunCallbacks();
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamGameServer_RunCallbacks" )]
					internal static extern void /*void*/ SteamGameServer_RunCallbacks();
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_RegisterCallback" )]
					internal static extern void /*void*/ SteamAPI_RegisterCallback( IntPtr /*void **/ pCallback, int /*int*/ callback );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_UnregisterCallback" )]
					internal static extern void /*void*/ SteamAPI_UnregisterCallback( IntPtr /*void **/ pCallback );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamInternal_GameServer_Init" )]
					internal static extern bool /*bool*/ SteamInternal_GameServer_Init( uint /*uint32*/ unIP, ushort /*uint16*/ usPort, ushort /*uint16*/ usGamePort, ushort /*uint16*/ usQueryPort, int /*int*/ eServerMode, string /*const char **/ pchVersionString );
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_Shutdown" )]
					internal static extern void /*void*/ SteamAPI_Shutdown();
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_GetHSteamUser" )]
					internal static extern HSteamUser /*(HSteamUser)*/ SteamAPI_GetHSteamUser();
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamAPI_GetHSteamPipe" )]
					internal static extern HSteamPipe /*(HSteamPipe)*/ SteamAPI_GetHSteamPipe();
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamGameServer_GetHSteamUser" )]
					internal static extern HSteamUser /*(HSteamUser)*/ SteamGameServer_GetHSteamUser();
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamGameServer_GetHSteamPipe" )]
					internal static extern HSteamPipe /*(HSteamPipe)*/ SteamGameServer_GetHSteamPipe();
					[DllImportAttribute( "libsteam_api.dylib", EntryPoint = "SteamInternal_CreateInterface" )]
					internal static extern IntPtr /*void **/ SteamInternal_CreateInterface( string /*const char **/ version );
				}
				
			}
		}
	}
}
