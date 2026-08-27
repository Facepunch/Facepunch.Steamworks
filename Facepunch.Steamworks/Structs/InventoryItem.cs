using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Steamworks.Data;

namespace Steamworks
{
	/// <summary>
	/// One item the player actually owns — a specific hat in their inventory, or a specific stack
	/// of wood. This is the instance; the item <i>type</i> behind it is an
	/// <see cref="InventoryDef"/>, reachable through <see cref="Def"/>.
	/// </summary>
	/// <remarks>
	/// <para>
	/// This is a snapshot taken when the containing <see cref="InventoryResult"/> was read, not a
	/// live view. Consuming, splitting or trading an item elsewhere does not update a copy you are
	/// holding, and because this is a struct, assigning it copies the snapshot. Re-read the
	/// inventory to see current state.
	/// </para>
	/// <para>
	/// <see cref="Properties"/> is only populated if you explicitly asked for properties when
	/// reading the result. Items handed to you through <see cref="SteamInventory.Items"/> never
	/// have them, which silently degrades <see cref="Origin"/> and <see cref="Acquired"/>.
	/// </para>
	/// <para>
	/// <see cref="Def"/> resolves against the loaded item schema, so it is null unless item
	/// definitions were loaded first — see <see cref="SteamInventory.WaitForDefinitions"/>.
	/// </para>
	/// </remarks>
	/// <example>
	/// <code>
	/// var result = await SteamInventory.GetAllItemsAsync();
	/// if ( !result.HasValue ) return;
	///
	/// using ( var inventory = result.Value )
	/// {
	///     foreach ( var item in inventory.GetItems( true ) ?? Array.Empty&lt;InventoryItem&gt;() )
	///     {
	///         Console.WriteLine( $"{item.Def?.Name} x{item.Quantity} from {item.Origin}" );
	///
	///         if ( item.IsNoTrade )
	///             Console.WriteLine( "  locked to this account" );
	///     }
	/// }
	/// </code>
	/// </example>
	public struct InventoryItem : IEquatable<InventoryItem>
	{
		internal InventoryItemId _id;
		internal InventoryDefId _def;
		internal SteamItemFlags _flags;
		internal ushort _quantity;
		internal Dictionary<string, string> _properties;

		/// <summary>
		/// The globally unique instance ID of this particular item. Valve guarantees it identifies
		/// the combination of player and item instance, is never transferred to another player, and
		/// is never reused — so it is the right thing to persist if your backend needs to refer to
		/// one specific copy of an item.
		/// </summary>
		/// <value>The instance ID. Use this for identity; use <see cref="DefId"/> for "what is it".</value>
		public InventoryItemId Id => _id;

		/// <summary>
		/// Which item type this is, as a schema definition ID. Two different hats the player owns
		/// share a <see cref="DefId"/> but have different <see cref="Id"/> values.
		/// </summary>
		/// <value>The definition ID, resolvable through <see cref="SteamInventory.FindDefinition"/>.</value>
		public InventoryDefId DefId => _def;

		/// <summary>
		/// How many units are in this stack. Stackable items such as currency or crafting materials
		/// arrive as one entry with a quantity rather than as many entries.
		/// </summary>
		/// <value>
		/// The stack size. Steam carries this as a 16-bit value, so it cannot exceed 65535 however
		/// it is exposed here.
		/// </value>
		public int Quantity => _quantity;

		/// <summary>
		/// The schema entry describing this item — its name, icons, and whether it can be traded.
		/// This is what you read to display the item.
		/// </summary>
		/// <value>
		/// The matching <see cref="InventoryDef"/>, or <c>null</c> if item definitions have not
		/// been loaded, or if this item's type is not in the loaded schema. A null here almost
		/// always means the schema was not loaded before the inventory was read — await
		/// <see cref="SteamInventory.WaitForDefinitions"/> at startup.
		/// </value>
		/// <remarks>
		/// Looked up on every access rather than cached, though the lookup itself is a dictionary
		/// hit.
		/// </remarks>
		public InventoryDef Def => SteamInventory.FindDefinition( DefId );


		/// <summary>
		/// The item's dynamic per-instance properties — things that vary between two copies of the
		/// same item type, such as where it came from or when it was obtained. Distinct from
		/// <see cref="InventoryDef"/> properties, which describe the type and are shared by every
		/// copy.
		/// </summary>
		/// <value>
		/// The properties, or <c>null</c> if the result this item came from was read without
		/// requesting them. This is not an error condition and not an empty dictionary — it is
		/// null, so check before indexing.
		/// </value>
		/// <remarks>
		/// Properties are opt-in because each one costs a native call per item. They are populated
		/// only by <c>InventoryResult.GetItems( true )</c> or the overload taking explicit property
		/// names. Items reached through <see cref="SteamInventory.Items"/> are read without
		/// properties, so this is always null there.
		/// </remarks>
		public Dictionary<string, string> Properties => _properties;

