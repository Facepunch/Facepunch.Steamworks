using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Steamworks.Data;


namespace Steamworks
{
	internal unsafe class ISteamNetworkingFakeUDPPort : SteamInterface
	{
		
		internal ISteamNetworkingFakeUDPPort( bool IsGameServer )
		{
			SetupInterface( IsGameServer );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamNetworkingFakeUDPPort_DestroyFakeUDPPort", CallingConvention = Platform.CC)]
		private static extern void _DestroyFakeUDPPort( IntPtr self );
		
		#endregion
		internal void DestroyFakeUDPPort()
		{
			_DestroyFakeUDPPort( Self );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamNetworkingFakeUDPPort_SendMessageToFakeIP", CallingConvention = Platform.CC)]
		private static extern Result _SendMessageToFakeIP( IntPtr self, ref NetAddress remoteAddress, IntPtr pData, uint cbData, int nSendFlags );
		
		#endregion
		internal Result SendMessageToFakeIP( ref NetAddress remoteAddress, IntPtr pData, uint cbData, int nSendFlags )
		{
			var returnValue = _SendMessageToFakeIP( Self, ref remoteAddress, pData, cbData, nSendFlags );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamNetworkingFakeUDPPort_ReceiveMessages", CallingConvention = Platform.CC)]
		private static extern int _ReceiveMessages( IntPtr self, IntPtr ppOutMessages, int nMaxMessages );
		
		#endregion
		internal int ReceiveMessages( IntPtr ppOutMessages, int nMaxMessages )
		{
			var returnValue = _ReceiveMessages( Self, ppOutMessages, nMaxMessages );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamNetworkingFakeUDPPort_ScheduleCleanup", CallingConvention = Platform.CC)]
		private static extern void _ScheduleCleanup( IntPtr self, ref NetAddress remoteAddress );
		
		#endregion
		internal void ScheduleCleanup( ref NetAddress remoteAddress )
		{
			_ScheduleCleanup( Self, ref remoteAddress );
		}
		
	}
}
