using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Steamworks.Data
{
	public struct Leaderboard
	{
		internal SteamLeaderboard_t Id;

		/// <summary>
		/// the name of a leaderboard
		/// </summary>
		public string Name => SteamUserStats.Internal.GetLeaderboardName( Id );
		public LeaderboardSort Sort => SteamUserStats.Internal.GetLeaderboardSortMethod( Id );
		public LeaderboardDisplay Display => SteamUserStats.Internal.GetLeaderboardDisplayType( Id );

		static int[] detailsBuffer = new int[64];

		/// <summary>
		/// Used to query for a sequential range of leaderboard entries by leaderboard Sort.
		/// </summary>
		public async Task<LeaderboardEntry[]> GetGlobalEntriesAsync( int count, int offset = 1 )
		{
			if ( offset <= 0 ) throw new System.ArgumentException( "Should be 1+", nameof( offset ) );

			var r = await SteamUserStats.Internal.DownloadLeaderboardEntries( Id, LeaderboardDataRequest.Global, offset, offset + count );
			if ( !r.HasValue )
				return null;

			return await LeaderboardResultToEntries( r.Value );
		}

		#region util
		internal async Task<LeaderboardEntry[]> LeaderboardResultToEntries( LeaderboardScoresDownloaded_t r )
		{
			if ( r.CEntryCount == 0 )
				return null;

			var output = new LeaderboardEntry[r.CEntryCount];
			var e = default( LeaderboardEntry_t );

			for ( int i = 0; i < r.CEntryCount; i++ )
			{
				if ( SteamUserStats.Internal.GetDownloadedLeaderboardEntry( r.SteamLeaderboardEntries, i, ref e, detailsBuffer, detailsBuffer.Length ) )
				{
					output[i] = LeaderboardEntry.From( e, detailsBuffer );
				}
			}

			await WaitForUserNames( output );

			return output;
		}

		internal async Task WaitForUserNames( LeaderboardEntry[] entries)
		{
			bool gotAll = false;
			while ( !gotAll )
			{
				gotAll = true;

				foreach ( var entry in entries )
				{
					if ( entry.User.Id == 0 ) continue;
					if ( !SteamFriends.Internal.RequestUserInformation( entry.User.Id, true ) ) continue;

					gotAll = false;
				}

				await Task.Delay( 1 );
			}
		}
		#endregion
	}
}