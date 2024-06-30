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
		
		internal static ISteamNetworkingSockets ServerInternal => InterfaceServer as ISteamNetworkingSockets;
		
		internal static ISteamNetworkingSockets ClientInternal => InterfaceClient as ISteamNetworkingSockets;

		/// <summary>
		/// Get the identity assigned to this interface.
		/// E.g. on Steam, this is the user's SteamID, or for the gameserver interface, the SteamID assigned
		/// to the gameserver.  Returns false and sets the result to an invalid identity if we don't know
		/// our identity yet.  (E.g. GameServer has not logged in.  On Steam, the user will know their SteamID
		/// even if they are not signed into Steam.)
		/// </summary>
		public static NetIdentity Identity
		{
			get
			{
				NetIdentity identity = default;

				Internal.GetIdentity( ref identity );

				return identity;
			}
		}

		internal override bool InitializeInterface( bool server )
		{
			SetInterface( server, new ISteamNetworkingSockets( server ) );
			if ( Interface.Self == IntPtr.Zero ) return false;

			InstallEvents( server );

			OnConnectionStatusChanged = null;
			return true;
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
			Dispatch.Install<SteamNetworkingFakeIPResult_t>( FakeIPResult, server );
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

		private static void FakeIPResult( SteamNetworkingFakeIPResult_t data )
		{
			foreach ( var port in data.Ports )
			{
				if ( port == 0 ) continue;

				var address = NetAddress.From( Utility.Int32ToIp( data.IP ), port );

				OnFakeIPResult?.Invoke( address );
			}
		}

		public static event Action<NetAddress> OnFakeIPResult;

		/// <summary>
		/// Creates a "server" socket that listens for clients to connect to by calling
		/// Connect, over ordinary UDP (IPv4 or IPv6)
		/// 
		/// To use this derive a class from <see cref="SocketManager"/> and override as much as you want.
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
		/// To use this you should pass a class that inherits <see cref="ISocketManager"/>. You can use
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
		/// Connect to a socket created via <c>CreateListenSocketIP</c>.
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
		/// Connect to a socket created via <c>CreateListenSocketIP</c>.
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
		/// Creates a server that will be relayed via Valve's network (hiding the IP and improving ping).
		/// 
		/// To use this derive a class from <see cref="SocketManager"/> and override as much as you want.
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
		/// Creates a server that will be relayed via Valve's network (hiding the IP and improving ping).
		/// 
		/// To use this you should pass a class that inherits <see cref="ISocketManager"/>. You can use
		/// <see cref="SocketManager"/> to get connections and send messages, but the <see cref="ISocketManager"/> class
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
		/// Connect to a relay server.
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

		/// <summary>
		/// Connect to a relay server.
		/// </summary>
		public static ConnectionManager ConnectRelay( SteamId serverId, int virtualport, IConnectionManager iface )
		{
			NetIdentity identity = serverId;
			var options = Array.Empty<NetKeyValue>();
			var connection = Internal.ConnectP2P( ref identity, virtualport, options.Length, options );

			var t = new ConnectionManager
			{
				Connection = connection,
				Interface = iface
			};

			SetConnectionManager( t.Connection.Id, t );
			return t;
		}

		public static Socket CreateNormalSocket_Server( NetAddress netAddress )
		{
			var options = Array.Empty<NetKeyValue>();

			return ServerInternal.CreateListenSocketIP( ref netAddress, options.Length, options );
		}

		public static Socket CreateRelaySocket_Server( int virtualPort = 0 )
		{
			var options = Array.Empty<NetKeyValue>();

			return ServerInternal.CreateListenSocketP2P( virtualPort, options.Length, options );
		}

		public static Socket CreateNormalSocket_Client( NetAddress netAddress )
		{
			var options = Array.Empty<NetKeyValue>();

			return ClientInternal.CreateListenSocketIP( ref netAddress, options.Length, options );
		}

		public static Socket CreateRelaySocket_Client( int virtualPort = 0 )
		{
			var options = Array.Empty<NetKeyValue>();

			return ClientInternal.CreateListenSocketP2P( virtualPort, options.Length, options );
		}

		public static bool CloseListenSocket_Server( Socket socket )
		{
			return ServerInternal.CloseListenSocket( socket );
		}
		
		public static bool CloseListenSocket_Client( Socket socket )
		{
			return ClientInternal.CloseListenSocket( socket );
		}

		public static Connection ConnectNormal_Client( NetAddress netAddress )
		{
			var options = Array.Empty<NetKeyValue>();
			
			return ClientInternal.ConnectByIPAddress( ref netAddress, options.Length, options  );
		}

		public static Connection ConnectRelay_Client( NetIdentity netIdentity, int virtualPort )
		{
			var options = Array.Empty<NetKeyValue>();
			
			return ClientInternal.ConnectP2P( ref netIdentity, virtualPort, options.Length, options  );
		}

		public static bool CloseConnection_Server( Connection connection, bool linger = false, int reasonCode = 0, string debugString = "Closing Connection" )
		{
			return ServerInternal.CloseConnection( connection, reasonCode, debugString, linger );
		}

		public static bool CloseConnection_Client( Connection connection, bool linger = false, int reasonCode = 0, string debugString = "Closing Connection" )
		{
			return ClientInternal.CloseConnection( connection, reasonCode, debugString, linger );
		}
		
		/// <returns><see cref="Result"/></returns>
		public static int AcceptConnection_Server( Connection connection )
		{
			return (int)ServerInternal.AcceptConnection( connection );
		}

		/// <returns><see cref="Result"/></returns>
		public static int AcceptConnection_Client( Connection connection )
		{
			return (int)ClientInternal.AcceptConnection( connection );
		}

		public static bool SetConnectionPollGroup_Server( Connection connection, HSteamNetPollGroup pollGroup )
		{
			return ServerInternal.SetConnectionPollGroup( connection, pollGroup );
		}

		public static bool SetConnectionPollGroup_Client( Connection connection, HSteamNetPollGroup pollGroup )
		{
			return ClientInternal.SetConnectionPollGroup( connection, pollGroup );
		}

		public static int ReceiveMessagesOnPollGroup_Server( HSteamNetPollGroup pollGroup, IntPtr buffer, int maxMessages )
		{
			return ServerInternal.ReceiveMessagesOnPollGroup( pollGroup, buffer, maxMessages );
		}

		public static int ReceiveMessagesOnPollGroup_Client( HSteamNetPollGroup pollGroup, IntPtr buffer, int maxMessages )
		{
			return ClientInternal.ReceiveMessagesOnPollGroup( pollGroup, buffer, maxMessages );
		}

		public static HSteamNetPollGroup CreatePollGroup_Server()
		{
			return ServerInternal.CreatePollGroup();
		}

		public static HSteamNetPollGroup CreatePollGroup_Client()
		{
			return ClientInternal.CreatePollGroup();
		}

		public static bool DestroyPollGroup_Server( HSteamNetPollGroup pollGroup )
		{
			return ServerInternal.DestroyPollGroup( pollGroup );
		}

		public static bool DestroyPollGroup_Client( HSteamNetPollGroup pollGroup )
		{
			return ClientInternal.DestroyPollGroup( pollGroup );
		}

		public static unsafe void SendMessages_Server( int messageCount, NetMsg** netMsg, long* numberMessagesResult )
		{
			ServerInternal.SendMessages( messageCount, netMsg, numberMessagesResult );
		}

		public static unsafe void SendMessages_Client( int messageCount, NetMsg** netMsg, long* numberMessagesResult )
		{
			ClientInternal.SendMessages( messageCount, netMsg, numberMessagesResult );
		}

		public static bool GetConnectionName( Connection hPeer, IntPtr pszName, int nMaxLen )
		{
			return ISteamNetworkingSockets._GetConnectionName( Internal.Self, hPeer, pszName, nMaxLen );
		}

		public static  int ReceiveMessagesOnConnection_Server( Connection hConn, IntPtr ppOutMessages, int nMaxMessages )
		{
			return ServerInternal.ReceiveMessagesOnConnection( hConn, ppOutMessages, nMaxMessages );
		}

		public static  int ReceiveMessagesOnConnection_Client( Connection hConn, IntPtr ppOutMessages, int nMaxMessages )
		{
			return ClientInternal.ReceiveMessagesOnConnection( hConn, ppOutMessages, nMaxMessages );
		}
		
		/// <summary>
		/// Begin asynchronous process of allocating a fake IPv4 address that other
		/// peers can use to contact us via P2P. IP addresses returned by this
		/// function are globally unique for a given appid.
		///
		/// For gameservers, you *must* call this after initializing the SDK but before
		/// beginning login.  Steam needs to know in advance that FakeIP will be used.
		/// </summary>
		public static bool RequestFakeIP( int numFakePorts = 1 )
		{
			return Internal.BeginAsyncRequestFakeIP( numFakePorts );
		}

		/// <summary>
		/// Return info about the FakeIP and port that we have been assigned, if any.
		/// 
		/// </summary>
		public static Result GetFakeIP( int fakePortIndex, out NetAddress address )
		{
			var pInfo = default( SteamNetworkingFakeIPResult_t );

			Internal.GetFakeIP( 0, ref pInfo );

			address = NetAddress.From( Utility.Int32ToIp( pInfo.IP ), pInfo.Ports[fakePortIndex] );
			return pInfo.Result;
		}

		/// <summary>
		/// Creates a server that will be relayed via Valve's network (hiding the IP and improving ping).
		/// 
		/// To use this derive a class from <see cref="SocketManager"/> and override as much as you want.
		/// 
		/// </summary>
		public static T CreateRelaySocketFakeIP<T>( int fakePortIndex = 0 ) where T : SocketManager, new()
		{
			var t = new T();
			var options = Array.Empty<NetKeyValue>();
			t.Socket = Internal.CreateListenSocketP2PFakeIP( 0, options.Length, options );
			t.Initialize();
			SetSocketManager( t.Socket.Id, t );
			return t;
		}

		/// <summary>
		/// Creates a server that will be relayed via Valve's network (hiding the IP and improving ping).
		/// 
		/// To use this you should pass a class that inherits <see cref="ISocketManager"/>. You can use
		/// <see cref="SocketManager"/> to get connections and send messages, but the <see cref="ISocketManager"/> class
		/// will received all the appropriate callbacks.
		/// 
		/// </summary>
		public static SocketManager CreateRelaySocketFakeIP( int fakePortIndex, ISocketManager intrface )
		{
			var options = Array.Empty<NetKeyValue>();
			var socket = Internal.CreateListenSocketP2PFakeIP( 0, options.Length, options );

			var t = new SocketManager
			{
				Socket = socket,
				Interface = intrface
			};

			t.Initialize();

			SetSocketManager( t.Socket.Id, t );
			return t;
		}
	}
}
