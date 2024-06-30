using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Steamworks.Data;


namespace Steamworks
{
	internal unsafe class ISteamTimeline : SteamInterface
	{
		
		internal ISteamTimeline( bool IsGameServer )
		{
			SetupInterface( IsGameServer );
		}
		
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_SteamTimeline_v001", CallingConvention = Platform.CC)]
		internal static extern IntPtr SteamAPI_SteamTimeline_v001();
		public override IntPtr GetUserInterfacePointer() => SteamAPI_SteamTimeline_v001();
		
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamTimeline_SetTimelineStateDescription", CallingConvention = Platform.CC)]
		internal static extern void _SetTimelineStateDescription( IntPtr self, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchDescription, float flTimeDelta );
		
		#endregion
		internal void SetTimelineStateDescription( [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchDescription, float flTimeDelta )
		{
			_SetTimelineStateDescription( Self, pchDescription, flTimeDelta );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamTimeline_ClearTimelineStateDescription", CallingConvention = Platform.CC)]
		internal static extern void _ClearTimelineStateDescription( IntPtr self, float flTimeDelta );
		
		#endregion
		internal void ClearTimelineStateDescription( float flTimeDelta )
		{
			_ClearTimelineStateDescription( Self, flTimeDelta );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamTimeline_AddTimelineEvent", CallingConvention = Platform.CC)]
		internal static extern void _AddTimelineEvent( IntPtr self, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchIcon, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchTitle, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchDescription, uint unPriority, float flStartOffsetSeconds, float flDurationSeconds, TimelineEventClipPriority ePossibleClip );
		
		#endregion
		internal void AddTimelineEvent( [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchIcon, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchTitle, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchDescription, uint unPriority, float flStartOffsetSeconds, float flDurationSeconds, TimelineEventClipPriority ePossibleClip )
		{
			_AddTimelineEvent( Self, pchIcon, pchTitle, pchDescription, unPriority, flStartOffsetSeconds, flDurationSeconds, ePossibleClip );
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamTimeline_SetTimelineGameMode", CallingConvention = Platform.CC)]
		internal static extern void _SetTimelineGameMode( IntPtr self, TimelineGameMode eMode );
		
		#endregion
		internal void SetTimelineGameMode( TimelineGameMode eMode )
		{
			_SetTimelineGameMode( Self, eMode );
		}
		
	}
}
