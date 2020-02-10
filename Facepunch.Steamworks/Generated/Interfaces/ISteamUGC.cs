using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Steamworks.Data;


namespace Steamworks
{
	internal class ISteamUGC : SteamInterface
	{
		public override IntPtr GetInterfacePointer() => GetApi.SteamUGC();
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamUGC_CreateQueryUserUGCRequest")]
		private static extern UGCQueryHandle_t _CreateQueryUserUGCRequest( IntPtr self, AccountID_t unAccountID, UserUGCList eListType, UgcType eMatchingUGCType, UserUGCListSortOrder eSortOrder, AppId nCreatorAppID, AppId nConsumerAppID, uint unPage );
		
		#endregion
		internal UGCQueryHandle_t CreateQueryUserUGCRequest( AccountID_t unAccountID, UserUGCList eListType, UgcType eMatchingUGCType, UserUGCListSortOrder eSortOrder, AppId nCreatorAppID, AppId nConsumerAppID, uint unPage )
		{
			var returnValue = _CreateQueryUserUGCRequest( Self, unAccountID, eListType, eMatchingUGCType, eSortOrder, nCreatorAppID, nConsumerAppID, unPage );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamUGC_CreateQueryAllUGCRequest")]
		private static extern UGCQueryHandle_t _CreateQueryAllUGCRequest( IntPtr self, UGCQuery eQueryType, UgcType eMatchingeMatchingUGCTypeFileType, AppId nCreatorAppID, AppId nConsumerAppID, uint unPage );
		
		#endregion
		internal UGCQueryHandle_t CreateQueryAllUGCRequest( UGCQuery eQueryType, UgcType eMatchingeMatchingUGCTypeFileType, AppId nCreatorAppID, AppId nConsumerAppID, uint unPage )
		{
			var returnValue = _CreateQueryAllUGCRequest( Self, eQueryType, eMatchingeMatchingUGCTypeFileType, nCreatorAppID, nConsumerAppID, unPage );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamUGC_CreateQueryAllUGCRequest0")]
		private static extern UGCQueryHandle_t _CreateQueryAllUGCRequest0( IntPtr self, UGCQuery eQueryType, UgcType eMatchingeMatchingUGCTypeFileType, AppId nCreatorAppID, AppId nConsumerAppID, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchCursor );
		
		#endregion
		internal UGCQueryHandle_t CreateQueryAllUGCRequest0( UGCQuery eQueryType, UgcType eMatchingeMatchingUGCTypeFileType, AppId nCreatorAppID, AppId nConsumerAppID, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchCursor )
		{
			var returnValue = _CreateQueryAllUGCRequest0( Self, eQueryType, eMatchingeMatchingUGCTypeFileType, nCreatorAppID, nConsumerAppID, pchCursor );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamUGC_CreateQueryUGCDetailsRequest")]
		private static extern UGCQueryHandle_t _CreateQueryUGCDetailsRequest( IntPtr self, [In,Out] PublishedFileId[]  pvecPublishedFileID, uint unNumPublishedFileIDs );
		
		#endregion
		internal UGCQueryHandle_t CreateQueryUGCDetailsRequest( [In,Out] PublishedFileId[]  pvecPublishedFileID, uint unNumPublishedFileIDs )
		{
			var returnValue = _CreateQueryUGCDetailsRequest( Self, pvecPublishedFileID, unNumPublishedFileIDs );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamUGC_SendQueryUGCRequest")]
		private static extern SteamAPICall_t _SendQueryUGCRequest( IntPtr self, UGCQueryHandle_t handle );
		
		#endregion
		internal CallbackResult SendQueryUGCRequest( UGCQueryHandle_t handle )
		{
			var returnValue = _SendQueryUGCRequest( Self, handle );
			return new CallbackResult( returnValue );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamUGC_GetQueryUGCResult")]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _GetQueryUGCResult( IntPtr self, UGCQueryHandle_t handle, uint index, ref SteamUGCDetails_t pDetails );
		
		#endregion
		internal bool GetQueryUGCResult( UGCQueryHandle_t handle, uint index, ref SteamUGCDetails_t pDetails )
		{
			var returnValue = _GetQueryUGCResult( Self, handle, index, ref pDetails );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamUGC_GetQueryUGCPreviewURL")]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _GetQueryUGCPreviewURL( IntPtr self, UGCQueryHandle_t handle, uint index, IntPtr pchURL, uint cchURLSize );
		
		#endregion
		internal bool GetQueryUGCPreviewURL( UGCQueryHandle_t handle, uint index, out string pchURL )
		{
			IntPtr mempchURL = Helpers.TakeMemory();
			var returnValue = _GetQueryUGCPreviewURL( Self, handle, index, mempchURL, (1024 * 32) );
			pchURL = Helpers.MemoryToString( mempchURL );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamUGC_GetQueryUGCMetadata")]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _GetQueryUGCMetadata( IntPtr self, UGCQueryHandle_t handle, uint index, IntPtr pchMetadata, uint cchMetadatasize );
		
		#endregion
		internal bool GetQueryUGCMetadata( UGCQueryHandle_t handle, uint index, out string pchMetadata )
		{
			IntPtr mempchMetadata = Helpers.TakeMemory();
			var returnValue = _GetQueryUGCMetadata( Self, handle, index, mempchMetadata, (1024 * 32) );
			pchMetadata = Helpers.MemoryToString( mempchMetadata );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamUGC_GetQueryUGCChildren")]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _GetQueryUGCChildren( IntPtr self, UGCQueryHandle_t handle, uint index, [In,Out] PublishedFileId[]  pvecPublishedFileID, uint cMaxEntries );
		
		#endregion
		internal bool GetQueryUGCChildren( UGCQueryHandle_t handle, uint index, [In,Out] PublishedFileId[]  pvecPublishedFileID, uint cMaxEntries )
		{
			var returnValue = _GetQueryUGCChildren( Self, handle, index, pvecPublishedFileID, cMaxEntries );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamUGC_GetQueryUGCStatistic")]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _GetQueryUGCStatistic( IntPtr self, UGCQueryHandle_t handle, uint index, ItemStatistic eStatType, ref ulong pStatValue );
		
		#endregion
		internal bool GetQueryUGCStatistic( UGCQueryHandle_t handle, uint index, ItemStatistic eStatType, ref ulong pStatValue )
		{
			var returnValue = _GetQueryUGCStatistic( Self, handle, index, eStatType, ref pStatValue );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamUGC_GetQueryUGCNumAdditionalPreviews")]
		private static extern uint _GetQueryUGCNumAdditionalPreviews( IntPtr self, UGCQueryHandle_t handle, uint index );
		
		#endregion
		internal uint GetQueryUGCNumAdditionalPreviews( UGCQueryHandle_t handle, uint index )
		{
			var returnValue = _GetQueryUGCNumAdditionalPreviews( Self, handle, index );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamUGC_GetQueryUGCAdditionalPreview")]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _GetQueryUGCAdditionalPreview( IntPtr self, UGCQueryHandle_t handle, uint index, uint previewIndex, IntPtr pchURLOrVideoID, uint cchURLSize, IntPtr pchOriginalFileName, uint cchOriginalFileNameSize, ref ItemPreviewType pPreviewType );
		
		#endregion
		internal bool GetQueryUGCAdditionalPreview( UGCQueryHandle_t handle, uint index, uint previewIndex, out string pchURLOrVideoID, out string pchOriginalFileName, ref ItemPreviewType pPreviewType )
		{
			IntPtr mempchURLOrVideoID = Helpers.TakeMemory();
			IntPtr mempchOriginalFileName = Helpers.TakeMemory();
			var returnValue = _GetQueryUGCAdditionalPreview( Self, handle, index, previewIndex, mempchURLOrVideoID, (1024 * 32), mempchOriginalFileName, (1024 * 32), ref pPreviewType );
			pchURLOrVideoID = Helpers.MemoryToString( mempchURLOrVideoID );
			pchOriginalFileName = Helpers.MemoryToString( mempchOriginalFileName );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamUGC_GetQueryUGCNumKeyValueTags")]
		private static extern uint _GetQueryUGCNumKeyValueTags( IntPtr self, UGCQueryHandle_t handle, uint index );
		
		#endregion
		internal uint GetQueryUGCNumKeyValueTags( UGCQueryHandle_t handle, uint index )
		{
			var returnValue = _GetQueryUGCNumKeyValueTags( Self, handle, index );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamUGC_GetQueryUGCKeyValueTag")]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _GetQueryUGCKeyValueTag( IntPtr self, UGCQueryHandle_t handle, uint index, uint keyValueTagIndex, IntPtr pchKey, uint cchKeySize, IntPtr pchValue, uint cchValueSize );
		
		#endregion
		internal bool GetQueryUGCKeyValueTag( UGCQueryHandle_t handle, uint index, uint keyValueTagIndex, out string pchKey, out string pchValue )
		{
			IntPtr mempchKey = Helpers.TakeMemory();
			IntPtr mempchValue = Helpers.TakeMemory();
			var returnValue = _GetQueryUGCKeyValueTag( Self, handle, index, keyValueTagIndex, mempchKey, (1024 * 32), mempchValue, (1024 * 32) );
			pchKey = Helpers.MemoryToString( mempchKey );
			pchValue = Helpers.MemoryToString( mempchValue );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamUGC_ReleaseQueryUGCRequest")]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _ReleaseQueryUGCRequest( IntPtr self, UGCQueryHandle_t handle );
		
		#endregion
		internal bool ReleaseQueryUGCRequest( UGCQueryHandle_t handle )
		{
			var returnValue = _ReleaseQueryUGCRequest( Self, handle );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamUGC_AddRequiredTag")]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _AddRequiredTag( IntPtr self, UGCQueryHandle_t handle, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pTagName );
		
		#endregion
		internal bool AddRequiredTag( UGCQueryHandle_t handle, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pTagName )
		{
			var returnValue = _AddRequiredTag( Self, handle, pTagName );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamUGC_AddExcludedTag")]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _AddExcludedTag( IntPtr self, UGCQueryHandle_t handle, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pTagName );
		
		#endregion
		internal bool AddExcludedTag( UGCQueryHandle_t handle, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pTagName )
		{
			var returnValue = _AddExcludedTag( Self, handle, pTagName );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamUGC_SetReturnOnlyIDs")]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _SetReturnOnlyIDs( IntPtr self, UGCQueryHandle_t handle, [MarshalAs( UnmanagedType.U1 )] bool bReturnOnlyIDs );
		
		#endregion
		internal bool SetReturnOnlyIDs( UGCQueryHandle_t handle, [MarshalAs( UnmanagedType.U1 )] bool bReturnOnlyIDs )
		{
			var returnValue = _SetReturnOnlyIDs( Self, handle, bReturnOnlyIDs );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamUGC_SetReturnKeyValueTags")]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _SetReturnKeyValueTags( IntPtr self, UGCQueryHandle_t handle, [MarshalAs( UnmanagedType.U1 )] bool bReturnKeyValueTags );
		
		#endregion
		internal bool SetReturnKeyValueTags( UGCQueryHandle_t handle, [MarshalAs( UnmanagedType.U1 )] bool bReturnKeyValueTags )
		{
			var returnValue = _SetReturnKeyValueTags( Self, handle, bReturnKeyValueTags );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamUGC_SetReturnLongDescription")]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _SetReturnLongDescription( IntPtr self, UGCQueryHandle_t handle, [MarshalAs( UnmanagedType.U1 )] bool bReturnLongDescription );
		
		#endregion
		internal bool SetReturnLongDescription( UGCQueryHandle_t handle, [MarshalAs( UnmanagedType.U1 )] bool bReturnLongDescription )
		{
			var returnValue = _SetReturnLongDescription( Self, handle, bReturnLongDescription );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamUGC_SetReturnMetadata")]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _SetReturnMetadata( IntPtr self, UGCQueryHandle_t handle, [MarshalAs( UnmanagedType.U1 )] bool bReturnMetadata );
		
		#endregion
		internal bool SetReturnMetadata( UGCQueryHandle_t handle, [MarshalAs( UnmanagedType.U1 )] bool bReturnMetadata )
		{
			var returnValue = _SetReturnMetadata( Self, handle, bReturnMetadata );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamUGC_SetReturnChildren")]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _SetReturnChildren( IntPtr self, UGCQueryHandle_t handle, [MarshalAs( UnmanagedType.U1 )] bool bReturnChildren );
		
		#endregion
		internal bool SetReturnChildren( UGCQueryHandle_t handle, [MarshalAs( UnmanagedType.U1 )] bool bReturnChildren )
		{
			var returnValue = _SetReturnChildren( Self, handle, bReturnChildren );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamUGC_SetReturnAdditionalPreviews")]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _SetReturnAdditionalPreviews( IntPtr self, UGCQueryHandle_t handle, [MarshalAs( UnmanagedType.U1 )] bool bReturnAdditionalPreviews );
		
		#endregion
		internal bool SetReturnAdditionalPreviews( UGCQueryHandle_t handle, [MarshalAs( UnmanagedType.U1 )] bool bReturnAdditionalPreviews )
		{
			var returnValue = _SetReturnAdditionalPreviews( Self, handle, bReturnAdditionalPreviews );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamUGC_SetReturnTotalOnly")]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _SetReturnTotalOnly( IntPtr self, UGCQueryHandle_t handle, [MarshalAs( UnmanagedType.U1 )] bool bReturnTotalOnly );
		
		#endregion
		internal bool SetReturnTotalOnly( UGCQueryHandle_t handle, [MarshalAs( UnmanagedType.U1 )] bool bReturnTotalOnly )
		{
			var returnValue = _SetReturnTotalOnly( Self, handle, bReturnTotalOnly );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamUGC_SetReturnPlaytimeStats")]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _SetReturnPlaytimeStats( IntPtr self, UGCQueryHandle_t handle, uint unDays );
		
		#endregion
		internal bool SetReturnPlaytimeStats( UGCQueryHandle_t handle, uint unDays )
		{
			var returnValue = _SetReturnPlaytimeStats( Self, handle, unDays );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamUGC_SetLanguage")]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _SetLanguage( IntPtr self, UGCQueryHandle_t handle, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchLanguage );
		
		#endregion
		internal bool SetLanguage( UGCQueryHandle_t handle, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchLanguage )
		{
			var returnValue = _SetLanguage( Self, handle, pchLanguage );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamUGC_SetAllowCachedResponse")]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _SetAllowCachedResponse( IntPtr self, UGCQueryHandle_t handle, uint unMaxAgeSeconds );
		
		#endregion
		internal bool SetAllowCachedResponse( UGCQueryHandle_t handle, uint unMaxAgeSeconds )
		{
			var returnValue = _SetAllowCachedResponse( Self, handle, unMaxAgeSeconds );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamUGC_SetCloudFileNameFilter")]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _SetCloudFileNameFilter( IntPtr self, UGCQueryHandle_t handle, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pMatchCloudFileName );
		
		#endregion
		internal bool SetCloudFileNameFilter( UGCQueryHandle_t handle, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pMatchCloudFileName )
		{
			var returnValue = _SetCloudFileNameFilter( Self, handle, pMatchCloudFileName );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamUGC_SetMatchAnyTag")]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _SetMatchAnyTag( IntPtr self, UGCQueryHandle_t handle, [MarshalAs( UnmanagedType.U1 )] bool bMatchAnyTag );
		
		#endregion
		internal bool SetMatchAnyTag( UGCQueryHandle_t handle, [MarshalAs( UnmanagedType.U1 )] bool bMatchAnyTag )
		{
			var returnValue = _SetMatchAnyTag( Self, handle, bMatchAnyTag );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamUGC_SetSearchText")]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _SetSearchText( IntPtr self, UGCQueryHandle_t handle, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pSearchText );
		
		#endregion
		internal bool SetSearchText( UGCQueryHandle_t handle, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pSearchText )
		{
			var returnValue = _SetSearchText( Self, handle, pSearchText );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamUGC_SetRankedByTrendDays")]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _SetRankedByTrendDays( IntPtr self, UGCQueryHandle_t handle, uint unDays );
		
		#endregion
		internal bool SetRankedByTrendDays( UGCQueryHandle_t handle, uint unDays )
		{
			var returnValue = _SetRankedByTrendDays( Self, handle, unDays );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamUGC_AddRequiredKeyValueTag")]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _AddRequiredKeyValueTag( IntPtr self, UGCQueryHandle_t handle, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pKey, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pValue );
		
		#endregion
		internal bool AddRequiredKeyValueTag( UGCQueryHandle_t handle, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pKey, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pValue )
		{
			var returnValue = _AddRequiredKeyValueTag( Self, handle, pKey, pValue );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamUGC_RequestUGCDetails")]
		private static extern SteamAPICall_t _RequestUGCDetails( IntPtr self, PublishedFileId nPublishedFileID, uint unMaxAgeSeconds );
		
		#endregion
		internal CallbackResult RequestUGCDetails( PublishedFileId nPublishedFileID, uint unMaxAgeSeconds )
		{
			var returnValue = _RequestUGCDetails( Self, nPublishedFileID, unMaxAgeSeconds );
			return new CallbackResult( returnValue );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamUGC_CreateItem")]
		private static extern SteamAPICall_t _CreateItem( IntPtr self, AppId nConsumerAppId, WorkshopFileType eFileType );
		
		#endregion
		internal CallbackResult CreateItem( AppId nConsumerAppId, WorkshopFileType eFileType )
		{
			var returnValue = _CreateItem( Self, nConsumerAppId, eFileType );
			return new CallbackResult( returnValue );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamUGC_StartItemUpdate")]
		private static extern UGCUpdateHandle_t _StartItemUpdate( IntPtr self, AppId nConsumerAppId, PublishedFileId nPublishedFileID );
		
		#endregion
		internal UGCUpdateHandle_t StartItemUpdate( AppId nConsumerAppId, PublishedFileId nPublishedFileID )
		{
			var returnValue = _StartItemUpdate( Self, nConsumerAppId, nPublishedFileID );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamUGC_SetItemTitle")]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _SetItemTitle( IntPtr self, UGCUpdateHandle_t handle, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchTitle );
		
		#endregion
		internal bool SetItemTitle( UGCUpdateHandle_t handle, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchTitle )
		{
			var returnValue = _SetItemTitle( Self, handle, pchTitle );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamUGC_SetItemDescription")]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _SetItemDescription( IntPtr self, UGCUpdateHandle_t handle, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchDescription );
		
		#endregion
		internal bool SetItemDescription( UGCUpdateHandle_t handle, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchDescription )
		{
			var returnValue = _SetItemDescription( Self, handle, pchDescription );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamUGC_SetItemUpdateLanguage")]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _SetItemUpdateLanguage( IntPtr self, UGCUpdateHandle_t handle, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchLanguage );
		
		#endregion
		internal bool SetItemUpdateLanguage( UGCUpdateHandle_t handle, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchLanguage )
		{
			var returnValue = _SetItemUpdateLanguage( Self, handle, pchLanguage );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamUGC_SetItemMetadata")]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _SetItemMetadata( IntPtr self, UGCUpdateHandle_t handle, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchMetaData );
		
		#endregion
		internal bool SetItemMetadata( UGCUpdateHandle_t handle, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchMetaData )
		{
			var returnValue = _SetItemMetadata( Self, handle, pchMetaData );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamUGC_SetItemVisibility")]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _SetItemVisibility( IntPtr self, UGCUpdateHandle_t handle, RemoteStoragePublishedFileVisibility eVisibility );
		
		#endregion
		internal bool SetItemVisibility( UGCUpdateHandle_t handle, RemoteStoragePublishedFileVisibility eVisibility )
		{
			var returnValue = _SetItemVisibility( Self, handle, eVisibility );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamUGC_SetItemTags")]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _SetItemTags( IntPtr self, UGCUpdateHandle_t updateHandle, ref SteamParamStringArray_t pTags );
		
		#endregion
		internal bool SetItemTags( UGCUpdateHandle_t updateHandle, ref SteamParamStringArray_t pTags )
		{
			var returnValue = _SetItemTags( Self, updateHandle, ref pTags );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamUGC_SetItemContent")]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _SetItemContent( IntPtr self, UGCUpdateHandle_t handle, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pszContentFolder );
		
		#endregion
		internal bool SetItemContent( UGCUpdateHandle_t handle, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pszContentFolder )
		{
			var returnValue = _SetItemContent( Self, handle, pszContentFolder );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamUGC_SetItemPreview")]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _SetItemPreview( IntPtr self, UGCUpdateHandle_t handle, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pszPreviewFile );
		
		#endregion
		internal bool SetItemPreview( UGCUpdateHandle_t handle, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pszPreviewFile )
		{
			var returnValue = _SetItemPreview( Self, handle, pszPreviewFile );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamUGC_SetAllowLegacyUpload")]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _SetAllowLegacyUpload( IntPtr self, UGCUpdateHandle_t handle, [MarshalAs( UnmanagedType.U1 )] bool bAllowLegacyUpload );
		
		#endregion
		internal bool SetAllowLegacyUpload( UGCUpdateHandle_t handle, [MarshalAs( UnmanagedType.U1 )] bool bAllowLegacyUpload )
		{
			var returnValue = _SetAllowLegacyUpload( Self, handle, bAllowLegacyUpload );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamUGC_RemoveItemKeyValueTags")]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _RemoveItemKeyValueTags( IntPtr self, UGCUpdateHandle_t handle, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchKey );
		
		#endregion
		internal bool RemoveItemKeyValueTags( UGCUpdateHandle_t handle, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchKey )
		{
			var returnValue = _RemoveItemKeyValueTags( Self, handle, pchKey );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamUGC_AddItemKeyValueTag")]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _AddItemKeyValueTag( IntPtr self, UGCUpdateHandle_t handle, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchKey, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchValue );
		
		#endregion
		internal bool AddItemKeyValueTag( UGCUpdateHandle_t handle, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchKey, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchValue )
		{
			var returnValue = _AddItemKeyValueTag( Self, handle, pchKey, pchValue );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamUGC_AddItemPreviewFile")]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _AddItemPreviewFile( IntPtr self, UGCUpdateHandle_t handle, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pszPreviewFile, ItemPreviewType type );
		
		#endregion
		internal bool AddItemPreviewFile( UGCUpdateHandle_t handle, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pszPreviewFile, ItemPreviewType type )
		{
			var returnValue = _AddItemPreviewFile( Self, handle, pszPreviewFile, type );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamUGC_AddItemPreviewVideo")]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _AddItemPreviewVideo( IntPtr self, UGCUpdateHandle_t handle, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pszVideoID );
		
		#endregion
		internal bool AddItemPreviewVideo( UGCUpdateHandle_t handle, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pszVideoID )
		{
			var returnValue = _AddItemPreviewVideo( Self, handle, pszVideoID );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamUGC_UpdateItemPreviewFile")]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _UpdateItemPreviewFile( IntPtr self, UGCUpdateHandle_t handle, uint index, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pszPreviewFile );
		
		#endregion
		internal bool UpdateItemPreviewFile( UGCUpdateHandle_t handle, uint index, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pszPreviewFile )
		{
			var returnValue = _UpdateItemPreviewFile( Self, handle, index, pszPreviewFile );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamUGC_UpdateItemPreviewVideo")]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _UpdateItemPreviewVideo( IntPtr self, UGCUpdateHandle_t handle, uint index, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pszVideoID );
		
		#endregion
		internal bool UpdateItemPreviewVideo( UGCUpdateHandle_t handle, uint index, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pszVideoID )
		{
			var returnValue = _UpdateItemPreviewVideo( Self, handle, index, pszVideoID );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamUGC_RemoveItemPreview")]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _RemoveItemPreview( IntPtr self, UGCUpdateHandle_t handle, uint index );
		
		#endregion
		internal bool RemoveItemPreview( UGCUpdateHandle_t handle, uint index )
		{
			var returnValue = _RemoveItemPreview( Self, handle, index );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamUGC_SubmitItemUpdate")]
		private static extern SteamAPICall_t _SubmitItemUpdate( IntPtr self, UGCUpdateHandle_t handle, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchChangeNote );
		
		#endregion
		internal CallbackResult SubmitItemUpdate( UGCUpdateHandle_t handle, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchChangeNote )
		{
			var returnValue = _SubmitItemUpdate( Self, handle, pchChangeNote );
			return new CallbackResult( returnValue );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamUGC_GetItemUpdateProgress")]
		private static extern ItemUpdateStatus _GetItemUpdateProgress( IntPtr self, UGCUpdateHandle_t handle, ref ulong punBytesProcessed, ref ulong punBytesTotal );
		
		#endregion
		internal ItemUpdateStatus GetItemUpdateProgress( UGCUpdateHandle_t handle, ref ulong punBytesProcessed, ref ulong punBytesTotal )
		{
			var returnValue = _GetItemUpdateProgress( Self, handle, ref punBytesProcessed, ref punBytesTotal );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamUGC_SetUserItemVote")]
		private static extern SteamAPICall_t _SetUserItemVote( IntPtr self, PublishedFileId nPublishedFileID, [MarshalAs( UnmanagedType.U1 )] bool bVoteUp );
		
		#endregion
		internal CallbackResult SetUserItemVote( PublishedFileId nPublishedFileID, [MarshalAs( UnmanagedType.U1 )] bool bVoteUp )
		{
			var returnValue = _SetUserItemVote( Self, nPublishedFileID, bVoteUp );
			return new CallbackResult( returnValue );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamUGC_GetUserItemVote")]
		private static extern SteamAPICall_t _GetUserItemVote( IntPtr self, PublishedFileId nPublishedFileID );
		
		#endregion
		internal CallbackResult GetUserItemVote( PublishedFileId nPublishedFileID )
		{
			var returnValue = _GetUserItemVote( Self, nPublishedFileID );
			return new CallbackResult( returnValue );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamUGC_AddItemToFavorites")]
		private static extern SteamAPICall_t _AddItemToFavorites( IntPtr self, AppId nAppId, PublishedFileId nPublishedFileID );
		
		#endregion
		internal CallbackResult AddItemToFavorites( AppId nAppId, PublishedFileId nPublishedFileID )
		{
			var returnValue = _AddItemToFavorites( Self, nAppId, nPublishedFileID );
			return new CallbackResult( returnValue );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamUGC_RemoveItemFromFavorites")]
		private static extern SteamAPICall_t _RemoveItemFromFavorites( IntPtr self, AppId nAppId, PublishedFileId nPublishedFileID );
		
		#endregion
		internal CallbackResult RemoveItemFromFavorites( AppId nAppId, PublishedFileId nPublishedFileID )
		{
			var returnValue = _RemoveItemFromFavorites( Self, nAppId, nPublishedFileID );
			return new CallbackResult( returnValue );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamUGC_SubscribeItem")]
		private static extern SteamAPICall_t _SubscribeItem( IntPtr self, PublishedFileId nPublishedFileID );
		
		#endregion
		internal CallbackResult SubscribeItem( PublishedFileId nPublishedFileID )
		{
			var returnValue = _SubscribeItem( Self, nPublishedFileID );
			return new CallbackResult( returnValue );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamUGC_UnsubscribeItem")]
		private static extern SteamAPICall_t _UnsubscribeItem( IntPtr self, PublishedFileId nPublishedFileID );
		
		#endregion
		internal CallbackResult UnsubscribeItem( PublishedFileId nPublishedFileID )
		{
			var returnValue = _UnsubscribeItem( Self, nPublishedFileID );
			return new CallbackResult( returnValue );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamUGC_GetNumSubscribedItems")]
		private static extern uint _GetNumSubscribedItems( IntPtr self );
		
		#endregion
		internal uint GetNumSubscribedItems()
		{
			var returnValue = _GetNumSubscribedItems( Self );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamUGC_GetSubscribedItems")]
		private static extern uint _GetSubscribedItems( IntPtr self, [In,Out] PublishedFileId[]  pvecPublishedFileID, uint cMaxEntries );
		
		#endregion
		internal uint GetSubscribedItems( [In,Out] PublishedFileId[]  pvecPublishedFileID, uint cMaxEntries )
		{
			var returnValue = _GetSubscribedItems( Self, pvecPublishedFileID, cMaxEntries );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamUGC_GetItemState")]
		private static extern uint _GetItemState( IntPtr self, PublishedFileId nPublishedFileID );
		
		#endregion
		internal uint GetItemState( PublishedFileId nPublishedFileID )
		{
			var returnValue = _GetItemState( Self, nPublishedFileID );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamUGC_GetItemInstallInfo")]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _GetItemInstallInfo( IntPtr self, PublishedFileId nPublishedFileID, ref ulong punSizeOnDisk, IntPtr pchFolder, uint cchFolderSize, ref uint punTimeStamp );
		
		#endregion
		internal bool GetItemInstallInfo( PublishedFileId nPublishedFileID, ref ulong punSizeOnDisk, out string pchFolder, ref uint punTimeStamp )
		{
			IntPtr mempchFolder = Helpers.TakeMemory();
			var returnValue = _GetItemInstallInfo( Self, nPublishedFileID, ref punSizeOnDisk, mempchFolder, (1024 * 32), ref punTimeStamp );
			pchFolder = Helpers.MemoryToString( mempchFolder );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamUGC_GetItemDownloadInfo")]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _GetItemDownloadInfo( IntPtr self, PublishedFileId nPublishedFileID, ref ulong punBytesDownloaded, ref ulong punBytesTotal );
		
		#endregion
		internal bool GetItemDownloadInfo( PublishedFileId nPublishedFileID, ref ulong punBytesDownloaded, ref ulong punBytesTotal )
		{
			var returnValue = _GetItemDownloadInfo( Self, nPublishedFileID, ref punBytesDownloaded, ref punBytesTotal );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamUGC_DownloadItem")]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _DownloadItem( IntPtr self, PublishedFileId nPublishedFileID, [MarshalAs( UnmanagedType.U1 )] bool bHighPriority );
		
		#endregion
		internal bool DownloadItem( PublishedFileId nPublishedFileID, [MarshalAs( UnmanagedType.U1 )] bool bHighPriority )
		{
			var returnValue = _DownloadItem( Self, nPublishedFileID, bHighPriority );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamUGC_BInitWorkshopForGameServer")]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _BInitWorkshopForGameServer( IntPtr self, DepotId_t unWorkshopDepotID, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pszFolder );
		
		#endregion
		internal bool BInitWorkshopForGameServer( DepotId_t unWorkshopDepotID, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pszFolder )
		{
			var returnValue = _BInitWorkshopForGameServer( Self, unWorkshopDepotID, pszFolder );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamUGC_SuspendDownloads")]
		private static extern void _SuspendDownloads( IntPtr self, [MarshalAs( UnmanagedType.U1 )] bool bSuspend );
		
		#endregion
		internal void SuspendDownloads( [MarshalAs( UnmanagedType.U1 )] bool bSuspend )
		{
			_SuspendDownloads( Self, bSuspend );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamUGC_StartPlaytimeTracking")]
		private static extern SteamAPICall_t _StartPlaytimeTracking( IntPtr self, [In,Out] PublishedFileId[]  pvecPublishedFileID, uint unNumPublishedFileIDs );
		
		#endregion
		internal CallbackResult StartPlaytimeTracking( [In,Out] PublishedFileId[]  pvecPublishedFileID, uint unNumPublishedFileIDs )
		{
			var returnValue = _StartPlaytimeTracking( Self, pvecPublishedFileID, unNumPublishedFileIDs );
			return new CallbackResult( returnValue );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamUGC_StopPlaytimeTracking")]
		private static extern SteamAPICall_t _StopPlaytimeTracking( IntPtr self, [In,Out] PublishedFileId[]  pvecPublishedFileID, uint unNumPublishedFileIDs );
		
		#endregion
		internal CallbackResult StopPlaytimeTracking( [In,Out] PublishedFileId[]  pvecPublishedFileID, uint unNumPublishedFileIDs )
		{
			var returnValue = _StopPlaytimeTracking( Self, pvecPublishedFileID, unNumPublishedFileIDs );
			return new CallbackResult( returnValue );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamUGC_StopPlaytimeTrackingForAllItems")]
		private static extern SteamAPICall_t _StopPlaytimeTrackingForAllItems( IntPtr self );
		
		#endregion
		internal CallbackResult StopPlaytimeTrackingForAllItems()
		{
			var returnValue = _StopPlaytimeTrackingForAllItems( Self );
			return new CallbackResult( returnValue );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamUGC_AddDependency")]
		private static extern SteamAPICall_t _AddDependency( IntPtr self, PublishedFileId nParentPublishedFileID, PublishedFileId nChildPublishedFileID );
		
		#endregion
		internal CallbackResult AddDependency( PublishedFileId nParentPublishedFileID, PublishedFileId nChildPublishedFileID )
		{
			var returnValue = _AddDependency( Self, nParentPublishedFileID, nChildPublishedFileID );
			return new CallbackResult( returnValue );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamUGC_RemoveDependency")]
		private static extern SteamAPICall_t _RemoveDependency( IntPtr self, PublishedFileId nParentPublishedFileID, PublishedFileId nChildPublishedFileID );
		
		#endregion
		internal CallbackResult RemoveDependency( PublishedFileId nParentPublishedFileID, PublishedFileId nChildPublishedFileID )
		{
			var returnValue = _RemoveDependency( Self, nParentPublishedFileID, nChildPublishedFileID );
			return new CallbackResult( returnValue );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamUGC_AddAppDependency")]
		private static extern SteamAPICall_t _AddAppDependency( IntPtr self, PublishedFileId nPublishedFileID, AppId nAppID );
		
		#endregion
		internal CallbackResult AddAppDependency( PublishedFileId nPublishedFileID, AppId nAppID )
		{
			var returnValue = _AddAppDependency( Self, nPublishedFileID, nAppID );
			return new CallbackResult( returnValue );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamUGC_RemoveAppDependency")]
		private static extern SteamAPICall_t _RemoveAppDependency( IntPtr self, PublishedFileId nPublishedFileID, AppId nAppID );
		
		#endregion
		internal CallbackResult RemoveAppDependency( PublishedFileId nPublishedFileID, AppId nAppID )
		{
			var returnValue = _RemoveAppDependency( Self, nPublishedFileID, nAppID );
			return new CallbackResult( returnValue );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamUGC_GetAppDependencies")]
		private static extern SteamAPICall_t _GetAppDependencies( IntPtr self, PublishedFileId nPublishedFileID );
		
		#endregion
		internal CallbackResult GetAppDependencies( PublishedFileId nPublishedFileID )
		{
			var returnValue = _GetAppDependencies( Self, nPublishedFileID );
			return new CallbackResult( returnValue );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamUGC_DeleteItem")]
		private static extern SteamAPICall_t _DeleteItem( IntPtr self, PublishedFileId nPublishedFileID );
		
		#endregion
		internal CallbackResult DeleteItem( PublishedFileId nPublishedFileID )
		{
			var returnValue = _DeleteItem( Self, nPublishedFileID );
			return new CallbackResult( returnValue );
		}
		
	}
}
