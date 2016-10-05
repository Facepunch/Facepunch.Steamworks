

using System;
using System.Runtime.InteropServices;

namespace Facepunch.Steamworks
{
    public partial class Client : BaseSteamworks
    {
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




        public Client( uint appId )
        {
            Valve.Steamworks.SteamAPIInterop.SteamAPI_Init();

            native = new Interop.NativeInterface();

            //
            // Get other interfaces
            //
            if ( !native.InitClient() )
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
            // Setup interfaces that client and server both have
            //
            SetupCommonInterfaces();

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
        public override void Update()
        {
            if ( !IsValid )
                return;

            Valve.Steamworks.SteamAPI.RunCallbacks();

            Voice.Update();

            base.Update();
        }

    }
}
