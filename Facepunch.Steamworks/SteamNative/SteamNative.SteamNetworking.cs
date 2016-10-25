using System;
using System.Runtime.InteropServices;

namespace SteamNative
{
	public unsafe class SteamNetworking
	{
		internal IntPtr _ptr;
		
		public SteamNetworking( IntPtr pointer )
		{
			_ptr = pointer;
		}
		
		
		// bool
		public bool AcceptP2PSessionWithUser( CSteamID steamIDRemote /*class CSteamID*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamNetworking.AcceptP2PSessionWithUser( _ptr, steamIDRemote );
			else return Platform.Win64.ISteamNetworking.AcceptP2PSessionWithUser( _ptr, steamIDRemote );
		}
		
		// bool
		public bool AllowP2PPacketRelay( bool bAllow /*bool*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamNetworking.AllowP2PPacketRelay( _ptr, bAllow );
			else return Platform.Win64.ISteamNetworking.AllowP2PPacketRelay( _ptr, bAllow );
		}
		
		// bool
		public bool CloseP2PChannelWithUser( CSteamID steamIDRemote /*class CSteamID*/, int nChannel /*int*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamNetworking.CloseP2PChannelWithUser( _ptr, steamIDRemote, nChannel );
			else return Platform.Win64.ISteamNetworking.CloseP2PChannelWithUser( _ptr, steamIDRemote, nChannel );
		}
		
		// bool
		public bool CloseP2PSessionWithUser( CSteamID steamIDRemote /*class CSteamID*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamNetworking.CloseP2PSessionWithUser( _ptr, steamIDRemote );
			else return Platform.Win64.ISteamNetworking.CloseP2PSessionWithUser( _ptr, steamIDRemote );
		}
		
		// SNetSocket_t
		public SNetSocket_t CreateConnectionSocket( uint nIP /*uint32*/, ushort nPort /*uint16*/, int nTimeoutSec /*int*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamNetworking.CreateConnectionSocket( _ptr, nIP, nPort, nTimeoutSec );
			else return Platform.Win64.ISteamNetworking.CreateConnectionSocket( _ptr, nIP, nPort, nTimeoutSec );
		}
		
		// SNetListenSocket_t
		public SNetListenSocket_t CreateListenSocket( int nVirtualP2PPort /*int*/, uint nIP /*uint32*/, ushort nPort /*uint16*/, bool bAllowUseOfPacketRelay /*bool*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamNetworking.CreateListenSocket( _ptr, nVirtualP2PPort, nIP, nPort, bAllowUseOfPacketRelay );
			else return Platform.Win64.ISteamNetworking.CreateListenSocket( _ptr, nVirtualP2PPort, nIP, nPort, bAllowUseOfPacketRelay );
		}
		
		// SNetSocket_t
		public SNetSocket_t CreateP2PConnectionSocket( CSteamID steamIDTarget /*class CSteamID*/, int nVirtualPort /*int*/, int nTimeoutSec /*int*/, bool bAllowUseOfPacketRelay /*bool*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamNetworking.CreateP2PConnectionSocket( _ptr, steamIDTarget, nVirtualPort, nTimeoutSec, bAllowUseOfPacketRelay );
			else return Platform.Win64.ISteamNetworking.CreateP2PConnectionSocket( _ptr, steamIDTarget, nVirtualPort, nTimeoutSec, bAllowUseOfPacketRelay );
		}
		
		// bool
		public bool DestroyListenSocket( SNetListenSocket_t hSocket /*SNetListenSocket_t*/, bool bNotifyRemoteEnd /*bool*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamNetworking.DestroyListenSocket( _ptr, hSocket, bNotifyRemoteEnd );
			else return Platform.Win64.ISteamNetworking.DestroyListenSocket( _ptr, hSocket, bNotifyRemoteEnd );
		}
		
		// bool
		public bool DestroySocket( SNetSocket_t hSocket /*SNetSocket_t*/, bool bNotifyRemoteEnd /*bool*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamNetworking.DestroySocket( _ptr, hSocket, bNotifyRemoteEnd );
			else return Platform.Win64.ISteamNetworking.DestroySocket( _ptr, hSocket, bNotifyRemoteEnd );
		}
		
		// bool
		public bool GetListenSocketInfo( SNetListenSocket_t hListenSocket /*SNetListenSocket_t*/, out uint pnIP /*uint32 **/, out ushort pnPort /*uint16 **/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamNetworking.GetListenSocketInfo( _ptr, hListenSocket, out pnIP, out pnPort );
			else return Platform.Win64.ISteamNetworking.GetListenSocketInfo( _ptr, hListenSocket, out pnIP, out pnPort );
		}
		
		// int
		public int GetMaxPacketSize( SNetSocket_t hSocket /*SNetSocket_t*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamNetworking.GetMaxPacketSize( _ptr, hSocket );
			else return Platform.Win64.ISteamNetworking.GetMaxPacketSize( _ptr, hSocket );
		}
		
		// bool
		public bool GetP2PSessionState( CSteamID steamIDRemote /*class CSteamID*/, ref P2PSessionState_t pConnectionState /*struct P2PSessionState_t **/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamNetworking.GetP2PSessionState( _ptr, steamIDRemote, ref pConnectionState );
			else return Platform.Win64.ISteamNetworking.GetP2PSessionState( _ptr, steamIDRemote, ref pConnectionState );
		}
		
