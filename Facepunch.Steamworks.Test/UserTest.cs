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

				User.VoiceRecord = true;

				var sw = Stopwatch.StartNew();

				while ( sw.Elapsed.TotalSeconds < 3 )
				{
					System.Threading.Thread.Sleep( 10 );
					compressed += User.ReadVoiceData( stream );
				}

				Assert.AreEqual( compressed, stream.Length );
				Console.WriteLine( $"compressed: {compressed}", compressed );
				Console.WriteLine( $"stream.Length: {stream.Length}", stream.Length );
			}
		}
		[TestMethod]
		public void OptimalSampleRate()
		{
			var rate = User.OptimalSampleRate;
			Assert.AreNotEqual( rate, 0 );
			Console.WriteLine( $"User.OptimalSampleRate: {User.OptimalSampleRate}" );
		}

		[TestMethod]
		public void IsLoggedOn()
		{
			Assert.AreNotEqual( false, User.IsLoggedOn );
			Console.WriteLine( $"User.IsLoggedOn: {User.IsLoggedOn}" );
		}

		[TestMethod]
		public void SteamID()
		{
			Assert.AreNotEqual( 0, User.SteamId.Value );
			Console.WriteLine( $"User.SteamID: {User.SteamId.Value}" );
		}

		[TestMethod]
		public void AuthSession()
		{
			var ticket = User.GetAuthSessionTicket();

			Assert.AreNotEqual( 0, ticket.Handle );
			Assert.AreNotEqual( 0, ticket.Data.Length );
			Console.WriteLine( $"ticket.Handle: {ticket.Handle}" );
			Console.WriteLine( $"ticket.Data: { string.Join( "", ticket.Data.Select( x => x.ToString( "x" ) ) ) }" );

			var result = User.BeginAuthSession( ticket.Data, User.SteamId );
			Console.WriteLine( $"result: { result }" );
			Assert.AreEqual( result, BeginAuthSessionResult.OK );

			User.EndAuthSession( User.SteamId );
		}


		[TestMethod]
		public void SteamLevel()
		{
			Assert.AreNotEqual( 0, User.SteamLevel );
			Console.WriteLine( $"User.SteamLevel: {User.SteamLevel}" );
		}

		[TestMethod]
		public async Task GetStoreAuthUrlAsync()
		{
			var rustskins = await User.GetStoreAuthUrlAsync( "https://store.steampowered.com/itemstore/252490/" );

			Assert.IsNotNull( rustskins );
			Console.WriteLine( $"rustskins: {rustskins}" );
		}

		[TestMethod]
		public void IsPhoneVerified()
		{
			Console.WriteLine( $"User.IsPhoneVerified: {User.IsPhoneVerified}" );
		}

		[TestMethod]
		public void IsTwoFactorEnabled()
		{
			Console.WriteLine( $"User.IsTwoFactorEnabled: {User.IsTwoFactorEnabled}" );
		}

		[TestMethod]
		public void IsPhoneIdentifying()
		{
			Console.WriteLine( $"User.IsPhoneIdentifying: {User.IsPhoneIdentifying}" );
		}

		[TestMethod]
		public void IsPhoneRequiringVerification()
		{
			Console.WriteLine( $"User.IsPhoneRequiringVerification: {User.IsPhoneRequiringVerification}" );
		}
	}

}
