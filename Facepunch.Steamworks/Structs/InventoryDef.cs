using System;
using System.Collections.Generic;
using System.Linq;
using Steamworks.Data;

namespace Steamworks
{
	/// <summary>
	/// One entry in this app's item schema — an item <i>type</i>, such as "Red Hat" or "Wood".
	/// This is the template, not something the player owns; an owned instance is an
	/// <see cref="InventoryItem"/>, and it points back here through
	/// <see cref="InventoryItem.Def"/>. Use definitions to build stores, crafting screens, and any
	/// UI that must show items the player does not have yet.
	/// </summary>
	/// <remarks>
	/// <para>
	/// Definitions are authored entirely on the Steamworks partner site, under your app's
	/// Inventory Service item schema. They cannot be created from code — the constructor here only
	/// wraps an ID. If your app has no schema published,
	/// <see cref="SteamInventory.Definitions"/> is null and there are no definitions to get.
	/// </para>
	/// <para>
	/// Everything a definition knows is a string key/value pair from that schema. The named
	/// properties on this class (<see cref="Name"/>, <see cref="Type"/> and friends) are just
	/// typed shortcuts over <see cref="GetProperty(string)"/>; anything custom you put in the
	/// schema is reachable through that same method. Values are fetched from Steam on first
	/// access and then cached on this instance, so repeated reads are cheap but a definition
	/// object will not notice a mid-session schema change.
	/// </para>
	/// <para>
	/// Price members return zero until <see cref="SteamInventory.GetDefinitionsWithPricesAsync"/>
	/// has completed — Steam requires an explicit price request before it will quote anything.
	/// </para>
	/// </remarks>
	/// <example>
	/// <code>
	/// await SteamInventory.WaitForDefinitions();
	///
	/// foreach ( var def in SteamInventory.Definitions )
	/// {
	///     Console.WriteLine( $"{def.Id} {def.Name} ({def.Type})" );
	///
	///     if ( def.Tradable )
	///         Console.WriteLine( "  can be traded" );
	///
	///     // Custom schema keys are read the same way as the built-in ones.
	///     var rarity = def.GetProperty( "my_rarity" );
	/// }
	/// </code>
	/// </example>
	public class InventoryDef : IEquatable<InventoryDef>
	{
		internal InventoryDefId _id;
		internal Dictionary<string, string> _properties;

		/// <summary>
		/// Wraps a definition ID so its schema properties can be read. This does not create an
		/// item type and does not validate the ID — the schema lives on the Steamworks partner
		/// site. Normally you obtain definitions from <see cref="SteamInventory.Definitions"/> or
		/// <see cref="SteamInventory.FindDefinition"/> rather than constructing them.
		/// </summary>
		/// <param name="defId">
		/// The definition ID to wrap. If it is not present in the loaded schema, every property
		/// read on the resulting object returns null.
		/// </param>
		public InventoryDef( InventoryDefId defId )
		{
			_id = defId;
		}

		/// <summary>
		/// The numeric schema ID of this item type, as configured on the Steamworks partner site.
		/// This is the value that appears on <see cref="InventoryItem.DefId"/> and the one to
		/// persist if you need to refer to an item type from your own backend.
		/// </summary>
		/// <value>
		/// The definition ID. Valve treats values at or below zero as invalid, and reserves one
		/// billion and above for internal Steam use, so valid IDs sit between those bounds.
		/// </value>
		public int Id => _id.Value;

		/// <summary>
		/// The item's display name, ready to show to the player. Reads the schema's
		/// <c>"name"</c> property.
		/// </summary>
		/// <value>
		/// The localized name, or <c>null</c> if the schema does not define one. Valve localizes
		/// this property against the player's current Steam language, so it will differ between
		/// users and must not be used as a key or compared against a hardcoded string — use
		/// <see cref="Id"/> for identity.
		/// </value>
		public string Name => GetProperty( "name" );

