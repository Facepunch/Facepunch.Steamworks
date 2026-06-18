using System;
using Steamworks.Data;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace Steamworks
{
	/// <summary>
	/// Class for utilizing Steam Input.
	/// </summary>
	public class SteamInput : SteamClientClass<SteamInput>
	{
		internal static ISteamInput Internal => Interface as ISteamInput;

		internal static int STEAM_INPUT_MAX_ORIGINS = 16;

		internal override bool InitializeInterface( bool server )
		{
			SetInterface( server, new ISteamInput( server ) );
			if ( Interface.Self == IntPtr.Zero ) return false;

			return true;
		}

		internal const int STEAM_CONTROLLER_MAX_COUNT = 16;

		/// <summary>
		/// Must be called when starting use of the ISteamInput interface.
		/// </summary>
		public static void Init( bool explicitlyCallRunFrame = false )
		{
			Internal.Init( explicitlyCallRunFrame );
		}


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
		/// Return an absolute path to the PNG image glyph for the provided analog action name. The current
		/// action set in use for the controller will be used for the lookup. You should cache the result and
		/// maintain your own list of loaded PNG assets.
		/// </summary>
		/// <param name="controller"></param>
		/// <param name="action"></param>
		/// <returns></returns>
		public static string GetAnalogActionGlyph( Controller controller, string action )
		{
			InputActionOrigin origin = InputActionOrigin.None;

			Internal.GetAnalogActionOrigins(
				controller.Handle,
				Internal.GetCurrentActionSet( controller.Handle ),
				GetAnalogActionHandle( action ),
				ref origin
			);

			return Internal.GetGlyphForActionOrigin_Legacy( origin );
		}


		/// <summary>
		/// Return an array of all origins mapped to the provided analog action in the current action set
		/// </summary>
		/// <returns></returns>	
		public static InputActionOrigin[] GetAnalogActionOrigins( Controller controller, string action )
		{
			InputActionOrigin[] origins = new InputActionOrigin[STEAM_INPUT_MAX_ORIGINS];

			Internal.GetAnalogActionOrigins(
				controller.Handle,
				Internal.GetCurrentActionSet( controller.Handle ),
				GetAnalogActionHandle( action ),
				ref origins[0]
			);
			return origins;
		}


		/// <summary>
		/// Return an array of all origins mapped to the provided analog action in the specified action set
		/// </summary>
		/// <returns></returns>	
		public static InputActionOrigin[] GetAnalogActionOrigins( Controller controller, string actionSet,  string action )
		{
			InputActionOrigin[] origins = new InputActionOrigin[STEAM_INPUT_MAX_ORIGINS];

			Internal.GetAnalogActionOrigins(
				controller.Handle,
				Internal.GetActionSetHandle(actionSet),
				GetAnalogActionHandle( action ),
				ref origins[0]
			);
			return origins;
		}


		/// <summary>
		/// Return an absolute path to the PNG image glyph for the provided digital action name. The current
		/// action set in use for the controller will be used for the lookup. You should cache the result and
		/// maintain your own list of loaded PNG assets.
		/// </summary>
		/// <param name="controller"></param>
		/// <param name="action"></param>
		/// <returns></returns>
		public static string GetDigitalActionGlyph( Controller controller, string action )
        {
            InputActionOrigin origin = InputActionOrigin.None;
			InputActionOrigin[] origins = new InputActionOrigin[16]; 

			int originCount = Internal.GetDigitalActionOrigins(
                controller.Handle,
                Internal.GetCurrentActionSet(controller.Handle),
                GetDigitalActionHandle(action),
                ref origin
            );

			return Internal.GetGlyphForActionOrigin_Legacy(origin);
        }


		/// <summary>
		/// Return an array of all origins mapped to the provided digital action in the current action set
		/// </summary>
		/// <returns></returns>	
		public static InputActionOrigin[] GetDigitalActionOrigins( Controller controller, string action )
		{
			InputActionOrigin[] origins = new InputActionOrigin[STEAM_INPUT_MAX_ORIGINS];

			Internal.GetDigitalActionOrigins(
				controller.Handle,
				Internal.GetCurrentActionSet( controller.Handle ),
				GetDigitalActionHandle( action ),
				ref origins[0]
			);
			return origins;
		}


		/// <summary>
		/// Return an array of all origins mapped to the provided action set and digital action
		/// </summary>
		/// <returns></returns>	
		public static InputActionOrigin[] GetDigitalActionOrigins( Controller controller, string actionSet, string action )
		{
			InputActionOrigin[] origins = new InputActionOrigin[STEAM_INPUT_MAX_ORIGINS];

			Internal.GetDigitalActionOrigins(
				controller.Handle,
				Internal.GetActionSetHandle( actionSet ),
				GetDigitalActionHandle( action ),
				ref origins[0]
			);
			return origins;
		}


		/// <summary>
		/// Return an absolute path to the PNG image glyph for the provided digital action name. The current
		/// action set in use for the controller will be used for the lookup. You should cache the result and
		/// maintain your own list of loaded PNG assets.
		/// </summary>
		public static string GetPngActionGlyph( Controller controller, string action, GlyphSize size )
		{
			InputActionOrigin origin = InputActionOrigin.None;

			Internal.GetDigitalActionOrigins( controller.Handle, Internal.GetCurrentActionSet( controller.Handle ), GetDigitalActionHandle( action ), ref origin );

			return Internal.GetGlyphPNGForActionOrigin( origin, size, 0 );
		}

		/// <summary>
		/// Return an absolute path to the PNG image glyph for the provided action origin
		/// </summary>
		public static string GetPngActionGlyphForOrigin( InputActionOrigin origin, GlyphSize size )
		{
			return Internal.GetGlyphPNGForActionOrigin( origin, size, 0 );
		}

		/// <summary>
		/// Return an absolute path to the SVF image glyph for the provided digital action name. The current
		/// action set in use for the controller will be used for the lookup. You should cache the result and
		/// maintain your own list of loaded PNG assets.
		/// </summary>
		public static string GetSvgActionGlyph( Controller controller, string action )
		{
			InputActionOrigin origin = InputActionOrigin.None;

			Internal.GetDigitalActionOrigins( controller.Handle, Internal.GetCurrentActionSet( controller.Handle ), GetDigitalActionHandle( action ), ref origin );

			return Internal.GetGlyphSVGForActionOrigin( origin, 0 );
		}

		/// <summary>
		/// Show the binding panel for the specified controller.  
		/// If the player is using Big Picture Mode the configuration will open in the overlay. 
		/// In desktop mode a popup window version of Big Picture will be created and open the configuration.
		/// </summary>
		/// <returns>true for success; false if overlay is disabled or unavailable.</returns>
		public static bool ShowBindingPanel(Controller controller)
		{
			return Internal.ShowBindingPanel(controller.Handle);
		}

		/// <summary>
		/// Get the localized description of the button assigned to the specified digital action
		/// </summary>
		public static string GetDigitalActionDescription(Controller controller, string action)
		{
			InputActionOrigin origin = InputActionOrigin.None;

			Internal.GetDigitalActionOrigins(
				controller.Handle,
				Internal.GetCurrentActionSet( controller.Handle ),
				GetDigitalActionHandle( action ),
				ref origin
			);
			return Internal.GetStringForActionOrigin( origin );
		}

		/// <summary>
		/// Get the localized description of the joystick or gamepad assigned to the specified analog action
		/// </summary>
		public static string GetAnalogActionDescription( Controller controller, string action )
		{
			InputActionOrigin origin = InputActionOrigin.None;

			Internal.GetAnalogActionOrigins(
				controller.Handle,
				Internal.GetCurrentActionSet( controller.Handle ),
				GetAnalogActionHandle( action ),
				ref origin
			);
			return Internal.GetStringForActionOrigin( origin );
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
	}
}
