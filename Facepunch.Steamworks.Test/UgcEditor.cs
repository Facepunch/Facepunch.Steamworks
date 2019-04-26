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
    public class UgcEditor
    {
		[TestMethod]
        public async Task CreateFile()
        {
			var result = await Ugc.Editor.NewCommunityFile
							  .WithTitle( "Unit Test Created Item" )
							  .WithDescription( "This item was created by Facepunch Steamworks unit tests.\n\n" +
												"It should have technically been deleted so you should never get to " +
												"read this unless something terrible has happened." )
							  .SubmitAsync();

			Assert.IsTrue( result.Success );
			Assert.AreNotEqual( result.FileId.Value, 0 );

			var deleted = await SteamUGC.DeleteFileAsync( result.FileId );
			Assert.IsTrue( deleted );

		}
	}

}
