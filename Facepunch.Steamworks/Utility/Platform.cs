using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;

namespace Steamworks
{
#if !RADISH_SDK
	internal static class Platform
    {
#if PLATFORM_WIN64
		public const int StructPlatformPackSize = 8;
		public const string LibraryName = "steam_api64";
#elif PLATFORM_WIN32
		public const int StructPlatformPackSize = 8;
		public const string LibraryName = "steam_api";
#elif PLATFORM_POSIX
		public const int StructPlatformPackSize = 4;
		public const string LibraryName = "libsteam_api";
#endif

		public const CallingConvention CC = CallingConvention.Cdecl;
		public const int StructPackSize = 4;
	}
#endif
}
