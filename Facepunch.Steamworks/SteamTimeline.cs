using System;
using System.Threading.Tasks;
using Steamworks.Data;

namespace Steamworks;

/// <summary>
/// Annotates the Steam Recording timeline for your game, so that Steam's recording and clipping UI can
/// show meaningful chapter markers instead of an undifferentiated block of footage.
/// <para>
/// There are three independent layers, and most games use all three:
/// </para>
/// <list type="bullet">
/// <item><description><b>Game mode</b> (<see cref="SetTimelineGameMode"/>) colours the timeline bar so
/// menus, loading and live gameplay are visually distinct at a glance.</description></item>
/// <item><description><b>Events</b> (<see cref="AddInstantaneousTimelineEvent"/>,
/// <see cref="AddRangeTimelineEvent"/>, <see cref="StartRangeTimelineEvent"/>) place icon markers on the
/// bar — a boss fight, a goal scored, a cut scene — and can nominate themselves as clip
/// suggestions.</description></item>
/// <item><description><b>Game phases</b> (<see cref="StartGamePhase"/>) split a play session into the
/// coarse rows a user browses by: a match, a chapter, a roguelike run. Valve's guidance is roughly ten
/// minutes to a few hours per phase.</description></item>
/// </list>
/// </summary>
/// <remarks>
/// <para>
/// <b>Icons are a Steamworks partner-site prerequisite, and failing that check is silent.</b> Every
/// <c>icon</c> argument in this class must be either the name of an icon your app has uploaded through the
/// Steamworks partner site, or one of Valve's built-in names, which all begin with <c>steam_</c>
/// (<c>steam_heart</c>, <c>steam_flag</c> and <c>steam_starburst</c> appear in Valve's own examples). An app
/// that has never configured timeline icons on the partner site can therefore only use the <c>steam_</c>
/// set. Note that none of these calls return a status and none raise a callback, so an icon name that
/// matches neither an uploaded icon nor a built-in one cannot be detected at runtime — Valve does not
/// document what, if anything, gets drawn in that case.
/// </para>
/// <para>
/// <b>Priorities are 0 to 1000.</b> The SDK caps them with <c>k_unMaxTimelinePriority</c> = 1000; larger
/// values are meant to display more prominently, and where space is tight lower-priority items may be
/// hidden entirely. Valve does not document whether out-of-range values are clamped or rejected. That
/// constant, and the related <c>k_unTimelinePriority_KeepCurrentValue</c>,
/// <c>k_flMaxTimelineEventDuration</c> and <c>k_cchMaxPhaseIDLength</c>, are <see langword="internal"/> in
/// this binding, so callers have to use the literal values documented on each member below.
/// </para>
/// <para>
/// <b>All time offsets are in seconds, relative to now, and negative means the past.</b> There are no
/// absolute timestamps anywhere in this API and no milliseconds — the only milliseconds in the whole
/// surface are the read-back counters on <see cref="GamePhaseRecordingInfo"/>.
/// </para>
/// <para>
/// <b>Everything except the two query methods is fire-and-forget.</b> The mutating calls return
/// <see langword="void"/> and report nothing, so a bad icon name, an unknown phase ID or a stale event
/// handle all fail quietly.
/// </para>
/// </remarks>
/// <example>
/// A multiplayer match marked up end to end:
/// <code>
/// // Player is picking a loadout
/// SteamTimeline.SetTimelineGameMode( TimelineGameMode.Menus );
///
/// // The match itself is one phase
/// SteamTimeline.SetTimelineGameMode( TimelineGameMode.Playing );
/// SteamTimeline.StartGamePhase();
/// SteamTimeline.SetGamePhaseId( matchId );                        // 63 bytes of UTF-8 at most
/// SteamTimeline.SetGamePhaseAttribute( "KDA", "0/0/0", 100 );
///
/// // A boss fight we would like Steam to offer as a clip
/// var boss = SteamTimeline.StartRangeTimelineEvent(
///     "Vault Guardian", "Phase 2 enrage", "steam_flag",
///     priority: 800, startOffsetSeconds: 0f, TimelineEventClipPriority.Featured );
///
/// // ... the fight happens ...
/// SteamTimeline.SetGamePhaseAttribute( "KDA", "7/1/3", 100 );     // overwrites the earlier value
/// SteamTimeline.EndRangeTimelineEvent( boss, 0f );                // MUST be called or the event is discarded
/// SteamTimeline.AddGamePhaseTag( "Vault Guardian", "steam_flag", "Bosses Defeated", 500 );
///
/// SteamTimeline.EndGamePhase();
/// SteamTimeline.SetTimelineGameMode( TimelineGameMode.Menus );
/// </code>
/// </example>
public class SteamTimeline : SteamClientClass<SteamTimeline>
{
	internal static ISteamTimeline Internal => Interface as ISteamTimeline;

