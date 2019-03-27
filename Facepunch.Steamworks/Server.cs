using System;
using System.Collections.Generic;
using SteamNative;

namespace Facepunch.Steamworks
{
    /// <summary>
    /// Initialize this class for Game Servers.
    /// 
    /// Game servers offer a limited amount of Steam functionality - and don't require the Steam client.
    /// </summary>
    public partial class Server : BaseSteamworks
    {
        /// <summary>
        /// A singleton accessor to get the current client instance.
        /// </summary>
        public static Server Instance { get; private set; }

        /// <summary>
        /// Serversd 's SteamId. Only set once logged in with Steam
        /// </summary>
        public ulong? SteamId { get; internal set; }

        internal override bool IsGameServer { get { return true; } }

        public ServerQuery Query { get; internal set; }
        public ServerStats Stats { get; internal set; }

        /// <summary>
        /// Initialize a Steam Server instance
        /// </summary>
        public Server( uint appId, ServerInit init) : base( appId )
        {
            if ( Instance != null )
            {
                throw new System.Exception( "Only one Facepunch.Steamworks.Server can exist - dispose the old one before trying to create a new one." );
            }

            Instance = this;
            native = new Interop.NativeInterface();
            uint ipaddress = 0; // Any Port

            if ( init.SteamPort == 0 ) init.RandomSteamPort();
            if ( init.IpAddress != null ) ipaddress = Utility.IpToInt32( init.IpAddress );

            //
            // Get other interfaces
            //
            if ( !native.InitServer( this, ipaddress, init.SteamPort, init.GamePort, init.QueryPort, init.Secure ? 3 : 2, init.VersionString ) )
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
            // Initial settings
            //
            native.gameServer.EnableHeartbeats( true );
            MaxPlayers = 32;
            BotCount = 0;
            Product = $"{AppId}";
            ModDir = init.ModDir;
            GameDescription = init.GameDescription;
            Passworded = false;
            DedicatedServer = true;

            //
            // Child classes
            //
            Auth = new ServerAuth( this );
            Query = new ServerQuery( this );
            Stats = new ServerStats( this );

            RegisterCallback<SteamNative.SteamServersConnected_t>( OnSteamServersConnected );

            //
            // Run update, first call does some initialization
            //
            Update();
        }

        ~Server()
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

            native.api.SteamGameServer_RunCallbacks();

            base.Update();
        }

        /// <summary>
        /// Sets whether this should be marked as a dedicated server.
        /// If not, it is assumed to be a listen server.
        /// </summary>
        public bool DedicatedServer
        {
            get { return _dedicatedServer; }
            set { if ( _dedicatedServer == value ) return; native.gameServer.SetDedicatedServer( value ); _dedicatedServer = value; }
        }
        private bool _dedicatedServer;

        /// <summary>
        /// Gets or sets the current MaxPlayers. 
        /// This doesn't enforce any kind of limit, it just updates the master server.
        /// </summary>
        public int MaxPlayers
        {
            get { return _maxplayers; }
            set { if ( _maxplayers == value ) return; native.gameServer.SetMaxPlayerCount( value ); _maxplayers = value; }
        }
        private int _maxplayers = 0;

        /// <summary>
        /// Gets or sets the current BotCount. 
        /// This doesn't enforce any kind of limit, it just updates the master server.
        /// </summary>
        public int BotCount
        {
            get { return _botcount; }
            set { if ( _botcount == value ) return; native.gameServer.SetBotPlayerCount( value ); _botcount = value; }
        }
        private int _botcount = 0;

        /// <summary>
        /// Gets or sets the current Map Name. 
        /// </summary>
        public string MapName
        {
            get { return _mapname; }
            set { if ( _mapname == value ) return; native.gameServer.SetMapName( value ); _mapname = value; }
        }
        private string _mapname;

        /// <summary>
        /// Gets or sets the current ModDir
        /// </summary>
        public string ModDir
        {
            get { return _modDir; }
            internal set { if ( _modDir == value ) return; native.gameServer.SetModDir( value ); _modDir = value; }
        }
        private string _modDir = "";

        /// <summary>
        /// Gets the current product
        /// </summary>
        public string Product
        {
            get { return _product; }
            internal set { if ( _product == value ) return; native.gameServer.SetProduct( value ); _product = value; }
        }
        private string _product = "";

