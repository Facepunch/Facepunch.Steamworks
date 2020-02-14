using Steamworks.Data;
using System.Collections.Generic;

namespace Steamworks
{
	public static class SteamInput
	{
		internal const int STEAM_CONTROLLER_MAX_COUNT = 16;

		static ISteamInput _internal;
		internal static ISteamInput Internal
		{
			get
			{
				SteamClient.ValidCheck();

				if ( _internal == null )
				{
					_internal = new ISteamInput();
					_internal.Init();
				}

				return _internal;
			}
		}

		internal static void Shutdown()
		{
			if ( _internal != null && _internal.IsValid )
			{
				_internal.DoShutdown();
			}

			_internal = null;
		}

		internal static void InstallEvents()
		{
			Internal.DoInit();
			Internal.RunFrame();

			// None?
		}

		/// <summary>
		/// You shouldn't really need to call this because it get called by RunCallbacks on SteamClient
		/// but Valve think it might be a nice idea if you call it right before you get input info -
		/// just to make sure the info you're getting is 100% up to date.
		/// </summary>
		public static void RunFrame()
		{
			Internal.RunFrame();
		}

		static InputHandle_t[] queryArray = new InputHandle_t[STEAM_CONTROLLER_MAX_COUNT];

		/// <summary>
		/// Return a list of connected controllers.
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

            Internal.GetDigitalActionOrigins(
                controller.Handle,
                Internal.GetCurrentActionSet(controller.Handle),
                GetDigitalActionHandle(action),
                ref origin
            );

            return Internal.GetGlyphForActionOrigin(origin);
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