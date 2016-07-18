using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using Valve.Steamworks;

namespace Facepunch.Steamworks
{
    public partial class Client : IDisposable
    {
        private ServerList _serverlist;

        public ServerList ServerList
        {
            get
            {
                if ( _serverlist == null )
                    _serverlist = new ServerList { client = this };

                return _serverlist;
            }
        }
    }



    public partial class ServerList
    {
        internal Client client;

        [StructLayout( LayoutKind.Sequential )]
        private struct MatchPair
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
            public string key;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
            public string value;
        }

        public unsafe Request Test()
        {
            var filters = new Dictionary<string, string>();
            filters.Add("gamedir", "rust" );

            var array = filters.ToArray();

            //fixed ( void* a = array )
            {

                var pairs = filters.Select( x => new MatchMakingKeyValuePair_t()
                {
                    m_szKey  = x.Key,
                    m_szValue = x.Value
                }).ToArray();


                var request = new Request( client );

                GCHandle h = GCHandle.Alloc( pairs, GCHandleType.Pinned );
                var a = h.AddrOfPinnedObject();

             //   request.Id = client.native.servers.RequestInternetServerList( client.AppId, h.AddrOfPinnedObject(), pairs.Length, IntPtr.Zero );

                h.Free();

                return request;
            }
        }

        class ResponseClass : ISteamMatchmakingServerListResponse
        {
            internal override IntPtr GetIntPtr()
            {
                return IntPtr.Zero;
            }

            internal override void RefreshComplete( uint hRequest, uint response )
            {
                throw new NotImplementedException();
            }

            internal override void ServerFailedToRespond( uint hRequest, int iServer )
            {
                throw new NotImplementedException();
            }

            internal override void ServerResponded( uint hRequest, int iServer )
            {
                throw new NotImplementedException();
            }
        }

        private IntPtr m_pNativeArray;
        private IntPtr m_pArrayEntries;

        internal void FilterStart( Dictionary<string, string> filter )
        {
            var filters = filter.Select( x =>
            {
                return new MatchMakingKeyValuePair_t()
                {
                    m_szKey  = x.Key,
                    m_szValue = x.Value
                };

            } ).ToArray();

            int sizeOfMMKVP = Marshal.SizeOf(typeof(MatchMakingKeyValuePair_t));
            m_pNativeArray = Marshal.AllocHGlobal( Marshal.SizeOf( typeof( IntPtr ) ) * filters.Length );
            m_pArrayEntries = Marshal.AllocHGlobal( sizeOfMMKVP * filters.Length );

            for ( int i = 0; i < filters.Length; ++i )
            {
                Marshal.StructureToPtr( filters[i], new IntPtr( m_pArrayEntries.ToInt64() + ( i * sizeOfMMKVP ) ), false );
            }

            Marshal.WriteIntPtr( m_pNativeArray, m_pArrayEntries );
        }

        internal void FilterFree()
        {
            if ( m_pArrayEntries != IntPtr.Zero )
            {
                Marshal.FreeHGlobal( m_pArrayEntries );
            }

            if ( m_pNativeArray != IntPtr.Zero )
            {
                Marshal.FreeHGlobal( m_pNativeArray );
            }
        }

        public Request Internet( Dictionary< string, string > filter )
        {
            FilterStart( filter );

            var request = new Request( client );
            request.Id = client.native.servers.RequestInternetServerList( client.AppId, m_pNativeArray, filter.Count, IntPtr.Zero );

            FilterFree();

            return request;
        }


        /// <summary>
        /// History filters don't seem to work, so we don't bother.
        /// You should apply them post process'dly
        /// </summary>
        public Request History()
        {
            var request = new Request( client );
            request.Id = client.native.servers.RequestHistoryServerList( client.AppId, new IntPtr[] { }, IntPtr.Zero );

            return request;
        }

        /// <summary>
        /// Favourite filters don't seem to work, so we don't bother.
        /// You should apply them post process'dly
        /// </summary>
        public Request Favourites()
        {
            var request = new Request( client );
            request.Id = client.native.servers.RequestFavoritesServerList( client.AppId, new IntPtr[] { }, IntPtr.Zero );

            return request;
        }

        public void AddToHistory( Request.Server server )
        {
            // client.native.matchmaking
        }

        public void RemoveFromHistory( Request.Server server )
        {
            // 
        }

        public void AddToFavourite( Request.Server server )
        {
            // client.native.matchmaking
        }

        public void RemoveFromFavourite( Request.Server server )
        {
            // 
        }

        public bool IsFavourite( Request.Server server )
        {
            return false;
        }
    }
}
