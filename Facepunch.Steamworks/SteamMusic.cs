using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Steamworks.Data;

namespace Steamworks
{
	/// <summary>
	/// Functions to control music playback in the steam client.
	/// This gives games the opportunity to do things like pause the music or lower the volume, 
	/// when an important cut scene is shown, and start playing afterwards.
	/// Nothing uses Steam Music though so this can probably get fucked
	/// </summary>
	public static class SteamMusic
	{
		static ISteamMusic _internal;
		internal static ISteamMusic Internal
		{
			get
			{
				SteamClient.ValidCheck();

				if ( _internal == null )
				{
					_internal = new ISteamMusic();
					_internal.Init();
				}

				return _internal;
			}
		}
		internal static void Shutdown()
		{
			_internal = null;
		}

		internal static void InstallEvents()
		{
			PlaybackStatusHasChanged_t.Install( x => OnPlaybackChanged?.Invoke() );
			VolumeHasChanged_t.Install( x => OnVolumeChanged?.Invoke( x.NewVolume ) );
		}

		/// <summary>
		/// Playback status changed
		/// </summary>
		public static event Action OnPlaybackChanged;

		/// <summary>
		/// Volume changed, parameter is new volume
		/// </summary>
		public static event Action<float> OnVolumeChanged;

		/// <summary>
		/// Checks if Steam Music is enabled
		/// </summary>
		public static bool IsEnabled => Internal.BIsEnabled();

		/// <summary>
		/// true if a song is currently playing, paused, or queued up to play; otherwise false.
		/// </summary>
		public static bool IsPlaying => Internal.BIsPlaying();

		/// <summary>
		/// Gets the current status of the Steam Music player
		/// </summary>
		public static MusicStatus Status => Internal.GetPlaybackStatus();


		public static void Play() => Internal.Play();

		public static void Pause() => Internal.Pause();

		/// <summary>
		/// Have the Steam Music player play the previous song.
		/// </summary>
		public static void PlayPrevious() => Internal.PlayPrevious();

		/// <summary>
		/// Have the Steam Music player skip to the next song
		/// </summary>
		public static void PlayNext() => Internal.PlayNext();

		/// <summary>
		/// Gets/Sets the current volume of the Steam Music player
		/// </summary>
		public static float Volume
		{
			get => Internal.GetVolume();
			set => Internal.SetVolume( value );
		}
	}
}