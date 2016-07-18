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
        ///  Return true if is a friend. Returns false if blocked, request etc.
        /// </summary>
        public bool IsFriend { get; internal set; }

        /// <summary>
        /// Returns true if this friend is playing a game (and we know about it)
        /// </summary>
        public bool IsPlaying { get; internal set; }

        /// <summary>
        /// If they're current in a game, what are they playing?
        /// </summary>
        public int CurrentAppId;

        /// <summary>
        /// Their current display name
        /// </summary>
        public string Name;
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

        public void Refresh()
        {
            _allFriends.Clear();

            //client.native.friends.GetFriendCount( 0 );

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

            uint width = 0;
            uint height = 0;

            if ( imageid != 0)
            {
                client.native.utils.GetImageSize( imageid, ref width, ref height );
            }

            return new Image()
            {
                Id = imageid,
                Width = (int)width,
                Height = (int)height
            };
        }
    }
}
