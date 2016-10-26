using System;
using System.Runtime.InteropServices;

namespace SteamNative
{
	public unsafe class SteamController : IDisposable
	{
		//
		// Holds a platform specific implentation
		//
		internal Platform.Interface _pi;
		
		//
		// Constructor decides which implementation to use based on current platform
		//
		public SteamController( IntPtr pointer )
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
		
		// void
		public void ActivateActionSet( ControllerHandle_t controllerHandle /*ControllerHandle_t*/, ControllerActionSetHandle_t actionSetHandle /*ControllerActionSetHandle_t*/ )
		{
			_pi.ISteamController_ActivateActionSet( controllerHandle.Value, actionSetHandle.Value );
		}
		
		// ControllerActionSetHandle_t
		public ControllerActionSetHandle_t GetActionSetHandle( string pszActionSetName /*const char **/ )
		{
			return _pi.ISteamController_GetActionSetHandle( pszActionSetName );
		}
		
		// ControllerAnalogActionData_t
		public ControllerAnalogActionData_t GetAnalogActionData( ControllerHandle_t controllerHandle /*ControllerHandle_t*/, ControllerAnalogActionHandle_t analogActionHandle /*ControllerAnalogActionHandle_t*/ )
		{
			return _pi.ISteamController_GetAnalogActionData( controllerHandle.Value, analogActionHandle.Value );
		}
		
		// ControllerAnalogActionHandle_t
		public ControllerAnalogActionHandle_t GetAnalogActionHandle( string pszActionName /*const char **/ )
		{
			return _pi.ISteamController_GetAnalogActionHandle( pszActionName );
		}
		
		// int
		public int GetAnalogActionOrigins( ControllerHandle_t controllerHandle /*ControllerHandle_t*/, ControllerActionSetHandle_t actionSetHandle /*ControllerActionSetHandle_t*/, ControllerAnalogActionHandle_t analogActionHandle /*ControllerAnalogActionHandle_t*/, out ControllerActionOrigin originsOut /*EControllerActionOrigin **/ )
		{
			return _pi.ISteamController_GetAnalogActionOrigins( controllerHandle.Value, actionSetHandle.Value, analogActionHandle.Value, out originsOut );
		}
		
		// int
		public int GetConnectedControllers( IntPtr handlesOut /*ControllerHandle_t **/ )
		{
			return _pi.ISteamController_GetConnectedControllers( (IntPtr) handlesOut );
		}
		
		// ControllerHandle_t
		public ControllerHandle_t GetControllerForGamepadIndex( int nIndex /*int*/ )
		{
			return _pi.ISteamController_GetControllerForGamepadIndex( nIndex );
		}
		
		// ControllerActionSetHandle_t
		public ControllerActionSetHandle_t GetCurrentActionSet( ControllerHandle_t controllerHandle /*ControllerHandle_t*/ )
		{
			return _pi.ISteamController_GetCurrentActionSet( controllerHandle.Value );
		}
		
		// ControllerDigitalActionData_t
		public ControllerDigitalActionData_t GetDigitalActionData( ControllerHandle_t controllerHandle /*ControllerHandle_t*/, ControllerDigitalActionHandle_t digitalActionHandle /*ControllerDigitalActionHandle_t*/ )
		{
			return _pi.ISteamController_GetDigitalActionData( controllerHandle.Value, digitalActionHandle.Value );
		}
		
		// ControllerDigitalActionHandle_t
		public ControllerDigitalActionHandle_t GetDigitalActionHandle( string pszActionName /*const char **/ )
		{
			return _pi.ISteamController_GetDigitalActionHandle( pszActionName );
		}
		
		// int
		public int GetDigitalActionOrigins( ControllerHandle_t controllerHandle /*ControllerHandle_t*/, ControllerActionSetHandle_t actionSetHandle /*ControllerActionSetHandle_t*/, ControllerDigitalActionHandle_t digitalActionHandle /*ControllerDigitalActionHandle_t*/, out ControllerActionOrigin originsOut /*EControllerActionOrigin **/ )
		{
			return _pi.ISteamController_GetDigitalActionOrigins( controllerHandle.Value, actionSetHandle.Value, digitalActionHandle.Value, out originsOut );
		}
		
		// int
		public int GetGamepadIndexForController( ControllerHandle_t ulControllerHandle /*ControllerHandle_t*/ )
		{
			return _pi.ISteamController_GetGamepadIndexForController( ulControllerHandle.Value );
		}
		
		// ControllerMotionData_t
		public ControllerMotionData_t GetMotionData( ControllerHandle_t controllerHandle /*ControllerHandle_t*/ )
		{
			return _pi.ISteamController_GetMotionData( controllerHandle.Value );
		}
		
		// bool
		public bool Init()
		{
			return _pi.ISteamController_Init();
		}
		
		// void
		public void RunFrame()
		{
			_pi.ISteamController_RunFrame();
		}
		
		// bool
		public bool ShowAnalogActionOrigins( ControllerHandle_t controllerHandle /*ControllerHandle_t*/, ControllerAnalogActionHandle_t analogActionHandle /*ControllerAnalogActionHandle_t*/, float flScale /*float*/, float flXPosition /*float*/, float flYPosition /*float*/ )
		{
			return _pi.ISteamController_ShowAnalogActionOrigins( controllerHandle.Value, analogActionHandle.Value, flScale, flXPosition, flYPosition );
		}
		
		// bool
		public bool ShowBindingPanel( ControllerHandle_t controllerHandle /*ControllerHandle_t*/ )
		{
			return _pi.ISteamController_ShowBindingPanel( controllerHandle.Value );
		}
		
		// bool
		public bool ShowDigitalActionOrigins( ControllerHandle_t controllerHandle /*ControllerHandle_t*/, ControllerDigitalActionHandle_t digitalActionHandle /*ControllerDigitalActionHandle_t*/, float flScale /*float*/, float flXPosition /*float*/, float flYPosition /*float*/ )
		{
			return _pi.ISteamController_ShowDigitalActionOrigins( controllerHandle.Value, digitalActionHandle.Value, flScale, flXPosition, flYPosition );
		}
		
		// bool
		public bool Shutdown()
		{
			return _pi.ISteamController_Shutdown();
		}
		
		// void
		public void StopAnalogActionMomentum( ControllerHandle_t controllerHandle /*ControllerHandle_t*/, ControllerAnalogActionHandle_t eAction /*ControllerAnalogActionHandle_t*/ )
		{
			_pi.ISteamController_StopAnalogActionMomentum( controllerHandle.Value, eAction.Value );
		}
		
		// void
		public void TriggerHapticPulse( ControllerHandle_t controllerHandle /*ControllerHandle_t*/, SteamControllerPad eTargetPad /*ESteamControllerPad*/, ushort usDurationMicroSec /*unsigned short*/ )
		{
			_pi.ISteamController_TriggerHapticPulse( controllerHandle.Value, eTargetPad, usDurationMicroSec );
		}
		
		// void
		public void TriggerRepeatedHapticPulse( ControllerHandle_t controllerHandle /*ControllerHandle_t*/, SteamControllerPad eTargetPad /*ESteamControllerPad*/, ushort usDurationMicroSec /*unsigned short*/, ushort usOffMicroSec /*unsigned short*/, ushort unRepeat /*unsigned short*/, uint nFlags /*unsigned int*/ )
		{
			_pi.ISteamController_TriggerRepeatedHapticPulse( controllerHandle.Value, eTargetPad, usDurationMicroSec, usOffMicroSec, unRepeat, nFlags );
		}
		
	}
}
