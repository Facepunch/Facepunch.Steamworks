using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Facepunch.Steamworks
{
    public partial class ServerList
    {
        public class Server
        {
            public string Name { get; set; }
            public int Ping { get; set; }
            public string GameDir { get; set; }
            public string Map { get; set; }
            public string Description { get; set; }
            public uint AppId { get; set; }
            public int Players { get; set; }
            public int MaxPlayers { get; set; }
            public int BotPlayers { get; set; }
            public bool Passworded { get; set; }
            public bool Secure { get; set; }
            public uint LastTimePlayed { get; set; }
            public int Version { get; set; }
            public string[] Tags { get; set; }
            public ulong SteamId { get; set; }
            public uint Address { get; set; }
            public int ConnectionPort { get; set; }
            public int QueryPort { get; set; }

            internal Client Client;

            public string AddressString
            {
                get
                {
                    return string.Format( "{0}.{1}.{2}.{3}", ( Address >> 24 ) & 0xFFul, ( Address >> 16 ) & 0xFFul, ( Address >> 8 ) & 0xFFul, Address & 0xFFul );
                }
            }
            public string ConnectionAddress
            {
                get
                {
                    return string.Format( "{0}.{1}.{2}.{3}:{4}", ( Address >> 24 ) & 0xFFul, ( Address >> 16 ) & 0xFFul, ( Address >> 8 ) & 0xFFul, Address & 0xFFul, ConnectionPort );
                }
            }

            internal static Server FromSteam( Client client, SteamNative.gameserveritem_t item )
            {
                return new Server()
                {
                    Client = client,
                    Address = item.NetAdr.IP,
                    ConnectionPort = item.NetAdr.ConnectionPort,
                    QueryPort = item.NetAdr.QueryPort,
                    Name = item.ServerName,
                    Ping = item.Ping,
                    GameDir = item.GameDir,
                    Map = item.Map,
                    Description = item.GameDescription,
                    AppId = item.AppID,
                    Players = item.Players,
                    MaxPlayers = item.MaxPlayers,
                    BotPlayers = item.BotPlayers,
                    Passworded = item.Password,
                    Secure = item.Secure,
                    LastTimePlayed = item.TimeLastPlayed,
                    Version = item.ServerVersion,
                    Tags = item.GameTags == null ? null : item.GameTags.Split( ',' ),
                    SteamId = item.SteamID
                };
            }

            /// <summary>
            /// Callback when rules are receieved.
            /// The bool is true if server responded properly.
            /// </summary>
            public Action<bool> OnReceivedRules;

            /// <summary>
            /// List of server rules. Use HasRules to see if this is safe to access.
            /// </summary>
            public Dictionary<string, string> Rules;

            /// <summary>
            /// Returns true if this server has rules
            /// </summary>
            public bool HasRules { get { return Rules != null && Rules.Count > 0; } }

           internal Interop.ServerRules RulesRequest;

            /// <summary>
            /// Populates Rules for this server
            /// </summary>
            public void FetchRules()
            {
                if ( RulesRequest != null )
                    return;

                Rules = new Dictionary<string, string>();

                RulesRequest = new Interop.ServerRules( this, Address, QueryPort );
            }

            internal void OnServerRulesReceiveFinished( bool Success )
            {
                RulesRequest.Dispose();
                RulesRequest = null;

                if ( OnReceivedRules != null )
                    OnReceivedRules( Success );
            }
        }
    }
}
