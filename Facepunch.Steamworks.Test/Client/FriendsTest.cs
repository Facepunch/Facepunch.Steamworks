using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Facepunch.Steamworks.Test
{
    [DeploymentItem( "steam_api.dll" )]
    [DeploymentItem( "steam_api64.dll" )]
    [TestClass]
    public class Friends
    {
        [TestMethod]
        public void FriendList()
        {
            using ( var client = new Facepunch.Steamworks.Client( 252490 ) )
            {
                Assert.IsTrue( client.IsValid );

                client.Friends.Refresh();

                Assert.IsNotNull( client.Friends.All );

                foreach ( var friend in client.Friends.All )
                {
                    Console.WriteLine( "{0}: {1} (Friend:{2}) (Blocked:{3})", friend.Id, friend.Name, friend.IsFriend, friend.IsBlocked );

                    Assert.IsNotNull(friend.GetAvatar( Steamworks.Friends.AvatarSize.Medium ));
                }
            }
        }

        [TestMethod]
        public void FriendListWithoutRefresh()
        {
            using ( var client = new Facepunch.Steamworks.Client( 252490 ) )
            {
                Assert.IsTrue( client.IsValid );

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
                Assert.IsTrue( client.IsValid );

                ulong id = (ulong)( 76561197960279927 + (new Random().Next() % 10000));
                bool passed = false;

                client.Friends.GetAvatar( Steamworks.Friends.AvatarSize.Medium, id, ( avatar) =>
                {
                    if ( avatar == null )
                    {
                        Console.WriteLine( "No Avatar" );
                    }
                    else
                    {
                        Assert.AreEqual( avatar.Width, 64 );
                        Assert.AreEqual( avatar.Height, 64 );
                        Assert.AreEqual( avatar.Data.Length, avatar.Width * avatar.Height * 4 );

                        DrawImage( avatar );
                    }
                    passed = true;
                });

                while (passed == false )
                {
                    client.Update();
                    System.Threading.Thread.Sleep( 10 );
                }
            }
        }

        [TestMethod]
        public void CachedAvatar()
        {
            using (var client = new Facepunch.Steamworks.Client(252490))
            {
                Assert.IsTrue(client.IsValid);

                var friend = client.Friends.All.First();

                var image = client.Friends.GetCachedAvatar( Steamworks.Friends.AvatarSize.Medium, friend.Id );

                if (image != null)
                {
                    Assert.AreEqual(image.Width, 64);
                    Assert.AreEqual(image.Height, 64);
                    Assert.AreEqual(image.Data.Length, image.Width * image.Height * 4);
                }
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
                    if ( c > 3 ) c = 3;
                    str += grad[c];
    
                }

                Console.WriteLine( str );
            }
        }
    }
}
