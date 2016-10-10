using System.Runtime.InteropServices;
using Facepunch.Steamworks.Interop;

namespace Facepunch.Steamworks.Callbacks.Workshop
{

    [StructLayout( LayoutKind.Sequential, Pack = 8 )]
    internal struct ItemInstalled
    {
        public uint AppId;
        public ulong FileId;

        public const int CallbackId = Index.UGC + 5;

        [StructLayout( LayoutKind.Sequential, Pack = 4 )]
        internal struct Small
        {
            public uint AppId;
            public ulong FileId;
        };
    };

    [StructLayout( LayoutKind.Sequential, Pack = 8 )]
    internal struct DownloadResult
    {
        public uint AppId;
        public ulong FileId;
        public Result Result;

        public const int CallbackId = Index.UGC + 6;

        [StructLayout( LayoutKind.Sequential, Pack = 4 )]
        internal struct Small
        {
            public uint AppId;
            public ulong FileId;
            public Result Result;
        };
    };


    internal class QueryCompleted : CallResult<QueryCompleted.Data, QueryCompleted.Data.Small>
    {
        public override int CallbackId { get { return Index.UGC + 1; } }

        [StructLayout( LayoutKind.Sequential, Pack = 8 )]
        internal struct Data
        {
            internal ulong Handle;
            internal int Result;
            internal uint NumResultsReturned;
            internal uint TotalMatchingResults;
            internal bool CachedData;

            [StructLayout( LayoutKind.Sequential, Pack = 4 )]
            internal struct Small
            {
                internal ulong Handle;
                internal int Result;
                internal uint NumResultsReturned;
                internal uint TotalMatchingResults;
                internal bool CachedData;
            };
        };
    }

    internal class CreateItem : CallResult<CreateItem.Data, CreateItem.Data.Small>
    {
        public override int CallbackId { get { return Index.UGC + 3; } }

        [StructLayout( LayoutKind.Sequential, Pack = 8 )]
        internal struct Data
        {
            internal Result Result;
            internal ulong FileId;
            internal bool NeedsLegalAgreement;

            [StructLayout( LayoutKind.Sequential, Pack = 4 )]
            internal struct Small
            {
                internal Result Result;
                internal ulong FileId;
 
                internal bool NeedsLegalAgreement;
            };
        };
    }

    internal class SubmitItemUpdate : CallResult<SubmitItemUpdate.Data, SubmitItemUpdate.Data.Small>
    {
        public override int CallbackId { get { return Index.UGC + 4; } }

        [StructLayout( LayoutKind.Sequential, Pack = 8 )]
        internal struct Data
        {
            internal Result Result;
            internal bool NeedsLegalAgreement;

            [StructLayout( LayoutKind.Sequential, Pack = 4 )]
            internal struct Small
            {
                internal Result Result;
                internal bool NeedsLegalAgreement;
            };
        };
    }
}
