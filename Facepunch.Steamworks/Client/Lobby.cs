using System;
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
            Error //happens if you try to get this when you aren't in a valid lobby
        }

        internal Client client;

        public Lobby(Client c)
        {
            client = c;
            SteamNative.LobbyDataUpdate_t.RegisterCallback(client, OnLobbyDataUpdatedAPI);
            SteamNative.LobbyChatMsg_t.RegisterCallback(client, OnLobbyChatMessageRecievedAPI);
            SteamNative.LobbyChatUpdate_t.RegisterCallback(client, OnLobbyStateUpdatedAPI);
            SteamNative.GameLobbyJoinRequested_t.RegisterCallback(client, OnLobbyJoinRequestedAPI);
            SteamNative.LobbyInvite_t.RegisterCallback(client, OnUserInvitedToLobbyAPI);
            SteamNative.PersonaStateChange_t.RegisterCallback(client, OnLobbyMemberPersonaChangeAPI);
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
        /// Join a Lobby through its LobbyID. OnLobbyJoined is called with the result of the Join attempt.
        /// </summary>
        /// <param name="lobbyID">CSteamID of lobby to join</param>
        public void Join(ulong lobbyID)
        {
            Leave();
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

        /// <summary>
        /// Called when a lobby has been attempted joined. Returns true if lobby was successfuly joined, false if not.
        /// </summary>
        public Action<bool> OnLobbyJoined;

        /// <summary>
        /// Creates a lobby and returns the created lobby. You auto join the created lobby. The lobby is stored in Client.CurrentLobby if successful.
        /// </summary>
        /// <param name="lobbyType">The Lobby.Type of Lobby to be created</param>
        /// <param name="maxMembers">The maximum amount of people you want to be able to be in this lobby, including yourself</param>
        public void Create(Lobby.Type lobbyType, int maxMembers)
        {
            client.native.matchmaking.CreateLobby((SteamNative.LobbyType)lobbyType, maxMembers, OnLobbyCreatedAPI);
            createdLobbyType = lobbyType;
        }

        internal Type createdLobbyType;

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
            LobbyType = createdLobbyType;
            CurrentLobbyData.SetData("lobbytype", LobbyType.ToString());
            Joinable = true;
            if (OnLobbyCreated != null) { OnLobbyCreated(true); }
        }

        /// <summary>
        /// Callback for when lobby is created
        /// </summary>
        public Action<bool> OnLobbyCreated;

        /// <summary>
        /// Class to hold global lobby data. This is stuff like maps/modes/etc. Data set here can be filtered by LobbyList.
        /// </summary>
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

            public List<KeyValuePair<string,string>> GetAllData()
            {
                List<KeyValuePair<string, string>> returnData = new List<KeyValuePair<string, string>>();
                foreach(KeyValuePair<string, string> item in data)
                {
                    returnData.Add(new KeyValuePair<string, string>(item.Key, item.Value));
                }
                return returnData;
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

        /// <summary>
        /// Sets user data for the Lobby. Things like Character, Skin, Ready, etc. Can only set your own member data
        /// </summary>
        public void SetMemberData(string key, string value)
        {
            if(CurrentLobby == 0) { return; }
            client.native.matchmaking.SetLobbyMemberData(CurrentLobby, key, value);
        }

        /// <summary>
        /// Get the per-user metadata from this lobby. Can get data from any user
        /// </summary>
        /// <param name="steamID">ulong SteamID of the user you want to get data from</param>
        /// <param name="key">String key of the type of data you want to get</param>
        /// <returns></returns>
        public string GetMemberData(ulong steamID, string key)
        {
            if (CurrentLobby == 0) { return "ERROR: NOT IN ANY LOBBY"; }
            return client.native.matchmaking.GetLobbyMemberData(CurrentLobby, steamID, key);
        }

        internal void OnLobbyDataUpdatedAPI(LobbyDataUpdate_t callback, bool error)
        {
            if(error || (callback.SteamIDLobby != CurrentLobby)) { return; }
            if(callback.SteamIDLobby == CurrentLobby) //actual lobby data was updated by owner
            {
                UpdateLobbyData();
            }

            if(UserIsInCurrentLobby(callback.SteamIDMember)) //some member of this lobby updated their information
            {
                if (OnLobbyMemberDataUpdated != null) { OnLobbyMemberDataUpdated(callback.SteamIDMember); }
            }
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

            if(OnLobbyDataUpdated != null) { OnLobbyDataUpdated(); }
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
                if (!IsValid) { return Type.Error; } //if we're currently in a valid server
                
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
                        return Type.Error;
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

        private unsafe void OnLobbyChatMessageRecievedAPI(LobbyChatMsg_t callback, bool error)
        {
            //from Client.Networking
            if(error || callback.SteamIDLobby != CurrentLobby) { return; }

            byte[] ReceiveBuffer = new byte[1024];
            SteamNative.CSteamID steamid = 1;
            ChatEntryType chatEntryType; //not used
            int readData = 0;
            fixed (byte* p = ReceiveBuffer)
            {
                readData = client.native.matchmaking.GetLobbyChatEntry(CurrentLobby, (int)callback.ChatID, out steamid, (IntPtr)p, ReceiveBuffer.Length, out chatEntryType);
                while (ReceiveBuffer.Length < readData)
                {
                    ReceiveBuffer = new byte[readData + 1024];
                    readData = client.native.matchmaking.GetLobbyChatEntry(CurrentLobby, (int)callback.ChatID, out steamid, (IntPtr)p, ReceiveBuffer.Length, out chatEntryType);
                }
                
            }

            if (OnChatMessageRecieved != null) { OnChatMessageRecieved(steamid, ReceiveBuffer, readData); }

        }

        /// <summary>
        /// Callback to get chat messages.
        /// </summary>
        public Action<ulong, byte[], int> OnChatMessageRecieved;

        /// <summary>
        /// Broadcasts a chat message to the all the users in the lobby users in the lobby (including the local user) will receive a LobbyChatMsg_t callback.
        /// </summary>
        /// <returns>True if message successfully sent</returns>
        public unsafe bool SendChatMessage(string message)
        {
            var data = Encoding.UTF8.GetBytes(message);
            fixed (byte* p = data)
            {
                // pvMsgBody can be binary or text data, up to 4k
                // if pvMsgBody is text, cubMsgBody should be strlen( text ) + 1, to include the null terminator
                return client.native.matchmaking.SendLobbyChatMsg(CurrentLobby, (IntPtr)p, data.Length);
            }
        }

        public enum MemberStateChange
        {
            Entered = ChatMemberStateChange.Entered,
            Left = ChatMemberStateChange.Left,
            Disconnected = ChatMemberStateChange.Disconnected,
            Kicked = ChatMemberStateChange.Kicked,
            Banned = ChatMemberStateChange.Banned,
        }

        internal void OnLobbyStateUpdatedAPI(LobbyChatUpdate_t callback, bool error)
        {
            if (error || callback.SteamIDLobby != CurrentLobby) { return; }
            MemberStateChange change = (MemberStateChange)callback.GfChatMemberStateChange;
            ulong initiator = callback.SteamIDMakingChange;
            ulong affected = callback.SteamIDUserChanged;

            if (OnLobbyStateChanged != null) { OnLobbyStateChanged(change, initiator, affected); }
        }

        /// <summary>
        /// Called when the state of the Lobby is somehow shifted. Usually when someone joins or leaves the lobby.
        /// </summary>
        public Action<MemberStateChange, ulong, ulong> OnLobbyStateChanged;

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

        /// <summary>
        /// Is the Lobby joinable by other people? Defaults to true;
        /// </summary>
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

        /// <summary>
        /// How many people can be in the Lobby
        /// </summary>
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

        /// <summary>
        /// How many people are currently in the Lobby
        /// </summary>
        public int NumMembers
        {
            get { return client.native.matchmaking.GetNumLobbyMembers(CurrentLobby);}
        }

        /// <summary>
        /// Leave the CurrentLobby.
        /// </summary>
        public void Leave()
        {
            if (CurrentLobby != 0) { client.native.matchmaking.LeaveLobby(CurrentLobby); }
            CurrentLobby = 0;
            _owner = 0;
            CurrentLobbyData = null;
        }

        public void Dispose()
        {
            client = null;
        }

        /// <summary>
        /// Get an array of all the CSteamIDs in the CurrentLobby.
        /// Note that you must be in the Lobby you are trying to request the MemberIDs from
        /// </summary>
        /// <returns></returns>
        public ulong[] GetMemberIDs()
        {

            ulong[] memIDs = new ulong[NumMembers];
            for (int i = 0; i < NumMembers; i++)
            {
                memIDs[i] = client.native.matchmaking.GetLobbyMemberByIndex(CurrentLobby, i);
            }
            return memIDs;
        }

        public bool UserIsInCurrentLobby(ulong steamID)
        {
            if(CurrentLobby == 0) { return false; }
            ulong[] mems = GetMemberIDs();
            for (int i = 0; i < mems.Length; i++)
            {
                if(mems[i] == steamID)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Invites the specified user to the CurrentLobby the user is in.
        /// </summary>
        /// <param name="friendID">ulong ID of person to invite</param>
        public bool InviteUserToLobby(ulong friendID)
        {
            return client.native.matchmaking.InviteUserToLobby(CurrentLobby, friendID);
        }

        internal void OnUserInvitedToLobbyAPI(LobbyInvite_t callback, bool error)
        {
            if (error || (callback.GameID != client.AppId)) { return; }
            if (OnUserInvitedToLobby != null) { OnUserInvitedToLobby(callback.SteamIDLobby, callback.SteamIDUser); }

        }

        /// <summary>
        /// Called when a user invites the current user to a lobby. The first parameter is the lobby the user was invited to, the second is the CSteamID of the person who invited this user
        /// </summary>
        public Action<ulong, ulong> OnUserInvitedToLobby;

        /// <summary>
        /// Joins a lobby is a request was made to join the lobby through the friends list or an invite
        /// </summary>
        internal void OnLobbyJoinRequestedAPI(GameLobbyJoinRequested_t callback, bool error)
        {
            if (error) { return; }
            Join(callback.SteamIDLobby);
        }

        /// <summary>
        /// Makes sure we send an update callback if a Lobby user updates their information
        /// </summary>
        internal void OnLobbyMemberPersonaChangeAPI(PersonaStateChange_t callback, bool error)
        {
            if (error || !UserIsInCurrentLobby(callback.SteamID)) { return; }
            if (OnLobbyMemberDataUpdated != null) { OnLobbyMemberDataUpdated(callback.SteamID); }
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