	internal override bool InitializeInterface( bool server )
	{
		SetInterface( server, new ISteamTimeline( server ) );
		if ( Interface.Self == IntPtr.Zero ) return false;

		InstallEvents();
		return true;
	}

	internal static void InstallEvents()
	{
	}

	/// <summary>
	/// Sets a description for the current game state in the timeline. These help the user to find specific moments in the timeline when saving clips. Setting a
	/// new state description replaces any previous description.
	/// <para>
	/// Valve's suggested uses are things that answer "what was going on here?" while scrubbing: where the
	/// player is in the world, which round is being played, or the current score.
	/// </para>
	/// </summary>
	/// <param name="description">
	/// The text shown to the user. Valve requires this to be localized into the language reported by
	/// <see cref="SteamUtils.SteamUILanguage"/> — Steam does not translate it for you. There is only ever one
	/// live tooltip: this replaces the previous one rather than stacking on it.
	/// </param>
	/// <param name="timeOffsetSeconds">
	/// When the description takes effect, in <b>seconds relative to now</b> — not an absolute timestamp and
	/// not milliseconds. Pass <c>0f</c> for "as of this instant". Valve documents negative values as meaning
	/// the change happened in the past; positive (future) offsets are not documented.
	/// </param>
	/// <remarks>
	/// Returns nothing and raises no callback, so a tooltip that never appears cannot be diagnosed from the
	/// call site. Clear it with <see cref="ClearTimelineTooltip"/>.
	/// </remarks>
	public static void SetTimelineTooltip( string description, float timeOffsetSeconds )
	{
		Internal.SetTimelineTooltip( description, timeOffsetSeconds );
	}

	/// <summary>
	/// Clears the previous set game state in the timeline.
	/// </summary>
	/// <param name="timeOffsetSeconds">
	/// When the clear takes effect, in <b>seconds relative to now</b>; negative values place it in the past.
	/// Valve gives this parameter no description of its own beyond the shared <c>flTimeDelta</c> note it
	/// inherits from <see cref="SetTimelineTooltip"/>, so treat the two as having identical semantics.
	/// </param>
	public static void ClearTimelineTooltip( float timeOffsetSeconds )
	{
		Internal.ClearTimelineTooltip( timeOffsetSeconds );
	}

