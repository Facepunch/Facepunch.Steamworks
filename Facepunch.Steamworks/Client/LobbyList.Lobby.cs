using System;
using System.Collections.Generic;
using System.Text;

namespace Facepunch.Steamworks
{
    public partial class LobbyList
    {
        public class Lobby 
        {
            internal Client Client;
            public string Name { get; private set; }
            public ulong LobbyID { get; private set; }
            public ulong Owner { get; private set; }
            public int MemberLimit{ get; private set; }
            public int NumMembers{ get; private set; }

            internal static Lobby FromSteam(Client client, ulong lobby)
            {
                return new Lobby()
                {
                    Client = client,
                    LobbyID = lobby,
                    Name = client.native.matchmaking.GetLobbyData(lobby, "name"),
                    MemberLimit = client.native.matchmaking.GetLobbyMemberLimit(lobby),
                    Owner = client.native.matchmaking.GetLobbyOwner(lobby),
                    NumMembers = client.native.matchmaking.GetNumLobbyMembers(lobby)
                };
                
            }
        }
    }
}
