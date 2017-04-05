using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO;

#if !NETCORE

internal class SourceServerQuery :IDisposable
{
    public class PlayersResponse
    {
        public short player_count;
        public List<Player> players = new List<Player>();

        public class Player
        {
            public String name { get; set; }
            public int score { get; set; }
            public float playtime { get; set; }
        }
    }

    private IPEndPoint endPoint;

    private Socket socket;
    private UdpClient client;

    // send & receive timeouts
    private int send_timeout = 2500;
    private int receive_timeout = 2500;

    // raw response returned from the server
    private byte[] raw_data;

    private int offset = 0;

    // constants
    private readonly byte[] FFFFFFFF = new byte[] { 0xFF, 0xFF, 0xFF, 0xFF };

    public SourceServerQuery( String ip, int port )
    {
        this.endPoint = new IPEndPoint( IPAddress.Parse( ip ), port );
    }

    /// <summary>
    /// Get a list of currently in-game clients on the specified gameserver.
    /// <b>Please note:</b> the playtime is stored as a float in <i>seconds</i>, you might want to convert it.
    /// 
    /// See https://developer.valvesoftware.com/wiki/Server_queries#A2S_PLAYER for more Information
    /// </summary>
    /// <returns>A PLayersResponse Object containing the name, score and playtime of each player</returns>
    public PlayersResponse GetPlayerList()
    {
        // open socket if not already open
        this.GetSocket();
        // we don't need the header, so set pointer to where the payload begins
        this.offset = 5;

        try
        {
            PlayersResponse pr = new PlayersResponse();

            // since A2S_PLAYER requests require a valid challenge, get it first
            byte[] challenge = this.GetChallenge(0x55, true);

            byte[] request = new byte[challenge.Length + this.FFFFFFFF.Length + 1];
            Array.Copy( this.FFFFFFFF, 0, request, 0, this.FFFFFFFF.Length );
            request[this.FFFFFFFF.Length] = 0x55;
            Array.Copy( challenge, 0, request, this.FFFFFFFF.Length + 1, challenge.Length );

            this.socket.Send( request );

            // MODIFIED BY ZACKBOE
            // Increased byte size from 1024 in order to receive more player data
            // Previously returned a socket exception at >~ 51 players.
            this.raw_data = new byte[2048];
            // END MODIFICATION
            this.socket.Receive( this.raw_data );

            byte player_count = this.ReadByte();

            // fill up the list of players
            for ( int i = 0; i < player_count; i++ )
            {
                this.ReadByte();

                PlayersResponse.Player p = new PlayersResponse.Player();

                p.name = this.ReadString();
                p.score = this.ReadInt32();
                p.playtime = this.ReadFloat();

                pr.players.Add( p );
            }

            pr.player_count = player_count;

            return pr;
        }
        catch ( SocketException )
        {
            return null;
        }
    }

