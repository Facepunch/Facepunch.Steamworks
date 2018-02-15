using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SteamNative;

namespace Facepunch.Steamworks
{
    public partial class Client : IDisposable
    {
        Friends _friends;

        public Friends Friends
        {
            get
            {
                if ( _friends == null )
                    _friends = new Friends( this );

                return _friends;
            }
        }
    }

    public class SteamFriend
    {
        /// <summary>
        /// Steam Id
        /// </summary>
        public ulong Id { get; internal set; }


        /// <summary>
        ///  Return true if blocked
        /// </summary>
        public bool IsBlocked => relationship == FriendRelationship.Blocked;

        /// <summary>
        ///  Return true if is a friend. Returns false if blocked, request etc.
        /// </summary>
        public bool IsFriend => relationship == FriendRelationship.Friend;

        /// <summary>
        /// Their current display name
        /// </summary>
        public string Name;

        /// <summary>
        /// Returns true if this friend is online
        /// </summary>
        public bool IsOnline => personaState != PersonaState.Offline;

        /// <summary>
        /// Returns true if this friend is marked as away
        /// </summary>
        public bool IsAway => personaState == PersonaState.Away; 

        /// <summary>
        /// Returns true if this friend is marked as busy
        /// </summary>
        public bool IsBusy => personaState == PersonaState.Busy;

        /// <summary>
        /// Returns true if this friend is marked as snoozing
        /// </summary>
        public bool IsSnoozing => personaState == PersonaState.Snooze;

        /// <summary>
        /// Returns true if this friend is online and playing this game
        /// </summary>
        public bool IsPlayingThisGame => CurrentAppId == Client.AppId;

        /// <summary>
        /// Returns true if this friend is online and playing this game
        /// </summary>
        public bool IsPlaying => CurrentAppId != 0;

        /// <summary>
        /// The AppId this guy is playing
        /// </summary>
        public ulong CurrentAppId { get; internal set; }

        public uint ServerIp { get; internal set; }
        public int ServerGamePort { get; internal set; }
        public int ServerQueryPort { get; internal set; }
        public ulong ServerLobbyId { get; internal set; }

        internal Client Client { get; set; }
        private PersonaState personaState;
        private FriendRelationship relationship;

        /// <summary>
        /// Returns null if the value doesn't exist
        /// </summary>
        public string GetRichPresence( string key )
        {
            var val = Client.native.friends.GetFriendRichPresence( Id, key );
            if ( string.IsNullOrEmpty( val ) ) return null;
            return val;
        }

        /// <summary>
        /// Update this friend, request the latest data from Steam's servers
        /// </summary>
        public void Refresh()
        {
            Name = Client.native.friends.GetFriendPersonaName( Id );

            relationship = Client.native.friends.GetFriendRelationship( Id );
            personaState = Client.native.friends.GetFriendPersonaState( Id );

            CurrentAppId = 0;
            ServerIp = 0;
            ServerGamePort = 0;
            ServerQueryPort = 0;
            ServerLobbyId = 0;

            var gameInfo = new SteamNative.FriendGameInfo_t();
            if ( Client.native.friends.GetFriendGamePlayed( Id, ref gameInfo ) && gameInfo.GameID > 0 )
            {
                CurrentAppId = gameInfo.GameID;
                ServerIp = gameInfo.GameIP;
                ServerGamePort = gameInfo.GamePort;
                ServerQueryPort = gameInfo.QueryPort;
                ServerLobbyId = gameInfo.SteamIDLobby;
            }

            Client.native.friends.RequestFriendRichPresence( Id );
        }

        /// <summary>
        /// This will return null if you don't have the target user's avatar in your cache.
        /// Which usually happens for people not on your friends list.
        /// </summary>
        public Image GetAvatar( Friends.AvatarSize size )
        {
            return Client.Friends.GetCachedAvatar( size, Id );
        }
        
        /// <summary>
        /// Invite this friend to the game that we are playing
        /// </summary>
        public bool InviteToGame(string Text)
        {
            return Client.native.friends.InviteUserToGame(Id, Text);
        }
    }

    /// <summary>
    /// Handles most interactions with people in Steam, not just friends as the name would suggest.
    /// </summary>
    /// <example>
    /// foreach ( var friend in client.Friends.AllFriends )
    /// {
    ///     Console.WriteLine( $"{friend.Id}: {friend.Name}" );
    /// }
    /// </example>
    public class Friends
    {
        internal Client client;

        internal Friends( Client c )
        {
            client = c;

            client.RegisterCallback<SteamNative.PersonaStateChange_t>( OnPersonaStateChange );
        }

        /// <summary>
        /// Try to get information about this user - which as name and avatar.
        /// If returns true, we already have this user's information.
        /// </summary>
        public bool UpdateInformation( ulong steamid )
        {
            return !client.native.friends.RequestUserInformation( steamid, false );
        }
        
        /// <summary>
        /// Get this user's name
        /// </summary>
        public string GetName( ulong steamid )
        {
            client.native.friends.RequestUserInformation( steamid, true );
            return client.native.friends.GetFriendPersonaName( steamid );
        }

