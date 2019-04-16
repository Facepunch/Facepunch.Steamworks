using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;


namespace Steamworks.Internal
{
	public class ISteamParentalSettings : BaseSteamInterface
	{
		public ISteamParentalSettings( bool server = false ) : base( server )
		{
		}
		
		public override string InterfaceName => "STEAMPARENTALSETTINGS_INTERFACE_VERSION001";
		
		public override void InitInternals()
		{
			BIsParentalLockEnabledDelegatePointer = Marshal.GetDelegateForFunctionPointer<BIsParentalLockEnabledDelegate>( Marshal.ReadIntPtr( VTable, 0) );
			BIsParentalLockLockedDelegatePointer = Marshal.GetDelegateForFunctionPointer<BIsParentalLockLockedDelegate>( Marshal.ReadIntPtr( VTable, 8) );
			BIsAppBlockedDelegatePointer = Marshal.GetDelegateForFunctionPointer<BIsAppBlockedDelegate>( Marshal.ReadIntPtr( VTable, 16) );
			BIsAppInBlockListDelegatePointer = Marshal.GetDelegateForFunctionPointer<BIsAppInBlockListDelegate>( Marshal.ReadIntPtr( VTable, 24) );
			BIsFeatureBlockedDelegatePointer = Marshal.GetDelegateForFunctionPointer<BIsFeatureBlockedDelegate>( Marshal.ReadIntPtr( VTable, 32) );
			BIsFeatureInBlockListDelegatePointer = Marshal.GetDelegateForFunctionPointer<BIsFeatureInBlockListDelegate>( Marshal.ReadIntPtr( VTable, 40) );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		public delegate bool BIsParentalLockEnabledDelegate( IntPtr self );
		private BIsParentalLockEnabledDelegate BIsParentalLockEnabledDelegatePointer;
		
		#endregion
		public bool BIsParentalLockEnabled()
		{
			return BIsParentalLockEnabledDelegatePointer( Self );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		public delegate bool BIsParentalLockLockedDelegate( IntPtr self );
		private BIsParentalLockLockedDelegate BIsParentalLockLockedDelegatePointer;
		
		#endregion
		public bool BIsParentalLockLocked()
		{
			return BIsParentalLockLockedDelegatePointer( Self );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		public delegate bool BIsAppBlockedDelegate( IntPtr self, AppId_t nAppID );
		private BIsAppBlockedDelegate BIsAppBlockedDelegatePointer;
		
		#endregion
		public bool BIsAppBlocked( AppId_t nAppID )
		{
			return BIsAppBlockedDelegatePointer( Self, nAppID );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		public delegate bool BIsAppInBlockListDelegate( IntPtr self, AppId_t nAppID );
		private BIsAppInBlockListDelegate BIsAppInBlockListDelegatePointer;
		
		#endregion
		public bool BIsAppInBlockList( AppId_t nAppID )
		{
			return BIsAppInBlockListDelegatePointer( Self, nAppID );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		public delegate bool BIsFeatureBlockedDelegate( IntPtr self, ParentalFeature eFeature );
		private BIsFeatureBlockedDelegate BIsFeatureBlockedDelegatePointer;
		
		#endregion
		public bool BIsFeatureBlocked( ParentalFeature eFeature )
		{
			return BIsFeatureBlockedDelegatePointer( Self, eFeature );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		public delegate bool BIsFeatureInBlockListDelegate( IntPtr self, ParentalFeature eFeature );
		private BIsFeatureInBlockListDelegate BIsFeatureInBlockListDelegatePointer;
		
		#endregion
		public bool BIsFeatureInBlockList( ParentalFeature eFeature )
		{
			return BIsFeatureInBlockListDelegatePointer( Self, eFeature );
		}
		
	}
}
