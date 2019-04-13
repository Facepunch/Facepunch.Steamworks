using System;

namespace Steamworks
{
	public interface ISteamCallback
	{
		int GetCallbackId();
		int GetStructSize();
		ISteamCallback Fill( IntPtr ptr, int size );
	}
}