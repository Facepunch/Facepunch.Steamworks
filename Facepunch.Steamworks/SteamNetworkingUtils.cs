using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Steamworks.Data;

namespace Steamworks
{
	/// <summary>
	/// Undocumented Parental Settings
	/// </summary>
	public static class SteamNetworkingUtils
	{
		static ISteamNetworkingUtils _internal;
		internal static ISteamNetworkingUtils Internal
		{
			get
			{
				if ( _internal == null )
				{
					_internal = new ISteamNetworkingUtils();
					_internal.InitUserless();
				}

				return _internal;
			}
		}

		internal static void Shutdown()
		{
			_internal = null;
		}

		/// <summary>
		/// Return location info for the current host.
		///
		/// It takes a few seconds to initialize access to the relay network.  If
		/// you call this very soon after startup the data may not be available yet.
		///
		/// This always return the most up-to-date information we have available
		/// right now, even if we are in the middle of re-calculating ping times.
		/// </summary>
		public static PingLocation? LocalPingLocation
		{
			get
			{
				PingLocation location = default;
				var age = Internal.GetLocalPingLocation( ref location );
				if ( age < 0 )
					return null;

				return location;
			}
		}

		/// <summary>
		/// Same as PingLocation.EstimatePingTo, but assumes that one location is the local host.
		/// This is a bit faster, especially if you need to calculate a bunch of
		/// these in a loop to find the fastest one.
		/// </summary>
		public static int EstimatePingTo( PingLocation target )
		{
			return Internal.EstimatePingTimeFromLocalHost( ref target );
		}

		/// <summary>
		/// If you need ping information straight away, wait on this. It will return
		/// immediately if you already have up to date ping data
		/// </summary>
		public static async Task WaitForPingDataAsync( float maxAgeInSeconds = 60 * 5 )
		{
			if ( Internal.CheckPingDataUpToDate( 60.0f ) )
				return;

			while ( Internal.IsPingMeasurementInProgress() )
			{
				await Task.Delay( 10 );
			}
		}

		public static long LocalTimestamp => Internal.GetLocalTimestamp();


		/// <summary>
		/// [0 - 100] - Randomly discard N pct of packets
		/// </summary>
		public static float FakeSendPacketLoss
		{
			get => GetConfigFloat( NetConfig.FakePacketLoss_Send );
			set => SetConfigFloat( NetConfig.FakePacketLoss_Send, value );
		}

		/// <summary>
		/// [0 - 100] - Randomly discard N pct of packets 
		/// </summary>
		public static float FakeRecvPacketLoss
		{
			get => GetConfigFloat( NetConfig.FakePacketLoss_Recv );
			set => SetConfigFloat( NetConfig.FakePacketLoss_Recv, value );
		}

		/// <summary>
		/// Delay all packets by N ms 
		/// </summary>
		public static float FakeSendPacketLag
		{
			get => GetConfigFloat( NetConfig.FakePacketLag_Send );
			set => SetConfigFloat( NetConfig.FakePacketLag_Send, value );
		}

		/// <summary>
		/// Delay all packets by N ms 
		/// </summary>
		public static float FakeRecvPacketLag
		{
			get => GetConfigFloat( NetConfig.FakePacketLag_Recv );
			set => SetConfigFloat( NetConfig.FakePacketLag_Recv, value );
		}

		#region Config Internals

		internal unsafe static bool GetConfigInt( NetConfig type, int value )
		{
			int* ptr = &value;
			return Internal.SetConfigValue( type, NetScope.Global, 0, NetConfigType.Int32, (IntPtr)ptr );
		}

		internal unsafe static int GetConfigInt( NetConfig type )
		{
			int value = 0;
			NetConfigType dtype = NetConfigType.Int32;
			int* ptr = &value;
			ulong size = sizeof( int );
			var result = Internal.GetConfigValue( type, NetScope.Global, 0, ref dtype, (IntPtr) ptr, ref size );
			if ( result != NetConfigResult.OK )
				return 0;

			return value;
		}

		internal unsafe static bool SetConfigFloat( NetConfig type, float value )
		{
			float* ptr = &value;
			return Internal.SetConfigValue( type, NetScope.Global, 0, NetConfigType.Float, (IntPtr)ptr );
		}

		internal unsafe static float GetConfigFloat( NetConfig type )
		{
			float value = 0;
			NetConfigType dtype = NetConfigType.Float;
			float* ptr = &value;
			ulong size = sizeof( float );
			var result = Internal.GetConfigValue( type, NetScope.Global, 0, ref dtype, (IntPtr)ptr, ref size );
			if ( result != NetConfigResult.OK )
				return 0;

			return value;
		}

		internal unsafe static bool SetConfigString( NetConfig type, string value )
		{
			var bytes = Encoding.UTF8.GetBytes( value );

			fixed ( byte* ptr = bytes )
			{
				return Internal.SetConfigValue( type, NetScope.Global, 0, NetConfigType.String, (IntPtr)ptr );
			}
		}

		/*
		internal unsafe static float GetConfigString( NetConfig type )
		{

			float value = 0;
			NetConfigType dtype = NetConfigType.Float;
			float* ptr = &value;
			ulong size = sizeof( float );
			var result = Internal.GetConfigValue( type, NetScope.Global, 0, ref dtype, (IntPtr)ptr, ref size );
			if ( result != SteamNetworkingGetConfigValueResult.OK )
				return 0;

			return value;
		}
		*/


		/*

		TODO - Connection object

		internal unsafe static bool SetConnectionConfig( uint con, NetConfig type, int value )
		{
			int* ptr = &value;
			return Internal.SetConfigValue( type, NetScope.Connection, con, NetConfigType.Int32, (IntPtr)ptr );
		}

		internal unsafe static bool SetConnectionConfig( uint con, NetConfig type, float value )
		{
			float* ptr = &value;
			return Internal.SetConfigValue( type, NetScope.Connection, con, NetConfigType.Float, (IntPtr)ptr );
		}

		internal unsafe static bool SetConnectionConfig( uint con, NetConfig type, string value )
		{
			var bytes = Encoding.UTF8.GetBytes( value );

			fixed ( byte* ptr = bytes )
			{
				return Internal.SetConfigValue( type, NetScope.Connection, con, NetConfigType.String, (IntPtr)ptr );
			}
		}*/

		#endregion
	}
}