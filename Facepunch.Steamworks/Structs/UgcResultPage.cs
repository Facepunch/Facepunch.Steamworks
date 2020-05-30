using System.Collections.Generic;
using Steamworks.Data;

namespace Steamworks.Ugc
{
	public struct ResultPage : System.IDisposable
	{
		internal UGCQueryHandle_t Handle;

		public int ResultCount;
		public int TotalCount;

		public bool CachedData;

		internal bool ReturnsKeyValueTags;
		internal bool ReturnsDefaultStats;
		internal bool ReturnsMetadata;

		public IEnumerable<Item> Entries
		{
			get
			{

				var details = default( SteamUGCDetails_t );
				for ( uint i=0; i< ResultCount; i++ )
				{
					if ( SteamUGC.Internal.GetQueryUGCResult( Handle, i, ref details ) )
					{
						var item = Item.From( details );


						if ( ReturnsDefaultStats )
						{
							item.NumSubscriptions = GetStat( i, ItemStatistic.NumSubscriptions );
							item.NumFavorites = GetStat( i, ItemStatistic.NumFavorites );
							item.NumFollowers = GetStat( i, ItemStatistic.NumFollowers );
							item.NumUniqueSubscriptions = GetStat( i, ItemStatistic.NumUniqueSubscriptions );
							item.NumUniqueFavorites = GetStat( i, ItemStatistic.NumUniqueFavorites );
							item.NumUniqueFollowers = GetStat( i, ItemStatistic.NumUniqueFollowers );
							item.NumUniqueWebsiteViews = GetStat( i, ItemStatistic.NumUniqueWebsiteViews );
							item.ReportScore = GetStat( i, ItemStatistic.ReportScore );
							item.NumSecondsPlayed = GetStat( i, ItemStatistic.NumSecondsPlayed );
							item.NumPlaytimeSessions = GetStat( i, ItemStatistic.NumPlaytimeSessions );
							item.NumComments = GetStat( i, ItemStatistic.NumComments );
							item.NumSecondsPlayedDuringTimePeriod = GetStat( i, ItemStatistic.NumSecondsPlayedDuringTimePeriod );
							item.NumPlaytimeSessionsDuringTimePeriod = GetStat( i, ItemStatistic.NumPlaytimeSessionsDuringTimePeriod );
						}

						if ( SteamUGC.Internal.GetQueryUGCPreviewURL( Handle, i, out string preview ) )
						{
							item.PreviewImageUrl = preview;
						}

						if ( ReturnsKeyValueTags )
						{
							var keyValueTagsCount = SteamUGC.Internal.GetQueryUGCNumKeyValueTags( Handle, i );

							item.KeyValueTags = new Dictionary<string, string>( (int)keyValueTagsCount );
							for ( uint j = 0; j < keyValueTagsCount; j++ )
							{
								string key, value;
								if ( SteamUGC.Internal.GetQueryUGCKeyValueTag( Handle, i, j, out key, out value ) )
									item.KeyValueTags[key] = value;
							}
						}

						if (ReturnsMetadata)
						{
							string metadata;
							if (SteamUGC.Internal.GetQueryUGCMetadata(Handle, i, out metadata))
							{
								item.Metadata = metadata;
							}
						}

						// TODO GetQueryUGCAdditionalPreview
						// TODO GetQueryUGCChildren

						yield return item;
					}
				}
			}
		}

		private ulong GetStat( uint index, ItemStatistic stat )
		{
			ulong val = 0;

			if ( !SteamUGC.Internal.GetQueryUGCStatistic( Handle, index, stat, ref val ) )
				return 0;

			return val;
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
}