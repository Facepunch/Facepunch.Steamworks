using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Facepunch.Steamworks
{
    public partial class ServerList
    {
        public class Request : IDisposable
        {
            internal Client client;

            internal List<SubRequest> Requests = new List<SubRequest>();

            internal class SubRequest
            {
                internal IntPtr Request;
                internal int Pointer = 0;
                internal List<int> WatchList = new List<int>();
                internal System.Diagnostics.Stopwatch Timer = System.Diagnostics.Stopwatch.StartNew();

                internal bool Update( SteamNative.SteamMatchmakingServers servers, Action<SteamNative.gameserveritem_t> OnServer, Action OnUpdate )
                {
                    if ( Request == IntPtr.Zero )
                        return true;

                    if ( Timer.Elapsed.TotalSeconds < 0.5f )
                        return false;

                    Timer.Reset();
                    Timer.Start();

                    bool changes = false;

                    //
                    // Add any servers we're not watching to our watch list
                    //
                    var count = servers.GetServerCount( Request );
                    if ( count != Pointer )
                    {
                        for ( int i = Pointer; i < count; i++ )
                        {
                            WatchList.Add( i );
                        }
                    }
                    Pointer = count;

                    //
                    // Remove any servers that respond successfully
                    //
                    WatchList.RemoveAll( x =>
                    {
                        var info = servers.GetServerDetails( Request, x );
                        if ( info.HadSuccessfulResponse )
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
                    if ( servers.IsRefreshing( Request ) == false )
                    {
                        //
                        // Put any other servers on the 'no response' list
                        //
                        WatchList.RemoveAll( x =>
                        {
                            var info = servers.GetServerDetails( Request, x );
                            OnServer( info );
                            return true;
                        } );

                        servers.CancelQuery( Request );
                        Request = IntPtr.Zero;
                        changes = true;
                    }

                    if ( changes && OnUpdate != null )
                        OnUpdate();

                    return Request == IntPtr.Zero;
                }
            }

            public Action OnUpdate;
            public Action<Server> OnServerResponded;
            public Action OnFinished;

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
            
            internal IEnumerable<string> ServerList { get; set; }
            internal Filter Filter { get; set; }

            internal void StartCustomQuery()
            {
                if ( ServerList == null )
                    return;

                int blockSize = 16;
                int Pointer = 0;

                while ( true )
                {
                    var sublist = ServerList.Skip( Pointer ).Take( blockSize );

                    if ( sublist.Count() == 0 )
                        break;

                    Pointer += sublist.Count();

                    var filter = new Filter();
                    filter.Add( "or", sublist.Count().ToString() );

                    foreach ( var server in sublist )
                    {
                        filter.Add( "gameaddr", server );
                    }

                    filter.Start();
                    var id = client.native.servers.RequestInternetServerList( client.AppId, filter.NativeArray, (uint)filter.Count, IntPtr.Zero );
                    filter.Free();

                    AddRequest( id );
                }

                ServerList = null;
            }

            internal void AddRequest( IntPtr id )
            {
                Requests.Add( new SubRequest() { Request = id } );
            }

            private void Update()
            {
                if ( Requests.Count == 0 )
                    return;

                for( int i=0; i< Requests.Count(); i++ )
                {
                    if ( Requests[i].Update( client.native.servers, OnServer, OnUpdate ) )
                    {
                        Requests.RemoveAt( i );
                        i--;
                    }
                }

                if ( Requests.Count == 0 )
                {
                    Finished = true;
                    client.OnUpdate -= Update;

                    OnFinished?.Invoke();
                }
            }

            private void OnServer( SteamNative.gameserveritem_t info )
            {
                if ( info.HadSuccessfulResponse )
                {
                    if ( Filter != null && !Filter.Test( info ) )
                        return;

                    var s = Server.FromSteam( client, info );
                    Responded.Add( s );

                    OnServerResponded?.Invoke( s );
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
                if ( client.IsValid )
                    client.OnUpdate -= Update;

                //
                // Cancel the query if it's still running
                //
                foreach( var subRequest in Requests )
                {
                    if ( client.IsValid )
                        client.native.servers.CancelQuery( subRequest.Request );
                }
                Requests.Clear();

            }

        }
    }
}
