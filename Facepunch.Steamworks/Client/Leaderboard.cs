using System;
using System.Collections.Generic;
using System.Linq;
using SteamNative;

namespace Facepunch.Steamworks
{
    public class Leaderboard : IDisposable
    {
        /// <summary>
        ///     Type of leaderboard request
        /// </summary>
        public enum RequestType
        {
            /// <summary>
            ///     Query everyone and everything
            /// </summary>
            Global = LeaderboardDataRequest.Global,

            /// <summary>
            ///     Query only users that are close to you geographically
            /// </summary>
            GlobalAroundUser = LeaderboardDataRequest.GlobalAroundUser,

            /// <summary>
            ///     Only show friends of this user
            /// </summary>
            Friends = LeaderboardDataRequest.Friends
        }

        private static readonly int[] subEntriesBuffer = new int[512];

        internal ulong BoardId;
        internal Client client;

        /// <summary>
        ///     The results from the last query. Can be null.
        /// </summary>
        public Entry[] Results;

        internal Leaderboard( Client c )
        {
            client = c;
        }

        /// <summary>
        ///     The name of this board, as retrieved from Steam
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        ///     The total number of entries on this board
        /// </summary>
        public int TotalEntries { get; private set; }

        /// <summary>
        ///     Returns true if this board is valid, ie, we've received
        ///     a positive response from Steam about it.
        /// </summary>
        public bool IsValid => BoardId != 0;

        /// <summary>
        ///     Returns true if we asked steam about this board but it returned
        ///     an error.
        /// </summary>
        public bool IsError { get; private set; }

        /// <summary>
        ///     Returns true if we're querying scores
        /// </summary>
        public bool IsQuerying { get; private set; }

        public void Dispose()
        {
            client = null;
        }

        internal void OnBoardCreated( LeaderboardFindResult_t result, bool error )
        {
            if ( error || ( result.LeaderboardFound == 0 ) )
            {
                IsError = true;
                return;
            }

            BoardId = result.SteamLeaderboard;

            if ( IsValid )
            {
                Name = client.native.userstats.GetLeaderboardName( BoardId );
                TotalEntries = client.native.userstats.GetLeaderboardEntryCount( BoardId );
            }
        }

        /// <summary>
        /// Add a score to this leaderboard.
        /// Subscores are totally optional, and can be used for other game defined data such as laps etc.. although
        /// they have no bearing on sorting at all
        /// If onlyIfBeatsOldScore is true, the score will only be updated if it beats the existing score, else it will always
        /// be updated.
        /// </summary>
        public void AddScore( bool onlyIfBeatsOldScore, int score, params int[] subscores )
        {
            if ( !IsValid ) return;

            var flags = LeaderboardUploadScoreMethod.ForceUpdate;
            if ( onlyIfBeatsOldScore ) flags = LeaderboardUploadScoreMethod.KeepBest;

            client.native.userstats.UploadLeaderboardScore( BoardId, flags, score, subscores, subscores.Length );
        }

        /// <summary>
        ///     Fetch a subset of scores. The scores end up in Results.
        /// </summary>
        /// <returns>Returns true if we have started the query</returns>
        public bool FetchScores( RequestType RequestType, int start, int end )
        {
            if ( !IsValid ) return false;
            if ( IsQuerying ) return false;

            client.native.userstats.DownloadLeaderboardEntries( BoardId, (LeaderboardDataRequest) RequestType, start, end, OnScores );

            Results = null;
            IsQuerying = true;
            return true;
        }

        private unsafe void OnScores( LeaderboardScoresDownloaded_t result, bool error )
        {
            IsQuerying = false;

            if ( client == null ) return;

            if ( error )
                return;

            var list = new List<Entry>();

            for ( var i = 0; i < result.CEntryCount; i++ )
                fixed ( int* ptr = subEntriesBuffer )
                {
                    var entry = new LeaderboardEntry_t();
                    if ( client.native.userstats.GetDownloadedLeaderboardEntry( result.SteamLeaderboardEntries, i, ref entry, (IntPtr) ptr, subEntriesBuffer.Length ) )
                        list.Add( new Entry
                        {
                            GlobalRank = entry.GlobalRank,
                            Score = entry.Score,
                            SteamId = entry.SteamIDUser,
                            SubScores = entry.CDetails == 0 ? null : subEntriesBuffer.Take( entry.CDetails ).ToArray(),
                            Name = client.Friends.GetName( entry.SteamIDUser )
                        } );
                }

            Results = list.ToArray();
        }

        /// <summary>
        ///     A single entry in a leaderboard
        /// </summary>
        public struct Entry
        {
            public ulong SteamId;
            public int Score;
            public int[] SubScores;
            public int GlobalRank;


            /// <summary>
            ///     Note that the player's name might not be immediately available.
            ///     If that's the case you'll have to use Friends.GetName to find the name
            /// </summary>
            public string Name;
        }
    }
}