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
    [DeploymentItem( "steam_api.dll" )]
    public class NetworkUtilsTest
	{
		static string GarrysLocation = "lhr=4+0,ams=13+1/10+0,par=17+1/12+0,lux=17+1,fra=18+1/18+0,sto=25+2,sto2=26+2,mad=27+2,vie=31+3/30+0,iad=90+9/75+0,sgp=173+17/174+17,gru=200+20/219+0";

		[TestMethod]
        public async Task LocalPingLocation()
        {
			await SteamNetworkingUtils.WaitForPingDataAsync();

			for ( int i = 0; i < 10; i++ )
			{
				var pl = SteamNetworkingUtils.LocalPingLocation;
				if ( !pl.HasValue )
				{
					await Task.Delay( 1000 );
					continue;
				}

				Console.WriteLine( $"{i} Seconds Until Result: {pl}" );
				return;
			}

			Assert.Fail();
		}

		[TestMethod]
		public void PingLocationParse()
		{
			var pl = Data.PingLocation.TryParseFromString( GarrysLocation );

			Assert.IsTrue( pl.HasValue );

			Console.WriteLine( $"Parsed OKAY! {pl}" );
		}

		[TestMethod]
		public async Task GetEstimatedPing()
		{
			await SteamNetworkingUtils.WaitForPingDataAsync();

			var garrysping = Data.PingLocation.TryParseFromString( GarrysLocation );
			Assert.IsTrue( garrysping.HasValue );

			var ping = SteamNetworkingUtils.EstimatePingTo( garrysping.Value );
			Assert.IsTrue( ping > 0 );

			Console.WriteLine( $"Ping returned: {ping}" );
		}
	}

}