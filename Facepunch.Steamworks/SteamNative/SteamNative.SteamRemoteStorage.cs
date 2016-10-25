using System;
using System.Runtime.InteropServices;

namespace SteamNative
{
	public unsafe class SteamRemoteStorage
	{
		internal IntPtr _ptr;
		
		public SteamRemoteStorage( IntPtr pointer )
		{
			_ptr = pointer;
		}
		
		
		// SteamAPICall_t
		public SteamAPICall_t CommitPublishedFileUpdate( PublishedFileUpdateHandle_t updateHandle /*PublishedFileUpdateHandle_t*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamRemoteStorage.CommitPublishedFileUpdate( _ptr, updateHandle );
			else return Platform.Win64.ISteamRemoteStorage.CommitPublishedFileUpdate( _ptr, updateHandle );
		}
		
		// PublishedFileUpdateHandle_t
		public PublishedFileUpdateHandle_t CreatePublishedFileUpdateRequest( PublishedFileId_t unPublishedFileId /*PublishedFileId_t*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamRemoteStorage.CreatePublishedFileUpdateRequest( _ptr, unPublishedFileId );
			else return Platform.Win64.ISteamRemoteStorage.CreatePublishedFileUpdateRequest( _ptr, unPublishedFileId );
		}
		
		// SteamAPICall_t
		public SteamAPICall_t DeletePublishedFile( PublishedFileId_t unPublishedFileId /*PublishedFileId_t*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamRemoteStorage.DeletePublishedFile( _ptr, unPublishedFileId );
			else return Platform.Win64.ISteamRemoteStorage.DeletePublishedFile( _ptr, unPublishedFileId );
		}
		
		// SteamAPICall_t
		public SteamAPICall_t EnumeratePublishedFilesByUserAction( WorkshopFileAction eAction /*EWorkshopFileAction*/, uint unStartIndex /*uint32*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamRemoteStorage.EnumeratePublishedFilesByUserAction( _ptr, eAction, unStartIndex );
			else return Platform.Win64.ISteamRemoteStorage.EnumeratePublishedFilesByUserAction( _ptr, eAction, unStartIndex );
		}
		
		// SteamAPICall_t
		public SteamAPICall_t EnumeratePublishedWorkshopFiles( WorkshopEnumerationType eEnumerationType /*EWorkshopEnumerationType*/, uint unStartIndex /*uint32*/, uint unCount /*uint32*/, uint unDays /*uint32*/, IntPtr pTags /*struct SteamParamStringArray_t **/, IntPtr pUserTags /*struct SteamParamStringArray_t **/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamRemoteStorage.EnumeratePublishedWorkshopFiles( _ptr, eEnumerationType, unStartIndex, unCount, unDays, (IntPtr) pTags, (IntPtr) pUserTags );
			else return Platform.Win64.ISteamRemoteStorage.EnumeratePublishedWorkshopFiles( _ptr, eEnumerationType, unStartIndex, unCount, unDays, (IntPtr) pTags, (IntPtr) pUserTags );
		}
		
		// SteamAPICall_t
		public SteamAPICall_t EnumerateUserPublishedFiles( uint unStartIndex /*uint32*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamRemoteStorage.EnumerateUserPublishedFiles( _ptr, unStartIndex );
			else return Platform.Win64.ISteamRemoteStorage.EnumerateUserPublishedFiles( _ptr, unStartIndex );
		}
		
		// SteamAPICall_t
		public SteamAPICall_t EnumerateUserSharedWorkshopFiles( CSteamID steamId /*class CSteamID*/, uint unStartIndex /*uint32*/, IntPtr pRequiredTags /*struct SteamParamStringArray_t **/, IntPtr pExcludedTags /*struct SteamParamStringArray_t **/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamRemoteStorage.EnumerateUserSharedWorkshopFiles( _ptr, steamId, unStartIndex, (IntPtr) pRequiredTags, (IntPtr) pExcludedTags );
			else return Platform.Win64.ISteamRemoteStorage.EnumerateUserSharedWorkshopFiles( _ptr, steamId, unStartIndex, (IntPtr) pRequiredTags, (IntPtr) pExcludedTags );
		}
		
