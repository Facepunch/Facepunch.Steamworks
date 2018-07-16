using System;
using System.Text;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Diagnostics;
using Facepunch.Steamworks.Callbacks;

namespace Facepunch.Steamworks.Test
{
    [TestClass]
    [DeploymentItem( "steam_api.dll" )]
    [DeploymentItem( "steam_api64.dll" )]
    public class WorkshopTest
    {
        [TestMethod]
        public void Query()
        {
            using ( var client = new Facepunch.Steamworks.Client( 252490 ) )
            {
                Assert.IsTrue( client.IsValid );

                var Query = client.Workshop.CreateQuery();
                Query.Order = Workshop.Order.RankedByTrend;

                Query.Run();

                // Block, wait for result
                // (don't do this in realtime)
                Query.Block();

                Assert.IsFalse( Query.IsRunning );
                Assert.IsTrue( Query.TotalResults > 0 );
                Assert.IsTrue( Query.Items.Length > 0 );

                Console.WriteLine( "Query.TotalResults: {0}", Query.TotalResults );
                Console.WriteLine( "Query.Items.Length: {0}", Query.Items.Length );

                // results

                Console.WriteLine( "Searching" );

                Query.Order = Workshop.Order.RankedByTextSearch;
                Query.QueryType = Workshop.QueryType.MicrotransactionItems;
                Query.SearchText = "black";
                Query.RequireTags.Add( "LongTShirt Skin" );
                Query.Run();

                // Block, wait for result
                // (don't do this in realtime)
                Query.Block();

                Console.WriteLine( "Query.TotalResults: {0}", Query.TotalResults );
                Console.WriteLine( "Query.Items.Length: {0}", Query.Items.Length );

                Assert.IsTrue( Query.TotalResults > 0 );
                Assert.IsTrue( Query.Items.Length > 0 );

                foreach ( var item in Query.Items )
                {
                    Console.WriteLine( "{0}", item.Title );
                    Console.WriteLine( "\t WebsiteViews:    {0}", item.WebsiteViews );
                    Console.WriteLine( "\t VotesUp:         {0}", item.VotesUp );
                    Console.WriteLine( "\t PreviewUrl:      {0}", item.PreviewImageUrl );

                    Assert.IsTrue( item.PreviewImageUrl.Contains( "http" ) );
                }
            }
        }

        [TestMethod]
        public void QueryMyFiles()
        {
            using ( var client = new Facepunch.Steamworks.Client( 252490 ) )
            {
                Assert.IsTrue( client.IsValid );

                var Query = client.Workshop.CreateQuery();

                Query.UserId = client.SteamId;
                Query.UserQueryType = Workshop.UserQueryType.Published;

                Query.Run();

                // Block, wait for result
                // (don't do this in realtime)
                Query.Block();

                Assert.IsFalse( Query.IsRunning );
                Assert.IsTrue( Query.TotalResults > 0 );
                Assert.IsTrue( Query.Items.Length > 0 );

                Console.WriteLine( "Query.TotalResults: {0}", Query.TotalResults );
                Console.WriteLine( "Query.Items.Length: {0}", Query.Items.Length );

                foreach ( var item in Query.Items )
                {
                    Console.WriteLine( "{0}", item.Title );
                    Console.WriteLine( "\t WebsiteViews:    {0}", item.WebsiteViews );
                    Console.WriteLine( "\t VotesUp:         {0}", item.VotesUp );
                    Console.WriteLine( "\t PreviewUrl:      {0}", item.PreviewImageUrl );
                    Console.WriteLine( "\t Directory:      {0}", item.Directory );

                    Assert.IsTrue( item.PreviewImageUrl.Contains( "http" ) );
                }
            }
        }

        [TestMethod]
        public void QueryTagRequire()
        {
            using ( var client = new Facepunch.Steamworks.Client( 252490 ) )
            {
                Assert.IsTrue( client.IsValid );

                using ( var Query = client.Workshop.CreateQuery() )
                {
                    Query.RequireTags.Add( "LongTShirt Skin" );
                    Query.Run();

                    Query.Block();

                    Assert.IsFalse( Query.IsRunning );
                    Assert.IsTrue( Query.TotalResults > 0 );
                    Assert.IsTrue( Query.Items.Length > 0 );

                    Console.WriteLine( "Query.TotalResults: {0}", Query.TotalResults );
                    Console.WriteLine( "Query.Items.Length: {0}", Query.Items.Length );

                    Assert.IsTrue( Query.TotalResults > 0 );
                    Assert.IsTrue( Query.Items.Length > 0 );

                    foreach ( var item in Query.Items )
                    {
                        Console.WriteLine( "{0}", item.Title );
                        Console.WriteLine( "\t{0}", string.Join( ";", item.Tags ) );

                        Assert.IsTrue( item.Tags.Contains( "longtshirt skin" ) );
                    }
                }
            }
        }

