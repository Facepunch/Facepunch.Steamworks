using System;
using System.Diagnostics;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Facepunch.Steamworks.Test
{
    [DeploymentItem( "steam_api.dll" )]
    [DeploymentItem( "steam_api64.dll" )]
    [TestClass]
    public class Voice
    {
        static readonly MemoryStream decompressStream = new MemoryStream();

        [TestMethod]
        public void GetVoice()
        {
            using ( var client = new Facepunch.Steamworks.Client( 252490 ) )
            {
                int unCompressed = 0;
                int compressed = 0;

                client.Voice.OnCompressedData = ( buffer, length ) =>
                {
                    compressed += length;

                    if ( !client.Voice.Decompress( buffer, length, decompressStream ) )
                    {
                        Assert.Fail( "Decompress returned false" );
                    }
                };

                client.Voice.OnUncompressedData = ( buffer, length ) =>
                {
                    unCompressed += length;
                };

                client.Voice.WantsRecording = true;

                var sw = Stopwatch.StartNew();

                while ( sw.Elapsed.TotalSeconds < 3 )
                {
                    client.Update();
                    System.Threading.Thread.Sleep( 10 );
                }

                Assert.AreNotEqual( unCompressed, 0 );
                Assert.AreNotEqual( compressed, 0 );

                // Should really be > 0 if the mic was getting audio
                Console.WriteLine( "unCompressed: {0}", unCompressed );
                Console.WriteLine( "compressed: {0}", compressed );
            }
        }

        [TestMethod]
        public void CompressedOnly()
        {
            using ( var client = new Facepunch.Steamworks.Client( 252490 ) )
            {
                int compressed = 0;

                client.Voice.OnCompressedData = ( buffer, length ) =>
                {
                    compressed += length;
                };

                client.Voice.WantsRecording = true;

                var sw = Stopwatch.StartNew();

                while ( sw.Elapsed.TotalSeconds < 3 )
                {
                    client.Update();
                    System.Threading.Thread.Sleep( 10 );
                }

                Assert.AreNotEqual( compressed, 0 );
                Console.WriteLine( "compressed: {0}", compressed );
            }
        }

        [TestMethod]
        public void UnCompressedOnly()
        {
            using ( var client = new Facepunch.Steamworks.Client( 252490 ) )
            {
                int unCompressed = 0;

                client.Voice.OnUncompressedData = ( buffer, length ) =>
                {
                    unCompressed += length;
                };

                client.Voice.WantsRecording = true;

                var sw = Stopwatch.StartNew();

                while ( sw.Elapsed.TotalSeconds < 3 )
                {
                    client.Update();
                    System.Threading.Thread.Sleep( 10 );
                }

                Assert.AreNotEqual( unCompressed, 0 );

                // Should really be > 0 if the mic was getting audio
                Console.WriteLine( "unCompressed: {0}", unCompressed );

            }
        }

        [TestMethod]
        public void OptimalSampleRate()
        {
            using ( var client = new Facepunch.Steamworks.Client( 252490 ) )
            {
                var rate = client.Voice.OptimalSampleRate;
                Assert.AreNotEqual( rate, 0 );
            }
        }
    }
}
