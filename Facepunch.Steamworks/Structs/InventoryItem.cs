using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Steamworks.Data;

namespace Steamworks
{
	public struct InventoryItem
	{
		internal InventoryItemId _id;
		internal InventoryDefId _def;
		internal int _flags;
		internal ushort _quantity;
		internal Dictionary<string, string> _properties;

		public InventoryItemId Id => _id;

		public InventoryDefId DefId => _def;

		public int Quantity => _quantity;

		public InventoryDef Def => SteamInventory.FindDefinition( DefId );


		/// <summary>
		/// Only available if the result set was created with the getproperties
		/// </summary>
		public Dictionary<string, string> Properties => _properties;

		/// <summary>
		/// This item is account-locked and cannot be traded or given away. 
		/// This is an item status flag which is permanently attached to specific item instances
		/// </summary>
		public bool IsNoTrade => (_flags & 1 << 0) != 0;

		/// <summary>
		/// The item has been destroyed, traded away, expired, or otherwise invalidated. 
		/// This is an action confirmation flag which is only set one time, as part of a result set.
		/// </summary>
		public bool IsRemoved => (_flags & 1 << 8) != 0;

		/// <summary>
		/// The item quantity has been decreased by 1 via ConsumeItem API. 
		/// This is an action confirmation flag which is only set one time, as part of a result set.
		/// </summary>
		public bool IsConsumed => (_flags & 1 << 9) != 0;

		/// <summary>
		/// Consumes items from a user's inventory. If the quantity of the given item goes to zero, it is permanently removed.
		/// Once an item is removed it cannot be recovered.This is not for the faint of heart - if your game implements item removal at all, 
		/// a high-friction UI confirmation process is highly recommended.ConsumeItem can be restricted to certain item definitions or fully
		/// blocked via the Steamworks website to minimize support/abuse issues such as the classic "my brother borrowed my laptop and deleted all of my rare items".
		/// </summary>
		public async Task<InventoryResult?> Consume( int amount = 1 )
		{
			var sresult = default( SteamInventoryResult_t );
			if ( !SteamInventory.Internal.ConsumeItem( ref sresult, Id, (uint)amount ) )
				return null;

			return await InventoryResult.GetAsync( sresult );
		}

		/// <summary>
		/// Split stack into two items
		/// </summary>
		public async Task<InventoryResult?> SplitStack( int quantity = 1 )
		{
			var sresult = default( SteamInventoryResult_t );
			if ( !SteamInventory.Internal.TransferItemQuantity( ref sresult, Id, (uint)quantity, ulong.MaxValue ) )
				return null;

			return await InventoryResult.GetAsync( sresult );
		}

		/// <summary>
		/// Add x units of the target item to this item
		/// </summary>
		public async Task<InventoryResult?> Add( InventoryItem add, int quantity = 1 )
		{
			var sresult = default( SteamInventoryResult_t );
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
				_flags = details.Flags,
				_quantity = details.Quantity
			};

			return i;
		}

		internal static Dictionary<string, string> GetProperties( SteamInventoryResult_t result, int index )
		{
			var sb = Helpers.TakeStringBuilder();
			var strlen = (uint) sb.Capacity;

			if ( !SteamInventory.Internal.GetResultItemProperty( result, (uint)index, null, sb, ref strlen ) )
				return null;

			var propNames = sb.ToString();

			var props = new Dictionary<string, string>();

			foreach ( var propertyName in propNames.Split( ',' ) )
			{
				strlen = (uint)sb.Capacity;

				if ( SteamInventory.Internal.GetResultItemProperty( result, (uint)index, propertyName, sb, ref strlen ) )
				{
					props.Add( propertyName, sb.ToString() );
				}
			}

			return props;
		}

		/// <summary>
		/// Small utility class to describe an item with a quantity
		/// </summary>
		public struct Amount
		{
			public InventoryItem Item;
			public int Quantity;
		}

		public static bool operator ==( InventoryItem a, InventoryItem b ) => a._id == b._id;
		public static bool operator !=( InventoryItem a, InventoryItem b ) => a._id != b._id;
	}
}