
using Steamworks.Data;

namespace Steamworks.Data
{
	/// <summary>
	/// Buffer for the English-language diagnostic messages Steam writes on failure —
	/// <c>SteamNetworkingErrMsg</c> in the SDK.
	/// </summary>
	/// <remarks>
	/// <para>
	/// <c>steamnetworkingtypes.h:87</c> declares this as
	/// <c>typedef char SteamNetworkingErrMsg[ k_cchMaxSteamNetworkingErrMsg ]</c> with
	/// <c>k_cchMaxSteamNetworkingErrMsg = 1024</c> — that is 1024 <b>bytes</b> of
	/// NUL-terminated UTF-8.
	/// </para>
	/// <para>
	/// The field is <c>byte</c>, not <c>char</c>. A C# <c>char</c> is UTF-16, so
	/// <c>fixed char Value[1024]</c> would reserve 2048 bytes for a 1024-byte native buffer
	/// and — more importantly — the contents could not be read as <c>char</c>s at all, since
	/// Steam writes UTF-8. Reading UTF-8 bytes as UTF-16 code units yields mojibake.
	/// </para>
	/// <para>
	/// Decode with <c>Utility.Utf8NoBom.GetString</c>, scanning to the first zero byte and
	/// bounding the scan at 1024 in case Steam does not terminate.
	/// </para>
	/// </remarks>
	internal unsafe struct NetErrorMessage
	{
		/// <summary>Maximum length in bytes, matching <c>k_cchMaxSteamNetworkingErrMsg</c>.</summary>
		public const int MaxLength = 1024;

		public fixed byte Value[MaxLength];
	}
}
