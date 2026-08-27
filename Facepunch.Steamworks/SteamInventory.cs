using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Steamworks.Data;

namespace Steamworks
{
	/// <summary>
	/// Steam's server-authoritative item backend: the set of item instances the local user owns
	/// for this app, plus the item schema (the "definitions") those instances point at. This is
	/// what you use for cosmetics, consumables, crafting materials, timed drops and
	/// microtransaction-purchased items — anything the player owns that has to survive a
	/// reinstall, or that has to be provable to other players.
	/// </summary>
	/// <remarks>
	/// <para>
	/// <b>None of this does anything until an item schema is configured on the Steamworks partner
	/// site</b> (Steamworks admin, your app, Inventory Service). With no schema published, the
	/// calls in this class do not throw and do not report an error — they succeed and return
	/// nothing. <see cref="Definitions"/> stays null, <see cref="Items"/> stays null,
	/// <see cref="FindDefinition"/> returns null forever, and every async call here returns a
	/// result containing zero items. If you are looking at "no items, no error", check the
	/// partner site before you debug this code. Valve documents this prerequisite only in
	/// passing, so it is a common first-time trap.
	/// </para>
	/// <para>
	/// Startup order that actually works: await <see cref="WaitForDefinitions"/> (or call
	/// <see cref="LoadItemDefinitions"/> and wait for <see cref="OnDefinitionsUpdated"/>) so the
	/// schema is present, and only then read the player's items with
	/// <see cref="GetAllItemsAsync"/>. If you read items first,
	/// <see cref="InventoryItem.Def"/> resolves to null on every item, because it looks the
	/// definition up in the schema cache this class populates.
	/// </para>
	/// <para>
	/// Every async method here returns a nullable <see cref="InventoryResult"/> that owns a
	/// native handle. Valve requires that handle to be destroyed when you are done with it,
	/// which in this binding means disposing the result — see <see cref="InventoryResult"/>.
	/// A null return means the underlying call was rejected outright (bad parameters, inventory
	/// service unavailable, or the API not being initialized); it is not the same as a result
	/// that succeeded with zero items.
	/// </para>
	/// <para>
	/// These calls only complete while Steam callbacks are being pumped. This binding polls the
	/// result status on a short timer, but the callbacks that populate schema and inventory
	/// updates still require your regular <c>SteamClient.RunCallbacks</c> loop to be running.
	/// </para>
	/// <para>
	/// Several operations here are irreversible and hit the live inventory service:
	/// <c>InventoryItem.ConsumeAsync</c> destroys items, <c>CraftItemAsync</c> destroys the
	/// inputs, and <see cref="GenerateItemAsync"/> mints items out of nothing. Treat them as
	/// you would a destructive database write, not as a local state change.
	/// </para>
	/// </remarks>
	/// <example>
	/// Read the player's inventory at startup:
	/// <code>
	/// // 1. Load the item schema from the partner site. Without this, Def is null on every item.
	/// if ( !await SteamInventory.WaitForDefinitions() )
	/// {
	///     Console.WriteLine( "No item schema — is the Inventory Service configured for this app?" );
	///     return;
	/// }
	///
	/// // 2. Ask what the player owns.
	/// var result = await SteamInventory.GetAllItemsAsync();
	/// if ( !result.HasValue )
	///     return; // the call was rejected, not "the player owns nothing"
	///
	/// // 3. The result holds a native handle — dispose it or you leak.
	/// using ( var inventory = result.Value )
	/// {
	///     var items = inventory.GetItems();
	///     if ( items == null )
	///         return; // empty inventory, or no schema configured
	///
	///     foreach ( var item in items )
	///         Console.WriteLine( $"{item.Def?.Name} x{item.Quantity}" );
	/// }
	/// </code>
	/// </example>
	public class SteamInventory : SteamSharedClass<SteamInventory>
	{
		internal static ISteamInventory Internal => Interface as ISteamInventory;

		internal override bool InitializeInterface( bool server )
		{
			SetInterface( server, new ISteamInventory( server ) );
			if ( Interface.Self == IntPtr.Zero ) return false;

			InstallEvents( server );

			return true;
		}
	
		internal static void InstallEvents( bool server )
		{
			if ( !server )
			{
				Dispatch.Install<SteamInventoryFullUpdate_t>( x => InventoryUpdated( x ) );
			}

			Dispatch.Install<SteamInventoryDefinitionUpdate_t>( x => LoadDefinitions(), server );
		}

		private static void InventoryUpdated( SteamInventoryFullUpdate_t x )
		{
			var r = new InventoryResult( x.Handle, false );
			Items = r.GetItems( false );

			OnInventoryUpdated?.Invoke( r );
		}

