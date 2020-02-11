using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Steamworks.Data;

namespace Steamworks
{
	/// <summary>
	/// Functions for clients to access matchmaking services, favorites, and to operate on game lobbies
	/// </summary>
	public class SteamMatchmakingServers : SteamClass
	{
		internal static ISteamMatchmakingServers Internal;
		internal override SteamInterface Interface => Internal;

		internal override void InitializeInterface()
		{
			Internal = new ISteamMatchmakingServers();
		}
	}
}