	/// <summary>
	/// Use this to mark an event on the Timeline. This event will be instantaneous. (See <see cref="AddRangeTimelineEvent"/> to add events that happened over time.)
	/// <para>
	/// Valve's examples of what deserves a marker: picking up a new weapon, scoring a goal, the start of a
	/// cut scene, a large team fight.
	/// </para>
	/// </summary>
	/// <param name="title">Short marker label, localized into <see cref="SteamUtils.SteamUILanguage"/> by you.</param>
	/// <param name="description">Longer detail shown alongside the title, also localized by you.</param>
	/// <param name="icon">
	/// Either an icon name your app uploaded through the Steamworks partner site, or one of Valve's built-in
	/// names beginning with <c>steam_</c> (for example <c>steam_heart</c>). An app with no partner-site icons
	/// configured is limited to the built-in set. Nothing validates this string.
	/// </param>
	/// <param name="priority">
	/// How prominently to display this marker relative to your other markers, from 0 to 1000
	/// (<c>k_unMaxTimelinePriority</c>). Higher is more prominent; low-priority markers may be dropped where
	/// the UI is short of space. Valve does not document the behaviour of values above 1000.
	/// </param>
	/// <param name="startOffsetSeconds">
	/// When the event happened, in <b>seconds relative to now</b>. Pass <c>0f</c> to mark this instant;
	/// negative values mark something that already happened, which is the usual case when you only detect the
	/// event a frame or two late.
	/// </param>
	/// <param name="possibleClip">
	/// Whether Steam should offer this moment to the user as a suggested clip.
	/// <see cref="TimelineEventClipPriority.None"/> means "marker only, do not suggest a clip";
	/// <see cref="TimelineEventClipPriority.Standard"/> and <see cref="TimelineEventClipPriority.Featured"/>
	/// both request a suggestion, with Featured offered ahead of Standard. For an instantaneous event Valve
	/// builds the suggested clip from a short window before and after the marker.
	/// </param>
	/// <returns>
	/// A handle for referring to this event later — <see cref="DoesEventRecordingExist"/>,
	/// <see cref="OpenOverlayToTimelineEvent"/> and <see cref="RemoveTimelineEvent"/> all take one. The handle
	/// is only meaningful inside the current game process and becomes invalid when the game exits, so never
	/// persist it. Valve documents no failure value, so a returned handle is not evidence that the event was
	/// accepted.
	/// </returns>
	public static TimelineEventHandle AddInstantaneousTimelineEvent( string title, string description, string icon,
		uint priority, float startOffsetSeconds, TimelineEventClipPriority possibleClip )
	{
		return Internal.AddInstantaneousTimelineEvent( title, description, icon, priority, startOffsetSeconds,
			possibleClip );
	}

	/// <summary>
	/// Use this to mark an event on the Timeline that takes some amount of time to complete.
	/// <para>
	/// Use this overload when you already know how long the event lasted — typically because it is already
	/// over by the time you notice. If the end is not known yet, use <see cref="StartRangeTimelineEvent"/>
	/// and <see cref="EndRangeTimelineEvent"/> instead.
	/// </para>
	/// </summary>
	/// <param name="title">Short marker label, localized into <see cref="SteamUtils.SteamUILanguage"/> by you.</param>
	/// <param name="description">Longer detail shown alongside the title, also localized by you.</param>
	/// <param name="icon">
	/// A partner-site icon name for your app, or a Valve built-in name beginning with <c>steam_</c>. Not
	/// validated, and a bad name fails silently.
	/// </param>
	/// <param name="priority">
	/// Display prominence relative to your other markers, 0 to 1000 (<c>k_unMaxTimelinePriority</c>). Higher
	/// is more prominent.
	/// </param>
	/// <param name="startOffsetSeconds">
	/// When the range <i>started</i>, in <b>seconds relative to now</b>. A range that began ten seconds ago
	/// and has just finished is <c>-10f</c> here with a <paramref name="durationSeconds"/> of <c>10f</c>.
	/// </param>
	/// <param name="durationSeconds">
	/// How long the range lasted, in <b>seconds</b> — a length, not an end offset. The SDK declares a ceiling
	/// of 600 seconds (<c>k_flMaxTimelineEventDuration</c>); Valve does not document whether a longer
	/// duration is clamped, rejected or accepted. Passing <c>0f</c> makes this equivalent to
	/// <see cref="AddInstantaneousTimelineEvent"/>.
	/// </param>
	/// <param name="possibleClip">
	/// Whether Steam should offer this range as a suggested clip.
	/// <see cref="TimelineEventClipPriority.Featured"/> is offered ahead of
	/// <see cref="TimelineEventClipPriority.Standard"/>; <see cref="TimelineEventClipPriority.None"/>
	/// suppresses the suggestion.
	/// </param>
	/// <returns>
	/// A handle valid only for the lifetime of this process, usable with
	/// <see cref="DoesEventRecordingExist"/>, <see cref="OpenOverlayToTimelineEvent"/> and
	/// <see cref="RemoveTimelineEvent"/>. Unlike <see cref="StartRangeTimelineEvent"/>, this event is already
	/// closed and needs no matching end call.
	/// </returns>
	public static TimelineEventHandle AddRangeTimelineEvent( string title, string description, string icon,
		uint priority, float startOffsetSeconds, float durationSeconds, TimelineEventClipPriority possibleClip )
	{
		return Internal.AddRangeTimelineEvent( title, description, icon, priority, startOffsetSeconds, durationSeconds,
			possibleClip );
	}

