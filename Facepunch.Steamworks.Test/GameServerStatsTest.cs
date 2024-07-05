using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Steamworks.Data;

namespace Steamworks
{
    [TestClass]
    [DeploymentItem( "steam_api64.dll" )]
	[DeploymentItem( "steam_api.dll" )]
	public class GameServerStatsTest
	{
		static SteamId Garry = 76561197960279927;

		//TODO:: Ensure test is valid
		//[TestMethod]
        public async Task GetAchievement()
        {
			var result = await SteamServerStats.RequestUserStatsAsync( Garry );
			Assert.AreEqual( result, Result.OK );

			var value = SteamServerStats.GetAchievement( Garry, "COLLECT_100_WOOD" );
			Assert.IsTrue( value );

			value = SteamServerStats.GetAchievement( Garry, "ACHIVEMENT_THAT_DOESNT_EXIST" );
			Assert.IsFalse( value );
		}
	}

}
