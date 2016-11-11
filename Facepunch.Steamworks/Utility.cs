using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Facepunch.Steamworks
{
    internal static class Utility
    {
        static internal uint SwapBytes( uint x )
        {
            return ( ( x & 0x000000ff ) << 24 ) +
                   ( ( x & 0x0000ff00 ) << 8 ) +
                   ( ( x & 0x00ff0000 ) >> 8 ) +
                   ( ( x & 0xff000000 ) >> 24 );
        }

        static internal class Epoch
        {
            private static readonly DateTime epoch = new DateTime( 1970, 1, 1, 0, 0, 0, DateTimeKind.Utc );

            /// <summary>
            /// Returns the current Unix Epoch
            /// </summary>
            public static int Current
            {
                get
                {
                    return (int)( DateTime.UtcNow.Subtract( epoch ).TotalSeconds );
                }
            }

            /// <summary>
            /// Convert an epoch to a datetime
            /// </summary>
            public static DateTime ToDateTime( decimal unixTime )
            {
                return epoch.AddSeconds( (long)unixTime );
            }

            /// <summary>
            /// Convert a DateTime to a unix time
            /// </summary>
            public static uint FromDateTime( DateTime dt )
            {
                return (uint)( dt.Subtract( epoch ).TotalSeconds );
            }

        }
    }
}