		/// <summary>
		/// This item is account-locked and cannot be traded or given away. 
		/// This is an item status flag which is permanently attached to specific item instances
		/// </summary>
		public bool IsNoTrade => _flags.HasFlag( SteamItemFlags.NoTrade );

		/// <summary>
		/// The item has been destroyed, traded away, expired, or otherwise invalidated. 
		/// This is an action confirmation flag which is only set one time, as part of a result set.
		/// </summary>
		public bool IsRemoved => _flags.HasFlag( SteamItemFlags.Removed );

		/// <summary>
		/// The item quantity has been decreased by 1 via ConsumeItem API. 
		/// This is an action confirmation flag which is only set one time, as part of a result set.
		/// </summary>
		public bool IsConsumed => _flags.HasFlag( SteamItemFlags.Consumed );

		/// <summary>
		/// <b>Permanently destroys units of this item. There is no undo and no recovery.</b> When
		/// the stack reaches zero the item is gone from the player's account for good. Use this for
		/// genuinely consumable things — potions, keys, ammo crates — and put a deliberate,
		/// high-friction confirmation in front of it.
		/// </summary>
		/// <param name="amount">
		/// How many units to destroy from this stack. Sent to Steam as an unsigned count, so a
		/// negative value does not fail cleanly — it wraps to an enormous number. Validate it, and
		/// bound it by <see cref="Quantity"/>.
		/// </param>
		/// <returns>
		/// A result whose items carry the confirmation flags for what happened — check
		/// <see cref="IsConsumed"/> and <see cref="IsRemoved"/> on them to verify the outcome
		/// rather than assuming. <c>null</c> if Steam rejected the call outright. The result owns a
		/// native handle and must be disposed.
		/// </returns>
		/// <remarks>
		/// <para>
		/// Valve's warning is worth repeating: if your game removes items at all, a confirmation
		/// step is strongly recommended, because the support case this creates is "my brother
		/// borrowed my laptop and deleted all of my rare items".
		/// </para>
		/// <para>
		/// You can restrict this to particular item definitions, or block it entirely, from the
		/// Steamworks partner site — which is the safer place to enforce it than in client code a
		/// hacked client can skip.
		/// </para>
		/// </remarks>
		/// <example>
		/// <code>
		/// if ( !await ConfirmWithPlayer( $"Permanently destroy 1 {item.Def?.Name}?" ) )
		///     return;
		///
		/// var result = await item.ConsumeAsync();
		/// if ( !result.HasValue )
		///     return;
		///
		/// using ( var r = result.Value )
		/// {
		///     var consumed = r.GetItems()?.Any( x =&gt; x.IsConsumed || x.IsRemoved ) == true;
		///     Console.WriteLine( consumed ? "Item consumed." : "Nothing was consumed." );
		/// }
		/// </code>
		/// </example>
		public async Task<InventoryResult?> ConsumeAsync( int amount = 1 )
		{
			var sresult = Defines.k_SteamInventoryResultInvalid;
			if ( !SteamInventory.Internal.ConsumeItem( ref sresult, Id, (uint)amount ) )
				return null;

			return await InventoryResult.GetAsync( sresult );
		}

		/// <summary>
		/// Peels units off this stack into a brand new item instance, leaving the remainder here.
		/// Use it when the player wants to move or trade part of a stack — say, half their wood —
		/// without giving up the whole thing.
		/// </summary>
		/// <param name="quantity">
		/// How many units to move into the new stack. Sent unsigned, so negative values wrap rather
		/// than failing; keep it between 1 and <see cref="Quantity"/> minus one.
		/// </param>
		/// <returns>
		/// A result describing the affected items, including the newly created stack, or
		/// <c>null</c> if Steam rejected the call. The result owns a native handle and must be
		/// disposed.
		/// </returns>
		/// <remarks>
		/// Implemented as a quantity transfer to an invalid destination instance, which is Valve's
		/// documented way of saying "make a new stack". Neither this item nor the new one is
		/// reflected in the copy of <see cref="InventoryItem"/> you called this on — it is a
		/// snapshot, so re-read the inventory afterwards.
		/// </remarks>
		public async Task<InventoryResult?> SplitStackAsync( int quantity = 1 )
		{
			var sresult = Defines.k_SteamInventoryResultInvalid;
			if ( !SteamInventory.Internal.TransferItemQuantity( ref sresult, Id, (uint)quantity, ulong.MaxValue ) )
				return null;

			return await InventoryResult.GetAsync( sresult );
		}

