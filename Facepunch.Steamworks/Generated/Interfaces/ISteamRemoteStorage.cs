using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Steamworks.Data;


namespace Steamworks
{
	internal class ISteamRemoteStorage : SteamInterface
	{
		public ISteamRemoteStorage( bool server = false ) : base( server )
		{
		}
		
		public override string InterfaceName => "STEAMREMOTESTORAGE_INTERFACE_VERSION014";
		
		public override void InitInternals()
		{
			_FileWriteAsync = Marshal.GetDelegateForFunctionPointer<FFileWriteAsync>( Marshal.ReadIntPtr( VTable, 0) );
			_FileReadAsync = Marshal.GetDelegateForFunctionPointer<FFileReadAsync>( Marshal.ReadIntPtr( VTable, 8) );
			_FileShare = Marshal.GetDelegateForFunctionPointer<FFileShare>( Marshal.ReadIntPtr( VTable, 16) );
			_FileWriteStreamOpen = Marshal.GetDelegateForFunctionPointer<FFileWriteStreamOpen>( Marshal.ReadIntPtr( VTable, 24) );
			_FileWriteStreamWriteChunk = Marshal.GetDelegateForFunctionPointer<FFileWriteStreamWriteChunk>( Marshal.ReadIntPtr( VTable, 32) );
			_FileWriteStreamClose = Marshal.GetDelegateForFunctionPointer<FFileWriteStreamClose>( Marshal.ReadIntPtr( VTable, 40) );
			_FileWriteStreamCancel = Marshal.GetDelegateForFunctionPointer<FFileWriteStreamCancel>( Marshal.ReadIntPtr( VTable, 48) );
			_GetSyncPlatforms = Marshal.GetDelegateForFunctionPointer<FGetSyncPlatforms>( Marshal.ReadIntPtr( VTable, 56) );
			_GetFileCount = Marshal.GetDelegateForFunctionPointer<FGetFileCount>( Marshal.ReadIntPtr( VTable, 64) );
			_GetFileNameAndSize = Marshal.GetDelegateForFunctionPointer<FGetFileNameAndSize>( Marshal.ReadIntPtr( VTable, 72) );
			_GetQuota = Marshal.GetDelegateForFunctionPointer<FGetQuota>( Marshal.ReadIntPtr( VTable, 80) );
			_IsCloudEnabledForAccount = Marshal.GetDelegateForFunctionPointer<FIsCloudEnabledForAccount>( Marshal.ReadIntPtr( VTable, 88) );
			_IsCloudEnabledForApp = Marshal.GetDelegateForFunctionPointer<FIsCloudEnabledForApp>( Marshal.ReadIntPtr( VTable, 96) );
			_SetCloudEnabledForApp = Marshal.GetDelegateForFunctionPointer<FSetCloudEnabledForApp>( Marshal.ReadIntPtr( VTable, 104) );
			_UGCDownload = Marshal.GetDelegateForFunctionPointer<FUGCDownload>( Marshal.ReadIntPtr( VTable, 112) );
			_GetCachedUGCHandle = Marshal.GetDelegateForFunctionPointer<FGetCachedUGCHandle>( Marshal.ReadIntPtr( VTable, 120) );
			_GetFileListFromServer = Marshal.GetDelegateForFunctionPointer<FGetFileListFromServer>( Marshal.ReadIntPtr( VTable, 128) );
			_FileFetch = Marshal.GetDelegateForFunctionPointer<FFileFetch>( Marshal.ReadIntPtr( VTable, 136) );
			_FilePersist = Marshal.GetDelegateForFunctionPointer<FFilePersist>( Marshal.ReadIntPtr( VTable, 144) );
			_SynchronizeToClient = Marshal.GetDelegateForFunctionPointer<FSynchronizeToClient>( Marshal.ReadIntPtr( VTable, 152) );
			_SynchronizeToServer = Marshal.GetDelegateForFunctionPointer<FSynchronizeToServer>( Marshal.ReadIntPtr( VTable, 160) );
			_ResetFileRequestState = Marshal.GetDelegateForFunctionPointer<FResetFileRequestState>( Marshal.ReadIntPtr( VTable, 168) );
			_CreatePublishedFileUpdateRequest = Marshal.GetDelegateForFunctionPointer<FCreatePublishedFileUpdateRequest>( Marshal.ReadIntPtr( VTable, 176) );
			_UpdatePublishedFileFile = Marshal.GetDelegateForFunctionPointer<FUpdatePublishedFileFile>( Marshal.ReadIntPtr( VTable, 184) );
			_UpdatePublishedFilePreviewFile = Marshal.GetDelegateForFunctionPointer<FUpdatePublishedFilePreviewFile>( Marshal.ReadIntPtr( VTable, 192) );
			_UpdatePublishedFileTitle = Marshal.GetDelegateForFunctionPointer<FUpdatePublishedFileTitle>( Marshal.ReadIntPtr( VTable, 200) );
			_UpdatePublishedFileDescription = Marshal.GetDelegateForFunctionPointer<FUpdatePublishedFileDescription>( Marshal.ReadIntPtr( VTable, 208) );
			_UpdatePublishedFileVisibility = Marshal.GetDelegateForFunctionPointer<FUpdatePublishedFileVisibility>( Marshal.ReadIntPtr( VTable, 216) );
			_UpdatePublishedFileTags = Marshal.GetDelegateForFunctionPointer<FUpdatePublishedFileTags>( Marshal.ReadIntPtr( VTable, 224) );
			_UpdatePublishedFileSetChangeDescription = Marshal.GetDelegateForFunctionPointer<FUpdatePublishedFileSetChangeDescription>( Marshal.ReadIntPtr( VTable, 232) );
			_UGCDownloadToLocation = Marshal.GetDelegateForFunctionPointer<FUGCDownloadToLocation>( Marshal.ReadIntPtr( VTable, 240) );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate SteamAPICall_t FFileWriteAsync( IntPtr self, string pchFile, IntPtr pvData, uint cubData );
		private FFileWriteAsync _FileWriteAsync;
		
		#endregion
		internal async Task<RemoteStorageFileWriteAsyncComplete_t?> FileWriteAsync( string pchFile, IntPtr pvData, uint cubData )
		{
			return await (new Result<RemoteStorageFileWriteAsyncComplete_t>( _FileWriteAsync( Self, pchFile, pvData, cubData ) )).GetResult();
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate SteamAPICall_t FFileReadAsync( IntPtr self, string pchFile, uint nOffset, uint cubToRead );
		private FFileReadAsync _FileReadAsync;
		
		#endregion
		internal async Task<RemoteStorageFileReadAsyncComplete_t?> FileReadAsync( string pchFile, uint nOffset, uint cubToRead )
		{
			return await (new Result<RemoteStorageFileReadAsyncComplete_t>( _FileReadAsync( Self, pchFile, nOffset, cubToRead ) )).GetResult();
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate SteamAPICall_t FFileShare( IntPtr self, string pchFile );
		private FFileShare _FileShare;
		
		#endregion
		internal async Task<RemoteStorageFileShareResult_t?> FileShare( string pchFile )
		{
			return await (new Result<RemoteStorageFileShareResult_t>( _FileShare( Self, pchFile ) )).GetResult();
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate bool	SetSyncPlatforms( char *pchFile, ERemoteStoragePlatform eRemoteStoragePlatform ) = 0; virtual UGCFileWriteStreamHandle_t FFileWriteStreamOpen( IntPtr self, string pchFile );
		private FFileWriteStreamOpen _FileWriteStreamOpen;
		
		#endregion
		internal bool	SetSyncPlatforms( char *pchFile, ERemoteStoragePlatform eRemoteStoragePlatform ) = 0; virtual UGCFileWriteStreamHandle_t FileWriteStreamOpen( string pchFile )
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
		private delegate bool	FileExists( const char *pchFile ) = 0; virtual bool	FilePersisted( const char *pchFile ) = 0; virtual int32	GetFileSize( const char *pchFile ) = 0; virtual int64	GetFileTimestamp( const char *pchFile ) = 0; virtual ERemoteStoragePlatform FGetSyncPlatforms( IntPtr self, string pchFile );
		private FGetSyncPlatforms _GetSyncPlatforms;
		
		#endregion
		internal bool	FileExists( const char *pchFile ) = 0; virtual bool	FilePersisted( const char *pchFile ) = 0; virtual int32	GetFileSize( const char *pchFile ) = 0; virtual int64	GetFileTimestamp( const char *pchFile ) = 0; virtual ERemoteStoragePlatform GetSyncPlatforms( string pchFile )
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
			return await (new Result<RemoteStorageDownloadUGCResult_t>( _UGCDownload( Self, hContent, unPriority ) )).GetResult();
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate bool	GetUGCDownloadProgress( UGCHandle_t hContent, int32 *pnBytesDownloaded, int32 *pnBytesExpected ) = 0; virtual bool	GetUGCDetails( UGCHandle_t hContent, AppId *pnAppID, STEAM_OUT_STRING() char **ppchName, int32 *pnFileSizeInBytes, SteamId *pSteamIDOwner ) = 0; virtual int32	UGCRead( UGCHandle_t hContent, void *pvData, int32 cubDataToRead, uint32 cOffset, EUGCReadAction eAction ) = 0; virtual int32	GetCachedUGCCount() = 0; virtual	UGCHandle_t FGetCachedUGCHandle( IntPtr self, int iCachedContent );
		private FGetCachedUGCHandle _GetCachedUGCHandle;
		
		#endregion
		internal bool	GetUGCDownloadProgress( UGCHandle_t hContent, int32 *pnBytesDownloaded, int32 *pnBytesExpected ) = 0; virtual bool	GetUGCDetails( UGCHandle_t hContent, AppId *pnAppID, STEAM_OUT_STRING() char **ppchName, int32 *pnFileSizeInBytes, SteamId *pSteamIDOwner ) = 0; virtual int32	UGCRead( UGCHandle_t hContent, void *pvData, int32 cubDataToRead, uint32 cOffset, EUGCReadAction eAction ) = 0; virtual int32	GetCachedUGCCount() = 0; virtual	UGCHandle_t GetCachedUGCHandle( int iCachedContent )
		{
			return _GetCachedUGCHandle( Self, iCachedContent );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate void FGetFileListFromServer( IntPtr self );
		private FGetFileListFromServer _GetFileListFromServer;
		
		#endregion
		internal void GetFileListFromServer()
		{
			_GetFileListFromServer( Self );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FFileFetch( IntPtr self, string pchFile );
		private FFileFetch _FileFetch;
		
		#endregion
		internal bool FileFetch( string pchFile )
		{
			return _FileFetch( Self, pchFile );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FFilePersist( IntPtr self, string pchFile );
		private FFilePersist _FilePersist;
		
		#endregion
		internal bool FilePersist( string pchFile )
		{
			return _FilePersist( Self, pchFile );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FSynchronizeToClient( IntPtr self );
		private FSynchronizeToClient _SynchronizeToClient;
		
		#endregion
		internal bool SynchronizeToClient()
		{
			return _SynchronizeToClient( Self );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FSynchronizeToServer( IntPtr self );
		private FSynchronizeToServer _SynchronizeToServer;
		
		#endregion
		internal bool SynchronizeToServer()
		{
			return _SynchronizeToServer( Self );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FResetFileRequestState( IntPtr self );
		private FResetFileRequestState _ResetFileRequestState;
		
		#endregion
		internal bool ResetFileRequestState()
		{
			return _ResetFileRequestState( Self );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate SteamAPICall_t	PublishWorkshopFile( char *pchFile, char *pchPreviewFile, AppId nConsumerAppId, char *pchTitle, char *pchDescription, ERemoteStoragePublishedFileVisibility eVisibility, SteamParamStringArray_t *pTags, EWorkshopFileType eWorkshopFileType ) = 0; virtual PublishedFileUpdateHandle_t FCreatePublishedFileUpdateRequest( IntPtr self, PublishedFileId unPublishedFileId );
		private FCreatePublishedFileUpdateRequest _CreatePublishedFileUpdateRequest;
		
		#endregion
		internal SteamAPICall_t	PublishWorkshopFile( char *pchFile, char *pchPreviewFile, AppId nConsumerAppId, char *pchTitle, char *pchDescription, ERemoteStoragePublishedFileVisibility eVisibility, SteamParamStringArray_t *pTags, EWorkshopFileType eWorkshopFileType ) = 0; virtual PublishedFileUpdateHandle_t CreatePublishedFileUpdateRequest( PublishedFileId unPublishedFileId )
		{
			return _CreatePublishedFileUpdateRequest( Self, unPublishedFileId );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FUpdatePublishedFileFile( IntPtr self, PublishedFileUpdateHandle_t updateHandle, string pchFile );
		private FUpdatePublishedFileFile _UpdatePublishedFileFile;
		
		#endregion
		internal bool UpdatePublishedFileFile( PublishedFileUpdateHandle_t updateHandle, string pchFile )
		{
			return _UpdatePublishedFileFile( Self, updateHandle, pchFile );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FUpdatePublishedFilePreviewFile( IntPtr self, PublishedFileUpdateHandle_t updateHandle, string pchPreviewFile );
		private FUpdatePublishedFilePreviewFile _UpdatePublishedFilePreviewFile;
		
		#endregion
		internal bool UpdatePublishedFilePreviewFile( PublishedFileUpdateHandle_t updateHandle, string pchPreviewFile )
		{
			return _UpdatePublishedFilePreviewFile( Self, updateHandle, pchPreviewFile );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FUpdatePublishedFileTitle( IntPtr self, PublishedFileUpdateHandle_t updateHandle, string pchTitle );
		private FUpdatePublishedFileTitle _UpdatePublishedFileTitle;
		
		#endregion
		internal bool UpdatePublishedFileTitle( PublishedFileUpdateHandle_t updateHandle, string pchTitle )
		{
			return _UpdatePublishedFileTitle( Self, updateHandle, pchTitle );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FUpdatePublishedFileDescription( IntPtr self, PublishedFileUpdateHandle_t updateHandle, string pchDescription );
		private FUpdatePublishedFileDescription _UpdatePublishedFileDescription;
		
		#endregion
		internal bool UpdatePublishedFileDescription( PublishedFileUpdateHandle_t updateHandle, string pchDescription )
		{
			return _UpdatePublishedFileDescription( Self, updateHandle, pchDescription );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FUpdatePublishedFileVisibility( IntPtr self, PublishedFileUpdateHandle_t updateHandle, RemoteStoragePublishedFileVisibility eVisibility );
		private FUpdatePublishedFileVisibility _UpdatePublishedFileVisibility;
		
		#endregion
		internal bool UpdatePublishedFileVisibility( PublishedFileUpdateHandle_t updateHandle, RemoteStoragePublishedFileVisibility eVisibility )
		{
			return _UpdatePublishedFileVisibility( Self, updateHandle, eVisibility );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FUpdatePublishedFileTags( IntPtr self, PublishedFileUpdateHandle_t updateHandle, ref SteamParamStringArray_t pTags );
		private FUpdatePublishedFileTags _UpdatePublishedFileTags;
		
		#endregion
		internal bool UpdatePublishedFileTags( PublishedFileUpdateHandle_t updateHandle, ref SteamParamStringArray_t pTags )
		{
			return _UpdatePublishedFileTags( Self, updateHandle, ref pTags );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate SteamAPICall_t	UnsubscribePublishedFile( PublishedFileId unPublishedFileId ) = 0; virtual bool FUpdatePublishedFileSetChangeDescription( IntPtr self, PublishedFileUpdateHandle_t updateHandle, string pchChangeDescription );
		private FUpdatePublishedFileSetChangeDescription _UpdatePublishedFileSetChangeDescription;
		
		#endregion
		internal SteamAPICall_t	UnsubscribePublishedFile( PublishedFileId unPublishedFileId ) = 0; virtual bool UpdatePublishedFileSetChangeDescription( PublishedFileUpdateHandle_t updateHandle, string pchChangeDescription )
		{
			return _UpdatePublishedFileSetChangeDescription( Self, updateHandle, pchChangeDescription );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate SteamAPICall_t FUGCDownloadToLocation( IntPtr self, UGCHandle_t hContent, string pchLocation, uint unPriority );
		private FUGCDownloadToLocation _UGCDownloadToLocation;
		
		#endregion
		internal async Task<RemoteStorageDownloadUGCResult_t?> UGCDownloadToLocation( UGCHandle_t hContent, string pchLocation, uint unPriority )
		{
			return await (new Result<RemoteStorageDownloadUGCResult_t>( _UGCDownloadToLocation( Self, hContent, pchLocation, unPriority ) )).GetResult();
		}
		
	}
}
