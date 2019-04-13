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

				t = (T)t.Fill( ptr, size );

				return t;
			}
			finally
			{
				Marshal.FreeHGlobal( ptr );
			}
		}
	}
}