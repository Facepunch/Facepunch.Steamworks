using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Facepunch.Steamworks
{
    public partial class Client : IDisposable
    {
        Stats _stats;

        public Stats Stats
        {
            get
            {
                if ( _stats == null )
                    _stats = new Stats( this );

                return _stats;
            }
        }
    }

    public class Stats
    {
        internal Client client;

        internal Stats( Client c )
        {
            client = c;
        }
    }
}
