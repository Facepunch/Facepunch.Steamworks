using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	[StructLayout( LayoutKind.Sequential, Pack = 0 )]
	public struct ConnectionInfo
	{
		internal NetworkIdentity identity;
		internal long userData;
		internal Socket listenSocket;
		internal NetworkAddress address;
		internal ushort pad;
		internal SteamNetworkingPOPID popRemote;
		internal SteamNetworkingPOPID popRelay;
		internal SteamNetworkingConnectionState state;
		internal int endReason;
		[MarshalAs( UnmanagedType.ByValTStr, SizeConst = 128 )]
		internal string endDebug;
		[MarshalAs( UnmanagedType.ByValTStr, SizeConst = 128 )]
		internal string connectionDescription;
	}
}