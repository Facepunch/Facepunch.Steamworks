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
                var query = client.ServerList.Test();

                for ( int i = 0; i < 100; i++ )
                {
                    client.Update();
                    System.Threading.Thread.Sleep( 10 );

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

                for ( int i = 0; i < 10; i++ )
                    queries.Add( client.ServerList.Test() );

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
        public void HistoryList()
        {
            using ( var client = new Facepunch.Steamworks.Client( 252490 ) )
            {
                var query = client.ServerList.History( new Dictionary<string, string>() );

                while ( true )
                {
                    client.Update();
                    System.Threading.Thread.Sleep( 2 );

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
    }
}
