using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using Facepunch.Steamworks.Interop;

namespace Facepunch.Steamworks.Callbacks.Workshop
{
    internal class QueryCompleted : CallResult<QueryCompleted.Data>
    {
        public override int CallbackId { get { return Callbacks.Index.UGC + 1; } }

        [StructLayout( LayoutKind.Sequential )]
        internal struct Data
        {
            internal ulong Handle;
            internal int Result;
            internal uint m_unNumResultsReturned;
            internal uint m_unTotalMatchingResults;
            [MarshalAs(UnmanagedType.I1)]
            internal bool m_bCachedData; // indicates whether this data was retrieved from the local on-disk cache
        };
    }


}
