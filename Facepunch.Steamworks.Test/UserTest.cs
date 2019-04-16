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
			Assert.AreNotEqual( false, SteamUser.IsLoggedOn );
			Console.WriteLine( $"User.IsLoggedOn: {SteamUser.IsLoggedOn}" );
		}

		[TestMethod]
		public void SteamID()
		{
			Assert.AreNotEqual( 0, SteamUser.SteamId.Value );
			Console.WriteLine( $"User.SteamID: {SteamUser.SteamId.Value}" );
		}

		[TestMethod]
		public void AuthSession()
		{
			var ticket = SteamUser.GetAuthSessionTicket();

			Assert.AreNotEqual( 0, ticket.Handle );
			Assert.AreNotEqual( 0, ticket.Data.Length );
			Console.WriteLine( $"ticket.Handle: {ticket.Handle}" );
			Console.WriteLine( $"ticket.Data: { string.Join( "", ticket.Data.Select( x => x.ToString( "x" ) ) ) }" );

			var result = SteamUser.BeginAuthSession( ticket.Data, SteamUser.SteamId );
			Console.WriteLine( $"result: { result }" );
			Assert.AreEqual( result, BeginAuthResult.OK );

			SteamUser.EndAuthSession( SteamUser.SteamId );
		}


		[TestMethod]
		public void SteamLevel()
		{
			Assert.AreNotEqual( 0, SteamUser.SteamLevel );
			Console.WriteLine( $"User.SteamLevel: {SteamUser.SteamLevel}" );
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
	}

}
