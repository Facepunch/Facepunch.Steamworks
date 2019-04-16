using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;

namespace Steamworks
{
	internal partial class Callback
    {
		[StructLayout( LayoutKind.Sequential, Pack = 1 )]
        public class VTableThis
        {
            [UnmanagedFunctionPointer( CallingConvention.ThisCall )] public delegate void ResultD( IntPtr thisptr, IntPtr pvParam );
            [UnmanagedFunctionPointer( CallingConvention.ThisCall )] public delegate void ResultWithInfoD( IntPtr thisptr, IntPtr pvParam, bool bIOFailure, SteamAPICall_t hSteamAPICall );

			internal static IntPtr GetVTable( ResultD onResultThis, ResultWithInfoD onResultWithInfoThis, GetSizeD onGetSizeThis, List<GCHandle> allocations )
			{
				var vTablePtr = Marshal.AllocHGlobal( Marshal.SizeOf( typeof( Callback.VTableThis ) ) );

				var vTable = new Callback.VTableThis
				{
					ResultA = onResultThis,
					ResultB = onResultWithInfoThis,
					GetSize = onGetSizeThis,
				};

				allocations.Add( GCHandle.Alloc( vTable.ResultA ) );
				allocations.Add( GCHandle.Alloc( vTable.ResultB ) );
				allocations.Add( GCHandle.Alloc( vTable.GetSize ) );

				Marshal.StructureToPtr( vTable, vTablePtr, false );

				return vTablePtr;
			}

			[UnmanagedFunctionPointer( CallingConvention.ThisCall )] public delegate int GetSizeD( IntPtr thisptr );

            public ResultD ResultA;
            public ResultWithInfoD ResultB;
            public GetSizeD GetSize;
        }
    };
}