	/// <summary>
	/// Use this to mark the start of an event on the Timeline that takes some amount of time to complete. The duration of the event is determined by a matching call
	/// to <see cref="EndRangeTimelineEvent"/>. If the game wants to cancel an event in progress, they can do that with a call to <see cref="RemoveTimelineEvent"/>.
	/// </summary>
	/// <param name="title">Short marker label, localized into <see cref="SteamUtils.SteamUILanguage"/> by you. Can be revised later with <see cref="UpdateRangeTimelineEvent"/>.</param>
	/// <param name="description">Longer detail shown alongside the title, also localized by you and also revisable.</param>
	/// <param name="icon">
	/// A partner-site icon name for your app, or a Valve built-in name beginning with <c>steam_</c>.
	/// </param>
	/// <param name="priority">
	/// Display prominence relative to your other markers, 0 to 1000 (<c>k_unMaxTimelinePriority</c>).
	/// </param>
	/// <param name="startOffsetSeconds">
	/// Where the range begins, in <b>seconds relative to now</b>. <c>0f</c> starts it at this instant;
	/// negative values back-date the start, which is what you want when the triggering condition was already
	/// true for a while before you detected it.
	/// </param>
	/// <param name="possibleClip">
	/// Whether Steam should offer the finished range as a suggested clip. Can be revised before the event is
	/// ended via <see cref="UpdateRangeTimelineEvent"/>.
	/// </param>
	/// <returns>
	/// The handle you must pass to <see cref="EndRangeTimelineEvent"/> (or
	/// <see cref="RemoveTimelineEvent"/> to abandon it). Valid only for this process.
	/// </returns>
	/// <remarks>
	/// <b>An unclosed event is destroyed, not auto-closed.</b> Valve is explicit that any timeline event still
	/// open when the game exits is discarded — so a crash, a hard quit, or simply forgetting the
	/// <see cref="EndRangeTimelineEvent"/> call loses the marker entirely rather than leaving a
	/// partial one. If a range might outlive an uncertain code path, prefer
	/// <see cref="AddRangeTimelineEvent"/> once the duration is known.
	/// </remarks>
	public static TimelineEventHandle StartRangeTimelineEvent( string title, string description, string icon,
		uint priority,
		float startOffsetSeconds, TimelineEventClipPriority possibleClip )
	{
		return Internal.StartRangeTimelineEvent( title, description, icon, priority, startOffsetSeconds, possibleClip );
	}

	/// <summary>
	/// Use this to update the details of an event that was started with <see cref="StartRangeTimelineEvent"/>.
	/// <para>
	/// Every field is overwritten on each call, so this is a whole-record replace rather than a patch — you
	/// must re-supply the values you want to keep.
	/// </para>
	/// </summary>
	/// <param name="handle">
	/// A handle from <see cref="StartRangeTimelineEvent"/> that has <b>not</b> yet been passed to
	/// <see cref="EndRangeTimelineEvent"/>. Valve only defines this call for events that are still open;
	/// the behaviour on an already-ended or removed handle is not documented and nothing is returned to tell
	/// you it was rejected.
	/// </param>
	/// <param name="title">Replacement label, localized into <see cref="SteamUtils.SteamUILanguage"/> by you.</param>
	/// <param name="description">Replacement detail text, also localized by you.</param>
	/// <param name="icon">Replacement partner-site or <c>steam_</c> built-in icon name.</param>
	/// <param name="priority">
	/// Replacement prominence, 0 to 1000. To leave the priority untouched, the SDK reserves the sentinel
	/// <c>k_unTimelinePriority_KeepCurrentValue</c> = <c>1000000</c> specifically for this call. That constant
	/// is <see langword="internal"/> in this binding, so pass the literal <c>1000000u</c>.
	/// </param>
	/// <param name="possibleClip">Replacement clip-suggestion setting.</param>
	public static void UpdateRangeTimelineEvent( TimelineEventHandle handle, string title, string description,
		string icon, uint priority, TimelineEventClipPriority possibleClip )
	{
		Internal.UpdateRangeTimelineEvent( handle, title, description, icon, priority, possibleClip );
	}

