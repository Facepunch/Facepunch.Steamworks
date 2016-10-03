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
        [UnmanagedFunctionPointer( CallingConvention.ThisCall )]
        public delegate void Result( IntPtr thisptr, IntPtr pvParam );

        [UnmanagedFunctionPointer( CallingConvention.ThisCall )]
        public delegate int GetSize( IntPtr thisptr );

        [MarshalAs(UnmanagedType.FunctionPtr)]
        public Result m_RunCallback;

        [MarshalAs(UnmanagedType.FunctionPtr)]
        public Result m_RunCallResult;

        [MarshalAs(UnmanagedType.FunctionPtr)]
        public GetSize m_GetCallbackSizeBytes;

        internal static IntPtr Get( Action<IntPtr> onRunCallback, Func<int> getSize )
        {
            var size = Marshal.SizeOf( typeof( Callback ) );
            var ptr = Marshal.AllocHGlobal( size );

            Callback.Result da = ( _, p ) => onRunCallback( p );
            Callback.GetSize dc = ( _ ) => getSize();

            var a = GCHandle.Alloc( da );
            var c = GCHandle.Alloc( dc );

            var table = new Callback()
            {
                m_RunCallResult = da,
                m_RunCallback = da,
                m_GetCallbackSizeBytes = dc
            };

            Marshal.StructureToPtr( table, ptr, false );

            return ptr;
        }
    }


}
