using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Steamworks.Data;

namespace Steamworks
{
	public class SteamNetworkingMessages : SteamSharedClass<SteamNetworkingMessages>
	{
		internal static ISteamNetworkingMessages Internal => Interface as ISteamNetworkingMessages;

		public static event Func<NetIdentity, bool> OnSessionRequested;

		public static event Action<ConnectionInfo> OnSessionFailed;

		public static event Action<NetIdentity, IntPtr, int, int> OnMessage; 

		public static HashSet<SteamId> Sessions;

		internal override bool InitializeInterface( bool server )
		{
			SetInterface( server, new ISteamNetworkingMessages( server ) );
			if ( Interface.Self == IntPtr.Zero ) return false;

			InstallEvents( server );

			Sessions = new HashSet<SteamId>();
			return true;
		}
		
		private void InstallEvents( bool server )
		{
			Dispatch.Install<SteamNetworkingMessagesSessionRequest_t>( SessionRequested, server );
			Dispatch.Install<SteamNetworkingMessagesSessionFailed_t>( SessionFailed, server );
		}

		private static void SessionRequested( SteamNetworkingMessagesSessionRequest_t data )
		{
			if ( OnSessionRequested == null || OnSessionRequested.Invoke( data.DentityRemote ) )
			{
				Sessions.Add( data.DentityRemote.SteamId );
				AcceptSessionWithUser( ref data.DentityRemote );
			}
		}

		private static void SessionFailed( SteamNetworkingMessagesSessionFailed_t data )
		{
			OnSessionFailed?.Invoke( data.Nfo );
		}

		public static void Update()
		{
			Receive();
		}

		public static void Cleanup()
		{
			foreach ( var steamId in Sessions )
			{
				NetIdentity netIdentity = steamId;
				Internal.CloseSessionWithUser( ref netIdentity );
			}
			
			Sessions.Clear();
		}

		public static Result SendMessageToUser( ref NetIdentity netIdentity, byte[] data, int size, SendType sendType = SendType.Reliable, int channel = 0 )
		{
			if( size <= 0)
				throw new ArgumentException( "`size` cannot be zero", nameof( size ) );
			
			GCHandle handle = GCHandle.Alloc(data, GCHandleType.Pinned);
			IntPtr pData = handle.AddrOfPinnedObject();

			var result = Internal.SendMessageToUser( ref netIdentity, [pData], (uint)size, (int)sendType, channel );
			
			handle.Free();

			switch ( result )
			{
				case Result.OK:
					Sessions.Add( netIdentity.SteamId );
					break;
				default:
					var info = new ConnectionInfo();

					var status = new ConnectionStatus();

					var state = Internal.GetSessionConnectionInfo( ref netIdentity, ref info, ref status );
					
					if ( state != ConnectionState.Connected )
					{
						Sessions.Remove( netIdentity.SteamId );
						CloseSessionWithUser( ref netIdentity );
					}
					break;
			}
			

			return result;
		}

		public static bool AcceptSessionWithUser( ref NetIdentity netIdentity )
		{
			return Internal.AcceptSessionWithUser( ref netIdentity );
		}

		public static bool CloseSessionWithUser( ref NetIdentity netIdentity )
		{
			Sessions.Remove( netIdentity.SteamId );
			
			return Internal.CloseSessionWithUser( ref netIdentity );
		}

		public static bool CloseChannelWithUser( ref NetIdentity netIdentity, int channel )
		{
			return Internal.CloseChannelWithUser( ref netIdentity, channel );
		}

		internal static int Receive( int bufferSize = 32, bool receiveToEnd = true )
		{
			int processed = 0;
			IntPtr messageBuffer = Marshal.AllocHGlobal( IntPtr.Size * bufferSize );

			try
			{
				processed += Internal.ReceiveMessagesOnChannel( 0, messageBuffer, bufferSize );

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

