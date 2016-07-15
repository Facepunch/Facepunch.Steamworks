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
            var filters = new List<MatchPair>();
            filters.Add( new MatchPair() { key = "gamedir", value = "rust" } );

            var array = filters.ToArray();

            //fixed ( void* a = array )
            {

                var request = new Request()
                {
                    client = client
                };

                request.Id = client.native.servers.RequestInternetServerList( client.AppId, new IntPtr[] { }, request.GetVTablePointer() );

                return request;
            }
        }

        public unsafe Request History( Dictionary< string, string > filter )
        {
            var request = new Request()
            {
                client = client
            };

            request.Id = client.native.servers.RequestHistoryServerList( client.AppId, new IntPtr[] { }, request.GetVTablePointer() );

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
