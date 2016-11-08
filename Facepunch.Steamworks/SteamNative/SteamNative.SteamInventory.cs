using System;
using System.Runtime.InteropServices;

namespace SteamNative
{
	internal unsafe class SteamInventory : IDisposable
	{
		//
		// Holds a platform specific implentation
		//
		internal Platform.Interface platform;
		internal Facepunch.Steamworks.BaseSteamworks steamworks;
		
		//
		// Constructor decides which implementation to use based on current platform
		//
		internal SteamInventory( Facepunch.Steamworks.BaseSteamworks steamworks, IntPtr pointer )
		{
			this.steamworks = steamworks;
			
			if ( Platform.IsWindows64 ) platform = new Platform.Win64( pointer );
			else if ( Platform.IsWindows32 ) platform = new Platform.Win32( pointer );
			else if ( Platform.IsLinux32 ) platform = new Platform.Linux32( pointer );
			else if ( Platform.IsLinux64 ) platform = new Platform.Linux64( pointer );
			else if ( Platform.IsOsx ) platform = new Platform.Mac( pointer );
		}
		
		//
		// Class is invalid if we don't have a valid implementation
		//
		public bool IsValid{ get{ return platform != null && platform.IsValid; } }
		
		//
		// When shutting down clear all the internals to avoid accidental use
		//
		public virtual void Dispose()
		{
			 if ( platform != null )
			{
				platform.Dispose();
				platform = null;
			}
		}
		
		// bool
		public bool AddPromoItem( ref SteamInventoryResult_t pResultHandle /*SteamInventoryResult_t **/, SteamItemDef_t itemDef /*SteamItemDef_t*/ )
		{
			return platform.ISteamInventory_AddPromoItem( ref pResultHandle.Value, itemDef.Value );
		}
		
		// bool
		public bool AddPromoItems( ref SteamInventoryResult_t pResultHandle /*SteamInventoryResult_t **/, SteamItemDef_t[] pArrayItemDefs /*const SteamItemDef_t **/, uint unArrayLength /*uint32*/ )
		{
			return platform.ISteamInventory_AddPromoItems( ref pResultHandle.Value, Array.ConvertAll( pArrayItemDefs, p => p.Value ), unArrayLength );
		}
		
		// bool
		public bool CheckResultSteamID( SteamInventoryResult_t resultHandle /*SteamInventoryResult_t*/, CSteamID steamIDExpected /*class CSteamID*/ )
		{
			return platform.ISteamInventory_CheckResultSteamID( resultHandle.Value, steamIDExpected.Value );
		}
		
		// bool
		public bool ConsumeItem( ref SteamInventoryResult_t pResultHandle /*SteamInventoryResult_t **/, SteamItemInstanceID_t itemConsume /*SteamItemInstanceID_t*/, uint unQuantity /*uint32*/ )
		{
			return platform.ISteamInventory_ConsumeItem( ref pResultHandle.Value, itemConsume.Value, unQuantity );
		}
		
		// bool
		public bool DeserializeResult( ref SteamInventoryResult_t pOutResultHandle /*SteamInventoryResult_t **/, IntPtr pBuffer /*const void **/, uint unBufferSize /*uint32*/, bool bRESERVED_MUST_BE_FALSE /*bool*/ )
		{
			return platform.ISteamInventory_DeserializeResult( ref pOutResultHandle.Value, (IntPtr) pBuffer, unBufferSize, bRESERVED_MUST_BE_FALSE );
		}
		
		// void
		public void DestroyResult( SteamInventoryResult_t resultHandle /*SteamInventoryResult_t*/ )
		{
			platform.ISteamInventory_DestroyResult( resultHandle.Value );
		}
		
