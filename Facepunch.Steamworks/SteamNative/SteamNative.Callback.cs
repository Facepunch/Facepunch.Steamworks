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
    };

    //
    // Created on registration of a callback
    //
    public class CallbackHandle : IDisposable
    {
        internal BaseSteamworks steamworks;

        internal SteamAPICall_t callHandle;

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

            if ( vTablePtr != IntPtr.Zero )
            {
                Marshal.FreeHGlobal( vTablePtr );
                vTablePtr = IntPtr.Zero;
            }
        }

        internal void UnregisterCallback()
        {
            if ( PinnedCallback.IsAllocated )
            {
                steamworks.native.api.SteamAPI_UnregisterCallback( PinnedCallback.AddrOfPinnedObject() );
            }

            Dispose();
        }

        internal void UnregisterCallResult()
        {
            if ( PinnedCallback.IsAllocated )
            {
                steamworks.native.api.SteamAPI_UnregisterCallResult( PinnedCallback.AddrOfPinnedObject(), callHandle );
            }

            Dispose();
        }
    }
    /*
    public class CallResult<T> : IDisposable
    {
        public SteamAPICall_t Handle { get; private set; }
        private Callback.Handle CallbackHandle;
        

        internal CallResult( BaseSteamworks steamworks, SteamAPICall_t Handle, Callback.Handle CallbackHandle )
        {
            this.Handle = Handle;
            this.CallbackHandle = CallbackHandle;
            this.steamworks = steamworks;
        }

        public void Dispose()
        {
            if ( !steamworks.IsValid ) return;
            if ( this.Handle == 0 ) return;

            steamworks.native.api.SteamAPI_UnregisterCallResult( CallbackHandle.PinnedCallback.AddrOfPinnedObject(), Handle );
            CallbackHandle.Dispose();
            Handle = 0;
        }
    }
    */

}
