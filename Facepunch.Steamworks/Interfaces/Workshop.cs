using System;
using System.Collections.Generic;
using Facepunch.Steamworks.Callbacks.Workshop;
using Valve.Steamworks;

namespace Facepunch.Steamworks
{
    public partial class Workshop : IDisposable
    {
        internal const ulong InvalidHandle = 0xffffffffffffffff;

        internal ISteamUGC ugc;
        internal BaseSteamworks steamworks;
        internal ISteamRemoteStorage remoteStorage;

        internal event Action<ulong, Callbacks.Result> OnFileDownloaded;
        internal event Action<ulong> OnItemInstalled;

        internal Workshop( BaseSteamworks steamworks, ISteamUGC ugc, ISteamRemoteStorage remoteStorage )
        {
            this.ugc = ugc;
            this.steamworks = steamworks;
            this.remoteStorage = remoteStorage;

            steamworks.AddCallback<DownloadResult, DownloadResult.Small>( onDownloadResult, DownloadResult.CallbackId );
            steamworks.AddCallback<ItemInstalled, DownloadResult.Small>( onItemInstalled, ItemInstalled.CallbackId );
        }

        public void Dispose()
        {
            ugc = null;
            steamworks = null;
            remoteStorage = null;

            OnFileDownloaded = null;
            OnItemInstalled = null;
        }

        private void onItemInstalled( ItemInstalled obj )
        {
            if ( OnItemInstalled != null )
                OnItemInstalled( obj.FileId );
        }

        private void onDownloadResult( DownloadResult obj )
        {
            if ( OnFileDownloaded != null )
                OnFileDownloaded( obj.FileId, obj.Result );
        }

        public Query CreateQuery()
        {
            return new Query()
            {
                AppId = steamworks.AppId,
                workshop = this
            };
        }

        public Editor CreateItem( ItemType type )
        {
            return new Editor() { workshop = this, Type = type };
        }

        public Editor EditItem( ulong itemId )
        {
            return new Editor() { workshop = this, Id = itemId };
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
            /// <summary>
            /// Both MicrotransactionItems and subscriptionItems
            /// </summary>
            Items = 0,
            /// <summary>
            /// Workshop item that is meant to be voted on for the purpose of selling in-game
            /// </summary>
            MicrotransactionItems = 1,
            /// <summary>
            /// normal Workshop item that can be subscribed to
            /// </summary>
            subscriptionItems = 2,
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

        public enum ItemType
        {
            Community = 0,       // normal Workshop item that can be subscribed to
            Microtransaction = 1,        // Workshop item that is meant to be voted on for the purpose of selling in-game
            Collection = 2,      // a collection of Workshop or Greenlight items
            Art = 3,     // artwork
            Video = 4,       // external video
            Screenshot = 5,      // screenshot
            Game = 6,        // Greenlight game entry
            Software = 7,        // Greenlight software entry
            Concept = 8,     // Greenlight concept
            WebGuide = 9,        // Steam web guide
            IntegratedGuide = 10,        // application integrated guide
            Merch = 11,      // Workshop merchandise meant to be voted on for the purpose of being sold
            ControllerBinding = 12,      // Steam Controller bindings
            SteamworksAccessInvite = 13,     // internal
            SteamVideo = 14,     // Steam video
            GameManagedItem = 15,        // managed completely by the game, not the user, and not shown on the web
        };

        public enum UserQueryType : uint
        {
            Published = 0,
            VotedOn,
            VotedUp,
            VotedDown,
            WillVoteLater,
            Favorited,
            Subscribed,
            UsedOrPlayed,
            Followed,
        }


    }
}
