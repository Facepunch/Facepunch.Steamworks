using System;
using System.Runtime.InteropServices;

namespace SteamNative
{
	public unsafe class SteamMusicRemote : IDisposable
	{
		//
		// Holds a platform specific implentation
		//
		internal Platform.Interface _pi;
		
		//
		// Constructor decides which implementation to use based on current platform
		//
		public SteamMusicRemote( IntPtr pointer )
		{
			if ( Platform.IsWindows64 ) _pi = new Platform.Win64( pointer );
			else if ( Platform.IsWindows32 ) _pi = new Platform.Win32( pointer );
			else if ( Platform.IsLinux32 ) _pi = new Platform.Linux32( pointer );
			else if ( Platform.IsLinux64 ) _pi = new Platform.Linux64( pointer );
			else if ( Platform.IsOsx ) _pi = new Platform.Mac( pointer );
		}
		
		//
		// Class is invalid if we don't have a valid implementation
		//
		public bool IsValid{ get{ return _pi != null && _pi.IsValid; } }
		
		//
		// When shutting down clear all the internals to avoid accidental use
		//
		public virtual void Dispose()
		{
			 if ( _pi != null )
			{
				_pi.Dispose();
				_pi = null;
			}
		}
		
		// bool
		public bool BActivationSuccess( bool bValue /*bool*/ )
		{
			return _pi.ISteamMusicRemote_BActivationSuccess( bValue );
		}
		
		// bool
		public bool BIsCurrentMusicRemote()
		{
			return _pi.ISteamMusicRemote_BIsCurrentMusicRemote();
		}
		
		// bool
		public bool CurrentEntryDidChange()
		{
			return _pi.ISteamMusicRemote_CurrentEntryDidChange();
		}
		
		// bool
		public bool CurrentEntryIsAvailable( bool bAvailable /*bool*/ )
		{
			return _pi.ISteamMusicRemote_CurrentEntryIsAvailable( bAvailable );
		}
		
		// bool
		public bool CurrentEntryWillChange()
		{
			return _pi.ISteamMusicRemote_CurrentEntryWillChange();
		}
		
		// bool
		public bool DeregisterSteamMusicRemote()
		{
			return _pi.ISteamMusicRemote_DeregisterSteamMusicRemote();
		}
		
		// bool
		public bool EnableLooped( bool bValue /*bool*/ )
		{
			return _pi.ISteamMusicRemote_EnableLooped( bValue );
		}
		
		// bool
		public bool EnablePlaylists( bool bValue /*bool*/ )
		{
			return _pi.ISteamMusicRemote_EnablePlaylists( bValue );
		}
		
		// bool
		public bool EnablePlayNext( bool bValue /*bool*/ )
		{
			return _pi.ISteamMusicRemote_EnablePlayNext( bValue );
		}
		
		// bool
		public bool EnablePlayPrevious( bool bValue /*bool*/ )
		{
			return _pi.ISteamMusicRemote_EnablePlayPrevious( bValue );
		}
		
		// bool
		public bool EnableQueue( bool bValue /*bool*/ )
		{
			return _pi.ISteamMusicRemote_EnableQueue( bValue );
		}
		
		// bool
		public bool EnableShuffled( bool bValue /*bool*/ )
		{
			return _pi.ISteamMusicRemote_EnableShuffled( bValue );
		}
		
		// bool
		public bool PlaylistDidChange()
		{
			return _pi.ISteamMusicRemote_PlaylistDidChange();
		}
		
		// bool
		public bool PlaylistWillChange()
		{
			return _pi.ISteamMusicRemote_PlaylistWillChange();
		}
		
		// bool
		public bool QueueDidChange()
		{
			return _pi.ISteamMusicRemote_QueueDidChange();
		}
		
		// bool
		public bool QueueWillChange()
		{
			return _pi.ISteamMusicRemote_QueueWillChange();
		}
		
		// bool
		public bool RegisterSteamMusicRemote( string pchName /*const char **/ )
		{
			return _pi.ISteamMusicRemote_RegisterSteamMusicRemote( pchName );
		}
		
		// bool
		public bool ResetPlaylistEntries()
		{
			return _pi.ISteamMusicRemote_ResetPlaylistEntries();
		}
		
		// bool
		public bool ResetQueueEntries()
		{
			return _pi.ISteamMusicRemote_ResetQueueEntries();
		}
		
		// bool
		public bool SetCurrentPlaylistEntry( int nID /*int*/ )
		{
			return _pi.ISteamMusicRemote_SetCurrentPlaylistEntry( nID );
		}
		
		// bool
		public bool SetCurrentQueueEntry( int nID /*int*/ )
		{
			return _pi.ISteamMusicRemote_SetCurrentQueueEntry( nID );
		}
		
		// bool
		public bool SetDisplayName( string pchDisplayName /*const char **/ )
		{
			return _pi.ISteamMusicRemote_SetDisplayName( pchDisplayName );
		}
		
		// bool
		public bool SetPlaylistEntry( int nID /*int*/, int nPosition /*int*/, string pchEntryText /*const char **/ )
		{
			return _pi.ISteamMusicRemote_SetPlaylistEntry( nID, nPosition, pchEntryText );
		}
		
		// bool
		public bool SetPNGIcon_64x64( IntPtr pvBuffer /*void **/, uint cbBufferLength /*uint32*/ )
		{
			return _pi.ISteamMusicRemote_SetPNGIcon_64x64( (IntPtr) pvBuffer, cbBufferLength );
		}
		
		// bool
		public bool SetQueueEntry( int nID /*int*/, int nPosition /*int*/, string pchEntryText /*const char **/ )
		{
			return _pi.ISteamMusicRemote_SetQueueEntry( nID, nPosition, pchEntryText );
		}
		
		// bool
		public bool UpdateCurrentEntryCoverArt( IntPtr pvBuffer /*void **/, uint cbBufferLength /*uint32*/ )
		{
			return _pi.ISteamMusicRemote_UpdateCurrentEntryCoverArt( (IntPtr) pvBuffer, cbBufferLength );
		}
		
		// bool
		public bool UpdateCurrentEntryElapsedSeconds( int nValue /*int*/ )
		{
			return _pi.ISteamMusicRemote_UpdateCurrentEntryElapsedSeconds( nValue );
		}
		
		// bool
		public bool UpdateCurrentEntryText( string pchText /*const char **/ )
		{
			return _pi.ISteamMusicRemote_UpdateCurrentEntryText( pchText );
		}
		
		// bool
		public bool UpdateLooped( bool bValue /*bool*/ )
		{
			return _pi.ISteamMusicRemote_UpdateLooped( bValue );
		}
		
		// bool
		public bool UpdatePlaybackStatus( AudioPlayback_Status nStatus /*AudioPlayback_Status*/ )
		{
			return _pi.ISteamMusicRemote_UpdatePlaybackStatus( nStatus );
		}
		
		// bool
		public bool UpdateShuffled( bool bValue /*bool*/ )
		{
			return _pi.ISteamMusicRemote_UpdateShuffled( bValue );
		}
		
		// bool
		public bool UpdateVolume( float flValue /*float*/ )
		{
			return _pi.ISteamMusicRemote_UpdateVolume( flValue );
		}
		
	}
}
