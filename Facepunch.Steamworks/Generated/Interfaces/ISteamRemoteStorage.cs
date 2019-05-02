using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Steamworks.Data;


namespace Steamworks
{
	internal class ISteamRemoteStorage : SteamInterface
	{
		public override string InterfaceName => "STEAMREMOTESTORAGE_INTERFACE_VERSION014";
		
		public override void InitInternals()
		{
			_FileWrite = Marshal.GetDelegateForFunctionPointer<FFileWrite>( Marshal.ReadIntPtr( VTable, 0) );
			_FileRead = Marshal.GetDelegateForFunctionPointer<FFileRead>( Marshal.ReadIntPtr( VTable, 8) );
			_FileWriteAsync = Marshal.GetDelegateForFunctionPointer<FFileWriteAsync>( Marshal.ReadIntPtr( VTable, 16) );
			_FileReadAsync = Marshal.GetDelegateForFunctionPointer<FFileReadAsync>( Marshal.ReadIntPtr( VTable, 24) );
			_FileReadAsyncComplete = Marshal.GetDelegateForFunctionPointer<FFileReadAsyncComplete>( Marshal.ReadIntPtr( VTable, 32) );
			_FileForget = Marshal.GetDelegateForFunctionPointer<FFileForget>( Marshal.ReadIntPtr( VTable, 40) );
			_FileDelete = Marshal.GetDelegateForFunctionPointer<FFileDelete>( Marshal.ReadIntPtr( VTable, 48) );
			_FileShare = Marshal.GetDelegateForFunctionPointer<FFileShare>( Marshal.ReadIntPtr( VTable, 56) );
			_SetSyncPlatforms = Marshal.GetDelegateForFunctionPointer<FSetSyncPlatforms>( Marshal.ReadIntPtr( VTable, 64) );
			_FileWriteStreamOpen = Marshal.GetDelegateForFunctionPointer<FFileWriteStreamOpen>( Marshal.ReadIntPtr( VTable, 72) );
			_FileWriteStreamWriteChunk = Marshal.GetDelegateForFunctionPointer<FFileWriteStreamWriteChunk>( Marshal.ReadIntPtr( VTable, 80) );
			_FileWriteStreamClose = Marshal.GetDelegateForFunctionPointer<FFileWriteStreamClose>( Marshal.ReadIntPtr( VTable, 88) );
			_FileWriteStreamCancel = Marshal.GetDelegateForFunctionPointer<FFileWriteStreamCancel>( Marshal.ReadIntPtr( VTable, 96) );
			_FileExists = Marshal.GetDelegateForFunctionPointer<FFileExists>( Marshal.ReadIntPtr( VTable, 104) );
			_FilePersisted = Marshal.GetDelegateForFunctionPointer<FFilePersisted>( Marshal.ReadIntPtr( VTable, 112) );
			_GetFileSize = Marshal.GetDelegateForFunctionPointer<FGetFileSize>( Marshal.ReadIntPtr( VTable, 120) );
			_GetFileTimestamp = Marshal.GetDelegateForFunctionPointer<FGetFileTimestamp>( Marshal.ReadIntPtr( VTable, 128) );
			_GetSyncPlatforms = Marshal.GetDelegateForFunctionPointer<FGetSyncPlatforms>( Marshal.ReadIntPtr( VTable, 136) );
			_GetFileCount = Marshal.GetDelegateForFunctionPointer<FGetFileCount>( Marshal.ReadIntPtr( VTable, 144) );
			_GetFileNameAndSize = Marshal.GetDelegateForFunctionPointer<FGetFileNameAndSize>( Marshal.ReadIntPtr( VTable, 152) );
			_GetQuota = Marshal.GetDelegateForFunctionPointer<FGetQuota>( Marshal.ReadIntPtr( VTable, 160) );
			_IsCloudEnabledForAccount = Marshal.GetDelegateForFunctionPointer<FIsCloudEnabledForAccount>( Marshal.ReadIntPtr( VTable, 168) );
			_IsCloudEnabledForApp = Marshal.GetDelegateForFunctionPointer<FIsCloudEnabledForApp>( Marshal.ReadIntPtr( VTable, 176) );
			_SetCloudEnabledForApp = Marshal.GetDelegateForFunctionPointer<FSetCloudEnabledForApp>( Marshal.ReadIntPtr( VTable, 184) );
			_UGCDownload = Marshal.GetDelegateForFunctionPointer<FUGCDownload>( Marshal.ReadIntPtr( VTable, 192) );
			_GetUGCDownloadProgress = Marshal.GetDelegateForFunctionPointer<FGetUGCDownloadProgress>( Marshal.ReadIntPtr( VTable, 200) );
			_GetUGCDetails = Marshal.GetDelegateForFunctionPointer<FGetUGCDetails>( Marshal.ReadIntPtr( VTable, 208) );
			_UGCRead = Marshal.GetDelegateForFunctionPointer<FUGCRead>( Marshal.ReadIntPtr( VTable, 216) );
			_GetCachedUGCCount = Marshal.GetDelegateForFunctionPointer<FGetCachedUGCCount>( Marshal.ReadIntPtr( VTable, 224) );
			 // PublishWorkshopFile is deprecated - 232
			 // CreatePublishedFileUpdateRequest is deprecated - 240
			 // UpdatePublishedFileFile is deprecated - 248
			 // UpdatePublishedFilePreviewFile is deprecated - 256
			 // UpdatePublishedFileTitle is deprecated - 264
			 // UpdatePublishedFileDescription is deprecated - 272
			 // UpdatePublishedFileVisibility is deprecated - 280
			 // UpdatePublishedFileTags is deprecated - 288
			 // CommitPublishedFileUpdate is deprecated - 296
			 // GetPublishedFileDetails is deprecated - 304
			 // DeletePublishedFile is deprecated - 312
			 // EnumerateUserPublishedFiles is deprecated - 320
			 // SubscribePublishedFile is deprecated - 328
			 // EnumerateUserSubscribedFiles is deprecated - 336
			 // UnsubscribePublishedFile is deprecated - 344
			 // UpdatePublishedFileSetChangeDescription is deprecated - 352
			 // GetPublishedItemVoteDetails is deprecated - 360
			 // UpdateUserPublishedItemVote is deprecated - 368
			 // GetUserPublishedItemVoteDetails is deprecated - 376
			 // EnumerateUserSharedWorkshopFiles is deprecated - 384
			 // PublishVideo is deprecated - 392
			 // SetUserPublishedFileAction is deprecated - 400
			 // EnumeratePublishedFilesByUserAction is deprecated - 408
			 // EnumeratePublishedWorkshopFiles is deprecated - 416
			_UGCDownloadToLocation = Marshal.GetDelegateForFunctionPointer<FUGCDownloadToLocation>( Marshal.ReadIntPtr( VTable, 424) );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FFileWrite( IntPtr self, string pchFile, IntPtr pvData, int cubData );
		private FFileWrite _FileWrite;
		
		#endregion
		internal bool FileWrite( string pchFile, IntPtr pvData, int cubData )
		{
			return _FileWrite( Self, pchFile, pvData, cubData );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate int FFileRead( IntPtr self, string pchFile, IntPtr pvData, int cubDataToRead );
		private FFileRead _FileRead;
		
		#endregion
		internal int FileRead( string pchFile, IntPtr pvData, int cubDataToRead )
		{
			return _FileRead( Self, pchFile, pvData, cubDataToRead );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate SteamAPICall_t FFileWriteAsync( IntPtr self, string pchFile, IntPtr pvData, uint cubData );
		private FFileWriteAsync _FileWriteAsync;
		
		#endregion
		internal async Task<RemoteStorageFileWriteAsyncComplete_t?> FileWriteAsync( string pchFile, IntPtr pvData, uint cubData )
		{
			return await RemoteStorageFileWriteAsyncComplete_t.GetResultAsync( _FileWriteAsync( Self, pchFile, pvData, cubData ) );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate SteamAPICall_t FFileReadAsync( IntPtr self, string pchFile, uint nOffset, uint cubToRead );
		private FFileReadAsync _FileReadAsync;
		
		#endregion
		internal async Task<RemoteStorageFileReadAsyncComplete_t?> FileReadAsync( string pchFile, uint nOffset, uint cubToRead )
		{
			return await RemoteStorageFileReadAsyncComplete_t.GetResultAsync( _FileReadAsync( Self, pchFile, nOffset, cubToRead ) );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FFileReadAsyncComplete( IntPtr self, SteamAPICall_t hReadCall, IntPtr pvBuffer, uint cubToRead );
		private FFileReadAsyncComplete _FileReadAsyncComplete;
		
		#endregion
		internal bool FileReadAsyncComplete( SteamAPICall_t hReadCall, IntPtr pvBuffer, uint cubToRead )
		{
			return _FileReadAsyncComplete( Self, hReadCall, pvBuffer, cubToRead );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FFileForget( IntPtr self, string pchFile );
		private FFileForget _FileForget;
		
		#endregion
		internal bool FileForget( string pchFile )
		{
			return _FileForget( Self, pchFile );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FFileDelete( IntPtr self, string pchFile );
		private FFileDelete _FileDelete;
		
		#endregion
		internal bool FileDelete( string pchFile )
		{
			return _FileDelete( Self, pchFile );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate SteamAPICall_t FFileShare( IntPtr self, string pchFile );
		private FFileShare _FileShare;
		
		#endregion
		internal async Task<RemoteStorageFileShareResult_t?> FileShare( string pchFile )
		{
			return await RemoteStorageFileShareResult_t.GetResultAsync( _FileShare( Self, pchFile ) );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FSetSyncPlatforms( IntPtr self, string pchFile, RemoteStoragePlatform eRemoteStoragePlatform );
		private FSetSyncPlatforms _SetSyncPlatforms;
		
		#endregion
		internal bool SetSyncPlatforms( string pchFile, RemoteStoragePlatform eRemoteStoragePlatform )
		{
			return _SetSyncPlatforms( Self, pchFile, eRemoteStoragePlatform );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate UGCFileWriteStreamHandle_t FFileWriteStreamOpen( IntPtr self, string pchFile );
		private FFileWriteStreamOpen _FileWriteStreamOpen;
		
		#endregion
		internal UGCFileWriteStreamHandle_t FileWriteStreamOpen( string pchFile )
		{
			return _FileWriteStreamOpen( Self, pchFile );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FFileWriteStreamWriteChunk( IntPtr self, UGCFileWriteStreamHandle_t writeHandle, IntPtr pvData, int cubData );
		private FFileWriteStreamWriteChunk _FileWriteStreamWriteChunk;
		
		#endregion
		internal bool FileWriteStreamWriteChunk( UGCFileWriteStreamHandle_t writeHandle, IntPtr pvData, int cubData )
		{
			return _FileWriteStreamWriteChunk( Self, writeHandle, pvData, cubData );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FFileWriteStreamClose( IntPtr self, UGCFileWriteStreamHandle_t writeHandle );
		private FFileWriteStreamClose _FileWriteStreamClose;
		
		#endregion
		internal bool FileWriteStreamClose( UGCFileWriteStreamHandle_t writeHandle )
		{
			return _FileWriteStreamClose( Self, writeHandle );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FFileWriteStreamCancel( IntPtr self, UGCFileWriteStreamHandle_t writeHandle );
		private FFileWriteStreamCancel _FileWriteStreamCancel;
		
		#endregion
		internal bool FileWriteStreamCancel( UGCFileWriteStreamHandle_t writeHandle )
		{
			return _FileWriteStreamCancel( Self, writeHandle );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FFileExists( IntPtr self, string pchFile );
		private FFileExists _FileExists;
		
		#endregion
		internal bool FileExists( string pchFile )
		{
			return _FileExists( Self, pchFile );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FFilePersisted( IntPtr self, string pchFile );
		private FFilePersisted _FilePersisted;
		
		#endregion
		internal bool FilePersisted( string pchFile )
		{
			return _FilePersisted( Self, pchFile );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate int FGetFileSize( IntPtr self, string pchFile );
		private FGetFileSize _GetFileSize;
		
		#endregion
		internal int GetFileSize( string pchFile )
		{
			return _GetFileSize( Self, pchFile );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate long FGetFileTimestamp( IntPtr self, string pchFile );
		private FGetFileTimestamp _GetFileTimestamp;
		
		#endregion
		internal long GetFileTimestamp( string pchFile )
		{
			return _GetFileTimestamp( Self, pchFile );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate RemoteStoragePlatform FGetSyncPlatforms( IntPtr self, string pchFile );
		private FGetSyncPlatforms _GetSyncPlatforms;
		
		#endregion
		internal RemoteStoragePlatform GetSyncPlatforms( string pchFile )
		{
			return _GetSyncPlatforms( Self, pchFile );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate int FGetFileCount( IntPtr self );
		private FGetFileCount _GetFileCount;
		
		#endregion
		internal int GetFileCount()
		{
			return _GetFileCount( Self );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate IntPtr FGetFileNameAndSize( IntPtr self, int iFile, ref int pnFileSizeInBytes );
		private FGetFileNameAndSize _GetFileNameAndSize;
		
		#endregion
		internal string GetFileNameAndSize( int iFile, ref int pnFileSizeInBytes )
		{
			return GetString( _GetFileNameAndSize( Self, iFile, ref pnFileSizeInBytes ) );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FGetQuota( IntPtr self, ref ulong pnTotalBytes, ref ulong puAvailableBytes );
		private FGetQuota _GetQuota;
		
		#endregion
		internal bool GetQuota( ref ulong pnTotalBytes, ref ulong puAvailableBytes )
		{
			return _GetQuota( Self, ref pnTotalBytes, ref puAvailableBytes );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FIsCloudEnabledForAccount( IntPtr self );
		private FIsCloudEnabledForAccount _IsCloudEnabledForAccount;
		
		#endregion
		internal bool IsCloudEnabledForAccount()
		{
			return _IsCloudEnabledForAccount( Self );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FIsCloudEnabledForApp( IntPtr self );
		private FIsCloudEnabledForApp _IsCloudEnabledForApp;
		
		#endregion
		internal bool IsCloudEnabledForApp()
		{
			return _IsCloudEnabledForApp( Self );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate void FSetCloudEnabledForApp( IntPtr self, [MarshalAs( UnmanagedType.U1 )] bool bEnabled );
		private FSetCloudEnabledForApp _SetCloudEnabledForApp;
		
		#endregion
		internal void SetCloudEnabledForApp( [MarshalAs( UnmanagedType.U1 )] bool bEnabled )
		{
			_SetCloudEnabledForApp( Self, bEnabled );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate SteamAPICall_t FUGCDownload( IntPtr self, UGCHandle_t hContent, uint unPriority );
		private FUGCDownload _UGCDownload;
		
		#endregion
		internal async Task<RemoteStorageDownloadUGCResult_t?> UGCDownload( UGCHandle_t hContent, uint unPriority )
		{
			return await RemoteStorageDownloadUGCResult_t.GetResultAsync( _UGCDownload( Self, hContent, unPriority ) );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FGetUGCDownloadProgress( IntPtr self, UGCHandle_t hContent, ref int pnBytesDownloaded, ref int pnBytesExpected );
		private FGetUGCDownloadProgress _GetUGCDownloadProgress;
		
		#endregion
		internal bool GetUGCDownloadProgress( UGCHandle_t hContent, ref int pnBytesDownloaded, ref int pnBytesExpected )
		{
			return _GetUGCDownloadProgress( Self, hContent, ref pnBytesDownloaded, ref pnBytesExpected );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FGetUGCDetails( IntPtr self, UGCHandle_t hContent, ref AppId pnAppID, [In,Out] ref char[]  ppchName, ref int pnFileSizeInBytes, ref SteamId pSteamIDOwner );
		private FGetUGCDetails _GetUGCDetails;
		
		#endregion
		internal bool GetUGCDetails( UGCHandle_t hContent, ref AppId pnAppID, [In,Out] ref char[]  ppchName, ref int pnFileSizeInBytes, ref SteamId pSteamIDOwner )
		{
			return _GetUGCDetails( Self, hContent, ref pnAppID, ref ppchName, ref pnFileSizeInBytes, ref pSteamIDOwner );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate int FUGCRead( IntPtr self, UGCHandle_t hContent, IntPtr pvData, int cubDataToRead, uint cOffset, UGCReadAction eAction );
		private FUGCRead _UGCRead;
		
		#endregion
		internal int UGCRead( UGCHandle_t hContent, IntPtr pvData, int cubDataToRead, uint cOffset, UGCReadAction eAction )
		{
			return _UGCRead( Self, hContent, pvData, cubDataToRead, cOffset, eAction );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate int FGetCachedUGCCount( IntPtr self );
		private FGetCachedUGCCount _GetCachedUGCCount;
		
		#endregion
		internal int GetCachedUGCCount()
		{
			return _GetCachedUGCCount( Self );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate SteamAPICall_t FUGCDownloadToLocation( IntPtr self, UGCHandle_t hContent, string pchLocation, uint unPriority );
		private FUGCDownloadToLocation _UGCDownloadToLocation;
		
		#endregion
		internal async Task<RemoteStorageDownloadUGCResult_t?> UGCDownloadToLocation( UGCHandle_t hContent, string pchLocation, uint unPriority )
		{
			return await RemoteStorageDownloadUGCResult_t.GetResultAsync( _UGCDownloadToLocation( Self, hContent, pchLocation, unPriority ) );
		}
		
	}
}