		// SteamAPICall_t
		public SteamAPICall_t EnumerateUserSubscribedFiles( uint unStartIndex /*uint32*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamRemoteStorage.EnumerateUserSubscribedFiles( _ptr, unStartIndex );
			else return Platform.Win64.ISteamRemoteStorage.EnumerateUserSubscribedFiles( _ptr, unStartIndex );
		}
		
		// bool
		public bool FileDelete( string pchFile /*const char **/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamRemoteStorage.FileDelete( _ptr, pchFile );
			else return Platform.Win64.ISteamRemoteStorage.FileDelete( _ptr, pchFile );
		}
		
		// bool
		public bool FileExists( string pchFile /*const char **/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamRemoteStorage.FileExists( _ptr, pchFile );
			else return Platform.Win64.ISteamRemoteStorage.FileExists( _ptr, pchFile );
		}
		
		// bool
		public bool FileForget( string pchFile /*const char **/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamRemoteStorage.FileForget( _ptr, pchFile );
			else return Platform.Win64.ISteamRemoteStorage.FileForget( _ptr, pchFile );
		}
		
		// bool
		public bool FilePersisted( string pchFile /*const char **/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamRemoteStorage.FilePersisted( _ptr, pchFile );
			else return Platform.Win64.ISteamRemoteStorage.FilePersisted( _ptr, pchFile );
		}
		
		// int
		public int FileRead( string pchFile /*const char **/, IntPtr pvData /*void **/, int cubDataToRead /*int32*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamRemoteStorage.FileRead( _ptr, pchFile, (IntPtr) pvData, cubDataToRead );
			else return Platform.Win64.ISteamRemoteStorage.FileRead( _ptr, pchFile, (IntPtr) pvData, cubDataToRead );
		}
		
		// SteamAPICall_t
		public SteamAPICall_t FileReadAsync( string pchFile /*const char **/, uint nOffset /*uint32*/, uint cubToRead /*uint32*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamRemoteStorage.FileReadAsync( _ptr, pchFile, nOffset, cubToRead );
			else return Platform.Win64.ISteamRemoteStorage.FileReadAsync( _ptr, pchFile, nOffset, cubToRead );
		}
		
		// bool
		public bool FileReadAsyncComplete( SteamAPICall_t hReadCall /*SteamAPICall_t*/, IntPtr pvBuffer /*void **/, uint cubToRead /*uint32*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamRemoteStorage.FileReadAsyncComplete( _ptr, hReadCall, (IntPtr) pvBuffer, cubToRead );
			else return Platform.Win64.ISteamRemoteStorage.FileReadAsyncComplete( _ptr, hReadCall, (IntPtr) pvBuffer, cubToRead );
		}
		
		// SteamAPICall_t
		public SteamAPICall_t FileShare( string pchFile /*const char **/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamRemoteStorage.FileShare( _ptr, pchFile );
			else return Platform.Win64.ISteamRemoteStorage.FileShare( _ptr, pchFile );
		}
		
		// bool
		public bool FileWrite( string pchFile /*const char **/, IntPtr pvData /*const void **/, int cubData /*int32*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamRemoteStorage.FileWrite( _ptr, pchFile, (IntPtr) pvData, cubData );
			else return Platform.Win64.ISteamRemoteStorage.FileWrite( _ptr, pchFile, (IntPtr) pvData, cubData );
		}
		
		// SteamAPICall_t
		public SteamAPICall_t FileWriteAsync( string pchFile /*const char **/, IntPtr pvData /*const void **/, uint cubData /*uint32*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamRemoteStorage.FileWriteAsync( _ptr, pchFile, (IntPtr) pvData, cubData );
			else return Platform.Win64.ISteamRemoteStorage.FileWriteAsync( _ptr, pchFile, (IntPtr) pvData, cubData );
		}
		
		// bool
		public bool FileWriteStreamCancel( UGCFileWriteStreamHandle_t writeHandle /*UGCFileWriteStreamHandle_t*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamRemoteStorage.FileWriteStreamCancel( _ptr, writeHandle );
			else return Platform.Win64.ISteamRemoteStorage.FileWriteStreamCancel( _ptr, writeHandle );
		}
		
		// bool
		public bool FileWriteStreamClose( UGCFileWriteStreamHandle_t writeHandle /*UGCFileWriteStreamHandle_t*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamRemoteStorage.FileWriteStreamClose( _ptr, writeHandle );
			else return Platform.Win64.ISteamRemoteStorage.FileWriteStreamClose( _ptr, writeHandle );
		}
		
		// UGCFileWriteStreamHandle_t
		public UGCFileWriteStreamHandle_t FileWriteStreamOpen( string pchFile /*const char **/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamRemoteStorage.FileWriteStreamOpen( _ptr, pchFile );
			else return Platform.Win64.ISteamRemoteStorage.FileWriteStreamOpen( _ptr, pchFile );
		}
		
