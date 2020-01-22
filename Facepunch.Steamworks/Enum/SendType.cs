using System;

namespace Steamworks.Data
{
	[Flags]
	public enum SendType : int
	{
		/// <summary>
		/// Send the message unreliably. Can be lost.  Messages *can* be larger than a
		/// single MTU (UDP packet), but there is no retransmission, so if any piece
		/// of the message is lost, the entire message will be dropped.
		///
		/// The sending API does have some knowledge of the underlying connection, so
		/// if there is no NAT-traversal accomplished or there is a recognized adjustment
		/// happening on the connection, the packet will be batched until the connection
		/// is open again.
		/// </summary>
		Unreliable = 0,

		/// <summary>
		/// Disable Nagle's algorithm.
		/// By default, Nagle's algorithm is applied to all outbound messages.  This means
		/// that the message will NOT be sent immediately, in case further messages are
		/// sent soon after you send this, which can be grouped together.  Any time there
		/// is enough buffered data to fill a packet, the packets will be pushed out immediately,
		/// but partially-full packets not be sent until the Nagle timer expires. 
		/// </summary>
		NoNagle = 1 << 0,

		/// <summary>
		/// If the message cannot be sent very soon (because the connection is still doing some initial
		/// handshaking, route negotiations, etc), then just drop it.  This is only applicable for unreliable
		/// messages.  Using this flag on reliable messages is invalid.
		/// </summary>
		NoDelay = 1 << 2,

		/// Reliable message send. Can send up to 0.5mb in a single message. 
		/// Does fragmentation/re-assembly of messages under the hood, as well as a sliding window for
		/// efficient sends of large chunks of data.
		Reliable = 1 << 3
	}
}
