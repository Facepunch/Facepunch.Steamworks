using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;


namespace Steamworks
{
	/// <summary>
	/// A 64-bit Steam identifier. Despite the name it does not only identify users: the same type
	/// names lobbies, clans (Steam groups), game servers and chat rooms, because Valve packs an
	/// account type into the upper bits. What a given value refers to depends on where you got it.
	/// </summary>
	/// <remarks>
	/// <para>
	/// The number you see on a Steam profile URL (<c>76561197960287930</c>) is one of these. It
	/// converts implicitly to and from <see cref="ulong"/>, so it is cheap to store and to send over
	/// a wire protocol.
	/// </para>
	/// <para>
	/// <b>This type does not decode the account type.</b> It exposes the raw <see cref="Value"/> and
	/// the lower 32 bits as <see cref="AccountId"/>, and nothing else. If you need to know whether a
	/// value is an individual, a clan or a game server, you must inspect the bits yourself against
	/// Valve's <c>EAccountType</c> layout in <c>steamclientpublic.h</c>.
	/// </para>
	/// </remarks>
	/// <example>
	/// <code>
	/// SteamId me = SteamClient.SteamId;
	/// Console.WriteLine( me );              // 76561197960287930
	/// Console.WriteLine( me.AccountId );    // 22202 - the "friend code" style short id
	///
	/// ulong wire = me;                      // implicit, for serialising
	/// SteamId back = wire;                  // implicit, for deserialising
	/// </code>
	/// </example>
	public struct SteamId
	{
		/// <summary>
		/// The raw 64-bit identifier, exactly as Steam represents it. This is the value to persist or
		/// transmit; every other member here is derived from it.
		/// </summary>
		public ulong Value;

		/// <summary>
		/// Wraps a raw 64-bit Steam identifier. Implicit, and performs no validation &#8212; any
		/// <see cref="ulong"/> converts, including values that name nothing.
		/// </summary>
		/// <param name="value">The raw identifier.</param>
		/// <returns>A <see cref="SteamId"/> carrying that value.</returns>
		public static implicit operator SteamId( ulong value )
		{
			return new SteamId { Value = value };
		}

		/// <summary>
		/// Unwraps to the raw 64-bit identifier, for storage or transmission.
		/// </summary>
		/// <param name="value">The id to unwrap.</param>
		/// <returns>The underlying <see cref="Value"/>.</returns>
		public static implicit operator ulong( SteamId value )
		{
			return value.Value;
		}

		/// <summary>
		/// The identifier rendered as its plain decimal digits, matching how Steam shows it in profile
		/// URLs and the Web API. Not a display name &#8212; use <c>Friend.Name</c> for that.
		/// </summary>
		/// <returns>The decimal form of <see cref="Value"/>.</returns>
		public override string ToString() => Value.ToString();

		/// <summary>
		/// The lower 32 bits of the identifier, which is the per-account number Steam calls the
		/// account id. Several Steamworks calls take this narrower form rather than the full 64-bit
		/// id &#8212; the Workshop user queries are the common example.
		/// </summary>
		/// <remarks>
		/// This is <b>lossy</b>: it discards the universe, account type and instance bits, so two
		/// different <see cref="SteamId"/> values can share an account id. Never use it as a key, and
		/// never try to reconstruct a <see cref="SteamId"/> from it.
		/// </remarks>
		public uint AccountId => (uint) (Value & 0xFFFFFFFFul);

		/// <summary>
		/// Whether this holds anything at all. This is only a check for zero &#8212; it does not
		/// verify that the id is well formed, that the account exists, or that it is of the type you
		/// expect. A garbage non-zero number reports as valid.
		/// </summary>
		public bool IsValid => Value != default;
	}
}
