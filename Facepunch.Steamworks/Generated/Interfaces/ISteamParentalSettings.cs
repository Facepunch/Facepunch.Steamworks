using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Steamworks.Data;


namespace Steamworks
{
	internal class ISteamParentalSettings : SteamInterface
	{
		
		internal ISteamParentalSettings( bool IsGameServer )
		{
			SetupInterface( IsGameServer );
		}
		
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_SteamParentalSettings_v001", CallingConvention = Platform.CC)]
		internal static extern IntPtr SteamAPI_SteamParentalSettings_v001();
		public override IntPtr GetUserInterfacePointer() => SteamAPI_SteamParentalSettings_v001();
		
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamParentalSettings_BIsParentalLockEnabled", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _BIsParentalLockEnabled( IntPtr self );
		
		#endregion
		internal bool BIsParentalLockEnabled()
		{
			var returnValue = _BIsParentalLockEnabled( Self );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamParentalSettings_BIsParentalLockLocked", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _BIsParentalLockLocked( IntPtr self );
		
		#endregion
		internal bool BIsParentalLockLocked()
		{
			var returnValue = _BIsParentalLockLocked( Self );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamParentalSettings_BIsAppBlocked", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _BIsAppBlocked( IntPtr self, AppId nAppID );
		
		#endregion
		internal bool BIsAppBlocked( AppId nAppID )
		{
			var returnValue = _BIsAppBlocked( Self, nAppID );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamParentalSettings_BIsAppInBlockList", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _BIsAppInBlockList( IntPtr self, AppId nAppID );
		
		#endregion
		internal bool BIsAppInBlockList( AppId nAppID )
		{
			var returnValue = _BIsAppInBlockList( Self, nAppID );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamParentalSettings_BIsFeatureBlocked", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _BIsFeatureBlocked( IntPtr self, ParentalFeature eFeature );
		
		#endregion
		internal bool BIsFeatureBlocked( ParentalFeature eFeature )
		{
			var returnValue = _BIsFeatureBlocked( Self, eFeature );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamParentalSettings_BIsFeatureInBlockList", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _BIsFeatureInBlockList( IntPtr self, ParentalFeature eFeature );
		
		#endregion
		internal bool BIsFeatureInBlockList( ParentalFeature eFeature )
		{
			var returnValue = _BIsFeatureInBlockList( Self, eFeature );
			return returnValue;
		}
		
	}
}
