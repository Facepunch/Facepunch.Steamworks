using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;

namespace Steamworks
{
	internal partial class Callback
    {
		[StructLayout( LayoutKind.Sequential, Pack = 1 )]
        public class VTableWinThis
        {
            [UnmanagedFunctionPointer( CallingConvention.ThisCall )] public delegate void ResultD( IntPtr thisptr, IntPtr pvParam );
            [UnmanagedFunctionPointer( CallingConvention.ThisCall )] public delegate void ResultWithInfoD( IntPtr thisptr, IntPtr pvParam, bool bIOFailure, SteamAPICall_t hSteamAPICall );
            [UnmanagedFunctionPointer( CallingConvention.ThisCall )] public delegate int GetSizeD( IntPtr thisptr );

            public ResultWithInfoD ResultB;
            public ResultD ResultA;
            public GetSizeD GetSize;

			internal static IntPtr GetVTable( ResultD onResultThis, ResultWithInfoD onResultWithInfoThis, GetSizeD onGetSizeThis, List<GCHandle> allocations )
			{
				var vTablePtr = Marshal.AllocHGlobal( Marshal.SizeOf( typeof( Callback.VTableWinThis ) ) );

				var vTable = new Callback.VTableWinThis
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
		}
    };
}
