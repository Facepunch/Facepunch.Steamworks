using System;
using System.Runtime.InteropServices;

namespace SteamNative
{
	public unsafe class SteamUGC
	{
		internal IntPtr _ptr;
		
		public SteamUGC( IntPtr pointer )
		{
			_ptr = pointer;
		}
		
		
		// bool
		public bool AddExcludedTag( UGCQueryHandle_t handle /*UGCQueryHandle_t*/, string pTagName /*const char **/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamUGC.AddExcludedTag( _ptr, handle, pTagName );
			else return Platform.Win64.ISteamUGC.AddExcludedTag( _ptr, handle, pTagName );
		}
		
		// bool
		public bool AddItemKeyValueTag( UGCUpdateHandle_t handle /*UGCUpdateHandle_t*/, string pchKey /*const char **/, string pchValue /*const char **/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamUGC.AddItemKeyValueTag( _ptr, handle, pchKey, pchValue );
			else return Platform.Win64.ISteamUGC.AddItemKeyValueTag( _ptr, handle, pchKey, pchValue );
		}
		
		// bool
		public bool AddItemPreviewFile( UGCUpdateHandle_t handle /*UGCUpdateHandle_t*/, string pszPreviewFile /*const char **/, ItemPreviewType type /*EItemPreviewType*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamUGC.AddItemPreviewFile( _ptr, handle, pszPreviewFile, type );
			else return Platform.Win64.ISteamUGC.AddItemPreviewFile( _ptr, handle, pszPreviewFile, type );
		}
		
		// bool
		public bool AddItemPreviewVideo( UGCUpdateHandle_t handle /*UGCUpdateHandle_t*/, string pszVideoID /*const char **/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamUGC.AddItemPreviewVideo( _ptr, handle, pszVideoID );
			else return Platform.Win64.ISteamUGC.AddItemPreviewVideo( _ptr, handle, pszVideoID );
		}
		
		// SteamAPICall_t
		public SteamAPICall_t AddItemToFavorites( AppId_t nAppId /*AppId_t*/, PublishedFileId_t nPublishedFileID /*PublishedFileId_t*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamUGC.AddItemToFavorites( _ptr, nAppId, nPublishedFileID );
			else return Platform.Win64.ISteamUGC.AddItemToFavorites( _ptr, nAppId, nPublishedFileID );
		}
		
		// bool
		public bool AddRequiredKeyValueTag( UGCQueryHandle_t handle /*UGCQueryHandle_t*/, string pKey /*const char **/, string pValue /*const char **/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamUGC.AddRequiredKeyValueTag( _ptr, handle, pKey, pValue );
			else return Platform.Win64.ISteamUGC.AddRequiredKeyValueTag( _ptr, handle, pKey, pValue );
		}
		
		// bool
		public bool AddRequiredTag( UGCQueryHandle_t handle /*UGCQueryHandle_t*/, string pTagName /*const char **/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamUGC.AddRequiredTag( _ptr, handle, pTagName );
			else return Platform.Win64.ISteamUGC.AddRequiredTag( _ptr, handle, pTagName );
		}
		
		// bool
		public bool BInitWorkshopForGameServer( DepotId_t unWorkshopDepotID /*DepotId_t*/, string pszFolder /*const char **/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamUGC.BInitWorkshopForGameServer( _ptr, unWorkshopDepotID, pszFolder );
			else return Platform.Win64.ISteamUGC.BInitWorkshopForGameServer( _ptr, unWorkshopDepotID, pszFolder );
		}
		
