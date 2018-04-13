using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Facepunch.Steamworks
{
    public class Networking : IDisposable
    {
        private static byte[] ReceiveBuffer = new byte[1024 * 64];

        public delegate void OnRecievedP2PData( ulong steamid, byte[] data, int dataLength, int channel );

        public OnRecievedP2PData OnP2PData;
        public Func<ulong, bool> OnIncomingConnection;
        public Action<ulong, SessionError> OnConnectionFailed;

        private List<int> ListenChannels = new List<int>();

        private System.Diagnostics.Stopwatch UpdateTimer = System.Diagnostics.Stopwatch.StartNew();

        internal SteamNative.SteamNetworking networking;

        internal Networking( BaseSteamworks steamworks, SteamNative.SteamNetworking networking )
        {
            this.networking = networking;

            steamworks.RegisterCallback<SteamNative.P2PSessionRequest_t>( onP2PConnectionRequest );
            steamworks.RegisterCallback<SteamNative.P2PSessionConnectFail_t>( onP2PConnectionFailed );
        }

        public void Dispose()
        {
            networking = null;

            OnIncomingConnection = null;
            OnConnectionFailed = null;
            OnP2PData = null;
            ListenChannels.Clear();
        }


        /// <summary>
        /// No need to call this manually if you're calling Update()
        /// </summary>
        public void Update()
        {
            if ( OnP2PData == null )
                return;

            // Update every 60th of a second
            if ( UpdateTimer.Elapsed.TotalSeconds < 1.0 / 60.0 )
                return;

            UpdateTimer.Reset();
            UpdateTimer.Start();

            foreach ( var channel in ListenChannels )
            {
                while ( ReadP2PPacket( channel ) )
                {
                    // Nothing Here.
                }
            }
        }

        /// <summary>
        /// Enable or disable listening on a specific channel.
        /// If you donp't enable the channel we won't listen to it,
        /// so you won't be able to receive messages on it.
        /// </summary>
        public void SetListenChannel( int ChannelId, bool Listen )
        {
            ListenChannels.RemoveAll( x => x == ChannelId );

            if ( Listen  )
            {
                ListenChannels.Add( ChannelId );
            }
        }

        private void onP2PConnectionRequest( SteamNative.P2PSessionRequest_t o )
        {
            if ( OnIncomingConnection != null )
            {
                var accept = OnIncomingConnection( o.SteamIDRemote );

                if ( accept )
                {
                    networking.AcceptP2PSessionWithUser( o.SteamIDRemote );
                }
                else
                {
                    networking.CloseP2PSessionWithUser( o.SteamIDRemote );
                }

                return;
            }

            //
            // Default is to reject the session
            //
            networking.CloseP2PSessionWithUser( o.SteamIDRemote );
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

        private void onP2PConnectionFailed( SteamNative.P2PSessionConnectFail_t o )
        {
            if ( OnConnectionFailed  != null )
            {
                OnConnectionFailed( o.SteamIDRemote, (SessionError) o.P2PSessionError );
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

            /// <summary>
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

        private unsafe bool ReadP2PPacket( int channel )
        {
            uint DataAvailable = 0;

            if ( !networking.IsP2PPacketAvailable( out DataAvailable, channel ) )
                return false;

            if ( ReceiveBuffer.Length < DataAvailable )
                ReceiveBuffer = new byte[ DataAvailable + 1024 ];

            fixed ( byte* p = ReceiveBuffer )
            {
                SteamNative.CSteamID steamid = 1;
                if ( !networking.ReadP2PPacket( (IntPtr)p, DataAvailable, out DataAvailable, out steamid, channel ) || DataAvailable == 0 )
                    return false;

                OnP2PData?.Invoke( steamid, ReceiveBuffer, (int) DataAvailable, channel );
                return true;
            }
        }

        /// <summary>
        /// This should be called when you're done communicating with a user, as this will free up all of the resources allocated for the connection under-the-hood.
        /// If the remote user tries to send data to you again, a new onP2PConnectionRequest callback will be posted.
        /// </summary>
        public bool CloseSession( ulong steamId )
        {
            return networking.CloseP2PSessionWithUser( steamId );
        }
    }
}