		// bool
		public bool FileWriteStreamWriteChunk( UGCFileWriteStreamHandle_t writeHandle /*UGCFileWriteStreamHandle_t*/, IntPtr pvData /*const void **/, int cubData /*int32*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamRemoteStorage.FileWriteStreamWriteChunk( _ptr, writeHandle, (IntPtr) pvData, cubData );
			else return Platform.Win64.ISteamRemoteStorage.FileWriteStreamWriteChunk( _ptr, writeHandle, (IntPtr) pvData, cubData );
		}
		
		// int
		public int GetCachedUGCCount()
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamRemoteStorage.GetCachedUGCCount( _ptr );
			else return Platform.Win64.ISteamRemoteStorage.GetCachedUGCCount( _ptr );
		}
		
		// UGCHandle_t
		public UGCHandle_t GetCachedUGCHandle( int iCachedContent /*int32*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamRemoteStorage.GetCachedUGCHandle( _ptr, iCachedContent );
			else return Platform.Win64.ISteamRemoteStorage.GetCachedUGCHandle( _ptr, iCachedContent );
		}
		
		// int
		public int GetFileCount()
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamRemoteStorage.GetFileCount( _ptr );
			else return Platform.Win64.ISteamRemoteStorage.GetFileCount( _ptr );
		}
		
		// string
		// with: Detect_StringReturn
		public string GetFileNameAndSize( int iFile /*int*/, IntPtr pnFileSizeInBytes /*int32 **/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			IntPtr string_pointer;
			if ( Platform.IsWindows32 ) string_pointer = Platform.Win32.ISteamRemoteStorage.GetFileNameAndSize( _ptr, iFile, (IntPtr) pnFileSizeInBytes );
			else string_pointer = Platform.Win64.ISteamRemoteStorage.GetFileNameAndSize( _ptr, iFile, (IntPtr) pnFileSizeInBytes );
			return Marshal.PtrToStringAnsi( string_pointer );
		}
		
		// int
		public int GetFileSize( string pchFile /*const char **/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamRemoteStorage.GetFileSize( _ptr, pchFile );
			else return Platform.Win64.ISteamRemoteStorage.GetFileSize( _ptr, pchFile );
		}
		
		// long
		public long GetFileTimestamp( string pchFile /*const char **/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamRemoteStorage.GetFileTimestamp( _ptr, pchFile );
			else return Platform.Win64.ISteamRemoteStorage.GetFileTimestamp( _ptr, pchFile );
		}
		
		// SteamAPICall_t
		public SteamAPICall_t GetPublishedFileDetails( PublishedFileId_t unPublishedFileId /*PublishedFileId_t*/, uint unMaxSecondsOld /*uint32*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamRemoteStorage.GetPublishedFileDetails( _ptr, unPublishedFileId, unMaxSecondsOld );
			else return Platform.Win64.ISteamRemoteStorage.GetPublishedFileDetails( _ptr, unPublishedFileId, unMaxSecondsOld );
		}
		
		// SteamAPICall_t
		public SteamAPICall_t GetPublishedItemVoteDetails( PublishedFileId_t unPublishedFileId /*PublishedFileId_t*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamRemoteStorage.GetPublishedItemVoteDetails( _ptr, unPublishedFileId );
			else return Platform.Win64.ISteamRemoteStorage.GetPublishedItemVoteDetails( _ptr, unPublishedFileId );
		}
		
		// bool
		public bool GetQuota( IntPtr pnTotalBytes /*int32 **/, IntPtr puAvailableBytes /*int32 **/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamRemoteStorage.GetQuota( _ptr, (IntPtr) pnTotalBytes, (IntPtr) puAvailableBytes );
			else return Platform.Win64.ISteamRemoteStorage.GetQuota( _ptr, (IntPtr) pnTotalBytes, (IntPtr) puAvailableBytes );
		}
		
