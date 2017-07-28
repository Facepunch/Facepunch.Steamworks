using System;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Facepunch.Steamworks.Test
{
    [TestClass]
    [DeploymentItem("steam_api.dll")]
    [DeploymentItem("steam_api64.dll")]
    [DeploymentItem("steam_appid.txt")]
    public class Lobby
    {
        [TestMethod]
        public void CreateLobby()
        {
            using (var client = new Facepunch.Steamworks.Client(252490))
            {
                Assert.IsTrue(client.IsValid);

                client.Lobby.Create(Steamworks.Lobby.Type.Public, 10);

                client.Lobby.OnLobbyCreated = (success) =>
                {
                    Assert.IsTrue(success);
                    Assert.IsTrue(client.Lobby.IsValid);
                    Console.WriteLine(client.Lobby.CurrentLobby);
                    client.Lobby.Leave();
                };

                var sw = Stopwatch.StartNew();

                while (sw.Elapsed.TotalSeconds < 3)
                {
                    client.Update();
                    System.Threading.Thread.Sleep(10);
                }

            }
        }
    }
}
