using System;
using System.Runtime.InteropServices;

namespace SteamNative
{
	public unsafe class SteamUGC
	{
		internal Platform.Interface _pi;
		
		public SteamUGC( IntPtr pointer )
		{
			if ( Platform.IsWindows64 ) _pi = new Platform.Win64( pointer );
			else if ( Platform.IsWindows32 ) _pi = new Platform.Win32( pointer );
			else if ( Platform.IsLinux32 ) _pi = new Platform.Linux32( pointer );
			else if ( Platform.IsLinux64 ) _pi = new Platform.Linux64( pointer );
			else if ( Platform.IsOsx ) _pi = new Platform.Mac( pointer );
		}
		
		public bool IsValid{ get{ return _pi != null && _pi.IsValid; } }
		
		// bool
		public bool AddExcludedTag( UGCQueryHandle_t handle /*UGCQueryHandle_t*/, string pTagName /*const char **/ )
		{
			return _pi.ISteamUGC_AddExcludedTag( handle, pTagName );
		}
		
		// bool
		public bool AddItemKeyValueTag( UGCUpdateHandle_t handle /*UGCUpdateHandle_t*/, string pchKey /*const char **/, string pchValue /*const char **/ )
		{
			return _pi.ISteamUGC_AddItemKeyValueTag( handle, pchKey, pchValue );
		}
		
		// bool
		public bool AddItemPreviewFile( UGCUpdateHandle_t handle /*UGCUpdateHandle_t*/, string pszPreviewFile /*const char **/, ItemPreviewType type /*EItemPreviewType*/ )
		{
			return _pi.ISteamUGC_AddItemPreviewFile( handle, pszPreviewFile, type );
		}
		
		// bool
		public bool AddItemPreviewVideo( UGCUpdateHandle_t handle /*UGCUpdateHandle_t*/, string pszVideoID /*const char **/ )
		{
			return _pi.ISteamUGC_AddItemPreviewVideo( handle, pszVideoID );
		}
		
		// SteamAPICall_t
		public SteamAPICall_t AddItemToFavorites( AppId_t nAppId /*AppId_t*/, PublishedFileId_t nPublishedFileID /*PublishedFileId_t*/ )
		{
			return _pi.ISteamUGC_AddItemToFavorites( nAppId, nPublishedFileID );
		}
		
		// bool
		public bool AddRequiredKeyValueTag( UGCQueryHandle_t handle /*UGCQueryHandle_t*/, string pKey /*const char **/, string pValue /*const char **/ )
		{
			return _pi.ISteamUGC_AddRequiredKeyValueTag( handle, pKey, pValue );
		}
		
		// bool
		public bool AddRequiredTag( UGCQueryHandle_t handle /*UGCQueryHandle_t*/, string pTagName /*const char **/ )
		{
			return _pi.ISteamUGC_AddRequiredTag( handle, pTagName );
		}
		
		// bool
		public bool BInitWorkshopForGameServer( DepotId_t unWorkshopDepotID /*DepotId_t*/, string pszFolder /*const char **/ )
		{
			return _pi.ISteamUGC_BInitWorkshopForGameServer( unWorkshopDepotID, pszFolder );
		}
		
		// SteamAPICall_t
		public SteamAPICall_t CreateItem( AppId_t nConsumerAppId /*AppId_t*/, WorkshopFileType eFileType /*EWorkshopFileType*/ )
		{
			return _pi.ISteamUGC_CreateItem( nConsumerAppId, eFileType );
		}
		
		// UGCQueryHandle_t
		public UGCQueryHandle_t CreateQueryAllUGCRequest( UGCQuery eQueryType /*EUGCQuery*/, UGCMatchingUGCType eMatchingeMatchingUGCTypeFileType /*EUGCMatchingUGCType*/, AppId_t nCreatorAppID /*AppId_t*/, AppId_t nConsumerAppID /*AppId_t*/, uint unPage /*uint32*/ )
		{
			return _pi.ISteamUGC_CreateQueryAllUGCRequest( eQueryType, eMatchingeMatchingUGCTypeFileType, nCreatorAppID, nConsumerAppID, unPage );
		}
		
