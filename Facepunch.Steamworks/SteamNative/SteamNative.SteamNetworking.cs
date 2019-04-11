using System;
using System.Runtime.InteropServices;
using System.Linq;

namespace SteamNative
{
	internal unsafe class SteamNetworking : IDisposable
	{
		//
		// Holds a platform specific implentation
		//
		internal Platform.Interface platform;
		internal Facepunch.Steamworks.BaseSteamworks steamworks;
		
		//
		// Constructor decides which implementation to use based on current platform
		//
		internal SteamNetworking( Facepunch.Steamworks.BaseSteamworks steamworks, IntPtr pointer )
		{
			this.steamworks = steamworks;
			
			if ( Platform.IsWindows ) platform = new Platform.Windows( pointer );
			else if ( Platform.IsLinux ) platform = new Platform.Linux( pointer );
			else if ( Platform.IsOsx ) platform = new Platform.Mac( pointer );
		}
		
		//
		// Class is invalid if we don't have a valid implementation
		//
		public bool IsValid => platform != null && platform.IsValid;
		
		//
		// When shutting down clear all the internals to avoid accidental use
		//
		public virtual void Dispose()
		{
			 if ( platform != null )
			{
				platform.Dispose();
				platform = null;
			}
		}
		
		// bool
		public bool AcceptP2PSessionWithUser( CSteamID steamIDRemote /*class CSteamID*/ )
		{
			return platform.ISteamNetworking_AcceptP2PSessionWithUser( steamIDRemote.Value );
		}
		
		// bool
		public bool AllowP2PPacketRelay( bool bAllow /*bool*/ )
		{
			return platform.ISteamNetworking_AllowP2PPacketRelay( bAllow );
		}
		
		// bool
		public bool CloseP2PChannelWithUser( CSteamID steamIDRemote /*class CSteamID*/, int nChannel /*int*/ )
		{
			return platform.ISteamNetworking_CloseP2PChannelWithUser( steamIDRemote.Value, nChannel );
		}
		
		// bool
		public bool CloseP2PSessionWithUser( CSteamID steamIDRemote /*class CSteamID*/ )
		{
			return platform.ISteamNetworking_CloseP2PSessionWithUser( steamIDRemote.Value );
		}
		
		// SNetSocket_t
		public SNetSocket_t CreateConnectionSocket( uint nIP /*uint32*/, ushort nPort /*uint16*/, int nTimeoutSec /*int*/ )
		{
			return platform.ISteamNetworking_CreateConnectionSocket( nIP, nPort, nTimeoutSec );
		}
		
		// SNetListenSocket_t
		public SNetListenSocket_t CreateListenSocket( int nVirtualP2PPort /*int*/, uint nIP /*uint32*/, ushort nPort /*uint16*/, bool bAllowUseOfPacketRelay /*bool*/ )
		{
			return platform.ISteamNetworking_CreateListenSocket( nVirtualP2PPort, nIP, nPort, bAllowUseOfPacketRelay );
		}
		
		// SNetSocket_t
		public SNetSocket_t CreateP2PConnectionSocket( CSteamID steamIDTarget /*class CSteamID*/, int nVirtualPort /*int*/, int nTimeoutSec /*int*/, bool bAllowUseOfPacketRelay /*bool*/ )
		{
			return platform.ISteamNetworking_CreateP2PConnectionSocket( steamIDTarget.Value, nVirtualPort, nTimeoutSec, bAllowUseOfPacketRelay );
		}
		
		// bool
		public bool DestroyListenSocket( SNetListenSocket_t hSocket /*SNetListenSocket_t*/, bool bNotifyRemoteEnd /*bool*/ )
		{
			return platform.ISteamNetworking_DestroyListenSocket( hSocket.Value, bNotifyRemoteEnd );
		}
		
		// bool
		public bool DestroySocket( SNetSocket_t hSocket /*SNetSocket_t*/, bool bNotifyRemoteEnd /*bool*/ )
		{
			return platform.ISteamNetworking_DestroySocket( hSocket.Value, bNotifyRemoteEnd );
		}
		
		// bool
		public bool GetListenSocketInfo( SNetListenSocket_t hListenSocket /*SNetListenSocket_t*/, out uint pnIP /*uint32 **/, out ushort pnPort /*uint16 **/ )
		{
			return platform.ISteamNetworking_GetListenSocketInfo( hListenSocket.Value, out pnIP, out pnPort );
		}
		