		// SNetSocketConnectionType
		public SNetSocketConnectionType GetSocketConnectionType( SNetSocket_t hSocket /*SNetSocket_t*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamNetworking.GetSocketConnectionType( _ptr, hSocket );
			else return Platform.Win64.ISteamNetworking.GetSocketConnectionType( _ptr, hSocket );
		}
		
		// bool
		public bool GetSocketInfo( SNetSocket_t hSocket /*SNetSocket_t*/, out CSteamID pSteamIDRemote /*class CSteamID **/, IntPtr peSocketStatus /*int **/, out uint punIPRemote /*uint32 **/, out ushort punPortRemote /*uint16 **/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamNetworking.GetSocketInfo( _ptr, hSocket, out pSteamIDRemote, (IntPtr) peSocketStatus, out punIPRemote, out punPortRemote );
			else return Platform.Win64.ISteamNetworking.GetSocketInfo( _ptr, hSocket, out pSteamIDRemote, (IntPtr) peSocketStatus, out punIPRemote, out punPortRemote );
		}
		
		// bool
		public bool IsDataAvailable( SNetListenSocket_t hListenSocket /*SNetListenSocket_t*/, out uint pcubMsgSize /*uint32 **/, ref SNetSocket_t phSocket /*SNetSocket_t **/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamNetworking.IsDataAvailable( _ptr, hListenSocket, out pcubMsgSize, ref phSocket );
			else return Platform.Win64.ISteamNetworking.IsDataAvailable( _ptr, hListenSocket, out pcubMsgSize, ref phSocket );
		}
		
		// bool
		public bool IsDataAvailableOnSocket( SNetSocket_t hSocket /*SNetSocket_t*/, out uint pcubMsgSize /*uint32 **/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamNetworking.IsDataAvailableOnSocket( _ptr, hSocket, out pcubMsgSize );
			else return Platform.Win64.ISteamNetworking.IsDataAvailableOnSocket( _ptr, hSocket, out pcubMsgSize );
		}
		
		// bool
		public bool IsP2PPacketAvailable( out uint pcubMsgSize /*uint32 **/, int nChannel /*int*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamNetworking.IsP2PPacketAvailable( _ptr, out pcubMsgSize, nChannel );
			else return Platform.Win64.ISteamNetworking.IsP2PPacketAvailable( _ptr, out pcubMsgSize, nChannel );
		}
		
		// bool
		public bool ReadP2PPacket( IntPtr pubDest /*void **/, uint cubDest /*uint32*/, out uint pcubMsgSize /*uint32 **/, out CSteamID psteamIDRemote /*class CSteamID **/, int nChannel /*int*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamNetworking.ReadP2PPacket( _ptr, (IntPtr) pubDest, cubDest, out pcubMsgSize, out psteamIDRemote, nChannel );
			else return Platform.Win64.ISteamNetworking.ReadP2PPacket( _ptr, (IntPtr) pubDest, cubDest, out pcubMsgSize, out psteamIDRemote, nChannel );
		}
		
		// bool
		public bool RetrieveData( SNetListenSocket_t hListenSocket /*SNetListenSocket_t*/, IntPtr pubDest /*void **/, uint cubDest /*uint32*/, out uint pcubMsgSize /*uint32 **/, ref SNetSocket_t phSocket /*SNetSocket_t **/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamNetworking.RetrieveData( _ptr, hListenSocket, (IntPtr) pubDest, cubDest, out pcubMsgSize, ref phSocket );
			else return Platform.Win64.ISteamNetworking.RetrieveData( _ptr, hListenSocket, (IntPtr) pubDest, cubDest, out pcubMsgSize, ref phSocket );
		}
		
		// bool
		public bool RetrieveDataFromSocket( SNetSocket_t hSocket /*SNetSocket_t*/, IntPtr pubDest /*void **/, uint cubDest /*uint32*/, out uint pcubMsgSize /*uint32 **/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamNetworking.RetrieveDataFromSocket( _ptr, hSocket, (IntPtr) pubDest, cubDest, out pcubMsgSize );
			else return Platform.Win64.ISteamNetworking.RetrieveDataFromSocket( _ptr, hSocket, (IntPtr) pubDest, cubDest, out pcubMsgSize );
		}
		
		// bool
		public bool SendDataOnSocket( SNetSocket_t hSocket /*SNetSocket_t*/, IntPtr pubData /*void **/, uint cubData /*uint32*/, bool bReliable /*bool*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamNetworking.SendDataOnSocket( _ptr, hSocket, (IntPtr) pubData, cubData, bReliable );
			else return Platform.Win64.ISteamNetworking.SendDataOnSocket( _ptr, hSocket, (IntPtr) pubData, cubData, bReliable );
		}
		
		// bool
		public bool SendP2PPacket( CSteamID steamIDRemote /*class CSteamID*/, IntPtr pubData /*const void **/, uint cubData /*uint32*/, P2PSend eP2PSendType /*EP2PSend*/, int nChannel /*int*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamNetworking.SendP2PPacket( _ptr, steamIDRemote, (IntPtr) pubData, cubData, eP2PSendType, nChannel );
			else return Platform.Win64.ISteamNetworking.SendP2PPacket( _ptr, steamIDRemote, (IntPtr) pubData, cubData, eP2PSendType, nChannel );
		}
		
	}
}