        [TestMethod]
        public void QueryTagRequireMultiple()
        {
            using ( var client = new Facepunch.Steamworks.Client( 252490 ) )
            {
                Assert.IsTrue( client.IsValid );

                using ( var Query = client.Workshop.CreateQuery() )
                {
                    Query.RequireTags.Add( "LongTShirt Skin" );
                    Query.RequireTags.Add( "version2" );
                    Query.RequireAllTags = true;
                    Query.Run();

                    Query.Block();

                    Assert.IsFalse( Query.IsRunning );
                    Assert.IsTrue( Query.TotalResults > 0 );
                    Assert.IsTrue( Query.Items.Length > 0 );

                    Console.WriteLine( "Query.TotalResults: {0}", Query.TotalResults );
                    Console.WriteLine( "Query.Items.Length: {0}", Query.Items.Length );

                    Assert.IsTrue( Query.TotalResults > 0 );
                    Assert.IsTrue( Query.Items.Length > 0 );

                    foreach ( var item in Query.Items )
                    {
                        Console.WriteLine( "{0}", item.Title );
                        Console.WriteLine( "\t{0}", string.Join( ";", item.Tags ) );

                        Assert.IsTrue( item.Tags.Contains( "longtshirt skin" ) );
                        Assert.IsTrue( item.Tags.Contains( "version2" ) );
                    }
                }
            }
        }

        [TestMethod]
        public void QueryTagExclude()
        {
            using ( var client = new Facepunch.Steamworks.Client( 252490 ) )
            {
                Assert.IsTrue( client.IsValid );

                using ( var Query = client.Workshop.CreateQuery() )
                {
                    Query.RequireTags.Add( "LongTShirt Skin" );
                    Query.ExcludeTags.Add( "version2" );
                    Query.Run();

                    Query.Block();

                    Assert.IsFalse( Query.IsRunning );
                    Assert.IsTrue( Query.TotalResults > 0 );
                    Assert.IsTrue( Query.Items.Length > 0 );

                    Console.WriteLine( "Query.TotalResults: {0}", Query.TotalResults );
                    Console.WriteLine( "Query.Items.Length: {0}", Query.Items.Length );

                    Assert.IsTrue( Query.TotalResults > 0 );
                    Assert.IsTrue( Query.Items.Length > 0 );

                    foreach ( var item in Query.Items )
                    {
                        Console.WriteLine( "{0}", item.Title );
                        Console.WriteLine( "\t{0}", string.Join( ";", item.Tags ) );

                        Assert.IsTrue( item.Tags.Contains( "longtshirt skin" ) );
                        Assert.IsFalse( item.Tags.Contains( "version2" ) );
                    }
                }
            }
        }

        [TestMethod]
        public void QueryFile()
        {
            using ( var client = new Facepunch.Steamworks.Client( 252490 ) )
            {
                Assert.IsTrue( client.IsValid );

                using ( var Query = client.Workshop.CreateQuery() )
                {
                    Query.FileId.Add( 751993251 );
                    Query.Run();

                    Assert.IsTrue( Query.IsRunning );

                    Query.Block();

                    Assert.IsFalse( Query.IsRunning );
                    Assert.AreEqual( Query.TotalResults, 1 );
                    Assert.AreEqual( Query.Items.Length, 1 );

                    Console.WriteLine( "Query.TotalResults: {0}", Query.TotalResults );
                    Console.WriteLine( "Query.Items.Length: {0}", Query.Items.Length );

                    Assert.AreEqual<ulong>( Query.Items[0].Id, 751993251 );
                }
            }
        }

