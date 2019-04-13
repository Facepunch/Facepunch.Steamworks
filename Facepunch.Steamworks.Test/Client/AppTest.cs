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
		public void AppInstallDir()
		{
			var str = Apps.AppInstallDir( 4000 );
			Assert.IsNotNull( str );
			Assert.IsTrue( str.Length > 3 );

			Console.WriteLine( $"{str}" );
		}

		[TestMethod]
		public void AppOwner()
		{
			var steamid = Apps.AppOwner;
			Assert.IsTrue( steamid.Value > 70561197960279927 );
			Assert.IsTrue( steamid.Value < 80561197960279927 );

			Console.WriteLine( $"{steamid.Value}" );
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

		[TestMethod]
		public async Task GetFileDetails()
		{
			var fileinfo = await Apps.GetFileDetails( "hl2.exe" );

			Console.WriteLine( $"fileinfo.SizeInBytes: {fileinfo.SizeInBytes}" );
		}
	}

}
