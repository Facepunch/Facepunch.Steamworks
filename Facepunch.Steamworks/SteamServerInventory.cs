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
	public static class SteamServerInventory
	{
		static ISteamInventory _internal;
		internal static ISteamInventory Internal
		{
			get
			{
				if ( _internal == null )
					_internal = new ISteamInventory();

				return _internal;
			}
		}
		internal static void Shutdown()
		{
			_internal = null;
		}

		internal static void InstallEvents()
		{
			Event.CreateEvent<SteamInventoryDefinitionUpdate_t>( x => DefinitionsUpdated(), true );
		}

		public static event Action OnDefinitionsUpdated;

		internal static int defUpdateCount = 0;

		internal static void DefinitionsUpdated()
		{
			Definitions = GetDefinitions();

			if ( Definitions != null )
			{
				_defMap = new Dictionary<int, InventoryDef>();

				foreach ( var d in Definitions )
				{
					_defMap[d.Id] = d;
				}
			}

			defUpdateCount++;

			OnDefinitionsUpdated?.Invoke();
		}


		/// <summary>
		/// Call this if you're going to want to access definition information. You should be able to get 
		/// away with calling this once at the start if your game, assuming your items don't change all the time.
		/// This will trigger OnDefinitionsUpdated at which point Definitions should be set.
		/// </summary>
		public static void LoadItemDefinitions()
		{
			Internal.LoadItemDefinitions();
		}

		/// <summary>
		/// Will call LoadItemDefinitions and wait until Definitions is not null
		/// </summary>
		public static async Task<bool> WaitForDefinitions( float timeoutSeconds = 10 )
		{
			LoadItemDefinitions();

			var sw = Stopwatch.StartNew();

			while ( Definitions == null )
			{
				if ( sw.Elapsed.TotalSeconds > timeoutSeconds )
					return false;

				await Task.Delay( 10 );
			}

			return true;
		}

		internal static InventoryDef FindDefinition( InventoryDefId defId )
		{
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

			Currency = priceRequest?.Currency;

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

		public static InventoryDef[] Definitions { get; internal set; }
		public static Dictionary<int, InventoryDef> _defMap;

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
		static async Task<InventoryResult?> DeserializeAsync( byte[] data, int dataLength = -1 )
		{
			if ( data == null )
				throw new ArgumentException( "data should nto be null" );

			if ( dataLength == -1 )
				dataLength = data.Length;

			var sresult = DeserializeResult( data, dataLength );
			if ( !sresult.HasValue ) return null;

			return await InventoryResult.GetAsync( sresult.Value );
		}

		internal static unsafe SteamInventoryResult_t? DeserializeResult( byte[] data, int dataLength = -1 )
		{
			var sresult = default( SteamInventoryResult_t );

			fixed ( byte* ptr = data )
			{
				if ( !Internal.DeserializeResult( ref sresult, (IntPtr)ptr, (uint)dataLength, false ) )
					return null;
			}

			return sresult;
		}

	}
}