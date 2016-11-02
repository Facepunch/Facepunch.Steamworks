using System;
using System.Runtime.InteropServices;

namespace SteamNative
{
	internal unsafe class SteamController : IDisposable
	{
		//
		// Holds a platform specific implentation
		//
		internal Platform.Interface platform;
		internal Facepunch.Steamworks.BaseSteamworks steamworks;
		
		//
		// Constructor decides which implementation to use based on current platform
		//
		internal SteamController( Facepunch.Steamworks.BaseSteamworks steamworks, IntPtr pointer )
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
		
		// void
		public void ActivateActionSet( ControllerHandle_t controllerHandle /*ControllerHandle_t*/, ControllerActionSetHandle_t actionSetHandle /*ControllerActionSetHandle_t*/ )
		{
			platform.ISteamController_ActivateActionSet( controllerHandle.Value, actionSetHandle.Value );
		}
		
		// ControllerActionSetHandle_t
		public ControllerActionSetHandle_t GetActionSetHandle( string pszActionSetName /*const char **/ )
		{
			return platform.ISteamController_GetActionSetHandle( pszActionSetName );
		}
		
		// ControllerAnalogActionData_t
		public ControllerAnalogActionData_t GetAnalogActionData( ControllerHandle_t controllerHandle /*ControllerHandle_t*/, ControllerAnalogActionHandle_t analogActionHandle /*ControllerAnalogActionHandle_t*/ )
		{
			return platform.ISteamController_GetAnalogActionData( controllerHandle.Value, analogActionHandle.Value );
		}
		
		// ControllerAnalogActionHandle_t
		public ControllerAnalogActionHandle_t GetAnalogActionHandle( string pszActionName /*const char **/ )
		{
			return platform.ISteamController_GetAnalogActionHandle( pszActionName );
		}
		
		// int
		public int GetAnalogActionOrigins( ControllerHandle_t controllerHandle /*ControllerHandle_t*/, ControllerActionSetHandle_t actionSetHandle /*ControllerActionSetHandle_t*/, ControllerAnalogActionHandle_t analogActionHandle /*ControllerAnalogActionHandle_t*/, out ControllerActionOrigin originsOut /*EControllerActionOrigin **/ )
		{
			return platform.ISteamController_GetAnalogActionOrigins( controllerHandle.Value, actionSetHandle.Value, analogActionHandle.Value, out originsOut );
		}
		
		// int
		public int GetConnectedControllers( IntPtr handlesOut /*ControllerHandle_t **/ )
		{
			return platform.ISteamController_GetConnectedControllers( (IntPtr) handlesOut );
		}
		
		// ControllerHandle_t
		public ControllerHandle_t GetControllerForGamepadIndex( int nIndex /*int*/ )
		{
			return platform.ISteamController_GetControllerForGamepadIndex( nIndex );
		}
		
		// ControllerActionSetHandle_t
		public ControllerActionSetHandle_t GetCurrentActionSet( ControllerHandle_t controllerHandle /*ControllerHandle_t*/ )
		{
			return platform.ISteamController_GetCurrentActionSet( controllerHandle.Value );
		}
		
		// ControllerDigitalActionData_t
		public ControllerDigitalActionData_t GetDigitalActionData( ControllerHandle_t controllerHandle /*ControllerHandle_t*/, ControllerDigitalActionHandle_t digitalActionHandle /*ControllerDigitalActionHandle_t*/ )
		{
			return platform.ISteamController_GetDigitalActionData( controllerHandle.Value, digitalActionHandle.Value );
		}
		
		// ControllerDigitalActionHandle_t
		public ControllerDigitalActionHandle_t GetDigitalActionHandle( string pszActionName /*const char **/ )
		{
			return platform.ISteamController_GetDigitalActionHandle( pszActionName );
		}
		
