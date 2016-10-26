using System;
using System.Runtime.InteropServices;

namespace SteamNative
{
	public unsafe class SteamRemoteStorage : IDisposable
	{
		//
		// Holds a platform specific implentation
		//
		internal Platform.Interface _pi;
		
		//
		// Constructor decides which implementation to use based on current platform
		//
		public SteamRemoteStorage( IntPtr pointer )
		{
			if ( Platform.IsWindows64 ) _pi = new Platform.Win64( pointer );
			else if ( Platform.IsWindows32 ) _pi = new Platform.Win32( pointer );
			else if ( Platform.IsLinux32 ) _pi = new Platform.Linux32( pointer );
			else if ( Platform.IsLinux64 ) _pi = new Platform.Linux64( pointer );
			else if ( Platform.IsOsx ) _pi = new Platform.Mac( pointer );
		}
		
		//
		// Class is invalid if we don't have a valid implementation
		//
		public bool IsValid{ get{ return _pi != null && _pi.IsValid; } }
		
		//
		// When shutting down clear all the internals to avoid accidental use
		//
		public virtual void Dispose()
		{
			 if ( _pi != null )
			{
				_pi.Dispose();
				_pi = null;
			}
		}
		
		// SteamAPICall_t
		public SteamAPICall_t CommitPublishedFileUpdate( PublishedFileUpdateHandle_t updateHandle /*PublishedFileUpdateHandle_t*/ )
		{
			return _pi.ISteamRemoteStorage_CommitPublishedFileUpdate( updateHandle.Value );
		}
		
		// PublishedFileUpdateHandle_t
		public PublishedFileUpdateHandle_t CreatePublishedFileUpdateRequest( PublishedFileId_t unPublishedFileId /*PublishedFileId_t*/ )
		{
			return _pi.ISteamRemoteStorage_CreatePublishedFileUpdateRequest( unPublishedFileId.Value );
		}
		
		// SteamAPICall_t
		public SteamAPICall_t DeletePublishedFile( PublishedFileId_t unPublishedFileId /*PublishedFileId_t*/ )
		{
			return _pi.ISteamRemoteStorage_DeletePublishedFile( unPublishedFileId.Value );
		}
		
		// SteamAPICall_t
		public SteamAPICall_t EnumeratePublishedFilesByUserAction( WorkshopFileAction eAction /*EWorkshopFileAction*/, uint unStartIndex /*uint32*/ )
		{
			return _pi.ISteamRemoteStorage_EnumeratePublishedFilesByUserAction( eAction, unStartIndex );
		}
		
		// SteamAPICall_t
		public SteamAPICall_t EnumeratePublishedWorkshopFiles( WorkshopEnumerationType eEnumerationType /*EWorkshopEnumerationType*/, uint unStartIndex /*uint32*/, uint unCount /*uint32*/, uint unDays /*uint32*/, IntPtr pTags /*struct SteamParamStringArray_t **/, IntPtr pUserTags /*struct SteamParamStringArray_t **/ )
		{
			return _pi.ISteamRemoteStorage_EnumeratePublishedWorkshopFiles( eEnumerationType, unStartIndex, unCount, unDays, (IntPtr) pTags, (IntPtr) pUserTags );
		}
		
		// SteamAPICall_t
		public SteamAPICall_t EnumerateUserPublishedFiles( uint unStartIndex /*uint32*/ )
		{
			return _pi.ISteamRemoteStorage_EnumerateUserPublishedFiles( unStartIndex );
		}
		
		// SteamAPICall_t
		public SteamAPICall_t EnumerateUserSharedWorkshopFiles( CSteamID steamId /*class CSteamID*/, uint unStartIndex /*uint32*/, IntPtr pRequiredTags /*struct SteamParamStringArray_t **/, IntPtr pExcludedTags /*struct SteamParamStringArray_t **/ )
		{
			return _pi.ISteamRemoteStorage_EnumerateUserSharedWorkshopFiles( steamId.Value, unStartIndex, (IntPtr) pRequiredTags, (IntPtr) pExcludedTags );
		}
		
