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
		}
		internal override void Shutdown()
		{
			base.Shutdown();
			
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
