using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using Valve.Steamworks;

namespace Facepunch.Steamworks
{ 
    public class Server
    {
        internal Client client;

        internal Server( Client c )
        {
            client = c;
        }

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

        internal static Server FromSteam( Client c, gameserveritem_t item )
        {
            return new Server( c )
            {
                Address = item.m_NetAdr.m_unIP,
                ConnectionPort = item.m_NetAdr.m_usConnectionPort,
                QueryPort = item.m_NetAdr.m_usQueryPort,
                Name = item.m_szServerName,
                Ping = item.m_nPing,
                GameDir = item.m_szGameDir,
                Map = item.m_szMap,
                Description = item.m_szGameDescription,
                AppId = item.m_nAppID,
                Players = item.m_nPlayers,
                MaxPlayers = item.m_nMaxPlayers,
                BotPlayers = item.m_nBotPlayers,
                Passworded = item.m_bPassword,
                Secure = item.m_bSecure,
                LastTimePlayed = item.m_ulTimeLastPlayed,
                Version = item.m_nServerVersion,
                Tags = item.m_szGameTags == null ? null : item.m_szGameTags.Split( ',' ),
                SteamId = item.m_steamID
            };
        }

        public Dictionary<string, string> Rules;
        public Action OnServerRules;

        public void UpdateRules()
        {

            //
            //
            // TEMPORARY, WE NEED TO WRITE OUR OWN VERSION OF THIS, DOESN'T WORK ON SPLIT PACKETS ETC
            //
            //

            using ( var q = new SourceServerQuery( AddressString, ConnectionPort ) )
            {
                Rules = q.GetRules();
            }

            if ( OnServerRules != null && Rules != null )
                OnServerRules();
        }
    }
}