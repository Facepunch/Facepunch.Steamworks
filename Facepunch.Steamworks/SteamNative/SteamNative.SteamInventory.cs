using System;
using System.Runtime.InteropServices;

namespace SteamNative
{
	public unsafe class SteamInventory : IDisposable
	{
		//
		// Holds a platform specific implentation
		//
		internal Platform.Interface _pi;
		
		//
		// Constructor decides which implementation to use based on current platform
		//
		public SteamInventory( IntPtr pointer )
		{
			if ( Platform.IsWindows64 ) _pi = new Platform.Win64( pointer );
			else if ( Platform.IsWindows32 ) _pi = new Platform.Win32( pointer );
			else if ( Platform.IsLinux32 ) _pi = new Platform.Linux32( pointer );
			else if ( Platform.IsLinux64 ) _pi = new Platform.Linux64( pointer );
			else if ( Platform.IsOsx ) _pi = new Platform.Mac( pointer );
		}
		
		//
		// Class is invalid if we don't have a valid implementation
		//
		public bool IsValid{ get{ return _pi != null && _pi.IsValid; } }
		
		//
		// When shutting down clear all the internals to avoid accidental use
		//
		public virtual void Dispose()
		{
			 if ( _pi != null )
			{
				_pi.Dispose();
				_pi = null;
			}
		}
		
		// bool
		public bool AddPromoItem( ref SteamInventoryResult_t pResultHandle /*SteamInventoryResult_t **/, SteamItemDef_t itemDef /*SteamItemDef_t*/ )
		{
			return _pi.ISteamInventory_AddPromoItem( ref pResultHandle.Value /*A*/, itemDef.Value /*C*/ );
		}
		
		// bool
		public bool AddPromoItems( ref SteamInventoryResult_t pResultHandle /*SteamInventoryResult_t **/, IntPtr pArrayItemDefs /*const SteamItemDef_t **/, uint unArrayLength /*uint32*/ )
		{
			return _pi.ISteamInventory_AddPromoItems( ref pResultHandle.Value /*A*/, (IntPtr) pArrayItemDefs, unArrayLength /*C*/ );
		}
		
		// bool
		public bool CheckResultSteamID( SteamInventoryResult_t resultHandle /*SteamInventoryResult_t*/, CSteamID steamIDExpected /*class CSteamID*/ )
		{
			return _pi.ISteamInventory_CheckResultSteamID( resultHandle.Value /*C*/, steamIDExpected.Value /*C*/ );
		}
		
		// bool
		public bool ConsumeItem( ref SteamInventoryResult_t pResultHandle /*SteamInventoryResult_t **/, SteamItemInstanceID_t itemConsume /*SteamItemInstanceID_t*/, uint unQuantity /*uint32*/ )
		{
			return _pi.ISteamInventory_ConsumeItem( ref pResultHandle.Value /*A*/, itemConsume.Value /*C*/, unQuantity /*C*/ );
		}
		
		// bool
		public bool DeserializeResult( ref SteamInventoryResult_t pOutResultHandle /*SteamInventoryResult_t **/, IntPtr pBuffer /*const void **/, uint unBufferSize /*uint32*/, bool bRESERVED_MUST_BE_FALSE /*bool*/ )
		{
			return _pi.ISteamInventory_DeserializeResult( ref pOutResultHandle.Value /*A*/, (IntPtr) pBuffer /*C*/, unBufferSize /*C*/, bRESERVED_MUST_BE_FALSE /*C*/ );
		}
		
		// void
		public void DestroyResult( SteamInventoryResult_t resultHandle /*SteamInventoryResult_t*/ )
		{
			_pi.ISteamInventory_DestroyResult( resultHandle.Value /*C*/ );
		}
		
