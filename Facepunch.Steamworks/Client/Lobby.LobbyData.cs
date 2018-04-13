using System.Collections.Generic;

namespace Facepunch.Steamworks
{
    public partial class Lobby
    {
        /// <summary>
        /// Class to hold global lobby data. This is stuff like maps/modes/etc. Data set here can be filtered by LobbyList.
        /// </summary>
        public class LobbyData
        {
            internal Client client;
            internal ulong lobby;
            internal Dictionary<string, string> data;

            public LobbyData( Client c, ulong l )
            {
                client = c;
                lobby = l;
                data = new Dictionary<string, string>();
            }

            /// <summary>
            /// Get the lobby value for the specific key
            /// </summary>
            /// <param name="k">The key to find</param>
            /// <returns>The value at key</returns>
            public string GetData( string k )
            {
                if ( data.ContainsKey( k ) )
                {
                    return data[k];
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
                foreach ( KeyValuePair<string, string> item in data )
                {
                    returnData.Add( item.Key, item.Value );
                }
                return returnData;
            }

            /// <summary>
            /// Set the value for specified Key. Note that the keys "joinable", "appid", "name", and "lobbytype" are reserved for internal library use.
            /// </summary>
            /// <param name="k">The key to set the value for</param>
            /// <param name="v">The value of the Key</param>
            /// <returns>True if data successfully set</returns>
            public bool SetData( string k, string v )
            {
                if ( data.ContainsKey( k ) )
                {
                    if ( data[k] == v ) { return true; }
                    if ( client.native.matchmaking.SetLobbyData( lobby, k, v ) )
                    {
                        data[k] = v;
                        return true;
                    }
                }
                else
                {
                    if ( client.native.matchmaking.SetLobbyData( lobby, k, v ) )
                    {
                        data.Add( k, v );
                        return true;
                    }
                }

                return false;
            }

            /// <summary>
            /// Remove the key from the LobbyData. Note that the keys "joinable", "appid", "name", and "lobbytype" are reserved for internal library use.
            /// </summary>
            /// <param name="k">The key to remove</param>
            /// <returns>True if Key successfully removed</returns>
            public bool RemoveData( string k )
            {
                if ( data.ContainsKey( k ) )
                {
                    if ( client.native.matchmaking.DeleteLobbyData( lobby, k ) )
                    {
                        data.Remove( k );
                        return true;
                    }
                }

                return false;
            }

        }

        /*not implemented

        //set the game server of the lobby
        client.native.matchmaking.GetLobbyGameServer;
        client.native.matchmaking.SetLobbyGameServer;

        //used with game server stuff
        SteamNative.LobbyGameCreated_t
        */
    }
}
