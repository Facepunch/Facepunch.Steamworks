using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Facepunch.Steamworks.Test
{
    [TestClass]
    [DeploymentItem( "steam_api.dll" )]
    [DeploymentItem( "steam_api64.dll" )]
    public class Leaderboard
    {
        [TestMethod]
        public void GetLeaderboard()
        {
            using ( var client = new Steamworks.Client( 252490 ) )
            {
                var board = client.GetLeaderboard( "TestLeaderboard", Steamworks.Client.LeaderboardSortMethod.Ascending, Steamworks.Client.LeaderboardDisplayType.Numeric );

                var time = Stopwatch.StartNew();
                while ( !board.IsValid )
                {
                    Thread.Sleep( 10 );
                    client.Update();

                    if (time.Elapsed.TotalSeconds > 10 )
                    {
                        throw new Exception("board.IsValid took too long");
                    }
                }

                Assert.IsTrue( board.IsValid );
                Assert.IsFalse( board.IsError );
                Assert.IsNotNull( board.Name );

                Console.WriteLine( $"Board name is \"{board.Name}\"" );
                Console.WriteLine( $"Board has \"{board.TotalEntries}\" entries" );

                board.AddScore( true, 86275309, 7, 8, 9 );

                board.FetchScores( Steamworks.Leaderboard.RequestType.Global, 0, 20 );

                time = Stopwatch.StartNew();
                while ( board.IsQuerying )
                {
                    Thread.Sleep( 10 );
                    client.Update();

                    if (time.Elapsed.TotalSeconds > 10)
                    {
                        throw new Exception("board.IsQuerying took too long");
                    }
                }

                Assert.IsFalse( board.IsError );
                Assert.IsNotNull( board.Results );

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

                var time = Stopwatch.StartNew();
                while ( !board.IsValid )
                {
                    Thread.Sleep( 10 );
                    client.Update();

                    if (time.Elapsed.TotalSeconds > 10)
                    {
                        throw new Exception("board.IsValid took too long");
                    }
                }

                Assert.IsTrue( board.IsValid );
                Assert.IsFalse( board.IsError );
                Assert.IsNotNull( board.Name );

                board.AddScore( true, 86275309, 7, 8, 9 );

                var done = false;

                board.FetchScores( Steamworks.Leaderboard.RequestType.Global, 0, 20, results =>
                {
                    foreach ( var entry in results )
                    {
                        Console.WriteLine( $"{entry.GlobalRank}: {entry.SteamId} ({entry.Name}) with {entry.Score}" );

                        if ( entry.SubScores != null )
                            Console.WriteLine( " - " + string.Join( ";", entry.SubScores.Select( x => x.ToString() ).ToArray() ) );
                    }

                    done = true;
                }, error => Assert.Fail( error.ToString() ) );

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

                var time = Stopwatch.StartNew();
                while (!board.IsValid)
                {
                    Thread.Sleep(10);
                    client.Update();

                    if (time.Elapsed.TotalSeconds > 10)
                    {
                        throw new Exception("board.IsValid took too long");
                    }
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

                var time = Stopwatch.StartNew();
                while (!board.IsValid)
                {
                    Thread.Sleep(10);
                    client.Update();

                    if ( board.IsError )
                    {
                        throw new Exception( "Board is Error" );
                    }

                    if (time.Elapsed.TotalSeconds > 10)
                    {
                        throw new Exception("board.IsValid took too long");
                    }
                }

                Assert.IsTrue( board.IsValid );
                Assert.IsFalse( board.IsError );

                var done = false;

                const int score = 5678;

                board.AddScore( false, score, null, result =>
                {
                    Assert.IsTrue( result.ScoreChanged );
                    Assert.AreEqual( result.Score, score );

                    done = true;
                }, error => Assert.Fail( error.ToString() ) );

                while ( !done )
                {
                    Thread.Sleep( 10 );
                    client.Update();
                }
            }
        }

        [TestMethod]
        public void AddFileAttachment()
        {
            using ( var client = new Steamworks.Client( 252490 ) )
            {
                var board = client.GetLeaderboard( "TestLeaderboard", Steamworks.Client.LeaderboardSortMethod.Ascending, Steamworks.Client.LeaderboardDisplayType.Numeric );

                var time = Stopwatch.StartNew();
                while (!board.IsValid)
                {
                    Thread.Sleep(10);
                    client.Update();

                    if (time.Elapsed.TotalSeconds > 10)
                    {
                        throw new Exception("board.IsValid took too long");
                    }
                }

                Assert.IsTrue( board.IsValid );
                Assert.IsFalse( board.IsError );

                var done = false;

                const int score = 5678;
                const string attachment = "Hello world!";

                var file = client.RemoteStorage.CreateFile( "score/example.txt" );
                file.WriteAllText( attachment );

                Assert.IsTrue( board.AddScore( false, score, null, result =>
                {
                    Assert.IsTrue( result.ScoreChanged );

                    Assert.IsTrue( board.AttachRemoteFile( file, () =>
                    {
                        done = true;
                    }, error => Assert.Fail( error.ToString() ) ) );
                }, error => Assert.Fail( error.ToString() ) ) );

                while ( !done )
                {
                    Thread.Sleep( 10 );
                    client.Update();
                }

                done = false;

                Assert.IsTrue( board.FetchScores( Steamworks.Leaderboard.RequestType.GlobalAroundUser, 0, 0, entries =>
                {
                    Assert.AreEqual( 1, entries.Length );
                    Assert.IsNotNull( entries[0].AttachedFile );

                    Assert.IsTrue( entries[0].AttachedFile.Download( () =>
                    {
                        Assert.AreEqual( attachment, entries[0].AttachedFile.ReadAllText() );

                        done = true;
                    }, error => Assert.Fail( error.ToString() ) ) );
                }, error => Assert.Fail( error.ToString() ) ) );

                while ( !done )
                {
                    Thread.Sleep( 10 );
                    client.Update();
                }
            }
        }
    }
}