		// with: Detect_VectorReturn
		// UGCQueryHandle_t
		public UGCQueryHandle_t CreateQueryUGCDetailsRequest( PublishedFileId_t[] pvecPublishedFileID /*PublishedFileId_t **/ )
		{
			var unNumPublishedFileIDs = (uint) pvecPublishedFileID.Length;
			fixed ( PublishedFileId_t* pvecPublishedFileID_ptr = pvecPublishedFileID  )
			{
				return _pi.ISteamUGC_CreateQueryUGCDetailsRequest( (IntPtr) pvecPublishedFileID_ptr, unNumPublishedFileIDs );
			}
		}
		
		// UGCQueryHandle_t
		public UGCQueryHandle_t CreateQueryUserUGCRequest( AccountID_t unAccountID /*AccountID_t*/, UserUGCList eListType /*EUserUGCList*/, UGCMatchingUGCType eMatchingUGCType /*EUGCMatchingUGCType*/, UserUGCListSortOrder eSortOrder /*EUserUGCListSortOrder*/, AppId_t nCreatorAppID /*AppId_t*/, AppId_t nConsumerAppID /*AppId_t*/, uint unPage /*uint32*/ )
		{
			return _pi.ISteamUGC_CreateQueryUserUGCRequest( unAccountID, eListType, eMatchingUGCType, eSortOrder, nCreatorAppID, nConsumerAppID, unPage );
		}
		
		// bool
		public bool DownloadItem( PublishedFileId_t nPublishedFileID /*PublishedFileId_t*/, bool bHighPriority /*bool*/ )
		{
			return _pi.ISteamUGC_DownloadItem( nPublishedFileID, bHighPriority );
		}
		
		// bool
		public bool GetItemDownloadInfo( PublishedFileId_t nPublishedFileID /*PublishedFileId_t*/, out ulong punBytesDownloaded /*uint64 **/, out ulong punBytesTotal /*uint64 **/ )
		{
			return _pi.ISteamUGC_GetItemDownloadInfo( nPublishedFileID, out punBytesDownloaded, out punBytesTotal );
		}
		
		// bool
		// with: Detect_StringFetch False
		public bool GetItemInstallInfo( PublishedFileId_t nPublishedFileID /*PublishedFileId_t*/, out ulong punSizeOnDisk /*uint64 **/, out string pchFolder /*char **/, out uint punTimeStamp /*uint32 **/ )
		{
			bool bSuccess = default( bool );
			pchFolder = string.Empty;
			System.Text.StringBuilder pchFolder_sb = new System.Text.StringBuilder( 4096 );
			uint cchFolderSize = 4096;
			bSuccess = _pi.ISteamUGC_GetItemInstallInfo( nPublishedFileID, out punSizeOnDisk, pchFolder_sb, cchFolderSize, out punTimeStamp );
			if ( !bSuccess ) return bSuccess;
			pchFolder = pchFolder_sb.ToString();
			return bSuccess;
		}
		
		// uint
		public uint GetItemState( PublishedFileId_t nPublishedFileID /*PublishedFileId_t*/ )
		{
			return _pi.ISteamUGC_GetItemState( nPublishedFileID );
		}
		
		// ItemUpdateStatus
		public ItemUpdateStatus GetItemUpdateProgress( UGCUpdateHandle_t handle /*UGCUpdateHandle_t*/, out ulong punBytesProcessed /*uint64 **/, out ulong punBytesTotal /*uint64 **/ )
		{
			return _pi.ISteamUGC_GetItemUpdateProgress( handle, out punBytesProcessed, out punBytesTotal );
		}
		
		// uint
		public uint GetNumSubscribedItems()
		{
			return _pi.ISteamUGC_GetNumSubscribedItems();
		}
		
