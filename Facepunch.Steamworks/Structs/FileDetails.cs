using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;


namespace Steamworks.Data
{
	/// <summary>
	/// Represents details of a file.
	/// </summary>
	public struct FileDetails
	{
		/// <summary>
		/// The size of the file in bytes.
		/// </summary>
		public ulong SizeInBytes;
		public string Sha1;
		public uint Flags;
	}
}
