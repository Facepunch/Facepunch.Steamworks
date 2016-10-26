using System;
using System.Runtime.InteropServices;

namespace SteamNative
{
	public unsafe class SteamUser : IDisposable
	{
		//
		// Holds a platform specific implentation
		//
		internal Platform.Interface _pi;
		
		//
		// Constructor decides which implementation to use based on current platform
		//
		public SteamUser( IntPtr pointer )
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
		
		// void
		public void AdvertiseGame( CSteamID steamIDGameServer /*class CSteamID*/, uint unIPServer /*uint32*/, ushort usPortServer /*uint16*/ )
		{
			_pi.ISteamUser_AdvertiseGame( steamIDGameServer.Value, unIPServer, usPortServer );
		}
		
		// BeginAuthSessionResult
		public BeginAuthSessionResult BeginAuthSession( IntPtr pAuthTicket /*const void **/, int cbAuthTicket /*int*/, CSteamID steamID /*class CSteamID*/ )
		{
			return _pi.ISteamUser_BeginAuthSession( (IntPtr) pAuthTicket, cbAuthTicket, steamID.Value );
		}
		
		// bool
		public bool BIsBehindNAT()
		{
			return _pi.ISteamUser_BIsBehindNAT();
		}
		
		// bool
		public bool BIsPhoneVerified()
		{
			return _pi.ISteamUser_BIsPhoneVerified();
		}
		
		// bool
		public bool BIsTwoFactorEnabled()
		{
			return _pi.ISteamUser_BIsTwoFactorEnabled();
		}
		
		// bool
		public bool BLoggedOn()
		{
			return _pi.ISteamUser_BLoggedOn();
		}
		
		// void
		public void CancelAuthTicket( HAuthTicket hAuthTicket /*HAuthTicket*/ )
		{
			_pi.ISteamUser_CancelAuthTicket( hAuthTicket.Value );
		}
		
		// VoiceResult
		public VoiceResult DecompressVoice( IntPtr pCompressed /*const void **/, uint cbCompressed /*uint32*/, IntPtr pDestBuffer /*void **/, uint cbDestBufferSize /*uint32*/, out uint nBytesWritten /*uint32 **/, uint nDesiredSampleRate /*uint32*/ )
		{
			return _pi.ISteamUser_DecompressVoice( (IntPtr) pCompressed, cbCompressed, (IntPtr) pDestBuffer, cbDestBufferSize, out nBytesWritten, nDesiredSampleRate );
		}
		
		// void
		public void EndAuthSession( CSteamID steamID /*class CSteamID*/ )
		{
			_pi.ISteamUser_EndAuthSession( steamID.Value );
		}
		
		// HAuthTicket
		public HAuthTicket GetAuthSessionTicket( IntPtr pTicket /*void **/, int cbMaxTicket /*int*/, out uint pcbTicket /*uint32 **/ )
		{
			return _pi.ISteamUser_GetAuthSessionTicket( (IntPtr) pTicket, cbMaxTicket, out pcbTicket );
		}
		
		// VoiceResult
		public VoiceResult GetAvailableVoice( out uint pcbCompressed /*uint32 **/, out uint pcbUncompressed /*uint32 **/, uint nUncompressedVoiceDesiredSampleRate /*uint32*/ )
		{
			return _pi.ISteamUser_GetAvailableVoice( out pcbCompressed, out pcbUncompressed, nUncompressedVoiceDesiredSampleRate );
		}
		
		// bool
		public bool GetEncryptedAppTicket( IntPtr pTicket /*void **/, int cbMaxTicket /*int*/, out uint pcbTicket /*uint32 **/ )
		{
			return _pi.ISteamUser_GetEncryptedAppTicket( (IntPtr) pTicket, cbMaxTicket, out pcbTicket );
		}
		
		// int
		public int GetGameBadgeLevel( int nSeries /*int*/, bool bFoil /*bool*/ )
		{
			return _pi.ISteamUser_GetGameBadgeLevel( nSeries, bFoil );
		}
		
		// HSteamUser
		public HSteamUser GetHSteamUser()
		{
			return _pi.ISteamUser_GetHSteamUser();
		}
		
