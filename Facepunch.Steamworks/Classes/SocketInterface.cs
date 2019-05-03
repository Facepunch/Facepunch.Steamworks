using System.Collections.Generic;
using Steamworks.Data;

namespace Steamworks
{
	public class SocketInterface
	{
		public List<NetConnection> Connecting = new List<NetConnection>();
		public List<NetConnection> Connected = new List<NetConnection>();
		public Socket Socket { get; internal set; }

		public bool Close() => Socket.Close();

		public override string ToString() => Socket.ToString();

		public virtual void OnConnectionChanged( NetConnection connection, ConnectionInfo data )
		{
			switch ( data.State )
			{
				case ConnectionState.Connecting:
					OnConnecting( connection, data );
					break;
				case ConnectionState.Connected:
					OnConnected( connection, data );
					break;
				case ConnectionState.ClosedByPeer:
				case ConnectionState.ProblemDetectedLocally:
					OnDisconnected( connection, data );
					break;
			}
		}

		/// <summary>
		/// Default behaviour is to accept every connection
		/// </summary>
		public virtual void OnConnecting( NetConnection connection, ConnectionInfo data )
		{
			connection.Accept();
			Connecting.Add( connection );
		}

		/// <summary>
		/// Client is connected. They move from connecting to Connections
		/// </summary>
		public virtual void OnConnected( NetConnection connection, ConnectionInfo data )
		{
			Connecting.Remove( connection );
			Connected.Add( connection );
		}

		/// <summary>
		/// The connection has been closed remotely or disconnected locally. Check data.State for details.
		/// </summary>
		public virtual void OnDisconnected( NetConnection connection, ConnectionInfo data )
		{
			connection.Close();

			Connecting.Remove( connection );
			Connected.Remove( connection );
		}
	}
}