		// bool
		public bool ExchangeItems( ref SteamInventoryResult_t pResultHandle /*SteamInventoryResult_t **/, ref SteamItemDef_t pArrayGenerate /*const SteamItemDef_t **/, out uint punArrayGenerateQuantity /*const uint32 **/, uint unArrayGenerateLength /*uint32*/, IntPtr pArrayDestroy /*const SteamItemInstanceID_t **/, IntPtr punArrayDestroyQuantity /*const uint32 **/, uint unArrayDestroyLength /*uint32*/ )
		{
			return _pi.ISteamInventory_ExchangeItems( ref pResultHandle.Value /*A*/, ref pArrayGenerate.Value /*A*/, out punArrayGenerateQuantity /*B*/, unArrayGenerateLength /*C*/, (IntPtr) pArrayDestroy, (IntPtr) punArrayDestroyQuantity, unArrayDestroyLength /*C*/ );
		}
		
		// bool
		public bool GenerateItems( ref SteamInventoryResult_t pResultHandle /*SteamInventoryResult_t **/, IntPtr pArrayItemDefs /*const SteamItemDef_t **/, out uint punArrayQuantity /*const uint32 **/, uint unArrayLength /*uint32*/ )
		{
			return _pi.ISteamInventory_GenerateItems( ref pResultHandle.Value /*A*/, (IntPtr) pArrayItemDefs, out punArrayQuantity /*B*/, unArrayLength /*C*/ );
		}
		
		// bool
		public bool GetAllItems( ref SteamInventoryResult_t pResultHandle /*SteamInventoryResult_t **/ )
		{
			return _pi.ISteamInventory_GetAllItems( ref pResultHandle.Value /*A*/ );
		}
		