		// SteamAPICall_t
		public SteamAPICall_t EnumerateUserSubscribedFiles( uint unStartIndex /*uint32*/ )
		{
			return _pi.ISteamRemoteStorage_EnumerateUserSubscribedFiles( unStartIndex );
		}
		
		// bool
		public bool FileDelete( string pchFile /*const char **/ )
		{
			return _pi.ISteamRemoteStorage_FileDelete( pchFile );
		}
		
		// bool
		public bool FileExists( string pchFile /*const char **/ )
		{
			return _pi.ISteamRemoteStorage_FileExists( pchFile );
		}
		
		// bool
		public bool FileForget( string pchFile /*const char **/ )
		{
			return _pi.ISteamRemoteStorage_FileForget( pchFile );
		}
		
		// bool
		public bool FilePersisted( string pchFile /*const char **/ )
		{
			return _pi.ISteamRemoteStorage_FilePersisted( pchFile );
		}
		
		// int
		public int FileRead( string pchFile /*const char **/, IntPtr pvData /*void **/, int cubDataToRead /*int32*/ )
		{
			return _pi.ISteamRemoteStorage_FileRead( pchFile, (IntPtr) pvData, cubDataToRead );
		}
		
		// SteamAPICall_t
		public SteamAPICall_t FileReadAsync( string pchFile /*const char **/, uint nOffset /*uint32*/, uint cubToRead /*uint32*/ )
		{
			return _pi.ISteamRemoteStorage_FileReadAsync( pchFile, nOffset, cubToRead );
		}
		
		// bool
		public bool FileReadAsyncComplete( SteamAPICall_t hReadCall /*SteamAPICall_t*/, IntPtr pvBuffer /*void **/, uint cubToRead /*uint32*/ )
		{
			return _pi.ISteamRemoteStorage_FileReadAsyncComplete( hReadCall.Value, (IntPtr) pvBuffer, cubToRead );
		}
		
		// SteamAPICall_t
		public SteamAPICall_t FileShare( string pchFile /*const char **/ )
		{
			return _pi.ISteamRemoteStorage_FileShare( pchFile );
		}
		
		// bool
		public bool FileWrite( string pchFile /*const char **/, IntPtr pvData /*const void **/, int cubData /*int32*/ )
		{
			return _pi.ISteamRemoteStorage_FileWrite( pchFile, (IntPtr) pvData, cubData );
		}
		
		// SteamAPICall_t
		public SteamAPICall_t FileWriteAsync( string pchFile /*const char **/, IntPtr pvData /*const void **/, uint cubData /*uint32*/ )
		{
			return _pi.ISteamRemoteStorage_FileWriteAsync( pchFile, (IntPtr) pvData, cubData );
		}
		
		// bool
		public bool FileWriteStreamCancel( UGCFileWriteStreamHandle_t writeHandle /*UGCFileWriteStreamHandle_t*/ )
		{
			return _pi.ISteamRemoteStorage_FileWriteStreamCancel( writeHandle.Value );
		}
		
		// bool
		public bool FileWriteStreamClose( UGCFileWriteStreamHandle_t writeHandle /*UGCFileWriteStreamHandle_t*/ )
		{
			return _pi.ISteamRemoteStorage_FileWriteStreamClose( writeHandle.Value );
		}
		
		// UGCFileWriteStreamHandle_t
		public UGCFileWriteStreamHandle_t FileWriteStreamOpen( string pchFile /*const char **/ )
		{
			return _pi.ISteamRemoteStorage_FileWriteStreamOpen( pchFile );
		}
		
		// bool
		public bool FileWriteStreamWriteChunk( UGCFileWriteStreamHandle_t writeHandle /*UGCFileWriteStreamHandle_t*/, IntPtr pvData /*const void **/, int cubData /*int32*/ )
		{
			return _pi.ISteamRemoteStorage_FileWriteStreamWriteChunk( writeHandle.Value, (IntPtr) pvData, cubData );
		}
		
