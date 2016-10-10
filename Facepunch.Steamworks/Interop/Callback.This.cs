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
        [UnmanagedFunctionPointer( CallingConvention.ThisCall )]    public delegate int GetSize( IntPtr thisptr );

        [MarshalAs(UnmanagedType.FunctionPtr)]  public Result RunCallResult;
        [MarshalAs(UnmanagedType.FunctionPtr)]  public Result RunCallback;
        [MarshalAs(UnmanagedType.FunctionPtr)]  public GetSize GetCallbackSizeBytes;

        internal static IntPtr Get( Action<IntPtr, IntPtr> onRunCallback, int size, Interop.Callback cb )
        {
            var ptr = Marshal.AllocHGlobal( Marshal.SizeOf( typeof( Callback ) ) );

            Callback.Result da = ( _, p ) => {  onRunCallback( _, p ); Console.WriteLine( "Callback.Result: {0}", _.ToInt64() ); };
            Callback.GetSize dc = ( _ ) => { return size; };

            cb.AddHandle( GCHandle.Alloc( da ) );
            cb.AddHandle( GCHandle.Alloc( dc ) );

            var table = new Callback()
            {
                RunCallback = da,
                RunCallResult = da,
                GetCallbackSizeBytes = dc
            };

            Marshal.StructureToPtr( table, ptr, false );

            return ptr;
        }
    }


}
