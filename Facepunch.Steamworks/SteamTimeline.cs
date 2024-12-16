using System;
using System.Threading.Tasks;
using Steamworks.Data;

namespace Steamworks;

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
	/// </summary>
	public static void SetTimelineTooltip( string description, float timeOffsetSeconds )
	{
		Internal.SetTimelineTooltip( description, timeOffsetSeconds );
	}

	/// <summary>
	/// Clears the previous set game state in the timeline.
	/// </summary>
	public static void ClearTimelineTooltip( float timeOffsetSeconds )
	{
		Internal.ClearTimelineTooltip( timeOffsetSeconds );
	}

	/// <summary>
	/// Use this to mark an event on the Timeline. This event will be instantaneous. (See <see cref="AddRangeTimelineEvent"/> to add events that happened over time.)
	/// </summary>
	public static TimelineEventHandle AddInstantaneousTimelineEvent( string title, string description, string icon,
		uint priority, float startOffsetSeconds, TimelineEventClipPriority possibleClip )
	{
		return Internal.AddInstantaneousTimelineEvent( title, description, icon, priority, startOffsetSeconds,
			possibleClip );
	}

	/// <summary>
	/// Use this to mark an event on the Timeline that takes some amount of time to complete.
	/// </summary>
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
	public static TimelineEventHandle StartRangeTimelineEvent( string title, string description, string icon,
		uint priority,
		float startOffsetSeconds, TimelineEventClipPriority possibleClip )
	{
		return Internal.StartRangeTimelineEvent( title, description, icon, priority, startOffsetSeconds, possibleClip );
	}

	/// <summary>
	/// Use this to update the details of an event that was started with <see cref="StartRangeTimelineEvent"/>.
	/// </summary>
	public static void UpdateRangeTimelineEvent( TimelineEventHandle handle, string title, string description,
		string icon, uint priority, TimelineEventClipPriority possibleClip )
	{
		Internal.UpdateRangeTimelineEvent( handle, title, description, icon, priority, possibleClip );
	}

	/// <summary>
	/// Use this to identify the end of an event that was started with <see cref="StartRangeTimelineEvent"/>.
	/// </summary>
	public static void EndRangeTimelineEvent( TimelineEventHandle handle, float endOffsetSeconds )
	{
		Internal.EndRangeTimelineEvent( handle, endOffsetSeconds );
	}

	/// <summary>
	/// Use this to remove a Timeline event that was previously added.
	/// </summary>
	public static void RemoveTimelineEvent( TimelineEventHandle handle )
	{
		Internal.RemoveTimelineEvent( handle );
	}

	/// <summary>
	/// Use this to determine if video recordings exist for the specified event. This can be useful when the game needs to decide whether or not to show a control
	/// that will call <see cref="OpenOverlayToTimelineEvent"/>.
	/// </summary>
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
	/// </summary>
	public static void StartGamePhase()
	{
		Internal.StartGamePhase();
	}

	/// <summary>
	/// Use this to end a game phase that was started with <see cref="StartGamePhase"/>.
	/// </summary>
	public static void EndGamePhase()
	{
		Internal.EndGamePhase();
	}

	/// <summary>
	/// The phase ID is used to let the game identify which phase it is referring to in calls to <see cref="DoesGamePhaseRecordingExist"/> or
	/// <see cref="OpenOverlayToGamePhase"/>. It may also be used to associated multiple phases with each other.
	/// </summary>
	/// <param name="phaseId">A game-provided persistent ID for a game phase. This could be a the match ID in a multiplayer game, a chapter name in a single player game, the ID of a character, etc.</param>
	public static void SetGamePhaseId( string phaseId )
	{
		Internal.SetGamePhaseID( phaseId );
	}

	/// <summary>
	/// Use this to determine if video recordings exist for the specified game phase. This can be useful when the game needs to decide whether or not to show a control that will call <see cref="OpenOverlayToGamePhase"/>.
	/// </summary>
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
	/// </summary>
	public static void AddGamePhaseTag( string tagName, string icon, string tagGroup, uint priority )
	{
		Internal.AddGamePhaseTag( tagName, icon, tagGroup, priority );
	}

	/// <summary>
	/// Use this to add a game phase attribute. Phase attributes represent generic text fields that can be updated throughout the duration of the phase. They are meant to be used for phase metadata
	/// that is not part of a well defined set of options. For example, a KDA attribute that starts with the value "0/0/0" and updates as the phase progresses, or something like a played-entered character
	/// name. Attributes can be set as many times as the game likes with SetGamePhaseAttribute, and only the last value will be shown to the user.
	/// </summary>
	public static void SetGamePhaseAttribute( string attributeGroup, string attributeValue, uint priority )
	{
		Internal.SetGamePhaseAttribute( attributeGroup, attributeValue, priority );
	}

	/// <summary>
	/// Changes the color of the timeline bar. See <see cref="TimelineGameMode"/> for how to use each value.
	/// </summary>
	public static void SetTimelineGameMode( TimelineGameMode gameMode )
	{
		Internal.SetTimelineGameMode( gameMode );
	}

	/// <summary>
	/// Opens the Steam overlay to the section of the timeline represented by the game phase.
	/// </summary>
	public static void OpenOverlayToGamePhase( string phaseId )
	{
		Internal.OpenOverlayToGamePhase( phaseId );
	}

	/// <summary>
	/// Opens the Steam overlay to the section of the timeline represented by the timeline event. This event must be in the current game session, since <see cref="TimelineEventHandle"/> values are not
	/// valid for future runs of the game.
	/// </summary>
	public static void OpenOverlayToTimelineEvent( TimelineEventHandle handle )
	{
		Internal.OpenOverlayToTimelineEvent( handle );
	}
}
