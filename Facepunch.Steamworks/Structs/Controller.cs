using Steamworks.Data;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Steamworks
{
	/// <summary>
	/// One controller currently connected through Steam Input, and the way you read actions from it.
	/// </summary>
	/// <remarks>
	/// <para>
	/// Steam Input is an <b>action-based</b> layer, not a raw gamepad API. Your game never asks
	/// "is the A button down"; it asks "is the <c>jump</c> action active", and the mapping from
	/// physical input to action lives in a controller configuration authored on the Steamworks
	/// partner site or by the player in the Steam overlay. That is what lets a player remap your game
	/// to a DualSense, a Steam Deck or a flight stick without you writing a line of code.
	/// </para>
	/// <para>
	/// <b>It does nothing without a configuration.</b> An app with no action manifest configured, or
	/// a game running outside Steam, returns controllers whose actions are all inactive. There is no
	/// error &#8212; you simply read <see langword="false"/> for ever. That is the single most common
	/// reason Steam Input "does not work".
	/// </para>
	/// <para>
	/// Handles are only valid for as long as the controller stays connected. Re-read the controller
	/// list rather than caching a <see cref="Controller"/> across a disconnect.
	/// </para>
	/// </remarks>
	/// <example>
	/// <code>
	/// SteamInput.RunFrame();                       // required every frame before reading
	///
	/// foreach ( var controller in SteamInput.Controllers )
	/// {
	///     controller.ActionSet = "InGameControls";  // cheap, safe to set every frame
	///
	///     if ( controller.GetDigitalState( "jump" ).Pressed )
	///         Jump();
	///
	///     var move = controller.GetAnalogState( "move" );
	///     if ( move.Active )
	///         Move( move.X, move.Y );
	/// }
	/// </code>
	/// </example>
	public struct Controller
	{
		internal InputHandle_t Handle;

		internal Controller( InputHandle_t inputHandle_t )
		{
			this.Handle = inputHandle_t;
		}

		/// <summary>
		/// Steam's opaque handle for this controller, exposed so you can key your own per-controller
		/// state off it. Not stable across sessions, and reused once a controller disconnects, so it
		/// is not a device identity you can persist.
		/// </summary>
		public ulong Id => Handle.Value;

		/// <summary>
		/// What kind of physical controller this is &#8212; Xbox, PlayStation, Switch, Steam
		/// Controller, generic XInput and so on. Use it to pick the right button glyphs to draw;
		/// do not use it to decide behaviour, because the action mapping already handles that.
		/// </summary>
		/// <remarks>
		/// Queried live from Steam each time you read it. Reports
		/// <see cref="Steamworks.InputType.Unknown"/> for a device Steam does not recognise, so
		/// always have a fallback glyph set.
		/// </remarks>
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

		/// <summary>
		/// Turn off an action set layer previously enabled with <see cref="ActivateLayer"/>.
		/// </summary>
		/// <param name="layer">
		/// The layer's name from your action manifest. An unknown name resolves to a zero handle and
		/// the call does nothing &#8212; there is no error and no return value, so a typo here is
		/// invisible.
		/// </param>
		public void DeactivateLayer( string layer ) => SteamInput.Internal.DeactivateActionSetLayer( Handle, SteamInput.Internal.GetActionSetHandle( layer ) );

		/// <summary>
		/// Enable an action set layer on top of the active action set. Layers are additive overlays
		/// &#8212; use one to rebind a handful of actions temporarily (a vehicle's controls, a
		/// scoped-in weapon) without replacing the whole set and losing everything else.
		/// </summary>
		/// <param name="layer">
		/// The layer's name from your action manifest. An unknown name silently does nothing.
		/// </param>
		/// <remarks>
		/// Several layers can be active at once and they stack, so activating repeatedly without
		/// deactivating leaves them all on. <see cref="ClearLayers"/> is the reliable way back to a
		/// known state.
		/// </remarks>
		public void ActivateLayer( string layer ) => SteamInput.Internal.ActivateActionSetLayer( Handle, SteamInput.Internal.GetActionSetHandle( layer ) );

		/// <summary>
		/// Turn off every action set layer on this controller at once, leaving just the base action
		/// set. The safe way to reset layer state on a scene or mode change.
		/// </summary>
		/// <remarks>
		/// Does not change the active action set itself &#8212; set <see cref="ActionSet"/> for that.
		/// </remarks>
		public void ClearLayers() => SteamInput.Internal.DeactivateAllActionSetLayers( Handle );


		/// <summary>
		/// Returns the current state of the supplied digital game action &#8212; a on/off action such
		/// as jump, fire or reload, whatever the player has bound it to.
		/// </summary>
		/// <param name="actionName">
		/// The action's name from your action manifest, not a button name. Case sensitivity is not
		/// documented by Valve. An unknown name yields a state that is never active rather than an
		/// error, so a typo here looks exactly like an unbound action.
		/// </param>
		/// <returns>
		/// The action's state. Check <see cref="DigitalState.Active"/> before trusting
		/// <see cref="DigitalState.Pressed"/> &#8212; an inactive action reports not-pressed whether
		/// or not the player is holding anything.
		/// </returns>
		/// <remarks>
		/// This reads a snapshot Steam refreshes in <c>SteamInput.RunFrame()</c>, so call that once
		/// per frame first or you will read the same values for ever. It is a level, not an edge:
		/// track the previous frame's value yourself if you need "was just pressed".
		/// </remarks>
		public DigitalState GetDigitalState( string actionName )
		{
			return SteamInput.Internal.GetDigitalActionData( Handle, SteamInput.GetDigitalActionHandle( actionName ) );
		}

		/// <summary>
		/// Returns the current state of the supplied analog game action &#8212; a continuous action
		/// such as move, look or a trigger pull.
		/// </summary>
		/// <param name="actionName">
		/// The action's name from your action manifest. An unknown name yields a never-active state
		/// rather than an error.
		/// </param>
		/// <returns>
		/// The action's state, including how the player's configuration is producing it (see
		/// <see cref="AnalogState.EMode"/>). Check <see cref="AnalogState.Active"/> first.
		/// </returns>
		/// <remarks>
		/// Refreshed by <c>SteamInput.RunFrame()</c>; call that once per frame first. The meaning of
		/// X and Y depends on the source mode &#8212; a joystick gives an absolute position while a
		/// trackpad in relative-mouse mode gives a delta &#8212; so do not assume the values are
		/// normalised or bounded. See <see cref="AnalogState"/>.
		/// </remarks>
		public AnalogState GetAnalogState( string actionName )
		{
			return SteamInput.Internal.GetAnalogActionData( Handle, SteamInput.GetAnalogActionHandle( actionName ) );
		}


		/// <summary>
		/// A short debug description of the form <c>"XBoxOneController.12345"</c>. For logging only.
		/// </summary>
		/// <returns>The controller's input type and handle.</returns>
		public override string ToString() => $"{InputType}.{Handle.Value}";


		/// <summary>
		/// Whether two values refer to the same connected controller.
		/// </summary>
		/// <param name="a">The first controller.</param>
		/// <param name="b">The second controller.</param>
		/// <returns><see langword="true"/> if both wrap the same Steam Input handle.</returns>
		public static bool operator ==( Controller a, Controller b ) => a.Equals( b );

		/// <summary>
		/// Whether two values refer to different controllers.
		/// </summary>
		/// <param name="a">The first controller.</param>
		/// <param name="b">The second controller.</param>
		/// <returns><see langword="true"/> if the handles differ.</returns>
		public static bool operator !=( Controller a, Controller b ) => !(a == b);

		/// <summary>
		/// Whether the object is a <see cref="Controller"/> with the same handle.
		/// </summary>
		/// <param name="p">The object to compare against.</param>
		/// <returns><see langword="true"/> if the handles match.</returns>
		/// <exception cref="System.InvalidCastException">
		/// Thrown for any argument that is not a <see cref="Controller"/>. This casts rather than
		/// type-checking, so unlike a normal <c>Equals</c> override it throws instead of returning
		/// <see langword="false"/> &#8212; and throws
		/// <see cref="System.NullReferenceException"/> for <see langword="null"/>.
		/// </exception>
		public override bool Equals( object p ) => this.Equals( (Controller)p );

		/// <summary>
		/// Hash of the underlying handle, so controllers can be used as dictionary keys for the
		/// duration of a connection.
		/// </summary>
		/// <returns>The handle's hash code.</returns>
		public override int GetHashCode() => Handle.GetHashCode();

		/// <summary>
		/// Whether this refers to the same connected controller as another value.
		/// </summary>
		/// <param name="p">The controller to compare against.</param>
		/// <returns><see langword="true"/> if both wrap the same Steam Input handle.</returns>
		/// <remarks>
		/// Compares handles, which Steam reuses after a disconnect. Two values equal here are the
		/// same controller <i>right now</i>; that is not evidence they are the same physical device
		/// across a reconnect.
		/// </remarks>
		public bool Equals( Controller p ) => p.Handle == Handle;
	}

	/// <summary>
	/// The current value of an analog Steam Input action &#8212; a stick, trackpad, trigger or
	/// gyro-driven axis, as produced by whatever the player bound it to.
	/// </summary>
	/// <remarks>
	/// <b>The units depend on <see cref="EMode"/>.</b> A joystick in <c>JoystickMove</c> mode gives an
	/// absolute position, roughly -1 to 1 per axis; a trackpad in <c>RelativeMouse</c> mode gives a
	/// per-frame delta with no fixed bound; a trigger gives 0 to 1 in X with Y unused. Writing input
	/// code that assumes a normalised stick will misbehave the moment a player rebinds the action to
	/// something else, which is the whole point of Steam Input. Branch on <see cref="EMode"/>, or
	/// clamp defensively.
	/// </remarks>
	[StructLayout( LayoutKind.Sequential, Pack = 1 )]
	public struct AnalogState
	{
		/// <summary>
		/// How the player's configuration is producing this action &#8212; joystick move, relative
		/// mouse, trigger, scroll wheel, radial menu and so on. Decides how to interpret
		/// <see cref="X"/> and <see cref="Y"/>.
		/// </summary>
		public InputSourceMode EMode; // eMode EInputSourceMode

		/// <summary>
		/// The horizontal component, or the whole value for one-dimensional sources such as a
		/// trigger. Range and meaning depend on <see cref="EMode"/>.
		/// </summary>
		public float X; // x float

		/// <summary>
		/// The vertical component. Left at 0 by one-dimensional sources. Range and meaning depend on
		/// <see cref="EMode"/>.
		/// </summary>
		/// <remarks>
		/// Sign convention is not documented by Valve and varies with the source mode, so verify
		/// which way is up against a real controller rather than assuming.
		/// </remarks>
		public float Y; // y float

		internal byte BActive; // bActive byte

		/// <summary>
		/// Whether this action is currently bound and available on this controller. When
		/// <see langword="false"/>, <see cref="X"/> and <see cref="Y"/> are meaningless rather than
		/// zero-because-centred &#8212; the action is not in the active action set, the name did not
		/// resolve, or no configuration is loaded.
		/// </summary>
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

	/// <summary>
	/// The current value of a digital Steam Input action &#8212; an on/off action such as jump or
	/// fire, however the player has bound it.
	/// </summary>
	/// <remarks>
	/// Always test <see cref="Active"/> before <see cref="Pressed"/>. An action that is not in the
	/// active action set reports <see cref="Pressed"/> as <see langword="false"/> regardless of what
	/// the player is holding, so the two together distinguish "not held" from "not listening".
	/// </remarks>
	[StructLayout( LayoutKind.Sequential, Pack = 1 )]
	public struct DigitalState
	{
		[MarshalAs( UnmanagedType.I1 )]
		internal byte BState; // bState byte
		[MarshalAs( UnmanagedType.I1 )]
		internal byte BActive; // bActive byte

		/// <summary>
		/// Whether the action is being held down right now. This is a level, not an edge &#8212; it
		/// stays <see langword="true"/> for every frame the input is held. Compare against last
		/// frame's value yourself to detect the press and release moments.
		/// </summary>
		public bool Pressed => BState != 0;

		/// <summary>
		/// Whether this action is currently bound and available on this controller. False when the
		/// action is not in the active action set, when the action name did not resolve, and when no
		/// controller configuration is loaded at all.
		/// </summary>
		public bool Active => BActive != 0;
	}
}