		/// <summary>
		/// The item's longer description text for tooltips and store pages. Reads the schema's
		/// <c>"description"</c> property.
		/// </summary>
		/// <value>
		/// The localized description, or <c>null</c> if the schema does not define one. Like
		/// <see cref="Name"/>, this is localized to the player's Steam language.
		/// </value>
		public string Description => GetProperty( "description" );

		/// <summary>
		/// URL of the item's small icon, as hosted by Steam. Reads the schema's <c>"icon_url"</c>
		/// property. You have to download this yourself — the binding does not fetch images.
		/// </summary>
		/// <value>The icon URL, or <c>null</c> if the schema does not define one.</value>
		public string IconUrl => GetProperty( "icon_url" );

		/// <summary>
		/// URL of the item's large icon, for detail views where <see cref="IconUrl"/> would look
		/// soft. Reads the schema's <c>"icon_url_large"</c> property.
		/// </summary>
		/// <value>The large icon URL, or <c>null</c> if the schema does not define one.</value>
		public string IconUrlLarge => GetProperty( "icon_url_large" );

		/// <summary>
		/// The pricing bucket this item was assigned on the partner site, rather than a price
		/// itself. Reads the schema's <c>"price_category"</c> property. For an actual amount to
		/// display, use <see cref="LocalPrice"/> or <see cref="LocalPriceFormatted"/>.
		/// </summary>
		/// <value>The price category string, or <c>null</c> if the item is not sold.</value>
		public string PriceCategory => GetProperty( "price_category" );

		/// <summary>
		/// The schema's classification for this item, which drives how Steam treats it — the value
		/// <c>"generator"</c> marks an item that produces other items rather than being one. Reads
		/// the schema's <c>"type"</c> property.
		/// </summary>
		/// <value>
		/// The type string, or <c>null</c> if the schema does not define one. Unlike
		/// <see cref="Name"/> this is not localized, so it is safe to compare against.
		/// </value>
		public string Type => GetProperty( "type" );

		/// <summary>
		/// Whether this definition produces items rather than being one the player can hold —
		/// a loot table or drop list, not a hat. These are the definitions you pass to
		/// <see cref="SteamInventory.TriggerItemDropAsync"/>; do not show them in an inventory UI.
		/// </summary>
		/// <value>
		/// <c>true</c> when <see cref="Type"/> is exactly <c>"generator"</c>.
		/// </value>
		public bool IsGenerator => Type == "generator";

		/// <summary>
		/// The raw exchange rules for this item, exactly as written in the schema — the encoded
		/// list of ingredient combinations that can be traded in to produce it. Reads the schema's
		/// <c>"exchange"</c> property. Prefer <see cref="GetRecipes"/>, which parses this into
		/// something usable.
		/// </summary>
		/// <value>
		/// The encoded exchange string, or <c>null</c>/empty if this item cannot be crafted.
		/// The format is Valve's, not this binding's; treat it as opaque.
		/// </value>
		public string ExchangeSchema => GetProperty( "exchange" );

		/// <summary>
		/// The parsed list of ways this item can be crafted — each entry being a set of ingredients
		/// that Steam will accept in exchange for one of these. Use this to drive a crafting UI and
		/// to work out what to hand to <c>SteamInventory.CraftItemAsync</c>.
		/// </summary>
		/// <returns>
		/// The available recipes, or <c>null</c> if this item has no exchange rules configured and
		/// therefore cannot be crafted at all. Null is the common case — most items are not
		/// craftable — so check before enumerating.
		/// </returns>
		/// <remarks>
		/// Parsed from <see cref="ExchangeSchema"/> on every call, so cache the array rather than
		/// calling this per frame. Ingredients whose definition ID fails to parse are dropped
		/// silently, and an ingredient's <c>Definition</c> will be null if that definition is not
		/// in the loaded schema — load definitions first for the recipe to be fully resolved.
		/// </remarks>
		public InventoryRecipe[] GetRecipes()
		{
			if ( string.IsNullOrEmpty( ExchangeSchema ) ) return null;

			var parts = ExchangeSchema.Split( new[] { ';' }, StringSplitOptions.RemoveEmptyEntries );

			return parts.Select( x => InventoryRecipe.FromString( x, this ) ).ToArray();
		}

