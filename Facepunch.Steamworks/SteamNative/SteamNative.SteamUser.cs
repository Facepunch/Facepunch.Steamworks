using System;
using System.Runtime.InteropServices;

namespace SteamNative
{
	public unsafe class SteamUser
	{
		internal IntPtr _ptr;
		
		public SteamUser( IntPtr pointer )
		{
			_ptr = pointer;
		}
		
		
		// void
		public void AdvertiseGame( CSteamID steamIDGameServer /*class CSteamID*/, uint unIPServer /*uint32*/, ushort usPortServer /*uint16*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) Platform.Win32.ISteamUser.AdvertiseGame( _ptr, steamIDGameServer, unIPServer, usPortServer );
			else Platform.Win64.ISteamUser.AdvertiseGame( _ptr, steamIDGameServer, unIPServer, usPortServer );
		}
		
		// BeginAuthSessionResult
		public BeginAuthSessionResult BeginAuthSession( IntPtr pAuthTicket /*const void **/, int cbAuthTicket /*int*/, CSteamID steamID /*class CSteamID*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamUser.BeginAuthSession( _ptr, (IntPtr) pAuthTicket, cbAuthTicket, steamID );
			else return Platform.Win64.ISteamUser.BeginAuthSession( _ptr, (IntPtr) pAuthTicket, cbAuthTicket, steamID );
		}
		
		// bool
		public bool BIsBehindNAT()
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamUser.BIsBehindNAT( _ptr );
			else return Platform.Win64.ISteamUser.BIsBehindNAT( _ptr );
		}
		
		// bool
		public bool BIsPhoneVerified()
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamUser.BIsPhoneVerified( _ptr );
			else return Platform.Win64.ISteamUser.BIsPhoneVerified( _ptr );
		}
		
		// bool
		public bool BIsTwoFactorEnabled()
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamUser.BIsTwoFactorEnabled( _ptr );
			else return Platform.Win64.ISteamUser.BIsTwoFactorEnabled( _ptr );
		}
		
		// bool
		public bool BLoggedOn()
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamUser.BLoggedOn( _ptr );
			else return Platform.Win64.ISteamUser.BLoggedOn( _ptr );
		}
		
		// void
		public void CancelAuthTicket( HAuthTicket hAuthTicket /*HAuthTicket*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) Platform.Win32.ISteamUser.CancelAuthTicket( _ptr, hAuthTicket );
			else Platform.Win64.ISteamUser.CancelAuthTicket( _ptr, hAuthTicket );
		}
		
		// VoiceResult
		public VoiceResult DecompressVoice( IntPtr pCompressed /*const void **/, uint cbCompressed /*uint32*/, IntPtr pDestBuffer /*void **/, uint cbDestBufferSize /*uint32*/, out uint nBytesWritten /*uint32 **/, uint nDesiredSampleRate /*uint32*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamUser.DecompressVoice( _ptr, (IntPtr) pCompressed, cbCompressed, (IntPtr) pDestBuffer, cbDestBufferSize, out nBytesWritten, nDesiredSampleRate );
			else return Platform.Win64.ISteamUser.DecompressVoice( _ptr, (IntPtr) pCompressed, cbCompressed, (IntPtr) pDestBuffer, cbDestBufferSize, out nBytesWritten, nDesiredSampleRate );
		}
		
		// void
		public void EndAuthSession( CSteamID steamID /*class CSteamID*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) Platform.Win32.ISteamUser.EndAuthSession( _ptr, steamID );
			else Platform.Win64.ISteamUser.EndAuthSession( _ptr, steamID );
		}
		
		// HAuthTicket
		public HAuthTicket GetAuthSessionTicket( IntPtr pTicket /*void **/, int cbMaxTicket /*int*/, out uint pcbTicket /*uint32 **/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamUser.GetAuthSessionTicket( _ptr, (IntPtr) pTicket, cbMaxTicket, out pcbTicket );
			else return Platform.Win64.ISteamUser.GetAuthSessionTicket( _ptr, (IntPtr) pTicket, cbMaxTicket, out pcbTicket );
		}
		
		// VoiceResult
		public VoiceResult GetAvailableVoice( out uint pcbCompressed /*uint32 **/, out uint pcbUncompressed /*uint32 **/, uint nUncompressedVoiceDesiredSampleRate /*uint32*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamUser.GetAvailableVoice( _ptr, out pcbCompressed, out pcbUncompressed, nUncompressedVoiceDesiredSampleRate );
			else return Platform.Win64.ISteamUser.GetAvailableVoice( _ptr, out pcbCompressed, out pcbUncompressed, nUncompressedVoiceDesiredSampleRate );
		}
		
		// bool
		public bool GetEncryptedAppTicket( IntPtr pTicket /*void **/, int cbMaxTicket /*int*/, out uint pcbTicket /*uint32 **/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamUser.GetEncryptedAppTicket( _ptr, (IntPtr) pTicket, cbMaxTicket, out pcbTicket );
			else return Platform.Win64.ISteamUser.GetEncryptedAppTicket( _ptr, (IntPtr) pTicket, cbMaxTicket, out pcbTicket );
		}
		
