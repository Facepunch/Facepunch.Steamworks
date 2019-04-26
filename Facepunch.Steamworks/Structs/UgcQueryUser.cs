using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Steamworks.Data;
using QueryType = Steamworks.Ugc.UserQuery;

namespace Steamworks.Ugc
{
	public struct UserQuery
	{
		public UserQuery( UgcType type, SteamId steamid = default ) : this()
		{
			if ( steamid == 0 )
				steamid = SteamClient.SteamId;

			this.steamid = steamid;
			this.matchingType = type;
		}

		SteamId steamid;

		UserUGCList userType;
		UserUGCListSortOrder userSort;
		UgcType matchingType;

		AppId consumerApp;
		AppId creatorApp;

		public static UserQuery All => new UserQuery( UgcType.All, 0 );
		public static UserQuery Items => new UserQuery( UgcType.Items, 0 );
		/*
		public static UserQuery ItemsMtx( SteamId steamid = default ) => new UserQuery( steamid ) { matchingType = UGCMatchingUGCType.Items_Mtx };
		public static UserQuery ItemsReadyToUse( SteamId steamid = default ) => new UserQuery( steamid ) { matchingType = UGCMatchingUGCType.Items_ReadyToUse };
		public static UserQuery Collections( SteamId steamid = default ) => new UserQuery( steamid ) { matchingType = UGCMatchingUGCType.Collections };
		public static UserQuery Artwork( SteamId steamid = default ) => new UserQuery( steamid ) { matchingType = UGCMatchingUGCType.Artwork };
		public static UserQuery Videos( SteamId steamid = default ) => new UserQuery( steamid ) { matchingType = UGCMatchingUGCType.Videos };
		public static UserQuery Screenshots( SteamId steamid = default ) => new UserQuery( steamid ) { matchingType = UGCMatchingUGCType.Screenshots };
		public static UserQuery AllGuides( SteamId steamid = default ) => new UserQuery( steamid ) { matchingType = UGCMatchingUGCType.AllGuides };
		public static UserQuery WebGuides( SteamId steamid = default ) => new UserQuery( steamid ) { matchingType = UGCMatchingUGCType.WebGuides };
		public static UserQuery IntegratedGuides( SteamId steamid = default ) => new UserQuery( steamid ) { matchingType = UGCMatchingUGCType.IntegratedGuides };
		public static UserQuery UsableInGame( SteamId steamid = default ) => new UserQuery( steamid ) { matchingType = UGCMatchingUGCType.UsableInGame };
		public static UserQuery ControllerBindings( SteamId steamid = default ) => new UserQuery( steamid ) { matchingType = UGCMatchingUGCType.ControllerBindings };
		public static UserQuery GameManagedItems( SteamId steamid = default ) => new UserQuery( steamid ) { matchingType = UGCMatchingUGCType.GameManagedItems };
		*/

		public UserQuery SortByCreationDate() { userSort = UserUGCListSortOrder.CreationOrderDesc; return this; }
		public UserQuery SortByCreationDateAsc() { userSort = UserUGCListSortOrder.CreationOrderAsc; return this; }
		public UserQuery SortByTitleAsc() { userSort = UserUGCListSortOrder.TitleAsc; return this; }
		public UserQuery SortByUpdateDate() { userSort = UserUGCListSortOrder.LastUpdatedDesc; return this; }
		public UserQuery SortBySubscriptionDate() { userSort = UserUGCListSortOrder.SubscriptionDateDesc; return this; }
		public UserQuery SortByVoteScore() { userSort = UserUGCListSortOrder.VoteScoreDesc; return this; }
		public UserQuery SortByModeration() { userSort = UserUGCListSortOrder.ForModeration; return this; }


		public UserQuery GetPublished() { userType = UserUGCList.Published; return this; }

		public UserQuery FromUser( SteamId steamid )
		{
			this.steamid = steamid;
			return this;
		}

		public UserQuery FromSelf()
		{
			this.steamid = SteamClient.SteamId;
			return this;
		}


		public async Task<ResultPage?> GetPageAsync( int page )
		{
			if ( page <= 0 ) throw new System.Exception( "page should be > 0" );

			if ( consumerApp == 0 ) consumerApp = SteamClient.AppId;
			if ( creatorApp == 0 ) creatorApp = consumerApp;

			UGCQueryHandle_t handle;

			handle = SteamUGC.Internal.CreateQueryUserUGCRequest( steamid.AccountId, userType, matchingType, userSort, creatorApp.Value, consumerApp.Value, (uint)page );

			ApplyConstraints( handle );

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
		public QueryType WithType( UgcType type ) { matchingType = type; return this; }
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

		List<string> requiredTags;
		bool? matchAnyTag;
		List<string> excludedTags;
		Dictionary<string, string> requiredKv;

		/// <summary>
		/// Found items must have at least one of the defined tags
		/// </summary>
		public QueryType MatchAnyTag() { matchAnyTag = true; return this; }

		/// <summary>
		/// Found items must have all defined tags
		/// </summary>
		public QueryType MatchAllTags() { matchAnyTag = false; return this; }

		public QueryType WithTag( string tag )
		{
			if ( requiredTags == null ) requiredTags = new List<string>();
			requiredTags.Add( tag );
			return this;
		}

		public QueryType WithoutTag( string tag )
		{
			if ( excludedTags == null ) excludedTags = new List<string>();
			excludedTags.Add( tag );
			return this;
		}

		void ApplyConstraints( UGCQueryHandle_t handle )
		{
			if ( requiredTags != null )
			{
				foreach ( var tag in requiredTags )
					SteamUGC.Internal.AddRequiredTag( handle, tag );
			}

			if ( excludedTags != null )
			{
				foreach ( var tag in excludedTags )
					SteamUGC.Internal.AddExcludedTag( handle, tag );
			}

			if ( requiredKv != null )
			{
				foreach ( var tag in requiredKv )
					SteamUGC.Internal.AddRequiredKeyValueTag( handle, tag.Key, tag.Value );
			}

			if ( matchAnyTag.HasValue )
			{
				SteamUGC.Internal.SetMatchAnyTag( handle, matchAnyTag.Value );
			}
		}

		#endregion

	}
}