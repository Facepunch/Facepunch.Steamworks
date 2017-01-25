using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using SteamNative;

namespace Facepunch.Steamworks
{
    public partial class Inventory : IDisposable
    {
        /// <summary>
        /// Called when the local client's items are first retrieved, and when they change.
        /// Obviously not called on the server.
        /// </summary>
        public Action OnUpdate;

        /// <summary>
        /// A list of items owned by this user. You should call Refresh() before trying to access this, 
        /// and then wait until it's non null or listen to OnUpdate to find out immediately when it's populated.
        /// </summary>
        public Item[] Items;

        /// <summary>
        /// You can send this data to a server, or another player who can then deserialize it
        /// and get a verified list of items.
        /// </summary>
        public byte[] SerializedItems;

        /// <summary>
        /// Serialized data exprires after an hour. This is the time the value in SerializedItems will expire.
        /// </summary>
        public DateTime SerializedExpireTime;

        internal uint LastTimestamp = 0;

        internal SteamNative.SteamInventory inventory;

        private Stopwatch fetchRetryTimer;

        private bool IsServer { get; set; }

        internal Inventory( BaseSteamworks steamworks, SteamNative.SteamInventory c, bool server )
        {
            IsServer = server;
            inventory = c;

            Result.Pending = new Dictionary<int, Result>();

            inventory.LoadItemDefinitions();
            FetchItemDefinitions();

            if ( !server )
            {
                SteamNative.SteamInventoryResultReady_t.RegisterCallback( steamworks, onResultReady );
                SteamNative.SteamInventoryFullUpdate_t.RegisterCallback( steamworks, onFullUpdate );

                //
                // Get a list of our items immediately
                //
                Refresh();
            }
        }

        /// <summary>
        /// We've received a FULL update
        /// </summary>
        private void onFullUpdate( SteamInventoryFullUpdate_t data, bool error )
        {
            if ( error ) return;

            var result = new Result( this, data.Handle, false );
            result.Fill();

            onResult( result, true );
        }

        /// <summary>
        /// A generic result has returned.
        /// </summary>
        private void onResultReady( SteamInventoryResultReady_t data, bool error )
        {
            if ( Result.Pending.ContainsKey( data.Handle ) )
            {
                var result = Result.Pending[data.Handle];

                result.OnSteamResult( data, error );

                if ( !error && data.Esult == SteamNative.Result.OK )
                {
                    onResult( result, false );
                }

                Result.Pending.Remove( data.Handle );
            }
        }

        private void onResult( Result r, bool serialize )
        {
            if ( r.IsSuccess )
            {
                //
                // We only serialize FULL updates
                //
                if ( serialize )
                {
                    //
                    // Only serialize if this result is newer than the last one
                    //
                    if ( r.Timestamp < LastTimestamp )
                        return;

                    SerializedItems = r.Serialize();
                    SerializedExpireTime = DateTime.Now.Add( TimeSpan.FromMinutes( 60 ) );
                }

                LastTimestamp = r.Timestamp;
                ApplyResult( r );
            }

            r.Dispose();
            r = null;
        }

        /// <summary>
        /// Apply this result to our current stack of Items
        /// Here we're trying to keep our stack up to date with whatever happens
        /// with the crafting, stacking etc
        /// </summary>
        internal void ApplyResult( Result r )
        {
            if ( IsServer ) return;

            if ( r.IsSuccess && r.Items != null )
            {
                if ( Items == null )
                    Items = new Item[0];

                Items = Items
                        .Union( r.Items )
                        .Distinct()
                        .Where( x => !r.Removed.Contains( x ) )
                        .Where( x => !r.Consumed.Contains( x ) )
                        .ToArray();

                //
                // Tell everyone we've got new items!
                //
                OnUpdate?.Invoke();
            }
        }

        public void Dispose()
        {
            inventory = null;

            Items = null;
            SerializedItems = null;

            Result.Pending = null;
        }

        /// <summary>
        /// Call this at least every two minutes, every frame doesn't hurt.
        /// You should call it when you consider it active play time.
        /// IE - your player is alive, and playing.
        /// Don't stress on it too much tho cuz it's super hijackable anyway.
        /// </summary>
        [Obsolete( "No longer required, will be removed in a later version" )]
        public void PlaytimeHeartbeat()
        {
        }

        /// <summary>
        /// Call this to retrieve the items.
        /// Note that if this has already been called it won't
        /// trigger a call to OnUpdate unless the items have changed
        /// </summary>
        public void Refresh()
        {
            if ( IsServer ) return;

            SteamNative.SteamInventoryResult_t request = 0;
            if ( !inventory.GetAllItems( ref request ) || request == -1 )
            {
                Console.WriteLine( "GetAllItems failed!?" );
                return;
            }
        }

