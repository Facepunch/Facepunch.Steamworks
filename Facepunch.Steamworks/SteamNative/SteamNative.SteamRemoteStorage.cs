using System;
using System.Runtime.InteropServices;
using System.Linq;

namespace SteamNative
{
	internal unsafe class SteamRemoteStorage : IDisposable
	{
		//
		// Holds a platform specific implentation
		//
		internal Platform.Interface platform;
		internal Facepunch.Steamworks.BaseSteamworks steamworks;
		
		//
		// Constructor decides which implementation to use based on current platform
		//
		internal SteamRemoteStorage( Facepunch.Steamworks.BaseSteamworks steamworks, IntPtr pointer )
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
		public CallbackHandle CommitPublishedFileUpdate( PublishedFileUpdateHandle_t updateHandle /*PublishedFileUpdateHandle_t*/, Action<RemoteStorageUpdatePublishedFileResult_t, bool> CallbackFunction = null /*Action<RemoteStorageUpdatePublishedFileResult_t, bool>*/ )
		{
			SteamAPICall_t callback = 0;
			callback = platform.ISteamRemoteStorage_CommitPublishedFileUpdate( updateHandle.Value );
			
			if ( CallbackFunction == null ) return null;
			
			return RemoteStorageUpdatePublishedFileResult_t.CallResult( steamworks, callback, CallbackFunction );
		}
		
		// PublishedFileUpdateHandle_t
		public PublishedFileUpdateHandle_t CreatePublishedFileUpdateRequest( PublishedFileId_t unPublishedFileId /*PublishedFileId_t*/ )
		{
			return platform.ISteamRemoteStorage_CreatePublishedFileUpdateRequest( unPublishedFileId.Value );
		}
		
		// SteamAPICall_t
		public CallbackHandle DeletePublishedFile( PublishedFileId_t unPublishedFileId /*PublishedFileId_t*/, Action<RemoteStorageDeletePublishedFileResult_t, bool> CallbackFunction = null /*Action<RemoteStorageDeletePublishedFileResult_t, bool>*/ )
		{
			SteamAPICall_t callback = 0;
			callback = platform.ISteamRemoteStorage_DeletePublishedFile( unPublishedFileId.Value );
			
			if ( CallbackFunction == null ) return null;
			
			return RemoteStorageDeletePublishedFileResult_t.CallResult( steamworks, callback, CallbackFunction );
		}
		
		// SteamAPICall_t
		public CallbackHandle EnumeratePublishedFilesByUserAction( WorkshopFileAction eAction /*EWorkshopFileAction*/, uint unStartIndex /*uint32*/, Action<RemoteStorageEnumeratePublishedFilesByUserActionResult_t, bool> CallbackFunction = null /*Action<RemoteStorageEnumeratePublishedFilesByUserActionResult_t, bool>*/ )
		{
			SteamAPICall_t callback = 0;
			callback = platform.ISteamRemoteStorage_EnumeratePublishedFilesByUserAction( eAction, unStartIndex );
			
			if ( CallbackFunction == null ) return null;
			
			return RemoteStorageEnumeratePublishedFilesByUserActionResult_t.CallResult( steamworks, callback, CallbackFunction );
		}
		
		// SteamAPICall_t
		// using: Detect_StringArray
		public CallbackHandle EnumeratePublishedWorkshopFiles( WorkshopEnumerationType eEnumerationType /*EWorkshopEnumerationType*/, uint unStartIndex /*uint32*/, uint unCount /*uint32*/, uint unDays /*uint32*/, string[] pTags /*struct SteamParamStringArray_t **/, ref SteamParamStringArray_t pUserTags /*struct SteamParamStringArray_t **/, Action<RemoteStorageEnumerateWorkshopFilesResult_t, bool> CallbackFunction = null /*Action<RemoteStorageEnumerateWorkshopFilesResult_t, bool>*/ )
		{
			SteamAPICall_t callback = 0;
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
				callback = platform.ISteamRemoteStorage_EnumeratePublishedWorkshopFiles( eEnumerationType, unStartIndex, unCount, unDays, ref tags, ref pUserTags );
			}
			finally
			{
				foreach ( var x in nativeStrings )
				   Marshal.FreeHGlobal( x );
				
			}
			
