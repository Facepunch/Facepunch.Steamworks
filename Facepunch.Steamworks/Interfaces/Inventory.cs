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
        public event Action OnUpdate;

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

        /// <summary>
        /// Controls whether per-item properties (<see cref="Item.Properties"/>) are available or not. Default true.
        /// This can improve performance of full inventory updates.
        /// </summary>
        public bool EnableItemProperties = true;

        internal uint LastTimestamp = 0;

        internal SteamNative.SteamInventory inventory;

        private bool IsServer { get; set; }

        public event Action OnDefinitionsUpdated;

        public event Action<Result> OnInventoryResultReady;

        internal Inventory( BaseSteamworks steamworks, SteamNative.SteamInventory c, bool server )
        {
            IsServer = server;
            inventory = c;

            steamworks.RegisterCallback<SteamNative.SteamInventoryDefinitionUpdate_t>( onDefinitionsUpdated );

            Result.Pending = new Dictionary<int, Result>();

            FetchItemDefinitions();
            LoadDefinitions();
            UpdatePrices();

            if ( !server )
            {
                steamworks.RegisterCallback<SteamNative.SteamInventoryResultReady_t>( onResultReady );
                steamworks.RegisterCallback<SteamNative.SteamInventoryFullUpdate_t>( onFullUpdate );
                

                //
                // Get a list of our items immediately
                //
                Refresh();
            }
        }

        /// <summary>
        /// Should get called when the definitions get updated from Steam.
        /// </summary>
        private void onDefinitionsUpdated( SteamInventoryDefinitionUpdate_t obj )
        {
            LoadDefinitions();
            UpdatePrices();

            if ( OnDefinitionsUpdated != null )
            {
                OnDefinitionsUpdated.Invoke();
            }
        }

        private bool LoadDefinitions()
        {
            var ids = inventory.GetItemDefinitionIDs();
            if ( ids == null )
                return false;

            Definitions = ids.Select( x => CreateDefinition( x ) ).ToArray();

            foreach ( var def in Definitions )
            {
                def.Link( Definitions );
            }

            return true;
        }

        /// <summary>
        /// We've received a FULL update
        /// </summary>
        private void onFullUpdate( SteamInventoryFullUpdate_t data )
        {
            var result = new Result( this, data.Handle, false );
            result.Fill();

            onResult( result, true );
        }

        /// <summary>
        /// A generic result has returned.
        /// </summary>
        private void onResultReady( SteamInventoryResultReady_t data )
        {
            Result result;
            if ( Result.Pending.TryGetValue( data.Handle, out result ) )
            {
                result.OnSteamResult( data );

                if ( data.Result == SteamNative.Result.OK )
                {
                    onResult( result, false );
                }

                Result.Pending.Remove( data.Handle );
                result.Dispose();
            }
            else
            {
                result = new Result(this, data.Handle, false);
                result.Fill();
            }

            OnInventoryResultReady?.Invoke(result);
        }

        private void onResult( Result r, bool isFullUpdate )
        {
            if ( r.IsSuccess )
            {
                //
                // We only serialize FULL updates
                //
                if ( isFullUpdate )
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
                ApplyResult( r, isFullUpdate );
            }

            r.Dispose();
            r = null;
        }

        /// <summary>
        /// Apply this result to our current stack of Items
        /// Here we're trying to keep our stack up to date with whatever happens
        /// with the crafting, stacking etc
        /// </summary>
        internal void ApplyResult( Result r, bool isFullUpdate )
        {
            if ( IsServer ) return;

            if ( r.IsSuccess && r.Items != null )
            {
                if ( Items == null )
                    Items = new Item[0];

                if (isFullUpdate)
                {
                    Items = r.Items;
                }
                else
                {
                    // keep the new item instance because it might have a different quantity, properties, etc
                    Items = Items
                        .UnionSelect(r.Items, (oldItem, newItem) => newItem)
                        .Where(x => !r.Removed.Contains(x))
                        .Where(x => !r.Consumed.Contains(x))
                        .ToArray();
                }

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
            return new Definition( this, id );
        }

        /// <summary>
        /// Fetch item definitions in case new ones have been added since we've initialized
        /// </summary>
        public void FetchItemDefinitions()
        {
            inventory.LoadItemDefinitions();
        }

        /// <summary>
        /// No need to call this manually if you're calling Update
        /// </summary>
        public void Update()
        {

        }

        /// <summary>
        /// A list of items defined for this app. 
        /// This should be immediately populated and available.
        /// </summary>
        public Definition[] Definitions;

        /// <summary>
        /// A list of item definitions that have prices and so can be bought.
        /// </summary>
        public IEnumerable<Definition> DefinitionsWithPrices
        {
            get
            {
                if ( Definitions == null )
                    yield break;

                for ( int i=0; i< Definitions.Length; i++ )
                {
                    if (Definitions[i].LocalPrice > 0)
                        yield return Definitions[i];
                }
            }
        }

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
        /// We might be better off using a dictionary for this, once there's 1000+ definitions
        /// </summary>
        public Definition FindDefinition( int DefinitionId )
        {
            if ( Definitions == null ) return null;

            for( int i=0; i< Definitions.Length; i++ )
            {
                if ( Definitions[i].Id == DefinitionId )
                    return Definitions[i];
            }

            return null;
        }

        public unsafe Result Deserialize( byte[] data, int dataLength = -1 )
        {
            if (data == null)
                throw new ArgumentException("data should nto be null");

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
        /// definitions. This assumes all the items passed in aren't stacked.
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
        /// Crafting! Uses the passed items to buy the target item.
        /// You need to have set up the appropriate exchange rules in your item
        /// definitions.
        /// </summary>
        public Result CraftItem( Item.Amount[] list, Definition target )
        {
            SteamNative.SteamInventoryResult_t resultHandle = -1;

            var newItems = new SteamNative.SteamItemDef_t[] { new SteamNative.SteamItemDef_t() { Value = target.Id } };
            var newItemC = new uint[] { 1 };

            var takeItems = list.Select( x => (SteamNative.SteamItemInstanceID_t)x.Item.Id ).ToArray();
            var takeItemsC = list.Select( x => (uint)x.Quantity ).ToArray();

            if ( !inventory.ExchangeItems( ref resultHandle, newItems, newItemC, 1, takeItems, takeItemsC, (uint)takeItems.Length ) )
                return null;

            return new Result( this, resultHandle, true );
        }

        /// <summary>
        /// Split stack into two items
        /// </summary>
        public Result SplitStack( Item item, int quantity = 1 )
        {
            return item.SplitStack( quantity );
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

        /// <summary>
        /// This is used to grant a specific item to the user. This should 
        /// only be used for development prototyping, from a trusted server, 
        /// or if you don't care about hacked clients granting arbitrary items. 
        /// This call can be disabled by a setting on Steamworks.
        /// </summary>
        public Result GenerateItem( Definition target, int amount )
        {
            SteamNative.SteamInventoryResult_t resultHandle = -1;

            var newItems = new SteamNative.SteamItemDef_t[] { new SteamNative.SteamItemDef_t() { Value = target.Id } };
            var newItemC = new uint[] { (uint) amount };

            if ( !inventory.GenerateItems( ref resultHandle, newItems, newItemC, 1 ) )
                return null;

            return new Result( this, resultHandle, true );
        }

        public delegate void StartPurchaseSuccess( ulong orderId, ulong transactionId );

        /// <summary>
        /// Starts the purchase process for the user, given a "shopping cart" of item definitions that the user would like to buy. 
        /// The user will be prompted in the Steam Overlay to complete the purchase in their local currency, funding their Steam Wallet if necessary, etc.
        /// 
        /// If was succesful the callback orderId and transactionId will be non 0
        /// </summary>
        public bool StartPurchase( Definition[] items, StartPurchaseSuccess callback = null )
        {
            var itemGroup = items.GroupBy(x => x.Id);

            var newItems = itemGroup.Select( x => new SteamItemDef_t { Value = x.Key } ).ToArray();
            var newItemC = itemGroup.Select( x => (uint) x.Count() ).ToArray();

            var h = inventory.StartPurchase( newItems, newItemC, (uint) newItemC.Length, ( result, error ) =>
            {
                if ( error )
                {
                    callback?.Invoke(0, 0);
                }
                else
                {
                    callback?.Invoke(result.OrderID, result.TransID);
                }
            });

            return h != null;
        }

        /// <summary>
        /// This might be null until Steam has actually recieved the prices.
        /// </summary>
        public string Currency { get; private set; }

        public void UpdatePrices()
        {
            if (IsServer)
                return;

           inventory.RequestPrices((result, b) =>
           {
               Currency = result.Currency;

               if ( Definitions == null )
                   return;

               for (int i = 0; i < Definitions.Length; i++)
               {
                   Definitions[i].UpdatePrice();
               }

               OnUpdate?.Invoke();
           });
        }
    }
}
