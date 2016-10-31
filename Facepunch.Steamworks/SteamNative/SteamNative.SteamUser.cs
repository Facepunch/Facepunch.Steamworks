using System;
using System.Runtime.InteropServices;

namespace SteamNative
{
	internal unsafe class SteamUser : IDisposable
	{
		//
		// Holds a platform specific implentation
		//
		internal Platform.Interface platform;
		internal Facepunch.Steamworks.BaseSteamworks steamworks;
		
		//
		// Constructor decides which implementation to use based on current platform
		//
		public SteamUser( Facepunch.Steamworks.BaseSteamworks steamworks, IntPtr pointer )
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
		
		// void
		public void AdvertiseGame( CSteamID steamIDGameServer /*class CSteamID*/, uint unIPServer /*uint32*/, ushort usPortServer /*uint16*/ )
		{
			platform.ISteamUser_AdvertiseGame( steamIDGameServer.Value, unIPServer, usPortServer );
		}
		
		// BeginAuthSessionResult
		public BeginAuthSessionResult BeginAuthSession( IntPtr pAuthTicket /*const void **/, int cbAuthTicket /*int*/, CSteamID steamID /*class CSteamID*/ )
		{
			return platform.ISteamUser_BeginAuthSession( (IntPtr) pAuthTicket, cbAuthTicket, steamID.Value );
		}
		
		// bool
		public bool BIsBehindNAT()
		{
			return platform.ISteamUser_BIsBehindNAT();
		}
		
		// bool
		public bool BIsPhoneIdentifying()
		{
			return platform.ISteamUser_BIsPhoneIdentifying();
		}
		
		// bool
		public bool BIsPhoneRequiringVerification()
		{
			return platform.ISteamUser_BIsPhoneRequiringVerification();
		}
		
		// bool
		public bool BIsPhoneVerified()
		{
			return platform.ISteamUser_BIsPhoneVerified();
		}
		
		// bool
		public bool BIsTwoFactorEnabled()
		{
			return platform.ISteamUser_BIsTwoFactorEnabled();
		}
		
		// bool
		public bool BLoggedOn()
		{
			return platform.ISteamUser_BLoggedOn();
		}
		
		// void
		public void CancelAuthTicket( HAuthTicket hAuthTicket /*HAuthTicket*/ )
		{
			platform.ISteamUser_CancelAuthTicket( hAuthTicket.Value );
		}
		
		// VoiceResult
		public VoiceResult DecompressVoice( IntPtr pCompressed /*const void **/, uint cbCompressed /*uint32*/, IntPtr pDestBuffer /*void **/, uint cbDestBufferSize /*uint32*/, out uint nBytesWritten /*uint32 **/, uint nDesiredSampleRate /*uint32*/ )
		{
			return platform.ISteamUser_DecompressVoice( (IntPtr) pCompressed, cbCompressed, (IntPtr) pDestBuffer, cbDestBufferSize, out nBytesWritten, nDesiredSampleRate );
		}
		
		// void
		public void EndAuthSession( CSteamID steamID /*class CSteamID*/ )
		{
			platform.ISteamUser_EndAuthSession( steamID.Value );
		}
		
		// HAuthTicket
		public HAuthTicket GetAuthSessionTicket( IntPtr pTicket /*void **/, int cbMaxTicket /*int*/, out uint pcbTicket /*uint32 **/ )
		{
			return platform.ISteamUser_GetAuthSessionTicket( (IntPtr) pTicket, cbMaxTicket, out pcbTicket );
		}
		
		// VoiceResult
		public VoiceResult GetAvailableVoice( out uint pcbCompressed /*uint32 **/, out uint pcbUncompressed /*uint32 **/, uint nUncompressedVoiceDesiredSampleRate /*uint32*/ )
		{
			return platform.ISteamUser_GetAvailableVoice( out pcbCompressed, out pcbUncompressed, nUncompressedVoiceDesiredSampleRate );
		}
		
		// bool
		public bool GetEncryptedAppTicket( IntPtr pTicket /*void **/, int cbMaxTicket /*int*/, out uint pcbTicket /*uint32 **/ )
		{
			return platform.ISteamUser_GetEncryptedAppTicket( (IntPtr) pTicket, cbMaxTicket, out pcbTicket );
		}
		
		// int
		public int GetGameBadgeLevel( int nSeries /*int*/, bool bFoil /*bool*/ )
		{
			return platform.ISteamUser_GetGameBadgeLevel( nSeries, bFoil );
		}
		
		// HSteamUser
		public HSteamUser GetHSteamUser()
		{
			return platform.ISteamUser_GetHSteamUser();
		}
		
