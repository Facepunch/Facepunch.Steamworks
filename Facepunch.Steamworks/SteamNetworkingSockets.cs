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
					_internal.InitUserless();
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

		/// <summary>
		/// Creates a "server" socket that listens for clients to connect to by calling
		/// Connect, over ordinary UDP (IPv4 or IPv6)
		/// </summary>
		public static HSteamListenSocket CreateExposedSocket( SteamNetworkingIPAddr address )
		{
			return Internal.CreateListenSocketIP( ref address );
		}

		/// <summary>
		/// Connect to a socket created via <method>CreateListenSocketIP</method>
		/// </summary>
		public static NetConnection ConnectExposed( SteamNetworkingIPAddr address )
		{
			return Internal.ConnectByIPAddress( ref address );
		}

		/// <summary>
		/// Creates a server that will be relayed via Valve's network (hiding the IP and improving ping)
		/// </summary>
		public static HSteamListenSocket CreateSocket( int virtualport = 0 )
		{
			return Internal.CreateListenSocketP2P( virtualport );
		}

		/// <summary>
		/// Connect to a relay server
		/// </summary>
		public static NetConnection Connect( SteamNetworkingIdentity identity, int virtualport = 0 )
		{
			return Internal.ConnectP2P( ref identity, virtualport );
		}
	}
}