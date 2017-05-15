using System;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Facepunch.Steamworks.Test
{
    [TestClass]
    [DeploymentItem( "steam_api.dll" )]
    [DeploymentItem( "steam_api64.dll" )]
    [DeploymentItem( "steam_appid.txt" )]
    public class RemoteStorage
    {
        [TestMethod]
        public void GetQuota()
        {
            using ( var client = new Steamworks.Client( 252490 ) )
            {
                ulong total, available;
                client.RemoteStorage.GetQuota( out total, out available );

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
                    Console.WriteLine( $"- {file.FileName} ({file.SizeInBytes} bytes)" );
                }
            }
        }
    }
}
