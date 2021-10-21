using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Steamworks.Data;

namespace Steamworks
{
	/// <summary>
	/// Class for utilizing the Steam Video API.
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
		}

		/// <summary>
		/// Return <see langword="true"/> if currently using Steam's live broadcasting
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
		/// Returns the number of viewers that are watching the stream, or <c>0</c> if <see cref="IsBroadcasting"/> is <see langword="false"/>.
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