		// SteamAPICall_t
		public SteamAPICall_t CreateItem( AppId_t nConsumerAppId /*AppId_t*/, WorkshopFileType eFileType /*EWorkshopFileType*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamUGC.CreateItem( _ptr, nConsumerAppId, eFileType );
			else return Platform.Win64.ISteamUGC.CreateItem( _ptr, nConsumerAppId, eFileType );
		}
		
		// UGCQueryHandle_t
		public UGCQueryHandle_t CreateQueryAllUGCRequest( UGCQuery eQueryType /*EUGCQuery*/, UGCMatchingUGCType eMatchingeMatchingUGCTypeFileType /*EUGCMatchingUGCType*/, AppId_t nCreatorAppID /*AppId_t*/, AppId_t nConsumerAppID /*AppId_t*/, uint unPage /*uint32*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamUGC.CreateQueryAllUGCRequest( _ptr, eQueryType, eMatchingeMatchingUGCTypeFileType, nCreatorAppID, nConsumerAppID, unPage );
			else return Platform.Win64.ISteamUGC.CreateQueryAllUGCRequest( _ptr, eQueryType, eMatchingeMatchingUGCTypeFileType, nCreatorAppID, nConsumerAppID, unPage );
		}
		
		// with: Detect_VectorReturn
		// UGCQueryHandle_t
		public UGCQueryHandle_t CreateQueryUGCDetailsRequest( PublishedFileId_t[] pvecPublishedFileID /*PublishedFileId_t **/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			var unNumPublishedFileIDs = (uint) pvecPublishedFileID.Length;
			fixed ( PublishedFileId_t* pvecPublishedFileID_ptr = pvecPublishedFileID  )
			{
				if ( Platform.IsWindows32 ) return Platform.Win32.ISteamUGC.CreateQueryUGCDetailsRequest( _ptr, (IntPtr) pvecPublishedFileID_ptr, unNumPublishedFileIDs );
				else return Platform.Win64.ISteamUGC.CreateQueryUGCDetailsRequest( _ptr, (IntPtr) pvecPublishedFileID_ptr, unNumPublishedFileIDs );
			}
		}
		
		// UGCQueryHandle_t
		public UGCQueryHandle_t CreateQueryUserUGCRequest( AccountID_t unAccountID /*AccountID_t*/, UserUGCList eListType /*EUserUGCList*/, UGCMatchingUGCType eMatchingUGCType /*EUGCMatchingUGCType*/, UserUGCListSortOrder eSortOrder /*EUserUGCListSortOrder*/, AppId_t nCreatorAppID /*AppId_t*/, AppId_t nConsumerAppID /*AppId_t*/, uint unPage /*uint32*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamUGC.CreateQueryUserUGCRequest( _ptr, unAccountID, eListType, eMatchingUGCType, eSortOrder, nCreatorAppID, nConsumerAppID, unPage );
			else return Platform.Win64.ISteamUGC.CreateQueryUserUGCRequest( _ptr, unAccountID, eListType, eMatchingUGCType, eSortOrder, nCreatorAppID, nConsumerAppID, unPage );
		}
		
		// bool
		public bool DownloadItem( PublishedFileId_t nPublishedFileID /*PublishedFileId_t*/, bool bHighPriority /*bool*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamUGC.DownloadItem( _ptr, nPublishedFileID, bHighPriority );
			else return Platform.Win64.ISteamUGC.DownloadItem( _ptr, nPublishedFileID, bHighPriority );
		}
		
		// bool
		public bool GetItemDownloadInfo( PublishedFileId_t nPublishedFileID /*PublishedFileId_t*/, out ulong punBytesDownloaded /*uint64 **/, out ulong punBytesTotal /*uint64 **/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamUGC.GetItemDownloadInfo( _ptr, nPublishedFileID, out punBytesDownloaded, out punBytesTotal );
			else return Platform.Win64.ISteamUGC.GetItemDownloadInfo( _ptr, nPublishedFileID, out punBytesDownloaded, out punBytesTotal );
		}
		
		// bool
		// with: Detect_StringFetch False
		public bool GetItemInstallInfo( PublishedFileId_t nPublishedFileID /*PublishedFileId_t*/, out ulong punSizeOnDisk /*uint64 **/, out string pchFolder /*char **/, out uint punTimeStamp /*uint32 **/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			bool bSuccess = default( bool );
			pchFolder = string.Empty;
			System.Text.StringBuilder pchFolder_sb = new System.Text.StringBuilder( 4096 );
			uint cchFolderSize = 4096;
			if ( Platform.IsWindows32 ) bSuccess = Platform.Win32.ISteamUGC.GetItemInstallInfo( _ptr, nPublishedFileID, out punSizeOnDisk, pchFolder_sb, cchFolderSize, out punTimeStamp );
			else bSuccess = Platform.Win64.ISteamUGC.GetItemInstallInfo( _ptr, nPublishedFileID, out punSizeOnDisk, pchFolder_sb, cchFolderSize, out punTimeStamp );
			if ( !bSuccess ) return bSuccess;
			pchFolder = pchFolder_sb.ToString();
			return bSuccess;
		}
		
