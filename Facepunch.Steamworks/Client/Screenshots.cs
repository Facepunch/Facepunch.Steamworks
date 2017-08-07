using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Facepunch.Steamworks
{
    public partial class Client : IDisposable
    {
        Screenshots _screenshots;

        public Screenshots Screenshots
        {
            get
            {
                if ( _screenshots == null )
                    _screenshots = new Screenshots( this );

                return _screenshots;
            }
        }
    }

    public class Screenshots
    {
        internal Client client;

        internal Screenshots( Client c )
        {
            client = c;
        }
        
        public void Trigger()
        {
            client.native.screenshots.TriggerScreenshot();
        }

        public unsafe void Write( byte[] rgbData, int width, int height )
        {
            if ( rgbData == null )
            {
                throw new ArgumentNullException( nameof(rgbData) );
            }

            if ( width < 1 )
            {
                throw new ArgumentOutOfRangeException( nameof(width), width,
                    $"Expected {nameof(width)} to be at least 1." );
            }

            if ( height < 1 )
            {
                throw new ArgumentOutOfRangeException( nameof(height), height,
                    $"Expected {nameof(height)} to be at least 1." );
            }

            var size = width * height * 3;
            if ( rgbData.Length < size )
            {
                throw new ArgumentException( nameof(rgbData),
                    $"Expected {nameof(rgbData)} to contain at least {size} elements (actual size: {rgbData.Length})." );
            }

            fixed ( byte* ptr = rgbData )
            {
                client.native.screenshots.WriteScreenshot( (IntPtr) ptr, (uint) rgbData.Length, width, height );
            }
        }
    }
}
