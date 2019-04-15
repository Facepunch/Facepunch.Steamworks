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
    public partial class ServerListTest
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
        public async Task ServerListInternetInterupted()
        {
			using ( var list = new ServerListInternet() )
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
			using ( var list = new ServerListInternet() )
			{
				var success = await list.RunQueryAsync();

				Console.WriteLine( $"success {success}" );
				Console.WriteLine( $"Found {list.Responsive.Count} Responsive Servers" );
				Console.WriteLine( $"Found {list.Unresponsive.Count} Unresponsive Servers" );
			}
		}

		[TestMethod]
		public async Task ServerListLan()
		{
			using ( var list = new ServerListLan() )
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
			using ( var list = new ServerListFavourites() )
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
			using ( var list = new ServerListFriends() )
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
			using ( var list = new ServerListHistory() )
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
			using ( var list = new ServerListInternet() )
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
    }
}
