using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Steamworks
{
    public struct Clan
    {
        public SteamId Id;

        public Clan(SteamId id)
        {
            Id = id;
        }

        public string Name => SteamFriends.Internal.GetClanName(Id);

        public string Tag => SteamFriends.Internal.GetClanTag(Id);

        public int ChatMemberCount => SteamFriends.Internal.GetClanChatMemberCount(Id);

        public Friend Owner => new Friend(SteamFriends.Internal.GetClanOwner(Id));

        public bool Public => SteamFriends.Internal.IsClanPublic(Id);

        /// <summary>
        /// Is the clan an official game group?
        /// </summary>
        public bool Official => SteamFriends.Internal.IsClanOfficialGameGroup(Id);

        public int Online => GetActivity().Online;

        public int Chatting => GetActivity().Chatting;

        public int InGame => GetActivity().InGame;

        /// <summary>
        /// Asynchronously fetches the officer list for a given clan
        /// </summary>
        /// <returns>Whether the request was successful or not</returns>
        public async Task<bool> RequestOfficerList()
        {
            var req = await SteamFriends.Internal.RequestClanOfficerList(Id);
            return req.HasValue && req.Value.Success != 0x0;
        }

        public IEnumerable<Friend> GetOfficers()
        {
            for (int i = 0; i < SteamFriends.Internal.GetClanOfficerCount(Id); i++)
            {
                yield return new Friend(SteamFriends.Internal.GetClanOfficerByIndex(Id, i));
            }
        }

        public Friend GetChatMember(int index)
        {
            return new Friend(SteamFriends.Internal.GetChatMemberByIndex(Id, index));
        }

        public IEnumerable<Friend> GetChatMembers()
        {
            for(int i = 0; i < ChatMemberCount; i++)
            {
                yield return GetChatMember(i);
            }
        }

        private ClanActivity GetActivity()
        {
            var result = new ClanActivity();
            SteamFriends.Internal.GetClanActivityCounts(Id, ref result.Online, ref result.InGame, ref result.Chatting);
            return result;
        }

        private struct ClanActivity
        {
            public int Online;
            public int InGame;
            public int Chatting;
        }

    }
}