			if ( CallbackFunction == null ) return null;
			
			return RemoteStorageEnumerateWorkshopFilesResult_t.CallResult( steamworks, callback, CallbackFunction );
		}
		
		// SteamAPICall_t
		public CallbackHandle EnumerateUserPublishedFiles( uint unStartIndex /*uint32*/, Action<RemoteStorageEnumerateUserPublishedFilesResult_t, bool> CallbackFunction = null /*Action<RemoteStorageEnumerateUserPublishedFilesResult_t, bool>*/ )
		{
			SteamAPICall_t callback = 0;
			callback = platform.ISteamRemoteStorage_EnumerateUserPublishedFiles( unStartIndex );
			
			if ( CallbackFunction == null ) return null;
			
			return RemoteStorageEnumerateUserPublishedFilesResult_t.CallResult( steamworks, callback, CallbackFunction );
		}
		
		// SteamAPICall_t
		// using: Detect_StringArray
		public CallbackHandle EnumerateUserSharedWorkshopFiles( CSteamID steamId /*class CSteamID*/, uint unStartIndex /*uint32*/, string[] pRequiredTags /*struct SteamParamStringArray_t **/, ref SteamParamStringArray_t pExcludedTags /*struct SteamParamStringArray_t **/, Action<RemoteStorageEnumerateUserPublishedFilesResult_t, bool> CallbackFunction = null /*Action<RemoteStorageEnumerateUserPublishedFilesResult_t, bool>*/ )
		{
			SteamAPICall_t callback = 0;
			// Create strings
			var nativeStrings = new IntPtr[pRequiredTags.Length];
			for ( int i = 0; i < pRequiredTags.Length; i++ )
			{
				nativeStrings[i] = Marshal.StringToHGlobalAnsi( pRequiredTags[i] );
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
				tags.NumStrings = pRequiredTags.Length;
				callback = platform.ISteamRemoteStorage_EnumerateUserSharedWorkshopFiles( steamId.Value, unStartIndex, ref tags, ref pExcludedTags );
			}
			finally
			{
				foreach ( var x in nativeStrings )
				   Marshal.FreeHGlobal( x );
				
			}
			
			if ( CallbackFunction == null ) return null;
			
			return RemoteStorageEnumerateUserPublishedFilesResult_t.CallResult( steamworks, callback, CallbackFunction );
		}
		
		// SteamAPICall_t
		public CallbackHandle EnumerateUserSubscribedFiles( uint unStartIndex /*uint32*/, Action<RemoteStorageEnumerateUserSubscribedFilesResult_t, bool> CallbackFunction = null /*Action<RemoteStorageEnumerateUserSubscribedFilesResult_t, bool>*/ )
		{
			SteamAPICall_t callback = 0;
			callback = platform.ISteamRemoteStorage_EnumerateUserSubscribedFiles( unStartIndex );
			
			if ( CallbackFunction == null ) return null;
			
			return RemoteStorageEnumerateUserSubscribedFilesResult_t.CallResult( steamworks, callback, CallbackFunction );
		}
		
		// bool
		public bool FileDelete( string pchFile /*const char **/ )
		{
			return platform.ISteamRemoteStorage_FileDelete( pchFile );
		}
		
		// bool
		public bool FileExists( string pchFile /*const char **/ )
		{
			return platform.ISteamRemoteStorage_FileExists( pchFile );
		}
		
		// bool
		public bool FileForget( string pchFile /*const char **/ )
		{
			return platform.ISteamRemoteStorage_FileForget( pchFile );
		}
		
		// bool
		public bool FilePersisted( string pchFile /*const char **/ )
		{
			return platform.ISteamRemoteStorage_FilePersisted( pchFile );
		}
		
		// int
		public int FileRead( string pchFile /*const char **/, IntPtr pvData /*void **/, int cubDataToRead /*int32*/ )
		{
			return platform.ISteamRemoteStorage_FileRead( pchFile, (IntPtr) pvData, cubDataToRead );
		}
		
