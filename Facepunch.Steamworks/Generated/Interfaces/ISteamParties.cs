using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Steamworks.Data;


namespace Steamworks
{
	internal class ISteamParties : SteamInterface
	{
		public override string InterfaceName => "SteamParties002";
		
		public override void InitInternals()
		{
			_GetNumActiveBeacons = Marshal.GetDelegateForFunctionPointer<FGetNumActiveBeacons>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 0 ) ) );
			_GetBeaconByIndex = Marshal.GetDelegateForFunctionPointer<FGetBeaconByIndex>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 8 ) ) );
			_GetBeaconDetails = Marshal.GetDelegateForFunctionPointer<FGetBeaconDetails>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 16 ) ) );
			_JoinParty = Marshal.GetDelegateForFunctionPointer<FJoinParty>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 24 ) ) );
			_GetNumAvailableBeaconLocations = Marshal.GetDelegateForFunctionPointer<FGetNumAvailableBeaconLocations>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 32 ) ) );
			_GetAvailableBeaconLocations = Marshal.GetDelegateForFunctionPointer<FGetAvailableBeaconLocations>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 40 ) ) );
			_CreateBeacon = Marshal.GetDelegateForFunctionPointer<FCreateBeacon>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 48 ) ) );
			_OnReservationCompleted = Marshal.GetDelegateForFunctionPointer<FOnReservationCompleted>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 56 ) ) );
			_CancelReservation = Marshal.GetDelegateForFunctionPointer<FCancelReservation>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 64 ) ) );
			_ChangeNumOpenSlots = Marshal.GetDelegateForFunctionPointer<FChangeNumOpenSlots>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 72 ) ) );
			_DestroyBeacon = Marshal.GetDelegateForFunctionPointer<FDestroyBeacon>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 80 ) ) );
			_GetBeaconLocationData = Marshal.GetDelegateForFunctionPointer<FGetBeaconLocationData>( Marshal.ReadIntPtr( VTable, Platform.MemoryOffset( 88 ) ) );
		}
		internal override void Shutdown()
		{
			base.Shutdown();
			
			_GetNumActiveBeacons = null;
			_GetBeaconByIndex = null;
			_GetBeaconDetails = null;
			_JoinParty = null;
			_GetNumAvailableBeaconLocations = null;
			_GetAvailableBeaconLocations = null;
			_CreateBeacon = null;
			_OnReservationCompleted = null;
			_CancelReservation = null;
			_ChangeNumOpenSlots = null;
			_DestroyBeacon = null;
			_GetBeaconLocationData = null;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate uint FGetNumActiveBeacons( IntPtr self );
		private FGetNumActiveBeacons _GetNumActiveBeacons;
		
		#endregion
		internal uint GetNumActiveBeacons()
		{
			var returnValue = _GetNumActiveBeacons( Self );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate PartyBeaconID_t FGetBeaconByIndex( IntPtr self, uint unIndex );
		private FGetBeaconByIndex _GetBeaconByIndex;
		
		#endregion
		internal PartyBeaconID_t GetBeaconByIndex( uint unIndex )
		{
			var returnValue = _GetBeaconByIndex( Self, unIndex );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FGetBeaconDetails( IntPtr self, PartyBeaconID_t ulBeaconID, ref SteamId pSteamIDBeaconOwner, ref SteamPartyBeaconLocation_t pLocation, IntPtr pchMetadata, int cchMetadata );
		private FGetBeaconDetails _GetBeaconDetails;
		
		#endregion
		internal bool GetBeaconDetails( PartyBeaconID_t ulBeaconID, ref SteamId pSteamIDBeaconOwner, ref SteamPartyBeaconLocation_t pLocation, out string pchMetadata )
		{
			IntPtr mempchMetadata = Helpers.TakeMemory();
			var returnValue = _GetBeaconDetails( Self, ulBeaconID, ref pSteamIDBeaconOwner, ref pLocation, mempchMetadata, (1024 * 32) );
			pchMetadata = Helpers.MemoryToString( mempchMetadata );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate SteamAPICall_t FJoinParty( IntPtr self, PartyBeaconID_t ulBeaconID );
		private FJoinParty _JoinParty;
		
		#endregion
		internal async Task<JoinPartyCallback_t?> JoinParty( PartyBeaconID_t ulBeaconID )
		{
			var returnValue = _JoinParty( Self, ulBeaconID );
			return await JoinPartyCallback_t.GetResultAsync( returnValue );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FGetNumAvailableBeaconLocations( IntPtr self, ref uint puNumLocations );
		private FGetNumAvailableBeaconLocations _GetNumAvailableBeaconLocations;
		
		#endregion
		internal bool GetNumAvailableBeaconLocations( ref uint puNumLocations )
		{
			var returnValue = _GetNumAvailableBeaconLocations( Self, ref puNumLocations );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FGetAvailableBeaconLocations( IntPtr self, ref SteamPartyBeaconLocation_t pLocationList, uint uMaxNumLocations );
		private FGetAvailableBeaconLocations _GetAvailableBeaconLocations;
		
		#endregion
		internal bool GetAvailableBeaconLocations( ref SteamPartyBeaconLocation_t pLocationList, uint uMaxNumLocations )
		{
			var returnValue = _GetAvailableBeaconLocations( Self, ref pLocationList, uMaxNumLocations );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate SteamAPICall_t FCreateBeacon( IntPtr self, uint unOpenSlots, ref SteamPartyBeaconLocation_t pBeaconLocation, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchConnectString, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchMetadata );
		private FCreateBeacon _CreateBeacon;
		
		#endregion
		internal async Task<CreateBeaconCallback_t?> CreateBeacon( uint unOpenSlots,  /* ref */ SteamPartyBeaconLocation_t pBeaconLocation, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchConnectString, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchMetadata )
		{
			var returnValue = _CreateBeacon( Self, unOpenSlots, ref pBeaconLocation, pchConnectString, pchMetadata );
			return await CreateBeaconCallback_t.GetResultAsync( returnValue );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate void FOnReservationCompleted( IntPtr self, PartyBeaconID_t ulBeacon, SteamId steamIDUser );
		private FOnReservationCompleted _OnReservationCompleted;
		
		#endregion
		internal void OnReservationCompleted( PartyBeaconID_t ulBeacon, SteamId steamIDUser )
		{
			_OnReservationCompleted( Self, ulBeacon, steamIDUser );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate void FCancelReservation( IntPtr self, PartyBeaconID_t ulBeacon, SteamId steamIDUser );
		private FCancelReservation _CancelReservation;
		
		#endregion
		internal void CancelReservation( PartyBeaconID_t ulBeacon, SteamId steamIDUser )
		{
			_CancelReservation( Self, ulBeacon, steamIDUser );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		private delegate SteamAPICall_t FChangeNumOpenSlots( IntPtr self, PartyBeaconID_t ulBeacon, uint unOpenSlots );
		private FChangeNumOpenSlots _ChangeNumOpenSlots;
		
		#endregion
		internal async Task<ChangeNumOpenSlotsCallback_t?> ChangeNumOpenSlots( PartyBeaconID_t ulBeacon, uint unOpenSlots )
		{
			var returnValue = _ChangeNumOpenSlots( Self, ulBeacon, unOpenSlots );
			return await ChangeNumOpenSlotsCallback_t.GetResultAsync( returnValue );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FDestroyBeacon( IntPtr self, PartyBeaconID_t ulBeacon );
		private FDestroyBeacon _DestroyBeacon;
		
		#endregion
		internal bool DestroyBeacon( PartyBeaconID_t ulBeacon )
		{
			var returnValue = _DestroyBeacon( Self, ulBeacon );
			return returnValue;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( Platform.MemberConvention )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FGetBeaconLocationData( IntPtr self, SteamPartyBeaconLocation_t BeaconLocation, SteamPartyBeaconLocationData eData, IntPtr pchDataStringOut, int cchDataStringOut );
		private FGetBeaconLocationData _GetBeaconLocationData;
		
		#endregion
		internal bool GetBeaconLocationData( SteamPartyBeaconLocation_t BeaconLocation, SteamPartyBeaconLocationData eData, out string pchDataStringOut )
		{
			IntPtr mempchDataStringOut = Helpers.TakeMemory();
			var returnValue = _GetBeaconLocationData( Self, BeaconLocation, eData, mempchDataStringOut, (1024 * 32) );
			pchDataStringOut = Helpers.MemoryToString( mempchDataStringOut );
			return returnValue;
		}
		
	}
}
