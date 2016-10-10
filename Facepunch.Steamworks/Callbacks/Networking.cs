using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Facepunch.Steamworks.Callbacks.Networking
{
    [StructLayout( LayoutKind.Sequential, Pack = 1 )]
    internal class P2PSessionRequest
    {
        public ulong SteamID;

        public const int CallbackId = Index.Networking + 2;
    };

    [StructLayout( LayoutKind.Sequential, Pack = 1 )]
    internal class P2PSessionConnectFail
    {
        public ulong SteamID;
        public Steamworks.Networking.SessionError Error;

        public const int CallbackId = Index.Networking + 3;
    };


}