        /// <summary>
        /// Some definitions aren't sent to the client, and all aren't available on the server.
        /// Manually getting a Definition here lets you call functions on those definitions.
        /// </summary>
        public Definition CreateDefinition( int id )
        {
            return new Definition( inventory, id );
        }

        internal void FetchItemDefinitions()
        {
            //
            // Make sure item definitions are loaded, because we're going to be using them.
            //

            var ids = inventory.GetItemDefinitionIDs();
            if ( ids == null )
                return;

            Definitions = ids.Select( x => CreateDefinition( x ) ).ToArray();

            foreach ( var def in Definitions )
            {
                def.Link( Definitions );
            }
        }

        /// <summary>
        /// No need to call this manually if you're calling Update
        /// </summary>
        public void Update()
        {
            if ( Definitions == null )
            {
                //
                // Don't try every frame, just try every 10 seconds.
                //
                {
                    if ( fetchRetryTimer != null && fetchRetryTimer.Elapsed.TotalSeconds < 10.0f )
                        return;

                    if ( fetchRetryTimer == null )
                        fetchRetryTimer = Stopwatch.StartNew();

                    fetchRetryTimer.Reset();
                    fetchRetryTimer.Start();
                }

                FetchItemDefinitions();
                inventory.LoadItemDefinitions();
            }
        }

        /// <summary>
        /// A list of items defined for this app. 
        /// This should be immediately populated and available.
        /// </summary>
        public Definition[] Definitions;

        /// <summary>
        /// Utility, given a "1;VLV250" string, convert it to a 2.5
        /// </summary>
        public static float PriceCategoryToFloat( string price )
        {
            if ( string.IsNullOrEmpty( price ) )
                return 0.0f;

            price = price.Replace( "1;VLV", "" );

            int iPrice = 0;
            if ( !int.TryParse( price, out iPrice ) )
                return 0.0f;

            return int.Parse( price ) / 100.0f;
        }

        /// <summary>
        /// You really need me to explain what this does?
        /// Use your brains.
        /// </summary>
        public Definition FindDefinition( int DefinitionId )
        {
            if ( Definitions == null ) return null;

            return Definitions.FirstOrDefault( x => x.Id == DefinitionId );
        }

        public unsafe Result Deserialize( byte[] data, int dataLength = -1 )
        {
            if ( dataLength == -1 )
                dataLength = data.Length;

            SteamNative.SteamInventoryResult_t resultHandle = -1;

            fixed ( byte* ptr = data )
            {
                var result = inventory.DeserializeResult( ref resultHandle, (IntPtr) ptr, (uint)dataLength, false );
                if ( !result || resultHandle == -1 )
                    return null;

                var r = new Result( this, resultHandle, false );
                r.Fill();
                return r;
            }
        }

        /// <summary>
        /// Crafting! Uses the passed items to buy the target item.
        /// You need to have set up the appropriate exchange rules in your item
        /// definitions.
        /// </summary>
        public Result CraftItem( Item[] list, Definition target )
        {
            SteamNative.SteamInventoryResult_t resultHandle = -1;

            var newItems = new SteamNative.SteamItemDef_t[] { new SteamNative.SteamItemDef_t() { Value = target.Id } };
            var newItemC = new uint[] { 1 };

            var takeItems = list.Select( x => (SteamNative.SteamItemInstanceID_t)x.Id ).ToArray();
            var takeItemsC = list.Select( x => (uint)1 ).ToArray();

            if ( !inventory.ExchangeItems( ref resultHandle, newItems, newItemC, 1, takeItems, takeItemsC, (uint)takeItems.Length ) )
                return null;

            return new Result( this, resultHandle, true );
        }

        /// <summary>
        /// Split stack into two items
        /// </summary>
        public Result SplitStack( Item item, int quantity = 1 )
        {
            SteamNative.SteamInventoryResult_t resultHandle = -1;
            if ( !inventory.TransferItemQuantity( ref resultHandle, item.Id, (uint)quantity, ulong.MaxValue ) )
                return null;

            return new Result( this, resultHandle, true );
        }

        /// <summary>
        /// Stack source item onto dest item
        /// </summary>
        public Result Stack( Item source, Item dest, int quantity = 1 )
        {
            SteamNative.SteamInventoryResult_t resultHandle = -1;
            if ( !inventory.TransferItemQuantity( ref resultHandle, source.Id, (uint)quantity, dest.Id ) )
                return null;

            return new Result( this, resultHandle, true );
        }
    }
}
