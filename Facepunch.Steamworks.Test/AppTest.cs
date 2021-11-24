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
    public class AppTest
    {
		[AssemblyInitialize]
		public static void AssemblyInit( TestContext context )
		{
			Steamworks.Dispatch.OnDebugCallback = ( type, str, server ) =>
			{
				Console.WriteLine( $"[Callback {type} {(server ? "server" : "client")}]" );
				Console.WriteLine( str );
				Console.WriteLine( $"" );
			};

			Steamworks.Dispatch.OnException = ( e ) =>
			{
				Console.Error.WriteLine( e.Message );
				Console.Error.WriteLine( e.StackTrace );
				Assert.Fail( e.Message );
			};

			//
			// Init Client
			//
			Steamworks.SteamClient.Init( 252490 );

			//
			// Init Server
			//
			var serverInit = new SteamServerInit( "rust", "Rusty Mode" )
			{
				GamePort = 28015,
				Secure = true,
				QueryPort = 28016
			};

			Steamworks.SteamServer.Init( 252490, serverInit );

			//
			// Needs to happen before LogOnAnonymous
			//
			SteamNetworkingSockets.RequestFakeIP();

			SteamServer.LogOnAnonymous();

		}

		[TestMethod]
        public void GameLangauge()
        {
			var gl = SteamApps.GameLanguage;
			Assert.IsNotNull( gl );
			Assert.IsTrue( gl.Length > 3 );

			Console.WriteLine( $"{gl}" );
		}

		[TestMethod]
		public void AppInstallDir()
		{
			var str = SteamApps.AppInstallDir();
			Assert.IsNotNull( str );
			Assert.IsTrue( str.Length > 3 );

			Console.WriteLine( $"{str}" );
		}

		[TestMethod]
		public void AppOwner()
		{
			var steamid = SteamApps.AppOwner;
			Assert.IsTrue( steamid.Value > 70561197960279927 );
			Assert.IsTrue( steamid.Value < 80561197960279927 );

			Console.WriteLine( $"{steamid.Value}" );
		}

		[TestMethod]
		public void InstalledDepots()
		{
			var depots = SteamApps.InstalledDepots().ToArray();

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
			var fileinfo = await SteamApps.GetFileDetailsAsync( "RustClient.exe" );

			Console.WriteLine( $"fileinfo.SizeInBytes: {fileinfo?.SizeInBytes}" );
			Console.WriteLine( $"fileinfo.Sha1: {fileinfo?.Sha1}" );
			Console.WriteLine( $"fileinfo.Flags: {fileinfo?.Flags}" );
		}

		[TestMethod]
		public void CommandLine()
		{
			var cl = SteamApps.CommandLine;

			Console.WriteLine( $"CommandLine: {cl}" );
		}
	}

}
