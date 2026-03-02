using Steamworks.Data;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Steamworks
{
	public struct Controller
	{
		internal InputHandle_t Handle;

		internal Controller( InputHandle_t inputHandle_t )
		{
			this.Handle = inputHandle_t;
		}

		public ulong Id => Handle.Value;
		public InputType InputType => SteamInput.Internal.GetInputTypeForHandle( Handle );

		/// <summary>
		/// Reconfigure the controller to use the specified action set (ie 'Menu', 'Walk' or 'Drive')
		/// This is cheap, and can be safely called repeatedly. It's often easier to repeatedly call it in
		/// our state loops, instead of trying to place it in all of your state transitions.
		/// </summary>
		public string ActionSet
		{
			set => SteamInput.Internal.ActivateActionSet( Handle, SteamInput.Internal.GetActionSetHandle( value ) );
		}

		public void DeactivateLayer( string layer ) => SteamInput.Internal.DeactivateActionSetLayer( Handle, SteamInput.Internal.GetActionSetHandle( layer ) );
		public void ActivateLayer( string layer ) => SteamInput.Internal.ActivateActionSetLayer( Handle, SteamInput.Internal.GetActionSetHandle( layer ) );
		public void ClearLayers() => SteamInput.Internal.DeactivateAllActionSetLayers( Handle );


		/// <summary>
		/// Returns the current state of the supplied digital game action
		/// </summary>
		public DigitalState GetDigitalState( string actionName )
		{
			return SteamInput.Internal.GetDigitalActionData( Handle, SteamInput.GetDigitalActionHandle( actionName ) );
		}

		/// <summary>
		/// Returns the current state of these supplied analog game action
		/// </summary>
		public AnalogState GetAnalogState( string actionName )
		{
			return SteamInput.Internal.GetAnalogActionData( Handle, SteamInput.GetAnalogActionHandle( actionName ) );
		}

		/// <summary>
		/// Trigger a vibration event on supported controllers.
		/// </summary>
		/// <remarks>
		/// <para>This API call will be ignored for incompatible controller models.</para>
		/// <para>This generates the traditional "rumble" vibration effect.</para>
		/// </remarks>
		/// <param name="leftSpeed">The intensity value for the left rumble motor.</param>
		/// <param name="rightSpeed">The intensity value of the right rumble motor.</param>
		public void TriggerVibration( ushort leftSpeed, ushort rightSpeed )
		{
			SteamInput.Internal.TriggerVibration( Handle, leftSpeed, rightSpeed );
		}

		/// <summary>
		/// Trigger a vibration event on supported controllers, including impulse trigger for Xbox One controllers.
		/// <para>This API call will be ignored for incompatible controller models.</para>
		/// <para>This generates the traditional "rumble" vibration effect.</para>
		/// </summary>
		/// <param name="leftSpeed">The intensity value for the left rumble motor.</param>
		/// <param name="rightSpeed">The intensity value of the right rumble motor.</param>
		/// <param name="leftTriggerSpeed">The intensity value of the Xbox One left trigger rumble</param>
		/// <param name="rightTriggerSpeed">The intensity value of the Xbox One right trigger rumble.</param>
		public void TriggerVibrationExtended( ushort leftSpeed, ushort rightSpeed, ushort leftTriggerSpeed, ushort rightTriggerSpeed )
		{
			SteamInput.Internal.TriggerVibrationExtended( Handle, leftSpeed, rightSpeed, leftTriggerSpeed, rightTriggerSpeed );
		}

		/// <summary>
		/// Set the controller LED color on supported controllers.
		/// </summary>
		/// <param name="red">The red component of the color to set (0-255).</param>
		/// <param name="green">The green component of the color to set (0-255).</param>
		/// <param name="blue">The blue component of the color to set (0-255).</param>
		public void SetLEDColor( byte red, byte green, byte blue )
		{
			SteamInput.Internal.SetLEDColor( Handle, red, green, blue, (uint)SteamControllerLEDFlag.SetColor );
		}

		/// <summary>
		/// Set the controller LED color on supported controllers.
		/// </summary>
		/// <param name="color">Color to set the LED</param>
		public void SetLEDColor( Color color )
		{
			SteamInput.Internal.SetLEDColor( Handle, color.r, color.g, color.b, (uint)SteamControllerLEDFlag.SetColor );
		}

		/// <summary>
		/// Restore the controller LED color to default (out-of-game) settings
		/// </summary>
		public void RestoreUserLEDColor()
		{
			SteamInput.Internal.SetLEDColor( Handle, 0, 0, 0, (uint)SteamControllerLEDFlag.RestoreUserDefault );
		}

		public override string ToString() => $"{InputType}.{Handle.Value}";


		public static bool operator ==( Controller a, Controller b ) => a.Equals( b );
		public static bool operator !=( Controller a, Controller b ) => !(a == b);
		public override bool Equals( object p ) => this.Equals( (Controller)p );
		public override int GetHashCode() => Handle.GetHashCode();
		public bool Equals( Controller p ) => p.Handle == Handle;
	}

	[StructLayout( LayoutKind.Sequential, Pack = 1 )]
	public struct AnalogState
	{
		public InputSourceMode EMode; // eMode EInputSourceMode
		public float X; // x float
		public float Y; // y float
		internal byte BActive; // bActive byte
		public bool Active => BActive != 0;
	}

	[StructLayout( LayoutKind.Sequential, Pack = 1 )]
	internal struct MotionState
	{
		public float RotQuatX; // rotQuatX float
		public float RotQuatY; // rotQuatY float
		public float RotQuatZ; // rotQuatZ float
		public float RotQuatW; // rotQuatW float
		public float PosAccelX; // posAccelX float
		public float PosAccelY; // posAccelY float
		public float PosAccelZ; // posAccelZ float
		public float RotVelX; // rotVelX float
		public float RotVelY; // rotVelY float
		public float RotVelZ; // rotVelZ float
	}

	[StructLayout( LayoutKind.Sequential, Pack = 1 )]
	public struct DigitalState
	{
		[MarshalAs( UnmanagedType.I1 )]
		internal byte BState; // bState byte
		[MarshalAs( UnmanagedType.I1 )]
		internal byte BActive; // bActive byte

		public bool Pressed => BState != 0;
		public bool Active => BActive != 0;
	}
}
