using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Steamworks.Data;


namespace Steamworks
{
	internal class ISteamUGC : SteamInterface
	{
		public ISteamUGC( bool server = false ) : base( server )
		{
		}
		
		public override string InterfaceName => "STEAMUGC_INTERFACE_VERSION012";
		
		public override void InitInternals()
		{
			_CreateQueryUserUGCRequest = Marshal.GetDelegateForFunctionPointer<FCreateQueryUserUGCRequest>( Marshal.ReadIntPtr( VTable, 0) );
			_CreateQueryAllUGCRequest1 = Marshal.GetDelegateForFunctionPointer<FCreateQueryAllUGCRequest1>( Marshal.ReadIntPtr( VTable, 16) );
			_CreateQueryAllUGCRequest2 = Marshal.GetDelegateForFunctionPointer<FCreateQueryAllUGCRequest2>( Marshal.ReadIntPtr( VTable, 8) );
			_CreateQueryUGCDetailsRequest = Marshal.GetDelegateForFunctionPointer<FCreateQueryUGCDetailsRequest>( Marshal.ReadIntPtr( VTable, 24) );
			_SendQueryUGCRequest = Marshal.GetDelegateForFunctionPointer<FSendQueryUGCRequest>( Marshal.ReadIntPtr( VTable, 32) );
			_GetQueryUGCResult = Marshal.GetDelegateForFunctionPointer<FGetQueryUGCResult>( Marshal.ReadIntPtr( VTable, 40) );
			_GetQueryUGCPreviewURL = Marshal.GetDelegateForFunctionPointer<FGetQueryUGCPreviewURL>( Marshal.ReadIntPtr( VTable, 48) );
			_GetQueryUGCMetadata = Marshal.GetDelegateForFunctionPointer<FGetQueryUGCMetadata>( Marshal.ReadIntPtr( VTable, 56) );
			_GetQueryUGCChildren = Marshal.GetDelegateForFunctionPointer<FGetQueryUGCChildren>( Marshal.ReadIntPtr( VTable, 64) );
			_GetQueryUGCStatistic = Marshal.GetDelegateForFunctionPointer<FGetQueryUGCStatistic>( Marshal.ReadIntPtr( VTable, 72) );
			_GetQueryUGCNumAdditionalPreviews = Marshal.GetDelegateForFunctionPointer<FGetQueryUGCNumAdditionalPreviews>( Marshal.ReadIntPtr( VTable, 80) );
			_GetQueryUGCAdditionalPreview = Marshal.GetDelegateForFunctionPointer<FGetQueryUGCAdditionalPreview>( Marshal.ReadIntPtr( VTable, 88) );
			_GetQueryUGCNumKeyValueTags = Marshal.GetDelegateForFunctionPointer<FGetQueryUGCNumKeyValueTags>( Marshal.ReadIntPtr( VTable, 96) );
			_GetQueryUGCKeyValueTag = Marshal.GetDelegateForFunctionPointer<FGetQueryUGCKeyValueTag>( Marshal.ReadIntPtr( VTable, 104) );
			_ReleaseQueryUGCRequest = Marshal.GetDelegateForFunctionPointer<FReleaseQueryUGCRequest>( Marshal.ReadIntPtr( VTable, 112) );
			_AddRequiredTag = Marshal.GetDelegateForFunctionPointer<FAddRequiredTag>( Marshal.ReadIntPtr( VTable, 120) );
			_AddExcludedTag = Marshal.GetDelegateForFunctionPointer<FAddExcludedTag>( Marshal.ReadIntPtr( VTable, 128) );
			_SetReturnOnlyIDs = Marshal.GetDelegateForFunctionPointer<FSetReturnOnlyIDs>( Marshal.ReadIntPtr( VTable, 136) );
			_SetReturnKeyValueTags = Marshal.GetDelegateForFunctionPointer<FSetReturnKeyValueTags>( Marshal.ReadIntPtr( VTable, 144) );
			_SetReturnLongDescription = Marshal.GetDelegateForFunctionPointer<FSetReturnLongDescription>( Marshal.ReadIntPtr( VTable, 152) );
			_SetReturnMetadata = Marshal.GetDelegateForFunctionPointer<FSetReturnMetadata>( Marshal.ReadIntPtr( VTable, 160) );
			_SetReturnChildren = Marshal.GetDelegateForFunctionPointer<FSetReturnChildren>( Marshal.ReadIntPtr( VTable, 168) );
			_SetReturnAdditionalPreviews = Marshal.GetDelegateForFunctionPointer<FSetReturnAdditionalPreviews>( Marshal.ReadIntPtr( VTable, 176) );
			_SetReturnTotalOnly = Marshal.GetDelegateForFunctionPointer<FSetReturnTotalOnly>( Marshal.ReadIntPtr( VTable, 184) );
			_SetReturnPlaytimeStats = Marshal.GetDelegateForFunctionPointer<FSetReturnPlaytimeStats>( Marshal.ReadIntPtr( VTable, 192) );
			_SetLanguage = Marshal.GetDelegateForFunctionPointer<FSetLanguage>( Marshal.ReadIntPtr( VTable, 200) );
			_SetAllowCachedResponse = Marshal.GetDelegateForFunctionPointer<FSetAllowCachedResponse>( Marshal.ReadIntPtr( VTable, 208) );
			_SetCloudFileNameFilter = Marshal.GetDelegateForFunctionPointer<FSetCloudFileNameFilter>( Marshal.ReadIntPtr( VTable, 216) );
			_SetMatchAnyTag = Marshal.GetDelegateForFunctionPointer<FSetMatchAnyTag>( Marshal.ReadIntPtr( VTable, 224) );
			_SetSearchText = Marshal.GetDelegateForFunctionPointer<FSetSearchText>( Marshal.ReadIntPtr( VTable, 232) );
			_SetRankedByTrendDays = Marshal.GetDelegateForFunctionPointer<FSetRankedByTrendDays>( Marshal.ReadIntPtr( VTable, 240) );
			_AddRequiredKeyValueTag = Marshal.GetDelegateForFunctionPointer<FAddRequiredKeyValueTag>( Marshal.ReadIntPtr( VTable, 248) );
			_RequestUGCDetails = Marshal.GetDelegateForFunctionPointer<FRequestUGCDetails>( Marshal.ReadIntPtr( VTable, 256) );
			_CreateItem = Marshal.GetDelegateForFunctionPointer<FCreateItem>( Marshal.ReadIntPtr( VTable, 264) );
			_StartItemUpdate = Marshal.GetDelegateForFunctionPointer<FStartItemUpdate>( Marshal.ReadIntPtr( VTable, 272) );
			_SetItemTitle = Marshal.GetDelegateForFunctionPointer<FSetItemTitle>( Marshal.ReadIntPtr( VTable, 280) );
			_SetItemDescription = Marshal.GetDelegateForFunctionPointer<FSetItemDescription>( Marshal.ReadIntPtr( VTable, 288) );
			_SetItemUpdateLanguage = Marshal.GetDelegateForFunctionPointer<FSetItemUpdateLanguage>( Marshal.ReadIntPtr( VTable, 296) );
			_SetItemMetadata = Marshal.GetDelegateForFunctionPointer<FSetItemMetadata>( Marshal.ReadIntPtr( VTable, 304) );
			_SetItemVisibility = Marshal.GetDelegateForFunctionPointer<FSetItemVisibility>( Marshal.ReadIntPtr( VTable, 312) );
			_SetItemTags = Marshal.GetDelegateForFunctionPointer<FSetItemTags>( Marshal.ReadIntPtr( VTable, 320) );
			_SetItemContent = Marshal.GetDelegateForFunctionPointer<FSetItemContent>( Marshal.ReadIntPtr( VTable, 328) );
			_SetItemPreview = Marshal.GetDelegateForFunctionPointer<FSetItemPreview>( Marshal.ReadIntPtr( VTable, 336) );
			_SetAllowLegacyUpload = Marshal.GetDelegateForFunctionPointer<FSetAllowLegacyUpload>( Marshal.ReadIntPtr( VTable, 344) );
			_RemoveItemKeyValueTags = Marshal.GetDelegateForFunctionPointer<FRemoveItemKeyValueTags>( Marshal.ReadIntPtr( VTable, 352) );
			_AddItemKeyValueTag = Marshal.GetDelegateForFunctionPointer<FAddItemKeyValueTag>( Marshal.ReadIntPtr( VTable, 360) );
			_AddItemPreviewFile = Marshal.GetDelegateForFunctionPointer<FAddItemPreviewFile>( Marshal.ReadIntPtr( VTable, 368) );
			_AddItemPreviewVideo = Marshal.GetDelegateForFunctionPointer<FAddItemPreviewVideo>( Marshal.ReadIntPtr( VTable, 376) );
			_UpdateItemPreviewFile = Marshal.GetDelegateForFunctionPointer<FUpdateItemPreviewFile>( Marshal.ReadIntPtr( VTable, 384) );
			_UpdateItemPreviewVideo = Marshal.GetDelegateForFunctionPointer<FUpdateItemPreviewVideo>( Marshal.ReadIntPtr( VTable, 392) );
			_RemoveItemPreview = Marshal.GetDelegateForFunctionPointer<FRemoveItemPreview>( Marshal.ReadIntPtr( VTable, 400) );
			_SubmitItemUpdate = Marshal.GetDelegateForFunctionPointer<FSubmitItemUpdate>( Marshal.ReadIntPtr( VTable, 408) );
			_GetItemUpdateProgress = Marshal.GetDelegateForFunctionPointer<FGetItemUpdateProgress>( Marshal.ReadIntPtr( VTable, 416) );
			_SetUserItemVote = Marshal.GetDelegateForFunctionPointer<FSetUserItemVote>( Marshal.ReadIntPtr( VTable, 424) );
			_GetUserItemVote = Marshal.GetDelegateForFunctionPointer<FGetUserItemVote>( Marshal.ReadIntPtr( VTable, 432) );
			_AddItemToFavorites = Marshal.GetDelegateForFunctionPointer<FAddItemToFavorites>( Marshal.ReadIntPtr( VTable, 440) );
			_RemoveItemFromFavorites = Marshal.GetDelegateForFunctionPointer<FRemoveItemFromFavorites>( Marshal.ReadIntPtr( VTable, 448) );
			_SubscribeItem = Marshal.GetDelegateForFunctionPointer<FSubscribeItem>( Marshal.ReadIntPtr( VTable, 456) );
			_UnsubscribeItem = Marshal.GetDelegateForFunctionPointer<FUnsubscribeItem>( Marshal.ReadIntPtr( VTable, 464) );
			_GetNumSubscribedItems = Marshal.GetDelegateForFunctionPointer<FGetNumSubscribedItems>( Marshal.ReadIntPtr( VTable, 472) );
			_GetSubscribedItems = Marshal.GetDelegateForFunctionPointer<FGetSubscribedItems>( Marshal.ReadIntPtr( VTable, 480) );
			_GetItemState = Marshal.GetDelegateForFunctionPointer<FGetItemState>( Marshal.ReadIntPtr( VTable, 488) );
			_GetItemInstallInfo = Marshal.GetDelegateForFunctionPointer<FGetItemInstallInfo>( Marshal.ReadIntPtr( VTable, 496) );
			_GetItemDownloadInfo = Marshal.GetDelegateForFunctionPointer<FGetItemDownloadInfo>( Marshal.ReadIntPtr( VTable, 504) );
			_DownloadItem = Marshal.GetDelegateForFunctionPointer<FDownloadItem>( Marshal.ReadIntPtr( VTable, 512) );
			_BInitWorkshopForGameServer = Marshal.GetDelegateForFunctionPointer<FBInitWorkshopForGameServer>( Marshal.ReadIntPtr( VTable, 520) );
			_SuspendDownloads = Marshal.GetDelegateForFunctionPointer<FSuspendDownloads>( Marshal.ReadIntPtr( VTable, 528) );
			_StartPlaytimeTracking = Marshal.GetDelegateForFunctionPointer<FStartPlaytimeTracking>( Marshal.ReadIntPtr( VTable, 536) );
			_StopPlaytimeTracking = Marshal.GetDelegateForFunctionPointer<FStopPlaytimeTracking>( Marshal.ReadIntPtr( VTable, 544) );
			_StopPlaytimeTrackingForAllItems = Marshal.GetDelegateForFunctionPointer<FStopPlaytimeTrackingForAllItems>( Marshal.ReadIntPtr( VTable, 552) );
			_AddDependency = Marshal.GetDelegateForFunctionPointer<FAddDependency>( Marshal.ReadIntPtr( VTable, 560) );
			_RemoveDependency = Marshal.GetDelegateForFunctionPointer<FRemoveDependency>( Marshal.ReadIntPtr( VTable, 568) );
			_AddAppDependency = Marshal.GetDelegateForFunctionPointer<FAddAppDependency>( Marshal.ReadIntPtr( VTable, 576) );
			_RemoveAppDependency = Marshal.GetDelegateForFunctionPointer<FRemoveAppDependency>( Marshal.ReadIntPtr( VTable, 584) );
			_GetAppDependencies = Marshal.GetDelegateForFunctionPointer<FGetAppDependencies>( Marshal.ReadIntPtr( VTable, 592) );
			_DeleteItem = Marshal.GetDelegateForFunctionPointer<FDeleteItem>( Marshal.ReadIntPtr( VTable, 600) );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate UGCQueryHandle_t FCreateQueryUserUGCRequest( IntPtr self, AccountID_t unAccountID, UserUGCList eListType, UGCMatchingUGCType eMatchingUGCType, UserUGCListSortOrder eSortOrder, AppId_t nCreatorAppID, AppId_t nConsumerAppID, uint unPage );
		private FCreateQueryUserUGCRequest _CreateQueryUserUGCRequest;
		
		#endregion
		internal UGCQueryHandle_t CreateQueryUserUGCRequest( AccountID_t unAccountID, UserUGCList eListType, UGCMatchingUGCType eMatchingUGCType, UserUGCListSortOrder eSortOrder, AppId_t nCreatorAppID, AppId_t nConsumerAppID, uint unPage )
		{
			return _CreateQueryUserUGCRequest( Self, unAccountID, eListType, eMatchingUGCType, eSortOrder, nCreatorAppID, nConsumerAppID, unPage );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate UGCQueryHandle_t FCreateQueryAllUGCRequest1( IntPtr self, UGCQuery eQueryType, UGCMatchingUGCType eMatchingeMatchingUGCTypeFileType, AppId_t nCreatorAppID, AppId_t nConsumerAppID, uint unPage );
		private FCreateQueryAllUGCRequest1 _CreateQueryAllUGCRequest1;
		
		#endregion
		internal UGCQueryHandle_t CreateQueryAllUGCRequest1( UGCQuery eQueryType, UGCMatchingUGCType eMatchingeMatchingUGCTypeFileType, AppId_t nCreatorAppID, AppId_t nConsumerAppID, uint unPage )
		{
			return _CreateQueryAllUGCRequest1( Self, eQueryType, eMatchingeMatchingUGCTypeFileType, nCreatorAppID, nConsumerAppID, unPage );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate UGCQueryHandle_t FCreateQueryAllUGCRequest2( IntPtr self, UGCQuery eQueryType, UGCMatchingUGCType eMatchingeMatchingUGCTypeFileType, AppId_t nCreatorAppID, AppId_t nConsumerAppID, string pchCursor );
		private FCreateQueryAllUGCRequest2 _CreateQueryAllUGCRequest2;
		
		#endregion
		internal UGCQueryHandle_t CreateQueryAllUGCRequest2( UGCQuery eQueryType, UGCMatchingUGCType eMatchingeMatchingUGCTypeFileType, AppId_t nCreatorAppID, AppId_t nConsumerAppID, string pchCursor )
		{
			return _CreateQueryAllUGCRequest2( Self, eQueryType, eMatchingeMatchingUGCTypeFileType, nCreatorAppID, nConsumerAppID, pchCursor );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate UGCQueryHandle_t FCreateQueryUGCDetailsRequest( IntPtr self, [In,Out] PublishedFileId[]  pvecPublishedFileID, uint unNumPublishedFileIDs );
		private FCreateQueryUGCDetailsRequest _CreateQueryUGCDetailsRequest;
		
		#endregion
		internal UGCQueryHandle_t CreateQueryUGCDetailsRequest( [In,Out] PublishedFileId[]  pvecPublishedFileID, uint unNumPublishedFileIDs )
		{
			return _CreateQueryUGCDetailsRequest( Self, pvecPublishedFileID, unNumPublishedFileIDs );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate SteamAPICall_t FSendQueryUGCRequest( IntPtr self, UGCQueryHandle_t handle );
		private FSendQueryUGCRequest _SendQueryUGCRequest;
		
		#endregion
		internal async Task<SteamUGCQueryCompleted_t?> SendQueryUGCRequest( UGCQueryHandle_t handle )
		{
			return await (new Result<SteamUGCQueryCompleted_t>( _SendQueryUGCRequest( Self, handle ) )).GetResult();
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FGetQueryUGCResult( IntPtr self, UGCQueryHandle_t handle, uint index, ref SteamUGCDetails_t pDetails );
		private FGetQueryUGCResult _GetQueryUGCResult;
		
		#endregion
		internal bool GetQueryUGCResult( UGCQueryHandle_t handle, uint index, ref SteamUGCDetails_t pDetails )
		{
			return _GetQueryUGCResult( Self, handle, index, ref pDetails );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FGetQueryUGCPreviewURL( IntPtr self, UGCQueryHandle_t handle, uint index, StringBuilder pchURL, uint cchURLSize );
		private FGetQueryUGCPreviewURL _GetQueryUGCPreviewURL;
		
		#endregion
		internal bool GetQueryUGCPreviewURL( UGCQueryHandle_t handle, uint index, StringBuilder pchURL, uint cchURLSize )
		{
			return _GetQueryUGCPreviewURL( Self, handle, index, pchURL, cchURLSize );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FGetQueryUGCMetadata( IntPtr self, UGCQueryHandle_t handle, uint index, StringBuilder pchMetadata, uint cchMetadatasize );
		private FGetQueryUGCMetadata _GetQueryUGCMetadata;
		
		#endregion
		internal bool GetQueryUGCMetadata( UGCQueryHandle_t handle, uint index, StringBuilder pchMetadata, uint cchMetadatasize )
		{
			return _GetQueryUGCMetadata( Self, handle, index, pchMetadata, cchMetadatasize );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FGetQueryUGCChildren( IntPtr self, UGCQueryHandle_t handle, uint index, [In,Out] PublishedFileId[]  pvecPublishedFileID, uint cMaxEntries );
		private FGetQueryUGCChildren _GetQueryUGCChildren;
		
		#endregion
		internal bool GetQueryUGCChildren( UGCQueryHandle_t handle, uint index, [In,Out] PublishedFileId[]  pvecPublishedFileID, uint cMaxEntries )
		{
			return _GetQueryUGCChildren( Self, handle, index, pvecPublishedFileID, cMaxEntries );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FGetQueryUGCStatistic( IntPtr self, UGCQueryHandle_t handle, uint index, ItemStatistic eStatType, ref ulong pStatValue );
		private FGetQueryUGCStatistic _GetQueryUGCStatistic;
		
		#endregion
		internal bool GetQueryUGCStatistic( UGCQueryHandle_t handle, uint index, ItemStatistic eStatType, ref ulong pStatValue )
		{
			return _GetQueryUGCStatistic( Self, handle, index, eStatType, ref pStatValue );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate uint FGetQueryUGCNumAdditionalPreviews( IntPtr self, UGCQueryHandle_t handle, uint index );
		private FGetQueryUGCNumAdditionalPreviews _GetQueryUGCNumAdditionalPreviews;
		
		#endregion
		internal uint GetQueryUGCNumAdditionalPreviews( UGCQueryHandle_t handle, uint index )
		{
			return _GetQueryUGCNumAdditionalPreviews( Self, handle, index );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FGetQueryUGCAdditionalPreview( IntPtr self, UGCQueryHandle_t handle, uint index, uint previewIndex, StringBuilder pchURLOrVideoID, uint cchURLSize, StringBuilder pchOriginalFileName, uint cchOriginalFileNameSize, ref ItemPreviewType pPreviewType );
		private FGetQueryUGCAdditionalPreview _GetQueryUGCAdditionalPreview;
		
		#endregion
		internal bool GetQueryUGCAdditionalPreview( UGCQueryHandle_t handle, uint index, uint previewIndex, StringBuilder pchURLOrVideoID, uint cchURLSize, StringBuilder pchOriginalFileName, uint cchOriginalFileNameSize, ref ItemPreviewType pPreviewType )
		{
			return _GetQueryUGCAdditionalPreview( Self, handle, index, previewIndex, pchURLOrVideoID, cchURLSize, pchOriginalFileName, cchOriginalFileNameSize, ref pPreviewType );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate uint FGetQueryUGCNumKeyValueTags( IntPtr self, UGCQueryHandle_t handle, uint index );
		private FGetQueryUGCNumKeyValueTags _GetQueryUGCNumKeyValueTags;
		
		#endregion
		internal uint GetQueryUGCNumKeyValueTags( UGCQueryHandle_t handle, uint index )
		{
			return _GetQueryUGCNumKeyValueTags( Self, handle, index );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FGetQueryUGCKeyValueTag( IntPtr self, UGCQueryHandle_t handle, uint index, uint keyValueTagIndex, StringBuilder pchKey, uint cchKeySize, StringBuilder pchValue, uint cchValueSize );
		private FGetQueryUGCKeyValueTag _GetQueryUGCKeyValueTag;
		
		#endregion
		internal bool GetQueryUGCKeyValueTag( UGCQueryHandle_t handle, uint index, uint keyValueTagIndex, StringBuilder pchKey, uint cchKeySize, StringBuilder pchValue, uint cchValueSize )
		{
			return _GetQueryUGCKeyValueTag( Self, handle, index, keyValueTagIndex, pchKey, cchKeySize, pchValue, cchValueSize );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FReleaseQueryUGCRequest( IntPtr self, UGCQueryHandle_t handle );
		private FReleaseQueryUGCRequest _ReleaseQueryUGCRequest;
		
		#endregion
		internal bool ReleaseQueryUGCRequest( UGCQueryHandle_t handle )
		{
			return _ReleaseQueryUGCRequest( Self, handle );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FAddRequiredTag( IntPtr self, UGCQueryHandle_t handle, string pTagName );
		private FAddRequiredTag _AddRequiredTag;
		
		#endregion
		internal bool AddRequiredTag( UGCQueryHandle_t handle, string pTagName )
		{
			return _AddRequiredTag( Self, handle, pTagName );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FAddExcludedTag( IntPtr self, UGCQueryHandle_t handle, string pTagName );
		private FAddExcludedTag _AddExcludedTag;
		
		#endregion
		internal bool AddExcludedTag( UGCQueryHandle_t handle, string pTagName )
		{
			return _AddExcludedTag( Self, handle, pTagName );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FSetReturnOnlyIDs( IntPtr self, UGCQueryHandle_t handle, [MarshalAs( UnmanagedType.U1 )] bool bReturnOnlyIDs );
		private FSetReturnOnlyIDs _SetReturnOnlyIDs;
		
		#endregion
		internal bool SetReturnOnlyIDs( UGCQueryHandle_t handle, [MarshalAs( UnmanagedType.U1 )] bool bReturnOnlyIDs )
		{
			return _SetReturnOnlyIDs( Self, handle, bReturnOnlyIDs );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FSetReturnKeyValueTags( IntPtr self, UGCQueryHandle_t handle, [MarshalAs( UnmanagedType.U1 )] bool bReturnKeyValueTags );
		private FSetReturnKeyValueTags _SetReturnKeyValueTags;
		
		#endregion
		internal bool SetReturnKeyValueTags( UGCQueryHandle_t handle, [MarshalAs( UnmanagedType.U1 )] bool bReturnKeyValueTags )
		{
			return _SetReturnKeyValueTags( Self, handle, bReturnKeyValueTags );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FSetReturnLongDescription( IntPtr self, UGCQueryHandle_t handle, [MarshalAs( UnmanagedType.U1 )] bool bReturnLongDescription );
		private FSetReturnLongDescription _SetReturnLongDescription;
		
		#endregion
		internal bool SetReturnLongDescription( UGCQueryHandle_t handle, [MarshalAs( UnmanagedType.U1 )] bool bReturnLongDescription )
		{
			return _SetReturnLongDescription( Self, handle, bReturnLongDescription );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FSetReturnMetadata( IntPtr self, UGCQueryHandle_t handle, [MarshalAs( UnmanagedType.U1 )] bool bReturnMetadata );
		private FSetReturnMetadata _SetReturnMetadata;
		
		#endregion
		internal bool SetReturnMetadata( UGCQueryHandle_t handle, [MarshalAs( UnmanagedType.U1 )] bool bReturnMetadata )
		{
			return _SetReturnMetadata( Self, handle, bReturnMetadata );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FSetReturnChildren( IntPtr self, UGCQueryHandle_t handle, [MarshalAs( UnmanagedType.U1 )] bool bReturnChildren );
		private FSetReturnChildren _SetReturnChildren;
		
		#endregion
		internal bool SetReturnChildren( UGCQueryHandle_t handle, [MarshalAs( UnmanagedType.U1 )] bool bReturnChildren )
		{
			return _SetReturnChildren( Self, handle, bReturnChildren );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FSetReturnAdditionalPreviews( IntPtr self, UGCQueryHandle_t handle, [MarshalAs( UnmanagedType.U1 )] bool bReturnAdditionalPreviews );
		private FSetReturnAdditionalPreviews _SetReturnAdditionalPreviews;
		
		#endregion
		internal bool SetReturnAdditionalPreviews( UGCQueryHandle_t handle, [MarshalAs( UnmanagedType.U1 )] bool bReturnAdditionalPreviews )
		{
			return _SetReturnAdditionalPreviews( Self, handle, bReturnAdditionalPreviews );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FSetReturnTotalOnly( IntPtr self, UGCQueryHandle_t handle, [MarshalAs( UnmanagedType.U1 )] bool bReturnTotalOnly );
		private FSetReturnTotalOnly _SetReturnTotalOnly;
		
		#endregion
		internal bool SetReturnTotalOnly( UGCQueryHandle_t handle, [MarshalAs( UnmanagedType.U1 )] bool bReturnTotalOnly )
		{
			return _SetReturnTotalOnly( Self, handle, bReturnTotalOnly );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FSetReturnPlaytimeStats( IntPtr self, UGCQueryHandle_t handle, uint unDays );
		private FSetReturnPlaytimeStats _SetReturnPlaytimeStats;
		
		#endregion
		internal bool SetReturnPlaytimeStats( UGCQueryHandle_t handle, uint unDays )
		{
			return _SetReturnPlaytimeStats( Self, handle, unDays );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FSetLanguage( IntPtr self, UGCQueryHandle_t handle, string pchLanguage );
		private FSetLanguage _SetLanguage;
		
		#endregion
		internal bool SetLanguage( UGCQueryHandle_t handle, string pchLanguage )
		{
			return _SetLanguage( Self, handle, pchLanguage );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FSetAllowCachedResponse( IntPtr self, UGCQueryHandle_t handle, uint unMaxAgeSeconds );
		private FSetAllowCachedResponse _SetAllowCachedResponse;
		
		#endregion
		internal bool SetAllowCachedResponse( UGCQueryHandle_t handle, uint unMaxAgeSeconds )
		{
			return _SetAllowCachedResponse( Self, handle, unMaxAgeSeconds );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FSetCloudFileNameFilter( IntPtr self, UGCQueryHandle_t handle, string pMatchCloudFileName );
		private FSetCloudFileNameFilter _SetCloudFileNameFilter;
		
		#endregion
		internal bool SetCloudFileNameFilter( UGCQueryHandle_t handle, string pMatchCloudFileName )
		{
			return _SetCloudFileNameFilter( Self, handle, pMatchCloudFileName );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FSetMatchAnyTag( IntPtr self, UGCQueryHandle_t handle, [MarshalAs( UnmanagedType.U1 )] bool bMatchAnyTag );
		private FSetMatchAnyTag _SetMatchAnyTag;
		
		#endregion
		internal bool SetMatchAnyTag( UGCQueryHandle_t handle, [MarshalAs( UnmanagedType.U1 )] bool bMatchAnyTag )
		{
			return _SetMatchAnyTag( Self, handle, bMatchAnyTag );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FSetSearchText( IntPtr self, UGCQueryHandle_t handle, string pSearchText );
		private FSetSearchText _SetSearchText;
		
		#endregion
		internal bool SetSearchText( UGCQueryHandle_t handle, string pSearchText )
		{
			return _SetSearchText( Self, handle, pSearchText );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FSetRankedByTrendDays( IntPtr self, UGCQueryHandle_t handle, uint unDays );
		private FSetRankedByTrendDays _SetRankedByTrendDays;
		
		#endregion
		internal bool SetRankedByTrendDays( UGCQueryHandle_t handle, uint unDays )
		{
			return _SetRankedByTrendDays( Self, handle, unDays );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FAddRequiredKeyValueTag( IntPtr self, UGCQueryHandle_t handle, string pKey, string pValue );
		private FAddRequiredKeyValueTag _AddRequiredKeyValueTag;
		
		#endregion
		internal bool AddRequiredKeyValueTag( UGCQueryHandle_t handle, string pKey, string pValue )
		{
			return _AddRequiredKeyValueTag( Self, handle, pKey, pValue );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate SteamAPICall_t FRequestUGCDetails( IntPtr self, PublishedFileId nPublishedFileID, uint unMaxAgeSeconds );
		private FRequestUGCDetails _RequestUGCDetails;
		
		#endregion
		internal async Task<SteamUGCRequestUGCDetailsResult_t?> RequestUGCDetails( PublishedFileId nPublishedFileID, uint unMaxAgeSeconds )
		{
			return await (new Result<SteamUGCRequestUGCDetailsResult_t>( _RequestUGCDetails( Self, nPublishedFileID, unMaxAgeSeconds ) )).GetResult();
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate SteamAPICall_t FCreateItem( IntPtr self, AppId_t nConsumerAppId, WorkshopFileType eFileType );
		private FCreateItem _CreateItem;
		
		#endregion
		internal async Task<CreateItemResult_t?> CreateItem( AppId_t nConsumerAppId, WorkshopFileType eFileType )
		{
			return await (new Result<CreateItemResult_t>( _CreateItem( Self, nConsumerAppId, eFileType ) )).GetResult();
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate UGCUpdateHandle_t FStartItemUpdate( IntPtr self, AppId_t nConsumerAppId, PublishedFileId nPublishedFileID );
		private FStartItemUpdate _StartItemUpdate;
		
		#endregion
		internal UGCUpdateHandle_t StartItemUpdate( AppId_t nConsumerAppId, PublishedFileId nPublishedFileID )
		{
			return _StartItemUpdate( Self, nConsumerAppId, nPublishedFileID );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FSetItemTitle( IntPtr self, UGCUpdateHandle_t handle, string pchTitle );
		private FSetItemTitle _SetItemTitle;
		
		#endregion
		internal bool SetItemTitle( UGCUpdateHandle_t handle, string pchTitle )
		{
			return _SetItemTitle( Self, handle, pchTitle );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FSetItemDescription( IntPtr self, UGCUpdateHandle_t handle, string pchDescription );
		private FSetItemDescription _SetItemDescription;
		
		#endregion
		internal bool SetItemDescription( UGCUpdateHandle_t handle, string pchDescription )
		{
			return _SetItemDescription( Self, handle, pchDescription );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FSetItemUpdateLanguage( IntPtr self, UGCUpdateHandle_t handle, string pchLanguage );
		private FSetItemUpdateLanguage _SetItemUpdateLanguage;
		
		#endregion
		internal bool SetItemUpdateLanguage( UGCUpdateHandle_t handle, string pchLanguage )
		{
			return _SetItemUpdateLanguage( Self, handle, pchLanguage );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FSetItemMetadata( IntPtr self, UGCUpdateHandle_t handle, string pchMetaData );
		private FSetItemMetadata _SetItemMetadata;
		
		#endregion
		internal bool SetItemMetadata( UGCUpdateHandle_t handle, string pchMetaData )
		{
			return _SetItemMetadata( Self, handle, pchMetaData );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FSetItemVisibility( IntPtr self, UGCUpdateHandle_t handle, RemoteStoragePublishedFileVisibility eVisibility );
		private FSetItemVisibility _SetItemVisibility;
		
		#endregion
		internal bool SetItemVisibility( UGCUpdateHandle_t handle, RemoteStoragePublishedFileVisibility eVisibility )
		{
			return _SetItemVisibility( Self, handle, eVisibility );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FSetItemTags( IntPtr self, UGCUpdateHandle_t updateHandle, ref SteamParamStringArray_t pTags );
		private FSetItemTags _SetItemTags;
		
		#endregion
		internal bool SetItemTags( UGCUpdateHandle_t updateHandle, ref SteamParamStringArray_t pTags )
		{
			return _SetItemTags( Self, updateHandle, ref pTags );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FSetItemContent( IntPtr self, UGCUpdateHandle_t handle, string pszContentFolder );
		private FSetItemContent _SetItemContent;
		
		#endregion
		internal bool SetItemContent( UGCUpdateHandle_t handle, string pszContentFolder )
		{
			return _SetItemContent( Self, handle, pszContentFolder );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FSetItemPreview( IntPtr self, UGCUpdateHandle_t handle, string pszPreviewFile );
		private FSetItemPreview _SetItemPreview;
		
		#endregion
		internal bool SetItemPreview( UGCUpdateHandle_t handle, string pszPreviewFile )
		{
			return _SetItemPreview( Self, handle, pszPreviewFile );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FSetAllowLegacyUpload( IntPtr self, UGCUpdateHandle_t handle, [MarshalAs( UnmanagedType.U1 )] bool bAllowLegacyUpload );
		private FSetAllowLegacyUpload _SetAllowLegacyUpload;
		
		#endregion
		internal bool SetAllowLegacyUpload( UGCUpdateHandle_t handle, [MarshalAs( UnmanagedType.U1 )] bool bAllowLegacyUpload )
		{
			return _SetAllowLegacyUpload( Self, handle, bAllowLegacyUpload );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FRemoveItemKeyValueTags( IntPtr self, UGCUpdateHandle_t handle, string pchKey );
		private FRemoveItemKeyValueTags _RemoveItemKeyValueTags;
		
		#endregion
		internal bool RemoveItemKeyValueTags( UGCUpdateHandle_t handle, string pchKey )
		{
			return _RemoveItemKeyValueTags( Self, handle, pchKey );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FAddItemKeyValueTag( IntPtr self, UGCUpdateHandle_t handle, string pchKey, string pchValue );
		private FAddItemKeyValueTag _AddItemKeyValueTag;
		
		#endregion
		internal bool AddItemKeyValueTag( UGCUpdateHandle_t handle, string pchKey, string pchValue )
		{
			return _AddItemKeyValueTag( Self, handle, pchKey, pchValue );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FAddItemPreviewFile( IntPtr self, UGCUpdateHandle_t handle, string pszPreviewFile, ItemPreviewType type );
		private FAddItemPreviewFile _AddItemPreviewFile;
		
		#endregion
		internal bool AddItemPreviewFile( UGCUpdateHandle_t handle, string pszPreviewFile, ItemPreviewType type )
		{
			return _AddItemPreviewFile( Self, handle, pszPreviewFile, type );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FAddItemPreviewVideo( IntPtr self, UGCUpdateHandle_t handle, string pszVideoID );
		private FAddItemPreviewVideo _AddItemPreviewVideo;
		
		#endregion
		internal bool AddItemPreviewVideo( UGCUpdateHandle_t handle, string pszVideoID )
		{
			return _AddItemPreviewVideo( Self, handle, pszVideoID );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FUpdateItemPreviewFile( IntPtr self, UGCUpdateHandle_t handle, uint index, string pszPreviewFile );
		private FUpdateItemPreviewFile _UpdateItemPreviewFile;
		
		#endregion
		internal bool UpdateItemPreviewFile( UGCUpdateHandle_t handle, uint index, string pszPreviewFile )
		{
			return _UpdateItemPreviewFile( Self, handle, index, pszPreviewFile );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FUpdateItemPreviewVideo( IntPtr self, UGCUpdateHandle_t handle, uint index, string pszVideoID );
		private FUpdateItemPreviewVideo _UpdateItemPreviewVideo;
		
		#endregion
		internal bool UpdateItemPreviewVideo( UGCUpdateHandle_t handle, uint index, string pszVideoID )
		{
			return _UpdateItemPreviewVideo( Self, handle, index, pszVideoID );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FRemoveItemPreview( IntPtr self, UGCUpdateHandle_t handle, uint index );
		private FRemoveItemPreview _RemoveItemPreview;
		
		#endregion
		internal bool RemoveItemPreview( UGCUpdateHandle_t handle, uint index )
		{
			return _RemoveItemPreview( Self, handle, index );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate SteamAPICall_t FSubmitItemUpdate( IntPtr self, UGCUpdateHandle_t handle, string pchChangeNote );
		private FSubmitItemUpdate _SubmitItemUpdate;
		
		#endregion
		internal async Task<SubmitItemUpdateResult_t?> SubmitItemUpdate( UGCUpdateHandle_t handle, string pchChangeNote )
		{
			return await (new Result<SubmitItemUpdateResult_t>( _SubmitItemUpdate( Self, handle, pchChangeNote ) )).GetResult();
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate ItemUpdateStatus FGetItemUpdateProgress( IntPtr self, UGCUpdateHandle_t handle, ref ulong punBytesProcessed, ref ulong punBytesTotal );
		private FGetItemUpdateProgress _GetItemUpdateProgress;
		
		#endregion
		internal ItemUpdateStatus GetItemUpdateProgress( UGCUpdateHandle_t handle, ref ulong punBytesProcessed, ref ulong punBytesTotal )
		{
			return _GetItemUpdateProgress( Self, handle, ref punBytesProcessed, ref punBytesTotal );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate SteamAPICall_t FSetUserItemVote( IntPtr self, PublishedFileId nPublishedFileID, [MarshalAs( UnmanagedType.U1 )] bool bVoteUp );
		private FSetUserItemVote _SetUserItemVote;
		
		#endregion
		internal async Task<SetUserItemVoteResult_t?> SetUserItemVote( PublishedFileId nPublishedFileID, [MarshalAs( UnmanagedType.U1 )] bool bVoteUp )
		{
			return await (new Result<SetUserItemVoteResult_t>( _SetUserItemVote( Self, nPublishedFileID, bVoteUp ) )).GetResult();
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate SteamAPICall_t FGetUserItemVote( IntPtr self, PublishedFileId nPublishedFileID );
		private FGetUserItemVote _GetUserItemVote;
		
		#endregion
		internal async Task<GetUserItemVoteResult_t?> GetUserItemVote( PublishedFileId nPublishedFileID )
		{
			return await (new Result<GetUserItemVoteResult_t>( _GetUserItemVote( Self, nPublishedFileID ) )).GetResult();
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate SteamAPICall_t FAddItemToFavorites( IntPtr self, AppId_t nAppId, PublishedFileId nPublishedFileID );
		private FAddItemToFavorites _AddItemToFavorites;
		
		#endregion
		internal async Task<UserFavoriteItemsListChanged_t?> AddItemToFavorites( AppId_t nAppId, PublishedFileId nPublishedFileID )
		{
			return await (new Result<UserFavoriteItemsListChanged_t>( _AddItemToFavorites( Self, nAppId, nPublishedFileID ) )).GetResult();
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate SteamAPICall_t FRemoveItemFromFavorites( IntPtr self, AppId_t nAppId, PublishedFileId nPublishedFileID );
		private FRemoveItemFromFavorites _RemoveItemFromFavorites;
		
		#endregion
		internal async Task<UserFavoriteItemsListChanged_t?> RemoveItemFromFavorites( AppId_t nAppId, PublishedFileId nPublishedFileID )
		{
			return await (new Result<UserFavoriteItemsListChanged_t>( _RemoveItemFromFavorites( Self, nAppId, nPublishedFileID ) )).GetResult();
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate SteamAPICall_t FSubscribeItem( IntPtr self, PublishedFileId nPublishedFileID );
		private FSubscribeItem _SubscribeItem;
		
		#endregion
		internal async Task<RemoteStorageSubscribePublishedFileResult_t?> SubscribeItem( PublishedFileId nPublishedFileID )
		{
			return await (new Result<RemoteStorageSubscribePublishedFileResult_t>( _SubscribeItem( Self, nPublishedFileID ) )).GetResult();
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate SteamAPICall_t FUnsubscribeItem( IntPtr self, PublishedFileId nPublishedFileID );
		private FUnsubscribeItem _UnsubscribeItem;
		
		#endregion
		internal async Task<RemoteStorageUnsubscribePublishedFileResult_t?> UnsubscribeItem( PublishedFileId nPublishedFileID )
		{
			return await (new Result<RemoteStorageUnsubscribePublishedFileResult_t>( _UnsubscribeItem( Self, nPublishedFileID ) )).GetResult();
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate uint FGetNumSubscribedItems( IntPtr self );
		private FGetNumSubscribedItems _GetNumSubscribedItems;
		
		#endregion
		internal uint GetNumSubscribedItems()
		{
			return _GetNumSubscribedItems( Self );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate uint FGetSubscribedItems( IntPtr self, [In,Out] PublishedFileId[]  pvecPublishedFileID, uint cMaxEntries );
		private FGetSubscribedItems _GetSubscribedItems;
		
		#endregion
		internal uint GetSubscribedItems( [In,Out] PublishedFileId[]  pvecPublishedFileID, uint cMaxEntries )
		{
			return _GetSubscribedItems( Self, pvecPublishedFileID, cMaxEntries );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate uint FGetItemState( IntPtr self, PublishedFileId nPublishedFileID );
		private FGetItemState _GetItemState;
		
		#endregion
		internal uint GetItemState( PublishedFileId nPublishedFileID )
		{
			return _GetItemState( Self, nPublishedFileID );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FGetItemInstallInfo( IntPtr self, PublishedFileId nPublishedFileID, ref ulong punSizeOnDisk, StringBuilder pchFolder, uint cchFolderSize, ref uint punTimeStamp );
		private FGetItemInstallInfo _GetItemInstallInfo;
		
		#endregion
		internal bool GetItemInstallInfo( PublishedFileId nPublishedFileID, ref ulong punSizeOnDisk, StringBuilder pchFolder, uint cchFolderSize, ref uint punTimeStamp )
		{
			return _GetItemInstallInfo( Self, nPublishedFileID, ref punSizeOnDisk, pchFolder, cchFolderSize, ref punTimeStamp );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FGetItemDownloadInfo( IntPtr self, PublishedFileId nPublishedFileID, ref ulong punBytesDownloaded, ref ulong punBytesTotal );
		private FGetItemDownloadInfo _GetItemDownloadInfo;
		
		#endregion
		internal bool GetItemDownloadInfo( PublishedFileId nPublishedFileID, ref ulong punBytesDownloaded, ref ulong punBytesTotal )
		{
			return _GetItemDownloadInfo( Self, nPublishedFileID, ref punBytesDownloaded, ref punBytesTotal );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FDownloadItem( IntPtr self, PublishedFileId nPublishedFileID, [MarshalAs( UnmanagedType.U1 )] bool bHighPriority );
		private FDownloadItem _DownloadItem;
		
		#endregion
		internal bool DownloadItem( PublishedFileId nPublishedFileID, [MarshalAs( UnmanagedType.U1 )] bool bHighPriority )
		{
			return _DownloadItem( Self, nPublishedFileID, bHighPriority );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FBInitWorkshopForGameServer( IntPtr self, DepotId_t unWorkshopDepotID, string pszFolder );
		private FBInitWorkshopForGameServer _BInitWorkshopForGameServer;
		
		#endregion
		internal bool BInitWorkshopForGameServer( DepotId_t unWorkshopDepotID, string pszFolder )
		{
			return _BInitWorkshopForGameServer( Self, unWorkshopDepotID, pszFolder );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate void FSuspendDownloads( IntPtr self, [MarshalAs( UnmanagedType.U1 )] bool bSuspend );
		private FSuspendDownloads _SuspendDownloads;
		
		#endregion
		internal void SuspendDownloads( [MarshalAs( UnmanagedType.U1 )] bool bSuspend )
		{
			_SuspendDownloads( Self, bSuspend );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate SteamAPICall_t FStartPlaytimeTracking( IntPtr self, [In,Out] PublishedFileId[]  pvecPublishedFileID, uint unNumPublishedFileIDs );
		private FStartPlaytimeTracking _StartPlaytimeTracking;
		
		#endregion
		internal async Task<StartPlaytimeTrackingResult_t?> StartPlaytimeTracking( [In,Out] PublishedFileId[]  pvecPublishedFileID, uint unNumPublishedFileIDs )
		{
			return await (new Result<StartPlaytimeTrackingResult_t>( _StartPlaytimeTracking( Self, pvecPublishedFileID, unNumPublishedFileIDs ) )).GetResult();
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate SteamAPICall_t FStopPlaytimeTracking( IntPtr self, [In,Out] PublishedFileId[]  pvecPublishedFileID, uint unNumPublishedFileIDs );
		private FStopPlaytimeTracking _StopPlaytimeTracking;
		
		#endregion
		internal async Task<StopPlaytimeTrackingResult_t?> StopPlaytimeTracking( [In,Out] PublishedFileId[]  pvecPublishedFileID, uint unNumPublishedFileIDs )
		{
			return await (new Result<StopPlaytimeTrackingResult_t>( _StopPlaytimeTracking( Self, pvecPublishedFileID, unNumPublishedFileIDs ) )).GetResult();
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate SteamAPICall_t FStopPlaytimeTrackingForAllItems( IntPtr self );
		private FStopPlaytimeTrackingForAllItems _StopPlaytimeTrackingForAllItems;
		
		#endregion
		internal async Task<StopPlaytimeTrackingResult_t?> StopPlaytimeTrackingForAllItems()
		{
			return await (new Result<StopPlaytimeTrackingResult_t>( _StopPlaytimeTrackingForAllItems( Self ) )).GetResult();
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate SteamAPICall_t FAddDependency( IntPtr self, PublishedFileId nParentPublishedFileID, PublishedFileId nChildPublishedFileID );
		private FAddDependency _AddDependency;
		
		#endregion
		internal async Task<AddUGCDependencyResult_t?> AddDependency( PublishedFileId nParentPublishedFileID, PublishedFileId nChildPublishedFileID )
		{
			return await (new Result<AddUGCDependencyResult_t>( _AddDependency( Self, nParentPublishedFileID, nChildPublishedFileID ) )).GetResult();
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate SteamAPICall_t FRemoveDependency( IntPtr self, PublishedFileId nParentPublishedFileID, PublishedFileId nChildPublishedFileID );
		private FRemoveDependency _RemoveDependency;
		
		#endregion
		internal async Task<RemoveUGCDependencyResult_t?> RemoveDependency( PublishedFileId nParentPublishedFileID, PublishedFileId nChildPublishedFileID )
		{
			return await (new Result<RemoveUGCDependencyResult_t>( _RemoveDependency( Self, nParentPublishedFileID, nChildPublishedFileID ) )).GetResult();
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate SteamAPICall_t FAddAppDependency( IntPtr self, PublishedFileId nPublishedFileID, AppId_t nAppID );
		private FAddAppDependency _AddAppDependency;
		
		#endregion
		internal async Task<AddAppDependencyResult_t?> AddAppDependency( PublishedFileId nPublishedFileID, AppId_t nAppID )
		{
			return await (new Result<AddAppDependencyResult_t>( _AddAppDependency( Self, nPublishedFileID, nAppID ) )).GetResult();
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate SteamAPICall_t FRemoveAppDependency( IntPtr self, PublishedFileId nPublishedFileID, AppId_t nAppID );
		private FRemoveAppDependency _RemoveAppDependency;
		
		#endregion
		internal async Task<RemoveAppDependencyResult_t?> RemoveAppDependency( PublishedFileId nPublishedFileID, AppId_t nAppID )
		{
			return await (new Result<RemoveAppDependencyResult_t>( _RemoveAppDependency( Self, nPublishedFileID, nAppID ) )).GetResult();
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate SteamAPICall_t FGetAppDependencies( IntPtr self, PublishedFileId nPublishedFileID );
		private FGetAppDependencies _GetAppDependencies;
		
		#endregion
		internal async Task<GetAppDependenciesResult_t?> GetAppDependencies( PublishedFileId nPublishedFileID )
		{
			return await (new Result<GetAppDependenciesResult_t>( _GetAppDependencies( Self, nPublishedFileID ) )).GetResult();
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate SteamAPICall_t FDeleteItem( IntPtr self, PublishedFileId nPublishedFileID );
		private FDeleteItem _DeleteItem;
		
		#endregion
		internal async Task<DeleteItemResult_t?> DeleteItem( PublishedFileId nPublishedFileID )
		{
			return await (new Result<DeleteItemResult_t>( _DeleteItem( Self, nPublishedFileID ) )).GetResult();
		}
		
	}
}
