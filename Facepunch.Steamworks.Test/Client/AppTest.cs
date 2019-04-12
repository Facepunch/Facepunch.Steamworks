using System;
using System.Linq;
using System.Text;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Steamworks
{
    [TestClass]
    [DeploymentItem( "steam_api64.dll" )]
    public class AppTest
    {
		[AssemblyInitialize]
		public static void AssemblyInit( TestContext context )
		{
			Steamworks.Steam.Init( 4000 );
		}

		[TestMethod]
        public void GameLangauge()
        {
			var gl = Apps.GameLanguage;
			Assert.IsNotNull( gl );
			Assert.IsTrue( gl.Length > 3 );

			Console.WriteLine( $"{gl}" );
		}

		[TestMethod]
		public void InstalledDepots()
		{
			var depots = Apps.InstalledDepots( 4000 ).ToArray();

			Assert.IsNotNull( depots );
			Assert.IsTrue( depots.Length > 0 );

			foreach ( var depot in depots )
			{
				Console.WriteLine( $"{depot.Value}" );
			}

			
		}
	}

}
