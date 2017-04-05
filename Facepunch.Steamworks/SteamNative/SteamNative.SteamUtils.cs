using System;
using System.Runtime.InteropServices;
using System.Linq;

namespace SteamNative
{
	internal unsafe class SteamUtils : IDisposable
	{
		//
		// Holds a platform specific implentation
		//
		internal Platform.Interface platform;
		internal Facepunch.Steamworks.BaseSteamworks steamworks;
		
		//
		// Constructor decides which implementation to use based on current platform
		//
		internal SteamUtils( Facepunch.Steamworks.BaseSteamworks steamworks, IntPtr pointer )
		{
			this.steamworks = steamworks;
			
			if ( Platform.IsWindows64 ) platform = new Platform.Win64( pointer );
			else if ( Platform.IsWindows32 ) platform = new Platform.Win32( pointer );
			else if ( Platform.IsLinux32 ) platform = new Platform.Linux32( pointer );
			else if ( Platform.IsLinux64 ) platform = new Platform.Linux64( pointer );
			else if ( Platform.IsOsx ) platform = new Platform.Mac( pointer );
		}
		
		//
		// Class is invalid if we don't have a valid implementation
		//
		public bool IsValid{ get{ return platform != null && platform.IsValid; } }
		
		//
		// When shutting down clear all the internals to avoid accidental use
		//
		public virtual void Dispose()
		{
			 if ( platform != null )
			{
				platform.Dispose();
				platform = null;
			}
		}
		
		// bool
		public bool BOverlayNeedsPresent()
		{
			return platform.ISteamUtils_BOverlayNeedsPresent();
		}
		
		// SteamAPICall_t
		public CallbackHandle CheckFileSignature( string szFileName /*const char **/, Action<CheckFileSignature_t, bool> CallbackFunction = null /*Action<CheckFileSignature_t, bool>*/ )
		{
			SteamAPICall_t callback = 0;
			callback = platform.ISteamUtils_CheckFileSignature( szFileName );
			
			if ( CallbackFunction == null ) return null;
			
			return CheckFileSignature_t.CallResult( steamworks, callback, CallbackFunction );
		}
		
		// SteamAPICallFailure
		public SteamAPICallFailure GetAPICallFailureReason( SteamAPICall_t hSteamAPICall /*SteamAPICall_t*/ )
		{
			return platform.ISteamUtils_GetAPICallFailureReason( hSteamAPICall.Value );
		}
		
		// bool
		public bool GetAPICallResult( SteamAPICall_t hSteamAPICall /*SteamAPICall_t*/, IntPtr pCallback /*void **/, int cubCallback /*int*/, int iCallbackExpected /*int*/, ref bool pbFailed /*bool **/ )
		{
			return platform.ISteamUtils_GetAPICallResult( hSteamAPICall.Value, (IntPtr) pCallback, cubCallback, iCallbackExpected, ref pbFailed );
		}
		
		// uint
		public uint GetAppID()
		{
			return platform.ISteamUtils_GetAppID();
		}
		
		// Universe
		public Universe GetConnectedUniverse()
		{
			return platform.ISteamUtils_GetConnectedUniverse();
		}
		
		// bool
		public bool GetCSERIPPort( out uint unIP /*uint32 **/, out ushort usPort /*uint16 **/ )
		{
			return platform.ISteamUtils_GetCSERIPPort( out unIP, out usPort );
		}
		
		// byte
		public byte GetCurrentBatteryPower()
		{
			return platform.ISteamUtils_GetCurrentBatteryPower();
		}
		
		// bool
		// with: Detect_StringFetch True
		public string GetEnteredGamepadTextInput()
		{
			bool bSuccess = default( bool );
			System.Text.StringBuilder pchText_sb = Helpers.TakeStringBuilder();
			uint cchText = 4096;
			bSuccess = platform.ISteamUtils_GetEnteredGamepadTextInput( pchText_sb, cchText );
			if ( !bSuccess ) return null;
			return pchText_sb.ToString();
		}
		