		// bool
		// using: Detect_MultiSizeArrayReturn
		public SteamItemDef_t[] GetItemDefinitionIDs()
		{
			uint punItemDefIDsArraySize = 0;
			
			bool success = false;
			success = _pi.ISteamInventory_GetItemDefinitionIDs( IntPtr.Zero, out punItemDefIDsArraySize /*B*/ );
			if ( !success || punItemDefIDsArraySize == 0) return null;
			
			var pItemDefIDs = new SteamItemDef_t[punItemDefIDsArraySize];
			fixed ( void* pItemDefIDs_ptr = pItemDefIDs )
			{
				success = _pi.ISteamInventory_GetItemDefinitionIDs( (IntPtr) pItemDefIDs_ptr /*C*/, out punItemDefIDsArraySize /*B*/ );
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
			System.Text.StringBuilder pchValueBuffer_sb = new System.Text.StringBuilder( 4096 );
			uint punValueBufferSize = 4096;
			bSuccess = _pi.ISteamInventory_GetItemDefinitionProperty( iDefinition.Value /*C*/, pchPropertyName /*C*/, pchValueBuffer_sb /*C*/, out punValueBufferSize /*B*/ );
			if ( !bSuccess ) return bSuccess;
			pchValueBuffer = pchValueBuffer_sb.ToString();
			return bSuccess;
		}
		
		// bool
		public bool GetItemsByID( ref SteamInventoryResult_t pResultHandle /*SteamInventoryResult_t **/, IntPtr pInstanceIDs /*const SteamItemInstanceID_t **/, uint unCountInstanceIDs /*uint32*/ )
		{
			return _pi.ISteamInventory_GetItemsByID( ref pResultHandle.Value /*A*/, (IntPtr) pInstanceIDs, unCountInstanceIDs /*C*/ );
		}
		
		// bool
		// using: Detect_MultiSizeArrayReturn
		public SteamItemDetails_t[] GetResultItems( SteamInventoryResult_t resultHandle /*SteamInventoryResult_t*/ )
		{
			uint punOutItemsArraySize = 0;
			
			bool success = false;
			success = _pi.ISteamInventory_GetResultItems( resultHandle.Value /*C*/, IntPtr.Zero, out punOutItemsArraySize /*B*/ );
			if ( !success || punOutItemsArraySize == 0) return null;
			
			var pOutItemsArray = new SteamItemDetails_t[punOutItemsArraySize];
			fixed ( void* pOutItemsArray_ptr = pOutItemsArray )
			{
				success = _pi.ISteamInventory_GetResultItems( resultHandle.Value /*C*/, (IntPtr) pOutItemsArray_ptr /*C*/, out punOutItemsArraySize /*B*/ );
				if ( !success ) return null;
				return pOutItemsArray;
			}
		}
		
		// Result
		public Result GetResultStatus( SteamInventoryResult_t resultHandle /*SteamInventoryResult_t*/ )
		{
			return _pi.ISteamInventory_GetResultStatus( resultHandle.Value /*C*/ );
		}
		
		// uint
		public uint GetResultTimestamp( SteamInventoryResult_t resultHandle /*SteamInventoryResult_t*/ )
		{
			return _pi.ISteamInventory_GetResultTimestamp( resultHandle.Value /*C*/ );
		}
		
		// bool
		public bool GrantPromoItems( ref SteamInventoryResult_t pResultHandle /*SteamInventoryResult_t **/ )
		{
			return _pi.ISteamInventory_GrantPromoItems( ref pResultHandle.Value /*A*/ );
		}
		
		// bool
		public bool LoadItemDefinitions()
		{
			return _pi.ISteamInventory_LoadItemDefinitions();
		}
		
		// void
		public void SendItemDropHeartbeat()
		{
			_pi.ISteamInventory_SendItemDropHeartbeat();
		}
		
		// bool
		public bool SerializeResult( SteamInventoryResult_t resultHandle /*SteamInventoryResult_t*/, IntPtr pOutBuffer /*void **/, out uint punOutBufferSize /*uint32 **/ )
		{
			return _pi.ISteamInventory_SerializeResult( resultHandle.Value /*C*/, (IntPtr) pOutBuffer /*C*/, out punOutBufferSize /*B*/ );
		}
		
		// bool
		public bool TradeItems( ref SteamInventoryResult_t pResultHandle /*SteamInventoryResult_t **/, CSteamID steamIDTradePartner /*class CSteamID*/, ref SteamItemInstanceID_t pArrayGive /*const SteamItemInstanceID_t **/, out uint pArrayGiveQuantity /*const uint32 **/, uint nArrayGiveLength /*uint32*/, ref SteamItemInstanceID_t pArrayGet /*const SteamItemInstanceID_t **/, out uint pArrayGetQuantity /*const uint32 **/, uint nArrayGetLength /*uint32*/ )
		{
			return _pi.ISteamInventory_TradeItems( ref pResultHandle.Value /*A*/, steamIDTradePartner.Value /*C*/, ref pArrayGive.Value /*A*/, out pArrayGiveQuantity /*B*/, nArrayGiveLength /*C*/, ref pArrayGet.Value /*A*/, out pArrayGetQuantity /*B*/, nArrayGetLength /*C*/ );
		}
		
		// bool
		public bool TransferItemQuantity( ref SteamInventoryResult_t pResultHandle /*SteamInventoryResult_t **/, SteamItemInstanceID_t itemIdSource /*SteamItemInstanceID_t*/, uint unQuantity /*uint32*/, SteamItemInstanceID_t itemIdDest /*SteamItemInstanceID_t*/ )
		{
			return _pi.ISteamInventory_TransferItemQuantity( ref pResultHandle.Value /*A*/, itemIdSource.Value /*C*/, unQuantity /*C*/, itemIdDest.Value /*C*/ );
		}
		
		// bool
		public bool TriggerItemDrop( ref SteamInventoryResult_t pResultHandle /*SteamInventoryResult_t **/, SteamItemDef_t dropListDefinition /*SteamItemDef_t*/ )
		{
			return _pi.ISteamInventory_TriggerItemDrop( ref pResultHandle.Value /*A*/, dropListDefinition.Value /*C*/ );
		}
		
	}
}
