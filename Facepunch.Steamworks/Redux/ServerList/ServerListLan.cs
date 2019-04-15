using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using SteamNative;

namespace Steamworks
{
	/// <summary>
	/// Not for reuse by newbs
	/// </summary>
	public class ServerListLan : BaseServerList
	{
		internal override void LaunchQuery()
		{
			request = Internal.RequestLANServerList( AppId.Value, IntPtr.Zero );
		}
	}
}