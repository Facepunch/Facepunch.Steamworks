﻿using System;
using System.Collections.Generic;
using System.Text;
using SteamNative;

namespace Facepunch.Steamworks
{
    public partial class Client : IDisposable
    {
        Lobby _lobby;

        public Lobby Lobby
        {
            get
            {
                if ( _lobby == null )
                    _lobby = new Steamworks.Lobby( this );
                return _lobby;
            }
        }
    }
    public partial class Lobby : IDisposable
    {
        //The type of lobby you are creating
        public enum Type : int
        {
            Private = SteamNative.LobbyType.Private,
            FriendsOnly = SteamNative.LobbyType.FriendsOnly,
            Public = SteamNative.LobbyType.Public, 
            Invisible = SteamNative.LobbyType.Invisible,
            Error //happens if you try to get this when you aren't in a valid lobby
        }

        internal Client client;

        public Lobby( Client c )
        {
            client = c;

            // For backwards compatibility
            OnLobbyJoinRequested = Join;

            client.RegisterCallback<SteamNative.LobbyDataUpdate_t>( OnLobbyDataUpdatedAPI );
            client.RegisterCallback<SteamNative.LobbyChatMsg_t>( OnLobbyChatMessageRecievedAPI );
            client.RegisterCallback<SteamNative.LobbyChatUpdate_t>( OnLobbyStateUpdatedAPI );
            client.RegisterCallback<SteamNative.GameLobbyJoinRequested_t>( OnLobbyJoinRequestedAPI );
            client.RegisterCallback<SteamNative.LobbyInvite_t>( OnUserInvitedToLobbyAPI );
            client.RegisterCallback<SteamNative.PersonaStateChange_t>( OnLobbyMemberPersonaChangeAPI );
        }

        /// <summary>
        /// The CSteamID of the lobby we're currently in.
        /// </summary>
        public ulong CurrentLobby { get; private set; }

        /// <summary>
        /// The LobbyData of the CurrentLobby. Note this is the global data for the lobby. Use SetMemberData to set specific member data.
        /// </summary>
        public LobbyData CurrentLobbyData { get; private set; }

        /// <summary>
        /// Returns true if this lobby is valid, ie, we've succesffuly created and/or joined a lobby.
        /// </summary>
        public bool IsValid => CurrentLobby != 0;

        /// <summary>
        /// Join a Lobby through its LobbyID. OnLobbyJoined is called with the result of the Join attempt.
        /// </summary>
        /// <param name="lobbyID">CSteamID of lobby to join</param>
        public void Join( ulong lobbyID )
        {
            Leave();
            client.native.matchmaking.JoinLobby( lobbyID, OnLobbyJoinedAPI );
        }

        void OnLobbyJoinedAPI( LobbyEnter_t callback, bool error )
        {
            if ( error || (callback.EChatRoomEnterResponse != (uint)(SteamNative.ChatRoomEnterResponse.Success)) )
            {
                if ( OnLobbyJoined != null ) { OnLobbyJoined( false ); }
                return;
            }

            CurrentLobby = callback.SteamIDLobby;
            UpdateLobbyData();
            if ( OnLobbyJoined != null ) { OnLobbyJoined( true ); }
        }

        /// <summary>
        /// Called when a lobby has been attempted joined. Returns true if lobby was successfuly joined, false if not.
        /// </summary>
        public Action<bool> OnLobbyJoined;

        /// <summary>
        /// Creates a lobby and returns the created lobby. You auto join the created lobby. The lobby is stored in Client.Lobby.CurrentLobby if successful.
        /// </summary>
        /// <param name="lobbyType">The Lobby.Type of Lobby to be created</param>
        /// <param name="maxMembers">The maximum amount of people you want to be able to be in this lobby, including yourself</param>
        public void Create( Lobby.Type lobbyType, int maxMembers )
        {
            client.native.matchmaking.CreateLobby( (SteamNative.LobbyType)lobbyType, maxMembers, OnLobbyCreatedAPI );
            createdLobbyType = lobbyType;
        }

