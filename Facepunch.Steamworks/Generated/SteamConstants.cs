using System;
using System.Runtime.InteropServices;
using System.Linq;
using Steamworks.Data;
using System.Threading.Tasks;

namespace Steamworks.Data
{
	internal static class CallbackIdentifiers
	{
		public const int SteamUser = 100;
		public const int SteamGameServer = 200;
		public const int SteamFriends = 300;
		public const int SteamBilling = 400;
		public const int SteamMatchmaking = 500;
		public const int SteamContentServer = 600;
		public const int SteamUtils = 700;
		public const int ClientFriends = 800;
		public const int ClientUser = 900;
		public const int SteamApps = 1000;
		public const int SteamUserStats = 1100;
		public const int SteamNetworking = 1200;
		public const int SteamNetworkingSockets = 1220;
		public const int SteamNetworkingMessages = 1250;
		public const int ClientRemoteStorage = 1300;
		public const int ClientDepotBuilder = 1400;
		public const int SteamGameServerItems = 1500;
		public const int ClientUtils = 1600;
		public const int SteamGameCoordinator = 1700;
		public const int SteamGameServerStats = 1800;
		public const int Steam2Async = 1900;
		public const int SteamGameStats = 2000;
		public const int ClientHTTP = 2100;
		public const int ClientScreenshots = 2200;
		public const int SteamScreenshots = 2300;
		public const int ClientAudio = 2400;
		public const int ClientUnifiedMessages = 2500;
		public const int SteamStreamLauncher = 2600;
		public const int ClientController = 2700;
		public const int SteamController = 2800;
		public const int ClientParentalSettings = 2900;
		public const int ClientDeviceAuth = 3000;
		public const int ClientNetworkDeviceManager = 3100;
		public const int ClientMusic = 3200;
		public const int ClientRemoteClientManager = 3300;
		public const int ClientUGC = 3400;
		public const int SteamStreamClient = 3500;
		public const int ClientProductBuilder = 3600;
		public const int ClientShortcuts = 3700;
		public const int ClientRemoteControlManager = 3800;
		public const int SteamAppList = 3900;
		public const int SteamMusic = 4000;
		public const int SteamMusicRemote = 4100;
		public const int ClientVR = 4200;
		public const int ClientGameNotification = 4300;
		public const int SteamGameNotification = 4400;
		public const int SteamHTMLSurface = 4500;
		public const int ClientVideo = 4600;
		public const int ClientInventory = 4700;
		public const int ClientBluetoothManager = 4800;
		public const int ClientSharedConnection = 4900;
		public const int SteamParentalSettings = 5000;
		public const int ClientShader = 5100;
		public const int SteamGameSearch = 5200;
		public const int SteamParties = 5300;
		public const int ClientParties = 5400;
	}
	internal static class Defines
	{
		internal const string STEAMAPPLIST_INTERFACE_VERSION = "STEAMAPPLIST_INTERFACE_VERSION001";
		internal const string STEAMAPPS_INTERFACE_VERSION = "STEAMAPPS_INTERFACE_VERSION008";
		internal const string STEAMAPPTICKET_INTERFACE_VERSION = "STEAMAPPTICKET_INTERFACE_VERSION001";
		internal const string STEAMCONTROLLER_INTERFACE_VERSION = "SteamController007";
		internal const string STEAMFRIENDS_INTERFACE_VERSION = "SteamFriends017";
		internal const string STEAMGAMECOORDINATOR_INTERFACE_VERSION = "SteamGameCoordinator001";
		internal const string STEAMGAMESERVER_INTERFACE_VERSION = "SteamGameServer012";
		internal const string STEAMGAMESERVERSTATS_INTERFACE_VERSION = "SteamGameServerStats001";
		internal const string STEAMHTMLSURFACE_INTERFACE_VERSION = "STEAMHTMLSURFACE_INTERFACE_VERSION_005";
		internal const string STEAMHTTP_INTERFACE_VERSION = "STEAMHTTP_INTERFACE_VERSION003";
		internal const string STEAMINPUT_INTERFACE_VERSION = "SteamInput001";
		internal const string STEAMINVENTORY_INTERFACE_VERSION = "STEAMINVENTORY_INTERFACE_V003";
		internal const string STEAMMATCHMAKING_INTERFACE_VERSION = "SteamMatchMaking009";
		internal const string STEAMMATCHMAKINGSERVERS_INTERFACE_VERSION = "SteamMatchMakingServers002";
		internal const string STEAMGAMESEARCH_INTERFACE_VERSION = "SteamMatchGameSearch001";
		internal const string STEAMPARTIES_INTERFACE_VERSION = "SteamParties002";
		internal const string STEAMMUSIC_INTERFACE_VERSION = "STEAMMUSIC_INTERFACE_VERSION001";
		internal const string STEAMMUSICREMOTE_INTERFACE_VERSION = "STEAMMUSICREMOTE_INTERFACE_VERSION001";
		internal const string STEAMNETWORKING_INTERFACE_VERSION = "SteamNetworking005";
		internal const string STEAMNETWORKINGSOCKETS_INTERFACE_VERSION = "SteamNetworkingSockets002";
		internal const string STEAMNETWORKINGUTILS_INTERFACE_VERSION = "SteamNetworkingUtils001";
		internal const string STEAMPARENTALSETTINGS_INTERFACE_VERSION = "STEAMPARENTALSETTINGS_INTERFACE_VERSION001";
		internal const string STEAMREMOTESTORAGE_INTERFACE_VERSION = "STEAMREMOTESTORAGE_INTERFACE_VERSION014";
		internal const string STEAMSCREENSHOTS_INTERFACE_VERSION = "STEAMSCREENSHOTS_INTERFACE_VERSION003";
		internal const string STEAMUGC_INTERFACE_VERSION = "STEAMUGC_INTERFACE_VERSION012";
		internal const string STEAMUSER_INTERFACE_VERSION = "SteamUser020";
		internal const string STEAMUSERSTATS_INTERFACE_VERSION = "STEAMUSERSTATS_INTERFACE_VERSION011";
		internal const string STEAMUTILS_INTERFACE_VERSION = "SteamUtils009";
		internal const string STEAMVIDEO_INTERFACE_VERSION = "STEAMVIDEO_INTERFACE_V002";
	}
}
