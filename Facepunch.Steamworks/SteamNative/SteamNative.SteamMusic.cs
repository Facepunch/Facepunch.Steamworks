using System;
using System.Runtime.InteropServices;
using System.Linq;

namespace SteamNative
{
	internal unsafe class SteamMusic : IDisposable
	{
		//
		// Holds a platform specific implentation
		//
		internal Platform.Interface platform;
		internal Facepunch.Steamworks.BaseSteamworks steamworks;
		
		//
		// Constructor decides which implementation to use based on current platform
		//
		internal SteamMusic( Facepunch.Steamworks.BaseSteamworks steamworks, IntPtr pointer )
		{
			this.steamworks = steamworks;
			
			if ( Platform.IsWindows ) platform = new Platform.Windows( pointer );
			else if ( Platform.IsLinux ) platform = new Platform.Linux( pointer );
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
		public bool BIsEnabled()
		{
			return platform.ISteamMusic_BIsEnabled();
		}
		
		// bool
		public bool BIsPlaying()
		{
			return platform.ISteamMusic_BIsPlaying();
		}
		
		// AudioPlayback_Status
		public AudioPlayback_Status GetPlaybackStatus()
		{
			return platform.ISteamMusic_GetPlaybackStatus();
		}
		
		// float
		public float GetVolume()
		{
			return platform.ISteamMusic_GetVolume();
		}
		
		// void
		public void Pause()
		{
			platform.ISteamMusic_Pause();
		}
		
		// void
		public void Play()
		{
			platform.ISteamMusic_Play();
		}
		
		// void
		public void PlayNext()
		{
			platform.ISteamMusic_PlayNext();
		}
		
		// void
		public void PlayPrevious()
		{
			platform.ISteamMusic_PlayPrevious();
		}
		
		// void
		public void SetVolume( float flVolume /*float*/ )
		{
			platform.ISteamMusic_SetVolume( flVolume );
		}
		
	}
}
