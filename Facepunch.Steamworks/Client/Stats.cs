using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Facepunch.Steamworks
{
    public partial class Client : IDisposable
    {
        Stats _stats;

        public Stats Stats
        {
            get
            {
                if ( _stats == null )
                    _stats = new Stats( this );

                return _stats;
            }
        }
    }

    public class Stats
    {
        internal Client client;

        internal Stats( Client c )
        {
            client = c;
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

    }
}
