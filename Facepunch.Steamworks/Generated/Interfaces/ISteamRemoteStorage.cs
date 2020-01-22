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
			_FileWrite = Marshal.GetDelegateForFunctionPointer<FFileWrite>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 0 ) ) );
			_FileRead = Marshal.GetDelegateForFunctionPointer<FFileRead>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 8 ) ) );
			_FileWriteAsync = Marshal.GetDelegateForFunctionPointer<FFileWriteAsync>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 16 ) ) );
			_FileReadAsync = Marshal.GetDelegateForFunctionPointer<FFileReadAsync>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 24 ) ) );
			_FileReadAsyncComplete = Marshal.GetDelegateForFunctionPointer<FFileReadAsyncComplete>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 32 ) ) );
			_FileForget = Marshal.GetDelegateForFunctionPointer<FFileForget>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 40 ) ) );
			_FileDelete = Marshal.GetDelegateForFunctionPointer<FFileDelete>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 48 ) ) );
			_FileShare = Marshal.GetDelegateForFunctionPointer<FFileShare>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 56 ) ) );
			_SetSyncPlatforms = Marshal.GetDelegateForFunctionPointer<FSetSyncPlatforms>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 64 ) ) );
			_FileWriteStreamOpen = Marshal.GetDelegateForFunctionPointer<FFileWriteStreamOpen>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 72 ) ) );
			_FileWriteStreamWriteChunk = Marshal.GetDelegateForFunctionPointer<FFileWriteStreamWriteChunk>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 80 ) ) );
			_FileWriteStreamClose = Marshal.GetDelegateForFunctionPointer<FFileWriteStreamClose>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 88 ) ) );
			_FileWriteStreamCancel = Marshal.GetDelegateForFunctionPointer<FFileWriteStreamCancel>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 96 ) ) );
			_FileExists = Marshal.GetDelegateForFunctionPointer<FFileExists>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 104 ) ) );
			_FilePersisted = Marshal.GetDelegateForFunctionPointer<FFilePersisted>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 112 ) ) );
			_GetFileSize = Marshal.GetDelegateForFunctionPointer<FGetFileSize>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 120 ) ) );
			_GetFileTimestamp = Marshal.GetDelegateForFunctionPointer<FGetFileTimestamp>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 128 ) ) );
			_GetSyncPlatforms = Marshal.GetDelegateForFunctionPointer<FGetSyncPlatforms>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 136 ) ) );
			_GetFileCount = Marshal.GetDelegateForFunctionPointer<FGetFileCount>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 144 ) ) );
			_GetFileNameAndSize = Marshal.GetDelegateForFunctionPointer<FGetFileNameAndSize>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 152 ) ) );
			_GetQuota = Marshal.GetDelegateForFunctionPointer<FGetQuota>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 160 ) ) );
			_IsCloudEnabledForAccount = Marshal.GetDelegateForFunctionPointer<FIsCloudEnabledForAccount>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 168 ) ) );
			_IsCloudEnabledForApp = Marshal.GetDelegateForFunctionPointer<FIsCloudEnabledForApp>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 176 ) ) );
			_SetCloudEnabledForApp = Marshal.GetDelegateForFunctionPointer<FSetCloudEnabledForApp>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 184 ) ) );
			_UGCDownload = Marshal.GetDelegateForFunctionPointer<FUGCDownload>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 192 ) ) );
			_GetUGCDownloadProgress = Marshal.GetDelegateForFunctionPointer<FGetUGCDownloadProgress>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 200 ) ) );
			_GetUGCDetails = Marshal.GetDelegateForFunctionPointer<FGetUGCDetails>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 208 ) ) );
			_UGCRead = Marshal.GetDelegateForFunctionPointer<FUGCRead>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 216 ) ) );
			_GetCachedUGCCount = Marshal.GetDelegateForFunctionPointer<FGetCachedUGCCount>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 224 ) ) );
			 // PublishWorkshopFile is deprecated
			 // CreatePublishedFileUpdateRequest is deprecated
			 // UpdatePublishedFileFile is deprecated
			 // UpdatePublishedFilePreviewFile is deprecated
			 // UpdatePublishedFileTitle is deprecated
			 // UpdatePublishedFileDescription is deprecated
			 // UpdatePublishedFileVisibility is deprecated
			 // UpdatePublishedFileTags is deprecated
			 // CommitPublishedFileUpdate is deprecated
			 // GetPublishedFileDetails is deprecated
			 // DeletePublishedFile is deprecated
			 // EnumerateUserPublishedFiles is deprecated
			 // SubscribePublishedFile is deprecated
			 // EnumerateUserSubscribedFiles is deprecated
			 // UnsubscribePublishedFile is deprecated
			 // UpdatePublishedFileSetChangeDescription is deprecated
			 // GetPublishedItemVoteDetails is deprecated
			 // UpdateUserPublishedItemVote is deprecated
			 // GetUserPublishedItemVoteDetails is deprecated
			 // EnumerateUserSharedWorkshopFiles is deprecated
			 // PublishVideo is deprecated
			 // SetUserPublishedFileAction is deprecated
			 // EnumeratePublishedFilesByUserAction is deprecated
			 // EnumeratePublishedWorkshopFiles is deprecated
			_UGCDownloadToLocation = Marshal.GetDelegateForFunctionPointer<FUGCDownloadToLocation>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 424 ) ) );
		}
		internal override void Shutdown()
		{
			base.Shutdown();
			
			_FileWrite = null;
			_FileRead = null;
			_FileWriteAsync = null;
			_FileReadAsync = null;
			_FileReadAsyncComplete = null;
			_FileForget = null;
			_FileDelete = null;
			_FileShare = null;
			_SetSyncPlatforms = null;
			_FileWriteStreamOpen = null;
			_FileWriteStreamWriteChunk = null;
			_FileWriteStreamClose = null;
			_FileWriteStreamCancel = null;
			_FileExists = null;
			_FilePersisted = null;
			_GetFileSize = null;
			_GetFileTimestamp = null;
			_GetSyncPlatforms = null;
			_GetFileCount = null;
			_GetFileNameAndSize = null;
			_GetQuota = null;
			_IsCloudEnabledForAccount = null;
			_IsCloudEnabledForApp = null;
			_SetCloudEnabledForApp = null;
			_UGCDownload = null;
			_GetUGCDownloadProgress = null;
			_GetUGCDetails = null;
			_UGCRead = null;
			_GetCachedUGCCount = null;
			_UGCDownloadToLocation = null;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FFileWrite( IntPtr self, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchFile, IntPtr pvData, int cubData );
		private FFileWrite _FileWrite;
		
		#endregion
		internal bool FileWrite( [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchFile, IntPtr pvData, int cubData )
		{
			var returnValue = _FileWrite( Self, pchFile, pvData, cubData );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate int FFileRead( IntPtr self, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchFile, IntPtr pvData, int cubDataToRead );
		private FFileRead _FileRead;
		
		#endregion
		internal int FileRead( [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchFile, IntPtr pvData, int cubDataToRead )
		{
			var returnValue = _FileRead( Self, pchFile, pvData, cubDataToRead );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate SteamAPICall_t FFileWriteAsync( IntPtr self, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchFile, IntPtr pvData, uint cubData );
		private FFileWriteAsync _FileWriteAsync;
		
		#endregion
		internal async Task<RemoteStorageFileWriteAsyncComplete_t?> FileWriteAsync( [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchFile, IntPtr pvData, uint cubData )
		{
			var returnValue = _FileWriteAsync( Self, pchFile, pvData, cubData );
			return await RemoteStorageFileWriteAsyncComplete_t.GetResultAsync( returnValue );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate SteamAPICall_t FFileReadAsync( IntPtr self, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchFile, uint nOffset, uint cubToRead );
		private FFileReadAsync _FileReadAsync;
		
		#endregion
		internal async Task<RemoteStorageFileReadAsyncComplete_t?> FileReadAsync( [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchFile, uint nOffset, uint cubToRead )
		{
			var returnValue = _FileReadAsync( Self, pchFile, nOffset, cubToRead );
			return await RemoteStorageFileReadAsyncComplete_t.GetResultAsync( returnValue );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FFileReadAsyncComplete( IntPtr self, SteamAPICall_t hReadCall, IntPtr pvBuffer, uint cubToRead );
		private FFileReadAsyncComplete _FileReadAsyncComplete;
		
		#endregion
		internal bool FileReadAsyncComplete( SteamAPICall_t hReadCall, IntPtr pvBuffer, uint cubToRead )
		{
			var returnValue = _FileReadAsyncComplete( Self, hReadCall, pvBuffer, cubToRead );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FFileForget( IntPtr self, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchFile );
		private FFileForget _FileForget;
		
		#endregion
		internal bool FileForget( [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchFile )
		{
			var returnValue = _FileForget( Self, pchFile );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FFileDelete( IntPtr self, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchFile );
		private FFileDelete _FileDelete;
		
		#endregion
		internal bool FileDelete( [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchFile )
		{
			var returnValue = _FileDelete( Self, pchFile );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate SteamAPICall_t FFileShare( IntPtr self, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchFile );
		private FFileShare _FileShare;
		
		#endregion
		internal async Task<RemoteStorageFileShareResult_t?> FileShare( [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchFile )
		{
			var returnValue = _FileShare( Self, pchFile );
			return await RemoteStorageFileShareResult_t.GetResultAsync( returnValue );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FSetSyncPlatforms( IntPtr self, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchFile, RemoteStoragePlatform eRemoteStoragePlatform );
		private FSetSyncPlatforms _SetSyncPlatforms;
		
		#endregion
		internal bool SetSyncPlatforms( [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchFile, RemoteStoragePlatform eRemoteStoragePlatform )
		{
			var returnValue = _SetSyncPlatforms( Self, pchFile, eRemoteStoragePlatform );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate UGCFileWriteStreamHandle_t FFileWriteStreamOpen( IntPtr self, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchFile );
		private FFileWriteStreamOpen _FileWriteStreamOpen;
		
		#endregion
		internal UGCFileWriteStreamHandle_t FileWriteStreamOpen( [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchFile )
		{
			var returnValue = _FileWriteStreamOpen( Self, pchFile );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FFileWriteStreamWriteChunk( IntPtr self, UGCFileWriteStreamHandle_t writeHandle, IntPtr pvData, int cubData );
		private FFileWriteStreamWriteChunk _FileWriteStreamWriteChunk;
		
		#endregion
		internal bool FileWriteStreamWriteChunk( UGCFileWriteStreamHandle_t writeHandle, IntPtr pvData, int cubData )
		{
			var returnValue = _FileWriteStreamWriteChunk( Self, writeHandle, pvData, cubData );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FFileWriteStreamClose( IntPtr self, UGCFileWriteStreamHandle_t writeHandle );
		private FFileWriteStreamClose _FileWriteStreamClose;
		
		#endregion
		internal bool FileWriteStreamClose( UGCFileWriteStreamHandle_t writeHandle )
		{
			var returnValue = _FileWriteStreamClose( Self, writeHandle );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FFileWriteStreamCancel( IntPtr self, UGCFileWriteStreamHandle_t writeHandle );
		private FFileWriteStreamCancel _FileWriteStreamCancel;
		
		#endregion
		internal bool FileWriteStreamCancel( UGCFileWriteStreamHandle_t writeHandle )
		{
			var returnValue = _FileWriteStreamCancel( Self, writeHandle );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FFileExists( IntPtr self, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchFile );
		private FFileExists _FileExists;
		
		#endregion
		internal bool FileExists( [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchFile )
		{
			var returnValue = _FileExists( Self, pchFile );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FFilePersisted( IntPtr self, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchFile );
		private FFilePersisted _FilePersisted;
		
		#endregion
		internal bool FilePersisted( [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchFile )
		{
			var returnValue = _FilePersisted( Self, pchFile );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate int FGetFileSize( IntPtr self, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchFile );
		private FGetFileSize _GetFileSize;
		
		#endregion
		internal int GetFileSize( [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchFile )
		{
			var returnValue = _GetFileSize( Self, pchFile );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate long FGetFileTimestamp( IntPtr self, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchFile );
		private FGetFileTimestamp _GetFileTimestamp;
		
		#endregion
		internal long GetFileTimestamp( [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchFile )
		{
			var returnValue = _GetFileTimestamp( Self, pchFile );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate RemoteStoragePlatform FGetSyncPlatforms( IntPtr self, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchFile );
		private FGetSyncPlatforms _GetSyncPlatforms;
		
		#endregion
		internal RemoteStoragePlatform GetSyncPlatforms( [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchFile )
		{
			var returnValue = _GetSyncPlatforms( Self, pchFile );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate int FGetFileCount( IntPtr self );
		private FGetFileCount _GetFileCount;
		
		#endregion
		internal int GetFileCount()
		{
			var returnValue = _GetFileCount( Self );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate Utf8StringPointer FGetFileNameAndSize( IntPtr self, int iFile, ref int pnFileSizeInBytes );
		private FGetFileNameAndSize _GetFileNameAndSize;
		
		#endregion
		internal string GetFileNameAndSize( int iFile, ref int pnFileSizeInBytes )
		{
			var returnValue = _GetFileNameAndSize( Self, iFile, ref pnFileSizeInBytes );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FGetQuota( IntPtr self, ref ulong pnTotalBytes, ref ulong puAvailableBytes );
		private FGetQuota _GetQuota;
		
		#endregion
		internal bool GetQuota( ref ulong pnTotalBytes, ref ulong puAvailableBytes )
		{
			var returnValue = _GetQuota( Self, ref pnTotalBytes, ref puAvailableBytes );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FIsCloudEnabledForAccount( IntPtr self );
		private FIsCloudEnabledForAccount _IsCloudEnabledForAccount;
		
		#endregion
		internal bool IsCloudEnabledForAccount()
		{
			var returnValue = _IsCloudEnabledForAccount( Self );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FIsCloudEnabledForApp( IntPtr self );
		private FIsCloudEnabledForApp _IsCloudEnabledForApp;
		
		#endregion
		internal bool IsCloudEnabledForApp()
		{
			var returnValue = _IsCloudEnabledForApp( Self );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate void FSetCloudEnabledForApp( IntPtr self, [MarshalAs( UnmanagedType.U1 )] bool bEnabled );
		private FSetCloudEnabledForApp _SetCloudEnabledForApp;
		
		#endregion
		internal void SetCloudEnabledForApp( [MarshalAs( UnmanagedType.U1 )] bool bEnabled )
		{
			_SetCloudEnabledForApp( Self, bEnabled );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate SteamAPICall_t FUGCDownload( IntPtr self, UGCHandle_t hContent, uint unPriority );
		private FUGCDownload _UGCDownload;
		
		#endregion
		internal async Task<RemoteStorageDownloadUGCResult_t?> UGCDownload( UGCHandle_t hContent, uint unPriority )
		{
			var returnValue = _UGCDownload( Self, hContent, unPriority );
			return await RemoteStorageDownloadUGCResult_t.GetResultAsync( returnValue );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FGetUGCDownloadProgress( IntPtr self, UGCHandle_t hContent, ref int pnBytesDownloaded, ref int pnBytesExpected );
		private FGetUGCDownloadProgress _GetUGCDownloadProgress;
		
		#endregion
		internal bool GetUGCDownloadProgress( UGCHandle_t hContent, ref int pnBytesDownloaded, ref int pnBytesExpected )
		{
			var returnValue = _GetUGCDownloadProgress( Self, hContent, ref pnBytesDownloaded, ref pnBytesExpected );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FGetUGCDetails( IntPtr self, UGCHandle_t hContent, ref AppId pnAppID, [In,Out] ref char[]  ppchName, ref int pnFileSizeInBytes, ref SteamId pSteamIDOwner );
		private FGetUGCDetails _GetUGCDetails;
		
		#endregion
		internal bool GetUGCDetails( UGCHandle_t hContent, ref AppId pnAppID, [In,Out] ref char[]  ppchName, ref int pnFileSizeInBytes, ref SteamId pSteamIDOwner )
		{
			var returnValue = _GetUGCDetails( Self, hContent, ref pnAppID, ref ppchName, ref pnFileSizeInBytes, ref pSteamIDOwner );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate int FUGCRead( IntPtr self, UGCHandle_t hContent, IntPtr pvData, int cubDataToRead, uint cOffset, UGCReadAction eAction );
		private FUGCRead _UGCRead;
		
		#endregion
		internal int UGCRead( UGCHandle_t hContent, IntPtr pvData, int cubDataToRead, uint cOffset, UGCReadAction eAction )
		{
			var returnValue = _UGCRead( Self, hContent, pvData, cubDataToRead, cOffset, eAction );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate int FGetCachedUGCCount( IntPtr self );
		private FGetCachedUGCCount _GetCachedUGCCount;
		
		#endregion
		internal int GetCachedUGCCount()
		{
			var returnValue = _GetCachedUGCCount( Self );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate SteamAPICall_t FUGCDownloadToLocation( IntPtr self, UGCHandle_t hContent, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchLocation, uint unPriority );
		private FUGCDownloadToLocation _UGCDownloadToLocation;
		
		#endregion
		internal async Task<RemoteStorageDownloadUGCResult_t?> UGCDownloadToLocation( UGCHandle_t hContent, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchLocation, uint unPriority )
		{
			var returnValue = _UGCDownloadToLocation( Self, hContent, pchLocation, unPriority );
			return await RemoteStorageDownloadUGCResult_t.GetResultAsync( returnValue );
		}
		
	}
}