		// int
		public int GetDigitalActionOrigins( ControllerHandle_t controllerHandle /*ControllerHandle_t*/, ControllerActionSetHandle_t actionSetHandle /*ControllerActionSetHandle_t*/, ControllerDigitalActionHandle_t digitalActionHandle /*ControllerDigitalActionHandle_t*/, out ControllerActionOrigin originsOut /*EControllerActionOrigin **/ )
		{
			return platform.ISteamController_GetDigitalActionOrigins( controllerHandle.Value, actionSetHandle.Value, digitalActionHandle.Value, out originsOut );
		}
		
		// int
		public int GetGamepadIndexForController( ControllerHandle_t ulControllerHandle /*ControllerHandle_t*/ )
		{
			return platform.ISteamController_GetGamepadIndexForController( ulControllerHandle.Value );
		}
		
		// ControllerMotionData_t
		public ControllerMotionData_t GetMotionData( ControllerHandle_t controllerHandle /*ControllerHandle_t*/ )
		{
			return platform.ISteamController_GetMotionData( controllerHandle.Value );
		}
		
		// bool
		public bool Init()
		{
			return platform.ISteamController_Init();
		}
		
		// void
		public void RunFrame()
		{
			platform.ISteamController_RunFrame();
		}
		
		// bool
		public bool ShowAnalogActionOrigins( ControllerHandle_t controllerHandle /*ControllerHandle_t*/, ControllerAnalogActionHandle_t analogActionHandle /*ControllerAnalogActionHandle_t*/, float flScale /*float*/, float flXPosition /*float*/, float flYPosition /*float*/ )
		{
			return platform.ISteamController_ShowAnalogActionOrigins( controllerHandle.Value, analogActionHandle.Value, flScale, flXPosition, flYPosition );
		}
		
		// bool
		public bool ShowBindingPanel( ControllerHandle_t controllerHandle /*ControllerHandle_t*/ )
		{
			return platform.ISteamController_ShowBindingPanel( controllerHandle.Value );
		}
		
		// bool
		public bool ShowDigitalActionOrigins( ControllerHandle_t controllerHandle /*ControllerHandle_t*/, ControllerDigitalActionHandle_t digitalActionHandle /*ControllerDigitalActionHandle_t*/, float flScale /*float*/, float flXPosition /*float*/, float flYPosition /*float*/ )
		{
			return platform.ISteamController_ShowDigitalActionOrigins( controllerHandle.Value, digitalActionHandle.Value, flScale, flXPosition, flYPosition );
		}
		
		// bool
		public bool Shutdown()
		{
			return platform.ISteamController_Shutdown();
		}
		
		// void
		public void StopAnalogActionMomentum( ControllerHandle_t controllerHandle /*ControllerHandle_t*/, ControllerAnalogActionHandle_t eAction /*ControllerAnalogActionHandle_t*/ )
		{
			platform.ISteamController_StopAnalogActionMomentum( controllerHandle.Value, eAction.Value );
		}
		
		// void
		public void TriggerHapticPulse( ControllerHandle_t controllerHandle /*ControllerHandle_t*/, SteamControllerPad eTargetPad /*ESteamControllerPad*/, ushort usDurationMicroSec /*unsigned short*/ )
		{
			platform.ISteamController_TriggerHapticPulse( controllerHandle.Value, eTargetPad, usDurationMicroSec );
		}
		
		// void
		public void TriggerRepeatedHapticPulse( ControllerHandle_t controllerHandle /*ControllerHandle_t*/, SteamControllerPad eTargetPad /*ESteamControllerPad*/, ushort usDurationMicroSec /*unsigned short*/, ushort usOffMicroSec /*unsigned short*/, ushort unRepeat /*unsigned short*/, uint nFlags /*unsigned int*/ )
		{
			platform.ISteamController_TriggerRepeatedHapticPulse( controllerHandle.Value, eTargetPad, usDurationMicroSec, usOffMicroSec, unRepeat, nFlags );
		}
		
	}
}
