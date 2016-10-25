using System;
using System.Runtime.InteropServices;

namespace SteamNative
{
	public unsafe class SteamController
	{
		internal IntPtr _ptr;
		
		public SteamController( IntPtr pointer )
		{
			_ptr = pointer;
		}
		
		
		// void
		public void ActivateActionSet( ControllerHandle_t controllerHandle /*ControllerHandle_t*/, ControllerActionSetHandle_t actionSetHandle /*ControllerActionSetHandle_t*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) Platform.Win32.ISteamController.ActivateActionSet( _ptr, controllerHandle, actionSetHandle );
			else Platform.Win64.ISteamController.ActivateActionSet( _ptr, controllerHandle, actionSetHandle );
		}
		
		// ControllerActionSetHandle_t
		public ControllerActionSetHandle_t GetActionSetHandle( string pszActionSetName /*const char **/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamController.GetActionSetHandle( _ptr, pszActionSetName );
			else return Platform.Win64.ISteamController.GetActionSetHandle( _ptr, pszActionSetName );
		}
		
		// ControllerAnalogActionData_t
		public ControllerAnalogActionData_t GetAnalogActionData( ControllerHandle_t controllerHandle /*ControllerHandle_t*/, ControllerAnalogActionHandle_t analogActionHandle /*ControllerAnalogActionHandle_t*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamController.GetAnalogActionData( _ptr, controllerHandle, analogActionHandle );
			else return Platform.Win64.ISteamController.GetAnalogActionData( _ptr, controllerHandle, analogActionHandle );
		}
		
		// ControllerAnalogActionHandle_t
		public ControllerAnalogActionHandle_t GetAnalogActionHandle( string pszActionName /*const char **/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamController.GetAnalogActionHandle( _ptr, pszActionName );
			else return Platform.Win64.ISteamController.GetAnalogActionHandle( _ptr, pszActionName );
		}
		
		// int
		public int GetAnalogActionOrigins( ControllerHandle_t controllerHandle /*ControllerHandle_t*/, ControllerActionSetHandle_t actionSetHandle /*ControllerActionSetHandle_t*/, ControllerAnalogActionHandle_t analogActionHandle /*ControllerAnalogActionHandle_t*/, out ControllerActionOrigin originsOut /*EControllerActionOrigin **/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamController.GetAnalogActionOrigins( _ptr, controllerHandle, actionSetHandle, analogActionHandle, out originsOut );
			else return Platform.Win64.ISteamController.GetAnalogActionOrigins( _ptr, controllerHandle, actionSetHandle, analogActionHandle, out originsOut );
		}
		
		// int
		public int GetConnectedControllers( ControllerHandle_t* handlesOut /*ControllerHandle_t **/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamController.GetConnectedControllers( _ptr, (IntPtr) handlesOut );
			else return Platform.Win64.ISteamController.GetConnectedControllers( _ptr, (IntPtr) handlesOut );
		}
		
		// ControllerActionSetHandle_t
		public ControllerActionSetHandle_t GetCurrentActionSet( ControllerHandle_t controllerHandle /*ControllerHandle_t*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamController.GetCurrentActionSet( _ptr, controllerHandle );
			else return Platform.Win64.ISteamController.GetCurrentActionSet( _ptr, controllerHandle );
		}
		
		// ControllerDigitalActionData_t
		public ControllerDigitalActionData_t GetDigitalActionData( ControllerHandle_t controllerHandle /*ControllerHandle_t*/, ControllerDigitalActionHandle_t digitalActionHandle /*ControllerDigitalActionHandle_t*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamController.GetDigitalActionData( _ptr, controllerHandle, digitalActionHandle );
			else return Platform.Win64.ISteamController.GetDigitalActionData( _ptr, controllerHandle, digitalActionHandle );
		}
		
		// ControllerDigitalActionHandle_t
		public ControllerDigitalActionHandle_t GetDigitalActionHandle( string pszActionName /*const char **/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamController.GetDigitalActionHandle( _ptr, pszActionName );
			else return Platform.Win64.ISteamController.GetDigitalActionHandle( _ptr, pszActionName );
		}
		
		// int
		public int GetDigitalActionOrigins( ControllerHandle_t controllerHandle /*ControllerHandle_t*/, ControllerActionSetHandle_t actionSetHandle /*ControllerActionSetHandle_t*/, ControllerDigitalActionHandle_t digitalActionHandle /*ControllerDigitalActionHandle_t*/, out ControllerActionOrigin originsOut /*EControllerActionOrigin **/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamController.GetDigitalActionOrigins( _ptr, controllerHandle, actionSetHandle, digitalActionHandle, out originsOut );
			else return Platform.Win64.ISteamController.GetDigitalActionOrigins( _ptr, controllerHandle, actionSetHandle, digitalActionHandle, out originsOut );
		}
		
		// bool
		public bool Init()
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamController.Init( _ptr );
			else return Platform.Win64.ISteamController.Init( _ptr );
		}
		
		// void
		public void RunFrame()
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) Platform.Win32.ISteamController.RunFrame( _ptr );
			else Platform.Win64.ISteamController.RunFrame( _ptr );
		}
		
		// bool
		public bool ShowBindingPanel( ControllerHandle_t controllerHandle /*ControllerHandle_t*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamController.ShowBindingPanel( _ptr, controllerHandle );
			else return Platform.Win64.ISteamController.ShowBindingPanel( _ptr, controllerHandle );
		}
		
		// bool
		public bool Shutdown()
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamController.Shutdown( _ptr );
			else return Platform.Win64.ISteamController.Shutdown( _ptr );
		}
		
		// void
		public void StopAnalogActionMomentum( ControllerHandle_t controllerHandle /*ControllerHandle_t*/, ControllerAnalogActionHandle_t eAction /*ControllerAnalogActionHandle_t*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) Platform.Win32.ISteamController.StopAnalogActionMomentum( _ptr, controllerHandle, eAction );
			else Platform.Win64.ISteamController.StopAnalogActionMomentum( _ptr, controllerHandle, eAction );
		}
		
		// void
		public void TriggerHapticPulse( ControllerHandle_t controllerHandle /*ControllerHandle_t*/, SteamControllerPad eTargetPad /*ESteamControllerPad*/, ushort usDurationMicroSec /*unsigned short*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) Platform.Win32.ISteamController.TriggerHapticPulse( _ptr, controllerHandle, eTargetPad, usDurationMicroSec );
			else Platform.Win64.ISteamController.TriggerHapticPulse( _ptr, controllerHandle, eTargetPad, usDurationMicroSec );
		}
		
		// void
		public void TriggerRepeatedHapticPulse( ControllerHandle_t controllerHandle /*ControllerHandle_t*/, SteamControllerPad eTargetPad /*ESteamControllerPad*/, ushort usDurationMicroSec /*unsigned short*/, ushort usOffMicroSec /*unsigned short*/, ushort unRepeat /*unsigned short*/, uint nFlags /*unsigned int*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) Platform.Win32.ISteamController.TriggerRepeatedHapticPulse( _ptr, controllerHandle, eTargetPad, usDurationMicroSec, usOffMicroSec, unRepeat, nFlags );
			else Platform.Win64.ISteamController.TriggerRepeatedHapticPulse( _ptr, controllerHandle, eTargetPad, usDurationMicroSec, usOffMicroSec, unRepeat, nFlags );
		}
		
	}
}
