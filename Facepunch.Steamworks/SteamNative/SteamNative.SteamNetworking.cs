using System;
using System.Runtime.InteropServices;

namespace SteamNative
{
	public unsafe class SteamNetworking : IDisposable
	{
		//
		// Holds a platform specific implentation
		//
		internal Platform.Interface _pi;
		
		//
		// Constructor decides which implementation to use based on current platform
		//
		public SteamNetworking( IntPtr pointer )
		{
			if ( Platform.IsWindows64 ) _pi = new Platform.Win64( pointer );
			else if ( Platform.IsWindows32 ) _pi = new Platform.Win32( pointer );
			else if ( Platform.IsLinux32 ) _pi = new Platform.Linux32( pointer );
			else if ( Platform.IsLinux64 ) _pi = new Platform.Linux64( pointer );
			else if ( Platform.IsOsx ) _pi = new Platform.Mac( pointer );
		}
		
		//
		// Class is invalid if we don't have a valid implementation
		//
		public bool IsValid{ get{ return _pi != null && _pi.IsValid; } }
		
		//
		// When shutting down clear all the internals to avoid accidental use
		//
		public virtual void Dispose()
		{
			 if ( _pi != null )
			{
				_pi.Dispose();
				_pi = null;
			}
		}
		
		// bool
		public bool AcceptP2PSessionWithUser( CSteamID steamIDRemote /*class CSteamID*/ )
		{
			return _pi.ISteamNetworking_AcceptP2PSessionWithUser( steamIDRemote.Value /*C*/ );
		}
		
		// bool
		public bool AllowP2PPacketRelay( bool bAllow /*bool*/ )
		{
			return _pi.ISteamNetworking_AllowP2PPacketRelay( bAllow /*C*/ );
		}
		
		// bool
		public bool CloseP2PChannelWithUser( CSteamID steamIDRemote /*class CSteamID*/, int nChannel /*int*/ )
		{
			return _pi.ISteamNetworking_CloseP2PChannelWithUser( steamIDRemote.Value /*C*/, nChannel /*C*/ );
		}
		
		// bool
		public bool CloseP2PSessionWithUser( CSteamID steamIDRemote /*class CSteamID*/ )
		{
			return _pi.ISteamNetworking_CloseP2PSessionWithUser( steamIDRemote.Value /*C*/ );
		}
		
		// SNetSocket_t
		public SNetSocket_t CreateConnectionSocket( uint nIP /*uint32*/, ushort nPort /*uint16*/, int nTimeoutSec /*int*/ )
		{
			return _pi.ISteamNetworking_CreateConnectionSocket( nIP /*C*/, nPort /*C*/, nTimeoutSec /*C*/ );
		}
		
		// SNetListenSocket_t
		public SNetListenSocket_t CreateListenSocket( int nVirtualP2PPort /*int*/, uint nIP /*uint32*/, ushort nPort /*uint16*/, bool bAllowUseOfPacketRelay /*bool*/ )
		{
			return _pi.ISteamNetworking_CreateListenSocket( nVirtualP2PPort /*C*/, nIP /*C*/, nPort /*C*/, bAllowUseOfPacketRelay /*C*/ );
		}
		
		// SNetSocket_t
		public SNetSocket_t CreateP2PConnectionSocket( CSteamID steamIDTarget /*class CSteamID*/, int nVirtualPort /*int*/, int nTimeoutSec /*int*/, bool bAllowUseOfPacketRelay /*bool*/ )
		{
			return _pi.ISteamNetworking_CreateP2PConnectionSocket( steamIDTarget.Value /*C*/, nVirtualPort /*C*/, nTimeoutSec /*C*/, bAllowUseOfPacketRelay /*C*/ );
		}
		
		// bool
		public bool DestroyListenSocket( SNetListenSocket_t hSocket /*SNetListenSocket_t*/, bool bNotifyRemoteEnd /*bool*/ )
		{
			return _pi.ISteamNetworking_DestroyListenSocket( hSocket.Value /*C*/, bNotifyRemoteEnd /*C*/ );
		}
		
		// bool
		public bool DestroySocket( SNetSocket_t hSocket /*SNetSocket_t*/, bool bNotifyRemoteEnd /*bool*/ )
		{
			return _pi.ISteamNetworking_DestroySocket( hSocket.Value /*C*/, bNotifyRemoteEnd /*C*/ );
		}
		
		// bool
		public bool GetListenSocketInfo( SNetListenSocket_t hListenSocket /*SNetListenSocket_t*/, out uint pnIP /*uint32 **/, out ushort pnPort /*uint16 **/ )
		{
			return _pi.ISteamNetworking_GetListenSocketInfo( hListenSocket.Value /*C*/, out pnIP /*B*/, out pnPort /*B*/ );
		}
		
		// int
		public int GetMaxPacketSize( SNetSocket_t hSocket /*SNetSocket_t*/ )
		{
			return _pi.ISteamNetworking_GetMaxPacketSize( hSocket.Value /*C*/ );
		}
		
