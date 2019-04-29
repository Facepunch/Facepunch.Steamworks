using System;
using System.Collections.Generic;
using Steamworks.Data;

namespace Steamworks
{
	public class InventoryDef
	{
		internal InventoryDefId _id;

		public InventoryDef( InventoryDefId defId )
		{
			_id = defId;
		}

		public int Id => _id.Value;

		/// <summary>
		/// Shortcut to call GetProperty( "name" )
		/// </summary>
		public string Name => GetProperty( "name" );

		/// <summary>
		/// Shortcut to call GetProperty( "description" )
		/// </summary>
		public string Description => GetProperty( "description" );

		/// <summary>
		/// Shortcut to call GetProperty( "icon_url" )
		/// </summary>
		public string IconUrl => GetProperty( "icon_url" );

		/// <summary>
		/// Shortcut to call GetProperty( "icon_url_large" )
		/// </summary>
		public string IconUrlLarge => GetProperty( "icon_url_large" );

		/// <summary>
		/// Shortcut to call GetProperty( "price_category" )
		/// </summary>
		public string PriceCategory => GetProperty( "price_category" );

		/// <summary>
		/// Shortcut to call GetProperty( "type" )
		/// </summary>
		public string Type => GetProperty( "type" );

		/// <summary>
		/// Shortcut to call GetProperty( "exchange" )
		/// </summary>
		public string Exchange => GetProperty( "exchange" );

		/// <summary>
		/// Shortcut to call GetBoolProperty( "marketable" )
		/// </summary>
		public bool Marketable => GetBoolProperty( "marketable" );

		/// <summary>
		/// Shortcut to call GetBoolProperty( "tradable" )
		/// </summary>
		public bool Tradable => GetBoolProperty( "tradable" );

		/// <summary>
		/// Gets the property timestamp
		/// </summary>
		public DateTime Created => GetProperty<DateTime>( "timestamp" );

		/// <summary>
		/// Gets the property modified
		/// </summary>
		public DateTime Modified => GetProperty<DateTime>( "modified" );

		/// <summary>
		/// Get a specific property by name
		/// </summary>
		public string GetProperty( string name )
		{
			var sb = Helpers.TakeStringBuilder();
			uint _ = (uint)sb.Capacity;

			if ( !SteamInventory.Internal.GetItemDefinitionProperty( Id, name, sb, ref _ ) )
				return null;

			return sb.ToString();
		}

		/// <summary>
		/// Read a raw property from the definition schema
		/// </summary>
		public bool GetBoolProperty( string name )
		{
			string val = GetProperty( name );

			if ( val.Length == 0 ) return false;
			if ( val[0] == '0' || val[0] == 'F' || val[0] == 'f' ) return false;

			return true;
		}

		/// <summary>
		/// Read a raw property from the definition schema
		/// </summary>
		public T GetProperty<T>( string name )
		{
			string val = GetProperty( name );

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

		/// <summary>
		/// Gets a list of all properties on this item
		/// </summary>
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
		/// Returns the price of this item in the local currency (SteamInventory.Currency)
		/// </summary>
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

		public string LocalPriceFormatted => Utility.FormatPrice( SteamInventory.Currency, LocalPrice / 100.0 );

		/// <summary>
		/// If the price has been discounted, LocalPrice will differ from LocalBasePrice
		/// (assumed, this isn't documented)
		/// </summary>
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

		public string LocalBasePriceFormatted => Utility.FormatPrice( SteamInventory.Currency, LocalPrice / 100.0 );
	}
}