		/// <summary>
		/// Whether players are allowed to buy and sell this item on the Steam Community Market.
		/// Reads the schema's <c>"marketable"</c> property. This is enforced by Steam, so use it to
		/// decide what to show in your UI, not to grant permission.
		/// </summary>
		/// <value><c>true</c> if the item may be listed on the Community Market.</value>
		/// <remarks>
		/// Throws a <see cref="NullReferenceException"/> if the schema does not define this key at
		/// all — see <see cref="GetBoolProperty"/>. Guard with
		/// <c>GetProperty( "marketable" ) != null</c> if your schema is not guaranteed to set it.
		/// </remarks>
		public bool Marketable => GetBoolProperty( "marketable" );

		/// <summary>
		/// Whether this item can be traded to another player. Reads the schema's <c>"tradable"</c>
		/// property. Note that individual instances can additionally be locked even when the type
		/// permits trading — check <see cref="InventoryItem.IsNoTrade"/> on the actual item.
		/// </summary>
		/// <value><c>true</c> if the item type may be traded.</value>
		/// <remarks>
		/// Throws a <see cref="NullReferenceException"/> if the schema does not define this key at
		/// all — see <see cref="GetBoolProperty"/>.
		/// </remarks>
		public bool Tradable => GetBoolProperty( "tradable" );

		/// <summary>
		/// When this item <i>type</i> was added to the schema on the partner site. This describes
		/// the definition, not the player's copy — for when an owned item was obtained, use
		/// <see cref="InventoryItem.Acquired"/>.
		/// </summary>
		/// <value>
		/// The creation time from the schema's <c>"timestamp"</c> property, or
		/// <see cref="DateTime.MinValue"/> if the property is absent or cannot be parsed.
		/// </value>
		public DateTime Created => GetProperty<DateTime>( "timestamp" );

		/// <summary>
		/// When this item type was last edited on the partner site. Useful for cache invalidation
		/// if you mirror the schema into your own backend.
		/// </summary>
		/// <value>
		/// The modification time from the schema's <c>"modified"</c> property, or
		/// <see cref="DateTime.MinValue"/> if the property is absent or cannot be parsed.
		/// </value>
		public DateTime Modified => GetProperty<DateTime>( "modified" );

		/// <summary>
		/// Reads any property off this item's schema entry by key, including custom keys your game
		/// defines on the partner site. This is the general escape hatch that all the named
		/// shortcuts on this class are built on — use it for anything the schema declares that
		/// this binding does not surface directly, such as your own rarity or slot fields.
		/// </summary>
		/// <param name="name">
		/// The schema key to read. Valve restricts property names to ASCII letters, numbers and
		/// underscores. Passing <c>null</c> is documented by Valve to return a comma-separated
		/// list of the available key names instead of a value — but see the remarks, because in
		/// this binding that only works on a freshly constructed definition.
		/// </param>
		/// <returns>
		/// The property value, or <c>null</c> if the schema does not define that key for this item
		/// (which is also what you get for every key when no schema is configured on the
		/// Steamworks partner site).
		/// </returns>
		/// <remarks>
		/// <para>
		/// Values are cached on this instance after the first read, so repeated access is free but
		/// a schema change during the session will not be picked up by an existing definition
		/// object.
		/// </para>
		/// <para>
		/// The cache is consulted before the <c>null</c>-name special case is handled, so once any
		/// property has been read on this instance, passing <c>null</c> throws
		/// <see cref="ArgumentNullException"/> from the dictionary lookup rather than returning the
		/// key list. Read the key list first if you need it.
		/// </para>
		/// </remarks>
		public string GetProperty( string name )
		{
			if ( _properties!= null && _properties.TryGetValue( name, out string val ) )
				return val;

			if ( !SteamInventory.Internal.GetItemDefinitionProperty( Id, name, out var vl ) )
				return null;
				
			if (name == null) //return keys string
				return vl;
				
			if ( _properties == null )
				_properties = new Dictionary<string, string>();

			_properties[name] = vl;

			return vl;
		}