		// bool
		public bool ExchangeItems( ref SteamInventoryResult_t pResultHandle /*SteamInventoryResult_t **/, SteamItemDef_t[] pArrayGenerate /*const SteamItemDef_t **/, uint[] punArrayGenerateQuantity /*const uint32 **/, uint unArrayGenerateLength /*uint32*/, SteamItemInstanceID_t[] pArrayDestroy /*const SteamItemInstanceID_t **/, uint[] punArrayDestroyQuantity /*const uint32 **/, uint unArrayDestroyLength /*uint32*/ )
		{
			return platform.ISteamInventory_ExchangeItems( ref pResultHandle.Value, Array.ConvertAll( pArrayGenerate, p => p.Value ), punArrayGenerateQuantity, unArrayGenerateLength, Array.ConvertAll( pArrayDestroy, p => p.Value ), punArrayDestroyQuantity, unArrayDestroyLength );
		}
		
		// bool
		public bool GenerateItems( ref SteamInventoryResult_t pResultHandle /*SteamInventoryResult_t **/, SteamItemDef_t[] pArrayItemDefs /*const SteamItemDef_t **/, uint[] punArrayQuantity /*const uint32 **/, uint unArrayLength /*uint32*/ )
		{
			return platform.ISteamInventory_GenerateItems( ref pResultHandle.Value, Array.ConvertAll( pArrayItemDefs, p => p.Value ), punArrayQuantity, unArrayLength );
		}
		
		// bool
		public bool GetAllItems( ref SteamInventoryResult_t pResultHandle /*SteamInventoryResult_t **/ )
		{
			return platform.ISteamInventory_GetAllItems( ref pResultHandle.Value );
		}
		
		// bool
		// using: Detect_MultiSizeArrayReturn
		public SteamItemDef_t[] GetItemDefinitionIDs()
		{
			uint punItemDefIDsArraySize = 0;
			
			bool success = false;
			success = platform.ISteamInventory_GetItemDefinitionIDs( IntPtr.Zero, out punItemDefIDsArraySize );
			if ( !success || punItemDefIDsArraySize == 0) return null;
			
			var pItemDefIDs = new SteamItemDef_t[punItemDefIDsArraySize];
			fixed ( void* pItemDefIDs_ptr = pItemDefIDs )
			{
				success = platform.ISteamInventory_GetItemDefinitionIDs( (IntPtr) pItemDefIDs_ptr, out punItemDefIDsArraySize );
				if ( !success ) return null;
				return pItemDefIDs;
			}
		}
		
		// bool
		// with: Detect_StringFetch False
		public bool GetItemDefinitionProperty( SteamItemDef_t iDefinition /*SteamItemDef_t*/, string pchPropertyName /*const char **/, out string pchValueBuffer /*char **/ )
		{
			bool bSuccess = default( bool );
			pchValueBuffer = string.Empty;
			System.Text.StringBuilder pchValueBuffer_sb = Helpers.TakeStringBuilder();
			uint punValueBufferSizeOut = 4096;
			bSuccess = platform.ISteamInventory_GetItemDefinitionProperty( iDefinition.Value, pchPropertyName, pchValueBuffer_sb, out punValueBufferSizeOut );
			if ( !bSuccess ) return bSuccess;
			pchValueBuffer = pchValueBuffer_sb.ToString();
			return bSuccess;
		}
		
		// bool
		public bool GetItemsByID( ref SteamInventoryResult_t pResultHandle /*SteamInventoryResult_t **/, SteamItemInstanceID_t[] pInstanceIDs /*const SteamItemInstanceID_t **/, uint unCountInstanceIDs /*uint32*/ )
		{
			return platform.ISteamInventory_GetItemsByID( ref pResultHandle.Value, Array.ConvertAll( pInstanceIDs, p => p.Value ), unCountInstanceIDs );
		}
		
