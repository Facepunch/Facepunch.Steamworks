using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Steamworks.Data;


namespace Steamworks
{
	internal class ISteamInventory : SteamInterface
	{
		public override IntPtr GetInterfacePointer() => GetApi.SteamInventory();
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamInventory_GetResultStatus")]
		private static extern Result _GetResultStatus( IntPtr self, SteamInventoryResult_t resultHandle );
		
		#endregion
		internal Result GetResultStatus( SteamInventoryResult_t resultHandle )
		{
			var returnValue = _GetResultStatus( Self, resultHandle );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamInventory_GetResultItems")]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _GetResultItems( IntPtr self, SteamInventoryResult_t resultHandle, [In,Out] SteamItemDetails_t[]  pOutItemsArray, ref uint punOutItemsArraySize );
		
		#endregion
		internal bool GetResultItems( SteamInventoryResult_t resultHandle, [In,Out] SteamItemDetails_t[]  pOutItemsArray, ref uint punOutItemsArraySize )
		{
			var returnValue = _GetResultItems( Self, resultHandle, pOutItemsArray, ref punOutItemsArraySize );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamInventory_GetResultItemProperty")]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _GetResultItemProperty( IntPtr self, SteamInventoryResult_t resultHandle, uint unItemIndex, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchPropertyName, IntPtr pchValueBuffer, ref uint punValueBufferSizeOut );
		
		#endregion
		internal bool GetResultItemProperty( SteamInventoryResult_t resultHandle, uint unItemIndex, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchPropertyName, out string pchValueBuffer, ref uint punValueBufferSizeOut )
		{
			IntPtr mempchValueBuffer = Helpers.TakeMemory();
			var returnValue = _GetResultItemProperty( Self, resultHandle, unItemIndex, pchPropertyName, mempchValueBuffer, ref punValueBufferSizeOut );
			pchValueBuffer = Helpers.MemoryToString( mempchValueBuffer );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamInventory_GetResultTimestamp")]
		private static extern uint _GetResultTimestamp( IntPtr self, SteamInventoryResult_t resultHandle );
		
		#endregion
		internal uint GetResultTimestamp( SteamInventoryResult_t resultHandle )
		{
			var returnValue = _GetResultTimestamp( Self, resultHandle );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamInventory_CheckResultSteamID")]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _CheckResultSteamID( IntPtr self, SteamInventoryResult_t resultHandle, SteamId steamIDExpected );
		
		#endregion
		internal bool CheckResultSteamID( SteamInventoryResult_t resultHandle, SteamId steamIDExpected )
		{
			var returnValue = _CheckResultSteamID( Self, resultHandle, steamIDExpected );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamInventory_DestroyResult")]
		private static extern void _DestroyResult( IntPtr self, SteamInventoryResult_t resultHandle );
		
		#endregion
		internal void DestroyResult( SteamInventoryResult_t resultHandle )
		{
			_DestroyResult( Self, resultHandle );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamInventory_GetAllItems")]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _GetAllItems( IntPtr self, ref SteamInventoryResult_t pResultHandle );
		
		#endregion
		internal bool GetAllItems( ref SteamInventoryResult_t pResultHandle )
		{
			var returnValue = _GetAllItems( Self, ref pResultHandle );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamInventory_GetItemsByID")]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _GetItemsByID( IntPtr self, ref SteamInventoryResult_t pResultHandle, ref InventoryItemId pInstanceIDs, uint unCountInstanceIDs );
		
		#endregion
		internal bool GetItemsByID( ref SteamInventoryResult_t pResultHandle, ref InventoryItemId pInstanceIDs, uint unCountInstanceIDs )
		{
			var returnValue = _GetItemsByID( Self, ref pResultHandle, ref pInstanceIDs, unCountInstanceIDs );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamInventory_SerializeResult")]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _SerializeResult( IntPtr self, SteamInventoryResult_t resultHandle, IntPtr pOutBuffer, ref uint punOutBufferSize );
		
		#endregion
		internal bool SerializeResult( SteamInventoryResult_t resultHandle, IntPtr pOutBuffer, ref uint punOutBufferSize )
		{
			var returnValue = _SerializeResult( Self, resultHandle, pOutBuffer, ref punOutBufferSize );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamInventory_DeserializeResult")]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _DeserializeResult( IntPtr self, ref SteamInventoryResult_t pOutResultHandle, IntPtr pBuffer, uint unBufferSize, [MarshalAs( UnmanagedType.U1 )] bool bRESERVED_MUST_BE_FALSE );
		
		#endregion
		internal bool DeserializeResult( ref SteamInventoryResult_t pOutResultHandle, IntPtr pBuffer, uint unBufferSize, [MarshalAs( UnmanagedType.U1 )] bool bRESERVED_MUST_BE_FALSE )
		{
			var returnValue = _DeserializeResult( Self, ref pOutResultHandle, pBuffer, unBufferSize, bRESERVED_MUST_BE_FALSE );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamInventory_GenerateItems")]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _GenerateItems( IntPtr self, ref SteamInventoryResult_t pResultHandle, [In,Out] InventoryDefId[]  pArrayItemDefs, [In,Out] uint[]  punArrayQuantity, uint unArrayLength );
		
		#endregion
		internal bool GenerateItems( ref SteamInventoryResult_t pResultHandle, [In,Out] InventoryDefId[]  pArrayItemDefs, [In,Out] uint[]  punArrayQuantity, uint unArrayLength )
		{
			var returnValue = _GenerateItems( Self, ref pResultHandle, pArrayItemDefs, punArrayQuantity, unArrayLength );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamInventory_GrantPromoItems")]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _GrantPromoItems( IntPtr self, ref SteamInventoryResult_t pResultHandle );
		
		#endregion
		internal bool GrantPromoItems( ref SteamInventoryResult_t pResultHandle )
		{
			var returnValue = _GrantPromoItems( Self, ref pResultHandle );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamInventory_AddPromoItem")]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _AddPromoItem( IntPtr self, ref SteamInventoryResult_t pResultHandle, InventoryDefId itemDef );
		
		#endregion
		internal bool AddPromoItem( ref SteamInventoryResult_t pResultHandle, InventoryDefId itemDef )
		{
			var returnValue = _AddPromoItem( Self, ref pResultHandle, itemDef );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamInventory_AddPromoItems")]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _AddPromoItems( IntPtr self, ref SteamInventoryResult_t pResultHandle, [In,Out] InventoryDefId[]  pArrayItemDefs, uint unArrayLength );
		
		#endregion
		internal bool AddPromoItems( ref SteamInventoryResult_t pResultHandle, [In,Out] InventoryDefId[]  pArrayItemDefs, uint unArrayLength )
		{
			var returnValue = _AddPromoItems( Self, ref pResultHandle, pArrayItemDefs, unArrayLength );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamInventory_ConsumeItem")]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _ConsumeItem( IntPtr self, ref SteamInventoryResult_t pResultHandle, InventoryItemId itemConsume, uint unQuantity );
		
		#endregion
		internal bool ConsumeItem( ref SteamInventoryResult_t pResultHandle, InventoryItemId itemConsume, uint unQuantity )
		{
			var returnValue = _ConsumeItem( Self, ref pResultHandle, itemConsume, unQuantity );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamInventory_ExchangeItems")]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _ExchangeItems( IntPtr self, ref SteamInventoryResult_t pResultHandle, [In,Out] InventoryDefId[]  pArrayGenerate, [In,Out] uint[]  punArrayGenerateQuantity, uint unArrayGenerateLength, [In,Out] InventoryItemId[]  pArrayDestroy, [In,Out] uint[]  punArrayDestroyQuantity, uint unArrayDestroyLength );
		
		#endregion
		internal bool ExchangeItems( ref SteamInventoryResult_t pResultHandle, [In,Out] InventoryDefId[]  pArrayGenerate, [In,Out] uint[]  punArrayGenerateQuantity, uint unArrayGenerateLength, [In,Out] InventoryItemId[]  pArrayDestroy, [In,Out] uint[]  punArrayDestroyQuantity, uint unArrayDestroyLength )
		{
			var returnValue = _ExchangeItems( Self, ref pResultHandle, pArrayGenerate, punArrayGenerateQuantity, unArrayGenerateLength, pArrayDestroy, punArrayDestroyQuantity, unArrayDestroyLength );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamInventory_TransferItemQuantity")]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _TransferItemQuantity( IntPtr self, ref SteamInventoryResult_t pResultHandle, InventoryItemId itemIdSource, uint unQuantity, InventoryItemId itemIdDest );
		
		#endregion
		internal bool TransferItemQuantity( ref SteamInventoryResult_t pResultHandle, InventoryItemId itemIdSource, uint unQuantity, InventoryItemId itemIdDest )
		{
			var returnValue = _TransferItemQuantity( Self, ref pResultHandle, itemIdSource, unQuantity, itemIdDest );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamInventory_SendItemDropHeartbeat")]
		private static extern void _SendItemDropHeartbeat( IntPtr self );
		
		#endregion
		internal void SendItemDropHeartbeat()
		{
			_SendItemDropHeartbeat( Self );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamInventory_TriggerItemDrop")]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _TriggerItemDrop( IntPtr self, ref SteamInventoryResult_t pResultHandle, InventoryDefId dropListDefinition );
		
		#endregion
		internal bool TriggerItemDrop( ref SteamInventoryResult_t pResultHandle, InventoryDefId dropListDefinition )
		{
			var returnValue = _TriggerItemDrop( Self, ref pResultHandle, dropListDefinition );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamInventory_TradeItems")]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _TradeItems( IntPtr self, ref SteamInventoryResult_t pResultHandle, SteamId steamIDTradePartner, [In,Out] InventoryItemId[]  pArrayGive, [In,Out] uint[]  pArrayGiveQuantity, uint nArrayGiveLength, [In,Out] InventoryItemId[]  pArrayGet, [In,Out] uint[]  pArrayGetQuantity, uint nArrayGetLength );
		
		#endregion
		internal bool TradeItems( ref SteamInventoryResult_t pResultHandle, SteamId steamIDTradePartner, [In,Out] InventoryItemId[]  pArrayGive, [In,Out] uint[]  pArrayGiveQuantity, uint nArrayGiveLength, [In,Out] InventoryItemId[]  pArrayGet, [In,Out] uint[]  pArrayGetQuantity, uint nArrayGetLength )
		{
			var returnValue = _TradeItems( Self, ref pResultHandle, steamIDTradePartner, pArrayGive, pArrayGiveQuantity, nArrayGiveLength, pArrayGet, pArrayGetQuantity, nArrayGetLength );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamInventory_LoadItemDefinitions")]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _LoadItemDefinitions( IntPtr self );
		
		#endregion
		internal bool LoadItemDefinitions()
		{
			var returnValue = _LoadItemDefinitions( Self );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamInventory_GetItemDefinitionIDs")]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _GetItemDefinitionIDs( IntPtr self, [In,Out] InventoryDefId[]  pItemDefIDs, ref uint punItemDefIDsArraySize );
		
		#endregion
		internal bool GetItemDefinitionIDs( [In,Out] InventoryDefId[]  pItemDefIDs, ref uint punItemDefIDsArraySize )
		{
			var returnValue = _GetItemDefinitionIDs( Self, pItemDefIDs, ref punItemDefIDsArraySize );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamInventory_GetItemDefinitionProperty")]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _GetItemDefinitionProperty( IntPtr self, InventoryDefId iDefinition, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchPropertyName, IntPtr pchValueBuffer, ref uint punValueBufferSizeOut );
		
		#endregion
		internal bool GetItemDefinitionProperty( InventoryDefId iDefinition, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchPropertyName, out string pchValueBuffer, ref uint punValueBufferSizeOut )
		{
			IntPtr mempchValueBuffer = Helpers.TakeMemory();
			var returnValue = _GetItemDefinitionProperty( Self, iDefinition, pchPropertyName, mempchValueBuffer, ref punValueBufferSizeOut );
			pchValueBuffer = Helpers.MemoryToString( mempchValueBuffer );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamInventory_RequestEligiblePromoItemDefinitionsIDs")]
		private static extern SteamAPICall_t _RequestEligiblePromoItemDefinitionsIDs( IntPtr self, SteamId steamID );
		
		#endregion
		internal CallbackResult<SteamInventoryEligiblePromoItemDefIDs_t> RequestEligiblePromoItemDefinitionsIDs( SteamId steamID )
		{
			var returnValue = _RequestEligiblePromoItemDefinitionsIDs( Self, steamID );
			return new CallbackResult<SteamInventoryEligiblePromoItemDefIDs_t>( returnValue );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamInventory_GetEligiblePromoItemDefinitionIDs")]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _GetEligiblePromoItemDefinitionIDs( IntPtr self, SteamId steamID, [In,Out] InventoryDefId[]  pItemDefIDs, ref uint punItemDefIDsArraySize );
		
		#endregion
		internal bool GetEligiblePromoItemDefinitionIDs( SteamId steamID, [In,Out] InventoryDefId[]  pItemDefIDs, ref uint punItemDefIDsArraySize )
		{
			var returnValue = _GetEligiblePromoItemDefinitionIDs( Self, steamID, pItemDefIDs, ref punItemDefIDsArraySize );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamInventory_StartPurchase")]
		private static extern SteamAPICall_t _StartPurchase( IntPtr self, [In,Out] InventoryDefId[]  pArrayItemDefs, [In,Out] uint[]  punArrayQuantity, uint unArrayLength );
		
		#endregion
		internal CallbackResult<SteamInventoryStartPurchaseResult_t> StartPurchase( [In,Out] InventoryDefId[]  pArrayItemDefs, [In,Out] uint[]  punArrayQuantity, uint unArrayLength )
		{
			var returnValue = _StartPurchase( Self, pArrayItemDefs, punArrayQuantity, unArrayLength );
			return new CallbackResult<SteamInventoryStartPurchaseResult_t>( returnValue );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamInventory_RequestPrices")]
		private static extern SteamAPICall_t _RequestPrices( IntPtr self );
		
		#endregion
		internal CallbackResult<SteamInventoryRequestPricesResult_t> RequestPrices()
		{
			var returnValue = _RequestPrices( Self );
			return new CallbackResult<SteamInventoryRequestPricesResult_t>( returnValue );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamInventory_GetNumItemsWithPrices")]
		private static extern uint _GetNumItemsWithPrices( IntPtr self );
		
		#endregion
		internal uint GetNumItemsWithPrices()
		{
			var returnValue = _GetNumItemsWithPrices( Self );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamInventory_GetItemsWithPrices")]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _GetItemsWithPrices( IntPtr self, [In,Out] InventoryDefId[]  pArrayItemDefs, [In,Out] ulong[]  pCurrentPrices, [In,Out] ulong[]  pBasePrices, uint unArrayLength );
		
		#endregion
		internal bool GetItemsWithPrices( [In,Out] InventoryDefId[]  pArrayItemDefs, [In,Out] ulong[]  pCurrentPrices, [In,Out] ulong[]  pBasePrices, uint unArrayLength )
		{
			var returnValue = _GetItemsWithPrices( Self, pArrayItemDefs, pCurrentPrices, pBasePrices, unArrayLength );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamInventory_GetItemPrice")]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _GetItemPrice( IntPtr self, InventoryDefId iDefinition, ref ulong pCurrentPrice, ref ulong pBasePrice );
		
		#endregion
		internal bool GetItemPrice( InventoryDefId iDefinition, ref ulong pCurrentPrice, ref ulong pBasePrice )
		{
			var returnValue = _GetItemPrice( Self, iDefinition, ref pCurrentPrice, ref pBasePrice );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamInventory_StartUpdateProperties")]
		private static extern SteamInventoryUpdateHandle_t _StartUpdateProperties( IntPtr self );
		
		#endregion
		internal SteamInventoryUpdateHandle_t StartUpdateProperties()
		{
			var returnValue = _StartUpdateProperties( Self );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamInventory_RemoveProperty")]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _RemoveProperty( IntPtr self, SteamInventoryUpdateHandle_t handle, InventoryItemId nItemID, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchPropertyName );
		
		#endregion
		internal bool RemoveProperty( SteamInventoryUpdateHandle_t handle, InventoryItemId nItemID, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchPropertyName )
		{
			var returnValue = _RemoveProperty( Self, handle, nItemID, pchPropertyName );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamInventory_SetProperty")]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _SetProperty( IntPtr self, SteamInventoryUpdateHandle_t handle, InventoryItemId nItemID, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchPropertyName, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchPropertyValue );
		
		#endregion
		internal bool SetProperty( SteamInventoryUpdateHandle_t handle, InventoryItemId nItemID, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchPropertyName, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchPropertyValue )
		{
			var returnValue = _SetProperty( Self, handle, nItemID, pchPropertyName, pchPropertyValue );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamInventory_SetProperty0")]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _SetProperty0( IntPtr self, SteamInventoryUpdateHandle_t handle, InventoryItemId nItemID, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchPropertyName, [MarshalAs( UnmanagedType.U1 )] bool bValue );
		
		#endregion
		internal bool SetProperty0( SteamInventoryUpdateHandle_t handle, InventoryItemId nItemID, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchPropertyName, [MarshalAs( UnmanagedType.U1 )] bool bValue )
		{
			var returnValue = _SetProperty0( Self, handle, nItemID, pchPropertyName, bValue );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamInventory_SetProperty1")]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _SetProperty1( IntPtr self, SteamInventoryUpdateHandle_t handle, InventoryItemId nItemID, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchPropertyName, long nValue );
		
		#endregion
		internal bool SetProperty1( SteamInventoryUpdateHandle_t handle, InventoryItemId nItemID, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchPropertyName, long nValue )
		{
			var returnValue = _SetProperty1( Self, handle, nItemID, pchPropertyName, nValue );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamInventory_SetProperty2")]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _SetProperty2( IntPtr self, SteamInventoryUpdateHandle_t handle, InventoryItemId nItemID, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchPropertyName, float flValue );
		
		#endregion
		internal bool SetProperty2( SteamInventoryUpdateHandle_t handle, InventoryItemId nItemID, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchPropertyName, float flValue )
		{
			var returnValue = _SetProperty2( Self, handle, nItemID, pchPropertyName, flValue );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamInventory_SubmitUpdateProperties")]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _SubmitUpdateProperties( IntPtr self, SteamInventoryUpdateHandle_t handle, ref SteamInventoryResult_t pResultHandle );
		
		#endregion
		internal bool SubmitUpdateProperties( SteamInventoryUpdateHandle_t handle, ref SteamInventoryResult_t pResultHandle )
		{
			var returnValue = _SubmitUpdateProperties( Self, handle, ref pResultHandle );
			return returnValue;
		}
		
	}
}
