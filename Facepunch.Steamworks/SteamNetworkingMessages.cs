using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Steamworks.Data;

namespace Steamworks
{
	public class SteamNetworkingMessages : SteamSharedClass<SteamNetworkingMessages>
	{
		internal static ISteamNetworkingMessages Internal => Interface as ISteamNetworkingMessages;

		internal override bool InitializeInterface( bool server )
		{
			SetInterface( server, new ISteamNetworkingMessages( server ) );
			if ( Interface.Self == IntPtr.Zero ) return false;

			InstallEvents( server );

			return true;
		}

		internal static void InstallEvents( bool server )
		{
			Dispatch.Install<SteamNetworkingMessagesSessionRequest_t>( x => OnSessionRequest?.Invoke( x.DentityRemote ), server );
			Dispatch.Install<SteamNetworkingMessagesSessionFailed_t>( x => OnSessionFailed?.Invoke( x.Nfo ), server );
		}

		/// <summary>
		/// Invoked when a <see cref="NetIdentity"/> wants to connect to the current user. You should respond by calling <see cref="AcceptSessionWithUser(ref NetIdentity)"/>
		/// if you want to recieve their messages.
		/// </summary>
		public static event Action<NetIdentity> OnSessionRequest;

		/// <summary>
		/// Invoked when a session fails to connect can't get through to the specified user.
		/// All queued packets unsent at this point will be dropped, further attempts
		/// to send will retry making the connection (but will be dropped if we fail again).
		/// </summary>
		public static event Action<ConnectionInfo> OnSessionFailed;

		/// <summary>
		/// Invoked when a <see cref="NetIdentity"/> has sent a message to the current user.
		/// </summary>
		public static event Action<NetIdentity, IntPtr, int, int> OnMessage;

		/// <summary>
		/// This should be called in response to a <see cref="OnSessionRequest"/>.
		/// </summary>
		public static bool AcceptSessionWithUser( ref NetIdentity netIdentity ) => Internal.AcceptSessionWithUser( ref netIdentity );

		/// <summary>
		/// This should be called when you're done communicating with a user, as this will
		/// free up all of the resources allocated for the connection under-the-hood.
		/// If the remote user tries to send data to you again, a new <see cref="OnSessionRequest"/> 
		/// callback will be posted
		/// </summary>
		public static bool CloseSessionWithUser( ref NetIdentity netIdentity ) => Internal.CloseSessionWithUser( ref netIdentity );

		/// <summary>
		/// This should be called when you're done communicating with a user on a specific channel, as this will
		/// free up all of the resources allocated for the channel connection under-the-hood.
		/// </summary>
		public static bool CloseChannelWithUser( ref NetIdentity netIdentity, int channel ) => Internal.CloseChannelWithUser( ref netIdentity, channel );

		public static unsafe Result SendMessageToUser( ref NetIdentity netIdentity, byte[] data, int length = -1, int nRemoteChannel = 0, SendType sendType = SendType.Reliable )
		{
			if ( length <= 0 )
				length = data.Length;

			fixed ( byte* ptr = data )
			{
				var result = Internal.SendMessageToUser( ref netIdentity, [(IntPtr)ptr], (uint)length, (int)sendType, nRemoteChannel );

				if ( result != Result.OK )
				{
					var info = new ConnectionInfo();

					var status = new ConnectionStatus();

					var state = Internal.GetSessionConnectionInfo( ref netIdentity, ref info, ref status );

					if ( state != ConnectionState.Connected )
					{
						CloseSessionWithUser( ref netIdentity );
					}
				}

				return result;
			}
		}

		public static int Receive( int channel = 0, int bufferSize = 32, bool receiveToEnd = true )
		{
			int processed = 0;
			IntPtr messageBuffer = Marshal.AllocHGlobal( IntPtr.Size * bufferSize );

			try
			{
				processed = Internal.ReceiveMessagesOnChannel( channel, messageBuffer, bufferSize );

				for ( int i = 0; i < processed; i++ )
				{
					ReceiveMessage( Marshal.ReadIntPtr( messageBuffer, i * IntPtr.Size ) );
				}
			}
			finally
			{
				Marshal.FreeHGlobal( messageBuffer );
			}

			//
			// Overwhelmed our buffer, keep going
			//
			if ( receiveToEnd && processed == bufferSize )
				processed += Receive( bufferSize );

			return processed;
		}
		
		internal static unsafe void ReceiveMessage( IntPtr msgPtr )
		{
			var msg = Marshal.PtrToStructure<NetMsg>( msgPtr );
			try
			{
				OnMessage?.Invoke( msg.Identity, msg.DataPtr, msg.DataSize, msg.Channel );
			}
			finally
			{
				//
				// Releases the message
				//
				NetMsg.InternalRelease( (NetMsg*) msgPtr );
			}
		}
	}

}

