using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	[StructLayout( LayoutKind.Sequential, Size = 696 )]
	public struct ConnectionInfo
	{
		internal NetIdentity identity;
		internal long userData;
		internal Socket listenSocket;
		internal NetAddress address;
		internal ushort pad;
		internal SteamNetworkingPOPID popRemote;
		internal SteamNetworkingPOPID popRelay;
		internal ConnectionState state;
		internal int endReason;
		[MarshalAs( UnmanagedType.ByValTStr, SizeConst = 128 )]
		internal string endDebug;
		[MarshalAs( UnmanagedType.ByValTStr, SizeConst = 128 )]
		internal string connectionDescription;

		public ConnectionState State => state;
		public SteamId SteamId => identity.steamID;
	}
}