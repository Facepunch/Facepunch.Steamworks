using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using SteamNative;

namespace Steamworks
{
	/// <summary>
	/// Undocumented Parental Settings
	/// </summary>
	public static class Music
	{
		static Internal.ISteamMusic _internal;
		internal static Internal.ISteamMusic music
		{
			get
			{
				if ( _internal == null )
					_internal = new Internal.ISteamMusic();

				return _internal;
			}
		}

		internal static void InstallEvents()
		{
			new Event<PlaybackStatusHasChanged_t>( x => OnPlaybackChanged?.Invoke() );
			new Event<VolumeHasChanged_t>( x => OnVolumeChanged?.Invoke( x.NewVolume ) );
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
		public static bool IsEnabled => music.BIsEnabled();

		/// <summary>
		/// true if a song is currently playing, paused, or queued up to play; otherwise false.
		/// </summary>
		public static bool IsPlaying => music.BIsPlaying();

		/// <summary>
		/// Gets the current status of the Steam Music player
		/// </summary>
		public static AudioPlayback_Status Status => music.GetPlaybackStatus();


		public static void Play() => music.Play();

		public static void Pause() => music.Pause();

		/// <summary>
		/// Have the Steam Music player play the previous song.
		/// </summary>
		public static void PlayPrevious() => music.PlayPrevious();

		/// <summary>
		/// Have the Steam Music player skip to the next song
		/// </summary>
		public static void PlayNext() => music.PlayNext();

		/// <summary>
		/// Gets/Sets the current volume of the Steam Music player
		/// </summary>
		public static float Volume
		{
			get => music.GetVolume();
			set => music.SetVolume( value );
		}
	}
}