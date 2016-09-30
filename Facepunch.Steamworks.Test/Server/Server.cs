using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Facepunch.Steamworks.Test
{
    [DeploymentItem( "FacepunchSteamworksApi.dll" )]
    [DeploymentItem( "steam_appid.txt" )]
    [DeploymentItem( "tier0_s.dll" )]
    [DeploymentItem( "vstdlib_s.dll" )]
    [DeploymentItem( "steamclient.dll" )]
    [TestClass]
    public class Server
    {
        [TestMethod]
        public void Init()
        {
            using ( var server = new Facepunch.Steamworks.Server( 252490, 0, 20216, 20816, 20817, false, "VersionString" ) )
            {
                Assert.IsTrue( server.Valid );
            }
        }
    }
}
