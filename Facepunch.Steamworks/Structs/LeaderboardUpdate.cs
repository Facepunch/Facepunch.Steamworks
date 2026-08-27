using System.Linq;

namespace Steamworks.Data
{
	/// <summary>
	/// What Steam did with a score you submitted: whether it beat the player's previous entry, and
	/// where they now sit on the table. This is the payload you build a "New personal best — you
	/// climbed 340 places!" banner from.
	/// </summary>
	/// <remarks>
	/// <para>
	/// Returned by <see cref="Leaderboard.SubmitScoreAsync"/> and
	/// <see cref="Leaderboard.ReplaceScore"/>. A plain snapshot with no live link to Steam.
	/// </para>
	/// <para>
	/// <b>Getting a value back is not proof the upload succeeded.</b> Steam reports a separate
	/// success flag that this binding does not expose, so a server-side failure arrives as a
	/// zero-filled instance rather than as <see langword="null"/>. A result with
	/// <see cref="NewGlobalRank"/> of <c>0</c> should be treated as a failure, since a real entry is
	/// always ranked 1 or better-numbered.
	/// </para>
	/// </remarks>
	/// <example>
	/// <code>
	/// var r = await board.SubmitScoreAsync( score );
	/// if ( r == null || r.Value.NewGlobalRank == 0 ) return; // failed
	///
	/// if ( !r.Value.Changed )
	/// {
	///     ShowMessage( "Your existing score was better." );
	/// }
	/// else if ( r.Value.OldGlobalRank == 0 )
	/// {
	///     ShowMessage( $"First entry! You are ranked #{r.Value.NewGlobalRank}." );
	/// }
	/// else
	/// {
	///     // Rank 1 is best, so improving means RankChange is negative.
	///     ShowMessage( $"Climbed {-r.Value.RankChange} places to #{r.Value.NewGlobalRank}." );
	/// }
	/// </code>
	/// </example>
	public struct LeaderboardUpdate
	{
		/// <summary>
		/// The score that was submitted, echoed back by Steam. Note this is the score you
		/// <i>attempted</i> to set, not necessarily the one now on the leaderboard — when
		/// <see cref="Changed"/> is <see langword="false"/> the player's older, better score is still
		/// the one that stands.
		/// </summary>
		public int Score;

		/// <summary>
		/// Whether this submission actually replaced the player's entry. Gate your "new personal
		/// best" celebration on this.
		/// </summary>
		/// <remarks>
		/// <see langword="false"/> means the existing score was better and was kept — a completely
		/// normal outcome for <see cref="Leaderboard.SubmitScoreAsync"/>, not an error. Steam decides
		/// what "better" means from the board's <see cref="Leaderboard.Sort"/>.
		/// </remarks>
		public bool Changed;

		/// <summary>
		/// The player's rank on this leaderboard after the submission, <c>1</c> being the best.
		/// </summary>
		/// <remarks>
		/// <c>0</c> is not a valid rank and indicates the upload failed — this binding has no other
		/// way to report that.
		/// </remarks>
		public int NewGlobalRank;

		/// <summary>
		/// The player's rank before this submission, or <c>0</c> if they had no entry on this
		/// leaderboard at all.
		/// </summary>
		/// <remarks>
		/// Valve is explicit that <c>0</c> here is the "no previous entry" sentinel rather than a
		/// rank. Check for it before doing arithmetic — see the warning on <see cref="RankChange"/>.
		/// </remarks>
		public int OldGlobalRank;

		/// <summary>
		/// How far the player moved, as <see cref="NewGlobalRank"/> minus <see cref="OldGlobalRank"/>.
		/// Because rank 1 is the best, <b>a negative value means the player improved</b> and a
		/// positive value means they slipped.
		/// </summary>
		/// <value>
		/// The signed change in rank. <b>Meaningless when <see cref="OldGlobalRank"/> is <c>0</c>:</b>
		/// a first-ever submission yields <c>NewGlobalRank - 0</c>, so a debut at rank 900 reads as a
		/// 900-place drop. Test <see cref="OldGlobalRank"/> for <c>0</c> and report "first entry"
		/// instead. It is likewise meaningless after a failed upload, where both ranks are <c>0</c>.
		/// </value>
		public int RankChange => NewGlobalRank - OldGlobalRank;

		internal static LeaderboardUpdate From( LeaderboardScoreUploaded_t e ) =>
			new LeaderboardUpdate
			{
				Score = e.Score,
				Changed = e.ScoreChanged == 1,
				NewGlobalRank = e.GlobalRankNew,
				OldGlobalRank = e.GlobalRankPrevious
			};
	}
}