	/// <summary>
	/// Use this to identify the end of an event that was started with <see cref="StartRangeTimelineEvent"/>.
	/// <para>
	/// This is the call that actually publishes the range to the timeline UI — until it happens the event is
	/// provisional, and is discarded outright if the game exits first.
	/// </para>
	/// </summary>
	/// <param name="handle">The still-open handle returned by <see cref="StartRangeTimelineEvent"/>.</param>
	/// <param name="endOffsetSeconds">
	/// Where the range ends, in <b>seconds relative to now</b> — pass <c>0f</c> to end it at this instant, or
	/// a negative value to back-date the end.
	/// <para>
	/// Valve does not describe this parameter anywhere in <c>isteamtimeline.h</c>; the reading above is
	/// inferred from its name and from the documented meaning of the matching
	/// <c>flStartOffsetSeconds</c> on <see cref="StartRangeTimelineEvent"/>. In particular it is an
	/// <i>offset</i>, not a duration — <c>10f</c> here does not mean "ten seconds long".
	/// </para>
	/// </param>
	public static void EndRangeTimelineEvent( TimelineEventHandle handle, float endOffsetSeconds )
	{
		Internal.EndRangeTimelineEvent( handle, endOffsetSeconds );
	}

	/// <summary>
	/// Use this to remove a Timeline event that was previously added. This doubles as the cancel operation for
	/// a range event that is still in progress — an event abandoned this way is never shown to the user.
	/// </summary>
	/// <param name="handle">
	/// A handle from <see cref="AddInstantaneousTimelineEvent"/>, <see cref="AddRangeTimelineEvent"/> or
	/// <see cref="StartRangeTimelineEvent"/>. Valve requires it to originate from the <b>current game
	/// process</b>; handles do not survive a restart, so there is no way to delete an event from an earlier
	/// session. Nothing is returned, so a handle Steam does not recognise is indistinguishable from a
	/// successful removal.
	/// </param>
	public static void RemoveTimelineEvent( TimelineEventHandle handle )
	{
		Internal.RemoveTimelineEvent( handle );
	}

	/// <summary>
	/// Use this to determine if video recordings exist for the specified event. This can be useful when the game needs to decide whether or not to show a control
	/// that will call <see cref="OpenOverlayToTimelineEvent"/>.
	/// </summary>
	/// <param name="handle">
	/// An event handle from this process. Because handles do not survive a restart, this can only ever ask
	/// about events the running session created.
	/// </param>
	/// <returns>
	/// <see langword="true"/> if Steam holds video covering the event.
	/// <para>
	/// <b>The failure mode is not distinguishable.</b> This binding collapses "no recording exists" and "the
	/// underlying Steam API call failed or never completed" into the same <see langword="false"/>. If you need
	/// to tell those apart, this wrapper cannot do it.
	/// </para>
	/// </returns>
	/// <remarks>
	/// This is a Steam call-result, so the returned task only ever completes while callbacks are being
	/// pumped — that is, while something is calling <see cref="SteamClient.RunCallbacks"/> on a regular
	/// tick. Awaiting it on a thread that never pumps will hang.
	/// </remarks>
	public static async Task<bool> DoesEventRecordingExist( TimelineEventHandle handle )
	{
		var result = await Internal.DoesEventRecordingExist( handle );
		return result?.RecordingExists ?? false;
	}

