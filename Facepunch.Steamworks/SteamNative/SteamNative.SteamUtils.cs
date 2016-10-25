using System;
using System.Runtime.InteropServices;

namespace SteamNative
{
	public unsafe class SteamUtils
	{
		internal Platform.Interface _pi;
		
		public SteamUtils( IntPtr pointer )
		{
			if ( Platform.IsWindows64 ) _pi = new Platform.Win64( pointer );
			else if ( Platform.IsWindows32 ) _pi = new Platform.Win32( pointer );
			else if ( Platform.IsLinux32 ) _pi = new Platform.Linux32( pointer );
			else if ( Platform.IsLinux64 ) _pi = new Platform.Linux64( pointer );
			else if ( Platform.IsOsx ) _pi = new Platform.Mac( pointer );
		}
		
		public bool IsValid{ get{ return _pi != null && _pi.IsValid; } }
		
		// bool
		public bool BOverlayNeedsPresent()
		{
			return _pi.ISteamUtils_BOverlayNeedsPresent();
		}
		
		// SteamAPICall_t
		public SteamAPICall_t CheckFileSignature( string szFileName /*const char **/ )
		{
			return _pi.ISteamUtils_CheckFileSignature( szFileName );
		}
		
		// SteamAPICallFailure
		public SteamAPICallFailure GetAPICallFailureReason( SteamAPICall_t hSteamAPICall /*SteamAPICall_t*/ )
		{
			return _pi.ISteamUtils_GetAPICallFailureReason( hSteamAPICall );
		}
		
		// bool
		public bool GetAPICallResult( SteamAPICall_t hSteamAPICall /*SteamAPICall_t*/, IntPtr pCallback /*void **/, int cubCallback /*int*/, int iCallbackExpected /*int*/, out bool pbFailed /*bool **/ )
		{
			return _pi.ISteamUtils_GetAPICallResult( hSteamAPICall, (IntPtr) pCallback, cubCallback, iCallbackExpected, out pbFailed );
		}
		
		// uint
		public uint GetAppID()
		{
			return _pi.ISteamUtils_GetAppID();
		}
		
		// Universe
		public Universe GetConnectedUniverse()
		{
			return _pi.ISteamUtils_GetConnectedUniverse();
		}
		
		// bool
		public bool GetCSERIPPort( out uint unIP /*uint32 **/, out ushort usPort /*uint16 **/ )
		{
			return _pi.ISteamUtils_GetCSERIPPort( out unIP, out usPort );
		}
		
		// byte
		public byte GetCurrentBatteryPower()
		{
			return _pi.ISteamUtils_GetCurrentBatteryPower();
		}
		
		// bool
		// with: Detect_StringFetch True
		public string GetEnteredGamepadTextInput()
		{
			bool bSuccess = default( bool );
			System.Text.StringBuilder pchText_sb = new System.Text.StringBuilder( 4096 );
			uint cchText = 4096;
			bSuccess = _pi.ISteamUtils_GetEnteredGamepadTextInput( pchText_sb, cchText );
			if ( !bSuccess ) return null;
			return pchText_sb.ToString();
		}
		
		// uint
		public uint GetEnteredGamepadTextLength()
		{
			return _pi.ISteamUtils_GetEnteredGamepadTextLength();
		}
		
		// bool
		public bool GetImageRGBA( int iImage /*int*/, IntPtr pubDest /*uint8 **/, int nDestBufferSize /*int*/ )
		{
			return _pi.ISteamUtils_GetImageRGBA( iImage, (IntPtr) pubDest, nDestBufferSize );
		}
		
		// bool
		public bool GetImageSize( int iImage /*int*/, out uint pnWidth /*uint32 **/, out uint pnHeight /*uint32 **/ )
		{
			return _pi.ISteamUtils_GetImageSize( iImage, out pnWidth, out pnHeight );
		}
		
		// uint
		public uint GetIPCCallCount()
		{
			return _pi.ISteamUtils_GetIPCCallCount();
		}
		
		// string
		// with: Detect_StringReturn
		public string GetIPCountry()
		{
			IntPtr string_pointer;
			string_pointer = _pi.ISteamUtils_GetIPCountry();
			return Marshal.PtrToStringAnsi( string_pointer );
		}
		
		// uint
		public uint GetSecondsSinceAppActive()
		{
			return _pi.ISteamUtils_GetSecondsSinceAppActive();
		}
		
		// uint
		public uint GetSecondsSinceComputerActive()
		{
			return _pi.ISteamUtils_GetSecondsSinceComputerActive();
		}
		
		// uint
		public uint GetServerRealTime()
		{
			return _pi.ISteamUtils_GetServerRealTime();
		}
		
		// string
		// with: Detect_StringReturn
		public string GetSteamUILanguage()
		{
			IntPtr string_pointer;
			string_pointer = _pi.ISteamUtils_GetSteamUILanguage();
			return Marshal.PtrToStringAnsi( string_pointer );
		}
		
		// bool
		public bool IsAPICallCompleted( SteamAPICall_t hSteamAPICall /*SteamAPICall_t*/, out bool pbFailed /*bool **/ )
		{
			return _pi.ISteamUtils_IsAPICallCompleted( hSteamAPICall, out pbFailed );
		}
		
		// bool
		public bool IsOverlayEnabled()
		{
			return _pi.ISteamUtils_IsOverlayEnabled();
		}
		
		// bool
		public bool IsSteamInBigPictureMode()
		{
			return _pi.ISteamUtils_IsSteamInBigPictureMode();
		}
		
		// bool
		public bool IsSteamRunningInVR()
		{
			return _pi.ISteamUtils_IsSteamRunningInVR();
		}
		
		// void
		public void SetOverlayNotificationInset( int nHorizontalInset /*int*/, int nVerticalInset /*int*/ )
		{
			_pi.ISteamUtils_SetOverlayNotificationInset( nHorizontalInset, nVerticalInset );
		}
		
		// void
		public void SetOverlayNotificationPosition( NotificationPosition eNotificationPosition /*ENotificationPosition*/ )
		{
			_pi.ISteamUtils_SetOverlayNotificationPosition( eNotificationPosition );
		}
		
		// void
		public void SetWarningMessageHook( IntPtr pFunction /*SteamAPIWarningMessageHook_t*/ )
		{
			_pi.ISteamUtils_SetWarningMessageHook( (IntPtr) pFunction );
		}
		
		// bool
		public bool ShowGamepadTextInput( GamepadTextInputMode eInputMode /*EGamepadTextInputMode*/, GamepadTextInputLineMode eLineInputMode /*EGamepadTextInputLineMode*/, string pchDescription /*const char **/, uint unCharMax /*uint32*/, string pchExistingText /*const char **/ )
		{
			return _pi.ISteamUtils_ShowGamepadTextInput( eInputMode, eLineInputMode, pchDescription, unCharMax, pchExistingText );
		}
		
		// void
		public void StartVRDashboard()
		{
			_pi.ISteamUtils_StartVRDashboard();
		}
		
	}
}
