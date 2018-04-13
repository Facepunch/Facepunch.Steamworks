using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Facepunch.Steamworks.Test
{
    [TestClass]
    [DeploymentItem( "steam_api.dll" )]
    [DeploymentItem( "steam_api64.dll" )]
    public partial class ServerList
    {
        [TestMethod]
        public void IpAddressConversions()
        {
            var ipstr = "185.38.150.40";
            var ip = IPAddress.Parse( ipstr );

            var ip_int = Facepunch.Steamworks.Utility.IpToInt32( ip );

            var ip_back = Facepunch.Steamworks.Utility.Int32ToIp( ip_int );

            Console.WriteLine( "ipstr: " + ipstr );
            Console.WriteLine( "ip: " + ip );
            Console.WriteLine( "ip int: " + ip_int );
            Console.WriteLine( "ip_back: " + ip_back );
        }


        [TestMethod]
        public void InternetList()
        {
            using ( var client = new Facepunch.Steamworks.Client( 252490 ) )
            {
                var filter = new Facepunch.Steamworks.ServerList.Filter();
                filter.Add( "appid", client.AppId.ToString() );
                filter.Add( "gamedir", "rust" );
                filter.Add( "secure", "1" );

                var query = client.ServerList.Internet( filter );

                for ( int i = 0; i < 1000; i++ )
                {
                    client.Update();
                    System.Threading.Thread.Sleep( 100 );

                    foreach ( var s in query.Responded )
                    {
                        Assert.AreEqual( s.AppId, client.AppId );
                        Assert.AreEqual( s.GameDir, "rust" );
                    }

                    if ( query.Finished )
                        break;
                }

                Assert.IsTrue( query.Responded.Count > 0 );

                Console.WriteLine( "Responded: " + query.Responded.Count.ToString() );
                Console.WriteLine( "Unresponsive: " + query.Unresponsive.Count.ToString() );

                foreach ( var server in query.Responded.Take( 20 ) )
                {
                    Console.WriteLine( "{0} {1}", server.Address, server.Name );
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
        public void MultipleInternetList()
        {
            using ( var client = new Facepunch.Steamworks.Client( 252490 ) )
            {
                var queries = new List<Facepunch.Steamworks.ServerList.Request>();

                var filter = new Facepunch.Steamworks.ServerList.Filter();
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
                var filter = new Facepunch.Steamworks.ServerList.Filter();
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
                var filter = new Facepunch.Steamworks.ServerList.Filter();
                filter.Add( "appid", client.AppId.ToString() );
                filter.Add( "gamedir", "rust" );
                filter.Add( "secure", "1" );

                var query = client.ServerList.History( filter );

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

        [TestMethod]
        public void FavouriteList()
        {
            using ( var client = new Facepunch.Steamworks.Client( 252490 ) )
            {
                var filter = new Facepunch.Steamworks.ServerList.Filter();
                filter.Add( "appid", client.AppId.ToString() );
                filter.Add( "gamedir", "rust" );
                filter.Add( "secure", "1" );

                var query = client.ServerList.Favourites( filter );

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

        [TestMethod]
        public void LocalList()
        {
            using ( var client = new Facepunch.Steamworks.Client( 252490 ) )
            {
                var filter = new Facepunch.Steamworks.ServerList.Filter();
                filter.Add( "appid", client.AppId.ToString() );
                filter.Add( "gamedir", "rust" );
                filter.Add( "secure", "1" );

                var query = client.ServerList.Local( filter );

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

        [TestMethod]
        public void CustomList()
        {
            using ( var client = new Facepunch.Steamworks.Client( 252490 ) )
            {
                var servers = new List<string>();

                servers.Add( "158.85.101.20:28015" );
                servers.Add( "158.85.101.20:28022" );
                servers.Add( "173.192.176.171:28615" );
                servers.Add( "109.95.212.35:28215" );
                servers.Add( "109.95.212.35:28115" );
                servers.Add( "27.50.72.176:28015" );
                servers.Add( "109.95.212.40:28015" );
                servers.Add( "212.38.168.149:28215" );
                servers.Add( "27.50.72.167:28215" );
                servers.Add( "85.236.105.7:28215" );
                servers.Add( "107.182.233.216:28215" );
                servers.Add( "85.236.105.11:28215" );
                servers.Add( "109.95.211.198:28215" );
                servers.Add( "8.26.94.190:28015" );
                servers.Add( "221.121.151.37:28215" );
                servers.Add( "161.202.144.216:28215" );
                servers.Add( "107.182.230.181:28215" );
                servers.Add( "107.182.231.134:27101" );
                servers.Add( "107.182.233.181:27101" );
                servers.Add( "78.129.153.47:27101" );
                servers.Add( "109.95.211.206:27101" );
                servers.Add( "169.57.142.73:27101" );
                servers.Add( "221.121.154.147:27101" );
                servers.Add( "31.216.52.44:30015" );
                servers.Add( "109.169.94.17:28215" );
                servers.Add( "109.169.94.17:28315" );
                servers.Add( "109.169.94.17:28015" );
                servers.Add( "41.0.11.167:27141" );
                servers.Add( "78.129.153.47:27131" );
                servers.Add( "109.95.211.206:27111" );
                servers.Add( "107.182.231.134:27111" );
                servers.Add( "198.27.70.162:28015" );
                servers.Add( "198.27.70.162:28215" );
                servers.Add( "198.27.70.162:28115" );
                servers.Add( "169.57.142.73:27111" );
                servers.Add( "221.121.154.147:27111" );
                servers.Add( "107.182.233.181:27111" );
                servers.Add( "78.129.153.47:27111" );
                servers.Add( "109.95.211.215:28015" );
                servers.Add( "50.23.131.208:28015" );
                servers.Add( "50.23.131.208:28115" );
                servers.Add( "50.23.131.208:28215" );
                servers.Add( "63.251.114.37:28215" );
                servers.Add( "63.251.114.37:28115" );
                servers.Add( "63.251.114.37:28015" );
                servers.Add( "149.202.89.85:27101" );
                servers.Add( "149.202.89.85:27111" );
                servers.Add( "149.202.89.85:27131" );
                servers.Add( "8.26.94.147:27101" );
                servers.Add( "8.26.94.147:27111" );
                servers.Add( "8.26.94.147:27121" );
                servers.Add( "159.8.147.197:28025" );
                servers.Add( "162.248.88.203:27038" );
                servers.Add( "162.248.88.203:28091" );
                servers.Add( "74.91.119.142:28069" );
                servers.Add( "162.248.88.203:25063" );
                servers.Add( "64.251.7.189:28115" );
                servers.Add( "64.251.7.189:28015" );
                servers.Add( "216.52.0.170:28215" );
                servers.Add( "217.147.91.80:28215" );
                servers.Add( "63.251.112.121:28215" );
                servers.Add( "162.248.88.203:28074" );
                servers.Add( "74.91.119.142:27095" );
                servers.Add( "95.172.92.176:28065" );
                servers.Add( "192.223.26.55:26032" );
                servers.Add( "40.114.199.6:28085" );
                servers.Add( "95.172.92.176:27095" );
                servers.Add( "216.52.0.172:28015" );
                servers.Add( "216.52.0.171:28115" );
                servers.Add( "27.50.72.179:28015" );
                servers.Add( "27.50.72.180:28115" );
                servers.Add( "221.121.158.203:28015" );
                servers.Add( "63.251.242.246:28015" );
                servers.Add( "85.236.105.51:28015" );
                servers.Add( "85.236.105.47:28015" );
                servers.Add( "209.95.60.216:28015" );
                servers.Add( "212.38.168.14:28015" );
                servers.Add( "217.147.91.138:28015" );
                servers.Add( "31.216.52.42:28015" );
                servers.Add( "107.182.226.225:28015" );
                servers.Add( "109.95.211.69:28015" );
                servers.Add( "209.95.56.13:28015" );
                servers.Add( "173.244.192.101:28015" );
                servers.Add( "221.121.158.201:28115" );
                servers.Add( "63.251.242.245:28115" );
                servers.Add( "85.236.105.50:28115" );
                servers.Add( "85.236.105.46:28115" );
                servers.Add( "209.95.60.217:28115" );
                servers.Add( "212.38.168.13:28115" );
                servers.Add( "217.147.91.139:28115" );
                servers.Add( "107.182.226.224:28115" );
                servers.Add( "109.95.211.14:28115" );
                servers.Add( "109.95.211.16:28115" );
                servers.Add( "109.95.211.17:28115" );
                servers.Add( "209.95.56.14:28115" );
                servers.Add( "173.244.192.100:28115" );
                servers.Add( "209.95.60.218:28215" );
                servers.Add( "109.95.211.13:28215" );
                servers.Add( "109.95.211.15:28215" );
                servers.Add( "31.216.52.41:29015" );

                var query = client.ServerList.Custom( servers );

                for ( int i = 0; i < 1000; i++ )
                {
                    client.Update();
                    System.Threading.Thread.Sleep( 20 );

                    if ( query.Finished )
                        break;
                }

                Console.WriteLine( "Responded: " + query.Responded.Count.ToString() );
                Console.WriteLine( "Unresponsive: " + query.Unresponsive.Count.ToString() );

                foreach ( var s in query.Responded )
                {
                    Console.WriteLine( "{0} - {1}", s.Address, s.Name );

                    Assert.IsTrue( servers.Contains( $"{s.Address}:{s.QueryPort}" ) );
                }

                query.Dispose();
            }
        }

        [TestMethod]
        public void Rules()
        {
            using ( var client = new Facepunch.Steamworks.Client( 252490 ) )
            {
                var filter = new Facepunch.Steamworks.ServerList.Filter();
                filter.Add( "appid", client.AppId.ToString() );
                filter.Add( "gamedir", "rust" );
                filter.Add( "secure", "1" );

                filter.Add( "addr", "185.97.254.146" );

                using ( var query = client.ServerList.Internet( filter ) )
                {
                    for ( int i = 0; i < 1000; i++ )
                    {
                        GC.Collect();
                        client.Update();
                        GC.Collect();
                        System.Threading.Thread.Sleep( 10 );

                    //    if ( query.Responded.Count > 20 )
                     //       break;

                        if ( query.Finished )
                            break;
                    }

                    query.Dispose();

                    var servers = query.Responded.Take( 100 );

                   foreach ( var server in servers )
                    {
                        server.FetchRules();

                        int i = 0;
                        while ( !server.HasRules )
                        {
                            i++;
                            client.Update();
                            System.Threading.Thread.Sleep( 10 );

                            if ( i > 100 )
                                break;
                        }

                        if ( server.HasRules )
                        {
                            Console.WriteLine( "" );
                            Console.WriteLine( "" );
                            Console.WriteLine( server.Address );
                            Console.WriteLine( "" );

                            foreach ( var rule in server.Rules )
                            {
                                Console.WriteLine( rule.Key + " = " + rule.Value );
                            }
                        }
                        else
                        {
                            Console.WriteLine( "SERVER HAS NO RULES :(" );
                        }
                    }



                }                
            }
        }
    }
}
