using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
        public bool IsBlocked { get; internal set; }

        /// <summary>
        ///  Return true if is a friend. Returns false if blocked, request etc.
        /// </summary>
        public bool IsFriend { get; internal set; }

        /// <summary>
        /// Their current display name
        /// </summary>
        public string Name;

        /// <summary>
        /// Returns true if this friend is online
        /// </summary>
        public bool IsOnline { get; internal set; }

        /// <summary>
        /// Returns true if this friend is online and playing this game
        /// </summary>
        public bool IsPlayingThisGame { get { return CurrentAppId == Client.AppId; } }

        /// <summary>
        /// Returns true if this friend is online and playing this game
        /// </summary>
        public bool IsPlaying { get { return CurrentAppId != 0; } }

        /// <summary>
        /// The AppId this guy is playing
        /// </summary>
        public ulong CurrentAppId { get; internal set; }

        public uint ServerIp { get; internal set; }
        public int ServerGamePort { get; internal set; }
        public int ServerQueryPort { get; internal set; }
        public ulong ServerLobbyId { get; internal set; }

        internal Client Client { get; set; }

        public void Refresh()
        {
            Name = Client.native.friends.GetFriendPersonaName( Id );

            SteamNative.FriendRelationship relationship = (SteamNative.FriendRelationship) Client.native.friends.GetFriendRelationship( Id );

            IsBlocked = relationship == SteamNative.FriendRelationship.Blocked;
            IsFriend = relationship == SteamNative.FriendRelationship.Friend;

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
        }
    }

    public class Friends
    {
        internal Client client;

        internal Friends( Client c )
        {
            client = c;
        }
        
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

        public Image GetAvatar( AvatarSize size, ulong steamid )
        {
            var imageid = 0;

            switch ( size )
            {
                case AvatarSize.Small:
                    imageid = client.native.friends.GetSmallFriendAvatar( steamid );
                    break;
                case AvatarSize.Medium:
                    imageid = client.native.friends.GetMediumFriendAvatar( steamid );
                    break;
                case AvatarSize.Large:
                    imageid = client.native.friends.GetLargeFriendAvatar( steamid );
                    break;
            }

            var img = new Image()
            {
                Id = imageid
            };

            if ( imageid == 0 )
                return img;

            if ( img.TryLoad( client.native.utils ) )
                return img;

            throw new System.NotImplementedException( "Deferred Avatar Loading Todo" );
            // Add to image loading list

            //return img;
        }


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
    }
}
