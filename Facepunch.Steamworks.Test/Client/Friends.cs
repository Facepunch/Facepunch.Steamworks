using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Facepunch.Steamworks.Test
{
    [DeploymentItem( Config.LibraryName + ".dll" )]
    [DeploymentItem( "steam_appid.txt" )]
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

        [TestMethod]
        public void Avatar()
        {
            using ( var client = new Facepunch.Steamworks.Client( 252490 ) )
            {
                Assert.IsTrue( client.Valid );

                var friend = client.Friends.All.First();

                var img = client.Friends.GetAvatar( Steamworks.Friends.AvatarSize.Medium, friend.Id );

                Assert.AreEqual( img.Width, 64 );
                Assert.AreEqual( img.Height, 64 );

                while ( !img.IsLoaded && !img.IsError )
                {
                    client.Update();
                    System.Threading.Thread.Sleep( 10 );
                }

                Assert.AreEqual( img.Data.Length, img.Width * img.Height * 4 );

                DrawImage( img );
            }
        }

        public static void DrawImage( Image img )
        {
            var grad = " -:+#";

            for ( int y = 0; y<img.Height; y++ )
            {
                var str = "";

                for ( int x = 0; x < img.Width; x++ )
                {
                    var p = img.GetPixel( x, y );

                    var brightness = 1 - ((float)(p.r + p.g + p.b) / (255.0f * 3.0f));
                    var c = (int) ((grad.Length) * brightness);
                    str += grad[c];
    
                }

                Console.WriteLine( str );
            }
        }
    }
}
