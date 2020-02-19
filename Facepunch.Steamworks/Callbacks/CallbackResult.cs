using Steamworks.Data;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace Steamworks
{
	internal struct CallResult<T> : INotifyCompletion where T : struct, ICallbackData
	{
		SteamAPICall_t call;

		public CallResult( SteamAPICall_t call )
		{
			this.call = call;
			Console.WriteLine( $"{this.GetType().ToString()} == {call.Value}" );
		}

		public void OnCompleted( Action continuation )
		{
			Dispatch.OnCallComplete( call, continuation );
		}

		public T? GetResult()
		{
			if ( !SteamUtils.IsCallComplete( call, out var failed ) || failed )
				return null;

			var t = default( T );
			var size = t.DataSize;
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

		public bool IsCompleted
		{
			get
			{
				if ( SteamUtils.IsCallComplete( call, out var failed ) || failed )
					return true;

				return false;
			}
		}

		internal CallResult<T> GetAwaiter()
		{
			return this;
		}
	}
}