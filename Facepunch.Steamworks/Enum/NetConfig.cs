namespace Steamworks.Data
{
	internal enum NetConfig : int
	{
		Invalid = 0,
		FakePacketLoss_Send = 2,
		FakePacketLoss_Recv = 3,
		FakePacketLag_Send = 4,
		FakePacketLag_Recv = 5,

		FakePacketReorder_Send = 6,
		FakePacketReorder_Recv = 7,

		FakePacketReorder_Time = 8,

		FakePacketDup_Send = 26,
		FakePacketDup_Recv = 27,

		FakePacketDup_TimeMax = 28,

		TimeoutInitial = 24,

		TimeoutConnected = 25,

		SendBufferSize = 9,

		SendRateMin = 10,
		SendRateMax = 11,

		NagleTime = 12,

		IP_AllowWithoutAuth = 23,

		SDRClient_ConsecutitivePingTimeoutsFailInitial = 19,

		SDRClient_ConsecutitivePingTimeoutsFail = 20,

		SDRClient_MinPingsBeforePingAccurate = 21,

		SDRClient_SingleSocket = 22,

		SDRClient_ForceRelayCluster = 29,

		SDRClient_DebugTicketAddress = 30,

		SDRClient_ForceProxyAddr = 31,

		LogLevel_AckRTT = 13,          
		LogLevel_PacketDecode = 14,       
		LogLevel_Message = 15,       
		LogLevel_PacketGaps = 16,     
		LogLevel_P2PRendezvous = 17,      
		LogLevel_SDRRelayPings = 18,     

		Force32Bit = 0x7fffffff
	}
}
