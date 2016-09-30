using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Facepunch.Steamworks.Test
{
    [TestClass]
    public class Friends
    {
        [TestMethod]
        public void FriendList()
        {
            using ( var client = new Facepunch.Steamworks.Client( 252490 ) )
            {
                Assert.IsTrue( client.Valid );

                client.Friends.Refresh();

                Assert.IsNotNull( client.Friends.All );

                foreach ( var friend in client.Friends.All )
                {
                    Console.WriteLine( "{0}: {1} (Friend:{2}) (Blocked:{3})", friend.Id, friend.Name, friend.IsFriend, friend.IsBlocked );
                }
            }
        }

        [TestMethod]
        public void FriendListWithoutRefresh()
        {
            using ( var client = new Facepunch.Steamworks.Client( 252490 ) )
            {
                Assert.IsTrue( client.Valid );

                foreach ( var friend in client.Friends.All )
                {
                    Console.WriteLine( "{0}: {1} (Friend:{2}) (Blocked:{3})", friend.Id, friend.Name, friend.IsFriend, friend.IsBlocked );
                }
            }
        }
    }
}
