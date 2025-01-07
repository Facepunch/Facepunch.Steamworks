using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Steamworks.Data;


namespace Steamworks
{
	internal unsafe class ISteamNetworkingMessages : SteamInterface
	{
		
		internal ISteamNetworkingMessages( bool IsGameServer )
		{
			SetupInterface( IsGameServer );
		}
		
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_SteamNetworkingMessages_SteamAPI_v002", CallingConvention = Platform.CC)]
		internal static extern IntPtr SteamAPI_SteamNetworkingMessages_SteamAPI_v002();
		public override IntPtr GetUserInterfacePointer() => SteamAPI_SteamNetworkingMessages_SteamAPI_v002();
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_SteamGameServerNetworkingMessages_SteamAPI_v002", CallingConvention = Platform.CC)]
		internal static extern IntPtr SteamAPI_SteamGameServerNetworkingMessages_SteamAPI_v002();
		public override IntPtr GetServerInterfacePointer() => SteamAPI_SteamGameServerNetworkingMessages_SteamAPI_v002();
		
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamNetworkingMessages_SendMessageToUser", CallingConvention = Platform.CC)]
		private static extern Result _SendMessageToUser( IntPtr self, ref NetIdentity identityRemote, [In,Out] IntPtr[]  pubData, uint cubData, int nSendFlags, int nRemoteChannel );
		
		#endregion
		internal Result SendMessageToUser( ref NetIdentity identityRemote, [In,Out] IntPtr[]  pubData, uint cubData, int nSendFlags, int nRemoteChannel )
		{
			var returnValue = _SendMessageToUser( Self, ref identityRemote, pubData, cubData, nSendFlags, nRemoteChannel );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamNetworkingMessages_ReceiveMessagesOnChannel", CallingConvention = Platform.CC)]
		private static extern int _ReceiveMessagesOnChannel( IntPtr self, int nLocalChannel, IntPtr ppOutMessages, int nMaxMessages );
		
		#endregion
		internal int ReceiveMessagesOnChannel( int nLocalChannel, IntPtr ppOutMessages, int nMaxMessages )
		{
			var returnValue = _ReceiveMessagesOnChannel( Self, nLocalChannel, ppOutMessages, nMaxMessages );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamNetworkingMessages_AcceptSessionWithUser", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _AcceptSessionWithUser( IntPtr self, ref NetIdentity identityRemote );
		
		#endregion
		internal bool AcceptSessionWithUser( ref NetIdentity identityRemote )
		{
			var returnValue = _AcceptSessionWithUser( Self, ref identityRemote );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamNetworkingMessages_CloseSessionWithUser", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _CloseSessionWithUser( IntPtr self, ref NetIdentity identityRemote );
		
		#endregion
		internal bool CloseSessionWithUser( ref NetIdentity identityRemote )
		{
			var returnValue = _CloseSessionWithUser( Self, ref identityRemote );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamNetworkingMessages_CloseChannelWithUser", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _CloseChannelWithUser( IntPtr self, ref NetIdentity identityRemote, int nLocalChannel );
		
		#endregion
		internal bool CloseChannelWithUser( ref NetIdentity identityRemote, int nLocalChannel )
		{
			var returnValue = _CloseChannelWithUser( Self, ref identityRemote, nLocalChannel );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamNetworkingMessages_GetSessionConnectionInfo", CallingConvention = Platform.CC)]
		private static extern ConnectionState _GetSessionConnectionInfo( IntPtr self, ref NetIdentity identityRemote, ref ConnectionInfo pConnectionInfo, ref ConnectionStatus pQuickStatus );
		
		#endregion
		internal ConnectionState GetSessionConnectionInfo( ref NetIdentity identityRemote, ref ConnectionInfo pConnectionInfo, ref ConnectionStatus pQuickStatus )
		{
			var returnValue = _GetSessionConnectionInfo( Self, ref identityRemote, ref pConnectionInfo, ref pQuickStatus );
			return returnValue;
		}
		
	}
}
