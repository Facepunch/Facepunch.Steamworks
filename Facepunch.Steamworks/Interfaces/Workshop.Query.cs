using System;
using System.Collections.Generic;
using System.Linq;

namespace Facepunch.Steamworks
{
    public partial class Workshop
    {
        public class Query : IDisposable
        {
            internal const int SteamResponseSize = 50;

            internal SteamNative.UGCQueryHandle_t Handle;
            internal SteamNative.CallbackHandle Callback;

            /// <summary>
            /// The AppId you're querying. This defaults to this appid.
            /// </summary>
            public uint AppId { get; set; }

            /// <summary>
            /// The AppId of the app used to upload the item. This defaults to 0
            /// which means all/any. 
            /// </summary>
            public uint UploaderAppId { get; set; }

            public QueryType QueryType { get; set; } = QueryType.Items;
            public Order Order { get; set; } = Order.RankedByVote;

            public string SearchText { get; set; }

            public Item[] Items { get; set; }

            public int TotalResults { get; set; }

            public ulong? UserId { get; set; }

            /// <summary>
            /// If order is RankedByTrend, this value represents how many days to take
            /// into account.
            /// </summary>
            public int RankedByTrendDays { get; set; }

            public UserQueryType UserQueryType { get; set; } = UserQueryType.Published;

            /// <summary>
            /// Called when the query finishes
            /// </summary>
            public Action<Query> OnResult;

            /// <summary>
            /// Page starts at 1 !!
            /// </summary>
            public int Page { get; set; } = 1;

            public int PerPage { get; set; } = SteamResponseSize;

            internal Workshop workshop;

            private int _resultPage = 0;
            private int _resultsRemain = 0;
            private int _resultSkip = 0;
            private List<Item> _results;

            public  void Run()
            {
                if ( Callback != null )
                    return;

                if ( Page <= 0 )
                    throw new System.Exception( "Page should be 1 or above" );

                var actualOffset = ((Page-1) * PerPage);

                TotalResults = 0;

                _resultSkip = actualOffset % SteamResponseSize;
                _resultsRemain = PerPage;
                _resultPage = (int) Math.Floor( (float) actualOffset / (float)SteamResponseSize );
                _results = new List<Item>();

                RunInternal();
            }

            unsafe void RunInternal()
            {
                if ( FileId.Count != 0 )
                {
                    var fileArray = FileId.Select( x => (SteamNative.PublishedFileId_t)x ).ToArray();
                    _resultsRemain = fileArray.Length;

                    Handle = workshop.ugc.CreateQueryUGCDetailsRequest( fileArray );
                }
                else if ( UserId.HasValue )
                {
                    uint accountId = (uint)( UserId.Value & 0xFFFFFFFFul );
                    Handle = workshop.ugc.CreateQueryUserUGCRequest( accountId, (SteamNative.UserUGCList)( int)UserQueryType, (SteamNative.UGCMatchingUGCType)( int)QueryType, SteamNative.UserUGCListSortOrder.LastUpdatedDesc, UploaderAppId, AppId, (uint)_resultPage + 1 );
                }
                else
                {
                    Handle = workshop.ugc.CreateQueryAllUGCRequest( (SteamNative.UGCQuery)(int)Order, (SteamNative.UGCMatchingUGCType)(int)QueryType, UploaderAppId, AppId, (uint)_resultPage + 1 );
                }

                if ( !string.IsNullOrEmpty( SearchText ) )
                    workshop.ugc.SetSearchText( Handle, SearchText );

                foreach ( var tag in RequireTags )
                    workshop.ugc.AddRequiredTag( Handle, tag );

                if ( RequireTags.Count > 0 )
                    workshop.ugc.SetMatchAnyTag( Handle, !RequireAllTags );

                if ( RankedByTrendDays > 0 )
                    workshop.ugc.SetRankedByTrendDays( Handle, (uint) RankedByTrendDays );

                foreach ( var tag in ExcludeTags )
                    workshop.ugc.AddExcludedTag( Handle, tag );

                Callback = workshop.ugc.SendQueryUGCRequest( Handle, ResultCallback );
            }