        internal Type createdLobbyType;

        internal void OnLobbyCreatedAPI( LobbyCreated_t callback, bool error )
        {
            //from SpaceWarClient.cpp 793
            if ( error || (callback.Result != Result.OK) )
            {
                if ( OnLobbyCreated != null ) { OnLobbyCreated( false ); }
                return;
            }

            //set owner specific properties
            Owner = client.SteamId;
            CurrentLobby = callback.SteamIDLobby;
            CurrentLobbyData = new LobbyData( client, CurrentLobby );
            Name = client.Username + "'s Lobby";
            CurrentLobbyData.SetData( "appid", client.AppId.ToString() );
            LobbyType = createdLobbyType;
            CurrentLobbyData.SetData( "lobbytype", LobbyType.ToString() );
            Joinable = true;
            if ( OnLobbyCreated != null ) { OnLobbyCreated( true ); }
        }

        /// <summary>
        /// Callback for when lobby is created. Parameter resolves true when the Lobby was successfully created
        /// </summary>
        public Action<bool> OnLobbyCreated;

        /// <summary>
        /// Sets user data for the Lobby. Things like Character, Skin, Ready, etc. Can only set your own member data
        /// </summary>
        public void SetMemberData( string key, string value )
        {
            if ( CurrentLobby == 0 ) { return; }
            client.native.matchmaking.SetLobbyMemberData( CurrentLobby, key, value );
        }

        /// <summary>
        /// Get the per-user metadata from this lobby. Can get data from any user
        /// </summary>
        /// <param name="steamID">ulong SteamID of the user you want to get data from</param>
        /// <param name="key">String key of the type of data you want to get</param>
        /// <returns></returns>
        public string GetMemberData( ulong steamID, string key )
        {
            if ( CurrentLobby == 0 ) { return "ERROR: NOT IN ANY LOBBY"; }
            return client.native.matchmaking.GetLobbyMemberData( CurrentLobby, steamID, key );
        }

        internal void OnLobbyDataUpdatedAPI( LobbyDataUpdate_t callback )
        {
            if ( callback.SteamIDLobby != CurrentLobby ) return;

            if ( callback.SteamIDLobby == CurrentLobby ) //actual lobby data was updated by owner
            {
                UpdateLobbyData();
            }

            if ( UserIsInCurrentLobby( callback.SteamIDMember ) ) //some member of this lobby updated their information
            {
                if ( OnLobbyMemberDataUpdated != null ) { OnLobbyMemberDataUpdated( callback.SteamIDMember ); }
            }
        }

        /// <summary>
        /// Updates the LobbyData property to have the data for the current lobby, if any
        /// </summary>
        internal void UpdateLobbyData()
        {
            int dataCount = client.native.matchmaking.GetLobbyDataCount( CurrentLobby );
            CurrentLobbyData = new LobbyData( client, CurrentLobby );
            for ( int i = 0; i < dataCount; i++ )
            {
                if ( client.native.matchmaking.GetLobbyDataByIndex( CurrentLobby, i, out string key, out string value ) )
                {
                    CurrentLobbyData.SetData( key, value );
                }
            }

            if ( OnLobbyDataUpdated != null ) { OnLobbyDataUpdated(); }
        }

        /// <summary>
        /// Called when the lobby data itself has been updated. Called when someone has joined/left, Owner has updated data, etc.
        /// </summary>
        public Action OnLobbyDataUpdated;

        /// <summary>
        /// Called when a member of the lobby has updated either their personal Lobby metadata or someone's global steam state has changed (like a display name). Parameter is the user who changed.
        /// </summary>
        public Action<ulong> OnLobbyMemberDataUpdated;


