using Steamworks.Data;
using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	[StructLayout( LayoutKind.Sequential )]
	internal struct NetMsg
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

		internal delegate void ReleaseDelegate( IntPtr msg );

		public void Release( IntPtr data )
		{
			//
			// I think this function might be a static global, so we could probably
			// cache this release call.
			//
			var d = Marshal.GetDelegateForFunctionPointer<ReleaseDelegate>( ReleasePtr );
			d( data );
		}
	}
}