using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Steamworks.Data;


namespace Steamworks
{
	internal class ISteamUser : SteamInterface
	{
		public ISteamUser( bool server = false ) : base( server )
		{
		}
		
		public override string InterfaceName => "SteamUser020";
		
		public override void InitInternals()
		{
			_GetHSteamUser = Marshal.GetDelegateForFunctionPointer<FGetHSteamUser>( Marshal.ReadIntPtr( VTable, 0) );
			_BLoggedOn = Marshal.GetDelegateForFunctionPointer<FBLoggedOn>( Marshal.ReadIntPtr( VTable, 8) );
			_GetSteamID = Marshal.GetDelegateForFunctionPointer<FGetSteamID>( Marshal.ReadIntPtr( VTable, 16) );
			_InitiateGameConnection = Marshal.GetDelegateForFunctionPointer<FInitiateGameConnection>( Marshal.ReadIntPtr( VTable, 24) );
			_TerminateGameConnection = Marshal.GetDelegateForFunctionPointer<FTerminateGameConnection>( Marshal.ReadIntPtr( VTable, 32) );
			_TrackAppUsageEvent = Marshal.GetDelegateForFunctionPointer<FTrackAppUsageEvent>( Marshal.ReadIntPtr( VTable, 40) );
			_GetUserDataFolder = Marshal.GetDelegateForFunctionPointer<FGetUserDataFolder>( Marshal.ReadIntPtr( VTable, 48) );
			_StartVoiceRecording = Marshal.GetDelegateForFunctionPointer<FStartVoiceRecording>( Marshal.ReadIntPtr( VTable, 56) );
			_StopVoiceRecording = Marshal.GetDelegateForFunctionPointer<FStopVoiceRecording>( Marshal.ReadIntPtr( VTable, 64) );
			_GetAvailableVoice = Marshal.GetDelegateForFunctionPointer<FGetAvailableVoice>( Marshal.ReadIntPtr( VTable, 72) );
			_GetVoice = Marshal.GetDelegateForFunctionPointer<FGetVoice>( Marshal.ReadIntPtr( VTable, 80) );
			_DecompressVoice = Marshal.GetDelegateForFunctionPointer<FDecompressVoice>( Marshal.ReadIntPtr( VTable, 88) );
			_GetVoiceOptimalSampleRate = Marshal.GetDelegateForFunctionPointer<FGetVoiceOptimalSampleRate>( Marshal.ReadIntPtr( VTable, 96) );
			_GetAuthSessionTicket = Marshal.GetDelegateForFunctionPointer<FGetAuthSessionTicket>( Marshal.ReadIntPtr( VTable, 104) );
			_BeginAuthSession = Marshal.GetDelegateForFunctionPointer<FBeginAuthSession>( Marshal.ReadIntPtr( VTable, 112) );
			_EndAuthSession = Marshal.GetDelegateForFunctionPointer<FEndAuthSession>( Marshal.ReadIntPtr( VTable, 120) );
			_CancelAuthTicket = Marshal.GetDelegateForFunctionPointer<FCancelAuthTicket>( Marshal.ReadIntPtr( VTable, 128) );
			_UserHasLicenseForApp = Marshal.GetDelegateForFunctionPointer<FUserHasLicenseForApp>( Marshal.ReadIntPtr( VTable, 136) );
			_BIsBehindNAT = Marshal.GetDelegateForFunctionPointer<FBIsBehindNAT>( Marshal.ReadIntPtr( VTable, 144) );
			_AdvertiseGame = Marshal.GetDelegateForFunctionPointer<FAdvertiseGame>( Marshal.ReadIntPtr( VTable, 152) );
			_RequestEncryptedAppTicket = Marshal.GetDelegateForFunctionPointer<FRequestEncryptedAppTicket>( Marshal.ReadIntPtr( VTable, 160) );
			_GetEncryptedAppTicket = Marshal.GetDelegateForFunctionPointer<FGetEncryptedAppTicket>( Marshal.ReadIntPtr( VTable, 168) );
			_GetGameBadgeLevel = Marshal.GetDelegateForFunctionPointer<FGetGameBadgeLevel>( Marshal.ReadIntPtr( VTable, 176) );
			_GetPlayerSteamLevel = Marshal.GetDelegateForFunctionPointer<FGetPlayerSteamLevel>( Marshal.ReadIntPtr( VTable, 184) );
			_RequestStoreAuthURL = Marshal.GetDelegateForFunctionPointer<FRequestStoreAuthURL>( Marshal.ReadIntPtr( VTable, 192) );
			_BIsPhoneVerified = Marshal.GetDelegateForFunctionPointer<FBIsPhoneVerified>( Marshal.ReadIntPtr( VTable, 200) );
			_BIsTwoFactorEnabled = Marshal.GetDelegateForFunctionPointer<FBIsTwoFactorEnabled>( Marshal.ReadIntPtr( VTable, 208) );
			_BIsPhoneIdentifying = Marshal.GetDelegateForFunctionPointer<FBIsPhoneIdentifying>( Marshal.ReadIntPtr( VTable, 216) );
			_BIsPhoneRequiringVerification = Marshal.GetDelegateForFunctionPointer<FBIsPhoneRequiringVerification>( Marshal.ReadIntPtr( VTable, 224) );
			_GetMarketEligibility = Marshal.GetDelegateForFunctionPointer<FGetMarketEligibility>( Marshal.ReadIntPtr( VTable, 232) );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate HSteamUser FGetHSteamUser( IntPtr self );
		private FGetHSteamUser _GetHSteamUser;
		
		#endregion
		internal HSteamUser GetHSteamUser()
		{
			return _GetHSteamUser( Self );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FBLoggedOn( IntPtr self );
		private FBLoggedOn _BLoggedOn;
		
		#endregion
		internal bool BLoggedOn()
		{
			return _BLoggedOn( Self );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate void FGetSteamID( IntPtr self, ref SteamId retVal );
		private FGetSteamID _GetSteamID;
		
		#endregion
		internal SteamId GetSteamID()
		{
			var retVal = default( SteamId );
			_GetSteamID( Self, ref retVal );
			return retVal;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate int FInitiateGameConnection( IntPtr self, IntPtr pAuthBlob, int cbMaxAuthBlob, SteamId steamIDGameServer, uint unIPServer, ushort usPortServer, [MarshalAs( UnmanagedType.U1 )] bool bSecure );
		private FInitiateGameConnection _InitiateGameConnection;
		
		#endregion
		internal int InitiateGameConnection( IntPtr pAuthBlob, int cbMaxAuthBlob, SteamId steamIDGameServer, uint unIPServer, ushort usPortServer, [MarshalAs( UnmanagedType.U1 )] bool bSecure )
		{
			return _InitiateGameConnection( Self, pAuthBlob, cbMaxAuthBlob, steamIDGameServer, unIPServer, usPortServer, bSecure );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate void FTerminateGameConnection( IntPtr self, uint unIPServer, ushort usPortServer );
		private FTerminateGameConnection _TerminateGameConnection;
		
		#endregion
		internal void TerminateGameConnection( uint unIPServer, ushort usPortServer )
		{
			_TerminateGameConnection( Self, unIPServer, usPortServer );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate void FTrackAppUsageEvent( IntPtr self, GameId gameID, int eAppUsageEvent, string pchExtraInfo );
		private FTrackAppUsageEvent _TrackAppUsageEvent;
		
		#endregion
		internal void TrackAppUsageEvent( GameId gameID, int eAppUsageEvent, string pchExtraInfo )
		{
			_TrackAppUsageEvent( Self, gameID, eAppUsageEvent, pchExtraInfo );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FGetUserDataFolder( IntPtr self, StringBuilder pchBuffer, int cubBuffer );
		private FGetUserDataFolder _GetUserDataFolder;
		
		#endregion
		internal bool GetUserDataFolder( StringBuilder pchBuffer, int cubBuffer )
		{
			return _GetUserDataFolder( Self, pchBuffer, cubBuffer );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate void FStartVoiceRecording( IntPtr self );
		private FStartVoiceRecording _StartVoiceRecording;
		
		#endregion
		internal void StartVoiceRecording()
		{
			_StartVoiceRecording( Self );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate void FStopVoiceRecording( IntPtr self );
		private FStopVoiceRecording _StopVoiceRecording;
		
		#endregion
		internal void StopVoiceRecording()
		{
			_StopVoiceRecording( Self );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate VoiceResult FGetAvailableVoice( IntPtr self, ref uint pcbCompressed, ref uint pcbUncompressed_Deprecated, uint nUncompressedVoiceDesiredSampleRate_Deprecated );
		private FGetAvailableVoice _GetAvailableVoice;
		
		#endregion
		internal VoiceResult GetAvailableVoice( ref uint pcbCompressed, ref uint pcbUncompressed_Deprecated, uint nUncompressedVoiceDesiredSampleRate_Deprecated )
		{
			return _GetAvailableVoice( Self, ref pcbCompressed, ref pcbUncompressed_Deprecated, nUncompressedVoiceDesiredSampleRate_Deprecated );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate VoiceResult FGetVoice( IntPtr self, [MarshalAs( UnmanagedType.U1 )] bool bWantCompressed, IntPtr pDestBuffer, uint cbDestBufferSize, ref uint nBytesWritten, [MarshalAs( UnmanagedType.U1 )] bool bWantUncompressed_Deprecated, IntPtr pUncompressedDestBuffer_Deprecated, uint cbUncompressedDestBufferSize_Deprecated, ref uint nUncompressBytesWritten_Deprecated, uint nUncompressedVoiceDesiredSampleRate_Deprecated );
		private FGetVoice _GetVoice;
		
		#endregion
		internal VoiceResult GetVoice( [MarshalAs( UnmanagedType.U1 )] bool bWantCompressed, IntPtr pDestBuffer, uint cbDestBufferSize, ref uint nBytesWritten, [MarshalAs( UnmanagedType.U1 )] bool bWantUncompressed_Deprecated, IntPtr pUncompressedDestBuffer_Deprecated, uint cbUncompressedDestBufferSize_Deprecated, ref uint nUncompressBytesWritten_Deprecated, uint nUncompressedVoiceDesiredSampleRate_Deprecated )
		{
			return _GetVoice( Self, bWantCompressed, pDestBuffer, cbDestBufferSize, ref nBytesWritten, bWantUncompressed_Deprecated, pUncompressedDestBuffer_Deprecated, cbUncompressedDestBufferSize_Deprecated, ref nUncompressBytesWritten_Deprecated, nUncompressedVoiceDesiredSampleRate_Deprecated );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate VoiceResult FDecompressVoice( IntPtr self, IntPtr pCompressed, uint cbCompressed, IntPtr pDestBuffer, uint cbDestBufferSize, ref uint nBytesWritten, uint nDesiredSampleRate );
		private FDecompressVoice _DecompressVoice;
		
		#endregion
		internal VoiceResult DecompressVoice( IntPtr pCompressed, uint cbCompressed, IntPtr pDestBuffer, uint cbDestBufferSize, ref uint nBytesWritten, uint nDesiredSampleRate )
		{
			return _DecompressVoice( Self, pCompressed, cbCompressed, pDestBuffer, cbDestBufferSize, ref nBytesWritten, nDesiredSampleRate );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate uint FGetVoiceOptimalSampleRate( IntPtr self );
		private FGetVoiceOptimalSampleRate _GetVoiceOptimalSampleRate;
		
		#endregion
		internal uint GetVoiceOptimalSampleRate()
		{
			return _GetVoiceOptimalSampleRate( Self );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate HAuthTicket FGetAuthSessionTicket( IntPtr self, IntPtr pTicket, int cbMaxTicket, ref uint pcbTicket );
		private FGetAuthSessionTicket _GetAuthSessionTicket;
		
		#endregion
		internal HAuthTicket GetAuthSessionTicket( IntPtr pTicket, int cbMaxTicket, ref uint pcbTicket )
		{
			return _GetAuthSessionTicket( Self, pTicket, cbMaxTicket, ref pcbTicket );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate BeginAuthResult FBeginAuthSession( IntPtr self, IntPtr pAuthTicket, int cbAuthTicket, SteamId steamID );
		private FBeginAuthSession _BeginAuthSession;
		
		#endregion
		internal BeginAuthResult BeginAuthSession( IntPtr pAuthTicket, int cbAuthTicket, SteamId steamID )
		{
			return _BeginAuthSession( Self, pAuthTicket, cbAuthTicket, steamID );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate void FEndAuthSession( IntPtr self, SteamId steamID );
		private FEndAuthSession _EndAuthSession;
		
		#endregion
		internal void EndAuthSession( SteamId steamID )
		{
			_EndAuthSession( Self, steamID );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate void FCancelAuthTicket( IntPtr self, HAuthTicket hAuthTicket );
		private FCancelAuthTicket _CancelAuthTicket;
		
		#endregion
		internal void CancelAuthTicket( HAuthTicket hAuthTicket )
		{
			_CancelAuthTicket( Self, hAuthTicket );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate UserHasLicenseForAppResult FUserHasLicenseForApp( IntPtr self, SteamId steamID, AppId appID );
		private FUserHasLicenseForApp _UserHasLicenseForApp;
		
		#endregion
		internal UserHasLicenseForAppResult UserHasLicenseForApp( SteamId steamID, AppId appID )
		{
			return _UserHasLicenseForApp( Self, steamID, appID );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FBIsBehindNAT( IntPtr self );
		private FBIsBehindNAT _BIsBehindNAT;
		
		#endregion
		internal bool BIsBehindNAT()
		{
			return _BIsBehindNAT( Self );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate void FAdvertiseGame( IntPtr self, SteamId steamIDGameServer, uint unIPServer, ushort usPortServer );
		private FAdvertiseGame _AdvertiseGame;
		
		#endregion
		internal void AdvertiseGame( SteamId steamIDGameServer, uint unIPServer, ushort usPortServer )
		{
			_AdvertiseGame( Self, steamIDGameServer, unIPServer, usPortServer );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate SteamAPICall_t FRequestEncryptedAppTicket( IntPtr self, IntPtr pDataToInclude, int cbDataToInclude );
		private FRequestEncryptedAppTicket _RequestEncryptedAppTicket;
		
		#endregion
		internal async Task<EncryptedAppTicketResponse_t?> RequestEncryptedAppTicket( IntPtr pDataToInclude, int cbDataToInclude )
		{
			return await (new Result<EncryptedAppTicketResponse_t>( _RequestEncryptedAppTicket( Self, pDataToInclude, cbDataToInclude ) )).GetResult();
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FGetEncryptedAppTicket( IntPtr self, IntPtr pTicket, int cbMaxTicket, ref uint pcbTicket );
		private FGetEncryptedAppTicket _GetEncryptedAppTicket;
		
		#endregion
		internal bool GetEncryptedAppTicket( IntPtr pTicket, int cbMaxTicket, ref uint pcbTicket )
		{
			return _GetEncryptedAppTicket( Self, pTicket, cbMaxTicket, ref pcbTicket );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate int FGetGameBadgeLevel( IntPtr self, int nSeries, [MarshalAs( UnmanagedType.U1 )] bool bFoil );
		private FGetGameBadgeLevel _GetGameBadgeLevel;
		
		#endregion
		internal int GetGameBadgeLevel( int nSeries, [MarshalAs( UnmanagedType.U1 )] bool bFoil )
		{
			return _GetGameBadgeLevel( Self, nSeries, bFoil );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate int FGetPlayerSteamLevel( IntPtr self );
		private FGetPlayerSteamLevel _GetPlayerSteamLevel;
		
		#endregion
		internal int GetPlayerSteamLevel()
		{
			return _GetPlayerSteamLevel( Self );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate SteamAPICall_t FRequestStoreAuthURL( IntPtr self, string pchRedirectURL );
		private FRequestStoreAuthURL _RequestStoreAuthURL;
		
		#endregion
		internal async Task<StoreAuthURLResponse_t?> RequestStoreAuthURL( string pchRedirectURL )
		{
			return await (new Result<StoreAuthURLResponse_t>( _RequestStoreAuthURL( Self, pchRedirectURL ) )).GetResult();
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FBIsPhoneVerified( IntPtr self );
		private FBIsPhoneVerified _BIsPhoneVerified;
		
		#endregion
		internal bool BIsPhoneVerified()
		{
			return _BIsPhoneVerified( Self );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FBIsTwoFactorEnabled( IntPtr self );
		private FBIsTwoFactorEnabled _BIsTwoFactorEnabled;
		
		#endregion
		internal bool BIsTwoFactorEnabled()
		{
			return _BIsTwoFactorEnabled( Self );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FBIsPhoneIdentifying( IntPtr self );
		private FBIsPhoneIdentifying _BIsPhoneIdentifying;
		
		#endregion
		internal bool BIsPhoneIdentifying()
		{
			return _BIsPhoneIdentifying( Self );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FBIsPhoneRequiringVerification( IntPtr self );
		private FBIsPhoneRequiringVerification _BIsPhoneRequiringVerification;
		
		#endregion
		internal bool BIsPhoneRequiringVerification()
		{
			return _BIsPhoneRequiringVerification( Self );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate SteamAPICall_t FGetMarketEligibility( IntPtr self );
		private FGetMarketEligibility _GetMarketEligibility;
		
		#endregion
		internal async Task<MarketEligibilityResponse_t?> GetMarketEligibility()
		{
			return await (new Result<MarketEligibilityResponse_t>( _GetMarketEligibility( Self ) )).GetResult();
		}
		
	}
}
