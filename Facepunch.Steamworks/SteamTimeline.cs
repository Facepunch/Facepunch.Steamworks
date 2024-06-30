namespace Steamworks;

public class SteamTimeline : SteamClientClass<SteamTimeline>
{
	/// Sets a description (B) for the current game state in the timeline. These help the user to find specific
	/// moments in the timeline when saving clips. Setting a new state description replaces any previous
	/// 	description.
	/// 	Examples could include:
	///			Where the user is in the world in a single player game
	///			Which round is happening in a multiplayer game
	/// 		The current score for a sports game
	/// <param name="pchDescription">A localized string in the language returned by SteamUtils()->GetSteamUILanguage()</param>
	/// <param name="flTimeDelta">The time offset in seconds to apply to this state change. Negative times indicate an event that happened in the past.</param>
	public static void SetTimelineStateDescription( string pchDescription, float flTimeDelta )
	{
		ISteamTimeline._SetTimelineStateDescription( ISteamTimeline.SteamAPI_SteamTimeline_v001(), pchDescription, flTimeDelta );
	}

	/// <summary>
	/// Clears the previous set game state in the timeline.
	/// </summary>
	/// <param name="flTimeDelta">	The time offset in seconds to apply to this state change. Negative times indicate an event that happened in the past.</param>
	public static void ClearTimelineStateDescription( float flTimeDelta )
	{
		ISteamTimeline._ClearTimelineStateDescription( ISteamTimeline.SteamAPI_SteamTimeline_v001(), flTimeDelta );
	}

	///  Use this to mark an event (A) on the Timeline. The event can be instantaneous or take some amount of time to complete, depending on the value passed in flDurationSeconds.
	///  
	///  	Examples could include:
	/// 			a boss battle
	///  		a cut scene
	/// 			a large team fight
	/// 			picking up a new weapon or ammo
	///  		scoring a goal
	///  
	///  The game can nominate an event as being suitable for a clip by passing k_ETimelineEventClipPriority_Standard or k_ETimelineEventClipPriority_Featured to ePossibleClip. Players can make clips of their own at any point, but this lets the game suggest some options to Steam to make that process easier for players.
	///  <param name="pchIcon">The name of the icon to show at the timeline at this point. This can be one of the icons uploaded through the Steamworks partner Site for your title, or one of the provided icons that start with steam_. The Steam Timelines overview includes a list of available icons.</param>
	///  <param name="pchTitle">Title-provided localized string in the language returned by SteamUtils()->GetSteamUILanguage().</param>
	///  <param name="pchDescription">	Title-provided localized string in the language returned by SteamUtils()->GetSteamUILanguage().</param>
	///  <param name="unPriority">Provide the priority to use when the UI is deciding which icons to display in crowded parts of the timeline. Events with larger priority values will be displayed more prominently than events with smaller priority values. This value must be between 0 and k_unMaxTimelinePriority.</param>
	///  <param name="flStartOffsetSeconds">The time offset in seconds to apply to the start of the event. Negative times indicate an event that happened in the past. One use of this parameter is to handle events whose significance is not clear until after the fact. For instance if the player starts a damage over time effect on another player, which kills them 3.5 seconds later, the game could pass -3.5 as the start offset and cause the event to appear in the timeline where the effect started.</param>
	///  <param name="flDurationSeconds">The duration of the event, in seconds. Pass 0 for instantaneous events.</param>
	///  <param name="ePossibleClip">Allows the game to describe events that should be suggested to the user as possible video clips.</param>
	public static void AddTimelineEvent( string pchIcon, string pchTitle, string pchDescription, uint unPriority, float flStartOffsetSeconds, float flDurationSeconds, TimelineEventClipPriority ePossibleClip )
	{
		ISteamTimeline._AddTimelineEvent( ISteamTimeline.SteamAPI_SteamTimeline_v001(), pchIcon, pchTitle, pchDescription, unPriority, flStartOffsetSeconds, flDurationSeconds, ePossibleClip );
	}
	
	/// <summary>
	/// Changes the color of the timeline bar (C). See ETimelineGameMode for how to use each value.
	/// </summary>
	/// <param name="eMode">The mode that the game is in.</param>
	public static void SetTimelineGameMode( TimelineGameMode eMode )
	{
		ISteamTimeline._SetTimelineGameMode( ISteamTimeline.SteamAPI_SteamTimeline_v001(), eMode );
	}
}
