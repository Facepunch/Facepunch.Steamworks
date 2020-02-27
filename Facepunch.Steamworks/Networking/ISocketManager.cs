using System;
using Steamworks.Data;

namespace Steamworks
{
	public interface ISocketManager
	{
		/// <summary>
		/// Must call Accept or Close on the connection within a second or so
		/// </summary>
		void OnConnecting( Connection connection, ConnectionInfo info );

		/// <summary>
		/// Called when the connection is fully connected and can start being communicated with
		/// </summary>
		void OnConnected( Connection connection, ConnectionInfo info );			
			
		/// <summary>
		/// Called when the connection leaves
		/// </summary>
		void OnDisconnected( Connection connection, ConnectionInfo info );

		/// <summary>
		/// Received a message from a connection
		/// </summary>
		void OnMessage( Connection connection, NetIdentity identity, IntPtr data, int size, long messageNum, long recvTime, int channel );
	}
}