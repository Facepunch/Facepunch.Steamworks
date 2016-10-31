using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SteamNative;

namespace Facepunch.Steamworks.Interop
{
    internal class NativeInterface : IDisposable
    {
        internal SteamNative.SteamApi api;
        internal SteamNative.SteamClient client;
        internal SteamNative.SteamUser user;
        internal SteamNative.SteamApps apps;
        internal SteamNative.SteamFriends friends;
        internal SteamNative.SteamMatchmakingServers servers;
        internal SteamNative.SteamInventory inventory;
        internal SteamNative.SteamNetworking networking;
        internal SteamNative.SteamUserStats userstats;
        internal SteamNative.SteamUtils utils;
        internal SteamNative.SteamScreenshots screenshots;
        internal SteamNative.SteamHTTP http;
        internal SteamNative.SteamUGC ugc;
        internal SteamNative.SteamGameServer gameServer;
        internal SteamNative.SteamGameServerStats gameServerStats;
        internal SteamNative.SteamRemoteStorage remoteStorage;

        private bool isServer;

        private HSteamUser hUser;
        private HSteamPipe hPipe;

        internal bool InitClient( BaseSteamworks steamworks )
        {
            isServer = false;

            api = new SteamNative.SteamApi( steamworks, (IntPtr) 1 );

            if ( !api.SteamAPI_Init() )
                return false;

            hUser = api.SteamAPI_GetHSteamUser();
            hPipe = api.SteamAPI_GetHSteamPipe();
            if ( hPipe == 0 )
                return false;

            FillInterfaces( steamworks, hUser, hPipe );

            // Ensure that the user has logged into Steam. This will always return true if the game is launched
            // from Steam, but if Steam is at the login prompt when you run your game it will return false.
            if ( !user.BLoggedOn() )
                return false;

            return true;
        }

        internal bool InitServer( BaseSteamworks steamworks, uint IpAddress /*uint32*/, ushort usPort /*uint16*/, ushort GamePort /*uint16*/, ushort QueryPort /*uint16*/, int eServerMode /*int*/, string pchVersionString /*const char **/)
        {
            isServer = true;

            api = new SteamNative.SteamApi( steamworks, ( IntPtr)1 );

            if ( !api.SteamInternal_GameServer_Init( IpAddress, usPort, GamePort, QueryPort, eServerMode, pchVersionString ) )
            {
                return false;
            }

            hUser = api.SteamGameServer_GetHSteamUser();
            hPipe = api.SteamGameServer_GetHSteamPipe();
            if ( hPipe == 0 )
                return false;

            FillInterfaces( steamworks, hPipe, hUser );

            if ( !gameServer.IsValid )
            {
                gameServer = null;
                throw new System.Exception( "Steam Server: Couldn't load SteamGameServer012" );
            }

            return true;
        }

        public void FillInterfaces( BaseSteamworks steamworks, int hpipe, int huser )
        {
            var clientPtr = api.SteamInternal_CreateInterface( "SteamClient017" );
            if ( clientPtr == IntPtr.Zero )
            {
                throw new System.Exception( "Steam Server: Couldn't load SteamClient017" );
            }

            client = new SteamNative.SteamClient( steamworks, clientPtr );

            user = client.GetISteamUser( huser, hpipe, SteamNative.Defines.STEAMUSER_INTERFACE_VERSION );
            utils = client.GetISteamUtils( hpipe, SteamNative.Defines.STEAMUTILS_INTERFACE_VERSION );
            networking = client.GetISteamNetworking( huser, hpipe, SteamNative.Defines.STEAMNETWORKING_INTERFACE_VERSION );
            gameServerStats = client.GetISteamGameServerStats( huser, hpipe, SteamNative.Defines.STEAMGAMESERVERSTATS_INTERFACE_VERSION );
            http = client.GetISteamHTTP( huser, hpipe, SteamNative.Defines.STEAMHTTP_INTERFACE_VERSION );
            inventory = client.GetISteamInventory( huser, hpipe, SteamNative.Defines.STEAMINVENTORY_INTERFACE_VERSION );
            ugc = client.GetISteamUGC( huser, hpipe, SteamNative.Defines.STEAMUGC_INTERFACE_VERSION );
            apps = client.GetISteamApps( huser, hpipe, SteamNative.Defines.STEAMAPPS_INTERFACE_VERSION );
            gameServer = client.GetISteamGameServer( huser, hpipe, SteamNative.Defines.STEAMGAMESERVER_INTERFACE_VERSION );
            friends = client.GetISteamFriends( huser, hpipe, SteamNative.Defines.STEAMFRIENDS_INTERFACE_VERSION );
            servers = client.GetISteamMatchmakingServers( huser, hpipe, SteamNative.Defines.STEAMMATCHMAKINGSERVERS_INTERFACE_VERSION );
            userstats = client.GetISteamUserStats( huser, hpipe, SteamNative.Defines.STEAMUSERSTATS_INTERFACE_VERSION );
            screenshots = client.GetISteamScreenshots( huser, hpipe, SteamNative.Defines.STEAMSCREENSHOTS_INTERFACE_VERSION );
            remoteStorage = client.GetISteamRemoteStorage( huser, hpipe, SteamNative.Defines.STEAMREMOTESTORAGE_INTERFACE_VERSION );
        }

        public void Dispose()
        {
            if ( client != null )
            {
                if ( hPipe != 0 )
                {
                    if ( hUser != 0 )
                    {
                        client.ReleaseUser( hPipe, hUser );
                        hUser = 0;
                    }

                    client.BReleaseSteamPipe( hPipe );
                    hPipe = 0;
                }

                if ( !client.BShutdownIfAllPipesClosed() )
                    Console.WriteLine( "BShutdownIfAllPipesClosed returned false" );

                client.Dispose();
                client = null;
            }

            if ( user != null )
            {
                user.Dispose();
                user = null;
            }

            if ( utils != null )
            {
                utils.Dispose();
                utils = null;
            }

            if ( networking != null )
            {
                networking.Dispose();
                networking = null;
            }

            if ( gameServerStats != null )
            {
                gameServerStats.Dispose();
                gameServerStats = null;
            }

            if ( http != null )
            {
                http.Dispose();
                http = null;
            }

            if ( inventory != null )
            {
                inventory.Dispose();
                inventory = null;
            }

            if ( ugc != null )
            {
                ugc.Dispose();
                ugc = null;
            }

            if ( apps != null )
            {
                apps.Dispose();
                apps = null;
            }

            if ( gameServer != null )
            {
                //
                // Calling this can cause the process to hang
                //
                //gameServer.LogOff();
                
                gameServer.Dispose();
                gameServer = null;
            }

            if ( friends != null )
            {
                friends.Dispose();
                friends = null;
            }

            if ( servers != null )
            {
                servers.Dispose();
                servers = null;
            }

            if ( userstats != null )
            {
                userstats.Dispose();
                userstats = null;
            }

            if ( screenshots != null )
            {
                screenshots.Dispose();
                screenshots = null;
            }

            if ( remoteStorage != null )
            {
                remoteStorage.Dispose();
                remoteStorage = null;
            }

            if ( api != null )
            {
                if ( isServer )
                    api.SteamGameServer_Shutdown();
                else
                    api.SteamAPI_Shutdown();

                api.Dispose();
                api = null;
            }
        }
    }
}
