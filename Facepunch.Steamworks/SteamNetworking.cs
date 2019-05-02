using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Steamworks.Data;

namespace Steamworks
{
	public static class SteamNetworkingSockets
	{
		static ISteamNetworkingSockets _internal;
		internal static ISteamNetworkingSockets Internal
		{
			get
			{
				if ( _internal == null )
				{
					_internal = new ISteamNetworkingSockets();
					_internal.InitClient();
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

		}
	}
}