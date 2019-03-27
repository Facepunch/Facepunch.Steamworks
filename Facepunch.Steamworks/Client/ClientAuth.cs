using System;
using System.Linq;

namespace Facepunch.Steamworks
{
    internal class ClientAuth : Auth
    {
        private Client _client;

        public ClientAuth( Client client ) : base( client )
        {
            _client = client;
        }

        public unsafe override Ticket GetAuthSessionTicket()
        {
            var data = new byte[1024];

            fixed ( byte* p = data )
            {
                uint ticketLength = 0;
                uint ticket = _client.native.user.GetAuthSessionTicket( (IntPtr) p, data.Length, out ticketLength );

                if ( ticket == 0 )
                    return null;

                return new Ticket( this, ticket, data.Take( (int)ticketLength ).ToArray() );
            }
        }

        public override void CancelAuthTicket( Ticket ticket )
        {
            _client.native.user.CancelAuthTicket( ticket.Handle );
        }

        public unsafe override StartAuthResult StartSession( byte[] data, ulong steamid )
        {
            fixed ( byte* p = data )
            {
                return (StartAuthResult) _client.native.user.BeginAuthSession( (IntPtr) p, data.Length, steamid );
            }
        }

        public override void EndSession( ulong steamid )
        {
            _client.native.user.EndAuthSession( steamid );
        }

        public override void Dispose()
        {
            _client = null;
        }
    }
}