		// bool
		// with: Detect_StringFetch False
		// with: Detect_StringFetch False
		public bool GetQueryUGCAdditionalPreview( UGCQueryHandle_t handle /*UGCQueryHandle_t*/, uint index /*uint32*/, uint previewIndex /*uint32*/, out string pchURLOrVideoID /*char **/, out string pchOriginalFileName /*char **/, out ItemPreviewType pPreviewType /*EItemPreviewType **/ )
		{
			bool bSuccess = default( bool );
			pchURLOrVideoID = string.Empty;
			System.Text.StringBuilder pchURLOrVideoID_sb = new System.Text.StringBuilder( 4096 );
			uint cchURLSize = 4096;
			pchOriginalFileName = string.Empty;
			System.Text.StringBuilder pchOriginalFileName_sb = new System.Text.StringBuilder( 4096 );
			uint cchOriginalFileNameSize = 4096;
			bSuccess = _pi.ISteamUGC_GetQueryUGCAdditionalPreview( handle, index, previewIndex, pchURLOrVideoID_sb, cchURLSize, pchOriginalFileName_sb, cchOriginalFileNameSize, out pPreviewType );
			if ( !bSuccess ) return bSuccess;
			pchOriginalFileName = pchOriginalFileName_sb.ToString();
			if ( !bSuccess ) return bSuccess;
			pchURLOrVideoID = pchURLOrVideoID_sb.ToString();
			return bSuccess;
		}
		
		// bool
		public bool GetQueryUGCChildren( UGCQueryHandle_t handle /*UGCQueryHandle_t*/, uint index /*uint32*/, PublishedFileId_t* pvecPublishedFileID /*PublishedFileId_t **/, uint cMaxEntries /*uint32*/ )
		{
			return _pi.ISteamUGC_GetQueryUGCChildren( handle, index, (IntPtr) pvecPublishedFileID, cMaxEntries );
		}
		
		// bool
		// with: Detect_StringFetch False
		// with: Detect_StringFetch False
		public bool GetQueryUGCKeyValueTag( UGCQueryHandle_t handle /*UGCQueryHandle_t*/, uint index /*uint32*/, uint keyValueTagIndex /*uint32*/, out string pchKey /*char **/, out string pchValue /*char **/ )
		{
			bool bSuccess = default( bool );
			pchKey = string.Empty;
			System.Text.StringBuilder pchKey_sb = new System.Text.StringBuilder( 4096 );
			uint cchKeySize = 4096;
			pchValue = string.Empty;
			System.Text.StringBuilder pchValue_sb = new System.Text.StringBuilder( 4096 );
			uint cchValueSize = 4096;
			bSuccess = _pi.ISteamUGC_GetQueryUGCKeyValueTag( handle, index, keyValueTagIndex, pchKey_sb, cchKeySize, pchValue_sb, cchValueSize );
			if ( !bSuccess ) return bSuccess;
			pchValue = pchValue_sb.ToString();
			if ( !bSuccess ) return bSuccess;
			pchKey = pchKey_sb.ToString();
			return bSuccess;
		}
		
		// bool
		// with: Detect_StringFetch False
		public bool GetQueryUGCMetadata( UGCQueryHandle_t handle /*UGCQueryHandle_t*/, uint index /*uint32*/, out string pchMetadata /*char **/ )
		{
			bool bSuccess = default( bool );
			pchMetadata = string.Empty;
			System.Text.StringBuilder pchMetadata_sb = new System.Text.StringBuilder( 4096 );
			uint cchMetadatasize = 4096;
			bSuccess = _pi.ISteamUGC_GetQueryUGCMetadata( handle, index, pchMetadata_sb, cchMetadatasize );
			if ( !bSuccess ) return bSuccess;
			pchMetadata = pchMetadata_sb.ToString();
			return bSuccess;
		}
		
		// uint
		public uint GetQueryUGCNumAdditionalPreviews( UGCQueryHandle_t handle /*UGCQueryHandle_t*/, uint index /*uint32*/ )
		{
			return _pi.ISteamUGC_GetQueryUGCNumAdditionalPreviews( handle, index );
		}
		
