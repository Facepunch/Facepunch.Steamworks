

using System;
using System.Runtime.InteropServices;

namespace Facepunch.Steamworks
{
    public partial class Client : IDisposable
    {


        internal class Internal : IDisposable
        {
            private uint _hpipe;
            private uint _huser;

            internal Valve.Steamworks.ISteamClient client;
            internal Valve.Steamworks.ISteamUser user;
            internal Valve.Steamworks.ISteamApps apps;
            internal Valve.Steamworks.ISteamFriends friends;
            internal Valve.Steamworks.ISteamMatchmakingServers servers;
            internal Valve.Steamworks.ISteamInventory inventory;
            internal Valve.Steamworks.ISteamNetworking networking;
            internal Valve.Steamworks.ISteamUserStats userstats;
            internal Valve.Steamworks.ISteamUtils utils;
            internal Valve.Steamworks.ISteamScreenshots screenshots;

            internal bool Init()
            {
                client = Valve.Steamworks.SteamAPI.SteamClient();

                if ( client.GetIntPtr() == IntPtr.Zero )
                {
                    client = null;
                    return false;
                }

                _hpipe = client.CreateSteamPipe();
                _huser = client.ConnectToGlobalUser( _hpipe );

                friends = client.GetISteamFriends( _huser, _hpipe, "SteamFriends015" );
                user = client.GetISteamUser( _huser, _hpipe, "SteamUser019" );
                servers = client.GetISteamMatchmakingServers( _huser, _hpipe, "SteamMatchMakingServers002" );
                inventory = client.GetISteamInventory( _huser, _hpipe, "STEAMINVENTORY_INTERFACE_V001" );
                networking = client.GetISteamNetworking( _huser, _hpipe, "SteamNetworking005" );
                apps = client.GetISteamApps( _huser, _hpipe, "STEAMAPPS_INTERFACE_VERSION008" );
                userstats = client.GetISteamUserStats( _huser, _hpipe, "STEAMUSERSTATS_INTERFACE_VERSION011" );
                screenshots = client.GetISteamScreenshots( _huser, _hpipe, "STEAMSCREENSHOTS_INTERFACE_VERSION002" );

                utils = client.GetISteamUtils( _hpipe, "SteamUtils008" );

                return true;
            }

            public void Dispose()
            {
                if ( _hpipe > 0 && client != null )
                {
                    if ( _huser > 0 )
                        client.ReleaseUser( _hpipe, _huser );

                    client.BReleaseSteamPipe( _hpipe );

                    _huser = 0;
                    _hpipe = 0;
                }

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

        /// <summary>
        /// Current Beta name, if ser
        /// </summary>
        public string BetaName { get; private set; }

        public enum MessageType : int
        {
            Message = 0,
            Warning = 1
        }

        /// <summary>
        /// Called with a message from Steam
        /// </summary>
        public Action<MessageType, string> OnMessage;

        public Client( uint appId )
        {
            Valve.Steamworks.SteamAPI.Init( appId );

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
            native.client.SetWarningMessageHook( Marshal.GetFunctionPointerForDelegate( ptr ) );

            //
            // Cache common, unchanging info
            //
            AppId = appId;
            Username = native.friends.GetPersonaName();
            SteamId = native.user.GetSteamID();
            BetaName = native.apps.GetCurrentBetaName();
        }

        public void Dispose()
        {
            if ( native  != null)
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

        /// <summary>
        /// Should be called at least once every frame
        /// </summary>
        public void Update()
        {
            Valve.Steamworks.SteamAPI.RunCallbacks();
            Voice.Update();
            Inventory.Update();
            Networking.Update();
        }

        public bool Valid
        {
            get { return native != null; }
        }

        internal Action InstallCallback( int type, Delegate action )
        {
            var ptr = Marshal.GetFunctionPointerForDelegate( action );
            Valve.Steamworks.SteamAPI.RegisterCallback( ptr, type );

            return () => Valve.Steamworks.SteamAPI.UnregisterCallback( ptr );
        }
    }
}
