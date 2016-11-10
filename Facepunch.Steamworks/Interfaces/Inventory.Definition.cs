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
            public string Name { get; set; }
            public string Description { get; set; }

            /// <summary>
            /// URL to an image specified by the schema, else empty
            /// </summary>
            public string IconUrl { get; set; }

            /// <summary>
            /// URL to an image specified by the schema, else empty
            /// </summary>
            public string IconLargeUrl { get; set; }

            /// <summary>
            /// Type can be whatever the schema defines. 
            /// </summary>
            public string Type { get; set; }

            /// <summary>
            /// If this item can be created using other items this string will contain a comma seperated 
            /// list of definition ids that can be used, ie "100,101;102x5;103x3,104x3"
            /// </summary>
            public string ExchangeSchema { get; set; }

            /// <summary>
            /// A list of recepies for creating this item. Can be null if none.
            /// </summary>
            public Recipe[] Recipes { get; set; }

            /// <summary>
            /// A list of recepies we're included in
            /// </summary>
            public Recipe[] IngredientFor { get; set; }

            public DateTime Created { get; set; }
            public DateTime Modified { get; set; }

            public bool IsGenerator
            {
                get { return Type == "generator"; }
            }

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
                ExchangeSchema = GetStringProperty( "exchange" );
                IconUrl = GetStringProperty( "icon_url" );
                IconLargeUrl = GetStringProperty( "icon_url_large" );
                Type = GetStringProperty( "type" );
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

            internal void Link( Definition[] definitions )
            {
                LinkExchange( definitions );
            }

            private void LinkExchange( Definition[] definitions )
            {
                if ( string.IsNullOrEmpty( ExchangeSchema ) ) return;

                var parts = ExchangeSchema.Split( new[] { ';' }, StringSplitOptions.RemoveEmptyEntries );

                Recipes = parts.Select( x => Recipe.FromString( x, definitions, this ) ).ToArray();
            }

            internal void InRecipe( Recipe r )
            {
                if ( IngredientFor == null )
                    IngredientFor = new Recipe[0];

                var list = new List<Recipe>( IngredientFor );
                list.Add( r );

                IngredientFor = list.ToArray();
            }
        }

        /// <summary>
        /// Represents a crafting recepie which was defined using the exchange
        /// section in the item schema.
        /// </summary>
        public struct Recipe
        {
            public struct Ingredient
            {
                /// <summary>
                /// The definition ID of the ingredient.
                /// </summary>
                public int DefinitionId;

                /// <summary>
                /// If we don't know about this item definition this might be null.
                /// In which case, DefinitionId should still hold the correct id.
                /// </summary>
                public Definition Definition;

                /// <summary>
                /// The amount of this item needed. Generally this will be 1.
                /// </summary>
                public int Count;

                internal static Ingredient FromString( string part, Definition[] definitions )
                {
                    var i = new Ingredient();
                    i.Count = 1;

                    if ( part.Contains( 'x' ) )
                    {
                        var idx = part.IndexOf( 'x' );
                        i.Count = int.Parse( part.Substring( idx + 1 ) );
                        part = part.Substring( 0, idx );
                    }

                    i.DefinitionId = int.Parse( part );
                    i.Definition = definitions.FirstOrDefault( x => x.Id == i.DefinitionId );

                    return i;
                }
            }

            /// <summary>
            /// The item that this will create.
            /// </summary>
            public Definition Result;

            /// <summary>
            /// The items, with quantity required to create this item.
            /// </summary>
            public Ingredient[] Ingredients;

            internal static Recipe FromString( string part, Definition[] definitions, Definition Result )
            {
                var r = new Recipe();
                r.Result = Result;
                var parts = part.Split( new[] { ',' }, StringSplitOptions.RemoveEmptyEntries );

                r.Ingredients = parts.Select( x => Ingredient.FromString( x, definitions ) ).ToArray();

                foreach ( var i in r.Ingredients )
                {
                    i.Definition.InRecipe( r );
                }

                return r;
            }
        }
    }
}
