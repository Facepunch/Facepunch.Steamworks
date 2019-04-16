using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Steamworks
{
	/// <summary>
	/// Not for reuse by newbs
	/// </summary>
	public class ServerListFavourites : BaseServerList
	{
		internal override void LaunchQuery()
		{
			var filters = GetFilters();
			request = Internal.RequestFavoritesServerList( AppId.Value, ref filters, (uint)filters.Length, IntPtr.Zero );
		}
	}
}