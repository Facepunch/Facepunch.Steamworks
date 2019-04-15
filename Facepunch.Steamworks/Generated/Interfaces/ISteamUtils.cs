using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using SteamNative;


namespace Steamworks.Internal
{
	public class ISteamUtils : BaseSteamInterface
	{
		public ISteamUtils( bool server = false ) : base( server )
		{
		}
		
		public override string InterfaceName => "SteamUtils009";
		
		public override void InitInternals()
		{
			GetSecondsSinceAppActiveDelegatePointer = Marshal.GetDelegateForFunctionPointer<GetSecondsSinceAppActiveDelegate>( Marshal.ReadIntPtr( VTable, 0) );
			GetSecondsSinceComputerActiveDelegatePointer = Marshal.GetDelegateForFunctionPointer<GetSecondsSinceComputerActiveDelegate>( Marshal.ReadIntPtr( VTable, 8) );
			GetConnectedUniverseDelegatePointer = Marshal.GetDelegateForFunctionPointer<GetConnectedUniverseDelegate>( Marshal.ReadIntPtr( VTable, 16) );
			GetServerRealTimeDelegatePointer = Marshal.GetDelegateForFunctionPointer<GetServerRealTimeDelegate>( Marshal.ReadIntPtr( VTable, 24) );
			GetIPCountryDelegatePointer = Marshal.GetDelegateForFunctionPointer<GetIPCountryDelegate>( Marshal.ReadIntPtr( VTable, 32) );
			GetImageSizeDelegatePointer = Marshal.GetDelegateForFunctionPointer<GetImageSizeDelegate>( Marshal.ReadIntPtr( VTable, 40) );
			GetImageRGBADelegatePointer = Marshal.GetDelegateForFunctionPointer<GetImageRGBADelegate>( Marshal.ReadIntPtr( VTable, 48) );
			GetCSERIPPortDelegatePointer = Marshal.GetDelegateForFunctionPointer<GetCSERIPPortDelegate>( Marshal.ReadIntPtr( VTable, 56) );
			GetCurrentBatteryPowerDelegatePointer = Marshal.GetDelegateForFunctionPointer<GetCurrentBatteryPowerDelegate>( Marshal.ReadIntPtr( VTable, 64) );
			GetAppIDDelegatePointer = Marshal.GetDelegateForFunctionPointer<GetAppIDDelegate>( Marshal.ReadIntPtr( VTable, 72) );
			SetOverlayNotificationPositionDelegatePointer = Marshal.GetDelegateForFunctionPointer<SetOverlayNotificationPositionDelegate>( Marshal.ReadIntPtr( VTable, 80) );
			IsAPICallCompletedDelegatePointer = Marshal.GetDelegateForFunctionPointer<IsAPICallCompletedDelegate>( Marshal.ReadIntPtr( VTable, 88) );
			GetAPICallFailureReasonDelegatePointer = Marshal.GetDelegateForFunctionPointer<GetAPICallFailureReasonDelegate>( Marshal.ReadIntPtr( VTable, 96) );
			GetAPICallResultDelegatePointer = Marshal.GetDelegateForFunctionPointer<GetAPICallResultDelegate>( Marshal.ReadIntPtr( VTable, 104) );
			RunFrameDelegatePointer = Marshal.GetDelegateForFunctionPointer<RunFrameDelegate>( Marshal.ReadIntPtr( VTable, 112) );
			GetIPCCallCountDelegatePointer = Marshal.GetDelegateForFunctionPointer<GetIPCCallCountDelegate>( Marshal.ReadIntPtr( VTable, 120) );
			SetWarningMessageHookDelegatePointer = Marshal.GetDelegateForFunctionPointer<SetWarningMessageHookDelegate>( Marshal.ReadIntPtr( VTable, 128) );
			IsOverlayEnabledDelegatePointer = Marshal.GetDelegateForFunctionPointer<IsOverlayEnabledDelegate>( Marshal.ReadIntPtr( VTable, 136) );
			BOverlayNeedsPresentDelegatePointer = Marshal.GetDelegateForFunctionPointer<BOverlayNeedsPresentDelegate>( Marshal.ReadIntPtr( VTable, 144) );
			CheckFileSignatureDelegatePointer = Marshal.GetDelegateForFunctionPointer<CheckFileSignatureDelegate>( Marshal.ReadIntPtr( VTable, 152) );
			ShowGamepadTextInputDelegatePointer = Marshal.GetDelegateForFunctionPointer<ShowGamepadTextInputDelegate>( Marshal.ReadIntPtr( VTable, 160) );
			GetEnteredGamepadTextLengthDelegatePointer = Marshal.GetDelegateForFunctionPointer<GetEnteredGamepadTextLengthDelegate>( Marshal.ReadIntPtr( VTable, 168) );
			GetEnteredGamepadTextInputDelegatePointer = Marshal.GetDelegateForFunctionPointer<GetEnteredGamepadTextInputDelegate>( Marshal.ReadIntPtr( VTable, 176) );
			GetSteamUILanguageDelegatePointer = Marshal.GetDelegateForFunctionPointer<GetSteamUILanguageDelegate>( Marshal.ReadIntPtr( VTable, 184) );
			IsSteamRunningInVRDelegatePointer = Marshal.GetDelegateForFunctionPointer<IsSteamRunningInVRDelegate>( Marshal.ReadIntPtr( VTable, 192) );
			SetOverlayNotificationInsetDelegatePointer = Marshal.GetDelegateForFunctionPointer<SetOverlayNotificationInsetDelegate>( Marshal.ReadIntPtr( VTable, 200) );
			IsSteamInBigPictureModeDelegatePointer = Marshal.GetDelegateForFunctionPointer<IsSteamInBigPictureModeDelegate>( Marshal.ReadIntPtr( VTable, 208) );
			StartVRDashboardDelegatePointer = Marshal.GetDelegateForFunctionPointer<StartVRDashboardDelegate>( Marshal.ReadIntPtr( VTable, 216) );
			IsVRHeadsetStreamingEnabledDelegatePointer = Marshal.GetDelegateForFunctionPointer<IsVRHeadsetStreamingEnabledDelegate>( Marshal.ReadIntPtr( VTable, 224) );
			SetVRHeadsetStreamingEnabledDelegatePointer = Marshal.GetDelegateForFunctionPointer<SetVRHeadsetStreamingEnabledDelegate>( Marshal.ReadIntPtr( VTable, 232) );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		public delegate uint GetSecondsSinceAppActiveDelegate( IntPtr self );
		private GetSecondsSinceAppActiveDelegate GetSecondsSinceAppActiveDelegatePointer;
		
		#endregion
		public uint GetSecondsSinceAppActive()
		{
			return GetSecondsSinceAppActiveDelegatePointer( Self );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		public delegate uint GetSecondsSinceComputerActiveDelegate( IntPtr self );
		private GetSecondsSinceComputerActiveDelegate GetSecondsSinceComputerActiveDelegatePointer;
		
		#endregion
		public uint GetSecondsSinceComputerActive()
		{
			return GetSecondsSinceComputerActiveDelegatePointer( Self );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		public delegate Universe GetConnectedUniverseDelegate( IntPtr self );
		private GetConnectedUniverseDelegate GetConnectedUniverseDelegatePointer;
		
		#endregion
		public Universe GetConnectedUniverse()
		{
			return GetConnectedUniverseDelegatePointer( Self );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		public delegate uint GetServerRealTimeDelegate( IntPtr self );
		private GetServerRealTimeDelegate GetServerRealTimeDelegatePointer;
		
		#endregion
		public uint GetServerRealTime()
		{
			return GetServerRealTimeDelegatePointer( Self );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		public delegate IntPtr GetIPCountryDelegate( IntPtr self );
		private GetIPCountryDelegate GetIPCountryDelegatePointer;
		
		#endregion
		public string GetIPCountry()
		{
			return GetString( GetIPCountryDelegatePointer( Self ) );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		public delegate bool GetImageSizeDelegate( IntPtr self, int iImage, ref uint pnWidth, ref uint pnHeight );
		private GetImageSizeDelegate GetImageSizeDelegatePointer;
		
		#endregion
		public bool GetImageSize( int iImage, ref uint pnWidth, ref uint pnHeight )
		{
			return GetImageSizeDelegatePointer( Self, iImage, ref pnWidth, ref pnHeight );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		public delegate bool GetImageRGBADelegate( IntPtr self, int iImage, [In,Out] byte[]  pubDest, int nDestBufferSize );
		private GetImageRGBADelegate GetImageRGBADelegatePointer;
		
		#endregion
		public bool GetImageRGBA( int iImage, [In,Out] byte[]  pubDest, int nDestBufferSize )
		{
			return GetImageRGBADelegatePointer( Self, iImage, pubDest, nDestBufferSize );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		public delegate bool GetCSERIPPortDelegate( IntPtr self, ref uint unIP, ref ushort usPort );
		private GetCSERIPPortDelegate GetCSERIPPortDelegatePointer;
		
		#endregion
		public bool GetCSERIPPort( ref uint unIP, ref ushort usPort )
		{
			return GetCSERIPPortDelegatePointer( Self, ref unIP, ref usPort );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		public delegate byte GetCurrentBatteryPowerDelegate( IntPtr self );
		private GetCurrentBatteryPowerDelegate GetCurrentBatteryPowerDelegatePointer;
		
		#endregion
		public byte GetCurrentBatteryPower()
		{
			return GetCurrentBatteryPowerDelegatePointer( Self );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		public delegate uint GetAppIDDelegate( IntPtr self );
		private GetAppIDDelegate GetAppIDDelegatePointer;
		
		#endregion
		public uint GetAppID()
		{
			return GetAppIDDelegatePointer( Self );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		public delegate void SetOverlayNotificationPositionDelegate( IntPtr self, NotificationPosition eNotificationPosition );
		private SetOverlayNotificationPositionDelegate SetOverlayNotificationPositionDelegatePointer;
		
		#endregion
		public void SetOverlayNotificationPosition( NotificationPosition eNotificationPosition )
		{
			SetOverlayNotificationPositionDelegatePointer( Self, eNotificationPosition );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		public delegate bool IsAPICallCompletedDelegate( IntPtr self, SteamAPICall_t hSteamAPICall, [MarshalAs( UnmanagedType.U1 )] ref bool pbFailed );
		private IsAPICallCompletedDelegate IsAPICallCompletedDelegatePointer;
		
		#endregion
		public bool IsAPICallCompleted( SteamAPICall_t hSteamAPICall, [MarshalAs( UnmanagedType.U1 )] ref bool pbFailed )
		{
			return IsAPICallCompletedDelegatePointer( Self, hSteamAPICall, ref pbFailed );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		public delegate SteamAPICallFailure GetAPICallFailureReasonDelegate( IntPtr self, SteamAPICall_t hSteamAPICall );
		private GetAPICallFailureReasonDelegate GetAPICallFailureReasonDelegatePointer;
		
		#endregion
		public SteamAPICallFailure GetAPICallFailureReason( SteamAPICall_t hSteamAPICall )
		{
			return GetAPICallFailureReasonDelegatePointer( Self, hSteamAPICall );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		public delegate bool GetAPICallResultDelegate( IntPtr self, SteamAPICall_t hSteamAPICall, IntPtr pCallback, int cubCallback, int iCallbackExpected, [MarshalAs( UnmanagedType.U1 )] ref bool pbFailed );
		private GetAPICallResultDelegate GetAPICallResultDelegatePointer;
		
		#endregion
		public bool GetAPICallResult( SteamAPICall_t hSteamAPICall, IntPtr pCallback, int cubCallback, int iCallbackExpected, [MarshalAs( UnmanagedType.U1 )] ref bool pbFailed )
		{
			return GetAPICallResultDelegatePointer( Self, hSteamAPICall, pCallback, cubCallback, iCallbackExpected, ref pbFailed );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		public delegate void RunFrameDelegate( IntPtr self );
		private RunFrameDelegate RunFrameDelegatePointer;
		
		#endregion
		public void RunFrame()
		{
			RunFrameDelegatePointer( Self );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		public delegate uint GetIPCCallCountDelegate( IntPtr self );
		private GetIPCCallCountDelegate GetIPCCallCountDelegatePointer;
		
		#endregion
		public uint GetIPCCallCount()
		{
			return GetIPCCallCountDelegatePointer( Self );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		public delegate void SetWarningMessageHookDelegate( IntPtr self, IntPtr pFunction );
		private SetWarningMessageHookDelegate SetWarningMessageHookDelegatePointer;
		
		#endregion
		public void SetWarningMessageHook( IntPtr pFunction )
		{
			SetWarningMessageHookDelegatePointer( Self, pFunction );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		public delegate bool IsOverlayEnabledDelegate( IntPtr self );
		private IsOverlayEnabledDelegate IsOverlayEnabledDelegatePointer;
		
		#endregion
		public bool IsOverlayEnabled()
		{
			return IsOverlayEnabledDelegatePointer( Self );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		public delegate bool BOverlayNeedsPresentDelegate( IntPtr self );
		private BOverlayNeedsPresentDelegate BOverlayNeedsPresentDelegatePointer;
		
		#endregion
		public bool BOverlayNeedsPresent()
		{
			return BOverlayNeedsPresentDelegatePointer( Self );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		public delegate SteamAPICall_t CheckFileSignatureDelegate( IntPtr self, string szFileName );
		private CheckFileSignatureDelegate CheckFileSignatureDelegatePointer;
		
		#endregion
		public async Task<CheckFileSignature_t?> CheckFileSignature( string szFileName )
		{
			return await (new Result<CheckFileSignature_t>( CheckFileSignatureDelegatePointer( Self, szFileName ) )).GetResult();
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		public delegate bool ShowGamepadTextInputDelegate( IntPtr self, GamepadTextInputMode eInputMode, GamepadTextInputLineMode eLineInputMode, string pchDescription, uint unCharMax, string pchExistingText );
		private ShowGamepadTextInputDelegate ShowGamepadTextInputDelegatePointer;
		
		#endregion
		public bool ShowGamepadTextInput( GamepadTextInputMode eInputMode, GamepadTextInputLineMode eLineInputMode, string pchDescription, uint unCharMax, string pchExistingText )
		{
			return ShowGamepadTextInputDelegatePointer( Self, eInputMode, eLineInputMode, pchDescription, unCharMax, pchExistingText );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		public delegate uint GetEnteredGamepadTextLengthDelegate( IntPtr self );
		private GetEnteredGamepadTextLengthDelegate GetEnteredGamepadTextLengthDelegatePointer;
		
		#endregion
		public uint GetEnteredGamepadTextLength()
		{
			return GetEnteredGamepadTextLengthDelegatePointer( Self );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		public delegate bool GetEnteredGamepadTextInputDelegate( IntPtr self, StringBuilder pchText, uint cchText );
		private GetEnteredGamepadTextInputDelegate GetEnteredGamepadTextInputDelegatePointer;
		
		#endregion
		public bool GetEnteredGamepadTextInput( StringBuilder pchText, uint cchText )
		{
			return GetEnteredGamepadTextInputDelegatePointer( Self, pchText, cchText );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		public delegate IntPtr GetSteamUILanguageDelegate( IntPtr self );
		private GetSteamUILanguageDelegate GetSteamUILanguageDelegatePointer;
		
		#endregion
		public string GetSteamUILanguage()
		{
			return GetString( GetSteamUILanguageDelegatePointer( Self ) );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		public delegate bool IsSteamRunningInVRDelegate( IntPtr self );
		private IsSteamRunningInVRDelegate IsSteamRunningInVRDelegatePointer;
		
		#endregion
		public bool IsSteamRunningInVR()
		{
			return IsSteamRunningInVRDelegatePointer( Self );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		public delegate void SetOverlayNotificationInsetDelegate( IntPtr self, int nHorizontalInset, int nVerticalInset );
		private SetOverlayNotificationInsetDelegate SetOverlayNotificationInsetDelegatePointer;
		
		#endregion
		public void SetOverlayNotificationInset( int nHorizontalInset, int nVerticalInset )
		{
			SetOverlayNotificationInsetDelegatePointer( Self, nHorizontalInset, nVerticalInset );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		public delegate bool IsSteamInBigPictureModeDelegate( IntPtr self );
		private IsSteamInBigPictureModeDelegate IsSteamInBigPictureModeDelegatePointer;
		
		#endregion
		public bool IsSteamInBigPictureMode()
		{
			return IsSteamInBigPictureModeDelegatePointer( Self );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		public delegate void StartVRDashboardDelegate( IntPtr self );
		private StartVRDashboardDelegate StartVRDashboardDelegatePointer;
		
		#endregion
		public void StartVRDashboard()
		{
			StartVRDashboardDelegatePointer( Self );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		public delegate bool IsVRHeadsetStreamingEnabledDelegate( IntPtr self );
		private IsVRHeadsetStreamingEnabledDelegate IsVRHeadsetStreamingEnabledDelegatePointer;
		
		#endregion
		public bool IsVRHeadsetStreamingEnabled()
		{
			return IsVRHeadsetStreamingEnabledDelegatePointer( Self );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		public delegate void SetVRHeadsetStreamingEnabledDelegate( IntPtr self, [MarshalAs( UnmanagedType.U1 )] bool bEnabled );
		private SetVRHeadsetStreamingEnabledDelegate SetVRHeadsetStreamingEnabledDelegatePointer;
		
		#endregion
		public void SetVRHeadsetStreamingEnabled( [MarshalAs( UnmanagedType.U1 )] bool bEnabled )
		{
			SetVRHeadsetStreamingEnabledDelegatePointer( Self, bEnabled );
		}
		
	}
}
