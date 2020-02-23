using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Steamworks.Data;


namespace Steamworks
{
	internal class ISteamInventory : SteamInterface
	{
		
		internal ISteamInventory( bool IsGameServer )
		{
			SetupInterface( IsGameServer );
		}
		
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_SteamInventory_v003", CallingConvention = Platform.CC)]
		internal static extern IntPtr SteamAPI_SteamInventory_v003();
		public override IntPtr GetUserInterfacePointer() => SteamAPI_SteamInventory_v003();
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_SteamGameServerInventory_v003", CallingConvention = Platform.CC)]
		internal static extern IntPtr SteamAPI_SteamGameServerInventory_v003();
		public override IntPtr GetServerInterfacePointer() => SteamAPI_SteamGameServerInventory_v003();
		
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamInventory_GetResultStatus", CallingConvention = Platform.CC)]
		private static extern Result _GetResultStatus( IntPtr self, SteamInventoryResult_t resultHandle );
		
		#endregion
		/// <summary>
		/// Find out the status of an asynchronous inventory result handle.
		/// </summary>
		internal Result GetResultStatus( SteamInventoryResult_t resultHandle )
		{
			var returnValue = _GetResultStatus( Self, resultHandle );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamInventory_GetResultItems", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _GetResultItems( IntPtr self, SteamInventoryResult_t resultHandle, [In,Out] SteamItemDetails_t[]  pOutItemsArray, ref uint punOutItemsArraySize );
		
		#endregion
		/// <summary>
		/// Copies the contents of a result set into a flat array. The specific contents of the result set depend on which query which was used.
		/// </summary>
		internal bool GetResultItems( SteamInventoryResult_t resultHandle, [In,Out] SteamItemDetails_t[]  pOutItemsArray, ref uint punOutItemsArraySize )
		{
			var returnValue = _GetResultItems( Self, resultHandle, pOutItemsArray, ref punOutItemsArraySize );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamInventory_GetResultItemProperty", CallingConvention = Platform.CC)]
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
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamInventory_GetResultTimestamp", CallingConvention = Platform.CC)]
		private static extern uint _GetResultTimestamp( IntPtr self, SteamInventoryResult_t resultHandle );
		
		#endregion
		/// <summary>
		/// Returns the server time at which the result was generated. Compare against the value of IClientUtils::GetServerRealTime() to determine age.
		/// </summary>
		internal uint GetResultTimestamp( SteamInventoryResult_t resultHandle )
		{
			var returnValue = _GetResultTimestamp( Self, resultHandle );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamInventory_CheckResultSteamID", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _CheckResultSteamID( IntPtr self, SteamInventoryResult_t resultHandle, SteamId steamIDExpected );
		
		#endregion
		/// <summary>
		/// Returns true if the result belongs to the target steam ID or false if the result does not. This is important when using DeserializeResult to verify that a remote player is not pretending to have a different users inventory.
		/// </summary>
		internal bool CheckResultSteamID( SteamInventoryResult_t resultHandle, SteamId steamIDExpected )
		{
			var returnValue = _CheckResultSteamID( Self, resultHandle, steamIDExpected );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamInventory_DestroyResult", CallingConvention = Platform.CC)]
		private static extern void _DestroyResult( IntPtr self, SteamInventoryResult_t resultHandle );
		
		#endregion
		/// <summary>
		/// Destroys a result handle and frees all associated memory.
		/// </summary>
		internal void DestroyResult( SteamInventoryResult_t resultHandle )
		{
			_DestroyResult( Self, resultHandle );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamInventory_GetAllItems", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _GetAllItems( IntPtr self, ref SteamInventoryResult_t pResultHandle );
		
		#endregion
		/// <summary>
		/// Captures the entire state of the current users Steam inventory.
		/// </summary>
		internal bool GetAllItems( ref SteamInventoryResult_t pResultHandle )
		{
			var returnValue = _GetAllItems( Self, ref pResultHandle );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamInventory_GetItemsByID", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _GetItemsByID( IntPtr self, ref SteamInventoryResult_t pResultHandle, ref InventoryItemId pInstanceIDs, uint unCountInstanceIDs );
		
		#endregion
		/// <summary>
		/// Captures the state of a subset of the current users Steam inventory identified by an array of item instance IDs.
		/// </summary>
		internal bool GetItemsByID( ref SteamInventoryResult_t pResultHandle, ref InventoryItemId pInstanceIDs, uint unCountInstanceIDs )
		{
			var returnValue = _GetItemsByID( Self, ref pResultHandle, ref pInstanceIDs, unCountInstanceIDs );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamInventory_SerializeResult", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _SerializeResult( IntPtr self, SteamInventoryResult_t resultHandle, IntPtr pOutBuffer, ref uint punOutBufferSize );
		
		#endregion
		internal bool SerializeResult( SteamInventoryResult_t resultHandle, IntPtr pOutBuffer, ref uint punOutBufferSize )
		{
			var returnValue = _SerializeResult( Self, resultHandle, pOutBuffer, ref punOutBufferSize );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamInventory_DeserializeResult", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _DeserializeResult( IntPtr self, ref SteamInventoryResult_t pOutResultHandle, IntPtr pBuffer, uint unBufferSize, [MarshalAs( UnmanagedType.U1 )] bool bRESERVED_MUST_BE_FALSE );
		
		#endregion
		internal bool DeserializeResult( ref SteamInventoryResult_t pOutResultHandle, IntPtr pBuffer, uint unBufferSize, [MarshalAs( UnmanagedType.U1 )] bool bRESERVED_MUST_BE_FALSE )
		{
			var returnValue = _DeserializeResult( Self, ref pOutResultHandle, pBuffer, unBufferSize, bRESERVED_MUST_BE_FALSE );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamInventory_GenerateItems", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _GenerateItems( IntPtr self, ref SteamInventoryResult_t pResultHandle, [In,Out] InventoryDefId[]  pArrayItemDefs, [In,Out] uint[]  punArrayQuantity, uint unArrayLength );
		
		#endregion
		internal bool GenerateItems( ref SteamInventoryResult_t pResultHandle, [In,Out] InventoryDefId[]  pArrayItemDefs, [In,Out] uint[]  punArrayQuantity, uint unArrayLength )
		{
			var returnValue = _GenerateItems( Self, ref pResultHandle, pArrayItemDefs, punArrayQuantity, unArrayLength );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamInventory_GrantPromoItems", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _GrantPromoItems( IntPtr self, ref SteamInventoryResult_t pResultHandle );
		
		#endregion
		/// <summary>
		/// GrantPromoItems() checks the list of promotional items for which the user may be eligible and grants the items (one time only).
		/// </summary>
		internal bool GrantPromoItems( ref SteamInventoryResult_t pResultHandle )
		{
			var returnValue = _GrantPromoItems( Self, ref pResultHandle );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamInventory_AddPromoItem", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _AddPromoItem( IntPtr self, ref SteamInventoryResult_t pResultHandle, InventoryDefId itemDef );
		
		#endregion
		internal bool AddPromoItem( ref SteamInventoryResult_t pResultHandle, InventoryDefId itemDef )
		{
			var returnValue = _AddPromoItem( Self, ref pResultHandle, itemDef );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamInventory_AddPromoItems", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _AddPromoItems( IntPtr self, ref SteamInventoryResult_t pResultHandle, [In,Out] InventoryDefId[]  pArrayItemDefs, uint unArrayLength );
		
		#endregion
		internal bool AddPromoItems( ref SteamInventoryResult_t pResultHandle, [In,Out] InventoryDefId[]  pArrayItemDefs, uint unArrayLength )
		{
			var returnValue = _AddPromoItems( Self, ref pResultHandle, pArrayItemDefs, unArrayLength );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamInventory_ConsumeItem", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _ConsumeItem( IntPtr self, ref SteamInventoryResult_t pResultHandle, InventoryItemId itemConsume, uint unQuantity );
		
		#endregion
		/// <summary>
		/// ConsumeItem() removes items from the inventory permanently.
		/// </summary>
		internal bool ConsumeItem( ref SteamInventoryResult_t pResultHandle, InventoryItemId itemConsume, uint unQuantity )
		{
			var returnValue = _ConsumeItem( Self, ref pResultHandle, itemConsume, unQuantity );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamInventory_ExchangeItems", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _ExchangeItems( IntPtr self, ref SteamInventoryResult_t pResultHandle, [In,Out] InventoryDefId[]  pArrayGenerate, [In,Out] uint[]  punArrayGenerateQuantity, uint unArrayGenerateLength, [In,Out] InventoryItemId[]  pArrayDestroy, [In,Out] uint[]  punArrayDestroyQuantity, uint unArrayDestroyLength );
		
		#endregion
		internal bool ExchangeItems( ref SteamInventoryResult_t pResultHandle, [In,Out] InventoryDefId[]  pArrayGenerate, [In,Out] uint[]  punArrayGenerateQuantity, uint unArrayGenerateLength, [In,Out] InventoryItemId[]  pArrayDestroy, [In,Out] uint[]  punArrayDestroyQuantity, uint unArrayDestroyLength )
		{
			var returnValue = _ExchangeItems( Self, ref pResultHandle, pArrayGenerate, punArrayGenerateQuantity, unArrayGenerateLength, pArrayDestroy, punArrayDestroyQuantity, unArrayDestroyLength );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamInventory_TransferItemQuantity", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _TransferItemQuantity( IntPtr self, ref SteamInventoryResult_t pResultHandle, InventoryItemId itemIdSource, uint unQuantity, InventoryItemId itemIdDest );
		
		#endregion
		internal bool TransferItemQuantity( ref SteamInventoryResult_t pResultHandle, InventoryItemId itemIdSource, uint unQuantity, InventoryItemId itemIdDest )
		{
			var returnValue = _TransferItemQuantity( Self, ref pResultHandle, itemIdSource, unQuantity, itemIdDest );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamInventory_SendItemDropHeartbeat", CallingConvention = Platform.CC)]
		private static extern void _SendItemDropHeartbeat( IntPtr self );
		
		#endregion
		/// <summary>
		/// Deprecated method. Playtime accounting is performed on the Steam servers.
		/// </summary>
		internal void SendItemDropHeartbeat()
		{
			_SendItemDropHeartbeat( Self );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamInventory_TriggerItemDrop", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _TriggerItemDrop( IntPtr self, ref SteamInventoryResult_t pResultHandle, InventoryDefId dropListDefinition );
		
		#endregion
		/// <summary>
		/// Playtime credit must be consumed and turned into item drops by your game.
		/// </summary>
		internal bool TriggerItemDrop( ref SteamInventoryResult_t pResultHandle, InventoryDefId dropListDefinition )
		{
			var returnValue = _TriggerItemDrop( Self, ref pResultHandle, dropListDefinition );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamInventory_TradeItems", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _TradeItems( IntPtr self, ref SteamInventoryResult_t pResultHandle, SteamId steamIDTradePartner, [In,Out] InventoryItemId[]  pArrayGive, [In,Out] uint[]  pArrayGiveQuantity, uint nArrayGiveLength, [In,Out] InventoryItemId[]  pArrayGet, [In,Out] uint[]  pArrayGetQuantity, uint nArrayGetLength );
		
		#endregion
		internal bool TradeItems( ref SteamInventoryResult_t pResultHandle, SteamId steamIDTradePartner, [In,Out] InventoryItemId[]  pArrayGive, [In,Out] uint[]  pArrayGiveQuantity, uint nArrayGiveLength, [In,Out] InventoryItemId[]  pArrayGet, [In,Out] uint[]  pArrayGetQuantity, uint nArrayGetLength )
		{
			var returnValue = _TradeItems( Self, ref pResultHandle, steamIDTradePartner, pArrayGive, pArrayGiveQuantity, nArrayGiveLength, pArrayGet, pArrayGetQuantity, nArrayGetLength );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamInventory_LoadItemDefinitions", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _LoadItemDefinitions( IntPtr self );
		
		#endregion
		/// <summary>
		/// LoadItemDefinitions triggers the automatic load and refresh of item definitions.
		/// </summary>
		internal bool LoadItemDefinitions()
		{
			var returnValue = _LoadItemDefinitions( Self );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamInventory_GetItemDefinitionIDs", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _GetItemDefinitionIDs( IntPtr self, [In,Out] InventoryDefId[]  pItemDefIDs, ref uint punItemDefIDsArraySize );
		
		#endregion
		internal bool GetItemDefinitionIDs( [In,Out] InventoryDefId[]  pItemDefIDs, ref uint punItemDefIDsArraySize )
		{
			var returnValue = _GetItemDefinitionIDs( Self, pItemDefIDs, ref punItemDefIDsArraySize );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamInventory_GetItemDefinitionProperty", CallingConvention = Platform.CC)]
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
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamInventory_RequestEligiblePromoItemDefinitionsIDs", CallingConvention = Platform.CC)]
		private static extern SteamAPICall_t _RequestEligiblePromoItemDefinitionsIDs( IntPtr self, SteamId steamID );
		
		#endregion
		internal CallResult<SteamInventoryEligiblePromoItemDefIDs_t> RequestEligiblePromoItemDefinitionsIDs( SteamId steamID )
		{
			var returnValue = _RequestEligiblePromoItemDefinitionsIDs( Self, steamID );
			return new CallResult<SteamInventoryEligiblePromoItemDefIDs_t>( returnValue, IsServer );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamInventory_GetEligiblePromoItemDefinitionIDs", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _GetEligiblePromoItemDefinitionIDs( IntPtr self, SteamId steamID, [In,Out] InventoryDefId[]  pItemDefIDs, ref uint punItemDefIDsArraySize );
		
		#endregion
		internal bool GetEligiblePromoItemDefinitionIDs( SteamId steamID, [In,Out] InventoryDefId[]  pItemDefIDs, ref uint punItemDefIDsArraySize )
		{
			var returnValue = _GetEligiblePromoItemDefinitionIDs( Self, steamID, pItemDefIDs, ref punItemDefIDsArraySize );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamInventory_StartPurchase", CallingConvention = Platform.CC)]
		private static extern SteamAPICall_t _StartPurchase( IntPtr self, [In,Out] InventoryDefId[]  pArrayItemDefs, [In,Out] uint[]  punArrayQuantity, uint unArrayLength );
		
		#endregion
		internal CallResult<SteamInventoryStartPurchaseResult_t> StartPurchase( [In,Out] InventoryDefId[]  pArrayItemDefs, [In,Out] uint[]  punArrayQuantity, uint unArrayLength )
		{
			var returnValue = _StartPurchase( Self, pArrayItemDefs, punArrayQuantity, unArrayLength );
			return new CallResult<SteamInventoryStartPurchaseResult_t>( returnValue, IsServer );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamInventory_RequestPrices", CallingConvention = Platform.CC)]
		private static extern SteamAPICall_t _RequestPrices( IntPtr self );
		
		#endregion
		internal CallResult<SteamInventoryRequestPricesResult_t> RequestPrices()
		{
			var returnValue = _RequestPrices( Self );
			return new CallResult<SteamInventoryRequestPricesResult_t>( returnValue, IsServer );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamInventory_GetNumItemsWithPrices", CallingConvention = Platform.CC)]
		private static extern uint _GetNumItemsWithPrices( IntPtr self );
		
		#endregion
		internal uint GetNumItemsWithPrices()
		{
			var returnValue = _GetNumItemsWithPrices( Self );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamInventory_GetItemsWithPrices", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _GetItemsWithPrices( IntPtr self, [In,Out] InventoryDefId[]  pArrayItemDefs, [In,Out] ulong[]  pCurrentPrices, [In,Out] ulong[]  pBasePrices, uint unArrayLength );
		
		#endregion
		internal bool GetItemsWithPrices( [In,Out] InventoryDefId[]  pArrayItemDefs, [In,Out] ulong[]  pCurrentPrices, [In,Out] ulong[]  pBasePrices, uint unArrayLength )
		{
			var returnValue = _GetItemsWithPrices( Self, pArrayItemDefs, pCurrentPrices, pBasePrices, unArrayLength );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamInventory_GetItemPrice", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _GetItemPrice( IntPtr self, InventoryDefId iDefinition, ref ulong pCurrentPrice, ref ulong pBasePrice );
		
		#endregion
		internal bool GetItemPrice( InventoryDefId iDefinition, ref ulong pCurrentPrice, ref ulong pBasePrice )
		{
			var returnValue = _GetItemPrice( Self, iDefinition, ref pCurrentPrice, ref pBasePrice );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamInventory_StartUpdateProperties", CallingConvention = Platform.CC)]
		private static extern SteamInventoryUpdateHandle_t _StartUpdateProperties( IntPtr self );
		
		#endregion
		internal SteamInventoryUpdateHandle_t StartUpdateProperties()
		{
			var returnValue = _StartUpdateProperties( Self );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamInventory_RemoveProperty", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _RemoveProperty( IntPtr self, SteamInventoryUpdateHandle_t handle, InventoryItemId nItemID, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchPropertyName );
		
		#endregion
		internal bool RemoveProperty( SteamInventoryUpdateHandle_t handle, InventoryItemId nItemID, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchPropertyName )
		{
			var returnValue = _RemoveProperty( Self, handle, nItemID, pchPropertyName );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamInventory_SetPropertyString", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _SetProperty( IntPtr self, SteamInventoryUpdateHandle_t handle, InventoryItemId nItemID, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchPropertyName, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchPropertyValue );
		
		#endregion
		internal bool SetProperty( SteamInventoryUpdateHandle_t handle, InventoryItemId nItemID, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchPropertyName, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchPropertyValue )
		{
			var returnValue = _SetProperty( Self, handle, nItemID, pchPropertyName, pchPropertyValue );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamInventory_SetPropertyBool", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _SetProperty( IntPtr self, SteamInventoryUpdateHandle_t handle, InventoryItemId nItemID, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchPropertyName, [MarshalAs( UnmanagedType.U1 )] bool bValue );
		
		#endregion
		internal bool SetProperty( SteamInventoryUpdateHandle_t handle, InventoryItemId nItemID, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchPropertyName, [MarshalAs( UnmanagedType.U1 )] bool bValue )
		{
			var returnValue = _SetProperty( Self, handle, nItemID, pchPropertyName, bValue );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamInventory_SetPropertyInt64", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _SetProperty( IntPtr self, SteamInventoryUpdateHandle_t handle, InventoryItemId nItemID, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchPropertyName, long nValue );
		
		#endregion
		internal bool SetProperty( SteamInventoryUpdateHandle_t handle, InventoryItemId nItemID, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchPropertyName, long nValue )
		{
			var returnValue = _SetProperty( Self, handle, nItemID, pchPropertyName, nValue );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamInventory_SetPropertyFloat", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _SetProperty( IntPtr self, SteamInventoryUpdateHandle_t handle, InventoryItemId nItemID, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchPropertyName, float flValue );
		
		#endregion
		internal bool SetProperty( SteamInventoryUpdateHandle_t handle, InventoryItemId nItemID, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchPropertyName, float flValue )
		{
			var returnValue = _SetProperty( Self, handle, nItemID, pchPropertyName, flValue );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamInventory_SubmitUpdateProperties", CallingConvention = Platform.CC)]
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