        public Type LobbyType
        {
            get
            {
                if ( !IsValid ) { return Type.Error; } //if we're currently in a valid server

                //we know that we've set the lobby type via the lobbydata in the creation function
                //ps this is important because steam doesn't have an easy way to get lobby type (why idk)
                string lobbyType = CurrentLobbyData.GetData( "lobbytype" );
                switch ( lobbyType )
                {
                    case "Private":
                        return Type.Private;
                    case "FriendsOnly":
                        return Type.FriendsOnly;
                    case "Invisible":
                        return Type.Invisible;
                    case "Public":
                        return Type.Public;
                    default:
                        return Type.Error;
                }
            }
            set
            {
                if ( !IsValid ) { return; }
                if ( client.native.matchmaking.SetLobbyType( CurrentLobby, (SteamNative.LobbyType)value ) )
                {
                    CurrentLobbyData.SetData( "lobbytype", value.ToString() );
                }
            }
        }

        private static byte[] chatMessageData = new byte[1024 * 4];

        private unsafe void OnLobbyChatMessageRecievedAPI( LobbyChatMsg_t callback )
        {
            //from Client.Networking
            if ( callback.SteamIDLobby != CurrentLobby )
                return;

            SteamNative.CSteamID steamid = 1;
            ChatEntryType chatEntryType; // "If set then this will just always return k_EChatEntryTypeChatMsg. This can usually just be set to NULL."
            int readData = 0;
            fixed ( byte* p = chatMessageData )
            {
                readData = client.native.matchmaking.GetLobbyChatEntry( CurrentLobby, (int)callback.ChatID, out steamid, (IntPtr)p, chatMessageData.Length, out chatEntryType );
            }


            OnChatMessageRecieved?.Invoke( steamid, chatMessageData, readData );

            if ( readData > 0 )
            {
                OnChatStringRecieved?.Invoke( steamid, Encoding.UTF8.GetString( chatMessageData, 0, readData ) );
            }
        }

        /// <summary>
        /// Callback to get chat messages. Use Encoding.UTF8.GetString to retrive the message.
        /// </summary>
        public Action<ulong, byte[], int> OnChatMessageRecieved;

        /// <summary>
        /// Like OnChatMessageRecieved but the data is converted to a string
        /// </summary>
        public Action<ulong, string> OnChatStringRecieved;

        /// <summary>
        /// Broadcasts a chat message to the all the users in the lobby users in the lobby (including the local user) will receive a LobbyChatMsg_t callback.
        /// </summary>
        /// <returns>True if message successfully sent</returns>
        public unsafe bool SendChatMessage( string message )
        {
            var data = Encoding.UTF8.GetBytes( message );
            fixed ( byte* p = data )
            {
                // pvMsgBody can be binary or text data, up to 4k
                // if pvMsgBody is text, cubMsgBody should be strlen( text ) + 1, to include the null terminator
                return client.native.matchmaking.SendLobbyChatMsg( CurrentLobby, (IntPtr)p, data.Length );
            }
        }

        /// <summary>
        /// Enums to catch the state of a user when their state has changed
        /// </summary>
        public enum MemberStateChange
        {
            Entered = ChatMemberStateChange.Entered,
            Left = ChatMemberStateChange.Left,
            Disconnected = ChatMemberStateChange.Disconnected,
            Kicked = ChatMemberStateChange.Kicked,
            Banned = ChatMemberStateChange.Banned,
        }

        internal void OnLobbyStateUpdatedAPI( LobbyChatUpdate_t callback )
        {
            if ( callback.SteamIDLobby != CurrentLobby )
                return;

            MemberStateChange change = (MemberStateChange)callback.GfChatMemberStateChange;
            ulong initiator = callback.SteamIDMakingChange;
            ulong affected = callback.SteamIDUserChanged;

            OnLobbyStateChanged?.Invoke( change, initiator, affected );
        }

        /// <summary>
        /// Called when the state of the Lobby is somehow shifted. Usually when someone joins or leaves the lobby.
        /// The first ulong is the SteamID of the user that initiated the change.
        /// The second ulong is the person that was affected
        /// </summary>
        public Action<MemberStateChange, ulong, ulong> OnLobbyStateChanged;