		/// <summary>
		/// Reads a schema property and interprets it as a boolean flag. Schema values are always
		/// strings, so this applies Steam's loose truthiness convention for you — use it for your
		/// own custom yes/no keys, the way <see cref="Tradable"/> and <see cref="Marketable"/> use
		/// it for Valve's.
		/// </summary>
		/// <param name="name">The schema key to read.</param>
		/// <returns>
		/// <c>false</c> if the value is empty or begins with <c>'0'</c>, <c>'F'</c> or <c>'f'</c>;
		/// <c>true</c> for anything else. Note that any unexpected value therefore reads as
		/// <c>true</c>, so do not rely on this to validate schema content.
		/// </returns>
		/// <remarks>
		/// If the key is absent from the schema the underlying read returns <c>null</c> and this
		/// method throws <see cref="NullReferenceException"/> rather than returning <c>false</c>.
		/// Check <see cref="GetProperty(string)"/> for null first when the key is optional.
		/// </remarks>
		public bool GetBoolProperty( string name )
		{
			string val = GetProperty( name );

			if ( val.Length == 0 ) return false;
			if ( val[0] == '0' || val[0] == 'F' || val[0] == 'f' ) return false;

			return true;
		}

		/// <summary>
		/// Reads a schema property and converts it to the type you ask for. Saves you parsing the
		/// raw string yourself for numeric or date-valued custom keys.
		/// </summary>
		/// <typeparam name="T">
		/// The type to convert to. Must be something <c>Convert.ChangeType</c> understands — the
		/// primitives, <c>string</c>, <c>decimal</c> and <see cref="DateTime"/>. Arbitrary types
		/// will not work.
		/// </typeparam>
		/// <param name="name">The schema key to read.</param>
		/// <returns>
		/// The converted value, or <c>default</c> for <typeparamref name="T"/> if the key is
		/// missing, empty, or the value cannot be converted.
		/// </returns>
		/// <remarks>
		/// Conversion failures are swallowed and reported as <c>default</c>, so a returned
		/// <c>0</c> or <see cref="DateTime.MinValue"/> is ambiguous — it could mean the schema
		/// genuinely says zero, that the key is absent, or that the value was malformed. If that
		/// distinction matters, read the raw string with <see cref="GetProperty(string)"/> and
		/// parse it yourself.
		/// </remarks>
		public T GetProperty<T>( string name )
		{
			string val = GetProperty( name );

			if ( string.IsNullOrEmpty( val ) )
				return default;

			try
			{
				return (T)Convert.ChangeType( val, typeof( T ) );
			}
			catch ( System.Exception )
			{
				return default;
			}
		}

		/// <summary>
		/// Every schema property on this item type as key/value pairs. Handy for debugging what the
		/// partner site actually published, or for generic UI that renders whatever fields a
		/// designer added without the code knowing their names.
		/// </summary>
		/// <value>
		/// A lazily evaluated sequence of the item's properties. Enumerating it issues one native
		/// call per key on top of the call that fetches the key list, so materialize it once rather
		/// than enumerating repeatedly.
		/// </value>
		/// <remarks>
		/// This works by asking for the key list with a <c>null</c> property name, which means it
		/// must be the first property access on a given definition instance. If any property has
		/// already been read — including through <see cref="Name"/> or any other shortcut —
		/// enumerating this throws <see cref="ArgumentNullException"/>. See
		/// <see cref="GetProperty(string)"/>.
		/// </remarks>
		public IEnumerable<KeyValuePair<string, string>> Properties
		{
			get
			{
				var list = GetProperty( null );
				var keys = list.Split( ',' );

				foreach ( var key in keys )
				{
					yield return new KeyValuePair<string, string>( key, GetProperty( key ) );
				}
			}
		}

