using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Steamworks.Data;


namespace Steamworks
{
	internal class ISteamInventory : SteamInterface
	{
		public override string InterfaceName => "STEAMINVENTORY_INTERFACE_V003";
		
		public override void InitInternals()
		{
			_GetResultStatus = Marshal.GetDelegateForFunctionPointer<FGetResultStatus>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 0 ) ) );
			_GetResultItems = Marshal.GetDelegateForFunctionPointer<FGetResultItems>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 8 ) ) );
			_GetResultItemProperty = Marshal.GetDelegateForFunctionPointer<FGetResultItemProperty>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 16 ) ) );
			_GetResultTimestamp = Marshal.GetDelegateForFunctionPointer<FGetResultTimestamp>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 24 ) ) );
			_CheckResultSteamID = Marshal.GetDelegateForFunctionPointer<FCheckResultSteamID>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 32 ) ) );
			_DestroyResult = Marshal.GetDelegateForFunctionPointer<FDestroyResult>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 40 ) ) );
			_GetAllItems = Marshal.GetDelegateForFunctionPointer<FGetAllItems>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 48 ) ) );
			_GetItemsByID = Marshal.GetDelegateForFunctionPointer<FGetItemsByID>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 56 ) ) );
			_SerializeResult = Marshal.GetDelegateForFunctionPointer<FSerializeResult>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 64 ) ) );
			_DeserializeResult = Marshal.GetDelegateForFunctionPointer<FDeserializeResult>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 72 ) ) );
			_GenerateItems = Marshal.GetDelegateForFunctionPointer<FGenerateItems>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 80 ) ) );
			_GrantPromoItems = Marshal.GetDelegateForFunctionPointer<FGrantPromoItems>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 88 ) ) );
			_AddPromoItem = Marshal.GetDelegateForFunctionPointer<FAddPromoItem>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 96 ) ) );
			_AddPromoItems = Marshal.GetDelegateForFunctionPointer<FAddPromoItems>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 104 ) ) );
			_ConsumeItem = Marshal.GetDelegateForFunctionPointer<FConsumeItem>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 112 ) ) );
			_ExchangeItems = Marshal.GetDelegateForFunctionPointer<FExchangeItems>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 120 ) ) );
			_TransferItemQuantity = Marshal.GetDelegateForFunctionPointer<FTransferItemQuantity>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 128 ) ) );
			_SendItemDropHeartbeat = Marshal.GetDelegateForFunctionPointer<FSendItemDropHeartbeat>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 136 ) ) );
			_TriggerItemDrop = Marshal.GetDelegateForFunctionPointer<FTriggerItemDrop>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 144 ) ) );
			_TradeItems = Marshal.GetDelegateForFunctionPointer<FTradeItems>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 152 ) ) );
			_LoadItemDefinitions = Marshal.GetDelegateForFunctionPointer<FLoadItemDefinitions>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 160 ) ) );
			_GetItemDefinitionIDs = Marshal.GetDelegateForFunctionPointer<FGetItemDefinitionIDs>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 168 ) ) );
			_GetItemDefinitionProperty = Marshal.GetDelegateForFunctionPointer<FGetItemDefinitionProperty>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 176 ) ) );
			_RequestEligiblePromoItemDefinitionsIDs = Marshal.GetDelegateForFunctionPointer<FRequestEligiblePromoItemDefinitionsIDs>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 184 ) ) );
			_GetEligiblePromoItemDefinitionIDs = Marshal.GetDelegateForFunctionPointer<FGetEligiblePromoItemDefinitionIDs>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 192 ) ) );
			_StartPurchase = Marshal.GetDelegateForFunctionPointer<FStartPurchase>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 200 ) ) );
			_RequestPrices = Marshal.GetDelegateForFunctionPointer<FRequestPrices>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 208 ) ) );
			_GetNumItemsWithPrices = Marshal.GetDelegateForFunctionPointer<FGetNumItemsWithPrices>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 216 ) ) );
			_GetItemsWithPrices = Marshal.GetDelegateForFunctionPointer<FGetItemsWithPrices>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 224 ) ) );
			_GetItemPrice = Marshal.GetDelegateForFunctionPointer<FGetItemPrice>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 232 ) ) );
			_StartUpdateProperties = Marshal.GetDelegateForFunctionPointer<FStartUpdateProperties>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 240 ) ) );
			_RemoveProperty = Marshal.GetDelegateForFunctionPointer<FRemoveProperty>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 248 ) ) );
			_SetProperty1 = Marshal.GetDelegateForFunctionPointer<FSetProperty1>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 256 ) ) );
			_SetProperty2 = Marshal.GetDelegateForFunctionPointer<FSetProperty2>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 264 ) ) );
			_SetProperty3 = Marshal.GetDelegateForFunctionPointer<FSetProperty3>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 272 ) ) );
			_SetProperty4 = Marshal.GetDelegateForFunctionPointer<FSetProperty4>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 280 ) ) );
			_SubmitUpdateProperties = Marshal.GetDelegateForFunctionPointer<FSubmitUpdateProperties>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 288 ) ) );
		}
		internal override void Shutdown()
		{
			base.Shutdown();
			
			_GetResultStatus = null;
			_GetResultItems = null;
			_GetResultItemProperty = null;
			_GetResultTimestamp = null;
			_CheckResultSteamID = null;
			_DestroyResult = null;
			_GetAllItems = null;
			_GetItemsByID = null;
			_SerializeResult = null;
			_DeserializeResult = null;
			_GenerateItems = null;
			_GrantPromoItems = null;
			_AddPromoItem = null;
			_AddPromoItems = null;
			_ConsumeItem = null;
			_ExchangeItems = null;
			_TransferItemQuantity = null;
			_SendItemDropHeartbeat = null;
			_TriggerItemDrop = null;
			_TradeItems = null;
			_LoadItemDefinitions = null;
			_GetItemDefinitionIDs = null;
			_GetItemDefinitionProperty = null;
			_RequestEligiblePromoItemDefinitionsIDs = null;
			_GetEligiblePromoItemDefinitionIDs = null;
			_StartPurchase = null;
			_RequestPrices = null;
			_GetNumItemsWithPrices = null;
			_GetItemsWithPrices = null;
			_GetItemPrice = null;
			_StartUpdateProperties = null;
			_RemoveProperty = null;
			_SetProperty1 = null;
			_SetProperty2 = null;
			_SetProperty3 = null;
			_SetProperty4 = null;
			_SubmitUpdateProperties = null;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate Result FGetResultStatus( IntPtr self, SteamInventoryResult_t resultHandle );
		private FGetResultStatus _GetResultStatus;
		
		#endregion
		internal Result GetResultStatus( SteamInventoryResult_t resultHandle )
		{
			var returnValue = _GetResultStatus( Self, resultHandle );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FGetResultItems( IntPtr self, SteamInventoryResult_t resultHandle, [In,Out] SteamItemDetails_t[]  pOutItemsArray, ref uint punOutItemsArraySize );
		private FGetResultItems _GetResultItems;
		
		#endregion
		internal bool GetResultItems( SteamInventoryResult_t resultHandle, [In,Out] SteamItemDetails_t[]  pOutItemsArray, ref uint punOutItemsArraySize )
		{
			var returnValue = _GetResultItems( Self, resultHandle, pOutItemsArray, ref punOutItemsArraySize );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FGetResultItemProperty( IntPtr self, SteamInventoryResult_t resultHandle, uint unItemIndex, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchPropertyName, IntPtr pchValueBuffer, ref uint punValueBufferSizeOut );
		private FGetResultItemProperty _GetResultItemProperty;
		
		#endregion
		internal bool GetResultItemProperty( SteamInventoryResult_t resultHandle, uint unItemIndex, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchPropertyName, out string pchValueBuffer, ref uint punValueBufferSizeOut )
		{
			IntPtr mempchValueBuffer = Helpers.TakeMemory();
			var returnValue = _GetResultItemProperty( Self, resultHandle, unItemIndex, pchPropertyName, mempchValueBuffer, ref punValueBufferSizeOut );
			pchValueBuffer = Helpers.MemoryToString( mempchValueBuffer );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate uint FGetResultTimestamp( IntPtr self, SteamInventoryResult_t resultHandle );
		private FGetResultTimestamp _GetResultTimestamp;
		
		#endregion
		internal uint GetResultTimestamp( SteamInventoryResult_t resultHandle )
		{
			var returnValue = _GetResultTimestamp( Self, resultHandle );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FCheckResultSteamID( IntPtr self, SteamInventoryResult_t resultHandle, SteamId steamIDExpected );
		private FCheckResultSteamID _CheckResultSteamID;
		
		#endregion
		internal bool CheckResultSteamID( SteamInventoryResult_t resultHandle, SteamId steamIDExpected )
		{
			var returnValue = _CheckResultSteamID( Self, resultHandle, steamIDExpected );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate void FDestroyResult( IntPtr self, SteamInventoryResult_t resultHandle );
		private FDestroyResult _DestroyResult;
		
		#endregion
		internal void DestroyResult( SteamInventoryResult_t resultHandle )
		{
			_DestroyResult( Self, resultHandle );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FGetAllItems( IntPtr self, ref SteamInventoryResult_t pResultHandle );
		private FGetAllItems _GetAllItems;
		
		#endregion
		internal bool GetAllItems( ref SteamInventoryResult_t pResultHandle )
		{
			var returnValue = _GetAllItems( Self, ref pResultHandle );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FGetItemsByID( IntPtr self, ref SteamInventoryResult_t pResultHandle, ref InventoryItemId pInstanceIDs, uint unCountInstanceIDs );
		private FGetItemsByID _GetItemsByID;
		
		#endregion
		internal bool GetItemsByID( ref SteamInventoryResult_t pResultHandle, ref InventoryItemId pInstanceIDs, uint unCountInstanceIDs )
		{
			var returnValue = _GetItemsByID( Self, ref pResultHandle, ref pInstanceIDs, unCountInstanceIDs );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FSerializeResult( IntPtr self, SteamInventoryResult_t resultHandle, IntPtr pOutBuffer, ref uint punOutBufferSize );
		private FSerializeResult _SerializeResult;
		
		#endregion
		internal bool SerializeResult( SteamInventoryResult_t resultHandle, IntPtr pOutBuffer, ref uint punOutBufferSize )
		{
			var returnValue = _SerializeResult( Self, resultHandle, pOutBuffer, ref punOutBufferSize );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FDeserializeResult( IntPtr self, ref SteamInventoryResult_t pOutResultHandle, IntPtr pBuffer, uint unBufferSize, [MarshalAs( UnmanagedType.U1 )] bool bRESERVED_MUST_BE_FALSE );
		private FDeserializeResult _DeserializeResult;
		
		#endregion
		internal bool DeserializeResult( ref SteamInventoryResult_t pOutResultHandle, IntPtr pBuffer, uint unBufferSize, [MarshalAs( UnmanagedType.U1 )] bool bRESERVED_MUST_BE_FALSE )
		{
			var returnValue = _DeserializeResult( Self, ref pOutResultHandle, pBuffer, unBufferSize, bRESERVED_MUST_BE_FALSE );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FGenerateItems( IntPtr self, ref SteamInventoryResult_t pResultHandle, [In,Out] InventoryDefId[]  pArrayItemDefs, [In,Out] uint[]  punArrayQuantity, uint unArrayLength );
		private FGenerateItems _GenerateItems;
		
		#endregion
		internal bool GenerateItems( ref SteamInventoryResult_t pResultHandle, [In,Out] InventoryDefId[]  pArrayItemDefs, [In,Out] uint[]  punArrayQuantity, uint unArrayLength )
		{
			var returnValue = _GenerateItems( Self, ref pResultHandle, pArrayItemDefs, punArrayQuantity, unArrayLength );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FGrantPromoItems( IntPtr self, ref SteamInventoryResult_t pResultHandle );
		private FGrantPromoItems _GrantPromoItems;
		
		#endregion
		internal bool GrantPromoItems( ref SteamInventoryResult_t pResultHandle )
		{
			var returnValue = _GrantPromoItems( Self, ref pResultHandle );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FAddPromoItem( IntPtr self, ref SteamInventoryResult_t pResultHandle, InventoryDefId itemDef );
		private FAddPromoItem _AddPromoItem;
		
		#endregion
		internal bool AddPromoItem( ref SteamInventoryResult_t pResultHandle, InventoryDefId itemDef )
		{
			var returnValue = _AddPromoItem( Self, ref pResultHandle, itemDef );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FAddPromoItems( IntPtr self, ref SteamInventoryResult_t pResultHandle, [In,Out] InventoryDefId[]  pArrayItemDefs, uint unArrayLength );
		private FAddPromoItems _AddPromoItems;
		
		#endregion
		internal bool AddPromoItems( ref SteamInventoryResult_t pResultHandle, [In,Out] InventoryDefId[]  pArrayItemDefs, uint unArrayLength )
		{
			var returnValue = _AddPromoItems( Self, ref pResultHandle, pArrayItemDefs, unArrayLength );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FConsumeItem( IntPtr self, ref SteamInventoryResult_t pResultHandle, InventoryItemId itemConsume, uint unQuantity );
		private FConsumeItem _ConsumeItem;
		
		#endregion
		internal bool ConsumeItem( ref SteamInventoryResult_t pResultHandle, InventoryItemId itemConsume, uint unQuantity )
		{
			var returnValue = _ConsumeItem( Self, ref pResultHandle, itemConsume, unQuantity );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FExchangeItems( IntPtr self, ref SteamInventoryResult_t pResultHandle, [In,Out] InventoryDefId[]  pArrayGenerate, [In,Out] uint[]  punArrayGenerateQuantity, uint unArrayGenerateLength, [In,Out] InventoryItemId[]  pArrayDestroy, [In,Out] uint[]  punArrayDestroyQuantity, uint unArrayDestroyLength );
		private FExchangeItems _ExchangeItems;
		
		#endregion
		internal bool ExchangeItems( ref SteamInventoryResult_t pResultHandle, [In,Out] InventoryDefId[]  pArrayGenerate, [In,Out] uint[]  punArrayGenerateQuantity, uint unArrayGenerateLength, [In,Out] InventoryItemId[]  pArrayDestroy, [In,Out] uint[]  punArrayDestroyQuantity, uint unArrayDestroyLength )
		{
			var returnValue = _ExchangeItems( Self, ref pResultHandle, pArrayGenerate, punArrayGenerateQuantity, unArrayGenerateLength, pArrayDestroy, punArrayDestroyQuantity, unArrayDestroyLength );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FTransferItemQuantity( IntPtr self, ref SteamInventoryResult_t pResultHandle, InventoryItemId itemIdSource, uint unQuantity, InventoryItemId itemIdDest );
		private FTransferItemQuantity _TransferItemQuantity;
		
		#endregion
		internal bool TransferItemQuantity( ref SteamInventoryResult_t pResultHandle, InventoryItemId itemIdSource, uint unQuantity, InventoryItemId itemIdDest )
		{
			var returnValue = _TransferItemQuantity( Self, ref pResultHandle, itemIdSource, unQuantity, itemIdDest );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate void FSendItemDropHeartbeat( IntPtr self );
		private FSendItemDropHeartbeat _SendItemDropHeartbeat;
		
		#endregion
		internal void SendItemDropHeartbeat()
		{
			_SendItemDropHeartbeat( Self );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FTriggerItemDrop( IntPtr self, ref SteamInventoryResult_t pResultHandle, InventoryDefId dropListDefinition );
		private FTriggerItemDrop _TriggerItemDrop;
		
		#endregion
		internal bool TriggerItemDrop( ref SteamInventoryResult_t pResultHandle, InventoryDefId dropListDefinition )
		{
			var returnValue = _TriggerItemDrop( Self, ref pResultHandle, dropListDefinition );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FTradeItems( IntPtr self, ref SteamInventoryResult_t pResultHandle, SteamId steamIDTradePartner, [In,Out] InventoryItemId[]  pArrayGive, [In,Out] uint[]  pArrayGiveQuantity, uint nArrayGiveLength, [In,Out] InventoryItemId[]  pArrayGet, [In,Out] uint[]  pArrayGetQuantity, uint nArrayGetLength );
		private FTradeItems _TradeItems;
		
		#endregion
		internal bool TradeItems( ref SteamInventoryResult_t pResultHandle, SteamId steamIDTradePartner, [In,Out] InventoryItemId[]  pArrayGive, [In,Out] uint[]  pArrayGiveQuantity, uint nArrayGiveLength, [In,Out] InventoryItemId[]  pArrayGet, [In,Out] uint[]  pArrayGetQuantity, uint nArrayGetLength )
		{
			var returnValue = _TradeItems( Self, ref pResultHandle, steamIDTradePartner, pArrayGive, pArrayGiveQuantity, nArrayGiveLength, pArrayGet, pArrayGetQuantity, nArrayGetLength );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FLoadItemDefinitions( IntPtr self );
		private FLoadItemDefinitions _LoadItemDefinitions;
		
		#endregion
		internal bool LoadItemDefinitions()
		{
			var returnValue = _LoadItemDefinitions( Self );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FGetItemDefinitionIDs( IntPtr self, [In,Out] InventoryDefId[]  pItemDefIDs, ref uint punItemDefIDsArraySize );
		private FGetItemDefinitionIDs _GetItemDefinitionIDs;
		
		#endregion
		internal bool GetItemDefinitionIDs( [In,Out] InventoryDefId[]  pItemDefIDs, ref uint punItemDefIDsArraySize )
		{
			var returnValue = _GetItemDefinitionIDs( Self, pItemDefIDs, ref punItemDefIDsArraySize );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FGetItemDefinitionProperty( IntPtr self, InventoryDefId iDefinition, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchPropertyName, IntPtr pchValueBuffer, ref uint punValueBufferSizeOut );
		private FGetItemDefinitionProperty _GetItemDefinitionProperty;
		
		#endregion
		internal bool GetItemDefinitionProperty( InventoryDefId iDefinition, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchPropertyName, out string pchValueBuffer, ref uint punValueBufferSizeOut )
		{
			IntPtr mempchValueBuffer = Helpers.TakeMemory();
			var returnValue = _GetItemDefinitionProperty( Self, iDefinition, pchPropertyName, mempchValueBuffer, ref punValueBufferSizeOut );
			pchValueBuffer = Helpers.MemoryToString( mempchValueBuffer );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate SteamAPICall_t FRequestEligiblePromoItemDefinitionsIDs( IntPtr self, SteamId steamID );
		private FRequestEligiblePromoItemDefinitionsIDs _RequestEligiblePromoItemDefinitionsIDs;
		
		#endregion
		internal async Task<SteamInventoryEligiblePromoItemDefIDs_t?> RequestEligiblePromoItemDefinitionsIDs( SteamId steamID )
		{
			var returnValue = _RequestEligiblePromoItemDefinitionsIDs( Self, steamID );
			return await SteamInventoryEligiblePromoItemDefIDs_t.GetResultAsync( returnValue );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FGetEligiblePromoItemDefinitionIDs( IntPtr self, SteamId steamID, [In,Out] InventoryDefId[]  pItemDefIDs, ref uint punItemDefIDsArraySize );
		private FGetEligiblePromoItemDefinitionIDs _GetEligiblePromoItemDefinitionIDs;
		
		#endregion
		internal bool GetEligiblePromoItemDefinitionIDs( SteamId steamID, [In,Out] InventoryDefId[]  pItemDefIDs, ref uint punItemDefIDsArraySize )
		{
			var returnValue = _GetEligiblePromoItemDefinitionIDs( Self, steamID, pItemDefIDs, ref punItemDefIDsArraySize );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate SteamAPICall_t FStartPurchase( IntPtr self, [In,Out] InventoryDefId[]  pArrayItemDefs, [In,Out] uint[]  punArrayQuantity, uint unArrayLength );
		private FStartPurchase _StartPurchase;
		
		#endregion
		internal async Task<SteamInventoryStartPurchaseResult_t?> StartPurchase( [In,Out] InventoryDefId[]  pArrayItemDefs, [In,Out] uint[]  punArrayQuantity, uint unArrayLength )
		{
			var returnValue = _StartPurchase( Self, pArrayItemDefs, punArrayQuantity, unArrayLength );
			return await SteamInventoryStartPurchaseResult_t.GetResultAsync( returnValue );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate SteamAPICall_t FRequestPrices( IntPtr self );
		private FRequestPrices _RequestPrices;
		
		#endregion
		internal async Task<SteamInventoryRequestPricesResult_t?> RequestPrices()
		{
			var returnValue = _RequestPrices( Self );
			return await SteamInventoryRequestPricesResult_t.GetResultAsync( returnValue );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate uint FGetNumItemsWithPrices( IntPtr self );
		private FGetNumItemsWithPrices _GetNumItemsWithPrices;
		
		#endregion
		internal uint GetNumItemsWithPrices()
		{
			var returnValue = _GetNumItemsWithPrices( Self );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FGetItemsWithPrices( IntPtr self, [In,Out] InventoryDefId[]  pArrayItemDefs, [In,Out] ulong[]  pCurrentPrices, [In,Out] ulong[]  pBasePrices, uint unArrayLength );
		private FGetItemsWithPrices _GetItemsWithPrices;
		
		#endregion
		internal bool GetItemsWithPrices( [In,Out] InventoryDefId[]  pArrayItemDefs, [In,Out] ulong[]  pCurrentPrices, [In,Out] ulong[]  pBasePrices, uint unArrayLength )
		{
			var returnValue = _GetItemsWithPrices( Self, pArrayItemDefs, pCurrentPrices, pBasePrices, unArrayLength );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FGetItemPrice( IntPtr self, InventoryDefId iDefinition, ref ulong pCurrentPrice, ref ulong pBasePrice );
		private FGetItemPrice _GetItemPrice;
		
		#endregion
		internal bool GetItemPrice( InventoryDefId iDefinition, ref ulong pCurrentPrice, ref ulong pBasePrice )
		{
			var returnValue = _GetItemPrice( Self, iDefinition, ref pCurrentPrice, ref pBasePrice );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate SteamInventoryUpdateHandle_t FStartUpdateProperties( IntPtr self );
		private FStartUpdateProperties _StartUpdateProperties;
		
		#endregion
		internal SteamInventoryUpdateHandle_t StartUpdateProperties()
		{
			var returnValue = _StartUpdateProperties( Self );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FRemoveProperty( IntPtr self, SteamInventoryUpdateHandle_t handle, InventoryItemId nItemID, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchPropertyName );
		private FRemoveProperty _RemoveProperty;
		
		#endregion
		internal bool RemoveProperty( SteamInventoryUpdateHandle_t handle, InventoryItemId nItemID, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchPropertyName )
		{
			var returnValue = _RemoveProperty( Self, handle, nItemID, pchPropertyName );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FSetProperty1( IntPtr self, SteamInventoryUpdateHandle_t handle, InventoryItemId nItemID, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchPropertyName, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchPropertyValue );
		private FSetProperty1 _SetProperty1;
		
		#endregion
		internal bool SetProperty1( SteamInventoryUpdateHandle_t handle, InventoryItemId nItemID, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchPropertyName, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchPropertyValue )
		{
			var returnValue = _SetProperty1( Self, handle, nItemID, pchPropertyName, pchPropertyValue );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FSetProperty2( IntPtr self, SteamInventoryUpdateHandle_t handle, InventoryItemId nItemID, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchPropertyName, [MarshalAs( UnmanagedType.U1 )] bool bValue );
		private FSetProperty2 _SetProperty2;
		
		#endregion
		internal bool SetProperty2( SteamInventoryUpdateHandle_t handle, InventoryItemId nItemID, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchPropertyName, [MarshalAs( UnmanagedType.U1 )] bool bValue )
		{
			var returnValue = _SetProperty2( Self, handle, nItemID, pchPropertyName, bValue );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FSetProperty3( IntPtr self, SteamInventoryUpdateHandle_t handle, InventoryItemId nItemID, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchPropertyName, long nValue );
		private FSetProperty3 _SetProperty3;
		
		#endregion
		internal bool SetProperty3( SteamInventoryUpdateHandle_t handle, InventoryItemId nItemID, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchPropertyName, long nValue )
		{
			var returnValue = _SetProperty3( Self, handle, nItemID, pchPropertyName, nValue );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FSetProperty4( IntPtr self, SteamInventoryUpdateHandle_t handle, InventoryItemId nItemID, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchPropertyName, float flValue );
		private FSetProperty4 _SetProperty4;
		
		#endregion
		internal bool SetProperty4( SteamInventoryUpdateHandle_t handle, InventoryItemId nItemID, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchPropertyName, float flValue )
		{
			var returnValue = _SetProperty4( Self, handle, nItemID, pchPropertyName, flValue );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FSubmitUpdateProperties( IntPtr self, SteamInventoryUpdateHandle_t handle, ref SteamInventoryResult_t pResultHandle );
		private FSubmitUpdateProperties _SubmitUpdateProperties;
		
		#endregion
		internal bool SubmitUpdateProperties( SteamInventoryUpdateHandle_t handle, ref SteamInventoryResult_t pResultHandle )
		{
			var returnValue = _SubmitUpdateProperties( Self, handle, ref pResultHandle );
			return returnValue;
		}
		
	}
}
