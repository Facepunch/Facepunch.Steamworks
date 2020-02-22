using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Steamworks.Data;


namespace Steamworks
{
	internal class ISteamNetworkingConnectionCustomSignaling : SteamInterface
	{
		
		internal ISteamNetworkingConnectionCustomSignaling( bool IsGameServer )
		{
			SetupInterface( IsGameServer );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamNetworkingConnectionCustomSignaling_SendSignal", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _SendSignal( IntPtr self, Connection hConn, ref ConnectionInfo info, IntPtr pMsg, int cbMsg );
		
		#endregion
		internal bool SendSignal( Connection hConn, ref ConnectionInfo info, IntPtr pMsg, int cbMsg )
		{
			var returnValue = _SendSignal( Self, hConn, ref info, pMsg, cbMsg );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamNetworkingConnectionCustomSignaling_Release", CallingConvention = Platform.CC)]
		private static extern void _Release( IntPtr self );
		
		#endregion
		internal void Release()
		{
			_Release( Self );
		}
		
	}
}
