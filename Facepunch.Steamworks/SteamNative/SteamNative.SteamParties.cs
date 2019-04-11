using System;
using System.Runtime.InteropServices;
using System.Linq;

namespace SteamNative
{
	internal unsafe class SteamParties : IDisposable
	{
		//
		// Holds a platform specific implentation
		//
		internal Platform.Interface platform;
		internal Facepunch.Steamworks.BaseSteamworks steamworks;
		
		//
		// Constructor decides which implementation to use based on current platform
		//
		internal SteamParties( Facepunch.Steamworks.BaseSteamworks steamworks, IntPtr pointer )
		{
			this.steamworks = steamworks;
			
			if ( Platform.IsWindows ) platform = new Platform.Windows( pointer );
			else if ( Platform.IsLinux ) platform = new Platform.Linux( pointer );
			else if ( Platform.IsOsx ) platform = new Platform.Mac( pointer );
		}
		
		//
		// Class is invalid if we don't have a valid implementation
		//
		public bool IsValid{ get{ return platform != null && platform.IsValid; } }
		
		//
		// When shutting down clear all the internals to avoid accidental use
		//
		public virtual void Dispose()
		{
			 if ( platform != null )
			{
				platform.Dispose();
				platform = null;
			}
		}
		
		// void
		public void CancelReservation( PartyBeaconID_t ulBeacon /*PartyBeaconID_t*/, CSteamID steamIDUser /*class CSteamID*/ )
		{
			platform.ISteamParties_CancelReservation( ulBeacon.Value, steamIDUser.Value );
		}
		
		// SteamAPICall_t
		public CallbackHandle ChangeNumOpenSlots( PartyBeaconID_t ulBeacon /*PartyBeaconID_t*/, uint unOpenSlots /*uint32*/, Action<ChangeNumOpenSlotsCallback_t, bool> CallbackFunction = null /*Action<ChangeNumOpenSlotsCallback_t, bool>*/ )
		{
			SteamAPICall_t callback = 0;
			callback = platform.ISteamParties_ChangeNumOpenSlots( ulBeacon.Value, unOpenSlots );
			
			if ( CallbackFunction == null ) return null;
			if ( callback == 0 ) return null;
			
			return ChangeNumOpenSlotsCallback_t.CallResult( steamworks, callback, CallbackFunction );
		}
		
		// SteamAPICall_t
		public CallbackHandle CreateBeacon( uint unOpenSlots /*uint32*/, ref SteamPartyBeaconLocation_t pBeaconLocation /*struct SteamPartyBeaconLocation_t **/, string pchConnectString /*const char **/, string pchMetadata /*const char **/, Action<CreateBeaconCallback_t, bool> CallbackFunction = null /*Action<CreateBeaconCallback_t, bool>*/ )
		{
			SteamAPICall_t callback = 0;
			callback = platform.ISteamParties_CreateBeacon( unOpenSlots, ref pBeaconLocation, pchConnectString, pchMetadata );
			
			if ( CallbackFunction == null ) return null;
			if ( callback == 0 ) return null;
			
			return CreateBeaconCallback_t.CallResult( steamworks, callback, CallbackFunction );
		}
		
		// bool
		public bool DestroyBeacon( PartyBeaconID_t ulBeacon /*PartyBeaconID_t*/ )
		{
			return platform.ISteamParties_DestroyBeacon( ulBeacon.Value );
		}
		
		// bool
		public bool GetAvailableBeaconLocations( ref SteamPartyBeaconLocation_t pLocationList /*struct SteamPartyBeaconLocation_t **/, uint uMaxNumLocations /*uint32*/ )
		{
			return platform.ISteamParties_GetAvailableBeaconLocations( ref pLocationList, uMaxNumLocations );
		}
		
		// PartyBeaconID_t
		public PartyBeaconID_t GetBeaconByIndex( uint unIndex /*uint32*/ )
		{
			return platform.ISteamParties_GetBeaconByIndex( unIndex );
		}
		
		// bool
		// with: Detect_StringFetch False
		public bool GetBeaconDetails( PartyBeaconID_t ulBeaconID /*PartyBeaconID_t*/, out CSteamID pSteamIDBeaconOwner /*class CSteamID **/, ref SteamPartyBeaconLocation_t pLocation /*struct SteamPartyBeaconLocation_t **/, out string pchMetadata /*char **/ )
		{
			bool bSuccess = default( bool );
			pchMetadata = string.Empty;
			System.Text.StringBuilder pchMetadata_sb = Helpers.TakeStringBuilder();
			int cchMetadata = 4096;
			bSuccess = platform.ISteamParties_GetBeaconDetails( ulBeaconID.Value, out pSteamIDBeaconOwner.Value, ref pLocation, pchMetadata_sb, cchMetadata );
			if ( !bSuccess ) return bSuccess;
			pchMetadata = pchMetadata_sb.ToString();
			return bSuccess;
		}
		
		// bool
		// with: Detect_StringFetch False
		public bool GetBeaconLocationData( SteamPartyBeaconLocation_t BeaconLocation /*struct SteamPartyBeaconLocation_t*/, SteamPartyBeaconLocationData eData /*ESteamPartyBeaconLocationData*/, out string pchDataStringOut /*char **/ )
		{
			bool bSuccess = default( bool );
			pchDataStringOut = string.Empty;
			System.Text.StringBuilder pchDataStringOut_sb = Helpers.TakeStringBuilder();
			int cchDataStringOut = 4096;
			bSuccess = platform.ISteamParties_GetBeaconLocationData( BeaconLocation, eData, pchDataStringOut_sb, cchDataStringOut );
			if ( !bSuccess ) return bSuccess;
			pchDataStringOut = pchDataStringOut_sb.ToString();
			return bSuccess;
		}
		
		// uint
		public uint GetNumActiveBeacons()
		{
			return platform.ISteamParties_GetNumActiveBeacons();
		}
		
		// bool
		public bool GetNumAvailableBeaconLocations( IntPtr puNumLocations /*uint32 **/ )
		{
			return platform.ISteamParties_GetNumAvailableBeaconLocations( (IntPtr) puNumLocations );
		}
		
		// SteamAPICall_t
		public CallbackHandle JoinParty( PartyBeaconID_t ulBeaconID /*PartyBeaconID_t*/, Action<JoinPartyCallback_t, bool> CallbackFunction = null /*Action<JoinPartyCallback_t, bool>*/ )
		{
			SteamAPICall_t callback = 0;
			callback = platform.ISteamParties_JoinParty( ulBeaconID.Value );
			
			if ( CallbackFunction == null ) return null;
			if ( callback == 0 ) return null;
			
			return JoinPartyCallback_t.CallResult( steamworks, callback, CallbackFunction );
		}
		
		// void
		public void OnReservationCompleted( PartyBeaconID_t ulBeacon /*PartyBeaconID_t*/, CSteamID steamIDUser /*class CSteamID*/ )
		{
			platform.ISteamParties_OnReservationCompleted( ulBeacon.Value, steamIDUser.Value );
		}
		
	}
}
