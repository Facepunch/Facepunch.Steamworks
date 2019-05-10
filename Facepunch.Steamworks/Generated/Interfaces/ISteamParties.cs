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
			_GetNumActiveBeacons = Marshal.GetDelegateForFunctionPointer<FGetNumActiveBeacons>( Marshal.ReadIntPtr( VTable, 0) );
			_GetBeaconByIndex = Marshal.GetDelegateForFunctionPointer<FGetBeaconByIndex>( Marshal.ReadIntPtr( VTable, 8) );
			_GetBeaconDetails = Marshal.GetDelegateForFunctionPointer<FGetBeaconDetails>( Marshal.ReadIntPtr( VTable, 16) );
			_GetBeaconDetails_Windows = Marshal.GetDelegateForFunctionPointer<FGetBeaconDetails_Windows>( Marshal.ReadIntPtr( VTable, 16) );
			_JoinParty = Marshal.GetDelegateForFunctionPointer<FJoinParty>( Marshal.ReadIntPtr( VTable, 24) );
			_GetNumAvailableBeaconLocations = Marshal.GetDelegateForFunctionPointer<FGetNumAvailableBeaconLocations>( Marshal.ReadIntPtr( VTable, 32) );
			_GetAvailableBeaconLocations = Marshal.GetDelegateForFunctionPointer<FGetAvailableBeaconLocations>( Marshal.ReadIntPtr( VTable, 40) );
			_GetAvailableBeaconLocations_Windows = Marshal.GetDelegateForFunctionPointer<FGetAvailableBeaconLocations_Windows>( Marshal.ReadIntPtr( VTable, 40) );
			_CreateBeacon = Marshal.GetDelegateForFunctionPointer<FCreateBeacon>( Marshal.ReadIntPtr( VTable, 48) );
			_CreateBeacon_Windows = Marshal.GetDelegateForFunctionPointer<FCreateBeacon_Windows>( Marshal.ReadIntPtr( VTable, 48) );
			_OnReservationCompleted = Marshal.GetDelegateForFunctionPointer<FOnReservationCompleted>( Marshal.ReadIntPtr( VTable, 56) );
			_CancelReservation = Marshal.GetDelegateForFunctionPointer<FCancelReservation>( Marshal.ReadIntPtr( VTable, 64) );
			_ChangeNumOpenSlots = Marshal.GetDelegateForFunctionPointer<FChangeNumOpenSlots>( Marshal.ReadIntPtr( VTable, 72) );
			_DestroyBeacon = Marshal.GetDelegateForFunctionPointer<FDestroyBeacon>( Marshal.ReadIntPtr( VTable, 80) );
			_GetBeaconLocationData = Marshal.GetDelegateForFunctionPointer<FGetBeaconLocationData>( Marshal.ReadIntPtr( VTable, 88) );
			_GetBeaconLocationData_Windows = Marshal.GetDelegateForFunctionPointer<FGetBeaconLocationData_Windows>( Marshal.ReadIntPtr( VTable, 88) );
		}
		internal override void Shutdown()
		{
			base.Shutdown();
			
			_GetNumActiveBeacons = null;
			_GetBeaconByIndex = null;
			_GetBeaconDetails = null;
			_GetBeaconDetails_Windows = null;
			_JoinParty = null;
			_GetNumAvailableBeaconLocations = null;
			_GetAvailableBeaconLocations = null;
			_GetAvailableBeaconLocations_Windows = null;
			_CreateBeacon = null;
			_CreateBeacon_Windows = null;
			_OnReservationCompleted = null;
			_CancelReservation = null;
			_ChangeNumOpenSlots = null;
			_DestroyBeacon = null;
			_GetBeaconLocationData = null;
			_GetBeaconLocationData_Windows = null;
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate uint FGetNumActiveBeacons( IntPtr self );
		private FGetNumActiveBeacons _GetNumActiveBeacons;
		
		#endregion
		internal uint GetNumActiveBeacons()
		{
			return _GetNumActiveBeacons( Self );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate PartyBeaconID_t FGetBeaconByIndex( IntPtr self, uint unIndex );
		private FGetBeaconByIndex _GetBeaconByIndex;
		
		#endregion
		internal PartyBeaconID_t GetBeaconByIndex( uint unIndex )
		{
			return _GetBeaconByIndex( Self, unIndex );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FGetBeaconDetails( IntPtr self, PartyBeaconID_t ulBeaconID, ref SteamId pSteamIDBeaconOwner, ref SteamPartyBeaconLocation_t pLocation, StringBuilder pchMetadata, int cchMetadata );
		private FGetBeaconDetails _GetBeaconDetails;
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FGetBeaconDetails_Windows( IntPtr self, PartyBeaconID_t ulBeaconID, ref SteamId pSteamIDBeaconOwner, ref SteamPartyBeaconLocation_t.Pack8 pLocation, StringBuilder pchMetadata, int cchMetadata );
		private FGetBeaconDetails_Windows _GetBeaconDetails_Windows;
		
		#endregion
		internal bool GetBeaconDetails( PartyBeaconID_t ulBeaconID, ref SteamId pSteamIDBeaconOwner, ref SteamPartyBeaconLocation_t pLocation, StringBuilder pchMetadata, int cchMetadata )
		{
			if ( Config.Os == OsType.Windows )
			{
				SteamPartyBeaconLocation_t.Pack8 pLocation_windows = pLocation;
				var retVal = _GetBeaconDetails_Windows( Self, ulBeaconID, ref pSteamIDBeaconOwner, ref pLocation_windows, pchMetadata, cchMetadata );
				pLocation = pLocation_windows;
				return retVal;
			}
			
			return _GetBeaconDetails( Self, ulBeaconID, ref pSteamIDBeaconOwner, ref pLocation, pchMetadata, cchMetadata );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate SteamAPICall_t FJoinParty( IntPtr self, PartyBeaconID_t ulBeaconID );
		private FJoinParty _JoinParty;
		
		#endregion
		internal async Task<JoinPartyCallback_t?> JoinParty( PartyBeaconID_t ulBeaconID )
		{
			return await JoinPartyCallback_t.GetResultAsync( _JoinParty( Self, ulBeaconID ) );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FGetNumAvailableBeaconLocations( IntPtr self, ref uint puNumLocations );
		private FGetNumAvailableBeaconLocations _GetNumAvailableBeaconLocations;
		
		#endregion
		internal bool GetNumAvailableBeaconLocations( ref uint puNumLocations )
		{
			return _GetNumAvailableBeaconLocations( Self, ref puNumLocations );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FGetAvailableBeaconLocations( IntPtr self, ref SteamPartyBeaconLocation_t pLocationList, uint uMaxNumLocations );
		private FGetAvailableBeaconLocations _GetAvailableBeaconLocations;
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FGetAvailableBeaconLocations_Windows( IntPtr self, ref SteamPartyBeaconLocation_t.Pack8 pLocationList, uint uMaxNumLocations );
		private FGetAvailableBeaconLocations_Windows _GetAvailableBeaconLocations_Windows;
		
		#endregion
		internal bool GetAvailableBeaconLocations( ref SteamPartyBeaconLocation_t pLocationList, uint uMaxNumLocations )
		{
			if ( Config.Os == OsType.Windows )
			{
				SteamPartyBeaconLocation_t.Pack8 pLocationList_windows = pLocationList;
				var retVal = _GetAvailableBeaconLocations_Windows( Self, ref pLocationList_windows, uMaxNumLocations );
				pLocationList = pLocationList_windows;
				return retVal;
			}
			
			return _GetAvailableBeaconLocations( Self, ref pLocationList, uMaxNumLocations );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate SteamAPICall_t FCreateBeacon( IntPtr self, uint unOpenSlots, ref SteamPartyBeaconLocation_t pBeaconLocation, string pchConnectString, string pchMetadata );
		private FCreateBeacon _CreateBeacon;
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate SteamAPICall_t FCreateBeacon_Windows( IntPtr self, uint unOpenSlots, ref SteamPartyBeaconLocation_t.Pack8 pBeaconLocation, string pchConnectString, string pchMetadata );
		private FCreateBeacon_Windows _CreateBeacon_Windows;
		
		#endregion
		internal async Task<CreateBeaconCallback_t?> CreateBeacon( uint unOpenSlots,  /* ref */ SteamPartyBeaconLocation_t pBeaconLocation, string pchConnectString, string pchMetadata )
		{
			if ( Config.Os == OsType.Windows )
			{
				SteamPartyBeaconLocation_t.Pack8 pBeaconLocation_windows = pBeaconLocation;
				var retVal = _CreateBeacon_Windows( Self, unOpenSlots, ref pBeaconLocation_windows, pchConnectString, pchMetadata );
				pBeaconLocation = pBeaconLocation_windows;
				return await CreateBeaconCallback_t.GetResultAsync( retVal );
			}
			
			return await CreateBeaconCallback_t.GetResultAsync( _CreateBeacon( Self, unOpenSlots, ref pBeaconLocation, pchConnectString, pchMetadata ) );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate void FOnReservationCompleted( IntPtr self, PartyBeaconID_t ulBeacon, SteamId steamIDUser );
		private FOnReservationCompleted _OnReservationCompleted;
		
		#endregion
		internal void OnReservationCompleted( PartyBeaconID_t ulBeacon, SteamId steamIDUser )
		{
			_OnReservationCompleted( Self, ulBeacon, steamIDUser );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate void FCancelReservation( IntPtr self, PartyBeaconID_t ulBeacon, SteamId steamIDUser );
		private FCancelReservation _CancelReservation;
		
		#endregion
		internal void CancelReservation( PartyBeaconID_t ulBeacon, SteamId steamIDUser )
		{
			_CancelReservation( Self, ulBeacon, steamIDUser );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		private delegate SteamAPICall_t FChangeNumOpenSlots( IntPtr self, PartyBeaconID_t ulBeacon, uint unOpenSlots );
		private FChangeNumOpenSlots _ChangeNumOpenSlots;
		
		#endregion
		internal async Task<ChangeNumOpenSlotsCallback_t?> ChangeNumOpenSlots( PartyBeaconID_t ulBeacon, uint unOpenSlots )
		{
			return await ChangeNumOpenSlotsCallback_t.GetResultAsync( _ChangeNumOpenSlots( Self, ulBeacon, unOpenSlots ) );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FDestroyBeacon( IntPtr self, PartyBeaconID_t ulBeacon );
		private FDestroyBeacon _DestroyBeacon;
		
		#endregion
		internal bool DestroyBeacon( PartyBeaconID_t ulBeacon )
		{
			return _DestroyBeacon( Self, ulBeacon );
		}
		
		#region FunctionMeta
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FGetBeaconLocationData( IntPtr self, SteamPartyBeaconLocation_t BeaconLocation, SteamPartyBeaconLocationData eData, StringBuilder pchDataStringOut, int cchDataStringOut );
		private FGetBeaconLocationData _GetBeaconLocationData;
		[UnmanagedFunctionPointer( CallingConvention.ThisCall )]
		[return: MarshalAs( UnmanagedType.I1 )]
		private delegate bool FGetBeaconLocationData_Windows( IntPtr self, SteamPartyBeaconLocation_t.Pack8 BeaconLocation, SteamPartyBeaconLocationData eData, StringBuilder pchDataStringOut, int cchDataStringOut );
		private FGetBeaconLocationData_Windows _GetBeaconLocationData_Windows;
		
		#endregion
		internal bool GetBeaconLocationData( SteamPartyBeaconLocation_t BeaconLocation, SteamPartyBeaconLocationData eData, StringBuilder pchDataStringOut, int cchDataStringOut )
		{
			if ( Config.Os == OsType.Windows )
			{
				SteamPartyBeaconLocation_t.Pack8 BeaconLocation_windows = BeaconLocation;
				var retVal = _GetBeaconLocationData_Windows( Self, BeaconLocation, eData, pchDataStringOut, cchDataStringOut );
				BeaconLocation = BeaconLocation_windows;
				return retVal;
			}
			
			return _GetBeaconLocationData( Self, BeaconLocation, eData, pchDataStringOut, cchDataStringOut );
		}
		
	}
}
