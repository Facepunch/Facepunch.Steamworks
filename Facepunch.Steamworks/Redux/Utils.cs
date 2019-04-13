using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using SteamNative;

namespace Steamworks
{
	/// <summary>
	/// Interface which provides access to a range of miscellaneous utility functions
	/// </summary>
	public static class Utils
	{
		static Internal.ISteamUtils _steamutils;
		internal static Internal.ISteamUtils steamutils
		{
			get
			{
				if ( _steamutils == null )
					_steamutils = new Internal.ISteamUtils();

				return _steamutils;
			}
		}


		public static uint SecondsSinceAppActive => _steamutils.GetSecondsSinceAppActive();

		public static uint SecondsSinceComputerActive => _steamutils.GetSecondsSinceComputerActive();

		// the universe this client is connecting to
		public static Universe ConnectedUniverse => _steamutils.GetConnectedUniverse();

		/// <summary>
		/// Steam server time.  Number of seconds since January 1, 1970, GMT (i.e unix time)
		/// </summary>
		public static DateTime SteamServerTime => Facepunch.Steamworks.Utility.Epoch.ToDateTime( _steamutils.GetServerRealTime() );

		/// <summary>
		/// returns the 2 digit ISO 3166-1-alpha-2 format country code this client is running in (as looked up via an IP-to-location database)
		/// e.g "US" or "UK".
		/// </summary>
		public static string IpCountry => _steamutils.GetIPCountry();

		/// <summary>
		/// returns true if the image exists, and the buffer was successfully filled out
		/// results are returned in RGBA format
		/// the destination buffer size should be 4 * height * width * sizeof(char)
		/// </summary>
		public static bool GetImageSize( int image, out uint width, out uint height )
		{
			width = 0;
			height = 0;
			return _steamutils.GetImageSize( image, ref width, ref height );
		}

		internal static bool IsCallComplete( SteamAPICall_t call, out bool failed )
		{
			failed = false;
			return steamutils.IsAPICallCompleted( call, ref failed );
		}

		internal static T? GetResult<T>( SteamAPICall_t call ) where T : struct, ISteamCallback
		{
			var t = new T();

			var size = t.GetStructSize();
			var ptr = Marshal.AllocHGlobal( size );

			try
			{
				bool failed = false;

				if ( !steamutils.GetAPICallResult( call, ptr, size, t.GetCallbackId(), ref failed ) )
					return null;

				if ( failed )
					return null;

				t = (T)t.Fill( ptr );

				return t;
			}
			finally
			{
				Marshal.FreeHGlobal( ptr );
			}
		}
	}
}