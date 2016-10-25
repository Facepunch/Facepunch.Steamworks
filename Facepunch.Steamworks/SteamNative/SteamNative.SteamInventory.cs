using System;
using System.Runtime.InteropServices;

namespace SteamNative
{
	public unsafe class SteamInventory
	{
		internal IntPtr _ptr;
		
		public SteamInventory( IntPtr pointer )
		{
			_ptr = pointer;
		}
		
		
		// bool
		public bool AddPromoItem( ref SteamInventoryResult_t pResultHandle /*SteamInventoryResult_t **/, SteamItemDef_t itemDef /*SteamItemDef_t*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamInventory.AddPromoItem( _ptr, ref pResultHandle, itemDef );
			else return Platform.Win64.ISteamInventory.AddPromoItem( _ptr, ref pResultHandle, itemDef );
		}
		
		// bool
		public bool AddPromoItems( ref SteamInventoryResult_t pResultHandle /*SteamInventoryResult_t **/, IntPtr pArrayItemDefs /*const SteamItemDef_t **/, uint unArrayLength /*uint32*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamInventory.AddPromoItems( _ptr, ref pResultHandle, (IntPtr) pArrayItemDefs, unArrayLength );
			else return Platform.Win64.ISteamInventory.AddPromoItems( _ptr, ref pResultHandle, (IntPtr) pArrayItemDefs, unArrayLength );
		}
		
		// bool
		public bool CheckResultSteamID( SteamInventoryResult_t resultHandle /*SteamInventoryResult_t*/, CSteamID steamIDExpected /*class CSteamID*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamInventory.CheckResultSteamID( _ptr, resultHandle, steamIDExpected );
			else return Platform.Win64.ISteamInventory.CheckResultSteamID( _ptr, resultHandle, steamIDExpected );
		}
		
		// bool
		public bool ConsumeItem( ref SteamInventoryResult_t pResultHandle /*SteamInventoryResult_t **/, SteamItemInstanceID_t itemConsume /*SteamItemInstanceID_t*/, uint unQuantity /*uint32*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamInventory.ConsumeItem( _ptr, ref pResultHandle, itemConsume, unQuantity );
			else return Platform.Win64.ISteamInventory.ConsumeItem( _ptr, ref pResultHandle, itemConsume, unQuantity );
		}
		
		// bool
		public bool DeserializeResult( ref SteamInventoryResult_t pOutResultHandle /*SteamInventoryResult_t **/, IntPtr pBuffer /*const void **/, uint unBufferSize /*uint32*/, bool bRESERVED_MUST_BE_FALSE /*bool*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamInventory.DeserializeResult( _ptr, ref pOutResultHandle, (IntPtr) pBuffer, unBufferSize, bRESERVED_MUST_BE_FALSE );
			else return Platform.Win64.ISteamInventory.DeserializeResult( _ptr, ref pOutResultHandle, (IntPtr) pBuffer, unBufferSize, bRESERVED_MUST_BE_FALSE );
		}
		
		// void
		public void DestroyResult( SteamInventoryResult_t resultHandle /*SteamInventoryResult_t*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) Platform.Win32.ISteamInventory.DestroyResult( _ptr, resultHandle );
			else Platform.Win64.ISteamInventory.DestroyResult( _ptr, resultHandle );
		}
		
		// bool
		public bool ExchangeItems( ref SteamInventoryResult_t pResultHandle /*SteamInventoryResult_t **/, ref SteamItemDef_t pArrayGenerate /*const SteamItemDef_t **/, out uint punArrayGenerateQuantity /*const uint32 **/, uint unArrayGenerateLength /*uint32*/, IntPtr pArrayDestroy /*const SteamItemInstanceID_t **/, IntPtr punArrayDestroyQuantity /*const uint32 **/, uint unArrayDestroyLength /*uint32*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamInventory.ExchangeItems( _ptr, ref pResultHandle, ref pArrayGenerate, out punArrayGenerateQuantity, unArrayGenerateLength, (IntPtr) pArrayDestroy, (IntPtr) punArrayDestroyQuantity, unArrayDestroyLength );
			else return Platform.Win64.ISteamInventory.ExchangeItems( _ptr, ref pResultHandle, ref pArrayGenerate, out punArrayGenerateQuantity, unArrayGenerateLength, (IntPtr) pArrayDestroy, (IntPtr) punArrayDestroyQuantity, unArrayDestroyLength );
		}
		
		// bool
		public bool GenerateItems( ref SteamInventoryResult_t pResultHandle /*SteamInventoryResult_t **/, IntPtr pArrayItemDefs /*const SteamItemDef_t **/, out uint punArrayQuantity /*const uint32 **/, uint unArrayLength /*uint32*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamInventory.GenerateItems( _ptr, ref pResultHandle, (IntPtr) pArrayItemDefs, out punArrayQuantity, unArrayLength );
			else return Platform.Win64.ISteamInventory.GenerateItems( _ptr, ref pResultHandle, (IntPtr) pArrayItemDefs, out punArrayQuantity, unArrayLength );
		}
		
		// bool
		public bool GetAllItems( ref SteamInventoryResult_t pResultHandle /*SteamInventoryResult_t **/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamInventory.GetAllItems( _ptr, ref pResultHandle );
			else return Platform.Win64.ISteamInventory.GetAllItems( _ptr, ref pResultHandle );
		}
		
