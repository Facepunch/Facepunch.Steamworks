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

                board.AddScore( true, false, 86275309, 7, 8, 9 );

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


                board.AddScore( true, false, 1234 );

                Thread.Sleep( 10 );
                client.Update();

                board.AddScore( true, true, 34566 );

                Thread.Sleep( 10 );
                client.Update();

                board.AddScore( true, false, 86275309, 7, 8, 9, 7, 4, 7, 98, 24, 5, 76, 124, 6 );

                Thread.Sleep( 10 );
                client.Update();

                board.AddScore( false, true, 86275309, 7, 8, 9, 7, 4, 7, 98, 24, 5, 76, 124, 6 );

                Thread.Sleep( 10 );
                client.Update();
            }
        }
    }
}