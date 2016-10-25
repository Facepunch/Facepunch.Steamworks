using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Facepunch.Steamworks
{
    public partial class Server
    {
        ServerStats _stats;

        public ServerStats Stats
        {
            get
            {
                if ( _stats == null )
                    _stats = new ServerStats( this );

                return _stats;
            }
        }
    }

    public class ServerStats
    {
        internal Server server;

        internal ServerStats( Server s )
        {
            server = s;
        }

        [StructLayout( LayoutKind.Sequential )]
        public struct StatsReceived
        {
            public int Result;
            public ulong SteamId;
        }

        /// <summary>
        /// Retrieve the stats for this user
        /// </summary>
        public void Refresh( ulong steamid )
        {
            var handle = server.native.gameServerStats.RequestUserStats( steamid );
        }

        public void Commit( ulong steamid )
        {
            server.native.gameServerStats.StoreUserStats( steamid );
        }

        /// <summary>
        /// Set the named statistic for this user
        /// </summary>
        public bool Set( ulong steamid, string name, int stat )
        {
            return server.native.gameServerStats.SetUserStat( steamid, name, stat );
        }

        /// <summary>
        /// Set the named statistic for this user
        /// </summary>
        public bool Set( ulong steamid, string name, float stat )
        {
            return server.native.gameServerStats.SetUserStat0( steamid, name, stat );
        }

        /// <summary>
        /// Set the named stat for this user
        /// </summary>
        public int GetInt( ulong steamid, string name, int defaultValue = 0 )
        {
            int data = defaultValue;

            if ( !server.native.gameServerStats.GetUserStat( steamid, name, out data ) )
                return defaultValue;

            return data;
        }

        /// <summary>
        /// Set the named stat for this user
        /// </summary>
        public float GetFloat( ulong steamid, string name, float defaultValue = 0 )
        {
            float data = defaultValue;

            if ( !server.native.gameServerStats.GetUserStat0( steamid, name, out data ) )
                return defaultValue;

            return data;
        }
    }
}