		// RemoteStoragePlatform
		public RemoteStoragePlatform GetSyncPlatforms( string pchFile /*const char **/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamRemoteStorage.GetSyncPlatforms( _ptr, pchFile );
			else return Platform.Win64.ISteamRemoteStorage.GetSyncPlatforms( _ptr, pchFile );
		}
		
		// bool
		// with: Detect_StringFetch False
		public bool GetUGCDetails( UGCHandle_t hContent /*UGCHandle_t*/, ref AppId_t pnAppID /*AppId_t **/, out string ppchName /*char ***/, out CSteamID pSteamIDOwner /*class CSteamID **/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			bool bSuccess = default( bool );
			ppchName = string.Empty;
			System.Text.StringBuilder ppchName_sb = new System.Text.StringBuilder( 4096 );
			int pnFileSizeInBytes = 4096;
			if ( Platform.IsWindows32 ) bSuccess = Platform.Win32.ISteamRemoteStorage.GetUGCDetails( _ptr, hContent, ref pnAppID, ppchName_sb, (IntPtr) pnFileSizeInBytes, out pSteamIDOwner );
			else bSuccess = Platform.Win64.ISteamRemoteStorage.GetUGCDetails( _ptr, hContent, ref pnAppID, ppchName_sb, (IntPtr) pnFileSizeInBytes, out pSteamIDOwner );
			if ( !bSuccess ) return bSuccess;
			ppchName = ppchName_sb.ToString();
			return bSuccess;
		}
		
		// bool
		public bool GetUGCDownloadProgress( UGCHandle_t hContent /*UGCHandle_t*/, out int pnBytesDownloaded /*int32 **/, out int pnBytesExpected /*int32 **/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamRemoteStorage.GetUGCDownloadProgress( _ptr, hContent, out pnBytesDownloaded, out pnBytesExpected );
			else return Platform.Win64.ISteamRemoteStorage.GetUGCDownloadProgress( _ptr, hContent, out pnBytesDownloaded, out pnBytesExpected );
		}
		
		// SteamAPICall_t
		public SteamAPICall_t GetUserPublishedItemVoteDetails( PublishedFileId_t unPublishedFileId /*PublishedFileId_t*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamRemoteStorage.GetUserPublishedItemVoteDetails( _ptr, unPublishedFileId );
			else return Platform.Win64.ISteamRemoteStorage.GetUserPublishedItemVoteDetails( _ptr, unPublishedFileId );
		}
		
		// bool
		public bool IsCloudEnabledForAccount()
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamRemoteStorage.IsCloudEnabledForAccount( _ptr );
			else return Platform.Win64.ISteamRemoteStorage.IsCloudEnabledForAccount( _ptr );
		}
		
		// bool
		public bool IsCloudEnabledForApp()
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamRemoteStorage.IsCloudEnabledForApp( _ptr );
			else return Platform.Win64.ISteamRemoteStorage.IsCloudEnabledForApp( _ptr );
		}
		
		// SteamAPICall_t
		public SteamAPICall_t PublishVideo( WorkshopVideoProvider eVideoProvider /*EWorkshopVideoProvider*/, string pchVideoAccount /*const char **/, string pchVideoIdentifier /*const char **/, string pchPreviewFile /*const char **/, AppId_t nConsumerAppId /*AppId_t*/, string pchTitle /*const char **/, string pchDescription /*const char **/, RemoteStoragePublishedFileVisibility eVisibility /*ERemoteStoragePublishedFileVisibility*/, IntPtr pTags /*struct SteamParamStringArray_t **/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamRemoteStorage.PublishVideo( _ptr, eVideoProvider, pchVideoAccount, pchVideoIdentifier, pchPreviewFile, nConsumerAppId, pchTitle, pchDescription, eVisibility, (IntPtr) pTags );
			else return Platform.Win64.ISteamRemoteStorage.PublishVideo( _ptr, eVideoProvider, pchVideoAccount, pchVideoIdentifier, pchPreviewFile, nConsumerAppId, pchTitle, pchDescription, eVisibility, (IntPtr) pTags );
		}
		
