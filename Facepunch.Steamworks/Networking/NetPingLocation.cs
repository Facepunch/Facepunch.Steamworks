using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	/// <summary>
	///
	/// Object that describes a "location" on the Internet with sufficient
	/// detail that we can reasonably estimate an upper bound on the ping between
	/// the two hosts, even if a direct route between the hosts is not possible,
	/// and the connection must be routed through the Steam Datagram Relay network.
	/// This does not contain any information that identifies the host.  Indeed,
	/// if two hosts are in the same building or otherwise have nearly identical
	/// networking characteristics, then it's valid to use the same location
	/// object for both of them.
	///
	/// NOTE: This object should only be used in the same process!  Do not serialize it,
	/// send it over the wire, or persist it in a file or database!  If you need
	/// to do that, convert it to a string representation using the methods in
	/// ISteamNetworkingUtils().
	///
	/// </summary>
	[StructLayout( LayoutKind.Explicit, Size = 512 )]
	public struct NetPingLocation
	{
		public static NetPingLocation? TryParseFromString( string str )
		{
			var result = default( NetPingLocation );
			if ( !SteamNetworkingUtils.Internal.ParsePingLocationString( str, ref result ) )
				return null;

			return result;
		}

		public override string ToString()
		{
			SteamNetworkingUtils.Internal.ConvertPingLocationToString( ref this, out var strVal );
			return strVal;
		}

		/// Estimate the round-trip latency between two arbitrary locations, in
		/// milliseconds.  This is a conservative estimate, based on routing through
		/// the relay network.  For most basic relayed connections, this ping time
		/// will be pretty accurate, since it will be based on the route likely to
		/// be actually used.
		///
		/// If a direct IP route is used (perhaps via NAT traversal), then the route
		/// will be different, and the ping time might be better.  Or it might actually
		/// be a bit worse!  Standard IP routing is frequently suboptimal!
		///
		/// But even in this case, the estimate obtained using this method is a
		/// reasonable upper bound on the ping time.  (Also it has the advantage
		/// of returning immediately and not sending any packets.)
		///
		/// In a few cases we might not able to estimate the route.  In this case
		/// a negative value is returned.  k_nSteamNetworkingPing_Failed means
		/// the reason was because of some networking difficulty.  (Failure to
		/// ping, etc)  k_nSteamNetworkingPing_Unknown is returned if we cannot
		/// currently answer the question for some other reason.
		///
		/// Do you need to be able to do this from a backend/matchmaking server?
		/// You are looking for the "ticketgen" library.
		public int EstimatePingTo( NetPingLocation target )
		{
			return SteamNetworkingUtils.Internal.EstimatePingTimeBetweenTwoLocations( ref this, ref target );
		}
	}
}