		// uint
		public uint GetItemState( PublishedFileId_t nPublishedFileID /*PublishedFileId_t*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamUGC.GetItemState( _ptr, nPublishedFileID );
			else return Platform.Win64.ISteamUGC.GetItemState( _ptr, nPublishedFileID );
		}
		
		// ItemUpdateStatus
		public ItemUpdateStatus GetItemUpdateProgress( UGCUpdateHandle_t handle /*UGCUpdateHandle_t*/, out ulong punBytesProcessed /*uint64 **/, out ulong punBytesTotal /*uint64 **/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamUGC.GetItemUpdateProgress( _ptr, handle, out punBytesProcessed, out punBytesTotal );
			else return Platform.Win64.ISteamUGC.GetItemUpdateProgress( _ptr, handle, out punBytesProcessed, out punBytesTotal );
		}
		
		// uint
		public uint GetNumSubscribedItems()
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamUGC.GetNumSubscribedItems( _ptr );
			else return Platform.Win64.ISteamUGC.GetNumSubscribedItems( _ptr );
		}
		
		// bool
		// with: Detect_StringFetch False
		// with: Detect_StringFetch False
		public bool GetQueryUGCAdditionalPreview( UGCQueryHandle_t handle /*UGCQueryHandle_t*/, uint index /*uint32*/, uint previewIndex /*uint32*/, out string pchURLOrVideoID /*char **/, out string pchOriginalFileName /*char **/, out ItemPreviewType pPreviewType /*EItemPreviewType **/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			bool bSuccess = default( bool );
			pchURLOrVideoID = string.Empty;
			System.Text.StringBuilder pchURLOrVideoID_sb = new System.Text.StringBuilder( 4096 );
			uint cchURLSize = 4096;
			pchOriginalFileName = string.Empty;
			System.Text.StringBuilder pchOriginalFileName_sb = new System.Text.StringBuilder( 4096 );
			uint cchOriginalFileNameSize = 4096;
			if ( Platform.IsWindows32 ) bSuccess = Platform.Win32.ISteamUGC.GetQueryUGCAdditionalPreview( _ptr, handle, index, previewIndex, pchURLOrVideoID_sb, cchURLSize, pchOriginalFileName_sb, cchOriginalFileNameSize, out pPreviewType );
			else bSuccess = Platform.Win64.ISteamUGC.GetQueryUGCAdditionalPreview( _ptr, handle, index, previewIndex, pchURLOrVideoID_sb, cchURLSize, pchOriginalFileName_sb, cchOriginalFileNameSize, out pPreviewType );
			if ( !bSuccess ) return bSuccess;
			pchOriginalFileName = pchOriginalFileName_sb.ToString();
			if ( !bSuccess ) return bSuccess;
			pchURLOrVideoID = pchURLOrVideoID_sb.ToString();
			return bSuccess;
		}
		
		// bool
		public bool GetQueryUGCChildren( UGCQueryHandle_t handle /*UGCQueryHandle_t*/, uint index /*uint32*/, PublishedFileId_t* pvecPublishedFileID /*PublishedFileId_t **/, uint cMaxEntries /*uint32*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamUGC.GetQueryUGCChildren( _ptr, handle, index, (IntPtr) pvecPublishedFileID, cMaxEntries );
			else return Platform.Win64.ISteamUGC.GetQueryUGCChildren( _ptr, handle, index, (IntPtr) pvecPublishedFileID, cMaxEntries );
		}
		
		// bool
		// with: Detect_StringFetch False
		// with: Detect_StringFetch False
		public bool GetQueryUGCKeyValueTag( UGCQueryHandle_t handle /*UGCQueryHandle_t*/, uint index /*uint32*/, uint keyValueTagIndex /*uint32*/, out string pchKey /*char **/, out string pchValue /*char **/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			bool bSuccess = default( bool );
			pchKey = string.Empty;
			System.Text.StringBuilder pchKey_sb = new System.Text.StringBuilder( 4096 );
			uint cchKeySize = 4096;
			pchValue = string.Empty;
			System.Text.StringBuilder pchValue_sb = new System.Text.StringBuilder( 4096 );
			uint cchValueSize = 4096;
			if ( Platform.IsWindows32 ) bSuccess = Platform.Win32.ISteamUGC.GetQueryUGCKeyValueTag( _ptr, handle, index, keyValueTagIndex, pchKey_sb, cchKeySize, pchValue_sb, cchValueSize );
			else bSuccess = Platform.Win64.ISteamUGC.GetQueryUGCKeyValueTag( _ptr, handle, index, keyValueTagIndex, pchKey_sb, cchKeySize, pchValue_sb, cchValueSize );
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
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			bool bSuccess = default( bool );
			pchMetadata = string.Empty;
			System.Text.StringBuilder pchMetadata_sb = new System.Text.StringBuilder( 4096 );
			uint cchMetadatasize = 4096;
			if ( Platform.IsWindows32 ) bSuccess = Platform.Win32.ISteamUGC.GetQueryUGCMetadata( _ptr, handle, index, pchMetadata_sb, cchMetadatasize );
			else bSuccess = Platform.Win64.ISteamUGC.GetQueryUGCMetadata( _ptr, handle, index, pchMetadata_sb, cchMetadatasize );
			if ( !bSuccess ) return bSuccess;
			pchMetadata = pchMetadata_sb.ToString();
			return bSuccess;
		}
		