		/// <summary>
		/// What this item currently costs the player, in the currency named by
		/// <see cref="SteamInventory.Currency"/>. This is the figure to show on a buy button, and
		/// the one that reflects any active discount.
		/// </summary>
		/// <value>
		/// The current price in <b>minor currency units</b> — cents, pence, and so on — so
		/// <c>499</c> means 4.99. Returns <c>0</c> if Steam has no price stored for this item, or
		/// if prices have not been requested yet. Zero therefore means "not for sale or not loaded",
		/// never "free".
		/// </value>
		/// <remarks>
		/// <para>
		/// You must await <see cref="SteamInventory.GetDefinitionsWithPricesAsync"/> before this
		/// returns anything — Valve requires an explicit price request first, and until then every
		/// item reads as zero.
		/// </para>
		/// <para>
		/// Valve's header says only that prices are "in the user's local currency" and does not
		/// state the unit. The minor-units reading is inferred from this binding, which divides by
		/// 100 when producing <see cref="LocalPriceFormatted"/>. That divisor is also a fixed
		/// assumption: currencies with no minor unit, such as JPY, will be formatted incorrectly.
		/// </para>
		/// </remarks>
		public int LocalPrice
		{
			get
			{
				ulong curprice = 0;
				ulong baseprice = 0;

				if ( !SteamInventory.Internal.GetItemPrice( Id, ref curprice, ref baseprice ) )
					return 0;

				return (int) curprice;
			}
		}

		/// <summary>
		/// <see cref="LocalPrice"/> rendered as a display string with the right currency symbol
		/// and placement for <see cref="SteamInventory.Currency"/>, for example <c>"£4.99"</c>.
		/// Use this rather than formatting the integer yourself.
		/// </summary>
		/// <value>
		/// The formatted price. Always shows two decimal places, so currencies without a minor
		/// unit are rendered incorrectly. If <see cref="SteamInventory.Currency"/> is null — that
		/// is, prices were never requested — the number is returned with no currency marker at all.
		/// </value>
		public string LocalPriceFormatted => Utility.FormatPrice( SteamInventory.Currency, LocalPrice / 100.0 );

		/// <summary>
		/// The item's undiscounted list price, in the same minor currency units as
		/// <see cref="LocalPrice"/>. Compare the two to detect a sale: when they differ, the item
		/// is discounted, and showing this struck through next to
		/// <see cref="LocalPriceFormatted"/> is the usual presentation.
		/// </summary>
		/// <value>
		/// The base price in minor currency units, or <c>0</c> if Steam has no price for this item
		/// or prices have not been requested yet.
		/// </value>
		/// <remarks>
		/// Valve does not document the relationship between the two price values it returns; that
		/// this one is the pre-discount price is inferred from naming and from how this binding
		/// uses it. Requires <see cref="SteamInventory.GetDefinitionsWithPricesAsync"/> to have
		/// completed, like <see cref="LocalPrice"/>.
		/// </remarks>
		public int LocalBasePrice
		{
			get
			{
				ulong curprice = 0;
				ulong baseprice = 0;

				if ( !SteamInventory.Internal.GetItemPrice( Id, ref curprice, ref baseprice ) )
					return 0;

				return (int)baseprice;
			}
		}

		/// <summary>
		/// The undiscounted list price as a display string, intended as the struck-through "was"
		/// figure alongside <see cref="LocalPriceFormatted"/>.
		/// </summary>
		/// <value>
		/// A formatted price string in <see cref="SteamInventory.Currency"/>.
		/// </value>
		/// <remarks>
		/// Divides by a fixed 100, so like <see cref="LocalPriceFormatted"/> it is wrong for
		/// zero-decimal currencies such as JPY. When the item is not discounted this renders the
		/// same text as <see cref="LocalPriceFormatted"/>, because base and current price are equal.
		/// </remarks>
		public string LocalBasePriceFormatted => Utility.FormatPrice( SteamInventory.Currency, LocalBasePrice / 100.0 );

