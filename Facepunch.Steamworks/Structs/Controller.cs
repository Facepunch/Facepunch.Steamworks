using Steamworks.Data;
using System.Collections.Generic;
using System.Linq;
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

		#region Action Origins

		internal const int STEAM_INPUT_MAX_ORIGINS = 8;
		internal InputActionSetHandle_t ActionSetHandle => SteamInput.Internal.GetCurrentActionSet( Handle );

		/// <summary>
		/// Get the origin(s) for an analog action within an action set.
		/// Use this to display the appropriate on-screen prompt for the action.
		/// <para>
		/// This is cheap, and Valve recommends you re-gather the origins each frame to update your ingame prompts in case they have changed.
		/// </para>
		/// </summary>
		public IEnumerable<InputActionOrigin> GetAnalogActionOrigins( string actionName )
		{
			InputActionOrigin[] actionOrigins = new InputActionOrigin[STEAM_INPUT_MAX_ORIGINS];
			int numOrigins = SteamInput.Internal.GetAnalogActionOrigins( Handle, ActionSetHandle, SteamInput.Internal.GetAnalogActionHandle( actionName ), ref actionOrigins[0] );
			return actionOrigins.Take( numOrigins );
		}

		/// <summary>
		/// Get the origin(s) for a digital action within an action set.
		/// Use this to display the appropriate on-screen prompt for the action.
		/// <para>
		/// This is cheap, and Valve recommends you re-gather the origins each frame to update your ingame prompts in case they have changed.
		/// </para>
		/// </summary>
		public IEnumerable<InputActionOrigin> GetDigitalActionOrigins( string actionName )
		{
			InputActionOrigin[] actionOrigins = new InputActionOrigin[STEAM_INPUT_MAX_ORIGINS];
			int numOrigins = SteamInput.Internal.GetDigitalActionOrigins( Handle, ActionSetHandle, SteamInput.Internal.GetDigitalActionHandle( actionName ), ref actionOrigins[0] );
			return actionOrigins.Take( numOrigins );
		}

		#endregion Action Origins

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