		/// <summary>
		/// Raised when Steam pushes a fresh, complete snapshot of the local user's inventory —
		/// typically after a <see cref="GetAllItems"/> call, a purchase completing, or a drop
		/// landing. Hook this instead of polling if you want your UI to react to the player
		/// gaining or losing items. <see cref="Items"/> has already been refreshed by the time
		/// your handler runs.
		/// </summary>
		/// <remarks>
		/// Valve only fires this when the snapshot is genuinely newer than the last known one —
		/// it will not fire if the inventory has not changed, and it will not fire for a stale
		/// result that arrived out of order behind a newer one. So this is an "something
		/// actually changed" signal, not a "the call finished" signal.
		/// <para>
		/// Valve does not document who owns the result handle delivered with this callback. This
		/// binding wraps it in an <see cref="InventoryResult"/> and does not dispose it, so do
		/// not dispose it in your handler either, and do not hold onto it after the handler
		/// returns.
		/// </para>
		/// </remarks>
		public static event Action<InventoryResult> OnInventoryUpdated;

		/// <summary>
		/// Raised once the item schema has been downloaded and <see cref="Definitions"/> is
		/// populated. This is the signal that <see cref="FindDefinition"/> and
		/// <see cref="InventoryItem.Def"/> will start returning real data.
		/// </summary>
		/// <remarks>
		/// Fires in response to <see cref="LoadItemDefinitions"/>, and also whenever Steam
		/// refreshes definitions on its own — new item types can be added on the partner site
		/// while players are in-game, so this can fire more than once per session. If it never
		/// fires at all, the app most likely has no item schema configured on the Steamworks
		/// partner site.
		/// </remarks>
		public static event Action OnDefinitionsUpdated;

		static void LoadDefinitions()
		{
			Definitions = GetDefinitions();

			if ( Definitions == null )
				return;

			_defMap = new Dictionary<int, InventoryDef>();

			foreach ( var d in Definitions )
			{
				_defMap[d.Id] = d;
			}

			OnDefinitionsUpdated?.Invoke();
		}


		/// <summary>
		/// Downloads the item schema — the definition of every item type your app has — so that
		/// <see cref="Definitions"/>, <see cref="FindDefinition"/> and
		/// <see cref="InventoryItem.Def"/> return real data. Call this once at startup before you
		/// read the player's items; you only need to call it again if you expect the schema to
		/// change mid-session. Completion is signalled by <see cref="OnDefinitionsUpdated"/>.
		/// </summary>
		/// <remarks>
		/// <para>
		/// This is the call that silently does nothing when the app has no item schema configured
		/// on the Steamworks partner site. It will not throw and it will not report failure —
		/// <see cref="OnDefinitionsUpdated"/> simply never fires with any content and
		/// <see cref="Definitions"/> stays null. If your items never show up, verify the
		/// Inventory Service schema on the partner site first.
		/// </para>
		/// <para>
		/// Before making the native call, this binding first tries to populate
		/// <see cref="Definitions"/> synchronously from whatever Steam already has locally, so
		/// you may get usable data immediately even while the fresh copy is still downloading.
		/// Valve does not document that a local cache exists; this is the binding's own
		/// behaviour and it is why <see cref="Definitions"/> is sometimes non-null the instant
		/// you call this.
		/// </para>
		/// <para>
		/// Steam also refreshes definitions on its own whenever an async request needs newer
		/// schema data, so <see cref="OnDefinitionsUpdated"/> can fire without you asking.
		/// </para>
		/// </remarks>
		/// <example>
		/// <code>
		/// SteamInventory.OnDefinitionsUpdated += () =>
		/// {
		///     foreach ( var def in SteamInventory.Definitions )
		///         Console.WriteLine( $"{def.Id}: {def.Name}" );
		/// };
		///
		/// SteamInventory.LoadItemDefinitions();
		/// </code>
		/// </example>
		public static void LoadItemDefinitions()
		{
			// If they're null, try to load them immediately
			// my hunch is that this loads a disk cached version
			// but waiting for LoadItemDefinitions downloads a new copy
			// from Steam's servers. So this will give us immediate data
			// where as Steam's inventory servers could be slow/down
			if ( Definitions == null )
			{
				LoadDefinitions();
			}

			Internal.LoadItemDefinitions();
		}

