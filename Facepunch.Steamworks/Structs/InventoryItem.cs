using System;
using System.Collections.Generic;
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
	}
}