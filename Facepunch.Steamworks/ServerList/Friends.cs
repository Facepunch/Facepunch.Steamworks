﻿using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Steamworks.ServerList
{
	public class Friends : Base
	{
		internal override void LaunchQuery()
		{
			var filters = GetFilters();
			request = Internal.RequestFriendsServerList( AppId.Value, ref filters, (uint)filters.Length, IntPtr.Zero );
		}
	}
}