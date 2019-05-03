using System;
using System.Collections.Generic;
using System.IO;
using Steamworks.Data;

namespace Steamworks
{
	public class ConnectionInterface
	{
		public NetConnection Connection;
		public bool Connected = false;

		public string ConnectionName
		{
			get => Connection.ConnectionName;
			set => Connection.ConnectionName = value;
		}

		public long UserData 
		{
			get => Connection.UserData;
			set => Connection.UserData = value;
		}

		public void Close() => Connection.Close();

		public override string ToString() => Connection.ToString();

		public virtual void OnConnectionChanged( ConnectionInfo data )
		{
			switch ( data.State )
			{
				case ConnectionState.Connecting:
					OnConnecting( data );
					break;
				case ConnectionState.Connected:
					OnConnected( data );
					break;
				case ConnectionState.ClosedByPeer:
				case ConnectionState.ProblemDetectedLocally:
					OnDisconnected( data );
					break;
			}
		}

		/// <summary>
		/// We're trying to connect!
		/// </summary>
		public virtual void OnConnecting( ConnectionInfo data )
		{

		}

		/// <summary>
		/// Client is connected. They move from connecting to Connections
		/// </summary>
		public virtual void OnConnected( ConnectionInfo data )
		{
			Connected = true;
		}

		/// <summary>
		/// The connection has been closed remotely or disconnected locally. Check data.State for details.
		/// </summary>
		public virtual void OnDisconnected( ConnectionInfo data )
		{
			Connected = false;
		}

		SteamNetworkingMessage_t[] messageBuffer;

		public void Receive()
		{
			if ( messageBuffer == null )
			{
				messageBuffer = new SteamNetworkingMessage_t[128];
			}

			var processed = SteamNetworkingSockets.Internal.ReceiveMessagesOnConnection( Connection, ref messageBuffer, messageBuffer.Length );

			for ( int i=0; i< processed; i++ )
			{
				Console.WriteLine( "FOUND SOME!" );
				ReceiveMessage( messageBuffer[i] );
			}

			//
			// Overwhelmed our buffer, keep going
			//
			if ( processed == messageBuffer.Length )
				Receive();
		}

		internal unsafe void ReceiveMessage( SteamNetworkingMessage_t msg )
		{
			var stream = new UnmanagedMemoryStream( (byte*)msg.data, msg.length, msg.length, FileAccess.Read );

			// read Message

			msg.release.Invoke( ref msg );
		}
	}
}