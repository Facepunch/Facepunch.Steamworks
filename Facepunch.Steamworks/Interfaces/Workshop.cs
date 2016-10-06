using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using Facepunch.Steamworks.Callbacks.Networking;
using Facepunch.Steamworks.Callbacks.Workshop;
using Facepunch.Steamworks.Interop;
using Valve.Steamworks;

namespace Facepunch.Steamworks
{
    public class Workshop
    {
        internal const ulong InvalidHandle = 0xffffffffffffffff;

        internal ISteamUGC ugc;
        internal BaseSteamworks steamworks;

        internal Workshop( BaseSteamworks sw, ISteamUGC ugc )
        {
            this.ugc = ugc;
            this.steamworks = sw;

           // sw.AddCallback<P2PSessionRequest>( onP2PConnectionRequest, P2PSessionRequest.CallbackId );

        }

        public enum Order
        {
            RankedByVote = 0,
            RankedByPublicationDate = 1,
            AcceptedForGameRankedByAcceptanceDate = 2,
            RankedByTrend = 3,
            FavoritedByFriendsRankedByPublicationDate = 4,
            CreatedByFriendsRankedByPublicationDate = 5,
            RankedByNumTimesReported = 6,
            CreatedByFollowedUsersRankedByPublicationDate = 7,
            NotYetRated = 8,
            RankedByTotalVotesAsc = 9,
            RankedByVotesUp = 10,
            RankedByTextSearch = 11,
            RankedByTotalUniqueSubscriptions = 12,
        };

        public enum QueryType
        {
            Items = 0,        // both mtx items and ready-to-use items
            Items_Mtx = 1,
            Items_ReadyToUse = 2,
            Collections = 3,
            Artwork = 4,
            Videos = 5,
            Screenshots = 6,
            AllGuides = 7,        // both web guides and integrated guides
            WebGuides = 8,
            IntegratedGuides = 9,
            UsableInGame = 10,        // ready-to-use items and integrated guides
            ControllerBindings = 11,
            GameManagedItems = 12,        // game managed items (not managed by users)
        };

        public WorkshopQuery CreateQuery()
        {
            var q = new WorkshopQuery();
            q.AppId = steamworks.AppId;
            q.workshop = this;
            return q;
        }

        public class WorkshopItem
        {
            public string Description { get; private set; }
            public ulong Id { get; private set; }
            public ulong OwnerId { get; private set; }
            public float Score { get; private set; }
            public string[] Tags { get; private set; }
            public string Title { get; private set; }
            public uint VotesDown { get; private set; }
            public uint VotesUp { get; private set; }

            internal static WorkshopItem From( SteamUGCDetails_t details )
            {
                var item = new WorkshopItem();

                item.Id = details.m_nPublishedFileId;
                item.Title = details.m_rgchTitle;
                item.Description = details.m_rgchDescription;
                item.OwnerId = details.m_ulSteamIDOwner;
                item.Tags = details.m_rgchTags.Split( ',' );
                item.Score = details.m_flScore;
                item.VotesUp = details.m_unVotesUp;
                item.VotesDown = details.m_unVotesDown;
    
                return item;
            }
        }

        public class WorkshopQuery : IDisposable
        {
            internal ulong Handle;
            internal QueryCompleted Callback;

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

            public WorkshopItem[] Items { get; set; }

            public int TotalResults { get; set; }

            /// <summary>
            /// Page starts at 1 !!
            /// </summary>
            public int Page { get; set; } = 1;
            internal Workshop workshop;

            public unsafe void Run()
            {
                if ( Callback != null )
                    return;

                if ( Page <= 0 )
                    throw new System.Exception( "Page should be 1 or above" );

                if ( FileId.Count != 0 )
                {
                    var fileArray = FileId.ToArray();

                    fixed ( ulong* array = fileArray )
                    {
                        Handle = workshop.ugc.CreateQueryUGCDetailsRequest( (IntPtr) array, (uint)fileArray.Length );
                    }
                }
                else
                {
                    Handle = workshop.ugc.CreateQueryAllUGCRequest( (uint)Order, (uint)QueryType, UploaderAppId, AppId, (uint)Page );
                }

                

                if ( !string.IsNullOrEmpty( SearchText ) )
                    workshop.ugc.SetSearchText( Handle, SearchText );

                foreach ( var tag in RequireTags )
                    workshop.ugc.AddRequiredTag( Handle, tag );

                if ( RequireTags.Count > 0 )
                    workshop.ugc.SetMatchAnyTag( Handle, RequireAllTags );

                foreach ( var tag in ExcludeTags )
                    workshop.ugc.AddExcludedTag( Handle, tag );

                Callback = new QueryCompleted();
                Callback.Handle = workshop.ugc.SendQueryUGCRequest( Handle );
                Callback.OnResult = OnResult;
                workshop.steamworks.AddCallResult( Callback );
            }

            void OnResult( QueryCompleted.Data data )
            {
                List< WorkshopItem > items = new List<WorkshopItem>();
                for ( int i = 0; i < data.m_unNumResultsReturned; i++ )
                {
                    SteamUGCDetails_t details = new SteamUGCDetails_t();
                    workshop.ugc.GetQueryUGCResult( data.Handle, (uint) i, ref details );

                    items.Add( WorkshopItem.From( details ) );
                }

                Items = items.ToArray();
                TotalResults = (int) data.m_unTotalMatchingResults;

                Callback.Dispose();
                Callback = null;
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
                workshop.steamworks.Update();

                while ( IsRunning )
                {
                    System.Threading.Thread.Sleep( 10 );
                    workshop.steamworks.Update();
                }
            }

            public void Dispose()
            {

            }
        }
    }
}
