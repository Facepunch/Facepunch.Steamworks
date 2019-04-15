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

          //      if ( ( flags & Server.k_unFavoriteFlagFavorite ) == Server.k_unFavoriteFlagFavorite )
           //         FavouriteHash.Add( encoded );

            //    if ( ( flags & Server.k_unFavoriteFlagFavorite ) == Server.k_unFavoriteFlagFavorite )
             //       HistoryHash.Add( encoded );
            }
        }

        public void Dispose()
        {
            client = null;
        }
    }
}
