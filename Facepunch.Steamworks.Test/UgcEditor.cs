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
							  .WithTag( "Arsehole" )
							  .WithTag( "Spiteful" )
							  .WithTag( "Fat-Head" )
							  .SubmitAsync();

			Assert.IsTrue( result.Success );
			Assert.AreNotEqual( result.FileId.Value, 0 );

			var deleted = await SteamUGC.DeleteFileAsync( result.FileId );
			Assert.IsTrue( deleted );

		}

		class ProgressBar : IProgress<float>
		{
			float Value = 0;

			public void Report( float value )
			{
				if ( Value >= value ) return;

				Value = value;

				Console.WriteLine( value );
			}
		}

		[TestMethod]
        public async Task UploadBigishFile()
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
			var testFile = new byte[1024 * 1024 * 32];
			rand.NextBytes( testFile );
			System.IO.File.WriteAllBytes( testFolder.FullName + "/testfile1.bin", testFile );

			Console.WriteLine( testFolder.FullName );

			try
			{
				var done = await created.SubmitAsync( new ProgressBar() );

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


		[TestMethod]
		public async Task CreateAndThenEditFile()
		{
			PublishedFileId fileid = default;

			//
			// Make a file
			//
			{
				var result = await Ugc.Editor.NewCommunityFile
								  .WithTitle( "Unedited File" )
								  .SubmitAsync();

				Assert.IsTrue( result.Success );
				Assert.AreNotEqual( result.FileId.Value, 0 );

				fileid = result.FileId;
			}

			await Task.Delay( 1000 );

			//
			// Edit it
			//
			{
				var editor = new Ugc.Editor( fileid );
				editor = editor.WithTitle( "An Edited File" );
				var result = await editor.SubmitAsync();

				Assert.IsTrue( result.Success );
				Assert.AreEqual( result.FileId, fileid );
			}

			await Task.Delay( 1000 );

			//
			// Make sure the edited file matches
			//
			{
				var details = await SteamUGC.QueryFileAsync( fileid ) ?? throw new Exception( "Somethign went wrong" );
				Assert.AreEqual( details.Id, fileid );
				Assert.AreEqual( details.Title, "An Edited File" );
			}

			//
			// Clean up
			//
			var deleted = await SteamUGC.DeleteFileAsync( fileid );
			Assert.IsTrue( deleted );

		}
	}

}
