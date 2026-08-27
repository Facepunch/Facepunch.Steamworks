using System.Linq;

namespace Steamworks.Data
{
	/// <summary>
	/// One row of a leaderboard: who, what rank, what score. A snapshot taken when the surrounding
	/// download completed — it is a plain value with no live connection to Steam, so it never
	/// refreshes and holding onto it is free.
	/// </summary>
	/// <remarks>
	/// <para>
	/// You never build these yourself; they come back from the <c>GetScores…</c> methods on
	/// <see cref="Leaderboard"/>. Those methods have already waited for each entrant's persona name
	/// to arrive, so <c>User.Name</c> is populated by the time you see one.
	/// </para>
	/// <para>
	/// <b>Watch for zero-filled rows.</b> If Steam fails to read an individual row back out of a
	/// downloaded result set, this binding leaves that array slot at its default rather than dropping
	/// it — a row with <see cref="GlobalRank"/> <c>0</c> and a <see cref="User"/> whose
	/// <c>Id</c> is <c>0</c> is such a placeholder, not a real player.
	/// </para>
	/// <para>
	/// Valve's native row also carries a UGC handle for content attached with
	/// <see cref="Leaderboard.AttachUgc"/>; this binding does not expose it (see the commented-out
	/// field in the source).
	/// </para>
	/// </remarks>
	/// <example>
	/// <code>
	/// foreach ( var entry in await board.GetScoresAsync( 10 ) )
	/// {
	///     if ( entry.User.Id == 0 ) continue; // placeholder row, skip it
	///
	///     var seed = entry.Details?.Length &gt; 0 ? entry.Details[0] : 0;
	///     Console.WriteLine( $"#{entry.GlobalRank} {entry.User.Name} {entry.Score} (seed {seed})" );
	/// }
	/// </code>
	/// </example>
	public struct LeaderboardEntry
	{
		/// <summary>
		/// The player who set this score. Use it to read their persona name and avatar, or to compare
		/// against the local user to find "your" row in a downloaded page.
		/// </summary>
		/// <remarks>
		/// An <c>Id</c> of <c>0</c> means this is a placeholder for a row that could not be read, not
		/// an anonymous player.
		/// </remarks>
		public Friend User;

		/// <summary>
		/// This entry's position on the leaderboard, <c>1</c> being the best. Always the position in
		/// the <i>global</i> table, even in results from
		/// <see cref="Leaderboard.GetScoresFromFriendsAsync"/>.
		/// </summary>
		/// <remarks>
		/// Valve defines the range as <c>[1..N]</c> where <c>N</c> is the number of players with an
		/// entry — see <see cref="Leaderboard.EntryCount"/>. Which score earns rank 1 depends on the
		/// board's <see cref="Leaderboard.Sort"/>, so a rank of 1 does not imply the highest number.
		/// </remarks>
		public int GlobalRank;

		/// <summary>
		/// The score exactly as it was uploaded. Format it according to the board's
		/// <see cref="Leaderboard.Display"/> — the same integer can mean points, seconds or
		/// milliseconds.
		/// </summary>
		public int Score;

		/// <summary>
		/// The game-defined context uploaded alongside the score — replay seed, loadout, track
		/// variant — or <see langword="null"/> when the entry has none.
		/// </summary>
		/// <remarks>
		/// <see langword="null"/> rather than empty when there are no details, so null-check before
		/// indexing. Length is whatever the submitter passed, up to Valve's ceiling of 64 values, and
		/// its meaning is entirely yours to define; nothing validates that an old entry's layout
		/// still matches what your current build expects.
		/// </remarks>
		public int[] Details;
		// UGCHandle_t m_hUGC

		internal static LeaderboardEntry From( LeaderboardEntry_t e, int[] detailsBuffer )
		{
			var r = new LeaderboardEntry
			{
				User = new Friend( e.SteamIDUser ),
				GlobalRank = e.GlobalRank,
				Score = e.Score,
				Details = null
			};

			if ( e.CDetails > 0 )
			{
				r.Details = detailsBuffer.Take( e.CDetails ).ToArray();
			}

			return r;
		}
	}
}