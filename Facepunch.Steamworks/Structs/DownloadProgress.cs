using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;


namespace Steamworks.Data
{
	public struct DownloadProgress
	{
		public bool Active;
		public ulong BytesDownloaded;
		public ulong BytesTotal;
	}
}