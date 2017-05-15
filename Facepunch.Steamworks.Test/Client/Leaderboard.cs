using System;
using System.Linq;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Facepunch.Steamworks.Test
{
    [TestClass]
    [DeploymentItem( "steam_api.dll" )]
    [DeploymentItem( "steam_api64.dll" )]
    [DeploymentItem( "steam_appid.txt" )]
    public class Leaderboard
    {
        [TestMethod]
        public void GetLeaderboard()
        {
            using ( var client = new Steamworks.Client( 252490 ) )
            {
                var board = client.GetLeaderboard( "TestLeaderboard", Steamworks.Client.LeaderboardSortMethod.Ascending, Steamworks.Client.LeaderboardDisplayType.Numeric );

                while ( !board.IsValid )
                {
                    Thread.Sleep( 10 );
                    client.Update();
                }

                Assert.IsTrue( board.IsValid );
                Assert.IsFalse( board.IsError );
                Assert.IsNotNull( board.Name );

                Console.WriteLine( $"Board name is \"{board.Name}\"" );
                Console.WriteLine( $"Board has \"{board.TotalEntries}\" entries" );

                board.AddScore( true, 86275309, 7, 8, 9 );

                board.FetchScores( Steamworks.Leaderboard.RequestType.Global, 0, 20 );

                while ( board.IsQuerying )
                {
                    Thread.Sleep( 10 );
                    client.Update();
                }

                foreach ( var entry in board.Results )
                {
                    Console.WriteLine( $"{entry.GlobalRank}: {entry.SteamId} ({entry.Name}) with {entry.Score}" );

                    if ( entry.SubScores != null )
                        Console.WriteLine( " - " + string.Join( ";", entry.SubScores.Select( x => x.ToString() ).ToArray() ) );
                }
            }
        }

        [TestMethod]
        public void GetLeaderboardCallback()
        {
            using ( var client = new Steamworks.Client( 252490 ) )
            {
                var board = client.GetLeaderboard( "TestLeaderboard", Steamworks.Client.LeaderboardSortMethod.Ascending, Steamworks.Client.LeaderboardDisplayType.Numeric );

                while ( !board.IsValid )
                {
                    Thread.Sleep( 10 );
                    client.Update();
                }

                Assert.IsTrue( board.IsValid );
                Assert.IsFalse( board.IsError );
                Assert.IsNotNull( board.Name );

                board.AddScore( true, 86275309, 7, 8, 9 );

                var done = false;

                board.FetchScores( Steamworks.Leaderboard.RequestType.Global, 0, 20, ( success, results ) =>
                {
                    Assert.IsTrue( success );

                    foreach ( var entry in results )
                    {
                        Console.WriteLine( $"{entry.GlobalRank}: {entry.SteamId} ({entry.Name}) with {entry.Score}" );

                        if ( entry.SubScores != null )
                            Console.WriteLine( " - " + string.Join( ";", entry.SubScores.Select( x => x.ToString() ).ToArray() ) );
                    }

                    done = true;
                } );

                while ( !done )
                {
                    Thread.Sleep( 10 );
                    client.Update();
                }
            }
        }

        [TestMethod]
        public void AddScores()
        {
            using ( var client = new Steamworks.Client( 252490 ) )
            {
                var board = client.GetLeaderboard( "TestLeaderboard", Steamworks.Client.LeaderboardSortMethod.Ascending, Steamworks.Client.LeaderboardDisplayType.Numeric );

                while ( !board.IsValid )
                {
                    Thread.Sleep( 10 );
                    client.Update();
                }

                Assert.IsTrue( board.IsValid );
                Assert.IsFalse( board.IsError );

                board.AddScore( true, 1234 );

                Thread.Sleep( 10 );
                client.Update();

                board.AddScore( true, 34566 );

                Thread.Sleep( 10 );
                client.Update();

                board.AddScore( true, 86275309, 7, 8, 9, 7, 4, 7, 98, 24, 5, 76, 124, 6 );

                Thread.Sleep( 10 );
                client.Update();

                board.AddScore( false, 86275309, 7, 8, 9, 7, 4, 7, 98, 24, 5, 76, 124, 6 );

                Thread.Sleep( 10 );
                client.Update();
            }
        }

        [TestMethod]
        public void AddScoresCallback()
        {
            using ( var client = new Steamworks.Client( 252490 ) )
            {
                var board = client.GetLeaderboard( "TestLeaderboard", Steamworks.Client.LeaderboardSortMethod.Ascending, Steamworks.Client.LeaderboardDisplayType.Numeric );

                while ( !board.IsValid )
                {
                    Thread.Sleep( 10 );
                    client.Update();
                }

                Assert.IsTrue( board.IsValid );
                Assert.IsFalse( board.IsError );

                var done = false;

                const int score = 5678;

                board.AddScore( false, score, null, ( success, result ) =>
                {
                    Assert.IsTrue( success );
                    Assert.IsTrue( result.ScoreChanged );
                    Assert.AreEqual( result.Score, score );

                    done = true;
                } );

                while ( !done )
                {
                    Thread.Sleep( 10 );
                    client.Update();
                }
            }
        }
    }
}