		// int
		public int GetCachedUGCCount()
		{
			return _pi.ISteamRemoteStorage_GetCachedUGCCount();
		}
		
		// UGCHandle_t
		public UGCHandle_t GetCachedUGCHandle( int iCachedContent /*int32*/ )
		{
			return _pi.ISteamRemoteStorage_GetCachedUGCHandle( iCachedContent );
		}
		
		// int
		public int GetFileCount()
		{
			return _pi.ISteamRemoteStorage_GetFileCount();
		}
		
		// string
		// with: Detect_StringReturn
		public string GetFileNameAndSize( int iFile /*int*/, IntPtr pnFileSizeInBytes /*int32 **/ )
		{
			IntPtr string_pointer;
			string_pointer = _pi.ISteamRemoteStorage_GetFileNameAndSize( iFile, (IntPtr) pnFileSizeInBytes );
			return Marshal.PtrToStringAnsi( string_pointer );
		}
		
		// int
		public int GetFileSize( string pchFile /*const char **/ )
		{
			return _pi.ISteamRemoteStorage_GetFileSize( pchFile );
		}
		
		// long
		public long GetFileTimestamp( string pchFile /*const char **/ )
		{
			return _pi.ISteamRemoteStorage_GetFileTimestamp( pchFile );
		}
		
		// SteamAPICall_t
		public SteamAPICall_t GetPublishedFileDetails( PublishedFileId_t unPublishedFileId /*PublishedFileId_t*/, uint unMaxSecondsOld /*uint32*/ )
		{
			return _pi.ISteamRemoteStorage_GetPublishedFileDetails( unPublishedFileId.Value, unMaxSecondsOld );
		}
		
		// SteamAPICall_t
		public SteamAPICall_t GetPublishedItemVoteDetails( PublishedFileId_t unPublishedFileId /*PublishedFileId_t*/ )
		{
			return _pi.ISteamRemoteStorage_GetPublishedItemVoteDetails( unPublishedFileId.Value );
		}
		
		// bool
		public bool GetQuota( IntPtr pnTotalBytes /*int32 **/, IntPtr puAvailableBytes /*int32 **/ )
		{
			return _pi.ISteamRemoteStorage_GetQuota( (IntPtr) pnTotalBytes, (IntPtr) puAvailableBytes );
		}
		
		// RemoteStoragePlatform
		public RemoteStoragePlatform GetSyncPlatforms( string pchFile /*const char **/ )
		{
			return _pi.ISteamRemoteStorage_GetSyncPlatforms( pchFile );
		}
		
		// bool
		// with: Detect_StringFetch False
		public bool GetUGCDetails( UGCHandle_t hContent /*UGCHandle_t*/, ref AppId_t pnAppID /*AppId_t **/, out string ppchName /*char ***/, out CSteamID pSteamIDOwner /*class CSteamID **/ )
		{
			bool bSuccess = default( bool );
			ppchName = string.Empty;
			System.Text.StringBuilder ppchName_sb = new System.Text.StringBuilder( 4096 );
			int pnFileSizeInBytes = 4096;
			bSuccess = _pi.ISteamRemoteStorage_GetUGCDetails( hContent.Value, ref pnAppID.Value, ppchName_sb, (IntPtr) pnFileSizeInBytes, out pSteamIDOwner.Value );
			if ( !bSuccess ) return bSuccess;
			ppchName = ppchName_sb.ToString();
			return bSuccess;
		}
		
		// bool
		public bool GetUGCDownloadProgress( UGCHandle_t hContent /*UGCHandle_t*/, out int pnBytesDownloaded /*int32 **/, out int pnBytesExpected /*int32 **/ )
		{
			return _pi.ISteamRemoteStorage_GetUGCDownloadProgress( hContent.Value, out pnBytesDownloaded, out pnBytesExpected );
		}
		