        /// <summary>
        /// The name of the lobby as a property for easy getting/setting. Note that this is setting LobbyData, which you cannot do unless you are the Owner of the lobby
        /// </summary>
        public string Name
        {
            get
            {
                if ( !IsValid ) { return ""; }
                return CurrentLobbyData.GetData( "name" );
            }
            set
            {
                if ( !IsValid ) { return; }
                CurrentLobbyData.SetData( "name", value );
            }
        }

        /// <summary>
        /// returns true if we're the current owner
        /// </summary>
        public bool IsOwner
        {
            get
            {
                return Owner == client.SteamId;
            }
        }

        /// <summary>
        /// The Owner of the current lobby. Returns 0 if you are not in a valid lobby.
        /// </summary>
        public ulong Owner
        {
            get
            {
                if ( IsValid )
                {
                    return client.native.matchmaking.GetLobbyOwner( CurrentLobby );
                }
                return 0;
            }
            set
            {
                if ( Owner == value ) return;
                client.native.matchmaking.SetLobbyOwner( CurrentLobby, value );
            }
        }

        /// <summary>
        /// Is the Lobby joinable by other people? Defaults to true;
        /// </summary>
        public bool Joinable
        {
            get
            {
                if ( !IsValid ) { return false; }
                string joinable = CurrentLobbyData.GetData( "joinable" );
                switch ( joinable )
                {
                    case "true":
                        return true;
                    case "false":
                        return false;
                    default:
                        return false;
                }
            }
            set
            {
                if ( !IsValid ) { return; }
                if ( client.native.matchmaking.SetLobbyJoinable( CurrentLobby, value ) )
                {
                    CurrentLobbyData.SetData( "joinable", value.ToString() );
                }
            }
        }

        /// <summary>
        /// How many people can be in the Lobby
        /// </summary>
        public int MaxMembers
        {
            get
            {
                if ( !IsValid ) { return 0; } //0 is default, but value is inited when lobby is created. 
                return client.native.matchmaking.GetLobbyMemberLimit( CurrentLobby );
            }
            set
            {
                if ( !IsValid ) { return; }
                client.native.matchmaking.SetLobbyMemberLimit( CurrentLobby, value );
            }
        }

        /// <summary>
        /// How many people are currently in the Lobby
        /// </summary>
        public int NumMembers
        {
            get { return client.native.matchmaking.GetNumLobbyMembers( CurrentLobby ); }
        }

        /// <summary>
        /// Leave the CurrentLobby.
        /// </summary>
        public void Leave()
        {
            if ( CurrentLobby != 0 )
            {
                client.native.matchmaking.LeaveLobby( CurrentLobby );
            }

            CurrentLobby = 0;
            CurrentLobbyData = null;
        }

        public void Dispose()
        {
            client = null;
        }

        /// <summary>
        /// Get an array of all the CSteamIDs in the CurrentLobby.
        /// Note that you must be in the Lobby you are trying to request the MemberIDs from.
        /// Returns an empty array if you aren't in a lobby.
        /// </summary>
        /// <returns>Array of member SteamIDs</returns>
        public ulong[] GetMemberIDs()
        {
            ulong[] memIDs = new ulong[NumMembers];
            for ( int i = 0; i < NumMembers; i++ )
            {
                memIDs[i] = client.native.matchmaking.GetLobbyMemberByIndex( CurrentLobby, i );
            }
            return memIDs;
        }

