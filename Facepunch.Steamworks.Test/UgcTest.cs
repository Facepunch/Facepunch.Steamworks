using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Steamworks.Data;

namespace Steamworks
{
    [TestClass]
    [DeploymentItem( "steam_api64.dll" )]
    public class UgcTest
    {
		[TestMethod]
        public async Task Download()
        {
			SteamUGC.Download( 1717844711 );
		}

		[TestMethod]
        public async Task GetInformation()
        {
			var itemInfo = await Ugc.Item.GetAsync( 1720164672 );

			Assert.IsTrue( itemInfo.HasValue );

			Console.WriteLine( $"Title: {itemInfo?.Title}" );
			Console.WriteLine( $"IsInstalled: {itemInfo?.IsInstalled}" );
			Console.WriteLine( $"IsDownloading: {itemInfo?.IsDownloading}" );
			Console.WriteLine( $"IsDownloadPending: {itemInfo?.IsDownloadPending}" );
			Console.WriteLine( $"IsSubscribed: {itemInfo?.IsSubscribed}" );
			Console.WriteLine( $"NeedsUpdate: {itemInfo?.NeedsUpdate}" );
			Console.WriteLine( $"Description: {itemInfo?.Description}" );
		}
	}
}
