using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Steamworks
{
    [TestClass]
    [DeploymentItem( "steam_api64.dll" )]
    public class UtilsTest
    {
		[TestMethod]
        public void SecondsSinceAppActive()
        {
			var time = SteamUtils.SecondsSinceAppActive;
			Console.WriteLine( $"{time}" );
		}

		[TestMethod]
		public void SecondsSinceComputerActive()
		{
			var time = SteamUtils.SecondsSinceComputerActive;
			Console.WriteLine( $"{time}" );
		}

		[TestMethod]
		public void ConnectedUniverse()
		{
			var u = SteamUtils.ConnectedUniverse;
			Console.WriteLine( $"{u}" );
		}

		[TestMethod]
		public void SteamServerTime()
		{
			var time = SteamUtils.SteamServerTime;
			Console.WriteLine( $"{time}" );
		}

		[TestMethod]
		public void IpCountry()
		{
			var cnt = SteamUtils.IpCountry;
			Console.WriteLine( $"{cnt}" );
		}

		[TestMethod]
		public void UsingBatteryPower()
		{
			var cnt = SteamUtils.UsingBatteryPower;
			Console.WriteLine( $"{cnt}" );
		}

		[TestMethod]
		public void CurrentBatteryPower()
		{
			var cnt = SteamUtils.CurrentBatteryPower;
			Console.WriteLine( $"{cnt}" );
		}

		[TestMethod]
		public void AppId()
		{
			var cnt = SteamUtils.AppId;

			Assert.IsTrue( cnt.Value > 0 );

			Console.WriteLine( $"{cnt.Value}" );
		}

		[TestMethod]
		public void IsOverlayEnabled()
		{
			var cnt = SteamUtils.IsOverlayEnabled;
			Console.WriteLine( $"{cnt}" );
		}

		[TestMethod]
		public async Task CheckFileSignature()
		{
			var sig = await SteamUtils.CheckFileSignature( "hl2.exe" );
			Console.WriteLine( $"{sig}" );
		}

		[TestMethod]
		public void SteamUILanguage()
		{
			var cnt = SteamUtils.SteamUILanguage;
			Console.WriteLine( $"{cnt}" );
		}

		[TestMethod]
		public void IsSteamRunningInVR()
		{
			var cnt = SteamUtils.IsSteamRunningInVR;
			Console.WriteLine( $"{cnt}" );
		}

		[TestMethod]
		public void IsSteamInBigPictureMode()
		{
			var cnt = SteamUtils.IsSteamInBigPictureMode;
			Console.WriteLine( $"{cnt}" );
		}

		[TestMethod]
		public void VrHeadsetStreaming()
		{
			var cnt = SteamUtils.VrHeadsetStreaming;
			Console.WriteLine( $"{cnt}" );
		}

	}

}
