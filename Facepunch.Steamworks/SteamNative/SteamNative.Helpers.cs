using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Collections.Generic;

namespace SteamNative
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
    }
}
