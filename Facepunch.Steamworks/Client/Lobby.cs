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

        // Create a lobby, auto joins the created lobby
        public Lobby CreateLobby(Lobby.Type lobbyType, int maxMembers)
        {
            var lobby = new Lobby(this, lobbyType);
            native.matchmaking.CreateLobby((SteamNative.LobbyType)lobbyType, maxMembers, lobby.OnLobbyCreatedAPI);
            return lobby;
        }
    }
    public class Lobby : IDisposable
    {
        //The type of lobby you are creating
        public enum Type : int
        {
            Private = SteamNative.LobbyType.Private,
            FriendsOnly = SteamNative.LobbyType.FriendsOnly,
            Public = SteamNative.LobbyType.Public,
            Invisible = SteamNative.LobbyType.Invisible
        }

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
            get { return _name; }
            set { if (_name == value) return; SetLobbyData("name", value); }
        }
        string _name = "";

        /// <summary>
        /// Callback for when lobby is created
        /// </summary>
        public Action OnLobbyCreated;

        public Lobby(Client c, Type type)
        {
            client = c;
            LobbyType = type;
        }

        internal void OnLobbyCreatedAPI(LobbyCreated_t callback, bool error)
        {
            //from SpaceWarClient.cpp 793
            if (error || (callback.Result != Result.OK))
            {
                IsError = true;
                return;
            }

            Owner = client.SteamId; //this is implicitly set on creation but need to cache it here
            LobbyID = callback.SteamIDLobby;
            MaxMembers = client.native.matchmaking.GetLobbyMemberLimit(LobbyID);
            SetLobbyData("appid", client.AppId.ToString());

            if (OnLobbyCreated != null) { OnLobbyCreated(); }
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

        public Type LobbyType
        {
            get { return _lobbyType; }
            set
            {
                if (_lobbyType == value) return;
                //only call the proper method if the lobby is valid, otherwise cache the value
                if(IsValid)
                {
                    client.native.matchmaking.SetLobbyType(LobbyID, (SteamNative.LobbyType)value); //returns bool?
                }
                _lobbyType = value;
            }
        }
        Type _lobbyType;


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

        //How many people are currently in the lobby
        public int NumMembers
        {
            get { return client.native.matchmaking.GetNumLobbyMembers(LobbyID);}
        }

        //leave the current lobby
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
