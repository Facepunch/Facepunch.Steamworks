using System;
using System.IO;
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
        /// If we're sharing this game, this is the owner of it.
        /// </summary>
        public ulong OwnerSteamId { get; private set; }

        /// <summary>
        /// Current Beta name, if we're using a beta branch.
        /// </summary>
        public string BetaName { get; private set; }

        /// <summary>
        /// The BuildId of the current build 
        /// </summary>
        public int BuildId { get; private set; }

        /// <summary>
        /// The folder in which this app is installed
        /// </summary>
        public DirectoryInfo InstallFolder { get; private set; }


        /// <summary>
        /// The currently selected language
        /// </summary>
        public string CurrentLanguage { get; }


        /// <summary>
        /// List of languages available to the game
        /// </summary>
        public string[] AvailableLanguages { get; }

        public Voice Voice { get; private set; }
        public ServerList ServerList { get; private set; }
        public App App { get; private set; }
        public Achievements Achievements { get; private set; }
        public Stats Stats { get; private set; }

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
            App = new App( this );
            Stats = new Stats( this );
            Achievements = new Achievements( this );

            Workshop.friends = Friends;

            //
            // Cache common, unchanging info
            //
            AppId = appId;
            Username = native.friends.GetPersonaName();
            SteamId = native.user.GetSteamID();
            BetaName = native.apps.GetCurrentBetaName();
            OwnerSteamId = native.apps.GetAppOwner();
            InstallFolder = new DirectoryInfo( native.apps.GetAppInstallDir( AppId ) );
            BuildId = native.apps.GetAppBuildId();
            CurrentLanguage = native.apps.GetCurrentGameLanguage();
            AvailableLanguages = native.apps.GetAvailableGameLanguages().Split( new[] {';'}, StringSplitOptions.RemoveEmptyEntries ); // TODO: Assumed colon separated

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

            if ( App != null )
            {
                App.Dispose();
                App = null;
            }

            if ( Stats  != null )
            {
                Stats.Dispose();
                Stats = null;
            }

            if ( Achievements != null )
            {
                Achievements.Dispose();
                Achievements = null;
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


        /// <summary>
        /// True if we're subscribed/authorised to be running this app
        /// </summary>
        public bool IsSubscribed => native.apps.BIsSubscribed();

        /// <summary>
        /// True if we're a cybercafe account
        /// </summary>
        public bool IsCybercafe => native.apps.BIsCybercafe();

        /// <summary>
        /// True if we're subscribed/authorised to be running this app, but only temporarily
        /// due to a free weekend etc.
        /// </summary>
        public bool IsSubscribedFromFreeWeekend => native.apps.BIsSubscribedFromFreeWeekend();

        /// <summary>
        /// True if we're in low violence mode (germans are only allowed to see the insides of bodies in porn)
        /// </summary>
        public bool IsLowViolence => native.apps.BIsLowViolence();
    }
}
