using System.Collections.Generic;
using Steamworks.Data;

namespace Steamworks.Ugc
{
	/// <summary>
	/// One page of results from a <see cref="Query"/>, and the native query handle that owns them.
	/// A page holds at most 50 items &#8212; Valve fixes that at <c>kNumUGCResultsPerPage</c>.
	/// </summary>
	/// <remarks>
	/// <b>You must dispose this.</b> The underlying native query is only released by
	/// <see cref="Dispose"/>; there is no finalizer, so a page you drop on the floor leaks its handle
	/// and its results for the lifetime of the process. Prefer a <c>using</c> block.
	/// <para>
	/// The item data is read out of the native query lazily, while you enumerate
	/// <see cref="Entries"/>. That means enumerating after disposal yields nothing rather than
	/// throwing, and it means you should materialise anything you want to keep (with <c>ToList()</c>,
	/// or by copying the fields you need) <i>before</i> the <c>using</c> block ends.
	/// </para>
	/// </remarks>
	/// <example>
	/// <code>
	/// var page = await Ugc.Query.Items.RankedByVote().GetPageAsync( 1 );
	/// if ( page.HasValue )
	/// {
	///     using ( var results = page.Value )
	///     {
	///         // materialise inside the using block - Entries is lazy
	///         var titles = results.Entries.Select( x =&gt; x.Title ).ToList();
	///     }
	/// }
	/// </code>
	/// </example>
	public struct ResultPage : System.IDisposable
	{
		internal UGCQueryHandle_t Handle;

		/// <summary>
		/// How many items are actually in <b>this page</b>, which is at most 50 and is lower on the
		/// last page. This is not the size of the whole result set &#8212; that is
		/// <see cref="TotalCount"/>. Zero here means the query succeeded and matched nothing on this
		/// page, which is not an error.
		/// </summary>
		public int ResultCount;

		/// <summary>
		/// How many items matched the query in total, across every page. Divide by 50 (rounding up) to
		/// work out how many pages exist. This is the number to show a player as "1,284 results".
		/// </summary>
		/// <remarks>
		/// Valve does not document whether this is exact or an estimate for very large result sets.
		/// </remarks>
		public int TotalCount;

		/// <summary>
		/// Whether Steam answered this page from its cache instead of querying the Workshop backend.
		/// Only ever <see langword="true"/> if you asked for caching with
		/// <see cref="Query.AllowCachedResponse"/>.
		/// </summary>
		/// <remarks>
		/// Cached pages carry stale vote and subscription counts, so do not use one to confirm that a
		/// write you just made has landed.
		/// </remarks>
		public bool CachedData;

		internal bool ReturnsKeyValueTags;
		internal bool ReturnsDefaultStats;
		internal bool ReturnsMetadata;
		internal bool ReturnsChildren;
		internal bool ReturnsAdditionalPreviews;

		/// <summary>
		/// The items in this page, read out of the native query one at a time as you enumerate.
		/// This is where a <see cref="Query"/> finally turns into something you can show a player.
		/// </summary>
		/// <remarks>
		/// <para>
		/// <b>Lazy and re-entrant.</b> Nothing is read until you enumerate, and enumerating twice does
		/// the work twice. Enumerating after <see cref="Dispose"/> silently yields nothing rather than
		/// throwing, so materialise what you need before disposing.
		/// </para>
		/// <para>
		/// Which fields are populated depends on the switches set on the query that produced this
		/// page: <see cref="Query.WithKeyValueTags"/>, <see cref="Query.WithMetadata"/>,
		/// <see cref="Query.WithChildren"/>, <see cref="Query.WithAdditionalPreviews"/> and
		/// <see cref="Query.WithDefaultStats"/>. Fields you did not ask for are left null or zero,
		/// which is indistinguishable from the item genuinely having none.
		/// </para>
		/// <para>
		/// Items that Steam fails to read are skipped silently, so the number of values you get back
		/// can be lower than <see cref="ResultCount"/>.
		/// </para>
		/// </remarks>
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

						uint numChildren = item.details.NumChildren;
						if ( ReturnsChildren && numChildren > 0 )
						{
							var children = new PublishedFileId[numChildren];
							if ( SteamUGC.Internal.GetQueryUGCChildren( Handle, i, children, numChildren ) )
							{
								item.Children = children;
							}
						}

						if ( ReturnsAdditionalPreviews )
						{
							var previewsCount = SteamUGC.Internal.GetQueryUGCNumAdditionalPreviews( Handle, i );
							if ( previewsCount > 0 )
							{
								item.AdditionalPreviews = new UgcAdditionalPreview[previewsCount];
								for ( uint j = 0; j < previewsCount; j++ )
								{
									string previewUrlOrVideo;
									string originalFileName; //what is this???
									ItemPreviewType previewType = default;
									if ( SteamUGC.Internal.GetQueryUGCAdditionalPreview(
										Handle, i, j, out previewUrlOrVideo, out originalFileName, ref previewType ) )
									{
										item.AdditionalPreviews[j] = new UgcAdditionalPreview( 
											previewUrlOrVideo, originalFileName, previewType );
									}
								}
							}
						}

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

		/// <summary>
		/// Release the native query and the results it holds. Mandatory &#8212; there is no finalizer
		/// backing this up, so a page that is never disposed leaks until the process exits.
		/// </summary>
		/// <remarks>
		/// Safe to call more than once; the handle is zeroed on the first call. After disposal
		/// <see cref="Entries"/> yields nothing instead of throwing.
		/// </remarks>
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