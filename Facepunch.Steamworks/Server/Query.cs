using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Facepunch.Steamworks
{
    public partial class Server
    {
        ServerQuery _query;

        public ServerQuery Query
        {
            get
            {
                if ( _query == null )
                    _query = new ServerQuery( this );

                return _query;
            }
        }
    }

    public class ServerQuery
    {
        internal Server server;
        internal static byte[] buffer = new byte[16*1024];

        internal ServerQuery( Server s )
        {
            server = s;
        }

        public struct Packet
        {
            public uint Address { get; internal set; }
            public byte[] Data { get; internal set; }
            public ushort Port { get; internal set; }
            public int Size { get; internal set; }
        }

        /// <summary>
        /// If true, Steam wants to send a packet. You should respond by sending
        /// this packet in an unconnected way to the returned Address and Port
        /// </summary>
        /// <param name="packet"></param>
        /// <returns></returns>
        public unsafe bool GetOutgoingPacket( out Packet packet )
        {
            packet = new Packet();

            fixed ( byte* ptr = buffer )
            {
                uint addr = 0;
                ushort port = 0;

                var size = server.native.gameServer.GetNextOutgoingPacket( (IntPtr)ptr, buffer.Length, out addr, out port );
                if ( size == 0 )
                    return false;

                packet.Size = size;
                packet.Data = buffer;
                packet.Address = addr;
                packet.Port = port;
                return true;
            }
        }

        /// <summary>
        /// We have received a server query on our game port. Pass it to Steam to handle.
        /// </summary>
        public unsafe void Handle( byte[] data, int size, uint address, ushort port )
        {
            fixed ( byte* ptr = data )
            {
                server.native.gameServer.HandleIncomingPacket( (IntPtr)ptr, size, address, port );
            }
        }

    }
}
