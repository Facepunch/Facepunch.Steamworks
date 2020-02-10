using Steamworks.Data;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Steamworks
{
	//
	// Created on registration of a callback
	//
	internal struct CallbackResult 
	{
		SteamAPICall_t call;

		public CallbackResult( SteamAPICall_t call )
		{
			this.call = call;
		}

		public async Task<T?> GetAsync<T>() where T : struct, ICallbackData
		{
			bool failed = false;
			var t = default( T );
			var size = t.DataSize;

			while ( !SteamUtils.IsCallComplete( call, out failed ) )
			{
				await Task.Delay( 1 );
				if ( !SteamClient.IsValid && !SteamServer.IsValid ) return null;
			}
			if ( failed ) return null;

			var ptr = Marshal.AllocHGlobal( size );

			try
			{
				if ( !SteamUtils.Internal.GetAPICallResult( call, ptr, size, t.CallbackId, ref failed ) || failed )
					return null;

				return ((T)Marshal.PtrToStructure( ptr, typeof( T ) ));
			}
			finally
			{
				Marshal.FreeHGlobal( ptr );
			}
		}

	}
}