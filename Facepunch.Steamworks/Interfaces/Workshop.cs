using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using SteamNative;

namespace Facepunch.Steamworks
{
    /// <summary>
    /// Allows you to interact with Steam's UGC stuff (User Generated Content).
    /// To put simply, this allows you to upload a folder of files to Steam.
    /// 
    /// To upload a new file use CreateItem. This returns an Editor object.
    /// This object is also used to edit existing items.
    /// 
    /// To get a list of items you can call CreateQuery. From there you can download
    /// an item and retrieve the folder that it's downloaded to. 
    /// 
    /// Generally there's no need to compress and decompress your uploads, so you should
    /// usually be able to use the content straight from the destination folder.
    /// 
    /// </summary>
    public partial class Workshop : IDisposable
    {
        static Workshop()
        {
            Debug.Assert( Marshal.SizeOf( typeof(PublishedFileId_t) ) == Marshal.SizeOf( typeof(ulong) ),
                $"sizeof({nameof(PublishedFileId_t)}) != sizeof({nameof(UInt64)})" );
        }

        internal const ulong InvalidHandle = 0xffffffffffffffff;

        internal SteamNative.SteamUGC ugc;
        internal Friends friends;
        internal BaseSteamworks steamworks;
        internal SteamNative.SteamRemoteStorage remoteStorage;

        /// <summary>
        /// Called when an item has been downloaded. This could have been
        /// because of a call to Download.
        /// </summary>
        public event Action<ulong, Callbacks.Result> OnFileDownloaded;

        /// <summary>
        /// Called when an item has been installed. This could have been
        /// because of a call to Download or because of a subscription triggered
        /// via the browser/app.
        /// </summary>
        public event Action<ulong> OnItemInstalled;

        internal Workshop( BaseSteamworks steamworks, SteamNative.SteamUGC ugc, SteamNative.SteamRemoteStorage remoteStorage )
        {
            this.ugc = ugc;
            this.steamworks = steamworks;
            this.remoteStorage = remoteStorage;

            steamworks.RegisterCallback<SteamNative.DownloadItemResult_t>( onDownloadResult );
            steamworks.RegisterCallback<SteamNative.ItemInstalled_t>( onItemInstalled );
        }

        /// <summary>
        /// You should never have to call this manually
        /// </summary>
        public void Dispose()
        {
            ugc = null;
            steamworks = null;
            remoteStorage = null;
            friends = null;

            OnFileDownloaded = null;
            OnItemInstalled = null;
        }

        private void onItemInstalled( SteamNative.ItemInstalled_t obj )
        {
            if ( OnItemInstalled != null && obj.AppID == Client.Instance.AppId )
                OnItemInstalled( obj.PublishedFileId );
        }

        private void onDownloadResult( SteamNative.DownloadItemResult_t obj )
        {
            if ( OnFileDownloaded != null && obj.AppID == Client.Instance.AppId )
                OnFileDownloaded( obj.PublishedFileId, (Callbacks.Result) obj.Result );
        }

        /// <summary>
        /// Get the IDs of all subscribed workshop items. Not all items may be currently installed.
        /// </summary>
        public unsafe ulong[] GetSubscribedItemIds()
        {
            var count = ugc.GetNumSubscribedItems();
            var array = new ulong[count];

            fixed ( ulong* ptr = array )
            {
                ugc.GetSubscribedItems( (PublishedFileId_t*) ptr, count );
            }

            return array;
        }

        [ThreadStatic]
        private static ulong[] _sSubscribedItemBuffer;

        /// <summary>
        /// Get the IDs of all subscribed workshop items, avoiding repeated allocations.
        /// Not all items may be currently installed.
        /// </summary>
        public unsafe int GetSubscribedItemIds( List<ulong> destList )
        {
            const int bufferSize = 1024;

            var count = ugc.GetNumSubscribedItems();

            if ( count >= bufferSize )
            {
                // Fallback for exceptional cases
                destList.AddRange( GetSubscribedItemIds() );
                return (int) count;
            }

            if ( _sSubscribedItemBuffer == null )
            {
                _sSubscribedItemBuffer = new ulong[bufferSize];
            }

            fixed ( ulong* ptr = _sSubscribedItemBuffer)
            {
                count = ugc.GetSubscribedItems( (PublishedFileId_t*) ptr, bufferSize );
            }

            for ( var i = 0; i < count; ++i )
            {
                destList.Add( _sSubscribedItemBuffer[i] );
            }

            return (int) count;
        }

        /// <summary>
        /// Creates a query object, which is used to get a list of items.
        /// 
        /// This could be a list of the most popular items, or a search, 
        /// or just getting a list of the items you've uploaded.
        /// </summary>
        public Query CreateQuery()
        {
            return new Query()
            {
                AppId = steamworks.AppId,
                workshop = this,
                friends = friends
            };
        }

        /// <summary>
        /// Create a new Editor object with the intention of creating a new item.
        /// Your item won't actually be created until you call Publish() on the object.
        /// </summary>
        public Editor CreateItem( ItemType type = ItemType.Community )
        {
        	return CreateItem(this.steamworks.AppId, type);
        }
        
        /// <summary>
        /// Create a new Editor object with the intention of creating a new item.
        /// Your item won't actually be created until you call Publish() on the object.
        /// Your item will be published to the provided appId.
        /// </summary>
        /// <remarks>You need to add app publish permissions for cross app uploading to work.</remarks> 
        public Editor CreateItem( uint workshopUploadAppId, ItemType type = ItemType.Community )
        {
        	return new Editor() { workshop = this, WorkshopUploadAppId = workshopUploadAppId, Type = type };
        }
        
        /// <summary>
        /// Returns a class representing this ItemId. We don't query
        /// item name, description etc. We don't verify that item exists.
        /// We don't verify that this item belongs to your app.
        /// </summary>
        public Editor EditItem( ulong itemId )
        {
            return new Editor() { workshop = this, Id = itemId, WorkshopUploadAppId = steamworks.AppId };
        }

        /// <summary>
        /// Gets an Item object for a specific item. This doesn't currently
        /// query the item's name and description. It's only really useful
        /// if you know an item's ID and want to download it, or check its
        /// current download status.
        /// </summary>
        public Item GetItem( ulong itemid )
        {
            return new Item( itemid, this );
        }

        /// <summary>
        /// How a query should be ordered.
        /// </summary>
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

        /// <summary>
        /// The type of item you are querying for
        /// </summary>
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
            SubscriptionItems = 2,
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

        /// <summary>
        /// Used to define the item type when creating
        /// </summary>
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

        /// <summary>
        /// When querying a specific user's items this defines what
        /// type of items you're looking for.
        /// </summary>
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
