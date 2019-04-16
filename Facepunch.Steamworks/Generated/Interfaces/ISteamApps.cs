using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Steamworks.Data;


namespace Steamworks
{
	internal class ISteamApps : SteamInterface
	{
		public ISteamApps( bool server = false ) : base( server )
		{
		}
		
		public override string InterfaceName => "STEAMAPPS_INTERFACE_VERSION008";
		
		public override void InitInternals()
		{
			BIsSubscribedDelegatePointer = Marshal.GetDelegateForFunctionPointer<BIsSubscribedDelegate>( Marshal.ReadIntPtr( VTable, 0) );
			BIsLowViolenceDelegatePointer = Marshal.GetDelegateForFunctionPointer<BIsLowViolenceDelegate>( Marshal.ReadIntPtr( VTable, 8) );
			BIsCybercafeDelegatePointer = Marshal.GetDelegateForFunctionPointer<BIsCybercafeDelegate>( Marshal.ReadIntPtr( VTable, 16) );
			BIsVACBannedDelegatePointer = Marshal.GetDelegateForFunctionPointer<BIsVACBannedDelegate>( Marshal.ReadIntPtr( VTable, 24) );
			GetCurrentGameLanguageDelegatePointer = Marshal.GetDelegateForFunctionPointer<GetCurrentGameLanguageDelegate>( Marshal.ReadIntPtr( VTable, 32) );
			GetAvailableGameLanguagesDelegatePointer = Marshal.GetDelegateForFunctionPointer<GetAvailableGameLanguagesDelegate>( Marshal.ReadIntPtr( VTable, 40) );
			BIsSubscribedAppDelegatePointer = Marshal.GetDelegateForFunctionPointer<BIsSubscribedAppDelegate>( Marshal.ReadIntPtr( VTable, 48) );
			BIsDlcInstalledDelegatePointer = Marshal.GetDelegateForFunctionPointer<BIsDlcInstalledDelegate>( Marshal.ReadIntPtr( VTable, 56) );
			GetEarliestPurchaseUnixTimeDelegatePointer = Marshal.GetDelegateForFunctionPointer<GetEarliestPurchaseUnixTimeDelegate>( Marshal.ReadIntPtr( VTable, 64) );
			BIsSubscribedFromFreeWeekendDelegatePointer = Marshal.GetDelegateForFunctionPointer<BIsSubscribedFromFreeWeekendDelegate>( Marshal.ReadIntPtr( VTable, 72) );
			GetDLCCountDelegatePointer = Marshal.GetDelegateForFunctionPointer<GetDLCCountDelegate>( Marshal.ReadIntPtr( VTable, 80) );
			BGetDLCDataByIndexDelegatePointer = Marshal.GetDelegateForFunctionPointer<BGetDLCDataByIndexDelegate>( Marshal.ReadIntPtr( VTable, 88) );
			InstallDLCDelegatePointer = Marshal.GetDelegateForFunctionPointer<InstallDLCDelegate>( Marshal.ReadIntPtr( VTable, 96) );
			UninstallDLCDelegatePointer = Marshal.GetDelegateForFunctionPointer<UninstallDLCDelegate>( Marshal.ReadIntPtr( VTable, 104) );
			RequestAppProofOfPurchaseKeyDelegatePointer = Marshal.GetDelegateForFunctionPointer<RequestAppProofOfPurchaseKeyDelegate>( Marshal.ReadIntPtr( VTable, 112) );
			GetCurrentBetaNameDelegatePointer = Marshal.GetDelegateForFunctionPointer<GetCurrentBetaNameDelegate>( Marshal.ReadIntPtr( VTable, 120) );
			MarkContentCorruptDelegatePointer = Marshal.GetDelegateForFunctionPointer<MarkContentCorruptDelegate>( Marshal.ReadIntPtr( VTable, 128) );
			GetInstalledDepotsDelegatePointer = Marshal.GetDelegateForFunctionPointer<GetInstalledDepotsDelegate>( Marshal.ReadIntPtr( VTable, 136) );
			GetAppInstallDirDelegatePointer = Marshal.GetDelegateForFunctionPointer<GetAppInstallDirDelegate>( Marshal.ReadIntPtr( VTable, 144) );
			BIsAppInstalledDelegatePointer = Marshal.GetDelegateForFunctionPointer<BIsAppInstalledDelegate>( Marshal.ReadIntPtr( VTable, 152) );
			GetAppOwnerDelegatePointer = Marshal.GetDelegateForFunctionPointer<GetAppOwnerDelegate>( Marshal.ReadIntPtr( VTable, 160) );
			GetLaunchQueryParamDelegatePointer = Marshal.GetDelegateForFunctionPointer<GetLaunchQueryParamDelegate>( Marshal.ReadIntPtr( VTable, 168) );
			GetDlcDownloadProgressDelegatePointer = Marshal.GetDelegateForFunctionPointer<GetDlcDownloadProgressDelegate>( Marshal.ReadIntPtr( VTable, 176) );
			GetAppBuildIdDelegatePointer = Marshal.GetDelegateForFunctionPointer<GetAppBuildIdDelegate>( Marshal.ReadIntPtr( VTable, 184) );
			RequestAllProofOfPurchaseKeysDelegatePointer = Marshal.GetDelegateForFunctionPointer<RequestAllProofOfPurchaseKeysDelegate>( Marshal.ReadIntPtr( VTable, 192) );
			GetFileDetailsDelegatePointer = Marshal.GetDelegateForFunctionPointer<GetFileDetailsDelegate>( Marshal.ReadIntPtr( VTable, 200) );
			GetLaunchCommandLineDelegatePointer = Marshal.GetDelegateForFunctionPointer<GetLaunchCommandLineDelegate>( Marshal.ReadIntPtr( VTable, 208) );
			BIsSubscribedFromFamilySharingDelegatePointer = Marshal.GetDelegateForFunctionPointer<BIsSubscribedFromFamilySharingDelegate>( Marshal.ReadIntPtr( VTable, 216) );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool BIsSubscribedDelegate( IntPtr self );
		private BIsSubscribedDelegate BIsSubscribedDelegatePointer;
		
		#endregion
		internal bool BIsSubscribed()
		{
			return BIsSubscribedDelegatePointer( Self );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool BIsLowViolenceDelegate( IntPtr self );
		private BIsLowViolenceDelegate BIsLowViolenceDelegatePointer;
		
		#endregion
		internal bool BIsLowViolence()
		{
			return BIsLowViolenceDelegatePointer( Self );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool BIsCybercafeDelegate( IntPtr self );
		private BIsCybercafeDelegate BIsCybercafeDelegatePointer;
		
		#endregion
		internal bool BIsCybercafe()
		{
			return BIsCybercafeDelegatePointer( Self );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool BIsVACBannedDelegate( IntPtr self );
		private BIsVACBannedDelegate BIsVACBannedDelegatePointer;
		
		#endregion
		internal bool BIsVACBanned()
		{
			return BIsVACBannedDelegatePointer( Self );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate IntPtr GetCurrentGameLanguageDelegate( IntPtr self );
		private GetCurrentGameLanguageDelegate GetCurrentGameLanguageDelegatePointer;
		
		#endregion
		internal string GetCurrentGameLanguage()
		{
			return GetString( GetCurrentGameLanguageDelegatePointer( Self ) );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate IntPtr GetAvailableGameLanguagesDelegate( IntPtr self );
		private GetAvailableGameLanguagesDelegate GetAvailableGameLanguagesDelegatePointer;
		
		#endregion
		internal string GetAvailableGameLanguages()
		{
			return GetString( GetAvailableGameLanguagesDelegatePointer( Self ) );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool BIsSubscribedAppDelegate( IntPtr self, AppId_t appID );
		private BIsSubscribedAppDelegate BIsSubscribedAppDelegatePointer;
		
		#endregion
		internal bool BIsSubscribedApp( AppId_t appID )
		{
			return BIsSubscribedAppDelegatePointer( Self, appID );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool BIsDlcInstalledDelegate( IntPtr self, AppId_t appID );
		private BIsDlcInstalledDelegate BIsDlcInstalledDelegatePointer;
		
		#endregion
		internal bool BIsDlcInstalled( AppId_t appID )
		{
			return BIsDlcInstalledDelegatePointer( Self, appID );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate uint GetEarliestPurchaseUnixTimeDelegate( IntPtr self, AppId_t nAppID );
		private GetEarliestPurchaseUnixTimeDelegate GetEarliestPurchaseUnixTimeDelegatePointer;
		
		#endregion
		internal uint GetEarliestPurchaseUnixTime( AppId_t nAppID )
		{
			return GetEarliestPurchaseUnixTimeDelegatePointer( Self, nAppID );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool BIsSubscribedFromFreeWeekendDelegate( IntPtr self );
		private BIsSubscribedFromFreeWeekendDelegate BIsSubscribedFromFreeWeekendDelegatePointer;
		
		#endregion
		internal bool BIsSubscribedFromFreeWeekend()
		{
			return BIsSubscribedFromFreeWeekendDelegatePointer( Self );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate int GetDLCCountDelegate( IntPtr self );
		private GetDLCCountDelegate GetDLCCountDelegatePointer;
		
		#endregion
		internal int GetDLCCount()
		{
			return GetDLCCountDelegatePointer( Self );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool BGetDLCDataByIndexDelegate( IntPtr self, int iDLC, ref AppId_t pAppID, [MarshalAs( UnmanagedType.U1 )] ref bool pbAvailable, StringBuilder pchName, int cchNameBufferSize );
		private BGetDLCDataByIndexDelegate BGetDLCDataByIndexDelegatePointer;
		
		#endregion
		internal bool BGetDLCDataByIndex( int iDLC, ref AppId_t pAppID, [MarshalAs( UnmanagedType.U1 )] ref bool pbAvailable, StringBuilder pchName, int cchNameBufferSize )
		{
			return BGetDLCDataByIndexDelegatePointer( Self, iDLC, ref pAppID, ref pbAvailable, pchName, cchNameBufferSize );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate void InstallDLCDelegate( IntPtr self, AppId_t nAppID );
		private InstallDLCDelegate InstallDLCDelegatePointer;
		
		#endregion
		internal void InstallDLC( AppId_t nAppID )
		{
			InstallDLCDelegatePointer( Self, nAppID );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate void UninstallDLCDelegate( IntPtr self, AppId_t nAppID );
		private UninstallDLCDelegate UninstallDLCDelegatePointer;
		
		#endregion
		internal void UninstallDLC( AppId_t nAppID )
		{
			UninstallDLCDelegatePointer( Self, nAppID );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate void RequestAppProofOfPurchaseKeyDelegate( IntPtr self, AppId_t nAppID );
		private RequestAppProofOfPurchaseKeyDelegate RequestAppProofOfPurchaseKeyDelegatePointer;
		
		#endregion
		internal void RequestAppProofOfPurchaseKey( AppId_t nAppID )
		{
			RequestAppProofOfPurchaseKeyDelegatePointer( Self, nAppID );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool GetCurrentBetaNameDelegate( IntPtr self, StringBuilder pchName, int cchNameBufferSize );
		private GetCurrentBetaNameDelegate GetCurrentBetaNameDelegatePointer;
		
		#endregion
		internal bool GetCurrentBetaName( StringBuilder pchName, int cchNameBufferSize )
		{
			return GetCurrentBetaNameDelegatePointer( Self, pchName, cchNameBufferSize );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool MarkContentCorruptDelegate( IntPtr self, [MarshalAs( UnmanagedType.U1 )] bool bMissingFilesOnly );
		private MarkContentCorruptDelegate MarkContentCorruptDelegatePointer;
		
		#endregion
		internal bool MarkContentCorrupt( [MarshalAs( UnmanagedType.U1 )] bool bMissingFilesOnly )
		{
			return MarkContentCorruptDelegatePointer( Self, bMissingFilesOnly );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate uint GetInstalledDepotsDelegate( IntPtr self, AppId_t appID, [In,Out] DepotId_t[]  pvecDepots, uint cMaxDepots );
		private GetInstalledDepotsDelegate GetInstalledDepotsDelegatePointer;
		
		#endregion
		internal uint GetInstalledDepots( AppId_t appID, [In,Out] DepotId_t[]  pvecDepots, uint cMaxDepots )
		{
			return GetInstalledDepotsDelegatePointer( Self, appID, pvecDepots, cMaxDepots );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate uint GetAppInstallDirDelegate( IntPtr self, AppId_t appID, StringBuilder pchFolder, uint cchFolderBufferSize );
		private GetAppInstallDirDelegate GetAppInstallDirDelegatePointer;
		
		#endregion
		internal uint GetAppInstallDir( AppId_t appID, StringBuilder pchFolder, uint cchFolderBufferSize )
		{
			return GetAppInstallDirDelegatePointer( Self, appID, pchFolder, cchFolderBufferSize );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool BIsAppInstalledDelegate( IntPtr self, AppId_t appID );
		private BIsAppInstalledDelegate BIsAppInstalledDelegatePointer;
		
		#endregion
		internal bool BIsAppInstalled( AppId_t appID )
		{
			return BIsAppInstalledDelegatePointer( Self, appID );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate void GetAppOwnerDelegate( IntPtr self, ref SteamId retVal );
		private GetAppOwnerDelegate GetAppOwnerDelegatePointer;
		
		#endregion
		internal SteamId GetAppOwner()
		{
			var retVal = default( SteamId );
			GetAppOwnerDelegatePointer( Self, ref retVal );
			return retVal;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate IntPtr GetLaunchQueryParamDelegate( IntPtr self, string pchKey );
		private GetLaunchQueryParamDelegate GetLaunchQueryParamDelegatePointer;
		
		#endregion
		internal string GetLaunchQueryParam( string pchKey )
		{
			return GetString( GetLaunchQueryParamDelegatePointer( Self, pchKey ) );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool GetDlcDownloadProgressDelegate( IntPtr self, AppId_t nAppID, ref ulong punBytesDownloaded, ref ulong punBytesTotal );
		private GetDlcDownloadProgressDelegate GetDlcDownloadProgressDelegatePointer;
		
		#endregion
		internal bool GetDlcDownloadProgress( AppId_t nAppID, ref ulong punBytesDownloaded, ref ulong punBytesTotal )
		{
			return GetDlcDownloadProgressDelegatePointer( Self, nAppID, ref punBytesDownloaded, ref punBytesTotal );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate int GetAppBuildIdDelegate( IntPtr self );
		private GetAppBuildIdDelegate GetAppBuildIdDelegatePointer;
		
		#endregion
		internal int GetAppBuildId()
		{
			return GetAppBuildIdDelegatePointer( Self );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate void RequestAllProofOfPurchaseKeysDelegate( IntPtr self );
		private RequestAllProofOfPurchaseKeysDelegate RequestAllProofOfPurchaseKeysDelegatePointer;
		
		#endregion
		internal void RequestAllProofOfPurchaseKeys()
		{
			RequestAllProofOfPurchaseKeysDelegatePointer( Self );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate SteamAPICall_t GetFileDetailsDelegate( IntPtr self, string pszFileName );
		private GetFileDetailsDelegate GetFileDetailsDelegatePointer;
		
		#endregion
		internal async Task<FileDetailsResult_t?> GetFileDetails( string pszFileName )
		{
			return await (new Result<FileDetailsResult_t>( GetFileDetailsDelegatePointer( Self, pszFileName ) )).GetResult();
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate int GetLaunchCommandLineDelegate( IntPtr self, StringBuilder pszCommandLine, int cubCommandLine );
		private GetLaunchCommandLineDelegate GetLaunchCommandLineDelegatePointer;
		
		#endregion
		internal int GetLaunchCommandLine( StringBuilder pszCommandLine, int cubCommandLine )
		{
			return GetLaunchCommandLineDelegatePointer( Self, pszCommandLine, cubCommandLine );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool BIsSubscribedFromFamilySharingDelegate( IntPtr self );
		private BIsSubscribedFromFamilySharingDelegate BIsSubscribedFromFamilySharingDelegatePointer;
		
		#endregion
		internal bool BIsSubscribedFromFamilySharing()
		{
			return BIsSubscribedFromFamilySharingDelegatePointer( Self );
		}
		
	}
}
