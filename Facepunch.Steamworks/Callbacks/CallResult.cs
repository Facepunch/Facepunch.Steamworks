using Steamworks.Data;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace Steamworks
{
	/// <summary>
	/// An awaitable version of a SteamAPICall_t
	/// </summary>
	/// <remarks>
	/// <c>T</c> is constrained to <c>unmanaged</c> for the same reason
	/// <see cref="Dispatch.Install{T}"/> is: <see cref="GetResult"/> reads the result struct
	/// straight out of the buffer Steam filled, and the constraint is the compiler proving
	/// that is safe. All 60 result types used with this satisfy it.
	/// </remarks>
	internal struct CallResult<T> : INotifyCompletion where T : unmanaged, ICallbackData
	{
		SteamAPICall_t call;
		ISteamUtils utils;
		bool server;

		public CallResult( SteamAPICall_t call, bool server )
		{
			this.call = call;
			this.server = server;

			utils = (server ? SteamUtils.InterfaceServer : SteamUtils.InterfaceClient) as ISteamUtils;

			if ( utils == null )
				utils = SteamUtils.Interface as ISteamUtils;
		}

		/// <summary>
		/// This gets called if IsComplete returned false on the first call.
		/// The Action "continues" the async call. We pass it to the Dispatch
		/// to be called when the callback returns.
		/// </summary>
		public void OnCompleted( Action continuation )
		{
			if (IsCompleted)
				continuation();
			else
				Dispatch.OnCallComplete<T>(call, continuation, server);
		}

		/// <summary>
		/// Gets the result. This is called internally by the async shit.
		/// </summary>
		public T? GetResult()
		{
			bool failed = false;
			if ( !utils.IsAPICallCompleted( call, ref failed ) || failed )
				return null;

			var t = default( T );
			var size = t.DataSize;
			var ptr = Marshal.AllocHGlobal( size );

			try
			{
				if ( !utils.GetAPICallResult( call, ptr, size, (int)t.CallbackType, ref failed ) || failed )
				{
					Dispatch.OnDebugCallback?.Invoke( t.CallbackType, "!GetAPICallResult or failed", server );
					return null;
				}

				Dispatch.OnDebugCallback?.Invoke( t.CallbackType, Dispatch.CallbackToString( t.CallbackType, ptr, size ), server );

				// Raw read rather than Marshal.PtrToStructure, which boxes sizeof(T)+16 bytes
				// and, for any struct carrying a [MarshalAs] attribute, walks the field list
				// one member at a time. This is the payload path for every awaited Steam
				// call. Measured: SteamUGCQueryCompleted_t 296 B / 1,740 ns -> 0 B / 19 ns;
				// SteamInventoryEligiblePromoItemDefIDs_t 40 B / 1,669 ns -> 0 B / 6.4 ns.
				return ptr.ToTypeUnmanaged<T>();
			}
			finally
			{
				Marshal.FreeHGlobal( ptr );
			}
		}

		/// <summary>
		/// Return true if complete or failed
		/// </summary>
		public bool IsCompleted
		{
			get
			{
				bool failed = false;
				if ( utils.IsAPICallCompleted( call, ref failed ) || failed )
					return true;

				return false;
			}
		}

		/// <summary>
		/// This is what makes this struct awaitable
		/// </summary>
		internal CallResult<T> GetAwaiter()
		{
			return this;
		}
	}
}