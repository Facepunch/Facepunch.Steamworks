using System.Runtime.InteropServices;
using Facepunch.Steamworks.Interop;

namespace Facepunch.Steamworks.Callbacks.Workshop
{
    [StructLayout( LayoutKind.Sequential, Pack = 8 )]
    internal struct ItemInstalled
    {
        public uint AppId;
        public ulong FileId;

        public const int CallbackId = SteamNative.CallbackIdentifiers.ClientUGC + 5;

        [StructLayout( LayoutKind.Sequential, Pack = 4 )]
        internal struct Small
        {
            public uint AppId;
            public ulong FileId;
        };
    };

    internal class QueryCompleted : CallResult<SteamNative.SteamUGCQueryCompleted_t, SteamNative.SteamUGCQueryCompleted_t.PackSmall>
    {
        public override int CallbackId { get { return SteamNative.SteamUGCQueryCompleted_t.CallbackId; } }
    }

    internal class CreateItem : CallResult<SteamNative.CreateItemResult_t, SteamNative.CreateItemResult_t.PackSmall>
    {
        public override int CallbackId { get { return SteamNative.CreateItemResult_t.CallbackId; } }
    }

    internal class SubmitItemUpdate : CallResult<SteamNative.SubmitItemUpdateResult_t, SteamNative.SubmitItemUpdateResult_t.PackSmall>
    {
        public override int CallbackId { get { return SteamNative.SubmitItemUpdateResult_t.CallbackId; } }

        public SteamNative.UGCUpdateHandle_t UpdateHandle;
    }
}