        /// <summary>
        /// Check to see if a user is in your CurrentLobby
        /// </summary>
        /// <param name="steamID">SteamID of the user to check for</param>
        /// <returns></returns>
        public bool UserIsInCurrentLobby( ulong steamID )
        {
            if ( CurrentLobby == 0 )
                return false;

            ulong[] mems = GetMemberIDs();

            for ( int i = 0; i < mems.Length; i++ )
            {
                if ( mems[i] == steamID )
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Invites the specified user to the CurrentLobby the user is in.
        /// </summary>
        /// <param name="friendID">ulong ID of person to invite</param>
        public bool InviteUserToLobby( ulong friendID )
        {
            return client.native.matchmaking.InviteUserToLobby( CurrentLobby, friendID );
        }

        internal void OnUserInvitedToLobbyAPI( LobbyInvite_t callback )
        {
            if ( callback.GameID != client.AppId ) return;
            if ( OnUserInvitedToLobby != null ) { OnUserInvitedToLobby( callback.SteamIDLobby, callback.SteamIDUser ); }

        }

        /// <summary>
        /// Activates the steam overlay to invite friends to the CurrentLobby the user is in.
        /// </summary>
        public void OpenFriendInviteOverlay()
        {
            client.native.friends.ActivateGameOverlayInviteDialog(CurrentLobby);
        }

        /// <summary>
        /// Called when a user invites the current user to a lobby. The first parameter is the lobby the user was invited to, the second is the CSteamID of the person who invited this user
        /// </summary>
        public Action<ulong, ulong> OnUserInvitedToLobby;

        /// <summary>
        /// Called when a user accepts an invitation to a lobby while the game is running. The parameter is a lobby id.
        /// </summary>
        public Action<ulong> OnLobbyJoinRequested;

        /// <summary>
        /// Joins a lobby if a request was made to join the lobby through the friends list or an invite
        /// </summary>
        internal void OnLobbyJoinRequestedAPI( GameLobbyJoinRequested_t callback )
        {
            if (OnLobbyJoinRequested != null) { OnLobbyJoinRequested(callback.SteamIDLobby); }
        }

        /// <summary>
        /// Makes sure we send an update callback if a Lobby user updates their information
        /// </summary>
        internal void OnLobbyMemberPersonaChangeAPI( PersonaStateChange_t callback )
        {
            if ( !UserIsInCurrentLobby( callback.SteamID ) ) return;
            if ( OnLobbyMemberDataUpdated != null ) { OnLobbyMemberDataUpdated( callback.SteamID ); }
        }

        /// <summary>
        /// Sets the game server associated with the lobby.
        /// This can only be set by the owner of the lobby.
        /// Either the IP/Port or the Steam ID of the game server must be valid, depending on how you want the clients to be able to connect.
        /// </summary>
        public bool SetGameServer( System.Net.IPAddress ip, int port, ulong serverSteamId = 0 )
        {
            if ( !IsValid || !IsOwner ) return false;

            var ipint = System.Net.IPAddress.NetworkToHostOrder( ip.Address );
            client.native.matchmaking.SetLobbyGameServer( CurrentLobby, (uint)ipint, (ushort)port, serverSteamId );
            return true;
        }

        /// <summary>
        /// Gets the details of a game server set in a lobby.
        /// </summary>
        public System.Net.IPAddress GameServerIp
        {
            get
            {
                uint ip;
                ushort port;
                CSteamID steamid;

                if ( !client.native.matchmaking.GetLobbyGameServer( CurrentLobby, out ip, out port, out steamid ) || ip == 0 )
                    return null;

                return new System.Net.IPAddress( System.Net.IPAddress.HostToNetworkOrder( ip ) );
            }
        }

        /// <summary>
        /// Gets the details of a game server set in a lobby.
        /// </summary>
        public int GameServerPort
        {
            get
            {
                uint ip;
                ushort port;
                CSteamID steamid;

                if ( !client.native.matchmaking.GetLobbyGameServer( CurrentLobby, out ip, out port, out steamid ) )
                    return 0;

                return (int)port;
            }
        }

        /// <summary>
        /// Gets the details of a game server set in a lobby.
        /// </summary>
        public ulong GameServerSteamId
        {
            get
            {
                uint ip;
                ushort port;
                CSteamID steamid;

                if ( !client.native.matchmaking.GetLobbyGameServer( CurrentLobby, out ip, out port, out steamid ) )
                    return 0;

                return steamid;
            }
        }

        /*not implemented

        //set the game server of the lobby
        client.native.matchmaking.GetLobbyGameServer;
        client.native.matchmaking.SetLobbyGameServer;

        //used with game server stuff
        SteamNative.LobbyGameCreated_t
        */
    }
}
