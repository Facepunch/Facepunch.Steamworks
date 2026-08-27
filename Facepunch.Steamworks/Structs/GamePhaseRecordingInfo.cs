namespace Steamworks;

/// <summary>
/// What Steam has captured for one game phase: how much background footage exists, how many clips the user
/// saved out of it, and how many screenshots they took. Returned by
/// <see cref="SteamTimeline.DoesGamePhaseRecordingExist"/>.
/// <para>
/// The practical use is deciding whether a "watch this again" or "share this match" control is worth showing
/// at all, rather than presenting a button that opens the overlay onto nothing.
/// </para>
/// </summary>
/// <remarks>
/// <para>
/// <b>Durations here are milliseconds.</b> Every other time value in <see cref="SteamTimeline"/> is a
/// <see langword="float"/> offset in seconds; these two are <see langword="ulong"/> counts of milliseconds.
/// Divide by 1000 before comparing the two.
/// </para>
/// <para>
/// <b>Valve documents none of these fields.</b> The Steamworks SDK declares the underlying struct with no
/// per-field comments at all, so the meanings below are read off the field names and this binding's
/// mapping of them. In particular there is no explicit "a recording exists" flag anywhere in the payload —
/// deciding that from the counters being zero is an inference, not documented behaviour.
/// </para>
/// </remarks>
/// <example>
/// <code>
/// var info = await SteamTimeline.DoesGamePhaseRecordingExist( matchId );
///
/// // null means the Steam call failed — not the same as "nothing was recorded".
/// if ( info is GamePhaseRecordingInfo rec &amp;&amp; rec.RecordingMs &gt; 0 )
/// {
///     WatchButton.Label = $"Watch match ({rec.RecordingMs / 1000}s, {rec.ClipCount} clips)";
///     WatchButton.OnClick = () =&gt; SteamTimeline.OpenOverlayToGamePhase( matchId );
///     WatchButton.Show();
/// }
/// </code>
/// </example>
public struct GamePhaseRecordingInfo
{
	/// <summary>
	/// The phase this describes, echoed back from the value the game passed to
	/// <see cref="SteamTimeline.SetGamePhaseId"/>. Steam returns it out of a fixed 64-byte buffer, so a
	/// phase ID longer than 63 bytes of UTF-8 may not survive the round trip intact.
	/// </summary>
	public string PhaseId;
	/// <summary>
	/// Total background recording Steam holds for the phase, in <b>milliseconds</b>. Zero is the closest
	/// thing to a "nothing was recorded" signal this struct offers.
	/// </summary>
	public ulong RecordingMs;
	/// <summary>
	/// Length of the longest clip the user saved from this phase, in <b>milliseconds</b>. This is one clip's
	/// length, not the total across all of them.
	/// </summary>
	public ulong LongestClipMs;
	/// <summary>
	/// How many clips the user saved from this phase.
	/// </summary>
	public uint ClipCount;
	/// <summary>
	/// How many screenshots the user took during this phase.
	/// </summary>
	public uint ScreenshotCount;
}
