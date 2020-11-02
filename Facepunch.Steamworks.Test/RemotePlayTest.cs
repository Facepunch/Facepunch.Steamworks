using System;
using System.Diagnostics;
using System.IO;
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
	public class RemotePlayTest
	{
		[TestMethod]
		public void BasicUsability()
		{
			Console.WriteLine( $"Sessions: {SteamRemotePlay.SessionCount}" );

			var session = SteamRemotePlay.GetSession( 4 );

			Assert.IsFalse( session.IsValid );
			Assert.IsFalse( session.SteamId.IsValid );
		}

	}

}