		// uint
		public uint GetQueryUGCNumKeyValueTags( UGCQueryHandle_t handle /*UGCQueryHandle_t*/, uint index /*uint32*/ )
		{
			return _pi.ISteamUGC_GetQueryUGCNumKeyValueTags( handle, index );
		}
		
		// bool
		// with: Detect_StringFetch False
		public bool GetQueryUGCPreviewURL( UGCQueryHandle_t handle /*UGCQueryHandle_t*/, uint index /*uint32*/, out string pchURL /*char **/ )
		{
			bool bSuccess = default( bool );
			pchURL = string.Empty;
			System.Text.StringBuilder pchURL_sb = new System.Text.StringBuilder( 4096 );
			uint cchURLSize = 4096;
			bSuccess = _pi.ISteamUGC_GetQueryUGCPreviewURL( handle, index, pchURL_sb, cchURLSize );
			if ( !bSuccess ) return bSuccess;
			pchURL = pchURL_sb.ToString();
			return bSuccess;
		}
		
		// bool
		public bool GetQueryUGCResult( UGCQueryHandle_t handle /*UGCQueryHandle_t*/, uint index /*uint32*/, ref SteamUGCDetails_t pDetails /*struct SteamUGCDetails_t **/ )
		{
			return _pi.ISteamUGC_GetQueryUGCResult( handle, index, ref pDetails );
		}
		
		// bool
		public bool GetQueryUGCStatistic( UGCQueryHandle_t handle /*UGCQueryHandle_t*/, uint index /*uint32*/, ItemStatistic eStatType /*EItemStatistic*/, out uint pStatValue /*uint32 **/ )
		{
			return _pi.ISteamUGC_GetQueryUGCStatistic( handle, index, eStatType, out pStatValue );
		}
		
		// uint
		public uint GetSubscribedItems( PublishedFileId_t* pvecPublishedFileID /*PublishedFileId_t **/, uint cMaxEntries /*uint32*/ )
		{
			return _pi.ISteamUGC_GetSubscribedItems( (IntPtr) pvecPublishedFileID, cMaxEntries );
		}
		
		// SteamAPICall_t
		public SteamAPICall_t GetUserItemVote( PublishedFileId_t nPublishedFileID /*PublishedFileId_t*/ )
		{
			return _pi.ISteamUGC_GetUserItemVote( nPublishedFileID );
		}
		
		// bool
		public bool ReleaseQueryUGCRequest( UGCQueryHandle_t handle /*UGCQueryHandle_t*/ )
		{
			return _pi.ISteamUGC_ReleaseQueryUGCRequest( handle );
		}
		
		// SteamAPICall_t
		public SteamAPICall_t RemoveItemFromFavorites( AppId_t nAppId /*AppId_t*/, PublishedFileId_t nPublishedFileID /*PublishedFileId_t*/ )
		{
			return _pi.ISteamUGC_RemoveItemFromFavorites( nAppId, nPublishedFileID );
		}
		
		// bool
		public bool RemoveItemKeyValueTags( UGCUpdateHandle_t handle /*UGCUpdateHandle_t*/, string pchKey /*const char **/ )
		{
			return _pi.ISteamUGC_RemoveItemKeyValueTags( handle, pchKey );
		}
		
		// bool
		public bool RemoveItemPreview( UGCUpdateHandle_t handle /*UGCUpdateHandle_t*/, uint index /*uint32*/ )
		{
			return _pi.ISteamUGC_RemoveItemPreview( handle, index );
		}
		
		// SteamAPICall_t
		public SteamAPICall_t RequestUGCDetails( PublishedFileId_t nPublishedFileID /*PublishedFileId_t*/, uint unMaxAgeSeconds /*uint32*/ )
		{
			return _pi.ISteamUGC_RequestUGCDetails( nPublishedFileID, unMaxAgeSeconds );
		}
		
		// SteamAPICall_t
		public SteamAPICall_t SendQueryUGCRequest( UGCQueryHandle_t handle /*UGCQueryHandle_t*/ )
		{
			return _pi.ISteamUGC_SendQueryUGCRequest( handle );
		}
		
