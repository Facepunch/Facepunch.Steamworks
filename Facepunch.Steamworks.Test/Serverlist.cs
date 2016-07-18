using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Facepunch.Steamworks.Test
{
    [TestClass]
    [DeploymentItem( "FacepunchSteamworksApi.dll" )]
    [DeploymentItem( "steam_appid.txt" )]
    public partial class ServerList
    {
        [TestMethod]
        public void InternetList()
        {
            using ( var client = new Facepunch.Steamworks.Client( 252490 ) )
            {
                var filter = new Dictionary<string, string>();
                filter.Add( "appid", client.AppId.ToString() );
                filter.Add( "gamedir", "rust" );
                filter.Add( "secure", "1" );

                var query = client.ServerList.Internet( filter );

                for ( int i = 0; i < 1000; i++ )
                {
                    client.Update();
                    System.Threading.Thread.Sleep( 10 );

                    foreach ( var s in query.Responded )
                    {
                        Assert.AreEqual( s.AppId, client.AppId );
                        Assert.AreEqual( s.GameDir, "rust" );
                    }

                    if ( query.Finished )
                        break;
                }               

                Console.WriteLine( "Responded: " + query.Responded.Count.ToString() );
                Console.WriteLine( "Unresponsive: " + query.Unresponsive.Count.ToString() );

                query.Dispose();

                for ( int i = 0; i < 100; i++ )
                {
                    client.Update();
                    System.Threading.Thread.Sleep( 1 );
                }
            }
        }

        [TestMethod]
        public void MultipleInternetList()
        {
            using ( var client = new Facepunch.Steamworks.Client( 252490 ) )
            {
                var queries = new List< Facepunch.Steamworks.ServerList.Request >();

                var filter = new Dictionary<string, string>();
                filter.Add( "map", "barren" );

                for ( int i = 0; i < 10; i++ )
                    queries.Add( client.ServerList.Internet( filter ) );

                for ( int i = 0; i < 100; i++ )
                {
                    client.Update();
                    System.Threading.Thread.Sleep( 5 );

                    if ( queries.Any( x => x.Finished ) )
                        break;
                }

                foreach ( var query in queries )
                {
                    Console.WriteLine( "Responded: " + query.Responded.Count.ToString() );
                    Console.WriteLine( "Unresponsive: " + query.Unresponsive.Count.ToString() );

                    client.Update();
                    query.Dispose();
                    client.Update();
                }
            }
        }

        [TestMethod]
        public void Filters()
        {
            using ( var client = new Facepunch.Steamworks.Client( 252490 ) )
            {
                var filter = new Dictionary<string, string>();
                filter.Add( "map", "barren" );


                var query = client.ServerList.Internet( filter );

                while ( true )
                {
                    client.Update();
                    System.Threading.Thread.Sleep( 2 );

                    if ( query.Finished )
                        break;
                }

                foreach ( var x in query.Responded )
                {
                    Assert.AreEqual( x.Map.ToLower(), "barren" );
                }

                query.Dispose();

                for ( int i = 0; i < 100; i++ )
                {
                    client.Update();
                    System.Threading.Thread.Sleep( 1 );
                }
            }
        }

        [TestMethod]
        public void HistoryList()
        {
            using ( var client = new Facepunch.Steamworks.Client( 252490 ) )
            {
                var query = client.ServerList.History();

                while ( true )
                {
                    client.Update();
                    System.Threading.Thread.Sleep( 2 );

                    if ( query.Finished )
                        break;
                }

                Console.WriteLine( "Responded: " + query.Responded.Count.ToString() );
                Console.WriteLine( "Unresponsive: " + query.Unresponsive.Count.ToString() );

                foreach ( var x in query.Responded )
                {
                    Console.WriteLine( x.Map );
                }

                query.Dispose();

                for ( int i = 0; i < 100; i++ )
                {
                    client.Update();
                    System.Threading.Thread.Sleep( 1 );
                }
            }
        }
    }
}
