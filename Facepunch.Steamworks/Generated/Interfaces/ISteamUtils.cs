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
		}
		internal override void Shutdown()
		{
			base.Shutdown();
			
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate uint FGetSecondsSinceAppActive( IntPtr self );
		private FGetSecondsSinceAppActive _GetSecondsSinceAppActive;
		
		#endregion
		internal uint GetSecondsSinceAppActive()
		{
			var returnValue = _GetSecondsSinceAppActive( Self );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate uint FGetSecondsSinceComputerActive( IntPtr self );
		private FGetSecondsSinceComputerActive _GetSecondsSinceComputerActive;
		
		#endregion
		internal uint GetSecondsSinceComputerActive()
		{
			var returnValue = _GetSecondsSinceComputerActive( Self );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate Universe FGetConnectedUniverse( IntPtr self );
		private FGetConnectedUniverse _GetConnectedUniverse;
		
		#endregion
		internal Universe GetConnectedUniverse()
		{
			var returnValue = _GetConnectedUniverse( Self );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate uint FGetServerRealTime( IntPtr self );
		private FGetServerRealTime _GetServerRealTime;
		
		#endregion
		internal uint GetServerRealTime()
		{
			var returnValue = _GetServerRealTime( Self );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate Utf8StringPointer FGetIPCountry( IntPtr self );
		private FGetIPCountry _GetIPCountry;
		
		#endregion
		internal string GetIPCountry()
		{
			var returnValue = _GetIPCountry( Self );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FGetImageSize( IntPtr self, int iImage, ref uint pnWidth, ref uint pnHeight );
		private FGetImageSize _GetImageSize;
		
		#endregion
		internal bool GetImageSize( int iImage, ref uint pnWidth, ref uint pnHeight )
		{
			var returnValue = _GetImageSize( Self, iImage, ref pnWidth, ref pnHeight );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FGetImageRGBA( IntPtr self, int iImage, [In,Out] byte[]  pubDest, int nDestBufferSize );
		private FGetImageRGBA _GetImageRGBA;
		
		#endregion
		internal bool GetImageRGBA( int iImage, [In,Out] byte[]  pubDest, int nDestBufferSize )
		{
			var returnValue = _GetImageRGBA( Self, iImage, pubDest, nDestBufferSize );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FGetCSERIPPort( IntPtr self, ref uint unIP, ref ushort usPort );
		private FGetCSERIPPort _GetCSERIPPort;
		
		#endregion
		internal bool GetCSERIPPort( ref uint unIP, ref ushort usPort )
		{
			var returnValue = _GetCSERIPPort( Self, ref unIP, ref usPort );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate byte FGetCurrentBatteryPower( IntPtr self );
		private FGetCurrentBatteryPower _GetCurrentBatteryPower;
		
		#endregion
		internal byte GetCurrentBatteryPower()
		{
			var returnValue = _GetCurrentBatteryPower( Self );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate uint FGetAppID( IntPtr self );
		private FGetAppID _GetAppID;
		
		#endregion
		internal uint GetAppID()
		{
			var returnValue = _GetAppID( Self );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate void FSetOverlayNotificationPosition( IntPtr self, NotificationPosition eNotificationPosition );
		private FSetOverlayNotificationPosition _SetOverlayNotificationPosition;
		
		#endregion
		internal void SetOverlayNotificationPosition( NotificationPosition eNotificationPosition )
		{
			_SetOverlayNotificationPosition( Self, eNotificationPosition );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FIsAPICallCompleted( IntPtr self, SteamAPICall_t hSteamAPICall, [MarshalAs( UnmanagedType.U1 )] ref bool pbFailed );
		private FIsAPICallCompleted _IsAPICallCompleted;
		
		#endregion
		internal bool IsAPICallCompleted( SteamAPICall_t hSteamAPICall, [MarshalAs( UnmanagedType.U1 )] ref bool pbFailed )
		{
			var returnValue = _IsAPICallCompleted( Self, hSteamAPICall, ref pbFailed );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate SteamAPICallFailure FGetAPICallFailureReason( IntPtr self, SteamAPICall_t hSteamAPICall );
		private FGetAPICallFailureReason _GetAPICallFailureReason;
		
		#endregion
		internal SteamAPICallFailure GetAPICallFailureReason( SteamAPICall_t hSteamAPICall )
		{
			var returnValue = _GetAPICallFailureReason( Self, hSteamAPICall );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FGetAPICallResult( IntPtr self, SteamAPICall_t hSteamAPICall, IntPtr pCallback, int cubCallback, int iCallbackExpected, [MarshalAs( UnmanagedType.U1 )] ref bool pbFailed );
		private FGetAPICallResult _GetAPICallResult;
		
		#endregion
		internal bool GetAPICallResult( SteamAPICall_t hSteamAPICall, IntPtr pCallback, int cubCallback, int iCallbackExpected, [MarshalAs( UnmanagedType.U1 )] ref bool pbFailed )
		{
			var returnValue = _GetAPICallResult( Self, hSteamAPICall, pCallback, cubCallback, iCallbackExpected, ref pbFailed );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate void FRunFrame( IntPtr self );
		private FRunFrame _RunFrame;
		
		#endregion
		internal void RunFrame()
		{
			_RunFrame( Self );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate uint FGetIPCCallCount( IntPtr self );
		private FGetIPCCallCount _GetIPCCallCount;
		
		#endregion
		internal uint GetIPCCallCount()
		{
			var returnValue = _GetIPCCallCount( Self );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate void FSetWarningMessageHook( IntPtr self, IntPtr pFunction );
		private FSetWarningMessageHook _SetWarningMessageHook;
		
		#endregion
		internal void SetWarningMessageHook( IntPtr pFunction )
		{
			_SetWarningMessageHook( Self, pFunction );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FIsOverlayEnabled( IntPtr self );
		private FIsOverlayEnabled _IsOverlayEnabled;
		
		#endregion
		internal bool IsOverlayEnabled()
		{
			var returnValue = _IsOverlayEnabled( Self );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FBOverlayNeedsPresent( IntPtr self );
		private FBOverlayNeedsPresent _BOverlayNeedsPresent;
		
		#endregion
		internal bool BOverlayNeedsPresent()
		{
			var returnValue = _BOverlayNeedsPresent( Self );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate SteamAPICall_t FCheckFileSignature( IntPtr self, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string szFileName );
		private FCheckFileSignature _CheckFileSignature;
		
		#endregion
		internal async Task<CheckFileSignature_t?> CheckFileSignature( [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string szFileName )
		{
			var returnValue = _CheckFileSignature( Self, szFileName );
			return await CheckFileSignature_t.GetResultAsync( returnValue );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FShowGamepadTextInput( IntPtr self, GamepadTextInputMode eInputMode, GamepadTextInputLineMode eLineInputMode, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchDescription, uint unCharMax, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchExistingText );
		private FShowGamepadTextInput _ShowGamepadTextInput;
		
		#endregion
		internal bool ShowGamepadTextInput( GamepadTextInputMode eInputMode, GamepadTextInputLineMode eLineInputMode, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchDescription, uint unCharMax, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchExistingText )
		{
			var returnValue = _ShowGamepadTextInput( Self, eInputMode, eLineInputMode, pchDescription, unCharMax, pchExistingText );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate uint FGetEnteredGamepadTextLength( IntPtr self );
		private FGetEnteredGamepadTextLength _GetEnteredGamepadTextLength;
		
		#endregion
		internal uint GetEnteredGamepadTextLength()
		{
			var returnValue = _GetEnteredGamepadTextLength( Self );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FGetEnteredGamepadTextInput( IntPtr self, IntPtr pchText, uint cchText );
		private FGetEnteredGamepadTextInput _GetEnteredGamepadTextInput;
		
		#endregion
		internal bool GetEnteredGamepadTextInput( out string pchText )
		{
			IntPtr mempchText = Helpers.TakeMemory();
			var returnValue = _GetEnteredGamepadTextInput( Self, mempchText, (1024 * 32) );
			pchText = Helpers.MemoryToString( mempchText );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate Utf8StringPointer FGetSteamUILanguage( IntPtr self );
		private FGetSteamUILanguage _GetSteamUILanguage;
		
		#endregion
		internal string GetSteamUILanguage()
		{
			var returnValue = _GetSteamUILanguage( Self );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FIsSteamRunningInVR( IntPtr self );
		private FIsSteamRunningInVR _IsSteamRunningInVR;
		
		#endregion
		internal bool IsSteamRunningInVR()
		{
			var returnValue = _IsSteamRunningInVR( Self );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate void FSetOverlayNotificationInset( IntPtr self, int nHorizontalInset, int nVerticalInset );
		private FSetOverlayNotificationInset _SetOverlayNotificationInset;
		
		#endregion
		internal void SetOverlayNotificationInset( int nHorizontalInset, int nVerticalInset )
		{
			_SetOverlayNotificationInset( Self, nHorizontalInset, nVerticalInset );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FIsSteamInBigPictureMode( IntPtr self );
		private FIsSteamInBigPictureMode _IsSteamInBigPictureMode;
		
		#endregion
		internal bool IsSteamInBigPictureMode()
		{
			var returnValue = _IsSteamInBigPictureMode( Self );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate void FStartVRDashboard( IntPtr self );
		private FStartVRDashboard _StartVRDashboard;
		
		#endregion
		internal void StartVRDashboard()
		{
			_StartVRDashboard( Self );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FIsVRHeadsetStreamingEnabled( IntPtr self );
		private FIsVRHeadsetStreamingEnabled _IsVRHeadsetStreamingEnabled;
		
		#endregion
		internal bool IsVRHeadsetStreamingEnabled()
		{
			var returnValue = _IsVRHeadsetStreamingEnabled( Self );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate void FSetVRHeadsetStreamingEnabled( IntPtr self, [MarshalAs( UnmanagedType.U1 )] bool bEnabled );
		private FSetVRHeadsetStreamingEnabled _SetVRHeadsetStreamingEnabled;
		
		#endregion
		internal void SetVRHeadsetStreamingEnabled( [MarshalAs( UnmanagedType.U1 )] bool bEnabled )
		{
			_SetVRHeadsetStreamingEnabled( Self, bEnabled );
		}
		
	}
}