		// int
		public int GetPlayerSteamLevel()
		{
			return _pi.ISteamUser_GetPlayerSteamLevel();
		}
		
		// ulong
		public ulong GetSteamID()
		{
			return _pi.ISteamUser_GetSteamID();
		}
		
		// bool
		// with: Detect_StringFetch True
		public string GetUserDataFolder()
		{
			bool bSuccess = default( bool );
			System.Text.StringBuilder pchBuffer_sb = new System.Text.StringBuilder( 4096 );
			int cubBuffer = 4096;
			bSuccess = _pi.ISteamUser_GetUserDataFolder( pchBuffer_sb, cubBuffer );
			if ( !bSuccess ) return null;
			return pchBuffer_sb.ToString();
		}
		
		// VoiceResult
		public VoiceResult GetVoice( bool bWantCompressed /*bool*/, IntPtr pDestBuffer /*void **/, uint cbDestBufferSize /*uint32*/, out uint nBytesWritten /*uint32 **/, bool bWantUncompressed /*bool*/, IntPtr pUncompressedDestBuffer /*void **/, uint cbUncompressedDestBufferSize /*uint32*/, out uint nUncompressBytesWritten /*uint32 **/, uint nUncompressedVoiceDesiredSampleRate /*uint32*/ )
		{
			return _pi.ISteamUser_GetVoice( bWantCompressed, (IntPtr) pDestBuffer, cbDestBufferSize, out nBytesWritten, bWantUncompressed, (IntPtr) pUncompressedDestBuffer, cbUncompressedDestBufferSize, out nUncompressBytesWritten, nUncompressedVoiceDesiredSampleRate );
		}
		
		// uint
		public uint GetVoiceOptimalSampleRate()
		{
			return _pi.ISteamUser_GetVoiceOptimalSampleRate();
		}
		
		// int
		public int InitiateGameConnection( IntPtr pAuthBlob /*void **/, int cbMaxAuthBlob /*int*/, CSteamID steamIDGameServer /*class CSteamID*/, uint unIPServer /*uint32*/, ushort usPortServer /*uint16*/, bool bSecure /*bool*/ )
		{
			return _pi.ISteamUser_InitiateGameConnection( (IntPtr) pAuthBlob, cbMaxAuthBlob, steamIDGameServer.Value, unIPServer, usPortServer, bSecure );
		}
		
		// SteamAPICall_t
		public SteamAPICall_t RequestEncryptedAppTicket( IntPtr pDataToInclude /*void **/, int cbDataToInclude /*int*/ )
		{
			return _pi.ISteamUser_RequestEncryptedAppTicket( (IntPtr) pDataToInclude, cbDataToInclude );
		}
		
		// SteamAPICall_t
		public SteamAPICall_t RequestStoreAuthURL( string pchRedirectURL /*const char **/ )
		{
			return _pi.ISteamUser_RequestStoreAuthURL( pchRedirectURL );
		}
		
		// void
		public void StartVoiceRecording()
		{
			_pi.ISteamUser_StartVoiceRecording();
		}
		
		// void
		public void StopVoiceRecording()
		{
			_pi.ISteamUser_StopVoiceRecording();
		}
		
		// void
		public void TerminateGameConnection( uint unIPServer /*uint32*/, ushort usPortServer /*uint16*/ )
		{
			_pi.ISteamUser_TerminateGameConnection( unIPServer, usPortServer );
		}
		
		// void
		public void TrackAppUsageEvent( CGameID gameID /*class CGameID*/, int eAppUsageEvent /*int*/, string pchExtraInfo /*const char **/ )
		{
			_pi.ISteamUser_TrackAppUsageEvent( gameID.Value, eAppUsageEvent, pchExtraInfo );
		}
		
		// UserHasLicenseForAppResult
		public UserHasLicenseForAppResult UserHasLicenseForApp( CSteamID steamID /*class CSteamID*/, AppId_t appID /*AppId_t*/ )
		{
			return _pi.ISteamUser_UserHasLicenseForApp( steamID.Value, appID.Value );
		}
		
	}
}
