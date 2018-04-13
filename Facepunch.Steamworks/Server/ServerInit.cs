using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;

namespace Facepunch.Steamworks
{
    /// <summary>
    /// Used to set up the server. 
    /// The variables in here are all required to be set, and can't be changed once the server is created.
    /// </summary>
    public class ServerInit
    {
        public IPAddress IpAddress;
        public ushort SteamPort;
        public ushort GamePort = 27015;
        public ushort QueryPort = 27016;
        public bool Secure = true;

        /// <summary>
        /// The version string is usually in the form x.x.x.x, and is used by the master server to detect when the server is out of date.
        /// If you go into the dedicated server tab on steamworks you'll be able to server the latest version. If this version number is
        /// less than that latest version then your server won't show.
        /// </summary>
        public string VersionString = "2.0.0.0";

        /// <summary>
        /// This should be the same directory game where gets installed into. Just the folder name, not the whole path. I.e. "Rust", "Garrysmod".
        /// </summary>
        public string ModDir = "unset";

        /// <summary>
        /// The game description. Setting this to the full name of your game is recommended.
        /// </summary>
        public string GameDescription = "unset";


        public ServerInit( string modDir, string gameDesc )
        {
            ModDir = modDir;
            GameDescription = gameDesc;
        }

        public ServerInit(IPAddress ip, string modDir, string gameDesc)
        {
            IpAddress = ip.IpToInt32();
            ModDir = modDir;
            GameDescription = gameDesc;
        }

        /// <summary>
        /// set the server ip
        /// </summary>
        public ServerInit Ip(string ip)
        {
            IpAddress = IPAddress.Parse(ip).IpToInt32();
            return this;
        }

        /// <summary>
        /// set the server ip
        /// </summary>
        public ServerInit Ip(IPAddress ip)
        {
            IpAddress = ip.IpToInt32();
            return this;
        }

        /// <summary>
        /// Set the Steam quert port 
        /// </summary>
        public ServerInit RandomSteamPort()
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
        public ServerInit QueryShareGamePort()
        {
            QueryPort = 0xFFFF;
            return this;
        }
    }
}
