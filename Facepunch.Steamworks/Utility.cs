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
    }
}
