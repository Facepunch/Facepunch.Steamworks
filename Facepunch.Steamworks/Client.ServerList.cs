using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Facepunch.Steamworks
{
    public partial class Client : IDisposable
    {
        private ServerList _serverlist;

        public ServerList ServerList
        {
            get
            {
                if ( _serverlist == null )
                    _serverlist = new ServerList { client = this };

                return _serverlist;
            }
        }
    }

    public class ServerList
    {
        internal Client client;

    }
}
