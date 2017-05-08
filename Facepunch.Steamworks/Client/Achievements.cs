using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SteamNative;

namespace Facepunch.Steamworks
{
    public class Achievements : IDisposable
    {
        internal Client client;

        public Achievement[] All { get; private set; }

        public event Action OnUpdated;

        internal Achievements( Client c )
        {
            client = c;

            All = new Achievement[0];

            SteamNative.UserStatsReceived_t.RegisterCallback( c, UserStatsReceived );
        }

        public void Refresh()
        {
            All = Enumerable.Range( 0, (int)client.native.userstats.GetNumAchievements() )
                .Select( x => new Achievement( client, x ) )
                .ToArray();
        }

        public void Dispose()
        {
            client = null;
        }

        private void UserStatsReceived( UserStatsReceived_t stats, bool isError )
        {
            if ( isError ) return;

            Refresh();

            OnUpdated?.Invoke();
        }
    }

    public class Achievement
    {
        private Client client;

        public string Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }

        /// <summary>
        /// True if unlocked
        /// </summary>
        public bool State { get; private set; }

        /// <summary>
        /// Should hold the unlock time if State is true
        /// </summary>
        public DateTime UnlockTime { get; private set; }

        /// <summary>
        /// If this achievement is linked to a stat this will return the progress.
        /// </summary>
        public float Percentage
        {
            get
            {
                if ( State )
                    return 1;

                float pct = 0;

                if ( !client.native.userstats.GetAchievementAchievedPercent( Id, out pct ) )
                    return -1.0f;

                return pct;
            }
        }

        public Achievement( Client client, int index )
        {
            this.client = client;

            Id = client.native.userstats.GetAchievementName( (uint) index );
            Name = client.native.userstats.GetAchievementDisplayAttribute( Id, "name" );
            Description = client.native.userstats.GetAchievementDisplayAttribute( Id, "desc" );

            Refresh();
        }

        /// <summary>
        /// Make this achievement earned
        /// </summary>
        public bool Trigger()
        {
            State = true;
            UnlockTime = DateTime.Now;

            return client.native.userstats.SetAchievement( Id );
        }

        /// <summary>
        /// Reset this achievement to not achieved
        /// </summary>
        public bool Reset()
        {
            State = false;
            UnlockTime = DateTime.Now;

            return client.native.userstats.ClearAchievement( Id );
        }

        /// <summary>
        /// Refresh the unlock state. You shouldn't need to call this manually
        /// but it's here if you have to for some reason.
        /// </summary>
        public void Refresh()
        {
            bool state = false;
            uint unlockTime;

            State = false;

            if ( client.native.userstats.GetAchievementAndUnlockTime( Id, ref state, out unlockTime ) )
            {
                State = state;
                UnlockTime = Utility.Epoch.ToDateTime( unlockTime );
            }
        }
    }

}
