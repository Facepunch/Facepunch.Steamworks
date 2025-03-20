using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Steamworks.Data;


namespace Steamworks
{
	internal unsafe partial class ISteamInput : SteamInterface
	{
		public const string Version = "SteamInput006";
		
		internal ISteamInput( bool IsGameServer )
		{
			SetupInterface( IsGameServer );
		}
		
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_SteamInput_v006", CallingConvention = Platform.CC)]
		internal static extern IntPtr SteamAPI_SteamInput_v006();
		public override IntPtr GetUserInterfacePointer() => SteamAPI_SteamInput_v006();
		
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamInput_Init", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _Init( IntPtr self, [MarshalAs( UnmanagedType.U1 )] bool bExplicitlyCallRunFrame );
		
		#endregion
		internal bool Init( [MarshalAs( UnmanagedType.U1 )] bool bExplicitlyCallRunFrame )
		{
			var returnValue = _Init( Self, bExplicitlyCallRunFrame );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamInput_Shutdown", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _Shutdown( IntPtr self );
		
		#endregion
		internal bool Shutdown()
		{
			var returnValue = _Shutdown( Self );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamInput_SetInputActionManifestFilePath", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _SetInputActionManifestFilePath( IntPtr self, IntPtr pchInputActionManifestAbsolutePath );
		
		#endregion
		internal bool SetInputActionManifestFilePath( string pchInputActionManifestAbsolutePath )
		{
			using var str__pchInputActionManifestAbsolutePath = new Utf8StringToNative( pchInputActionManifestAbsolutePath );
			var returnValue = _SetInputActionManifestFilePath( Self, str__pchInputActionManifestAbsolutePath.Pointer );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamInput_RunFrame", CallingConvention = Platform.CC)]
		private static extern void _RunFrame( IntPtr self, [MarshalAs( UnmanagedType.U1 )] bool bReservedValue );
		
		#endregion
		internal void RunFrame( [MarshalAs( UnmanagedType.U1 )] bool bReservedValue )
		{
			_RunFrame( Self, bReservedValue );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamInput_BWaitForData", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _BWaitForData( IntPtr self, [MarshalAs( UnmanagedType.U1 )] bool bWaitForever, uint unTimeout );
		
		#endregion
		internal bool BWaitForData( [MarshalAs( UnmanagedType.U1 )] bool bWaitForever, uint unTimeout )
		{
			var returnValue = _BWaitForData( Self, bWaitForever, unTimeout );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamInput_BNewDataAvailable", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _BNewDataAvailable( IntPtr self );
		
		#endregion
		internal bool BNewDataAvailable()
		{
			var returnValue = _BNewDataAvailable( Self );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamInput_GetConnectedControllers", CallingConvention = Platform.CC)]
		private static extern int _GetConnectedControllers( IntPtr self, [In,Out] InputHandle_t[]  handlesOut );
		
		#endregion
		internal int GetConnectedControllers( [In,Out] InputHandle_t[]  handlesOut )
		{
			var returnValue = _GetConnectedControllers( Self, handlesOut );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamInput_EnableDeviceCallbacks", CallingConvention = Platform.CC)]
		private static extern void _EnableDeviceCallbacks( IntPtr self );
		
		#endregion
		internal void EnableDeviceCallbacks()
		{
			_EnableDeviceCallbacks( Self );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamInput_GetActionSetHandle", CallingConvention = Platform.CC)]
		private static extern InputActionSetHandle_t _GetActionSetHandle( IntPtr self, IntPtr pszActionSetName );
		
		#endregion
		internal InputActionSetHandle_t GetActionSetHandle( string pszActionSetName )
		{
			using var str__pszActionSetName = new Utf8StringToNative( pszActionSetName );
			var returnValue = _GetActionSetHandle( Self, str__pszActionSetName.Pointer );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamInput_ActivateActionSet", CallingConvention = Platform.CC)]
		private static extern void _ActivateActionSet( IntPtr self, InputHandle_t inputHandle, InputActionSetHandle_t actionSetHandle );
		
		#endregion
		internal void ActivateActionSet( InputHandle_t inputHandle, InputActionSetHandle_t actionSetHandle )
		{
			_ActivateActionSet( Self, inputHandle, actionSetHandle );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamInput_GetCurrentActionSet", CallingConvention = Platform.CC)]
		private static extern InputActionSetHandle_t _GetCurrentActionSet( IntPtr self, InputHandle_t inputHandle );
		
		#endregion
		internal InputActionSetHandle_t GetCurrentActionSet( InputHandle_t inputHandle )
		{
			var returnValue = _GetCurrentActionSet( Self, inputHandle );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamInput_ActivateActionSetLayer", CallingConvention = Platform.CC)]
		private static extern void _ActivateActionSetLayer( IntPtr self, InputHandle_t inputHandle, InputActionSetHandle_t actionSetLayerHandle );
		
		#endregion
		internal void ActivateActionSetLayer( InputHandle_t inputHandle, InputActionSetHandle_t actionSetLayerHandle )
		{
			_ActivateActionSetLayer( Self, inputHandle, actionSetLayerHandle );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamInput_DeactivateActionSetLayer", CallingConvention = Platform.CC)]
		private static extern void _DeactivateActionSetLayer( IntPtr self, InputHandle_t inputHandle, InputActionSetHandle_t actionSetLayerHandle );
		
		#endregion
		internal void DeactivateActionSetLayer( InputHandle_t inputHandle, InputActionSetHandle_t actionSetLayerHandle )
		{
			_DeactivateActionSetLayer( Self, inputHandle, actionSetLayerHandle );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamInput_DeactivateAllActionSetLayers", CallingConvention = Platform.CC)]
		private static extern void _DeactivateAllActionSetLayers( IntPtr self, InputHandle_t inputHandle );
		
		#endregion
		internal void DeactivateAllActionSetLayers( InputHandle_t inputHandle )
		{
			_DeactivateAllActionSetLayers( Self, inputHandle );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamInput_GetActiveActionSetLayers", CallingConvention = Platform.CC)]
		private static extern int _GetActiveActionSetLayers( IntPtr self, InputHandle_t inputHandle, [In,Out] InputActionSetHandle_t[]  handlesOut );
		
		#endregion
		internal int GetActiveActionSetLayers( InputHandle_t inputHandle, [In,Out] InputActionSetHandle_t[]  handlesOut )
		{
			var returnValue = _GetActiveActionSetLayers( Self, inputHandle, handlesOut );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamInput_GetDigitalActionHandle", CallingConvention = Platform.CC)]
		private static extern InputDigitalActionHandle_t _GetDigitalActionHandle( IntPtr self, IntPtr pszActionName );
		
		#endregion
		internal InputDigitalActionHandle_t GetDigitalActionHandle( string pszActionName )
		{
			using var str__pszActionName = new Utf8StringToNative( pszActionName );
			var returnValue = _GetDigitalActionHandle( Self, str__pszActionName.Pointer );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamInput_GetDigitalActionData", CallingConvention = Platform.CC)]
		private static extern DigitalState _GetDigitalActionData( IntPtr self, InputHandle_t inputHandle, InputDigitalActionHandle_t digitalActionHandle );
		
		#endregion
		internal DigitalState GetDigitalActionData( InputHandle_t inputHandle, InputDigitalActionHandle_t digitalActionHandle )
		{
			var returnValue = _GetDigitalActionData( Self, inputHandle, digitalActionHandle );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamInput_GetDigitalActionOrigins", CallingConvention = Platform.CC)]
		private static extern int _GetDigitalActionOrigins( IntPtr self, InputHandle_t inputHandle, InputActionSetHandle_t actionSetHandle, InputDigitalActionHandle_t digitalActionHandle, ref InputActionOrigin originsOut );
		
		#endregion
		internal int GetDigitalActionOrigins( InputHandle_t inputHandle, InputActionSetHandle_t actionSetHandle, InputDigitalActionHandle_t digitalActionHandle, ref InputActionOrigin originsOut )
		{
			var returnValue = _GetDigitalActionOrigins( Self, inputHandle, actionSetHandle, digitalActionHandle, ref originsOut );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamInput_GetStringForDigitalActionName", CallingConvention = Platform.CC)]
		private static extern Utf8StringPointer _GetStringForDigitalActionName( IntPtr self, InputDigitalActionHandle_t eActionHandle );
		
		#endregion
		internal string GetStringForDigitalActionName( InputDigitalActionHandle_t eActionHandle )
		{
			var returnValue = _GetStringForDigitalActionName( Self, eActionHandle );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamInput_GetAnalogActionHandle", CallingConvention = Platform.CC)]
		private static extern InputAnalogActionHandle_t _GetAnalogActionHandle( IntPtr self, IntPtr pszActionName );
		
		#endregion
		internal InputAnalogActionHandle_t GetAnalogActionHandle( string pszActionName )
		{
			using var str__pszActionName = new Utf8StringToNative( pszActionName );
			var returnValue = _GetAnalogActionHandle( Self, str__pszActionName.Pointer );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamInput_GetAnalogActionData", CallingConvention = Platform.CC)]
		private static extern AnalogState _GetAnalogActionData( IntPtr self, InputHandle_t inputHandle, InputAnalogActionHandle_t analogActionHandle );
		
		#endregion
		internal AnalogState GetAnalogActionData( InputHandle_t inputHandle, InputAnalogActionHandle_t analogActionHandle )
		{
			var returnValue = _GetAnalogActionData( Self, inputHandle, analogActionHandle );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamInput_GetAnalogActionOrigins", CallingConvention = Platform.CC)]
		private static extern int _GetAnalogActionOrigins( IntPtr self, InputHandle_t inputHandle, InputActionSetHandle_t actionSetHandle, InputAnalogActionHandle_t analogActionHandle, ref InputActionOrigin originsOut );
		
		#endregion
		internal int GetAnalogActionOrigins( InputHandle_t inputHandle, InputActionSetHandle_t actionSetHandle, InputAnalogActionHandle_t analogActionHandle, ref InputActionOrigin originsOut )
		{
			var returnValue = _GetAnalogActionOrigins( Self, inputHandle, actionSetHandle, analogActionHandle, ref originsOut );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamInput_GetGlyphPNGForActionOrigin", CallingConvention = Platform.CC)]
		private static extern Utf8StringPointer _GetGlyphPNGForActionOrigin( IntPtr self, InputActionOrigin eOrigin, GlyphSize eSize, uint unFlags );
		
		#endregion
		internal string GetGlyphPNGForActionOrigin( InputActionOrigin eOrigin, GlyphSize eSize, uint unFlags )
		{
			var returnValue = _GetGlyphPNGForActionOrigin( Self, eOrigin, eSize, unFlags );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamInput_GetGlyphSVGForActionOrigin", CallingConvention = Platform.CC)]
		private static extern Utf8StringPointer _GetGlyphSVGForActionOrigin( IntPtr self, InputActionOrigin eOrigin, uint unFlags );
		
		#endregion
		internal string GetGlyphSVGForActionOrigin( InputActionOrigin eOrigin, uint unFlags )
		{
			var returnValue = _GetGlyphSVGForActionOrigin( Self, eOrigin, unFlags );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamInput_GetGlyphForActionOrigin_Legacy", CallingConvention = Platform.CC)]
		private static extern Utf8StringPointer _GetGlyphForActionOrigin_Legacy( IntPtr self, InputActionOrigin eOrigin );
		
		#endregion
		internal string GetGlyphForActionOrigin_Legacy( InputActionOrigin eOrigin )
		{
			var returnValue = _GetGlyphForActionOrigin_Legacy( Self, eOrigin );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamInput_GetStringForActionOrigin", CallingConvention = Platform.CC)]
		private static extern Utf8StringPointer _GetStringForActionOrigin( IntPtr self, InputActionOrigin eOrigin );
		
		#endregion
		internal string GetStringForActionOrigin( InputActionOrigin eOrigin )
		{
			var returnValue = _GetStringForActionOrigin( Self, eOrigin );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamInput_GetStringForAnalogActionName", CallingConvention = Platform.CC)]
		private static extern Utf8StringPointer _GetStringForAnalogActionName( IntPtr self, InputAnalogActionHandle_t eActionHandle );
		
		#endregion
		internal string GetStringForAnalogActionName( InputAnalogActionHandle_t eActionHandle )
		{
			var returnValue = _GetStringForAnalogActionName( Self, eActionHandle );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamInput_StopAnalogActionMomentum", CallingConvention = Platform.CC)]
		private static extern void _StopAnalogActionMomentum( IntPtr self, InputHandle_t inputHandle, InputAnalogActionHandle_t eAction );
		
		#endregion
		internal void StopAnalogActionMomentum( InputHandle_t inputHandle, InputAnalogActionHandle_t eAction )
		{
			_StopAnalogActionMomentum( Self, inputHandle, eAction );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamInput_GetMotionData", CallingConvention = Platform.CC)]
		private static extern MotionState _GetMotionData( IntPtr self, InputHandle_t inputHandle );
		
		#endregion
		internal MotionState GetMotionData( InputHandle_t inputHandle )
		{
			var returnValue = _GetMotionData( Self, inputHandle );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamInput_TriggerVibration", CallingConvention = Platform.CC)]
		private static extern void _TriggerVibration( IntPtr self, InputHandle_t inputHandle, ushort usLeftSpeed, ushort usRightSpeed );
		
		#endregion
		internal void TriggerVibration( InputHandle_t inputHandle, ushort usLeftSpeed, ushort usRightSpeed )
		{
			_TriggerVibration( Self, inputHandle, usLeftSpeed, usRightSpeed );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamInput_TriggerVibrationExtended", CallingConvention = Platform.CC)]
		private static extern void _TriggerVibrationExtended( IntPtr self, InputHandle_t inputHandle, ushort usLeftSpeed, ushort usRightSpeed, ushort usLeftTriggerSpeed, ushort usRightTriggerSpeed );
		
		#endregion
		internal void TriggerVibrationExtended( InputHandle_t inputHandle, ushort usLeftSpeed, ushort usRightSpeed, ushort usLeftTriggerSpeed, ushort usRightTriggerSpeed )
		{
			_TriggerVibrationExtended( Self, inputHandle, usLeftSpeed, usRightSpeed, usLeftTriggerSpeed, usRightTriggerSpeed );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamInput_TriggerSimpleHapticEvent", CallingConvention = Platform.CC)]
		private static extern void _TriggerSimpleHapticEvent( IntPtr self, InputHandle_t inputHandle, ControllerHapticLocation eHapticLocation, byte nIntensity, char nGainDB, byte nOtherIntensity, char nOtherGainDB );
		
		#endregion
		internal void TriggerSimpleHapticEvent( InputHandle_t inputHandle, ControllerHapticLocation eHapticLocation, byte nIntensity, char nGainDB, byte nOtherIntensity, char nOtherGainDB )
		{
			_TriggerSimpleHapticEvent( Self, inputHandle, eHapticLocation, nIntensity, nGainDB, nOtherIntensity, nOtherGainDB );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamInput_SetLEDColor", CallingConvention = Platform.CC)]
		private static extern void _SetLEDColor( IntPtr self, InputHandle_t inputHandle, byte nColorR, byte nColorG, byte nColorB, uint nFlags );
		
		#endregion
		internal void SetLEDColor( InputHandle_t inputHandle, byte nColorR, byte nColorG, byte nColorB, uint nFlags )
		{
			_SetLEDColor( Self, inputHandle, nColorR, nColorG, nColorB, nFlags );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamInput_Legacy_TriggerHapticPulse", CallingConvention = Platform.CC)]
		private static extern void _Legacy_TriggerHapticPulse( IntPtr self, InputHandle_t inputHandle, SteamControllerPad eTargetPad, ushort usDurationMicroSec );
		
		#endregion
		internal void Legacy_TriggerHapticPulse( InputHandle_t inputHandle, SteamControllerPad eTargetPad, ushort usDurationMicroSec )
		{
			_Legacy_TriggerHapticPulse( Self, inputHandle, eTargetPad, usDurationMicroSec );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamInput_Legacy_TriggerRepeatedHapticPulse", CallingConvention = Platform.CC)]
		private static extern void _Legacy_TriggerRepeatedHapticPulse( IntPtr self, InputHandle_t inputHandle, SteamControllerPad eTargetPad, ushort usDurationMicroSec, ushort usOffMicroSec, ushort unRepeat, uint nFlags );
		
		#endregion
		internal void Legacy_TriggerRepeatedHapticPulse( InputHandle_t inputHandle, SteamControllerPad eTargetPad, ushort usDurationMicroSec, ushort usOffMicroSec, ushort unRepeat, uint nFlags )
		{
			_Legacy_TriggerRepeatedHapticPulse( Self, inputHandle, eTargetPad, usDurationMicroSec, usOffMicroSec, unRepeat, nFlags );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamInput_ShowBindingPanel", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _ShowBindingPanel( IntPtr self, InputHandle_t inputHandle );
		
		#endregion
		internal bool ShowBindingPanel( InputHandle_t inputHandle )
		{
			var returnValue = _ShowBindingPanel( Self, inputHandle );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamInput_GetInputTypeForHandle", CallingConvention = Platform.CC)]
		private static extern InputType _GetInputTypeForHandle( IntPtr self, InputHandle_t inputHandle );
		
		#endregion
		internal InputType GetInputTypeForHandle( InputHandle_t inputHandle )
		{
			var returnValue = _GetInputTypeForHandle( Self, inputHandle );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamInput_GetControllerForGamepadIndex", CallingConvention = Platform.CC)]
		private static extern InputHandle_t _GetControllerForGamepadIndex( IntPtr self, int nIndex );
		
		#endregion
		internal InputHandle_t GetControllerForGamepadIndex( int nIndex )
		{
			var returnValue = _GetControllerForGamepadIndex( Self, nIndex );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamInput_GetGamepadIndexForController", CallingConvention = Platform.CC)]
		private static extern int _GetGamepadIndexForController( IntPtr self, InputHandle_t ulinputHandle );
		
		#endregion
		internal int GetGamepadIndexForController( InputHandle_t ulinputHandle )
		{
			var returnValue = _GetGamepadIndexForController( Self, ulinputHandle );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamInput_GetStringForXboxOrigin", CallingConvention = Platform.CC)]
		private static extern Utf8StringPointer _GetStringForXboxOrigin( IntPtr self, XboxOrigin eOrigin );
		
		#endregion
		internal string GetStringForXboxOrigin( XboxOrigin eOrigin )
		{
			var returnValue = _GetStringForXboxOrigin( Self, eOrigin );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamInput_GetGlyphForXboxOrigin", CallingConvention = Platform.CC)]
		private static extern Utf8StringPointer _GetGlyphForXboxOrigin( IntPtr self, XboxOrigin eOrigin );
		
		#endregion
		internal string GetGlyphForXboxOrigin( XboxOrigin eOrigin )
		{
			var returnValue = _GetGlyphForXboxOrigin( Self, eOrigin );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamInput_GetActionOriginFromXboxOrigin", CallingConvention = Platform.CC)]
		private static extern InputActionOrigin _GetActionOriginFromXboxOrigin( IntPtr self, InputHandle_t inputHandle, XboxOrigin eOrigin );
		
		#endregion
		internal InputActionOrigin GetActionOriginFromXboxOrigin( InputHandle_t inputHandle, XboxOrigin eOrigin )
		{
			var returnValue = _GetActionOriginFromXboxOrigin( Self, inputHandle, eOrigin );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamInput_TranslateActionOrigin", CallingConvention = Platform.CC)]
		private static extern InputActionOrigin _TranslateActionOrigin( IntPtr self, InputType eDestinationInputType, InputActionOrigin eSourceOrigin );
		
		#endregion
		internal InputActionOrigin TranslateActionOrigin( InputType eDestinationInputType, InputActionOrigin eSourceOrigin )
		{
			var returnValue = _TranslateActionOrigin( Self, eDestinationInputType, eSourceOrigin );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamInput_GetDeviceBindingRevision", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _GetDeviceBindingRevision( IntPtr self, InputHandle_t inputHandle, ref int pMajor, ref int pMinor );
		
		#endregion
		internal bool GetDeviceBindingRevision( InputHandle_t inputHandle, ref int pMajor, ref int pMinor )
		{
			var returnValue = _GetDeviceBindingRevision( Self, inputHandle, ref pMajor, ref pMinor );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamInput_GetRemotePlaySessionID", CallingConvention = Platform.CC)]
		private static extern uint _GetRemotePlaySessionID( IntPtr self, InputHandle_t inputHandle );
		
		#endregion
		internal uint GetRemotePlaySessionID( InputHandle_t inputHandle )
		{
			var returnValue = _GetRemotePlaySessionID( Self, inputHandle );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamInput_GetSessionInputConfigurationSettings", CallingConvention = Platform.CC)]
		private static extern ushort _GetSessionInputConfigurationSettings( IntPtr self );
		
		#endregion
		internal ushort GetSessionInputConfigurationSettings()
		{
			var returnValue = _GetSessionInputConfigurationSettings( Self );
			return returnValue;
		}
		
	}
}