		// uint
		public uint GetQueryUGCNumAdditionalPreviews( UGCQueryHandle_t handle /*UGCQueryHandle_t*/, uint index /*uint32*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamUGC.GetQueryUGCNumAdditionalPreviews( _ptr, handle, index );
			else return Platform.Win64.ISteamUGC.GetQueryUGCNumAdditionalPreviews( _ptr, handle, index );
		}
		
		// uint
		public uint GetQueryUGCNumKeyValueTags( UGCQueryHandle_t handle /*UGCQueryHandle_t*/, uint index /*uint32*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamUGC.GetQueryUGCNumKeyValueTags( _ptr, handle, index );
			else return Platform.Win64.ISteamUGC.GetQueryUGCNumKeyValueTags( _ptr, handle, index );
		}
		
		// bool
		// with: Detect_StringFetch False
		public bool GetQueryUGCPreviewURL( UGCQueryHandle_t handle /*UGCQueryHandle_t*/, uint index /*uint32*/, out string pchURL /*char **/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			bool bSuccess = default( bool );
			pchURL = string.Empty;
			System.Text.StringBuilder pchURL_sb = new System.Text.StringBuilder( 4096 );
			uint cchURLSize = 4096;
			if ( Platform.IsWindows32 ) bSuccess = Platform.Win32.ISteamUGC.GetQueryUGCPreviewURL( _ptr, handle, index, pchURL_sb, cchURLSize );
			else bSuccess = Platform.Win64.ISteamUGC.GetQueryUGCPreviewURL( _ptr, handle, index, pchURL_sb, cchURLSize );
			if ( !bSuccess ) return bSuccess;
			pchURL = pchURL_sb.ToString();
			return bSuccess;
		}
		
		// bool
		public bool GetQueryUGCResult( UGCQueryHandle_t handle /*UGCQueryHandle_t*/, uint index /*uint32*/, ref SteamUGCDetails_t pDetails /*struct SteamUGCDetails_t **/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamUGC.GetQueryUGCResult( _ptr, handle, index, ref pDetails );
			else return Platform.Win64.ISteamUGC.GetQueryUGCResult( _ptr, handle, index, ref pDetails );
		}
		
		// bool
		public bool GetQueryUGCStatistic( UGCQueryHandle_t handle /*UGCQueryHandle_t*/, uint index /*uint32*/, ItemStatistic eStatType /*EItemStatistic*/, out uint pStatValue /*uint32 **/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamUGC.GetQueryUGCStatistic( _ptr, handle, index, eStatType, out pStatValue );
			else return Platform.Win64.ISteamUGC.GetQueryUGCStatistic( _ptr, handle, index, eStatType, out pStatValue );
		}
		
