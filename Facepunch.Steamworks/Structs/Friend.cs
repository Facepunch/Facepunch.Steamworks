using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Steamworks.Data;

namespace Steamworks
{
	public struct Friend
	{
		public SteamId Id;

		public Friend( SteamId steamid )
		{
			Id = steamid;
		}

		public override string ToString()
		{
			return $"{Name} ({Id.ToString()})";
		}


		/// <summary>
		/// Returns true if this is the local user
		/// </summary>
		public bool IsMe => Id == SteamClient.SteamId;

		/// <summary>
		/// Return true if this is a friend
		/// </summary>
		public bool IsFriend => Relationship == Relationship.Friend;

		/// <summary>
		/// Returns true if you have this user blocked
		/// </summary>
		public bool IsBlocked => Relationship == Relationship.Blocked;

		/// <summary>
		/// Return true if this user is playing the game we're running
		/// </summary>
		public bool IsPlayingThisGame => GameInfo?.GameID == SteamClient.AppId;

		/// <summary>
		/// Returns true if this friend is online
		/// </summary>
		public bool IsOnline => State != FriendState.Offline;

		/// <summary>
		/// Sometimes we don't know the user's name. This will wait until we have
		/// downloaded the information on this user.
		/// </summary>
		public async Task RequestInfoAsync()
		{
			await SteamFriends.CacheUserInformationAsync( Id, true );
		}

		/// <summary>
		/// Returns true if this friend is marked as away
		/// </summary>
		public bool IsAway => State == FriendState.Away;

		/// <summary>
		/// Returns true if this friend is marked as busy
		/// </summary>
		public bool IsBusy => State == FriendState.Busy;

		/// <summary>
		/// Returns true if this friend is marked as snoozing
		/// </summary>
		public bool IsSnoozing => State == FriendState.Snooze;



		public Relationship Relationship => SteamFriends.Internal.GetFriendRelationship( Id );
		public FriendState State => SteamFriends.Internal.GetFriendPersonaState( Id );
		public string Name => SteamFriends.Internal.GetFriendPersonaName( Id );
		public IEnumerable<string> NameHistory
		{
			get
			{
				for( int i=0; i<32; i++ )
				{
					var n = SteamFriends.Internal.GetFriendPersonaNameHistory( Id, i );
					if ( string.IsNullOrEmpty( n ) )
						break;

					yield return n;
				}
			}
		}

		public int SteamLevel => SteamFriends.Internal.GetFriendSteamLevel( Id );



		public FriendGameInfo? GameInfo
		{
			get
			{
				FriendGameInfo_t gameInfo = default;
				if ( !SteamFriends.Internal.GetFriendGamePlayed( Id, ref gameInfo ) )
					return null;

				return FriendGameInfo.From( gameInfo );
			}
		}

		public bool IsIn( SteamId group_or_room )
		{
			return SteamFriends.Internal.IsUserInSource( Id, group_or_room );
		}

		public struct FriendGameInfo
		{
			internal ulong GameID; // m_gameID class CGameID
			internal uint GameIP; // m_unGameIP uint32
			internal ulong SteamIDLobby; // m_steamIDLobby class CSteamID

			public int ConnectionPort;
			public int QueryPort;

			public uint IpAddressRaw => GameIP;
			public System.Net.IPAddress IpAddress => Utility.Int32ToIp( GameIP );

			public Lobby? Lobby
			{
				get
				{
					if ( SteamIDLobby == 0 ) return null;
					return new Lobby( SteamIDLobby );
				}
			}

			internal static FriendGameInfo From( FriendGameInfo_t i )
			{
				return new FriendGameInfo
				{
					GameID = i.GameID,
					GameIP = i.GameIP,
					ConnectionPort = i.GamePort,
					QueryPort = i.QueryPort,
					SteamIDLobby = i.SteamIDLobby,
				};
			}
		}

		public async Task<Data.Image?> GetSmallAvatarAsync()
		{
			return await SteamFriends.GetSmallAvatarAsync( Id );
		}

		public async Task<Data.Image?> GetMediumAvatarAsync()
		{
			return await SteamFriends.GetMediumAvatarAsync( Id );
		}

		public async Task<Data.Image?> GetLargeAvatarAsync()
		{
			return await SteamFriends.GetLargeAvatarAsync( Id );
		}

		public string GetRichPresence( string key )
		{
			var val = SteamFriends.Internal.GetFriendRichPresence( Id, key );
			if ( string.IsNullOrEmpty( val ) ) return null;
			return val;
		}

		/// <summary>
		/// Invite this friend to the game that we are playing
		/// </summary>
		public bool InviteToGame( string Text )
		{
			return SteamFriends.Internal.InviteUserToGame( Id, Text );
		}

		/// <summary>
		/// Sends a message to a Steam friend. Returns true if success
		/// </summary>
		public bool SendMessage( string message )
		{
			return SteamFriends.Internal.ReplyToFriendMessage( Id, message );
		}


		/// <summary>
		/// Tries to get download the latest user stats
		/// </summary>
		/// <returns>True if successful, False if failure</returns>
		public async Task<bool> RequestUserStatsAsync()
		{
			var result = await SteamUserStats.Internal.RequestUserStats( Id );
			return result.HasValue && result.Value.Result == Result.OK;
		}

		/// <summary>
		/// Gets a user stat. Must call RequestUserStats first.
		/// </summary>
		/// <param name="statName">The name of the stat you want to get</param>
		/// <param name="defult">Will return this value if not available</param>
		/// <returns>The value, or defult if not available</returns>
		public float GetStatFloat( string statName, float defult = 0 )
		{
			var val = defult;

			if ( !SteamUserStats.Internal.GetUserStat( Id, statName, ref val ) )
				return defult;

			return val;
		}

		/// <summary>
		/// Gets a user stat. Must call RequestUserStats first.
		/// </summary>
		/// <param name="statName">The name of the stat you want to get</param>
		/// <param name="defult">Will return this value if not available</param>
		/// <returns>The value, or defult if not available</returns>
		public int GetStatInt( string statName, int defult = 0 )
		{
			var val = defult;

			if ( !SteamUserStats.Internal.GetUserStat( Id, statName, ref val ) )
				return defult;

			return val;
		}

		/// <summary>
		/// Gets a user achievement state. Must call RequestUserStats first.
		/// </summary>
		/// <param name="statName">The name of the achievement you want to get</param>
		/// <param name="defult">Will return this value if not available</param>
		/// <returns>The value, or defult if not available</returns>
		public bool GetAchievement( string statName, bool defult = false )
		{
			var val = defult;

			if ( !SteamUserStats.Internal.GetUserAchievement( Id, statName, ref val ) )
				return defult;

			return val;
		}		
		
		/// <summary>
		/// Gets a the time this achievement was unlocked.
		/// </summary>
		/// <param name="statName">The name of the achievement you want to get</param>
		/// <returns>The time unlocked. If it wasn't unlocked, or you haven't downloaded the stats yet - will return DateTime.MinValue</returns>
		public DateTime GetAchievementUnlockTime( string statName )
		{
			bool val = false;
			uint time = 0;

			if ( !SteamUserStats.Internal.GetUserAchievementAndUnlockTime( Id, statName, ref val, ref time ) || !val )
				return DateTime.MinValue;

			return Epoch.ToDateTime( time );
		}

	}
}