		// bool
		public bool GetP2PSessionState( CSteamID steamIDRemote /*class CSteamID*/, ref P2PSessionState_t pConnectionState /*struct P2PSessionState_t **/ )
		{
			return _pi.ISteamNetworking_GetP2PSessionState( steamIDRemote.Value /*C*/, ref pConnectionState /*A*/ );
		}
		
		// SNetSocketConnectionType
		public SNetSocketConnectionType GetSocketConnectionType( SNetSocket_t hSocket /*SNetSocket_t*/ )
		{
			return _pi.ISteamNetworking_GetSocketConnectionType( hSocket.Value /*C*/ );
		}
		
		// bool
		public bool GetSocketInfo( SNetSocket_t hSocket /*SNetSocket_t*/, out CSteamID pSteamIDRemote /*class CSteamID **/, IntPtr peSocketStatus /*int **/, out uint punIPRemote /*uint32 **/, out ushort punPortRemote /*uint16 **/ )
		{
			return _pi.ISteamNetworking_GetSocketInfo( hSocket.Value /*C*/, out pSteamIDRemote.Value /*B*/, (IntPtr) peSocketStatus, out punIPRemote /*B*/, out punPortRemote /*B*/ );
		}
		
		// bool
		public bool IsDataAvailable( SNetListenSocket_t hListenSocket /*SNetListenSocket_t*/, out uint pcubMsgSize /*uint32 **/, ref SNetSocket_t phSocket /*SNetSocket_t **/ )
		{
			return _pi.ISteamNetworking_IsDataAvailable( hListenSocket.Value /*C*/, out pcubMsgSize /*B*/, ref phSocket.Value /*A*/ );
		}
		
		// bool
		public bool IsDataAvailableOnSocket( SNetSocket_t hSocket /*SNetSocket_t*/, out uint pcubMsgSize /*uint32 **/ )
		{
			return _pi.ISteamNetworking_IsDataAvailableOnSocket( hSocket.Value /*C*/, out pcubMsgSize /*B*/ );
		}
		
		// bool
		public bool IsP2PPacketAvailable( out uint pcubMsgSize /*uint32 **/, int nChannel /*int*/ )
		{
			return _pi.ISteamNetworking_IsP2PPacketAvailable( out pcubMsgSize /*B*/, nChannel /*C*/ );
		}
		
		// bool
		public bool ReadP2PPacket( IntPtr pubDest /*void **/, uint cubDest /*uint32*/, out uint pcubMsgSize /*uint32 **/, out CSteamID psteamIDRemote /*class CSteamID **/, int nChannel /*int*/ )
		{
			return _pi.ISteamNetworking_ReadP2PPacket( (IntPtr) pubDest /*C*/, cubDest /*C*/, out pcubMsgSize /*B*/, out psteamIDRemote.Value /*B*/, nChannel /*C*/ );
		}
		
		// bool
		public bool RetrieveData( SNetListenSocket_t hListenSocket /*SNetListenSocket_t*/, IntPtr pubDest /*void **/, uint cubDest /*uint32*/, out uint pcubMsgSize /*uint32 **/, ref SNetSocket_t phSocket /*SNetSocket_t **/ )
		{
			return _pi.ISteamNetworking_RetrieveData( hListenSocket.Value /*C*/, (IntPtr) pubDest /*C*/, cubDest /*C*/, out pcubMsgSize /*B*/, ref phSocket.Value /*A*/ );
		}
		
		// bool
		public bool RetrieveDataFromSocket( SNetSocket_t hSocket /*SNetSocket_t*/, IntPtr pubDest /*void **/, uint cubDest /*uint32*/, out uint pcubMsgSize /*uint32 **/ )
		{
			return _pi.ISteamNetworking_RetrieveDataFromSocket( hSocket.Value /*C*/, (IntPtr) pubDest /*C*/, cubDest /*C*/, out pcubMsgSize /*B*/ );
		}
		
		// bool
		public bool SendDataOnSocket( SNetSocket_t hSocket /*SNetSocket_t*/, IntPtr pubData /*void **/, uint cubData /*uint32*/, bool bReliable /*bool*/ )
		{
			return _pi.ISteamNetworking_SendDataOnSocket( hSocket.Value /*C*/, (IntPtr) pubData /*C*/, cubData /*C*/, bReliable /*C*/ );
		}
		
		// bool
		public bool SendP2PPacket( CSteamID steamIDRemote /*class CSteamID*/, IntPtr pubData /*const void **/, uint cubData /*uint32*/, P2PSend eP2PSendType /*EP2PSend*/, int nChannel /*int*/ )
		{
			return _pi.ISteamNetworking_SendP2PPacket( steamIDRemote.Value /*C*/, (IntPtr) pubData /*C*/, cubData /*C*/, eP2PSendType /*C*/, nChannel /*C*/ );
		}
		
	}
}
