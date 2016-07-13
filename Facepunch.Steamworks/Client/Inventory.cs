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
        private int updateRequest = 0;

        internal Inventory( Client c )
        {
            client = c;

            LoadItemDefinitions();


        }

        /// <summary>
        /// Call this to retrieve the items.
        /// Note that if this has already been called it won't
        /// trigger a call to OnUpdate unless the items have changed
        /// </summary>
        public void Refresh()
        {
            // Pending
            if ( updateRequest != 0 )
                return;

            client._inventory.GetAllItems( ref updateRequest );
        }

        internal void LoadItemDefinitions()
        {
            //
            // Make sure item definitions are loaded, because we're going to be using them.
            //
            client._inventory.LoadItemDefinitions();

            int[] ids;
            if ( !client._inventory.GetItemDefinitionIDs( out ids ) )
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
            client._inventory.GetItemDefinitionProperty( i, name, out val );
            return (T) Convert.ChangeType( val, typeof( T) );
        }

        internal void DestroyResult()
        {
            if ( updateRequest != 0 )
            {
                client._inventory.DestroyResult( updateRequest );
                updateRequest = 0;
            }
        }


        internal void Update()
        {
            UpdateRequest();
        }

        private void UpdateRequest()
        {
            if ( updateRequest == 0 )
                return;

            var status = (Valve.Steamworks.EResult) client._inventory.GetResultStatus( updateRequest );

            if ( status == Valve.Steamworks.EResult.k_EResultPending )
                return;

            if ( status == Valve.Steamworks.EResult.k_EResultOK || status == Valve.Steamworks.EResult.k_EResultExpired )
            {
                RetrieveInventory();
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

        private void RetrieveInventory()
        {
            Valve.Steamworks.SteamItemDetails_t[] items = null;
            client._inventory.GetResultItems( updateRequest, out items );

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

                if ( !client._inventory.GetItemDefinitionProperty( Id, name, out val ) )
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
    }

    }
}
