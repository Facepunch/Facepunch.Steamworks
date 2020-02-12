using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Steamworks.Data;


namespace Steamworks
{
	internal class ISteamNetworkingCustomSignalingRecvContext : SteamInterface
	{
		public override IntPtr GetInterfacePointer() => GetApi.SteamNetworkingCustomSignalingRecvContext();
		
		
		internal ISteamNetworkingCustomSignalingRecvContext()
		{
			SetupInterface();
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamNetworkingCustomSignalingRecvContext_OnConnectRequest")]
		private static extern IntPtr _OnConnectRequest( IntPtr self, Connection hConn, ref NetIdentity identityPeer );
		
		#endregion
		internal IntPtr OnConnectRequest( Connection hConn, ref NetIdentity identityPeer )
		{
			var returnValue = _OnConnectRequest( Self, hConn, ref identityPeer );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamNetworkingCustomSignalingRecvContext_SendRejectionSignal")]
		private static extern void _SendRejectionSignal( IntPtr self, ref NetIdentity identityPeer, IntPtr pMsg, int cbMsg );
		
		#endregion
		internal void SendRejectionSignal( ref NetIdentity identityPeer, IntPtr pMsg, int cbMsg )
		{
			_SendRejectionSignal( Self, ref identityPeer, pMsg, cbMsg );
		}
		
	}
}
