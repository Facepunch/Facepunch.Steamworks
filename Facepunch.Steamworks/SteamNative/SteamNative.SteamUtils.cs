using System;
using System.Runtime.InteropServices;

namespace SteamNative
{
	public unsafe class SteamUtils : IDisposable
	{
		//
		// Holds a platform specific implentation
		//
		internal Platform.Interface _pi;
		
		//
		// Constructor decides which implementation to use based on current platform
		//
		public SteamUtils( IntPtr pointer )
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
		
		// bool
		public bool BOverlayNeedsPresent()
		{
			return _pi.ISteamUtils_BOverlayNeedsPresent();
		}
		
		// SteamAPICall_t
		public SteamAPICall_t CheckFileSignature( string szFileName /*const char **/ )
		{
			return _pi.ISteamUtils_CheckFileSignature( szFileName /*C*/ );
		}
		
		// SteamAPICallFailure
		public SteamAPICallFailure GetAPICallFailureReason( SteamAPICall_t hSteamAPICall /*SteamAPICall_t*/ )
		{
			return _pi.ISteamUtils_GetAPICallFailureReason( hSteamAPICall.Value /*C*/ );
		}
		
		// bool
		public bool GetAPICallResult( SteamAPICall_t hSteamAPICall /*SteamAPICall_t*/, IntPtr pCallback /*void **/, int cubCallback /*int*/, int iCallbackExpected /*int*/, ref bool pbFailed /*bool **/ )
		{
			return _pi.ISteamUtils_GetAPICallResult( hSteamAPICall.Value /*C*/, (IntPtr) pCallback /*C*/, cubCallback /*C*/, iCallbackExpected /*C*/, ref pbFailed /*A*/ );
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
			return _pi.ISteamUtils_GetCSERIPPort( out unIP /*B*/, out usPort /*B*/ );
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
			bSuccess = _pi.ISteamUtils_GetEnteredGamepadTextInput( pchText_sb /*C*/, cchText /*C*/ );
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
			return _pi.ISteamUtils_GetImageRGBA( iImage /*C*/, (IntPtr) pubDest, nDestBufferSize /*C*/ );
		}
		
		// bool
		public bool GetImageSize( int iImage /*int*/, out uint pnWidth /*uint32 **/, out uint pnHeight /*uint32 **/ )
		{
			return _pi.ISteamUtils_GetImageSize( iImage /*C*/, out pnWidth /*B*/, out pnHeight /*B*/ );
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
		public bool IsAPICallCompleted( SteamAPICall_t hSteamAPICall /*SteamAPICall_t*/, ref bool pbFailed /*bool **/ )
		{
			return _pi.ISteamUtils_IsAPICallCompleted( hSteamAPICall.Value /*C*/, ref pbFailed /*A*/ );
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
			_pi.ISteamUtils_SetOverlayNotificationInset( nHorizontalInset /*C*/, nVerticalInset /*C*/ );
		}
		
		// void
		public void SetOverlayNotificationPosition( NotificationPosition eNotificationPosition /*ENotificationPosition*/ )
		{
			_pi.ISteamUtils_SetOverlayNotificationPosition( eNotificationPosition /*C*/ );
		}
		
		// void
		public void SetWarningMessageHook( IntPtr pFunction /*SteamAPIWarningMessageHook_t*/ )
		{
			_pi.ISteamUtils_SetWarningMessageHook( (IntPtr) pFunction /*C*/ );
		}
		
		// bool
		public bool ShowGamepadTextInput( GamepadTextInputMode eInputMode /*EGamepadTextInputMode*/, GamepadTextInputLineMode eLineInputMode /*EGamepadTextInputLineMode*/, string pchDescription /*const char **/, uint unCharMax /*uint32*/, string pchExistingText /*const char **/ )
		{
			return _pi.ISteamUtils_ShowGamepadTextInput( eInputMode /*C*/, eLineInputMode /*C*/, pchDescription /*C*/, unCharMax /*C*/, pchExistingText /*C*/ );
		}
		
		// void
		public void StartVRDashboard()
		{
			_pi.ISteamUtils_StartVRDashboard();
		}
		
	}
}
