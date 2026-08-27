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

		/// <summary>
		/// Only <see cref="Steamworks.Data.MatchMakingKeyValuePair"/> uses this now, and only
		/// because it always has - that struct is two <c>char[256]</c> arrays, so it measures
		/// 512 bytes at any pack value.
		/// </summary>
		/// <remarks>
		/// This used to be applied to 26 generated structs as a way of expressing that
		/// <c>CSteamID</c>/<c>CGameID</c> are 1-aligned (Valve declares them inside
		/// <c>#pragma pack( push, 1 )</c>, <c>steamclientpublic.h:475</c>). Packing the whole
		/// struct down to 4 cannot say that - it also drags genuine <c>uint64</c> members off
		/// their natural boundary - and it mis-laid-out 16 structs. The alignment now lives on
		/// the field's type instead, see <see cref="Steamworks.Data.PackedId"/>, and every
		/// generated struct carries <see cref="StructPlatformPackSize"/>: the value the header
		/// itself declares.
		/// </remarks>
		public const int StructPackSize = 4;
	}
}