		// SteamAPICall_t
		public SteamAPICall_t PublishWorkshopFile( string pchFile /*const char **/, string pchPreviewFile /*const char **/, AppId_t nConsumerAppId /*AppId_t*/, string pchTitle /*const char **/, string pchDescription /*const char **/, RemoteStoragePublishedFileVisibility eVisibility /*ERemoteStoragePublishedFileVisibility*/, IntPtr pTags /*struct SteamParamStringArray_t **/, WorkshopFileType eWorkshopFileType /*EWorkshopFileType*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamRemoteStorage.PublishWorkshopFile( _ptr, pchFile, pchPreviewFile, nConsumerAppId, pchTitle, pchDescription, eVisibility, (IntPtr) pTags, eWorkshopFileType );
			else return Platform.Win64.ISteamRemoteStorage.PublishWorkshopFile( _ptr, pchFile, pchPreviewFile, nConsumerAppId, pchTitle, pchDescription, eVisibility, (IntPtr) pTags, eWorkshopFileType );
		}
		
		// void
		public void SetCloudEnabledForApp( bool bEnabled /*bool*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) Platform.Win32.ISteamRemoteStorage.SetCloudEnabledForApp( _ptr, bEnabled );
			else Platform.Win64.ISteamRemoteStorage.SetCloudEnabledForApp( _ptr, bEnabled );
		}
		
		// bool
		public bool SetSyncPlatforms( string pchFile /*const char **/, RemoteStoragePlatform eRemoteStoragePlatform /*ERemoteStoragePlatform*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamRemoteStorage.SetSyncPlatforms( _ptr, pchFile, eRemoteStoragePlatform );
			else return Platform.Win64.ISteamRemoteStorage.SetSyncPlatforms( _ptr, pchFile, eRemoteStoragePlatform );
		}
		
		// SteamAPICall_t
		public SteamAPICall_t SetUserPublishedFileAction( PublishedFileId_t unPublishedFileId /*PublishedFileId_t*/, WorkshopFileAction eAction /*EWorkshopFileAction*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamRemoteStorage.SetUserPublishedFileAction( _ptr, unPublishedFileId, eAction );
			else return Platform.Win64.ISteamRemoteStorage.SetUserPublishedFileAction( _ptr, unPublishedFileId, eAction );
		}
		
		// SteamAPICall_t
		public SteamAPICall_t SubscribePublishedFile( PublishedFileId_t unPublishedFileId /*PublishedFileId_t*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamRemoteStorage.SubscribePublishedFile( _ptr, unPublishedFileId );
			else return Platform.Win64.ISteamRemoteStorage.SubscribePublishedFile( _ptr, unPublishedFileId );
		}
		
		// SteamAPICall_t
		public SteamAPICall_t UGCDownload( UGCHandle_t hContent /*UGCHandle_t*/, uint unPriority /*uint32*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamRemoteStorage.UGCDownload( _ptr, hContent, unPriority );
			else return Platform.Win64.ISteamRemoteStorage.UGCDownload( _ptr, hContent, unPriority );
		}
		
		// SteamAPICall_t
		public SteamAPICall_t UGCDownloadToLocation( UGCHandle_t hContent /*UGCHandle_t*/, string pchLocation /*const char **/, uint unPriority /*uint32*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamRemoteStorage.UGCDownloadToLocation( _ptr, hContent, pchLocation, unPriority );
			else return Platform.Win64.ISteamRemoteStorage.UGCDownloadToLocation( _ptr, hContent, pchLocation, unPriority );
		}
		
		// int
		public int UGCRead( UGCHandle_t hContent /*UGCHandle_t*/, IntPtr pvData /*void **/, int cubDataToRead /*int32*/, uint cOffset /*uint32*/, UGCReadAction eAction /*EUGCReadAction*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamRemoteStorage.UGCRead( _ptr, hContent, (IntPtr) pvData, cubDataToRead, cOffset, eAction );
			else return Platform.Win64.ISteamRemoteStorage.UGCRead( _ptr, hContent, (IntPtr) pvData, cubDataToRead, cOffset, eAction );
		}
		
		// SteamAPICall_t
		public SteamAPICall_t UnsubscribePublishedFile( PublishedFileId_t unPublishedFileId /*PublishedFileId_t*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamRemoteStorage.UnsubscribePublishedFile( _ptr, unPublishedFileId );
			else return Platform.Win64.ISteamRemoteStorage.UnsubscribePublishedFile( _ptr, unPublishedFileId );
		}
		
		// bool
		public bool UpdatePublishedFileDescription( PublishedFileUpdateHandle_t updateHandle /*PublishedFileUpdateHandle_t*/, string pchDescription /*const char **/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamRemoteStorage.UpdatePublishedFileDescription( _ptr, updateHandle, pchDescription );
			else return Platform.Win64.ISteamRemoteStorage.UpdatePublishedFileDescription( _ptr, updateHandle, pchDescription );
		}
		
