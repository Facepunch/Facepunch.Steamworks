using System;
using System.IO;
using System.Runtime.InteropServices;

namespace Facepunch.Steamworks
{
    public partial class Client : BaseSteamworks
    {
        /// <summary>
        /// A singleton accessor to get the current client instance.
        /// </summary>
        public static Client Instance { get; private set; }

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
        public LobbyList LobbyList { get; private set; }
        public App App { get; private set; }
        public Achievements Achievements { get; private set; }
        public Stats Stats { get; private set; }
        public MicroTransactions MicroTransactions { get; private set; }
        public User User { get; private set; }
        public RemoteStorage RemoteStorage { get; private set; }
        public Overlay Overlay { get; private set; }

        public Client( uint appId ) : base( appId )
        {
            if ( Instance != null )
            {
                throw new System.Exception( "Only one Facepunch.Steamworks.Client can exist - dispose the old one before trying to create a new one." );
            }

            Instance = this;
            native = new Interop.NativeInterface();

            //
            // Get other interfaces
            //
            if ( !native.InitClient( this ) )
            {
                native.Dispose();
                native = null;
                Instance = null;
                return;
            }

            //
            // Register Callbacks
            //

            SteamNative.Callbacks.RegisterCallbacks( this );

            //
            // Setup interfaces that client and server both have
            //
            SetupCommonInterfaces();

            //
            // Client only interfaces
            //
            Voice = new Voice( this );
            ServerList = new ServerList( this );
            LobbyList = new LobbyList(this);
            App = new App( this );
            Stats = new Stats( this );
            Achievements = new Achievements( this );
            MicroTransactions = new MicroTransactions( this );
            User = new User( this );
            RemoteStorage = new RemoteStorage( this );
            Overlay = new Overlay( this );

            Workshop.friends = Friends;

            Stats.UpdateStats();

            //
            // Cache common, unchanging info
            //
            AppId = appId;
            Username = native.friends.GetPersonaName();
            SteamId = native.user.GetSteamID();
            BetaName = native.apps.GetCurrentBetaName();
            OwnerSteamId = native.apps.GetAppOwner();
            var appInstallDir = native.apps.GetAppInstallDir(AppId);

            if (!String.IsNullOrEmpty(appInstallDir) && Directory.Exists(appInstallDir))
                InstallFolder = new DirectoryInfo(appInstallDir);

            BuildId = native.apps.GetAppBuildId();
            CurrentLanguage = native.apps.GetCurrentGameLanguage();
            AvailableLanguages = native.apps.GetAvailableGameLanguages().Split( new[] {';'}, StringSplitOptions.RemoveEmptyEntries ); // TODO: Assumed colon separated

            //
            // Run update, first call does some initialization
            //
            Update();
        }

        ~Client()
        {
            Dispose();
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
            Friends.Cycle();

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
            if ( disposed ) return;

            if ( Voice != null )
            {
                Voice = null;
            }

            if ( ServerList  != null )
            {
                ServerList.Dispose();
                ServerList = null;
            }

            if (LobbyList != null)
            {
                LobbyList.Dispose();
                LobbyList = null;
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

            if ( MicroTransactions != null )
            {
                MicroTransactions.Dispose();
                MicroTransactions = null;
            }

            if ( User != null )
            {
                User.Dispose();
                User = null;
            }

            if ( RemoteStorage  != null )
            {
                RemoteStorage.Dispose();
                RemoteStorage = null;
            }

            if ( Instance == this )
            {
                Instance = null;
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
        /// Checks if the current user's Steam client is connected and logged on to the Steam servers.
        /// If it's not then no real-time services provided by the Steamworks API will be enabled. 
        /// The Steam client will automatically be trying to recreate the connection as often as possible.
        /// All of the API calls that rely on this will check internally.
        /// </summary>
        public bool IsLoggedOn => native.user.BLoggedOn();

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

        /// <summary>
        /// Checks if your executable was launched through Steam and relaunches it through Steam if it wasn't.
        /// If this returns true then it starts the Steam client if required and launches your game again through it, 
        /// and you should quit your process as soon as possible. This effectively runs steam://run/AppId so it may 
        /// not relaunch the exact executable that called it, as it will always relaunch from the version installed 
        /// in your Steam library folder.
        /// If it returns false, then your game was launched by the Steam client and no action needs to be taken.
        /// One exception is if a steam_appid.txt file is present then this will return false regardless. This allows
        /// you to develop and test without launching your game through the Steam client. Make sure to remove the 
        /// steam_appid.txt file when uploading the game to your Steam depot!
        /// </summary>
        public static bool RestartIfNecessary( uint appid )
        {
            using ( var api = new SteamNative.SteamApi() )
            {
                return api.SteamAPI_RestartAppIfNecessary( appid );
            }
        }
    }
}
