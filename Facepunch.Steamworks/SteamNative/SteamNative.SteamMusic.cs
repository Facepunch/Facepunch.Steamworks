using System;
using System.Runtime.InteropServices;

namespace SteamNative
{
	public unsafe class SteamMusic
	{
		internal IntPtr _ptr;
		
		public SteamMusic( IntPtr pointer )
		{
			_ptr = pointer;
		}
		
		
		// bool
		public bool BIsEnabled()
		{
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamMusic.BIsEnabled( _ptr );
			else return Platform.Win64.ISteamMusic.BIsEnabled( _ptr );
		}
		
		// bool
		public bool BIsPlaying()
		{
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamMusic.BIsPlaying( _ptr );
			else return Platform.Win64.ISteamMusic.BIsPlaying( _ptr );
		}
		
		// AudioPlayback_Status
		public AudioPlayback_Status GetPlaybackStatus()
		{
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamMusic.GetPlaybackStatus( _ptr );
			else return Platform.Win64.ISteamMusic.GetPlaybackStatus( _ptr );
		}
		
		// float
		public float GetVolume()
		{
			if ( Platform.IsWindows32 ) return Platform.Win32.ISteamMusic.GetVolume( _ptr );
			else return Platform.Win64.ISteamMusic.GetVolume( _ptr );
		}
		
		// void
		public void Pause()
		{
			if ( Platform.IsWindows32 ) Platform.Win32.ISteamMusic.Pause( _ptr );
			else Platform.Win64.ISteamMusic.Pause( _ptr );
		}
		
		// void
		public void Play()
		{
			if ( Platform.IsWindows32 ) Platform.Win32.ISteamMusic.Play( _ptr );
			else Platform.Win64.ISteamMusic.Play( _ptr );
		}
		
		// void
		public void PlayNext()
		{
			if ( Platform.IsWindows32 ) Platform.Win32.ISteamMusic.PlayNext( _ptr );
			else Platform.Win64.ISteamMusic.PlayNext( _ptr );
		}
		
		// void
		public void PlayPrevious()
		{
			if ( Platform.IsWindows32 ) Platform.Win32.ISteamMusic.PlayPrevious( _ptr );
			else Platform.Win64.ISteamMusic.PlayPrevious( _ptr );
		}
		
		// void
		public void SetVolume( float flVolume /*float*/ )
		{
			if ( Platform.IsWindows32 ) Platform.Win32.ISteamMusic.SetVolume( _ptr, flVolume );
			else Platform.Win64.ISteamMusic.SetVolume( _ptr, flVolume );
		}
		
	}
}