    /// <summary>
    /// Get a list of all publically available CVars ("rules") from the server.
    /// <b>Note:</b> Due to a bug in the Source Engine, it might happen that some CVars/values are cut off.
    /// 
    /// Example: mp_idlemaxtime = [nothing]
    /// Only Valve can fix that.
    /// </summary>
    /// <returns>A RulesResponse Object containing a Name-Value pair of each CVar</returns>
    public Dictionary<string, string> GetRules()
    {
        // open udpclient if not already open
        this.GetClient();

        try
        {
            var d = new Dictionary<string, string>();

            // similar to A2S_PLAYER requests, A2S_RULES require a valid challenge
            byte[] challenge = this.GetChallenge(0x56, false);

            byte[] request = new byte[challenge.Length + this.FFFFFFFF.Length + 1];
            Array.Copy( this.FFFFFFFF, 0, request, 0, this.FFFFFFFF.Length );
            request[this.FFFFFFFF.Length] = 0x56;
            Array.Copy( challenge, 0, request, this.FFFFFFFF.Length + 1, challenge.Length );

            this.client.Send( request, request.Length );

            //
            // Since A2S_RULES responses might be split up into several packages/compressed, we have to do a special handling of them
            //
            int bytesRead;

            // this will keep our assembled message
            byte[] buffer = new byte[4096];

            // send first request

            this.raw_data = this.client.Receive( ref this.endPoint );

            bytesRead = this.raw_data.Length;

            // reset pointer 
            this.offset = 0;

            int is_split = this.ReadInt32();
            int requestid = this.ReadInt32();

            this.offset = 4;

            // response is split up into several packets
            if ( this.PacketIsSplit( is_split ) )
            {
                bool isCompressed = false;
                byte[] splitData;
                int packetCount, packetNumber, requestId;
                int packetsReceived = 1;
                int packetChecksum = 0;
                int packetSplit = 0;
                short splitSize;
                int uncompressedSize = 0;
                List<byte[]> splitPackets = new List<byte[]>();

                do
                {
                    // unique request id 
                    requestId = this.ReverseBytes( this.ReadInt32() );
                    isCompressed = this.PacketIsCompressed( requestId );

                    packetCount = this.ReadByte();
                    packetNumber = this.ReadByte() + 1;
                    // so we know how big our byte arrays have to be
                    splitSize = this.ReadInt16();
                    splitSize -= 4; // fix

                    if ( packetsReceived == 1 )
                    {
                        for ( int i = 0; i < packetCount; i++ )
                        {
                            splitPackets.Add( new byte[] { } );
                        }
                    }

                    // if the packets are compressed, get some data to decompress them
                    if ( isCompressed )
                    {
                        uncompressedSize = ReverseBytes( this.ReadInt32() );
                        packetChecksum = ReverseBytes( this.ReadInt32() );
                    }

                    // ommit header in first packet
                    if ( packetNumber == 1 ) this.ReadInt32();

                    splitData = new byte[splitSize];
                    splitPackets[packetNumber - 1] = this.ReadBytes();

                    // fixes a case where the returned package might still contain a character after the last \0 terminator (truncated name => value)
                    // please note: this therefore also removes the value of said variable, but atleast the program won't crash
                    if ( splitPackets[packetNumber - 1].Length - 1 > 0 && splitPackets[packetNumber - 1][splitPackets[packetNumber - 1].Length - 1] != 0x00 )
                    {
                        splitPackets[packetNumber - 1][splitPackets[packetNumber - 1].Length - 1] = 0x00;
                    }

                    // reset pointer again, so we can copy over the contents
                    this.offset = 0;

                    if ( packetsReceived < packetCount )
                    {

                        this.raw_data = this.client.Receive( ref this.endPoint );
                        bytesRead = this.raw_data.Length;

                        // continue with the next packets
                        packetSplit = this.ReadInt32();
                        packetsReceived++;
                    }
                    else
                    {
                        // all packets received
                        bytesRead = 0;
                    }
                }
                while ( packetsReceived <= packetCount && bytesRead > 0 && packetSplit == -2 );

                // decompress
                if ( isCompressed )
                {
                    buffer = ReassemblePacket( splitPackets, true, uncompressedSize, packetChecksum );
                }
                else
                {
                    buffer = ReassemblePacket( splitPackets, false, 0, 0 );
                }
            }
            else
            {
                buffer = this.raw_data;
            }

            // move our final result over to handle it
            this.raw_data = buffer;

            // omitting header
            this.offset += 1;
            var count = this.ReadInt16();

            for ( int i = 0; i < count; i++ )
            {
                var k = this.ReadString();
                var v = this.ReadString();

                if ( !d.ContainsKey( k ) )
                    d.Add( k, v );
            }

            return d;
        }
        catch ( SocketException e )
        {
            Console.WriteLine( e );
            return null;
        }
    }

    /// <summary>
    /// Close all currently open socket/UdpClient connections
    /// </summary>
    public void Dispose()
    {
        if ( this.socket != null ) this.socket.Close();
        if ( this.client != null ) this.client.Close();
    }

    /// <summary>
    /// Open up a new Socket-based connection to a server, if not already open.
    /// </summary>
    private void GetSocket()
    {
        if ( this.socket == null )
        {
            this.socket = new Socket(
                        AddressFamily.InterNetwork,
                        SocketType.Dgram,
                        ProtocolType.Udp );

            this.socket.SendTimeout = this.send_timeout;
            this.socket.ReceiveTimeout = this.receive_timeout;

            this.socket.Connect( this.endPoint );
        }
    }

    /// <summary>
    /// Create a new UdpClient connection to a server (mostly used for multi-packet answers)
    /// </summary>
    private void GetClient()
    {
        if ( this.client == null )
        {
            this.client = new UdpClient();
            this.client.Connect( this.endPoint );
            this.client.DontFragment = true;

            this.client.Client.SendTimeout = this.send_timeout;
            this.client.Client.ReceiveTimeout = this.receive_timeout;
        }
    }

    /// <summary>
    /// Reassmble a multi-packet response.
    /// </summary>
    /// <param name="splitPackets">The packets to assemble</param>
    /// <param name="isCompressed">true: packets are compressed; false: not</param>
    /// <param name="uncompressedSize">The size of the message after decompression (for comparison)</param>
    /// <param name="packetChecksum">Validation of the result</param>
    /// <returns>A byte-array containing all packets assembled together/decompressed.</returns>
    private byte[] ReassemblePacket( List<byte[]> splitPackets, bool isCompressed, int uncompressedSize, int packetChecksum )
    {
        byte[] packetData, tmpData;
        packetData = new byte[0];

        foreach ( byte[] splitPacket in splitPackets )
        {
            if ( splitPacket == null )
            {
                throw new Exception();
            }

            tmpData = packetData;
            packetData = new byte[tmpData.Length + splitPacket.Length];

            MemoryStream memStream = new MemoryStream(packetData);
            memStream.Write( tmpData, 0, tmpData.Length );
            memStream.Write( splitPacket, 0, splitPacket.Length );
        }

        if ( isCompressed )
        {
            throw new System.NotImplementedException();
        }

        return packetData;
    }

