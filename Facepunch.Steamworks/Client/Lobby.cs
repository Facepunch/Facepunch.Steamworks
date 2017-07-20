using System;
using System.Collections.Generic;
using System.Text;
using SteamNative;
using Result = SteamNative.Result;

namespace Facepunch.Steamworks
{
    public partial class Client : IDisposable
    {
        public void JoinLobby(ulong LobbyID)
        {
            native.matchmaking.JoinLobby(LobbyID, OnLobbyJoined);
        }

        void OnLobbyJoined(LobbyEnter_t callback, bool error)
        {
            //TODO:
        }


        //The type of lobby you are creating
        public enum LobbyType : int
        {
            Private = SteamNative.LobbyType.Private,
            FriendsOnly = SteamNative.LobbyType.FriendsOnly,
            Public = SteamNative.LobbyType.Public,
            Invisible = SteamNative.LobbyType.Invisible
        }

        // Create a lobby, auto joins the created lobby
        public Lobby CreateLobby(LobbyType lobbyType, int maxMembers, string name)
        {
            var lobby = new Lobby(this);
            native.matchmaking.CreateLobby((SteamNative.LobbyType)lobbyType, maxMembers, lobby.OnLobbyCreated);
            if (lobby.IsValid)
            {
                lobby.Name = name;
                lobby.MaxMembers = maxMembers;
                lobby.LobbyType = lobbyType;
            }
            return lobby;
        }
    }
    public class Lobby : IDisposable
    {
        internal Client client;

        /// <summary>
        ///     Returns true if we tried to create this lobby but it returned
        ///     an error.
        /// </summary>
        public bool IsError { get; private set; }

        /// <summary>
        ///     Returns true if this lobby is valid, ie, we've received
        ///     a positive response from Steam about it.
        /// </summary>
        public bool IsValid => LobbyID != 0;

        /// <summary>
        /// The CSteamID of the lobby that was created
        /// </summary>
        internal ulong LobbyID { get; private set; }
        
        /// <summary>
        /// The name of the lobby as a property for easy getting/setting
        /// </summary>
        public string Name
        {
            get { return LobbyData["name"]; }
            set { SetLobbyData("name", value); }
        }

        public Lobby(Client c)
        {
            client = c;
        }

        internal void OnLobbyCreated(LobbyCreated_t callback, bool error)
        {
            //from SpaceWarClient.cpp 793
            if (error || (callback.Result != Result.OK))
            {
                IsError = true;
                return;
            }

            Owner = client.SteamId; //this is implicitly set on creation but need to cache it here
            LobbyID = callback.SteamIDLobby;
        }

        Dictionary<string, string> LobbyData = new Dictionary<string, string>();
        public void SetLobbyData(string key, string value)
        {
            if (LobbyData.ContainsKey(key))
            {
                if (LobbyData[key] == value)
                    return;

                LobbyData[key] = value;
            }
            else
            {
                LobbyData.Add(key, value);
            }

            client.native.matchmaking.SetLobbyData(LobbyID, key, value);
        }

        public void RemoveLobbyData(string key)
        {
            if (LobbyData.ContainsKey(key))
            {
                LobbyData.Remove(key);
            }

            client.native.matchmaking.DeleteLobbyData(LobbyID, key);
        }

        public Client.LobbyType LobbyType
        {
            get { return _lobbyType; }
            set { if (_lobbyType == value) return; client.native.matchmaking.SetLobbyType(LobbyID, (SteamNative.LobbyType)value); _lobbyType = value; } //returns bool
        }
        Client.LobbyType _lobbyType;


        //Must be the owner to change the owner
        public ulong Owner
        {
            get
            {
                if (_owner == 0)
                {
                    _owner = client.native.matchmaking.GetLobbyOwner(LobbyID);
                    return _owner;
                }
                return _owner;
            }
            private set { if (_owner == value) return; client.native.matchmaking.SetLobbyOwner(LobbyID, value); _owner = value; }
        }
        ulong _owner = 0;

        // Can the lobby be joined by other people
        public bool Joinable
        {
            get { return _joinable; }
            set { if (_joinable == value) return; client.native.matchmaking.SetLobbyJoinable(LobbyID, value); _joinable = value; }
        }
        bool _joinable = true; //steam default

        // How many people can be in the Lobby
        public int MaxMembers
        {
            get { return _maxMembers; }
            set { if (_maxMembers == value) return; client.native.matchmaking.SetLobbyMemberLimit(LobbyID, value); _maxMembers = value; }
        }
        int _maxMembers = 0;

        public void Leave()
        {
            client.native.matchmaking.LeaveLobby(LobbyID);
        }

        public void Dispose()
        {
            client = null;
        }

        /*not implemented
        // returns a lobby metadata key/values pair by index
        client.native.matchmaking.GetLobbyDataByIndex;

        //set the game server of the lobby
        client.native.matchmaking.GetLobbyGameServer;
        client.native.matchmaking.SetLobbyGameServer;

        //data for people in the actual lobby - scores/elo/characters/etc.
        client.native.matchmaking.SetLobbyMemberData; //local user
        client.native.matchmaking.GetLobbyMemberData; //any user in this lobby
        

        // returns steamid of member
        // note that the current user must be in a lobby to retrieve CSteamIDs of other users in that lobby
        client.native.matchmaking.GetLobbyMemberByIndex;

        //for linking lobbies idk havent looked hard yet
        client.native.matchmaking.SetLinkedLobby;

        //chat functions
        client.native.matchmaking.SendLobbyChatMsg;
        client.native.matchmaking.GetLobbyChatEntry;

        //get total data count (why?)
        client.native.matchmaking.GetLobbyDataCount

        //invite your frans
        client.native.matchmaking.InviteUserToLobby //this invites the user the current lobby the invitee is in
        */
    }
}
