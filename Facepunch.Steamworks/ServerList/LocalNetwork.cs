using System;

namespace Steamworks.ServerList
{
	public class LocalNetwork : Base
	{
		internal override void LaunchQuery()
		{
			request = Internal.RequestLANServerList( AppId.Value, IntPtr.Zero );
		}
	}
}
