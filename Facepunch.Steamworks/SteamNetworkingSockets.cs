using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Steamworks.Data;

namespace Steamworks
{
	public partial class SteamNetworkingSockets : SteamSharedClass<SteamNetworkingSockets>
	{
		internal static ISteamNetworkingSockets Internal => Interface as ISteamNetworkingSockets;

		//
		// Interface resolves to the CLIENT interface whenever one exists, falling back to the
		// server one. That is the right default for almost everything, but the SDR
		// hosted-dedicated-server calls are explicitly documented as gameserver-only:
		//
		//   "This call MUST be made through the SteamGameServerNetworkingSockets() interface."
		//     - isteamnetworkingsockets.h, CreateHostedDedicatedServerListenSocket
		//
		// In a listen-server build (SteamClient.Init AND SteamServer.Init in one process)
		// Internal would hand back the user interface and those calls would quietly do the
		// wrong thing, so they go through here instead.
		//
		internal static ISteamNetworkingSockets InternalServer => InterfaceServer as ISteamNetworkingSockets;

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
			if ( data.Info.listenSocket.Id > 0 )
			{
				var iface = GetSocketManager( data.Info.listenSocket.Id );
				iface?.OnConnectionChanged( data.Conn, data.Info );
			}
			else
			{
				var iface = GetConnectionManager( data.Conn.Id );
				iface?.OnConnectionChanged( data.Info );
			}

			OnConnectionStatusChanged?.Invoke( data.Conn, data.Info );
		}

		public static event Action<Connection, ConnectionInfo> OnConnectionStatusChanged;

		/// <summary>
		/// k_nMaxSteamNetworkingFakeIPPorts - m_unPorts is always this long, and Steam zeroes
		/// the entries it did not assign.
		/// </summary>
		private const int FakeIPPortCount = 8;

