using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Facepunch.Steamworks
{
    public partial class Client : IDisposable
    {
        public class AuthTicket
        {
            public byte[] Data;
            public uint Handle;
        }

        /// <summary>
        /// Creates an auth ticket. 
        /// Which you can send to a server to authenticate that you are who you say you are.
        /// </summary>
        public unsafe AuthTicket GetAuthSessionTicket()
        {
            var data = new byte[1024];
          
            fixed ( byte* b = data )
            {
                uint ticketLength = 0;
                uint ticket = _user.GetAuthSessionTicket( (IntPtr) b, data.Length, ref ticketLength );

                if ( ticket == 0 )
                    return null;

                return new AuthTicket()
                {
                    Data = data.Take( (int)ticketLength ).ToArray(),
                    Handle = ticket
                };
            }
        }

        /// <summary>
        /// Cancels a ticket. 
        /// You should cancel your ticket when you close the game or leave a server.
        /// </summary>
        public void CancelAuthTicket( AuthTicket ticket )
        {
            _user.CancelAuthTicket( ticket.Handle );
            ticket.Handle = 0;
            ticket.Data = null;
        }
    }
}
