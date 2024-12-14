using System;

namespace Steamworks.ServerList
{
	public class Friends : Base
	{
		internal override void LaunchQuery()
		{
			using var filters = new ServerFilterMarshaler( GetFilters() );
			request = Internal.RequestFriendsServerList( AppId.Value, filters.Pointer, (uint)filters.Count, IntPtr.Zero );
		}
	}
}
