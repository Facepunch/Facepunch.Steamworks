using System.Runtime.InteropServices;

namespace Steamworks
{
	internal static class Platform
    {
#if PLATFORM_WIN64
		public const int StructPlatformPackSize = 8;
		public const string LibraryName = "steam_api64";
#elif PLATFORM_WIN32
		public const int StructPlatformPackSize = 8;
		public const string LibraryName = "steam_api";
#elif PLATFORM_POSIX64 || PLATFORM_POSIX32
		public const int StructPlatformPackSize = 4;
		public const string LibraryName = "libsteam_api";
#elif  PLATFORM_OSX64
		public const int StructPlatformPackSize = 8;
		public const string LibraryName = "libsteam_api";
#else
		public const int StructPlatformPackSize = 8;
		public const string LibraryName = "steam_api";
#endif

		public const CallingConvention CC = CallingConvention.Cdecl;
		public const int StructPackSize = 4;
	}
}
