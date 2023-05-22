using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Collections.Generic;

namespace Steamworks
{
	internal static class Helpers
	{
		public const int MemoryBufferSize = 1024 * 32;

		internal struct Memory : IDisposable
		{
			private const int MaxBagSize = 4;
			private static readonly Queue<IntPtr> BufferBag = new Queue<IntPtr>();

			public IntPtr Ptr { get; private set; }

			public static implicit operator IntPtr(in Memory m) => m.Ptr;

			internal static unsafe Memory Take()
			{
				IntPtr ptr;
				lock (BufferBag)
				{
					ptr = BufferBag.Count > 0 ? BufferBag.Dequeue() : Marshal.AllocHGlobal(MemoryBufferSize);
				}
				((byte*)ptr)[0] = 0;
				return new Memory
				{
					Ptr = ptr
				};
			}

			public void Dispose()
			{
				if (Ptr == IntPtr.Zero) { return; }
				lock (BufferBag)
				{
					if (BufferBag.Count < MaxBagSize)
					{
						BufferBag.Enqueue(Ptr);
					}
					else
					{
						Marshal.FreeHGlobal(Ptr);
					}
				}
				Ptr = IntPtr.Zero;
			}
		}

		public static Memory TakeMemory()
		{
			return Memory.Take();
		}


		private static byte[][] BufferPool = new byte[4][];
		private static int BufferPoolIndex;

		/// <summary>
		/// Returns a buffer. This will get returned and reused later on.
		/// We shouldn't really be using this anymore. 
		/// </summary>
		public static byte[] TakeBuffer( int minSize )
		{
			lock ( BufferPool  )
			{
				BufferPoolIndex++;

				if ( BufferPoolIndex >= BufferPool.Length )
					BufferPoolIndex = 0;

				if ( BufferPool[BufferPoolIndex] == null ) 
					BufferPool[BufferPoolIndex] = new byte[1024 * 256];

				if ( BufferPool[BufferPoolIndex].Length < minSize )
				{
					BufferPool[BufferPoolIndex] = new byte[minSize + 1024];
				}

				return BufferPool[BufferPoolIndex];
			}
		}

		internal unsafe static string MemoryToString( IntPtr ptr )
		{
			var len = 0;

			for( len = 0; len < MemoryBufferSize; len++ )
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
