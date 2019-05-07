using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using Steamworks.Data;

namespace Steamworks
{
	internal static class SourceServerQuery
	{
		private static readonly byte[] A2S_SERVERQUERY_GETCHALLENGE = { 0x55, 0xFF, 0xFF, 0xFF, 0xFF };
		//      private static readonly byte A2S_PLAYER = 0x55;
		private static readonly byte A2S_RULES = 0x56;

		internal static async Task<Dictionary<string, string>> GetRules( ServerInfo server )
		{
			try
			{
				var endpoint = new IPEndPoint( server.Address, server.QueryPort );

				using ( var client = new UdpClient() )
				{
					client.Client.SendTimeout = 3000;
					client.Client.ReceiveTimeout = 3000;
					client.Connect( endpoint );

					return await GetRules( client );
				}
			}
			catch ( System.Exception e )
			{
				Console.Error.WriteLine( e.Message );
				return null;
			}
		}

		static async Task<Dictionary<string, string>> GetRules( UdpClient client )
		{
			var challengeBytes = await GetChallengeData( client );
			challengeBytes[0] = A2S_RULES;
			await Send( client, challengeBytes );
			var ruleData = await Receive( client );

			var rules = new Dictionary<string, string>();

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

			return rules;
		}

		static byte[] readBuffer = new byte[1024 * 8];

		static async Task<byte[]> Receive( UdpClient client )
		{
			byte[][] packets = null;
			byte packetNumber = 0, packetCount = 1;

			do
			{
				var result = await client.ReceiveAsync();
				var buffer = result.Buffer;

				using ( var br = new BinaryReader( new MemoryStream( buffer ) ) )
				{
					var header = br.ReadInt32();

					if ( header == -1 )
					{
						var unsplitdata = new byte[buffer.Length - br.BaseStream.Position];
						Buffer.BlockCopy( buffer, (int)br.BaseStream.Position, unsplitdata, 0, unsplitdata.Length );
						return unsplitdata;
					}
					else if ( header == -2 )
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

					var data = new byte[buffer.Length - br.BaseStream.Position];
					Buffer.BlockCopy( buffer, (int)br.BaseStream.Position, data, 0, data.Length );
					packets[packetNumber] = data;
				}
			}
			while ( packets.Any( p => p == null ) );

			var combinedData = Combine( packets );
			return combinedData;
		}

		private static async Task<byte[]> GetChallengeData( UdpClient client )
		{
			await Send( client, A2S_SERVERQUERY_GETCHALLENGE );

			var challengeData = await Receive( client );

			if ( challengeData[0] != 0x41 )
				throw new Exception( "Invalid Challenge" );

			return challengeData;
		}

		static byte[] sendBuffer = new byte[1024];

		static async Task Send( UdpClient client, byte[] message )
		{
			sendBuffer[0] = 0xFF;
			sendBuffer[1] = 0xFF;
			sendBuffer[2] = 0xFF;
			sendBuffer[3] = 0xFF;

			Buffer.BlockCopy( message, 0, sendBuffer, 4, message.Length );

			await client.SendAsync( sendBuffer, message.Length + 4 );
		}

		static byte[] Combine( byte[][] arrays )
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
	};

}
