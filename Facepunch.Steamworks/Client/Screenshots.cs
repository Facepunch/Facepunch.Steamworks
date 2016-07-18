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

    }
}
