using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;

namespace Facepunch.Steamworks
{

    internal class SourceServerQuery : IDisposable
    {
        public static List<SourceServerQuery> Current = new List<SourceServerQuery>();

        public static void Cycle()
        {
            if ( Current.Count == 0 )
                return;

            for( int i = Current.Count; i>0; i-- )
            {
                Current[i-1].Update();
            }
        }

        private static readonly byte[] A2S_SERVERQUERY_GETCHALLENGE = { 0x55, 0xFF, 0xFF, 0xFF, 0xFF };
  //      private static readonly byte A2S_PLAYER = 0x55;
        private static readonly byte A2S_RULES = 0x56;

        public volatile bool IsRunning;
        public volatile bool IsSuccessful;

        private ServerList.Server Server;
        private UdpClient udpClient;
        private IPEndPoint endPoint;
        private System.Threading.Thread thread;
        private byte[] _challengeBytes;

        private Dictionary<string, string> rules = new Dictionary<string, string>();

        public SourceServerQuery( ServerList.Server server, IPAddress address, int queryPort )
        {
            Server = server;
            endPoint = new IPEndPoint( address, queryPort );

            Current.Add( this );

            IsRunning = true;
            IsSuccessful = false;
            thread = new System.Threading.Thread( ThreadedStart );
            thread.Start();
        }

        void Update()
        {
            if ( !IsRunning )
            {
                Current.Remove( this );
                Server.OnServerRulesReceiveFinished( rules, IsSuccessful );
            }              
        }

        private void ThreadedStart( object obj )
        {
            try
            {
                using ( udpClient = new UdpClient() )
                {
                    udpClient.Client.SendTimeout = 3000;
                    udpClient.Client.ReceiveTimeout = 3000;
                    udpClient.Connect( endPoint );

                    GetRules();

                    IsSuccessful = true;
                }
            }
            catch ( System.Exception )
            {
                IsSuccessful = false;
            }

            udpClient = null;
            IsRunning = false;
        }

        void GetRules()
        {
            GetChallengeData();

            _challengeBytes[0] = A2S_RULES;
            Send( _challengeBytes );
            var ruleData = Receive();

            using ( var br = new BinaryReader( new MemoryStream( ruleData ) ) )
            {
                if ( br.ReadByte() != 0x45 )
                    throw new Exception( "Invalid data received in response to A2S_RULES request" );

                var numRules = br.ReadUInt16();
                for ( int index = 0; index < numRules; index++ )
                {
                    rules.Add( br.ReadNullTerminatedUTF8String( readBuffer ), br.ReadNullTerminatedUTF8String( readBuffer ) );
                }
            }

        }

        byte[] readBuffer = new byte[1024 * 4];

        private byte[] Receive()
        {
            byte[][] packets = null;
            byte packetNumber = 0, packetCount = 1;

            do
            {
                var result = udpClient.Receive( ref endPoint );

                using ( var br = new BinaryReader( new MemoryStream( result ) ) )
                {
                    var header = br.ReadInt32();

                    if ( header == -1 )
                    {
                        var unsplitdata = new byte[result.Length - br.BaseStream.Position];
                        Buffer.BlockCopy( result, (int)br.BaseStream.Position, unsplitdata, 0, unsplitdata.Length );
                        return unsplitdata;
                    }
                    else  if ( header == -2 )
                    {
                        int requestId = br.ReadInt32();
                        packetNumber = br.ReadByte();
                        packetCount = br.ReadByte();
                        int splitSize = br.ReadInt32();
                    }
                    else
                    {
                        throw new System.Exception( "Invalid Header" );
                    }

                    if ( packets == null ) packets = new byte[packetCount][];

                    var data = new byte[result.Length - br.BaseStream.Position];
                    Buffer.BlockCopy( result, (int)br.BaseStream.Position, data, 0, data.Length );
                    packets[packetNumber] = data;
                }
            }
            while ( packets.Any( p => p == null ) );

            var combinedData = Combine( packets );
            return combinedData;
        }

        private void GetChallengeData()
        {
            if ( _challengeBytes != null ) return;

            Send( A2S_SERVERQUERY_GETCHALLENGE );

            var challengeData = Receive();

            if ( challengeData[0] != 0x41 )
                throw new Exception( "Invalid Challenge" );

            _challengeBytes = challengeData;
        }

        byte[] sendBuffer = new byte[1024];

        private void Send( byte[] message )
        {
            sendBuffer[0] = 0xFF;
            sendBuffer[1] = 0xFF;
            sendBuffer[2] = 0xFF;
            sendBuffer[3] = 0xFF;

            Buffer.BlockCopy( message, 0, sendBuffer, 4, message.Length );

            udpClient.Send( sendBuffer, message.Length + 4 );
        }

        private byte[] Combine( byte[][] arrays )
        {
            var rv = new byte[arrays.Sum( a => a.Length )];
            int offset = 0;
            foreach ( byte[] array in arrays )
            {
                Buffer.BlockCopy( array, 0, rv, offset, array.Length );
                offset += array.Length;
            }
            return rv;
        }

        public void Dispose()
        {
            if ( thread != null && thread.IsAlive )
            {
                thread.Abort();
            }

            thread = null;
        }
    };
    
}
