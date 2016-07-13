using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Valve.Steamworks;

namespace Facepunch.Steamworks
{
    public partial class Client : IDisposable
    {
        private Networking _net;

        public Networking Networking
        {
            get
            {
                if ( _net == null )
                    _net = new Networking( this );

                return _net;
            }
        }
    }

    public class Networking
    {
        public Action<ulong, MemoryStream, int> OnP2PData;

        internal Client client;

        internal class Callback
        {
            internal delegate void P2PSessionRequest( P2PSessionRequest_t a );
            internal delegate void P2PSessionConnectFail( P2PSessionConnectFail_t a );
        }

        internal Networking( Client c )
        {
            client = c;

            {
                Callback.P2PSessionRequest cb = onP2PConnectionRequest;
                client.InstallCallback( Valve.Steamworks.SteamAPI.k_iSteamNetworkingCallbacks + 2, cb );
            }

            {
                Callback.P2PSessionConnectFail cb = onP2PConnectionFailed;
                client.InstallCallback( Valve.Steamworks.SteamAPI.k_iSteamNetworkingCallbacks + 2, cb );
            }
        }

        internal void Update()
        {
            for ( int i = 0; i < 32; i++ )
            {
                // POOL ME
                using ( var ms = new MemoryStream() )
                {
                    while( ReadP2PPacket( ms, i ) )
                    {
                        // Nothing Here.
                    }
                }
            }
        }

        private void onP2PConnectionRequest( P2PSessionRequest_t o )
        {
            Console.WriteLine( "onP2PConnectionRequest " + o.m_steamIDRemote );
        }

        private void onP2PConnectionFailed( P2PSessionConnectFail_t o )
        {
            Console.WriteLine( "onP2PConnectionFailed " + o.m_steamIDRemote );
        }

        public enum EP2PSend : int
        {
            /// <summary>
            /// Basic UDP send. Packets can't be bigger than 1200 bytes (your typical MTU size). Can be lost, or arrive out of order (rare).
            /// The sending API does have some knowledge of the underlying connection, so if there is no NAT-traversal accomplished or
            /// there is a recognized adjustment happening on the connection, the packet will be batched until the connection is open again.
            /// </summary>

            Unreliable = 0,

            /// <summary>
            /// As above, but if the underlying p2p connection isn't yet established the packet will just be thrown away. Using this on the first
            /// packet sent to a remote host almost guarantees the packet will be dropped.
            /// This is only really useful for kinds of data that should never buffer up, i.e. voice payload packets
            /// </summary>
            UnreliableNoDelay = 1,

            //// <summary>
            /// Reliable message send. Can send up to 1MB of data in a single message.
            /// Does fragmentation/re-assembly of messages under the hood, as well as a sliding window for efficient sends of large chunks of data.
            /// </summary>
            Reliable = 2,

            /// <summary>
            /// As above, but applies the Nagle algorithm to the send - sends will accumulate
            /// until the current MTU size (typically ~1200 bytes, but can change) or ~200ms has passed (Nagle algorithm).
            /// Useful if you want to send a set of smaller messages but have the coalesced into a single packet
            /// Since the reliable stream is all ordered, you can do several small message sends with k_EP2PSendReliableWithBuffering and then
            /// do a normal k_EP2PSendReliable to force all the buffered data to be sent.
            /// </summary>
            ReliableWithBuffering = 3,

        }

        public unsafe bool SendP2PPacket( ulong steamid, byte[] data, int length, EP2PSend eP2PSendType = EP2PSend.Reliable, int nChannel = 0 )
        {
            fixed ( byte* p = data )
            {
                return client._networking.SendP2PPacket( steamid, (IntPtr) p, (uint)length, (uint)eP2PSendType, nChannel );
            }
        }

        private unsafe bool ReadP2PPacket( MemoryStream ms, int channel = 0 )
        {
            uint DataAvailable = 0;

            if ( !client._networking.IsP2PPacketAvailable( ref DataAvailable, channel ) || DataAvailable == 0 )
                return false;

            if ( ms.Capacity < DataAvailable )
                ms.Capacity = (int) DataAvailable;

            ms.Position = 0;
            ms.SetLength( DataAvailable );

            fixed ( byte* p = ms.GetBuffer() )
            {
                ulong steamid = 1;
                if ( !client._networking.ReadP2PPacket( (IntPtr)p, (uint)DataAvailable, ref DataAvailable, ref steamid, channel ) || DataAvailable == 0 )
                    return false;

                ms.SetLength( DataAvailable );

                OnP2PData?.Invoke( steamid, ms, channel );
                return true;
            }
        }
    }
}
