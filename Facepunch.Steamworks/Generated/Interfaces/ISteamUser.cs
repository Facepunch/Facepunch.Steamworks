using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;


namespace Steamworks.Internal
{
	public class ISteamUser : BaseSteamInterface
	{
		public ISteamUser( bool server = false ) : base( server )
		{
		}
		
		public override string InterfaceName => "SteamUser020";
		
		public override void InitInternals()
		{
			GetHSteamUserDelegatePointer = Marshal.GetDelegateForFunctionPointer<GetHSteamUserDelegate>( Marshal.ReadIntPtr( VTable, 0) );
			BLoggedOnDelegatePointer = Marshal.GetDelegateForFunctionPointer<BLoggedOnDelegate>( Marshal.ReadIntPtr( VTable, 8) );
			GetSteamIDDelegatePointer = Marshal.GetDelegateForFunctionPointer<GetSteamIDDelegate>( Marshal.ReadIntPtr( VTable, 16) );
			InitiateGameConnectionDelegatePointer = Marshal.GetDelegateForFunctionPointer<InitiateGameConnectionDelegate>( Marshal.ReadIntPtr( VTable, 24) );
			TerminateGameConnectionDelegatePointer = Marshal.GetDelegateForFunctionPointer<TerminateGameConnectionDelegate>( Marshal.ReadIntPtr( VTable, 32) );
			TrackAppUsageEventDelegatePointer = Marshal.GetDelegateForFunctionPointer<TrackAppUsageEventDelegate>( Marshal.ReadIntPtr( VTable, 40) );
			GetUserDataFolderDelegatePointer = Marshal.GetDelegateForFunctionPointer<GetUserDataFolderDelegate>( Marshal.ReadIntPtr( VTable, 48) );
			StartVoiceRecordingDelegatePointer = Marshal.GetDelegateForFunctionPointer<StartVoiceRecordingDelegate>( Marshal.ReadIntPtr( VTable, 56) );
			StopVoiceRecordingDelegatePointer = Marshal.GetDelegateForFunctionPointer<StopVoiceRecordingDelegate>( Marshal.ReadIntPtr( VTable, 64) );
			GetAvailableVoiceDelegatePointer = Marshal.GetDelegateForFunctionPointer<GetAvailableVoiceDelegate>( Marshal.ReadIntPtr( VTable, 72) );
			GetVoiceDelegatePointer = Marshal.GetDelegateForFunctionPointer<GetVoiceDelegate>( Marshal.ReadIntPtr( VTable, 80) );
			DecompressVoiceDelegatePointer = Marshal.GetDelegateForFunctionPointer<DecompressVoiceDelegate>( Marshal.ReadIntPtr( VTable, 88) );
			GetVoiceOptimalSampleRateDelegatePointer = Marshal.GetDelegateForFunctionPointer<GetVoiceOptimalSampleRateDelegate>( Marshal.ReadIntPtr( VTable, 96) );
			GetAuthSessionTicketDelegatePointer = Marshal.GetDelegateForFunctionPointer<GetAuthSessionTicketDelegate>( Marshal.ReadIntPtr( VTable, 104) );
			BeginAuthSessionDelegatePointer = Marshal.GetDelegateForFunctionPointer<BeginAuthSessionDelegate>( Marshal.ReadIntPtr( VTable, 112) );
			EndAuthSessionDelegatePointer = Marshal.GetDelegateForFunctionPointer<EndAuthSessionDelegate>( Marshal.ReadIntPtr( VTable, 120) );
			CancelAuthTicketDelegatePointer = Marshal.GetDelegateForFunctionPointer<CancelAuthTicketDelegate>( Marshal.ReadIntPtr( VTable, 128) );
			UserHasLicenseForAppDelegatePointer = Marshal.GetDelegateForFunctionPointer<UserHasLicenseForAppDelegate>( Marshal.ReadIntPtr( VTable, 136) );
			BIsBehindNATDelegatePointer = Marshal.GetDelegateForFunctionPointer<BIsBehindNATDelegate>( Marshal.ReadIntPtr( VTable, 144) );
			AdvertiseGameDelegatePointer = Marshal.GetDelegateForFunctionPointer<AdvertiseGameDelegate>( Marshal.ReadIntPtr( VTable, 152) );
			RequestEncryptedAppTicketDelegatePointer = Marshal.GetDelegateForFunctionPointer<RequestEncryptedAppTicketDelegate>( Marshal.ReadIntPtr( VTable, 160) );
			GetEncryptedAppTicketDelegatePointer = Marshal.GetDelegateForFunctionPointer<GetEncryptedAppTicketDelegate>( Marshal.ReadIntPtr( VTable, 168) );
			GetGameBadgeLevelDelegatePointer = Marshal.GetDelegateForFunctionPointer<GetGameBadgeLevelDelegate>( Marshal.ReadIntPtr( VTable, 176) );
			GetPlayerSteamLevelDelegatePointer = Marshal.GetDelegateForFunctionPointer<GetPlayerSteamLevelDelegate>( Marshal.ReadIntPtr( VTable, 184) );
			RequestStoreAuthURLDelegatePointer = Marshal.GetDelegateForFunctionPointer<RequestStoreAuthURLDelegate>( Marshal.ReadIntPtr( VTable, 192) );
			BIsPhoneVerifiedDelegatePointer = Marshal.GetDelegateForFunctionPointer<BIsPhoneVerifiedDelegate>( Marshal.ReadIntPtr( VTable, 200) );
			BIsTwoFactorEnabledDelegatePointer = Marshal.GetDelegateForFunctionPointer<BIsTwoFactorEnabledDelegate>( Marshal.ReadIntPtr( VTable, 208) );
			BIsPhoneIdentifyingDelegatePointer = Marshal.GetDelegateForFunctionPointer<BIsPhoneIdentifyingDelegate>( Marshal.ReadIntPtr( VTable, 216) );
			BIsPhoneRequiringVerificationDelegatePointer = Marshal.GetDelegateForFunctionPointer<BIsPhoneRequiringVerificationDelegate>( Marshal.ReadIntPtr( VTable, 224) );
			GetMarketEligibilityDelegatePointer = Marshal.GetDelegateForFunctionPointer<GetMarketEligibilityDelegate>( Marshal.ReadIntPtr( VTable, 232) );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		public delegate HSteamUser GetHSteamUserDelegate( IntPtr self );
		private GetHSteamUserDelegate GetHSteamUserDelegatePointer;
		
		#endregion
		public HSteamUser GetHSteamUser()
		{
			return GetHSteamUserDelegatePointer( Self );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		public delegate bool BLoggedOnDelegate( IntPtr self );
		private BLoggedOnDelegate BLoggedOnDelegatePointer;
		
		#endregion
		public bool BLoggedOn()
		{
			return BLoggedOnDelegatePointer( Self );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		public delegate void GetSteamIDDelegate( IntPtr self, ref SteamId retVal );
		private GetSteamIDDelegate GetSteamIDDelegatePointer;
		
		#endregion
		public SteamId GetSteamID()
		{
			var retVal = default( SteamId );
			GetSteamIDDelegatePointer( Self, ref retVal );
			return retVal;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		public delegate int InitiateGameConnectionDelegate( IntPtr self, IntPtr pAuthBlob, int cbMaxAuthBlob, SteamId steamIDGameServer, uint unIPServer, ushort usPortServer, [MarshalAs( UnmanagedType.U1 )] bool bSecure );
		private InitiateGameConnectionDelegate InitiateGameConnectionDelegatePointer;
		
		#endregion
		public int InitiateGameConnection( IntPtr pAuthBlob, int cbMaxAuthBlob, SteamId steamIDGameServer, uint unIPServer, ushort usPortServer, [MarshalAs( UnmanagedType.U1 )] bool bSecure )
		{
			return InitiateGameConnectionDelegatePointer( Self, pAuthBlob, cbMaxAuthBlob, steamIDGameServer, unIPServer, usPortServer, bSecure );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		public delegate void TerminateGameConnectionDelegate( IntPtr self, uint unIPServer, ushort usPortServer );
		private TerminateGameConnectionDelegate TerminateGameConnectionDelegatePointer;
		
		#endregion
		public void TerminateGameConnection( uint unIPServer, ushort usPortServer )
		{
			TerminateGameConnectionDelegatePointer( Self, unIPServer, usPortServer );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		public delegate void TrackAppUsageEventDelegate( IntPtr self, GameId gameID, int eAppUsageEvent, string pchExtraInfo );
		private TrackAppUsageEventDelegate TrackAppUsageEventDelegatePointer;
		
		#endregion
		public void TrackAppUsageEvent( GameId gameID, int eAppUsageEvent, string pchExtraInfo )
		{
			TrackAppUsageEventDelegatePointer( Self, gameID, eAppUsageEvent, pchExtraInfo );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		public delegate bool GetUserDataFolderDelegate( IntPtr self, StringBuilder pchBuffer, int cubBuffer );
		private GetUserDataFolderDelegate GetUserDataFolderDelegatePointer;
		
		#endregion
		public bool GetUserDataFolder( StringBuilder pchBuffer, int cubBuffer )
		{
			return GetUserDataFolderDelegatePointer( Self, pchBuffer, cubBuffer );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		public delegate void StartVoiceRecordingDelegate( IntPtr self );
		private StartVoiceRecordingDelegate StartVoiceRecordingDelegatePointer;
		
		#endregion
		public void StartVoiceRecording()
		{
			StartVoiceRecordingDelegatePointer( Self );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		public delegate void StopVoiceRecordingDelegate( IntPtr self );
		private StopVoiceRecordingDelegate StopVoiceRecordingDelegatePointer;
		
		#endregion
		public void StopVoiceRecording()
		{
			StopVoiceRecordingDelegatePointer( Self );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		public delegate VoiceResult GetAvailableVoiceDelegate( IntPtr self, ref uint pcbCompressed, ref uint pcbUncompressed_Deprecated, uint nUncompressedVoiceDesiredSampleRate_Deprecated );
		private GetAvailableVoiceDelegate GetAvailableVoiceDelegatePointer;
		
		#endregion
		public VoiceResult GetAvailableVoice( ref uint pcbCompressed, ref uint pcbUncompressed_Deprecated, uint nUncompressedVoiceDesiredSampleRate_Deprecated )
		{
			return GetAvailableVoiceDelegatePointer( Self, ref pcbCompressed, ref pcbUncompressed_Deprecated, nUncompressedVoiceDesiredSampleRate_Deprecated );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		public delegate VoiceResult GetVoiceDelegate( IntPtr self, [MarshalAs( UnmanagedType.U1 )] bool bWantCompressed, IntPtr pDestBuffer, uint cbDestBufferSize, ref uint nBytesWritten, [MarshalAs( UnmanagedType.U1 )] bool bWantUncompressed_Deprecated, IntPtr pUncompressedDestBuffer_Deprecated, uint cbUncompressedDestBufferSize_Deprecated, ref uint nUncompressBytesWritten_Deprecated, uint nUncompressedVoiceDesiredSampleRate_Deprecated );
		private GetVoiceDelegate GetVoiceDelegatePointer;
		
		#endregion
		public VoiceResult GetVoice( [MarshalAs( UnmanagedType.U1 )] bool bWantCompressed, IntPtr pDestBuffer, uint cbDestBufferSize, ref uint nBytesWritten, [MarshalAs( UnmanagedType.U1 )] bool bWantUncompressed_Deprecated, IntPtr pUncompressedDestBuffer_Deprecated, uint cbUncompressedDestBufferSize_Deprecated, ref uint nUncompressBytesWritten_Deprecated, uint nUncompressedVoiceDesiredSampleRate_Deprecated )
		{
			return GetVoiceDelegatePointer( Self, bWantCompressed, pDestBuffer, cbDestBufferSize, ref nBytesWritten, bWantUncompressed_Deprecated, pUncompressedDestBuffer_Deprecated, cbUncompressedDestBufferSize_Deprecated, ref nUncompressBytesWritten_Deprecated, nUncompressedVoiceDesiredSampleRate_Deprecated );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		public delegate VoiceResult DecompressVoiceDelegate( IntPtr self, IntPtr pCompressed, uint cbCompressed, IntPtr pDestBuffer, uint cbDestBufferSize, ref uint nBytesWritten, uint nDesiredSampleRate );
		private DecompressVoiceDelegate DecompressVoiceDelegatePointer;
		
		#endregion
		public VoiceResult DecompressVoice( IntPtr pCompressed, uint cbCompressed, IntPtr pDestBuffer, uint cbDestBufferSize, ref uint nBytesWritten, uint nDesiredSampleRate )
		{
			return DecompressVoiceDelegatePointer( Self, pCompressed, cbCompressed, pDestBuffer, cbDestBufferSize, ref nBytesWritten, nDesiredSampleRate );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		public delegate uint GetVoiceOptimalSampleRateDelegate( IntPtr self );
		private GetVoiceOptimalSampleRateDelegate GetVoiceOptimalSampleRateDelegatePointer;
		
		#endregion
		public uint GetVoiceOptimalSampleRate()
		{
			return GetVoiceOptimalSampleRateDelegatePointer( Self );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		public delegate HAuthTicket GetAuthSessionTicketDelegate( IntPtr self, IntPtr pTicket, int cbMaxTicket, ref uint pcbTicket );
		private GetAuthSessionTicketDelegate GetAuthSessionTicketDelegatePointer;
		
		#endregion
		public HAuthTicket GetAuthSessionTicket( IntPtr pTicket, int cbMaxTicket, ref uint pcbTicket )
		{
			return GetAuthSessionTicketDelegatePointer( Self, pTicket, cbMaxTicket, ref pcbTicket );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		public delegate BeginAuthSessionResult BeginAuthSessionDelegate( IntPtr self, IntPtr pAuthTicket, int cbAuthTicket, SteamId steamID );
		private BeginAuthSessionDelegate BeginAuthSessionDelegatePointer;
		
		#endregion
		public BeginAuthSessionResult BeginAuthSession( IntPtr pAuthTicket, int cbAuthTicket, SteamId steamID )
		{
			return BeginAuthSessionDelegatePointer( Self, pAuthTicket, cbAuthTicket, steamID );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		public delegate void EndAuthSessionDelegate( IntPtr self, SteamId steamID );
		private EndAuthSessionDelegate EndAuthSessionDelegatePointer;
		
		#endregion
		public void EndAuthSession( SteamId steamID )
		{
			EndAuthSessionDelegatePointer( Self, steamID );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		public delegate void CancelAuthTicketDelegate( IntPtr self, HAuthTicket hAuthTicket );
		private CancelAuthTicketDelegate CancelAuthTicketDelegatePointer;
		
		#endregion
		public void CancelAuthTicket( HAuthTicket hAuthTicket )
		{
			CancelAuthTicketDelegatePointer( Self, hAuthTicket );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		public delegate UserHasLicenseForAppResult UserHasLicenseForAppDelegate( IntPtr self, SteamId steamID, AppId_t appID );
		private UserHasLicenseForAppDelegate UserHasLicenseForAppDelegatePointer;
		
		#endregion
		public UserHasLicenseForAppResult UserHasLicenseForApp( SteamId steamID, AppId_t appID )
		{
			return UserHasLicenseForAppDelegatePointer( Self, steamID, appID );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		public delegate bool BIsBehindNATDelegate( IntPtr self );
		private BIsBehindNATDelegate BIsBehindNATDelegatePointer;
		
		#endregion
		public bool BIsBehindNAT()
		{
			return BIsBehindNATDelegatePointer( Self );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		public delegate void AdvertiseGameDelegate( IntPtr self, SteamId steamIDGameServer, uint unIPServer, ushort usPortServer );
		private AdvertiseGameDelegate AdvertiseGameDelegatePointer;
		
		#endregion
		public void AdvertiseGame( SteamId steamIDGameServer, uint unIPServer, ushort usPortServer )
		{
			AdvertiseGameDelegatePointer( Self, steamIDGameServer, unIPServer, usPortServer );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		public delegate SteamAPICall_t RequestEncryptedAppTicketDelegate( IntPtr self, IntPtr pDataToInclude, int cbDataToInclude );
		private RequestEncryptedAppTicketDelegate RequestEncryptedAppTicketDelegatePointer;
		
		#endregion
		public async Task<EncryptedAppTicketResponse_t?> RequestEncryptedAppTicket( IntPtr pDataToInclude, int cbDataToInclude )
		{
			return await (new Result<EncryptedAppTicketResponse_t>( RequestEncryptedAppTicketDelegatePointer( Self, pDataToInclude, cbDataToInclude ) )).GetResult();
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		public delegate bool GetEncryptedAppTicketDelegate( IntPtr self, IntPtr pTicket, int cbMaxTicket, ref uint pcbTicket );
		private GetEncryptedAppTicketDelegate GetEncryptedAppTicketDelegatePointer;
		
		#endregion
		public bool GetEncryptedAppTicket( IntPtr pTicket, int cbMaxTicket, ref uint pcbTicket )
		{
			return GetEncryptedAppTicketDelegatePointer( Self, pTicket, cbMaxTicket, ref pcbTicket );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		public delegate int GetGameBadgeLevelDelegate( IntPtr self, int nSeries, [MarshalAs( UnmanagedType.U1 )] bool bFoil );
		private GetGameBadgeLevelDelegate GetGameBadgeLevelDelegatePointer;
		
		#endregion
		public int GetGameBadgeLevel( int nSeries, [MarshalAs( UnmanagedType.U1 )] bool bFoil )
		{
			return GetGameBadgeLevelDelegatePointer( Self, nSeries, bFoil );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		public delegate int GetPlayerSteamLevelDelegate( IntPtr self );
		private GetPlayerSteamLevelDelegate GetPlayerSteamLevelDelegatePointer;
		
		#endregion
		public int GetPlayerSteamLevel()
		{
			return GetPlayerSteamLevelDelegatePointer( Self );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		public delegate SteamAPICall_t RequestStoreAuthURLDelegate( IntPtr self, string pchRedirectURL );
		private RequestStoreAuthURLDelegate RequestStoreAuthURLDelegatePointer;
		
		#endregion
		public async Task<StoreAuthURLResponse_t?> RequestStoreAuthURL( string pchRedirectURL )
		{
			return await (new Result<StoreAuthURLResponse_t>( RequestStoreAuthURLDelegatePointer( Self, pchRedirectURL ) )).GetResult();
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		public delegate bool BIsPhoneVerifiedDelegate( IntPtr self );
		private BIsPhoneVerifiedDelegate BIsPhoneVerifiedDelegatePointer;
		
		#endregion
		public bool BIsPhoneVerified()
		{
			return BIsPhoneVerifiedDelegatePointer( Self );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		public delegate bool BIsTwoFactorEnabledDelegate( IntPtr self );
		private BIsTwoFactorEnabledDelegate BIsTwoFactorEnabledDelegatePointer;
		
		#endregion
		public bool BIsTwoFactorEnabled()
		{
			return BIsTwoFactorEnabledDelegatePointer( Self );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		public delegate bool BIsPhoneIdentifyingDelegate( IntPtr self );
		private BIsPhoneIdentifyingDelegate BIsPhoneIdentifyingDelegatePointer;
		
		#endregion
		public bool BIsPhoneIdentifying()
		{
			return BIsPhoneIdentifyingDelegatePointer( Self );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		public delegate bool BIsPhoneRequiringVerificationDelegate( IntPtr self );
		private BIsPhoneRequiringVerificationDelegate BIsPhoneRequiringVerificationDelegatePointer;
		
		#endregion
		public bool BIsPhoneRequiringVerification()
		{
			return BIsPhoneRequiringVerificationDelegatePointer( Self );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		public delegate SteamAPICall_t GetMarketEligibilityDelegate( IntPtr self );
		private GetMarketEligibilityDelegate GetMarketEligibilityDelegatePointer;
		
		#endregion
		public async Task<MarketEligibilityResponse_t?> GetMarketEligibility()
		{
			return await (new Result<MarketEligibilityResponse_t>( GetMarketEligibilityDelegatePointer( Self ) )).GetResult();
		}
		
	}
}
