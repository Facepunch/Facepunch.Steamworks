using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Steamworks.Data;


namespace Steamworks
{
	internal class ISteamController : SteamInterface
	{
		
		internal ISteamController( bool IsGameServer )
		{
			SetupInterface( IsGameServer );
		}
		
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_SteamController_v007", CallingConvention = Platform.CC)]
		internal static extern IntPtr SteamAPI_SteamController_v007();
		public override IntPtr GetUserInterfacePointer() => SteamAPI_SteamController_v007();
		
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamController_Init", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _Init( IntPtr self );
		
		#endregion
		internal bool Init()
		{
			var returnValue = _Init( Self );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamController_Shutdown", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _Shutdown( IntPtr self );
		
		#endregion
		internal bool Shutdown()
		{
			var returnValue = _Shutdown( Self );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamController_RunFrame", CallingConvention = Platform.CC)]
		private static extern void _RunFrame( IntPtr self );
		
		#endregion
		internal void RunFrame()
		{
			_RunFrame( Self );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamController_GetConnectedControllers", CallingConvention = Platform.CC)]
		private static extern int _GetConnectedControllers( IntPtr self, [In,Out] ControllerHandle_t[]  handlesOut );
		
		#endregion
		internal int GetConnectedControllers( [In,Out] ControllerHandle_t[]  handlesOut )
		{
			var returnValue = _GetConnectedControllers( Self, handlesOut );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamController_GetActionSetHandle", CallingConvention = Platform.CC)]
		private static extern ControllerActionSetHandle_t _GetActionSetHandle( IntPtr self, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pszActionSetName );
		
		#endregion
		internal ControllerActionSetHandle_t GetActionSetHandle( [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pszActionSetName )
		{
			var returnValue = _GetActionSetHandle( Self, pszActionSetName );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamController_ActivateActionSet", CallingConvention = Platform.CC)]
		private static extern void _ActivateActionSet( IntPtr self, ControllerHandle_t controllerHandle, ControllerActionSetHandle_t actionSetHandle );
		
		#endregion
		internal void ActivateActionSet( ControllerHandle_t controllerHandle, ControllerActionSetHandle_t actionSetHandle )
		{
			_ActivateActionSet( Self, controllerHandle, actionSetHandle );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamController_GetCurrentActionSet", CallingConvention = Platform.CC)]
		private static extern ControllerActionSetHandle_t _GetCurrentActionSet( IntPtr self, ControllerHandle_t controllerHandle );
		
		#endregion
		internal ControllerActionSetHandle_t GetCurrentActionSet( ControllerHandle_t controllerHandle )
		{
			var returnValue = _GetCurrentActionSet( Self, controllerHandle );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamController_ActivateActionSetLayer", CallingConvention = Platform.CC)]
		private static extern void _ActivateActionSetLayer( IntPtr self, ControllerHandle_t controllerHandle, ControllerActionSetHandle_t actionSetLayerHandle );
		
		#endregion
		internal void ActivateActionSetLayer( ControllerHandle_t controllerHandle, ControllerActionSetHandle_t actionSetLayerHandle )
		{
			_ActivateActionSetLayer( Self, controllerHandle, actionSetLayerHandle );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamController_DeactivateActionSetLayer", CallingConvention = Platform.CC)]
		private static extern void _DeactivateActionSetLayer( IntPtr self, ControllerHandle_t controllerHandle, ControllerActionSetHandle_t actionSetLayerHandle );
		
		#endregion
		internal void DeactivateActionSetLayer( ControllerHandle_t controllerHandle, ControllerActionSetHandle_t actionSetLayerHandle )
		{
			_DeactivateActionSetLayer( Self, controllerHandle, actionSetLayerHandle );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamController_DeactivateAllActionSetLayers", CallingConvention = Platform.CC)]
		private static extern void _DeactivateAllActionSetLayers( IntPtr self, ControllerHandle_t controllerHandle );
		
		#endregion
		internal void DeactivateAllActionSetLayers( ControllerHandle_t controllerHandle )
		{
			_DeactivateAllActionSetLayers( Self, controllerHandle );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamController_GetActiveActionSetLayers", CallingConvention = Platform.CC)]
		private static extern int _GetActiveActionSetLayers( IntPtr self, ControllerHandle_t controllerHandle, [In,Out] ControllerActionSetHandle_t[]  handlesOut );
		
		#endregion
		internal int GetActiveActionSetLayers( ControllerHandle_t controllerHandle, [In,Out] ControllerActionSetHandle_t[]  handlesOut )
		{
			var returnValue = _GetActiveActionSetLayers( Self, controllerHandle, handlesOut );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamController_GetDigitalActionHandle", CallingConvention = Platform.CC)]
		private static extern ControllerDigitalActionHandle_t _GetDigitalActionHandle( IntPtr self, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pszActionName );
		
		#endregion
		internal ControllerDigitalActionHandle_t GetDigitalActionHandle( [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pszActionName )
		{
			var returnValue = _GetDigitalActionHandle( Self, pszActionName );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamController_GetDigitalActionData", CallingConvention = Platform.CC)]
		private static extern DigitalState _GetDigitalActionData( IntPtr self, ControllerHandle_t controllerHandle, ControllerDigitalActionHandle_t digitalActionHandle );
		
		#endregion
		internal DigitalState GetDigitalActionData( ControllerHandle_t controllerHandle, ControllerDigitalActionHandle_t digitalActionHandle )
		{
			var returnValue = _GetDigitalActionData( Self, controllerHandle, digitalActionHandle );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamController_GetDigitalActionOrigins", CallingConvention = Platform.CC)]
		private static extern int _GetDigitalActionOrigins( IntPtr self, ControllerHandle_t controllerHandle, ControllerActionSetHandle_t actionSetHandle, ControllerDigitalActionHandle_t digitalActionHandle, ref ControllerActionOrigin originsOut );
		
		#endregion
		internal int GetDigitalActionOrigins( ControllerHandle_t controllerHandle, ControllerActionSetHandle_t actionSetHandle, ControllerDigitalActionHandle_t digitalActionHandle, ref ControllerActionOrigin originsOut )
		{
			var returnValue = _GetDigitalActionOrigins( Self, controllerHandle, actionSetHandle, digitalActionHandle, ref originsOut );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamController_GetAnalogActionHandle", CallingConvention = Platform.CC)]
		private static extern ControllerAnalogActionHandle_t _GetAnalogActionHandle( IntPtr self, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pszActionName );
		
		#endregion
		internal ControllerAnalogActionHandle_t GetAnalogActionHandle( [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pszActionName )
		{
			var returnValue = _GetAnalogActionHandle( Self, pszActionName );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamController_GetAnalogActionData", CallingConvention = Platform.CC)]
		private static extern AnalogState _GetAnalogActionData( IntPtr self, ControllerHandle_t controllerHandle, ControllerAnalogActionHandle_t analogActionHandle );
		
		#endregion
		internal AnalogState GetAnalogActionData( ControllerHandle_t controllerHandle, ControllerAnalogActionHandle_t analogActionHandle )
		{
			var returnValue = _GetAnalogActionData( Self, controllerHandle, analogActionHandle );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamController_GetAnalogActionOrigins", CallingConvention = Platform.CC)]
		private static extern int _GetAnalogActionOrigins( IntPtr self, ControllerHandle_t controllerHandle, ControllerActionSetHandle_t actionSetHandle, ControllerAnalogActionHandle_t analogActionHandle, ref ControllerActionOrigin originsOut );
		
		#endregion
		internal int GetAnalogActionOrigins( ControllerHandle_t controllerHandle, ControllerActionSetHandle_t actionSetHandle, ControllerAnalogActionHandle_t analogActionHandle, ref ControllerActionOrigin originsOut )
		{
			var returnValue = _GetAnalogActionOrigins( Self, controllerHandle, actionSetHandle, analogActionHandle, ref originsOut );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamController_GetGlyphForActionOrigin", CallingConvention = Platform.CC)]
		private static extern Utf8StringPointer _GetGlyphForActionOrigin( IntPtr self, ControllerActionOrigin eOrigin );
		
		#endregion
		internal string GetGlyphForActionOrigin( ControllerActionOrigin eOrigin )
		{
			var returnValue = _GetGlyphForActionOrigin( Self, eOrigin );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamController_GetStringForActionOrigin", CallingConvention = Platform.CC)]
		private static extern Utf8StringPointer _GetStringForActionOrigin( IntPtr self, ControllerActionOrigin eOrigin );
		
		#endregion
		internal string GetStringForActionOrigin( ControllerActionOrigin eOrigin )
		{
			var returnValue = _GetStringForActionOrigin( Self, eOrigin );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamController_StopAnalogActionMomentum", CallingConvention = Platform.CC)]
		private static extern void _StopAnalogActionMomentum( IntPtr self, ControllerHandle_t controllerHandle, ControllerAnalogActionHandle_t eAction );
		
		#endregion
		internal void StopAnalogActionMomentum( ControllerHandle_t controllerHandle, ControllerAnalogActionHandle_t eAction )
		{
			_StopAnalogActionMomentum( Self, controllerHandle, eAction );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamController_GetMotionData", CallingConvention = Platform.CC)]
		private static extern MotionState _GetMotionData( IntPtr self, ControllerHandle_t controllerHandle );
		
		#endregion
		internal MotionState GetMotionData( ControllerHandle_t controllerHandle )
		{
			var returnValue = _GetMotionData( Self, controllerHandle );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamController_TriggerHapticPulse", CallingConvention = Platform.CC)]
		private static extern void _TriggerHapticPulse( IntPtr self, ControllerHandle_t controllerHandle, SteamControllerPad eTargetPad, ushort usDurationMicroSec );
		
		#endregion
		internal void TriggerHapticPulse( ControllerHandle_t controllerHandle, SteamControllerPad eTargetPad, ushort usDurationMicroSec )
		{
			_TriggerHapticPulse( Self, controllerHandle, eTargetPad, usDurationMicroSec );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamController_TriggerRepeatedHapticPulse", CallingConvention = Platform.CC)]
		private static extern void _TriggerRepeatedHapticPulse( IntPtr self, ControllerHandle_t controllerHandle, SteamControllerPad eTargetPad, ushort usDurationMicroSec, ushort usOffMicroSec, ushort unRepeat, uint nFlags );
		
		#endregion
		internal void TriggerRepeatedHapticPulse( ControllerHandle_t controllerHandle, SteamControllerPad eTargetPad, ushort usDurationMicroSec, ushort usOffMicroSec, ushort unRepeat, uint nFlags )
		{
			_TriggerRepeatedHapticPulse( Self, controllerHandle, eTargetPad, usDurationMicroSec, usOffMicroSec, unRepeat, nFlags );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamController_TriggerVibration", CallingConvention = Platform.CC)]
		private static extern void _TriggerVibration( IntPtr self, ControllerHandle_t controllerHandle, ushort usLeftSpeed, ushort usRightSpeed );
		
		#endregion
		internal void TriggerVibration( ControllerHandle_t controllerHandle, ushort usLeftSpeed, ushort usRightSpeed )
		{
			_TriggerVibration( Self, controllerHandle, usLeftSpeed, usRightSpeed );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamController_SetLEDColor", CallingConvention = Platform.CC)]
		private static extern void _SetLEDColor( IntPtr self, ControllerHandle_t controllerHandle, byte nColorR, byte nColorG, byte nColorB, uint nFlags );
		
		#endregion
		internal void SetLEDColor( ControllerHandle_t controllerHandle, byte nColorR, byte nColorG, byte nColorB, uint nFlags )
		{
			_SetLEDColor( Self, controllerHandle, nColorR, nColorG, nColorB, nFlags );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamController_ShowBindingPanel", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _ShowBindingPanel( IntPtr self, ControllerHandle_t controllerHandle );
		
		#endregion
		internal bool ShowBindingPanel( ControllerHandle_t controllerHandle )
		{
			var returnValue = _ShowBindingPanel( Self, controllerHandle );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamController_GetInputTypeForHandle", CallingConvention = Platform.CC)]
		private static extern InputType _GetInputTypeForHandle( IntPtr self, ControllerHandle_t controllerHandle );
		
		#endregion
		internal InputType GetInputTypeForHandle( ControllerHandle_t controllerHandle )
		{
			var returnValue = _GetInputTypeForHandle( Self, controllerHandle );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamController_GetControllerForGamepadIndex", CallingConvention = Platform.CC)]
		private static extern ControllerHandle_t _GetControllerForGamepadIndex( IntPtr self, int nIndex );
		
		#endregion
		internal ControllerHandle_t GetControllerForGamepadIndex( int nIndex )
		{
			var returnValue = _GetControllerForGamepadIndex( Self, nIndex );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamController_GetGamepadIndexForController", CallingConvention = Platform.CC)]
		private static extern int _GetGamepadIndexForController( IntPtr self, ControllerHandle_t ulControllerHandle );
		
		#endregion
		internal int GetGamepadIndexForController( ControllerHandle_t ulControllerHandle )
		{
			var returnValue = _GetGamepadIndexForController( Self, ulControllerHandle );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamController_GetStringForXboxOrigin", CallingConvention = Platform.CC)]
		private static extern Utf8StringPointer _GetStringForXboxOrigin( IntPtr self, XboxOrigin eOrigin );
		
		#endregion
		internal string GetStringForXboxOrigin( XboxOrigin eOrigin )
		{
			var returnValue = _GetStringForXboxOrigin( Self, eOrigin );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamController_GetGlyphForXboxOrigin", CallingConvention = Platform.CC)]
		private static extern Utf8StringPointer _GetGlyphForXboxOrigin( IntPtr self, XboxOrigin eOrigin );
		
		#endregion
		internal string GetGlyphForXboxOrigin( XboxOrigin eOrigin )
		{
			var returnValue = _GetGlyphForXboxOrigin( Self, eOrigin );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamController_GetActionOriginFromXboxOrigin", CallingConvention = Platform.CC)]
		private static extern ControllerActionOrigin _GetActionOriginFromXboxOrigin( IntPtr self, ControllerHandle_t controllerHandle, XboxOrigin eOrigin );
		
		#endregion
		internal ControllerActionOrigin GetActionOriginFromXboxOrigin( ControllerHandle_t controllerHandle, XboxOrigin eOrigin )
		{
			var returnValue = _GetActionOriginFromXboxOrigin( Self, controllerHandle, eOrigin );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamController_TranslateActionOrigin", CallingConvention = Platform.CC)]
		private static extern ControllerActionOrigin _TranslateActionOrigin( IntPtr self, InputType eDestinationInputType, ControllerActionOrigin eSourceOrigin );
		
		#endregion
		internal ControllerActionOrigin TranslateActionOrigin( InputType eDestinationInputType, ControllerActionOrigin eSourceOrigin )
		{
			var returnValue = _TranslateActionOrigin( Self, eDestinationInputType, eSourceOrigin );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamController_GetControllerBindingRevision", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _GetControllerBindingRevision( IntPtr self, ControllerHandle_t controllerHandle, ref int pMajor, ref int pMinor );
		
		#endregion
		internal bool GetControllerBindingRevision( ControllerHandle_t controllerHandle, ref int pMajor, ref int pMinor )
		{
			var returnValue = _GetControllerBindingRevision( Self, controllerHandle, ref pMajor, ref pMinor );
			return returnValue;
		}
		
	}
}
