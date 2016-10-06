using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Facepunch.Steamworks.Interop
{
    internal class NativeInterface : IDisposable
    {
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
        internal Valve.Steamworks.ISteamHTTP http;
        internal Valve.Steamworks.ISteamUGC ugc;
        internal Valve.Steamworks.ISteamGameServer gameServer;
        internal Valve.Steamworks.ISteamGameServerStats gameServerStats;
        internal Valve.Steamworks.ISteamRemoteStorage remoteStorage;

        internal bool InitClient()
        {
            var user = Valve.Interop.NativeEntrypoints.Extended.SteamAPI_GetHSteamUser();
            var pipe = Valve.Interop.NativeEntrypoints.Extended.SteamAPI_GetHSteamPipe();
            if ( pipe == 0 )
                return false;

            FillInterfaces( user, pipe );

            return true;
        }

        internal bool InitServer()
        {
            var user = Valve.Interop.NativeEntrypoints.Extended.SteamGameServer_GetHSteamUser();
            var pipe = Valve.Interop.NativeEntrypoints.Extended.SteamGameServer_GetHSteamPipe();
            if ( pipe == 0 )
                return false;

            FillInterfaces( pipe, user );

            if ( gameServer.GetIntPtr() == IntPtr.Zero )
            {
                gameServer = null;
                throw new System.Exception( "Steam Server: Couldn't load SteamGameServer012" );
            }

            return true;
        }

        public void FillInterfaces( int hpipe, int huser )
        {
            var clientPtr = Valve.Interop.NativeEntrypoints.Extended.SteamInternal_CreateInterface( "SteamClient017" );
            if ( clientPtr == IntPtr.Zero )
            {
                throw new System.Exception( "Steam Server: Couldn't load SteamClient017" );
            }

            client = new Valve.Steamworks.CSteamClient( clientPtr );

            user = client.GetISteamUser( huser, hpipe, "SteamUser019" );
            utils = client.GetISteamUtils( hpipe, "SteamUtils008" );
            networking = client.GetISteamNetworking( huser, hpipe, "SteamNetworking005" );
            gameServerStats = client.GetISteamGameServerStats( huser, hpipe, "SteamGameServerStats001" );
            http = client.GetISteamHTTP( huser, hpipe, "STEAMHTTP_INTERFACE_VERSION002" );
            inventory = client.GetISteamInventory( huser, hpipe, "STEAMINVENTORY_INTERFACE_V001" );
            ugc = client.GetISteamUGC( huser, hpipe, "STEAMUGC_INTERFACE_VERSION008" );
            apps = client.GetISteamApps( huser, hpipe, "STEAMAPPS_INTERFACE_VERSION008" );
            gameServer = client.GetISteamGameServer( huser, hpipe, "SteamGameServer012" );
            friends = client.GetISteamFriends( huser, hpipe, "SteamFriends015" );
            servers = client.GetISteamMatchmakingServers( huser, hpipe, "SteamMatchMakingServers002" );
            userstats = client.GetISteamUserStats( huser, hpipe, "STEAMUSERSTATS_INTERFACE_VERSION011" );
            screenshots = client.GetISteamScreenshots( huser, hpipe, "STEAMSCREENSHOTS_INTERFACE_VERSION002" );
            remoteStorage = client.GetISteamRemoteStorage( huser, hpipe, "STEAMREMOTESTORAGE_INTERFACE_VERSION013" );
        }

        public void Dispose()
        {
            if ( client != null )
            {
                client.BShutdownIfAllPipesClosed();
                client = null;
            }

            Valve.Interop.NativeEntrypoints.Extended.SteamAPI_Shutdown();
        }
    }
}
