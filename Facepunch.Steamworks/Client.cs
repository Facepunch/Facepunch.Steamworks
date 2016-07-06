

using System;

namespace Facepunch.Steamworks
{
    public class Client : IDisposable
    {
        private Valve.Steamworks.ISteamClient _client;
        private uint _hpipe;
        private uint _huser;
        private Valve.Steamworks.ISteamUser _user;
        private Valve.Steamworks.ISteamFriends _friends;

        /// <summary>
        /// Current running program's AppId
        /// </summary>
        public int AppId;

        /// <summary>
        /// Current user's Username
        /// </summary>
        public string Username { get; private set; }

        public Client( int appId )
        {
            Valve.Steamworks.SteamAPI.Init( (uint) appId );
            _client = Valve.Steamworks.SteamAPI.SteamClient();

            if ( _client.GetIntPtr() == IntPtr.Zero )
            {
                _client = null;
                return;
            }


            _hpipe = _client.CreateSteamPipe();
            _huser = _client.ConnectToGlobalUser( _hpipe );

            _friends = _client.GetISteamFriends( _huser, _hpipe, "SteamFriends015" );
           // _user = Valve.Steamworks.SteamAPI.SteamUser();

            AppId = appId;
            Username = _friends.GetPersonaName();
        }

        public void Dispose()
        {
            if ( _client != null )
            {
                _client.BShutdownIfAllPipesClosed();
                _client = null;
            }
        }

        public bool Valid
        {
            get { return _client != null; }
        }
    }
}
