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
				Console.WriteLine( $"{a.Identifier}" );
				Console.WriteLine( $"	a.State: {a.State}" );
				Console.WriteLine( $"	a.UnlockTime: {a.UnlockTime}" );
				Console.WriteLine( $"	a.Name: {a.Name}" );
				Console.WriteLine( $"	a.Description: {a.Description}" );
				Console.WriteLine( $"	a.GlobalUnlocked:	{a.GlobalUnlocked}" );

				var icon = a.GetIcon();

				Console.WriteLine( $"	a.Icon:	{icon}" );
			}			
		}

		[TestMethod]
		public async Task StoreStats()
		{
			var result = Result.NotSettled;

			SteamUserStats.OnUserStatsStored += ( r ) =>
			{
				result = r;
			};

			SteamUserStats.StoreStats();

			while ( result  == Result.NotSettled )
			{
				await Task.Delay( 10 );
			}

			Assert.AreEqual( result, Result.OK );

		}

	}

}
