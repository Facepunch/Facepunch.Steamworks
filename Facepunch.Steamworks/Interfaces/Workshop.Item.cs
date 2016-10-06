using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using Facepunch.Steamworks.Callbacks.Networking;
using Facepunch.Steamworks.Callbacks.Workshop;
using Facepunch.Steamworks.Interop;
using Valve.Steamworks;

namespace Facepunch.Steamworks
{
    public partial class Workshop
    {
        public class Item
        {
            internal ISteamUGC ugc;

            public string Description { get; private set; }
            public ulong Id { get; private set; }
            public ulong OwnerId { get; private set; }
            public float Score { get; private set; }
            public string[] Tags { get; private set; }
            public string Title { get; private set; }
            public uint VotesDown { get; private set; }
            public uint VotesUp { get; private set; }

            internal static Item From( SteamUGCDetails_t details, ISteamUGC ugc )
            {
                var item = new Item();

                item.ugc = ugc;
                item.Id = details.m_nPublishedFileId;
                item.Title = details.m_rgchTitle;
                item.Description = details.m_rgchDescription;
                item.OwnerId = details.m_ulSteamIDOwner;
                item.Tags = details.m_rgchTags.Split( ',' );
                item.Score = details.m_flScore;
                item.VotesUp = details.m_unVotesUp;
                item.VotesDown = details.m_unVotesDown;
    
                return item;
            }

            public void Download()
            {
               
            }
        }
    }
}
