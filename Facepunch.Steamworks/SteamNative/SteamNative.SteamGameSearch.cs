using System;
using System.Runtime.InteropServices;
using System.Linq;

namespace SteamNative
{
	internal unsafe class SteamGameSearch : IDisposable
	{
		//
		// Holds a platform specific implentation
		//
		internal Platform.Interface platform;
		internal Facepunch.Steamworks.BaseSteamworks steamworks;
		
		//
		// Constructor decides which implementation to use based on current platform
		//
		internal SteamGameSearch( Facepunch.Steamworks.BaseSteamworks steamworks, IntPtr pointer )
		{
			this.steamworks = steamworks;
			
			if ( Platform.IsWindows ) platform = new Platform.Windows( pointer );
			else if ( Platform.IsLinux ) platform = new Platform.Linux( pointer );
			else if ( Platform.IsOsx ) platform = new Platform.Mac( pointer );
		}
		
		//
		// Class is invalid if we don't have a valid implementation
		//
		public bool IsValid => platform != null && platform.IsValid;
		
		//
		// When shutting down clear all the internals to avoid accidental use
		//
		public virtual void Dispose()
		{
			 if ( platform != null )
			{
				platform.Dispose();
				platform = null;
			}
		}
		
		// GameSearchErrorCode_t
		public GameSearchErrorCode_t AcceptGame()
		{
			return platform.ISteamGameSearch_AcceptGame();
		}
		
		// GameSearchErrorCode_t
		public GameSearchErrorCode_t AddGameSearchParams( string pchKeyToFind /*const char **/, string pchValuesToFind /*const char **/ )
		{
			return platform.ISteamGameSearch_AddGameSearchParams( pchKeyToFind, pchValuesToFind );
		}
		
		// GameSearchErrorCode_t
		public GameSearchErrorCode_t CancelRequestPlayersForGame()
		{
			return platform.ISteamGameSearch_CancelRequestPlayersForGame();
		}
		
		// GameSearchErrorCode_t
		public GameSearchErrorCode_t DeclineGame()
		{
			return platform.ISteamGameSearch_DeclineGame();
		}
		
		// GameSearchErrorCode_t
		public GameSearchErrorCode_t EndGame( ulong ullUniqueGameID /*uint64*/ )
		{
			return platform.ISteamGameSearch_EndGame( ullUniqueGameID );
		}
		
		// GameSearchErrorCode_t
		public GameSearchErrorCode_t EndGameSearch()
		{
			return platform.ISteamGameSearch_EndGameSearch();
		}
		
		// GameSearchErrorCode_t
		public GameSearchErrorCode_t HostConfirmGameStart( ulong ullUniqueGameID /*uint64*/ )
		{
			return platform.ISteamGameSearch_HostConfirmGameStart( ullUniqueGameID );
		}
		
		// GameSearchErrorCode_t
		public GameSearchErrorCode_t RequestPlayersForGame( int nPlayerMin /*int*/, int nPlayerMax /*int*/, int nMaxTeamSize /*int*/ )
		{
			return platform.ISteamGameSearch_RequestPlayersForGame( nPlayerMin, nPlayerMax, nMaxTeamSize );
		}
		
		// GameSearchErrorCode_t
		// with: Detect_StringFetch False
		public GameSearchErrorCode_t RetrieveConnectionDetails( CSteamID steamIDHost /*class CSteamID*/, out string pchConnectionDetails /*char **/ )
		{
			GameSearchErrorCode_t bSuccess = default( GameSearchErrorCode_t );
			pchConnectionDetails = string.Empty;
			System.Text.StringBuilder pchConnectionDetails_sb = Helpers.TakeStringBuilder();
			int cubConnectionDetails = 4096;
			bSuccess = platform.ISteamGameSearch_RetrieveConnectionDetails( steamIDHost.Value, pchConnectionDetails_sb, cubConnectionDetails );
			pchConnectionDetails = pchConnectionDetails_sb.ToString();
			return bSuccess;
		}
		
		// GameSearchErrorCode_t
		public GameSearchErrorCode_t SearchForGameSolo( int nPlayerMin /*int*/, int nPlayerMax /*int*/ )
		{
			return platform.ISteamGameSearch_SearchForGameSolo( nPlayerMin, nPlayerMax );
		}
		
		// GameSearchErrorCode_t
		public GameSearchErrorCode_t SearchForGameWithLobby( CSteamID steamIDLobby /*class CSteamID*/, int nPlayerMin /*int*/, int nPlayerMax /*int*/ )
		{
			return platform.ISteamGameSearch_SearchForGameWithLobby( steamIDLobby.Value, nPlayerMin, nPlayerMax );
		}
		
		// GameSearchErrorCode_t
		public GameSearchErrorCode_t SetConnectionDetails( string pchConnectionDetails /*const char **/, int cubConnectionDetails /*int*/ )
		{
			return platform.ISteamGameSearch_SetConnectionDetails( pchConnectionDetails, cubConnectionDetails );
		}
		
		// GameSearchErrorCode_t
		public GameSearchErrorCode_t SetGameHostParams( string pchKey /*const char **/, string pchValue /*const char **/ )
		{
			return platform.ISteamGameSearch_SetGameHostParams( pchKey, pchValue );
		}
		
		// GameSearchErrorCode_t
		public GameSearchErrorCode_t SubmitPlayerResult( ulong ullUniqueGameID /*uint64*/, CSteamID steamIDPlayer /*class CSteamID*/, PlayerResult_t EPlayerResult /*EPlayerResult_t*/ )
		{
			return platform.ISteamGameSearch_SubmitPlayerResult( ullUniqueGameID, steamIDPlayer.Value, EPlayerResult );
		}
		
	}
}
