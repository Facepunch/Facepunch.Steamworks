using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Facepunch.Steamworks.Test
{
    [TestClass]
    public partial class IWorkshopService
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
        public void GetItemDailyRevenue()
        {
            var response = SteamApi.IWorkshopService.GetItemDailyRevenue( 252490, 20006, DateTime.Now.Subtract( TimeSpan.FromDays( 30 ) ), DateTime.Now.AddDays( 1 )  );

            Console.WriteLine( "Item Sold " + response.TotalUnitsSold + " worldwide" );
            Console.WriteLine( "Item Generated  $" + response.TotalRevenue + " worldwide" );
        }
        
    }
}
