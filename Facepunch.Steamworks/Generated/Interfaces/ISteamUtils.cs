using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Steamworks.Data;


namespace Steamworks
{
	internal class ISteamUtils : SteamInterface
	{
		public override IntPtr GetInterfacePointer() => GetApi.SteamUtils();
		
		
		internal ISteamUtils()
		{
			SetupInterface();
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamUtils_GetSecondsSinceAppActive")]
		private static extern uint _GetSecondsSinceAppActive( IntPtr self );
		
		#endregion
		internal uint GetSecondsSinceAppActive()
		{
			var returnValue = _GetSecondsSinceAppActive( Self );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamUtils_GetSecondsSinceComputerActive")]
		private static extern uint _GetSecondsSinceComputerActive( IntPtr self );
		
		#endregion
		internal uint GetSecondsSinceComputerActive()
		{
			var returnValue = _GetSecondsSinceComputerActive( Self );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamUtils_GetConnectedUniverse")]
		private static extern Universe _GetConnectedUniverse( IntPtr self );
		
		#endregion
		internal Universe GetConnectedUniverse()
		{
			var returnValue = _GetConnectedUniverse( Self );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamUtils_GetServerRealTime")]
		private static extern uint _GetServerRealTime( IntPtr self );
		
		#endregion
		internal uint GetServerRealTime()
		{
			var returnValue = _GetServerRealTime( Self );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamUtils_GetIPCountry")]
		private static extern Utf8StringPointer _GetIPCountry( IntPtr self );
		
		#endregion
		internal string GetIPCountry()
		{
			var returnValue = _GetIPCountry( Self );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamUtils_GetImageSize")]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _GetImageSize( IntPtr self, int iImage, ref uint pnWidth, ref uint pnHeight );
		
		#endregion
		internal bool GetImageSize( int iImage, ref uint pnWidth, ref uint pnHeight )
		{
			var returnValue = _GetImageSize( Self, iImage, ref pnWidth, ref pnHeight );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamUtils_GetImageRGBA")]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _GetImageRGBA( IntPtr self, int iImage, [In,Out] byte[]  pubDest, int nDestBufferSize );
		
		#endregion
		internal bool GetImageRGBA( int iImage, [In,Out] byte[]  pubDest, int nDestBufferSize )
		{
			var returnValue = _GetImageRGBA( Self, iImage, pubDest, nDestBufferSize );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamUtils_GetCSERIPPort")]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _GetCSERIPPort( IntPtr self, ref uint unIP, ref ushort usPort );
		
		#endregion
		internal bool GetCSERIPPort( ref uint unIP, ref ushort usPort )
		{
			var returnValue = _GetCSERIPPort( Self, ref unIP, ref usPort );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamUtils_GetCurrentBatteryPower")]
		private static extern byte _GetCurrentBatteryPower( IntPtr self );
		
		#endregion
		internal byte GetCurrentBatteryPower()
		{
			var returnValue = _GetCurrentBatteryPower( Self );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamUtils_GetAppID")]
		private static extern uint _GetAppID( IntPtr self );
		
		#endregion
		internal uint GetAppID()
		{
			var returnValue = _GetAppID( Self );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamUtils_SetOverlayNotificationPosition")]
		private static extern void _SetOverlayNotificationPosition( IntPtr self, NotificationPosition eNotificationPosition );
		
		#endregion
		internal void SetOverlayNotificationPosition( NotificationPosition eNotificationPosition )
		{
			_SetOverlayNotificationPosition( Self, eNotificationPosition );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamUtils_IsAPICallCompleted")]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _IsAPICallCompleted( IntPtr self, SteamAPICall_t hSteamAPICall, [MarshalAs( UnmanagedType.U1 )] ref bool pbFailed );
		
		#endregion
		internal bool IsAPICallCompleted( SteamAPICall_t hSteamAPICall, [MarshalAs( UnmanagedType.U1 )] ref bool pbFailed )
		{
			var returnValue = _IsAPICallCompleted( Self, hSteamAPICall, ref pbFailed );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamUtils_GetAPICallFailureReason")]
		private static extern SteamAPICallFailure _GetAPICallFailureReason( IntPtr self, SteamAPICall_t hSteamAPICall );
		
		#endregion
		internal SteamAPICallFailure GetAPICallFailureReason( SteamAPICall_t hSteamAPICall )
		{
			var returnValue = _GetAPICallFailureReason( Self, hSteamAPICall );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamUtils_GetAPICallResult")]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _GetAPICallResult( IntPtr self, SteamAPICall_t hSteamAPICall, IntPtr pCallback, int cubCallback, int iCallbackExpected, [MarshalAs( UnmanagedType.U1 )] ref bool pbFailed );
		
		#endregion
		internal bool GetAPICallResult( SteamAPICall_t hSteamAPICall, IntPtr pCallback, int cubCallback, int iCallbackExpected, [MarshalAs( UnmanagedType.U1 )] ref bool pbFailed )
		{
			var returnValue = _GetAPICallResult( Self, hSteamAPICall, pCallback, cubCallback, iCallbackExpected, ref pbFailed );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamUtils_GetIPCCallCount")]
		private static extern uint _GetIPCCallCount( IntPtr self );
		
		#endregion
		internal uint GetIPCCallCount()
		{
			var returnValue = _GetIPCCallCount( Self );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamUtils_SetWarningMessageHook")]
		private static extern void _SetWarningMessageHook( IntPtr self, IntPtr pFunction );
		
		#endregion
		internal void SetWarningMessageHook( IntPtr pFunction )
		{
			_SetWarningMessageHook( Self, pFunction );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamUtils_IsOverlayEnabled")]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _IsOverlayEnabled( IntPtr self );
		
		#endregion
		internal bool IsOverlayEnabled()
		{
			var returnValue = _IsOverlayEnabled( Self );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamUtils_BOverlayNeedsPresent")]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _BOverlayNeedsPresent( IntPtr self );
		
		#endregion
		internal bool BOverlayNeedsPresent()
		{
			var returnValue = _BOverlayNeedsPresent( Self );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamUtils_CheckFileSignature")]
		private static extern SteamAPICall_t _CheckFileSignature( IntPtr self, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string szFileName );
		
		#endregion
		internal CallbackResult<CheckFileSignature_t> CheckFileSignature( [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string szFileName )
		{
			var returnValue = _CheckFileSignature( Self, szFileName );
			return new CallbackResult<CheckFileSignature_t>( returnValue );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamUtils_ShowGamepadTextInput")]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _ShowGamepadTextInput( IntPtr self, GamepadTextInputMode eInputMode, GamepadTextInputLineMode eLineInputMode, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchDescription, uint unCharMax, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchExistingText );
		
		#endregion
		internal bool ShowGamepadTextInput( GamepadTextInputMode eInputMode, GamepadTextInputLineMode eLineInputMode, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchDescription, uint unCharMax, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchExistingText )
		{
			var returnValue = _ShowGamepadTextInput( Self, eInputMode, eLineInputMode, pchDescription, unCharMax, pchExistingText );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamUtils_GetEnteredGamepadTextLength")]
		private static extern uint _GetEnteredGamepadTextLength( IntPtr self );
		
		#endregion
		internal uint GetEnteredGamepadTextLength()
		{
			var returnValue = _GetEnteredGamepadTextLength( Self );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamUtils_GetEnteredGamepadTextInput")]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _GetEnteredGamepadTextInput( IntPtr self, IntPtr pchText, uint cchText );
		
		#endregion
		internal bool GetEnteredGamepadTextInput( out string pchText )
		{
			IntPtr mempchText = Helpers.TakeMemory();
			var returnValue = _GetEnteredGamepadTextInput( Self, mempchText, (1024 * 32) );
			pchText = Helpers.MemoryToString( mempchText );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamUtils_GetSteamUILanguage")]
		private static extern Utf8StringPointer _GetSteamUILanguage( IntPtr self );
		
		#endregion
		internal string GetSteamUILanguage()
		{
			var returnValue = _GetSteamUILanguage( Self );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamUtils_IsSteamRunningInVR")]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _IsSteamRunningInVR( IntPtr self );
		
		#endregion
		internal bool IsSteamRunningInVR()
		{
			var returnValue = _IsSteamRunningInVR( Self );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamUtils_SetOverlayNotificationInset")]
		private static extern void _SetOverlayNotificationInset( IntPtr self, int nHorizontalInset, int nVerticalInset );
		
		#endregion
		internal void SetOverlayNotificationInset( int nHorizontalInset, int nVerticalInset )
		{
			_SetOverlayNotificationInset( Self, nHorizontalInset, nVerticalInset );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamUtils_IsSteamInBigPictureMode")]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _IsSteamInBigPictureMode( IntPtr self );
		
		#endregion
		internal bool IsSteamInBigPictureMode()
		{
			var returnValue = _IsSteamInBigPictureMode( Self );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamUtils_StartVRDashboard")]
		private static extern void _StartVRDashboard( IntPtr self );
		
		#endregion
		internal void StartVRDashboard()
		{
			_StartVRDashboard( Self );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamUtils_IsVRHeadsetStreamingEnabled")]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _IsVRHeadsetStreamingEnabled( IntPtr self );
		
		#endregion
		internal bool IsVRHeadsetStreamingEnabled()
		{
			var returnValue = _IsVRHeadsetStreamingEnabled( Self );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamUtils_SetVRHeadsetStreamingEnabled")]
		private static extern void _SetVRHeadsetStreamingEnabled( IntPtr self, [MarshalAs( UnmanagedType.U1 )] bool bEnabled );
		
		#endregion
		internal void SetVRHeadsetStreamingEnabled( [MarshalAs( UnmanagedType.U1 )] bool bEnabled )
		{
			_SetVRHeadsetStreamingEnabled( Self, bEnabled );
		}
		
	}
}
