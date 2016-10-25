using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Facepunch.Steamworks.Callbacks.Networking;

namespace Facepunch.Steamworks
{
    public class Networking : IDisposable
    {
        public Action<ulong, MemoryStream, int> OnP2PData;
        public Func<ulong, bool> OnIncomingConnection;
        public Action<ulong, SessionError> OnConnectionFailed;

        internal SteamNative.SteamNetworking networking;

        internal Networking( BaseSteamworks sw, SteamNative.SteamNetworking networking )
        {
            this.networking = networking;

            sw.AddCallback<P2PSessionRequest>( onP2PConnectionRequest, P2PSessionRequest.CallbackId );
            sw.AddCallback<P2PSessionConnectFail>( onP2PConnectionFailed, P2PSessionConnectFail.CallbackId );
        }

        public void Dispose()
        {
            networking = null;

            OnIncomingConnection = null;
            OnConnectionFailed = null;
            OnP2PData = null;
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

        private void onP2PConnectionRequest( P2PSessionRequest o )
        {
            if ( OnIncomingConnection != null )
            {
                var accept = OnIncomingConnection( o.SteamID );

                if ( accept )
                {
                    networking.AcceptP2PSessionWithUser( o.SteamID );
                }
                else
                {
                    networking.CloseP2PSessionWithUser( o.SteamID );
                }

                return;
            }

            //
            // Default is to reject the session
            //
            networking.CloseP2PSessionWithUser( o.SteamID );
        }

        public enum SessionError : byte
        {
            None = 0,
            NotRunningApp = 1,            // target is not running the same game
            NoRightsToApp = 2,            // local user doesn't own the app that is running
            DestinationNotLoggedIn = 3,   // target user isn't connected to Steam
            Timeout = 4,                  // target isn't responding, perhaps not calling AcceptP2PSessionWithUser()
                                          // corporate firewalls can also block this (NAT traversal is not firewall traversal)
                                          // make sure that UDP ports 3478, 4379, and 4380 are open in an outbound direction
            Max = 5
        };

        private void onP2PConnectionFailed( P2PSessionConnectFail o )
        {
            if ( OnConnectionFailed  != null )
            {
                OnConnectionFailed( o.SteamID, o.Error );
            }
        }

        public enum SendType : int
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

        public unsafe bool SendP2PPacket( ulong steamid, byte[] data, int length, SendType eP2PSendType = SendType.Reliable, int nChannel = 0 )
        {
            fixed ( byte* p = data )
            {
                return networking.SendP2PPacket( steamid, (IntPtr) p, (uint)length, (SteamNative.P2PSend)(int)eP2PSendType, nChannel );
            }
        }

        private unsafe bool ReadP2PPacket( MemoryStream ms, int channel = 0 )
        {
            uint DataAvailable = 0;

            if ( !networking.IsP2PPacketAvailable( out DataAvailable, channel ) || DataAvailable == 0 )
                return false;

            if ( ms.Capacity < DataAvailable )
                ms.Capacity = (int) DataAvailable;

            ms.Position = 0;
            ms.SetLength( DataAvailable );

            fixed ( byte* p = ms.GetBuffer() )
            {
                SteamNative.CSteamID steamid = 1;
                if ( !networking.ReadP2PPacket( (IntPtr)p, (uint)DataAvailable, out DataAvailable, out steamid, channel ) || DataAvailable == 0 )
                    return false;

                ms.SetLength( DataAvailable );

                OnP2PData?.Invoke( steamid, ms, channel );
                return true;
            }
        }
    }
}
