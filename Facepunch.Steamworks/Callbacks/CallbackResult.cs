using Steamworks.Data;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace Steamworks
{
	internal struct CallbackResult<T> : INotifyCompletion where T : struct, ICallbackData
	{
		SteamAPICall_t call;

		public CallbackResult( SteamAPICall_t call )
		{
			this.call = call;
		}

		public void OnCompleted( Action continuation )
		{
			var ts = TaskScheduler.Current;
			var sc = SynchronizationContext.Current;

			while ( !IsCompleted )
			{
				// Nothing
			}

			continuation();
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

		internal CallbackResult<T> GetAwaiter()
		{
			return this;
		}
	}
}