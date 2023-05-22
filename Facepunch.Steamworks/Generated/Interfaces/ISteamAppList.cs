using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Steamworks.Data;


namespace Steamworks
{
	internal unsafe class ISteamAppList : SteamInterface
	{
		
		internal ISteamAppList( bool IsGameServer )
		{
			SetupInterface( IsGameServer );
		}
		
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_SteamAppList_v001", CallingConvention = Platform.CC)]
		internal static extern IntPtr SteamAPI_SteamAppList_v001();
		public override IntPtr GetUserInterfacePointer() => SteamAPI_SteamAppList_v001();
		
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamAppList_GetNumInstalledApps", CallingConvention = Platform.CC)]
		private static extern uint _GetNumInstalledApps( IntPtr self );
		
		#endregion
		internal uint GetNumInstalledApps()
		{
			var returnValue = _GetNumInstalledApps( Self );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamAppList_GetInstalledApps", CallingConvention = Platform.CC)]
		private static extern uint _GetInstalledApps( IntPtr self, [In,Out] AppId[]  pvecAppID, uint unMaxAppIDs );
		
		#endregion
		internal uint GetInstalledApps( [In,Out] AppId[]  pvecAppID, uint unMaxAppIDs )
		{
			var returnValue = _GetInstalledApps( Self, pvecAppID, unMaxAppIDs );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamAppList_GetAppName", CallingConvention = Platform.CC)]
		private static extern int _GetAppName( IntPtr self, AppId nAppID, IntPtr pchName, int cchNameMax );
		
		#endregion
		internal int GetAppName( AppId nAppID, out string pchName )
		{
			using var mempchName = Helpers.TakeMemory();
			var returnValue = _GetAppName( Self, nAppID, mempchName, (1024 * 32) );
			pchName = Helpers.MemoryToString( mempchName );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamAppList_GetAppInstallDir", CallingConvention = Platform.CC)]
		private static extern int _GetAppInstallDir( IntPtr self, AppId nAppID, IntPtr pchDirectory, int cchNameMax );
		
		#endregion
		internal int GetAppInstallDir( AppId nAppID, out string pchDirectory )
		{
			using var mempchDirectory = Helpers.TakeMemory();
			var returnValue = _GetAppInstallDir( Self, nAppID, mempchDirectory, (1024 * 32) );
			pchDirectory = Helpers.MemoryToString( mempchDirectory );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamAppList_GetAppBuildId", CallingConvention = Platform.CC)]
		private static extern int _GetAppBuildId( IntPtr self, AppId nAppID );
		
		#endregion
		internal int GetAppBuildId( AppId nAppID )
		{
			var returnValue = _GetAppBuildId( Self, nAppID );
			return returnValue;
		}
		
	}
}