	/// <summary>
	/// Use this to start a game phase. Game phases allow the user to navigate their background recordings and clips. Exactly what a game phase means will vary game
	/// to game, but the game phase should be a section of gameplay that is usually between 10 minutes and a few hours in length, and should be the main way a user
	/// would think to divide up the game. These are presented to the user in a UI that shows the date the game was played, with one row per game slice. Game phases
	/// should be used to mark sections of gameplay that the user might be interested in watching.
	/// <para>
	/// Valve's examples: a single match in a multiplayer PvP game, a chapter of a story campaign, one run in
	/// a roguelike.
	/// </para>
	/// </summary>
	/// <remarks>
	/// <para>
	/// <b>Phases do not nest, and starting one implicitly ends the last.</b> Valve defines exactly three ways
	/// a phase stops: the game exits, <see cref="EndGamePhase"/> is called, or this method is called again to
	/// begin a new phase. There is no phase handle and no way to have two open at once.
	/// </para>
	/// <para>
	/// <b>A phase left open at exit survives; a timeline event left open at exit does not.</b> The two have
	/// opposite failure behaviour in Valve's own wording, so forgetting <see cref="EndGamePhase"/> is far less
	/// damaging than forgetting <see cref="EndRangeTimelineEvent"/>.
	/// </para>
	/// <para>
	/// <see cref="SetGamePhaseId"/>, <see cref="AddGamePhaseTag"/> and
	/// <see cref="SetGamePhaseAttribute"/> all apply to whichever phase is currently open, so they only make
	/// sense between this call and the matching end.
	/// </para>
	/// </remarks>
	public static void StartGamePhase()
	{
		Internal.StartGamePhase();
	}

	/// <summary>
	/// Use this to end a game phase that was started with <see cref="StartGamePhase"/>. After this, tags and
	/// attributes have no phase to attach to until the next <see cref="StartGamePhase"/>.
	/// </summary>
	/// <remarks>
	/// Optional if the next thing you do is start another phase, since that ends this one anyway. Valve does
	/// not document what happens if this is called with no phase open.
	/// </remarks>
	public static void EndGamePhase()
	{
		Internal.EndGamePhase();
	}

	/// <summary>
	/// The phase ID is used to let the game identify which phase it is referring to in calls to <see cref="DoesGamePhaseRecordingExist"/> or
	/// <see cref="OpenOverlayToGamePhase"/>. It may also be used to associated multiple phases with each other.
	/// </summary>
	/// <param name="phaseId">
	/// A game-provided persistent ID for a game phase. This could be a the match ID in a multiplayer game, a chapter name in a single player game, the ID of a character, etc.
	/// <para>
	/// Unlike <see cref="TimelineEventHandle"/>, this identifier is <b>persistent across runs of the game</b>
	/// — that is the whole point of it, and it is what makes
	/// <see cref="DoesGamePhaseRecordingExist"/> able to ask about sessions the player finished days ago.
	/// Choose something you can regenerate later, not a runtime pointer or a random per-session value.
	/// </para>
	/// <para>
	/// The SDK caps phase IDs at 64 bytes (<c>k_cchMaxPhaseIDLength</c>), and Steam returns them out of a
	/// fixed 64-byte buffer, so budget for at most 63 bytes of UTF-8 plus a terminator. Valve does not
	/// document whether a longer ID is truncated or rejected, and nothing is returned here to tell you.
	/// </para>
	/// </param>
	/// <remarks>
	/// Applies to the phase currently open (see <see cref="StartGamePhase"/>). Valve does not document the
	/// effect of calling it with no phase open.
	/// </remarks>
	public static void SetGamePhaseId( string phaseId )
	{
		Internal.SetGamePhaseID( phaseId );
	}

