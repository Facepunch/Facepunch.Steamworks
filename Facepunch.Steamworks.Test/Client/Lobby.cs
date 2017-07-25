using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Facepunch.Steamworks.Test
{
    [DeploymentItem("steam_api.dll")]
    [DeploymentItem("steam_api64.dll")]
    [DeploymentItem("steam_appid.txt")]
    [TestClass]
    class Lobby
    {
        [TestMethod]
        public void FriendList()
        {
            /*
            using (var client = new Facepunch.Steamworks.Client(252490))
            {
                Assert.IsTrue(client.IsValid);

                client.Friends.Refresh();

                Assert.IsNotNull(client.Friends.All);

                foreach (var friend in client.Friends.All)
                {
                    Console.WriteLine("{0}: {1} (Friend:{2}) (Blocked:{3})", friend.Id, friend.Name, friend.IsFriend, friend.IsBlocked);
                }
            }
            */
        }
    }
}
