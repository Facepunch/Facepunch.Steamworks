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
							.WithTag( "Version3" )
							.WithTag( "Hunting Bow" )
							.MatchAllTags();

			var result = await q.GetPageAsync( 1 );
			Assert.IsNotNull( result );
			Assert.IsTrue( result?.ResultCount > 0 );

			Console.WriteLine( $"ResultCount: {result?.ResultCount}" );
			Console.WriteLine( $"TotalCount: {result?.TotalCount}" );

			foreach ( var entry in result.Value.Entries )
			{
				Assert.IsTrue( entry.HasTag( "Version3" ), "Has Tag Version3" );
				Assert.IsTrue( entry.HasTag( "Hunting Bow" ), "Has Tag HuntingBow" );

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
		public async Task QueryGarry()
		{
			var q = Ugc.UserQuery.All
								.FromUser( 76561197960279927 );

			var result = await q.GetPageAsync( 1 );
			Assert.IsNotNull( result );
			Assert.IsTrue( result?.ResultCount > 0 );

			Console.WriteLine( $"ResultCount: {result?.ResultCount}" );
			Console.WriteLine( $"TotalCount: {result?.TotalCount}" );

			foreach ( var entry in result.Value.Entries )
			{
				Console.WriteLine( $" {entry.Title}" );
			}
		}

		[TestMethod]
		public async Task QuerySpecificFile()
		{
			var item = await SteamUGC.QueryFileAsync( 1734427277 );

			Assert.IsTrue( item.HasValue );
			Assert.IsNotNull( item.Value.Title );

			Console.WriteLine( $"Title: {item?.Title}" );
			Console.WriteLine( $"Desc: {item?.Description}" );
			Console.WriteLine( $"Tags: {string.Join( ",", item?.Tags )}" );
			Console.WriteLine( $"Author: {item?.Owner.Name} [{item?.Owner.Id}]" );
			Console.WriteLine( $"PreviewImageUrl: {item?.PreviewImageUrl}" );
			Console.WriteLine( $"NumComments: {item?.NumComments}" );
			Console.WriteLine( $"Url: {item?.Url}" );
			Console.WriteLine( $"Directory: {item?.Directory}" );
			Console.WriteLine( $"IsInstalled: {item?.IsInstalled}" );
			Console.WriteLine( $"IsAcceptedForUse: {item?.IsAcceptedForUse}" );
			Console.WriteLine( $"IsPublic: {item?.IsPublic}" );
			Console.WriteLine( $"Created: {item?.Created}" );
			Console.WriteLine( $"Updated: {item?.Updated}" );
			Console.WriteLine( $"Score: {item?.Score}" );
		}
	}

}