	/// <summary>
	/// Use this to determine if video recordings exist for the specified game phase. This can be useful when the game needs to decide whether or not to show a control that will call <see cref="OpenOverlayToGamePhase"/>.
	/// </summary>
	/// <param name="phaseId">
	/// An ID previously supplied via <see cref="SetGamePhaseId"/>. Because phase IDs persist across runs, this
	/// may name a phase from an earlier session — for example, to light up a "watch this match again" button
	/// in a match-history screen.
	/// </param>
	/// <returns>
	/// A <see cref="GamePhaseRecordingInfo"/> carrying how much footage, how many clips and how many
	/// screenshots Steam holds for the phase, or <see langword="null"/> if the underlying Steam call failed or
	/// did not complete.
	/// <para>
	/// <b>There is no explicit "exists" flag.</b> Despite the method name and Valve's own
	/// <c>...RecordingExists_t</c> struct name, the payload is only counters. Valve does not document what
	/// those counters contain when nothing was recorded; treating an all-zero
	/// <see cref="GamePhaseRecordingInfo.RecordingMs"/>, <see cref="GamePhaseRecordingInfo.ClipCount"/> and
	/// <see cref="GamePhaseRecordingInfo.ScreenshotCount"/> as "nothing to show" is this binding's inference,
	/// not documented behaviour.
	/// </para>
	/// <para>
	/// A <see langword="null"/> result is likewise ambiguous: it means the call result was unavailable, which
	/// this binding cannot separate from an unrecognised phase ID.
	/// </para>
	/// </returns>
	/// <remarks>
	/// A Steam call-result: the task only completes while callbacks are being pumped via
	/// <see cref="SteamClient.RunCallbacks"/>.
	/// </remarks>
	public static async Task<GamePhaseRecordingInfo?> DoesGamePhaseRecordingExist( string phaseId )
	{
		var result = await Internal.DoesGamePhaseRecordingExist( phaseId );
		if ( !result.HasValue )
		{
			return null;
		}

		var info = result.Value;
		return new GamePhaseRecordingInfo
		{
			PhaseId = info.PhaseIDUTF8(),
			RecordingMs = info.RecordingMS,
			LongestClipMs = info.LongestClipMS,
			ClipCount = info.ClipCount,
			ScreenshotCount = info.ScreenshotCount,
		};
	}

	/// <summary>
	/// Use this to add a game phase tag. Phase tags represent data with a well defined set of options, which could be data such as match resolution, hero played, game mode, etc. Tags can have an icon
	/// in addition to a text name. Multiple tags within the same group may be added per phase and all will be remembered. For example, this may be called multiple times for a "Bosses Defeated" group,
	/// with different names and icons for each boss defeated during the phase, all of which will be shown to the user.
	/// <para>
	/// Tags <b>accumulate</b>. That is the difference from <see cref="SetGamePhaseAttribute"/>, where each
	/// write replaces the previous value.
	/// </para>
	/// </summary>
	/// <param name="tagName">
	/// The tag's display name, localized into <see cref="SteamUtils.SteamUILanguage"/> by you — "Vault
	/// Guardian", "Ranked", "Sniper".
	/// </param>
	/// <param name="icon">
	/// Icon shown next to the tag name in the UI: a name your app uploaded through the Steamworks partner
	/// site, or a Valve built-in beginning with <c>steam_</c>.
	/// </param>
	/// <param name="tagGroup">
	/// The localized name of the group this tag belongs to — "Bosses Defeated", "Game Mode". Adding several
	/// tags to the same group is expected and all of them are kept. Valve's parameter notes state that a tag
	/// with no group specified cannot be filtered on by users; that note appears in the timeline-event tag
	/// documentation and Valve does not repeat it for phase tags, so treat it as likely rather than certain
	/// here.
	/// </param>
	/// <param name="priority">
	/// Orders this tag against your other tags and attributes in the UI, 0 to 1000
	/// (<c>k_unMaxTimelinePriority</c>). Higher values get more prominent positioning; where space is limited,
	/// lower-priority items may be hidden altogether.
	/// </param>
	/// <remarks>
	/// Applies to the phase currently open. Valve documents tags and attributes as things added "while a phase
	/// is still happening", so calling this outside a phase has no documented effect and returns nothing to
	/// signal that.
	/// </remarks>
	public static void AddGamePhaseTag( string tagName, string icon, string tagGroup, uint priority )
	{
		Internal.AddGamePhaseTag( tagName, icon, tagGroup, priority );
	}

