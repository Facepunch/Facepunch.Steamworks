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

        public void Dispose()
        {
            client = null;
        }
    }
}
