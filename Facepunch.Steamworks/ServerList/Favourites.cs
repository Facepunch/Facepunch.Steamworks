using System;

namespace Steamworks.ServerList
{
	public class Favourites : Base
	{
		internal override void LaunchQuery()
		{
			using var filters = new ServerFilterMarshaler( GetFilters() );
			request = Internal.RequestFavoritesServerList( AppId.Value, filters.Pointer, (uint)filters.Count, IntPtr.Zero );
		}
	}
}
