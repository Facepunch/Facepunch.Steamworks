using System.Linq;

namespace Steamworks.Data
{
	public struct LeaderboardEntry
	{
		public Friend User;
		public int GlobalRank;
		public int Score;
		public int[] Details;
		public PublishedFileId? AttachedUgcId;

		internal static LeaderboardEntry From( LeaderboardEntry_t e, int[] detailsBuffer )
		{
			var r = new LeaderboardEntry
			{
				User = new Friend( e.SteamIDUser ),
				GlobalRank = e.GlobalRank,
				Score = e.Score,
				Details = null
			};
			if ( e.UGC != 0 )
				r.AttachedUgcId = e.UGC;

			if ( e.CDetails > 0 )
			{
				r.Details = detailsBuffer.Take( e.CDetails ).ToArray();
			}

			return r;
		}
	}
}