		// int
		public int GetGameBadgeLevel( int nSeries /*int*/, bool bFoil /*bool*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamUser.GetGameBadgeLevel( _ptr, nSeries, bFoil );
			else return Platform.Win64.ISteamUser.GetGameBadgeLevel( _ptr, nSeries, bFoil );
		}
		
		// HSteamUser
		public HSteamUser GetHSteamUser()
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamUser.GetHSteamUser( _ptr );
			else return Platform.Win64.ISteamUser.GetHSteamUser( _ptr );
		}
		
		// int
		public int GetPlayerSteamLevel()
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamUser.GetPlayerSteamLevel( _ptr );
			else return Platform.Win64.ISteamUser.GetPlayerSteamLevel( _ptr );
		}
		
		// ulong
		public ulong GetSteamID()
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamUser.GetSteamID( _ptr );
			else return Platform.Win64.ISteamUser.GetSteamID( _ptr );
		}
		
		// bool
		// with: Detect_StringFetch True
		public string GetUserDataFolder()
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			bool bSuccess = default( bool );
			System.Text.StringBuilder pchBuffer_sb = new System.Text.StringBuilder( 4096 );
			int cubBuffer = 4096;
			if ( Platform.IsWindows32 ) bSuccess = Platform.Win32.ISteamUser.GetUserDataFolder( _ptr, pchBuffer_sb, cubBuffer );
			else bSuccess = Platform.Win64.ISteamUser.GetUserDataFolder( _ptr, pchBuffer_sb, cubBuffer );
			if ( !bSuccess ) return null;
			return pchBuffer_sb.ToString();
		}
		
		// VoiceResult
		public VoiceResult GetVoice( bool bWantCompressed /*bool*/, IntPtr pDestBuffer /*void **/, uint cbDestBufferSize /*uint32*/, out uint nBytesWritten /*uint32 **/, bool bWantUncompressed /*bool*/, IntPtr pUncompressedDestBuffer /*void **/, uint cbUncompressedDestBufferSize /*uint32*/, out uint nUncompressBytesWritten /*uint32 **/, uint nUncompressedVoiceDesiredSampleRate /*uint32*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamUser.GetVoice( _ptr, bWantCompressed, (IntPtr) pDestBuffer, cbDestBufferSize, out nBytesWritten, bWantUncompressed, (IntPtr) pUncompressedDestBuffer, cbUncompressedDestBufferSize, out nUncompressBytesWritten, nUncompressedVoiceDesiredSampleRate );
			else return Platform.Win64.ISteamUser.GetVoice( _ptr, bWantCompressed, (IntPtr) pDestBuffer, cbDestBufferSize, out nBytesWritten, bWantUncompressed, (IntPtr) pUncompressedDestBuffer, cbUncompressedDestBufferSize, out nUncompressBytesWritten, nUncompressedVoiceDesiredSampleRate );
		}
		
		// uint
		public uint GetVoiceOptimalSampleRate()
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamUser.GetVoiceOptimalSampleRate( _ptr );
			else return Platform.Win64.ISteamUser.GetVoiceOptimalSampleRate( _ptr );
		}
		
		// int
		public int InitiateGameConnection( IntPtr pAuthBlob /*void **/, int cbMaxAuthBlob /*int*/, CSteamID steamIDGameServer /*class CSteamID*/, uint unIPServer /*uint32*/, ushort usPortServer /*uint16*/, bool bSecure /*bool*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamUser.InitiateGameConnection( _ptr, (IntPtr) pAuthBlob, cbMaxAuthBlob, steamIDGameServer, unIPServer, usPortServer, bSecure );
			else return Platform.Win64.ISteamUser.InitiateGameConnection( _ptr, (IntPtr) pAuthBlob, cbMaxAuthBlob, steamIDGameServer, unIPServer, usPortServer, bSecure );
		}
		
		// SteamAPICall_t
		public SteamAPICall_t RequestEncryptedAppTicket( IntPtr pDataToInclude /*void **/, int cbDataToInclude /*int*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamUser.RequestEncryptedAppTicket( _ptr, (IntPtr) pDataToInclude, cbDataToInclude );
			else return Platform.Win64.ISteamUser.RequestEncryptedAppTicket( _ptr, (IntPtr) pDataToInclude, cbDataToInclude );
		}
		
		// SteamAPICall_t
		public SteamAPICall_t RequestStoreAuthURL( string pchRedirectURL /*const char **/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamUser.RequestStoreAuthURL( _ptr, pchRedirectURL );
			else return Platform.Win64.ISteamUser.RequestStoreAuthURL( _ptr, pchRedirectURL );
		}
		
		// void
		public void StartVoiceRecording()
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) Platform.Win32.ISteamUser.StartVoiceRecording( _ptr );
			else Platform.Win64.ISteamUser.StartVoiceRecording( _ptr );
		}
		
		// void
		public void StopVoiceRecording()
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) Platform.Win32.ISteamUser.StopVoiceRecording( _ptr );
			else Platform.Win64.ISteamUser.StopVoiceRecording( _ptr );
		}
		
		// void
		public void TerminateGameConnection( uint unIPServer /*uint32*/, ushort usPortServer /*uint16*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) Platform.Win32.ISteamUser.TerminateGameConnection( _ptr, unIPServer, usPortServer );
			else Platform.Win64.ISteamUser.TerminateGameConnection( _ptr, unIPServer, usPortServer );
		}
		
		// void
		public void TrackAppUsageEvent( CGameID gameID /*class CGameID*/, int eAppUsageEvent /*int*/, string pchExtraInfo /*const char **/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) Platform.Win32.ISteamUser.TrackAppUsageEvent( _ptr, gameID, eAppUsageEvent, pchExtraInfo );
			else Platform.Win64.ISteamUser.TrackAppUsageEvent( _ptr, gameID, eAppUsageEvent, pchExtraInfo );
		}
		
		// UserHasLicenseForAppResult
		public UserHasLicenseForAppResult UserHasLicenseForApp( CSteamID steamID /*class CSteamID*/, AppId_t appID /*AppId_t*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamUser.UserHasLicenseForApp( _ptr, steamID, appID );
			else return Platform.Win64.ISteamUser.UserHasLicenseForApp( _ptr, steamID, appID );
		}
		
	}
}
