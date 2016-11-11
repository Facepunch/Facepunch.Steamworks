using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Facepunch.Steamworks.Test
{
    [TestClass]
    public partial class WebSteamApps
    {
        [TestInitialize]
        public void Init()
        {
            //
            // For the sake of tests, we store our Key in an environment variable
            // (so we don't end up shipping it to github and exposing it to everyone)
            //
            Facepunch.SteamApi.Config.Key = Environment.GetEnvironmentVariable( "SteamWebApi", EnvironmentVariableTarget.User );

            //
            // We're going to be using Newtonsoft to deserialize our json
            //
            Facepunch.SteamApi.Config.DeserializeJson = ( str, type ) =>
            {
                return Newtonsoft.Json.JsonConvert.DeserializeObject( str, type );
            };
        }

        [TestMethod]
        public void GetAppBetas()
        {
            foreach ( var beta in Facepunch.SteamApi.ISteamApps.GetAppBetas( 252490 ).betas )
            {
                Console.WriteLine( beta.Key );
            }
        }

        [TestMethod]
        public void GetAppBuilds()
        {
            foreach ( var build in Facepunch.SteamApi.ISteamApps.GetAppBuilds( 252490, 10 ).builds )
            {
                Console.WriteLine( build.Key );
                Console.WriteLine( "   Desc: " + build.Value.Description );
                Console.WriteLine( "   Accnt:" + build.Value.AccountIDCreator );

                foreach ( var depot in build.Value.depots )
                {
                    Console.WriteLine( "   Depot" + depot.Value.DepotId + ":" );
                    Console.WriteLine( "      GID: " + depot.Value.DepotVersionGID );
                    Console.WriteLine( "      Bytes: " + depot.Value.TotalOriginalBytes );
                    Console.WriteLine( "      Compressed: " + depot.Value.TotalCompressedBytes );
                }
            }
        }
    }
}