		// SteamAPICall_t
		public CallbackHandle FileReadAsync( string pchFile /*const char **/, uint nOffset /*uint32*/, uint cubToRead /*uint32*/, Action<RemoteStorageFileReadAsyncComplete_t, bool> CallbackFunction = null /*Action<RemoteStorageFileReadAsyncComplete_t, bool>*/ )
		{
			SteamAPICall_t callback = 0;
			callback = platform.ISteamRemoteStorage_FileReadAsync( pchFile, nOffset, cubToRead );
			
			if ( CallbackFunction == null ) return null;
			
			return RemoteStorageFileReadAsyncComplete_t.CallResult( steamworks, callback, CallbackFunction );
		}
		
		// bool
		public bool FileReadAsyncComplete( SteamAPICall_t hReadCall /*SteamAPICall_t*/, IntPtr pvBuffer /*void **/, uint cubToRead /*uint32*/ )
		{
			return platform.ISteamRemoteStorage_FileReadAsyncComplete( hReadCall.Value, (IntPtr) pvBuffer, cubToRead );
		}
		
		// SteamAPICall_t
		public CallbackHandle FileShare( string pchFile /*const char **/, Action<RemoteStorageFileShareResult_t, bool> CallbackFunction = null /*Action<RemoteStorageFileShareResult_t, bool>*/ )
		{
			SteamAPICall_t callback = 0;
			callback = platform.ISteamRemoteStorage_FileShare( pchFile );
			
			if ( CallbackFunction == null ) return null;
			
			return RemoteStorageFileShareResult_t.CallResult( steamworks, callback, CallbackFunction );
		}
		
		// bool
		public bool FileWrite( string pchFile /*const char **/, IntPtr pvData /*const void **/, int cubData /*int32*/ )
		{
			return platform.ISteamRemoteStorage_FileWrite( pchFile, (IntPtr) pvData, cubData );
		}
		
		// SteamAPICall_t
		public CallbackHandle FileWriteAsync( string pchFile /*const char **/, IntPtr pvData /*const void **/, uint cubData /*uint32*/, Action<RemoteStorageFileWriteAsyncComplete_t, bool> CallbackFunction = null /*Action<RemoteStorageFileWriteAsyncComplete_t, bool>*/ )
		{
			SteamAPICall_t callback = 0;
			callback = platform.ISteamRemoteStorage_FileWriteAsync( pchFile, (IntPtr) pvData, cubData );
			
			if ( CallbackFunction == null ) return null;
			
			return RemoteStorageFileWriteAsyncComplete_t.CallResult( steamworks, callback, CallbackFunction );
		}
		
		// bool
		public bool FileWriteStreamCancel( UGCFileWriteStreamHandle_t writeHandle /*UGCFileWriteStreamHandle_t*/ )
		{
			return platform.ISteamRemoteStorage_FileWriteStreamCancel( writeHandle.Value );
		}
		
		// bool
		public bool FileWriteStreamClose( UGCFileWriteStreamHandle_t writeHandle /*UGCFileWriteStreamHandle_t*/ )
		{
			return platform.ISteamRemoteStorage_FileWriteStreamClose( writeHandle.Value );
		}
		
		// UGCFileWriteStreamHandle_t
		public UGCFileWriteStreamHandle_t FileWriteStreamOpen( string pchFile /*const char **/ )
		{
			return platform.ISteamRemoteStorage_FileWriteStreamOpen( pchFile );
		}
		
		// bool
		public bool FileWriteStreamWriteChunk( UGCFileWriteStreamHandle_t writeHandle /*UGCFileWriteStreamHandle_t*/, IntPtr pvData /*const void **/, int cubData /*int32*/ )
		{
			return platform.ISteamRemoteStorage_FileWriteStreamWriteChunk( writeHandle.Value, (IntPtr) pvData, cubData );
		}
		
		// int
		public int GetCachedUGCCount()
		{
			return platform.ISteamRemoteStorage_GetCachedUGCCount();
		}
		
