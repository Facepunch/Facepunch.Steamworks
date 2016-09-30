//====== Copyright 1996-2016, Valve Corporation, All rights reserved. =======
//
// Purpose: This file contains C#/managed code bindings for the SteamAPI interfaces
// This file is auto-generated, do not edit it.
//
//=============================================================================

using System;
using System.Runtime.InteropServices;
using Valve.Steamworks;
using Valve.Interop;

namespace Valve.Interop
{

    internal class NativeEntrypoints
    {
        internal class Extended
        {
            [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl )]
            public static extern bool SteamInternal_GameServer_Init( uint unIP, ushort usSteamPort, ushort usGamePort, ushort usQueryPort, int eServerMode, string pchVersionString );

            [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl)]
            internal static extern IntPtr SteamInternal_CreateInterface( string ver );

            [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl )]
            internal static extern uint SteamGameServer_GetHSteamUser();

            [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl )]
            internal static extern uint SteamGameServer_GetHSteamPipe();

            [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl )]
            internal static extern void SteamGameServer_Shutdown();

            [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl )]
            internal static extern void SteamGameServer_RunCallbacks();
            
        }

        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamClient_CreateSteamPipe" )]
        internal static extern uint SteamAPI_ISteamClient_CreateSteamPipe( IntPtr instancePtr );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamClient_BReleaseSteamPipe" )]
        internal static extern bool SteamAPI_ISteamClient_BReleaseSteamPipe( IntPtr instancePtr, uint hSteamPipe );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamClient_ConnectToGlobalUser" )]
        internal static extern uint SteamAPI_ISteamClient_ConnectToGlobalUser( IntPtr instancePtr, uint hSteamPipe );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamClient_CreateLocalUser" )]
        internal static extern uint SteamAPI_ISteamClient_CreateLocalUser( IntPtr instancePtr, ref uint phSteamPipe, uint eAccountType );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamClient_ReleaseUser" )]
        internal static extern void SteamAPI_ISteamClient_ReleaseUser( IntPtr instancePtr, uint hSteamPipe, uint hUser );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamClient_GetISteamUser" )]
        internal static extern IntPtr SteamAPI_ISteamClient_GetISteamUser( IntPtr instancePtr, uint hSteamUser, uint hSteamPipe, string pchVersion );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamClient_GetISteamGameServer" )]
        internal static extern IntPtr SteamAPI_ISteamClient_GetISteamGameServer( IntPtr instancePtr, uint hSteamUser, uint hSteamPipe, string pchVersion );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamClient_SetLocalIPBinding" )]
        internal static extern void SteamAPI_ISteamClient_SetLocalIPBinding( IntPtr instancePtr, uint unIP, char usPort );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamClient_GetISteamFriends" )]
        internal static extern IntPtr SteamAPI_ISteamClient_GetISteamFriends( IntPtr instancePtr, uint hSteamUser, uint hSteamPipe, string pchVersion );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamClient_GetISteamUtils" )]
        internal static extern IntPtr SteamAPI_ISteamClient_GetISteamUtils( IntPtr instancePtr, uint hSteamPipe, string pchVersion );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamClient_GetISteamMatchmaking" )]
        internal static extern IntPtr SteamAPI_ISteamClient_GetISteamMatchmaking( IntPtr instancePtr, uint hSteamUser, uint hSteamPipe, string pchVersion );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamClient_GetISteamMatchmakingServers" )]
        internal static extern IntPtr SteamAPI_ISteamClient_GetISteamMatchmakingServers( IntPtr instancePtr, uint hSteamUser, uint hSteamPipe, string pchVersion );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamClient_GetISteamGenericInterface" )]
        internal static extern IntPtr SteamAPI_ISteamClient_GetISteamGenericInterface( IntPtr instancePtr, uint hSteamUser, uint hSteamPipe, string pchVersion );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamClient_GetISteamUserStats" )]
        internal static extern IntPtr SteamAPI_ISteamClient_GetISteamUserStats( IntPtr instancePtr, uint hSteamUser, uint hSteamPipe, string pchVersion );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamClient_GetISteamGameServerStats" )]
        internal static extern IntPtr SteamAPI_ISteamClient_GetISteamGameServerStats( IntPtr instancePtr, uint hSteamuser, uint hSteamPipe, string pchVersion );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamClient_GetISteamApps" )]
        internal static extern IntPtr SteamAPI_ISteamClient_GetISteamApps( IntPtr instancePtr, uint hSteamUser, uint hSteamPipe, string pchVersion );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamClient_GetISteamNetworking" )]
        internal static extern IntPtr SteamAPI_ISteamClient_GetISteamNetworking( IntPtr instancePtr, uint hSteamUser, uint hSteamPipe, string pchVersion );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamClient_GetISteamRemoteStorage" )]
        internal static extern IntPtr SteamAPI_ISteamClient_GetISteamRemoteStorage( IntPtr instancePtr, uint hSteamuser, uint hSteamPipe, string pchVersion );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamClient_GetISteamScreenshots" )]
        internal static extern IntPtr SteamAPI_ISteamClient_GetISteamScreenshots( IntPtr instancePtr, uint hSteamuser, uint hSteamPipe, string pchVersion );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamClient_GetIPCCallCount" )]
        internal static extern uint SteamAPI_ISteamClient_GetIPCCallCount( IntPtr instancePtr );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamClient_SetWarningMessageHook" )]
        internal static extern void SteamAPI_ISteamClient_SetWarningMessageHook( IntPtr instancePtr, IntPtr pFunction );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamClient_BShutdownIfAllPipesClosed" )]
        internal static extern bool SteamAPI_ISteamClient_BShutdownIfAllPipesClosed( IntPtr instancePtr );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamClient_GetISteamHTTP" )]
        internal static extern IntPtr SteamAPI_ISteamClient_GetISteamHTTP( IntPtr instancePtr, uint hSteamuser, uint hSteamPipe, string pchVersion );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamClient_GetISteamUnifiedMessages" )]
        internal static extern IntPtr SteamAPI_ISteamClient_GetISteamUnifiedMessages( IntPtr instancePtr, uint hSteamuser, uint hSteamPipe, string pchVersion );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamClient_GetISteamController" )]
        internal static extern IntPtr SteamAPI_ISteamClient_GetISteamController( IntPtr instancePtr, uint hSteamUser, uint hSteamPipe, string pchVersion );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamClient_GetISteamUGC" )]
        internal static extern IntPtr SteamAPI_ISteamClient_GetISteamUGC( IntPtr instancePtr, uint hSteamUser, uint hSteamPipe, string pchVersion );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamClient_GetISteamAppList" )]
        internal static extern IntPtr SteamAPI_ISteamClient_GetISteamAppList( IntPtr instancePtr, uint hSteamUser, uint hSteamPipe, string pchVersion );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamClient_GetISteamMusic" )]
        internal static extern IntPtr SteamAPI_ISteamClient_GetISteamMusic( IntPtr instancePtr, uint hSteamuser, uint hSteamPipe, string pchVersion );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamClient_GetISteamMusicRemote" )]
        internal static extern IntPtr SteamAPI_ISteamClient_GetISteamMusicRemote( IntPtr instancePtr, uint hSteamuser, uint hSteamPipe, string pchVersion );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamClient_GetISteamHTMLSurface" )]
        internal static extern IntPtr SteamAPI_ISteamClient_GetISteamHTMLSurface( IntPtr instancePtr, uint hSteamuser, uint hSteamPipe, string pchVersion );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamClient_GetISteamInventory" )]
        internal static extern IntPtr SteamAPI_ISteamClient_GetISteamInventory( IntPtr instancePtr, uint hSteamuser, uint hSteamPipe, string pchVersion );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamClient_GetISteamVideo" )]
        internal static extern IntPtr SteamAPI_ISteamClient_GetISteamVideo( IntPtr instancePtr, uint hSteamuser, uint hSteamPipe, string pchVersion );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUser_GetHSteamUser" )]
        internal static extern uint SteamAPI_ISteamUser_GetHSteamUser( IntPtr instancePtr );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUser_BLoggedOn" )]
        internal static extern bool SteamAPI_ISteamUser_BLoggedOn( IntPtr instancePtr );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUser_GetSteamID" )]
        internal static extern ulong SteamAPI_ISteamUser_GetSteamID( IntPtr instancePtr );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUser_InitiateGameConnection" )]
        internal static extern int SteamAPI_ISteamUser_InitiateGameConnection( IntPtr instancePtr, IntPtr pAuthBlob, int cbMaxAuthBlob, ulong steamIDGameServer, uint unIPServer, char usPortServer, bool bSecure );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUser_TerminateGameConnection" )]
        internal static extern void SteamAPI_ISteamUser_TerminateGameConnection( IntPtr instancePtr, uint unIPServer, char usPortServer );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUser_TrackAppUsageEvent" )]
        internal static extern void SteamAPI_ISteamUser_TrackAppUsageEvent( IntPtr instancePtr, ulong gameID, int eAppUsageEvent, string pchExtraInfo );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUser_GetUserDataFolder" )]
        internal static extern bool SteamAPI_ISteamUser_GetUserDataFolder( IntPtr instancePtr, string pchBuffer, int cubBuffer );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUser_StartVoiceRecording" )]
        internal static extern void SteamAPI_ISteamUser_StartVoiceRecording( IntPtr instancePtr );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUser_StopVoiceRecording" )]
        internal static extern void SteamAPI_ISteamUser_StopVoiceRecording( IntPtr instancePtr );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUser_GetAvailableVoice" )]
        internal static extern uint SteamAPI_ISteamUser_GetAvailableVoice( IntPtr instancePtr, ref uint pcbCompressed, ref uint pcbUncompressed, uint nUncompressedVoiceDesiredSampleRate );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUser_GetVoice" )]
        internal static extern uint SteamAPI_ISteamUser_GetVoice( IntPtr instancePtr, bool bWantCompressed, IntPtr pDestBuffer, uint cbDestBufferSize, ref uint nBytesWritten, bool bWantUncompressed, IntPtr pUncompressedDestBuffer, uint cbUncompressedDestBufferSize, ref uint nUncompressBytesWritten, uint nUncompressedVoiceDesiredSampleRate );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUser_DecompressVoice" )]
        internal static extern uint SteamAPI_ISteamUser_DecompressVoice( IntPtr instancePtr, IntPtr pCompressed, uint cbCompressed, IntPtr pDestBuffer, uint cbDestBufferSize, ref uint nBytesWritten, uint nDesiredSampleRate );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUser_GetVoiceOptimalSampleRate" )]
        internal static extern uint SteamAPI_ISteamUser_GetVoiceOptimalSampleRate( IntPtr instancePtr );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUser_GetAuthSessionTicket" )]
        internal static extern uint SteamAPI_ISteamUser_GetAuthSessionTicket( IntPtr instancePtr, IntPtr pTicket, int cbMaxTicket, ref uint pcbTicket );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUser_BeginAuthSession" )]
        internal static extern uint SteamAPI_ISteamUser_BeginAuthSession( IntPtr instancePtr, IntPtr pAuthTicket, int cbAuthTicket, ulong steamID );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUser_EndAuthSession" )]
        internal static extern void SteamAPI_ISteamUser_EndAuthSession( IntPtr instancePtr, ulong steamID );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUser_CancelAuthTicket" )]
        internal static extern void SteamAPI_ISteamUser_CancelAuthTicket( IntPtr instancePtr, uint hAuthTicket );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUser_UserHasLicenseForApp" )]
        internal static extern uint SteamAPI_ISteamUser_UserHasLicenseForApp( IntPtr instancePtr, ulong steamID, uint appID );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUser_BIsBehindNAT" )]
        internal static extern bool SteamAPI_ISteamUser_BIsBehindNAT( IntPtr instancePtr );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUser_AdvertiseGame" )]
        internal static extern void SteamAPI_ISteamUser_AdvertiseGame( IntPtr instancePtr, ulong steamIDGameServer, uint unIPServer, char usPortServer );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUser_RequestEncryptedAppTicket" )]
        internal static extern ulong SteamAPI_ISteamUser_RequestEncryptedAppTicket( IntPtr instancePtr, IntPtr pDataToInclude, int cbDataToInclude );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUser_GetEncryptedAppTicket" )]
        internal static extern bool SteamAPI_ISteamUser_GetEncryptedAppTicket( IntPtr instancePtr, IntPtr pTicket, int cbMaxTicket, ref uint pcbTicket );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUser_GetGameBadgeLevel" )]
        internal static extern int SteamAPI_ISteamUser_GetGameBadgeLevel( IntPtr instancePtr, int nSeries, bool bFoil );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUser_GetPlayerSteamLevel" )]
        internal static extern int SteamAPI_ISteamUser_GetPlayerSteamLevel( IntPtr instancePtr );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUser_RequestStoreAuthURL" )]
        internal static extern ulong SteamAPI_ISteamUser_RequestStoreAuthURL( IntPtr instancePtr, string pchRedirectURL );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUser_BIsPhoneVerified" )]
        internal static extern bool SteamAPI_ISteamUser_BIsPhoneVerified( IntPtr instancePtr );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUser_BIsTwoFactorEnabled" )]
        internal static extern bool SteamAPI_ISteamUser_BIsTwoFactorEnabled( IntPtr instancePtr );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_GetPersonaName" )]
        internal static extern IntPtr SteamAPI_ISteamFriends_GetPersonaName( IntPtr instancePtr );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_SetPersonaName" )]
        internal static extern ulong SteamAPI_ISteamFriends_SetPersonaName( IntPtr instancePtr, string pchPersonaName );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_GetPersonaState" )]
        internal static extern uint SteamAPI_ISteamFriends_GetPersonaState( IntPtr instancePtr );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_GetFriendCount" )]
        internal static extern int SteamAPI_ISteamFriends_GetFriendCount( IntPtr instancePtr, int iFriendFlags );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_GetFriendByIndex" )]
        internal static extern ulong SteamAPI_ISteamFriends_GetFriendByIndex( IntPtr instancePtr, int iFriend, int iFriendFlags );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_GetFriendRelationship" )]
        internal static extern uint SteamAPI_ISteamFriends_GetFriendRelationship( IntPtr instancePtr, ulong steamIDFriend );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_GetFriendPersonaState" )]
        internal static extern uint SteamAPI_ISteamFriends_GetFriendPersonaState( IntPtr instancePtr, ulong steamIDFriend );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_GetFriendPersonaName" )]
        internal static extern IntPtr SteamAPI_ISteamFriends_GetFriendPersonaName( IntPtr instancePtr, ulong steamIDFriend );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_GetFriendGamePlayed" )]
        internal static extern bool SteamAPI_ISteamFriends_GetFriendGamePlayed( IntPtr instancePtr, ulong steamIDFriend, ref FriendGameInfo_t pFriendGameInfo );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_GetFriendPersonaNameHistory" )]
        internal static extern IntPtr SteamAPI_ISteamFriends_GetFriendPersonaNameHistory( IntPtr instancePtr, ulong steamIDFriend, int iPersonaName );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_GetFriendSteamLevel" )]
        internal static extern int SteamAPI_ISteamFriends_GetFriendSteamLevel( IntPtr instancePtr, ulong steamIDFriend );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_GetPlayerNickname" )]
        internal static extern IntPtr SteamAPI_ISteamFriends_GetPlayerNickname( IntPtr instancePtr, ulong steamIDPlayer );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_GetFriendsGroupCount" )]
        internal static extern int SteamAPI_ISteamFriends_GetFriendsGroupCount( IntPtr instancePtr );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_GetFriendsGroupIDByIndex" )]
        internal static extern char SteamAPI_ISteamFriends_GetFriendsGroupIDByIndex( IntPtr instancePtr, int iFG );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_GetFriendsGroupName" )]
        internal static extern IntPtr SteamAPI_ISteamFriends_GetFriendsGroupName( IntPtr instancePtr, char friendsGroupID );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_GetFriendsGroupMembersCount" )]
        internal static extern int SteamAPI_ISteamFriends_GetFriendsGroupMembersCount( IntPtr instancePtr, char friendsGroupID );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_GetFriendsGroupMembersList" )]
        internal static extern void SteamAPI_ISteamFriends_GetFriendsGroupMembersList( IntPtr instancePtr, char friendsGroupID, [In, Out] CSteamID[] pOutSteamIDMembers, int nMembersCount );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_HasFriend" )]
        internal static extern bool SteamAPI_ISteamFriends_HasFriend( IntPtr instancePtr, ulong steamIDFriend, int iFriendFlags );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_GetClanCount" )]
        internal static extern int SteamAPI_ISteamFriends_GetClanCount( IntPtr instancePtr );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_GetClanByIndex" )]
        internal static extern ulong SteamAPI_ISteamFriends_GetClanByIndex( IntPtr instancePtr, int iClan );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_GetClanName" )]
        internal static extern IntPtr SteamAPI_ISteamFriends_GetClanName( IntPtr instancePtr, ulong steamIDClan );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_GetClanTag" )]
        internal static extern IntPtr SteamAPI_ISteamFriends_GetClanTag( IntPtr instancePtr, ulong steamIDClan );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_GetClanActivityCounts" )]
        internal static extern bool SteamAPI_ISteamFriends_GetClanActivityCounts( IntPtr instancePtr, ulong steamIDClan, ref int pnOnline, ref int pnInGame, ref int pnChatting );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_DownloadClanActivityCounts" )]
        internal static extern ulong SteamAPI_ISteamFriends_DownloadClanActivityCounts( IntPtr instancePtr, [In, Out] CSteamID[] psteamIDClans, int cClansToRequest );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_GetFriendCountFromSource" )]
        internal static extern int SteamAPI_ISteamFriends_GetFriendCountFromSource( IntPtr instancePtr, ulong steamIDSource );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_GetFriendFromSourceByIndex" )]
        internal static extern ulong SteamAPI_ISteamFriends_GetFriendFromSourceByIndex( IntPtr instancePtr, ulong steamIDSource, int iFriend );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_IsUserInSource" )]
        internal static extern bool SteamAPI_ISteamFriends_IsUserInSource( IntPtr instancePtr, ulong steamIDUser, ulong steamIDSource );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_SetInGameVoiceSpeaking" )]
        internal static extern void SteamAPI_ISteamFriends_SetInGameVoiceSpeaking( IntPtr instancePtr, ulong steamIDUser, bool bSpeaking );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_ActivateGameOverlay" )]
        internal static extern void SteamAPI_ISteamFriends_ActivateGameOverlay( IntPtr instancePtr, string pchDialog );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_ActivateGameOverlayToUser" )]
        internal static extern void SteamAPI_ISteamFriends_ActivateGameOverlayToUser( IntPtr instancePtr, string pchDialog, ulong steamID );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_ActivateGameOverlayToWebPage" )]
        internal static extern void SteamAPI_ISteamFriends_ActivateGameOverlayToWebPage( IntPtr instancePtr, string pchURL );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_ActivateGameOverlayToStore" )]
        internal static extern void SteamAPI_ISteamFriends_ActivateGameOverlayToStore( IntPtr instancePtr, uint nAppID, char eFlag );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_SetPlayedWith" )]
        internal static extern void SteamAPI_ISteamFriends_SetPlayedWith( IntPtr instancePtr, ulong steamIDUserPlayedWith );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_ActivateGameOverlayInviteDialog" )]
        internal static extern void SteamAPI_ISteamFriends_ActivateGameOverlayInviteDialog( IntPtr instancePtr, ulong steamIDLobby );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_GetSmallFriendAvatar" )]
        internal static extern int SteamAPI_ISteamFriends_GetSmallFriendAvatar( IntPtr instancePtr, ulong steamIDFriend );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_GetMediumFriendAvatar" )]
        internal static extern int SteamAPI_ISteamFriends_GetMediumFriendAvatar( IntPtr instancePtr, ulong steamIDFriend );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_GetLargeFriendAvatar" )]
        internal static extern int SteamAPI_ISteamFriends_GetLargeFriendAvatar( IntPtr instancePtr, ulong steamIDFriend );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_RequestUserInformation" )]
        internal static extern bool SteamAPI_ISteamFriends_RequestUserInformation( IntPtr instancePtr, ulong steamIDUser, bool bRequireNameOnly );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_RequestClanOfficerList" )]
        internal static extern ulong SteamAPI_ISteamFriends_RequestClanOfficerList( IntPtr instancePtr, ulong steamIDClan );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_GetClanOwner" )]
        internal static extern ulong SteamAPI_ISteamFriends_GetClanOwner( IntPtr instancePtr, ulong steamIDClan );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_GetClanOfficerCount" )]
        internal static extern int SteamAPI_ISteamFriends_GetClanOfficerCount( IntPtr instancePtr, ulong steamIDClan );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_GetClanOfficerByIndex" )]
        internal static extern ulong SteamAPI_ISteamFriends_GetClanOfficerByIndex( IntPtr instancePtr, ulong steamIDClan, int iOfficer );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_GetUserRestrictions" )]
        internal static extern uint SteamAPI_ISteamFriends_GetUserRestrictions( IntPtr instancePtr );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_SetRichPresence" )]
        internal static extern bool SteamAPI_ISteamFriends_SetRichPresence( IntPtr instancePtr, string pchKey, string pchValue );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_ClearRichPresence" )]
        internal static extern void SteamAPI_ISteamFriends_ClearRichPresence( IntPtr instancePtr );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_GetFriendRichPresence" )]
        internal static extern IntPtr SteamAPI_ISteamFriends_GetFriendRichPresence( IntPtr instancePtr, ulong steamIDFriend, string pchKey );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_GetFriendRichPresenceKeyCount" )]
        internal static extern int SteamAPI_ISteamFriends_GetFriendRichPresenceKeyCount( IntPtr instancePtr, ulong steamIDFriend );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_GetFriendRichPresenceKeyByIndex" )]
        internal static extern IntPtr SteamAPI_ISteamFriends_GetFriendRichPresenceKeyByIndex( IntPtr instancePtr, ulong steamIDFriend, int iKey );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_RequestFriendRichPresence" )]
        internal static extern void SteamAPI_ISteamFriends_RequestFriendRichPresence( IntPtr instancePtr, ulong steamIDFriend );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_InviteUserToGame" )]
        internal static extern bool SteamAPI_ISteamFriends_InviteUserToGame( IntPtr instancePtr, ulong steamIDFriend, string pchConnectString );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_GetCoplayFriendCount" )]
        internal static extern int SteamAPI_ISteamFriends_GetCoplayFriendCount( IntPtr instancePtr );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_GetCoplayFriend" )]
        internal static extern ulong SteamAPI_ISteamFriends_GetCoplayFriend( IntPtr instancePtr, int iCoplayFriend );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_GetFriendCoplayTime" )]
        internal static extern int SteamAPI_ISteamFriends_GetFriendCoplayTime( IntPtr instancePtr, ulong steamIDFriend );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_GetFriendCoplayGame" )]
        internal static extern uint SteamAPI_ISteamFriends_GetFriendCoplayGame( IntPtr instancePtr, ulong steamIDFriend );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_JoinClanChatRoom" )]
        internal static extern ulong SteamAPI_ISteamFriends_JoinClanChatRoom( IntPtr instancePtr, ulong steamIDClan );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_LeaveClanChatRoom" )]
        internal static extern bool SteamAPI_ISteamFriends_LeaveClanChatRoom( IntPtr instancePtr, ulong steamIDClan );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_GetClanChatMemberCount" )]
        internal static extern int SteamAPI_ISteamFriends_GetClanChatMemberCount( IntPtr instancePtr, ulong steamIDClan );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_GetChatMemberByIndex" )]
        internal static extern ulong SteamAPI_ISteamFriends_GetChatMemberByIndex( IntPtr instancePtr, ulong steamIDClan, int iUser );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_SendClanChatMessage" )]
        internal static extern bool SteamAPI_ISteamFriends_SendClanChatMessage( IntPtr instancePtr, ulong steamIDClanChat, string pchText );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_GetClanChatMessage" )]
        internal static extern int SteamAPI_ISteamFriends_GetClanChatMessage( IntPtr instancePtr, ulong steamIDClanChat, int iMessage, IntPtr prgchText, int cchTextMax, ref uint peChatEntryType, ref CSteamID psteamidChatter );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_IsClanChatAdmin" )]
        internal static extern bool SteamAPI_ISteamFriends_IsClanChatAdmin( IntPtr instancePtr, ulong steamIDClanChat, ulong steamIDUser );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_IsClanChatWindowOpenInSteam" )]
        internal static extern bool SteamAPI_ISteamFriends_IsClanChatWindowOpenInSteam( IntPtr instancePtr, ulong steamIDClanChat );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_OpenClanChatWindowInSteam" )]
        internal static extern bool SteamAPI_ISteamFriends_OpenClanChatWindowInSteam( IntPtr instancePtr, ulong steamIDClanChat );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_CloseClanChatWindowInSteam" )]
        internal static extern bool SteamAPI_ISteamFriends_CloseClanChatWindowInSteam( IntPtr instancePtr, ulong steamIDClanChat );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_SetListenForFriendsMessages" )]
        internal static extern bool SteamAPI_ISteamFriends_SetListenForFriendsMessages( IntPtr instancePtr, bool bInterceptEnabled );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_ReplyToFriendMessage" )]
        internal static extern bool SteamAPI_ISteamFriends_ReplyToFriendMessage( IntPtr instancePtr, ulong steamIDFriend, string pchMsgToSend );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_GetFriendMessage" )]
        internal static extern int SteamAPI_ISteamFriends_GetFriendMessage( IntPtr instancePtr, ulong steamIDFriend, int iMessageID, IntPtr pvData, int cubData, ref uint peChatEntryType );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_GetFollowerCount" )]
        internal static extern ulong SteamAPI_ISteamFriends_GetFollowerCount( IntPtr instancePtr, ulong steamID );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_IsFollowing" )]
        internal static extern ulong SteamAPI_ISteamFriends_IsFollowing( IntPtr instancePtr, ulong steamID );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamFriends_EnumerateFollowingList" )]
        internal static extern ulong SteamAPI_ISteamFriends_EnumerateFollowingList( IntPtr instancePtr, uint unStartIndex );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUtils_GetSecondsSinceAppActive" )]
        internal static extern uint SteamAPI_ISteamUtils_GetSecondsSinceAppActive( IntPtr instancePtr );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUtils_GetSecondsSinceComputerActive" )]
        internal static extern uint SteamAPI_ISteamUtils_GetSecondsSinceComputerActive( IntPtr instancePtr );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUtils_GetConnectedUniverse" )]
        internal static extern int SteamAPI_ISteamUtils_GetConnectedUniverse( IntPtr instancePtr );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUtils_GetServerRealTime" )]
        internal static extern uint SteamAPI_ISteamUtils_GetServerRealTime( IntPtr instancePtr );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUtils_GetIPCountry" )]
        internal static extern IntPtr SteamAPI_ISteamUtils_GetIPCountry( IntPtr instancePtr );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUtils_GetImageSize" )]
        internal static extern bool SteamAPI_ISteamUtils_GetImageSize( IntPtr instancePtr, int iImage, ref uint pnWidth, ref uint pnHeight );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUtils_GetImageRGBA" )]
        internal static extern bool SteamAPI_ISteamUtils_GetImageRGBA( IntPtr instancePtr, int iImage, IntPtr pubDest, int nDestBufferSize );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUtils_GetCSERIPPort" )]
        internal static extern bool SteamAPI_ISteamUtils_GetCSERIPPort( IntPtr instancePtr, ref uint unIP, ref char usPort );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUtils_GetCurrentBatteryPower" )]
        internal static extern byte SteamAPI_ISteamUtils_GetCurrentBatteryPower( IntPtr instancePtr );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUtils_GetAppID" )]
        internal static extern uint SteamAPI_ISteamUtils_GetAppID( IntPtr instancePtr );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUtils_SetOverlayNotificationPosition" )]
        internal static extern void SteamAPI_ISteamUtils_SetOverlayNotificationPosition( IntPtr instancePtr, uint eNotificationPosition );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUtils_IsAPICallCompleted" )]
        internal static extern bool SteamAPI_ISteamUtils_IsAPICallCompleted( IntPtr instancePtr, ulong hSteamAPICall, ref bool pbFailed );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUtils_GetAPICallFailureReason" )]
        internal static extern int SteamAPI_ISteamUtils_GetAPICallFailureReason( IntPtr instancePtr, ulong hSteamAPICall );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUtils_GetAPICallResult" )]
        internal static extern bool SteamAPI_ISteamUtils_GetAPICallResult( IntPtr instancePtr, ulong hSteamAPICall, IntPtr pCallback, int cubCallback, int iCallbackExpected, ref bool pbFailed );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUtils_GetIPCCallCount" )]
        internal static extern uint SteamAPI_ISteamUtils_GetIPCCallCount( IntPtr instancePtr );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUtils_SetWarningMessageHook" )]
        internal static extern void SteamAPI_ISteamUtils_SetWarningMessageHook( IntPtr instancePtr, IntPtr pFunction );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUtils_IsOverlayEnabled" )]
        internal static extern bool SteamAPI_ISteamUtils_IsOverlayEnabled( IntPtr instancePtr );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUtils_BOverlayNeedsPresent" )]
        internal static extern bool SteamAPI_ISteamUtils_BOverlayNeedsPresent( IntPtr instancePtr );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUtils_CheckFileSignature" )]
        internal static extern ulong SteamAPI_ISteamUtils_CheckFileSignature( IntPtr instancePtr, string szFileName );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUtils_ShowGamepadTextInput" )]
        internal static extern bool SteamAPI_ISteamUtils_ShowGamepadTextInput( IntPtr instancePtr, int eInputMode, int eLineInputMode, string pchDescription, uint unCharMax, string pchExistingText );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUtils_GetEnteredGamepadTextLength" )]
        internal static extern uint SteamAPI_ISteamUtils_GetEnteredGamepadTextLength( IntPtr instancePtr );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUtils_GetEnteredGamepadTextInput" )]
        internal static extern bool SteamAPI_ISteamUtils_GetEnteredGamepadTextInput( IntPtr instancePtr, string pchText, uint cchText );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUtils_GetSteamUILanguage" )]
        internal static extern IntPtr SteamAPI_ISteamUtils_GetSteamUILanguage( IntPtr instancePtr );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUtils_IsSteamRunningInVR" )]
        internal static extern bool SteamAPI_ISteamUtils_IsSteamRunningInVR( IntPtr instancePtr );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUtils_SetOverlayNotificationInset" )]
        internal static extern void SteamAPI_ISteamUtils_SetOverlayNotificationInset( IntPtr instancePtr, int nHorizontalInset, int nVerticalInset );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUtils_IsSteamInBigPictureMode" )]
        internal static extern bool SteamAPI_ISteamUtils_IsSteamInBigPictureMode( IntPtr instancePtr );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUtils_StartVRDashboard" )]
        internal static extern void SteamAPI_ISteamUtils_StartVRDashboard( IntPtr instancePtr );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMatchmaking_GetFavoriteGameCount" )]
        internal static extern int SteamAPI_ISteamMatchmaking_GetFavoriteGameCount( IntPtr instancePtr );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMatchmaking_GetFavoriteGame" )]
        internal static extern bool SteamAPI_ISteamMatchmaking_GetFavoriteGame( IntPtr instancePtr, int iGame, ref uint pnAppID, ref uint pnIP, ref char pnConnPort, ref char pnQueryPort, ref uint punFlags, ref uint pRTime32LastPlayedOnServer );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMatchmaking_AddFavoriteGame" )]
        internal static extern int SteamAPI_ISteamMatchmaking_AddFavoriteGame( IntPtr instancePtr, uint nAppID, uint nIP, char nConnPort, char nQueryPort, uint unFlags, uint rTime32LastPlayedOnServer );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMatchmaking_RemoveFavoriteGame" )]
        internal static extern bool SteamAPI_ISteamMatchmaking_RemoveFavoriteGame( IntPtr instancePtr, uint nAppID, uint nIP, char nConnPort, char nQueryPort, uint unFlags );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMatchmaking_RequestLobbyList" )]
        internal static extern ulong SteamAPI_ISteamMatchmaking_RequestLobbyList( IntPtr instancePtr );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMatchmaking_AddRequestLobbyListStringFilter" )]
        internal static extern void SteamAPI_ISteamMatchmaking_AddRequestLobbyListStringFilter( IntPtr instancePtr, string pchKeyToMatch, string pchValueToMatch, uint eComparisonType );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMatchmaking_AddRequestLobbyListNumericalFilter" )]
        internal static extern void SteamAPI_ISteamMatchmaking_AddRequestLobbyListNumericalFilter( IntPtr instancePtr, string pchKeyToMatch, int nValueToMatch, uint eComparisonType );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMatchmaking_AddRequestLobbyListNearValueFilter" )]
        internal static extern void SteamAPI_ISteamMatchmaking_AddRequestLobbyListNearValueFilter( IntPtr instancePtr, string pchKeyToMatch, int nValueToBeCloseTo );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMatchmaking_AddRequestLobbyListFilterSlotsAvailable" )]
        internal static extern void SteamAPI_ISteamMatchmaking_AddRequestLobbyListFilterSlotsAvailable( IntPtr instancePtr, int nSlotsAvailable );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMatchmaking_AddRequestLobbyListDistanceFilter" )]
        internal static extern void SteamAPI_ISteamMatchmaking_AddRequestLobbyListDistanceFilter( IntPtr instancePtr, uint eLobbyDistanceFilter );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMatchmaking_AddRequestLobbyListResultCountFilter" )]
        internal static extern void SteamAPI_ISteamMatchmaking_AddRequestLobbyListResultCountFilter( IntPtr instancePtr, int cMaxResults );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMatchmaking_AddRequestLobbyListCompatibleMembersFilter" )]
        internal static extern void SteamAPI_ISteamMatchmaking_AddRequestLobbyListCompatibleMembersFilter( IntPtr instancePtr, ulong steamIDLobby );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMatchmaking_GetLobbyByIndex" )]
        internal static extern ulong SteamAPI_ISteamMatchmaking_GetLobbyByIndex( IntPtr instancePtr, int iLobby );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMatchmaking_CreateLobby" )]
        internal static extern ulong SteamAPI_ISteamMatchmaking_CreateLobby( IntPtr instancePtr, uint eLobbyType, int cMaxMembers );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMatchmaking_JoinLobby" )]
        internal static extern ulong SteamAPI_ISteamMatchmaking_JoinLobby( IntPtr instancePtr, ulong steamIDLobby );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMatchmaking_LeaveLobby" )]
        internal static extern void SteamAPI_ISteamMatchmaking_LeaveLobby( IntPtr instancePtr, ulong steamIDLobby );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMatchmaking_InviteUserToLobby" )]
        internal static extern bool SteamAPI_ISteamMatchmaking_InviteUserToLobby( IntPtr instancePtr, ulong steamIDLobby, ulong steamIDInvitee );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMatchmaking_GetNumLobbyMembers" )]
        internal static extern int SteamAPI_ISteamMatchmaking_GetNumLobbyMembers( IntPtr instancePtr, ulong steamIDLobby );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMatchmaking_GetLobbyMemberByIndex" )]
        internal static extern ulong SteamAPI_ISteamMatchmaking_GetLobbyMemberByIndex( IntPtr instancePtr, ulong steamIDLobby, int iMember );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMatchmaking_GetLobbyData" )]
        internal static extern IntPtr SteamAPI_ISteamMatchmaking_GetLobbyData( IntPtr instancePtr, ulong steamIDLobby, string pchKey );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMatchmaking_SetLobbyData" )]
        internal static extern bool SteamAPI_ISteamMatchmaking_SetLobbyData( IntPtr instancePtr, ulong steamIDLobby, string pchKey, string pchValue );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMatchmaking_GetLobbyDataCount" )]
        internal static extern int SteamAPI_ISteamMatchmaking_GetLobbyDataCount( IntPtr instancePtr, ulong steamIDLobby );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMatchmaking_GetLobbyDataByIndex" )]
        internal static extern bool SteamAPI_ISteamMatchmaking_GetLobbyDataByIndex( IntPtr instancePtr, ulong steamIDLobby, int iLobbyData, string pchKey, int cchKeyBufferSize, string pchValue, int cchValueBufferSize );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMatchmaking_DeleteLobbyData" )]
        internal static extern bool SteamAPI_ISteamMatchmaking_DeleteLobbyData( IntPtr instancePtr, ulong steamIDLobby, string pchKey );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMatchmaking_GetLobbyMemberData" )]
        internal static extern IntPtr SteamAPI_ISteamMatchmaking_GetLobbyMemberData( IntPtr instancePtr, ulong steamIDLobby, ulong steamIDUser, string pchKey );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMatchmaking_SetLobbyMemberData" )]
        internal static extern void SteamAPI_ISteamMatchmaking_SetLobbyMemberData( IntPtr instancePtr, ulong steamIDLobby, string pchKey, string pchValue );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMatchmaking_SendLobbyChatMsg" )]
        internal static extern bool SteamAPI_ISteamMatchmaking_SendLobbyChatMsg( IntPtr instancePtr, ulong steamIDLobby, IntPtr pvMsgBody, int cubMsgBody );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMatchmaking_GetLobbyChatEntry" )]
        internal static extern int SteamAPI_ISteamMatchmaking_GetLobbyChatEntry( IntPtr instancePtr, ulong steamIDLobby, int iChatID, ref CSteamID pSteamIDUser, IntPtr pvData, int cubData, ref uint peChatEntryType );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMatchmaking_RequestLobbyData" )]
        internal static extern bool SteamAPI_ISteamMatchmaking_RequestLobbyData( IntPtr instancePtr, ulong steamIDLobby );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMatchmaking_SetLobbyGameServer" )]
        internal static extern void SteamAPI_ISteamMatchmaking_SetLobbyGameServer( IntPtr instancePtr, ulong steamIDLobby, uint unGameServerIP, char unGameServerPort, ulong steamIDGameServer );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMatchmaking_GetLobbyGameServer" )]
        internal static extern bool SteamAPI_ISteamMatchmaking_GetLobbyGameServer( IntPtr instancePtr, ulong steamIDLobby, ref uint punGameServerIP, ref char punGameServerPort, ref CSteamID psteamIDGameServer );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMatchmaking_SetLobbyMemberLimit" )]
        internal static extern bool SteamAPI_ISteamMatchmaking_SetLobbyMemberLimit( IntPtr instancePtr, ulong steamIDLobby, int cMaxMembers );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMatchmaking_GetLobbyMemberLimit" )]
        internal static extern int SteamAPI_ISteamMatchmaking_GetLobbyMemberLimit( IntPtr instancePtr, ulong steamIDLobby );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMatchmaking_SetLobbyType" )]
        internal static extern bool SteamAPI_ISteamMatchmaking_SetLobbyType( IntPtr instancePtr, ulong steamIDLobby, uint eLobbyType );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMatchmaking_SetLobbyJoinable" )]
        internal static extern bool SteamAPI_ISteamMatchmaking_SetLobbyJoinable( IntPtr instancePtr, ulong steamIDLobby, bool bLobbyJoinable );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMatchmaking_GetLobbyOwner" )]
        internal static extern ulong SteamAPI_ISteamMatchmaking_GetLobbyOwner( IntPtr instancePtr, ulong steamIDLobby );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMatchmaking_SetLobbyOwner" )]
        internal static extern bool SteamAPI_ISteamMatchmaking_SetLobbyOwner( IntPtr instancePtr, ulong steamIDLobby, ulong steamIDNewOwner );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMatchmaking_SetLinkedLobby" )]
        internal static extern bool SteamAPI_ISteamMatchmaking_SetLinkedLobby( IntPtr instancePtr, ulong steamIDLobby, ulong steamIDLobbyDependent );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMatchmakingServerListResponse_ServerResponded" )]
        internal static extern void SteamAPI_ISteamMatchmakingServerListResponse_ServerResponded( IntPtr instancePtr, uint hRequest, int iServer );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMatchmakingServerListResponse_ServerFailedToRespond" )]
        internal static extern void SteamAPI_ISteamMatchmakingServerListResponse_ServerFailedToRespond( IntPtr instancePtr, uint hRequest, int iServer );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMatchmakingServerListResponse_RefreshComplete" )]
        internal static extern void SteamAPI_ISteamMatchmakingServerListResponse_RefreshComplete( IntPtr instancePtr, uint hRequest, uint response );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMatchmakingPingResponse_ServerResponded" )]
        internal static extern void SteamAPI_ISteamMatchmakingPingResponse_ServerResponded( IntPtr instancePtr, IntPtr server );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMatchmakingPingResponse_ServerFailedToRespond" )]
        internal static extern void SteamAPI_ISteamMatchmakingPingResponse_ServerFailedToRespond( IntPtr instancePtr );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMatchmakingPlayersResponse_AddPlayerToList" )]
        internal static extern void SteamAPI_ISteamMatchmakingPlayersResponse_AddPlayerToList( IntPtr instancePtr, string pchName, int nScore, float flTimePlayed );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMatchmakingPlayersResponse_PlayersFailedToRespond" )]
        internal static extern void SteamAPI_ISteamMatchmakingPlayersResponse_PlayersFailedToRespond( IntPtr instancePtr );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMatchmakingPlayersResponse_PlayersRefreshComplete" )]
        internal static extern void SteamAPI_ISteamMatchmakingPlayersResponse_PlayersRefreshComplete( IntPtr instancePtr );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMatchmakingRulesResponse_RulesResponded" )]
        internal static extern void SteamAPI_ISteamMatchmakingRulesResponse_RulesResponded( IntPtr instancePtr, string pchRule, string pchValue );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMatchmakingRulesResponse_RulesFailedToRespond" )]
        internal static extern void SteamAPI_ISteamMatchmakingRulesResponse_RulesFailedToRespond( IntPtr instancePtr );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMatchmakingRulesResponse_RulesRefreshComplete" )]
        internal static extern void SteamAPI_ISteamMatchmakingRulesResponse_RulesRefreshComplete( IntPtr instancePtr );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMatchmakingServers_RequestInternetServerList" )]
        internal static extern IntPtr SteamAPI_ISteamMatchmakingServers_RequestInternetServerList( IntPtr instancePtr, uint iApp, [In, Out] IntPtr ppchFilters, uint nFilters, IntPtr pRequestServersResponse );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMatchmakingServers_RequestLANServerList" )]
        internal static extern IntPtr SteamAPI_ISteamMatchmakingServers_RequestLANServerList( IntPtr instancePtr, uint iApp, IntPtr pRequestServersResponse );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMatchmakingServers_RequestFriendsServerList" )]
        internal static extern IntPtr SteamAPI_ISteamMatchmakingServers_RequestFriendsServerList( IntPtr instancePtr, uint iApp, [In, Out] IntPtr[] ppchFilters, uint nFilters, IntPtr pRequestServersResponse );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMatchmakingServers_RequestFavoritesServerList" )]
        internal static extern IntPtr SteamAPI_ISteamMatchmakingServers_RequestFavoritesServerList( IntPtr instancePtr, uint iApp, [In, Out] IntPtr[] ppchFilters, uint nFilters, IntPtr pRequestServersResponse );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMatchmakingServers_RequestHistoryServerList" )]
        internal static extern IntPtr SteamAPI_ISteamMatchmakingServers_RequestHistoryServerList( IntPtr instancePtr, uint iApp, [In, Out] IntPtr[] ppchFilters, uint nFilters, IntPtr pRequestServersResponse );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMatchmakingServers_RequestSpectatorServerList" )]
        internal static extern IntPtr SteamAPI_ISteamMatchmakingServers_RequestSpectatorServerList( IntPtr instancePtr, uint iApp, [In, Out] IntPtr[] ppchFilters, uint nFilters, IntPtr pRequestServersResponse );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMatchmakingServers_ReleaseRequest" )]
        internal static extern void SteamAPI_ISteamMatchmakingServers_ReleaseRequest( IntPtr instancePtr, IntPtr hServerListRequest );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMatchmakingServers_GetServerDetails" )]
        internal static extern IntPtr SteamAPI_ISteamMatchmakingServers_GetServerDetails( IntPtr instancePtr, IntPtr hRequest, int iServer );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMatchmakingServers_CancelQuery" )]
        internal static extern void SteamAPI_ISteamMatchmakingServers_CancelQuery( IntPtr instancePtr, IntPtr hRequest );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMatchmakingServers_RefreshQuery" )]
        internal static extern void SteamAPI_ISteamMatchmakingServers_RefreshQuery( IntPtr instancePtr, IntPtr hRequest );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMatchmakingServers_IsRefreshing" )]
        internal static extern bool SteamAPI_ISteamMatchmakingServers_IsRefreshing( IntPtr instancePtr, IntPtr hRequest );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMatchmakingServers_GetServerCount" )]
        internal static extern int SteamAPI_ISteamMatchmakingServers_GetServerCount( IntPtr instancePtr, IntPtr hRequest );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMatchmakingServers_RefreshServer" )]
        internal static extern void SteamAPI_ISteamMatchmakingServers_RefreshServer( IntPtr instancePtr, IntPtr hRequest, int iServer );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMatchmakingServers_PingServer" )]
        internal static extern uint SteamAPI_ISteamMatchmakingServers_PingServer( IntPtr instancePtr, uint unIP, char usPort, IntPtr pRequestServersResponse );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMatchmakingServers_PlayerDetails" )]
        internal static extern uint SteamAPI_ISteamMatchmakingServers_PlayerDetails( IntPtr instancePtr, uint unIP, char usPort, IntPtr pRequestServersResponse );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMatchmakingServers_ServerRules" )]
        internal static extern uint SteamAPI_ISteamMatchmakingServers_ServerRules( IntPtr instancePtr, uint unIP, short usPort, IntPtr pRequestServersResponse );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMatchmakingServers_CancelServerQuery" )]
        internal static extern void SteamAPI_ISteamMatchmakingServers_CancelServerQuery( IntPtr instancePtr, uint hServerQuery );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamRemoteStorage_FileWrite" )]
        internal static extern bool SteamAPI_ISteamRemoteStorage_FileWrite( IntPtr instancePtr, string pchFile, IntPtr pvData, int cubData );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamRemoteStorage_FileRead" )]
        internal static extern int SteamAPI_ISteamRemoteStorage_FileRead( IntPtr instancePtr, string pchFile, IntPtr pvData, int cubDataToRead );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamRemoteStorage_FileWriteAsync" )]
        internal static extern ulong SteamAPI_ISteamRemoteStorage_FileWriteAsync( IntPtr instancePtr, string pchFile, IntPtr pvData, uint cubData );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamRemoteStorage_FileReadAsync" )]
        internal static extern ulong SteamAPI_ISteamRemoteStorage_FileReadAsync( IntPtr instancePtr, string pchFile, uint nOffset, uint cubToRead );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamRemoteStorage_FileReadAsyncComplete" )]
        internal static extern bool SteamAPI_ISteamRemoteStorage_FileReadAsyncComplete( IntPtr instancePtr, ulong hReadCall, IntPtr pvBuffer, uint cubToRead );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamRemoteStorage_FileForget" )]
        internal static extern bool SteamAPI_ISteamRemoteStorage_FileForget( IntPtr instancePtr, string pchFile );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamRemoteStorage_FileDelete" )]
        internal static extern bool SteamAPI_ISteamRemoteStorage_FileDelete( IntPtr instancePtr, string pchFile );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamRemoteStorage_FileShare" )]
        internal static extern ulong SteamAPI_ISteamRemoteStorage_FileShare( IntPtr instancePtr, string pchFile );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamRemoteStorage_SetSyncPlatforms" )]
        internal static extern bool SteamAPI_ISteamRemoteStorage_SetSyncPlatforms( IntPtr instancePtr, string pchFile, uint eRemoteStoragePlatform );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamRemoteStorage_FileWriteStreamOpen" )]
        internal static extern ulong SteamAPI_ISteamRemoteStorage_FileWriteStreamOpen( IntPtr instancePtr, string pchFile );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamRemoteStorage_FileWriteStreamWriteChunk" )]
        internal static extern bool SteamAPI_ISteamRemoteStorage_FileWriteStreamWriteChunk( IntPtr instancePtr, ulong writeHandle, IntPtr pvData, int cubData );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamRemoteStorage_FileWriteStreamClose" )]
        internal static extern bool SteamAPI_ISteamRemoteStorage_FileWriteStreamClose( IntPtr instancePtr, ulong writeHandle );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamRemoteStorage_FileWriteStreamCancel" )]
        internal static extern bool SteamAPI_ISteamRemoteStorage_FileWriteStreamCancel( IntPtr instancePtr, ulong writeHandle );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamRemoteStorage_FileExists" )]
        internal static extern bool SteamAPI_ISteamRemoteStorage_FileExists( IntPtr instancePtr, string pchFile );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamRemoteStorage_FilePersisted" )]
        internal static extern bool SteamAPI_ISteamRemoteStorage_FilePersisted( IntPtr instancePtr, string pchFile );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamRemoteStorage_GetFileSize" )]
        internal static extern int SteamAPI_ISteamRemoteStorage_GetFileSize( IntPtr instancePtr, string pchFile );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamRemoteStorage_GetFileTimestamp" )]
        internal static extern long SteamAPI_ISteamRemoteStorage_GetFileTimestamp( IntPtr instancePtr, string pchFile );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamRemoteStorage_GetSyncPlatforms" )]
        internal static extern uint SteamAPI_ISteamRemoteStorage_GetSyncPlatforms( IntPtr instancePtr, string pchFile );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamRemoteStorage_GetFileCount" )]
        internal static extern int SteamAPI_ISteamRemoteStorage_GetFileCount( IntPtr instancePtr );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamRemoteStorage_GetFileNameAndSize" )]
        internal static extern IntPtr SteamAPI_ISteamRemoteStorage_GetFileNameAndSize( IntPtr instancePtr, int iFile, ref int pnFileSizeInBytes );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamRemoteStorage_GetQuota" )]
        internal static extern bool SteamAPI_ISteamRemoteStorage_GetQuota( IntPtr instancePtr, ref int pnTotalBytes, ref int puAvailableBytes );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamRemoteStorage_IsCloudEnabledForAccount" )]
        internal static extern bool SteamAPI_ISteamRemoteStorage_IsCloudEnabledForAccount( IntPtr instancePtr );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamRemoteStorage_IsCloudEnabledForApp" )]
        internal static extern bool SteamAPI_ISteamRemoteStorage_IsCloudEnabledForApp( IntPtr instancePtr );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamRemoteStorage_SetCloudEnabledForApp" )]
        internal static extern void SteamAPI_ISteamRemoteStorage_SetCloudEnabledForApp( IntPtr instancePtr, bool bEnabled );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamRemoteStorage_UGCDownload" )]
        internal static extern ulong SteamAPI_ISteamRemoteStorage_UGCDownload( IntPtr instancePtr, ulong hContent, uint unPriority );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamRemoteStorage_GetUGCDownloadProgress" )]
        internal static extern bool SteamAPI_ISteamRemoteStorage_GetUGCDownloadProgress( IntPtr instancePtr, ulong hContent, ref int pnBytesDownloaded, ref int pnBytesExpected );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamRemoteStorage_GetUGCDetails" )]
        internal static extern bool SteamAPI_ISteamRemoteStorage_GetUGCDetails( IntPtr instancePtr, ulong hContent, ref uint pnAppID, System.Text.StringBuilder ppchName, ref int pnFileSizeInBytes, ref CSteamID pSteamIDOwner );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamRemoteStorage_UGCRead" )]
        internal static extern int SteamAPI_ISteamRemoteStorage_UGCRead( IntPtr instancePtr, ulong hContent, IntPtr pvData, int cubDataToRead, uint cOffset, uint eAction );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamRemoteStorage_GetCachedUGCCount" )]
        internal static extern int SteamAPI_ISteamRemoteStorage_GetCachedUGCCount( IntPtr instancePtr );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamRemoteStorage_GetCachedUGCHandle" )]
        internal static extern ulong SteamAPI_ISteamRemoteStorage_GetCachedUGCHandle( IntPtr instancePtr, int iCachedContent );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamRemoteStorage_PublishWorkshopFile" )]
        internal static extern ulong SteamAPI_ISteamRemoteStorage_PublishWorkshopFile( IntPtr instancePtr, string pchFile, string pchPreviewFile, uint nConsumerAppId, string pchTitle, string pchDescription, uint eVisibility, ref SteamParamStringArray_t pTags, uint eWorkshopFileType );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamRemoteStorage_CreatePublishedFileUpdateRequest" )]
        internal static extern ulong SteamAPI_ISteamRemoteStorage_CreatePublishedFileUpdateRequest( IntPtr instancePtr, ulong unPublishedFileId );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamRemoteStorage_UpdatePublishedFileFile" )]
        internal static extern bool SteamAPI_ISteamRemoteStorage_UpdatePublishedFileFile( IntPtr instancePtr, ulong updateHandle, string pchFile );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamRemoteStorage_UpdatePublishedFilePreviewFile" )]
        internal static extern bool SteamAPI_ISteamRemoteStorage_UpdatePublishedFilePreviewFile( IntPtr instancePtr, ulong updateHandle, string pchPreviewFile );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamRemoteStorage_UpdatePublishedFileTitle" )]
        internal static extern bool SteamAPI_ISteamRemoteStorage_UpdatePublishedFileTitle( IntPtr instancePtr, ulong updateHandle, string pchTitle );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamRemoteStorage_UpdatePublishedFileDescription" )]
        internal static extern bool SteamAPI_ISteamRemoteStorage_UpdatePublishedFileDescription( IntPtr instancePtr, ulong updateHandle, string pchDescription );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamRemoteStorage_UpdatePublishedFileVisibility" )]
        internal static extern bool SteamAPI_ISteamRemoteStorage_UpdatePublishedFileVisibility( IntPtr instancePtr, ulong updateHandle, uint eVisibility );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamRemoteStorage_UpdatePublishedFileTags" )]
        internal static extern bool SteamAPI_ISteamRemoteStorage_UpdatePublishedFileTags( IntPtr instancePtr, ulong updateHandle, ref SteamParamStringArray_t pTags );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamRemoteStorage_CommitPublishedFileUpdate" )]
        internal static extern ulong SteamAPI_ISteamRemoteStorage_CommitPublishedFileUpdate( IntPtr instancePtr, ulong updateHandle );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamRemoteStorage_GetPublishedFileDetails" )]
        internal static extern ulong SteamAPI_ISteamRemoteStorage_GetPublishedFileDetails( IntPtr instancePtr, ulong unPublishedFileId, uint unMaxSecondsOld );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamRemoteStorage_DeletePublishedFile" )]
        internal static extern ulong SteamAPI_ISteamRemoteStorage_DeletePublishedFile( IntPtr instancePtr, ulong unPublishedFileId );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamRemoteStorage_EnumerateUserPublishedFiles" )]
        internal static extern ulong SteamAPI_ISteamRemoteStorage_EnumerateUserPublishedFiles( IntPtr instancePtr, uint unStartIndex );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamRemoteStorage_SubscribePublishedFile" )]
        internal static extern ulong SteamAPI_ISteamRemoteStorage_SubscribePublishedFile( IntPtr instancePtr, ulong unPublishedFileId );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamRemoteStorage_EnumerateUserSubscribedFiles" )]
        internal static extern ulong SteamAPI_ISteamRemoteStorage_EnumerateUserSubscribedFiles( IntPtr instancePtr, uint unStartIndex );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamRemoteStorage_UnsubscribePublishedFile" )]
        internal static extern ulong SteamAPI_ISteamRemoteStorage_UnsubscribePublishedFile( IntPtr instancePtr, ulong unPublishedFileId );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamRemoteStorage_UpdatePublishedFileSetChangeDescription" )]
        internal static extern bool SteamAPI_ISteamRemoteStorage_UpdatePublishedFileSetChangeDescription( IntPtr instancePtr, ulong updateHandle, string pchChangeDescription );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamRemoteStorage_GetPublishedItemVoteDetails" )]
        internal static extern ulong SteamAPI_ISteamRemoteStorage_GetPublishedItemVoteDetails( IntPtr instancePtr, ulong unPublishedFileId );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamRemoteStorage_UpdateUserPublishedItemVote" )]
        internal static extern ulong SteamAPI_ISteamRemoteStorage_UpdateUserPublishedItemVote( IntPtr instancePtr, ulong unPublishedFileId, bool bVoteUp );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamRemoteStorage_GetUserPublishedItemVoteDetails" )]
        internal static extern ulong SteamAPI_ISteamRemoteStorage_GetUserPublishedItemVoteDetails( IntPtr instancePtr, ulong unPublishedFileId );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamRemoteStorage_EnumerateUserSharedWorkshopFiles" )]
        internal static extern ulong SteamAPI_ISteamRemoteStorage_EnumerateUserSharedWorkshopFiles( IntPtr instancePtr, ulong steamId, uint unStartIndex, ref SteamParamStringArray_t pRequiredTags, ref SteamParamStringArray_t pExcludedTags );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamRemoteStorage_PublishVideo" )]
        internal static extern ulong SteamAPI_ISteamRemoteStorage_PublishVideo( IntPtr instancePtr, uint eVideoProvider, string pchVideoAccount, string pchVideoIdentifier, string pchPreviewFile, uint nConsumerAppId, string pchTitle, string pchDescription, uint eVisibility, ref SteamParamStringArray_t pTags );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamRemoteStorage_SetUserPublishedFileAction" )]
        internal static extern ulong SteamAPI_ISteamRemoteStorage_SetUserPublishedFileAction( IntPtr instancePtr, ulong unPublishedFileId, uint eAction );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamRemoteStorage_EnumeratePublishedFilesByUserAction" )]
        internal static extern ulong SteamAPI_ISteamRemoteStorage_EnumeratePublishedFilesByUserAction( IntPtr instancePtr, uint eAction, uint unStartIndex );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamRemoteStorage_EnumeratePublishedWorkshopFiles" )]
        internal static extern ulong SteamAPI_ISteamRemoteStorage_EnumeratePublishedWorkshopFiles( IntPtr instancePtr, uint eEnumerationType, uint unStartIndex, uint unCount, uint unDays, ref SteamParamStringArray_t pTags, ref SteamParamStringArray_t pUserTags );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamRemoteStorage_UGCDownloadToLocation" )]
        internal static extern ulong SteamAPI_ISteamRemoteStorage_UGCDownloadToLocation( IntPtr instancePtr, ulong hContent, string pchLocation, uint unPriority );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUserStats_RequestCurrentStats" )]
        internal static extern bool SteamAPI_ISteamUserStats_RequestCurrentStats( IntPtr instancePtr );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUserStats_GetStat" )]
        internal static extern bool SteamAPI_ISteamUserStats_GetStat( IntPtr instancePtr, string pchName, ref int pData );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUserStats_GetStat0" )]
        internal static extern bool SteamAPI_ISteamUserStats_GetStat0( IntPtr instancePtr, string pchName, ref float pData );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUserStats_SetStat" )]
        internal static extern bool SteamAPI_ISteamUserStats_SetStat( IntPtr instancePtr, string pchName, int nData );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUserStats_SetStat0" )]
        internal static extern bool SteamAPI_ISteamUserStats_SetStat0( IntPtr instancePtr, string pchName, float fData );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUserStats_UpdateAvgRateStat" )]
        internal static extern bool SteamAPI_ISteamUserStats_UpdateAvgRateStat( IntPtr instancePtr, string pchName, float flCountThisSession, double dSessionLength );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUserStats_GetAchievement" )]
        internal static extern bool SteamAPI_ISteamUserStats_GetAchievement( IntPtr instancePtr, string pchName, ref bool pbAchieved );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUserStats_SetAchievement" )]
        internal static extern bool SteamAPI_ISteamUserStats_SetAchievement( IntPtr instancePtr, string pchName );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUserStats_ClearAchievement" )]
        internal static extern bool SteamAPI_ISteamUserStats_ClearAchievement( IntPtr instancePtr, string pchName );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUserStats_GetAchievementAndUnlockTime" )]
        internal static extern bool SteamAPI_ISteamUserStats_GetAchievementAndUnlockTime( IntPtr instancePtr, string pchName, ref bool pbAchieved, ref uint punUnlockTime );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUserStats_StoreStats" )]
        internal static extern bool SteamAPI_ISteamUserStats_StoreStats( IntPtr instancePtr );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUserStats_GetAchievementIcon" )]
        internal static extern int SteamAPI_ISteamUserStats_GetAchievementIcon( IntPtr instancePtr, string pchName );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUserStats_GetAchievementDisplayAttribute" )]
        internal static extern IntPtr SteamAPI_ISteamUserStats_GetAchievementDisplayAttribute( IntPtr instancePtr, string pchName, string pchKey );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUserStats_IndicateAchievementProgress" )]
        internal static extern bool SteamAPI_ISteamUserStats_IndicateAchievementProgress( IntPtr instancePtr, string pchName, uint nCurProgress, uint nMaxProgress );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUserStats_GetNumAchievements" )]
        internal static extern uint SteamAPI_ISteamUserStats_GetNumAchievements( IntPtr instancePtr );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUserStats_GetAchievementName" )]
        internal static extern IntPtr SteamAPI_ISteamUserStats_GetAchievementName( IntPtr instancePtr, uint iAchievement );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUserStats_RequestUserStats" )]
        internal static extern ulong SteamAPI_ISteamUserStats_RequestUserStats( IntPtr instancePtr, ulong steamIDUser );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUserStats_GetUserStat" )]
        internal static extern bool SteamAPI_ISteamUserStats_GetUserStat( IntPtr instancePtr, ulong steamIDUser, string pchName, ref int pData );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUserStats_GetUserStat0" )]
        internal static extern bool SteamAPI_ISteamUserStats_GetUserStat0( IntPtr instancePtr, ulong steamIDUser, string pchName, ref float pData );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUserStats_GetUserAchievement" )]
        internal static extern bool SteamAPI_ISteamUserStats_GetUserAchievement( IntPtr instancePtr, ulong steamIDUser, string pchName, ref bool pbAchieved );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUserStats_GetUserAchievementAndUnlockTime" )]
        internal static extern bool SteamAPI_ISteamUserStats_GetUserAchievementAndUnlockTime( IntPtr instancePtr, ulong steamIDUser, string pchName, ref bool pbAchieved, ref uint punUnlockTime );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUserStats_ResetAllStats" )]
        internal static extern bool SteamAPI_ISteamUserStats_ResetAllStats( IntPtr instancePtr, bool bAchievementsToo );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUserStats_FindOrCreateLeaderboard" )]
        internal static extern ulong SteamAPI_ISteamUserStats_FindOrCreateLeaderboard( IntPtr instancePtr, string pchLeaderboardName, uint eLeaderboardSortMethod, uint eLeaderboardDisplayType );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUserStats_FindLeaderboard" )]
        internal static extern ulong SteamAPI_ISteamUserStats_FindLeaderboard( IntPtr instancePtr, string pchLeaderboardName );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUserStats_GetLeaderboardName" )]
        internal static extern IntPtr SteamAPI_ISteamUserStats_GetLeaderboardName( IntPtr instancePtr, ulong hSteamLeaderboard );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUserStats_GetLeaderboardEntryCount" )]
        internal static extern int SteamAPI_ISteamUserStats_GetLeaderboardEntryCount( IntPtr instancePtr, ulong hSteamLeaderboard );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUserStats_GetLeaderboardSortMethod" )]
        internal static extern uint SteamAPI_ISteamUserStats_GetLeaderboardSortMethod( IntPtr instancePtr, ulong hSteamLeaderboard );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUserStats_GetLeaderboardDisplayType" )]
        internal static extern uint SteamAPI_ISteamUserStats_GetLeaderboardDisplayType( IntPtr instancePtr, ulong hSteamLeaderboard );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUserStats_DownloadLeaderboardEntries" )]
        internal static extern ulong SteamAPI_ISteamUserStats_DownloadLeaderboardEntries( IntPtr instancePtr, ulong hSteamLeaderboard, uint eLeaderboardDataRequest, int nRangeStart, int nRangeEnd );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUserStats_DownloadLeaderboardEntriesForUsers" )]
        internal static extern ulong SteamAPI_ISteamUserStats_DownloadLeaderboardEntriesForUsers( IntPtr instancePtr, ulong hSteamLeaderboard, [In, Out] CSteamID[] prgUsers, int cUsers );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUserStats_GetDownloadedLeaderboardEntry" )]
        internal static extern bool SteamAPI_ISteamUserStats_GetDownloadedLeaderboardEntry( IntPtr instancePtr, ulong hSteamLeaderboardEntries, int index, ref LeaderboardEntry_t pLeaderboardEntry, ref int pDetails, int cDetailsMax );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUserStats_UploadLeaderboardScore" )]
        internal static extern ulong SteamAPI_ISteamUserStats_UploadLeaderboardScore( IntPtr instancePtr, ulong hSteamLeaderboard, uint eLeaderboardUploadScoreMethod, int nScore, ref int pScoreDetails, int cScoreDetailsCount );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUserStats_AttachLeaderboardUGC" )]
        internal static extern ulong SteamAPI_ISteamUserStats_AttachLeaderboardUGC( IntPtr instancePtr, ulong hSteamLeaderboard, ulong hUGC );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUserStats_GetNumberOfCurrentPlayers" )]
        internal static extern ulong SteamAPI_ISteamUserStats_GetNumberOfCurrentPlayers( IntPtr instancePtr );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUserStats_RequestGlobalAchievementPercentages" )]
        internal static extern ulong SteamAPI_ISteamUserStats_RequestGlobalAchievementPercentages( IntPtr instancePtr );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUserStats_GetMostAchievedAchievementInfo" )]
        internal static extern int SteamAPI_ISteamUserStats_GetMostAchievedAchievementInfo( IntPtr instancePtr, string pchName, uint unNameBufLen, ref float pflPercent, ref bool pbAchieved );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUserStats_GetNextMostAchievedAchievementInfo" )]
        internal static extern int SteamAPI_ISteamUserStats_GetNextMostAchievedAchievementInfo( IntPtr instancePtr, int iIteratorPrevious, string pchName, uint unNameBufLen, ref float pflPercent, ref bool pbAchieved );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUserStats_GetAchievementAchievedPercent" )]
        internal static extern bool SteamAPI_ISteamUserStats_GetAchievementAchievedPercent( IntPtr instancePtr, string pchName, ref float pflPercent );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUserStats_RequestGlobalStats" )]
        internal static extern ulong SteamAPI_ISteamUserStats_RequestGlobalStats( IntPtr instancePtr, int nHistoryDays );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUserStats_GetGlobalStat" )]
        internal static extern bool SteamAPI_ISteamUserStats_GetGlobalStat( IntPtr instancePtr, string pchStatName, ref long pData );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUserStats_GetGlobalStat0" )]
        internal static extern bool SteamAPI_ISteamUserStats_GetGlobalStat0( IntPtr instancePtr, string pchStatName, ref double pData );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUserStats_GetGlobalStatHistory" )]
        internal static extern int SteamAPI_ISteamUserStats_GetGlobalStatHistory( IntPtr instancePtr, string pchStatName, [In, Out] long[] pData, uint cubData );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUserStats_GetGlobalStatHistory0" )]
        internal static extern int SteamAPI_ISteamUserStats_GetGlobalStatHistory0( IntPtr instancePtr, string pchStatName, [In, Out] double[] pData, uint cubData );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamApps_BIsSubscribed" )]
        internal static extern bool SteamAPI_ISteamApps_BIsSubscribed( IntPtr instancePtr );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamApps_BIsLowViolence" )]
        internal static extern bool SteamAPI_ISteamApps_BIsLowViolence( IntPtr instancePtr );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamApps_BIsCybercafe" )]
        internal static extern bool SteamAPI_ISteamApps_BIsCybercafe( IntPtr instancePtr );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamApps_BIsVACBanned" )]
        internal static extern bool SteamAPI_ISteamApps_BIsVACBanned( IntPtr instancePtr );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamApps_GetCurrentGameLanguage" )]
        internal static extern IntPtr SteamAPI_ISteamApps_GetCurrentGameLanguage( IntPtr instancePtr );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamApps_GetAvailableGameLanguages" )]
        internal static extern IntPtr SteamAPI_ISteamApps_GetAvailableGameLanguages( IntPtr instancePtr );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamApps_BIsSubscribedApp" )]
        internal static extern bool SteamAPI_ISteamApps_BIsSubscribedApp( IntPtr instancePtr, uint appID );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamApps_BIsDlcInstalled" )]
        internal static extern bool SteamAPI_ISteamApps_BIsDlcInstalled( IntPtr instancePtr, uint appID );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamApps_GetEarliestPurchaseUnixTime" )]
        internal static extern uint SteamAPI_ISteamApps_GetEarliestPurchaseUnixTime( IntPtr instancePtr, uint nAppID );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamApps_BIsSubscribedFromFreeWeekend" )]
        internal static extern bool SteamAPI_ISteamApps_BIsSubscribedFromFreeWeekend( IntPtr instancePtr );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamApps_GetDLCCount" )]
        internal static extern int SteamAPI_ISteamApps_GetDLCCount( IntPtr instancePtr );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamApps_BGetDLCDataByIndex" )]
        internal static extern bool SteamAPI_ISteamApps_BGetDLCDataByIndex( IntPtr instancePtr, int iDLC, ref uint pAppID, ref bool pbAvailable, string pchName, int cchNameBufferSize );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamApps_InstallDLC" )]
        internal static extern void SteamAPI_ISteamApps_InstallDLC( IntPtr instancePtr, uint nAppID );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamApps_UninstallDLC" )]
        internal static extern void SteamAPI_ISteamApps_UninstallDLC( IntPtr instancePtr, uint nAppID );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamApps_RequestAppProofOfPurchaseKey" )]
        internal static extern void SteamAPI_ISteamApps_RequestAppProofOfPurchaseKey( IntPtr instancePtr, uint nAppID );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamApps_GetCurrentBetaName" )]
        internal static extern bool SteamAPI_ISteamApps_GetCurrentBetaName( IntPtr instancePtr, IntPtr pchName, int cchNameBufferSize );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamApps_MarkContentCorrupt" )]
        internal static extern bool SteamAPI_ISteamApps_MarkContentCorrupt( IntPtr instancePtr, bool bMissingFilesOnly );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamApps_GetInstalledDepots" )]
        internal static extern uint SteamAPI_ISteamApps_GetInstalledDepots( IntPtr instancePtr, uint appID, ref uint pvecDepots, uint cMaxDepots );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamApps_GetAppInstallDir" )]
        internal static extern uint SteamAPI_ISteamApps_GetAppInstallDir( IntPtr instancePtr, uint appID, IntPtr pchFolder, uint cchFolderBufferSize );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamApps_BIsAppInstalled" )]
        internal static extern bool SteamAPI_ISteamApps_BIsAppInstalled( IntPtr instancePtr, uint appID );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamApps_GetAppOwner" )]
        internal static extern ulong SteamAPI_ISteamApps_GetAppOwner( IntPtr instancePtr );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamApps_GetLaunchQueryParam" )]
        internal static extern IntPtr SteamAPI_ISteamApps_GetLaunchQueryParam( IntPtr instancePtr, string pchKey );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamApps_GetDlcDownloadProgress" )]
        internal static extern bool SteamAPI_ISteamApps_GetDlcDownloadProgress( IntPtr instancePtr, uint nAppID, ref ulong punBytesDownloaded, ref ulong punBytesTotal );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamApps_GetAppBuildId" )]
        internal static extern int SteamAPI_ISteamApps_GetAppBuildId( IntPtr instancePtr );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamApps_RequestAllProofOfPurchaseKeys" )]
        internal static extern void SteamAPI_ISteamApps_RequestAllProofOfPurchaseKeys( IntPtr instancePtr );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworking_SendP2PPacket" )]
        internal static extern bool SteamAPI_ISteamNetworking_SendP2PPacket( IntPtr instancePtr, ulong steamIDRemote, IntPtr pubData, uint cubData, uint eP2PSendType, int nChannel );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworking_IsP2PPacketAvailable" )]
        internal static extern bool SteamAPI_ISteamNetworking_IsP2PPacketAvailable( IntPtr instancePtr, ref uint pcubMsgSize, int nChannel );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworking_ReadP2PPacket" )]
        internal static extern bool SteamAPI_ISteamNetworking_ReadP2PPacket( IntPtr instancePtr, IntPtr pubDest, uint cubDest, ref uint pcubMsgSize, ref ulong psteamIDRemote, int nChannel );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworking_AcceptP2PSessionWithUser" )]
        internal static extern bool SteamAPI_ISteamNetworking_AcceptP2PSessionWithUser( IntPtr instancePtr, ulong steamIDRemote );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworking_CloseP2PSessionWithUser" )]
        internal static extern bool SteamAPI_ISteamNetworking_CloseP2PSessionWithUser( IntPtr instancePtr, ulong steamIDRemote );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworking_CloseP2PChannelWithUser" )]
        internal static extern bool SteamAPI_ISteamNetworking_CloseP2PChannelWithUser( IntPtr instancePtr, ulong steamIDRemote, int nChannel );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworking_GetP2PSessionState" )]
        internal static extern bool SteamAPI_ISteamNetworking_GetP2PSessionState( IntPtr instancePtr, ulong steamIDRemote, ref P2PSessionState_t pConnectionState );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworking_AllowP2PPacketRelay" )]
        internal static extern bool SteamAPI_ISteamNetworking_AllowP2PPacketRelay( IntPtr instancePtr, bool bAllow );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworking_CreateListenSocket" )]
        internal static extern uint SteamAPI_ISteamNetworking_CreateListenSocket( IntPtr instancePtr, int nVirtualP2PPort, uint nIP, char nPort, bool bAllowUseOfPacketRelay );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworking_CreateP2PConnectionSocket" )]
        internal static extern uint SteamAPI_ISteamNetworking_CreateP2PConnectionSocket( IntPtr instancePtr, ulong steamIDTarget, int nVirtualPort, int nTimeoutSec, bool bAllowUseOfPacketRelay );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworking_CreateConnectionSocket" )]
        internal static extern uint SteamAPI_ISteamNetworking_CreateConnectionSocket( IntPtr instancePtr, uint nIP, char nPort, int nTimeoutSec );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworking_DestroySocket" )]
        internal static extern bool SteamAPI_ISteamNetworking_DestroySocket( IntPtr instancePtr, uint hSocket, bool bNotifyRemoteEnd );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworking_DestroyListenSocket" )]
        internal static extern bool SteamAPI_ISteamNetworking_DestroyListenSocket( IntPtr instancePtr, uint hSocket, bool bNotifyRemoteEnd );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworking_SendDataOnSocket" )]
        internal static extern bool SteamAPI_ISteamNetworking_SendDataOnSocket( IntPtr instancePtr, uint hSocket, IntPtr pubData, uint cubData, bool bReliable );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworking_IsDataAvailableOnSocket" )]
        internal static extern bool SteamAPI_ISteamNetworking_IsDataAvailableOnSocket( IntPtr instancePtr, uint hSocket, ref uint pcubMsgSize );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworking_RetrieveDataFromSocket" )]
        internal static extern bool SteamAPI_ISteamNetworking_RetrieveDataFromSocket( IntPtr instancePtr, uint hSocket, IntPtr pubDest, uint cubDest, ref uint pcubMsgSize );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworking_IsDataAvailable" )]
        internal static extern bool SteamAPI_ISteamNetworking_IsDataAvailable( IntPtr instancePtr, uint hListenSocket, ref uint pcubMsgSize, ref uint phSocket );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworking_RetrieveData" )]
        internal static extern bool SteamAPI_ISteamNetworking_RetrieveData( IntPtr instancePtr, uint hListenSocket, IntPtr pubDest, uint cubDest, ref uint pcubMsgSize, ref uint phSocket );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworking_GetSocketInfo" )]
        internal static extern bool SteamAPI_ISteamNetworking_GetSocketInfo( IntPtr instancePtr, uint hSocket, ref CSteamID pSteamIDRemote, ref int peSocketStatus, ref uint punIPRemote, ref char punPortRemote );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworking_GetListenSocketInfo" )]
        internal static extern bool SteamAPI_ISteamNetworking_GetListenSocketInfo( IntPtr instancePtr, uint hListenSocket, ref uint pnIP, ref char pnPort );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworking_GetSocketConnectionType" )]
        internal static extern uint SteamAPI_ISteamNetworking_GetSocketConnectionType( IntPtr instancePtr, uint hSocket );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworking_GetMaxPacketSize" )]
        internal static extern int SteamAPI_ISteamNetworking_GetMaxPacketSize( IntPtr instancePtr, uint hSocket );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamScreenshots_WriteScreenshot" )]
        internal static extern uint SteamAPI_ISteamScreenshots_WriteScreenshot( IntPtr instancePtr, IntPtr pubRGB, uint cubRGB, int nWidth, int nHeight );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamScreenshots_AddScreenshotToLibrary" )]
        internal static extern uint SteamAPI_ISteamScreenshots_AddScreenshotToLibrary( IntPtr instancePtr, string pchFilename, string pchThumbnailFilename, int nWidth, int nHeight );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamScreenshots_TriggerScreenshot" )]
        internal static extern void SteamAPI_ISteamScreenshots_TriggerScreenshot( IntPtr instancePtr );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamScreenshots_HookScreenshots" )]
        internal static extern void SteamAPI_ISteamScreenshots_HookScreenshots( IntPtr instancePtr, bool bHook );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamScreenshots_SetLocation" )]
        internal static extern bool SteamAPI_ISteamScreenshots_SetLocation( IntPtr instancePtr, uint hScreenshot, string pchLocation );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamScreenshots_TagUser" )]
        internal static extern bool SteamAPI_ISteamScreenshots_TagUser( IntPtr instancePtr, uint hScreenshot, ulong steamID );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamScreenshots_TagPublishedFile" )]
        internal static extern bool SteamAPI_ISteamScreenshots_TagPublishedFile( IntPtr instancePtr, uint hScreenshot, ulong unPublishedFileID );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMusic_BIsEnabled" )]
        internal static extern bool SteamAPI_ISteamMusic_BIsEnabled( IntPtr instancePtr );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMusic_BIsPlaying" )]
        internal static extern bool SteamAPI_ISteamMusic_BIsPlaying( IntPtr instancePtr );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMusic_GetPlaybackStatus" )]
        internal static extern int SteamAPI_ISteamMusic_GetPlaybackStatus( IntPtr instancePtr );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMusic_Play" )]
        internal static extern void SteamAPI_ISteamMusic_Play( IntPtr instancePtr );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMusic_Pause" )]
        internal static extern void SteamAPI_ISteamMusic_Pause( IntPtr instancePtr );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMusic_PlayPrevious" )]
        internal static extern void SteamAPI_ISteamMusic_PlayPrevious( IntPtr instancePtr );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMusic_PlayNext" )]
        internal static extern void SteamAPI_ISteamMusic_PlayNext( IntPtr instancePtr );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMusic_SetVolume" )]
        internal static extern void SteamAPI_ISteamMusic_SetVolume( IntPtr instancePtr, float flVolume );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMusic_GetVolume" )]
        internal static extern float SteamAPI_ISteamMusic_GetVolume( IntPtr instancePtr );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMusicRemote_RegisterSteamMusicRemote" )]
        internal static extern bool SteamAPI_ISteamMusicRemote_RegisterSteamMusicRemote( IntPtr instancePtr, string pchName );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMusicRemote_DeregisterSteamMusicRemote" )]
        internal static extern bool SteamAPI_ISteamMusicRemote_DeregisterSteamMusicRemote( IntPtr instancePtr );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMusicRemote_BIsCurrentMusicRemote" )]
        internal static extern bool SteamAPI_ISteamMusicRemote_BIsCurrentMusicRemote( IntPtr instancePtr );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMusicRemote_BActivationSuccess" )]
        internal static extern bool SteamAPI_ISteamMusicRemote_BActivationSuccess( IntPtr instancePtr, bool bValue );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMusicRemote_SetDisplayName" )]
        internal static extern bool SteamAPI_ISteamMusicRemote_SetDisplayName( IntPtr instancePtr, string pchDisplayName );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMusicRemote_SetPNGIcon_64x64" )]
        internal static extern bool SteamAPI_ISteamMusicRemote_SetPNGIcon_64x64( IntPtr instancePtr, IntPtr pvBuffer, uint cbBufferLength );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMusicRemote_EnablePlayPrevious" )]
        internal static extern bool SteamAPI_ISteamMusicRemote_EnablePlayPrevious( IntPtr instancePtr, bool bValue );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMusicRemote_EnablePlayNext" )]
        internal static extern bool SteamAPI_ISteamMusicRemote_EnablePlayNext( IntPtr instancePtr, bool bValue );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMusicRemote_EnableShuffled" )]
        internal static extern bool SteamAPI_ISteamMusicRemote_EnableShuffled( IntPtr instancePtr, bool bValue );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMusicRemote_EnableLooped" )]
        internal static extern bool SteamAPI_ISteamMusicRemote_EnableLooped( IntPtr instancePtr, bool bValue );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMusicRemote_EnableQueue" )]
        internal static extern bool SteamAPI_ISteamMusicRemote_EnableQueue( IntPtr instancePtr, bool bValue );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMusicRemote_EnablePlaylists" )]
        internal static extern bool SteamAPI_ISteamMusicRemote_EnablePlaylists( IntPtr instancePtr, bool bValue );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMusicRemote_UpdatePlaybackStatus" )]
        internal static extern bool SteamAPI_ISteamMusicRemote_UpdatePlaybackStatus( IntPtr instancePtr, int nStatus );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMusicRemote_UpdateShuffled" )]
        internal static extern bool SteamAPI_ISteamMusicRemote_UpdateShuffled( IntPtr instancePtr, bool bValue );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMusicRemote_UpdateLooped" )]
        internal static extern bool SteamAPI_ISteamMusicRemote_UpdateLooped( IntPtr instancePtr, bool bValue );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMusicRemote_UpdateVolume" )]
        internal static extern bool SteamAPI_ISteamMusicRemote_UpdateVolume( IntPtr instancePtr, float flValue );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMusicRemote_CurrentEntryWillChange" )]
        internal static extern bool SteamAPI_ISteamMusicRemote_CurrentEntryWillChange( IntPtr instancePtr );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMusicRemote_CurrentEntryIsAvailable" )]
        internal static extern bool SteamAPI_ISteamMusicRemote_CurrentEntryIsAvailable( IntPtr instancePtr, bool bAvailable );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMusicRemote_UpdateCurrentEntryText" )]
        internal static extern bool SteamAPI_ISteamMusicRemote_UpdateCurrentEntryText( IntPtr instancePtr, string pchText );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMusicRemote_UpdateCurrentEntryElapsedSeconds" )]
        internal static extern bool SteamAPI_ISteamMusicRemote_UpdateCurrentEntryElapsedSeconds( IntPtr instancePtr, int nValue );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMusicRemote_UpdateCurrentEntryCoverArt" )]
        internal static extern bool SteamAPI_ISteamMusicRemote_UpdateCurrentEntryCoverArt( IntPtr instancePtr, IntPtr pvBuffer, uint cbBufferLength );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMusicRemote_CurrentEntryDidChange" )]
        internal static extern bool SteamAPI_ISteamMusicRemote_CurrentEntryDidChange( IntPtr instancePtr );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMusicRemote_QueueWillChange" )]
        internal static extern bool SteamAPI_ISteamMusicRemote_QueueWillChange( IntPtr instancePtr );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMusicRemote_ResetQueueEntries" )]
        internal static extern bool SteamAPI_ISteamMusicRemote_ResetQueueEntries( IntPtr instancePtr );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMusicRemote_SetQueueEntry" )]
        internal static extern bool SteamAPI_ISteamMusicRemote_SetQueueEntry( IntPtr instancePtr, int nID, int nPosition, string pchEntryText );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMusicRemote_SetCurrentQueueEntry" )]
        internal static extern bool SteamAPI_ISteamMusicRemote_SetCurrentQueueEntry( IntPtr instancePtr, int nID );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMusicRemote_QueueDidChange" )]
        internal static extern bool SteamAPI_ISteamMusicRemote_QueueDidChange( IntPtr instancePtr );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMusicRemote_PlaylistWillChange" )]
        internal static extern bool SteamAPI_ISteamMusicRemote_PlaylistWillChange( IntPtr instancePtr );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMusicRemote_ResetPlaylistEntries" )]
        internal static extern bool SteamAPI_ISteamMusicRemote_ResetPlaylistEntries( IntPtr instancePtr );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMusicRemote_SetPlaylistEntry" )]
        internal static extern bool SteamAPI_ISteamMusicRemote_SetPlaylistEntry( IntPtr instancePtr, int nID, int nPosition, string pchEntryText );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMusicRemote_SetCurrentPlaylistEntry" )]
        internal static extern bool SteamAPI_ISteamMusicRemote_SetCurrentPlaylistEntry( IntPtr instancePtr, int nID );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMusicRemote_PlaylistDidChange" )]
        internal static extern bool SteamAPI_ISteamMusicRemote_PlaylistDidChange( IntPtr instancePtr );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamHTTP_CreateHTTPRequest" )]
        internal static extern uint SteamAPI_ISteamHTTP_CreateHTTPRequest( IntPtr instancePtr, uint eHTTPRequestMethod, string pchAbsoluteURL );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamHTTP_SetHTTPRequestContextValue" )]
        internal static extern bool SteamAPI_ISteamHTTP_SetHTTPRequestContextValue( IntPtr instancePtr, uint hRequest, ulong ulContextValue );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamHTTP_SetHTTPRequestNetworkActivityTimeout" )]
        internal static extern bool SteamAPI_ISteamHTTP_SetHTTPRequestNetworkActivityTimeout( IntPtr instancePtr, uint hRequest, uint unTimeoutSeconds );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamHTTP_SetHTTPRequestHeaderValue" )]
        internal static extern bool SteamAPI_ISteamHTTP_SetHTTPRequestHeaderValue( IntPtr instancePtr, uint hRequest, string pchHeaderName, string pchHeaderValue );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamHTTP_SetHTTPRequestGetOrPostParameter" )]
        internal static extern bool SteamAPI_ISteamHTTP_SetHTTPRequestGetOrPostParameter( IntPtr instancePtr, uint hRequest, string pchParamName, string pchParamValue );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamHTTP_SendHTTPRequest" )]
        internal static extern bool SteamAPI_ISteamHTTP_SendHTTPRequest( IntPtr instancePtr, uint hRequest, ref ulong pCallHandle );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamHTTP_SendHTTPRequestAndStreamResponse" )]
        internal static extern bool SteamAPI_ISteamHTTP_SendHTTPRequestAndStreamResponse( IntPtr instancePtr, uint hRequest, ref ulong pCallHandle );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamHTTP_DeferHTTPRequest" )]
        internal static extern bool SteamAPI_ISteamHTTP_DeferHTTPRequest( IntPtr instancePtr, uint hRequest );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamHTTP_PrioritizeHTTPRequest" )]
        internal static extern bool SteamAPI_ISteamHTTP_PrioritizeHTTPRequest( IntPtr instancePtr, uint hRequest );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamHTTP_GetHTTPResponseHeaderSize" )]
        internal static extern bool SteamAPI_ISteamHTTP_GetHTTPResponseHeaderSize( IntPtr instancePtr, uint hRequest, string pchHeaderName, ref uint unResponseHeaderSize );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamHTTP_GetHTTPResponseHeaderValue" )]
        internal static extern bool SteamAPI_ISteamHTTP_GetHTTPResponseHeaderValue( IntPtr instancePtr, uint hRequest, string pchHeaderName, IntPtr pHeaderValueBuffer, uint unBufferSize );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamHTTP_GetHTTPResponseBodySize" )]
        internal static extern bool SteamAPI_ISteamHTTP_GetHTTPResponseBodySize( IntPtr instancePtr, uint hRequest, ref uint unBodySize );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamHTTP_GetHTTPResponseBodyData" )]
        internal static extern bool SteamAPI_ISteamHTTP_GetHTTPResponseBodyData( IntPtr instancePtr, uint hRequest, IntPtr pBodyDataBuffer, uint unBufferSize );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamHTTP_GetHTTPStreamingResponseBodyData" )]
        internal static extern bool SteamAPI_ISteamHTTP_GetHTTPStreamingResponseBodyData( IntPtr instancePtr, uint hRequest, uint cOffset, IntPtr pBodyDataBuffer, uint unBufferSize );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamHTTP_ReleaseHTTPRequest" )]
        internal static extern bool SteamAPI_ISteamHTTP_ReleaseHTTPRequest( IntPtr instancePtr, uint hRequest );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamHTTP_GetHTTPDownloadProgressPct" )]
        internal static extern bool SteamAPI_ISteamHTTP_GetHTTPDownloadProgressPct( IntPtr instancePtr, uint hRequest, ref float pflPercentOut );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamHTTP_SetHTTPRequestRawPostBody" )]
        internal static extern bool SteamAPI_ISteamHTTP_SetHTTPRequestRawPostBody( IntPtr instancePtr, uint hRequest, string pchContentType, IntPtr pubBody, uint unBodyLen );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamHTTP_CreateCookieContainer" )]
        internal static extern uint SteamAPI_ISteamHTTP_CreateCookieContainer( IntPtr instancePtr, bool bAllowResponsesToModify );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamHTTP_ReleaseCookieContainer" )]
        internal static extern bool SteamAPI_ISteamHTTP_ReleaseCookieContainer( IntPtr instancePtr, uint hCookieContainer );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamHTTP_SetCookie" )]
        internal static extern bool SteamAPI_ISteamHTTP_SetCookie( IntPtr instancePtr, uint hCookieContainer, string pchHost, string pchUrl, string pchCookie );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamHTTP_SetHTTPRequestCookieContainer" )]
        internal static extern bool SteamAPI_ISteamHTTP_SetHTTPRequestCookieContainer( IntPtr instancePtr, uint hRequest, uint hCookieContainer );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamHTTP_SetHTTPRequestUserAgentInfo" )]
        internal static extern bool SteamAPI_ISteamHTTP_SetHTTPRequestUserAgentInfo( IntPtr instancePtr, uint hRequest, string pchUserAgentInfo );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamHTTP_SetHTTPRequestRequiresVerifiedCertificate" )]
        internal static extern bool SteamAPI_ISteamHTTP_SetHTTPRequestRequiresVerifiedCertificate( IntPtr instancePtr, uint hRequest, bool bRequireVerifiedCertificate );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamHTTP_SetHTTPRequestAbsoluteTimeoutMS" )]
        internal static extern bool SteamAPI_ISteamHTTP_SetHTTPRequestAbsoluteTimeoutMS( IntPtr instancePtr, uint hRequest, uint unMilliseconds );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamHTTP_GetHTTPRequestWasTimedOut" )]
        internal static extern bool SteamAPI_ISteamHTTP_GetHTTPRequestWasTimedOut( IntPtr instancePtr, uint hRequest, ref bool pbWasTimedOut );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUnifiedMessages_SendMethod" )]
        internal static extern ulong SteamAPI_ISteamUnifiedMessages_SendMethod( IntPtr instancePtr, string pchServiceMethod, IntPtr pRequestBuffer, uint unRequestBufferSize, ulong unContext );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUnifiedMessages_GetMethodResponseInfo" )]
        internal static extern bool SteamAPI_ISteamUnifiedMessages_GetMethodResponseInfo( IntPtr instancePtr, ulong hHandle, ref uint punResponseSize, ref uint peResult );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUnifiedMessages_GetMethodResponseData" )]
        internal static extern bool SteamAPI_ISteamUnifiedMessages_GetMethodResponseData( IntPtr instancePtr, ulong hHandle, IntPtr pResponseBuffer, uint unResponseBufferSize, bool bAutoRelease );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUnifiedMessages_ReleaseMethod" )]
        internal static extern bool SteamAPI_ISteamUnifiedMessages_ReleaseMethod( IntPtr instancePtr, ulong hHandle );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUnifiedMessages_SendNotification" )]
        internal static extern bool SteamAPI_ISteamUnifiedMessages_SendNotification( IntPtr instancePtr, string pchServiceNotification, IntPtr pNotificationBuffer, uint unNotificationBufferSize );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamController_Init" )]
        internal static extern bool SteamAPI_ISteamController_Init( IntPtr instancePtr );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamController_Shutdown" )]
        internal static extern bool SteamAPI_ISteamController_Shutdown( IntPtr instancePtr );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamController_RunFrame" )]
        internal static extern void SteamAPI_ISteamController_RunFrame( IntPtr instancePtr );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamController_GetConnectedControllers" )]
        internal static extern int SteamAPI_ISteamController_GetConnectedControllers( IntPtr instancePtr, ref ulong handlesOut );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamController_ShowBindingPanel" )]
        internal static extern bool SteamAPI_ISteamController_ShowBindingPanel( IntPtr instancePtr, ulong controllerHandle );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamController_GetActionSetHandle" )]
        internal static extern ulong SteamAPI_ISteamController_GetActionSetHandle( IntPtr instancePtr, string pszActionSetName );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamController_ActivateActionSet" )]
        internal static extern void SteamAPI_ISteamController_ActivateActionSet( IntPtr instancePtr, ulong controllerHandle, ulong actionSetHandle );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamController_GetCurrentActionSet" )]
        internal static extern ulong SteamAPI_ISteamController_GetCurrentActionSet( IntPtr instancePtr, ulong controllerHandle );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamController_GetDigitalActionHandle" )]
        internal static extern ulong SteamAPI_ISteamController_GetDigitalActionHandle( IntPtr instancePtr, string pszActionName );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamController_GetDigitalActionData" )]
        internal static extern ControllerDigitalActionData_t SteamAPI_ISteamController_GetDigitalActionData( IntPtr instancePtr, ulong controllerHandle, ulong digitalActionHandle );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamController_GetDigitalActionOrigins" )]
        internal static extern int SteamAPI_ISteamController_GetDigitalActionOrigins( IntPtr instancePtr, ulong controllerHandle, ulong actionSetHandle, ulong digitalActionHandle, ref uint originsOut );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamController_GetAnalogActionHandle" )]
        internal static extern ulong SteamAPI_ISteamController_GetAnalogActionHandle( IntPtr instancePtr, string pszActionName );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamController_GetAnalogActionData" )]
        internal static extern ControllerAnalogActionData_t SteamAPI_ISteamController_GetAnalogActionData( IntPtr instancePtr, ulong controllerHandle, ulong analogActionHandle );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamController_GetAnalogActionOrigins" )]
        internal static extern int SteamAPI_ISteamController_GetAnalogActionOrigins( IntPtr instancePtr, ulong controllerHandle, ulong actionSetHandle, ulong analogActionHandle, ref uint originsOut );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamController_StopAnalogActionMomentum" )]
        internal static extern void SteamAPI_ISteamController_StopAnalogActionMomentum( IntPtr instancePtr, ulong controllerHandle, ulong eAction );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamController_TriggerHapticPulse" )]
        internal static extern void SteamAPI_ISteamController_TriggerHapticPulse( IntPtr instancePtr, ulong controllerHandle, uint eTargetPad, char usDurationMicroSec );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamController_TriggerRepeatedHapticPulse" )]
        internal static extern void SteamAPI_ISteamController_TriggerRepeatedHapticPulse( IntPtr instancePtr, ulong controllerHandle, uint eTargetPad, char usDurationMicroSec, char usOffMicroSec, char unRepeat, uint nFlags );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_CreateQueryUserUGCRequest" )]
        internal static extern ulong SteamAPI_ISteamUGC_CreateQueryUserUGCRequest( IntPtr instancePtr, uint unAccountID, uint eListType, uint eMatchingUGCType, uint eSortOrder, uint nCreatorAppID, uint nConsumerAppID, uint unPage );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_CreateQueryAllUGCRequest" )]
        internal static extern ulong SteamAPI_ISteamUGC_CreateQueryAllUGCRequest( IntPtr instancePtr, uint eQueryType, uint eMatchingeMatchingUGCTypeFileType, uint nCreatorAppID, uint nConsumerAppID, uint unPage );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_CreateQueryUGCDetailsRequest" )]
        internal static extern ulong SteamAPI_ISteamUGC_CreateQueryUGCDetailsRequest( IntPtr instancePtr, ref ulong pvecPublishedFileID, uint unNumPublishedFileIDs );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_SendQueryUGCRequest" )]
        internal static extern ulong SteamAPI_ISteamUGC_SendQueryUGCRequest( IntPtr instancePtr, ulong handle );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_GetQueryUGCResult" )]
        internal static extern bool SteamAPI_ISteamUGC_GetQueryUGCResult( IntPtr instancePtr, ulong handle, uint index, ref SteamUGCDetails_t pDetails );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_GetQueryUGCPreviewURL" )]
        internal static extern bool SteamAPI_ISteamUGC_GetQueryUGCPreviewURL( IntPtr instancePtr, ulong handle, uint index, System.Text.StringBuilder pchURL, uint cchURLSize );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_GetQueryUGCMetadata" )]
        internal static extern bool SteamAPI_ISteamUGC_GetQueryUGCMetadata( IntPtr instancePtr, ulong handle, uint index, System.Text.StringBuilder pchMetadata, uint cchMetadatasize );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_GetQueryUGCChildren" )]
        internal static extern bool SteamAPI_ISteamUGC_GetQueryUGCChildren( IntPtr instancePtr, ulong handle, uint index, ref ulong pvecPublishedFileID, uint cMaxEntries );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_GetQueryUGCStatistic" )]
        internal static extern bool SteamAPI_ISteamUGC_GetQueryUGCStatistic( IntPtr instancePtr, ulong handle, uint index, uint eStatType, ref uint pStatValue );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_GetQueryUGCNumAdditionalPreviews" )]
        internal static extern uint SteamAPI_ISteamUGC_GetQueryUGCNumAdditionalPreviews( IntPtr instancePtr, ulong handle, uint index );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_GetQueryUGCAdditionalPreview" )]
        internal static extern bool SteamAPI_ISteamUGC_GetQueryUGCAdditionalPreview( IntPtr instancePtr, ulong handle, uint index, uint previewIndex, System.Text.StringBuilder pchURLOrVideoID, uint cchURLSize, System.Text.StringBuilder pchOriginalFileName, uint cchOriginalFileNameSize, ref uint pPreviewType );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_GetQueryUGCNumKeyValueTags" )]
        internal static extern uint SteamAPI_ISteamUGC_GetQueryUGCNumKeyValueTags( IntPtr instancePtr, ulong handle, uint index );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_GetQueryUGCKeyValueTag" )]
        internal static extern bool SteamAPI_ISteamUGC_GetQueryUGCKeyValueTag( IntPtr instancePtr, ulong handle, uint index, uint keyValueTagIndex, System.Text.StringBuilder pchKey, uint cchKeySize, System.Text.StringBuilder pchValue, uint cchValueSize );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_ReleaseQueryUGCRequest" )]
        internal static extern bool SteamAPI_ISteamUGC_ReleaseQueryUGCRequest( IntPtr instancePtr, ulong handle );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_AddRequiredTag" )]
        internal static extern bool SteamAPI_ISteamUGC_AddRequiredTag( IntPtr instancePtr, ulong handle, string pTagName );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_AddExcludedTag" )]
        internal static extern bool SteamAPI_ISteamUGC_AddExcludedTag( IntPtr instancePtr, ulong handle, string pTagName );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_SetReturnKeyValueTags" )]
        internal static extern bool SteamAPI_ISteamUGC_SetReturnKeyValueTags( IntPtr instancePtr, ulong handle, bool bReturnKeyValueTags );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_SetReturnLongDescription" )]
        internal static extern bool SteamAPI_ISteamUGC_SetReturnLongDescription( IntPtr instancePtr, ulong handle, bool bReturnLongDescription );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_SetReturnMetadata" )]
        internal static extern bool SteamAPI_ISteamUGC_SetReturnMetadata( IntPtr instancePtr, ulong handle, bool bReturnMetadata );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_SetReturnChildren" )]
        internal static extern bool SteamAPI_ISteamUGC_SetReturnChildren( IntPtr instancePtr, ulong handle, bool bReturnChildren );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_SetReturnAdditionalPreviews" )]
        internal static extern bool SteamAPI_ISteamUGC_SetReturnAdditionalPreviews( IntPtr instancePtr, ulong handle, bool bReturnAdditionalPreviews );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_SetReturnTotalOnly" )]
        internal static extern bool SteamAPI_ISteamUGC_SetReturnTotalOnly( IntPtr instancePtr, ulong handle, bool bReturnTotalOnly );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_SetLanguage" )]
        internal static extern bool SteamAPI_ISteamUGC_SetLanguage( IntPtr instancePtr, ulong handle, string pchLanguage );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_SetAllowCachedResponse" )]
        internal static extern bool SteamAPI_ISteamUGC_SetAllowCachedResponse( IntPtr instancePtr, ulong handle, uint unMaxAgeSeconds );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_SetCloudFileNameFilter" )]
        internal static extern bool SteamAPI_ISteamUGC_SetCloudFileNameFilter( IntPtr instancePtr, ulong handle, string pMatchCloudFileName );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_SetMatchAnyTag" )]
        internal static extern bool SteamAPI_ISteamUGC_SetMatchAnyTag( IntPtr instancePtr, ulong handle, bool bMatchAnyTag );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_SetSearchText" )]
        internal static extern bool SteamAPI_ISteamUGC_SetSearchText( IntPtr instancePtr, ulong handle, string pSearchText );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_SetRankedByTrendDays" )]
        internal static extern bool SteamAPI_ISteamUGC_SetRankedByTrendDays( IntPtr instancePtr, ulong handle, uint unDays );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_AddRequiredKeyValueTag" )]
        internal static extern bool SteamAPI_ISteamUGC_AddRequiredKeyValueTag( IntPtr instancePtr, ulong handle, string pKey, string pValue );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_RequestUGCDetails" )]
        internal static extern ulong SteamAPI_ISteamUGC_RequestUGCDetails( IntPtr instancePtr, ulong nPublishedFileID, uint unMaxAgeSeconds );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_CreateItem" )]
        internal static extern ulong SteamAPI_ISteamUGC_CreateItem( IntPtr instancePtr, uint nConsumerAppId, uint eFileType );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_StartItemUpdate" )]
        internal static extern ulong SteamAPI_ISteamUGC_StartItemUpdate( IntPtr instancePtr, uint nConsumerAppId, ulong nPublishedFileID );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_SetItemTitle" )]
        internal static extern bool SteamAPI_ISteamUGC_SetItemTitle( IntPtr instancePtr, ulong handle, string pchTitle );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_SetItemDescription" )]
        internal static extern bool SteamAPI_ISteamUGC_SetItemDescription( IntPtr instancePtr, ulong handle, string pchDescription );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_SetItemUpdateLanguage" )]
        internal static extern bool SteamAPI_ISteamUGC_SetItemUpdateLanguage( IntPtr instancePtr, ulong handle, string pchLanguage );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_SetItemMetadata" )]
        internal static extern bool SteamAPI_ISteamUGC_SetItemMetadata( IntPtr instancePtr, ulong handle, string pchMetaData );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_SetItemVisibility" )]
        internal static extern bool SteamAPI_ISteamUGC_SetItemVisibility( IntPtr instancePtr, ulong handle, uint eVisibility );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_SetItemTags" )]
        internal static extern bool SteamAPI_ISteamUGC_SetItemTags( IntPtr instancePtr, ulong updateHandle, ref SteamParamStringArray_t pTags );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_SetItemContent" )]
        internal static extern bool SteamAPI_ISteamUGC_SetItemContent( IntPtr instancePtr, ulong handle, string pszContentFolder );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_SetItemPreview" )]
        internal static extern bool SteamAPI_ISteamUGC_SetItemPreview( IntPtr instancePtr, ulong handle, string pszPreviewFile );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_RemoveItemKeyValueTags" )]
        internal static extern bool SteamAPI_ISteamUGC_RemoveItemKeyValueTags( IntPtr instancePtr, ulong handle, string pchKey );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_AddItemKeyValueTag" )]
        internal static extern bool SteamAPI_ISteamUGC_AddItemKeyValueTag( IntPtr instancePtr, ulong handle, string pchKey, string pchValue );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_AddItemPreviewFile" )]
        internal static extern bool SteamAPI_ISteamUGC_AddItemPreviewFile( IntPtr instancePtr, ulong handle, string pszPreviewFile, uint type );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_AddItemPreviewVideo" )]
        internal static extern bool SteamAPI_ISteamUGC_AddItemPreviewVideo( IntPtr instancePtr, ulong handle, string pszVideoID );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_UpdateItemPreviewFile" )]
        internal static extern bool SteamAPI_ISteamUGC_UpdateItemPreviewFile( IntPtr instancePtr, ulong handle, uint index, string pszPreviewFile );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_UpdateItemPreviewVideo" )]
        internal static extern bool SteamAPI_ISteamUGC_UpdateItemPreviewVideo( IntPtr instancePtr, ulong handle, uint index, string pszVideoID );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_RemoveItemPreview" )]
        internal static extern bool SteamAPI_ISteamUGC_RemoveItemPreview( IntPtr instancePtr, ulong handle, uint index );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_SubmitItemUpdate" )]
        internal static extern ulong SteamAPI_ISteamUGC_SubmitItemUpdate( IntPtr instancePtr, ulong handle, string pchChangeNote );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_GetItemUpdateProgress" )]
        internal static extern uint SteamAPI_ISteamUGC_GetItemUpdateProgress( IntPtr instancePtr, ulong handle, ref ulong punBytesProcessed, ref ulong punBytesTotal );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_SetUserItemVote" )]
        internal static extern ulong SteamAPI_ISteamUGC_SetUserItemVote( IntPtr instancePtr, ulong nPublishedFileID, bool bVoteUp );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_GetUserItemVote" )]
        internal static extern ulong SteamAPI_ISteamUGC_GetUserItemVote( IntPtr instancePtr, ulong nPublishedFileID );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_AddItemToFavorites" )]
        internal static extern ulong SteamAPI_ISteamUGC_AddItemToFavorites( IntPtr instancePtr, uint nAppId, ulong nPublishedFileID );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_RemoveItemFromFavorites" )]
        internal static extern ulong SteamAPI_ISteamUGC_RemoveItemFromFavorites( IntPtr instancePtr, uint nAppId, ulong nPublishedFileID );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_SubscribeItem" )]
        internal static extern ulong SteamAPI_ISteamUGC_SubscribeItem( IntPtr instancePtr, ulong nPublishedFileID );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_UnsubscribeItem" )]
        internal static extern ulong SteamAPI_ISteamUGC_UnsubscribeItem( IntPtr instancePtr, ulong nPublishedFileID );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_GetNumSubscribedItems" )]
        internal static extern uint SteamAPI_ISteamUGC_GetNumSubscribedItems( IntPtr instancePtr );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_GetSubscribedItems" )]
        internal static extern uint SteamAPI_ISteamUGC_GetSubscribedItems( IntPtr instancePtr, ref ulong pvecPublishedFileID, uint cMaxEntries );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_GetItemState" )]
        internal static extern uint SteamAPI_ISteamUGC_GetItemState( IntPtr instancePtr, ulong nPublishedFileID );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_GetItemInstallInfo" )]
        internal static extern bool SteamAPI_ISteamUGC_GetItemInstallInfo( IntPtr instancePtr, ulong nPublishedFileID, ref ulong punSizeOnDisk, System.Text.StringBuilder pchFolder, uint cchFolderSize, ref uint punTimeStamp );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_GetItemDownloadInfo" )]
        internal static extern bool SteamAPI_ISteamUGC_GetItemDownloadInfo( IntPtr instancePtr, ulong nPublishedFileID, ref ulong punBytesDownloaded, ref ulong punBytesTotal );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_DownloadItem" )]
        internal static extern bool SteamAPI_ISteamUGC_DownloadItem( IntPtr instancePtr, ulong nPublishedFileID, bool bHighPriority );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_BInitWorkshopForGameServer" )]
        internal static extern bool SteamAPI_ISteamUGC_BInitWorkshopForGameServer( IntPtr instancePtr, uint unWorkshopDepotID, string pszFolder );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamUGC_SuspendDownloads" )]
        internal static extern void SteamAPI_ISteamUGC_SuspendDownloads( IntPtr instancePtr, bool bSuspend );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamAppList_GetNumInstalledApps" )]
        internal static extern uint SteamAPI_ISteamAppList_GetNumInstalledApps( IntPtr instancePtr );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamAppList_GetInstalledApps" )]
        internal static extern uint SteamAPI_ISteamAppList_GetInstalledApps( IntPtr instancePtr, ref uint pvecAppID, uint unMaxAppIDs );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamAppList_GetAppName" )]
        internal static extern int SteamAPI_ISteamAppList_GetAppName( IntPtr instancePtr, uint nAppID, System.Text.StringBuilder pchName, int cchNameMax );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamAppList_GetAppInstallDir" )]
        internal static extern int SteamAPI_ISteamAppList_GetAppInstallDir( IntPtr instancePtr, uint nAppID, string pchDirectory, int cchNameMax );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamAppList_GetAppBuildId" )]
        internal static extern int SteamAPI_ISteamAppList_GetAppBuildId( IntPtr instancePtr, uint nAppID );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamHTMLSurface_DestructISteamHTMLSurface" )]
        internal static extern void SteamAPI_ISteamHTMLSurface_DestructISteamHTMLSurface( IntPtr instancePtr );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamHTMLSurface_Init" )]
        internal static extern bool SteamAPI_ISteamHTMLSurface_Init( IntPtr instancePtr );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamHTMLSurface_Shutdown" )]
        internal static extern bool SteamAPI_ISteamHTMLSurface_Shutdown( IntPtr instancePtr );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamHTMLSurface_CreateBrowser" )]
        internal static extern ulong SteamAPI_ISteamHTMLSurface_CreateBrowser( IntPtr instancePtr, string pchUserAgent, string pchUserCSS );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamHTMLSurface_RemoveBrowser" )]
        internal static extern void SteamAPI_ISteamHTMLSurface_RemoveBrowser( IntPtr instancePtr, uint unBrowserHandle );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamHTMLSurface_LoadURL" )]
        internal static extern void SteamAPI_ISteamHTMLSurface_LoadURL( IntPtr instancePtr, uint unBrowserHandle, string pchURL, string pchPostData );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamHTMLSurface_SetSize" )]
        internal static extern void SteamAPI_ISteamHTMLSurface_SetSize( IntPtr instancePtr, uint unBrowserHandle, uint unWidth, uint unHeight );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamHTMLSurface_StopLoad" )]
        internal static extern void SteamAPI_ISteamHTMLSurface_StopLoad( IntPtr instancePtr, uint unBrowserHandle );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamHTMLSurface_Reload" )]
        internal static extern void SteamAPI_ISteamHTMLSurface_Reload( IntPtr instancePtr, uint unBrowserHandle );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamHTMLSurface_GoBack" )]
        internal static extern void SteamAPI_ISteamHTMLSurface_GoBack( IntPtr instancePtr, uint unBrowserHandle );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamHTMLSurface_GoForward" )]
        internal static extern void SteamAPI_ISteamHTMLSurface_GoForward( IntPtr instancePtr, uint unBrowserHandle );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamHTMLSurface_AddHeader" )]
        internal static extern void SteamAPI_ISteamHTMLSurface_AddHeader( IntPtr instancePtr, uint unBrowserHandle, string pchKey, string pchValue );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamHTMLSurface_ExecuteJavascript" )]
        internal static extern void SteamAPI_ISteamHTMLSurface_ExecuteJavascript( IntPtr instancePtr, uint unBrowserHandle, string pchScript );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamHTMLSurface_MouseUp" )]
        internal static extern void SteamAPI_ISteamHTMLSurface_MouseUp( IntPtr instancePtr, uint unBrowserHandle, uint eMouseButton );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamHTMLSurface_MouseDown" )]
        internal static extern void SteamAPI_ISteamHTMLSurface_MouseDown( IntPtr instancePtr, uint unBrowserHandle, uint eMouseButton );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamHTMLSurface_MouseDoubleClick" )]
        internal static extern void SteamAPI_ISteamHTMLSurface_MouseDoubleClick( IntPtr instancePtr, uint unBrowserHandle, uint eMouseButton );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamHTMLSurface_MouseMove" )]
        internal static extern void SteamAPI_ISteamHTMLSurface_MouseMove( IntPtr instancePtr, uint unBrowserHandle, int x, int y );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamHTMLSurface_MouseWheel" )]
        internal static extern void SteamAPI_ISteamHTMLSurface_MouseWheel( IntPtr instancePtr, uint unBrowserHandle, int nDelta );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamHTMLSurface_KeyDown" )]
        internal static extern void SteamAPI_ISteamHTMLSurface_KeyDown( IntPtr instancePtr, uint unBrowserHandle, uint nNativeKeyCode, uint eHTMLKeyModifiers );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamHTMLSurface_KeyUp" )]
        internal static extern void SteamAPI_ISteamHTMLSurface_KeyUp( IntPtr instancePtr, uint unBrowserHandle, uint nNativeKeyCode, uint eHTMLKeyModifiers );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamHTMLSurface_KeyChar" )]
        internal static extern void SteamAPI_ISteamHTMLSurface_KeyChar( IntPtr instancePtr, uint unBrowserHandle, uint cUnicodeChar, uint eHTMLKeyModifiers );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamHTMLSurface_SetHorizontalScroll" )]
        internal static extern void SteamAPI_ISteamHTMLSurface_SetHorizontalScroll( IntPtr instancePtr, uint unBrowserHandle, uint nAbsolutePixelScroll );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamHTMLSurface_SetVerticalScroll" )]
        internal static extern void SteamAPI_ISteamHTMLSurface_SetVerticalScroll( IntPtr instancePtr, uint unBrowserHandle, uint nAbsolutePixelScroll );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamHTMLSurface_SetKeyFocus" )]
        internal static extern void SteamAPI_ISteamHTMLSurface_SetKeyFocus( IntPtr instancePtr, uint unBrowserHandle, bool bHasKeyFocus );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamHTMLSurface_ViewSource" )]
        internal static extern void SteamAPI_ISteamHTMLSurface_ViewSource( IntPtr instancePtr, uint unBrowserHandle );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamHTMLSurface_CopyToClipboard" )]
        internal static extern void SteamAPI_ISteamHTMLSurface_CopyToClipboard( IntPtr instancePtr, uint unBrowserHandle );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamHTMLSurface_PasteFromClipboard" )]
        internal static extern void SteamAPI_ISteamHTMLSurface_PasteFromClipboard( IntPtr instancePtr, uint unBrowserHandle );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamHTMLSurface_Find" )]
        internal static extern void SteamAPI_ISteamHTMLSurface_Find( IntPtr instancePtr, uint unBrowserHandle, string pchSearchStr, bool bCurrentlyInFind, bool bReverse );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamHTMLSurface_StopFind" )]
        internal static extern void SteamAPI_ISteamHTMLSurface_StopFind( IntPtr instancePtr, uint unBrowserHandle );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamHTMLSurface_GetLinkAtPosition" )]
        internal static extern void SteamAPI_ISteamHTMLSurface_GetLinkAtPosition( IntPtr instancePtr, uint unBrowserHandle, int x, int y );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamHTMLSurface_SetCookie" )]
        internal static extern void SteamAPI_ISteamHTMLSurface_SetCookie( IntPtr instancePtr, string pchHostname, string pchKey, string pchValue, string pchPath, ulong nExpires, bool bSecure, bool bHTTPOnly );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamHTMLSurface_SetPageScaleFactor" )]
        internal static extern void SteamAPI_ISteamHTMLSurface_SetPageScaleFactor( IntPtr instancePtr, uint unBrowserHandle, float flZoom, int nPointX, int nPointY );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamHTMLSurface_SetBackgroundMode" )]
        internal static extern void SteamAPI_ISteamHTMLSurface_SetBackgroundMode( IntPtr instancePtr, uint unBrowserHandle, bool bBackgroundMode );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamHTMLSurface_AllowStartRequest" )]
        internal static extern void SteamAPI_ISteamHTMLSurface_AllowStartRequest( IntPtr instancePtr, uint unBrowserHandle, bool bAllowed );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamHTMLSurface_JSDialogResponse" )]
        internal static extern void SteamAPI_ISteamHTMLSurface_JSDialogResponse( IntPtr instancePtr, uint unBrowserHandle, bool bResult );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamInventory_GetResultStatus" )]
        internal static extern uint SteamAPI_ISteamInventory_GetResultStatus( IntPtr instancePtr, int resultHandle );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamInventory_GetResultItems" )]
        internal static extern bool SteamAPI_ISteamInventory_GetResultItems( IntPtr instancePtr, int resultHandle, [In, Out] SteamItemDetails_t[] pOutItemsArray, ref uint punOutItemsArraySize );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamInventory_GetResultTimestamp" )]
        internal static extern uint SteamAPI_ISteamInventory_GetResultTimestamp( IntPtr instancePtr, int resultHandle );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamInventory_CheckResultSteamID" )]
        internal static extern bool SteamAPI_ISteamInventory_CheckResultSteamID( IntPtr instancePtr, int resultHandle, ulong steamIDExpected );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamInventory_DestroyResult" )]
        internal static extern void SteamAPI_ISteamInventory_DestroyResult( IntPtr instancePtr, int resultHandle );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamInventory_GetAllItems" )]
        internal static extern bool SteamAPI_ISteamInventory_GetAllItems( IntPtr instancePtr, ref int pResultHandle );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamInventory_GetItemsByID" )]
        internal static extern bool SteamAPI_ISteamInventory_GetItemsByID( IntPtr instancePtr, ref int pResultHandle, [In, Out] ulong[] pInstanceIDs, uint unCountInstanceIDs );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamInventory_SerializeResult" )]
        internal static extern bool SteamAPI_ISteamInventory_SerializeResult( IntPtr instancePtr, int resultHandle, IntPtr pOutBuffer, ref uint punOutBufferSize );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamInventory_DeserializeResult" )]
        internal static extern bool SteamAPI_ISteamInventory_DeserializeResult( IntPtr instancePtr, ref int pOutResultHandle, IntPtr pBuffer, uint unBufferSize, bool bRESERVED_MUST_BE_FALSE );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamInventory_GenerateItems" )]
        internal static extern bool SteamAPI_ISteamInventory_GenerateItems( IntPtr instancePtr, ref int pResultHandle, [In, Out] int[] pArrayItemDefs, [In, Out] uint[] punArrayQuantity, uint unArrayLength );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamInventory_GrantPromoItems" )]
        internal static extern bool SteamAPI_ISteamInventory_GrantPromoItems( IntPtr instancePtr, ref int pResultHandle );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamInventory_AddPromoItem" )]
        internal static extern bool SteamAPI_ISteamInventory_AddPromoItem( IntPtr instancePtr, ref int pResultHandle, int itemDef );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamInventory_AddPromoItems" )]
        internal static extern bool SteamAPI_ISteamInventory_AddPromoItems( IntPtr instancePtr, ref int pResultHandle, [In, Out] int[] pArrayItemDefs, uint unArrayLength );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamInventory_ConsumeItem" )]
        internal static extern bool SteamAPI_ISteamInventory_ConsumeItem( IntPtr instancePtr, ref int pResultHandle, ulong itemConsume, uint unQuantity );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamInventory_ExchangeItems" )]
        internal static extern bool SteamAPI_ISteamInventory_ExchangeItems( IntPtr instancePtr, ref int pResultHandle, [In, Out] int[] pArrayGenerate, [In, Out] uint[] punArrayGenerateQuantity, uint unArrayGenerateLength, [In, Out] ulong[] pArrayDestroy, [In, Out] uint[] punArrayDestroyQuantity, uint unArrayDestroyLength );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamInventory_TransferItemQuantity" )]
        internal static extern bool SteamAPI_ISteamInventory_TransferItemQuantity( IntPtr instancePtr, ref int pResultHandle, ulong itemIdSource, uint unQuantity, ulong itemIdDest );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamInventory_SendItemDropHeartbeat" )]
        internal static extern void SteamAPI_ISteamInventory_SendItemDropHeartbeat( IntPtr instancePtr );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamInventory_TriggerItemDrop" )]
        internal static extern bool SteamAPI_ISteamInventory_TriggerItemDrop( IntPtr instancePtr, ref int pResultHandle, int dropListDefinition );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamInventory_TradeItems" )]
        internal static extern bool SteamAPI_ISteamInventory_TradeItems( IntPtr instancePtr, ref int pResultHandle, ulong steamIDTradePartner, [In, Out] ulong[] pArrayGive, [In, Out] uint[] pArrayGiveQuantity, uint nArrayGiveLength, [In, Out] ulong[] pArrayGet, [In, Out] uint[] pArrayGetQuantity, uint nArrayGetLength );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamInventory_LoadItemDefinitions" )]
        internal static extern bool SteamAPI_ISteamInventory_LoadItemDefinitions( IntPtr instancePtr );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamInventory_GetItemDefinitionIDs" )]
        internal static extern bool SteamAPI_ISteamInventory_GetItemDefinitionIDs( IntPtr instancePtr, [In, Out] int[] pItemDefIDs, ref uint punItemDefIDsArraySize );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamInventory_GetItemDefinitionProperty" )]
        internal static extern bool SteamAPI_ISteamInventory_GetItemDefinitionProperty( IntPtr instancePtr, int iDefinition, string pchPropertyName, System.Text.StringBuilder pchValueBuffer, ref uint punValueBufferSize );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamVideo_GetVideoURL" )]
        internal static extern void SteamAPI_ISteamVideo_GetVideoURL( IntPtr instancePtr, uint unVideoAppID );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamVideo_IsBroadcasting" )]
        internal static extern bool SteamAPI_ISteamVideo_IsBroadcasting( IntPtr instancePtr, ref int pnNumViewers );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamGameServer_InitGameServer" )]
        internal static extern bool SteamAPI_ISteamGameServer_InitGameServer( IntPtr instancePtr, uint unIP, char usGamePort, char usQueryPort, uint unFlags, uint nGameAppId, string pchVersionString );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamGameServer_SetProduct" )]
        internal static extern void SteamAPI_ISteamGameServer_SetProduct( IntPtr instancePtr, string pszProduct );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamGameServer_SetGameDescription" )]
        internal static extern void SteamAPI_ISteamGameServer_SetGameDescription( IntPtr instancePtr, string pszGameDescription );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamGameServer_SetModDir" )]
        internal static extern void SteamAPI_ISteamGameServer_SetModDir( IntPtr instancePtr, string pszModDir );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamGameServer_SetDedicatedServer" )]
        internal static extern void SteamAPI_ISteamGameServer_SetDedicatedServer( IntPtr instancePtr, bool bDedicated );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamGameServer_LogOn" )]
        internal static extern void SteamAPI_ISteamGameServer_LogOn( IntPtr instancePtr, string pszToken );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamGameServer_LogOnAnonymous" )]
        internal static extern void SteamAPI_ISteamGameServer_LogOnAnonymous( IntPtr instancePtr );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamGameServer_LogOff" )]
        internal static extern void SteamAPI_ISteamGameServer_LogOff( IntPtr instancePtr );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamGameServer_BLoggedOn" )]
        internal static extern bool SteamAPI_ISteamGameServer_BLoggedOn( IntPtr instancePtr );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamGameServer_BSecure" )]
        internal static extern bool SteamAPI_ISteamGameServer_BSecure( IntPtr instancePtr );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamGameServer_GetSteamID" )]
        internal static extern ulong SteamAPI_ISteamGameServer_GetSteamID( IntPtr instancePtr );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamGameServer_WasRestartRequested" )]
        internal static extern bool SteamAPI_ISteamGameServer_WasRestartRequested( IntPtr instancePtr );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamGameServer_SetMaxPlayerCount" )]
        internal static extern void SteamAPI_ISteamGameServer_SetMaxPlayerCount( IntPtr instancePtr, int cPlayersMax );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamGameServer_SetBotPlayerCount" )]
        internal static extern void SteamAPI_ISteamGameServer_SetBotPlayerCount( IntPtr instancePtr, int cBotplayers );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamGameServer_SetServerName" )]
        internal static extern void SteamAPI_ISteamGameServer_SetServerName( IntPtr instancePtr, string pszServerName );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamGameServer_SetMapName" )]
        internal static extern void SteamAPI_ISteamGameServer_SetMapName( IntPtr instancePtr, string pszMapName );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamGameServer_SetPasswordProtected" )]
        internal static extern void SteamAPI_ISteamGameServer_SetPasswordProtected( IntPtr instancePtr, bool bPasswordProtected );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamGameServer_SetSpectatorPort" )]
        internal static extern void SteamAPI_ISteamGameServer_SetSpectatorPort( IntPtr instancePtr, char unSpectatorPort );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamGameServer_SetSpectatorServerName" )]
        internal static extern void SteamAPI_ISteamGameServer_SetSpectatorServerName( IntPtr instancePtr, string pszSpectatorServerName );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamGameServer_ClearAllKeyValues" )]
        internal static extern void SteamAPI_ISteamGameServer_ClearAllKeyValues( IntPtr instancePtr );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamGameServer_SetKeyValue" )]
        internal static extern void SteamAPI_ISteamGameServer_SetKeyValue( IntPtr instancePtr, string pKey, string pValue );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamGameServer_SetGameTags" )]
        internal static extern void SteamAPI_ISteamGameServer_SetGameTags( IntPtr instancePtr, string pchGameTags );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamGameServer_SetGameData" )]
        internal static extern void SteamAPI_ISteamGameServer_SetGameData( IntPtr instancePtr, string pchGameData );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamGameServer_SetRegion" )]
        internal static extern void SteamAPI_ISteamGameServer_SetRegion( IntPtr instancePtr, string pszRegion );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamGameServer_SendUserConnectAndAuthenticate" )]
        internal static extern bool SteamAPI_ISteamGameServer_SendUserConnectAndAuthenticate( IntPtr instancePtr, uint unIPClient, IntPtr pvAuthBlob, uint cubAuthBlobSize, ref CSteamID pSteamIDUser );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamGameServer_CreateUnauthenticatedUserConnection" )]
        internal static extern ulong SteamAPI_ISteamGameServer_CreateUnauthenticatedUserConnection( IntPtr instancePtr );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamGameServer_SendUserDisconnect" )]
        internal static extern void SteamAPI_ISteamGameServer_SendUserDisconnect( IntPtr instancePtr, ulong steamIDUser );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamGameServer_BUpdateUserData" )]
        internal static extern bool SteamAPI_ISteamGameServer_BUpdateUserData( IntPtr instancePtr, ulong steamIDUser, string pchPlayerName, uint uScore );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamGameServer_GetAuthSessionTicket" )]
        internal static extern uint SteamAPI_ISteamGameServer_GetAuthSessionTicket( IntPtr instancePtr, IntPtr pTicket, int cbMaxTicket, ref uint pcbTicket );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamGameServer_BeginAuthSession" )]
        internal static extern uint SteamAPI_ISteamGameServer_BeginAuthSession( IntPtr instancePtr, IntPtr pAuthTicket, int cbAuthTicket, ulong steamID );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamGameServer_EndAuthSession" )]
        internal static extern void SteamAPI_ISteamGameServer_EndAuthSession( IntPtr instancePtr, ulong steamID );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamGameServer_CancelAuthTicket" )]
        internal static extern void SteamAPI_ISteamGameServer_CancelAuthTicket( IntPtr instancePtr, uint hAuthTicket );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamGameServer_UserHasLicenseForApp" )]
        internal static extern uint SteamAPI_ISteamGameServer_UserHasLicenseForApp( IntPtr instancePtr, ulong steamID, uint appID );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamGameServer_RequestUserGroupStatus" )]
        internal static extern bool SteamAPI_ISteamGameServer_RequestUserGroupStatus( IntPtr instancePtr, ulong steamIDUser, ulong steamIDGroup );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamGameServer_GetGameplayStats" )]
        internal static extern void SteamAPI_ISteamGameServer_GetGameplayStats( IntPtr instancePtr );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamGameServer_GetServerReputation" )]
        internal static extern ulong SteamAPI_ISteamGameServer_GetServerReputation( IntPtr instancePtr );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamGameServer_GetPublicIP" )]
        internal static extern uint SteamAPI_ISteamGameServer_GetPublicIP( IntPtr instancePtr );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamGameServer_HandleIncomingPacket" )]
        internal static extern bool SteamAPI_ISteamGameServer_HandleIncomingPacket( IntPtr instancePtr, IntPtr pData, int cbData, uint srcIP, char srcPort );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamGameServer_GetNextOutgoingPacket" )]
        internal static extern int SteamAPI_ISteamGameServer_GetNextOutgoingPacket( IntPtr instancePtr, IntPtr pOut, int cbMaxOut, ref uint pNetAdr, ref char pPort );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamGameServer_EnableHeartbeats" )]
        internal static extern void SteamAPI_ISteamGameServer_EnableHeartbeats( IntPtr instancePtr, bool bActive );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamGameServer_SetHeartbeatInterval" )]
        internal static extern void SteamAPI_ISteamGameServer_SetHeartbeatInterval( IntPtr instancePtr, int iHeartbeatInterval );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamGameServer_ForceHeartbeat" )]
        internal static extern void SteamAPI_ISteamGameServer_ForceHeartbeat( IntPtr instancePtr );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamGameServer_AssociateWithClan" )]
        internal static extern ulong SteamAPI_ISteamGameServer_AssociateWithClan( IntPtr instancePtr, ulong steamIDClan );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamGameServer_ComputeNewPlayerCompatibility" )]
        internal static extern ulong SteamAPI_ISteamGameServer_ComputeNewPlayerCompatibility( IntPtr instancePtr, ulong steamIDNewPlayer );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamGameServerStats_RequestUserStats" )]
        internal static extern ulong SteamAPI_ISteamGameServerStats_RequestUserStats( IntPtr instancePtr, ulong steamIDUser );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamGameServerStats_GetUserStat" )]
        internal static extern bool SteamAPI_ISteamGameServerStats_GetUserStat( IntPtr instancePtr, ulong steamIDUser, string pchName, ref int pData );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamGameServerStats_GetUserStat0" )]
        internal static extern bool SteamAPI_ISteamGameServerStats_GetUserStat0( IntPtr instancePtr, ulong steamIDUser, string pchName, ref float pData );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamGameServerStats_GetUserAchievement" )]
        internal static extern bool SteamAPI_ISteamGameServerStats_GetUserAchievement( IntPtr instancePtr, ulong steamIDUser, string pchName, ref bool pbAchieved );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamGameServerStats_SetUserStat" )]
        internal static extern bool SteamAPI_ISteamGameServerStats_SetUserStat( IntPtr instancePtr, ulong steamIDUser, string pchName, int nData );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamGameServerStats_SetUserStat0" )]
        internal static extern bool SteamAPI_ISteamGameServerStats_SetUserStat0( IntPtr instancePtr, ulong steamIDUser, string pchName, float fData );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamGameServerStats_UpdateUserAvgRateStat" )]
        internal static extern bool SteamAPI_ISteamGameServerStats_UpdateUserAvgRateStat( IntPtr instancePtr, ulong steamIDUser, string pchName, float flCountThisSession, double dSessionLength );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamGameServerStats_SetUserAchievement" )]
        internal static extern bool SteamAPI_ISteamGameServerStats_SetUserAchievement( IntPtr instancePtr, ulong steamIDUser, string pchName );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamGameServerStats_ClearUserAchievement" )]
        internal static extern bool SteamAPI_ISteamGameServerStats_ClearUserAchievement( IntPtr instancePtr, ulong steamIDUser, string pchName );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamGameServerStats_StoreUserStats" )]
        internal static extern ulong SteamAPI_ISteamGameServerStats_StoreUserStats( IntPtr instancePtr, ulong steamIDUser );
        public delegate void SteamAPI_UserStatsReceived_t_Callback( UserStatsReceived_t pUserStatsReceived_t );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "CUserStatsReceived_t_SetCallback" )]
        public static extern ulong CUserStatsReceived_t_SetCallback( SteamAPI_UserStatsReceived_t_Callback func );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "CUserStatsReceived_t_RemoveCallback" )]
        public static extern ulong CUserStatsReceived_t_RemoveCallback( ulong handle );
        public delegate void SteamAPI_RemoteStorageFileReadAsyncComplete_t_CallResult( RemoteStorageFileReadAsyncComplete_t pRemoteStorageFileReadAsyncComplete_t, bool bIOFailure );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "CRemoteStorageFileReadAsyncComplete_t_SetCallResult" )]
        public static extern ulong CRemoteStorageFileReadAsyncComplete_t_SetCallResult( ulong hAPICall, SteamAPI_RemoteStorageFileReadAsyncComplete_t_CallResult func );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "CRemoteStorageFileReadAsyncComplete_t_RemoveCallResult" )]
        public static extern ulong CRemoteStorageFileReadAsyncComplete_t_RemoveCallResult( ulong handle );
        public delegate void SteamAPI_RemoteStorageGetPublishedItemVoteDetailsResult_t_CallResult( RemoteStorageGetPublishedItemVoteDetailsResult_t pRemoteStorageGetPublishedItemVoteDetailsResult_t, bool bIOFailure );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "CRemoteStorageGetPublishedItemVoteDetailsResult_t_SetCallResult" )]
        public static extern ulong CRemoteStorageGetPublishedItemVoteDetailsResult_t_SetCallResult( ulong hAPICall, SteamAPI_RemoteStorageGetPublishedItemVoteDetailsResult_t_CallResult func );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "CRemoteStorageGetPublishedItemVoteDetailsResult_t_RemoveCallResult" )]
        public static extern ulong CRemoteStorageGetPublishedItemVoteDetailsResult_t_RemoveCallResult( ulong handle );
        public delegate void SteamAPI_GSStatsStored_t_CallResult( GSStatsStored_t pGSStatsStored_t, bool bIOFailure );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "CGSStatsStored_t_SetCallResult" )]
        public static extern ulong CGSStatsStored_t_SetCallResult( ulong hAPICall, SteamAPI_GSStatsStored_t_CallResult func );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "CGSStatsStored_t_RemoveCallResult" )]
        public static extern ulong CGSStatsStored_t_RemoveCallResult( ulong handle );
        public delegate void SteamAPI_HTML_BrowserReady_t_CallResult( HTML_BrowserReady_t pHTML_BrowserReady_t, bool bIOFailure );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "CHTML_BrowserReady_t_SetCallResult" )]
        public static extern ulong CHTML_BrowserReady_t_SetCallResult( ulong hAPICall, SteamAPI_HTML_BrowserReady_t_CallResult func );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "CHTML_BrowserReady_t_RemoveCallResult" )]
        public static extern ulong CHTML_BrowserReady_t_RemoveCallResult( ulong handle );
        public delegate void SteamAPI_FriendsGetFollowerCount_t_CallResult( FriendsGetFollowerCount_t pFriendsGetFollowerCount_t, bool bIOFailure );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "CFriendsGetFollowerCount_t_SetCallResult" )]
        public static extern ulong CFriendsGetFollowerCount_t_SetCallResult( ulong hAPICall, SteamAPI_FriendsGetFollowerCount_t_CallResult func );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "CFriendsGetFollowerCount_t_RemoveCallResult" )]
        public static extern ulong CFriendsGetFollowerCount_t_RemoveCallResult( ulong handle );
        public delegate void SteamAPI_FriendsIsFollowing_t_CallResult( FriendsIsFollowing_t pFriendsIsFollowing_t, bool bIOFailure );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "CFriendsIsFollowing_t_SetCallResult" )]
        public static extern ulong CFriendsIsFollowing_t_SetCallResult( ulong hAPICall, SteamAPI_FriendsIsFollowing_t_CallResult func );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "CFriendsIsFollowing_t_RemoveCallResult" )]
        public static extern ulong CFriendsIsFollowing_t_RemoveCallResult( ulong handle );
        public delegate void SteamAPI_LobbyMatchList_t_CallResult( LobbyMatchList_t pLobbyMatchList_t, bool bIOFailure );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "CLobbyMatchList_t_SetCallResult" )]
        public static extern ulong CLobbyMatchList_t_SetCallResult( ulong hAPICall, SteamAPI_LobbyMatchList_t_CallResult func );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "CLobbyMatchList_t_RemoveCallResult" )]
        public static extern ulong CLobbyMatchList_t_RemoveCallResult( ulong handle );
        public delegate void SteamAPI_RemoteStorageUpdatePublishedFileResult_t_CallResult( RemoteStorageUpdatePublishedFileResult_t pRemoteStorageUpdatePublishedFileResult_t, bool bIOFailure );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "CRemoteStorageUpdatePublishedFileResult_t_SetCallResult" )]
        public static extern ulong CRemoteStorageUpdatePublishedFileResult_t_SetCallResult( ulong hAPICall, SteamAPI_RemoteStorageUpdatePublishedFileResult_t_CallResult func );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "CRemoteStorageUpdatePublishedFileResult_t_RemoveCallResult" )]
        public static extern ulong CRemoteStorageUpdatePublishedFileResult_t_RemoveCallResult( ulong handle );
        public delegate void SteamAPI_StoreAuthURLResponse_t_CallResult( StoreAuthURLResponse_t pStoreAuthURLResponse_t, bool bIOFailure );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "CStoreAuthURLResponse_t_SetCallResult" )]
        public static extern ulong CStoreAuthURLResponse_t_SetCallResult( ulong hAPICall, SteamAPI_StoreAuthURLResponse_t_CallResult func );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "CStoreAuthURLResponse_t_RemoveCallResult" )]
        public static extern ulong CStoreAuthURLResponse_t_RemoveCallResult( ulong handle );
        public delegate void SteamAPI_LobbyCreated_t_CallResult( LobbyCreated_t pLobbyCreated_t, bool bIOFailure );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "CLobbyCreated_t_SetCallResult" )]
        public static extern ulong CLobbyCreated_t_SetCallResult( ulong hAPICall, SteamAPI_LobbyCreated_t_CallResult func );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "CLobbyCreated_t_RemoveCallResult" )]
        public static extern ulong CLobbyCreated_t_RemoveCallResult( ulong handle );
        public delegate void SteamAPI_RemoteStorageFileWriteAsyncComplete_t_CallResult( RemoteStorageFileWriteAsyncComplete_t pRemoteStorageFileWriteAsyncComplete_t, bool bIOFailure );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "CRemoteStorageFileWriteAsyncComplete_t_SetCallResult" )]
        public static extern ulong CRemoteStorageFileWriteAsyncComplete_t_SetCallResult( ulong hAPICall, SteamAPI_RemoteStorageFileWriteAsyncComplete_t_CallResult func );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "CRemoteStorageFileWriteAsyncComplete_t_RemoveCallResult" )]
        public static extern ulong CRemoteStorageFileWriteAsyncComplete_t_RemoveCallResult( ulong handle );
        public delegate void SteamAPI_RemoteStorageDeletePublishedFileResult_t_CallResult( RemoteStorageDeletePublishedFileResult_t pRemoteStorageDeletePublishedFileResult_t, bool bIOFailure );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "CRemoteStorageDeletePublishedFileResult_t_SetCallResult" )]
        public static extern ulong CRemoteStorageDeletePublishedFileResult_t_SetCallResult( ulong hAPICall, SteamAPI_RemoteStorageDeletePublishedFileResult_t_CallResult func );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "CRemoteStorageDeletePublishedFileResult_t_RemoveCallResult" )]
        public static extern ulong CRemoteStorageDeletePublishedFileResult_t_RemoveCallResult( ulong handle );
        public delegate void SteamAPI_RemoteStorageGetPublishedFileDetailsResult_t_CallResult( RemoteStorageGetPublishedFileDetailsResult_t pRemoteStorageGetPublishedFileDetailsResult_t, bool bIOFailure );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "CRemoteStorageGetPublishedFileDetailsResult_t_SetCallResult" )]
        public static extern ulong CRemoteStorageGetPublishedFileDetailsResult_t_SetCallResult( ulong hAPICall, SteamAPI_RemoteStorageGetPublishedFileDetailsResult_t_CallResult func );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "CRemoteStorageGetPublishedFileDetailsResult_t_RemoveCallResult" )]
        public static extern ulong CRemoteStorageGetPublishedFileDetailsResult_t_RemoveCallResult( ulong handle );
        public delegate void SteamAPI_RemoteStorageDownloadUGCResult_t_CallResult( RemoteStorageDownloadUGCResult_t pRemoteStorageDownloadUGCResult_t, bool bIOFailure );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "CRemoteStorageDownloadUGCResult_t_SetCallResult" )]
        public static extern ulong CRemoteStorageDownloadUGCResult_t_SetCallResult( ulong hAPICall, SteamAPI_RemoteStorageDownloadUGCResult_t_CallResult func );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "CRemoteStorageDownloadUGCResult_t_RemoveCallResult" )]
        public static extern ulong CRemoteStorageDownloadUGCResult_t_RemoveCallResult( ulong handle );
        public delegate void SteamAPI_SteamUGCQueryCompleted_t_CallResult( SteamUGCQueryCompleted_t pSteamUGCQueryCompleted_t, bool bIOFailure );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "CSteamUGCQueryCompleted_t_SetCallResult" )]
        public static extern ulong CSteamUGCQueryCompleted_t_SetCallResult( ulong hAPICall, SteamAPI_SteamUGCQueryCompleted_t_CallResult func );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "CSteamUGCQueryCompleted_t_RemoveCallResult" )]
        public static extern ulong CSteamUGCQueryCompleted_t_RemoveCallResult( ulong handle );
        public delegate void SteamAPI_RemoteStorageFileShareResult_t_CallResult( RemoteStorageFileShareResult_t pRemoteStorageFileShareResult_t, bool bIOFailure );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "CRemoteStorageFileShareResult_t_SetCallResult" )]
        public static extern ulong CRemoteStorageFileShareResult_t_SetCallResult( ulong hAPICall, SteamAPI_RemoteStorageFileShareResult_t_CallResult func );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "CRemoteStorageFileShareResult_t_RemoveCallResult" )]
        public static extern ulong CRemoteStorageFileShareResult_t_RemoveCallResult( ulong handle );
        public delegate void SteamAPI_LobbyEnter_t_CallResult( LobbyEnter_t pLobbyEnter_t, bool bIOFailure );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "CLobbyEnter_t_SetCallResult" )]
        public static extern ulong CLobbyEnter_t_SetCallResult( ulong hAPICall, SteamAPI_LobbyEnter_t_CallResult func );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "CLobbyEnter_t_RemoveCallResult" )]
        public static extern ulong CLobbyEnter_t_RemoveCallResult( ulong handle );
        public delegate void SteamAPI_SubmitItemUpdateResult_t_CallResult( SubmitItemUpdateResult_t pSubmitItemUpdateResult_t, bool bIOFailure );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "CSubmitItemUpdateResult_t_SetCallResult" )]
        public static extern ulong CSubmitItemUpdateResult_t_SetCallResult( ulong hAPICall, SteamAPI_SubmitItemUpdateResult_t_CallResult func );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "CSubmitItemUpdateResult_t_RemoveCallResult" )]
        public static extern ulong CSubmitItemUpdateResult_t_RemoveCallResult( ulong handle );
        public delegate void SteamAPI_NumberOfCurrentPlayers_t_CallResult( NumberOfCurrentPlayers_t pNumberOfCurrentPlayers_t, bool bIOFailure );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "CNumberOfCurrentPlayers_t_SetCallResult" )]
        public static extern ulong CNumberOfCurrentPlayers_t_SetCallResult( ulong hAPICall, SteamAPI_NumberOfCurrentPlayers_t_CallResult func );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "CNumberOfCurrentPlayers_t_RemoveCallResult" )]
        public static extern ulong CNumberOfCurrentPlayers_t_RemoveCallResult( ulong handle );
        public delegate void SteamAPI_GSStatsReceived_t_CallResult( GSStatsReceived_t pGSStatsReceived_t, bool bIOFailure );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "CGSStatsReceived_t_SetCallResult" )]
        public static extern ulong CGSStatsReceived_t_SetCallResult( ulong hAPICall, SteamAPI_GSStatsReceived_t_CallResult func );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "CGSStatsReceived_t_RemoveCallResult" )]
        public static extern ulong CGSStatsReceived_t_RemoveCallResult( ulong handle );
        public delegate void SteamAPI_LeaderboardScoresDownloaded_t_CallResult( LeaderboardScoresDownloaded_t pLeaderboardScoresDownloaded_t, bool bIOFailure );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "CLeaderboardScoresDownloaded_t_SetCallResult" )]
        public static extern ulong CLeaderboardScoresDownloaded_t_SetCallResult( ulong hAPICall, SteamAPI_LeaderboardScoresDownloaded_t_CallResult func );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "CLeaderboardScoresDownloaded_t_RemoveCallResult" )]
        public static extern ulong CLeaderboardScoresDownloaded_t_RemoveCallResult( ulong handle );
        public delegate void SteamAPI_RemoteStorageUpdateUserPublishedItemVoteResult_t_CallResult( RemoteStorageUpdateUserPublishedItemVoteResult_t pRemoteStorageUpdateUserPublishedItemVoteResult_t, bool bIOFailure );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "CRemoteStorageUpdateUserPublishedItemVoteResult_t_SetCallResult" )]
        public static extern ulong CRemoteStorageUpdateUserPublishedItemVoteResult_t_SetCallResult( ulong hAPICall, SteamAPI_RemoteStorageUpdateUserPublishedItemVoteResult_t_CallResult func );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "CRemoteStorageUpdateUserPublishedItemVoteResult_t_RemoveCallResult" )]
        public static extern ulong CRemoteStorageUpdateUserPublishedItemVoteResult_t_RemoveCallResult( ulong handle );
        public delegate void SteamAPI_RemoteStorageEnumerateUserSubscribedFilesResult_t_CallResult( RemoteStorageEnumerateUserSubscribedFilesResult_t pRemoteStorageEnumerateUserSubscribedFilesResult_t, bool bIOFailure );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "CRemoteStorageEnumerateUserSubscribedFilesResult_t_SetCallResult" )]
        public static extern ulong CRemoteStorageEnumerateUserSubscribedFilesResult_t_SetCallResult( ulong hAPICall, SteamAPI_RemoteStorageEnumerateUserSubscribedFilesResult_t_CallResult func );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "CRemoteStorageEnumerateUserSubscribedFilesResult_t_RemoveCallResult" )]
        public static extern ulong CRemoteStorageEnumerateUserSubscribedFilesResult_t_RemoveCallResult( ulong handle );
        public delegate void SteamAPI_CreateItemResult_t_CallResult( CreateItemResult_t pCreateItemResult_t, bool bIOFailure );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "CCreateItemResult_t_SetCallResult" )]
        public static extern ulong CCreateItemResult_t_SetCallResult( ulong hAPICall, SteamAPI_CreateItemResult_t_CallResult func );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "CCreateItemResult_t_RemoveCallResult" )]
        public static extern ulong CCreateItemResult_t_RemoveCallResult( ulong handle );
        public delegate void SteamAPI_SetUserItemVoteResult_t_CallResult( SetUserItemVoteResult_t pSetUserItemVoteResult_t, bool bIOFailure );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "CSetUserItemVoteResult_t_SetCallResult" )]
        public static extern ulong CSetUserItemVoteResult_t_SetCallResult( ulong hAPICall, SteamAPI_SetUserItemVoteResult_t_CallResult func );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "CSetUserItemVoteResult_t_RemoveCallResult" )]
        public static extern ulong CSetUserItemVoteResult_t_RemoveCallResult( ulong handle );
        public delegate void SteamAPI_ComputeNewPlayerCompatibilityResult_t_CallResult( ComputeNewPlayerCompatibilityResult_t pComputeNewPlayerCompatibilityResult_t, bool bIOFailure );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "CComputeNewPlayerCompatibilityResult_t_SetCallResult" )]
        public static extern ulong CComputeNewPlayerCompatibilityResult_t_SetCallResult( ulong hAPICall, SteamAPI_ComputeNewPlayerCompatibilityResult_t_CallResult func );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "CComputeNewPlayerCompatibilityResult_t_RemoveCallResult" )]
        public static extern ulong CComputeNewPlayerCompatibilityResult_t_RemoveCallResult( ulong handle );
        public delegate void SteamAPI_LeaderboardScoreUploaded_t_CallResult( LeaderboardScoreUploaded_t pLeaderboardScoreUploaded_t, bool bIOFailure );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "CLeaderboardScoreUploaded_t_SetCallResult" )]
        public static extern ulong CLeaderboardScoreUploaded_t_SetCallResult( ulong hAPICall, SteamAPI_LeaderboardScoreUploaded_t_CallResult func );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "CLeaderboardScoreUploaded_t_RemoveCallResult" )]
        public static extern ulong CLeaderboardScoreUploaded_t_RemoveCallResult( ulong handle );
        public delegate void SteamAPI_GlobalAchievementPercentagesReady_t_CallResult( GlobalAchievementPercentagesReady_t pGlobalAchievementPercentagesReady_t, bool bIOFailure );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "CGlobalAchievementPercentagesReady_t_SetCallResult" )]
        public static extern ulong CGlobalAchievementPercentagesReady_t_SetCallResult( ulong hAPICall, SteamAPI_GlobalAchievementPercentagesReady_t_CallResult func );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "CGlobalAchievementPercentagesReady_t_RemoveCallResult" )]
        public static extern ulong CGlobalAchievementPercentagesReady_t_RemoveCallResult( ulong handle );
        public delegate void SteamAPI_GlobalStatsReceived_t_CallResult( GlobalStatsReceived_t pGlobalStatsReceived_t, bool bIOFailure );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "CGlobalStatsReceived_t_SetCallResult" )]
        public static extern ulong CGlobalStatsReceived_t_SetCallResult( ulong hAPICall, SteamAPI_GlobalStatsReceived_t_CallResult func );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "CGlobalStatsReceived_t_RemoveCallResult" )]
        public static extern ulong CGlobalStatsReceived_t_RemoveCallResult( ulong handle );
        public delegate void SteamAPI_RemoteStorageEnumeratePublishedFilesByUserActionResult_t_CallResult( RemoteStorageEnumeratePublishedFilesByUserActionResult_t pRemoteStorageEnumeratePublishedFilesByUserActionResult_t, bool bIOFailure );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "CRemoteStorageEnumeratePublishedFilesByUserActionResult_t_SetCallResult" )]
        public static extern ulong CRemoteStorageEnumeratePublishedFilesByUserActionResult_t_SetCallResult( ulong hAPICall, SteamAPI_RemoteStorageEnumeratePublishedFilesByUserActionResult_t_CallResult func );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "CRemoteStorageEnumeratePublishedFilesByUserActionResult_t_RemoveCallResult" )]
        public static extern ulong CRemoteStorageEnumeratePublishedFilesByUserActionResult_t_RemoveCallResult( ulong handle );
        public delegate void SteamAPI_ClanOfficerListResponse_t_CallResult( ClanOfficerListResponse_t pClanOfficerListResponse_t, bool bIOFailure );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "CClanOfficerListResponse_t_SetCallResult" )]
        public static extern ulong CClanOfficerListResponse_t_SetCallResult( ulong hAPICall, SteamAPI_ClanOfficerListResponse_t_CallResult func );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "CClanOfficerListResponse_t_RemoveCallResult" )]
        public static extern ulong CClanOfficerListResponse_t_RemoveCallResult( ulong handle );
        public delegate void SteamAPI_RemoteStoragePublishFileProgress_t_CallResult( RemoteStoragePublishFileProgress_t pRemoteStoragePublishFileProgress_t, bool bIOFailure );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "CRemoteStoragePublishFileProgress_t_SetCallResult" )]
        public static extern ulong CRemoteStoragePublishFileProgress_t_SetCallResult( ulong hAPICall, SteamAPI_RemoteStoragePublishFileProgress_t_CallResult func );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "CRemoteStoragePublishFileProgress_t_RemoveCallResult" )]
        public static extern ulong CRemoteStoragePublishFileProgress_t_RemoveCallResult( ulong handle );
        public delegate void SteamAPI_RemoteStorageEnumerateWorkshopFilesResult_t_CallResult( RemoteStorageEnumerateWorkshopFilesResult_t pRemoteStorageEnumerateWorkshopFilesResult_t, bool bIOFailure );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "CRemoteStorageEnumerateWorkshopFilesResult_t_SetCallResult" )]
        public static extern ulong CRemoteStorageEnumerateWorkshopFilesResult_t_SetCallResult( ulong hAPICall, SteamAPI_RemoteStorageEnumerateWorkshopFilesResult_t_CallResult func );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "CRemoteStorageEnumerateWorkshopFilesResult_t_RemoveCallResult" )]
        public static extern ulong CRemoteStorageEnumerateWorkshopFilesResult_t_RemoveCallResult( ulong handle );
        public delegate void SteamAPI_GSReputation_t_CallResult( GSReputation_t pGSReputation_t, bool bIOFailure );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "CGSReputation_t_SetCallResult" )]
        public static extern ulong CGSReputation_t_SetCallResult( ulong hAPICall, SteamAPI_GSReputation_t_CallResult func );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "CGSReputation_t_RemoveCallResult" )]
        public static extern ulong CGSReputation_t_RemoveCallResult( ulong handle );
        public delegate void SteamAPI_UserFavoriteItemsListChanged_t_CallResult( UserFavoriteItemsListChanged_t pUserFavoriteItemsListChanged_t, bool bIOFailure );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "CUserFavoriteItemsListChanged_t_SetCallResult" )]
        public static extern ulong CUserFavoriteItemsListChanged_t_SetCallResult( ulong hAPICall, SteamAPI_UserFavoriteItemsListChanged_t_CallResult func );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "CUserFavoriteItemsListChanged_t_RemoveCallResult" )]
        public static extern ulong CUserFavoriteItemsListChanged_t_RemoveCallResult( ulong handle );
        public delegate void SteamAPI_EncryptedAppTicketResponse_t_CallResult( EncryptedAppTicketResponse_t pEncryptedAppTicketResponse_t, bool bIOFailure );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "CEncryptedAppTicketResponse_t_SetCallResult" )]
        public static extern ulong CEncryptedAppTicketResponse_t_SetCallResult( ulong hAPICall, SteamAPI_EncryptedAppTicketResponse_t_CallResult func );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "CEncryptedAppTicketResponse_t_RemoveCallResult" )]
        public static extern ulong CEncryptedAppTicketResponse_t_RemoveCallResult( ulong handle );
        public delegate void SteamAPI_RemoteStorageEnumerateUserPublishedFilesResult_t_CallResult( RemoteStorageEnumerateUserPublishedFilesResult_t pRemoteStorageEnumerateUserPublishedFilesResult_t, bool bIOFailure );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "CRemoteStorageEnumerateUserPublishedFilesResult_t_SetCallResult" )]
        public static extern ulong CRemoteStorageEnumerateUserPublishedFilesResult_t_SetCallResult( ulong hAPICall, SteamAPI_RemoteStorageEnumerateUserPublishedFilesResult_t_CallResult func );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "CRemoteStorageEnumerateUserPublishedFilesResult_t_RemoveCallResult" )]
        public static extern ulong CRemoteStorageEnumerateUserPublishedFilesResult_t_RemoveCallResult( ulong handle );
        public delegate void SteamAPI_FriendsEnumerateFollowingList_t_CallResult( FriendsEnumerateFollowingList_t pFriendsEnumerateFollowingList_t, bool bIOFailure );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "CFriendsEnumerateFollowingList_t_SetCallResult" )]
        public static extern ulong CFriendsEnumerateFollowingList_t_SetCallResult( ulong hAPICall, SteamAPI_FriendsEnumerateFollowingList_t_CallResult func );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "CFriendsEnumerateFollowingList_t_RemoveCallResult" )]
        public static extern ulong CFriendsEnumerateFollowingList_t_RemoveCallResult( ulong handle );
        public delegate void SteamAPI_RemoteStorageSubscribePublishedFileResult_t_CallResult( RemoteStorageSubscribePublishedFileResult_t pRemoteStorageSubscribePublishedFileResult_t, bool bIOFailure );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "CRemoteStorageSubscribePublishedFileResult_t_SetCallResult" )]
        public static extern ulong CRemoteStorageSubscribePublishedFileResult_t_SetCallResult( ulong hAPICall, SteamAPI_RemoteStorageSubscribePublishedFileResult_t_CallResult func );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "CRemoteStorageSubscribePublishedFileResult_t_RemoveCallResult" )]
        public static extern ulong CRemoteStorageSubscribePublishedFileResult_t_RemoveCallResult( ulong handle );
        public delegate void SteamAPI_GetUserItemVoteResult_t_CallResult( GetUserItemVoteResult_t pGetUserItemVoteResult_t, bool bIOFailure );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "CGetUserItemVoteResult_t_SetCallResult" )]
        public static extern ulong CGetUserItemVoteResult_t_SetCallResult( ulong hAPICall, SteamAPI_GetUserItemVoteResult_t_CallResult func );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "CGetUserItemVoteResult_t_RemoveCallResult" )]
        public static extern ulong CGetUserItemVoteResult_t_RemoveCallResult( ulong handle );
        public delegate void SteamAPI_AssociateWithClanResult_t_CallResult( AssociateWithClanResult_t pAssociateWithClanResult_t, bool bIOFailure );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "CAssociateWithClanResult_t_SetCallResult" )]
        public static extern ulong CAssociateWithClanResult_t_SetCallResult( ulong hAPICall, SteamAPI_AssociateWithClanResult_t_CallResult func );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "CAssociateWithClanResult_t_RemoveCallResult" )]
        public static extern ulong CAssociateWithClanResult_t_RemoveCallResult( ulong handle );
        public delegate void SteamAPI_LeaderboardUGCSet_t_CallResult( LeaderboardUGCSet_t pLeaderboardUGCSet_t, bool bIOFailure );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "CLeaderboardUGCSet_t_SetCallResult" )]
        public static extern ulong CLeaderboardUGCSet_t_SetCallResult( ulong hAPICall, SteamAPI_LeaderboardUGCSet_t_CallResult func );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "CLeaderboardUGCSet_t_RemoveCallResult" )]
        public static extern ulong CLeaderboardUGCSet_t_RemoveCallResult( ulong handle );
        public delegate void SteamAPI_CheckFileSignature_t_CallResult( CheckFileSignature_t pCheckFileSignature_t, bool bIOFailure );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "CCheckFileSignature_t_SetCallResult" )]
        public static extern ulong CCheckFileSignature_t_SetCallResult( ulong hAPICall, SteamAPI_CheckFileSignature_t_CallResult func );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "CCheckFileSignature_t_RemoveCallResult" )]
        public static extern ulong CCheckFileSignature_t_RemoveCallResult( ulong handle );
        public delegate void SteamAPI_RemoteStorageUnsubscribePublishedFileResult_t_CallResult( RemoteStorageUnsubscribePublishedFileResult_t pRemoteStorageUnsubscribePublishedFileResult_t, bool bIOFailure );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "CRemoteStorageUnsubscribePublishedFileResult_t_SetCallResult" )]
        public static extern ulong CRemoteStorageUnsubscribePublishedFileResult_t_SetCallResult( ulong hAPICall, SteamAPI_RemoteStorageUnsubscribePublishedFileResult_t_CallResult func );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "CRemoteStorageUnsubscribePublishedFileResult_t_RemoveCallResult" )]
        public static extern ulong CRemoteStorageUnsubscribePublishedFileResult_t_RemoveCallResult( ulong handle );
        public delegate void SteamAPI_SetPersonaNameResponse_t_CallResult( SetPersonaNameResponse_t pSetPersonaNameResponse_t, bool bIOFailure );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "CSetPersonaNameResponse_t_SetCallResult" )]
        public static extern ulong CSetPersonaNameResponse_t_SetCallResult( ulong hAPICall, SteamAPI_SetPersonaNameResponse_t_CallResult func );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "CSetPersonaNameResponse_t_RemoveCallResult" )]
        public static extern ulong CSetPersonaNameResponse_t_RemoveCallResult( ulong handle );
        public delegate void SteamAPI_UserStatsReceived_t_CallResult( UserStatsReceived_t pUserStatsReceived_t, bool bIOFailure );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "CUserStatsReceived_t_SetCallResult" )]
        public static extern ulong CUserStatsReceived_t_SetCallResult( ulong hAPICall, SteamAPI_UserStatsReceived_t_CallResult func );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "CUserStatsReceived_t_RemoveCallResult" )]
        public static extern ulong CUserStatsReceived_t_RemoveCallResult( ulong handle );
        public delegate void SteamAPI_JoinClanChatRoomCompletionResult_t_CallResult( JoinClanChatRoomCompletionResult_t pJoinClanChatRoomCompletionResult_t, bool bIOFailure );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "CJoinClanChatRoomCompletionResult_t_SetCallResult" )]
        public static extern ulong CJoinClanChatRoomCompletionResult_t_SetCallResult( ulong hAPICall, SteamAPI_JoinClanChatRoomCompletionResult_t_CallResult func );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "CJoinClanChatRoomCompletionResult_t_RemoveCallResult" )]
        public static extern ulong CJoinClanChatRoomCompletionResult_t_RemoveCallResult( ulong handle );
        public delegate void SteamAPI_LeaderboardFindResult_t_CallResult( LeaderboardFindResult_t pLeaderboardFindResult_t, bool bIOFailure );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "CLeaderboardFindResult_t_SetCallResult" )]
        public static extern ulong CLeaderboardFindResult_t_SetCallResult( ulong hAPICall, SteamAPI_LeaderboardFindResult_t_CallResult func );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "CLeaderboardFindResult_t_RemoveCallResult" )]
        public static extern ulong CLeaderboardFindResult_t_RemoveCallResult( ulong handle );

    }

}

namespace Valve.Steamworks
{

    internal abstract class ISteamClient
    {
        internal abstract IntPtr GetIntPtr();
        internal abstract uint CreateSteamPipe();
        internal abstract bool BReleaseSteamPipe( uint hSteamPipe );
        internal abstract uint ConnectToGlobalUser( uint hSteamPipe );
        internal abstract uint CreateLocalUser( ref uint phSteamPipe, uint eAccountType );
        internal abstract void ReleaseUser( uint hSteamPipe, uint hUser );
        internal abstract ISteamUser GetISteamUser( uint hSteamUser, uint hSteamPipe, string pchVersion );
        internal abstract ISteamGameServer GetISteamGameServer( uint hSteamUser, uint hSteamPipe, string pchVersion );
        internal abstract void SetLocalIPBinding( uint unIP, char usPort );
        internal abstract ISteamFriends GetISteamFriends( uint hSteamUser, uint hSteamPipe, string pchVersion );
        internal abstract ISteamUtils GetISteamUtils( uint hSteamPipe, string pchVersion );
        internal abstract ISteamMatchmaking GetISteamMatchmaking( uint hSteamUser, uint hSteamPipe, string pchVersion );
        internal abstract ISteamMatchmakingServers GetISteamMatchmakingServers( uint hSteamUser, uint hSteamPipe, string pchVersion );
        internal abstract IntPtr GetISteamGenericInterface( uint hSteamUser, uint hSteamPipe, string pchVersion );
        internal abstract ISteamUserStats GetISteamUserStats( uint hSteamUser, uint hSteamPipe, string pchVersion );
        internal abstract ISteamGameServerStats GetISteamGameServerStats( uint hSteamuser, uint hSteamPipe, string pchVersion );
        internal abstract ISteamApps GetISteamApps( uint hSteamUser, uint hSteamPipe, string pchVersion );
        internal abstract ISteamNetworking GetISteamNetworking( uint hSteamUser, uint hSteamPipe, string pchVersion );
        internal abstract ISteamRemoteStorage GetISteamRemoteStorage( uint hSteamuser, uint hSteamPipe, string pchVersion );
        internal abstract ISteamScreenshots GetISteamScreenshots( uint hSteamuser, uint hSteamPipe, string pchVersion );
        internal abstract uint GetIPCCallCount();
        internal abstract void SetWarningMessageHook( IntPtr pFunction );
        internal abstract bool BShutdownIfAllPipesClosed();
        internal abstract ISteamHTTP GetISteamHTTP( uint hSteamuser, uint hSteamPipe, string pchVersion );
        internal abstract ISteamUnifiedMessages GetISteamUnifiedMessages( uint hSteamuser, uint hSteamPipe, string pchVersion );
        internal abstract ISteamController GetISteamController( uint hSteamUser, uint hSteamPipe, string pchVersion );
        internal abstract ISteamUGC GetISteamUGC( uint hSteamUser, uint hSteamPipe, string pchVersion );
        internal abstract ISteamAppList GetISteamAppList( uint hSteamUser, uint hSteamPipe, string pchVersion );
        internal abstract ISteamMusic GetISteamMusic( uint hSteamuser, uint hSteamPipe, string pchVersion );
        internal abstract ISteamMusicRemote GetISteamMusicRemote( uint hSteamuser, uint hSteamPipe, string pchVersion );
        internal abstract ISteamHTMLSurface GetISteamHTMLSurface( uint hSteamuser, uint hSteamPipe, string pchVersion );
        internal abstract ISteamInventory GetISteamInventory( uint hSteamuser, uint hSteamPipe, string pchVersion );
        internal abstract ISteamVideo GetISteamVideo( uint hSteamuser, uint hSteamPipe, string pchVersion );
    }


    internal abstract class ISteamUser
    {
        internal abstract IntPtr GetIntPtr();
        internal abstract uint GetHSteamUser();
        internal abstract bool BLoggedOn();
        internal abstract ulong GetSteamID();
        internal abstract int InitiateGameConnection( IntPtr pAuthBlob, int cbMaxAuthBlob, ulong steamIDGameServer, uint unIPServer, char usPortServer, bool bSecure );
        internal abstract void TerminateGameConnection( uint unIPServer, char usPortServer );
        internal abstract void TrackAppUsageEvent( ulong gameID, int eAppUsageEvent, string pchExtraInfo );
        internal abstract bool GetUserDataFolder( string pchBuffer, int cubBuffer );
        internal abstract void StartVoiceRecording();
        internal abstract void StopVoiceRecording();
        internal abstract uint GetAvailableVoice( ref uint pcbCompressed, ref uint pcbUncompressed, uint nUncompressedVoiceDesiredSampleRate );
        internal abstract uint GetVoice( bool bWantCompressed, IntPtr pDestBuffer, uint cbDestBufferSize, ref uint nBytesWritten, bool bWantUncompressed, IntPtr pUncompressedDestBuffer, uint cbUncompressedDestBufferSize, ref uint nUncompressBytesWritten, uint nUncompressedVoiceDesiredSampleRate );
        internal abstract uint DecompressVoice( IntPtr pCompressed, uint cbCompressed, IntPtr pDestBuffer, uint cbDestBufferSize, ref uint nBytesWritten, uint nDesiredSampleRate );
        internal abstract uint GetVoiceOptimalSampleRate();
        internal abstract uint GetAuthSessionTicket( IntPtr pTicket, int cbMaxTicket, ref uint pcbTicket );
        internal abstract uint BeginAuthSession( IntPtr pAuthTicket, int cbAuthTicket, ulong steamID );
        internal abstract void EndAuthSession( ulong steamID );
        internal abstract void CancelAuthTicket( uint hAuthTicket );
        internal abstract uint UserHasLicenseForApp( ulong steamID, uint appID );
        internal abstract bool BIsBehindNAT();
        internal abstract void AdvertiseGame( ulong steamIDGameServer, uint unIPServer, char usPortServer );
        internal abstract ulong RequestEncryptedAppTicket( IntPtr pDataToInclude, int cbDataToInclude );
        internal abstract bool GetEncryptedAppTicket( IntPtr pTicket, int cbMaxTicket, ref uint pcbTicket );
        internal abstract int GetGameBadgeLevel( int nSeries, bool bFoil );
        internal abstract int GetPlayerSteamLevel();
        internal abstract ulong RequestStoreAuthURL( string pchRedirectURL );
        internal abstract bool BIsPhoneVerified();
        internal abstract bool BIsTwoFactorEnabled();
    }


    internal abstract class ISteamFriends
    {
        internal abstract IntPtr GetIntPtr();
        internal abstract string GetPersonaName();
        internal abstract ulong SetPersonaName( string pchPersonaName );
        internal abstract uint GetPersonaState();
        internal abstract int GetFriendCount( int iFriendFlags );
        internal abstract ulong GetFriendByIndex( int iFriend, int iFriendFlags );
        internal abstract uint GetFriendRelationship( ulong steamIDFriend );
        internal abstract uint GetFriendPersonaState( ulong steamIDFriend );
        internal abstract string GetFriendPersonaName( ulong steamIDFriend );
        internal abstract bool GetFriendGamePlayed( ulong steamIDFriend, out FriendGameInfo_t pFriendGameInfo );
        internal abstract string GetFriendPersonaNameHistory( ulong steamIDFriend, int iPersonaName );
        internal abstract int GetFriendSteamLevel( ulong steamIDFriend );
        internal abstract string GetPlayerNickname( ulong steamIDPlayer );
        internal abstract int GetFriendsGroupCount();
        internal abstract char GetFriendsGroupIDByIndex( int iFG );
        internal abstract string GetFriendsGroupName( char friendsGroupID );
        internal abstract int GetFriendsGroupMembersCount( char friendsGroupID );
        internal abstract void GetFriendsGroupMembersList( char friendsGroupID, out CSteamID[] pOutSteamIDMembers );
        internal abstract bool HasFriend( ulong steamIDFriend, int iFriendFlags );
        internal abstract int GetClanCount();
        internal abstract ulong GetClanByIndex( int iClan );
        internal abstract string GetClanName( ulong steamIDClan );
        internal abstract string GetClanTag( ulong steamIDClan );
        internal abstract bool GetClanActivityCounts( ulong steamIDClan, ref int pnOnline, ref int pnInGame, ref int pnChatting );
        internal abstract ulong DownloadClanActivityCounts( CSteamID[] psteamIDClans );
        internal abstract int GetFriendCountFromSource( ulong steamIDSource );
        internal abstract ulong GetFriendFromSourceByIndex( ulong steamIDSource, int iFriend );
        internal abstract bool IsUserInSource( ulong steamIDUser, ulong steamIDSource );
        internal abstract void SetInGameVoiceSpeaking( ulong steamIDUser, bool bSpeaking );
        internal abstract void ActivateGameOverlay( string pchDialog );
        internal abstract void ActivateGameOverlayToUser( string pchDialog, ulong steamID );
        internal abstract void ActivateGameOverlayToWebPage( string pchURL );
        internal abstract void ActivateGameOverlayToStore( uint nAppID, char eFlag );
        internal abstract void SetPlayedWith( ulong steamIDUserPlayedWith );
        internal abstract void ActivateGameOverlayInviteDialog( ulong steamIDLobby );
        internal abstract int GetSmallFriendAvatar( ulong steamIDFriend );
        internal abstract int GetMediumFriendAvatar( ulong steamIDFriend );
        internal abstract int GetLargeFriendAvatar( ulong steamIDFriend );
        internal abstract bool RequestUserInformation( ulong steamIDUser, bool bRequireNameOnly );
        internal abstract ulong RequestClanOfficerList( ulong steamIDClan );
        internal abstract ulong GetClanOwner( ulong steamIDClan );
        internal abstract int GetClanOfficerCount( ulong steamIDClan );
        internal abstract ulong GetClanOfficerByIndex( ulong steamIDClan, int iOfficer );
        internal abstract uint GetUserRestrictions();
        internal abstract bool SetRichPresence( string pchKey, string pchValue );
        internal abstract void ClearRichPresence();
        internal abstract string GetFriendRichPresence( ulong steamIDFriend, string pchKey );
        internal abstract int GetFriendRichPresenceKeyCount( ulong steamIDFriend );
        internal abstract string GetFriendRichPresenceKeyByIndex( ulong steamIDFriend, int iKey );
        internal abstract void RequestFriendRichPresence( ulong steamIDFriend );
        internal abstract bool InviteUserToGame( ulong steamIDFriend, string pchConnectString );
        internal abstract int GetCoplayFriendCount();
        internal abstract ulong GetCoplayFriend( int iCoplayFriend );
        internal abstract int GetFriendCoplayTime( ulong steamIDFriend );
        internal abstract uint GetFriendCoplayGame( ulong steamIDFriend );
        internal abstract ulong JoinClanChatRoom( ulong steamIDClan );
        internal abstract bool LeaveClanChatRoom( ulong steamIDClan );
        internal abstract int GetClanChatMemberCount( ulong steamIDClan );
        internal abstract ulong GetChatMemberByIndex( ulong steamIDClan, int iUser );
        internal abstract bool SendClanChatMessage( ulong steamIDClanChat, string pchText );
        internal abstract int GetClanChatMessage( ulong steamIDClanChat, int iMessage, IntPtr prgchText, int cchTextMax, ref uint peChatEntryType, out CSteamID psteamidChatter );
        internal abstract bool IsClanChatAdmin( ulong steamIDClanChat, ulong steamIDUser );
        internal abstract bool IsClanChatWindowOpenInSteam( ulong steamIDClanChat );
        internal abstract bool OpenClanChatWindowInSteam( ulong steamIDClanChat );
        internal abstract bool CloseClanChatWindowInSteam( ulong steamIDClanChat );
        internal abstract bool SetListenForFriendsMessages( bool bInterceptEnabled );
        internal abstract bool ReplyToFriendMessage( ulong steamIDFriend, string pchMsgToSend );
        internal abstract int GetFriendMessage( ulong steamIDFriend, int iMessageID, IntPtr pvData, int cubData, ref uint peChatEntryType );
        internal abstract ulong GetFollowerCount( ulong steamID );
        internal abstract ulong IsFollowing( ulong steamID );
        internal abstract ulong EnumerateFollowingList( uint unStartIndex );
    }


    internal abstract class ISteamUtils
    {
        internal abstract IntPtr GetIntPtr();
        internal abstract uint GetSecondsSinceAppActive();
        internal abstract uint GetSecondsSinceComputerActive();
        internal abstract int GetConnectedUniverse();
        internal abstract uint GetServerRealTime();
        internal abstract string GetIPCountry();
        internal abstract bool GetImageSize( int iImage, ref uint pnWidth, ref uint pnHeight );
        internal abstract bool GetImageRGBA( int iImage, IntPtr pubDest, int nDestBufferSize );
        internal abstract bool GetCSERIPPort( ref uint unIP, ref char usPort );
        internal abstract byte GetCurrentBatteryPower();
        internal abstract uint GetAppID();
        internal abstract void SetOverlayNotificationPosition( uint eNotificationPosition );
        internal abstract bool IsAPICallCompleted( ulong hSteamAPICall, ref bool pbFailed );
        internal abstract int GetAPICallFailureReason( ulong hSteamAPICall );
        internal abstract bool GetAPICallResult( ulong hSteamAPICall, IntPtr pCallback, int cubCallback, int iCallbackExpected, ref bool pbFailed );
        internal abstract uint GetIPCCallCount();
        internal abstract void SetWarningMessageHook( IntPtr pFunction );
        internal abstract bool IsOverlayEnabled();
        internal abstract bool BOverlayNeedsPresent();
        internal abstract ulong CheckFileSignature( string szFileName );
        internal abstract bool ShowGamepadTextInput( int eInputMode, int eLineInputMode, string pchDescription, uint unCharMax, string pchExistingText );
        internal abstract uint GetEnteredGamepadTextLength();
        internal abstract bool GetEnteredGamepadTextInput( string pchText, uint cchText );
        internal abstract string GetSteamUILanguage();
        internal abstract bool IsSteamRunningInVR();
        internal abstract void SetOverlayNotificationInset( int nHorizontalInset, int nVerticalInset );
        internal abstract bool IsSteamInBigPictureMode();
        internal abstract void StartVRDashboard();
    }


    internal abstract class ISteamMatchmaking
    {
        internal abstract IntPtr GetIntPtr();
        internal abstract int GetFavoriteGameCount();
        internal abstract bool GetFavoriteGame( int iGame, ref uint pnAppID, ref uint pnIP, ref char pnConnPort, ref char pnQueryPort, ref uint punFlags, ref uint pRTime32LastPlayedOnServer );
        internal abstract int AddFavoriteGame( uint nAppID, uint nIP, char nConnPort, char nQueryPort, uint unFlags, uint rTime32LastPlayedOnServer );
        internal abstract bool RemoveFavoriteGame( uint nAppID, uint nIP, char nConnPort, char nQueryPort, uint unFlags );
        internal abstract ulong RequestLobbyList();
        internal abstract void AddRequestLobbyListStringFilter( string pchKeyToMatch, string pchValueToMatch, uint eComparisonType );
        internal abstract void AddRequestLobbyListNumericalFilter( string pchKeyToMatch, int nValueToMatch, uint eComparisonType );
        internal abstract void AddRequestLobbyListNearValueFilter( string pchKeyToMatch, int nValueToBeCloseTo );
        internal abstract void AddRequestLobbyListFilterSlotsAvailable( int nSlotsAvailable );
        internal abstract void AddRequestLobbyListDistanceFilter( uint eLobbyDistanceFilter );
        internal abstract void AddRequestLobbyListResultCountFilter( int cMaxResults );
        internal abstract void AddRequestLobbyListCompatibleMembersFilter( ulong steamIDLobby );
        internal abstract ulong GetLobbyByIndex( int iLobby );
        internal abstract ulong CreateLobby( uint eLobbyType, int cMaxMembers );
        internal abstract ulong JoinLobby( ulong steamIDLobby );
        internal abstract void LeaveLobby( ulong steamIDLobby );
        internal abstract bool InviteUserToLobby( ulong steamIDLobby, ulong steamIDInvitee );
        internal abstract int GetNumLobbyMembers( ulong steamIDLobby );
        internal abstract ulong GetLobbyMemberByIndex( ulong steamIDLobby, int iMember );
        internal abstract string GetLobbyData( ulong steamIDLobby, string pchKey );
        internal abstract bool SetLobbyData( ulong steamIDLobby, string pchKey, string pchValue );
        internal abstract int GetLobbyDataCount( ulong steamIDLobby );
        internal abstract bool GetLobbyDataByIndex( ulong steamIDLobby, int iLobbyData, string pchKey, int cchKeyBufferSize, string pchValue, int cchValueBufferSize );
        internal abstract bool DeleteLobbyData( ulong steamIDLobby, string pchKey );
        internal abstract string GetLobbyMemberData( ulong steamIDLobby, ulong steamIDUser, string pchKey );
        internal abstract void SetLobbyMemberData( ulong steamIDLobby, string pchKey, string pchValue );
        internal abstract bool SendLobbyChatMsg( ulong steamIDLobby, IntPtr pvMsgBody, int cubMsgBody );
        internal abstract int GetLobbyChatEntry( ulong steamIDLobby, int iChatID, out CSteamID pSteamIDUser, IntPtr pvData, int cubData, ref uint peChatEntryType );
        internal abstract bool RequestLobbyData( ulong steamIDLobby );
        internal abstract void SetLobbyGameServer( ulong steamIDLobby, uint unGameServerIP, char unGameServerPort, ulong steamIDGameServer );
        internal abstract bool GetLobbyGameServer( ulong steamIDLobby, ref uint punGameServerIP, ref char punGameServerPort, out CSteamID psteamIDGameServer );
        internal abstract bool SetLobbyMemberLimit( ulong steamIDLobby, int cMaxMembers );
        internal abstract int GetLobbyMemberLimit( ulong steamIDLobby );
        internal abstract bool SetLobbyType( ulong steamIDLobby, uint eLobbyType );
        internal abstract bool SetLobbyJoinable( ulong steamIDLobby, bool bLobbyJoinable );
        internal abstract ulong GetLobbyOwner( ulong steamIDLobby );
        internal abstract bool SetLobbyOwner( ulong steamIDLobby, ulong steamIDNewOwner );
        internal abstract bool SetLinkedLobby( ulong steamIDLobby, ulong steamIDLobbyDependent );
    }


    internal abstract class ISteamMatchmakingServerListResponse
    {
        internal abstract IntPtr GetIntPtr();
        internal abstract void ServerResponded( uint hRequest, int iServer );
        internal abstract void ServerFailedToRespond( uint hRequest, int iServer );
        internal abstract void RefreshComplete( uint hRequest, uint response );
    }


    internal abstract class ISteamMatchmakingPingResponse
    {
        internal abstract IntPtr GetIntPtr();
        internal abstract void ServerResponded( IntPtr server );
        internal abstract void ServerFailedToRespond();
    }


    internal abstract class ISteamMatchmakingPlayersResponse
    {
        internal abstract IntPtr GetIntPtr();
        internal abstract void AddPlayerToList( string pchName, int nScore, float flTimePlayed );
        internal abstract void PlayersFailedToRespond();
        internal abstract void PlayersRefreshComplete();
    }


    internal abstract class ISteamMatchmakingRulesResponse
    {
        internal abstract IntPtr GetIntPtr();
        internal abstract void RulesResponded( string pchRule, string pchValue );
        internal abstract void RulesFailedToRespond();
        internal abstract void RulesRefreshComplete();
    }


    internal abstract class ISteamMatchmakingServers
    {
        internal abstract IntPtr GetIntPtr();
        internal abstract IntPtr RequestInternetServerList( uint iApp, IntPtr ppchFilters, int length, IntPtr pRequestServersResponse );
        internal abstract IntPtr RequestLANServerList( uint iApp, IntPtr pRequestServersResponse );
        internal abstract IntPtr RequestFriendsServerList( uint iApp, IntPtr[] ppchFilters, IntPtr pRequestServersResponse );
        internal abstract IntPtr RequestFavoritesServerList( uint iApp, IntPtr[] ppchFilters, IntPtr pRequestServersResponse );
        internal abstract IntPtr RequestHistoryServerList( uint iApp, IntPtr[] ppchFilters, IntPtr pRequestServersResponse );
        internal abstract IntPtr RequestSpectatorServerList( uint iApp, IntPtr[] ppchFilters, IntPtr pRequestServersResponse );
        internal abstract void ReleaseRequest( IntPtr hServerListRequest );
        internal abstract gameserveritem_t GetServerDetails( IntPtr hRequest, int iServer );
        internal abstract void CancelQuery( IntPtr hRequest );
        internal abstract void RefreshQuery( IntPtr hRequest );
        internal abstract bool IsRefreshing( IntPtr hRequest );
        internal abstract int GetServerCount( IntPtr hRequest );
        internal abstract void RefreshServer( IntPtr hRequest, int iServer );
        internal abstract uint PingServer( uint unIP, char usPort, ISteamMatchmakingPingResponse pRequestServersResponse );
        internal abstract uint PlayerDetails( uint unIP, char usPort, ISteamMatchmakingPlayersResponse pRequestServersResponse );
        internal abstract uint ServerRules( uint unIP, char usPort, ISteamMatchmakingRulesResponse pRequestServersResponse );
        internal abstract void CancelServerQuery( uint hServerQuery );
    }


    internal abstract class ISteamRemoteStorage
    {
        internal abstract IntPtr GetIntPtr();
        internal abstract bool FileWrite( string pchFile, IntPtr pvData, int cubData );
        internal abstract int FileRead( string pchFile, IntPtr pvData, int cubDataToRead );
        internal abstract ulong FileWriteAsync( string pchFile, IntPtr pvData, uint cubData );
        internal abstract ulong FileReadAsync( string pchFile, uint nOffset, uint cubToRead );
        internal abstract bool FileReadAsyncComplete( ulong hReadCall, IntPtr pvBuffer, uint cubToRead );
        internal abstract bool FileForget( string pchFile );
        internal abstract bool FileDelete( string pchFile );
        internal abstract ulong FileShare( string pchFile );
        internal abstract bool SetSyncPlatforms( string pchFile, uint eRemoteStoragePlatform );
        internal abstract ulong FileWriteStreamOpen( string pchFile );
        internal abstract bool FileWriteStreamWriteChunk( ulong writeHandle, IntPtr pvData, int cubData );
        internal abstract bool FileWriteStreamClose( ulong writeHandle );
        internal abstract bool FileWriteStreamCancel( ulong writeHandle );
        internal abstract bool FileExists( string pchFile );
        internal abstract bool FilePersisted( string pchFile );
        internal abstract int GetFileSize( string pchFile );
        internal abstract long GetFileTimestamp( string pchFile );
        internal abstract uint GetSyncPlatforms( string pchFile );
        internal abstract int GetFileCount();
        internal abstract string GetFileNameAndSize( int iFile, ref int pnFileSizeInBytes );
        internal abstract bool GetQuota( ref int pnTotalBytes, ref int puAvailableBytes );
        internal abstract bool IsCloudEnabledForAccount();
        internal abstract bool IsCloudEnabledForApp();
        internal abstract void SetCloudEnabledForApp( bool bEnabled );
        internal abstract ulong UGCDownload( ulong hContent, uint unPriority );
        internal abstract bool GetUGCDownloadProgress( ulong hContent, ref int pnBytesDownloaded, ref int pnBytesExpected );
        internal abstract bool GetUGCDetails( ulong hContent, ref uint pnAppID, System.Text.StringBuilder ppchName, ref int pnFileSizeInBytes, out CSteamID pSteamIDOwner );
        internal abstract int UGCRead( ulong hContent, IntPtr pvData, int cubDataToRead, uint cOffset, uint eAction );
        internal abstract int GetCachedUGCCount();
        internal abstract ulong GetCachedUGCHandle( int iCachedContent );
        internal abstract ulong PublishWorkshopFile( string pchFile, string pchPreviewFile, uint nConsumerAppId, string pchTitle, string pchDescription, uint eVisibility, ref SteamParamStringArray_t pTags, uint eWorkshopFileType );
        internal abstract ulong CreatePublishedFileUpdateRequest( ulong unPublishedFileId );
        internal abstract bool UpdatePublishedFileFile( ulong updateHandle, string pchFile );
        internal abstract bool UpdatePublishedFilePreviewFile( ulong updateHandle, string pchPreviewFile );
        internal abstract bool UpdatePublishedFileTitle( ulong updateHandle, string pchTitle );
        internal abstract bool UpdatePublishedFileDescription( ulong updateHandle, string pchDescription );
        internal abstract bool UpdatePublishedFileVisibility( ulong updateHandle, uint eVisibility );
        internal abstract bool UpdatePublishedFileTags( ulong updateHandle, ref SteamParamStringArray_t pTags );
        internal abstract ulong CommitPublishedFileUpdate( ulong updateHandle );
        internal abstract ulong GetPublishedFileDetails( ulong unPublishedFileId, uint unMaxSecondsOld );
        internal abstract ulong DeletePublishedFile( ulong unPublishedFileId );
        internal abstract ulong EnumerateUserPublishedFiles( uint unStartIndex );
        internal abstract ulong SubscribePublishedFile( ulong unPublishedFileId );
        internal abstract ulong EnumerateUserSubscribedFiles( uint unStartIndex );
        internal abstract ulong UnsubscribePublishedFile( ulong unPublishedFileId );
        internal abstract bool UpdatePublishedFileSetChangeDescription( ulong updateHandle, string pchChangeDescription );
        internal abstract ulong GetPublishedItemVoteDetails( ulong unPublishedFileId );
        internal abstract ulong UpdateUserPublishedItemVote( ulong unPublishedFileId, bool bVoteUp );
        internal abstract ulong GetUserPublishedItemVoteDetails( ulong unPublishedFileId );
        internal abstract ulong EnumerateUserSharedWorkshopFiles( ulong steamId, uint unStartIndex, ref SteamParamStringArray_t pRequiredTags, ref SteamParamStringArray_t pExcludedTags );
        internal abstract ulong PublishVideo( uint eVideoProvider, string pchVideoAccount, string pchVideoIdentifier, string pchPreviewFile, uint nConsumerAppId, string pchTitle, string pchDescription, uint eVisibility, ref SteamParamStringArray_t pTags );
        internal abstract ulong SetUserPublishedFileAction( ulong unPublishedFileId, uint eAction );
        internal abstract ulong EnumeratePublishedFilesByUserAction( uint eAction, uint unStartIndex );
        internal abstract ulong EnumeratePublishedWorkshopFiles( uint eEnumerationType, uint unStartIndex, uint unCount, uint unDays, ref SteamParamStringArray_t pTags, ref SteamParamStringArray_t pUserTags );
        internal abstract ulong UGCDownloadToLocation( ulong hContent, string pchLocation, uint unPriority );
    }


    internal abstract class ISteamUserStats
    {
        internal abstract IntPtr GetIntPtr();
        internal abstract bool RequestCurrentStats();
        internal abstract bool GetStat( string pchName, ref int pData );
        internal abstract bool GetStat0( string pchName, ref float pData );
        internal abstract bool SetStat( string pchName, int nData );
        internal abstract bool SetStat0( string pchName, float fData );
        internal abstract bool UpdateAvgRateStat( string pchName, float flCountThisSession, double dSessionLength );
        internal abstract bool GetAchievement( string pchName, ref bool pbAchieved );
        internal abstract bool SetAchievement( string pchName );
        internal abstract bool ClearAchievement( string pchName );
        internal abstract bool GetAchievementAndUnlockTime( string pchName, ref bool pbAchieved, ref uint punUnlockTime );
        internal abstract bool StoreStats();
        internal abstract int GetAchievementIcon( string pchName );
        internal abstract string GetAchievementDisplayAttribute( string pchName, string pchKey );
        internal abstract bool IndicateAchievementProgress( string pchName, uint nCurProgress, uint nMaxProgress );
        internal abstract uint GetNumAchievements();
        internal abstract string GetAchievementName( uint iAchievement );
        internal abstract ulong RequestUserStats( ulong steamIDUser );
        internal abstract bool GetUserStat( ulong steamIDUser, string pchName, ref int pData );
        internal abstract bool GetUserStat0( ulong steamIDUser, string pchName, ref float pData );
        internal abstract bool GetUserAchievement( ulong steamIDUser, string pchName, ref bool pbAchieved );
        internal abstract bool GetUserAchievementAndUnlockTime( ulong steamIDUser, string pchName, ref bool pbAchieved, ref uint punUnlockTime );
        internal abstract bool ResetAllStats( bool bAchievementsToo );
        internal abstract ulong FindOrCreateLeaderboard( string pchLeaderboardName, uint eLeaderboardSortMethod, uint eLeaderboardDisplayType );
        internal abstract ulong FindLeaderboard( string pchLeaderboardName );
        internal abstract string GetLeaderboardName( ulong hSteamLeaderboard );
        internal abstract int GetLeaderboardEntryCount( ulong hSteamLeaderboard );
        internal abstract uint GetLeaderboardSortMethod( ulong hSteamLeaderboard );
        internal abstract uint GetLeaderboardDisplayType( ulong hSteamLeaderboard );
        internal abstract ulong DownloadLeaderboardEntries( ulong hSteamLeaderboard, uint eLeaderboardDataRequest, int nRangeStart, int nRangeEnd );
        internal abstract ulong DownloadLeaderboardEntriesForUsers( ulong hSteamLeaderboard, CSteamID[] prgUsers );
        internal abstract bool GetDownloadedLeaderboardEntry( ulong hSteamLeaderboardEntries, int index, ref LeaderboardEntry_t pLeaderboardEntry, ref int pDetails, int cDetailsMax );
        internal abstract ulong UploadLeaderboardScore( ulong hSteamLeaderboard, uint eLeaderboardUploadScoreMethod, int nScore, ref int pScoreDetails, int cScoreDetailsCount );
        internal abstract ulong AttachLeaderboardUGC( ulong hSteamLeaderboard, ulong hUGC );
        internal abstract ulong GetNumberOfCurrentPlayers();
        internal abstract ulong RequestGlobalAchievementPercentages();
        internal abstract int GetMostAchievedAchievementInfo( string pchName, uint unNameBufLen, ref float pflPercent, ref bool pbAchieved );
        internal abstract int GetNextMostAchievedAchievementInfo( int iIteratorPrevious, string pchName, uint unNameBufLen, ref float pflPercent, ref bool pbAchieved );
        internal abstract bool GetAchievementAchievedPercent( string pchName, ref float pflPercent );
        internal abstract ulong RequestGlobalStats( int nHistoryDays );
        internal abstract bool GetGlobalStat( string pchStatName, ref long pData );
        internal abstract bool GetGlobalStat0( string pchStatName, ref double pData );
        internal abstract int GetGlobalStatHistory( string pchStatName, long[] pData );
        internal abstract int GetGlobalStatHistory0( string pchStatName, double[] pData );
    }


    internal abstract class ISteamApps
    {
        internal abstract IntPtr GetIntPtr();
        internal abstract bool BIsSubscribed();
        internal abstract bool BIsLowViolence();
        internal abstract bool BIsCybercafe();
        internal abstract bool BIsVACBanned();
        internal abstract string GetCurrentGameLanguage();
        internal abstract string GetAvailableGameLanguages();
        internal abstract bool BIsSubscribedApp( uint appID );
        internal abstract bool BIsDlcInstalled( uint appID );
        internal abstract uint GetEarliestPurchaseUnixTime( uint nAppID );
        internal abstract bool BIsSubscribedFromFreeWeekend();
        internal abstract int GetDLCCount();
        internal abstract bool BGetDLCDataByIndex( int iDLC, ref uint pAppID, ref bool pbAvailable, string pchName, int cchNameBufferSize );
        internal abstract void InstallDLC( uint nAppID );
        internal abstract void UninstallDLC( uint nAppID );
        internal abstract void RequestAppProofOfPurchaseKey( uint nAppID );
        internal abstract string GetCurrentBetaName();
        internal abstract bool MarkContentCorrupt( bool bMissingFilesOnly );
        internal abstract uint GetInstalledDepots( uint appID, ref uint pvecDepots, uint cMaxDepots );
        internal abstract string GetAppInstallDir( uint appID );
        internal abstract bool BIsAppInstalled( uint appID );
        internal abstract ulong GetAppOwner();
        internal abstract string GetLaunchQueryParam( string pchKey );
        internal abstract bool GetDlcDownloadProgress( uint nAppID, ref ulong punBytesDownloaded, ref ulong punBytesTotal );
        internal abstract int GetAppBuildId();
        internal abstract void RequestAllProofOfPurchaseKeys();
    }


    internal abstract class ISteamNetworking
    {
        internal abstract IntPtr GetIntPtr();
        internal abstract bool SendP2PPacket( ulong steamIDRemote, IntPtr pubData, uint cubData, uint eP2PSendType, int nChannel );
        internal abstract bool IsP2PPacketAvailable( ref uint pcubMsgSize, int nChannel );
        internal abstract bool ReadP2PPacket( IntPtr pubDest, uint cubDest, ref uint pcubMsgSize, ref ulong psteamIDRemote, int nChannel );
        internal abstract bool AcceptP2PSessionWithUser( ulong steamIDRemote );
        internal abstract bool CloseP2PSessionWithUser( ulong steamIDRemote );
        internal abstract bool CloseP2PChannelWithUser( ulong steamIDRemote, int nChannel );
        internal abstract bool GetP2PSessionState( ulong steamIDRemote, ref P2PSessionState_t pConnectionState );
        internal abstract bool AllowP2PPacketRelay( bool bAllow );
        internal abstract uint CreateListenSocket( int nVirtualP2PPort, uint nIP, char nPort, bool bAllowUseOfPacketRelay );
        internal abstract uint CreateP2PConnectionSocket( ulong steamIDTarget, int nVirtualPort, int nTimeoutSec, bool bAllowUseOfPacketRelay );
        internal abstract uint CreateConnectionSocket( uint nIP, char nPort, int nTimeoutSec );
        internal abstract bool DestroySocket( uint hSocket, bool bNotifyRemoteEnd );
        internal abstract bool DestroyListenSocket( uint hSocket, bool bNotifyRemoteEnd );
        internal abstract bool SendDataOnSocket( uint hSocket, IntPtr pubData, uint cubData, bool bReliable );
        internal abstract bool IsDataAvailableOnSocket( uint hSocket, ref uint pcubMsgSize );
        internal abstract bool RetrieveDataFromSocket( uint hSocket, IntPtr pubDest, uint cubDest, ref uint pcubMsgSize );
        internal abstract bool IsDataAvailable( uint hListenSocket, ref uint pcubMsgSize, ref uint phSocket );
        internal abstract bool RetrieveData( uint hListenSocket, IntPtr pubDest, uint cubDest, ref uint pcubMsgSize, ref uint phSocket );
        internal abstract bool GetSocketInfo( uint hSocket, ref CSteamID pSteamIDRemote, ref int peSocketStatus, ref uint punIPRemote, ref char punPortRemote );
        internal abstract bool GetListenSocketInfo( uint hListenSocket, ref uint pnIP, ref char pnPort );
        internal abstract uint GetSocketConnectionType( uint hSocket );
        internal abstract int GetMaxPacketSize( uint hSocket );
    }


    internal abstract class ISteamScreenshots
    {
        internal abstract IntPtr GetIntPtr();
        internal abstract uint WriteScreenshot( IntPtr pubRGB, uint cubRGB, int nWidth, int nHeight );
        internal abstract uint AddScreenshotToLibrary( string pchFilename, string pchThumbnailFilename, int nWidth, int nHeight );
        internal abstract void TriggerScreenshot();
        internal abstract void HookScreenshots( bool bHook );
        internal abstract bool SetLocation( uint hScreenshot, string pchLocation );
        internal abstract bool TagUser( uint hScreenshot, ulong steamID );
        internal abstract bool TagPublishedFile( uint hScreenshot, ulong unPublishedFileID );
    }


    internal abstract class ISteamMusic
    {
        internal abstract IntPtr GetIntPtr();
        internal abstract bool BIsEnabled();
        internal abstract bool BIsPlaying();
        internal abstract int GetPlaybackStatus();
        internal abstract void Play();
        internal abstract void Pause();
        internal abstract void PlayPrevious();
        internal abstract void PlayNext();
        internal abstract void SetVolume( float flVolume );
        internal abstract float GetVolume();
    }


    internal abstract class ISteamMusicRemote
    {
        internal abstract IntPtr GetIntPtr();
        internal abstract bool RegisterSteamMusicRemote( string pchName );
        internal abstract bool DeregisterSteamMusicRemote();
        internal abstract bool BIsCurrentMusicRemote();
        internal abstract bool BActivationSuccess( bool bValue );
        internal abstract bool SetDisplayName( string pchDisplayName );
        internal abstract bool SetPNGIcon_64x64( IntPtr pvBuffer, uint cbBufferLength );
        internal abstract bool EnablePlayPrevious( bool bValue );
        internal abstract bool EnablePlayNext( bool bValue );
        internal abstract bool EnableShuffled( bool bValue );
        internal abstract bool EnableLooped( bool bValue );
        internal abstract bool EnableQueue( bool bValue );
        internal abstract bool EnablePlaylists( bool bValue );
        internal abstract bool UpdatePlaybackStatus( int nStatus );
        internal abstract bool UpdateShuffled( bool bValue );
        internal abstract bool UpdateLooped( bool bValue );
        internal abstract bool UpdateVolume( float flValue );
        internal abstract bool CurrentEntryWillChange();
        internal abstract bool CurrentEntryIsAvailable( bool bAvailable );
        internal abstract bool UpdateCurrentEntryText( string pchText );
        internal abstract bool UpdateCurrentEntryElapsedSeconds( int nValue );
        internal abstract bool UpdateCurrentEntryCoverArt( IntPtr pvBuffer, uint cbBufferLength );
        internal abstract bool CurrentEntryDidChange();
        internal abstract bool QueueWillChange();
        internal abstract bool ResetQueueEntries();
        internal abstract bool SetQueueEntry( int nID, int nPosition, string pchEntryText );
        internal abstract bool SetCurrentQueueEntry( int nID );
        internal abstract bool QueueDidChange();
        internal abstract bool PlaylistWillChange();
        internal abstract bool ResetPlaylistEntries();
        internal abstract bool SetPlaylistEntry( int nID, int nPosition, string pchEntryText );
        internal abstract bool SetCurrentPlaylistEntry( int nID );
        internal abstract bool PlaylistDidChange();
    }


    internal abstract class ISteamHTTP
    {
        internal abstract IntPtr GetIntPtr();
        internal abstract uint CreateHTTPRequest( uint eHTTPRequestMethod, string pchAbsoluteURL );
        internal abstract bool SetHTTPRequestContextValue( uint hRequest, ulong ulContextValue );
        internal abstract bool SetHTTPRequestNetworkActivityTimeout( uint hRequest, uint unTimeoutSeconds );
        internal abstract bool SetHTTPRequestHeaderValue( uint hRequest, string pchHeaderName, string pchHeaderValue );
        internal abstract bool SetHTTPRequestGetOrPostParameter( uint hRequest, string pchParamName, string pchParamValue );
        internal abstract bool SendHTTPRequest( uint hRequest, ref ulong pCallHandle );
        internal abstract bool SendHTTPRequestAndStreamResponse( uint hRequest, ref ulong pCallHandle );
        internal abstract bool DeferHTTPRequest( uint hRequest );
        internal abstract bool PrioritizeHTTPRequest( uint hRequest );
        internal abstract bool GetHTTPResponseHeaderSize( uint hRequest, string pchHeaderName, ref uint unResponseHeaderSize );
        internal abstract bool GetHTTPResponseHeaderValue( uint hRequest, string pchHeaderName, IntPtr pHeaderValueBuffer, uint unBufferSize );
        internal abstract bool GetHTTPResponseBodySize( uint hRequest, ref uint unBodySize );
        internal abstract bool GetHTTPResponseBodyData( uint hRequest, IntPtr pBodyDataBuffer, uint unBufferSize );
        internal abstract bool GetHTTPStreamingResponseBodyData( uint hRequest, uint cOffset, IntPtr pBodyDataBuffer, uint unBufferSize );
        internal abstract bool ReleaseHTTPRequest( uint hRequest );
        internal abstract bool GetHTTPDownloadProgressPct( uint hRequest, ref float pflPercentOut );
        internal abstract bool SetHTTPRequestRawPostBody( uint hRequest, string pchContentType, IntPtr pubBody, uint unBodyLen );
        internal abstract uint CreateCookieContainer( bool bAllowResponsesToModify );
        internal abstract bool ReleaseCookieContainer( uint hCookieContainer );
        internal abstract bool SetCookie( uint hCookieContainer, string pchHost, string pchUrl, string pchCookie );
        internal abstract bool SetHTTPRequestCookieContainer( uint hRequest, uint hCookieContainer );
        internal abstract bool SetHTTPRequestUserAgentInfo( uint hRequest, string pchUserAgentInfo );
        internal abstract bool SetHTTPRequestRequiresVerifiedCertificate( uint hRequest, bool bRequireVerifiedCertificate );
        internal abstract bool SetHTTPRequestAbsoluteTimeoutMS( uint hRequest, uint unMilliseconds );
        internal abstract bool GetHTTPRequestWasTimedOut( uint hRequest, ref bool pbWasTimedOut );
    }


    internal abstract class ISteamUnifiedMessages
    {
        internal abstract IntPtr GetIntPtr();
        internal abstract ulong SendMethod( string pchServiceMethod, IntPtr pRequestBuffer, uint unRequestBufferSize, ulong unContext );
        internal abstract bool GetMethodResponseInfo( ulong hHandle, ref uint punResponseSize, ref uint peResult );
        internal abstract bool GetMethodResponseData( ulong hHandle, IntPtr pResponseBuffer, uint unResponseBufferSize, bool bAutoRelease );
        internal abstract bool ReleaseMethod( ulong hHandle );
        internal abstract bool SendNotification( string pchServiceNotification, IntPtr pNotificationBuffer, uint unNotificationBufferSize );
    }


    internal abstract class ISteamController
    {
        internal abstract IntPtr GetIntPtr();
        internal abstract bool Init();
        internal abstract bool Shutdown();
        internal abstract void RunFrame();
        internal abstract int GetConnectedControllers( ref ulong handlesOut );
        internal abstract bool ShowBindingPanel( ulong controllerHandle );
        internal abstract ulong GetActionSetHandle( string pszActionSetName );
        internal abstract void ActivateActionSet( ulong controllerHandle, ulong actionSetHandle );
        internal abstract ulong GetCurrentActionSet( ulong controllerHandle );
        internal abstract ulong GetDigitalActionHandle( string pszActionName );
        internal abstract ControllerDigitalActionData_t GetDigitalActionData( ulong controllerHandle, ulong digitalActionHandle );
        internal abstract int GetDigitalActionOrigins( ulong controllerHandle, ulong actionSetHandle, ulong digitalActionHandle, ref uint originsOut );
        internal abstract ulong GetAnalogActionHandle( string pszActionName );
        internal abstract ControllerAnalogActionData_t GetAnalogActionData( ulong controllerHandle, ulong analogActionHandle );
        internal abstract int GetAnalogActionOrigins( ulong controllerHandle, ulong actionSetHandle, ulong analogActionHandle, ref uint originsOut );
        internal abstract void StopAnalogActionMomentum( ulong controllerHandle, ulong eAction );
        internal abstract void TriggerHapticPulse( ulong controllerHandle, uint eTargetPad, char usDurationMicroSec );
        internal abstract void TriggerRepeatedHapticPulse( ulong controllerHandle, uint eTargetPad, char usDurationMicroSec, char usOffMicroSec, char unRepeat, uint nFlags );
    }


    internal abstract class ISteamUGC
    {
        internal abstract IntPtr GetIntPtr();
        internal abstract ulong CreateQueryUserUGCRequest( uint unAccountID, uint eListType, uint eMatchingUGCType, uint eSortOrder, uint nCreatorAppID, uint nConsumerAppID, uint unPage );
        internal abstract ulong CreateQueryAllUGCRequest( uint eQueryType, uint eMatchingeMatchingUGCTypeFileType, uint nCreatorAppID, uint nConsumerAppID, uint unPage );
        internal abstract ulong CreateQueryUGCDetailsRequest( ref ulong pvecPublishedFileID, uint unNumPublishedFileIDs );
        internal abstract ulong SendQueryUGCRequest( ulong handle );
        internal abstract bool GetQueryUGCResult( ulong handle, uint index, ref SteamUGCDetails_t pDetails );
        internal abstract bool GetQueryUGCPreviewURL( ulong handle, uint index, out string pchURL );
        internal abstract bool GetQueryUGCMetadata( ulong handle, uint index, out string pchMetadata );
        internal abstract bool GetQueryUGCChildren( ulong handle, uint index, ref ulong pvecPublishedFileID, uint cMaxEntries );
        internal abstract bool GetQueryUGCStatistic( ulong handle, uint index, uint eStatType, ref uint pStatValue );
        internal abstract uint GetQueryUGCNumAdditionalPreviews( ulong handle, uint index );
        internal abstract bool GetQueryUGCAdditionalPreview( ulong handle, uint index, uint previewIndex, out string pchURLOrVideoID, out string pchOriginalFileName, uint cchOriginalFileNameSize, ref uint pPreviewType );
        internal abstract uint GetQueryUGCNumKeyValueTags( ulong handle, uint index );
        internal abstract bool GetQueryUGCKeyValueTag( ulong handle, uint index, uint keyValueTagIndex, out string pchKey, out string pchValue );
        internal abstract bool ReleaseQueryUGCRequest( ulong handle );
        internal abstract bool AddRequiredTag( ulong handle, string pTagName );
        internal abstract bool AddExcludedTag( ulong handle, string pTagName );
        internal abstract bool SetReturnKeyValueTags( ulong handle, bool bReturnKeyValueTags );
        internal abstract bool SetReturnLongDescription( ulong handle, bool bReturnLongDescription );
        internal abstract bool SetReturnMetadata( ulong handle, bool bReturnMetadata );
        internal abstract bool SetReturnChildren( ulong handle, bool bReturnChildren );
        internal abstract bool SetReturnAdditionalPreviews( ulong handle, bool bReturnAdditionalPreviews );
        internal abstract bool SetReturnTotalOnly( ulong handle, bool bReturnTotalOnly );
        internal abstract bool SetLanguage( ulong handle, string pchLanguage );
        internal abstract bool SetAllowCachedResponse( ulong handle, uint unMaxAgeSeconds );
        internal abstract bool SetCloudFileNameFilter( ulong handle, string pMatchCloudFileName );
        internal abstract bool SetMatchAnyTag( ulong handle, bool bMatchAnyTag );
        internal abstract bool SetSearchText( ulong handle, string pSearchText );
        internal abstract bool SetRankedByTrendDays( ulong handle, uint unDays );
        internal abstract bool AddRequiredKeyValueTag( ulong handle, string pKey, string pValue );
        internal abstract ulong RequestUGCDetails( ulong nPublishedFileID, uint unMaxAgeSeconds );
        internal abstract ulong CreateItem( uint nConsumerAppId, uint eFileType );
        internal abstract ulong StartItemUpdate( uint nConsumerAppId, ulong nPublishedFileID );
        internal abstract bool SetItemTitle( ulong handle, string pchTitle );
        internal abstract bool SetItemDescription( ulong handle, string pchDescription );
        internal abstract bool SetItemUpdateLanguage( ulong handle, string pchLanguage );
        internal abstract bool SetItemMetadata( ulong handle, string pchMetaData );
        internal abstract bool SetItemVisibility( ulong handle, uint eVisibility );
        internal abstract bool SetItemTags( ulong updateHandle, ref SteamParamStringArray_t pTags );
        internal abstract bool SetItemContent( ulong handle, string pszContentFolder );
        internal abstract bool SetItemPreview( ulong handle, string pszPreviewFile );
        internal abstract bool RemoveItemKeyValueTags( ulong handle, string pchKey );
        internal abstract bool AddItemKeyValueTag( ulong handle, string pchKey, string pchValue );
        internal abstract bool AddItemPreviewFile( ulong handle, string pszPreviewFile, uint type );
        internal abstract bool AddItemPreviewVideo( ulong handle, string pszVideoID );
        internal abstract bool UpdateItemPreviewFile( ulong handle, uint index, string pszPreviewFile );
        internal abstract bool UpdateItemPreviewVideo( ulong handle, uint index, string pszVideoID );
        internal abstract bool RemoveItemPreview( ulong handle, uint index );
        internal abstract ulong SubmitItemUpdate( ulong handle, string pchChangeNote );
        internal abstract uint GetItemUpdateProgress( ulong handle, ref ulong punBytesProcessed, ref ulong punBytesTotal );
        internal abstract ulong SetUserItemVote( ulong nPublishedFileID, bool bVoteUp );
        internal abstract ulong GetUserItemVote( ulong nPublishedFileID );
        internal abstract ulong AddItemToFavorites( uint nAppId, ulong nPublishedFileID );
        internal abstract ulong RemoveItemFromFavorites( uint nAppId, ulong nPublishedFileID );
        internal abstract ulong SubscribeItem( ulong nPublishedFileID );
        internal abstract ulong UnsubscribeItem( ulong nPublishedFileID );
        internal abstract uint GetNumSubscribedItems();
        internal abstract uint GetSubscribedItems( ref ulong pvecPublishedFileID, uint cMaxEntries );
        internal abstract uint GetItemState( ulong nPublishedFileID );
        internal abstract bool GetItemInstallInfo( ulong nPublishedFileID, ref ulong punSizeOnDisk, out string pchFolder, ref uint punTimeStamp );
        internal abstract bool GetItemDownloadInfo( ulong nPublishedFileID, ref ulong punBytesDownloaded, ref ulong punBytesTotal );
        internal abstract bool DownloadItem( ulong nPublishedFileID, bool bHighPriority );
        internal abstract bool BInitWorkshopForGameServer( uint unWorkshopDepotID, string pszFolder );
        internal abstract void SuspendDownloads( bool bSuspend );
    }


    internal abstract class ISteamAppList
    {
        internal abstract IntPtr GetIntPtr();
        internal abstract uint GetNumInstalledApps();
        internal abstract uint GetInstalledApps( ref uint pvecAppID, uint unMaxAppIDs );
        internal abstract int GetAppName( uint nAppID, System.Text.StringBuilder pchName, int cchNameMax );
        internal abstract int GetAppInstallDir( uint nAppID, string pchDirectory, int cchNameMax );
        internal abstract int GetAppBuildId( uint nAppID );
    }


    internal abstract class ISteamHTMLSurface
    {
        internal abstract IntPtr GetIntPtr();
        internal abstract void DestructISteamHTMLSurface();
        internal abstract bool Init();
        internal abstract bool Shutdown();
        internal abstract ulong CreateBrowser( string pchUserAgent, string pchUserCSS );
        internal abstract void RemoveBrowser( uint unBrowserHandle );
        internal abstract void LoadURL( uint unBrowserHandle, string pchURL, string pchPostData );
        internal abstract void SetSize( uint unBrowserHandle, uint unWidth, uint unHeight );
        internal abstract void StopLoad( uint unBrowserHandle );
        internal abstract void Reload( uint unBrowserHandle );
        internal abstract void GoBack( uint unBrowserHandle );
        internal abstract void GoForward( uint unBrowserHandle );
        internal abstract void AddHeader( uint unBrowserHandle, string pchKey, string pchValue );
        internal abstract void ExecuteJavascript( uint unBrowserHandle, string pchScript );
        internal abstract void MouseUp( uint unBrowserHandle, uint eMouseButton );
        internal abstract void MouseDown( uint unBrowserHandle, uint eMouseButton );
        internal abstract void MouseDoubleClick( uint unBrowserHandle, uint eMouseButton );
        internal abstract void MouseMove( uint unBrowserHandle, int x, int y );
        internal abstract void MouseWheel( uint unBrowserHandle, int nDelta );
        internal abstract void KeyDown( uint unBrowserHandle, uint nNativeKeyCode, uint eHTMLKeyModifiers );
        internal abstract void KeyUp( uint unBrowserHandle, uint nNativeKeyCode, uint eHTMLKeyModifiers );
        internal abstract void KeyChar( uint unBrowserHandle, uint cUnicodeChar, uint eHTMLKeyModifiers );
        internal abstract void SetHorizontalScroll( uint unBrowserHandle, uint nAbsolutePixelScroll );
        internal abstract void SetVerticalScroll( uint unBrowserHandle, uint nAbsolutePixelScroll );
        internal abstract void SetKeyFocus( uint unBrowserHandle, bool bHasKeyFocus );
        internal abstract void ViewSource( uint unBrowserHandle );
        internal abstract void CopyToClipboard( uint unBrowserHandle );
        internal abstract void PasteFromClipboard( uint unBrowserHandle );
        internal abstract void Find( uint unBrowserHandle, string pchSearchStr, bool bCurrentlyInFind, bool bReverse );
        internal abstract void StopFind( uint unBrowserHandle );
        internal abstract void GetLinkAtPosition( uint unBrowserHandle, int x, int y );
        internal abstract void SetCookie( string pchHostname, string pchKey, string pchValue, string pchPath, ulong nExpires, bool bSecure, bool bHTTPOnly );
        internal abstract void SetPageScaleFactor( uint unBrowserHandle, float flZoom, int nPointX, int nPointY );
        internal abstract void SetBackgroundMode( uint unBrowserHandle, bool bBackgroundMode );
        internal abstract void AllowStartRequest( uint unBrowserHandle, bool bAllowed );
        internal abstract void JSDialogResponse( uint unBrowserHandle, bool bResult );
    }


    internal abstract class ISteamInventory
    {
        internal abstract IntPtr GetIntPtr();
        internal abstract uint GetResultStatus( int resultHandle );
        internal abstract bool GetResultItems( int resultHandle, out SteamItemDetails_t[] pOutItemsArray );
        internal abstract uint GetResultTimestamp( int resultHandle );
        internal abstract bool CheckResultSteamID( int resultHandle, ulong steamIDExpected );
        internal abstract void DestroyResult( int resultHandle );
        internal abstract bool GetAllItems( ref int pResultHandle );
        internal abstract bool GetItemsByID( ref int pResultHandle, ulong[] pInstanceIDs );
        internal abstract bool SerializeResult( int resultHandle, IntPtr pOutBuffer, ref uint punOutBufferSize );
        internal abstract bool DeserializeResult( ref int pOutResultHandle, IntPtr pBuffer, uint unBufferSize, bool bRESERVED_MUST_BE_FALSE );
        internal abstract bool GenerateItems( ref int pResultHandle, int[] pArrayItemDefs, uint[] punArrayQuantity );
        internal abstract bool GrantPromoItems( ref int pResultHandle );
        internal abstract bool AddPromoItem( ref int pResultHandle, int itemDef );
        internal abstract bool AddPromoItems( ref int pResultHandle, int[] pArrayItemDefs );
        internal abstract bool ConsumeItem( ref int pResultHandle, ulong itemConsume, uint unQuantity );
        internal abstract bool ExchangeItems( ref int pResultHandle, int[] pArrayGenerate, uint[] punArrayGenerateQuantity, ulong[] pArrayDestroy, uint[] punArrayDestroyQuantity );
        internal abstract bool TransferItemQuantity( ref int pResultHandle, ulong itemIdSource, uint unQuantity, ulong itemIdDest );
        internal abstract void SendItemDropHeartbeat();
        internal abstract bool TriggerItemDrop( ref int pResultHandle, int dropListDefinition );
        internal abstract bool TradeItems( ref int pResultHandle, ulong steamIDTradePartner, ulong[] pArrayGive, uint[] pArrayGiveQuantity, ulong[] pArrayGet, uint[] pArrayGetQuantity );
        internal abstract bool LoadItemDefinitions();
        internal abstract bool GetItemDefinitionIDs( out int[] pItemDefIDs );
        internal abstract bool GetItemDefinitionProperty( int iDefinition, string pchPropertyName, out string pchValueBuffer );
    }


    internal abstract class ISteamVideo
    {
        internal abstract IntPtr GetIntPtr();
        internal abstract void GetVideoURL( uint unVideoAppID );
        internal abstract bool IsBroadcasting( ref int pnNumViewers );
    }


    internal abstract class ISteamGameServer
    {
        internal abstract IntPtr GetIntPtr();
        internal abstract bool InitGameServer( uint unIP, char usGamePort, char usQueryPort, uint unFlags, uint nGameAppId, string pchVersionString );
        internal abstract void SetProduct( string pszProduct );
        internal abstract void SetGameDescription( string pszGameDescription );
        internal abstract void SetModDir( string pszModDir );
        internal abstract void SetDedicatedServer( bool bDedicated );
        internal abstract void LogOn( string pszToken );
        internal abstract void LogOnAnonymous();
        internal abstract void LogOff();
        internal abstract bool BLoggedOn();
        internal abstract bool BSecure();
        internal abstract ulong GetSteamID();
        internal abstract bool WasRestartRequested();
        internal abstract void SetMaxPlayerCount( int cPlayersMax );
        internal abstract void SetBotPlayerCount( int cBotplayers );
        internal abstract void SetServerName( string pszServerName );
        internal abstract void SetMapName( string pszMapName );
        internal abstract void SetPasswordProtected( bool bPasswordProtected );
        internal abstract void SetSpectatorPort( char unSpectatorPort );
        internal abstract void SetSpectatorServerName( string pszSpectatorServerName );
        internal abstract void ClearAllKeyValues();
        internal abstract void SetKeyValue( string pKey, string pValue );
        internal abstract void SetGameTags( string pchGameTags );
        internal abstract void SetGameData( string pchGameData );
        internal abstract void SetRegion( string pszRegion );
        internal abstract bool SendUserConnectAndAuthenticate( uint unIPClient, IntPtr pvAuthBlob, uint cubAuthBlobSize, ref CSteamID pSteamIDUser );
        internal abstract ulong CreateUnauthenticatedUserConnection();
        internal abstract void SendUserDisconnect( ulong steamIDUser );
        internal abstract bool BUpdateUserData( ulong steamIDUser, string pchPlayerName, uint uScore );
        internal abstract uint GetAuthSessionTicket( IntPtr pTicket, int cbMaxTicket, ref uint pcbTicket );
        internal abstract uint BeginAuthSession( IntPtr pAuthTicket, int cbAuthTicket, ulong steamID );
        internal abstract void EndAuthSession( ulong steamID );
        internal abstract void CancelAuthTicket( uint hAuthTicket );
        internal abstract uint UserHasLicenseForApp( ulong steamID, uint appID );
        internal abstract bool RequestUserGroupStatus( ulong steamIDUser, ulong steamIDGroup );
        internal abstract void GetGameplayStats();
        internal abstract ulong GetServerReputation();
        internal abstract uint GetPublicIP();
        internal abstract bool HandleIncomingPacket( IntPtr pData, int cbData, uint srcIP, char srcPort );
        internal abstract int GetNextOutgoingPacket( IntPtr pOut, int cbMaxOut, ref uint pNetAdr, ref char pPort );
        internal abstract void EnableHeartbeats( bool bActive );
        internal abstract void SetHeartbeatInterval( int iHeartbeatInterval );
        internal abstract void ForceHeartbeat();
        internal abstract ulong AssociateWithClan( ulong steamIDClan );
        internal abstract ulong ComputeNewPlayerCompatibility( ulong steamIDNewPlayer );
    }


    internal abstract class ISteamGameServerStats
    {
        internal abstract IntPtr GetIntPtr();
        internal abstract ulong RequestUserStats( ulong steamIDUser );
        internal abstract bool GetUserStat( ulong steamIDUser, string pchName, ref int pData );
        internal abstract bool GetUserStat0( ulong steamIDUser, string pchName, ref float pData );
        internal abstract bool GetUserAchievement( ulong steamIDUser, string pchName, ref bool pbAchieved );
        internal abstract bool SetUserStat( ulong steamIDUser, string pchName, int nData );
        internal abstract bool SetUserStat0( ulong steamIDUser, string pchName, float fData );
        internal abstract bool UpdateUserAvgRateStat( ulong steamIDUser, string pchName, float flCountThisSession, double dSessionLength );
        internal abstract bool SetUserAchievement( ulong steamIDUser, string pchName );
        internal abstract bool ClearUserAchievement( ulong steamIDUser, string pchName );
        internal abstract ulong StoreUserStats( ulong steamIDUser );
    }


    internal class CSteamClient : ISteamClient
    {
        public CSteamClient( IntPtr SteamClient )
        {
            m_pSteamClient = SteamClient;
        }
        IntPtr m_pSteamClient;

        internal override IntPtr GetIntPtr() { return m_pSteamClient; }

        private void CheckIfUsable()
        {
            if ( m_pSteamClient == IntPtr.Zero )
            {
                throw new Exception( "Steam Pointer not configured" );
            }
        }
        internal override uint CreateSteamPipe()
        {
            CheckIfUsable();
            uint result = NativeEntrypoints.SteamAPI_ISteamClient_CreateSteamPipe(m_pSteamClient);
            return result;
        }
        internal override bool BReleaseSteamPipe( uint hSteamPipe )
        {
            CheckIfUsable();
            bool result = NativeEntrypoints.SteamAPI_ISteamClient_BReleaseSteamPipe(m_pSteamClient,hSteamPipe);
            return result;
        }
        internal override uint ConnectToGlobalUser( uint hSteamPipe )
        {
            CheckIfUsable();
            uint result = NativeEntrypoints.SteamAPI_ISteamClient_ConnectToGlobalUser(m_pSteamClient,hSteamPipe);
            return result;
        }
        internal override uint CreateLocalUser( ref uint phSteamPipe, uint eAccountType )
        {
            CheckIfUsable();
            phSteamPipe = 0;
            uint result = NativeEntrypoints.SteamAPI_ISteamClient_CreateLocalUser(m_pSteamClient,ref phSteamPipe,eAccountType);
            return result;
        }
        internal override void ReleaseUser( uint hSteamPipe, uint hUser )
        {
            CheckIfUsable();
            NativeEntrypoints.SteamAPI_ISteamClient_ReleaseUser( m_pSteamClient, hSteamPipe, hUser );
        }
        internal override ISteamUser GetISteamUser( uint hSteamUser, uint hSteamPipe, string pchVersion )
        {
            CheckIfUsable();
            IntPtr result = NativeEntrypoints.SteamAPI_ISteamClient_GetISteamUser(m_pSteamClient,hSteamUser,hSteamPipe,pchVersion);
            return new CSteamUser( result );
        }
        internal override ISteamGameServer GetISteamGameServer( uint hSteamUser, uint hSteamPipe, string pchVersion )
        {
            CheckIfUsable();
            IntPtr result = NativeEntrypoints.SteamAPI_ISteamClient_GetISteamGameServer(m_pSteamClient,hSteamUser,hSteamPipe,pchVersion);
            return new CSteamGameServer( result );
        }
        internal override void SetLocalIPBinding( uint unIP, char usPort )
        {
            CheckIfUsable();
            NativeEntrypoints.SteamAPI_ISteamClient_SetLocalIPBinding( m_pSteamClient, unIP, usPort );
        }
        internal override ISteamFriends GetISteamFriends( uint hSteamUser, uint hSteamPipe, string pchVersion )
        {
            CheckIfUsable();
            IntPtr result = NativeEntrypoints.SteamAPI_ISteamClient_GetISteamFriends(m_pSteamClient,hSteamUser,hSteamPipe,pchVersion);
            return new CSteamFriends( result );
        }
        internal override ISteamUtils GetISteamUtils( uint hSteamPipe, string pchVersion )
        {
            CheckIfUsable();
            IntPtr result = NativeEntrypoints.SteamAPI_ISteamClient_GetISteamUtils(m_pSteamClient,hSteamPipe,pchVersion);
            return new CSteamUtils( result );
        }
        internal override ISteamMatchmaking GetISteamMatchmaking( uint hSteamUser, uint hSteamPipe, string pchVersion )
        {
            CheckIfUsable();
            IntPtr result = NativeEntrypoints.SteamAPI_ISteamClient_GetISteamMatchmaking(m_pSteamClient,hSteamUser,hSteamPipe,pchVersion);
            return (ISteamMatchmaking)Marshal.PtrToStructure( result, typeof( ISteamMatchmaking ) );
        }
        internal override ISteamMatchmakingServers GetISteamMatchmakingServers( uint hSteamUser, uint hSteamPipe, string pchVersion )
        {
            CheckIfUsable();
            IntPtr result = NativeEntrypoints.SteamAPI_ISteamClient_GetISteamMatchmakingServers(m_pSteamClient,hSteamUser,hSteamPipe,pchVersion);
            return new CSteamMatchmakingServers( result );
        }
        internal override IntPtr GetISteamGenericInterface( uint hSteamUser, uint hSteamPipe, string pchVersion )
        {
            CheckIfUsable();
            IntPtr result = NativeEntrypoints.SteamAPI_ISteamClient_GetISteamGenericInterface(m_pSteamClient,hSteamUser,hSteamPipe,pchVersion);
            return (IntPtr)Marshal.PtrToStructure( result, typeof( IntPtr ) );
        }
        internal override ISteamUserStats GetISteamUserStats( uint hSteamUser, uint hSteamPipe, string pchVersion )
        {
            CheckIfUsable();
            IntPtr result = NativeEntrypoints.SteamAPI_ISteamClient_GetISteamUserStats(m_pSteamClient,hSteamUser,hSteamPipe,pchVersion);
            return new CSteamUserStats( result );
        }
        internal override ISteamGameServerStats GetISteamGameServerStats( uint hSteamuser, uint hSteamPipe, string pchVersion )
        {
            CheckIfUsable();
            IntPtr result = NativeEntrypoints.SteamAPI_ISteamClient_GetISteamGameServerStats(m_pSteamClient,hSteamuser,hSteamPipe,pchVersion);
            return new CSteamGameServerStats( result );
        }
        internal override ISteamApps GetISteamApps( uint hSteamUser, uint hSteamPipe, string pchVersion )
        {
            CheckIfUsable();
            IntPtr result = NativeEntrypoints.SteamAPI_ISteamClient_GetISteamApps(m_pSteamClient,hSteamUser,hSteamPipe,pchVersion);
            return new CSteamApps( result );
        }
        internal override ISteamNetworking GetISteamNetworking( uint hSteamUser, uint hSteamPipe, string pchVersion )
        {
            CheckIfUsable();
            IntPtr result = NativeEntrypoints.SteamAPI_ISteamClient_GetISteamNetworking(m_pSteamClient,hSteamUser,hSteamPipe,pchVersion);
            return new CSteamNetworking( result );
        }
        internal override ISteamRemoteStorage GetISteamRemoteStorage( uint hSteamuser, uint hSteamPipe, string pchVersion )
        {
            CheckIfUsable();
            IntPtr result = NativeEntrypoints.SteamAPI_ISteamClient_GetISteamRemoteStorage(m_pSteamClient,hSteamuser,hSteamPipe,pchVersion);
            return (ISteamRemoteStorage)Marshal.PtrToStructure( result, typeof( ISteamRemoteStorage ) );
        }
        internal override ISteamScreenshots GetISteamScreenshots( uint hSteamuser, uint hSteamPipe, string pchVersion )
        {
            CheckIfUsable();
            IntPtr result = NativeEntrypoints.SteamAPI_ISteamClient_GetISteamScreenshots(m_pSteamClient,hSteamuser,hSteamPipe,pchVersion);
            return new CSteamScreenshots( result );
        }
        internal override uint GetIPCCallCount()
        {
            CheckIfUsable();
            uint result = NativeEntrypoints.SteamAPI_ISteamClient_GetIPCCallCount(m_pSteamClient);
            return result;
        }
        internal override void SetWarningMessageHook( IntPtr pFunction )
        {
            CheckIfUsable();
            NativeEntrypoints.SteamAPI_ISteamClient_SetWarningMessageHook( m_pSteamClient, pFunction );
        }
        internal override bool BShutdownIfAllPipesClosed()
        {
            CheckIfUsable();
            bool result = NativeEntrypoints.SteamAPI_ISteamClient_BShutdownIfAllPipesClosed(m_pSteamClient);
            return result;
        }
        internal override ISteamHTTP GetISteamHTTP( uint hSteamuser, uint hSteamPipe, string pchVersion )
        {
            CheckIfUsable();
            IntPtr result = NativeEntrypoints.SteamAPI_ISteamClient_GetISteamHTTP(m_pSteamClient,hSteamuser,hSteamPipe,pchVersion);
            return new CSteamHTTP( result );
        }
        internal override ISteamUnifiedMessages GetISteamUnifiedMessages( uint hSteamuser, uint hSteamPipe, string pchVersion )
        {
            CheckIfUsable();
            IntPtr result = NativeEntrypoints.SteamAPI_ISteamClient_GetISteamUnifiedMessages(m_pSteamClient,hSteamuser,hSteamPipe,pchVersion);
            return (ISteamUnifiedMessages)Marshal.PtrToStructure( result, typeof( ISteamUnifiedMessages ) );
        }
        internal override ISteamController GetISteamController( uint hSteamUser, uint hSteamPipe, string pchVersion )
        {
            CheckIfUsable();
            IntPtr result = NativeEntrypoints.SteamAPI_ISteamClient_GetISteamController(m_pSteamClient,hSteamUser,hSteamPipe,pchVersion);
            return (ISteamController)Marshal.PtrToStructure( result, typeof( ISteamController ) );
        }
        internal override ISteamUGC GetISteamUGC( uint hSteamUser, uint hSteamPipe, string pchVersion )
        {
            CheckIfUsable();
            IntPtr result = NativeEntrypoints.SteamAPI_ISteamClient_GetISteamUGC(m_pSteamClient,hSteamUser,hSteamPipe,pchVersion);
            return new CSteamUGC( result );
        }
        internal override ISteamAppList GetISteamAppList( uint hSteamUser, uint hSteamPipe, string pchVersion )
        {
            CheckIfUsable();
            IntPtr result = NativeEntrypoints.SteamAPI_ISteamClient_GetISteamAppList(m_pSteamClient,hSteamUser,hSteamPipe,pchVersion);
            return (ISteamAppList)Marshal.PtrToStructure( result, typeof( ISteamAppList ) );
        }
        internal override ISteamMusic GetISteamMusic( uint hSteamuser, uint hSteamPipe, string pchVersion )
        {
            CheckIfUsable();
            IntPtr result = NativeEntrypoints.SteamAPI_ISteamClient_GetISteamMusic(m_pSteamClient,hSteamuser,hSteamPipe,pchVersion);
            return (ISteamMusic)Marshal.PtrToStructure( result, typeof( ISteamMusic ) );
        }
        internal override ISteamMusicRemote GetISteamMusicRemote( uint hSteamuser, uint hSteamPipe, string pchVersion )
        {
            CheckIfUsable();
            IntPtr result = NativeEntrypoints.SteamAPI_ISteamClient_GetISteamMusicRemote(m_pSteamClient,hSteamuser,hSteamPipe,pchVersion);
            return (ISteamMusicRemote)Marshal.PtrToStructure( result, typeof( ISteamMusicRemote ) );
        }
        internal override ISteamHTMLSurface GetISteamHTMLSurface( uint hSteamuser, uint hSteamPipe, string pchVersion )
        {
            CheckIfUsable();
            IntPtr result = NativeEntrypoints.SteamAPI_ISteamClient_GetISteamHTMLSurface(m_pSteamClient,hSteamuser,hSteamPipe,pchVersion);
            return (ISteamHTMLSurface)Marshal.PtrToStructure( result, typeof( ISteamHTMLSurface ) );
        }
        internal override ISteamInventory GetISteamInventory( uint hSteamuser, uint hSteamPipe, string pchVersion )
        {
            CheckIfUsable();
            IntPtr result = NativeEntrypoints.SteamAPI_ISteamClient_GetISteamInventory(m_pSteamClient,hSteamuser,hSteamPipe,pchVersion);
            return new CSteamInventory( result );
        }
        internal override ISteamVideo GetISteamVideo( uint hSteamuser, uint hSteamPipe, string pchVersion )
        {
            CheckIfUsable();
            IntPtr result = NativeEntrypoints.SteamAPI_ISteamClient_GetISteamVideo(m_pSteamClient,hSteamuser,hSteamPipe,pchVersion);
            return (ISteamVideo)Marshal.PtrToStructure( result, typeof( ISteamVideo ) );
        }
    }


    internal class CSteamUser : ISteamUser
    {
        public CSteamUser( IntPtr SteamUser )
        {
            m_pSteamUser = SteamUser;
        }
        IntPtr m_pSteamUser;

        internal override IntPtr GetIntPtr() { return m_pSteamUser; }

        private void CheckIfUsable()
        {
            if ( m_pSteamUser == IntPtr.Zero )
            {
                throw new Exception( "Steam Pointer not configured" );
            }
        }
        internal override uint GetHSteamUser()
        {
            CheckIfUsable();
            uint result = NativeEntrypoints.SteamAPI_ISteamUser_GetHSteamUser(m_pSteamUser);
            return result;
        }
        internal override bool BLoggedOn()
        {
            CheckIfUsable();
            bool result = NativeEntrypoints.SteamAPI_ISteamUser_BLoggedOn(m_pSteamUser);
            return result;
        }
        internal override ulong GetSteamID()
        {
            CheckIfUsable();
            ulong result = NativeEntrypoints.SteamAPI_ISteamUser_GetSteamID(m_pSteamUser);
            return result;
        }
        internal override int InitiateGameConnection( IntPtr pAuthBlob, int cbMaxAuthBlob, ulong steamIDGameServer, uint unIPServer, char usPortServer, bool bSecure )
        {
            CheckIfUsable();
            int result = NativeEntrypoints.SteamAPI_ISteamUser_InitiateGameConnection(m_pSteamUser,pAuthBlob,cbMaxAuthBlob,steamIDGameServer,unIPServer,usPortServer,bSecure);
            return result;
        }
        internal override void TerminateGameConnection( uint unIPServer, char usPortServer )
        {
            CheckIfUsable();
            NativeEntrypoints.SteamAPI_ISteamUser_TerminateGameConnection( m_pSteamUser, unIPServer, usPortServer );
        }
        internal override void TrackAppUsageEvent( ulong gameID, int eAppUsageEvent, string pchExtraInfo )
        {
            CheckIfUsable();
            NativeEntrypoints.SteamAPI_ISteamUser_TrackAppUsageEvent( m_pSteamUser, gameID, eAppUsageEvent, pchExtraInfo );
        }
        internal override bool GetUserDataFolder( string pchBuffer, int cubBuffer )
        {
            CheckIfUsable();
            bool result = NativeEntrypoints.SteamAPI_ISteamUser_GetUserDataFolder(m_pSteamUser,pchBuffer,cubBuffer);
            return result;
        }
        internal override void StartVoiceRecording()
        {
            CheckIfUsable();
            NativeEntrypoints.SteamAPI_ISteamUser_StartVoiceRecording( m_pSteamUser );
        }
        internal override void StopVoiceRecording()
        {
            CheckIfUsable();
            NativeEntrypoints.SteamAPI_ISteamUser_StopVoiceRecording( m_pSteamUser );
        }
        internal override uint GetAvailableVoice( ref uint pcbCompressed, ref uint pcbUncompressed, uint nUncompressedVoiceDesiredSampleRate )
        {
            CheckIfUsable();
            pcbCompressed = 0;
            pcbUncompressed = 0;
            uint result = NativeEntrypoints.SteamAPI_ISteamUser_GetAvailableVoice(m_pSteamUser,ref pcbCompressed,ref pcbUncompressed,nUncompressedVoiceDesiredSampleRate);
            return result;
        }
        internal override uint GetVoice( bool bWantCompressed, IntPtr pDestBuffer, uint cbDestBufferSize, ref uint nBytesWritten, bool bWantUncompressed, IntPtr pUncompressedDestBuffer, uint cbUncompressedDestBufferSize, ref uint nUncompressBytesWritten, uint nUncompressedVoiceDesiredSampleRate )
        {
            CheckIfUsable();
            nBytesWritten = 0;
            nUncompressBytesWritten = 0;
            uint result = NativeEntrypoints.SteamAPI_ISteamUser_GetVoice(m_pSteamUser,bWantCompressed,pDestBuffer,cbDestBufferSize,ref nBytesWritten,bWantUncompressed,pUncompressedDestBuffer,cbUncompressedDestBufferSize,ref nUncompressBytesWritten,nUncompressedVoiceDesiredSampleRate);
            return result;
        }
        internal override uint DecompressVoice( IntPtr pCompressed, uint cbCompressed, IntPtr pDestBuffer, uint cbDestBufferSize, ref uint nBytesWritten, uint nDesiredSampleRate )
        {
            CheckIfUsable();
            nBytesWritten = 0;
            uint result = NativeEntrypoints.SteamAPI_ISteamUser_DecompressVoice(m_pSteamUser,pCompressed,cbCompressed,pDestBuffer,cbDestBufferSize,ref nBytesWritten,nDesiredSampleRate);
            return result;
        }
        internal override uint GetVoiceOptimalSampleRate()
        {
            CheckIfUsable();
            uint result = NativeEntrypoints.SteamAPI_ISteamUser_GetVoiceOptimalSampleRate(m_pSteamUser);
            return result;
        }
        internal override uint GetAuthSessionTicket( IntPtr pTicket, int cbMaxTicket, ref uint pcbTicket )
        {
            CheckIfUsable();
            pcbTicket = 0;
            uint result = NativeEntrypoints.SteamAPI_ISteamUser_GetAuthSessionTicket(m_pSteamUser,pTicket,cbMaxTicket,ref pcbTicket);
            return result;
        }
        internal override uint BeginAuthSession( IntPtr pAuthTicket, int cbAuthTicket, ulong steamID )
        {
            CheckIfUsable();
            uint result = NativeEntrypoints.SteamAPI_ISteamUser_BeginAuthSession(m_pSteamUser,pAuthTicket,cbAuthTicket,steamID);
            return result;
        }
        internal override void EndAuthSession( ulong steamID )
        {
            CheckIfUsable();
            NativeEntrypoints.SteamAPI_ISteamUser_EndAuthSession( m_pSteamUser, steamID );
        }
        internal override void CancelAuthTicket( uint hAuthTicket )
        {
            CheckIfUsable();
            NativeEntrypoints.SteamAPI_ISteamUser_CancelAuthTicket( m_pSteamUser, hAuthTicket );
        }
        internal override uint UserHasLicenseForApp( ulong steamID, uint appID )
        {
            CheckIfUsable();
            uint result = NativeEntrypoints.SteamAPI_ISteamUser_UserHasLicenseForApp(m_pSteamUser,steamID,appID);
            return result;
        }
        internal override bool BIsBehindNAT()
        {
            CheckIfUsable();
            bool result = NativeEntrypoints.SteamAPI_ISteamUser_BIsBehindNAT(m_pSteamUser);
            return result;
        }
        internal override void AdvertiseGame( ulong steamIDGameServer, uint unIPServer, char usPortServer )
        {
            CheckIfUsable();
            NativeEntrypoints.SteamAPI_ISteamUser_AdvertiseGame( m_pSteamUser, steamIDGameServer, unIPServer, usPortServer );
        }
        internal override ulong RequestEncryptedAppTicket( IntPtr pDataToInclude, int cbDataToInclude )
        {
            CheckIfUsable();
            ulong result = NativeEntrypoints.SteamAPI_ISteamUser_RequestEncryptedAppTicket(m_pSteamUser,pDataToInclude,cbDataToInclude);
            return result;
        }
        internal override bool GetEncryptedAppTicket( IntPtr pTicket, int cbMaxTicket, ref uint pcbTicket )
        {
            CheckIfUsable();
            pcbTicket = 0;
            bool result = NativeEntrypoints.SteamAPI_ISteamUser_GetEncryptedAppTicket(m_pSteamUser,pTicket,cbMaxTicket,ref pcbTicket);
            return result;
        }
        internal override int GetGameBadgeLevel( int nSeries, bool bFoil )
        {
            CheckIfUsable();
            int result = NativeEntrypoints.SteamAPI_ISteamUser_GetGameBadgeLevel(m_pSteamUser,nSeries,bFoil);
            return result;
        }
        internal override int GetPlayerSteamLevel()
        {
            CheckIfUsable();
            int result = NativeEntrypoints.SteamAPI_ISteamUser_GetPlayerSteamLevel(m_pSteamUser);
            return result;
        }
        internal override ulong RequestStoreAuthURL( string pchRedirectURL )
        {
            CheckIfUsable();
            ulong result = NativeEntrypoints.SteamAPI_ISteamUser_RequestStoreAuthURL(m_pSteamUser,pchRedirectURL);
            return result;
        }
        internal override bool BIsPhoneVerified()
        {
            CheckIfUsable();
            bool result = NativeEntrypoints.SteamAPI_ISteamUser_BIsPhoneVerified(m_pSteamUser);
            return result;
        }
        internal override bool BIsTwoFactorEnabled()
        {
            CheckIfUsable();
            bool result = NativeEntrypoints.SteamAPI_ISteamUser_BIsTwoFactorEnabled(m_pSteamUser);
            return result;
        }
    }


    internal class CSteamFriends : ISteamFriends
    {
        public CSteamFriends( IntPtr SteamFriends )
        {
            m_pSteamFriends = SteamFriends;
        }
        IntPtr m_pSteamFriends;

        internal override IntPtr GetIntPtr() { return m_pSteamFriends; }

        private void CheckIfUsable()
        {
            if ( m_pSteamFriends == IntPtr.Zero )
            {
                throw new Exception( "Steam Pointer not configured" );
            }
        }
        internal override string GetPersonaName()
        {
            CheckIfUsable();
            IntPtr result = NativeEntrypoints.SteamAPI_ISteamFriends_GetPersonaName(m_pSteamFriends);
            return Marshal.PtrToStringAnsi( result );
        }
        internal override ulong SetPersonaName( string pchPersonaName )
        {
            CheckIfUsable();
            ulong result = NativeEntrypoints.SteamAPI_ISteamFriends_SetPersonaName(m_pSteamFriends,pchPersonaName);
            return result;
        }
        internal override uint GetPersonaState()
        {
            CheckIfUsable();
            uint result = NativeEntrypoints.SteamAPI_ISteamFriends_GetPersonaState(m_pSteamFriends);
            return result;
        }
        internal override int GetFriendCount( int iFriendFlags )
        {
            CheckIfUsable();
            int result = NativeEntrypoints.SteamAPI_ISteamFriends_GetFriendCount(m_pSteamFriends,iFriendFlags);
            return result;
        }
        internal override ulong GetFriendByIndex( int iFriend, int iFriendFlags )
        {
            CheckIfUsable();
            ulong result = NativeEntrypoints.SteamAPI_ISteamFriends_GetFriendByIndex(m_pSteamFriends,iFriend,iFriendFlags);
            return result;
        }
        internal override uint GetFriendRelationship( ulong steamIDFriend )
        {
            CheckIfUsable();
            uint result = NativeEntrypoints.SteamAPI_ISteamFriends_GetFriendRelationship(m_pSteamFriends,steamIDFriend);
            return result;
        }
        internal override uint GetFriendPersonaState( ulong steamIDFriend )
        {
            CheckIfUsable();
            uint result = NativeEntrypoints.SteamAPI_ISteamFriends_GetFriendPersonaState(m_pSteamFriends,steamIDFriend);
            return result;
        }
        internal override string GetFriendPersonaName( ulong steamIDFriend )
        {
            CheckIfUsable();
            IntPtr result = NativeEntrypoints.SteamAPI_ISteamFriends_GetFriendPersonaName(m_pSteamFriends,steamIDFriend);
            return Marshal.PtrToStringAnsi( result );
        }
        internal override bool GetFriendGamePlayed( ulong steamIDFriend, out FriendGameInfo_t pFriendGameInfo )
        {
            CheckIfUsable();
            pFriendGameInfo = new FriendGameInfo_t();
            bool result = NativeEntrypoints.SteamAPI_ISteamFriends_GetFriendGamePlayed(m_pSteamFriends,steamIDFriend,ref pFriendGameInfo);
            return result;
        }
        internal override string GetFriendPersonaNameHistory( ulong steamIDFriend, int iPersonaName )
        {
            CheckIfUsable();
            IntPtr result = NativeEntrypoints.SteamAPI_ISteamFriends_GetFriendPersonaNameHistory(m_pSteamFriends,steamIDFriend,iPersonaName);
            return Marshal.PtrToStringAnsi( result );
        }
        internal override int GetFriendSteamLevel( ulong steamIDFriend )
        {
            CheckIfUsable();
            int result = NativeEntrypoints.SteamAPI_ISteamFriends_GetFriendSteamLevel(m_pSteamFriends,steamIDFriend);
            return result;
        }
        internal override string GetPlayerNickname( ulong steamIDPlayer )
        {
            CheckIfUsable();
            IntPtr result = NativeEntrypoints.SteamAPI_ISteamFriends_GetPlayerNickname(m_pSteamFriends,steamIDPlayer);
            return Marshal.PtrToStringAnsi( result );
        }
        internal override int GetFriendsGroupCount()
        {
            CheckIfUsable();
            int result = NativeEntrypoints.SteamAPI_ISteamFriends_GetFriendsGroupCount(m_pSteamFriends);
            return result;
        }
        internal override char GetFriendsGroupIDByIndex( int iFG )
        {
            CheckIfUsable();
            char result = NativeEntrypoints.SteamAPI_ISteamFriends_GetFriendsGroupIDByIndex(m_pSteamFriends,iFG);
            return result;
        }
        internal override string GetFriendsGroupName( char friendsGroupID )
        {
            CheckIfUsable();
            IntPtr result = NativeEntrypoints.SteamAPI_ISteamFriends_GetFriendsGroupName(m_pSteamFriends,friendsGroupID);
            return Marshal.PtrToStringAnsi( result );
        }
        internal override int GetFriendsGroupMembersCount( char friendsGroupID )
        {
            CheckIfUsable();
            int result = NativeEntrypoints.SteamAPI_ISteamFriends_GetFriendsGroupMembersCount(m_pSteamFriends,friendsGroupID);
            return result;
        }
        internal override void GetFriendsGroupMembersList( char friendsGroupID, out CSteamID[] pOutSteamIDMembers )
        {
            CheckIfUsable();
            int nMembersCount = GetFriendsGroupMembersCount (friendsGroupID);
            pOutSteamIDMembers = new CSteamID[nMembersCount];
            NativeEntrypoints.SteamAPI_ISteamFriends_GetFriendsGroupMembersList( m_pSteamFriends, friendsGroupID, pOutSteamIDMembers, nMembersCount );
        }
        internal override bool HasFriend( ulong steamIDFriend, int iFriendFlags )
        {
            CheckIfUsable();
            bool result = NativeEntrypoints.SteamAPI_ISteamFriends_HasFriend(m_pSteamFriends,steamIDFriend,iFriendFlags);
            return result;
        }
        internal override int GetClanCount()
        {
            CheckIfUsable();
            int result = NativeEntrypoints.SteamAPI_ISteamFriends_GetClanCount(m_pSteamFriends);
            return result;
        }
        internal override ulong GetClanByIndex( int iClan )
        {
            CheckIfUsable();
            ulong result = NativeEntrypoints.SteamAPI_ISteamFriends_GetClanByIndex(m_pSteamFriends,iClan);
            return result;
        }
        internal override string GetClanName( ulong steamIDClan )
        {
            CheckIfUsable();
            IntPtr result = NativeEntrypoints.SteamAPI_ISteamFriends_GetClanName(m_pSteamFriends,steamIDClan);
            return Marshal.PtrToStringAnsi( result );
        }
        internal override string GetClanTag( ulong steamIDClan )
        {
            CheckIfUsable();
            IntPtr result = NativeEntrypoints.SteamAPI_ISteamFriends_GetClanTag(m_pSteamFriends,steamIDClan);
            return Marshal.PtrToStringAnsi( result );
        }
        internal override bool GetClanActivityCounts( ulong steamIDClan, ref int pnOnline, ref int pnInGame, ref int pnChatting )
        {
            CheckIfUsable();
            pnOnline = 0;
            pnInGame = 0;
            pnChatting = 0;
            bool result = NativeEntrypoints.SteamAPI_ISteamFriends_GetClanActivityCounts(m_pSteamFriends,steamIDClan,ref pnOnline,ref pnInGame,ref pnChatting);
            return result;
        }
        internal override ulong DownloadClanActivityCounts( CSteamID[] psteamIDClans )
        {
            CheckIfUsable();
            ulong result = NativeEntrypoints.SteamAPI_ISteamFriends_DownloadClanActivityCounts(m_pSteamFriends,psteamIDClans,(int) psteamIDClans.Length);
            return result;
        }
        internal override int GetFriendCountFromSource( ulong steamIDSource )
        {
            CheckIfUsable();
            int result = NativeEntrypoints.SteamAPI_ISteamFriends_GetFriendCountFromSource(m_pSteamFriends,steamIDSource);
            return result;
        }
        internal override ulong GetFriendFromSourceByIndex( ulong steamIDSource, int iFriend )
        {
            CheckIfUsable();
            ulong result = NativeEntrypoints.SteamAPI_ISteamFriends_GetFriendFromSourceByIndex(m_pSteamFriends,steamIDSource,iFriend);
            return result;
        }
        internal override bool IsUserInSource( ulong steamIDUser, ulong steamIDSource )
        {
            CheckIfUsable();
            bool result = NativeEntrypoints.SteamAPI_ISteamFriends_IsUserInSource(m_pSteamFriends,steamIDUser,steamIDSource);
            return result;
        }
        internal override void SetInGameVoiceSpeaking( ulong steamIDUser, bool bSpeaking )
        {
            CheckIfUsable();
            NativeEntrypoints.SteamAPI_ISteamFriends_SetInGameVoiceSpeaking( m_pSteamFriends, steamIDUser, bSpeaking );
        }
        internal override void ActivateGameOverlay( string pchDialog )
        {
            CheckIfUsable();
            NativeEntrypoints.SteamAPI_ISteamFriends_ActivateGameOverlay( m_pSteamFriends, pchDialog );
        }
        internal override void ActivateGameOverlayToUser( string pchDialog, ulong steamID )
        {
            CheckIfUsable();
            NativeEntrypoints.SteamAPI_ISteamFriends_ActivateGameOverlayToUser( m_pSteamFriends, pchDialog, steamID );
        }
        internal override void ActivateGameOverlayToWebPage( string pchURL )
        {
            CheckIfUsable();
            NativeEntrypoints.SteamAPI_ISteamFriends_ActivateGameOverlayToWebPage( m_pSteamFriends, pchURL );
        }
        internal override void ActivateGameOverlayToStore( uint nAppID, char eFlag )
        {
            CheckIfUsable();
            NativeEntrypoints.SteamAPI_ISteamFriends_ActivateGameOverlayToStore( m_pSteamFriends, nAppID, eFlag );
        }
        internal override void SetPlayedWith( ulong steamIDUserPlayedWith )
        {
            CheckIfUsable();
            NativeEntrypoints.SteamAPI_ISteamFriends_SetPlayedWith( m_pSteamFriends, steamIDUserPlayedWith );
        }
        internal override void ActivateGameOverlayInviteDialog( ulong steamIDLobby )
        {
            CheckIfUsable();
            NativeEntrypoints.SteamAPI_ISteamFriends_ActivateGameOverlayInviteDialog( m_pSteamFriends, steamIDLobby );
        }
        internal override int GetSmallFriendAvatar( ulong steamIDFriend )
        {
            CheckIfUsable();
            int result = NativeEntrypoints.SteamAPI_ISteamFriends_GetSmallFriendAvatar(m_pSteamFriends,steamIDFriend);
            return result;
        }
        internal override int GetMediumFriendAvatar( ulong steamIDFriend )
        {
            CheckIfUsable();
            int result = NativeEntrypoints.SteamAPI_ISteamFriends_GetMediumFriendAvatar(m_pSteamFriends,steamIDFriend);
            return result;
        }
        internal override int GetLargeFriendAvatar( ulong steamIDFriend )
        {
            CheckIfUsable();
            int result = NativeEntrypoints.SteamAPI_ISteamFriends_GetLargeFriendAvatar(m_pSteamFriends,steamIDFriend);
            return result;
        }
        internal override bool RequestUserInformation( ulong steamIDUser, bool bRequireNameOnly )
        {
            CheckIfUsable();
            bool result = NativeEntrypoints.SteamAPI_ISteamFriends_RequestUserInformation(m_pSteamFriends,steamIDUser,bRequireNameOnly);
            return result;
        }
        internal override ulong RequestClanOfficerList( ulong steamIDClan )
        {
            CheckIfUsable();
            ulong result = NativeEntrypoints.SteamAPI_ISteamFriends_RequestClanOfficerList(m_pSteamFriends,steamIDClan);
            return result;
        }
        internal override ulong GetClanOwner( ulong steamIDClan )
        {
            CheckIfUsable();
            ulong result = NativeEntrypoints.SteamAPI_ISteamFriends_GetClanOwner(m_pSteamFriends,steamIDClan);
            return result;
        }
        internal override int GetClanOfficerCount( ulong steamIDClan )
        {
            CheckIfUsable();
            int result = NativeEntrypoints.SteamAPI_ISteamFriends_GetClanOfficerCount(m_pSteamFriends,steamIDClan);
            return result;
        }
        internal override ulong GetClanOfficerByIndex( ulong steamIDClan, int iOfficer )
        {
            CheckIfUsable();
            ulong result = NativeEntrypoints.SteamAPI_ISteamFriends_GetClanOfficerByIndex(m_pSteamFriends,steamIDClan,iOfficer);
            return result;
        }
        internal override uint GetUserRestrictions()
        {
            CheckIfUsable();
            uint result = NativeEntrypoints.SteamAPI_ISteamFriends_GetUserRestrictions(m_pSteamFriends);
            return result;
        }
        internal override bool SetRichPresence( string pchKey, string pchValue )
        {
            CheckIfUsable();
            bool result = NativeEntrypoints.SteamAPI_ISteamFriends_SetRichPresence(m_pSteamFriends,pchKey,pchValue);
            return result;
        }
        internal override void ClearRichPresence()
        {
            CheckIfUsable();
            NativeEntrypoints.SteamAPI_ISteamFriends_ClearRichPresence( m_pSteamFriends );
        }
        internal override string GetFriendRichPresence( ulong steamIDFriend, string pchKey )
        {
            CheckIfUsable();
            IntPtr result = NativeEntrypoints.SteamAPI_ISteamFriends_GetFriendRichPresence(m_pSteamFriends,steamIDFriend,pchKey);
            return Marshal.PtrToStringAnsi( result );
        }
        internal override int GetFriendRichPresenceKeyCount( ulong steamIDFriend )
        {
            CheckIfUsable();
            int result = NativeEntrypoints.SteamAPI_ISteamFriends_GetFriendRichPresenceKeyCount(m_pSteamFriends,steamIDFriend);
            return result;
        }
        internal override string GetFriendRichPresenceKeyByIndex( ulong steamIDFriend, int iKey )
        {
            CheckIfUsable();
            IntPtr result = NativeEntrypoints.SteamAPI_ISteamFriends_GetFriendRichPresenceKeyByIndex(m_pSteamFriends,steamIDFriend,iKey);
            return Marshal.PtrToStringAnsi( result );
        }
        internal override void RequestFriendRichPresence( ulong steamIDFriend )
        {
            CheckIfUsable();
            NativeEntrypoints.SteamAPI_ISteamFriends_RequestFriendRichPresence( m_pSteamFriends, steamIDFriend );
        }
        internal override bool InviteUserToGame( ulong steamIDFriend, string pchConnectString )
        {
            CheckIfUsable();
            bool result = NativeEntrypoints.SteamAPI_ISteamFriends_InviteUserToGame(m_pSteamFriends,steamIDFriend,pchConnectString);
            return result;
        }
        internal override int GetCoplayFriendCount()
        {
            CheckIfUsable();
            int result = NativeEntrypoints.SteamAPI_ISteamFriends_GetCoplayFriendCount(m_pSteamFriends);
            return result;
        }
        internal override ulong GetCoplayFriend( int iCoplayFriend )
        {
            CheckIfUsable();
            ulong result = NativeEntrypoints.SteamAPI_ISteamFriends_GetCoplayFriend(m_pSteamFriends,iCoplayFriend);
            return result;
        }
        internal override int GetFriendCoplayTime( ulong steamIDFriend )
        {
            CheckIfUsable();
            int result = NativeEntrypoints.SteamAPI_ISteamFriends_GetFriendCoplayTime(m_pSteamFriends,steamIDFriend);
            return result;
        }
        internal override uint GetFriendCoplayGame( ulong steamIDFriend )
        {
            CheckIfUsable();
            uint result = NativeEntrypoints.SteamAPI_ISteamFriends_GetFriendCoplayGame(m_pSteamFriends,steamIDFriend);
            return result;
        }
        internal override ulong JoinClanChatRoom( ulong steamIDClan )
        {
            CheckIfUsable();
            ulong result = NativeEntrypoints.SteamAPI_ISteamFriends_JoinClanChatRoom(m_pSteamFriends,steamIDClan);
            return result;
        }
        internal override bool LeaveClanChatRoom( ulong steamIDClan )
        {
            CheckIfUsable();
            bool result = NativeEntrypoints.SteamAPI_ISteamFriends_LeaveClanChatRoom(m_pSteamFriends,steamIDClan);
            return result;
        }
        internal override int GetClanChatMemberCount( ulong steamIDClan )
        {
            CheckIfUsable();
            int result = NativeEntrypoints.SteamAPI_ISteamFriends_GetClanChatMemberCount(m_pSteamFriends,steamIDClan);
            return result;
        }
        internal override ulong GetChatMemberByIndex( ulong steamIDClan, int iUser )
        {
            CheckIfUsable();
            ulong result = NativeEntrypoints.SteamAPI_ISteamFriends_GetChatMemberByIndex(m_pSteamFriends,steamIDClan,iUser);
            return result;
        }
        internal override bool SendClanChatMessage( ulong steamIDClanChat, string pchText )
        {
            CheckIfUsable();
            bool result = NativeEntrypoints.SteamAPI_ISteamFriends_SendClanChatMessage(m_pSteamFriends,steamIDClanChat,pchText);
            return result;
        }
        internal override int GetClanChatMessage( ulong steamIDClanChat, int iMessage, IntPtr prgchText, int cchTextMax, ref uint peChatEntryType, out CSteamID psteamidChatter )
        {
            CheckIfUsable();
            peChatEntryType = 0;
            psteamidChatter = new CSteamID();
            int result = NativeEntrypoints.SteamAPI_ISteamFriends_GetClanChatMessage(m_pSteamFriends,steamIDClanChat,iMessage,prgchText,cchTextMax,ref peChatEntryType,ref psteamidChatter);
            return result;
        }
        internal override bool IsClanChatAdmin( ulong steamIDClanChat, ulong steamIDUser )
        {
            CheckIfUsable();
            bool result = NativeEntrypoints.SteamAPI_ISteamFriends_IsClanChatAdmin(m_pSteamFriends,steamIDClanChat,steamIDUser);
            return result;
        }
        internal override bool IsClanChatWindowOpenInSteam( ulong steamIDClanChat )
        {
            CheckIfUsable();
            bool result = NativeEntrypoints.SteamAPI_ISteamFriends_IsClanChatWindowOpenInSteam(m_pSteamFriends,steamIDClanChat);
            return result;
        }
        internal override bool OpenClanChatWindowInSteam( ulong steamIDClanChat )
        {
            CheckIfUsable();
            bool result = NativeEntrypoints.SteamAPI_ISteamFriends_OpenClanChatWindowInSteam(m_pSteamFriends,steamIDClanChat);
            return result;
        }
        internal override bool CloseClanChatWindowInSteam( ulong steamIDClanChat )
        {
            CheckIfUsable();
            bool result = NativeEntrypoints.SteamAPI_ISteamFriends_CloseClanChatWindowInSteam(m_pSteamFriends,steamIDClanChat);
            return result;
        }
        internal override bool SetListenForFriendsMessages( bool bInterceptEnabled )
        {
            CheckIfUsable();
            bool result = NativeEntrypoints.SteamAPI_ISteamFriends_SetListenForFriendsMessages(m_pSteamFriends,bInterceptEnabled);
            return result;
        }
        internal override bool ReplyToFriendMessage( ulong steamIDFriend, string pchMsgToSend )
        {
            CheckIfUsable();
            bool result = NativeEntrypoints.SteamAPI_ISteamFriends_ReplyToFriendMessage(m_pSteamFriends,steamIDFriend,pchMsgToSend);
            return result;
        }
        internal override int GetFriendMessage( ulong steamIDFriend, int iMessageID, IntPtr pvData, int cubData, ref uint peChatEntryType )
        {
            CheckIfUsable();
            peChatEntryType = 0;
            int result = NativeEntrypoints.SteamAPI_ISteamFriends_GetFriendMessage(m_pSteamFriends,steamIDFriend,iMessageID,pvData,cubData,ref peChatEntryType);
            return result;
        }
        internal override ulong GetFollowerCount( ulong steamID )
        {
            CheckIfUsable();
            ulong result = NativeEntrypoints.SteamAPI_ISteamFriends_GetFollowerCount(m_pSteamFriends,steamID);
            return result;
        }
        internal override ulong IsFollowing( ulong steamID )
        {
            CheckIfUsable();
            ulong result = NativeEntrypoints.SteamAPI_ISteamFriends_IsFollowing(m_pSteamFriends,steamID);
            return result;
        }
        internal override ulong EnumerateFollowingList( uint unStartIndex )
        {
            CheckIfUsable();
            ulong result = NativeEntrypoints.SteamAPI_ISteamFriends_EnumerateFollowingList(m_pSteamFriends,unStartIndex);
            return result;
        }
    }


    internal class CSteamUtils : ISteamUtils
    {
        public CSteamUtils( IntPtr SteamUtils )
        {
            m_pSteamUtils = SteamUtils;
        }
        IntPtr m_pSteamUtils;

        internal override IntPtr GetIntPtr() { return m_pSteamUtils; }

        private void CheckIfUsable()
        {
            if ( m_pSteamUtils == IntPtr.Zero )
            {
                throw new Exception( "Steam Pointer not configured" );
            }
        }
        internal override uint GetSecondsSinceAppActive()
        {
            CheckIfUsable();
            uint result = NativeEntrypoints.SteamAPI_ISteamUtils_GetSecondsSinceAppActive(m_pSteamUtils);
            return result;
        }
        internal override uint GetSecondsSinceComputerActive()
        {
            CheckIfUsable();
            uint result = NativeEntrypoints.SteamAPI_ISteamUtils_GetSecondsSinceComputerActive(m_pSteamUtils);
            return result;
        }
        internal override int GetConnectedUniverse()
        {
            CheckIfUsable();
            int result = NativeEntrypoints.SteamAPI_ISteamUtils_GetConnectedUniverse(m_pSteamUtils);
            return result;
        }
        internal override uint GetServerRealTime()
        {
            CheckIfUsable();
            uint result = NativeEntrypoints.SteamAPI_ISteamUtils_GetServerRealTime(m_pSteamUtils);
            return result;
        }
        internal override string GetIPCountry()
        {
            CheckIfUsable();
            IntPtr result = NativeEntrypoints.SteamAPI_ISteamUtils_GetIPCountry(m_pSteamUtils);
            return Marshal.PtrToStringAnsi( result );
        }
        internal override bool GetImageSize( int iImage, ref uint pnWidth, ref uint pnHeight )
        {
            CheckIfUsable();
            pnWidth = 0;
            pnHeight = 0;
            bool result = NativeEntrypoints.SteamAPI_ISteamUtils_GetImageSize(m_pSteamUtils,iImage,ref pnWidth,ref pnHeight);
            return result;
        }
        internal override bool GetImageRGBA( int iImage, IntPtr pubDest, int nDestBufferSize )
        {
            CheckIfUsable();
            bool result = NativeEntrypoints.SteamAPI_ISteamUtils_GetImageRGBA(m_pSteamUtils,iImage,pubDest,nDestBufferSize);
            return result;
        }
        internal override bool GetCSERIPPort( ref uint unIP, ref char usPort )
        {
            CheckIfUsable();
            unIP = 0;
            usPort = (char)0;
            bool result = NativeEntrypoints.SteamAPI_ISteamUtils_GetCSERIPPort(m_pSteamUtils,ref unIP,ref usPort);
            return result;
        }
        internal override byte GetCurrentBatteryPower()
        {
            CheckIfUsable();
            byte result = NativeEntrypoints.SteamAPI_ISteamUtils_GetCurrentBatteryPower(m_pSteamUtils);
            return result;
        }
        internal override uint GetAppID()
        {
            CheckIfUsable();
            uint result = NativeEntrypoints.SteamAPI_ISteamUtils_GetAppID(m_pSteamUtils);
            return result;
        }
        internal override void SetOverlayNotificationPosition( uint eNotificationPosition )
        {
            CheckIfUsable();
            NativeEntrypoints.SteamAPI_ISteamUtils_SetOverlayNotificationPosition( m_pSteamUtils, eNotificationPosition );
        }
        internal override bool IsAPICallCompleted( ulong hSteamAPICall, ref bool pbFailed )
        {
            CheckIfUsable();
            pbFailed = false;
            bool result = NativeEntrypoints.SteamAPI_ISteamUtils_IsAPICallCompleted(m_pSteamUtils,hSteamAPICall,ref pbFailed);
            return result;
        }
        internal override int GetAPICallFailureReason( ulong hSteamAPICall )
        {
            CheckIfUsable();
            int result = NativeEntrypoints.SteamAPI_ISteamUtils_GetAPICallFailureReason(m_pSteamUtils,hSteamAPICall);
            return result;
        }
        internal override bool GetAPICallResult( ulong hSteamAPICall, IntPtr pCallback, int cubCallback, int iCallbackExpected, ref bool pbFailed )
        {
            CheckIfUsable();
            pbFailed = false;
            bool result = NativeEntrypoints.SteamAPI_ISteamUtils_GetAPICallResult(m_pSteamUtils,hSteamAPICall,pCallback,cubCallback,iCallbackExpected,ref pbFailed);
            return result;
        }
        internal override uint GetIPCCallCount()
        {
            CheckIfUsable();
            uint result = NativeEntrypoints.SteamAPI_ISteamUtils_GetIPCCallCount(m_pSteamUtils);
            return result;
        }
        internal override void SetWarningMessageHook( IntPtr pFunction )
        {
            CheckIfUsable();
            NativeEntrypoints.SteamAPI_ISteamUtils_SetWarningMessageHook( m_pSteamUtils, pFunction );
        }
        internal override bool IsOverlayEnabled()
        {
            CheckIfUsable();
            bool result = NativeEntrypoints.SteamAPI_ISteamUtils_IsOverlayEnabled(m_pSteamUtils);
            return result;
        }
        internal override bool BOverlayNeedsPresent()
        {
            CheckIfUsable();
            bool result = NativeEntrypoints.SteamAPI_ISteamUtils_BOverlayNeedsPresent(m_pSteamUtils);
            return result;
        }
        internal override ulong CheckFileSignature( string szFileName )
        {
            CheckIfUsable();
            ulong result = NativeEntrypoints.SteamAPI_ISteamUtils_CheckFileSignature(m_pSteamUtils,szFileName);
            return result;
        }
        internal override bool ShowGamepadTextInput( int eInputMode, int eLineInputMode, string pchDescription, uint unCharMax, string pchExistingText )
        {
            CheckIfUsable();
            bool result = NativeEntrypoints.SteamAPI_ISteamUtils_ShowGamepadTextInput(m_pSteamUtils,eInputMode,eLineInputMode,pchDescription,unCharMax,pchExistingText);
            return result;
        }
        internal override uint GetEnteredGamepadTextLength()
        {
            CheckIfUsable();
            uint result = NativeEntrypoints.SteamAPI_ISteamUtils_GetEnteredGamepadTextLength(m_pSteamUtils);
            return result;
        }
        internal override bool GetEnteredGamepadTextInput( string pchText, uint cchText )
        {
            CheckIfUsable();
            bool result = NativeEntrypoints.SteamAPI_ISteamUtils_GetEnteredGamepadTextInput(m_pSteamUtils,pchText,cchText);
            return result;
        }
        internal override string GetSteamUILanguage()
        {
            CheckIfUsable();
            IntPtr result = NativeEntrypoints.SteamAPI_ISteamUtils_GetSteamUILanguage(m_pSteamUtils);
            return Marshal.PtrToStringAnsi( result );
        }
        internal override bool IsSteamRunningInVR()
        {
            CheckIfUsable();
            bool result = NativeEntrypoints.SteamAPI_ISteamUtils_IsSteamRunningInVR(m_pSteamUtils);
            return result;
        }
        internal override void SetOverlayNotificationInset( int nHorizontalInset, int nVerticalInset )
        {
            CheckIfUsable();
            NativeEntrypoints.SteamAPI_ISteamUtils_SetOverlayNotificationInset( m_pSteamUtils, nHorizontalInset, nVerticalInset );
        }
        internal override bool IsSteamInBigPictureMode()
        {
            CheckIfUsable();
            bool result = NativeEntrypoints.SteamAPI_ISteamUtils_IsSteamInBigPictureMode(m_pSteamUtils);
            return result;
        }
        internal override void StartVRDashboard()
        {
            CheckIfUsable();
            NativeEntrypoints.SteamAPI_ISteamUtils_StartVRDashboard( m_pSteamUtils );
        }
    }


    internal class CSteamMatchmaking : ISteamMatchmaking
    {
        public CSteamMatchmaking( IntPtr SteamMatchmaking )
        {
            m_pSteamMatchmaking = SteamMatchmaking;
        }
        IntPtr m_pSteamMatchmaking;

        internal override IntPtr GetIntPtr() { return m_pSteamMatchmaking; }

        private void CheckIfUsable()
        {
            if ( m_pSteamMatchmaking == IntPtr.Zero )
            {
                throw new Exception( "Steam Pointer not configured" );
            }
        }
        internal override int GetFavoriteGameCount()
        {
            CheckIfUsable();
            int result = NativeEntrypoints.SteamAPI_ISteamMatchmaking_GetFavoriteGameCount(m_pSteamMatchmaking);
            return result;
        }
        internal override bool GetFavoriteGame( int iGame, ref uint pnAppID, ref uint pnIP, ref char pnConnPort, ref char pnQueryPort, ref uint punFlags, ref uint pRTime32LastPlayedOnServer )
        {
            CheckIfUsable();
            pnAppID = 0;
            pnIP = 0;
            pnConnPort = (char)0;
            pnQueryPort = (char)0;
            punFlags = 0;
            pRTime32LastPlayedOnServer = 0;
            bool result = NativeEntrypoints.SteamAPI_ISteamMatchmaking_GetFavoriteGame(m_pSteamMatchmaking,iGame,ref pnAppID,ref pnIP,ref pnConnPort,ref pnQueryPort,ref punFlags,ref pRTime32LastPlayedOnServer);
            return result;
        }
        internal override int AddFavoriteGame( uint nAppID, uint nIP, char nConnPort, char nQueryPort, uint unFlags, uint rTime32LastPlayedOnServer )
        {
            CheckIfUsable();
            int result = NativeEntrypoints.SteamAPI_ISteamMatchmaking_AddFavoriteGame(m_pSteamMatchmaking,nAppID,nIP,nConnPort,nQueryPort,unFlags,rTime32LastPlayedOnServer);
            return result;
        }
        internal override bool RemoveFavoriteGame( uint nAppID, uint nIP, char nConnPort, char nQueryPort, uint unFlags )
        {
            CheckIfUsable();
            bool result = NativeEntrypoints.SteamAPI_ISteamMatchmaking_RemoveFavoriteGame(m_pSteamMatchmaking,nAppID,nIP,nConnPort,nQueryPort,unFlags);
            return result;
        }
        internal override ulong RequestLobbyList()
        {
            CheckIfUsable();
            ulong result = NativeEntrypoints.SteamAPI_ISteamMatchmaking_RequestLobbyList(m_pSteamMatchmaking);
            return result;
        }
        internal override void AddRequestLobbyListStringFilter( string pchKeyToMatch, string pchValueToMatch, uint eComparisonType )
        {
            CheckIfUsable();
            NativeEntrypoints.SteamAPI_ISteamMatchmaking_AddRequestLobbyListStringFilter( m_pSteamMatchmaking, pchKeyToMatch, pchValueToMatch, eComparisonType );
        }
        internal override void AddRequestLobbyListNumericalFilter( string pchKeyToMatch, int nValueToMatch, uint eComparisonType )
        {
            CheckIfUsable();
            NativeEntrypoints.SteamAPI_ISteamMatchmaking_AddRequestLobbyListNumericalFilter( m_pSteamMatchmaking, pchKeyToMatch, nValueToMatch, eComparisonType );
        }
        internal override void AddRequestLobbyListNearValueFilter( string pchKeyToMatch, int nValueToBeCloseTo )
        {
            CheckIfUsable();
            NativeEntrypoints.SteamAPI_ISteamMatchmaking_AddRequestLobbyListNearValueFilter( m_pSteamMatchmaking, pchKeyToMatch, nValueToBeCloseTo );
        }
        internal override void AddRequestLobbyListFilterSlotsAvailable( int nSlotsAvailable )
        {
            CheckIfUsable();
            NativeEntrypoints.SteamAPI_ISteamMatchmaking_AddRequestLobbyListFilterSlotsAvailable( m_pSteamMatchmaking, nSlotsAvailable );
        }
        internal override void AddRequestLobbyListDistanceFilter( uint eLobbyDistanceFilter )
        {
            CheckIfUsable();
            NativeEntrypoints.SteamAPI_ISteamMatchmaking_AddRequestLobbyListDistanceFilter( m_pSteamMatchmaking, eLobbyDistanceFilter );
        }
        internal override void AddRequestLobbyListResultCountFilter( int cMaxResults )
        {
            CheckIfUsable();
            NativeEntrypoints.SteamAPI_ISteamMatchmaking_AddRequestLobbyListResultCountFilter( m_pSteamMatchmaking, cMaxResults );
        }
        internal override void AddRequestLobbyListCompatibleMembersFilter( ulong steamIDLobby )
        {
            CheckIfUsable();
            NativeEntrypoints.SteamAPI_ISteamMatchmaking_AddRequestLobbyListCompatibleMembersFilter( m_pSteamMatchmaking, steamIDLobby );
        }
        internal override ulong GetLobbyByIndex( int iLobby )
        {
            CheckIfUsable();
            ulong result = NativeEntrypoints.SteamAPI_ISteamMatchmaking_GetLobbyByIndex(m_pSteamMatchmaking,iLobby);
            return result;
        }
        internal override ulong CreateLobby( uint eLobbyType, int cMaxMembers )
        {
            CheckIfUsable();
            ulong result = NativeEntrypoints.SteamAPI_ISteamMatchmaking_CreateLobby(m_pSteamMatchmaking,eLobbyType,cMaxMembers);
            return result;
        }
        internal override ulong JoinLobby( ulong steamIDLobby )
        {
            CheckIfUsable();
            ulong result = NativeEntrypoints.SteamAPI_ISteamMatchmaking_JoinLobby(m_pSteamMatchmaking,steamIDLobby);
            return result;
        }
        internal override void LeaveLobby( ulong steamIDLobby )
        {
            CheckIfUsable();
            NativeEntrypoints.SteamAPI_ISteamMatchmaking_LeaveLobby( m_pSteamMatchmaking, steamIDLobby );
        }
        internal override bool InviteUserToLobby( ulong steamIDLobby, ulong steamIDInvitee )
        {
            CheckIfUsable();
            bool result = NativeEntrypoints.SteamAPI_ISteamMatchmaking_InviteUserToLobby(m_pSteamMatchmaking,steamIDLobby,steamIDInvitee);
            return result;
        }
        internal override int GetNumLobbyMembers( ulong steamIDLobby )
        {
            CheckIfUsable();
            int result = NativeEntrypoints.SteamAPI_ISteamMatchmaking_GetNumLobbyMembers(m_pSteamMatchmaking,steamIDLobby);
            return result;
        }
        internal override ulong GetLobbyMemberByIndex( ulong steamIDLobby, int iMember )
        {
            CheckIfUsable();
            ulong result = NativeEntrypoints.SteamAPI_ISteamMatchmaking_GetLobbyMemberByIndex(m_pSteamMatchmaking,steamIDLobby,iMember);
            return result;
        }
        internal override string GetLobbyData( ulong steamIDLobby, string pchKey )
        {
            CheckIfUsable();
            IntPtr result = NativeEntrypoints.SteamAPI_ISteamMatchmaking_GetLobbyData(m_pSteamMatchmaking,steamIDLobby,pchKey);
            return Marshal.PtrToStringAnsi( result );
        }
        internal override bool SetLobbyData( ulong steamIDLobby, string pchKey, string pchValue )
        {
            CheckIfUsable();
            bool result = NativeEntrypoints.SteamAPI_ISteamMatchmaking_SetLobbyData(m_pSteamMatchmaking,steamIDLobby,pchKey,pchValue);
            return result;
        }
        internal override int GetLobbyDataCount( ulong steamIDLobby )
        {
            CheckIfUsable();
            int result = NativeEntrypoints.SteamAPI_ISteamMatchmaking_GetLobbyDataCount(m_pSteamMatchmaking,steamIDLobby);
            return result;
        }
        internal override bool GetLobbyDataByIndex( ulong steamIDLobby, int iLobbyData, string pchKey, int cchKeyBufferSize, string pchValue, int cchValueBufferSize )
        {
            CheckIfUsable();
            bool result = NativeEntrypoints.SteamAPI_ISteamMatchmaking_GetLobbyDataByIndex(m_pSteamMatchmaking,steamIDLobby,iLobbyData,pchKey,cchKeyBufferSize,pchValue,cchValueBufferSize);
            return result;
        }
        internal override bool DeleteLobbyData( ulong steamIDLobby, string pchKey )
        {
            CheckIfUsable();
            bool result = NativeEntrypoints.SteamAPI_ISteamMatchmaking_DeleteLobbyData(m_pSteamMatchmaking,steamIDLobby,pchKey);
            return result;
        }
        internal override string GetLobbyMemberData( ulong steamIDLobby, ulong steamIDUser, string pchKey )
        {
            CheckIfUsable();
            IntPtr result = NativeEntrypoints.SteamAPI_ISteamMatchmaking_GetLobbyMemberData(m_pSteamMatchmaking,steamIDLobby,steamIDUser,pchKey);
            return Marshal.PtrToStringAnsi( result );
        }
        internal override void SetLobbyMemberData( ulong steamIDLobby, string pchKey, string pchValue )
        {
            CheckIfUsable();
            NativeEntrypoints.SteamAPI_ISteamMatchmaking_SetLobbyMemberData( m_pSteamMatchmaking, steamIDLobby, pchKey, pchValue );
        }
        internal override bool SendLobbyChatMsg( ulong steamIDLobby, IntPtr pvMsgBody, int cubMsgBody )
        {
            CheckIfUsable();
            bool result = NativeEntrypoints.SteamAPI_ISteamMatchmaking_SendLobbyChatMsg(m_pSteamMatchmaking,steamIDLobby,pvMsgBody,cubMsgBody);
            return result;
        }
        internal override int GetLobbyChatEntry( ulong steamIDLobby, int iChatID, out CSteamID pSteamIDUser, IntPtr pvData, int cubData, ref uint peChatEntryType )
        {
            CheckIfUsable();
            pSteamIDUser = new CSteamID();
            peChatEntryType = 0;
            int result = NativeEntrypoints.SteamAPI_ISteamMatchmaking_GetLobbyChatEntry(m_pSteamMatchmaking,steamIDLobby,iChatID,ref pSteamIDUser,pvData,cubData,ref peChatEntryType);
            return result;
        }
        internal override bool RequestLobbyData( ulong steamIDLobby )
        {
            CheckIfUsable();
            bool result = NativeEntrypoints.SteamAPI_ISteamMatchmaking_RequestLobbyData(m_pSteamMatchmaking,steamIDLobby);
            return result;
        }
        internal override void SetLobbyGameServer( ulong steamIDLobby, uint unGameServerIP, char unGameServerPort, ulong steamIDGameServer )
        {
            CheckIfUsable();
            NativeEntrypoints.SteamAPI_ISteamMatchmaking_SetLobbyGameServer( m_pSteamMatchmaking, steamIDLobby, unGameServerIP, unGameServerPort, steamIDGameServer );
        }
        internal override bool GetLobbyGameServer( ulong steamIDLobby, ref uint punGameServerIP, ref char punGameServerPort, out CSteamID psteamIDGameServer )
        {
            CheckIfUsable();
            punGameServerIP = 0;
            punGameServerPort = (char)0;
            psteamIDGameServer = new CSteamID();
            bool result = NativeEntrypoints.SteamAPI_ISteamMatchmaking_GetLobbyGameServer(m_pSteamMatchmaking,steamIDLobby,ref punGameServerIP,ref punGameServerPort,ref psteamIDGameServer);
            return result;
        }
        internal override bool SetLobbyMemberLimit( ulong steamIDLobby, int cMaxMembers )
        {
            CheckIfUsable();
            bool result = NativeEntrypoints.SteamAPI_ISteamMatchmaking_SetLobbyMemberLimit(m_pSteamMatchmaking,steamIDLobby,cMaxMembers);
            return result;
        }
        internal override int GetLobbyMemberLimit( ulong steamIDLobby )
        {
            CheckIfUsable();
            int result = NativeEntrypoints.SteamAPI_ISteamMatchmaking_GetLobbyMemberLimit(m_pSteamMatchmaking,steamIDLobby);
            return result;
        }
        internal override bool SetLobbyType( ulong steamIDLobby, uint eLobbyType )
        {
            CheckIfUsable();
            bool result = NativeEntrypoints.SteamAPI_ISteamMatchmaking_SetLobbyType(m_pSteamMatchmaking,steamIDLobby,eLobbyType);
            return result;
        }
        internal override bool SetLobbyJoinable( ulong steamIDLobby, bool bLobbyJoinable )
        {
            CheckIfUsable();
            bool result = NativeEntrypoints.SteamAPI_ISteamMatchmaking_SetLobbyJoinable(m_pSteamMatchmaking,steamIDLobby,bLobbyJoinable);
            return result;
        }
        internal override ulong GetLobbyOwner( ulong steamIDLobby )
        {
            CheckIfUsable();
            ulong result = NativeEntrypoints.SteamAPI_ISteamMatchmaking_GetLobbyOwner(m_pSteamMatchmaking,steamIDLobby);
            return result;
        }
        internal override bool SetLobbyOwner( ulong steamIDLobby, ulong steamIDNewOwner )
        {
            CheckIfUsable();
            bool result = NativeEntrypoints.SteamAPI_ISteamMatchmaking_SetLobbyOwner(m_pSteamMatchmaking,steamIDLobby,steamIDNewOwner);
            return result;
        }
        internal override bool SetLinkedLobby( ulong steamIDLobby, ulong steamIDLobbyDependent )
        {
            CheckIfUsable();
            bool result = NativeEntrypoints.SteamAPI_ISteamMatchmaking_SetLinkedLobby(m_pSteamMatchmaking,steamIDLobby,steamIDLobbyDependent);
            return result;
        }
    }


    internal class CSteamMatchmakingServerListResponse : ISteamMatchmakingServerListResponse
    {
        public CSteamMatchmakingServerListResponse( IntPtr SteamMatchmakingServerListResponse )
        {
            m_pSteamMatchmakingServerListResponse = SteamMatchmakingServerListResponse;
        }
        IntPtr m_pSteamMatchmakingServerListResponse;

        internal override IntPtr GetIntPtr() { return m_pSteamMatchmakingServerListResponse; }

        private void CheckIfUsable()
        {
            if ( m_pSteamMatchmakingServerListResponse == IntPtr.Zero )
            {
                throw new Exception( "Steam Pointer not configured" );
            }
        }
        internal override void ServerResponded( uint hRequest, int iServer )
        {
            CheckIfUsable();
            NativeEntrypoints.SteamAPI_ISteamMatchmakingServerListResponse_ServerResponded( m_pSteamMatchmakingServerListResponse, hRequest, iServer );
        }
        internal override void ServerFailedToRespond( uint hRequest, int iServer )
        {
            CheckIfUsable();
            NativeEntrypoints.SteamAPI_ISteamMatchmakingServerListResponse_ServerFailedToRespond( m_pSteamMatchmakingServerListResponse, hRequest, iServer );
        }
        internal override void RefreshComplete( uint hRequest, uint response )
        {
            CheckIfUsable();
            NativeEntrypoints.SteamAPI_ISteamMatchmakingServerListResponse_RefreshComplete( m_pSteamMatchmakingServerListResponse, hRequest, response );
        }
    }


    internal class CSteamMatchmakingPingResponse : ISteamMatchmakingPingResponse
    {
        public CSteamMatchmakingPingResponse( IntPtr SteamMatchmakingPingResponse )
        {
            m_pSteamMatchmakingPingResponse = SteamMatchmakingPingResponse;
        }
        IntPtr m_pSteamMatchmakingPingResponse;

        internal override IntPtr GetIntPtr() { return m_pSteamMatchmakingPingResponse; }

        private void CheckIfUsable()
        {
            if ( m_pSteamMatchmakingPingResponse == IntPtr.Zero )
            {
                throw new Exception( "Steam Pointer not configured" );
            }
        }
        internal override void ServerResponded( IntPtr server )
        {
            CheckIfUsable();
            NativeEntrypoints.SteamAPI_ISteamMatchmakingPingResponse_ServerResponded( m_pSteamMatchmakingPingResponse, server );
        }
        internal override void ServerFailedToRespond()
        {
            CheckIfUsable();
            NativeEntrypoints.SteamAPI_ISteamMatchmakingPingResponse_ServerFailedToRespond( m_pSteamMatchmakingPingResponse );
        }
    }


    internal class CSteamMatchmakingPlayersResponse : ISteamMatchmakingPlayersResponse
    {
        public CSteamMatchmakingPlayersResponse( IntPtr SteamMatchmakingPlayersResponse )
        {
            m_pSteamMatchmakingPlayersResponse = SteamMatchmakingPlayersResponse;
        }
        IntPtr m_pSteamMatchmakingPlayersResponse;

        internal override IntPtr GetIntPtr() { return m_pSteamMatchmakingPlayersResponse; }

        private void CheckIfUsable()
        {
            if ( m_pSteamMatchmakingPlayersResponse == IntPtr.Zero )
            {
                throw new Exception( "Steam Pointer not configured" );
            }
        }
        internal override void AddPlayerToList( string pchName, int nScore, float flTimePlayed )
        {
            CheckIfUsable();
            NativeEntrypoints.SteamAPI_ISteamMatchmakingPlayersResponse_AddPlayerToList( m_pSteamMatchmakingPlayersResponse, pchName, nScore, flTimePlayed );
        }
        internal override void PlayersFailedToRespond()
        {
            CheckIfUsable();
            NativeEntrypoints.SteamAPI_ISteamMatchmakingPlayersResponse_PlayersFailedToRespond( m_pSteamMatchmakingPlayersResponse );
        }
        internal override void PlayersRefreshComplete()
        {
            CheckIfUsable();
            NativeEntrypoints.SteamAPI_ISteamMatchmakingPlayersResponse_PlayersRefreshComplete( m_pSteamMatchmakingPlayersResponse );
        }
    }


    internal class CSteamMatchmakingRulesResponse : ISteamMatchmakingRulesResponse
    {
        public CSteamMatchmakingRulesResponse( IntPtr SteamMatchmakingRulesResponse )
        {
            m_pSteamMatchmakingRulesResponse = SteamMatchmakingRulesResponse;
        }
        IntPtr m_pSteamMatchmakingRulesResponse;

        internal override IntPtr GetIntPtr() { return m_pSteamMatchmakingRulesResponse; }

        private void CheckIfUsable()
        {
            if ( m_pSteamMatchmakingRulesResponse == IntPtr.Zero )
            {
                throw new Exception( "Steam Pointer not configured" );
            }
        }
        internal override void RulesResponded( string pchRule, string pchValue )
        {
            CheckIfUsable();
            NativeEntrypoints.SteamAPI_ISteamMatchmakingRulesResponse_RulesResponded( m_pSteamMatchmakingRulesResponse, pchRule, pchValue );
        }
        internal override void RulesFailedToRespond()
        {
            CheckIfUsable();
            NativeEntrypoints.SteamAPI_ISteamMatchmakingRulesResponse_RulesFailedToRespond( m_pSteamMatchmakingRulesResponse );
        }
        internal override void RulesRefreshComplete()
        {
            CheckIfUsable();
            NativeEntrypoints.SteamAPI_ISteamMatchmakingRulesResponse_RulesRefreshComplete( m_pSteamMatchmakingRulesResponse );
        }
    }


    internal class CSteamMatchmakingServers : ISteamMatchmakingServers
    {
        public CSteamMatchmakingServers( IntPtr SteamMatchmakingServers )
        {
            m_pSteamMatchmakingServers = SteamMatchmakingServers;
        }
        IntPtr m_pSteamMatchmakingServers;

        internal override IntPtr GetIntPtr() { return m_pSteamMatchmakingServers; }

        private void CheckIfUsable()
        {
            if ( m_pSteamMatchmakingServers == IntPtr.Zero )
            {
                throw new Exception( "Steam Pointer not configured" );
            }
        }
        internal override IntPtr RequestInternetServerList( uint iApp, IntPtr ppchFilters, int length, IntPtr pRequestServersResponse )
        {
            CheckIfUsable();
            IntPtr result = NativeEntrypoints.SteamAPI_ISteamMatchmakingServers_RequestInternetServerList(m_pSteamMatchmakingServers,iApp,ppchFilters,(uint) length,pRequestServersResponse);
            return result;
        }
        internal override IntPtr RequestLANServerList( uint iApp, IntPtr pRequestServersResponse )
        {
            CheckIfUsable();
            IntPtr result = NativeEntrypoints.SteamAPI_ISteamMatchmakingServers_RequestLANServerList(m_pSteamMatchmakingServers,iApp,pRequestServersResponse);
            return result;
        }
        internal override IntPtr RequestFriendsServerList( uint iApp, IntPtr[] ppchFilters, IntPtr pRequestServersResponse )
        {
            CheckIfUsable();
            IntPtr result = NativeEntrypoints.SteamAPI_ISteamMatchmakingServers_RequestFriendsServerList(m_pSteamMatchmakingServers,iApp,ppchFilters,(uint) ppchFilters.Length,pRequestServersResponse);
            return result;
        }
        internal override IntPtr RequestFavoritesServerList( uint iApp, IntPtr[] ppchFilters, IntPtr pRequestServersResponse )
        {
            CheckIfUsable();
            IntPtr result = NativeEntrypoints.SteamAPI_ISteamMatchmakingServers_RequestFavoritesServerList(m_pSteamMatchmakingServers,iApp,ppchFilters,(uint) ppchFilters.Length,pRequestServersResponse);
            return result;
        }
        internal override IntPtr RequestHistoryServerList( uint iApp, IntPtr[] ppchFilters, IntPtr pRequestServersResponse )
        {
            CheckIfUsable();
            IntPtr result = NativeEntrypoints.SteamAPI_ISteamMatchmakingServers_RequestHistoryServerList(m_pSteamMatchmakingServers,iApp,ppchFilters,(uint) ppchFilters.Length,pRequestServersResponse);
            return result;
        }
        internal override IntPtr RequestSpectatorServerList( uint iApp, IntPtr[] ppchFilters, IntPtr pRequestServersResponse )
        {
            CheckIfUsable();
            IntPtr result = NativeEntrypoints.SteamAPI_ISteamMatchmakingServers_RequestSpectatorServerList(m_pSteamMatchmakingServers,iApp,ppchFilters,(uint) ppchFilters.Length,pRequestServersResponse);
            return result;
        }
        internal override void ReleaseRequest( IntPtr hServerListRequest )
        {
            CheckIfUsable();
            NativeEntrypoints.SteamAPI_ISteamMatchmakingServers_ReleaseRequest( m_pSteamMatchmakingServers, hServerListRequest );
        }
        internal override gameserveritem_t GetServerDetails( IntPtr hRequest, int iServer )
        {
            CheckIfUsable();
            IntPtr result = NativeEntrypoints.SteamAPI_ISteamMatchmakingServers_GetServerDetails(m_pSteamMatchmakingServers,hRequest,iServer);
            return (gameserveritem_t)Marshal.PtrToStructure( result, typeof( gameserveritem_t ) );
        }
        internal override void CancelQuery( IntPtr hRequest )
        {
            CheckIfUsable();
            NativeEntrypoints.SteamAPI_ISteamMatchmakingServers_CancelQuery( m_pSteamMatchmakingServers, hRequest );
        }
        internal override void RefreshQuery( IntPtr hRequest )
        {
            CheckIfUsable();
            NativeEntrypoints.SteamAPI_ISteamMatchmakingServers_RefreshQuery( m_pSteamMatchmakingServers, hRequest );
        }
        internal override bool IsRefreshing( IntPtr hRequest )
        {
            CheckIfUsable();
            bool result = NativeEntrypoints.SteamAPI_ISteamMatchmakingServers_IsRefreshing(m_pSteamMatchmakingServers,hRequest);
            return result;
        }
        internal override int GetServerCount( IntPtr hRequest )
        {
            CheckIfUsable();
            int result = NativeEntrypoints.SteamAPI_ISteamMatchmakingServers_GetServerCount(m_pSteamMatchmakingServers,hRequest);
            return result;
        }
        internal override void RefreshServer( IntPtr hRequest, int iServer )
        {
            CheckIfUsable();
            NativeEntrypoints.SteamAPI_ISteamMatchmakingServers_RefreshServer( m_pSteamMatchmakingServers, hRequest, iServer );
        }
        internal override uint PingServer( uint unIP, char usPort, ISteamMatchmakingPingResponse pRequestServersResponse )
        {
            CheckIfUsable();
            uint result = NativeEntrypoints.SteamAPI_ISteamMatchmakingServers_PingServer(m_pSteamMatchmakingServers,unIP,usPort,pRequestServersResponse.GetIntPtr());
            return result;
        }
        internal override uint PlayerDetails( uint unIP, char usPort, ISteamMatchmakingPlayersResponse pRequestServersResponse )
        {
            CheckIfUsable();
            uint result = NativeEntrypoints.SteamAPI_ISteamMatchmakingServers_PlayerDetails(m_pSteamMatchmakingServers,unIP,usPort,pRequestServersResponse.GetIntPtr());
            return result;
        }
        internal override uint ServerRules( uint unIP, char usPort, ISteamMatchmakingRulesResponse pRequestServersResponse )
        {
            CheckIfUsable();
            uint result = NativeEntrypoints.SteamAPI_ISteamMatchmakingServers_ServerRules(m_pSteamMatchmakingServers,unIP, (short)usPort,pRequestServersResponse.GetIntPtr());
            return result;
        }
        internal override void CancelServerQuery( uint hServerQuery )
        {
            CheckIfUsable();
            NativeEntrypoints.SteamAPI_ISteamMatchmakingServers_CancelServerQuery( m_pSteamMatchmakingServers, hServerQuery );
        }
    }


    internal class CSteamRemoteStorage : ISteamRemoteStorage
    {
        public CSteamRemoteStorage( IntPtr SteamRemoteStorage )
        {
            m_pSteamRemoteStorage = SteamRemoteStorage;
        }
        IntPtr m_pSteamRemoteStorage;

        internal override IntPtr GetIntPtr() { return m_pSteamRemoteStorage; }

        private void CheckIfUsable()
        {
            if ( m_pSteamRemoteStorage == IntPtr.Zero )
            {
                throw new Exception( "Steam Pointer not configured" );
            }
        }
        internal override bool FileWrite( string pchFile, IntPtr pvData, int cubData )
        {
            CheckIfUsable();
            bool result = NativeEntrypoints.SteamAPI_ISteamRemoteStorage_FileWrite(m_pSteamRemoteStorage,pchFile,pvData,cubData);
            return result;
        }
        internal override int FileRead( string pchFile, IntPtr pvData, int cubDataToRead )
        {
            CheckIfUsable();
            int result = NativeEntrypoints.SteamAPI_ISteamRemoteStorage_FileRead(m_pSteamRemoteStorage,pchFile,pvData,cubDataToRead);
            return result;
        }
        internal override ulong FileWriteAsync( string pchFile, IntPtr pvData, uint cubData )
        {
            CheckIfUsable();
            ulong result = NativeEntrypoints.SteamAPI_ISteamRemoteStorage_FileWriteAsync(m_pSteamRemoteStorage,pchFile,pvData,cubData);
            return result;
        }
        internal override ulong FileReadAsync( string pchFile, uint nOffset, uint cubToRead )
        {
            CheckIfUsable();
            ulong result = NativeEntrypoints.SteamAPI_ISteamRemoteStorage_FileReadAsync(m_pSteamRemoteStorage,pchFile,nOffset,cubToRead);
            return result;
        }
        internal override bool FileReadAsyncComplete( ulong hReadCall, IntPtr pvBuffer, uint cubToRead )
        {
            CheckIfUsable();
            bool result = NativeEntrypoints.SteamAPI_ISteamRemoteStorage_FileReadAsyncComplete(m_pSteamRemoteStorage,hReadCall,pvBuffer,cubToRead);
            return result;
        }
        internal override bool FileForget( string pchFile )
        {
            CheckIfUsable();
            bool result = NativeEntrypoints.SteamAPI_ISteamRemoteStorage_FileForget(m_pSteamRemoteStorage,pchFile);
            return result;
        }
        internal override bool FileDelete( string pchFile )
        {
            CheckIfUsable();
            bool result = NativeEntrypoints.SteamAPI_ISteamRemoteStorage_FileDelete(m_pSteamRemoteStorage,pchFile);
            return result;
        }
        internal override ulong FileShare( string pchFile )
        {
            CheckIfUsable();
            ulong result = NativeEntrypoints.SteamAPI_ISteamRemoteStorage_FileShare(m_pSteamRemoteStorage,pchFile);
            return result;
        }
        internal override bool SetSyncPlatforms( string pchFile, uint eRemoteStoragePlatform )
        {
            CheckIfUsable();
            bool result = NativeEntrypoints.SteamAPI_ISteamRemoteStorage_SetSyncPlatforms(m_pSteamRemoteStorage,pchFile,eRemoteStoragePlatform);
            return result;
        }
        internal override ulong FileWriteStreamOpen( string pchFile )
        {
            CheckIfUsable();
            ulong result = NativeEntrypoints.SteamAPI_ISteamRemoteStorage_FileWriteStreamOpen(m_pSteamRemoteStorage,pchFile);
            return result;
        }
        internal override bool FileWriteStreamWriteChunk( ulong writeHandle, IntPtr pvData, int cubData )
        {
            CheckIfUsable();
            bool result = NativeEntrypoints.SteamAPI_ISteamRemoteStorage_FileWriteStreamWriteChunk(m_pSteamRemoteStorage,writeHandle,pvData,cubData);
            return result;
        }
        internal override bool FileWriteStreamClose( ulong writeHandle )
        {
            CheckIfUsable();
            bool result = NativeEntrypoints.SteamAPI_ISteamRemoteStorage_FileWriteStreamClose(m_pSteamRemoteStorage,writeHandle);
            return result;
        }
        internal override bool FileWriteStreamCancel( ulong writeHandle )
        {
            CheckIfUsable();
            bool result = NativeEntrypoints.SteamAPI_ISteamRemoteStorage_FileWriteStreamCancel(m_pSteamRemoteStorage,writeHandle);
            return result;
        }
        internal override bool FileExists( string pchFile )
        {
            CheckIfUsable();
            bool result = NativeEntrypoints.SteamAPI_ISteamRemoteStorage_FileExists(m_pSteamRemoteStorage,pchFile);
            return result;
        }
        internal override bool FilePersisted( string pchFile )
        {
            CheckIfUsable();
            bool result = NativeEntrypoints.SteamAPI_ISteamRemoteStorage_FilePersisted(m_pSteamRemoteStorage,pchFile);
            return result;
        }
        internal override int GetFileSize( string pchFile )
        {
            CheckIfUsable();
            int result = NativeEntrypoints.SteamAPI_ISteamRemoteStorage_GetFileSize(m_pSteamRemoteStorage,pchFile);
            return result;
        }
        internal override long GetFileTimestamp( string pchFile )
        {
            CheckIfUsable();
            long result = NativeEntrypoints.SteamAPI_ISteamRemoteStorage_GetFileTimestamp(m_pSteamRemoteStorage,pchFile);
            return result;
        }
        internal override uint GetSyncPlatforms( string pchFile )
        {
            CheckIfUsable();
            uint result = NativeEntrypoints.SteamAPI_ISteamRemoteStorage_GetSyncPlatforms(m_pSteamRemoteStorage,pchFile);
            return result;
        }
        internal override int GetFileCount()
        {
            CheckIfUsable();
            int result = NativeEntrypoints.SteamAPI_ISteamRemoteStorage_GetFileCount(m_pSteamRemoteStorage);
            return result;
        }
        internal override string GetFileNameAndSize( int iFile, ref int pnFileSizeInBytes )
        {
            CheckIfUsable();
            pnFileSizeInBytes = 0;
            IntPtr result = NativeEntrypoints.SteamAPI_ISteamRemoteStorage_GetFileNameAndSize(m_pSteamRemoteStorage,iFile,ref pnFileSizeInBytes);
            return Marshal.PtrToStringAnsi( result );
        }
        internal override bool GetQuota( ref int pnTotalBytes, ref int puAvailableBytes )
        {
            CheckIfUsable();
            pnTotalBytes = 0;
            puAvailableBytes = 0;
            bool result = NativeEntrypoints.SteamAPI_ISteamRemoteStorage_GetQuota(m_pSteamRemoteStorage,ref pnTotalBytes,ref puAvailableBytes);
            return result;
        }
        internal override bool IsCloudEnabledForAccount()
        {
            CheckIfUsable();
            bool result = NativeEntrypoints.SteamAPI_ISteamRemoteStorage_IsCloudEnabledForAccount(m_pSteamRemoteStorage);
            return result;
        }
        internal override bool IsCloudEnabledForApp()
        {
            CheckIfUsable();
            bool result = NativeEntrypoints.SteamAPI_ISteamRemoteStorage_IsCloudEnabledForApp(m_pSteamRemoteStorage);
            return result;
        }
        internal override void SetCloudEnabledForApp( bool bEnabled )
        {
            CheckIfUsable();
            NativeEntrypoints.SteamAPI_ISteamRemoteStorage_SetCloudEnabledForApp( m_pSteamRemoteStorage, bEnabled );
        }
        internal override ulong UGCDownload( ulong hContent, uint unPriority )
        {
            CheckIfUsable();
            ulong result = NativeEntrypoints.SteamAPI_ISteamRemoteStorage_UGCDownload(m_pSteamRemoteStorage,hContent,unPriority);
            return result;
        }
        internal override bool GetUGCDownloadProgress( ulong hContent, ref int pnBytesDownloaded, ref int pnBytesExpected )
        {
            CheckIfUsable();
            pnBytesDownloaded = 0;
            pnBytesExpected = 0;
            bool result = NativeEntrypoints.SteamAPI_ISteamRemoteStorage_GetUGCDownloadProgress(m_pSteamRemoteStorage,hContent,ref pnBytesDownloaded,ref pnBytesExpected);
            return result;
        }
        internal override bool GetUGCDetails( ulong hContent, ref uint pnAppID, System.Text.StringBuilder ppchName, ref int pnFileSizeInBytes, out CSteamID pSteamIDOwner )
        {
            CheckIfUsable();
            pnAppID = 0;
            pnFileSizeInBytes = 0;
            pSteamIDOwner = new CSteamID();
            bool result = NativeEntrypoints.SteamAPI_ISteamRemoteStorage_GetUGCDetails(m_pSteamRemoteStorage,hContent,ref pnAppID,ppchName,ref pnFileSizeInBytes,ref pSteamIDOwner);
            return result;
        }
        internal override int UGCRead( ulong hContent, IntPtr pvData, int cubDataToRead, uint cOffset, uint eAction )
        {
            CheckIfUsable();
            int result = NativeEntrypoints.SteamAPI_ISteamRemoteStorage_UGCRead(m_pSteamRemoteStorage,hContent,pvData,cubDataToRead,cOffset,eAction);
            return result;
        }
        internal override int GetCachedUGCCount()
        {
            CheckIfUsable();
            int result = NativeEntrypoints.SteamAPI_ISteamRemoteStorage_GetCachedUGCCount(m_pSteamRemoteStorage);
            return result;
        }
        internal override ulong GetCachedUGCHandle( int iCachedContent )
        {
            CheckIfUsable();
            ulong result = NativeEntrypoints.SteamAPI_ISteamRemoteStorage_GetCachedUGCHandle(m_pSteamRemoteStorage,iCachedContent);
            return result;
        }
        internal override ulong PublishWorkshopFile( string pchFile, string pchPreviewFile, uint nConsumerAppId, string pchTitle, string pchDescription, uint eVisibility, ref SteamParamStringArray_t pTags, uint eWorkshopFileType )
        {
            CheckIfUsable();
            ulong result = NativeEntrypoints.SteamAPI_ISteamRemoteStorage_PublishWorkshopFile(m_pSteamRemoteStorage,pchFile,pchPreviewFile,nConsumerAppId,pchTitle,pchDescription,eVisibility,ref pTags,eWorkshopFileType);
            return result;
        }
        internal override ulong CreatePublishedFileUpdateRequest( ulong unPublishedFileId )
        {
            CheckIfUsable();
            ulong result = NativeEntrypoints.SteamAPI_ISteamRemoteStorage_CreatePublishedFileUpdateRequest(m_pSteamRemoteStorage,unPublishedFileId);
            return result;
        }
        internal override bool UpdatePublishedFileFile( ulong updateHandle, string pchFile )
        {
            CheckIfUsable();
            bool result = NativeEntrypoints.SteamAPI_ISteamRemoteStorage_UpdatePublishedFileFile(m_pSteamRemoteStorage,updateHandle,pchFile);
            return result;
        }
        internal override bool UpdatePublishedFilePreviewFile( ulong updateHandle, string pchPreviewFile )
        {
            CheckIfUsable();
            bool result = NativeEntrypoints.SteamAPI_ISteamRemoteStorage_UpdatePublishedFilePreviewFile(m_pSteamRemoteStorage,updateHandle,pchPreviewFile);
            return result;
        }
        internal override bool UpdatePublishedFileTitle( ulong updateHandle, string pchTitle )
        {
            CheckIfUsable();
            bool result = NativeEntrypoints.SteamAPI_ISteamRemoteStorage_UpdatePublishedFileTitle(m_pSteamRemoteStorage,updateHandle,pchTitle);
            return result;
        }
        internal override bool UpdatePublishedFileDescription( ulong updateHandle, string pchDescription )
        {
            CheckIfUsable();
            bool result = NativeEntrypoints.SteamAPI_ISteamRemoteStorage_UpdatePublishedFileDescription(m_pSteamRemoteStorage,updateHandle,pchDescription);
            return result;
        }
        internal override bool UpdatePublishedFileVisibility( ulong updateHandle, uint eVisibility )
        {
            CheckIfUsable();
            bool result = NativeEntrypoints.SteamAPI_ISteamRemoteStorage_UpdatePublishedFileVisibility(m_pSteamRemoteStorage,updateHandle,eVisibility);
            return result;
        }
        internal override bool UpdatePublishedFileTags( ulong updateHandle, ref SteamParamStringArray_t pTags )
        {
            CheckIfUsable();
            bool result = NativeEntrypoints.SteamAPI_ISteamRemoteStorage_UpdatePublishedFileTags(m_pSteamRemoteStorage,updateHandle,ref pTags);
            return result;
        }
        internal override ulong CommitPublishedFileUpdate( ulong updateHandle )
        {
            CheckIfUsable();
            ulong result = NativeEntrypoints.SteamAPI_ISteamRemoteStorage_CommitPublishedFileUpdate(m_pSteamRemoteStorage,updateHandle);
            return result;
        }
        internal override ulong GetPublishedFileDetails( ulong unPublishedFileId, uint unMaxSecondsOld )
        {
            CheckIfUsable();
            ulong result = NativeEntrypoints.SteamAPI_ISteamRemoteStorage_GetPublishedFileDetails(m_pSteamRemoteStorage,unPublishedFileId,unMaxSecondsOld);
            return result;
        }
        internal override ulong DeletePublishedFile( ulong unPublishedFileId )
        {
            CheckIfUsable();
            ulong result = NativeEntrypoints.SteamAPI_ISteamRemoteStorage_DeletePublishedFile(m_pSteamRemoteStorage,unPublishedFileId);
            return result;
        }
        internal override ulong EnumerateUserPublishedFiles( uint unStartIndex )
        {
            CheckIfUsable();
            ulong result = NativeEntrypoints.SteamAPI_ISteamRemoteStorage_EnumerateUserPublishedFiles(m_pSteamRemoteStorage,unStartIndex);
            return result;
        }
        internal override ulong SubscribePublishedFile( ulong unPublishedFileId )
        {
            CheckIfUsable();
            ulong result = NativeEntrypoints.SteamAPI_ISteamRemoteStorage_SubscribePublishedFile(m_pSteamRemoteStorage,unPublishedFileId);
            return result;
        }
        internal override ulong EnumerateUserSubscribedFiles( uint unStartIndex )
        {
            CheckIfUsable();
            ulong result = NativeEntrypoints.SteamAPI_ISteamRemoteStorage_EnumerateUserSubscribedFiles(m_pSteamRemoteStorage,unStartIndex);
            return result;
        }
        internal override ulong UnsubscribePublishedFile( ulong unPublishedFileId )
        {
            CheckIfUsable();
            ulong result = NativeEntrypoints.SteamAPI_ISteamRemoteStorage_UnsubscribePublishedFile(m_pSteamRemoteStorage,unPublishedFileId);
            return result;
        }
        internal override bool UpdatePublishedFileSetChangeDescription( ulong updateHandle, string pchChangeDescription )
        {
            CheckIfUsable();
            bool result = NativeEntrypoints.SteamAPI_ISteamRemoteStorage_UpdatePublishedFileSetChangeDescription(m_pSteamRemoteStorage,updateHandle,pchChangeDescription);
            return result;
        }
        internal override ulong GetPublishedItemVoteDetails( ulong unPublishedFileId )
        {
            CheckIfUsable();
            ulong result = NativeEntrypoints.SteamAPI_ISteamRemoteStorage_GetPublishedItemVoteDetails(m_pSteamRemoteStorage,unPublishedFileId);
            return result;
        }
        internal override ulong UpdateUserPublishedItemVote( ulong unPublishedFileId, bool bVoteUp )
        {
            CheckIfUsable();
            ulong result = NativeEntrypoints.SteamAPI_ISteamRemoteStorage_UpdateUserPublishedItemVote(m_pSteamRemoteStorage,unPublishedFileId,bVoteUp);
            return result;
        }
        internal override ulong GetUserPublishedItemVoteDetails( ulong unPublishedFileId )
        {
            CheckIfUsable();
            ulong result = NativeEntrypoints.SteamAPI_ISteamRemoteStorage_GetUserPublishedItemVoteDetails(m_pSteamRemoteStorage,unPublishedFileId);
            return result;
        }
        internal override ulong EnumerateUserSharedWorkshopFiles( ulong steamId, uint unStartIndex, ref SteamParamStringArray_t pRequiredTags, ref SteamParamStringArray_t pExcludedTags )
        {
            CheckIfUsable();
            ulong result = NativeEntrypoints.SteamAPI_ISteamRemoteStorage_EnumerateUserSharedWorkshopFiles(m_pSteamRemoteStorage,steamId,unStartIndex,ref pRequiredTags,ref pExcludedTags);
            return result;
        }
        internal override ulong PublishVideo( uint eVideoProvider, string pchVideoAccount, string pchVideoIdentifier, string pchPreviewFile, uint nConsumerAppId, string pchTitle, string pchDescription, uint eVisibility, ref SteamParamStringArray_t pTags )
        {
            CheckIfUsable();
            ulong result = NativeEntrypoints.SteamAPI_ISteamRemoteStorage_PublishVideo(m_pSteamRemoteStorage,eVideoProvider,pchVideoAccount,pchVideoIdentifier,pchPreviewFile,nConsumerAppId,pchTitle,pchDescription,eVisibility,ref pTags);
            return result;
        }
        internal override ulong SetUserPublishedFileAction( ulong unPublishedFileId, uint eAction )
        {
            CheckIfUsable();
            ulong result = NativeEntrypoints.SteamAPI_ISteamRemoteStorage_SetUserPublishedFileAction(m_pSteamRemoteStorage,unPublishedFileId,eAction);
            return result;
        }
        internal override ulong EnumeratePublishedFilesByUserAction( uint eAction, uint unStartIndex )
        {
            CheckIfUsable();
            ulong result = NativeEntrypoints.SteamAPI_ISteamRemoteStorage_EnumeratePublishedFilesByUserAction(m_pSteamRemoteStorage,eAction,unStartIndex);
            return result;
        }
        internal override ulong EnumeratePublishedWorkshopFiles( uint eEnumerationType, uint unStartIndex, uint unCount, uint unDays, ref SteamParamStringArray_t pTags, ref SteamParamStringArray_t pUserTags )
        {
            CheckIfUsable();
            ulong result = NativeEntrypoints.SteamAPI_ISteamRemoteStorage_EnumeratePublishedWorkshopFiles(m_pSteamRemoteStorage,eEnumerationType,unStartIndex,unCount,unDays,ref pTags,ref pUserTags);
            return result;
        }
        internal override ulong UGCDownloadToLocation( ulong hContent, string pchLocation, uint unPriority )
        {
            CheckIfUsable();
            ulong result = NativeEntrypoints.SteamAPI_ISteamRemoteStorage_UGCDownloadToLocation(m_pSteamRemoteStorage,hContent,pchLocation,unPriority);
            return result;
        }
    }


    internal class CSteamUserStats : ISteamUserStats
    {
        public CSteamUserStats( IntPtr SteamUserStats )
        {
            m_pSteamUserStats = SteamUserStats;
        }
        IntPtr m_pSteamUserStats;

        internal override IntPtr GetIntPtr() { return m_pSteamUserStats; }

        private void CheckIfUsable()
        {
            if ( m_pSteamUserStats == IntPtr.Zero )
            {
                throw new Exception( "Steam Pointer not configured" );
            }
        }
        internal override bool RequestCurrentStats()
        {
            CheckIfUsable();
            bool result = NativeEntrypoints.SteamAPI_ISteamUserStats_RequestCurrentStats(m_pSteamUserStats);
            return result;
        }
        internal override bool GetStat( string pchName, ref int pData )
        {
            CheckIfUsable();
            pData = 0;
            bool result = NativeEntrypoints.SteamAPI_ISteamUserStats_GetStat(m_pSteamUserStats,pchName,ref pData);
            return result;
        }
        internal override bool GetStat0( string pchName, ref float pData )
        {
            CheckIfUsable();
            pData = 0;
            bool result = NativeEntrypoints.SteamAPI_ISteamUserStats_GetStat0(m_pSteamUserStats,pchName,ref pData);
            return result;
        }
        internal override bool SetStat( string pchName, int nData )
        {
            CheckIfUsable();
            bool result = NativeEntrypoints.SteamAPI_ISteamUserStats_SetStat(m_pSteamUserStats,pchName,nData);
            return result;
        }
        internal override bool SetStat0( string pchName, float fData )
        {
            CheckIfUsable();
            bool result = NativeEntrypoints.SteamAPI_ISteamUserStats_SetStat0(m_pSteamUserStats,pchName,fData);
            return result;
        }
        internal override bool UpdateAvgRateStat( string pchName, float flCountThisSession, double dSessionLength )
        {
            CheckIfUsable();
            bool result = NativeEntrypoints.SteamAPI_ISteamUserStats_UpdateAvgRateStat(m_pSteamUserStats,pchName,flCountThisSession,dSessionLength);
            return result;
        }
        internal override bool GetAchievement( string pchName, ref bool pbAchieved )
        {
            CheckIfUsable();
            pbAchieved = false;
            bool result = NativeEntrypoints.SteamAPI_ISteamUserStats_GetAchievement(m_pSteamUserStats,pchName,ref pbAchieved);
            return result;
        }
        internal override bool SetAchievement( string pchName )
        {
            CheckIfUsable();
            bool result = NativeEntrypoints.SteamAPI_ISteamUserStats_SetAchievement(m_pSteamUserStats,pchName);
            return result;
        }
        internal override bool ClearAchievement( string pchName )
        {
            CheckIfUsable();
            bool result = NativeEntrypoints.SteamAPI_ISteamUserStats_ClearAchievement(m_pSteamUserStats,pchName);
            return result;
        }
        internal override bool GetAchievementAndUnlockTime( string pchName, ref bool pbAchieved, ref uint punUnlockTime )
        {
            CheckIfUsable();
            pbAchieved = false;
            punUnlockTime = 0;
            bool result = NativeEntrypoints.SteamAPI_ISteamUserStats_GetAchievementAndUnlockTime(m_pSteamUserStats,pchName,ref pbAchieved,ref punUnlockTime);
            return result;
        }
        internal override bool StoreStats()
        {
            CheckIfUsable();
            bool result = NativeEntrypoints.SteamAPI_ISteamUserStats_StoreStats(m_pSteamUserStats);
            return result;
        }
        internal override int GetAchievementIcon( string pchName )
        {
            CheckIfUsable();
            int result = NativeEntrypoints.SteamAPI_ISteamUserStats_GetAchievementIcon(m_pSteamUserStats,pchName);
            return result;
        }
        internal override string GetAchievementDisplayAttribute( string pchName, string pchKey )
        {
            CheckIfUsable();
            IntPtr result = NativeEntrypoints.SteamAPI_ISteamUserStats_GetAchievementDisplayAttribute(m_pSteamUserStats,pchName,pchKey);
            return Marshal.PtrToStringAnsi( result );
        }
        internal override bool IndicateAchievementProgress( string pchName, uint nCurProgress, uint nMaxProgress )
        {
            CheckIfUsable();
            bool result = NativeEntrypoints.SteamAPI_ISteamUserStats_IndicateAchievementProgress(m_pSteamUserStats,pchName,nCurProgress,nMaxProgress);
            return result;
        }
        internal override uint GetNumAchievements()
        {
            CheckIfUsable();
            uint result = NativeEntrypoints.SteamAPI_ISteamUserStats_GetNumAchievements(m_pSteamUserStats);
            return result;
        }
        internal override string GetAchievementName( uint iAchievement )
        {
            CheckIfUsable();
            IntPtr result = NativeEntrypoints.SteamAPI_ISteamUserStats_GetAchievementName(m_pSteamUserStats,iAchievement);
            return Marshal.PtrToStringAnsi( result );
        }
        internal override ulong RequestUserStats( ulong steamIDUser )
        {
            CheckIfUsable();
            ulong result = NativeEntrypoints.SteamAPI_ISteamUserStats_RequestUserStats(m_pSteamUserStats,steamIDUser);
            return result;
        }
        internal override bool GetUserStat( ulong steamIDUser, string pchName, ref int pData )
        {
            CheckIfUsable();
            pData = 0;
            bool result = NativeEntrypoints.SteamAPI_ISteamUserStats_GetUserStat(m_pSteamUserStats,steamIDUser,pchName,ref pData);
            return result;
        }
        internal override bool GetUserStat0( ulong steamIDUser, string pchName, ref float pData )
        {
            CheckIfUsable();
            pData = 0;
            bool result = NativeEntrypoints.SteamAPI_ISteamUserStats_GetUserStat0(m_pSteamUserStats,steamIDUser,pchName,ref pData);
            return result;
        }
        internal override bool GetUserAchievement( ulong steamIDUser, string pchName, ref bool pbAchieved )
        {
            CheckIfUsable();
            pbAchieved = false;
            bool result = NativeEntrypoints.SteamAPI_ISteamUserStats_GetUserAchievement(m_pSteamUserStats,steamIDUser,pchName,ref pbAchieved);
            return result;
        }
        internal override bool GetUserAchievementAndUnlockTime( ulong steamIDUser, string pchName, ref bool pbAchieved, ref uint punUnlockTime )
        {
            CheckIfUsable();
            pbAchieved = false;
            punUnlockTime = 0;
            bool result = NativeEntrypoints.SteamAPI_ISteamUserStats_GetUserAchievementAndUnlockTime(m_pSteamUserStats,steamIDUser,pchName,ref pbAchieved,ref punUnlockTime);
            return result;
        }
        internal override bool ResetAllStats( bool bAchievementsToo )
        {
            CheckIfUsable();
            bool result = NativeEntrypoints.SteamAPI_ISteamUserStats_ResetAllStats(m_pSteamUserStats,bAchievementsToo);
            return result;
        }
        internal override ulong FindOrCreateLeaderboard( string pchLeaderboardName, uint eLeaderboardSortMethod, uint eLeaderboardDisplayType )
        {
            CheckIfUsable();
            ulong result = NativeEntrypoints.SteamAPI_ISteamUserStats_FindOrCreateLeaderboard(m_pSteamUserStats,pchLeaderboardName,eLeaderboardSortMethod,eLeaderboardDisplayType);
            return result;
        }
        internal override ulong FindLeaderboard( string pchLeaderboardName )
        {
            CheckIfUsable();
            ulong result = NativeEntrypoints.SteamAPI_ISteamUserStats_FindLeaderboard(m_pSteamUserStats,pchLeaderboardName);
            return result;
        }
        internal override string GetLeaderboardName( ulong hSteamLeaderboard )
        {
            CheckIfUsable();
            IntPtr result = NativeEntrypoints.SteamAPI_ISteamUserStats_GetLeaderboardName(m_pSteamUserStats,hSteamLeaderboard);
            return Marshal.PtrToStringAnsi( result );
        }
        internal override int GetLeaderboardEntryCount( ulong hSteamLeaderboard )
        {
            CheckIfUsable();
            int result = NativeEntrypoints.SteamAPI_ISteamUserStats_GetLeaderboardEntryCount(m_pSteamUserStats,hSteamLeaderboard);
            return result;
        }
        internal override uint GetLeaderboardSortMethod( ulong hSteamLeaderboard )
        {
            CheckIfUsable();
            uint result = NativeEntrypoints.SteamAPI_ISteamUserStats_GetLeaderboardSortMethod(m_pSteamUserStats,hSteamLeaderboard);
            return result;
        }
        internal override uint GetLeaderboardDisplayType( ulong hSteamLeaderboard )
        {
            CheckIfUsable();
            uint result = NativeEntrypoints.SteamAPI_ISteamUserStats_GetLeaderboardDisplayType(m_pSteamUserStats,hSteamLeaderboard);
            return result;
        }
        internal override ulong DownloadLeaderboardEntries( ulong hSteamLeaderboard, uint eLeaderboardDataRequest, int nRangeStart, int nRangeEnd )
        {
            CheckIfUsable();
            ulong result = NativeEntrypoints.SteamAPI_ISteamUserStats_DownloadLeaderboardEntries(m_pSteamUserStats,hSteamLeaderboard,eLeaderboardDataRequest,nRangeStart,nRangeEnd);
            return result;
        }
        internal override ulong DownloadLeaderboardEntriesForUsers( ulong hSteamLeaderboard, CSteamID[] prgUsers )
        {
            CheckIfUsable();
            ulong result = NativeEntrypoints.SteamAPI_ISteamUserStats_DownloadLeaderboardEntriesForUsers(m_pSteamUserStats,hSteamLeaderboard,prgUsers,(int) prgUsers.Length);
            return result;
        }
        internal override bool GetDownloadedLeaderboardEntry( ulong hSteamLeaderboardEntries, int index, ref LeaderboardEntry_t pLeaderboardEntry, ref int pDetails, int cDetailsMax )
        {
            CheckIfUsable();
            pDetails = 0;
            bool result = NativeEntrypoints.SteamAPI_ISteamUserStats_GetDownloadedLeaderboardEntry(m_pSteamUserStats,hSteamLeaderboardEntries,index,ref pLeaderboardEntry,ref pDetails,cDetailsMax);
            return result;
        }
        internal override ulong UploadLeaderboardScore( ulong hSteamLeaderboard, uint eLeaderboardUploadScoreMethod, int nScore, ref int pScoreDetails, int cScoreDetailsCount )
        {
            CheckIfUsable();
            pScoreDetails = 0;
            ulong result = NativeEntrypoints.SteamAPI_ISteamUserStats_UploadLeaderboardScore(m_pSteamUserStats,hSteamLeaderboard,eLeaderboardUploadScoreMethod,nScore,ref pScoreDetails,cScoreDetailsCount);
            return result;
        }
        internal override ulong AttachLeaderboardUGC( ulong hSteamLeaderboard, ulong hUGC )
        {
            CheckIfUsable();
            ulong result = NativeEntrypoints.SteamAPI_ISteamUserStats_AttachLeaderboardUGC(m_pSteamUserStats,hSteamLeaderboard,hUGC);
            return result;
        }
        internal override ulong GetNumberOfCurrentPlayers()
        {
            CheckIfUsable();
            ulong result = NativeEntrypoints.SteamAPI_ISteamUserStats_GetNumberOfCurrentPlayers(m_pSteamUserStats);
            return result;
        }
        internal override ulong RequestGlobalAchievementPercentages()
        {
            CheckIfUsable();
            ulong result = NativeEntrypoints.SteamAPI_ISteamUserStats_RequestGlobalAchievementPercentages(m_pSteamUserStats);
            return result;
        }
        internal override int GetMostAchievedAchievementInfo( string pchName, uint unNameBufLen, ref float pflPercent, ref bool pbAchieved )
        {
            CheckIfUsable();
            pflPercent = 0;
            pbAchieved = false;
            int result = NativeEntrypoints.SteamAPI_ISteamUserStats_GetMostAchievedAchievementInfo(m_pSteamUserStats,pchName,unNameBufLen,ref pflPercent,ref pbAchieved);
            return result;
        }
        internal override int GetNextMostAchievedAchievementInfo( int iIteratorPrevious, string pchName, uint unNameBufLen, ref float pflPercent, ref bool pbAchieved )
        {
            CheckIfUsable();
            pflPercent = 0;
            pbAchieved = false;
            int result = NativeEntrypoints.SteamAPI_ISteamUserStats_GetNextMostAchievedAchievementInfo(m_pSteamUserStats,iIteratorPrevious,pchName,unNameBufLen,ref pflPercent,ref pbAchieved);
            return result;
        }
        internal override bool GetAchievementAchievedPercent( string pchName, ref float pflPercent )
        {
            CheckIfUsable();
            pflPercent = 0;
            bool result = NativeEntrypoints.SteamAPI_ISteamUserStats_GetAchievementAchievedPercent(m_pSteamUserStats,pchName,ref pflPercent);
            return result;
        }
        internal override ulong RequestGlobalStats( int nHistoryDays )
        {
            CheckIfUsable();
            ulong result = NativeEntrypoints.SteamAPI_ISteamUserStats_RequestGlobalStats(m_pSteamUserStats,nHistoryDays);
            return result;
        }
        internal override bool GetGlobalStat( string pchStatName, ref long pData )
        {
            CheckIfUsable();
            pData = 0;
            bool result = NativeEntrypoints.SteamAPI_ISteamUserStats_GetGlobalStat(m_pSteamUserStats,pchStatName,ref pData);
            return result;
        }
        internal override bool GetGlobalStat0( string pchStatName, ref double pData )
        {
            CheckIfUsable();
            pData = 0;
            bool result = NativeEntrypoints.SteamAPI_ISteamUserStats_GetGlobalStat0(m_pSteamUserStats,pchStatName,ref pData);
            return result;
        }
        internal override int GetGlobalStatHistory( string pchStatName, long[] pData )
        {
            CheckIfUsable();
            int result = NativeEntrypoints.SteamAPI_ISteamUserStats_GetGlobalStatHistory(m_pSteamUserStats,pchStatName,pData,(uint) pData.Length);
            return result;
        }
        internal override int GetGlobalStatHistory0( string pchStatName, double[] pData )
        {
            CheckIfUsable();
            int result = NativeEntrypoints.SteamAPI_ISteamUserStats_GetGlobalStatHistory0(m_pSteamUserStats,pchStatName,pData,(uint) pData.Length);
            return result;
        }
    }


    internal class CSteamApps : ISteamApps
    {
        public CSteamApps( IntPtr SteamApps )
        {
            m_pSteamApps = SteamApps;
        }
        IntPtr m_pSteamApps;

        internal override IntPtr GetIntPtr() { return m_pSteamApps; }

        private void CheckIfUsable()
        {
            if ( m_pSteamApps == IntPtr.Zero )
            {
                throw new Exception( "Steam Pointer not configured" );
            }
        }
        internal override bool BIsSubscribed()
        {
            CheckIfUsable();
            bool result = NativeEntrypoints.SteamAPI_ISteamApps_BIsSubscribed(m_pSteamApps);
            return result;
        }
        internal override bool BIsLowViolence()
        {
            CheckIfUsable();
            bool result = NativeEntrypoints.SteamAPI_ISteamApps_BIsLowViolence(m_pSteamApps);
            return result;
        }
        internal override bool BIsCybercafe()
        {
            CheckIfUsable();
            bool result = NativeEntrypoints.SteamAPI_ISteamApps_BIsCybercafe(m_pSteamApps);
            return result;
        }
        internal override bool BIsVACBanned()
        {
            CheckIfUsable();
            bool result = NativeEntrypoints.SteamAPI_ISteamApps_BIsVACBanned(m_pSteamApps);
            return result;
        }
        internal override string GetCurrentGameLanguage()
        {
            CheckIfUsable();
            IntPtr result = NativeEntrypoints.SteamAPI_ISteamApps_GetCurrentGameLanguage(m_pSteamApps);
            return Marshal.PtrToStringAnsi( result );
        }
        internal override string GetAvailableGameLanguages()
        {
            CheckIfUsable();
            IntPtr result = NativeEntrypoints.SteamAPI_ISteamApps_GetAvailableGameLanguages(m_pSteamApps);
            return Marshal.PtrToStringAnsi( result );
        }
        internal override bool BIsSubscribedApp( uint appID )
        {
            CheckIfUsable();
            bool result = NativeEntrypoints.SteamAPI_ISteamApps_BIsSubscribedApp(m_pSteamApps,appID);
            return result;
        }
        internal override bool BIsDlcInstalled( uint appID )
        {
            CheckIfUsable();
            bool result = NativeEntrypoints.SteamAPI_ISteamApps_BIsDlcInstalled(m_pSteamApps,appID);
            return result;
        }
        internal override uint GetEarliestPurchaseUnixTime( uint nAppID )
        {
            CheckIfUsable();
            uint result = NativeEntrypoints.SteamAPI_ISteamApps_GetEarliestPurchaseUnixTime(m_pSteamApps,nAppID);
            return result;
        }
        internal override bool BIsSubscribedFromFreeWeekend()
        {
            CheckIfUsable();
            bool result = NativeEntrypoints.SteamAPI_ISteamApps_BIsSubscribedFromFreeWeekend(m_pSteamApps);
            return result;
        }
        internal override int GetDLCCount()
        {
            CheckIfUsable();
            int result = NativeEntrypoints.SteamAPI_ISteamApps_GetDLCCount(m_pSteamApps);
            return result;
        }
        internal override bool BGetDLCDataByIndex( int iDLC, ref uint pAppID, ref bool pbAvailable, string pchName, int cchNameBufferSize )
        {
            CheckIfUsable();
            pAppID = 0;
            pbAvailable = false;
            bool result = NativeEntrypoints.SteamAPI_ISteamApps_BGetDLCDataByIndex(m_pSteamApps,iDLC,ref pAppID,ref pbAvailable,pchName,cchNameBufferSize);
            return result;
        }
        internal override void InstallDLC( uint nAppID )
        {
            CheckIfUsable();
            NativeEntrypoints.SteamAPI_ISteamApps_InstallDLC( m_pSteamApps, nAppID );
        }
        internal override void UninstallDLC( uint nAppID )
        {
            CheckIfUsable();
            NativeEntrypoints.SteamAPI_ISteamApps_UninstallDLC( m_pSteamApps, nAppID );
        }
        internal override void RequestAppProofOfPurchaseKey( uint nAppID )
        {
            CheckIfUsable();
            NativeEntrypoints.SteamAPI_ISteamApps_RequestAppProofOfPurchaseKey( m_pSteamApps, nAppID );
        }
        internal unsafe override string GetCurrentBetaName()
        {
            CheckIfUsable();

            var buffer = new byte[512];

            fixed ( byte* p = buffer )
            {
                if ( !NativeEntrypoints.SteamAPI_ISteamApps_GetCurrentBetaName( m_pSteamApps, (IntPtr)p, buffer.Length ) )
                    return string.Empty;

                return Marshal.PtrToStringAuto( (IntPtr)p );
            }
        }
        internal override bool MarkContentCorrupt( bool bMissingFilesOnly )
        {
            CheckIfUsable();
            bool result = NativeEntrypoints.SteamAPI_ISteamApps_MarkContentCorrupt(m_pSteamApps,bMissingFilesOnly);
            return result;
        }
        internal override uint GetInstalledDepots( uint appID, ref uint pvecDepots, uint cMaxDepots )
        {
            CheckIfUsable();
            pvecDepots = 0;
            uint result = NativeEntrypoints.SteamAPI_ISteamApps_GetInstalledDepots(m_pSteamApps,appID,ref pvecDepots,cMaxDepots);
            return result;
        }
        internal override unsafe string GetAppInstallDir( uint appID )
        {
            CheckIfUsable();

            var buffer = new byte[512];

            fixed ( byte* p = buffer )
            {
                if ( 0 == NativeEntrypoints.SteamAPI_ISteamApps_GetAppInstallDir( m_pSteamApps, appID,( IntPtr)p, (uint) buffer.Length ) )
                    return string.Empty;

                return Marshal.PtrToStringAuto( (IntPtr)p );
            }
        }
        internal override bool BIsAppInstalled( uint appID )
        {
            CheckIfUsable();
            bool result = NativeEntrypoints.SteamAPI_ISteamApps_BIsAppInstalled(m_pSteamApps,appID);
            return result;
        }
        internal override ulong GetAppOwner()
        {
            CheckIfUsable();
            ulong result = NativeEntrypoints.SteamAPI_ISteamApps_GetAppOwner(m_pSteamApps);
            return result;
        }
        internal override string GetLaunchQueryParam( string pchKey )
        {
            CheckIfUsable();
            IntPtr result = NativeEntrypoints.SteamAPI_ISteamApps_GetLaunchQueryParam(m_pSteamApps,pchKey);
            return Marshal.PtrToStringAnsi( result );
        }
        internal override bool GetDlcDownloadProgress( uint nAppID, ref ulong punBytesDownloaded, ref ulong punBytesTotal )
        {
            CheckIfUsable();
            punBytesDownloaded = 0;
            punBytesTotal = 0;
            bool result = NativeEntrypoints.SteamAPI_ISteamApps_GetDlcDownloadProgress(m_pSteamApps,nAppID,ref punBytesDownloaded,ref punBytesTotal);
            return result;
        }
        internal override int GetAppBuildId()
        {
            CheckIfUsable();
            int result = NativeEntrypoints.SteamAPI_ISteamApps_GetAppBuildId(m_pSteamApps);
            return result;
        }
        internal override void RequestAllProofOfPurchaseKeys()
        {
            CheckIfUsable();
            NativeEntrypoints.SteamAPI_ISteamApps_RequestAllProofOfPurchaseKeys( m_pSteamApps );
        }
    }


    internal class CSteamNetworking : ISteamNetworking
    {
        public CSteamNetworking( IntPtr SteamNetworking )
        {
            m_pSteamNetworking = SteamNetworking;
        }
        IntPtr m_pSteamNetworking;

        internal override IntPtr GetIntPtr() { return m_pSteamNetworking; }

        private void CheckIfUsable()
        {
            if ( m_pSteamNetworking == IntPtr.Zero )
            {
                throw new Exception( "Steam Pointer not configured" );
            }
        }
        internal override bool SendP2PPacket( ulong steamIDRemote, IntPtr pubData, uint cubData, uint eP2PSendType, int nChannel )
        {
            CheckIfUsable();
            bool result = NativeEntrypoints.SteamAPI_ISteamNetworking_SendP2PPacket(m_pSteamNetworking,steamIDRemote,pubData,cubData,eP2PSendType,nChannel);
            return result;
        }
        internal override bool IsP2PPacketAvailable( ref uint pcubMsgSize, int nChannel )
        {
            CheckIfUsable();
            pcubMsgSize = 0;
            bool result = NativeEntrypoints.SteamAPI_ISteamNetworking_IsP2PPacketAvailable(m_pSteamNetworking,ref pcubMsgSize,nChannel);
            return result;
        }
        internal override bool ReadP2PPacket( IntPtr pubDest, uint cubDest, ref uint pcubMsgSize, ref ulong psteamIDRemote, int nChannel )
        {
            CheckIfUsable();
            pcubMsgSize = 0;
            bool result = NativeEntrypoints.SteamAPI_ISteamNetworking_ReadP2PPacket(m_pSteamNetworking,pubDest,cubDest,ref pcubMsgSize,ref psteamIDRemote,nChannel);
            return result;
        }
        internal override bool AcceptP2PSessionWithUser( ulong steamIDRemote )
        {
            CheckIfUsable();
            bool result = NativeEntrypoints.SteamAPI_ISteamNetworking_AcceptP2PSessionWithUser(m_pSteamNetworking,steamIDRemote);
            return result;
        }
        internal override bool CloseP2PSessionWithUser( ulong steamIDRemote )
        {
            CheckIfUsable();
            bool result = NativeEntrypoints.SteamAPI_ISteamNetworking_CloseP2PSessionWithUser(m_pSteamNetworking,steamIDRemote);
            return result;
        }
        internal override bool CloseP2PChannelWithUser( ulong steamIDRemote, int nChannel )
        {
            CheckIfUsable();
            bool result = NativeEntrypoints.SteamAPI_ISteamNetworking_CloseP2PChannelWithUser(m_pSteamNetworking,steamIDRemote,nChannel);
            return result;
        }
        internal override bool GetP2PSessionState( ulong steamIDRemote, ref P2PSessionState_t pConnectionState )
        {
            CheckIfUsable();
            bool result = NativeEntrypoints.SteamAPI_ISteamNetworking_GetP2PSessionState(m_pSteamNetworking,steamIDRemote,ref pConnectionState);
            return result;
        }
        internal override bool AllowP2PPacketRelay( bool bAllow )
        {
            CheckIfUsable();
            bool result = NativeEntrypoints.SteamAPI_ISteamNetworking_AllowP2PPacketRelay(m_pSteamNetworking,bAllow);
            return result;
        }
        internal override uint CreateListenSocket( int nVirtualP2PPort, uint nIP, char nPort, bool bAllowUseOfPacketRelay )
        {
            CheckIfUsable();
            uint result = NativeEntrypoints.SteamAPI_ISteamNetworking_CreateListenSocket(m_pSteamNetworking,nVirtualP2PPort,nIP,nPort,bAllowUseOfPacketRelay);
            return result;
        }
        internal override uint CreateP2PConnectionSocket( ulong steamIDTarget, int nVirtualPort, int nTimeoutSec, bool bAllowUseOfPacketRelay )
        {
            CheckIfUsable();
            uint result = NativeEntrypoints.SteamAPI_ISteamNetworking_CreateP2PConnectionSocket(m_pSteamNetworking,steamIDTarget,nVirtualPort,nTimeoutSec,bAllowUseOfPacketRelay);
            return result;
        }
        internal override uint CreateConnectionSocket( uint nIP, char nPort, int nTimeoutSec )
        {
            CheckIfUsable();
            uint result = NativeEntrypoints.SteamAPI_ISteamNetworking_CreateConnectionSocket(m_pSteamNetworking,nIP,nPort,nTimeoutSec);
            return result;
        }
        internal override bool DestroySocket( uint hSocket, bool bNotifyRemoteEnd )
        {
            CheckIfUsable();
            bool result = NativeEntrypoints.SteamAPI_ISteamNetworking_DestroySocket(m_pSteamNetworking,hSocket,bNotifyRemoteEnd);
            return result;
        }
        internal override bool DestroyListenSocket( uint hSocket, bool bNotifyRemoteEnd )
        {
            CheckIfUsable();
            bool result = NativeEntrypoints.SteamAPI_ISteamNetworking_DestroyListenSocket(m_pSteamNetworking,hSocket,bNotifyRemoteEnd);
            return result;
        }
        internal override bool SendDataOnSocket( uint hSocket, IntPtr pubData, uint cubData, bool bReliable )
        {
            CheckIfUsable();
            bool result = NativeEntrypoints.SteamAPI_ISteamNetworking_SendDataOnSocket(m_pSteamNetworking,hSocket,pubData,cubData,bReliable);
            return result;
        }
        internal override bool IsDataAvailableOnSocket( uint hSocket, ref uint pcubMsgSize )
        {
            CheckIfUsable();
            pcubMsgSize = 0;
            bool result = NativeEntrypoints.SteamAPI_ISteamNetworking_IsDataAvailableOnSocket(m_pSteamNetworking,hSocket,ref pcubMsgSize);
            return result;
        }
        internal override bool RetrieveDataFromSocket( uint hSocket, IntPtr pubDest, uint cubDest, ref uint pcubMsgSize )
        {
            CheckIfUsable();
            pcubMsgSize = 0;
            bool result = NativeEntrypoints.SteamAPI_ISteamNetworking_RetrieveDataFromSocket(m_pSteamNetworking,hSocket,pubDest,cubDest,ref pcubMsgSize);
            return result;
        }
        internal override bool IsDataAvailable( uint hListenSocket, ref uint pcubMsgSize, ref uint phSocket )
        {
            CheckIfUsable();
            pcubMsgSize = 0;
            phSocket = 0;
            bool result = NativeEntrypoints.SteamAPI_ISteamNetworking_IsDataAvailable(m_pSteamNetworking,hListenSocket,ref pcubMsgSize,ref phSocket);
            return result;
        }
        internal override bool RetrieveData( uint hListenSocket, IntPtr pubDest, uint cubDest, ref uint pcubMsgSize, ref uint phSocket )
        {
            CheckIfUsable();
            pcubMsgSize = 0;
            phSocket = 0;
            bool result = NativeEntrypoints.SteamAPI_ISteamNetworking_RetrieveData(m_pSteamNetworking,hListenSocket,pubDest,cubDest,ref pcubMsgSize,ref phSocket);
            return result;
        }
        internal override bool GetSocketInfo( uint hSocket, ref CSteamID pSteamIDRemote, ref int peSocketStatus, ref uint punIPRemote, ref char punPortRemote )
        {
            CheckIfUsable();
            peSocketStatus = 0;
            punIPRemote = 0;
            punPortRemote = (char)0;
            bool result = NativeEntrypoints.SteamAPI_ISteamNetworking_GetSocketInfo(m_pSteamNetworking,hSocket,ref pSteamIDRemote,ref peSocketStatus,ref punIPRemote,ref punPortRemote);
            return result;
        }
        internal override bool GetListenSocketInfo( uint hListenSocket, ref uint pnIP, ref char pnPort )
        {
            CheckIfUsable();
            pnIP = 0;
            pnPort = (char)0;
            bool result = NativeEntrypoints.SteamAPI_ISteamNetworking_GetListenSocketInfo(m_pSteamNetworking,hListenSocket,ref pnIP,ref pnPort);
            return result;
        }
        internal override uint GetSocketConnectionType( uint hSocket )
        {
            CheckIfUsable();
            uint result = NativeEntrypoints.SteamAPI_ISteamNetworking_GetSocketConnectionType(m_pSteamNetworking,hSocket);
            return result;
        }
        internal override int GetMaxPacketSize( uint hSocket )
        {
            CheckIfUsable();
            int result = NativeEntrypoints.SteamAPI_ISteamNetworking_GetMaxPacketSize(m_pSteamNetworking,hSocket);
            return result;
        }
    }


    internal class CSteamScreenshots : ISteamScreenshots
    {
        public CSteamScreenshots( IntPtr SteamScreenshots )
        {
            m_pSteamScreenshots = SteamScreenshots;
        }
        IntPtr m_pSteamScreenshots;

        internal override IntPtr GetIntPtr() { return m_pSteamScreenshots; }

        private void CheckIfUsable()
        {
            if ( m_pSteamScreenshots == IntPtr.Zero )
            {
                throw new Exception( "Steam Pointer not configured" );
            }
        }
        internal override uint WriteScreenshot( IntPtr pubRGB, uint cubRGB, int nWidth, int nHeight )
        {
            CheckIfUsable();
            uint result = NativeEntrypoints.SteamAPI_ISteamScreenshots_WriteScreenshot(m_pSteamScreenshots,pubRGB,cubRGB,nWidth,nHeight);
            return result;
        }
        internal override uint AddScreenshotToLibrary( string pchFilename, string pchThumbnailFilename, int nWidth, int nHeight )
        {
            CheckIfUsable();
            uint result = NativeEntrypoints.SteamAPI_ISteamScreenshots_AddScreenshotToLibrary(m_pSteamScreenshots,pchFilename,pchThumbnailFilename,nWidth,nHeight);
            return result;
        }
        internal override void TriggerScreenshot()
        {
            CheckIfUsable();
            NativeEntrypoints.SteamAPI_ISteamScreenshots_TriggerScreenshot( m_pSteamScreenshots );
        }
        internal override void HookScreenshots( bool bHook )
        {
            CheckIfUsable();
            NativeEntrypoints.SteamAPI_ISteamScreenshots_HookScreenshots( m_pSteamScreenshots, bHook );
        }
        internal override bool SetLocation( uint hScreenshot, string pchLocation )
        {
            CheckIfUsable();
            bool result = NativeEntrypoints.SteamAPI_ISteamScreenshots_SetLocation(m_pSteamScreenshots,hScreenshot,pchLocation);
            return result;
        }
        internal override bool TagUser( uint hScreenshot, ulong steamID )
        {
            CheckIfUsable();
            bool result = NativeEntrypoints.SteamAPI_ISteamScreenshots_TagUser(m_pSteamScreenshots,hScreenshot,steamID);
            return result;
        }
        internal override bool TagPublishedFile( uint hScreenshot, ulong unPublishedFileID )
        {
            CheckIfUsable();
            bool result = NativeEntrypoints.SteamAPI_ISteamScreenshots_TagPublishedFile(m_pSteamScreenshots,hScreenshot,unPublishedFileID);
            return result;
        }
    }


    internal class CSteamMusic : ISteamMusic
    {
        public CSteamMusic( IntPtr SteamMusic )
        {
            m_pSteamMusic = SteamMusic;
        }
        IntPtr m_pSteamMusic;

        internal override IntPtr GetIntPtr() { return m_pSteamMusic; }

        private void CheckIfUsable()
        {
            if ( m_pSteamMusic == IntPtr.Zero )
            {
                throw new Exception( "Steam Pointer not configured" );
            }
        }
        internal override bool BIsEnabled()
        {
            CheckIfUsable();
            bool result = NativeEntrypoints.SteamAPI_ISteamMusic_BIsEnabled(m_pSteamMusic);
            return result;
        }
        internal override bool BIsPlaying()
        {
            CheckIfUsable();
            bool result = NativeEntrypoints.SteamAPI_ISteamMusic_BIsPlaying(m_pSteamMusic);
            return result;
        }
        internal override int GetPlaybackStatus()
        {
            CheckIfUsable();
            int result = NativeEntrypoints.SteamAPI_ISteamMusic_GetPlaybackStatus(m_pSteamMusic);
            return result;
        }
        internal override void Play()
        {
            CheckIfUsable();
            NativeEntrypoints.SteamAPI_ISteamMusic_Play( m_pSteamMusic );
        }
        internal override void Pause()
        {
            CheckIfUsable();
            NativeEntrypoints.SteamAPI_ISteamMusic_Pause( m_pSteamMusic );
        }
        internal override void PlayPrevious()
        {
            CheckIfUsable();
            NativeEntrypoints.SteamAPI_ISteamMusic_PlayPrevious( m_pSteamMusic );
        }
        internal override void PlayNext()
        {
            CheckIfUsable();
            NativeEntrypoints.SteamAPI_ISteamMusic_PlayNext( m_pSteamMusic );
        }
        internal override void SetVolume( float flVolume )
        {
            CheckIfUsable();
            NativeEntrypoints.SteamAPI_ISteamMusic_SetVolume( m_pSteamMusic, flVolume );
        }
        internal override float GetVolume()
        {
            CheckIfUsable();
            float result = NativeEntrypoints.SteamAPI_ISteamMusic_GetVolume(m_pSteamMusic);
            return result;
        }
    }


    internal class CSteamMusicRemote : ISteamMusicRemote
    {
        public CSteamMusicRemote( IntPtr SteamMusicRemote )
        {
            m_pSteamMusicRemote = SteamMusicRemote;
        }
        IntPtr m_pSteamMusicRemote;

        internal override IntPtr GetIntPtr() { return m_pSteamMusicRemote; }

        private void CheckIfUsable()
        {
            if ( m_pSteamMusicRemote == IntPtr.Zero )
            {
                throw new Exception( "Steam Pointer not configured" );
            }
        }
        internal override bool RegisterSteamMusicRemote( string pchName )
        {
            CheckIfUsable();
            bool result = NativeEntrypoints.SteamAPI_ISteamMusicRemote_RegisterSteamMusicRemote(m_pSteamMusicRemote,pchName);
            return result;
        }
        internal override bool DeregisterSteamMusicRemote()
        {
            CheckIfUsable();
            bool result = NativeEntrypoints.SteamAPI_ISteamMusicRemote_DeregisterSteamMusicRemote(m_pSteamMusicRemote);
            return result;
        }
        internal override bool BIsCurrentMusicRemote()
        {
            CheckIfUsable();
            bool result = NativeEntrypoints.SteamAPI_ISteamMusicRemote_BIsCurrentMusicRemote(m_pSteamMusicRemote);
            return result;
        }
        internal override bool BActivationSuccess( bool bValue )
        {
            CheckIfUsable();
            bool result = NativeEntrypoints.SteamAPI_ISteamMusicRemote_BActivationSuccess(m_pSteamMusicRemote,bValue);
            return result;
        }
        internal override bool SetDisplayName( string pchDisplayName )
        {
            CheckIfUsable();
            bool result = NativeEntrypoints.SteamAPI_ISteamMusicRemote_SetDisplayName(m_pSteamMusicRemote,pchDisplayName);
            return result;
        }
        internal override bool SetPNGIcon_64x64( IntPtr pvBuffer, uint cbBufferLength )
        {
            CheckIfUsable();
            bool result = NativeEntrypoints.SteamAPI_ISteamMusicRemote_SetPNGIcon_64x64(m_pSteamMusicRemote,pvBuffer,cbBufferLength);
            return result;
        }
        internal override bool EnablePlayPrevious( bool bValue )
        {
            CheckIfUsable();
            bool result = NativeEntrypoints.SteamAPI_ISteamMusicRemote_EnablePlayPrevious(m_pSteamMusicRemote,bValue);
            return result;
        }
        internal override bool EnablePlayNext( bool bValue )
        {
            CheckIfUsable();
            bool result = NativeEntrypoints.SteamAPI_ISteamMusicRemote_EnablePlayNext(m_pSteamMusicRemote,bValue);
            return result;
        }
        internal override bool EnableShuffled( bool bValue )
        {
            CheckIfUsable();
            bool result = NativeEntrypoints.SteamAPI_ISteamMusicRemote_EnableShuffled(m_pSteamMusicRemote,bValue);
            return result;
        }
        internal override bool EnableLooped( bool bValue )
        {
            CheckIfUsable();
            bool result = NativeEntrypoints.SteamAPI_ISteamMusicRemote_EnableLooped(m_pSteamMusicRemote,bValue);
            return result;
        }
        internal override bool EnableQueue( bool bValue )
        {
            CheckIfUsable();
            bool result = NativeEntrypoints.SteamAPI_ISteamMusicRemote_EnableQueue(m_pSteamMusicRemote,bValue);
            return result;
        }
        internal override bool EnablePlaylists( bool bValue )
        {
            CheckIfUsable();
            bool result = NativeEntrypoints.SteamAPI_ISteamMusicRemote_EnablePlaylists(m_pSteamMusicRemote,bValue);
            return result;
        }
        internal override bool UpdatePlaybackStatus( int nStatus )
        {
            CheckIfUsable();
            bool result = NativeEntrypoints.SteamAPI_ISteamMusicRemote_UpdatePlaybackStatus(m_pSteamMusicRemote,nStatus);
            return result;
        }
        internal override bool UpdateShuffled( bool bValue )
        {
            CheckIfUsable();
            bool result = NativeEntrypoints.SteamAPI_ISteamMusicRemote_UpdateShuffled(m_pSteamMusicRemote,bValue);
            return result;
        }
        internal override bool UpdateLooped( bool bValue )
        {
            CheckIfUsable();
            bool result = NativeEntrypoints.SteamAPI_ISteamMusicRemote_UpdateLooped(m_pSteamMusicRemote,bValue);
            return result;
        }
        internal override bool UpdateVolume( float flValue )
        {
            CheckIfUsable();
            bool result = NativeEntrypoints.SteamAPI_ISteamMusicRemote_UpdateVolume(m_pSteamMusicRemote,flValue);
            return result;
        }
        internal override bool CurrentEntryWillChange()
        {
            CheckIfUsable();
            bool result = NativeEntrypoints.SteamAPI_ISteamMusicRemote_CurrentEntryWillChange(m_pSteamMusicRemote);
            return result;
        }
        internal override bool CurrentEntryIsAvailable( bool bAvailable )
        {
            CheckIfUsable();
            bool result = NativeEntrypoints.SteamAPI_ISteamMusicRemote_CurrentEntryIsAvailable(m_pSteamMusicRemote,bAvailable);
            return result;
        }
        internal override bool UpdateCurrentEntryText( string pchText )
        {
            CheckIfUsable();
            bool result = NativeEntrypoints.SteamAPI_ISteamMusicRemote_UpdateCurrentEntryText(m_pSteamMusicRemote,pchText);
            return result;
        }
        internal override bool UpdateCurrentEntryElapsedSeconds( int nValue )
        {
            CheckIfUsable();
            bool result = NativeEntrypoints.SteamAPI_ISteamMusicRemote_UpdateCurrentEntryElapsedSeconds(m_pSteamMusicRemote,nValue);
            return result;
        }
        internal override bool UpdateCurrentEntryCoverArt( IntPtr pvBuffer, uint cbBufferLength )
        {
            CheckIfUsable();
            bool result = NativeEntrypoints.SteamAPI_ISteamMusicRemote_UpdateCurrentEntryCoverArt(m_pSteamMusicRemote,pvBuffer,cbBufferLength);
            return result;
        }
        internal override bool CurrentEntryDidChange()
        {
            CheckIfUsable();
            bool result = NativeEntrypoints.SteamAPI_ISteamMusicRemote_CurrentEntryDidChange(m_pSteamMusicRemote);
            return result;
        }
        internal override bool QueueWillChange()
        {
            CheckIfUsable();
            bool result = NativeEntrypoints.SteamAPI_ISteamMusicRemote_QueueWillChange(m_pSteamMusicRemote);
            return result;
        }
        internal override bool ResetQueueEntries()
        {
            CheckIfUsable();
            bool result = NativeEntrypoints.SteamAPI_ISteamMusicRemote_ResetQueueEntries(m_pSteamMusicRemote);
            return result;
        }
        internal override bool SetQueueEntry( int nID, int nPosition, string pchEntryText )
        {
            CheckIfUsable();
            bool result = NativeEntrypoints.SteamAPI_ISteamMusicRemote_SetQueueEntry(m_pSteamMusicRemote,nID,nPosition,pchEntryText);
            return result;
        }
        internal override bool SetCurrentQueueEntry( int nID )
        {
            CheckIfUsable();
            bool result = NativeEntrypoints.SteamAPI_ISteamMusicRemote_SetCurrentQueueEntry(m_pSteamMusicRemote,nID);
            return result;
        }
        internal override bool QueueDidChange()
        {
            CheckIfUsable();
            bool result = NativeEntrypoints.SteamAPI_ISteamMusicRemote_QueueDidChange(m_pSteamMusicRemote);
            return result;
        }
        internal override bool PlaylistWillChange()
        {
            CheckIfUsable();
            bool result = NativeEntrypoints.SteamAPI_ISteamMusicRemote_PlaylistWillChange(m_pSteamMusicRemote);
            return result;
        }
        internal override bool ResetPlaylistEntries()
        {
            CheckIfUsable();
            bool result = NativeEntrypoints.SteamAPI_ISteamMusicRemote_ResetPlaylistEntries(m_pSteamMusicRemote);
            return result;
        }
        internal override bool SetPlaylistEntry( int nID, int nPosition, string pchEntryText )
        {
            CheckIfUsable();
            bool result = NativeEntrypoints.SteamAPI_ISteamMusicRemote_SetPlaylistEntry(m_pSteamMusicRemote,nID,nPosition,pchEntryText);
            return result;
        }
        internal override bool SetCurrentPlaylistEntry( int nID )
        {
            CheckIfUsable();
            bool result = NativeEntrypoints.SteamAPI_ISteamMusicRemote_SetCurrentPlaylistEntry(m_pSteamMusicRemote,nID);
            return result;
        }
        internal override bool PlaylistDidChange()
        {
            CheckIfUsable();
            bool result = NativeEntrypoints.SteamAPI_ISteamMusicRemote_PlaylistDidChange(m_pSteamMusicRemote);
            return result;
        }
    }


    internal class CSteamHTTP : ISteamHTTP
    {
        public CSteamHTTP( IntPtr SteamHTTP )
        {
            m_pSteamHTTP = SteamHTTP;
        }
        IntPtr m_pSteamHTTP;

        internal override IntPtr GetIntPtr() { return m_pSteamHTTP; }

        private void CheckIfUsable()
        {
            if ( m_pSteamHTTP == IntPtr.Zero )
            {
                throw new Exception( "Steam Pointer not configured" );
            }
        }
        internal override uint CreateHTTPRequest( uint eHTTPRequestMethod, string pchAbsoluteURL )
        {
            CheckIfUsable();
            uint result = NativeEntrypoints.SteamAPI_ISteamHTTP_CreateHTTPRequest(m_pSteamHTTP,eHTTPRequestMethod,pchAbsoluteURL);
            return result;
        }
        internal override bool SetHTTPRequestContextValue( uint hRequest, ulong ulContextValue )
        {
            CheckIfUsable();
            bool result = NativeEntrypoints.SteamAPI_ISteamHTTP_SetHTTPRequestContextValue(m_pSteamHTTP,hRequest,ulContextValue);
            return result;
        }
        internal override bool SetHTTPRequestNetworkActivityTimeout( uint hRequest, uint unTimeoutSeconds )
        {
            CheckIfUsable();
            bool result = NativeEntrypoints.SteamAPI_ISteamHTTP_SetHTTPRequestNetworkActivityTimeout(m_pSteamHTTP,hRequest,unTimeoutSeconds);
            return result;
        }
        internal override bool SetHTTPRequestHeaderValue( uint hRequest, string pchHeaderName, string pchHeaderValue )
        {
            CheckIfUsable();
            bool result = NativeEntrypoints.SteamAPI_ISteamHTTP_SetHTTPRequestHeaderValue(m_pSteamHTTP,hRequest,pchHeaderName,pchHeaderValue);
            return result;
        }
        internal override bool SetHTTPRequestGetOrPostParameter( uint hRequest, string pchParamName, string pchParamValue )
        {
            CheckIfUsable();
            bool result = NativeEntrypoints.SteamAPI_ISteamHTTP_SetHTTPRequestGetOrPostParameter(m_pSteamHTTP,hRequest,pchParamName,pchParamValue);
            return result;
        }
        internal override bool SendHTTPRequest( uint hRequest, ref ulong pCallHandle )
        {
            CheckIfUsable();
            pCallHandle = 0;
            bool result = NativeEntrypoints.SteamAPI_ISteamHTTP_SendHTTPRequest(m_pSteamHTTP,hRequest,ref pCallHandle);
            return result;
        }
        internal override bool SendHTTPRequestAndStreamResponse( uint hRequest, ref ulong pCallHandle )
        {
            CheckIfUsable();
            pCallHandle = 0;
            bool result = NativeEntrypoints.SteamAPI_ISteamHTTP_SendHTTPRequestAndStreamResponse(m_pSteamHTTP,hRequest,ref pCallHandle);
            return result;
        }
        internal override bool DeferHTTPRequest( uint hRequest )
        {
            CheckIfUsable();
            bool result = NativeEntrypoints.SteamAPI_ISteamHTTP_DeferHTTPRequest(m_pSteamHTTP,hRequest);
            return result;
        }
        internal override bool PrioritizeHTTPRequest( uint hRequest )
        {
            CheckIfUsable();
            bool result = NativeEntrypoints.SteamAPI_ISteamHTTP_PrioritizeHTTPRequest(m_pSteamHTTP,hRequest);
            return result;
        }
        internal override bool GetHTTPResponseHeaderSize( uint hRequest, string pchHeaderName, ref uint unResponseHeaderSize )
        {
            CheckIfUsable();
            unResponseHeaderSize = 0;
            bool result = NativeEntrypoints.SteamAPI_ISteamHTTP_GetHTTPResponseHeaderSize(m_pSteamHTTP,hRequest,pchHeaderName,ref unResponseHeaderSize);
            return result;
        }
        internal override bool GetHTTPResponseHeaderValue( uint hRequest, string pchHeaderName, IntPtr pHeaderValueBuffer, uint unBufferSize )
        {
            CheckIfUsable();
            bool result = NativeEntrypoints.SteamAPI_ISteamHTTP_GetHTTPResponseHeaderValue(m_pSteamHTTP,hRequest,pchHeaderName,pHeaderValueBuffer,unBufferSize);
            return result;
        }
        internal override bool GetHTTPResponseBodySize( uint hRequest, ref uint unBodySize )
        {
            CheckIfUsable();
            unBodySize = 0;
            bool result = NativeEntrypoints.SteamAPI_ISteamHTTP_GetHTTPResponseBodySize(m_pSteamHTTP,hRequest,ref unBodySize);
            return result;
        }
        internal override bool GetHTTPResponseBodyData( uint hRequest, IntPtr pBodyDataBuffer, uint unBufferSize )
        {
            CheckIfUsable();
            bool result = NativeEntrypoints.SteamAPI_ISteamHTTP_GetHTTPResponseBodyData(m_pSteamHTTP,hRequest,pBodyDataBuffer,unBufferSize);
            return result;
        }
        internal override bool GetHTTPStreamingResponseBodyData( uint hRequest, uint cOffset, IntPtr pBodyDataBuffer, uint unBufferSize )
        {
            CheckIfUsable();
            bool result = NativeEntrypoints.SteamAPI_ISteamHTTP_GetHTTPStreamingResponseBodyData(m_pSteamHTTP,hRequest,cOffset,pBodyDataBuffer,unBufferSize);
            return result;
        }
        internal override bool ReleaseHTTPRequest( uint hRequest )
        {
            CheckIfUsable();
            bool result = NativeEntrypoints.SteamAPI_ISteamHTTP_ReleaseHTTPRequest(m_pSteamHTTP,hRequest);
            return result;
        }
        internal override bool GetHTTPDownloadProgressPct( uint hRequest, ref float pflPercentOut )
        {
            CheckIfUsable();
            pflPercentOut = 0;
            bool result = NativeEntrypoints.SteamAPI_ISteamHTTP_GetHTTPDownloadProgressPct(m_pSteamHTTP,hRequest,ref pflPercentOut);
            return result;
        }
        internal override bool SetHTTPRequestRawPostBody( uint hRequest, string pchContentType, IntPtr pubBody, uint unBodyLen )
        {
            CheckIfUsable();
            bool result = NativeEntrypoints.SteamAPI_ISteamHTTP_SetHTTPRequestRawPostBody(m_pSteamHTTP,hRequest,pchContentType,pubBody,unBodyLen);
            return result;
        }
        internal override uint CreateCookieContainer( bool bAllowResponsesToModify )
        {
            CheckIfUsable();
            uint result = NativeEntrypoints.SteamAPI_ISteamHTTP_CreateCookieContainer(m_pSteamHTTP,bAllowResponsesToModify);
            return result;
        }
        internal override bool ReleaseCookieContainer( uint hCookieContainer )
        {
            CheckIfUsable();
            bool result = NativeEntrypoints.SteamAPI_ISteamHTTP_ReleaseCookieContainer(m_pSteamHTTP,hCookieContainer);
            return result;
        }
        internal override bool SetCookie( uint hCookieContainer, string pchHost, string pchUrl, string pchCookie )
        {
            CheckIfUsable();
            bool result = NativeEntrypoints.SteamAPI_ISteamHTTP_SetCookie(m_pSteamHTTP,hCookieContainer,pchHost,pchUrl,pchCookie);
            return result;
        }
        internal override bool SetHTTPRequestCookieContainer( uint hRequest, uint hCookieContainer )
        {
            CheckIfUsable();
            bool result = NativeEntrypoints.SteamAPI_ISteamHTTP_SetHTTPRequestCookieContainer(m_pSteamHTTP,hRequest,hCookieContainer);
            return result;
        }
        internal override bool SetHTTPRequestUserAgentInfo( uint hRequest, string pchUserAgentInfo )
        {
            CheckIfUsable();
            bool result = NativeEntrypoints.SteamAPI_ISteamHTTP_SetHTTPRequestUserAgentInfo(m_pSteamHTTP,hRequest,pchUserAgentInfo);
            return result;
        }
        internal override bool SetHTTPRequestRequiresVerifiedCertificate( uint hRequest, bool bRequireVerifiedCertificate )
        {
            CheckIfUsable();
            bool result = NativeEntrypoints.SteamAPI_ISteamHTTP_SetHTTPRequestRequiresVerifiedCertificate(m_pSteamHTTP,hRequest,bRequireVerifiedCertificate);
            return result;
        }
        internal override bool SetHTTPRequestAbsoluteTimeoutMS( uint hRequest, uint unMilliseconds )
        {
            CheckIfUsable();
            bool result = NativeEntrypoints.SteamAPI_ISteamHTTP_SetHTTPRequestAbsoluteTimeoutMS(m_pSteamHTTP,hRequest,unMilliseconds);
            return result;
        }
        internal override bool GetHTTPRequestWasTimedOut( uint hRequest, ref bool pbWasTimedOut )
        {
            CheckIfUsable();
            pbWasTimedOut = false;
            bool result = NativeEntrypoints.SteamAPI_ISteamHTTP_GetHTTPRequestWasTimedOut(m_pSteamHTTP,hRequest,ref pbWasTimedOut);
            return result;
        }
    }


    internal class CSteamUnifiedMessages : ISteamUnifiedMessages
    {
        public CSteamUnifiedMessages( IntPtr SteamUnifiedMessages )
        {
            m_pSteamUnifiedMessages = SteamUnifiedMessages;
        }
        IntPtr m_pSteamUnifiedMessages;

        internal override IntPtr GetIntPtr() { return m_pSteamUnifiedMessages; }

        private void CheckIfUsable()
        {
            if ( m_pSteamUnifiedMessages == IntPtr.Zero )
            {
                throw new Exception( "Steam Pointer not configured" );
            }
        }
        internal override ulong SendMethod( string pchServiceMethod, IntPtr pRequestBuffer, uint unRequestBufferSize, ulong unContext )
        {
            CheckIfUsable();
            ulong result = NativeEntrypoints.SteamAPI_ISteamUnifiedMessages_SendMethod(m_pSteamUnifiedMessages,pchServiceMethod,pRequestBuffer,unRequestBufferSize,unContext);
            return result;
        }
        internal override bool GetMethodResponseInfo( ulong hHandle, ref uint punResponseSize, ref uint peResult )
        {
            CheckIfUsable();
            punResponseSize = 0;
            peResult = 0;
            bool result = NativeEntrypoints.SteamAPI_ISteamUnifiedMessages_GetMethodResponseInfo(m_pSteamUnifiedMessages,hHandle,ref punResponseSize,ref peResult);
            return result;
        }
        internal override bool GetMethodResponseData( ulong hHandle, IntPtr pResponseBuffer, uint unResponseBufferSize, bool bAutoRelease )
        {
            CheckIfUsable();
            bool result = NativeEntrypoints.SteamAPI_ISteamUnifiedMessages_GetMethodResponseData(m_pSteamUnifiedMessages,hHandle,pResponseBuffer,unResponseBufferSize,bAutoRelease);
            return result;
        }
        internal override bool ReleaseMethod( ulong hHandle )
        {
            CheckIfUsable();
            bool result = NativeEntrypoints.SteamAPI_ISteamUnifiedMessages_ReleaseMethod(m_pSteamUnifiedMessages,hHandle);
            return result;
        }
        internal override bool SendNotification( string pchServiceNotification, IntPtr pNotificationBuffer, uint unNotificationBufferSize )
        {
            CheckIfUsable();
            bool result = NativeEntrypoints.SteamAPI_ISteamUnifiedMessages_SendNotification(m_pSteamUnifiedMessages,pchServiceNotification,pNotificationBuffer,unNotificationBufferSize);
            return result;
        }
    }


    internal class CSteamController : ISteamController
    {
        public CSteamController( IntPtr SteamController )
        {
            m_pSteamController = SteamController;
        }
        IntPtr m_pSteamController;

        internal override IntPtr GetIntPtr() { return m_pSteamController; }

        private void CheckIfUsable()
        {
            if ( m_pSteamController == IntPtr.Zero )
            {
                throw new Exception( "Steam Pointer not configured" );
            }
        }
        internal override bool Init()
        {
            CheckIfUsable();
            bool result = NativeEntrypoints.SteamAPI_ISteamController_Init(m_pSteamController);
            return result;
        }
        internal override bool Shutdown()
        {
            CheckIfUsable();
            bool result = NativeEntrypoints.SteamAPI_ISteamController_Shutdown(m_pSteamController);
            return result;
        }
        internal override void RunFrame()
        {
            CheckIfUsable();
            NativeEntrypoints.SteamAPI_ISteamController_RunFrame( m_pSteamController );
        }
        internal override int GetConnectedControllers( ref ulong handlesOut )
        {
            CheckIfUsable();
            handlesOut = 0;
            int result = NativeEntrypoints.SteamAPI_ISteamController_GetConnectedControllers(m_pSteamController,ref handlesOut);
            return result;
        }
        internal override bool ShowBindingPanel( ulong controllerHandle )
        {
            CheckIfUsable();
            bool result = NativeEntrypoints.SteamAPI_ISteamController_ShowBindingPanel(m_pSteamController,controllerHandle);
            return result;
        }
        internal override ulong GetActionSetHandle( string pszActionSetName )
        {
            CheckIfUsable();
            ulong result = NativeEntrypoints.SteamAPI_ISteamController_GetActionSetHandle(m_pSteamController,pszActionSetName);
            return result;
        }
        internal override void ActivateActionSet( ulong controllerHandle, ulong actionSetHandle )
        {
            CheckIfUsable();
            NativeEntrypoints.SteamAPI_ISteamController_ActivateActionSet( m_pSteamController, controllerHandle, actionSetHandle );
        }
        internal override ulong GetCurrentActionSet( ulong controllerHandle )
        {
            CheckIfUsable();
            ulong result = NativeEntrypoints.SteamAPI_ISteamController_GetCurrentActionSet(m_pSteamController,controllerHandle);
            return result;
        }
        internal override ulong GetDigitalActionHandle( string pszActionName )
        {
            CheckIfUsable();
            ulong result = NativeEntrypoints.SteamAPI_ISteamController_GetDigitalActionHandle(m_pSteamController,pszActionName);
            return result;
        }
        internal override ControllerDigitalActionData_t GetDigitalActionData( ulong controllerHandle, ulong digitalActionHandle )
        {
            CheckIfUsable();
            ControllerDigitalActionData_t result = NativeEntrypoints.SteamAPI_ISteamController_GetDigitalActionData(m_pSteamController,controllerHandle,digitalActionHandle);
            return result;
        }
        internal override int GetDigitalActionOrigins( ulong controllerHandle, ulong actionSetHandle, ulong digitalActionHandle, ref uint originsOut )
        {
            CheckIfUsable();
            originsOut = 0;
            int result = NativeEntrypoints.SteamAPI_ISteamController_GetDigitalActionOrigins(m_pSteamController,controllerHandle,actionSetHandle,digitalActionHandle,ref originsOut);
            return result;
        }
        internal override ulong GetAnalogActionHandle( string pszActionName )
        {
            CheckIfUsable();
            ulong result = NativeEntrypoints.SteamAPI_ISteamController_GetAnalogActionHandle(m_pSteamController,pszActionName);
            return result;
        }
        internal override ControllerAnalogActionData_t GetAnalogActionData( ulong controllerHandle, ulong analogActionHandle )
        {
            CheckIfUsable();
            ControllerAnalogActionData_t result = NativeEntrypoints.SteamAPI_ISteamController_GetAnalogActionData(m_pSteamController,controllerHandle,analogActionHandle);
            return result;
        }
        internal override int GetAnalogActionOrigins( ulong controllerHandle, ulong actionSetHandle, ulong analogActionHandle, ref uint originsOut )
        {
            CheckIfUsable();
            originsOut = 0;
            int result = NativeEntrypoints.SteamAPI_ISteamController_GetAnalogActionOrigins(m_pSteamController,controllerHandle,actionSetHandle,analogActionHandle,ref originsOut);
            return result;
        }
        internal override void StopAnalogActionMomentum( ulong controllerHandle, ulong eAction )
        {
            CheckIfUsable();
            NativeEntrypoints.SteamAPI_ISteamController_StopAnalogActionMomentum( m_pSteamController, controllerHandle, eAction );
        }
        internal override void TriggerHapticPulse( ulong controllerHandle, uint eTargetPad, char usDurationMicroSec )
        {
            CheckIfUsable();
            NativeEntrypoints.SteamAPI_ISteamController_TriggerHapticPulse( m_pSteamController, controllerHandle, eTargetPad, usDurationMicroSec );
        }
        internal override void TriggerRepeatedHapticPulse( ulong controllerHandle, uint eTargetPad, char usDurationMicroSec, char usOffMicroSec, char unRepeat, uint nFlags )
        {
            CheckIfUsable();
            NativeEntrypoints.SteamAPI_ISteamController_TriggerRepeatedHapticPulse( m_pSteamController, controllerHandle, eTargetPad, usDurationMicroSec, usOffMicroSec, unRepeat, nFlags );
        }
    }


    internal class CSteamUGC : ISteamUGC
    {
        public CSteamUGC( IntPtr SteamUGC )
        {
            m_pSteamUGC = SteamUGC;
        }
        IntPtr m_pSteamUGC;

        internal override IntPtr GetIntPtr() { return m_pSteamUGC; }

        private void CheckIfUsable()
        {
            if ( m_pSteamUGC == IntPtr.Zero )
            {
                throw new Exception( "Steam Pointer not configured" );
            }
        }
        internal override ulong CreateQueryUserUGCRequest( uint unAccountID, uint eListType, uint eMatchingUGCType, uint eSortOrder, uint nCreatorAppID, uint nConsumerAppID, uint unPage )
        {
            CheckIfUsable();
            ulong result = NativeEntrypoints.SteamAPI_ISteamUGC_CreateQueryUserUGCRequest(m_pSteamUGC,unAccountID,eListType,eMatchingUGCType,eSortOrder,nCreatorAppID,nConsumerAppID,unPage);
            return result;
        }
        internal override ulong CreateQueryAllUGCRequest( uint eQueryType, uint eMatchingeMatchingUGCTypeFileType, uint nCreatorAppID, uint nConsumerAppID, uint unPage )
        {
            CheckIfUsable();
            ulong result = NativeEntrypoints.SteamAPI_ISteamUGC_CreateQueryAllUGCRequest(m_pSteamUGC,eQueryType,eMatchingeMatchingUGCTypeFileType,nCreatorAppID,nConsumerAppID,unPage);
            return result;
        }
        internal override ulong CreateQueryUGCDetailsRequest( ref ulong pvecPublishedFileID, uint unNumPublishedFileIDs )
        {
            CheckIfUsable();
            pvecPublishedFileID = 0;
            ulong result = NativeEntrypoints.SteamAPI_ISteamUGC_CreateQueryUGCDetailsRequest(m_pSteamUGC,ref pvecPublishedFileID,unNumPublishedFileIDs);
            return result;
        }
        internal override ulong SendQueryUGCRequest( ulong handle )
        {
            CheckIfUsable();
            ulong result = NativeEntrypoints.SteamAPI_ISteamUGC_SendQueryUGCRequest(m_pSteamUGC,handle);
            return result;
        }
        internal override bool GetQueryUGCResult( ulong handle, uint index, ref SteamUGCDetails_t pDetails )
        {
            CheckIfUsable();
            bool result = NativeEntrypoints.SteamAPI_ISteamUGC_GetQueryUGCResult(m_pSteamUGC,handle,index,ref pDetails);
            return result;
        }
        internal override bool GetQueryUGCPreviewURL( ulong handle, uint index, out string pchURL )
        {
            CheckIfUsable();
            System.Text.StringBuilder pStrBuffer1 = new System.Text.StringBuilder(2048);
            bool result = NativeEntrypoints.SteamAPI_ISteamUGC_GetQueryUGCPreviewURL(m_pSteamUGC,handle,index,pStrBuffer1,2048);
            pchURL = pStrBuffer1.ToString();
            return result;
        }
        internal override bool GetQueryUGCMetadata( ulong handle, uint index, out string pchMetadata )
        {
            CheckIfUsable();
            System.Text.StringBuilder pStrBuffer1 = new System.Text.StringBuilder(2048);
            bool result = NativeEntrypoints.SteamAPI_ISteamUGC_GetQueryUGCMetadata(m_pSteamUGC,handle,index,pStrBuffer1,2048);
            pchMetadata = pStrBuffer1.ToString();
            return result;
        }
        internal override bool GetQueryUGCChildren( ulong handle, uint index, ref ulong pvecPublishedFileID, uint cMaxEntries )
        {
            CheckIfUsable();
            pvecPublishedFileID = 0;
            bool result = NativeEntrypoints.SteamAPI_ISteamUGC_GetQueryUGCChildren(m_pSteamUGC,handle,index,ref pvecPublishedFileID,cMaxEntries);
            return result;
        }
        internal override bool GetQueryUGCStatistic( ulong handle, uint index, uint eStatType, ref uint pStatValue )
        {
            CheckIfUsable();
            pStatValue = 0;
            bool result = NativeEntrypoints.SteamAPI_ISteamUGC_GetQueryUGCStatistic(m_pSteamUGC,handle,index,eStatType,ref pStatValue);
            return result;
        }
        internal override uint GetQueryUGCNumAdditionalPreviews( ulong handle, uint index )
        {
            CheckIfUsable();
            uint result = NativeEntrypoints.SteamAPI_ISteamUGC_GetQueryUGCNumAdditionalPreviews(m_pSteamUGC,handle,index);
            return result;
        }
        internal override bool GetQueryUGCAdditionalPreview( ulong handle, uint index, uint previewIndex, out string pchURLOrVideoID, out string pchOriginalFileName, uint cchOriginalFileNameSize, ref uint pPreviewType )
        {
            CheckIfUsable();
            pPreviewType = 0;
            System.Text.StringBuilder pStrBuffer1 = new System.Text.StringBuilder(2048);
            System.Text.StringBuilder pStrBuffer2 = new System.Text.StringBuilder(2048);
            bool result = NativeEntrypoints.SteamAPI_ISteamUGC_GetQueryUGCAdditionalPreview(m_pSteamUGC,handle,index,previewIndex,pStrBuffer1,2048,pStrBuffer2,cchOriginalFileNameSize,ref pPreviewType);
            pchURLOrVideoID = pStrBuffer1.ToString();
            pchOriginalFileName = pStrBuffer2.ToString();
            return result;
        }
        internal override uint GetQueryUGCNumKeyValueTags( ulong handle, uint index )
        {
            CheckIfUsable();
            uint result = NativeEntrypoints.SteamAPI_ISteamUGC_GetQueryUGCNumKeyValueTags(m_pSteamUGC,handle,index);
            return result;
        }
        internal override bool GetQueryUGCKeyValueTag( ulong handle, uint index, uint keyValueTagIndex, out string pchKey, out string pchValue )
        {
            CheckIfUsable();
            System.Text.StringBuilder pStrBuffer1 = new System.Text.StringBuilder(2048);
            System.Text.StringBuilder pStrBuffer2 = new System.Text.StringBuilder(2048);
            bool result = NativeEntrypoints.SteamAPI_ISteamUGC_GetQueryUGCKeyValueTag(m_pSteamUGC,handle,index,keyValueTagIndex,pStrBuffer1,2048,pStrBuffer2,2048);
            pchKey = pStrBuffer1.ToString();
            pchValue = pStrBuffer2.ToString();
            return result;
        }
        internal override bool ReleaseQueryUGCRequest( ulong handle )
        {
            CheckIfUsable();
            bool result = NativeEntrypoints.SteamAPI_ISteamUGC_ReleaseQueryUGCRequest(m_pSteamUGC,handle);
            return result;
        }
        internal override bool AddRequiredTag( ulong handle, string pTagName )
        {
            CheckIfUsable();
            bool result = NativeEntrypoints.SteamAPI_ISteamUGC_AddRequiredTag(m_pSteamUGC,handle,pTagName);
            return result;
        }
        internal override bool AddExcludedTag( ulong handle, string pTagName )
        {
            CheckIfUsable();
            bool result = NativeEntrypoints.SteamAPI_ISteamUGC_AddExcludedTag(m_pSteamUGC,handle,pTagName);
            return result;
        }
        internal override bool SetReturnKeyValueTags( ulong handle, bool bReturnKeyValueTags )
        {
            CheckIfUsable();
            bool result = NativeEntrypoints.SteamAPI_ISteamUGC_SetReturnKeyValueTags(m_pSteamUGC,handle,bReturnKeyValueTags);
            return result;
        }
        internal override bool SetReturnLongDescription( ulong handle, bool bReturnLongDescription )
        {
            CheckIfUsable();
            bool result = NativeEntrypoints.SteamAPI_ISteamUGC_SetReturnLongDescription(m_pSteamUGC,handle,bReturnLongDescription);
            return result;
        }
        internal override bool SetReturnMetadata( ulong handle, bool bReturnMetadata )
        {
            CheckIfUsable();
            bool result = NativeEntrypoints.SteamAPI_ISteamUGC_SetReturnMetadata(m_pSteamUGC,handle,bReturnMetadata);
            return result;
        }
        internal override bool SetReturnChildren( ulong handle, bool bReturnChildren )
        {
            CheckIfUsable();
            bool result = NativeEntrypoints.SteamAPI_ISteamUGC_SetReturnChildren(m_pSteamUGC,handle,bReturnChildren);
            return result;
        }
        internal override bool SetReturnAdditionalPreviews( ulong handle, bool bReturnAdditionalPreviews )
        {
            CheckIfUsable();
            bool result = NativeEntrypoints.SteamAPI_ISteamUGC_SetReturnAdditionalPreviews(m_pSteamUGC,handle,bReturnAdditionalPreviews);
            return result;
        }
        internal override bool SetReturnTotalOnly( ulong handle, bool bReturnTotalOnly )
        {
            CheckIfUsable();
            bool result = NativeEntrypoints.SteamAPI_ISteamUGC_SetReturnTotalOnly(m_pSteamUGC,handle,bReturnTotalOnly);
            return result;
        }
        internal override bool SetLanguage( ulong handle, string pchLanguage )
        {
            CheckIfUsable();
            bool result = NativeEntrypoints.SteamAPI_ISteamUGC_SetLanguage(m_pSteamUGC,handle,pchLanguage);
            return result;
        }
        internal override bool SetAllowCachedResponse( ulong handle, uint unMaxAgeSeconds )
        {
            CheckIfUsable();
            bool result = NativeEntrypoints.SteamAPI_ISteamUGC_SetAllowCachedResponse(m_pSteamUGC,handle,unMaxAgeSeconds);
            return result;
        }
        internal override bool SetCloudFileNameFilter( ulong handle, string pMatchCloudFileName )
        {
            CheckIfUsable();
            bool result = NativeEntrypoints.SteamAPI_ISteamUGC_SetCloudFileNameFilter(m_pSteamUGC,handle,pMatchCloudFileName);
            return result;
        }
        internal override bool SetMatchAnyTag( ulong handle, bool bMatchAnyTag )
        {
            CheckIfUsable();
            bool result = NativeEntrypoints.SteamAPI_ISteamUGC_SetMatchAnyTag(m_pSteamUGC,handle,bMatchAnyTag);
            return result;
        }
        internal override bool SetSearchText( ulong handle, string pSearchText )
        {
            CheckIfUsable();
            bool result = NativeEntrypoints.SteamAPI_ISteamUGC_SetSearchText(m_pSteamUGC,handle,pSearchText);
            return result;
        }
        internal override bool SetRankedByTrendDays( ulong handle, uint unDays )
        {
            CheckIfUsable();
            bool result = NativeEntrypoints.SteamAPI_ISteamUGC_SetRankedByTrendDays(m_pSteamUGC,handle,unDays);
            return result;
        }
        internal override bool AddRequiredKeyValueTag( ulong handle, string pKey, string pValue )
        {
            CheckIfUsable();
            bool result = NativeEntrypoints.SteamAPI_ISteamUGC_AddRequiredKeyValueTag(m_pSteamUGC,handle,pKey,pValue);
            return result;
        }
        internal override ulong RequestUGCDetails( ulong nPublishedFileID, uint unMaxAgeSeconds )
        {
            CheckIfUsable();
            ulong result = NativeEntrypoints.SteamAPI_ISteamUGC_RequestUGCDetails(m_pSteamUGC,nPublishedFileID,unMaxAgeSeconds);
            return result;
        }
        internal override ulong CreateItem( uint nConsumerAppId, uint eFileType )
        {
            CheckIfUsable();
            ulong result = NativeEntrypoints.SteamAPI_ISteamUGC_CreateItem(m_pSteamUGC,nConsumerAppId,eFileType);
            return result;
        }
        internal override ulong StartItemUpdate( uint nConsumerAppId, ulong nPublishedFileID )
        {
            CheckIfUsable();
            ulong result = NativeEntrypoints.SteamAPI_ISteamUGC_StartItemUpdate(m_pSteamUGC,nConsumerAppId,nPublishedFileID);
            return result;
        }
        internal override bool SetItemTitle( ulong handle, string pchTitle )
        {
            CheckIfUsable();
            bool result = NativeEntrypoints.SteamAPI_ISteamUGC_SetItemTitle(m_pSteamUGC,handle,pchTitle);
            return result;
        }
        internal override bool SetItemDescription( ulong handle, string pchDescription )
        {
            CheckIfUsable();
            bool result = NativeEntrypoints.SteamAPI_ISteamUGC_SetItemDescription(m_pSteamUGC,handle,pchDescription);
            return result;
        }
        internal override bool SetItemUpdateLanguage( ulong handle, string pchLanguage )
        {
            CheckIfUsable();
            bool result = NativeEntrypoints.SteamAPI_ISteamUGC_SetItemUpdateLanguage(m_pSteamUGC,handle,pchLanguage);
            return result;
        }
        internal override bool SetItemMetadata( ulong handle, string pchMetaData )
        {
            CheckIfUsable();
            bool result = NativeEntrypoints.SteamAPI_ISteamUGC_SetItemMetadata(m_pSteamUGC,handle,pchMetaData);
            return result;
        }
        internal override bool SetItemVisibility( ulong handle, uint eVisibility )
        {
            CheckIfUsable();
            bool result = NativeEntrypoints.SteamAPI_ISteamUGC_SetItemVisibility(m_pSteamUGC,handle,eVisibility);
            return result;
        }
        internal override bool SetItemTags( ulong updateHandle, ref SteamParamStringArray_t pTags )
        {
            CheckIfUsable();
            bool result = NativeEntrypoints.SteamAPI_ISteamUGC_SetItemTags(m_pSteamUGC,updateHandle,ref pTags);
            return result;
        }
        internal override bool SetItemContent( ulong handle, string pszContentFolder )
        {
            CheckIfUsable();
            bool result = NativeEntrypoints.SteamAPI_ISteamUGC_SetItemContent(m_pSteamUGC,handle,pszContentFolder);
            return result;
        }
        internal override bool SetItemPreview( ulong handle, string pszPreviewFile )
        {
            CheckIfUsable();
            bool result = NativeEntrypoints.SteamAPI_ISteamUGC_SetItemPreview(m_pSteamUGC,handle,pszPreviewFile);
            return result;
        }
        internal override bool RemoveItemKeyValueTags( ulong handle, string pchKey )
        {
            CheckIfUsable();
            bool result = NativeEntrypoints.SteamAPI_ISteamUGC_RemoveItemKeyValueTags(m_pSteamUGC,handle,pchKey);
            return result;
        }
        internal override bool AddItemKeyValueTag( ulong handle, string pchKey, string pchValue )
        {
            CheckIfUsable();
            bool result = NativeEntrypoints.SteamAPI_ISteamUGC_AddItemKeyValueTag(m_pSteamUGC,handle,pchKey,pchValue);
            return result;
        }
        internal override bool AddItemPreviewFile( ulong handle, string pszPreviewFile, uint type )
        {
            CheckIfUsable();
            bool result = NativeEntrypoints.SteamAPI_ISteamUGC_AddItemPreviewFile(m_pSteamUGC,handle,pszPreviewFile,type);
            return result;
        }
        internal override bool AddItemPreviewVideo( ulong handle, string pszVideoID )
        {
            CheckIfUsable();
            bool result = NativeEntrypoints.SteamAPI_ISteamUGC_AddItemPreviewVideo(m_pSteamUGC,handle,pszVideoID);
            return result;
        }
        internal override bool UpdateItemPreviewFile( ulong handle, uint index, string pszPreviewFile )
        {
            CheckIfUsable();
            bool result = NativeEntrypoints.SteamAPI_ISteamUGC_UpdateItemPreviewFile(m_pSteamUGC,handle,index,pszPreviewFile);
            return result;
        }
        internal override bool UpdateItemPreviewVideo( ulong handle, uint index, string pszVideoID )
        {
            CheckIfUsable();
            bool result = NativeEntrypoints.SteamAPI_ISteamUGC_UpdateItemPreviewVideo(m_pSteamUGC,handle,index,pszVideoID);
            return result;
        }
        internal override bool RemoveItemPreview( ulong handle, uint index )
        {
            CheckIfUsable();
            bool result = NativeEntrypoints.SteamAPI_ISteamUGC_RemoveItemPreview(m_pSteamUGC,handle,index);
            return result;
        }
        internal override ulong SubmitItemUpdate( ulong handle, string pchChangeNote )
        {
            CheckIfUsable();
            ulong result = NativeEntrypoints.SteamAPI_ISteamUGC_SubmitItemUpdate(m_pSteamUGC,handle,pchChangeNote);
            return result;
        }
        internal override uint GetItemUpdateProgress( ulong handle, ref ulong punBytesProcessed, ref ulong punBytesTotal )
        {
            CheckIfUsable();
            punBytesProcessed = 0;
            punBytesTotal = 0;
            uint result = NativeEntrypoints.SteamAPI_ISteamUGC_GetItemUpdateProgress(m_pSteamUGC,handle,ref punBytesProcessed,ref punBytesTotal);
            return result;
        }
        internal override ulong SetUserItemVote( ulong nPublishedFileID, bool bVoteUp )
        {
            CheckIfUsable();
            ulong result = NativeEntrypoints.SteamAPI_ISteamUGC_SetUserItemVote(m_pSteamUGC,nPublishedFileID,bVoteUp);
            return result;
        }
        internal override ulong GetUserItemVote( ulong nPublishedFileID )
        {
            CheckIfUsable();
            ulong result = NativeEntrypoints.SteamAPI_ISteamUGC_GetUserItemVote(m_pSteamUGC,nPublishedFileID);
            return result;
        }
        internal override ulong AddItemToFavorites( uint nAppId, ulong nPublishedFileID )
        {
            CheckIfUsable();
            ulong result = NativeEntrypoints.SteamAPI_ISteamUGC_AddItemToFavorites(m_pSteamUGC,nAppId,nPublishedFileID);
            return result;
        }
        internal override ulong RemoveItemFromFavorites( uint nAppId, ulong nPublishedFileID )
        {
            CheckIfUsable();
            ulong result = NativeEntrypoints.SteamAPI_ISteamUGC_RemoveItemFromFavorites(m_pSteamUGC,nAppId,nPublishedFileID);
            return result;
        }
        internal override ulong SubscribeItem( ulong nPublishedFileID )
        {
            CheckIfUsable();
            ulong result = NativeEntrypoints.SteamAPI_ISteamUGC_SubscribeItem(m_pSteamUGC,nPublishedFileID);
            return result;
        }
        internal override ulong UnsubscribeItem( ulong nPublishedFileID )
        {
            CheckIfUsable();
            ulong result = NativeEntrypoints.SteamAPI_ISteamUGC_UnsubscribeItem(m_pSteamUGC,nPublishedFileID);
            return result;
        }
        internal override uint GetNumSubscribedItems()
        {
            CheckIfUsable();
            uint result = NativeEntrypoints.SteamAPI_ISteamUGC_GetNumSubscribedItems(m_pSteamUGC);
            return result;
        }
        internal override uint GetSubscribedItems( ref ulong pvecPublishedFileID, uint cMaxEntries )
        {
            CheckIfUsable();
            pvecPublishedFileID = 0;
            uint result = NativeEntrypoints.SteamAPI_ISteamUGC_GetSubscribedItems(m_pSteamUGC,ref pvecPublishedFileID,cMaxEntries);
            return result;
        }
        internal override uint GetItemState( ulong nPublishedFileID )
        {
            CheckIfUsable();
            uint result = NativeEntrypoints.SteamAPI_ISteamUGC_GetItemState(m_pSteamUGC,nPublishedFileID);
            return result;
        }
        internal override bool GetItemInstallInfo( ulong nPublishedFileID, ref ulong punSizeOnDisk, out string pchFolder, ref uint punTimeStamp )
        {
            CheckIfUsable();
            punSizeOnDisk = 0;
            punTimeStamp = 0;
            System.Text.StringBuilder pStrBuffer1 = new System.Text.StringBuilder(2048);
            bool result = NativeEntrypoints.SteamAPI_ISteamUGC_GetItemInstallInfo(m_pSteamUGC,nPublishedFileID,ref punSizeOnDisk,pStrBuffer1,2048,ref punTimeStamp);
            pchFolder = pStrBuffer1.ToString();
            return result;
        }
        internal override bool GetItemDownloadInfo( ulong nPublishedFileID, ref ulong punBytesDownloaded, ref ulong punBytesTotal )
        {
            CheckIfUsable();
            punBytesDownloaded = 0;
            punBytesTotal = 0;
            bool result = NativeEntrypoints.SteamAPI_ISteamUGC_GetItemDownloadInfo(m_pSteamUGC,nPublishedFileID,ref punBytesDownloaded,ref punBytesTotal);
            return result;
        }
        internal override bool DownloadItem( ulong nPublishedFileID, bool bHighPriority )
        {
            CheckIfUsable();
            bool result = NativeEntrypoints.SteamAPI_ISteamUGC_DownloadItem(m_pSteamUGC,nPublishedFileID,bHighPriority);
            return result;
        }
        internal override bool BInitWorkshopForGameServer( uint unWorkshopDepotID, string pszFolder )
        {
            CheckIfUsable();
            bool result = NativeEntrypoints.SteamAPI_ISteamUGC_BInitWorkshopForGameServer(m_pSteamUGC,unWorkshopDepotID,pszFolder);
            return result;
        }
        internal override void SuspendDownloads( bool bSuspend )
        {
            CheckIfUsable();
            NativeEntrypoints.SteamAPI_ISteamUGC_SuspendDownloads( m_pSteamUGC, bSuspend );
        }
    }


    internal class CSteamAppList : ISteamAppList
    {
        public CSteamAppList( IntPtr SteamAppList )
        {
            m_pSteamAppList = SteamAppList;
        }
        IntPtr m_pSteamAppList;

        internal override IntPtr GetIntPtr() { return m_pSteamAppList; }

        private void CheckIfUsable()
        {
            if ( m_pSteamAppList == IntPtr.Zero )
            {
                throw new Exception( "Steam Pointer not configured" );
            }
        }
        internal override uint GetNumInstalledApps()
        {
            CheckIfUsable();
            uint result = NativeEntrypoints.SteamAPI_ISteamAppList_GetNumInstalledApps(m_pSteamAppList);
            return result;
        }
        internal override uint GetInstalledApps( ref uint pvecAppID, uint unMaxAppIDs )
        {
            CheckIfUsable();
            pvecAppID = 0;
            uint result = NativeEntrypoints.SteamAPI_ISteamAppList_GetInstalledApps(m_pSteamAppList,ref pvecAppID,unMaxAppIDs);
            return result;
        }
        internal override int GetAppName( uint nAppID, System.Text.StringBuilder pchName, int cchNameMax )
        {
            CheckIfUsable();
            int result = NativeEntrypoints.SteamAPI_ISteamAppList_GetAppName(m_pSteamAppList,nAppID,pchName,cchNameMax);
            return result;
        }
        internal override int GetAppInstallDir( uint nAppID, string pchDirectory, int cchNameMax )
        {
            CheckIfUsable();
            int result = NativeEntrypoints.SteamAPI_ISteamAppList_GetAppInstallDir(m_pSteamAppList,nAppID,pchDirectory,cchNameMax);
            return result;
        }
        internal override int GetAppBuildId( uint nAppID )
        {
            CheckIfUsable();
            int result = NativeEntrypoints.SteamAPI_ISteamAppList_GetAppBuildId(m_pSteamAppList,nAppID);
            return result;
        }
    }


    internal class CSteamHTMLSurface : ISteamHTMLSurface
    {
        public CSteamHTMLSurface( IntPtr SteamHTMLSurface )
        {
            m_pSteamHTMLSurface = SteamHTMLSurface;
        }
        IntPtr m_pSteamHTMLSurface;

        internal override IntPtr GetIntPtr() { return m_pSteamHTMLSurface; }

        private void CheckIfUsable()
        {
            if ( m_pSteamHTMLSurface == IntPtr.Zero )
            {
                throw new Exception( "Steam Pointer not configured" );
            }
        }
        internal override void DestructISteamHTMLSurface()
        {
            CheckIfUsable();
            NativeEntrypoints.SteamAPI_ISteamHTMLSurface_DestructISteamHTMLSurface( m_pSteamHTMLSurface );
        }
        internal override bool Init()
        {
            CheckIfUsable();
            bool result = NativeEntrypoints.SteamAPI_ISteamHTMLSurface_Init(m_pSteamHTMLSurface);
            return result;
        }
        internal override bool Shutdown()
        {
            CheckIfUsable();
            bool result = NativeEntrypoints.SteamAPI_ISteamHTMLSurface_Shutdown(m_pSteamHTMLSurface);
            return result;
        }
        internal override ulong CreateBrowser( string pchUserAgent, string pchUserCSS )
        {
            CheckIfUsable();
            ulong result = NativeEntrypoints.SteamAPI_ISteamHTMLSurface_CreateBrowser(m_pSteamHTMLSurface,pchUserAgent,pchUserCSS);
            return result;
        }
        internal override void RemoveBrowser( uint unBrowserHandle )
        {
            CheckIfUsable();
            NativeEntrypoints.SteamAPI_ISteamHTMLSurface_RemoveBrowser( m_pSteamHTMLSurface, unBrowserHandle );
        }
        internal override void LoadURL( uint unBrowserHandle, string pchURL, string pchPostData )
        {
            CheckIfUsable();
            NativeEntrypoints.SteamAPI_ISteamHTMLSurface_LoadURL( m_pSteamHTMLSurface, unBrowserHandle, pchURL, pchPostData );
        }
        internal override void SetSize( uint unBrowserHandle, uint unWidth, uint unHeight )
        {
            CheckIfUsable();
            NativeEntrypoints.SteamAPI_ISteamHTMLSurface_SetSize( m_pSteamHTMLSurface, unBrowserHandle, unWidth, unHeight );
        }
        internal override void StopLoad( uint unBrowserHandle )
        {
            CheckIfUsable();
            NativeEntrypoints.SteamAPI_ISteamHTMLSurface_StopLoad( m_pSteamHTMLSurface, unBrowserHandle );
        }
        internal override void Reload( uint unBrowserHandle )
        {
            CheckIfUsable();
            NativeEntrypoints.SteamAPI_ISteamHTMLSurface_Reload( m_pSteamHTMLSurface, unBrowserHandle );
        }
        internal override void GoBack( uint unBrowserHandle )
        {
            CheckIfUsable();
            NativeEntrypoints.SteamAPI_ISteamHTMLSurface_GoBack( m_pSteamHTMLSurface, unBrowserHandle );
        }
        internal override void GoForward( uint unBrowserHandle )
        {
            CheckIfUsable();
            NativeEntrypoints.SteamAPI_ISteamHTMLSurface_GoForward( m_pSteamHTMLSurface, unBrowserHandle );
        }
        internal override void AddHeader( uint unBrowserHandle, string pchKey, string pchValue )
        {
            CheckIfUsable();
            NativeEntrypoints.SteamAPI_ISteamHTMLSurface_AddHeader( m_pSteamHTMLSurface, unBrowserHandle, pchKey, pchValue );
        }
        internal override void ExecuteJavascript( uint unBrowserHandle, string pchScript )
        {
            CheckIfUsable();
            NativeEntrypoints.SteamAPI_ISteamHTMLSurface_ExecuteJavascript( m_pSteamHTMLSurface, unBrowserHandle, pchScript );
        }
        internal override void MouseUp( uint unBrowserHandle, uint eMouseButton )
        {
            CheckIfUsable();
            NativeEntrypoints.SteamAPI_ISteamHTMLSurface_MouseUp( m_pSteamHTMLSurface, unBrowserHandle, eMouseButton );
        }
        internal override void MouseDown( uint unBrowserHandle, uint eMouseButton )
        {
            CheckIfUsable();
            NativeEntrypoints.SteamAPI_ISteamHTMLSurface_MouseDown( m_pSteamHTMLSurface, unBrowserHandle, eMouseButton );
        }
        internal override void MouseDoubleClick( uint unBrowserHandle, uint eMouseButton )
        {
            CheckIfUsable();
            NativeEntrypoints.SteamAPI_ISteamHTMLSurface_MouseDoubleClick( m_pSteamHTMLSurface, unBrowserHandle, eMouseButton );
        }
        internal override void MouseMove( uint unBrowserHandle, int x, int y )
        {
            CheckIfUsable();
            NativeEntrypoints.SteamAPI_ISteamHTMLSurface_MouseMove( m_pSteamHTMLSurface, unBrowserHandle, x, y );
        }
        internal override void MouseWheel( uint unBrowserHandle, int nDelta )
        {
            CheckIfUsable();
            NativeEntrypoints.SteamAPI_ISteamHTMLSurface_MouseWheel( m_pSteamHTMLSurface, unBrowserHandle, nDelta );
        }
        internal override void KeyDown( uint unBrowserHandle, uint nNativeKeyCode, uint eHTMLKeyModifiers )
        {
            CheckIfUsable();
            NativeEntrypoints.SteamAPI_ISteamHTMLSurface_KeyDown( m_pSteamHTMLSurface, unBrowserHandle, nNativeKeyCode, eHTMLKeyModifiers );
        }
        internal override void KeyUp( uint unBrowserHandle, uint nNativeKeyCode, uint eHTMLKeyModifiers )
        {
            CheckIfUsable();
            NativeEntrypoints.SteamAPI_ISteamHTMLSurface_KeyUp( m_pSteamHTMLSurface, unBrowserHandle, nNativeKeyCode, eHTMLKeyModifiers );
        }
        internal override void KeyChar( uint unBrowserHandle, uint cUnicodeChar, uint eHTMLKeyModifiers )
        {
            CheckIfUsable();
            NativeEntrypoints.SteamAPI_ISteamHTMLSurface_KeyChar( m_pSteamHTMLSurface, unBrowserHandle, cUnicodeChar, eHTMLKeyModifiers );
        }
        internal override void SetHorizontalScroll( uint unBrowserHandle, uint nAbsolutePixelScroll )
        {
            CheckIfUsable();
            NativeEntrypoints.SteamAPI_ISteamHTMLSurface_SetHorizontalScroll( m_pSteamHTMLSurface, unBrowserHandle, nAbsolutePixelScroll );
        }
        internal override void SetVerticalScroll( uint unBrowserHandle, uint nAbsolutePixelScroll )
        {
            CheckIfUsable();
            NativeEntrypoints.SteamAPI_ISteamHTMLSurface_SetVerticalScroll( m_pSteamHTMLSurface, unBrowserHandle, nAbsolutePixelScroll );
        }
        internal override void SetKeyFocus( uint unBrowserHandle, bool bHasKeyFocus )
        {
            CheckIfUsable();
            NativeEntrypoints.SteamAPI_ISteamHTMLSurface_SetKeyFocus( m_pSteamHTMLSurface, unBrowserHandle, bHasKeyFocus );
        }
        internal override void ViewSource( uint unBrowserHandle )
        {
            CheckIfUsable();
            NativeEntrypoints.SteamAPI_ISteamHTMLSurface_ViewSource( m_pSteamHTMLSurface, unBrowserHandle );
        }
        internal override void CopyToClipboard( uint unBrowserHandle )
        {
            CheckIfUsable();
            NativeEntrypoints.SteamAPI_ISteamHTMLSurface_CopyToClipboard( m_pSteamHTMLSurface, unBrowserHandle );
        }
        internal override void PasteFromClipboard( uint unBrowserHandle )
        {
            CheckIfUsable();
            NativeEntrypoints.SteamAPI_ISteamHTMLSurface_PasteFromClipboard( m_pSteamHTMLSurface, unBrowserHandle );
        }
        internal override void Find( uint unBrowserHandle, string pchSearchStr, bool bCurrentlyInFind, bool bReverse )
        {
            CheckIfUsable();
            NativeEntrypoints.SteamAPI_ISteamHTMLSurface_Find( m_pSteamHTMLSurface, unBrowserHandle, pchSearchStr, bCurrentlyInFind, bReverse );
        }
        internal override void StopFind( uint unBrowserHandle )
        {
            CheckIfUsable();
            NativeEntrypoints.SteamAPI_ISteamHTMLSurface_StopFind( m_pSteamHTMLSurface, unBrowserHandle );
        }
        internal override void GetLinkAtPosition( uint unBrowserHandle, int x, int y )
        {
            CheckIfUsable();
            NativeEntrypoints.SteamAPI_ISteamHTMLSurface_GetLinkAtPosition( m_pSteamHTMLSurface, unBrowserHandle, x, y );
        }
        internal override void SetCookie( string pchHostname, string pchKey, string pchValue, string pchPath, ulong nExpires, bool bSecure, bool bHTTPOnly )
        {
            CheckIfUsable();
            NativeEntrypoints.SteamAPI_ISteamHTMLSurface_SetCookie( m_pSteamHTMLSurface, pchHostname, pchKey, pchValue, pchPath, nExpires, bSecure, bHTTPOnly );
        }
        internal override void SetPageScaleFactor( uint unBrowserHandle, float flZoom, int nPointX, int nPointY )
        {
            CheckIfUsable();
            NativeEntrypoints.SteamAPI_ISteamHTMLSurface_SetPageScaleFactor( m_pSteamHTMLSurface, unBrowserHandle, flZoom, nPointX, nPointY );
        }
        internal override void SetBackgroundMode( uint unBrowserHandle, bool bBackgroundMode )
        {
            CheckIfUsable();
            NativeEntrypoints.SteamAPI_ISteamHTMLSurface_SetBackgroundMode( m_pSteamHTMLSurface, unBrowserHandle, bBackgroundMode );
        }
        internal override void AllowStartRequest( uint unBrowserHandle, bool bAllowed )
        {
            CheckIfUsable();
            NativeEntrypoints.SteamAPI_ISteamHTMLSurface_AllowStartRequest( m_pSteamHTMLSurface, unBrowserHandle, bAllowed );
        }
        internal override void JSDialogResponse( uint unBrowserHandle, bool bResult )
        {
            CheckIfUsable();
            NativeEntrypoints.SteamAPI_ISteamHTMLSurface_JSDialogResponse( m_pSteamHTMLSurface, unBrowserHandle, bResult );
        }
    }


    internal class CSteamInventory : ISteamInventory
    {
        public CSteamInventory( IntPtr SteamInventory )
        {
            m_pSteamInventory = SteamInventory;
        }
        IntPtr m_pSteamInventory;

        internal override IntPtr GetIntPtr() { return m_pSteamInventory; }

        private void CheckIfUsable()
        {
            if ( m_pSteamInventory == IntPtr.Zero )
            {
                throw new Exception( "Steam Pointer not configured" );
            }
        }
        internal override uint GetResultStatus( int resultHandle )
        {
            CheckIfUsable();
            uint result = NativeEntrypoints.SteamAPI_ISteamInventory_GetResultStatus(m_pSteamInventory,resultHandle);
            return result;
        }
        internal override bool GetResultItems( int resultHandle, out SteamItemDetails_t[] pOutItemsArray )
        {
            CheckIfUsable();
            uint punOutItemsArraySize = 0;
            bool result = NativeEntrypoints.SteamAPI_ISteamInventory_GetResultItems(m_pSteamInventory,resultHandle,null,ref punOutItemsArraySize);
            pOutItemsArray = new SteamItemDetails_t[punOutItemsArraySize];
            result = NativeEntrypoints.SteamAPI_ISteamInventory_GetResultItems( m_pSteamInventory, resultHandle, pOutItemsArray, ref punOutItemsArraySize );
            return result;
        }
        internal override uint GetResultTimestamp( int resultHandle )
        {
            CheckIfUsable();
            uint result = NativeEntrypoints.SteamAPI_ISteamInventory_GetResultTimestamp(m_pSteamInventory,resultHandle);
            return result;
        }
        internal override bool CheckResultSteamID( int resultHandle, ulong steamIDExpected )
        {
            CheckIfUsable();
            bool result = NativeEntrypoints.SteamAPI_ISteamInventory_CheckResultSteamID(m_pSteamInventory,resultHandle,steamIDExpected);
            return result;
        }
        internal override void DestroyResult( int resultHandle )
        {
            CheckIfUsable();
            NativeEntrypoints.SteamAPI_ISteamInventory_DestroyResult( m_pSteamInventory, resultHandle );
        }
        internal override bool GetAllItems( ref int pResultHandle )
        {
            CheckIfUsable();
            pResultHandle = 0;
            bool result = NativeEntrypoints.SteamAPI_ISteamInventory_GetAllItems(m_pSteamInventory,ref pResultHandle);
            return result;
        }
        internal override bool GetItemsByID( ref int pResultHandle, ulong[] pInstanceIDs )
        {
            CheckIfUsable();
            pResultHandle = 0;
            bool result = NativeEntrypoints.SteamAPI_ISteamInventory_GetItemsByID(m_pSteamInventory,ref pResultHandle,pInstanceIDs,(uint) pInstanceIDs.Length);
            return result;
        }
        internal override bool SerializeResult( int resultHandle, IntPtr pOutBuffer, ref uint punOutBufferSize )
        {
            CheckIfUsable();
            punOutBufferSize = 0;
            bool result = NativeEntrypoints.SteamAPI_ISteamInventory_SerializeResult(m_pSteamInventory,resultHandle,pOutBuffer,ref punOutBufferSize);
            return result;
        }
        internal override bool DeserializeResult( ref int pOutResultHandle, IntPtr pBuffer, uint unBufferSize, bool bRESERVED_MUST_BE_FALSE )
        {
            CheckIfUsable();
            pOutResultHandle = 0;
            bool result = NativeEntrypoints.SteamAPI_ISteamInventory_DeserializeResult(m_pSteamInventory,ref pOutResultHandle,pBuffer,unBufferSize,bRESERVED_MUST_BE_FALSE);
            return result;
        }
        internal override bool GenerateItems( ref int pResultHandle, int[] pArrayItemDefs, uint[] punArrayQuantity )
        {
            CheckIfUsable();
            pResultHandle = 0;
            bool result = NativeEntrypoints.SteamAPI_ISteamInventory_GenerateItems(m_pSteamInventory,ref pResultHandle,pArrayItemDefs,punArrayQuantity,(uint) punArrayQuantity.Length);
            return result;
        }
        internal override bool GrantPromoItems( ref int pResultHandle )
        {
            CheckIfUsable();
            pResultHandle = 0;
            bool result = NativeEntrypoints.SteamAPI_ISteamInventory_GrantPromoItems(m_pSteamInventory,ref pResultHandle);
            return result;
        }
        internal override bool AddPromoItem( ref int pResultHandle, int itemDef )
        {
            CheckIfUsable();
            pResultHandle = 0;
            bool result = NativeEntrypoints.SteamAPI_ISteamInventory_AddPromoItem(m_pSteamInventory,ref pResultHandle,itemDef);
            return result;
        }
        internal override bool AddPromoItems( ref int pResultHandle, int[] pArrayItemDefs )
        {
            CheckIfUsable();
            pResultHandle = 0;
            bool result = NativeEntrypoints.SteamAPI_ISteamInventory_AddPromoItems(m_pSteamInventory,ref pResultHandle,pArrayItemDefs,(uint) pArrayItemDefs.Length);
            return result;
        }
        internal override bool ConsumeItem( ref int pResultHandle, ulong itemConsume, uint unQuantity )
        {
            CheckIfUsable();
            pResultHandle = 0;
            bool result = NativeEntrypoints.SteamAPI_ISteamInventory_ConsumeItem(m_pSteamInventory,ref pResultHandle,itemConsume,unQuantity);
            return result;
        }
        internal override bool ExchangeItems( ref int pResultHandle, int[] pArrayGenerate, uint[] punArrayGenerateQuantity, ulong[] pArrayDestroy, uint[] punArrayDestroyQuantity )
        {
            CheckIfUsable();
            pResultHandle = 0;
            bool result = NativeEntrypoints.SteamAPI_ISteamInventory_ExchangeItems(m_pSteamInventory,ref pResultHandle,pArrayGenerate,punArrayGenerateQuantity,(uint) punArrayGenerateQuantity.Length,pArrayDestroy,punArrayDestroyQuantity,(uint) punArrayDestroyQuantity.Length);
            return result;
        }
        internal override bool TransferItemQuantity( ref int pResultHandle, ulong itemIdSource, uint unQuantity, ulong itemIdDest )
        {
            CheckIfUsable();
            pResultHandle = 0;
            bool result = NativeEntrypoints.SteamAPI_ISteamInventory_TransferItemQuantity(m_pSteamInventory,ref pResultHandle,itemIdSource,unQuantity,itemIdDest);
            return result;
        }
        internal override void SendItemDropHeartbeat()
        {
            CheckIfUsable();
            NativeEntrypoints.SteamAPI_ISteamInventory_SendItemDropHeartbeat( m_pSteamInventory );
        }
        internal override bool TriggerItemDrop( ref int pResultHandle, int dropListDefinition )
        {
            CheckIfUsable();
            pResultHandle = 0;
            bool result = NativeEntrypoints.SteamAPI_ISteamInventory_TriggerItemDrop(m_pSteamInventory,ref pResultHandle,dropListDefinition);
            return result;
        }
        internal override bool TradeItems( ref int pResultHandle, ulong steamIDTradePartner, ulong[] pArrayGive, uint[] pArrayGiveQuantity, ulong[] pArrayGet, uint[] pArrayGetQuantity )
        {
            CheckIfUsable();
            pResultHandle = 0;
            bool result = NativeEntrypoints.SteamAPI_ISteamInventory_TradeItems(m_pSteamInventory,ref pResultHandle,steamIDTradePartner,pArrayGive,pArrayGiveQuantity,(uint) pArrayGiveQuantity.Length,pArrayGet,pArrayGetQuantity,(uint) pArrayGetQuantity.Length);
            return result;
        }
        internal override bool LoadItemDefinitions()
        {
            CheckIfUsable();
            bool result = NativeEntrypoints.SteamAPI_ISteamInventory_LoadItemDefinitions(m_pSteamInventory);
            return result;
        }
        internal override bool GetItemDefinitionIDs( out int[] pItemDefIDs )
        {
            CheckIfUsable();
            uint punItemDefIDsArraySize = 0;
            bool result = NativeEntrypoints.SteamAPI_ISteamInventory_GetItemDefinitionIDs(m_pSteamInventory,null,ref punItemDefIDsArraySize);
            pItemDefIDs = new int[punItemDefIDsArraySize];
            result = NativeEntrypoints.SteamAPI_ISteamInventory_GetItemDefinitionIDs( m_pSteamInventory, pItemDefIDs, ref punItemDefIDsArraySize );
            return result;
        }
        internal override bool GetItemDefinitionProperty( int iDefinition, string pchPropertyName, out string pchValueBuffer )
        {
            CheckIfUsable();
            uint punValueBufferSize = 0;
            bool result = NativeEntrypoints.SteamAPI_ISteamInventory_GetItemDefinitionProperty(m_pSteamInventory,iDefinition,pchPropertyName,null,ref punValueBufferSize);
            System.Text.StringBuilder pStrBuffer1 = new System.Text.StringBuilder((int)punValueBufferSize);
            result = NativeEntrypoints.SteamAPI_ISteamInventory_GetItemDefinitionProperty( m_pSteamInventory, iDefinition, pchPropertyName, pStrBuffer1, ref punValueBufferSize );
            pchValueBuffer = pStrBuffer1.ToString();
            return result;
        }
    }


    internal class CSteamVideo : ISteamVideo
    {
        public CSteamVideo( IntPtr SteamVideo )
        {
            m_pSteamVideo = SteamVideo;
        }
        IntPtr m_pSteamVideo;

        internal override IntPtr GetIntPtr() { return m_pSteamVideo; }

        private void CheckIfUsable()
        {
            if ( m_pSteamVideo == IntPtr.Zero )
            {
                throw new Exception( "Steam Pointer not configured" );
            }
        }
        internal override void GetVideoURL( uint unVideoAppID )
        {
            CheckIfUsable();
            NativeEntrypoints.SteamAPI_ISteamVideo_GetVideoURL( m_pSteamVideo, unVideoAppID );
        }
        internal override bool IsBroadcasting( ref int pnNumViewers )
        {
            CheckIfUsable();
            pnNumViewers = 0;
            bool result = NativeEntrypoints.SteamAPI_ISteamVideo_IsBroadcasting(m_pSteamVideo,ref pnNumViewers);
            return result;
        }
    }


    internal class CSteamGameServer : ISteamGameServer
    {
        public CSteamGameServer( IntPtr SteamGameServer )
        {
            m_pSteamGameServer = SteamGameServer;
        }
        IntPtr m_pSteamGameServer;

        internal override IntPtr GetIntPtr() { return m_pSteamGameServer; }

        private void CheckIfUsable()
        {
            if ( m_pSteamGameServer == IntPtr.Zero )
            {
                throw new Exception( "Steam Pointer not configured" );
            }
        }
        internal override bool InitGameServer( uint unIP, char usGamePort, char usQueryPort, uint unFlags, uint nGameAppId, string pchVersionString )
        {
            CheckIfUsable();
            bool result = NativeEntrypoints.SteamAPI_ISteamGameServer_InitGameServer(m_pSteamGameServer,unIP,usGamePort,usQueryPort,unFlags,nGameAppId,pchVersionString);
            return result;
        }
        internal override void SetProduct( string pszProduct )
        {
            CheckIfUsable();
            NativeEntrypoints.SteamAPI_ISteamGameServer_SetProduct( m_pSteamGameServer, pszProduct );
        }
        internal override void SetGameDescription( string pszGameDescription )
        {
            CheckIfUsable();
            NativeEntrypoints.SteamAPI_ISteamGameServer_SetGameDescription( m_pSteamGameServer, pszGameDescription );
        }
        internal override void SetModDir( string pszModDir )
        {
            CheckIfUsable();
            NativeEntrypoints.SteamAPI_ISteamGameServer_SetModDir( m_pSteamGameServer, pszModDir );
        }
        internal override void SetDedicatedServer( bool bDedicated )
        {
            CheckIfUsable();
            NativeEntrypoints.SteamAPI_ISteamGameServer_SetDedicatedServer( m_pSteamGameServer, bDedicated );
        }
        internal override void LogOn( string pszToken )
        {
            CheckIfUsable();
            NativeEntrypoints.SteamAPI_ISteamGameServer_LogOn( m_pSteamGameServer, pszToken );
        }
        internal override void LogOnAnonymous()
        {
            CheckIfUsable();
            NativeEntrypoints.SteamAPI_ISteamGameServer_LogOnAnonymous( m_pSteamGameServer );
        }
        internal override void LogOff()
        {
            CheckIfUsable();
            NativeEntrypoints.SteamAPI_ISteamGameServer_LogOff( m_pSteamGameServer );
        }
        internal override bool BLoggedOn()
        {
            CheckIfUsable();
            bool result = NativeEntrypoints.SteamAPI_ISteamGameServer_BLoggedOn(m_pSteamGameServer);
            return result;
        }
        internal override bool BSecure()
        {
            CheckIfUsable();
            bool result = NativeEntrypoints.SteamAPI_ISteamGameServer_BSecure(m_pSteamGameServer);
            return result;
        }
        internal override ulong GetSteamID()
        {
            CheckIfUsable();
            ulong result = NativeEntrypoints.SteamAPI_ISteamGameServer_GetSteamID(m_pSteamGameServer);
            return result;
        }
        internal override bool WasRestartRequested()
        {
            CheckIfUsable();
            bool result = NativeEntrypoints.SteamAPI_ISteamGameServer_WasRestartRequested(m_pSteamGameServer);
            return result;
        }
        internal override void SetMaxPlayerCount( int cPlayersMax )
        {
            CheckIfUsable();
            NativeEntrypoints.SteamAPI_ISteamGameServer_SetMaxPlayerCount( m_pSteamGameServer, cPlayersMax );
        }
        internal override void SetBotPlayerCount( int cBotplayers )
        {
            CheckIfUsable();
            NativeEntrypoints.SteamAPI_ISteamGameServer_SetBotPlayerCount( m_pSteamGameServer, cBotplayers );
        }
        internal override void SetServerName( string pszServerName )
        {
            CheckIfUsable();
            NativeEntrypoints.SteamAPI_ISteamGameServer_SetServerName( m_pSteamGameServer, pszServerName );
        }
        internal override void SetMapName( string pszMapName )
        {
            CheckIfUsable();
            NativeEntrypoints.SteamAPI_ISteamGameServer_SetMapName( m_pSteamGameServer, pszMapName );
        }
        internal override void SetPasswordProtected( bool bPasswordProtected )
        {
            CheckIfUsable();
            NativeEntrypoints.SteamAPI_ISteamGameServer_SetPasswordProtected( m_pSteamGameServer, bPasswordProtected );
        }
        internal override void SetSpectatorPort( char unSpectatorPort )
        {
            CheckIfUsable();
            NativeEntrypoints.SteamAPI_ISteamGameServer_SetSpectatorPort( m_pSteamGameServer, unSpectatorPort );
        }
        internal override void SetSpectatorServerName( string pszSpectatorServerName )
        {
            CheckIfUsable();
            NativeEntrypoints.SteamAPI_ISteamGameServer_SetSpectatorServerName( m_pSteamGameServer, pszSpectatorServerName );
        }
        internal override void ClearAllKeyValues()
        {
            CheckIfUsable();
            NativeEntrypoints.SteamAPI_ISteamGameServer_ClearAllKeyValues( m_pSteamGameServer );
        }
        internal override void SetKeyValue( string pKey, string pValue )
        {
            CheckIfUsable();
            NativeEntrypoints.SteamAPI_ISteamGameServer_SetKeyValue( m_pSteamGameServer, pKey, pValue );
        }
        internal override void SetGameTags( string pchGameTags )
        {
            CheckIfUsable();
            NativeEntrypoints.SteamAPI_ISteamGameServer_SetGameTags( m_pSteamGameServer, pchGameTags );
        }
        internal override void SetGameData( string pchGameData )
        {
            CheckIfUsable();
            NativeEntrypoints.SteamAPI_ISteamGameServer_SetGameData( m_pSteamGameServer, pchGameData );
        }
        internal override void SetRegion( string pszRegion )
        {
            CheckIfUsable();
            NativeEntrypoints.SteamAPI_ISteamGameServer_SetRegion( m_pSteamGameServer, pszRegion );
        }
        internal override bool SendUserConnectAndAuthenticate( uint unIPClient, IntPtr pvAuthBlob, uint cubAuthBlobSize, ref CSteamID pSteamIDUser )
        {
            CheckIfUsable();
            bool result = NativeEntrypoints.SteamAPI_ISteamGameServer_SendUserConnectAndAuthenticate(m_pSteamGameServer,unIPClient,pvAuthBlob,cubAuthBlobSize,ref pSteamIDUser);
            return result;
        }
        internal override ulong CreateUnauthenticatedUserConnection()
        {
            CheckIfUsable();
            ulong result = NativeEntrypoints.SteamAPI_ISteamGameServer_CreateUnauthenticatedUserConnection(m_pSteamGameServer);
            return result;
        }
        internal override void SendUserDisconnect( ulong steamIDUser )
        {
            CheckIfUsable();
            NativeEntrypoints.SteamAPI_ISteamGameServer_SendUserDisconnect( m_pSteamGameServer, steamIDUser );
        }
        internal override bool BUpdateUserData( ulong steamIDUser, string pchPlayerName, uint uScore )
        {
            CheckIfUsable();
            bool result = NativeEntrypoints.SteamAPI_ISteamGameServer_BUpdateUserData(m_pSteamGameServer,steamIDUser,pchPlayerName,uScore);
            return result;
        }
        internal override uint GetAuthSessionTicket( IntPtr pTicket, int cbMaxTicket, ref uint pcbTicket )
        {
            CheckIfUsable();
            pcbTicket = 0;
            uint result = NativeEntrypoints.SteamAPI_ISteamGameServer_GetAuthSessionTicket(m_pSteamGameServer,pTicket,cbMaxTicket,ref pcbTicket);
            return result;
        }
        internal override uint BeginAuthSession( IntPtr pAuthTicket, int cbAuthTicket, ulong steamID )
        {
            CheckIfUsable();
            uint result = NativeEntrypoints.SteamAPI_ISteamGameServer_BeginAuthSession(m_pSteamGameServer,pAuthTicket,cbAuthTicket,steamID);
            return result;
        }
        internal override void EndAuthSession( ulong steamID )
        {
            CheckIfUsable();
            NativeEntrypoints.SteamAPI_ISteamGameServer_EndAuthSession( m_pSteamGameServer, steamID );
        }
        internal override void CancelAuthTicket( uint hAuthTicket )
        {
            CheckIfUsable();
            NativeEntrypoints.SteamAPI_ISteamGameServer_CancelAuthTicket( m_pSteamGameServer, hAuthTicket );
        }
        internal override uint UserHasLicenseForApp( ulong steamID, uint appID )
        {
            CheckIfUsable();
            uint result = NativeEntrypoints.SteamAPI_ISteamGameServer_UserHasLicenseForApp(m_pSteamGameServer,steamID,appID);
            return result;
        }
        internal override bool RequestUserGroupStatus( ulong steamIDUser, ulong steamIDGroup )
        {
            CheckIfUsable();
            bool result = NativeEntrypoints.SteamAPI_ISteamGameServer_RequestUserGroupStatus(m_pSteamGameServer,steamIDUser,steamIDGroup);
            return result;
        }
        internal override void GetGameplayStats()
        {
            CheckIfUsable();
            NativeEntrypoints.SteamAPI_ISteamGameServer_GetGameplayStats( m_pSteamGameServer );
        }
        internal override ulong GetServerReputation()
        {
            CheckIfUsable();
            ulong result = NativeEntrypoints.SteamAPI_ISteamGameServer_GetServerReputation(m_pSteamGameServer);
            return result;
        }
        internal override uint GetPublicIP()
        {
            CheckIfUsable();
            uint result = NativeEntrypoints.SteamAPI_ISteamGameServer_GetPublicIP(m_pSteamGameServer);
            return result;
        }
        internal override bool HandleIncomingPacket( IntPtr pData, int cbData, uint srcIP, char srcPort )
        {
            CheckIfUsable();
            bool result = NativeEntrypoints.SteamAPI_ISteamGameServer_HandleIncomingPacket(m_pSteamGameServer,pData,cbData,srcIP,srcPort);
            return result;
        }
        internal override int GetNextOutgoingPacket( IntPtr pOut, int cbMaxOut, ref uint pNetAdr, ref char pPort )
        {
            CheckIfUsable();
            pNetAdr = 0;
            pPort = (char)0;
            int result = NativeEntrypoints.SteamAPI_ISteamGameServer_GetNextOutgoingPacket(m_pSteamGameServer,pOut,cbMaxOut,ref pNetAdr,ref pPort);
            return result;
        }
        internal override void EnableHeartbeats( bool bActive )
        {
            CheckIfUsable();
            NativeEntrypoints.SteamAPI_ISteamGameServer_EnableHeartbeats( m_pSteamGameServer, bActive );
        }
        internal override void SetHeartbeatInterval( int iHeartbeatInterval )
        {
            CheckIfUsable();
            NativeEntrypoints.SteamAPI_ISteamGameServer_SetHeartbeatInterval( m_pSteamGameServer, iHeartbeatInterval );
        }
        internal override void ForceHeartbeat()
        {
            CheckIfUsable();
            NativeEntrypoints.SteamAPI_ISteamGameServer_ForceHeartbeat( m_pSteamGameServer );
        }
        internal override ulong AssociateWithClan( ulong steamIDClan )
        {
            CheckIfUsable();
            ulong result = NativeEntrypoints.SteamAPI_ISteamGameServer_AssociateWithClan(m_pSteamGameServer,steamIDClan);
            return result;
        }
        internal override ulong ComputeNewPlayerCompatibility( ulong steamIDNewPlayer )
        {
            CheckIfUsable();
            ulong result = NativeEntrypoints.SteamAPI_ISteamGameServer_ComputeNewPlayerCompatibility(m_pSteamGameServer,steamIDNewPlayer);
            return result;
        }
    }


    internal class CSteamGameServerStats : ISteamGameServerStats
    {
        public CSteamGameServerStats( IntPtr SteamGameServerStats )
        {
            m_pSteamGameServerStats = SteamGameServerStats;
        }
        IntPtr m_pSteamGameServerStats;

        internal override IntPtr GetIntPtr() { return m_pSteamGameServerStats; }

        private void CheckIfUsable()
        {
            if ( m_pSteamGameServerStats == IntPtr.Zero )
            {
                throw new Exception( "Steam Pointer not configured" );
            }
        }
        internal override ulong RequestUserStats( ulong steamIDUser )
        {
            CheckIfUsable();
            ulong result = NativeEntrypoints.SteamAPI_ISteamGameServerStats_RequestUserStats(m_pSteamGameServerStats,steamIDUser);
            return result;
        }
        internal override bool GetUserStat( ulong steamIDUser, string pchName, ref int pData )
        {
            CheckIfUsable();
            pData = 0;
            bool result = NativeEntrypoints.SteamAPI_ISteamGameServerStats_GetUserStat(m_pSteamGameServerStats,steamIDUser,pchName,ref pData);
            return result;
        }
        internal override bool GetUserStat0( ulong steamIDUser, string pchName, ref float pData )
        {
            CheckIfUsable();
            pData = 0;
            bool result = NativeEntrypoints.SteamAPI_ISteamGameServerStats_GetUserStat0(m_pSteamGameServerStats,steamIDUser,pchName,ref pData);
            return result;
        }
        internal override bool GetUserAchievement( ulong steamIDUser, string pchName, ref bool pbAchieved )
        {
            CheckIfUsable();
            pbAchieved = false;
            bool result = NativeEntrypoints.SteamAPI_ISteamGameServerStats_GetUserAchievement(m_pSteamGameServerStats,steamIDUser,pchName,ref pbAchieved);
            return result;
        }
        internal override bool SetUserStat( ulong steamIDUser, string pchName, int nData )
        {
            CheckIfUsable();
            bool result = NativeEntrypoints.SteamAPI_ISteamGameServerStats_SetUserStat(m_pSteamGameServerStats,steamIDUser,pchName,nData);
            return result;
        }
        internal override bool SetUserStat0( ulong steamIDUser, string pchName, float fData )
        {
            CheckIfUsable();
            bool result = NativeEntrypoints.SteamAPI_ISteamGameServerStats_SetUserStat0(m_pSteamGameServerStats,steamIDUser,pchName,fData);
            return result;
        }
        internal override bool UpdateUserAvgRateStat( ulong steamIDUser, string pchName, float flCountThisSession, double dSessionLength )
        {
            CheckIfUsable();
            bool result = NativeEntrypoints.SteamAPI_ISteamGameServerStats_UpdateUserAvgRateStat(m_pSteamGameServerStats,steamIDUser,pchName,flCountThisSession,dSessionLength);
            return result;
        }
        internal override bool SetUserAchievement( ulong steamIDUser, string pchName )
        {
            CheckIfUsable();
            bool result = NativeEntrypoints.SteamAPI_ISteamGameServerStats_SetUserAchievement(m_pSteamGameServerStats,steamIDUser,pchName);
            return result;
        }
        internal override bool ClearUserAchievement( ulong steamIDUser, string pchName )
        {
            CheckIfUsable();
            bool result = NativeEntrypoints.SteamAPI_ISteamGameServerStats_ClearUserAchievement(m_pSteamGameServerStats,steamIDUser,pchName);
            return result;
        }
        internal override ulong StoreUserStats( ulong steamIDUser )
        {
            CheckIfUsable();
            ulong result = NativeEntrypoints.SteamAPI_ISteamGameServerStats_StoreUserStats(m_pSteamGameServerStats,steamIDUser);
            return result;
        }
    }


    internal class CUserStatsReceived_t_Callback
    {
        public CUserStatsReceived_t_Callback() { }
        ~CUserStatsReceived_t_Callback()
        {
            if ( m_Handle != 0 )
            {
                Valve.Interop.NativeEntrypoints.CUserStatsReceived_t_RemoveCallback( m_Handle );
            }
        }
        ulong m_Handle = 0;
        public void Set( Valve.Interop.NativeEntrypoints.SteamAPI_UserStatsReceived_t_Callback func )
        {
            if ( m_Handle != 0 )
            {
                Valve.Interop.NativeEntrypoints.CUserStatsReceived_t_RemoveCallback( m_Handle );
            }
            m_Handle = Valve.Interop.NativeEntrypoints.CUserStatsReceived_t_SetCallback( func );
        }
    }
    internal class CRemoteStorageFileReadAsyncComplete_t_CallResult
    {
        public CRemoteStorageFileReadAsyncComplete_t_CallResult() { }
        ~CRemoteStorageFileReadAsyncComplete_t_CallResult()
        {
            if ( m_Handle != 0 )
            {
                Valve.Interop.NativeEntrypoints.CRemoteStorageFileReadAsyncComplete_t_RemoveCallResult( m_Handle );
            }
        }
        ulong m_Handle = 0;
        public void Set( ulong hAPICall, Valve.Interop.NativeEntrypoints.SteamAPI_RemoteStorageFileReadAsyncComplete_t_CallResult func )
        {
            if ( m_Handle != 0 )
            {
                Valve.Interop.NativeEntrypoints.CRemoteStorageFileReadAsyncComplete_t_RemoveCallResult( m_Handle );
            }
            m_Handle = Valve.Interop.NativeEntrypoints.CRemoteStorageFileReadAsyncComplete_t_SetCallResult( hAPICall, func );
        }
    }
    internal class CRemoteStorageGetPublishedItemVoteDetailsResult_t_CallResult
    {
        public CRemoteStorageGetPublishedItemVoteDetailsResult_t_CallResult() { }
        ~CRemoteStorageGetPublishedItemVoteDetailsResult_t_CallResult()
        {
            if ( m_Handle != 0 )
            {
                Valve.Interop.NativeEntrypoints.CRemoteStorageGetPublishedItemVoteDetailsResult_t_RemoveCallResult( m_Handle );
            }
        }
        ulong m_Handle = 0;
        public void Set( ulong hAPICall, Valve.Interop.NativeEntrypoints.SteamAPI_RemoteStorageGetPublishedItemVoteDetailsResult_t_CallResult func )
        {
            if ( m_Handle != 0 )
            {
                Valve.Interop.NativeEntrypoints.CRemoteStorageGetPublishedItemVoteDetailsResult_t_RemoveCallResult( m_Handle );
            }
            m_Handle = Valve.Interop.NativeEntrypoints.CRemoteStorageGetPublishedItemVoteDetailsResult_t_SetCallResult( hAPICall, func );
        }
    }
    internal class CGSStatsStored_t_CallResult
    {
        public CGSStatsStored_t_CallResult() { }
        ~CGSStatsStored_t_CallResult()
        {
            if ( m_Handle != 0 )
            {
                Valve.Interop.NativeEntrypoints.CGSStatsStored_t_RemoveCallResult( m_Handle );
            }
        }
        ulong m_Handle = 0;
        public void Set( ulong hAPICall, Valve.Interop.NativeEntrypoints.SteamAPI_GSStatsStored_t_CallResult func )
        {
            if ( m_Handle != 0 )
            {
                Valve.Interop.NativeEntrypoints.CGSStatsStored_t_RemoveCallResult( m_Handle );
            }
            m_Handle = Valve.Interop.NativeEntrypoints.CGSStatsStored_t_SetCallResult( hAPICall, func );
        }
    }
    internal class CHTML_BrowserReady_t_CallResult
    {
        public CHTML_BrowserReady_t_CallResult() { }
        ~CHTML_BrowserReady_t_CallResult()
        {
            if ( m_Handle != 0 )
            {
                Valve.Interop.NativeEntrypoints.CHTML_BrowserReady_t_RemoveCallResult( m_Handle );
            }
        }
        ulong m_Handle = 0;
        public void Set( ulong hAPICall, Valve.Interop.NativeEntrypoints.SteamAPI_HTML_BrowserReady_t_CallResult func )
        {
            if ( m_Handle != 0 )
            {
                Valve.Interop.NativeEntrypoints.CHTML_BrowserReady_t_RemoveCallResult( m_Handle );
            }
            m_Handle = Valve.Interop.NativeEntrypoints.CHTML_BrowserReady_t_SetCallResult( hAPICall, func );
        }
    }
    internal class CFriendsGetFollowerCount_t_CallResult
    {
        public CFriendsGetFollowerCount_t_CallResult() { }
        ~CFriendsGetFollowerCount_t_CallResult()
        {
            if ( m_Handle != 0 )
            {
                Valve.Interop.NativeEntrypoints.CFriendsGetFollowerCount_t_RemoveCallResult( m_Handle );
            }
        }
        ulong m_Handle = 0;
        public void Set( ulong hAPICall, Valve.Interop.NativeEntrypoints.SteamAPI_FriendsGetFollowerCount_t_CallResult func )
        {
            if ( m_Handle != 0 )
            {
                Valve.Interop.NativeEntrypoints.CFriendsGetFollowerCount_t_RemoveCallResult( m_Handle );
            }
            m_Handle = Valve.Interop.NativeEntrypoints.CFriendsGetFollowerCount_t_SetCallResult( hAPICall, func );
        }
    }
    internal class CFriendsIsFollowing_t_CallResult
    {
        public CFriendsIsFollowing_t_CallResult() { }
        ~CFriendsIsFollowing_t_CallResult()
        {
            if ( m_Handle != 0 )
            {
                Valve.Interop.NativeEntrypoints.CFriendsIsFollowing_t_RemoveCallResult( m_Handle );
            }
        }
        ulong m_Handle = 0;
        public void Set( ulong hAPICall, Valve.Interop.NativeEntrypoints.SteamAPI_FriendsIsFollowing_t_CallResult func )
        {
            if ( m_Handle != 0 )
            {
                Valve.Interop.NativeEntrypoints.CFriendsIsFollowing_t_RemoveCallResult( m_Handle );
            }
            m_Handle = Valve.Interop.NativeEntrypoints.CFriendsIsFollowing_t_SetCallResult( hAPICall, func );
        }
    }
    internal class CLobbyMatchList_t_CallResult
    {
        public CLobbyMatchList_t_CallResult() { }
        ~CLobbyMatchList_t_CallResult()
        {
            if ( m_Handle != 0 )
            {
                Valve.Interop.NativeEntrypoints.CLobbyMatchList_t_RemoveCallResult( m_Handle );
            }
        }
        ulong m_Handle = 0;
        public void Set( ulong hAPICall, Valve.Interop.NativeEntrypoints.SteamAPI_LobbyMatchList_t_CallResult func )
        {
            if ( m_Handle != 0 )
            {
                Valve.Interop.NativeEntrypoints.CLobbyMatchList_t_RemoveCallResult( m_Handle );
            }
            m_Handle = Valve.Interop.NativeEntrypoints.CLobbyMatchList_t_SetCallResult( hAPICall, func );
        }
    }
    internal class CRemoteStorageUpdatePublishedFileResult_t_CallResult
    {
        public CRemoteStorageUpdatePublishedFileResult_t_CallResult() { }
        ~CRemoteStorageUpdatePublishedFileResult_t_CallResult()
        {
            if ( m_Handle != 0 )
            {
                Valve.Interop.NativeEntrypoints.CRemoteStorageUpdatePublishedFileResult_t_RemoveCallResult( m_Handle );
            }
        }
        ulong m_Handle = 0;
        public void Set( ulong hAPICall, Valve.Interop.NativeEntrypoints.SteamAPI_RemoteStorageUpdatePublishedFileResult_t_CallResult func )
        {
            if ( m_Handle != 0 )
            {
                Valve.Interop.NativeEntrypoints.CRemoteStorageUpdatePublishedFileResult_t_RemoveCallResult( m_Handle );
            }
            m_Handle = Valve.Interop.NativeEntrypoints.CRemoteStorageUpdatePublishedFileResult_t_SetCallResult( hAPICall, func );
        }
    }
    internal class CStoreAuthURLResponse_t_CallResult
    {
        public CStoreAuthURLResponse_t_CallResult() { }
        ~CStoreAuthURLResponse_t_CallResult()
        {
            if ( m_Handle != 0 )
            {
                Valve.Interop.NativeEntrypoints.CStoreAuthURLResponse_t_RemoveCallResult( m_Handle );
            }
        }
        ulong m_Handle = 0;
        public void Set( ulong hAPICall, Valve.Interop.NativeEntrypoints.SteamAPI_StoreAuthURLResponse_t_CallResult func )
        {
            if ( m_Handle != 0 )
            {
                Valve.Interop.NativeEntrypoints.CStoreAuthURLResponse_t_RemoveCallResult( m_Handle );
            }
            m_Handle = Valve.Interop.NativeEntrypoints.CStoreAuthURLResponse_t_SetCallResult( hAPICall, func );
        }
    }
    internal class CLobbyCreated_t_CallResult
    {
        public CLobbyCreated_t_CallResult() { }
        ~CLobbyCreated_t_CallResult()
        {
            if ( m_Handle != 0 )
            {
                Valve.Interop.NativeEntrypoints.CLobbyCreated_t_RemoveCallResult( m_Handle );
            }
        }
        ulong m_Handle = 0;
        public void Set( ulong hAPICall, Valve.Interop.NativeEntrypoints.SteamAPI_LobbyCreated_t_CallResult func )
        {
            if ( m_Handle != 0 )
            {
                Valve.Interop.NativeEntrypoints.CLobbyCreated_t_RemoveCallResult( m_Handle );
            }
            m_Handle = Valve.Interop.NativeEntrypoints.CLobbyCreated_t_SetCallResult( hAPICall, func );
        }
    }
    internal class CRemoteStorageFileWriteAsyncComplete_t_CallResult
    {
        public CRemoteStorageFileWriteAsyncComplete_t_CallResult() { }
        ~CRemoteStorageFileWriteAsyncComplete_t_CallResult()
        {
            if ( m_Handle != 0 )
            {
                Valve.Interop.NativeEntrypoints.CRemoteStorageFileWriteAsyncComplete_t_RemoveCallResult( m_Handle );
            }
        }
        ulong m_Handle = 0;
        public void Set( ulong hAPICall, Valve.Interop.NativeEntrypoints.SteamAPI_RemoteStorageFileWriteAsyncComplete_t_CallResult func )
        {
            if ( m_Handle != 0 )
            {
                Valve.Interop.NativeEntrypoints.CRemoteStorageFileWriteAsyncComplete_t_RemoveCallResult( m_Handle );
            }
            m_Handle = Valve.Interop.NativeEntrypoints.CRemoteStorageFileWriteAsyncComplete_t_SetCallResult( hAPICall, func );
        }
    }
    internal class CRemoteStorageDeletePublishedFileResult_t_CallResult
    {
        public CRemoteStorageDeletePublishedFileResult_t_CallResult() { }
        ~CRemoteStorageDeletePublishedFileResult_t_CallResult()
        {
            if ( m_Handle != 0 )
            {
                Valve.Interop.NativeEntrypoints.CRemoteStorageDeletePublishedFileResult_t_RemoveCallResult( m_Handle );
            }
        }
        ulong m_Handle = 0;
        public void Set( ulong hAPICall, Valve.Interop.NativeEntrypoints.SteamAPI_RemoteStorageDeletePublishedFileResult_t_CallResult func )
        {
            if ( m_Handle != 0 )
            {
                Valve.Interop.NativeEntrypoints.CRemoteStorageDeletePublishedFileResult_t_RemoveCallResult( m_Handle );
            }
            m_Handle = Valve.Interop.NativeEntrypoints.CRemoteStorageDeletePublishedFileResult_t_SetCallResult( hAPICall, func );
        }
    }
    internal class CRemoteStorageGetPublishedFileDetailsResult_t_CallResult
    {
        public CRemoteStorageGetPublishedFileDetailsResult_t_CallResult() { }
        ~CRemoteStorageGetPublishedFileDetailsResult_t_CallResult()
        {
            if ( m_Handle != 0 )
            {
                Valve.Interop.NativeEntrypoints.CRemoteStorageGetPublishedFileDetailsResult_t_RemoveCallResult( m_Handle );
            }
        }
        ulong m_Handle = 0;
        public void Set( ulong hAPICall, Valve.Interop.NativeEntrypoints.SteamAPI_RemoteStorageGetPublishedFileDetailsResult_t_CallResult func )
        {
            if ( m_Handle != 0 )
            {
                Valve.Interop.NativeEntrypoints.CRemoteStorageGetPublishedFileDetailsResult_t_RemoveCallResult( m_Handle );
            }
            m_Handle = Valve.Interop.NativeEntrypoints.CRemoteStorageGetPublishedFileDetailsResult_t_SetCallResult( hAPICall, func );
        }
    }
    internal class CRemoteStorageDownloadUGCResult_t_CallResult
    {
        public CRemoteStorageDownloadUGCResult_t_CallResult() { }
        ~CRemoteStorageDownloadUGCResult_t_CallResult()
        {
            if ( m_Handle != 0 )
            {
                Valve.Interop.NativeEntrypoints.CRemoteStorageDownloadUGCResult_t_RemoveCallResult( m_Handle );
            }
        }
        ulong m_Handle = 0;
        public void Set( ulong hAPICall, Valve.Interop.NativeEntrypoints.SteamAPI_RemoteStorageDownloadUGCResult_t_CallResult func )
        {
            if ( m_Handle != 0 )
            {
                Valve.Interop.NativeEntrypoints.CRemoteStorageDownloadUGCResult_t_RemoveCallResult( m_Handle );
            }
            m_Handle = Valve.Interop.NativeEntrypoints.CRemoteStorageDownloadUGCResult_t_SetCallResult( hAPICall, func );
        }
    }
    internal class CSteamUGCQueryCompleted_t_CallResult
    {
        public CSteamUGCQueryCompleted_t_CallResult() { }
        ~CSteamUGCQueryCompleted_t_CallResult()
        {
            if ( m_Handle != 0 )
            {
                Valve.Interop.NativeEntrypoints.CSteamUGCQueryCompleted_t_RemoveCallResult( m_Handle );
            }
        }
        ulong m_Handle = 0;
        public void Set( ulong hAPICall, Valve.Interop.NativeEntrypoints.SteamAPI_SteamUGCQueryCompleted_t_CallResult func )
        {
            if ( m_Handle != 0 )
            {
                Valve.Interop.NativeEntrypoints.CSteamUGCQueryCompleted_t_RemoveCallResult( m_Handle );
            }
            m_Handle = Valve.Interop.NativeEntrypoints.CSteamUGCQueryCompleted_t_SetCallResult( hAPICall, func );
        }
    }
    internal class CRemoteStorageFileShareResult_t_CallResult
    {
        public CRemoteStorageFileShareResult_t_CallResult() { }
        ~CRemoteStorageFileShareResult_t_CallResult()
        {
            if ( m_Handle != 0 )
            {
                Valve.Interop.NativeEntrypoints.CRemoteStorageFileShareResult_t_RemoveCallResult( m_Handle );
            }
        }
        ulong m_Handle = 0;
        public void Set( ulong hAPICall, Valve.Interop.NativeEntrypoints.SteamAPI_RemoteStorageFileShareResult_t_CallResult func )
        {
            if ( m_Handle != 0 )
            {
                Valve.Interop.NativeEntrypoints.CRemoteStorageFileShareResult_t_RemoveCallResult( m_Handle );
            }
            m_Handle = Valve.Interop.NativeEntrypoints.CRemoteStorageFileShareResult_t_SetCallResult( hAPICall, func );
        }
    }
    internal class CLobbyEnter_t_CallResult
    {
        public CLobbyEnter_t_CallResult() { }
        ~CLobbyEnter_t_CallResult()
        {
            if ( m_Handle != 0 )
            {
                Valve.Interop.NativeEntrypoints.CLobbyEnter_t_RemoveCallResult( m_Handle );
            }
        }
        ulong m_Handle = 0;
        public void Set( ulong hAPICall, Valve.Interop.NativeEntrypoints.SteamAPI_LobbyEnter_t_CallResult func )
        {
            if ( m_Handle != 0 )
            {
                Valve.Interop.NativeEntrypoints.CLobbyEnter_t_RemoveCallResult( m_Handle );
            }
            m_Handle = Valve.Interop.NativeEntrypoints.CLobbyEnter_t_SetCallResult( hAPICall, func );
        }
    }
    internal class CSubmitItemUpdateResult_t_CallResult
    {
        public CSubmitItemUpdateResult_t_CallResult() { }
        ~CSubmitItemUpdateResult_t_CallResult()
        {
            if ( m_Handle != 0 )
            {
                Valve.Interop.NativeEntrypoints.CSubmitItemUpdateResult_t_RemoveCallResult( m_Handle );
            }
        }
        ulong m_Handle = 0;
        public void Set( ulong hAPICall, Valve.Interop.NativeEntrypoints.SteamAPI_SubmitItemUpdateResult_t_CallResult func )
        {
            if ( m_Handle != 0 )
            {
                Valve.Interop.NativeEntrypoints.CSubmitItemUpdateResult_t_RemoveCallResult( m_Handle );
            }
            m_Handle = Valve.Interop.NativeEntrypoints.CSubmitItemUpdateResult_t_SetCallResult( hAPICall, func );
        }
    }
    internal class CNumberOfCurrentPlayers_t_CallResult
    {
        public CNumberOfCurrentPlayers_t_CallResult() { }
        ~CNumberOfCurrentPlayers_t_CallResult()
        {
            if ( m_Handle != 0 )
            {
                Valve.Interop.NativeEntrypoints.CNumberOfCurrentPlayers_t_RemoveCallResult( m_Handle );
            }
        }
        ulong m_Handle = 0;
        public void Set( ulong hAPICall, Valve.Interop.NativeEntrypoints.SteamAPI_NumberOfCurrentPlayers_t_CallResult func )
        {
            if ( m_Handle != 0 )
            {
                Valve.Interop.NativeEntrypoints.CNumberOfCurrentPlayers_t_RemoveCallResult( m_Handle );
            }
            m_Handle = Valve.Interop.NativeEntrypoints.CNumberOfCurrentPlayers_t_SetCallResult( hAPICall, func );
        }
    }
    internal class CGSStatsReceived_t_CallResult
    {
        public CGSStatsReceived_t_CallResult() { }
        ~CGSStatsReceived_t_CallResult()
        {
            if ( m_Handle != 0 )
            {
                Valve.Interop.NativeEntrypoints.CGSStatsReceived_t_RemoveCallResult( m_Handle );
            }
        }
        ulong m_Handle = 0;
        public void Set( ulong hAPICall, Valve.Interop.NativeEntrypoints.SteamAPI_GSStatsReceived_t_CallResult func )
        {
            if ( m_Handle != 0 )
            {
                Valve.Interop.NativeEntrypoints.CGSStatsReceived_t_RemoveCallResult( m_Handle );
            }
            m_Handle = Valve.Interop.NativeEntrypoints.CGSStatsReceived_t_SetCallResult( hAPICall, func );
        }
    }
    internal class CLeaderboardScoresDownloaded_t_CallResult
    {
        public CLeaderboardScoresDownloaded_t_CallResult() { }
        ~CLeaderboardScoresDownloaded_t_CallResult()
        {
            if ( m_Handle != 0 )
            {
                Valve.Interop.NativeEntrypoints.CLeaderboardScoresDownloaded_t_RemoveCallResult( m_Handle );
            }
        }
        ulong m_Handle = 0;
        public void Set( ulong hAPICall, Valve.Interop.NativeEntrypoints.SteamAPI_LeaderboardScoresDownloaded_t_CallResult func )
        {
            if ( m_Handle != 0 )
            {
                Valve.Interop.NativeEntrypoints.CLeaderboardScoresDownloaded_t_RemoveCallResult( m_Handle );
            }
            m_Handle = Valve.Interop.NativeEntrypoints.CLeaderboardScoresDownloaded_t_SetCallResult( hAPICall, func );
        }
    }
    internal class CRemoteStorageUpdateUserPublishedItemVoteResult_t_CallResult
    {
        public CRemoteStorageUpdateUserPublishedItemVoteResult_t_CallResult() { }
        ~CRemoteStorageUpdateUserPublishedItemVoteResult_t_CallResult()
        {
            if ( m_Handle != 0 )
            {
                Valve.Interop.NativeEntrypoints.CRemoteStorageUpdateUserPublishedItemVoteResult_t_RemoveCallResult( m_Handle );
            }
        }
        ulong m_Handle = 0;
        public void Set( ulong hAPICall, Valve.Interop.NativeEntrypoints.SteamAPI_RemoteStorageUpdateUserPublishedItemVoteResult_t_CallResult func )
        {
            if ( m_Handle != 0 )
            {
                Valve.Interop.NativeEntrypoints.CRemoteStorageUpdateUserPublishedItemVoteResult_t_RemoveCallResult( m_Handle );
            }
            m_Handle = Valve.Interop.NativeEntrypoints.CRemoteStorageUpdateUserPublishedItemVoteResult_t_SetCallResult( hAPICall, func );
        }
    }
    internal class CRemoteStorageEnumerateUserSubscribedFilesResult_t_CallResult
    {
        public CRemoteStorageEnumerateUserSubscribedFilesResult_t_CallResult() { }
        ~CRemoteStorageEnumerateUserSubscribedFilesResult_t_CallResult()
        {
            if ( m_Handle != 0 )
            {
                Valve.Interop.NativeEntrypoints.CRemoteStorageEnumerateUserSubscribedFilesResult_t_RemoveCallResult( m_Handle );
            }
        }
        ulong m_Handle = 0;
        public void Set( ulong hAPICall, Valve.Interop.NativeEntrypoints.SteamAPI_RemoteStorageEnumerateUserSubscribedFilesResult_t_CallResult func )
        {
            if ( m_Handle != 0 )
            {
                Valve.Interop.NativeEntrypoints.CRemoteStorageEnumerateUserSubscribedFilesResult_t_RemoveCallResult( m_Handle );
            }
            m_Handle = Valve.Interop.NativeEntrypoints.CRemoteStorageEnumerateUserSubscribedFilesResult_t_SetCallResult( hAPICall, func );
        }
    }
    internal class CCreateItemResult_t_CallResult
    {
        public CCreateItemResult_t_CallResult() { }
        ~CCreateItemResult_t_CallResult()
        {
            if ( m_Handle != 0 )
            {
                Valve.Interop.NativeEntrypoints.CCreateItemResult_t_RemoveCallResult( m_Handle );
            }
        }
        ulong m_Handle = 0;
        public void Set( ulong hAPICall, Valve.Interop.NativeEntrypoints.SteamAPI_CreateItemResult_t_CallResult func )
        {
            if ( m_Handle != 0 )
            {
                Valve.Interop.NativeEntrypoints.CCreateItemResult_t_RemoveCallResult( m_Handle );
            }
            m_Handle = Valve.Interop.NativeEntrypoints.CCreateItemResult_t_SetCallResult( hAPICall, func );
        }
    }
    internal class CSetUserItemVoteResult_t_CallResult
    {
        public CSetUserItemVoteResult_t_CallResult() { }
        ~CSetUserItemVoteResult_t_CallResult()
        {
            if ( m_Handle != 0 )
            {
                Valve.Interop.NativeEntrypoints.CSetUserItemVoteResult_t_RemoveCallResult( m_Handle );
            }
        }
        ulong m_Handle = 0;
        public void Set( ulong hAPICall, Valve.Interop.NativeEntrypoints.SteamAPI_SetUserItemVoteResult_t_CallResult func )
        {
            if ( m_Handle != 0 )
            {
                Valve.Interop.NativeEntrypoints.CSetUserItemVoteResult_t_RemoveCallResult( m_Handle );
            }
            m_Handle = Valve.Interop.NativeEntrypoints.CSetUserItemVoteResult_t_SetCallResult( hAPICall, func );
        }
    }
    internal class CComputeNewPlayerCompatibilityResult_t_CallResult
    {
        public CComputeNewPlayerCompatibilityResult_t_CallResult() { }
        ~CComputeNewPlayerCompatibilityResult_t_CallResult()
        {
            if ( m_Handle != 0 )
            {
                Valve.Interop.NativeEntrypoints.CComputeNewPlayerCompatibilityResult_t_RemoveCallResult( m_Handle );
            }
        }
        ulong m_Handle = 0;
        public void Set( ulong hAPICall, Valve.Interop.NativeEntrypoints.SteamAPI_ComputeNewPlayerCompatibilityResult_t_CallResult func )
        {
            if ( m_Handle != 0 )
            {
                Valve.Interop.NativeEntrypoints.CComputeNewPlayerCompatibilityResult_t_RemoveCallResult( m_Handle );
            }
            m_Handle = Valve.Interop.NativeEntrypoints.CComputeNewPlayerCompatibilityResult_t_SetCallResult( hAPICall, func );
        }
    }
    internal class CLeaderboardScoreUploaded_t_CallResult
    {
        public CLeaderboardScoreUploaded_t_CallResult() { }
        ~CLeaderboardScoreUploaded_t_CallResult()
        {
            if ( m_Handle != 0 )
            {
                Valve.Interop.NativeEntrypoints.CLeaderboardScoreUploaded_t_RemoveCallResult( m_Handle );
            }
        }
        ulong m_Handle = 0;
        public void Set( ulong hAPICall, Valve.Interop.NativeEntrypoints.SteamAPI_LeaderboardScoreUploaded_t_CallResult func )
        {
            if ( m_Handle != 0 )
            {
                Valve.Interop.NativeEntrypoints.CLeaderboardScoreUploaded_t_RemoveCallResult( m_Handle );
            }
            m_Handle = Valve.Interop.NativeEntrypoints.CLeaderboardScoreUploaded_t_SetCallResult( hAPICall, func );
        }
    }
    internal class CGlobalAchievementPercentagesReady_t_CallResult
    {
        public CGlobalAchievementPercentagesReady_t_CallResult() { }
        ~CGlobalAchievementPercentagesReady_t_CallResult()
        {
            if ( m_Handle != 0 )
            {
                Valve.Interop.NativeEntrypoints.CGlobalAchievementPercentagesReady_t_RemoveCallResult( m_Handle );
            }
        }
        ulong m_Handle = 0;
        public void Set( ulong hAPICall, Valve.Interop.NativeEntrypoints.SteamAPI_GlobalAchievementPercentagesReady_t_CallResult func )
        {
            if ( m_Handle != 0 )
            {
                Valve.Interop.NativeEntrypoints.CGlobalAchievementPercentagesReady_t_RemoveCallResult( m_Handle );
            }
            m_Handle = Valve.Interop.NativeEntrypoints.CGlobalAchievementPercentagesReady_t_SetCallResult( hAPICall, func );
        }
    }
    internal class CGlobalStatsReceived_t_CallResult
    {
        public CGlobalStatsReceived_t_CallResult() { }
        ~CGlobalStatsReceived_t_CallResult()
        {
            if ( m_Handle != 0 )
            {
                Valve.Interop.NativeEntrypoints.CGlobalStatsReceived_t_RemoveCallResult( m_Handle );
            }
        }
        ulong m_Handle = 0;
        public void Set( ulong hAPICall, Valve.Interop.NativeEntrypoints.SteamAPI_GlobalStatsReceived_t_CallResult func )
        {
            if ( m_Handle != 0 )
            {
                Valve.Interop.NativeEntrypoints.CGlobalStatsReceived_t_RemoveCallResult( m_Handle );
            }
            m_Handle = Valve.Interop.NativeEntrypoints.CGlobalStatsReceived_t_SetCallResult( hAPICall, func );
        }
    }
    internal class CRemoteStorageEnumeratePublishedFilesByUserActionResult_t_CallResult
    {
        public CRemoteStorageEnumeratePublishedFilesByUserActionResult_t_CallResult() { }
        ~CRemoteStorageEnumeratePublishedFilesByUserActionResult_t_CallResult()
        {
            if ( m_Handle != 0 )
            {
                Valve.Interop.NativeEntrypoints.CRemoteStorageEnumeratePublishedFilesByUserActionResult_t_RemoveCallResult( m_Handle );
            }
        }
        ulong m_Handle = 0;
        public void Set( ulong hAPICall, Valve.Interop.NativeEntrypoints.SteamAPI_RemoteStorageEnumeratePublishedFilesByUserActionResult_t_CallResult func )
        {
            if ( m_Handle != 0 )
            {
                Valve.Interop.NativeEntrypoints.CRemoteStorageEnumeratePublishedFilesByUserActionResult_t_RemoveCallResult( m_Handle );
            }
            m_Handle = Valve.Interop.NativeEntrypoints.CRemoteStorageEnumeratePublishedFilesByUserActionResult_t_SetCallResult( hAPICall, func );
        }
    }
    internal class CClanOfficerListResponse_t_CallResult
    {
        public CClanOfficerListResponse_t_CallResult() { }
        ~CClanOfficerListResponse_t_CallResult()
        {
            if ( m_Handle != 0 )
            {
                Valve.Interop.NativeEntrypoints.CClanOfficerListResponse_t_RemoveCallResult( m_Handle );
            }
        }
        ulong m_Handle = 0;
        public void Set( ulong hAPICall, Valve.Interop.NativeEntrypoints.SteamAPI_ClanOfficerListResponse_t_CallResult func )
        {
            if ( m_Handle != 0 )
            {
                Valve.Interop.NativeEntrypoints.CClanOfficerListResponse_t_RemoveCallResult( m_Handle );
            }
            m_Handle = Valve.Interop.NativeEntrypoints.CClanOfficerListResponse_t_SetCallResult( hAPICall, func );
        }
    }
    internal class CRemoteStoragePublishFileProgress_t_CallResult
    {
        public CRemoteStoragePublishFileProgress_t_CallResult() { }
        ~CRemoteStoragePublishFileProgress_t_CallResult()
        {
            if ( m_Handle != 0 )
            {
                Valve.Interop.NativeEntrypoints.CRemoteStoragePublishFileProgress_t_RemoveCallResult( m_Handle );
            }
        }
        ulong m_Handle = 0;
        public void Set( ulong hAPICall, Valve.Interop.NativeEntrypoints.SteamAPI_RemoteStoragePublishFileProgress_t_CallResult func )
        {
            if ( m_Handle != 0 )
            {
                Valve.Interop.NativeEntrypoints.CRemoteStoragePublishFileProgress_t_RemoveCallResult( m_Handle );
            }
            m_Handle = Valve.Interop.NativeEntrypoints.CRemoteStoragePublishFileProgress_t_SetCallResult( hAPICall, func );
        }
    }
    internal class CRemoteStorageEnumerateWorkshopFilesResult_t_CallResult
    {
        public CRemoteStorageEnumerateWorkshopFilesResult_t_CallResult() { }
        ~CRemoteStorageEnumerateWorkshopFilesResult_t_CallResult()
        {
            if ( m_Handle != 0 )
            {
                Valve.Interop.NativeEntrypoints.CRemoteStorageEnumerateWorkshopFilesResult_t_RemoveCallResult( m_Handle );
            }
        }
        ulong m_Handle = 0;
        public void Set( ulong hAPICall, Valve.Interop.NativeEntrypoints.SteamAPI_RemoteStorageEnumerateWorkshopFilesResult_t_CallResult func )
        {
            if ( m_Handle != 0 )
            {
                Valve.Interop.NativeEntrypoints.CRemoteStorageEnumerateWorkshopFilesResult_t_RemoveCallResult( m_Handle );
            }
            m_Handle = Valve.Interop.NativeEntrypoints.CRemoteStorageEnumerateWorkshopFilesResult_t_SetCallResult( hAPICall, func );
        }
    }
    internal class CGSReputation_t_CallResult
    {
        public CGSReputation_t_CallResult() { }
        ~CGSReputation_t_CallResult()
        {
            if ( m_Handle != 0 )
            {
                Valve.Interop.NativeEntrypoints.CGSReputation_t_RemoveCallResult( m_Handle );
            }
        }
        ulong m_Handle = 0;
        public void Set( ulong hAPICall, Valve.Interop.NativeEntrypoints.SteamAPI_GSReputation_t_CallResult func )
        {
            if ( m_Handle != 0 )
            {
                Valve.Interop.NativeEntrypoints.CGSReputation_t_RemoveCallResult( m_Handle );
            }
            m_Handle = Valve.Interop.NativeEntrypoints.CGSReputation_t_SetCallResult( hAPICall, func );
        }
    }
    internal class CUserFavoriteItemsListChanged_t_CallResult
    {
        public CUserFavoriteItemsListChanged_t_CallResult() { }
        ~CUserFavoriteItemsListChanged_t_CallResult()
        {
            if ( m_Handle != 0 )
            {
                Valve.Interop.NativeEntrypoints.CUserFavoriteItemsListChanged_t_RemoveCallResult( m_Handle );
            }
        }
        ulong m_Handle = 0;
        public void Set( ulong hAPICall, Valve.Interop.NativeEntrypoints.SteamAPI_UserFavoriteItemsListChanged_t_CallResult func )
        {
            if ( m_Handle != 0 )
            {
                Valve.Interop.NativeEntrypoints.CUserFavoriteItemsListChanged_t_RemoveCallResult( m_Handle );
            }
            m_Handle = Valve.Interop.NativeEntrypoints.CUserFavoriteItemsListChanged_t_SetCallResult( hAPICall, func );
        }
    }
    internal class CEncryptedAppTicketResponse_t_CallResult
    {
        public CEncryptedAppTicketResponse_t_CallResult() { }
        ~CEncryptedAppTicketResponse_t_CallResult()
        {
            if ( m_Handle != 0 )
            {
                Valve.Interop.NativeEntrypoints.CEncryptedAppTicketResponse_t_RemoveCallResult( m_Handle );
            }
        }
        ulong m_Handle = 0;
        public void Set( ulong hAPICall, Valve.Interop.NativeEntrypoints.SteamAPI_EncryptedAppTicketResponse_t_CallResult func )
        {
            if ( m_Handle != 0 )
            {
                Valve.Interop.NativeEntrypoints.CEncryptedAppTicketResponse_t_RemoveCallResult( m_Handle );
            }
            m_Handle = Valve.Interop.NativeEntrypoints.CEncryptedAppTicketResponse_t_SetCallResult( hAPICall, func );
        }
    }
    internal class CRemoteStorageEnumerateUserPublishedFilesResult_t_CallResult
    {
        public CRemoteStorageEnumerateUserPublishedFilesResult_t_CallResult() { }
        ~CRemoteStorageEnumerateUserPublishedFilesResult_t_CallResult()
        {
            if ( m_Handle != 0 )
            {
                Valve.Interop.NativeEntrypoints.CRemoteStorageEnumerateUserPublishedFilesResult_t_RemoveCallResult( m_Handle );
            }
        }
        ulong m_Handle = 0;
        public void Set( ulong hAPICall, Valve.Interop.NativeEntrypoints.SteamAPI_RemoteStorageEnumerateUserPublishedFilesResult_t_CallResult func )
        {
            if ( m_Handle != 0 )
            {
                Valve.Interop.NativeEntrypoints.CRemoteStorageEnumerateUserPublishedFilesResult_t_RemoveCallResult( m_Handle );
            }
            m_Handle = Valve.Interop.NativeEntrypoints.CRemoteStorageEnumerateUserPublishedFilesResult_t_SetCallResult( hAPICall, func );
        }
    }
    internal class CFriendsEnumerateFollowingList_t_CallResult
    {
        public CFriendsEnumerateFollowingList_t_CallResult() { }
        ~CFriendsEnumerateFollowingList_t_CallResult()
        {
            if ( m_Handle != 0 )
            {
                Valve.Interop.NativeEntrypoints.CFriendsEnumerateFollowingList_t_RemoveCallResult( m_Handle );
            }
        }
        ulong m_Handle = 0;
        public void Set( ulong hAPICall, Valve.Interop.NativeEntrypoints.SteamAPI_FriendsEnumerateFollowingList_t_CallResult func )
        {
            if ( m_Handle != 0 )
            {
                Valve.Interop.NativeEntrypoints.CFriendsEnumerateFollowingList_t_RemoveCallResult( m_Handle );
            }
            m_Handle = Valve.Interop.NativeEntrypoints.CFriendsEnumerateFollowingList_t_SetCallResult( hAPICall, func );
        }
    }
    internal class CRemoteStorageSubscribePublishedFileResult_t_CallResult
    {
        public CRemoteStorageSubscribePublishedFileResult_t_CallResult() { }
        ~CRemoteStorageSubscribePublishedFileResult_t_CallResult()
        {
            if ( m_Handle != 0 )
            {
                Valve.Interop.NativeEntrypoints.CRemoteStorageSubscribePublishedFileResult_t_RemoveCallResult( m_Handle );
            }
        }
        ulong m_Handle = 0;
        public void Set( ulong hAPICall, Valve.Interop.NativeEntrypoints.SteamAPI_RemoteStorageSubscribePublishedFileResult_t_CallResult func )
        {
            if ( m_Handle != 0 )
            {
                Valve.Interop.NativeEntrypoints.CRemoteStorageSubscribePublishedFileResult_t_RemoveCallResult( m_Handle );
            }
            m_Handle = Valve.Interop.NativeEntrypoints.CRemoteStorageSubscribePublishedFileResult_t_SetCallResult( hAPICall, func );
        }
    }
    internal class CGetUserItemVoteResult_t_CallResult
    {
        public CGetUserItemVoteResult_t_CallResult() { }
        ~CGetUserItemVoteResult_t_CallResult()
        {
            if ( m_Handle != 0 )
            {
                Valve.Interop.NativeEntrypoints.CGetUserItemVoteResult_t_RemoveCallResult( m_Handle );
            }
        }
        ulong m_Handle = 0;
        public void Set( ulong hAPICall, Valve.Interop.NativeEntrypoints.SteamAPI_GetUserItemVoteResult_t_CallResult func )
        {
            if ( m_Handle != 0 )
            {
                Valve.Interop.NativeEntrypoints.CGetUserItemVoteResult_t_RemoveCallResult( m_Handle );
            }
            m_Handle = Valve.Interop.NativeEntrypoints.CGetUserItemVoteResult_t_SetCallResult( hAPICall, func );
        }
    }
    internal class CAssociateWithClanResult_t_CallResult
    {
        public CAssociateWithClanResult_t_CallResult() { }
        ~CAssociateWithClanResult_t_CallResult()
        {
            if ( m_Handle != 0 )
            {
                Valve.Interop.NativeEntrypoints.CAssociateWithClanResult_t_RemoveCallResult( m_Handle );
            }
        }
        ulong m_Handle = 0;
        public void Set( ulong hAPICall, Valve.Interop.NativeEntrypoints.SteamAPI_AssociateWithClanResult_t_CallResult func )
        {
            if ( m_Handle != 0 )
            {
                Valve.Interop.NativeEntrypoints.CAssociateWithClanResult_t_RemoveCallResult( m_Handle );
            }
            m_Handle = Valve.Interop.NativeEntrypoints.CAssociateWithClanResult_t_SetCallResult( hAPICall, func );
        }
    }
    internal class CLeaderboardUGCSet_t_CallResult
    {
        public CLeaderboardUGCSet_t_CallResult() { }
        ~CLeaderboardUGCSet_t_CallResult()
        {
            if ( m_Handle != 0 )
            {
                Valve.Interop.NativeEntrypoints.CLeaderboardUGCSet_t_RemoveCallResult( m_Handle );
            }
        }
        ulong m_Handle = 0;
        public void Set( ulong hAPICall, Valve.Interop.NativeEntrypoints.SteamAPI_LeaderboardUGCSet_t_CallResult func )
        {
            if ( m_Handle != 0 )
            {
                Valve.Interop.NativeEntrypoints.CLeaderboardUGCSet_t_RemoveCallResult( m_Handle );
            }
            m_Handle = Valve.Interop.NativeEntrypoints.CLeaderboardUGCSet_t_SetCallResult( hAPICall, func );
        }
    }
    internal class CCheckFileSignature_t_CallResult
    {
        public CCheckFileSignature_t_CallResult() { }
        ~CCheckFileSignature_t_CallResult()
        {
            if ( m_Handle != 0 )
            {
                Valve.Interop.NativeEntrypoints.CCheckFileSignature_t_RemoveCallResult( m_Handle );
            }
        }
        ulong m_Handle = 0;
        public void Set( ulong hAPICall, Valve.Interop.NativeEntrypoints.SteamAPI_CheckFileSignature_t_CallResult func )
        {
            if ( m_Handle != 0 )
            {
                Valve.Interop.NativeEntrypoints.CCheckFileSignature_t_RemoveCallResult( m_Handle );
            }
            m_Handle = Valve.Interop.NativeEntrypoints.CCheckFileSignature_t_SetCallResult( hAPICall, func );
        }
    }
    internal class CRemoteStorageUnsubscribePublishedFileResult_t_CallResult
    {
        public CRemoteStorageUnsubscribePublishedFileResult_t_CallResult() { }
        ~CRemoteStorageUnsubscribePublishedFileResult_t_CallResult()
        {
            if ( m_Handle != 0 )
            {
                Valve.Interop.NativeEntrypoints.CRemoteStorageUnsubscribePublishedFileResult_t_RemoveCallResult( m_Handle );
            }
        }
        ulong m_Handle = 0;
        public void Set( ulong hAPICall, Valve.Interop.NativeEntrypoints.SteamAPI_RemoteStorageUnsubscribePublishedFileResult_t_CallResult func )
        {
            if ( m_Handle != 0 )
            {
                Valve.Interop.NativeEntrypoints.CRemoteStorageUnsubscribePublishedFileResult_t_RemoveCallResult( m_Handle );
            }
            m_Handle = Valve.Interop.NativeEntrypoints.CRemoteStorageUnsubscribePublishedFileResult_t_SetCallResult( hAPICall, func );
        }
    }
    internal class CSetPersonaNameResponse_t_CallResult
    {
        public CSetPersonaNameResponse_t_CallResult() { }
        ~CSetPersonaNameResponse_t_CallResult()
        {
            if ( m_Handle != 0 )
            {
                Valve.Interop.NativeEntrypoints.CSetPersonaNameResponse_t_RemoveCallResult( m_Handle );
            }
        }
        ulong m_Handle = 0;
        public void Set( ulong hAPICall, Valve.Interop.NativeEntrypoints.SteamAPI_SetPersonaNameResponse_t_CallResult func )
        {
            if ( m_Handle != 0 )
            {
                Valve.Interop.NativeEntrypoints.CSetPersonaNameResponse_t_RemoveCallResult( m_Handle );
            }
            m_Handle = Valve.Interop.NativeEntrypoints.CSetPersonaNameResponse_t_SetCallResult( hAPICall, func );
        }
    }
    internal class CUserStatsReceived_t_CallResult
    {
        public CUserStatsReceived_t_CallResult() { }
        ~CUserStatsReceived_t_CallResult()
        {
            if ( m_Handle != 0 )
            {
                Valve.Interop.NativeEntrypoints.CUserStatsReceived_t_RemoveCallResult( m_Handle );
            }
        }
        ulong m_Handle = 0;
        public void Set( ulong hAPICall, Valve.Interop.NativeEntrypoints.SteamAPI_UserStatsReceived_t_CallResult func )
        {
            if ( m_Handle != 0 )
            {
                Valve.Interop.NativeEntrypoints.CUserStatsReceived_t_RemoveCallResult( m_Handle );
            }
            m_Handle = Valve.Interop.NativeEntrypoints.CUserStatsReceived_t_SetCallResult( hAPICall, func );
        }
    }
    internal class CJoinClanChatRoomCompletionResult_t_CallResult
    {
        public CJoinClanChatRoomCompletionResult_t_CallResult() { }
        ~CJoinClanChatRoomCompletionResult_t_CallResult()
        {
            if ( m_Handle != 0 )
            {
                Valve.Interop.NativeEntrypoints.CJoinClanChatRoomCompletionResult_t_RemoveCallResult( m_Handle );
            }
        }
        ulong m_Handle = 0;
        public void Set( ulong hAPICall, Valve.Interop.NativeEntrypoints.SteamAPI_JoinClanChatRoomCompletionResult_t_CallResult func )
        {
            if ( m_Handle != 0 )
            {
                Valve.Interop.NativeEntrypoints.CJoinClanChatRoomCompletionResult_t_RemoveCallResult( m_Handle );
            }
            m_Handle = Valve.Interop.NativeEntrypoints.CJoinClanChatRoomCompletionResult_t_SetCallResult( hAPICall, func );
        }
    }
    internal class CLeaderboardFindResult_t_CallResult
    {
        public CLeaderboardFindResult_t_CallResult() { }
        ~CLeaderboardFindResult_t_CallResult()
        {
            if ( m_Handle != 0 )
            {
                Valve.Interop.NativeEntrypoints.CLeaderboardFindResult_t_RemoveCallResult( m_Handle );
            }
        }
        ulong m_Handle = 0;
        public void Set( ulong hAPICall, Valve.Interop.NativeEntrypoints.SteamAPI_LeaderboardFindResult_t_CallResult func )
        {
            if ( m_Handle != 0 )
            {
                Valve.Interop.NativeEntrypoints.CLeaderboardFindResult_t_RemoveCallResult( m_Handle );
            }
            m_Handle = Valve.Interop.NativeEntrypoints.CLeaderboardFindResult_t_SetCallResult( hAPICall, func );
        }
    }
    internal class SteamAPIInterop
    {
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_RestartAppIfNecessary" )]
        internal static extern void SteamAPI_RestartAppIfNecessary( uint unOwnAppID );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_Init" )]
        internal static extern void SteamAPI_Init();
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_RunCallbacks" )]
        internal static extern void SteamAPI_RunCallbacks();
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_RegisterCallback" )]
        internal static extern void SteamAPI_RegisterCallback( IntPtr pCallback, int iCallback );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_UnregisterCallback" )]
        internal static extern void SteamAPI_UnregisterCallback( IntPtr pCallback );
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamClient" )]
        internal static extern IntPtr SteamClient();
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamUser" )]
        internal static extern IntPtr SteamUser();
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamFriends" )]
        internal static extern IntPtr SteamFriends();
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamUtils" )]
        internal static extern IntPtr SteamUtils();
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamMatchmaking" )]
        internal static extern IntPtr SteamMatchmaking();
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamMatchmakingServerListResponse" )]
        internal static extern IntPtr SteamMatchmakingServerListResponse();
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamMatchmakingPingResponse" )]
        internal static extern IntPtr SteamMatchmakingPingResponse();
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamMatchmakingPlayersResponse" )]
        internal static extern IntPtr SteamMatchmakingPlayersResponse();
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamMatchmakingRulesResponse" )]
        internal static extern IntPtr SteamMatchmakingRulesResponse();
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamMatchmakingServers" )]
        internal static extern IntPtr SteamMatchmakingServers();
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamRemoteStorage" )]
        internal static extern IntPtr SteamRemoteStorage();
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamUserStats" )]
        internal static extern IntPtr SteamUserStats();
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamApps" )]
        internal static extern IntPtr SteamApps();
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamNetworking" )]
        internal static extern IntPtr SteamNetworking();
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamScreenshots" )]
        internal static extern IntPtr SteamScreenshots();
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamMusic" )]
        internal static extern IntPtr SteamMusic();
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamMusicRemote" )]
        internal static extern IntPtr SteamMusicRemote();
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamHTTP" )]
        internal static extern IntPtr SteamHTTP();
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamUnifiedMessages" )]
        internal static extern IntPtr SteamUnifiedMessages();
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamController" )]
        internal static extern IntPtr SteamController();
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamUGC" )]
        internal static extern IntPtr SteamUGC();
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAppList" )]
        internal static extern IntPtr SteamAppList();
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamHTMLSurface" )]
        internal static extern IntPtr SteamHTMLSurface();
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamInventory" )]
        internal static extern IntPtr SteamInventory();
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamVideo" )]
        internal static extern IntPtr SteamVideo();
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamGameServer" )]
        internal static extern IntPtr SteamGameServer();
        [DllImportAttribute( "FacepunchSteamworksApi", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamGameServerStats" )]
        internal static extern IntPtr SteamGameServerStats();
    }


    public enum EUniverse
    {
        k_EUniverseInvalid = 0,
        k_EUniversePublic = 1,
        k_EUniverseBeta = 2,
        k_EUniverseInternal = 3,
        k_EUniverseDev = 4,
        k_EUniverseMax = 5,
    }
    public enum EResult
    {
        k_EResultOK = 1,
        k_EResultFail = 2,
        k_EResultNoConnection = 3,
        k_EResultInvalidPassword = 5,
        k_EResultLoggedInElsewhere = 6,
        k_EResultInvalidProtocolVer = 7,
        k_EResultInvalidParam = 8,
        k_EResultFileNotFound = 9,
        k_EResultBusy = 10,
        k_EResultInvalidState = 11,
        k_EResultInvalidName = 12,
        k_EResultInvalidEmail = 13,
        k_EResultDuplicateName = 14,
        k_EResultAccessDenied = 15,
        k_EResultTimeout = 16,
        k_EResultBanned = 17,
        k_EResultAccountNotFound = 18,
        k_EResultInvalidSteamID = 19,
        k_EResultServiceUnavailable = 20,
        k_EResultNotLoggedOn = 21,
        k_EResultPending = 22,
        k_EResultEncryptionFailure = 23,
        k_EResultInsufficientPrivilege = 24,
        k_EResultLimitExceeded = 25,
        k_EResultRevoked = 26,
        k_EResultExpired = 27,
        k_EResultAlreadyRedeemed = 28,
        k_EResultDuplicateRequest = 29,
        k_EResultAlreadyOwned = 30,
        k_EResultIPNotFound = 31,
        k_EResultPersistFailed = 32,
        k_EResultLockingFailed = 33,
        k_EResultLogonSessionReplaced = 34,
        k_EResultConnectFailed = 35,
        k_EResultHandshakeFailed = 36,
        k_EResultIOFailure = 37,
        k_EResultRemoteDisconnect = 38,
        k_EResultShoppingCartNotFound = 39,
        k_EResultBlocked = 40,
        k_EResultIgnored = 41,
        k_EResultNoMatch = 42,
        k_EResultAccountDisabled = 43,
        k_EResultServiceReadOnly = 44,
        k_EResultAccountNotFeatured = 45,
        k_EResultAdministratorOK = 46,
        k_EResultContentVersion = 47,
        k_EResultTryAnotherCM = 48,
        k_EResultPasswordRequiredToKickSession = 49,
        k_EResultAlreadyLoggedInElsewhere = 50,
        k_EResultSuspended = 51,
        k_EResultCancelled = 52,
        k_EResultDataCorruption = 53,
        k_EResultDiskFull = 54,
        k_EResultRemoteCallFailed = 55,
        k_EResultPasswordUnset = 56,
        k_EResultExternalAccountUnlinked = 57,
        k_EResultPSNTicketInvalid = 58,
        k_EResultExternalAccountAlreadyLinked = 59,
        k_EResultRemoteFileConflict = 60,
        k_EResultIllegalPassword = 61,
        k_EResultSameAsPreviousValue = 62,
        k_EResultAccountLogonDenied = 63,
        k_EResultCannotUseOldPassword = 64,
        k_EResultInvalidLoginAuthCode = 65,
        k_EResultAccountLogonDeniedNoMail = 66,
        k_EResultHardwareNotCapableOfIPT = 67,
        k_EResultIPTInitError = 68,
        k_EResultParentalControlRestricted = 69,
        k_EResultFacebookQueryError = 70,
        k_EResultExpiredLoginAuthCode = 71,
        k_EResultIPLoginRestrictionFailed = 72,
        k_EResultAccountLockedDown = 73,
        k_EResultAccountLogonDeniedVerifiedEmailRequired = 74,
        k_EResultNoMatchingURL = 75,
        k_EResultBadResponse = 76,
        k_EResultRequirePasswordReEntry = 77,
        k_EResultValueOutOfRange = 78,
        k_EResultUnexpectedError = 79,
        k_EResultDisabled = 80,
        k_EResultInvalidCEGSubmission = 81,
        k_EResultRestrictedDevice = 82,
        k_EResultRegionLocked = 83,
        k_EResultRateLimitExceeded = 84,
        k_EResultAccountLoginDeniedNeedTwoFactor = 85,
        k_EResultItemDeleted = 86,
        k_EResultAccountLoginDeniedThrottle = 87,
        k_EResultTwoFactorCodeMismatch = 88,
        k_EResultTwoFactorActivationCodeMismatch = 89,
        k_EResultAccountAssociatedToMultiplePartners = 90,
        k_EResultNotModified = 91,
        k_EResultNoMobileDevice = 92,
        k_EResultTimeNotSynced = 93,
        k_EResultSmsCodeFailed = 94,
        k_EResultAccountLimitExceeded = 95,
        k_EResultAccountActivityLimitExceeded = 96,
        k_EResultPhoneActivityLimitExceeded = 97,
        k_EResultRefundToWallet = 98,
        k_EResultEmailSendFailure = 99,
        k_EResultNotSettled = 100,
        k_EResultNeedCaptcha = 101,
        k_EResultGSLTDenied = 102,
        k_EResultGSOwnerDenied = 103,
        k_EResultInvalidItemType = 104,
        k_EResultIPBanned = 105,
    }
    public enum EVoiceResult
    {
        k_EVoiceResultOK = 0,
        k_EVoiceResultNotInitialized = 1,
        k_EVoiceResultNotRecording = 2,
        k_EVoiceResultNoData = 3,
        k_EVoiceResultBufferTooSmall = 4,
        k_EVoiceResultDataCorrupted = 5,
        k_EVoiceResultRestricted = 6,
        k_EVoiceResultUnsupportedCodec = 7,
        k_EVoiceResultReceiverOutOfDate = 8,
        k_EVoiceResultReceiverDidNotAnswer = 9,
    }
    public enum EDenyReason
    {
        k_EDenyInvalid = 0,
        k_EDenyInvalidVersion = 1,
        k_EDenyGeneric = 2,
        k_EDenyNotLoggedOn = 3,
        k_EDenyNoLicense = 4,
        k_EDenyCheater = 5,
        k_EDenyLoggedInElseWhere = 6,
        k_EDenyUnknownText = 7,
        k_EDenyIncompatibleAnticheat = 8,
        k_EDenyMemoryCorruption = 9,
        k_EDenyIncompatibleSoftware = 10,
        k_EDenySteamConnectionLost = 11,
        k_EDenySteamConnectionError = 12,
        k_EDenySteamResponseTimedOut = 13,
        k_EDenySteamValidationStalled = 14,
        k_EDenySteamOwnerLeftGuestUser = 15,
    }
    public enum EBeginAuthSessionResult
    {
        k_EBeginAuthSessionResultOK = 0,
        k_EBeginAuthSessionResultInvalidTicket = 1,
        k_EBeginAuthSessionResultDuplicateRequest = 2,
        k_EBeginAuthSessionResultInvalidVersion = 3,
        k_EBeginAuthSessionResultGameMismatch = 4,
        k_EBeginAuthSessionResultExpiredTicket = 5,
    }
    public enum EAuthSessionResponse
    {
        k_EAuthSessionResponseOK = 0,
        k_EAuthSessionResponseUserNotConnectedToSteam = 1,
        k_EAuthSessionResponseNoLicenseOrExpired = 2,
        k_EAuthSessionResponseVACBanned = 3,
        k_EAuthSessionResponseLoggedInElseWhere = 4,
        k_EAuthSessionResponseVACCheckTimedOut = 5,
        k_EAuthSessionResponseAuthTicketCanceled = 6,
        k_EAuthSessionResponseAuthTicketInvalidAlreadyUsed = 7,
        k_EAuthSessionResponseAuthTicketInvalid = 8,
        k_EAuthSessionResponsePublisherIssuedBan = 9,
    }
    public enum EUserHasLicenseForAppResult
    {
        k_EUserHasLicenseResultHasLicense = 0,
        k_EUserHasLicenseResultDoesNotHaveLicense = 1,
        k_EUserHasLicenseResultNoAuth = 2,
    }
    public enum EAccountType
    {
        k_EAccountTypeInvalid = 0,
        k_EAccountTypeIndividual = 1,
        k_EAccountTypeMultiseat = 2,
        k_EAccountTypeGameServer = 3,
        k_EAccountTypeAnonGameServer = 4,
        k_EAccountTypePending = 5,
        k_EAccountTypeContentServer = 6,
        k_EAccountTypeClan = 7,
        k_EAccountTypeChat = 8,
        k_EAccountTypeConsoleUser = 9,
        k_EAccountTypeAnonUser = 10,
        k_EAccountTypeMax = 11,
    }
    public enum EAppReleaseState
    {
        k_EAppReleaseState_Unknown = 0,
        k_EAppReleaseState_Unavailable = 1,
        k_EAppReleaseState_Prerelease = 2,
        k_EAppReleaseState_PreloadOnly = 3,
        k_EAppReleaseState_Released = 4,
    }
    public enum EAppOwnershipFlags
    {
        k_EAppOwnershipFlags_None = 0,
        k_EAppOwnershipFlags_OwnsLicense = 1,
        k_EAppOwnershipFlags_FreeLicense = 2,
        k_EAppOwnershipFlags_RegionRestricted = 4,
        k_EAppOwnershipFlags_LowViolence = 8,
        k_EAppOwnershipFlags_InvalidPlatform = 16,
        k_EAppOwnershipFlags_SharedLicense = 32,
        k_EAppOwnershipFlags_FreeWeekend = 64,
        k_EAppOwnershipFlags_RetailLicense = 128,
        k_EAppOwnershipFlags_LicenseLocked = 256,
        k_EAppOwnershipFlags_LicensePending = 512,
        k_EAppOwnershipFlags_LicenseExpired = 1024,
        k_EAppOwnershipFlags_LicensePermanent = 2048,
        k_EAppOwnershipFlags_LicenseRecurring = 4096,
        k_EAppOwnershipFlags_LicenseCanceled = 8192,
        k_EAppOwnershipFlags_AutoGrant = 16384,
        k_EAppOwnershipFlags_PendingGift = 32768,
        k_EAppOwnershipFlags_RentalNotActivated = 65536,
        k_EAppOwnershipFlags_Rental = 131072,
    }
    public enum EAppType
    {
        k_EAppType_Invalid = 0,
        k_EAppType_Game = 1,
        k_EAppType_Application = 2,
        k_EAppType_Tool = 4,
        k_EAppType_Demo = 8,
        k_EAppType_Media_DEPRECATED = 16,
        k_EAppType_DLC = 32,
        k_EAppType_Guide = 64,
        k_EAppType_Driver = 128,
        k_EAppType_Config = 256,
        k_EAppType_Hardware = 512,
        k_EAppType_Video = 2048,
        k_EAppType_Plugin = 4096,
        k_EAppType_Music = 8192,
        k_EAppType_Shortcut = 1073741824,
        k_EAppType_DepotOnly = -2147483648,
    }
    public enum ESteamUserStatType
    {
        k_ESteamUserStatTypeINVALID = 0,
        k_ESteamUserStatTypeINT = 1,
        k_ESteamUserStatTypeFLOAT = 2,
        k_ESteamUserStatTypeAVGRATE = 3,
        k_ESteamUserStatTypeACHIEVEMENTS = 4,
        k_ESteamUserStatTypeGROUPACHIEVEMENTS = 5,
        k_ESteamUserStatTypeMAX = 6,
    }
    public enum EChatEntryType
    {
        k_EChatEntryTypeInvalid = 0,
        k_EChatEntryTypeChatMsg = 1,
        k_EChatEntryTypeTyping = 2,
        k_EChatEntryTypeInviteGame = 3,
        k_EChatEntryTypeEmote = 4,
        k_EChatEntryTypeLeftConversation = 6,
        k_EChatEntryTypeEntered = 7,
        k_EChatEntryTypeWasKicked = 8,
        k_EChatEntryTypeWasBanned = 9,
        k_EChatEntryTypeDisconnected = 10,
        k_EChatEntryTypeHistoricalChat = 11,
        k_EChatEntryTypeLinkBlocked = 14,
    }
    public enum EChatRoomEnterResponse
    {
        k_EChatRoomEnterResponseSuccess = 1,
        k_EChatRoomEnterResponseDoesntExist = 2,
        k_EChatRoomEnterResponseNotAllowed = 3,
        k_EChatRoomEnterResponseFull = 4,
        k_EChatRoomEnterResponseError = 5,
        k_EChatRoomEnterResponseBanned = 6,
        k_EChatRoomEnterResponseLimited = 7,
        k_EChatRoomEnterResponseClanDisabled = 8,
        k_EChatRoomEnterResponseCommunityBan = 9,
        k_EChatRoomEnterResponseMemberBlockedYou = 10,
        k_EChatRoomEnterResponseYouBlockedMember = 11,
    }
    public enum EChatSteamIDInstanceFlags
    {
        k_EChatAccountInstanceMask = 4095,
        k_EChatInstanceFlagClan = 524288,
        k_EChatInstanceFlagLobby = 262144,
        k_EChatInstanceFlagMMSLobby = 131072,
    }
    public enum EMarketingMessageFlags
    {
        k_EMarketingMessageFlagsNone = 0,
        k_EMarketingMessageFlagsHighPriority = 1,
        k_EMarketingMessageFlagsPlatformWindows = 2,
        k_EMarketingMessageFlagsPlatformMac = 4,
        k_EMarketingMessageFlagsPlatformLinux = 8,
        k_EMarketingMessageFlagsPlatformRestrictions = 14,
    }
    public enum ENotificationPosition
    {
        k_EPositionTopLeft = 0,
        k_EPositionTopRight = 1,
        k_EPositionBottomLeft = 2,
        k_EPositionBottomRight = 3,
    }
    public enum EBroadcastUploadResult
    {
        k_EBroadcastUploadResultNone = 0,
        k_EBroadcastUploadResultOK = 1,
        k_EBroadcastUploadResultInitFailed = 2,
        k_EBroadcastUploadResultFrameFailed = 3,
        k_EBroadcastUploadResultTimeout = 4,
        k_EBroadcastUploadResultBandwidthExceeded = 5,
        k_EBroadcastUploadResultLowFPS = 6,
        k_EBroadcastUploadResultMissingKeyFrames = 7,
        k_EBroadcastUploadResultNoConnection = 8,
        k_EBroadcastUploadResultRelayFailed = 9,
        k_EBroadcastUploadResultSettingsChanged = 10,
        k_EBroadcastUploadResultMissingAudio = 11,
        k_EBroadcastUploadResultTooFarBehind = 12,
        k_EBroadcastUploadResultTranscodeBehind = 13,
    }
    public enum ELaunchOptionType
    {
        k_ELaunchOptionType_None = 0,
        k_ELaunchOptionType_Default = 1,
        k_ELaunchOptionType_SafeMode = 2,
        k_ELaunchOptionType_Multiplayer = 3,
        k_ELaunchOptionType_Config = 4,
        k_ELaunchOptionType_OpenVR = 5,
        k_ELaunchOptionType_Server = 6,
        k_ELaunchOptionType_Editor = 7,
        k_ELaunchOptionType_Manual = 8,
        k_ELaunchOptionType_Benchmark = 9,
        k_ELaunchOptionType_Option1 = 10,
        k_ELaunchOptionType_Option2 = 11,
        k_ELaunchOptionType_Option3 = 12,
        k_ELaunchOptionType_OtherVR = 13,
        k_ELaunchOptionType_OpenVROverlay = 14,
        k_ELaunchOptionType_Dialog = 1000,
    }
    public enum EVRHMDType
    {
        k_eEVRHMDType_Unknown = 0,
        k_eEVRHMDType_HTC_Dev = 1,
        k_eEVRHMDType_HTC_VivePre = 2,
        k_eEVRHMDType_HTC_Vive = 3,
        k_eEVRHMDType_HTC_Unknown = 20,
        k_eEVRHMDType_Oculus_DK1 = 21,
        k_eEVRHMDType_Oculus_DK2 = 22,
        k_eEVRHMDType_Oculus_Rift = 23,
        k_eEVRHMDType_Oculus_Unknown = 40,
    }
    public enum EFailureType
    {
        k_EFailureFlushedCallbackQueue = 0,
        k_EFailurePipeFail = 1,
    }
    public enum EFriendRelationship
    {
        k_EFriendRelationshipNone = 0,
        k_EFriendRelationshipBlocked = 1,
        k_EFriendRelationshipRequestRecipient = 2,
        k_EFriendRelationshipFriend = 3,
        k_EFriendRelationshipRequestInitiator = 4,
        k_EFriendRelationshipIgnored = 5,
        k_EFriendRelationshipIgnoredFriend = 6,
        k_EFriendRelationshipSuggested = 7,
        k_EFriendRelationshipMax = 8,
    }
    public enum EPersonaState
    {
        k_EPersonaStateOffline = 0,
        k_EPersonaStateOnline = 1,
        k_EPersonaStateBusy = 2,
        k_EPersonaStateAway = 3,
        k_EPersonaStateSnooze = 4,
        k_EPersonaStateLookingToTrade = 5,
        k_EPersonaStateLookingToPlay = 6,
        k_EPersonaStateMax = 7,
    }
    public enum EFriendFlags
    {
        k_EFriendFlagNone = 0,
        k_EFriendFlagBlocked = 1,
        k_EFriendFlagFriendshipRequested = 2,
        k_EFriendFlagImmediate = 4,
        k_EFriendFlagClanMember = 8,
        k_EFriendFlagOnGameServer = 16,
        k_EFriendFlagRequestingFriendship = 128,
        k_EFriendFlagRequestingInfo = 256,
        k_EFriendFlagIgnored = 512,
        k_EFriendFlagIgnoredFriend = 1024,
        k_EFriendFlagSuggested = 2048,
        k_EFriendFlagChatMember = 4096,
        k_EFriendFlagAll = 65535,
    }
    public enum EUserRestriction
    {
        k_nUserRestrictionNone = 0,
        k_nUserRestrictionUnknown = 1,
        k_nUserRestrictionAnyChat = 2,
        k_nUserRestrictionVoiceChat = 4,
        k_nUserRestrictionGroupChat = 8,
        k_nUserRestrictionRating = 16,
        k_nUserRestrictionGameInvites = 32,
        k_nUserRestrictionTrading = 64,
    }
    public enum EOverlayToStoreFlag
    {
        k_EOverlayToStoreFlag_None = 0,
        k_EOverlayToStoreFlag_AddToCart = 1,
        k_EOverlayToStoreFlag_AddToCartAndShow = 2,
    }
    public enum EPersonaChange
    {
        k_EPersonaChangeName = 1,
        k_EPersonaChangeStatus = 2,
        k_EPersonaChangeComeOnline = 4,
        k_EPersonaChangeGoneOffline = 8,
        k_EPersonaChangeGamePlayed = 16,
        k_EPersonaChangeGameServer = 32,
        k_EPersonaChangeAvatar = 64,
        k_EPersonaChangeJoinedSource = 128,
        k_EPersonaChangeLeftSource = 256,
        k_EPersonaChangeRelationshipChanged = 512,
        k_EPersonaChangeNameFirstSet = 1024,
        k_EPersonaChangeFacebookInfo = 2048,
        k_EPersonaChangeNickname = 4096,
        k_EPersonaChangeSteamLevel = 8192,
    }
    public enum ESteamAPICallFailure
    {
        k_ESteamAPICallFailureNone = -1,
        k_ESteamAPICallFailureSteamGone = 0,
        k_ESteamAPICallFailureNetworkFailure = 1,
        k_ESteamAPICallFailureInvalidHandle = 2,
        k_ESteamAPICallFailureMismatchedCallback = 3,
    }
    public enum EGamepadTextInputMode
    {
        k_EGamepadTextInputModeNormal = 0,
        k_EGamepadTextInputModePassword = 1,
    }
    public enum EGamepadTextInputLineMode
    {
        k_EGamepadTextInputLineModeSingleLine = 0,
        k_EGamepadTextInputLineModeMultipleLines = 1,
    }
    public enum ECheckFileSignature
    {
        k_ECheckFileSignatureInvalidSignature = 0,
        k_ECheckFileSignatureValidSignature = 1,
        k_ECheckFileSignatureFileNotFound = 2,
        k_ECheckFileSignatureNoSignaturesFoundForThisApp = 3,
        k_ECheckFileSignatureNoSignaturesFoundForThisFile = 4,
    }
    public enum EMatchMakingServerResponse
    {
        eServerResponded = 0,
        eServerFailedToRespond = 1,
        eNoServersListedOnMasterServer = 2,
    }
    public enum ELobbyType
    {
        k_ELobbyTypePrivate = 0,
        k_ELobbyTypeFriendsOnly = 1,
        k_ELobbyTypePublic = 2,
        k_ELobbyTypeInvisible = 3,
    }
    public enum ELobbyComparison
    {
        k_ELobbyComparisonEqualToOrLessThan = -2,
        k_ELobbyComparisonLessThan = -1,
        k_ELobbyComparisonEqual = 0,
        k_ELobbyComparisonGreaterThan = 1,
        k_ELobbyComparisonEqualToOrGreaterThan = 2,
        k_ELobbyComparisonNotEqual = 3,
    }
    public enum ELobbyDistanceFilter
    {
        k_ELobbyDistanceFilterClose = 0,
        k_ELobbyDistanceFilterDefault = 1,
        k_ELobbyDistanceFilterFar = 2,
        k_ELobbyDistanceFilterWorldwide = 3,
    }
    public enum EChatMemberStateChange
    {
        k_EChatMemberStateChangeEntered = 1,
        k_EChatMemberStateChangeLeft = 2,
        k_EChatMemberStateChangeDisconnected = 4,
        k_EChatMemberStateChangeKicked = 8,
        k_EChatMemberStateChangeBanned = 16,
    }
    public enum EResolveConflict
    {
        k_EResolveConflictKeepClient = 1,
        k_EResolveConflictKeepServer = 2,
    }
    public enum ERemoteStoragePlatform
    {
        k_ERemoteStoragePlatformNone = 0,
        k_ERemoteStoragePlatformWindows = 1,
        k_ERemoteStoragePlatformOSX = 2,
        k_ERemoteStoragePlatformPS3 = 4,
        k_ERemoteStoragePlatformLinux = 8,
        k_ERemoteStoragePlatformReserved2 = 16,
        k_ERemoteStoragePlatformAll = -1,
    }
    public enum ERemoteStoragePublishedFileVisibility
    {
        k_ERemoteStoragePublishedFileVisibilityPublic = 0,
        k_ERemoteStoragePublishedFileVisibilityFriendsOnly = 1,
        k_ERemoteStoragePublishedFileVisibilityPrivate = 2,
    }
    public enum EWorkshopFileType
    {
        k_EWorkshopFileTypeFirst = 0,
        k_EWorkshopFileTypeCommunity = 0,
        k_EWorkshopFileTypeMicrotransaction = 1,
        k_EWorkshopFileTypeCollection = 2,
        k_EWorkshopFileTypeArt = 3,
        k_EWorkshopFileTypeVideo = 4,
        k_EWorkshopFileTypeScreenshot = 5,
        k_EWorkshopFileTypeGame = 6,
        k_EWorkshopFileTypeSoftware = 7,
        k_EWorkshopFileTypeConcept = 8,
        k_EWorkshopFileTypeWebGuide = 9,
        k_EWorkshopFileTypeIntegratedGuide = 10,
        k_EWorkshopFileTypeMerch = 11,
        k_EWorkshopFileTypeControllerBinding = 12,
        k_EWorkshopFileTypeSteamworksAccessInvite = 13,
        k_EWorkshopFileTypeSteamVideo = 14,
        k_EWorkshopFileTypeGameManagedItem = 15,
        k_EWorkshopFileTypeMax = 16,
    }
    public enum EWorkshopVote
    {
        k_EWorkshopVoteUnvoted = 0,
        k_EWorkshopVoteFor = 1,
        k_EWorkshopVoteAgainst = 2,
        k_EWorkshopVoteLater = 3,
    }
    public enum EWorkshopFileAction
    {
        k_EWorkshopFileActionPlayed = 0,
        k_EWorkshopFileActionCompleted = 1,
    }
    public enum EWorkshopEnumerationType
    {
        k_EWorkshopEnumerationTypeRankedByVote = 0,
        k_EWorkshopEnumerationTypeRecent = 1,
        k_EWorkshopEnumerationTypeTrending = 2,
        k_EWorkshopEnumerationTypeFavoritesOfFriends = 3,
        k_EWorkshopEnumerationTypeVotedByFriends = 4,
        k_EWorkshopEnumerationTypeContentByFriends = 5,
        k_EWorkshopEnumerationTypeRecentFromFollowedUsers = 6,
    }
    public enum EWorkshopVideoProvider
    {
        k_EWorkshopVideoProviderNone = 0,
        k_EWorkshopVideoProviderYoutube = 1,
    }
    public enum EUGCReadAction
    {
        k_EUGCRead_ContinueReadingUntilFinished = 0,
        k_EUGCRead_ContinueReading = 1,
        k_EUGCRead_Close = 2,
    }
    public enum ELeaderboardDataRequest
    {
        k_ELeaderboardDataRequestGlobal = 0,
        k_ELeaderboardDataRequestGlobalAroundUser = 1,
        k_ELeaderboardDataRequestFriends = 2,
        k_ELeaderboardDataRequestUsers = 3,
    }
    public enum ELeaderboardSortMethod
    {
        k_ELeaderboardSortMethodNone = 0,
        k_ELeaderboardSortMethodAscending = 1,
        k_ELeaderboardSortMethodDescending = 2,
    }
    public enum ELeaderboardDisplayType
    {
        k_ELeaderboardDisplayTypeNone = 0,
        k_ELeaderboardDisplayTypeNumeric = 1,
        k_ELeaderboardDisplayTypeTimeSeconds = 2,
        k_ELeaderboardDisplayTypeTimeMilliSeconds = 3,
    }
    public enum ELeaderboardUploadScoreMethod
    {
        k_ELeaderboardUploadScoreMethodNone = 0,
        k_ELeaderboardUploadScoreMethodKeepBest = 1,
        k_ELeaderboardUploadScoreMethodForceUpdate = 2,
    }
    public enum ERegisterActivationCodeResult
    {
        k_ERegisterActivationCodeResultOK = 0,
        k_ERegisterActivationCodeResultFail = 1,
        k_ERegisterActivationCodeResultAlreadyRegistered = 2,
        k_ERegisterActivationCodeResultTimeout = 3,
        k_ERegisterActivationCodeAlreadyOwned = 4,
    }
    public enum EP2PSessionError
    {
        k_EP2PSessionErrorNone = 0,
        k_EP2PSessionErrorNotRunningApp = 1,
        k_EP2PSessionErrorNoRightsToApp = 2,
        k_EP2PSessionErrorDestinationNotLoggedIn = 3,
        k_EP2PSessionErrorTimeout = 4,
        k_EP2PSessionErrorMax = 5,
    }
    public enum EP2PSend
    {
        k_EP2PSendUnreliable = 0,
        k_EP2PSendUnreliableNoDelay = 1,
        k_EP2PSendReliable = 2,
        k_EP2PSendReliableWithBuffering = 3,
    }
    public enum ESNetSocketState
    {
        k_ESNetSocketStateInvalid = 0,
        k_ESNetSocketStateConnected = 1,
        k_ESNetSocketStateInitiated = 10,
        k_ESNetSocketStateLocalCandidatesFound = 11,
        k_ESNetSocketStateReceivedRemoteCandidates = 12,
        k_ESNetSocketStateChallengeHandshake = 15,
        k_ESNetSocketStateDisconnecting = 21,
        k_ESNetSocketStateLocalDisconnect = 22,
        k_ESNetSocketStateTimeoutDuringConnect = 23,
        k_ESNetSocketStateRemoteEndDisconnected = 24,
        k_ESNetSocketStateConnectionBroken = 25,
    }
    public enum ESNetSocketConnectionType
    {
        k_ESNetSocketConnectionTypeNotConnected = 0,
        k_ESNetSocketConnectionTypeUDP = 1,
        k_ESNetSocketConnectionTypeUDPRelay = 2,
    }
    public enum AudioPlayback_Status
    {
        AudioPlayback_Undefined = 0,
        AudioPlayback_Playing = 1,
        AudioPlayback_Paused = 2,
        AudioPlayback_Idle = 3,
    }
    public enum EHTTPMethod
    {
        k_EHTTPMethodInvalid = 0,
        k_EHTTPMethodGET = 1,
        k_EHTTPMethodHEAD = 2,
        k_EHTTPMethodPOST = 3,
        k_EHTTPMethodPUT = 4,
        k_EHTTPMethodDELETE = 5,
        k_EHTTPMethodOPTIONS = 6,
        k_EHTTPMethodPATCH = 7,
    }
    public enum EHTTPStatusCode
    {
        k_EHTTPStatusCodeInvalid = 0,
        k_EHTTPStatusCode100Continue = 100,
        k_EHTTPStatusCode101SwitchingProtocols = 101,
        k_EHTTPStatusCode200OK = 200,
        k_EHTTPStatusCode201Created = 201,
        k_EHTTPStatusCode202Accepted = 202,
        k_EHTTPStatusCode203NonAuthoritative = 203,
        k_EHTTPStatusCode204NoContent = 204,
        k_EHTTPStatusCode205ResetContent = 205,
        k_EHTTPStatusCode206PartialContent = 206,
        k_EHTTPStatusCode300MultipleChoices = 300,
        k_EHTTPStatusCode301MovedPermanently = 301,
        k_EHTTPStatusCode302Found = 302,
        k_EHTTPStatusCode303SeeOther = 303,
        k_EHTTPStatusCode304NotModified = 304,
        k_EHTTPStatusCode305UseProxy = 305,
        k_EHTTPStatusCode307TemporaryRedirect = 307,
        k_EHTTPStatusCode400BadRequest = 400,
        k_EHTTPStatusCode401Unauthorized = 401,
        k_EHTTPStatusCode402PaymentRequired = 402,
        k_EHTTPStatusCode403Forbidden = 403,
        k_EHTTPStatusCode404NotFound = 404,
        k_EHTTPStatusCode405MethodNotAllowed = 405,
        k_EHTTPStatusCode406NotAcceptable = 406,
        k_EHTTPStatusCode407ProxyAuthRequired = 407,
        k_EHTTPStatusCode408RequestTimeout = 408,
        k_EHTTPStatusCode409Conflict = 409,
        k_EHTTPStatusCode410Gone = 410,
        k_EHTTPStatusCode411LengthRequired = 411,
        k_EHTTPStatusCode412PreconditionFailed = 412,
        k_EHTTPStatusCode413RequestEntityTooLarge = 413,
        k_EHTTPStatusCode414RequestURITooLong = 414,
        k_EHTTPStatusCode415UnsupportedMediaType = 415,
        k_EHTTPStatusCode416RequestedRangeNotSatisfiable = 416,
        k_EHTTPStatusCode417ExpectationFailed = 417,
        k_EHTTPStatusCode4xxUnknown = 418,
        k_EHTTPStatusCode429TooManyRequests = 429,
        k_EHTTPStatusCode500InternalServerError = 500,
        k_EHTTPStatusCode501NotImplemented = 501,
        k_EHTTPStatusCode502BadGateway = 502,
        k_EHTTPStatusCode503ServiceUnavailable = 503,
        k_EHTTPStatusCode504GatewayTimeout = 504,
        k_EHTTPStatusCode505HTTPVersionNotSupported = 505,
        k_EHTTPStatusCode5xxUnknown = 599,
    }
    public enum ESteamControllerPad
    {
        k_ESteamControllerPad_Left = 0,
        k_ESteamControllerPad_Right = 1,
    }
    public enum EControllerSource
    {
        k_EControllerSource_None = 0,
        k_EControllerSource_LeftTrackpad = 1,
        k_EControllerSource_RightTrackpad = 2,
        k_EControllerSource_Joystick = 3,
        k_EControllerSource_ABXY = 4,
        k_EControllerSource_Switch = 5,
        k_EControllerSource_LeftTrigger = 6,
        k_EControllerSource_RightTrigger = 7,
        k_EControllerSource_Gyro = 8,
        k_EControllerSource_Count = 9,
    }
    public enum EControllerSourceMode
    {
        k_EControllerSourceMode_None = 0,
        k_EControllerSourceMode_Dpad = 1,
        k_EControllerSourceMode_Buttons = 2,
        k_EControllerSourceMode_FourButtons = 3,
        k_EControllerSourceMode_AbsoluteMouse = 4,
        k_EControllerSourceMode_RelativeMouse = 5,
        k_EControllerSourceMode_JoystickMove = 6,
        k_EControllerSourceMode_JoystickCamera = 7,
        k_EControllerSourceMode_ScrollWheel = 8,
        k_EControllerSourceMode_Trigger = 9,
        k_EControllerSourceMode_TouchMenu = 10,
        k_EControllerSourceMode_MouseJoystick = 11,
        k_EControllerSourceMode_MouseRegion = 12,
    }
    public enum EControllerActionOrigin
    {
        k_EControllerActionOrigin_None = 0,
        k_EControllerActionOrigin_A = 1,
        k_EControllerActionOrigin_B = 2,
        k_EControllerActionOrigin_X = 3,
        k_EControllerActionOrigin_Y = 4,
        k_EControllerActionOrigin_LeftBumper = 5,
        k_EControllerActionOrigin_RightBumper = 6,
        k_EControllerActionOrigin_LeftGrip = 7,
        k_EControllerActionOrigin_RightGrip = 8,
        k_EControllerActionOrigin_Start = 9,
        k_EControllerActionOrigin_Back = 10,
        k_EControllerActionOrigin_LeftPad_Touch = 11,
        k_EControllerActionOrigin_LeftPad_Swipe = 12,
        k_EControllerActionOrigin_LeftPad_Click = 13,
        k_EControllerActionOrigin_LeftPad_DPadNorth = 14,
        k_EControllerActionOrigin_LeftPad_DPadSouth = 15,
        k_EControllerActionOrigin_LeftPad_DPadWest = 16,
        k_EControllerActionOrigin_LeftPad_DPadEast = 17,
        k_EControllerActionOrigin_RightPad_Touch = 18,
        k_EControllerActionOrigin_RightPad_Swipe = 19,
        k_EControllerActionOrigin_RightPad_Click = 20,
        k_EControllerActionOrigin_RightPad_DPadNorth = 21,
        k_EControllerActionOrigin_RightPad_DPadSouth = 22,
        k_EControllerActionOrigin_RightPad_DPadWest = 23,
        k_EControllerActionOrigin_RightPad_DPadEast = 24,
        k_EControllerActionOrigin_LeftTrigger_Pull = 25,
        k_EControllerActionOrigin_LeftTrigger_Click = 26,
        k_EControllerActionOrigin_RightTrigger_Pull = 27,
        k_EControllerActionOrigin_RightTrigger_Click = 28,
        k_EControllerActionOrigin_LeftStick_Move = 29,
        k_EControllerActionOrigin_LeftStick_Click = 30,
        k_EControllerActionOrigin_LeftStick_DPadNorth = 31,
        k_EControllerActionOrigin_LeftStick_DPadSouth = 32,
        k_EControllerActionOrigin_LeftStick_DPadWest = 33,
        k_EControllerActionOrigin_LeftStick_DPadEast = 34,
        k_EControllerActionOrigin_Gyro_Move = 35,
        k_EControllerActionOrigin_Gyro_Pitch = 36,
        k_EControllerActionOrigin_Gyro_Yaw = 37,
        k_EControllerActionOrigin_Gyro_Roll = 38,
        k_EControllerActionOrigin_Count = 39,
    }
    public enum EUGCMatchingUGCType
    {
        k_EUGCMatchingUGCType_Items = 0,
        k_EUGCMatchingUGCType_Items_Mtx = 1,
        k_EUGCMatchingUGCType_Items_ReadyToUse = 2,
        k_EUGCMatchingUGCType_Collections = 3,
        k_EUGCMatchingUGCType_Artwork = 4,
        k_EUGCMatchingUGCType_Videos = 5,
        k_EUGCMatchingUGCType_Screenshots = 6,
        k_EUGCMatchingUGCType_AllGuides = 7,
        k_EUGCMatchingUGCType_WebGuides = 8,
        k_EUGCMatchingUGCType_IntegratedGuides = 9,
        k_EUGCMatchingUGCType_UsableInGame = 10,
        k_EUGCMatchingUGCType_ControllerBindings = 11,
        k_EUGCMatchingUGCType_GameManagedItems = 12,
        k_EUGCMatchingUGCType_All = -1,
    }
    public enum EUserUGCList
    {
        k_EUserUGCList_Published = 0,
        k_EUserUGCList_VotedOn = 1,
        k_EUserUGCList_VotedUp = 2,
        k_EUserUGCList_VotedDown = 3,
        k_EUserUGCList_WillVoteLater = 4,
        k_EUserUGCList_Favorited = 5,
        k_EUserUGCList_Subscribed = 6,
        k_EUserUGCList_UsedOrPlayed = 7,
        k_EUserUGCList_Followed = 8,
    }
    public enum EUserUGCListSortOrder
    {
        k_EUserUGCListSortOrder_CreationOrderDesc = 0,
        k_EUserUGCListSortOrder_CreationOrderAsc = 1,
        k_EUserUGCListSortOrder_TitleAsc = 2,
        k_EUserUGCListSortOrder_LastUpdatedDesc = 3,
        k_EUserUGCListSortOrder_SubscriptionDateDesc = 4,
        k_EUserUGCListSortOrder_VoteScoreDesc = 5,
        k_EUserUGCListSortOrder_ForModeration = 6,
    }
    public enum EUGCQuery
    {
        k_EUGCQuery_RankedByVote = 0,
        k_EUGCQuery_RankedByPublicationDate = 1,
        k_EUGCQuery_AcceptedForGameRankedByAcceptanceDate = 2,
        k_EUGCQuery_RankedByTrend = 3,
        k_EUGCQuery_FavoritedByFriendsRankedByPublicationDate = 4,
        k_EUGCQuery_CreatedByFriendsRankedByPublicationDate = 5,
        k_EUGCQuery_RankedByNumTimesReported = 6,
        k_EUGCQuery_CreatedByFollowedUsersRankedByPublicationDate = 7,
        k_EUGCQuery_NotYetRated = 8,
        k_EUGCQuery_RankedByTotalVotesAsc = 9,
        k_EUGCQuery_RankedByVotesUp = 10,
        k_EUGCQuery_RankedByTextSearch = 11,
        k_EUGCQuery_RankedByTotalUniqueSubscriptions = 12,
    }
    public enum EItemUpdateStatus
    {
        k_EItemUpdateStatusInvalid = 0,
        k_EItemUpdateStatusPreparingConfig = 1,
        k_EItemUpdateStatusPreparingContent = 2,
        k_EItemUpdateStatusUploadingContent = 3,
        k_EItemUpdateStatusUploadingPreviewFile = 4,
        k_EItemUpdateStatusCommittingChanges = 5,
    }
    public enum EItemState
    {
        k_EItemStateNone = 0,
        k_EItemStateSubscribed = 1,
        k_EItemStateLegacyItem = 2,
        k_EItemStateInstalled = 4,
        k_EItemStateNeedsUpdate = 8,
        k_EItemStateDownloading = 16,
        k_EItemStateDownloadPending = 32,
    }
    public enum EItemStatistic
    {
        k_EItemStatistic_NumSubscriptions = 0,
        k_EItemStatistic_NumFavorites = 1,
        k_EItemStatistic_NumFollowers = 2,
        k_EItemStatistic_NumUniqueSubscriptions = 3,
        k_EItemStatistic_NumUniqueFavorites = 4,
        k_EItemStatistic_NumUniqueFollowers = 5,
        k_EItemStatistic_NumUniqueWebsiteViews = 6,
        k_EItemStatistic_ReportScore = 7,
    }
    public enum EItemPreviewType
    {
        k_EItemPreviewType_Image = 0,
        k_EItemPreviewType_YouTubeVideo = 1,
        k_EItemPreviewType_Sketchfab = 2,
        k_EItemPreviewType_EnvironmentMap_HorizontalCross = 3,
        k_EItemPreviewType_EnvironmentMap_LatLong = 4,
        k_EItemPreviewType_ReservedMax = 255,
    }
    public enum EHTMLMouseButton
    {
        eHTMLMouseButton_Left = 0,
        eHTMLMouseButton_Right = 1,
        eHTMLMouseButton_Middle = 2,
    }
    public enum EMouseCursor
    {
        dc_user = 0,
        dc_none = 1,
        dc_arrow = 2,
        dc_ibeam = 3,
        dc_hourglass = 4,
        dc_waitarrow = 5,
        dc_crosshair = 6,
        dc_up = 7,
        dc_sizenw = 8,
        dc_sizese = 9,
        dc_sizene = 10,
        dc_sizesw = 11,
        dc_sizew = 12,
        dc_sizee = 13,
        dc_sizen = 14,
        dc_sizes = 15,
        dc_sizewe = 16,
        dc_sizens = 17,
        dc_sizeall = 18,
        dc_no = 19,
        dc_hand = 20,
        dc_blank = 21,
        dc_middle_pan = 22,
        dc_north_pan = 23,
        dc_north_east_pan = 24,
        dc_east_pan = 25,
        dc_south_east_pan = 26,
        dc_south_pan = 27,
        dc_south_west_pan = 28,
        dc_west_pan = 29,
        dc_north_west_pan = 30,
        dc_alias = 31,
        dc_cell = 32,
        dc_colresize = 33,
        dc_copycur = 34,
        dc_verticaltext = 35,
        dc_rowresize = 36,
        dc_zoomin = 37,
        dc_zoomout = 38,
        dc_help = 39,
        dc_custom = 40,
        dc_last = 41,
    }
    public enum EHTMLKeyModifiers
    {
        k_eHTMLKeyModifier_None = 0,
        k_eHTMLKeyModifier_AltDown = 1,
        k_eHTMLKeyModifier_CtrlDown = 2,
        k_eHTMLKeyModifier_ShiftDown = 4,
    }
    public enum ESteamItemFlags
    {
        k_ESteamItemNoTrade = 1,
        k_ESteamItemRemoved = 256,
        k_ESteamItemConsumed = 512,
    }
    [StructLayout( LayoutKind.Sequential )]
    public struct CSteamID
    {
        public SteamID_t m_steamid;
    }
    [StructLayout( LayoutKind.Sequential )]
    public struct SteamID_t
    {
        public SteamIDComponent_t m_comp;
        public ulong m_unAll64Bits;
    }
    [StructLayout( LayoutKind.Sequential )]
    public struct SteamIDComponent_t
    {
        public uint m_unAccountID;
        public uint m_unAccountInstance;
        public uint m_EAccountType;
        public EUniverse m_EUniverse;
    }
    [StructLayout( LayoutKind.Sequential )]
    public struct GameID_t
    {
        public uint m_nAppID;
        public uint m_nType;
        public uint m_nModID;
    }
    [StructLayout( LayoutKind.Sequential )]
    public struct ValvePackingSentinel_t
    {
        public uint m_u32;
        public ulong m_u64;
        public char m_u16;
        public double m_d;
    }
    [StructLayout( LayoutKind.Sequential )]
    public struct CallbackMsg_t
    {
        public uint m_hSteamUser;
        public int m_iCallback;
        public IntPtr m_pubParam; // uint8 *
        public int m_cubParam;
    }
    [StructLayout( LayoutKind.Sequential )]
    public struct SteamServerConnectFailure_t
    {
        public EResult m_eResult;
        [MarshalAs(UnmanagedType.I1)]
        public bool m_bStillRetrying;
    }
    [StructLayout( LayoutKind.Sequential )]
    public struct SteamServersDisconnected_t
    {
        public EResult m_eResult;
    }
    [StructLayout( LayoutKind.Sequential )]
    public struct ClientGameServerDeny_t
    {
        public uint m_uAppID;
        public uint m_unGameServerIP;
        public char m_usGameServerPort;
        public char m_bSecure;
        public uint m_uReason;
    }
    [StructLayout( LayoutKind.Sequential )]
    public struct ValidateAuthTicketResponse_t
    {
        public ulong m_SteamID;
        public EAuthSessionResponse m_eAuthSessionResponse;
        public ulong m_OwnerSteamID;
    }
    [StructLayout( LayoutKind.Sequential )]
    public struct MicroTxnAuthorizationResponse_t
    {
        public uint m_unAppID;
        public ulong m_ulOrderID;
        public byte m_bAuthorized;
    }
    [StructLayout( LayoutKind.Sequential )]
    public struct EncryptedAppTicketResponse_t
    {
        public EResult m_eResult;
    }
    [StructLayout( LayoutKind.Sequential )]
    public struct GetAuthSessionTicketResponse_t
    {
        public uint m_hAuthTicket;
        public EResult m_eResult;
    }
    [StructLayout( LayoutKind.Sequential )]
    public struct GameWebCallback_t
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
        public string m_szURL; //char[256]
    }
    [StructLayout( LayoutKind.Sequential )]
    public struct StoreAuthURLResponse_t
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 512)]
        public string m_szURL; //char[512]
    }
    [StructLayout( LayoutKind.Sequential )]
    public struct FriendGameInfo_t
    {
        public ulong m_gameID;
        public uint m_unGameIP;
        public short m_usGamePort;
        public short m_usQueryPort;
        public ulong m_steamIDLobby;
    }
    [StructLayout( LayoutKind.Sequential )]
    public struct FriendSessionStateInfo_t
    {
        public uint m_uiOnlineSessionInstances;
        public byte m_uiPublishedToFriendsSessionInstance;
    }
    [StructLayout( LayoutKind.Sequential )]
    public struct PersonaStateChange_t
    {
        public ulong m_ulSteamID;
        public int m_nChangeFlags;
    }
    [StructLayout( LayoutKind.Sequential )]
    public struct GameOverlayActivated_t
    {
        public byte m_bActive;
    }
    [StructLayout( LayoutKind.Sequential )]
    public struct GameServerChangeRequested_t
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
        public string m_rgchServer; //char[64]
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
        public string m_rgchPassword; //char[64]
    }
    [StructLayout( LayoutKind.Sequential )]
    public struct GameLobbyJoinRequested_t
    {
        public ulong m_steamIDLobby;
        public ulong m_steamIDFriend;
    }
    [StructLayout( LayoutKind.Sequential )]
    public struct AvatarImageLoaded_t
    {
        public ulong m_steamID;
        public int m_iImage;
        public int m_iWide;
        public int m_iTall;
    }
    [StructLayout( LayoutKind.Sequential )]
    public struct ClanOfficerListResponse_t
    {
        public ulong m_steamIDClan;
        public int m_cOfficers;
        public byte m_bSuccess;
    }
    [StructLayout( LayoutKind.Sequential )]
    public struct FriendRichPresenceUpdate_t
    {
        public ulong m_steamIDFriend;
        public uint m_nAppID;
    }
    [StructLayout( LayoutKind.Sequential )]
    public struct GameRichPresenceJoinRequested_t
    {
        public ulong m_steamIDFriend;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
        public string m_rgchConnect; //char[256]
    }
    [StructLayout( LayoutKind.Sequential )]
    public struct GameConnectedClanChatMsg_t
    {
        public ulong m_steamIDClanChat;
        public ulong m_steamIDUser;
        public int m_iMessageID;
    }
    [StructLayout( LayoutKind.Sequential )]
    public struct GameConnectedChatJoin_t
    {
        public ulong m_steamIDClanChat;
        public ulong m_steamIDUser;
    }
    [StructLayout( LayoutKind.Sequential )]
    public struct GameConnectedChatLeave_t
    {
        public ulong m_steamIDClanChat;
        public ulong m_steamIDUser;
        [MarshalAs(UnmanagedType.I1)]
        public bool m_bKicked;
        [MarshalAs(UnmanagedType.I1)]
        public bool m_bDropped;
    }
    [StructLayout( LayoutKind.Sequential )]
    public struct DownloadClanActivityCountsResult_t
    {
        [MarshalAs(UnmanagedType.I1)]
        public bool m_bSuccess;
    }
    [StructLayout( LayoutKind.Sequential )]
    public struct JoinClanChatRoomCompletionResult_t
    {
        public ulong m_steamIDClanChat;
        public EChatRoomEnterResponse m_eChatRoomEnterResponse;
    }
    [StructLayout( LayoutKind.Sequential )]
    public struct GameConnectedFriendChatMsg_t
    {
        public ulong m_steamIDUser;
        public int m_iMessageID;
    }
    [StructLayout( LayoutKind.Sequential )]
    public struct FriendsGetFollowerCount_t
    {
        public EResult m_eResult;
        public ulong m_steamID;
        public int m_nCount;
    }
    [StructLayout( LayoutKind.Sequential )]
    public struct FriendsIsFollowing_t
    {
        public EResult m_eResult;
        public ulong m_steamID;
        [MarshalAs(UnmanagedType.I1)]
        public bool m_bIsFollowing;
    }
    [StructLayout( LayoutKind.Sequential )]
    public struct FriendsEnumerateFollowingList_t
    {
        public EResult m_eResult;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 50, ArraySubType = UnmanagedType.U8)]
        public CSteamID[] m_rgSteamID; //CSteamID[50]
        public int m_nResultsReturned;
        public int m_nTotalResultCount;
    }
    [StructLayout( LayoutKind.Sequential )]
    public struct SetPersonaNameResponse_t
    {
        [MarshalAs(UnmanagedType.I1)]
        public bool m_bSuccess;
        [MarshalAs(UnmanagedType.I1)]
        public bool m_bLocalSuccess;
        public EResult m_result;
    }
    [StructLayout( LayoutKind.Sequential )]
    public struct LowBatteryPower_t
    {
        public byte m_nMinutesBatteryLeft;
    }
    [StructLayout( LayoutKind.Sequential )]
    public struct SteamAPICallCompleted_t
    {
        public ulong m_hAsyncCall;
        public int m_iCallback;
        public uint m_cubParam;
    }
    [StructLayout( LayoutKind.Sequential )]
    public struct CheckFileSignature_t
    {
        public ECheckFileSignature m_eCheckFileSignature;
    }
    [StructLayout( LayoutKind.Sequential )]
    public struct GamepadTextInputDismissed_t
    {
        [MarshalAs(UnmanagedType.I1)]
        public bool m_bSubmitted;
        public uint m_unSubmittedText;
    }
    [StructLayout( LayoutKind.Sequential )]
    public struct MatchMakingKeyValuePair_t
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
        public string m_szKey; //char[256]
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
        public string m_szValue; //char[256]
    }
    [StructLayout( LayoutKind.Sequential, CharSet = CharSet.Ansi )]
    public struct servernetadr_t
    {
        public ushort m_usConnectionPort;
        public ushort m_usQueryPort;
        public uint m_unIP;
    }
    [StructLayout( LayoutKind.Sequential, Size = 372, Pack = 4, CharSet = CharSet.Ansi )]
    public struct gameserveritem_t
    {
        public servernetadr_t m_NetAdr;
        public int m_nPing;
        [MarshalAs(UnmanagedType.I1)]
        public bool m_bHadSuccessfulResponse;
        [MarshalAs(UnmanagedType.I1)]
        public bool m_bDoNotRefresh;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string m_szGameDir; //char[32]
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string m_szMap; //char[32]
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
        public string m_szGameDescription; //char[64]
        public uint m_nAppID;
        public int m_nPlayers;
        public int m_nMaxPlayers;
        public int m_nBotPlayers;
        [MarshalAs(UnmanagedType.I1)]
        public bool m_bPassword;
        [MarshalAs(UnmanagedType.I1)]
        public bool m_bSecure;
        public uint m_ulTimeLastPlayed;
        public int m_nServerVersion;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
        public string m_szServerName; //char[64]
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
        public string m_szGameTags; //char[128]
        public ulong m_steamID;
    }
    [StructLayout( LayoutKind.Sequential )]
    public struct FavoritesListChanged_t
    {
        public uint m_nIP;
        public uint m_nQueryPort;
        public uint m_nConnPort;
        public uint m_nAppID;
        public uint m_nFlags;
        [MarshalAs(UnmanagedType.I1)]
        public bool m_bAdd;
        public uint m_unAccountId;
    }
    [StructLayout( LayoutKind.Sequential )]
    public struct LobbyInvite_t
    {
        public ulong m_ulSteamIDUser;
        public ulong m_ulSteamIDLobby;
        public ulong m_ulGameID;
    }
    [StructLayout( LayoutKind.Sequential )]
    public struct LobbyEnter_t
    {
        public ulong m_ulSteamIDLobby;
        public uint m_rgfChatPermissions;
        [MarshalAs(UnmanagedType.I1)]
        public bool m_bLocked;
        public uint m_EChatRoomEnterResponse;
    }
    [StructLayout( LayoutKind.Sequential )]
    public struct LobbyDataUpdate_t
    {
        public ulong m_ulSteamIDLobby;
        public ulong m_ulSteamIDMember;
        public byte m_bSuccess;
    }
    [StructLayout( LayoutKind.Sequential )]
    public struct LobbyChatUpdate_t
    {
        public ulong m_ulSteamIDLobby;
        public ulong m_ulSteamIDUserChanged;
        public ulong m_ulSteamIDMakingChange;
        public uint m_rgfChatMemberStateChange;
    }
    [StructLayout( LayoutKind.Sequential )]
    public struct LobbyChatMsg_t
    {
        public ulong m_ulSteamIDLobby;
        public ulong m_ulSteamIDUser;
        public byte m_eChatEntryType;
        public uint m_iChatID;
    }
    [StructLayout( LayoutKind.Sequential )]
    public struct LobbyGameCreated_t
    {
        public ulong m_ulSteamIDLobby;
        public ulong m_ulSteamIDGameServer;
        public uint m_unIP;
        public char m_usPort;
    }
    [StructLayout( LayoutKind.Sequential )]
    public struct LobbyMatchList_t
    {
        public uint m_nLobbiesMatching;
    }
    [StructLayout( LayoutKind.Sequential )]
    public struct LobbyKicked_t
    {
        public ulong m_ulSteamIDLobby;
        public ulong m_ulSteamIDAdmin;
        public byte m_bKickedDueToDisconnect;
    }
    [StructLayout( LayoutKind.Sequential )]
    public struct LobbyCreated_t
    {
        public EResult m_eResult;
        public ulong m_ulSteamIDLobby;
    }
    [StructLayout( LayoutKind.Sequential )]
    public struct PSNGameBootInviteResult_t
    {
        [MarshalAs(UnmanagedType.I1)]
        public bool m_bGameBootInviteExists;
        public ulong m_steamIDLobby;
    }
    [StructLayout( LayoutKind.Sequential )]
    public struct FavoritesListAccountsUpdated_t
    {
        public EResult m_eResult;
    }
    [StructLayout( LayoutKind.Sequential )]
    public struct SteamParamStringArray_t
    {
        public IntPtr m_ppStrings; // const char **
        public int m_nNumStrings;
    }
    [StructLayout( LayoutKind.Sequential )]
    public struct RemoteStorageAppSyncedClient_t
    {
        public uint m_nAppID;
        public EResult m_eResult;
        public int m_unNumDownloads;
    }
    [StructLayout( LayoutKind.Sequential )]
    public struct RemoteStorageAppSyncedServer_t
    {
        public uint m_nAppID;
        public EResult m_eResult;
        public int m_unNumUploads;
    }
    [StructLayout( LayoutKind.Sequential )]
    public struct RemoteStorageAppSyncProgress_t
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
        public string m_rgchCurrentFile; //char[260]
        public uint m_nAppID;
        public uint m_uBytesTransferredThisChunk;
        public double m_dAppPercentComplete;
        [MarshalAs(UnmanagedType.I1)]
        public bool m_bUploading;
    }
    [StructLayout( LayoutKind.Sequential )]
    public struct RemoteStorageAppSyncStatusCheck_t
    {
        public uint m_nAppID;
        public EResult m_eResult;
    }
    [StructLayout( LayoutKind.Sequential )]
    public struct RemoteStorageConflictResolution_t
    {
        public uint m_nAppID;
        public EResult m_eResult;
    }
    [StructLayout( LayoutKind.Sequential )]
    public struct RemoteStorageFileShareResult_t
    {
        public EResult m_eResult;
        public ulong m_hFile;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
        public string m_rgchFilename; //char[260]
    }
    [StructLayout( LayoutKind.Sequential )]
    public struct RemoteStoragePublishFileResult_t
    {
        public EResult m_eResult;
        public ulong m_nPublishedFileId;
        [MarshalAs(UnmanagedType.I1)]
        public bool m_bUserNeedsToAcceptWorkshopLegalAgreement;
    }
    [StructLayout( LayoutKind.Sequential )]
    public struct RemoteStorageDeletePublishedFileResult_t
    {
        public EResult m_eResult;
        public ulong m_nPublishedFileId;
    }
    [StructLayout( LayoutKind.Sequential )]
    public struct RemoteStorageEnumerateUserPublishedFilesResult_t
    {
        public EResult m_eResult;
        public int m_nResultsReturned;
        public int m_nTotalResultCount;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 50, ArraySubType = UnmanagedType.U8)]
        public ulong[] m_rgPublishedFileId; //ulong[50]
    }
    [StructLayout( LayoutKind.Sequential )]
    public struct RemoteStorageSubscribePublishedFileResult_t
    {
        public EResult m_eResult;
        public ulong m_nPublishedFileId;
    }
    [StructLayout( LayoutKind.Sequential )]
    public struct RemoteStorageEnumerateUserSubscribedFilesResult_t
    {
        public EResult m_eResult;
        public int m_nResultsReturned;
        public int m_nTotalResultCount;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 50, ArraySubType = UnmanagedType.U8)]
        public ulong[] m_rgPublishedFileId; //ulong[50]
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 50, ArraySubType = UnmanagedType.U4)]
        public uint[] m_rgRTimeSubscribed; //uint[50]
    }
    [StructLayout( LayoutKind.Sequential )]
    public struct RemoteStorageUnsubscribePublishedFileResult_t
    {
        public EResult m_eResult;
        public ulong m_nPublishedFileId;
    }
    [StructLayout( LayoutKind.Sequential )]
    public struct RemoteStorageUpdatePublishedFileResult_t
    {
        public EResult m_eResult;
        public ulong m_nPublishedFileId;
        [MarshalAs(UnmanagedType.I1)]
        public bool m_bUserNeedsToAcceptWorkshopLegalAgreement;
    }
    [StructLayout( LayoutKind.Sequential )]
    public struct RemoteStorageDownloadUGCResult_t
    {
        public EResult m_eResult;
        public ulong m_hFile;
        public uint m_nAppID;
        public int m_nSizeInBytes;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
        public string m_pchFileName; //char[260]
        public ulong m_ulSteamIDOwner;
    }
    [StructLayout( LayoutKind.Sequential )]
    public struct RemoteStorageGetPublishedFileDetailsResult_t
    {
        public EResult m_eResult;
        public ulong m_nPublishedFileId;
        public uint m_nCreatorAppID;
        public uint m_nConsumerAppID;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 129)]
        public string m_rgchTitle; //char[129]
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 8000)]
        public string m_rgchDescription; //char[8000]
        public ulong m_hFile;
        public ulong m_hPreviewFile;
        public ulong m_ulSteamIDOwner;
        public uint m_rtimeCreated;
        public uint m_rtimeUpdated;
        public ERemoteStoragePublishedFileVisibility m_eVisibility;
        [MarshalAs(UnmanagedType.I1)]
        public bool m_bBanned;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 1025)]
        public string m_rgchTags; //char[1025]
        [MarshalAs(UnmanagedType.I1)]
        public bool m_bTagsTruncated;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
        public string m_pchFileName; //char[260]
        public int m_nFileSize;
        public int m_nPreviewFileSize;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
        public string m_rgchURL; //char[256]
        public EWorkshopFileType m_eFileType;
        [MarshalAs(UnmanagedType.I1)]
        public bool m_bAcceptedForUse;
    }
    [StructLayout( LayoutKind.Sequential )]
    public struct RemoteStorageEnumerateWorkshopFilesResult_t
    {
        public EResult m_eResult;
        public int m_nResultsReturned;
        public int m_nTotalResultCount;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 50, ArraySubType = UnmanagedType.U8)]
        public ulong[] m_rgPublishedFileId; //ulong[50]
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 50, ArraySubType = UnmanagedType.R4)]
        public float[] m_rgScore; //float[50]
        public uint m_nAppId;
        public uint m_unStartIndex;
    }
    [StructLayout( LayoutKind.Sequential )]
    public struct RemoteStorageGetPublishedItemVoteDetailsResult_t
    {
        public EResult m_eResult;
        public ulong m_unPublishedFileId;
        public int m_nVotesFor;
        public int m_nVotesAgainst;
        public int m_nReports;
        public float m_fScore;
    }
    [StructLayout( LayoutKind.Sequential )]
    public struct RemoteStoragePublishedFileSubscribed_t
    {
        public ulong m_nPublishedFileId;
        public uint m_nAppID;
    }
    [StructLayout( LayoutKind.Sequential )]
    public struct RemoteStoragePublishedFileUnsubscribed_t
    {
        public ulong m_nPublishedFileId;
        public uint m_nAppID;
    }
    [StructLayout( LayoutKind.Sequential )]
    public struct RemoteStoragePublishedFileDeleted_t
    {
        public ulong m_nPublishedFileId;
        public uint m_nAppID;
    }
    [StructLayout( LayoutKind.Sequential )]
    public struct RemoteStorageUpdateUserPublishedItemVoteResult_t
    {
        public EResult m_eResult;
        public ulong m_nPublishedFileId;
    }
    [StructLayout( LayoutKind.Sequential )]
    public struct RemoteStorageUserVoteDetails_t
    {
        public EResult m_eResult;
        public ulong m_nPublishedFileId;
        public EWorkshopVote m_eVote;
    }
    [StructLayout( LayoutKind.Sequential )]
    public struct RemoteStorageEnumerateUserSharedWorkshopFilesResult_t
    {
        public EResult m_eResult;
        public int m_nResultsReturned;
        public int m_nTotalResultCount;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 50, ArraySubType = UnmanagedType.U8)]
        public ulong[] m_rgPublishedFileId; //ulong[50]
    }
    [StructLayout( LayoutKind.Sequential )]
    public struct RemoteStorageSetUserPublishedFileActionResult_t
    {
        public EResult m_eResult;
        public ulong m_nPublishedFileId;
        public EWorkshopFileAction m_eAction;
    }
    [StructLayout( LayoutKind.Sequential )]
    public struct RemoteStorageEnumeratePublishedFilesByUserActionResult_t
    {
        public EResult m_eResult;
        public EWorkshopFileAction m_eAction;
        public int m_nResultsReturned;
        public int m_nTotalResultCount;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 50, ArraySubType = UnmanagedType.U8)]
        public ulong[] m_rgPublishedFileId; //ulong[50]
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 50, ArraySubType = UnmanagedType.U4)]
        public uint[] m_rgRTimeUpdated; //uint[50]
    }
    [StructLayout( LayoutKind.Sequential )]
    public struct RemoteStoragePublishFileProgress_t
    {
        public double m_dPercentFile;
        [MarshalAs(UnmanagedType.I1)]
        public bool m_bPreview;
    }
    [StructLayout( LayoutKind.Sequential )]
    public struct RemoteStoragePublishedFileUpdated_t
    {
        public ulong m_nPublishedFileId;
        public uint m_nAppID;
        public ulong m_ulUnused;
    }
    [StructLayout( LayoutKind.Sequential )]
    public struct RemoteStorageFileWriteAsyncComplete_t
    {
        public EResult m_eResult;
    }
    [StructLayout( LayoutKind.Sequential )]
    public struct RemoteStorageFileReadAsyncComplete_t
    {
        public ulong m_hFileReadAsync;
        public EResult m_eResult;
        public uint m_nOffset;
        public uint m_cubRead;
    }
    [StructLayout( LayoutKind.Sequential )]
    public struct LeaderboardEntry_t
    {
        public ulong m_steamIDUser;
        public int m_nGlobalRank;
        public int m_nScore;
        public int m_cDetails;
        public ulong m_hUGC;
    }
    [StructLayout( LayoutKind.Sequential )]
    public struct UserStatsReceived_t
    {
        public ulong m_nGameID;
        public EResult m_eResult;
        public ulong m_steamIDUser;
    }
    [StructLayout( LayoutKind.Sequential )]
    public struct UserStatsStored_t
    {
        public ulong m_nGameID;
        public EResult m_eResult;
    }
    [StructLayout( LayoutKind.Sequential )]
    public struct UserAchievementStored_t
    {
        public ulong m_nGameID;
        [MarshalAs(UnmanagedType.I1)]
        public bool m_bGroupAchievement;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
        public string m_rgchAchievementName; //char[128]
        public uint m_nCurProgress;
        public uint m_nMaxProgress;
    }
    [StructLayout( LayoutKind.Sequential )]
    public struct LeaderboardFindResult_t
    {
        public ulong m_hSteamLeaderboard;
        public byte m_bLeaderboardFound;
    }
    [StructLayout( LayoutKind.Sequential )]
    public struct LeaderboardScoresDownloaded_t
    {
        public ulong m_hSteamLeaderboard;
        public ulong m_hSteamLeaderboardEntries;
        public int m_cEntryCount;
    }
    [StructLayout( LayoutKind.Sequential )]
    public struct LeaderboardScoreUploaded_t
    {
        public byte m_bSuccess;
        public ulong m_hSteamLeaderboard;
        public int m_nScore;
        public byte m_bScoreChanged;
        public int m_nGlobalRankNew;
        public int m_nGlobalRankPrevious;
    }
    [StructLayout( LayoutKind.Sequential )]
    public struct NumberOfCurrentPlayers_t
    {
        public byte m_bSuccess;
        public int m_cPlayers;
    }
    [StructLayout( LayoutKind.Sequential )]
    public struct UserStatsUnloaded_t
    {
        public ulong m_steamIDUser;
    }
    [StructLayout( LayoutKind.Sequential )]
    public struct UserAchievementIconFetched_t
    {
        public ulong m_nGameID;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
        public string m_rgchAchievementName; //char[128]
        [MarshalAs(UnmanagedType.I1)]
        public bool m_bAchieved;
        public int m_nIconHandle;
    }
    [StructLayout( LayoutKind.Sequential )]
    public struct GlobalAchievementPercentagesReady_t
    {
        public ulong m_nGameID;
        public EResult m_eResult;
    }
    [StructLayout( LayoutKind.Sequential )]
    public struct LeaderboardUGCSet_t
    {
        public EResult m_eResult;
        public ulong m_hSteamLeaderboard;
    }
    [StructLayout( LayoutKind.Sequential )]
    public struct PS3TrophiesInstalled_t
    {
        public ulong m_nGameID;
        public EResult m_eResult;
        public ulong m_ulRequiredDiskSpace;
    }
    [StructLayout( LayoutKind.Sequential )]
    public struct GlobalStatsReceived_t
    {
        public ulong m_nGameID;
        public EResult m_eResult;
    }
    [StructLayout( LayoutKind.Sequential )]
    public struct DlcInstalled_t
    {
        public uint m_nAppID;
    }
    [StructLayout( LayoutKind.Sequential )]
    public struct RegisterActivationCodeResponse_t
    {
        public ERegisterActivationCodeResult m_eResult;
        public uint m_unPackageRegistered;
    }
    [StructLayout( LayoutKind.Sequential )]
    public struct AppProofOfPurchaseKeyResponse_t
    {
        public EResult m_eResult;
        public uint m_nAppID;
        public uint m_cchKeyLength;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 240)]
        public string m_rgchKey; //char[240]
    }
    [StructLayout( LayoutKind.Sequential )]
    public struct P2PSessionState_t
    {
        public byte m_bConnectionActive;
        public byte m_bConnecting;
        public byte m_eP2PSessionError;
        public byte m_bUsingRelay;
        public int m_nBytesQueuedForSend;
        public int m_nPacketsQueuedForSend;
        public uint m_nRemoteIP;
        public char m_nRemotePort;
    }
    [StructLayout( LayoutKind.Sequential )]
    public struct P2PSessionRequest_t
    {
        public ulong m_steamIDRemote;
    }
    [StructLayout( LayoutKind.Sequential )]
    public struct P2PSessionConnectFail_t
    {
        public ulong m_steamIDRemote;
        public byte m_eP2PSessionError;
    }
    [StructLayout( LayoutKind.Sequential )]
    public struct SocketStatusCallback_t
    {
        public uint m_hSocket;
        public uint m_hListenSocket;
        public ulong m_steamIDRemote;
        public int m_eSNetSocketState;
    }
    [StructLayout( LayoutKind.Sequential )]
    public struct ScreenshotReady_t
    {
        public uint m_hLocal;
        public EResult m_eResult;
    }
    [StructLayout( LayoutKind.Sequential )]
    public struct SteamCallback_t
    {
        public float m_flNewVolume;
    }
    [StructLayout( LayoutKind.Sequential )]
    public struct HTTPRequestCompleted_t
    {
        public uint m_hRequest;
        public ulong m_ulContextValue;
        [MarshalAs(UnmanagedType.I1)]
        public bool m_bRequestSuccessful;
        public EHTTPStatusCode m_eStatusCode;
        public uint m_unBodySize;
    }
    [StructLayout( LayoutKind.Sequential )]
    public struct HTTPRequestHeadersReceived_t
    {
        public uint m_hRequest;
        public ulong m_ulContextValue;
    }
    [StructLayout( LayoutKind.Sequential )]
    public struct HTTPRequestDataReceived_t
    {
        public uint m_hRequest;
        public ulong m_ulContextValue;
        public uint m_cOffset;
        public uint m_cBytesReceived;
    }
    [StructLayout( LayoutKind.Sequential )]
    public struct SteamUnifiedMessagesSendMethodResult_t
    {
        public ulong m_hHandle;
        public ulong m_unContext;
        public EResult m_eResult;
        public uint m_unResponseSize;
    }
    [StructLayout( LayoutKind.Sequential )]
    public struct ControllerAnalogActionData_t
    {
        public EControllerSourceMode eMode;
        public float x;
        public float y;
        [MarshalAs(UnmanagedType.I1)]
        public bool bActive;
    }
    [StructLayout( LayoutKind.Sequential )]
    public struct ControllerDigitalActionData_t
    {
        [MarshalAs(UnmanagedType.I1)]
        public bool bState;
        [MarshalAs(UnmanagedType.I1)]
        public bool bActive;
    }
    [StructLayout( LayoutKind.Sequential )]
    public struct SteamUGCDetails_t
    {
        public ulong m_nPublishedFileId;
        public EResult m_eResult;
        public EWorkshopFileType m_eFileType;
        public uint m_nCreatorAppID;
        public uint m_nConsumerAppID;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 129)]
        public string m_rgchTitle; //char[129]
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 8000)]
        public string m_rgchDescription; //char[8000]
        public ulong m_ulSteamIDOwner;
        public uint m_rtimeCreated;
        public uint m_rtimeUpdated;
        public uint m_rtimeAddedToUserList;
        public ERemoteStoragePublishedFileVisibility m_eVisibility;
        [MarshalAs(UnmanagedType.I1)]
        public bool m_bBanned;
        [MarshalAs(UnmanagedType.I1)]
        public bool m_bAcceptedForUse;
        [MarshalAs(UnmanagedType.I1)]
        public bool m_bTagsTruncated;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 1025)]
        public string m_rgchTags; //char[1025]
        public ulong m_hFile;
        public ulong m_hPreviewFile;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
        public string m_pchFileName; //char[260]
        public int m_nFileSize;
        public int m_nPreviewFileSize;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
        public string m_rgchURL; //char[256]
        public uint m_unVotesUp;
        public uint m_unVotesDown;
        public float m_flScore;
        public uint m_unNumChildren;
    }
    [StructLayout( LayoutKind.Sequential )]
    public struct SteamUGCQueryCompleted_t
    {
        public ulong m_handle;
        public EResult m_eResult;
        public uint m_unNumResultsReturned;
        public uint m_unTotalMatchingResults;
        [MarshalAs(UnmanagedType.I1)]
        public bool m_bCachedData;
    }
    [StructLayout( LayoutKind.Sequential )]
    public struct SteamUGCRequestUGCDetailsResult_t
    {
        public SteamUGCDetails_t m_details;
        [MarshalAs(UnmanagedType.I1)]
        public bool m_bCachedData;
    }
    [StructLayout( LayoutKind.Sequential )]
    public struct CreateItemResult_t
    {
        public EResult m_eResult;
        public ulong m_nPublishedFileId;
        [MarshalAs(UnmanagedType.I1)]
        public bool m_bUserNeedsToAcceptWorkshopLegalAgreement;
    }
    [StructLayout( LayoutKind.Sequential )]
    public struct SubmitItemUpdateResult_t
    {
        public EResult m_eResult;
        [MarshalAs(UnmanagedType.I1)]
        public bool m_bUserNeedsToAcceptWorkshopLegalAgreement;
    }
    [StructLayout( LayoutKind.Sequential )]
    public struct DownloadItemResult_t
    {
        public uint m_unAppID;
        public ulong m_nPublishedFileId;
        public EResult m_eResult;
    }
    [StructLayout( LayoutKind.Sequential )]
    public struct UserFavoriteItemsListChanged_t
    {
        public ulong m_nPublishedFileId;
        public EResult m_eResult;
        [MarshalAs(UnmanagedType.I1)]
        public bool m_bWasAddRequest;
    }
    [StructLayout( LayoutKind.Sequential )]
    public struct SetUserItemVoteResult_t
    {
        public ulong m_nPublishedFileId;
        public EResult m_eResult;
        [MarshalAs(UnmanagedType.I1)]
        public bool m_bVoteUp;
    }
    [StructLayout( LayoutKind.Sequential )]
    public struct GetUserItemVoteResult_t
    {
        public ulong m_nPublishedFileId;
        public EResult m_eResult;
        [MarshalAs(UnmanagedType.I1)]
        public bool m_bVotedUp;
        [MarshalAs(UnmanagedType.I1)]
        public bool m_bVotedDown;
        [MarshalAs(UnmanagedType.I1)]
        public bool m_bVoteSkipped;
    }
    [StructLayout( LayoutKind.Sequential )]
    public struct HTML_BrowserReady_t
    {
        public uint unBrowserHandle;
    }
    [StructLayout( LayoutKind.Sequential )]
    public struct HTML_NeedsPaint_t
    {
        public uint unBrowserHandle;
        public IntPtr pBGRA; // const char *
        public uint unWide;
        public uint unTall;
        public uint unUpdateX;
        public uint unUpdateY;
        public uint unUpdateWide;
        public uint unUpdateTall;
        public uint unScrollX;
        public uint unScrollY;
        public float flPageScale;
        public uint unPageSerial;
    }
    [StructLayout( LayoutKind.Sequential )]
    public struct HTML_StartRequest_t
    {
        public uint unBrowserHandle;
        public IntPtr pchURL; // const char *
        public IntPtr pchTarget; // const char *
        public IntPtr pchPostData; // const char *
        [MarshalAs(UnmanagedType.I1)]
        public bool bIsRedirect;
    }
    [StructLayout( LayoutKind.Sequential )]
    public struct HTML_CloseBrowser_t
    {
        public uint unBrowserHandle;
    }
    [StructLayout( LayoutKind.Sequential )]
    public struct HTML_URLChanged_t
    {
        public uint unBrowserHandle;
        public IntPtr pchURL; // const char *
        public IntPtr pchPostData; // const char *
        [MarshalAs(UnmanagedType.I1)]
        public bool bIsRedirect;
        public IntPtr pchPageTitle; // const char *
        [MarshalAs(UnmanagedType.I1)]
        public bool bNewNavigation;
    }
    [StructLayout( LayoutKind.Sequential )]
    public struct HTML_FinishedRequest_t
    {
        public uint unBrowserHandle;
        public IntPtr pchURL; // const char *
        public IntPtr pchPageTitle; // const char *
    }
    [StructLayout( LayoutKind.Sequential )]
    public struct HTML_OpenLinkInNewTab_t
    {
        public uint unBrowserHandle;
        public IntPtr pchURL; // const char *
    }
    [StructLayout( LayoutKind.Sequential )]
    public struct HTML_ChangedTitle_t
    {
        public uint unBrowserHandle;
        public IntPtr pchTitle; // const char *
    }
    [StructLayout( LayoutKind.Sequential )]
    public struct HTML_SearchResults_t
    {
        public uint unBrowserHandle;
        public uint unResults;
        public uint unCurrentMatch;
    }
    [StructLayout( LayoutKind.Sequential )]
    public struct HTML_CanGoBackAndForward_t
    {
        public uint unBrowserHandle;
        [MarshalAs(UnmanagedType.I1)]
        public bool bCanGoBack;
        [MarshalAs(UnmanagedType.I1)]
        public bool bCanGoForward;
    }
    [StructLayout( LayoutKind.Sequential )]
    public struct HTML_HorizontalScroll_t
    {
        public uint unBrowserHandle;
        public uint unScrollMax;
        public uint unScrollCurrent;
        public float flPageScale;
        [MarshalAs(UnmanagedType.I1)]
        public bool bVisible;
        public uint unPageSize;
    }
    [StructLayout( LayoutKind.Sequential )]
    public struct HTML_VerticalScroll_t
    {
        public uint unBrowserHandle;
        public uint unScrollMax;
        public uint unScrollCurrent;
        public float flPageScale;
        [MarshalAs(UnmanagedType.I1)]
        public bool bVisible;
        public uint unPageSize;
    }
    [StructLayout( LayoutKind.Sequential )]
    public struct HTML_LinkAtPosition_t
    {
        public uint unBrowserHandle;
        public uint x;
        public uint y;
        public IntPtr pchURL; // const char *
        [MarshalAs(UnmanagedType.I1)]
        public bool bInput;
        [MarshalAs(UnmanagedType.I1)]
        public bool bLiveLink;
    }
    [StructLayout( LayoutKind.Sequential )]
    public struct HTML_JSAlert_t
    {
        public uint unBrowserHandle;
        public IntPtr pchMessage; // const char *
    }
    [StructLayout( LayoutKind.Sequential )]
    public struct HTML_JSConfirm_t
    {
        public uint unBrowserHandle;
        public IntPtr pchMessage; // const char *
    }
    [StructLayout( LayoutKind.Sequential )]
    public struct HTML_FileOpenDialog_t
    {
        public uint unBrowserHandle;
        public IntPtr pchTitle; // const char *
        public IntPtr pchInitialFile; // const char *
    }
    [StructLayout( LayoutKind.Sequential )]
    public struct HTML_NewWindow_t
    {
        public uint unBrowserHandle;
        public IntPtr pchURL; // const char *
        public uint unX;
        public uint unY;
        public uint unWide;
        public uint unTall;
        public uint unNewWindow_BrowserHandle;
    }
    [StructLayout( LayoutKind.Sequential )]
    public struct HTML_SetCursor_t
    {
        public uint unBrowserHandle;
        public uint eMouseCursor;
    }
    [StructLayout( LayoutKind.Sequential )]
    public struct HTML_StatusText_t
    {
        public uint unBrowserHandle;
        public IntPtr pchMsg; // const char *
    }
    [StructLayout( LayoutKind.Sequential )]
    public struct HTML_ShowToolTip_t
    {
        public uint unBrowserHandle;
        public IntPtr pchMsg; // const char *
    }
    [StructLayout( LayoutKind.Sequential )]
    public struct HTML_UpdateToolTip_t
    {
        public uint unBrowserHandle;
        public IntPtr pchMsg; // const char *
    }
    [StructLayout( LayoutKind.Sequential )]
    public struct HTML_HideToolTip_t
    {
        public uint unBrowserHandle;
    }
    [StructLayout( LayoutKind.Sequential )]
    public struct SteamItemDetails_t
    {
        public ulong m_itemId;
        public int m_iDefinition;
        public char m_unQuantity;
        public char m_unFlags;
    }
    [StructLayout( LayoutKind.Sequential )]
    public struct SteamInventoryResultReady_t
    {
        public int m_handle;
        public EResult m_result;
    }
    [StructLayout( LayoutKind.Sequential )]
    public struct SteamInventoryFullUpdate_t
    {
        public int m_handle;
    }
    [StructLayout( LayoutKind.Sequential )]
    public struct CSteamAPIContext
    {
        public IntPtr m_pSteamClient; // class ISteamClient *
        public IntPtr m_pSteamUser; // class ISteamUser *
        public IntPtr m_pSteamFriends; // class ISteamFriends *
        public IntPtr m_pSteamUtils; // class ISteamUtils *
        public IntPtr m_pSteamMatchmaking; // class ISteamMatchmaking *
        public IntPtr m_pSteamUserStats; // class ISteamUserStats *
        public IntPtr m_pSteamApps; // class ISteamApps *
        public IntPtr m_pSteamMatchmakingServers; // class ISteamMatchmakingServers *
        public IntPtr m_pSteamNetworking; // class ISteamNetworking *
        public IntPtr m_pSteamRemoteStorage; // class ISteamRemoteStorage *
        public IntPtr m_pSteamScreenshots; // class ISteamScreenshots *
        public IntPtr m_pSteamHTTP; // class ISteamHTTP *
        public IntPtr m_pSteamUnifiedMessages; // class ISteamUnifiedMessages *
        public IntPtr m_pController; // class ISteamController *
        public IntPtr m_pSteamUGC; // class ISteamUGC *
        public IntPtr m_pSteamAppList; // class ISteamAppList *
        public IntPtr m_pSteamMusic; // class ISteamMusic *
        public IntPtr m_pSteamMusicRemote; // class ISteamMusicRemote *
        public IntPtr m_pSteamHTMLSurface; // class ISteamHTMLSurface *
        public IntPtr m_pSteamInventory; // class ISteamInventory *
        public IntPtr m_pSteamVideo; // class ISteamVideo *
    }
    [StructLayout( LayoutKind.Sequential )]
    public struct CCallbackBase
    {
        public byte m_nCallbackFlags;
        public int m_iCallback;
    }
    [StructLayout( LayoutKind.Sequential )]
    public struct CCallResult
    {
        public ulong m_hAPICall;
        public IntPtr m_pObj; // T *
        public IntPtr m_Func;
    }
    [StructLayout( LayoutKind.Sequential )]
    public struct CCallback
    {
        public IntPtr m_pObj; // T *
        public IntPtr m_Func;
    }
    [StructLayout( LayoutKind.Sequential )]
    public struct GSClientApprove_t
    {
        public ulong m_SteamID;
        public ulong m_OwnerSteamID;
    }
    [StructLayout( LayoutKind.Sequential )]
    public struct GSClientDeny_t
    {
        public ulong m_SteamID;
        public EDenyReason m_eDenyReason;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
        public string m_rgchOptionalText; //char[128]
    }
    [StructLayout( LayoutKind.Sequential )]
    public struct GSClientKick_t
    {
        public ulong m_SteamID;
        public EDenyReason m_eDenyReason;
    }
    [StructLayout( LayoutKind.Sequential )]
    public struct GSClientAchievementStatus_t
    {
        public ulong m_SteamID;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
        public string m_pchAchievement; //char[128]
        [MarshalAs(UnmanagedType.I1)]
        public bool m_bUnlocked;
    }
    [StructLayout( LayoutKind.Sequential )]
    public struct GSPolicyResponse_t
    {
        public byte m_bSecure;
    }
    [StructLayout( LayoutKind.Sequential )]
    public struct GSGameplayStats_t
    {
        public EResult m_eResult;
        public int m_nRank;
        public uint m_unTotalConnects;
        public uint m_unTotalMinutesPlayed;
    }
    [StructLayout( LayoutKind.Sequential )]
    public struct GSClientGroupStatus_t
    {
        public ulong m_SteamIDUser;
        public ulong m_SteamIDGroup;
        [MarshalAs(UnmanagedType.I1)]
        public bool m_bMember;
        [MarshalAs(UnmanagedType.I1)]
        public bool m_bOfficer;
    }
    [StructLayout( LayoutKind.Sequential )]
    public struct GSReputation_t
    {
        public EResult m_eResult;
        public uint m_unReputationScore;
        [MarshalAs(UnmanagedType.I1)]
        public bool m_bBanned;
        public uint m_unBannedIP;
        public char m_usBannedPort;
        public ulong m_ulBannedGameID;
        public uint m_unBanExpires;
    }
    [StructLayout( LayoutKind.Sequential )]
    public struct AssociateWithClanResult_t
    {
        public EResult m_eResult;
    }
    [StructLayout( LayoutKind.Sequential )]
    public struct ComputeNewPlayerCompatibilityResult_t
    {
        public EResult m_eResult;
        public int m_cPlayersThatDontLikeCandidate;
        public int m_cPlayersThatCandidateDoesntLike;
        public int m_cClanPlayersThatDontLikeCandidate;
        public ulong m_SteamIDCandidate;
    }
    [StructLayout( LayoutKind.Sequential )]
    public struct GSStatsReceived_t
    {
        public EResult m_eResult;
        public ulong m_steamIDUser;
    }
    [StructLayout( LayoutKind.Sequential )]
    public struct GSStatsStored_t
    {
        public EResult m_eResult;
        public ulong m_steamIDUser;
    }
    [StructLayout( LayoutKind.Sequential )]
    public struct GSStatsUnloaded_t
    {
        public ulong m_steamIDUser;
    }

    internal class SteamAPI
    {
        public static void Init( uint appId )
        {
            SteamAPIInterop.SteamAPI_RestartAppIfNecessary( appId );
            SteamAPIInterop.SteamAPI_Init();
        }

        public static void RunCallbacks()
        {
            SteamAPIInterop.SteamAPI_RunCallbacks();
        }

        public static void RegisterCallback( IntPtr pCallback, int iCallback )
        {
            SteamAPIInterop.SteamAPI_RegisterCallback( pCallback, iCallback );
        }

        public static void UnregisterCallback( IntPtr pCallback )
        {
            SteamAPIInterop.SteamAPI_UnregisterCallback( pCallback );
        }

        public const int k_iSteamUserCallbacks = 100;
        public const int k_iSteamGameServerCallbacks = 200;
        public const int k_iSteamFriendsCallbacks = 300;
        public const int k_iSteamBillingCallbacks = 400;
        public const int k_iSteamMatchmakingCallbacks = 500;
        public const int k_iSteamContentServerCallbacks = 600;
        public const int k_iSteamUtilsCallbacks = 700;
        public const int k_iClientFriendsCallbacks = 800;
        public const int k_iClientUserCallbacks = 900;
        public const int k_iSteamAppsCallbacks = 1000;
        public const int k_iSteamUserStatsCallbacks = 1100;
        public const int k_iSteamNetworkingCallbacks = 1200;
        public const int k_iClientRemoteStorageCallbacks = 1300;
        public const int k_iClientDepotBuilderCallbacks = 1400;
        public const int k_iSteamGameServerItemsCallbacks = 1500;
        public const int k_iClientUtilsCallbacks = 1600;
        public const int k_iSteamGameCoordinatorCallbacks = 1700;
        public const int k_iSteamGameServerStatsCallbacks = 1800;
        public const int k_iSteam2AsyncCallbacks = 1900;
        public const int k_iSteamGameStatsCallbacks = 2000;
        public const int k_iClientHTTPCallbacks = 2100;
        public const int k_iClientScreenshotsCallbacks = 2200;
        public const int k_iSteamScreenshotsCallbacks = 2300;
        public const int k_iClientAudioCallbacks = 2400;
        public const int k_iClientUnifiedMessagesCallbacks = 2500;
        public const int k_iSteamStreamLauncherCallbacks = 2600;
        public const int k_iClientControllerCallbacks = 2700;
        public const int k_iSteamControllerCallbacks = 2800;
        public const int k_iClientParentalSettingsCallbacks = 2900;
        public const int k_iClientDeviceAuthCallbacks = 3000;
        public const int k_iClientNetworkDeviceManagerCallbacks = 3100;
        public const int k_iClientMusicCallbacks = 3200;
        public const int k_iClientRemoteClientManagerCallbacks = 3300;
        public const int k_iClientUGCCallbacks = 3400;
        public const int k_iSteamStreamClientCallbacks = 3500;
        public const int k_IClientProductBuilderCallbacks = 3600;
        public const int k_iClientShortcutsCallbacks = 3700;
        public const int k_iClientRemoteControlManagerCallbacks = 3800;
        public const int k_iSteamAppListCallbacks = 3900;
        public const int k_iSteamMusicCallbacks = 4000;
        public const int k_iSteamMusicRemoteCallbacks = 4100;
        public const int k_iClientVRCallbacks = 4200;
        public const int k_iClientGameNotificationCallbacks = 4300;
        public const int k_iSteamGameNotificationCallbacks = 4400;
        public const int k_iSteamHTMLSurfaceCallbacks = 4500;
        public const int k_iClientVideoCallbacks = 4600;
        public const int k_iClientInventoryCallbacks = 4700;
        public const int k_iClientBluetoothManagerCallbacks = 4800;
        public const int k_cchPersonaNameMax = 128;
        public const int k_cwchPersonaNameMax = 32;
        public const int k_cchMaxRichPresenceKeys = 20;
        public const int k_cchMaxRichPresenceKeyLength = 64;
        public const int k_cchMaxRichPresenceValueLength = 256;
        public const int k_cchStatNameMax = 128;
        public const int k_cchLeaderboardNameMax = 128;
        public const int k_cLeaderboardDetailsMax = 64;
        public const ulong k_InvalidUnifiedMessageHandle = 0;
        public const ulong k_SteamItemInstanceIDInvalid = 0xffffffffffffffff;
        public const int k_SteamInventoryResultInvalid = -1;
        public static ISteamClient SteamClient()
        {
            return new CSteamClient( SteamAPIInterop.SteamClient() );
        }

        public static ISteamUser SteamUser()
        {
            return new CSteamUser( SteamAPIInterop.SteamUser() );
        }

        public static ISteamFriends SteamFriends()
        {
            return new CSteamFriends( SteamAPIInterop.SteamFriends() );
        }

        public static ISteamUtils SteamUtils()
        {
            return new CSteamUtils( SteamAPIInterop.SteamUtils() );
        }

        public static ISteamMatchmaking SteamMatchmaking()
        {
            return new CSteamMatchmaking( SteamAPIInterop.SteamMatchmaking() );
        }

        public static ISteamMatchmakingServerListResponse SteamMatchmakingServerListResponse()
        {
            return new CSteamMatchmakingServerListResponse( SteamAPIInterop.SteamMatchmakingServerListResponse() );
        }

        public static ISteamMatchmakingPingResponse SteamMatchmakingPingResponse()
        {
            return new CSteamMatchmakingPingResponse( SteamAPIInterop.SteamMatchmakingPingResponse() );
        }

        public static ISteamMatchmakingPlayersResponse SteamMatchmakingPlayersResponse()
        {
            return new CSteamMatchmakingPlayersResponse( SteamAPIInterop.SteamMatchmakingPlayersResponse() );
        }

        public static ISteamMatchmakingRulesResponse SteamMatchmakingRulesResponse()
        {
            return new CSteamMatchmakingRulesResponse( SteamAPIInterop.SteamMatchmakingRulesResponse() );
        }

        public static ISteamMatchmakingServers SteamMatchmakingServers()
        {
            return new CSteamMatchmakingServers( SteamAPIInterop.SteamMatchmakingServers() );
        }

        public static ISteamRemoteStorage SteamRemoteStorage()
        {
            return new CSteamRemoteStorage( SteamAPIInterop.SteamRemoteStorage() );
        }

        public static ISteamUserStats SteamUserStats()
        {
            return new CSteamUserStats( SteamAPIInterop.SteamUserStats() );
        }

        public static ISteamApps SteamApps()
        {
            return new CSteamApps( SteamAPIInterop.SteamApps() );
        }

        public static ISteamNetworking SteamNetworking()
        {
            return new CSteamNetworking( SteamAPIInterop.SteamNetworking() );
        }

        public static ISteamScreenshots SteamScreenshots()
        {
            return new CSteamScreenshots( SteamAPIInterop.SteamScreenshots() );
        }

        public static ISteamMusic SteamMusic()
        {
            return new CSteamMusic( SteamAPIInterop.SteamMusic() );
        }

        public static ISteamMusicRemote SteamMusicRemote()
        {
            return new CSteamMusicRemote( SteamAPIInterop.SteamMusicRemote() );
        }

        public static ISteamHTTP SteamHTTP()
        {
            return new CSteamHTTP( SteamAPIInterop.SteamHTTP() );
        }

        public static ISteamUnifiedMessages SteamUnifiedMessages()
        {
            return new CSteamUnifiedMessages( SteamAPIInterop.SteamUnifiedMessages() );
        }

        public static ISteamController SteamController()
        {
            return new CSteamController( SteamAPIInterop.SteamController() );
        }

        public static ISteamUGC SteamUGC()
        {
            return new CSteamUGC( SteamAPIInterop.SteamUGC() );
        }

        public static ISteamAppList SteamAppList()
        {
            return new CSteamAppList( SteamAPIInterop.SteamAppList() );
        }

        public static ISteamHTMLSurface SteamHTMLSurface()
        {
            return new CSteamHTMLSurface( SteamAPIInterop.SteamHTMLSurface() );
        }

        public static ISteamInventory SteamInventory()
        {
            return new CSteamInventory( SteamAPIInterop.SteamInventory() );
        }

        public static ISteamVideo SteamVideo()
        {
            return new CSteamVideo( SteamAPIInterop.SteamVideo() );
        }

        public static ISteamGameServer SteamGameServer()
        {
            return new CSteamGameServer( SteamAPIInterop.SteamGameServer() );
        }

        public static ISteamGameServerStats SteamGameServerStats()
        {
            return new CSteamGameServerStats( SteamAPIInterop.SteamGameServerStats() );
        }

    }



}

