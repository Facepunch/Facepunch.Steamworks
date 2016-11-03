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

        public Voice Voice { get; internal set; }

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

            native.api.SteamAPI_RunCallbacks();

            Voice.Update();
            base.Update();
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

            base.Dispose();
        }
    }
}