		/// <summary>
		/// Awaitable form of <see cref="LoadItemDefinitions"/>: kicks off the schema load and does
		/// not return until <see cref="Definitions"/> is populated or the timeout elapses. This is
		/// the convenient thing to await once during startup before touching any other inventory
		/// call, so you never read items against an empty schema.
		/// </summary>
		/// <param name="timeoutSeconds">
		/// How long to keep waiting, in seconds, before giving up and returning <c>false</c>.
		/// The wait polls roughly every 10ms. Note the timeout only bounds the wait — it does not
		/// cancel the underlying Steam request, which may still complete later and fire
		/// <see cref="OnDefinitionsUpdated"/> after you have already been told <c>false</c>.
		/// </param>
		/// <returns>
		/// <c>true</c> if <see cref="Definitions"/> is populated (either it already was, or it
		/// became so within the timeout). <c>false</c> if the timeout elapsed first — which in
		/// practice most often means the app has no item schema configured on the Steamworks
		/// partner site, rather than a slow network.
		/// </returns>
		/// <remarks>
		/// Returning <c>false</c> is not an exception case you should ignore: every downstream
		/// call will appear to work while returning nothing. Treat it as "inventory is
		/// unavailable this session" and disable the relevant UI.
		/// </remarks>
		public static async Task<bool> WaitForDefinitions( float timeoutSeconds = 30 )
		{
			if ( Definitions != null )
				return true;

			LoadDefinitions();
			LoadItemDefinitions();

			if ( Definitions != null )
				return true;

			var sw = Stopwatch.StartNew();

			while ( Definitions == null )
			{
				if ( sw.Elapsed.TotalSeconds > timeoutSeconds )
					return false;

				await Task.Delay( 10 );
			}

			return true;
		}

		/// <summary>
		/// Looks up the schema entry for an item definition ID — the "what kind of thing is this"
		/// behind an owned <see cref="InventoryItem"/>. Backed by a dictionary, so it is cheap
		/// enough to call per-item while building UI.
		/// </summary>
		/// <param name="defId">
		/// The definition ID to look up, as found on <see cref="InventoryItem.DefId"/>. Valve
		/// reserves IDs of zero or below as invalid and one billion and above for internal Steam
		/// use, so only IDs your schema actually declares will ever match.
		/// </param>
		/// <returns>
		/// The matching <see cref="InventoryDef"/>, or <c>null</c> if the schema has not been
		/// loaded yet or contains no such ID. A null return before
		/// <see cref="OnDefinitionsUpdated"/> has fired means "not loaded", not "does not exist" —
		/// call <see cref="LoadItemDefinitions"/> or await <see cref="WaitForDefinitions"/> first.
		/// </returns>
		public static InventoryDef FindDefinition( InventoryDefId defId )
		{
			if ( _defMap == null )
				return null;

			if ( _defMap.TryGetValue( defId, out var val  ) )
				return val;

			return null;
		}

		/// <summary>
		/// The ISO 4217 currency code Steam is quoting prices in for this user, for example
		/// <c>"USD"</c> or <c>"EUR"</c>. Every price on <see cref="InventoryDef"/> is denominated
		/// in this currency, so you need it to display a price meaningfully.
		/// </summary>
		/// <value>
		/// The three-letter currency code, or <c>null</c> if prices have not been requested yet.
		/// This is only populated as a side effect of
		/// <see cref="GetDefinitionsWithPricesAsync"/> — reading it before that call has completed
		/// always gives <c>null</c>, and formatting a price against a null currency produces an
		/// unprefixed number.
		/// </value>
		/// <remarks>
		/// The currency is chosen by Steam from the user's wallet and store region. It is not
		/// something your game picks, and it can differ between two players in the same session.
		/// </remarks>
		public static string Currency { get; internal set; }

		/// <summary>
		/// Requests current store prices for this app's items and returns the definitions that
		/// actually have a price attached. Call this before building any storefront UI — it is
		/// what populates <see cref="Currency"/> and makes <see cref="InventoryDef.LocalPrice"/>
		/// return anything other than zero.
		/// </summary>
		/// <returns>
		/// The definitions that have a price, or <c>null</c> if the price request failed, if the
		/// request came back with a non-OK result, or if Steam reports that no items have prices
		/// at all. A null return most commonly means the app has no priced items configured on
		/// the Steamworks partner site rather than a transient failure, so do not retry it in a
		/// tight loop.
		/// </returns>
		/// <remarks>
		/// The returned objects carry only the definition ID; the prices themselves are read back
		/// lazily per definition through <see cref="InventoryDef.LocalPrice"/> and
		/// <see cref="InventoryDef.LocalBasePrice"/>. Those values are only valid because this
		/// call has run — Valve requires a price request before any price accessor returns data.
		/// </remarks>
		/// <example>
		/// <code>
		/// var priced = await SteamInventory.GetDefinitionsWithPricesAsync();
		/// if ( priced == null )
		///     return; // nothing is for sale, or the request failed
		///
		/// foreach ( var def in priced )
		///     Console.WriteLine( $"{def.Name}: {def.LocalPriceFormatted} ({SteamInventory.Currency})" );
		/// </code>
		/// </example>
		public static async Task<InventoryDef[]> GetDefinitionsWithPricesAsync()
		{
			var priceRequest = await Internal.RequestPrices();
			if ( !priceRequest.HasValue || priceRequest.Value.Result != Result.OK )
				return null;

			Currency = priceRequest?.CurrencyUTF8();

			var num = Internal.GetNumItemsWithPrices();

			if ( num <= 0 )
				return null;

			var defs = new InventoryDefId[num];
			var currentPrices = new ulong[num];
			var baseprices = new ulong[num];

			var gotPrices = Internal.GetItemsWithPrices( defs, currentPrices, baseprices, num );
			if ( !gotPrices )
				return null;

			return defs.Select( x => new InventoryDef( x ) ).ToArray();
		}

