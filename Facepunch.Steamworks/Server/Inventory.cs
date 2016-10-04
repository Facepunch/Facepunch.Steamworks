using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Facepunch.Steamworks
{
    public partial class Server
    {
        Inventory _inv;

        public Inventory Inventory
        {
            get
            {
                if ( _inv == null )
                    _inv = new Inventory( native.inventory, true );

                return _inv;
            }
        }
    }

}
