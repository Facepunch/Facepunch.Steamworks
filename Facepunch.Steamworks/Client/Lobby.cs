using System;
using System.Collections.Generic;
using System.Text;
using SteamNative;
using Result = SteamNative.Result;

namespace Facepunch.Steamworks
{
    public partial class Client : IDisposable
    {
        Lobby _lobby;

        public Lobby Lobby
        {
            get
            {
                if (_lobby == null)
                    _lobby = new Steamworks.Lobby(this);
                return _lobby;
            }
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
            Invisible = SteamNative.LobbyType.Invisible,
            None
        }

        internal Client client;

        public Lobby(Client c)
        {
            client = c;
            SteamNative.LobbyDataUpdate_t.RegisterCallback(client, OnLobbyDataUpdated);
        }

        /// <summary>
        /// The CSteamID of the lobby we're currently in.
        /// </summary>
        public ulong CurrentLobby { get; private set; }

        public LobbyData CurrentLobbyData { get; private set; }

        /// <summary>
        /// Returns true if this lobby is valid, ie, we've succesffuly created and/or joined a lobby.
        /// </summary>
        public bool IsValid => CurrentLobby != 0;

        /// <summary>
        /// Join a Lobby through its LobbyID. LobbyJoined is called when the lobby has successfully been joined.
        /// </summary>
        /// <param name="lobbyID">CSteamID of lobby to join</param>
        public void Join(ulong lobbyID)
        {
            client.native.matchmaking.JoinLobby(lobbyID, OnLobbyJoinedAPI);
        }

        void OnLobbyJoinedAPI(LobbyEnter_t callback, bool error)
        {
            if (error || (callback.EChatRoomEnterResponse != (uint)(SteamNative.ChatRoomEnterResponse.Success))) 
            {
                if (OnLobbyJoined != null) { OnLobbyJoined(false); }
                return;
            }

            CurrentLobby = callback.SteamIDLobby;
            UpdateLobbyData();
            if (OnLobbyJoined != null) { OnLobbyJoined(true); }
        }

        public Action<bool> OnLobbyJoined;

        /// <summary>
        /// Creates a lobby and returns the created lobby. You auto join the created lobby. The lobby is stored in Client.CurrentLobby if successful.
        /// </summary>
        /// <param name="lobbyType">The Lobby.Type of Lobby to be created</param>
        /// <param name="maxMembers">The maximum amount of people you want to be able to be in this lobby, including yourself</param>
        public void Create(Lobby.Type lobbyType, int maxMembers)
        {
            client.native.matchmaking.CreateLobby((SteamNative.LobbyType)lobbyType, maxMembers, OnLobbyCreatedAPI);
            LobbyType = lobbyType;
        }

        internal void OnLobbyCreatedAPI(LobbyCreated_t callback, bool error)
        {
            //from SpaceWarClient.cpp 793
            if (error || (callback.Result != Result.OK))
            {
                if ( OnLobbyCreated != null) { OnLobbyCreated(false); }
                return;
            }

            //set owner specific properties
            Owner = client.SteamId;
            CurrentLobby = callback.SteamIDLobby;
            CurrentLobbyData = new LobbyData(client, CurrentLobby);
            Name = client.Username + "'s Lobby";
            CurrentLobbyData.SetData("appid", client.AppId.ToString());
            CurrentLobbyData.SetData("lobbytype", LobbyType.ToString());
            if (OnLobbyCreated != null) { OnLobbyCreated(true); }
        }

        /// <summary>
        /// Callback for when lobby is created
        /// </summary>
        public Action<bool> OnLobbyCreated;

        public class LobbyData
        {
            internal Client client;
            internal ulong lobby;
            internal Dictionary<string, string> data; 

            public LobbyData(Client c, ulong l)
            {
                client = c;
                lobby = l;
                data = new Dictionary<string, string>();
            }

            public string GetData(string k)
            {
                if (data.ContainsKey(k))
                {
                    return data[k];
                }

                return "ERROR: key not found";
            }

            public bool SetData(string k, string v)
            {
                if (data.ContainsKey(k))
                {
                    if (data[k] == v) { return true; }
                    if (client.native.matchmaking.SetLobbyData(lobby, k, v))
                    {
                        data[k] = v;
                        return true;
                    }
                }
                else
                {
                    if (client.native.matchmaking.SetLobbyData(lobby, k, v))
                    {
                        data.Add(k, v);
                        return true;
                    }
                }

                return false;
            }

            public bool RemoveData(string k)
            {
                if (data.ContainsKey(k))
                {
                    if (client.native.matchmaking.DeleteLobbyData(lobby, k))
                    {
                        data.Remove(k);
                        return true;
                    }
                }

                return false;
            }

        }

