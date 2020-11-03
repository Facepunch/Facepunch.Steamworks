using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;

namespace Steamworks
{
	internal static class Platform
    {
#if PLATFORM_WIN64 || ((UNITY_STANDALONE_WIN || UNITY_EDITOR_WIN) && UNITY_64) 
		public const int StructPlatformPackSize = 8;
		public const string LibraryName = "steam_api64";
#elif PLATFORM_WIN32 || UNITY_STANDALONE_WIN || UNITY_EDITOR_WIN 
		public const int StructPlatformPackSize = 8;
		public const string LibraryName = "steam_api";
#elif PLATFORM_POSIX || UNITY_EDITOR_LINUX || UNITY_STANDALONE_LINUX
		public const int StructPlatformPackSize = 4;
		public const string LibraryName = "libsteam_api";
#endif

		public const CallingConvention CC = CallingConvention.Cdecl;
		public const int StructPackSize = 4;
	}
}