		// bool
		// using: Detect_MultiSizeArrayReturn
		public SteamItemDef_t[] GetItemDefinitionIDs()
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			uint punItemDefIDsArraySize = 0;
			
			bool success = false;
			if ( Platform.IsWindows32 ) success = Platform.Win32.ISteamInventory.GetItemDefinitionIDs( _ptr, IntPtr.Zero, out punItemDefIDsArraySize );
			else success = Platform.Win64.ISteamInventory.GetItemDefinitionIDs( _ptr, IntPtr.Zero, out punItemDefIDsArraySize );
			if ( !success || punItemDefIDsArraySize == 0) return null;
			
			var pItemDefIDs = new SteamItemDef_t[punItemDefIDsArraySize];
			fixed ( void* pItemDefIDs_ptr = pItemDefIDs )
			{
				if ( Platform.IsWindows32 ) success = Platform.Win32.ISteamInventory.GetItemDefinitionIDs( _ptr, (IntPtr) pItemDefIDs_ptr, out punItemDefIDsArraySize );
				else success = Platform.Win64.ISteamInventory.GetItemDefinitionIDs( _ptr, (IntPtr) pItemDefIDs_ptr, out punItemDefIDsArraySize );
				if ( !success ) return null;
				return pItemDefIDs;
			}
		}
		
		// bool
		// with: Detect_StringFetch False
		public bool GetItemDefinitionProperty( SteamItemDef_t iDefinition /*SteamItemDef_t*/, string pchPropertyName /*const char **/, out string pchValueBuffer /*char **/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			bool bSuccess = default( bool );
			pchValueBuffer = string.Empty;
			System.Text.StringBuilder pchValueBuffer_sb = new System.Text.StringBuilder( 4096 );
			uint punValueBufferSize = 4096;
			if ( Platform.IsWindows32 ) bSuccess = Platform.Win32.ISteamInventory.GetItemDefinitionProperty( _ptr, iDefinition, pchPropertyName, pchValueBuffer_sb, out punValueBufferSize );
			else bSuccess = Platform.Win64.ISteamInventory.GetItemDefinitionProperty( _ptr, iDefinition, pchPropertyName, pchValueBuffer_sb, out punValueBufferSize );
			if ( !bSuccess ) return bSuccess;
			pchValueBuffer = pchValueBuffer_sb.ToString();
			return bSuccess;
		}
		
		// bool
		public bool GetItemsByID( ref SteamInventoryResult_t pResultHandle /*SteamInventoryResult_t **/, IntPtr pInstanceIDs /*const SteamItemInstanceID_t **/, uint unCountInstanceIDs /*uint32*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamInventory.GetItemsByID( _ptr, ref pResultHandle, (IntPtr) pInstanceIDs, unCountInstanceIDs );
			else return Platform.Win64.ISteamInventory.GetItemsByID( _ptr, ref pResultHandle, (IntPtr) pInstanceIDs, unCountInstanceIDs );
		}
		
		// bool
		// using: Detect_MultiSizeArrayReturn
		public SteamItemDetails_t[] GetResultItems( SteamInventoryResult_t resultHandle /*SteamInventoryResult_t*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			uint punOutItemsArraySize = 0;
			
			bool success = false;
			if ( Platform.IsWindows32 ) success = Platform.Win32.ISteamInventory.GetResultItems( _ptr, resultHandle, IntPtr.Zero, out punOutItemsArraySize );
			else success = Platform.Win64.ISteamInventory.GetResultItems( _ptr, resultHandle, IntPtr.Zero, out punOutItemsArraySize );
			if ( !success || punOutItemsArraySize == 0) return null;
			
			var pOutItemsArray = new SteamItemDetails_t[punOutItemsArraySize];
			fixed ( void* pOutItemsArray_ptr = pOutItemsArray )
			{
				if ( Platform.IsWindows32 ) success = Platform.Win32.ISteamInventory.GetResultItems( _ptr, resultHandle, (IntPtr) pOutItemsArray_ptr, out punOutItemsArraySize );
				else success = Platform.Win64.ISteamInventory.GetResultItems( _ptr, resultHandle, (IntPtr) pOutItemsArray_ptr, out punOutItemsArraySize );
				if ( !success ) return null;
				return pOutItemsArray;
			}
		}
		
		// Result
		public Result GetResultStatus( SteamInventoryResult_t resultHandle /*SteamInventoryResult_t*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamInventory.GetResultStatus( _ptr, resultHandle );
			else return Platform.Win64.ISteamInventory.GetResultStatus( _ptr, resultHandle );
		}
		
		// uint
		public uint GetResultTimestamp( SteamInventoryResult_t resultHandle /*SteamInventoryResult_t*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamInventory.GetResultTimestamp( _ptr, resultHandle );
			else return Platform.Win64.ISteamInventory.GetResultTimestamp( _ptr, resultHandle );
		}
		
		// bool
		public bool GrantPromoItems( ref SteamInventoryResult_t pResultHandle /*SteamInventoryResult_t **/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamInventory.GrantPromoItems( _ptr, ref pResultHandle );
			else return Platform.Win64.ISteamInventory.GrantPromoItems( _ptr, ref pResultHandle );
		}
		
