using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Steamworks.Data;

namespace Steamworks
{
	/// <summary>
	/// This API can be used to selectively advertise your multiplayer game session in a Steam chat room group. 
	/// Tell Steam the number of player spots that are available for your party, and a join-game string, and it
	/// will show a beacon in the selected group and allow that many users to “follow” the beacon to your party. 
	/// Adjust the number of open slots if other players join through alternate matchmaking methods.
	/// </summary>
	public class SteamParties : SteamClientClass<SteamParties>
	{
		internal static ISteamParties Internal => Interface as ISteamParties;

		internal override void InitializeInterface( bool server )
		{
			SetInterface( server, new ISteamParties( server ) );
			InstallEvents( server );
		}

		internal void InstallEvents( bool server )
		{
			Dispatch.Install<AvailableBeaconLocationsUpdated_t>( x => OnBeaconLocationsUpdated?.Invoke(), server );
			Dispatch.Install<ActiveBeaconsUpdated_t>( x => OnActiveBeaconsUpdated?.Invoke(), server );
		}

		/// <summary>
		/// The list of possible Party beacon locations has changed
		/// </summary>
		public static event Action OnBeaconLocationsUpdated;

		/// <summary>
		/// The list of active beacons may have changed
		/// </summary>
		public static event Action OnActiveBeaconsUpdated;


		public static int ActiveBeaconCount => (int) Internal.GetNumActiveBeacons();

		public static IEnumerable<PartyBeacon> ActiveBeacons
		{
			get
			{
				for ( uint i = 0; i < ActiveBeaconCount; i++ )
				{
					yield return new PartyBeacon
					{
						Id = Internal.GetBeaconByIndex( i )
					};
				}
			}
		}
	}
}