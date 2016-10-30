using System;
using System.Runtime.InteropServices;
using Facepunch.Steamworks;

namespace SteamNative
{
    [StructLayout( LayoutKind.Sequential )]
    internal class Callback
    {
        internal enum Flags : byte
        {
            Registered = 0x01,
            GameServer = 0x02
        }

        public IntPtr vTablePtr;
        public byte CallbackFlags;
        public int CallbackId;

        [StructLayout( LayoutKind.Sequential, Pack = 1 )]
        public class VTable
        {
            public IntPtr ResultA;
            public IntPtr ResultB;
            public IntPtr GetSize;
        }

        //
        // All possible functions
        //

        [UnmanagedFunctionPointer( CallingConvention.ThisCall )]    public delegate void Result( IntPtr thisptr, IntPtr pvParam );
        [UnmanagedFunctionPointer( CallingConvention.ThisCall )]    public delegate void ResultWithInfo( IntPtr thisptr, IntPtr pvParam, bool bIOFailure, SteamNative.SteamAPICall_t hSteamAPICall );
        [UnmanagedFunctionPointer( CallingConvention.ThisCall )]    public delegate int GetSize( IntPtr thisptr );

        //
        // Created on registration of a callback
        //
        public class Handle : IDisposable
        {
            public GCHandle FuncA;
            public GCHandle FuncB;
            public GCHandle FuncC;

            public IntPtr vTablePtr;

            public GCHandle PinnedCallback;

            public void Dispose()
            {
                if ( FuncA.IsAllocated )
                    FuncA.Free();

                if ( FuncB.IsAllocated )
                    FuncB.Free();

                if ( FuncC.IsAllocated )
                    FuncC.Free();

                if ( PinnedCallback.IsAllocated )
                    PinnedCallback.Free();

                if ( vTablePtr  != IntPtr.Zero )
                {
                    Marshal.FreeHGlobal( vTablePtr );
                    vTablePtr = IntPtr.Zero;
                }
            }

            internal void Remove( BaseSteamworks baseSteamworks )
            {
                if ( PinnedCallback.IsAllocated )
                {
                    baseSteamworks.native.api.SteamAPI_UnregisterCallback( PinnedCallback.AddrOfPinnedObject() );
                }

                Dispose();
            }
        }
    };


}
