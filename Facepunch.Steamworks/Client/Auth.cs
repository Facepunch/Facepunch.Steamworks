using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Facepunch.Steamworks
{
    public partial class Client : IDisposable
    {
        Auth _auth;

        public Auth Auth
        {
            get
            {
                if ( _auth == null )
                    _auth = new Auth{ client = this };

                return _auth;
            }
        }
    }

    public class Auth
    {
        internal Client client;

        public class Ticket : IDisposable
        {
            internal Client client;

            public byte[] Data;
            public uint Handle;

            /// <summary>
            /// Cancels a ticket. 
            /// You should cancel your ticket when you close the game or leave a server.
            /// </summary>
            public void Cancel()
            {
                if ( client.IsValid && Handle != 0 )
                {
                    client.native.user.CancelAuthTicket( Handle );
                    Handle = 0;
                    Data = null;
                }
            }

            public void Dispose()
            {
                Cancel();
            }
        }

        /// <summary>
        /// Creates an auth ticket. 
        /// Which you can send to a server to authenticate that you are who you say you are.
        /// </summary>
        public unsafe Ticket GetAuthSessionTicket()
        {
            var data = new byte[1024];

            fixed ( byte* b = data )
            {
                uint ticketLength = 0;
                uint ticket = client.native.user.GetAuthSessionTicket( (IntPtr) b, data.Length, ref ticketLength );

                if ( ticket == 0 )
                    return null;

                return new Ticket()
                {
                    client = client,
                    Data = data.Take( (int)ticketLength ).ToArray(),
                    Handle = ticket
                };
            }
        }


    }
}
