using SteamNative;

namespace Facepunch.Steamworks
{
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

        /// <summary>
        /// Sends a message to a Steam friend. Returns true if success
        /// </summary>
        public bool SendMessage( string message )
        {
            return Client.native.friends.ReplyToFriendMessage( Id, message );
        }
    }
}
