using Facepunch.Steamworks;
using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

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
			return platform.ISteamInventory_AddPromoItems( ref pResultHandle.Value, pArrayItemDefs.Select( x => x.Value ).ToArray(), unArrayLength );
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
			return platform.ISteamInventory_ExchangeItems( ref pResultHandle.Value, pArrayGenerate.Select( x => x.Value ).ToArray(), punArrayGenerateQuantity, unArrayGenerateLength, pArrayDestroy.Select( x => x.Value ).ToArray(), punArrayDestroyQuantity, unArrayDestroyLength );
		}
		
		// bool
		public bool GenerateItems( ref SteamInventoryResult_t pResultHandle /*SteamInventoryResult_t **/, SteamItemDef_t[] pArrayItemDefs /*const SteamItemDef_t **/, uint[] punArrayQuantity /*const uint32 **/, uint unArrayLength /*uint32*/ )
		{
			return platform.ISteamInventory_GenerateItems( ref pResultHandle.Value, pArrayItemDefs.Select( x => x.Value ).ToArray(), punArrayQuantity, unArrayLength );
		}
		
		// bool
		public bool GetAllItems( ref SteamInventoryResult_t pResultHandle /*SteamInventoryResult_t **/ )
		{
			return platform.ISteamInventory_GetAllItems( ref pResultHandle.Value );
		}
		
		// bool
		// using: Detect_MultiSizeArrayReturn
		public SteamItemDef_t[] GetEligiblePromoItemDefinitionIDs( CSteamID steamID /*class CSteamID*/ )
		{
			uint punItemDefIDsArraySize = 0;
			
			bool success = false;
			success = platform.ISteamInventory_GetEligiblePromoItemDefinitionIDs( steamID.Value, IntPtr.Zero, out punItemDefIDsArraySize );
			if ( !success || punItemDefIDsArraySize == 0) return null;
			
			var pItemDefIDs = new SteamItemDef_t[punItemDefIDsArraySize];
			fixed ( void* pItemDefIDs_ptr = pItemDefIDs )
			{
				success = platform.ISteamInventory_GetEligiblePromoItemDefinitionIDs( steamID.Value, (IntPtr) pItemDefIDs_ptr, out punItemDefIDsArraySize );
				if ( !success ) return null;
				return pItemDefIDs;
			}
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
			bSuccess = platform.ISteamInventory_GetItemDefinitionProperty( iDefinition.Value, Utility.GetUtf8Bytes(pchPropertyName), pchValueBuffer_sb, out punValueBufferSizeOut );
			if ( !bSuccess ) return bSuccess;
			pchValueBuffer = pchValueBuffer_sb.ToString();
			return bSuccess;
		}
		
		// bool
		public bool GetItemPrice( SteamItemDef_t iDefinition /*SteamItemDef_t*/, out ulong pPrice /*uint64 **/ )
		{
			return platform.ISteamInventory_GetItemPrice( iDefinition.Value, out pPrice );
		}
		
		// bool
		public bool GetItemsByID( ref SteamInventoryResult_t pResultHandle /*SteamInventoryResult_t **/, SteamItemInstanceID_t[] pInstanceIDs /*const SteamItemInstanceID_t **/, uint unCountInstanceIDs /*uint32*/ )
		{
			return platform.ISteamInventory_GetItemsByID( ref pResultHandle.Value, pInstanceIDs.Select( x => x.Value ).ToArray(), unCountInstanceIDs );
		}
		
		// bool
		public bool GetItemsWithPrices( IntPtr pArrayItemDefs /*SteamItemDef_t **/, IntPtr pPrices /*uint64 **/, uint unArrayLength /*uint32*/ )
		{
			return platform.ISteamInventory_GetItemsWithPrices( (IntPtr) pArrayItemDefs, (IntPtr) pPrices, unArrayLength );
		}
		
		// uint
		public uint GetNumItemsWithPrices()
		{
			return platform.ISteamInventory_GetNumItemsWithPrices();
		}
		
		// bool
		// with: Detect_StringFetch False
		public bool GetResultItemProperty( SteamInventoryResult_t resultHandle /*SteamInventoryResult_t*/, uint unItemIndex /*uint32*/, string pchPropertyName /*const char **/, out string pchValueBuffer /*char **/ )
		{
			bool bSuccess = default( bool );
			pchValueBuffer = string.Empty;
			System.Text.StringBuilder pchValueBuffer_sb = Helpers.TakeStringBuilder();
			uint punValueBufferSizeOut = 4096;
			bSuccess = platform.ISteamInventory_GetResultItemProperty( resultHandle.Value, unItemIndex, Utility.GetUtf8Bytes(pchPropertyName), pchValueBuffer_sb, out punValueBufferSizeOut );
			if ( !bSuccess ) return bSuccess;
			pchValueBuffer = pchValueBuffer_sb.ToString();
			return bSuccess;
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
		
		// bool
		public bool RemoveProperty( SteamInventoryUpdateHandle_t handle /*SteamInventoryUpdateHandle_t*/, SteamItemInstanceID_t nItemID /*SteamItemInstanceID_t*/, string pchPropertyName /*const char **/ )
		{
			return platform.ISteamInventory_RemoveProperty( handle.Value, nItemID.Value, Utility.GetUtf8Bytes(pchPropertyName) );
		}
		
		// SteamAPICall_t
		public CallbackHandle RequestEligiblePromoItemDefinitionsIDs( CSteamID steamID /*class CSteamID*/, Action<SteamInventoryEligiblePromoItemDefIDs_t, bool> CallbackFunction = null /*Action<SteamInventoryEligiblePromoItemDefIDs_t, bool>*/ )
		{
			SteamAPICall_t callback = 0;
			callback = platform.ISteamInventory_RequestEligiblePromoItemDefinitionsIDs( steamID.Value );
			
			if ( CallbackFunction == null ) return null;
			if ( callback == 0 ) return null;
			
			return SteamInventoryEligiblePromoItemDefIDs_t.CallResult( steamworks, callback, CallbackFunction );
		}
		
		// SteamAPICall_t
		public CallbackHandle RequestPrices( Action<SteamInventoryRequestPricesResult_t, bool> CallbackFunction = null /*Action<SteamInventoryRequestPricesResult_t, bool>*/ )
		{
			SteamAPICall_t callback = 0;
			callback = platform.ISteamInventory_RequestPrices();
			
			if ( CallbackFunction == null ) return null;
			if ( callback == 0 ) return null;
			
			return SteamInventoryRequestPricesResult_t.CallResult( steamworks, callback, CallbackFunction );
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
		public bool SetProperty( SteamInventoryUpdateHandle_t handle /*SteamInventoryUpdateHandle_t*/, SteamItemInstanceID_t nItemID /*SteamItemInstanceID_t*/, string pchPropertyName /*const char **/, string pchPropertyValue /*const char **/ )
		{
			return platform.ISteamInventory_SetProperty( handle.Value, nItemID.Value, Utility.GetUtf8Bytes(pchPropertyName), Utility.GetUtf8Bytes(pchPropertyValue) );
		}
		
		// bool
		public bool SetProperty0( SteamInventoryUpdateHandle_t handle /*SteamInventoryUpdateHandle_t*/, SteamItemInstanceID_t nItemID /*SteamItemInstanceID_t*/, string pchPropertyName /*const char **/, bool bValue /*bool*/ )
		{
			return platform.ISteamInventory_SetProperty0( handle.Value, nItemID.Value, Utility.GetUtf8Bytes(pchPropertyName), bValue );
		}
		
		// bool
		public bool SetProperty1( SteamInventoryUpdateHandle_t handle /*SteamInventoryUpdateHandle_t*/, SteamItemInstanceID_t nItemID /*SteamItemInstanceID_t*/, string pchPropertyName /*const char **/, long nValue /*int64*/ )
		{
			return platform.ISteamInventory_SetProperty0( handle.Value, nItemID.Value, Utility.GetUtf8Bytes(pchPropertyName), nValue );
		}
		
		// bool
		public bool SetProperty2( SteamInventoryUpdateHandle_t handle /*SteamInventoryUpdateHandle_t*/, SteamItemInstanceID_t nItemID /*SteamItemInstanceID_t*/, string pchPropertyName /*const char **/, float flValue /*float*/ )
		{
			return platform.ISteamInventory_SetProperty0( handle.Value, nItemID.Value, Utility.GetUtf8Bytes(pchPropertyName), flValue );
		}
		
		// SteamAPICall_t
		public CallbackHandle StartPurchase( SteamItemDef_t[] pArrayItemDefs /*const SteamItemDef_t **/, uint[] punArrayQuantity /*const uint32 **/, uint unArrayLength /*uint32*/, Action<SteamInventoryStartPurchaseResult_t, bool> CallbackFunction = null /*Action<SteamInventoryStartPurchaseResult_t, bool>*/ )
		{
			SteamAPICall_t callback = 0;
			callback = platform.ISteamInventory_StartPurchase( pArrayItemDefs.Select( x => x.Value ).ToArray(), punArrayQuantity, unArrayLength );
			
			if ( CallbackFunction == null ) return null;
			if ( callback == 0 ) return null;
			
			return SteamInventoryStartPurchaseResult_t.CallResult( steamworks, callback, CallbackFunction );
		}
		
		// SteamInventoryUpdateHandle_t
		public SteamInventoryUpdateHandle_t StartUpdateProperties()
		{
			return platform.ISteamInventory_StartUpdateProperties();
		}
		
		// bool
		public bool SubmitUpdateProperties( SteamInventoryUpdateHandle_t handle /*SteamInventoryUpdateHandle_t*/, ref SteamInventoryResult_t pResultHandle /*SteamInventoryResult_t **/ )
		{
			return platform.ISteamInventory_SubmitUpdateProperties( handle.Value, ref pResultHandle.Value );
		}
		
		// bool
		public bool TradeItems( ref SteamInventoryResult_t pResultHandle /*SteamInventoryResult_t **/, CSteamID steamIDTradePartner /*class CSteamID*/, SteamItemInstanceID_t[] pArrayGive /*const SteamItemInstanceID_t **/, uint[] pArrayGiveQuantity /*const uint32 **/, uint nArrayGiveLength /*uint32*/, SteamItemInstanceID_t[] pArrayGet /*const SteamItemInstanceID_t **/, uint[] pArrayGetQuantity /*const uint32 **/, uint nArrayGetLength /*uint32*/ )
		{
			return platform.ISteamInventory_TradeItems( ref pResultHandle.Value, steamIDTradePartner.Value, pArrayGive.Select( x => x.Value ).ToArray(), pArrayGiveQuantity, nArrayGiveLength, pArrayGet.Select( x => x.Value ).ToArray(), pArrayGetQuantity, nArrayGetLength );
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
