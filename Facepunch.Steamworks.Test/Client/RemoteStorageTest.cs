using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Facepunch.Steamworks.Test
{
    [TestClass]
    [DeploymentItem( "steam_api.dll" )]
    [DeploymentItem( "steam_api64.dll" )]
    public class RemoteStorage
    {
        [TestMethod]
        public void GetQuota()
        {
            using ( var client = new Steamworks.Client( 252490 ) )
            {
                ulong total = client.RemoteStorage.QuotaTotal;
                var available = client.RemoteStorage.QuotaRemaining;

                Console.WriteLine( $"Total quota: {total} bytes" );
                Console.WriteLine( $"Available: {available} bytes" );
            }
        }
        
        [TestMethod]
        public void WriteFile()
        {
            using ( var client = new Steamworks.Client( 252490 ) )
            {
                var file = client.RemoteStorage.CreateFile( "test.txt" );

                const string text = "Hello world!";

                file.WriteAllText( text );

                Assert.IsTrue( file.Exists );

                var read = file.ReadAllText();
                Assert.AreEqual( text, read );
            }
        }

        [TestMethod]
        public void ReadText()
        {
            using ( var client = new Steamworks.Client( 252490 ) )
            {
                var text = client.RemoteStorage.ReadString( "test.txt" );

                Assert.IsNotNull( text );
                Assert.AreEqual( text, "Hello world!" );
            }
        }

        [TestMethod]
        public void WriteText()
        {
            using ( var client = new Steamworks.Client( 252490 ) )
            {
                var result = client.RemoteStorage.WriteString( "test.txt", "Hello world!" );
                Assert.IsTrue( result );
            }
        }

        [TestMethod]
        public void WriteFiles()
        {
            using ( var client = new Steamworks.Client( 252490 ) )
            {
                for ( var i = 0; i < 10; ++i )
                {
                    client.RemoteStorage
                        .CreateFile( $"test_{i}/example.txt" )
                        .WriteAllText( Guid.NewGuid().ToString() );
                }

                Console.WriteLine( $"File count: {client.RemoteStorage.FileCount}" );

                foreach ( var file in client.RemoteStorage.Files )
                {
                    DateTime t = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc).AddSeconds(file.FileTimestamp);
                    Console.WriteLine( $"- {file.FileName} ({file.SizeInBytes} bytes), modified {t:O}" );
                }
            }
        }
    }
}
