using System;
using System.Linq;

namespace Facepunch.Steamworks
{
    internal class ServerAuth : Auth
    {
        private Server _server;

        public ServerAuth( Server server )
        {
            _server = server;
            _server.RegisterCallback<SteamNative.ValidateAuthTicketResponse_t>( OnAuthTicketValidate );
            _server.RegisterCallback<SteamNative.GetAuthSessionTicketResponse_t>( OnGetAuthSessionTicketResponseThing );
        }

        public unsafe override Ticket GetAuthSessionTicket()
        {
            var data = new byte[1024];

            fixed ( byte* p = data )
            {
                uint ticketLength = 0;
                uint ticket = _server.native.gameServer.GetAuthSessionTicket( (IntPtr) p, data.Length, out ticketLength );

                if ( ticket == 0 )
                    return null;

                return new Ticket( this, ticket, data.Take( (int)ticketLength ).ToArray() );
            }
        }

        public override void CancelAuthTicket( Ticket ticket )
        {
            _server.native.gameServer.CancelAuthTicket( ticket.Handle );
        }

        public unsafe override StartAuthResult StartSession( byte[] data, ulong steamid )
        {
            fixed ( byte* p = data )
            {
                return (StartAuthResult) _server.native.gameServer.BeginAuthSession( (IntPtr) p, data.Length, steamid );
            }
        }

        public override void EndSession( ulong steamid )
        {
            _server.native.gameServer.EndAuthSession( steamid );
        }

        public override void Dispose()
        {
            _server = null;
        }
    }
}
