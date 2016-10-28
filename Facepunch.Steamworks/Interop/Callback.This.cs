using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Facepunch.Steamworks.Interop.VTable.This
{
    [StructLayout( LayoutKind.Sequential )]
    internal struct Callback
    {
        [UnmanagedFunctionPointer( CallingConvention.ThisCall )]    public delegate void Result( IntPtr thisptr, IntPtr pvParam );
        [UnmanagedFunctionPointer( CallingConvention.ThisCall )]    public delegate void ResultBool( IntPtr thisptr, IntPtr pvParam, bool bIOFailure, SteamNative.SteamAPICall_t hSteamAPICall );
        [UnmanagedFunctionPointer( CallingConvention.ThisCall )]    public delegate int GetSize( IntPtr thisptr );

        [MarshalAs(UnmanagedType.FunctionPtr)]  public ResultBool RunCallResult;
        [MarshalAs(UnmanagedType.FunctionPtr)]  public Result RunCallback;
        [MarshalAs(UnmanagedType.FunctionPtr)]  public GetSize GetCallbackSizeBytes;

        internal static IntPtr Get( Action<IntPtr, IntPtr, bool> onRunCallback, int size, Interop.Callback cb )
        {
            var ptr = Marshal.AllocHGlobal( Marshal.SizeOf( typeof( Callback ) ) );

            Callback.Result da = ( _, p ) => { onRunCallback( _, p, false ); };
            Callback.ResultBool db = ( _, p, iofailure, call ) => { onRunCallback( _, p, iofailure ); };
            Callback.GetSize dc = ( _ ) => { return size; };

            cb.AddHandle( GCHandle.Alloc( da ) );
            cb.AddHandle( GCHandle.Alloc( db ) );
            cb.AddHandle( GCHandle.Alloc( dc ) );

            var table = new Callback()
            {
                RunCallback = da,
                RunCallResult = db,
                GetCallbackSizeBytes = dc
            };

            Marshal.StructureToPtr( table, ptr, false );

            return ptr;
        }
    }


}
