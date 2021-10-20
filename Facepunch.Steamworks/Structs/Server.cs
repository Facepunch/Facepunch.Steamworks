using System;
using System.Collections.Generic;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Steamworks.Data
{
	public struct ServerInfo : IEquatable<ServerInfo>
	{
		public string Name { get; set; }
		public int Ping { get; set; }
		public string GameDir { get; set; }
		public string Map { get; set; }
		public string Description { get; set; }
		public uint AppId { get; set; }
		public int Players { get; set; }
		public int MaxPlayers { get; set; }
		public int BotPlayers { get; set; }
		public bool Passworded { get; set; }
		public bool Secure { get; set; }
		public uint LastTimePlayed { get; set; }
		public int Version { get; set; }
		public string TagString { get; set; }
		public ulong SteamId { get; set; }
		public uint AddressRaw { get; set; }
		public IPAddress Address { get; set; }
		public int ConnectionPort { get; set; }
		public int QueryPort { get; set; }

		string[] _tags;

		/// <summary>
		/// Gets the individual tags for this server
		/// </summary>
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
		/// Add this server to our history list
		/// If we're already in the history list, weill set the last played time to now
		/// </summary>
		public void AddToHistory()
		{
			SteamMatchmaking.Internal.AddFavoriteGame( SteamClient.AppId, AddressRaw, (ushort)ConnectionPort, (ushort)QueryPort, k_unFavoriteFlagHistory, (uint)Epoch.Current );
		}

		/// <summary>
		/// If this server responds to source engine style queries, we'll be able to get a list of rules here
		/// </summary>
		public async Task<Dictionary<string, string>> QueryRulesAsync()
		{
			return await SourceServerQuery.GetRules( this );
		}

		/// <summary>
		/// Remove this server from our history list
		/// </summary>
		public void RemoveFromHistory()
		{
			SteamMatchmaking.Internal.RemoveFavoriteGame( SteamClient.AppId, AddressRaw, (ushort)ConnectionPort, (ushort)QueryPort, k_unFavoriteFlagHistory );
		}

		/// <summary>
		/// Add this server to our favourite list
		/// </summary>
		public void AddToFavourites()
		{
			SteamMatchmaking.Internal.AddFavoriteGame( SteamClient.AppId, AddressRaw, (ushort)ConnectionPort, (ushort)QueryPort, k_unFavoriteFlagFavorite, (uint)Epoch.Current );
		}

		/// <summary>
		/// Remove this server from our favourite list
		/// </summary>
		public void RemoveFromFavourites()
		{
			SteamMatchmaking.Internal.RemoveFavoriteGame( SteamClient.AppId, AddressRaw, (ushort)ConnectionPort, (ushort)QueryPort, k_unFavoriteFlagFavorite );
		}

		public bool Equals( ServerInfo other )
		{
			return this.GetHashCode() == other.GetHashCode();
		}

		public override int GetHashCode()
		{
			return Address.GetHashCode() + SteamId.GetHashCode() + ConnectionPort.GetHashCode() + QueryPort.GetHashCode();
		}
	}
}
