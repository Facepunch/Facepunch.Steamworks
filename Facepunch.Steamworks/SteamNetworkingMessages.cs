using Steamworks.Data;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;

namespace Steamworks
{
	public enum SteamNetworkingOptions
	{
		Unreliable = 0,
		NoNagle = 1,
		UnreliableNoNagle = Unreliable | NoNagle,
		NoDelay = 4,
		UnreliableNoDelay = Unreliable | NoDelay | NoNagle,
		Reliable = 8,
		ReliableNoNagle = Reliable | NoNagle
	}

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
			Dispatch.Install<SteamNetworkingMessagesSessionRequest_t>( x => OnSessionRequest?.Invoke( x.DentityRemote), server );
			Dispatch.Install<SteamNetworkingMessagesSessionFailed_t>( x => OnSessionFailed?.Invoke( x.Nfo), server );
		}

		public static Action<NetIdentity> OnSessionRequest;

		public static Action<ConnectionInfo> OnSessionFailed;

		public static bool AcceptSessionWithUser( ref NetIdentity identity ) => Internal.AcceptSessionWithUser( ref identity );
		public static bool CloseSessionWithUser( ref NetIdentity identity ) => Internal.CloseSessionWithUser( ref identity );
		public static bool CloseChannelWithUser( ref NetIdentity identity, int channel ) => Internal.CloseChannelWithUser( ref identity, channel );
		public static ConnectionState GetSessionConnectionInfo( ref NetIdentity identity, ref ConnectionInfo info, ref ConnectionStatus status ) => Internal.GetSessionConnectionInfo( ref identity, ref info, ref status );

		public static unsafe Result SendMessageToUser( ref NetIdentity identity, byte[] data, SteamNetworkingOptions flags, int channel)
		{
			uint length = (uint)data.Length;
			fixed ( byte* p = data )
			{
				IntPtr[] pubData = new IntPtr[] { (IntPtr)p };
				
				return Internal.SendMessageToUser( ref identity, pubData, length, (int)flags, channel );
			}
		}

		public unsafe static int ReceiveMessagesOnChannel( int channel, Action<byte[], int, long , long , int> callback, int bufferSize = 32, bool receiveToEnd = true )
		{
			if ( bufferSize < 1 || bufferSize > 256 ) throw new ArgumentOutOfRangeException( nameof( bufferSize ) );

			int totalProcessed = 0;
			NetMsg** messageBuffer = stackalloc NetMsg*[bufferSize];

			while ( true )
			{
				int processed = Internal.ReceiveMessagesOnChannel(channel, new IntPtr( &messageBuffer[0] ), bufferSize );
				totalProcessed += processed;

				try
				{
					for ( int i = 0; i < processed; i++ )
					{
						ReceiveMessage( ref messageBuffer[i], callback );
					}
				}
				catch
				{
					for ( int i = 0; i < processed; i++ )
					{
						if ( messageBuffer[i] != null )
						{
							NetMsg.InternalRelease( messageBuffer[i] );
						}
					}

					throw;
				}


				//
				// Keep going if receiveToEnd and we filled the buffer
				//
				if ( !receiveToEnd || processed < bufferSize )
					break;
			}

			return totalProcessed;
		}

		internal unsafe static void ReceiveMessage( ref NetMsg* msg, Action<byte[], int, long, long, int> callback )
		{
			try
			{
				byte[] bytes = new byte[msg->DataSize];
				Marshal.Copy(msg->DataPtr, bytes, 0, msg->DataSize);
				callback( bytes, msg->DataSize, msg->RecvTime, msg->MessageNumber, msg->Channel );
			}
			finally
			{
				//
				// Releases the message
				//
				NetMsg.InternalRelease( msg );
				msg = null;
			}
		}
	}
}
