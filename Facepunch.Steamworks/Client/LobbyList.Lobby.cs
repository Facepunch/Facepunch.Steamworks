using System;
using System.Collections.Generic;
using System.Text;

namespace Facepunch.Steamworks
{
    public partial class LobbyList
    {
        public class Lobby 
        {
            private Dictionary<string, string> m_lobbyData;
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
                if (m_lobbyData.ContainsKey(k))
                {
                    return m_lobbyData[k];
                }

                return "ERROR: key not found";
            }

            /// <summary>
            /// Get a list of all the data in the Lobby
            /// </summary>
            /// <returns>Dictionary of all the key/value pairs in the data</returns>
            public Dictionary<string, string> GetAllData()
            {
                Dictionary<string, string> returnData = new Dictionary<string, string>();
                foreach (KeyValuePair<string, string> item in m_lobbyData)
                {
                    returnData.Add(item.Key, item.Value);
                }
                return returnData;
            }

            internal static Lobby FromSteam(Client client, ulong lobby)
            {
                Dictionary<string, string> lobbyData = new Dictionary<string, string>();
                int dataCount = client.native.matchmaking.GetLobbyDataCount(lobby);
                for (int i = 0; i < dataCount; i++)
                {
                    string datakey = string.Empty;
                    string datavalue = string.Empty;
                    if (client.native.matchmaking.GetLobbyDataByIndex(lobby, i, out datakey, out datavalue))
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
                    m_lobbyData = lobbyData
                };
                
            }
        }
    }
}
