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
	}
}