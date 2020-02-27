using System;
using Steamworks.Data;

namespace Steamworks
{
	public interface IConnectionManager
	{
		/// <summary>
		/// We started connecting to this guy
		/// </summary>
		void OnConnecting( ConnectionInfo info );

		/// <summary>
		/// Called when the connection is fully connected and can start being communicated with
		/// </summary>
		void OnConnected( ConnectionInfo info );			
			
		/// <summary>
		/// We got disconnected
		/// </summary>
		void OnDisconnected( ConnectionInfo info );

		/// <summary>
		/// Received a message
		/// </summary>
		void OnMessage( IntPtr data, int size, long messageNum, long recvTime, int channel );
	}
}