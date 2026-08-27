using System;

namespace Steamworks
{
	/// <summary>
	/// Constants for the Steam Input action-origin lookups.
	/// </summary>
	/// <remarks>
	/// Hand-written so the value cannot be lost to a regeneration. The origin lookups are the
	/// only functions in this interface whose output parameter is an ARRAY rather than a
	/// single value, and getting that wrong is a buffer overflow rather than a wrong answer —
	/// see the remarks on <c>GetDigitalActionOrigins</c>.
	/// </remarks>
	internal unsafe partial class ISteamInput
	{
		/// <summary>
		/// How many origins Steam may write into the buffer passed to
		/// <c>GetDigitalActionOrigins</c> / <c>GetAnalogActionOrigins</c>.
		/// </summary>
		/// <remarks>
		/// Mirrors <c>STEAM_INPUT_MAX_ORIGINS</c>, defined as <c>8</c> at
		/// <c>Generator/steam_sdk/isteaminput.h:24</c>. Both functions annotate their output
		/// parameter <c>STEAM_OUT_ARRAY_COUNT( STEAM_INPUT_MAX_ORIGINS, ... )</c>, and the
		/// comment at <c>:823</c> states the buffer "should point to a STEAM_INPUT_MAX_ORIGINS
		/// sized array". A single action can genuinely be bound to several inputs at once,
		/// which is why the result is a list.
		/// </remarks>
		internal const int STEAM_INPUT_MAX_ORIGINS = 8;
	}
}
