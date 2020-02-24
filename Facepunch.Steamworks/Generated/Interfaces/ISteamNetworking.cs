using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Steamworks.Data;


namespace Steamworks
{
	internal class ISteamNetworking : SteamInterface
	{
		
		internal ISteamNetworking( bool IsGameServer )
		{
			SetupInterface( IsGameServer );
		}
		
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_SteamNetworking_v006", CallingConvention = Platform.CC)]
		internal static extern IntPtr SteamAPI_SteamNetworking_v006();
		public override IntPtr GetUserInterfacePointer() => SteamAPI_SteamNetworking_v006();
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_SteamGameServerNetworking_v006", CallingConvention = Platform.CC)]
		internal static extern IntPtr SteamAPI_SteamGameServerNetworking_v006();
		public override IntPtr GetServerInterfacePointer() => SteamAPI_SteamGameServerNetworking_v006();
		
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamNetworking_SendP2PPacket", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _SendP2PPacket( IntPtr self, SteamId steamIDRemote, IntPtr pubData, uint cubData, P2PSend eP2PSendType, int nChannel );
		
		#endregion
		internal bool SendP2PPacket( SteamId steamIDRemote, IntPtr pubData, uint cubData, P2PSend eP2PSendType, int nChannel )
		{
			var returnValue = _SendP2PPacket( Self, steamIDRemote, pubData, cubData, eP2PSendType, nChannel );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamNetworking_IsP2PPacketAvailable", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _IsP2PPacketAvailable( IntPtr self, ref uint pcubMsgSize, int nChannel );
		
		#endregion
		internal bool IsP2PPacketAvailable( ref uint pcubMsgSize, int nChannel )
		{
			var returnValue = _IsP2PPacketAvailable( Self, ref pcubMsgSize, nChannel );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamNetworking_ReadP2PPacket", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _ReadP2PPacket( IntPtr self, IntPtr pubDest, uint cubDest, ref uint pcubMsgSize, ref SteamId psteamIDRemote, int nChannel );
		
		#endregion
		internal bool ReadP2PPacket( IntPtr pubDest, uint cubDest, ref uint pcubMsgSize, ref SteamId psteamIDRemote, int nChannel )
		{
			var returnValue = _ReadP2PPacket( Self, pubDest, cubDest, ref pcubMsgSize, ref psteamIDRemote, nChannel );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamNetworking_AcceptP2PSessionWithUser", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _AcceptP2PSessionWithUser( IntPtr self, SteamId steamIDRemote );
		
		#endregion
		internal bool AcceptP2PSessionWithUser( SteamId steamIDRemote )
		{
			var returnValue = _AcceptP2PSessionWithUser( Self, steamIDRemote );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamNetworking_CloseP2PSessionWithUser", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _CloseP2PSessionWithUser( IntPtr self, SteamId steamIDRemote );
		
		#endregion
		internal bool CloseP2PSessionWithUser( SteamId steamIDRemote )
		{
			var returnValue = _CloseP2PSessionWithUser( Self, steamIDRemote );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamNetworking_CloseP2PChannelWithUser", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _CloseP2PChannelWithUser( IntPtr self, SteamId steamIDRemote, int nChannel );
		
		#endregion
		internal bool CloseP2PChannelWithUser( SteamId steamIDRemote, int nChannel )
		{
			var returnValue = _CloseP2PChannelWithUser( Self, steamIDRemote, nChannel );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamNetworking_GetP2PSessionState", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _GetP2PSessionState( IntPtr self, SteamId steamIDRemote, ref P2PSessionState_t pConnectionState );
		
		#endregion
		internal bool GetP2PSessionState( SteamId steamIDRemote, ref P2PSessionState_t pConnectionState )
		{
			var returnValue = _GetP2PSessionState( Self, steamIDRemote, ref pConnectionState );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamNetworking_AllowP2PPacketRelay", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _AllowP2PPacketRelay( IntPtr self, [MarshalAs( UnmanagedType.U1 )] bool bAllow );
		
		#endregion
		internal bool AllowP2PPacketRelay( [MarshalAs( UnmanagedType.U1 )] bool bAllow )
		{
			var returnValue = _AllowP2PPacketRelay( Self, bAllow );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamNetworking_CreateP2PConnectionSocket", CallingConvention = Platform.CC)]
		private static extern SNetSocket_t _CreateP2PConnectionSocket( IntPtr self, SteamId steamIDTarget, int nVirtualPort, int nTimeoutSec, [MarshalAs( UnmanagedType.U1 )] bool bAllowUseOfPacketRelay );
		
		#endregion
		internal SNetSocket_t CreateP2PConnectionSocket( SteamId steamIDTarget, int nVirtualPort, int nTimeoutSec, [MarshalAs( UnmanagedType.U1 )] bool bAllowUseOfPacketRelay )
		{
			var returnValue = _CreateP2PConnectionSocket( Self, steamIDTarget, nVirtualPort, nTimeoutSec, bAllowUseOfPacketRelay );
			return returnValue;
		}
		
	}
}