        private List<SteamFriend> _allFriends;

        /// <summary>
        /// Returns all friends, even blocked, ignored, friend requests etc
        /// </summary>
        public IEnumerable<SteamFriend> All
        {
            get
            {
                if ( _allFriends == null )
                {
                    _allFriends = new List<SteamFriend>();
                    Refresh();
                }

                return _allFriends;
            }
        }

        /// <summary>
        /// Returns only friends
        /// </summary>
        public IEnumerable<SteamFriend> AllFriends
        {
            get
            {
                foreach ( var friend in All )
                {
                    if ( !friend.IsFriend ) continue;

                    yield return friend;
                }
            }
        }

        /// <summary>
        /// Returns all blocked users
        /// </summary>
        public IEnumerable<SteamFriend> AllBlocked
        {
            get
            {
                foreach ( var friend in All )
                {
                    if ( !friend.IsBlocked ) continue;

                    yield return friend;
                }
            }
        }

        public void Refresh()
        {
            if ( _allFriends == null )
            {
                _allFriends = new List<SteamFriend>();
            }

            _allFriends.Clear();

            var flags = (int) SteamNative.FriendFlags.All;
            var count = client.native.friends.GetFriendCount( flags );

            for ( int i=0; i<count; i++ )
            {
                var steamid = client.native.friends.GetFriendByIndex( i, flags );
                _allFriends.Add( Get( steamid ) );
            }
        }

        public enum AvatarSize
        {
            /// <summary>
            /// Should be 32x32 - but make sure to check!
            /// </summary>
            Small,

            /// <summary>
            /// Should be 64x64 - but make sure to check!
            /// </summary>
            Medium,

            /// <summary>
            /// Should be 184x184 - but make sure to check!
            /// </summary>
            Large
        }

        /// <summary>
        /// Try to get the avatar immediately. This should work for people on your friends list.
        /// </summary>
        public Image GetCachedAvatar( AvatarSize size, ulong steamid )
        {
            var imageid = 0;

            switch (size)
            {
                case AvatarSize.Small:
                    imageid = client.native.friends.GetSmallFriendAvatar(steamid);
                    break;
                case AvatarSize.Medium:
                    imageid = client.native.friends.GetMediumFriendAvatar(steamid);
                    break;
                case AvatarSize.Large:
                    imageid = client.native.friends.GetLargeFriendAvatar(steamid);
                    break;
            }

            if ( imageid == 0 || imageid == 2 )
                return null;

            var img = new Image()
            {
                Id = imageid
            };

            if ( !img.TryLoad( client.native.utils ) )
                return null;

            return img;
        }


        /// <summary>
        /// Callback will be called when the avatar is ready. If we fail to get an
        /// avatar, might be called with a null Image.
        /// </summary>
        public void GetAvatar( AvatarSize size, ulong steamid, Action<Image> callback )
        {
            // Maybe we already have it downloaded?
            var image = GetCachedAvatar(size, steamid);
            if ( image != null )
            {
                callback(image);
                return;
            }

            // Lets request it from Steam
            if (!client.native.friends.RequestUserInformation(steamid, false))
            {
                // Steam told us to get fucked
                callback(null);
                return;
            }

            PersonaCallbacks.Add( new PersonaCallback
            {
                SteamId = steamid,
                Size = size,
                Callback = callback,
                Time = DateTime.Now
            });
        }

        private class PersonaCallback
        {
            public ulong SteamId;
            public AvatarSize Size;
            public Action<Image> Callback;
            public DateTime Time;
        }

        List<PersonaCallback> PersonaCallbacks = new List<PersonaCallback>();

        public SteamFriend Get( ulong steamid )
        {
            var f = new SteamFriend()
            {
                Id = steamid,
                Client = client
            };

            f.Refresh();

            return f;
        }

        internal void Cycle()
        {
            if ( PersonaCallbacks.Count == 0 ) return;

            var timeOut = DateTime.Now.AddSeconds( -10 );

            for ( int i = 0; i < PersonaCallbacks.Count; i++ )
            {
                var cb = PersonaCallbacks[i];

                // Timeout
                if ( cb.Time < timeOut )
                {
                    if ( cb.Callback != null )
                    {
                        cb.Callback( null );
                    }

                    PersonaCallbacks.Remove( cb );
                    continue;
                }
            }
        }

        private void OnPersonaStateChange( PersonaStateChange_t data )
        {
            if ( (data.ChangeFlags & 0x0040) != 0x0040 ) return; // wait for k_EPersonaChangeAvatar	

            for ( int i=0; i< PersonaCallbacks.Count; i++ )
            {
                var cb = PersonaCallbacks[i];
                if ( cb.SteamId != data.SteamID ) continue;

                var image = GetCachedAvatar( cb.Size, cb.SteamId );
                if ( image == null ) continue;

                PersonaCallbacks.Remove( cb );

                if ( cb.Callback != null )
                {
                    cb.Callback( image );
                }
            }
        }

    }
}
