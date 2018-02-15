using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Facepunch.Steamworks
{
    /// <summary>
    /// Allows getting and setting stats on users from the gameserver. These stats
    /// should have been set up on the Steamworks website for your app.
    /// </summary>
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
        /// Retrieve the stats for this user. If you pass a callback function in
        /// this will be called when the stats are recieved, the bool will signify whether
        /// it was successful or not.
        /// </summary>
        public void Refresh( ulong steamid, Action<ulong, bool> Callback = null )
        {
            if ( Callback == null )
            {
                server.native.gameServerStats.RequestUserStats( steamid );
                return;
            }

            server.native.gameServerStats.RequestUserStats( steamid, ( o, failed ) =>
            {
                Callback( steamid, o.Result == SteamNative.Result.OK && !failed );
            } );
        }

        /// <summary>
        /// Once you've set a stat change on a user you need to commit your changes.
        /// You can do that using this function. The callback will let you know if
        /// your action succeeded, but most of the time you can fire and forget.
        /// </summary>
        public void Commit( ulong steamid, Action<ulong, bool> Callback = null )
        {
            if ( Callback == null )
            {
                server.native.gameServerStats.StoreUserStats( steamid );
                return;
            }

            server.native.gameServerStats.StoreUserStats( steamid, ( o, failed ) =>
            {
                Callback( steamid, o.Result == SteamNative.Result.OK && !failed );
            } );
        }

        /// <summary>
        /// Set the named stat for this user. Setting stats should follow the rules
        /// you defined in Steamworks.
        /// </summary>
        public bool SetInt( ulong steamid, string name, int stat )
        {
            return server.native.gameServerStats.SetUserStat( steamid, name, stat );
        }

        /// <summary>
        /// Set the named stat for this user. Setting stats should follow the rules
        /// you defined in Steamworks.
        /// </summary>
        public bool SetFloat( ulong steamid, string name, float stat )
        {
            return server.native.gameServerStats.SetUserStat0( steamid, name, stat );
        }

        /// <summary>
        /// Get the named stat for this user. If getting the stat failed, will return
        /// defaultValue. You should have called Refresh for this userid - which downloads
        /// the stats from the backend. If you didn't call it this will always return defaultValue.
        /// </summary>
        public int GetInt( ulong steamid, string name, int defaultValue = 0 )
        {
            int data = defaultValue;

            if ( !server.native.gameServerStats.GetUserStat( steamid, name, out data ) )
                return defaultValue;

            return data;
        }

        /// <summary>
        /// Get the named stat for this user. If getting the stat failed, will return
        /// defaultValue. You should have called Refresh for this userid - which downloads
        /// the stats from the backend. If you didn't call it this will always return defaultValue.
        /// </summary>
        public float GetFloat( ulong steamid, string name, float defaultValue = 0 )
        {
            float data = defaultValue;

            if ( !server.native.gameServerStats.GetUserStat0( steamid, name, out data ) )
                return defaultValue;

            return data;
        }

        /// <summary>
        /// Resets the unlock status of an achievement for the specified user. Must have called Refresh on a steamid first.
        /// </summary>
        public bool ClearAchievement( ulong steamid, string name )
        {
            return server.native.gameServerStats.ClearUserAchievement( steamid, name );
        }

        /// <summary>
        /// Return true if available, exists and unlocked
        /// </summary>
        public bool GetAchievement( ulong steamid, string name )
        {
            bool achieved = false;

            if ( !server.native.gameServerStats.GetUserAchievement( steamid, name, ref achieved ) )
                return false;

            return achieved;
        }
    }
}
