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
	public class SteamVideo : SteamClientClass<SteamVideo>
	{
		internal static ISteamVideo Internal => Interface as ISteamVideo;

		internal override void InitializeInterface( bool server )
		{
			SetInterface( server, new ISteamVideo( server ) );
			InstallEvents();
		}

		internal static void InstallEvents()
		{
			Dispatch.Install<BroadcastUploadStart_t>( x => OnBroadcastStarted?.Invoke() );
			Dispatch.Install<BroadcastUploadStop_t>( x => OnBroadcastStopped?.Invoke( x.Result ) );
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