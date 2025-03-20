using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Steamworks.Data;


namespace Steamworks
{
	internal unsafe partial class ISteamTimeline : SteamInterface
	{
		public const string Version = "STEAMTIMELINE_INTERFACE_V004";
		
		internal ISteamTimeline( bool IsGameServer )
		{
			SetupInterface( IsGameServer );
		}
		
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_SteamTimeline_v004", CallingConvention = Platform.CC)]
		internal static extern IntPtr SteamAPI_SteamTimeline_v004();
		public override IntPtr GetUserInterfacePointer() => SteamAPI_SteamTimeline_v004();
		
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamTimeline_SetTimelineTooltip", CallingConvention = Platform.CC)]
		private static extern void _SetTimelineTooltip( IntPtr self, IntPtr pchDescription, float flTimeDelta );
		
		#endregion
		internal void SetTimelineTooltip( string pchDescription, float flTimeDelta )
		{
			using var str__pchDescription = new Utf8StringToNative( pchDescription );
			_SetTimelineTooltip( Self, str__pchDescription.Pointer, flTimeDelta );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamTimeline_ClearTimelineTooltip", CallingConvention = Platform.CC)]
		private static extern void _ClearTimelineTooltip( IntPtr self, float flTimeDelta );
		
		#endregion
		internal void ClearTimelineTooltip( float flTimeDelta )
		{
			_ClearTimelineTooltip( Self, flTimeDelta );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamTimeline_SetTimelineGameMode", CallingConvention = Platform.CC)]
		private static extern void _SetTimelineGameMode( IntPtr self, TimelineGameMode eMode );
		
		#endregion
		internal void SetTimelineGameMode( TimelineGameMode eMode )
		{
			_SetTimelineGameMode( Self, eMode );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamTimeline_AddInstantaneousTimelineEvent", CallingConvention = Platform.CC)]
		private static extern TimelineEventHandle _AddInstantaneousTimelineEvent( IntPtr self, IntPtr pchTitle, IntPtr pchDescription, IntPtr pchIcon, uint unIconPriority, float flStartOffsetSeconds, TimelineEventClipPriority ePossibleClip );
		
		#endregion
		internal TimelineEventHandle AddInstantaneousTimelineEvent( string pchTitle, string pchDescription, string pchIcon, uint unIconPriority, float flStartOffsetSeconds, TimelineEventClipPriority ePossibleClip )
		{
			using var str__pchTitle = new Utf8StringToNative( pchTitle );
			using var str__pchDescription = new Utf8StringToNative( pchDescription );
			using var str__pchIcon = new Utf8StringToNative( pchIcon );
			var returnValue = _AddInstantaneousTimelineEvent( Self, str__pchTitle.Pointer, str__pchDescription.Pointer, str__pchIcon.Pointer, unIconPriority, flStartOffsetSeconds, ePossibleClip );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamTimeline_AddRangeTimelineEvent", CallingConvention = Platform.CC)]
		private static extern TimelineEventHandle _AddRangeTimelineEvent( IntPtr self, IntPtr pchTitle, IntPtr pchDescription, IntPtr pchIcon, uint unIconPriority, float flStartOffsetSeconds, float flDuration, TimelineEventClipPriority ePossibleClip );
		
		#endregion
		internal TimelineEventHandle AddRangeTimelineEvent( string pchTitle, string pchDescription, string pchIcon, uint unIconPriority, float flStartOffsetSeconds, float flDuration, TimelineEventClipPriority ePossibleClip )
		{
			using var str__pchTitle = new Utf8StringToNative( pchTitle );
			using var str__pchDescription = new Utf8StringToNative( pchDescription );
			using var str__pchIcon = new Utf8StringToNative( pchIcon );
			var returnValue = _AddRangeTimelineEvent( Self, str__pchTitle.Pointer, str__pchDescription.Pointer, str__pchIcon.Pointer, unIconPriority, flStartOffsetSeconds, flDuration, ePossibleClip );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamTimeline_StartRangeTimelineEvent", CallingConvention = Platform.CC)]
		private static extern TimelineEventHandle _StartRangeTimelineEvent( IntPtr self, IntPtr pchTitle, IntPtr pchDescription, IntPtr pchIcon, uint unPriority, float flStartOffsetSeconds, TimelineEventClipPriority ePossibleClip );
		
		#endregion
		internal TimelineEventHandle StartRangeTimelineEvent( string pchTitle, string pchDescription, string pchIcon, uint unPriority, float flStartOffsetSeconds, TimelineEventClipPriority ePossibleClip )
		{
			using var str__pchTitle = new Utf8StringToNative( pchTitle );
			using var str__pchDescription = new Utf8StringToNative( pchDescription );
			using var str__pchIcon = new Utf8StringToNative( pchIcon );
			var returnValue = _StartRangeTimelineEvent( Self, str__pchTitle.Pointer, str__pchDescription.Pointer, str__pchIcon.Pointer, unPriority, flStartOffsetSeconds, ePossibleClip );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamTimeline_UpdateRangeTimelineEvent", CallingConvention = Platform.CC)]
		private static extern void _UpdateRangeTimelineEvent( IntPtr self, TimelineEventHandle ulEvent, IntPtr pchTitle, IntPtr pchDescription, IntPtr pchIcon, uint unPriority, TimelineEventClipPriority ePossibleClip );
		
		#endregion
		internal void UpdateRangeTimelineEvent( TimelineEventHandle ulEvent, string pchTitle, string pchDescription, string pchIcon, uint unPriority, TimelineEventClipPriority ePossibleClip )
		{
			using var str__pchTitle = new Utf8StringToNative( pchTitle );
			using var str__pchDescription = new Utf8StringToNative( pchDescription );
			using var str__pchIcon = new Utf8StringToNative( pchIcon );
			_UpdateRangeTimelineEvent( Self, ulEvent, str__pchTitle.Pointer, str__pchDescription.Pointer, str__pchIcon.Pointer, unPriority, ePossibleClip );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamTimeline_EndRangeTimelineEvent", CallingConvention = Platform.CC)]
		private static extern void _EndRangeTimelineEvent( IntPtr self, TimelineEventHandle ulEvent, float flEndOffsetSeconds );
		
		#endregion
		internal void EndRangeTimelineEvent( TimelineEventHandle ulEvent, float flEndOffsetSeconds )
		{
			_EndRangeTimelineEvent( Self, ulEvent, flEndOffsetSeconds );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamTimeline_RemoveTimelineEvent", CallingConvention = Platform.CC)]
		private static extern void _RemoveTimelineEvent( IntPtr self, TimelineEventHandle ulEvent );
		
		#endregion
		internal void RemoveTimelineEvent( TimelineEventHandle ulEvent )
		{
			_RemoveTimelineEvent( Self, ulEvent );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamTimeline_DoesEventRecordingExist", CallingConvention = Platform.CC)]
		private static extern SteamAPICall_t _DoesEventRecordingExist( IntPtr self, TimelineEventHandle ulEvent );
		
		#endregion
		internal CallResult<SteamTimelineEventRecordingExists_t> DoesEventRecordingExist( TimelineEventHandle ulEvent )
		{
			var returnValue = _DoesEventRecordingExist( Self, ulEvent );
			return new CallResult<SteamTimelineEventRecordingExists_t>( returnValue, IsServer );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamTimeline_StartGamePhase", CallingConvention = Platform.CC)]
		private static extern void _StartGamePhase( IntPtr self );
		
		#endregion
		internal void StartGamePhase()
		{
			_StartGamePhase( Self );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamTimeline_EndGamePhase", CallingConvention = Platform.CC)]
		private static extern void _EndGamePhase( IntPtr self );
		
		#endregion
		internal void EndGamePhase()
		{
			_EndGamePhase( Self );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamTimeline_SetGamePhaseID", CallingConvention = Platform.CC)]
		private static extern void _SetGamePhaseID( IntPtr self, IntPtr pchPhaseID );
		
		#endregion
		internal void SetGamePhaseID( string pchPhaseID )
		{
			using var str__pchPhaseID = new Utf8StringToNative( pchPhaseID );
			_SetGamePhaseID( Self, str__pchPhaseID.Pointer );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamTimeline_DoesGamePhaseRecordingExist", CallingConvention = Platform.CC)]
		private static extern SteamAPICall_t _DoesGamePhaseRecordingExist( IntPtr self, IntPtr pchPhaseID );
		
		#endregion
		internal CallResult<SteamTimelineGamePhaseRecordingExists_t> DoesGamePhaseRecordingExist( string pchPhaseID )
		{
			using var str__pchPhaseID = new Utf8StringToNative( pchPhaseID );
			var returnValue = _DoesGamePhaseRecordingExist( Self, str__pchPhaseID.Pointer );
			return new CallResult<SteamTimelineGamePhaseRecordingExists_t>( returnValue, IsServer );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamTimeline_AddGamePhaseTag", CallingConvention = Platform.CC)]
		private static extern void _AddGamePhaseTag( IntPtr self, IntPtr pchTagName, IntPtr pchTagIcon, IntPtr pchTagGroup, uint unPriority );
		
		#endregion
		internal void AddGamePhaseTag( string pchTagName, string pchTagIcon, string pchTagGroup, uint unPriority )
		{
			using var str__pchTagName = new Utf8StringToNative( pchTagName );
			using var str__pchTagIcon = new Utf8StringToNative( pchTagIcon );
			using var str__pchTagGroup = new Utf8StringToNative( pchTagGroup );
			_AddGamePhaseTag( Self, str__pchTagName.Pointer, str__pchTagIcon.Pointer, str__pchTagGroup.Pointer, unPriority );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamTimeline_SetGamePhaseAttribute", CallingConvention = Platform.CC)]
		private static extern void _SetGamePhaseAttribute( IntPtr self, IntPtr pchAttributeGroup, IntPtr pchAttributeValue, uint unPriority );
		
		#endregion
		internal void SetGamePhaseAttribute( string pchAttributeGroup, string pchAttributeValue, uint unPriority )
		{
			using var str__pchAttributeGroup = new Utf8StringToNative( pchAttributeGroup );
			using var str__pchAttributeValue = new Utf8StringToNative( pchAttributeValue );
			_SetGamePhaseAttribute( Self, str__pchAttributeGroup.Pointer, str__pchAttributeValue.Pointer, unPriority );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamTimeline_OpenOverlayToGamePhase", CallingConvention = Platform.CC)]
		private static extern void _OpenOverlayToGamePhase( IntPtr self, IntPtr pchPhaseID );
		
		#endregion
		internal void OpenOverlayToGamePhase( string pchPhaseID )
		{
			using var str__pchPhaseID = new Utf8StringToNative( pchPhaseID );
			_OpenOverlayToGamePhase( Self, str__pchPhaseID.Pointer );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamTimeline_OpenOverlayToTimelineEvent", CallingConvention = Platform.CC)]
		private static extern void _OpenOverlayToTimelineEvent( IntPtr self, TimelineEventHandle ulEvent );
		
		#endregion
		internal void OpenOverlayToTimelineEvent( TimelineEventHandle ulEvent )
		{
			_OpenOverlayToTimelineEvent( Self, ulEvent );
		}
		
	}
}
