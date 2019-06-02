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
					_internal.Init();

					SocketInterfaces = new Dictionary<uint, SocketInterface>();
					ConnectionInterfaces = new Dictionary<uint, ConnectionInterface>();
				}

				return _internal;
			}
		}

		#region SocketInterface

		static Dictionary<uint, SocketInterface> SocketInterfaces;

		internal static SocketInterface GetSocketInterface( uint id )
		{
			if ( SocketInterfaces == null ) return null;
			if ( id == 0 ) throw new System.ArgumentException( "Invalid Socket" );

			if ( SocketInterfaces.TryGetValue( id, out var isocket ) )
				return isocket;

			return null;
		}

		internal static void SetSocketInterface( uint id, SocketInterface iface )
		{
			if ( id == 0 ) throw new System.ArgumentException( "Invalid Socket" );

			Console.WriteLine( $"Installing Socket For {id}" );
			SocketInterfaces[id] = iface;
		}
		#endregion

		#region ConnectionInterface
		static Dictionary<uint, ConnectionInterface> ConnectionInterfaces;


		internal static ConnectionInterface GetConnectionInterface( uint id )
		{
			if ( ConnectionInterfaces == null ) return null;
			if ( id == 0 ) return null;

			if ( ConnectionInterfaces.TryGetValue( id, out var iconnection ) )
				return iconnection;

			return null;
		}

		internal static void SetConnectionInterface( uint id, ConnectionInterface iface )
		{
			if ( id == 0 ) throw new System.ArgumentException( "Invalid Connection" );
			ConnectionInterfaces[id] = iface;
		}
		#endregion

		internal static void Shutdown()
		{
			_internal = null;
			SocketInterfaces = null;
			ConnectionInterfaces = null;
		}

		internal static void InstallEvents( bool server = false )
		{
			SteamNetConnectionStatusChangedCallback_t.Install( x => ConnectionStatusChanged( x ), server );
		}

		private static void ConnectionStatusChanged( SteamNetConnectionStatusChangedCallback_t data )
		{
			//
			// This is a message from/to a listen socket
			//
			if ( data.Nfo.listenSocket.Id > 0 )
			{
				var iface = GetSocketInterface( data.Nfo.listenSocket.Id );
				iface?.OnConnectionChanged( data.Conn, data.Nfo );
			}
			else
			{
				var iface = GetConnectionInterface( data.Conn.Id );
				iface?.OnConnectionChanged( data.Nfo );
			}

			OnConnectionStatusChanged?.Invoke( data.Conn, data.Nfo );
		}

		public static event Action<Connection, ConnectionInfo> OnConnectionStatusChanged;


		/// <summary>
		/// Creates a "server" socket that listens for clients to connect to by calling
		/// Connect, over ordinary UDP (IPv4 or IPv6)
		/// </summary>
		public static T CreateNormalSocket<T>( NetAddress address ) where T : SocketInterface, new()
		{
			var t = new T();
			t.Socket = Internal.CreateListenSocketIP( ref address );
			SetSocketInterface( t.Socket.Id, t );
			return t;
		}

		/// <summary>
		/// Connect to a socket created via <method>CreateListenSocketIP</method>
		/// </summary>
		public static T ConnectNormal<T>( NetAddress address ) where T : ConnectionInterface, new()
		{
			var t = new T();
			t.Connection = Internal.ConnectByIPAddress( ref address );
			SetConnectionInterface( t.Connection.Id, t );
			return t;
		}

		/// <summary>
		/// Creates a server that will be relayed via Valve's network (hiding the IP and improving ping)
		/// </summary>
		public static T CreateRelaySocket<T>( int virtualport = 0 ) where T : SocketInterface, new()
		{
			var t = new T();
			t.Socket = Internal.CreateListenSocketP2P( virtualport );
			SetSocketInterface( t.Socket.Id, t );
			return t;
		}

		/// <summary>
		/// Connect to a relay server
		/// </summary>
		public static T ConnectRelay<T>( SteamId serverId, int virtualport = 0 ) where T : ConnectionInterface, new()
		{
			var t = new T();
			NetIdentity identity = serverId;
			t.Connection = Internal.ConnectP2P( ref identity, virtualport );
			SetConnectionInterface( t.Connection.Id, t );
			return t;
		}
	}
}