		// bool
		public bool SetAllowCachedResponse( UGCQueryHandle_t handle /*UGCQueryHandle_t*/, uint unMaxAgeSeconds /*uint32*/ )
		{
			return _pi.ISteamUGC_SetAllowCachedResponse( handle, unMaxAgeSeconds );
		}
		
		// bool
		public bool SetCloudFileNameFilter( UGCQueryHandle_t handle /*UGCQueryHandle_t*/, string pMatchCloudFileName /*const char **/ )
		{
			return _pi.ISteamUGC_SetCloudFileNameFilter( handle, pMatchCloudFileName );
		}
		
		// bool
		public bool SetItemContent( UGCUpdateHandle_t handle /*UGCUpdateHandle_t*/, string pszContentFolder /*const char **/ )
		{
			return _pi.ISteamUGC_SetItemContent( handle, pszContentFolder );
		}
		
		// bool
		public bool SetItemDescription( UGCUpdateHandle_t handle /*UGCUpdateHandle_t*/, string pchDescription /*const char **/ )
		{
			return _pi.ISteamUGC_SetItemDescription( handle, pchDescription );
		}
		
		// bool
		public bool SetItemMetadata( UGCUpdateHandle_t handle /*UGCUpdateHandle_t*/, string pchMetaData /*const char **/ )
		{
			return _pi.ISteamUGC_SetItemMetadata( handle, pchMetaData );
		}
		
		// bool
		public bool SetItemPreview( UGCUpdateHandle_t handle /*UGCUpdateHandle_t*/, string pszPreviewFile /*const char **/ )
		{
			return _pi.ISteamUGC_SetItemPreview( handle, pszPreviewFile );
		}
		
		// bool
		public bool SetItemTags( UGCUpdateHandle_t updateHandle /*UGCUpdateHandle_t*/, IntPtr pTags /*const struct SteamParamStringArray_t **/ )
		{
			return _pi.ISteamUGC_SetItemTags( updateHandle, (IntPtr) pTags );
		}
		
		// bool
		public bool SetItemTitle( UGCUpdateHandle_t handle /*UGCUpdateHandle_t*/, string pchTitle /*const char **/ )
		{
			return _pi.ISteamUGC_SetItemTitle( handle, pchTitle );
		}
		
		// bool
		public bool SetItemUpdateLanguage( UGCUpdateHandle_t handle /*UGCUpdateHandle_t*/, string pchLanguage /*const char **/ )
		{
			return _pi.ISteamUGC_SetItemUpdateLanguage( handle, pchLanguage );
		}
		
		// bool
		public bool SetItemVisibility( UGCUpdateHandle_t handle /*UGCUpdateHandle_t*/, RemoteStoragePublishedFileVisibility eVisibility /*ERemoteStoragePublishedFileVisibility*/ )
		{
			return _pi.ISteamUGC_SetItemVisibility( handle, eVisibility );
		}
		
		// bool
		public bool SetLanguage( UGCQueryHandle_t handle /*UGCQueryHandle_t*/, string pchLanguage /*const char **/ )
		{
			return _pi.ISteamUGC_SetLanguage( handle, pchLanguage );
		}
		
		// bool
		public bool SetMatchAnyTag( UGCQueryHandle_t handle /*UGCQueryHandle_t*/, bool bMatchAnyTag /*bool*/ )
		{
			return _pi.ISteamUGC_SetMatchAnyTag( handle, bMatchAnyTag );
		}
		
		// bool
		public bool SetRankedByTrendDays( UGCQueryHandle_t handle /*UGCQueryHandle_t*/, uint unDays /*uint32*/ )
		{
			return _pi.ISteamUGC_SetRankedByTrendDays( handle, unDays );
		}
		
		// bool
		public bool SetReturnAdditionalPreviews( UGCQueryHandle_t handle /*UGCQueryHandle_t*/, bool bReturnAdditionalPreviews /*bool*/ )
		{
			return _pi.ISteamUGC_SetReturnAdditionalPreviews( handle, bReturnAdditionalPreviews );
		}
		