            void ResultCallback( SteamNative.SteamUGCQueryCompleted_t data, bool bFailed )
            {
                if ( bFailed )
                    throw new System.Exception( "bFailed!" );

                var gotFiles = 0;
                for ( int i = 0; i < data.NumResultsReturned; i++ )
                {
                    if ( _resultSkip > 0 )
                    {
                        _resultSkip--;
                        continue;
                    }

                    SteamNative.SteamUGCDetails_t details = new SteamNative.SteamUGCDetails_t();
                    if ( !workshop.ugc.GetQueryUGCResult( data.Handle, (uint)i, ref details ) )
                        continue;

                    // We already have this file, so skip it
                    if ( _results.Any( x => x.Id == details.PublishedFileId ) )
                        continue;

                    var item = Item.From( details, workshop );

                    item.SubscriptionCount = GetStat( data.Handle, i, ItemStatistic.NumSubscriptions );
                    item.FavouriteCount = GetStat( data.Handle, i, ItemStatistic.NumFavorites );
                    item.FollowerCount = GetStat( data.Handle, i, ItemStatistic.NumFollowers );
                    item.WebsiteViews = GetStat( data.Handle, i, ItemStatistic.NumUniqueWebsiteViews );
                    item.ReportScore = GetStat( data.Handle, i, ItemStatistic.ReportScore );

                    string url = null;
                    if ( workshop.ugc.GetQueryUGCPreviewURL( data.Handle, (uint)i, out url ) )
                        item.PreviewImageUrl = url;

                    _results.Add( item );

                    _resultsRemain--;
                    gotFiles++;

                    if ( _resultsRemain <= 0 )
                        break;
                }

                TotalResults = TotalResults > data.TotalMatchingResults ? TotalResults : (int)data.TotalMatchingResults;

                Callback.Dispose();
                Callback = null;

                _resultPage++;

                if ( _resultsRemain > 0 && gotFiles > 0 )
                {
                    RunInternal();
                }
                else
                {
                    Items = _results.ToArray();

                    if ( OnResult != null )
                    {
                        OnResult( this );
                    }
                }
            }

            private int GetStat( ulong handle, int index, ItemStatistic stat )
            {
                ulong val = 0;
                if ( !workshop.ugc.GetQueryUGCStatistic( handle, (uint)index, (SteamNative.ItemStatistic)(uint)stat, out val ) )
                    return 0;

                return (int) val;
            }

            public bool IsRunning
            {
                get { return Callback != null; }
            }

            /// <summary>
            /// Only return items with these tags
            /// </summary>
            public List<string> RequireTags { get; set; } = new List<string>();

            /// <summary>
            /// If true, return items that have all RequireTags
            /// If false, return items that have any tags in RequireTags
            /// </summary>
            public bool RequireAllTags { get; set; } = false;

            /// <summary>
            /// Don't return any items with this tag
            /// </summary>
            public List<string> ExcludeTags { get; set; } = new List<string>();

            /// <summary>
            /// If you're querying for a particular file or files, add them to this.
            /// </summary>
            public List<ulong> FileId { get; set; } = new List<ulong>();



            /// <summary>
            /// Don't call this in production!
            /// </summary>
            public void Block()
            {
                const int sleepMs = 10;

                workshop.steamworks.Update();

                while ( IsRunning )
                {
#if NET_CORE
                    System.Threading.Tasks.Task.Delay( sleepMs ).Wait();
#else
                    System.Threading.Thread.Sleep( sleepMs );
#endif
                    workshop.steamworks.Update();
                }
            }

            public void Dispose()
            {
                // ReleaseQueryUGCRequest
            }
        }

        private enum ItemStatistic : uint
        {
            NumSubscriptions = 0,
            NumFavorites = 1,
            NumFollowers = 2,
            NumUniqueSubscriptions = 3,
            NumUniqueFavorites = 4,
            NumUniqueFollowers = 5,
            NumUniqueWebsiteViews = 6,
            ReportScore = 7,
        };
    }
}
