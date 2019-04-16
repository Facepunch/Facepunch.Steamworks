using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;

namespace Steamworks
{
	[StructLayout( LayoutKind.Sequential )]
    internal partial class Callback
    {
        internal enum Flags : byte
        {
            Registered = 0x01,
            GameServer = 0x02
        }

        public IntPtr vTablePtr;
        public byte CallbackFlags;
        public int CallbackId;
    };
}
