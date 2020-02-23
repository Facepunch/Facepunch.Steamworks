using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Steamworks.Data;


namespace Steamworks
{
	internal class ISteamRemoteStorage : SteamInterface
	{
		
		internal ISteamRemoteStorage( bool IsGameServer )
		{
			SetupInterface( IsGameServer );
		}
		
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_SteamRemoteStorage_v014", CallingConvention = Platform.CC)]
		internal static extern IntPtr SteamAPI_SteamRemoteStorage_v014();
		public override IntPtr GetUserInterfacePointer() => SteamAPI_SteamRemoteStorage_v014();
		
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamRemoteStorage_FileWrite", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _FileWrite( IntPtr self, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchFile, IntPtr pvData, int cubData );
		
		#endregion
		internal bool FileWrite( [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchFile, IntPtr pvData, int cubData )
		{
			var returnValue = _FileWrite( Self, pchFile, pvData, cubData );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamRemoteStorage_FileRead", CallingConvention = Platform.CC)]
		private static extern int _FileRead( IntPtr self, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchFile, IntPtr pvData, int cubDataToRead );
		
		#endregion
		internal int FileRead( [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchFile, IntPtr pvData, int cubDataToRead )
		{
			var returnValue = _FileRead( Self, pchFile, pvData, cubDataToRead );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamRemoteStorage_FileWriteAsync", CallingConvention = Platform.CC)]
		private static extern SteamAPICall_t _FileWriteAsync( IntPtr self, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchFile, IntPtr pvData, uint cubData );
		
		#endregion
		internal CallResult<RemoteStorageFileWriteAsyncComplete_t> FileWriteAsync( [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchFile, IntPtr pvData, uint cubData )
		{
			var returnValue = _FileWriteAsync( Self, pchFile, pvData, cubData );
			return new CallResult<RemoteStorageFileWriteAsyncComplete_t>( returnValue, IsServer );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamRemoteStorage_FileReadAsync", CallingConvention = Platform.CC)]
		private static extern SteamAPICall_t _FileReadAsync( IntPtr self, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchFile, uint nOffset, uint cubToRead );
		
		#endregion
		internal CallResult<RemoteStorageFileReadAsyncComplete_t> FileReadAsync( [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchFile, uint nOffset, uint cubToRead )
		{
			var returnValue = _FileReadAsync( Self, pchFile, nOffset, cubToRead );
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
		private static extern bool _FileForget( IntPtr self, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchFile );
		
		#endregion
		internal bool FileForget( [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchFile )
		{
			var returnValue = _FileForget( Self, pchFile );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamRemoteStorage_FileDelete", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _FileDelete( IntPtr self, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchFile );
		
		#endregion
		internal bool FileDelete( [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchFile )
		{
			var returnValue = _FileDelete( Self, pchFile );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamRemoteStorage_FileShare", CallingConvention = Platform.CC)]
		private static extern SteamAPICall_t _FileShare( IntPtr self, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchFile );
		
		#endregion
		internal CallResult<RemoteStorageFileShareResult_t> FileShare( [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchFile )
		{
			var returnValue = _FileShare( Self, pchFile );
			return new CallResult<RemoteStorageFileShareResult_t>( returnValue, IsServer );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamRemoteStorage_SetSyncPlatforms", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _SetSyncPlatforms( IntPtr self, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchFile, RemoteStoragePlatform eRemoteStoragePlatform );
		
		#endregion
		internal bool SetSyncPlatforms( [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchFile, RemoteStoragePlatform eRemoteStoragePlatform )
		{
			var returnValue = _SetSyncPlatforms( Self, pchFile, eRemoteStoragePlatform );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamRemoteStorage_FileWriteStreamOpen", CallingConvention = Platform.CC)]
		private static extern UGCFileWriteStreamHandle_t _FileWriteStreamOpen( IntPtr self, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchFile );
		
		#endregion
		internal UGCFileWriteStreamHandle_t FileWriteStreamOpen( [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchFile )
		{
			var returnValue = _FileWriteStreamOpen( Self, pchFile );
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
		private static extern bool _FileExists( IntPtr self, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchFile );
		
		#endregion
		internal bool FileExists( [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchFile )
		{
			var returnValue = _FileExists( Self, pchFile );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamRemoteStorage_FilePersisted", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _FilePersisted( IntPtr self, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchFile );
		
		#endregion
		internal bool FilePersisted( [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchFile )
		{
			var returnValue = _FilePersisted( Self, pchFile );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamRemoteStorage_GetFileSize", CallingConvention = Platform.CC)]
		private static extern int _GetFileSize( IntPtr self, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchFile );
		
		#endregion
		internal int GetFileSize( [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchFile )
		{
			var returnValue = _GetFileSize( Self, pchFile );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamRemoteStorage_GetFileTimestamp", CallingConvention = Platform.CC)]
		private static extern long _GetFileTimestamp( IntPtr self, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchFile );
		
		#endregion
		internal long GetFileTimestamp( [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchFile )
		{
			var returnValue = _GetFileTimestamp( Self, pchFile );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamRemoteStorage_GetSyncPlatforms", CallingConvention = Platform.CC)]
		private static extern RemoteStoragePlatform _GetSyncPlatforms( IntPtr self, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchFile );
		
		#endregion
		internal RemoteStoragePlatform GetSyncPlatforms( [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchFile )
		{
			var returnValue = _GetSyncPlatforms( Self, pchFile );
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
		private static extern SteamAPICall_t _UGCDownloadToLocation( IntPtr self, UGCHandle_t hContent, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchLocation, uint unPriority );
		
		#endregion
		internal CallResult<RemoteStorageDownloadUGCResult_t> UGCDownloadToLocation( UGCHandle_t hContent, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchLocation, uint unPriority )
		{
			var returnValue = _UGCDownloadToLocation( Self, hContent, pchLocation, unPriority );
			return new CallResult<RemoteStorageDownloadUGCResult_t>( returnValue, IsServer );
		}
		
	}
}
