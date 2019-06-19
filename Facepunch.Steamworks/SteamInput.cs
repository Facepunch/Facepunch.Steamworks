using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Steamworks.Data;

namespace Steamworks
{
	public static class SteamInput
	{
		internal const  int STEAM_CONTROLLER_MAX_COUNT = 16;

		static ISteamInput _internal;
		internal static ISteamInput Internal
		{
			get
			{
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
			_internal?.DoShutdown();
			_internal = null;
		}

		internal static void InstallEvents()
		{
			Internal.DoInit();
			Internal.RunFrame();

			// None?
		}


		static InputHandle_t[] queryArray = new InputHandle_t[STEAM_CONTROLLER_MAX_COUNT];

		/// <summary>
		/// Return a list of connected controllers. Will return null if none found.
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

	}
}