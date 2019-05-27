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
		static int[] noDetails = new int[0];

		/// <summary>
		/// Submit your score and replace your old score even if it was better
		/// </summary>
		public async Task<LeaderboardUpdate?> ReplaceScore( int score, int[] details = null )
		{
			if ( details == null ) details = noDetails;

			var r = await SteamUserStats.Internal.UploadLeaderboardScore( Id, LeaderboardUploadScoreMethod.ForceUpdate, score, details, details.Length );
			if ( !r.HasValue ) return null;

			return LeaderboardUpdate.From( r.Value );
		}

		/// <summary>
		/// Submit your new score, but won't replace your high score if it's lower
		/// </summary>
		public async Task<LeaderboardUpdate?> SubmitScoreAsync( int score, int[] details = null )
		{
			if ( details == null ) details = noDetails;

			var r = await SteamUserStats.Internal.UploadLeaderboardScore( Id, LeaderboardUploadScoreMethod.KeepBest, score, details, details.Length );
			if ( !r.HasValue ) return null;

			return LeaderboardUpdate.From( r.Value );
		}

		/// <summary>
		/// Attaches a piece of user generated content the user's entry on a leaderboard
		/// </summary>
		public async Task<Result> AttachUgc( Ugc file )
		{
			var r = await SteamUserStats.Internal.AttachLeaderboardUGC( Id, file.Handle );
			if ( !r.HasValue ) return Result.Fail;

			return r.Value.Result;
		}

		/// <summary>
		/// Used to query for a sequential range of leaderboard entries by leaderboard Sort.
		/// </summary>
		public async Task<LeaderboardEntry[]> GetScoresAsync( int count, int offset = 1 )
		{
			if ( offset <= 0 ) throw new System.ArgumentException( "Should be 1+", nameof( offset ) );

			var r = await SteamUserStats.Internal.DownloadLeaderboardEntries( Id, LeaderboardDataRequest.Global, offset, offset + count );
			if ( !r.HasValue )
				return null;

			return await LeaderboardResultToEntries( r.Value );
		}

		/// <summary>
		/// Used to retrieve leaderboard entries relative a user's entry. If there are not enough entries in the leaderboard 
		/// before or after the user's entry, Steam will adjust the range to try to return the number of entries requested.
		/// For example, if the user is #1 on the leaderboard and start is set to -2, end is set to 2, Steam will return the first 
		/// 5 entries in the leaderboard. If The current user has no entry, this will return null.
		/// </summary>
		public async Task<LeaderboardEntry[]> GetScoresAroundUserAsync( int start = -10, int end = 10 )
		{
			var r = await SteamUserStats.Internal.DownloadLeaderboardEntries( Id, LeaderboardDataRequest.GlobalAroundUser, start, end );
			if ( !r.HasValue )
				return null;

			return await LeaderboardResultToEntries( r.Value );
		}

		/// <summary>
		/// Used to retrieve all leaderboard entries for friends of the current user
		/// </summary>
		public async Task<LeaderboardEntry[]> GetScoresFromFriendsAsync()
		{
			var r = await SteamUserStats.Internal.DownloadLeaderboardEntries( Id, LeaderboardDataRequest.Friends, 0, 0 );
			if ( !r.HasValue )
				return null;

			return await LeaderboardResultToEntries( r.Value );
		}

		#region util
		internal async Task<LeaderboardEntry[]> LeaderboardResultToEntries( LeaderboardScoresDownloaded_t r )
		{
			if ( r.CEntryCount <= 0 )
				return null;

			var output = new LeaderboardEntry[r.CEntryCount];
			var e = default( LeaderboardEntry_t );

			for ( int i = 0; i < output.Length; i++ )
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