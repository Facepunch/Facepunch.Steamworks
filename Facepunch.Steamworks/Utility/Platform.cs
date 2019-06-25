using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace Steamworks
{
	internal static class Platform
    {
#if PLATFORM_WIN64
		public const int StructPlatformPackSize = 8;
		public const string LibraryName = "steam_api64";
#elif PLATFORM_WIN32
		public const int StructPlatformPackSize = 4;
		public const string LibraryName = "steam_api";
#elif PLATFORM_POSIX32
		public const int StructPlatformPackSize = 4;
		public const string LibraryName = "libsteam_api";
#elif PLATFORM_POSIX64
		public const int StructPlatformPackSize = 4;
		public const string LibraryName = "libsteam_api";
#endif

		public const int StructPackSize = 4;


		public static int MemoryOffset( int memLocation )
		{
#if PLATFORM_WIN64 || PLATFORM_POSIX64
			return memLocation;
#else
			return memLocation / 2;
#endif
		}
	}
}
