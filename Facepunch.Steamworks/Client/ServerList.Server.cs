﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
            public IPAddress Address { get; set; }
            public int ConnectionPort { get; set; }
            public int QueryPort { get; set; }

            /// <summary>
            /// Returns true if this server is in the favourites list
            /// </summary>
            public bool Favourite
            {
                get
                {
                    return Client.ServerList.IsFavourite( this );
                }
            }

            internal Client Client;


            internal static Server FromSteam( Client client, SteamNative.gameserveritem_t item )
            {
                return new Server()
                {
                    Client = client,
                    Address = Utility.Int32ToIp( item.NetAdr.IP ), 
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

           internal SourceServerQuery RulesRequest;

            /// <summary>
            /// Populates Rules for this server
            /// </summary>
            public void FetchRules()
            {
                if ( RulesRequest != null )
                    return;

                Rules = null;
                RulesRequest = new SourceServerQuery( this, Address, QueryPort );
            }

            internal void OnServerRulesReceiveFinished( Dictionary<string, string> rules, bool Success )
            {
                RulesRequest = null;

                if ( Success )
                {
                    Rules = rules;
                }

                if ( OnReceivedRules != null )
                {
                    OnReceivedRules( Success );
                }
            }

            internal const uint k_unFavoriteFlagNone           = 0x00;
            internal const uint k_unFavoriteFlagFavorite       = 0x01; // this game favorite entry is for the favorites list
            internal const uint k_unFavoriteFlagHistory        = 0x02; // this game favorite entry is for the history list

            /// <summary>
            /// Add this server to our history list
            /// If we're already in the history list, weill set the last played time to now
            /// </summary>
            public void AddToHistory()
            {
                Client.native.matchmaking.AddFavoriteGame( AppId, Utility.IpToInt32( Address ), (ushort)ConnectionPort, (ushort)QueryPort, k_unFavoriteFlagHistory, (uint)Utility.Epoch.Current );
                Client.ServerList.UpdateFavouriteList();
            }

            /// <summary>
            /// Remove this server from our history list
            /// </summary>
            public void RemoveFromHistory()
            {
                Client.native.matchmaking.RemoveFavoriteGame( AppId, Utility.IpToInt32( Address ), (ushort)ConnectionPort, (ushort)QueryPort, k_unFavoriteFlagHistory );
                Client.ServerList.UpdateFavouriteList();
            }

            /// <summary>
            /// Add this server to our favourite list
            /// </summary>
            public void AddToFavourites()
            {
                Client.native.matchmaking.AddFavoriteGame( AppId, Utility.IpToInt32( Address ), (ushort)ConnectionPort, (ushort)QueryPort, k_unFavoriteFlagFavorite, (uint)Utility.Epoch.Current );
                Client.ServerList.UpdateFavouriteList();
            }

            /// <summary>
            /// Remove this server from our favourite list
            /// </summary>
            public void RemoveFromFavourites()
            {
                Client.native.matchmaking.RemoveFavoriteGame( AppId, Utility.IpToInt32( Address ), (ushort)ConnectionPort, (ushort)QueryPort, k_unFavoriteFlagFavorite );
                Client.ServerList.UpdateFavouriteList();
            }
        }
    }
}