		/// <summary>
		/// The local user's items, refreshed automatically whenever Steam sends a full inventory
		/// update. Read this when you want the current contents of the player's inventory without
		/// managing an <see cref="InventoryResult"/> yourself — there is no handle here to dispose.
		/// </summary>
		/// <value>
		/// The owned items, or <c>null</c> until the first full inventory update arrives. It stays
		/// null indefinitely if the app has no item schema on the Steamworks partner site, so
		/// null-check it rather than assuming it becomes non-null after startup.
		/// </value>
		/// <remarks>
		/// <para>
		/// These items are built without their dynamic properties, so
		/// <see cref="InventoryItem.Properties"/> is null on every entry here. That in turn means
		/// <see cref="InventoryItem.Origin"/> returns null and
		/// <see cref="InventoryItem.Acquired"/> falls back to the current time rather than the real
		/// acquisition date. If you need either, fetch your own result with
		/// <see cref="GetAllItemsAsync"/> and call <c>GetItems</c> asking for properties.
		/// </para>
		/// <para>
		/// The array is replaced wholesale on each update, so cache the reference for the duration
		/// of a frame rather than holding it across updates, and prefer
		/// <see cref="OnInventoryUpdated"/> over polling.
		/// </para>
		/// </remarks>
		public static InventoryItem[] Items { get; internal set; }

		/// <summary>
		/// Every item type declared in this app's schema — the catalogue of what can exist, as
		/// opposed to <see cref="Items"/>, which is what the player actually owns. Use it to build
		/// a store, a crafting screen, or any UI that has to show items the player does not have
		/// yet.
		/// </summary>
		/// <value>
		/// The full set of definitions, or <c>null</c> until the schema has been loaded. Because
		/// this stays null when the app has no Inventory Service schema configured on the
		/// Steamworks partner site, a null here is the single clearest symptom of that
		/// misconfiguration.
		/// </value>
		/// <remarks>
		/// Populated by <see cref="LoadItemDefinitions"/> and refreshed whenever Steam updates the
		/// schema; <see cref="OnDefinitionsUpdated"/> fires each time. Look individual entries up
		/// with <see cref="FindDefinition"/> rather than scanning this array.
		/// </remarks>
		public static InventoryDef[] Definitions { get; internal set; }
		static Dictionary<int, InventoryDef> _defMap;

		internal static InventoryDef[] GetDefinitions()
		{
			uint num = 0;
			if ( !Internal.GetItemDefinitionIDs( null, ref num ) )
				return null;

			var defs = new InventoryDefId[num];

			if ( !Internal.GetItemDefinitionIDs( defs, ref num ) )
				return null;

			return defs.Select( x => new InventoryDef( x ) ).ToArray();
		}

		/// <summary>
		/// Fire-and-forget refresh of the local user's inventory. Asks Steam for a full snapshot
		/// and lets the result arrive through <see cref="OnInventoryUpdated"/>, which repopulates
		/// <see cref="Items"/>. Use this when you just want <see cref="Items"/> to be current and
		/// do not need to inspect the snapshot yourself; use <see cref="GetAllItemsAsync"/> when
		/// you do.
		/// </summary>
		/// <returns>
		/// <c>true</c> if the request was accepted. <c>false</c> if the inventory is unavailable —
		/// note this reports whether the request started, not whether the player owns anything, so
		/// a <c>true</c> here is entirely compatible with <see cref="Items"/> staying empty.
		/// </returns>
		/// <remarks>
		/// <para>
		/// Valve rate-limits this call and will hand back cached results if you call it too often.
		/// Call it when you are about to show the player their full inventory, or when you have
		/// reason to believe the inventory changed — not every frame, and not on a timer.
		/// </para>
		/// <para>
		/// Valve requires the result handle produced by this call to be destroyed. This binding
		/// discards the handle rather than exposing it, so there is nothing for you to dispose
		/// here; if you need deterministic cleanup, use <see cref="GetAllItemsAsync"/> and dispose
		/// the result it gives you.
		/// </para>
		/// </remarks>
		public static bool GetAllItems()
		{
			var sresult = Defines.k_SteamInventoryResultInvalid;
			return Internal.GetAllItems( ref sresult );
		}

