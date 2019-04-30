using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using Steamworks.Data;

namespace Steamworks
{
	internal partial class Callback
    {
        internal static class VTable
        {
            [UnmanagedFunctionPointer( CallingConvention.ThisCall )]
			public delegate void Run( IntPtr thisptr, IntPtr pvParam );

            [UnmanagedFunctionPointer( CallingConvention.ThisCall )]
			public delegate void RunCall( IntPtr thisptr, IntPtr pvParam, bool bIOFailure, SteamAPICall_t hSteamAPICall );

            [UnmanagedFunctionPointer( CallingConvention.ThisCall )]
			public delegate int GetCallbackSizeBytes( IntPtr thisptr );

			internal static IntPtr GetVTable( Run run, RunCall runInfo, GetCallbackSizeBytes size, List<GCHandle> allocations )
			{
				allocations.Add( GCHandle.Alloc( run ) );
				allocations.Add( GCHandle.Alloc( runInfo ) );
				allocations.Add( GCHandle.Alloc( size ) );

				var a = Marshal.GetFunctionPointerForDelegate<Run>( run );
				var b = Marshal.GetFunctionPointerForDelegate<RunCall>( runInfo );
				var c = Marshal.GetFunctionPointerForDelegate<GetCallbackSizeBytes>( size );

				var vt = Marshal.AllocHGlobal( IntPtr.Size * 3 );

				if ( Config.Os == OsType.Windows )
				{
					Marshal.WriteIntPtr( vt, IntPtr.Size * 0, b );
					Marshal.WriteIntPtr( vt, IntPtr.Size * 1, a );
					Marshal.WriteIntPtr( vt, IntPtr.Size * 2, c );
				}
				else
				{
					Marshal.WriteIntPtr( vt, IntPtr.Size * 0, a );
					Marshal.WriteIntPtr( vt, IntPtr.Size * 1, b );
					Marshal.WriteIntPtr( vt, IntPtr.Size * 2, c );
				}

				return vt;
			}
		}
    };
}
