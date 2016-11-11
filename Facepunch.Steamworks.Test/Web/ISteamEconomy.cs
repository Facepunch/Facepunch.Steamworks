using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Facepunch.Steamworks.Test
{
    [TestClass]
    public partial class ISteamEconomy
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
        public void GetMarketPrices()
        {
            var response = Facepunch.SteamApi.ISteamEconomy.GetMarketPrices( 252490 );

            var items = response.groups.OrderByDescending( x => x.sell_listings );

            foreach ( var i in items )
            {
                Console.WriteLine( i.sell_listings + "   " + i.name );
            }
        }
        
    }
}