		// uint
		public uint GetSubscribedItems( PublishedFileId_t* pvecPublishedFileID /*PublishedFileId_t **/, uint cMaxEntries /*uint32*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamUGC.GetSubscribedItems( _ptr, (IntPtr) pvecPublishedFileID, cMaxEntries );
			else return Platform.Win64.ISteamUGC.GetSubscribedItems( _ptr, (IntPtr) pvecPublishedFileID, cMaxEntries );
		}
		
		// SteamAPICall_t
		public SteamAPICall_t GetUserItemVote( PublishedFileId_t nPublishedFileID /*PublishedFileId_t*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamUGC.GetUserItemVote( _ptr, nPublishedFileID );
			else return Platform.Win64.ISteamUGC.GetUserItemVote( _ptr, nPublishedFileID );
		}
		
		// bool
		public bool ReleaseQueryUGCRequest( UGCQueryHandle_t handle /*UGCQueryHandle_t*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamUGC.ReleaseQueryUGCRequest( _ptr, handle );
			else return Platform.Win64.ISteamUGC.ReleaseQueryUGCRequest( _ptr, handle );
		}
		
		// SteamAPICall_t
		public SteamAPICall_t RemoveItemFromFavorites( AppId_t nAppId /*AppId_t*/, PublishedFileId_t nPublishedFileID /*PublishedFileId_t*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamUGC.RemoveItemFromFavorites( _ptr, nAppId, nPublishedFileID );
			else return Platform.Win64.ISteamUGC.RemoveItemFromFavorites( _ptr, nAppId, nPublishedFileID );
		}
		
		// bool
		public bool RemoveItemKeyValueTags( UGCUpdateHandle_t handle /*UGCUpdateHandle_t*/, string pchKey /*const char **/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamUGC.RemoveItemKeyValueTags( _ptr, handle, pchKey );
			else return Platform.Win64.ISteamUGC.RemoveItemKeyValueTags( _ptr, handle, pchKey );
		}
		
		// bool
		public bool RemoveItemPreview( UGCUpdateHandle_t handle /*UGCUpdateHandle_t*/, uint index /*uint32*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamUGC.RemoveItemPreview( _ptr, handle, index );
			else return Platform.Win64.ISteamUGC.RemoveItemPreview( _ptr, handle, index );
		}
		
		// SteamAPICall_t
		public SteamAPICall_t RequestUGCDetails( PublishedFileId_t nPublishedFileID /*PublishedFileId_t*/, uint unMaxAgeSeconds /*uint32*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamUGC.RequestUGCDetails( _ptr, nPublishedFileID, unMaxAgeSeconds );
			else return Platform.Win64.ISteamUGC.RequestUGCDetails( _ptr, nPublishedFileID, unMaxAgeSeconds );
		}
		
		// SteamAPICall_t
		public SteamAPICall_t SendQueryUGCRequest( UGCQueryHandle_t handle /*UGCQueryHandle_t*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamUGC.SendQueryUGCRequest( _ptr, handle );
			else return Platform.Win64.ISteamUGC.SendQueryUGCRequest( _ptr, handle );
		}
		
		// bool
		public bool SetAllowCachedResponse( UGCQueryHandle_t handle /*UGCQueryHandle_t*/, uint unMaxAgeSeconds /*uint32*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamUGC.SetAllowCachedResponse( _ptr, handle, unMaxAgeSeconds );
			else return Platform.Win64.ISteamUGC.SetAllowCachedResponse( _ptr, handle, unMaxAgeSeconds );
		}
		
		// bool
		public bool SetCloudFileNameFilter( UGCQueryHandle_t handle /*UGCQueryHandle_t*/, string pMatchCloudFileName /*const char **/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamUGC.SetCloudFileNameFilter( _ptr, handle, pMatchCloudFileName );
			else return Platform.Win64.ISteamUGC.SetCloudFileNameFilter( _ptr, handle, pMatchCloudFileName );
		}
		
		// bool
		public bool SetItemContent( UGCUpdateHandle_t handle /*UGCUpdateHandle_t*/, string pszContentFolder /*const char **/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamUGC.SetItemContent( _ptr, handle, pszContentFolder );
			else return Platform.Win64.ISteamUGC.SetItemContent( _ptr, handle, pszContentFolder );
		}
		
		// bool
		public bool SetItemDescription( UGCUpdateHandle_t handle /*UGCUpdateHandle_t*/, string pchDescription /*const char **/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamUGC.SetItemDescription( _ptr, handle, pchDescription );
			else return Platform.Win64.ISteamUGC.SetItemDescription( _ptr, handle, pchDescription );
		}
		
		// bool
		public bool SetItemMetadata( UGCUpdateHandle_t handle /*UGCUpdateHandle_t*/, string pchMetaData /*const char **/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamUGC.SetItemMetadata( _ptr, handle, pchMetaData );
			else return Platform.Win64.ISteamUGC.SetItemMetadata( _ptr, handle, pchMetaData );
		}
		
