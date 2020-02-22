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
	public class SteamNetworkingTest
    {
		[TestMethod]
        public async Task SendP2PPacket()
        {
			var sent = SteamNetworking.SendP2PPacket( SteamClient.SteamId, new byte[] { 1, 2, 3 } );

			Assert.IsTrue( sent );

			while ( !SteamNetworking.IsP2PPacketAvailable() )
			{
				await Task.Delay( 10 );
			}

			var packet = SteamNetworking.ReadP2PPacket();

			Assert.IsTrue( packet.HasValue );

			Assert.AreEqual( packet.Value.SteamId, SteamClient.SteamId );
			Assert.AreEqual( packet.Value.Data[1], 2 );
			Assert.AreEqual( packet.Value.Data.Length, 3 );
		}
	}

}
