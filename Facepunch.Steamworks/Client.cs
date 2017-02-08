using System;
using System.Runtime.InteropServices;

namespace Facepunch.Steamworks
{
    public partial class Client : BaseSteamworks
    {
        /// <summary>
        /// Current user's Username
        /// </summary>
        public string Username { get; private set; }

        /// <summary>
        /// Current user's SteamId
        /// </summary>
        public ulong SteamId { get; private set; }

        /// <summary>
        /// Current Beta name, if we're using a beta branch.
        /// </summary>
        public string BetaName { get; private set; }

        public Voice Voice { get; private set; }
        public ServerList ServerList { get; private set; }

        public Client( uint appId )
        {
            native = new Interop.NativeInterface();

            //
            // Get other interfaces
            //
            if ( !native.InitClient( this ) )
            {
                native.Dispose();
                native = null;
                return;
            }

            //
            // Setup interfaces that client and server both have
            //
            SetupCommonInterfaces();

            //
            // Client only interfaces
            //
            Voice = new Voice( this );
            ServerList = new ServerList( this );

            Workshop.friends = Friends;


            //
            // Cache common, unchanging info
            //
            AppId = appId;
            Username = native.friends.GetPersonaName();
            SteamId = native.user.GetSteamID();
            BetaName = native.apps.GetCurrentBetaName();

            //
            // Run update, first call does some initialization
            //
            Update();
        }

        /// <summary>
        /// Should be called at least once every frame
        /// </summary>
        public override void Update()
        {
            if ( !IsValid )
                return;

            RunCallbacks();
            Voice.Update();

            base.Update();            
        }

        /// <summary>
        /// This is called in Update() - there's no need to call it manually unless you're running your own Update
        /// </summary>
        public void RunCallbacks()
        {
            native.api.SteamAPI_RunCallbacks();
        }

        /// <summary>
        /// Call when finished to shut down the Steam client.
        /// </summary>
        public override void Dispose()
        {
            if ( Voice != null )
            {
                Voice.Dispose();
                Voice = null;
            }

            if ( ServerList  != null )
            {
                ServerList.Dispose();
                ServerList = null;
            }

            base.Dispose();
        }

        public enum LeaderboardSortMethod
        {
            None = 0,
            Ascending = 1,  // top-score is lowest number
            Descending = 2, // top-score is highest number
        };

        // the display type (used by the Steam Community web site) for a leaderboard
        public enum LeaderboardDisplayType
        {
            None = 0,
            Numeric = 1,           // simple numerical score
            TimeSeconds = 2,       // the score represents a time, in seconds
            TimeMilliSeconds = 3,  // the score represents a time, in milliseconds
        };

        public Leaderboard GetLeaderboard( string name, LeaderboardSortMethod sortMethod = LeaderboardSortMethod.None, LeaderboardDisplayType displayType = LeaderboardDisplayType.None )
        {
            var board = new Leaderboard( this );
            native.userstats.FindOrCreateLeaderboard( name, (SteamNative.LeaderboardSortMethod)sortMethod, (SteamNative.LeaderboardDisplayType)displayType, board.OnBoardCreated );
            return board;
        }
    }
}
