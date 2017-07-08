using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Facepunch.Steamworks
{
    public class Stats : IDisposable
    {
        internal Client client;

        internal Stats( Client c )
        {
            client = c;
        }

		public bool StoreStats()
		{
			return client.native.userstats.StoreStats();
		}

        public void UpdateStats()
        {
            client.native.userstats.RequestCurrentStats();
        }

        public void UpdateGlobalStats( int days = 1 )
        {
            client.native.userstats.GetNumberOfCurrentPlayers();
            client.native.userstats.RequestGlobalAchievementPercentages();
            client.native.userstats.RequestGlobalStats( days );
        }

        public int GetInt( string name )
        {
            int data = 0;
            client.native.userstats.GetStat( name, out data );
            return data;
        }

        public long GetGlobalInt( string name )
        {
            long data = 0;
            client.native.userstats.GetGlobalStat( name, out data );
            return data;
        }

        public float GetFloat( string name )
        {
            float data = 0;
            client.native.userstats.GetStat0( name, out data );
            return data;
        }

        public double GetGlobalFloat( string name )
        {
            double data = 0;
            client.native.userstats.GetGlobalStat0( name, out data );
            return data;
        }

        /// <summary>
        /// Set a stat value. This will automatically call StoreStats() after a successful call
        /// unless you pass false as the last argument.
        /// </summary>
        public bool Set( string name, int value, bool store = true )
        {
            var r = client.native.userstats.SetStat( name, value );

            if ( store )
            {
                return r && client.native.userstats.StoreStats();
            }

            return r;
        }

        /// <summary>
        /// Set a stat value. This will automatically call StoreStats() after a successful call
        /// unless you pass false as the last argument.
        /// </summary>
        public bool Set( string name, float value, bool store = true )
        {
            var r = client.native.userstats.SetStat0( name, value );

            if ( store )
            {
                return r && client.native.userstats.StoreStats();
            }

            return r;
        }

        /// <summary>
        /// Adds this amount to the named stat. Internally this calls Get() and adds 
        /// to that value. Steam doesn't provide a mechanism for atomically increasing
        /// stats like this, this functionality is added here as a convenience.
        /// </summary>
        public bool Add( string name, int amount = 1, bool store = true )
        {
            var val = GetInt( name );
            val += amount;
            return Set( name, val, store );
        }

        /// <summary>
        /// Practically wipes the slate clean for this user. If includeAchievements is true, will wipe
        /// any achievements too.
        /// </summary>
        /// <returns></returns>
        public bool ResetAll( bool includeAchievements )
        {
            return client.native.userstats.ResetAllStats( includeAchievements );
        }

        public void Dispose()
        {
            client = null;
        }
    }
}
