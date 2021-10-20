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
	public class SteamMusic : SteamClientClass<SteamMusic>
	{
		internal static ISteamMusic Internal => Interface as ISteamMusic;

		internal override void InitializeInterface( bool server )
		{
			SetInterface( server, new ISteamMusic( server ) );

			InstallEvents();
		}

		internal static void InstallEvents()
		{
			Dispatch.Install<PlaybackStatusHasChanged_t>( x => OnPlaybackChanged?.Invoke() );
			Dispatch.Install<VolumeHasChanged_t>( x => OnVolumeChanged?.Invoke( x.NewVolume ) );
		}

		/// <summary>
		/// Invoked when playback status is changed.
		/// </summary>
		public static event Action OnPlaybackChanged;

		/// <summary>
		/// Invoked when the volume of the music player is changed.
		/// </summary>
		public static event Action<float> OnVolumeChanged;

		/// <summary>
		/// Checks if Steam Music is enabled.
		/// </summary>
		public static bool IsEnabled => Internal.BIsEnabled();

		/// <summary>
		/// <see langword="true"/> if a song is currently playing, paused, or queued up to play; otherwise <see langword="false"/>.
		/// </summary>
		public static bool IsPlaying => Internal.BIsPlaying();

		/// <summary>
		/// Gets the current status of the Steam Music player
		/// </summary>
		public static MusicStatus Status => Internal.GetPlaybackStatus();

		/// <summary>
		/// Plays the music player.
		/// </summary>
		public static void Play() => Internal.Play();

		/// <summary>
		/// Pauses the music player.
		/// </summary>
		public static void Pause() => Internal.Pause();

		/// <summary>
		/// Forces the music player to play the previous song.
		/// </summary>
		public static void PlayPrevious() => Internal.PlayPrevious();

		/// <summary>
		/// Forces the music player to skip to the next song.
		/// </summary>
		public static void PlayNext() => Internal.PlayNext();

		/// <summary>
		/// Gets and sets the current volume of the Steam Music player
		/// </summary>
		public static float Volume
		{
			get => Internal.GetVolume();
			set => Internal.SetVolume( value );
		}
	}
}
