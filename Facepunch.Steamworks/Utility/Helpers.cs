using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Collections.Generic;

namespace Steamworks
{
	internal static class Helpers
	{
		public const int MaxStringSize = 1024 * 32;

		private static IntPtr[] MemoryPool;
		private static int MemoryPoolIndex;

		public static unsafe IntPtr TakeMemory()
		{
			if ( MemoryPool == null )
			{
				//
				// The pool has 5 items. This should be safe because we shouldn't really
				// ever be using more than 2 memory pools
				//
				MemoryPool = new IntPtr[5];

				for ( int i = 0; i < MemoryPool.Length; i++ )
					MemoryPool[i] = Marshal.AllocHGlobal( MaxStringSize );
			}

			MemoryPoolIndex++;
			if ( MemoryPoolIndex >= MemoryPool.Length )
				MemoryPoolIndex = 0;

			var take = MemoryPool[MemoryPoolIndex];

			((byte*)take)[0] = 0;

			return take;
		}


		private static byte[][] BufferPool;
		private static int BufferPoolIndex;

		/// <summary>
		/// Returns a buffer. This will get returned and reused later on.
		/// </summary>
		public static byte[] TakeBuffer( int minSize )
		{
			if ( BufferPool == null )
			{
				//
				// The pool has 8 items.
				//
				BufferPool = new byte[8][];

				for ( int i = 0; i < BufferPool.Length; i++ )
					BufferPool[i] = new byte[ 1024 * 128 ];
			}

			BufferPoolIndex++;
			if ( BufferPoolIndex >= BufferPool.Length )
				BufferPoolIndex = 0;

			if ( BufferPool[BufferPoolIndex].Length < minSize )
			{
				BufferPool[BufferPoolIndex] = new byte[minSize + 1024];
			}

			return BufferPool[BufferPoolIndex];
		}

		internal unsafe static string MemoryToString( IntPtr ptr )
		{
			var len = 0;

			for( len = 0; len < MaxStringSize; len++ )
			{
				if ( ((byte*)ptr)[len] == 0 )
					break;
			}

			if ( len == 0 )
				return string.Empty;

			return UTF8Encoding.UTF8.GetString( (byte*)ptr, len );
		}
	}

	internal class MonoPInvokeCallbackAttribute : Attribute
	{
		public MonoPInvokeCallbackAttribute() { }
	}

	/// <summary>
	/// Prevent unity from stripping shit we depend on
	/// https://docs.unity3d.com/Manual/ManagedCodeStripping.html
	/// </summary>
	internal class PreserveAttribute : System.Attribute { }
}
