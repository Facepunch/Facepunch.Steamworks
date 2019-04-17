using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Steamworks
{
    [TestClass]
    [DeploymentItem( "steam_api64.dll" )]
    public class UserStatsTest
	{
		[TestMethod]
        public void AchievementList()
        {
			foreach ( var a in SteamUserStats.Achievements )
			{
				Console.WriteLine( $"{a.Identifier}" );
				Console.WriteLine( $"	a.State: {a.State}" );
				Console.WriteLine( $"	a.UnlockTime: {a.UnlockTime}" );
				Console.WriteLine( $"	a.Name: {a.Name}" );
				Console.WriteLine( $"	a.Description: {a.Description}" );
				Console.WriteLine( $"	a.GlobalUnlocked:	{a.GlobalUnlocked}" );

				var icon = a.GetIcon();

				Console.WriteLine( $"	a.Icon:	{icon}" );
			}			
		}

		[TestMethod]
		public async Task StoreStats()
		{
			var result = Result.NotSettled;

			SteamUserStats.OnUserStatsStored += ( r ) =>
			{
				result = r;
			};

			SteamUserStats.StoreStats();

			while ( result  == Result.NotSettled )
			{
				await Task.Delay( 10 );
			}

			Assert.AreEqual( result, Result.OK );

		}

		[TestMethod]
		public async Task CreateLeaderboard()
		{
			var leaderboard = await SteamUserStats.FindOrCreateLeaderboard( "Testleaderboard", Data.LeaderboardSort.Descending, Data.LeaderboardDisplay.Numeric );
			
			Assert.IsTrue( leaderboard.HasValue );
		}

		[TestMethod]
		public async Task FindLeaderboard()
		{
			var leaderboard = await SteamUserStats.FindLeaderboard( "Testleaderboard" );
			Assert.IsTrue( leaderboard.HasValue );

			// Get top 20 global scores
			var globalsScores = await leaderboard.Value.GetGlobalEntriesAsync( 20 );
			Assert.IsNotNull( globalsScores );

			foreach ( var e in globalsScores )
			{
				Console.WriteLine( $"{e.GlobalRank}: {e.Score} {e.User}" );
			}

		}

	}

}
