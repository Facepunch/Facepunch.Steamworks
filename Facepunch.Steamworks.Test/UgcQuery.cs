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
    public class UgcQueryTests
    {
		[TestMethod]
        public async Task QueryAll()
        {
			var q = Ugc.Query.All;

			var result = await q.GetPageAsync( 1 );
			Assert.IsNotNull( result );

			Console.WriteLine( $"ResultCount: {result?.ResultCount}" );
			Console.WriteLine( $"TotalCount: {result?.TotalCount}" );
		}

		[TestMethod]
		public async Task QueryWithTags()
		{
			var q = Ugc.Query.All
							.WithTag( "Fun" )
							.WithTag( "Movie" )
							.MatchAllTags();

			var result = await q.GetPageAsync( 1 );
			Assert.IsNotNull( result );

			Console.WriteLine( $"ResultCount: {result?.ResultCount}" );
			Console.WriteLine( $"TotalCount: {result?.TotalCount}" );

			foreach ( var entry in result.Value.Entries )
			{
				Assert.IsTrue( entry.HasTag( "Fun" ) );
				Assert.IsTrue( entry.HasTag( "Movie" ) );

			}
		}

		[TestMethod]
		public async Task QueryAllFromFriends()
		{
			var q = Ugc.Query.All
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

		[TestMethod]
		public async Task QueryUserOwn()
		{
			var q = Ugc.UserQuery.All
								.FromSelf();

			var result = await q.GetPageAsync( 1 );
			Assert.IsNotNull( result );

			Console.WriteLine( $"ResultCount: {result?.ResultCount}" );
			Console.WriteLine( $"TotalCount: {result?.TotalCount}" );

			foreach ( var entry in result.Value.Entries )
			{
				Console.WriteLine( $" {entry.Title}" );
			}
		}

		[TestMethod]
		public async Task QueryFoohy()
		{
			var q = Ugc.UserQuery.All
								.FromUser( 76561197997689747 );

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
