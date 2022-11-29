using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Steamworks.Data;

namespace Steamworks
{
	/// <summary>
	/// Methods for clients to access matchmaking services, favorites, and to operate on game lobbies
	/// </summary>
	internal class SteamMatchmakingServers : SteamClientClass<SteamMatchmakingServers>
	{
		internal static ISteamMatchmakingServers Internal => Interface as ISteamMatchmakingServers;

		internal override bool InitializeInterface( bool server )
		{
			SetInterface( server, new ISteamMatchmakingServers( server ) );
			if ( Interface.Self == IntPtr.Zero ) return false;

			return true;
		}
	}
}
