using Steamworks.Data;
using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	[UnmanagedFunctionPointer( Platform.CC )]
	delegate void NetDebugFunc( [In] NetDebugOutput nType, [In] IntPtr pszMsg );
}