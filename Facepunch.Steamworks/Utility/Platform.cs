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
#else
		public const int StructPlatformPackSize = 4;
#endif

		public const int StructPackSize = 4;
	}
}