		private static unsafe void FakeIPResult( SteamNetworkingFakeIPResult_t data )
		{
			for ( var i = 0; i < FakeIPPortCount; i++ )
			{
				var port = data.Ports[i];

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

		public static IntPtr CreateFakeUDPPort( int index )
		{
			return Internal.CreateFakeUDPPort( index );
		}
		/// <summary>
		/// Return info about the FakeIP and port that we have been assigned, if any.
		/// 
		/// </summary>
		/// <exception cref="ArgumentOutOfRangeException">
		/// <paramref name="fakePortIndex"/> is outside the <see cref="FakeIPPortCount"/> ports
		/// Steam reports. m_unPorts is an inline fixed size buffer with no bounds check of its
		/// own, so this has to be checked here rather than left to the indexer.
		/// </exception>
		public static unsafe Result GetFakeIP( int fakePortIndex, out NetAddress address )
		{
			if ( fakePortIndex < 0 || fakePortIndex >= FakeIPPortCount )
				throw new ArgumentOutOfRangeException( nameof( fakePortIndex ), $"Steam reports {FakeIPPortCount} fake ports, so the index has to be 0 to {FakeIPPortCount - 1}" );

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
		public static T CreateRelaySocketFakeIP<T>( int fakePortIndex = 0, int sendBufferSize = 524288 ) where T : SocketManager, new()
		{
			var t = new T();
			var options = new NetKeyValue[1];
			options[0].DataType = NetConfigType.Int32;
			options[0].Value = NetConfig.SendBufferSize;
			options[0].Int32Value = sendBufferSize;
			t.Socket = Internal.CreateListenSocketP2PFakeIP( fakePortIndex, options.Length, options );
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
			var socket = Internal.CreateListenSocketP2PFakeIP( fakePortIndex, options.Length, options );

			var t = new SocketManager
			{
				Socket = socket,
				Interface = intrface
			};

			t.Initialize();

			SetSocketManager( t.Socket.Id, t );
			return t;
		}

		//
		// A single-element scratch array for GetRemoteFakeIPForConnection. The generated
		// binding takes a NetAddress[], so without this every lookup would allocate an
		// array just to carry one 18-byte struct back out. [ThreadStatic] keeps it safe
		// if a game resolves FakeIPs from more than one thread.
		//
		[ThreadStatic] private static NetAddress[] fakeIpScratch;

		/// <summary>
		/// Get the FakeIP address of the peer on the other end of <paramref name="connection"/>.
		///
		/// <para>
		/// A "FakeIP" is a real-looking IPv4 address that Valve hands out purely as an
		/// <i>identifier</i>. No packet is ever sent to it. It exists so that engine code
		/// which already identifies peers by <c>IPAddress</c> — server browsers, ban lists,
		/// admin tools, logs — can keep working unchanged on top of SDR, where peers are
		/// really addressed by Steam identity.
		/// </para>
		///
		/// <para>
		/// If the peer had a globally allocated FakeIP when the connection was established,
		/// you get that one. If not, Steam mints one from a <i>local</i> address space that is
		/// only meaningful inside this process. Valve says repeat connections to the same
		/// host will "probably" reuse the same local FakeIP, but that the namespace is limited
		/// so it cannot be guaranteed — do not persist a locally allocated FakeIP and expect
		/// it to still mean the same peer tomorrow.
		/// </para>
		///
		/// <para>
		/// The addresses currently come from 169.254.0.0/16 with a port above 1024, but Valve
		/// explicitly warns that this will change. Test with
		/// <see cref="NetAddress.IsFakeIPv4"/> rather than range-checking it yourself.
		/// </para>
		/// </summary>
		/// <param name="connection">The connection whose remote peer you want an address for.</param>
		/// <param name="address">The peer's FakeIP address, or <c>default</c> on failure.</param>
		/// <returns>
		/// <see cref="Result.OK"/> on success;
		/// <see cref="Result.InvalidParam"/> if <paramref name="connection"/> is not a valid handle;
		/// <see cref="Result.IPNotFound"/> if this connection was not made using the FakeIP system at all.
		/// </returns>
		/// <exception cref="ArgumentException"><paramref name="connection"/> is the invalid connection handle.</exception>
		/// <example>
		/// <code>
		/// if ( SteamNetworkingSockets.GetRemoteFakeIPForConnection( conn, out var addr ) == Result.OK )
		///     Log.Info( $"{conn} is {addr.Address}:{addr.Port}" );
		/// </code>
		/// </example>
		public static Result GetRemoteFakeIPForConnection( Connection connection, out NetAddress address )
		{
			if ( connection.Id == 0 )
				throw new ArgumentException( "Invalid Connection", nameof( connection ) );

			var scratch = fakeIpScratch ?? ( fakeIpScratch = new NetAddress[1] );

			//
			// The scratch array outlives the call, so clear it first. Without this, a native
			// call that returned OK without writing would hand back the address from a
			// PREVIOUS lookup on this thread - i.e. silently attribute one peer's FakeIP to
			// another. Cheap insurance against a bug that would be near-impossible to trace.
			//
			scratch[0] = default;

			var result = Internal.GetRemoteFakeIPForConnection( connection, scratch );

			address = result == Result.OK ? scratch[0] : default;
			return result;
		}

		/// <summary>
		/// Create a poll group: a set of connections whose inbound messages can be drained
		/// with a single native call instead of one call per connection.
		///
		/// <para>
		/// This is the primitive that lets a server's receive cost stop scaling with player
		/// count. See <see cref="PollGroup"/> for the full explanation, the ordering
		/// guarantees and a worked example.
		/// </para>
		///
		/// <para>
		/// You own the returned handle. Call <see cref="PollGroup.Destroy"/> when you are
		/// done with it, or it stays alive inside the Steam networking library until the
		/// process exits.
		/// </para>
		/// </summary>
		/// <returns>A new, empty poll group.</returns>
		/// <exception cref="InvalidOperationException">Steam refused to create the group — the networking interface is not initialised.</exception>
		public static PollGroup CreatePollGroup()
		{
			var group = new PollGroup { Id = Internal.CreatePollGroup() };

			if ( !group.IsValid )
				throw new InvalidOperationException( "Steam returned an invalid poll group. Is SteamClient.Init / SteamServer.Init done?" );

			return group;
		}
	}
}
