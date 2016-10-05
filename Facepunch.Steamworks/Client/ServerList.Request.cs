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
                    Responded.Add( Server.FromSteam( client, info ) );
                }
                else
                {
                    Unresponsive.Add( Server.FromSteam( client, info ) );
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
                    if ( client.IsValid )
                        client.native.servers.CancelQuery( Id );

                    Id = IntPtr.Zero;
                }
            }

        }
    }
}
