using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Steamworks.Data;
using QueryType = Steamworks.Ugc.FileQuery;

namespace Steamworks.Ugc
{
	public struct FileQuery
	{
		PublishedFileId[] Files;

		public FileQuery( params PublishedFileId[] files ) : this()
		{
			Files = files;
		}

		public async Task<ResultPage?> GetPageAsync( int page )
		{
			if ( page <= 0 ) throw new System.Exception( "page should be > 0" );
			if ( Files == null ) throw new System.Exception( "Files is null" );
			if ( Files.Length == 0 ) throw new System.Exception( "Files is empty" );

			UGCQueryHandle_t handle;

			handle = SteamUGC.Internal.CreateQueryUGCDetailsRequest( Files, (uint)Files.Length );

			var result = await SteamUGC.Internal.SendQueryUGCRequest( handle );
			if ( !result.HasValue )
				return null;

			if ( result.Value.Result != Steamworks.Result.OK )
				return null;

			return new ResultPage
			{
				Handle = result.Value.Handle,
				ResultCount = (int) result.Value.NumResultsReturned,
				TotalCount = (int)result.Value.TotalMatchingResults,
				CachedData = result.Value.CachedData
			};
		}

		#region SharedConstraints
		bool? WantsReturnOnlyIDs;
		public QueryType WithOnlyIDs( bool b ) { WantsReturnOnlyIDs = b; return this; }
		bool? WantsReturnKeyValueTags;
		public QueryType WithKeyValueTag( bool b ) { WantsReturnKeyValueTags = b; return this; }
		bool? WantsReturnLongDescription;
		public QueryType WithLongDescription( bool b ) { WantsReturnLongDescription = b; return this; }
		bool? WantsReturnMetadata;
		public QueryType WithMetadata( bool b ) { WantsReturnMetadata = b; return this; }
		bool? WantsReturnChildren;
		public QueryType WithChildren( bool b ) { WantsReturnChildren = b; return this; }
		bool? WantsReturnAdditionalPreviews;
		public QueryType WithAdditionalPreviews( bool b ) { WantsReturnAdditionalPreviews = b; return this; }
		bool? WantsReturnTotalOnly;
		public QueryType WithTotalOnly( bool b ) { WantsReturnTotalOnly = b; return this; }
		bool? WantsReturnPlaytimeStats;
		public QueryType WithPlaytimeStats( bool b ) { WantsReturnPlaytimeStats = b; return this; }
		int? maxCacheAge;
		public QueryType AllowCachedResponse( int maxSecondsAge ) { maxCacheAge = maxSecondsAge; return this; }
		string language;
		public QueryType InLanguage( string lang ) { language = lang; return this; }
		#endregion

	}
}