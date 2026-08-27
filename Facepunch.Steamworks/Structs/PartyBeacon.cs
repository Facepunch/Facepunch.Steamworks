using System.Threading.Tasks;
using Steamworks.Data;

namespace Steamworks
{
	/// <summary>
	/// A handle to a party beacon - an advertisement a player places in a Steam chat group saying "my
	/// game has room, come join". Other members of that group see it in their Steam UI and can follow it
	/// straight into the party. Enumerate the ones you can join through
	/// <see cref="SteamParties.ActiveBeacons"/>.
	/// </summary>
	/// <remarks>
	/// This binding only exposes the joining side end to end. Steam's beacon-creation call is not surfaced
	/// publicly by <see cref="SteamParties"/>, and the reservation notification that tells a host someone
	/// is inbound is not raised as an event anywhere, so <see cref="OnReservationCompleted"/>,
	/// <see cref="CancelReservation"/> and <see cref="Destroy"/> have no in-binding source of the
	/// information they need. Treat the host-side members here as incomplete rather than as a working
	/// hosting API.
	/// <para>
	/// Like the other handle types in this library, nothing is cached: <see cref="Owner"/> and
	/// <see cref="MetaData"/> each make their own native call every time you read them.
	/// </para>
	/// </remarks>
	public struct PartyBeacon
	{
		static ISteamParties Internal => SteamParties.Internal;

		internal PartyBeaconID_t Id;

		/// <summary>
		/// The player advertising this beacon - the person whose party you would be joining. Use it to
		/// show who is hosting, or to check whether they are a friend before following the beacon.
		/// </summary>
		/// <value>
		/// The beacon owner's <see cref="SteamId"/>. This binding discards Steam's success flag, so a
		/// beacon that has expired or was never valid yields a default <see cref="SteamId"/> rather than
		/// an error - check <c>IsValid</c> before trusting it.
		/// </value>
		public SteamId Owner
		{
			get
			{
				var owner = default( SteamId );
				var location = default( SteamPartyBeaconLocation_t );
				Internal.GetBeaconDetails( Id, ref owner, ref location, out _ );
				return owner;
			}
		}

		/// <summary>
		/// The free-form string the host attached to this beacon when they created it. Its meaning is
		/// entirely your game's - typically what is being played, so a player can choose between beacons.
		/// </summary>
		/// <value>
		/// The metadata string, or an empty string. As with <see cref="Owner"/>, Steam's success flag is
		/// discarded, so an expired or invalid beacon yields an empty string rather than an error, and
		/// that is indistinguishable from a host who set no metadata. Never <see langword="null"/>.
		/// </value>
		/// <remarks>
		/// Untrusted input written by another player - validate it before displaying or parsing.
		/// </remarks>
		public string MetaData
		{
			get
			{
				var owner = default( SteamId );
				var location = default( SteamPartyBeaconLocation_t );
				_ = Internal.GetBeaconDetails( Id, ref owner, ref location, out var strVal );
				return strVal;
			}
		}

		/// <summary>
		/// Will attempt to join the party. If successful will return a connection string.
		/// If failed, will return <see langword="null"/>
		/// </summary>
		/// <returns>
		/// The host's connect string on success - an opaque, game-defined value that your code interprets
		/// to reach the party, exactly as the host supplied it when creating the beacon. Steam does not
		/// connect you; acting on the string is your job.
		/// <para>
		/// <see langword="null"/> on failure. The two failure modes - the call result never arriving, and
		/// Steam returning a non-OK result (typically the beacon is gone or its slots were taken) - are not
		/// distinguishable here.
		/// </para>
		/// </returns>
		/// <remarks>
		/// The SDK header notes that a successful join reserves a beacon slot for the local user, so
		/// calling this speculatively consumes capacity in the host's party. The task only completes while
		/// Steam callbacks are being pumped.
		/// <para>
		/// The connect string is another player's data. Validate it before using it to open a connection.
		/// </para>
		/// </remarks>
		public async Task<string> JoinAsync()
		{
			var result = await Internal.JoinParty( Id );
			if ( !result.HasValue || result.Value.Result != Result.OK )
				return null;

			return result.Value.ConnectStringUTF8();
		}

		/// <summary>
		/// When a user follows your beacon, Steam will reserve one of the open party slots for them, and send your game a ReservationNotification callback. 
		/// When that user joins your party, call this method to notify Steam that the user has joined successfully.
		/// </summary>
		/// <param name="steamid">The user who had a reservation and has now actually joined your party.</param>
		/// <remarks>
		/// Host-side, and confirmation only - it does not admit anyone, it tells Steam that a slot it was
		/// holding open is now genuinely occupied so it can manage the remaining open slots. Failing to
		/// call it leaves the reservation outstanding until Steam times it out, which needlessly narrows
		/// your beacon.
		/// <para>
		/// Returns nothing and Steam reports no result, so a call for a user who never had a reservation
		/// does nothing observable.
		/// </para>
		/// <para>
		/// This binding does not raise the reservation notification the summary refers to as an event, so
		/// there is no supported way to learn a user is inbound through Facepunch.Steamworks. In practice
		/// you would call this from your own join handling instead.
		/// </para>
		/// </remarks>
		public void OnReservationCompleted( SteamId steamid )
		{
			Internal.OnReservationCompleted( Id, steamid );
		}

		/// <summary>
		/// To cancel a reservation (due to timeout or user input), call this.
		/// Steam will open a new reservation slot.
		/// Note: The user may already be in-flight to your game, so it's possible they will still connect and try to join your party.
		/// </summary>
		/// <param name="steamid">The user whose held slot should be released.</param>
		/// <remarks>
		/// Host-side. The counterpart to <see cref="OnReservationCompleted"/>: use it when the reserved
		/// player is not going to arrive. Returns nothing and Steam reports no result, so this fails
		/// silently if the user had no reservation.
		/// <para>
		/// Because of the in-flight case the SDK header warns about, cancelling is not a way to reject
		/// someone - you must still handle their arrival in your own join logic.
		/// </para>
		/// </remarks>
		public void CancelReservation( SteamId steamid )
		{
			Internal.CancelReservation( Id, steamid );
		}

		/// <summary>
		/// Takes the beacon down so it stops being advertised and no further players are sent to your
		/// party. Call this once the party is full or the session has started.
		/// </summary>
		/// <returns>
		/// <see langword="true"/> if Steam accepted the request. <see langword="false"/> gives no reason -
		/// the SDK header documents nothing about this return value beyond its existence, so the most you
		/// can conclude is that the beacon was not torn down.
		/// </returns>
		/// <remarks>
		/// Only meaningful for a beacon you created. Valve does not document what happens to reservations
		/// that were already outstanding when the beacon is destroyed, so players who were in-flight may
		/// still arrive.
		/// </remarks>
		public bool Destroy()
		{
			return Internal.DestroyBeacon( Id );
		}
	}
}
