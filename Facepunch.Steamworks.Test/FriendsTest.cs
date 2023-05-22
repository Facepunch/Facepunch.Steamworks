using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Threading.Tasks;
using Steamworks.Data;

namespace Steamworks
{
	[DeploymentItem( "steam_api64.dll" )]
	[DeploymentItem( "steam_api.dll" )]
	[TestClass]
	public class FriendsTest
	{
		[TestMethod]
		public void GetFriends()
		{
			foreach ( var friend in SteamFriends.GetFriends() )
			{
				Console.WriteLine( $"{friend.Id.Value}: {friend.Name} (Friend:{friend.IsFriend}) (Blocked:{friend.IsBlocked})" );
				Console.WriteLine( $"		{string.Join( ", ", friend.NameHistory )}" );

				//	Assert.IsNotNull( friend.GetAvatar( Steamworks.Friends.AvatarSize.Medium ) );
			}
		}

		[TestMethod]
		public void GetBlocked()
		{
			foreach ( var friend in SteamFriends.GetBlocked() )
			{
				Console.WriteLine( $"{friend.Id.Value}: {friend.Name} (Friend:{friend.IsFriend}) (Blocked:{friend.IsBlocked})" );
				Console.WriteLine( $"		{string.Join( ", ", friend.NameHistory )}" );

				//	Assert.IsNotNull( friend.GetAvatar( Steamworks.Friends.AvatarSize.Medium ) );
			}
		}

		[TestMethod]
		public async Task GetPlayedWith()
		{
			foreach ( var friend in SteamFriends.GetPlayedWith() )
			{
				await friend.RequestInfoAsync();

				Console.WriteLine( $"{friend.Id.Value}: {friend.Name} (Friend:{friend.IsFriend}) (Blocked:{friend.IsBlocked})" );
				Console.WriteLine( $"		{string.Join( ", ", friend.NameHistory )}" );

				//	Assert.IsNotNull( friend.GetAvatar( Steamworks.Friends.AvatarSize.Medium ) );
			}
		}

		[TestMethod]
		public async Task LargeAvatar()
		{
			ulong id = (ulong)(76561197960279927 + (new Random().Next() % 10000));

			var image = await SteamFriends.GetLargeAvatarAsync( id );
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

			var image = await SteamFriends.GetMediumAvatarAsync( id );
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

			var image = await SteamFriends.GetSmallAvatarAsync( id );
			if ( !image.HasValue )
				return;

			Console.WriteLine( $"image.Width {image.Value.Width}" );
			Console.WriteLine( $"image.Height {image.Value.Height}" );

			DrawImage( image.Value );
		}

		[TestMethod]
		public async Task GetFriendsAvatars()
		{
			foreach ( var friend in SteamFriends.GetFriends() )
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

		[TestMethod]
		public async Task OpenWebOverlay()
		{
			if ( SteamUtils.IsOverlayEnabled )
				Console.WriteLine( "Overlay Is Enabled" );
			else
				Console.WriteLine( "Overlay Is Not Enabled" );

			SteamFriends.OpenWebOverlay( "https://www.google.com/" );

			await Task.Delay( 2000 );
		}


		public static void DrawImage( Image img )
		{
			var grad = " -:+#";

			for ( int y = 0; y < img.Height; y++ )
			{
				var str = "";

				for ( int x = 0; x < img.Width; x++ )
				{
					var p = img.GetPixel( x, y );

					var brightness = 1 - ((float)(p.r + p.g + p.b) / (255.0f * 3.0f));
					var c = (int)((grad.Length) * brightness);
					if ( c > 3 ) c = 3;
					str += grad[c];

				}

				Console.WriteLine( str );
			}
		}

	}
}