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
    public class UserStatsTest
	{
		[TestMethod]
        public void AchievementList()
        {
			foreach ( var a in SteamUserStats.Achievements )
			{
				Console.WriteLine( $"{a.Value}" );
				Console.WriteLine( $"	a.State: {a.State}" );
				Console.WriteLine( $"	a.UnlockTime: {a.UnlockTime}" );
				Console.WriteLine( $"	a.GlobalUnlockedPercentage:	{a.GlobalUnlockedPercentage}" );

				var icon = a.GetIcon();

				Console.WriteLine( $"	a.Icon:	{icon}" );
			}			
		}

	}

}
