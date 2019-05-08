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
			_SendP2PPacket = Marshal.GetDelegateForFunctionPointer<FSendP2PPacket>( Marshal.ReadIntPtr( VTable, 0) );
			_IsP2PPacketAvailable = Marshal.GetDelegateForFunctionPointer<FIsP2PPacketAvailable>( Marshal.ReadIntPtr( VTable, 8) );
			_ReadP2PPacket = Marshal.GetDelegateForFunctionPointer<FReadP2PPacket>( Marshal.ReadIntPtr( VTable, 16) );
			_AcceptP2PSessionWithUser = Marshal.GetDelegateForFunctionPointer<FAcceptP2PSessionWithUser>( Marshal.ReadIntPtr( VTable, 24) );
			_CloseP2PSessionWithUser = Marshal.GetDelegateForFunctionPointer<FCloseP2PSessionWithUser>( Marshal.ReadIntPtr( VTable, 32) );
			_CloseP2PChannelWithUser = Marshal.GetDelegateForFunctionPointer<FCloseP2PChannelWithUser>( Marshal.ReadIntPtr( VTable, 40) );
			_GetP2PSessionState = Marshal.GetDelegateForFunctionPointer<FGetP2PSessionState>( Marshal.ReadIntPtr( VTable, 48) );
			_GetP2PSessionState_Windows = Marshal.GetDelegateForFunctionPointer<FGetP2PSessionState_Windows>( Marshal.ReadIntPtr( VTable, 48) );
			_AllowP2PPacketRelay = Marshal.GetDelegateForFunctionPointer<FAllowP2PPacketRelay>( Marshal.ReadIntPtr( VTable, 56) );
			_CreateListenSocket = Marshal.GetDelegateForFunctionPointer<FCreateListenSocket>( Marshal.ReadIntPtr( VTable, 64) );
			_CreateP2PConnectionSocket = Marshal.GetDelegateForFunctionPointer<FCreateP2PConnectionSocket>( Marshal.ReadIntPtr( VTable, 72) );
			_CreateConnectionSocket = Marshal.GetDelegateForFunctionPointer<FCreateConnectionSocket>( Marshal.ReadIntPtr( VTable, 80) );
			_DestroySocket = Marshal.GetDelegateForFunctionPointer<FDestroySocket>( Marshal.ReadIntPtr( VTable, 88) );
			_DestroyListenSocket = Marshal.GetDelegateForFunctionPointer<FDestroyListenSocket>( Marshal.ReadIntPtr( VTable, 96) );
			_SendDataOnSocket = Marshal.GetDelegateForFunctionPointer<FSendDataOnSocket>( Marshal.ReadIntPtr( VTable, 104) );
			_IsDataAvailableOnSocket = Marshal.GetDelegateForFunctionPointer<FIsDataAvailableOnSocket>( Marshal.ReadIntPtr( VTable, 112) );
			_RetrieveDataFromSocket = Marshal.GetDelegateForFunctionPointer<FRetrieveDataFromSocket>( Marshal.ReadIntPtr( VTable, 120) );
			_IsDataAvailable = Marshal.GetDelegateForFunctionPointer<FIsDataAvailable>( Marshal.ReadIntPtr( VTable, 128) );
			_RetrieveData = Marshal.GetDelegateForFunctionPointer<FRetrieveData>( Marshal.ReadIntPtr( VTable, 136) );
			_GetSocketInfo = Marshal.GetDelegateForFunctionPointer<FGetSocketInfo>( Marshal.ReadIntPtr( VTable, 144) );
			_GetListenSocketInfo = Marshal.GetDelegateForFunctionPointer<FGetListenSocketInfo>( Marshal.ReadIntPtr( VTable, 152) );
			_GetSocketConnectionType = Marshal.GetDelegateForFunctionPointer<FGetSocketConnectionType>( Marshal.ReadIntPtr( VTable, 160) );
			_GetMaxPacketSize = Marshal.GetDelegateForFunctionPointer<FGetMaxPacketSize>( Marshal.ReadIntPtr( VTable, 168) );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FSendP2PPacket( IntPtr self, SteamId steamIDRemote, IntPtr pubData, uint cubData, P2PSend eP2PSendType, int nChannel );
		private FSendP2PPacket _SendP2PPacket;
		
		#endregion
		internal bool SendP2PPacket( SteamId steamIDRemote, IntPtr pubData, uint cubData, P2PSend eP2PSendType, int nChannel )
		{
			return _SendP2PPacket( Self, steamIDRemote, pubData, cubData, eP2PSendType, nChannel );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FIsP2PPacketAvailable( IntPtr self, ref uint pcubMsgSize, int nChannel );
		private FIsP2PPacketAvailable _IsP2PPacketAvailable;
		
		#endregion
		internal bool IsP2PPacketAvailable( ref uint pcubMsgSize, int nChannel )
		{
			return _IsP2PPacketAvailable( Self, ref pcubMsgSize, nChannel );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FReadP2PPacket( IntPtr self, IntPtr pubDest, uint cubDest, ref uint pcubMsgSize, ref SteamId psteamIDRemote, int nChannel );
		private FReadP2PPacket _ReadP2PPacket;
		
		#endregion
		internal bool ReadP2PPacket( IntPtr pubDest, uint cubDest, ref uint pcubMsgSize, ref SteamId psteamIDRemote, int nChannel )
		{
			return _ReadP2PPacket( Self, pubDest, cubDest, ref pcubMsgSize, ref psteamIDRemote, nChannel );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FAcceptP2PSessionWithUser( IntPtr self, SteamId steamIDRemote );
		private FAcceptP2PSessionWithUser _AcceptP2PSessionWithUser;
		
		#endregion
		internal bool AcceptP2PSessionWithUser( SteamId steamIDRemote )
		{
			return _AcceptP2PSessionWithUser( Self, steamIDRemote );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FCloseP2PSessionWithUser( IntPtr self, SteamId steamIDRemote );
		private FCloseP2PSessionWithUser _CloseP2PSessionWithUser;
		
		#endregion
		internal bool CloseP2PSessionWithUser( SteamId steamIDRemote )
		{
			return _CloseP2PSessionWithUser( Self, steamIDRemote );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FCloseP2PChannelWithUser( IntPtr self, SteamId steamIDRemote, int nChannel );
		private FCloseP2PChannelWithUser _CloseP2PChannelWithUser;
		
		#endregion
		internal bool CloseP2PChannelWithUser( SteamId steamIDRemote, int nChannel )
		{
			return _CloseP2PChannelWithUser( Self, steamIDRemote, nChannel );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FGetP2PSessionState( IntPtr self, SteamId steamIDRemote, ref P2PSessionState_t pConnectionState );
		private FGetP2PSessionState _GetP2PSessionState;
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FGetP2PSessionState_Windows( IntPtr self, SteamId steamIDRemote, ref P2PSessionState_t.Pack8 pConnectionState );
		private FGetP2PSessionState_Windows _GetP2PSessionState_Windows;
		
		#endregion
		internal bool GetP2PSessionState( SteamId steamIDRemote, ref P2PSessionState_t pConnectionState )
		{
			if ( Config.Os == OsType.Windows )
			{
				P2PSessionState_t.Pack8 pConnectionState_windows = pConnectionState;
				var retVal = _GetP2PSessionState_Windows( Self, steamIDRemote, ref pConnectionState_windows );
				pConnectionState = pConnectionState_windows;
				return retVal;
			}
			
			return _GetP2PSessionState( Self, steamIDRemote, ref pConnectionState );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FAllowP2PPacketRelay( IntPtr self, [MarshalAs( UnmanagedType.U1 )] bool bAllow );
		private FAllowP2PPacketRelay _AllowP2PPacketRelay;
		
		#endregion
		internal bool AllowP2PPacketRelay( [MarshalAs( UnmanagedType.U1 )] bool bAllow )
		{
			return _AllowP2PPacketRelay( Self, bAllow );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate SNetListenSocket_t FCreateListenSocket( IntPtr self, int nVirtualP2PPort, uint nIP, ushort nPort, [MarshalAs( UnmanagedType.U1 )] bool bAllowUseOfPacketRelay );
		private FCreateListenSocket _CreateListenSocket;
		
		#endregion
		internal SNetListenSocket_t CreateListenSocket( int nVirtualP2PPort, uint nIP, ushort nPort, [MarshalAs( UnmanagedType.U1 )] bool bAllowUseOfPacketRelay )
		{
			return _CreateListenSocket( Self, nVirtualP2PPort, nIP, nPort, bAllowUseOfPacketRelay );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate SNetSocket_t FCreateP2PConnectionSocket( IntPtr self, SteamId steamIDTarget, int nVirtualPort, int nTimeoutSec, [MarshalAs( UnmanagedType.U1 )] bool bAllowUseOfPacketRelay );
		private FCreateP2PConnectionSocket _CreateP2PConnectionSocket;
		
		#endregion
		internal SNetSocket_t CreateP2PConnectionSocket( SteamId steamIDTarget, int nVirtualPort, int nTimeoutSec, [MarshalAs( UnmanagedType.U1 )] bool bAllowUseOfPacketRelay )
		{
			return _CreateP2PConnectionSocket( Self, steamIDTarget, nVirtualPort, nTimeoutSec, bAllowUseOfPacketRelay );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate SNetSocket_t FCreateConnectionSocket( IntPtr self, uint nIP, ushort nPort, int nTimeoutSec );
		private FCreateConnectionSocket _CreateConnectionSocket;
		
		#endregion
		internal SNetSocket_t CreateConnectionSocket( uint nIP, ushort nPort, int nTimeoutSec )
		{
			return _CreateConnectionSocket( Self, nIP, nPort, nTimeoutSec );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FDestroySocket( IntPtr self, SNetSocket_t hSocket, [MarshalAs( UnmanagedType.U1 )] bool bNotifyRemoteEnd );
		private FDestroySocket _DestroySocket;
		
		#endregion
		internal bool DestroySocket( SNetSocket_t hSocket, [MarshalAs( UnmanagedType.U1 )] bool bNotifyRemoteEnd )
		{
			return _DestroySocket( Self, hSocket, bNotifyRemoteEnd );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FDestroyListenSocket( IntPtr self, SNetListenSocket_t hSocket, [MarshalAs( UnmanagedType.U1 )] bool bNotifyRemoteEnd );
		private FDestroyListenSocket _DestroyListenSocket;
		
		#endregion
		internal bool DestroyListenSocket( SNetListenSocket_t hSocket, [MarshalAs( UnmanagedType.U1 )] bool bNotifyRemoteEnd )
		{
			return _DestroyListenSocket( Self, hSocket, bNotifyRemoteEnd );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FSendDataOnSocket( IntPtr self, SNetSocket_t hSocket, [In,Out] IntPtr[]  pubData, uint cubData, [MarshalAs( UnmanagedType.U1 )] bool bReliable );
		private FSendDataOnSocket _SendDataOnSocket;
		
		#endregion
		internal bool SendDataOnSocket( SNetSocket_t hSocket, [In,Out] IntPtr[]  pubData, uint cubData, [MarshalAs( UnmanagedType.U1 )] bool bReliable )
		{
			return _SendDataOnSocket( Self, hSocket, pubData, cubData, bReliable );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FIsDataAvailableOnSocket( IntPtr self, SNetSocket_t hSocket, ref uint pcubMsgSize );
		private FIsDataAvailableOnSocket _IsDataAvailableOnSocket;
		
		#endregion
		internal bool IsDataAvailableOnSocket( SNetSocket_t hSocket, ref uint pcubMsgSize )
		{
			return _IsDataAvailableOnSocket( Self, hSocket, ref pcubMsgSize );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FRetrieveDataFromSocket( IntPtr self, SNetSocket_t hSocket, [In,Out] IntPtr[]  pubDest, uint cubDest, ref uint pcubMsgSize );
		private FRetrieveDataFromSocket _RetrieveDataFromSocket;
		
		#endregion
		internal bool RetrieveDataFromSocket( SNetSocket_t hSocket, [In,Out] IntPtr[]  pubDest, uint cubDest, ref uint pcubMsgSize )
		{
			return _RetrieveDataFromSocket( Self, hSocket, pubDest, cubDest, ref pcubMsgSize );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FIsDataAvailable( IntPtr self, SNetListenSocket_t hListenSocket, ref uint pcubMsgSize, ref SNetSocket_t phSocket );
		private FIsDataAvailable _IsDataAvailable;
		
		#endregion
		internal bool IsDataAvailable( SNetListenSocket_t hListenSocket, ref uint pcubMsgSize, ref SNetSocket_t phSocket )
		{
			return _IsDataAvailable( Self, hListenSocket, ref pcubMsgSize, ref phSocket );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FRetrieveData( IntPtr self, SNetListenSocket_t hListenSocket, [In,Out] IntPtr[]  pubDest, uint cubDest, ref uint pcubMsgSize, ref SNetSocket_t phSocket );
		private FRetrieveData _RetrieveData;
		
		#endregion
		internal bool RetrieveData( SNetListenSocket_t hListenSocket, [In,Out] IntPtr[]  pubDest, uint cubDest, ref uint pcubMsgSize, ref SNetSocket_t phSocket )
		{
			return _RetrieveData( Self, hListenSocket, pubDest, cubDest, ref pcubMsgSize, ref phSocket );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FGetSocketInfo( IntPtr self, SNetSocket_t hSocket, ref SteamId pSteamIDRemote, ref int peSocketStatus, ref uint punIPRemote, ref ushort punPortRemote );
		private FGetSocketInfo _GetSocketInfo;
		
		#endregion
		internal bool GetSocketInfo( SNetSocket_t hSocket, ref SteamId pSteamIDRemote, ref int peSocketStatus, ref uint punIPRemote, ref ushort punPortRemote )
		{
			return _GetSocketInfo( Self, hSocket, ref pSteamIDRemote, ref peSocketStatus, ref punIPRemote, ref punPortRemote );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FGetListenSocketInfo( IntPtr self, SNetListenSocket_t hListenSocket, ref uint pnIP, ref ushort pnPort );
		private FGetListenSocketInfo _GetListenSocketInfo;
		
		#endregion
		internal bool GetListenSocketInfo( SNetListenSocket_t hListenSocket, ref uint pnIP, ref ushort pnPort )
		{
			return _GetListenSocketInfo( Self, hListenSocket, ref pnIP, ref pnPort );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate SNetSocketConnectionType FGetSocketConnectionType( IntPtr self, SNetSocket_t hSocket );
		private FGetSocketConnectionType _GetSocketConnectionType;
		
		#endregion
		internal SNetSocketConnectionType GetSocketConnectionType( SNetSocket_t hSocket )
		{
			return _GetSocketConnectionType( Self, hSocket );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate int FGetMaxPacketSize( IntPtr self, SNetSocket_t hSocket );
		private FGetMaxPacketSize _GetMaxPacketSize;
		
		#endregion
		internal int GetMaxPacketSize( SNetSocket_t hSocket )
		{
			return _GetMaxPacketSize( Self, hSocket );
		}
		
	}
}
