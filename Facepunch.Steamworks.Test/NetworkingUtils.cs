using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

#if false

namespace Steamworks
{
    [TestClass]
    [DeploymentItem( "steam_api64.dll" )]
    public class NetworkUtilsTest
	{
		static string GarrysLocation = "lhr=19+1,ams=25+2/25+1,par=29+2,fra=31+3/30+1,lux=33+3,vie=44+4/41+1,waw=47+4/45+1,sto2=48+4/46+2,sto=50+5/46+2,iad=107+10/91+1,sgp=186+18,gru=252+25/234+1";

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

#endif