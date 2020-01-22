using System;
using System.Collections.Generic;

namespace Steamworks.Data
{
	public struct Connection
	{
		internal uint Id;

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
				if ( !SteamNetworkingSockets.Internal.GetConnectionName( this, out var strVal ) )
					return "ERROR";

				return strVal;
			}

			set => SteamNetworkingSockets.Internal.SetConnectionName( this, value );
		}


		public Result SendMessage( IntPtr ptr, int size, SendType sendType = SendType.Reliable )
		{
			return SteamNetworkingSockets.Internal.SendMessageToConnection( this, ptr, (uint) size, (int)sendType );
		}

		public unsafe Result SendMessage( byte[] data, SendType sendType = SendType.Reliable )
		{
			fixed ( byte* ptr = data )
			{
				return SendMessage( (IntPtr)ptr, data.Length, sendType );
			}
		}

		public unsafe Result SendMessage( byte[] data, int offset, int length, SendType sendType = SendType.Reliable )
		{
			fixed ( byte* ptr = data )
			{
				return SendMessage( (IntPtr)ptr + offset, length, sendType );
			}
		}

		public unsafe Result SendMessage( string str, SendType sendType = SendType.Reliable )
		{
			var bytes = System.Text.Encoding.UTF8.GetBytes( str );
			return SendMessage( bytes, sendType );
		}

		/// <summary>
		/// Flush any messages waiting on the Nagle timer and send them at the next transmission opportunity (often that means right now).
		/// </summary>
		public Result Flush() => SteamNetworkingSockets.Internal.FlushMessagesOnConnection( this );

		public string DetailedStatus()
		{
			if ( SteamNetworkingSockets.Internal.GetDetailedConnectionStatus( this, out var strVal ) != 0 )
				return null;

			return strVal;
		}

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