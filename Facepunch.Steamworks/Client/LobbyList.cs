using System;
using System.Collections.Generic;
using System.Text;
using SteamNative;

namespace Facepunch.Steamworks
{
    public partial class LobbyList : IDisposable
    {
        internal Client client;

        //The list of retrieved lobbies
        public List<Lobby> Lobbies { get; private set; }

        //True when all the possible lobbies have had their data updated
        //if the number of lobbies is now equal to the initial request number, we've found all lobbies
        public bool Finished { get; private set; }

        //The number of possible lobbies we can get data from
        internal List<ulong> requests;

        internal LobbyList(Client client)
        {
            this.client = client;
            Lobbies = new List<Lobby>();
            requests = new List<ulong>();
        }

        /// <summary>
        /// Refresh the List of Lobbies. If no filter is passed in, a default one is created that filters based on AppId ("appid").
        /// </summary>
        /// <param name="filter"></param>
        public void Refresh ( Filter filter = null)
        {
            //init out values
            Lobbies.Clear();
            requests.Clear();
            Finished = false;

            if (filter == null)
            {
                filter = new Filter();
                filter.StringFilters.Add("appid", client.AppId.ToString());
            }

            client.native.matchmaking.AddRequestLobbyListDistanceFilter((SteamNative.LobbyDistanceFilter)filter.DistanceFilter);

            if (filter.SlotsAvailable != null)
            {
                client.native.matchmaking.AddRequestLobbyListFilterSlotsAvailable((int)filter.SlotsAvailable);
            }

            if (filter.MaxResults != null)
            {
                client.native.matchmaking.AddRequestLobbyListResultCountFilter((int)filter.MaxResults);
            }

            foreach (KeyValuePair<string, string> fil in filter.StringFilters)
            {
                client.native.matchmaking.AddRequestLobbyListStringFilter(fil.Key, fil.Value, SteamNative.LobbyComparison.Equal);
            }
            foreach (KeyValuePair<string, int> fil in filter.NearFilters)
            {
                client.native.matchmaking.AddRequestLobbyListNearValueFilter(fil.Key, fil.Value);
            }
            //foreach (KeyValuePair<string, KeyValuePair<Filter.Comparison, int>> fil in filter.NumericalFilters)
            //{
            //    client.native.matchmaking.AddRequestLobbyListNumericalFilter(fil.Key, fil.Value.Value, (SteamNative.LobbyComparison)fil.Value.Key);
            //}


            // this will never return lobbies that are full (via the actual api)
            client.native.matchmaking.RequestLobbyList(OnLobbyList);

        }


        void OnLobbyList(LobbyMatchList_t callback, bool error)
        {
            if (error) return;

            //how many lobbies matched
            uint lobbiesMatching = callback.LobbiesMatching;

            // lobbies are returned in order of closeness to the user, so add them to the list in that order
            for (int i = 0; i < lobbiesMatching; i++)
            {
                //add the lobby to the list of requests
                ulong lobby = client.native.matchmaking.GetLobbyByIndex(i);
                requests.Add(lobby);

                //cast to a LobbyList.Lobby
                Lobby newLobby = Lobby.FromSteam(client, lobby);
                if (newLobby.Name != "")
                {
                    //if the lobby is valid add it to the valid return lobbies
                    Lobbies.Add(newLobby);
                    checkFinished();
                }
                else
                {
                    //else we need to get the info for the missing lobby
                    client.native.matchmaking.RequestLobbyData(lobby);
                    SteamNative.LobbyDataUpdate_t.RegisterCallback(client, OnLobbyDataUpdated);
                }

            }

            checkFinished();

            if (OnLobbiesUpdated != null) { OnLobbiesUpdated(); }
        }

        void checkFinished()
        {
            if (Lobbies.Count == requests.Count)
            {
                Finished = true;
                return;
            }
            Finished = false;
        }

        void OnLobbyDataUpdated(LobbyDataUpdate_t callback, bool error)
        {
            if (callback.Success == 1) //1 if success, 0 if failure
            {
                //find the lobby that has been updated
                Lobby lobby = Lobbies.Find(x => x.LobbyID == callback.SteamIDLobby);

                //if this lobby isn't yet in the list of lobbies, we know that we should add it
                if (lobby == null)
                {
                    Lobbies.Add(lobby);
                    checkFinished();
                }

                //otherwise lobby data in general was updated and you should listen to see what changed
                if (OnLobbiesUpdated != null) { OnLobbiesUpdated(); }
            }

            
        }

        public Action OnLobbiesUpdated;

        public void Dispose()
        {
            client = null;
        }

        public class Filter
        {
            // Filters that match actual metadata keys exactly
            public Dictionary<string, string> StringFilters = new Dictionary<string, string>();
            // Filters that are of string key and int value for that key to be close to
            public Dictionary<string, int> NearFilters = new Dictionary<string, int>();
            //Filters that are of string key and int value, with a comparison filter to say how we should relate to the value
            //public Dictionary<string, KeyValuePair<Comparison, int>> NumericalFilters = new Dictionary<string, KeyValuePair<Comparison, int>>();
            public Distance DistanceFilter = Distance.Worldwide;
            public int? SlotsAvailable { get; set; }
            public int? MaxResults { get; set; }

            public enum Distance : int
            {
                Close = SteamNative.LobbyDistanceFilter.Close,
                Default = SteamNative.LobbyDistanceFilter.Default,
                Far = SteamNative.LobbyDistanceFilter.Far,
                Worldwide = SteamNative.LobbyDistanceFilter.Worldwide
            }

            public enum Comparison : int
            {
                EqualToOrLessThan = SteamNative.LobbyComparison.EqualToOrLessThan,
                LessThan = SteamNative.LobbyComparison.LessThan,
                Equal = SteamNative.LobbyComparison.Equal,
                GreaterThan = SteamNative.LobbyComparison.GreaterThan,
                EqualToOrGreaterThan = SteamNative.LobbyComparison.EqualToOrGreaterThan,
                NotEqual = SteamNative.LobbyComparison.NotEqual
            }
        }
    }
}
