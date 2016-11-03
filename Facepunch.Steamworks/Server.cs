

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

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
        /// Current user's Username
        /// </summary>
        public string Username { get; private set; }

        /// <summary>
        /// Current user's SteamId
        /// </summary>
        public ulong SteamId { get; private set; }

        internal override bool IsGameServer { get { return true; } }

        /// <summary>
        /// Initialize a Steam Server instance
        /// </summary>
        /// <param name="appId">You game's AppId</param>
        /// <param name="IpAddress">The IP Address to bind to. Can be 0 to mean "any".</param>
        /// <param name="GamePort">The port you game listens to for connections.</param>
        /// <param name="QueryPort">The port Steam should use for server queries.</param>
        /// <param name="Secure">True if you want to use VAC</param>
        /// <param name="VersionString">A string defining version, ie "1001"</param>
        public Server( uint appId, uint IpAddress, ushort GamePort, ushort QueryPort, bool Secure, string VersionString )
        {
            native = new Interop.NativeInterface();

            //
            // Get other interfaces
            //
            if ( !native.InitServer( this, IpAddress, 0, GamePort, QueryPort, Secure ? 3 : 2, VersionString ) )
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
            // Cache common, unchanging info
            //
            AppId = appId;

            //
            // Initial settings
            //
            native.gameServer.EnableHeartbeats( true );
            MaxPlayers = 32;
            BotCount = 0;
            MapName = "unset";

            //
            // Run update, first call does some initialization
            //
            Update();
        }

        /// <summary>
        /// Initialize a server - query port will use the same as GamePort (MASTERSERVERUPDATERPORT_USEGAMESOCKETSHARE)
        /// This means you'll need to detect and manually process and reply to server queries.
        /// </summary>
        public Server( uint appId, uint IpAddress, ushort GamePort, bool Secure, string VersionString ) : this( appId, IpAddress, GamePort, 0xFFFF, Secure, VersionString )
        {
            
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
            set { if ( _modDir == value ) return; native.gameServer.SetModDir( value ); _modDir = value; }
        }
        private string _modDir = "";

        /// <summary>
        /// Gets or sets the current product. This isn't really used.
        /// </summary>
        public string Product
        {
            get { return _product; }
            set { if ( _product == value ) return; native.gameServer.SetProduct( value ); _product = value; }
        }
        private string _product = "";

        /// <summary>
        /// Gets or sets the current Product
        /// </summary>
        public string GameDescription
        {
            get { return _gameDescription; }
            set { if ( _gameDescription == value ) return; native.gameServer.SetGameDescription( value ); _gameDescription = value; }
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
        }

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
        /// Returns true if the server is connected and registered with the Steam master server
        /// You should have called LogOnAnonymous etc on startup.
        /// </summary>
        public bool LoggedOn
        {
            get { return native.gameServer.BLoggedOn(); }
        }
    }
}