		// bool
		public bool SetItemPreview( UGCUpdateHandle_t handle /*UGCUpdateHandle_t*/, string pszPreviewFile /*const char **/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamUGC.SetItemPreview( _ptr, handle, pszPreviewFile );
			else return Platform.Win64.ISteamUGC.SetItemPreview( _ptr, handle, pszPreviewFile );
		}
		
		// bool
		public bool SetItemTags( UGCUpdateHandle_t updateHandle /*UGCUpdateHandle_t*/, IntPtr pTags /*const struct SteamParamStringArray_t **/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamUGC.SetItemTags( _ptr, updateHandle, (IntPtr) pTags );
			else return Platform.Win64.ISteamUGC.SetItemTags( _ptr, updateHandle, (IntPtr) pTags );
		}
		
		// bool
		public bool SetItemTitle( UGCUpdateHandle_t handle /*UGCUpdateHandle_t*/, string pchTitle /*const char **/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamUGC.SetItemTitle( _ptr, handle, pchTitle );
			else return Platform.Win64.ISteamUGC.SetItemTitle( _ptr, handle, pchTitle );
		}
		
		// bool
		public bool SetItemUpdateLanguage( UGCUpdateHandle_t handle /*UGCUpdateHandle_t*/, string pchLanguage /*const char **/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamUGC.SetItemUpdateLanguage( _ptr, handle, pchLanguage );
			else return Platform.Win64.ISteamUGC.SetItemUpdateLanguage( _ptr, handle, pchLanguage );
		}
		
		// bool
		public bool SetItemVisibility( UGCUpdateHandle_t handle /*UGCUpdateHandle_t*/, RemoteStoragePublishedFileVisibility eVisibility /*ERemoteStoragePublishedFileVisibility*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamUGC.SetItemVisibility( _ptr, handle, eVisibility );
			else return Platform.Win64.ISteamUGC.SetItemVisibility( _ptr, handle, eVisibility );
		}
		
		// bool
		public bool SetLanguage( UGCQueryHandle_t handle /*UGCQueryHandle_t*/, string pchLanguage /*const char **/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamUGC.SetLanguage( _ptr, handle, pchLanguage );
			else return Platform.Win64.ISteamUGC.SetLanguage( _ptr, handle, pchLanguage );
		}
		
		// bool
		public bool SetMatchAnyTag( UGCQueryHandle_t handle /*UGCQueryHandle_t*/, bool bMatchAnyTag /*bool*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamUGC.SetMatchAnyTag( _ptr, handle, bMatchAnyTag );
			else return Platform.Win64.ISteamUGC.SetMatchAnyTag( _ptr, handle, bMatchAnyTag );
		}
		
		// bool
		public bool SetRankedByTrendDays( UGCQueryHandle_t handle /*UGCQueryHandle_t*/, uint unDays /*uint32*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamUGC.SetRankedByTrendDays( _ptr, handle, unDays );
			else return Platform.Win64.ISteamUGC.SetRankedByTrendDays( _ptr, handle, unDays );
		}
		
		// bool
		public bool SetReturnAdditionalPreviews( UGCQueryHandle_t handle /*UGCQueryHandle_t*/, bool bReturnAdditionalPreviews /*bool*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamUGC.SetReturnAdditionalPreviews( _ptr, handle, bReturnAdditionalPreviews );
			else return Platform.Win64.ISteamUGC.SetReturnAdditionalPreviews( _ptr, handle, bReturnAdditionalPreviews );
		}
		
		// bool
		public bool SetReturnChildren( UGCQueryHandle_t handle /*UGCQueryHandle_t*/, bool bReturnChildren /*bool*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamUGC.SetReturnChildren( _ptr, handle, bReturnChildren );
			else return Platform.Win64.ISteamUGC.SetReturnChildren( _ptr, handle, bReturnChildren );
		}
		
		// bool
		public bool SetReturnKeyValueTags( UGCQueryHandle_t handle /*UGCQueryHandle_t*/, bool bReturnKeyValueTags /*bool*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamUGC.SetReturnKeyValueTags( _ptr, handle, bReturnKeyValueTags );
			else return Platform.Win64.ISteamUGC.SetReturnKeyValueTags( _ptr, handle, bReturnKeyValueTags );
		}
		
		// bool
		public bool SetReturnLongDescription( UGCQueryHandle_t handle /*UGCQueryHandle_t*/, bool bReturnLongDescription /*bool*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamUGC.SetReturnLongDescription( _ptr, handle, bReturnLongDescription );
			else return Platform.Win64.ISteamUGC.SetReturnLongDescription( _ptr, handle, bReturnLongDescription );
		}
		
		// bool
		public bool SetReturnMetadata( UGCQueryHandle_t handle /*UGCQueryHandle_t*/, bool bReturnMetadata /*bool*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamUGC.SetReturnMetadata( _ptr, handle, bReturnMetadata );
			else return Platform.Win64.ISteamUGC.SetReturnMetadata( _ptr, handle, bReturnMetadata );
		}
		
		// bool
		public bool SetReturnTotalOnly( UGCQueryHandle_t handle /*UGCQueryHandle_t*/, bool bReturnTotalOnly /*bool*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamUGC.SetReturnTotalOnly( _ptr, handle, bReturnTotalOnly );
			else return Platform.Win64.ISteamUGC.SetReturnTotalOnly( _ptr, handle, bReturnTotalOnly );
		}
		
		// bool
		public bool SetSearchText( UGCQueryHandle_t handle /*UGCQueryHandle_t*/, string pSearchText /*const char **/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamUGC.SetSearchText( _ptr, handle, pSearchText );
			else return Platform.Win64.ISteamUGC.SetSearchText( _ptr, handle, pSearchText );
		}
		