		// UGCHandle_t
		public UGCHandle_t GetCachedUGCHandle( int iCachedContent /*int32*/ )
		{
			return platform.ISteamRemoteStorage_GetCachedUGCHandle( iCachedContent );
		}
		
		// int
		public int GetFileCount()
		{
			return platform.ISteamRemoteStorage_GetFileCount();
		}
		
		// string
		// with: Detect_StringReturn
		public string GetFileNameAndSize( int iFile /*int*/, out int pnFileSizeInBytes /*int32 **/ )
		{
			IntPtr string_pointer;
			string_pointer = platform.ISteamRemoteStorage_GetFileNameAndSize( iFile, out pnFileSizeInBytes );
			return Marshal.PtrToStringAnsi( string_pointer );
		}
		
		// int
		public int GetFileSize( string pchFile /*const char **/ )
		{
			return platform.ISteamRemoteStorage_GetFileSize( pchFile );
		}
		
		// long
		public long GetFileTimestamp( string pchFile /*const char **/ )
		{
			return platform.ISteamRemoteStorage_GetFileTimestamp( pchFile );
		}
		
		// SteamAPICall_t
		public CallbackHandle GetPublishedFileDetails( PublishedFileId_t unPublishedFileId /*PublishedFileId_t*/, uint unMaxSecondsOld /*uint32*/, Action<RemoteStorageGetPublishedFileDetailsResult_t, bool> CallbackFunction = null /*Action<RemoteStorageGetPublishedFileDetailsResult_t, bool>*/ )
		{
			SteamAPICall_t callback = 0;
			callback = platform.ISteamRemoteStorage_GetPublishedFileDetails( unPublishedFileId.Value, unMaxSecondsOld );
			
			if ( CallbackFunction == null ) return null;
			
			return RemoteStorageGetPublishedFileDetailsResult_t.CallResult( steamworks, callback, CallbackFunction );
		}
		
		// SteamAPICall_t
		public CallbackHandle GetPublishedItemVoteDetails( PublishedFileId_t unPublishedFileId /*PublishedFileId_t*/, Action<RemoteStorageGetPublishedItemVoteDetailsResult_t, bool> CallbackFunction = null /*Action<RemoteStorageGetPublishedItemVoteDetailsResult_t, bool>*/ )
		{
			SteamAPICall_t callback = 0;
			callback = platform.ISteamRemoteStorage_GetPublishedItemVoteDetails( unPublishedFileId.Value );
			
			if ( CallbackFunction == null ) return null;
			
			return RemoteStorageGetPublishedItemVoteDetailsResult_t.CallResult( steamworks, callback, CallbackFunction );
		}
		
		// bool
		public bool GetQuota( out ulong pnTotalBytes /*uint64 **/, out ulong puAvailableBytes /*uint64 **/ )
		{
			return platform.ISteamRemoteStorage_GetQuota( out pnTotalBytes, out puAvailableBytes );
		}
		
		// RemoteStoragePlatform
		public RemoteStoragePlatform GetSyncPlatforms( string pchFile /*const char **/ )
		{
			return platform.ISteamRemoteStorage_GetSyncPlatforms( pchFile );
		}
		
		// bool
		// with: Detect_StringFetch False
		public bool GetUGCDetails( UGCHandle_t hContent /*UGCHandle_t*/, ref AppId_t pnAppID /*AppId_t **/, out string ppchName /*char ***/, out CSteamID pSteamIDOwner /*class CSteamID **/ )
		{
			bool bSuccess = default( bool );
			ppchName = string.Empty;
			System.Text.StringBuilder ppchName_sb = Helpers.TakeStringBuilder();
			int pnFileSizeInBytes = 4096;
			bSuccess = platform.ISteamRemoteStorage_GetUGCDetails( hContent.Value, ref pnAppID.Value, ppchName_sb, out pnFileSizeInBytes, out pSteamIDOwner.Value );
			if ( !bSuccess ) return bSuccess;
			ppchName = ppchName_sb.ToString();
			return bSuccess;
		}
		
