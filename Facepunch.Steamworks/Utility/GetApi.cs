using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;

namespace Steamworks
{
	internal static class GetApi
	{
		[DllImport( Platform.LibraryName, EntryPoint = "SteamClient" )]
		internal static extern IntPtr SteamClient();
		[DllImport( Platform.LibraryName, EntryPoint = "SteamUser" )]
		internal static extern IntPtr SteamUser();
		[DllImport( Platform.LibraryName, EntryPoint = "SteamFriends" )]
		internal static extern IntPtr SteamFriends();
		[DllImport( Platform.LibraryName, EntryPoint = "SteamUtils" )]
		internal static extern IntPtr SteamUtils();
		[DllImport( Platform.LibraryName, EntryPoint = "SteamMatchmaking" )]
		internal static extern IntPtr SteamMatchmaking();
		[DllImport( Platform.LibraryName, EntryPoint = "SteamMatchmakingServerListResponse" )]
		internal static extern IntPtr SteamMatchmakingServerListResponse();
		[DllImport( Platform.LibraryName, EntryPoint = "SteamMatchmakingPingResponse" )]
		internal static extern IntPtr SteamMatchmakingPingResponse();
		[DllImport( Platform.LibraryName, EntryPoint = "SteamMatchmakingPlayersResponse" )]
		internal static extern IntPtr SteamMatchmakingPlayersResponse();
		[DllImport( Platform.LibraryName, EntryPoint = "SteamMatchmakingRulesResponse" )]
		internal static extern IntPtr SteamMatchmakingRulesResponse();
		[DllImport( Platform.LibraryName, EntryPoint = "SteamMatchmakingServers" )]
		internal static extern IntPtr SteamMatchmakingServers();
		[DllImport( Platform.LibraryName, EntryPoint = "SteamGameSearch" )]
		internal static extern IntPtr SteamGameSearch();
		[DllImport( Platform.LibraryName, EntryPoint = "SteamParties" )]
		internal static extern IntPtr SteamParties();
		[DllImport( Platform.LibraryName, EntryPoint = "SteamRemoteStorage" )]
		internal static extern IntPtr SteamRemoteStorage();
		[DllImport( Platform.LibraryName, EntryPoint = "SteamUserStats" )]
		internal static extern IntPtr SteamUserStats();
		[DllImport( Platform.LibraryName, EntryPoint = "SteamApps" )]
		internal static extern IntPtr SteamApps();
		[DllImport( Platform.LibraryName, EntryPoint = "SteamNetworking" )]
		internal static extern IntPtr SteamNetworking();
		[DllImport( Platform.LibraryName, EntryPoint = "SteamScreenshots" )]
		internal static extern IntPtr SteamScreenshots();
		[DllImport( Platform.LibraryName, EntryPoint = "SteamMusic" )]
		internal static extern IntPtr SteamMusic();
		[DllImport( Platform.LibraryName, EntryPoint = "SteamMusicRemote" )]
		internal static extern IntPtr SteamMusicRemote();
		[DllImport( Platform.LibraryName, EntryPoint = "SteamHTTP" )]
		internal static extern IntPtr SteamHTTP();
		[DllImport( Platform.LibraryName, EntryPoint = "SteamInput" )]
		internal static extern IntPtr SteamInput();
		[DllImport( Platform.LibraryName, EntryPoint = "SteamController" )]
		internal static extern IntPtr SteamController();
		[DllImport( Platform.LibraryName, EntryPoint = "SteamUGC" )]
		internal static extern IntPtr SteamUGC();
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAppList" )]
		internal static extern IntPtr SteamAppList();
		[DllImport( Platform.LibraryName, EntryPoint = "SteamHTMLSurface" )]
		internal static extern IntPtr SteamHTMLSurface();
		[DllImport( Platform.LibraryName, EntryPoint = "SteamInventory" )]
		internal static extern IntPtr SteamInventory();
		[DllImport( Platform.LibraryName, EntryPoint = "SteamVideo" )]
		internal static extern IntPtr SteamVideo();
		[DllImport( Platform.LibraryName, EntryPoint = "SteamParentalSettings" )]
		internal static extern IntPtr SteamParentalSettings();
		[DllImport( Platform.LibraryName, EntryPoint = "SteamGameServer" )]
		internal static extern IntPtr SteamGameServer();
		[DllImport( Platform.LibraryName, EntryPoint = "SteamGameServerStats" )]
		internal static extern IntPtr SteamGameServerStats();		
		
		//
		// NOTE: Stubs - these don't exist
		//
		[DllImport( Platform.LibraryName, EntryPoint = "SteamNetworkingSockets" )]
		internal static extern IntPtr SteamNetworkingSockets();		
		[DllImport( Platform.LibraryName, EntryPoint = "SteamNetworkingUtils" )]
		internal static extern IntPtr SteamNetworkingUtils();
	}
}