		// bool
		// using: Detect_MultiSizeArrayReturn
		public SteamItemDetails_t[] GetResultItems( SteamInventoryResult_t resultHandle /*SteamInventoryResult_t*/ )
		{
			uint punOutItemsArraySize = 0;
			
			bool success = false;
			success = platform.ISteamInventory_GetResultItems( resultHandle.Value, IntPtr.Zero, out punOutItemsArraySize );
			if ( !success || punOutItemsArraySize == 0) return null;
			
			var pOutItemsArray = new SteamItemDetails_t[punOutItemsArraySize];
			fixed ( void* pOutItemsArray_ptr = pOutItemsArray )
			{
				success = platform.ISteamInventory_GetResultItems( resultHandle.Value, (IntPtr) pOutItemsArray_ptr, out punOutItemsArraySize );
				if ( !success ) return null;
				return pOutItemsArray;
			}
		}
		
		// Result
		public Result GetResultStatus( SteamInventoryResult_t resultHandle /*SteamInventoryResult_t*/ )
		{
			return platform.ISteamInventory_GetResultStatus( resultHandle.Value );
		}
		
		// uint
		public uint GetResultTimestamp( SteamInventoryResult_t resultHandle /*SteamInventoryResult_t*/ )
		{
			return platform.ISteamInventory_GetResultTimestamp( resultHandle.Value );
		}
		
		// bool
		public bool GrantPromoItems( ref SteamInventoryResult_t pResultHandle /*SteamInventoryResult_t **/ )
		{
			return platform.ISteamInventory_GrantPromoItems( ref pResultHandle.Value );
		}
		
		// bool
		public bool LoadItemDefinitions()
		{
			return platform.ISteamInventory_LoadItemDefinitions();
		}
		
		// void
		public void SendItemDropHeartbeat()
		{
			platform.ISteamInventory_SendItemDropHeartbeat();
		}
		
		// bool
		public bool SerializeResult( SteamInventoryResult_t resultHandle /*SteamInventoryResult_t*/, IntPtr pOutBuffer /*void **/, out uint punOutBufferSize /*uint32 **/ )
		{
			return platform.ISteamInventory_SerializeResult( resultHandle.Value, (IntPtr) pOutBuffer, out punOutBufferSize );
		}
		
		// bool
		public bool TradeItems( ref SteamInventoryResult_t pResultHandle /*SteamInventoryResult_t **/, CSteamID steamIDTradePartner /*class CSteamID*/, SteamItemInstanceID_t[] pArrayGive /*const SteamItemInstanceID_t **/, uint[] pArrayGiveQuantity /*const uint32 **/, uint nArrayGiveLength /*uint32*/, SteamItemInstanceID_t[] pArrayGet /*const SteamItemInstanceID_t **/, uint[] pArrayGetQuantity /*const uint32 **/, uint nArrayGetLength /*uint32*/ )
		{
			return platform.ISteamInventory_TradeItems( ref pResultHandle.Value, steamIDTradePartner.Value, Array.ConvertAll( pArrayGive, p => p.Value ), pArrayGiveQuantity, nArrayGiveLength, Array.ConvertAll( pArrayGet, p => p.Value ), pArrayGetQuantity, nArrayGetLength );
		}
		
		// bool
		public bool TransferItemQuantity( ref SteamInventoryResult_t pResultHandle /*SteamInventoryResult_t **/, SteamItemInstanceID_t itemIdSource /*SteamItemInstanceID_t*/, uint unQuantity /*uint32*/, SteamItemInstanceID_t itemIdDest /*SteamItemInstanceID_t*/ )
		{
			return platform.ISteamInventory_TransferItemQuantity( ref pResultHandle.Value, itemIdSource.Value, unQuantity, itemIdDest.Value );
		}
		
		// bool
		public bool TriggerItemDrop( ref SteamInventoryResult_t pResultHandle /*SteamInventoryResult_t **/, SteamItemDef_t dropListDefinition /*SteamItemDef_t*/ )
		{
			return platform.ISteamInventory_TriggerItemDrop( ref pResultHandle.Value, dropListDefinition.Value );
		}
		
	}
}
