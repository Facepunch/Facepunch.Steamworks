using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Steamworks.Data;

namespace Steamworks
{
	public class SteamUserStats : SteamClientClass<SteamUserStats>
	{
		internal static ISteamUserStats Internal => Interface as ISteamUserStats;

		internal override void InitializeInterface( bool server )
		{
			SetInterface( server, new ISteamUserStats( server ) );
			InstallEvents();
			RequestCurrentStats();
		}

		public static bool StatsRecieved { get; internal set; }

		internal static void InstallEvents()
		{
			Dispatch.Install<UserStatsReceived_t>( x =>
			{
				if ( x.SteamIDUser == SteamClient.SteamId )
					StatsRecieved = true;

				OnUserStatsReceived?.Invoke( x.SteamIDUser, x.Result );
			} );

			Dispatch.Install<UserStatsStored_t>( x => OnUserStatsStored?.Invoke( x.Result ) );
			Dispatch.Install<UserAchievementStored_t>( x => OnAchievementProgress?.Invoke( new Achievement( x.AchievementNameUTF8() ), (int) x.CurProgress, (int)x.MaxProgress ) );
			Dispatch.Install<UserStatsUnloaded_t>( x => OnUserStatsUnloaded?.Invoke( x.SteamIDUser ) );
			Dispatch.Install<UserAchievementIconFetched_t>( x => OnAchievementIconFetched?.Invoke( x.AchievementNameUTF8(), x.IconHandle ) );
		}


		/// <summary>
		/// Invoked when an achivement icon is loaded.
		/// </summary>
		internal static event Action<string, int> OnAchievementIconFetched;

		/// <summary>
		/// Invoked when the latests stats and achievements have been received
		///	from the server.
		/// </summary>
		public static event Action<SteamId, Result> OnUserStatsReceived;

		/// <summary>
		/// Result of a request to store the user stats for a game.
		/// </summary>
		public static event Action<Result> OnUserStatsStored;

		/// <summary>
		/// Result of a request to store the achievements for a game, or an 
		///	"indicate progress" call. If both m_nCurProgress and m_nMaxProgress
		///	are zero, that means the achievement has been fully unlocked.
		/// </summary>
		public static event Action<Achievement, int, int> OnAchievementProgress;


		/// <summary>
		/// Callback indicating that a user's stats have been unloaded
		/// </summary>
		public static event Action<SteamId> OnUserStatsUnloaded;

		/// <summary>
		/// Get all available achievements.
		/// </summary>
		public static IEnumerable<Achievement> Achievements
		{
			get
			{
				for( int i=0; i< Internal.GetNumAchievements(); i++  )
				{
					yield return new Achievement( Internal.GetAchievementName( (uint) i ) );
				}
			}
		}

		/// <summary>
		/// Show the user a pop-up notification with the current progress toward an achievement.
		/// Will return false if RequestCurrentStats has not completed and successfully returned 
		/// its callback, if the achievement doesn't exist/has unpublished changes in the app's 
		/// Steamworks Admin page, or if the achievement is unlocked. 
		/// </summary>
		public static bool IndicateAchievementProgress( string achName, int curProg, int maxProg )
		{
			if ( string.IsNullOrEmpty( achName ) )
				throw new ArgumentNullException( "Achievement string is null or empty" );

			if ( curProg >= maxProg )
				throw new ArgumentException( $" Current progress [{curProg}] arguement toward achievement greater than or equal to max [{maxProg}]" );

			return Internal.IndicateAchievementProgress( achName, (uint)curProg, (uint)maxProg );
		}

		/// <summary>
		/// Tries to get the number of players currently playing this game.
		/// Or <c>-1</c> if failed.
		/// </summary>
		public static async Task<int> PlayerCountAsync()
		{
			var result = await Internal.GetNumberOfCurrentPlayers();
			if ( !result.HasValue || result.Value.Success == 0 )
				return -1;

			return result.Value.CPlayers;
		}

		/// <summary>
		/// Send the changed stats and achievements data to the server for permanent storage.
		/// If this fails then nothing is sent to the server. It's advisable to keep trying until the call is successful.
		/// This call can be rate limited. Call frequency should be on the order of minutes, rather than seconds.You should only be calling this during major state changes such as the end of a round, the map changing, or the user leaving a server. This call is required to display the achievement unlock notification dialog though, so if you have called SetAchievement then it's advisable to call this soon after that.
		/// If you have stats or achievements that you have saved locally but haven't uploaded with this function when your application process ends then this function will automatically be called.
		/// You can find additional debug information written to the %steam_install%\logs\stats_log.txt file.
		/// This function returns true upon success if :
		/// RequestCurrentStats has completed and successfully returned its callback AND
		/// the current game has stats associated with it in the Steamworks Partner backend, and those stats are published.
		/// </summary>
		public static bool StoreStats()
		{
			return Internal.StoreStats();
		}

