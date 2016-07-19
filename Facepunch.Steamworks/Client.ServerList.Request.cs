using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using Valve.Steamworks;

namespace Facepunch.Steamworks
{
    public partial class ServerList
    {
        public class Request : IDisposable
        {
            internal Client client;
            internal IntPtr Id;

            public Action OnUpdate;

            public struct Server
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

                internal static Server FromSteam( gameserveritem_t item )
                {
                    return new Server()
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
            }

            /// <summary>
            /// A list of servers that responded. If you're only interested in servers that responded since you
            /// last updated, then simply clear this list.
            /// </summary>
            public List<Server> Responded = new List<Server>();

            /// <summary>
            /// A list of servers that were in the master list but didn't respond. 
            /// </summary>
            public List<Server> Unresponsive = new List<Server>();

            /// <summary>
            /// True when we have finished
            /// </summary>
            public bool Finished = false;

            internal Request( Client c )
            {
                client = c;

                client.OnUpdate += Update;
            }

            ~Request()
            {
                Dispose();
            }

            int lastCount = 0;

            internal List<int> watchlist = new List<int>();

            internal IEnumerable<string> ServerList { get; set; }
            internal int ServerListPointer = 0;


            void UpdateCustomQuery()
            {
                if ( ServerList == null )
                    return;

                if ( Id != IntPtr.Zero )
                    return;

                var sublist = ServerList.Skip( ServerListPointer ).Take( 10 );

                if ( sublist.Count() == 0 )
                {
                    ServerList = null;
                    ServerListPointer = 0;
                    Finished = true;
                    return;
                }

                ServerListPointer += sublist.Count();

                var filter = new Filter();
                filter.Add( "or", sublist.Count().ToString() );

                foreach ( var server in sublist )
                {
                    filter.Add( "gameaddr", server );
                }


                filter.Start();
                Id = client.native.servers.RequestInternetServerList( client.AppId, filter.NativeArray, filter.Count, IntPtr.Zero );
                filter.Free();
            }

            private void Update()
            {
                UpdateCustomQuery();

                if ( Id == IntPtr.Zero )
                    return;

                bool changes = false;

                //
                // Add any servers we're not watching to our watch list
                //
                var count = client.native.servers.GetServerCount( Id );
                if ( count != lastCount )
                {
                    for ( int i = lastCount; i < count; i++ )
                    {
                        watchlist.Add( i );
                    }

                    lastCount = count;
                }

                //
                // Remove any servers that respond successfully
                //
                watchlist.RemoveAll( x =>
                {
                    var info = client.native.servers.GetServerDetails( Id, x );
                    if ( info.m_bHadSuccessfulResponse )
                    {
                        OnServer( info );
                        changes = true;
                        return true;
                    }

                    return false;
                } );

                //
                // If we've finished refreshing
                // 
                if ( client.native.servers.IsRefreshing( Id ) == false )
                {
                    //
                    // Put any other servers on the 'no response' list
                    //
                    watchlist.RemoveAll( x =>
                    {
                        var info = client.native.servers.GetServerDetails( Id, x );
                        OnServer( info );
                        return true;
                    } );

                    //
                    // We have more to process
                    //
                    if ( ServerList == null )
                    {
                        Finished = true;
                        client.OnUpdate -= Update;
                    }

                    client.native.servers.CancelQuery( Id );
                    Id = IntPtr.Zero;
                    changes = true;
                }

                if ( changes && OnUpdate != null)
                    OnUpdate();
            }

            private void OnServer( gameserveritem_t info )
            {
                if ( info.m_bHadSuccessfulResponse )
                {
                    Responded.Add( Server.FromSteam( info ) );
                }
                else
                {
                    Unresponsive.Add( Server.FromSteam( info ) );
                }

            }

            /// <summary>
            /// Disposing will end the query
            /// </summary>
            public void Dispose()
            {
                client.OnUpdate -= Update;

                //
                // Cancel the query if it's still running
                //
                if ( Id != IntPtr.Zero )
                {
                    if ( client.Valid )
                        client.native.servers.CancelQuery( Id );

                    Id = IntPtr.Zero;
                }
            }

        }
    }
}
