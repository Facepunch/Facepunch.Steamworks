using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Facepunch.Steamworks
{
    public class Voice
    {
        const int ReadBufferSize = 1024 * 128;

        internal Client client;

        internal byte[] ReadCompressedBuffer = new byte[ReadBufferSize];
        internal byte[] ReadUncompressedBuffer = new byte[ReadBufferSize];

        internal byte[] UncompressBuffer = new byte[1024 * 256];

        public Action<byte[], int> OnCompressedData;
        public Action<byte[], int> OnUncompressedData;

        private System.Diagnostics.Stopwatch UpdateTimer = System.Diagnostics.Stopwatch.StartNew();

        /// <summary>
        /// Returns the optimal sample rate for voice - according to Steam
        /// </summary>
        public uint OptimalSampleRate
        {
            get { return client.native.user.GetVoiceOptimalSampleRate(); }
        }

        private bool _wantsrecording = false;


        /// <summary>
        /// If set to true we are listening to the mic. 
        /// You should usually toggle this with the press of a key for push to talk.
        /// </summary>
        public bool WantsRecording
        {
            get { return _wantsrecording; }
            set
            {
                _wantsrecording = value;

                if ( value )
                {
                    client.native.user.StartVoiceRecording();
                }
                else
                {
                    client.native.user.StopVoiceRecording();
                }
            }
        }

        /// <summary>
        /// The last time voice was detected, recorded
        /// </summary>
        public DateTime LastVoiceRecordTime { get; private set; }

        public TimeSpan TimeSinceLastVoiceRecord { get { return DateTime.Now.Subtract( LastVoiceRecordTime ); } }

        public bool IsRecording = false;

        /// <summary>
        /// If set we will capture the audio at this rate. If unset (set to 0) will capture at OptimalSampleRate
        /// </summary>
        public uint DesiredSampleRate = 0;

        internal Voice( Client client )
        {
            this.client = client;
        }

        /// <summary>
        /// This gets called inside Update - so there's no need to call this manually if you're calling update
        /// </summary>
        public unsafe void Update()
        {
            if ( OnCompressedData == null && OnUncompressedData == null )
                return;

            if ( UpdateTimer.Elapsed.TotalSeconds < 1.0f / 10.0f )
                return;

            UpdateTimer.Reset();
            UpdateTimer.Start();

            uint bufferRegularLastWrite = 0;
            uint bufferCompressedLastWrite = 0;

            var result = client.native.user.GetAvailableVoice( out bufferCompressedLastWrite, out bufferRegularLastWrite, DesiredSampleRate == 0 ? OptimalSampleRate : DesiredSampleRate );

            if ( result == SteamNative.VoiceResult.NotRecording || result == SteamNative.VoiceResult.NotInitialized )
            {
                IsRecording = false;
                return;
            }

            fixed (byte* compressedPtr = ReadCompressedBuffer)
            fixed (byte* uncompressedPtr = ReadUncompressedBuffer)
            {
                result = client.native.user.GetVoice( OnCompressedData != null, (IntPtr) compressedPtr, ReadBufferSize, out bufferCompressedLastWrite,
                                                        OnUncompressedData != null, (IntPtr) uncompressedPtr, ReadBufferSize, out bufferRegularLastWrite, 
                                                        DesiredSampleRate == 0 ? OptimalSampleRate : DesiredSampleRate );
            }

            IsRecording = true;

            if ( result == SteamNative.VoiceResult.OK )
            {
                if ( OnCompressedData != null && bufferCompressedLastWrite > 0 )
                {
                    OnCompressedData( ReadCompressedBuffer, (int)bufferCompressedLastWrite );
                }

                if ( OnUncompressedData != null && bufferRegularLastWrite > 0 )
                {
                    OnUncompressedData( ReadUncompressedBuffer, (int)bufferRegularLastWrite );
                }

                LastVoiceRecordTime = DateTime.Now;
            }

            if ( result == SteamNative.VoiceResult.NotRecording ||
                result == SteamNative.VoiceResult.NotInitialized )
                IsRecording = false;
            
        }

        public bool Decompress( byte[] input, MemoryStream output, uint samepleRate = 0 )
        {
            return Decompress( input, 0, input.Length, output, samepleRate );
        }

        public bool Decompress( byte[] input, int inputsize, MemoryStream output, uint samepleRate = 0 )
        {
            return Decompress( input, 0, inputsize, output, samepleRate );
        }

        public unsafe bool Decompress( byte[] input, int inputoffset, int inputsize, MemoryStream output, uint samepleRate = 0 )
        {
            if ( inputoffset < 0 || inputoffset >= input.Length )
                throw new ArgumentOutOfRangeException( "inputoffset" );

            if ( inputsize <= 0 || inputoffset + inputsize > input.Length )
                throw new ArgumentOutOfRangeException( "inputsize" );

            fixed ( byte* p = input )
            {
                return Decompress( (IntPtr)p, inputoffset, inputsize, output, samepleRate );
            }
        }

        private unsafe bool Decompress( IntPtr input, int inputoffset, int inputsize, MemoryStream output, uint samepleRate = 0 )
        {
            if ( samepleRate == 0 )
                samepleRate = OptimalSampleRate;

            uint bytesOut = 0;

            SteamNative.VoiceResult result = SteamNative.VoiceResult.NoData;

            fixed ( byte* outBuf = UncompressBuffer )
            {
                result = client.native.user.DecompressVoice( (IntPtr)(((byte*)input) + inputoffset), (uint)inputsize, (IntPtr)outBuf, (uint)UncompressBuffer.Length, out bytesOut, samepleRate );
            }

            if ( result == SteamNative.VoiceResult.OK )
            {
                output.Write( (byte[])UncompressBuffer, 0, (int)bytesOut );

                return true;
            }

            return false;
        }
    }
}
