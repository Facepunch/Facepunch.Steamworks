using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	/// <summary>
	/// Describe the status of a connection
	/// </summary>
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPlatformPackSize )]
	public struct ConnectionStatus
	{
		internal ConnectionState state; // m_eState ESteamNetworkingConnectionState
		internal int ping; // m_nPing int
		internal float connectionQualityLocal; // m_flConnectionQualityLocal float
		internal float connectionQualityRemote; // m_flConnectionQualityRemote float
		internal float outPacketsPerSec; // m_flOutPacketsPerSec float
		internal float outBytesPerSec; // m_flOutBytesPerSec float
		internal float inPacketsPerSec; // m_flInPacketsPerSec float
		internal float inBytesPerSec; // m_flInBytesPerSec float
		internal int sendRateBytesPerSecond; // m_nSendRateBytesPerSecond int
		internal int cbPendingUnreliable; // m_cbPendingUnreliable int
		internal int cbPendingReliable; // m_cbPendingReliable int
		internal int cbSentUnackedReliable; // m_cbSentUnackedReliable int
		internal long ecQueueTime; // m_usecQueueTime SteamNetworkingMicroseconds
		[MarshalAs( UnmanagedType.ByValArray, SizeConst = 16, ArraySubType = UnmanagedType.U4 )]
		internal uint[] reserved; // reserved uint32 [16]

		/// <summary>
		/// Current ping (ms)
		/// </summary>
		public int Ping => ping;

		/// <summary>
		/// Outgoing packets per second
		/// </summary>
		public float OutPacketsPerSec => outPacketsPerSec;

		/// <summary>
		/// Outgoing bytes per second
		/// </summary>
		public float OutBytesPerSec => outBytesPerSec;

		/// <summary>
		/// Incoming packets per second
		/// </summary>
		public float InPacketsPerSec => inPacketsPerSec;

		/// <summary>
		/// Incoming bytes per second
		/// </summary>
		public float InBytesPerSec => inBytesPerSec;

		/// <summary>
		/// Connection quality measured locally, 0...1 (percentage of packets delivered end-to-end in order).
		/// </summary>
		public float ConnectionQualityLocal => connectionQualityLocal;

		/// <summary>
		/// Packet delivery success rate as observed from remote host, 0...1 (percentage of packets delivered end-to-end in order).
		/// </summary>
		public float ConnectionQualityRemote => connectionQualityRemote;

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