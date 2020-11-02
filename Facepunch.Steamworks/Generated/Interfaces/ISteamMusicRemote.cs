using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Steamworks.Data;


namespace Steamworks
{
	internal class ISteamMusicRemote : SteamInterface
	{
		
		internal ISteamMusicRemote( bool IsGameServer )
		{
			SetupInterface( IsGameServer );
		}
		
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_SteamMusicRemote_v001", CallingConvention = Platform.CC)]
		internal static extern IntPtr SteamAPI_SteamMusicRemote_v001();
		public override IntPtr GetUserInterfacePointer() => SteamAPI_SteamMusicRemote_v001();
		
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamMusicRemote_RegisterSteamMusicRemote", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _RegisterSteamMusicRemote( IntPtr self, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchName );
		
		#endregion
		internal bool RegisterSteamMusicRemote( [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchName )
		{
			var returnValue = _RegisterSteamMusicRemote( Self, pchName );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamMusicRemote_DeregisterSteamMusicRemote", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _DeregisterSteamMusicRemote( IntPtr self );
		
		#endregion
		internal bool DeregisterSteamMusicRemote()
		{
			var returnValue = _DeregisterSteamMusicRemote( Self );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamMusicRemote_BIsCurrentMusicRemote", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _BIsCurrentMusicRemote( IntPtr self );
		
		#endregion
		internal bool BIsCurrentMusicRemote()
		{
			var returnValue = _BIsCurrentMusicRemote( Self );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamMusicRemote_BActivationSuccess", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _BActivationSuccess( IntPtr self, [MarshalAs( UnmanagedType.U1 )] bool bValue );
		
		#endregion
		internal bool BActivationSuccess( [MarshalAs( UnmanagedType.U1 )] bool bValue )
		{
			var returnValue = _BActivationSuccess( Self, bValue );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamMusicRemote_SetDisplayName", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _SetDisplayName( IntPtr self, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchDisplayName );
		
		#endregion
		internal bool SetDisplayName( [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchDisplayName )
		{
			var returnValue = _SetDisplayName( Self, pchDisplayName );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamMusicRemote_SetPNGIcon_64x64", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _SetPNGIcon_64x64( IntPtr self, IntPtr pvBuffer, uint cbBufferLength );
		
		#endregion
		internal bool SetPNGIcon_64x64( IntPtr pvBuffer, uint cbBufferLength )
		{
			var returnValue = _SetPNGIcon_64x64( Self, pvBuffer, cbBufferLength );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamMusicRemote_EnablePlayPrevious", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _EnablePlayPrevious( IntPtr self, [MarshalAs( UnmanagedType.U1 )] bool bValue );
		
		#endregion
		internal bool EnablePlayPrevious( [MarshalAs( UnmanagedType.U1 )] bool bValue )
		{
			var returnValue = _EnablePlayPrevious( Self, bValue );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamMusicRemote_EnablePlayNext", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _EnablePlayNext( IntPtr self, [MarshalAs( UnmanagedType.U1 )] bool bValue );
		
		#endregion
		internal bool EnablePlayNext( [MarshalAs( UnmanagedType.U1 )] bool bValue )
		{
			var returnValue = _EnablePlayNext( Self, bValue );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamMusicRemote_EnableShuffled", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _EnableShuffled( IntPtr self, [MarshalAs( UnmanagedType.U1 )] bool bValue );
		
		#endregion
		internal bool EnableShuffled( [MarshalAs( UnmanagedType.U1 )] bool bValue )
		{
			var returnValue = _EnableShuffled( Self, bValue );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamMusicRemote_EnableLooped", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _EnableLooped( IntPtr self, [MarshalAs( UnmanagedType.U1 )] bool bValue );
		
		#endregion
		internal bool EnableLooped( [MarshalAs( UnmanagedType.U1 )] bool bValue )
		{
			var returnValue = _EnableLooped( Self, bValue );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamMusicRemote_EnableQueue", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _EnableQueue( IntPtr self, [MarshalAs( UnmanagedType.U1 )] bool bValue );
		
		#endregion
		internal bool EnableQueue( [MarshalAs( UnmanagedType.U1 )] bool bValue )
		{
			var returnValue = _EnableQueue( Self, bValue );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamMusicRemote_EnablePlaylists", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _EnablePlaylists( IntPtr self, [MarshalAs( UnmanagedType.U1 )] bool bValue );
		
		#endregion
		internal bool EnablePlaylists( [MarshalAs( UnmanagedType.U1 )] bool bValue )
		{
			var returnValue = _EnablePlaylists( Self, bValue );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamMusicRemote_UpdatePlaybackStatus", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _UpdatePlaybackStatus( IntPtr self, MusicStatus nStatus );
		
		#endregion
		internal bool UpdatePlaybackStatus( MusicStatus nStatus )
		{
			var returnValue = _UpdatePlaybackStatus( Self, nStatus );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamMusicRemote_UpdateShuffled", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _UpdateShuffled( IntPtr self, [MarshalAs( UnmanagedType.U1 )] bool bValue );
		
		#endregion
		internal bool UpdateShuffled( [MarshalAs( UnmanagedType.U1 )] bool bValue )
		{
			var returnValue = _UpdateShuffled( Self, bValue );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamMusicRemote_UpdateLooped", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _UpdateLooped( IntPtr self, [MarshalAs( UnmanagedType.U1 )] bool bValue );
		
		#endregion
		internal bool UpdateLooped( [MarshalAs( UnmanagedType.U1 )] bool bValue )
		{
			var returnValue = _UpdateLooped( Self, bValue );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamMusicRemote_UpdateVolume", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _UpdateVolume( IntPtr self, float flValue );
		
		#endregion
		internal bool UpdateVolume( float flValue )
		{
			var returnValue = _UpdateVolume( Self, flValue );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamMusicRemote_CurrentEntryWillChange", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _CurrentEntryWillChange( IntPtr self );
		
		#endregion
		internal bool CurrentEntryWillChange()
		{
			var returnValue = _CurrentEntryWillChange( Self );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamMusicRemote_CurrentEntryIsAvailable", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _CurrentEntryIsAvailable( IntPtr self, [MarshalAs( UnmanagedType.U1 )] bool bAvailable );
		
		#endregion
		internal bool CurrentEntryIsAvailable( [MarshalAs( UnmanagedType.U1 )] bool bAvailable )
		{
			var returnValue = _CurrentEntryIsAvailable( Self, bAvailable );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamMusicRemote_UpdateCurrentEntryText", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _UpdateCurrentEntryText( IntPtr self, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchText );
		
		#endregion
		internal bool UpdateCurrentEntryText( [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchText )
		{
			var returnValue = _UpdateCurrentEntryText( Self, pchText );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamMusicRemote_UpdateCurrentEntryElapsedSeconds", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _UpdateCurrentEntryElapsedSeconds( IntPtr self, int nValue );
		
		#endregion
		internal bool UpdateCurrentEntryElapsedSeconds( int nValue )
		{
			var returnValue = _UpdateCurrentEntryElapsedSeconds( Self, nValue );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamMusicRemote_UpdateCurrentEntryCoverArt", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _UpdateCurrentEntryCoverArt( IntPtr self, IntPtr pvBuffer, uint cbBufferLength );
		
		#endregion
		internal bool UpdateCurrentEntryCoverArt( IntPtr pvBuffer, uint cbBufferLength )
		{
			var returnValue = _UpdateCurrentEntryCoverArt( Self, pvBuffer, cbBufferLength );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamMusicRemote_CurrentEntryDidChange", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _CurrentEntryDidChange( IntPtr self );
		
		#endregion
		internal bool CurrentEntryDidChange()
		{
			var returnValue = _CurrentEntryDidChange( Self );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamMusicRemote_QueueWillChange", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _QueueWillChange( IntPtr self );
		
		#endregion
		internal bool QueueWillChange()
		{
			var returnValue = _QueueWillChange( Self );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamMusicRemote_ResetQueueEntries", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _ResetQueueEntries( IntPtr self );
		
		#endregion
		internal bool ResetQueueEntries()
		{
			var returnValue = _ResetQueueEntries( Self );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamMusicRemote_SetQueueEntry", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _SetQueueEntry( IntPtr self, int nID, int nPosition, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchEntryText );
		
		#endregion
		internal bool SetQueueEntry( int nID, int nPosition, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchEntryText )
		{
			var returnValue = _SetQueueEntry( Self, nID, nPosition, pchEntryText );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamMusicRemote_SetCurrentQueueEntry", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _SetCurrentQueueEntry( IntPtr self, int nID );
		
		#endregion
		internal bool SetCurrentQueueEntry( int nID )
		{
			var returnValue = _SetCurrentQueueEntry( Self, nID );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamMusicRemote_QueueDidChange", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _QueueDidChange( IntPtr self );
		
		#endregion
		internal bool QueueDidChange()
		{
			var returnValue = _QueueDidChange( Self );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamMusicRemote_PlaylistWillChange", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _PlaylistWillChange( IntPtr self );
		
		#endregion
		internal bool PlaylistWillChange()
		{
			var returnValue = _PlaylistWillChange( Self );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamMusicRemote_ResetPlaylistEntries", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _ResetPlaylistEntries( IntPtr self );
		
		#endregion
		internal bool ResetPlaylistEntries()
		{
			var returnValue = _ResetPlaylistEntries( Self );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamMusicRemote_SetPlaylistEntry", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _SetPlaylistEntry( IntPtr self, int nID, int nPosition, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchEntryText );
		
		#endregion
		internal bool SetPlaylistEntry( int nID, int nPosition, [MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] string pchEntryText )
		{
			var returnValue = _SetPlaylistEntry( Self, nID, nPosition, pchEntryText );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamMusicRemote_SetCurrentPlaylistEntry", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _SetCurrentPlaylistEntry( IntPtr self, int nID );
		
		#endregion
		internal bool SetCurrentPlaylistEntry( int nID )
		{
			var returnValue = _SetCurrentPlaylistEntry( Self, nID );
			return returnValue;
		}
		
		#region FunctionMeta
		[DllImport( Platform.LibraryName, EntryPoint = "SteamAPI_ISteamMusicRemote_PlaylistDidChange", CallingConvention = Platform.CC)]
		[return: MarshalAs( UnmanagedType.I1 )]
		private static extern bool _PlaylistDidChange( IntPtr self );
		
		#endregion
		internal bool PlaylistDidChange()
		{
			var returnValue = _PlaylistDidChange( Self );
			return returnValue;
		}
		
	}
}