		// bool
		public bool GetUGCDownloadProgress( UGCHandle_t hContent /*UGCHandle_t*/, out int pnBytesDownloaded /*int32 **/, out int pnBytesExpected /*int32 **/ )
		{
			return platform.ISteamRemoteStorage_GetUGCDownloadProgress( hContent.Value, out pnBytesDownloaded, out pnBytesExpected );
		}
		
		// SteamAPICall_t
		public CallbackHandle GetUserPublishedItemVoteDetails( PublishedFileId_t unPublishedFileId /*PublishedFileId_t*/, Action<RemoteStorageGetPublishedItemVoteDetailsResult_t, bool> CallbackFunction = null /*Action<RemoteStorageGetPublishedItemVoteDetailsResult_t, bool>*/ )
		{
			SteamAPICall_t callback = 0;
			callback = platform.ISteamRemoteStorage_GetUserPublishedItemVoteDetails( unPublishedFileId.Value );
			
			if ( CallbackFunction == null ) return null;
			
			return RemoteStorageGetPublishedItemVoteDetailsResult_t.CallResult( steamworks, callback, CallbackFunction );
		}
		
		// bool
		public bool IsCloudEnabledForAccount()
		{
			return platform.ISteamRemoteStorage_IsCloudEnabledForAccount();
		}
		
		// bool
		public bool IsCloudEnabledForApp()
		{
			return platform.ISteamRemoteStorage_IsCloudEnabledForApp();
		}
		
		// SteamAPICall_t
		// using: Detect_StringArray
		public CallbackHandle PublishVideo( WorkshopVideoProvider eVideoProvider /*EWorkshopVideoProvider*/, string pchVideoAccount /*const char **/, string pchVideoIdentifier /*const char **/, string pchPreviewFile /*const char **/, AppId_t nConsumerAppId /*AppId_t*/, string pchTitle /*const char **/, string pchDescription /*const char **/, RemoteStoragePublishedFileVisibility eVisibility /*ERemoteStoragePublishedFileVisibility*/, string[] pTags /*struct SteamParamStringArray_t **/, Action<RemoteStoragePublishFileProgress_t, bool> CallbackFunction = null /*Action<RemoteStoragePublishFileProgress_t, bool>*/ )
		{
			SteamAPICall_t callback = 0;
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
				callback = platform.ISteamRemoteStorage_PublishVideo( eVideoProvider, pchVideoAccount, pchVideoIdentifier, pchPreviewFile, nConsumerAppId.Value, pchTitle, pchDescription, eVisibility, ref tags );
			}
			finally
			{
				foreach ( var x in nativeStrings )
				   Marshal.FreeHGlobal( x );
				
			}
			
			if ( CallbackFunction == null ) return null;
			
