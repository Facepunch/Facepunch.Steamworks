using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Steamworks.Data
{
	delegate void FSteamNetworkingSocketsDebugOutput (SteamNetworkingSocketsDebugOutputType nType, string pszMsg );

	public struct SteamNetworkingMicroseconds
	{
		public long Value;

		public static implicit operator SteamNetworkingMicroseconds( long value )
		{
			return new SteamNetworkingMicroseconds { Value = value };
		}

		public static implicit operator long( SteamNetworkingMicroseconds value )
		{
			return value.Value;
		}

		public override string ToString() => Value.ToString();
	}

	public struct SteamNetworkingPOPID
	{
		public uint Value;

		public static implicit operator SteamNetworkingPOPID( uint value )
		{
			return new SteamNetworkingPOPID { Value = value };
		}

		public static implicit operator uint( SteamNetworkingPOPID value )
		{
			return value.Value;
		}

		public override string ToString() => Value.ToString();
	}

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
	public struct PingLocation
	{
		[MarshalAs( UnmanagedType.ByValArray, SizeConst = 512, ArraySubType = UnmanagedType.U8 )]
		public ushort[] Data;

		public static PingLocation? TryParseFromString( string str )
		{
			var result = default( PingLocation );
			if ( !SteamNetworkingUtils.Internal.ParsePingLocationString( str, ref result ) )
				return null;

			return result;
		}


		public override string ToString()
		{
			var sb = Helpers.TakeStringBuilder();
			SteamNetworkingUtils.Internal.ConvertPingLocationToString( ref this, sb, sb.Capacity );
			return sb.ToString();
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
		public int EstimatePingTo( PingLocation target )
		{
			return SteamNetworkingUtils.Internal.EstimatePingTimeBetweenTwoLocations( ref this, ref target );
		}
	}
}