        /// <summary>
        /// Gets or sets the current Product
        /// </summary>
        public string GameDescription
        {
            get { return _gameDescription; }
            internal set { if ( _gameDescription == value ) return; native.gameServer.SetGameDescription( value ); _gameDescription = value; }
        }
        private string _gameDescription = "";

        /// <summary>
        /// Gets or sets the current ServerName
        /// </summary>
        public string ServerName
        {
            get { return _serverName; }
            set { if ( _serverName == value ) return; native.gameServer.SetServerName( value ); _serverName = value; }
        }
        private string _serverName = "";

        /// <summary>
        /// Set whether the server should report itself as passworded
        /// </summary>
        public bool Passworded
        {
            get { return _passworded; }
            set { if ( _passworded == value ) return; native.gameServer.SetPasswordProtected( value ); _passworded = value; }
        }
        private bool _passworded;

        /// <summary>
        /// Gets or sets the current GameTags. This is a comma seperated list of tags for this server.
        /// When querying the server list you can filter by these tags.
        /// </summary>
        public string GameTags
        {
            get { return _gametags; }
            set { if ( _gametags == value ) return; native.gameServer.SetGameTags( value ); _gametags = value; }
        }
        private string _gametags = "";

        /// <summary>
        /// Log onto Steam anonymously.
        /// </summary>
        public void LogOnAnonymous()
        {
            native.gameServer.LogOnAnonymous();
            ForceHeartbeat();
        }

        /// <summary>
        /// Returns true if the server is connected and registered with the Steam master server
        /// You should have called LogOnAnonymous etc on startup.
        /// </summary>
        public bool LoggedOn => native.gameServer.BLoggedOn();

        Dictionary<string, string> KeyValue = new Dictionary<string, string>();

        /// <summary>
        /// Sets a Key Value. These can be anything you like, and are accessible
        /// when querying servers from the server list.
        /// 
        /// Information describing gamemodes are common here.
        /// </summary>
        public void SetKey( string Key, string Value )
        {
            if ( KeyValue.ContainsKey( Key ) )
            {
                if ( KeyValue[Key] == Value )
                    return;

                KeyValue[Key] = Value;
            }
            else
            {
                KeyValue.Add( Key, Value );
            }

            native.gameServer.SetKeyValue( Key, Value );
        }

        /// <summary>
        /// Update this connected player's information. You should really call this
        /// any time a player's name or score changes. This keeps the information shown
        /// to server queries up to date.
        /// </summary>
        public void UpdatePlayer( ulong steamid, string name, int score )
        {
            native.gameServer.BUpdateUserData( steamid, name, (uint) score );
        }


        /// <summary>
        /// Shutdown interface, disconnect from Steam
        /// </summary>
        public override void Dispose()
        {
            if ( disposed ) return;

            if ( Auth != null )
            {
                Auth = null;
            }

            if ( Query != null )
            {
                Query = null;
            }

            if ( Stats != null )
            {
                Stats = null;
            }

            if ( Instance == this )
            {
                Instance = null;
            }

            base.Dispose();
        }

        /// <summary>
        /// To the best of its ability this tries to get the server's
        /// current public ip address. Be aware that this is likely to return
        /// null for the first few seconds after initialization.
        /// </summary>
        public System.Net.IPAddress PublicIp
        {
            get
            {
                var ip = native.gameServer.GetPublicIP();
                if ( ip == 0 ) return null;

                return Utility.Int32ToIp( ip );
            }
        }

        /// <summary>
        /// Enable or disable heartbeats, which are sent regularly to the master server.
        /// Enabled by default.
        /// </summary>
        public bool AutomaticHeartbeats
        {
            set { native.gameServer.EnableHeartbeats( value ); }
        }

        /// <summary>
        /// Set heartbeat interval, if automatic heartbeats are enabled.
        /// You can leave this at the default.
        /// </summary>
        public int AutomaticHeartbeatRate
        {
            set { native.gameServer.SetHeartbeatInterval( value ); }
        }

        /// <summary>
        /// Force send a heartbeat to the master server instead of waiting
        /// for the next automatic update (if you've left them enabled)
        /// </summary>
        public void ForceHeartbeat()
        {
            native.gameServer.ForceHeartbeat();
        }

        private void OnSteamServersConnected( SteamServersConnected_t p )
        {
            SteamId = native.gameServer.GetSteamID();
        }
    }
}