		/// <summary>
		/// Asynchronously request the user's current stats and achievements from the server.
		/// You must always call this first to get the initial status of stats and achievements.
		/// Only after the resulting callback comes back can you start calling the rest of the stats 
		/// and achievement functions for the current user.
		/// </summary>
		public static bool RequestCurrentStats()
		{
			return Internal.RequestCurrentStats();
		}

		/// <summary>
		/// Asynchronously fetches global stats data, which is available for stats marked as 
		/// "aggregated" in the App Admin panel of the Steamworks website.
		/// You must have called <see cref="RequestCurrentStats"/> and it needs to return successfully via 
		/// its callback prior to calling this.
		/// </summary>
		/// <param name="days">How many days of day-by-day history to retrieve in addition to the overall totals. The limit is <c>60</c>.</param>
		/// <returns><see cref="Result.OK"/> indicates success, <see cref="Result.InvalidState"/> means you need to call <see cref="RequestCurrentStats"/> first, <see cref="Result.Fail"/> means the remote call failed</returns>
		public static async Task<Result> RequestGlobalStatsAsync( int days )
		{
			var result = await SteamUserStats.Internal.RequestGlobalStats( days );
			if ( !result.HasValue ) return Result.Fail;
			return result.Value.Result;
		}


		/// <summary>
		/// Gets a leaderboard by name, it will create it if it's not yet created.
		/// Leaderboards created with this function will not automatically show up in the Steam Community.
		/// You must manually set the Community Name field in the App Admin panel of the Steamworks website. 
		/// As such it's generally recommended to prefer creating the leaderboards in the App Admin panel on 
		/// the Steamworks website and using FindLeaderboard unless you're expected to have a large amount of
		/// dynamically created leaderboards.
		/// </summary>
		public static async Task<Leaderboard?> FindOrCreateLeaderboardAsync( string name, LeaderboardSort sort, LeaderboardDisplay display )
		{
			var result = await Internal.FindOrCreateLeaderboard( name, sort, display );
			if ( !result.HasValue || result.Value.LeaderboardFound == 0 )
				return null;

			return new Leaderboard { Id = result.Value.SteamLeaderboard };
		}


		public static async Task<Leaderboard?> FindLeaderboardAsync( string name )
		{
			var result = await Internal.FindLeaderboard( name );
			if ( !result.HasValue || result.Value.LeaderboardFound == 0 )
				return null;

			return new Leaderboard { Id = result.Value.SteamLeaderboard };
		}



		/// <summary>
		/// Adds this amount to the named stat. Internally this calls Get() and adds 
		/// to that value. Steam doesn't provide a mechanism for atomically increasing
		/// stats like this, this functionality is added here as a convenience.
		/// </summary>
		public static bool AddStat( string name, int amount = 1 )
		{
			var val = GetStatInt( name );
			val += amount;
			return SetStat( name, val );
		}

		/// <summary>
		/// Adds this amount to the named stat. Internally this calls Get() and adds 
		/// to that value. Steam doesn't provide a mechanism for atomically increasing
		/// stats like this, this functionality is added here as a convenience.
		/// </summary>
		public static bool AddStat( string name, float amount = 1.0f )
		{
			var val = GetStatFloat( name );
			val += amount;
			return SetStat( name, val );
		}

		/// <summary>
		/// Set a stat value. This will automatically call <see cref="StoreStats"/> after a successful call.
		/// </summary>
		public static bool SetStat( string name, int value )
		{
			return Internal.SetStat( name, value );
		}

		/// <summary>
		/// Set a stat value. This will automatically call <see cref="StoreStats"/> after a successful call.
		/// </summary>
		public static bool SetStat( string name, float value )
		{
			return Internal.SetStat( name, value );
		}

		/// <summary>
		/// Get an <see langword="int"/> stat value.
		/// </summary>
		public static int GetStatInt( string name )
		{
			int data = 0;
			Internal.GetStat( name, ref data );
			return data;
		}

		/// <summary>
		/// Get a <see langword="float"/> stat value.
		/// </summary>
		public static float GetStatFloat( string name )
		{
			float data = 0;
			Internal.GetStat( name, ref data );
			return data;
		}

		/// <summary>
		/// Practically wipes the slate clean for this user. If <paramref name="includeAchievements"/> is <see langword="true"/>, will also wipe
		/// any achievements too.
		/// </summary>
		/// <returns></returns>
		public static bool ResetAll( bool includeAchievements )
		{
			return Internal.ResetAllStats( includeAchievements );
		}
	}
}
