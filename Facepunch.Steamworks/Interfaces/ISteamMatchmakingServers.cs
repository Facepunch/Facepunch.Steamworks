using Steamworks.Data;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Steamworks
{
	internal partial class ISteamMatchmakingServers
	{
		// Cached offset of gameserveritem_t.m_bHadSuccessfulResponse
		private static int hasSuccessfulResponseOffset;

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

			// Cache the offset of m_bHadSuccessfulResponse
			if ( hasSuccessfulResponseOffset == 0 )
			{
				hasSuccessfulResponseOffset = Marshal.OffsetOf<gameserveritem_t>( nameof( gameserveritem_t.HadSuccessfulResponse ) ).ToInt32();

				if ( hasSuccessfulResponseOffset == 0 )
				{
					throw new Exception( "Failed to get offset of gameserveritem_t.HadSuccessfulResponse" );
				}
			}

			// Read byte m_bHadSuccessfulResponse
			return Marshal.ReadByte( IntPtr.Add( returnValue, hasSuccessfulResponseOffset ) ) == 1;
		}
	}
}
