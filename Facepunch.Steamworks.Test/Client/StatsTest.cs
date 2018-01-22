using System;
using System.Text;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Facepunch.Steamworks.Test
{
    [TestClass]
    [DeploymentItem( "steam_api.dll" )]
    [DeploymentItem( "steam_api64.dll" )]
    public class Stats
    {
        [TestMethod]
        public void UpdateStats()
        {
            using ( var client = new Facepunch.Steamworks.Client( 252490 ) )
            {
                client.Stats.UpdateStats();
            }
        }

        [TestMethod]
        public void UpdateSUpdateGlobalStatstats()
        {
            using ( var client = new Facepunch.Steamworks.Client( 252490 ) )
            {
                client.Stats.UpdateGlobalStats( 1 );
                client.Stats.UpdateGlobalStats( 3 );
                client.Stats.UpdateGlobalStats( 7 );
            }
        }

        [TestMethod]
        public void GetClientFloat()
        {
            using ( var client = new Facepunch.Steamworks.Client( 252490 ) )
            {
                var v = client.Stats.GetFloat( "deaths" );
                Console.WriteLine( v );
            }
        }

        [TestMethod]
        public void GetClientInt()
        {
            using ( var client = new Facepunch.Steamworks.Client( 252490 ) )
            {
                var v = client.Stats.GetInt( "deaths" );
                Console.WriteLine( v );
            }
        }

        [TestMethod]
        public void GetGlobalFloat()
        {
            using ( var client = new Facepunch.Steamworks.Client( 252490 ) )
            {
                var v = client.Stats.GetGlobalFloat( "deaths" );
                Console.WriteLine( v );
            }
        }

        [TestMethod]
        public void GetGlobalInt()
        {
            using ( var client = new Facepunch.Steamworks.Client( 252490 ) )
            {
                var v = client.Stats.GetGlobalInt( "deaths" );
                Console.WriteLine( v );
            }
        }
    }
}
