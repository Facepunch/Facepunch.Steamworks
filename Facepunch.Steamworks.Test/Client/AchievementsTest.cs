using System;
using System.Text;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Facepunch.Steamworks.Test
{
    [TestClass]
    [DeploymentItem( "steam_api.dll" )]
    [DeploymentItem( "steam_api64.dll" )]
    public class Achievements
    {
        [TestMethod]
        public void GetCount()
        {
            using ( var client = new Facepunch.Steamworks.Client( 252490 ) )
            {
                Assert.IsTrue( client.IsValid );

                var gotStats = false;
                client.Achievements.OnUpdated += () => { gotStats = true; };

                while ( !gotStats )
                {
                    client.Update();
                }

                Console.WriteLine( "Found " + client.Achievements.All.Length + " Achievements" );

                Assert.AreNotEqual( 0, client.Achievements.All.Length );
            }
        }

        [TestMethod]
        public void GetNames()
        {
            using ( var client = new Facepunch.Steamworks.Client( 252490 ) )
            {
                Assert.IsTrue( client.IsValid );

                var gotStats = false;
                client.Achievements.OnUpdated += () => { gotStats = true; };

                while ( !gotStats )
                {
                    client.Update();
                }

                foreach( var ach in client.Achievements.All )
                {
                    Assert.IsNotNull( ach.Id );

                    Console.WriteLine( " " + ach.Id );
                    Console.WriteLine( " - - " + ach.Name );
                    Console.WriteLine( " - - " + ach.Description );
                    Console.WriteLine( " - - " + ach.State );
                    Console.WriteLine( " - - " + ach.UnlockTime );
                    Console.WriteLine( " - - " + ach.GlobalUnlockedPercentage );

                    if ( ach.Icon != null )
                    {
                        Console.WriteLine( " - - " + ach.Icon.Width + " x " + ach.Icon.Height );
                    }
                    
                    Console.WriteLine( "" );
                }
            }
        }

        [TestMethod]
        public void Trigger()
        {
            using ( var client = new Facepunch.Steamworks.Client( 252490 ) )
            {
                Assert.IsTrue( client.IsValid );

                var gotStats = false;
                client.Achievements.OnUpdated += () => { gotStats = true; };

                while ( !gotStats )
                {
                    client.Update();
                }

                foreach ( var ach in client.Achievements.All )
                {
                    ach.Trigger();
                }
            }
        }

    }
}