		/// <summary>
		/// Merges units from another stack into this one — the inverse of
		/// <see cref="SplitStackAsync"/>. Use it to tidy an inventory that has ended up with the
		/// same material spread across several stacks.
		/// </summary>
		/// <param name="add">
		/// The stack to take units from. Must be the same item type as this one; Valve only
		/// supports transferring quantity between stacks of identical items, and the transfer fails
		/// otherwise.
		/// </param>
		/// <param name="quantity">
		/// How many units to move out of <paramref name="add"/> and into this item. Sent unsigned,
		/// so negative values wrap rather than failing. Moving the source's entire quantity leaves
		/// it empty and it ceases to exist.
		/// </param>
		/// <returns>
		/// A result describing the affected items, or <c>null</c> if Steam rejected the call. The
		/// result owns a native handle and must be disposed.
		/// </returns>
		/// <remarks>
		/// Note the direction: units flow <i>from</i> <paramref name="add"/> <i>into</i> the item
		/// you called this on. Both snapshots are stale afterwards — re-read the inventory to see
		/// the merged stack.
		/// </remarks>
		public async Task<InventoryResult?> AddAsync( InventoryItem add, int quantity = 1 )
		{
			var sresult = Defines.k_SteamInventoryResultInvalid;
			if ( !SteamInventory.Internal.TransferItemQuantity( ref sresult, add.Id, (uint)quantity, Id ) )
				return null;

			return await InventoryResult.GetAsync( sresult );
		}


		internal static InventoryItem From( SteamItemDetails_t details )
		{
			var i = new InventoryItem
			{
				_id = details.ItemId,
				_def = details.Definition,
				_flags = (SteamItemFlags) details.Flags,
				_quantity = details.Quantity
			};

			return i;
		}

		internal static Dictionary<string, string> GetProperties( SteamInventoryResult_t result, int index )
		{
			if ( !SteamInventory.Internal.GetResultItemProperty( result, (uint)index, null, out var propNames ) )
				return null;

			var props = new Dictionary<string, string>();

			foreach ( var propertyName in propNames.Split( ',' ) )
			{
				if ( SteamInventory.Internal.GetResultItemProperty( result, (uint)index, propertyName, out var strVal ) )
				{
					props.Add( propertyName, strVal );
				}
			}

			return props;
		}

		internal static Dictionary<string, string> GetProperties( SteamInventoryResult_t result, int index, string[] propertyNames )
		{
			// Only fetch the requested properties. This skips the "list all property names" call and avoids reading properties we don't care about
			// Each property is a separate native call, so for large result sets it can't be super slow
			var props = new Dictionary<string, string>( propertyNames.Length );

			foreach ( var propertyName in propertyNames )
			{
				if ( SteamInventory.Internal.GetResultItemProperty( result, (uint)index, propertyName, out var strVal ) )
				{
					props.Add( propertyName, strVal );
				}
			}

			return props;
		}

		/// <summary>
		/// When the player obtained this particular item, parsed from its <c>"acquired"</c>
		/// property. Useful for "new item" badges and for sorting an inventory by recency.
		/// </summary>
		/// <value>
		/// The acquisition time in UTC.
		/// </value>
		/// <remarks>
		/// <para>
		/// <b>This silently returns the current time when it cannot answer properly.</b> If
		/// <see cref="Properties"/> is null — which is the case for every item from
		/// <see cref="SteamInventory.Items"/>, and for any result read without properties — or if
		/// the property is missing, you get <see cref="DateTime.UtcNow"/> rather than an error or a
		/// sentinel. Every item will therefore look brand new. Read the inventory with properties
		/// if this value matters, and treat "acquired just now" with suspicion.
		/// </para>
		/// <para>
		/// The stored value is parsed positionally rather than with a date parser, so a value that
		/// is not in Steam's expected fixed-width timestamp layout throws rather than falling back.
		/// </para>
		/// </remarks>
		public DateTime Acquired
		{
			get
			{
				if ( Properties == null ) return DateTime.UtcNow;

				if ( Properties.TryGetValue( "acquired", out var str ) )
				{
					var y = int.Parse( str.Substring( 0, 4 ) );
					var m = int.Parse( str.Substring( 4, 2 ) );
					var d = int.Parse( str.Substring( 6, 2 ) );

					var h = int.Parse( str.Substring( 9, 2 ) );
					var mn = int.Parse( str.Substring( 11, 2 ) );
					var s = int.Parse( str.Substring( 13, 2 ) );

					return new DateTime( y, m, d, h, mn, s, DateTimeKind.Utc );
				}

				return DateTime.UtcNow;
			}
		}

