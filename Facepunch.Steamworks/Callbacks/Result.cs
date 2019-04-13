using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using SteamNative;

namespace Steamworks
{
	/// <summary>
	/// Results are Steam Callbacks that are direct responses to function calls
	/// </summary>
	public struct Result<T> where T : struct, ISteamCallback
	{
		public ulong CallHandle;

		public Result( ulong callbackHandle )
		{
			CallHandle = callbackHandle;
		}

		public bool IsComplete( out bool failed )
		{
			return Utils.IsCallComplete( CallHandle, out failed );
		}

		public async Task<T?> GetResult()
		{
			bool failed = false;

			while ( !IsComplete( out failed ) )
			{
				await Task.Delay( 1 );
			}

			if ( failed )
				return null;

			return Utils.GetResult<T>( CallHandle );
		}
	}
}