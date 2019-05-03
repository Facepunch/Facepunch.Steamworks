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

	[StructLayout( LayoutKind.Sequential )]
	public struct SteamNetworkingMessage_t
	{
		public IntPtr data;
		public int length;
		public NetConnection connection;
		public NetworkIdentity identity;
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