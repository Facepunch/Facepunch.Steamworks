using System;
using System.Text;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Facepunch.Steamworks.Test
{
    [TestClass]
    [DeploymentItem( Config.LibraryName + ".dll" )]
    [DeploymentItem( "steam_appid.txt" )]
    public class WorkshopTest
    {
        [TestMethod]
        public void Query()
        {
            using ( var client = new Facepunch.Steamworks.Client( 252490 ) )
            {
                Assert.IsTrue( client.IsValid );

                var Query = client.Workshop.CreateQuery();

                Query.Run();

                // Block, wait for result
                // (don't do this in realtime)
                Query.Block();

                Assert.IsFalse( Query.IsRunning );
                Assert.IsTrue( Query.TotalResults > 0 );
                Assert.IsTrue( Query.Items.Length > 0 );

                // results

                Console.WriteLine( "Searching" );

                Query.Order = Workshop.Order.RankedByTextSearch;
                Query.QueryType = Workshop.QueryType.Items_Mtx;
                Query.SearchText = "shit";
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

                        Assert.IsTrue( item.Tags.Contains( "LongTShirt Skin" ) );
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

                        Assert.IsTrue( item.Tags.Contains( "LongTShirt Skin" ) );
                        Assert.IsFalse( item.Tags.Contains( "version2" ) );
                    }
                }
            }
        }

    }
}
