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
	public class SteamNetworkingUtils : SteamSharedClass<SteamNetworkingUtils>
	{
		internal static ISteamNetworkingUtils Internal => Interface as ISteamNetworkingUtils;

		internal override void InitializeInterface( bool server )
		{
			SetInterface( server, new ISteamNetworkingUtils( server ) );
			InstallCallbacks( server );
		}

		static void InstallCallbacks( bool server )
		{
			Dispatch.Install<SteamRelayNetworkStatus_t>( x =>
			{
				Status = x.Avail;
			}, server );
		}

		/// <summary>
		/// A function to receive debug network information on. This will do nothing
		/// unless you set DebugLevel to something other than None.
		/// 
		/// You should set this to an appropriate level instead of setting it to the highest
		/// and then filtering it by hand because a lot of energy is used by creating the strings
		/// and your frame rate will tank and you won't know why.
		/// </summary>

		public static event Action<NetDebugOutput, string> OnDebugOutput;

		/// <summary>
		/// The latest available status gathered from the SteamRelayNetworkStatus callback
		/// </summary>
		public static SteamNetworkingAvailability Status { get; private set; }

		/// <summary>
		/// If you know that you are going to be using the relay network (for example,
		/// because you anticipate making P2P connections), call this to initialize the
		/// relay network.  If you do not call this, the initialization will
		/// be delayed until the first time you use a feature that requires access
		/// to the relay network, which will delay that first access.
		///
		/// You can also call this to force a retry if the previous attempt has failed.
		/// Performing any action that requires access to the relay network will also
		/// trigger a retry, and so calling this function is never strictly necessary,
		/// but it can be useful to call it a program launch time, if access to the
		/// relay network is anticipated.
		///
		/// Use GetRelayNetworkStatus or listen for SteamRelayNetworkStatus_t
		/// callbacks to know when initialization has completed.
		/// Typically initialization completes in a few seconds.
		///
		/// Note: dedicated servers hosted in known data centers do *not* need
		/// to call this, since they do not make routing decisions.  However, if
		/// the dedicated server will be using P2P functionality, it will act as
		/// a "client" and this should be called.
		/// </summary>
		public static void InitRelayNetworkAccess()
		{
			Internal.InitRelayNetworkAccess();
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
		public static NetPingLocation? LocalPingLocation
		{
			get
			{
				NetPingLocation location = default;
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
		public static int EstimatePingTo( NetPingLocation target )
		{
			return Internal.EstimatePingTimeFromLocalHost( ref target );
		}

		/// <summary>
		/// If you need ping information straight away, wait on this. It will return
		/// immediately if you already have up to date ping data
		/// </summary>
		public static async Task WaitForPingDataAsync( float maxAgeInSeconds = 60 * 5 )
		{
			if ( Internal.CheckPingDataUpToDate( maxAgeInSeconds ) )
				return;

			SteamRelayNetworkStatus_t status = default;

			while ( Internal.GetRelayNetworkStatus( ref status ) != SteamNetworkingAvailability.Current )
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

		/// <summary>
		/// Timeout value (in ms) to use when first connecting
		/// </summary>
		public static int ConnectionTimeout
		{
			get => GetConfigInt( NetConfig.TimeoutInitial );
			set => SetConfigInt( NetConfig.TimeoutInitial, value );
		}

		/// <summary>
		/// Timeout value (in ms) to use after connection is established
		/// </summary>
		public static int Timeout
		{
			get => GetConfigInt( NetConfig.TimeoutConnected );
			set => SetConfigInt( NetConfig.TimeoutConnected, value );
		}

		/// <summary>
		/// Upper limit of buffered pending bytes to be sent.
		/// If this is reached SendMessage will return LimitExceeded.
		/// Default is 524288 bytes (512k)
		/// </summary>
		public static int SendBufferSize
		{
			get => GetConfigInt( NetConfig.SendBufferSize );
			set => SetConfigInt( NetConfig.SendBufferSize, value );
		}


		/// <summary>
		/// Get Debug Information via OnDebugOutput event
		/// 
		/// Except when debugging, you should only use NetDebugOutput.Msg
		/// or NetDebugOutput.Warning.  For best performance, do NOT
		/// request a high detail level and then filter out messages in the callback.  
		/// 
		/// This incurs all of the expense of formatting the messages, which are then discarded.  
		/// Setting a high priority value (low numeric value) here allows the library to avoid 
		/// doing this work.
		/// </summary>
		public static NetDebugOutput DebugLevel
		{
			get => _debugLevel;
			set
			{
				_debugLevel = value;
				_debugFunc = new NetDebugFunc( OnDebugMessage );

				Internal.SetDebugOutputFunction( value, _debugFunc );
			}
		}

		/// <summary>
		/// So we can remember and provide a Get for DebugLEvel
		/// </summary>
		private static NetDebugOutput _debugLevel;

		/// <summary>
		/// We need to keep the delegate around until it's not used anymore
		/// </summary>
		static NetDebugFunc _debugFunc;

		struct DebugMessage
		{
			public NetDebugOutput Type;
			public string Msg;
		}

		private static System.Collections.Concurrent.ConcurrentQueue<DebugMessage> debugMessages = new System.Collections.Concurrent.ConcurrentQueue<DebugMessage>();

		/// <summary>
		/// This can be called from other threads - so we're going to queue these up and process them in a safe place.
		/// </summary>
		[MonoPInvokeCallback]
		private static void OnDebugMessage( NetDebugOutput nType, IntPtr str )
		{
			debugMessages.Enqueue( new DebugMessage { Type = nType, Msg = Helpers.MemoryToString( str ) } );
		}

		/// <summary>
		/// Called regularly from the Dispatch loop so we can provide a timely
		/// stream of messages.
		/// </summary>
		internal static void OutputDebugMessages()
		{
			if ( debugMessages.IsEmpty )
				return;

			while ( debugMessages.TryDequeue( out var result ) )
			{
				OnDebugOutput?.Invoke( result.Type, result.Msg );
			}
		}

		#region Config Internals

		internal unsafe static bool SetConfigInt( NetConfig type, int value )
		{
			int* ptr = &value;
			return Internal.SetConfigValue( type, NetConfigScope.Global, IntPtr.Zero, NetConfigType.Int32, (IntPtr)ptr );
		}

		internal unsafe static int GetConfigInt( NetConfig type )
		{
			int value = 0;
			NetConfigType dtype = NetConfigType.Int32;
			int* ptr = &value;
			UIntPtr size = new UIntPtr( sizeof( int ) );
			var result = Internal.GetConfigValue( type, NetConfigScope.Global, IntPtr.Zero, ref dtype, (IntPtr) ptr, ref size );
			if ( result != NetConfigResult.OK )
				return 0;

			return value;
		}

		internal unsafe static bool SetConfigFloat( NetConfig type, float value )
		{
			float* ptr = &value;
			return Internal.SetConfigValue( type, NetConfigScope.Global, IntPtr.Zero, NetConfigType.Float, (IntPtr)ptr );
		}

		internal unsafe static float GetConfigFloat( NetConfig type )
		{
			float value = 0;
			NetConfigType dtype = NetConfigType.Float;
			float* ptr = &value;
			UIntPtr size = new UIntPtr( sizeof( float ) );
			var result = Internal.GetConfigValue( type, NetConfigScope.Global, IntPtr.Zero, ref dtype, (IntPtr)ptr, ref size );
			if ( result != NetConfigResult.OK )
				return 0;

			return value;
		}

		internal unsafe static bool SetConfigString( NetConfig type, string value )
		{
			var bytes = Encoding.UTF8.GetBytes( value );

			fixed ( byte* ptr = bytes )
			{
				return Internal.SetConfigValue( type, NetConfigScope.Global, IntPtr.Zero, NetConfigType.String, (IntPtr)ptr );
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
