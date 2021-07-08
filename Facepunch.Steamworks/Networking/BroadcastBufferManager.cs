using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;
using Steamworks.Data;

namespace Steamworks
{
	internal static unsafe class BufferManager
	{
		private sealed class ReferenceCounter
		{
			public IntPtr Pointer { get; private set; }
			public int Size { get; private set; }
			private int _count;

			public void Set( IntPtr ptr, int size, int referenceCount )
			{
				if ( ptr == IntPtr.Zero )
					throw new ArgumentNullException( nameof( ptr ) );
				if ( size <= 0 )
					throw new ArgumentOutOfRangeException( nameof( size ) );
				if ( referenceCount <= 0 )
					throw new ArgumentOutOfRangeException( nameof( referenceCount ) );

				Pointer = ptr;
				Size = size;

				var prevCount = Interlocked.Exchange(ref _count, referenceCount);
				if (prevCount != 0)
				{
#if DEBUG
					SteamNetworkingUtils.LogDebugMessage( NetDebugOutput.Warning, $"{nameof( BufferManager )} set reference count when current count was not 0" );
#endif
				}
			}

			public bool Decrement()
			{
				var newCount = Interlocked.Decrement( ref _count );
				if ( newCount < 0 )
				{
					SteamNetworkingUtils.LogDebugMessage( NetDebugOutput.Bug, $"Prevented double free of {nameof(BufferManager)} pointer" );
					return false;
				}

				return newCount == 0;
			}
		}

		[UnmanagedFunctionPointer( CallingConvention.Cdecl )]
		private delegate void FreeFn( NetMsg* msg );

		private static readonly Stack<ReferenceCounter> ReferenceCounterPool =
			new Stack<ReferenceCounter>( 1024 );

		private static readonly Dictionary<int, Stack<IntPtr>> BufferPools =
			new Dictionary<int, Stack<IntPtr>>();

		private static readonly Dictionary<IntPtr, ReferenceCounter> ReferenceCounters =
			new Dictionary<IntPtr, ReferenceCounter>( 1024 );

		private static readonly FreeFn FreeFunctionPin = new FreeFn( Free );

		public static readonly IntPtr FreeFunctionPointer = Marshal.GetFunctionPointerForDelegate( FreeFunctionPin );

		public static IntPtr Get( int size, int referenceCount )
		{
			const int maxSize = 16 * 1024 * 1024;
			if ( size < 0 || size > maxSize )
				throw new ArgumentOutOfRangeException( nameof( size ) );
			if ( referenceCount <= 0 )
				throw new ArgumentOutOfRangeException( nameof( referenceCount ) );

			AllocateBuffer( size, out var ptr, out var actualSize );
			var counter = AllocateReferenceCounter( ptr, actualSize, referenceCount );

#if DEBUG
			SteamNetworkingUtils.LogDebugMessage( NetDebugOutput.Verbose,
				$"{nameof( BufferManager )} allocated {ptr.ToInt64():X8} (size={size}, actualSize={actualSize}) with {referenceCount} references" );
#endif

			lock ( ReferenceCounters )
			{
				ReferenceCounters.Add( ptr, counter );
			}

			return ptr;
		}
		
		[MonoPInvokeCallback]
		private static void Free( NetMsg* msg )
		{
			var ptr = msg->DataPtr;

			lock ( ReferenceCounters )
			{
				if ( !ReferenceCounters.TryGetValue( ptr, out var counter ) )
				{
					SteamNetworkingUtils.LogDebugMessage( NetDebugOutput.Bug, $"Attempt to free pointer not tracked by {nameof(BufferManager)}: {ptr.ToInt64():X8}" );
					return;
				}

#if DEBUG
				SteamNetworkingUtils.LogDebugMessage( NetDebugOutput.Verbose, $"{nameof( BufferManager )} decrementing reference count of {ptr.ToInt64():X8}" );
#endif

				if ( counter.Decrement() )
				{
#if DEBUG
					SteamNetworkingUtils.LogDebugMessage( NetDebugOutput.Verbose, $"{nameof( BufferManager )} freeing {ptr.ToInt64():X8} as it is now unreferenced" );

					if ( ptr != counter.Pointer )
					{
						SteamNetworkingUtils.LogDebugMessage( NetDebugOutput.Bug,
							$"{nameof( BufferManager )} freed pointer ({ptr.ToInt64():X8}) does not match counter pointer ({counter.Pointer.ToInt64():X8})" );
					}

					var bucketSize = GetBucketSize( counter.Size );
					if ( counter.Size != bucketSize )
					{
						SteamNetworkingUtils.LogDebugMessage( NetDebugOutput.Bug,
							$"{nameof( BufferManager )} freed pointer size ({counter.Size}) does not match bucket size ({bucketSize})" );
					}
#endif

					ReferenceCounters.Remove( ptr );
					FreeBuffer( ptr, counter.Size );
					FreeReferenceCounter( counter );
				}
			}
		}

