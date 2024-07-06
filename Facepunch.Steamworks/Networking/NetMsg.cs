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
		internal GCHandle64 DataHandle;
		internal ushort IdxLane;
		internal ushort _pad1__;

		
	}

	[StructLayout( LayoutKind.Sequential, Size = 4 )]
	internal struct GCHandle64 {
		public GCHandle value;

		public GCHandle64( GCHandle handle ) : this() => value = handle;

		public static implicit operator GCHandle(GCHandle64 handle) => handle.value;
		public static implicit operator GCHandle64(GCHandle handle) => new(handle);
	}
}