		InventoryRecipe[] _recContaining;

		/// <summary>
		/// Every recipe across the whole schema that uses this item as an ingredient — the inverse
		/// of <see cref="GetRecipes"/>. Use it to answer "what can I make with this?" from an
		/// item's detail view.
		/// </summary>
		/// <returns>
		/// The recipes that consume this item. Empty if nothing is crafted from it. The array is
		/// computed once and cached on this instance, so later calls are free but will not reflect
		/// a schema change.
		/// </returns>
		/// <remarks>
		/// The first call scans every definition in <see cref="SteamInventory.Definitions"/> and
		/// parses each one's exchange rules, so it is expensive — call it once per item, not per
		/// frame. It also dereferences <see cref="SteamInventory.Definitions"/> directly, so it
		/// throws <see cref="NullReferenceException"/> if the schema has not been loaded. Await
		/// <see cref="SteamInventory.WaitForDefinitions"/> first.
		/// </remarks>
		public InventoryRecipe[] GetRecipesContainingThis()
		{
			if ( _recContaining != null ) return _recContaining;

			var allRec = SteamInventory.Definitions
							.Select( x => x.GetRecipes() )
							.Where( x => x != null ) 
							.SelectMany( x => x );

			_recContaining = allRec.Where( x => x.ContainsIngredient( this ) ).ToArray();
			return _recContaining;
		}

		/// <summary>
		/// Compares two definitions by <see cref="Id"/>, so two objects wrapping the same item type
		/// are equal even though they are separate instances. Null-safe on both sides.
		/// </summary>
		/// <param name="a">The first definition, or <c>null</c>.</param>
		/// <param name="b">The second definition, or <c>null</c>.</param>
		/// <returns><c>true</c> if both are null, or both describe the same item type.</returns>
		public static bool operator ==( InventoryDef a, InventoryDef b )
		{
			if ( Object.ReferenceEquals( a, null ) )
				return Object.ReferenceEquals( b, null );

			return a.Equals( b );
		}

		/// <summary>
		/// The inverse of the equality operator: true when the two definitions describe different
		/// item types.
		/// </summary>
		/// <param name="a">The first definition, or <c>null</c>.</param>
		/// <param name="b">The second definition, or <c>null</c>.</param>
		/// <returns><c>true</c> if they are not the same item type.</returns>
		public static bool operator !=( InventoryDef a, InventoryDef b ) => !(a == b);

		/// <summary>
		/// Compares this definition to an arbitrary object by item type.
		/// </summary>
		/// <param name="p">The object to compare against.</param>
		/// <returns><c>true</c> if it is a definition describing the same item type.</returns>
		/// <remarks>
		/// This casts rather than type-testing, so passing an object that is not an
		/// <see cref="InventoryDef"/> throws <see cref="InvalidCastException"/> instead of
		/// returning <c>false</c>. Prefer the strongly typed overload.
		/// </remarks>
		public override bool Equals( object p ) => this.Equals( (InventoryDef)p );

		/// <summary>
		/// Hash code derived from <see cref="Id"/>, so definitions can be used as dictionary keys
		/// and set members keyed on item type.
		/// </summary>
		/// <returns>The hash code of the definition ID.</returns>
		public override int GetHashCode() => Id.GetHashCode();

		/// <summary>
		/// Compares this definition to another by item type.
		/// </summary>
		/// <param name="p">The definition to compare against, or <c>null</c>.</param>
		/// <returns>
		/// <c>true</c> if <paramref name="p"/> is non-null and has the same <see cref="Id"/>.
		/// </returns>
		public bool Equals( InventoryDef p )
		{
			if ( p == null ) return false;
			return p.Id == Id;
		}

	}
}
