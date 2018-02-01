using System;
using System.Collections.Generic;
using System.Text;

namespace Facepunch.Steamworks
{
    public partial class LobbyList
    {
        public class Lobby 
        {
            private Dictionary<string, string> lobbyData;
            internal Client Client;
            public string Name { get; private set; }
            public ulong LobbyID { get; private set; }
            public ulong Owner { get; private set; }
            public int MemberLimit{ get; private set; }
            public int NumMembers{ get; private set; }
            public string LobbyType { get; private set; }

            /// <summary>
            /// Get the lobby value for the specific key
            /// </summary>
            /// <param name="k">The key to find</param>
            /// <returns>The value at key</returns>
            public string GetData(string k)
            {
                if (lobbyData.TryGetValue(k, out var v))
                    return v;

                return string.Empty;
            }

            /// <summary>
            /// Get a list of all the data in the Lobby
            /// </summary>
            /// <returns>Dictionary of all the key/value pairs in the data</returns>
            public Dictionary<string, string> GetAllData()
            {
                var returnData = new Dictionary<string, string>();

                foreach ( var item in lobbyData)
                {
                    returnData.Add(item.Key, item.Value);
                }

                return returnData;
            }

            internal static Lobby FromSteam(Client client, ulong lobby)
            {
                var lobbyData = new Dictionary<string, string>();
                int dataCount = client.native.matchmaking.GetLobbyDataCount(lobby);

                for (int i = 0; i < dataCount; i++)
                {
                    if (client.native.matchmaking.GetLobbyDataByIndex(lobby, i, out var datakey, out var datavalue))
                    {
                        lobbyData.Add(datakey, datavalue);
                    }
                }

                return new Lobby()
                {
                    Client = client,
                    LobbyID = lobby,
                    Name = client.native.matchmaking.GetLobbyData(lobby, "name"),
                    LobbyType = client.native.matchmaking.GetLobbyData(lobby, "lobbytype"),
                    MemberLimit = client.native.matchmaking.GetLobbyMemberLimit(lobby),
                    Owner = client.native.matchmaking.GetLobbyOwner(lobby),
                    NumMembers = client.native.matchmaking.GetNumLobbyMembers(lobby),
                    lobbyData = lobbyData
                };
                
            }
        }
    }
}
