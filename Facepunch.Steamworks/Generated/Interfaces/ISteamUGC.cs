using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Steamworks.Data;


namespace Steamworks
{
	internal class ISteamUGC : SteamInterface
	{
		public override string InterfaceName => "STEAMUGC_INTERFACE_VERSION012";
		
		public override void InitInternals()
		{
			_CreateQueryUserUGCRequest = Marshal.GetDelegateForFunctionPointer<FCreateQueryUserUGCRequest>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 0 ) ) );
			_CreateQueryUGCDetailsRequest = Marshal.GetDelegateForFunctionPointer<FCreateQueryUGCDetailsRequest>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 24 ) ) );
			_SendQueryUGCRequest = Marshal.GetDelegateForFunctionPointer<FSendQueryUGCRequest>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 32 ) ) );
			_GetQueryUGCResult = Marshal.GetDelegateForFunctionPointer<FGetQueryUGCResult>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 40 ) ) );
			_GetQueryUGCPreviewURL = Marshal.GetDelegateForFunctionPointer<FGetQueryUGCPreviewURL>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 48 ) ) );
			_GetQueryUGCMetadata = Marshal.GetDelegateForFunctionPointer<FGetQueryUGCMetadata>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 56 ) ) );
			_GetQueryUGCChildren = Marshal.GetDelegateForFunctionPointer<FGetQueryUGCChildren>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 64 ) ) );
			_GetQueryUGCStatistic = Marshal.GetDelegateForFunctionPointer<FGetQueryUGCStatistic>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 72 ) ) );
			_GetQueryUGCNumAdditionalPreviews = Marshal.GetDelegateForFunctionPointer<FGetQueryUGCNumAdditionalPreviews>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 80 ) ) );
			_GetQueryUGCAdditionalPreview = Marshal.GetDelegateForFunctionPointer<FGetQueryUGCAdditionalPreview>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 88 ) ) );
			_GetQueryUGCNumKeyValueTags = Marshal.GetDelegateForFunctionPointer<FGetQueryUGCNumKeyValueTags>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 96 ) ) );
			_GetQueryUGCKeyValueTag = Marshal.GetDelegateForFunctionPointer<FGetQueryUGCKeyValueTag>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 104 ) ) );
			_ReleaseQueryUGCRequest = Marshal.GetDelegateForFunctionPointer<FReleaseQueryUGCRequest>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 112 ) ) );
			_AddRequiredTag = Marshal.GetDelegateForFunctionPointer<FAddRequiredTag>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 120 ) ) );
			_AddExcludedTag = Marshal.GetDelegateForFunctionPointer<FAddExcludedTag>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 128 ) ) );
			_SetReturnOnlyIDs = Marshal.GetDelegateForFunctionPointer<FSetReturnOnlyIDs>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 136 ) ) );
			_SetReturnKeyValueTags = Marshal.GetDelegateForFunctionPointer<FSetReturnKeyValueTags>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 144 ) ) );
			_SetReturnLongDescription = Marshal.GetDelegateForFunctionPointer<FSetReturnLongDescription>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 152 ) ) );
			_SetReturnMetadata = Marshal.GetDelegateForFunctionPointer<FSetReturnMetadata>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 160 ) ) );
			_SetReturnChildren = Marshal.GetDelegateForFunctionPointer<FSetReturnChildren>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 168 ) ) );
			_SetReturnAdditionalPreviews = Marshal.GetDelegateForFunctionPointer<FSetReturnAdditionalPreviews>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 176 ) ) );
			_SetReturnTotalOnly = Marshal.GetDelegateForFunctionPointer<FSetReturnTotalOnly>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 184 ) ) );
			_SetReturnPlaytimeStats = Marshal.GetDelegateForFunctionPointer<FSetReturnPlaytimeStats>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 192 ) ) );
			_SetLanguage = Marshal.GetDelegateForFunctionPointer<FSetLanguage>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 200 ) ) );
			_SetAllowCachedResponse = Marshal.GetDelegateForFunctionPointer<FSetAllowCachedResponse>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 208 ) ) );
			_SetCloudFileNameFilter = Marshal.GetDelegateForFunctionPointer<FSetCloudFileNameFilter>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 216 ) ) );
			_SetMatchAnyTag = Marshal.GetDelegateForFunctionPointer<FSetMatchAnyTag>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 224 ) ) );
			_SetSearchText = Marshal.GetDelegateForFunctionPointer<FSetSearchText>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 232 ) ) );
			_SetRankedByTrendDays = Marshal.GetDelegateForFunctionPointer<FSetRankedByTrendDays>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 240 ) ) );
			_AddRequiredKeyValueTag = Marshal.GetDelegateForFunctionPointer<FAddRequiredKeyValueTag>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 248 ) ) );
			_RequestUGCDetails = Marshal.GetDelegateForFunctionPointer<FRequestUGCDetails>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 256 ) ) );
			_CreateItem = Marshal.GetDelegateForFunctionPointer<FCreateItem>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 264 ) ) );
			_StartItemUpdate = Marshal.GetDelegateForFunctionPointer<FStartItemUpdate>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 272 ) ) );
			_SetItemTitle = Marshal.GetDelegateForFunctionPointer<FSetItemTitle>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 280 ) ) );
			_SetItemDescription = Marshal.GetDelegateForFunctionPointer<FSetItemDescription>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 288 ) ) );
			_SetItemUpdateLanguage = Marshal.GetDelegateForFunctionPointer<FSetItemUpdateLanguage>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 296 ) ) );
			_SetItemMetadata = Marshal.GetDelegateForFunctionPointer<FSetItemMetadata>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 304 ) ) );
			_SetItemVisibility = Marshal.GetDelegateForFunctionPointer<FSetItemVisibility>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 312 ) ) );
			_SetItemTags = Marshal.GetDelegateForFunctionPointer<FSetItemTags>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 320 ) ) );
			_SetItemContent = Marshal.GetDelegateForFunctionPointer<FSetItemContent>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 328 ) ) );
			_SetItemPreview = Marshal.GetDelegateForFunctionPointer<FSetItemPreview>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 336 ) ) );
			_SetAllowLegacyUpload = Marshal.GetDelegateForFunctionPointer<FSetAllowLegacyUpload>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 344 ) ) );
			_RemoveItemKeyValueTags = Marshal.GetDelegateForFunctionPointer<FRemoveItemKeyValueTags>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 352 ) ) );
			_AddItemKeyValueTag = Marshal.GetDelegateForFunctionPointer<FAddItemKeyValueTag>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 360 ) ) );
			_AddItemPreviewFile = Marshal.GetDelegateForFunctionPointer<FAddItemPreviewFile>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 368 ) ) );
			_AddItemPreviewVideo = Marshal.GetDelegateForFunctionPointer<FAddItemPreviewVideo>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 376 ) ) );
			_UpdateItemPreviewFile = Marshal.GetDelegateForFunctionPointer<FUpdateItemPreviewFile>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 384 ) ) );
			_UpdateItemPreviewVideo = Marshal.GetDelegateForFunctionPointer<FUpdateItemPreviewVideo>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 392 ) ) );
			_RemoveItemPreview = Marshal.GetDelegateForFunctionPointer<FRemoveItemPreview>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 400 ) ) );
			_SubmitItemUpdate = Marshal.GetDelegateForFunctionPointer<FSubmitItemUpdate>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 408 ) ) );
			_GetItemUpdateProgress = Marshal.GetDelegateForFunctionPointer<FGetItemUpdateProgress>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 416 ) ) );
			_SetUserItemVote = Marshal.GetDelegateForFunctionPointer<FSetUserItemVote>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 424 ) ) );
			_GetUserItemVote = Marshal.GetDelegateForFunctionPointer<FGetUserItemVote>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 432 ) ) );
			_AddItemToFavorites = Marshal.GetDelegateForFunctionPointer<FAddItemToFavorites>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 440 ) ) );
			_RemoveItemFromFavorites = Marshal.GetDelegateForFunctionPointer<FRemoveItemFromFavorites>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 448 ) ) );
			_SubscribeItem = Marshal.GetDelegateForFunctionPointer<FSubscribeItem>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 456 ) ) );
			_UnsubscribeItem = Marshal.GetDelegateForFunctionPointer<FUnsubscribeItem>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 464 ) ) );
			_GetNumSubscribedItems = Marshal.GetDelegateForFunctionPointer<FGetNumSubscribedItems>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 472 ) ) );
			_GetSubscribedItems = Marshal.GetDelegateForFunctionPointer<FGetSubscribedItems>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 480 ) ) );
			_GetItemState = Marshal.GetDelegateForFunctionPointer<FGetItemState>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 488 ) ) );
			_GetItemInstallInfo = Marshal.GetDelegateForFunctionPointer<FGetItemInstallInfo>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 496 ) ) );
			_GetItemDownloadInfo = Marshal.GetDelegateForFunctionPointer<FGetItemDownloadInfo>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 504 ) ) );
			_DownloadItem = Marshal.GetDelegateForFunctionPointer<FDownloadItem>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 512 ) ) );
			_BInitWorkshopForGameServer = Marshal.GetDelegateForFunctionPointer<FBInitWorkshopForGameServer>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 520 ) ) );
			_SuspendDownloads = Marshal.GetDelegateForFunctionPointer<FSuspendDownloads>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 528 ) ) );
			_StartPlaytimeTracking = Marshal.GetDelegateForFunctionPointer<FStartPlaytimeTracking>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 536 ) ) );
			_StopPlaytimeTracking = Marshal.GetDelegateForFunctionPointer<FStopPlaytimeTracking>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 544 ) ) );
			_StopPlaytimeTrackingForAllItems = Marshal.GetDelegateForFunctionPointer<FStopPlaytimeTrackingForAllItems>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 552 ) ) );
			_AddDependency = Marshal.GetDelegateForFunctionPointer<FAddDependency>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 560 ) ) );
			_RemoveDependency = Marshal.GetDelegateForFunctionPointer<FRemoveDependency>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 568 ) ) );
			_AddAppDependency = Marshal.GetDelegateForFunctionPointer<FAddAppDependency>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 576 ) ) );
			_RemoveAppDependency = Marshal.GetDelegateForFunctionPointer<FRemoveAppDependency>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 584 ) ) );
			_GetAppDependencies = Marshal.GetDelegateForFunctionPointer<FGetAppDependencies>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 592 ) ) );
			_DeleteItem = Marshal.GetDelegateForFunctionPointer<FDeleteItem>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 600 ) ) );
			
			#if PLATFORM_WIN
			_CreateQueryAllUGCRequest1 = Marshal.GetDelegateForFunctionPointer<FCreateQueryAllUGCRequest1>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 16 ) ) );
			_CreateQueryAllUGCRequest2 = Marshal.GetDelegateForFunctionPointer<FCreateQueryAllUGCRequest2>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 8 ) ) );
			#else
			_CreateQueryAllUGCRequest1 = Marshal.GetDelegateForFunctionPointer<FCreateQueryAllUGCRequest1>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 8 ) ) );
			_CreateQueryAllUGCRequest2 = Marshal.GetDelegateForFunctionPointer<FCreateQueryAllUGCRequest2>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 16 ) ) );
			#endif
		}
		internal override void Shutdown()
		{
			base.Shutdown();
			
			_CreateQueryUserUGCRequest = null;
			_CreateQueryAllUGCRequest1 = null;
			_CreateQueryAllUGCRequest2 = null;
			_CreateQueryUGCDetailsRequest = null;
			_SendQueryUGCRequest = null;
			_GetQueryUGCResult = null;
			_GetQueryUGCPreviewURL = null;
			_GetQueryUGCMetadata = null;
			_GetQueryUGCChildren = null;
			_GetQueryUGCStatistic = null;
			_GetQueryUGCNumAdditionalPreviews = null;
			_GetQueryUGCAdditionalPreview = null;
			_GetQueryUGCNumKeyValueTags = null;
			_GetQueryUGCKeyValueTag = null;
			_ReleaseQueryUGCRequest = null;
			_AddRequiredTag = null;
			_AddExcludedTag = null;
			_SetReturnOnlyIDs = null;
			_SetReturnKeyValueTags = null;
			_SetReturnLongDescription = null;
			_SetReturnMetadata = null;
			_SetReturnChildren = null;
			_SetReturnAdditionalPreviews = null;
			_SetReturnTotalOnly = null;
			_SetReturnPlaytimeStats = null;
			_SetLanguage = null;
			_SetAllowCachedResponse = null;
			_SetCloudFileNameFilter = null;
			_SetMatchAnyTag = null;
			_SetSearchText = null;
			_SetRankedByTrendDays = null;
			_AddRequiredKeyValueTag = null;
			_RequestUGCDetails = null;
			_CreateItem = null;
			_StartItemUpdate = null;
			_SetItemTitle = null;
			_SetItemDescription = null;
			_SetItemUpdateLanguage = null;
			_SetItemMetadata = null;
			_SetItemVisibility = null;
			_SetItemTags = null;
			_SetItemContent = null;
			_SetItemPreview = null;
			_SetAllowLegacyUpload = null;
			_RemoveItemKeyValueTags = null;
			_AddItemKeyValueTag = null;
			_AddItemPreviewFile = null;
			_AddItemPreviewVideo = null;
			_UpdateItemPreviewFile = null;
			_UpdateItemPreviewVideo = null;
			_RemoveItemPreview = null;
			_SubmitItemUpdate = null;
			_GetItemUpdateProgress = null;
			_SetUserItemVote = null;
			_GetUserItemVote = null;
			_AddItemToFavorites = null;
			_RemoveItemFromFavorites = null;
			_SubscribeItem = null;
			_UnsubscribeItem = null;
			_GetNumSubscribedItems = null;
			_GetSubscribedItems = null;
			_GetItemState = null;
			_GetItemInstallInfo = null;
			_GetItemDownloadInfo = null;
			_DownloadItem = null;
			_BInitWorkshopForGameServer = null;
			_SuspendDownloads = null;
			_StartPlaytimeTracking = null;
			_StopPlaytimeTracking = null;
			_StopPlaytimeTrackingForAllItems = null;
			_AddDependency = null;
			_RemoveDependency = null;
			_AddAppDependency = null;
			_RemoveAppDependency = null;
			_GetAppDependencies = null;
			_DeleteItem = null;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate UGCQueryHandle_t FCreateQueryUserUGCRequest( IntPtr self, AccountID_t unAccountID, UserUGCList eListType, UgcType eMatchingUGCType, UserUGCListSortOrder eSortOrder, AppId nCreatorAppID, AppId nConsumerAppID, uint unPage );
		private FCreateQueryUserUGCRequest _CreateQueryUserUGCRequest;
		
		#endregion
		internal UGCQueryHandle_t CreateQueryUserUGCRequest( AccountID_t unAccountID, UserUGCList eListType, UgcType eMatchingUGCType, UserUGCListSortOrder eSortOrder, AppId nCreatorAppID, AppId nConsumerAppID, uint unPage )
		{
			var returnValue = _CreateQueryUserUGCRequest( Self, unAccountID, eListType, eMatchingUGCType, eSortOrder, nCreatorAppID, nConsumerAppID, unPage );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate UGCQueryHandle_t FCreateQueryAllUGCRequest1( IntPtr self, UGCQuery eQueryType, UgcType eMatchingeMatchingUGCTypeFileType, AppId nCreatorAppID, AppId nConsumerAppID, uint unPage );
		private FCreateQueryAllUGCRequest1 _CreateQueryAllUGCRequest1;
		
		#endregion
		internal UGCQueryHandle_t CreateQueryAllUGCRequest1( UGCQuery eQueryType, UgcType eMatchingeMatchingUGCTypeFileType, AppId nCreatorAppID, AppId nConsumerAppID, uint unPage )
		{
			var returnValue = _CreateQueryAllUGCRequest1( Self, eQueryType, eMatchingeMatchingUGCTypeFileType, nCreatorAppID, nConsumerAppID, unPage );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate UGCQueryHandle_t FCreateQueryAllUGCRequest2( IntPtr self, UGCQuery eQueryType, UgcType eMatchingeMatchingUGCTypeFileType, AppId nCreatorAppID, AppId nConsumerAppID, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchCursor );
		private FCreateQueryAllUGCRequest2 _CreateQueryAllUGCRequest2;
		
		#endregion
		internal UGCQueryHandle_t CreateQueryAllUGCRequest2( UGCQuery eQueryType, UgcType eMatchingeMatchingUGCTypeFileType, AppId nCreatorAppID, AppId nConsumerAppID, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchCursor )
		{
			var returnValue = _CreateQueryAllUGCRequest2( Self, eQueryType, eMatchingeMatchingUGCTypeFileType, nCreatorAppID, nConsumerAppID, pchCursor );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate UGCQueryHandle_t FCreateQueryUGCDetailsRequest( IntPtr self, [In,Out] PublishedFileId[]  pvecPublishedFileID, uint unNumPublishedFileIDs );
		private FCreateQueryUGCDetailsRequest _CreateQueryUGCDetailsRequest;
		
		#endregion
		internal UGCQueryHandle_t CreateQueryUGCDetailsRequest( [In,Out] PublishedFileId[]  pvecPublishedFileID, uint unNumPublishedFileIDs )
		{
			var returnValue = _CreateQueryUGCDetailsRequest( Self, pvecPublishedFileID, unNumPublishedFileIDs );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate SteamAPICall_t FSendQueryUGCRequest( IntPtr self, UGCQueryHandle_t handle );
		private FSendQueryUGCRequest _SendQueryUGCRequest;
		
		#endregion
		internal async Task<SteamUGCQueryCompleted_t?> SendQueryUGCRequest( UGCQueryHandle_t handle )
		{
			var returnValue = _SendQueryUGCRequest( Self, handle );
			return await SteamUGCQueryCompleted_t.GetResultAsync( returnValue );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FGetQueryUGCResult( IntPtr self, UGCQueryHandle_t handle, uint index, ref SteamUGCDetails_t pDetails );
		private FGetQueryUGCResult _GetQueryUGCResult;
		
		#endregion
		internal bool GetQueryUGCResult( UGCQueryHandle_t handle, uint index, ref SteamUGCDetails_t pDetails )
		{
			var returnValue = _GetQueryUGCResult( Self, handle, index, ref pDetails );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FGetQueryUGCPreviewURL( IntPtr self, UGCQueryHandle_t handle, uint index, IntPtr pchURL, uint cchURLSize );
		private FGetQueryUGCPreviewURL _GetQueryUGCPreviewURL;
		
		#endregion
		internal bool GetQueryUGCPreviewURL( UGCQueryHandle_t handle, uint index, out string pchURL )
		{
			IntPtr mempchURL = Helpers.TakeMemory();
			var returnValue = _GetQueryUGCPreviewURL( Self, handle, index, mempchURL, (1024 * 32) );
			pchURL = Helpers.MemoryToString( mempchURL );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FGetQueryUGCMetadata( IntPtr self, UGCQueryHandle_t handle, uint index, IntPtr pchMetadata, uint cchMetadatasize );
		private FGetQueryUGCMetadata _GetQueryUGCMetadata;
		
		#endregion
		internal bool GetQueryUGCMetadata( UGCQueryHandle_t handle, uint index, out string pchMetadata )
		{
			IntPtr mempchMetadata = Helpers.TakeMemory();
			var returnValue = _GetQueryUGCMetadata( Self, handle, index, mempchMetadata, (1024 * 32) );
			pchMetadata = Helpers.MemoryToString( mempchMetadata );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FGetQueryUGCChildren( IntPtr self, UGCQueryHandle_t handle, uint index, [In,Out] PublishedFileId[]  pvecPublishedFileID, uint cMaxEntries );
		private FGetQueryUGCChildren _GetQueryUGCChildren;
		
		#endregion
		internal bool GetQueryUGCChildren( UGCQueryHandle_t handle, uint index, [In,Out] PublishedFileId[]  pvecPublishedFileID, uint cMaxEntries )
		{
			var returnValue = _GetQueryUGCChildren( Self, handle, index, pvecPublishedFileID, cMaxEntries );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FGetQueryUGCStatistic( IntPtr self, UGCQueryHandle_t handle, uint index, ItemStatistic eStatType, ref ulong pStatValue );
		private FGetQueryUGCStatistic _GetQueryUGCStatistic;
		
		#endregion
		internal bool GetQueryUGCStatistic( UGCQueryHandle_t handle, uint index, ItemStatistic eStatType, ref ulong pStatValue )
		{
			var returnValue = _GetQueryUGCStatistic( Self, handle, index, eStatType, ref pStatValue );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate uint FGetQueryUGCNumAdditionalPreviews( IntPtr self, UGCQueryHandle_t handle, uint index );
		private FGetQueryUGCNumAdditionalPreviews _GetQueryUGCNumAdditionalPreviews;
		
		#endregion
		internal uint GetQueryUGCNumAdditionalPreviews( UGCQueryHandle_t handle, uint index )
		{
			var returnValue = _GetQueryUGCNumAdditionalPreviews( Self, handle, index );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FGetQueryUGCAdditionalPreview( IntPtr self, UGCQueryHandle_t handle, uint index, uint previewIndex, IntPtr pchURLOrVideoID, uint cchURLSize, IntPtr pchOriginalFileName, uint cchOriginalFileNameSize, ref ItemPreviewType pPreviewType );
		private FGetQueryUGCAdditionalPreview _GetQueryUGCAdditionalPreview;
		
		#endregion
		internal bool GetQueryUGCAdditionalPreview( UGCQueryHandle_t handle, uint index, uint previewIndex, out string pchURLOrVideoID, out string pchOriginalFileName, ref ItemPreviewType pPreviewType )
		{
			IntPtr mempchURLOrVideoID = Helpers.TakeMemory();
			IntPtr mempchOriginalFileName = Helpers.TakeMemory();
			var returnValue = _GetQueryUGCAdditionalPreview( Self, handle, index, previewIndex, mempchURLOrVideoID, (1024 * 32), mempchOriginalFileName, (1024 * 32), ref pPreviewType );
			pchURLOrVideoID = Helpers.MemoryToString( mempchURLOrVideoID );
			pchOriginalFileName = Helpers.MemoryToString( mempchOriginalFileName );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate uint FGetQueryUGCNumKeyValueTags( IntPtr self, UGCQueryHandle_t handle, uint index );
		private FGetQueryUGCNumKeyValueTags _GetQueryUGCNumKeyValueTags;
		
		#endregion
		internal uint GetQueryUGCNumKeyValueTags( UGCQueryHandle_t handle, uint index )
		{
			var returnValue = _GetQueryUGCNumKeyValueTags( Self, handle, index );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FGetQueryUGCKeyValueTag( IntPtr self, UGCQueryHandle_t handle, uint index, uint keyValueTagIndex, IntPtr pchKey, uint cchKeySize, IntPtr pchValue, uint cchValueSize );
		private FGetQueryUGCKeyValueTag _GetQueryUGCKeyValueTag;
		
		#endregion
		internal bool GetQueryUGCKeyValueTag( UGCQueryHandle_t handle, uint index, uint keyValueTagIndex, out string pchKey, out string pchValue )
		{
			IntPtr mempchKey = Helpers.TakeMemory();
			IntPtr mempchValue = Helpers.TakeMemory();
			var returnValue = _GetQueryUGCKeyValueTag( Self, handle, index, keyValueTagIndex, mempchKey, (1024 * 32), mempchValue, (1024 * 32) );
			pchKey = Helpers.MemoryToString( mempchKey );
			pchValue = Helpers.MemoryToString( mempchValue );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FReleaseQueryUGCRequest( IntPtr self, UGCQueryHandle_t handle );
		private FReleaseQueryUGCRequest _ReleaseQueryUGCRequest;
		
		#endregion
		internal bool ReleaseQueryUGCRequest( UGCQueryHandle_t handle )
		{
			var returnValue = _ReleaseQueryUGCRequest( Self, handle );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FAddRequiredTag( IntPtr self, UGCQueryHandle_t handle, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pTagName );
		private FAddRequiredTag _AddRequiredTag;
		
		#endregion
		internal bool AddRequiredTag( UGCQueryHandle_t handle, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pTagName )
		{
			var returnValue = _AddRequiredTag( Self, handle, pTagName );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FAddExcludedTag( IntPtr self, UGCQueryHandle_t handle, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pTagName );
		private FAddExcludedTag _AddExcludedTag;
		
		#endregion
		internal bool AddExcludedTag( UGCQueryHandle_t handle, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pTagName )
		{
			var returnValue = _AddExcludedTag( Self, handle, pTagName );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FSetReturnOnlyIDs( IntPtr self, UGCQueryHandle_t handle, [MarshalAs( UnmanagedType.U1 )] bool bReturnOnlyIDs );
		private FSetReturnOnlyIDs _SetReturnOnlyIDs;
		
		#endregion
		internal bool SetReturnOnlyIDs( UGCQueryHandle_t handle, [MarshalAs( UnmanagedType.U1 )] bool bReturnOnlyIDs )
		{
			var returnValue = _SetReturnOnlyIDs( Self, handle, bReturnOnlyIDs );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FSetReturnKeyValueTags( IntPtr self, UGCQueryHandle_t handle, [MarshalAs( UnmanagedType.U1 )] bool bReturnKeyValueTags );
		private FSetReturnKeyValueTags _SetReturnKeyValueTags;
		
		#endregion
		internal bool SetReturnKeyValueTags( UGCQueryHandle_t handle, [MarshalAs( UnmanagedType.U1 )] bool bReturnKeyValueTags )
		{
			var returnValue = _SetReturnKeyValueTags( Self, handle, bReturnKeyValueTags );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FSetReturnLongDescription( IntPtr self, UGCQueryHandle_t handle, [MarshalAs( UnmanagedType.U1 )] bool bReturnLongDescription );
		private FSetReturnLongDescription _SetReturnLongDescription;
		
		#endregion
		internal bool SetReturnLongDescription( UGCQueryHandle_t handle, [MarshalAs( UnmanagedType.U1 )] bool bReturnLongDescription )
		{
			var returnValue = _SetReturnLongDescription( Self, handle, bReturnLongDescription );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FSetReturnMetadata( IntPtr self, UGCQueryHandle_t handle, [MarshalAs( UnmanagedType.U1 )] bool bReturnMetadata );
		private FSetReturnMetadata _SetReturnMetadata;
		
		#endregion
		internal bool SetReturnMetadata( UGCQueryHandle_t handle, [MarshalAs( UnmanagedType.U1 )] bool bReturnMetadata )
		{
			var returnValue = _SetReturnMetadata( Self, handle, bReturnMetadata );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FSetReturnChildren( IntPtr self, UGCQueryHandle_t handle, [MarshalAs( UnmanagedType.U1 )] bool bReturnChildren );
		private FSetReturnChildren _SetReturnChildren;
		
		#endregion
		internal bool SetReturnChildren( UGCQueryHandle_t handle, [MarshalAs( UnmanagedType.U1 )] bool bReturnChildren )
		{
			var returnValue = _SetReturnChildren( Self, handle, bReturnChildren );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FSetReturnAdditionalPreviews( IntPtr self, UGCQueryHandle_t handle, [MarshalAs( UnmanagedType.U1 )] bool bReturnAdditionalPreviews );
		private FSetReturnAdditionalPreviews _SetReturnAdditionalPreviews;
		
		#endregion
		internal bool SetReturnAdditionalPreviews( UGCQueryHandle_t handle, [MarshalAs( UnmanagedType.U1 )] bool bReturnAdditionalPreviews )
		{
			var returnValue = _SetReturnAdditionalPreviews( Self, handle, bReturnAdditionalPreviews );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FSetReturnTotalOnly( IntPtr self, UGCQueryHandle_t handle, [MarshalAs( UnmanagedType.U1 )] bool bReturnTotalOnly );
		private FSetReturnTotalOnly _SetReturnTotalOnly;
		
		#endregion
		internal bool SetReturnTotalOnly( UGCQueryHandle_t handle, [MarshalAs( UnmanagedType.U1 )] bool bReturnTotalOnly )
		{
			var returnValue = _SetReturnTotalOnly( Self, handle, bReturnTotalOnly );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FSetReturnPlaytimeStats( IntPtr self, UGCQueryHandle_t handle, uint unDays );
		private FSetReturnPlaytimeStats _SetReturnPlaytimeStats;
		
		#endregion
		internal bool SetReturnPlaytimeStats( UGCQueryHandle_t handle, uint unDays )
		{
			var returnValue = _SetReturnPlaytimeStats( Self, handle, unDays );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FSetLanguage( IntPtr self, UGCQueryHandle_t handle, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchLanguage );
		private FSetLanguage _SetLanguage;
		
		#endregion
		internal bool SetLanguage( UGCQueryHandle_t handle, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchLanguage )
		{
			var returnValue = _SetLanguage( Self, handle, pchLanguage );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FSetAllowCachedResponse( IntPtr self, UGCQueryHandle_t handle, uint unMaxAgeSeconds );
		private FSetAllowCachedResponse _SetAllowCachedResponse;
		
		#endregion
		internal bool SetAllowCachedResponse( UGCQueryHandle_t handle, uint unMaxAgeSeconds )
		{
			var returnValue = _SetAllowCachedResponse( Self, handle, unMaxAgeSeconds );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FSetCloudFileNameFilter( IntPtr self, UGCQueryHandle_t handle, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pMatchCloudFileName );
		private FSetCloudFileNameFilter _SetCloudFileNameFilter;
		
		#endregion
		internal bool SetCloudFileNameFilter( UGCQueryHandle_t handle, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pMatchCloudFileName )
		{
			var returnValue = _SetCloudFileNameFilter( Self, handle, pMatchCloudFileName );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FSetMatchAnyTag( IntPtr self, UGCQueryHandle_t handle, [MarshalAs( UnmanagedType.U1 )] bool bMatchAnyTag );
		private FSetMatchAnyTag _SetMatchAnyTag;
		
		#endregion
		internal bool SetMatchAnyTag( UGCQueryHandle_t handle, [MarshalAs( UnmanagedType.U1 )] bool bMatchAnyTag )
		{
			var returnValue = _SetMatchAnyTag( Self, handle, bMatchAnyTag );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FSetSearchText( IntPtr self, UGCQueryHandle_t handle, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pSearchText );
		private FSetSearchText _SetSearchText;
		
		#endregion
		internal bool SetSearchText( UGCQueryHandle_t handle, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pSearchText )
		{
			var returnValue = _SetSearchText( Self, handle, pSearchText );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FSetRankedByTrendDays( IntPtr self, UGCQueryHandle_t handle, uint unDays );
		private FSetRankedByTrendDays _SetRankedByTrendDays;
		
		#endregion
		internal bool SetRankedByTrendDays( UGCQueryHandle_t handle, uint unDays )
		{
			var returnValue = _SetRankedByTrendDays( Self, handle, unDays );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FAddRequiredKeyValueTag( IntPtr self, UGCQueryHandle_t handle, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pKey, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pValue );
		private FAddRequiredKeyValueTag _AddRequiredKeyValueTag;
		
		#endregion
		internal bool AddRequiredKeyValueTag( UGCQueryHandle_t handle, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pKey, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pValue )
		{
			var returnValue = _AddRequiredKeyValueTag( Self, handle, pKey, pValue );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate SteamAPICall_t FRequestUGCDetails( IntPtr self, PublishedFileId nPublishedFileID, uint unMaxAgeSeconds );
		private FRequestUGCDetails _RequestUGCDetails;
		
		#endregion
		internal async Task<SteamUGCRequestUGCDetailsResult_t?> RequestUGCDetails( PublishedFileId nPublishedFileID, uint unMaxAgeSeconds )
		{
			var returnValue = _RequestUGCDetails( Self, nPublishedFileID, unMaxAgeSeconds );
			return await SteamUGCRequestUGCDetailsResult_t.GetResultAsync( returnValue );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate SteamAPICall_t FCreateItem( IntPtr self, AppId nConsumerAppId, WorkshopFileType eFileType );
		private FCreateItem _CreateItem;
		
		#endregion
		internal async Task<CreateItemResult_t?> CreateItem( AppId nConsumerAppId, WorkshopFileType eFileType )
		{
			var returnValue = _CreateItem( Self, nConsumerAppId, eFileType );
			return await CreateItemResult_t.GetResultAsync( returnValue );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate UGCUpdateHandle_t FStartItemUpdate( IntPtr self, AppId nConsumerAppId, PublishedFileId nPublishedFileID );
		private FStartItemUpdate _StartItemUpdate;
		
		#endregion
		internal UGCUpdateHandle_t StartItemUpdate( AppId nConsumerAppId, PublishedFileId nPublishedFileID )
		{
			var returnValue = _StartItemUpdate( Self, nConsumerAppId, nPublishedFileID );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FSetItemTitle( IntPtr self, UGCUpdateHandle_t handle, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchTitle );
		private FSetItemTitle _SetItemTitle;
		
		#endregion
		internal bool SetItemTitle( UGCUpdateHandle_t handle, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchTitle )
		{
			var returnValue = _SetItemTitle( Self, handle, pchTitle );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FSetItemDescription( IntPtr self, UGCUpdateHandle_t handle, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchDescription );
		private FSetItemDescription _SetItemDescription;
		
		#endregion
		internal bool SetItemDescription( UGCUpdateHandle_t handle, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchDescription )
		{
			var returnValue = _SetItemDescription( Self, handle, pchDescription );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FSetItemUpdateLanguage( IntPtr self, UGCUpdateHandle_t handle, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchLanguage );
		private FSetItemUpdateLanguage _SetItemUpdateLanguage;
		
		#endregion
		internal bool SetItemUpdateLanguage( UGCUpdateHandle_t handle, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchLanguage )
		{
			var returnValue = _SetItemUpdateLanguage( Self, handle, pchLanguage );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FSetItemMetadata( IntPtr self, UGCUpdateHandle_t handle, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchMetaData );
		private FSetItemMetadata _SetItemMetadata;
		
		#endregion
		internal bool SetItemMetadata( UGCUpdateHandle_t handle, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchMetaData )
		{
			var returnValue = _SetItemMetadata( Self, handle, pchMetaData );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FSetItemVisibility( IntPtr self, UGCUpdateHandle_t handle, RemoteStoragePublishedFileVisibility eVisibility );
		private FSetItemVisibility _SetItemVisibility;
		
		#endregion
		internal bool SetItemVisibility( UGCUpdateHandle_t handle, RemoteStoragePublishedFileVisibility eVisibility )
		{
			var returnValue = _SetItemVisibility( Self, handle, eVisibility );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FSetItemTags( IntPtr self, UGCUpdateHandle_t updateHandle, ref SteamParamStringArray_t pTags );
		private FSetItemTags _SetItemTags;
		
		#endregion
		internal bool SetItemTags( UGCUpdateHandle_t updateHandle, ref SteamParamStringArray_t pTags )
		{
			var returnValue = _SetItemTags( Self, updateHandle, ref pTags );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FSetItemContent( IntPtr self, UGCUpdateHandle_t handle, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pszContentFolder );
		private FSetItemContent _SetItemContent;
		
		#endregion
		internal bool SetItemContent( UGCUpdateHandle_t handle, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pszContentFolder )
		{
			var returnValue = _SetItemContent( Self, handle, pszContentFolder );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FSetItemPreview( IntPtr self, UGCUpdateHandle_t handle, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pszPreviewFile );
		private FSetItemPreview _SetItemPreview;
		
		#endregion
		internal bool SetItemPreview( UGCUpdateHandle_t handle, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pszPreviewFile )
		{
			var returnValue = _SetItemPreview( Self, handle, pszPreviewFile );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FSetAllowLegacyUpload( IntPtr self, UGCUpdateHandle_t handle, [MarshalAs( UnmanagedType.U1 )] bool bAllowLegacyUpload );
		private FSetAllowLegacyUpload _SetAllowLegacyUpload;
		
		#endregion
		internal bool SetAllowLegacyUpload( UGCUpdateHandle_t handle, [MarshalAs( UnmanagedType.U1 )] bool bAllowLegacyUpload )
		{
			var returnValue = _SetAllowLegacyUpload( Self, handle, bAllowLegacyUpload );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FRemoveItemKeyValueTags( IntPtr self, UGCUpdateHandle_t handle, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchKey );
		private FRemoveItemKeyValueTags _RemoveItemKeyValueTags;
		
		#endregion
		internal bool RemoveItemKeyValueTags( UGCUpdateHandle_t handle, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchKey )
		{
			var returnValue = _RemoveItemKeyValueTags( Self, handle, pchKey );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FAddItemKeyValueTag( IntPtr self, UGCUpdateHandle_t handle, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchKey, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchValue );
		private FAddItemKeyValueTag _AddItemKeyValueTag;
		
		#endregion
		internal bool AddItemKeyValueTag( UGCUpdateHandle_t handle, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchKey, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchValue )
		{
			var returnValue = _AddItemKeyValueTag( Self, handle, pchKey, pchValue );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FAddItemPreviewFile( IntPtr self, UGCUpdateHandle_t handle, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pszPreviewFile, ItemPreviewType type );
		private FAddItemPreviewFile _AddItemPreviewFile;
		
		#endregion
		internal bool AddItemPreviewFile( UGCUpdateHandle_t handle, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pszPreviewFile, ItemPreviewType type )
		{
			var returnValue = _AddItemPreviewFile( Self, handle, pszPreviewFile, type );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FAddItemPreviewVideo( IntPtr self, UGCUpdateHandle_t handle, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pszVideoID );
		private FAddItemPreviewVideo _AddItemPreviewVideo;
		
		#endregion
		internal bool AddItemPreviewVideo( UGCUpdateHandle_t handle, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pszVideoID )
		{
			var returnValue = _AddItemPreviewVideo( Self, handle, pszVideoID );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FUpdateItemPreviewFile( IntPtr self, UGCUpdateHandle_t handle, uint index, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pszPreviewFile );
		private FUpdateItemPreviewFile _UpdateItemPreviewFile;
		
		#endregion
		internal bool UpdateItemPreviewFile( UGCUpdateHandle_t handle, uint index, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pszPreviewFile )
		{
			var returnValue = _UpdateItemPreviewFile( Self, handle, index, pszPreviewFile );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FUpdateItemPreviewVideo( IntPtr self, UGCUpdateHandle_t handle, uint index, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pszVideoID );
		private FUpdateItemPreviewVideo _UpdateItemPreviewVideo;
		
		#endregion
		internal bool UpdateItemPreviewVideo( UGCUpdateHandle_t handle, uint index, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pszVideoID )
		{
			var returnValue = _UpdateItemPreviewVideo( Self, handle, index, pszVideoID );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FRemoveItemPreview( IntPtr self, UGCUpdateHandle_t handle, uint index );
		private FRemoveItemPreview _RemoveItemPreview;
		
		#endregion
		internal bool RemoveItemPreview( UGCUpdateHandle_t handle, uint index )
		{
			var returnValue = _RemoveItemPreview( Self, handle, index );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate SteamAPICall_t FSubmitItemUpdate( IntPtr self, UGCUpdateHandle_t handle, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchChangeNote );
		private FSubmitItemUpdate _SubmitItemUpdate;
		
		#endregion
		internal async Task<SubmitItemUpdateResult_t?> SubmitItemUpdate( UGCUpdateHandle_t handle, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchChangeNote )
		{
			var returnValue = _SubmitItemUpdate( Self, handle, pchChangeNote );
			return await SubmitItemUpdateResult_t.GetResultAsync( returnValue );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate ItemUpdateStatus FGetItemUpdateProgress( IntPtr self, UGCUpdateHandle_t handle, ref ulong punBytesProcessed, ref ulong punBytesTotal );
		private FGetItemUpdateProgress _GetItemUpdateProgress;
		
		#endregion
		internal ItemUpdateStatus GetItemUpdateProgress( UGCUpdateHandle_t handle, ref ulong punBytesProcessed, ref ulong punBytesTotal )
		{
			var returnValue = _GetItemUpdateProgress( Self, handle, ref punBytesProcessed, ref punBytesTotal );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate SteamAPICall_t FSetUserItemVote( IntPtr self, PublishedFileId nPublishedFileID, [MarshalAs( UnmanagedType.U1 )] bool bVoteUp );
		private FSetUserItemVote _SetUserItemVote;
		
		#endregion
		internal async Task<SetUserItemVoteResult_t?> SetUserItemVote( PublishedFileId nPublishedFileID, [MarshalAs( UnmanagedType.U1 )] bool bVoteUp )
		{
			var returnValue = _SetUserItemVote( Self, nPublishedFileID, bVoteUp );
			return await SetUserItemVoteResult_t.GetResultAsync( returnValue );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate SteamAPICall_t FGetUserItemVote( IntPtr self, PublishedFileId nPublishedFileID );
		private FGetUserItemVote _GetUserItemVote;
		
		#endregion
		internal async Task<GetUserItemVoteResult_t?> GetUserItemVote( PublishedFileId nPublishedFileID )
		{
			var returnValue = _GetUserItemVote( Self, nPublishedFileID );
			return await GetUserItemVoteResult_t.GetResultAsync( returnValue );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate SteamAPICall_t FAddItemToFavorites( IntPtr self, AppId nAppId, PublishedFileId nPublishedFileID );
		private FAddItemToFavorites _AddItemToFavorites;
		
		#endregion
		internal async Task<UserFavoriteItemsListChanged_t?> AddItemToFavorites( AppId nAppId, PublishedFileId nPublishedFileID )
		{
			var returnValue = _AddItemToFavorites( Self, nAppId, nPublishedFileID );
			return await UserFavoriteItemsListChanged_t.GetResultAsync( returnValue );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate SteamAPICall_t FRemoveItemFromFavorites( IntPtr self, AppId nAppId, PublishedFileId nPublishedFileID );
		private FRemoveItemFromFavorites _RemoveItemFromFavorites;
		
		#endregion
		internal async Task<UserFavoriteItemsListChanged_t?> RemoveItemFromFavorites( AppId nAppId, PublishedFileId nPublishedFileID )
		{
			var returnValue = _RemoveItemFromFavorites( Self, nAppId, nPublishedFileID );
			return await UserFavoriteItemsListChanged_t.GetResultAsync( returnValue );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate SteamAPICall_t FSubscribeItem( IntPtr self, PublishedFileId nPublishedFileID );
		private FSubscribeItem _SubscribeItem;
		
		#endregion
		internal async Task<RemoteStorageSubscribePublishedFileResult_t?> SubscribeItem( PublishedFileId nPublishedFileID )
		{
			var returnValue = _SubscribeItem( Self, nPublishedFileID );
			return await RemoteStorageSubscribePublishedFileResult_t.GetResultAsync( returnValue );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate SteamAPICall_t FUnsubscribeItem( IntPtr self, PublishedFileId nPublishedFileID );
		private FUnsubscribeItem _UnsubscribeItem;
		
		#endregion
		internal async Task<RemoteStorageUnsubscribePublishedFileResult_t?> UnsubscribeItem( PublishedFileId nPublishedFileID )
		{
			var returnValue = _UnsubscribeItem( Self, nPublishedFileID );
			return await RemoteStorageUnsubscribePublishedFileResult_t.GetResultAsync( returnValue );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate uint FGetNumSubscribedItems( IntPtr self );
		private FGetNumSubscribedItems _GetNumSubscribedItems;
		
		#endregion
		internal uint GetNumSubscribedItems()
		{
			var returnValue = _GetNumSubscribedItems( Self );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate uint FGetSubscribedItems( IntPtr self, [In,Out] PublishedFileId[]  pvecPublishedFileID, uint cMaxEntries );
		private FGetSubscribedItems _GetSubscribedItems;
		
		#endregion
		internal uint GetSubscribedItems( [In,Out] PublishedFileId[]  pvecPublishedFileID, uint cMaxEntries )
		{
			var returnValue = _GetSubscribedItems( Self, pvecPublishedFileID, cMaxEntries );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate uint FGetItemState( IntPtr self, PublishedFileId nPublishedFileID );
		private FGetItemState _GetItemState;
		
		#endregion
		internal uint GetItemState( PublishedFileId nPublishedFileID )
		{
			var returnValue = _GetItemState( Self, nPublishedFileID );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FGetItemInstallInfo( IntPtr self, PublishedFileId nPublishedFileID, ref ulong punSizeOnDisk, IntPtr pchFolder, uint cchFolderSize, ref uint punTimeStamp );
		private FGetItemInstallInfo _GetItemInstallInfo;
		
		#endregion
		internal bool GetItemInstallInfo( PublishedFileId nPublishedFileID, ref ulong punSizeOnDisk, out string pchFolder, ref uint punTimeStamp )
		{
			IntPtr mempchFolder = Helpers.TakeMemory();
			var returnValue = _GetItemInstallInfo( Self, nPublishedFileID, ref punSizeOnDisk, mempchFolder, (1024 * 32), ref punTimeStamp );
			pchFolder = Helpers.MemoryToString( mempchFolder );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FGetItemDownloadInfo( IntPtr self, PublishedFileId nPublishedFileID, ref ulong punBytesDownloaded, ref ulong punBytesTotal );
		private FGetItemDownloadInfo _GetItemDownloadInfo;
		
		#endregion
		internal bool GetItemDownloadInfo( PublishedFileId nPublishedFileID, ref ulong punBytesDownloaded, ref ulong punBytesTotal )
		{
			var returnValue = _GetItemDownloadInfo( Self, nPublishedFileID, ref punBytesDownloaded, ref punBytesTotal );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FDownloadItem( IntPtr self, PublishedFileId nPublishedFileID, [MarshalAs( UnmanagedType.U1 )] bool bHighPriority );
		private FDownloadItem _DownloadItem;
		
		#endregion
		internal bool DownloadItem( PublishedFileId nPublishedFileID, [MarshalAs( UnmanagedType.U1 )] bool bHighPriority )
		{
			var returnValue = _DownloadItem( Self, nPublishedFileID, bHighPriority );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FBInitWorkshopForGameServer( IntPtr self, DepotId_t unWorkshopDepotID, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pszFolder );
		private FBInitWorkshopForGameServer _BInitWorkshopForGameServer;
		
		#endregion
		internal bool BInitWorkshopForGameServer( DepotId_t unWorkshopDepotID, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pszFolder )
		{
			var returnValue = _BInitWorkshopForGameServer( Self, unWorkshopDepotID, pszFolder );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate void FSuspendDownloads( IntPtr self, [MarshalAs( UnmanagedType.U1 )] bool bSuspend );
		private FSuspendDownloads _SuspendDownloads;
		
		#endregion
		internal void SuspendDownloads( [MarshalAs( UnmanagedType.U1 )] bool bSuspend )
		{
			_SuspendDownloads( Self, bSuspend );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate SteamAPICall_t FStartPlaytimeTracking( IntPtr self, [In,Out] PublishedFileId[]  pvecPublishedFileID, uint unNumPublishedFileIDs );
		private FStartPlaytimeTracking _StartPlaytimeTracking;
		
		#endregion
		internal async Task<StartPlaytimeTrackingResult_t?> StartPlaytimeTracking( [In,Out] PublishedFileId[]  pvecPublishedFileID, uint unNumPublishedFileIDs )
		{
			var returnValue = _StartPlaytimeTracking( Self, pvecPublishedFileID, unNumPublishedFileIDs );
			return await StartPlaytimeTrackingResult_t.GetResultAsync( returnValue );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate SteamAPICall_t FStopPlaytimeTracking( IntPtr self, [In,Out] PublishedFileId[]  pvecPublishedFileID, uint unNumPublishedFileIDs );
		private FStopPlaytimeTracking _StopPlaytimeTracking;
		
		#endregion
		internal async Task<StopPlaytimeTrackingResult_t?> StopPlaytimeTracking( [In,Out] PublishedFileId[]  pvecPublishedFileID, uint unNumPublishedFileIDs )
		{
			var returnValue = _StopPlaytimeTracking( Self, pvecPublishedFileID, unNumPublishedFileIDs );
			return await StopPlaytimeTrackingResult_t.GetResultAsync( returnValue );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate SteamAPICall_t FStopPlaytimeTrackingForAllItems( IntPtr self );
		private FStopPlaytimeTrackingForAllItems _StopPlaytimeTrackingForAllItems;
		
		#endregion
		internal async Task<StopPlaytimeTrackingResult_t?> StopPlaytimeTrackingForAllItems()
		{
			var returnValue = _StopPlaytimeTrackingForAllItems( Self );
			return await StopPlaytimeTrackingResult_t.GetResultAsync( returnValue );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate SteamAPICall_t FAddDependency( IntPtr self, PublishedFileId nParentPublishedFileID, PublishedFileId nChildPublishedFileID );
		private FAddDependency _AddDependency;
		
		#endregion
		internal async Task<AddUGCDependencyResult_t?> AddDependency( PublishedFileId nParentPublishedFileID, PublishedFileId nChildPublishedFileID )
		{
			var returnValue = _AddDependency( Self, nParentPublishedFileID, nChildPublishedFileID );
			return await AddUGCDependencyResult_t.GetResultAsync( returnValue );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate SteamAPICall_t FRemoveDependency( IntPtr self, PublishedFileId nParentPublishedFileID, PublishedFileId nChildPublishedFileID );
		private FRemoveDependency _RemoveDependency;
		
		#endregion
		internal async Task<RemoveUGCDependencyResult_t?> RemoveDependency( PublishedFileId nParentPublishedFileID, PublishedFileId nChildPublishedFileID )
		{
			var returnValue = _RemoveDependency( Self, nParentPublishedFileID, nChildPublishedFileID );
			return await RemoveUGCDependencyResult_t.GetResultAsync( returnValue );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate SteamAPICall_t FAddAppDependency( IntPtr self, PublishedFileId nPublishedFileID, AppId nAppID );
		private FAddAppDependency _AddAppDependency;
		
		#endregion
		internal async Task<AddAppDependencyResult_t?> AddAppDependency( PublishedFileId nPublishedFileID, AppId nAppID )
		{
			var returnValue = _AddAppDependency( Self, nPublishedFileID, nAppID );
			return await AddAppDependencyResult_t.GetResultAsync( returnValue );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate SteamAPICall_t FRemoveAppDependency( IntPtr self, PublishedFileId nPublishedFileID, AppId nAppID );
		private FRemoveAppDependency _RemoveAppDependency;
		
		#endregion
		internal async Task<RemoveAppDependencyResult_t?> RemoveAppDependency( PublishedFileId nPublishedFileID, AppId nAppID )
		{
			var returnValue = _RemoveAppDependency( Self, nPublishedFileID, nAppID );
			return await RemoveAppDependencyResult_t.GetResultAsync( returnValue );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate SteamAPICall_t FGetAppDependencies( IntPtr self, PublishedFileId nPublishedFileID );
		private FGetAppDependencies _GetAppDependencies;
		
		#endregion
		internal async Task<GetAppDependenciesResult_t?> GetAppDependencies( PublishedFileId nPublishedFileID )
		{
			var returnValue = _GetAppDependencies( Self, nPublishedFileID );
			return await GetAppDependenciesResult_t.GetResultAsync( returnValue );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate SteamAPICall_t FDeleteItem( IntPtr self, PublishedFileId nPublishedFileID );
		private FDeleteItem _DeleteItem;
		
		#endregion
		internal async Task<DeleteItemResult_t?> DeleteItem( PublishedFileId nPublishedFileID )
		{
			var returnValue = _DeleteItem( Self, nPublishedFileID );
			return await DeleteItemResult_t.GetResultAsync( returnValue );
		}
		
	}
}
