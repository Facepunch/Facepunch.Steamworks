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

		[TestMethod]
        public async Task UploadBigFile()
        {

			var created = Ugc.Editor.NewCommunityFile
							.WithTitle( "Unit Test Upload Item" )
							.WithDescription( "This item was created by Facepunch Steamworks unit tests.\n\n" +
									"It should have technically been deleted so you should never get to " +
									"read this unless something terrible has happened." )
							//.WithTag( "Apple" )
							//.WithTag( "Banana" )
							;


			// Make a folder
			var testFolder = new System.IO.DirectoryInfo( "WorkshopUpload" );
			if ( !testFolder.Exists ) testFolder.Create();

			created = created.WithContent( testFolder.FullName );

			// Upload a file of random bytes
			var rand = new Random();
			var testFile = new byte[1024 * 1024 * 256];
			rand.NextBytes( testFile );
			System.IO.File.WriteAllBytes( testFolder.FullName + "/testfile1.bin", testFile );

			Console.WriteLine( testFolder.FullName );

			try
			{
				var done = await created.SubmitAsync();

				// TODO - Upload Progress

				Assert.IsTrue( done.Success );
				Console.WriteLine( "item.Id: {0}", done.FileId );

				var deleted = await SteamUGC.DeleteFileAsync( done.FileId );
				Assert.IsTrue( deleted );
			}
			finally
			{
				System.IO.File.Delete( testFolder.FullName + "/testfile.bin" );
			}

		}
	}

}
