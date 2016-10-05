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
            All = ~0,     // return everything
        };

        public WorkshopQuery CreateQuery()
        {
            var q = new WorkshopQuery();
            q.workshop = this;
            return q;
        }

        public class WorkshopQuery
        {
            internal ulong Handle;
            internal QueryCompleted Callback;

            public QueryType QueryType { get; set; } = QueryType.All;
            public Order Order { get; set; } = Order.RankedByPublicationDate;

            public string SearchText { get; set; }

            /// <summary>
            /// Page starts at 1 !!
            /// </summary>
            public int Page { get; set; } = 1;
            internal Workshop workshop;

            public void Run()
            {
                if ( Callback != null )
                    return;

                if ( Page <= 0 )
                    throw new System.Exception( "Page should be 1 or above" );

                Handle = workshop.ugc.CreateQueryAllUGCRequest( (uint)Order, (uint)QueryType, workshop.steamworks.AppId, workshop.steamworks.AppId, (uint)Page );

                if ( !string.IsNullOrEmpty( SearchText ) )
                    workshop.ugc.SetSearchText( Handle, SearchText );

                Callback = new QueryCompleted();
                Callback.Handle = workshop.ugc.SendQueryUGCRequest( Handle );
                Callback.OnResult = OnResult;
                workshop.steamworks.AddCallResult( Callback );
            }

            void OnResult( QueryCompleted.Data data )
            {
                Callback.Dispose();
                Callback = null;

                Console.WriteLine( "Results: " + data.m_unTotalMatchingResults );
            }

            public bool IsRunning
            {
                get { return Callback != null; }
            }

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
        }
    }
}
