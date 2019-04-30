using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Collections.Generic;

namespace Steamworks
{
	internal static class Helpers
	{
        private static StringBuilder[] StringBuilderPool;
        private static int StringBuilderPoolIndex;

        /// <summary>
        /// Returns a StringBuilder. This will get returned and reused later on.
        /// </summary>
        public static StringBuilder TakeStringBuilder()
        {
            if ( StringBuilderPool == null )
            {
                //
                // The pool has 8 items. This should be safe because we shouldn't really
                // ever be using more than 2 StringBuilders at the same time.
                //
                StringBuilderPool = new StringBuilder[8];

                for ( int i = 0; i < StringBuilderPool.Length; i++ )
                    StringBuilderPool[i] = new StringBuilder( 4096 );
            }

            StringBuilderPoolIndex++;
            if ( StringBuilderPoolIndex >= StringBuilderPool.Length )
                StringBuilderPoolIndex = 0;

            StringBuilderPool[StringBuilderPoolIndex].Capacity = 4096;
            StringBuilderPool[StringBuilderPoolIndex].Length = 0;

            return StringBuilderPool[StringBuilderPoolIndex];
        }


		private static byte[][] BufferPool;
		private static int BufferPoolIndex;

		/// <summary>
		/// Returns a StringBuilder. This will get returned and reused later on.
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
	}

	internal class MonoPInvokeCallbackAttribute : Attribute
	{
		public MonoPInvokeCallbackAttribute() { }
	}
}
