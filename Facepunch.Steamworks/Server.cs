

using System;
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

        public Server( uint appId, uint IpAddress, ushort SteamPort, ushort GamePort, ushort QueryPort, bool Secure, string VersionString )
        {
            if ( !Valve.Interop.NativeEntrypoints.Extended.SteamInternal_GameServer_Init( IpAddress, SteamPort, GamePort, QueryPort, Secure ? 3 : 2 , VersionString ) )
            {
                return;
            }

            //Valve.Steamworks.SteamAPI.Init( appId );

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
          //  native.client.SetWarningMessageHook( Marshal.GetFunctionPointerForDelegate( ptr ) );

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

        public void Dispose()
        {
            if ( native != null)
            {
                native.Dispose();
                native = null;
            }
        }

        [UnmanagedFunctionPointer( CallingConvention.Cdecl )]
        public delegate void SteamAPIWarningMessageHook( int nSeverity, System.Text.StringBuilder pchDebugText );

        private void InternalOnWarning( int nSeverity, System.Text.StringBuilder text )
        {
            if ( OnMessage != null )
            {
                OnMessage( ( MessageType)nSeverity, text.ToString() );
            }
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

        internal Action InstallCallback( int type, Delegate action )
        {
            var del = Marshal.GetFunctionPointerForDelegate( action );

            // var ptr = Marshal.GetFunctionPointerForDelegate( action );
          //  Valve.Steamworks.SteamAPI.RegisterCallback( del, type );

           // Valve.Steamworks.SteamAPI.UnregisterCallback( del );

            //return () => Valve.Steamworks.SteamAPI.UnregisterCallback( ptr );
            return null;
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
    }
}
