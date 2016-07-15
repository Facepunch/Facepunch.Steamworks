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
            return 0;
        }

        public int GetGlobalInt( string name )
        {
            return 0;
        }

        public int GetFloat( string name )
        {
            return 0;
        }

        public int GetGlobalFloat( string name )
        {
            return 0;
        }

    }
}