        [TestMethod]
        public void QueryCallback()
        {
            using ( var client = new Facepunch.Steamworks.Client( 252490 ) )
            {
                Assert.IsTrue( client.IsValid );

                using ( var Query = client.Workshop.CreateQuery() )
                {
                    var gotCallback = false;

                    Query.OnResult = ( q ) =>
                    {
                        Assert.AreEqual( q.Items.Length, 1 );
                        Console.WriteLine( "Query.TotalResults: {0}", q.TotalResults );
                        Console.WriteLine( "Query.Items.Length: {0}", q.Items.Length );

                        gotCallback = true;
                    };

                    Query.FileId.Add( 751993251 );
                    Query.Run();

                    Assert.IsTrue( Query.IsRunning );

                    client.UpdateWhile( () => gotCallback == false );

                    Assert.IsFalse( Query.IsRunning );
                    Assert.AreEqual( Query.TotalResults, 1 );

                    Assert.AreEqual<ulong>( Query.Items[0].Id, 751993251 );
                }
            }
        }

        [TestMethod]
        public void QueryFiles()
        {
            using ( var client = new Facepunch.Steamworks.Client( 252490 ) )
            {
                Assert.IsTrue( client.IsValid );

                using ( var Query = client.Workshop.CreateQuery() )
                {
                    Query.FileId.Add( 751993251 );
                    Query.FileId.Add( 747266909 );
                    Query.Run();

                    Assert.IsTrue( Query.IsRunning );

                    Query.Block();

                    Assert.IsFalse( Query.IsRunning );
                    Assert.AreEqual( Query.TotalResults, 2 );
                    Assert.AreEqual( Query.Items.Length, 2 );

                    Console.WriteLine( "Query.TotalResults: {0}", Query.TotalResults );
                    Console.WriteLine( "Query.Items.Length: {0}", Query.Items.Length );

                    Assert.IsTrue( Query.Items.Any( x => x.Id == 751993251 ) );
                    Assert.IsTrue( Query.Items.Any( x => x.Id == 747266909 ) );
                }
            }
        }

        [TestMethod]
        public void Query_255()
        {
            using ( var client = new Facepunch.Steamworks.Client( 252490 ) )
            {
                Assert.IsTrue( client.IsValid );

                using ( var Query = client.Workshop.CreateQuery() )
                {
                    Query.PerPage = 255;
                    Query.Run();

                    Assert.IsTrue( Query.IsRunning );

                    Query.Block();

                    Assert.IsFalse( Query.IsRunning );
                    Assert.AreEqual( Query.Items.Length, 255 );

                    Console.WriteLine( "Query.TotalResults: {0}", Query.TotalResults );
                    Console.WriteLine( "Query.Items.Length: {0}", Query.Items.Length );
                }
            }
        }

        [TestMethod]
        public void Query_28()
        {
            using ( var client = new Facepunch.Steamworks.Client( 252490 ) )
            {
                Assert.IsTrue( client.IsValid );

                using ( var Query = client.Workshop.CreateQuery() )
                {
                    Query.PerPage = 28;
                    Query.Run();
                    Query.Block();

                    var firstPage = Query.Items;
                    Assert.AreEqual( firstPage.Length, 28 );

                    Console.WriteLine( "Page 2" );
                    Query.Page++;
                    Query.Run();
                    Query.Block();

                    
                    var secondPage = Query.Items;
                    Assert.AreEqual( secondPage.Length, 28 );

                    Console.WriteLine( "Page 3" );
                    Query.Page++;
                    Query.Run();
                    Query.Block();

                    var thirdPage = Query.Items;
                    Assert.AreEqual( thirdPage.Length, 28 );

                    foreach ( var i in firstPage )
                    {
                        Assert.IsFalse( secondPage.Any( x => x.Id == i.Id ) );
                        Assert.IsFalse( thirdPage.Any( x => x.Id == i.Id ) );
                    }

                    foreach ( var i in secondPage )
                    {
                        Assert.IsFalse( firstPage.Any( x => x.Id == i.Id ) );
                        Assert.IsFalse( thirdPage.Any( x => x.Id == i.Id ) );
                    }

                    foreach ( var i in thirdPage )
                    {
                        Assert.IsFalse( secondPage.Any( x => x.Id == i.Id ) );
                        Assert.IsFalse( firstPage.Any( x => x.Id == i.Id ) );
                    }
                }
            }
        }

