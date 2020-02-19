using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Steamworks.Data;


namespace Steamworks
{
	internal class ISteamInput : SteamInterface
	{
		
		internal ISteamInput( bool IsGameServer )
		{
			SetupInterface( IsGameServer );
		}
		
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_SteamInput_v001", CallingConvention = Platform.CC)]
		internal static extern IntPtr SteamAPI_SteamInput_v001();
		public override IntPtr GetUserInterfacePointer() => SteamAPI_SteamInput_v001();
		
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamInput_Init", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _Init( IntPtr self );
		
		#endregion
		internal bool Init()
		{
			var returnValue = _Init( Self );
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
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamInput_RunFrame", CallingConvention = Platform.CC)]
		private static extern void _RunFrame( IntPtr self );
		
		#endregion
		internal void RunFrame()
		{
			_RunFrame( Self );
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
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamInput_GetActionSetHandle", CallingConvention = Platform.CC)]
		private static extern InputActionSetHandle_t _GetActionSetHandle( IntPtr self, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pszActionSetName );
		
		#endregion
		internal InputActionSetHandle_t GetActionSetHandle( [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pszActionSetName )
		{
			var returnValue = _GetActionSetHandle( Self, pszActionSetName );
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
		private static extern InputDigitalActionHandle_t _GetDigitalActionHandle( IntPtr self, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pszActionName );
		
		#endregion
		internal InputDigitalActionHandle_t GetDigitalActionHandle( [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pszActionName )
		{
			var returnValue = _GetDigitalActionHandle( Self, pszActionName );
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
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamInput_GetAnalogActionHandle", CallingConvention = Platform.CC)]
		private static extern InputAnalogActionHandle_t _GetAnalogActionHandle( IntPtr self, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pszActionName );
		
		#endregion
		internal InputAnalogActionHandle_t GetAnalogActionHandle( [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pszActionName )
		{
			var returnValue = _GetAnalogActionHandle( Self, pszActionName );
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
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamInput_GetGlyphForActionOrigin", CallingConvention = Platform.CC)]
		private static extern Utf8StringPointer _GetGlyphForActionOrigin( IntPtr self, InputActionOrigin eOrigin );
		
		#endregion
		internal string GetGlyphForActionOrigin( InputActionOrigin eOrigin )
		{
			var returnValue = _GetGlyphForActionOrigin( Self, eOrigin );
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
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamInput_SetLEDColor", CallingConvention = Platform.CC)]
		private static extern void _SetLEDColor( IntPtr self, InputHandle_t inputHandle, byte nColorR, byte nColorG, byte nColorB, uint nFlags );
		
		#endregion
		internal void SetLEDColor( InputHandle_t inputHandle, byte nColorR, byte nColorG, byte nColorB, uint nFlags )
		{
			_SetLEDColor( Self, inputHandle, nColorR, nColorG, nColorB, nFlags );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamInput_TriggerHapticPulse", CallingConvention = Platform.CC)]
		private static extern void _TriggerHapticPulse( IntPtr self, InputHandle_t inputHandle, SteamControllerPad eTargetPad, ushort usDurationMicroSec );
		
		#endregion
		internal void TriggerHapticPulse( InputHandle_t inputHandle, SteamControllerPad eTargetPad, ushort usDurationMicroSec )
		{
			_TriggerHapticPulse( Self, inputHandle, eTargetPad, usDurationMicroSec );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamInput_TriggerRepeatedHapticPulse", CallingConvention = Platform.CC)]
		private static extern void _TriggerRepeatedHapticPulse( IntPtr self, InputHandle_t inputHandle, SteamControllerPad eTargetPad, ushort usDurationMicroSec, ushort usOffMicroSec, ushort unRepeat, uint nFlags );
		
		#endregion
		internal void TriggerRepeatedHapticPulse( InputHandle_t inputHandle, SteamControllerPad eTargetPad, ushort usDurationMicroSec, ushort usOffMicroSec, ushort unRepeat, uint nFlags )
		{
			_TriggerRepeatedHapticPulse( Self, inputHandle, eTargetPad, usDurationMicroSec, usOffMicroSec, unRepeat, nFlags );
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
		
	}
}