		/// <summary>
		/// Fetches a full snapshot of the local user's inventory for this app and gives you the
		/// result to inspect. This is the call you make on startup to find out what the player
		/// owns, and the one to use when you need item properties, a serializable snapshot, or
		/// deterministic cleanup.
		/// </summary>
		/// <returns>
		/// The snapshot, or <c>null</c> if Steam refused the request (inventory unavailable, or
		/// the underlying query failed). The returned result owns a native handle and
		/// <b>must be disposed</b> — see <see cref="InventoryResult"/>. A non-null result
		/// containing zero items is normal and means the player owns nothing, or that the app has
		/// no schema configured on the Steamworks partner site.
		/// </returns>
		/// <remarks>
		/// Subject to the same rate limiting as <see cref="GetAllItems"/>: call it when you are
		/// about to display the inventory or when you expect it to have changed, not on a timer.
		/// Load the item schema first, or <see cref="InventoryItem.Def"/> will be null on every
		/// item you read back.
		/// </remarks>
		/// <example>
		/// <code>
		/// var result = await SteamInventory.GetAllItemsAsync();
		/// if ( !result.HasValue )
		///     return;
		///
		/// using ( var inventory = result.Value )
		/// {
		///     // Ask for the properties you actually need — each one is a separate native call.
		///     var items = inventory.GetItems( new[] { "origin", "acquired" } );
		///
		///     foreach ( var item in items ?? Array.Empty&lt;InventoryItem&gt;() )
		///         Console.WriteLine( $"{item.Def?.Name} from {item.Origin}" );
		/// }
		/// </code>
		/// </example>
		public static async Task<InventoryResult?> GetAllItemsAsync()
		{
			var sresult = Defines.k_SteamInventoryResultInvalid;

			if ( !Internal.GetAllItems( ref sresult ) )
				return null;

			return await InventoryResult.GetAsync( sresult );
		}

		/// <summary>
		/// Mints items out of nothing and gives them to the user. This is a prototyping tool, not
		/// a shipping mechanic — Valve restricts it to Steam accounts in your game's publisher
		/// group, so it will simply fail for your players. Use it while bringing up your item
		/// schema, and grant real items through drops, promos, purchases or exchanges instead.
		/// </summary>
		/// <param name="target">
		/// The definition of the item to create. Dereferenced immediately, so passing <c>null</c>
		/// throws.
		/// </param>
		/// <param name="amount">
		/// How many to create. Passed to Steam as an unsigned count, so a negative value does not
		/// error — it wraps to an enormous positive quantity. Validate this before calling.
		/// </param>
		/// <returns>
		/// The result describing the items that were granted, or <c>null</c> if the call was
		/// rejected — which is the normal outcome for any account outside your publisher group.
		/// The result owns a native handle and must be disposed.
		/// </returns>
		/// <remarks>
		/// This writes to the live inventory service and cannot be undone from the client. There
		/// is no "ungrant"; the only way back is <c>InventoryItem.ConsumeAsync</c>, which is
		/// itself irreversible.
		/// </remarks>
		public static async Task<InventoryResult?> GenerateItemAsync( InventoryDef target, int amount )
		{
			var sresult = Defines.k_SteamInventoryResultInvalid;

			var defs = new InventoryDefId[] { target.Id };
			var cnts = new uint[] { (uint)amount };

			if ( !Internal.GenerateItems( ref sresult, defs, cnts, 1 ) )
				return null;

			return await InventoryResult.GetAsync( sresult );
		}

