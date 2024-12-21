using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Steamworks.Data;


namespace Steamworks
{
	internal unsafe partial class ISteamTimeline : SteamInterface
	{
		
		internal ISteamTimeline( bool IsGameServer )
		{
			SetupInterface( IsGameServer );
		}
		
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_SteamTimeline_v004", CallingConvention = Platform.CC)]
		internal static extern IntPtr SteamAPI_SteamTimeline_v004();
		public override IntPtr GetUserInterfacePointer() => SteamAPI_SteamTimeline_v004();
		
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamTimeline_SetTimelineTooltip", CallingConvention = Platform.CC)]
		private static extern void _SetTimelineTooltip( IntPtr self, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchDescription, float flTimeDelta );
		
		#endregion
		internal void SetTimelineTooltip( [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchDescription, float flTimeDelta )
		{
			_SetTimelineTooltip( Self, pchDescription, flTimeDelta );
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
		private static extern TimelineEventHandle_t _AddInstantaneousTimelineEvent( IntPtr self, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchTitle, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchDescription, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchIcon, uint unIconPriority, float flStartOffsetSeconds, TimelineEventClipPriority ePossibleClip );
		
		#endregion
		internal TimelineEventHandle_t AddInstantaneousTimelineEvent( [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchTitle, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchDescription, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchIcon, uint unIconPriority, float flStartOffsetSeconds, TimelineEventClipPriority ePossibleClip )
		{
			var returnValue = _AddInstantaneousTimelineEvent( Self, pchTitle, pchDescription, pchIcon, unIconPriority, flStartOffsetSeconds, ePossibleClip );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamTimeline_AddRangeTimelineEvent", CallingConvention = Platform.CC)]
		private static extern TimelineEventHandle_t _AddRangeTimelineEvent( IntPtr self, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchTitle, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchDescription, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchIcon, uint unIconPriority, float flStartOffsetSeconds, float flDuration, TimelineEventClipPriority ePossibleClip );
		
		#endregion
		internal TimelineEventHandle_t AddRangeTimelineEvent( [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchTitle, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchDescription, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchIcon, uint unIconPriority, float flStartOffsetSeconds, float flDuration, TimelineEventClipPriority ePossibleClip )
		{
			var returnValue = _AddRangeTimelineEvent( Self, pchTitle, pchDescription, pchIcon, unIconPriority, flStartOffsetSeconds, flDuration, ePossibleClip );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamTimeline_StartRangeTimelineEvent", CallingConvention = Platform.CC)]
		private static extern TimelineEventHandle_t _StartRangeTimelineEvent( IntPtr self, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchTitle, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchDescription, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchIcon, uint unPriority, float flStartOffsetSeconds, TimelineEventClipPriority ePossibleClip );
		
		#endregion
		internal TimelineEventHandle_t StartRangeTimelineEvent( [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchTitle, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchDescription, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchIcon, uint unPriority, float flStartOffsetSeconds, TimelineEventClipPriority ePossibleClip )
		{
			var returnValue = _StartRangeTimelineEvent( Self, pchTitle, pchDescription, pchIcon, unPriority, flStartOffsetSeconds, ePossibleClip );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamTimeline_UpdateRangeTimelineEvent", CallingConvention = Platform.CC)]
		private static extern void _UpdateRangeTimelineEvent( IntPtr self, TimelineEventHandle_t ulEvent, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchTitle, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchDescription, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchIcon, uint unPriority, TimelineEventClipPriority ePossibleClip );
		
		#endregion
		internal void UpdateRangeTimelineEvent( TimelineEventHandle_t ulEvent, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchTitle, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchDescription, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchIcon, uint unPriority, TimelineEventClipPriority ePossibleClip )
		{
			_UpdateRangeTimelineEvent( Self, ulEvent, pchTitle, pchDescription, pchIcon, unPriority, ePossibleClip );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamTimeline_EndRangeTimelineEvent", CallingConvention = Platform.CC)]
		private static extern void _EndRangeTimelineEvent( IntPtr self, TimelineEventHandle_t ulEvent, float flEndOffsetSeconds );
		
		#endregion
		internal void EndRangeTimelineEvent( TimelineEventHandle_t ulEvent, float flEndOffsetSeconds )
		{
			_EndRangeTimelineEvent( Self, ulEvent, flEndOffsetSeconds );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamTimeline_RemoveTimelineEvent", CallingConvention = Platform.CC)]
		private static extern void _RemoveTimelineEvent( IntPtr self, TimelineEventHandle_t ulEvent );
		
		#endregion
		internal void RemoveTimelineEvent( TimelineEventHandle_t ulEvent )
		{
			_RemoveTimelineEvent( Self, ulEvent );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamTimeline_DoesEventRecordingExist", CallingConvention = Platform.CC)]
		private static extern SteamAPICall_t _DoesEventRecordingExist( IntPtr self, TimelineEventHandle_t ulEvent );
		
		#endregion
		internal CallResult<SteamTimelineEventRecordingExists_t> DoesEventRecordingExist( TimelineEventHandle_t ulEvent )
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
		private static extern void _SetGamePhaseID( IntPtr self, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchPhaseID );
		
		#endregion
		internal void SetGamePhaseID( [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchPhaseID )
		{
			_SetGamePhaseID( Self, pchPhaseID );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamTimeline_DoesGamePhaseRecordingExist", CallingConvention = Platform.CC)]
		private static extern SteamAPICall_t _DoesGamePhaseRecordingExist( IntPtr self, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchPhaseID );
		
		#endregion
		internal CallResult<SteamTimelineGamePhaseRecordingExists_t> DoesGamePhaseRecordingExist( [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchPhaseID )
		{
			var returnValue = _DoesGamePhaseRecordingExist( Self, pchPhaseID );
			return new CallResult<SteamTimelineGamePhaseRecordingExists_t>( returnValue, IsServer );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamTimeline_AddGamePhaseTag", CallingConvention = Platform.CC)]
		private static extern void _AddGamePhaseTag( IntPtr self, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchTagName, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchTagIcon, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchTagGroup, uint unPriority );
		
		#endregion
		internal void AddGamePhaseTag( [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchTagName, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchTagIcon, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchTagGroup, uint unPriority )
		{
			_AddGamePhaseTag( Self, pchTagName, pchTagIcon, pchTagGroup, unPriority );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamTimeline_SetGamePhaseAttribute", CallingConvention = Platform.CC)]
		private static extern void _SetGamePhaseAttribute( IntPtr self, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchAttributeGroup, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchAttributeValue, uint unPriority );
		
		#endregion
		internal void SetGamePhaseAttribute( [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchAttributeGroup, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchAttributeValue, uint unPriority )
		{
			_SetGamePhaseAttribute( Self, pchAttributeGroup, pchAttributeValue, unPriority );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamTimeline_OpenOverlayToGamePhase", CallingConvention = Platform.CC)]
		private static extern void _OpenOverlayToGamePhase( IntPtr self, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchPhaseID );
		
		#endregion
		internal void OpenOverlayToGamePhase( [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchPhaseID )
		{
			_OpenOverlayToGamePhase( Self, pchPhaseID );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamTimeline_OpenOverlayToTimelineEvent", CallingConvention = Platform.CC)]
		private static extern void _OpenOverlayToTimelineEvent( IntPtr self, TimelineEventHandle_t ulEvent );
		
		#endregion
		internal void OpenOverlayToTimelineEvent( TimelineEventHandle_t ulEvent )
		{
			_OpenOverlayToTimelineEvent( Self, ulEvent );
		}
		
	}
}
