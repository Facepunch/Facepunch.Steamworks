using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Steamworks.Data;


namespace Steamworks
{
	internal unsafe partial class ISteamRemoteStorage : SteamInterface
	{
		public const string Version = "STEAMREMOTESTORAGE_INTERFACE_VERSION016";
		
		internal ISteamRemoteStorage( bool IsGameServer )
		{
			SetupInterface( IsGameServer );
		}
		
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_SteamRemoteStorage_v016", CallingConvention = Platform.CC)]
		internal static extern IntPtr SteamAPI_SteamRemoteStorage_v016();
		public override IntPtr GetUserInterfacePointer() => SteamAPI_SteamRemoteStorage_v016();
		
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamRemoteStorage_FileWrite", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _FileWrite( IntPtr self, IntPtr pchFile, IntPtr pvData, int cubData );
		
		#endregion
		internal bool FileWrite( string pchFile, IntPtr pvData, int cubData )
		{
			using var str__pchFile = new Utf8StringToNative( pchFile );
			var returnValue = _FileWrite( Self, str__pchFile.Pointer, pvData, cubData );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamRemoteStorage_FileRead", CallingConvention = Platform.CC)]
		private static extern int _FileRead( IntPtr self, IntPtr pchFile, IntPtr pvData, int cubDataToRead );
		
		#endregion
		internal int FileRead( string pchFile, IntPtr pvData, int cubDataToRead )
		{
			using var str__pchFile = new Utf8StringToNative( pchFile );
			var returnValue = _FileRead( Self, str__pchFile.Pointer, pvData, cubDataToRead );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamRemoteStorage_FileWriteAsync", CallingConvention = Platform.CC)]
		private static extern SteamAPICall_t _FileWriteAsync( IntPtr self, IntPtr pchFile, IntPtr pvData, uint cubData );
		
		#endregion
		internal CallResult<RemoteStorageFileWriteAsyncComplete_t> FileWriteAsync( string pchFile, IntPtr pvData, uint cubData )
		{
			using var str__pchFile = new Utf8StringToNative( pchFile );
			var returnValue = _FileWriteAsync( Self, str__pchFile.Pointer, pvData, cubData );
			return new CallResult<RemoteStorageFileWriteAsyncComplete_t>( returnValue, IsServer );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamRemoteStorage_FileReadAsync", CallingConvention = Platform.CC)]
		private static extern SteamAPICall_t _FileReadAsync( IntPtr self, IntPtr pchFile, uint nOffset, uint cubToRead );
		
		#endregion
		internal CallResult<RemoteStorageFileReadAsyncComplete_t> FileReadAsync( string pchFile, uint nOffset, uint cubToRead )
		{
			using var str__pchFile = new Utf8StringToNative( pchFile );
			var returnValue = _FileReadAsync( Self, str__pchFile.Pointer, nOffset, cubToRead );
			return new CallResult<RemoteStorageFileReadAsyncComplete_t>( returnValue, IsServer );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamRemoteStorage_FileReadAsyncComplete", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _FileReadAsyncComplete( IntPtr self, SteamAPICall_t hReadCall, IntPtr pvBuffer, uint cubToRead );
		
		#endregion
		internal bool FileReadAsyncComplete( SteamAPICall_t hReadCall, IntPtr pvBuffer, uint cubToRead )
		{
			var returnValue = _FileReadAsyncComplete( Self, hReadCall, pvBuffer, cubToRead );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamRemoteStorage_FileForget", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _FileForget( IntPtr self, IntPtr pchFile );
		
		#endregion
		internal bool FileForget( string pchFile )
		{
			using var str__pchFile = new Utf8StringToNative( pchFile );
			var returnValue = _FileForget( Self, str__pchFile.Pointer );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamRemoteStorage_FileDelete", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _FileDelete( IntPtr self, IntPtr pchFile );
		
		#endregion
		internal bool FileDelete( string pchFile )
		{
			using var str__pchFile = new Utf8StringToNative( pchFile );
			var returnValue = _FileDelete( Self, str__pchFile.Pointer );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamRemoteStorage_FileShare", CallingConvention = Platform.CC)]
		private static extern SteamAPICall_t _FileShare( IntPtr self, IntPtr pchFile );
		
		#endregion
		internal CallResult<RemoteStorageFileShareResult_t> FileShare( string pchFile )
		{
			using var str__pchFile = new Utf8StringToNative( pchFile );
			var returnValue = _FileShare( Self, str__pchFile.Pointer );
			return new CallResult<RemoteStorageFileShareResult_t>( returnValue, IsServer );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamRemoteStorage_SetSyncPlatforms", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _SetSyncPlatforms( IntPtr self, IntPtr pchFile, RemoteStoragePlatform eRemoteStoragePlatform );
		
		#endregion
		internal bool SetSyncPlatforms( string pchFile, RemoteStoragePlatform eRemoteStoragePlatform )
		{
			using var str__pchFile = new Utf8StringToNative( pchFile );
			var returnValue = _SetSyncPlatforms( Self, str__pchFile.Pointer, eRemoteStoragePlatform );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamRemoteStorage_FileWriteStreamOpen", CallingConvention = Platform.CC)]
		private static extern UGCFileWriteStreamHandle_t _FileWriteStreamOpen( IntPtr self, IntPtr pchFile );
		
		#endregion
		internal UGCFileWriteStreamHandle_t FileWriteStreamOpen( string pchFile )
		{
			using var str__pchFile = new Utf8StringToNative( pchFile );
			var returnValue = _FileWriteStreamOpen( Self, str__pchFile.Pointer );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamRemoteStorage_FileWriteStreamWriteChunk", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _FileWriteStreamWriteChunk( IntPtr self, UGCFileWriteStreamHandle_t writeHandle, IntPtr pvData, int cubData );
		
		#endregion
		internal bool FileWriteStreamWriteChunk( UGCFileWriteStreamHandle_t writeHandle, IntPtr pvData, int cubData )
		{
			var returnValue = _FileWriteStreamWriteChunk( Self, writeHandle, pvData, cubData );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamRemoteStorage_FileWriteStreamClose", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _FileWriteStreamClose( IntPtr self, UGCFileWriteStreamHandle_t writeHandle );
		
		#endregion
		internal bool FileWriteStreamClose( UGCFileWriteStreamHandle_t writeHandle )
		{
			var returnValue = _FileWriteStreamClose( Self, writeHandle );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamRemoteStorage_FileWriteStreamCancel", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _FileWriteStreamCancel( IntPtr self, UGCFileWriteStreamHandle_t writeHandle );
		
		#endregion
		internal bool FileWriteStreamCancel( UGCFileWriteStreamHandle_t writeHandle )
		{
			var returnValue = _FileWriteStreamCancel( Self, writeHandle );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamRemoteStorage_FileExists", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _FileExists( IntPtr self, IntPtr pchFile );
		
		#endregion
		internal bool FileExists( string pchFile )
		{
			using var str__pchFile = new Utf8StringToNative( pchFile );
			var returnValue = _FileExists( Self, str__pchFile.Pointer );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamRemoteStorage_FilePersisted", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _FilePersisted( IntPtr self, IntPtr pchFile );
		
		#endregion
		internal bool FilePersisted( string pchFile )
		{
			using var str__pchFile = new Utf8StringToNative( pchFile );
			var returnValue = _FilePersisted( Self, str__pchFile.Pointer );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamRemoteStorage_GetFileSize", CallingConvention = Platform.CC)]
		private static extern int _GetFileSize( IntPtr self, IntPtr pchFile );
		
		#endregion
		internal int GetFileSize( string pchFile )
		{
			using var str__pchFile = new Utf8StringToNative( pchFile );
			var returnValue = _GetFileSize( Self, str__pchFile.Pointer );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamRemoteStorage_GetFileTimestamp", CallingConvention = Platform.CC)]
		private static extern long _GetFileTimestamp( IntPtr self, IntPtr pchFile );
		
		#endregion
		internal long GetFileTimestamp( string pchFile )
		{
			using var str__pchFile = new Utf8StringToNative( pchFile );
			var returnValue = _GetFileTimestamp( Self, str__pchFile.Pointer );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamRemoteStorage_GetSyncPlatforms", CallingConvention = Platform.CC)]
		private static extern RemoteStoragePlatform _GetSyncPlatforms( IntPtr self, IntPtr pchFile );
		
		#endregion
		internal RemoteStoragePlatform GetSyncPlatforms( string pchFile )
		{
			using var str__pchFile = new Utf8StringToNative( pchFile );
			var returnValue = _GetSyncPlatforms( Self, str__pchFile.Pointer );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamRemoteStorage_GetFileCount", CallingConvention = Platform.CC)]
		private static extern int _GetFileCount( IntPtr self );
		
		#endregion
		internal int GetFileCount()
		{
			var returnValue = _GetFileCount( Self );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamRemoteStorage_GetFileNameAndSize", CallingConvention = Platform.CC)]
		private static extern Utf8StringPointer _GetFileNameAndSize( IntPtr self, int iFile, ref int pnFileSizeInBytes );
		
		#endregion
		internal string GetFileNameAndSize( int iFile, ref int pnFileSizeInBytes )
		{
			var returnValue = _GetFileNameAndSize( Self, iFile, ref pnFileSizeInBytes );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamRemoteStorage_GetQuota", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _GetQuota( IntPtr self, ref ulong pnTotalBytes, ref ulong puAvailableBytes );
		
		#endregion
		internal bool GetQuota( ref ulong pnTotalBytes, ref ulong puAvailableBytes )
		{
			var returnValue = _GetQuota( Self, ref pnTotalBytes, ref puAvailableBytes );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamRemoteStorage_IsCloudEnabledForAccount", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _IsCloudEnabledForAccount( IntPtr self );
		
		#endregion
		internal bool IsCloudEnabledForAccount()
		{
			var returnValue = _IsCloudEnabledForAccount( Self );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamRemoteStorage_IsCloudEnabledForApp", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _IsCloudEnabledForApp( IntPtr self );
		
		#endregion
		internal bool IsCloudEnabledForApp()
		{
			var returnValue = _IsCloudEnabledForApp( Self );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamRemoteStorage_SetCloudEnabledForApp", CallingConvention = Platform.CC)]
		private static extern void _SetCloudEnabledForApp( IntPtr self, [MarshalAs( UnmanagedType.U1 )] bool bEnabled );
		
		#endregion
		internal void SetCloudEnabledForApp( [MarshalAs( UnmanagedType.U1 )] bool bEnabled )
		{
			_SetCloudEnabledForApp( Self, bEnabled );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamRemoteStorage_UGCDownload", CallingConvention = Platform.CC)]
		private static extern SteamAPICall_t _UGCDownload( IntPtr self, UGCHandle_t hContent, uint unPriority );
		
		#endregion
		internal CallResult<RemoteStorageDownloadUGCResult_t> UGCDownload( UGCHandle_t hContent, uint unPriority )
		{
			var returnValue = _UGCDownload( Self, hContent, unPriority );
			return new CallResult<RemoteStorageDownloadUGCResult_t>( returnValue, IsServer );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamRemoteStorage_GetUGCDownloadProgress", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _GetUGCDownloadProgress( IntPtr self, UGCHandle_t hContent, ref int pnBytesDownloaded, ref int pnBytesExpected );
		
		#endregion
		internal bool GetUGCDownloadProgress( UGCHandle_t hContent, ref int pnBytesDownloaded, ref int pnBytesExpected )
		{
			var returnValue = _GetUGCDownloadProgress( Self, hContent, ref pnBytesDownloaded, ref pnBytesExpected );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamRemoteStorage_GetUGCDetails", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _GetUGCDetails( IntPtr self, UGCHandle_t hContent, ref AppId pnAppID, [In,Out] ref char[]  ppchName, ref int pnFileSizeInBytes, ref SteamId pSteamIDOwner );
		
		#endregion
		internal bool GetUGCDetails( UGCHandle_t hContent, ref AppId pnAppID, [In,Out] ref char[]  ppchName, ref int pnFileSizeInBytes, ref SteamId pSteamIDOwner )
		{
			var returnValue = _GetUGCDetails( Self, hContent, ref pnAppID, ref ppchName, ref pnFileSizeInBytes, ref pSteamIDOwner );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamRemoteStorage_UGCRead", CallingConvention = Platform.CC)]
		private static extern int _UGCRead( IntPtr self, UGCHandle_t hContent, IntPtr pvData, int cubDataToRead, uint cOffset, UGCReadAction eAction );
		
		#endregion
		internal int UGCRead( UGCHandle_t hContent, IntPtr pvData, int cubDataToRead, uint cOffset, UGCReadAction eAction )
		{
			var returnValue = _UGCRead( Self, hContent, pvData, cubDataToRead, cOffset, eAction );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamRemoteStorage_GetCachedUGCCount", CallingConvention = Platform.CC)]
		private static extern int _GetCachedUGCCount( IntPtr self );
		
		#endregion
		internal int GetCachedUGCCount()
		{
			var returnValue = _GetCachedUGCCount( Self );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamRemoteStorage_GetCachedUGCHandle", CallingConvention = Platform.CC)]
		private static extern UGCHandle_t _GetCachedUGCHandle( IntPtr self, int iCachedContent );
		
		#endregion
		internal UGCHandle_t GetCachedUGCHandle( int iCachedContent )
		{
			var returnValue = _GetCachedUGCHandle( Self, iCachedContent );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamRemoteStorage_UGCDownloadToLocation", CallingConvention = Platform.CC)]
		private static extern SteamAPICall_t _UGCDownloadToLocation( IntPtr self, UGCHandle_t hContent, IntPtr pchLocation, uint unPriority );
		
		#endregion
		internal CallResult<RemoteStorageDownloadUGCResult_t> UGCDownloadToLocation( UGCHandle_t hContent, string pchLocation, uint unPriority )
		{
			using var str__pchLocation = new Utf8StringToNative( pchLocation );
			var returnValue = _UGCDownloadToLocation( Self, hContent, str__pchLocation.Pointer, unPriority );
			return new CallResult<RemoteStorageDownloadUGCResult_t>( returnValue, IsServer );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamRemoteStorage_GetLocalFileChangeCount", CallingConvention = Platform.CC)]
		private static extern int _GetLocalFileChangeCount( IntPtr self );
		
		#endregion
		internal int GetLocalFileChangeCount()
		{
			var returnValue = _GetLocalFileChangeCount( Self );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamRemoteStorage_GetLocalFileChange", CallingConvention = Platform.CC)]
		private static extern Utf8StringPointer _GetLocalFileChange( IntPtr self, int iFile, ref RemoteStorageLocalFileChange pEChangeType, ref RemoteStorageFilePathType pEFilePathType );
		
		#endregion
		internal string GetLocalFileChange( int iFile, ref RemoteStorageLocalFileChange pEChangeType, ref RemoteStorageFilePathType pEFilePathType )
		{
			var returnValue = _GetLocalFileChange( Self, iFile, ref pEChangeType, ref pEFilePathType );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamRemoteStorage_BeginFileWriteBatch", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _BeginFileWriteBatch( IntPtr self );
		
		#endregion
		internal bool BeginFileWriteBatch()
		{
			var returnValue = _BeginFileWriteBatch( Self );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamRemoteStorage_EndFileWriteBatch", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _EndFileWriteBatch( IntPtr self );
		
		#endregion
		internal bool EndFileWriteBatch()
		{
			var returnValue = _EndFileWriteBatch( Self );
			return returnValue;
		}
		
	}
}
