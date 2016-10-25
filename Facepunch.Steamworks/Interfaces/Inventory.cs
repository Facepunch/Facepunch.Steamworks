using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

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

        internal SteamNative.SteamInventory inventory;

        private Result LocalPlayerRequest;

        private bool IsServer { get; set; }

        internal Inventory( SteamNative.SteamInventory c, bool server )
        {
            IsServer = server;
            inventory = c;

            inventory.LoadItemDefinitions();
            FetchItemDefinitions();
        }

        public void Dispose()
        {
            if ( LocalPlayerRequest != null )
            {
                LocalPlayerRequest.Dispose();
                LocalPlayerRequest = null;
            }

            inventory = null;

            Items = null;
            SerializedItems = null;
        }

        /// <summary>
        /// Call this at least every two minutes, every frame doesn't hurt.
        /// You should call it when you consider it active play time.
        /// IE - your player is alive, and playing.
        /// Don't stress on it too much tho cuz it's super hijackable anyway.
        /// </summary>
        public void PlaytimeHeartbeat()
        {
            inventory.SendItemDropHeartbeat();
        }

        /// <summary>
        /// Call this to retrieve the items.
        /// Note that if this has already been called it won't
        /// trigger a call to OnUpdate unless the items have changed
        /// </summary>
        public void Refresh()
        {
            if ( IsServer ) return;

            // Pending
            if ( LocalPlayerRequest != null )
                return;

            SteamNative.SteamInventoryResult_t request = 0;
            if ( !inventory.GetAllItems( ref request ) || request == -1 )
            {
                Console.WriteLine( "GetAllItems failed!?" );
                return;
            }

            LocalPlayerRequest = new Result( this, request );
        }

        /// <summary>
        /// Some definitions aren't sent to the client, and all aren't available on the server.
        /// Manually getting a Definition here lets you call functions on those definitions.
        /// </summary>
        public Definition CreateDefinition( int id )
        {
            return new Definition( id )
            {
                inventory = inventory
            };
        }

        internal void FetchItemDefinitions()
        {
            //
            // Make sure item definitions are loaded, because we're going to be using them.
            //

            var ids = inventory.GetItemDefinitionIDs();
            if ( ids == null )
                return;

            Definitions = ids.Select( x =>
            {
                var d = CreateDefinition( x );
                d.SetupCommonProperties();
                return d;

            } ).ToArray();
        }

        /// <summary>
        /// Called every frame
        /// </summary>
        internal void Update()
        {
            if ( Definitions == null && !IsServer )
                FetchItemDefinitions();

            UpdateLocalRequest();
        }

        /// <summary>
        /// If we have a local player request process it.
        /// </summary>
        private void UpdateLocalRequest()
        {
            if ( LocalPlayerRequest == null )
                return;

            if ( LocalPlayerRequest.IsPending )
                return;

            if ( LocalPlayerRequest.IsSuccess )
            {
                // Try again.
                RetrieveInventory();
                return;
            }

            // Some other error
            // Lets just retry.
            LocalPlayerRequest.Dispose();
            LocalPlayerRequest = null;
            Refresh();
        }

        private unsafe void RetrieveInventory()
        {
            SerializedItems = LocalPlayerRequest.Serialize();
            SerializedExpireTime = DateTime.Now.Add( TimeSpan.FromMinutes( 60 ) );

            Items = LocalPlayerRequest.Items;

            LocalPlayerRequest.Dispose();
            LocalPlayerRequest = null;

            //
            // Tell everyone we've got new items!
            //
            OnUpdate?.Invoke();
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
            price = price.Replace( "1;VLV", "" );

            int iPrice = 0;
            if ( !int.TryParse( price, out iPrice ) )
                return 0.0f;

            return int.Parse( price ) / 100.0f;
        }

        private Definition FindDefinition( int def )
        {
            if ( Definitions == null ) return null;

            return Definitions.FirstOrDefault( x => x.Id == def );
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

                return new Result( this, resultHandle );
            }
        }
    }
}