        [TestMethod]
        public void DownloadFile()
        {
            using ( var client = new Facepunch.Steamworks.Client( 252490 ) )
            {
                Assert.IsTrue( client.IsValid );

                var item = client.Workshop.GetItem( 844466101 );

                var time = Stopwatch.StartNew();
                if ( !item.Installed )
                {
                    item.Download();

                    while ( !item.Installed )
                    {
                        Thread.Sleep( 500 );
                        client.Update();
                        Console.WriteLine( "Download Progress: {0}", item.DownloadProgress );

                        if (time.Elapsed.Seconds > 30)
                            throw new Exception("item.Installed Took Too Long");
                    }
                }

                Assert.IsNotNull( item.Directory );
                Assert.AreNotEqual( 0, item.Size );

                Console.WriteLine( "item.Installed:         {0}", item.Installed );
                Console.WriteLine( "item.Downloading:       {0}", item.Downloading );
                Console.WriteLine( "item.DownloadPending:   {0}", item.DownloadPending );
                Console.WriteLine( "item.Directory:         {0}", item.Directory );
                Console.WriteLine( "item.Size:              {0}mb", (item.Size / 1024 / 1024) );
            }
        }

        [TestMethod]
        [TestCategory( "Run Manually" )]
        public void CreatePublish()
        {
            using ( var client = new Facepunch.Steamworks.Client( 252490 ) )
            {
                Assert.IsTrue( client.IsValid );

                var item = client.Workshop.CreateItem( Workshop.ItemType.Microtransaction );

                item.Title = "Facepunch.Steamworks Unit test";
                item.Tags.Add( "Apple" );
                item.Tags.Add( "Banana" );

                // Make a folder
                var testFolder = new System.IO.DirectoryInfo("BlahBlah");
                if (!testFolder.Exists) testFolder.Create();

                item.Folder = testFolder.FullName;

                // Upload a file of random bytes
                var rand = new Random();
                var testFile = new byte[1024 * 1024 * 32];
                rand.NextBytes(testFile);
                System.IO.File.WriteAllBytes( testFolder.FullName + "/testfile1.bin", testFile);


                Console.WriteLine(item.Folder);

                item.OnChangesSubmitted += result =>
                {
                    Console.WriteLine( "OnChangesSubmitted called: " + result );
                    Assert.AreEqual( Result.OK, result );
                };

                try
                {
                    item.Publish();

                    while ( item.Publishing )
                    {
                        client.Update();
                        Thread.Sleep( 10 );

                        Console.WriteLine("Progress: " + item.Progress);
                        Console.WriteLine("BytesUploaded: " + item.BytesUploaded);
                        Console.WriteLine("BytesTotal: " + item.BytesTotal);
                    }

                    Assert.IsFalse( item.Publishing );
                    Assert.AreNotEqual( 0, item.Id );
                    Assert.IsNull( item.Error, item.Error );

                    Console.WriteLine( "item.Id: {0}", item.Id );

                    using ( var Query = client.Workshop.CreateQuery() )
                    {
                        Query.FileId.Add( item.Id );
                        Query.Run();

                        Query.Block();

                        var itemInfo = Query.Items[0];

                        Assert.AreEqual( itemInfo.Id, item.Id );
                        Assert.AreEqual( itemInfo.OwnerId, client.SteamId );
                        Assert.AreEqual( itemInfo.Title, item.Title );
                        Assert.IsTrue( itemInfo.Tags.Contains( "apple" ), "Missing Tag" );
                        Assert.IsTrue( itemInfo.Tags.Contains( "banana" ), "Missing Tag" );
                    }
                }
                finally
                {
                    Console.WriteLine( "Deleting: {0}", item.Id );
                    item.Delete();

                    System.IO.File.Delete(testFolder.FullName + "/testfile.bin");
                }
            }
        }

        [TestMethod]
        public void UserQuery()
        {
            using ( var client = new Facepunch.Steamworks.Client( 252490 ) )
            {
                Assert.IsTrue( client.IsValid );

                var Query = client.Workshop.CreateQuery();

                Query.UserId = 76561197960279927;
                Query.UserQueryType = Workshop.UserQueryType.Published;

                Query.Run();

                // Block, wait for result
                // (don't do this in realtime)
                Query.Block();

                Assert.IsFalse( Query.IsRunning );
                Assert.IsTrue( Query.TotalResults > 0 );
                Assert.IsTrue( Query.Items.Length > 0 );

                Console.WriteLine( "Query.TotalResults: {0}", Query.TotalResults );
                Console.WriteLine( "Query.Items.Length: {0}", Query.Items.Length );

                foreach ( var item in Query.Items )
                {
                    Console.WriteLine( "{0}", item.Title );
                    Assert.AreEqual<ulong>( item.OwnerId, 76561197960279927 );
                }
            }
        }

    }
}
