using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using Steamworks.Data;

namespace Steamworks
{
	internal partial class Callback
    {
		[StructLayout( LayoutKind.Sequential, Pack = 1 )]
        public class VTable
        {
            [UnmanagedFunctionPointer( CallingConvention.StdCall )] public delegate void ResultD( IntPtr pvParam );
            [UnmanagedFunctionPointer( CallingConvention.StdCall )] public delegate void ResultWithInfoD( IntPtr pvParam, bool bIOFailure, SteamAPICall_t hSteamAPICall );
            [UnmanagedFunctionPointer( CallingConvention.StdCall )] public delegate int GetSizeD();

            public ResultD ResultA;
            public ResultWithInfoD ResultB;
            public GetSizeD GetSize;

			internal static IntPtr GetVTable( ResultD onResultThis, ResultWithInfoD onResultWithInfoThis, GetSizeD onGetSizeThis, List<GCHandle> allocations )
			{
				var vTablePtr = Marshal.AllocHGlobal( Marshal.SizeOf( typeof( Callback.VTable ) ) );

				var vTable = new Callback.VTable
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
