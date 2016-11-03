using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Facepunch.Steamworks
{
    /// <summary>
    /// If you're manually processing the server queries, you should use this class.
    /// </summary>
    public class ServerQuery
    {
        internal Server server;
        internal static byte[] buffer = new byte[16*1024];

        internal ServerQuery( Server s )
        {
            server = s;
        }

        /// <summary>
        /// A server query packet.
        /// </summary>
        public struct Packet
        {
            /// <summary>
            /// Target IP address
            /// </summary>
            public uint Address { get; internal set; }

            /// <summary>
            /// Target port
            /// </summary>
            public ushort Port { get; internal set; }

            /// <summary>
            /// This data is pooled. Make a copy if you don't use it immediately.
            /// This buffer is also quite large - so pay attention to Size.
            /// </summary>
            public byte[] Data { get; internal set; }

            /// <summary>
            /// Size of the data
            /// </summary>
            public int Size { get; internal set; }
        }

        /// <summary>
        /// If true, Steam wants to send a packet. You should respond by sending
        /// this packet in an unconnected way to the returned Address and Port.
        /// </summary>
        /// <param name="packet">Packet to send. The Data passed is pooled - so use it immediately.</param>
        /// <returns>True if we want to send a packet</returns>
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
