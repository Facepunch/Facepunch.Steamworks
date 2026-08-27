using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Steamworks.Data
{
	/// <summary>
	/// A resolved handle to one Steam leaderboard — the ranked score tables shown on a game's
	/// community hub and in the in-game overlay. Use it to submit the local player's score and to
	/// pull back slices of the table: the global top N, the rows either side of the player, or just
	/// their friends.
	/// </summary>
	/// <remarks>
	/// <para>
	/// <b>You cannot construct one of these.</b> The only way to get a <see cref="Leaderboard"/> is
	/// to resolve a name asynchronously through <see cref="SteamUserStats.FindLeaderboardAsync"/> or
	/// <see cref="SteamUserStats.FindOrCreateLeaderboardAsync"/>. That lookup is a network round trip
	/// entirely separate from reading scores, and it returns <see langword="null"/> when the
	/// leaderboard does not exist — <see cref="SteamUserStats.FindLeaderboardAsync"/> collapses "no
	/// such leaderboard" and "the request failed" into that same <see langword="null"/>, so a typo in
	/// the name is indistinguishable from being offline.
	/// </para>
	/// <para>
	/// Every score read below is then a <i>second</i> round trip. Plan for two awaits before you can
	/// draw a scoreboard, resolve the handle once at startup, and cache it — handles stay valid for
	/// the session.
	/// </para>
	/// <para>
	/// Leaderboards are independent of <see cref="SteamUserStats.StatsReceived"/> and work before the
	/// local user's stats have arrived. They are also independent of <c>StoreStats</c>: a submitted
	/// score goes straight to Valve's servers, there is nothing to flush.
	/// </para>
	/// </remarks>
	/// <example>
	/// <code>
	/// // First round trip: name -> handle. Do this once, keep the result.
	/// var board = await SteamUserStats.FindLeaderboardAsync( "Fastest Lap" );
	/// if ( board == null ) return; // Missing leaderboard, or the lookup failed.
	///
	/// // Submitting: KeepBest, so a slower lap will not overwrite a faster one.
	/// var update = await board.Value.SubmitScoreAsync( lapTimeMilliseconds );
	/// if ( update.HasValue &amp;&amp; update.Value.Changed )
	/// {
	///     ShowNewPersonalBest( update.Value.NewGlobalRank, update.Value.RankChange );
	/// }
	///
	/// // Second round trip: reading. offset is 1-based, so this is ranks 1-10.
	/// var top10 = await board.Value.GetScoresAsync( 10 );
	/// foreach ( var e in top10 ?? Array.Empty&lt;LeaderboardEntry&gt;() )
	/// {
	///     Console.WriteLine( $"#{e.GlobalRank} {e.User.Name} {e.Score}" );
	/// }
	/// </code>
	/// </example>
	public struct Leaderboard
	{
		internal SteamLeaderboard_t Id;

		/// <summary>
		/// The leaderboard's internal name — the one you passed to
		/// <see cref="SteamUserStats.FindLeaderboardAsync"/>, not the Community Name shown on the
		/// Steam website.
		/// </summary>
		/// <value>
		/// The name Steam has cached for this handle, or an empty string if the handle is not
		/// recognised. This is a local lookup and does not hit the network.
		/// </value>
		public string Name => SteamUserStats.Internal.GetLeaderboardName( Id );

		/// <summary>
		/// Which direction counts as "best" on this leaderboard, and therefore what rank 1 means.
		/// Read it before rendering scores so you order and label them the way the board is actually
		/// configured.
		/// </summary>
		/// <value>
		/// <see cref="LeaderboardSort.Ascending"/> when the lowest score is the top score (lap times,
		/// stroke counts), or <see cref="LeaderboardSort.Descending"/> when the highest score wins.
		/// Set when the leaderboard was created and not changeable at runtime.
		/// </value>
		public LeaderboardSort Sort => SteamUserStats.Internal.GetLeaderboardSortMethod( Id );

		/// <summary>
		/// How the Steam Community website formats this board's score column. Mirror it in your own
		/// UI so a score reading "94500" is not shown as a raw number when Steam presents it as
		/// 1:34.500.
		/// </summary>
		/// <value>
		/// <see cref="LeaderboardDisplay.Numeric"/>, <see cref="LeaderboardDisplay.TimeSeconds"/> or
		/// <see cref="LeaderboardDisplay.TimeMilliSeconds"/>. Presentation only — it does not change
		/// the integer you submit or read back.
		/// </value>
		public LeaderboardDisplay Display => SteamUserStats.Internal.GetLeaderboardDisplayType( Id );

		/// <summary>
		/// How many players have an entry on this leaderboard — the <c>N</c> that
		/// <see cref="LeaderboardEntry.GlobalRank"/> is out of. Use it to size pagination or to show
		/// "rank 412 of 90,331".
		/// </summary>
		/// <value>
		/// The total entry count. Valve is explicit that this is the count <i>as of the last
		/// request</i>: it is a cached number, refreshed by the find and download calls, not a live
		/// query. Reading it immediately after resolving the handle and before any download can
		/// therefore give you a stale or zero count.
		/// </value>
		public int EntryCount => SteamUserStats.Internal.GetLeaderboardEntryCount(Id);

		static int[] detailsBuffer = new int[64];
		static int[] noDetails = Array.Empty<int>();

		/// <summary>
		/// Uploads the local player's score, overwriting whatever they had before <b>even if the old
		/// score was better</b>. Use this for "current season" or "latest attempt" boards where the
		/// most recent result is the one that counts; use <see cref="SubmitScoreAsync"/> for
		/// personal bests.
		/// </summary>
		/// <param name="score">
		/// The score to record. Always an <see langword="int"/> — if <see cref="Display"/> is a time
		/// type, encode it as whole seconds or whole milliseconds to match.
		/// </param>
		/// <param name="details">
		/// Optional game-defined context stored alongside the entry and returned in
		/// <see cref="LeaderboardEntry.Details"/> — a replay seed, the loadout used, the track
		/// variant. Valve caps this at 64 <see langword="int"/> values per entry. Pass
		/// <see langword="null"/> (the default) for none; this binding substitutes an empty array.
		/// </param>
		/// <returns>
		/// The outcome of the upload, or <see langword="null"/> if the call did not complete at all.
		/// </returns>
		/// <remarks>
		/// <b>A non-null result does not mean the upload succeeded.</b> Steam reports a success flag
		/// alongside the new ranking and this binding does not surface it, so a server-side failure
		/// comes back as a <see cref="LeaderboardUpdate"/> full of zeros rather than as
		/// <see langword="null"/>. Treat <see cref="LeaderboardUpdate.NewGlobalRank"/> of <c>0</c> as
		/// suspect. Uploads go straight to Valve — there is no local staging and no <c>StoreStats</c>
		/// step.
		/// </remarks>
		public async Task<LeaderboardUpdate?> ReplaceScore( int score, int[] details = null )
		{
			if ( details == null ) details = noDetails;

			var r = await SteamUserStats.Internal.UploadLeaderboardScore( Id, LeaderboardUploadScoreMethod.ForceUpdate, score, details, details.Length );
			if ( !r.HasValue ) return null;

			return LeaderboardUpdate.From( r.Value );
		}

		/// <summary>
		/// Uploads the local player's score, keeping their existing entry if it was better. This is
		/// the normal way to submit — Steam decides what "better" means from <see cref="Sort"/>, so
		/// you do not have to compare against the old score yourself.
		/// </summary>
		/// <param name="score">
		/// The score to submit. Encode times as whole seconds or milliseconds to match
		/// <see cref="Display"/>.
		/// </param>
		/// <param name="details">
		/// Optional game-defined context stored with the entry, surfaced as
		/// <see cref="LeaderboardEntry.Details"/>. Valve's ceiling is 64 <see langword="int"/> values.
		/// <see langword="null"/> (the default) means none. Note that when the score is <i>not</i>
		/// beaten these details are not applied either — the whole old entry is kept.
		/// </param>
		/// <returns>
		/// The outcome of the upload, or <see langword="null"/> if the call did not complete.
		/// Inspect <see cref="LeaderboardUpdate.Changed"/> to find out whether this score actually
		/// beat the previous one: a rejected-because-worse submission is a normal, successful result,
		/// not an error.
		/// </returns>
		/// <remarks>
		/// As with <see cref="ReplaceScore"/>, a non-null result is not proof of success — Steam's
		/// upload success flag is discarded by this binding, so a genuine failure surfaces as a
		/// zero-filled <see cref="LeaderboardUpdate"/>.
		/// </remarks>
		public async Task<LeaderboardUpdate?> SubmitScoreAsync( int score, int[] details = null )
		{
			if ( details == null ) details = noDetails;

			var r = await SteamUserStats.Internal.UploadLeaderboardScore( Id, LeaderboardUploadScoreMethod.KeepBest, score, details, details.Length );
			if ( !r.HasValue ) return null;

			return LeaderboardUpdate.From( r.Value );
		}

		/// <summary>
		/// Attaches a shared file to the local player's entry on this leaderboard — the mechanism
		/// behind "watch the world record's replay" or "download the ghost". Other players fetch it
		/// from the UGC handle on the downloaded entry.
		/// </summary>
		/// <param name="file">
		/// A handle to content already shared through Steam Remote Storage. Valve is specific that
		/// this must come from a <c>FileShare</c> call — an ordinary Remote Storage file that has not
		/// been shared will not work.
		/// </param>
		/// <returns>
		/// <see cref="Result.OK"/> if the attachment was accepted. <see cref="Result.Fail"/> is
		/// returned both by Steam for a genuine rejection and by this binding when the call did not
		/// complete at all, so it does not by itself tell you which happened.
		/// </returns>
		/// <remarks>
		/// The player must already have an entry on this leaderboard — submit a score first. Note
		/// that this binding's <see cref="LeaderboardEntry"/> does not currently expose the attached
		/// UGC handle back to you, so attaching content here is only useful to code that obtains the
		/// handle another way.
		/// </remarks>
		public async Task<Result> AttachUgc( Ugc file )
		{
			var r = await SteamUserStats.Internal.AttachLeaderboardUGC( Id, file.Handle );
			if ( !r.HasValue ) return Result.Fail;

			return r.Value.Result;
		}

		/// <summary>
		/// Downloads this leaderboard's entries for a specific list of players — your lobby, your
		/// clan, the eight people in the current match — rather than a rank range.
		/// </summary>
		/// <param name="users">
		/// The players to look up. Valve caps this at <b>100 users per call</b> and allows only
		/// <b>one outstanding request at a time</b>; neither limit is enforced by this binding, so
		/// batch the list yourself and await each batch before starting the next.
		/// </param>
		/// <returns>
		/// One entry per player who actually has a score here — Valve omits users with no entry, so
		/// the array is usually shorter than <paramref name="users"/> and the order does not match.
		/// Match results back by <see cref="LeaderboardEntry.User"/>, never by index. Returns
		/// <see langword="null"/> if <paramref name="users"/> is <see langword="null"/> or empty, if
		/// the request failed, or if none of the requested users had an entry — those cases are not
		/// distinguishable.
		/// </returns>
		/// <remarks>
		/// Before returning, this waits for Steam to resolve each entrant's persona name so
		/// <c>entry.User.Name</c> is populated. That wait has no timeout and only progresses while
		/// callbacks are being pumped.
		/// </remarks>
		public async Task<LeaderboardEntry[]> GetScoresForUsersAsync( SteamId[] users )
		{
			if ( users == null || users.Length == 0 )
				return null;

			var r = await SteamUserStats.Internal.DownloadLeaderboardEntriesForUsers( Id, users, users.Length );
			if ( !r.HasValue )
				return null;

			return await LeaderboardResultToEntries( r.Value );
		}

		/// <summary>
		/// Downloads a page of the global table, in rank order — "the top 10", "ranks 51 to 100".
		/// The building block for a paginated scoreboard.
		/// </summary>
		/// <param name="count">
		/// How many entries to fetch. Valve returns as many as exist, so asking for more than the
		/// leaderboard holds is safe rather than an error. A <paramref name="count"/> of <c>0</c> or
		/// less produces an empty range and comes back as <see langword="null"/>.
		/// </param>
		/// <param name="offset">
		/// The rank to start at, <b>1-based</b> — <c>1</c> is the top-ranked player, not <c>0</c>.
		/// Page two of a ten-per-page list is <c>offset: 11</c>. Which score is rank 1 depends on
		/// <see cref="Sort"/>.
		/// </param>
		/// <returns>
		/// The requested entries in rank order, or <see langword="null"/>. <see langword="null"/>
		/// covers a failed request <i>and</i> a range that simply contains no rows (an empty
		/// leaderboard, or an offset past the end) — this binding does not distinguish them, and it
		/// never returns an empty array.
		/// </returns>
		/// <exception cref="System.ArgumentException">
		/// <paramref name="offset"/> is <c>0</c> or negative. Ranks start at 1; use
		/// <see cref="GetScoresAroundUserAsync"/> if you wanted a range relative to the player.
		/// </exception>
		/// <remarks>
		/// A network round trip on every call, separate from the
		/// <see cref="SteamUserStats.FindLeaderboardAsync"/> lookup that produced this handle. Before
		/// returning it waits for every entrant's persona name to resolve, which has no timeout and
		/// requires callbacks to be pumping. Individual rows that Steam fails to read back are left
		/// as default-valued entries with a zero <see cref="LeaderboardEntry.User"/> rather than
		/// being skipped.
		/// </remarks>
		public async Task<LeaderboardEntry[]> GetScoresAsync( int count, int offset = 1 )
		{
			if ( offset <= 0 ) throw new System.ArgumentException( "Should be 1+", nameof( offset ) );

			var r = await SteamUserStats.Internal.DownloadLeaderboardEntries( Id, LeaderboardDataRequest.Global, offset, offset + count - 1 );
			if ( !r.HasValue )
				return null;

			return await LeaderboardResultToEntries( r.Value );
		}

		/// <summary>
		/// Downloads the rows immediately above and below the local player's own entry — the
		/// "you're here" window that makes a leaderboard meaningful to someone ranked 40,000th.
		/// </summary>
		/// <param name="start">
		/// Offset of the first row relative to the player, where <c>0</c> is the player themselves.
		/// Normally <b>negative</b> to reach players ranked above them: <c>-10</c> means ten places
		/// better.
		/// </param>
		/// <param name="end">
		/// Offset of the last row relative to the player. Positive values reach players ranked below.
		/// The window is inclusive, so the defaults of <c>-10</c> and <c>10</c> ask for 21 rows.
		/// </param>
		/// <returns>
		/// The window of entries in rank order, or <see langword="null"/> if the local player has no
		/// entry on this leaderboard yet, if the request failed, or if the leaderboard is empty —
		/// again indistinguishable. Submit a score before expecting this to return anything.
		/// </returns>
		/// <remarks>
		/// <para>
		/// The window is clamped, not centred: Valve slides the range to return the number of rows
		/// requested where it can. A player at rank 1 asking for <c>-2</c> to <c>2</c> gets the top
		/// five entries, not three. So do not assume the player's own row sits at the array's
		/// midpoint — find it by comparing <see cref="LeaderboardEntry.User"/>.
		/// </para>
		/// <para>
		/// A network round trip, and it waits for persona names before returning.
		/// </para>
		/// </remarks>
		public async Task<LeaderboardEntry[]> GetScoresAroundUserAsync( int start = -10, int end = 10 )
		{
			var r = await SteamUserStats.Internal.DownloadLeaderboardEntries( Id, LeaderboardDataRequest.GlobalAroundUser, start, end );
			if ( !r.HasValue )
				return null;

			return await LeaderboardResultToEntries( r.Value );
		}

		/// <summary>
		/// Downloads every entry belonging to the local player's Steam friends, plus their own. The
		/// friends-only view is usually far more motivating than a global table nobody can reach the
		/// top of.
		/// </summary>
		/// <returns>
		/// The friends' entries in rank order, or <see langword="null"/> if no friend (and not the
		/// player themselves) has an entry here, or if the request failed. There is no page size to
		/// choose — Steam returns the whole set.
		/// </returns>
		/// <remarks>
		/// Ranks in these entries are still <i>global</i> ranks, not 1..N within the friend group, so
		/// display them as "#4,812 among your friends' scores" or renumber them yourself. A network
		/// round trip, and it waits for persona names before returning.
		/// </remarks>
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

		internal static async Task WaitForUserNames( LeaderboardEntry[] entries)
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