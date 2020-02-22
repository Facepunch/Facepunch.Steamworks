using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Steamworks.Data
{
	delegate void FSteamNetworkingSocketsDebugOutput (DebugOutputType nType, string pszMsg );

	[StructLayout( LayoutKind.Sequential )]
	public struct SteamNetworkingQuickConnectionStatus
	{
		public ConnectionState state;
		public int ping;
		public float connectionQualityLocal;
		public float connectionQualityRemote;
		public float outPacketsPerSecond;
		public float outBytesPerSecond;
		public float inPacketsPerSecond;
		public float inBytesPerSecond;
		public int sendRateBytesPerSecond;
		public int pendingUnreliable;
		public int pendingReliable;
		public int sentUnackedReliable;
		public long queueTime;

		[MarshalAs( UnmanagedType.ByValArray, SizeConst = 16 )]
		uint[] reserved;
	}

	struct SteamDatagramRelayAuthTicket
	{
		// Not implemented
	};
}