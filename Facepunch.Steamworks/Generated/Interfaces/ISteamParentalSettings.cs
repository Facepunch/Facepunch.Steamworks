using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Steamworks.Data;


namespace Steamworks
{
	internal class ISteamParentalSettings : SteamInterface
	{
		public ISteamParentalSettings( bool server = false ) : base( server )
		{
		}
		
		public override string InterfaceName => "STEAMPARENTALSETTINGS_INTERFACE_VERSION001";
		
		public override void InitInternals()
		{
			_BIsParentalLockEnabled = Marshal.GetDelegateForFunctionPointer<FBIsParentalLockEnabled>( Marshal.ReadIntPtr( VTable, 0) );
			_BIsParentalLockLocked = Marshal.GetDelegateForFunctionPointer<FBIsParentalLockLocked>( Marshal.ReadIntPtr( VTable, 8) );
			_BIsAppBlocked = Marshal.GetDelegateForFunctionPointer<FBIsAppBlocked>( Marshal.ReadIntPtr( VTable, 16) );
			_BIsAppInBlockList = Marshal.GetDelegateForFunctionPointer<FBIsAppInBlockList>( Marshal.ReadIntPtr( VTable, 24) );
			_BIsFeatureBlocked = Marshal.GetDelegateForFunctionPointer<FBIsFeatureBlocked>( Marshal.ReadIntPtr( VTable, 32) );
			_BIsFeatureInBlockList = Marshal.GetDelegateForFunctionPointer<FBIsFeatureInBlockList>( Marshal.ReadIntPtr( VTable, 40) );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FBIsParentalLockEnabled( IntPtr self );
		private FBIsParentalLockEnabled _BIsParentalLockEnabled;
		
		#endregion
		internal bool BIsParentalLockEnabled()
		{
			return _BIsParentalLockEnabled( Self );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FBIsParentalLockLocked( IntPtr self );
		private FBIsParentalLockLocked _BIsParentalLockLocked;
		
		#endregion
		internal bool BIsParentalLockLocked()
		{
			return _BIsParentalLockLocked( Self );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FBIsAppBlocked( IntPtr self, AppId_t nAppID );
		private FBIsAppBlocked _BIsAppBlocked;
		
		#endregion
		internal bool BIsAppBlocked( AppId_t nAppID )
		{
			return _BIsAppBlocked( Self, nAppID );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FBIsAppInBlockList( IntPtr self, AppId_t nAppID );
		private FBIsAppInBlockList _BIsAppInBlockList;
		
		#endregion
		internal bool BIsAppInBlockList( AppId_t nAppID )
		{
			return _BIsAppInBlockList( Self, nAppID );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FBIsFeatureBlocked( IntPtr self, ParentalFeature eFeature );
		private FBIsFeatureBlocked _BIsFeatureBlocked;
		
		#endregion
		internal bool BIsFeatureBlocked( ParentalFeature eFeature )
		{
			return _BIsFeatureBlocked( Self, eFeature );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FBIsFeatureInBlockList( IntPtr self, ParentalFeature eFeature );
		private FBIsFeatureInBlockList _BIsFeatureInBlockList;
		
		#endregion
		internal bool BIsFeatureInBlockList( ParentalFeature eFeature )
		{
			return _BIsFeatureInBlockList( Self, eFeature );
		}
		
	}
}
