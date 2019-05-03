namespace Steamworks.Data
{
	enum SteamNetworkingGetConfigValueResult
	{
		BadValue = -1, // No such configuration value
		BadScopeObj = -2,  // Bad connection handle, etc
		BufferTooSmall = -3, // Couldn't fit the result in your buffer
		OK = 1,
		OKInherited = 2, // A value was not set at this level, but the effective (inherited) value was returned.

		Force32Bit = 0x7fffffff
	};

	enum NetConfigType
	{
		Int32 = 1,
		Int64 = 2,
		Float = 3,
		String = 4,
		FunctionPtr = 5, // NOTE: When setting	callbacks, you should put the pointer into a variable and pass a pointer to that variable.

		Force32Bit = 0x7fffffff
	};

	enum SteamNetworkingSocketsDebugOutputType : int
	{
		None = 0,
		Bug = 1, // You used the API incorrectly, or an internal error happened
		Error = 2, // Run-time error condition that isn't the result of a bug.  (E.g. we are offline, cannot bind a port, etc)
		Important = 3, // Nothing is wrong, but this is an important notification
		Warning = 4,
		Msg = 5, // Recommended amount
		Verbose = 6, // Quite a bit
		Debug = 7, // Practically everything
		Everything = 8, // Wall of text, detailed packet contents breakdown, etc

		Force32Bit = 0x7fffffff
	};

	internal enum NetScope : int
	{
		Global = 1,
		SocketsInterface = 2,
		ListenSocket = 3,
		Connection = 4,

		Force32Bit = 0x7fffffff
	}

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
