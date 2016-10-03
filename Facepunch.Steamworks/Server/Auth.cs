using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Facepunch.Steamworks
{
    public partial class Server
    {
        ServerAuth _auth;

        public ServerAuth Auth
        {
            get
            {
                if ( _auth == null )
                    _auth = new ServerAuth { server = this };

                return _auth;
            }
        }
    }

    public class ServerAuth
    {
        internal Server server;

        /// <summary>
        /// Start authorizing a ticket
        /// </summary>
        public unsafe bool StartSession( byte[] data, ulong steamid )
        {
            fixed ( byte* p = data )
            {
                var result = (Valve.Steamworks.EBeginAuthSessionResult) server.native.gameServer.BeginAuthSession( (IntPtr)p, data.Length, steamid );

                if ( result == Valve.Steamworks.EBeginAuthSessionResult.k_EBeginAuthSessionResultOK )
                    return true;

                return false;
            }
        }

        public void EndSession( ulong steamid )
        {
            server.native.gameServer.EndAuthSession( steamid );
        }

    }
}
