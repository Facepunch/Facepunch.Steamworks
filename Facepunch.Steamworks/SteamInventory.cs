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
	/// Undocumented Parental Settings
	/// </summary>
	public class SteamInventory : SteamSharedClass<SteamInventory>
	{
		internal static ISteamInventory Internal => Interface as ISteamInventory;

		internal override void InitializeInterface( bool server )
		{
			SetInterface( server, new ISteamInventory( server ) );

			InstallEvents( server );
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

		public static event Action<InventoryResult> OnInventoryUpdated;
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
		/// Call this if you're going to want to access definition information. You should be able to get 
		/// away with calling this once at the start if your game, assuming your items don't change all the time.
		/// This will trigger OnDefinitionsUpdated at which point Definitions should be set.
		/// </summary>
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
		/// Will call LoadItemDefinitions and wait until Definitions is not null
		/// </summary>
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
		/// Try to find the definition that matches this definition ID.
		/// Uses a dictionary so should be about as fast as possible.
		/// </summary>
		public static InventoryDef FindDefinition( InventoryDefId defId )
		{
			if ( _defMap == null )
				return null;

			if ( _defMap.TryGetValue( defId, out var val  ) )
				return val;

			return null;
		}

		public static string Currency { get; internal set; }

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
		/// We will try to keep this list of your items automatically up to date.
		/// </summary>
		public static InventoryItem[] Items { get; internal set; }

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
		/// Update the list of Items[]
		/// </summary>
		public static bool GetAllItems()
		{
			var sresult = Defines.k_SteamInventoryResultInvalid;
			return Internal.GetAllItems( ref sresult );
		}

		/// <summary>
		/// Get all items and return the InventoryResult
		/// </summary>
		public static async Task<InventoryResult?> GetAllItemsAsync()
		{
			var sresult = Defines.k_SteamInventoryResultInvalid;

			if ( !Internal.GetAllItems( ref sresult ) )
				return null;

			return await InventoryResult.GetAsync( sresult );
		}

		/// <summary>
		/// This is used to grant a specific item to the user. This should 
		/// only be used for development prototyping, from a trusted server, 
		/// or if you don't care about hacked clients granting arbitrary items. 
		/// This call can be disabled by a setting on Steamworks.
		/// </summary>
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
		/// Crafting! Uses the passed items to buy the target item.
		/// You need to have set up the appropriate exchange rules in your item
		/// definitions. This assumes all the items passed in aren't stacked.
		/// </summary>
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
		/// Crafting! Uses the passed items to buy the target item.
		/// You need to have set up the appropriate exchange rules in your item
		/// definitions. This assumes all the items passed in aren't stacked.
		/// </summary>
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
		/// Deserializes a result set and verifies the signature bytes.	
		/// This call has a potential soft-failure mode where the Result is expired, it will 
		/// still succeed in this mode.The "expired" 
		/// result could indicate that the data may be out of date - not just due to timed 
		/// expiration( one hour ), but also because one of the items in the result set may 
		/// have been traded or consumed since the result set was generated.You could compare 
		/// the timestamp from GetResultTimestamp to ISteamUtils::GetServerRealTime to determine
		/// how old the data is. You could simply ignore the "expired" result code and 
		/// continue as normal, or you could request the player with expired data to send 
		/// an updated result set.
		/// You should call CheckResultSteamID on the result handle when it completes to verify 
		/// that a remote player is not pretending to have a different user's inventory.
		/// </summary>
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
		/// Grant all promotional items the user is eligible for
		/// </summary>
		public static async Task<InventoryResult?> GrantPromoItemsAsync()
		{
			var sresult = Defines.k_SteamInventoryResultInvalid;

			if ( !Internal.GrantPromoItems( ref sresult ) )
				return null;

			return await InventoryResult.GetAsync( sresult );
		}


		/// <summary>
		/// Trigger an item drop for this user. This is for timed drops.
		/// </summary>
		public static async Task<InventoryResult?> TriggerItemDropAsync( InventoryDefId id )
		{
			var sresult = Defines.k_SteamInventoryResultInvalid;

			if ( !Internal.TriggerItemDrop( ref sresult, id ) )
				return null;

			return await InventoryResult.GetAsync( sresult );
		}

		/// <summary>
		/// Trigger a promo item drop. You can call this at startup, it won't
		/// give users multiple promo drops.
		/// </summary>
		public static async Task<InventoryResult?> AddPromoItemAsync( InventoryDefId id )
		{
			var sresult = Defines.k_SteamInventoryResultInvalid;

			if ( !Internal.AddPromoItem( ref sresult, id ) )
				return null;

			return await InventoryResult.GetAsync( sresult );
		}


		/// <summary>
		/// Start buying a cart load of items. This will return a positive result is the purchase has
		/// begun. You should listen out for SteamUser.OnMicroTxnAuthorizationResponse for a success.
		/// </summary>
		public static async Task<InventoryPurchaseResult?> StartPurchaseAsync( InventoryDef[] items )
		{
			var item_i = items.Select( x => x._id ).ToArray();
			var item_q = items.Select( x => (uint)1 ).ToArray();

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