    /// <summary>
    /// Invert the Byte-order Mark of an value, used for compatibility between Little Large BOM
    /// </summary>
    /// <param name="value">The value to invert</param>
    /// <returns>BOM-inversed value (if needed), otherwise the original value</returns>
    private int ReverseBytes( int value )
    {
        byte[] bytes = BitConverter.GetBytes(value);
        if ( BitConverter.IsLittleEndian )
        {
            Array.Reverse( bytes );
        }
        return BitConverter.ToInt32( bytes, 0 );
    }

    /// <summary>
    /// Determine whetever or not a message is compressed.
    /// Simply detects if the most significant bit is 1.
    /// </summary>
    /// <param name="value">The value to check</param>
    /// <returns>true, if message is compressed, false otherwise</returns>
    private bool PacketIsCompressed( int value )
    {
        return ( value & 0x8000 ) != 0;
    }

    /// <summary>
    /// Determine whetever or not a message is split up.
    /// </summary>
    /// <param name="paket">The value to check</param>
    /// <returns>true, if message is split up, false otherwise</returns>
    private bool PacketIsSplit( int paket )
    {
        return ( paket == -2 );
    }

    /// <summary>
    /// Request the 4-byte challenge id from the server, required for A2S_RULES and A2S_PLAYER.
    /// </summary>
    /// <param name="type">The type of message to request the challenge for (see constants)</param>
    /// <param name="socket">Request method to use (performance reasons)</param>
    /// <returns>A Byte Array (4-bytes) containing the challenge</returns>
    private Byte[] GetChallenge( byte type, bool socket = true )
    {
        byte[] request = new byte[this.FFFFFFFF.Length + this.FFFFFFFF.Length + 1];
        Array.Copy( this.FFFFFFFF, 0, request, 0, this.FFFFFFFF.Length );
        request[FFFFFFFF.Length] = type;
        Array.Copy( this.FFFFFFFF, 0, request, this.FFFFFFFF.Length + 1, this.FFFFFFFF.Length );

        byte[] raw_response = new byte[24];
        byte[] challenge = new byte[4];

        // using sockets
        if ( socket )
        {
            this.socket.Send( request );
            this.socket.Receive( raw_response );
        }
        else
        {
            this.client.Send( request, request.Length );
            raw_response = this.client.Receive( ref this.endPoint );
        }

        Array.Copy( raw_response, 5, challenge, 0, 4 ); // change this valve modifies the protocol!

        return challenge;
    }

    /// <summary>
    /// Read a single byte value from our raw data.
    /// </summary>
    /// <returns>A single Byte at the next Offset Address</returns>
    private Byte ReadByte()
    {
        byte[] b = new byte[1];
        Array.Copy( this.raw_data, this.offset, b, 0, 1 );

        this.offset++;
        return b[0];
    }

    /// <summary>
    /// Read all remaining Bytes from our raw data.
    /// Used for multi-packet responses.
    /// </summary>
    /// <returns>All remaining data</returns>
    private Byte[] ReadBytes()
    {
        int size = (this.raw_data.Length - this.offset - 4);
        if ( size < 1 ) return new Byte[] { };

        byte[] b = new byte[size];
        Array.Copy( this.raw_data, this.offset, b, 0, this.raw_data.Length - this.offset - 4 );

        this.offset += ( this.raw_data.Length - this.offset - 4 );
        return b;
    }

    /// <summary>
    /// Read a 32-Bit Integer value from the next offset address.
    /// </summary>
    /// <returns>The Int32 Value found at the offset address</returns>
    private Int32 ReadInt32()
    {
        byte[] b = new byte[4];
        Array.Copy( this.raw_data, this.offset, b, 0, 4 );

        this.offset += 4;
        return BitConverter.ToInt32( b, 0 );
    }

    /// <summary>
    /// Read a 16-Bit Integer (also called "short") value from the next offset address.
    /// </summary>
    /// <returns>The Int16 Value found at the offset address</returns>
    private Int16 ReadInt16()
    {
        byte[] b = new byte[2];
        Array.Copy( this.raw_data, this.offset, b, 0, 2 );

        this.offset += 2;
        return BitConverter.ToInt16( b, 0 );
    }

    /// <summary>
    /// Read a Float value from the next offset address.
    /// </summary>
    /// <returns>The Float Value found at the offset address</returns>
    private float ReadFloat()
    {
        byte[] b = new byte[4];
        Array.Copy( this.raw_data, this.offset, b, 0, 4 );

        this.offset += 4;
        return BitConverter.ToSingle( b, 0 );
    }

    /// <summary>
    /// Read a String until its end starting from the next offset address.
    /// Reading stops once the method detects a 0x00 Character at the next position (\0 terminator)
    /// </summary>
    /// <returns>The String read</returns>
    private String ReadString()
    {
        byte[] cache = new byte[1] { 0x01 };
        String output = "";

        while ( cache[0] != 0x00 )
        {
            if ( this.offset == this.raw_data.Length ) break; // fixes Valve's inability to code a proper query protocol
            Array.Copy( this.raw_data, this.offset, cache, 0, 1 );
            this.offset++;

            if ( cache[0] != 0x00)
                output += Encoding.UTF8.GetString( cache );
        }

        return output;
    }
}

#endif