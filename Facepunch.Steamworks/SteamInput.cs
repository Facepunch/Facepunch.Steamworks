using System;
using Steamworks.Data;
using System.Collections.Generic;

namespace Steamworks
{
	/// <summary>
	/// Class for utilizing Steam Input.
	/// </summary>
	public class SteamInput : SteamClientClass<SteamInput>
	{
		internal static ISteamInput Internal => Interface as ISteamInput;

		internal override bool InitializeInterface( bool server )
		{
			SetInterface( server, new ISteamInput( server ) );
			if ( Interface.Self == IntPtr.Zero ) return false;

			// ISteamInput requires an explicit Init before any other call - without it
			// GetConnectedControllers always returns zero controllers.
			return Internal.Init( false );
		}

		internal override void DestroyInterface( bool server )
		{
			if ( Internal != null && Internal.IsValid )
				Internal.Shutdown();

			// Action and action-set handles are only valid for the lifetime of the input
			// session - keeping them across a Shutdown/Init cycle would serve stale handles
			DigitalHandles.Clear();
			AnalogHandles.Clear();
			ActionSets.Clear();

			base.DestroyInterface( server );
		}

		internal const int STEAM_CONTROLLER_MAX_COUNT = 16;


		/// <summary>
		/// You shouldn't really need to call this because it gets called by <see cref="SteamClient.RunCallbacks"/>
		/// but Valve think it might be a nice idea if you call it right before you get input info -
		/// just to make sure the info you're getting is 100% up to date.
		/// </summary>
		public static void RunFrame()
		{
			Internal.RunFrame( false );
		}

		static readonly InputHandle_t[] queryArray = new InputHandle_t[STEAM_CONTROLLER_MAX_COUNT];

		/// <summary>
		/// Gets a list of connected controllers.
		/// </summary>
		public static IEnumerable<Controller> Controllers
		{
			get
			{
				var num = Internal.GetConnectedControllers( queryArray );

				for ( int i = 0; i < num; i++ )
				{
					yield return new Controller( queryArray[i] );
				}
			}
		}

		/// <summary>
		/// Fills the buffer with connected controllers and returns the count. Identical
		/// results to <see cref="Controllers"/> but doesn't allocate, for per-frame polling.
		/// </summary>
		public static int GetControllers( Controller[] buffer )
		{
			var num = Internal.GetConnectedControllers( queryArray );
			if ( num > buffer.Length ) num = buffer.Length;

			for ( int i = 0; i < num; i++ )
			{
				buffer[i] = new Controller( queryArray[i] );
			}

			return num;
		}


        /// <summary>
        /// Return an absolute path to the PNG image glyph for the provided digital action name. The current
        /// action set in use for the controller will be used for the lookup. You should cache the result and
        /// maintain your own list of loaded PNG assets.
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static unsafe string GetDigitalActionGlyph( Controller controller, string action )
        {
            // Steam writes up to STEAM_INPUT_MAX_ORIGINS entries here - an action can be bound
            // to several inputs at once. stackalloc so the correct-sized buffer costs nothing.
            InputActionOrigin* origins = stackalloc InputActionOrigin[ISteamInput.STEAM_INPUT_MAX_ORIGINS];

            var count = Internal.GetDigitalActionOrigins(
                controller.Handle,
                Internal.GetCurrentActionSet(controller.Handle),
                GetDigitalActionHandle(action),
                origins,
                ISteamInput.STEAM_INPUT_MAX_ORIGINS
            );

            if ( count <= 0 )
                return null;

            return Internal.GetGlyphForActionOrigin_Legacy(origins[0]);
        }


		/// <summary>
		/// Return an absolute path to the PNG image glyph for the provided digital action name. The current
		/// action set in use for the controller will be used for the lookup. You should cache the result and
		/// maintain your own list of loaded PNG assets.
		/// </summary>
		public static unsafe string GetPngActionGlyph( Controller controller, string action, GlyphSize size )
		{
			InputActionOrigin* origins = stackalloc InputActionOrigin[ISteamInput.STEAM_INPUT_MAX_ORIGINS];

			var count = Internal.GetDigitalActionOrigins( controller.Handle, Internal.GetCurrentActionSet( controller.Handle ), GetDigitalActionHandle( action ), origins, ISteamInput.STEAM_INPUT_MAX_ORIGINS );

			if ( count <= 0 )
				return null;

			return Internal.GetGlyphPNGForActionOrigin( origins[0], size, 0 );
		}

		/// <summary>
		/// Return an absolute path to the SVF image glyph for the provided digital action name. The current
		/// action set in use for the controller will be used for the lookup. You should cache the result and
		/// maintain your own list of loaded PNG assets.
		/// </summary>
		public static unsafe string GetSvgActionGlyph( Controller controller, string action )
		{
			InputActionOrigin* origins = stackalloc InputActionOrigin[ISteamInput.STEAM_INPUT_MAX_ORIGINS];

			var count = Internal.GetDigitalActionOrigins( controller.Handle, Internal.GetCurrentActionSet( controller.Handle ), GetDigitalActionHandle( action ), origins, ISteamInput.STEAM_INPUT_MAX_ORIGINS );

			if ( count <= 0 )
				return null;

			return Internal.GetGlyphSVGForActionOrigin( origins[0], 0 );
		}

