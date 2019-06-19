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
			_internal = null;
		}

		internal static void InstallEvents()
		{
			// None?
		}

	}
}