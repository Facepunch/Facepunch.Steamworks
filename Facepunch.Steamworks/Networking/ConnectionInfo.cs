using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	/// <summary>
	/// Describe the state of a connection
	/// </summary>
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

		/// <summary>
		/// High level state of the connection
		/// </summary>
		public ConnectionState State => state;

		/// <summary>
		/// Remote address.  Might be all 0's if we don't know it, or if this is N/A.
		/// </summary>
		public NetAddress Address => address;

		/// <summary>
		/// Who is on the other end?  Depending on the connection type and phase of the connection, we might not know
		/// </summary>
		public NetIdentity Identity => identity;

		/// <summary>
		/// Basic cause of the connection termination or problem.
		/// </summary>
		public NetConnectionEnd EndReason => (NetConnectionEnd)endReason;
	}
}