		// SteamAPICall_t
		public SteamAPICall_t GetUserPublishedItemVoteDetails( PublishedFileId_t unPublishedFileId /*PublishedFileId_t*/ )
		{
			return _pi.ISteamRemoteStorage_GetUserPublishedItemVoteDetails( unPublishedFileId.Value );
		}
		
		// bool
		public bool IsCloudEnabledForAccount()
		{
			return _pi.ISteamRemoteStorage_IsCloudEnabledForAccount();
		}
		
		// bool
		public bool IsCloudEnabledForApp()
		{
			return _pi.ISteamRemoteStorage_IsCloudEnabledForApp();
		}
		
		// SteamAPICall_t
		public SteamAPICall_t PublishVideo( WorkshopVideoProvider eVideoProvider /*EWorkshopVideoProvider*/, string pchVideoAccount /*const char **/, string pchVideoIdentifier /*const char **/, string pchPreviewFile /*const char **/, AppId_t nConsumerAppId /*AppId_t*/, string pchTitle /*const char **/, string pchDescription /*const char **/, RemoteStoragePublishedFileVisibility eVisibility /*ERemoteStoragePublishedFileVisibility*/, IntPtr pTags /*struct SteamParamStringArray_t **/ )
		{
			return _pi.ISteamRemoteStorage_PublishVideo( eVideoProvider, pchVideoAccount, pchVideoIdentifier, pchPreviewFile, nConsumerAppId.Value, pchTitle, pchDescription, eVisibility, (IntPtr) pTags );
		}
		
		// SteamAPICall_t
		public SteamAPICall_t PublishWorkshopFile( string pchFile /*const char **/, string pchPreviewFile /*const char **/, AppId_t nConsumerAppId /*AppId_t*/, string pchTitle /*const char **/, string pchDescription /*const char **/, RemoteStoragePublishedFileVisibility eVisibility /*ERemoteStoragePublishedFileVisibility*/, IntPtr pTags /*struct SteamParamStringArray_t **/, WorkshopFileType eWorkshopFileType /*EWorkshopFileType*/ )
		{
			return _pi.ISteamRemoteStorage_PublishWorkshopFile( pchFile, pchPreviewFile, nConsumerAppId.Value, pchTitle, pchDescription, eVisibility, (IntPtr) pTags, eWorkshopFileType );
		}
		
		// void
		public void SetCloudEnabledForApp( bool bEnabled /*bool*/ )
		{
			_pi.ISteamRemoteStorage_SetCloudEnabledForApp( bEnabled );
		}
		
		// bool
		public bool SetSyncPlatforms( string pchFile /*const char **/, RemoteStoragePlatform eRemoteStoragePlatform /*ERemoteStoragePlatform*/ )
		{
			return _pi.ISteamRemoteStorage_SetSyncPlatforms( pchFile, eRemoteStoragePlatform );
		}
		
		// SteamAPICall_t
		public SteamAPICall_t SetUserPublishedFileAction( PublishedFileId_t unPublishedFileId /*PublishedFileId_t*/, WorkshopFileAction eAction /*EWorkshopFileAction*/ )
		{
			return _pi.ISteamRemoteStorage_SetUserPublishedFileAction( unPublishedFileId.Value, eAction );
		}
		
		// SteamAPICall_t
		public SteamAPICall_t SubscribePublishedFile( PublishedFileId_t unPublishedFileId /*PublishedFileId_t*/ )
		{
			return _pi.ISteamRemoteStorage_SubscribePublishedFile( unPublishedFileId.Value );
		}
		
		// SteamAPICall_t
		public SteamAPICall_t UGCDownload( UGCHandle_t hContent /*UGCHandle_t*/, uint unPriority /*uint32*/ )
		{
			return _pi.ISteamRemoteStorage_UGCDownload( hContent.Value, unPriority );
		}
		
