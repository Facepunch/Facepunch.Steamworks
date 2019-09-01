using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Steamworks.Data;


namespace Steamworks
{
	internal class ISteamNetworking : SteamInterface
	{
		public override string InterfaceName => "SteamNetworking005";
		
		public override void InitInternals()
		{
			_SendP2PPacket = Marshal.GetDelegateForFunctionPointer<FSendP2PPacket>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 0 ) ) );
			_IsP2PPacketAvailable = Marshal.GetDelegateForFunctionPointer<FIsP2PPacketAvailable>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 8 ) ) );
			_ReadP2PPacket = Marshal.GetDelegateForFunctionPointer<FReadP2PPacket>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 16 ) ) );
			_AcceptP2PSessionWithUser = Marshal.GetDelegateForFunctionPointer<FAcceptP2PSessionWithUser>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 24 ) ) );
			_CloseP2PSessionWithUser = Marshal.GetDelegateForFunctionPointer<FCloseP2PSessionWithUser>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 32 ) ) );
			_CloseP2PChannelWithUser = Marshal.GetDelegateForFunctionPointer<FCloseP2PChannelWithUser>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 40 ) ) );
			_GetP2PSessionState = Marshal.GetDelegateForFunctionPointer<FGetP2PSessionState>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 48 ) ) );
			_AllowP2PPacketRelay = Marshal.GetDelegateForFunctionPointer<FAllowP2PPacketRelay>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 56 ) ) );
			_CreateListenSocket = Marshal.GetDelegateForFunctionPointer<FCreateListenSocket>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 64 ) ) );
			_CreateP2PConnectionSocket = Marshal.GetDelegateForFunctionPointer<FCreateP2PConnectionSocket>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 72 ) ) );
			_CreateConnectionSocket = Marshal.GetDelegateForFunctionPointer<FCreateConnectionSocket>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 80 ) ) );
			_DestroySocket = Marshal.GetDelegateForFunctionPointer<FDestroySocket>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 88 ) ) );
			_DestroyListenSocket = Marshal.GetDelegateForFunctionPointer<FDestroyListenSocket>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 96 ) ) );
			_SendDataOnSocket = Marshal.GetDelegateForFunctionPointer<FSendDataOnSocket>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 104 ) ) );
			_IsDataAvailableOnSocket = Marshal.GetDelegateForFunctionPointer<FIsDataAvailableOnSocket>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 112 ) ) );
			_RetrieveDataFromSocket = Marshal.GetDelegateForFunctionPointer<FRetrieveDataFromSocket>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 120 ) ) );
			_IsDataAvailable = Marshal.GetDelegateForFunctionPointer<FIsDataAvailable>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 128 ) ) );
			_RetrieveData = Marshal.GetDelegateForFunctionPointer<FRetrieveData>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 136 ) ) );
			_GetSocketInfo = Marshal.GetDelegateForFunctionPointer<FGetSocketInfo>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 144 ) ) );
			_GetListenSocketInfo = Marshal.GetDelegateForFunctionPointer<FGetListenSocketInfo>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 152 ) ) );
			_GetSocketConnectionType = Marshal.GetDelegateForFunctionPointer<FGetSocketConnectionType>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 160 ) ) );
			_GetMaxPacketSize = Marshal.GetDelegateForFunctionPointer<FGetMaxPacketSize>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 168 ) ) );
		}
		internal override void Shutdown()
		{
			base.Shutdown();
			
			_SendP2PPacket = null;
			_IsP2PPacketAvailable = null;
			_ReadP2PPacket = null;
			_AcceptP2PSessionWithUser = null;
			_CloseP2PSessionWithUser = null;
			_CloseP2PChannelWithUser = null;
			_GetP2PSessionState = null;
			_AllowP2PPacketRelay = null;
			_CreateListenSocket = null;
			_CreateP2PConnectionSocket = null;
			_CreateConnectionSocket = null;
			_DestroySocket = null;
			_DestroyListenSocket = null;
			_SendDataOnSocket = null;
			_IsDataAvailableOnSocket = null;
			_RetrieveDataFromSocket = null;
			_IsDataAvailable = null;
			_RetrieveData = null;
			_GetSocketInfo = null;
			_GetListenSocketInfo = null;
			_GetSocketConnectionType = null;
			_GetMaxPacketSize = null;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FSendP2PPacket( IntPtr self, SteamId steamIDRemote, IntPtr pubData, uint cubData, P2PSend eP2PSendType, int nChannel );
		private FSendP2PPacket _SendP2PPacket;
		
		#endregion
		internal bool SendP2PPacket( SteamId steamIDRemote, IntPtr pubData, uint cubData, P2PSend eP2PSendType, int nChannel )
		{
			var returnValue = _SendP2PPacket( Self, steamIDRemote, pubData, cubData, eP2PSendType, nChannel );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FIsP2PPacketAvailable( IntPtr self, ref uint pcubMsgSize, int nChannel );
		private FIsP2PPacketAvailable _IsP2PPacketAvailable;
		
		#endregion
		internal bool IsP2PPacketAvailable( ref uint pcubMsgSize, int nChannel )
		{
			var returnValue = _IsP2PPacketAvailable( Self, ref pcubMsgSize, nChannel );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FReadP2PPacket( IntPtr self, IntPtr pubDest, uint cubDest, ref uint pcubMsgSize, ref SteamId psteamIDRemote, int nChannel );
		private FReadP2PPacket _ReadP2PPacket;
		
		#endregion
		internal bool ReadP2PPacket( IntPtr pubDest, uint cubDest, ref uint pcubMsgSize, ref SteamId psteamIDRemote, int nChannel )
		{
			var returnValue = _ReadP2PPacket( Self, pubDest, cubDest, ref pcubMsgSize, ref psteamIDRemote, nChannel );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FAcceptP2PSessionWithUser( IntPtr self, SteamId steamIDRemote );
		private FAcceptP2PSessionWithUser _AcceptP2PSessionWithUser;
		
		#endregion
		internal bool AcceptP2PSessionWithUser( SteamId steamIDRemote )
		{
			var returnValue = _AcceptP2PSessionWithUser( Self, steamIDRemote );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FCloseP2PSessionWithUser( IntPtr self, SteamId steamIDRemote );
		private FCloseP2PSessionWithUser _CloseP2PSessionWithUser;
		
		#endregion
		internal bool CloseP2PSessionWithUser( SteamId steamIDRemote )
		{
			var returnValue = _CloseP2PSessionWithUser( Self, steamIDRemote );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FCloseP2PChannelWithUser( IntPtr self, SteamId steamIDRemote, int nChannel );
		private FCloseP2PChannelWithUser _CloseP2PChannelWithUser;
		
		#endregion
		internal bool CloseP2PChannelWithUser( SteamId steamIDRemote, int nChannel )
		{
			var returnValue = _CloseP2PChannelWithUser( Self, steamIDRemote, nChannel );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FGetP2PSessionState( IntPtr self, SteamId steamIDRemote, ref P2PSessionState_t pConnectionState );
		private FGetP2PSessionState _GetP2PSessionState;
		
		#endregion
		internal bool GetP2PSessionState( SteamId steamIDRemote, ref P2PSessionState_t pConnectionState )
		{
			var returnValue = _GetP2PSessionState( Self, steamIDRemote, ref pConnectionState );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FAllowP2PPacketRelay( IntPtr self, [MarshalAs( UnmanagedType.U1 )] bool bAllow );
		private FAllowP2PPacketRelay _AllowP2PPacketRelay;
		
		#endregion
		internal bool AllowP2PPacketRelay( [MarshalAs( UnmanagedType.U1 )] bool bAllow )
		{
			var returnValue = _AllowP2PPacketRelay( Self, bAllow );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate SNetListenSocket_t FCreateListenSocket( IntPtr self, int nVirtualP2PPort, uint nIP, ushort nPort, [MarshalAs( UnmanagedType.U1 )] bool bAllowUseOfPacketRelay );
		private FCreateListenSocket _CreateListenSocket;
		
		#endregion
		internal SNetListenSocket_t CreateListenSocket( int nVirtualP2PPort, uint nIP, ushort nPort, [MarshalAs( UnmanagedType.U1 )] bool bAllowUseOfPacketRelay )
		{
			var returnValue = _CreateListenSocket( Self, nVirtualP2PPort, nIP, nPort, bAllowUseOfPacketRelay );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate SNetSocket_t FCreateP2PConnectionSocket( IntPtr self, SteamId steamIDTarget, int nVirtualPort, int nTimeoutSec, [MarshalAs( UnmanagedType.U1 )] bool bAllowUseOfPacketRelay );
		private FCreateP2PConnectionSocket _CreateP2PConnectionSocket;
		
		#endregion
		internal SNetSocket_t CreateP2PConnectionSocket( SteamId steamIDTarget, int nVirtualPort, int nTimeoutSec, [MarshalAs( UnmanagedType.U1 )] bool bAllowUseOfPacketRelay )
		{
			var returnValue = _CreateP2PConnectionSocket( Self, steamIDTarget, nVirtualPort, nTimeoutSec, bAllowUseOfPacketRelay );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate SNetSocket_t FCreateConnectionSocket( IntPtr self, uint nIP, ushort nPort, int nTimeoutSec );
		private FCreateConnectionSocket _CreateConnectionSocket;
		
		#endregion
		internal SNetSocket_t CreateConnectionSocket( uint nIP, ushort nPort, int nTimeoutSec )
		{
			var returnValue = _CreateConnectionSocket( Self, nIP, nPort, nTimeoutSec );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FDestroySocket( IntPtr self, SNetSocket_t hSocket, [MarshalAs( UnmanagedType.U1 )] bool bNotifyRemoteEnd );
		private FDestroySocket _DestroySocket;
		
		#endregion
		internal bool DestroySocket( SNetSocket_t hSocket, [MarshalAs( UnmanagedType.U1 )] bool bNotifyRemoteEnd )
		{
			var returnValue = _DestroySocket( Self, hSocket, bNotifyRemoteEnd );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FDestroyListenSocket( IntPtr self, SNetListenSocket_t hSocket, [MarshalAs( UnmanagedType.U1 )] bool bNotifyRemoteEnd );
		private FDestroyListenSocket _DestroyListenSocket;
		
		#endregion
		internal bool DestroyListenSocket( SNetListenSocket_t hSocket, [MarshalAs( UnmanagedType.U1 )] bool bNotifyRemoteEnd )
		{
			var returnValue = _DestroyListenSocket( Self, hSocket, bNotifyRemoteEnd );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FSendDataOnSocket( IntPtr self, SNetSocket_t hSocket, [In,Out] IntPtr[]  pubData, uint cubData, [MarshalAs( UnmanagedType.U1 )] bool bReliable );
		private FSendDataOnSocket _SendDataOnSocket;
		
		#endregion
		internal bool SendDataOnSocket( SNetSocket_t hSocket, [In,Out] IntPtr[]  pubData, uint cubData, [MarshalAs( UnmanagedType.U1 )] bool bReliable )
		{
			var returnValue = _SendDataOnSocket( Self, hSocket, pubData, cubData, bReliable );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FIsDataAvailableOnSocket( IntPtr self, SNetSocket_t hSocket, ref uint pcubMsgSize );
		private FIsDataAvailableOnSocket _IsDataAvailableOnSocket;
		
		#endregion
		internal bool IsDataAvailableOnSocket( SNetSocket_t hSocket, ref uint pcubMsgSize )
		{
			var returnValue = _IsDataAvailableOnSocket( Self, hSocket, ref pcubMsgSize );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FRetrieveDataFromSocket( IntPtr self, SNetSocket_t hSocket, [In,Out] IntPtr[]  pubDest, uint cubDest, ref uint pcubMsgSize );
		private FRetrieveDataFromSocket _RetrieveDataFromSocket;
		
		#endregion
		internal bool RetrieveDataFromSocket( SNetSocket_t hSocket, [In,Out] IntPtr[]  pubDest, uint cubDest, ref uint pcubMsgSize )
		{
			var returnValue = _RetrieveDataFromSocket( Self, hSocket, pubDest, cubDest, ref pcubMsgSize );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FIsDataAvailable( IntPtr self, SNetListenSocket_t hListenSocket, ref uint pcubMsgSize, ref SNetSocket_t phSocket );
		private FIsDataAvailable _IsDataAvailable;
		
		#endregion
		internal bool IsDataAvailable( SNetListenSocket_t hListenSocket, ref uint pcubMsgSize, ref SNetSocket_t phSocket )
		{
			var returnValue = _IsDataAvailable( Self, hListenSocket, ref pcubMsgSize, ref phSocket );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FRetrieveData( IntPtr self, SNetListenSocket_t hListenSocket, [In,Out] IntPtr[]  pubDest, uint cubDest, ref uint pcubMsgSize, ref SNetSocket_t phSocket );
		private FRetrieveData _RetrieveData;
		
		#endregion
		internal bool RetrieveData( SNetListenSocket_t hListenSocket, [In,Out] IntPtr[]  pubDest, uint cubDest, ref uint pcubMsgSize, ref SNetSocket_t phSocket )
		{
			var returnValue = _RetrieveData( Self, hListenSocket, pubDest, cubDest, ref pcubMsgSize, ref phSocket );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FGetSocketInfo( IntPtr self, SNetSocket_t hSocket, ref SteamId pSteamIDRemote, ref int peSocketStatus, ref uint punIPRemote, ref ushort punPortRemote );
		private FGetSocketInfo _GetSocketInfo;
		
		#endregion
		internal bool GetSocketInfo( SNetSocket_t hSocket, ref SteamId pSteamIDRemote, ref int peSocketStatus, ref uint punIPRemote, ref ushort punPortRemote )
		{
			var returnValue = _GetSocketInfo( Self, hSocket, ref pSteamIDRemote, ref peSocketStatus, ref punIPRemote, ref punPortRemote );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FGetListenSocketInfo( IntPtr self, SNetListenSocket_t hListenSocket, ref uint pnIP, ref ushort pnPort );
		private FGetListenSocketInfo _GetListenSocketInfo;
		
		#endregion
		internal bool GetListenSocketInfo( SNetListenSocket_t hListenSocket, ref uint pnIP, ref ushort pnPort )
		{
			var returnValue = _GetListenSocketInfo( Self, hListenSocket, ref pnIP, ref pnPort );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate SNetSocketConnectionType FGetSocketConnectionType( IntPtr self, SNetSocket_t hSocket );
		private FGetSocketConnectionType _GetSocketConnectionType;
		
		#endregion
		internal SNetSocketConnectionType GetSocketConnectionType( SNetSocket_t hSocket )
		{
			var returnValue = _GetSocketConnectionType( Self, hSocket );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate int FGetMaxPacketSize( IntPtr self, SNetSocket_t hSocket );
		private FGetMaxPacketSize _GetMaxPacketSize;
		
		#endregion
		internal int GetMaxPacketSize( SNetSocket_t hSocket )
		{
			var returnValue = _GetMaxPacketSize( Self, hSocket );
			return returnValue;
		}
		
	}
}
