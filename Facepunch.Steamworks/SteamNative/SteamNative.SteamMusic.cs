using System;
using System.Runtime.InteropServices;

namespace SteamNative
{
	public unsafe class SteamMusic
	{
		internal Platform.Interface _pi;
		
		public SteamMusic( IntPtr pointer )
		{
			if ( Platform.IsWindows64 ) _pi = new Platform.Win64( pointer );
			else if ( Platform.IsWindows32 ) _pi = new Platform.Win32( pointer );
			else if ( Platform.IsLinux32 ) _pi = new Platform.Linux32( pointer );
			else if ( Platform.IsLinux64 ) _pi = new Platform.Linux64( pointer );
			else if ( Platform.IsOsx ) _pi = new Platform.Mac( pointer );
		}
		
		public bool IsValid{ get{ return _pi != null && _pi.IsValid; } }
		
		// bool
		public bool BIsEnabled()
		{
			return _pi.ISteamMusic_BIsEnabled();
		}
		
		// bool
		public bool BIsPlaying()
		{
			return _pi.ISteamMusic_BIsPlaying();
		}
		
		// AudioPlayback_Status
		public AudioPlayback_Status GetPlaybackStatus()
		{
			return _pi.ISteamMusic_GetPlaybackStatus();
		}
		
		// float
		public float GetVolume()
		{
			return _pi.ISteamMusic_GetVolume();
		}
		
		// void
		public void Pause()
		{
			_pi.ISteamMusic_Pause();
		}
		
		// void
		public void Play()
		{
			_pi.ISteamMusic_Play();
		}
		
		// void
		public void PlayNext()
		{
			_pi.ISteamMusic_PlayNext();
		}
		
		// void
		public void PlayPrevious()
		{
			_pi.ISteamMusic_PlayPrevious();
		}
		
		// void
		public void SetVolume( float flVolume /*float*/ )
		{
			_pi.ISteamMusic_SetVolume( flVolume );
		}
		
	}
}
