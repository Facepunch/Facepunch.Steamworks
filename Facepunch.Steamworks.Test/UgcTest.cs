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
    public class UgcTests
    {
		[TestMethod]
        public async Task QueryAll()
        {
			var q = UgcQuery.All();

			var result = await q.GetPageAsync( 1 );
			Assert.IsNotNull( result );

			Console.WriteLine( $"ResultCount: {result?.ResultCount}" );
			Console.WriteLine( $"TotalCount: {result?.TotalCount}" );
		}

		[TestMethod]
		public async Task QueryAllFromFriends()
		{
			var q = UgcQuery.All()
						.CreatedByFriends();

			var result = await q.GetPageAsync( 1 );
			Assert.IsNotNull( result );

			Console.WriteLine( $"ResultCount: {result?.ResultCount}" );
			Console.WriteLine( $"TotalCount: {result?.TotalCount}" );

			foreach ( var entry in result.Value.Entries )
			{
				Console.WriteLine( $" {entry.Title}" );
			}
		}
	}

}
