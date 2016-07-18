using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Facepunch.Steamworks
{
    public partial class Client : IDisposable
    {
        private Voice _voice;

        public Voice Voice
        {
            get
            {
                if ( _voice == null )
                    _voice = new Voice { client = this };

                return _voice;
            }
        }
    }

    public class Voice
    {
        internal Client client;
        internal byte[] bufferRegular = new byte[ 1024 * 8 ];
        internal uint bufferRegularLastWrite = 0;

        internal byte[] bufferCompressed = new byte[ 1024 * 8 ];
        internal uint bufferCompressedLastWrite = 0;

        public Action<byte[]> OnCompressedData;
        public Action<byte[]> OnUncompressedData;


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


        public bool IsRecording = false;

        /// <summary>
        /// If set we will capture the audio at this rate. If unset (set to 0) will capture at OptimalSampleRate
        /// </summary>
        public uint DesiredSampleRate = 0;

        internal unsafe void Update()
        {
            if ( OnCompressedData == null && OnUncompressedData == null )
                return;

            fixed ( byte* pbufferRegular = bufferRegular )
            fixed ( byte* pbufferCompressed = bufferCompressed )
            {
                bufferRegularLastWrite = 0;
                bufferCompressedLastWrite = 0;

                Valve.Steamworks.EVoiceResult result = (Valve.Steamworks.EVoiceResult) client.native.user.GetVoice(     OnUncompressedData != null, (IntPtr) pbufferCompressed, (uint) bufferCompressed.Length, ref bufferCompressedLastWrite,
                                                        OnCompressedData != null, (IntPtr) pbufferRegular, (uint) bufferRegular.Length, ref bufferRegularLastWrite, 
                                                        DesiredSampleRate == 0 ? OptimalSampleRate : DesiredSampleRate );

                IsRecording = true;

                if ( result == Valve.Steamworks.EVoiceResult.k_EVoiceResultOK )
                {
                    if ( OnCompressedData != null && bufferCompressedLastWrite > 0 )
                    {
                        OnCompressedData( bufferRegular.Take( (int)bufferCompressedLastWrite ).ToArray() );
                    }

                    if ( OnUncompressedData != null && bufferRegularLastWrite > 0 )
                    {
                        OnUncompressedData( bufferRegular.Take( (int)bufferRegularLastWrite ).ToArray() );
                    }
                }

                if ( result == Valve.Steamworks.EVoiceResult.k_EVoiceResultNotRecording ||
                    result == Valve.Steamworks.EVoiceResult.k_EVoiceResultNotInitialized )
                    IsRecording = false;
            }
        }

        public unsafe bool Decompress( byte[] input, int inputoffset, int inputsize, MemoryStream output, uint samepleRate = 0 )
        {
            if ( samepleRate == 0 )
                samepleRate = OptimalSampleRate;

            //
            // Guessing the uncompressed size cuz we're dicks
            //
            {
                var targetBufferSize = inputsize * 10;

                if ( output.Capacity < targetBufferSize )
                    output.Capacity = targetBufferSize;

                output.SetLength( targetBufferSize );
            }

            fixed ( byte* pout = output.GetBuffer() )
            fixed ( byte* p = input )
            {
                uint bytesOut = 0;
                var result = (Valve.Steamworks.EVoiceResult) client.native.user.DecompressVoice( (IntPtr)( p + inputoffset ), (uint) inputsize, (IntPtr) pout, (uint) output.Length, ref bytesOut, (uint)samepleRate );

                if ( bytesOut > 0 )
                    output.SetLength( bytesOut );

                return result == Valve.Steamworks.EVoiceResult.k_EVoiceResultOK;
            }
        }
    }
}
