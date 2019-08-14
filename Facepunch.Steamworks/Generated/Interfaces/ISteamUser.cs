using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Steamworks.Data;


namespace Steamworks
{
	internal class ISteamUser : SteamInterface
	{
		public override string InterfaceName => "SteamUser020";
		
		public override void InitInternals()
		{
			_GetHSteamUser = Marshal.GetDelegateForFunctionPointer<FGetHSteamUser>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 0 ) ) );
			_BLoggedOn = Marshal.GetDelegateForFunctionPointer<FBLoggedOn>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 8 ) ) );
			_GetSteamID = Marshal.GetDelegateForFunctionPointer<FGetSteamID>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 16 ) ) );
			_InitiateGameConnection = Marshal.GetDelegateForFunctionPointer<FInitiateGameConnection>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 24 ) ) );
			_TerminateGameConnection = Marshal.GetDelegateForFunctionPointer<FTerminateGameConnection>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 32 ) ) );
			_TrackAppUsageEvent = Marshal.GetDelegateForFunctionPointer<FTrackAppUsageEvent>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 40 ) ) );
			_GetUserDataFolder = Marshal.GetDelegateForFunctionPointer<FGetUserDataFolder>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 48 ) ) );
			_StartVoiceRecording = Marshal.GetDelegateForFunctionPointer<FStartVoiceRecording>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 56 ) ) );
			_StopVoiceRecording = Marshal.GetDelegateForFunctionPointer<FStopVoiceRecording>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 64 ) ) );
			_GetAvailableVoice = Marshal.GetDelegateForFunctionPointer<FGetAvailableVoice>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 72 ) ) );
			_GetVoice = Marshal.GetDelegateForFunctionPointer<FGetVoice>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 80 ) ) );
			_DecompressVoice = Marshal.GetDelegateForFunctionPointer<FDecompressVoice>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 88 ) ) );
			_GetVoiceOptimalSampleRate = Marshal.GetDelegateForFunctionPointer<FGetVoiceOptimalSampleRate>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 96 ) ) );
			_GetAuthSessionTicket = Marshal.GetDelegateForFunctionPointer<FGetAuthSessionTicket>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 104 ) ) );
			_BeginAuthSession = Marshal.GetDelegateForFunctionPointer<FBeginAuthSession>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 112 ) ) );
			_EndAuthSession = Marshal.GetDelegateForFunctionPointer<FEndAuthSession>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 120 ) ) );
			_CancelAuthTicket = Marshal.GetDelegateForFunctionPointer<FCancelAuthTicket>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 128 ) ) );
			_UserHasLicenseForApp = Marshal.GetDelegateForFunctionPointer<FUserHasLicenseForApp>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 136 ) ) );
			_BIsBehindNAT = Marshal.GetDelegateForFunctionPointer<FBIsBehindNAT>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 144 ) ) );
			_AdvertiseGame = Marshal.GetDelegateForFunctionPointer<FAdvertiseGame>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 152 ) ) );
			_RequestEncryptedAppTicket = Marshal.GetDelegateForFunctionPointer<FRequestEncryptedAppTicket>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 160 ) ) );
			_GetEncryptedAppTicket = Marshal.GetDelegateForFunctionPointer<FGetEncryptedAppTicket>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 168 ) ) );
			_GetGameBadgeLevel = Marshal.GetDelegateForFunctionPointer<FGetGameBadgeLevel>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 176 ) ) );
			_GetPlayerSteamLevel = Marshal.GetDelegateForFunctionPointer<FGetPlayerSteamLevel>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 184 ) ) );
			_RequestStoreAuthURL = Marshal.GetDelegateForFunctionPointer<FRequestStoreAuthURL>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 192 ) ) );
			_BIsPhoneVerified = Marshal.GetDelegateForFunctionPointer<FBIsPhoneVerified>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 200 ) ) );
			_BIsTwoFactorEnabled = Marshal.GetDelegateForFunctionPointer<FBIsTwoFactorEnabled>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 208 ) ) );
			_BIsPhoneIdentifying = Marshal.GetDelegateForFunctionPointer<FBIsPhoneIdentifying>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 216 ) ) );
			_BIsPhoneRequiringVerification = Marshal.GetDelegateForFunctionPointer<FBIsPhoneRequiringVerification>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 224 ) ) );
			_GetMarketEligibility = Marshal.GetDelegateForFunctionPointer<FGetMarketEligibility>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 232 ) ) );
		}
		internal override void Shutdown()
		{
			base.Shutdown();
			
			_GetHSteamUser = null;
			_BLoggedOn = null;
			_GetSteamID = null;
			_InitiateGameConnection = null;
			_TerminateGameConnection = null;
			_TrackAppUsageEvent = null;
			_GetUserDataFolder = null;
			_StartVoiceRecording = null;
			_StopVoiceRecording = null;
			_GetAvailableVoice = null;
			_GetVoice = null;
			_DecompressVoice = null;
			_GetVoiceOptimalSampleRate = null;
			_GetAuthSessionTicket = null;
			_BeginAuthSession = null;
			_EndAuthSession = null;
			_CancelAuthTicket = null;
			_UserHasLicenseForApp = null;
			_BIsBehindNAT = null;
			_AdvertiseGame = null;
			_RequestEncryptedAppTicket = null;
			_GetEncryptedAppTicket = null;
			_GetGameBadgeLevel = null;
			_GetPlayerSteamLevel = null;
			_RequestStoreAuthURL = null;
			_BIsPhoneVerified = null;
			_BIsTwoFactorEnabled = null;
			_BIsPhoneIdentifying = null;
			_BIsPhoneRequiringVerification = null;
			_GetMarketEligibility = null;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate HSteamUser FGetHSteamUser( IntPtr self );
		private FGetHSteamUser _GetHSteamUser;
		
		#endregion
		internal HSteamUser GetHSteamUser()
		{
			var returnValue = _GetHSteamUser( Self );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FBLoggedOn( IntPtr self );
		private FBLoggedOn _BLoggedOn;
		
		#endregion
		internal bool BLoggedOn()
		{
			var returnValue = _BLoggedOn( Self );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		#if PLATFORM_WIN
		private delegate void FGetSteamID( IntPtr self, ref SteamId retVal );
		#else
		private delegate SteamId FGetSteamID( IntPtr self );
		#endif
		private FGetSteamID _GetSteamID;
		
		#endregion
		internal SteamId GetSteamID()
		{
			#if PLATFORM_WIN
			var retVal = default( SteamId );
			_GetSteamID( Self, ref retVal );
			return retVal;
			#else
			var returnValue = _GetSteamID( Self );
			return returnValue;
			#endif
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate int FInitiateGameConnection( IntPtr self, IntPtr pAuthBlob, int cbMaxAuthBlob, SteamId steamIDGameServer, uint unIPServer, ushort usPortServer, [MarshalAs( UnmanagedType.U1 )] bool bSecure );
		private FInitiateGameConnection _InitiateGameConnection;
		
		#endregion
		internal int InitiateGameConnection( IntPtr pAuthBlob, int cbMaxAuthBlob, SteamId steamIDGameServer, uint unIPServer, ushort usPortServer, [MarshalAs( UnmanagedType.U1 )] bool bSecure )
		{
			var returnValue = _InitiateGameConnection( Self, pAuthBlob, cbMaxAuthBlob, steamIDGameServer, unIPServer, usPortServer, bSecure );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate void FTerminateGameConnection( IntPtr self, uint unIPServer, ushort usPortServer );
		private FTerminateGameConnection _TerminateGameConnection;
		
		#endregion
		internal void TerminateGameConnection( uint unIPServer, ushort usPortServer )
		{
			_TerminateGameConnection( Self, unIPServer, usPortServer );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate void FTrackAppUsageEvent( IntPtr self, GameId gameID, int eAppUsageEvent, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchExtraInfo );
		private FTrackAppUsageEvent _TrackAppUsageEvent;
		
		#endregion
		internal void TrackAppUsageEvent( GameId gameID, int eAppUsageEvent, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchExtraInfo )
		{
			_TrackAppUsageEvent( Self, gameID, eAppUsageEvent, pchExtraInfo );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FGetUserDataFolder( IntPtr self, IntPtr pchBuffer, int cubBuffer );
		private FGetUserDataFolder _GetUserDataFolder;
		
		#endregion
		internal bool GetUserDataFolder( out string pchBuffer )
		{
			IntPtr mempchBuffer = Helpers.TakeMemory();
			var returnValue = _GetUserDataFolder( Self, mempchBuffer, (1024 * 32) );
			pchBuffer = Helpers.MemoryToString( mempchBuffer );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate void FStartVoiceRecording( IntPtr self );
		private FStartVoiceRecording _StartVoiceRecording;
		
		#endregion
		internal void StartVoiceRecording()
		{
			_StartVoiceRecording( Self );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate void FStopVoiceRecording( IntPtr self );
		private FStopVoiceRecording _StopVoiceRecording;
		
		#endregion
		internal void StopVoiceRecording()
		{
			_StopVoiceRecording( Self );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate VoiceResult FGetAvailableVoice( IntPtr self, ref uint pcbCompressed, ref uint pcbUncompressed_Deprecated, uint nUncompressedVoiceDesiredSampleRate_Deprecated );
		private FGetAvailableVoice _GetAvailableVoice;
		
		#endregion
		internal VoiceResult GetAvailableVoice( ref uint pcbCompressed, ref uint pcbUncompressed_Deprecated, uint nUncompressedVoiceDesiredSampleRate_Deprecated )
		{
			var returnValue = _GetAvailableVoice( Self, ref pcbCompressed, ref pcbUncompressed_Deprecated, nUncompressedVoiceDesiredSampleRate_Deprecated );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate VoiceResult FGetVoice( IntPtr self, [MarshalAs( UnmanagedType.U1 )] bool bWantCompressed, IntPtr pDestBuffer, uint cbDestBufferSize, ref uint nBytesWritten, [MarshalAs( UnmanagedType.U1 )] bool bWantUncompressed_Deprecated, IntPtr pUncompressedDestBuffer_Deprecated, uint cbUncompressedDestBufferSize_Deprecated, ref uint nUncompressBytesWritten_Deprecated, uint nUncompressedVoiceDesiredSampleRate_Deprecated );
		private FGetVoice _GetVoice;
		
		#endregion
		internal VoiceResult GetVoice( [MarshalAs( UnmanagedType.U1 )] bool bWantCompressed, IntPtr pDestBuffer, uint cbDestBufferSize, ref uint nBytesWritten, [MarshalAs( UnmanagedType.U1 )] bool bWantUncompressed_Deprecated, IntPtr pUncompressedDestBuffer_Deprecated, uint cbUncompressedDestBufferSize_Deprecated, ref uint nUncompressBytesWritten_Deprecated, uint nUncompressedVoiceDesiredSampleRate_Deprecated )
		{
			var returnValue = _GetVoice( Self, bWantCompressed, pDestBuffer, cbDestBufferSize, ref nBytesWritten, bWantUncompressed_Deprecated, pUncompressedDestBuffer_Deprecated, cbUncompressedDestBufferSize_Deprecated, ref nUncompressBytesWritten_Deprecated, nUncompressedVoiceDesiredSampleRate_Deprecated );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate VoiceResult FDecompressVoice( IntPtr self, IntPtr pCompressed, uint cbCompressed, IntPtr pDestBuffer, uint cbDestBufferSize, ref uint nBytesWritten, uint nDesiredSampleRate );
		private FDecompressVoice _DecompressVoice;
		
		#endregion
		internal VoiceResult DecompressVoice( IntPtr pCompressed, uint cbCompressed, IntPtr pDestBuffer, uint cbDestBufferSize, ref uint nBytesWritten, uint nDesiredSampleRate )
		{
			var returnValue = _DecompressVoice( Self, pCompressed, cbCompressed, pDestBuffer, cbDestBufferSize, ref nBytesWritten, nDesiredSampleRate );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate uint FGetVoiceOptimalSampleRate( IntPtr self );
		private FGetVoiceOptimalSampleRate _GetVoiceOptimalSampleRate;
		
		#endregion
		internal uint GetVoiceOptimalSampleRate()
		{
			var returnValue = _GetVoiceOptimalSampleRate( Self );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate HAuthTicket FGetAuthSessionTicket( IntPtr self, IntPtr pTicket, int cbMaxTicket, ref uint pcbTicket );
		private FGetAuthSessionTicket _GetAuthSessionTicket;
		
		#endregion
		internal HAuthTicket GetAuthSessionTicket( IntPtr pTicket, int cbMaxTicket, ref uint pcbTicket )
		{
			var returnValue = _GetAuthSessionTicket( Self, pTicket, cbMaxTicket, ref pcbTicket );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate BeginAuthResult FBeginAuthSession( IntPtr self, IntPtr pAuthTicket, int cbAuthTicket, SteamId steamID );
		private FBeginAuthSession _BeginAuthSession;
		
		#endregion
		internal BeginAuthResult BeginAuthSession( IntPtr pAuthTicket, int cbAuthTicket, SteamId steamID )
		{
			var returnValue = _BeginAuthSession( Self, pAuthTicket, cbAuthTicket, steamID );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate void FEndAuthSession( IntPtr self, SteamId steamID );
		private FEndAuthSession _EndAuthSession;
		
		#endregion
		internal void EndAuthSession( SteamId steamID )
		{
			_EndAuthSession( Self, steamID );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate void FCancelAuthTicket( IntPtr self, HAuthTicket hAuthTicket );
		private FCancelAuthTicket _CancelAuthTicket;
		
		#endregion
		internal void CancelAuthTicket( HAuthTicket hAuthTicket )
		{
			_CancelAuthTicket( Self, hAuthTicket );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate UserHasLicenseForAppResult FUserHasLicenseForApp( IntPtr self, SteamId steamID, AppId appID );
		private FUserHasLicenseForApp _UserHasLicenseForApp;
		
		#endregion
		internal UserHasLicenseForAppResult UserHasLicenseForApp( SteamId steamID, AppId appID )
		{
			var returnValue = _UserHasLicenseForApp( Self, steamID, appID );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FBIsBehindNAT( IntPtr self );
		private FBIsBehindNAT _BIsBehindNAT;
		
		#endregion
		internal bool BIsBehindNAT()
		{
			var returnValue = _BIsBehindNAT( Self );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate void FAdvertiseGame( IntPtr self, SteamId steamIDGameServer, uint unIPServer, ushort usPortServer );
		private FAdvertiseGame _AdvertiseGame;
		
		#endregion
		internal void AdvertiseGame( SteamId steamIDGameServer, uint unIPServer, ushort usPortServer )
		{
			_AdvertiseGame( Self, steamIDGameServer, unIPServer, usPortServer );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate SteamAPICall_t FRequestEncryptedAppTicket( IntPtr self, IntPtr pDataToInclude, int cbDataToInclude );
		private FRequestEncryptedAppTicket _RequestEncryptedAppTicket;
		
		#endregion
		internal async Task<EncryptedAppTicketResponse_t?> RequestEncryptedAppTicket( IntPtr pDataToInclude, int cbDataToInclude )
		{
			var returnValue = _RequestEncryptedAppTicket( Self, pDataToInclude, cbDataToInclude );
			return await EncryptedAppTicketResponse_t.GetResultAsync( returnValue );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FGetEncryptedAppTicket( IntPtr self, IntPtr pTicket, int cbMaxTicket, ref uint pcbTicket );
		private FGetEncryptedAppTicket _GetEncryptedAppTicket;
		
		#endregion
		internal bool GetEncryptedAppTicket( IntPtr pTicket, int cbMaxTicket, ref uint pcbTicket )
		{
			var returnValue = _GetEncryptedAppTicket( Self, pTicket, cbMaxTicket, ref pcbTicket );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate int FGetGameBadgeLevel( IntPtr self, int nSeries, [MarshalAs( UnmanagedType.U1 )] bool bFoil );
		private FGetGameBadgeLevel _GetGameBadgeLevel;
		
		#endregion
		internal int GetGameBadgeLevel( int nSeries, [MarshalAs( UnmanagedType.U1 )] bool bFoil )
		{
			var returnValue = _GetGameBadgeLevel( Self, nSeries, bFoil );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate int FGetPlayerSteamLevel( IntPtr self );
		private FGetPlayerSteamLevel _GetPlayerSteamLevel;
		
		#endregion
		internal int GetPlayerSteamLevel()
		{
			var returnValue = _GetPlayerSteamLevel( Self );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate SteamAPICall_t FRequestStoreAuthURL( IntPtr self, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchRedirectURL );
		private FRequestStoreAuthURL _RequestStoreAuthURL;
		
		#endregion
		internal async Task<StoreAuthURLResponse_t?> RequestStoreAuthURL( [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchRedirectURL )
		{
			var returnValue = _RequestStoreAuthURL( Self, pchRedirectURL );
			return await StoreAuthURLResponse_t.GetResultAsync( returnValue );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FBIsPhoneVerified( IntPtr self );
		private FBIsPhoneVerified _BIsPhoneVerified;
		
		#endregion
		internal bool BIsPhoneVerified()
		{
			var returnValue = _BIsPhoneVerified( Self );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FBIsTwoFactorEnabled( IntPtr self );
		private FBIsTwoFactorEnabled _BIsTwoFactorEnabled;
		
		#endregion
		internal bool BIsTwoFactorEnabled()
		{
			var returnValue = _BIsTwoFactorEnabled( Self );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FBIsPhoneIdentifying( IntPtr self );
		private FBIsPhoneIdentifying _BIsPhoneIdentifying;
		
		#endregion
		internal bool BIsPhoneIdentifying()
		{
			var returnValue = _BIsPhoneIdentifying( Self );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FBIsPhoneRequiringVerification( IntPtr self );
		private FBIsPhoneRequiringVerification _BIsPhoneRequiringVerification;
		
		#endregion
		internal bool BIsPhoneRequiringVerification()
		{
			var returnValue = _BIsPhoneRequiringVerification( Self );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate SteamAPICall_t FGetMarketEligibility( IntPtr self );
		private FGetMarketEligibility _GetMarketEligibility;
		
		#endregion
		internal async Task<MarketEligibilityResponse_t?> GetMarketEligibility()
		{
			var returnValue = _GetMarketEligibility( Self );
			return await MarketEligibilityResponse_t.GetResultAsync( returnValue );
		}
		
	}
}
