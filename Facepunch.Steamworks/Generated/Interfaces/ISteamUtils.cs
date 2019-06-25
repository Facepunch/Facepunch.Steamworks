using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Steamworks.Data;


namespace Steamworks
{
	internal class ISteamUtils : SteamInterface
	{
		public override string InterfaceName => "SteamUtils009";
		
		public override void InitInternals()
		{
			_GetSecondsSinceAppActive = Marshal.GetDelegateForFunctionPointer<FGetSecondsSinceAppActive>( Marshal.ReadIntPtr( VTable, 0 ) );
			_GetSecondsSinceComputerActive = Marshal.GetDelegateForFunctionPointer<FGetSecondsSinceComputerActive>( Marshal.ReadIntPtr( VTable, 8 ) );
			_GetConnectedUniverse = Marshal.GetDelegateForFunctionPointer<FGetConnectedUniverse>( Marshal.ReadIntPtr( VTable, 16 ) );
			_GetServerRealTime = Marshal.GetDelegateForFunctionPointer<FGetServerRealTime>( Marshal.ReadIntPtr( VTable, 24 ) );
			_GetIPCountry = Marshal.GetDelegateForFunctionPointer<FGetIPCountry>( Marshal.ReadIntPtr( VTable, 32 ) );
			_GetImageSize = Marshal.GetDelegateForFunctionPointer<FGetImageSize>( Marshal.ReadIntPtr( VTable, 40 ) );
			_GetImageRGBA = Marshal.GetDelegateForFunctionPointer<FGetImageRGBA>( Marshal.ReadIntPtr( VTable, 48 ) );
			_GetCSERIPPort = Marshal.GetDelegateForFunctionPointer<FGetCSERIPPort>( Marshal.ReadIntPtr( VTable, 56 ) );
			_GetCurrentBatteryPower = Marshal.GetDelegateForFunctionPointer<FGetCurrentBatteryPower>( Marshal.ReadIntPtr( VTable, 64 ) );
			_GetAppID = Marshal.GetDelegateForFunctionPointer<FGetAppID>( Marshal.ReadIntPtr( VTable, 72 ) );
			_SetOverlayNotificationPosition = Marshal.GetDelegateForFunctionPointer<FSetOverlayNotificationPosition>( Marshal.ReadIntPtr( VTable, 80 ) );
			_IsAPICallCompleted = Marshal.GetDelegateForFunctionPointer<FIsAPICallCompleted>( Marshal.ReadIntPtr( VTable, 88 ) );
			_GetAPICallFailureReason = Marshal.GetDelegateForFunctionPointer<FGetAPICallFailureReason>( Marshal.ReadIntPtr( VTable, 96 ) );
			_GetAPICallResult = Marshal.GetDelegateForFunctionPointer<FGetAPICallResult>( Marshal.ReadIntPtr( VTable, 104 ) );
			_RunFrame = Marshal.GetDelegateForFunctionPointer<FRunFrame>( Marshal.ReadIntPtr( VTable, 112 ) );
			_GetIPCCallCount = Marshal.GetDelegateForFunctionPointer<FGetIPCCallCount>( Marshal.ReadIntPtr( VTable, 120 ) );
			_SetWarningMessageHook = Marshal.GetDelegateForFunctionPointer<FSetWarningMessageHook>( Marshal.ReadIntPtr( VTable, 128 ) );
			_IsOverlayEnabled = Marshal.GetDelegateForFunctionPointer<FIsOverlayEnabled>( Marshal.ReadIntPtr( VTable, 136 ) );
			_BOverlayNeedsPresent = Marshal.GetDelegateForFunctionPointer<FBOverlayNeedsPresent>( Marshal.ReadIntPtr( VTable, 144 ) );
			_CheckFileSignature = Marshal.GetDelegateForFunctionPointer<FCheckFileSignature>( Marshal.ReadIntPtr( VTable, 152 ) );
			_ShowGamepadTextInput = Marshal.GetDelegateForFunctionPointer<FShowGamepadTextInput>( Marshal.ReadIntPtr( VTable, 160 ) );
			_GetEnteredGamepadTextLength = Marshal.GetDelegateForFunctionPointer<FGetEnteredGamepadTextLength>( Marshal.ReadIntPtr( VTable, 168 ) );
			_GetEnteredGamepadTextInput = Marshal.GetDelegateForFunctionPointer<FGetEnteredGamepadTextInput>( Marshal.ReadIntPtr( VTable, 176 ) );
			_GetSteamUILanguage = Marshal.GetDelegateForFunctionPointer<FGetSteamUILanguage>( Marshal.ReadIntPtr( VTable, 184 ) );
			_IsSteamRunningInVR = Marshal.GetDelegateForFunctionPointer<FIsSteamRunningInVR>( Marshal.ReadIntPtr( VTable, 192 ) );
			_SetOverlayNotificationInset = Marshal.GetDelegateForFunctionPointer<FSetOverlayNotificationInset>( Marshal.ReadIntPtr( VTable, 200 ) );
			_IsSteamInBigPictureMode = Marshal.GetDelegateForFunctionPointer<FIsSteamInBigPictureMode>( Marshal.ReadIntPtr( VTable, 208 ) );
			_StartVRDashboard = Marshal.GetDelegateForFunctionPointer<FStartVRDashboard>( Marshal.ReadIntPtr( VTable, 216 ) );
			_IsVRHeadsetStreamingEnabled = Marshal.GetDelegateForFunctionPointer<FIsVRHeadsetStreamingEnabled>( Marshal.ReadIntPtr( VTable, 224 ) );
			_SetVRHeadsetStreamingEnabled = Marshal.GetDelegateForFunctionPointer<FSetVRHeadsetStreamingEnabled>( Marshal.ReadIntPtr( VTable, 232 ) );
		}
		internal override void Shutdown()
		{
			base.Shutdown();
			
			_GetSecondsSinceAppActive = null;
			_GetSecondsSinceComputerActive = null;
			_GetConnectedUniverse = null;
			_GetServerRealTime = null;
			_GetIPCountry = null;
			_GetImageSize = null;
			_GetImageRGBA = null;
			_GetCSERIPPort = null;
			_GetCurrentBatteryPower = null;
			_GetAppID = null;
			_SetOverlayNotificationPosition = null;
			_IsAPICallCompleted = null;
			_GetAPICallFailureReason = null;
			_GetAPICallResult = null;
			_RunFrame = null;
			_GetIPCCallCount = null;
			_SetWarningMessageHook = null;
			_IsOverlayEnabled = null;
			_BOverlayNeedsPresent = null;
			_CheckFileSignature = null;
			_ShowGamepadTextInput = null;
			_GetEnteredGamepadTextLength = null;
			_GetEnteredGamepadTextInput = null;
			_GetSteamUILanguage = null;
			_IsSteamRunningInVR = null;
			_SetOverlayNotificationInset = null;
			_IsSteamInBigPictureMode = null;
			_StartVRDashboard = null;
			_IsVRHeadsetStreamingEnabled = null;
			_SetVRHeadsetStreamingEnabled = null;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate uint FGetSecondsSinceAppActive( IntPtr self );
		private FGetSecondsSinceAppActive _GetSecondsSinceAppActive;
		
		#endregion
		internal uint GetSecondsSinceAppActive()
		{
			return _GetSecondsSinceAppActive( Self );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate uint FGetSecondsSinceComputerActive( IntPtr self );
		private FGetSecondsSinceComputerActive _GetSecondsSinceComputerActive;
		
		#endregion
		internal uint GetSecondsSinceComputerActive()
		{
			return _GetSecondsSinceComputerActive( Self );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate Universe FGetConnectedUniverse( IntPtr self );
		private FGetConnectedUniverse _GetConnectedUniverse;
		
		#endregion
		internal Universe GetConnectedUniverse()
		{
			return _GetConnectedUniverse( Self );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate uint FGetServerRealTime( IntPtr self );
		private FGetServerRealTime _GetServerRealTime;
		
		#endregion
		internal uint GetServerRealTime()
		{
			return _GetServerRealTime( Self );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringFromNative ) )]
		private delegate string FGetIPCountry( IntPtr self );
		private FGetIPCountry _GetIPCountry;
		
		#endregion
		internal string GetIPCountry()
		{
			return _GetIPCountry( Self );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FGetImageSize( IntPtr self, int iImage, ref uint pnWidth, ref uint pnHeight );
		private FGetImageSize _GetImageSize;
		
		#endregion
		internal bool GetImageSize( int iImage, ref uint pnWidth, ref uint pnHeight )
		{
			return _GetImageSize( Self, iImage, ref pnWidth, ref pnHeight );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FGetImageRGBA( IntPtr self, int iImage, [In,Out] byte[]  pubDest, int nDestBufferSize );
		private FGetImageRGBA _GetImageRGBA;
		
		#endregion
		internal bool GetImageRGBA( int iImage, [In,Out] byte[]  pubDest, int nDestBufferSize )
		{
			return _GetImageRGBA( Self, iImage, pubDest, nDestBufferSize );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FGetCSERIPPort( IntPtr self, ref uint unIP, ref ushort usPort );
		private FGetCSERIPPort _GetCSERIPPort;
		
		#endregion
		internal bool GetCSERIPPort( ref uint unIP, ref ushort usPort )
		{
			return _GetCSERIPPort( Self, ref unIP, ref usPort );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate byte FGetCurrentBatteryPower( IntPtr self );
		private FGetCurrentBatteryPower _GetCurrentBatteryPower;
		
		#endregion
		internal byte GetCurrentBatteryPower()
		{
			return _GetCurrentBatteryPower( Self );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate uint FGetAppID( IntPtr self );
		private FGetAppID _GetAppID;
		
		#endregion
		internal uint GetAppID()
		{
			return _GetAppID( Self );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate void FSetOverlayNotificationPosition( IntPtr self, NotificationPosition eNotificationPosition );
		private FSetOverlayNotificationPosition _SetOverlayNotificationPosition;
		
		#endregion
		internal void SetOverlayNotificationPosition( NotificationPosition eNotificationPosition )
		{
			_SetOverlayNotificationPosition( Self, eNotificationPosition );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FIsAPICallCompleted( IntPtr self, SteamAPICall_t hSteamAPICall, [MarshalAs( UnmanagedType.U1 )] ref bool pbFailed );
		private FIsAPICallCompleted _IsAPICallCompleted;
		
		#endregion
		internal bool IsAPICallCompleted( SteamAPICall_t hSteamAPICall, [MarshalAs( UnmanagedType.U1 )] ref bool pbFailed )
		{
			return _IsAPICallCompleted( Self, hSteamAPICall, ref pbFailed );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate SteamAPICallFailure FGetAPICallFailureReason( IntPtr self, SteamAPICall_t hSteamAPICall );
		private FGetAPICallFailureReason _GetAPICallFailureReason;
		
		#endregion
		internal SteamAPICallFailure GetAPICallFailureReason( SteamAPICall_t hSteamAPICall )
		{
			return _GetAPICallFailureReason( Self, hSteamAPICall );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FGetAPICallResult( IntPtr self, SteamAPICall_t hSteamAPICall, IntPtr pCallback, int cubCallback, int iCallbackExpected, [MarshalAs( UnmanagedType.U1 )] ref bool pbFailed );
		private FGetAPICallResult _GetAPICallResult;
		
		#endregion
		internal bool GetAPICallResult( SteamAPICall_t hSteamAPICall, IntPtr pCallback, int cubCallback, int iCallbackExpected, [MarshalAs( UnmanagedType.U1 )] ref bool pbFailed )
		{
			return _GetAPICallResult( Self, hSteamAPICall, pCallback, cubCallback, iCallbackExpected, ref pbFailed );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate void FRunFrame( IntPtr self );
		private FRunFrame _RunFrame;
		
		#endregion
		internal void RunFrame()
		{
			_RunFrame( Self );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate uint FGetIPCCallCount( IntPtr self );
		private FGetIPCCallCount _GetIPCCallCount;
		
		#endregion
		internal uint GetIPCCallCount()
		{
			return _GetIPCCallCount( Self );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate void FSetWarningMessageHook( IntPtr self, IntPtr pFunction );
		private FSetWarningMessageHook _SetWarningMessageHook;
		
		#endregion
		internal void SetWarningMessageHook( IntPtr pFunction )
		{
			_SetWarningMessageHook( Self, pFunction );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FIsOverlayEnabled( IntPtr self );
		private FIsOverlayEnabled _IsOverlayEnabled;
		
		#endregion
		internal bool IsOverlayEnabled()
		{
			return _IsOverlayEnabled( Self );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FBOverlayNeedsPresent( IntPtr self );
		private FBOverlayNeedsPresent _BOverlayNeedsPresent;
		
		#endregion
		internal bool BOverlayNeedsPresent()
		{
			return _BOverlayNeedsPresent( Self );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate SteamAPICall_t FCheckFileSignature( IntPtr self, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string szFileName );
		private FCheckFileSignature _CheckFileSignature;
		
		#endregion
		internal async Task<CheckFileSignature_t?> CheckFileSignature( [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string szFileName )
		{
			return await CheckFileSignature_t.GetResultAsync( _CheckFileSignature( Self, szFileName ) );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FShowGamepadTextInput( IntPtr self, GamepadTextInputMode eInputMode, GamepadTextInputLineMode eLineInputMode, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchDescription, uint unCharMax, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchExistingText );
		private FShowGamepadTextInput _ShowGamepadTextInput;
		
		#endregion
		internal bool ShowGamepadTextInput( GamepadTextInputMode eInputMode, GamepadTextInputLineMode eLineInputMode, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchDescription, uint unCharMax, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchExistingText )
		{
			return _ShowGamepadTextInput( Self, eInputMode, eLineInputMode, pchDescription, unCharMax, pchExistingText );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate uint FGetEnteredGamepadTextLength( IntPtr self );
		private FGetEnteredGamepadTextLength _GetEnteredGamepadTextLength;
		
		#endregion
		internal uint GetEnteredGamepadTextLength()
		{
			return _GetEnteredGamepadTextLength( Self );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FGetEnteredGamepadTextInput( IntPtr self, StringBuilder pchText, uint cchText );
		private FGetEnteredGamepadTextInput _GetEnteredGamepadTextInput;
		
		#endregion
		internal bool GetEnteredGamepadTextInput( StringBuilder pchText, uint cchText )
		{
			return _GetEnteredGamepadTextInput( Self, pchText, cchText );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringFromNative ) )]
		private delegate string FGetSteamUILanguage( IntPtr self );
		private FGetSteamUILanguage _GetSteamUILanguage;
		
		#endregion
		internal string GetSteamUILanguage()
		{
			return _GetSteamUILanguage( Self );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FIsSteamRunningInVR( IntPtr self );
		private FIsSteamRunningInVR _IsSteamRunningInVR;
		
		#endregion
		internal bool IsSteamRunningInVR()
		{
			return _IsSteamRunningInVR( Self );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate void FSetOverlayNotificationInset( IntPtr self, int nHorizontalInset, int nVerticalInset );
		private FSetOverlayNotificationInset _SetOverlayNotificationInset;
		
		#endregion
		internal void SetOverlayNotificationInset( int nHorizontalInset, int nVerticalInset )
		{
			_SetOverlayNotificationInset( Self, nHorizontalInset, nVerticalInset );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FIsSteamInBigPictureMode( IntPtr self );
		private FIsSteamInBigPictureMode _IsSteamInBigPictureMode;
		
		#endregion
		internal bool IsSteamInBigPictureMode()
		{
			return _IsSteamInBigPictureMode( Self );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate void FStartVRDashboard( IntPtr self );
		private FStartVRDashboard _StartVRDashboard;
		
		#endregion
		internal void StartVRDashboard()
		{
			_StartVRDashboard( Self );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FIsVRHeadsetStreamingEnabled( IntPtr self );
		private FIsVRHeadsetStreamingEnabled _IsVRHeadsetStreamingEnabled;
		
		#endregion
		internal bool IsVRHeadsetStreamingEnabled()
		{
			return _IsVRHeadsetStreamingEnabled( Self );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate void FSetVRHeadsetStreamingEnabled( IntPtr self, [MarshalAs( UnmanagedType.U1 )] bool bEnabled );
		private FSetVRHeadsetStreamingEnabled _SetVRHeadsetStreamingEnabled;
		
		#endregion
		internal void SetVRHeadsetStreamingEnabled( [MarshalAs( UnmanagedType.U1 )] bool bEnabled )
		{
			_SetVRHeadsetStreamingEnabled( Self, bEnabled );
		}
		
	}
}
