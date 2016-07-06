using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Facepunch.Steamworks.Test
{
    [TestClass]
    public class Client
    {
        [TestMethod]
        public void ClientInit()
        {
            using ( var client = new Facepunch.Steamworks.Client( 252490 ) )
            {
                Assert.IsTrue( client.Valid );
            }
        }

        [TestMethod]
        public void ClientName()
        {
            using ( var client = new Facepunch.Steamworks.Client( 252490 ) )
            {
                var username = client.Username;
                Console.WriteLine( username );
                Assert.IsTrue( client.Valid );
            }
        }
    }
}
