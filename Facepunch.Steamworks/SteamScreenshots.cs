using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Steamworks.Data;

namespace Steamworks
{
	/// <summary>
	/// Undocumented Parental Settings
	/// </summary>
	public class SteamScreenshots : SteamClientClass<SteamScreenshots>
	{
		internal static ISteamScreenshots Internal => Interface as ISteamScreenshots;

		internal override void InitializeInterface( bool server )
		{
			SetInterface( server, new ISteamScreenshots( server ) );
			InstallEvents();
		}

		internal static void InstallEvents()
		{
			Dispatch.Install<ScreenshotRequested_t>( x => OnScreenshotRequested?.Invoke() );
			Dispatch.Install<ScreenshotReady_t>( x =>
			{
				if ( x.Result != Result.OK )
					OnScreenshotFailed?.Invoke( x.Result );
				else
					OnScreenshotReady?.Invoke( new Screenshot { Value = x.Local } );
			} );
		}

		/// <summary>
		/// A screenshot has been requested by the user from the Steam screenshot hotkey. 
		/// This will only be called if Hooked is true, in which case Steam 
		/// will not take the screenshot itself.
		/// </summary>
		public static event Action OnScreenshotRequested;

		/// <summary>
		/// A screenshot successfully written or otherwise added to the library and can now be tagged.
		/// </summary>
		public static event Action<Screenshot> OnScreenshotReady;

		/// <summary>
		/// A screenshot attempt failed
		/// </summary>
		public static event Action<Result> OnScreenshotFailed;

		/// <summary>
		/// Writes a screenshot to the user's screenshot library given the raw image data, which must be in RGB format.
		/// The return value is a handle that is valid for the duration of the game process and can be used to apply tags.
		/// </summary>
		public unsafe static Screenshot? WriteScreenshot( byte[] data, int width, int height )
		{
			fixed ( byte* ptr = data )
			{
				var handle = Internal.WriteScreenshot( (IntPtr)ptr, (uint)data.Length, width, height );
				if ( handle.Value == 0 ) return null;

				return new Screenshot { Value = handle };
			}
		}

		/// <summary>
		/// Adds a screenshot to the user's screenshot library from disk.  If a thumbnail is provided, it must be 200 pixels wide and the same aspect ratio
		/// as the screenshot, otherwise a thumbnail will be generated if the user uploads the screenshot.  The screenshots must be in either JPEG or TGA format.
		/// The return value is a handle that is valid for the duration of the game process and can be used to apply tags.
		/// JPEG, TGA, and PNG formats are supported.
		/// </summary>
		public unsafe static Screenshot? AddScreenshot( string filename, string thumbnail, int width, int height )
		{
			var handle = Internal.AddScreenshotToLibrary( filename, thumbnail, width, height );
			if ( handle.Value == 0 ) return null;

			return new Screenshot { Value = handle };
		}

		/// <summary>
		/// Causes the Steam overlay to take a screenshot.  
		/// If screenshots are being hooked by the game then a 
		/// ScreenshotRequested callback is sent back to the game instead. 
		/// </summary>
		public static void TriggerScreenshot() => Internal.TriggerScreenshot();

		/// <summary>
		/// Toggles whether the overlay handles screenshots when the user presses the screenshot hotkey, or if the game handles them.
		/// Hooking is disabled by default, and only ever enabled if you do so with this function.
		/// If the hooking is enabled, then the ScreenshotRequested_t callback will be sent if the user presses the hotkey or 
		/// when TriggerScreenshot is called, and then the game is expected to call WriteScreenshot or AddScreenshotToLibrary in response.
		/// </summary>
		public static bool Hooked
		{
			get => Internal.IsScreenshotsHooked();
			set => Internal.HookScreenshots( value );
		}
	}
}