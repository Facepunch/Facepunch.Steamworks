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
			var time = Utils.SecondsSinceAppActive;
			Console.WriteLine( $"{time}" );
		}

		[TestMethod]
		public void SecondsSinceComputerActive()
		{
			var time = Utils.SecondsSinceComputerActive;
			Console.WriteLine( $"{time}" );
		}

		[TestMethod]
		public void ConnectedUniverse()
		{
			var u = Utils.ConnectedUniverse;
			Console.WriteLine( $"{u}" );
		}

		[TestMethod]
		public void SteamServerTime()
		{
			var time = Utils.SteamServerTime;
			Console.WriteLine( $"{time}" );
		}

		[TestMethod]
		public void IpCountry()
		{
			var cnt = Utils.IpCountry;
			Console.WriteLine( $"{cnt}" );
		}

		[TestMethod]
		public void UsingBatteryPower()
		{
			var cnt = Utils.UsingBatteryPower;
			Console.WriteLine( $"{cnt}" );
		}

		[TestMethod]
		public void CurrentBatteryPower()
		{
			var cnt = Utils.CurrentBatteryPower;
			Console.WriteLine( $"{cnt}" );
		}

		[TestMethod]
		public void AppId()
		{
			var cnt = Utils.AppId;

			Assert.IsTrue( cnt.Value > 0 );

			Console.WriteLine( $"{cnt.Value}" );
		}

		[TestMethod]
		public void IsOverlayEnabled()
		{
			var cnt = Utils.IsOverlayEnabled;
			Console.WriteLine( $"{cnt}" );
		}

		[TestMethod]
		public async Task CheckFileSignature()
		{
			var sig = await Utils.CheckFileSignature( "hl2.exe" );
			Console.WriteLine( $"{sig}" );
		}

		[TestMethod]
		public void SteamUILanguage()
		{
			var cnt = Utils.SteamUILanguage;
			Console.WriteLine( $"{cnt}" );
		}

		[TestMethod]
		public void IsSteamRunningInVR()
		{
			var cnt = Utils.IsSteamRunningInVR;
			Console.WriteLine( $"{cnt}" );
		}

		[TestMethod]
		public void IsSteamInBigPictureMode()
		{
			var cnt = Utils.IsSteamInBigPictureMode;
			Console.WriteLine( $"{cnt}" );
		}

		[TestMethod]
		public void VrHeadsetStreaming()
		{
			var cnt = Utils.VrHeadsetStreaming;
			Console.WriteLine( $"{cnt}" );
		}

	}

}
