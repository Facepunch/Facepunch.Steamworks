using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Facepunch.Steamworks
{
    public partial class Client : IDisposable
    {
        Friends _friends;

        public Friends Friends
        {
            get
            {
                if ( _friends == null )
                    _friends = new Friends( this );

                return _friends;
            }
        }
    }

    public class Friends
    {
        internal Client client;

        internal Friends( Client c )
        {
            client = c;
        }
        
        public string GetName( ulong steamid )
        {
            client.native.friends.RequestUserInformation( steamid, true );

            return client.native.friends.GetFriendPersonaName( steamid );
        }
    }
}