		// int
		public int GetPlayerSteamLevel()
		{
			return platform.ISteamUser_GetPlayerSteamLevel();
		}
		
		// ulong
		public ulong GetSteamID()
		{
			return platform.ISteamUser_GetSteamID();
		}
		
		// bool
		// with: Detect_StringFetch True
		public string GetUserDataFolder()
		{
			bool bSuccess = default( bool );
			System.Text.StringBuilder pchBuffer_sb = new System.Text.StringBuilder( 4096 );
			int cubBuffer = 4096;
			bSuccess = platform.ISteamUser_GetUserDataFolder( pchBuffer_sb, cubBuffer );
			if ( !bSuccess ) return null;
			return pchBuffer_sb.ToString();
		}
		
		// VoiceResult
		public VoiceResult GetVoice( bool bWantCompressed /*bool*/, IntPtr pDestBuffer /*void **/, uint cbDestBufferSize /*uint32*/, out uint nBytesWritten /*uint32 **/, bool bWantUncompressed /*bool*/, IntPtr pUncompressedDestBuffer /*void **/, uint cbUncompressedDestBufferSize /*uint32*/, out uint nUncompressBytesWritten /*uint32 **/, uint nUncompressedVoiceDesiredSampleRate /*uint32*/ )
		{
			return platform.ISteamUser_GetVoice( bWantCompressed, (IntPtr) pDestBuffer, cbDestBufferSize, out nBytesWritten, bWantUncompressed, (IntPtr) pUncompressedDestBuffer, cbUncompressedDestBufferSize, out nUncompressBytesWritten, nUncompressedVoiceDesiredSampleRate );
		}
		
		// uint
		public uint GetVoiceOptimalSampleRate()
		{
			return platform.ISteamUser_GetVoiceOptimalSampleRate();
		}
		
		// int
		public int InitiateGameConnection( IntPtr pAuthBlob /*void **/, int cbMaxAuthBlob /*int*/, CSteamID steamIDGameServer /*class CSteamID*/, uint unIPServer /*uint32*/, ushort usPortServer /*uint16*/, bool bSecure /*bool*/ )
		{
			return platform.ISteamUser_InitiateGameConnection( (IntPtr) pAuthBlob, cbMaxAuthBlob, steamIDGameServer.Value, unIPServer, usPortServer, bSecure );
		}
		
		// SteamAPICall_t
		public CallbackHandle RequestEncryptedAppTicket( IntPtr pDataToInclude /*void **/, int cbDataToInclude /*int*/, Action<EncryptedAppTicketResponse_t, bool> CallbackFunction = null /*Action<EncryptedAppTicketResponse_t, bool>*/ )
		{
			SteamAPICall_t callback = 0;
			callback = platform.ISteamUser_RequestEncryptedAppTicket( (IntPtr) pDataToInclude, cbDataToInclude );
			
			if ( CallbackFunction == null ) return null;
			
			return EncryptedAppTicketResponse_t.CallResult( steamworks, callback, CallbackFunction );
		}
		
		// SteamAPICall_t
		public CallbackHandle RequestStoreAuthURL( string pchRedirectURL /*const char **/, Action<StoreAuthURLResponse_t, bool> CallbackFunction = null /*Action<StoreAuthURLResponse_t, bool>*/ )
		{
			SteamAPICall_t callback = 0;
			callback = platform.ISteamUser_RequestStoreAuthURL( pchRedirectURL );
			
			if ( CallbackFunction == null ) return null;
			
			return StoreAuthURLResponse_t.CallResult( steamworks, callback, CallbackFunction );
		}
		
		// void
		public void StartVoiceRecording()
		{
			platform.ISteamUser_StartVoiceRecording();
		}
		
		// void
		public void StopVoiceRecording()
		{
			platform.ISteamUser_StopVoiceRecording();
		}
		
		// void
		public void TerminateGameConnection( uint unIPServer /*uint32*/, ushort usPortServer /*uint16*/ )
		{
			platform.ISteamUser_TerminateGameConnection( unIPServer, usPortServer );
		}
		
		// void
		public void TrackAppUsageEvent( CGameID gameID /*class CGameID*/, int eAppUsageEvent /*int*/, string pchExtraInfo /*const char **/ )
		{
			platform.ISteamUser_TrackAppUsageEvent( gameID.Value, eAppUsageEvent, pchExtraInfo );
		}
		
		// UserHasLicenseForAppResult
		public UserHasLicenseForAppResult UserHasLicenseForApp( CSteamID steamID /*class CSteamID*/, AppId_t appID /*AppId_t*/ )
		{
			return platform.ISteamUser_UserHasLicenseForApp( steamID.Value, appID.Value );
		}
		
	}
}
