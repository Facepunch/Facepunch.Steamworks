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
		}
		internal override void Shutdown()
		{
			base.Shutdown();
			
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