		// SteamAPICall_t
		public SteamAPICall_t UGCDownloadToLocation( UGCHandle_t hContent /*UGCHandle_t*/, string pchLocation /*const char **/, uint unPriority /*uint32*/ )
		{
			return _pi.ISteamRemoteStorage_UGCDownloadToLocation( hContent.Value, pchLocation, unPriority );
		}
		
		// int
		public int UGCRead( UGCHandle_t hContent /*UGCHandle_t*/, IntPtr pvData /*void **/, int cubDataToRead /*int32*/, uint cOffset /*uint32*/, UGCReadAction eAction /*EUGCReadAction*/ )
		{
			return _pi.ISteamRemoteStorage_UGCRead( hContent.Value, (IntPtr) pvData, cubDataToRead, cOffset, eAction );
		}
		
		// SteamAPICall_t
		public SteamAPICall_t UnsubscribePublishedFile( PublishedFileId_t unPublishedFileId /*PublishedFileId_t*/ )
		{
			return _pi.ISteamRemoteStorage_UnsubscribePublishedFile( unPublishedFileId.Value );
		}
		
		// bool
		public bool UpdatePublishedFileDescription( PublishedFileUpdateHandle_t updateHandle /*PublishedFileUpdateHandle_t*/, string pchDescription /*const char **/ )
		{
			return _pi.ISteamRemoteStorage_UpdatePublishedFileDescription( updateHandle.Value, pchDescription );
		}
		
		// bool
		public bool UpdatePublishedFileFile( PublishedFileUpdateHandle_t updateHandle /*PublishedFileUpdateHandle_t*/, string pchFile /*const char **/ )
		{
			return _pi.ISteamRemoteStorage_UpdatePublishedFileFile( updateHandle.Value, pchFile );
		}
		
		// bool
		public bool UpdatePublishedFilePreviewFile( PublishedFileUpdateHandle_t updateHandle /*PublishedFileUpdateHandle_t*/, string pchPreviewFile /*const char **/ )
		{
			return _pi.ISteamRemoteStorage_UpdatePublishedFilePreviewFile( updateHandle.Value, pchPreviewFile );
		}
		
		// bool
		public bool UpdatePublishedFileSetChangeDescription( PublishedFileUpdateHandle_t updateHandle /*PublishedFileUpdateHandle_t*/, string pchChangeDescription /*const char **/ )
		{
			return _pi.ISteamRemoteStorage_UpdatePublishedFileSetChangeDescription( updateHandle.Value, pchChangeDescription );
		}
		
		// bool
		public bool UpdatePublishedFileTags( PublishedFileUpdateHandle_t updateHandle /*PublishedFileUpdateHandle_t*/, IntPtr pTags /*struct SteamParamStringArray_t **/ )
		{
			return _pi.ISteamRemoteStorage_UpdatePublishedFileTags( updateHandle.Value, (IntPtr) pTags );
		}
		
		// bool
		public bool UpdatePublishedFileTitle( PublishedFileUpdateHandle_t updateHandle /*PublishedFileUpdateHandle_t*/, string pchTitle /*const char **/ )
		{
			return _pi.ISteamRemoteStorage_UpdatePublishedFileTitle( updateHandle.Value, pchTitle );
		}
		
		// bool
		public bool UpdatePublishedFileVisibility( PublishedFileUpdateHandle_t updateHandle /*PublishedFileUpdateHandle_t*/, RemoteStoragePublishedFileVisibility eVisibility /*ERemoteStoragePublishedFileVisibility*/ )
		{
			return _pi.ISteamRemoteStorage_UpdatePublishedFileVisibility( updateHandle.Value, eVisibility );
		}
		
		// SteamAPICall_t
		public SteamAPICall_t UpdateUserPublishedItemVote( PublishedFileId_t unPublishedFileId /*PublishedFileId_t*/, bool bVoteUp /*bool*/ )
		{
			return _pi.ISteamRemoteStorage_UpdateUserPublishedItemVote( unPublishedFileId.Value, bVoteUp );
		}
		
	}
}
