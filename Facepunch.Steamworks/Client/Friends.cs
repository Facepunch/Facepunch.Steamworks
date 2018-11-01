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
        private byte[] buffer = new byte[1024 * 128];

        internal Friends( Client c )
        {
            client = c;

            client.RegisterCallback<AvatarImageLoaded_t>( OnAvatarImageLoaded );
            client.RegisterCallback<PersonaStateChange_t>( OnPersonaStateChange );
            client.RegisterCallback<GameRichPresenceJoinRequested_t>( OnGameJoinRequested );
            client.RegisterCallback<GameConnectedFriendChatMsg_t>( OnFriendChatMessage );
        }

        public delegate void ChatMessageDelegate( SteamFriend friend, string type, string message );

        /// <summary>
        /// Called when chat message has been received from a friend. You'll need to turn on
        /// ListenForFriendsMessages to recieve this.
        /// </summary>
        public event ChatMessageDelegate OnChatMessage;

        private unsafe void OnFriendChatMessage( GameConnectedFriendChatMsg_t data )
        {
            if ( OnChatMessage == null ) return;

            var friend = Get( data.SteamIDUser );
            var type = ChatEntryType.ChatMsg;
            fixed ( byte* ptr = buffer )
            {
                var len = client.native.friends.GetFriendMessage( data.SteamIDUser, data.MessageID, (IntPtr)ptr, buffer.Length, out type );

                if ( len == 0 && type == ChatEntryType.Invalid )
                    return;

                var typeName = type.ToString();
                var message = Encoding.UTF8.GetString( buffer, 0, len );

                OnChatMessage( friend, typeName, message );
            }
        }

        private bool _listenForFriendsMessages;

        /// <summary>
        /// Listens for Steam friends chat messages.
        /// You can then show these chats inline in the game. For example with a Blizzard style chat message system or the chat system in Dota 2.
        /// After enabling this you will receive callbacks when ever the user receives a chat message.
        /// </summary>
        public bool ListenForFriendsMessages
        {
            get
            {
                return _listenForFriendsMessages;
            }

            set
            {
                _listenForFriendsMessages = value;
                client.native.friends.SetListenForFriendsMessages( value );
            }
        }


        public delegate void JoinRequestedDelegate( SteamFriend friend, string connect );

        //
        // Called when a friend has invited you to their game (using InviteToGame)
        //
        public event JoinRequestedDelegate OnInvitedToGame;


        private void OnGameJoinRequested( GameRichPresenceJoinRequested_t data )
        {
            if ( OnInvitedToGame != null )
            {
                OnInvitedToGame( Get( data.SteamIDFriend ), data.Connect );
            }
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

            if ( imageid == 1 ) return null; // Placeholder large
            if ( imageid == 2 ) return null; // Placeholder medium
            if ( imageid == 3 ) return null; // Placeholder small

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
                // from docs: false means that we already have all the details about that user, and functions that require this information can be used immediately
                // but that's probably not true because we just checked the cache

                // check again in case it was just a race
                image = GetCachedAvatar(size, steamid);
                if ( image != null )
                {
                    callback(image);
                    return;
                }

                // maybe Steam returns false if it was already requested? just add another callback and hope it comes
                // if not it'll time out anyway
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
            var friend = All.Where( x => x.Id == steamid ).FirstOrDefault();
            if ( friend != null ) return friend;

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

            for ( int i = PersonaCallbacks.Count-1; i >= 0; i-- )
            {
                var cb = PersonaCallbacks[i];

                // Timeout
                if ( cb.Time < timeOut )
                {
                    if ( cb.Callback != null )
                    {
                        // final attempt, will be null unless the callback was missed somehow
                        var image = GetCachedAvatar( cb.Size, cb.SteamId );

                        cb.Callback( image );
                    }

                    PersonaCallbacks.Remove( cb );
                    continue;
                }
            }
        }

        
        private void OnPersonaStateChange( PersonaStateChange_t data )
        {
            // k_EPersonaChangeAvatar	
            if ( (data.ChangeFlags & 0x0040) == 0x0040 )
            {
                LoadAvatarForSteamId( data.SteamID );
            }

            //
            // Find and refresh this friend's status
            //
            foreach ( var friend in All )
            {
                if ( friend.Id != data.SteamID ) continue;

                friend.Refresh();
            }
        }

        void LoadAvatarForSteamId( ulong Steamid )
        {
            for ( int i = PersonaCallbacks.Count - 1; i >= 0; i-- )
            {
                var cb = PersonaCallbacks[i];
                if ( cb.SteamId != Steamid ) continue;

                var image = GetCachedAvatar( cb.Size, cb.SteamId );
                if ( image == null ) continue;

                PersonaCallbacks.Remove( cb );

                if ( cb.Callback != null )
                {
                    cb.Callback( image );
                }
            }
        }

        private void OnAvatarImageLoaded( AvatarImageLoaded_t data )
        {
            LoadAvatarForSteamId( data.SteamID );
        }

    }
}
