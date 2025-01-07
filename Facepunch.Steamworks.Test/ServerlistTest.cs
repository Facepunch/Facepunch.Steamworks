using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Steamworks
{
    [TestClass]
    [DeploymentItem( "steam_api64.dll" )]
	[DeploymentItem( "steam_api.dll" )]
	public partial class ServerListTest
    {
        [TestMethod]
        public void IpAddressConversions()
        {
            var ipstr = "185.38.150.40";
            var ip = IPAddress.Parse( ipstr );

            var ip_int = Utility.IpToInt32( ip );

            var ip_back = Utility.Int32ToIp( ip_int );

            Console.WriteLine( "ipstr: " + ipstr );
            Console.WriteLine( "ip: " + ip );
            Console.WriteLine( "ip int: " + ip_int );
            Console.WriteLine( "ip_back: " + ip_back );
        }


        [TestMethod]
        public async Task ServerListInternetInterupted()
        {
			using ( var list = new ServerList.Internet() )
			{
				var task = list.RunQueryAsync();

				await Task.Delay( 1000 );

				Console.WriteLine( $"Querying.." );

				list.Cancel();

				foreach ( var s in list.Responsive )
				{
					Console.WriteLine( $"{s.Address} {s.Name}" );
				}

				Console.WriteLine( $"Found {list.Responsive.Count} Responsive Servers" );
				Console.WriteLine( $"Found {list.Unresponsive.Count} Unresponsive Servers" );
				Console.WriteLine( $"task.IsCompleted {task.IsCompleted}" );

			}
		}

		[TestMethod]
		public async Task ServerListInternet()
		{
			using ( var list = new ServerList.Internet() )
			{
				var success = await list.RunQueryAsync();

				Console.WriteLine( $"success {success}" );
				Console.WriteLine( $"Found {list.Responsive.Count} Responsive Servers" );
				Console.WriteLine( $"Found {list.Unresponsive.Count} Unresponsive Servers" );
			}
		}

		// Used to reproduce steam serverlist stopping querying after ~10s around august 2023
		[TestMethod]
		public async Task RustServerListTest()
		{
			using ( var list = new ServerList.Internet() )
			{
				list.AddFilter( "secure", "1" );
				list.AddFilter( "and", "1" );
				list.AddFilter( "gametype", "v2405" );
				list.AddFilter( "appid", "252490" );
				list.AddFilter( "gamedir", "rust" );
				list.AddFilter( "empty", "1" );

				var success = await list.RunQueryAsync( 90 );

				Console.WriteLine( $"success {success}" );
				Console.WriteLine( $"Found {list.Responsive.Count} Responsive Servers" );
				Console.WriteLine( $"Found {list.Unresponsive.Count} Unresponsive Servers" );
			}
		}

		[TestMethod]
		public async Task SourceQuery()
		{
			using ( var list = new ServerList.Internet() )
			{
				var task = list.RunQueryAsync();
				await Task.Delay( 1000 );
				list.Cancel();

				foreach ( var s in list.Responsive.Take( 10 ).ToArray() )
				{
					Console.WriteLine( $"{s.Name} [{s.Address}]" );

					var rules = await s.QueryRulesAsync();
					Assert.IsNotNull( rules );

					foreach ( var rule in rules )
					{
						Console.WriteLine( $"	{rule.Key}  = {rule.Value}" );
					}
				}
			}
		}

		[TestMethod]
		public async Task ServerListLan()
		{
			using ( var list = new ServerList.LocalNetwork() )
			{
				var success = await list.RunQueryAsync();

				Console.WriteLine( $"success {success}" );
				Console.WriteLine( $"Found {list.Responsive.Count} Responsive Servers" );
				Console.WriteLine( $"Found {list.Unresponsive.Count} Unresponsive Servers" );
			}
		}

		[TestMethod]
		public async Task ServerListFavourites()
		{
			using ( var list = new ServerList.Favourites() )
			{
				var success = await list.RunQueryAsync();

				Console.WriteLine( $"success {success}" );
				Console.WriteLine( $"Found {list.Responsive.Count} Responsive Servers" );
				Console.WriteLine( $"Found {list.Unresponsive.Count} Unresponsive Servers" );
			}
		}

		[TestMethod]
		public async Task ServerListFriends()
		{
			using ( var list = new ServerList.Friends() )
			{
				var success = await list.RunQueryAsync();

				Console.WriteLine( $"success {success}" );
				Console.WriteLine( $"Found {list.Responsive.Count} Responsive Servers" );
				Console.WriteLine( $"Found {list.Unresponsive.Count} Unresponsive Servers" );
			}
		}

		[TestMethod]
		public async Task ServerListHistory()
		{
			using ( var list = new ServerList.History() )
			{
				var success = await list.RunQueryAsync();

				Console.WriteLine( $"success {success}" );
				Console.WriteLine( $"Found {list.Responsive.Count} Responsive Servers" );
				Console.WriteLine( $"Found {list.Unresponsive.Count} Unresponsive Servers" );
			}
		}

		[TestMethod]
		public async Task FilterByMap()
		{
			using ( var list = new ServerList.Internet() )
			{
				list.AddFilter( "map", "de_dust" );

				var success = await list.RunQueryAsync();

				Console.WriteLine( $"success {success}" );
				Console.WriteLine( $"Found {list.Responsive.Count} Responsive Servers" );
				Console.WriteLine( $"Found {list.Unresponsive.Count} Unresponsive Servers" );

				foreach ( var server in list.Responsive )
				{
					Assert.AreEqual( server.Map.ToLower(), "de_dust" );

					Console.WriteLine( $"[{server.Map}] - {server.Name}" );
				}
			}
		}

		[TestMethod]
		public async Task ServerListIps()
		{
			var ips = new string[]
			{
				"31.186.251.76",
				"31.186.251.76",
				"31.186.251.76",
				"31.186.251.76",
				"31.186.251.76",
				"74.91.119.142",
				"74.91.119.142",
				"74.91.119.142",
				"74.91.119.142",
				"74.91.119.142",
				"74.91.119.142",
				"74.91.119.142",
				"74.91.119.142",
				"74.91.119.142",
				"74.91.119.142",
				"74.91.119.142",
				"74.91.119.142",
				"139.99.144.70",
				"139.99.144.70",
				"139.99.144.70",
				"139.99.144.70",
				"139.99.144.70",
				"74.91.119.142",
				"74.91.119.142",
				"74.91.119.142",
				"74.91.119.142",
				"95.172.92.176",
				"95.172.92.176",
				"95.172.92.176",
				"95.172.92.176",
				"95.172.92.176",
				"164.132.205.154",
				"164.132.205.154",
				"164.132.205.154",
				"164.132.205.154",
				"164.132.205.154",
			};

			using ( var list = new ServerList.IpList( ips ) )
			{
				var success = await list.RunQueryAsync();

				Console.WriteLine( $"success {success}" );
				Console.WriteLine( $"Found {list.Responsive.Count} Responsive Servers" );
				Console.WriteLine( $"Found {list.Unresponsive.Count} Unresponsive Servers" );

				Assert.AreNotEqual( list.Responsive.Count, 0 );

				foreach ( var server in list.Responsive )
				{
					Console.WriteLine( $"[{server.Address}:{server.ConnectionPort}] - {server.Name}" );
				}
			}
		}
	}
}
