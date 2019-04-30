using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;


namespace Steamworks.Data
{
	public struct FileDetails
	{
		public ulong SizeInBytes;
		public string Sha1;
		public uint Flags;
	}
}