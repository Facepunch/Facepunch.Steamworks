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

	public struct HSteamListenSocket
	{
		public uint Value;

		public static implicit operator HSteamListenSocket( uint value )
		{
			return new HSteamListenSocket { Value = value };
		}

		public static implicit operator uint( HSteamListenSocket value )
		{
			return value.Value;
		}

		public override string ToString() => Value.ToString();
	}

	public struct HSteamNetConnection
	{
		public uint Value;

		public static implicit operator HSteamNetConnection( uint value )
		{
			return new HSteamNetConnection { Value = value };
		}

		public static implicit operator uint( HSteamNetConnection value )
		{
			return value.Value;
		}

		public override string ToString() => Value.ToString();
	}

	public enum IdentityType
	{
		Invalid = 0,
		IPAddress = 1,
		GenericString = 2,
		GenericBytes = 3,
		SteamID = 16
	}

	[StructLayout( LayoutKind.Explicit, Size = 136 )]
	public struct SteamNetworkingIdentity
	{
		[FieldOffset( 0 )]
		public IdentityType type;
		/*
		public bool IsInvalid
		{
			get
			{
				return Native.SteamAPI_SteamNetworkingIdentity_IsInvalid( this );
			}
		}

		public ulong GetSteamID()
		{
			return Native.SteamAPI_SteamNetworkingIdentity_GetSteamID64( this );
		}

		public void SetSteamID( ulong steamID )
		{
			Native.SteamAPI_SteamNetworkingIdentity_SetSteamID64( ref this, steamID );
		}

		public bool EqualsTo( NetworkingIdentity identity )
		{
			return Native.SteamAPI_SteamNetworkingIdentity_EqualTo( this, identity );
		}*/
	}

	[StructLayout( LayoutKind.Sequential )]
	public struct SteamNetworkingIPAddr
	{
		[MarshalAs( UnmanagedType.ByValArray, SizeConst = 16 )]
		public byte[] ip;
		public ushort port;
		/*
		public bool IsLocalHost
		{
			get
			{
				return Native.SteamAPI_SteamNetworkingIPAddr_IsLocalHost( ref this );
			}
		}

		public string GetIP()
		{
			return ip.ParseIP();
		}

		public void SetLocalHost( ushort port )
		{
			Native.SteamAPI_SteamNetworkingIPAddr_SetIPv6LocalHost( ref this, port );
		}

		public void SetAddress( string ip, ushort port )
		{
			if ( !ip.Contains( ":" ) )
				Native.SteamAPI_SteamNetworkingIPAddr_SetIPv4( ref this, ip.ParseIPv4(), port );
			else
				Native.SteamAPI_SteamNetworkingIPAddr_SetIPv6( ref this, ip.ParseIPv6(), port );
		}*/
	}

	[StructLayout( LayoutKind.Sequential )]
	public struct SteamNetworkingMessage_t
	{
		public IntPtr data;
		public int length;
		public HSteamNetConnection connection;
		public SteamNetworkingIdentity identity;
		public long userData;
		public SteamNetworkingMicroseconds timeReceived;
		public long messageNumber;
		internal IntPtr release;
		public int channel;
		private int pad;
		/*
		public void CopyTo( byte[] destination )
		{
			if ( destination == null )
				throw new ArgumentNullException( "destination" );

			Marshal.Copy( data, destination, 0, length );
		}

		public void Destroy()
		{
			if ( release == IntPtr.Zero )
				throw new InvalidOperationException( "Message not created" );

			Native.SteamAPI_SteamNetworkingMessage_t_Release( release );
		}*/
	}

	[StructLayout( LayoutKind.Sequential )]
	public struct SteamNetConnectionInfo_t
	{
		public SteamNetworkingIdentity identity;
		public long userData;
		public HSteamListenSocket listenSocket;
		public SteamNetworkingIPAddr address;
		private ushort pad;
		private SteamNetworkingPOPID popRemote;
		private SteamNetworkingPOPID popRelay;
		public SteamNetworkingConnectionState state;
		public int endReason;
		[MarshalAs( UnmanagedType.ByValTStr, SizeConst = 128 )]
		public string endDebug;
		[MarshalAs( UnmanagedType.ByValTStr, SizeConst = 128 )]
		public string connectionDescription;
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

	[StructLayout( LayoutKind.Sequential )]
	public struct SteamNetworkingQuickConnectionStatus
	{
		public SteamNetworkingConnectionState state;
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
		public SteamNetworkingMicroseconds queueTime;

		[MarshalAs( UnmanagedType.ByValArray, SizeConst = 16 )]
		uint[] reserved;
	}

	struct SteamDatagramRelayAuthTicket
	{
		// Not implemented
	};

	struct SteamDatagramHostedAddress
	{
		// Not implemented
	}
}