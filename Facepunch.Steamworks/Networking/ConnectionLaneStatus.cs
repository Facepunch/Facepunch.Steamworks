using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	/// <summary>
	/// Describe the status of a connection
	/// </summary>
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	public struct ConnectionLaneStatus
	{
		internal int cbPendingUnreliable; // m_cbPendingUnreliable int
		internal int cbPendingReliable; // m_cbPendingReliable int
		internal int cbSentUnackedReliable; // m_cbSentUnackedReliable int
		internal int _reservePad1; // _reservePad1 int
		internal long ecQueueTime; // m_usecQueueTime SteamNetworkingMicroseconds
		[MarshalAs( UnmanagedType.ByValArray, SizeConst = 10, ArraySubType = UnmanagedType.U4 )]
		internal uint[] reserved; // reserved uint32 [10]

		/// <summary>
		/// Number of bytes unreliable data pending to be sent. This is data that you have recently requested to be sent but has not yet actually been put on the wire.
		/// </summary>
		public int PendingUnreliable => cbPendingUnreliable;

		/// <summary>
		/// Number of bytes reliable data pending to be sent. This is data that you have recently requested to be sent but has not yet actually been put on the wire.
		/// </summary>
		public int PendingReliable => cbPendingReliable;

		/// <summary>
		/// Number of bytes of reliable data that has been placed the wire, but for which we have not yet received an acknowledgment, and thus we may have to re-transmit.
		/// </summary>
		public int SentUnackedReliable => cbSentUnackedReliable;
	}
}