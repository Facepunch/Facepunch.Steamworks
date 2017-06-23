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
        /// Sets client stat integer
        /// You should call StoreStats() manually after this
        /// </summary>
        public bool SetInt( string name, int data )
        {
            return client.native.userstats.SetStat( name, data );
        }

        /// <summary>
        /// Sets client stat float
        /// You should call StoreStats() manually after this
        /// </summary>
        public bool SetFloat( string name, float data )
        {
            return client.native.userstats.SetStat0( name, data );
        }

        /// <summary>
        /// Sets client stat integer and store it
        /// Retruns true if stat was successfully stored
        /// </summary>
        public bool SetIntAndStore( string name, int data )
        {
            bool saveInt = client.native.userstats.SetStat( name, data );
            if ( saveInt )
            {
                return client.native.userstats.StoreStats();
            }
            return false;
        }

        /// <summary>
        /// Sets client stat float and store it
        /// Retruns true if stat was successfully stored
        /// </summary>
        public bool SetFloatAndStore( string name, float data )
        {
            bool saveFloat = client.native.userstats.SetStat0( name, data );
            if ( saveFloat )
            {
                return client.native.userstats.StoreStats();
            }
            return false;
        }

        public void Dispose()
        {
            client = null;
        }
    }
}
