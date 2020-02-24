using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	[StructLayout( LayoutKind.Sequential, Pack = Platform.StructPackSize )]
	internal partial struct MatchMakingKeyValuePair
	{
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
		internal string Key;

		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
		internal string Value;
	}
	
}