		// bool
		public bool UpdatePublishedFileFile( PublishedFileUpdateHandle_t updateHandle /*PublishedFileUpdateHandle_t*/, string pchFile /*const char **/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamRemoteStorage.UpdatePublishedFileFile( _ptr, updateHandle, pchFile );
			else return Platform.Win64.ISteamRemoteStorage.UpdatePublishedFileFile( _ptr, updateHandle, pchFile );
		}
		
		// bool
		public bool UpdatePublishedFilePreviewFile( PublishedFileUpdateHandle_t updateHandle /*PublishedFileUpdateHandle_t*/, string pchPreviewFile /*const char **/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamRemoteStorage.UpdatePublishedFilePreviewFile( _ptr, updateHandle, pchPreviewFile );
			else return Platform.Win64.ISteamRemoteStorage.UpdatePublishedFilePreviewFile( _ptr, updateHandle, pchPreviewFile );
		}
		
		// bool
		public bool UpdatePublishedFileSetChangeDescription( PublishedFileUpdateHandle_t updateHandle /*PublishedFileUpdateHandle_t*/, string pchChangeDescription /*const char **/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamRemoteStorage.UpdatePublishedFileSetChangeDescription( _ptr, updateHandle, pchChangeDescription );
			else return Platform.Win64.ISteamRemoteStorage.UpdatePublishedFileSetChangeDescription( _ptr, updateHandle, pchChangeDescription );
		}
		
		// bool
		public bool UpdatePublishedFileTags( PublishedFileUpdateHandle_t updateHandle /*PublishedFileUpdateHandle_t*/, IntPtr pTags /*struct SteamParamStringArray_t **/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamRemoteStorage.UpdatePublishedFileTags( _ptr, updateHandle, (IntPtr) pTags );
			else return Platform.Win64.ISteamRemoteStorage.UpdatePublishedFileTags( _ptr, updateHandle, (IntPtr) pTags );
		}
		
		// bool
		public bool UpdatePublishedFileTitle( PublishedFileUpdateHandle_t updateHandle /*PublishedFileUpdateHandle_t*/, string pchTitle /*const char **/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamRemoteStorage.UpdatePublishedFileTitle( _ptr, updateHandle, pchTitle );
			else return Platform.Win64.ISteamRemoteStorage.UpdatePublishedFileTitle( _ptr, updateHandle, pchTitle );
		}
		
		// bool
		public bool UpdatePublishedFileVisibility( PublishedFileUpdateHandle_t updateHandle /*PublishedFileUpdateHandle_t*/, RemoteStoragePublishedFileVisibility eVisibility /*ERemoteStoragePublishedFileVisibility*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamRemoteStorage.UpdatePublishedFileVisibility( _ptr, updateHandle, eVisibility );
			else return Platform.Win64.ISteamRemoteStorage.UpdatePublishedFileVisibility( _ptr, updateHandle, eVisibility );
		}
		
		// SteamAPICall_t
		public SteamAPICall_t UpdateUserPublishedItemVote( PublishedFileId_t unPublishedFileId /*PublishedFileId_t*/, bool bVoteUp /*bool*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamRemoteStorage.UpdateUserPublishedItemVote( _ptr, unPublishedFileId, bVoteUp );
			else return Platform.Win64.ISteamRemoteStorage.UpdateUserPublishedItemVote( _ptr, unPublishedFileId, bVoteUp );
		}
		
	}
}