		// uint
		public uint GetEnteredGamepadTextLength()
		{
			return platform.ISteamUtils_GetEnteredGamepadTextLength();
		}
		
		// bool
		public bool GetImageRGBA( int iImage /*int*/, IntPtr pubDest /*uint8 **/, int nDestBufferSize /*int*/ )
		{
			return platform.ISteamUtils_GetImageRGBA( iImage, (IntPtr) pubDest, nDestBufferSize );
		}
		
		// bool
		public bool GetImageSize( int iImage /*int*/, out uint pnWidth /*uint32 **/, out uint pnHeight /*uint32 **/ )
		{
			return platform.ISteamUtils_GetImageSize( iImage, out pnWidth, out pnHeight );
		}
		
		// uint
		public uint GetIPCCallCount()
		{
			return platform.ISteamUtils_GetIPCCallCount();
		}
		
		// string
		// with: Detect_StringReturn
		public string GetIPCountry()
		{
			IntPtr string_pointer;
			string_pointer = platform.ISteamUtils_GetIPCountry();
			return Marshal.PtrToStringAnsi( string_pointer );
		}
		
		// uint
		public uint GetSecondsSinceAppActive()
		{
			return platform.ISteamUtils_GetSecondsSinceAppActive();
		}
		
		// uint
		public uint GetSecondsSinceComputerActive()
		{
			return platform.ISteamUtils_GetSecondsSinceComputerActive();
		}
		
		// uint
		public uint GetServerRealTime()
		{
			return platform.ISteamUtils_GetServerRealTime();
		}
		
		// string
		// with: Detect_StringReturn
		public string GetSteamUILanguage()
		{
			IntPtr string_pointer;
			string_pointer = platform.ISteamUtils_GetSteamUILanguage();
			return Marshal.PtrToStringAnsi( string_pointer );
		}
		
		// bool
		public bool IsAPICallCompleted( SteamAPICall_t hSteamAPICall /*SteamAPICall_t*/, ref bool pbFailed /*bool **/ )
		{
			return platform.ISteamUtils_IsAPICallCompleted( hSteamAPICall.Value, ref pbFailed );
		}
		
		// bool
		public bool IsOverlayEnabled()
		{
			return platform.ISteamUtils_IsOverlayEnabled();
		}
		
		// bool
		public bool IsSteamInBigPictureMode()
		{
			return platform.ISteamUtils_IsSteamInBigPictureMode();
		}
		
		// bool
		public bool IsSteamRunningInVR()
		{
			return platform.ISteamUtils_IsSteamRunningInVR();
		}
		
		// void
		public void SetOverlayNotificationInset( int nHorizontalInset /*int*/, int nVerticalInset /*int*/ )
		{
			platform.ISteamUtils_SetOverlayNotificationInset( nHorizontalInset, nVerticalInset );
		}
		
		// void
		public void SetOverlayNotificationPosition( NotificationPosition eNotificationPosition /*ENotificationPosition*/ )
		{
			platform.ISteamUtils_SetOverlayNotificationPosition( eNotificationPosition );
		}
		
		// void
		public void SetWarningMessageHook( IntPtr pFunction /*SteamAPIWarningMessageHook_t*/ )
		{
			platform.ISteamUtils_SetWarningMessageHook( (IntPtr) pFunction );
		}
		
		// bool
		public bool ShowGamepadTextInput( GamepadTextInputMode eInputMode /*EGamepadTextInputMode*/, GamepadTextInputLineMode eLineInputMode /*EGamepadTextInputLineMode*/, string pchDescription /*const char **/, uint unCharMax /*uint32*/, string pchExistingText /*const char **/ )
		{
			return platform.ISteamUtils_ShowGamepadTextInput( eInputMode, eLineInputMode, pchDescription, unCharMax, pchExistingText );
		}
		
		// void
		public void StartVRDashboard()
		{
			platform.ISteamUtils_StartVRDashboard();
		}
		
	}
}