		// SteamAPICall_t
		public SteamAPICall_t SetUserItemVote( PublishedFileId_t nPublishedFileID /*PublishedFileId_t*/, bool bVoteUp /*bool*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamUGC.SetUserItemVote( _ptr, nPublishedFileID, bVoteUp );
			else return Platform.Win64.ISteamUGC.SetUserItemVote( _ptr, nPublishedFileID, bVoteUp );
		}
		
		// UGCUpdateHandle_t
		public UGCUpdateHandle_t StartItemUpdate( AppId_t nConsumerAppId /*AppId_t*/, PublishedFileId_t nPublishedFileID /*PublishedFileId_t*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamUGC.StartItemUpdate( _ptr, nConsumerAppId, nPublishedFileID );
			else return Platform.Win64.ISteamUGC.StartItemUpdate( _ptr, nConsumerAppId, nPublishedFileID );
		}
		
		// SteamAPICall_t
		public SteamAPICall_t SubmitItemUpdate( UGCUpdateHandle_t handle /*UGCUpdateHandle_t*/, string pchChangeNote /*const char **/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamUGC.SubmitItemUpdate( _ptr, handle, pchChangeNote );
			else return Platform.Win64.ISteamUGC.SubmitItemUpdate( _ptr, handle, pchChangeNote );
		}
		
		// SteamAPICall_t
		public SteamAPICall_t SubscribeItem( PublishedFileId_t nPublishedFileID /*PublishedFileId_t*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamUGC.SubscribeItem( _ptr, nPublishedFileID );
			else return Platform.Win64.ISteamUGC.SubscribeItem( _ptr, nPublishedFileID );
		}
		
		// void
		public void SuspendDownloads( bool bSuspend /*bool*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) Platform.Win32.ISteamUGC.SuspendDownloads( _ptr, bSuspend );
			else Platform.Win64.ISteamUGC.SuspendDownloads( _ptr, bSuspend );
		}
		
		// SteamAPICall_t
		public SteamAPICall_t UnsubscribeItem( PublishedFileId_t nPublishedFileID /*PublishedFileId_t*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamUGC.UnsubscribeItem( _ptr, nPublishedFileID );
			else return Platform.Win64.ISteamUGC.UnsubscribeItem( _ptr, nPublishedFileID );
		}
		
		// bool
		public bool UpdateItemPreviewFile( UGCUpdateHandle_t handle /*UGCUpdateHandle_t*/, uint index /*uint32*/, string pszPreviewFile /*const char **/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamUGC.UpdateItemPreviewFile( _ptr, handle, index, pszPreviewFile );
			else return Platform.Win64.ISteamUGC.UpdateItemPreviewFile( _ptr, handle, index, pszPreviewFile );
		}
		
		// bool
		public bool UpdateItemPreviewVideo( UGCUpdateHandle_t handle /*UGCUpdateHandle_t*/, uint index /*uint32*/, string pszVideoID /*const char **/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamUGC.UpdateItemPreviewVideo( _ptr, handle, index, pszVideoID );
			else return Platform.Win64.ISteamUGC.UpdateItemPreviewVideo( _ptr, handle, index, pszVideoID );
		}
		
	}
}
