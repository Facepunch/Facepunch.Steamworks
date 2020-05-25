using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steamworks
{
    [DeploymentItem("steam_api64.dll")]
    [DeploymentItem("steam_api.dll")]
    [TestClass]
    public class ClanTest
    {
        [TestMethod]
        public void GetName()
        {
            var clan = new Clan(103582791433666425);

            Assert.AreEqual("Steamworks Development", clan.Name);
        }

        [TestMethod]
        public void GetClanTag()
        {
            var clan = new Clan(103582791433666425);

            Assert.AreEqual("SteamworksDev", clan.Tag);
        }

        [TestMethod]
        public async Task GetOwner()
        {
            var clan = new Clan(103582791433666425);
            await clan.RequestOfficerList();

            Assert.AreNotEqual(new SteamId(), clan.Owner.Id);
        }

        [TestMethod]
        public void GetOfficers()
        {
            var clan = new Clan(103582791433666425);
            foreach (var officer in clan.GetOfficers())
            {
                Console.WriteLine($"{officer.Name} : {officer.Id}");
            }
        }
        
        [TestMethod]
        public async Task RequestOfficerList()
        {
            var clan = new Clan(103582791433666425);
            bool res = await clan.RequestOfficerList();

            Assert.AreEqual(true, res);
        }
    }
}