		// bool
		public bool LoadItemDefinitions()
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamInventory.LoadItemDefinitions( _ptr );
			else return Platform.Win64.ISteamInventory.LoadItemDefinitions( _ptr );
		}
		
		// void
		public void SendItemDropHeartbeat()
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) Platform.Win32.ISteamInventory.SendItemDropHeartbeat( _ptr );
			else Platform.Win64.ISteamInventory.SendItemDropHeartbeat( _ptr );
		}
		
		// bool
		public bool SerializeResult( SteamInventoryResult_t resultHandle /*SteamInventoryResult_t*/, IntPtr pOutBuffer /*void **/, out uint punOutBufferSize /*uint32 **/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamInventory.SerializeResult( _ptr, resultHandle, (IntPtr) pOutBuffer, out punOutBufferSize );
			else return Platform.Win64.ISteamInventory.SerializeResult( _ptr, resultHandle, (IntPtr) pOutBuffer, out punOutBufferSize );
		}
		
		// bool
		public bool TradeItems( ref SteamInventoryResult_t pResultHandle /*SteamInventoryResult_t **/, CSteamID steamIDTradePartner /*class CSteamID*/, ref SteamItemInstanceID_t pArrayGive /*const SteamItemInstanceID_t **/, out uint pArrayGiveQuantity /*const uint32 **/, uint nArrayGiveLength /*uint32*/, ref SteamItemInstanceID_t pArrayGet /*const SteamItemInstanceID_t **/, out uint pArrayGetQuantity /*const uint32 **/, uint nArrayGetLength /*uint32*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamInventory.TradeItems( _ptr, ref pResultHandle, steamIDTradePartner, ref pArrayGive, out pArrayGiveQuantity, nArrayGiveLength, ref pArrayGet, out pArrayGetQuantity, nArrayGetLength );
			else return Platform.Win64.ISteamInventory.TradeItems( _ptr, ref pResultHandle, steamIDTradePartner, ref pArrayGive, out pArrayGiveQuantity, nArrayGiveLength, ref pArrayGet, out pArrayGetQuantity, nArrayGetLength );
		}
		
		// bool
		public bool TransferItemQuantity( ref SteamInventoryResult_t pResultHandle /*SteamInventoryResult_t **/, SteamItemInstanceID_t itemIdSource /*SteamItemInstanceID_t*/, uint unQuantity /*uint32*/, SteamItemInstanceID_t itemIdDest /*SteamItemInstanceID_t*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamInventory.TransferItemQuantity( _ptr, ref pResultHandle, itemIdSource, unQuantity, itemIdDest );
			else return Platform.Win64.ISteamInventory.TransferItemQuantity( _ptr, ref pResultHandle, itemIdSource, unQuantity, itemIdDest );
		}
		
		// bool
		public bool TriggerItemDrop( ref SteamInventoryResult_t pResultHandle /*SteamInventoryResult_t **/, SteamItemDef_t dropListDefinition /*SteamItemDef_t*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamInventory.TriggerItemDrop( _ptr, ref pResultHandle, dropListDefinition );
			else return Platform.Win64.ISteamInventory.TriggerItemDrop( _ptr, ref pResultHandle, dropListDefinition );
		}
		
	}
}
