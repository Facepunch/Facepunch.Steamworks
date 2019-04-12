using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;


namespace Steamworks
{
	public struct FileDetails
	{
		public ulong SizeInBytes;
		public string Sha1;
		public ulong BytesTotal;
	}
}