using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Steamworks.Data;


namespace Steamworks
{
	internal class ISteamParties : SteamInterface
	{
		public override void InitInternals()
		{
		}
		internal override void Shutdown()
		{
			base.Shutdown();
			
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamParties_GetNumActiveBeacons")]
		private static extern uint _GetNumActiveBeacons( IntPtr self );
		
		#endregion
		internal uint GetNumActiveBeacons()
		{
			var returnValue = _GetNumActiveBeacons( Self );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamParties_GetBeaconByIndex")]
		private static extern PartyBeaconID_t _GetBeaconByIndex( IntPtr self, uint unIndex );
		
		#endregion
		internal PartyBeaconID_t GetBeaconByIndex( uint unIndex )
		{
			var returnValue = _GetBeaconByIndex( Self, unIndex );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamParties_GetBeaconDetails")]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _GetBeaconDetails( IntPtr self, PartyBeaconID_t ulBeaconID, ref SteamId pSteamIDBeaconOwner, ref SteamPartyBeaconLocation_t pLocation, IntPtr pchMetadata, int cchMetadata );
		
		#endregion
		internal bool GetBeaconDetails( PartyBeaconID_t ulBeaconID, ref SteamId pSteamIDBeaconOwner, ref SteamPartyBeaconLocation_t pLocation, out string pchMetadata )
		{
			IntPtr mempchMetadata = Helpers.TakeMemory();
			var returnValue = _GetBeaconDetails( Self, ulBeaconID, ref pSteamIDBeaconOwner, ref pLocation, mempchMetadata, (1024 * 32) );
			pchMetadata = Helpers.MemoryToString( mempchMetadata );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamParties_JoinParty")]
		private static extern SteamAPICall_t _JoinParty( IntPtr self, PartyBeaconID_t ulBeaconID );
		
		#endregion
		internal CallbackResult JoinParty( PartyBeaconID_t ulBeaconID )
		{
			var returnValue = _JoinParty( Self, ulBeaconID );
			return new CallbackResult( returnValue );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamParties_GetNumAvailableBeaconLocations")]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _GetNumAvailableBeaconLocations( IntPtr self, ref uint puNumLocations );
		
		#endregion
		internal bool GetNumAvailableBeaconLocations( ref uint puNumLocations )
		{
			var returnValue = _GetNumAvailableBeaconLocations( Self, ref puNumLocations );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamParties_GetAvailableBeaconLocations")]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _GetAvailableBeaconLocations( IntPtr self, ref SteamPartyBeaconLocation_t pLocationList, uint uMaxNumLocations );
		
		#endregion
		internal bool GetAvailableBeaconLocations( ref SteamPartyBeaconLocation_t pLocationList, uint uMaxNumLocations )
		{
			var returnValue = _GetAvailableBeaconLocations( Self, ref pLocationList, uMaxNumLocations );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamParties_CreateBeacon")]
		private static extern SteamAPICall_t _CreateBeacon( IntPtr self, uint unOpenSlots, ref SteamPartyBeaconLocation_t pBeaconLocation, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchConnectString, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchMetadata );
		
		#endregion
		internal CallbackResult CreateBeacon( uint unOpenSlots,  /* ref */ SteamPartyBeaconLocation_t pBeaconLocation, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchConnectString, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchMetadata )
		{
			var returnValue = _CreateBeacon( Self, unOpenSlots, ref pBeaconLocation, pchConnectString, pchMetadata );
			return new CallbackResult( returnValue );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamParties_OnReservationCompleted")]
		private static extern void _OnReservationCompleted( IntPtr self, PartyBeaconID_t ulBeacon, SteamId steamIDUser );
		
		#endregion
		internal void OnReservationCompleted( PartyBeaconID_t ulBeacon, SteamId steamIDUser )
		{
			_OnReservationCompleted( Self, ulBeacon, steamIDUser );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamParties_CancelReservation")]
		private static extern void _CancelReservation( IntPtr self, PartyBeaconID_t ulBeacon, SteamId steamIDUser );
		
		#endregion
		internal void CancelReservation( PartyBeaconID_t ulBeacon, SteamId steamIDUser )
		{
			_CancelReservation( Self, ulBeacon, steamIDUser );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamParties_ChangeNumOpenSlots")]
		private static extern SteamAPICall_t _ChangeNumOpenSlots( IntPtr self, PartyBeaconID_t ulBeacon, uint unOpenSlots );
		
		#endregion
		internal CallbackResult ChangeNumOpenSlots( PartyBeaconID_t ulBeacon, uint unOpenSlots )
		{
			var returnValue = _ChangeNumOpenSlots( Self, ulBeacon, unOpenSlots );
			return new CallbackResult( returnValue );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamParties_DestroyBeacon")]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _DestroyBeacon( IntPtr self, PartyBeaconID_t ulBeacon );
		
		#endregion
		internal bool DestroyBeacon( PartyBeaconID_t ulBeacon )
		{
			var returnValue = _DestroyBeacon( Self, ulBeacon );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamParties_GetBeaconLocationData")]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _GetBeaconLocationData( IntPtr self, SteamPartyBeaconLocation_t BeaconLocation, SteamPartyBeaconLocationData eData, IntPtr pchDataStringOut, int cchDataStringOut );
		
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
