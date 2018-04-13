﻿using System;
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
        public event Action<Achievement> OnAchievementStateChanged;

        private List<Achievement> unlockedRecently = new List<Achievement>();

        internal Achievements( Client c )
        {
            client = c;

            All = new Achievement[0];
            c.RegisterCallback<UserStatsReceived_t>( UserStatsReceived );
            c.RegisterCallback<UserStatsStored_t>( UserStatsStored );

            Refresh();
        }

        public void Refresh()
        {
            var old = All;

            All = Enumerable.Range( 0, (int)client.native.userstats.GetNumAchievements() )
                .Select( x =>
                {
                    if ( old != null )
                    {
                        var name = client.native.userstats.GetAchievementName( (uint)x );
                        var found = old.FirstOrDefault( y => y.Id == name );
                        if ( found != null )
                        {
                            if ( found.Refresh() )
                            {
                                unlockedRecently.Add( found );
                            }
                            return found;
                        }
                    }

                    return new Achievement( client, x );
                } )
                .ToArray();

            foreach ( var i in unlockedRecently )
            {
                OnUnlocked( i );
            }

            unlockedRecently.Clear();
        }

        internal void OnUnlocked( Achievement a )
        {
            OnAchievementStateChanged?.Invoke( a );
        }

        public void Dispose()
        {
            client = null;
        }

        /// <summary>
        /// Find an achievement by name. Will be null if not found, or not ready.
        /// </summary>
        public Achievement Find( string identifier )
        {
            return All.FirstOrDefault( x => x.Id == identifier );
        }

        /// <summary>
        /// Unlock an achievement by identifier. If apply is true this will happen as expected
        /// and the achievement overlay will popup etc. If it's false then you'll have to manually
        /// call Stats.StoreStats() to actually trigger it.
        /// </summary>
        public bool Trigger( string identifier, bool apply = true )
        {
            var a = Find( identifier );
            if ( a == null ) return false;

            return a.Trigger( apply );
        }

        /// <summary>
        /// Reset an achievement by identifier
        /// </summary>
        public bool Reset( string identifier )
        {
            return client.native.userstats.ClearAchievement( identifier );
        }

        private void UserStatsReceived( UserStatsReceived_t stats )
        {
            if ( stats.GameID != client.AppId ) return;

            Refresh();

            OnUpdated?.Invoke();
        }

        private void UserStatsStored( UserStatsStored_t stats )
        {
            if ( stats.GameID != client.AppId ) return;

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

        private int iconId { get; set; } = -1;
        private int refreshCount = 0;

        /// <summary>
        /// Returns the percentage of users who have unlocked the specified achievement, or -1 if no data available.
        /// </summary>
        public float GlobalUnlockedPercentage
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

        private Image _icon;

        public Image Icon
        {
            get
            {
                if ( iconId  <= 0 ) return null;

                if ( _icon == null )
                {
                    _icon = new Image();
                    _icon.Id = iconId;
                }

                if ( _icon.IsLoaded )
                    return _icon;

                if ( !_icon.TryLoad( client.native.utils ) )
                    return null;

                return _icon;
            }
        }

        public Achievement( Client client, int index )
        {
            this.client = client;

            Id = client.native.userstats.GetAchievementName( (uint) index );
            Name = client.native.userstats.GetAchievementDisplayAttribute( Id, "name" );
            Description = client.native.userstats.GetAchievementDisplayAttribute( Id, "desc" );

            iconId = client.native.userstats.GetAchievementIcon( Id );

            Refresh();
        }

        /// <summary>
        /// Make this achievement earned
        /// </summary>
        public bool Trigger( bool apply = true )
        {
            if ( State )
                return false;

            State = true;
            UnlockTime = DateTime.Now;

            var r = client.native.userstats.SetAchievement( Id );

            if ( apply )
            {
                client.Stats.StoreStats();
            }

            client.Achievements.OnUnlocked( this );

            return r;
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
        /// but it's here if you have to for some reason. Retuns true if state changed (meaning, probably unlocked)
        /// </summary>
        public bool Refresh()
        {
            bool previousState = State;

            bool state = false;
            uint unlockTime;

            State = false;

            if ( client.native.userstats.GetAchievementAndUnlockTime( Id, ref state, out unlockTime ) )
            {
                State = state;
                UnlockTime = Utility.Epoch.ToDateTime( unlockTime );
            }

            refreshCount++;

            if ( previousState != State && refreshCount > 1 )
            {
                return true;
            }

            return false;            
        }
    }

}
