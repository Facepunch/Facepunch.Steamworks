using System;
using System.Runtime.InteropServices;

namespace SteamNative
{
	public unsafe class SteamMusicRemote
	{
		internal IntPtr _ptr;
		
		public SteamMusicRemote( IntPtr pointer )
		{
			_ptr = pointer;
		}
		
		
		// bool
		public bool BActivationSuccess( bool bValue /*bool*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamMusicRemote.BActivationSuccess( _ptr, bValue );
			else return Platform.Win64.ISteamMusicRemote.BActivationSuccess( _ptr, bValue );
		}
		
		// bool
		public bool BIsCurrentMusicRemote()
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamMusicRemote.BIsCurrentMusicRemote( _ptr );
			else return Platform.Win64.ISteamMusicRemote.BIsCurrentMusicRemote( _ptr );
		}
		
		// bool
		public bool CurrentEntryDidChange()
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamMusicRemote.CurrentEntryDidChange( _ptr );
			else return Platform.Win64.ISteamMusicRemote.CurrentEntryDidChange( _ptr );
		}
		
		// bool
		public bool CurrentEntryIsAvailable( bool bAvailable /*bool*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamMusicRemote.CurrentEntryIsAvailable( _ptr, bAvailable );
			else return Platform.Win64.ISteamMusicRemote.CurrentEntryIsAvailable( _ptr, bAvailable );
		}
		
		// bool
		public bool CurrentEntryWillChange()
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamMusicRemote.CurrentEntryWillChange( _ptr );
			else return Platform.Win64.ISteamMusicRemote.CurrentEntryWillChange( _ptr );
		}
		
		// bool
		public bool DeregisterSteamMusicRemote()
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamMusicRemote.DeregisterSteamMusicRemote( _ptr );
			else return Platform.Win64.ISteamMusicRemote.DeregisterSteamMusicRemote( _ptr );
		}
		
		// bool
		public bool EnableLooped( bool bValue /*bool*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamMusicRemote.EnableLooped( _ptr, bValue );
			else return Platform.Win64.ISteamMusicRemote.EnableLooped( _ptr, bValue );
		}
		
		// bool
		public bool EnablePlaylists( bool bValue /*bool*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamMusicRemote.EnablePlaylists( _ptr, bValue );
			else return Platform.Win64.ISteamMusicRemote.EnablePlaylists( _ptr, bValue );
		}
		
		// bool
		public bool EnablePlayNext( bool bValue /*bool*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamMusicRemote.EnablePlayNext( _ptr, bValue );
			else return Platform.Win64.ISteamMusicRemote.EnablePlayNext( _ptr, bValue );
		}
		
		// bool
		public bool EnablePlayPrevious( bool bValue /*bool*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamMusicRemote.EnablePlayPrevious( _ptr, bValue );
			else return Platform.Win64.ISteamMusicRemote.EnablePlayPrevious( _ptr, bValue );
		}
		
		// bool
		public bool EnableQueue( bool bValue /*bool*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamMusicRemote.EnableQueue( _ptr, bValue );
			else return Platform.Win64.ISteamMusicRemote.EnableQueue( _ptr, bValue );
		}
		
		// bool
		public bool EnableShuffled( bool bValue /*bool*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamMusicRemote.EnableShuffled( _ptr, bValue );
			else return Platform.Win64.ISteamMusicRemote.EnableShuffled( _ptr, bValue );
		}
		
		// bool
		public bool PlaylistDidChange()
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamMusicRemote.PlaylistDidChange( _ptr );
			else return Platform.Win64.ISteamMusicRemote.PlaylistDidChange( _ptr );
		}
		
		// bool
		public bool PlaylistWillChange()
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamMusicRemote.PlaylistWillChange( _ptr );
			else return Platform.Win64.ISteamMusicRemote.PlaylistWillChange( _ptr );
		}
		
		// bool
		public bool QueueDidChange()
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamMusicRemote.QueueDidChange( _ptr );
			else return Platform.Win64.ISteamMusicRemote.QueueDidChange( _ptr );
		}
		
		// bool
		public bool QueueWillChange()
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamMusicRemote.QueueWillChange( _ptr );
			else return Platform.Win64.ISteamMusicRemote.QueueWillChange( _ptr );
		}
		
		// bool
		public bool RegisterSteamMusicRemote( string pchName /*const char **/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamMusicRemote.RegisterSteamMusicRemote( _ptr, pchName );
			else return Platform.Win64.ISteamMusicRemote.RegisterSteamMusicRemote( _ptr, pchName );
		}
		
		// bool
		public bool ResetPlaylistEntries()
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamMusicRemote.ResetPlaylistEntries( _ptr );
			else return Platform.Win64.ISteamMusicRemote.ResetPlaylistEntries( _ptr );
		}
		
