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
        internal class ThisCall
        {
            [UnmanagedFunctionPointer( CallingConvention.StdCall )]    public delegate void Result( IntPtr thisptr, IntPtr pvParam );
            [UnmanagedFunctionPointer( CallingConvention.StdCall )]    public delegate void ResultWithInfo( IntPtr thisptr, IntPtr pvParam, bool bIOFailure, SteamNative.SteamAPICall_t hSteamAPICall );
            [UnmanagedFunctionPointer( CallingConvention.StdCall )]    public delegate int GetSize( IntPtr thisptr );
        }


    };

    //
    // Created on registration of a callback
    //
    internal class CallbackHandle : IDisposable
    {
        internal BaseSteamworks steamworks;
        internal SteamAPICall_t CallResultHandle;
        internal bool CallResult;
        internal GCHandle FuncA;
        internal GCHandle FuncB;
        internal GCHandle FuncC;
        internal IntPtr vTablePtr;
        internal GCHandle PinnedCallback;

        public void Dispose()
        {
            if ( CallResult )
                UnregisterCallResult();
            else
                UnregisterCallback();

            if ( FuncA.IsAllocated )
                FuncA.Free();

            if ( FuncB.IsAllocated )
                FuncB.Free();

            if ( FuncC.IsAllocated )
                FuncC.Free();

            if ( PinnedCallback.IsAllocated )
                PinnedCallback.Free();

            if ( vTablePtr != IntPtr.Zero )
            {
                Marshal.FreeHGlobal( vTablePtr );
                vTablePtr = IntPtr.Zero;
            }
        }

        private void UnregisterCallback()
        {
            if ( !PinnedCallback.IsAllocated )
                return;

            steamworks.native.api.SteamAPI_UnregisterCallback( PinnedCallback.AddrOfPinnedObject() );
        }

        private void UnregisterCallResult()
        {
            if ( CallResultHandle == 0 )
                return;

            if ( !PinnedCallback.IsAllocated )
                return;

            steamworks.native.api.SteamAPI_UnregisterCallResult( PinnedCallback.AddrOfPinnedObject(), CallResultHandle );
        }
    }

}
