using System;
using System.Runtime.InteropServices;
using System.Linq;

namespace SteamNative
{
	internal unsafe class SteamInput : IDisposable
	{
		//
		// Holds a platform specific implentation
		//
		internal Platform.Interface platform;
		internal Facepunch.Steamworks.BaseSteamworks steamworks;
		
		//
		// Constructor decides which implementation to use based on current platform
		//
		internal SteamInput( Facepunch.Steamworks.BaseSteamworks steamworks, IntPtr pointer )
		{
			this.steamworks = steamworks;
			
			if ( Platform.IsWindows ) platform = new Platform.Windows( pointer );
			else if ( Platform.IsLinux ) platform = new Platform.Linux( pointer );
			else if ( Platform.IsOsx ) platform = new Platform.Mac( pointer );
		}
		
		//
		// Class is invalid if we don't have a valid implementation
		//
		public bool IsValid => platform != null && platform.IsValid;
		
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
		
		// void
		public void ActivateActionSet( InputHandle_t inputHandle /*InputHandle_t*/, InputActionSetHandle_t actionSetHandle /*InputActionSetHandle_t*/ )
		{
			platform.ISteamInput_ActivateActionSet( inputHandle.Value, actionSetHandle.Value );
		}
		
		// void
		public void ActivateActionSetLayer( InputHandle_t inputHandle /*InputHandle_t*/, InputActionSetHandle_t actionSetLayerHandle /*InputActionSetHandle_t*/ )
		{
			platform.ISteamInput_ActivateActionSetLayer( inputHandle.Value, actionSetLayerHandle.Value );
		}
		
		// void
		public void DeactivateActionSetLayer( InputHandle_t inputHandle /*InputHandle_t*/, InputActionSetHandle_t actionSetLayerHandle /*InputActionSetHandle_t*/ )
		{
			platform.ISteamInput_DeactivateActionSetLayer( inputHandle.Value, actionSetLayerHandle.Value );
		}
		
		// void
		public void DeactivateAllActionSetLayers( InputHandle_t inputHandle /*InputHandle_t*/ )
		{
			platform.ISteamInput_DeactivateAllActionSetLayers( inputHandle.Value );
		}
		
		// InputActionOrigin
		public InputActionOrigin GetActionOriginFromXboxOrigin( InputHandle_t inputHandle /*InputHandle_t*/, XboxOrigin eOrigin /*EXboxOrigin*/ )
		{
			return platform.ISteamInput_GetActionOriginFromXboxOrigin( inputHandle.Value, eOrigin );
		}
		
		// InputActionSetHandle_t
		public InputActionSetHandle_t GetActionSetHandle( string pszActionSetName /*const char **/ )
		{
			return platform.ISteamInput_GetActionSetHandle( pszActionSetName );
		}
		
		// int
		public int GetActiveActionSetLayers( InputHandle_t inputHandle /*InputHandle_t*/, IntPtr handlesOut /*InputActionSetHandle_t **/ )
		{
			return platform.ISteamInput_GetActiveActionSetLayers( inputHandle.Value, (IntPtr) handlesOut );
		}
		
		// InputAnalogActionData_t
		public InputAnalogActionData_t GetAnalogActionData( InputHandle_t inputHandle /*InputHandle_t*/, InputAnalogActionHandle_t analogActionHandle /*InputAnalogActionHandle_t*/ )
		{
			return platform.ISteamInput_GetAnalogActionData( inputHandle.Value, analogActionHandle.Value );
		}
		
		// InputAnalogActionHandle_t
		public InputAnalogActionHandle_t GetAnalogActionHandle( string pszActionName /*const char **/ )
		{
			return platform.ISteamInput_GetAnalogActionHandle( pszActionName );
		}
		
		// int
		public int GetAnalogActionOrigins( InputHandle_t inputHandle /*InputHandle_t*/, InputActionSetHandle_t actionSetHandle /*InputActionSetHandle_t*/, InputAnalogActionHandle_t analogActionHandle /*InputAnalogActionHandle_t*/, out InputActionOrigin originsOut /*EInputActionOrigin **/ )
		{
			return platform.ISteamInput_GetAnalogActionOrigins( inputHandle.Value, actionSetHandle.Value, analogActionHandle.Value, out originsOut );
		}
		
		// int
		public int GetConnectedControllers( IntPtr handlesOut /*InputHandle_t **/ )
		{
			return platform.ISteamInput_GetConnectedControllers( (IntPtr) handlesOut );
		}
		
		// InputHandle_t
		public InputHandle_t GetControllerForGamepadIndex( int nIndex /*int*/ )
		{
			return platform.ISteamInput_GetControllerForGamepadIndex( nIndex );
		}
		
		// InputActionSetHandle_t
		public InputActionSetHandle_t GetCurrentActionSet( InputHandle_t inputHandle /*InputHandle_t*/ )
		{
			return platform.ISteamInput_GetCurrentActionSet( inputHandle.Value );
		}
		
		// InputDigitalActionData_t
		public InputDigitalActionData_t GetDigitalActionData( InputHandle_t inputHandle /*InputHandle_t*/, InputDigitalActionHandle_t digitalActionHandle /*InputDigitalActionHandle_t*/ )
		{
			return platform.ISteamInput_GetDigitalActionData( inputHandle.Value, digitalActionHandle.Value );
		}
		
		// InputDigitalActionHandle_t
		public InputDigitalActionHandle_t GetDigitalActionHandle( string pszActionName /*const char **/ )
		{
			return platform.ISteamInput_GetDigitalActionHandle( pszActionName );
		}
		
