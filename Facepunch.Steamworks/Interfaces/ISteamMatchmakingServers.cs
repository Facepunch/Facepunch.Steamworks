using Steamworks.Data;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Steamworks
{
	internal partial class ISteamMatchmakingServers
	{
		/// <summary>
		/// Read gameserveritem_t.m_bHadSuccessfulResponse without allocating the struct on the heap
		/// </summary>
		/// <param name="hRequest"></param>
		/// <param name="iServer"></param>
		/// <returns></returns>
		internal bool HasServerResponded( HServerListRequest hRequest, int iServer )
		{
			IntPtr returnValue = _GetServerDetails( Self, hRequest, iServer );

			// Return false if steam returned null
			if ( returnValue == IntPtr.Zero ) return false;

			// first 8 bytes is IPAddress, next 4 bytes is ping, next 1 byte is m_bHadSuccessfulResponse
			return Marshal.ReadByte( IntPtr.Add( returnValue, 12 ) ) == 1;
		}
	}
}
