using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;

namespace Steamworks
{
    /// <summary>
    /// Used to set up the server. 
    /// The variables in here are all required to be set, and can't be changed once the server is created.
    /// </summary>
    public struct SteamServerInit
    {
        public IPAddress IpAddress;
        public ushort SteamPort;
        public ushort GamePort;
        public ushort QueryPort;
        public bool Secure;

        /// <summary>
        /// The version string is usually in the form x.x.x.x, and is used by the master server to detect when the server is out of date.
        /// If you go into the dedicated server tab on steamworks you'll be able to server the latest version. If this version number is
        /// less than that latest version then your server won't show.
        /// </summary>
        public string VersionString;

		/// <summary>
		/// This should be the same directory game where gets installed into. Just the folder name, not the whole path. I.e. "Rust", "Garrysmod".
		/// </summary>
		public string ModDir;

        /// <summary>
        /// The game description. Setting this to the full name of your game is recommended.
        /// </summary>
        public string GameDescription;

		/// <summary>
		/// Is a dedicated server
		/// </summary>
		public bool DedicatedServer;


		public SteamServerInit( string modDir, string gameDesc )
        {
			DedicatedServer = true;
			ModDir = modDir;
            GameDescription = gameDesc;
			GamePort = 27015;
			QueryPort = 27016;
			Secure = true;
			VersionString = "1.0.0.0";
			IpAddress = null;
			SteamPort = 0;
		}

        /// <summary>
        /// Set the Steam quert port 
        /// </summary>
        public SteamServerInit WithRandomSteamPort()
        {
            SteamPort = (ushort)new Random().Next( 10000, 60000 );
            return this;
        }

        /// <summary>
        /// If you pass MASTERSERVERUPDATERPORT_USEGAMESOCKETSHARE into usQueryPort, then it causes the game server API to use 
        /// "GameSocketShare" mode, which means that the game is responsible for sending and receiving UDP packets for the master
        /// server updater.
        /// 
        /// More info about this here: https://partner.steamgames.com/doc/api/ISteamGameServer#HandleIncomingPacket
        /// </summary>
        public SteamServerInit WithQueryShareGamePort()
        {
            QueryPort = 0xFFFF;
            return this;
        }
    }
}
