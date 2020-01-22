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
	public static class SteamVideo
	{
		static ISteamVideo _internal;
		internal static ISteamVideo Internal
		{
			get
			{
				SteamClient.ValidCheck();

				if ( _internal == null )
				{
					_internal = new ISteamVideo();
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
			BroadcastUploadStart_t.Install( x => OnBroadcastStarted?.Invoke() );
			BroadcastUploadStop_t.Install( x => OnBroadcastStopped?.Invoke( x.Result ) );
		}

		public static event Action OnBroadcastStarted;
		public static event Action<BroadcastUploadResult> OnBroadcastStopped;

		/// <summary>
		/// Return true if currently using Steam's live broadcasting
		/// </summary>
		public static bool IsBroadcasting
		{
			get
			{
				int viewers = 0;
				return Internal.IsBroadcasting( ref viewers );
			}
		}

		/// <summary>
		/// If we're broadcasting, will return the number of live viewers
		/// </summary>
		public static int NumViewers
		{
			get
			{
				int viewers = 0;

				if ( !Internal.IsBroadcasting( ref viewers ) )
					return 0;

				return viewers;
			}
		}
	}
}