using System;
using System.Collections.Generic;
using System.Text;

namespace Steamworks.Structs
{
    public struct JoinClanChatRoomResult
    {
        public bool Success;
        /// <summary>
        /// SteamId of the clan chat. 
        /// note: not the same as the clan's SteamId
        /// </summary>
        public SteamId Id;
    }
}