		// bool
		public bool ResetQueueEntries()
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamMusicRemote.ResetQueueEntries( _ptr );
			else return Platform.Win64.ISteamMusicRemote.ResetQueueEntries( _ptr );
		}
		
		// bool
		public bool SetCurrentPlaylistEntry( int nID /*int*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamMusicRemote.SetCurrentPlaylistEntry( _ptr, nID );
			else return Platform.Win64.ISteamMusicRemote.SetCurrentPlaylistEntry( _ptr, nID );
		}
		
		// bool
		public bool SetCurrentQueueEntry( int nID /*int*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamMusicRemote.SetCurrentQueueEntry( _ptr, nID );
			else return Platform.Win64.ISteamMusicRemote.SetCurrentQueueEntry( _ptr, nID );
		}
		
		// bool
		public bool SetDisplayName( string pchDisplayName /*const char **/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamMusicRemote.SetDisplayName( _ptr, pchDisplayName );
			else return Platform.Win64.ISteamMusicRemote.SetDisplayName( _ptr, pchDisplayName );
		}
		
		// bool
		public bool SetPlaylistEntry( int nID /*int*/, int nPosition /*int*/, string pchEntryText /*const char **/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamMusicRemote.SetPlaylistEntry( _ptr, nID, nPosition, pchEntryText );
			else return Platform.Win64.ISteamMusicRemote.SetPlaylistEntry( _ptr, nID, nPosition, pchEntryText );
		}
		
		// bool
		public bool SetPNGIcon_64x64( IntPtr pvBuffer /*void **/, uint cbBufferLength /*uint32*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamMusicRemote.SetPNGIcon_64x64( _ptr, (IntPtr) pvBuffer, cbBufferLength );
			else return Platform.Win64.ISteamMusicRemote.SetPNGIcon_64x64( _ptr, (IntPtr) pvBuffer, cbBufferLength );
		}
		
		// bool
		public bool SetQueueEntry( int nID /*int*/, int nPosition /*int*/, string pchEntryText /*const char **/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamMusicRemote.SetQueueEntry( _ptr, nID, nPosition, pchEntryText );
			else return Platform.Win64.ISteamMusicRemote.SetQueueEntry( _ptr, nID, nPosition, pchEntryText );
		}
		
		// bool
		public bool UpdateCurrentEntryCoverArt( IntPtr pvBuffer /*void **/, uint cbBufferLength /*uint32*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamMusicRemote.UpdateCurrentEntryCoverArt( _ptr, (IntPtr) pvBuffer, cbBufferLength );
			else return Platform.Win64.ISteamMusicRemote.UpdateCurrentEntryCoverArt( _ptr, (IntPtr) pvBuffer, cbBufferLength );
		}
		
		// bool
		public bool UpdateCurrentEntryElapsedSeconds( int nValue /*int*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamMusicRemote.UpdateCurrentEntryElapsedSeconds( _ptr, nValue );
			else return Platform.Win64.ISteamMusicRemote.UpdateCurrentEntryElapsedSeconds( _ptr, nValue );
		}
		
		// bool
		public bool UpdateCurrentEntryText( string pchText /*const char **/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamMusicRemote.UpdateCurrentEntryText( _ptr, pchText );
			else return Platform.Win64.ISteamMusicRemote.UpdateCurrentEntryText( _ptr, pchText );
		}
		
		// bool
		public bool UpdateLooped( bool bValue /*bool*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamMusicRemote.UpdateLooped( _ptr, bValue );
			else return Platform.Win64.ISteamMusicRemote.UpdateLooped( _ptr, bValue );
		}
		
		// bool
		public bool UpdatePlaybackStatus( AudioPlayback_Status nStatus /*AudioPlayback_Status*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamMusicRemote.UpdatePlaybackStatus( _ptr, nStatus );
			else return Platform.Win64.ISteamMusicRemote.UpdatePlaybackStatus( _ptr, nStatus );
		}
		
		// bool
		public bool UpdateShuffled( bool bValue /*bool*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamMusicRemote.UpdateShuffled( _ptr, bValue );
			else return Platform.Win64.ISteamMusicRemote.UpdateShuffled( _ptr, bValue );
		}
		
		// bool
		public bool UpdateVolume( float flValue /*float*/ )
		{
			if ( _ptr == IntPtr.Zero ) throw new System.Exception( "Internal pointer is null"); // 
			
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamMusicRemote.UpdateVolume( _ptr, flValue );
			else return Platform.Win64.ISteamMusicRemote.UpdateVolume( _ptr, flValue );
		}
		
	}
}
