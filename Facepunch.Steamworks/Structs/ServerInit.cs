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
    /// <remarks>
    /// <para>
    /// This struct exists because Steam splits server configuration in two. Everything here is either
    /// consumed by the native init itself (address, ports, security mode, version string) or is one of the
    /// properties Valve requires to be established before login and forbids changing afterwards -
    /// <see cref="ModDir"/> and <see cref="GameDescription"/> map to <see cref="SteamServer.ModDir"/> and
    /// <see cref="SteamServer.GameDescription"/>, and <see cref="DedicatedServer"/> to
    /// <see cref="SteamServer.DedicatedServer"/>. Those properties have internal setters precisely so this
    /// is the only place they can come from.
    /// </para>
    /// <para>
    /// Anything Valve classes as server state - <see cref="SteamServer.ServerName"/>,
    /// <see cref="SteamServer.MapName"/>, <see cref="SteamServer.MaxPlayers"/>,
    /// <see cref="SteamServer.BotCount"/>, <see cref="SteamServer.Passworded"/>,
    /// <see cref="SteamServer.GameTags"/>, key values - is deliberately absent here, because it may be
    /// changed at any time and belongs after <see cref="SteamServer.Init"/> rather than in it.
    /// </para>
    /// <para>
    /// Being a struct, it is copied when passed. Mutating your local copy after calling
    /// <see cref="SteamServer.Init"/> has no effect on the running server.
    /// </para>
    /// </remarks>
    public struct SteamServerInit
    {
        /// <summary>
        /// Which local network interface to bind to. Leave <see langword="null"/> - the normal case - and
        /// Steam binds to any address. Set it only on a machine with several IP addresses where you need a
        /// specific one to be the address the server browser advertises.
        /// </summary>
        /// <remarks>
        /// Converted to a packed 32-bit host-order value by <see cref="SteamServer.Init"/>, which passes 0
        /// when this is <see langword="null"/>. Only IPv4 is meaningful here.
        /// </remarks>
        public IPAddress IpAddress;

        /// <summary>
        /// The UDP port clients connect to for gameplay. You open and own the socket on this port yourself -
        /// Steam only advertises the number, so nothing here binds it or checks it is free.
        /// </summary>
        /// <remarks>
        /// Defaults to 27015 via the constructor. A port already in use surfaces as
        /// <see cref="SteamServer.Init"/> throwing, not as a quiet fallback to another port.
        /// </remarks>
        public ushort GamePort;

        /// <summary>
        /// The UDP port Steam uses for server-browser pings and rules/player queries. Steam opens this
        /// socket itself, so it must be free and reachable, and it must differ from
        /// <see cref="GamePort"/> unless you are deliberately sharing.
        /// </summary>
        /// <remarks>
        /// Defaults to 27016 via the constructor. The single sentinel value <c>0xFFFF</c> means something
        /// entirely different - it selects GameSocketShare mode, where Steam opens no socket and you relay
        /// its traffic yourself. Set that through <see cref="WithQueryShareGamePort"/> rather than by hand,
        /// and see <c>SteamServer.HandleIncomingPacket</c> and
        /// <see cref="SteamServer.GetOutgoingPacket"/> for what it then obliges you to do.
        /// </remarks>
        public ushort QueryPort;

        /// <summary>
        /// Whether connecting clients should be VAC protected. This selects Valve's server mode:
        /// <see langword="true"/> means authenticate users, list on the master server, and run VAC on
        /// clients; <see langword="false"/> means authenticate and list but no VAC.
        /// </summary>
        /// <remarks>
        /// Neither setting disables authentication - the ticket flow through
        /// <see cref="SteamServer.BeginAuthSession"/> applies either way. Defaults to
        /// <see langword="true"/> via the constructor. Fixed at init: there is no way to turn VAC on or off
        /// on a running server.
        /// </remarks>
        public bool Secure;

        /// <summary>
        /// The version string is usually in the form x.x.x.x, and is used by the master server to detect when the server is out of date.
        /// If you go into the dedicated server tab on steamworks you'll be able to server the latest version. If this version number is
        /// less than that latest version then your server won't show.
        /// </summary>
        /// <remarks>
        /// "Won't show" is the whole failure mode, and it is silent: the server initializes, logs on, and
        /// runs normally - it is simply filtered out of the master server list. It is the first thing to
        /// check when a server works over direct connect but is invisible in the browser. The constructor
        /// leaves this at the placeholder <c>"1.0.0.0"</c>, so wire it to your real build version rather
        /// than shipping the default.
        /// </remarks>
        public string VersionString;

		/// <summary>
		/// This should be the same directory game where gets installed into. Just the folder name, not the whole path. I.e. "Rust", "Garrysmod".
		/// </summary>
		/// <remarks>
		/// Valve's default for this is the empty string, meaning "the original game, not a mod". The server
		/// browser groups servers by this value, so a string that does not match what clients derive from
		/// their own install makes the server invisible to them - the same silent symptom as a stale
		/// <see cref="VersionString"/>. Valve references <c>k_cbMaxGameServerGameDir</c>, which is 32 bytes.
		/// </remarks>
		public string ModDir;

        /// <summary>
        /// The game description. Setting this to the full name of your game is recommended.
        /// </summary>
        /// <remarks>
        /// Display text, not an identifier - unlike <see cref="ModDir"/> nothing matches on it, so a
        /// mistake here is cosmetic rather than a reason to disappear from the browser. Required by Valve
        /// and fixed once the server logs on, so anything that varies per session belongs in
        /// <see cref="SteamServer.ServerName"/> or <see cref="SteamServer.GameTags"/> instead. The browser
        /// record stores it in a 64-byte field.
        /// </remarks>
        public string GameDescription;

		/// <summary>
		/// Whether Steam should present this as a dedicated server rather than a listen server running
		/// inside a player's game client. Affects how the server is categorised and filtered in the
		/// browser; it does not change how the process behaves.
		/// </summary>
		/// <remarks>
		/// Valve's native default is <see langword="false"/>, but the constructor here sets it
		/// <see langword="true"/> - so a <see cref="SteamServerInit"/> built with <c>default</c> instead of
		/// the constructor reports a listen server. Fixed before login and not changeable afterwards.
		/// </remarks>
		public bool DedicatedServer;


		/// <summary>
		/// Builds an init block for a dedicated server with the conventional Source-style defaults already
		/// filled in, so you only have to override what actually differs. Use this rather than
		/// <c>default</c>: a zero-initialised <see cref="SteamServerInit"/> has no ports, no version string,
		/// and reports itself as a listen server.
		/// </summary>
		/// <param name="modDir">
		/// Assigned to <see cref="ModDir"/> - the install folder name only, not a path.
		/// </param>
		/// <param name="gameDesc">
		/// Assigned to <see cref="GameDescription"/> - the human-readable game name shown in the browser.
		/// </param>
		/// <remarks>
		/// Defaults applied: <see cref="GamePort"/> 27015, <see cref="QueryPort"/> 27016,
		/// <see cref="Secure"/> <see langword="true"/>, <see cref="DedicatedServer"/>
		/// <see langword="true"/>, <see cref="VersionString"/> <c>"1.0.0.0"</c>, and
		/// <see cref="IpAddress"/> <see langword="null"/> (bind to any). The version default in particular
		/// is a placeholder - leave it and the master server compares <c>1.0.0.0</c> against the latest
		/// version registered for your app, which will hide the server as soon as you publish anything
		/// newer.
		/// </remarks>
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
		}

        /// <summary>
        /// If you pass MASTERSERVERUPDATERPORT_USEGAMESOCKETSHARE into usQueryPort, then it causes the game server API to use 
        /// "GameSocketShare" mode, which means that the game is responsible for sending and receiving UDP packets for the master
        /// server updater.
        /// 
        /// More info about this here: https://partner.steamgames.com/doc/api/ISteamGameServer#HandleIncomingPacket
        /// </summary>
        /// <returns>
        /// A copy of this struct with <see cref="QueryPort"/> set to the shared-socket sentinel
        /// <c>0xFFFF</c>. <see cref="SteamServerInit"/> is a value type, so this returns a modified copy and
        /// leaves the original untouched - chain it into the value you pass to
        /// <see cref="SteamServer.Init"/>, do not call it and discard the result.
        /// </returns>
        /// <remarks>
        /// Taking this option is a commitment: Steam will not open a query socket, so nothing answers server
        /// browser pings unless you do it yourself. Every frame you must pass Steam any datagram arriving on
        /// your game socket that begins with <c>0xFFFFFFFF</c> via <c>SteamServer.HandleIncomingPacket</c>,
        /// and then drain <see cref="SteamServer.GetOutgoingPacket"/> until it returns
        /// <see langword="false"/>, sending each packet connectionlessly. Skip either half and the server
        /// silently stops appearing in the browser while otherwise working perfectly. The upside is that
        /// server operators only need one UDP port open in their firewall.
        /// </remarks>
        public SteamServerInit WithQueryShareGamePort()
        {
            QueryPort = 0xFFFF;
            return this;
        }
    }
}