		// bool
		public bool SetReturnChildren( UGCQueryHandle_t handle /*UGCQueryHandle_t*/, bool bReturnChildren /*bool*/ )
		{
			return _pi.ISteamUGC_SetReturnChildren( handle, bReturnChildren );
		}
		
		// bool
		public bool SetReturnKeyValueTags( UGCQueryHandle_t handle /*UGCQueryHandle_t*/, bool bReturnKeyValueTags /*bool*/ )
		{
			return _pi.ISteamUGC_SetReturnKeyValueTags( handle, bReturnKeyValueTags );
		}
		
		// bool
		public bool SetReturnLongDescription( UGCQueryHandle_t handle /*UGCQueryHandle_t*/, bool bReturnLongDescription /*bool*/ )
		{
			return _pi.ISteamUGC_SetReturnLongDescription( handle, bReturnLongDescription );
		}
		
		// bool
		public bool SetReturnMetadata( UGCQueryHandle_t handle /*UGCQueryHandle_t*/, bool bReturnMetadata /*bool*/ )
		{
			return _pi.ISteamUGC_SetReturnMetadata( handle, bReturnMetadata );
		}
		
		// bool
		public bool SetReturnTotalOnly( UGCQueryHandle_t handle /*UGCQueryHandle_t*/, bool bReturnTotalOnly /*bool*/ )
		{
			return _pi.ISteamUGC_SetReturnTotalOnly( handle, bReturnTotalOnly );
		}
		
		// bool
		public bool SetSearchText( UGCQueryHandle_t handle /*UGCQueryHandle_t*/, string pSearchText /*const char **/ )
		{
			return _pi.ISteamUGC_SetSearchText( handle, pSearchText );
		}
		
		// SteamAPICall_t
		public SteamAPICall_t SetUserItemVote( PublishedFileId_t nPublishedFileID /*PublishedFileId_t*/, bool bVoteUp /*bool*/ )
		{
			return _pi.ISteamUGC_SetUserItemVote( nPublishedFileID, bVoteUp );
		}
		
		// UGCUpdateHandle_t
		public UGCUpdateHandle_t StartItemUpdate( AppId_t nConsumerAppId /*AppId_t*/, PublishedFileId_t nPublishedFileID /*PublishedFileId_t*/ )
		{
			return _pi.ISteamUGC_StartItemUpdate( nConsumerAppId, nPublishedFileID );
		}
		
		// SteamAPICall_t
		public SteamAPICall_t SubmitItemUpdate( UGCUpdateHandle_t handle /*UGCUpdateHandle_t*/, string pchChangeNote /*const char **/ )
		{
			return _pi.ISteamUGC_SubmitItemUpdate( handle, pchChangeNote );
		}
		
		// SteamAPICall_t
		public SteamAPICall_t SubscribeItem( PublishedFileId_t nPublishedFileID /*PublishedFileId_t*/ )
		{
			return _pi.ISteamUGC_SubscribeItem( nPublishedFileID );
		}
		
		// void
		public void SuspendDownloads( bool bSuspend /*bool*/ )
		{
			_pi.ISteamUGC_SuspendDownloads( bSuspend );
		}
		
		// SteamAPICall_t
		public SteamAPICall_t UnsubscribeItem( PublishedFileId_t nPublishedFileID /*PublishedFileId_t*/ )
		{
			return _pi.ISteamUGC_UnsubscribeItem( nPublishedFileID );
		}
		
		// bool
		public bool UpdateItemPreviewFile( UGCUpdateHandle_t handle /*UGCUpdateHandle_t*/, uint index /*uint32*/, string pszPreviewFile /*const char **/ )
		{
			return _pi.ISteamUGC_UpdateItemPreviewFile( handle, index, pszPreviewFile );
		}
		
		// bool
		public bool UpdateItemPreviewVideo( UGCUpdateHandle_t handle /*UGCUpdateHandle_t*/, uint index /*uint32*/, string pszVideoID /*const char **/ )
		{
			return _pi.ISteamUGC_UpdateItemPreviewVideo( handle, index, pszVideoID );
		}
		
	}
}