		/// <summary>
		/// Crafting. Destroys the items you pass in and creates one of the target item in their
		/// place, in a single atomic operation on Steam's servers. Use this for recipes,
		/// transmutation, and for items that unpack themselves into other items such as crates.
		/// </summary>
		/// <param name="list">
		/// The item instances to consume. Exactly one unit of each is spent, so this overload is
		/// only correct when none of the inputs are stacks — use the
		/// <c>InventoryItem.Amount</c> overload when a recipe needs more than one of something.
		/// </param>
		/// <param name="target">
		/// The definition of the item to create. Exactly one is produced. Dereferenced
		/// immediately, so passing <c>null</c> throws.
		/// </param>
		/// <returns>
		/// The result describing the exchange, or <c>null</c> if Steam rejected the request
		/// outright. The result owns a native handle and must be disposed.
		/// </returns>
		/// <remarks>
		/// <para>
		/// <b>This destroys the input items and cannot be undone.</b> Put a confirmation step in
		/// front of it.
		/// </para>
		/// <para>
		/// The recipe must already exist in the item schema on the Steamworks partner site — the
		/// exchange rules live in the item definition, not in your code. Steam evaluates the
		/// recipe atomically: if the items you supply do not match a declared recipe, or there is
		/// not enough quantity, the whole exchange fails and nothing is consumed. Read the recipes
		/// your schema declares with <see cref="InventoryDef.GetRecipes"/>.
		/// </para>
		/// </remarks>
		/// <example>
		/// <code>
		/// var recipes = target.GetRecipes();
		/// if ( recipes == null || recipes.Length == 0 )
		///     return; // no exchange rules configured for this item
		///
		/// if ( !await ConfirmWithPlayer( "This will destroy the ingredients. Continue?" ) )
		///     return;
		///
		/// var result = await SteamInventory.CraftItemAsync( ingredients, target );
		/// if ( !result.HasValue )
		/// {
		///     Console.WriteLine( "Craft rejected — wrong ingredients, or not enough of them." );
		///     return;
		/// }
		///
		/// using ( var crafted = result.Value )
		/// {
		///     foreach ( var item in crafted.GetItems() ?? Array.Empty&lt;InventoryItem&gt;() )
		///         Console.WriteLine( $"Crafted {item.Def?.Name}" );
		/// }
		/// </code>
		/// </example>
		public static async Task<InventoryResult?> CraftItemAsync( InventoryItem[] list, InventoryDef target )
		{
			var sresult = Defines.k_SteamInventoryResultInvalid;

			var give = new InventoryDefId[] { target.Id };
			var givec = new uint[] { 1 };

			var sell = list.Select( x => x.Id ).ToArray();
			var sellc = list.Select( x => (uint)1 ).ToArray();

			if ( !Internal.ExchangeItems( ref sresult, give, givec, 1, sell, sellc, (uint)sell.Length ) )
				return null;

			return await InventoryResult.GetAsync( sresult );
		}

		/// <summary>
		/// Crafting, with explicit quantities. Same atomic exchange as the other overload, but each
		/// input carries how many units of that stack to spend, so it works for recipes that need
		/// more than one of an ingredient.
		/// </summary>
		/// <param name="list">
		/// The item instances to consume, each paired with the quantity to spend from that stack.
		/// Quantities are sent to Steam unsigned, so a negative <c>Quantity</c> wraps to an
		/// enormous positive number rather than failing cleanly — validate before calling.
		/// </param>
		/// <param name="target">
		/// The definition of the item to create. Exactly one is produced. Dereferenced
		/// immediately, so passing <c>null</c> throws.
		/// </param>
		/// <returns>
		/// The result describing the exchange, or <c>null</c> if Steam rejected the request
		/// outright. The result owns a native handle and must be disposed.
		/// </returns>
		/// <remarks>
		/// <b>This destroys the input items and cannot be undone.</b> The recipe must be declared
		/// in the item schema on the Steamworks partner site; Steam evaluates it atomically and
		/// consumes nothing if the ingredients or quantities do not match.
		/// </remarks>
		public static async Task<InventoryResult?> CraftItemAsync( InventoryItem.Amount[] list, InventoryDef target )
		{
			var sresult = Defines.k_SteamInventoryResultInvalid;

			var give = new InventoryDefId[] { target.Id };
			var givec = new uint[] { 1 };

			var sell = list.Select( x => x.Item.Id ).ToArray();
			var sellc = list.Select( x => (uint) x.Quantity ).ToArray();

			if ( !Internal.ExchangeItems( ref sresult, give, givec, 1, sell, sellc, (uint)sell.Length ) )
				return null;

			return await InventoryResult.GetAsync( sresult );
		}

