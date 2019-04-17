using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Steamworks.Data
{
	public struct UgcQuery
	{
		UGCMatchingUGCType matching;

		UserUGCList userGc;
		UGCQuery queryType;

		AppId consumerApp;
		AppId creatorApp;

		List<string> requiredTags;
		bool? matchAnyTag;

		List<string> excludedTags;

		Dictionary<string, string> requiredKv;

		bool? WantsReturnOnlyIDs;
		bool? WantsReturnKeyValueTags;
		bool? WantsReturnLongDescription;
		bool? WantsReturnMetadata;
		bool? WantsReturnChildren;
		bool? WantsReturnAdditionalPreviews;
		bool? WantsReturnTotalOnly;
		bool? WantsReturnPlaytimeStats;

		string searchText;
		string language;
		int? trendDays;
		int? maxCacheAge;

		public static UgcQuery Items() => new UgcQuery { matching = UGCMatchingUGCType.Items };
		public static UgcQuery ItemsMtx() => new UgcQuery { matching = UGCMatchingUGCType.Items_Mtx };
		public static UgcQuery ItemsReadyToUse() => new UgcQuery { matching = UGCMatchingUGCType.Items_ReadyToUse };
		public static UgcQuery Collections() => new UgcQuery { matching = UGCMatchingUGCType.Collections };
		public static UgcQuery Artwork() => new UgcQuery { matching = UGCMatchingUGCType.Artwork };
		public static UgcQuery Videos() => new UgcQuery { matching = UGCMatchingUGCType.Videos };
		public static UgcQuery Screenshots() => new UgcQuery { matching = UGCMatchingUGCType.Screenshots };
		public static UgcQuery AllGuides() => new UgcQuery { matching = UGCMatchingUGCType.AllGuides };
		public static UgcQuery WebGuides() => new UgcQuery { matching = UGCMatchingUGCType.WebGuides };
		public static UgcQuery IntegratedGuides() => new UgcQuery { matching = UGCMatchingUGCType.IntegratedGuides };
		public static UgcQuery UsableInGame() => new UgcQuery { matching = UGCMatchingUGCType.UsableInGame };
		public static UgcQuery ControllerBindings() => new UgcQuery { matching = UGCMatchingUGCType.ControllerBindings };
		public static UgcQuery GameManagedItems() => new UgcQuery { matching = UGCMatchingUGCType.GameManagedItems };
		public static UgcQuery All() => new UgcQuery { matching = UGCMatchingUGCType.All };

		public UgcQuery RankedByVote() { queryType = UGCQuery.RankedByVote; return this; }
		public UgcQuery RankedByPublicationDate() { queryType = UGCQuery.RankedByPublicationDate; return this; }
		public UgcQuery RankedByAcceptanceDate() { queryType = UGCQuery.AcceptedForGameRankedByAcceptanceDate; return this; }
		public UgcQuery RankedByTrend() { queryType = UGCQuery.RankedByTrend; return this; }
		public UgcQuery FavoritedByFriends() { queryType = UGCQuery.FavoritedByFriendsRankedByPublicationDate; return this; }
		public UgcQuery CreatedByFriends() { queryType = UGCQuery.CreatedByFriendsRankedByPublicationDate; return this; }
		public UgcQuery RankedByNumTimesReported() { queryType = UGCQuery.RankedByNumTimesReported; return this; }
		public UgcQuery CreatedByFollowedUsers() { queryType = UGCQuery.CreatedByFollowedUsersRankedByPublicationDate; return this; }
		public UgcQuery NotYetRated() { queryType = UGCQuery.NotYetRated; return this; }
		public UgcQuery RankedByTotalVotesAsc() { queryType = UGCQuery.RankedByTotalVotesAsc; return this; }
		public UgcQuery RankedByVotesUp() { queryType = UGCQuery.RankedByVotesUp; return this; }
		public UgcQuery RankedByTextSearch() { queryType = UGCQuery.RankedByTextSearch; return this; }
		public UgcQuery RankedByTotalUniqueSubscriptions() { queryType = UGCQuery.RankedByTotalUniqueSubscriptions; return this; }
		public UgcQuery RankedByPlaytimeTrend() { queryType = UGCQuery.RankedByPlaytimeTrend; return this; }
		public UgcQuery RankedByTotalPlaytime() { queryType = UGCQuery.RankedByTotalPlaytime; return this; }
		public UgcQuery RankedByAveragePlaytimeTrend() { queryType = UGCQuery.RankedByAveragePlaytimeTrend; return this; }
		public UgcQuery RankedByLifetimeAveragePlaytime() { queryType = UGCQuery.RankedByLifetimeAveragePlaytime; return this; }
		public UgcQuery RankedByPlaytimeSessionsTrend() { queryType = UGCQuery.RankedByPlaytimeSessionsTrend; return this; }
		public UgcQuery RankedByLifetimePlaytimeSessions() { queryType = UGCQuery.RankedByLifetimePlaytimeSessions; return this; }

		public UgcQuery ReturnOnlyIDs( bool b) { WantsReturnOnlyIDs = b; return this; }
		public UgcQuery ReturnKeyValueTag( bool b ) { WantsReturnKeyValueTags = b; return this; }
		public UgcQuery ReturnLongDescription( bool b ) { WantsReturnLongDescription = b; return this; }
		public UgcQuery ReturnMetadata( bool b ) { WantsReturnMetadata = b; return this; }
		public UgcQuery ReturnChildren( bool b ) { WantsReturnChildren = b; return this; }
		public UgcQuery ReturnAdditionalPreviews( bool b ) { WantsReturnAdditionalPreviews = b; return this; }
		public UgcQuery ReturnTotalOnly( bool b ) { WantsReturnTotalOnly = b; return this; }
		public UgcQuery ReturnPlaytimeStats( bool b ) { WantsReturnPlaytimeStats = b; return this; }
		public UgcQuery AllowCachedResponse( int maxSecondsAge ) { maxCacheAge = maxSecondsAge; return this; }

		public UgcQuery InLanguage( string lang ) { language = lang; return this; }
		public UgcQuery MatchAnyTag( bool b ) { matchAnyTag = b; return this; }

		public UgcQuery WithTag( string tag )
		{
			if ( requiredTags == null ) requiredTags = new List<string>();
			requiredTags.Add( tag );
			return this;
		}

		public UgcQuery WithoutTag( string tag )
		{
			if ( excludedTags == null ) excludedTags = new List<string>();
			excludedTags.Add( tag );
			return this;
		}

		public async Task<UgcQueryPage?> GetPageAsync( int page )
		{
			if ( page <= 0 ) throw new System.Exception( "page should be > 0" );

			if ( consumerApp == 0 ) consumerApp = SteamClient.AppId;
			if ( creatorApp == 0 ) creatorApp = consumerApp;

			UGCQueryHandle_t handle;

			handle = SteamUGC.Internal.CreateQueryAllUGCRequest1( queryType, matching, creatorApp.Value, consumerApp.Value, (uint) page );

			// Apply stored constraints
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

				//
				// TODO - add more
				//
			}

			var result = await SteamUGC.Internal.SendQueryUGCRequest( handle );
			if ( !result.HasValue )
				return null;

			if ( result.Value.Result != Result.OK )
				return null;

			return new UgcQueryPage
			{
				Handle = result.Value.Handle,
				ResultCount = (int) result.Value.NumResultsReturned,
				TotalCount = (int)result.Value.TotalMatchingResults,
				CachedData = result.Value.CachedData
			};
		}

	}

	public struct UgcQueryPage : System.IDisposable
	{
		internal UGCQueryHandle_t Handle;

		public int ResultCount;
		public int TotalCount;

		public bool CachedData;

		public IEnumerable<UgcDetails> Entries
		{
			get
			{
				var details = default( SteamUGCDetails_t );
				for ( uint i=0; i< ResultCount; i++ )
				{
					if ( SteamUGC.Internal.GetQueryUGCResult( Handle, i, ref details ) )
					{
						yield return UgcDetails.From( details, Handle );
					}
				}
			}
		}


		public void Dispose()
		{
			if ( Handle > 0 )
			{
				SteamUGC.Internal.ReleaseQueryUGCRequest( Handle );
				Handle = 0;
			}
		}
	}

	public struct UgcDetails
	{
		public PublishedFileId Id;

		public string Title;
		public string Description;


		//
		// TODO;
		//
		internal Result Result; // m_eResult enum EResult
		internal WorkshopFileType FileType; // m_eFileType enum EWorkshopFileType
		internal uint CreatorAppID; // m_nCreatorAppID AppId_t
		internal uint ConsumerAppID; // m_nConsumerAppID AppId_t

		internal ulong SteamIDOwner; // m_ulSteamIDOwner uint64
		internal uint TimeCreated; // m_rtimeCreated uint32
		internal uint TimeUpdated; // m_rtimeUpdated uint32
		internal uint TimeAddedToUserList; // m_rtimeAddedToUserList uint32
		internal RemoteStoragePublishedFileVisibility Visibility; // m_eVisibility enum ERemoteStoragePublishedFileVisibility
		internal bool Banned; // m_bBanned _Bool
		internal bool AcceptedForUse; // m_bAcceptedForUse _Bool
		internal bool TagsTruncated; // m_bTagsTruncated _Bool
		internal string Tags; // m_rgchTags char [1025]
		internal ulong File; // m_hFile UGCHandle_t
		internal ulong PreviewFile; // m_hPreviewFile UGCHandle_t
		internal string PchFileName; // m_pchFileName char [260]
		internal int FileSize; // m_nFileSize int32
		internal int PreviewFileSize; // m_nPreviewFileSize int32
		internal string URL; // m_rgchURL char [256]
		internal uint VotesUp; // m_unVotesUp uint32
		internal uint VotesDown; // m_unVotesDown uint32
		internal float Score; // m_flScore float
		internal uint NumChildren; // m_unNumChildren uint32

		internal static UgcDetails From( SteamUGCDetails_t details, UGCQueryHandle_t handle )
		{
			var d = new UgcDetails
			{
				Id = details.PublishedFileId,
				FileType = details.FileType,

				Title = details.Title,
				Description = details.Description,

			};

			return d;
		}
	}
}