		internal static Dictionary<string, InputDigitalActionHandle_t> DigitalHandles = new Dictionary<string, InputDigitalActionHandle_t>();
		internal static InputDigitalActionHandle_t GetDigitalActionHandle( string name )
		{
			if ( DigitalHandles.TryGetValue( name, out var val ) )
				return val;

			val = Internal.GetDigitalActionHandle( name );
			DigitalHandles.Add( name, val );
			return val;
		}

		internal static Dictionary<string, InputAnalogActionHandle_t> AnalogHandles = new Dictionary<string, InputAnalogActionHandle_t>();
		internal static InputAnalogActionHandle_t GetAnalogActionHandle( string name )
		{
			if ( AnalogHandles.TryGetValue( name, out var val ) )
				return val;

			val = Internal.GetAnalogActionHandle( name );
			AnalogHandles.Add( name, val );
			return val;
		}

		internal static Dictionary<string, InputActionSetHandle_t> ActionSets = new Dictionary<string, InputActionSetHandle_t>();
		internal static InputActionSetHandle_t GetActionSetHandle( string name )
		{
			if ( ActionSets.TryGetValue( name, out var val ) )
				return val;

			val = Internal.GetActionSetHandle( name );
			ActionSets.Add( name, val );
			return val;
		}

		/// <summary>
		/// Applies a haptic effect to a PlayStation 5 DualSense controller's adaptive
		/// triggers — the motorised resistance that makes a trigger feel like a gun, a
		/// bowstring, or a stiff brake pedal.
		/// </summary>
		/// <param name="controller">The controller to affect.</param>
		/// <param name="effect">
		/// The effect, built with one of the factory methods on
		/// <see cref="DualSenseTriggerEffect"/> — for example
		/// <see cref="DualSenseTriggerEffect.Weapon"/> or
		/// <see cref="DualSenseTriggerEffect.Off"/>.
		/// </param>
		/// <param name="triggers">Which trigger(s) to apply it to. Defaults to both.</param>
		/// <remarks>
		/// <para>
		/// <b>This is a state, not a one-shot.</b> The effect stays applied until you
		/// replace it or send <see cref="DualSenseTriggerEffect.Off"/>. Set it when game
		/// state changes — not every frame — and always clear it when the player holsters
		/// a weapon or returns to a menu, or the trigger stays stiff.
		/// </para>
		/// <para>
		/// <b>It fails silently by design.</b> On anything that is not a DualSense the
		/// call does nothing, and the underlying API returns no status, so there is no way
		/// to detect that it was ignored. If you need to branch on hardware, check the
		/// controller's input type first. Steam Input must be enabled for your app.
		/// </para>
		/// <para>
		/// Passing <see cref="DualSenseTrigger.None"/> is a no-op.
		/// </para>
		/// <example>
		/// <code>
		/// foreach ( var controller in SteamInput.Controllers )
		/// {
		///     SteamInput.SetDualSenseTriggerEffect(
		///         controller,
		///         DualSenseTriggerEffect.Weapon( startPosition: 2, endPosition: 7, strength: 8 ),
		///         DualSenseTrigger.Right );
		/// }
		/// </code>
		/// </example>
		/// </remarks>
		public static void SetDualSenseTriggerEffect( Controller controller, DualSenseTriggerEffect effect, DualSenseTrigger triggers = DualSenseTrigger.Both )
		{
			if ( triggers == DualSenseTrigger.None )
				return;

			var param = new ScePadTriggerEffectParam { TriggerMask = (byte)triggers };

			// The native struct always carries a command slot per trigger; the mask decides
			// which are honoured. Fill only the selected ones so an unselected trigger is
			// left as a zeroed (Off) command rather than an uninitialised one.
			if ( (triggers & DualSenseTrigger.Left) != 0 )
				param.Left = effect.Command;

			if ( (triggers & DualSenseTrigger.Right) != 0 )
				param.Right = effect.Command;

			Internal.SetDualSenseTriggerEffect( controller.Handle, ref param );
		}

		/// <summary>
		/// Applies separate adaptive-trigger effects to the left and right triggers of a
		/// DualSense controller in a single call.
		/// </summary>
		/// <param name="controller">The controller to affect.</param>
		/// <param name="left">The effect for the left trigger (L2).</param>
		/// <param name="right">The effect for the right trigger (R2).</param>
		/// <remarks>
		/// Use this instead of two calls when the triggers do different things — for
		/// example a weapon break on the right and a vehicle brake on the left. See
		/// <see cref="SetDualSenseTriggerEffect(Controller, DualSenseTriggerEffect, DualSenseTrigger)"/>
		/// for the caveats, which apply equally here.
		/// </remarks>
		public static void SetDualSenseTriggerEffects( Controller controller, DualSenseTriggerEffect left, DualSenseTriggerEffect right )
		{
			var param = new ScePadTriggerEffectParam
			{
				TriggerMask = (byte)DualSenseTrigger.Both,
				Left = left.Command,
				Right = right.Command
			};

			Internal.SetDualSenseTriggerEffect( controller.Handle, ref param );
		}
	}
}
