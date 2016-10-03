

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Facepunch.Steamworks
{
    public partial class Server : IDisposable
    {
        internal class Internal : IDisposable
        {
            internal Valve.Steamworks.ISteamClient client;
            internal Valve.Steamworks.ISteamGameServer gameServer;
            internal Valve.Steamworks.ISteamUtils utils;
            internal Valve.Steamworks.ISteamNetworking networking;
            internal Valve.Steamworks.ISteamGameServerStats stats;
            internal Valve.Steamworks.ISteamHTTP http;
            internal Valve.Steamworks.ISteamInventory inventory;
            internal Valve.Steamworks.ISteamUGC ugc;
            internal Valve.Steamworks.ISteamApps apps;

            internal bool Init()
            {
                var user = Valve.Interop.NativeEntrypoints.Extended.SteamGameServer_GetHSteamUser();
                var pipe = Valve.Interop.NativeEntrypoints.Extended.SteamGameServer_GetHSteamPipe();
                if ( pipe == 0 )
                    return false;

                var clientPtr = Valve.Interop.NativeEntrypoints.Extended.SteamInternal_CreateInterface( "SteamClient017" );
                if ( clientPtr == IntPtr.Zero )
                {
                    throw new System.Exception( "Steam Server: Couldn't load SteamClient017" );
                }

                client = new Valve.Steamworks.CSteamClient( clientPtr );


                gameServer = client.GetISteamGameServer( user, pipe, "SteamGameServer012" );

                if ( gameServer.GetIntPtr() == IntPtr.Zero )
                {
                    gameServer = null;
                    throw new System.Exception( "Steam Server: Couldn't load SteamGameServer012" );
                }

                utils = client.GetISteamUtils( pipe, "SteamUtils008" );
                networking = client.GetISteamNetworking( user, pipe, "SteamNetworking005" );
                stats = client.GetISteamGameServerStats( user, pipe, "SteamGameServerStats001" );
                http = client.GetISteamHTTP( user, pipe, "STEAMHTTP_INTERFACE_VERSION002" );
                inventory = client.GetISteamInventory( user, pipe, "STEAMINVENTORY_INTERFACE_V001" );
                ugc = client.GetISteamUGC( user, pipe, "STEAMUGC_INTERFACE_VERSION008" );

                if ( ugc.GetIntPtr() == IntPtr.Zero )
                    throw new System.Exception( "Steam Server: Couldn't load STEAMUGC_INTERFACE_VERSION008" );

                apps = client.GetISteamApps( user, pipe, "STEAMAPPS_INTERFACE_VERSION008" );

                if ( apps.GetIntPtr() == IntPtr.Zero )
                    throw new System.Exception( "Steam Server: Couldn't load STEAMAPPS_INTERFACE_VERSION008" );

                return true;
            }

            public void Dispose()
            {
                if ( client != null )
                {
                    client.BShutdownIfAllPipesClosed();
                    client = null;
                }
            }
        }

        internal Internal native;

        /// <summary>
        /// Current running program's AppId
        /// </summary>
        public uint AppId { get; private set; }

        /// <summary>
        /// Current user's Username
        /// </summary>
        public string Username { get; private set; }

        /// <summary>
        /// Current user's SteamId
        /// </summary>
        public ulong SteamId { get; private set; }

        public enum MessageType : int
        {
            Message = 0,
            Warning = 1
        }

        /// <summary>
        /// Called with a message from Steam
        /// </summary>
        public Action<MessageType, string> OnMessage;

        public Server( uint appId, uint IpAddress, ushort GamePort, ushort QueryPort, bool Secure, string VersionString )
        {
            Valve.Interop.NativeEntrypoints.Extended.SteamInternal_GameServer_Init( IpAddress, 0, GamePort, QueryPort, Secure ? 3 : 2, VersionString );

            native = new Internal();

            //
            // Get other interfaces
            //
            if ( !native.Init() )
            {
                native.Dispose();
                native = null;
                return;
            }

            //
            // Set up warning hook callback
            //
            SteamAPIWarningMessageHook ptr = InternalOnWarning;
            var d = Marshal.GetFunctionPointerForDelegate( ptr );
            var rr = GCHandle.Alloc( d );
            native.utils.SetWarningMessageHook( d );

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
        /// </summary>
        public Server( uint appId, uint IpAddress, ushort GamePort, bool Secure, string VersionString ) : this( appId, IpAddress, GamePort, 0xFFFF, Secure, VersionString )
        {
            
        }

        public void Dispose()
        {
            if ( native != null)
            {
                native.Dispose();
                native = null;
            }

            foreach ( var d in Disposables )
            {
                d.Dispose();
            }
            Disposables.Clear();
        }

        [UnmanagedFunctionPointer( CallingConvention.Cdecl )]
        public delegate void SteamAPIWarningMessageHook( int nSeverity, string pchDebugText );

        private void InternalOnWarning( int nSeverity, string text )
        {
            if ( OnMessage != null )
            {
                OnMessage( ( MessageType)nSeverity, text.ToString() );
            }

            Console.WriteLine( "STEAM: {0}", text );
        }

        internal event Action OnUpdate;

        /// <summary>
        /// Should be called at least once every frame
        /// </summary>
        public void Update()
        {
            if ( native == null )
                return;

            Valve.Interop.NativeEntrypoints.Extended.SteamGameServer_RunCallbacks();
            Valve.Steamworks.SteamAPI.RunCallbacks();
          //  Voice.Update();
          //  Inventory.Update();
          // Networking.Update();

            if ( OnUpdate != null )
                OnUpdate();
        }

        public bool Valid
        {
            get { return native != null; }
        }

        internal Action InstallCallback<T>( Action<T> action )
        {
           // var del = Marshal.GetFunctionPointerForDelegate( action );

            // var ptr = Marshal.GetFunctionPointerForDelegate( action );
            //  Valve.Steamworks.SteamAPI.RegisterCallback( del, type );

            // Valve.Steamworks.SteamAPI.UnregisterCallback( del );

            //return () => Valve.Steamworks.SteamAPI.UnregisterCallback( ptr );
            return null;
        }

        List<IDisposable> Disposables = new List<IDisposable>();

        internal void CallResult<T>( Action<T> Callback, int id )
        {
            var callback = new Facepunch.Steamworks.Interop.Callback<T>( true, id, Callback );
            Disposables.Add( callback );
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
        /// Gets or sets the current Product
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
        /// Gets or sets the current Passworded
        /// </summary>
        public bool Passworded
        {
            get { return _passworded; }
            set { if ( _passworded == value ) return; native.gameServer.SetPasswordProtected( value ); _passworded = value; }
        }
        private bool _passworded;

        /// <summary>
        /// Gets or sets the current GameTags
        /// </summary>
        public string GameTags
        {
            get { return _gametags; }
            set { if ( _gametags == value ) return; native.gameServer.SetGameTags( value ); _gametags = value; }
        }
        private string _gametags = "";

        /// <summary>
        /// Log onto Steam anonymously
        /// </summary>
        public void LogOnAnonymous()
        {
            native.gameServer.LogOnAnonymous();
        }

        Dictionary<string, string> KeyValue = new Dictionary<string, string>();

        /// <summary>
        /// Sets a Key Value
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

        public void UpdatePlayer( ulong steamid, string name, int score )
        {
            native.gameServer.BUpdateUserData( steamid, name, (uint) score );
        }

        public bool LoggedOn
        {
            get { return native.gameServer.BLoggedOn(); }
        }
    }
}
