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

		public bool IsComplete()
		{
			return Utils.IsCallComplete( CallHandle, out bool failed );
		}

		public T? GetResult()
		{
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