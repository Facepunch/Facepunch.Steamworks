using System;
using System.Collections.Generic;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Steamworks.Data
{
	/// <summary>
	/// A snapshot of one game server as returned by a server-list query &#8212; its name, map, player
	/// counts, ping and address. This is what you build a server browser out of.
	/// </summary>
	/// <remarks>
	/// <para>
	/// Every field is a point-in-time copy taken when Steam answered the query. Nothing here updates
	/// itself: player counts and ping go stale the moment you have them, so re-query rather than
	/// holding a list open.
	/// </para>
	/// <para>
	/// <b>Equality on this type is unreliable.</b> <see cref="Equals(ServerInfo)"/> compares hash
	/// codes rather than fields, and <see cref="GetHashCode"/> sums its components, so distinct
	/// servers can compare equal. Match on <see cref="Address"/> plus <see cref="ConnectionPort"/>
	/// yourself if it matters. <see cref="GetHashCode"/> also throws on a default-constructed value,
	/// because <see cref="Address"/> is null until something fills it in.
	/// </para>
	/// </remarks>
	/// <example>
	/// <code>
	/// using ( var list = new ServerList.Internet() )
	/// {
	///     list.AddFilter( "map", "de_dust2" );
	///     await list.RunQueryAsync( timeoutSeconds: 5 );
	///
	///     foreach ( var server in list.Responsive.OrderBy( x =&gt; x.Ping ) )
	///         Console.WriteLine( $"{server.Name} {server.Map} "
	///                          + $"{server.Players}/{server.MaxPlayers} {server.Ping}ms" );
	/// }
	/// </code>
	/// </example>
	public struct ServerInfo : IEquatable<ServerInfo>
	{
		/// <summary>
		/// The server's advertised hostname &#8212; the headline string in a server browser. Operator
		/// controlled, so treat it as untrusted: it can contain colour codes, misleading text
		/// impersonating official servers, or nothing at all.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Round trip time to the server in milliseconds, as measured by Steam when it answered the
		/// query. A single sample rather than an average, so it is noisy; and it is measured from the
		/// machine that ran the query.
		/// </summary>
		public int Ping { get; set; }

		/// <summary>
		/// The game directory the server is running, which for a modded game distinguishes one mod
		/// from another. Matches the <c>gamedir</c> filter you can pass to a server-list query.
		/// </summary>
		public string GameDir { get; set; }

		/// <summary>
		/// The map currently loaded. Format is entirely up to the game &#8212; Steam does not
		/// normalise it, so casing and any path prefix are whatever the server reported.
		/// </summary>
		public string Map { get; set; }

		/// <summary>
		/// The game's own description of itself, set by the server rather than by Steam. Often the
		/// game mode or a product name.
		/// </summary>
		public string Description { get; set; }

		/// <summary>
		/// Which Steam application this server is hosting. Worth checking if you query with a filter
		/// set that could match servers for another app.
		/// </summary>
		public uint AppId { get; set; }

		/// <summary>
		/// Number of players currently connected, <b>including bots</b>. Subtract
		/// <see cref="BotPlayers"/> for the human count, which is usually what a browser should show.
		/// </summary>
		public int Players { get; set; }

		/// <summary>
		/// The server's player slot limit. A server can briefly report more players than this, for
		/// example while reserved slots are in use.
		/// </summary>
		public int MaxPlayers { get; set; }

		/// <summary>
		/// How many of the connected players are bots. Servers self-report this, and padding it with
		/// fake bots to look busy is a known abuse, so do not treat it as authoritative.
		/// </summary>
		public int BotPlayers { get; set; }

		/// <summary>
		/// Whether the server requires a password to join. Note this says a password exists, not that
		/// you have it &#8212; joining still fails without the right one.
		/// </summary>
		public bool Passworded { get; set; }

		/// <summary>
		/// Whether the server runs with VAC (Valve Anti-Cheat) enabled. Players banned by VAC for this
		/// game cannot join secure servers.
		/// </summary>
		public bool Secure { get; set; }

		/// <summary>
		/// When the local user last played on this server, as a Unix timestamp in seconds. Only
		/// populated for servers that came from the history or favourites lists; it is 0 for a fresh
		/// internet or LAN query.
		/// </summary>
		public uint LastTimePlayed { get; set; }

		/// <summary>
		/// The server's self-reported version number. The meaning is entirely game-specific &#8212;
		/// Steam neither defines nor validates it.
		/// </summary>
		public int Version { get; set; }

		/// <summary>
		/// The raw, comma-separated tag string exactly as the server advertised it. Use
		/// <see cref="Tags"/> for the split form. This is the field the <c>gametype</c> server-list
		/// filter matches against.
		/// </summary>
		public string TagString { get; set; }

		/// <summary>
		/// The server's own Steam ID, which game servers get by logging in to Steam. Zero for servers
		/// running without a Steam login, so it is not a reliable unique key.
		/// </summary>
		public ulong SteamId { get; set; }

		/// <summary>
		/// The server's IPv4 address packed into a host-order <see cref="uint"/>. This is the form the
		/// favourites and history APIs take; <see cref="Address"/> is the same value in a usable type.
		/// </summary>
		public uint AddressRaw { get; set; }

		/// <summary>
		/// The server's IP address. Always IPv4 &#8212; Steam's server-list API predates IPv6 and has
		/// no representation for it.
		/// </summary>
		/// <remarks>
		/// <see langword="null"/> on a default-constructed <see cref="ServerInfo"/>, which is what
		/// makes <see cref="GetHashCode"/> throw on one.
		/// </remarks>
		public IPAddress Address { get; set; }

		/// <summary>
		/// The port a game client connects to. This is frequently <b>not</b> the same as
		/// <see cref="QueryPort"/>, and using the wrong one is the usual reason a join silently fails.
		/// </summary>
		public int ConnectionPort { get; set; }

		/// <summary>
		/// The port Steam and Source-engine style queries talk to for server info and rules. Pass this
		/// one to <see cref="QueryRulesAsync"/>, not <see cref="ConnectionPort"/>.
		/// </summary>
		public int QueryPort { get; set; }

		string[] _tags;

		/// <summary>
		/// Gets the individual tags for this server, split out of the comma-separated
		/// <see cref="TagString"/>. Tags are how most games advertise game mode, region, ruleset and
		/// whether a server is modded.
		/// </summary>
		/// <returns>
		/// The tags, or <see langword="null"/> when the server advertised none &#8212; <b>not</b> an
		/// empty array. Null-check before enumerating.
		/// </returns>
		/// <remarks>
		/// The split result is cached in the struct on first access. Because this is a value type,
		/// that caching only benefits the particular copy you accessed. There is no trimming or
		/// case normalisation; tags are exactly as the server sent them.
		/// </remarks>
		public string[] Tags
		{
			get
			{
				if ( _tags == null )
				{
					if ( !string.IsNullOrEmpty( TagString ) )
					{
						_tags = TagString.Split( ',' );
					}
				}

				return _tags;
			}
		}

		internal static ServerInfo From( gameserveritem_t item )
		{
			return new ServerInfo()
			{
				AddressRaw = item.NetAdr.IP,
				Address = Utility.Int32ToIp( item.NetAdr.IP ),
				ConnectionPort = item.NetAdr.ConnectionPort,
				QueryPort = item.NetAdr.QueryPort,
				Name = item.ServerNameUTF8(),
				Ping = item.Ping,
				GameDir = item.GameDirUTF8(),
				Map = item.MapUTF8(),
				Description = item.GameDescriptionUTF8(),
				AppId = item.AppID,
				Players = item.Players,
				MaxPlayers = item.MaxPlayers,
				BotPlayers = item.BotPlayers,
				Passworded = item.Password,
				Secure = item.Secure,
				LastTimePlayed = item.TimeLastPlayed,
				Version = item.ServerVersion,
				TagString = item.GameTagsUTF8(),
				SteamId = item.SteamID
			};
		}

		/// <summary>
		/// Build a server reference by address, without querying it. Use this to act on a server you
		/// already know about &#8212; adding a known address to favourites, for example &#8212;
		/// rather than to describe one.
		/// </summary>
		/// <param name="ip">The server's IPv4 address packed into a host-order <see cref="uint"/>.</param>
		/// <param name="cport">The port game clients connect to.</param>
		/// <param name="qport">The port used for server queries, which is often different from <paramref name="cport"/>.</param>
		/// <param name="timeplayed">When the user last played here, as a Unix timestamp in seconds. Pass 0 if unknown.</param>
		/// <remarks>
		/// Everything descriptive &#8212; <see cref="Name"/>, <see cref="Map"/>,
		/// <see cref="Players"/>, <see cref="Ping"/> &#8212; is left empty or zero. Only the address
		/// fields are set, which is enough for <see cref="AddToFavourites"/>,
		/// <see cref="AddToHistory"/> and their removals.
		/// </remarks>
		public ServerInfo( uint ip, ushort cport, ushort qport, uint timeplayed ) : this()
		{
			AddressRaw = ip;
			Address = Utility.Int32ToIp( ip );
			ConnectionPort = cport;
			QueryPort = qport;
			LastTimePlayed = timeplayed;
		}

		internal const uint k_unFavoriteFlagNone = 0x00;
		internal const uint k_unFavoriteFlagFavorite = 0x01; // this game favorite entry is for the favorites list
		internal const uint k_unFavoriteFlagHistory = 0x02; // this game favorite entry is for the history list



		/// <summary>
		/// Add this server to our history list.
		/// If we're already in the history list, will set the last played time to now.
		/// The history list is the "recently played" list shared with the Steam client's own server
		/// browser, so entries you add here show up there too.
		/// </summary>
		/// <remarks>
		/// Recorded against <see cref="SteamClient.AppId"/>, not against this server's
		/// <see cref="AppId"/>. There is no return value and no callback, so a failure is not
		/// observable from here. Requires <see cref="AddressRaw"/> and the ports to be set.
		/// </remarks>
		public void AddToHistory()
		{
			SteamMatchmaking.Internal.AddFavoriteGame( SteamClient.AppId, AddressRaw, (ushort)ConnectionPort, (ushort)QueryPort, k_unFavoriteFlagHistory, (uint)Epoch.Current );
		}

		/// <summary>
		/// If this server responds to source engine style queries, we'll be able to get a list of rules here.
		/// Rules are the server's advertised convars &#8212; things like round time, friendly fire and
		/// game mode &#8212; which is detail the server list itself does not carry.
		/// </summary>
		/// <returns>
		/// The server's rules as key/value pairs, or an empty or null result if the server did not
		/// answer. Servers that are not Source-engine style, are firewalled on their query port, or
		/// simply choose not to reply all look the same from here.
		/// </returns>
		/// <remarks>
		/// <para>
		/// This talks <b>directly to the game server over UDP</b> from this machine, on
		/// <see cref="QueryPort"/>. It does not go through Steam, so it is subject to your own
		/// network's firewall rules, it will not work from behind a restrictive NAT, and the server
		/// learns your IP.
		/// </para>
		/// <para>
		/// Query one server at a time in response to a user selecting it. Fanning this out across a
		/// whole server list sends a UDP packet to every server in it.
		/// </para>
		/// </remarks>
		public async Task<Dictionary<string, string>> QueryRulesAsync()
		{
			return await SourceServerQuery.GetRules( this );
		}

		/// <summary>
		/// Remove this server from our history list. Silent if it was not in the list.
		/// </summary>
		public void RemoveFromHistory()
		{
			SteamMatchmaking.Internal.RemoveFavoriteGame( SteamClient.AppId, AddressRaw, (ushort)ConnectionPort, (ushort)QueryPort, k_unFavoriteFlagHistory );
		}

		/// <summary>
		/// Add this server to our favourite list &#8212; the user's deliberately saved servers, as
		/// opposed to the automatic history list. Shared with the Steam client's own server browser.
		/// </summary>
		/// <remarks>
		/// Saved against <see cref="SteamClient.AppId"/>. No return value and no callback, so failures
		/// are not observable. Favourites and history are separate lists; adding to one does not touch
		/// the other.
		/// </remarks>
		public void AddToFavourites()
		{
			SteamMatchmaking.Internal.AddFavoriteGame( SteamClient.AppId, AddressRaw, (ushort)ConnectionPort, (ushort)QueryPort, k_unFavoriteFlagFavorite, (uint)Epoch.Current );
		}

		/// <summary>
		/// Remove this server from our favourite list. Silent if it was not in the list, and leaves
		/// any history entry for the same server alone.
		/// </summary>
		public void RemoveFromFavourites()
		{
			SteamMatchmaking.Internal.RemoveFavoriteGame( SteamClient.AppId, AddressRaw, (ushort)ConnectionPort, (ushort)QueryPort, k_unFavoriteFlagFavorite );
		}

		/// <summary>
		/// Whether this refers to the same server as another value, by address and identity rather
		/// than by the descriptive fields &#8212; so two snapshots of one server taken at different
		/// times compare equal even if the player count changed.
		/// </summary>
		/// <param name="other">The server to compare against.</param>
		/// <returns><see langword="true"/> if the two are considered the same server.</returns>
		/// <remarks>
		/// <b>This compares hash codes, not fields.</b> Two genuinely different servers whose hashes
		/// collide will compare equal, and because <see cref="GetHashCode"/> simply adds its
		/// components together, collisions are more likely than a well-mixed hash would give. Do not
		/// rely on this for anything that matters; compare <see cref="Address"/> and
		/// <see cref="ConnectionPort"/> directly instead.
		/// </remarks>
		public bool Equals( ServerInfo other )
		{
			return this.GetHashCode() == other.GetHashCode();
		}

		/// <summary>
		/// A hash over the server's address, ports and Steam ID, so servers can be used as dictionary
		/// keys or de-duplicated across repeated queries.
		/// </summary>
		/// <returns>The combined hash code.</returns>
		/// <remarks>
		/// <b>Throws <see cref="NullReferenceException"/> on a default-constructed value</b>, because
		/// <see cref="Address"/> is null until it is populated. Guard before putting a
		/// <c>default(ServerInfo)</c> into a hashed collection.
		/// </remarks>
		public override int GetHashCode()
		{
			return Address.GetHashCode() + SteamId.GetHashCode() + ConnectionPort.GetHashCode() + QueryPort.GetHashCode();
		}
	}
}
