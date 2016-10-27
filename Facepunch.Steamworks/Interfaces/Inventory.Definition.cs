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
            internal SteamNative.SteamInventory inventory;

            public int Id { get; private set; }
            public string Name;
            public string Description;

            public DateTime Created;
            public DateTime Modified;

            private Dictionary<string, string> customProperties;

            internal Definition( SteamNative.SteamInventory i, int id )
            {
                inventory = i;
                Id = id;

                SetupCommonProperties();
            }

            /// <summary>
            /// If you're manually occupying the Definition (because maybe you're on a server
            /// and want to hack around the fact that definitions aren't presented to you), 
            /// you can use this to set propertis.
            /// </summary>
            public void SetProperty( string name, string value )
            {
                if ( customProperties == null )
                    customProperties = new Dictionary<string, string>();

                if ( !customProperties.ContainsKey( name ) )
                    customProperties.Add( name, value );
                else
                    customProperties[name] = value;
            }

            public T GetProperty<T>( string name )
            {
                string val = GetStringProperty( name );

                if ( string.IsNullOrEmpty( val ) )
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

            public string GetStringProperty( string name )
            {
                string val = string.Empty;

                if ( customProperties != null && customProperties.ContainsKey( name ) )
                    return customProperties[name];

                if ( !inventory.GetItemDefinitionProperty( Id, name, out val ) )
                    return string.Empty;

                return val;
            }

            internal void SetupCommonProperties()
            {
                Name = GetStringProperty( "name" );
                Description = GetStringProperty( "description" );
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
                SteamNative.SteamInventoryResult_t result = 0;
                inventory.TriggerItemDrop( ref result, Id );
                inventory.DestroyResult( result );
            }
        }
    }
}