        internal void OnLobbyDataUpdated(LobbyDataUpdate_t callback, bool error)
        {
            if(error) { return; }
            if(callback.SteamIDLobby == CurrentLobby) //actual lobby data was updated by owner
            {
                UpdateLobbyData();
            }

            //TODO: need to check and see if the updated member is in this lobby
        }

        /// <summary>
        /// Updates the LobbyData property to have the data for the current lobby, if any
        /// </summary>
        internal void UpdateLobbyData()
        {
            int dataCount = client.native.matchmaking.GetLobbyDataCount(CurrentLobby);
            CurrentLobbyData = new LobbyData(client, CurrentLobby);
            for (int i = 0; i < dataCount; i++)
            {
                if (client.native.matchmaking.GetLobbyDataByIndex(CurrentLobby, i, out string key, out string value))
                {
                    CurrentLobbyData.SetData(key, value);
                }
            }
        }

        public Type LobbyType
        {
            get
            {
                if (!IsValid) { return Type.None; } //if we're currently in a valid server
                
                //we know that we've set the lobby type via the lobbydata in the creation function
                //ps this is important because steam doesn't have an easy way to get lobby type (why idk)
                string lobbyType = CurrentLobbyData.GetData("lobbytype");
                switch (lobbyType)
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
                        return Type.None;
                }
            }
            set
            {
                if(!IsValid) { return; }
                if(client.native.matchmaking.SetLobbyType(CurrentLobby, (SteamNative.LobbyType)value))
                {
                    CurrentLobbyData.SetData("lobbytype", value.ToString());
                }
            }
        }

        /// <summary>
        /// The name of the lobby as a property for easy getting/setting. Note that this is setting LobbyData, which you cannot do unless you are the Owner of the lobby
        /// </summary>
        public string Name
        {
            get
            {
                if (!IsValid) { return ""; }
                return CurrentLobbyData.GetData("name");
            }
            set
            {
                if (!IsValid) { return; }
                CurrentLobbyData.SetData("name", value);
            }
        }

        /// <summary>
        /// The Owner of the current lobby. Returns 0 if you are not in a valid lobby.
        /// </summary>
        public ulong Owner
        {
            get
            {
                if (_owner == 0 && IsValid)
                {
                    _owner = client.native.matchmaking.GetLobbyOwner(CurrentLobby);
                }
                return _owner;
            }
            private set
            {
                if (_owner == value) return;
                if (client.native.matchmaking.SetLobbyOwner(CurrentLobby, value)) { _owner = value; }
            }
        }
        ulong _owner = 0;

        // Can the lobby be joined by other people
        public bool Joinable
        {
            get
            {
                if (!IsValid) { return false; }
                string joinable = CurrentLobbyData.GetData("joinable");
                switch (joinable)
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
                if (!IsValid) { return; }
                if (client.native.matchmaking.SetLobbyJoinable(CurrentLobby, value))
                {
                    CurrentLobbyData.SetData("joinable", value.ToString());
                }
            }
        }

        // How many people can be in the Lobby
        public int MaxMembers
        {
            get
            {
                if (!IsValid) { return 0; } //0 is default, but value is inited when lobby is created. 
                return client.native.matchmaking.GetLobbyMemberLimit(CurrentLobby);
            }
            set
            {
                if (!IsValid) { return; }
                client.native.matchmaking.SetLobbyMemberLimit(CurrentLobby, value);
            }
        }

        //How many people are currently in the lobby
        public int NumMembers
        {
            get { return client.native.matchmaking.GetNumLobbyMembers(CurrentLobby);}
        }

        //leave the current lobby
        public void Leave()
        {
            client.native.matchmaking.LeaveLobby(CurrentLobby);
            _owner = 0;
            CurrentLobbyData = null;
        }

        public void Dispose()
        {
            client = null;
        }

        /*not implemented

        //set the game server of the lobby
        client.native.matchmaking.GetLobbyGameServer;
        client.native.matchmaking.SetLobbyGameServer;

        //data for people in the actual lobby - scores/elo/characters/etc.
        client.native.matchmaking.SetLobbyMemberData; //local user
        client.native.matchmaking.GetLobbyMemberData; //any user in this lobby

        // returns steamid of member
        // note that the current user must be in a lobby to retrieve CSteamIDs of other users in that lobby
        client.native.matchmaking.GetLobbyMemberByIndex;

        //chat functions
        client.native.matchmaking.SendLobbyChatMsg;
        client.native.matchmaking.GetLobbyChatEntry;

        //invite your frans
        client.native.matchmaking.InviteUserToLobby //this invites the user the current lobby the invitee is in
        */
    }
}
