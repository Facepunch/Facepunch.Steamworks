using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Steamworks.Data;

namespace Steamworks
{
	public class SteamNetworkingSockets : SteamSharedClass<SteamNetworkingSockets>
	{
		internal static ISteamNetworkingSockets Internal => Interface as ISteamNetworkingSockets;

		internal override void InitializeInterface( bool server )
		{
			SetInterface( server, new ISteamNetworkingSockets( server ) );
			InstallEvents( server );
		}
	
#region SocketInterface

		static readonly Dictionary<uint, SocketManager> SocketInterfaces = new Dictionary<uint, SocketManager>();

		internal static SocketManager GetSocketManager( uint id )
		{
			if ( SocketInterfaces == null ) return null;
			if ( id == 0 ) throw new System.ArgumentException( "Invalid Socket" );

			if ( SocketInterfaces.TryGetValue( id, out var isocket ) )
				return isocket;

			return null;
		}

		internal static void SetSocketManager( uint id, SocketManager manager )
		{
			if ( id == 0 ) throw new System.ArgumentException( "Invalid Socket" );
			SocketInterfaces[id] = manager;
		}
#endregion

#region ConnectionInterface
		static readonly Dictionary<uint, ConnectionManager> ConnectionInterfaces = new Dictionary<uint, ConnectionManager>();

		internal static ConnectionManager GetConnectionManager( uint id )
		{
			if ( ConnectionInterfaces == null ) return null;
			if ( id == 0 ) return null;

			if ( ConnectionInterfaces.TryGetValue( id, out var iconnection ) )
				return iconnection;

			return null;
		}

		internal static void SetConnectionManager( uint id, ConnectionManager manager )
		{
			if ( id == 0 ) throw new System.ArgumentException( "Invalid Connection" );
			ConnectionInterfaces[id] = manager;
		}
#endregion



		internal void InstallEvents( bool server )
		{
			Dispatch.Install<SteamNetConnectionStatusChangedCallback_t>( ConnectionStatusChanged, server );
		}


		private static void ConnectionStatusChanged( SteamNetConnectionStatusChangedCallback_t data )
		{
			//
			// This is a message from/to a listen socket
			//
			if ( data.Nfo.listenSocket.Id > 0 )
			{
				var iface = GetSocketManager( data.Nfo.listenSocket.Id );
				iface?.OnConnectionChanged( data.Conn, data.Nfo );
			}
			else
			{
				var iface = GetConnectionManager( data.Conn.Id );
				iface?.OnConnectionChanged( data.Nfo );
			}

			OnConnectionStatusChanged?.Invoke( data.Conn, data.Nfo );
		}

		public static event Action<Connection, ConnectionInfo> OnConnectionStatusChanged;


		/// <summary>
		/// Creates a "server" socket that listens for clients to connect to by calling
		/// Connect, over ordinary UDP (IPv4 or IPv6)
		/// 
		/// To use this derive a class from SocketManager and override as much as you want.
		/// 
		/// </summary>
		public static T CreateNormalSocket<T>( NetAddress address ) where T : SocketManager, new()
		{
			var t = new T();
			var options = Array.Empty<NetKeyValue>();
			t.Socket = Internal.CreateListenSocketIP( ref address, options.Length, options );
			t.Initialize();

			SetSocketManager( t.Socket.Id, t );
			return t;
		}

		/// <summary>
		/// Creates a "server" socket that listens for clients to connect to by calling
		/// Connect, over ordinary UDP (IPv4 or IPv6).
		/// 
		/// To use this you should pass a class that inherits ISocketManager. You can use
		/// SocketManager to get connections and send messages, but the ISocketManager class
		/// will received all the appropriate callbacks.
		/// 
		/// </summary>
		public static SocketManager CreateNormalSocket( NetAddress address, ISocketManager intrface )
		{
			var options = Array.Empty<NetKeyValue>();
			var socket = Internal.CreateListenSocketIP( ref address, options.Length, options );

			var t = new SocketManager
			{
				Socket = socket,
				Interface = intrface
			};

			t.Initialize();

			SetSocketManager( t.Socket.Id, t );
			return t;
		}

		/// <summary>
		/// Connect to a socket created via <method>CreateListenSocketIP</method>
		/// </summary>
		public static T ConnectNormal<T>( NetAddress address ) where T : ConnectionManager, new()
		{
			var t = new T();
			var options = Array.Empty<NetKeyValue>();
			t.Connection = Internal.ConnectByIPAddress( ref address, options.Length, options );
			SetConnectionManager( t.Connection.Id, t );
			return t;
		}

		/// <summary>
		/// Connect to a socket created via <method>CreateListenSocketIP</method>
		/// </summary>
		public static ConnectionManager ConnectNormal( NetAddress address, IConnectionManager iface )
		{
			var options = Array.Empty<NetKeyValue>();
			var connection = Internal.ConnectByIPAddress( ref address, options.Length, options );

			var t = new ConnectionManager
			{
				Connection = connection,
				Interface = iface
			};

			SetConnectionManager( t.Connection.Id, t );
			return t;
		}

		/// <summary>
		/// Creates a server that will be relayed via Valve's network (hiding the IP and improving ping)
		/// 
		/// To use this derive a class from SocketManager and override as much as you want.
		/// 
		/// </summary>
		public static T CreateRelaySocket<T>( int virtualport = 0 ) where T : SocketManager, new()
		{
			var t = new T();
			var options = Array.Empty<NetKeyValue>();
			t.Socket = Internal.CreateListenSocketP2P( virtualport, options.Length, options );
			t.Initialize();
			SetSocketManager( t.Socket.Id, t );
			return t;
		}

		/// <summary>
		/// Creates a server that will be relayed via Valve's network (hiding the IP and improving ping)
		/// 
		/// To use this you should pass a class that inherits ISocketManager. You can use
		/// SocketManager to get connections and send messages, but the ISocketManager class
		/// will received all the appropriate callbacks.
		/// 
		/// </summary>
		public static SocketManager CreateRelaySocket( int virtualport, ISocketManager intrface )
		{
			var options = Array.Empty<NetKeyValue>();
			var socket = Internal.CreateListenSocketP2P( virtualport, options.Length, options );

			var t = new SocketManager
			{
				Socket = socket,
				Interface = intrface
			};

			t.Initialize();

			SetSocketManager( t.Socket.Id, t );
			return t;
		}

		/// <summary>
		/// Connect to a relay server
		/// </summary>
		public static T ConnectRelay<T>( SteamId serverId, int virtualport = 0 ) where T : ConnectionManager, new()
		{
			var t = new T();
			NetIdentity identity = serverId;
			var options = Array.Empty<NetKeyValue>();
			t.Connection = Internal.ConnectP2P( ref identity, virtualport, options.Length, options );
			SetConnectionManager( t.Connection.Id, t );
			return t;
		}
	}
}