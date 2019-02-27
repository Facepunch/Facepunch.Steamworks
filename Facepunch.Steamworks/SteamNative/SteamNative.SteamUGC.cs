using Facepunch.Steamworks;
using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace SteamNative
{
	internal unsafe class SteamUGC : IDisposable
	{
		//
		// Holds a platform specific implentation
		//
		internal Platform.Interface platform;
		internal Facepunch.Steamworks.BaseSteamworks steamworks;
		
		//
		// Constructor decides which implementation to use based on current platform
		//
		internal SteamUGC( Facepunch.Steamworks.BaseSteamworks steamworks, IntPtr pointer )
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
		
		// SteamAPICall_t
		public CallbackHandle AddAppDependency( PublishedFileId_t nPublishedFileID /*PublishedFileId_t*/, AppId_t nAppID /*AppId_t*/, Action<AddAppDependencyResult_t, bool> CallbackFunction = null /*Action<AddAppDependencyResult_t, bool>*/ )
		{
			SteamAPICall_t callback = 0;
			callback = platform.ISteamUGC_AddAppDependency( nPublishedFileID.Value, nAppID.Value );
			
			if ( CallbackFunction == null ) return null;
			if ( callback == 0 ) return null;
			
			return AddAppDependencyResult_t.CallResult( steamworks, callback, CallbackFunction );
		}
		
		// SteamAPICall_t
		public CallbackHandle AddDependency( PublishedFileId_t nParentPublishedFileID /*PublishedFileId_t*/, PublishedFileId_t nChildPublishedFileID /*PublishedFileId_t*/, Action<AddUGCDependencyResult_t, bool> CallbackFunction = null /*Action<AddUGCDependencyResult_t, bool>*/ )
		{
			SteamAPICall_t callback = 0;
			callback = platform.ISteamUGC_AddDependency( nParentPublishedFileID.Value, nChildPublishedFileID.Value );
			
			if ( CallbackFunction == null ) return null;
			if ( callback == 0 ) return null;
			
			return AddUGCDependencyResult_t.CallResult( steamworks, callback, CallbackFunction );
		}
		
		// bool
		public bool AddExcludedTag( UGCQueryHandle_t handle /*UGCQueryHandle_t*/, string pTagName /*const char **/ )
		{
			return platform.ISteamUGC_AddExcludedTag( handle.Value, Utility.GetUtf8Bytes(pTagName) );
		}
		
		// bool
		public bool AddItemKeyValueTag( UGCUpdateHandle_t handle /*UGCUpdateHandle_t*/, string pchKey /*const char **/, string pchValue /*const char **/ )
		{
			return platform.ISteamUGC_AddItemKeyValueTag( handle.Value, Utility.GetUtf8Bytes(pchKey), Utility.GetUtf8Bytes(pchValue) );
		}
		
		// bool
		public bool AddItemPreviewFile( UGCUpdateHandle_t handle /*UGCUpdateHandle_t*/, string pszPreviewFile /*const char **/, ItemPreviewType type /*EItemPreviewType*/ )
		{
			return platform.ISteamUGC_AddItemPreviewFile( handle.Value, Utility.GetUtf8Bytes(pszPreviewFile), type );
		}
		
		// bool
		public bool AddItemPreviewVideo( UGCUpdateHandle_t handle /*UGCUpdateHandle_t*/, string pszVideoID /*const char **/ )
		{
			return platform.ISteamUGC_AddItemPreviewVideo( handle.Value, Utility.GetUtf8Bytes(pszVideoID) );
		}
		
		// SteamAPICall_t
		public CallbackHandle AddItemToFavorites( AppId_t nAppId /*AppId_t*/, PublishedFileId_t nPublishedFileID /*PublishedFileId_t*/, Action<UserFavoriteItemsListChanged_t, bool> CallbackFunction = null /*Action<UserFavoriteItemsListChanged_t, bool>*/ )
		{
			SteamAPICall_t callback = 0;
			callback = platform.ISteamUGC_AddItemToFavorites( nAppId.Value, nPublishedFileID.Value );
			
			if ( CallbackFunction == null ) return null;
			if ( callback == 0 ) return null;
			
			return UserFavoriteItemsListChanged_t.CallResult( steamworks, callback, CallbackFunction );
		}
		
		// bool
		public bool AddRequiredKeyValueTag( UGCQueryHandle_t handle /*UGCQueryHandle_t*/, string pKey /*const char **/, string pValue /*const char **/ )
		{
			return platform.ISteamUGC_AddRequiredKeyValueTag( handle.Value, Utility.GetUtf8Bytes(pKey), Utility.GetUtf8Bytes(pValue) );
		}
		
		// bool
		public bool AddRequiredTag( UGCQueryHandle_t handle /*UGCQueryHandle_t*/, string pTagName /*const char **/ )
		{
			return platform.ISteamUGC_AddRequiredTag( handle.Value, Utility.GetUtf8Bytes(pTagName) );
		}
		
		// bool
		public bool BInitWorkshopForGameServer( DepotId_t unWorkshopDepotID /*DepotId_t*/, string pszFolder /*const char **/ )
		{
			return platform.ISteamUGC_BInitWorkshopForGameServer( unWorkshopDepotID.Value, Utility.GetUtf8Bytes(pszFolder) );
		}
		
		// SteamAPICall_t
		public CallbackHandle CreateItem( AppId_t nConsumerAppId /*AppId_t*/, WorkshopFileType eFileType /*EWorkshopFileType*/, Action<CreateItemResult_t, bool> CallbackFunction = null /*Action<CreateItemResult_t, bool>*/ )
		{
			SteamAPICall_t callback = 0;
			callback = platform.ISteamUGC_CreateItem( nConsumerAppId.Value, eFileType );
			
			if ( CallbackFunction == null ) return null;
			if ( callback == 0 ) return null;
			
			return CreateItemResult_t.CallResult( steamworks, callback, CallbackFunction );
		}
		
		// UGCQueryHandle_t
		public UGCQueryHandle_t CreateQueryAllUGCRequest( UGCQuery eQueryType /*EUGCQuery*/, UGCMatchingUGCType eMatchingeMatchingUGCTypeFileType /*EUGCMatchingUGCType*/, AppId_t nCreatorAppID /*AppId_t*/, AppId_t nConsumerAppID /*AppId_t*/, uint unPage /*uint32*/ )
		{
			return platform.ISteamUGC_CreateQueryAllUGCRequest( eQueryType, eMatchingeMatchingUGCTypeFileType, nCreatorAppID.Value, nConsumerAppID.Value, unPage );
		}
		
		// with: Detect_VectorReturn
		// UGCQueryHandle_t
		public UGCQueryHandle_t CreateQueryUGCDetailsRequest( PublishedFileId_t[] pvecPublishedFileID /*PublishedFileId_t **/ )
		{
			var unNumPublishedFileIDs = (uint) pvecPublishedFileID.Length;
			fixed ( PublishedFileId_t* pvecPublishedFileID_ptr = pvecPublishedFileID  )
			{
				return platform.ISteamUGC_CreateQueryUGCDetailsRequest( (IntPtr) pvecPublishedFileID_ptr, unNumPublishedFileIDs );
			}
		}
		
		// UGCQueryHandle_t
		public UGCQueryHandle_t CreateQueryUserUGCRequest( AccountID_t unAccountID /*AccountID_t*/, UserUGCList eListType /*EUserUGCList*/, UGCMatchingUGCType eMatchingUGCType /*EUGCMatchingUGCType*/, UserUGCListSortOrder eSortOrder /*EUserUGCListSortOrder*/, AppId_t nCreatorAppID /*AppId_t*/, AppId_t nConsumerAppID /*AppId_t*/, uint unPage /*uint32*/ )
		{
			return platform.ISteamUGC_CreateQueryUserUGCRequest( unAccountID.Value, eListType, eMatchingUGCType, eSortOrder, nCreatorAppID.Value, nConsumerAppID.Value, unPage );
		}
		
		// SteamAPICall_t
		public CallbackHandle DeleteItem( PublishedFileId_t nPublishedFileID /*PublishedFileId_t*/, Action<DeleteItemResult_t, bool> CallbackFunction = null /*Action<DeleteItemResult_t, bool>*/ )
		{
			SteamAPICall_t callback = 0;
			callback = platform.ISteamUGC_DeleteItem( nPublishedFileID.Value );
			
			if ( CallbackFunction == null ) return null;
			if ( callback == 0 ) return null;
			
			return DeleteItemResult_t.CallResult( steamworks, callback, CallbackFunction );
		}
		
		// bool
		public bool DownloadItem( PublishedFileId_t nPublishedFileID /*PublishedFileId_t*/, bool bHighPriority /*bool*/ )
		{
			return platform.ISteamUGC_DownloadItem( nPublishedFileID.Value, bHighPriority );
		}
		
		// SteamAPICall_t
		public CallbackHandle GetAppDependencies( PublishedFileId_t nPublishedFileID /*PublishedFileId_t*/, Action<GetAppDependenciesResult_t, bool> CallbackFunction = null /*Action<GetAppDependenciesResult_t, bool>*/ )
		{
			SteamAPICall_t callback = 0;
			callback = platform.ISteamUGC_GetAppDependencies( nPublishedFileID.Value );
			
			if ( CallbackFunction == null ) return null;
			if ( callback == 0 ) return null;
			
			return GetAppDependenciesResult_t.CallResult( steamworks, callback, CallbackFunction );
		}
		
		// bool
		public bool GetItemDownloadInfo( PublishedFileId_t nPublishedFileID /*PublishedFileId_t*/, out ulong punBytesDownloaded /*uint64 **/, out ulong punBytesTotal /*uint64 **/ )
		{
			return platform.ISteamUGC_GetItemDownloadInfo( nPublishedFileID.Value, out punBytesDownloaded, out punBytesTotal );
		}
		
		// bool
		// with: Detect_StringFetch False
		public bool GetItemInstallInfo( PublishedFileId_t nPublishedFileID /*PublishedFileId_t*/, out ulong punSizeOnDisk /*uint64 **/, out string pchFolder /*char **/, out uint punTimeStamp /*uint32 **/ )
		{
			bool bSuccess = default( bool );
			pchFolder = string.Empty;
			System.Text.StringBuilder pchFolder_sb = Helpers.TakeStringBuilder();
			uint cchFolderSize = 4096;
			bSuccess = platform.ISteamUGC_GetItemInstallInfo( nPublishedFileID.Value, out punSizeOnDisk, pchFolder_sb, cchFolderSize, out punTimeStamp );
			if ( !bSuccess ) return bSuccess;
			pchFolder = pchFolder_sb.ToString();
			return bSuccess;
		}
		
		// uint
		public uint GetItemState( PublishedFileId_t nPublishedFileID /*PublishedFileId_t*/ )
		{
			return platform.ISteamUGC_GetItemState( nPublishedFileID.Value );
		}
		
		// ItemUpdateStatus
		public ItemUpdateStatus GetItemUpdateProgress( UGCUpdateHandle_t handle /*UGCUpdateHandle_t*/, out ulong punBytesProcessed /*uint64 **/, out ulong punBytesTotal /*uint64 **/ )
		{
			return platform.ISteamUGC_GetItemUpdateProgress( handle.Value, out punBytesProcessed, out punBytesTotal );
		}
		
		// uint
		public uint GetNumSubscribedItems()
		{
			return platform.ISteamUGC_GetNumSubscribedItems();
		}
		
		// bool
		// with: Detect_StringFetch False
		// with: Detect_StringFetch False
		public bool GetQueryUGCAdditionalPreview( UGCQueryHandle_t handle /*UGCQueryHandle_t*/, uint index /*uint32*/, uint previewIndex /*uint32*/, out string pchURLOrVideoID /*char **/, out string pchOriginalFileName /*char **/, out ItemPreviewType pPreviewType /*EItemPreviewType **/ )
		{
			bool bSuccess = default( bool );
			pchURLOrVideoID = string.Empty;
			System.Text.StringBuilder pchURLOrVideoID_sb = Helpers.TakeStringBuilder();
			uint cchURLSize = 4096;
			pchOriginalFileName = string.Empty;
			System.Text.StringBuilder pchOriginalFileName_sb = Helpers.TakeStringBuilder();
			uint cchOriginalFileNameSize = 4096;
			bSuccess = platform.ISteamUGC_GetQueryUGCAdditionalPreview( handle.Value, index, previewIndex, pchURLOrVideoID_sb, cchURLSize, pchOriginalFileName_sb, cchOriginalFileNameSize, out pPreviewType );
			if ( !bSuccess ) return bSuccess;
			pchOriginalFileName = pchOriginalFileName_sb.ToString();
			if ( !bSuccess ) return bSuccess;
			pchURLOrVideoID = pchURLOrVideoID_sb.ToString();
			return bSuccess;
		}
		
		// bool
		public bool GetQueryUGCChildren( UGCQueryHandle_t handle /*UGCQueryHandle_t*/, uint index /*uint32*/, PublishedFileId_t* pvecPublishedFileID /*PublishedFileId_t **/, uint cMaxEntries /*uint32*/ )
		{
			return platform.ISteamUGC_GetQueryUGCChildren( handle.Value, index, (IntPtr) pvecPublishedFileID, cMaxEntries );
		}
		
		// bool
		// with: Detect_StringFetch False
		// with: Detect_StringFetch False
		public bool GetQueryUGCKeyValueTag( UGCQueryHandle_t handle /*UGCQueryHandle_t*/, uint index /*uint32*/, uint keyValueTagIndex /*uint32*/, out string pchKey /*char **/, out string pchValue /*char **/ )
		{
			bool bSuccess = default( bool );
			pchKey = string.Empty;
			System.Text.StringBuilder pchKey_sb = Helpers.TakeStringBuilder();
			uint cchKeySize = 4096;
			pchValue = string.Empty;
			System.Text.StringBuilder pchValue_sb = Helpers.TakeStringBuilder();
			uint cchValueSize = 4096;
			bSuccess = platform.ISteamUGC_GetQueryUGCKeyValueTag( handle.Value, index, keyValueTagIndex, pchKey_sb, cchKeySize, pchValue_sb, cchValueSize );
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
			System.Text.StringBuilder pchMetadata_sb = Helpers.TakeStringBuilder();
			uint cchMetadatasize = 4096;
			bSuccess = platform.ISteamUGC_GetQueryUGCMetadata( handle.Value, index, pchMetadata_sb, cchMetadatasize );
			if ( !bSuccess ) return bSuccess;
			pchMetadata = pchMetadata_sb.ToString();
			return bSuccess;
		}
		
		// uint
		public uint GetQueryUGCNumAdditionalPreviews( UGCQueryHandle_t handle /*UGCQueryHandle_t*/, uint index /*uint32*/ )
		{
			return platform.ISteamUGC_GetQueryUGCNumAdditionalPreviews( handle.Value, index );
		}
		
		// uint
		public uint GetQueryUGCNumKeyValueTags( UGCQueryHandle_t handle /*UGCQueryHandle_t*/, uint index /*uint32*/ )
		{
			return platform.ISteamUGC_GetQueryUGCNumKeyValueTags( handle.Value, index );
		}
		
		// bool
		// with: Detect_StringFetch False
		public bool GetQueryUGCPreviewURL( UGCQueryHandle_t handle /*UGCQueryHandle_t*/, uint index /*uint32*/, out string pchURL /*char **/ )
		{
			bool bSuccess = default( bool );
			pchURL = string.Empty;
			System.Text.StringBuilder pchURL_sb = Helpers.TakeStringBuilder();
			uint cchURLSize = 4096;
			bSuccess = platform.ISteamUGC_GetQueryUGCPreviewURL( handle.Value, index, pchURL_sb, cchURLSize );
			if ( !bSuccess ) return bSuccess;
			pchURL = pchURL_sb.ToString();
			return bSuccess;
		}
		
		// bool
		public bool GetQueryUGCResult( UGCQueryHandle_t handle /*UGCQueryHandle_t*/, uint index /*uint32*/, ref SteamUGCDetails_t pDetails /*struct SteamUGCDetails_t **/ )
		{
			return platform.ISteamUGC_GetQueryUGCResult( handle.Value, index, ref pDetails );
		}
		
		// bool
		public bool GetQueryUGCStatistic( UGCQueryHandle_t handle /*UGCQueryHandle_t*/, uint index /*uint32*/, ItemStatistic eStatType /*EItemStatistic*/, out ulong pStatValue /*uint64 **/ )
		{
			return platform.ISteamUGC_GetQueryUGCStatistic( handle.Value, index, eStatType, out pStatValue );
		}
		
		// uint
		public uint GetSubscribedItems( PublishedFileId_t* pvecPublishedFileID /*PublishedFileId_t **/, uint cMaxEntries /*uint32*/ )
		{
			return platform.ISteamUGC_GetSubscribedItems( (IntPtr) pvecPublishedFileID, cMaxEntries );
		}
		
		// SteamAPICall_t
		public CallbackHandle GetUserItemVote( PublishedFileId_t nPublishedFileID /*PublishedFileId_t*/, Action<GetUserItemVoteResult_t, bool> CallbackFunction = null /*Action<GetUserItemVoteResult_t, bool>*/ )
		{
			SteamAPICall_t callback = 0;
			callback = platform.ISteamUGC_GetUserItemVote( nPublishedFileID.Value );
			
			if ( CallbackFunction == null ) return null;
			if ( callback == 0 ) return null;
			
			return GetUserItemVoteResult_t.CallResult( steamworks, callback, CallbackFunction );
		}
		
		// bool
		public bool ReleaseQueryUGCRequest( UGCQueryHandle_t handle /*UGCQueryHandle_t*/ )
		{
			return platform.ISteamUGC_ReleaseQueryUGCRequest( handle.Value );
		}
		
		// SteamAPICall_t
		public CallbackHandle RemoveAppDependency( PublishedFileId_t nPublishedFileID /*PublishedFileId_t*/, AppId_t nAppID /*AppId_t*/, Action<RemoveAppDependencyResult_t, bool> CallbackFunction = null /*Action<RemoveAppDependencyResult_t, bool>*/ )
		{
			SteamAPICall_t callback = 0;
			callback = platform.ISteamUGC_RemoveAppDependency( nPublishedFileID.Value, nAppID.Value );
			
			if ( CallbackFunction == null ) return null;
			if ( callback == 0 ) return null;
			
			return RemoveAppDependencyResult_t.CallResult( steamworks, callback, CallbackFunction );
		}
		
		// SteamAPICall_t
		public CallbackHandle RemoveDependency( PublishedFileId_t nParentPublishedFileID /*PublishedFileId_t*/, PublishedFileId_t nChildPublishedFileID /*PublishedFileId_t*/, Action<RemoveUGCDependencyResult_t, bool> CallbackFunction = null /*Action<RemoveUGCDependencyResult_t, bool>*/ )
		{
			SteamAPICall_t callback = 0;
			callback = platform.ISteamUGC_RemoveDependency( nParentPublishedFileID.Value, nChildPublishedFileID.Value );
			
			if ( CallbackFunction == null ) return null;
			if ( callback == 0 ) return null;
			
			return RemoveUGCDependencyResult_t.CallResult( steamworks, callback, CallbackFunction );
		}
		
		// SteamAPICall_t
		public CallbackHandle RemoveItemFromFavorites( AppId_t nAppId /*AppId_t*/, PublishedFileId_t nPublishedFileID /*PublishedFileId_t*/, Action<UserFavoriteItemsListChanged_t, bool> CallbackFunction = null /*Action<UserFavoriteItemsListChanged_t, bool>*/ )
		{
			SteamAPICall_t callback = 0;
			callback = platform.ISteamUGC_RemoveItemFromFavorites( nAppId.Value, nPublishedFileID.Value );
			
			if ( CallbackFunction == null ) return null;
			if ( callback == 0 ) return null;
			
			return UserFavoriteItemsListChanged_t.CallResult( steamworks, callback, CallbackFunction );
		}
		
		// bool
		public bool RemoveItemKeyValueTags( UGCUpdateHandle_t handle /*UGCUpdateHandle_t*/, string pchKey /*const char **/ )
		{
			return platform.ISteamUGC_RemoveItemKeyValueTags( handle.Value, Utility.GetUtf8Bytes(pchKey) );
		}
		
		// bool
		public bool RemoveItemPreview( UGCUpdateHandle_t handle /*UGCUpdateHandle_t*/, uint index /*uint32*/ )
		{
			return platform.ISteamUGC_RemoveItemPreview( handle.Value, index );
		}
		
		// SteamAPICall_t
		public SteamAPICall_t RequestUGCDetails( PublishedFileId_t nPublishedFileID /*PublishedFileId_t*/, uint unMaxAgeSeconds /*uint32*/ )
		{
			return platform.ISteamUGC_RequestUGCDetails( nPublishedFileID.Value, unMaxAgeSeconds );
		}
		
		// SteamAPICall_t
		public CallbackHandle SendQueryUGCRequest( UGCQueryHandle_t handle /*UGCQueryHandle_t*/, Action<SteamUGCQueryCompleted_t, bool> CallbackFunction = null /*Action<SteamUGCQueryCompleted_t, bool>*/ )
		{
			SteamAPICall_t callback = 0;
			callback = platform.ISteamUGC_SendQueryUGCRequest( handle.Value );
			
			if ( CallbackFunction == null ) return null;
			if ( callback == 0 ) return null;
			
			return SteamUGCQueryCompleted_t.CallResult( steamworks, callback, CallbackFunction );
		}
		
		// bool
		public bool SetAllowCachedResponse( UGCQueryHandle_t handle /*UGCQueryHandle_t*/, uint unMaxAgeSeconds /*uint32*/ )
		{
			return platform.ISteamUGC_SetAllowCachedResponse( handle.Value, unMaxAgeSeconds );
		}
		
		// bool
		public bool SetCloudFileNameFilter( UGCQueryHandle_t handle /*UGCQueryHandle_t*/, string pMatchCloudFileName /*const char **/ )
		{
			return platform.ISteamUGC_SetCloudFileNameFilter( handle.Value, Utility.GetUtf8Bytes(pMatchCloudFileName) );
		}
		
		// bool
		public bool SetItemContent( UGCUpdateHandle_t handle /*UGCUpdateHandle_t*/, string pszContentFolder /*const char **/ )
		{
			return platform.ISteamUGC_SetItemContent( handle.Value, Utility.GetUtf8Bytes(pszContentFolder) );
		}
		
		// bool
		public bool SetItemDescription( UGCUpdateHandle_t handle /*UGCUpdateHandle_t*/, string pchDescription /*const char **/ )
		{
			return platform.ISteamUGC_SetItemDescription( handle.Value, Utility.GetUtf8Bytes(pchDescription) );
		}
		
		// bool
		public bool SetItemMetadata( UGCUpdateHandle_t handle /*UGCUpdateHandle_t*/, string pchMetaData /*const char **/ )
		{
			return platform.ISteamUGC_SetItemMetadata( handle.Value, Utility.GetUtf8Bytes(pchMetaData) );
		}
		
		// bool
		public bool SetItemPreview( UGCUpdateHandle_t handle /*UGCUpdateHandle_t*/, string pszPreviewFile /*const char **/ )
		{
			return platform.ISteamUGC_SetItemPreview( handle.Value, Utility.GetUtf8Bytes(pszPreviewFile) );
		}
		
		// bool
		// using: Detect_StringArray
		public bool SetItemTags( UGCUpdateHandle_t updateHandle /*UGCUpdateHandle_t*/, string[] pTags /*const struct SteamParamStringArray_t **/ )
		{
			// Create strings
			var nativeStrings = new IntPtr[pTags.Length];
			for ( int i = 0; i < pTags.Length; i++ )
			{
				nativeStrings[i] = Marshal.StringToHGlobalAnsi( pTags[i] );
			}
			try
			{
				
				// Create string array
				var size = Marshal.SizeOf( typeof( IntPtr ) ) * nativeStrings.Length;
				var nativeArray = Marshal.AllocHGlobal( size );
				Marshal.Copy( nativeStrings, 0, nativeArray, nativeStrings.Length );
				
				// Create SteamParamStringArray_t
				var tags = new SteamParamStringArray_t();
				tags.Strings = nativeArray;
				tags.NumStrings = pTags.Length;
				return platform.ISteamUGC_SetItemTags( updateHandle.Value, ref tags );
			}
			finally
			{
				foreach ( var x in nativeStrings )
				   Marshal.FreeHGlobal( x );
				
			}
		}
		
		// bool
		public bool SetItemTitle( UGCUpdateHandle_t handle /*UGCUpdateHandle_t*/, string pchTitle /*const char **/ )
		{
			return platform.ISteamUGC_SetItemTitle( handle.Value, Utility.GetUtf8Bytes(pchTitle) );
		}
		
		// bool
		public bool SetItemUpdateLanguage( UGCUpdateHandle_t handle /*UGCUpdateHandle_t*/, string pchLanguage /*const char **/ )
		{
			return platform.ISteamUGC_SetItemUpdateLanguage( handle.Value, Utility.GetUtf8Bytes(pchLanguage) );
		}
		
		// bool
		public bool SetItemVisibility( UGCUpdateHandle_t handle /*UGCUpdateHandle_t*/, RemoteStoragePublishedFileVisibility eVisibility /*ERemoteStoragePublishedFileVisibility*/ )
		{
			return platform.ISteamUGC_SetItemVisibility( handle.Value, eVisibility );
		}
		
		// bool
		public bool SetLanguage( UGCQueryHandle_t handle /*UGCQueryHandle_t*/, string pchLanguage /*const char **/ )
		{
			return platform.ISteamUGC_SetLanguage( handle.Value, Utility.GetUtf8Bytes(pchLanguage) );
		}
		
		// bool
		public bool SetMatchAnyTag( UGCQueryHandle_t handle /*UGCQueryHandle_t*/, bool bMatchAnyTag /*bool*/ )
		{
			return platform.ISteamUGC_SetMatchAnyTag( handle.Value, bMatchAnyTag );
		}
		
		// bool
		public bool SetRankedByTrendDays( UGCQueryHandle_t handle /*UGCQueryHandle_t*/, uint unDays /*uint32*/ )
		{
			return platform.ISteamUGC_SetRankedByTrendDays( handle.Value, unDays );
		}
		
		// bool
		public bool SetReturnAdditionalPreviews( UGCQueryHandle_t handle /*UGCQueryHandle_t*/, bool bReturnAdditionalPreviews /*bool*/ )
		{
			return platform.ISteamUGC_SetReturnAdditionalPreviews( handle.Value, bReturnAdditionalPreviews );
		}
		
		// bool
		public bool SetReturnChildren( UGCQueryHandle_t handle /*UGCQueryHandle_t*/, bool bReturnChildren /*bool*/ )
		{
			return platform.ISteamUGC_SetReturnChildren( handle.Value, bReturnChildren );
		}
		
		// bool
		public bool SetReturnKeyValueTags( UGCQueryHandle_t handle /*UGCQueryHandle_t*/, bool bReturnKeyValueTags /*bool*/ )
		{
			return platform.ISteamUGC_SetReturnKeyValueTags( handle.Value, bReturnKeyValueTags );
		}
		
		// bool
		public bool SetReturnLongDescription( UGCQueryHandle_t handle /*UGCQueryHandle_t*/, bool bReturnLongDescription /*bool*/ )
		{
			return platform.ISteamUGC_SetReturnLongDescription( handle.Value, bReturnLongDescription );
		}
		
		// bool
		public bool SetReturnMetadata( UGCQueryHandle_t handle /*UGCQueryHandle_t*/, bool bReturnMetadata /*bool*/ )
		{
			return platform.ISteamUGC_SetReturnMetadata( handle.Value, bReturnMetadata );
		}
		
		// bool
		public bool SetReturnOnlyIDs( UGCQueryHandle_t handle /*UGCQueryHandle_t*/, bool bReturnOnlyIDs /*bool*/ )
		{
			return platform.ISteamUGC_SetReturnOnlyIDs( handle.Value, bReturnOnlyIDs );
		}
		
		// bool
		public bool SetReturnPlaytimeStats( UGCQueryHandle_t handle /*UGCQueryHandle_t*/, uint unDays /*uint32*/ )
		{
			return platform.ISteamUGC_SetReturnPlaytimeStats( handle.Value, unDays );
		}
		
		// bool
		public bool SetReturnTotalOnly( UGCQueryHandle_t handle /*UGCQueryHandle_t*/, bool bReturnTotalOnly /*bool*/ )
		{
			return platform.ISteamUGC_SetReturnTotalOnly( handle.Value, bReturnTotalOnly );
		}
		
		// bool
		public bool SetSearchText( UGCQueryHandle_t handle /*UGCQueryHandle_t*/, string pSearchText /*const char **/ )
		{
			return platform.ISteamUGC_SetSearchText( handle.Value, Utility.GetUtf8Bytes(pSearchText) );
		}
		
		// SteamAPICall_t
		public CallbackHandle SetUserItemVote( PublishedFileId_t nPublishedFileID /*PublishedFileId_t*/, bool bVoteUp /*bool*/, Action<SetUserItemVoteResult_t, bool> CallbackFunction = null /*Action<SetUserItemVoteResult_t, bool>*/ )
		{
			SteamAPICall_t callback = 0;
			callback = platform.ISteamUGC_SetUserItemVote( nPublishedFileID.Value, bVoteUp );
			
			if ( CallbackFunction == null ) return null;
			if ( callback == 0 ) return null;
			
			return SetUserItemVoteResult_t.CallResult( steamworks, callback, CallbackFunction );
		}
		
		// UGCUpdateHandle_t
		public UGCUpdateHandle_t StartItemUpdate( AppId_t nConsumerAppId /*AppId_t*/, PublishedFileId_t nPublishedFileID /*PublishedFileId_t*/ )
		{
			return platform.ISteamUGC_StartItemUpdate( nConsumerAppId.Value, nPublishedFileID.Value );
		}
		
		// with: Detect_VectorReturn
		// SteamAPICall_t
		public CallbackHandle StartPlaytimeTracking( PublishedFileId_t[] pvecPublishedFileID /*PublishedFileId_t **/, Action<StartPlaytimeTrackingResult_t, bool> CallbackFunction = null /*Action<StartPlaytimeTrackingResult_t, bool>*/ )
		{
			SteamAPICall_t callback = 0;
			var unNumPublishedFileIDs = (uint) pvecPublishedFileID.Length;
			fixed ( PublishedFileId_t* pvecPublishedFileID_ptr = pvecPublishedFileID  )
			{
				callback = platform.ISteamUGC_StartPlaytimeTracking( (IntPtr) pvecPublishedFileID_ptr, unNumPublishedFileIDs );
			}
			
			if ( CallbackFunction == null ) return null;
			if ( callback == 0 ) return null;
			
			return StartPlaytimeTrackingResult_t.CallResult( steamworks, callback, CallbackFunction );
		}
		
		// with: Detect_VectorReturn
		// SteamAPICall_t
		public CallbackHandle StopPlaytimeTracking( PublishedFileId_t[] pvecPublishedFileID /*PublishedFileId_t **/, Action<StopPlaytimeTrackingResult_t, bool> CallbackFunction = null /*Action<StopPlaytimeTrackingResult_t, bool>*/ )
		{
			SteamAPICall_t callback = 0;
			var unNumPublishedFileIDs = (uint) pvecPublishedFileID.Length;
			fixed ( PublishedFileId_t* pvecPublishedFileID_ptr = pvecPublishedFileID  )
			{
				callback = platform.ISteamUGC_StopPlaytimeTracking( (IntPtr) pvecPublishedFileID_ptr, unNumPublishedFileIDs );
			}
			
			if ( CallbackFunction == null ) return null;
			if ( callback == 0 ) return null;
			
			return StopPlaytimeTrackingResult_t.CallResult( steamworks, callback, CallbackFunction );
		}
		
		// SteamAPICall_t
		public CallbackHandle StopPlaytimeTrackingForAllItems( Action<StopPlaytimeTrackingResult_t, bool> CallbackFunction = null /*Action<StopPlaytimeTrackingResult_t, bool>*/ )
		{
			SteamAPICall_t callback = 0;
			callback = platform.ISteamUGC_StopPlaytimeTrackingForAllItems();
			
			if ( CallbackFunction == null ) return null;
			if ( callback == 0 ) return null;
			
			return StopPlaytimeTrackingResult_t.CallResult( steamworks, callback, CallbackFunction );
		}
		
		// SteamAPICall_t
		public CallbackHandle SubmitItemUpdate( UGCUpdateHandle_t handle /*UGCUpdateHandle_t*/, string pchChangeNote /*const char **/, Action<SubmitItemUpdateResult_t, bool> CallbackFunction = null /*Action<SubmitItemUpdateResult_t, bool>*/ )
		{
			SteamAPICall_t callback = 0;
			callback = platform.ISteamUGC_SubmitItemUpdate( handle.Value, Utility.GetUtf8Bytes(pchChangeNote) );
			
			if ( CallbackFunction == null ) return null;
			if ( callback == 0 ) return null;
			
			return SubmitItemUpdateResult_t.CallResult( steamworks, callback, CallbackFunction );
		}
		
		// SteamAPICall_t
		public CallbackHandle SubscribeItem( PublishedFileId_t nPublishedFileID /*PublishedFileId_t*/, Action<RemoteStorageSubscribePublishedFileResult_t, bool> CallbackFunction = null /*Action<RemoteStorageSubscribePublishedFileResult_t, bool>*/ )
		{
			SteamAPICall_t callback = 0;
			callback = platform.ISteamUGC_SubscribeItem( nPublishedFileID.Value );
			
			if ( CallbackFunction == null ) return null;
			if ( callback == 0 ) return null;
			
			return RemoteStorageSubscribePublishedFileResult_t.CallResult( steamworks, callback, CallbackFunction );
		}
		
		// void
		public void SuspendDownloads( bool bSuspend /*bool*/ )
		{
			platform.ISteamUGC_SuspendDownloads( bSuspend );
		}
		
		// SteamAPICall_t
		public CallbackHandle UnsubscribeItem( PublishedFileId_t nPublishedFileID /*PublishedFileId_t*/, Action<RemoteStorageUnsubscribePublishedFileResult_t, bool> CallbackFunction = null /*Action<RemoteStorageUnsubscribePublishedFileResult_t, bool>*/ )
		{
			SteamAPICall_t callback = 0;
			callback = platform.ISteamUGC_UnsubscribeItem( nPublishedFileID.Value );
			
			if ( CallbackFunction == null ) return null;
			if ( callback == 0 ) return null;
			
			return RemoteStorageUnsubscribePublishedFileResult_t.CallResult( steamworks, callback, CallbackFunction );
		}
		
		// bool
		public bool UpdateItemPreviewFile( UGCUpdateHandle_t handle /*UGCUpdateHandle_t*/, uint index /*uint32*/, string pszPreviewFile /*const char **/ )
		{
			return platform.ISteamUGC_UpdateItemPreviewFile( handle.Value, index, Utility.GetUtf8Bytes(pszPreviewFile) );
		}
		
		// bool
		public bool UpdateItemPreviewVideo( UGCUpdateHandle_t handle /*UGCUpdateHandle_t*/, uint index /*uint32*/, string pszVideoID /*const char **/ )
		{
			return platform.ISteamUGC_UpdateItemPreviewVideo( handle.Value, index, Utility.GetUtf8Bytes(pszVideoID) );
		}
		
	}
}