	/// <summary>
	/// Use this to add a game phase attribute. Phase attributes represent generic text fields that can be updated throughout the duration of the phase. They are meant to be used for phase metadata
	/// that is not part of a well defined set of options. For example, a KDA attribute that starts with the value "0/0/0" and updates as the phase progresses, or something like a played-entered character
	/// name. Attributes can be set as many times as the game likes with SetGamePhaseAttribute, and only the last value will be shown to the user.
	/// <para>
	/// Attributes <b>overwrite</b>; <see cref="AddGamePhaseTag"/> accumulates. Pick this one when the field
	/// has a current value rather than a history.
	/// </para>
	/// </summary>
	/// <param name="attributeGroup">
	/// The localized name of the attribute — this doubles as the key. Writing the same group again replaces
	/// the earlier value rather than adding a second row, so keep it stable for the whole phase ("KDA",
	/// "Character Name", "Difficulty").
	/// </param>
	/// <param name="attributeValue">
	/// The localized current value, as free text. Call this as often as the value changes; only the final
	/// value is ever shown to the user, so intermediate writes cost nothing but the call.
	/// </param>
	/// <param name="priority">
	/// Orders this attribute against your other tags and attributes in the UI, 0 to 1000
	/// (<c>k_unMaxTimelinePriority</c>). Higher is more prominent; lower-priority items may be hidden where
	/// space is limited.
	/// </param>
	/// <remarks>
	/// Applies to the phase currently open (see <see cref="StartGamePhase"/>).
	/// </remarks>
	public static void SetGamePhaseAttribute( string attributeGroup, string attributeValue, uint priority )
	{
		Internal.SetGamePhaseAttribute( attributeGroup, attributeValue, priority );
	}

	/// <summary>
	/// Changes the color of the timeline bar. See <see cref="TimelineGameMode"/> for how to use each value.
	/// <para>
	/// This is the cheapest useful thing you can do with this API: colouring menus and loading differently
	/// from live gameplay means a user scrubbing a recording can skip the dead air without any events or
	/// phases being set up at all.
	/// </para>
	/// </summary>
	/// <param name="gameMode">
	/// The band to paint from now until the next call. Valve's names describe a multiplayer flow but are
	/// meant to be mapped onto whatever your game does — their own single-player example uses
	/// <see cref="TimelineGameMode.Menus"/> for buying items in town,
	/// <see cref="TimelineGameMode.Staging"/> while a dungeon loads and
	/// <see cref="TimelineGameMode.Playing"/> for fighting inside it, with
	/// <see cref="TimelineGameMode.LoadingScreen"/> for load screens.
	/// <para>
	/// <see cref="TimelineGameMode.Invalid"/> and <see cref="TimelineGameMode.Max"/> are sentinels rather
	/// than states — <c>Max</c> is defined in the SDK as one past the last valid value. Valve does not
	/// document what passing either one does.
	/// </para>
	/// </param>
	public static void SetTimelineGameMode( TimelineGameMode gameMode )
	{
		Internal.SetTimelineGameMode( gameMode );
	}

	/// <summary>
	/// Opens the Steam overlay to the section of the timeline represented by the game phase.
	/// </summary>
	/// <param name="phaseId">
	/// An ID the game previously supplied through <see cref="SetGamePhaseId"/>. Since phase IDs persist
	/// across runs, this can target a phase from an earlier session.
	/// </param>
	/// <remarks>
	/// Returns nothing and raises no callback, so an ID the Steam client does not recognise fails silently.
	/// Valve's own advice is to gate the control that triggers this on
	/// <see cref="DoesGamePhaseRecordingExist"/> rather than showing a button that may do nothing.
	/// </remarks>
	public static void OpenOverlayToGamePhase( string phaseId )
	{
		Internal.OpenOverlayToGamePhase( phaseId );
	}

	/// <summary>
	/// Opens the Steam overlay to the section of the timeline represented by the timeline event. This event must be in the current game session, since <see cref="TimelineEventHandle"/> values are not
	/// valid for future runs of the game.
	/// </summary>
	/// <param name="handle">
	/// A handle returned by <see cref="AddInstantaneousTimelineEvent"/>,
	/// <see cref="AddRangeTimelineEvent"/>, or <see cref="StartRangeTimelineEvent"/> after it has been closed
	/// with <see cref="EndRangeTimelineEvent"/> — and created by the running process. If you need a jump-to
	/// control that survives a restart, use game phases and <see cref="OpenOverlayToGamePhase"/> instead,
	/// because phase IDs persist and event handles do not.
	/// </param>
	/// <remarks>
	/// Fire-and-forget: a stale or unrecognised handle produces no error. Gate the control on
	/// <see cref="DoesEventRecordingExist"/>, which is exactly what Valve suggests it is for.
	/// </remarks>
	public static void OpenOverlayToTimelineEvent( TimelineEventHandle handle )
	{
		Internal.OpenOverlayToTimelineEvent( handle );
	}
}