			return RemoteStoragePublishFileProgress_t.CallResult( steamworks, callback, CallbackFunction );
		}
		
		// SteamAPICall_t
		// using: Detect_StringArray
		public CallbackHandle PublishWorkshopFile( string pchFile /*const char **/, string pchPreviewFile /*const char **/, AppId_t nConsumerAppId /*AppId_t*/, string pchTitle /*const char **/, string pchDescription /*const char **/, RemoteStoragePublishedFileVisibility eVisibility /*ERemoteStoragePublishedFileVisibility*/, string[] pTags /*struct SteamParamStringArray_t **/, WorkshopFileType eWorkshopFileType /*EWorkshopFileType*/, Action<RemoteStoragePublishFileProgress_t, bool> CallbackFunction = null /*Action<RemoteStoragePublishFileProgress_t, bool>*/ )
		{
			SteamAPICall_t callback = 0;
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
				callback = platform.ISteamRemoteStorage_PublishWorkshopFile( pchFile, pchPreviewFile, nConsumerAppId.Value, pchTitle, pchDescription, eVisibility, ref tags, eWorkshopFileType );
			}
			finally
			{
				foreach ( var x in nativeStrings )
				   Marshal.FreeHGlobal( x );
				
			}
			
			if ( CallbackFunction == null ) return null;
			
			return RemoteStoragePublishFileProgress_t.CallResult( steamworks, callback, CallbackFunction );
		}
		
		// void
		public void SetCloudEnabledForApp( bool bEnabled /*bool*/ )
		{
			platform.ISteamRemoteStorage_SetCloudEnabledForApp( bEnabled );
		}
		
		// bool
		public bool SetSyncPlatforms( string pchFile /*const char **/, RemoteStoragePlatform eRemoteStoragePlatform /*ERemoteStoragePlatform*/ )
		{
			return platform.ISteamRemoteStorage_SetSyncPlatforms( pchFile, eRemoteStoragePlatform );
		}
		
		// SteamAPICall_t
		public CallbackHandle SetUserPublishedFileAction( PublishedFileId_t unPublishedFileId /*PublishedFileId_t*/, WorkshopFileAction eAction /*EWorkshopFileAction*/, Action<RemoteStorageSetUserPublishedFileActionResult_t, bool> CallbackFunction = null /*Action<RemoteStorageSetUserPublishedFileActionResult_t, bool>*/ )
		{
			SteamAPICall_t callback = 0;
			callback = platform.ISteamRemoteStorage_SetUserPublishedFileAction( unPublishedFileId.Value, eAction );
			
			if ( CallbackFunction == null ) return null;
			
			return RemoteStorageSetUserPublishedFileActionResult_t.CallResult( steamworks, callback, CallbackFunction );
		}
		
		// SteamAPICall_t
		public CallbackHandle SubscribePublishedFile( PublishedFileId_t unPublishedFileId /*PublishedFileId_t*/, Action<RemoteStorageSubscribePublishedFileResult_t, bool> CallbackFunction = null /*Action<RemoteStorageSubscribePublishedFileResult_t, bool>*/ )
		{
			SteamAPICall_t callback = 0;
			callback = platform.ISteamRemoteStorage_SubscribePublishedFile( unPublishedFileId.Value );
			
			if ( CallbackFunction == null ) return null;
			
			return RemoteStorageSubscribePublishedFileResult_t.CallResult( steamworks, callback, CallbackFunction );
		}
		
		// SteamAPICall_t
		public CallbackHandle UGCDownload( UGCHandle_t hContent /*UGCHandle_t*/, uint unPriority /*uint32*/, Action<RemoteStorageDownloadUGCResult_t, bool> CallbackFunction = null /*Action<RemoteStorageDownloadUGCResult_t, bool>*/ )
		{
			SteamAPICall_t callback = 0;
			callback = platform.ISteamRemoteStorage_UGCDownload( hContent.Value, unPriority );
			
			if ( CallbackFunction == null ) return null;
			
			return RemoteStorageDownloadUGCResult_t.CallResult( steamworks, callback, CallbackFunction );
		}
		
		// SteamAPICall_t
		public CallbackHandle UGCDownloadToLocation( UGCHandle_t hContent /*UGCHandle_t*/, string pchLocation /*const char **/, uint unPriority /*uint32*/, Action<RemoteStorageDownloadUGCResult_t, bool> CallbackFunction = null /*Action<RemoteStorageDownloadUGCResult_t, bool>*/ )
		{
			SteamAPICall_t callback = 0;
			callback = platform.ISteamRemoteStorage_UGCDownloadToLocation( hContent.Value, pchLocation, unPriority );
			
			if ( CallbackFunction == null ) return null;
			
			return RemoteStorageDownloadUGCResult_t.CallResult( steamworks, callback, CallbackFunction );
		}
		
		// int
		public int UGCRead( UGCHandle_t hContent /*UGCHandle_t*/, IntPtr pvData /*void **/, int cubDataToRead /*int32*/, uint cOffset /*uint32*/, UGCReadAction eAction /*EUGCReadAction*/ )
		{
			return platform.ISteamRemoteStorage_UGCRead( hContent.Value, (IntPtr) pvData, cubDataToRead, cOffset, eAction );
		}
		
		// SteamAPICall_t
		public CallbackHandle UnsubscribePublishedFile( PublishedFileId_t unPublishedFileId /*PublishedFileId_t*/, Action<RemoteStorageUnsubscribePublishedFileResult_t, bool> CallbackFunction = null /*Action<RemoteStorageUnsubscribePublishedFileResult_t, bool>*/ )
		{
			SteamAPICall_t callback = 0;
			callback = platform.ISteamRemoteStorage_UnsubscribePublishedFile( unPublishedFileId.Value );
			
			if ( CallbackFunction == null ) return null;
			
			return RemoteStorageUnsubscribePublishedFileResult_t.CallResult( steamworks, callback, CallbackFunction );
		}
		
		// bool
		public bool UpdatePublishedFileDescription( PublishedFileUpdateHandle_t updateHandle /*PublishedFileUpdateHandle_t*/, string pchDescription /*const char **/ )
		{
			return platform.ISteamRemoteStorage_UpdatePublishedFileDescription( updateHandle.Value, pchDescription );
		}
		
		// bool
		public bool UpdatePublishedFileFile( PublishedFileUpdateHandle_t updateHandle /*PublishedFileUpdateHandle_t*/, string pchFile /*const char **/ )
		{
			return platform.ISteamRemoteStorage_UpdatePublishedFileFile( updateHandle.Value, pchFile );
		}
		
		// bool
		public bool UpdatePublishedFilePreviewFile( PublishedFileUpdateHandle_t updateHandle /*PublishedFileUpdateHandle_t*/, string pchPreviewFile /*const char **/ )
		{
			return platform.ISteamRemoteStorage_UpdatePublishedFilePreviewFile( updateHandle.Value, pchPreviewFile );
		}
		
		// bool
		public bool UpdatePublishedFileSetChangeDescription( PublishedFileUpdateHandle_t updateHandle /*PublishedFileUpdateHandle_t*/, string pchChangeDescription /*const char **/ )
		{
			return platform.ISteamRemoteStorage_UpdatePublishedFileSetChangeDescription( updateHandle.Value, pchChangeDescription );
		}
		
		// bool
		// using: Detect_StringArray
		public bool UpdatePublishedFileTags( PublishedFileUpdateHandle_t updateHandle /*PublishedFileUpdateHandle_t*/, string[] pTags /*struct SteamParamStringArray_t **/ )
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
				return platform.ISteamRemoteStorage_UpdatePublishedFileTags( updateHandle.Value, ref tags );
			}
			finally
			{
				foreach ( var x in nativeStrings )
				   Marshal.FreeHGlobal( x );
				
			}
		}
		
		// bool
		public bool UpdatePublishedFileTitle( PublishedFileUpdateHandle_t updateHandle /*PublishedFileUpdateHandle_t*/, string pchTitle /*const char **/ )
		{
			return platform.ISteamRemoteStorage_UpdatePublishedFileTitle( updateHandle.Value, pchTitle );
		}
		
		// bool
		public bool UpdatePublishedFileVisibility( PublishedFileUpdateHandle_t updateHandle /*PublishedFileUpdateHandle_t*/, RemoteStoragePublishedFileVisibility eVisibility /*ERemoteStoragePublishedFileVisibility*/ )
		{
			return platform.ISteamRemoteStorage_UpdatePublishedFileVisibility( updateHandle.Value, eVisibility );
		}
		
		// SteamAPICall_t
		public CallbackHandle UpdateUserPublishedItemVote( PublishedFileId_t unPublishedFileId /*PublishedFileId_t*/, bool bVoteUp /*bool*/, Action<RemoteStorageUpdateUserPublishedItemVoteResult_t, bool> CallbackFunction = null /*Action<RemoteStorageUpdateUserPublishedItemVoteResult_t, bool>*/ )
		{
			SteamAPICall_t callback = 0;
			callback = platform.ISteamRemoteStorage_UpdateUserPublishedItemVote( unPublishedFileId.Value, bVoteUp );
			
			if ( CallbackFunction == null ) return null;
			
			return RemoteStorageUpdateUserPublishedItemVoteResult_t.CallResult( steamworks, callback, CallbackFunction );
		}
		
	}
}
