using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Steamworks
{
    [TestClass]
    [DeploymentItem( "steam_api64.dll" )]
	[DeploymentItem( "steam_api.dll" )]
	public class UserTest
    {
		[TestMethod]
		public void GetVoice()
		{
			using ( var stream = new MemoryStream() )
			{
				int compressed = 0;

				SteamUser.VoiceRecord = true;

				var sw = Stopwatch.StartNew();

				while ( sw.Elapsed.TotalSeconds < 3 )
				{
					System.Threading.Thread.Sleep( 10 );
					compressed += SteamUser.ReadVoiceData( stream );
				}

				Assert.AreEqual( compressed, stream.Length );
				Console.WriteLine( $"compressed: {compressed}", compressed );
				Console.WriteLine( $"stream.Length: {stream.Length}", stream.Length );
			}
		}
		[TestMethod]
		public void OptimalSampleRate()
		{
			var rate = SteamUser.OptimalSampleRate;
			Assert.AreNotEqual( rate, 0 );
			Console.WriteLine( $"User.OptimalSampleRate: {SteamUser.OptimalSampleRate}" );
		}

		[TestMethod]
		public void IsLoggedOn()
		{
			Assert.AreNotEqual( false, SteamClient.IsLoggedOn );
			Console.WriteLine( $"User.IsLoggedOn: {SteamClient.IsLoggedOn}" );
		}

		[TestMethod]
		public void SteamID()
		{
			Assert.AreNotEqual( 0, SteamClient.SteamId.Value );
			Console.WriteLine( $"User.SteamID: {SteamClient.SteamId.Value}" );
		}

		[TestMethod]
		public void AuthSession()
		{
			var ticket = SteamUser.GetAuthSessionTicket( SteamClient.SteamId );

			Assert.AreNotEqual( 0, ticket.Handle );
			Assert.AreNotEqual( 0, ticket.Data.Length );
			Console.WriteLine( $"ticket.Handle: {ticket.Handle}" );
			Console.WriteLine( $"ticket.Data: { string.Join( "", ticket.Data.Select( x => x.ToString( "x" ) ) ) }" );

			var result = SteamUser.BeginAuthSession( ticket.Data, SteamClient.SteamId );
			Console.WriteLine( $"result: { result }" );
			Assert.AreEqual( result, BeginAuthResult.OK );

			SteamUser.EndAuthSession( SteamClient.SteamId );
		}

		[TestMethod]
		public async Task AuthSessionAsync()
		{
			var ticket = await SteamUser.GetAuthSessionTicketAsync( SteamClient.SteamId, 5.0 );

			Assert.AreNotEqual( 0, ticket.Handle );
			Assert.AreNotEqual( 0, ticket.Data.Length );
			Console.WriteLine( $"ticket.Handle: {ticket.Handle}" );
			Console.WriteLine( $"ticket.Data: { string.Join( "", ticket.Data.Select( x => x.ToString( "x" ) ) ) }" );
		}

		[TestMethod]
		public async Task AuthTicketForWebApiAsync()
		{
			var ticket = await SteamUser.GetAuthTicketForWebApiAsync( "Test" );

			Assert.AreNotEqual( 0, ticket.Handle );
			Assert.AreNotEqual( 0, ticket.Data.Length );
			Console.WriteLine( $"ticket.Handle: {ticket.Handle}" );
			Console.WriteLine( $"ticket.Data: { string.Join( "", ticket.Data.Select( x => x.ToString( "x" ) ) ) }" );
		}

		[TestMethod]
		public void SteamLevel()
		{
			Assert.AreNotEqual( 0, SteamUser.SteamLevel );
			Console.WriteLine( $"User.SteamLevel: {SteamUser.SteamLevel}" );
		}

		[TestMethod]
		public void Name()
		{
			Console.WriteLine( $"SteamClient.Name: {SteamClient.Name}" );
		}

		[TestMethod]
		public async Task GetStoreAuthUrlAsync()
		{
			var rustskins = await SteamUser.GetStoreAuthUrlAsync( "https://store.steampowered.com/itemstore/252490/" );

			Assert.IsNotNull( rustskins );
			Console.WriteLine( $"rustskins: {rustskins}" );
		}

		[TestMethod]
		public void IsPhoneVerified()
		{
			Console.WriteLine( $"User.IsPhoneVerified: {SteamUser.IsPhoneVerified}" );
		}

		[TestMethod]
		public void IsTwoFactorEnabled()
		{
			Console.WriteLine( $"User.IsTwoFactorEnabled: {SteamUser.IsTwoFactorEnabled}" );
		}

		[TestMethod]
		public void IsPhoneIdentifying()
		{
			Console.WriteLine( $"User.IsPhoneIdentifying: {SteamUser.IsPhoneIdentifying}" );
		}

		[TestMethod]
		public void IsPhoneRequiringVerification()
		{
			Console.WriteLine( $"User.IsPhoneRequiringVerification: {SteamUser.IsPhoneRequiringVerification}" );
		}


		[TestMethod]
		public async Task RequestEncryptedAppTicketAsyncWithData()
		{
			for ( int i=0; i<10; i++ )
			{
				var data = await SteamUser.RequestEncryptedAppTicketAsync( new byte[] { 0, 1, 2, 3, 4, 5, 6, 7 } );

				if ( data == null )
				{
					Console.WriteLine( $"Attempt {i}: Returned null.. waiting 1 seconds" );
					await Task.Delay( 10000 );
					continue;
				}

				Console.WriteLine( $"data: {BitConverter.ToString( data )}" );
				return;
			}

			Assert.Fail();
		}

		[TestMethod]
		public async Task RequestEncryptedAppTicketAsync()
		{
			for ( int i = 0; i < 6; i++ )
			{
				var data = await SteamUser.RequestEncryptedAppTicketAsync();

				if ( data == null )
				{
					Console.WriteLine( $"Attempt {i}: Returned null.. waiting 1 seconds" );
					await Task.Delay( 10000 );
					continue;
				}

				Console.WriteLine( $"data: {BitConverter.ToString( data )}" );
				return;
			}

			Assert.Fail();
		}

	}

}
