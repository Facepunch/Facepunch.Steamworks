using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Steamworks.Data;

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
		public async Task PlayerCountAsync()
		{
			var players = await SteamUserStats.PlayerCountAsync();
			Assert.AreNotEqual( players, -1 );
			Console.WriteLine( $"players:	{players}" );
		}
		
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
			var leaderboard = await SteamUserStats.FindOrCreateLeaderboard( "Testleaderboard", Data.LeaderboardSort.Ascending, Data.LeaderboardDisplay.Numeric );
			
			Assert.IsTrue( leaderboard.HasValue );
		}

		[TestMethod]
		public async Task FindLeaderboard()
		{
			var leaderboard = await SteamUserStats.FindLeaderboard( "Testleaderboard" );
			Assert.IsTrue( leaderboard.HasValue );
		}

		[TestMethod]
		public async Task SubmitScore()
		{
			var leaderboard = await SteamUserStats.FindLeaderboard( "Testleaderboard" );
			Assert.IsTrue( leaderboard.HasValue );

			var result = await leaderboard.Value.SubmitScore( 576 );
			Assert.IsTrue( result.HasValue );

			Console.WriteLine( $"result.Changed: {result?.Changed}" );
			Console.WriteLine( $"result.OldGlobalRank: {result?.OldGlobalRank}" );
			Console.WriteLine( $"result.NewGlobalRank: {result?.NewGlobalRank}" );
			Console.WriteLine( $"result.RankChange: {result?.RankChange}" );
			Console.WriteLine( $"result.Score: {result?.Score}" );
		}

		[TestMethod]
		public async Task ReplaceScore()
		{
			var leaderboard = await SteamUserStats.FindLeaderboard( "Testleaderboard" );
			Assert.IsTrue( leaderboard.HasValue );

			var result = await leaderboard.Value.ReplaceScore( 576 );
			Assert.IsTrue( result.HasValue );

			Console.WriteLine( $"result.Changed: {result?.Changed}" );
			Console.WriteLine( $"result.OldGlobalRank: {result?.OldGlobalRank}" );
			Console.WriteLine( $"result.NewGlobalRank: {result?.NewGlobalRank}" );
			Console.WriteLine( $"result.RankChange: {result?.RankChange}" );
			Console.WriteLine( $"result.Score: {result?.Score}" );
		}

		[TestMethod]
		public async Task GetScoresFromFriends()
		{
			var leaderboard = await SteamUserStats.FindLeaderboard( "Testleaderboard" );
			Assert.IsTrue( leaderboard.HasValue );

			// Get entries around user
			var friendScores = await leaderboard.Value.GetScoresFromFriends();
			Assert.IsNotNull( friendScores );

			Console.WriteLine( $"" );
			Console.WriteLine( $"Friend Scores:" );
			foreach ( var e in friendScores )
			{
				Console.WriteLine( $"{e.GlobalRank}: {e.Score} {e.User}" );
			}
		}

		[TestMethod]
		public async Task GetScoresAroundUserAsync()
		{
			var leaderboard = await SteamUserStats.FindLeaderboard( "Testleaderboard" );
			Assert.IsTrue( leaderboard.HasValue );

			// Get entries around user
			var relativeScores = await leaderboard.Value.GetScoresAroundUserAsync( -5, 5 );
			Assert.IsNotNull( relativeScores );

			Console.WriteLine( $"" );
			Console.WriteLine( $"Relative Scores:" );
			foreach ( var e in relativeScores )
			{
				Console.WriteLine( $"{e.GlobalRank}: {e.Score} {e.User}" );
			}
		}

		[TestMethod]
		public async Task GetScoresAsync()
		{
			var leaderboard = await SteamUserStats.FindLeaderboard( "Testleaderboard" );
			Assert.IsTrue( leaderboard.HasValue );

			// Get top 20 global scores
			var globalsScores = await leaderboard.Value.GetScoresAsync( 20 );
			Assert.IsNotNull( globalsScores );

			Console.WriteLine( $"" );
			Console.WriteLine( $"Global Scores:" );
			foreach ( var e in globalsScores )
			{
				Console.WriteLine( $"{e.GlobalRank}: {e.Score} {e.User}" );
			}
		}

		[TestMethod]
		public void GetStatInt()
		{
			var deaths = new Stat( "deaths" );
			Console.WriteLine( $"{deaths.Name} {deaths.GetInt()} times" );
			Console.WriteLine( $"{deaths.Name} {deaths.GetFloat()} times" );

			Assert.AreNotEqual( 0, deaths.GetInt() );
		}

		[TestMethod]
		public async Task GetStatGlobalInt()
		{
			var deaths = new Stat( "deaths" );
			await deaths.GetGlobalIntDays( 5 );

			var totalStartups = deaths.GetGlobalInt();
			Assert.AreNotEqual( 0, totalStartups );
			Console.WriteLine( $"Rust has had {totalStartups} deaths" );
		}

		[TestMethod]
		public async Task GetStatGlobalHistoryInt()
		{
			var deaths = new Stat( "deaths" );

			var history = await deaths.GetGlobalIntDays( 10 );
			Assert.AreNotEqual( 0, history.Length );

			for ( int i=0; i< history.Length; i++ )
			{
				Console.WriteLine( $"{i} : {history[i]}" );
			}
		}

	}

}