		/// <summary>
		/// How the player came by this item — bought on the Community Market, dropped in game,
		/// granted as a promo, crafted, and so on. Read from the <c>"origin"</c> property. Useful
		/// for provenance UI, and for rules like "market-bought items do not count towards this
		/// achievement".
		/// </summary>
		/// <value>
		/// An origin string such as <c>"market"</c>, or <c>null</c> if <see cref="Properties"/> was
		/// not loaded or the property is absent. Because a missing property and an unpopulated
		/// result both give null, null does not tell you the item has no origin — check
		/// <see cref="Properties"/> for null first to tell the two apart.
		/// </value>
		/// <remarks>
		/// The set of possible values is Valve's and is not enumerated in the SDK header, so treat
		/// unknown strings as valid rather than assuming a fixed list.
		/// </remarks>
		public string Origin
		{
			get
			{
				if ( Properties == null ) return null;
				
				if ( Properties.TryGetValue( "origin", out var str ) )
					return str;

				return null;
			}
		}

		/// <summary>
		/// Pairs an item with how many units of it to use. Needed wherever an operation has to act
		/// on part of a stack rather than the whole thing — most notably the
		/// <c>SteamInventory.CraftItemAsync</c> overload that takes explicit quantities.
		/// </summary>
		/// <example>
		/// <code>
		/// var ingredients = new[]
		/// {
		///     new InventoryItem.Amount { Item = wood,  Quantity = 4 },
		///     new InventoryItem.Amount { Item = nails, Quantity = 12 },
		/// };
		///
		/// var result = await SteamInventory.CraftItemAsync( ingredients, tableDef );
		/// </code>
		/// </example>
		public struct Amount
		{
			/// <summary>
			/// The stack to take units from.
			/// </summary>
			public InventoryItem Item;

			/// <summary>
			/// How many units of <see cref="Item"/> to use. Must not exceed that item's
			/// <see cref="InventoryItem.Quantity"/>, and must not be negative — quantities are sent
			/// to Steam unsigned, so a negative value wraps to an enormous number instead of
			/// failing cleanly.
			/// </summary>
			public int Quantity;
		}

		/// <summary>
		/// Compares two items by instance ID, so this asks "are these the same physical item",
		/// not "are these the same kind of item". Compare <see cref="DefId"/> for the latter.
		/// </summary>
		/// <param name="a">The first item.</param>
		/// <param name="b">The second item.</param>
		/// <returns><c>true</c> if both refer to the same item instance.</returns>
		public static bool operator ==( InventoryItem a, InventoryItem b ) => a._id == b._id;

		/// <summary>
		/// The inverse of the equality operator: true when the two values refer to different item
		/// instances.
		/// </summary>
		/// <param name="a">The first item.</param>
		/// <param name="b">The second item.</param>
		/// <returns><c>true</c> if they are different item instances.</returns>
		public static bool operator !=( InventoryItem a, InventoryItem b ) => a._id != b._id;

		/// <summary>
		/// Compares this item to an arbitrary object by instance ID.
		/// </summary>
		/// <param name="p">The object to compare against.</param>
		/// <returns><c>true</c> if it is the same item instance.</returns>
		/// <remarks>
		/// This casts rather than type-testing, so passing anything that is not an
		/// <see cref="InventoryItem"/> — including <c>null</c> — throws instead of returning
		/// <c>false</c>. Prefer the strongly typed overload.
		/// </remarks>
		public override bool Equals( object p ) => this.Equals( (InventoryItem)p );

		/// <summary>
		/// Hash code derived from the instance ID, so items can be used as dictionary keys and set
		/// members keyed on the specific item.
		/// </summary>
		/// <returns>The hash code of the item instance ID.</returns>
		public override int GetHashCode() => _id.GetHashCode();

		/// <summary>
		/// Compares this item to another by instance ID.
		/// </summary>
		/// <param name="p">The item to compare against.</param>
		/// <returns><c>true</c> if both refer to the same item instance.</returns>
		public bool Equals( InventoryItem p ) => p._id == _id;
	}
}
