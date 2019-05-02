using System;
using System.Collections.Generic;

namespace Steamworks.Data
{
	public struct NetConnection
	{
		uint Id;

		public static implicit operator NetConnection( uint value )
		{
			return new NetConnection { Id = value };
		}

		public static implicit operator uint( NetConnection value )
		{
			return value.Id;
		}

		public override string ToString() => Id.ToString();

		/// <summary>
		/// Accept an incoming connection that has been received on a listen socket.
		/// </summary>
		public Result Accept()
		{
			return SteamNetworkingSockets.Internal.AcceptConnection( this );
		}

		/// <summary>
		/// Disconnects from the remote host and invalidates the connection handle. Any unread data on the connection is discarded..
		/// reasonCode is defined and used by you.
		/// </summary>
		public bool Close( bool linger = false, int reasonCode = 0, string debugString = "Closing Connection" )
		{
			return SteamNetworkingSockets.Internal.CloseConnection( this, reasonCode, debugString, linger );
		}

		/// <summary>
		/// Get/Set connection user data
		/// </summary>
		public long UserData
		{
			get => SteamNetworkingSockets.Internal.GetConnectionUserData( this );
			set => SteamNetworkingSockets.Internal.SetConnectionUserData( this, value );
		}

		/// <summary>
		/// A name for the connection, used mostly for debugging
		/// </summary>
		public string ConnectionName
		{
			get
			{
				var sb = Helpers.TakeStringBuilder();
				if ( !SteamNetworkingSockets.Internal.GetConnectionName( this, sb, sb.Capacity ) )
					return "ERROR";

				return sb.ToString();
			}

			set => SteamNetworkingSockets.Internal.SetConnectionName( this, value );
		}

		/// <summary>
		/// Flush any messages waiting on the Nagle timer and send them at the next transmission opportunity (often that means right now).
		/// </summary>
		public Result Flush() => SteamNetworkingSockets.Internal.FlushMessagesOnConnection( this );

		/*
		[ThreadStatic]
		private static SteamNetworkingMessage_t[] messageBuffer;

		public IEnumerable<SteamNetworkingMessage_t> Messages
		{
			get
			{
				if ( messageBuffer == null )
					messageBuffer = new SteamNetworkingMessage_t[128];

				var num = SteamNetworkingSockets.Internal.ReceiveMessagesOnConnection( this, ref messageBuffer, messageBuffer.Length );

				for ( int i = 0; i < num; i++)
				{
					yield return messageBuffer[i];
					messageBuffer[i].Release();
				}
			}
		}*/

	}
}