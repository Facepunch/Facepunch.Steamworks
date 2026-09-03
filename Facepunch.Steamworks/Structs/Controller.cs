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


		/// <summary>Rumble the body motors, 0-65535. Speeds persist until changed, so call StopVibration when done.</summary>
		public void TriggerVibration( ushort leftSpeed, ushort rightSpeed )
		{
			SteamInput.Internal.TriggerVibration( Handle, leftSpeed, rightSpeed );
		}

		/// <summary>Rumble the body motors and the Xbox trigger motors, 0-65535. Trigger speeds are ignored on other controllers.</summary>
		public void TriggerVibrationExtended( ushort leftSpeed, ushort rightSpeed, ushort leftTriggerSpeed, ushort rightTriggerSpeed )
		{
			SteamInput.Internal.TriggerVibrationExtended( Handle, leftSpeed, rightSpeed, leftTriggerSpeed, rightTriggerSpeed );
		}

		/// <summary>Stops every rumble motor, including the trigger motors.</summary>
		public void StopVibration()
		{
			SteamInput.Internal.TriggerVibrationExtended( Handle, 0, 0, 0, 0 );
		}

		/// <summary>One-shot haptic tick on Steam Deck, Steam Controller and DualSense. Intensity 0-255, gain in dB. No-op on Xbox.</summary>
		public void TriggerSimpleHapticEvent( ControllerHapticLocation location, byte intensity, sbyte gainDb, byte otherIntensity = 0, sbyte otherGainDb = 0 )
		{
			SteamInput.Internal.TriggerSimpleHapticEvent( Handle, location, intensity, (char)gainDb, otherIntensity, (char)otherGainDb );
		}

		/// <summary>Sets the light bar colour on PlayStation controllers and the LED on Steam Controller.</summary>
		public void SetLEDColor( byte r, byte g, byte b )
		{
			SteamInput.Internal.SetLEDColor( Handle, r, g, b, (uint)SteamControllerLEDFlag.SetColor );
		}

		/// <summary>Hands the light bar / LED colour back to the user's own setting.</summary>
		public void RestoreLEDColor()
		{
			SteamInput.Internal.SetLEDColor( Handle, 0, 0, 0, (uint)SteamControllerLEDFlag.RestoreUserDefault );
		}

		/// <summary>Accelerometer and gyro readings for this frame; all zero when the controller has no motion sensors.</summary>
		public MotionState GetMotionData()
		{
			return SteamInput.Internal.GetMotionData( Handle );
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
	public struct MotionState
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