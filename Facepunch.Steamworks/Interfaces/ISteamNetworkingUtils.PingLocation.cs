using System;
using Steamworks.Data;

namespace Steamworks
{
	/// <summary>
	/// Caller-buffer forms of the ping-location string functions.
	/// </summary>
	/// <remarks>
	/// <para>
	/// These add <b>no new P/Invoke</b>. They call the same
	/// <c>SteamAPI_ISteamNetworkingUtils_ConvertPingLocationToString</c> and
	/// <c>SteamAPI_ISteamNetworkingUtils_ParsePingLocationString</c> externs the generator already
	/// emitted in <c>Generated/Interfaces/ISteamNetworkingUtils.cs</c> — a <c>partial</c> class
	/// shares member accessibility across files, so the generated <c>private extern</c> is
	/// reachable from here.
	/// </para>
	/// <para>
	/// <b>Why these exist.</b> The generated wrappers are shaped for convenience, not for volume:
	/// <c>ConvertPingLocationToString</c> leases a 32 KB buffer from the shared
	/// <see cref="Helpers"/> pool (behind a lock) and returns a <see cref="string"/>, and
	/// <c>ParsePingLocationString</c> takes a <see cref="string"/> and heap-allocates a
	/// NUL-terminated UTF-8 copy of it. A matchmaker converting a location per player per query
	/// pays both costs on every call. These overloads let the public layer in
	/// <see cref="SteamNetworkingUtils"/> use a 1 KB stack buffer instead, which is all Valve's own
	/// <c>k_cchMaxSteamNetworkingPingLocationString</c> asks for.
	/// </para>
	/// <para>
	/// Because this is a hand-written <c>partial</c>, re-running the generator will not delete it.
	/// </para>
	/// </remarks>
	internal unsafe partial class ISteamNetworkingUtils
	{
		/// <summary>
		/// Writes the location's string form as NUL-terminated UTF-8 into a caller-owned buffer.
		/// </summary>
		/// <param name="location">The location to serialise.</param>
		/// <param name="buffer">Destination. Must hold at least <paramref name="bufferSize"/> bytes.</param>
		/// <param name="bufferSize">
		/// Capacity of <paramref name="buffer"/> in bytes. Valve requires at least
		/// <c>k_cchMaxSteamNetworkingPingLocationString</c> (1024).
		/// </param>
		internal void ConvertPingLocationToString( ref NetPingLocation location, IntPtr buffer, int bufferSize )
		{
			_ConvertPingLocationToString( Self, ref location, buffer, bufferSize );
		}

		/// <summary>
		/// Parses a location from a caller-owned NUL-terminated UTF-8 buffer.
		/// </summary>
		/// <param name="utf8String">Pointer to NUL-terminated UTF-8. Must not be <see cref="IntPtr.Zero"/>.</param>
		/// <param name="result">Receives the parsed location on success.</param>
		/// <returns><see langword="false"/> if Steam could not understand the string.</returns>
		internal bool ParsePingLocationString( IntPtr utf8String, ref NetPingLocation result )
		{
			return _ParsePingLocationString( Self, utf8String, ref result );
		}
	}
}
