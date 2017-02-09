using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using SteamNative;

namespace Facepunch.Steamworks
{
    public partial class ServerList : IDisposable
    {
        internal Client client;

        internal ServerList( Client client )
        {
            this.client = client;

            UpdateFavouriteList();
        }

        HashSet<ulong> FavouriteHash = new HashSet<ulong>();
        HashSet<ulong> HistoryHash = new HashSet<ulong>();

        internal void UpdateFavouriteList()
        {
            FavouriteHash.Clear();
            HistoryHash.Clear();

            for ( int i=0; i< client.native.matchmaking.GetFavoriteGameCount(); i++ )
            {
                AppId_t appid = 0;
                uint ip;
                ushort conPort;
                ushort queryPort;
                uint lastplayed;
                uint flags;

                client.native.matchmaking.GetFavoriteGame( i, ref appid, out ip, out conPort, out queryPort, out flags, out lastplayed );

                ulong encoded = ip;
                encoded = encoded << 32;
                encoded = encoded | (uint)conPort;

                if ( ( flags & Server.k_unFavoriteFlagFavorite ) == Server.k_unFavoriteFlagFavorite )
                    FavouriteHash.Add( encoded );

                if ( ( flags & Server.k_unFavoriteFlagFavorite ) == Server.k_unFavoriteFlagFavorite )
                    HistoryHash.Add( encoded );
            }
        }

        public void Dispose()
        {
            client = null;
        }

        public class Filter : List<KeyValuePair<string, string>>
        {
            public void Add( string k, string v )
            {
                Add( new KeyValuePair<string, string>( k, v ) );
            }

            internal IntPtr NativeArray;
            private IntPtr m_pArrayEntries;

            private int AppId = 0;

            internal void Start()
            {
                var filters = this.Select( x =>
                {
                    if ( x.Key == "appid" ) AppId = int.Parse( x.Value );

                    return new SteamNative.MatchMakingKeyValuePair_t()
                    {
                        Key  = x.Key,
                        Value = x.Value
                    };
                } ).ToArray();

                int sizeOfMMKVP = Marshal.SizeOf(typeof(SteamNative.MatchMakingKeyValuePair_t));
                NativeArray = Marshal.AllocHGlobal( Marshal.SizeOf( typeof( IntPtr ) ) * filters.Length );
                m_pArrayEntries = Marshal.AllocHGlobal( sizeOfMMKVP * filters.Length );

                for ( int i = 0; i < filters.Length; ++i )
                {
                    Marshal.StructureToPtr( filters[i], new IntPtr( m_pArrayEntries.ToInt64() + ( i * sizeOfMMKVP ) ), false );
                }

                Marshal.WriteIntPtr( NativeArray, m_pArrayEntries );
            }

            internal void Free()
            {
                if ( m_pArrayEntries != IntPtr.Zero )
                {
                    Marshal.FreeHGlobal( m_pArrayEntries );
                }

                if ( NativeArray != IntPtr.Zero )
                {
                    Marshal.FreeHGlobal( NativeArray );
                }
            }

            internal bool Test( gameserveritem_t info )
            {
                if ( AppId != 0 && AppId != info.AppID )
                    return false;

                return true;
            }
        }

 

        [StructLayout( LayoutKind.Sequential )]
        private struct MatchPair
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
            public string key;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
            public string value;
        }



        public Request Internet( Filter filter = null )
        {
            if ( filter == null )
            {
                filter = new Filter();
                filter.Add( "appid", client.AppId.ToString() );
            }

            filter.Start();

            var request = new Request( client );
            request.Filter = filter;
            request.AddRequest( client.native.servers.RequestInternetServerList( client.AppId, filter.NativeArray, (uint) filter.Count, IntPtr.Zero ) );

            filter.Free();

            return request;
        }

        /// <summary>
        /// Query a list of addresses. No filters applied.
        /// </summary>
        public Request Custom( IEnumerable<string> serverList )
        {
            var request = new Request( client );
            request.ServerList = serverList;
            request.StartCustomQuery();
            return request;
        }

        /// <summary>
        /// Request a list of servers we've been on. History isn't applied automatically
        /// You need to call server.AddtoHistoryList() when you join a server etc.
        /// </summary>
        public Request History( Filter filter = null )
        {
            if ( filter == null )
            {
                filter = new Filter();
                filter.Add( "appid", client.AppId.ToString() );
            }

            filter.Start();

            var request = new Request( client );
            request.Filter = filter;
            request.AddRequest( client.native.servers.RequestHistoryServerList( client.AppId, filter.NativeArray, (uint)filter.Count, IntPtr.Zero ) );

            filter.Free();

            return request;
        }

        /// <summary>
        /// Request a list of servers we've favourited
        /// </summary>
        public Request Favourites( Filter filter = null )
        {
            if ( filter == null )
            {
                filter = new Filter();
                filter.Add( "appid", client.AppId.ToString() );
            }

            filter.Start();

            var request = new Request( client );
            request.Filter = filter;
            request.AddRequest( client.native.servers.RequestFavoritesServerList( client.AppId, filter.NativeArray, (uint)filter.Count, IntPtr.Zero ) );

            filter.Free();

            return request;
        }

        /// <summary>
        /// Request a list of servers that our friends are on
        /// </summary>
        public Request Friends( Filter filter = null )
        {
            if ( filter == null )
            {
                filter = new Filter();
                filter.Add( "appid", client.AppId.ToString() );
            }

            filter.Start();

            var request = new Request( client );
            request.Filter = filter;
            request.AddRequest( client.native.servers.RequestFriendsServerList( client.AppId, filter.NativeArray, (uint)filter.Count, IntPtr.Zero ) );

            filter.Free();

            return request;
        }

        /// <summary>
        /// Request a list of servers that are running on our LAN
        /// </summary>
        public Request Local( Filter filter = null )
        {
            if ( filter == null )
            {
                filter = new Filter();
                filter.Add( "appid", client.AppId.ToString() );
            }

            filter.Start();

            var request = new Request( client );
            request.Filter = filter;
            request.AddRequest( client.native.servers.RequestLANServerList( client.AppId, IntPtr.Zero ) );

            filter.Free();

            return request;
        }


        internal bool IsFavourite( Server server )
        {
            ulong encoded = server.Address;
            encoded = encoded << 32;
            encoded = encoded | (uint)server.ConnectionPort;

            return FavouriteHash.Contains( encoded );
        }

        internal bool IsHistory( Server server )
        {
            ulong encoded = server.Address;
            encoded = encoded << 32;
            encoded = encoded | (uint)server.ConnectionPort;

            return HistoryHash.Contains( encoded );
        }
    }
}
