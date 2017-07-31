using System;
using System.Collections.Generic;
using System.Diagnostics;
using Facepunch.Steamworks;
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
                    Console.WriteLine("lobby created: " + client.Lobby.CurrentLobby);
                    Console.WriteLine($"Owner: {client.Lobby.Owner}");
                    Console.WriteLine($"Max Members: {client.Lobby.MaxMembers}");
                    Console.WriteLine($"Num Members: {client.Lobby.NumMembers}");
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

        [TestMethod]
        public void GetCreatedLobbyData()
        {
            using (var client = new Facepunch.Steamworks.Client(252490))
            {
                Assert.IsTrue(client.IsValid);

                client.Lobby.Create(Steamworks.Lobby.Type.Public, 10);

                client.Lobby.OnLobbyCreated = (success) =>
                {
                    Assert.IsTrue(success);
                    Assert.IsTrue(client.Lobby.IsValid);
                    Console.WriteLine("lobby created: " + client.Lobby.CurrentLobby);
                    foreach (KeyValuePair<string, string> data in client.Lobby.CurrentLobbyData.GetAllData())
                    {
                        if (data.Key == "appid")
                        {
                            Assert.IsTrue(data.Value == "252490");
                        }
                        Console.WriteLine($"{data.Key} {data.Value}");
                    }
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

        [TestMethod]
        public void UpdateLobbyData()
        {
            using (var client = new Facepunch.Steamworks.Client(252490))
            {
                Assert.IsTrue(client.IsValid);

                client.Lobby.Create(Steamworks.Lobby.Type.Public, 10);

                client.Lobby.OnLobbyCreated = (success) =>
                {
                    Assert.IsTrue(success);
                    Assert.IsTrue(client.Lobby.IsValid);
                    Console.WriteLine("lobby created: " + client.Lobby.CurrentLobby);

                    client.Lobby.Name = "My Updated Lobby Name";
                    client.Lobby.CurrentLobbyData.SetData("testkey", "testvalue");
                    client.Lobby.LobbyType = Steamworks.Lobby.Type.Private;
                    client.Lobby.MaxMembers = 5;
                    client.Lobby.Joinable = false;

                    foreach (KeyValuePair<string, string> data in client.Lobby.CurrentLobbyData.GetAllData())
                    {
                        if (data.Key == "appid")
                        {
                            Assert.IsTrue(data.Value == "252490");
                        }

                        if (data.Key == "testkey")
                        {
                            Assert.IsTrue(data.Value == "testvalue");
                        }

                        if (data.Key == "lobbytype")
                        {
                            Assert.IsTrue(data.Value == Steamworks.Lobby.Type.Private.ToString());
                        }

                        Console.WriteLine($"{data.Key} {data.Value}");
                    }



                };


                client.Lobby.OnLobbyDataUpdated = () =>
                {
                    Console.WriteLine("lobby data updated");
                    Console.WriteLine(client.Lobby.MaxMembers);
                    Console.WriteLine(client.Lobby.Joinable);
                };



                var sw = Stopwatch.StartNew();

                while (sw.Elapsed.TotalSeconds < 3)
                {
                    client.Update();
                    System.Threading.Thread.Sleep(10);
                }

                client.Lobby.Leave();

            }
        }

        [TestMethod]
        public void RefreshLobbyList()
        {
            using (var client = new Facepunch.Steamworks.Client(252490))
            {
                Assert.IsTrue(client.IsValid);

                client.Lobby.OnLobbyCreated = (success) =>
                {
                    Assert.IsTrue(success);
                    Assert.IsTrue(client.Lobby.IsValid);
                    Console.WriteLine("lobby created: " + client.Lobby.CurrentLobby);
                    client.LobbyList.Refresh();
                };

                client.LobbyList.OnLobbiesUpdated = () =>
                {
                    Console.WriteLine("lobbies updating");
                    if (client.LobbyList.Finished)
                    {
                        Console.WriteLine("lobbies finished updating");
                        Console.WriteLine($"found {client.LobbyList.Lobbies.Count} lobbies");

                        foreach (LobbyList.Lobby lobby in client.LobbyList.Lobbies)
                        {
                            Console.WriteLine($"Found Lobby: {lobby.Name}");
                        }

                        client.Lobby.Leave();

                    }
                    
                };

                client.Lobby.Create(Steamworks.Lobby.Type.Public, 10);

                var sw = Stopwatch.StartNew();

                while (sw.Elapsed.TotalSeconds < 3)
                {
                    client.Update();
                    System.Threading.Thread.Sleep(10);
                }

            }
        }

        [TestMethod]
        public void RefreshLobbyListWithFilter()
        {
            using (var client = new Facepunch.Steamworks.Client(480))
            {
                Assert.IsTrue(client.IsValid);

                client.Lobby.OnLobbyCreated = (success) =>
                {
                    Assert.IsTrue(success);
                    Assert.IsTrue(client.Lobby.IsValid);
                    Console.WriteLine("lobby created: " + client.Lobby.CurrentLobby);
                    client.Lobby.CurrentLobbyData.SetData("testkey", "testvalue");
                };

                client.Lobby.OnLobbyDataUpdated = () =>
                {
                    var filter = new LobbyList.Filter();
                    filter.StringFilters.Add("testkey", "testvalue");
                    client.LobbyList.Refresh(filter);
                };

                client.LobbyList.OnLobbiesUpdated = () =>
                {
                    Console.WriteLine("lobbies updating");
                    if (client.LobbyList.Finished)
                    {
                        Console.WriteLine("lobbies finished updating");
                        Console.WriteLine($"found {client.LobbyList.Lobbies.Count} lobbies");

                        foreach (LobbyList.Lobby lobby in client.LobbyList.Lobbies)
                        {
                            Console.WriteLine($"Found Lobby: {lobby.Name}");
                        }

                    }

                };

                client.Lobby.Create(Steamworks.Lobby.Type.Public, 10);

                var sw = Stopwatch.StartNew();

                while (sw.Elapsed.TotalSeconds < 5)
                {
                    client.Update();
                    System.Threading.Thread.Sleep(10);
                }

                client.Lobby.Leave();

            }
        }
    }
}