		// int
		public int GetMaxPacketSize( SNetSocket_t hSocket /*SNetSocket_t*/ )
		{
			return platform.ISteamNetworking_GetMaxPacketSize( hSocket.Value );
		}
		
		// bool
		public bool GetP2PSessionState( CSteamID steamIDRemote /*class CSteamID*/, ref P2PSessionState_t pConnectionState /*struct P2PSessionState_t **/ )
		{
			return platform.ISteamNetworking_GetP2PSessionState( steamIDRemote.Value, ref pConnectionState );
		}
		
		// SNetSocketConnectionType
		public SNetSocketConnectionType GetSocketConnectionType( SNetSocket_t hSocket /*SNetSocket_t*/ )
		{
			return platform.ISteamNetworking_GetSocketConnectionType( hSocket.Value );
		}
		
		// bool
		public bool GetSocketInfo( SNetSocket_t hSocket /*SNetSocket_t*/, out CSteamID pSteamIDRemote /*class CSteamID **/, IntPtr peSocketStatus /*int **/, out uint punIPRemote /*uint32 **/, out ushort punPortRemote /*uint16 **/ )
		{
			return platform.ISteamNetworking_GetSocketInfo( hSocket.Value, out pSteamIDRemote.Value, (IntPtr) peSocketStatus, out punIPRemote, out punPortRemote );
		}
		
		// bool
		public bool IsDataAvailable( SNetListenSocket_t hListenSocket /*SNetListenSocket_t*/, out uint pcubMsgSize /*uint32 **/, ref SNetSocket_t phSocket /*SNetSocket_t **/ )
		{
			return platform.ISteamNetworking_IsDataAvailable( hListenSocket.Value, out pcubMsgSize, ref phSocket.Value );
		}
		
		// bool
		public bool IsDataAvailableOnSocket( SNetSocket_t hSocket /*SNetSocket_t*/, out uint pcubMsgSize /*uint32 **/ )
		{
			return platform.ISteamNetworking_IsDataAvailableOnSocket( hSocket.Value, out pcubMsgSize );
		}
		
		// bool
		public bool IsP2PPacketAvailable( out uint pcubMsgSize /*uint32 **/, int nChannel /*int*/ )
		{
			return platform.ISteamNetworking_IsP2PPacketAvailable( out pcubMsgSize, nChannel );
		}
		
		// bool
		public bool ReadP2PPacket( IntPtr pubDest /*void **/, uint cubDest /*uint32*/, out uint pcubMsgSize /*uint32 **/, out CSteamID psteamIDRemote /*class CSteamID **/, int nChannel /*int*/ )
		{
			return platform.ISteamNetworking_ReadP2PPacket( (IntPtr) pubDest, cubDest, out pcubMsgSize, out psteamIDRemote.Value, nChannel );
		}
		
		// bool
		public bool RetrieveData( SNetListenSocket_t hListenSocket /*SNetListenSocket_t*/, IntPtr pubDest /*void **/, uint cubDest /*uint32*/, out uint pcubMsgSize /*uint32 **/, ref SNetSocket_t phSocket /*SNetSocket_t **/ )
		{
			return platform.ISteamNetworking_RetrieveData( hListenSocket.Value, (IntPtr) pubDest, cubDest, out pcubMsgSize, ref phSocket.Value );
		}
		
		// bool
		public bool RetrieveDataFromSocket( SNetSocket_t hSocket /*SNetSocket_t*/, IntPtr pubDest /*void **/, uint cubDest /*uint32*/, out uint pcubMsgSize /*uint32 **/ )
		{
			return platform.ISteamNetworking_RetrieveDataFromSocket( hSocket.Value, (IntPtr) pubDest, cubDest, out pcubMsgSize );
		}
		
		// bool
		public bool SendDataOnSocket( SNetSocket_t hSocket /*SNetSocket_t*/, IntPtr pubData /*void **/, uint cubData /*uint32*/, bool bReliable /*bool*/ )
		{
			return platform.ISteamNetworking_SendDataOnSocket( hSocket.Value, (IntPtr) pubData, cubData, bReliable );
		}
		
		// bool
		public bool SendP2PPacket( CSteamID steamIDRemote /*class CSteamID*/, IntPtr pubData /*const void **/, uint cubData /*uint32*/, P2PSend eP2PSendType /*EP2PSend*/, int nChannel /*int*/ )
		{
			return platform.ISteamNetworking_SendP2PPacket( steamIDRemote.Value, (IntPtr) pubData, cubData, eP2PSendType, nChannel );
		}
		
	}
}
