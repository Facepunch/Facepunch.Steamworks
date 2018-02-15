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
            [UnmanagedFunctionPointer( CallingConvention.StdCall )] public delegate void ResultD( IntPtr pvParam );
            [UnmanagedFunctionPointer( CallingConvention.StdCall )] public delegate void ResultWithInfoD( IntPtr pvParam, bool bIOFailure, SteamNative.SteamAPICall_t hSteamAPICall );
            [UnmanagedFunctionPointer( CallingConvention.StdCall )] public delegate int GetSizeD();

            public ResultD ResultA;
            public ResultWithInfoD ResultB;
            public GetSizeD GetSize;
        }

        [StructLayout( LayoutKind.Sequential, Pack = 1 )]
        public class VTableWin
        {
            [UnmanagedFunctionPointer( CallingConvention.StdCall )] public delegate void ResultD( IntPtr pvParam );
            [UnmanagedFunctionPointer( CallingConvention.StdCall )] public delegate void ResultWithInfoD( IntPtr pvParam, bool bIOFailure, SteamNative.SteamAPICall_t hSteamAPICall );
            [UnmanagedFunctionPointer( CallingConvention.StdCall )] public delegate int GetSizeD();

            public ResultWithInfoD ResultB;
            public ResultD ResultA;
            public GetSizeD GetSize;
        }

        [StructLayout( LayoutKind.Sequential, Pack = 1 )]
        public class VTableThis
        {
            [UnmanagedFunctionPointer( CallingConvention.ThisCall )] public delegate void ResultD( IntPtr thisptr, IntPtr pvParam );
            [UnmanagedFunctionPointer( CallingConvention.ThisCall )] public delegate void ResultWithInfoD( IntPtr thisptr, IntPtr pvParam, bool bIOFailure, SteamNative.SteamAPICall_t hSteamAPICall );
            [UnmanagedFunctionPointer( CallingConvention.ThisCall )] public delegate int GetSizeD( IntPtr thisptr );

            public ResultD ResultA;
            public ResultWithInfoD ResultB;
            public GetSizeD GetSize;
        }

        [StructLayout( LayoutKind.Sequential, Pack = 1 )]
        public class VTableWinThis
        {
            [UnmanagedFunctionPointer( CallingConvention.ThisCall )] public delegate void ResultD( IntPtr thisptr, IntPtr pvParam );
            [UnmanagedFunctionPointer( CallingConvention.ThisCall )] public delegate void ResultWithInfoD( IntPtr thisptr, IntPtr pvParam, bool bIOFailure, SteamNative.SteamAPICall_t hSteamAPICall );
            [UnmanagedFunctionPointer( CallingConvention.ThisCall )] public delegate int GetSizeD( IntPtr thisptr );

            public ResultWithInfoD ResultB;
            public ResultD ResultA;
            public GetSizeD GetSize;
        }
    };

    //
    // Created on registration of a callback
    //
    internal class CallbackHandle : IDisposable
    {
        internal BaseSteamworks Steamworks;

        // Get Rid
        internal GCHandle FuncA;
        internal GCHandle FuncB;
        internal GCHandle FuncC;
        internal IntPtr vTablePtr;
        internal GCHandle PinnedCallback;

        internal CallbackHandle( Facepunch.Steamworks.BaseSteamworks steamworks )
        {
            Steamworks = steamworks;
        }

        public void Dispose()
        {
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

            Steamworks.native.api.SteamAPI_UnregisterCallback( PinnedCallback.AddrOfPinnedObject() );
        }

        public virtual bool IsValid { get { return true; } }
    }

    internal abstract class CallResult : CallbackHandle
    {
        internal SteamAPICall_t Call;
        public override bool IsValid { get { return Call > 0; } }


        internal CallResult( Facepunch.Steamworks.BaseSteamworks steamworks, SteamAPICall_t call ) : base( steamworks )
        {
            Call = call;
        }

        internal void Try()
        {
            bool failed = false;

            if ( !Steamworks.native.utils.IsAPICallCompleted( Call, ref failed ))
                return;

            Steamworks.UnregisterCallResult( this );

            RunCallback();
        }

        internal abstract void RunCallback();
    }


    internal class CallResult<T> : CallResult
    {
        private static byte[] resultBuffer = new byte[1024 * 16];

        internal delegate T ConvertFromPointer( IntPtr p );

        Action<T, bool> CallbackFunction;
        ConvertFromPointer ConvertFromPointerFunction;

        internal int ResultSize = -1;
        internal int CallbackId = 0;

        internal CallResult( Facepunch.Steamworks.BaseSteamworks steamworks, SteamAPICall_t call, Action<T, bool> callbackFunction, ConvertFromPointer fromPointer, int resultSize, int callbackId ) : base( steamworks, call )
        {
            ResultSize = resultSize;
            CallbackId = callbackId;
            CallbackFunction = callbackFunction;
            ConvertFromPointerFunction = fromPointer;

            Steamworks.RegisterCallResult( this );
        }

        public override string ToString()
        {
            return $"CallResult( {typeof(T).Name}, {CallbackId}, {ResultSize}b )";
        }

        unsafe internal override void RunCallback()
        {
            bool failed = false;

            fixed ( byte* ptr = resultBuffer )
            {
                if ( !Steamworks.native.utils.GetAPICallResult( Call, (IntPtr)ptr, resultBuffer.Length, CallbackId, ref failed ) || failed )
                {
                    CallbackFunction( default(T), true );
                    return;
                }

                var val = ConvertFromPointerFunction( (IntPtr)ptr );
                CallbackFunction( val, false );
            }
        }
    }

    internal class MonoPInvokeCallbackAttribute : Attribute
    {
        public MonoPInvokeCallbackAttribute() { }
    }
}
