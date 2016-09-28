using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Facepunch.Steamworks
{
    public partial class Client : IDisposable
    {
        Inventory _inv;

        public Inventory Inventory
        {
            get
            {
                if ( _inv == null )
                    _inv = new Inventory( this );

                return _inv;
            }
        }
    }

    public class Inventory
    {
        /// <summary>
        /// Called when the items are first retrieved, and when they change
        /// </summary>
        public Action OnUpdate;

        internal Client client;
        private int updateRequest = -1;

        internal Inventory( Client c )
        {
            client = c;

            LoadItemDefinitions();
        }

        /// <summary>
        /// Call this at least every two minutes, every frame doesn't hurt.
        /// You should call it when you consider it active play time.
        /// IE - your player is alive, and playing.
        /// Don't stress on it too much tho cuz it's super hijackable anyway.
        /// </summary>
        public void PlaytimeHeartbeat()
        {
            client.native.inventory.SendItemDropHeartbeat();
        }

        /// <summary>
        /// Call this to retrieve the items.
        /// Note that if this has already been called it won't
        /// trigger a call to OnUpdate unless the items have changed
        /// </summary>
        public void Refresh()
        {
            // Pending
            if ( updateRequest != -1 )
                return;

            if ( !client.native.inventory.GetAllItems( ref updateRequest ) )
            {
                Console.WriteLine( "GetAllItems failed!?" );
            }
        }

        internal void LoadItemDefinitions()
        {
            //
            // Make sure item definitions are loaded, because we're going to be using them.
            //
            client.native.inventory.LoadItemDefinitions();

            int[] ids;
            if ( !client.native.inventory.GetItemDefinitionIDs( out ids ) )
                return;

            Definitions = ids.Select( x =>
            {
                var d = new Definition()
                {
                    client = client,
                    Id = x
                };

                d.SetupCommonProperties();
                return d;

            } ).ToArray(); 
        }

        internal T DefinitionProperty<T>( int i, string name )
        {
            string val = string.Empty;
            client.native.inventory.GetItemDefinitionProperty( i, name, out val );
            return (T) Convert.ChangeType( val, typeof( T) );
        }

        internal void DestroyResult()
        {
            if ( updateRequest == -1 ) return;
            
            client.native.inventory.DestroyResult( updateRequest );
            updateRequest = -1;
        }


        internal void Update()
        {
            UpdateRequest();
        }

        private void UpdateRequest()
        {
            if ( updateRequest == -1 )
                return;

            var status = (Valve.Steamworks.EResult) client.native.inventory.GetResultStatus( updateRequest );

            if ( status == Valve.Steamworks.EResult.k_EResultPending )
                return;

            if ( status == Valve.Steamworks.EResult.k_EResultOK || status == Valve.Steamworks.EResult.k_EResultExpired )
            {
                RetrieveInventory();
                DestroyResult();
                return;
            }

            // Some other error
            // Lets just retry.
            DestroyResult();
            Refresh();
        }

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

        private unsafe void RetrieveInventory()
        {
            Valve.Steamworks.SteamItemDetails_t[] items = null;
            client.native.inventory.GetResultItems( updateRequest, out items );

            Items = items.Select( x =>
            {
                return new Item()
                {
                    client          = client,
                    Quantity        = x.m_unQuantity,
                    Id              = x.m_itemId,
                    DefinitionId    = x.m_iDefinition,
                    TradeLocked     = ( (int)x.m_unFlags & (int)Valve.Steamworks.ESteamItemFlags.k_ESteamItemNoTrade ) != 0,
                    Definition      = Definitions.FirstOrDefault( y => y.Id == x.m_iDefinition )
                };
            } ).ToArray();

            //
            // Get a serialized version
            //
            uint size = 0;
            client.native.inventory.SerializeResult( updateRequest, IntPtr.Zero, ref size );
            SerializedItems = new byte[size];

            fixed( byte* b = SerializedItems )
            {
                client.native.inventory.SerializeResult( updateRequest, (IntPtr) b, ref size );
            }

            SerializedExpireTime = DateTime.Now.Add( TimeSpan.FromMinutes( 60 ) );

            //
            // Tell everyone we've got new items!
            //
            OnUpdate?.Invoke();
        }

        /// <summary>
        /// An item in your inventory.
        /// </summary>
        public class Item
        {
            internal Client client;

            public ulong Id;
            public int Quantity;

            public int DefinitionId;
            public Definition Definition;

            public bool TradeLocked;
        }

        /// <summary>
        /// A list of items defined for this app. 
        /// This should be immediately populated and available.
        /// </summary>
        public Definition[] Definitions;


        /// <summary>
        /// An item definition. This describes an item in your Steam inventory, but is
        /// not unique to that item. For example, this might be a tshirt, but you might be able to own
        /// multiple tshirts.
        /// </summary>
        public class Definition
        {
            internal Client client;

            public int Id;
            public string Name;
            public string Description;

            public DateTime Created;
            public DateTime Modified;

            public T GetProperty<T>( string name )
            {
                string val = string.Empty;

                if ( !client.native.inventory.GetItemDefinitionProperty( Id, name, out val ) )
                    return default( T );

                try
                {
                    return (T)Convert.ChangeType( val, typeof( T ) );
                }
                catch( System.Exception )
                {
                    return default( T );
                }
            }

            internal void SetupCommonProperties()
            {
                Name = GetProperty<string>( "name" );
                Description = GetProperty<string>( "description" );
                Created = GetProperty<DateTime>( "timestamp" );
                Modified = GetProperty<DateTime>( "modified" );
            }

            /// <summary>
            /// Trigger an item drop. Call this when it's a good time to award
            /// an item drop to a player. This won't automatically result in giving
            /// an item to a player. Just call it every minute or so, or on launch.
            /// ItemDefinition is usually a generator
            /// </summary>
            public void TriggerItemDrop()
            {
                int result = 0;
                client.native.inventory.TriggerItemDrop( ref result, Id );
                client.native.inventory.DestroyResult( result );
            }
        }


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

    }
}
