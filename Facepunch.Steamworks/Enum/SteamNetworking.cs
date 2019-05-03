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

	/// High level connection status
	public enum ConnectionState
	{

		/// Dummy value used to indicate an error condition in the API.
		/// Specified connection doesn't exist or has already been closed.
		None = 0,

		/// We are trying to establish whether peers can talk to each other,
		/// whether they WANT to talk to each other, perform basic auth,
		/// and exchange crypt keys.
		///
		/// - For connections on the "client" side (initiated locally):
		///   We're in the process of trying to establish a connection.
		///   Depending on the connection type, we might not know who they are.
		///   Note that it is not possible to tell if we are waiting on the
		///   network to complete handshake packets, or for the application layer
		///   to accept the connection.
		///
		/// - For connections on the "server" side (accepted through listen socket):
		///   We have completed some basic handshake and the client has presented
		///   some proof of identity.  The connection is ready to be accepted
		///   using AcceptConnection().
		///
		/// In either case, any unreliable packets sent now are almost certain
		/// to be dropped.  Attempts to receive packets are guaranteed to fail.
		/// You may send messages if the send mode allows for them to be queued.
		/// but if you close the connection before the connection is actually
		/// established, any queued messages will be discarded immediately.
		/// (We will not attempt to flush the queue and confirm delivery to the
		/// remote host, which ordinarily happens when a connection is closed.)
		Connecting = 1,

		/// Some connection types use a back channel or trusted 3rd party
		/// for earliest communication.  If the server accepts the connection,
		/// then these connections switch into the rendezvous state.  During this
		/// state, we still have not yet established an end-to-end route (through
		/// the relay network), and so if you send any messages unreliable, they
		/// are going to be discarded.
		FindingRoute = 2,

		/// We've received communications from our peer (and we know
		/// who they are) and are all good.  If you close the connection now,
		/// we will make our best effort to flush out any reliable sent data that
		/// has not been acknowledged by the peer.  (But note that this happens
		/// from within the application process, so unlike a TCP connection, you are
		/// not totally handing it off to the operating system to deal with it.)
		Connected = 3,

		/// Connection has been closed by our peer, but not closed locally.
		/// The connection still exists from an API perspective.  You must close the
		/// handle to free up resources.  If there are any messages in the inbound queue,
		/// you may retrieve them.  Otherwise, nothing may be done with the connection
		/// except to close it.
		///
		/// This stats is similar to CLOSE_WAIT in the TCP state machine.
		ClosedByPeer = 4,

		/// A disruption in the connection has been detected locally.  (E.g. timeout,
		/// local internet connection disrupted, etc.)
		///
		/// The connection still exists from an API perspective.  You must close the
		/// handle to free up resources.
		///
		/// Attempts to send further messages will fail.  Any remaining received messages
		/// in the queue are available.
		ProblemDetectedLocally = 5,

		//
		// The following values are used internally and will not be returned by any API.
		// We document them here to provide a little insight into the state machine that is used
		// under the hood.
		//

		/// We've disconnected on our side, and from an API perspective the connection is closed.
		/// No more data may be sent or received.  All reliable data has been flushed, or else
		/// we've given up and discarded it.  We do not yet know for sure that the peer knows
		/// the connection has been closed, however, so we're just hanging around so that if we do
		/// get a packet from them, we can send them the appropriate packets so that they can
		/// know why the connection was closed (and not have to rely on a timeout, which makes
		/// it appear as if something is wrong).
		FinWait = -1,

		/// We've disconnected on our side, and from an API perspective the connection is closed.
		/// No more data may be sent or received.  From a network perspective, however, on the wire,
		/// we have not yet given any indication to the peer that the connection is closed.
		/// We are in the process of flushing out the last bit of reliable data.  Once that is done,
		/// we will inform the peer that the connection has been closed, and transition to the
		/// FinWait state.
		///
		/// Note that no indication is given to the remote host that we have closed the connection,
		/// until the data has been flushed.  If the remote host attempts to send us data, we will
		/// do whatever is necessary to keep the connection alive until it can be closed properly.
		/// But in fact the data will be discarded, since there is no way for the application to
		/// read it back.  Typically this is not a problem, as application protocols that utilize
		/// the lingering functionality are designed for the remote host to wait for the response
		/// before sending any more data.
		Linger = -2,

		/// Connection is completely inactive and ready to be destroyed
		Dead = -3,

		Force32Bit = 0x7fffffff
	};
}