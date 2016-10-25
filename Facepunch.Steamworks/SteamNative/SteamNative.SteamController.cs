using System;
using System.Runtime.InteropServices;

namespace SteamNative
{
	public unsafe class SteamController
	{
		internal Platform.Interface _pi;
		
		public SteamController( IntPtr pointer )
		{
			if ( Platform.IsWindows64 ) _pi = new Platform.Win64( pointer );
			else if ( Platform.IsWindows32 ) _pi = new Platform.Win32( pointer );
			else if ( Platform.IsLinux32 ) _pi = new Platform.Linux32( pointer );
			else if ( Platform.IsLinux64 ) _pi = new Platform.Linux64( pointer );
			else if ( Platform.IsOsx ) _pi = new Platform.Mac( pointer );
		}
		
		public bool IsValid{ get{ return _pi != null && _pi.IsValid; } }
		
		// void
		public void ActivateActionSet( ControllerHandle_t controllerHandle /*ControllerHandle_t*/, ControllerActionSetHandle_t actionSetHandle /*ControllerActionSetHandle_t*/ )
		{
			_pi.ISteamController_ActivateActionSet( controllerHandle, actionSetHandle );
		}
		
		// ControllerActionSetHandle_t
		public ControllerActionSetHandle_t GetActionSetHandle( string pszActionSetName /*const char **/ )
		{
			return _pi.ISteamController_GetActionSetHandle( pszActionSetName );
		}
		
		// ControllerAnalogActionData_t
		public ControllerAnalogActionData_t GetAnalogActionData( ControllerHandle_t controllerHandle /*ControllerHandle_t*/, ControllerAnalogActionHandle_t analogActionHandle /*ControllerAnalogActionHandle_t*/ )
		{
			return _pi.ISteamController_GetAnalogActionData( controllerHandle, analogActionHandle );
		}
		
		// ControllerAnalogActionHandle_t
		public ControllerAnalogActionHandle_t GetAnalogActionHandle( string pszActionName /*const char **/ )
		{
			return _pi.ISteamController_GetAnalogActionHandle( pszActionName );
		}
		
		// int
		public int GetAnalogActionOrigins( ControllerHandle_t controllerHandle /*ControllerHandle_t*/, ControllerActionSetHandle_t actionSetHandle /*ControllerActionSetHandle_t*/, ControllerAnalogActionHandle_t analogActionHandle /*ControllerAnalogActionHandle_t*/, out ControllerActionOrigin originsOut /*EControllerActionOrigin **/ )
		{
			return _pi.ISteamController_GetAnalogActionOrigins( controllerHandle, actionSetHandle, analogActionHandle, out originsOut );
		}
		
		// int
		public int GetConnectedControllers( ControllerHandle_t* handlesOut /*ControllerHandle_t **/ )
		{
			return _pi.ISteamController_GetConnectedControllers( (IntPtr) handlesOut );
		}
		
		// ControllerActionSetHandle_t
		public ControllerActionSetHandle_t GetCurrentActionSet( ControllerHandle_t controllerHandle /*ControllerHandle_t*/ )
		{
			return _pi.ISteamController_GetCurrentActionSet( controllerHandle );
		}
		
		// ControllerDigitalActionData_t
		public ControllerDigitalActionData_t GetDigitalActionData( ControllerHandle_t controllerHandle /*ControllerHandle_t*/, ControllerDigitalActionHandle_t digitalActionHandle /*ControllerDigitalActionHandle_t*/ )
		{
			return _pi.ISteamController_GetDigitalActionData( controllerHandle, digitalActionHandle );
		}
		
		// ControllerDigitalActionHandle_t
		public ControllerDigitalActionHandle_t GetDigitalActionHandle( string pszActionName /*const char **/ )
		{
			return _pi.ISteamController_GetDigitalActionHandle( pszActionName );
		}
		
		// int
		public int GetDigitalActionOrigins( ControllerHandle_t controllerHandle /*ControllerHandle_t*/, ControllerActionSetHandle_t actionSetHandle /*ControllerActionSetHandle_t*/, ControllerDigitalActionHandle_t digitalActionHandle /*ControllerDigitalActionHandle_t*/, out ControllerActionOrigin originsOut /*EControllerActionOrigin **/ )
		{
			return _pi.ISteamController_GetDigitalActionOrigins( controllerHandle, actionSetHandle, digitalActionHandle, out originsOut );
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
		public bool ShowBindingPanel( ControllerHandle_t controllerHandle /*ControllerHandle_t*/ )
		{
			return _pi.ISteamController_ShowBindingPanel( controllerHandle );
		}
		
		// bool
		public bool Shutdown()
		{
			return _pi.ISteamController_Shutdown();
		}
		
		// void
		public void StopAnalogActionMomentum( ControllerHandle_t controllerHandle /*ControllerHandle_t*/, ControllerAnalogActionHandle_t eAction /*ControllerAnalogActionHandle_t*/ )
		{
			_pi.ISteamController_StopAnalogActionMomentum( controllerHandle, eAction );
		}
		
		// void
		public void TriggerHapticPulse( ControllerHandle_t controllerHandle /*ControllerHandle_t*/, SteamControllerPad eTargetPad /*ESteamControllerPad*/, ushort usDurationMicroSec /*unsigned short*/ )
		{
			_pi.ISteamController_TriggerHapticPulse( controllerHandle, eTargetPad, usDurationMicroSec );
		}
		
		// void
		public void TriggerRepeatedHapticPulse( ControllerHandle_t controllerHandle /*ControllerHandle_t*/, SteamControllerPad eTargetPad /*ESteamControllerPad*/, ushort usDurationMicroSec /*unsigned short*/, ushort usOffMicroSec /*unsigned short*/, ushort unRepeat /*unsigned short*/, uint nFlags /*unsigned int*/ )
		{
			_pi.ISteamController_TriggerRepeatedHapticPulse( controllerHandle, eTargetPad, usDurationMicroSec, usOffMicroSec, unRepeat, nFlags );
		}
		
	}
}
