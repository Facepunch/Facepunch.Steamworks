using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Steamworks.Data;


namespace Steamworks
{
	internal class ISteamApps : SteamInterface
	{
		public override string InterfaceName => "STEAMAPPS_INTERFACE_VERSION008";
		
		public override void InitInternals()
		{
			_BIsSubscribed = Marshal.GetDelegateForFunctionPointer<FBIsSubscribed>( Marshal.ReadIntPtr( VTable, 0) );
			_BIsLowViolence = Marshal.GetDelegateForFunctionPointer<FBIsLowViolence>( Marshal.ReadIntPtr( VTable, 8) );
			_BIsCybercafe = Marshal.GetDelegateForFunctionPointer<FBIsCybercafe>( Marshal.ReadIntPtr( VTable, 16) );
			_BIsVACBanned = Marshal.GetDelegateForFunctionPointer<FBIsVACBanned>( Marshal.ReadIntPtr( VTable, 24) );
			_GetCurrentGameLanguage = Marshal.GetDelegateForFunctionPointer<FGetCurrentGameLanguage>( Marshal.ReadIntPtr( VTable, 32) );
			_GetAvailableGameLanguages = Marshal.GetDelegateForFunctionPointer<FGetAvailableGameLanguages>( Marshal.ReadIntPtr( VTable, 40) );
			_BIsSubscribedApp = Marshal.GetDelegateForFunctionPointer<FBIsSubscribedApp>( Marshal.ReadIntPtr( VTable, 48) );
			_BIsDlcInstalled = Marshal.GetDelegateForFunctionPointer<FBIsDlcInstalled>( Marshal.ReadIntPtr( VTable, 56) );
			_GetEarliestPurchaseUnixTime = Marshal.GetDelegateForFunctionPointer<FGetEarliestPurchaseUnixTime>( Marshal.ReadIntPtr( VTable, 64) );
			_BIsSubscribedFromFreeWeekend = Marshal.GetDelegateForFunctionPointer<FBIsSubscribedFromFreeWeekend>( Marshal.ReadIntPtr( VTable, 72) );
			_GetDLCCount = Marshal.GetDelegateForFunctionPointer<FGetDLCCount>( Marshal.ReadIntPtr( VTable, 80) );
			_BGetDLCDataByIndex = Marshal.GetDelegateForFunctionPointer<FBGetDLCDataByIndex>( Marshal.ReadIntPtr( VTable, 88) );
			_InstallDLC = Marshal.GetDelegateForFunctionPointer<FInstallDLC>( Marshal.ReadIntPtr( VTable, 96) );
			_UninstallDLC = Marshal.GetDelegateForFunctionPointer<FUninstallDLC>( Marshal.ReadIntPtr( VTable, 104) );
			_RequestAppProofOfPurchaseKey = Marshal.GetDelegateForFunctionPointer<FRequestAppProofOfPurchaseKey>( Marshal.ReadIntPtr( VTable, 112) );
			_GetCurrentBetaName = Marshal.GetDelegateForFunctionPointer<FGetCurrentBetaName>( Marshal.ReadIntPtr( VTable, 120) );
			_MarkContentCorrupt = Marshal.GetDelegateForFunctionPointer<FMarkContentCorrupt>( Marshal.ReadIntPtr( VTable, 128) );
			_GetInstalledDepots = Marshal.GetDelegateForFunctionPointer<FGetInstalledDepots>( Marshal.ReadIntPtr( VTable, 136) );
			_GetAppInstallDir = Marshal.GetDelegateForFunctionPointer<FGetAppInstallDir>( Marshal.ReadIntPtr( VTable, 144) );
			_BIsAppInstalled = Marshal.GetDelegateForFunctionPointer<FBIsAppInstalled>( Marshal.ReadIntPtr( VTable, 152) );
			_GetAppOwner = Marshal.GetDelegateForFunctionPointer<FGetAppOwner>( Marshal.ReadIntPtr( VTable, 160) );
			_GetAppOwner_Windows = Marshal.GetDelegateForFunctionPointer<FGetAppOwner_Windows>( Marshal.ReadIntPtr( VTable, 160) );
			_GetLaunchQueryParam = Marshal.GetDelegateForFunctionPointer<FGetLaunchQueryParam>( Marshal.ReadIntPtr( VTable, 168) );
			_GetDlcDownloadProgress = Marshal.GetDelegateForFunctionPointer<FGetDlcDownloadProgress>( Marshal.ReadIntPtr( VTable, 176) );
			_GetAppBuildId = Marshal.GetDelegateForFunctionPointer<FGetAppBuildId>( Marshal.ReadIntPtr( VTable, 184) );
			_RequestAllProofOfPurchaseKeys = Marshal.GetDelegateForFunctionPointer<FRequestAllProofOfPurchaseKeys>( Marshal.ReadIntPtr( VTable, 192) );
			_GetFileDetails = Marshal.GetDelegateForFunctionPointer<FGetFileDetails>( Marshal.ReadIntPtr( VTable, 200) );
			_GetLaunchCommandLine = Marshal.GetDelegateForFunctionPointer<FGetLaunchCommandLine>( Marshal.ReadIntPtr( VTable, 208) );
			_BIsSubscribedFromFamilySharing = Marshal.GetDelegateForFunctionPointer<FBIsSubscribedFromFamilySharing>( Marshal.ReadIntPtr( VTable, 216) );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FBIsSubscribed( IntPtr self );
		private FBIsSubscribed _BIsSubscribed;
		
		#endregion
		internal bool BIsSubscribed()
		{
			return _BIsSubscribed( Self );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FBIsLowViolence( IntPtr self );
		private FBIsLowViolence _BIsLowViolence;
		
		#endregion
		internal bool BIsLowViolence()
		{
			return _BIsLowViolence( Self );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FBIsCybercafe( IntPtr self );
		private FBIsCybercafe _BIsCybercafe;
		
		#endregion
		internal bool BIsCybercafe()
		{
			return _BIsCybercafe( Self );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FBIsVACBanned( IntPtr self );
		private FBIsVACBanned _BIsVACBanned;
		
		#endregion
		internal bool BIsVACBanned()
		{
			return _BIsVACBanned( Self );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate IntPtr FGetCurrentGameLanguage( IntPtr self );
		private FGetCurrentGameLanguage _GetCurrentGameLanguage;
		
		#endregion
		internal string GetCurrentGameLanguage()
		{
			return GetString( _GetCurrentGameLanguage( Self ) );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate IntPtr FGetAvailableGameLanguages( IntPtr self );
		private FGetAvailableGameLanguages _GetAvailableGameLanguages;
		
		#endregion
		internal string GetAvailableGameLanguages()
		{
			return GetString( _GetAvailableGameLanguages( Self ) );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FBIsSubscribedApp( IntPtr self, AppId appID );
		private FBIsSubscribedApp _BIsSubscribedApp;
		
		#endregion
		internal bool BIsSubscribedApp( AppId appID )
		{
			return _BIsSubscribedApp( Self, appID );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FBIsDlcInstalled( IntPtr self, AppId appID );
		private FBIsDlcInstalled _BIsDlcInstalled;
		
		#endregion
		internal bool BIsDlcInstalled( AppId appID )
		{
			return _BIsDlcInstalled( Self, appID );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate uint FGetEarliestPurchaseUnixTime( IntPtr self, AppId nAppID );
		private FGetEarliestPurchaseUnixTime _GetEarliestPurchaseUnixTime;
		
		#endregion
		internal uint GetEarliestPurchaseUnixTime( AppId nAppID )
		{
			return _GetEarliestPurchaseUnixTime( Self, nAppID );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FBIsSubscribedFromFreeWeekend( IntPtr self );
		private FBIsSubscribedFromFreeWeekend _BIsSubscribedFromFreeWeekend;
		
		#endregion
		internal bool BIsSubscribedFromFreeWeekend()
		{
			return _BIsSubscribedFromFreeWeekend( Self );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate int FGetDLCCount( IntPtr self );
		private FGetDLCCount _GetDLCCount;
		
		#endregion
		internal int GetDLCCount()
		{
			return _GetDLCCount( Self );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FBGetDLCDataByIndex( IntPtr self, int iDLC, ref AppId pAppID, [MarshalAs( UnmanagedType.U1 )] ref bool pbAvailable, StringBuilder pchName, int cchNameBufferSize );
		private FBGetDLCDataByIndex _BGetDLCDataByIndex;
		
		#endregion
		internal bool BGetDLCDataByIndex( int iDLC, ref AppId pAppID, [MarshalAs( UnmanagedType.U1 )] ref bool pbAvailable, StringBuilder pchName, int cchNameBufferSize )
		{
			return _BGetDLCDataByIndex( Self, iDLC, ref pAppID, ref pbAvailable, pchName, cchNameBufferSize );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate void FInstallDLC( IntPtr self, AppId nAppID );
		private FInstallDLC _InstallDLC;
		
		#endregion
		internal void InstallDLC( AppId nAppID )
		{
			_InstallDLC( Self, nAppID );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate void FUninstallDLC( IntPtr self, AppId nAppID );
		private FUninstallDLC _UninstallDLC;
		
		#endregion
		internal void UninstallDLC( AppId nAppID )
		{
			_UninstallDLC( Self, nAppID );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate void FRequestAppProofOfPurchaseKey( IntPtr self, AppId nAppID );
		private FRequestAppProofOfPurchaseKey _RequestAppProofOfPurchaseKey;
		
		#endregion
		internal void RequestAppProofOfPurchaseKey( AppId nAppID )
		{
			_RequestAppProofOfPurchaseKey( Self, nAppID );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FGetCurrentBetaName( IntPtr self, StringBuilder pchName, int cchNameBufferSize );
		private FGetCurrentBetaName _GetCurrentBetaName;
		
		#endregion
		internal bool GetCurrentBetaName( StringBuilder pchName, int cchNameBufferSize )
		{
			return _GetCurrentBetaName( Self, pchName, cchNameBufferSize );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FMarkContentCorrupt( IntPtr self, [MarshalAs( UnmanagedType.U1 )] bool bMissingFilesOnly );
		private FMarkContentCorrupt _MarkContentCorrupt;
		
		#endregion
		internal bool MarkContentCorrupt( [MarshalAs( UnmanagedType.U1 )] bool bMissingFilesOnly )
		{
			return _MarkContentCorrupt( Self, bMissingFilesOnly );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate uint FGetInstalledDepots( IntPtr self, AppId appID, [In,Out] DepotId_t[]  pvecDepots, uint cMaxDepots );
		private FGetInstalledDepots _GetInstalledDepots;
		
		#endregion
		internal uint GetInstalledDepots( AppId appID, [In,Out] DepotId_t[]  pvecDepots, uint cMaxDepots )
		{
			return _GetInstalledDepots( Self, appID, pvecDepots, cMaxDepots );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate uint FGetAppInstallDir( IntPtr self, AppId appID, StringBuilder pchFolder, uint cchFolderBufferSize );
		private FGetAppInstallDir _GetAppInstallDir;
		
		#endregion
		internal uint GetAppInstallDir( AppId appID, StringBuilder pchFolder, uint cchFolderBufferSize )
		{
			return _GetAppInstallDir( Self, appID, pchFolder, cchFolderBufferSize );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FBIsAppInstalled( IntPtr self, AppId appID );
		private FBIsAppInstalled _BIsAppInstalled;
		
		#endregion
		internal bool BIsAppInstalled( AppId appID )
		{
			return _BIsAppInstalled( Self, appID );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate SteamId FGetAppOwner( IntPtr self );
		private FGetAppOwner _GetAppOwner;
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate void FGetAppOwner_Windows( IntPtr self, ref SteamId retVal );
		private FGetAppOwner_Windows _GetAppOwner_Windows;
		
		#endregion
		internal SteamId GetAppOwner()
		{
			if ( Config.Os == OsType.Windows )
			{
				var retVal = default( SteamId );
				_GetAppOwner_Windows( Self, ref retVal );
				return retVal;
			}
			
			return _GetAppOwner( Self );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate IntPtr FGetLaunchQueryParam( IntPtr self, string pchKey );
		private FGetLaunchQueryParam _GetLaunchQueryParam;
		
		#endregion
		internal string GetLaunchQueryParam( string pchKey )
		{
			return GetString( _GetLaunchQueryParam( Self, pchKey ) );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FGetDlcDownloadProgress( IntPtr self, AppId nAppID, ref ulong punBytesDownloaded, ref ulong punBytesTotal );
		private FGetDlcDownloadProgress _GetDlcDownloadProgress;
		
		#endregion
		internal bool GetDlcDownloadProgress( AppId nAppID, ref ulong punBytesDownloaded, ref ulong punBytesTotal )
		{
			return _GetDlcDownloadProgress( Self, nAppID, ref punBytesDownloaded, ref punBytesTotal );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate int FGetAppBuildId( IntPtr self );
		private FGetAppBuildId _GetAppBuildId;
		
		#endregion
		internal int GetAppBuildId()
		{
			return _GetAppBuildId( Self );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate void FRequestAllProofOfPurchaseKeys( IntPtr self );
		private FRequestAllProofOfPurchaseKeys _RequestAllProofOfPurchaseKeys;
		
		#endregion
		internal void RequestAllProofOfPurchaseKeys()
		{
			_RequestAllProofOfPurchaseKeys( Self );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate SteamAPICall_t FGetFileDetails( IntPtr self, string pszFileName );
		private FGetFileDetails _GetFileDetails;
		
		#endregion
		internal async Task<FileDetailsResult_t?> GetFileDetails( string pszFileName )
		{
			return await FileDetailsResult_t.GetResultAsync( _GetFileDetails( Self, pszFileName ) );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate int FGetLaunchCommandLine( IntPtr self, StringBuilder pszCommandLine, int cubCommandLine );
		private FGetLaunchCommandLine _GetLaunchCommandLine;
		
		#endregion
		internal int GetLaunchCommandLine( StringBuilder pszCommandLine, int cubCommandLine )
		{
			return _GetLaunchCommandLine( Self, pszCommandLine, cubCommandLine );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FBIsSubscribedFromFamilySharing( IntPtr self );
		private FBIsSubscribedFromFamilySharing _BIsSubscribedFromFamilySharing;
		
		#endregion
		internal bool BIsSubscribedFromFamilySharing()
		{
			return _BIsSubscribedFromFamilySharing( Self );
		}
		
	}
}