		private static ReferenceCounter AllocateReferenceCounter( IntPtr ptr, int size, int referenceCount )
		{
			lock ( ReferenceCounterPool )
			{
				var counter = ReferenceCounterPool.Count > 0
					? ReferenceCounterPool.Pop()
					: new ReferenceCounter();
				
				counter.Set( ptr, size, referenceCount );
				return counter;
			}
		}

		private static void FreeReferenceCounter( ReferenceCounter counter )
		{
			if ( counter == null )
				throw new ArgumentNullException( nameof( counter ) );

			lock ( ReferenceCounterPool )
			{
				if ( ReferenceCounterPool.Count >= 1024 )
				{
					// we don't want to keep a ton of these lying around - let it GC if we have too many
					return;
				}

				ReferenceCounterPool.Push( counter );
			}
		}

		private static void AllocateBuffer( int minimumSize, out IntPtr ptr, out int size )
		{
			var bucketSize = GetBucketSize( minimumSize );

			if ( bucketSize <= 0 )
			{
				// not bucketed, no pooling for this size
				ptr = Marshal.AllocHGlobal( minimumSize );
				size = minimumSize;

#if DEBUG
				SteamNetworkingUtils.LogDebugMessage( NetDebugOutput.Verbose,
					$"{nameof( BufferManager )} allocated unpooled pointer {ptr.ToInt64():X8} (size={size})" );
#endif
				return;
			}

			lock ( BufferPools )
			{
				if ( !BufferPools.TryGetValue( bucketSize, out var bucketPool ) || bucketPool.Count == 0 )
				{
					// nothing pooled yet, but we can pool this size
					ptr = Marshal.AllocHGlobal( bucketSize );
					size = bucketSize;

#if DEBUG
					SteamNetworkingUtils.LogDebugMessage( NetDebugOutput.Verbose,
						$"{nameof( BufferManager )} allocated new poolable pointer {ptr.ToInt64():X8} (size={size})" );
#endif
					return;
				}

				ptr = bucketPool.Pop();
				size = bucketSize;
#if DEBUG
				SteamNetworkingUtils.LogDebugMessage( NetDebugOutput.Verbose,
					$"{nameof( BufferManager )} allocated pointer from pool {ptr.ToInt64():X8} (size={size})" );
#endif
			}
		}

		private static void FreeBuffer( IntPtr ptr, int size )
		{
			var bucketSize = GetBucketSize( size );
			var bucketLimit = GetBucketLimit( size );

			if ( bucketSize <= 0 || bucketLimit <= 0 )
			{
				// not bucketed, no pooling for this size
				Marshal.FreeHGlobal( ptr );

#if DEBUG
				SteamNetworkingUtils.LogDebugMessage( NetDebugOutput.Verbose,
					$"{nameof( BufferManager )} freed unpooled pointer {ptr.ToInt64():X8} (size={size})" );
#endif
				return;
			}

			lock ( BufferPools )
			{
				if ( !BufferPools.TryGetValue( bucketSize, out var bucketPool ) )
				{
					bucketPool = new Stack<IntPtr>( bucketLimit );
					BufferPools.Add( bucketSize, bucketPool );
				}

				if ( bucketPool.Count >= bucketLimit )
				{
					// pool overflow, get rid
					Marshal.FreeHGlobal( ptr );

#if DEBUG
					SteamNetworkingUtils.LogDebugMessage( NetDebugOutput.Verbose,
						$"{nameof( BufferManager )} pool overflow, freed pooled pointer {ptr.ToInt64():X8} (size={size})" );
#endif
					return;
				}

				bucketPool.Push( ptr );
				
#if DEBUG
				SteamNetworkingUtils.LogDebugMessage( NetDebugOutput.Verbose,
					$"{nameof( BufferManager )} returned pointer to pool {ptr.ToInt64():X8} (size={size})" );
#endif
			}
		}

		private const int Bucket512 = 512;
		private const int Bucket1Kb = 1 * 1024;
		private const int Bucket4Kb = 4 * 1024;
		private const int Bucket16Kb = 16 * 1024;
		private const int Bucket64Kb = 64 * 1024;
		private const int Bucket256Kb = 256 * 1024;

		private static int GetBucketSize( int size )
		{
			if ( size <= Bucket512 ) return Bucket512;
			if ( size <= Bucket1Kb ) return Bucket1Kb;
			if ( size <= Bucket4Kb ) return Bucket4Kb;
			if ( size <= Bucket16Kb ) return Bucket16Kb;
			if ( size <= Bucket64Kb ) return Bucket64Kb;
			if ( size <= Bucket256Kb ) return Bucket256Kb;

			return -1;
		}

		private static int GetBucketLimit( int size )
		{
			if ( size <= Bucket512 ) return 1024;
			if ( size <= Bucket1Kb ) return 512;
			if ( size <= Bucket4Kb ) return 128;
			if ( size <= Bucket16Kb ) return 32;
			if ( size <= Bucket64Kb ) return 16;
			if ( size <= Bucket256Kb ) return 8;

			return -1;
		}
	}
}
