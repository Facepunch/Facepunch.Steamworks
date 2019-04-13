using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using SteamNative;

namespace Steamworks
{
	public struct SteamApiCallback<T> where T : struct, ISteamCallback
	{
		public ulong CallHandle;

		public SteamApiCallback( ulong callbackHandle )
		{
			CallHandle = callbackHandle;
		}

		public bool IsComplete( out bool failed )
		{
			return Utils.IsCallComplete( CallHandle, out failed );
		}

		public async Task<T?> GetResult()
		{
			bool failed = false;

			while ( !IsComplete( out failed ) )
			{
				await Task.Delay( 1 );
			}

			if ( failed )
				return null;

			return Utils.GetResult<T>( CallHandle );
		}
	}

	public interface ISteamCallback
	{
		int GetCallbackId();
		int GetStructSize();
		ISteamCallback Fill( IntPtr ptr, int size );
	}
}