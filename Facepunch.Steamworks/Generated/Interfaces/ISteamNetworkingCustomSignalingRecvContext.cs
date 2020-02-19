using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Steamworks.Data;


namespace Steamworks
{
	internal class ISteamNetworkingCustomSignalingRecvContext : SteamInterface
	{
		
		internal ISteamNetworkingCustomSignalingRecvContext( bool IsGameServer )
		{
			SetupInterface( IsGameServer );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamNetworkingCustomSignalingRecvContext_OnConnectRequest", CallingConvention = Platform.CC)]
		private static extern IntPtr _OnConnectRequest( IntPtr self, Connection hConn, ref NetIdentity identityPeer );
		
		#endregion
		internal IntPtr OnConnectRequest( Connection hConn, ref NetIdentity identityPeer )
		{
			var returnValue = _OnConnectRequest( Self, hConn, ref identityPeer );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamNetworkingCustomSignalingRecvContext_SendRejectionSignal", CallingConvention = Platform.CC)]
		private static extern void _SendRejectionSignal( IntPtr self, ref NetIdentity identityPeer, IntPtr pMsg, int cbMsg );
		
		#endregion
		internal void SendRejectionSignal( ref NetIdentity identityPeer, IntPtr pMsg, int cbMsg )
		{
			_SendRejectionSignal( Self, ref identityPeer, pMsg, cbMsg );
		}
		
	}
}
