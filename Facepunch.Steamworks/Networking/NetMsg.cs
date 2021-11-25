using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	[StructLayout( LayoutKind.Sequential )]
	internal partial struct NetMsg
	{
		internal IntPtr DataPtr;
		internal int DataSize;
		internal Connection Connection;
		internal NetIdentity Identity;
		internal long ConnectionUserData;
		internal long RecvTime;
		internal long MessageNumber;
		internal IntPtr FreeDataPtr;
		internal IntPtr ReleasePtr;
		internal int Channel;
		internal SendType Flags;
		internal long UserData;
		internal ushort IdxLane;
		internal ushort _pad1__;
	}
}