		/// <summary>
		/// Rebuilds an inventory snapshot that some other machine produced with
		/// <see cref="InventoryResult.Serialize"/>, and verifies Steam's signature over it. This is
		/// the receiving half of the anti-forgery mechanism: a client serializes proof of the items
		/// it owns, sends the blob over your own networking, and your game server (or another
		/// peer) calls this to confirm the claim is genuine rather than taking the client's word
		/// for it.
		/// </summary>
		/// <param name="data">
		/// The signed blob produced by <see cref="InventoryResult.Serialize"/> on the sending
		/// machine. Must not be <c>null</c>. Treat it as untrusted input — the signature check is
		/// what makes it trustworthy, and the ownership check below is what makes it relevant.
		/// </param>
		/// <param name="dataLength">
		/// How many bytes of <paramref name="data"/> to read. The default of <c>-1</c> means "the
		/// whole array". Pass an explicit length when the blob sits inside a larger receive
		/// buffer. This is not validated against the array size, so a value larger than
		/// <c>data.Length</c> throws from the underlying copy.
		/// </param>
		/// <returns>
		/// The verified snapshot, or <c>null</c> if the data failed to deserialize. The result owns
		/// a native handle and must be disposed.
		/// </returns>
		/// <exception cref="ArgumentException">
		/// Thrown when <paramref name="data"/> is <c>null</c>.
		/// </exception>
		/// <remarks>
		/// <para>
		/// <b>A valid signature only proves the snapshot is real, not whose it is.</b> Always call
		/// <see cref="InventoryResult.BelongsTo"/> with the Steam ID you expect, or a player can
		/// replay a snapshot of somebody else's rare items and pass your check.
		/// </para>
		/// <para>
		/// There is a soft-failure mode: the snapshot can come back flagged as expired and still
		/// succeed, exposed here as <see cref="InventoryResult.Expired"/>. Expired means the data
		/// may be stale — either the built-in one hour timestamp elapsed, or an item in the set has
		/// been traded or consumed since it was generated. Whether that matters is your call: you
		/// can accept it, or ask the player to send a fresh snapshot. Valve suggests comparing the
		/// result timestamp against Steam's server time to judge staleness precisely, but this
		/// binding does not expose the result timestamp, so <see cref="InventoryResult.Expired"/>
		/// is the signal you have.
		/// </para>
		/// <para>
		/// Valve reserves the fourth parameter of the underlying call for future use and requires
		/// it to be false; this binding hardcodes it, so there is nothing to pass.
		/// </para>
		/// </remarks>
		/// <example>
		/// <code>
		/// // On the server, having received `blob` and the claimed owner's id from a client:
		/// var result = await SteamInventory.DeserializeAsync( blob );
		/// if ( !result.HasValue )
		///     return false; // not a genuine Steam-signed snapshot
		///
		/// using ( var snapshot = result.Value )
		/// {
		///     if ( !snapshot.BelongsTo( claimedOwner ) )
		///         return false; // real snapshot, wrong player — someone is replaying it
		///
		///     if ( snapshot.Expired )
		///         Console.WriteLine( "Snapshot is over an hour old or has since changed." );
		///
		///     return snapshot.GetItems()?.Any( x =&gt; x.DefId == requiredHat ) == true;
		/// }
		/// </code>
		/// </example>
		public static async Task<InventoryResult?> DeserializeAsync( byte[] data, int dataLength = -1 )
		{
			if ( data == null )
				throw new ArgumentException( "data should not be null" );

			if ( dataLength == -1 )
				dataLength = data.Length;

			var ptr = Marshal.AllocHGlobal( dataLength );

			try
			{
				Marshal.Copy( data, 0, ptr, dataLength );

				var sresult = Defines.k_SteamInventoryResultInvalid;

				if ( !Internal.DeserializeResult( ref sresult, (IntPtr)ptr, (uint)dataLength, false ) )
					return null;

				

				return await InventoryResult.GetAsync( sresult.Value );
			}
			finally
			{
				Marshal.FreeHGlobal( ptr );
			}
		}


		/// <summary>
		/// Scans every promotional item this app declares, works out which ones the user qualifies
		/// for, and grants them — once and once only. Safe to call at startup as a catch-all so
		/// players who become eligible between sessions get their items.
		/// </summary>
		/// <returns>
		/// The result listing whatever was granted, or <c>null</c> if Steam rejected the request.
		/// The result owns a native handle and must be disposed.
		/// </returns>
		/// <remarks>
		/// A user who is eligible for nothing still counts as success: you get a non-null result
		/// containing zero items, not an error. So an empty result here is the expected everyday
		/// case, not something to log as a failure. Which items are promotional, and what makes a
		/// user eligible, is configured entirely in the item schema on the Steamworks partner site.
		/// Use <see cref="AddPromoItemAsync"/> instead when you want to grant one specific promo
		/// item behind your own UI.
		/// </remarks>
		public static async Task<InventoryResult?> GrantPromoItemsAsync()
		{
			var sresult = Defines.k_SteamInventoryResultInvalid;

			if ( !Internal.GrantPromoItems( ref sresult ) )
				return null;

			return await InventoryResult.GetAsync( sresult );
		}