		// int
		public int GetDigitalActionOrigins( InputHandle_t inputHandle /*InputHandle_t*/, InputActionSetHandle_t actionSetHandle /*InputActionSetHandle_t*/, InputDigitalActionHandle_t digitalActionHandle /*InputDigitalActionHandle_t*/, out InputActionOrigin originsOut /*EInputActionOrigin **/ )
		{
			return platform.ISteamInput_GetDigitalActionOrigins( inputHandle.Value, actionSetHandle.Value, digitalActionHandle.Value, out originsOut );
		}
		
		// int
		public int GetGamepadIndexForController( InputHandle_t ulinputHandle /*InputHandle_t*/ )
		{
			return platform.ISteamInput_GetGamepadIndexForController( ulinputHandle.Value );
		}
		
		// string
		// with: Detect_StringReturn
		public string GetGlyphForActionOrigin( InputActionOrigin eOrigin /*EInputActionOrigin*/ )
		{
			IntPtr string_pointer;
			string_pointer = platform.ISteamInput_GetGlyphForActionOrigin( eOrigin );
			return Marshal.PtrToStringAnsi( string_pointer );
		}
		
		// string
		// with: Detect_StringReturn
		public string GetGlyphForXboxOrigin( XboxOrigin eOrigin /*EXboxOrigin*/ )
		{
			IntPtr string_pointer;
			string_pointer = platform.ISteamInput_GetGlyphForXboxOrigin( eOrigin );
			return Marshal.PtrToStringAnsi( string_pointer );
		}
		
		// SteamInputType
		public SteamInputType GetInputTypeForHandle( InputHandle_t inputHandle /*InputHandle_t*/ )
		{
			return platform.ISteamInput_GetInputTypeForHandle( inputHandle.Value );
		}
		
		// InputMotionData_t
		public InputMotionData_t GetMotionData( InputHandle_t inputHandle /*InputHandle_t*/ )
		{
			return platform.ISteamInput_GetMotionData( inputHandle.Value );
		}
		
		// string
		// with: Detect_StringReturn
		public string GetStringForActionOrigin( InputActionOrigin eOrigin /*EInputActionOrigin*/ )
		{
			IntPtr string_pointer;
			string_pointer = platform.ISteamInput_GetStringForActionOrigin( eOrigin );
			return Marshal.PtrToStringAnsi( string_pointer );
		}
		
		// string
		// with: Detect_StringReturn
		public string GetStringForXboxOrigin( XboxOrigin eOrigin /*EXboxOrigin*/ )
		{
			IntPtr string_pointer;
			string_pointer = platform.ISteamInput_GetStringForXboxOrigin( eOrigin );
			return Marshal.PtrToStringAnsi( string_pointer );
		}
		
		// bool
		public bool Init()
		{
			return platform.ISteamInput_Init();
		}
		
		// void
		public void RunFrame()
		{
			platform.ISteamInput_RunFrame();
		}
		
		// void
		public void SetLEDColor( InputHandle_t inputHandle /*InputHandle_t*/, byte nColorR /*uint8*/, byte nColorG /*uint8*/, byte nColorB /*uint8*/, uint nFlags /*unsigned int*/ )
		{
			platform.ISteamInput_SetLEDColor( inputHandle.Value, nColorR, nColorG, nColorB, nFlags );
		}
		
		// bool
		public bool ShowBindingPanel( InputHandle_t inputHandle /*InputHandle_t*/ )
		{
			return platform.ISteamInput_ShowBindingPanel( inputHandle.Value );
		}
		
		// bool
		public bool Shutdown()
		{
			return platform.ISteamInput_Shutdown();
		}
		
		// void
		public void StopAnalogActionMomentum( InputHandle_t inputHandle /*InputHandle_t*/, InputAnalogActionHandle_t eAction /*InputAnalogActionHandle_t*/ )
		{
			platform.ISteamInput_StopAnalogActionMomentum( inputHandle.Value, eAction.Value );
		}
		
		// InputActionOrigin
		public InputActionOrigin TranslateActionOrigin( SteamInputType eDestinationInputType /*ESteamInputType*/, InputActionOrigin eSourceOrigin /*EInputActionOrigin*/ )
		{
			return platform.ISteamInput_TranslateActionOrigin( eDestinationInputType, eSourceOrigin );
		}
		
		// void
		public void TriggerHapticPulse( InputHandle_t inputHandle /*InputHandle_t*/, SteamControllerPad eTargetPad /*ESteamControllerPad*/, ushort usDurationMicroSec /*unsigned short*/ )
		{
			platform.ISteamInput_TriggerHapticPulse( inputHandle.Value, eTargetPad, usDurationMicroSec );
		}
		
		// void
		public void TriggerRepeatedHapticPulse( InputHandle_t inputHandle /*InputHandle_t*/, SteamControllerPad eTargetPad /*ESteamControllerPad*/, ushort usDurationMicroSec /*unsigned short*/, ushort usOffMicroSec /*unsigned short*/, ushort unRepeat /*unsigned short*/, uint nFlags /*unsigned int*/ )
		{
			platform.ISteamInput_TriggerRepeatedHapticPulse( inputHandle.Value, eTargetPad, usDurationMicroSec, usOffMicroSec, unRepeat, nFlags );
		}
		
		// void
		public void TriggerVibration( InputHandle_t inputHandle /*InputHandle_t*/, ushort usLeftSpeed /*unsigned short*/, ushort usRightSpeed /*unsigned short*/ )
		{
			platform.ISteamInput_TriggerVibration( inputHandle.Value, usLeftSpeed, usRightSpeed );
		}
		
	}
}
