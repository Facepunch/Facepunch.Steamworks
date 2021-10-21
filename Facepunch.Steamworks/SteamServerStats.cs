using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Steamworks.Data;

namespace Steamworks
{
	public class SteamServerStats : SteamServerClass<SteamServerStats>
	{
		internal static ISteamGameServerStats Internal => Interface as ISteamGameServerStats;

		internal override void InitializeInterface( bool server )
		{
			SetInterface( server, new ISteamGameServerStats( server ) );
		}
		

		/// <summary>
		/// Downloads stats for the user.
		/// If the user has no stats, this will return <see cref="Result.Fail"/>.
		/// These stats will only be auto-updated for clients playing on the server.
		/// </summary>
		/// <param name="steamid">The SteamId of the user to get stats for.</param>
		/// <returns>A task describing the progress and result of the download.</returns>
		public static async Task<Result> RequestUserStatsAsync( SteamId steamid )
		{
			var r = await Internal.RequestUserStats( steamid );
			if ( !r.HasValue ) return Result.Fail;
			return r.Value.Result;
		}

		/// <summary>
		/// Set the named stat for this user. Setting stats should follow the rules
		/// you defined in Steamworks.
		/// </summary>
		/// <param name="steamid">The SteamId of the user to set stats for.</param>
		/// <param name="name">The name of the stat.</param>
		/// <param name="stat">The value of the stat.</param>
		public static bool SetInt( SteamId steamid, string name, int stat )
		{
			return Internal.SetUserStat( steamid, name, stat );
		}

		/// <summary>
		/// Set the named stat for this user. Setting stats should follow the rules
		/// you defined in Steamworks.
		/// </summary>
		/// <param name="steamid">The SteamId of the user to set stats for.</param>
		/// <param name="name">The name of the stat.</param>
		/// <param name="stat">The value of the stat.</param>
		public static bool SetFloat( SteamId steamid, string name, float stat )
		{
			return Internal.SetUserStat( steamid, name, stat );
		}

		/// <summary>
		/// Get the named stat for this user. If getting the stat failed, will return
		/// <paramref name="defaultValue"/>. You should have called <see cref="RequestUserStatsAsync(SteamId)"/> for this SteamID - which downloads
		/// the stats from the backend. If you didn't call it this will always return <paramref name="defaultValue"/>.
		/// </summary>
		/// <param name="steamid">The SteamId of the user to get stats for.</param>
		/// <param name="name">The name of the stat.</param>
		/// <param name="defaultValue">The value to return if the stats cannot be received.</param>
		public static int GetInt( SteamId steamid, string name, int defaultValue = 0 )
		{
			int data = defaultValue;

			if ( !Internal.GetUserStat( steamid, name, ref data ) )
				return defaultValue;

			return data;
		}

		/// <summary>
		/// Get the named stat for this user. If getting the stat failed, will return
		/// defaultValue. You should have called Refresh for this userid - which downloads
		/// the stats from the backend. If you didn't call it this will always return defaultValue.
		/// </summary>
		/// <param name="steamid">The SteamId of the user to get stats for.</param>
		/// <param name="name">The name of the stat.</param>
		/// <param name="defaultValue">The value to return if the stats cannot be received.</param>
		public static float GetFloat( SteamId steamid, string name, float defaultValue = 0 )
		{
			float data = defaultValue;

			if ( !Internal.GetUserStat( steamid, name, ref data ) )
				return defaultValue;

			return data;
		}

		/// <summary>
		/// Unlocks the specified achievement for the specified user. Must have called <see cref="RequestUserStatsAsync(SteamId)"/> on a SteamID first.
		/// Remember to use <see cref="StoreUserStats(SteamId)"/> after use.
		/// </summary>
		/// <param name="steamid">The SteamId of the user to unlock the achievement for.</param>
		/// <param name="name">The ID of the achievement.</param>
		public static bool SetAchievement( SteamId steamid, string name )
		{
			return Internal.SetUserAchievement( steamid, name );
		}

		/// <summary>
		/// Resets the unlock status of an achievement for the specified user. Must have called <see cref="RequestUserStatsAsync(SteamId)"/> on a SteamID first.
		/// Remember to use <see cref="StoreUserStats(SteamId)"/> after use.
		/// </summary>
		/// <param name="steamid">The SteamId of the user to clear the achievement for.</param>
		/// <param name="name">The ID of the achievement.</param>
		public static bool ClearAchievement( SteamId steamid, string name )
		{
			return Internal.ClearUserAchievement( steamid, name );
		}

		/// <summary>
		/// Return <see langword="true"/> if available, exists and unlocked
		/// </summary>
		public static bool GetAchievement( SteamId steamid, string name )
		{
			bool achieved = false;

			if ( !Internal.GetUserAchievement( steamid, name, ref achieved ) )
				return false;

			return achieved;
		}

		/// <summary>
		/// Once you've set a stat change on a user you need to commit your changes.
		/// You can do that using this method. The callback will let you know if
		/// your action succeeded, but most of the time you can fire and forget.
		/// </summary>
		/// <param name="steamid">The SteamId of the user to store stats for.</param>
		/// <returns>A task describing the progress and result of the commit.</returns>
		public static async Task<Result> StoreUserStats( SteamId steamid )
		{
			var r = await Internal.StoreUserStats( steamid );
			if ( !r.HasValue ) return Result.Fail;
			return r.Value.Result;
		}
	}
}