		/// <summary>
		/// Cashes the player's accumulated playtime credit in for an item drop. Steam accrues the
		/// credit as they play, but it never becomes an item until your game asks — so you must
		/// call this at a sensible moment (between rounds, on respawn, at a lull) or the player
		/// simply never receives drops.
		/// </summary>
		/// <param name="id">
		/// The definition ID of the drop list to roll against. Only definitions marked as playtime
		/// item generators in your schema can produce anything here.
		/// </param>
		/// <returns>
		/// The result listing anything that dropped, or <c>null</c> if Steam rejected the request.
		/// An empty result is the normal outcome when the player has not banked enough playtime
		/// credit yet, and is not an error. The result owns a native handle and must be disposed.
		/// </returns>
		/// <remarks>
		/// <para>
		/// A hacked client can change which <paramref name="id"/> gets passed, so never use it to
		/// control rarity — put the odds in the drop rates configured per item definition on the
		/// Steamworks partner site, where the server enforces them.
		/// </para>
		/// <para>
		/// Steam's client library suppresses calls it considers too frequent, so hammering this
		/// will not produce more drops. Call it at natural break points instead.
		/// </para>
		/// </remarks>
		public static async Task<InventoryResult?> TriggerItemDropAsync( InventoryDefId id )
		{
			var sresult = Defines.k_SteamInventoryResultInvalid;

			if ( !Internal.TriggerItemDrop( ref sresult, id ) )
				return null;

			return await InventoryResult.GetAsync( sresult );
		}

		/// <summary>
		/// Grants one specific promotional item, if the user is eligible for it. This is the
		/// narrow version of <see cref="GrantPromoItemsAsync"/> — use it when your game has its own
		/// UI built around a particular promo item and you want to grant just that one rather than
		/// sweeping for everything.
		/// </summary>
		/// <param name="id">
		/// The definition ID of the promotional item to check and grant. Eligibility is decided by
		/// Steam from the schema on the partner site, not by your game.
		/// </param>
		/// <returns>
		/// The result listing the item if it was granted, or <c>null</c> if Steam rejected the
		/// request. An empty result means the user was not eligible, which is a success, not an
		/// error. The result owns a native handle and must be disposed.
		/// </returns>
		/// <remarks>
		/// Safe to call at startup and safe to call repeatedly — promotional grants are one time
		/// only, so a user who already has the item will not receive a second copy.
		/// </remarks>
		public static async Task<InventoryResult?> AddPromoItemAsync( InventoryDefId id )
		{
			var sresult = Defines.k_SteamInventoryResultInvalid;

			if ( !Internal.AddPromoItem( ref sresult, id ) )
				return null;

			return await InventoryResult.GetAsync( sresult );
		}


		/// <summary>
		/// Opens the Steam microtransaction flow for a cart of items. This only <i>starts</i> the
		/// purchase — Steam then shows the user its own authorization overlay, and the money and
		/// the items move later, outside this call.
		/// </summary>
		/// <param name="items">
		/// The items to buy. Quantity is expressed by repetition, not by a count: to buy three of
		/// something, put the same definition in this array three times. This binding groups the
		/// array by definition ID and sends the occurrence count as the quantity.
		/// </param>
		/// <returns>
		/// A <see cref="InventoryPurchaseResult"/> carrying the order and transaction IDs if Steam
		/// managed to open the transaction, or <c>null</c> if it did not. There is no native handle
		/// here and nothing to dispose.
		/// </returns>
		/// <remarks>
		/// <para>
		/// <b>A non-null return does not mean the player paid.</b> It means Steam initialized the
		/// transaction. The user still has to authorize it, and they can cancel. Wait for
		/// <see cref="SteamUser.OnMicroTxnAuthorizationResponse"/> to learn the real outcome, and
		/// check the <c>Result</c> field on the value returned here before trusting the IDs.
		/// </para>
		/// <para>
		/// Once the purchase completes the inventory changes on Steam's side, which will surface
		/// through <see cref="OnInventoryUpdated"/> — do not grant the item locally yourself off
		/// the back of this call.
		/// </para>
		/// <para>
		/// Items only have a price if one is configured on the Steamworks partner site; call
		/// <see cref="GetDefinitionsWithPricesAsync"/> first to find out which items are actually
		/// purchasable and what they cost.
		/// </para>
		/// </remarks>
		public static async Task<InventoryPurchaseResult?> StartPurchaseAsync( InventoryDef[] items )
		{
			var d = items.GroupBy( x => x._id ).ToDictionary( x => x.Key, x => (uint) x.Count() );
			var item_i = d.Keys.ToArray();
			var item_q = d.Values.ToArray();

			var r = await Internal.StartPurchase( item_i, item_q, (uint)item_i.Length );
			if ( !r.HasValue ) return null;

			return new InventoryPurchaseResult
			{
				Result = r.Value.Result,
				OrderID = r.Value.OrderID,
				TransID = r.Value.TransID
			};
		}

	}
}
