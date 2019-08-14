using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Steamworks.Data;


namespace Steamworks
{
	internal class ISteamNetworkingSockets : SteamInterface
	{
		public override string InterfaceName => "SteamNetworkingSockets002";
		
		public override void InitInternals()
		{
			_CreateListenSocketIP = Marshal.GetDelegateForFunctionPointer<FCreateListenSocketIP>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 0 ) ) );
			_ConnectByIPAddress = Marshal.GetDelegateForFunctionPointer<FConnectByIPAddress>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 8 ) ) );
			_CreateListenSocketP2P = Marshal.GetDelegateForFunctionPointer<FCreateListenSocketP2P>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 16 ) ) );
			_ConnectP2P = Marshal.GetDelegateForFunctionPointer<FConnectP2P>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 24 ) ) );
			_AcceptConnection = Marshal.GetDelegateForFunctionPointer<FAcceptConnection>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 32 ) ) );
			_CloseConnection = Marshal.GetDelegateForFunctionPointer<FCloseConnection>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 40 ) ) );
			_CloseListenSocket = Marshal.GetDelegateForFunctionPointer<FCloseListenSocket>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 48 ) ) );
			_SetConnectionUserData = Marshal.GetDelegateForFunctionPointer<FSetConnectionUserData>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 56 ) ) );
			_GetConnectionUserData = Marshal.GetDelegateForFunctionPointer<FGetConnectionUserData>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 64 ) ) );
			_SetConnectionName = Marshal.GetDelegateForFunctionPointer<FSetConnectionName>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 72 ) ) );
			_GetConnectionName = Marshal.GetDelegateForFunctionPointer<FGetConnectionName>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 80 ) ) );
			_SendMessageToConnection = Marshal.GetDelegateForFunctionPointer<FSendMessageToConnection>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 88 ) ) );
			_FlushMessagesOnConnection = Marshal.GetDelegateForFunctionPointer<FFlushMessagesOnConnection>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 96 ) ) );
			_ReceiveMessagesOnConnection = Marshal.GetDelegateForFunctionPointer<FReceiveMessagesOnConnection>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 104 ) ) );
			_ReceiveMessagesOnListenSocket = Marshal.GetDelegateForFunctionPointer<FReceiveMessagesOnListenSocket>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 112 ) ) );
			_GetConnectionInfo = Marshal.GetDelegateForFunctionPointer<FGetConnectionInfo>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 120 ) ) );
			_GetQuickConnectionStatus = Marshal.GetDelegateForFunctionPointer<FGetQuickConnectionStatus>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 128 ) ) );
			_GetDetailedConnectionStatus = Marshal.GetDelegateForFunctionPointer<FGetDetailedConnectionStatus>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 136 ) ) );
			_GetListenSocketAddress = Marshal.GetDelegateForFunctionPointer<FGetListenSocketAddress>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 144 ) ) );
			_CreateSocketPair = Marshal.GetDelegateForFunctionPointer<FCreateSocketPair>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 152 ) ) );
			_GetIdentity = Marshal.GetDelegateForFunctionPointer<FGetIdentity>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 160 ) ) );
			_ReceivedRelayAuthTicket = Marshal.GetDelegateForFunctionPointer<FReceivedRelayAuthTicket>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 168 ) ) );
			_FindRelayAuthTicketForServer = Marshal.GetDelegateForFunctionPointer<FFindRelayAuthTicketForServer>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 176 ) ) );
			_ConnectToHostedDedicatedServer = Marshal.GetDelegateForFunctionPointer<FConnectToHostedDedicatedServer>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 184 ) ) );
			_GetHostedDedicatedServerPort = Marshal.GetDelegateForFunctionPointer<FGetHostedDedicatedServerPort>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 192 ) ) );
			_GetHostedDedicatedServerPOPID = Marshal.GetDelegateForFunctionPointer<FGetHostedDedicatedServerPOPID>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 200 ) ) );
			_GetHostedDedicatedServerAddress = Marshal.GetDelegateForFunctionPointer<FGetHostedDedicatedServerAddress>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 208 ) ) );
			_CreateHostedDedicatedServerListenSocket = Marshal.GetDelegateForFunctionPointer<FCreateHostedDedicatedServerListenSocket>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 216 ) ) );
			_RunCallbacks = Marshal.GetDelegateForFunctionPointer<FRunCallbacks>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 224 ) ) );
		}
		internal override void Shutdown()
		{
			base.Shutdown();
			
			_CreateListenSocketIP = null;
			_ConnectByIPAddress = null;
			_CreateListenSocketP2P = null;
			_ConnectP2P = null;
			_AcceptConnection = null;
			_CloseConnection = null;
			_CloseListenSocket = null;
			_SetConnectionUserData = null;
			_GetConnectionUserData = null;
			_SetConnectionName = null;
			_GetConnectionName = null;
			_SendMessageToConnection = null;
			_FlushMessagesOnConnection = null;
			_ReceiveMessagesOnConnection = null;
			_ReceiveMessagesOnListenSocket = null;
			_GetConnectionInfo = null;
			_GetQuickConnectionStatus = null;
			_GetDetailedConnectionStatus = null;
			_GetListenSocketAddress = null;
			_CreateSocketPair = null;
			_GetIdentity = null;
			_ReceivedRelayAuthTicket = null;
			_FindRelayAuthTicketForServer = null;
			_ConnectToHostedDedicatedServer = null;
			_GetHostedDedicatedServerPort = null;
			_GetHostedDedicatedServerPOPID = null;
			_GetHostedDedicatedServerAddress = null;
			_CreateHostedDedicatedServerListenSocket = null;
			_RunCallbacks = null;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate Socket FCreateListenSocketIP( IntPtr self, ref NetAddress localAddress );
		private FCreateListenSocketIP _CreateListenSocketIP;
		
		#endregion
		internal Socket CreateListenSocketIP( ref NetAddress localAddress )
		{
			var returnValue = _CreateListenSocketIP( Self, ref localAddress );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate Connection FConnectByIPAddress( IntPtr self, ref NetAddress address );
		private FConnectByIPAddress _ConnectByIPAddress;
		
		#endregion
		internal Connection ConnectByIPAddress( ref NetAddress address )
		{
			var returnValue = _ConnectByIPAddress( Self, ref address );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate Socket FCreateListenSocketP2P( IntPtr self, int nVirtualPort );
		private FCreateListenSocketP2P _CreateListenSocketP2P;
		
		#endregion
		internal Socket CreateListenSocketP2P( int nVirtualPort )
		{
			var returnValue = _CreateListenSocketP2P( Self, nVirtualPort );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate Connection FConnectP2P( IntPtr self, ref NetIdentity identityRemote, int nVirtualPort );
		private FConnectP2P _ConnectP2P;
		
		#endregion
		internal Connection ConnectP2P( ref NetIdentity identityRemote, int nVirtualPort )
		{
			var returnValue = _ConnectP2P( Self, ref identityRemote, nVirtualPort );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate Result FAcceptConnection( IntPtr self, Connection hConn );
		private FAcceptConnection _AcceptConnection;
		
		#endregion
		internal Result AcceptConnection( Connection hConn )
		{
			var returnValue = _AcceptConnection( Self, hConn );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FCloseConnection( IntPtr self, Connection hPeer, int nReason, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pszDebug, [MarshalAs( UnmanagedType.U1 )] bool bEnableLinger );
		private FCloseConnection _CloseConnection;
		
		#endregion
		internal bool CloseConnection( Connection hPeer, int nReason, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pszDebug, [MarshalAs( UnmanagedType.U1 )] bool bEnableLinger )
		{
			var returnValue = _CloseConnection( Self, hPeer, nReason, pszDebug, bEnableLinger );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FCloseListenSocket( IntPtr self, Socket hSocket );
		private FCloseListenSocket _CloseListenSocket;
		
		#endregion
		internal bool CloseListenSocket( Socket hSocket )
		{
			var returnValue = _CloseListenSocket( Self, hSocket );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FSetConnectionUserData( IntPtr self, Connection hPeer, long nUserData );
		private FSetConnectionUserData _SetConnectionUserData;
		
		#endregion
		internal bool SetConnectionUserData( Connection hPeer, long nUserData )
		{
			var returnValue = _SetConnectionUserData( Self, hPeer, nUserData );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate long FGetConnectionUserData( IntPtr self, Connection hPeer );
		private FGetConnectionUserData _GetConnectionUserData;
		
		#endregion
		internal long GetConnectionUserData( Connection hPeer )
		{
			var returnValue = _GetConnectionUserData( Self, hPeer );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate void FSetConnectionName( IntPtr self, Connection hPeer, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pszName );
		private FSetConnectionName _SetConnectionName;
		
		#endregion
		internal void SetConnectionName( Connection hPeer, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pszName )
		{
			_SetConnectionName( Self, hPeer, pszName );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FGetConnectionName( IntPtr self, Connection hPeer, IntPtr pszName, int nMaxLen );
		private FGetConnectionName _GetConnectionName;
		
		#endregion
		internal bool GetConnectionName( Connection hPeer, out string pszName )
		{
			IntPtr mempszName = Helpers.TakeMemory();
			var returnValue = _GetConnectionName( Self, hPeer, mempszName, (1024 * 32) );
			pszName = Helpers.MemoryToString( mempszName );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate Result FSendMessageToConnection( IntPtr self, Connection hConn, IntPtr pData, uint cbData, int nSendFlags );
		private FSendMessageToConnection _SendMessageToConnection;
		
		#endregion
		internal Result SendMessageToConnection( Connection hConn, IntPtr pData, uint cbData, int nSendFlags )
		{
			var returnValue = _SendMessageToConnection( Self, hConn, pData, cbData, nSendFlags );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate Result FFlushMessagesOnConnection( IntPtr self, Connection hConn );
		private FFlushMessagesOnConnection _FlushMessagesOnConnection;
		
		#endregion
		internal Result FlushMessagesOnConnection( Connection hConn )
		{
			var returnValue = _FlushMessagesOnConnection( Self, hConn );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate int FReceiveMessagesOnConnection( IntPtr self, Connection hConn, IntPtr ppOutMessages, int nMaxMessages );
		private FReceiveMessagesOnConnection _ReceiveMessagesOnConnection;
		
		#endregion
		internal int ReceiveMessagesOnConnection( Connection hConn, IntPtr ppOutMessages, int nMaxMessages )
		{
			var returnValue = _ReceiveMessagesOnConnection( Self, hConn, ppOutMessages, nMaxMessages );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate int FReceiveMessagesOnListenSocket( IntPtr self, Socket hSocket, IntPtr ppOutMessages, int nMaxMessages );
		private FReceiveMessagesOnListenSocket _ReceiveMessagesOnListenSocket;
		
		#endregion
		internal int ReceiveMessagesOnListenSocket( Socket hSocket, IntPtr ppOutMessages, int nMaxMessages )
		{
			var returnValue = _ReceiveMessagesOnListenSocket( Self, hSocket, ppOutMessages, nMaxMessages );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FGetConnectionInfo( IntPtr self, Connection hConn, ref ConnectionInfo pInfo );
		private FGetConnectionInfo _GetConnectionInfo;
		
		#endregion
		internal bool GetConnectionInfo( Connection hConn, ref ConnectionInfo pInfo )
		{
			var returnValue = _GetConnectionInfo( Self, hConn, ref pInfo );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FGetQuickConnectionStatus( IntPtr self, Connection hConn, ref SteamNetworkingQuickConnectionStatus pStats );
		private FGetQuickConnectionStatus _GetQuickConnectionStatus;
		
		#endregion
		internal bool GetQuickConnectionStatus( Connection hConn, ref SteamNetworkingQuickConnectionStatus pStats )
		{
			var returnValue = _GetQuickConnectionStatus( Self, hConn, ref pStats );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate int FGetDetailedConnectionStatus( IntPtr self, Connection hConn, IntPtr pszBuf, int cbBuf );
		private FGetDetailedConnectionStatus _GetDetailedConnectionStatus;
		
		#endregion
		internal int GetDetailedConnectionStatus( Connection hConn, out string pszBuf )
		{
			IntPtr mempszBuf = Helpers.TakeMemory();
			var returnValue = _GetDetailedConnectionStatus( Self, hConn, mempszBuf, (1024 * 32) );
			pszBuf = Helpers.MemoryToString( mempszBuf );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FGetListenSocketAddress( IntPtr self, Socket hSocket, ref NetAddress address );
		private FGetListenSocketAddress _GetListenSocketAddress;
		
		#endregion
		internal bool GetListenSocketAddress( Socket hSocket, ref NetAddress address )
		{
			var returnValue = _GetListenSocketAddress( Self, hSocket, ref address );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FCreateSocketPair( IntPtr self, [In,Out] Connection[]  pOutConnection1, [In,Out] Connection[]  pOutConnection2, [MarshalAs( UnmanagedType.U1 )] bool bUseNetworkLoopback, ref NetIdentity pIdentity1, ref NetIdentity pIdentity2 );
		private FCreateSocketPair _CreateSocketPair;
		
		#endregion
		internal bool CreateSocketPair( [In,Out] Connection[]  pOutConnection1, [In,Out] Connection[]  pOutConnection2, [MarshalAs( UnmanagedType.U1 )] bool bUseNetworkLoopback, ref NetIdentity pIdentity1, ref NetIdentity pIdentity2 )
		{
			var returnValue = _CreateSocketPair( Self, pOutConnection1, pOutConnection2, bUseNetworkLoopback, ref pIdentity1, ref pIdentity2 );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FGetIdentity( IntPtr self, ref NetIdentity pIdentity );
		private FGetIdentity _GetIdentity;
		
		#endregion
		internal bool GetIdentity( ref NetIdentity pIdentity )
		{
			var returnValue = _GetIdentity( Self, ref pIdentity );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FReceivedRelayAuthTicket( IntPtr self, IntPtr pvTicket, int cbTicket, [In,Out] SteamDatagramRelayAuthTicket[]  pOutParsedTicket );
		private FReceivedRelayAuthTicket _ReceivedRelayAuthTicket;
		
		#endregion
		internal bool ReceivedRelayAuthTicket( IntPtr pvTicket, int cbTicket, [In,Out] SteamDatagramRelayAuthTicket[]  pOutParsedTicket )
		{
			var returnValue = _ReceivedRelayAuthTicket( Self, pvTicket, cbTicket, pOutParsedTicket );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate int FFindRelayAuthTicketForServer( IntPtr self, ref NetIdentity identityGameServer, int nVirtualPort, [In,Out] SteamDatagramRelayAuthTicket[]  pOutParsedTicket );
		private FFindRelayAuthTicketForServer _FindRelayAuthTicketForServer;
		
		#endregion
		internal int FindRelayAuthTicketForServer( ref NetIdentity identityGameServer, int nVirtualPort, [In,Out] SteamDatagramRelayAuthTicket[]  pOutParsedTicket )
		{
			var returnValue = _FindRelayAuthTicketForServer( Self, ref identityGameServer, nVirtualPort, pOutParsedTicket );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate Connection FConnectToHostedDedicatedServer( IntPtr self, ref NetIdentity identityTarget, int nVirtualPort );
		private FConnectToHostedDedicatedServer _ConnectToHostedDedicatedServer;
		
		#endregion
		internal Connection ConnectToHostedDedicatedServer( ref NetIdentity identityTarget, int nVirtualPort )
		{
			var returnValue = _ConnectToHostedDedicatedServer( Self, ref identityTarget, nVirtualPort );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate ushort FGetHostedDedicatedServerPort( IntPtr self );
		private FGetHostedDedicatedServerPort _GetHostedDedicatedServerPort;
		
		#endregion
		internal ushort GetHostedDedicatedServerPort()
		{
			var returnValue = _GetHostedDedicatedServerPort( Self );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate SteamNetworkingPOPID FGetHostedDedicatedServerPOPID( IntPtr self );
		private FGetHostedDedicatedServerPOPID _GetHostedDedicatedServerPOPID;
		
		#endregion
		internal SteamNetworkingPOPID GetHostedDedicatedServerPOPID()
		{
			var returnValue = _GetHostedDedicatedServerPOPID( Self );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FGetHostedDedicatedServerAddress( IntPtr self, ref SteamDatagramHostedAddress pRouting );
		private FGetHostedDedicatedServerAddress _GetHostedDedicatedServerAddress;
		
		#endregion
		internal bool GetHostedDedicatedServerAddress( ref SteamDatagramHostedAddress pRouting )
		{
			var returnValue = _GetHostedDedicatedServerAddress( Self, ref pRouting );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate Socket FCreateHostedDedicatedServerListenSocket( IntPtr self, int nVirtualPort );
		private FCreateHostedDedicatedServerListenSocket _CreateHostedDedicatedServerListenSocket;
		
		#endregion
		internal Socket CreateHostedDedicatedServerListenSocket( int nVirtualPort )
		{
			var returnValue = _CreateHostedDedicatedServerListenSocket( Self, nVirtualPort );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate void FRunCallbacks( IntPtr self, IntPtr pCallbacks );
		private FRunCallbacks _RunCallbacks;
		
		#endregion
		internal void RunCallbacks( IntPtr pCallbacks )
		{
			_RunCallbacks( Self, pCallbacks );
		}
		
	}
}
