using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Facepunch.Steamworks
{
    public partial class Inventory
    {
        /// <summary>
        /// An item definition. This describes an item in your Steam inventory, but is
        /// not unique to that item. For example, this might be a tshirt, but you might be able to own
        /// multiple tshirts.
        /// </summary>
        public class Definition
        {
            internal Valve.Steamworks.ISteamInventory inventory;

            public int Id;
            public string Name;
            public string Description;

            public DateTime Created;
            public DateTime Modified;

            internal Definition( int id )
            {
                Id = id;
            }

            public T GetProperty<T>( string name )
            {
                string val = string.Empty;

                if ( !inventory.GetItemDefinitionProperty( Id, name, out val ) )
                    return default( T );

                try
                {
                    return (T)Convert.ChangeType( val, typeof( T ) );
                }
                catch ( System.Exception )
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
                inventory.TriggerItemDrop( ref result, Id );
                inventory.DestroyResult( result );
            }
        }
    }
}
