using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;


namespace Steamworks
{
	public struct FileDetails
	{
		public bool Found;
		public ulong SizeInBytes;
		public string Sha1;
		public uint Flags;
	}
}