using System;
using System.Runtime.InteropServices;
using System.Linq;

namespace SteamNative
{
	internal unsafe class SteamMusicRemote : IDisposable
	{
		//
		// Holds a platform specific implentation
		//
		internal Platform.Interface platform;
		internal Facepunch.Steamworks.BaseSteamworks steamworks;
		
		//
		// Constructor decides which implementation to use based on current platform
		//
		internal SteamMusicRemote( Facepunch.Steamworks.BaseSteamworks steamworks, IntPtr pointer )
		{
			this.steamworks = steamworks;
			
			if ( Platform.IsWindows64 ) platform = new Platform.Win64( pointer );
			else if ( Platform.IsWindows32 ) platform = new Platform.Win32( pointer );
			else if ( Platform.IsLinux32 ) platform = new Platform.Linux32( pointer );
			else if ( Platform.IsLinux64 ) platform = new Platform.Linux64( pointer );
			else if ( Platform.IsOsx ) platform = new Platform.Mac( pointer );
		}
		
		//
		// Class is invalid if we don't have a valid implementation
		//
		public bool IsValid{ get{ return platform != null && platform.IsValid; } }
		
		//
		// When shutting down clear all the internals to avoid accidental use
		//
		public virtual void Dispose()
		{
			 if ( platform != null )
			{
				platform.Dispose();
				platform = null;
			}
		}
		
		// bool
		public bool BActivationSuccess( bool bValue /*bool*/ )
		{
			return platform.ISteamMusicRemote_BActivationSuccess( bValue );
		}
		
		// bool
		public bool BIsCurrentMusicRemote()
		{
			return platform.ISteamMusicRemote_BIsCurrentMusicRemote();
		}
		
		// bool
		public bool CurrentEntryDidChange()
		{
			return platform.ISteamMusicRemote_CurrentEntryDidChange();
		}
		
		// bool
		public bool CurrentEntryIsAvailable( bool bAvailable /*bool*/ )
		{
			return platform.ISteamMusicRemote_CurrentEntryIsAvailable( bAvailable );
		}
		
		// bool
		public bool CurrentEntryWillChange()
		{
			return platform.ISteamMusicRemote_CurrentEntryWillChange();
		}
		
		// bool
		public bool DeregisterSteamMusicRemote()
		{
			return platform.ISteamMusicRemote_DeregisterSteamMusicRemote();
		}
		
		// bool
		public bool EnableLooped( bool bValue /*bool*/ )
		{
			return platform.ISteamMusicRemote_EnableLooped( bValue );
		}
		
		// bool
		public bool EnablePlaylists( bool bValue /*bool*/ )
		{
			return platform.ISteamMusicRemote_EnablePlaylists( bValue );
		}
		
		// bool
		public bool EnablePlayNext( bool bValue /*bool*/ )
		{
			return platform.ISteamMusicRemote_EnablePlayNext( bValue );
		}
		
		// bool
		public bool EnablePlayPrevious( bool bValue /*bool*/ )
		{
			return platform.ISteamMusicRemote_EnablePlayPrevious( bValue );
		}
		
		// bool
		public bool EnableQueue( bool bValue /*bool*/ )
		{
			return platform.ISteamMusicRemote_EnableQueue( bValue );
		}
		
		// bool
		public bool EnableShuffled( bool bValue /*bool*/ )
		{
			return platform.ISteamMusicRemote_EnableShuffled( bValue );
		}
		
		// bool
		public bool PlaylistDidChange()
		{
			return platform.ISteamMusicRemote_PlaylistDidChange();
		}
		
		// bool
		public bool PlaylistWillChange()
		{
			return platform.ISteamMusicRemote_PlaylistWillChange();
		}
		
		// bool
		public bool QueueDidChange()
		{
			return platform.ISteamMusicRemote_QueueDidChange();
		}
		
		// bool
		public bool QueueWillChange()
		{
			return platform.ISteamMusicRemote_QueueWillChange();
		}
		
		// bool
		public bool RegisterSteamMusicRemote( string pchName /*const char **/ )
		{
			return platform.ISteamMusicRemote_RegisterSteamMusicRemote( pchName );
		}
		
		// bool
		public bool ResetPlaylistEntries()
		{
			return platform.ISteamMusicRemote_ResetPlaylistEntries();
		}
		
		// bool
		public bool ResetQueueEntries()
		{
			return platform.ISteamMusicRemote_ResetQueueEntries();
		}
		
		// bool
		public bool SetCurrentPlaylistEntry( int nID /*int*/ )
		{
			return platform.ISteamMusicRemote_SetCurrentPlaylistEntry( nID );
		}
		
		// bool
		public bool SetCurrentQueueEntry( int nID /*int*/ )
		{
			return platform.ISteamMusicRemote_SetCurrentQueueEntry( nID );
		}
		
		// bool
		public bool SetDisplayName( string pchDisplayName /*const char **/ )
		{
			return platform.ISteamMusicRemote_SetDisplayName( pchDisplayName );
		}
		
		// bool
		public bool SetPlaylistEntry( int nID /*int*/, int nPosition /*int*/, string pchEntryText /*const char **/ )
		{
			return platform.ISteamMusicRemote_SetPlaylistEntry( nID, nPosition, pchEntryText );
		}
		
		// bool
		public bool SetPNGIcon_64x64( IntPtr pvBuffer /*void **/, uint cbBufferLength /*uint32*/ )
		{
			return platform.ISteamMusicRemote_SetPNGIcon_64x64( (IntPtr) pvBuffer, cbBufferLength );
		}
		
		// bool
		public bool SetQueueEntry( int nID /*int*/, int nPosition /*int*/, string pchEntryText /*const char **/ )
		{
			return platform.ISteamMusicRemote_SetQueueEntry( nID, nPosition, pchEntryText );
		}
		
		// bool
		public bool UpdateCurrentEntryCoverArt( IntPtr pvBuffer /*void **/, uint cbBufferLength /*uint32*/ )
		{
			return platform.ISteamMusicRemote_UpdateCurrentEntryCoverArt( (IntPtr) pvBuffer, cbBufferLength );
		}
		
		// bool
		public bool UpdateCurrentEntryElapsedSeconds( int nValue /*int*/ )
		{
			return platform.ISteamMusicRemote_UpdateCurrentEntryElapsedSeconds( nValue );
		}
		
		// bool
		public bool UpdateCurrentEntryText( string pchText /*const char **/ )
		{
			return platform.ISteamMusicRemote_UpdateCurrentEntryText( pchText );
		}
		
		// bool
		public bool UpdateLooped( bool bValue /*bool*/ )
		{
			return platform.ISteamMusicRemote_UpdateLooped( bValue );
		}
		
		// bool
		public bool UpdatePlaybackStatus( AudioPlayback_Status nStatus /*AudioPlayback_Status*/ )
		{
			return platform.ISteamMusicRemote_UpdatePlaybackStatus( nStatus );
		}
		
		// bool
		public bool UpdateShuffled( bool bValue /*bool*/ )
		{
			return platform.ISteamMusicRemote_UpdateShuffled( bValue );
		}
		
		// bool
		public bool UpdateVolume( float flValue /*float*/ )
		{
			return platform.ISteamMusicRemote_UpdateVolume( flValue );
		}
		
	}
}
