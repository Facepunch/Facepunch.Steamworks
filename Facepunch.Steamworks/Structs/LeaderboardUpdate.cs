using System.Linq;

namespace Steamworks.Data
{
	public struct LeaderboardUpdate
	{
		private readonly LeaderboardScoreUploaded_t _internal;

		public bool Success => _internal.Success != 0;
		public int Score => _internal.Score;
		public bool Changed => _internal.ScoreChanged != 0;
		public int NewGlobalRank => _internal.GlobalRankNew;
		public int OldGlobalRank => _internal.GlobalRankPrevious;
		public int RankChange => NewGlobalRank - OldGlobalRank;

		internal LeaderboardUpdate( LeaderboardScoreUploaded_t result )
		{
			_internal = result;
		}
	}
}
