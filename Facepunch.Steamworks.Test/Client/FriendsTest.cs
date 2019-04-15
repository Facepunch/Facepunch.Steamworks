using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Threading.Tasks;

namespace Steamworks
{
    [DeploymentItem( "steam_api64.dll" )]
    [TestClass]
    public class FriendsTest
    {
        [TestMethod]
        public void GetFriends()
        {
			foreach ( var friend in Friends.GetFriends() )
			{
				Console.WriteLine( $"{friend.Id.Value}: {friend.Name} (Friend:{friend.IsFriend}) (Blocked:{friend.IsBlocked})" );
				Console.WriteLine( $"		{string.Join( ", ", friend.NameHistory)}" );

				//	Assert.IsNotNull( friend.GetAvatar( Steamworks.Friends.AvatarSize.Medium ) );
			}
        }

		[TestMethod]
		public void GetBlocked()
		{
			foreach ( var friend in Friends.GetBlocked() )
			{
				Console.WriteLine( $"{friend.Id.Value}: {friend.Name} (Friend:{friend.IsFriend}) (Blocked:{friend.IsBlocked})" );
				Console.WriteLine( $"		{string.Join( ", ", friend.NameHistory )}" );

				//	Assert.IsNotNull( friend.GetAvatar( Steamworks.Friends.AvatarSize.Medium ) );
			}
		}

		[TestMethod]
		public void GetPlayedWith()
		{
			foreach ( var friend in Friends.GetPlayedWith() )
			{
				Console.WriteLine( $"{friend.Id.Value}: {friend.Name} (Friend:{friend.IsFriend}) (Blocked:{friend.IsBlocked})" );
				Console.WriteLine( $"		{string.Join( ", ", friend.NameHistory )}" );

				//	Assert.IsNotNull( friend.GetAvatar( Steamworks.Friends.AvatarSize.Medium ) );
			}
		}

		[TestMethod]
		public async Task LargeAvatar()
		{
			ulong id = (ulong)(76561197960279927 + (new Random().Next() % 10000));

			var image = await Friends.GetLargeAvatarAsync( id );
			if ( !image.HasValue )
				return;

			Console.WriteLine( $"image.Width {image.Value.Width}" );
			Console.WriteLine( $"image.Height {image.Value.Height}" );

			DrawImage( image.Value );			
		}

		[TestMethod]
		public async Task MediumAvatar()
		{
			ulong id = (ulong)(76561197960279927 + (new Random().Next() % 10000));

			Console.WriteLine( $"Steam: http://steamcommunity.com/profiles/{id}" );

			var image = await Friends.GetMediumAvatarAsync( id );
			if ( !image.HasValue )
				return;

			Console.WriteLine( $"image.Width {image.Value.Width}" );
			Console.WriteLine( $"image.Height {image.Value.Height}" );

			DrawImage( image.Value );
		}

		[TestMethod]
		public async Task SmallAvatar()
		{
			ulong id = (ulong)(76561197960279927 + (new Random().Next() % 10000));

			var image = await Friends.GetSmallAvatarAsync( id );
			if ( !image.HasValue )
				return;

			Console.WriteLine( $"image.Width {image.Value.Width}" );
			Console.WriteLine( $"image.Height {image.Value.Height}" );

			DrawImage( image.Value );
		}

		[TestMethod]
		public async Task GetFriendsAvatars()
		{
			foreach ( var friend in Friends.GetFriends() )
			{
				Console.WriteLine( $"{friend.Id.Value}: {friend.Name}" );

				var image = await friend.GetSmallAvatarAsync();
				if ( image.HasValue )
				{
					DrawImage( image.Value );
				}

				//	Assert.IsNotNull( friend.GetAvatar( Steamworks.Friends.AvatarSize.Medium ) );
			}
		}

		/*
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
				*/
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
